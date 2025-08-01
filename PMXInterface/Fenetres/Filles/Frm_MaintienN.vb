Public Class Frm_MaintienN

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

#End Region

#Region " Variables "

    Dim lBuild As Boolean

    Private MyCouleurs As struc_Colors

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_MaintienN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangue()
        GestionStyle()
        InitialiseFenetre()

        lBuild = False
    End Sub

    Private Sub GestionLangue()

        Me.lbl_Maintiens.Text = "Maintien latéral de la poutre"

        Me.btn_Annuler.Text = "Annuler"
        Me.btn_OK.Text = "OK"

        Me.PoMbtn_Construction.Caption = "En phase de construction"
        Me.PoMbtn_Normal.Caption = "En phase normale"

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Maintiens.BackColor = CouleurBackBandeaux
        Me.lbl_Maintiens.ForeColor = CouleurForeBandeaux

        InitialiseCouleurs()
        PreparePomBouton(PoMbtn_Construction)
        PreparePomBouton(PoMbtn_Normal)

    End Sub

    Private Sub InitialiseFenetre()


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

        MyCouleurs.TextBoxEnSaisie = Me.pan_Test.BackColor
        MyCouleurs.TextBoxFixe = Color.Gray
        MyCouleurs.TextBoxEnInfo = Color.LightGray

        MyCouleurs.SaisieOK = Color.Black
        MyCouleurs.SaisieError = Color.Red

        MyCouleurs.EtqInfo = Color.LightGoldenrodYellow
        MyCouleurs.EtqInfo = Color.Gold

        MyCouleurs.Panels = Me.pan_Test.BackColor

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

#End Region

#Region "    Gestion des boutons - Paint Overrides "

    Private Sub PomBoutonsClick(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles PoMbtn_Normal.Click, PoMbtn_Construction.Click

        If Not sender.checked Then  '-> Si bouton déjà séléctionné :
            sender.checked = True       'on le garde checké
            Exit Sub                    'on ne recharge pas la fenêtre fille
        End If

        Dim SenderName As String = sender.name
        ' HideToutesLesFilles()
        UncheckedAllPomBtns(SenderName)
        Select Case SenderName
            Case Me.PoMbtn_Normal.Name
                'LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General
                'AfficherFenetreFille()

            Case Me.PoMbtn_Construction.Name
                ' LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.Directories
                'AfficherFenetreFille()



        End Select
        RedrawAllPomBtns()

        '--> Bouton checké ne change pas de couleur quand il est survolé (MouseOn)
        sender.CouleurMouseOnBtn = MyCouleurs.ColorSelectedBtn


    End Sub

    Private Sub RedrawAllPomBtns()

        For Each MyPomBtn As Object In Me.TLpan_ChoixEtat.Controls
            If MyPomBtn.Name.ToUpper.Contains("POM") Then
                '--> Initialisation de la couleur de survole (MouseOn)
                MyPomBtn.CouleurMouseOnBtn = MyCouleurs.ColorMouseOnBtn
                '--> MAJ du bouton
                MyPomBtn.Invalidate()
            End If
        Next

    End Sub

    Private Sub UncheckedAllPomBtns(ByVal SenderName As String)

        If SenderName <> Me.PoMbtn_Construction.Name Then Me.PoMbtn_Construction.Checked = False
        If SenderName <> Me.PoMbtn_Normal.Name Then Me.PoMbtn_Normal.Checked = False

        'If SenderName <> Me.PomBtnExpert.Name Then MAJBtnExpert()

    End Sub

#End Region

End Class