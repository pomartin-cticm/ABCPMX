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

        EntraxeD = Me.EntraxeSolive(MyProjet.Poutres(MyProjet.IndEnCours))

    End Sub

    Private Function EntraxeSolive(MyBeam As cls_Poutre) As Decimal

        Dim MyD As Decimal

        If MyBeam.lIntermediaire Then
            MyD = (MyBeam.EntraxeD1 + MyBeam.EntraxeD2) / 2
        Else
            MyD = MyBeam.EntraxeD2
        End If

        Return MyD

    End Function

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

        PrepareTextBoxDipo(Me.txt_LargeurP, False)
        PrepareTextBoxDipo(Me.txt_LongueurP, False)
        PrepareTextBoxDipo(Me.txt_SheetLength, False)
        PrepareTextBoxDipo(Me.txt_SheetWidth, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitSact.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

    Private Sub AffichePoutreEnCours()

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

    Private Sub txt_NbSheetsTransverse_TextChanged(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            localMaitienBac.nt = CInt(Valeur)
            MAJI_DimensionsPlancher()
            MAJI_Transition()
            MAJI_Calculs()
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
                ValMax = 5
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
        Dim c12 As Decimal = localMaitienBac.Flexibility_C12_Shear(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung, Poisson)
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


    End Sub

#End Region

#Region " Symboles "

    Private Sub PaintSymbols(sender As Object, e As PaintEventArgs) _
        Handles img_K.Paint, img_c12.Paint, img_c11.Paint, img_Alpha5.Paint, img_c22.Paint, img_c21.Paint, img_c.Paint

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

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region


End Class