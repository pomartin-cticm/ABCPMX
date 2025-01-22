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

        Me.lbl_Floor.BackColor = CouleurBackBandeaux
        Me.lbl_Floor.ForeColor = CouleurForeBandeaux

        Me.lbl_Panneau.BackColor = CouleurBackBandeaux
        Me.lbl_Panneau.ForeColor = CouleurForeBandeaux

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

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

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
        ' Me.chk_Theta.Checked = localMaitienBac.lTheta

        Me.cmb_NbSpan.SelectedIndex = Frm_MaintienBacN.localMaitienBac.m - 1
        Select Case Frm_MaintienBacN.localMaitienBac.Transition
            Case cls_MaintienBac.Enu_Transition.Emboitement : Me.cmb_Transition.SelectedIndex = 0
            Case cls_MaintienBac.Enu_Transition.Aboutage : Me.cmb_Transition.SelectedIndex = 1
            Case cls_MaintienBac.Enu_Transition.Adistance : Me.cmb_Transition.SelectedIndex = 2
        End Select

        Me.txt_NbSheetsTransverse.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.nt, Enu_TypeVariable.SansType, 2, 0, NON_U, False)

        MAJI_DimensionsPlancher()
        MAJI_DimensionsPanneau()
        MAJI_Transition()
        MAJI_Dessin()

    End Sub

#End Region

#Region " Evènements "

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

    Private Sub PaintSymbols(sender As Object, e As PaintEventArgs) Handles img_ap.Paint, img_bp.Paint

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

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements select "

    Private Sub cmb_NbSpan_Leave(sender As Object, e As EventArgs) Handles cmb_NbSpan.Leave
        Frm_MaintienBacN.ChangeSelect(-1)
    End Sub

    Private Sub cmb_NbSpan_Enter(sender As Object, e As EventArgs) Handles cmb_NbSpan.Enter
        Frm_MaintienBacN.ChangeSelect(1)
    End Sub

#End Region

End Class