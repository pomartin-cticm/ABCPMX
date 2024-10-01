Imports PMXMoteur2

Public Class Frm_OptionsCalculIncendie

#Region " Attributs "

    Dim lBuild As Boolean
    Const BALISE As String = "OPTCALFIRE"


#End Region

#Region "===Ouverture==="

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Incendie.Text = MyBloc("TITLE")

            Me.lbl_Constantes.Text = MyBloc("CONSTANTS")
            Me.lbl_Boltzman.Text = MyBloc("BOLTZMAN")
            Me.lbl_EmissiviteBeton.Text = MyBloc("EMISSIVITYC")
            Me.lbl_EmissiviteFeu.Text = MyBloc("EMISSIVITYF")
            Me.lbl_AlphaC.Text = MyBloc("CONVECTIONFACTOR")
            Me.lbl_AlphaCC.Text = MyBloc("CONVECTIONFACTORSLAB")
            Me.lbl_FormFactorPhi.Text = MyBloc("FORMFACTOR")
            Me.lbl_ShadowKsh.Text = MyBloc("FORMFACTOR")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Incendie.Dock = DockStyle.Fill

        Me.lbl_Incendie.BackColor = CouleurBackBandeaux
        Me.lbl_Incendie.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_Sigma, False)
        PrepareTextBoxDipo(Me.txt_EmissiviteBeton, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_EmissiviteFeu, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_AlphaC, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_AlphaCC, LogicielOptions.lExpert)

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitBoltzman.Text = LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

    Private Sub AfficherOptionsEnCours()

        Me.txt_Sigma.Text = GetStringInUnitN(cls_OptionsFeu.BOLTZMANN * 10 ^ 8, Enu_TypeVariable.SansType, 5, 4, False, True)
        Me.txt_EmissiviteFeu.Text = GetStringInUnitN(OptionsFeu.EmissiviteF, Enu_TypeVariable.SansType, 5, 4, False, True)
        Me.txt_EmissiviteBeton.Text = GetStringInUnitN(OptionsFeu.EmissiviteC, Enu_TypeVariable.SansType, 5, 4, False, True)
        Me.txt_AlphaC.Text = GetStringInUnitN(OptionsFeu.AlphaC, Enu_TypeVariable.SansType, 5, 4, False, True)
        Me.txt_AlphaCC.Text = GetStringInUnitN(OptionsFeu.AlphaCC, Enu_TypeVariable.SansType, 5, 4, False, True)
        Me.txt_ksh.Text = GetStringInUnitN(OptionsFeu.ksh, Enu_TypeVariable.SansType, 5, 4, False, True)
        Me.txt_Phi.Text = GetStringInUnitN(OptionsFeu.Phi, Enu_TypeVariable.SansType, 5, 4, False, True)

    End Sub

#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Sigma.Paint, img_dNodes.Paint, img_T0.Paint, img_EpsilonF.Paint, img_EpsilonC.Paint, img_Deltat.Paint, img_AlphaC.Paint, img_AlphaCC.Paint, img_Phi.Paint, img_ksh.Paint

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
        lEgal = True
        AlignH = Enu_AlignementH.Droite

        Select Case sender.name
            Case Me.img_Sigma.Name
                strSymbol = "s"
                strIndice = ""
                lEgal = True
                lGrec = True
                'AlignH = Enu_AlignementH.Droite
            Case Me.img_dNodes.Name
                strSymbol = "d"
                strIndice = ""
                lEgal = False
            Case Me.img_T0.Name
                strSymbol = "q"
                strIndice = "0"
                lGrec = True
            Case Me.img_EpsilonC.Name
                strSymbol = "e"
                strIndice = "c"
                lGrec = True
            Case Me.img_EpsilonF.Name
                strSymbol = "e"
                strIndice = "f"
                lGrec = True
            Case Me.img_Deltat.Name
                strSymbol = "D"
                strIndice = "t"
                lGrec = True

            Case Me.img_AlphaC.Name
                strSymbol = "a"
                strIndice = "c"
                lGrec = True

            Case Me.img_AlphaCC.Name
                strSymbol = "a"
                strIndice = "cc"
                lGrec = True

            Case Me.img_ksh.Name
                strSymbol = "k"
                strIndice = "sh"
                lGrec = False

            Case Me.img_Phi.Name
                strSymbol = "f"
                strIndice = ""
                lGrec = True


        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region "   Dessin des unités spéciales"

    Private Sub img_UnitBoltzmann_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_UnitBoltzmann.Paint
        DrawUnitBoltzmann(e.Graphics, Me.img_UnitBoltzmann.ClientRectangle.Width, Me.img_UnitBoltzmann.ClientRectangle.Height)
    End Sub

    Private Sub DrawUnitBoltzmann(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single)
        '----------------------------------------------------------------------------------------
        '   30/09/09 :  Création - Version 2.00
        '----------------------------------------------------------------------------------------
        '   Affiche unités cte de Boltzman
        '----------------------------------------------------------------------------------------

        Dim Chaine As String
        Dim xPen, yPen As Single
        Dim sCar, xDec, hDec As Single
        Dim FontNormal As New Font(Me.txt_Sigma.Font.Name, 8.25)
        Dim FontExp As New Font(Me.txt_Sigma.Font.Name, 6.25)
        Const kMatch As Single = 0.93

        Chaine = "x10"
        sCar = MyGr.MeasureString(Chaine, FontNormal).Height
        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        yPen = (sHI - sCar) / 2
        xPen = 1

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec
        hDec = sCar / 4

        Chaine = "-8"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)


        xPen += kMatch * xDec
        Chaine = " W/m"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec

        Chaine = "2"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)

        xPen += kMatch * xDec
        Chaine = "K"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec

        Chaine = "4"

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)

        FontNormal.Dispose()
        FontExp.Dispose()
    End Sub

    Private Sub img_UnitThermConvection_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_UnitAlphaC.Paint, img_UnitAlphaCC.Paint
        DrawUnitAlphaConvection(e.Graphics, Me.img_UnitAlphaC.ClientRectangle.Width, Me.img_UnitAlphaC.ClientRectangle.Height)
    End Sub

    Private Sub DrawUnitAlphaConvection(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single)
        '----------------------------------------------------------------------------------------
        '   30/09/09 :  Création - Version 2.00
        '----------------------------------------------------------------------------------------
        '   Affiche unités cte de Boltzman
        '----------------------------------------------------------------------------------------

        Dim Chaine As String
        Dim xPen, yPen As Single
        Dim sCar, xDec, hDec As Single
        Dim FontNormal As New Font(Me.txt_Sigma.Font.Name, 8.25)
        Dim FontExp As New Font(Me.txt_Sigma.Font.Name, 6.25)
        Const kMatch As Single = 0.97

        Chaine = "W/m"
        sCar = MyGr.MeasureString(Chaine, FontNormal).Height
        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        yPen = (sHI - sCar) / 2
        xPen = 1

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec
        hDec = sCar / 4

        Chaine = "2"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)


        xPen += kMatch * xDec
        Chaine = "K"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        FontNormal.Dispose()
        FontExp.Dispose()
    End Sub


#End Region

#Region " Gestion des évènements de saisie "

    Private Sub txt_CteFeu_TextChanged(sender As Object, e As EventArgs) Handles txt_AlphaCC.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            LocalOptionsFeu.AlphaCC = ValeurUI
        End If

    End Sub

    Private Sub txt_AlphaC_TextChanged(sender As Object, e As EventArgs) Handles txt_AlphaC.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            LocalOptionsFeu.AlphaC = ValeurUI
        End If
    End Sub

    Private Sub txt_EmissiviteFeu_TextChanged(sender As Object, e As EventArgs) Handles txt_EmissiviteFeu.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            LocalOptionsFeu.EmissiviteF = ValeurUI
        End If
    End Sub

    Private Sub txt_EmissiviteBeton_TextChanged(sender As Object, e As EventArgs) Handles txt_EmissiviteBeton.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            LocalOptionsFeu.EmissiviteC = ValeurUI
        End If
    End Sub

    Private Sub txt_ksh_TextChanged(sender As Object, e As EventArgs) Handles txt_ksh.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            LocalOptionsFeu.ksh = ValeurUI
        End If
    End Sub

    Private Sub txt_Phi_TextChanged(sender As Object, e As EventArgs) Handles txt_Phi.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            LocalOptionsFeu.Phi = ValeurUI
        End If
    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal

        Select Case MyTxt.Name
            Case Me.txt_AlphaC.Name

                lValMax = False
                kUnit = 1
                ValMin = 0

            Case Me.txt_AlphaCC.Name

                lValMax = False
                kUnit = 1
                ValMin = 0

            Case Me.txt_EmissiviteFeu.Name, Me.txt_EmissiviteBeton.Name

                lValMax = True
                kUnit = 1
                ValMin = 0
                ValMax = 1

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin / kUnit, lValMax, ValMax / kUnit)

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


End Class