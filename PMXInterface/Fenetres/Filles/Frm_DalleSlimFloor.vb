Imports PMXMoteur2
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class Frm_DalleSlimFloor

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim strType(3) As String
    Dim strCofradal() As String 'contient les noms des cofradals à afficher dans le combobox
    Dim strCofradalNotFound As String 'message d'erreur dans la cas où le cofradal n'aurait pas été trouvé dans la BDD

    Dim ClasseBeton() As String = cls_Beton.TabClasseBeton
    Dim ClasseBetonLeger() As String = cls_Beton.TabClasseBetonLeger
    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

    Public MyPoutreLoc As New cls_Poutre(NomChargements)
    Public MyDalleLoc As New cls_Dalle
    Public MySectionLoc As New cls_Section
    'Dim COULEURTXTREADONLY As Color = SystemColors.ControlDark
    Const kADJUST As Decimal = 0.95

    Dim strAppuiTcontinus, strAppuiTRibContinu, strAppuiTBacNonContinu As String
    Dim strAppuiTDiscontinus As String
    Dim strAppuiLbacUncut As String
    Dim strAppuiLbacCut1, strAppuiLbacCut2 As String
    Dim strToolTipAddRebar, strToolTipRemoveRebar As String
    Dim strLitNo(1) As String
    Dim msgDessin(1) As String
    Dim lCote As Boolean = True 'indique si on affiche les cotations ou non

    Dim iSelect As Integer = -1
    Dim iLitSelect As Integer = 0       'Indice du lit d'armatures à l'affichage

    Dim lCofraPlus220 As Boolean

    ''' <summary>
    ''' Ajout GUD: les positions des lits d'armatures peuvent être relatives dans le cas de plusieurs nappes 
    ''' </summary>
    Dim zMin_Rel, zMax_Rel As Decimal

    Private Const TDMAXI As Decimal = 0.5
    'Private Const TDMINI As Decimal = 0.05
    Private Const HPMINI As Decimal = 0.04
    Private Const PHIMIN As Decimal = 0.003
    Private Const PHIMAX As Decimal = 0.04
    Private Const ESPMIN As Decimal = 0.05
    Private Const ESPMAX As Decimal = 0.5
    Private Const ZMIN As Decimal = 0.02
    Private Const DPMIN As Decimal = 0.05
    Private Const DPMAX As Decimal = 0.5
    Private Const MUPFMIN As Decimal = 100
    Private Const MUPFMAX As Decimal = 5000

    Private Enum Enu_DefEpMixte
        Totale                  ' Définition d'une dalle mixte par son épaisseur totale
        Pleine                  ' Définition d'une dalle mixte par son épaisseur au dessus du bac
    End Enum
    Dim DefEpMixte As Enu_DefEpMixte = Enu_DefEpMixte.Totale

    Dim FontFrm As Font

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialisationVariablesLocales()
        PreparerFenetre()
        AfficherDalleEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SLAB")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== FENETRE =====================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== GENERAL ======================================================================

                Me.lbl_General.Text = Bloc("GENERAL")
                strType(0) = Bloc("SOLIDSLAB")
                strType(1) = Bloc("COMPOSITESLAB")
                strType(2) = Bloc("PRECASTSLAB")
                strType(3) = Bloc("FULLPRECAST")

                ReDim strCofradal(Get_LenghtCofradalTable)
                strCofradal(0) = Bloc("USER")

                Dim str_TableCofra() As String = Get_ListName_Cofradal()

                For i As Integer = 0 To str_TableCofra.Length - 1
                    strCofradal(i + 1) = str_TableCofra(i)
                Next

                Me.lbl_TypeDalle.Text = Bloc("TYPE")
                Me.lbl_Epaisseur.Text = Bloc("THICKNESS")
                Me.lbl_EpaisseurM.Text = Bloc("THICKNESS")
                Me.lbl_EpPreDalle.Text = Bloc("PRESLAB")
                Me.lbl_EpJoint.Text = Bloc("JOINT")
                Me.lbl_Cofradal.Text = Bloc("COFRADAL")
                Me.lbl_Name.Text = Bloc("NAME")
                Me.lbl_dp.Text = Bloc("PRESLAB")
                Me.lbl_mupf.Text = Bloc("MSURF")

                strCofradalNotFound = Bloc("COFRANOTFOUND")

                '=== BETON ========================================================================

                Me.lbl_Beton.Text = Bloc("CONCRETE")

                Me.lbl_ClasseE.Text = Bloc("CLASS")
                Me.chk_BetonLeger.Text = Bloc("LIGHTCONCRETE")

                '=== BAC ==========================================================================

                Me.lbl_Bac.Text = Bloc("SHEETING")
                Me.lbl_BacNom.Text = Bloc("NAME")
                Me.btn_ModifierBac.Text = Bloc("MODIFYSH")

                Me.lbl_HauteurHp.Text = Bloc("HEIGHT")


                '=== ARMATURES ====================================================================

                strToolTipAddRebar = Bloc("ADDLAYER")
                strToolTipRemoveRebar = Bloc("REMOVELAYER")
                strLitNo(0) = Bloc("FIRSTLAYER")
                strLitNo(1) = Bloc("SECONDLAYER")

                '=== ACIER DES ARMATURES ==========================================================

                Me.lbl_Acier.Text = Bloc("REBARSTEEL")        '"Reinforcement steel"
                Me.lbl_ClasseA.Text = Bloc("CLASS")

                '=== MESSAGES =====================================================================

                strAppuiTcontinus = "Nervure et bac continus"
                strAppuiTRibContinu = "Nervure continue"
                strAppuiTBacNonContinu = "Bac discontinu"

                strAppuiTDiscontinus = "Nervure et bac discontinus"
                strAppuiLbacUncut = "Uncut deck"
                strAppuiLbacCut1 = "Cut deck"
                strAppuiLbacCut2 = "the width of the concrete through is equal to the width of the deck through"

                msgDessin(0) = Bloc("CONCRETE")
                msgDessin(1) = Bloc("REBARSTEEL")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim12.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim13.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "2"

        Me.etq_UnitRhoC.Text = "kg/m3"
        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub InitialisationVariablesLocales()

        'cls_Dalle.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Dalle, MyDalleLoc)
        'cls_Section.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Section, MySectionLoc)

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        MyDalleLoc = MyPoutreLoc.Dalle
        MySectionLoc = MyPoutreLoc.Section

        lCofraPlus220 = MyDalleLoc.Bac.lCofraplus220

        'Dans la première génération des EN, on supprime la dernière classes des tableaux

        If OptionsCalcul.Norme = Enu_Normes.Eurocodes_G1 Then

            Dim nbClasse As Integer = ClasseBeton.GetUpperBound(0)
            ReDim Preserve ClasseBeton(nbClasse)

            nbClasse = ClasseBetonLeger.GetUpperBound(0)
            ReDim Preserve ClasseBetonLeger(nbClasse)

        End If



    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux

        Me.lbl_Bac.BackColor = CouleurBackBandeaux
        Me.lbl_Bac.ForeColor = CouleurForeBandeaux
        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux
        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        '# Blocage des txtbox non modifiables

        Me.txt_BacNom.Enabled = False
        Me.txt_BacNom.BackColor = CouleurReadOnly


        'Me.txt_RhoC.Enabled = False
        'Me.txt_RhoC.BackColor = CouleurReadOnly
        Me.txt_Fsk.Enabled = False
        Me.txt_Fsk.BackColor = CouleurReadOnly

        Me.txt_Hp.Enabled = False
        Me.txt_Hp.BackColor = CouleurReadOnly

        Me.img_Bac.BorderStyle = BorderStyle.FixedSingle

    End Sub

    Private Sub PreparerFenetre()

        ErrorProvider.Clear()

        Me.img_Dalle.Dock = DockStyle.Fill

        ' Const MARGEPAN As Integer = 0

        Me.pan_Type.Controls.Add(Me.pan_Epaisseur)
        Me.pan_Epaisseur.Left = 5
        Me.pan_Epaisseur.Top = 35

        Me.pan_Type.Controls.Add(Me.pan_EpaisseurMixte)
        Me.pan_EpaisseurMixte.Left = 5
        Me.pan_EpaisseurMixte.Top = Me.pan_Epaisseur.Top

        Me.pan_Type.Controls.Add(Me.pan_Predalle)
        Me.pan_Predalle.Left = 5
        Me.pan_Predalle.Top = 55

        Me.pan_Type.Controls.Add(Me.pan_Cofradal)
        Me.pan_Cofradal.Left = 5
        Me.pan_Cofradal.Top = 60

        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboAvecTableau(Me.cmb_Cofradal, strCofradal)
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)
        RemplirComboClasseBeton()

        Me.rdb_EpPleine.Checked = (Me.DefEpMixte = Enu_DefEpMixte.Pleine)
        Me.rdb_EpTotale.Checked = (Me.DefEpMixte = Enu_DefEpMixte.Totale)

        MAJI_SaisieEpMixte()

    End Sub

    Private Sub RemplirComboClasseBeton()
        If MyDalleLoc.beton.lLeger Then
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBetonLeger)
        Else
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBeton)
        End If
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub AfficherDalleEnCours()

        '--> Type de dalle

        Select Case MyDalleLoc.type
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

        Me.txt_Td2.Text = GetStringNoUnit(MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension)
        Me.txt_Tc.Text = GetStringNoUnit(MyDalleLoc.Ep_td - MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension)

        Me.txt_Hd.Text = GetStringNoUnit(MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension)

        Me.txt_EpPredalle.Text = GetStringNoUnit(MyDalleLoc.preDalle_ep, Enu_TypeVariable.Dimension)
        Me.txt_EpJoint.Text = GetStringNoUnit(MyDalleLoc.preDalle_tjoint, Enu_TypeVariable.Dimension)

        '--> Béton

        Dim Chaine As String
        Chaine = MyDalleLoc.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) And Not MyDalleLoc.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        ElseIf Me.ClasseBetonLeger.Contains(Chaine) And MyDalleLoc.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBetonLeger, Chaine)
        Else
            Me.cmb_ClasseBetonDalle.SelectedIndex = 0
        End If

        Me.txt_RhoC.Text = GetStringInUnit(MyDalleLoc.beton.RhoC, Enu_TypeVariable.SansType, 3, 0, False)
        Me.chk_BetonLeger.Checked = MyDalleLoc.beton.lLeger

        MAJI_ProprietesBeton()

        '--> Bac

        AfficherBacEnCours()

        '--> Acier

        Chaine = MyDalleLoc.AcierArmatures.Classe
        If Me.ClasseAcierArma.Contains(Chaine) Then
            Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
        Else
            Me.cmb_Acier.SelectedIndex = 0
        End If
        MAJI_ProprietesAcier()

        '--> Armatures

        'Ajout GuD: Permet de réinitialiser la variable iLitSelect à l'ouverture
        If MyDalleLoc.LitArma(1).lActive Then
            iLitSelect = 1
            iSelect = 200
        Else
            iLitSelect = 0
            iSelect = 100
        End If

        '--> Cofradal

        If MyDalleLoc.Cofradal.lCustom Then
            Me.cmb_Cofradal.SelectedIndex = 0
        Else
            Me.cmb_Cofradal.SelectedItem = MyDalleLoc.Cofradal.nom
        End If

        Me.txt_dp.Enabled = MyDalleLoc.Cofradal.lCustom
        Me.txt_mupf.Enabled = MyDalleLoc.Cofradal.lCustom

        AfficherCofradalEnCours()

        '--> MAJ GUI

        MAJI_TypeDalle()

    End Sub

    Private Sub MAJI_SaisieEpMixte()

        PrepareTextBoxDipo(Me.txt_Td2, DefEpMixte = Enu_DefEpMixte.Totale)
        PrepareTextBoxDipo(Me.txt_Tc, DefEpMixte = Enu_DefEpMixte.Pleine)

    End Sub


    Private Sub AfficheNomBacEnCours()
        Me.txt_BacNom.Text = MyDalleLoc.Bac.Etiquette
        Me.txt_Hp.Text = GetStringInUnit(MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension, 4, 3, False)
    End Sub

    Private Sub AfficherBacEnCours()

        '--> Nom du bac

        AfficheNomBacEnCours()

    End Sub

    Private Sub MAJI_ProprietesBeton()

        MyDalleLoc.beton.Calcul_Proprietes()

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

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
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Type et géométrie dalle ----------------------------------------------------------------------------------------------------

        If (MyDalleLoc.type <> MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type = MyDalleLoc.type
        End If

        GereTransfertValeur(MyDalleLoc.Ep_td, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Ep_td, lModif)

        If MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Pleine Then _
        GereTransfertValeur(MyDalleLoc.Ep_th, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Ep_th, lModif)

        If MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee Then
            GereTransfertValeur(MyDalleLoc.preDalle_ep, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.preDalle_ep, lModif)
            GereTransfertValeur(MyDalleLoc.preDalle_tjoint, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.preDalle_tjoint, lModif)
        End If

        '--> Béton de la dalle

        GereTransfertValeur(MyDalleLoc.beton.lLeger, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.lLeger, lModif)
        GereTransfertValeur(MyDalleLoc.beton.RhoC, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.RhoC, lModif)

        GereTransfertValeur(MyDalleLoc.beton.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe, lModif)

        '--> Acier des armatures

        GereTransfertValeur(MyDalleLoc.AcierArmatures.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe, lModif)

        '--> Bac

        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Copie(MyDalleLoc.Bac, lModif)
        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.CopieAutresParam(MyDalleLoc.Bac, lModif)

        '--> Cofradal

        If cmb_Cofradal.SelectedIndex = 0 And Me.txt_NameCustomCofra.Text = "" Then
            MyDalleLoc.Cofradal.nom = Me.cmb_Cofradal.Items(0) 'on ajoute un nom par défaut = User ou Utilisateur
        End If

        GereTransfertValeur(MyDalleLoc.Cofradal.nom, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.nom, lModif)
        GereTransfertValeur(MyDalleLoc.Cofradal.dp, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.dp, lModif)
        GereTransfertValeur(MyDalleLoc.Cofradal.msurf, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.msurf, lModif)
        GereTransfertValeur(MyDalleLoc.Cofradal.lCustom, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.lCustom, lModif)

    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, MyDalleLoc.Bac, True, kADJUST)
    End Sub

    Private Sub img_Dalle_Paint(sender As Object, e As PaintEventArgs) Handles img_Dalle.Paint
        DessineDalle(e.Graphics, Me.img_Dalle.ClientRectangle.Width, Me.img_Dalle.ClientRectangle.Height,
                     MyPoutreLoc, fontfrm, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire, iSelect, msgDessin, lCote)
    End Sub

    '==== A METTRE DANS LE MODULE DESSIN ================================================================


#End Region

#Region " Evènements sur la BO Armatures + évènements saisie "

    Private Sub BOArma_CheckedChanged(sender As Object, e As EventArgs)

        If lBuild Then Exit Sub

        Me.img_Dalle.Invalidate()
    End Sub


#End Region

#Region " Evènements "

    Private Sub btn_ModifierBac_Click(sender As Object, e As EventArgs) Handles btn_ModifierBac.Click, txt_BacNom.Click, img_Bac.Click

        Dim Tc As Decimal = MyDalleLoc.EpaisseurActive
        Dim lOldCfp220 As Boolean = lCofraPlus220

        iFrmAppel = EnuFenetres.Dalle
        Frm_BacN.ShowDialog()

        If lCofraPlus220 <> lOldCfp220 Then
            Dim Td As Decimal = Tc + MyDalleLoc.Bac.Hp
            MyDalleLoc.Ep_td = Td
            Me.txt_Hd.Text = GetStringNoUnit(MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension)
        End If

        AfficheNomBacEnCours()
        Me.img_Bac.Invalidate()
        Me.img_Dalle.Invalidate()

    End Sub


    Private Sub img_Dalle_Resize(sender As Object, e As EventArgs) Handles img_Dalle.Resize
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub ComboBox_Enter(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.cmb_ClasseBetonDalle.Name
                iSelect = 1000
        End Select
        Me.img_Dalle.Invalidate()
    End Sub


    Private Sub LeaveAcierArma(sender As Object, e As EventArgs) Handles cmb_Acier.Leave
        If lBuild Then Exit Sub

        iSelect = -1

        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub EnterAcierArma(sender As Object, e As EventArgs) Handles cmb_Acier.Enter
        If lBuild Then Exit Sub
        iSelect = 1001
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub ComboBox_Leave(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub chk_BetonLeger_Enter(sender As Object, e As EventArgs) Handles chk_BetonLeger.Enter
        If lBuild Then Exit Sub

        iSelect = 1000

        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub chk_BetonLeger_Leave(sender As Object, e As EventArgs) Handles chk_BetonLeger.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Me.img_Dalle.Invalidate()
    End Sub


    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) Handles txt_RhoC.Leave, txt_Td2.Leave, txt_Tc.Leave, txt_Hd.Leave, txt_EpPredalle.Leave, txt_EpJoint.Leave, txt_dp.Leave, txt_mupf.Leave, txt_NameCustomCofra.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) Handles txt_RhoC.Enter, txt_Td2.Enter, txt_Tc.Enter, txt_Hd.Enter, txt_EpPredalle.Enter, txt_EpJoint.Enter, txt_dp.Enter, txt_mupf.Enter, txt_NameCustomCofra.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.txt_Hd.Name, Me.txt_Td2.Name
                iSelect = 0
            Case Me.txt_Tc.Name
                iSelect = 2
            Case Me.txt_EpPredalle.Name
                iSelect = 10
            Case Me.txt_EpJoint.Name
                iSelect = 11

            Case Me.txt_RhoC.Name
                iSelect = 1000
        End Select
        Me.img_Dalle.Invalidate()
    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
            Case 3 : MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
        End Select

        MAJI_TypeDalle()
        AfficherDalleEnCours()

        'Dim ValeurUI As Decimal
        'VerificationSaisie(Me.txt_zs, ValeurUI)
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub MAJI_TypeDalle()
        '------------------------------------------------------------------------------------
        '   27/06/23 :  Création - POM
        '------------------------------------------------------------------------------------
        '   MAJ de l'interface en fonction du type de dalle
        '------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------

        Select Case MyDalleLoc.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                Me.pan_Bac.Enabled = False

                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True
                Me.pan_Cofradal.Visible = False

                Me.TLPan_Dalle.ColumnStyles(0).Width = 280
                Me.TLpan_PartageV.ColumnStyles(1).Width = 0

                Me.Height = 410

                Me.TLpan_PartageV.Height = 325 'Ajustement du TL
                Me.TLpan_Gauche.RowStyles(1).Height = 75 'Ajustement du pan_Type

                Me.Width = 930

            Case cls_Dalle.Enum_TypeDalle.Mixte
                Me.pan_Bac.Enabled = True

                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = True
                Me.pan_Epaisseur.Visible = False
                Me.pan_Cofradal.Visible = False

                Me.TLPan_Dalle.ColumnStyles(0).Width = 560
                Me.TLpan_PartageV.ColumnStyles(1).Width = 280

                Me.Height = 430
                Me.TLpan_PartageV.Height = 345 'Ajustement du TL
                Me.TLpan_Gauche.RowStyles(1).Height = 95 'Ajustement du pan_Type

                Me.Width = 1210

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                Me.pan_Bac.Enabled = False

                Me.pan_Predalle.Visible = True
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True
                Me.pan_Cofradal.Visible = False

                Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                Me.TLPan_Dalle.ColumnStyles(0).Width = 280

                Me.Height = 450
                Me.TLpan_PartageV.Height = 365 'Ajustement du TL
                Me.TLpan_Gauche.RowStyles(1).Height = 115 'Ajustement du pan_Type

                Me.Width = 930

            Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                Me.pan_Bac.Enabled = False

                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True
                Me.pan_Cofradal.Visible = True

                'Me.Height -> voir MAJI_Cofradal

                Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                Me.TLPan_Dalle.ColumnStyles(0).Width = 280

                MAJI_Cofradal()

                Me.Width = 930
        End Select


    End Sub

    Private Sub MAJI_Cofradal()

        If cmb_Cofradal.SelectedIndex = 0 Then 'Cofradal Utilisateur
            Me.txt_NameCustomCofra.Visible = True
            Me.lbl_Name.Visible = True

            Me.lbl_dp.Location = New Point(Me.lbl_dp.Location.X, Me.lbl_Name.Location.Y + 25)
            Me.img_dp.Location = New Point(Me.img_dp.Location.X, Me.lbl_dp.Location.Y)
            Me.txt_dp.Location = New Point(Me.txt_dp.Location.X, Me.lbl_dp.Location.Y)
            Me.etq_UnitDim12.Location = New Point(Me.etq_UnitDim12.Location.X, Me.lbl_dp.Location.Y + 2.5)

            Me.lbl_mupf.Location = New Point(Me.lbl_mupf.Location.X, Me.lbl_dp.Location.Y + 20)
            Me.img_mupf.Location = New Point(Me.img_mupf.Location.X, Me.lbl_mupf.Location.Y)
            Me.txt_mupf.Location = New Point(Me.txt_mupf.Location.X, Me.lbl_mupf.Location.Y)
            Me.etq_UnitDim13.Location = New Point(Me.etq_UnitDim13.Location.X, Me.lbl_mupf.Location.Y + 2.5)


            Me.Height = 510
            Me.TLpan_PartageV.Height = 425 'Ajustement du TL
            Me.TLpan_Gauche.RowStyles(1).Height = 175 'Ajustement du pan_Type
        Else
            Me.txt_NameCustomCofra.Visible = False
            Me.lbl_Name.Visible = False

            Me.lbl_dp.Location = New Point(Me.lbl_dp.Location.X, Me.lbl_Name.Location.Y)
            Me.img_dp.Location = New Point(Me.img_dp.Location.X, Me.lbl_dp.Location.Y)
            Me.txt_dp.Location = New Point(Me.txt_dp.Location.X, Me.lbl_dp.Location.Y)
            Me.etq_UnitDim12.Location = New Point(Me.etq_UnitDim12.Location.X, Me.lbl_dp.Location.Y + 2.5)

            Me.lbl_mupf.Location = New Point(Me.lbl_mupf.Location.X, Me.lbl_dp.Location.Y + 20)
            Me.img_mupf.Location = New Point(Me.img_mupf.Location.X, Me.lbl_mupf.Location.Y)
            Me.txt_mupf.Location = New Point(Me.txt_mupf.Location.X, Me.lbl_mupf.Location.Y)
            Me.etq_UnitDim13.Location = New Point(Me.etq_UnitDim13.Location.X, Me.lbl_mupf.Location.Y + 2.5)

            Me.Height = 480
            Me.TLpan_PartageV.Height = 395 'Ajustement du TL
            Me.TLpan_Gauche.RowStyles(1).Height = 145 'Ajustement du pan_Type
        End If



    End Sub

    Private Sub AfficherCofradalEnCours()

        Me.txt_NameCustomCofra.Text = MyDalleLoc.Cofradal.nom
        Me.txt_dp.Text = GetStringInUnit(MyDalleLoc.Cofradal.dp, Enu_TypeVariable.Dimension, 4, 3, False)
        Me.txt_mupf.Text = GetStringInUnit(MyDalleLoc.Cofradal.msurf, Enu_TypeVariable.ChargeSurfacique, 4, 3, False)
    End Sub

    Private Sub SaisieTextChanged(sender As Object, e As EventArgs) Handles txt_RhoC.TextChanged, txt_Td2.TextChanged, txt_Tc.TextChanged, txt_Hd.TextChanged, txt_EpPredalle.TextChanged, txt_EpJoint.TextChanged, txt_dp.TextChanged, txt_mupf.TextChanged

        If lBuild Then Exit Sub
        'lBuild = True
        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_EpJoint.Name
                    MyDalleLoc.preDalle_tjoint = Valeur
                Case Me.txt_EpPredalle.Name
                    MyDalleLoc.preDalle_ep = Valeur

                Case Me.txt_Hd.Name
                    MyDalleLoc.Ep_td = Valeur
                    'lBuild = True
                    'Me.txt_Td2.Text = Me.txt_Hd.Text

                Case Me.txt_RhoC.Name
                    MyDalleLoc.beton.RhoC = Valeur

                Case Me.txt_Td2.Name
                    MyDalleLoc.Ep_td = Valeur
                    'lBuild = True
                    Me.txt_Tc.Text = GetStringNoUnit(MyDalleLoc.Ep_td - MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension)
                    'lBuild = False

                Case Me.txt_Tc.Name
                    MyDalleLoc.Ep_td = Valeur + MyDalleLoc.Bac.Hp
                    'lBuild = True
                    Me.txt_Td2.Text = GetStringNoUnit(MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension)
                    'lBuild = False

                Case Me.txt_dp.Name
                    If cmb_Cofradal.SelectedIndex = 0 Then MyDalleLoc.Cofradal.dp = Valeur

                Case Me.txt_mupf.Name
                    If cmb_Cofradal.SelectedIndex = 0 Then MyDalleLoc.Cofradal.msurf = Valeur

            End Select

            Me.img_Dalle.Invalidate()
        End If

        'lBuild = False

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

                Select Case MyDalleLoc.type
                    Case cls_Dalle.Enum_TypeDalle.Pleine, cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                        ValMin = OptionsScope.EpDallePleineMin / kUnit
                    Case cls_Dalle.Enum_TypeDalle.Mixte
                        ValMin = (OptionsScope.EpDalleMixteMin + HPMINI) / kUnit
                    Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                        ValMin = OptionsScope.EpDallePleineMin / kUnit
                End Select

                ValMin = Math.Max(ValMin, (MySectionLoc.hec + OptionsCalcul.DeltaCDev) / kUnit)

                ValMax = TDMAXI / kUnit

            Case txt_EpPredalle.Name
                ValMin = 0 / kUnit
                ValMax = Math.Min(MySectionLoc.hec, OptionsScope.RatioEpPredalleMax * MyDalleLoc.Ep_td) / kUnit

            Case Me.txt_EpJoint.Name
                ValMin = 0
                ValMax = MyDalleLoc.preDalle_ep / kUnit
            Case Me.txt_RhoC.Name

                ValMin = 1500
                ValMax = 0
                lValMax = False
                kUnit = 1

            Case Me.txt_Tc.Name 'dans le cas d'une dalle mixte
                ValMin = Math.Max(OptionsScope.EpDalleMixteMin, MySectionLoc.hec + OptionsCalcul.DeltaCDev - MyDalleLoc.Bac.Hp) / kUnit
                ValMax = 0
                lValMax = False

            Case Me.txt_dp.Name
                ValMin = DPMIN / kUnit '50 mm
                ValMax = Math.Min(MySectionLoc.hec, DPMAX) / kUnit '500 mm

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


    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged
        If lBuild Then Exit Sub
        MyDalleLoc.AcierArmatures.Classe = Me.ClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()

        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub cmb_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Cofradal.SelectedIndexChanged
        If lBuild Then Exit Sub

        Me.txt_dp.Enabled = cmb_Cofradal.SelectedIndex = 0
        Me.txt_mupf.Enabled = cmb_Cofradal.SelectedIndex = 0

        If Not cmb_Cofradal.SelectedIndex = 0 Then
            If Not MyDalleLoc.Cofradal.AjouteCofradalBDD(cmb_Cofradal.Text) Then
                MsgBox(strCofradalNotFound)
            End If
        End If

        MAJI_Cofradal()
        AfficherCofradalEnCours()


    End Sub

    Private Sub txt_NameCustomCofra_TextChanged(sender As Object, e As EventArgs) Handles txt_NameCustomCofra.TextChanged
        If cmb_Cofradal.SelectedIndex = 0 Then
            MyDalleLoc.Cofradal.nom = Me.txt_NameCustomCofra.Text
        End If
    End Sub

    Private Sub MAJI_ProprietesAcier()
        MyDalleLoc.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(MyDalleLoc.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

    Private Sub chk_BetonLeger_CheckedChanged(sender As Object, e As EventArgs) Handles chk_BetonLeger.CheckedChanged
        If lBuild Then Exit Sub

        Dim Index As Integer = Me.cmb_ClasseBetonDalle.SelectedIndex

        MyDalleLoc.beton.lLeger = Me.chk_BetonLeger.Checked

        RemplirComboClasseBeton()

        Me.cmb_ClasseBetonDalle.SelectedIndex = Index
        MyDalleLoc.beton.Classe = Me.cmb_ClasseBetonDalle.Text

        Me.img_Dalle.Invalidate()

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

    Private Sub cmb_ClasseBetonEnrobage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        If MyDalleLoc.beton.lLeger Then
            MyDalleLoc.beton.Classe = Me.ClasseBetonLeger(Me.cmb_ClasseBetonDalle.SelectedIndex)
        Else
            MyDalleLoc.beton.Classe = Me.ClasseBeton(Me.cmb_ClasseBetonDalle.SelectedIndex)
        End If

        MAJI_ProprietesBeton()
        Me.img_Dalle.Invalidate()
    End Sub


#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Fy.Paint, img_RhoC.Paint, img_Hp.Paint,
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

            Case Me.img_Fy.Name
                strSymbol = "f"
                strIndice = "sk"
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
            Case Me.img_Hp.Name
                strSymbol = "h"
                strIndice = "p"
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
        'DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Gauche,
        '   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class