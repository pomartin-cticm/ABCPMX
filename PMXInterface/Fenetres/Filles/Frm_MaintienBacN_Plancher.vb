Imports PMXInterface.Frm_MaintienBac
Imports PMXMoteur2

Public Class Frm_MaintienBacN_Plancher

#Region " Variables "

    Dim lBuild As Boolean
    Dim strTransitionOptions(2) As String

    Dim EntraxeD As Decimal

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

        Me.lbl_Options.BackColor = CouleurBackBandeaux
        Me.lbl_Options.ForeColor = CouleurForeBandeaux

        Me.lbl_Floor.BackColor = CouleurBackBandeaux
        Me.lbl_Floor.ForeColor = CouleurForeBandeaux

        Me.lbl_Panneau.BackColor = CouleurBackBandeaux
        Me.lbl_Panneau.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_LargeurP, False)
        PrepareTextBoxDipo(Me.txt_LongueurP, False)
        PrepareTextBoxDipo(Me.txt_SheetLength, False)
        PrepareTextBoxDipo(Me.txt_SheetWidth, False)
        PrepareTextBoxDipo(Me.txt_EntraxeD, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitF1.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '=== OPTIONS ===============================================================

            Me.lbl_Options.Text = Bloc("OPTIONS")
            Me.chk_PriseEnCompteBac.Text = Bloc("RESTRAINTBYTHEDECK")
            Me.chk_Theta.Text = Bloc("THETA")

            '=== PLANCHER ===============================================================

            Me.lbl_Floor.Text = Bloc("FLOORDEF")
            Me.lbl_EntraxeD.Text = Bloc("BEAMSPACING")
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

            Me.lbl_Tpr.Text = Bloc("THCOATING")
            Me.lbl_Fu.Text = Bloc("FUP")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally

        End Try

    End Sub

    Private Sub PrepareFenetre()
        EntraxeD = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeSolive

        RemplirCmbNbSpans()
        RemplirCmbTransition()
    End Sub

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

    Private Sub AffichePoutreEnCours()

        Me.chk_PriseEnCompteBac.Checked = Frm_MaintienBacN.localMaitienBac.lMaintienBac
        Me.chk_Theta.Checked = Frm_MaintienBacN.localMaitienBac.lTheta

        Me.cmb_NbSpan.SelectedIndex = Frm_MaintienBacN.localMaitienBac.m - 1
        Select Case Frm_MaintienBacN.localMaitienBac.Transition
            Case cls_MaintienBac.Enu_Transition.Emboitement : Me.cmb_Transition.SelectedIndex = 0
            Case cls_MaintienBac.Enu_Transition.Aboutage : Me.cmb_Transition.SelectedIndex = 1
            Case cls_MaintienBac.Enu_Transition.Adistance : Me.cmb_Transition.SelectedIndex = 2
        End Select

        Me.txt_NbSheetsTransverse.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.nt, Enu_TypeVariable.SansType, 2, 0, NON_U, False)
        Me.txt_EntraxeD.Text = GetStringInUnitN(EntraxeD, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        Me.txt_Tpr.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.Tpr, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_Fup.Text = GetStringInUnitN(Frm_MaintienBacN.localFup, Enu_TypeVariable.Contrainte, 4, 3, NON_U, True)

        MAJI_DimensionsPlancher()
        MAJI_DimensionsPanneau()
        MAJI_Transition()
        MAJI_Dessin()

    End Sub

#End Region

#Region " Evènements "

    Private Sub txt_Fup_TextChanged(sender As Object, e As EventArgs) Handles txt_Fup.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localFup = Valeur
            'MAJI_DimensionsPlancher()
            'MAJI_Transition()
            'MAJI_Calculs()

            'MAJI_Dessin()
        End If
    End Sub

    Private Sub txt_Tpr_TextChanged(sender As Object, e As EventArgs) Handles txt_Tpr.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.Tpr = Valeur
            'MAJI_DimensionsPlancher()
            'MAJI_Transition()
            'MAJI_Calculs()

            'MAJI_Dessin()
        End If
    End Sub

    Private Sub txt_NbSheetsTransverse_TextChanged(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.nt = CInt(Valeur)
            MAJI_DimensionsPlancher()
            MAJI_Transition()
            'MAJI_Calculs()

            MAJI_Dessin()
        End If

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

            Case Me.txt_NbSheetsTransverse.Name
                ValMin = 1
                ValMax = 10
                lValMax = True
                kUnit = 1

            Case Me.txt_Tpr.Name

                ValMin = 0
                ValMax = (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Tp / 10) / kUnit
                lValMax = True

            Case Me.txt_Fup.Name

                kUnit = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)

                ValMin = 1
                ValMax = 1000 / kUnit
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

    Private Sub chk_PriseEnCompteBac_CheckedChanged(sender As Object, e As EventArgs) Handles chk_PriseEnCompteBac.CheckedChanged
        Frm_MaintienBacN.localMaitienBac.lMaintienBac = Me.chk_PriseEnCompteBac.Checked
        MAJI_Dessin()
    End Sub

    Private Sub chk_Theta_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Theta.CheckedChanged
        Frm_MaintienBacN.localMaitienBac.lTheta = Me.chk_Theta.Checked
        MAJI_Dessin()
    End Sub

    Private Sub cmb_Transition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Transition.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_Transition.SelectedIndex
            Case 0 : Frm_MaintienBacN.localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Emboitement
            Case 1 : Frm_MaintienBacN.localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Aboutage
            Case 2 : Frm_MaintienBacN.localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Adistance
        End Select

        MAJI_Calculs()
        MAJI_Dessin()

    End Sub

    Private Sub cmb_NbSpan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbSpan.SelectedIndexChanged
        If lBuild Then Exit Sub
        Frm_MaintienBacN.localMaitienBac.m = Me.cmb_NbSpan.SelectedIndex + 1
        MAJI_DimensionsPanneau()
        MAJI_DimensionsPlancher()
        MAJI_Transition()
        MAJI_Calculs()
        MAJI_Dessin()
    End Sub

#End Region

#Region " Gestion des mises à jour de la fenêtre "

    Private Sub MAJI_Calculs()
        Frm_MaintienBacN.MAJI_Calculs()
    End Sub

    Private Sub MAJI_Dessin()

        Frm_MaintienBacN.MAJI_Dessin()

    End Sub

    Private Sub MAJI_DimensionsPlancher()

        Me.txt_LongueurP.Text = GetStringInUnitN(MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1), Enu_TypeVariable.Longueur, 4, 2, NON_U, True)
        Me.txt_LargeurP.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.LargeurPlancher(EntraxeD), Enu_TypeVariable.Longueur, 4, 2, NON_U, True)

    End Sub

    Private Sub MAJI_Transition()

        Me.cmb_Transition.Visible = (Frm_MaintienBacN.localMaitienBac.nt > 1)
        Me.lbl_Transition.Visible = (Frm_MaintienBacN.localMaitienBac.nt > 1)

    End Sub

    Private Sub MAJI_DimensionsPanneau()

        Me.txt_SheetLength.Text = GetStringInUnit(Frm_MaintienBacN.localMaitienBac.LongueurPanneau(EntraxeD), Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_SheetWidth.Text = GetStringInUnit(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.LargeurModule, Enu_TypeVariable.Longueur, 4, 2, False)

    End Sub

#End Region

#Region " Symboles "

    Private Sub PaintSymbols(sender As Object, e As PaintEventArgs) Handles img_ap.Paint, img_bp.Paint, img_m.Paint, img_nt.Paint, img_Tpr.Paint, img_Fup.Paint

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

            Case Me.img_ap.Name
                strSymbol = "a"
                strIndice = "p"

            Case Me.img_bp.Name
                strSymbol = "b"
                strIndice = "p"

            Case Me.img_m.Name
                strSymbol = "m"
                strIndice = ""

            Case Me.img_nt.Name
                strSymbol = "n"
                strIndice = "t"

            Case Me.img_Tpr.Name
                strSymbol = "t"
                strIndice = "pr"

            Case Me.img_Fup.Name
                strSymbol = "f"
                strIndice = "up"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements select "

    'Private Sub cmb_NbSpan_Leave(sender As Object, e As EventArgs) Handles cmb_NbSpan.Leave
    '    Frm_MaintienBacN.ChangeSelect(-1)
    'End Sub

    'Private Sub cmb_NbSpan_Enter(sender As Object, e As EventArgs) Handles cmb_NbSpan.Enter
    '    Frm_MaintienBacN.ChangeSelectBacIndi()
    'End Sub

    Private Sub chk_PriseEnCompteBac_MouseEnter(sender As Object, e As EventArgs) Handles chk_Theta.MouseEnter, chk_PriseEnCompteBac.MouseEnter
        Frm_MaintienBacN.ChangeSelectPoutre()
    End Sub

    Private Sub chk_PriseEnCompteBac_MouseLeave(sender As Object, e As EventArgs) Handles chk_Theta.MouseLeave, chk_PriseEnCompteBac.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub txt_NbSheetsTransverse_Leave(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.Leave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    'Private Sub txt_NbSheetsTransverse_Enter(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.Enter
    '    Frm_MaintienBacN.ChangeSelectLargeurP()
    'End Sub

    'Private Sub cmb_NbSpan_MouseLeave(sender As Object, e As EventArgs) Handles cmb_NbSpan.MouseLeave
    '    Frm_MaintienBacN.ChangeSelect(-1)
    'End Sub

    Private Sub cmb_NbSpan_MouseEnter(sender As Object, e As EventArgs) Handles cmb_NbSpan.MouseEnter
        Frm_MaintienBacN.ChangeSelectBacIndi()
    End Sub

    Private Sub txt_NbSheetsTransverse_MouseEnter(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.MouseEnter
        Frm_MaintienBacN.ChangeSelectLargeurP()
    End Sub

    Private Sub txt_NbSheetsTransverse_MouseLeave(sender As Object, e As EventArgs) Handles txt_NbSheetsTransverse.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub txt_LongueurP_MouseEnter(sender As Object, e As EventArgs) Handles txt_LongueurP.MouseEnter
        Frm_MaintienBacN.ChangeSelectPortee()
    End Sub

    Private Sub txt_LongueurP_MouseLeave(sender As Object, e As EventArgs) Handles txt_LongueurP.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub txt_EntraxeD_MouseLeave(sender As Object, e As EventArgs) Handles txt_EntraxeD.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub txt_EntraxeD_MouseEnter(sender As Object, e As EventArgs) Handles txt_EntraxeD.MouseEnter
        Frm_MaintienBacN.ChangeSelectEntraxe()
    End Sub

    Private Sub txt_LargeurP_MouseEnter(sender As Object, e As EventArgs) Handles txt_LargeurP.MouseEnter
        Frm_MaintienBacN.ChangeSelectLargeurP()
    End Sub

    Private Sub txt_LargeurP_MouseLeave(sender As Object, e As EventArgs) Handles txt_LargeurP.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub txt_SheetLength_MouseEnter(sender As Object, e As EventArgs) Handles txt_SheetLength.MouseEnter
        Frm_MaintienBacN.ChangeSelectAP()
    End Sub

    Private Sub txt_SheetLength_MouseLeave(sender As Object, e As EventArgs) Handles txt_SheetLength.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub txt_SheetWidth_MouseEnter(sender As Object, e As EventArgs) Handles txt_SheetWidth.MouseEnter
        Frm_MaintienBacN.ChangeSelectBP()
    End Sub

    Private Sub txt_SheetWidth_MouseLeave(sender As Object, e As EventArgs) Handles txt_SheetWidth.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub cmb_NbSpan_MouseLeave(sender As Object, e As EventArgs) Handles cmb_NbSpan.MouseLeave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub


#End Region

End Class