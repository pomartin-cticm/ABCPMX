Imports PMXMoteur2

Public Class Frm_OptionsLogiciel

#Region " Déclarations "

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

    Structure Struc_LocalOptionsLogiciel

        Public lExpert As Boolean                   'Activation Mode Expert
        'Public lDebug As Boolean                    'Fonctionnement en mode debug
        Public IndLangue As Integer                 'Indice de la langue de l'interface
        Public IndLangueNDC As Integer              'Indice de la langue de la note de calcul

        Public IndUnitLongueur As Integer           'Indice de l'unité de longueur utilisée
        Public IndUnitDimension As Integer          'Indice de l'unité de longueur utilisée
        Public IndUnitEffort As Integer             'Indice de l'unité d'effort utilisée
        Public IndUnitMoment As Integer             'Indice de l'unité de moment utilisée
        Public IndUnitInerties As Integer           'Indice de l'unité des inerties
        Public IndUnitContraintes As Integer        'Indice de l'unité des contraintes
        Public IndUnitModulesY As Integer           'Indice de l'unité des modules d'élasticité

        Public UserName As String                   'Nom de l'utilisateur
        Public CompanyName As String                'Nom de l'entreprise

        Public RepertoireTravail As String          'Répertoire de l'espace de travail
        Public lRepTravailDefault As Boolean        'Répertoire de travail par défaut ou le dernier utilisé

        Public lUpdateStart As Boolean              'Vérification des mises à jour au démarrage du logiciel

    End Structure

#End Region

#Region " Variables "

    Dim lBuild As Boolean = True

    Private MyCouleurs As struc_Colors

    Public BlocLangues As Dictionary(Of String, Dictionary(Of String, String))
    Const BALISE As String = "OPTSOFTMAIN"

    Public pLocalLogicielOptions As Struc_LocalOptionsLogiciel
    Public pLocalRepWDefaut As String = ""
    Public pLocallDefaultRepW As Boolean

    Dim htBtn As Integer = 40

    Dim pFichierLangue As String

    Public pLocalOptionsNdC As struc_LocalOptionsNdC


    Structure struc_LocalOptionsNdC
        Dim lShowHivossCurve As Boolean
        Dim lDispFMLoadCase As Boolean
        Dim lDispFM_ULS As Boolean
        Dim lDispFM_SLS As Boolean
        Dim lDispFM_FLS As Boolean

    End Structure
#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_OptionsLogiciel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        InitialiseParametresLocaux()
        ChargesBlocsLangues()
        GestionLangues(BlocLangues(BALISE))
        GestionStyle()
        'GestionUnites()
        AfficherFenetreFille()
        MAJIExpert()
        lBuild = False
    End Sub

    Public Sub ChargesBlocsLangues()
        '----------------------------------------------------------------------------------------
        '   Récupération des blocs langues pour toutes les fenêtres fille
        '----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Lines As Cls_LinesOfFile
        Dim BlocALire() As String = {"OPTSOFTMAIN", "OPTSOFTDATABASES", "OPTSOFTDIRECTORIES", "OPTSOFTEXPERT", "OPTSOFTGENERAL", "OPTSOFTUNITS", "OPTSOFTCALCULSHEET"}
        Dim lBlocEnCours As Boolean = False
        Dim BlocEnCours As String = Nothing
        Dim MotCle, Argument As String
        Dim MyBloc As Dictionary(Of String, String) = Nothing
        Dim Index As Integer

        '--> Initialisation

        InitialiseLNGFileName(pLocalLogicielOptions.IndLangue, pFichierLangue)
        Lines = New Cls_LinesOfFile(pFichierLangue, False)

        BlocLangues = New Dictionary(Of String, Dictionary(Of String, String))

        '--> Boucle sur les lignes

        For i As Integer = 0 To Lines.Lines.Count - 1

            If Lines.Lines(i).Trim.IndexOf("#") = 0 Then
                MotCle = Lines.Lines(i).Trim.ToUpper.Substring(1)
                If (Array.IndexOf(BlocALire, MotCle) > -1) Then
                    lBlocEnCours = True
                    BlocEnCours = MotCle
                    MyBloc = New Dictionary(Of String, String)
                    MyBloc.Clear()
                End If
            ElseIf Lines.Lines(i).Trim.Length = 0 Then
                If lBlocEnCours Then
                    BlocLangues.Add(BlocEnCours, MyBloc)
                End If
                lBlocEnCours = False
            ElseIf lBlocEnCours Then
                Index = Lines.Lines(i).IndexOf("=")
                If Index > -1 Then
                    MotCle = Lines.Lines(i).Substring(0, Index).Trim
                    Argument = Lines.Lines(i).Substring(Index + 1).Trim
                    MyBloc.Add(MotCle, Argument)
                End If

            End If

        Next

        If lBlocEnCours Then
            BlocLangues.Add(BlocEnCours, MyBloc)
        End If
        lBlocEnCours = False

    End Sub

    Public Sub ReinitLangues()
        GestionLangues(BlocLangues(BALISE))
    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            Me.Text = MyBloc("TITLE")

            Me.PoMBtn_General.Caption = MyBloc("GENERAL")
            Me.PoMbtn_Directories.Caption = MyBloc("DIRECTORIES")
            Me.PoMbtn_Units.Caption = MyBloc("UNITS")
            Me.PoMbtn_Expert.Caption = MyBloc("EXPERT")
            Me.PoMbtn_Databases.Caption = MyBloc("DATABASES")
            Me.PoMbtn_NdC.Caption = MyBloc("CALCULSHEET")

            Me.btn_Appliquer.Text = MyBloc("APPLY")
            Me.btn_Cancel.Text = MyBloc("CANCEL")

            Me.lbl_ExpertMode.Text = MyBloc("EXPERTMODEACTIVE")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        InitialiseCouleurs()
        PreparePomBouton(PoMBtn_General)
        PreparePomBouton(PoMbtn_Directories)
        PreparePomBouton(PoMbtn_Units)
        PreparePomBouton(PoMbtn_Expert)
        PreparePomBouton(PoMbtn_Databases)
        PreparePomBouton(PoMbtn_NdC)
        PoMbtn_Expert.Visible = LogicielOptions.lExpert

        'PreparePomBouton(PoMBtn)

        Select Case LastIndexW.OptionsLogiciel
            Case Enu_OptionsLogiciel.General
                Me.PoMBtn_General.Checked = True
                Me.PoMBtn_General.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

        End Select

        For i As Integer = 0 To 3
            Me.TLpan_Gauche.RowStyles(i).Height = htBtn
        Next

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

    Private Sub InitialiseCouleurs()

        MyCouleurs.TextBoxEnSaisie = Me.pan_Main.BackColor
        MyCouleurs.TextBoxFixe = Color.Gray
        MyCouleurs.TextBoxEnInfo = Color.LightGray

        MyCouleurs.SaisieOK = Color.Black
        MyCouleurs.SaisieError = Color.Red

        MyCouleurs.EtqInfo = Color.LightGoldenrodYellow
        MyCouleurs.EtqInfo = Color.Gold

        MyCouleurs.Panels = Me.pan_Main.BackColor

        Const ALPHABLEND As Integer = 95  '125

        'MyCouleurs.ColorWhiteForGradient = Color.FromArgb(ALPHABLEND, 245, 245, 245)
        'MyCouleurs.ColorMouseOnBtn = Color.FromArgb(ALPHABLEND, 255, 215, 0)
        'MyCouleurs.ColorSelectedBtn = Color.FromArgb(ALPHABLEND * 1.5, 255, 215, 0)

        'MyCouleurs.ContourMouse = Color.DarkOrange
        'MyCouleurs.ContourNormal = Color.Black
        'MyCouleurs.ContourSelect = Color.Orange

        MyCouleurs.ColorWhiteForGradient = Color.FromArgb(ALPHABLEND, 245, 245, 245)
        MyCouleurs.ColorMouseOnBtn = Color.FromArgb(ALPHABLEND / 1.5, 255, 215, 0)
        MyCouleurs.ColorSelectedBtn = Color.FromArgb(ALPHABLEND * 2.5, 255, 215, 0)
        MyCouleurs.ContourNormal = Color.Orange

    End Sub

    Private Sub InitialiseParametresLocaux()

        pLocalLogicielOptions.IndUnitContraintes = LogicielOptions.IndUnitContraintes
        pLocalLogicielOptions.IndUnitInerties = LogicielOptions.IndUnitInerties
        pLocalLogicielOptions.IndUnitDimension = LogicielOptions.IndUnitDimension
        pLocalLogicielOptions.IndUnitEffort = LogicielOptions.IndUnitEffort
        pLocalLogicielOptions.IndUnitLongueur = LogicielOptions.IndUnitLongueur
        pLocalLogicielOptions.IndUnitModulesY = LogicielOptions.IndUnitModulesY
        pLocalLogicielOptions.IndUnitMoment = LogicielOptions.IndUnitMoment
        pLocalLogicielOptions.lExpert = LogicielOptions.lExpert

        pLocalLogicielOptions.IndLangue = LogicielOptions.IndLangue
        pLocalLogicielOptions.IndLangueNDC = LogicielOptions.IndLangueNDC

        pLocalRepWDefaut = LogicielRep.TravailDefaut
        pLocallDefaultRepW = LogicielRep.lTravailDefaut

        pLocalLogicielOptions.UserName = LogicielOptions.UserName
        pLocalLogicielOptions.CompanyName = LogicielOptions.CompanyName

        pLocalOptionsNdC.lShowHivossCurve = OptionsNdC.lShowHivossCurve
        pLocalOptionsNdC.lDispFMLoadCase = OptionsNdC.lDispFMLoadCase
        pLocalOptionsNdC.lDispFM_SLS = OptionsNdC.lDispFM_SLS
        pLocalOptionsNdC.lDispFM_FLS = OptionsNdC.lDispFM_FLS
        pLocalOptionsNdC.lDispFM_ULS = OptionsNdC.lDispFM_ULS
    End Sub

    Private Sub AfficherFenetreFille()

        Me.pan_Contenu.Controls.Clear()

        Select Case LastIndexW.OptionsLogiciel
            Case Enu_OptionsLogiciel.General

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielGeneral.pan_General)
                Frm_OptionsLogicielGeneral.InitialiseFrm()

            Case Enu_OptionsLogiciel.Directories

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielDirectories.pan_General)
                Frm_OptionsLogicielDirectories.InitialiseFrm()

            Case Enu_OptionsLogiciel.Units

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielUnits.pan_Units)
                Frm_OptionsLogicielUnits.InitialiseFrm()

            Case Enu_OptionsLogiciel.Databases

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielDataBases.pan_DataBases)
                Frm_OptionsLogicielDataBases.InitialiseFrm()

            Case Enu_OptionsLogiciel.NoteCalcul
                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielNdC.pan_NdC)
                Frm_OptionsLogicielNdC.InitialiseFrm()

            Case Enu_OptionsLogiciel.Expert

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielExpert.pan_Expert)
                Frm_OptionsLogicielExpert.InitialiseFrm()

        End Select

    End Sub


#End Region

#Region "    Gestion des boutons - Paint Overrides "

    Private Sub PomBoutonsClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles PoMBtn_General.Click, PoMbtn_Directories.Click, PoMbtn_Units.Click, PoMbtn_Expert.Click, PoMbtn_Databases.Click, PoMbtn_NdC.Click

        If Not sender.checked Then  '-> Si bouton déjà séléctionné :
            sender.checked = True       'on le garde checké
            Exit Sub                    'on ne recharge pas la fenêtre fille
        End If

        Dim SenderName As String = sender.name
        ' HideToutesLesFilles()
        UncheckedAllPomBtns(SenderName)
        Select Case SenderName
            Case Me.PoMBtn_General.Name
                LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General
                AfficherFenetreFille()

            Case Me.PoMbtn_Directories.Name
                LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.Directories
                AfficherFenetreFille()

            Case Me.PoMbtn_Units.Name
                LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.Units
                AfficherFenetreFille()

            Case Me.PoMbtn_Expert.Name
                LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.Expert
                AfficherFenetreFille()

            Case Me.PoMbtn_Databases.Name
                LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.Databases
                AfficherFenetreFille()

            Case Me.PoMbtn_NdC.Name
                LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.NoteCalcul
                AfficherFenetreFille()

        End Select
        RedrawAllPomBtns()

        '--> Bouton checké ne change pas de couleur quand il est survolé (MouseOn)
        sender.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

        ' Me.etq_Debug.Text = LastIndexWindow.ConfigurationNEW.ToString

        If LastIndexW.OptionsLogiciel <> Enu_OptionsLogiciel.Expert Then
            Me.AcceptButton = Me.btn_Appliquer
        Else
            Me.AcceptButton = Nothing
        End If

    End Sub

    Private Sub RedrawAllPomBtns()

        For Each MyPomBtn As Object In Me.TLpan_Gauche.Controls
            If MyPomBtn.Name.ToUpper.Contains("POM") Then
                '--> Initialisation de la couleur de survole (MouseOn)
                MyPomBtn.CouleurMouseOnBtn = MyCouleurs.ColorMouseOnBtn
                '--> MAJ du bouton
                MyPomBtn.Invalidate()
            End If
        Next

    End Sub

    Private Sub UncheckedAllPomBtns(ByVal SenderName As String)

        If SenderName <> Me.PoMBtn_General.Name Then Me.PoMBtn_General.Checked = False
        If SenderName <> Me.PoMbtn_Directories.Name Then Me.PoMbtn_Directories.Checked = False
        If SenderName <> Me.PoMbtn_Units.Name Then Me.PoMbtn_Units.Checked = False
        If SenderName <> Me.PoMbtn_Expert.Name Then Me.PoMbtn_Expert.Checked = False
        If SenderName <> Me.PoMbtn_NdC.Name Then Me.PoMbtn_NdC.Checked = False
        If SenderName <> Me.PoMbtn_Databases.Name Then Me.PoMbtn_Databases.Checked = False

        'If SenderName <> Me.PomBtnExpert.Name Then MAJBtnExpert()

    End Sub

#End Region

#Region " Gestion Mode Expert "

    Private Sub AccesModeExpert()

        '--> Affichage de la fenêtre fille Expert
        'AfficheMaFille(Frm_O_Expert.TableLayoutPanel_Contenu)
        LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.Expert
        AfficherFenetreFille()

        '--> Maj des boutons
        Me.PoMbtn_Expert.Visible = True
        UncheckedAllPomBtns(Me.PoMbtn_Expert.Name)
        Me.PoMbtn_Expert.Checked = True
        Me.AcceptButton = Nothing
        RedrawAllPomBtns()
        Me.PoMbtn_Expert.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

    End Sub

    Public Sub MAJIExpert()
        Me.PoMbtn_Expert.Visible = pLocalLogicielOptions.lExpert
        Me.pan_Expert.Visible = pLocalLogicielOptions.lExpert
    End Sub

    Public Sub MAJModeExpert(ByVal lMAJ_BtnExpert As Boolean)

        '   If lMAJ_BtnExpert Then Me.PoMbtn_Expert.Visible = Options_Local.lExpertMode

        '  Me.img_Expert.Visible = Options_Local.lExpertMode
        '  Me.etq_Expert.Visible = Options_Local.lExpertMode

    End Sub

#End Region

#Region "===Fermeture==="

    Private Sub btn_Appliquer_Click(sender As Object, e As EventArgs) Handles btn_Appliquer.Click

        Dim lModif As Boolean

        If My.Computer.Keyboard.CtrlKeyDown Then    'touche Ctrl + click OK

            AccesModeExpert()

        Else
            If ValideSaisie() Then
                TransfereSaisie(lModif)
                Me.Close()
            End If
        End If

    End Sub

    Private Sub TransfereSaisie(ByRef lModif As Boolean)

        lModif = False

        GereTransfertValeur(Me.pLocalLogicielOptions.IndLangue, LogicielOptions.IndLangue, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.IndLangueNDC, LogicielOptions.IndLangueNDC, lModif)

        GereTransfertValeur(Me.pLocalLogicielOptions.UserName, LogicielOptions.UserName, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.CompanyName, LogicielOptions.CompanyName, lModif)

        GereTransfertValeur(Me.pLocalLogicielOptions.IndUnitDimension, LogicielOptions.IndUnitDimension, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.IndUnitLongueur, LogicielOptions.IndUnitLongueur, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.IndUnitContraintes, LogicielOptions.IndUnitContraintes, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.IndUnitEffort, LogicielOptions.IndUnitEffort, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.IndUnitInerties, LogicielOptions.IndUnitInerties, lModif)
        GereTransfertValeur(Me.pLocalLogicielOptions.IndUnitMoment, LogicielOptions.IndUnitMoment, lModif)

        GereTransfertValeur(Me.pLocallDefaultRepW, LogicielRep.lTravailDefaut, lModif)

        GereTransfertValeur(Me.pLocalLogicielOptions.lExpert, LogicielOptions.lExpert, lModif)

        GereTransfertValeur(Me.pLocalOptionsNdC.lShowHivossCurve, OptionsNdC.lShowHivossCurve, lModif)
        GereTransfertValeur(Me.pLocalOptionsNdC.lDispFM_ULS, OptionsNdC.lDispFM_ULS, lModif)
        GereTransfertValeur(Me.pLocalOptionsNdC.lDispFM_SLS, OptionsNdC.lDispFM_SLS, lModif)
        GereTransfertValeur(Me.pLocalOptionsNdC.lDispFM_FLS, OptionsNdC.lDispFM_FLS, lModif)
        GereTransfertValeur(Me.pLocalOptionsNdC.lDispFMLoadCase, OptionsNdC.lDispFMLoadCase, lModif)

    End Sub

    Private Function ValideSaisie() As Boolean
        Return True
    End Function


#End Region
End Class