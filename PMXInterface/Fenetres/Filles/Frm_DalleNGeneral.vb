Imports System.Reflection
Imports PMXMoteur2

Public Class Frm_DalleNGeneral


#Region " Variables "

    Dim lBuild As Boolean

    Dim strType(2) As String

    Dim ClasseBeton() As String = cls_Beton.TabClasseBeton
    Dim ClasseBetonLeger() As String = cls_Beton.TabClasseBetonLeger

    Private Enum Enu_DefEpMixte
        Totale                  ' Définition d'une dalle mixte par son épaisseur totale
        Pleine                  ' Définition d'une dalle mixte par son épaisseur au dessus du bac
    End Enum
    Dim DefEpMixte As Enu_DefEpMixte = Enu_DefEpMixte.Totale

    Private Const TDMAXI As Decimal = 0.5
    Private Const HPMINI As Decimal = 0.04

    Dim strErreurLeger, strErreurNormal As String

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangue(myBloc)
        GestionUnites()
        GestionStyle()
        PreparerFenetre()
        AfficherDalleEnCours()

        lBuild = False
    End Sub

    Private Sub GestionLangue(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try
            '=== GENERAL ======================================================================

            CLE = "GENERAL" : Me.lbl_General.Text = myBloc(CLE)
            CLE = "SOLIDSLAB" : strType(0) = myBloc(CLE)
            CLE = "COMPOSITESLAB" : strType(1) = myBloc(CLE)
            CLE = "PRECASTSLAB" : strType(2) = myBloc(CLE)

            CLE = "TYPE" : Me.lbl_TypeDalle.Text = myBloc(CLE)
            CLE = "THICKNESS" : Me.lbl_Epaisseur.Text = myBloc(CLE)
            CLE = "THICKNESS" : Me.lbl_EpaisseurM.Text = myBloc(CLE)
            CLE = "HAUNCH" : Me.lbl_Renformis.Text = myBloc(CLE)
            CLE = "PRESLAB" : Me.lbl_EpPreDalle.Text = myBloc(CLE)
            CLE = "JOINT" : Me.lbl_EpJoint.Text = myBloc(CLE)

            CLE = "PRIOFULLDEPTH" : Me.ToolTipDalle.SetToolTip(Me.rdb_EpTotale, myBloc(CLE))
            CLE = "PRIODEPTHABOVE" : Me.ToolTipDalle.SetToolTip(Me.rdb_EpPleine, myBloc(CLE))

            '=== BETON ========================================================================

            CLE = "CONCRETE" : Me.lbl_Beton.Text = myBloc(CLE)

            CLE = "CLASS" : Me.lbl_ClasseE.Text = myBloc(CLE)
            CLE = "RHOB" : Me.lbl_RhoB.Text = myBloc(CLE)
            CLE = "LIGHTCONCRETE" : Me.chk_BetonLeger.Text = myBloc(CLE)

            '=== MESSAGES ERREURS =============================================================

            CLE = "ERRORLWC" : strErreurLeger = myBloc(CLE)
            CLE = "ERRORNWC" : strErreurNormal = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub PreparerFenetre()

        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboClasseBeton()
        RemplirListeClasseBeton()

        Const MARGEPAN As Integer = 0

        Me.pan_Type.Controls.Add(Me.pan_Epaisseur)
        Me.pan_Epaisseur.Left = 5
        Me.pan_Epaisseur.Top = 35

        Me.pan_Type.Controls.Add(Me.pan_EpaisseurMixte)
        Me.pan_EpaisseurMixte.Left = 5
        Me.pan_EpaisseurMixte.Top = Me.pan_Epaisseur.Top

        Me.pan_Type.Controls.Add(Me.pan_Renformis)
        Me.pan_Renformis.Left = 5
        Me.pan_Renformis.Top = Me.pan_Epaisseur.Top + Me.pan_Epaisseur.Height + MARGEPAN

        Me.pan_Type.Controls.Add(Me.pan_Predalle)
        Me.pan_Predalle.Left = 5
        Me.pan_Predalle.Top = Me.pan_Renformis.Top

        Select Case DefEpMixte
            Case Enu_DefEpMixte.Pleine
                Me.rdb_EpPleine.Checked = True
            Case Enu_DefEpMixte.Totale
                Me.rdb_EpTotale.Checked = True
        End Select

        MAJI_SaisieEpMixte()

    End Sub

    Private Sub RemplirComboClasseBeton()
        If Frm_DalleN.MyDalleLoc.beton.lLeger Then
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBetonLeger)
        Else
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBeton)
        End If
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub RemplirListeClasseBeton()
        If Frm_DalleN.myDalleLoc.beton.lLeger Then
            RemplirListeAvecTableau(Me.lst_ClasseBetonDalle, ClasseBetonLeger)
        Else
            RemplirListeAvecTableau(Me.lst_ClasseBetonDalle, ClasseBeton)
        End If
    End Sub

    Private Sub RemplirListeAvecTableau(myList As ListBox, tabValeurs() As String)

        myList.Items.Clear()
        myList.Items.AddRange(tabValeurs)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.Pan_Contenu.Dock = DockStyle.Fill

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux


        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux

        Me.pan_Predalle.Top = Me.pan_Renformis.Top

        PrepareTextBoxDipo(txt_Fck, False)
        PrepareTextBoxDipo(txt_Ecm, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitDim8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma1.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

        Me.etq_UnitRhoC.Text = "kg/m3"

    End Sub

    Private Sub AfficherDalleEnCours()

        '--> Type de dalle

        Select Case Frm_DalleN.MyDalleLoc.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                Me.cmb_TypeDalle.SelectedIndex = 0
            Case cls_Dalle.Enum_TypeDalle.Mixte
                Me.cmb_TypeDalle.SelectedIndex = 1
            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                Me.cmb_TypeDalle.SelectedIndex = 2
        End Select
        MAJI_TypeDalle()
        Frm_DalleN.MAJI_TauxArma()

        '--> Epaisseur

        Me.txt_Td2.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension, True)
        Me.txt_Tc.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td - Frm_DalleN.MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension, True)

        Me.txt_Hd.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension, True)
        Me.txt_Hh.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_th, Enu_TypeVariable.Dimension, True)

        Me.txt_EpPredalle.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.preDalle_ep, Enu_TypeVariable.Dimension, True)
        Me.txt_EpJoint.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.preDalle_tjoint, Enu_TypeVariable.Dimension, True)

        '--> Béton

        Dim Chaine As String
        Chaine = Frm_DalleN.MyDalleLoc.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) And Not Frm_DalleN.MyDalleLoc.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        ElseIf Me.ClasseBetonLeger.Contains(Chaine) And Frm_DalleN.MyDalleLoc.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBetonLeger, Chaine)
        Else
            Me.cmb_ClasseBetonDalle.SelectedIndex = 0
        End If

        If Me.ClasseBeton.Contains(Chaine) And Not Frm_DalleN.myDalleLoc.beton.lLeger Then
            Me.lst_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        ElseIf Me.ClasseBetonLeger.Contains(Chaine) And Frm_DalleN.myDalleLoc.beton.lLeger Then
            Me.lst_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBetonLeger, Chaine)
        Else
            Me.lst_ClasseBetonDalle.SelectedIndex = 0
        End If

        Me.txt_RhoC.Text = GetStringInUnit(Frm_DalleN.MyDalleLoc.beton.RhoC, Enu_TypeVariable.SansType, 3, 0, False, True)
        Me.chk_BetonLeger.Checked = Frm_DalleN.MyDalleLoc.beton.lLeger

        MAJI_ProprietesBeton()

    End Sub

#End Region

#Region " Evènements de saisie "

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : Frm_DalleN.MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : Frm_DalleN.MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : Frm_DalleN.MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
        End Select

        MAJI_TypeDalle()
        Frm_DalleN.MAJI_TypeDalle()

        ' MAJ_ValeursLimites()

        ' AfficherLitEncours() 'Permet de relancer la vérification 

        Frm_DalleN.MAJI_ImageDalle()
    End Sub

    Private Sub SaisieTextChanged(sender As Object, e As EventArgs) Handles txt_Hh.TextChanged, txt_RhoC.TextChanged, txt_Td2.TextChanged, txt_Tc.TextChanged, txt_Hd.TextChanged, txt_EpPredalle.TextChanged, txt_EpJoint.TextChanged

        If lBuild Then Exit Sub
        lBuild = True
        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_EpJoint.Name
                    Frm_DalleN.MyDalleLoc.preDalle_tjoint = Valeur

                Case Me.txt_EpPredalle.Name
                    Frm_DalleN.myDalleLoc.preDalle_ep = Valeur

                Case Me.txt_Hd.Name
                    Frm_DalleN.myDalleLoc.Ep_td = Valeur
                    lBuild = True
                    'Me.txt_Td2.Text = Me.txt_Hd.Text

                Case Me.txt_Hh.Name
                    Frm_DalleN.myDalleLoc.Ep_th = Valeur

                Case Me.txt_RhoC.Name
                    Frm_DalleN.myDalleLoc.beton.RhoC = Valeur

                Case Me.txt_Td2.Name
                    Frm_DalleN.myDalleLoc.Ep_td = Valeur
                    lBuild = True
                    Me.txt_Tc.Text = GetStringNoUnit(Frm_DalleN.myDalleLoc.Ep_td - Frm_DalleN.myDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension)
                    lBuild = False

                Case Me.txt_Tc.Name
                    Frm_DalleN.myDalleLoc.Ep_td = Valeur + Frm_DalleN.myDalleLoc.Bac.Hp
                    lBuild = True
                    Me.txt_Td2.Text = GetStringNoUnit(Frm_DalleN.myDalleLoc.Ep_td, Enu_TypeVariable.Dimension)
                    lBuild = False

            End Select

            '            AfficherLitEncours() 'Permet de relancer les vérifications

            Frm_DalleN.MAJI_ImageDalle()
        End If

        lBuild = False

    End Sub

    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name
            Case Me.txt_Hd.Name, Me.txt_Td2.Name

                Select Case Frm_DalleN.myDalleLoc.type
                    Case cls_Dalle.Enum_TypeDalle.Pleine, cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                        ValMin = OptionsScope.EpDallePleineMin / kUnit
                    Case cls_Dalle.Enum_TypeDalle.Mixte
                        ValMin = (OptionsScope.EpDalleMixteMin + HPMINI) / kUnit
                End Select

                ValMax = TDMAXI / kUnit

            Case txt_EpPredalle.Name
                ValMin = 0 / kUnit
                ValMax = OptionsScope.RatioEpPredalleMax * Frm_DalleN.myDalleLoc.Ep_td / kUnit

            Case Me.txt_EpJoint.Name
                ValMin = 0
                ValMax = Frm_DalleN.myDalleLoc.preDalle_ep / kUnit

            Case Me.txt_RhoC.Name

                ValMin = 1500
                ValMin = Math.Min(OptionsScope.RhoCBetonLegerMin, OptionsScope.RhoCBetonNormalMin)
                ValMax = 0
                lValMax = False
                kUnit = 1

            Case Me.txt_Hh.Name

                ValMin = 0
                ValMax = OptionsScope.RatioEpRenformisMax * Frm_DalleN.myDalleLoc.Ep_td / kUnit

            Case Me.txt_Tc.Name
                ValMin = (OptionsScope.EpDalleMixteMin) / kUnit
                ValMax = 0
                lValMax = False

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

    Private Sub chk_BetonLeger_CheckedChanged(sender As Object, e As EventArgs) Handles chk_BetonLeger.CheckedChanged
        If lBuild Then Exit Sub

        Dim Index As Integer = Me.lst_ClasseBetonDalle.SelectedIndex

        Frm_DalleN.myDalleLoc.beton.lLeger = Me.chk_BetonLeger.Checked

        'RemplirComboClasseBeton()
        RemplirListeClasseBeton()

        'Me.cmb_ClasseBetonDalle.SelectedIndex = Index
        Me.lst_ClasseBetonDalle.SelectedIndex = Math.Min(Index, Me.lst_ClasseBetonDalle.Items.Count - 1)
        Frm_DalleN.myDalleLoc.beton.Classe = Me.lst_ClasseBetonDalle.Text

        Frm_DalleN.MAJI_ImageDalle()

        Dim lOK As Boolean
        ValideSaisieBeton(lOK, True)
    End Sub

    Private Sub ValideSaisieBeton(ByRef lOK As Boolean, lAffMesg As Boolean)
        '----------------------------------------------------------------------------------------------------------------------------
        '   24/01/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------------
        '   Vérifie les valeurs de Rho béton
        '----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations
        Dim valMax, valMin As Decimal
        Dim iErreur As Integer
        Const lValMin As Boolean = True
        Dim lValMax As Boolean = False
        Dim MessageErreur As String = ""
        Dim strValMin As String = ""
        Dim strValMax As String = ""

        '--( Traitement

        If Frm_DalleN.myDalleLoc.beton.lLeger Then
            lValMax = True
            valMax = OptionsScope.RhoCBetonLegerMax
            valMin = OptionsScope.RhoCBetonLegerMin
        Else
            valMin = OptionsScope.RhoCBetonNormalMin
        End If

        iErreur = ValideSaisieNombre(Me.txt_RhoC.Text, lValMin, valMin, lValMax, valMax)

        If iErreur = -3 Then
            lOK = False
            If Frm_DalleN.myDalleLoc.beton.lLeger Then
                strValMin = GetStringInUnitN(OptionsScope.RhoCBetonLegerMin, Enu_TypeVariable.MasseVolumique, 4, 1, Enu_AfficheUnite.OuiInterface, True)
                strValMax = GetStringInUnitN(OptionsScope.RhoCBetonLegerMax, Enu_TypeVariable.MasseVolumique, 4, 1, Enu_AfficheUnite.OuiInterface, True)
                MessageErreur = RemplaceDollar(RemplaceDollar(strErreurLeger, strValMin), strValMax)
            Else
                strValMin = GetStringInUnitN(OptionsScope.RhoCBetonNormalMin, Enu_TypeVariable.MasseVolumique, 4, 1, Enu_AfficheUnite.OuiInterface, True)
                MessageErreur = RemplaceDollar(strErreurNormal, strValMin)
            End If
            ErrorProvider.SetError(Me.txt_RhoC, MessageErreur)
        ElseIf iErreur = 0 Then
            ErrorProvider.Clear()
        End If

    End Sub

    Private Sub lst_ClasseBetonDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lst_ClasseBetonDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        If Frm_DalleN.myDalleLoc.beton.lLeger Then
            Frm_DalleN.myDalleLoc.beton.Classe = Me.ClasseBetonLeger(Me.lst_ClasseBetonDalle.SelectedIndex)
        Else
            Frm_DalleN.myDalleLoc.beton.Classe = Me.ClasseBeton(Me.lst_ClasseBetonDalle.SelectedIndex)
        End If

        MAJI_ProprietesBeton()
        Frm_DalleN.MAJI_ImageDalle()
    End Sub
#End Region

#Region " Autres évènements "

    Private Sub ComboBox_Enter(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.Enter, lst_ClasseBetonDalle.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.cmb_ClasseBetonDalle.Name, Me.lst_ClasseBetonDalle.Name
                Frm_DalleN.MAJI_SelectionTxtbox(1000)
        End Select

    End Sub

    Private Sub ComboBox_Leave(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.Leave
        If lBuild Then Exit Sub
        Frm_DalleN.MAJI_DeselectionTxtbox()
    End Sub

    Private Sub SaisieRdBDefEpMixte(sender As Object, e As EventArgs) Handles rdb_EpTotale.CheckedChanged, rdb_EpPleine.CheckedChanged

        If lBuild Then Exit Sub
        If Me.rdb_EpPleine.Checked Then
            DefEpMixte = Enu_DefEpMixte.Pleine
        Else
            DefEpMixte = Enu_DefEpMixte.Totale
        End If
        MAJI_SaisieEpMixte()
        Frm_DalleN.MAJI_DeselectionTxtbox()
        Frm_DalleN.MAJI_ImageDalle()
    End Sub

    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) Handles txt_Hh.Leave, txt_RhoC.Leave, txt_Td2.Leave, txt_Tc.Leave,
                                                                        txt_Hd.Leave, txt_EpPredalle.Leave, txt_EpJoint.Leave
        If lBuild Then Exit Sub
        Frm_DalleN.MAJI_DeselectionTxtbox()
    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) Handles txt_Hh.Enter, txt_RhoC.Enter, txt_Td2.Enter, txt_Tc.Enter,
                                                                        txt_Hd.Enter, txt_EpPredalle.Enter, txt_EpJoint.Enter
        If lBuild Then Exit Sub
        Dim iSelect As Integer
        Select Case sender.name
            Case Me.txt_Hd.Name, Me.txt_Td2.Name
                iSelect = 0
            Case Me.txt_Hh.Name
                iSelect = 1
            Case Me.txt_Tc.Name
                iSelect = 2
            Case Me.txt_EpPredalle.Name
                iSelect = 10
            Case Me.txt_EpJoint.Name
                iSelect = 11


            Case Me.txt_RhoC.Name
                iSelect = 1000
        End Select


        Frm_DalleN.MAJI_SelectionTxtbox(iSelect)
    End Sub

#End Region

#Region " Routines MAJI "

    Private Sub MAJI_ProprietesBeton()

        Frm_DalleN.myDalleLoc.beton.Calcul_Proprietes(MyProjet.Poutres(MyProjet.IndEnCours).Param.lGeneration1)

        Me.txt_Ecm.Text = GetStringInUnit(Frm_DalleN.myDalleLoc.beton.Ecm, Enu_TypeVariable.Contrainte, 0, 0, False, True)
        Me.txt_Fck.Text = GetStringInUnit(Frm_DalleN.myDalleLoc.beton.Fck, Enu_TypeVariable.Contrainte, 0, 0, False, True)

    End Sub

    Private Sub MAJI_TypeDalle()
        '------------------------------------------------------------------------------------
        '   27/06/23 :  Création - POM
        '------------------------------------------------------------------------------------
        '   MAJ de l'interface en fonction du type de dalle
        '------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------

        Select Case Frm_DalleN.MyDalleLoc.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                ' Me.pan_Bac.Enabled = False

                Me.pan_Renformis.Visible = True
                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True

                'Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                'Me.TLPan_Dalle.ColumnStyles(0).Width = 255

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                '  Me.pan_Bac.Enabled = False
                Me.pan_Predalle.Visible = True
                Me.pan_Renformis.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True

                'Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                'Me.TLPan_Dalle.ColumnStyles(0).Width = 280

            Case cls_Dalle.Enum_TypeDalle.Mixte
                '  Me.pan_Bac.Enabled = True
                Me.pan_Predalle.Visible = False
                Me.pan_Renformis.Visible = False
                Me.pan_EpaisseurMixte.Visible = True
                Me.pan_Epaisseur.Visible = False

                'Me.TLPan_Dalle.ColumnStyles(0).Width = 501
                'Me.TLpan_PartageV.ColumnStyles(1).Width = 250
        End Select

    End Sub

      Private Sub MAJI_SaisieEpMixte()

        PrepareTextBoxDipo(Me.txt_Td2, DefEpMixte = Enu_DefEpMixte.Totale)
        PrepareTextBoxDipo(Me.txt_Tc, DefEpMixte = Enu_DefEpMixte.Pleine)

    End Sub



#End Region


#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_RhoC.Paint, Img_Hh.Paint, img_EpPredalle.Paint,
                                                                           img_EpJoint.Paint, img_Td2.Paint, img_Tc.Paint, Img_Hd.Paint, img_Fck.Paint, img_Ecm.Paint

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

            Case Me.img_Ecm.Name
                strSymbol = "E"
                strIndice = "cm"
                lGrec = False
                lIndice = True

            Case Me.img_Fck.Name
                strSymbol = "f"
                strIndice = "ck"
                lGrec = False
                lIndice = True

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
            Case Me.Img_Hh.Name
                strSymbol = "t"
                strIndice = "h"

            Case Me.img_EpPredalle.Name
                strSymbol = "t"
                strIndice = "pre"
            Case Me.img_EpJoint.Name
                strSymbol = "t"
                strIndice = "j"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class