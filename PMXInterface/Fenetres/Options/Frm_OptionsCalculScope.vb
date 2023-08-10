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

            '#--------------------

            Me.lbl_DefinitionPoutre.Text = MyBloc("BEAMDEF")

            Me.lbl_SpanL.Text = MyBloc("SPANLENGTH")
            Me.lbl_PorteeConsole.Text = MyBloc("CANTILEVERSPAN")

            '#--------------------

            Me.lbl_Dalle.Text = MyBloc("SLAB")
            Me.lbl_ThetaRd.Text = MyBloc("THETAHAUNCH")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Scope.Dock = DockStyle.Fill

        Me.lbl_Scope.BackColor = CouleurBackBandeaux
        Me.lbl_Scope.ForeColor = CouleurForeBandeaux

        PrepareTextBoxExpert(Me.txt_PorteeMini, LogicielOptions.lExpert)
        PrepareTextBoxExpert(Me.txt_PorteeMaxi, LogicielOptions.lExpert)
        PrepareTextBoxExpert(Me.txt_PorteeConsoleMin, LogicielOptions.lExpert)
        PrepareTextBoxExpert(Me.txt_RatioConsoleMax, LogicielOptions.lExpert)

        PrepareTextBoxExpert(Me.txt_EpDalleMin, LogicielOptions.lExpert)

    End Sub

    Private Sub PrepareTextBoxExpert(ByRef MyTxt As TextBox, lExpert As Boolean)
        '------------------------------------------------------------------------------------
        '   10/08/23 :  Création - POM
        '------------------------------------------------------------------------------------
        '   Préparation de l'état d'un textbox en fonction du mode expert (bloqué en mode normal)
        '------------------------------------------------------------------------------------
        '   MyTxt       [E] :   Textbox à préparer
        '   lExpert     [E] :   Indicateur mode expert
        '------------------------------------------------------------------------------------

        If lExpert Then
            MyTxt.Enabled = True
            MyTxt.BackColor = SystemColors.Window
        Else
            MyTxt.Enabled = False
            MyTxt.BackColor = CouleurReadOnly
        End If

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitA1.Text = "°"
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

    End Sub

#End Region

#Region " Evènements saisie "


    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_PorteeMini.TextChanged, txt_ThetaH.TextChanged,
        txt_PorteeMaxi.TextChanged, txt_PorteeConsoleMin.TextChanged, txt_RatioConsoleMax.TextChanged, txt_EpDalleMin.TextChanged

        If lBuild Then Exit Sub
        Dim lPortees As Boolean = False
        Dim lCoupe As Boolean = False

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
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
            End Select

        End If


    End Sub


    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Const PORTEECONSOLEMINMIN As Decimal = 0.2
        Const PORTEECONSOLEMINMAX As Decimal = 0.5

        Const PORTEEMINMIN As Decimal = 2
        Const PORTEEMINMAX As Decimal = 5
        Const PORTEEMAXMIN As Decimal = 10
        Const PORTEEMAXMAX As Decimal = 100
        Const ANGLEMIN As Decimal = 0
        Const ANGLEMAX As Decimal = 45

        Select Case MyTxt.Name
            Case Me.txt_PorteeConsoleMin.Name

                ValMin = PORTEECONSOLEMINMIN / kUnit
                ValMax = PORTEECONSOLEMINMAX / kUnit
                lValMax = False

            Case Me.txt_PorteeMini.Name

                ValMin = PORTEEMINMIN / kUnit
                ValMax = PORTEEMINMAX / kUnit

            Case Me.txt_PorteeMini.Name

                ValMin = PORTEEMAXMIN / kUnit
                ValMax = PORTEEMAXMAX / kUnit

            Case Me.txt_PorteeMini.Name

                ValMin = ANGLEMIN
                ValMax = ANGLEMAX

            Case Me.txt_RatioConsoleMax.Name

                ValMin = 0.1
                ValMax = 1

            Case Me.txt_EpDalleMin.Name

                ValMin = 0.04
                ValMax = 1
                lValMax = False

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            'ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region


#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_ThetaRd.Paint, img_PorteeMini.Paint, img_PorteeConsoleMin.Paint, img_PorteeL2.Paint, img_Td1.Paint

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

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region


End Class