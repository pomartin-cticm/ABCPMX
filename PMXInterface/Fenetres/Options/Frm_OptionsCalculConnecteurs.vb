Public Class Frm_OptionsCalculConnecteurs


#Region " Variables locales "


    Const BALISE As String = "OPTCALCONNECTORS"

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiserFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherValeurs()
        lBuild = False
    End Sub

    Private Sub GestionStyle()

        Me.pan_General.Dock = DockStyle.Fill

        Me.lbl_Connecteurs.BackColor = CouleurBackBandeaux
        Me.lbl_Connecteurs.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_DMin, False)
        PrepareTextBoxDipo(Me.txt_DMax1, False)

    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try
            Me.lbl_Connecteurs.Text = MyBloc("TITLE")

            Me.lbl_Dimensions.Text = MyBloc("DIMENSIONS")
            Me.lbl_DiameterMin.Text = MyBloc("DIAMIN")
            Me.lbl_DiameterMax.Text = MyBloc("DIAMAX")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub AfficherValeurs()

    End Sub

#End Region




End Class