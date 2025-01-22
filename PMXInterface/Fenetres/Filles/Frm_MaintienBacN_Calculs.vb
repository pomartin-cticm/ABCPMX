Public Class Frm_MaintienBacN_Calculs

#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()
        'PrepareFenetre()
        'AffichePoutreEnCours()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Calculs.BackColor = CouleurBackBandeaux
        Me.lbl_Calculs.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_Alpha5, False)
        PrepareTextBoxDipo(Me.txt_c, False)
        PrepareTextBoxDipo(Me.txt_c11, False)
        PrepareTextBoxDipo(Me.txt_c12, False)
        PrepareTextBoxDipo(Me.txt_c21, False)
        PrepareTextBoxDipo(Me.txt_c22, False)
        PrepareTextBoxDipo(Me.txt_K, False)
        PrepareTextBoxDipo(Me.txt_kTheta, False)
        PrepareTextBoxDipo(Me.txt_kThetaA, False)
        PrepareTextBoxDipo(Me.txt_kThetaC, False)
        PrepareTextBoxDipo(Me.txt_Sact, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitSact.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitC11.Text = "mm/kN"
        Me.etq_Unitc12.Text = "mm/kN"
        Me.etq_Unitc21.Text = "mm/kN"
        Me.etq_Unitc22.Text = "mm/kN"

        Me.etq_UnitK.Text = ""
        Me.etq_UnitAlpha5.Text = ""

        Me.etq_UnitkTheta.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "." & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitkThetaA.Text = Me.etq_UnitkTheta.Text
        Me.etq_UnitkThetaC.Text = Me.etq_UnitkTheta.Text

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '=== CALCULS =======================================================================

            Me.lbl_Calculs.Text = Bloc("PARAMETERS")
            Me.lbl_BendingRigidity.Text = Bloc("BENDINGSTIFF")
            Me.chk_Theta.Text = Bloc("THETA")
            Me.lbl_ShearRigidity.Text = Bloc("SHEARSTIFF")

            'strResultats = Bloc("PARAMETERS")
            'strDessin = Bloc("DRAWING")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally

        End Try

    End Sub


#End Region


End Class