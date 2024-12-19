Imports PMXMoteur2

Public Class Frm_OptionsCalculCalcul

#Region " Variables locales "


    Const BALISE As String = "OPTCALCALCUL"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

    Dim strSymbolJour As String = "d"

    Dim tabNorme(1) As String

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        RemplirCombos()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Calcul.Text = MyBloc("TITLE")

            Me.lbl_Norme.Text = MyBloc("STANDARD")
            tabNorme(0) = MyBloc("ENGEN1")
            tabNorme(1) = MyBloc("ENGEN2")

            Me.lbl_CrossSectionProperties.Text = MyBloc("SECTIONPROP")
            Me.chk_RebarsInCompression.Text = MyBloc("COMPRESSIONREBARS")
            Me.chk_SimplifiedEffectiveW.Text = MyBloc("SIMPLIFIEDEFFW")

            Me.lbl_YoungRebars.Text = MyBloc("YOUNGSREBAR")
            Me.lbl_DeltaCDev.Text = MyBloc("DELTACDEV")

            Me.lbl_Discretisation.Text = MyBloc("MODEL")
            Me.lbl_DistanceMaxNoeuds.Text = MyBloc("NODESPACING")
            Me.lbl_NbNodes.Text = MyBloc("NBNODES")
            Me.lbl_Console.Text = MyBloc("PERCANTILEVERSPAN")
            Me.lbl_TraveesI.Text = MyBloc("PERINTSPAN")

            Me.lbl_LoadCases.Text = MyBloc("LOADCASES")
            Me.lbl_CreepMultiplier.Text = MyBloc("CREEPMULT")
            Me.lbl_PermanentLC.Text = MyBloc("PERMANENTLC")
            Me.lbl_ShrinkageLC.Text = MyBloc("SHRINKAGELC")

            Me.lbl_TimeT0.Text = MyBloc("TIMETZERO")
            Me.lbl_Dalle.Text = MyBloc("SLAB")
            Me.lbl_Enrobage.Text = MyBloc("ENCASEMENT")
            Me.lbl_G1.Text = "G1"   ' MyBloc("SELFWEIGHT")
            Me.lbl_G2.Text = "G2"   ' MyBloc("OTHERPERMANENT")
            Me.lbl_SH.Text = "SH"   ' MyBloc("SHRINKAGE")
            strSymbolJour = MyBloc("SYMBOLFORDAY")

            Me.lbl_ShearBuckling.Text = MyBloc("ETAW")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Calcul.Dock = DockStyle.Fill

        Me.lbl_Calcul.BackColor = CouleurBackBandeaux
        Me.lbl_Calcul.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_PsiLG, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_PsiLSH, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_t0SHEnrob, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_t0SHDalle, LogicielOptions.lExpert)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitEs.Text = LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)
        Me.etq_UnitDeltaCDev.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitJour1.Text = strSymbolJour
        Me.etq_UnitJour2.Text = strSymbolJour
        Me.etq_UnitJour3.Text = strSymbolJour
        Me.etq_UnitJour4.Text = strSymbolJour
        Me.etq_UnitJour5.Text = strSymbolJour
        Me.etq_UnitJour6.Text = strSymbolJour

    End Sub

    Private Sub AfficherOptionsEnCours()

        '--> Norme

        Select Case LocalOptionsCalcul.Norme
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG1
                Me.cmb_Norme.SelectedIndex = 0
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG2
                Me.cmb_Norme.SelectedIndex = Math.Min(1, Me.cmb_Norme.Items.Count - 1)
        End Select

        '--> Modélisation

        Me.txt_EspNoeuds.Text = GetStringInUnit(LocalOptionsCalcul.dMaxNodes, Enu_TypeVariable.Longueur, 4, 3, False)
        Me.txt_NbMiniNTravee.Text = Format(LocalOptionsCalcul.nbMinNodesTravee, "0")
        Me.txt_NbMiniNConsole.Text = Format(LocalOptionsCalcul.nbMinNodesConsole, "0")

        '--> Propriétés des sections

        Me.chk_RebarsInCompression.Checked = LocalOptionsCalcul.lCompressionArma
        Me.chk_SimplifiedEffectiveW.Checked = LocalOptionsCalcul.lLargeurEfficaceSimplifiee
        Me.txt_Es.Text = GetStringInUnit(LocalOptionsCalcul.EsArmatures, Enu_TypeVariable.ModuleY, 4, 2, False)
        Me.txt_DeltaCDev.Text = GetStringInUnit(LocalOptionsCalcul.DeltaCDev, Enu_TypeVariable.Dimension, 4, 2, False)

        '--> Cas de charge

        Me.txt_PsiLG.Text = GetStringInUnit(LocalOptionsCalcul.PsiLPermanent, Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_PsiLSH.Text = GetStringInUnit(LocalOptionsCalcul.PsiLRetrait, Enu_TypeVariable.SansType, 4, 2, False)

        Me.txt_t0G1Dalle.Text = GetStringInUnit(LocalOptionsCalcul.TimeT0G1(0), Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_t0G1Enrob.Text = GetStringInUnit(LocalOptionsCalcul.TimeT0G1(1), Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_t0G2Dalle.Text = GetStringInUnit(LocalOptionsCalcul.TimeT0G2(0), Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_t0G2Enrob.Text = GetStringInUnit(LocalOptionsCalcul.TimeT0G2(1), Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_t0SHDalle.Text = GetStringInUnit(LocalOptionsCalcul.TimeT0SH(0), Enu_TypeVariable.SansType, 4, 2, False)
        Me.txt_t0SHEnrob.Text = GetStringInUnit(LocalOptionsCalcul.TimeT0SH(1), Enu_TypeVariable.SansType, 4, 2, False)

        '--> Voilement par cisaillement 

        Me.txt_eta.Text = GetStringInUnit(LocalOptionsCalcul.EtaW, Enu_TypeVariable.SansType, 4, 2, False)

    End Sub

    Private Sub RemplirCombos()
        Me.cmb_Norme.Items.Clear()
        'Me.cmb_Norme.Items.AddRange(tabNorme)
        Me.cmb_Norme.Items.Add(tabNorme(0))
        If LogicielReglages.lG2 Or LogicielOptions.lExpert Then
            Me.cmb_Norme.Items.Add(tabNorme(1))
        End If

        Me.cmb_Norme.SelectedIndex = 0
    End Sub

#End Region

#Region " Evènements "

    Private Sub cmb_Norme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Norme.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_Norme.SelectedIndex
            Case 0 : LocalOptionsCalcul.Norme = cls_OptionsCalcul.Enu_Normes.EurocodesG1
            Case 1 : LocalOptionsCalcul.Norme = cls_OptionsCalcul.Enu_Normes.EurocodesG2
        End Select

    End Sub

    Private Sub ChangeCheckBoxes(sender As Object, e As EventArgs) Handles chk_SimplifiedEffectiveW.CheckedChanged, chk_RebarsInCompression.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_RebarsInCompression.Name
                LocalOptionsCalcul.lCompressionArma = Me.chk_RebarsInCompression.Checked
            Case Me.chk_SimplifiedEffectiveW.Name
                LocalOptionsCalcul.lLargeurEfficaceSimplifiee = Me.chk_SimplifiedEffectiveW.Checked
        End Select
    End Sub


#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_NbNodes2.Paint, img_NbNodes1.Paint, img_Es.Paint, img_dNodes.Paint, img_PsiLSH.Paint, img_PsiLG.Paint, img_T0SH.Paint, img_T0G2.Paint, img_T0G1.Paint, img_Eta.Paint, img_DeltaCDev.Paint

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
        lEgal = False
        AlignH = Enu_AlignementH.Droite

        Select Case sender.name
            Case Me.img_Es.Name
                strSymbol = "E"
                strIndice = "s"
                lEgal = True
                'AlignH = Enu_AlignementH.Droite
            Case Me.img_DeltaCDev.Name
                strSymbol = "Δc"
                strIndice = "dev"
                lEgal = True
            Case Me.img_dNodes.Name
                strSymbol = "d"
                strIndice = ""
            Case Me.img_NbNodes1.Name, Me.img_NbNodes2.Name
                strSymbol = "n"
                strIndice = ""
            Case Me.img_PsiLG.Name
                strSymbol = "y"
                strIndice = "L"
                lGrec = True
                lEgal = True
            Case Me.img_PsiLSH.Name
                strSymbol = "y"
                strIndice = "L"
                lGrec = True
                lEgal = True
            Case Me.img_T0G1.Name, Me.img_T0G2.Name, Me.img_T0SH.Name
                strSymbol = "t"
                strIndice = "0"
                lEgal = True
            Case Me.img_Eta.Name
                strSymbol = "h"
                strIndice = ""
                lGrec = True
                lEgal = True
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements saisie "


    Private Sub SaisieTxtBox_TextChanged(sender As Object, e As EventArgs) Handles txt_EspNoeuds.TextChanged, txt_Es.TextChanged,
        txt_NbMiniNTravee.TextChanged, txt_NbMiniNConsole.TextChanged, txt_PsiLSH.TextChanged, txt_PsiLG.TextChanged, txt_t0SHDalle.TextChanged, txt_t0SHEnrob.TextChanged, txt_t0G2Dalle.TextChanged, txt_t0G2Enrob.TextChanged, txt_t0G1Dalle.TextChanged, txt_t0G1Enrob.TextChanged, txt_eta.TextChanged, txt_DeltaCDev.TextChanged

        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case Me.txt_Es.Name
                    LocalOptionsCalcul.EsArmatures = ValeurUI
                Case Me.txt_DeltaCDev.Name
                    LocalOptionsCalcul.DeltaCDev = ValeurUI
                Case Me.txt_EspNoeuds.Name
                    LocalOptionsCalcul.dMaxNodes = ValeurUI
                Case Me.txt_NbMiniNConsole.Name
                    LocalOptionsCalcul.nbMinNodesConsole = ValeurUI
                Case Me.txt_NbMiniNTravee.Name
                    LocalOptionsCalcul.nbMinNodesTravee = ValeurUI
                Case Me.txt_PsiLG.Name
                    LocalOptionsCalcul.PsiLPermanent = ValeurUI
                Case Me.txt_PsiLSH.Name
                    LocalOptionsCalcul.PsiLRetrait = ValeurUI

                Case Me.txt_t0G1Dalle.Name
                    LocalOptionsCalcul.TimeT0G1(0) = ValeurUI
                Case Me.txt_t0G1Enrob.Name
                    LocalOptionsCalcul.TimeT0G1(1) = ValeurUI
                Case Me.txt_t0G2Dalle.Name
                    LocalOptionsCalcul.TimeT0G2(0) = ValeurUI
                Case Me.txt_t0G2Enrob.Name
                    LocalOptionsCalcul.TimeT0G2(1) = ValeurUI
                Case Me.txt_t0SHDalle.Name
                    LocalOptionsCalcul.TimeT0SH(0) = ValeurUI
                Case Me.txt_t0SHEnrob.Name
                    LocalOptionsCalcul.TimeT0SH(1) = ValeurUI
                Case Me.txt_eta.Name
                    LocalOptionsCalcul.EtaW = ValeurUI

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
        Dim kUnit As Decimal

        'Const PORTEECONSOLEMINMIN As Decimal = 0.2
        'Const PORTEECONSOLEMINMAX As Decimal = 0.5

        Select Case MyTxt.Name
            Case Me.txt_Es.Name

                lValMax = False
                kUnit = LogicielInfo.Transfert_ModulesY(LogicielOptions.IndUnitModulesY)
                ValMin = 100000
                ValMax = 500000

            Case Me.txt_DeltaCDev.Name

                lValMax = False
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
                ValMin = 0
                'ValMax = 500000

            Case Me.txt_EspNoeuds.Name
                ValMin = 0.1
                ValMax = 2
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
            Case Me.txt_NbMiniNTravee.Name
                ValMin = 1
                ValMax = 1000
                kUnit = 1
            Case Me.txt_NbMiniNConsole.Name
                ValMin = 1
                ValMax = 1000
                kUnit = 1

            Case Me.txt_PsiLG.Name, Me.txt_PsiLSH.Name
                kUnit = 1
                ValMin = 0
                ValMax = 2

            Case Me.txt_t0G1Enrob.Name, Me.txt_t0G1Dalle.Name, Me.txt_t0G2Enrob.Name, Me.txt_t0G2Dalle.Name, Me.txt_t0SHEnrob.Name, Me.txt_t0SHDalle.Name
                kUnit = 1
                ValMin = 1
                ValMax = 100

            Case Me.txt_eta.Name
                ValMin = 1
                ValMax = 1.2
                kUnit = 1
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