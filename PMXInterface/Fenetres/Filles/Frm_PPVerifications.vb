Public Class Frm_PPVerifications


#Region " Variables "

    Dim lBuild As Boolean

#End Region


#Region "===OUVERTURE==="
    Private Sub Frm_PPVerifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()

        lBuild = False
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Verification.BackColor = CouleurBackBandeaux
        Me.lbl_Verification.ForeColor = CouleurForeBandeaux

        Me.img_Verifications.Dock = DockStyle.Fill
    End Sub

    Private Sub GestionLangues()



    End Sub

#End Region

End Class