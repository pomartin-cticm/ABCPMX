Imports PMXMoteur2
Imports System.IO
Imports System.Drawing.Drawing2D


Public Class Frm_Dalle

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim strType(2) As String

    Dim ClasseBeton() As String = Cls_Beton.TabClasseBeton
    Dim ClasseBetonLeger() As String = Cls_Beton.TabClasseBetonLeger
    Dim ClasseAcierArma() As String = Cls_AcierArmature.tabClasseAcierArma

    Public MyDalleLoc As New Cls_Dalle
    'Dim COULEURTXTREADONLY As Color = SystemColors.ControlDark
    Const kADJUST As Decimal = 0.95

    Dim strAppuiTcontinus, strAppuiTRibContinu, strAppuiTBacNonContinu As String
    Dim strAppuiTDiscontinus As String
    Dim strAppuiLbacUncut As String
    Dim strAppuiLbacCut1, strAppuiLbacCut2 As String
    Dim strToolTipAddRebar, strToolTipRemoveRebar As String
    Dim strLitNo(1) As String

    Dim iSelect As Integer = -1
    Dim iLitSelect As Integer = 0       'Indice du lit d'armatures à l'affichage

    Dim lCofraPlus220 As Boolean

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

                Me.lbl_TypeDalle.Text = Bloc("TYPE")
                Me.lbl_Epaisseur.Text = Bloc("THICKNESS")
                Me.lbl_Renformis.Text = Bloc("HAUNCH")
                Me.lbl_EpPreDalle.Text = Bloc("PRESLAB")
                Me.lbl_EpJoint.Text = Bloc("JOINT")

                '=== BETON ========================================================================

                Me.lbl_Beton.Text = Bloc("CONCRETE")

                Me.lbl_ClasseE.Text = Bloc("CLASS")
                Me.chk_BetonLeger.Text = Bloc("LIGHTCONCRETE")

                '=== BAC ==========================================================================

                Me.lbl_Bac.Text = Bloc("SHEETING")
                Me.lbl_BacNom.Text = Bloc("NAME")
                Me.btn_ModifierBac.Text = Bloc("MODIFYSH")
                Me.lbl_BacOrientation.Text = Bloc("RIBORIENT")
                Me.rdb_BacParallele.Text = Bloc("PARALLEL")
                Me.rdb_BacPerpendiculaire.Text = Bloc("PERPENDICULAR")

                Me.lbl_HauteurHp.Text = Bloc("HEIGHT")

                Me.lbl_BacConfiguration.Text = Bloc("RIBCONFIG")              '"Configuration des nervures sur appui"

                'Me.chk_BacPreperce.Text = "Bac prepercé"
                Me.lbl_ConnectorThroughTheWeb.Text = Bloc("CONNECTORANDSHEET")
                Me.rdb_ATraversBac.Text = Bloc("THROUGHDECKW")
                Me.rdb_Preperce.Text = Bloc("PREPUNCHED")

                '=== ARMATURES ====================================================================

                Me.lbl_Armatures.Text = Bloc("REBARS")
                Me.ToolTipDalle.SetToolTip(Me.chk_Lit1, Bloc("REINFLAYER1"))
                Me.ToolTipDalle.SetToolTip(Me.chk_Lit2, Bloc("REINFLAYER2"))
                Me.lbl_Diametre.Text = Bloc("DIAMETER")
                Me.lbl_Espacement.Text = Bloc("SPACING")
                Me.lbl_zs.Text = Bloc("LOCATION")

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

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitRhoC.Text = "kg/m3"
        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub InitialisationVariablesLocales()

        Cls_Dalle.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Dalle, MyDalleLoc)

        lCofraPlus220 = MyDalleLoc.Bac.lCofraplus220

        'Dans la première génération des EN, on supprime la dernière classes des tableaux

        If OptionsCalcul.Norme = Enu_Normes.Eurocodes_G1 Then

            Dim nbClasse As Integer = ClasseBeton.GetUpperBound(0)
            ReDim Preserve ClasseBeton(nbClasse - 1)

            nbClasse = ClasseBetonLeger.GetUpperBound(0)
            ReDim Preserve ClasseBetonLeger(nbClasse - 1)

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.rtxt_Configuration.BorderStyle = BorderStyle.None

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux

        Me.lbl_Bac.BackColor = CouleurBackBandeaux
        Me.lbl_Bac.ForeColor = CouleurForeBandeaux
        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux
        Me.lbl_Armatures.BackColor = CouleurBackBandeaux
        Me.lbl_Armatures.ForeColor = CouleurForeBandeaux
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

        Me.pan_Predalle.Top = Me.pan_Renformis.Top

    End Sub

    Private Sub PreparerFenetre()

        Me.img_Dalle.Dock = DockStyle.Fill

        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)
        RemplirComboClasseBeton()

        MAJI_ChangeBac()

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
            Case Cls_Dalle.Enum_TypeDalle.Pleine
                Me.cmb_TypeDalle.SelectedIndex = 0
            Case Cls_Dalle.Enum_TypeDalle.Mixte
                Me.cmb_TypeDalle.SelectedIndex = 1
            Case Cls_Dalle.Enum_TypeDalle.Prefabriquee
                Me.cmb_TypeDalle.SelectedIndex = 2
        End Select
        MAJI_TypeDalle()

        '--> Epaisseur

        Me.txt_Hd.Text = GetStringNoUnit(MyDalleLoc.t_d, Enu_TypeVariable.Dimension)
        Me.txt_Hh.Text = GetStringNoUnit(MyDalleLoc.t_h, Enu_TypeVariable.Dimension)

        '--> Béton

        Dim Chaine As String
        Chaine = MyDalleLoc.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
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

        MAJI_BOArmatures()
        MAJI_StatutBOArma()
        AfficherLitEncours()

    End Sub

    Private Sub AfficheNomBacEnCours()
        Me.txt_BacNom.Text = MyDalleLoc.Bac.Etiquette
        Me.txt_Hp.Text = GetStringInUnit(MyDalleLoc.Bac.h_p, Enu_TypeVariable.Dimension, 4, 3, False)
    End Sub

    Private Sub AfficherBacEnCours()

        '--> Nom du bac

        AfficheNomBacEnCours()

        '--> Orientation

        Select Case MyDalleLoc.Bac.orientation
            Case Cls_Bac.Enum_Orientation.Parallele
                Me.rdb_BacParallele.Checked = True
            Case Cls_Bac.Enum_Orientation.Perpendiculaire
                Me.rdb_BacPerpendiculaire.Checked = True
        End Select
        MAJI_OrientationBac()

        '--> Bac prépercé

        'Me.chk_BacPreperce.Checked = MyDalleLoc.Bac.lPreperce
        If MyDalleLoc.Bac.lPreperce Then
            Me.rdb_Preperce.Checked = True
        Else
            Me.rdb_ATraversBac.Checked = True
        End If

        '--> Configurations sur appui

        MAJI_ConfigurationAppuiBac()

        Select Case MyDalleLoc.Bac.AppuiL
            Case Cls_Bac.EnuConfigLAppui.BacCoupe
                Me.chk_L_PA2.Checked = True
            Case Cls_Bac.EnuConfigLAppui.BacNonCoupe
                Me.chk_L_PA1.Checked = True
        End Select

        Select Case MyDalleLoc.Bac.AppuiT
            Case Cls_Bac.EnuConfigTAppui.BetonSeulContinu
                Me.chk_T_PA2.Checked = True
            Case Cls_Bac.EnuConfigTAppui.Discontinu
                Me.chk_T_PA3.Checked = True
            Case Cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                Me.chk_T_PA1.Checked = True
        End Select

    End Sub

    Private Sub MAJI_ProprietesBeton()

        MyDalleLoc.beton.Calcul_Proprietes()

        'Me.txt_Fck.Text = GetStringNoUnit(MyDalleLoc.beton.Fck, Enu_TypeVariable.Contrainte)
        'Me.txt_RhoC.Text = GetStringNoUnit(MyDalleLoc.beton.Ecm, Enu_TypeVariable.ModuleY)

    End Sub

    Private Sub MAJI_BOArmatures()
        '-------------------------------------------------------------------------------------
        '   06/07/2023 - MAJ de la barre d'outils des armatures en fonction du nombre de lit
        '-------------------------------------------------------------------------------------

        Select Case MyDalleLoc.NbLitsArmaActifs
            Case 1
                'Me.TLpan_ChoixLit.ColumnStyles(1).Width = 0
                Me.chk_AjouterSupprimerLit.Image = imgList_BOArma.Images("Ajouter")
                Me.ToolTipDalle.SetToolTip(Me.chk_AjouterSupprimerLit, strToolTipAddRebar)
                Me.chk_Lit2.Visible = False
            Case 2
                'Me.TLpan_ChoixLit.ColumnStyles(1).Width = 46
                Me.chk_AjouterSupprimerLit.Image = imgList_BOArma.Images("Supprimer")
                Me.ToolTipDalle.SetToolTip(Me.chk_AjouterSupprimerLit, strToolTipRemoveRebar)
                Me.chk_Lit2.Visible = True
        End Select

    End Sub

    Private Sub AfficherLitEncours()

        Me.lbl_LitNo.Text = strLitNo(iLitSelect)
        Me.txt_PhiS.Text = GetStringInUnit(MyDalleLoc.LitArma(iLitSelect).PhiS, Enu_TypeVariable.Dimension, 4, 3, False)
        Me.txt_esp.Text = GetStringInUnit(MyDalleLoc.LitArma(iLitSelect).EspBar, Enu_TypeVariable.Dimension, 4, 3, False)
        Me.txt_zs.Text = GetStringInUnit(MyDalleLoc.LitArma(iLitSelect).z_s, Enu_TypeVariable.Dimension, 4, 3, False)

        Me.img_esp.Invalidate()
        Me.img_PhiS.Invalidate()
        Me.img_zs.Invalidate()
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

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMslab)

            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Dimensions ----------------------------------------------------------------------------------------------------

        If (MyDalleLoc.type <> MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type = MyDalleLoc.type
        End If

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_d <> MyDalleLoc.t_d) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_d = MyDalleLoc.t_d
        End If

        '--> Béton de la dalle

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe <> MyDalleLoc.beton.Classe) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe = MyDalleLoc.beton.Classe
        End If

        '--> Acier des armatures

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe <> MyDalleLoc.AcierArmatures.Classe) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe = MyDalleLoc.AcierArmatures.Classe
        End If

        '--> Bac

        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Copie(MyDalleLoc.Bac, lModif)
        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.CopieAutresParam(MyDalleLoc.Bac, lModif)

    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, MyDalleLoc.Bac, True)
    End Sub

    Private Sub img_Dalle_Paint(sender As Object, e As PaintEventArgs) Handles img_Dalle.Paint
        DessineDalle(e.Graphics, Me.img_Dalle.ClientRectangle.Width, Me.img_Dalle.ClientRectangle.Height,
                     MyDalleLoc, MyProjet.Poutres(MyProjet.IndEnCours).Section, iSelect)
    End Sub

    '==== A METTRE DANS LE MODULE DESSIN ================================================================

    Public Sub DessineBacTout(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyBac As Cls_Bac,
                              ByVal lTitre As Boolean,
                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '   EpDalle     [E] :   Epaisseur de la dalle béton
        '   VariableBac [E] :   Parametre du bac sélectionné (pour affichage en rouge)
        '   nbOndes     [E] :   Nombre d'ondes sur lequel on représente le bac
        '   lCotation   [E] :   Indique si on met les cotations sur le dessin
        '   lCotEpTot   [E] :   Indique si cotation epaisseur bac+dalle
        '   lTitre      [E] :   Indique si affichage du titre du bac
        '   ParAff      [S] :   Paramètres d'Affichage
        '   lMemb       [E] :   Indique si on représente la semelle sup de la memb sup
        '   tfSup       [E] :   Epasseur semelle de la membrure superieure
        '   hMax        [E] :   Epaisseur maximale à considérer pour le dessin de la dalle
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage

        Dim ColorPen As Color = Color.Blue
        Dim ColorRedPen As Color = Color.Red

        Dim myBrushBac As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.LightGray, Color.DarkGray)
        Dim MyPenBrush As New SolidBrush(ColorPen)
        Dim MyPenRedBrush As New SolidBrush(ColorRedPen)
        Dim MyPen As New Pen(ColorPen)
        Dim MyPenRed As New Pen(ColorRedPen)
        Dim MyFontNormal As Font = FontBase

        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double

        Dim lRaidSup As Boolean

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim nbOndes As Integer = 5

        Dim MyPenBac As New Pen(BleuCTICM, 2)

        '--> Initialisation

        lRaidSup = MyBac.HasRaidisseurSup

        dCar = (MyBac.e_p + MyBac.b_b) / 2

        '--> Preparation de la zone d'affichage - Calcul de ParAff


        xMin = 0
        xMax = MyBac.LargeurModule

        yMin = 0
        yMax = MyBac.h_p

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Calcul des points du pourtour de la dalle

        If lRaidSup Then
            MyBac.PrepareContourModuleBacRaidi(xPts, yPts, nbPts)
        Else
            MyBac.PrepareContourModuleBacSimple(xPts, yPts, nbPts)
        End If

        '--> Remplissage contour

        'ContourZone(myGr, New Pen(BlueAM), xPts, yPts, nbPts, MyParAff, True)

        ContourZone(myGr, MyPenBac, xPts, yPts, nbPts, MyParAff, True)

        ''--> Liberation des Font, Pen et Brush

        MyPenBac.Dispose()


    End Sub

#End Region

#Region " Evènements sur la BO Armatures + évènements saisie "

    Private Sub BOArma_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Lit2.CheckedChanged, chk_Lit1.CheckedChanged

        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_Lit1.Name : iLitSelect = 0 : iSelect = 100
            Case Me.chk_Lit2.Name : iLitSelect = 1 : iSelect = 200
        End Select

        AfficherLitEncours()
        MAJI_StatutBOArma()
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub MAJI_StatutBOArma()
        Dim lBuildBack As Boolean = lBuild
        lBuild = True

        Me.chk_Lit1.Checked = (iLitSelect = 0)
        Me.chk_Lit2.Checked = (iLitSelect = 1)

        Me.chk_AjouterSupprimerLit.Checked = False

        lBuild = lBuildBack
    End Sub

    Private Sub SaisieArma(sender As Object, e As EventArgs) Handles txt_zs.TextChanged, txt_PhiS.TextChanged, txt_esp.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_PhiS.Name
                    MyDalleLoc.LitArma(iLitSelect).PhiS = Valeur

                Case Me.txt_esp.Name
                    MyDalleLoc.LitArma(iLitSelect).EspBar = Valeur

                Case Me.txt_zs.Name
                    MyDalleLoc.LitArma(iLitSelect).z_s = Valeur

            End Select

            Me.img_Dalle.Invalidate()
        End If

    End Sub

    Private Sub chk_AjouterSupprimerLit_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AjouterSupprimerLit.CheckedChanged
        If lBuild Then Exit Sub

        Select Case iLitSelect
            Case 0
                '# Cas où on ajoute un lit = on sélectionne le second (créé)
                MyDalleLoc.LitArma(1).lActive = True
                iLitSelect = 1 : iSelect = 200
            Case 1
                '# Cas où on supprime le second lit : on sélectionne le premier
                MyDalleLoc.LitArma(1).lActive = False
                iLitSelect = 0 : iSelect = 100
        End Select

        Me.img_Dalle.Invalidate()
        MAJI_BOArmatures()
        MAJI_StatutBOArma()
        AfficherLitEncours()

    End Sub
#End Region

#Region " Evènements "

    Private Sub btn_ModifierBac_Click(sender As Object, e As EventArgs) Handles btn_ModifierBac.Click, txt_BacNom.Click, img_Bac.Click

        Dim Tc As Decimal = MyDalleLoc.EpaisseurActive
        Dim lOldCfp220 As Boolean = lCofraPlus220

        iFrmAppel = EnuFenetres.DalleN
        Frm_BacN.ShowDialog()

        MAJI_ChangeBac()
        If lCofraPlus220 <> lOldCfp220 Then
            Dim Td As Decimal = Tc + MyDalleLoc.Bac.h_p
            MyDalleLoc.t_d = Td
            Me.txt_Hd.Text = GetStringNoUnit(MyDalleLoc.t_d, Enu_TypeVariable.Dimension)
        End If

        AfficheNomBacEnCours()
        Me.img_Bac.Invalidate()
        Me.img_Dalle.Invalidate()

    End Sub

    Private Sub MAJI_ChangeBac()
        lCofraPlus220 = MyDalleLoc.Bac.lCofraplus220

        If lCofraPlus220 Then
            Me.rdb_BacPerpendiculaire.Checked = True
            Me.rdb_BacParallele.Visible = False
            Me.pan_ConfigurationNervures.Visible = False
            'Me.pan_DispoConnecteur.Visible = False
        Else
            Me.rdb_BacParallele.Visible = True
            Me.pan_DispoConnecteur.Visible = True
            ' Me.pan_ConfigurationNervures.Visible = True
        End If

        MAJI_OrientationBac()

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


    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) Handles txt_Hh.Leave, txt_Hd.Leave, txt_zs.Leave, txt_PhiS.Leave, txt_esp.Leave, txt_RhoC.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) Handles txt_Hh.Enter, txt_Hd.Enter, txt_zs.Enter, txt_PhiS.Enter, txt_esp.Enter, txt_RhoC.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.txt_Hd.Name
                iSelect = 0
            Case Me.txt_Hh.Name
                iSelect = 1
            Case Me.txt_PhiS.Name
                iSelect = (iLitSelect + 1) * 100 + 1
            Case Me.txt_esp.Name
                iSelect = (iLitSelect + 1) * 100 + 2
            Case Me.txt_zs.Name
                iSelect = (iLitSelect + 1) * 100 + 3

            Case Me.txt_RhoC.Name
                iSelect = 1000
        End Select
        Me.img_Dalle.Invalidate()
    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub ConfigTCheckedChanged(sender As Object, e As EventArgs) Handles chk_T_PA3.CheckedChanged, chk_T_PA2.CheckedChanged, chk_T_PA1.CheckedChanged, chk_L_PA2.CheckedChanged, chk_L_PA1.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_T_PA1.Name
                'Me.rtxt_Configuration.Text = "Nervure et bac continus"
                MyDalleLoc.Bac.AppuiT = Cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                UnselectChkTConfig(Me.chk_T_PA1.Name)
            Case Me.chk_T_PA2.Name
                'Me.rtxt_Configuration.Text = "Nervure continue" & Chr(13) & "Bac discontinu"
                MyDalleLoc.Bac.AppuiT = Cls_Bac.EnuConfigTAppui.BetonSeulContinu
                UnselectChkTConfig(Me.chk_T_PA2.Name)
            Case Me.chk_T_PA3.Name
                'Me.rtxt_Configuration.Text = "Nervure et bac discontinus"
                MyDalleLoc.Bac.AppuiT = Cls_Bac.EnuConfigTAppui.Discontinu
                UnselectChkTConfig(Me.chk_T_PA3.Name)
            Case Me.chk_L_PA1.Name
                'Me.rtxt_Configuration.Text = "Uncut deck"
                MyDalleLoc.Bac.AppuiL = Cls_Bac.EnuConfigLAppui.BacNonCoupe
                UnselectChkLConfig(Me.chk_L_PA1.Name)
            Case Me.chk_L_PA2.Name
                'Me.rtxt_Configuration.Text = "Uncut deck" & Chr(13) & "the width of the concrete through is equal to the width of the deck through"
                MyDalleLoc.Bac.AppuiL = Cls_Bac.EnuConfigLAppui.BacCoupe
                UnselectChkLConfig(Me.chk_L_PA2.Name)
        End Select

        MAJI_ConfigurationAppuiBac()
        Me.img_Dalle.Invalidate()

    End Sub

    Private Sub MAJI_ConfigurationAppuiBac()
        '----------------------------------------------------------------------------------------------------------------
        '   Mise à jour du texte d'explication en fct de la configuration d'appui du bac
        '----------------------------------------------------------------------------------------------------------------

        Select Case MyDalleLoc.Bac.orientation
            Case Cls_Bac.Enum_Orientation.Perpendiculaire
                Select Case MyDalleLoc.Bac.AppuiT
                    Case Cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                        Me.rtxt_Configuration.Text = strAppuiTcontinus
                    Case Cls_Bac.EnuConfigTAppui.BetonSeulContinu
                        Me.rtxt_Configuration.Text = strAppuiTRibContinu & Chr(13) & strAppuiTBacNonContinu
                    Case Cls_Bac.EnuConfigTAppui.Discontinu
                        Me.rtxt_Configuration.Text = strAppuiTDiscontinus
                End Select
            Case Cls_Bac.Enum_Orientation.Parallele
                Select Case MyDalleLoc.Bac.AppuiL
                    Case Cls_Bac.EnuConfigLAppui.BacCoupe
                        Me.rtxt_Configuration.Text = strAppuiLbacCut1 & Chr(13) & strAppuiLbacCut2
                    Case Cls_Bac.EnuConfigLAppui.BacNonCoupe
                        Me.rtxt_Configuration.Text = strAppuiLbacUncut
                End Select
        End Select

    End Sub

    Private Sub UnselectChkTConfig(NameSelect As String)
        Dim lbuildBack As Boolean = lBuild
        lBuild = True
        If Me.chk_T_PA1.Name <> NameSelect Then Me.chk_T_PA1.Checked = False
        If Me.chk_T_PA2.Name <> NameSelect Then Me.chk_T_PA2.Checked = False
        If Me.chk_T_PA3.Name <> NameSelect Then Me.chk_T_PA3.Checked = False
        lBuild = lbuildBack
    End Sub

    Private Sub UnselectChkLConfig(NameSelect As String)
        Dim lbuildBack As Boolean = lBuild
        lBuild = True
        If Me.chk_L_PA1.Name <> NameSelect Then Me.chk_L_PA1.Checked = False
        If Me.chk_L_PA2.Name <> NameSelect Then Me.chk_L_PA2.Checked = False
        lBuild = lbuildBack
    End Sub

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Prefabriquee
        End Select

        MAJI_TypeDalle()
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
            Case Cls_Dalle.Enum_TypeDalle.Pleine
                Me.pan_Bac.Enabled = False

                Me.pan_Renformis.Visible = True
                Me.pan_Predalle.Visible = False

                Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                Me.TLPan_Dalle.ColumnStyles(0).Width = 255

            Case Cls_Dalle.Enum_TypeDalle.Prefabriquee
                Me.pan_Bac.Enabled = False
                Me.pan_Predalle.Visible = True
                Me.pan_Renformis.Visible = False

                Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                Me.TLPan_Dalle.ColumnStyles(0).Width = 255

            Case Cls_Dalle.Enum_TypeDalle.Mixte
                Me.pan_Bac.Enabled = True
                Me.pan_Predalle.Visible = False
                Me.pan_Renformis.Visible = False

                Me.TLPan_Dalle.ColumnStyles(0).Width = 501
                Me.TLpan_PartageV.ColumnStyles(1).Width = 250
        End Select

    End Sub

    Private Sub SaisieTextChanged(sender As Object, e As EventArgs) Handles txt_Hd.TextChanged, txt_Hh.TextChanged, txt_RhoC.TextChanged

        If lBuild Then Exit Sub
        lBuild = True
        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_Hd.Name
                    MyDalleLoc.t_d = Valeur

                Case Me.txt_Hh.Name
                    MyDalleLoc.t_h = Valeur

                Case Me.txt_RhoC.Name
                    MyDalleLoc.beton.RhoC = Valeur

            End Select

            Me.img_Dalle.Invalidate()
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
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Const TDMAXI As Decimal = 0.5
        'Const TDMINI As Decimal = 0.05
        Const HPMINI As Decimal = 0.04
        Const PHIMIN As Decimal = 0.003
        Const PHIMAX As Decimal = 0.04
        Const ESPMIN As Decimal = 0.05
        Const ESPMAX As Decimal = 0.5
        Const ZMIN As Decimal = 0.02

        Select Case MyTxt.Name
            Case Me.txt_Hd.Name

                Select Case MyDalleLoc.type
                    Case Cls_Dalle.Enum_TypeDalle.Pleine, Cls_Dalle.Enum_TypeDalle.Prefabriquee
                        ValMin = OptionsScope.EpDallePleineMin / kUnit
                    Case Cls_Dalle.Enum_TypeDalle.Mixte
                        ValMin = (OptionsScope.EpDalleMixteMin + HPMINI) / kUnit
                End Select

                ValMax = TDMAXI / kUnit

            Case Me.txt_RhoC.Name

                ValMin = 1500
                ValMax = 0
                lValMax = False
                kUnit = 1

            Case Me.txt_PhiS.Name

                ValMin = PHIMIN / kUnit
                ValMax = PHIMAX / kUnit

            Case Me.txt_esp.Name

                ValMin = ESPMIN / kUnit
                ValMax = ESPMAX / kUnit

            Case Me.txt_zs.Name

                ValMin = ZMIN / kUnit
                ValMax = (MyDalleLoc.EpaisseurActive - ZMIN) / kUnit

            Case Me.txt_Hh.Name

                ValMin = 0
                ValMax = OptionsScope.RatioEpRenformisMax * MyDalleLoc.t_d / kUnit

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
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

    Private Sub MAJI_ProprietesAcier()
        MyDalleLoc.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(MyDalleLoc.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

    Private Sub rdb_Preperce_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Preperce.CheckedChanged, rdb_ATraversBac.CheckedChanged
        If lBuild Then Exit Sub

        MyDalleLoc.Bac.lPreperce = Me.rdb_Preperce.Checked
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

    Private Sub OrientationBac_checkedChanged(sender As Object, e As EventArgs) Handles rdb_BacParallele.CheckedChanged, rdb_BacPerpendiculaire.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.rdb_BacParallele.Name : MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele
            Case Me.rdb_BacPerpendiculaire.Name : MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire
        End Select
        MAJI_OrientationBac()
        Me.img_Dalle.Invalidate()

    End Sub

    Private Sub MAJI_OrientationBac()

        Me.chk_L_PA1.Visible = False '   (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele)
        Me.chk_L_PA2.Visible = False '   (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele)
        Me.chk_T_PA1.Visible = (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire)
        Me.chk_T_PA2.Visible = (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire)
        Me.chk_T_PA3.Visible = (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire)

        Me.pan_DispoConnecteur.Visible = (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire) And Not lCofraPlus220

    End Sub

    Private Sub cmb_ClasseBetonEnrobage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBetonDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        MyDalleLoc.beton.Classe = Me.ClasseBeton(Me.cmb_ClasseBetonDalle.SelectedIndex)

        MAJI_ProprietesBeton()
        Me.img_Dalle.Invalidate()
    End Sub


#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles Img_Hd.Paint, img_Fy.Paint, img_RhoC.Paint, img_Hp.Paint, Img_Hh.Paint, img_zs.Paint, img_PhiS.Paint, img_esp.Paint,
        img_EpPredalle.Paint, img_EpJoint.Paint

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

            Case Me.Img_Hd.Name
                strSymbol = "t"
                strIndice = "d"
            Case Me.Img_Hh.Name
                strSymbol = "t"
                strIndice = "h"
            Case Me.img_Hp.Name
                strSymbol = "h"
                strIndice = "p"
            Case Me.img_esp.Name
                strSymbol = "e"
                strIndice = "s" & (iLitSelect + 1).ToString
            Case Me.img_PhiS.Name
                strSymbol = "f"
                strIndice = "s" & (iLitSelect + 1).ToString
                lGrec = True
            Case Me.img_zs.Name
                strSymbol = "z"
                strIndice = "s" & (iLitSelect + 1).ToString
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
        'DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Gauche,
        '   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class