Public Class Frm_InfoLogiciel

#Region " Variables "



#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GestionLangues()
        GestionStyle
        PrepareFenetre()
    End Sub

    Private Sub GestionStyle()

        Me.lbl_InfoW.BackColor = Me.pan_General.BackColor
        Me.lbl_InfoW.ForeColor = CouleurForeBandeaux
        Me.rtb_Info.BorderStyle = BorderStyle.None

        If InfoW.linfo Then
            Me.img_info.Image = imgList_Info.Images("Info")
        Else
            Me.img_info.Image = imgList_Info.Images("Warning")
        End If

    End Sub

    Private Sub GestionLangues()

        If InfoW.linfo Then
            Me.lbl_InfoW.Text = LogicielInfo.NomLogiciel & " - " & InfoW.BlocF("INFO")
        Else
            Me.lbl_InfoW.Text = LogicielInfo.NomLogiciel & " - " & InfoW.BlocF("WARNING")
        End If

        Me.lbl_ContactSupport.Text = InfoW.BlocF("CONTACT")

    End Sub

    Private Sub PrepareFenetre()

        '== Affichage des informations du logiciel ==

        Me.rtb_Info.Text = String.Join(Environment.NewLine, InfoW.InfoW_msg)

        Me.lbk_Support.Text = LogicielInfo.MailSupport
        Me.lbk_Support.Left = Me.lbl_ContactSupport.Left + Me.lbl_ContactSupport.Width + 3

    End Sub

#End Region

#Region "===FERMETURE==="
    Private Sub Frm_InfoLogiciel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

    End Sub

    Private Sub Fermer()
        Me.Close()
    End Sub

    Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
        Fermer()
    End Sub

    'Private Sub rtb_Info_MouseMove(sender As Object, e As MouseEventArgs) Handles rtb_Info.MouseMove
    '    Fermer()
    'End Sub

    Private Sub img_Close_Click(sender As Object, e As EventArgs) Handles img_Close.Click
        Fermer()
    End Sub

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