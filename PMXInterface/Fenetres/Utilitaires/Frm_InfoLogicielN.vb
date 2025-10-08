Public Class Frm_InfoLogicielN

#Region " Variables "

    Dim CouleurBase As Color

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GestionLangues()
        GestionStyle()
        PrepareFenetre()
    End Sub

    Private Sub GestionStyle()

        CouleurBase = InfoW.CouleurBase
        Me.lbl_InfoW.BackColor = CouleurBase
        '  Me.pan_General.BackColor = CouleurBase
        Me.lbl_InfoW.ForeColor = CouleurForeBandeaux
        Me.rtb_Info.BorderStyle = BorderStyle.None

        'If InfoW.linfo Then
        '    Me.img_info.Image = imgList_Info.Images("Info")
        'Else
        '    Me.img_info.Image = imgList_Info.Images("Warning")
        'End If

        If InfoW.Mode = Cls_InfoW.enu_ModeW.Information Then
            Me.TLPan_General.RowStyles(2).Height = 0
        End If

    End Sub

    Private Sub GestionLangues()

        Dim Cle As String = "INFO"

        Select Case InfoW.Mode
            Case Cls_InfoW.enu_ModeW.Information : Cle = "INFO"
            Case Cls_InfoW.enu_ModeW.Erreur : Cle = "ERROR"
            Case Cls_InfoW.enu_ModeW.Avertissement : Cle = "WARNING"
        End Select

        Me.lbl_InfoW.Text = LogicielInfo.NomLogiciel & " - " & InfoW.BlocF(Cle)

        Me.lbl_ContactSupport.Text = InfoW.BlocF("CONTACT")

    End Sub

    Private Sub PrepareFenetre()

        '== Affichage des informations du logiciel ==

        Me.rtb_Info.Text = String.Join(Environment.NewLine, InfoW.InfoW_msg)

        Me.lbk_Support.Text = LogicielInfo.MailSupport
        Me.lbk_Support.Left = Me.lbl_ContactSupport.Left + Me.lbl_ContactSupport.Width + 3

        Me.Icon = Frm_PMX.Icon
        Me.Text = LogicielInfo.NomLogiciel

    End Sub

#End Region

#Region "===FERMETURE==="
    Private Sub Frm_InfoLogiciel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

    End Sub

    Private Sub Fermer()
        Me.Close()
    End Sub

    'Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
    '    Fermer()
    'End Sub

    ''Private Sub rtb_Info_MouseMove(sender As Object, e As MouseEventArgs) Handles rtb_Info.MouseMove
    ''    Fermer()
    ''End Sub

    'Private Sub img_Close_Click(sender As Object, e As EventArgs) Handles img_Close.Click
    '    Fermer()
    'End Sub

    Private Sub lbl_InfoW_Click(sender As Object, e As EventArgs) Handles lbl_InfoW.Click
        Fermer()
    End Sub

#End Region

#Region " Evènements "
    Private Sub lbk_Support_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbk_Support.LinkClicked
        Dim myDest As String = LogicielInfo.MailSupport

        Try

            PrepareMailSupport(myDest)

        Catch ex As Exception
            MsgBox("Erreur d'ouverture mail | Error opening mail", MsgBoxStyle.Critical, "Frm_About/LinkSupport_LinkClicked")
        End Try
    End Sub


#End Region

End Class