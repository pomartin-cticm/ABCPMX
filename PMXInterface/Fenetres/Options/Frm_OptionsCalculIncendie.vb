Public Class Frm_OptionsCalculIncendie

#Region " Attributs "

    Dim lBuild As Boolean
    Const BALISE As String = "OPTCALFIRE"


#End Region

#Region "===Ouverture==="

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Incendie.Text = MyBloc("TITLE")

            Me.lbl_Constantes.Text = MyBloc("CONSTANTS")


        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Incendie.Dock = DockStyle.Fill

        Me.lbl_Incendie.BackColor = CouleurBackBandeaux
        Me.lbl_Incendie.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitEs.Text = LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

    Private Sub AfficherOptionsEnCours()

    End Sub

#End Region

End Class