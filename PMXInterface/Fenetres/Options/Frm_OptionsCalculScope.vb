Public Class Frm_OptionsCalculScope

#Region " Variables locales "


    Const BALISE As String = "OPTCALSCOPE"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean


#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherScopeEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Scope.Text = MyBloc("TITLE")

            '#-------------------- GEOMETRIE SELPOUTRE

            Me.lbl_DefinitionPoutre.Text = MyBloc("BEAMDEF")

            Me.lbl_SpanL.Text = MyBloc("SPANLENGTH")
            Me.lbl_PorteeConsole.Text = MyBloc("CANTILEVERSPAN")

            '#-------------------- DALLE

            Me.lbl_Dalle.Text = MyBloc("SLAB")
            Me.lbl_ThetaRd.Text = MyBloc("THETAHAUNCH")
            Me.lbl_Renformis.Text = MyBloc("HAUNCHTH")
            Me.lbl_EpDallePleine.Text = MyBloc("SOLIDSLABTH")
            Me.lbl_EpDalleMixte.Text = MyBloc("COMPOSITESLABTH")

            '#-------------------- MATERIAU

            Me.lbl_Materiau.Text = MyBloc("CONCRETEPROP")
            Me.lbl_RhoBetonLeger.Text = MyBloc("RHOLWC")

            '#-------------------- DIMENSIONS D'UN PRS

            Me.lbl_PRS.Text = MyBloc("WELDEDSECTION")

            Me.lbl_EpAme.Text = MyBloc("WEBTHICKNESS")
            Me.lbl_HauteurAme.Text = MyBloc("WEBHEIGHT")

            Me.lbl_EpSemelles.Text = MyBloc("FLANGETHICKNESS")
            Me.lbl_LargeurSemelles.Text = MyBloc("FLANGEWIDTH")

            Me.lbl_RapportAf.Text = MyBloc("RATIOFLANGEAREA")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Scope.Dock = DockStyle.Fill

        Me.lbl_Scope.BackColor = CouleurBackBandeaux
        Me.lbl_Scope.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_PorteeMini, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_PorteeMaxi, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_PorteeConsoleMin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_RatioConsoleMax, LogicielOptions.lExpert)

        PrepareTextBoxDipo(Me.txt_EpDalleMin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_EpDalleMixteMin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_RatioEpReformis, LogicielOptions.lExpert)

        PrepareTextBoxDipo(Me.txt_RhoC_LWC_Min, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_RhoC_LWC_Max, LogicielOptions.lExpert)

        PrepareTextBoxDipo(Me.txt_HwMin, False)
        PrepareTextBoxDipo(Me.txt_HwMax, False)
        PrepareTextBoxDipo(Me.txt_TwMin, False)
        PrepareTextBoxDipo(Me.txt_TwMax, False)

        PrepareTextBoxDipo(Me.txt_BfMin, False)
        PrepareTextBoxDipo(Me.txt_BfMax, False)
        PrepareTextBoxDipo(Me.txt_TfMin, False)
        PrepareTextBoxDipo(Me.txt_TfMax, False)

        PrepareTextBoxDipo(Me.txt_RapAfMax, False)
        PrepareTextBoxDipo(Me.txt_RapAfMin, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD10.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitA1.Text = "°"
        Me.etq_UnitMassV1.Text = "kg/m3"
        Me.etq_UnitMassV2.Text = "kg/m3"

    End Sub

    Private Sub AfficherScopeEnCours()

        '--> Portées

        Me.txt_PorteeMini.Text = GetStringInUnit(LocalOptionsScope.PorteeMin, Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_PorteeMaxi.Text = GetStringInUnit(LocalOptionsScope.PorteeMax, Enu_TypeVariable.Longueur, 4, 2, False)

        Me.txt_PorteeConsoleMin.Text = GetStringInUnit(LocalOptionsScope.PorteeConsoleMin, Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_RatioConsoleMax.Text = GetStringInUnit(LocalOptionsScope.RatioPorteeConsoleMax, Enu_TypeVariable.SansType, 4, 2, False)

        '--> Angle inclinaison des renformis

        Me.txt_ThetaH.Text = GetStringInUnit(LocalOptionsScope.ThetaH, Enu_TypeVariable.SansType, 4, 2, False)

        '--> Epaisseurs de dalle

        Me.txt_EpDalleMin.Text = GetStringInUnit(LocalOptionsScope.EpDallePleineMin, Enu_TypeVariable.Dimension, 4, 2, False)
        Me.txt_EpDalleMixteMin.Text = GetStringInUnit(LocalOptionsScope.EpDalleMixteMin, Enu_TypeVariable.Dimension, 4, 2, False)
        Me.txt_RatioEpReformis.Text = GetStringInUnit(LocalOptionsScope.RatioEpRenformisMax, Enu_TypeVariable.SansType, 4, 2, False)

        '--> Matériau

        Me.txt_RhoC_LWC_Max.Text = GetStringInUnit(LocalOptionsScope.RhoCBetonLegerMax, Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_RhoC_LWC_Min.Text = GetStringInUnit(LocalOptionsScope.RhoCBetonLegerMin, Enu_TypeVariable.SansType, 4, 2, False)

        '--( Dimensions d'un PRS

        If LogicielReglages.lPRS Then
            Me.txt_HwMin.Text = GetStringInUnitN(LocalOptionsScope.HwMin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            Me.txt_HwMax.Text = GetStringInUnitN(LocalOptionsScope.HwMax, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            Me.txt_TwMin.Text = GetStringInUnitN(LocalOptionsScope.TwMin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            Me.txt_TwMax.Text = GetStringInUnitN(LocalOptionsScope.TwMax, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)

            Me.txt_BfMin.Text = GetStringInUnitN(LocalOptionsScope.BfMin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            Me.txt_BfMax.Text = GetStringInUnitN(LocalOptionsScope.BfMax, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            Me.txt_TfMin.Text = GetStringInUnitN(LocalOptionsScope.TfMin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            Me.txt_TfMax.Text = GetStringInUnitN(LocalOptionsScope.TfMax, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)

            Me.txt_RapAfMax.Text = GetStringInUnitN(LocalOptionsScope.RapportAfMax, Enu_TypeVariable.SansType, 4, 3, NON_U, True)
            Me.txt_RapAfMin.Text = GetStringInUnitN(LocalOptionsScope.RapportAfMin, Enu_TypeVariable.SansType, 4, 3, NON_U, True)
        Else
            Dim DeltaZ As Single = Me.pan_PRS.Top - Me.pan_Dalle.Top
            Me.pan_Dalle.Top = Me.pan_PRS.Top
            Me.pan_PRS.Visible = False
            Me.pan_Materiau.Top += DeltaZ
            Me.TLpan_Conteneur.Height += (DeltaZ)
        End If

    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_PorteeMini.TextChanged, txt_ThetaH.TextChanged,
        txt_PorteeMaxi.TextChanged, txt_PorteeConsoleMin.TextChanged, txt_RatioConsoleMax.TextChanged, txt_EpDalleMin.TextChanged, txt_RatioEpReformis.TextChanged, txt_EpDalleMixteMin.TextChanged, txt_RhoC_LWC_Min.TextChanged, txt_RhoC_LWC_Max.TextChanged

        If lBuild Then Exit Sub
        Dim lPortees As Boolean = False
        Dim lCoupe As Boolean = False

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case Me.txt_RhoC_LWC_Min.Name
                    LocalOptionsScope.RhoCBetonLegerMin = ValeurUI
                Case Me.txt_RhoC_LWC_Max.Name
                    LocalOptionsScope.RhoCBetonLegerMax = ValeurUI
                Case Me.txt_PorteeConsoleMin.Name
                    LocalOptionsScope.PorteeConsoleMin = ValeurUI
                Case Me.txt_PorteeMini.Name
                    LocalOptionsScope.PorteeMin = ValeurUI
                Case Me.txt_PorteeMaxi.Name
                    LocalOptionsScope.PorteeMax = ValeurUI
                Case Me.txt_ThetaH.Name
                    LocalOptionsScope.ThetaH = ValeurUI
                Case Me.txt_RatioConsoleMax.Name
                    LocalOptionsScope.RatioPorteeConsoleMax = ValeurUI
                Case Me.txt_EpDalleMin.Name
                    LocalOptionsScope.EpDallePleineMin = ValeurUI
                Case Me.txt_RatioEpReformis.Name
                    LocalOptionsScope.RatioEpRenformisMax = ValeurUI
                Case Me.txt_EpDalleMixteMin.Name
                    LocalOptionsScope.EpDalleMixteMin = ValeurUI
            End Select

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Const PORTEECONSOLEMINMIN As Decimal = 0.2
        Const PORTEECONSOLEMINMAX As Decimal = 0.5

        Const PORTEEMINMIN As Decimal = 2
        Const PORTEEMINMAX As Decimal = 5
        'Const PORTEEMAXMIN As Decimal = 10
        'Const PORTEEMAXMAX As Decimal = 100
        'Const ANGLEMIN As Decimal = 0
        'Const ANGLEMAX As Decimal = 45

        Select Case MyTxt.Name
            Case Me.txt_PorteeConsoleMin.Name

                ValMin = PORTEECONSOLEMINMIN / kUnit
                ValMax = PORTEECONSOLEMINMAX / kUnit
                lValMax = False

            Case Me.txt_PorteeMini.Name

                ValMin = PORTEEMINMIN / kUnit
                ValMax = PORTEEMINMAX / kUnit

            Case Me.txt_RhoC_LWC_Max.Name, Me.txt_RhoC_LWC_Min.Name

                ValMin = 0
                ValMax = 3000
                lValMax = False
                kUnit = 1

            Case Me.txt_RatioConsoleMax.Name

                ValMin = 0.1
                ValMax = 1
                kUnit = 1

            Case Me.txt_EpDalleMin.Name, Me.txt_EpDalleMixteMin.Name

                ValMin = 0.04
                ValMax = 1
                lValMax = False
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

            Case Me.txt_RatioEpReformis.Name

                ValMin = 0.1
                ValMax = 1
                kUnit = 1

            Case Me.txt_ThetaH.Name

                ValMin = 0
                ValMax = 45
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

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_ThetaRd.Paint, img_PorteeMini.Paint, img_PorteeConsoleMin.Paint,
        img_PorteeL2.Paint, img_Td1.Paint, img_xTd.Paint, img_Th.Paint, img_EpDalleMixte.Paint, img_RhoC.Paint,
        img_Tw.Paint, img_Tf.Paint, img_hw.Paint, img_Bf.Paint, img_Aft.Paint, img_Afb.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim AlignH As Enu_AlignementH

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = False
        AlignH = Enu_AlignementH.Centre

        Select Case sender.name

            Case Me.img_Afb.Name
                strSymbol = "A"
                strIndice = "fb"
                AlignH = Enu_AlignementH.Droite
            Case Me.img_Aft.Name
                strSymbol = "A"
                strIndice = "ft"
                AlignH = Enu_AlignementH.Gauche

            Case Me.img_Tw.Name
                strSymbol = "t"
                strIndice = "w"
            Case Me.img_hw.Name
                strSymbol = "h"
                strIndice = "w"
            Case Me.img_Tf.Name
                strSymbol = "t"
                strIndice = "f"
            Case Me.img_Bf.Name
                strSymbol = "b"
                strIndice = "f"


            Case Me.img_EpDalleMixte.Name
                strSymbol = "t"
                strIndice = "c"

            Case Me.img_PorteeMini.Name
                strSymbol = "L"
                strIndice = ""

            Case Me.img_PorteeL2.Name
                strSymbol = "x L"
                strIndice = ""
                AlignH = Enu_AlignementH.Gauche

            Case Me.img_ThetaRd.Name
                strSymbol = "q"
                strIndice = "h"
                lGrec = True
                lIndice = False
                AlignH = Enu_AlignementH.Droite
                lEgal = True
            Case Me.img_PorteeConsoleMin.Name
                strSymbol = "L"
                strIndice = "c"

            Case Me.img_Td1.Name
                strSymbol = "t"
                strIndice = "d"

            Case Me.img_xTd.Name
                strSymbol = "x t"
                strIndice = "d"
                AlignH = Enu_AlignementH.Gauche

            Case Me.img_Th.Name
                strSymbol = "t"
                strIndice = "h"

            Case Me.img_RhoC.Name
                strSymbol = "r"
                strIndice = "c"
                lGrec = True
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region

End Class