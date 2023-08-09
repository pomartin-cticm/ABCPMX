Public Class Frm_OptionsCalculScope

#Region " Variables locales "


    Const BALISE As String = "OPTCALSCOPE"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_OptionsCalculScope_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


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

            Me.lbl_DefinitionPoutre.Text = MyBloc("BEAMDEF")
            Me.lbl_PorteeMini.Text = MyBloc("SPANLENGTHMIN")
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

        If Not LogicielOptions.lExpert Then
            Me.txt_PorteeMini.Enabled = False
            Me.txt_PorteeMini.BackColor = CouleurReadOnly
        Else

        End If

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitA1.Text = "°"
    End Sub

    Private Sub AfficherScopeEnCours()

        '--> Portée

        Me.txt_PorteeMini.Text = GetStringInUnit(LocalOptionsScope.PorteeMin, Enu_TypeVariable.Longueur, 4, 2, False)


        '--> Angle inclinaison des renformis

        Me.txt_ThetaH.Text = GetStringInUnit(LocalOptionsScope.ThetaH, Enu_TypeVariable.SansType, 4, 2, False)


    End Sub

#End Region

#Region " Evènements saisie "


    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_PorteeMini.TextChanged, txt_ThetaH.TextChanged

        If lBuild Then Exit Sub
        Dim lPortees As Boolean = False
        Dim lCoupe As Boolean = False

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case Me.txt_PorteeMini.Name
                    LocalOptionsScope.PorteeMin = ValeurUI
                Case Me.txt_ThetaH.Name
                    LocalOptionsScope.ThetaH = ValeurUI
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

        Const PORTEEMINMIN As Decimal = 2
        Const PORTEEMINMAX As Decimal = 5
        Const ANGLEMIN As Decimal = 0
        Const ANGLEMAX As Decimal = 45

        Select Case MyTxt.Name
            Case Me.txt_PorteeMini.Name

                ValMin = PORTEEMINMIN / kUnit
                ValMax = PORTEEMINMAX / kUnit

            Case Me.txt_PorteeMini.Name

                ValMin = ANGLEMIN
                ValMax = ANGLEMAX

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




End Class