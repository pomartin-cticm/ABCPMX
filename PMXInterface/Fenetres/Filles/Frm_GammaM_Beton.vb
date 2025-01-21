Imports PMXMoteur2

Public Class Frm_GammaM_Beton


#Region " Variables "

    Dim lBuild As Boolean

    'Permet la gestion de l'activation ou non du checkbox

    Dim y_decal As Decimal

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(msgChoixGammaV As String)

        lBuild = True

        InitialiserVariables()
        PrepareFenetre(msgChoixGammaV)
        AfficherGammaM()

        lBuild = False

    End Sub

    Private Sub PrepareFenetre(msgChoixGammaV As String)

        Me.pan_GammaM.Dock = DockStyle.Fill
        MAJI_GammaV_Unique()

        Me.chk_GammaV_Unique.Text = msgChoixGammaV

    End Sub

    Private Sub AfficherGammaM()

        Me.txt_GammaC.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaC, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaVs.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaVs, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaVc.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaVc, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaS.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaS, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        Me.txt_GammaP.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaP, Enu_TypeVariable.SansType, 4, 2, NON_U, False)

        Me.chk_GammaV_Unique.Checked = Frm_GammaN.locGammaM.lGammaV_unique

    End Sub

    Public Sub ReInit()

        AfficherGammaM()

    End Sub

    Private Sub InitialiserVariables()

        y_decal = Math.Abs(Me.txt_GammaVs.Top - Me.txt_GammaC.Top)

    End Sub

#End Region

#Region " Affichage des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaC.Paint, img_GammaVs.Paint, img_GammaVc.Paint, img_GammaS.Paint, img_GammaP.Paint

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

            Case Me.img_GammaC.Name

                strSymbol = "g"
                strIndice = "C"

            Case Me.img_GammaVs.Name

                If Frm_GammaN.locGammaM.lGammaV_unique Then
                    strSymbol = "g"
                    strIndice = "V"
                Else
                    strSymbol = "g"
                    strIndice = "Vs"
                End If

            Case Me.img_GammaVc.Name

                If Frm_GammaN.locGammaM.lGammaV_unique Then
                    strSymbol = "g"
                    strIndice = "V"
                Else
                    strSymbol = "g"
                    strIndice = "Vc"
                End If

            Case Me.img_GammaS.Name

                strSymbol = "g"
                strIndice = "s"

            Case Me.img_GammaP.Name

                strSymbol = "g"
                strIndice = "P"


        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènement saisie "

    Private Sub txt_GammaM0_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaC.TextChanged, txt_GammaVs.TextChanged, txt_GammaVc.TextChanged, txt_GammaS.TextChanged, txt_GammaP.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name

                Case txt_GammaC.Name

                    Frm_GammaN.locGammaM.GammaC = ValeurUI
                Case txt_GammaVs.Name
                    If Frm_GammaN.locGammaM.lGammaV_unique Then

                        Frm_GammaN.locGammaM.GammaVs = ValeurUI
                        Frm_GammaN.locGammaM.GammaVc = ValeurUI
                    Else

                        Frm_GammaN.locGammaM.GammaVs = ValeurUI
                    End If
                Case txt_GammaVc.Name
                    Frm_GammaN.locGammaM.GammaVc = ValeurUI

                Case txt_GammaS.Name
                    Frm_GammaN.locGammaM.GammaS = ValeurUI
                Case txt_GammaP.Name
                    Frm_GammaN.locGammaM.GammaP = ValeurUI

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

            Case Me.txt_GammaC.Name, Me.txt_GammaVs.Name, Me.txt_GammaVc.Name, Me.txt_GammaS.Name, Me.txt_GammaP.Name

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

    Private Sub chk_GammaV_Unique_CheckedChanged(sender As Object, e As EventArgs) Handles chk_GammaV_Unique.CheckedChanged
        If lBuild Then Exit Sub

        Frm_GammaN.locGammaM.lGammaV_unique = chk_GammaV_Unique.Checked

        If chk_GammaV_Unique.Checked Then
            Frm_GammaN.locGammaM.GammaVc = Frm_GammaN.locGammaM.GammaVs
            Me.txt_GammaVc.Text = GetStringInUnitN(Frm_GammaN.locGammaM.GammaVc, Enu_TypeVariable.SansType, 4, 2, NON_U, False)
        End If

        MAJI_GammaV_Unique()

    End Sub


    Private Sub MAJI_GammaV_Unique()

        If Frm_GammaN.locGammaM.lGammaV_unique Then

            Me.txt_GammaS.Top = Me.txt_GammaVs.Top + y_decal
            Me.img_GammaS.Top = Me.img_GammaVs.Top + y_decal

        Else

            Me.txt_GammaVc.Top = Me.txt_GammaVs.Top + y_decal
            Me.img_GammaVc.Top = Me.img_GammaVs.Top + y_decal

            Me.txt_GammaS.Top = Me.txt_GammaVc.Top + y_decal
            Me.img_GammaS.Top = Me.img_GammaVc.Top + y_decal

        End If

        Me.txt_GammaP.Top = Me.txt_GammaS.Top + y_decal
        Me.img_GammaP.Top = Me.img_GammaS.Top + y_decal

        Me.txt_GammaVs.Visible = True
        Me.img_GammaVs.Visible = True
        Me.txt_GammaVc.Visible = Not Me.chk_GammaV_Unique.Checked
        Me.img_GammaVc.Visible = Not Me.chk_GammaV_Unique.Checked

        img_GammaVs.Invalidate()
        img_GammaVc.Invalidate()

    End Sub
#End Region

End Class