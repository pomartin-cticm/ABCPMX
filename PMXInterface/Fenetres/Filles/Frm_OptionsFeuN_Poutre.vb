Imports PMXMoteur2

Public Class Frm_OptionsFeuN_Poutre

#Region "   Paramètres "

    Dim lBuild As Boolean

    Dim strSurfaceType, strProtectionType, strInsulationTypeSpray, strInsulationTypeBoards As String() 'cmb_SurfaceType

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_OptionsFeuN_Poutre_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub InitialiseFenetre(BlocL As Dictionary(Of String, String), myBeam As cls_poutre)

        lBuild = True

        GestionLangue(BlocL)
        GestionStyle()
        GestionUnites()

        Me.pan_General.Dock = DockStyle.Fill

        AffichePoutreEncours(myBeam)

        lBuild = False

    End Sub

    Private Sub AffichePoutreEncours(myBeam As cls_Poutre)

        With myBeam.ParamFeu

            '--> Partie qui concerne les paramètres de la poutre

            chk_CalculFeu.Checked = .lCalculFeu
            Affiche_SurfaceType(myBeam)

            Me.chk_ProtectionThermique.Checked = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
            Me.chk_AcierGalva.Checked = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise)

            MAJ_ProtectionType(myBeam)
            MAJ_InsulationType(myBeam)

            Me.chk_ArmaFroid.Checked = myBeam.ParamFeu.lArmaFormeeAFroid
            'Me.chk_DalleFEM.Checked = myBeam.ParamFeu.lDalleFEM
            Me.txt_EpProtec.Text = GetStringInUnitN(.EpProtection, Enu_TypeVariable.Dimension, 3, 2, False, True)

            MAJI_CalcuFeu(myBeam)
            MAJI_TypeProtection(myBeam)

        End With
    End Sub

    Private Sub GestionLangue(BlocL As Dictionary(Of String, String))

        '=== PARAMETRES POUTRES ==============================================================='

        Me.lbl_ParamPoutre.Text = BlocL("BEAMPARAM")

        '--> chk_CalculFeu

        chk_CalculFeu.Text = BlocL("FIRECALCULATION")

        '--> cmb_SurfaceType
        Me.lbl_SurfaceType.Text = BlocL("SURFACETYPE")

        ReDim strSurfaceType(1)
        strSurfaceType(0) = BlocL("PROTECTED")
        strSurfaceType(1) = BlocL("UNPROTECTED")

        RemplirComboAvecTableau(Me.cmb_SurfaceType, strSurfaceType)

        '--> chk_AcierGalva

        Me.chk_AcierGalva.Text = BlocL("GALVANISED")

        '--> cmb_ProtectionType

        Me.lbl_ProtectionType.Text = BlocL("PROTECTIONTYPE")

        ReDim strProtectionType(2)
        strProtectionType(0) = BlocL("SPRAY")
        strProtectionType(1) = BlocL("INTUMESCENTPAINT")
        strProtectionType(2) = BlocL("BOARDS")

        RemplirComboAvecTableau(Me.cmb_ProtectionType, strProtectionType)

        '--> cmb_InsulationType

        Me.lbl_InsulationType.Text = BlocL("INSULATIONTYPE")
        Me.chk_ProtectionThermique.Text = BlocL("PROTECTEDSTEEL")

        ReDim strInsulationTypeSpray(3)

        strInsulationTypeSpray(0) = BlocL("MINERALFIBRES")
        strInsulationTypeSpray(1) = BlocL("VERMICULITE")
        strInsulationTypeSpray(2) = BlocL("PERLITECEMENT")
        strInsulationTypeSpray(3) = BlocL("PERLITEPLASTER")

        ReDim strInsulationTypeBoards(3)

        strInsulationTypeBoards(0) = BlocL("VERMICULITEPERLITE")
        strInsulationTypeBoards(1) = BlocL("SILICATE")
        strInsulationTypeBoards(2) = BlocL("FIBROCEMENT")
        strInsulationTypeBoards(3) = BlocL("PLASTER")

        lbl_Density.Text = BlocL("DENSITY")
        lbl_ThermalConductivity.Text = BlocL("THERMALCONDUC")
        lbl_SpecificHeat.Text = BlocL("SPECIFICHEAT")

        lbl_EpProtec.Text = BlocL("PROTECTIONTH")

        ''--> chk_ReductionConcreteStrenght

        'Me.chk_ReductionConcreteStrenght.Text = Bloc("CONCRETEREDUC250")

        '--> chk_ArmaFroid

        chk_ArmaFroid.Text = BlocL("ARMAFROID")

    End Sub

    Private Sub GestionUnites()

        lbl_UnitDensity.Text = "kg/m3"

        lbl_UnitThermalCond.Text = "W/m.K"
        lbl_UnitSpecificHeat.Text = "J/kg.K"

        etq_UnitD2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub GestionStyle()

        Me.lbl_ParamPoutre.BackColor = CouleurBackBandeaux
        Me.lbl_ParamPoutre.ForeColor = CouleurForeBandeaux

        Me.txt_Density.ReadOnly = True
        Me.txt_SpecificHeat.ReadOnly = True

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

#End Region

#Region "   Affichage "

    Private Sub MAJI_TypeProtection(myBeam As cls_Poutre)
        'Gestion de l'affichage en fonction de si l'acier est protégé au feu ou non

        Dim lProtege As Boolean = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)

        Me.pan_Protection.Visible = lProtege

        Me.pan_Protection.Top = Me.chk_AcierGalva.Top

    End Sub

    Private Sub Affiche_SurfaceType(myBeam As cls_Poutre)

        With myBeam.ParamFeu

            Select Case .TypeSurface
                Case cls_OptionsFeu.enu_TypeSurface.Protege
                    Me.cmb_SurfaceType.SelectedIndex = 0
                Case cls_OptionsFeu.enu_TypeSurface.AcierNu, cls_OptionsFeu.enu_TypeSurface.Galvanise
                    Me.cmb_SurfaceType.SelectedIndex = 1
            End Select

        End With
    End Sub

    ''' <summary>
    ''' Gère l'affichage du cmb_ProtectionType
    ''' </summary>
    Private Sub MAJ_ProtectionType(myBeam As cls_Poutre)

        With myBeam.ParamFeu

            If .TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege Then

                cmb_ProtectionType.Enabled = True

                Select Case .Protection
                    Case cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral,
                         cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Vermiculite,
                         cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerliteCement,
                            cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerlitePlaster
                        Me.cmb_ProtectionType.SelectedIndex = 0

                    Case cls_OptionsFeu.enu_TypeProtection.IntumescentPaint
                        Me.cmb_ProtectionType.SelectedIndex = 1

                    Case cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite,
                         cls_OptionsFeu.enu_TypeProtection.BoardsSilicate,
                         cls_OptionsFeu.enu_TypeProtection.BoardsFibroCement,
                         cls_OptionsFeu.enu_TypeProtection.BoardsPlaster
                        Me.cmb_ProtectionType.SelectedIndex = 2

                End Select

            Else
                cmb_ProtectionType.Enabled = False
                cmb_ProtectionType.SelectedIndex = -1
            End If

        End With
    End Sub

    ''' <summary>
    ''' Gère l'affichage du cmb_InsulationType et des labels liés
    ''' </summary>
    Private Sub MAJ_InsulationType(myBeam As cls_Poutre)

        With myBeam.ParamFeu

            If .TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege Then

                Me.cmb_InsulationType.Enabled = Not (.Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint) 'on désactive le combobox si peinture intumescente

                Select Case .Protection
                    Case cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral,
                             cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Vermiculite,
                             cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerliteCement,
                                cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerlitePlaster
                        RemplirComboAvecTableau(Me.cmb_InsulationType, strInsulationTypeSpray)

                    Case cls_OptionsFeu.enu_TypeProtection.IntumescentPaint
                        Me.cmb_InsulationType.SelectedIndex = -1

                    Case cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite,
                             cls_OptionsFeu.enu_TypeProtection.BoardsSilicate,
                             cls_OptionsFeu.enu_TypeProtection.BoardsFibroCement,
                             cls_OptionsFeu.enu_TypeProtection.BoardsPlaster
                        RemplirComboAvecTableau(Me.cmb_InsulationType, strInsulationTypeBoards)

                End Select

                Select Case .Protection
                    Case cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral
                        Me.cmb_InsulationType.SelectedIndex = 0

                    Case cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Vermiculite
                        Me.cmb_InsulationType.SelectedIndex = 1

                    Case cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerliteCement
                        Me.cmb_InsulationType.SelectedIndex = 2

                    Case cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerlitePlaster
                        Me.cmb_InsulationType.SelectedIndex = 3

                    Case cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite
                        Me.cmb_InsulationType.SelectedIndex = 0

                    Case cls_OptionsFeu.enu_TypeProtection.BoardsSilicate
                        Me.cmb_InsulationType.SelectedIndex = 1

                    Case cls_OptionsFeu.enu_TypeProtection.BoardsFibroCement
                        Me.cmb_InsulationType.SelectedIndex = 2

                    Case cls_OptionsFeu.enu_TypeProtection.BoardsPlaster
                        Me.cmb_InsulationType.SelectedIndex = 3

                End Select


                '--> Gestion des propriétés thermiques qui dépendent du type d'isolation
                '
                Me.txt_Density.Enabled = True
                Me.txt_ThermalConductivity.Enabled = True
                Me.txt_SpecificHeat.Enabled = True

                Me.txt_Density.Text = GetStringInUnit(.Protection_MasseVol, Enu_TypeVariable.SansType, 3, 2, False)
                Me.txt_SpecificHeat.Text = GetStringInUnit(.Protection_ChaleurMassique, Enu_TypeVariable.SansType, 3, 2, False)

                If .Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint Then
                    Me.txt_ThermalConductivity.ReadOnly = False
                Else
                    Me.txt_ThermalConductivity.ReadOnly = True
                End If

                Me.txt_ThermalConductivity.Text = GetStringInUnit(.Protection_Conductivite, Enu_TypeVariable.SansType, 3, 3, False)

            Else

                Me.cmb_InsulationType.Enabled = False
                Me.cmb_InsulationType.SelectedIndex = -1

                Me.txt_Density.Enabled = False
                Me.txt_ThermalConductivity.Enabled = False
                Me.txt_SpecificHeat.Enabled = False

                Me.txt_Density.Text = ""
                Me.txt_ThermalConductivity.Text = ""
                Me.txt_SpecificHeat.Text = ""

            End If

        End With
    End Sub

    Private Sub MAJI_CalcuFeu(myBeam As cls_Poutre)

        'Gestion de l'affichage en fonction de si le calcul au feu est demandé ou non

        Dim lCalculFeu As Boolean = myBeam.ParamFeu.lCalculFeu

        cmb_SurfaceType.Enabled = lCalculFeu
        chk_AcierGalva.Enabled = lCalculFeu
        chk_ProtectionThermique.Enabled = lCalculFeu
        cmb_ProtectionType.Enabled = lCalculFeu
        cmb_InsulationType.Enabled = lCalculFeu And cmb_InsulationType.Enabled

        txt_Density.Enabled = lCalculFeu
        txt_ThermalConductivity.Enabled = lCalculFeu
        txt_SpecificHeat.Enabled = lCalculFeu

        chk_ArmaFroid.Enabled = lCalculFeu

    End Sub

#End Region

#Region "   Evènements "

    Private Sub chk_ArmaFroid_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ArmaFroid.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lArmaFormeeAFroid = chk_ArmaFroid.Checked

    End Sub

    Private Sub chk_CalculFeu_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CalculFeu.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsFeuN.BeamLoc.ParamFeu.lCalculFeu = chk_CalculFeu.Checked

        MAJI_CalcuFeu(Frm_OptionsFeuN.BeamLoc)

    End Sub

    Private Sub chk_AcierGalva_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AcierGalva.CheckedChanged
        If lBuild Then Exit Sub

        If Not Me.chk_AcierGalva.Checked Then
            Frm_OptionsFeuN.BeamLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
        Else
            Frm_OptionsFeuN.BeamLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise
        End If

    End Sub

    Private Sub chk_ProtectionThermique_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ProtectionThermique.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        If Me.chk_ProtectionThermique.Checked Then
            Frm_OptionsFeuN.BeamLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
        Else
            If Not Me.chk_AcierGalva.Checked Then
                Frm_OptionsFeuN.BeamLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
            Else
                Frm_OptionsFeuN.BeamLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise
            End If
        End If

        ErrorProvider_OptionsFeu.SetError(txt_ThermalConductivity, String.Empty)

        MAJI_TypeProtection(Frm_OptionsFeuN.BeamLoc)
        MAJ_ProtectionType(Frm_OptionsFeuN.BeamLoc)
        MAJ_InsulationType(Frm_OptionsFeuN.BeamLoc)

        lBuild = False
    End Sub

    Private Sub cmb_ProtectionType_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cmb_ProtectionType.SelectedIndexChanged
        If lBuild Then Exit Sub

        lBuild = True

        With Frm_OptionsFeuN.BeamLoc.ParamFeu

            Select Case cmb_ProtectionType.SelectedIndex
                Case 0 'Spray
                    Select Case cmb_InsulationType.SelectedIndex
                        Case 0
                            .Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral
                        Case 1
                            .Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Vermiculite
                        Case 2
                            .Protection = cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerliteCement
                        Case 3
                            .Protection = cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerlitePlaster
                        Case Else
                            .Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral
                    End Select
                Case 1 'Peinture intumescente
                    .Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint
                Case 2 'Boards
                    Select Case cmb_InsulationType.SelectedIndex
                        Case 0
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite
                        Case 1
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsSilicate
                        Case 2
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsFibroCement
                        Case 3
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsPlaster
                        Case Else
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite
                    End Select
            End Select

        End With

        ErrorProvider_OptionsFeu.SetError(txt_ThermalConductivity, String.Empty)

        If sender.name = Me.cmb_ProtectionType.Name Then _
        MAJ_InsulationType(Frm_OptionsFeuN.BeamLoc)

        lBuild = False

    End Sub

    Private Sub cmb_InsulationType_SelectedIndexChanged(sender As Object, e As EventArgs) _
      Handles cmb_InsulationType.SelectedIndexChanged
        If lBuild Then Exit Sub

        With Frm_OptionsFeuN.BeamLoc.ParamFeu

            Select Case cmb_ProtectionType.SelectedIndex
                Case 0 'Spray
                    Select Case cmb_InsulationType.SelectedIndex
                        Case 0
                            .Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral
                        Case 1
                            .Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Vermiculite
                        Case 2
                            .Protection = cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerliteCement
                        Case 3
                            .Protection = cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerlitePlaster
                        Case Else
                            .Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral
                    End Select
                Case 1 'Peinture intumescente
                    .Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint
                Case 2 'Boards
                    Select Case cmb_InsulationType.SelectedIndex
                        Case 0
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite
                        Case 1
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsSilicate
                        Case 2
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsFibroCement
                        Case 3
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsPlaster
                        Case Else
                            .Protection = cls_OptionsFeu.enu_TypeProtection.BoardsVermiculite
                    End Select
            End Select

        End With

    End Sub


#End Region


#Region " Dessins des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) _
        Handles img_Density.Paint, img_ThermalConductivity.Paint, img_SpecificHeat.Paint, img_EpProtec.Paint

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

            Case Me.img_Density.Name

                strSymbol = "r"
                strIndice = "p"

            Case Me.img_ThermalConductivity.Name

                strSymbol = "l"
                strIndice = "p"

            Case Me.img_SpecificHeat.Name

                strSymbol = "c"
                strIndice = "p"

                lGrec = False

            Case Me.img_EpProtec.Name

                strSymbol = "d"
                strIndice = "p"

                lGrec = False

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region


#Region " Evenements de saisie "

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) _
        Handles txt_ThermalConductivity.TextChanged, txt_EpProtec.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With Frm_OptionsFeuN.BeamLoc.ParamFeu
                Select Case sender.name

                    Case txt_ThermalConductivity.Name
                        .CustomLambdaP = ValeurUI

                    Case txt_EpProtec.Name
                        .EpProtection = ValeurUI

                End Select

            End With

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_OptionsFeu.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin, lValMax As Boolean
        Dim kUnit As Decimal = 1

        lValMin = True
        lValMax = True

        Select Case MyTxt.Name


            Case Me.txt_EpProtec.Name
                ValMin = 0
                ValMax = 2
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

            Case txt_ThermalConductivity.Name

                ValMin = 0.005
                ValMax = 0.012

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin / kUnit, lValMax, ValMax / kUnit)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_OptionsFeu, ValMin / kUnit, lValMin, ValMax / kUnit, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region


End Class