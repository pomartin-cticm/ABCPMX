Public Class Frm_OptionsLogicielGeneral

#Region " Variables et constantes "

    Const BALISE As String = "OPTSOFTGENERAL"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        GestionStyle()
        RemplirCmbVersion()
        GestionVersions()
        'remplirTest()

        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Langues.Text = MyBloc("LANGUAGES")
            Me.lbl_LangueGUI.Text = MyBloc("USERGI")
            Me.lbl_LangueNdC.Text = MyBloc("CALCULREPORT")

            Me.lbl_Identification.Text = MyBloc("IDENTIFICATION")
            Me.lbl_Firm.Text = MyBloc("FIRM")
            Me.lbl_UserName.Text = MyBloc("USERNAME")

            Me.lbl_Version.Text = MyBloc("VERSION")
            Me.lbl_Version2.Text = MyBloc("VERSION")

            Me.lbl_Web.Text = MyBloc("WEB")
            Me.chk_ControlFichier.Text = MyBloc("WEBFILE")
            Me.chk_ControlVersion.Text = MyBloc("WEBVERSION")
            Me.cmd_CheckUpdates.Text = MyBloc("WEBUPDATE")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_General.Dock = DockStyle.Fill

        Me.lbl_Identification.BackColor = CouleurBackBandeaux
        Me.lbl_Identification.ForeColor = CouleurForeBandeaux

        Me.lbl_Langues.BackColor = CouleurBackBandeaux
        Me.lbl_Langues.ForeColor = CouleurForeBandeaux

        Me.lbl_Version.BackColor = CouleurBackBandeaux
        Me.lbl_Version.ForeColor = CouleurForeBandeaux

        ' Me.lst_LangueNdC.Items(Me.lst_LangueNdC.SelectedIndex)
    End Sub

    Private Sub GestionVersions()
        'Me.pan_Version.Visible = LogicielOptions.lExpert
        Me.pan_Version.Visible = Frm_OptionsLogiciel.pLocalLogicielOptions.lExpert
    End Sub

    Private Sub RemplirCmbVersion()

        Me.cmb_Maitre.Items.Clear()
        Me.cmb_Maitre.Items.Add("CTICM")
        Me.cmb_Maitre.Items.Add("ARCELORMITTAL")

        Select Case LogicielInfo.Maitre
            Case EnuMaitre.ArcelorMittal : Me.cmb_Maitre.SelectedIndex = 1
            Case EnuMaitre.CTICM : Me.cmb_Maitre.SelectedIndex = 0
        End Select

    End Sub

    Private Sub AfficherOptionsEnCours()

        Dim lFrenchOnly As Boolean = LogicielReglages.lFrenchOnly And (Not LogicielOptions.lExpert)
        InitialiseLangues(Me.lst_LangueGUI, LogicielInfo.ListeLangue, Frm_OptionsLogiciel.pLocalLogicielOptions.IndLangue, lFrenchOnly)
        InitialiseLangues(Me.lst_LangueNdC, LogicielInfo.ListeLangueNDC, Frm_OptionsLogiciel.pLocalLogicielOptions.IndLangueNDC)

        Me.txt_Firm.Text = Frm_OptionsLogiciel.pLocalLogicielOptions.CompanyName
        Me.txt_UserName.Text = Frm_OptionsLogiciel.pLocalLogicielOptions.UserName

    End Sub


    Private Sub InitialiseLangues(ByRef MyLst As ListBox, ByVal tabLangues As String(), IndexL As Integer,
                                  Optional lFrenchOnly As Boolean = False)

        '--> Déclarations

        Dim i As Integer
        Dim lDisplay As Boolean

        '--> Remplissage liste langue

        MyLst.Items.Clear()
        For i = 0 To tabLangues.Count - 1
            lDisplay = (Not lFrenchOnly) Or (lFrenchOnly And tabLangues(i) = FRANCAIS)
            If lDisplay Then _
                MyLst.Items.Add(tabLangues(i))
        Next i

        MyLst.SelectedIndex = IndexL
        MyLst.Refresh()

    End Sub

    'Private Sub remplirTest()

    '    Me.lstbox_Test.Items.Clear()

    '    Me.lstbox_Test.Items.Add("Auto")
    '    Me.lstbox_Test.Items.Add("Avion")
    '    Me.lstbox_Test.Items.Add("Train")

    '    Me.lstbox_Test.SelectedIndex = 0

    'End Sub

#End Region

#Region " Evènements "

    Private Sub TextChangedIdentification(sender As Object, e As EventArgs) Handles txt_UserName.TextChanged, txt_Firm.TextChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.txt_Firm.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.CompanyName = Me.txt_Firm.Text
            Case Me.txt_UserName.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.UserName = Me.txt_UserName.Text
        End Select

    End Sub

    Private Sub GestionChangeLangue(sender As Object, e As EventArgs) Handles lst_LangueNdC.SelectedIndexChanged, lst_LangueGUI.SelectedIndexChanged

        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.lst_LangueGUI.Name

                Frm_OptionsLogiciel.pLocalLogicielOptions.IndLangue = Me.lst_LangueGUI.SelectedIndex
                Frm_OptionsLogiciel.ChargeBlocsLangues()
                Frm_OptionsLogiciel.ReinitLangues()
                GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))

                Me.lst_LangueGUI.Invalidate()
            Case Me.lst_LangueNdC.Name

                Frm_OptionsLogiciel.pLocalLogicielOptions.IndLangueNDC = Me.lst_LangueNdC.SelectedIndex
                Me.lst_LangueNdC.Invalidate()
        End Select

    End Sub

    Private Sub cmb_Maitre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Maitre.SelectedIndexChanged
        Select Case Me.cmb_Maitre.SelectedIndex
            Case 0 : LogicielInfo.Maitre = EnuMaitre.CTICM
            Case 1 : LogicielInfo.Maitre = EnuMaitre.ArcelorMittal
        End Select
    End Sub

    Private Sub cmd_CheckUpdates_Click(sender As Object, e As EventArgs) Handles cmd_CheckUpdates.Click
        'Frm_UpDate.ShowDialog()
        'Frm_UpDate.Dispose()
    End Sub

    Private Sub chk_ControlVersion_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ControlVersion.CheckedChanged
        If lBuild Then Exit Sub
        Frm_OptionsLogiciel.pLocalLogicielOptions.lControlWebVersion = Me.chk_ControlVersion.Checked
    End Sub

    Private Sub chk_ControlFichier_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ControlFichier.CheckedChanged
        If lBuild Then Exit Sub
        Frm_OptionsLogiciel.pLocalLogicielOptions.lControlWebFichier = Me.chk_ControlFichier.Checked
    End Sub

#End Region

#Region " Test affichage "

    'Private Sub lstbox_Test_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstbox_Test.DrawItem

    '    Dim myFontColor As Color
    '    Dim myBackColor As Color

    '    'e.DrawBackground()

    '    If e.Index = Me.lstbox_Test.SelectedIndex Then
    '        myBackColor = Color.FromArgb(231, 62, 1)
    '        myFontColor = Color.White
    '    Else
    '        myBackColor = Me.lstbox_Test.BackColor
    '        myFontColor = Me.lstbox_Test.ForeColor
    '    End If

    '    Dim myBrush As New SolidBrush(myFontColor)
    '    Dim myBrushB As New SolidBrush(myBackColor)

    '    e.Graphics.FillRectangle(myBrushB, e.Bounds)

    '    e.Graphics.DrawString(lstbox_Test.Items(e.Index).ToString(),
    '                          e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)

    '    e.DrawFocusRectangle()

    'End Sub

    'Private Sub lstbox_Test_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstbox_Test.SelectedIndexChanged
    '    Me.lstbox_Test.Invalidate()
    'End Sub

    'Private Sub GestionDrawItem(myLstBox As ListBox, e As DrawItemEventArgs)
    '    Dim myFontColor As Color
    '    Dim myBackColor As Color

    '    'e.DrawBackground()

    '    If e.Index = myLstBox.SelectedIndex Then
    '        myBackColor = Color.FromArgb(231, 62, 1)
    '        myFontColor = Color.White
    '    Else
    '        myBackColor = myLstBox.BackColor
    '        myFontColor = myLstBox.ForeColor
    '    End If

    '    Dim myBrush As New SolidBrush(myFontColor)
    '    Dim myBrushB As New SolidBrush(myBackColor)

    '    e.Graphics.FillRectangle(myBrushB, e.Bounds)

    '    e.Graphics.DrawString(myLstBox.Items(e.Index).ToString(),
    '                          e.Font, myBrush, e.Bounds, StringFormat.GenericDefault)

    '    e.DrawFocusRectangle()
    'End Sub

    'Private Sub lst_LangueNdC_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lst_LangueNdC.DrawItem
    '    GestionDrawItem(Me.lst_LangueNdC, e)
    'End Sub

    'Private Sub lst_LangueGUI_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lst_LangueGUI.DrawItem
    '    GestionDrawItem(Me.lst_LangueGUI, e)
    'End Sub
#End Region

#Region " Tests "

    'Private Sub BranchListBox_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lst_LangueGUI.DrawItem

    '    'exit this event in case there is not valid index.
    '    If e.Index = -1 Then
    '        Exit Sub
    '    End If

    '    Dim mybrush As New System.Drawing.SolidBrush(Color.FromArgb(0, 177, 89))
    '    mybrush.Color = Color.FromArgb(0, 177, 89)

    '    Try
    '        e.DrawBackground()

    '        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
    '            e.Graphics.FillRectangle(mybrush, e.Bounds)
    '        End If

    '        Using b As New SolidBrush(e.ForeColor)
    '            e.Graphics.DrawString(lst_LangueGUI.GetItemText(lst_LangueGUI.Items(e.Index)), e.Font, b, e.Bounds)
    '        End Using

    '        e.DrawFocusRectangle()
    '    Catch ex As Exception
    '        'ColorAppend(LogsBox, Color.Red, TimeOfDay.ToString("h:mm:ss") & SystemLog & ex.Message & Environment.NewLine)
    '        'LogsBox.ScrollToCaret()
    '    End Try
    'End Sub


#End Region



End Class