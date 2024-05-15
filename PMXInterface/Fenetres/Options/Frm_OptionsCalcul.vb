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

    Public GammaLoc As New cls_Gamma

    Private strAvertissementModif() As String

    Private lSettingsReset As Boolean


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

        Dim Lines As New Cls_LinesOfFile(LogicielFichiers.Langue, True)
        Dim BlocALire() As String = {"OPTCALCULMAIN", "OPTCALGAMMA", "OPTCALSCOPE", "OPTCALCALCUL", "OPTCALSLIMFLOOR", "OPTCALFIRE"}
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
        ' Me.GammaLoc.TransfertFrom(LogicielOptions.Gamma)
        LocalOptionsScope = OptionsScope
        LocalOptionsCalcul = OptionsCalcul
        LocalOptionsSlimFloor = OptionsSlimFloor

        Me.lSettingsReset = False

    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            Me.Text = MyBloc("TITLE")

            Me.PoMBtn_Gamma.Caption = MyBloc("GAMMA")
            Me.PoMbtn_Scope.Caption = MyBloc("SCOPE")
            Me.PoMbtn_Calcul.Caption = MyBloc("CALCUL")
            Me.PoMbtn_SlimFloor.Caption = MyBloc("SLIMFLOOR")
            Me.PoMbtn_Fire.Caption = MyBloc("FIRE")

            Me.btn_Appliquer.Text = MyBloc("APPLY")
            Me.btn_Cancel.Text = MyBloc("CANCEL")
            Me.strAvertissementModif = {MyBloc("MODIF"), MyBloc("MODIF2")}

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
        PreparePomBouton(PoMbtn_Scope)
        PreparePomBouton(PoMbtn_SlimFloor)
        PreparePomBouton(PoMbtn_Calcul)
        PreparePomBouton(PoMbtn_Fire)

        Select Case LastIndexW.OptionsCalcul
            Case Enu_OptionsCalcul.Gamma
                Me.PoMBtn_Gamma.Checked = True
                Me.PoMBtn_Gamma.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn
            Case Enu_OptionsCalcul.Scope
                Me.PoMbtn_Scope.Checked = True
                Me.PoMbtn_Scope.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn
            Case Enu_OptionsCalcul.Calcul
                Me.PoMbtn_Calcul.Checked = True
                Me.PoMbtn_Calcul.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn
            Case Enu_OptionsCalcul.Slimfloor
                Me.PoMbtn_SlimFloor.Checked = True
                Me.PoMbtn_SlimFloor.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn
            Case Enu_OptionsCalcul.Incendie
                Me.PoMbtn_Fire.Checked = True
                Me.PoMbtn_Fire.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn
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

        Me.pan_Contenu.Controls.Clear()

        Select Case LastIndexW.OptionsCalcul
            Case Enu_OptionsCalcul.Gamma
                ' Me.TLpan_Main.Controls.Add(Frm_OptionsCalculsGamma.pan_Gamma, 1, 0)
                Me.pan_Contenu.Controls.Add(Frm_OptionsCalculsGamma.pan_Gamma)
                Frm_OptionsCalculsGamma.InitialiseFrm()

            Case Enu_OptionsCalcul.Scope

                Me.pan_Contenu.Controls.Add(Frm_OptionsCalculScope.pan_Scope)
                Frm_OptionsCalculScope.InitialiseFrm()

            Case Enu_OptionsCalcul.Calcul

                Me.pan_Contenu.Controls.Add(Frm_OptionsCalculCalcul.pan_Calcul)
                Frm_OptionsCalculCalcul.InitialiseFrm()

            Case Enu_OptionsCalcul.Slimfloor

                Me.pan_Contenu.Controls.Add(Frm_OptionsCalculSlimFloor.pan_Slimfloor)
                Frm_OptionsCalculSlimFloor.InitialiseFrm()

            Case Enu_OptionsCalcul.Incendie

                Me.pan_Contenu.Controls.Add(Frm_OptionsCalculIncendie.pan_Incendie)
                Frm_OptionsCalculIncendie.InitialiserFenetre()

        End Select

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_Appliquer_Click(sender As Object, e As EventArgs) Handles btn_Appliquer.Click

        Dim lModif As Boolean
        If ValideSaisie() Then

            TransfertSaisie(lModif)

            If lModif Then
                MsgBox(strAvertissementModif(0) & Chr(13) & Chr(10) & strAvertissementModif(1))
            End If

            Me.Close()
        End If


    End Sub

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False
        Dim lExpert As Boolean = LogicielOptions.lExpert

        '# Fenêtre Gamma
        GereTransfertValeur(GammaLoc.GammaC, LogicielOptions.Gamma.GammaC, lModif)
        GereTransfertValeur(GammaLoc.GammaC_fi, LogicielOptions.Gamma.GammaC_fi, lModif)
        'GereTransfertValeur(GammaLoc.GammaS_fi, LogicielOptions.Gamma.GammaS_fi, lModif)
        GereTransfertValeur(GammaLoc.GammaG_inf, LogicielOptions.Gamma.GammaG_inf, lModif)
        GereTransfertValeur(GammaLoc.GammaG_sup, LogicielOptions.Gamma.GammaG_sup, lModif)
        GereTransfertValeur(GammaLoc.GammaM0, LogicielOptions.Gamma.GammaM0, lModif)
        GereTransfertValeur(GammaLoc.GammaM1, LogicielOptions.Gamma.GammaM1, lModif)
        GereTransfertValeur(GammaLoc.GammaM2, LogicielOptions.Gamma.GammaM2, lModif)
        GereTransfertValeur(GammaLoc.GammaM_fi, LogicielOptions.Gamma.GammaM_fi, lModif)
        GereTransfertValeur(GammaLoc.GammaS_fi, LogicielOptions.Gamma.GammaS_fi, lModif)
        GereTransfertValeur(GammaLoc.GammaVs, LogicielOptions.Gamma.GammaVs, lModif)
        GereTransfertValeur(GammaLoc.GammaVc, LogicielOptions.Gamma.GammaVc, lModif)
        GereTransfertValeur(GammaLoc.lGammaV_unique, LogicielOptions.Gamma.lGammaV_unique, lModif)
        GereTransfertValeur(GammaLoc.GammaV_fi, LogicielOptions.Gamma.GammaV_fi, lModif)
        GereTransfertValeur(GammaLoc.GammaP, LogicielOptions.Gamma.GammaP, lModif)
        GereTransfertValeur(GammaLoc.GammaQ, LogicielOptions.Gamma.GammaQ, lModif)
        GereTransfertValeur(GammaLoc.GammaS, LogicielOptions.Gamma.GammaS, lModif)

        GereTransfertValeur(GammaLoc.Psi0_Q1, LogicielOptions.Gamma.Psi0_Q1, lModif)
        GereTransfertValeur(GammaLoc.Psi1_Q1, LogicielOptions.Gamma.Psi1_Q1, lModif)
        GereTransfertValeur(GammaLoc.Psi2_Q1, LogicielOptions.Gamma.Psi2_Q1, lModif)

        GereTransfertValeur(GammaLoc.Psi0_Q2, LogicielOptions.Gamma.Psi0_Q2, lModif)
        GereTransfertValeur(GammaLoc.Psi1_Q2, LogicielOptions.Gamma.Psi1_Q2, lModif)
        GereTransfertValeur(GammaLoc.Psi2_Q2, LogicielOptions.Gamma.Psi2_Q2, lModif)

        '# Fenêtre Scope
        If lExpert Then
            GereTransfertValeur(LocalOptionsScope.PorteeMin, OptionsScope.PorteeMin, lModif)
            GereTransfertValeur(LocalOptionsScope.PorteeMax, OptionsScope.PorteeMax, lModif)
            GereTransfertValeur(LocalOptionsScope.PorteeConsoleMin, OptionsScope.PorteeConsoleMin, lModif)
            GereTransfertValeur(LocalOptionsScope.RatioPorteeConsoleMax, OptionsScope.RatioPorteeConsoleMax, lModif)
            GereTransfertValeur(LocalOptionsScope.RatioEpRenformisMax, OptionsScope.RatioEpRenformisMax, lModif)
            GereTransfertValeur(LocalOptionsScope.EpDalleMixteMin, OptionsScope.EpDalleMixteMin, lModif)
            GereTransfertValeur(LocalOptionsScope.EpDallePleineMin, OptionsScope.EpDallePleineMin, lModif)
            GereTransfertValeur(LocalOptionsScope.RhoCBetonLegerMax, OptionsScope.RhoCBetonLegerMax, lModif)
            GereTransfertValeur(LocalOptionsScope.RhoCBetonLegerMin, OptionsScope.RhoCBetonLegerMin, lModif)
        End If
        GereTransfertValeur(LocalOptionsScope.ThetaH, OptionsScope.ThetaH, lModif)

        '# Fenêtre Slimfloor
        If lExpert Then
            GereTransfertValeur(LocalOptionsSlimFloor.hslimmax, OptionsSlimFloor.hslimmax, lModif)
            GereTransfertValeur(LocalOptionsSlimFloor.bappmin, OptionsSlimFloor.bappmin, lModif)
            GereTransfertValeur(LocalOptionsSlimFloor.tpinfmin, OptionsSlimFloor.tpinfmin, lModif)
        End If

        '# Fenêtre Options Calculs
        If OptionsCalcul.Norme <> LocalOptionsCalcul.Norme Then lModif = True
        OptionsCalcul.Norme = LocalOptionsCalcul.Norme
        GereTransfertValeur(LocalOptionsCalcul.lLargeurEfficaceSimplifiee, OptionsCalcul.lLargeurEfficaceSimplifiee, lModif)
        GereTransfertValeur(LocalOptionsCalcul.lCompressionArma, OptionsCalcul.lCompressionArma, lModif)
        GereTransfertValeur(LocalOptionsCalcul.EsArmatures, OptionsCalcul.EsArmatures, lModif)
        GereTransfertValeur(LocalOptionsCalcul.DeltaCDev, OptionsCalcul.DeltaCDev, lModif)
        GereTransfertValeur(LocalOptionsCalcul.dMaxNodes, OptionsCalcul.dMaxNodes, lModif)
        GereTransfertValeur(LocalOptionsCalcul.nbMinNodesConsole, OptionsCalcul.nbMinNodesConsole, lModif)
        GereTransfertValeur(LocalOptionsCalcul.nbMinNodesTravee, OptionsCalcul.nbMinNodesTravee, lModif)

        If lExpert Then
            GereTransfertValeur(LocalOptionsCalcul.PsiLPermanent, OptionsCalcul.PsiLPermanent, lModif)
            GereTransfertValeur(LocalOptionsCalcul.PsiLRetrait, OptionsCalcul.PsiLRetrait, lModif)
            GereTransfertValeur(LocalOptionsCalcul.TimeT0SH(0), OptionsCalcul.TimeT0SH(0), lModif)
            GereTransfertValeur(LocalOptionsCalcul.TimeT0SH(1), OptionsCalcul.TimeT0SH(1), lModif)
        End If
        GereTransfertValeur(LocalOptionsCalcul.TimeT0G1(0), OptionsCalcul.TimeT0G1(0), lModif)
        GereTransfertValeur(LocalOptionsCalcul.TimeT0G1(1), OptionsCalcul.TimeT0G1(1), lModif)
        GereTransfertValeur(LocalOptionsCalcul.TimeT0G2(0), OptionsCalcul.TimeT0G2(0), lModif)
        GereTransfertValeur(LocalOptionsCalcul.TimeT0G2(1), OptionsCalcul.TimeT0G2(1), lModif)

        GereTransfertValeur(LocalOptionsCalcul.EtaW, OptionsCalcul.EtaW, lModif)

        'AppliquerReglagesProjetEnCours() '--> GUD: Désactivation de cette ligne car elle modifie l'ensemble des poutres du projet, ce qui n'est pas souhaitable

    End Sub

    'Private Sub AppliquerReglagesProjetEnCours()

    '    For i As Integer = 0 To MyProjet.Poutres.Count - 1
    '        MyProjet.Poutres(i).Dalle.ThetaRd = OptionsScope.ThetaH
    '        MyProjet.Poutres(i).Param.Norme = OptionsCalcul.Norme
    '        MyProjet.Poutres(i).Param.lCompressionArma = OptionsCalcul.lCompressionArma

    '        MyProjet.Poutres(i).Param.PsiLPermanent = OptionsCalcul.PsiLPermanent
    '        MyProjet.Poutres(i).Param.PsiLRetrait = OptionsCalcul.PsiLRetrait

    '        For j As Integer = 0 To 1
    '            MyProjet.Poutres(i).Param.AgeT0G1(j) = OptionsCalcul.TimeT0G1(j)
    '            MyProjet.Poutres(i).Param.AgeT0G2(j) = OptionsCalcul.TimeT0G2(j)
    '            MyProjet.Poutres(i).Param.AgeT0SH(j) = OptionsCalcul.TimeT0SH(j)
    '        Next

    '    Next
    '    'IL faut faire la même chose à l'oouverture des fhciers et la création d'une poutre
    'End Sub

    Private Function ValideSaisie() As Boolean

        Dim lOK As Boolean = True
        Return lOK

    End Function


#End Region

#Region "    Gestion des boutons - Paint Overrides "

    Private Sub PomBoutonsClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles PoMBtn_Gamma.Click, PoMbtn_Scope.Click, PoMbtn_Calcul.Click, PoMbtn_Fire.Click, PoMbtn_SlimFloor.Click

        If Not sender.checked Then  '-> Si bouton déjà séléctionné :
            sender.checked = True       'on le garde checké
            Exit Sub                    'on ne recharge pas la fenêtre fille
        End If

        Dim SenderName As String = sender.name
        ' HideToutesLesFilles()
        UncheckedAllPomBtns(SenderName)
        Select Case SenderName
            Case Me.PoMBtn_Gamma.Name
                LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Gamma
                AfficherFenetreFille()

            Case Me.PoMbtn_Scope.Name
                LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Scope
                AfficherFenetreFille()

            Case Me.PoMbtn_SlimFloor.Name
                LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Slimfloor
                AfficherFenetreFille()

            Case Me.PoMbtn_Calcul.Name
                LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Calcul
                AfficherFenetreFille()

            Case Me.PoMbtn_Fire.Name
                LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Incendie
                AfficherFenetreFille()

        End Select
        RedrawAllPomBtns()

        '--> Bouton checké ne change pas de couleur quand il est survolé (MouseOn)
        sender.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn

        ' Me.etq_Debug.Text = LastIndexWindow.ConfigurationNEW.ToString

        'If LastIndexW.OptionsLogiciel <> Enu_OptionsLogiciel.Expert Then
        '    Me.AcceptButton = Me.btn_Appliquer
        'Else
        '    Me.AcceptButton = Nothing
        'End If

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

        If SenderName <> Me.PoMBtn_Gamma.Name Then Me.PoMBtn_Gamma.Checked = False
        If SenderName <> Me.PoMbtn_Scope.Name Then Me.PoMbtn_Scope.Checked = False
        If SenderName <> Me.PoMbtn_Calcul.Name Then Me.PoMbtn_Calcul.Checked = False
        If SenderName <> Me.PoMbtn_SlimFloor.Name Then Me.PoMbtn_SlimFloor.Checked = False
        If SenderName <> Me.PoMbtn_Fire.Name Then Me.PoMbtn_Fire.Checked = False

    End Sub

    Private Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        My.Settings.Reset()
        Me.lSettingsReset = True
    End Sub



#End Region


End Class