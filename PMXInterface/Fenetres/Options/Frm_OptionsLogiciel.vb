Imports PMXMoteur2

Public Class Frm_OptionsLogiciel


#Region " Variables "

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

    Dim lBuild As Boolean = True

    Private MyCouleurs As struc_Colors

    Public BlocLangues As Dictionary(Of String, Dictionary(Of String, String))
    Const BALISE As String = "OPTSOFTMAIN"


#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_OptionsLogiciel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        ChargesBlocsLangues()
        GestionLangues(BlocLangues(BALISE))
        GestionStyle()
        'GestionUnites()
        InitialiseParametresLocaux()
        AfficherFenetreFille()
        lBuild = False
    End Sub

    Public Sub ChargesBlocsLangues()
        '----------------------------------------------------------------------------------------
        '   Récupération des blocs langues pour toutes les fenêtres fille
        '----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Lines As New Cls_LinesOfFile(LogicielFichiers.Langue, False)
        Dim BlocALire() As String = {"OPTSOFTMAIN", "OPTSOFTGENERAL", "OPTSOFTUNITS"}
        Dim lBlocEnCours As Boolean = False
        Dim BlocEnCours As String = Nothing
        Dim MotCle, Argument As String
        Dim MyBloc As Dictionary(Of String, String) = Nothing
        Dim Index As Integer

        '--> Initialisation

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

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            Me.Text = MyBloc("TITLE")

            Me.PoMBtn_General.Caption = MyBloc("GENERAL")
            Me.PoMbtn_Directories.Caption = MyBloc("DIRECTORIES")
            Me.PoMbtn_Units.Caption = MyBloc("UNITS")

            Me.btn_Appliquer.Text = MyBloc("APPLY")
            Me.btn_Cancel.Text = MyBloc("CANCEL")

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
        'PreparePomBouton(PoMBtn)

        Select Case LastIndexW.OptionsLogiciel
            Case Enu_OptionsLogiciel.General
                Me.PoMBtn_General.Checked = True
                Me.PoMBtn_General.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

        End Select

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

    End Sub

    Private Sub AfficherFenetreFille()

        Me.pan_Contenu.Controls.Clear()

        Select Case LastIndexW.OptionsLogiciel
            Case Enu_OptionsLogiciel.General

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielGeneral.pan_General)
                Frm_OptionsLogicielGeneral.InitialiseFrm()

            Case Enu_OptionsLogiciel.Directories

            Case Enu_OptionsLogiciel.Units

                Me.pan_Contenu.Controls.Add(Frm_OptionsLogicielUnits.pan_Units)
                Frm_OptionsLogicielUnits.InitialiseFrm()

            Case Enu_OptionsLogiciel.Databases

            Case Enu_OptionsLogiciel.Expert


        End Select

    End Sub


#End Region


#Region "    Gestion des boutons - Paint Overrides "

    Private Sub PomBoutonsClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles PoMBtn_General.Click, PoMbtn_Directories.Click, PoMbtn_Units.Click

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

        'If SenderName <> Me.PomBtnExpert.Name Then MAJBtnExpert()

    End Sub


#End Region


End Class