Public Class Frm_OptionsLogicielNdC


#Region " Variables "

    Dim lBuild As Boolean
    Const BALISE As String = "OPTSOFTCALCULSHEET"

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True

        GestionStyle()
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        PrepareFenetre()
        AfficheOptionsEnCours()

        lBuild = False


    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Sollicitations.Text = MyBloc("FORCESANDMOMENTS")
            Me.lbl_ELS.Text = MyBloc("SLS")
            Me.lbl_Hivoss.Text = MyBloc("HIVOSS")
            Me.chk_ShowHivossDiagram.Text = MyBloc("SHOWHIVOSSCURVE")


        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_NdC.Dock = DockStyle.Fill



        Me.lbl_Sollicitations.BackColor = CouleurBackBandeaux
        Me.lbl_Sollicitations.ForeColor = CouleurForeBandeaux

        Me.lbl_ELS.BackColor = CouleurBackBandeaux
        Me.lbl_ELS.ForeColor = CouleurForeBandeaux
    End Sub

    Private Sub PrepareFenetre()

    End Sub

    Private Sub AfficheOptionsEnCours()

        Me.chk_ShowHivossDiagram.Checked = Frm_OptionsLogiciel.pLocalOptionsNdC.lShowHivossCurve

    End Sub

#End Region

#Region " Evènements "

    Private Sub chk_ShowHivossDiagram_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ShowHivossDiagram.CheckedChanged
        Frm_OptionsLogiciel.pLocalOptionsNdC.lShowHivossCurve = Me.chk_ShowHivossDiagram.Checked
    End Sub

#End Region


End Class