Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsCalcul

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
    Const BALISE As String = "OPTCALCULMAIN"

    Public GammaLoc As New Cls_Gamma

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_OptionsCalcul_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitialiserFenetre()

    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        ChargesBlocsLangues()
        GestionLangues(BlocLangues(balise))
        GestionStyle()
        GestionUnites()
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
        Dim BlocALire() As String = {"OPTCALCULMAIN", "OPTCALGAMMA"}
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

    Private Sub InitialiseParametresLocaux()

        Me.GammaLoc = LogicielOptions.Gamma.Clone

    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            Me.Text = MyBloc("TITLE")

            Me.PoMBtn_Gamma.Caption = MyBloc("GAMMA")

            Me.btn_Appliquer.Text = MyBloc("APPLY")
            Me.btn_Cancel.Text = MyBloc("CANCEL")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        InitialiseCouleurs()
        PreparePomBouton(PoMBtn_Gamma)

        Select Case LastIndexW.OptionsCalcul
            Case Enu_OptionsCalcul.Gamma
                Me.PoMBtn_Gamma.Checked = True
                Me.PoMBtn_Gamma.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

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

    Private Sub AfficherFenetreFille()

        Select Case LastIndexW.OptionsCalcul
            Case Enu_OptionsCalcul.Gamma
                ' Me.TLpan_Main.Controls.Add(Frm_OptionsCalculsGamma.pan_Gamma, 1, 0)
                Me.pan_Contenu.Controls.Add(Frm_OptionsCalculsGamma.pan_Gamma)
                Frm_OptionsCalculsGamma.InitialiseFrm()
        End Select

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_Appliquer_Click(sender As Object, e As EventArgs) Handles btn_Appliquer.Click

        Dim lModif As Boolean
        If ValideSaisie() Then

            TransfertSaisie(lModif)

            If lModif Then

            End If

        End If


    End Sub

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        GereTransfertValeur(GammaLoc.GammaC, LogicielOptions.Gamma.GammaC, lModif)
        GereTransfertValeur(GammaLoc.GammaC_fi, LogicielOptions.Gamma.GammaC_fi, lModif)
        GereTransfertValeur(GammaLoc.GammaG_inf, LogicielOptions.Gamma.GammaG_inf, lModif)
        GereTransfertValeur(GammaLoc.GammaG_sup, LogicielOptions.Gamma.GammaG_sup, lModif)
        GereTransfertValeur(GammaLoc.GammaM0, LogicielOptions.Gamma.GammaM0, lModif)
        GereTransfertValeur(GammaLoc.GammaM1, LogicielOptions.Gamma.GammaM1, lModif)
        GereTransfertValeur(GammaLoc.GammaM2, LogicielOptions.Gamma.GammaM2, lModif)
        GereTransfertValeur(GammaLoc.GammaM_fi, LogicielOptions.Gamma.GammaM_fi, lModif)
        GereTransfertValeur(GammaLoc.GammaP, LogicielOptions.Gamma.GammaP, lModif)
        GereTransfertValeur(GammaLoc.GammaQ, LogicielOptions.Gamma.GammaQ, lModif)
        GereTransfertValeur(GammaLoc.GammaS, LogicielOptions.Gamma.GammaS, lModif)

        GereTransfertValeur(GammaLoc.Psi0_Q1, LogicielOptions.Gamma.Psi0_Q1, lModif)
        GereTransfertValeur(GammaLoc.Psi1_Q1, LogicielOptions.Gamma.Psi1_Q1, lModif)
        GereTransfertValeur(GammaLoc.Psi2_Q1, LogicielOptions.Gamma.Psi2_Q1, lModif)

        LogicielOptions.Gamma.Psi0_Q2 = LogicielOptions.Gamma.Psi0_Q1
        LogicielOptions.Gamma.Psi1_Q2 = LogicielOptions.Gamma.Psi1_Q1
        LogicielOptions.Gamma.Psi2_Q2 = LogicielOptions.Gamma.Psi2_Q1

    End Sub


    Private Function ValideSaisie() As Boolean

        Dim lOK As Boolean = True
        Return lOK

    End Function


#End Region

End Class