Public Class Frm_GammaM_Acier

#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre()

        lBuild = True

        PrepareFenetre()
        AfficherGammaM()

        lBuild = False

    End Sub

    Private Sub PrepareFenetre()

        Me.pan_GammaM.Dock = DockStyle.Fill

    End Sub

    Private Sub AfficherGammaM()

        Me.txt_GammaM0.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaM0, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaM1.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaM1, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaM2.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaM2, Enu_TypeVariable.SansType, 4, 2, NON_U, False)

    End Sub

    Public Sub ReInit()

        AfficherGammaM()
        'Me.txt_GammaM0.Text = GetStringInUnitN(LogicielOptions.Gamma.GammaM0, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        'Me.txt_GammaM1.Text = GetStringInUnitN(LogicielOptions.Gamma.GammaM1, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        'Me.txt_GammaM2.Text = GetStringInUnitN(LogicielOptions.Gamma.GammaM2, Enu_TypeVariable.SansType, 4, 2, NON_U, False)

    End Sub

#End Region

#Region " Affichage des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaM0.Paint, img_GammaM1.Paint, img_GammaM2.Paint

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

        '--> Initialisation

        lIndice = True
        lGrec = True
        lEgal = True

        Select Case sender.name

            Case Me.img_GammaM0.Name

                strSymbol = "g"
                strIndice = "M0"

            Case Me.img_GammaM1.Name

                strSymbol = "g"
                strIndice = "M1"

            Case Me.img_GammaM2.Name

                strSymbol = "g"
                strIndice = "M2"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènement saisie "

    Private Sub txt_GammaM0_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaM2.TextChanged, txt_GammaM1.TextChanged, txt_GammaM0.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name

                Case txt_GammaM0.Name
                    Frm_GammaN.locGammaM.GammaM0 = ValeurUI
                Case txt_GammaM1.Name
                    Frm_GammaN.locGammaM.GammaM1 = ValeurUI
                Case txt_GammaM2.Name
                    Frm_GammaN.locGammaM.GammaM2 = ValeurUI

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
        'Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case MyTxt.Name

            Case Me.txt_GammaM0.Name, Me.txt_GammaM1.Name, Me.txt_GammaM2.Name
                ValMin = GAMMA_RESISTANCE_MIN
                ValMax = GAMMA_RESISTANCE_MAX
        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) '* kUnit
            'ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region
End Class