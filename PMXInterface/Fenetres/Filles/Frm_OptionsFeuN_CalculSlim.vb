Imports PMXMoteur2

Public Class Frm_OptionsFeuN_CalculSlim


#Region " Variables locales "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(BlocL As Dictionary(Of String, String))
        lBuild = True

        GestionStyle()
        GestionLangues(BlocL)
        GestionUnites()

        Me.pan_General.Dock = DockStyle.Fill

        AffichePoutreEnCours(Frm_OptionsFeuN.BeamLoc)

        lBuild = False
    End Sub

    Private Sub GestionStyle()

        Me.lbl_CalculOptions.BackColor = CouleurBackBandeaux
        Me.lbl_CalculOptions.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub GestionUnites()

        lbl_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        etq_UnitU.Text = "%"

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '--> chk_ReductionConcreteStrenght

            Me.chk_ReductionConcreteStrenght.Text = Bloc("CONCRETEREDUC250")

            '=== OPTIONS DE CALCUL ==============================================================='

            Me.lbl_CalculOptions.Text = Bloc("CALCULOPTIONS")



            Me.btn_Maillage.Text = "Maillage"

            Me.lbl_SizeElt.Text = Bloc("SIZEELT")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub AffichePoutreEnCours(myBeam As cls_Poutre)

    End Sub


#End Region

#Region " Evènements "

    Private Sub btn_Maillage_Click(sender As Object, e As EventArgs) Handles btn_Maillage.Click

        Frm_MaillageSlim.ShowDialog()

    End Sub


#End Region

End Class