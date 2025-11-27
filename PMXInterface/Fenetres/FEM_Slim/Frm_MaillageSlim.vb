Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Net
Imports System.Net.Mime.MediaTypeNames
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.Logging
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
    Dim lCalculTh As Boolean = False            ' Indique si on réalise le calcul thermique
    Dim lCalculThTermine As Boolean = False     ' Indique si le calcul thermique est terminé

    Dim lAffChTh As Boolean = True              ' Indique si on affiche le champ thermique
    Dim lLissage As Boolean = False             ' Indique si le lissage de couleur est réalisé
    Dim lEchelleTempPerso As Boolean = False    ' Indique si on utilise une échelle de température personnelle pour les couleurs (val min/max trouvée automatiquement ou données)
    Dim lAff2DUniquement As Boolean = False     ' Indique si on affiche uniquement les mailles calculées en 2D (les mailles centrales)

    Dim lAffPoutre As Boolean = True            ' Indique si on affiche la poutre
    Dim lAffDalle As Boolean = True             ' Indique si on affiche la dalle
    Dim LaffArmature As Boolean = True          ' Indique si on affiche l'armature

    Dim lAffChThPoutre As Boolean = True        ' Indique si on affiche le champ thermique de la poutre
    Dim lAffChThDalle As Boolean = True         ' Indique si on affiche le champ thermique de la dalle
    Dim lAffChThVideOuvert As Boolean = True    ' Indique si on affiche le champ thermique du vide ouvert = air pas enfermé
    Dim LaffChThArmature As Boolean = True      ' Indique si on affiche le champ thermique de l'armature

    Dim tempMin As Double                       'température minimale sur toutes les mailles
    Dim tempMax As Double                       'température maximale sur toutes les mailles

    Dim Tab_Couleurs() As Color = {Color.DarkBlue, Color.Blue, Color.MediumTurquoise, Color.LightGreen, Color.Yellow, Color.Orange, Color.Red, Color.DarkRed}
    Dim Tab_IntervalleCouleurs(Tab_Couleurs.Length - 1) As Double




#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_MaillageSlim_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangues()
        GestionStyle()
        PrepareFenetre()
        AfficheInfoMaillage()
        RemplirComboTherm()
        MAJI_Images()
        MAJI_chk_ChampTherm()
        chk_Lissage_Couleur.Enabled = False
        lBuild = False

    End Sub

    Private Sub GestionLangues()

        Me.Text = "Maillage"
        Me.btn_OK.Text = "Fermer"
        Me.lbl_Maillage.Text = "Maillage"

        Me.chk_CoutourSeul.Text = "Afficher le contour des matériaux uniquement"

        Me.lbl_NbMailles.Text = "Nombre de mailles :"
        Me.chk_AffChampTherm.Text = "Afficher le champ thermique"
        Me.chk_Lissage_Couleur.Text = "Lissage"
        Me.chk_EchelleTempPerso.Text = "Utiliser une échelle de température perso"
        Me.chk_Aff2D.Text = "Afficher uniquement les éléments 2D"

        Me.lbl_poutre.Text = "Poutre"
        Me.lbl_dalle.Text = "Dalle"
        Me.lbl_vide.Text = "Vide ouvert"
        Me.lbl_arma.Text = "Armature"

        Me.chk_AffPoutre.Text = ""
        Me.chk_AffDalle.Text = ""
        Me.chk_AffArmature.Text = ""

        Me.chk_AffChThPoutre.Text = ""
        Me.chk_AffChThDalle.Text = ""
        Me.chk_AffChThVideOuvert.Text = ""
        Me.chk_AffChThArmature.Text = ""

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
        Me.chk_AffChampTherm.Checked = lAffChTh
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
        Me.img_Legende.Invalidate()
    End Sub

    Private Sub chk_CoutourSeul_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CoutourSeul.CheckedChanged
        lContourSeul = Me.chk_CoutourSeul.Checked
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub chk_CalculTherm_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CalculTherm.CheckedChanged
        lCalculTh = Me.chk_CalculTherm.Checked
        MAJI_CalculTh()
        MAJI_chk_ChampTherm()
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub chk_ChampTherm_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AffChampTherm.CheckedChanged
        lAffChTh = Me.chk_AffChampTherm.Checked
        MAJI_chk_ChThOptions()
        MAJI_chk_AfficherElements()
        MAJI_Images()
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub chk_ChThOptions_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Lissage_Couleur.CheckedChanged, chk_EchelleTempPerso.CheckedChanged, chk_Aff2D.CheckedChanged
        lLissage = chk_Lissage_Couleur.Checked
        lEchelleTempPerso = chk_EchelleTempPerso.Checked
        lAff2DUniquement = chk_Aff2D.Checked
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub chk_AfficherElementsCheckedChanged(sender As Object, e As EventArgs) Handles chk_AffPoutre.CheckedChanged, chk_AffDalle.CheckedChanged, chk_AffArmature.CheckedChanged
        lAffPoutre = chk_AffPoutre.Checked
        lAffDalle = chk_AffDalle.Checked
        LaffArmature = chk_AffArmature.Checked
        MAJI_chk_ChampTherm_Autres()
        Me.img_Maillage.Invalidate()
    End Sub

    Private Sub chk_ChampTherm_AutresCheckedChanged(sender As Object, e As EventArgs) Handles chk_AffChThDalle.CheckedChanged, chk_AffChThPoutre.CheckedChanged, chk_AffChThVideOuvert.CheckedChanged, chk_AffChThArmature.CheckedChanged
        lAffChThPoutre = chk_AffChThPoutre.Checked
        lAffChThDalle = chk_AffChThDalle.Checked
        lAffChThVideOuvert = chk_AffChThVideOuvert.Checked
        LaffChThArmature = chk_AffChThArmature.Checked
        Me.img_Legende.Invalidate() 'pour déclencher le calcul de l'échelle des températures (si le calcul à l'incendie est déjà fait)
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

                'sur la dalle
                locMail.Tab_mesh_temp(20, 31) = 1800
                locMail.Tab_mesh_temp(20, 30) = 1600
                locMail.Tab_mesh_temp(20, 29) = 1500
                locMail.Tab_mesh_temp(20, 28) = 1400
                locMail.Tab_mesh_temp(20, 27) = 1300
                locMail.Tab_mesh_temp(20, 26) = 1200
                locMail.Tab_mesh_temp(20, 25) = 1100
                locMail.Tab_mesh_temp(20, 24) = 1000
                locMail.Tab_mesh_temp(20, 23) = 750
                locMail.Tab_mesh_temp(20, 22) = 650
                locMail.Tab_mesh_temp(20, 21) = 550
                locMail.Tab_mesh_temp(20, 20) = 450
                locMail.Tab_mesh_temp(20, 19) = 350
                locMail.Tab_mesh_temp(20, 18) = 250
                locMail.Tab_mesh_temp(20, 17) = 150
                locMail.Tab_mesh_temp(20, 16) = 100
                locMail.Tab_mesh_temp(20, 15) = 0


                'sur l'ame
                locMail.Tab_mesh_temp(50, 60) = 200
                locMail.Tab_mesh_temp(50, 59) = 190
                locMail.Tab_mesh_temp(50, 58) = 180
                locMail.Tab_mesh_temp(50, 57) = 170
                locMail.Tab_mesh_temp(50, 56) = 160
                locMail.Tab_mesh_temp(50, 55) = 150
                locMail.Tab_mesh_temp(50, 54) = 140
                locMail.Tab_mesh_temp(50, 53) = 130
                locMail.Tab_mesh_temp(50, 52) = 120
                locMail.Tab_mesh_temp(50, 51) = 110
                locMail.Tab_mesh_temp(50, 50) = 100
                locMail.Tab_mesh_temp(50, 49) = 90
                locMail.Tab_mesh_temp(50, 48) = 80
                locMail.Tab_mesh_temp(50, 47) = 70
                locMail.Tab_mesh_temp(50, 46) = 60
                locMail.Tab_mesh_temp(50, 45) = 50
                locMail.Tab_mesh_temp(50, 44) = 40
                locMail.Tab_mesh_temp(50, 43) = 30
                locMail.Tab_mesh_temp(50, 42) = 20
                locMail.Tab_mesh_temp(50, 41) = 10

                CalculThermique(MyProjet.Poutres(MyProjet.IndEnCours), TargetStep - 1)

                lCalculThTermine = True

                MAJI_chk_ChampTherm()
                PrepareEchelleTemp()
                img_Legende.Invalidate()

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
                                                             val_U, lNormal, lANF, lGeneration1, RhoC, lRhoCVar)

            lCont = lTargetT

            Me.prb_CalculTh.Value = TimeT / TimeTarget * 100

        Loop

    End Sub

    Private Sub MAJI_chk_ChampTherm()
        chk_AffChampTherm.Enabled = chk_CalculTherm.Checked And Me.cmb_TempR.SelectedIndex > 0
        chk_AffChampTherm.Checked = chk_CalculTherm.Checked And Me.cmb_TempR.SelectedIndex > 0
    End Sub

    Private Sub MAJI_chk_ChThOptions()
        chk_Lissage_Couleur.Enabled = Me.chk_AffChampTherm.Checked
        chk_EchelleTempPerso.Enabled = Me.chk_AffChampTherm.Checked
        chk_Aff2D.Enabled = Me.chk_AffChampTherm.Checked
    End Sub

    Private Sub MAJI_chk_AfficherElements()
        lbl_elements.Enabled = lAffChTh
        lbl_ch_th.Enabled = lAffChTh
        lbl_poutre.Enabled = lAffChTh
        lbl_dalle.Enabled = lAffChTh
        lbl_vide.Enabled = lAffChTh
        lbl_arma.Enabled = lAffChTh

        chk_AffPoutre.Enabled = lAffChTh
        chk_AffPoutre.Checked = lAffChThPoutre

        chk_AffDalle.Enabled = lAffChTh
        chk_AffDalle.Checked = lAffDalle

        chk_AffArmature.Enabled = lAffChTh
        chk_AffArmature.Checked = LaffArmature

        MAJI_chk_ChampTherm_Autres()

    End Sub

    Private Sub MAJI_chk_ChampTherm_Autres()
        'si un élément n'est pas affiché on ne peut afficher son champ thermique, donc on désactive le btn et on le décoche

        chk_AffChThPoutre.Enabled = lAffChTh And chk_AffPoutre.Checked
        chk_AffChThPoutre.Checked = lAffChThPoutre

        chk_AffChThDalle.Enabled = lAffChTh And chk_AffDalle.Checked
        chk_AffChThDalle.Checked = lAffChThDalle

        chk_AffChThVideOuvert.Enabled = lAffChTh And lAffChTh
        chk_AffChThVideOuvert.Checked = lAffChThVideOuvert

        chk_AffChThArmature.Enabled = lAffChTh And chk_AffArmature.Checked
        chk_AffChThArmature.Checked = LaffChThArmature

    End Sub

    Private Sub PrepareEchelleTemp()
        Dim elemPoutre() As Integer = {cls_MaillageSlimFloor.MATACIERPLAT, cls_MaillageSlimFloor.MATACIERSEMI, cls_MaillageSlimFloor.MATACIERAME, cls_MaillageSlimFloor.MATACIERSEMS, cls_MaillageSlimFloor.MATACIERSOUD}
        tempMin = 10000
        tempMax = 0
        For i = 0 To locMail.nb_cells_y - 1
            For j = 0 To locMail.nb_cells_z - 1

                'si le champ thermique de l'élément n'est pas affiché les températures de ce dernier ne sont pas prises en compte pour l'échelle
                'poutre
                If lAffChThPoutre And elemPoutre.Contains(locMail.Tab_mesh_mat(i, j)) Then
                    checkMinMax(i, j)
                End If

                'dalle
                If lAffChThDalle And locMail.Tab_mesh_mat(i, j) = cls_MaillageSlimFloor.MATBETON Then
                    checkMinMax(i, j)
                End If

                'vide
                If lAffChThVideOuvert And locMail.Tab_mesh_mat(i, j) = cls_MaillageSlimFloor.MATVIDEOUVERT Then
                    checkMinMax(i, j)
                End If

                'armature
                If LaffChThArmature And locMail.Tab_mesh_mat(i, j) = cls_MaillageSlimFloor.MATARMA Then
                    checkMinMax(i, j)
                End If

                'si aucun champs thermiques n'est selectionné, on prend en compte les quatres éléments
                If Not lAffChThPoutre And Not lAffChThDalle And Not lAffChThVideOuvert And Not LaffChThArmature Then
                    checkMinMax(i, j)
                End If

            Next
        Next

        Dim nbInterv As Double = Tab_IntervalleCouleurs.Length


        Tab_IntervalleCouleurs(0) = tempMin
        Tab_IntervalleCouleurs(nbInterv - 1) = tempMax

        Dim tempInt As Double
        For i = 1 To nbInterv - 2
            tempInt = tempMin + (i / nbInterv * (tempMax - tempMin))
            Tab_IntervalleCouleurs(i) = tempInt
        Next
    End Sub

    Private Sub checkMinMax(iMail As Integer, jMail As Integer)
        If (locMail.Tab_mesh_temp(iMail, jMail) < tempMin) Then
            tempMin = locMail.Tab_mesh_temp(iMail, jMail)
        End If
        If (locMail.Tab_mesh_temp(iMail, jMail) > tempMax) Then
            tempMax = locMail.Tab_mesh_temp(iMail, jMail)
        End If
    End Sub

#End Region

#Region " Dessin Maillage "

    Private Sub MAJI_Images()

        Dim lOKTh As Boolean

        lOKTh = locMail.Tab_mesh_temp IsNot Nothing

        'If lAffChTh And lOKTh Then
        '    Me.tlp_Images.RowStyles(1).Height = 50
        'Else
        '    Me.tlp_Images.RowStyles(1).Height = 0
        'End If

    End Sub

    Private Sub img_Legende_Paint(sender As Object, e As PaintEventArgs) Handles img_Legende.Paint

        If lCalculThTermine Then
            PrepareEchelleTemp()
            DessinLegende(e.Graphics, img_Legende.Width, img_Legende.Height)
        End If
    End Sub

    Private Sub DessinLegende(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   20/11/25 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   Représentation de la légende pour les champs thermiques
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   pWi         [E] :   Largeur de la zone de dessin
        '   pHi         [E] :   Hauteur de la zone de dessin
        '------------------------------------------------------------------------------------------------------------------------------------------------

        Const kADJUST As Decimal = 0.95
        Dim xMin, yMin, xMax, yMax As Double
        Dim LegParaff As Struc_Affichage
        Dim dCar As Double
        Const LL As Double = 100
        Dim H As Double = LL * pHi / pWi
        Dim nbColors As Integer = Tab_Couleurs.GetUpperBound(0) + 1

        dCar = Math.Sqrt((LL) ^ 2 + (H) ^ 2) / 50
        xMin = 0
        yMin = 0
        xMax = LL
        yMax = H + dCar

        ParametresAffichage(LegParaff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, 0, 0, kADJUST)

        For i As Integer = 0 To nbColors - 2
            Dim xo, yo As Double
            Dim xe, ye As Double
            xo = xe
            xe = (i + 1) * LL / (nbColors - 1)
            yo = 0
            ye = H
            'AddRectanglePlein(myGr, Tab_Couleurs(i), xo, yo, xe, ye, LegParaff, False)

            AddRectanglePleinGradient(myGr, Tab_Couleurs(i), Tab_Couleurs(i + 1), xo, yo, xe, ye, LegParaff)
        Next

        For i As Integer = 0 To nbColors - 1
            Dim xo, yo As Double
            Dim xe, ye As Double
            xo = i * LL / (nbColors - 1)
            xe = xo
            yo = H + dCar / 2
            ye = H
            AddLigne(myGr, New Pen(Color.Black, 1), xo, yo, xe, ye, LegParaff)

            Dim chaine = Int(Tab_IntervalleCouleurs(i))

            AddTexte(myGr, New SolidBrush(Color.Black), chaine, FontFrm, xo, yo, LegParaff,
                       HorizontalAlignment.Center, VerticalAlignement.Top)
        Next

        Dim CompRed As Byte = Tab_Couleurs(0).R
        Dim CompGreen As Byte = Tab_Couleurs(0).G
        Dim CompBlue As Byte = Tab_Couleurs(0).B

        Dim MyClolor As Color = Color.FromArgb(CompRed, CompGreen, CompBlue)
    End Sub

    Private Sub img_Maillage_Paint(sender As Object, e As PaintEventArgs) Handles img_Maillage.Paint

        'Dim lOKTh As Boolean

        'lOKTh = locMail.Tab_mesh_temp IsNot Nothing

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

        If Not lAffChTh Then        'si on affiche pas le champ thermique -> affichage normal
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
        Else                                'si on affiche le champ thermique on vérifie si on doit dessiner la maille
            If Not lContourSeul Then
                For i = 0 To nbY - 1
                    For j = 0 To nbZ - 1
                        If DoWeDraw(myMail, i, j) Then
                            If lLissage Then
                                Dessine_Maille_Lissage(myGr, myParAff, myMail, i, j, iSelect, jSelect)
                            Else
                                Dessine_Maille(myGr, myParAff, myMail, i, j, iSelect, jSelect)
                            End If
                        End If
                    Next
                Next
            End If

            '%% Coutour des zones

            For i = 0 To nbY - 1
                For j = 0 To nbZ - 1
                    If DoWeDraw(myMail, i, j) Then DessineMailleContourSeul(myGr, myParAff, myMail, i, j, iSelect, jSelect)
                Next
            Next
        End If


        '--( Affichage températures des gazs chaud si l'option est activée
        If lAffChThVideOuvert Then
            IndiquerTempGaz(myGr, myParAff, myMail, img_Legende.Width, img_Legende.Height)
        End If



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

    Private Sub Dessine_Maille_Lissage(ByRef myGr As Graphics, myParaff As Struc_Affichage, myMail As cls_MaillageSlimFloor,
                                       iMail As Integer, jMail As Integer, iSelect As Integer, jSelect As Integer)
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   26/11/25 :  Création - BEB
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   Représentation graphique d'une maille du maillage avec lissage des couleurs 
        '------------------------------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '------------------------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration des variables

        Dim myColor As Color
        Dim x1, y1 As Double
        Dim x2, y2 As Double

        Dim lDessin As Boolean
        Dim lSelect As Boolean = (iMail = iSelect) AndAlso (jMail = jSelect)
        Dim ChMat As String = ""

        'Dim points As Point()
        Dim coordonnees As Double()
        Dim couleurs As Color()
        Dim coulCentre As Color
        Dim tempCoins As Double()

        '--( Informations de la maille
        InfoMaille(myMail, iMail, jMail, myColor, lDessin, ChMat)

        coulCentre = myColor


        '--( Coordonnées de la maille
        x1 = myMail.Tab_mesh_cent_y(iMail, jMail) - myMail.Tab_mesh_y(iMail) / 2
        x2 = myMail.Tab_mesh_cent_y(iMail, jMail) + myMail.Tab_mesh_y(iMail) / 2
        y1 = myMail.Tab_mesh_cent_z(iMail, jMail) - myMail.Tab_mesh_z(jMail) / 2
        y2 = myMail.Tab_mesh_cent_z(iMail, jMail) + myMail.Tab_mesh_z(jMail) / 2


        'Debug.WriteLine("Maille " & iMail & " ; " & jMail)
        'Debug.WriteLine(x1 & " , " & y2 & " , " & x2 & " , " & y2 & " , " & x2 & " , " & y1 & " , " & x1 & " , " & y1)



        'points = {New Point(x1, y2), New Point(x2, y2), New Point(x2, y1), New Point(x1, y1)}
        coordonnees = {x1, y2, x2, y2, x2, y1, x1, y1}

        '--( Calcul des températures des coins
        tempCoins = CalculTempCoins(myMail, myMail.Tab_mesh_temp(iMail, jMail), iMail, jMail)

        '--( Traduction en couleur
        couleurs = {AttributionCouleurChTh(tempCoins(0)), AttributionCouleurChTh(tempCoins(1)), AttributionCouleurChTh(tempCoins(2)), AttributionCouleurChTh(tempCoins(3))}

        '--( Dessine la maille
        If lDessin Or lSelect Then _
        AddRectanglePleinGradientPath(myGr, myParaff, coordonnees, couleurs, coulCentre)

    End Sub

    Private Function CalculTempCoins(myMail As cls_MaillageSlimFloor, tempMaille As Double, iMail As Integer, jMail As Integer) As Double()
        Dim tempCoins As Double() = New Double(3) {}
        Dim voisinGauche As Boolean
        Dim voisinDroite As Boolean
        Dim voisinHaut As Boolean
        Dim voisinBas As Boolean
        Dim voisinGaucheTemp As Double
        Dim voisinDroiteTemp As Double
        Dim voisinHautTemp As Double
        Dim voisinBasTemp As Double

        '   1 ______ 2
        '    |      |  
        '    |      |      
        '    |______| 
        '   4        3

        'on regarde où il y a une 'frontière' (i.e différence de matériaux ou rien)
        If (iMail = 0) Then                             'frontière à gauche
            voisinGauche = False
        ElseIf (myMail.Tab_mesh_mat(iMail - 1, jMail) = myMail.Tab_mesh_mat(iMail, jMail)) Then
            voisinGauche = True
            voisinGaucheTemp = myMail.Tab_mesh_temp(iMail - 1, jMail)
        End If


        If (jMail = 0) Then                             'frontière en bas
            voisinBas = False
        ElseIf (myMail.Tab_mesh_mat(iMail, jMail - 1) = myMail.Tab_mesh_mat(iMail, jMail)) Then
            voisinBas = True
            voisinBasTemp = myMail.Tab_mesh_temp(iMail, jMail - 1)
        End If


        If (iMail = myMail.nb_cells_y - 1) Then         'frontière à droite
            voisinDroite = False
        ElseIf (myMail.Tab_mesh_mat(iMail + 1, jMail) = myMail.Tab_mesh_mat(iMail, jMail)) Then

            voisinDroite = True
            voisinDroiteTemp = myMail.Tab_mesh_temp(iMail + 1, jMail)
        End If


        If (jMail = myMail.nb_cells_z - 1) Then         'frontière en haut
            voisinHaut = False
        ElseIf (myMail.Tab_mesh_mat(iMail, jMail + 1) = myMail.Tab_mesh_mat(iMail, jMail)) Then
            voisinHaut = True
            voisinHautTemp = myMail.Tab_mesh_temp(iMail, jMail + 1)
        End If


        'coin 1 = Haut Gauche
        If voisinHaut And voisinGauche Then
            'moy(maille, voisinHaut, voisinGauche)
            tempCoins(0) = Moyenne(tempMaille, voisinHautTemp, voisinGaucheTemp)

        ElseIf voisinHaut And Not voisinGauche Then
            'moy(maille, voisinHaut)
            tempCoins(0) = Moyenne(tempMaille, voisinHautTemp)

        ElseIf voisinGauche And Not voisinHaut Then
            'moy(maille, voisinGauche)
            tempCoins(0) = Moyenne(tempMaille, voisinGaucheTemp)
        Else
            tempCoins(0) = tempMaille
        End If


        'coin 2 = Haut Droite
        If voisinHaut And voisinDroite Then
            'moy(maille, voisinHaut, voisinDroite)
            tempCoins(1) = Moyenne(tempMaille, voisinHautTemp, voisinDroiteTemp)

        ElseIf voisinHaut And Not voisinDroite Then
            'moy(maille, voisinHaut)
            tempCoins(1) = Moyenne(tempMaille, voisinHautTemp)

        ElseIf voisinDroite And Not voisinHaut Then
            'moy(maille, voisinDroite)
            tempCoins(1) = Moyenne(tempMaille, voisinDroiteTemp)
        Else
            tempCoins(1) = tempMaille
        End If


        'coin 3 = Bas Droite
        If voisinBas And voisinDroite Then
            'moy(maille, voisinBas, voisinDroite)
            tempCoins(2) = Moyenne(tempMaille, voisinBasTemp, voisinDroiteTemp)

        ElseIf voisinBas And Not voisinDroite Then
            'moy(maille, voisinBas)
            tempCoins(2) = Moyenne(tempMaille, voisinBasTemp)

        ElseIf voisinDroite And Not voisinBas Then
            'moy(maille, voisinDroite)
            tempCoins(2) = Moyenne(tempMaille, voisinDroiteTemp)
        Else
            tempCoins(2) = tempMaille
        End If


        'coin 4 = Bas Gauche
        If voisinBas And voisinGauche Then
            'moy(maille, voisinBas, voisinGauche)
            tempCoins(3) = Moyenne(tempMaille, voisinBasTemp, voisinGaucheTemp)

        ElseIf voisinBas And Not voisinGauche Then
            'moy(maille, voisinBas)
            tempCoins(3) = Moyenne(tempMaille, voisinBasTemp)

        ElseIf voisinGauche And Not voisinBas Then
            'moy(maille, voisinGauche)
            tempCoins(3) = Moyenne(tempMaille, voisinGaucheTemp)
        Else
            tempCoins(3) = tempMaille
        End If



        Return tempCoins
    End Function

    Private Function Moyenne(nb1 As Double, nb2 As Double, Optional nb3 As Double = -1) As Double
        If nb3 = -1 Then
            Return (nb1 + nb2) / 2
        Else
            Return (nb1 + nb2 + nb3) / 3
        End If
    End Function

    Private Sub IndiquerTempGaz(ByRef myGr As Graphics, myParaff As Struc_Affichage, myMail As cls_MaillageSlimFloor, ByVal pWi As Single, ByVal pHi As Single)
        'si lAffChThVideOuvert on ajoute un text sous le dessin de la structure pour indiquer la température des gazs chaud (température qui est uniforme)
        'on cherche une maille vide
        Dim temp As Double
        Dim chaine As String = "Aucune maille de vide ouvert trouvée"
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double
        Const LL As Double = 100
        Dim H As Double = LL * pHi / pWi

        dCar = Math.Sqrt((LL) ^ 2 + (H) ^ 2) / 50
        xMin = 0
        yMin = 0
        xMax = LL
        yMax = H + dCar

        For i = 0 To myMail.nb_cells_y - 1
            For j = 0 To myMail.nb_cells_z - 1
                If myMail.Tab_mesh_mat(i, j) = cls_MaillageSlimFloor.MATVIDEOUVERT Then
                    temp = myMail.Tab_mesh_temp(i, j)
                    chaine = "Gaz chaud : " + Math.Round(temp).ToString + "°C"
                    Exit For
                End If
            Next
        Next
        Dim yo As Double = zCarMail + dCar
        AddTexte(myGr, New SolidBrush(Color.Black), chaine, FontFrm, 0, 2 * zCarMail, myParaff, HorizontalAlignment.Center, VerticalAlignement.Bottom)


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


        'couleur par défaut
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

        'si l'option d'afficher le champ thermique est selectionnée & si le calcul thermique est terminé
        If lAffChTh And lCalculThTermine Then
            Dim tempMaille = myMail.Tab_mesh_temp(iMail, jMail)
            'si l'option vide ouvert est selectionnée
            If lAffChThVideOuvert Then
                If myMail.Tab_mesh_mat(iMail, jMail) = cls_MaillageSlimFloor.MATVIDEOUVERT Then

                    myColor = AttributionCouleurChTh(tempMaille)
                    lDessin = True
                End If
            End If

            'si l'option dalle est selectionnée
            If lAffChThDalle Then
                If myMail.Tab_mesh_mat(iMail, jMail) = cls_MaillageSlimFloor.MATBETON Then
                    myColor = AttributionCouleurChTh(tempMaille)
                End If
            End If

            'si l'option Poutre est selectionnée
            If lAffChThPoutre Then

                If (iMail = 51 And jMail = 1) Then
                    Dim i As Integer = 0
                End If

                Dim listeAcier = New List(Of String) From {cls_MaillageSlimFloor.MATACIERSEMI, cls_MaillageSlimFloor.MATACIERSEMS, cls_MaillageSlimFloor.MATACIERAME, cls_MaillageSlimFloor.MATACIERPLAT, cls_MaillageSlimFloor.MATARMA, cls_MaillageSlimFloor.MATACIERSOUD}
                If listeAcier.Contains(myMail.Tab_mesh_mat(iMail, jMail)) Then
                    myColor = AttributionCouleurChTh(tempMaille)
                End If
            End If

        End If
    End Sub

    Private Function AttributionCouleurChTh(tempMaille As Double) As Color
        Dim indBorneInf As Integer    'indice borne inférieure
        Dim indBorneSup As Integer    'indice borne supérieure
        Dim tempInf As Double         'température borne inférieure
        Dim tempSup As Double         'température borne supérieure
        Dim coefCouleur As Double
        Dim coulInf As Color
        Dim coulSup As Color

        For i = 1 To Tab_IntervalleCouleurs.Length - 1
            If tempMaille < Tab_IntervalleCouleurs(i) Then
                indBorneInf = i - 1
                indBorneSup = i
                Exit For
            ElseIf tempMaille = Tab_IntervalleCouleurs(i) Then
                Return Tab_Couleurs(i)
            End If
        Next



        tempInf = Tab_IntervalleCouleurs(indBorneInf)
        tempSup = Tab_IntervalleCouleurs(indBorneSup)
        coulInf = Tab_Couleurs(indBorneInf)
        coulSup = Tab_Couleurs(indBorneSup)

        coefCouleur = (tempMaille - tempInf) / (tempSup - tempInf)

        Dim r, g, b
        If coulInf.R > coulSup.R Then
            r = coulSup.R + (1 - coefCouleur) * (coulInf.R - coulSup.R)
        Else
            r = coulInf.R + coefCouleur * (coulSup.R - coulInf.R)
        End If


        If coulInf.G > coulSup.G Then
            g = coulSup.G + (1 - coefCouleur) * (coulInf.G - coulSup.G)
        Else
            g = coulInf.G + coefCouleur * (coulSup.G - coulInf.G)
        End If


        If coulInf.B > coulSup.B Then
            b = coulSup.B + (1 - coefCouleur) * (coulInf.B - coulSup.B)
        Else
            b = coulInf.B + coefCouleur * (coulSup.B - coulInf.B)
        End If


        Return Color.FromArgb(r, g, b)


    End Function

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

        xReel = XUnivers(myParAff, xSouris)
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


    ' renvoie un bouléen indiquant si on dessine cette maille, ses contours
    ' i.e est ce que le matériaux de la maille est selectionné comme à afficher ou non
    Private Function DoWeDraw(mymail As cls_MaillageSlimFloor, iMail As Integer, jmail As Integer) As Boolean
        Dim matMail As Integer = mymail.Tab_mesh_mat(iMail, jmail)
        Dim elemPoutre() As Integer = {cls_MaillageSlimFloor.MATACIERPLAT, cls_MaillageSlimFloor.MATACIERSEMI, cls_MaillageSlimFloor.MATACIERAME, cls_MaillageSlimFloor.MATACIERSEMS, cls_MaillageSlimFloor.MATACIERSOUD}

        'on affiche la poutre et notre maille est une maille de la poutre
        If lAffPoutre And elemPoutre.Contains(matMail) Then
            Return True
        End If

        'on affiche la dalle et notre maille est une maille de la dalle
        If lAffDalle And matMail = cls_MaillageSlimFloor.MATBETON Then
            Return True
        End If

        'on affiche l'armature et notre maille est une maille de l'armature
        If LaffArmature And matMail = cls_MaillageSlimFloor.MATARMA Then
            Return True
        End If

        'sinon
        Return False

    End Function

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

End Class