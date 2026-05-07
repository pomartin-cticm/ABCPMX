Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.IO
Imports System.Net
Imports System.Net.Mime.MediaTypeNames
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.Logging
Imports PMXMoteur2

Public Class Frm_MaillageSlim

#Region " Attributs "

    Dim lBuild As Boolean

    Dim myParAff As Struc_Affichage

    Dim iSelect As Integer = -1
    Dim jSelect As Integer = -1

    Dim FontFrm As Font

    Public TempMailStepLoc(,,) As Decimal       ' Tableau des températures du maillage à chaque pas de temps
    Public Tab_IntervalleCouleurs(Tab_Couleurs_ChTh.Length - 1) As Decimal
    Dim locMail As cls_MaillageSlimFloor

    Dim zCarMail As Decimal

    Dim lAffChTh As Boolean = True              ' Indique si on affiche le champ thermique, si faux, on affiche contour seuls

    Dim strEchelles() As String
    Dim lEchelleLocale As Boolean               ' Indique si on utilise une échelle de température avec les valeurs min et max de l'étape de calcul affichée
    Dim lEchelleGlobale As Boolean              ' Indique si on utilise une échelle de température avec les valeurs min et max sur l'ensemble des étapes
    Dim lEchelleConstante As Boolean            ' Indique si on utilise une échelle de température constante pour les températures (20, 1200°C)

    Dim lAff2DUniquement As Boolean             ' Indique si on affiche uniquement les mailles calculées en 2D (les mailles centrales)

    Dim tempMin As Double                       ' température minimale sur toutes les mailles
    Dim tempMax As Double                       ' température maximale sur toutes les mailles

    Dim iStep As Integer = 0                    ' Indice de l'étape de calcul à afficher (selection via le combobox)


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_MaillageSlim_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangues()
        GestionStyle()
        AfficheInfoMaillage()
        PrepareFenetre()
        RemplirComboBox()
        PrepareParAff(myParAff, locMail, img_Maillage.Width, img_Maillage.Height, lAff2DUniquement)
        PrepareEchelleTemp()

        lBuild = False

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Dim CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_POSTPROCESSING")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                CLE = "TITLE" : Text = Bloc(CLE)
                CLE = "CLOSE" : btn_OK.Text = Bloc(CLE)

                CLE = "PARAMETERS" : lbl_Parametres.Text = Bloc(CLE)

                CLE = "NOMAILLES" : lbl_NbMailles.Text = Bloc(CLE)
                lbl_SuivantX.Text = "// X"
                lbl_SuivantY.Text = "// Y"

                CLE = "HEATFIELD" : chk_AffChampTherm.Text = Bloc(CLE)
                CLE = "2DELEMT" : chk_Aff2D.Text = Bloc(CLE)

                CLE = "STEP" : lbl_PasDeTemps.Text = Bloc(CLE)
                CLE = "SCALE" : lbl_Echelle.Text = Bloc(CLE)

                strEchelles = {Bloc("LOCSCALE"), Bloc("GLOBSCALE"), Bloc("CONSTSCALE")}

                CLE = "LOCSCALEINFO" : lbl_InfoEchLocale.Text = Bloc(CLE)
                CLE = "GLOBSCALEINFO" : lbl_InfoEchGlobale.Text = Bloc(CLE)
                CLE = "CONSTSCALEINFO" : lbl_InfoEchConstante.Text = Bloc(CLE)



            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub RemplirComboBox()

        cmb_TempR.Items.Clear()

        For i As Integer = 0 To cls_VerifFeuSlimAcier.TimeSteps.GetUpperBound(0)
            cmb_TempR.Items.Add("R" & cls_VerifFeuSlimAcier.TimeSteps(i).ToString)
        Next

        cmb_TempR.SelectedIndex = 0

        cmb_Echelle.Items.Clear()
        cmb_Echelle.Items.Add(strEchelles(0))
        cmb_Echelle.Items.Add(strEchelles(1))
        cmb_Echelle.Items.Add(strEchelles(2))
        cmb_Echelle.SelectedIndex = 0

    End Sub

    Private Sub AfficheInfoMaillage()

        txt_NbMailX.Text = MyProjet.Poutres(MyProjet.IndEnCours).VerifFeuSlimAcier.Maillage_NbY.ToString
        txt_NbMailY.Text = MyProjet.Poutres(MyProjet.IndEnCours).VerifFeuSlimAcier.Maillage_NbZ.ToString


    End Sub

    Private Sub GestionStyle()

        Icon = Frm_PMX.Icon

        img_Maillage.Dock = DockStyle.Fill

        lbl_Parametres.BackColor = CouleurBackBandeaux
        lbl_Parametres.ForeColor = CouleurForeBandeaux

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        PrepareTextBoxDipo(Me.txt_NbMailX, False)
        PrepareTextBoxDipo(Me.txt_NbMailY, False)

    End Sub

    Private Sub PrepareFenetre()

        Dim bEffG, bEffD, bApp As Decimal

        If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
            bEffG = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeD1 / 2
        Else
            bEffG = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeD1
        End If
        bEffD = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeD2 / 2
        bApp = 0.05

        locMail = MyProjet.Poutres(MyProjet.IndEnCours).VerifFeuSlimAcier.GetMaillage
        TempMailStepLoc = MyProjet.Poutres(MyProjet.IndEnCours).VerifFeuSlimAcier.TempMailStep

        chk_AffChampTherm.Checked = lAffChTh

        lEchelleLocale = True

        img_Legende.Invalidate()
        img_Maillage.Invalidate()

    End Sub

#End Region

#Region " Evènements "

    Private Sub Frm_MaillageSlim_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If lBuild Or locMail Is Nothing Then Exit Sub

        PrepareParAff(myParAff, locMail, img_Maillage.Width, img_Maillage.Height, lAff2DUniquement)

        img_Maillage.Invalidate()
        img_Legende.Invalidate()
    End Sub

    Private Sub chk_ChampTherm_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AffChampTherm.CheckedChanged
        lAffChTh = chk_AffChampTherm.Checked
        img_Maillage.Invalidate()
    End Sub

    Private Sub chk_ChThOptions_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Aff2D.CheckedChanged
        If lBuild Then Exit Sub

        lAff2DUniquement = chk_Aff2D.Checked

        ' Il faut actualiser myParAff pour que la gestion de la souris fonctionne
        PrepareParAff(myParAff, locMail, img_Maillage.Width, img_Maillage.Height, lAff2DUniquement)

        img_Maillage.Invalidate()
    End Sub

    Private Sub cmb_TempR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TempR.SelectedIndexChanged
        If lBuild Then Exit Sub

        iStep = cmb_TempR.SelectedIndex
        PrepareEchelleTemp()
        img_Legende.Invalidate()
        img_Maillage.Invalidate()
    End Sub

    Private Sub cmb_Echelle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Echelle.SelectedIndexChanged
        If lBuild Then Exit Sub

        lEchelleLocale = (cmb_Echelle.SelectedIndex = 0)
        lEchelleGlobale = (cmb_Echelle.SelectedIndex = 1)
        lEchelleConstante = (cmb_Echelle.SelectedIndex = 2)

        PrepareEchelleTemp()

        img_Legende.Invalidate()
        img_Maillage.Invalidate()
    End Sub

    Private Sub PrepareEchelleTemp()
        Dim elemPoutre() As Integer = {cls_MaillageSlimFloor.MATACIERPLAT, cls_MaillageSlimFloor.MATACIERSEMI, cls_MaillageSlimFloor.MATACIERAME, cls_MaillageSlimFloor.MATACIERSEMS, cls_MaillageSlimFloor.MATACIERSOUD}
        If lEchelleConstante Then
            tempMin = TEMPERATURE_MIN
            tempMax = TEMPERATURE_MAX
        ElseIf lEchelleLocale Then
            'échelle de température locale = sur le pas de calcul en cours (iStep)
            tempMin = 1200
            tempMax = 0

            With MyProjet.Poutres(MyProjet.IndEnCours).VerifFeuSlimAcier

                For i = 0 To .Maillage_NbY - 1
                    For j = 0 To .Maillage_NbZ - 1

                        checkTempMinMax(i, j)

                    Next
                Next
            End With
        Else
            'échelle de température globale = sur tous les pas de calcul (on sauvegarde iStep avant de le mettre à 0 et d'itérer sur toutes les étapes, puis on lui redonne sa valeur d'origine)
            Dim iStepBack = iStep
            tempMin = 1200
            tempMax = 0
            For cptStep As Integer = 0 To cls_VerifFeuSlimAcier.TimeSteps.GetUpperBound(0)
                With MyProjet.Poutres(MyProjet.IndEnCours).VerifFeuSlimAcier
                    iStep = cptStep
                    For i = 0 To .Maillage_NbY - 1
                        For j = 0 To .Maillage_NbZ - 1

                            checkTempMinMax(i, j)

                        Next
                    Next
                End With
            Next
            iStep = iStepBack
        End If

        Dim nbInterv As Double = Tab_IntervalleCouleurs.Length


        Tab_IntervalleCouleurs(0) = tempMin
        Tab_IntervalleCouleurs(nbInterv - 1) = tempMax

        Dim tempInt As Double
        For i = 1 To nbInterv - 2
            tempInt = tempMin + (i / nbInterv * (tempMax - tempMin))
            Tab_IntervalleCouleurs(i) = tempInt
        Next
    End Sub

    Private Sub checkTempMinMax(iMail As Integer, jMail As Integer)
        If TempMailStepLoc(iStep, iMail, jMail) < tempMin Then
            tempMin = TempMailStepLoc(iStep, iMail, jMail)
        End If
        If TempMailStepLoc(iStep, iMail, jMail) > tempMax Then
            tempMax = TempMailStepLoc(iStep, iMail, jMail)
        End If
    End Sub

#End Region

#Region " Dessin "

    Private Sub img_Legende_Paint(sender As Object, e As PaintEventArgs) Handles img_Legende.Paint
        Dim TempeMaille As Decimal = -1
        'si une maille est selectionnée 
        If Not (IsEqual(iSelect, -1) Or IsEqual(jSelect, -1)) Then
            TempeMaille = TempMailStepLoc(iStep, iSelect, jSelect)
        End If

        DessinLegende(e.Graphics, img_Legende.Width, img_Legende.Height, tempMin, tempMax, TempeMaille, False)

    End Sub

    Private Sub img_Maillage_Paint(sender As Object, e As PaintEventArgs) Handles img_Maillage.Paint
        If lBuild Then Exit Sub

        DessineChampThSlim(e.Graphics, img_Maillage.Width, img_Maillage.Height, MyProjet.Poutres(MyProjet.IndEnCours), iStep,
                           lAff2DUniquement, lAffChTh, True, 'Indique qu'on a besoin de la place pour écrire sous le dessin
                           iSelect, jSelect,
                           tempMin, tempMax)

    End Sub

    Private Sub img_Maillage_MouseMove(sender As Object, e As MouseEventArgs) Handles img_Maillage.MouseMove

        Dim xSouris, ySouris As Single

        xSouris = e.X
        ySouris = e.Y

        WhereIsTheMouse(xSouris, ySouris)

        ' Si on survolle une maille alors on met à jour la légende, le test est réalisé une fois ici et une fois dans le handler du paint de img_Legende
        ' Mais ainsi on évite de invalidate la légende dès qu'on bouge la souris
        If iSelect <> -1 And jSelect <> -1 Then img_Legende.Invalidate()
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
        Dim lVide As Boolean = False
        lVide = (locMail.Tab_mesh_mat(i0, j0) = cls_MaillageSlimFloor.MATVIDEOUVERT) Or (locMail.Tab_mesh_mat(i0, j0) = cls_MaillageSlimFloor.MATVIDEFERME)
        If lTrouve And Not lVide Then
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
        Close()
    End Sub

#End Region

End Class