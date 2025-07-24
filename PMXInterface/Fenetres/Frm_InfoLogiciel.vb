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

        Me.lbl_InfoW.BackColor = CouleurBackBandeaux
        Me.lbl_InfoW.ForeColor = CouleurForeBandeaux
        Me.rtb_Info.BorderStyle = BorderStyle.None

    End Sub

    Private Sub GestionLangues()

        Me.lbl_InfoW.Text = str_InformationW

    End Sub

    Private Sub PrepareFenetre()


        '== Affichage des informations du logiciel ==

        Me.rtb_Info.Text = String.Join(Environment.NewLine, InfoW_msg)

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

    Private Sub rtb_Info_MouseMove(sender As Object, e As MouseEventArgs) Handles rtb_Info.MouseMove
        Fermer()
    End Sub

#End Region


End Class