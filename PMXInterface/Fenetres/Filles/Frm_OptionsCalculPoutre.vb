Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsCalculPoutre


#Region " Variables "

    Dim lBuild As Boolean
    Dim MyParam As cls_OptionsCalcul
    ' Dim ArmaYoung As Decimal

    Dim tabNorme(1) As String
    Dim SymbolJour As String

    Const kUnitEpsilon As Decimal = 10 ^ -6

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_OptionsCalculPoutre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        InitialisationsFenetre()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()

        lBuild = False
    End Sub

    Private Sub InitialisationsFenetre()
        cls_OptionsCalcul.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Param, MyParam)
        RemplirComboStandard()
        RemplirComboRH()
        RemplirComboG()
        RemplirComboWk()
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_BEAMOPTIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("OK")

                Me.lbl_CadreNorm.Text = Bloc("TSTANDARD")
                Me.lbl_Norme.Text = Bloc("TSTANDARD")
                tabNorme(0) = Bloc("ENGEN1")
                tabNorme(1) = Bloc("ENGEN2")

                Me.lbl_CadreELU.Text = Bloc("TELUOPTIONS")
                Me.rdb_NormalDesign.Text = Bloc("NORMALDESIGN")
                Me.rdb_ElasticDesignVM.Text = Bloc("ELASTICDESIGNVM")
                Me.rdb_ElasticDesignClasse3.Text = Bloc("ELASTICDESIGNCL3")
                Me.lbl_eta.Text = Bloc("ETAW")

                Me.lbl_CadreELS.Text = Bloc("TELSOPTIONS")
                Me.lbl_CombinationVibration.Text = Bloc("COMBINATIONFREQ")
                Me.chk_FlechesETA.Text = Bloc("ETADEFLECTIONS")
                Me.lbl_StudDeflection.Text = Bloc("STUDSE")
                Me.chk_MaitriseFissuration.Text = Bloc("CONTROLCRACKW")
                Me.lbl_LargeurFissure.Text = Bloc("CRACKWIDTH")

                Me.lbl_CadreBeton.Text = Bloc("TCONCRETE")
                Me.lbl_BetonMessage.Text = Bloc("CONCRETEMSG")
                Me.lbl_RH.Text = Bloc("RELATIVEHUMIDITY")
                Me.lbl_Shrinkage.Text = Bloc("SHRINKAGEDEFORMATION")
                Me.chk_RetraitEnrobage.Text = Bloc("SHRINKAGETOENCASEMENT")
                Me.lbl_ArmaYoung.Text = Bloc("YOUNGSMODULUSREBAR")

                Me.lbl_AgeT.Text = Bloc("AGET")
                Me.lbl_TimeT0.Text = Bloc("AGET0")
                SymbolJour = Bloc("SYMBOLFORDAY")
                Me.lbl_Dalle.Text = Bloc("SLAB")
                Me.lbl_Enrobage.Text = Bloc("ENCASEMENT")
                Me.lbl_G1.Text = Bloc("SELFWEIGHT")
                Me.lbl_G2.Text = Bloc("OTHERPERM")
                Me.lbl_SH.Text = Bloc("SHRINKAGE")

                Me.lbl_CadreSections.Text = Bloc("SECTIONSPROP")
                Me.chk_ArmaComprimees.Text = Bloc("REBARSINCOMPRESSION")
                Me.chk_LargeursPartipantesSimples.Text = Bloc("SIMPLIFIEDBEFF")

                Me.lbl_CadreParametres.Text = Bloc("TPARAMETERS")
                Me.lbl_GraviteG.Text = Bloc("GFORCE")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub RemplirComboG()
        Me.cmb_GraviteG.Items.Clear()

        For i = 0 To cls_OptionsCalcul.tabGraviteG.GetUpperBound(0)
            Me.cmb_GraviteG.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabGraviteG(i), Enu_TypeVariable.SansType, 3, 2, False))
        Next

    End Sub

    Private Sub RemplirComboStandard()
        Me.cmb_Norme.Items.Clear()
        Me.cmb_Norme.Items.Add(Me.tabNorme(0))

        If LogicielOptions.lExpert Then
            Me.cmb_Norme.Items.Add(Me.tabNorme(1))
        End If

    End Sub

    Private Sub RemplirComboRH()

        Me.cmb_RH.Items.Clear()

        For i = 0 To cls_OptionsCalcul.tabRH.GetUpperBound(0)
            Me.cmb_RH.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabRH(i), Enu_TypeVariable.SansType, 2, 0, False) & "%")
        Next

    End Sub

    Private Sub RemplirComboWk()

        Me.cmb_Wk.Items.Clear()
        For i = 0 To cls_OptionsCalcul.tabWk.GetUpperBound(0)
            Me.cmb_Wk.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabWk(i), Enu_TypeVariable.SansType, 2, 1, False))
        Next

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitJour1.Text = SymbolJour
        Me.etq_UnitJour2.Text = SymbolJour
        Me.etq_UnitJour3.Text = SymbolJour
        Me.etq_UnitJour4.Text = SymbolJour
        Me.etq_UnitJour5.Text = SymbolJour
        Me.etq_UnitJour6.Text = SymbolJour
        Me.etq_UnitJour7.Text = SymbolJour

        Me.etq_UnitG.Text = "m/s2"

        Me.etq_UnitDimension1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitLargeurF.Text = "mm"
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_CadreELU.BackColor = CouleurBackBandeaux
        Me.lbl_CadreELU.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreELS.BackColor = CouleurBackBandeaux
        Me.lbl_CadreELS.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreNorm.BackColor = CouleurBackBandeaux
        Me.lbl_CadreNorm.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreBeton.BackColor = CouleurBackBandeaux
        Me.lbl_CadreBeton.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreSections.BackColor = CouleurBackBandeaux
        Me.lbl_CadreSections.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreParametres.BackColor = CouleurBackBandeaux
        Me.lbl_CadreParametres.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_t0SHDalle, False)
        PrepareTextBoxDipo(Me.txt_t0SHEnrob, False)

    End Sub

    Private Sub AfficherPoutreEnCours()

        '==> Norme

        Select Case MyParam.Norme
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG1
                Me.cmb_Norme.SelectedIndex = 0
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG2
                Me.cmb_Norme.SelectedIndex = Math.Min(1, Me.cmb_Norme.Items.Count - 1)
        End Select

        '==> Options ELU

        If MyParam.lElasticDesignVM Then
            Me.rdb_ElasticDesignVM.Checked = True
        ElseIf MyParam.lElasticDesignCl3 Then
            Me.rdb_ElasticDesignClasse3.Checked = True
        Else
            Me.rdb_NormalDesign.Checked = True
        End If

        Me.txt_eta.Text = GetStringInUnit(MyParam.EtaW, Enu_TypeVariable.SansType, 2, 1, False)

        '==> Options ELS

        Me.chk_FlechesETA.Checked = MyParam.lFlechesETA
        Me.txt_Se.Text = GetStringInUnit(MyParam.DeltaD, Enu_TypeVariable.Dimension, 4, 3, False)

        '# maitrise de la fissuration
        Me.chk_MaitriseFissuration.Checked = MyParam.lMaitriseFissuration
        Me.cmb_Wk.SelectedIndex = Array.IndexOf(cls_OptionsCalcul.tabWk, MyParam.FissureWk)

        '==> Béton

        Me.cmb_RH.SelectedIndex = Array.IndexOf(cls_OptionsCalcul.tabRH, MyParam.RH)
        Me.txt_EpsilonSh.Text = GetStringInUnit(MyParam.EpsilonSH / kUnitEpsilon, Enu_TypeVariable.SansType, 3, 0, False)
        Me.chk_RetraitEnrobage.Checked = MyParam.lRetraitEnrobage
        Me.txt_Es.Text = GetStringInUnit(MyParam.ArmaYoung, Enu_TypeVariable.ModuleY, 3, 1, False)

        Me.txt_AgeT.Text = GetStringInUnit(MyParam.AgeT, Enu_TypeVariable.SansType, 4, 0, False)
        Me.txt_t0G1Dalle.Text = GetStringInUnit(MyParam.AgeT0G1(0), Enu_TypeVariable.SansType, 4, 0, False)
        Me.txt_t0G2Dalle.Text = GetStringInUnit(MyParam.AgeT0G2(0), Enu_TypeVariable.SansType, 4, 0, False)
        Me.txt_t0SHDalle.Text = GetStringInUnit(MyParam.AgeT0SH(0), Enu_TypeVariable.SansType, 4, 0, False)
        Me.txt_t0G1Enrob.Text = GetStringInUnit(MyParam.AgeT0G1(1), Enu_TypeVariable.SansType, 4, 0, False)
        Me.txt_t0G2Enrob.Text = GetStringInUnit(MyParam.AgeT0G2(1), Enu_TypeVariable.SansType, 4, 0, False)
        Me.txt_t0SHEnrob.Text = GetStringInUnit(MyParam.AgeT0SH(1), Enu_TypeVariable.SansType, 4, 0, False)

        '==> Propriétés sections

        Me.chk_LargeursPartipantesSimples.Checked = MyParam.lLargeurEfficaceSimplifiee
        Me.chk_ArmaComprimees.Checked = MyParam.lCompressionArma

        '==> Paramètres

        Me.cmb_GraviteG.SelectedIndex = Array.IndexOf(cls_OptionsCalcul.tabGraviteG, MyParam.GraviteG)


    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        GereTransfertValeur(MyParam.lElasticDesignVM, MyProjet.Poutres(MyProjet.IndEnCours).Param.lElasticDesignVM, lModif)
        GereTransfertValeur(MyParam.lElasticDesignCl3, MyProjet.Poutres(MyProjet.IndEnCours).Param.lElasticDesignCl3, lModif)
        GereTransfertValeur(MyParam.EtaW, MyProjet.Poutres(MyProjet.IndEnCours).Param.EtaW, lModif)

        GereTransfertValeur(MyParam.RH, MyProjet.Poutres(MyProjet.IndEnCours).Param.RH, lModif)
        GereTransfertValeur(MyParam.EpsilonSH, MyProjet.Poutres(MyProjet.IndEnCours).Param.EpsilonSH, lModif)
        GereTransfertValeur(MyParam.ArmaYoung, MyProjet.Poutres(MyProjet.IndEnCours).Param.ArmaYoung, lModif)
        GereTransfertValeur(MyParam.lRetraitEnrobage, MyProjet.Poutres(MyProjet.IndEnCours).Param.lRetraitEnrobage, lModif)
        GereTransfertValeur(MyParam.AgeT, MyProjet.Poutres(MyProjet.IndEnCours).Param.AgeT, lModif)
        GereTransfertValeur(MyParam.AgeT0G1(0), MyProjet.Poutres(MyProjet.IndEnCours).Param.AgeT0G1(0), lModif)
        GereTransfertValeur(MyParam.AgeT0G1(1), MyProjet.Poutres(MyProjet.IndEnCours).Param.AgeT0G1(1), lModif)
        GereTransfertValeur(MyParam.AgeT0G2(0), MyProjet.Poutres(MyProjet.IndEnCours).Param.AgeT0G2(0), lModif)
        GereTransfertValeur(MyParam.AgeT0G2(1), MyProjet.Poutres(MyProjet.IndEnCours).Param.AgeT0G2(1), lModif)

        GereTransfertValeur(MyParam.lLargeurEfficaceSimplifiee, MyProjet.Poutres(MyProjet.IndEnCours).Param.lLargeurEfficaceSimplifiee, lModif)
        GereTransfertValeur(MyParam.lCompressionArma, MyProjet.Poutres(MyProjet.IndEnCours).Param.lCompressionArma, lModif)

        GereTransfertValeur(MyParam.GraviteG, MyProjet.Poutres(MyProjet.IndEnCours).Param.GraviteG, lModif)

        GereTransfertValeur(Me.chk_FlechesETA.Checked, MyProjet.Poutres(MyProjet.IndEnCours).Param.lFlechesETA, lModif)
        GereTransfertValeur(MyParam.DeltaD, MyProjet.Poutres(MyProjet.IndEnCours).Param.DeltaD, lModif)

        GereTransfertValeur(Me.chk_MaitriseFissuration.Checked, MyProjet.Poutres(MyProjet.IndEnCours).Param.lMaitriseFissuration, lModif)
        GereTransfertValeur(cls_OptionsCalcul.tabWk(Me.cmb_Wk.SelectedIndex), MyProjet.Poutres(MyProjet.IndEnCours).Param.FissureWk, lModif)

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub

#End Region

#Region " Evènements "

    Private Sub ULSSectionDesign_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdb_NormalDesign.CheckedChanged, rdb_ElasticDesignVM.CheckedChanged, rdb_ElasticDesignClasse3.CheckedChanged

        MyParam.lElasticDesignVM = Me.rdb_ElasticDesignVM.Checked
        MyParam.lElasticDesignCl3 = Me.rdb_ElasticDesignClasse3.Checked

    End Sub

    Private Sub cmb_GraviteG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_GraviteG.SelectedIndexChanged
        MyParam.GraviteG = cls_OptionsCalcul.tabGraviteG(Me.cmb_GraviteG.SelectedIndex)
    End Sub

    Private Sub cmb_RH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_RH.SelectedIndexChanged
        MyParam.RH = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)
    End Sub

    Private Sub chk_RetraitEnrobage_CheckedChanged(sender As Object, e As EventArgs) Handles chk_RetraitEnrobage.CheckedChanged
        MyParam.lRetraitEnrobage = Me.chk_RetraitEnrobage.Checked
    End Sub

    Private Sub chk_LargeursPartipantesSimples_CheckedChanged(sender As Object, e As EventArgs) Handles chk_LargeursPartipantesSimples.CheckedChanged
        MyParam.lLargeurEfficaceSimplifiee = Me.chk_LargeursPartipantesSimples.Checked
    End Sub

    Private Sub chk_ArmaComprimees_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ArmaComprimees.CheckedChanged
        MyParam.lCompressionArma = Me.chk_ArmaComprimees.Checked
    End Sub

    Private Sub txt_EpsilonSh_TextChanged(sender As Object, e As EventArgs) Handles txt_EpsilonSh.TextChanged, txt_Es.TextChanged, txt_eta.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_EpsilonSh.Name
                    MyParam.EpsilonSH = Valeur
                Case Me.txt_Es.Name
                    MyParam.ArmaYoung = Valeur
                Case Me.txt_eta.Name
                    MyParam.EtaW = Valeur
            End Select
        End If
    End Sub

    Private Sub txt_Se_TextChanged(sender As Object, e As EventArgs) Handles txt_Se.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            MyParam.DeltaD = Valeur
        End If

    End Sub

    Private Sub ChangeAgeT(sender As Object, e As EventArgs) Handles txt_t0G2Enrob.TextChanged, txt_t0G2Dalle.TextChanged, txt_t0G1Enrob.TextChanged, txt_t0G1Dalle.TextChanged, txt_AgeT.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_AgeT.Name
                    MyParam.AgeT = Valeur
                Case Me.txt_t0G1Dalle.Name
                    MyParam.AgeT0G1(0) = Valeur
                Case Me.txt_t0G2Dalle.Name
                    MyParam.AgeT0G2(0) = Valeur
                Case Me.txt_t0G1Enrob.Name
                    MyParam.AgeT0G1(1) = Valeur
                Case Me.txt_t0G2Enrob.Name
                    MyParam.AgeT0G2(1) = Valeur
            End Select
        End If
    End Sub
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name

            Case Me.txt_EpsilonSh.Name
                ValMin = 0
                lValMax = False
                kUnit = kUnitEpsilon
            Case Me.txt_Es.Name
                kUnit = LogicielInfo.Transfert_ModulesY(LogicielOptions.IndUnitModulesY)
                ValMin = 190000 / kUnit
                ValMax = 210000 / kUnit
                lValMax = True

            Case Me.txt_AgeT.Name
                lValMax = False
                ValMin = 365
                kUnit = 1
            Case Me.txt_t0G1Dalle.Name, Me.txt_t0G2Dalle.Name, Me.txt_t0G1Enrob.Name, Me.txt_t0G2Enrob.Name
                lValMax = True
                ValMin = 28
                ValMax = 200
                kUnit = 1

            Case Me.txt_Se.Name
                ValMin = 0.00005 / kUnit
                ValMax = 0.005 / kUnit
                lValMax = True

            Case Me.txt_eta.Name
                ValMin = 1
                ValMax = 1.2
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

#End Region

#Region " Dessin des symboles "

    Private Sub DrawSymbols(sender As Object, e As PaintEventArgs) Handles img_RH.Paint, img_EpsilonSh.Paint, img_Es.Paint, img_T0SH.Paint, img_T0G2.Paint, img_T0G1.Paint, img_AgeT.Paint, img_G.Paint, img_se.Paint, img_eta.Paint, img_Wk.Paint
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

            Case Me.cmb_Wk.Name
                strSymbol = "w"
                strIndice = "k"

            Case Me.img_RH.Name
                strSymbol = "RH"
                strIndice = ""

            Case Me.img_Es.Name
                strSymbol = "E"
                strIndice = "s"

            Case Me.img_EpsilonSh.Name
                strSymbol = "e"
                strIndice = "sh"
                lGrec = True

            Case Me.img_G.Name
                strSymbol = "g"
                strIndice = ""

            Case Me.img_AgeT.Name
                strSymbol = "t"
                strIndice = ""

            Case Me.img_T0G1.Name, Me.img_T0G2.Name, Me.img_T0SH.Name
                strSymbol = "t"
                strIndice = "0"

            Case Me.img_se.Name
                strSymbol = "s"
                strIndice = "e"

            Case Me.img_eta.Name
                strSymbol = "h"
                strIndice = ""
                lGrec = True

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region

End Class