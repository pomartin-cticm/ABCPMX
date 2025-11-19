Imports System.Net
Imports System.Net.Mime.MediaTypeNames
Imports System.Security.Cryptography
Imports PMXMoteur2

Public Class Frm_MaillageSlim

#Region " Attributs "

    Dim lBuild As Boolean

    Dim locMail As cls_MaillageSlimFloor

    Dim myParAff As Struc_Affichage

    Dim iSelect As Integer = -1
    Dim jSelect As Integer = -1

    Dim FontFrm As Font

    Dim zCarMail As Decimal
    Dim lContourSeul As Boolean = False
    Dim lCalculTh As Boolean = False

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_MaillageSlim_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangues()
        GestionStyle()
        PrepareFenetre()
        AfficheInfoMaillage()
        RemplirComboTherm()

        lBuild = False

    End Sub

    Private Sub GestionLangues()

        Me.Text = "Maillage"
        Me.btn_OK.Text = "Fermer"
        Me.lbl_Maillage.Text = "Maillage"

        Me.chk_CoutourSeul.Text = "Afficher le contour des matériaux uniquement"

        Me.lbl_NbMailles.Text = "Nombre de mailles :"

        Me.lbl_SuivantX.Text = "// X"
        Me.lbl_SuivantY.Text = "// Y"

        Me.chk_CalculTherm.Text = "Calcul thermique"

    End Sub

    Private Sub RemplirComboTherm()

        Me.cmb_TempR.Items.Clear()

        Me.cmb_TempR.Items.Add("0")
        For i As Integer = 0 To cls_VerifFeuSlimAcier.TimeSteps.GetUpperBound(0)
            Me.cmb_TempR.Items.Add("R" & cls_VerifFeuSlimAcier.TimeSteps(i).ToString)
        Next

        Me.cmb_TempR.SelectedIndex = 0
    End Sub

    Private Sub AfficheInfoMaillage()

        Me.txt_NbMailX.Text = locMail.nb_cells_y.ToString
        Me.txt_NbMailY.Text = locMail.nb_cells_z.ToString

        Me.chk_CoutourSeul.Checked = lContourSeul
        Me.chk_CalculTherm.Checked = lCalculTh
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Maillage.BackColor = CouleurBackBandeaux
        Me.lbl_Maillage.ForeColor = CouleurForeBandeaux

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        PrepareTextBoxDipo(Me.txt_NbMailX, False)
        PrepareTextBoxDipo(Me.txt_NbMailY, False)

    End Sub

    Private Sub PrepareFenetre()

        Dim bEffG, bEffD, bApp As Decimal

        Me.img_Maillage.Dock = DockStyle.Fill

        locMail = New cls_MaillageSlimFloor

        If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
            bEffG = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeD1 / 2
        Else
            bEffG = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeD1
        End If
        bEffD = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeD2 / 2
        bApp = 0.05

        locMail.Creation_maillage_2D_poutre_plancher_mince(MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA, MyProjet.Poutres(MyProjet.IndEnCours).Dalle,
                                                           MyProjet.Poutres(MyProjet.IndEnCours).ParamFeu,
                                                           bEffG, bEffD, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire, bApp)

    End Sub

#End Region

#Region " Evènements "

    Private Sub Frm_MaillageSlim_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.img_Maillage.Invalidate()
    End Sub
    Private Sub chk_CoutourSeul_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CoutourSeul.CheckedChanged
        lContourSeul = Me.chk_CoutourSeul.Checked
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub chk_CalculTherm_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CalculTherm.CheckedChanged
        lCalculTh = Me.chk_CalculTherm.Checked
        MAJI_CalculTh()
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub cmb_TempR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TempR.SelectedIndexChanged

        MAJI_CalculTh()
        Me.img_Maillage.Invalidate()

    End Sub

    Private Sub MAJI_CalculTh()

        If lCalculTh Then
            locMail.InitialiseTemp(MyProjet.Poutres(MyProjet.IndEnCours).ParamFeu.TempRef)

            Dim TargetStep As Integer = Me.cmb_TempR.SelectedIndex

            If TargetStep > 0 Then

                CalculThermique(MyProjet.Poutres(MyProjet.IndEnCours), TargetStep - 1)

            End If

        End If

    End Sub

    Private Sub CalculThermique(myBeam As cls_Poutre, iStep As Integer)

        Dim TimeTarget As Double
        Dim lCont As Boolean
        Dim TimeT As Double = 0
        Dim TempG As Double
        Dim DeltaT As Double = 0.2 ' secondes
        Dim lTargetT As Boolean = False

        Dim val_U As Double = myBeam.ParamFeu.TeneurU
        Dim lNormal As Boolean = Not myBeam.Dalle.beton.lLeger
        Dim lANF As Boolean = myBeam.ParamFeu.lANFrance
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim lRhoCVar As Boolean = myBeam.ParamFeu.lRhoCvar
        Dim RhoC As Double = myBeam.Dalle.beton.RhoC

        Dim EN_Feu As New cls_EurocodesFeu
        Dim SolveurTh As New cls_EchauffementSlimFEM

        TimeTarget = cls_VerifFeuAcier.TimeSteps(iStep) * kConvMinSec
        lCont = IsSmaller(TimeT, TimeTarget)

        Me.prb_CalculTh.Value = 0

        Do While lCont

            '# Boucle sur le temps jusqu'à obtenir la durée cible

            TimeT += DeltaT

            '# Température des gaz chauds

            TempG = EN_Feu.TemperatureGazISO(TimeT)
            lTargetT = IsSmaller(TimeT, TimeTarget)

            SolveurTh.Calcul_thermique_Poutre_plancher_mince(locMail, myBeam.ParamFeu, TimeT, DeltaT, lTargetT, TempG,
                                                             val_U, lNormal, lANF, lGeneration1, rhoc, lrhocVar)

            lCont = lTargetT

            Me.prb_CalculTh.Value = TimeT / TimeTarget * 100

        Loop

    End Sub

#End Region

#Region " Dessin Maillage "

    Private Sub img_Maillage_Paint(sender As Object, e As PaintEventArgs) Handles img_Maillage.Paint

        DessinMaillage(e.Graphics, img_Maillage.Width, img_Maillage.Height, MyProjet.Poutres(MyProjet.IndEnCours),
                       locMail, iSelect, jSelect, lContourSeul, lCalculTh)

    End Sub

    Private Sub DessinMaillage(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, myBeam As cls_Poutre, myMail As cls_MaillageSlimFloor,
                               iSelect As Integer, jSelect As Integer, lContourOnly As Boolean, lAffTh As Boolean,
                               ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   Représentation graphique du maillage d'une poutre de plancher mince
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   pWi         [E] :   Largeur de la zone de dessin
        '   pHi         [E] :   Hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre en cours
        '   myMail      [E] :   Maillage de la poutre en cours
        '   iSelect     [E] :   Indice de la maille sélectionnée en X
        '   jSelect     [E] :   Indice de la maille sélectionnée en Y
        '   lContourOnly[E] :   Indique si on affiche uniquement le contour des matériaux
        '   lAffTh      [E] :   Indique si on affiche les températures
        '------------------------------------------------------------------------------------------------------------------------------------------------

        Const kADJUST As Decimal = 0.95
        Dim i, j As Integer
        Dim xMin, yMin, xMax, yMax As Double
        Dim nbY, nbZ As Integer
        Dim dCar As Double
        Dim myPenN As New Pen(Color.Black, 1)
        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim myPenR As New Pen(Color.DarkRed, 1)

        '--( Initialisation 

        nbY = myMail.nb_cells_y
        nbZ = myMail.nb_cells_z

        '--( Paramètres d'affichage

        'If myBeam.lIntermediaire Then
        '    xMin = -myBeam.EntraxeD1 / 2
        'Else
        '    xMin = -myBeam.EntraxeD1
        'End If
        'xMax = myBeam.EntraxeD2 / 2
        'yMin = -Math.Max(myBeam.Section.ProfilA.Plat_t, myBeam.Section.ProfilA.Tfi)
        'yMax = myBeam.Dalle.zTop

        xMin = myMail.Tab_mesh_cent_y(0, 0) - myMail.Tab_mesh_y(0) / 2
        yMin = myMail.Tab_mesh_cent_z(0, 0) - myMail.Tab_mesh_z(0) / 2
        xMax = myMail.Tab_mesh_cent_y(nbY - 1, 0) + myMail.Tab_mesh_y(nbY - 1) / 2
        yMax = myMail.Tab_mesh_cent_z(0, nbZ - 1) + myMail.Tab_mesh_z(nbZ - 1) / 2

        zCarMail = 2 * yMin

        dCar = Math.Sqrt((xMax - xMin) ^ 2 + (yMax - yMin) ^ 2) / 20

        yMin = zCarMail
        yMax += dCar

        ParametresAffichage(myParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--( Représentation des mailles

        '%% Fond en couleur

        If Not lContourSeul Then
            For i = 0 To nbY - 1
                For j = 0 To nbZ - 1

                    Dessine_Maille(myGr, myParAff, myMail, i, j, iSelect, jSelect)

                Next
            Next
        End If

        '%% Coutour des zones

        For i = 0 To nbY - 1
            For j = 0 To nbZ - 1
                DessineMailleContourSeul(myGr, myParAff, myMail, i, j, iSelect, jSelect)
            Next
        Next

        '--( Affichage de la maille sélectionnée

        Dim lSelect As Boolean = (iSelect >= 0) AndAlso (jSelect >= 0)

        If lSelect Then
            Dim myColor As Color
            Dim lDessin As Boolean
            Dim ChMat As String = ""

            InfoMaille(myMail, iSelect, jSelect, myColor, lDessin, ChMat)
            AddRectangle(myGr, myPenN, xo, yo, xe, ye, myParAff)
            If lCalculTh Then
                AfficheInfoMaille(myGr, myParAff, iSelect, jSelect, ChMat, lAffTh, myMail.Tab_mesh_temp(iSelect, jSelect))
            Else
                AfficheInfoMaille(myGr, myParAff, iSelect, jSelect, ChMat, lAffTh, 0)
            End If
        End If

        '--( Représentation des largeurs 2D

        If myBeam.lIntermediaire Then

            xo = 0
            xe = myBeam.ParamFeu.bEffect2D
            yo = myBeam.Dalle.zTop + dCar / 2
            ye = yo

            AddLigne(myGr, New Pen(Color.Black, 1), xo, myBeam.Dalle.zTop, xo, ye + dCar / 4, myParAff)

            AddFleche(myGr, myPenR, xo, yo, xe, ye, myParAff, True, True)
            AddLigne(myGr, myPenR, xe, ye, xe, myBeam.Dalle.zTop, myParAff)
            AddTexte(myGr, New SolidBrush(Color.DarkRed), "2D", FontFrm, (xo + xe) / 2, ye, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)

            xe = -xe
            AddFleche(myGr, myPenR, xo, yo, xe, ye, myParAff, True, True)
            AddLigne(myGr, myPenR, xe, ye, xe, myBeam.Dalle.zTop, myParAff)
            AddTexte(myGr, New SolidBrush(Color.DarkRed), "2D", FontFrm, (xo + xe) / 2, ye, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)

        End If

    End Sub

    Private Sub DessineMailleContourSeul(ByRef myGr As Graphics, myParaff As Struc_Affichage, myMail As cls_MaillageSlimFloor, iMail As Integer, jMail As Integer,
                                         iSelect As Integer, jSelect As Integer)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   Représentation graphique d'une maille du maillage
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '------------------------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration des variables

        Dim myColor As Color

        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim lDessin As Boolean
        Dim lSelect As Boolean = (iMail = iSelect) AndAlso (jMail = jSelect)
        Dim ChMat As String = ""
        Dim myPen As New Pen(Color.Black, 1)
        Dim lOK As Boolean = False

        '--( Informations de la maille

        InfoMaille(myMail, iMail, jMail, myColor, lDessin, ChMat)

        '--( Coordonnées de la maille

        xo = myMail.Tab_mesh_cent_y(iMail, jMail) - myMail.Tab_mesh_y(iMail) / 2
        xe = myMail.Tab_mesh_cent_y(iMail, jMail) + myMail.Tab_mesh_y(iMail) / 2
        yo = myMail.Tab_mesh_cent_z(iMail, jMail) - myMail.Tab_mesh_z(jMail) / 2
        ye = myMail.Tab_mesh_cent_z(iMail, jMail) + myMail.Tab_mesh_z(jMail) / 2

        '--( Dessin des contours

        If lDessin Then

            '%% Contour gauche
            If (iMail = 0) Then
                lOK = True
            ElseIf (myMail.Tab_mesh_mat(iMail - 1, jMail) <> myMail.Tab_mesh_mat(iMail, jMail)) Then
                lOK = True
            Else
                lOK = False
            End If
            If lOK Then AddLigne(myGr, myPen, xo, yo, xo, ye, myParaff)


            '%% Contour haut
            If (jMail = myMail.nb_cells_z - 1) Then
                lOK = True
            ElseIf (myMail.Tab_mesh_mat(iMail, jMail + 1) <> myMail.Tab_mesh_mat(iMail, jMail)) Then
                lOK = True
            Else
                lOK = False
            End If
            If lOK Then AddLigne(myGr, myPen, xo, ye, xe, ye, myParaff)


            '%% Contour droite
            If (iMail = myMail.nb_cells_y - 1) Then
                lOK = True
            ElseIf (myMail.Tab_mesh_mat(iMail + 1, jMail) <> myMail.Tab_mesh_mat(iMail, jMail)) Then
                lOK = True
            Else
                lOK = False
            End If
            If lOK Then AddLigne(myGr, myPen, xe, yo, xe, ye, myParaff)

            '%% Contour bas
            If (jMail = 0) Then
                lOK = True
            ElseIf (myMail.Tab_mesh_mat(iMail, jMail - 1) <> myMail.Tab_mesh_mat(iMail, jMail)) Then
                lOK = True
            Else
                lOK = False
            End If
            If lOK Then AddLigne(myGr, myPen, xo, yo, xe, yo, myParaff)
        End If


    End Sub

    Private Sub Dessine_Maille(ByRef myGr As Graphics, myParaff As Struc_Affichage, myMail As cls_MaillageSlimFloor, iMail As Integer, jMail As Integer,
                               iSelect As Integer, jSelect As Integer)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   Représentation graphique d'une maille du maillage
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '------------------------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration des variables

        Dim myColor As Color
        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim lDessin As Boolean
        Dim lSelect As Boolean = (iMail = iSelect) AndAlso (jMail = jSelect)
        Dim ChMat As String = ""

        '--( Informations de la maille

        InfoMaille(myMail, iMail, jMail, myColor, lDessin, ChMat)

        '--( Coordonnées de la maille

        xo = myMail.Tab_mesh_cent_y(iMail, jMail) - myMail.Tab_mesh_y(iMail) / 2
        xe = myMail.Tab_mesh_cent_y(iMail, jMail) + myMail.Tab_mesh_y(iMail) / 2
        yo = myMail.Tab_mesh_cent_z(iMail, jMail) - myMail.Tab_mesh_z(jMail) / 2
        ye = myMail.Tab_mesh_cent_z(iMail, jMail) + myMail.Tab_mesh_z(jMail) / 2

        '--( Dessine la maille

        If lDessin Or lSelect Then _
        AddRectanglePlein(myGr, myColor, xo, yo, xe, ye, myParaff, lSelect)

        'If lSelect Then

        '    AfficheInfoMaille(myGr, myParaff, iMail, jMail, ChMat)

        'End If
    End Sub

    Private Sub AfficheInfoMaille(ByRef myGr As Graphics, myParaff As Struc_Affichage, iMail As Integer, jMail As Integer, ChMat As String,
                                  lTemp As Boolean, Theta As Double)

        Dim Chaine As String

        Chaine = "Maille (" & iMail.ToString & "," & jMail.ToString & ")" & " - Matériau : " & ChMat

        If lTemp Then
            Chaine &= " - Température : " & Theta.ToString("F1") & " °C"
        End If

        AddTexte(myGr, New SolidBrush(Color.Black), Chaine, FontFrm, 0, zCarMail, myParaff, HorizontalAlignment.Center, VerticalAlignement.Bottom)

    End Sub

    Private Sub InfoMaille(myMail As cls_MaillageSlimFloor, iMail As Integer, jMail As Integer,
                           ByRef myColor As Color, ByRef lDessin As Boolean, ByRef ChaineMat As String)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   Exraction des informations relatives à une maille du maillage
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   myMail      [E] :   Maillage de la poutre en cours
        '   iMail       [E] :   Indice de la maille en X
        '   jMail       [E] :   Indice de la maille en Y
        '   myColor     [S] :   Couleur associée à la maille, en fonction du matériau
        '   lDessin     [S] :   Indique si on dessine de la maille
        '   ChaineMat   [S] :   Description du matériau de la maille
        '------------------------------------------------------------------------------------------------------------------------------------------------

        Select Case myMail.Tab_mesh_mat(iMail, jMail)
            Case cls_MaillageSlimFloor.MATVIDEFERME, cls_MaillageSlimFloor.MATVIDEOUVERT
                myColor = Color.White
                lDessin = False
                ChaineMat = "Vide"

            Case cls_MaillageSlimFloor.MATACIERSEMI
                myColor = CouleurAcierNormal
                lDessin = True
                ChaineMat = "Semelle inférieure"

            Case cls_MaillageSlimFloor.MATACIERSEMS
                myColor = CouleurAcierNormal
                lDessin = True
                ChaineMat = "Semelle supérieure"

            Case cls_MaillageSlimFloor.MATACIERAME
                myColor = CouleurAcierNormal
                lDessin = True
                ChaineMat = "Ame"

            Case cls_MaillageSlimFloor.MATBETON
                myColor = CouleurBetonNormal
                lDessin = True
                ChaineMat = "Béton"

            Case cls_MaillageSlimFloor.MATACIERPLAT
                myColor = BleuCTICM
                lDessin = True
                ChaineMat = "Plat"

            Case cls_MaillageSlimFloor.MATARMA
                myColor = CouleurArmaSelect
                lDessin = True
                ChaineMat = "Armatures"

            Case cls_MaillageSlimFloor.MATACIERSOUD
                myColor = CouleurArmaNormal
                lDessin = True
                ChaineMat = "Soudures"

        End Select

    End Sub

    Private Sub img_Maillage_MouseMove(sender As Object, e As MouseEventArgs) Handles img_Maillage.MouseMove

        Dim xSouris, ySouris As Single

        xSouris = e.X
        ySouris = e.Y

        WhereIsTheMouse(xSouris, ySouris)

    End Sub

    Private Sub WhereIsTheMouse(xSouris As Single, ySouris As Single)

        Dim xReel, yReel As Single
        Dim lCont As Boolean
        Dim i0, j0 As Integer
        Dim lTrouve As Boolean = False
        Dim iSelectBack, jSelectBack As Integer

        xReel = XUnivers(MyParAff, xSouris)
        yReel = YUnivers(myParAff, ySouris)
        iSelectBack = iSelect
        jSelectBack = jSelect

        '--( RechercheLangue position X

        i0 = 0
        lCont = IsGreater(xReel, locMail.Tab_mesh_cent_y(i0, 0) + locMail.Tab_mesh_y(i0) / 2)

        If lCont Then
            Do While lCont And i0 < locMail.nb_cells_y - 1
                i0 += 1
                lCont = IsGreater(xReel, locMail.Tab_mesh_cent_y(i0, 0) + locMail.Tab_mesh_y(i0) / 2)
            Loop
            lTrouve = Not lCont
        Else
            lTrouve = IsGreater(xReel, locMail.Tab_mesh_cent_y(i0, 0) - locMail.Tab_mesh_y(i0) / 2)
        End If

        '--( Recherche de la position Y

        If lTrouve Then
            lTrouve = False

            j0 = 0
            lCont = IsGreater(yReel, locMail.Tab_mesh_cent_z(0, j0) + locMail.Tab_mesh_z(j0) / 2)

            If lCont Then
                Do While lCont And j0 < locMail.nb_cells_z - 1
                    j0 += 1
                    lCont = IsGreater(yReel, locMail.Tab_mesh_cent_z(0, j0) + locMail.Tab_mesh_z(j0) / 2)
                Loop
                lTrouve = Not lCont
            Else
                lTrouve = IsGreater(yReel, locMail.Tab_mesh_cent_z(0, j0) - locMail.Tab_mesh_z(j0) / 2)
            End If

        End If

        '--( Traitement du résultat de la recherche

        If lTrouve Then
            iSelect = i0
            jSelect = j0
        Else
            iSelect = -1
            jSelect = -1
        End If

        If (iSelect <> iSelectBack) Or (jSelect <> jSelectBack) Then
            img_Maillage.Invalidate()
        End If

    End Sub



#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

    Private Sub img_Maillage_Click(sender As Object, e As EventArgs) Handles img_Maillage.Click

    End Sub


#End Region

End Class