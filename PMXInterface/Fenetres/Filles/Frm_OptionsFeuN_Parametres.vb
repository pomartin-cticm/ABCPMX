Imports PMXMoteur2

Public Class Frm_OptionsFeuN_Parametres

#Region " Variables locales "

    Dim lBuild As Boolean

#End Region

#Region "===Ouverture==="

    Public Sub InitialiseFenetre(BlocL As Dictionary(Of String, String))

        lBuild = True

        GestionStyle()
        GestionLangues(BlocL)
        GestionUnites()
        AffichagePoutreEnCours(Frm_OptionsFeuN.BeamLoc)

        Me.pan_General.Dock = DockStyle.Fill

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        Me.lbl_ParamCalcul.BackColor = CouleurBackBandeaux
        Me.lbl_ParamCalcul.ForeColor = CouleurForeBandeaux

        Me.txt_Boltzmann.ReadOnly = True
        Me.txt_TimeIncrement.ReadOnly = False
        Me.txt_ReferenceTemp.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_MaxTemp.ReadOnly = True
        Me.txt_FormFactor.ReadOnly = Not LogicielOptions.lExpert
        'Me.txt_EmissivitySteel.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_EmissivityFire.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_ConvectionFactor.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_ShadowEffect.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_ConvectionSlab.ReadOnly = True
        Me.txt_ConcreteResistance.ReadOnly = True

    End Sub

    Private Sub GestionUnites()

        etq_UnitTime.Text = "s"
        etq_UnitTemp1.Text = "°C"
        etq_UnitTemp2.Text = "°C"

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '=== PARAMETRES CALCUL ==============================================================='

            Me.lbl_ParamCalcul.Text = Bloc("CALCULPARAM")

            lbl_Boltzmann.Text = Bloc("BOLTZMANN")
            lbl_TimeIncrement.Text = Bloc("TIMEINCREMENT")

            lbl_ReferenceTemp.Text = Bloc("REFERENCETEMP")
            lbl_MaxTemp.Text = Bloc("MAXTEMP")
            lbl_FormFactor.Text = Bloc("FORMFACTOR")
            'lbl_EmissivitySteel.Text = Bloc("EMISSIVITYSTEELSURF")
            lbl_EmissivityFire.Text = Bloc("EMISSIVITYFIRE")
            lbl_EmissiviteBeton.Text = Bloc("EMISSIVITYCONCRETE")
            lbl_ConvectionFactor.Text = Bloc("CONVECTIONFACTOR")
            lbl_ShadowEffect.Text = Bloc("SHADOWEFFECT")
            lbl_ConcreteResistance.Text = Bloc("CONCRETEFACTOR")

            Me.lbl_SousDalle.Text = Bloc("BELOWSLAB")
            Me.lbl_SurDalle.Text = Bloc("ABOVESLAB")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub


#End Region

#Region " Affichage Poutre "

    Private Sub AffichagePoutreEnCours(myBeam As cls_Poutre)

        With myBeam.ParamFeu
            Me.txt_Boltzmann.Text = GetStringInUnit(cls_OptionsFeu.BOLTZMANN * 10 ^ 8, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_TimeIncrement.Text = GetStringInUnit(.DeltaTCalcul, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ReferenceTemp.Text = GetStringInUnit(.TempRef, Enu_TypeVariable.Temperature, 3, 2, False)
            Me.txt_MaxTemp.Text = GetStringInUnit(cls_OptionsFeu.TempMax, Enu_TypeVariable.Temperature, 3, 2, False)
            Me.txt_FormFactor.Text = GetStringInUnit(.PhiViewFactor, Enu_TypeVariable.SansType, 3, 2, False)
            'Me.txt_EmissivitySteel.Text = GetStringInUnit(.EmissivitySteel, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_EmissivityFire.Text = GetStringInUnit(.EmissivityFire, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_EmissiviteBeton.Text = GetStringInUnit(.EmissivityC, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ConvectionFactor.Text = GetStringInUnit(.ConvectionCoef, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ShadowEffect.Text = GetStringInUnit(.ksh, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ConvectionSlab.Text = GetStringInUnit(.ConvectionCoefDalle, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ConcreteResistance.Text = GetStringInUnit(.AlphaSlab, Enu_TypeVariable.SansType, 3, 2, False)
        End With

    End Sub

#End Region

#Region " Evenements de saisie "

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_TimeIncrement.TextChanged, txt_ReferenceTemp.TextChanged,
        txt_FormFactor.TextChanged, txt_EmissivityFire.TextChanged, txt_ConvectionFactor.TextChanged,
        txt_ShadowEffect.TextChanged, txt_EmissiviteBeton.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With Frm_OptionsFeuN.BeamLoc.ParamFeu
                Select Case sender.name

                    Case txt_TimeIncrement.Name
                        .DeltaTCalcul = ValeurUI
                    Case txt_ReferenceTemp.Name
                        .TempRef = ValeurUI
                    Case txt_FormFactor.Name
                        .PhiViewFactor = ValeurUI
                        'Case txt_EmissivitySteel.Name
                        '.EmissivitySteel = ValeurUI
                    Case txt_EmissivityFire.Name
                        .EmissivityFire = ValeurUI
                    Case txt_EmissiviteBeton.Name
                        .EmissivityC = ValeurUI
                    Case txt_ConvectionFactor.Name
                        .ConvectionCoef = ValeurUI
                    Case txt_ShadowEffect.Name
                        .ksh = ValeurUI

                End Select

            End With

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_FeuParam.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin, lValMax As Boolean
        Dim kUnit As Decimal = 1

        lValMin = True
        lValMax = True

        Select Case MyTxt.Name




            Case txt_TimeIncrement.Name
                ValMin = 1 's
                If Frm_OptionsFeuN.BeamLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege Then
                    ValMax = 30 's
                Else
                    ValMax = 5 's
                End If

            Case txt_ReferenceTemp.Name
                lValMin = False
                lValMax = False

            Case txt_FormFactor.Name, txt_EmissivityFire.Name, txt_ShadowEffect.Name, txt_EmissiviteBeton.Name
                ValMin = 0
                ValMax = 1

            Case txt_ConvectionFactor.Name
                ValMin = 0
                lValMax = False

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin / kUnit, lValMax, ValMax / kUnit)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_FeuParam, ValMin / kUnit, lValMin, ValMax / kUnit, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region


#Region " Dessins des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) _
        Handles img_Boltzmann.Paint, img_TimeIncrement.Paint, img_ReferenceTemp.Paint, img_MaxTemp.Paint, img_FormFactor.Paint,
        img_EmissivityFire.Paint, img_ConvectionFactor.Paint, img_ShadowEffect.Paint, img_ConvectionSlab.Paint, img_ConcreteResistance.Paint, img_EmissiviteBeton.Paint


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

            Case Me.img_Boltzmann.Name

                strSymbol = "s"
                strIndice = ""

            Case Me.img_TimeIncrement.Name

                strSymbol = "D"
                strIndice = "t"

            Case Me.img_ReferenceTemp.Name

                strSymbol = "q"
                strIndice = "ref"

            Case Me.img_MaxTemp.Name

                strSymbol = "q"
                strIndice = "max"

            Case Me.img_FormFactor.Name

                strSymbol = "F"
                strIndice = ""

            'Case Me.img_EmissivitySteel.Name

            '    strSymbol = "e"
            '    strIndice = "m"

            Case Me.img_EmissivityFire.Name

                strSymbol = "e"
                strIndice = "f"

            Case Me.img_EmissiviteBeton.Name

                strSymbol = "e"
                strIndice = "c"

            Case Me.img_ConvectionFactor.Name

                strSymbol = "a"
                strIndice = "c"

            Case Me.img_ShadowEffect.Name

                strSymbol = "k"
                strIndice = "sh"

                lGrec = False

            Case Me.img_ConvectionSlab.Name

                strSymbol = "a"
                strIndice = "cc"

            Case Me.img_ConcreteResistance.Name

                strSymbol = "a"
                strIndice = "slab"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Dessin des unités spéciales "

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
        Dim FontNormal As New Font(Me.txt_Boltzmann.Font.Name, 8.25)
        Dim FontExp As New Font(Me.txt_Boltzmann.Font.Name, 6.25)
        Const kMatch As Single = 0.93

        Chaine = "x10"
        sCar = MyGr.MeasureString(Chaine, FontNormal).Height
        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        yPen = (sHI / 2 - sCar) / 2
        yPen = sHI / 2 - sCar / 2
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


    Private Sub img_UnitThermConvection_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_UnitThermConvection.Paint, img_UnitThermConvection2.Paint
        DrawUnitConvection(e.Graphics, Me.img_UnitThermConvection.ClientRectangle.Width, Me.img_UnitThermConvection.ClientRectangle.Height)
    End Sub

    Private Sub DrawUnitConvection(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single)
        '----------------------------------------------------------------------------------------
        '   30/09/09 :  Création - Version 2.00
        '----------------------------------------------------------------------------------------
        '   Affiche unités cte de Boltzman
        '----------------------------------------------------------------------------------------

        Dim Chaine As String
        Dim xPen, yPen As Single
        Dim sCar, xDec, hDec As Single
        Dim FontNormal As New Font(Me.txt_Boltzmann.Font.Name, 8.25)
        Dim FontExp As New Font(Me.txt_Boltzmann.Font.Name, 6.25)
        Const kMatch As Single = 0.85

        '*********************************************

        Chaine = "W/m"
        sCar = MyGr.MeasureString(Chaine, FontNormal).Height
        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        yPen = (sHI - sCar) / 2
        xPen = 1

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec
        hDec = sCar / 4

        '*********************************************

        Chaine = "2"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)

        xPen += kMatch * xDec

        '*********************************************

        Chaine = "K"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        FontNormal.Dispose()
        FontExp.Dispose()

    End Sub

#End Region

End Class