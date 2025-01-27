Imports PMXMoteur2

Public Class Frm_MaintienBacN_Fixation


#Region " Variables "

    Dim lBuild As Boolean
    Dim strTransitionOptions(2) As String

    Dim EntraxeD As Decimal

    Dim strFastening(1) As String
    Dim strTypeFixation(2) As String
    Dim strDiametreFixation(2) As String
    Dim strSlipFixation(2) As String

    Dim strTypeCouturage(1) As String
    Dim strDiametreCouturage(1) As String
    Dim strSlipCouturage(1) As String

    Const kUnitSlip As Decimal = 10 ^ 6

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()
        PrepareFenetre()
        AffichePoutreEnCours()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Couturage.BackColor = CouleurBackBandeaux
        Me.lbl_Couturage.ForeColor = CouleurForeBandeaux
        Me.lbl_FixationSolive.BackColor = CouleurBackBandeaux
        Me.lbl_FixationSolive.ForeColor = CouleurForeBandeaux


    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitL5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '=== FIXATIONS AUX POUTRES ========================================================

            Me.lbl_FixationSolive.Text = Bloc("BEAMFASTENING")
            Me.lbl_Fixation.Text = Bloc("FASTENING")
            strFastening(0) = Bloc("EVERYRIB")
            strFastening(1) = Bloc("EVERY2RIBS")

            Me.lbl_TypeFixation.Text = Bloc("TYPEFASTENER")
            strTypeFixation(0) = Bloc("SELFSCREWNORMAL")
            strTypeFixation(1) = Bloc("SELFSCREWNEOPRENE")
            strTypeFixation(2) = Bloc("FIREDPINS")
            strDiametreFixation(0) = Bloc("DIASCREWS")
            strDiametreFixation(1) = Bloc("DIASCREWS")
            strDiametreFixation(2) = Bloc("DIAPINS")

            Me.lbl_Diametre.Text = Bloc("DIAMETER") & ": "
            Me.lbl_Glissement.Text = Bloc("SLIP")

            strSlipFixation(0) = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.FixNervuresSlip(cls_MaintienBac.Enu_FixNervuresType.VisNormale) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, NON_U, False) & " mm/kN"
            strSlipFixation(1) = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.FixNervuresSlip(cls_MaintienBac.Enu_FixNervuresType.VisNeoprene) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, NON_U, False) & " mm/kN"
            strSlipFixation(2) = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.FixNervuresSlip(cls_MaintienBac.Enu_FixNervuresType.Pistolet) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, NON_U, False) & " mm/kN"

            '=== COUTURAGE =====================================================================

            Me.lbl_Couturage.Text = Bloc("SEAMFASTENING")
            Me.lbl_TypeCouturage.Text = Bloc("TYPEFASTENER")

            Me.lbl_DiaS.Text = Bloc("DIAMETER") & ": "
            Me.lbl_GlisseS.Text = Bloc("SLIP") & ": "

            strTypeCouturage(0) = Bloc("SELFSCREW")
            strTypeCouturage(1) = Bloc("RIVET")

            strDiametreCouturage(0) = Bloc("DIASCREWS_SEAM")
            strDiametreCouturage(1) = Bloc("DIAPINS_SEAM")


            strSlipCouturage(0) = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.FixCoutureSlip(cls_MaintienBac.Enu_CoutureType.Vis) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, NON_U, False) & " mm/kN"
            strSlipCouturage(1) = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.FixCoutureSlip(cls_MaintienBac.Enu_CoutureType.Rivet) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, NON_U, False) & " mm/kN"

            Me.lbl_EspCouturage.Text = Bloc("SPACING")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally

        End Try

    End Sub

    Private Sub RemplirCmbFixationPoutre()
        Me.cmb_FixationPoutre.Items.Clear()
        Me.cmb_FixationPoutre.Items.AddRange(strFastening)
    End Sub

    Private Sub RemplirCmbTypeFixation()
        Me.cmb_TypeFixation.Items.Clear()
        Me.cmb_TypeFixation.Items.AddRange(strTypeFixation)

        Me.cmb_TypSeamFastener.Items.Clear()
        Me.cmb_TypSeamFastener.Items.AddRange(strTypeCouturage)
    End Sub

    Private Sub PrepareFenetre()
        EntraxeD = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeSolive

        RemplirCmbFixationPoutre()
        RemplirCmbTypeFixation()
    End Sub

    Private Sub AffichePoutreEnCours()

        Select Case Frm_MaintienBacN.localMaitienBac.FixNervuresMod
            Case cls_MaintienBac.Enu_FixationNervures.Toutes : Me.cmb_FixationPoutre.SelectedIndex = 0
            Case cls_MaintienBac.Enu_FixationNervures.UneSurDeux : Me.cmb_FixationPoutre.SelectedIndex = 0
        End Select

        Select Case Frm_MaintienBacN.localMaitienBac.FixnervuresTyp
            Case cls_MaintienBac.Enu_FixNervuresType.VisNormale : Me.cmb_TypeFixation.SelectedIndex = 0
            Case cls_MaintienBac.Enu_FixNervuresType.VisNeoprene : Me.cmb_TypeFixation.SelectedIndex = 1
            Case cls_MaintienBac.Enu_FixNervuresType.Pistolet : Me.cmb_TypeFixation.SelectedIndex = 2
        End Select

        Select Case Frm_MaintienBacN.localMaitienBac.FixCoutureType
            Case cls_MaintienBac.Enu_CoutureType.Vis : Me.cmb_TypSeamFastener.SelectedIndex = 0
            Case cls_MaintienBac.Enu_CoutureType.Rivet : Me.cmb_TypSeamFastener.SelectedIndex = 1
        End Select

        Me.txt_EspCouturage.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.ec, Enu_TypeVariable.Dimension, 3, 1, NON_U, False)

        MAJI_InfoFixations()
        MAJI_InfoCouturage()

    End Sub

#End Region

#Region " Evènements "


    Private Sub cmb_FixationPoutre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_FixationPoutre.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_FixationPoutre.SelectedIndex

            Case 0 : Frm_MaintienBacN.localMaitienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes
            Case 1 : Frm_MaintienBacN.localMaitienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.UneSurDeux

        End Select

        Frm_MaintienBacN.MAJI_Calculs()

    End Sub

    Private Sub cmb_TypeFixation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeFixation.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeFixation.SelectedIndex
            Case 0 : Frm_MaintienBacN.localMaitienBac.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.VisNormale
            Case 1 : Frm_MaintienBacN.localMaitienBac.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.VisNeoprene
            Case 2 : Frm_MaintienBacN.localMaitienBac.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.Pistolet
        End Select

        MAJI_InfoFixations()
        Frm_MaintienBacN.MAJI_Calculs()
    End Sub

    Private Sub cmb_TypSeamFastener_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypSeamFastener.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypSeamFastener.SelectedIndex
            Case 0 : Frm_MaintienBacN.localMaitienBac.FixCoutureType = cls_MaintienBac.Enu_CoutureType.Vis
            Case 1 : Frm_MaintienBacN.localMaitienBac.FixCoutureType = cls_MaintienBac.Enu_CoutureType.Rivet
        End Select

        MAJI_InfoCouturage()
        Frm_MaintienBacN.MAJI_Calculs()

    End Sub

    Private Sub txt_EspCouturage_TextChanged(sender As Object, e As EventArgs) Handles txt_EspCouturage.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.ec = (Valeur)
            Frm_MaintienBacN.MAJI_Calculs()
        End If

    End Sub

    Private Sub MAJI_InfoFixations()

        Me.lbl_DiametreInfo.Text = strDiametreFixation(Me.cmb_TypeFixation.SelectedIndex)
        Me.lbl_SlipInfo.Text = strSlipFixation(Me.cmb_TypeFixation.SelectedIndex)

    End Sub

    Private Sub MAJI_InfoCouturage()

        Me.lbl_DiaS_Info.Text = strDiametreCouturage(Me.cmb_TypSeamFastener.SelectedIndex)
        Me.lbl_GlisseS_Info.Text = strSlipCouturage(Me.cmb_TypSeamFastener.SelectedIndex)

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean
        '----------------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------
        '   Vérification de la saisie des textbox
        '----------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)


        Select Case MyTxt.Name

            Case Me.txt_EspCouturage.Name

                ValMin = 0.02 / kUnit
                ValMax = OptionsScope.MB_spCoutureMax / kUnit

                lValMax = True

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

#End Region

End Class