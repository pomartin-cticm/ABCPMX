Imports System.IO
Imports PMXMoteur2

Public Class Frm_PMX

#Region " Variables locales "

    Dim lBuild As Boolean = True
    Private lOpenAvecFichier As Boolean = False

    Private ReadOnly ToolFiles As New List(Of ToolStripMenuItem)

    Dim FilleEnCours As EnuFenetres = EnuFenetres.Main

    '--> Gestion de l'affichage des projets et des poutres

    Dim tab_ChkSections As List(Of CheckBox)

    Dim CouleurBtnActive As Color = Color.DarkOrange
    Dim CouleurBtnNormal As Color = GrayAM

    Dim Str_WarningFile As String
    'Ajout GUD: Permet de stocker la traduction du terme "File" pour la fenetre d'ouverture du projet (voir la fonction "OuvrirFichier")
    Dim strFiltresExtension As String
    Dim strRacineELU As String
    Dim strRacineELS As String
    Dim strRacineELF As String
    Dim strRacineELUC As String
    Dim strRacineELSC As String
    Dim strCopy As String

    ''' <summary>
    ''' Booleens utilisés pour les controles du dessin
    ''' </summary>
    Dim lZoomPlus, lCotation As Boolean

#End Region

#Region " Variables locales pour btnPoutres "

    Dim tab_BtnPoutres As List(Of POMbutton)

    Structure struc_Colors

        Dim TextBoxFixe As Color
        Dim TextBoxEnSaisie As Color
        Dim TextBoxEnInfo As Color
        Dim SaisieOK As Color
        Dim SaisieError As Color
        Dim Panels As Color

        Dim EtqInfo As Color

        Dim ColorWhiteForGradient As Color
        Dim ColorMouseOnBtn As Color
        Dim ColorSelectedBtn As Color
        Dim ContourNormal As Color
        Dim ContourSelect As Color
        Dim ContourMouse As Color

    End Structure

    Private MyCouleurs As struc_Colors

    Const RACnomBTN As String = "MyX"

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_PMX_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '--> Initialisation générale du logiciel

        InitialiseLogiciel()

        '--> Fenetre Juridique
        If My.Settings.lJuridique Then

            If Frm_Juridique.ShowDialog() = DialogResult.No Then
                Me.Close()
                Exit Sub
            End If

        End If

        '--> Préparation de la fenêtre

        GestionLangueMessageGeneraux()
        GestionLangue()
        GestionStyle()

        lZoomPlus = False
        lCotation = True
        MAJ_btnZoomPlus()
        MAJ_btnCotation()

        AffichageRecentFiles()

        '--> Affichage

        'Si le chemin d'un fichier est passé en argument
        '--> c'est-à-dire que le logiciel est ouvert par le biais d'un fichier sdx+
        If Environment.GetCommandLineArgs().Length = 2 Then
            If (Environment.GetCommandLineArgs(1) <> "") Then
                'Sauvegarde du chemin du fichier
                lOpenAvecFichier = True
                ' FileName = Environment.GetCommandLineArgs(1)
            End If
        End If

        If lOpenAvecFichier Then
            'ReadInFile(FileName)    '--> Ouverture du fichier 
        Else
            lBuild = False
            Frm_Ouverture.ShowDialog()    '--> Fenetre Ouverture

        End If
        'AfficheFenetreEnCours() 'GuD: A discuter j'ai un doute (31/08/2023), cela ouvrait directement
        MAJToolBarPoutre()
        MAJI_BOBasse()

    End Sub

    Private Sub GestionLangueMessageGeneraux()
        '----------------------------------------------------------------------------------------
        '   26/10/23 :  Création - pOM
        '----------------------------------------------------------------------------------------
        '   Récupération des messages généraux dans le fichier langue
        '----------------------------------------------------------------------------------------
        If File.Exists(LogicielFichiers.Langue) Then
            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#GENERAL")
            BlocLine.CreationBloc(Bloc)

            Try

                ReDim NomChargements(3)
                NomChargements(0) = Bloc("SELFWEIGHT")
                NomChargements(1) = Bloc("OTHERG")
                NomChargements(2) = Bloc("QLOADS")
                NomChargements(3) = Bloc("CLOADS")

                ReDim NomChargesA(9)
                NomChargesA(0) = Bloc("PERMANENTLOADS")
                NomChargesA(1) = Bloc("SELFWEIGHT")
                NomChargesA(2) = Bloc("SELFWEIGHTPROPPED")
                NomChargesA(3) = Bloc("SELFWEIGHTNOPROPS")
                NomChargesA(4) = Bloc("OTHERG")
                NomChargesA(5) = Bloc("QLOADS")
                NomChargesA(6) = Bloc("CONFIGURATION")
                NomChargesA(7) = Bloc("SHRINKAGESLAB")
                NomChargesA(8) = Bloc("SHRINKAGEENCASEMENT")
                NomChargesA(9) = Bloc("CLOADS")

                strRacineELU = Bloc("ULS")                              ' "ULS"
                strRacineELS = Bloc("SLS")                              ' "SLS"
                strRacineELF = Bloc("FLS")                              ' "FLS"
                strRacineELUC = Bloc("ULSC")                              ' "ULS_C"
                strRacineELSC = Bloc("SLSC")                              ' "SLS_C"

                strCopy = Bloc("COPY")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_PMX/GestionLangueMessagesGeneraux")
            End Try
        End If
    End Sub

    Private Sub GestionLangue()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_MAIN")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                '--> Menu 'Fichier'
                Me.btn_FilesN.Text = Bloc("FILE")
                strFiltresExtension = Bloc("FILE")
                Me.OpenToolStripMenuItemN.Text = Bloc("OPEN") & "..."
                Me.NewToolStripMenuItemN.Text = Bloc("NEW")
                Me.SaveToolStripMenuItemN.Text = Bloc("SAVE")
                Me.SaveAsToolStripMenuItemN.Text = Bloc("SAVEAS") & "..."
                Me.RecentFileToolStripMenuItemN.Text = Bloc("RECENTFILES")
                Me.QuitToolStripMenuItemN.Text = Bloc("EXIT")

                '--> Menu 'Edit'
                Me.TSbtn_Edit.Text = Bloc("EDIT")
                Me.TSbtn_EditBacs.Text = Bloc("EDITDECKS")
                Me.TSbtn_EditProfiles.Text = Bloc("EDITPROFILES")
                Me.TSbtn_EditStuds.Text = Bloc("EDITSTUDS")

                '--> Menu 'Projet'
                Me.btn_ProjectN.Text = Bloc("PROJECT")
                Me.AddPoutreTSMenuItemN.Text = Bloc("ADDBEAM")
                Me.DeletePoutreTSMenuItemN.Text = Bloc("DELBEAM")
                Me.DuplicatePoutreTSMenuItemN.Text = Bloc("DUPBEAM")
                Me.CalculCoeffToolStripMenuItemN.Text = Bloc("CALCULATION")
                Me.CalculationSheetToolStripMenuItemN.Text = Bloc("CALCULATIONREPORT")

                '--> Menu 'Options'
                Me.btn_OptionsN.Text = Bloc("OPTIONS")
                Me.ConfigToolStripMenuItemN.Text = Bloc("SOFTOPT")
                Me.CalculOptionToolStripMenuItemN.Text = Bloc("CALCULOPTIONS")

                '--> Menu 'Other'
                Me.btn_OtherN.Text = "?"     ' Bloc("OTHER")
                Me.AboutToolStripMenuItemN.Text = Bloc("ABOUT")
                Me.SupportToolStripMenuItemN.Text = Bloc("SUPPORT")
                Me.TechnicalToolStripMenuItemN.Text = Bloc("TECHNICALMANUEL")
                Me.ValidationToolStripMenuItemN.Text = Bloc("VALIDATIONMANUEL")

                '=== CONTENU DE LA FENETRE DE NAVIGATION GAUCHE =========================================================

                Me.Label_Nom_Projet.Text = Bloc("PROJECTNAME")
                Me.Label_Nav_Poutre.Text = Bloc("PROJECTNAVIGATION")

                '=== CONTENU DE LA FENETRE =========================================================

                Me.TSBbtn_NewN.ToolTipText = Bloc("NPROJET") '& "..."
                Me.TSbtn_OpenN.ToolTipText = Bloc("OPEN")
                Me.TSbtn_SaveN.ToolTipText = Bloc("SAVE")
                Me.TSbtn_AddBeamN.ToolTipText = Bloc("ADDBEAM")
                Me.TSbtn_SupprBeam.ToolTipText = Bloc("DELBEAM")
                Me.TSbtn_DupBeam.ToolTipText = Bloc("DUPBEAM")
                Me.TSbtn_Calcul.ToolTipText = Bloc("CALCULATION")
                Me.TSbtn_NoteCalcul.Text = Bloc("CALCULATIONREPORT")

                Me.TSbtn_OptionsCalcul.ToolTipText = Bloc("CALCULOPTIONS")
                Me.TSbtn_OptionsLogiciel.ToolTipText = Bloc("SOFTOPT")

                Me.TSbtn_ZoomPlus.ToolTipText = Bloc("ZOOMIN")
                Me.TSbtn_ZoomMoins.ToolTipText = Bloc("ZOOMOUT")
                Me.TSbtn_Cotations.ToolTipText = Bloc("COTATIONS")
                Me.TSbtn_ExpertMode.ToolTipText = Bloc("EXPERT")

                '=== BARRE d'OUTILS POUR LES POUTRES

                'Me.TSbtn_Identification.ToolTipText = Bloc("TSBHOME")
                Me.TSbtn_Identification.ToolTipText = Bloc("TSBIDENTIFICATION")
                Me.TSbtn_Portees.ToolTipText = Bloc("TSBSPANS")
                'Me.TSbtn_Entraxe.ToolTipText = Bloc("TSBSPACINGS")

                Me.TSbtn_Dalle.ToolTipText = Bloc("TSBSLAB")
                Me.TSbtn_SectionA.ToolTipText = Bloc("TSBSECTIONA")
                Me.TSbtn_Enrobage.ToolTipText = Bloc("TSBENCASEMENT")
                Me.TSbtn_Connexion.ToolTipText = Bloc("TSBCONNECTION")
                Me.TSbtn_Maintiens.ToolTipText = Bloc("TSBRESTRAINTS")
                Me.TSbtn_MaintienBac.ToolTipText = Bloc("TSBSHEETRESTRAIN")

                Me.TSbtn_Etaiement.ToolTipText = Bloc("TSBPROPPING")

                Me.TSbtn_Chargements.ToolTipText = Bloc("TSBLOADS")
                Me.TSbtn_Combinaisons.ToolTipText = Bloc("TSBCOMBINATIONS")
                Me.TSbtn_Gamma.ToolTipText = Bloc("TSBGAMMA")

                Me.TSbtn_OptionsCalculPoutre.ToolTipText = Bloc("TSOPTIONS")
                Me.TSbtn_Hivoss.ToolTipText = Bloc("TSBHIVOSS")
                Me.TSbtn_OptionsIncendie.ToolTipText = Bloc("TSBOPTIONSINCENDIE")
                Me.TSbtn_NdcPoutre.ToolTipText = Bloc("TSBNDCPOUTRE")

                Me.TSbtn_PPCombi.Text = Bloc("TSPPCOMBINATIONS")
                Me.TSbtn_PPLargeurEfficace.Text = Bloc("TSPPEFFWIDTHS")
                Me.TSbtn_PPLoadCases.Text = Bloc("TSPPLOADCASES")
                Me.TSbtn_PPVerifications.Text = Bloc("TSPPVERIFICATIONS")
                Me.TSbtn_PostT.Text = Bloc("TSPPGENERAL")

                Me.TSmenuPPLargeurEfficace.Text = Bloc("PPEFFWIDTHS")
                Me.TSmenuPPChargements.Text = Bloc("PPLOADCASES")
                Me.TSmenuPPCombinaisons.Text = Bloc("PPCOMBINATIONS")
                Me.TSmenuPPVerifications.Text = Bloc("PPVERIFICATIONS")
                Me.TSmenuPPModePropre.Text = Bloc("PPEIGENMODE")

                Me.TSbtn_PostT.Text = Bloc("PPOSTT")

                '=== MESSAGES GENERAUX

                ErreurCapacite_LNG = Bloc("ERRORCAPACITY")
                ErreurNonNul_LNG = Bloc("ERROREMPTYCELL")
                ErreurNonNum_LNG = Bloc("ERRORNONNUMERIC")
                ErreurHorsBornes_LNG = Bloc("ERROROUTBOUNDS")

                Str_WarningFile = "Probleme lecture fichier"

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_PMX/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Text = LogicielInfo.NomLogiciel

        Select Case LogicielInfo.Maitre
            Case EnuMaitre.CTICM
                Me.img_Logo.Image = Me.ContainerLogo.Images("Logo_CTICM")

        End Select

        Me.TLPan_Main.RowStyles(3).Height = 0

        Me.TSbtn_PPLargeurEfficace.Visible = False
        Me.TSbtn_PPCombi.Visible = False
        Me.TSbtn_PPLoadCases.Visible = False
        Me.TSbtn_PPVerifications.Visible = False
        'Me.TSbtn_PPCombi.Visible = False
    End Sub

#End Region

#Region " Provisoire "

    Private Sub PaintPanel(sender As Object, e As PaintEventArgs)
        DessineBordurePanel(e.Graphics, sender.ClientRectangle.Width, sender.ClientRectangle.Height)
    End Sub

    Private Sub DessineBordurePanel(MyGr As Graphics, sWi As Single, sHi As Single)

        Dim xo, yo As Integer
        Dim xe, ye As Integer

        Dim PenGris As New Pen(Color.Gray)
        Dim PenBlanc As New Pen(Color.White)

        xo = sWi - 2
        xe = xo
        yo = 1
        ye = sHi - 1

        MyGr.DrawLine(PenGris, xo, yo, xe, ye)

        xo = sWi - 1
        xe = xo

        MyGr.DrawLine(PenBlanc, xo, yo, xe, ye)


    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        'Me.Split_Main.Panel1Collapsed = Not Me.CheckBox3.Checked
    End Sub


#End Region

#Region " Gestion Evènements sur Projets "
    Private Sub cmb_Projet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Projet.SelectedIndexChanged

    End Sub

    Private Sub AfficheProjetEnCours()



    End Sub

    Private Sub AffichePoutreEnCours()

    End Sub

    Private Sub ClickNouveauProjet(sender As Object, e As EventArgs) Handles TSBbtn_NewN.Click

        Frm_AjoutePP.ShowDialog()

    End Sub

    Private Sub AjouterPoutre()

        '--> Ouverture de la fenêtre
        Dim dg As DialogResult = Frm_AjoutePP.ShowDialog()

        '--> Si ajout d'une nouvelle section
        If dg = DialogResult.OK Then

            '--> Affichage de la section créee
            'AffichageFenetreFille()

            '--> Mise à jour du TreeView
            AffichageTViewChk()
            MAJToolBarPoutre()
            Me.img_Main.Invalidate()

        End If
    End Sub

    Private Sub SupprimerPoutre()

        MyProjet.Poutres.Remove(MyProjet.Poutres(MyProjet.IndEnCours))

        MyProjet.IndEnCours = Math.Max(Math.Min(MyProjet.IndEnCours, MyProjet.Poutres.Count - 1), 0)

        '--> Mise à jour du TreeView
        AffichageTViewChk()
        MAJToolBarPoutre()
        Me.img_Main.Invalidate()

    End Sub

    Private Sub DupliquerPoutre()
        Dim NouvellePoutre As New cls_Poutre
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), NouvellePoutre)

        If MyProjet.Poutres(MyProjet.IndEnCours).BeamID.Contains(strCopy) Then
            Dim index As Integer = MyProjet.Poutres(MyProjet.IndEnCours).BeamID.LastIndexOf(strCopy)
            index += strCopy.Length
            Dim strRight = MyProjet.Poutres(MyProjet.IndEnCours).BeamID.Substring(index)
            Dim strLeft = MyProjet.Poutres(MyProjet.IndEnCours).BeamID.Substring(0, index)
            Dim intAppend As Integer

            If Integer.TryParse(strRight, intAppend) Then
                intAppend += 1
                NouvellePoutre.BeamID = strLeft + CStr(intAppend)
            Else
                NouvellePoutre.BeamID += "1"
            End If
        Else
            NouvellePoutre.BeamID += strCopy
        End If
        MyProjet.Poutres.Add(NouvellePoutre)

        MyProjet.IndEnCours = Math.Max(0, MyProjet.Poutres.Count - 1)

        '--> Mise à jour du TreeView
        AffichageTViewChk()
        MAJToolBarPoutre()
        Me.img_Main.Invalidate()
    End Sub


#End Region

#Region " Gestion des barres d'outils "

    Private Sub Btn_AddSection_Click(sender As Object, e As EventArgs) Handles TSbtn_AddBeamN.Click
        AjouterPoutre()
    End Sub

    Private Sub TSbtn_SupprBeam_Click(sender As Object, e As EventArgs) Handles TSbtn_SupprBeam.Click
        SupprimerPoutre()
    End Sub

    Private Sub TSbtn_DupBeam_Click(sender As Object, e As EventArgs) Handles TSbtn_DupBeam.Click
        DupliquerPoutre()
    End Sub

    Private Sub TSbtn_NoteCalcul_Click(sender As Object, e As EventArgs) Handles TSbtn_NoteCalcul.Click, TSbtn_NdcPoutre.Click
        CalculsEtNdC()
    End Sub

    Private Sub TSbtn_Calcul_Click(sender As Object, e As EventArgs) Handles TSbtn_Calcul.Click
        Frm_ModularRatio.ShowDialog()
        Frm_ModularRatio.Dispose()
    End Sub


#End Region

#Region " Note de calculs "

    Private Sub CalculsEtNdC()
        '--------------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------
        '   La poutre en cours est elle conforme pour le calcul
        '   Si oui, execution du calcul et édition de la note de calcul
        '--------------------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------------------

        '--[ Déclarations

        Dim lOK As Boolean

        '--[ Test de la poutre

        lOK = MaPoutreOKpourleCalcul(MyProjet.Poutres(MyProjet.IndEnCours))

        '--[ Analyse calcul RDM 

        If lOK Then
            MyProjet.Poutres(MyProjet.IndEnCours).AAA_Verifications(NomChargesA, strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC)
        End If

        '--[ Edition de la note de calcul

        AAA_EditionNOTEdeCALCUL(True)

    End Sub

    Private Function MaPoutreOKpourleCalcul(myPoutre As cls_Poutre) As Boolean
        '--------------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------
        '   Indique si le calcul de la poutre peut être effectué
        '--------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lOK As Boolean = True

        '--> Traitement

        Return lOK

    End Function

#End Region

#Region " Gestion Barre d'outils poutre "

    Private Sub GestionBoutonsBarreOutilGenerale(sender As Object, e As EventArgs) Handles TSbtn_EditStuds.Click, TSbtn_EditProfiles.Click, TSbtn_EditBacs.Click, AboutToolStripMenuItemN.Click, SupportToolStripMenuItemN.Click

        Dim lSupport As Boolean = False

        Select Case sender.name
            Case Me.TSbtn_EditStuds.Name
                FilleEnCours = EnuFenetres.EditGoujons
            Case Me.TSbtn_EditProfiles.Name
                FilleEnCours = EnuFenetres.EditSection
            Case Me.TSbtn_EditBacs.Name
                FilleEnCours = EnuFenetres.EditBac
            Case Me.AboutToolStripMenuItemN.Name
                FilleEnCours = EnuFenetres.About
            Case Me.SupportToolStripMenuItemN.Name
                lSupport = True
                PrepareMailSupport()
        End Select

        If Not lSupport Then AfficheFenetreEnCours()

    End Sub

    Private Sub TSmenuPP_Click(sender As Object, e As EventArgs) Handles TSmenuPPLargeurEfficace.Click,
        TSmenuPPChargements.Click, TSmenuPPCombinaisons.Click, TSmenuPPVerifications.Click, TSmenuPPModePropre.Click, TSMenuPP_Hivoss.Click

        Select Case sender.name
            Case Me.TSmenuPPChargements.Name
                FilleEnCours = EnuFenetres.PPCasDeCharge

            Case Me.TSmenuPPCombinaisons.Name
                FilleEnCours = EnuFenetres.PPCombinaison

            Case Me.TSmenuPPLargeurEfficace.Name
                FilleEnCours = EnuFenetres.PPLargeurEfficace

            Case Me.TSmenuPPModePropre.Name
                FilleEnCours = EnuFenetres.PPModePropre

            Case Me.TSmenuPPVerifications.Name
                FilleEnCours = EnuFenetres.PPVerifications

            Case Me.TSMenuPP_Hivoss.Name
                FilleEnCours = EnuFenetres.PPHivoss

        End Select
        AfficheFenetreEnCours()
    End Sub


    Private Sub GestionBoutonsMenuPoutre(sender As Object, e As EventArgs) _
        Handles TSbtn_Portees.Click, TSbtn_Identification.Click, TSbtn_Maintiens.Click, TSbtn_Etaiement.Click, TSbtn_SectionA.Click,
                TSbtn_Enrobage.Click, TSbtn_Dalle.Click, TSbtn_Connexion.Click, TSbtn_Hivoss.Click, TSbtn_DalleN.Click,
                TSbtn_Gamma.Click, TSbtn_PPLargeurEfficace.Click, TSbtn_Combinaisons.Click, TSbtn_PPLoadCases.Click, TSbtn_Chargements.Click,
                TSbtn_PPCombi.Click, TSbtn_PPVerifications.Click, TSbtn_OptionsCalculPoutre.Click, TSbtn_OptionsIncendie.Click, TSbtn_MaintienBac.Click

        Select Case sender.name

            Case Me.TSbtn_Identification.Name
                FilleEnCours = EnuFenetres.Identification
            Case Me.TSbtn_Portees.Name
                FilleEnCours = EnuFenetres.Portees
                ' Case Me.TSbtn_Entraxe.Name
           '     FilleEnCours = EnuFilleEnCours.Entraxes

            Case Me.TSbtn_Dalle.Name
                FilleEnCours = EnuFenetres.Dalle
            Case Me.TSbtn_DalleN.Name
                FilleEnCours = EnuFenetres.DalleN
            Case Me.TSbtn_SectionA.Name
                FilleEnCours = EnuFenetres.Section
            Case Me.TSbtn_Enrobage.Name
                FilleEnCours = EnuFenetres.Enrobage

            Case Me.TSbtn_Connexion.Name
                FilleEnCours = EnuFenetres.Connexion


            Case Me.TSbtn_Maintiens.Name
                FilleEnCours = EnuFenetres.Maintiens
            Case Me.TSbtn_Etaiement.Name
                FilleEnCours = EnuFenetres.Etaiement

                '--> Chargements, combinaisons, Coefficients

            Case Me.TSbtn_Chargements.Name
                FilleEnCours = EnuFenetres.Chargements
            Case Me.TSbtn_Gamma.Name
                FilleEnCours = EnuFenetres.Gamma
            Case Me.TSbtn_Combinaisons.Name
                FilleEnCours = EnuFenetres.Combinaisons

                '--> Options

            Case Me.TSbtn_OptionsCalculPoutre.Name
                FilleEnCours = EnuFenetres.OptionsCalculPoutre

            Case Me.TSbtn_Hivoss.Name
                FilleEnCours = EnuFenetres.Hivoss

            Case Me.TSbtn_PPLargeurEfficace.Name
                FilleEnCours = EnuFenetres.PPLargeurEfficace

            Case Me.TSbtn_PPLoadCases.Name
                FilleEnCours = EnuFenetres.PPCasDeCharge
            Case Me.TSbtn_PPCombi.Name
                FilleEnCours = EnuFenetres.PPCombinaison
            Case Me.TSbtn_PPVerifications.Name
                FilleEnCours = EnuFenetres.PPVerifications

            Case Me.TSbtn_OptionsIncendie.Name
                FilleEnCours = EnuFenetres.OptionsIncendie

            Case Me.TSbtn_MaintienBac.Name
                FilleEnCours = EnuFenetres.Test

        End Select
        AfficheFenetreEnCours()

    End Sub

    Private Sub AfficheFenetreEnCours()

        Select Case FilleEnCours
            Case EnuFenetres.Identification
                Frm_Identification.ShowDialog()
                AffichageTViewChk() 'Ajout GuD: MAJ du volet gauche. A voir si c'est pertinent

            Case EnuFenetres.Portees
                If LogicielOptions.lFenetres Then
                    Frm_Portees.ShowDialog()
                Else
                    Frm_Portees.InitialiserFenetre()
                    Me.TLPan_ZoneDeSaisie.Controls.Add(Frm_Portees.pan_Main, 0, 1)
                End If

            Case EnuFenetres.Dalle
                If LogicielOptions.lFenetres Then
                    Frm_Dalle.ShowDialog()

                End If
            Case EnuFenetres.DalleN


            Case EnuFenetres.Section
                If LogicielOptions.lFenetres Then
                    Frm_SectionAcierStandard.ShowDialog()
                Else

                End If

            Case EnuFenetres.Enrobage
                If LogicielOptions.lFenetres Then
                    Frm_Enrobage.ShowDialog()
                Else

                End If

            Case EnuFenetres.Connexion
                If LogicielOptions.lFenetres Then
                    Frm_Connection.ShowDialog()
                Else

                End If

            Case EnuFenetres.Maintiens
                If LogicielOptions.lFenetres Then
                    Frm_Maintiens.ShowDialog()
                Else

                End If

            Case EnuFenetres.Etaiement
                If LogicielOptions.lFenetres Then
                    Frm_Etaiement.ShowDialog()
                Else

                End If

            Case EnuFenetres.Chargements
                If LogicielOptions.lFenetres Then
                    Frm_Chargement.ShowDialog()
                Else

                End If

            Case EnuFenetres.Gamma
                If LogicielOptions.lFenetres Then
                    Frm_Gamma.ShowDialog()
                End If

            Case EnuFenetres.Combinaisons
                If LogicielOptions.lFenetres Then
                    Frm_Combinaisons.ShowDialog()
                End If

            Case EnuFenetres.Hivoss
                If LogicielOptions.lFenetres Then
                    iFrmAppel = EnuFenetres.Main
                    Frm_Hivoss.ShowDialog()
                Else

                End If

            Case EnuFenetres.OptionsCalculPoutre
                Frm_OptionsCalculPoutre.ShowDialog()
                Frm_OptionsCalculPoutre.Dispose()

            Case EnuFenetres.PPLargeurEfficace
                Frm_PPLargeurEfficace.ShowDialog()
                Frm_PPLargeurEfficace.Dispose()

            Case EnuFenetres.PPCasDeCharge
                Frm_PPCasDeCharge.ShowDialog()
                Frm_PPCasDeCharge.Dispose()

            Case EnuFenetres.PPCombinaison
                Frm_PPCombinaison.ShowDialog()
                Frm_PPCombinaison.Dispose()

            Case EnuFenetres.PPHivoss
                Frm_PPHivoss.ShowDialog()
                Frm_PPHivoss.Dispose()

            Case EnuFenetres.PPModePropre
                Frm_PPModePropre.ShowDialog()
                Frm_PPModePropre.Dispose()

            Case EnuFenetres.PPVerifications
                Frm_PPVerifications.ShowDialog()
                Frm_PPVerifications.Dispose()

            Case EnuFenetres.EditGoujons
                If LogicielOptions.lFenetres Then
                    Frm_EditGoujons.ShowDialog()
                End If

            Case EnuFenetres.EditBac
                If LogicielOptions.lFenetres Then
                    Frm_EditBaseBacsAcier.ShowDialog()
                End If

            Case EnuFenetres.EditSection
                If LogicielOptions.lFenetres Then
                    Frm_Catalogue.ShowDialog()
                End If

            Case EnuFenetres.About
                If LogicielOptions.lFenetres Then
                    Frm_About.ShowDialog()
                    Frm_About.Dispose()
                End If

            Case EnuFenetres.Test
                Frm_MaintienBac.ShowDialog()

        End Select

        MAJMainToolBar()
        GestionModificationPoutreEnCours()
        img_Main.Invalidate()

    End Sub

    Private Sub MAJMainToolBar()

        If MyProjet.Poutres.Count = 0 Then Exit Sub

        If MyProjet.Poutres(MyProjet.IndEnCours).lDonneesSauvees Then
            Me.TSbtn_SaveN.Image = ImgList_Menu.Images("Enregistrer_OK")
        Else
            If MyProjet.Poutres(MyProjet.IndEnCours).NouvellePoutre Then
                Me.TSbtn_SaveN.Image = ImgList_Menu.Images("EnregistrerVierge")
            Else

                Me.TSbtn_SaveN.Image = ImgList_Menu.Images("Enregistrer_NotOK")
            End If
        End If


    End Sub

    ''' <summary>
    ''' Méthode qui permet de gérer les conséquences sur la poutre lorsqu'on sort d'une fenetre fille et qu'elle a été modifiée 
    ''' </summary>
    Private Sub GestionModificationPoutreEnCours()
        'il faut gérer les mises à jour de la poutre après modification dans la routine cls_Poutre.estmodifiee

        'With MyProjet.Poutres(MyProjet.IndEnCours)
        '    If .lPoutreModifiee Then

        '        'Select Case FilleEnCours
        '        '    Case EnuFenetres.Portees
        '        '        .InitialisePoidsPropres()
        '        '    Case EnuFenetres.Dalle
        '        '        .InitialisePoidsPropres()
        '        '    Case EnuFenetres.Section
        '        '        .InitialisePoidsPropres()
        '        '    Case EnuFenetres.Enrobage
        '        '        .InitialisePoidsPropres()
        '        '    Case EnuFenetres.Connexion

        '        '    Case EnuFenetres.Maintiens

        '        '    Case EnuFenetres.Etaiement

        '        '    Case EnuFenetres.Chargements

        '        '    Case EnuFenetres.Gamma

        '        '    Case EnuFenetres.Combinaisons

        '        '    Case EnuFenetres.Hivoss

        '        'End Select
        '    End If

        'End With
    End Sub

    ''' <summary>
    ''' Ajout GUD: Permet de gérer l'affichage des Frm_Filles en fonction du type de la poutre affichée 
    ''' </summary>
    Private Sub MAJToolBarPoutre()

        Dim lFrmEnrobage As Boolean = True
        Dim lFrmConnection As Boolean = True
        Dim lFrmProppin As Boolean = True
        Dim lFrmMaintienBac As Boolean = True
        Dim lNothing As Boolean = MyProjet.Poutres.Count = 0

        If Not lNothing Then
            Select Case MyProjet.Poutres(MyProjet.IndEnCours).TypeSection
                Case cls_Section.Enum_TypeSection.AcierSeul
                    lFrmConnection = False
                    lFrmEnrobage = False
                    lFrmProppin = False
                    lFrmMaintienBac = False
                Case cls_Section.Enum_TypeSection.AcierSeulEnrobage
                    lFrmConnection = False
                    lFrmProppin = False
                    lFrmMaintienBac = False
                Case cls_Section.Enum_TypeSection.Mixte
                    lFrmEnrobage = False
                Case cls_Section.Enum_TypeSection.MixteEnrobage
                Case cls_Section.Enum_TypeSection.SFB
                    lFrmEnrobage = False
                    lFrmProppin = False
                    lFrmMaintienBac = False
                Case cls_Section.Enum_TypeSection.SFBmixte
                Case cls_Section.Enum_TypeSection.IFB_A
                    lFrmEnrobage = False
                    lFrmProppin = False
                    lFrmMaintienBac = False
                Case cls_Section.Enum_TypeSection.IFB_Amixte
                Case cls_Section.Enum_TypeSection.IFB_B
                    lFrmEnrobage = False
                    lFrmProppin = False
                    lFrmMaintienBac = False
                Case cls_Section.Enum_TypeSection.IFB_Bmixte
                Case cls_Section.Enum_TypeSection.SAB
                    lFrmEnrobage = False
                    lFrmProppin = False
                    lFrmMaintienBac = False
                Case cls_Section.Enum_TypeSection.SABmixte
            End Select
        End If

        Me.TSbtn_Enrobage.Visible = lFrmEnrobage
        Me.TSbtn_Connexion.Visible = lFrmConnection
        Me.TSbtn_Etaiement.Visible = lFrmProppin
        Me.TSbtn_MaintienBac.Visible = lFrmMaintienBac
        Me.ToolStrip_Poutre.Visible = Not lNothing

        Me.TSbtn_SaveN.Visible = Not lNothing
        Me.TSbtn_SupprBeam.Visible = Not lNothing
        Me.TSbtn_DupBeam.Visible = Not lNothing
        Me.ToolStripSeparator24.Visible = Not lNothing
        Me.TSbtn_Calcul.Visible = Not lNothing
        Me.ToolStripSeparator25.Visible = Not lNothing
        Me.TSbtn_NoteCalcul.Visible = Not lNothing

        Me.SaveToolStripMenuItemN.Visible = Not lNothing
        Me.SaveAsToolStripMenuItemN.Visible = Not lNothing
        Me.ToolStripSeparator14.Visible = Not lNothing

        Me.DeletePoutreTSMenuItemN.Visible = Not lNothing
        Me.DuplicatePoutreTSMenuItemN.Visible = Not lNothing
        Me.ToolStripSeparator18.Visible = Not lNothing
        Me.CalculCoeffToolStripMenuItemN.Visible = Not lNothing
        Me.ToolStripSeparator19.Visible = Not lNothing
        Me.CalculationSheetToolStripMenuItemN.Visible = Not lNothing

        Me.TSGestionImagePoutre.Visible = Not lNothing
    End Sub

    Private Sub MAJVoletGauche()

    End Sub


#End Region

#Region " Barre d'outils principale "

    Private Sub TSbtn_OptionsCalcul_Click(sender As Object, e As EventArgs) Handles TSbtn_OptionsCalcul.Click, CalculOptionToolStripMenuItemN.Click

        Frm_OptionsCalcul.ShowDialog()

    End Sub

    Private Sub MAJI_BOBasse()

        Me.TSbtn_ExpertMode.Visible = LogicielOptions.lExpert
        Me.TSbtn_ExpertMode.Checked = LogicielOptions.lExpert

    End Sub


    Private Sub TSbtn_OptionsLogiciel_Click(sender As Object, e As EventArgs) Handles TSbtn_OptionsLogiciel.Click, ConfigToolStripMenuItemN.Click

        Frm_OptionsLogiciel.ShowDialog()

        If ComWindow = enu_ComWindow.OK Then
            EnregistrerOptionsLogiciel()
            MAJI_BOBasse()
        End If

    End Sub


    Private Sub TSbtn_OpenN_Click(sender As Object, e As EventArgs) Handles TSbtn_OpenN.Click

        OuvrirFichier()

    End Sub


    Private Sub TSbtn_SaveN_Click(sender As Object, e As EventArgs) Handles TSbtn_SaveN.Click

        EnregistrerProjetEnCours()

    End Sub

#End Region

#Region " Fonctions de sauvegarde et lecture "

    Private Sub EnregistrerOptionsLogiciel(Optional lRecentFile As Boolean = False)

        '--( Mode expert 

        My.Settings.lExpertMode = LogicielOptions.lExpert

        '--( Général

        '# Langues

        My.Settings.IndLangue = LogicielOptions.IndLangue
        My.Settings.IndLangueNDC = LogicielOptions.IndLangueNDC

        '# Identification
        My.Settings.UserName = LogicielOptions.UserName
        My.Settings.CompanyName = LogicielOptions.CompanyName

        '--( Unités
        '# Dimensions et Longueurs
        My.Settings.indUnitDimension = LogicielOptions.IndUnitDimension
        My.Settings.indUnitLongueur = LogicielOptions.IndUnitLongueur

        '# Contraintes et module d'élasticité
        My.Settings.indUnitContraintes = LogicielOptions.IndUnitContraintes
        My.Settings.indUnitModuleY = LogicielOptions.IndUnitModulesY

        '# Efforts et moments
        My.Settings.indUnitEffort = LogicielOptions.IndUnitEffort
        My.Settings.indUnitMoment = LogicielOptions.IndUnitMoment

        '--( Note de calcul
        My.Settings.lNdCCourbeHivoss = OptionsNdC.lShowHivossCurve
        My.Settings.lNdCDispFM_ELF = OptionsNdC.lDispFM_FLS
        My.Settings.lNdCDispFM_ELS = OptionsNdC.lDispFM_SLS
        My.Settings.lNdCDispFM_ELU = OptionsNdC.lDispFM_ULS
        My.Settings.lNdCDispLoadCase = OptionsNdC.lDispFMLoadCase
        My.Settings.lNdCShowDiagram = OptionsNdC.lDispFMDiagrams
        My.Settings.lNdCDispSigmaCharges = OptionsNdC.lDispSigmaCharges
        My.Settings.lNdCDispMelMixte = OptionsNdC.lDispMelPoutreMixte

        '--( Fichiers récents
        If lRecentFile Then
            '--> Fichiers récemment ouverts
            My.Settings.RecentFiles = New Specialized.StringCollection

            For i = 0 To LogicielFichiers.RecentFiles.Count - 1
                My.Settings.RecentFiles.Add(LogicielFichiers.RecentFiles(i))
                If i = 9 Then Exit For '--> on se limite au 10 derniers fichiers
            Next
        End If

        My.Settings.Save()
    End Sub

    Private Sub EnregistrerProjetEnCours()
        '-----------------------------------------------------------------------------------
        '   11/04/08 :  Création - Version 1.00
        '-----------------------------------------------------------------------------------
        '   Enregistrement du projet en cours
        '-----------------------------------------------------------------------------------
        '  
        '-----------------------------------------------------------------------------------

        If My.Computer.FileSystem.FileExists(MyProjet.FileName) Then

            EcrireProjetInFile(MyProjet.FileName)
            MyProjet.Poutres(MyProjet.IndEnCours).lDonneesSauvees = True
            MAJMainToolBar()

            'MemoriserNouveauFichier(MyProjet.FileName)

            LogicielRep.Travail = Repertoire(MyProjet.FileName)

        Else

            EnregistrerSousProjetEnCours()

        End If

    End Sub

    Private Sub EnregistrerSousProjetEnCours()
        '-----------------------------------------------------------------------------------
        '   11/04/08 :  Création - Version 1.00
        '-----------------------------------------------------------------------------------
        '   Enregistrement du projet en cours
        '-----------------------------------------------------------------------------------
        '  
        '-----------------------------------------------------------------------------------

        '# Gestion projet en cours (sauvegarde)

        '# Récupération du nom de fichier

        Me.SaveFileDialog_Project.Title = Me.SaveToolStripMenuItemN.Text
        Me.SaveFileDialog_Project.FileName = MyProjet.Nom
        Me.SaveFileDialog_Project.DefaultExt = LogicielInfo.Extension

        Me.SaveFileDialog_Project.InitialDirectory = LogicielOptions.RepertoireTravail

        If Me.SaveFileDialog_Project.ShowDialog() = Windows.Forms.DialogResult.OK Then

            '# MAJ paramètres
            '.Save = True
            MyProjet.FileName = Me.SaveFileDialog_Project.FileName

            '# AJout dans FichierRecents
            '   si déjà dans la liste, on le supprime pour le rajouter à la première position

            'If LogicielFichiers.RecentFiles.Contains(MyProjet.FileName) Then LogicielFichiers.RecentFiles.Remove(MyProjet.FileName)
            'LogicielFichiers.RecentFiles.Insert(0, MyProjet.FileName)
            EnregistreDansFichiersRecents(MyProjet.FileName)

            '# Enregistrer

            EcrireProjetInFile(MyProjet.FileName)

            '--> MAJ Rep de travail 
            If Not LogicielOptions.lRepTravailDefault Then
                LogicielOptions.RepertoireTravail = RecupRepertoire(MyProjet.FileName)
            End If

            '--> MAJ fichier recent
            Me.AffichageRecentFiles()

            MyProjet.Poutres(MyProjet.IndEnCours).lDonneesSauvees = True
            MAJMainToolBar()

        End If


    End Sub

    Private Sub EnregistreDansFichiersRecents(FileName As String)
        '-----------------------------------------------------------------------------------
        '   13/03/24 :  Création - Version 1.00
        '-----------------------------------------------------------------------------------
        '   Ajoute un fichier ouverts ou enrgistrés dans la liste des fichiers récents
        '-----------------------------------------------------------------------------------
        '   FileNmae    [E] :   Nom du fichier à enregistrer comme fichier récent
        '-----------------------------------------------------------------------------------

        If LogicielFichiers.RecentFiles.Contains(FileName) Then LogicielFichiers.RecentFiles.Remove(FileName)
        LogicielFichiers.RecentFiles.Insert(0, FileName)

    End Sub

    Private Sub OuvrirFichier()
        '-----------------------------------------------------------------------------------
        '   09/08/23 :  Création - Version 1.00
        '-----------------------------------------------------------------------------------
        '   Gestion de la demande d'ouverture d'un fichier
        '-----------------------------------------------------------------------------------

        '--> Déclaration
        Dim FileName As String

        '# Contrôle sauvegarde du projet en cours

        '# Demande nom fichier

        '--> Préparation de la boite de dialogue OpenFile

        Me.OpenFileDialog_Project.InitialDirectory = LogicielOptions.RepertoireTravail
        'Me.OpenFileDialog_Project.DefaultExt = LogicielInfo.Extension
        Me.OpenFileDialog_Project.Filter = strFiltresExtension & " (*." & LogicielInfo.Extension & ")|*." & LogicielInfo.Extension
        Me.OpenFileDialog_Project.FileName = ""
        Me.OpenFileDialog_Project.ShowDialog()

        FileName = Me.OpenFileDialog_Project.FileName

        '# Ouverture

        '--> Gestion du résultat de la boite de dialogue
        If FileName <> "" Then

            OuvrirFichier(FileName)

        End If

    End Sub

    Private Sub OuvrirFichier(FileName As String)
        '----------------------------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------
        '   Ouverture d'un fichier dont on connait le nom
        '----------------------------------------------------------------------------------------------------
        '   FileName    [E] :   
        '----------------------------------------------------------------------------------------------------

        '--> Lecture du fichier
        ReadInFile(FileName)
        MAJToolBarPoutre()
        Me.img_Main.Invalidate()

        '--> Gestion Recent Files

        EnregistreDansFichiersRecents(FileName)

        '--> MAJ fichier recent
        Me.AffichageRecentFiles()
    End Sub

    Public Sub ReadInFile(ByVal FileName As String)
        '-----------------------------------------------------------------------------------
        '   09/08/23 :  Création - Version 1.00
        '-----------------------------------------------------------------------------------
        '   Enregistrement du projet en cours
        '-----------------------------------------------------------------------------------

        '--> Curseur de chargement
        Cursor.Current = Cursors.WaitCursor

        '--> AJout dans FichierRecents
        'si déjà dans la liste, on le supprime pour le rajouter à la première position
        If LogicielFichiers.RecentFiles.Contains(FileName) Then LogicielFichiers.RecentFiles.Remove(FileName)
        LogicielFichiers.RecentFiles.Insert(0, FileName)

        '--> MAJ Rep de travail 
        If Not LogicielOptions.lRepTravailDefault Then
            LogicielOptions.RepertoireTravail = RecupRepertoire(FileName)
        End If

        '--> Lecture du fichier
        MyProjet = New cls_Projet
        MyProjet.RecuperationFile(FileName, Str_WarningFile)
        MyProjet.IndEnCours = 0

        Dim lTrouve As Boolean

        For Each MyPoutre As cls_Poutre In MyProjet.Poutres
            AssocieAcierCompatible(MyPoutre, LogicielFichiers.Base_Aciers, LogicielFichiers.Base_Sections, lTrouve, True)
        Next

        '--> Aucune modification par rapport au fichier ouvert
        MyProjet.lModif = False

        '--> Initialisation de l'interface avec le projet ouvert
        'AfficheFenetreEnCours()
        AffichageTViewChk()


        'Frm_MAIN.ModifImageSave()

        '--> Curseur par défaut
        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region " Fichiers Recents "

    ''' <summary>
    ''' Affichage des 10 derniers fichiers utilisés
    ''' </summary>
    Public Sub AffichageRecentFiles()

        '--> Initialisation
        Me.RecentFileToolStripMenuItemN.DropDownItems.Clear()
        Me.ToolFiles.Clear()

        '--> Traitement
        For i = 0 To LogicielFichiers.RecentFiles.Count - 1

            ToolFiles.Add(New ToolStripMenuItem(LogicielFichiers.RecentFiles(i)))
            Me.RecentFileToolStripMenuItemN.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {ToolFiles(i)})
            AddHandler ToolFiles(i).Click, AddressOf Me.Click_RecentFile
            If i = 9 Then Exit For

        Next

    End Sub

    ''' <summary>
    ''' Click sur un fichier récent
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Click_RecentFile(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim FileName As String = sender.text

        '--> Fichier n'existe plus
        If Not File.Exists(FileName) Then
            MsgBox("Le fichier n'existe pas | File doesn't exist : " & FileName, MsgBoxStyle.Critical)

            '--> Suppression de la liste
            If LogicielFichiers.RecentFiles.Contains(FileName) Then LogicielFichiers.RecentFiles.Remove(FileName)
            AffichageRecentFiles()

            Exit Sub
        End If

        If MyProjet.lModif Then '--> Projet  modifié

            '    '--> Affichage Fenetre Avertissement
            '    Dim dg As DialogResult = Dlg_VerifSave.ShowDialog

            '    If dg = Windows.Forms.DialogResult.OK Then

            '        Me.SaveFileDialog_Project.Title = Me.SaveToolStripMenuItemN.Text
            '        Me.SaveFileDialog_Project.FileName = MyProjet.Nom

            '        If Save_Project() Then

            '            '--> Lecture du fichier
            '            ReadInFile(FileName)

            '        End If

            '    ElseIf dg = Windows.Forms.DialogResult.Ignore Then
            '        '--> Utilisateur ne veut pas sauvegarder l'assemblage en cours

            '        '--> Lecture du fichier
            '        ReadInFile(FileName)

            '    End If

        Else '--> Projet en cours déjà sauvegardé et pas modifié --> pas d'avertissement

            '--> Lecture du fichier
            OuvrirFichier(FileName)

        End If

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub FermerLogiciel()
        Me.Close()
    End Sub

    Private Sub Frm_PMX_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        '--( Gestion Fermeture de la fenêtre

        '--( Enregistrement du projet en cours

        For i As Integer = 0 To MyProjet.Poutres.Count - 1



        Next

        '--( Enregistrement des paramètres d'environnement, y compris les fichiers récents

        EnregistrerOptionsLogiciel(True)

    End Sub

#End Region

#Region " Menu Fichiers "


    Private Sub NewToolStripMenuItemN_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItemN.Click
        '------------------------------------------------------------------------------------------
        '   MENU FICHIER/NOUVEAU
        '------------------------------------------------------------------------------------------

        Frm_AjoutePP.ShowDialog()
    End Sub

    Private Sub OpenToolStripMenuItemN_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItemN.Click
        '------------------------------------------------------------------------------------------
        '   MENU FICHIER/OUVRIR
        '------------------------------------------------------------------------------------------

        OuvrirFichier()

    End Sub

    Private Sub SaveToolStripMenuItemN_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItemN.Click
        '------------------------------------------------------------------------------------------
        '   MENU FICHIER/ENREGISTRER
        '------------------------------------------------------------------------------------------
        EnregistrerProjetEnCours()

    End Sub

    Private Sub SaveAsToolStripMenuItemN_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItemN.Click
        '------------------------------------------------------------------------------------------
        '   MENU FICHIER/ENREGISTRER SOUS
        '------------------------------------------------------------------------------------------
        EnregistrerSousProjetEnCours()
    End Sub

    Private Sub QuitToolStripMenuItemN_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItemN.Click
        '------------------------------------------------------------------------------------------
        '   MENU FICHIER/QUITTER
        '------------------------------------------------------------------------------------------

        FermerLogiciel()

    End Sub


#End Region

#Region " Menu Edit "



#End Region

#Region " Affichage poutres projet PomBtn "

    Private Sub InitialiseCouleurs()

        MyCouleurs.TextBoxEnSaisie = SystemColors.ControlLightLight   ' Me.Panel_SideBar.BackColor
        MyCouleurs.TextBoxFixe = Color.Gray
        MyCouleurs.TextBoxEnInfo = Color.LightGray

        MyCouleurs.SaisieOK = Color.Black
        MyCouleurs.SaisieError = Color.Red

        MyCouleurs.EtqInfo = Color.LightGoldenrodYellow
        MyCouleurs.EtqInfo = Color.Gold

        MyCouleurs.Panels = SystemColors.ControlLightLight   ' Me.Panel_SideBar.BackColor

        Const ALPHABLEND As Integer = 95  '125

        MyCouleurs.ColorWhiteForGradient = Color.FromArgb(ALPHABLEND, 245, 245, 245)
        MyCouleurs.ColorMouseOnBtn = Color.FromArgb(ALPHABLEND / 1.5, 255, 215, 0)
        MyCouleurs.ColorSelectedBtn = Color.FromArgb(ALPHABLEND * 2.5, 255, 215, 0)
        MyCouleurs.ContourNormal = Color.Orange

    End Sub

    Private Sub PreparePomBouton(ByVal MyPomBtn As POMbutton)

        MyPomBtn.CouleurChecked = MyCouleurs.ColorSelectedBtn           'couleur de fond - btn séléctionné
        MyPomBtn.CouleurForGradient = MyCouleurs.ColorWhiteForGradient  'couleur de degradé
        MyPomBtn.CouleurMouseOnBtn = MyCouleurs.ColorMouseOnBtn         'couleur de fond - btn survolé
        MyPomBtn.BorderStyle = BorderStyle.None
        MyPomBtn.RatioArrondi = 0.05
        MyPomBtn.CaptionAlignement = HorizontalAlignment.Center
        MyPomBtn.CouleurContourChecked = MyCouleurs.ContourNormal             'couleur bordure
        MyPomBtn.CouleurContourMouseOn = MyCouleurs.ContourNormal
        MyPomBtn.LContourFond = False

    End Sub


    Public Sub AffichageTViewChk()

        'If MyProjet.Poutres.Count = 0 Then Exit Sub
        InitialiseCouleurs()

        Me.cmb_Projet.Items.Clear()
        Me.cmb_Projet.Items.Add(MyProjet.Nom)
        Me.cmb_Projet.SelectedIndex = 0

        Me.tab_BtnPoutres = New List(Of POMbutton)

        For i As Integer = 0 To MyProjet.Poutres.Count - 1

            Me.tab_BtnPoutres.Add(New POMbutton)
            PreparePomBouton(Me.tab_BtnPoutres(i))

            Me.tab_BtnPoutres(i).Dock = DockStyle.Fill

            Me.tab_BtnPoutres(i).Caption = MyProjet.Poutres(i).BeamID
            Me.tab_BtnPoutres(i).Name = RACnomBTN & CStr(i)
            Me.tab_BtnPoutres(i).Tag = CStr(i)
            AddHandler Me.tab_BtnPoutres(i).Click, AddressOf PomBoutonsClick


            'AddHandler Me.tab_ChkSections(i).Paint, AddressOf chkBox_Section_Paint
        Next

        Me.TLPan_ListPoutres.RowCount = MyProjet.Poutres.Count + 1
        Me.TLPan_ListPoutres.Controls.Clear()
        Me.TLPan_ListPoutres.RowStyles.Clear()

        For i As Integer = 0 To MyProjet.Poutres.Count - 1

            Me.TLPan_ListPoutres.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
            'Me.TableLayoutPanel_TreeChk.RowStyles(i).SizeType = SizeType.Absolute
            'Me.TableLayoutPanel_TreeChk.RowStyles(i).Height = 40.0!
        Next

        Dim HCum = 0
        For i As Integer = 0 To MyProjet.Poutres.Count - 1
            Me.TLPan_ListPoutres.Controls.Add(Me.tab_BtnPoutres(i), 0, i)
            HCum += Me.TLPan_ListPoutres.RowStyles(i).Height
        Next
        Me.TLPan_ListPoutres.Height = HCum

        If Not MyProjet.Poutres.Count = 0 Then Me.tab_BtnPoutres(MyProjet.IndEnCours).Checked = True

    End Sub


    Private Sub PomBoutonsClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '-------------------------------------------------------------------------------------------
        '   Gestion de la selection de la poutre à afficher
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iPos, Indice As Integer

        '--> Gestion

        If Not sender.checked Then  '-> Si bouton déjà séléctionné :
            sender.checked = True       'on le garde checké
            Exit Sub                    'on ne recharge pas la fenêtre fille
        End If

        Dim SenderName As String = sender.name
        ' HideToutesLesFilles()
        UncheckedAllPomBtns(SenderName)

        RedrawAllPomBtns()

        '--> Selection de la poutre correspondant au bouton

        iPos = RACnomBTN.Length
        Indice = CInt(SenderName.Substring(iPos))

        MyProjet.IndEnCours = Indice

        Me.img_Main.Invalidate()

        '--> Bouton checké ne change pas de couleur quand il est survolé (MouseOn)

        sender.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

        MAJToolBarPoutre()

    End Sub

    Private Sub UncheckedAllPomBtns(ByVal SenderName As String)

        For i As Integer = 0 To Me.tab_BtnPoutres.Count - 1
            If SenderName <> Me.tab_BtnPoutres(i).Name Then tab_BtnPoutres(i).Checked = False
        Next

    End Sub


    Private Sub RedrawAllPomBtns()

        For Each MyPomBtn As Object In Me.TLPan_ListPoutres.Controls
            If MyPomBtn.Name.ToUpper.Contains("MYX") Then
                '--> Initialisation de la couleur de survole (MouseOn)
                MyPomBtn.CouleurMouseOnBtn = MyCouleurs.ColorMouseOnBtn
                '--> MAJ du bouton
                MyPomBtn.Invalidate()
            End If
        Next

    End Sub


    Private Sub TLPan_ListPoutres_Resize(sender As Object, e As EventArgs) Handles TLPan_ListPoutres.Resize
        RedrawAllPomBtns()
    End Sub


#End Region

#Region " Affichage des poutres du projet "

    Private Sub ChoixSection_CheckedChanged(sender As Object, e As EventArgs)

        If lBuild Then Exit Sub
        Dim lChecked As Boolean = sender.checked
        lBuild = True

        DeselectionneTousChk(sender.name)

        Dim Indice As Integer = TraiteReal(sender.name.ToString.Substring(3))
        MyProjet.IndEnCours = Indice
        'AffichageFenetreFille()

        sender.checked = lChecked
        sender.backcolor = CouleurBtnActive
        sender.forecolor = SystemColors.WindowText
        lBuild = False

        'MsgBox("Poutre activée :" & MyProjet.Poutres(MyProjet.IndEnCours).Label)

    End Sub

    ''' <summary>
    ''' Déselectionne tous les checkbox de la fenêtre
    ''' </summary>
    '''  ''' <param name="SenderName">[E] Nom du chkbox qui n'est pas déselectionné</param>
    Private Sub DeselectionneTousChk(Optional SenderName As String = "")

        For Each o As Control In Me.TLPan_ListPoutres.Controls
            If TypeOf o Is CheckBox Then
                If o.Name <> SenderName Then
                    CType(o, CheckBox).Checked = False
                    CType(o, CheckBox).BackColor = Me.TLPan_ListPoutres.BackColor
                    'CType(o,CheckBox).
                End If
            End If
        Next

    End Sub

    Public Sub AffichageTViewChkOLD()

        If MyProjet.Poutres.Count = 0 Then Exit Sub

        Me.cmb_Projet.Items.Clear()
        Me.cmb_Projet.Items.Add(MyProjet.Nom)
        Me.cmb_Projet.SelectedIndex = 0

        Me.tab_ChkSections = New List(Of CheckBox)

        For i As Integer = 0 To MyProjet.Poutres.Count - 1

            Me.tab_ChkSections.Add(New CheckBox)
            Me.tab_ChkSections(i).Appearance = Appearance.Button
            Me.tab_ChkSections(i).Dock = DockStyle.Fill
            'Me.tab_ChkSections(i).BackColor = SystemColors.ControlLight 'Me.ToolStrip_Menu_Section.BackColor
            Me.tab_ChkSections(i).BackColor = CouleurBtnNormal
            Me.tab_ChkSections(i).ForeColor = SystemColors.WindowText 'Color.White

            Me.tab_ChkSections(i).Text = MyProjet.Poutres(i).BeamID
            Me.tab_ChkSections(i).Name = "MyX" & CStr(i)
            Me.tab_ChkSections(i).Tag = CStr(i)
            AddHandler Me.tab_ChkSections(i).CheckedChanged, AddressOf ChoixSection_CheckedChanged
            'AddHandler Me.tab_ChkSections(i).Paint, AddressOf chkBox_Section_Paint
        Next

        Me.TLPan_ListPoutres.RowCount = MyProjet.Poutres.Count + 1
        Me.TLPan_ListPoutres.Controls.Clear()
        Me.TLPan_ListPoutres.RowStyles.Clear()

        For i As Integer = 0 To MyProjet.Poutres.Count - 1

            Me.TLPan_ListPoutres.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
            'Me.TableLayoutPanel_TreeChk.RowStyles(i).SizeType = SizeType.Absolute
            'Me.TableLayoutPanel_TreeChk.RowStyles(i).Height = 40.0!
        Next

        Dim HCum = 0
        For i As Integer = 0 To MyProjet.Poutres.Count - 1
            Me.TLPan_ListPoutres.Controls.Add(Me.tab_ChkSections(i), 0, i)
            HCum += Me.TLPan_ListPoutres.RowStyles(i).Height
        Next
        Me.TLPan_ListPoutres.Height = HCum

        Me.tab_ChkSections(MyProjet.IndEnCours).Checked = True

    End Sub

#End Region

#Region "Dessins"

    Private Sub img_Main_Paint(sender As Object, e As PaintEventArgs) Handles img_Main.Paint
        If Not MyProjet.Poutres.Count = 0 Then DessinFrmMain_Coupe(e.Graphics, Me.img_Main.ClientRectangle.Width, Me.img_Main.ClientRectangle.Height, MyProjet.Poutres(MyProjet.IndEnCours), lZoomPlus, lCotation)
    End Sub

    Private Sub Frm_PMX_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        img_Main.Invalidate()
    End Sub

    Private Sub TSbtn_ZoomPlusMoins_Click(sender As Object, e As EventArgs) Handles TSbtn_ZoomPlus.Click, TSbtn_ZoomMoins.Click
        lZoomPlus = sender.name = TSbtn_ZoomPlus.Name
        img_Main.Invalidate()
        MAJ_btnZoomPlus()
    End Sub

    Private Sub PictureBox_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles img_Main.MouseWheel
        lZoomPlus = e.Delta > 0 'Gère si le scrool est en avant (>0) ou en arrière (<0)
        img_Main.Invalidate()
        MAJ_btnZoomPlus()
    End Sub

    ''' <summary>
    ''' Fonction qui permet de gérer l'affichage du zoom en cours 
    ''' </summary>
    Private Sub MAJ_btnZoomPlus()
        Me.TSbtn_ZoomPlus.Checked = lZoomPlus
        Me.TSbtn_ZoomMoins.Checked = Not lZoomPlus
    End Sub

    Private Sub MAJ_btnCotation()
        Me.TSbtn_Cotations.Checked = lCotation
    End Sub

    Private Sub TSbtn_ExpertMode_Click(sender As Object, e As EventArgs) Handles TSbtn_ExpertMode.Click
        LogicielOptions.lExpert = Not LogicielOptions.lExpert
        Me.TSbtn_ExpertMode.Checked = LogicielOptions.lExpert
    End Sub



    Private Sub TSbtn_Cotations_Click(sender As Object, e As EventArgs) Handles TSbtn_Cotations.Click
        lCotation = Not lCotation
        MAJ_btnCotation()
        img_Main.Invalidate()
    End Sub




#End Region


End Class
