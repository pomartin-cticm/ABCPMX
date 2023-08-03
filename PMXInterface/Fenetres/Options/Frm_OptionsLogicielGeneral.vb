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
        'GestionUnites()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try



        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_General.Dock = DockStyle.Fill

        Me.lbl_Loads.BackColor = CouleurBackBandeaux
        Me.lbl_Loads.ForeColor = CouleurForeBandeaux

        Me.lbl_Combination.BackColor = CouleurBackBandeaux
        Me.lbl_Combination.ForeColor = CouleurForeBandeaux

        Me.lbl_Materials.BackColor = CouleurBackBandeaux
        Me.lbl_Materials.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherOptionsEnCours()


    End Sub

#End Region


End Class