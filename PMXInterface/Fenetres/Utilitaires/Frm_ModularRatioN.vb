Imports PMXMoteur2
Imports System.IO

Public Class Frm_ModularRatioN


#Region " Attributs "

    Dim lBuild As Boolean
    Dim tabPsiL() As Decimal = {0, 1.1, 0.55, 1.5}
    Dim tabCharges(3) As String

    Dim AgeT As Integer = 50 * 365
    Dim AgeT0 As Integer = 28

    Dim AgeT0_SH As Integer = 1
    Dim AgeT0_Normal As Integer = 28

    Dim MonBeton As New cls_Beton

    Dim RayonH0 As Decimal = 0.2

    Const iLive As Integer = 0
    Const iPerm As Integer = 1
    Const iShri As Integer = 2
    Const iDefI As Integer = 3

    Dim iCharge As Integer
    Dim iChargePrec As Integer

    Dim lCalculG1 As Boolean            ' Indique si le calcul se fait selon la génération 1

    Dim Bloc As New Dictionary(Of String, String)

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_ModularRatio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiserFenetre()
        IntialiseVariables()
        MAJI_ProprietesBeton()
        MAJI_Coefficients()
        lBuild = False
    End Sub

    Private Sub IntialiseVariables()

        MonBeton.lLeger = False
        MonBeton.Classe = cls_Beton.TabClasseBeton(0)

    End Sub

    Private Sub InitialiserFenetre()

        RemplirComboRH()
        RemplirComboBeton()
        RemplirComboCharges()
        RemplirComboCiment()
        RemplirComboNorm()
        MAJI_Norme()

        Me.txt_AgeT.Text = GetStringInUnitN(AgeT, Enu_TypeVariable.SansType, 2, 0, NON_U, False)

        Me.txt_H0.Text = GetStringInUnitN(RayonH0, Enu_TypeVariable.Dimension, 3, 2, NON_U, True)

        Me.txt_kE.Text = GetStringInUnitN(MonBeton.kE, Enu_TypeVariable.SansType, 4, 1, NON_U, True)

        AfficherAgeT0()
        MAJI_TxtAgeT0()

        PrepareAffResultats()
        If lCalculG1 Then
            AfficherResultatsIntermediaresG1()
        Else
        End If

    End Sub

    Private Sub AfficherAgeT0()

        Me.txt_AgeT0.Text = GetStringInUnit(AgeT0, Enu_TypeVariable.SansType, 2, 0, False)

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitModule.Text = "GPa"
        Me.etq_UnitSigma1.Text = "MPa"
        Me.etq_UnitSigma2.Text = "MPa"
        'Me.etq_UnitSigma3.Text = "MPa"

        Dim lFrancais As Boolean = (LogicielInfo.ListeLangue(LogicielOptions.IndLangue) = FRANCAIS)
        Dim UnitJour As String = "d"
        If lFrancais Then UnitJour = "j"
        Me.etq_UnitD1.Text = UnitJour
        Me.etq_UnitD2.Text = UnitJour

    End Sub

    Private Sub RemplirComboRH()

        Me.cmb_RH.Items.Clear()

        For i As Integer = 0 To cls_OptionsCalcul.tabRH.GetUpperBound(0)

            Me.cmb_RH.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabRH(i), Enu_TypeVariable.SansType, 2, 0, False) & "%")

        Next

        Me.cmb_RH.SelectedIndex = 0

    End Sub

    Private Sub RemplirComboCharges()

        Me.cmb_Charge.Items.Clear()

        For i As Integer = 0 To Me.tabCharges.GetUpperBound(0)

            Me.cmb_Charge.Items.Add(Me.tabCharges(i))

        Next

        Me.cmb_Charge.SelectedIndex = 0

        iCharge = 0
        iChargePrec = 0

    End Sub

    Private Sub RemplirComboBeton()

        Me.cmb_ClasseBeton.Items.Clear()

        Me.cmb_ClasseBeton.Items.AddRange(cls_Beton.TabClasseBeton)

        'Me.cmb_ClasseBeton.SelectedIndex = 0

        Me.cmb_ClasseBeton.SelectedIndex = Array.IndexOf(cls_Beton.TabClasseBeton, MonBeton.Classe)

    End Sub

    Private Sub RemplirComboCiment()

        Me.cmb_ClassCiment.Items.Clear()

        Me.cmb_ClassCiment.Items.AddRange(cls_Beton.TabClasseCiment)

        'Me.cmb_ClassCiment.SelectedIndex = 1
        Me.cmb_ClassCiment.SelectedIndex = Array.IndexOf(cls_Beton.TabClasseCiment, MonBeton.Ciment)

    End Sub

    Private Sub RemplirComboNorm()

        Dim indG1 As Integer = -1
        Dim indG2 As Integer = -1
        Dim iSelect As Integer = 0

        Me.cmb_Norme.Items.Clear()

        If LogicielReglages.lG1 Or LogicielOptions.lExpert Then
            Me.cmb_Norme.Items.Add(TabNormeEN(0))
            indG1 = 0
        End If
        If LogicielReglages.lG2 Or LogicielOptions.lExpert Then
            Me.cmb_Norme.Items.Add(TabNormeEN(1))
            indG2 = indG1 + 1
        End If

        Select Case OptionsCalcul.Norme
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG1
                iSelect = Math.Max(indG1, 0)
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG2
                iSelect = Math.Max(indG2, 0)
        End Select
        Me.cmb_Norme.SelectedIndex = iSelect

    End Sub

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_MODULARRATIOTOOL")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                Me.Text = Bloc("TITLE")

                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("CLOSE")

                Me.lbl_Parameters.Text = Bloc("PARAMETERS")
                Me.lbl_Norme.Text = Bloc("NORM")
                Me.lbl_Beton.Text = Bloc("CONCRETE")
                Me.lbl_PsiL.Text = Bloc("PSIL")
                Me.lbl_RelativeRH.Text = Bloc("RELATIVEHUMIDITY")

                Me.lbl_Ages.Text = Bloc("AGES")
                Me.lbl_AgeT.Text = Bloc("AGETN")
                Me.lbl_AgeT0.Text = Bloc("AGET0N")
                Me.lbl_DimensionH0.Text = Bloc("NOTIONALSIZE")

                Me.lbl_PropBeton.Text = Bloc("CONCRETEPROP")
                Me.lbl_ClasseCiment.Text = Bloc("CLASSCEMENT")
                Me.lbl_ModuleEcm.Text = Bloc("SECANTMODULUS")
                Me.lbl_ResistanceCompression.Text = Bloc("RCOMPRESSION")
                'Me.lbl_ResistanceTraction.Text = Bloc("RTENSION")

                Me.lbl_Resultats.Text = Bloc("RESULTS")
                'Me.lbl_CoefLongTerme.Text = Bloc("LONGTERM_N")
                'Me.lbl_CoefCourtTerme.Text = Bloc("SHORTTERM_N")

                Me.lbl_CoefficientAnnexB.Text = Bloc("COEFANNEXB")

                Me.lbl_Charge.Text = Bloc("LOADS")
                Me.tabCharges(0) = Bloc("LIVELOADS")
                Me.tabCharges(1) = Bloc("PERMANENTLOADS")
                Me.tabCharges(2) = Bloc("SHRINKAGE")
                Me.tabCharges(3) = Bloc("IMPOSEDD")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_ModularRatio/GestionLangue")
            Finally
                'Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

        Me.lbl_Parameters.BackColor = CouleurBackBandeaux
        Me.lbl_Parameters.ForeColor = CouleurForeBandeaux

        Me.lbl_PropBeton.BackColor = CouleurBackBandeaux
        Me.lbl_PropBeton.ForeColor = CouleurForeBandeaux

        Me.lbl_Resultats.BackColor = CouleurBackBandeaux
        Me.lbl_Resultats.ForeColor = CouleurForeBandeaux

        Me.lbl_CoefficientAnnexB.BackColor = CouleurBackBandeaux
        Me.lbl_CoefficientAnnexB.ForeColor = CouleurForeBandeaux


        PrepareTextBoxDipo(Me.txt_Ecm, False)
        PrepareTextBoxDipo(Me.txt_Fck, False)
        PrepareTextBoxDipo(Me.txt_Fcm, False)
        'PrepareTextBoxDipo(Me.txt_Fctm, False)
        PrepareTextBoxDipo(Me.txt_PsiL2, False)

        PrepareTextBoxDipo(Me.txt_n0, False)
        PrepareTextBoxDipo(Me.txt_nL, False)

    End Sub

    Private Sub PrepareAffResultats()

        Me.pan_ConteneurResultats.Controls.Clear()

        If lCalculG1 Then
            Me.pan_ConteneurResultats.Controls.Add(Frm_ModularN_ResultatsG1.pan_AnnexB)
            Frm_ModularN_ResultatsG1.InitialiseFenetre(Bloc)
        Else
            Me.pan_ConteneurResultats.Controls.Add(Frm_ModularN_ResultatsG2.pan_ResultsAnnexB)
            Frm_ModularN_ResultatsG2.InitialiseFenetre(Bloc)
        End If

    End Sub

#End Region

#Region " FERMETURE "

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Mise à jour de la fenêtre"

    Private Sub MAJI_ProprietesBeton()
        '--------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------
        '   Mise à joure des propriétés du béton
        '--------------------------------------------------------------------------------------------------

        MonBeton.Calcul_Proprietes(lCalculG1)

        Me.txt_Ecm.Text = GetStringInUnitN(MonBeton.Ecm, Enu_TypeVariable.ContrainteGPa, 4, 3, NON_U, False)
        Me.txt_Fck.Text = GetStringInUnitN(MonBeton.Fck, Enu_TypeVariable.ContrainteMPa, 3, 2, NON_U, False)
        Me.txt_Fcm.Text = GetStringInUnitN(MonBeton.Fcm, Enu_TypeVariable.ContrainteMPa, 3, 2, NON_U, False)
        'Me.txt_Fctm.Text = GetStringInUnit(MonBeton.Fctm, Enu_TypeVariable.ContrainteMPa, 3, 2, False)

    End Sub

    Private Sub MAJI_Coefficients()

        Dim RH As Decimal = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)
        Dim PsiL As Decimal = tabPsiL(Me.cmb_Charge.SelectedIndex)
        Dim n0 As Decimal = MonBeton.CoefficientEquivalenceCT(lCalculG1)
        Dim nL As Decimal = MonBeton.CoefficientEquivalence(RH, RayonH0, AgeT, AgeT0, PsiL, lCalculG1)

        Me.txt_n0.Text = GetStringInUnitN(n0, Enu_TypeVariable.SansType, 3, 2, NON_U, False)
        Me.txt_nL.Text = GetStringInUnitN(nL, Enu_TypeVariable.SansType, 3, 2, NON_U, False)

        Me.txt_PsiL2.Text = GetStringInUnitN(PsiL, Enu_TypeVariable.SansType, 3, 2, NON_U, True)

        If lCalculG1 Then
            AfficherResultatsIntermediaresG1()
        Else
            AfficherResultatsIntermediaresG2()
        End If

    End Sub

    Private Sub AfficherResultatsIntermediaresG2()

        Dim RH As Decimal = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)

        Dim PhiT, PhiTBc, PhiTDc As Decimal
        Dim pBetaBcFcm As Decimal
        Dim pBetaDcFcm As Decimal
        Dim pBetaBcT As Decimal
        Dim pBetaDcRH As Decimal
        Dim pBetaDcT0, pBetaDcTT0 As Decimal
        Dim t0Adj As Decimal

        t0Adj = MonBeton.AgeAjuste(AgeT0)

        pBetaBcFcm = MonBeton.BetaBcFcm
        pBetaDcFcm = MonBeton.BetaDcFcm
        pBetaBcT = MonBeton.BetaBcT(AgeT, AgeT0, t0Adj)
        pBetaDcRH = MonBeton.BetaDcRH(RH, RayonH0)
        pBetaDcT0 = MonBeton.BetaDcT0(t0Adj)
        pBetaDcTT0 = MonBeton.BetaDcTT0(RayonH0, AgeT, AgeT0, t0Adj)

        PhiTBc = pBetaBcFcm * pBetaBcT
        PhiTDc = pBetaDcFcm * pBetaDcRH * pBetaDcT0 * pBetaDcTT0

        PhiT = PhiTBc + PhiTDc

        Frm_ModularN_ResultatsG2.AfficherResultats(PhiT, PhiTBc, PhiTDc, pBetaDcRH, pBetaBcFcm, pBetaDcFcm, pBetaBcT, pBetaDcTT0, pBetaDcT0)

    End Sub

    Private Sub AfficherResultatsIntermediaresG1()

        Dim RH As Decimal = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)
        Dim PhiRH As Decimal = MonBeton.PhiRH(RH, RayonH0)

        Dim BetaFcm As Decimal = MonBeton.BetaFcm

        Dim BetaT0 As Decimal = MonBeton.Beta_t0(AgeT0)

        Dim Phi0 As Decimal = PhiRH * BetaFcm * BetaT0

        Dim BetaC As Decimal = MonBeton.BetaC_tt0(RH, RayonH0, AgeT, AgeT0)

        Dim PhiT As Decimal = Phi0 * BetaC

        Frm_ModularN_ResultatsG1.AfficherResultats(PhiRH, BetaFcm, BetaT0, Phi0, BetaC, PhiT)

    End Sub

#End Region

#Region " Evènements "

    Private Sub cmb_Norme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Norme.SelectedIndexChanged
        MAJI_Norme()
        PrepareAffResultats()
        MAJI_Coefficients()
    End Sub

    Private Sub MAJI_Norme()
        lCalculG1 = (Me.cmb_Norme.SelectedIndex = 0) And (LogicielReglages.lG1 Or LogicielOptions.lExpert)

        If lCalculG1 Then
            Me.TLpan_Milieu.RowStyles(1).Height = 0
        Else
            Me.TLpan_Milieu.RowStyles(1).Height = 65
        End If

        Me.img_H0.Invalidate()
    End Sub

    Private Sub cmb_ClasseBeton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBeton.SelectedIndexChanged

        If lBuild Then Exit Sub
        MonBeton.Classe = cls_Beton.TabClasseBeton(Me.cmb_ClasseBeton.SelectedIndex)
        MAJI_ProprietesBeton()
        MAJI_Coefficients()

    End Sub

    Private Sub cmb_ClassCiment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClassCiment.SelectedIndexChanged
        If lBuild Then Exit Sub
        MonBeton.Ciment = cls_Beton.TabClasseCiment(Me.cmb_ClassCiment.SelectedIndex)
        'MAJI_ProprietesBeton()
        MAJI_Coefficients()

    End Sub

    Private Sub cmb_RH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_RH.SelectedIndexChanged

        If lBuild Then Exit Sub
        MAJI_Coefficients()

    End Sub

    Private Sub cmb_PsiL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Charge.SelectedIndexChanged

        If lBuild Then Exit Sub

        iCharge = Me.cmb_Charge.SelectedIndex
        If ((iCharge = 2) And (iChargePrec <> 2)) _
        Or ((iCharge <> 2) And (iChargePrec = 2)) Then
            MAJI_AgeT0()
        End If

        MAJI_Coefficients()
        iChargePrec = iCharge

    End Sub

    Private Sub MAJI_AgeT0()
        lBuild = True

        If iCharge = 2 Then

            AgeT0 = AgeT0_SH

        Else

            AgeT0 = AgeT0_Normal

        End If
        MAJI_TxtAgeT0()
        AfficherAgeT0()

        lBuild = False
    End Sub
    Private Sub MAJI_TxtAgeT0()

        PrepareTextBoxDipo(Me.txt_AgeT0, Not (iCharge = 2))

    End Sub

    Private Sub txt_Age_TextChanged(sender As Object, e As EventArgs) Handles txt_AgeT.TextChanged, txt_AgeT0.TextChanged, txt_kE.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal
        Dim lBeton As Boolean = False

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_kE.Name
                    MonBeton.kE = Valeur
                    lBeton = True
                Case Me.txt_AgeT.Name
                    AgeT = Valeur
                Case Me.txt_AgeT0.Name
                    AgeT0 = Valeur
                    AgeT0_Normal = Valeur
            End Select
        End If
        If lBeton Then MAJI_ProprietesBeton()
        MAJI_Coefficients()
    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal

        Select Case MyTxt.Name

            Case Me.txt_AgeT.Name
                ValMin = AgeT0 + 1
                lValMax = False
                kUnit = 1

            Case Me.txt_AgeT0.Name
                ValMin = 1
                ValMax = AgeT - 1
                lValMax = True
                kUnit = 1

            Case Me.txt_H0.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
                ValMin = 0.02 / kUnit
                lValMax = False

            Case Me.txt_kE.Name

                kUnit = 1
                ValMin = 5000
                ValMax = 13000
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

    Private Sub txt_H0_TextChanged(sender As Object, e As EventArgs) Handles txt_H0.TextChanged

        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_H0.Name
                    RayonH0 = Valeur

            End Select
        End If

        MAJI_Coefficients()
    End Sub


#End Region

#Region " Dessin des symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_PsiL2.Paint, img_RH.Paint, img_H0.Paint,
        img_AgeT0.Paint, img_AgeT.Paint, img_Fcm.Paint, img_Fck.Paint, img_Ecm.Paint, img_nL.Paint, img_n0.Paint, img_kE.Paint

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

            Case Me.img_kE.Name
                strSymbol = "k"
                strIndice = "E"

            Case Me.img_RH.Name
                strSymbol = "RH"
                strIndice = ""

            Case Me.img_PsiL2.Name
                strSymbol = "y"
                strIndice = "L"
                lGrec = True

            Case Me.img_AgeT.Name
                strSymbol = "t"
                strIndice = ""

            Case Me.img_AgeT0.Name
                strSymbol = "t"
                strIndice = "0"

            Case Me.img_H0.Name
                strSymbol = "h"
                If lCalculG1 Then strIndice = "0" Else strIndice = "n"

            Case Me.img_Ecm.Name
                strSymbol = "E"
                strIndice = "cm"

            Case Me.img_Fck.Name
                strSymbol = "f"
                strIndice = "ck"

            Case Me.img_Fcm.Name
                strSymbol = "f"
                strIndice = "cm"

            'Case Me.img_Fctm.Name
            '    strSymbol = "f"
            '    strIndice = "ctm"

            Case Me.img_n0.Name
                strSymbol = "n"
                strIndice = "0"

            Case Me.img_nL.Name
                strSymbol = "n"
                strIndice = "L"


        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region

End Class