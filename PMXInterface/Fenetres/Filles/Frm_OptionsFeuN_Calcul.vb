Imports PMXMoteur2

Public Class Frm_OptionsFeuN_Calcul

#Region " Variables locales "

    Dim lBuild As Boolean
    Dim strTempRebars(2)

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(BlocL As Dictionary(Of String, String))
        lBuild = True

        GestionStyle()
        GestionLangues(BlocL)
        GestionUnites()
        PrepareFenetre()
        RemplirComboTempRebar()

        Me.pan_General.Dock = DockStyle.Fill

        AffichePoutreEnCours(Frm_OptionsFeuN.BeamLoc)

        lBuild = False
    End Sub

    Private Sub GestionStyle()

        Me.lbl_CalculOptions.BackColor = CouleurBackBandeaux
        Me.lbl_CalculOptions.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub PrepareFenetre()
        Me.pan_TempArma.Visible = LogicielOptions.lExpert
    End Sub

    Private Sub GestionUnites()

        lbl_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        etq_UnitU.Text = "%"

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '--> chk_ReductionConcreteStrenght

            Me.chk_ReductionConcreteStrenght.Text = Bloc("CONCRETEREDUC250")

            '=== OPTIONS DE CALCUL ==============================================================='

            Me.lbl_CalculOptions.Text = Bloc("CALCULOPTIONS")

            '--> chk_ArmaComp

            chk_ArmaComp.Text = Bloc("ARMACOMP")

            '--> chk_DalleFEM

            chk_DalleFEM.Text = Bloc("DALLEFEM")
            ' lbl_tDalleFEMmax.Text = Bloc("TDALLEFEM")

            Me.chk_ANFrance.Text = Bloc("FRENCHNA")
            Me.chk_RhoCconstante.Text = Bloc("CONSTANTRHOC")
            Me.lbl_TeneurEau.Text = Bloc("MOISTURECONTENT")

            Me.lbl_TempRebars.Text = Bloc("TEMPREBARS")
            strTempRebars(0) = Bloc("AVERAGETEMP")
            strTempRebars(1) = Bloc("MAXTEMP2")
            strTempRebars(2) = Bloc("AXISTEMP")

            Me.chk_CongeEnrobe.Text = Bloc("ENCASEDROOTFILLET")

            Me.lbl_SizeElt.Text = Bloc("SIZEELT")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub RemplirComboTempRebar()

        Me.cmb_TempRebars.Items.Clear()
        Me.cmb_TempRebars.Items.AddRange(strTempRebars)
        Me.cmb_TempRebars.SelectedIndex = 0

    End Sub
#End Region

#Region " Affichage poutre en cours "

    Private Sub AffichePoutreEnCours(myBeam As cls_Poutre)

        With myBeam.ParamFeu

            '--> Options de calcul

            Me.chk_DalleFEM.Checked = .lDalleFEM

            MAJI_FEM(myBeam)
            Me.txt_tDalleFEMmax.Text = GetStringInUnitN(.tDalleEFmax, Enu_TypeVariable.Dimension, 3, 2, Enu_AfficheUnite.Non, True)
            Me.chk_ArmaComp.Checked = .lArmaCompression
            Me.chk_ReductionConcreteStrenght.Checked = .lReductionConcreteStrength
            Select Case .MethodTempArma
                Case cls_OptionsFeu.enuTypeInterpoleTempArma.Axe : Me.cmb_TempRebars.SelectedIndex = 2
                Case cls_OptionsFeu.enuTypeInterpoleTempArma.Maximale : Me.cmb_TempRebars.SelectedIndex = 1
                Case cls_OptionsFeu.enuTypeInterpoleTempArma.Moyenne : Me.cmb_TempRebars.SelectedIndex = 0
            End Select

            Me.chk_CongeEnrobe.Checked = myBeam.ParamFeu.lCongesEnrobe

            '--> Options FEM

            Me.chk_RhoCconstante.Checked = Not .lRhoCvar
            Me.chk_ANFrance.Checked = .lANFrance

            Me.txt_U.Text = GetStringInUnitN(.TeneurU, Enu_TypeVariable.SansType, 3, 2, Enu_AfficheUnite.Non, True)

            MAJI_OptionsFEM()
        End With

    End Sub

    Private Sub MAJI_FEM(myBeam As cls_Poutre)
        Dim lDalleFEM As Boolean = myBeam.ParamFeu.lDalleFEM

        'lbl_tDalleFEMmax.Enabled = lDalleFEM
        img_tDalleFEMmax.Enabled = lDalleFEM
        txt_tDalleFEMmax.Enabled = lDalleFEM
        lbl_UnitD1.Enabled = lDalleFEM
    End Sub
#End Region

#Region " Dessins des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_tDalleFEMmax.Paint, img_U.Paint

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

            Case Me.img_tDalleFEMmax.Name

                strSymbol = "t"
                strIndice = "max"

                lGrec = False

            Case Me.img_U.Name

                strSymbol = "u"
                strIndice = ""
                lGrec = False

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "

    Private Sub chk_CongeEnrobe_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CongeEnrobe.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lCongesEnrobe = Me.chk_CongeEnrobe.Checked

    End Sub

    Private Sub chk_ArmaComp_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ArmaComp.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lArmaCompression = chk_ArmaComp.Checked

    End Sub

    Private Sub chk_DalleFEM_CheckedChanged(sender As Object, e As EventArgs) Handles chk_DalleFEM.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lDalleFEM = chk_DalleFEM.Checked

        MAJI_OptionsFEM()

        lBuild = False
    End Sub

    Private Sub MAJI_OptionsFEM()

        Me.pan_OptionsFEM.Enabled = Frm_OptionsFeuN.BeamLoc.ParamFeu.lDalleFEM

        Dim lDalleFEM As Boolean = Frm_OptionsFeuN.BeamLoc.ParamFeu.lDalleFEM

        img_tDalleFEMmax.Enabled = lDalleFEM
        txt_tDalleFEMmax.Enabled = lDalleFEM
        lbl_UnitD1.Enabled = lDalleFEM
    End Sub

    Private Sub chk_ReductionConcreteStrenght_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ReductionConcreteStrenght.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lReductionConcreteStrength = chk_ReductionConcreteStrenght.Checked

    End Sub

    Private Sub cmb_TempRebars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TempRebars.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TempRebars.SelectedIndex
            Case 0 : Frm_OptionsFeuN.BeamLoc.ParamFeu.MethodTempArma = cls_OptionsFeu.enuTypeInterpoleTempArma.Moyenne
            Case 1 : Frm_OptionsFeuN.BeamLoc.ParamFeu.MethodTempArma = cls_OptionsFeu.enuTypeInterpoleTempArma.Maximale
            Case 2 : Frm_OptionsFeuN.BeamLoc.ParamFeu.MethodTempArma = cls_OptionsFeu.enuTypeInterpoleTempArma.Axe
        End Select

    End Sub


#End Region

#Region " Evenements de saisie "

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) _
        Handles txt_tDalleFEMmax.TextChanged, txt_U.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With Frm_OptionsFeuN.BeamLoc.ParamFeu
                Select Case sender.name
                    Case txt_U.Name
                        .TeneurU = ValeurUI

                    Case txt_tDalleFEMmax.Name
                        .tDalleEFmax = ValeurUI

                End Select

            End With

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_FeuCal.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin, lValMax As Boolean
        Dim kUnit As Decimal = 1

        lValMin = True
        lValMax = True

        Select Case MyTxt.Name
            Case Me.txt_U.Name
                ValMin = 0
                ValMax = 10
                kUnit = 1

            Case txt_tDalleFEMmax.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = 1 / 1000 '1 mm
                lValMax = False

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin / kUnit, lValMax, ValMax / kUnit)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_FeuCal, ValMin / kUnit, lValMin, ValMax / kUnit, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

    Private Sub chk_RhoCconstante_CheckedChanged(sender As Object, e As EventArgs) Handles chk_RhoCconstante.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lRhoCvar = Not chk_RhoCconstante.Checked

    End Sub
    Private Sub chk_ANFrance_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ANFrance.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lANFrance = chk_ANFrance.Checked

    End Sub

#End Region

End Class