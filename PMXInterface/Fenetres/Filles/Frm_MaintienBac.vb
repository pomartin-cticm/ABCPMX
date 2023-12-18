Imports PMXMoteur2

Public Class Frm_MaintienBac

#Region " Variables "

    Dim lBuild As Boolean

    Dim localMaitienBac As New cls_MaintienBac
    Dim EntraxeD As Decimal                     ' Entraxe entre les solives pour les calculs
    Dim strTransitionOptions(2) As String
    Dim strFastening(1) As String

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

            Me.lbl_FixationSolive.Text = Bloc("FASTENING")
            strFastening(0) = Bloc("EVERYRIB")
            strFastening(1) = Bloc("EVERY2RIBS")

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

        MAJI_DimensionsPlancher()
        MAJI_DimensionsPanneau()

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

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition <> localMaitienBac.Transition Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition = localMaitienBac.Transition

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod <> localMaitienBac.FixNervuresMod Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod = localMaitienBac.FixNervuresMod

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
    End Sub

    Private Sub MAJI_DimensionsPanneau()

        Me.txt_SheetLength.Text = GetStringInUnit(localMaitienBac.LongueurPanneau(EntraxeD), Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_SheetWidth.Text = GetStringInUnit(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.LargeurModule, Enu_TypeVariable.Longueur, 4, 2, False)

    End Sub

    Private Sub cmb_Transition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Transition.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_Transition.SelectedIndex
            Case 0 : localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Emboitement
            Case 1 : localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Aboutage
            Case 2 : localMaitienBac.Transition = cls_MaintienBac.Enu_Transition.Adistance
        End Select
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
    End Sub


#End Region

End Class