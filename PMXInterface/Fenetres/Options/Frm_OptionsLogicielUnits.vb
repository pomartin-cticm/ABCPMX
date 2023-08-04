Public Class Frm_OptionsLogicielUnits


#Region " Variables et constantes "

    Const BALISE As String = "OPTSOFTUNITS"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        GestionStyle()
        'GestionUnites()
        IntialiseObjets()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Units.Text = MyBloc("UNITS")

            Me.lbl_Dimensions.Text = MyBloc("DIMENSIONS")
            Me.lbl_Longueur.Text = MyBloc("LENGTHS")
            Me.lbl_Contraintes.Text = MyBloc("STRESSES")
            Me.lbl_Inertie.Text = MyBloc("SMAREA")
            Me.lbl_Moments.Text = MyBloc("MOMENTS")
            Me.lbl_Forces.Text = MyBloc("FORCES")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Units.Dock = DockStyle.Fill

        Me.lbl_Units.BackColor = CouleurBackBandeaux
        Me.lbl_Units.ForeColor = CouleurForeBandeaux


    End Sub

    Private Sub IntialiseObjets()
        RemplirComboAvecTableau(Me.cmb_Dimensions, LogicielInfo.Unit_Longueur)
        RemplirComboAvecTableau(Me.cmb_Longueur, LogicielInfo.Unit_Longueur)
        RemplirComboAvecTableau(Me.cmb_Contraintes, LogicielInfo.Unit_Contraintes)
        RemplirComboAvecTableau(Me.cmb_Forces, LogicielInfo.Unit_Effort)
        RemplirComboAvecTableau(Me.cmb_Inertie, LogicielInfo.Unit_Inerties)
        'RemplirComboAvecTableau(Me.cmb_ModuleW, LogicielInfo.Unit_ModulesY)
        RemplirComboAvecTableau(Me.cmb_Moments, LogicielInfo.Unit_Moment)

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)
        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)
    End Sub


    Private Sub AfficherOptionsEnCours()

        Me.cmb_Dimensions.SelectedIndex = Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitDimension
        Me.cmb_Longueur.SelectedIndex = Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitLongueur
        Me.cmb_Contraintes.SelectedIndex = Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitContraintes
        Me.cmb_Forces.SelectedIndex = Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitEffort
        Me.cmb_Inertie.SelectedIndex = Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitInerties
        Me.cmb_Moments.SelectedIndex = Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitMoment

    End Sub


#End Region

#Region " Evènements saisie "

    Private Sub ChangeUnites(sender As Object, e As EventArgs) Handles cmb_Longueur.SelectedIndexChanged, cmb_Dimensions.SelectedIndexChanged, cmb_Moments.SelectedIndexChanged, cmb_ModuleW.SelectedIndexChanged, cmb_Inertie.SelectedIndexChanged, cmb_Forces.SelectedIndexChanged, cmb_Contraintes.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.cmb_Contraintes.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitContraintes = Me.cmb_Contraintes.SelectedIndex

            Case Me.cmb_Dimensions.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitDimension = Me.cmb_Dimensions.SelectedIndex

            Case Me.cmb_Forces.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitEffort = Me.cmb_Forces.SelectedIndex

            Case Me.cmb_Inertie.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitInerties = Me.cmb_Inertie.SelectedIndex

            Case Me.cmb_Longueur.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitLongueur = Me.cmb_Longueur.SelectedIndex

            Case Me.cmb_Moments.Name
                Frm_OptionsLogiciel.pLocalLogicielOptions.IndUnitMoment = Me.cmb_Moments.SelectedIndex

        End Select

    End Sub

#End Region

End Class