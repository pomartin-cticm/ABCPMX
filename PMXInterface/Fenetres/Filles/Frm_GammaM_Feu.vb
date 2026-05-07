Imports PMXMoteur2

Public Class Frm_GammaM_Feu


#Region " Variables "

    Dim lBuild As Boolean
    Dim myNorme As Enu_Normes

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

        myNorme = MyProjet.Poutres(MyProjet.IndEnCours).Param.Norme

    End Sub

    Private Sub AfficherGammaM()

        Me.txt_GammaM_fi.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaM_fi, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaC_fi.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaC_fi, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaS_fi.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaS_fi, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaV_fi.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaV_fi, Enu_TypeVariable.SansType, 4, 2, NON_U, False)

    End Sub

    Public Sub ReInit()

        AfficherGammaM()

    End Sub

#End Region

#Region " Evènement saisie "

    Private Sub txt_GammaM0_TextChanged(sender As Object, e As EventArgs) _
        Handles txt_GammaM_fi.TextChanged, txt_GammaC_fi.TextChanged, txt_GammaS_fi.TextChanged, txt_GammaV_fi.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name

                Case txt_GammaM_fi.Name
                    Frm_GammaN.locGammaM.GammaM_fi = ValeurUI
                Case txt_GammaC_fi.Name
                    Frm_GammaN.locGammaM.GammaC_fi = ValeurUI
                Case txt_GammaV_fi.Name
                    Frm_GammaN.locGammaM.GammaV_fi = ValeurUI
                Case txt_GammaS_fi.Name
                    Frm_GammaN.locGammaM.GammaS_fi = ValeurUI

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

            Case Me.txt_GammaM_fi.Name, Me.txt_GammaC_fi.Name, Me.txt_GammaV_fi.Name, Me.txt_GammaS_fi.Name
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

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) _
        Handles img_GammaM_fi.Paint, img_GammaC_fi.Paint, img_GammaV_fi.Paint, img_GammaS_fi.Paint

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

        Dim ENFeu As New cls_EurocodesFeu

        '--> Initialisation

        lIndice = True
        lGrec = True
        lEgal = True

        Select Case sender.name

            Case Me.img_GammaM_fi.Name

                strSymbol = "g"
                ' strIndice = "M,fi"
                strIndice = ENFeu.IndiceGammaFeu(myNorme, "a")

            Case Me.img_GammaC_fi.Name

                strSymbol = "g"
                'strIndice = "C,fi"
                strIndice = ENFeu.IndiceGammaFeu(myNorme, "c")

            Case Me.img_GammaS_fi.Name

                strSymbol = "g"
                'strIndice = "s,fi"
                strIndice = ENFeu.IndiceGammaFeu(myNorme, "s")

            Case Me.img_GammaV_fi.Name

                strSymbol = "g"
                'strIndice = "V,fi"
                strIndice = ENFeu.IndiceGammaFeu(myNorme, "v")

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class