Imports System.Drawing.Drawing2D
Imports PMXMoteur2

Public Class Frm_MaintienBac

#Region " Variables "

    Dim lBuild As Boolean

    Dim localMaitienBac As New cls_MaintienBac
    Dim EntraxeD As Decimal                     ' Entraxe entre les solives pour les calculs
    Dim strTransitionOptions(2) As String
    Dim strFastening(1) As String
    Dim strTypeFixation(2) As String
    Dim strDiametreFixation(2) As String
    Dim strSlipFixation(2) As String

    Dim strTypeCouturage(1) As String
    Dim strDiametreCouturage(1) As String
    Dim strSlipCouturage(1) As String

    Const kUnitSlip As Decimal = 10 ^ 6

    Enum Enu_AffichageD
        Valeurs
        Graphique
    End Enum

    Dim AfficheD As Enu_AffichageD = Enu_AffichageD.Valeurs

    Const kMarge As Single = 0.12
    Dim xMargeZone As Single
    Dim SizeZone As Single

    Dim lMouseG As Boolean = False
    Dim lMouseD As Boolean = False

    Dim strResultats As String
    Dim strDessin As String

#End Region


#Region "===OUVERTURE==="

    Private Sub Frm_MaintienBac_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()
        GestionUnites()

        InitialisationFenetre()
        AffichePoutreEnCours()

        lBuild = False
    End Sub

    Private Sub InitialisationFenetre()

        RemplirCmbNbSpans()
        RemplirCmbTransition()
        RemplirCmbFixationPoutre()
        RemplirCmbTypeFixation()

        localMaitienBac = MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Clone

        ' EntraxeD = Me.EntraxeSolive(MyProjet.Poutres(MyProjet.IndEnCours))
        EntraxeD = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeSolive

        xMargeZone = Me.lbl_Calculs.ClientRectangle.Height * kMarge
        SizeZone = Me.lbl_Calculs.ClientRectangle.Height * (1 - 2 * kMarge)

        MAJI_NaviLableCalculs()

        Me.img_Deck.Top = Me.pan_RigiditeShear.Top
        Me.img_Deck.Width = Me.pan_Rigidite.Width
        Me.img_Deck.Height = Me.pan_Rigidite.Height + Me.pan_RigiditeShear.Height

    End Sub

    'Private Function EntraxeSolive(MyBeam As cls_Poutre) As Decimal

    '    Dim MyD As Decimal

    '    If MyBeam.lIntermediaire Then
    '        MyD = (MyBeam.EntraxeD1 + MyBeam.EntraxeD2) / 2
    '    Else
    '        MyD = MyBeam.EntraxeD2
    '    End If

    '    Return MyD

    'End Function

    Private Sub RemplirCmbNbSpans()
        Me.cmb_NbSpan.Items.Clear()
        For i As Integer = 1 To 5
            Me.cmb_NbSpan.Items.Add(CStr(i))
        Next
    End Sub

    Private Sub RemplirCmbTransition()

        Me.cmb_Transition.Items.Clear()

        Me.cmb_Transition.Items.AddRange(strTransitionOptions)

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

    Private Sub GestionLangues()

        Dim Bloc As New Dictionary(Of String, String)
        Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_DECKRESTRAINT")
        BlocLine.CreationBloc(Bloc)

        Try

            '=== GENERAL ======================================================================

            Me.Text = Bloc("TITLE")
            Me.btn_OK.Text = Bloc("OK")
            Me.btn_Annuler.Text = Bloc("CANCEL")

            '=== PLANCHER ===============================================================

            Me.chk_PriseEnCompteBac.Text = Bloc("RESTRAINTBYTHEDECK")
            Me.lbl_Floor.Text = Bloc("FLOORDEF")
            Me.lbl_NbSheetsTransverse.Text = Bloc("NBSHEETSTRANSVERSE")

            Me.lbl_DimensionsGlobales.Text = Bloc("FLOORDIMENSIONS")
            Me.lbl_Portee.Text = Bloc("FLOORLENGTH")
            Me.lbl_Largeur.Text = Bloc("FLOORWIDTH")

            Me.lbl_Transition.Text = Bloc("TRANSITION")
            strTransitionOptions(0) = Bloc("OVERLAPPING")
            strTransitionOptions(1) = Bloc("ADJACENTNOGAP")
            strTransitionOptions(2) = Bloc("FLANGEEDGES")


            '=== PANNEAU ELEMENTAIRE ==========================================================

            Me.lbl_Panneau.Text = Bloc("INDIVIDUALSHEET")
            Me.lbl_NbSpans.Text = Bloc("NUMBERSPANS")
            Me.lbl_IndSheetDimensions.Text = Bloc("DIMENSIONS")
            Me.lbl_SheetLength.Text = Bloc("SHEETLENGTH")
            Me.lbl_SheetWidth.Text = Bloc("SHEETWIDTH")

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

            strSlipFixation(0) = GetStringInUnit(localMaitienBac.FixNervuresSlip(cls_MaintienBac.Enu_FixNervuresType.VisNormale) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & " mm/kN"
            strSlipFixation(1) = GetStringInUnit(localMaitienBac.FixNervuresSlip(cls_MaintienBac.Enu_FixNervuresType.VisNeoprene) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & " mm/kN"
            strSlipFixation(2) = GetStringInUnit(localMaitienBac.FixNervuresSlip(cls_MaintienBac.Enu_FixNervuresType.Pistolet) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & " mm/kN"

            '=== COUTURAGE =====================================================================

            Me.lbl_Couturage.Text = Bloc("SEAMFASTENING")
            Me.lbl_TypeCouturage.Text = Bloc("TYPEFASTENER")

            Me.lbl_DiaS.Text = Bloc("DIAMETER") & ": "
            Me.lbl_GlisseS.Text = Bloc("SLIP") & ": "

            strTypeCouturage(0) = Bloc("SELFSCREW")
            strTypeCouturage(1) = Bloc("RIVET")

            strDiametreCouturage(0) = Bloc("DIASCREWS_SEAM")
            strDiametreCouturage(1) = Bloc("DIAPINS_SEAM")

            strSlipCouturage(0) = GetStringInUnit(localMaitienBac.FixCoutureSlip(cls_MaintienBac.Enu_CoutureType.Vis) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & " mm/kN"
            strSlipCouturage(1) = GetStringInUnit(localMaitienBac.FixCoutureSlip(cls_MaintienBac.Enu_CoutureType.Rivet) * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & " mm/kN"

            Me.lbl_EspCouturage.Text = Bloc("SPACING")

            '=== CALCULS =======================================================================

            Me.lbl_Calculs.Text = Bloc("PARAMETERS")
            Me.lbl_BendingRigidity.Text = Bloc("BENDINGSTIFF")
            Me.chk_Theta.Text = Bloc("THETA")
            Me.lbl_ShearRigidity.Text = Bloc("SHEARSTIFF")

            strResultats = Bloc("PARAMETERS")
            strDessin = Bloc("DRAWING")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
            Bloc.Clear()
        End Try

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Floor.BackColor = CouleurBackBandeaux
        Me.lbl_Floor.ForeColor = CouleurForeBandeaux

        Me.lbl_Panneau.BackColor = CouleurBackBandeaux
        Me.lbl_Panneau.ForeColor = CouleurForeBandeaux

        Me.lbl_Couturage.BackColor = CouleurBackBandeaux
        Me.lbl_Couturage.ForeColor = CouleurForeBandeaux
        Me.lbl_FixationSolive.BackColor = CouleurBackBandeaux
        Me.lbl_FixationSolive.ForeColor = CouleurForeBandeaux

        Me.lbl_Calculs.BackColor = CouleurBackBandeaux
        Me.lbl_Calculs.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_LargeurP, False)
        PrepareTextBoxDipo(Me.txt_LongueurP, False)
        PrepareTextBoxDipo(Me.txt_SheetLength, False)
        PrepareTextBoxDipo(Me.txt_SheetWidth, False)

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

        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
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

    Private Sub AffichePoutreEnCours()

        Me.chk_PriseEnCompteBac.Checked = localMaitienBac.lMaintienBac
        Me.chk_Theta.Checked = localMaitienBac.lTheta

        Me.cmb_NbSpan.SelectedIndex = localMaitienBac.m - 1
        Select Case localMaitienBac.Transition
            Case cls_MaintienBac.Enu_Transition.Emboitement : Me.cmb_Transition.SelectedIndex = 0
            Case cls_MaintienBac.Enu_Transition.Aboutage : Me.cmb_Transition.SelectedIndex = 1
            Case cls_MaintienBac.Enu_Transition.Adistance : Me.cmb_Transition.SelectedIndex = 2
        End Select

        Me.txt_NbSheetsTransverse.Text = GetStringInUnit(localMaitienBac.nt, Enu_TypeVariable.SansType, 2, 0, False)

        Select Case localMaitienBac.FixNervuresMod
            Case cls_MaintienBac.Enu_FixationNervures.Toutes : Me.cmb_FixationPoutre.SelectedIndex = 0
            Case cls_MaintienBac.Enu_FixationNervures.UneSurDeux : Me.cmb_FixationPoutre.SelectedIndex = 0
        End Select

        Select Case localMaitienBac.FixnervuresTyp
            Case cls_MaintienBac.Enu_FixNervuresType.VisNormale : Me.cmb_TypeFixation.SelectedIndex = 0
            Case cls_MaintienBac.Enu_FixNervuresType.VisNeoprene : Me.cmb_TypeFixation.SelectedIndex = 1
            Case cls_MaintienBac.Enu_FixNervuresType.Pistolet : Me.cmb_TypeFixation.SelectedIndex = 2
        End Select

        Select Case localMaitienBac.FixCoutureType
            Case cls_MaintienBac.Enu_CoutureType.Vis : Me.cmb_TypSeamFastener.SelectedIndex = 0
            Case cls_MaintienBac.Enu_CoutureType.Rivet : Me.cmb_TypSeamFastener.SelectedIndex = 1
        End Select

        Me.txt_EspCouturage.Text = GetStringInUnit(localMaitienBac.ec, Enu_TypeVariable.Dimension, 3, 1, False)

        MAJI_DimensionsPlancher()
        MAJI_DimensionsPanneau()
        MAJI_InfoFixations()
        MAJI_InfoCouturage()
        MAJI_Calculs()

    End Sub

#End Region


#Region " Fermeture "

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMslab)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        GereTransfertValeur(localMaitienBac.m, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.m, lModif)
        GereTransfertValeur(localMaitienBac.nt, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.nt, lModif)
        GereTransfertValeur(localMaitienBac.ec, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.ec, lModif)
        GereTransfertValeur(localMaitienBac.lMaintienBac, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.lMaintienBac, lModif)
        GereTransfertValeur(localMaitienBac.lTheta, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.lTheta, lModif)

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition <> localMaitienBac.Transition Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition = localMaitienBac.Transition

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod <> localMaitienBac.FixNervuresMod Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod = localMaitienBac.FixNervuresMod

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixnervuresTyp <> localMaitienBac.FixnervuresTyp Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixnervuresTyp = localMaitienBac.FixnervuresTyp

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixCoutureType <> localMaitienBac.FixCoutureType Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixCoutureType = localMaitienBac.FixCoutureType

    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub chk_Theta_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Theta.CheckedChanged
        localMaitienBac.lTheta = Me.chk_Theta.Checked
    End Sub

    Private Sub chk_PriseEnCompteBac_CheckedChanged(sender As Object, e As EventArgs) Handles chk_PriseEnCompteBac.CheckedChanged
        localMaitienBac.lMaintienBac = Me.chk_PriseEnCompteBac.Checked
    End Sub


    Private Sub txt_NbSheetsTransverse_TextChanged(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            localMaitienBac.nt = CInt(Valeur)
            MAJI_DimensionsPlancher()
            MAJI_Transition()
            MAJI_Calculs()

            If AfficheD = Enu_AffichageD.Graphique Then Me.img_Deck.Invalidate()
        End If

    End Sub

    Private Sub MAJI_DimensionsPlancher()

        Me.txt_LongueurP.Text = GetStringInUnit(MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1), Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_LargeurP.Text = GetStringInUnit(localMaitienBac.LargeurPlancher(EntraxeD), Enu_TypeVariable.Longueur, 4, 2, False)

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
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)


        Select Case MyTxt.Name

            Case Me.txt_NbSheetsTransverse.Name
                ValMin = 1
                ValMax = 10
                lValMax = True
                kUnit = 1

            Case Me.txt_EspCouturage.Name

                ValMin = 0.02 / kUnit
                ValMax = 0.5 / kUnit

                lValMax = True

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

    Private Sub cmb_NbSpan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbSpan.SelectedIndexChanged
        If lBuild Then Exit Sub
        localMaitienBac.m = Me.cmb_NbSpan.SelectedIndex + 1
        MAJI_DimensionsPanneau()
        MAJI_DimensionsPlancher()
        MAJI_Transition()
        MAJI_Calculs()
        If AfficheD = Enu_AffichageD.Graphique Then Me.img_Deck.Invalidate()
    End Sub

    Private Sub MAJI_DimensionsPanneau()

        Me.txt_SheetLength.Text = GetStringInUnit(localMaitienBac.LongueurPanneau(EntraxeD), Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_SheetWidth.Text = GetStringInUnit(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.LargeurModule, Enu_TypeVariable.Longueur, 4, 2, False)

    End Sub

    Private Sub MAJI_InfoFixations()

        Me.lbl_DiametreInfo.Text = strDiametreFixation(Me.cmb_TypeFixation.SelectedIndex)
        Me.lbl_SlipInfo.Text = strSlipFixation(Me.cmb_TypeFixation.SelectedIndex)

    End Sub

    Private Sub MAJI_InfoCouturage()

        Me.lbl_DiaS_Info.Text = strDiametreCouturage(Me.cmb_TypSeamFastener.SelectedIndex)
        Me.lbl_GlisseS_Info.Text = strSlipCouturage(Me.cmb_TypSeamFastener.SelectedIndex)

    End Sub

    Private Sub cmb_Transition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Transition.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_Transition.SelectedIndex
            Case 0 : localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Emboitement
            Case 1 : localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Aboutage
            Case 2 : localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Adistance
        End Select

        MAJI_Calculs()
        If AfficheD = Enu_AffichageD.Graphique Then Me.img_Deck.Invalidate()

    End Sub

    Private Sub MAJI_Transition()

        Me.cmb_Transition.Visible = (localMaitienBac.nt > 1)
        Me.lbl_Transition.Visible = (localMaitienBac.nt > 1)

    End Sub

    Private Sub cmb_FixationPoutre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_FixationPoutre.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_FixationPoutre.SelectedIndex

            Case 0 : localMaitienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes
            Case 1 : localMaitienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.UneSurDeux

        End Select

        MAJI_Calculs()

    End Sub

    Private Sub cmb_TypeFixation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeFixation.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeFixation.SelectedIndex
            Case 0 : localMaitienBac.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.VisNormale
            Case 1 : localMaitienBac.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.VisNeoprene
            Case 2 : localMaitienBac.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.Pistolet
        End Select

        MAJI_InfoFixations()
        MAJI_Calculs()
    End Sub

    Private Sub cmb_TypSeamFastener_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypSeamFastener.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypSeamFastener.SelectedIndex
            Case 0 : localMaitienBac.FixCoutureType = cls_MaintienBac.Enu_CoutureType.Vis
            Case 1 : localMaitienBac.FixCoutureType = cls_MaintienBac.Enu_CoutureType.Rivet
        End Select

        MAJI_InfoCouturage()
        MAJI_Calculs()

    End Sub

    Private Sub txt_EspCouturage_TextChanged(sender As Object, e As EventArgs) Handles txt_EspCouturage.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            localMaitienBac.ec = (Valeur)
            MAJI_Calculs()
        End If

    End Sub

#End Region

#Region " Calculs "

    Private Sub MAJI_Calculs()

        Const kUnitFlex As Decimal = 10 ^ 6
        Dim PorteeL As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1)
        Dim eYoung As Decimal = cls_Acier.EYACIER
        Dim Poisson As Decimal = cls_Acier.NU
        Dim c11 As Decimal = localMaitienBac.Flexibilite_C11_DistorsionBac(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung)
        Dim c12 As Decimal = localMaitienBac.Flexibilite_C12_Shear(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung, Poisson)
        Dim c21 As Decimal = localMaitienBac.Flexibilite_C21_BeamFasteners(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Ep)
        Dim c22 As Decimal = localMaitienBac.Flexibilite_C21_BeamFasteners(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Ep)
        Dim cCumul As Decimal
        Dim SAct As Decimal

        Me.txt_Alpha5.Text = GetStringInUnit(localMaitienBac.Alpha5, Enu_TypeVariable.SansType, 3, 3, False)
        Me.txt_K.Text = GetStringInUnit(localMaitienBac.CoefficientK(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac), Enu_TypeVariable.SansType, 3, 3, False)
        Me.txt_c11.Text = GetStringInUnit(c11 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False)
        Me.txt_c12.Text = GetStringInUnit(c12 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False)
        Me.txt_c21.Text = GetStringInUnit(c21 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False)
        Me.txt_c22.Text = GetStringInUnit(c22 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False)

        cCumul = c11 + c12 + c21 + c22
        SAct = PorteeL / cCumul

        Me.txt_c.Text = GetStringInUnit(cCumul * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False)
        Me.txt_Sact.Text = GetStringInUnit(SAct, Enu_TypeVariable.Rigidite, 4, 3, False)

        Dim kTheta, kThetaA, kThetaC As Decimal
        Dim bFs As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Bfs

        kThetaA = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.RigiditeFlexionnelleA(localMaitienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes, bFs)
        kThetaC = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.RigiditeFlexionnelleC(EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire)
        kTheta = 1 / (1 / kThetaA + 1 / kThetaC)

        Me.txt_kTheta.Text = GetStringInUnit(kTheta, Enu_TypeVariable.Effort, 3, 2, False)
        Me.txt_kThetaA.Text = GetStringInUnit(kThetaA, Enu_TypeVariable.Effort, 3, 2, False)
        Me.txt_kThetaC.Text = GetStringInUnit(kThetaC, Enu_TypeVariable.Effort, 3, 2, False)

    End Sub

#End Region

#Region " Symboles "

    Private Sub PaintSymbols(sender As Object, e As PaintEventArgs) _
        Handles img_K.Paint, img_c12.Paint, img_c11.Paint, img_Alpha5.Paint, img_c22.Paint, img_c21.Paint, img_c.Paint, img_Sact.Paint, img_kThetaC.Paint, img_kThetaA.Paint, img_kTheta.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Droite

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_K.Name
                strSymbol = "K"
                strIndice = ""

            Case Me.img_Alpha5.Name
                strSymbol = "a"
                strIndice = "5"
                lGrec = True

            Case Me.img_c11.Name
                strSymbol = "c"
                strIndice = "11"

            Case Me.img_c12.Name
                strSymbol = "c"
                strIndice = "12"

            Case Me.img_c21.Name
                strSymbol = "c"
                strIndice = "21"

            Case Me.img_c22.Name
                strSymbol = "c"
                strIndice = "22"

            Case Me.img_c.Name
                strSymbol = "c"
                strIndice = ""

            Case Me.img_Sact.Name
                strSymbol = "S"
                strIndice = "act"
            Case Me.img_kTheta.Name
                strSymbol = "k"
                strIndice = "theta"
            Case Me.img_kThetaA.Name
                strSymbol = "k"
                strIndice = "theta,A"
            Case Me.img_kThetaC.Name
                strSymbol = "k"
                strIndice = "theta,C"
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub





#End Region

#Region " Gestion Labels Navigation "

    Private Sub lbl_Calculs_Paint(sender As Object, e As PaintEventArgs) Handles lbl_Calculs.Paint

        DrawNaviLabel(e.Graphics, Me.lbl_Calculs.ClientRectangle.Width, Me.lbl_Calculs.ClientRectangle.Height,
                      Me.lbl_Calculs.BackColor, AfficheD = Enu_AffichageD.Valeurs, AfficheD = Enu_AffichageD.Graphique,
                      lMouseG, lMouseD)

    End Sub

    Private Sub DrawNaviLabel(ByVal MyGr As Graphics, ByVal sWi As Single, ByVal sHi As Single, ByVal MyColor As Color,
                              lDrawG As Boolean, lDrawD As Boolean, lMouseG As Boolean, lMouseD As Boolean)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------
        Dim MyRect As New Rectangle(sWi - sHi, 0, sHi - 1, sHi - 1)
        'Dim MyBrush As New LinearGradientBrush(New PointF(sWi - sHi / 2, 0), New PointF(sWi - sHi / 2, sHi), Color.White, MyColor)
        Dim myBrushM As New SolidBrush(Color.White)
        Dim myBrushMSelect As New SolidBrush(OrangeAM)
        Dim myBrushO As New SolidBrush(Color.LightGray)

        MyGr.FillRectangle(New SolidBrush(MyColor), MyRect)

        'MyGr.DrawRectangle(Pens.Black, MyRect)

        Dim MyPts() As PointF = Nothing
        Dim nbPts As Integer

        Const DecOmbre As Single = 1

        '# préparation de la flèche gauche

        GenereFlecheG(MyPts, nbPts, xMargeZone, sHi / 2, SizeZone)

        '# représentation flèche gauche

        If lDrawG Then
            MyGr.FillPolygon(myBrushO, MyPts)

            DecalPts(MyPts, nbPts, -DecOmbre, -DecOmbre)

            If lMouseG Then
                MyGr.FillPolygon(myBrushMSelect, MyPts)
            Else
                MyGr.FillPolygon(myBrushM, MyPts)
            End If
        End If


        '# préparation flèche droite

        DecalPts(MyPts, nbPts, sWi - SizeZone - xMargeZone + DecOmbre, +DecOmbre)

        MirroirX(MyPts, nbPts)

        '# représentation flèche droite

        If lDrawD Then
            MyGr.FillPolygon(myBrushO, MyPts)

            DecalPts(MyPts, nbPts, DecOmbre, DecOmbre)

            If lMouseD Then
                MyGr.FillPolygon(myBrushMSelect, MyPts)
            Else
                MyGr.FillPolygon(myBrushM, MyPts)
            End If

        End If


    End Sub

    Private Sub MirroirX(ByRef MyPts() As PointF, nbPts As Integer)
        Dim xCentre As Single = (xMax(MyPts, nbPts) + xMin(MyPts, nbPts)) / 2

        For i As Integer = 0 To nbPts - 1
            MyPts(i).X = 2 * xCentre - MyPts(i).X
        Next

    End Sub

    Private Function xMax(MyPts() As PointF, nbPts As Integer) As Single
        Dim i As Integer

        Dim valMax As Single = MyPts(0).X

        For i = 1 To nbPts - 1
            valMax = Math.Max(valMax, MyPts(i).X)
        Next
        Return valMax
    End Function

    Private Function xMin(MyPts() As PointF, nbPts As Integer) As Single
        Dim i As Integer

        Dim valMin As Single = MyPts(0).X

        For i = 1 To nbPts - 1
            valMin = Math.Min(valMin, MyPts(i).X)
        Next
        Return valMin
    End Function

    Private Sub DecalPts(ByRef MyPts() As PointF, nbPts As Integer, xDec As Single, yDec As Single)

        For i As Integer = 0 To nbPts - 1

            MyPts(i).X += xDec
            MyPts(i).Y += yDec

        Next

    End Sub

    Private Sub GenereFlecheG(ByRef MyPts() As PointF, ByRef nbPts As Integer, xPosP As Single, yPosP As Single, Size As Single)
        nbPts = 3

        ReDim MyPts(nbPts - 1)

        MyPts(0).X = xPosP
        MyPts(0).Y = yPosP
        MyPts(1).X = xPosP + Size / 2
        MyPts(1).Y = yPosP - Size / 2
        MyPts(2).X = xPosP + Size / 2
        MyPts(2).Y = yPosP + Size / 2

    End Sub

    Private Sub lbl_Calculs_MouseMove(sender As Object, e As MouseEventArgs) Handles lbl_Calculs.MouseMove

        Dim xSouris As Single = e.X
        Dim ySouris As Single = e.Y

        Dim sWi As Single = Me.lbl_Calculs.ClientRectangle.Width
        Dim sHi As Single = Me.lbl_Calculs.ClientRectangle.Height

        lMouseG = (xSouris >= xMargeZone) And (xSouris <= xMargeZone + SizeZone / 2) _
              And (ySouris >= xMargeZone) And (ySouris <= sHi + xMargeZone)

        lMouseD = (xSouris <= sWi - xMargeZone) And (xSouris >= sWi - xMargeZone - SizeZone) _
              And (ySouris >= xMargeZone) And (ySouris <= sHi + xMargeZone)

        Me.lbl_Calculs.Invalidate()

    End Sub

    Private Sub lbl_Calculs_MouseUp(sender As Object, e As MouseEventArgs) Handles lbl_Calculs.MouseUp

        If lMouseG Then

            AfficheD = Enu_AffichageD.Graphique

        ElseIf lMouseD Then

            AfficheD = Enu_AffichageD.Valeurs

        End If

        Me.lbl_Calculs.Invalidate()
        MAJI_NaviLableCalculs()

    End Sub

    Private Sub MAJI_NaviLableCalculs()

        Select Case AfficheD
            Case Enu_AffichageD.Valeurs
                Me.lbl_Calculs.Text = strResultats
            Case Enu_AffichageD.Graphique
                Me.lbl_Calculs.Text = strDessin
        End Select

        Me.pan_Rigidite.Visible = (AfficheD = Enu_AffichageD.Valeurs)
        Me.pan_RigiditeShear.Visible = (AfficheD = Enu_AffichageD.Valeurs)
        Me.img_Deck.Visible = (AfficheD = Enu_AffichageD.Graphique)

        If Me.img_Deck.Visible Then Me.img_Deck.Invalidate()
    End Sub


#End Region

#Region " Dessin du plancher "

    Private Sub img_Deck_Paint(sender As Object, e As PaintEventArgs) Handles img_Deck.Paint

        DrawPlancher(e.Graphics, Me.img_Deck.ClientRectangle.Width, Me.img_Deck.ClientRectangle.Height,
                     localMaitienBac, MyProjet.Poutres(MyProjet.IndEnCours))

    End Sub

    Private Sub DrawPlancher(ByVal MyGr As Graphics, ByVal pWi As Single, ByVal pHi As Single,
                             MyDeck As cls_MaintienBac, myPoutre As cls_Poutre, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   06/01/24:   Création - POM - ACBPMX V1
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Représentation du plancher et de la disposition des bacs pour le maitien
        '-----------------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyParAffD As Struc_Affichage
        Const kAdjust As Decimal = 0.9

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim LargeurP As Decimal = MyDeck.LargeurPlancher(EntraxeD)
        Dim NbPoutres As Integer
        Dim PorteeL As Decimal
        Dim MyPen As Pen
        Dim MyPenNormal As New Pen(Color.Black, 1.0)
        Dim MyPenSelect As New Pen(Color.DarkBlue, 1.5)
        Dim iPoutreRef As Integer
        Dim nbLongi As Integer
        Dim xC, yC As Decimal
        Dim LongBac As Decimal

        '--> Initialisation

        PorteeL = myPoutre.LongueurTravee(1)
        If myPoutre.lIntermediaire Then iPoutreRef = 2 Else iPoutreRef = 1

        '--> Initialisation des paramètres d'affichage

        yMin = 0
        xMin = 0
        xMax = LargeurP
        yMax = PorteeL
        dCar = EntraxeD / 10

        ParametresAffichage(MyParAffD, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation des poutres

        Dim xPoutre As Decimal
        NbPoutres = MyDeck.m * MyDeck.nt + 1

        For iPoutre As Integer = 1 To NbPoutres

            xPoutre = (iPoutre - 1) * EntraxeD
            If iPoutreRef = iPoutre Then MyPen = MyPenSelect Else MyPen = MyPenNormal
            AddLigne(MyGr, MyPen, xPoutre, 0, xPoutre, PorteeL, MyParAffD)

        Next

        '--> Représentation des bacs (individuels)

        nbLongi = Math.Floor(PorteeL / myPoutre.Dalle.Bac.LargeurModule)
        Select Case MyDeck.Transition
            Case cls_MaintienBac.Enu_Transition.Aboutage
                LongBac = (EntraxeD * MyDeck.m)
            Case cls_MaintienBac.Enu_Transition.Adistance
                LongBac = (EntraxeD * MyDeck.m) - dCar
            Case cls_MaintienBac.Enu_Transition.Emboitement
                LongBac = (EntraxeD * MyDeck.m) + dCar
        End Select
        For iTrans As Integer = 1 To MyDeck.nt

            For iLongi As Integer = 1 To nbLongi

                xC = (iTrans - 1 / 2) * (EntraxeD * MyDeck.m)
                yC = (iLongi - 1 / 2) * myPoutre.Dalle.Bac.LargeurModule

                DrawBacInd(MyGr, MyParAffD, xC, yC, LongBac, myPoutre.Dalle.Bac, False, myPoutre.Dalle.Bac.LargeurModule)

            Next

        Next

        If IsSmaller(nbLongi * myPoutre.Dalle.Bac.LargeurModule, PorteeL) Then

            Dim DeltaL As Decimal = PorteeL - nbLongi * myPoutre.Dalle.Bac.LargeurModule

            yC = PorteeL - DeltaL / 2
            For iTrans As Integer = 1 To MyDeck.nt

                xC = (iTrans - 1 / 2) * (EntraxeD * MyDeck.m)

                DrawBacInd(MyGr, MyParAffD, xC, yC, LongBac, myPoutre.Dalle.Bac, False, DeltaL)

            Next

        End If

    End Sub


    Private Sub DrawBacInd(MyGr As Graphics, myParAff As Struc_Affichage, xC As Decimal, yC As Decimal, LongueurB As Decimal,
                           MyBac As cls_Bac, lNervures As Boolean, LargeurBac As Decimal)

        Dim xo, yo As Decimal
        Dim xe, ye As Decimal

        xo = xC - LongueurB / 2
        yo = yC - LargeurBac / 2
        xe = xC + LongueurB / 2
        ye = yC + LargeurBac / 2        ' MyBac.LargeurModule / 2

        AddRectanglePlein(MyGr, New SolidBrush(Color.White), New Pen(BleuCTICM), xo, yo, xe, ye, myParAff, False, True)

    End Sub



#End Region

End Class