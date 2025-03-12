Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsFeu

#Region " Variables locales "

    Dim lBuild As Boolean

    Dim strSurfaceType, strProtectionType, strInsulationTypeSpray, strInsulationTypeBoards As String() 'cmb_SurfaceType

    Dim strTempRebars(2)

    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    Dim list_txtbox As New List(Of TextBox) 'liste des textboxs donc l'utilisateur peut changer la valeur 

    Dim lFrm_Valide As Boolean

    'Dim y_AcierGalva, y_ReductionConcreteStrenght, y_ArmaFroid, y_ArmaComp, y_DalleFEM, y_tDalleFEMmax As Decimal

#End Region

#Region "===Ouverture==="

    Private Sub Frm_OptionsFeu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Private Sub InitialiserFenetre()
        lBuild = True
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        PrepareFenetre()
        AfficherPoutreEnCours()
        MAJI_OptionsFEM()
        lBuild = False
    End Sub

    Private Sub PrepareFenetre()
        RemplirComboTempRebar()
    End Sub

    Private Sub InitialiserVariables()

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        list_txtbox.Add(txt_ThermalConductivity)
        list_txtbox.Add(txt_TimeIncrement)
        list_txtbox.Add(txt_ReferenceTemp)
        list_txtbox.Add(txt_FormFactor)
        'list_txtbox.Add(txt_EmissivitySteel)
        list_txtbox.Add(txt_EmissivityFire)
        list_txtbox.Add(txt_ConvectionFactor)
        list_txtbox.Add(txt_ShadowEffect)

    End Sub

    Private Sub GestionUnites()

        lbl_UnitDensity.Text = "kg/m3"

        lbl_UnitThermalCond.Text = "W/m.K"
        lbl_UnitSpecificHeat.Text = "J/kg.K"

        lbl_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        etq_UnitD2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        etq_UnitTime.Text = "s"
        etq_UnitTemp1.Text = "°C"
        etq_UnitTemp2.Text = "°C"

        etq_UnitU.Text = "%"

        'lbl_UnitBoltzmann.Text = "x 10E-8 W.m-2.K-4"
        'lbl_UnitConvectionFactor.Text = "W.m-2.K-1"
        ' lbl_UnitConvectionSlab.Text = "W.m-2.K-1"

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_FIREOPTIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== PARAMETRES POUTRES ==============================================================='

                Me.lbl_ParamPoutre.Text = Bloc("BEAMPARAM")

                '--> chk_CalculFeu

                chk_CalculFeu.Text = Bloc("FIRECALCULATION")

                '--> cmb_SurfaceType
                Me.lbl_SurfaceType.Text = Bloc("SURFACETYPE")

                ReDim strSurfaceType(1)
                strSurfaceType(0) = Bloc("PROTECTED")
                strSurfaceType(1) = Bloc("UNPROTECTED")

                RemplirComboAvecTableau(Me.cmb_SurfaceType, strSurfaceType)

                '--> chk_AcierGalva

                Me.chk_AcierGalva.Text = Bloc("GALVANISED")

                '--> cmb_ProtectionType

                Me.lbl_ProtectionType.Text = Bloc("PROTECTIONTYPE")

                ReDim strProtectionType(2)
                strProtectionType(0) = Bloc("SPRAY")
                strProtectionType(1) = Bloc("INTUMESCENTPAINT")
                strProtectionType(2) = Bloc("BOARDS")

                RemplirComboAvecTableau(Me.cmb_ProtectionType, strProtectionType)

                '--> cmb_InsulationType

                Me.lbl_InsulationType.Text = Bloc("INSULATIONTYPE")
                Me.chk_ProtectionThermique.Text = Bloc("PROTECTEDSTEEL")

                ReDim strInsulationTypeSpray(3)

                strInsulationTypeSpray(0) = Bloc("MINERALFIBRES")
                strInsulationTypeSpray(1) = Bloc("VERMICULITE")
                strInsulationTypeSpray(2) = Bloc("PERLITECEMENT")
                strInsulationTypeSpray(3) = Bloc("PERLITEPLASTER")

                ReDim strInsulationTypeBoards(3)

                strInsulationTypeBoards(0) = Bloc("VERMICULITEPERLITE")
                strInsulationTypeBoards(1) = Bloc("SILICATE")
                strInsulationTypeBoards(2) = Bloc("FIBROCEMENT")
                strInsulationTypeBoards(3) = Bloc("PLASTER")

                lbl_Density.Text = Bloc("DENSITY")
                lbl_ThermalConductivity.Text = Bloc("THERMALCONDUC")
                lbl_SpecificHeat.Text = Bloc("SPECIFICHEAT")

                lbl_EpProtec.Text = Bloc("PROTECTIONTH")

                '--> chk_ReductionConcreteStrenght

                Me.chk_ReductionConcreteStrenght.Text = Bloc("CONCRETEREDUC250")

                '--> chk_ArmaFroid

                chk_ArmaFroid.Text = Bloc("ARMAFROID")

                '=== OPTIONS DE CALCUL ==============================================================='

                Me.lbl_CalculOptions.Text = Bloc("CALCULOPTIONS")

                '--> chk_ArmaComp

                chk_ArmaComp.Text = Bloc("ARMACOMP")

                '--> chk_DalleFEM

                chk_DalleFEM.Text = Bloc("DALLEFEM")
                lbl_tDalleFEMmax.Text = Bloc("TDALLEFEM")

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

                Me.lbl_TempRebars.Text = Bloc("TEMPREBARS")
                strTempRebars(0) = Bloc("AVERAGETEMP")
                strTempRebars(1) = Bloc("MAXTEMP2")
                strTempRebars(2) = Bloc("AXISTEMP")

                '=== OPTIONS POUR LE CALCUL NUMERIQUE DE L'ECHAUFFEMENT dE LA DALLE

                Me.chk_ANFrance.Text = Bloc("FRENCHNA")
                Me.chk_RhoCconstante.Text = Bloc("CONSTANTRHOC")
                Me.lbl_TeneurEau.Text = Bloc("MOISTURECONTENT")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_ParamPoutre.BackColor = CouleurBackBandeaux
        Me.lbl_ParamPoutre.ForeColor = CouleurForeBandeaux

        Me.lbl_CalculOptions.BackColor = CouleurBackBandeaux
        Me.lbl_CalculOptions.ForeColor = CouleurForeBandeaux

        Me.lbl_ParamCalcul.BackColor = CouleurBackBandeaux
        Me.lbl_ParamCalcul.ForeColor = CouleurForeBandeaux

        Me.txt_Density.ReadOnly = True
        Me.txt_SpecificHeat.ReadOnly = True

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

    Private Sub AfficherPoutreEnCours()

        With MyPoutreLoc.ParamFeu

            '--> Partie qui concerne les paramètres de la poutre

            chk_CalculFeu.Checked = .lCalculFeu
            Affiche_SurfaceType()

            Me.chk_ProtectionThermique.Checked = (MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
            Me.chk_AcierGalva.Checked = (MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise)

            MAJ_ProtectionType()
            MAJ_InsulationType()

            Me.chk_ArmaFroid.Checked = MyPoutreLoc.ParamFeu.lArmaFormeeAFroid
            Me.chk_DalleFEM.Checked = MyPoutreLoc.ParamFeu.lDalleFEM
            Me.txt_EpProtec.Text = GetStringInUnitN(.EpProtection, Enu_TypeVariable.Dimension, 3, 2, Enu_AfficheUnite.Non, True)

            GestionPositionElements() 'Gère les positions des éléments dans la partie de droite
            MAJI_TypeProtection()

            '--> Options de calcul

            MAJI_FEM()
            Me.txt_tDalleFEMmax.Text = GetStringInUnitN(.tDalleEFmax, Enu_TypeVariable.Dimension, 3, 2, Enu_AfficheUnite.Non, True)
            Me.chk_ArmaComp.Checked = MyPoutreLoc.ParamFeu.lArmaCompression
            Me.chk_ReductionConcreteStrenght.Checked = MyPoutreLoc.ParamFeu.lReductionConcreteStrength
            Select Case MyPoutreLoc.ParamFeu.MethodTempArma
                Case cls_OptionsFeu.enuTypeInterpoleTempArma.Axe : Me.cmb_TempRebars.SelectedIndex = 2
                Case cls_OptionsFeu.enuTypeInterpoleTempArma.Maximale : Me.cmb_TempRebars.SelectedIndex = 1
                Case cls_OptionsFeu.enuTypeInterpoleTempArma.Moyenne : Me.cmb_TempRebars.SelectedIndex = 0
            End Select

            '--> Partie qui concerne les paramètres de calcul

            Affichage_ParamCalcul()

            '--> Options FEM

            Me.chk_RhoCconstante.Checked = Not .lRhoCvar
            Me.chk_ANFrance.Checked = .lANFrance

            Me.txt_U.Text = GetStringInUnitN(.TeneurU, Enu_TypeVariable.SansType, 3, 2, Enu_AfficheUnite.Non, True)

        End With

    End Sub

    ''' <summary>
    ''' Gère l'affichage du cmb_SurfaceType
    ''' </summary>
    Private Sub Affiche_SurfaceType()

        With MyPoutreLoc.ParamFeu

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
    Private Sub MAJ_ProtectionType()

        With MyPoutreLoc.ParamFeu

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
    Private Sub MAJ_InsulationType()

        With MyPoutreLoc.ParamFeu

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

    ''' <summary>
    ''' Gère les valeurs dans les textbox dans la partie ParamCalcul
    ''' </summary>
    Private Sub Affichage_ParamCalcul()
        With MyPoutreLoc.ParamFeu
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

    Private Sub MAJI_TypeProtection()
        'Gestion de l'affichage en fonction de si l'acier est protégé au feu ou non

        Dim lProtege As Boolean = (MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)

        Me.pan_Protection.Visible = lProtege

        Me.pan_Protection.Top = Me.chk_AcierGalva.Top

    End Sub

    ''' <summary>
    ''' Gère la position des éléments dans le panneau de droite en fonction du type de surface de la poutre locale 
    ''' </summary>
    Private Sub GestionPositionElements()

        'Gestion de l'affichage en fonction de si le calcul au feu est demandé ou non

        Dim lCalculFeu As Boolean = MyPoutreLoc.ParamFeu.lCalculFeu

        cmb_SurfaceType.Enabled = lCalculFeu
        chk_AcierGalva.Enabled = lCalculFeu
        cmb_ProtectionType.Enabled = lCalculFeu
        cmb_InsulationType.Enabled = lCalculFeu And cmb_InsulationType.Enabled

        txt_Density.Enabled = lCalculFeu
        txt_ThermalConductivity.Enabled = lCalculFeu
        txt_SpecificHeat.Enabled = lCalculFeu

        chk_ReductionConcreteStrenght.Enabled = lCalculFeu
        chk_ArmaFroid.Enabled = lCalculFeu
        chk_ArmaComp.Enabled = lCalculFeu
        chk_DalleFEM.Enabled = lCalculFeu
        txt_tDalleFEMmax.Enabled = lCalculFeu

    End Sub

    Private Sub MAJI_FEM()
        Dim lDalleFEM As Boolean = MyPoutreLoc.ParamFeu.lDalleFEM

        lbl_tDalleFEMmax.Enabled = lDalleFEM
        img_tDalleFEMmax.Enabled = lDalleFEM
        txt_tDalleFEMmax.Enabled = lDalleFEM
        lbl_UnitD1.Enabled = lDalleFEM
    End Sub

    Private Sub RemplirComboTempRebar()

        Me.cmb_TempRebars.Items.Clear()
        Me.cmb_TempRebars.Items.AddRange(strTempRebars)
        Me.cmb_TempRebars.SelectedIndex = 0

    End Sub

#End Region

#Region " Evenements "

    'Private Sub cmb_SurfaceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SurfaceType.SelectedIndexChanged, chk_AcierGalva.CheckedChanged

    '    If lBuild Then Exit Sub

    '    lBuild = True

    '    With myBeamLoc.ParamFeu

    '        Select Case cmb_SurfaceType.SelectedIndex
    '            Case 0
    '                .TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
    '            Case 1
    '                If Not chk_AcierGalva.Checked Then
    '                    .TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
    '                Else
    '                    .TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise
    '                End If
    '        End Select

    '    End With

    '    ErrorProvider_Frm_OptionsFeu.SetError(txt_ThermalConductivity, String.Empty)

    '    AfficherPoutreEnCours()

    '    lBuild = False
    'End Sub

    Private Sub chk_AcierGalva_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AcierGalva.CheckedChanged
        If lBuild Then Exit Sub

        If Not Me.chk_AcierGalva.Checked Then
            MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
        Else
            MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise
        End If

    End Sub

    Private Sub chk_ProtectionThermique_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ProtectionThermique.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        If Me.chk_ProtectionThermique.Checked Then
            MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
        Else
            If Not Me.chk_AcierGalva.Checked Then
                MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
            Else
                MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise
            End If
        End If

        ErrorProvider_Frm_OptionsFeu.SetError(txt_ThermalConductivity, String.Empty)

        AfficherPoutreEnCours()

        lBuild = False
    End Sub

    Private Sub cmb_ProtectionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ProtectionType.SelectedIndexChanged, cmb_InsulationType.SelectedIndexChanged
        If lBuild Then Exit Sub

        lBuild = True

        With MyPoutreLoc.ParamFeu

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

        ErrorProvider_Frm_OptionsFeu.SetError(txt_ThermalConductivity, String.Empty)

        AfficherPoutreEnCours()

        lBuild = False
    End Sub

    Private Sub chk_CalculFeu_CheckedChanged(sender As Object, e As EventArgs) Handles chk_CalculFeu.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        MyPoutreLoc.ParamFeu.lCalculFeu = chk_CalculFeu.Checked
        AfficherPoutreEnCours()

        lBuild = False
    End Sub


    Private Sub chk_ReductionConcreteStrenght_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ReductionConcreteStrenght.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        MyPoutreLoc.ParamFeu.lReductionConcreteStrength = chk_ReductionConcreteStrenght.Checked

        ErrorProvider_Frm_OptionsFeu.SetError(txt_ThermalConductivity, String.Empty)

        Me.chk_ReductionConcreteStrenght.Checked = MyPoutreLoc.ParamFeu.lReductionConcreteStrength

        lBuild = False

    End Sub

    Private Sub chk_ArmaFroid_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ArmaFroid.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        MyPoutreLoc.ParamFeu.lArmaFormeeAFroid = chk_ArmaFroid.Checked

        lBuild = False

    End Sub

    Private Sub chk_ArmaComp_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ArmaComp.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        MyPoutreLoc.ParamFeu.lArmaCompression = chk_ArmaComp.Checked

        lBuild = False
    End Sub

    Private Sub chk_DalleFEM_CheckedChanged(sender As Object, e As EventArgs) Handles chk_DalleFEM.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        MyPoutreLoc.ParamFeu.lDalleFEM = chk_DalleFEM.Checked

        AfficherPoutreEnCours()
        MAJI_OptionsFEM()

        lBuild = False
    End Sub


    Private Sub chk_RhoCconstante_CheckedChanged(sender As Object, e As EventArgs) Handles chk_RhoCconstante.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.ParamFeu.lRhoCvar = Not chk_RhoCconstante.Checked

    End Sub

    Private Sub chk_ANFrance_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ANFrance.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.ParamFeu.lANFrance = chk_ANFrance.Checked
    End Sub

    Private Sub MAJI_OptionsFEM()

        Me.pan_OptionsFEM.Visible = MyPoutreLoc.ParamFeu.lDalleFEM

    End Sub

    Private Sub cmb_TempRebars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TempRebars.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TempRebars.SelectedIndex
            Case 0 : MyPoutreLoc.ParamFeu.MethodTempArma = cls_OptionsFeu.enuTypeInterpoleTempArma.Moyenne
            Case 1 : MyPoutreLoc.ParamFeu.MethodTempArma = cls_OptionsFeu.enuTypeInterpoleTempArma.Maximale
            Case 2 : MyPoutreLoc.ParamFeu.MethodTempArma = cls_OptionsFeu.enuTypeInterpoleTempArma.Axe
        End Select

    End Sub

#End Region

#Region " Evenements de saisie "

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_ThermalConductivity.TextChanged, txt_tDalleFEMmax.TextChanged, txt_TimeIncrement.TextChanged, txt_ReferenceTemp.TextChanged,
        txt_FormFactor.TextChanged, txt_EmissivityFire.TextChanged, txt_ConvectionFactor.TextChanged,
        txt_ShadowEffect.TextChanged, txt_EpProtec.TextChanged, txt_EmissiviteBeton.TextChanged, txt_U.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With MyPoutreLoc.ParamFeu
                Select Case sender.name
                    Case txt_U.Name
                        .TeneurU = ValeurUI
                    Case txt_ThermalConductivity.Name
                        .CustomLambdaP = ValeurUI
                    Case txt_tDalleFEMmax.Name
                        .tDalleEFmax = ValeurUI
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
                    Case txt_EpProtec.Name
                        .EpProtection = ValeurUI
                End Select

            End With

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_Frm_OptionsFeu.SetError(MyTxt, String.Empty)

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

            Case Me.txt_EpProtec.Name
                ValMin = 0
                ValMax = 2
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

            Case txt_ThermalConductivity.Name

                ValMin = 0.005
                ValMax = 0.012

            Case txt_TimeIncrement.Name
                ValMin = 1 's
                If MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege Then
                    ValMax = 30 's
                Else
                    ValMax = 5 's
                End If

            Case txt_tDalleFEMmax.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = 1 / 1000 '1 mm
                lValMax = False

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
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_OptionsFeu, ValMin / kUnit, lValMin, ValMax / kUnit, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region

#Region " Fermeture "

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False

        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        lFrm_Valide = True

        Dim ValeurUI As Decimal

        For Each txtbox_loc As TextBox In list_txtbox
            If txtbox_loc.Name = txt_ThermalConductivity.Name And (txtbox_loc.ReadOnly Or Not txtbox_loc.Enabled Or Not txtbox_loc.Visible) Then
                ErrorProvider_Frm_OptionsFeu.SetError(txtbox_loc, String.Empty)
                Continue For
            End If
            VerificationSaisie(txtbox_loc, ValeurUI)
            If Not ErrorProvider_Frm_OptionsFeu.GetError(txtbox_loc) = String.Empty Then
                lFrm_Valide = False
                Exit For
            End If
        Next
        Return lFrm_Valide
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours).ParamFeu

            '--> Partie paramètres de la poutre

            GereTransfertValeur(MyPoutreLoc.ParamFeu.lCalculFeu, .lCalculFeu, lModif)

            If MyPoutreLoc.ParamFeu.TypeSurface <> .TypeSurface Then
                lModif = True
                .TypeSurface = MyPoutreLoc.ParamFeu.TypeSurface
            End If

            If MyPoutreLoc.ParamFeu.Protection <> .Protection Then
                lModif = True
                .Protection = MyPoutreLoc.ParamFeu.Protection
            End If

            If MyPoutreLoc.ParamFeu.Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint Then
                GereTransfertValeur(MyPoutreLoc.ParamFeu.CustomLambdaP, .CustomLambdaP, lModif)
            End If
            GereTransfertValeur(MyPoutreLoc.ParamFeu.EpProtection, .EpProtection, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.lArmaFormeeAFroid, .lArmaFormeeAFroid, lModif)

            '--> Options de calcul

            GereTransfertValeur(MyPoutreLoc.ParamFeu.lArmaCompression, .lArmaCompression, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.lDalleFEM, .lDalleFEM, lModif)
            If .lDalleFEM Then
                GereTransfertValeur(MyPoutreLoc.ParamFeu.tDalleEFmax, .tDalleEFmax, lModif)
            End If
            GereTransfertValeur(MyPoutreLoc.ParamFeu.lReductionConcreteStrength, .lReductionConcreteStrength, lModif)
            If MyPoutreLoc.ParamFeu.MethodTempArma <> .MethodTempArma Then
                lModif = True
                .MethodTempArma = MyPoutreLoc.ParamFeu.MethodTempArma
            End If

            '--> Partie paramètres de calcul

            GereTransfertValeur(MyPoutreLoc.ParamFeu.DeltaTCalcul, .DeltaTCalcul, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.TempRef, .TempRef, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.PhiViewFactor, .PhiViewFactor, lModif)
            'GereTransfertValeur(myBeamLoc.ParamFeu.EmissivitySteel, .EmissivitySteel, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.EmissivityFire, .EmissivityFire, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.EmissivityC, .EmissivityC, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.ConvectionCoef, .ConvectionCoef, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.ksh, .ksh, lModif)

            '--> Options calcul FEM

            GereTransfertValeur(MyPoutreLoc.ParamFeu.lANFrance, .lANFrance, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.lRhoCvar, .lRhoCvar, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.TeneurU, .TeneurU, lModif)

        End With

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        ErrorProvider_Frm_OptionsFeu.Clear()
    End Sub

#End Region

#Region " Dessins des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_Density.Paint, img_ThermalConductivity.Paint, img_SpecificHeat.Paint, img_tDalleFEMmax.Paint,
     img_Boltzmann.Paint, img_TimeIncrement.Paint, img_ReferenceTemp.Paint, img_MaxTemp.Paint, img_FormFactor.Paint,
     img_EmissivityFire.Paint, img_ConvectionFactor.Paint, img_ShadowEffect.Paint, img_ConvectionSlab.Paint, img_ConcreteResistance.Paint, img_EpProtec.Paint, img_EmissiviteBeton.Paint, img_U.Paint

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

            Case Me.img_tDalleFEMmax.Name

                strSymbol = "t"
                strIndice = "max"

                lGrec = False

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