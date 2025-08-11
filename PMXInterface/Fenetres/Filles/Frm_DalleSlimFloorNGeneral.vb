'Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Reflection
Imports PMXMoteur2

Public Class Frm_DalleSlimFloorNGeneral

#Region " Variables "

    Dim lBuild As Boolean

    Dim strType(3) As String
    Dim strCofradal() As String                                             'contient les noms des cofradals à afficher dans le combobox

    Dim ClasseBeton() As String = cls_Beton.TabClasseBeton
    Dim ClasseBetonLeger() As String = cls_Beton.TabClasseBetonLeger

    Private Const TDMAXI As Decimal = 0.5
    Private Const HPMINI As Decimal = 0.04
    Private Const DPMIN As Decimal = 0.05
    Private Const DPMAX As Decimal = 0.5
    Private Const MUPFMIN As Decimal = 100
    Private Const MUPFMAX As Decimal = 5000

    Dim localSection As New cls_Section

    Dim iSelect As Integer = -1

    Dim lInter As Boolean
    Private Enum Enu_DefEpMixte
        Totale                  ' Définition d'une dalle mixte par son épaisseur totale
        Pleine                  ' Définition d'une dalle mixte par son épaisseur au dessus du bac
    End Enum
    Dim DefEpMixte As Enu_DefEpMixte = Enu_DefEpMixte.Totale

    Dim strMasseAvecBac As String
    Dim strMasseSansBac As String
    Dim strMassePrefa As String
    Dim strMassePreDal As String

    Const SELECT_EPDALLED As Integer = 1
    Const SELECT_EPDALLEC As Integer = 2
    Const SELECT_EPPREDAL As Integer = 3
    Const SELECT_EPPREJNT As Integer = 4
    Const SELECT_EPPREFAB As Integer = 5
    Const SELECT_NOMPREFAB As Integer = 6

    Const SELECT_BETON As Integer = 1000

    Dim nbCofraDal As Integer = cls_Cofradal.TAB_CofraDal.Length

    Dim strCofradalNotFound As String

    Dim AccelG As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Param.GraviteG

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String), plInter As Boolean)

        lBuild = True
        lInter = plInter

        GestionStyle()
        GestionLangue(myBloc)
        GestionUnites()
        InitialiseVariablesLocales()

        RemplirComboClasseBeton()
        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboCofra()

        PrepareFenetre()

        AfficheDalleEnCours()

        lBuild = False

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        '  Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim12.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim13.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "2"

        Me.etq_UnitRhoC.Text = "kg/m3"
        Me.etq_UnitMassSurf.Text = "kg/m2"
        ' Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux

        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux

        Me.lbl_Masses.BackColor = CouleurBackBandeaux
        Me.lbl_Masses.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub GestionLangue(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try
            '=== GENERAL ======================================================================

            CLE = "GENERAL" : Me.lbl_General.Text = myBloc(CLE)
            CLE = "EDGECHAMBERFILLED" : Me.chk_ChambreRivePleine.Text = myBloc(CLE)
            CLE = "SOLIDSLAB" : strType(0) = myBloc(CLE)
            CLE = "COMPOSITESLAB" : strType(1) = myBloc(CLE)
            CLE = "PRECASTSLAB" : strType(2) = myBloc(CLE)
            CLE = "FULLPRECAST" : strType(3) = myBloc(CLE)

            CLE = "TYPE" : Me.lbl_TypeDalle.Text = myBloc(CLE)
            CLE = "THICKNESS" : Me.lbl_Epaisseur.Text = myBloc(CLE)
            CLE = "THICKNESS" : Me.lbl_EpaisseurM.Text = myBloc(CLE)
            CLE = "PRESLAB" : Me.lbl_EpPreDalle.Text = myBloc(CLE)
            CLE = "JOINT" : Me.lbl_EpJoint.Text = myBloc(CLE)

            CLE = "COFRADAL" : Me.lbl_Cofradal.Text = myBloc(CLE)
            CLE = "NAME" : Me.lbl_Name.Text = myBloc(CLE)
            CLE = "PRESLAB" : Me.lbl_dp.Text = myBloc(CLE)
            CLE = "MSURF" : Me.lbl_mupf.Text = myBloc(CLE)

            ReDim strCofradal(nbCofraDal)
            CLE = "USER" : strCofradal(0) = myBloc(CLE)
            CLE = "COFRANOTFOUND" : strCofradalNotFound = myBloc(CLE)

            '=== BETON ========================================================================

            CLE = "CONCRETE" : Me.lbl_Beton.Text = myBloc(CLE)

            CLE = "CLASS" : Me.lbl_ClasseE.Text = myBloc(CLE)
            CLE = "LIGHTCONCRETE" : Me.chk_BetonLeger.Text = myBloc(CLE)

            '=== MASSES ========================================================================

            CLE = "MASSES" : Me.lbl_Masses.Text = myBloc(CLE)
            CLE = "INFOMASSESSHEET" : strMasseAvecBac = myBloc(CLE)
            CLE = "INFOMASSESNOSHEET" : strMasseSansBac = myBloc(CLE)
            CLE = "INFOMASSESPRESLAB" : strMassePreDal = myBloc(CLE)
            CLE = "INFOMASSESPREFAB" : strMassePrefa = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub PrepareFenetre()

        Dim yRef As Integer

        Me.pan_OptionRive.Visible = Not lInter

        yRef = 35
        If Not lInter Then
            yRef += Me.pan_OptionRive.Height + 5
        End If

        Me.pan_Type.Controls.Add(Me.pan_Epaisseur)
        Me.pan_Epaisseur.Left = 5
        Me.pan_Epaisseur.Top = yRef

        Me.pan_Type.Controls.Add(Me.pan_EpaisseurMixte)
        Me.pan_EpaisseurMixte.Left = 5
        Me.pan_EpaisseurMixte.Top = Me.pan_Epaisseur.Top

        Me.pan_Type.Controls.Add(Me.pan_Predalle)
        Me.pan_Predalle.Left = 5
        Me.pan_Predalle.Top = yRef + 20        ' 55

        Me.pan_Type.Controls.Add(Me.pan_Cofradal)
        Me.pan_Cofradal.Left = 5
        Me.pan_Cofradal.Top = yRef + 25        '  60

        MAJI_SaisieEpMixte()
        MAJI_MasseDalle()

        Select Case DefEpMixte
            Case Enu_DefEpMixte.Pleine : Me.rdb_EpPleine.Checked = True
            Case Enu_DefEpMixte.Totale : Me.rdb_EpTotale.Checked = True
        End Select

        MAJI_TypeDalle()

    End Sub

    Private Sub RemplirComboCofra()

        Dim str_TableCofra() As String = cls_Cofradal.Get_ListName_Cofradal()

        Me.cmb_Cofradal.Items.Clear()

        Me.cmb_Cofradal.Items.Add(strCofradal(0))

        For i As Integer = 1 To nbCofraDal
            Me.cmb_Cofradal.Items.Add(str_TableCofra(i - 1))
        Next

    End Sub

    Private Sub AfficheDalleEnCours()

        '--> Rive

        If Not lInter Then
            Me.chk_ChambreRivePleine.Checked = Frm_DalleSlimFloorN.localDalle.lRiveRemplie
        End If

        '--> Type de dalle

        Select Case Frm_DalleSlimFloorN.localDalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                Me.cmb_TypeDalle.SelectedIndex = 0
            Case cls_Dalle.Enum_TypeDalle.Mixte
                Me.cmb_TypeDalle.SelectedIndex = 1
            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                Me.cmb_TypeDalle.SelectedIndex = 2
            Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                Me.cmb_TypeDalle.SelectedIndex = 3
        End Select

        '--> Epaisseur

        Me.txt_Td2.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.Ep_td, Enu_TypeVariable.Dimension)
        Me.txt_Tc.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.Ep_td - Frm_DalleSlimFloorN.localDalle.Bac.Hp, Enu_TypeVariable.Dimension)

        Me.txt_Hd.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.Ep_td, Enu_TypeVariable.Dimension)

        Me.txt_EpPredalle.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.preDalle_ep, Enu_TypeVariable.Dimension)
        Me.txt_EpJoint.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.preDalle_tjoint, Enu_TypeVariable.Dimension)

        '--> Béton

        Dim Chaine As String
        Chaine = Frm_DalleSlimFloorN.localDalle.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) And Not Frm_DalleSlimFloorN.localDalle.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        ElseIf Me.ClasseBetonLeger.Contains(Chaine) And Frm_DalleSlimFloorN.localDalle.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBetonLeger, Chaine)
        Else
            Me.cmb_ClasseBetonDalle.SelectedIndex = 0
        End If

        Me.txt_RhoC.Text = GetStringInUnit(Frm_DalleSlimFloorN.localDalle.beton.RhoC, Enu_TypeVariable.SansType, 3, 0, False)
        Me.chk_BetonLeger.Checked = Frm_DalleSlimFloorN.localDalle.beton.lLeger

        Frm_DalleSlimFloorN.MAJI_ProprietesBeton()

        '--( Cofradalle

        If Frm_DalleSlimFloorN.localDalle.Cofradal.lCustom Then
            Me.cmb_Cofradal.SelectedIndex = 0
        Else
            Me.cmb_Cofradal.SelectedItem = Frm_DalleSlimFloorN.localDalle.Cofradal.Nom
        End If

        Me.txt_dp.Enabled = Frm_DalleSlimFloorN.localDalle.Cofradal.lCustom
        Me.txt_mupf.Enabled = Frm_DalleSlimFloorN.localDalle.Cofradal.lCustom

        MAJI_Cofradal()
        MAJI_CofradalEnCours()

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub RemplirComboClasseBeton()
        If Frm_DalleSlimFloorN.localDalle.beton.lLeger Then
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBetonLeger)
        Else
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBeton)
        End If
    End Sub

    Private Sub InitialiseVariablesLocales()

        localSection = MyProjet.Poutres(MyProjet.IndEnCours).Section

        'Dans la première génération des EN, on supprime la dernière classes des tableaux

        If OptionsCalcul.Norme = Enu_Normes.Eurocodes_G1 Then

            Dim nbClasse As Integer = ClasseBeton.GetUpperBound(0)
            ReDim Preserve ClasseBeton(nbClasse)

            nbClasse = ClasseBetonLeger.GetUpperBound(0)
            ReDim Preserve ClasseBetonLeger(nbClasse)

        End If
    End Sub

#End Region

#Region " Evenements sur les textbox "

    Private Sub txt_NameCustomCofra_TextChanged(sender As Object, e As EventArgs) Handles txt_NameCustomCofra.TextChanged
        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.Cofradal.Nom = Me.txt_NameCustomCofra.Text

        Frm_DalleSlimFloorN.RedessineDalle()
    End Sub


    Private Sub SaisieTextChanged(sender As Object, e As EventArgs) _
        Handles txt_RhoC.TextChanged, txt_Td2.TextChanged, txt_Tc.TextChanged, txt_Hd.TextChanged, txt_EpPredalle.TextChanged,
        txt_EpJoint.TextChanged, txt_mupf.TextChanged, txt_dp.TextChanged ', txt_dp.TextChanged, txt_mupf.TextChanged

        If lBuild Then Exit Sub
        'lBuild = True
        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_EpJoint.Name
                    Frm_DalleSlimFloorN.localDalle.preDalle_tjoint = Valeur
                Case Me.txt_EpPredalle.Name
                    Frm_DalleSlimFloorN.localDalle.preDalle_ep = Valeur

                Case Me.txt_Hd.Name
                    Frm_DalleSlimFloorN.localDalle.Ep_td = Valeur


                Case Me.txt_RhoC.Name
                    Frm_DalleSlimFloorN.localDalle.beton.RhoC = Valeur

                Case Me.txt_Td2.Name
                    Frm_DalleSlimFloorN.localDalle.Ep_td = Valeur
                    'lBuild = True
                    Me.txt_Tc.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.Ep_td - Frm_DalleSlimFloorN.localDalle.Bac.Hp, Enu_TypeVariable.Dimension)
                    'lBuild = False

                Case Me.txt_Tc.Name
                    Frm_DalleSlimFloorN.localDalle.Ep_td = Valeur + Frm_DalleSlimFloorN.localDalle.Bac.Hp
                    'lBuild = True
                    Me.txt_Td2.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.Ep_td, Enu_TypeVariable.Dimension)
                    'lBuild = False

                Case Me.txt_dp.Name
                    If cmb_Cofradal.SelectedIndex = 0 Then Frm_DalleSlimFloorN.localDalle.Cofradal.dp = Valeur

                Case Me.txt_mupf.Name
                    If cmb_Cofradal.SelectedIndex = 0 Then Frm_DalleSlimFloorN.localDalle.Cofradal.mSurf = Valeur

            End Select

            MAJI_MasseDalle()
            Frm_DalleSlimFloorN.RedessineDalle()

        End If

    End Sub


    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim lValMin As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name
            Case Me.txt_Hd.Name, Me.txt_Td2.Name

                Select Case Frm_DalleSlimFloorN.localDalle.type
                    Case cls_Dalle.Enum_TypeDalle.Pleine, cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                        ValMin = OptionsScope.EpDallePleineMin / kUnit
                    Case cls_Dalle.Enum_TypeDalle.Mixte
                        ValMin = (OptionsScope.EpDalleMixteMin + HPMINI) / kUnit
                    Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                        ValMin = OptionsScope.EpDallePleineMin / kUnit
                End Select

                ValMin = Math.Max(ValMin, (localSection.hec + OptionsCalcul.DeltaCDev) / kUnit)

                ValMax = TDMAXI / kUnit

            Case txt_EpPredalle.Name
                ValMin = 0 / kUnit
                ValMax = Math.Min(localSection.hec, OptionsScope.RatioEpPredalleMax * Frm_DalleSlimFloorN.localDalle.Ep_td) / kUnit

            Case Me.txt_EpJoint.Name
                ValMin = 0
                ValMax = Frm_DalleSlimFloorN.localDalle.preDalle_ep / kUnit
            Case Me.txt_RhoC.Name

                ValMin = 1500
                ValMax = 0
                lValMax = False
                kUnit = 1

            Case Me.txt_Tc.Name 'dans le cas d'une dalle mixte
                ValMin = Math.Max(OptionsScope.EpDalleMixteMin, localSection.hec + OptionsCalcul.DeltaCDev - Frm_DalleSlimFloorN.localDalle.Bac.Hp) / kUnit
                ValMax = 0
                lValMax = False

            Case Me.txt_dp.Name
                ValMin = DPMIN / kUnit '50 mm
                ValMax = Math.Min(Frm_DalleSlimFloorN.LocalSectionHec, DPMAX) / kUnit '500 mm

            Case Me.txt_mupf.Name 'kN/m2
                kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2
                ValMin = MUPFMIN / kUnit '0.1 kN/m2
                ValMax = MUPFMAX / kUnit '5 kN/m2

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            'ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function


#End Region

#Region " Autres evenements de saisie "

    Private Sub cmb_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Cofradal.SelectedIndexChanged
        If lBuild Then Exit Sub

        Me.txt_dp.Enabled = cmb_Cofradal.SelectedIndex = 0
        Me.txt_mupf.Enabled = cmb_Cofradal.SelectedIndex = 0



        If cmb_Cofradal.SelectedIndex = 0 Then

            Frm_DalleSlimFloorN.localDalle.Cofradal.lCustom = True

        Else


            Dim lOK As Boolean = True

            Frm_DalleSlimFloorN.localDalle.Cofradal.SetCofradalBDD(Me.cmb_Cofradal.Text, lOK)

            If Not lOK Then
                GestionErrorsPMX(Me.Name, "", strCofradalNotFound, True)
            End If

        End If

        MAJI_Cofradal()
        MAJI_CofradalEnCours()

        MAJI_MasseDalle()
        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub MAJI_CofradalEnCours()

        Me.txt_NameCustomCofra.Text = Frm_DalleSlimFloorN.localDalle.Cofradal.Nom
        Me.txt_dp.Text = GetStringInUnitN(Frm_DalleSlimFloorN.localDalle.Cofradal.dp, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_mupf.Text = GetStringInUnitN(Frm_DalleSlimFloorN.localDalle.Cofradal.mSurf, Enu_TypeVariable.ChargeSurfacique, 4, 3, NON_U, True)

    End Sub


    Private Sub MAJI_Cofradal()

        If cmb_Cofradal.SelectedIndex = 0 Then 'Cofradal Utilisateur

            Me.txt_NameCustomCofra.Visible = True
            Me.lbl_Name.Visible = True

        Else
            Me.txt_NameCustomCofra.Visible = False
            Me.lbl_Name.Visible = False

        End If

    End Sub

    Private Sub chk_ChambreRivePleine_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ChambreRivePleine.CheckedChanged
        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.lRiveRemplie = Me.chk_ChambreRivePleine.Checked

        Frm_DalleSlimFloorN.RedessineDalle()
    End Sub

    Private Sub cmb_ClasseBetonEnrobage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        If Frm_DalleSlimFloorN.localDalle.beton.lLeger Then
            Frm_DalleSlimFloorN.localDalle.beton.Classe = Me.ClasseBetonLeger(Me.cmb_ClasseBetonDalle.SelectedIndex)
        Else
            Frm_DalleSlimFloorN.localDalle.beton.Classe = Me.ClasseBeton(Me.cmb_ClasseBetonDalle.SelectedIndex)
        End If

        Frm_DalleSlimFloorN.MAJI_ProprietesBeton()
        Frm_DalleSlimFloorN.RedessineDalle()
    End Sub

    Private Sub chk_BetonLeger_CheckedChanged(sender As Object, e As EventArgs) Handles chk_BetonLeger.CheckedChanged

        If lBuild Then Exit Sub

        Dim Index As Integer = Me.cmb_ClasseBetonDalle.SelectedIndex

        Frm_DalleSlimFloorN.localDalle.beton.lLeger = Me.chk_BetonLeger.Checked

        RemplirComboClasseBeton()

        Me.cmb_ClasseBetonDalle.SelectedIndex = Index
        Frm_DalleSlimFloorN.localDalle.beton.Classe = Me.cmb_ClasseBetonDalle.Text

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub SaisieRdBDefEpMixte(sender As Object, e As EventArgs) Handles rdb_EpTotale.CheckedChanged, rdb_EpPleine.CheckedChanged
        If lBuild Then Exit Sub
        If Me.rdb_EpPleine.Checked Then
            DefEpMixte = Enu_DefEpMixte.Pleine
        Else
            DefEpMixte = Enu_DefEpMixte.Totale
        End If
        MAJI_SaisieEpMixte()

    End Sub

    Private Sub MAJI_SaisieEpMixte()

        PrepareTextBoxDipo(Me.txt_Td2, DefEpMixte = Enu_DefEpMixte.Totale)
        PrepareTextBoxDipo(Me.txt_Tc, DefEpMixte = Enu_DefEpMixte.Pleine)

    End Sub

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : Frm_DalleSlimFloorN.localDalle.type = cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : Frm_DalleSlimFloorN.localDalle.type = cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : Frm_DalleSlimFloorN.localDalle.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
            Case 3 : Frm_DalleSlimFloorN.localDalle.type = cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
        End Select

        MAJI_TypeDalle()
        MAJI_MasseDalle()
        'AfficherDalleEnCours()
        Frm_DalleSlimFloorN.MAJI_RdbTypeDalle()

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

#End Region

#Region " Gestion Enter Leave sur les objets "

    Private Sub ComboBoxBeton_Enter(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.cmb_ClasseBetonDalle.Name
                iSelect = SELECT_BETON
        End Select
        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)
    End Sub
    Private Sub ComboBoxBeton_Leave(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)
    End Sub

    Private Sub chk_BetonLeger_Enter(sender As Object, e As EventArgs) Handles chk_BetonLeger.Enter
        If lBuild Then Exit Sub

        iSelect = SELECT_BETON

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)
    End Sub

    Private Sub chk_BetonLeger_Leave(sender As Object, e As EventArgs) Handles chk_BetonLeger.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)
    End Sub

    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) _
                Handles txt_RhoC.Leave, txt_Td2.Leave, txt_Tc.Leave, txt_Hd.Leave, txt_EpPredalle.Leave,
                txt_EpJoint.Leave, txt_dp.Leave, txt_mupf.Leave, txt_NameCustomCofra.Leave

        If lBuild Then Exit Sub
        iSelect = -1

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)

    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) _
        Handles txt_RhoC.Enter, txt_Td2.Enter, txt_Tc.Enter, txt_Hd.Enter, txt_EpPredalle.Enter,
        txt_EpJoint.Enter, txt_dp.Enter, txt_mupf.Enter, txt_NameCustomCofra.Enter


        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.txt_Hd.Name, Me.txt_Td2.Name
                iSelect = SELECT_EPDALLED
            Case Me.txt_Tc.Name
                iSelect = SELECT_EPDALLEC
            Case Me.txt_EpPredalle.Name
                iSelect = SELECT_EPPREDAL
            Case Me.txt_EpJoint.Name
                iSelect = SELECT_EPPREJNT
            Case Me.txt_dp.Name
                iSelect = SELECT_EPPREFAB

            Case Me.txt_NameCustomCofra.Name
                iSelect = SELECT_NOMPREFAB


            Case Me.txt_RhoC.Name
                iSelect = SELECT_BETON

        End Select

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)

    End Sub

#End Region

#Region " Gestion zone de saisie générale "

    Private Sub MAJI_TypeDalle()
        '------------------------------------------------------------------------------------
        '   27/06/23 :  Création - POM
        '------------------------------------------------------------------------------------
        '   MAJ de l'interface en fonction du type de dalle
        '------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------

        Dim ChaineM As String = ""

        Select Case Frm_DalleSlimFloorN.localDalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                ' Me.pan_Bac.Enabled = False

                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True
                Me.pan_Cofradal.Visible = False
                ChaineM = strMasseSansBac

            Case cls_Dalle.Enum_TypeDalle.Mixte

                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = True
                Me.pan_Epaisseur.Visible = False
                Me.pan_Cofradal.Visible = False
                ChaineM = strMasseAvecBac

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee

                Me.pan_Predalle.Visible = True
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True
                Me.pan_Cofradal.Visible = False
                ChaineM = strMassePreDal

            Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique

                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True
                Me.pan_Cofradal.Visible = True
                ChaineM = strMassePrefa

        End Select

        Me.lbl_InfoMassUs.Text = ChaineM

    End Sub

#End Region

#Region " Mise à jour de la masse de la dalle "

    Private Sub MAJI_MasseDalle()

        Dim mSurf = Frm_DalleSlimFloorN.localDalle.MasseSurfacique(True, True, accelg)

        Me.txt_MassSurf.Text = GetStringInUnitN(mSurf, Enu_TypeVariable.SansType, 4, 3, NON_U, True)

    End Sub

#End Region

#Region " Dessins symboles "
    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles _
        img_EpPredalle.Paint, img_EpJoint.Paint, img_Td2.Paint, img_Tc.Paint, Img_Hd.Paint, img_dp.Paint, img_mupf.Paint

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
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Droite

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_RhoC.Name
                strSymbol = "r"
                strIndice = "c"
                lGrec = True
            Case Me.img_Tc.Name
                strSymbol = "t"
                strIndice = "c"
            Case Me.Img_Hd.Name, Me.img_Td2.Name
                strSymbol = "t"
                strIndice = "d"

            Case Me.img_EpPredalle.Name
                strSymbol = "t"
                strIndice = "pre"
            Case Me.img_EpJoint.Name
                strSymbol = "t"
                strIndice = "j"
            Case Me.img_dp.Name
                strSymbol = "d"
                strIndice = "p"
            Case img_mupf.Name
                strSymbol = "m"
                strIndice = "pf"
                lGrec = True

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region



End Class