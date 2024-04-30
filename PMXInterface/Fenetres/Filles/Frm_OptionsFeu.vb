Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsFeu

#Region " Variables locales "
    Dim lBuild As Boolean

    Dim strSurfaceType, strProtectionType, strInsulationTypeSpray, strInsulationTypeBoards As String() 'cmb_SurfaceType

    Dim MyPoutreLoc As New cls_Poutre

    Dim list_txtbox As New List(Of TextBox) 'liste des textboxs donc l'utilisateur peut changer la valeur 

    Dim lFrm_Valide As Boolean

#End Region

#Region " Ouverture "

    Private Sub Frm_OptionsFeu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Private Sub InitialiserFenetre()
        lBuild = True
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        list_txtbox.Add(txt_LambdaP)
        list_txtbox.Add(txt_TimeIncrement)
        list_txtbox.Add(txt_ReferenceTemp)
        list_txtbox.Add(txt_FormFactor)
        list_txtbox.Add(txt_EmissivitySteel)
        list_txtbox.Add(txt_EmissivityFire)
        list_txtbox.Add(txt_ConvectionFactor)
        list_txtbox.Add(txt_ShadowEffect)

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

                '--> cmb_SurfaceType
                Me.lbl_SurfaceType.Text = Bloc("SURFACETYPE")

                ReDim strSurfaceType(2)
                strSurfaceType(0) = Bloc("PROTECTED")
                strSurfaceType(1) = Bloc("UNPROTECTED")
                strSurfaceType(2) = Bloc("GALVANISED")

                RemplirComboAvecTableau(Me.cmb_SurfaceType, strSurfaceType)

                '--> cmb_ProtectionType

                Me.lbl_ProtectionType.Text = Bloc("PROTECTIONTYPE")

                ReDim strProtectionType(2)
                strProtectionType(0) = Bloc("SPRAY")
                strProtectionType(1) = Bloc("INTUMESCENTPAINT")
                strProtectionType(2) = Bloc("BOARDS")

                RemplirComboAvecTableau(Me.cmb_ProtectionType, strProtectionType)

                '--> cmb_InsulationType

                Me.lbl_InsulationType.Text = Bloc("INSULATIONTYPE")

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

                lbl_UnitDensity.Text = "kg/m3"
                lbl_UnitThermalConduc.Text = "W.m-1.K-1"
                lbl_UnitSpecificHeat.Text = "J.kg-1.K-1"

                '--> chk_ReductionConcreteStrenght

                Me.chk_ReductionConcreteStrenght.Text = Bloc("CONCRETEREDUC250")


                '=== PARAMETRES CALCUL ==============================================================='

                Me.lbl_ParamCalcul.Text = Bloc("CALCULPARAM")

                lbl_Boltzmann.Text = Bloc("BOLTZMANN")
                lbl_TimeIncrement.Text = Bloc("TIMEINCREMENT")

                lbl_ReferenceTemp.Text = Bloc("REFERENCETEMP")
                lbl_MaxTemp.Text = Bloc("MAXTEMP")
                lbl_FormFactor.Text = Bloc("FORMFACTOR")
                lbl_EmissivitySteel.Text = Bloc("EMISSIVITYSTEELSURF")
                lbl_EmissivityFire.Text = Bloc("EMISSIVITYFIRE")
                lbl_ConvectionFactor.Text = Bloc("CONVECTIONFACTOR")
                lbl_ShadowEffect.Text = Bloc("SHADOWEFFECT")
                lbl_ConvectionSlab.Text = Bloc("CONVECTIONSLAB")
                lbl_ConcreteResistance.Text = Bloc("CONCRETEFACTOR")

                lbl_UnitBoltzmann.Text = "x 10E-8 W.m-2.K-4"
                lbl_UnitTimeIncrement.Text = "s"
                lbl_UnitReferenceTemp.Text = "°C"
                lbl_UnitMaxTemp.Text = "°C"
                lbl_UnitFormFactor.Text = ""
                lbl_UnitEmissivitySteel.Text = ""
                lbl_UnitEmissivityFire.Text = ""
                lbl_UnitConvectionFactor.Text = "W.m-2.K-1"
                lbl_UnitShadowEffect.Text = ""
                lbl_UnitConvectionSlab.Text = "W.m-2.K-1"
                lbl_UnitConcreteResistance.Text = ""

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

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

        Me.lbl_ParamCalcul.BackColor = CouleurBackBandeaux
        Me.lbl_ParamCalcul.ForeColor = CouleurForeBandeaux

        Me.txt_rhoP.ReadOnly = True
        Me.txt_SpecificHeat.ReadOnly = True

        Me.txt_Boltzmann.ReadOnly = True
        Me.txt_TimeIncrement.ReadOnly = False
        Me.txt_ReferenceTemp.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_MaxTemp.ReadOnly = True
        Me.txt_FormFactor.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_EmissivitySteel.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_EmissivityFire.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_ConvectionFactor.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_ShadowEffect.ReadOnly = Not LogicielOptions.lExpert
        Me.txt_ConvectionSlab.ReadOnly = True
        Me.txt_ConcreteResistance.ReadOnly = True

    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyPoutreLoc.ParamFeu


            '--> Partie qui concerne les paramètres de la poutre

            MAJ_SurfaceType()
            MAJ_ProtectionType()
            MAJ_InsulationType()
            MAJ_ReductionConcreteStrenght()

            '--> Partie qui concerne les paramètres de calcul

            MAJ_ParamCalcul()

        End With

    End Sub

    ''' <summary>
    ''' Gère l'affichage du cmb_SurfaceType
    ''' </summary>
    Private Sub MAJ_SurfaceType()

        With MyPoutreLoc.ParamFeu

            Select Case .TypeSurface
                Case cls_OptionsFeu.enu_TypeSurface.Protege
                    Me.cmb_SurfaceType.SelectedIndex = 0
                Case cls_OptionsFeu.enu_TypeSurface.AcierNu
                    Me.cmb_SurfaceType.SelectedIndex = 1
                Case cls_OptionsFeu.enu_TypeSurface.Galvanise
                    Me.cmb_SurfaceType.SelectedIndex = 2
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
                Me.txt_rhoP.Enabled = True
                Me.txt_LambdaP.Enabled = True
                Me.txt_SpecificHeat.Enabled = True

                Me.txt_rhoP.Text = GetStringInUnit(.Protection_MasseVol, Enu_TypeVariable.SansType, 3, 2, False)
                Me.txt_SpecificHeat.Text = GetStringInUnit(.Protection_Conductivite, Enu_TypeVariable.SansType, 3, 2, False)

                If .Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint Then
                    Me.txt_LambdaP.ReadOnly = False
                Else
                    Me.txt_LambdaP.Text = GetStringInUnit(.Protection_Conductivite, Enu_TypeVariable.SansType, 3, 2, False)
                    Me.txt_LambdaP.ReadOnly = True
                End If

            Else

                Me.cmb_InsulationType.Enabled = False

                Me.txt_rhoP.Enabled = False
                Me.txt_LambdaP.Enabled = False
                Me.txt_SpecificHeat.Enabled = False

                Me.txt_rhoP.Text = ""
                Me.txt_LambdaP.Text = ""
                Me.txt_SpecificHeat.Text = ""

            End If

        End With
    End Sub

    ''' <summary>
    ''' Gère l'état du chk_ReductionConcreteStrenght
    ''' </summary>
    Private Sub MAJ_ReductionConcreteStrenght()
        Me.chk_ReductionConcreteStrenght.Checked = MyPoutreLoc.ParamFeu.lReductionConcreteStrenght
    End Sub


    ''' <summary>
    ''' Gère les valeurs dans les textbox dans la partie ParamCalcul
    ''' </summary>
    Private Sub MAJ_ParamCalcul()
        With MyPoutreLoc.ParamFeu
            Me.txt_Boltzmann.Text = GetStringInUnit(.BOLTZMANN * 10 ^ 8, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_TimeIncrement.Text = GetStringInUnit(.DeltaTCalcul, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ReferenceTemp.Text = GetStringInUnit(.TempRef, Enu_TypeVariable.Temperature, 3, 2, False)
            Me.txt_MaxTemp.Text = GetStringInUnit(.TempMax, Enu_TypeVariable.Temperature, 3, 2, False)
            Me.txt_FormFactor.Text = GetStringInUnit(.PhiViewFactor, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_EmissivitySteel.Text = GetStringInUnit(.EmissivitySteel, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_EmissivityFire.Text = GetStringInUnit(.EmissivityFire, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ConvectionFactor.Text = GetStringInUnit(.ConvectionCoef, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ShadowEffect.Text = GetStringInUnit(.ksh, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ConvectionSlab.Text = GetStringInUnit(.ConvectionCoefDalle, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_ConcreteResistance.Text = GetStringInUnit(.AlphaSlab, Enu_TypeVariable.SansType, 3, 2, False)
        End With
    End Sub

#End Region

#Region " Evenements "

    Private Sub cmb_SurfaceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SurfaceType.SelectedIndexChanged

        If lBuild Then Exit Sub

        lBuild = True

        With MyPoutreLoc.ParamFeu

            Select Case cmb_SurfaceType.SelectedIndex
                Case 0
                    .TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
                Case 1
                    .TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
                Case 2
                    .TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise
            End Select

        End With

        ErrorProvider_Frm_OptionsFeu.SetError(txt_LambdaP, String.Empty)

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
                    End Select
            End Select

        End With

        ErrorProvider_Frm_OptionsFeu.SetError(txt_LambdaP, String.Empty)

        AfficherPoutreEnCours()

        lBuild = False
    End Sub


    Private Sub chk_ReductionConcreteStrenght_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ReductionConcreteStrenght.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        MyPoutreLoc.ParamFeu.lReductionConcreteStrenght = chk_ReductionConcreteStrenght.Checked

        ErrorProvider_Frm_OptionsFeu.SetError(txt_LambdaP, String.Empty)

        MAJ_ReductionConcreteStrenght()

        lBuild = False

    End Sub


#End Region

#Region " Evenements de saisie "

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_LambdaP.TextChanged, txt_TimeIncrement.TextChanged, txt_ReferenceTemp.TextChanged,
        txt_FormFactor.TextChanged, txt_EmissivitySteel.TextChanged, txt_EmissivityFire.TextChanged, txt_ConvectionFactor.TextChanged, txt_ShadowEffect.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With MyPoutreLoc.ParamFeu
                Select Case sender.name
                    Case txt_LambdaP.Name
                        .CustomLambdaP = ValeurUI
                    Case txt_TimeIncrement.Name
                        .DeltaTCalcul = ValeurUI
                    Case txt_ReferenceTemp.Name
                        .TempRef = ValeurUI
                    Case txt_FormFactor.Name
                        .PhiViewFactor = ValeurUI
                    Case txt_EmissivitySteel.Name
                        .EmissivitySteel = ValeurUI
                    Case txt_EmissivityFire.Name
                        .EmissivityFire = ValeurUI
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
        ErrorProvider_Frm_OptionsFeu.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin, lValMax As Boolean

        lValMin = True
        lValMax = True

        Select Case MyTxt.Name
            Case txt_LambdaP.Name

                ValMin = 0.005
                ValMax = 0.012

            Case txt_TimeIncrement.Name
                ValMin = 1 's
                If MyPoutreLoc.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege Then
                    ValMax = 30 's
                Else
                    ValMax = 5 's
                End If

            Case txt_ReferenceTemp.Name
                lValMin = False
                lValMax = False

            Case txt_FormFactor.Name, txt_EmissivitySteel.Name, txt_EmissivityFire.Name, txt_ShadowEffect.Name
                ValMin = 0
                ValMax = 1

            Case txt_ConvectionFactor.Name
                ValMin = 0
                lValMax = False

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_OptionsFeu, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text)
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
            If txtbox_loc.Name = txt_LambdaP.Name And (txtbox_loc.ReadOnly Or Not txtbox_loc.Enabled) Then
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

            GereTransfertValeur(MyPoutreLoc.ParamFeu.lReductionConcreteStrenght, .lReductionConcreteStrenght, lModif)

            '--> Partie paramètres de calcul

            GereTransfertValeur(MyPoutreLoc.ParamFeu.DeltaTCalcul, .DeltaTCalcul, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.TempRef, .TempRef, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.PhiViewFactor, .PhiViewFactor, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.EmissivitySteel, .EmissivitySteel, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.EmissivityFire, .EmissivityFire, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.ConvectionCoef, .ConvectionCoef, lModif)
            GereTransfertValeur(MyPoutreLoc.ParamFeu.ksh, .ksh, lModif)

        End With

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        ErrorProvider_Frm_OptionsFeu.Clear()
    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_rhoP.Paint, img_LambdaP.Paint, img_SpecificHeat.Paint,
     img_Boltzmann.Paint, img_TimeIncrement.Paint, img_ReferenceTemp.Paint, img_MaxTemp.Paint, img_FormFactor.Paint, img_EmissivitySteel.Paint,
     img_EmissivityFire.Paint, img_ConvectionFactor.Paint, img_ShadowEffect.Paint, img_ConvectionSlab.Paint, img_ConcreteResistance.Paint

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

            Case Me.img_rhoP.Name

                strSymbol = "r"
                strIndice = "p"

            Case Me.img_LambdaP.Name

                strSymbol = "l"
                strIndice = "p"

            Case Me.img_SpecificHeat.Name

                strSymbol = "c"
                strIndice = "p"

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

            Case Me.img_EmissivitySteel.Name

                strSymbol = "e"
                strIndice = "m"

            Case Me.img_EmissivityFire.Name

                strSymbol = "e"
                strIndice = "f"

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

End Class