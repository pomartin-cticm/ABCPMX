Public Class Frm_OptionsLogicielDataBases

#Region " Variables "

    Dim lBuild As Boolean
    Const BALISE As String = "OPTSOFTDATABASES"

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True

        GestionStyle()
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))

        lBuild = False
    End Sub


    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_DataBases.Text = MyBloc("DATABASES")


        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_DataBases.Dock = DockStyle.Fill

        Me.lbl_DataBases.BackColor = CouleurBackBandeaux
        Me.lbl_DataBases.ForeColor = CouleurForeBandeaux


    End Sub

#End Region



End Class