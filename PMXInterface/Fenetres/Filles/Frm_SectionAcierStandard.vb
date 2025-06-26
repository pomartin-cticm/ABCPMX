Imports System.IO
Imports PMXMoteur2

Public Class Frm_SectionAcierStandard

#Region " Constantes et structures "
    Const STARCHAR As String = "*"
    Const RATIOHIGAMME As Double = 0.45
    Dim ColorGridHI As Color = Color.Blue
    Const EPSILONG As Double = 0.0001
    Const iFRMSECTION As Integer = 4
    Structure strucAcierLocal
        Dim Nuance As String
        Dim Qualite As String
        Dim Reduc As String
        Dim lAvailable As Boolean
    End Structure

    Enum EnuDrawProperty
        Fy
        Fu
    End Enum

#End Region

#Region " Variables "

    Dim lBuild As Boolean = True

    Dim MySectionLoc As New cls_Section
    Dim myHauteurHw As Decimal

    Enum Enu_DefinitionH
        HauteurTotale
        HauteurAme
    End Enum
    Dim DefinitionHauteur As Enu_DefinitionH = Enu_DefinitionH.HauteurTotale

    Dim DrawProperty As EnuDrawProperty = EnuDrawProperty.Fy

    '--> Couleurs
    Dim BClrCompatible As Color
    Dim BClrNotC As Color = Color.LightGray
    Dim ColorGrade As Color = Color.Crimson
    Dim ColorNotPossible As Color = Color.LightGray

    '---- Memoriser les lignes tableaux sélectionnées
    Dim iLignePro, iLigneAcier As Integer

    '--> Gestion des dessins
    Dim iSelect As Integer = -1
    Dim kAdjust As Decimal = 0.95

    '--> Textes
    Dim ILangueDelivery As Integer = 0
    Dim strDeliveryConditions As String
    Dim str_InfoH(1) As String

    '---- Gestion du cas où aucun profilé n'est disponible pour une série
    Dim lAvailPro As Boolean = True

    '---- Nuance S235/S275 autorisée (V3.09)
    Dim lNuancePossible As Boolean
    Dim NuancesExclues() As String = {"S235", "S275"}

    Dim SizeFont As Single = SizeFontFrm
    Dim FontFrm As Font

    'Dim lDessinFy As Boolean = True

    '---- Gestion des aciers pour les plats

    Dim AcierPlats As New List(Of strucAcierLocal)
    Private NuancesPlats() As String = {"S235", "S275", "S355"}


    Dim strWarningA As String
    Dim ErreurRatioAf As String

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_SectionAcierStandard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Private Sub Frm_SectionAcierStandard_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        PreparerFenetre()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_STEELSECTIONSUSUAL")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ===============================================================

                Me.Text = Bloc("TITLE")
                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("OK")

                '=== PROFILE ===============================================================

                Me.lbl_Section.Text = Bloc("PROFILE")

                Me.rdb_Lamine.Text = Bloc("HOTROLLED")
                Me.rdb_PRS.Text = Bloc("WELDEDP")
                Me.rdb_PRS_symetrique.Text = Bloc("WELDEDSYM")

                Me.lbl_Gamme.Text = Bloc("SERIAL")
                Me.lbl_Profiles.Text = Bloc("PROFILE")


                Me.lbl_Height.Text = Bloc("HEIGHT")
                Me.lbl_SemelleInf.Text = Bloc("LOWERF")
                Me.lbl_SemelleSup.Text = Bloc("UPPERF")
                Me.lbl_Ame.Text = Bloc("WEB")

                '=== RENFORT =============================================================

                Me.chk_Plat.Text = Bloc("PLATE")
                Me.lbl_Wplat.Text = Bloc("PLATEW")
                Me.lbl_Tplat.Text = Bloc("PLATET")
                Me.lbl_NuancePlat.Text = Bloc("PLATEGRADE")

                '=== STEEL ===============================================================

                Me.lbl_Acier.Text = Bloc("STEEL")
                Me.lbl_Grade.Text = Bloc("STEELGRADE")
                Me.lbl_Qualite.Text = Bloc("QUALITY")
                Me.lbl_ReductionCurve.Text = Bloc("REDUCTIONCURVE")


                '=== CHAINES =============================================================

                strDeliveryConditions = Bloc("DELIVERYCOND")
                str_InfoH(0) = Bloc("AUTOMATICHW")
                str_InfoH(1) = Bloc("AUTOMATICHA")

                strWarningA = Bloc("WARNINGRATIOAF")

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

    Private Sub InitialiseLngDeliveryIndex()

        If MyCatalogue.Langues.IndexOf(LogicielOptions.IndLangue) > 0 Then
            ILangueDelivery = MyCatalogue.Langues.IndexOf(LogicielInfo.ListeLangue(LogicielOptions.IndLangue).Substring(0, 2).ToUpper)
        Else
            If MyCatalogue.Langues.IndexOf(LogicielInfo.ListeLangue(0).Substring(0, 2).ToUpper) >= 0 Then
                ILangueDelivery = MyCatalogue.Langues.IndexOf(LogicielInfo.ListeLangue(0).Substring(0, 2).ToUpper)
            Else
                ILangueDelivery = 0
            End If
        End If

    End Sub

    Private Sub PreparerFenetre()

        Dim lPRS As Boolean
        Dim Ratio1, Ratio2 As Single
        Dim lEnrob As Boolean

        Me.pan_Acier.Dock = DockStyle.Fill
        Me.img_Section.Dock = DockStyle.Fill
        Me.img_ReductionCurve.Dock = DockStyle.Fill

        AfficheBtnFyFu()

        '== Preparation des options disponibles en fonctions du maitre d'ouvrage

        'Select Case LogicielInfo.Maitre
        '    Case EnuMaitre.ArcelorMittal
        '        If Not LogicielOptions.lExpert Then Me.TLpan_Gauche.RowStyles(1).Height = 0
        '    Case EnuMaitre.CTICM
        '        Me.GridDelivery.Visible = True ' LogicielOptions.lExpert
        'End Select

        '== Preparation des options disponibles en fonctions des réglages

        '# (PRS accessibles en expert)
        lPRS = LogicielReglages.lPRS Or LogicielOptions.lExpert
        lEnrob = MyProjet.Poutres(MyProjet.IndEnCours).lEnrobage
        Me.rdb_PRS.Visible = lPRS And Not lenrob
        Me.rdb_PRS_symetrique.Visible = lPRS
        'Me.GridDelivery.Visible = LogicielReglages.lDelivery
        '  Me.lbl_Delivery.Visible = LogicielReglages.lDelivery

        '== Transfert vers variable locale

        cls_Section.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Section, MySectionLoc)
        myHauteurHw = MySectionLoc.ProfilA.HauteurAmeHw

        BClrCompatible = Me.lst_GammeS.BackColor

        '== Préparation des listes

        RemplirSeries()
        InitialiseLngDeliveryIndex()

        Ratio1 = Me.lbl_Grade.Width / (Me.lbl_Grade.Width + Me.lbl_Qualite.Width + Me.lbl_ReductionCurve.Width)
        Ratio2 = Me.lbl_Qualite.Width / (Me.lbl_Grade.Width + Me.lbl_Qualite.Width + Me.lbl_ReductionCurve.Width)

        PrepareLookGrille(Me.Grid_ProfilesSup, Me.Col_HISTARSup, Me.Col_ListeSup, Me.lst_GammeS.BackColor, RATIOHIGAMME)
        PrepareLookGrille(Me.GridAciers, Me.Col_Grade, Me.Col_Qualite, Me.Col_ReductionCurve, Me.lst_GammeS.BackColor, Ratio1, Ratio2)
        'PrepareLookGrille(Me.GridDelivery, Me.Col_Index, Me.Col_Message, Me.lst_GammeS.BackColor, 0.1)
        'PrepareGridDelivery()

        '== Parametrage PRS

        Select Case DefinitionHauteur
            Case Enu_DefinitionH.HauteurAme : Me.chk_Hw.Checked = True
            Case Enu_DefinitionH.HauteurTotale : Me.chk_Ht.Checked = True
        End Select
        MAJ_DefinitionHauteur()

        '== Plats

        Me.pan_Plat.Width = Me.Grid_ProfilesSup.Width + Me.Grid_ProfilesSup.Left - Me.lst_GammeS.Left
        RemplirCmbNuancesPlats()

        '==Rapoort des aires

        Dim strvalMin, strvalMax As String

        strvalMin = GetStringInUnitN(OptionsScope.RapportAfMin, Enu_TypeVariable.SansType, 4, 3, NON_U, True)
        strValMax = GetStringInUnitN(OptionsScope.RapportAfMax, Enu_TypeVariable.SansType, 4, 3, NON_U, True)

        ErreurRatioAf = RemplaceDollar(RemplaceDollar(strWarningA, strValMin), strValMax)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Section.BackColor = CouleurBackBandeaux
        Me.lbl_Section.ForeColor = CouleurForeBandeaux

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        'Me.pan_Gauche.AutoScroll = False
        Me.TLpan_Gauche.Height = 410

        FontFrm = New Font(FontBase.Name, SizeFont)

        PrepareTextBoxDipo(Me.txt_RatioAf, False)

    End Sub

    Private Sub AfficherPoutreEnCours()

        Const NON As Enu_AfficheUnite = Enu_AfficheUnite.Non

        Select Case MySectionLoc.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                Me.rdb_Lamine.Checked = True
                AfficherProfileLamineEnCours()
                AfficherPlatEnCours()
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                Me.rdb_PRS_symetrique.Checked = True
                AfficherPRSEnCours()
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                Me.rdb_PRS.Checked = True
                AfficherPRSEnCours()
        End Select

        MAJ_FonctionType()

        Me.txt_Bfi.ReadOnly = Not (MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym)
        Me.txt_Tfi.ReadOnly = Not (MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym)

        If MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym Then
            MySectionLoc.ProfilA.Bfi = MySectionLoc.ProfilA.Bfs
            MySectionLoc.ProfilA.Tfi = MySectionLoc.ProfilA.Tfs
        End If

        Me.txt_Ha.Text = GetStringInUnitN(MySectionLoc.ProfilA.ha, Enu_TypeVariable.Dimension, 4, 1, NON, True)
        Me.txt_Hw.Text = GetStringInUnitN(MySectionLoc.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension, 4, 1, NON, True)
        Me.txt_Bfi.Text = GetStringInUnitN(MySectionLoc.ProfilA.Bfi, Enu_TypeVariable.Dimension, 4, 1, NON, True)
        Me.txt_Bfs.Text = GetStringInUnitN(MySectionLoc.ProfilA.Bfs, Enu_TypeVariable.Dimension, 4, 1, NON, True)
        Me.txt_Tfi.Text = GetStringInUnitN(MySectionLoc.ProfilA.Tfi, Enu_TypeVariable.Dimension, 4, 1, NON, True)
        Me.txt_Tfs.Text = GetStringInUnitN(MySectionLoc.ProfilA.Tfs, Enu_TypeVariable.Dimension, 4, 1, NON, True)
        Me.txt_Tw.Text = GetStringInUnitN(MySectionLoc.ProfilA.Tw, Enu_TypeVariable.Dimension, 4, 1, NON, True)

        MAJI_RatioAirePRS()

        '== Plats


    End Sub

    Private Sub AfficherPlatEnCours()
        '---------------------------------------------------------------------------------------------------------
        '   18/11/24 : Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du plat de renfort en cours
        '---------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Me.chk_Plat.Checked = MySectionLoc.ProfilA.lPlat

        Me.txt_EpPlat.Text = GetStringInUnitN(MySectionLoc.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 4, 1, Enu_AfficheUnite.Non, True)
        Me.txt_Wplat.Text = GetStringInUnitN(MySectionLoc.ProfilA.Plat_b, Enu_TypeVariable.Dimension, 4, 1, Enu_AfficheUnite.Non, True)
        MAJI_Plats()

        '==> AffichageOptFeu de la nuance

        Me.cmb_NuancePlat.SelectedIndex = GetIndiceNuancePlat()

    End Sub

    Private Function GetIndiceNuancePlat() As Integer
        '---------------------------------------------------------------------------------------------------------
        '   18/11/24 : Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Fonction qui retourne l'indice de la nuance de plat à afficher dans le combobox
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Indice As Integer = 0

        Dim iAcier As Integer = 0
        Dim lTrouve As Boolean = False
        Dim nbAciers As Integer = AcierPlats.Count

        '--( Traitements

        Do While (Not lTrouve) And iAcier < nbAciers
            iAcier += 1

            lTrouve = (MySectionLoc.AcierPlat.Nuance = AcierPlats(iAcier - 1).Nuance) _
                  And (MySectionLoc.AcierPlat.Qualite = AcierPlats(iAcier - 1).Qualite) _
                  And (MySectionLoc.AcierPlat.Reduction = AcierPlats(iAcier - 1).Reduc)

        Loop

        If lTrouve Then Indice = iAcier - 1

        Return Indice

    End Function

    Private Sub AfficherProfileLamineEnCours()
        '---------------------------------------------------------------------------------------------------------
        '   08/06/23 : Création - POM
        '---------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyGam As String = MySectionLoc.ProfilA.Gamme                ' MySection.Gamme
        Dim MyProf As String = MySectionLoc.ProfilA.NomProfile          ' MySection.Etiquette
        Dim NbProG As Integer

        If MyCatalogue.Series.ContainsKey(MyGam) Then

            Me.lst_GammeS.Text = MyGam
            RemplissageGrilleProfile(Me.Grid_ProfilesSup, MyGam, NbProG)

            'RemplirDelivery(MyGam, MyProf)

            Dim iPro As Integer = 0
            Dim lTrouve As Boolean = False

            Do While iPro < Me.Grid_ProfilesSup.Rows.Count And (Not lTrouve)
                iPro += 1
                lTrouve = (Me.Grid_ProfilesSup(0, iPro - 1).Value.ToString.Trim = MyProf)
            Loop
            If lTrouve Then Me.Grid_ProfilesSup(0, iPro - 1).Selected = True
            iLignePro = Me.Grid_ProfilesSup.SelectedCells(0).RowIndex

            MAJ_Aciers(MyGam, MyProf)

            Dim iSteel As Integer = 0
            lTrouve = False
            Do While iSteel < Me.GridAciers.Rows.Count And Not lTrouve
                iSteel += 1
                lTrouve = (Me.GridAciers(0, iSteel - 1).Value.ToString.Trim = MySectionLoc.Acier.Nuance) _
                      And (Me.GridAciers(1, iSteel - 1).Value.ToString.Trim = MySectionLoc.Acier.Qualite) _
                      And (Me.GridAciers(2, iSteel - 1).Value.ToString.Trim = MySectionLoc.Acier.Reduction)
            Loop
            If lTrouve Then
                Me.GridAciers(0, iSteel - 1).Selected = True
            Else 'on selectionne la derniere ligne par défaut 
                Me.GridAciers(0, Me.GridAciers.Rows.Count - 1).Selected = True
            End If
            GetAcierFromGrid()
        Else

        End If


    End Sub

    ''' <summary>
    ''' Routine pour afficher le tableaux des nuances d'acier dans le cas d'un PRS
    ''' </summary>
    Private Sub AfficherPRSEnCours()
        '---------------------------------------------------------------------------------------------------------
        '   10/01/24 : Création - GUD
        '---------------------------------------------------------------------------------------------------------

        MAJ_Aciers("", "")

        Dim iSteel As Integer = 0
        Dim lTrouve As Boolean = False
        lTrouve = False
        Do While iSteel < Me.GridAciers.Rows.Count And Not lTrouve
            iSteel += 1
            lTrouve = (Me.GridAciers(0, iSteel - 1).Value.ToString.Trim = MySectionLoc.Acier.Nuance) _
                      And (Me.GridAciers(1, iSteel - 1).Value.ToString.Trim = MySectionLoc.Acier.Qualite) _
                      And (Me.GridAciers(2, iSteel - 1).Value.ToString.Trim = MySectionLoc.Acier.Reduction)
        Loop

        If lTrouve Then
            Me.GridAciers(0, iSteel - 1).Selected = True
        Else 'on selectionne la derniere ligne par défaut 
            Me.GridAciers(0, Me.GridAciers.Rows.Count - 1).Selected = True
        End If
        GetAcierFromGrid()

    End Sub

    Private Sub RemplirSeries()

        '--> Remplissage des series

        Me.lst_GammeS.Items.Clear()
        'Me.lst_GammeI.Items.Clear()
        For Each kvp As KeyValuePair(Of String, StrucGamme) In MyCatalogue.Series
            Me.lst_GammeS.Items.Add(kvp.Key)
            'Me.lst_GammeI.Items.Add(kvp.Key)
        Next

    End Sub

    Private Sub AfficheBtnFyFu()

        'If Me.lDessinFy Then
        '    Me.btn_FyFu.Image = imgList_UY.Images("Fy")

        'Else
        '    Me.btn_FyFu.Image = imgList_UY.Images("Fu")
        'End If

        Select Case DrawProperty
            Case EnuDrawProperty.Fy : Me.btn_FyFu.Image = imgList_UY.Images("Fy")
            Case EnuDrawProperty.Fu : Me.btn_FyFu.Image = imgList_UY.Images("Fu")

        End Select

    End Sub

#End Region

#Region " Préparation des grilles "

    Sub PrepareLookGrille(ByVal MyGrille As DataGridView,
                          ByVal ColHiStar As DataGridViewTextBoxColumn,
                          ByVal ColListe As DataGridViewTextBoxColumn,
                          ByVal BackColor As Color, ByVal RatioHI As Single)
        '--------------------------------------------------------------------------
        '
        '   Préparation du "Look" de la grille
        '
        '--------------------------------------------------------------------------
        '
        '   MyGrille    [E] :   Grille à préparer
        '   ColListe
        '   ColHISTAR   [E] :   Colonnes de la grille
        '   BackColor   [E] :   Color de fond à appliquer à la grille
        '   RatioHI     [E] :   Ratio pour la largeur de la colonne HiStar
        '
        '--------------------------------------------------------------------------

        MyGrille.CellBorderStyle = DataGridViewCellBorderStyle.None

        MyGrille.DefaultCellStyle.Padding = New Padding(0)
        MyGrille.Font = New Font(MyGrille.Font.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        MyGrille.BorderStyle = BorderStyle.Fixed3D
        MyGrille.BackgroundColor = BackColor

        ColListe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        ColHiStar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ColHiStar.Width = CInt(MyGrille.Width * RatioHI)
        ColListe.Width = CInt(MyGrille.Width * (1 - RatioHI))

        ColHiStar.DefaultCellStyle.ForeColor = ColorGridHI

    End Sub

    Sub PrepareLookGrille(ByVal MyGrille As DataGridView,
                          ByVal ColGrade As DataGridViewTextBoxColumn,
                          ByVal ColQualite As DataGridViewTextBoxColumn,
                          ByVal ColReduc As DataGridViewTextBoxColumn,
                          ByVal BackColor As Color, ByVal Ratio1 As Single, ByVal Ratio2 As Single)
        '--------------------------------------------------------------------------
        '
        '   Préparation du "Look" de la grille
        '
        '--------------------------------------------------------------------------
        '
        '   MyGrille    [E] :   Grille à préparer
        '   ColGrade
        '   ColQualite
        '   ColReduc    [E] :   Colonnes de la grille
        '   BackColor   [E] :   Color de fond à appliquer à la grille
        '   RatioHI     [E] :   Ratio pour la largeur de la colonne HiStar
        '
        '--------------------------------------------------------------------------

        MyGrille.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical

        MyGrille.DefaultCellStyle.Padding = New Padding(0)
        MyGrille.Font = New Font(MyGrille.Font.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        MyGrille.BorderStyle = BorderStyle.Fixed3D
        MyGrille.BackgroundColor = BackColor

        ColGrade.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        ColQualite.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        ColReduc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ColGrade.Width = CInt(MyGrille.Width * Ratio1)
        ColQualite.Width = CInt(MyGrille.Width * Ratio2)
        ColReduc.Width = CInt(MyGrille.Width * (1 - Ratio1 - Ratio2))

        ColGrade.DefaultCellStyle.ForeColor = ColorGridHI

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If ValideSaisieFenetre() Then

            Dim lModif As Boolean = False

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMSECTION)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean

        Dim lOK As Boolean = True

        Dim Aft, Afb As Decimal
        Dim RapA As Decimal
        Dim Chaine As String
        Dim strValMin As String
        Dim strValMax As String

        If MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym Then

            Aft = MySectionLoc.ProfilA.AireFs
            Afb = MySectionLoc.ProfilA.AireFi

            RapA = Afb / Aft

            If IsSmaller(RapA, OptionsScope.RapportAfMin) Or IsGreater(RapA, OptionsScope.RapportAfMax) Then

                lOK = False
                strValMin = GetStringInUnitN(OptionsScope.RapportAfMin, Enu_TypeVariable.SansType, 4, 3, NON_U, True)
                strValMax = GetStringInUnitN(OptionsScope.RapportAfMax, Enu_TypeVariable.SansType, 4, 3, NON_U, True)

                Chaine = RemplaceDollar(RemplaceDollar(strWarningA, strValMin), strValMax)

                MsgBox(Chaine, MsgBoxStyle.Information, LogicielInfo.Racine)

            End If

        End If

        Return lOK

    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False
        Dim lAcierPlatModifie As Boolean = False

        If MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.typeProfileAcier <> MySectionLoc.ProfilA.typeProfileAcier Then
            MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.typeProfileAcier = MySectionLoc.ProfilA.typeProfileAcier
            lModif = True
        End If

        If MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then

            GereTransfertValeur(MySectionLoc.ProfilA.lPlat, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.lPlat, lModif)
            GereTransfertValeur(MySectionLoc.ProfilA.Plat_b, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Plat_b, lModif)
            GereTransfertValeur(MySectionLoc.ProfilA.Plat_t, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Plat_t, lModif)

            GereTransfertValeur(MySectionLoc.AcierPlat.Nuance, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierPlat.Nuance, lAcierPlatModifie)
            GereTransfertValeur(MySectionLoc.AcierPlat.Qualite, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierPlat.Qualite, lAcierPlatModifie)
            GereTransfertValeur(MySectionLoc.AcierPlat.Reduction, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierPlat.Reduction, lAcierPlatModifie)

            If lAcierPlatModifie Then
                lModif = True

                Dim Nuance, Qualite, Reduction As String

                Nuance = MySectionLoc.AcierPlat.Nuance
                Qualite = MySectionLoc.AcierPlat.Qualite
                Reduction = MySectionLoc.AcierPlat.Reduction

                TransfertPlagesAcier(Nuance, Qualite, Reduction, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierPlat)
            End If

        End If

        GereTransfertValeur(MySectionLoc.ProfilA.ha, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.ha, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Bfs, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Bfs, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Bfi, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Bfi, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Tfs, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Tfs, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Tfi, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Tfi, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Rcs, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Rcs, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Rci, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Rci, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Tw, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Tw, lModif)

        GereTransfertValeur(MySectionLoc.ProfilA.NomProfile, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.NomProfile, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.Gamme, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Gamme, lModif)

        Dim lAcierModifie As Boolean = False

        GereTransfertValeur(MySectionLoc.Acier.Nuance, MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Nuance, lAcierModifie)
        GereTransfertValeur(MySectionLoc.Acier.Qualite, MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Qualite, lAcierModifie)
        GereTransfertValeur(MySectionLoc.Acier.NormeProduit, MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.NormeProduit, lAcierModifie)
        GereTransfertValeur(MySectionLoc.Acier.Reduction, MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Reduction, lAcierModifie)

        If lAcierModifie Then
            lModif = True

            'MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Plages.Clear()
            'Dim MyPlage As cls_Acier.strucPlage
            Dim Nuance, Qualite, Reduction As String

            Nuance = MySectionLoc.Acier.Nuance
            Qualite = MySectionLoc.Acier.Qualite
            Reduction = MySectionLoc.Acier.Reduction

            'For i As Integer = 0 To SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages.Count - 1
            '    MyPlage.Ep = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Ep
            '    MyPlage.Fy = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fy
            '    MyPlage.Fu = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fu
            '    MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Plages.Add(MyPlage)
            'Next

            TransfertPlagesAcier(Nuance, Qualite, Reduction, MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier)
        End If

        With MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA ' --> Sécurité supplémentaire pour s'assurer que les valeurs qui n'ont pas de sens restent égales à 0
            Select Case .typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    .hb = .ha
                    .aW = 0
                    '      .Plat_b = 0
              '      .Plat_t = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                    .hb = 0
                    .Rcs = 0
                    .Rci = 0
                    .Plat_b = 0
                    .Plat_t = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    .hb = .ha
                    .aW = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    .Bfi = 0
                    .Tfi = 0
                    .Rci = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    .Bfs = 0
                    .Tfs = 0
                    .Rcs = 0
            End Select

        End With

    End Sub

    Private Sub TransfertPlagesAcier(Nuance As String, Qualite As String, Reduction As String, ByRef mySteel As cls_Acier)
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/11/24 :  Création - POM 
        '------------------------------------------------------------------------------------------------------------------------------
        '   Transfert des plages epaisseurs/fu/fu pour un acier selectionné
        '------------------------------------------------------------------------------------------------------------------------------
        '   Nuance      [E] :   Nuance de l'acier sélectionné
        '   Qualite     [E] :   Qualité de l'acier sélectionné
        '   Reduction   [E] :   Courbe de réduction de l'acier sélectionné
        '   mySteel     [S] :   Acier à définir (classe)
        '------------------------------------------------------------------------------------------------------------------------------

        Dim MyPlage As cls_Acier.strucPlage

        mySteel.Plages.Clear()

        For i As Integer = 0 To SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages.Count - 1
            MyPlage.Ep = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Ep
            MyPlage.Fy = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fy
            MyPlage.Fu = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fu
            mySteel.Plages.Add(MyPlage)
        Next

    End Sub

#End Region

#Region " DESSINS "

    Private Sub img_Section_Paint(sender As Object, e As PaintEventArgs) Handles img_Section.Paint

        DessinProfileAcierN(e.Graphics, MySectionLoc, Me.img_Section.ClientRectangle.Width, Me.img_Section.ClientRectangle.Height,
                            FontFrm, kAdjust, True, False, iSelect)
        'DessinProfileAcier(e.Graphics, MySectionLoc, Me.img_Section.ClientRectangle.Width, Me.img_Section.ClientRectangle.Height,
        '                    FontBase, kAdjust, True, False, iSelect)

    End Sub

    Private Sub img_ReductionCurve_Paint(sender As Object, e As PaintEventArgs) Handles img_ReductionCurve.Paint

        DessinPropAcier(e.Graphics, Me.img_ReductionCurve.ClientRectangle.Height, Me.img_ReductionCurve.ClientRectangle.Width, True,
                        DrawProperty = EnuDrawProperty.Fy)

    End Sub

    Sub DessinPropAcier(ByVal MyGr As Graphics, ByVal sHI As Single, ByVal sWI As Single, ByVal lNuanceOK As Boolean, lFy As Boolean)
        '----------------------------------------------------------------------------------------------
        '
        '   23/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   AffichageOptFeu graphique de la section dans la fenêtre
        '
        '----------------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   sHI, sWI        [E] :   Dimensions du PictureBox
        '   lNuanceOK       [E] :   Indique si l'utilisateur en mode normal peut sélectionner cette nuance
        '   lFy             [E] :   Indique si affichage fy ou fu
        '
        '----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim RCParAff As Struc_Affichage
        Dim xMin, xMax, yMin, yMax As Double
        Dim Nuance, Norme, Qualite As String
        Dim iSteel As Integer
        Dim EpMin, EpMax, VMax As Double
        Dim kFact As Double

        Dim EpProfile, FyPro As Double
        Dim EpPRSfs, EpPRSw, EpPRSfi, EpPRSMax, FyPRSfs, FyPRSw, FyPRSfi, FyPRSMin As Double

        Dim ColorPen As Color = Color.Black
        Dim ColorExclu As Color = ColorNotPossible
        Dim ColorNormal As Color
        Dim ColorSelect As Color

        Dim MyPen As Pen 'New Pen(ColorPen)
        Dim MyBrush As Brush 'New SolidBrush(ColorPen)
        Dim myFont As New Font(FontBase.Name, 8, FontStyle.Bold)
        Dim MyBrushTitre As Brush   'New SolidBrush(Color.DarkRed)

        Dim zBoni, xBoni As Double
        Dim EpPlagesMax As Double

        '--( Initialisation couleurs

        If lNuanceOK Then
            MyPen = New Pen(ColorPen)
            MyBrush = New SolidBrush(ColorPen)
            ColorNormal = Color.DarkGray
            ColorSelect = Color.DarkOrange
            MyBrushTitre = New SolidBrush(Color.DarkRed)
        Else
            MyPen = New Pen(ColorExclu)
            MyBrush = New SolidBrush(ColorExclu)
            ColorNormal = ColorExclu
            ColorSelect = ColorExclu
            MyBrushTitre = New SolidBrush(ColorExclu)
        End If

        '--( Epaisseur du profilé pour le calcul

        If MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
            EpProfile = Math.Max(MySectionLoc.ProfilA.Tw, MySectionLoc.ProfilA.Tfs)
            If lFy Then
                FyPro = MySectionLoc.Acier.LimiteFy(EpProfile)
            Else
                FyPro = MySectionLoc.Acier.LimiteFu(EpProfile)
            End If
        Else
            EpPRSfs = MySectionLoc.ProfilA.Tfs
            EpPRSw = MySectionLoc.ProfilA.Tw
            EpPRSfi = MySectionLoc.ProfilA.Tfi
            EpPRSMax = Math.Max(EpPRSfs, Math.Max(EpPRSfi, EpPRSw))

            If lFy Then
                FyPRSfs = MySectionLoc.Acier.LimiteFy(EpPRSfs)
                FyPRSw = MySectionLoc.Acier.LimiteFy(EpPRSw)
                FyPRSfi = MySectionLoc.Acier.LimiteFy(EpPRSfi)
                FyPRSMin = MySectionLoc.Acier.LimiteFy(EpPRSMax)
            Else
                FyPRSfs = MySectionLoc.Acier.LimiteFu(EpPRSfs)
                FyPRSw = MySectionLoc.Acier.LimiteFu(EpPRSw)
                FyPRSfi = MySectionLoc.Acier.LimiteFu(EpPRSfi)
                FyPRSMin = MySectionLoc.Acier.LimiteFu(EpPRSMax)
            End If

        End If

        EpPlagesMax = MySectionLoc.Acier.EpMax

        '--( Initialisation

        Const kEch As Decimal = 0.92

        If Me.GridAciers.Rows.Count <= 0 Then Exit Sub

        iSteel = Me.GridAciers.SelectedCells(0).RowIndex
        Nuance = Me.GridAciers(0, iSteel).Value.ToString
        Qualite = Me.GridAciers(1, iSteel).Value.ToString
        Norme = Me.GridAciers(2, iSteel).Value.ToString

        ExtraitValeursEnveloppeAciers(Nuance, DrawProperty, EpMin, EpMax, VMax)

        If MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
            EpMax = Math.Max(EpMax, EpProfile)
        Else
            EpMax = Math.Max(EpMax, EpPRSMax)
        End If

        kFact = EpMax / VMax * sHI / sWI
        xMin = 0
        xMax = EpMax
        yMin = 0
        yMax = VMax * kFact

        ParametresAffichage(RCParAff, xMin, yMin, xMax - xMin, yMax - yMin, sWI, sHI)

        Dim ChaineFy As String = ""
        Dim hBoni As Single = MyGr.MeasureString(ChaineFy, myFont).Height
        Dim ChaineT As String = "t (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")"
        Dim wBoni As Single = MyGr.MeasureString(ChaineT, myFont).Width

        If lFy Then ChaineFy = "fy (MPa)" Else ChaineFy = "fu (MPa)"

        yMax += hBoni / RCParAff.CRed
        xMax += wBoni / RCParAff.CRed
        yMin -= 2 * hBoni / RCParAff.CRed
        'xMin -= wBoni / RCParAff.CRed

        ParametresAffichage(RCParAff, xMin, yMin, xMax - xMin, yMax - yMin, sWI, sHI)

        '--( Dessin des axes

        AddFleche(MyGr, MyPen, 0, 0, xMax, 0, RCParAff, False, True)
        AddFleche(MyGr, MyPen, 0, 0, 0, yMax, RCParAff, False, True)

        AddTexte(MyGr, MyBrush, ChaineFy, myFont, 0, yMax, RCParAff, HorizontalAlignment.Left, VerticalAlignement.Top)
        AddTexte(MyGr, MyBrush, ChaineT, myFont, xMax, 0, RCParAff, HorizontalAlignment.Right, VerticalAlignement.Bottom)

        '--( Représentation des courbes

        Dim iEp As Integer
        Const iEPNORMAL As Integer = 1
        Const iEPSELECT As Integer = 2

        Dim MyColor As Color
        Dim lSelect As Boolean

        For Each kvpQualite As KeyValuePair(Of String, strucQualite) In SteelBase.Grades(Nuance).Qualites

            For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv

                If Qualite = kvpQualite.Key And Norme = kvpSteel.Key Then
                    MyColor = ColorSelect
                    iEp = iEPSELECT
                    lSelect = True
                Else
                    MyColor = ColorNormal
                    iEp = iEPNORMAL
                    lSelect = False
                End If

                If lSelect Then
                    DrawReductionCurve(MyGr, RCParAff, myFont, kEch * kFact, lSelect,
                                       kvpSteel.Value.EpMax, kvpSteel.Value.Plages, MyColor, iEp, lNuanceOK, lFy)
                End If
            Next

        Next

        '--( Représentation de la position du profilé dans la courbe de réduction

        zBoni = YUnivers(RCParAff, sHI)
        xBoni = XUnivers(RCParAff, sWI / 2)

        If MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
            DrawEpEtFyCalcul(MyGr, RCParAff, kEch * kFact, EpPlagesMax, EpProfile, FyPro, xBoni, zBoni, myFont, lNuanceOK, lFy)
        Else
            DrawEpEtFyCalcul(MyGr, RCParAff, kEch * kFact, EpPlagesMax, EpPRSfs, FyPRSfs, xBoni, zBoni, myFont, lNuanceOK, lFy, False)
            DrawEpEtFyCalcul(MyGr, RCParAff, kEch * kFact, EpPlagesMax, EpPRSw, FyPRSw, xBoni, zBoni, myFont, lNuanceOK, lFy, False)
            DrawEpEtFyCalcul(MyGr, RCParAff, kEch * kFact, EpPlagesMax, EpPRSfi, FyPRSfi, xBoni, zBoni, myFont, lNuanceOK, lFy, False)
            DrawEpEtFyCalcul(MyGr, RCParAff, kEch * kFact, EpPlagesMax, EpPRSMax, FyPRSMin, xBoni, zBoni, myFont, lNuanceOK, lFy, True, True)
        End If

        '--( Titre

        Dim Chaine As String

        Chaine = Nuance & " - " & Qualite
        Dim wC, hC As Single
        Dim MyFontTitre As New Font(FontBase.Name, 8, FontStyle.Bold)

        wC = MyGr.MeasureString(Chaine, MyFontTitre).Width
        hC = MyGr.MeasureString(Chaine, MyFontTitre).Height

        MyGr.DrawString(Chaine, MyFontTitre, MyBrushTitre, sWI / 2 - wC / 2, 1 / 2 * hC)

        '--( Gestion du message d'avertissement pour les nuances non autorisées

        If Not lNuanceOK Then
            '     DrawWarningNuance(MyGr, sHI, sWI)
        End If

        '--( Libérer la mémoire

        myFont.Dispose()
        MyPen.Dispose()
        MyBrush.Dispose()
        MyFontTitre.Dispose()
        MyBrushTitre.Dispose()

    End Sub

    Private Sub DrawReductionCurve(ByVal MyGr As Graphics, ByVal RcParAff As Struc_Affichage, myFont As Font,
                                   ByVal kFact As Double, ByVal lSelect As Boolean,
                                   ByVal EpMax As Double, ByVal Plages As List(Of cls_Acier.strucPlage),
                                   ByVal MyColor As Color, ByVal iEp As Integer, ByVal lNuanceOK As Boolean,
                                   lDessineFy As Boolean)
        '----------------------------------------------------------------------------------------------
        '
        '   21/09/12 :  Création - Version 3.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   AffichageOptFeu de l'épaisseur max et de fy calcul
        '
        '----------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   RcParAff    [E] :   Paramètre d'affichage
        '   myFont      [E] :   Police utilisée pour affichage
        '   kFact       [E] :   Facteur d'affichage des valeurs fy
        '   lSelect     [E] :   
        '   EpMax,Plages[E] :   Paramètres décrivant la fonction fy-t
        '   MyColor     [E] :   Couleur d'affichage
        '   iEp         [E] :   Epaisseur du trait
        '   lNuanceOK   [E] :   ?
        '   lDessineFy  [E] :   Indique si on dessine Fy ou Fu
        '----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbPlages As Integer
        Dim Chaine As String

        Dim MyPen As New Pen(MyColor, iEp)
        Dim MyPenBlack As Pen 'New Pen(Color.Black, 0.75)
        Dim MyColorBlack As Color
        Dim TabVal As New List(Of Decimal)
        Dim i As Integer

        '--> Initialisation

        NbPlages = Plages.Count
        If lNuanceOK Then
            MyPenBlack = New Pen(Color.Black)
            MyColorBlack = Color.Black
        Else
            MyPenBlack = New Pen(ColorNotPossible)
            MyColorBlack = ColorNotPossible
        End If

        '--> Tableau des valeurs

        For i = 0 To NbPlages - 1
            If lDessineFy Then
                TabVal.Add(Plages(i).Fy)
            Else
                TabVal.Add(Plages(i).Fu)
            End If
        Next

        '--> Traitement

        If lSelect Then

            For i = 0 To NbPlages - 2

                AddLigne(MyGr, MyPen, Plages(i).Ep, kFact * TabVal(i), Plages(i + 1).Ep, kFact * TabVal(i), RcParAff)
                AddLigne(MyGr, MyPenBlack, Plages(i + 1).Ep, kFact * TabVal(i), Plages(i + 1).Ep, kFact * TabVal(i + 1), RcParAff)

            Next
            AddLigne(MyGr, MyPen, Plages(NbPlages - 1).Ep, kFact * TabVal(NbPlages - 1), EpMax, kFact * TabVal(NbPlages - 1), RcParAff)


            For i = 0 To NbPlages - 1

                AddLigne(MyGr, MyPenBlack, Plages(i).Ep, 0, Plages(i).Ep, kFact * TabVal(i), RcParAff)

            Next
            AddLigne(MyGr, MyPenBlack, EpMax, 0, EpMax, kFact * TabVal(NbPlages - 1), RcParAff)

        End If

        '--> Cotation

        If lSelect Then
            For i = 0 To NbPlages - 2
                'Chaine = GetStringInUnit(Plages(i).Fy, Enu_TypeVariable.Contrainte, 3, 0, False)
                Chaine = GetStringInUnit(TabVal(i), Enu_TypeVariable.SansType, 3, 0, False)
                AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, (Plages(i).Ep + Plages(i + 1).Ep) / 2, kFact * TabVal(i), RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            Next
            Chaine = GetStringInUnit(TabVal(NbPlages - 1), Enu_TypeVariable.SansType, 3, 0, False)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, (EpMax + Plages(NbPlages - 1).Ep) / 2, kFact * TabVal(NbPlages - 1), RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            For i = 0 To NbPlages - 1
                Chaine = GetStringInUnit(Plages(i).Ep, Enu_TypeVariable.Dimension, 3, 0, False)
                AddTexte(MyGr, New SolidBrush(MyColorBlack), Chaine, myFont, Plages(i).Ep, 0, RcParAff, HorizontalAlignment.Right, VerticalAlignement.Top)
            Next
            Chaine = GetStringInUnit(EpMax, Enu_TypeVariable.Dimension, 3, 0, False)
            AddTexte(MyGr, New SolidBrush(MyColorBlack), Chaine, myFont, EpMax, 0, RcParAff, HorizontalAlignment.Right, VerticalAlignement.Top)
        End If

    End Sub

    'Private Sub DrawReductionCurveOLD(ByVal MyGr As Graphics, ByVal RcParAff As Struc_Affichage, myFont As Font,
    '                               ByVal kFact As Double, ByVal lSelect As Boolean,
    '                               ByVal EpMax As Double, ByVal Plages As List(Of cls_Acier.strucPlage),
    '                               ByVal MyColor As Color, ByVal iEp As Integer, ByVal lNuanceOK As Boolean,
    '                               lDessineFy As Boolean)
    '    '----------------------------------------------------------------------------------------------
    '    '
    '    '   21/09/12 :  Création - Version 3.00
    '    '
    '    '----------------------------------------------------------------------------------------------
    '    '
    '    '   AffichageOptFeu de l'épaisseur max et de fy calcul
    '    '
    '    '----------------------------------------------------------------------------------------------
    '    '
    '    '   MyGr        [E] :   Graphics dans lequel on dessine
    '    '   RcParAff    [E] :   Paramètre d'affichage
    '    '   myFont      [E] :   Police utilisée pour affichage
    '    '   kFact       [E] :   Facteur d'affichage des valeurs fy
    '    '   lSelect     [E] :   
    '    '   EpMax,Plages[E] :   Paramètres décrivant la fonction fy-t
    '    '   MyColor     [E] :   Couleur d'affichage
    '    '   iEp         [E] :   Epaisseur du trait
    '    '   lNuanceOK   [E] :   ?
    '    '   lDessineFy  [E] :   Indique si on dessine Fy ou Fu
    '    '----------------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim NbPlages As Integer
    '    Dim Chaine As String

    '    Dim MyPen As New Pen(MyColor, iEp)
    '    Dim MyPenBlack As Pen 'New Pen(Color.Black, 0.75)
    '    Dim MyColorBlack As Color
    '    Dim TabVal As New List(Of Decimal)
    '    Dim i As Integer

    '    '--> Initialisation

    '    NbPlages = Plages.Count
    '    If lNuanceOK Then
    '        MyPenBlack = New Pen(Color.Black)
    '        MyColorBlack = Color.Black
    '    Else
    '        MyPenBlack = New Pen(ColorNotPossible)
    '        MyColorBlack = ColorNotPossible
    '    End If

    '    '--> Tableau des valeurs

    '    For i = 0 To NbPlages - 1

    '    Next

    '    '--> Traitement

    '    If lSelect Then

    '        For i = 0 To NbPlages - 2

    '            AddLigne(MyGr, MyPen, Plages(i).Ep, kFact * Plages(i).Fy, Plages(i + 1).Ep, kFact * Plages(i).Fy, RcParAff)
    '            AddLigne(MyGr, MyPenBlack, Plages(i + 1).Ep, kFact * Plages(i).Fy, Plages(i + 1).Ep, kFact * Plages(i + 1).Fy, RcParAff)

    '        Next
    '        AddLigne(MyGr, MyPen, Plages(NbPlages - 1).Ep, kFact * Plages(NbPlages - 1).Fy, EpMax, kFact * Plages(NbPlages - 1).Fy, RcParAff)


    '        For i = 0 To NbPlages - 1

    '            AddLigne(MyGr, MyPenBlack, Plages(i).Ep, 0, Plages(i).Ep, kFact * Plages(i).Fy, RcParAff)

    '        Next
    '        AddLigne(MyGr, MyPenBlack, EpMax, 0, EpMax, kFact * Plages(NbPlages - 1).Fy, RcParAff)

    '    End If

    '    '--> Cotation

    '    If lSelect Then
    '        For i = 0 To NbPlages - 2
    '            'Chaine = GetStringInUnit(Plages(i).Fy, Enu_TypeVariable.Contrainte, 3, 0, False)
    '            Chaine = GetStringInUnit(Plages(i).Fy, Enu_TypeVariable.SansType, 3, 0, False)
    '            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, (Plages(i).Ep + Plages(i + 1).Ep) / 2, kFact * Plages(i).Fy, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
    '        Next
    '        Chaine = GetStringInUnit(Plages(NbPlages - 1).Fy, Enu_TypeVariable.SansType, 3, 0, False)
    '        AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, (EpMax + Plages(NbPlages - 1).Ep) / 2, kFact * Plages(NbPlages - 1).Fy, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
    '        For i = 0 To NbPlages - 1
    '            Chaine = GetStringInUnit(Plages(i).Ep, Enu_TypeVariable.Dimension, 3, 0, False)
    '            AddTexte(MyGr, New SolidBrush(MyColorBlack), Chaine, myFont, Plages(i).Ep, 0, RcParAff, HorizontalAlignment.Right, VerticalAlignement.Top)
    '        Next
    '        Chaine = GetStringInUnit(EpMax, Enu_TypeVariable.Dimension, 3, 0, False)
    '        AddTexte(MyGr, New SolidBrush(MyColorBlack), Chaine, myFont, EpMax, 0, RcParAff, HorizontalAlignment.Right, VerticalAlignement.Top)
    '    End If

    'End Sub

    Private Sub DrawEpEtFyCalcul(ByVal MyGr As Graphics, ByVal RcParAff As Struc_Affichage, ByVal kFact As Double,
                                 ByVal EpPlagesMax As Double, ByVal EpProf As Double, ByVal FyCalcul As Double,
                                 ByVal xBoni As Double, ByVal zBoni As Double,
                                 ByVal myFont As Font, ByVal lNuanceOK As Boolean, lFy As Boolean,
                                 Optional ByVal lLegende As Boolean = True, Optional ByVal lPRS As Boolean = False)
        '----------------------------------------------------------------------------------------------
        '
        '   21/09/12 :  Création - Version 3.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   AffichageOptFeu de l'épaisseur max et de fy calcul
        '
        '----------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   RcParAff    [E] :   Paramètre d'affichage
        '   kFact       [E] :   Facteur d'affichage des valeurs fy
        '   
        '   EpPlagesMax [E] :   Epaisseur maximale des plages de la courbe de réduction'
        '   EpProf      [E] :   Epaisseur de profilé pris en compte pour les calcul
        '   FyCalcul    [E] :   Valeur de Fy (ou Fu) pour le calcul
        '   
        '   xBoni, zBoni[E] :   Position pour l'affichage du texte sur les valeurs de calcul
        '   MyFont      [E] :   Police d'affichage
        '   lNuanceOK   [E) :   Indique si la nuance d'acier est accessible à l'utilisateur
        '
        '----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Chaine As String
        Dim MyColor As Color '= Color.DarkOrchid
        Dim MyPen As Pen    'New Pen(MyColor, 2)
        Dim MyPenProf As Pen    'New Pen(MyColor, 2)
        Dim SymbIndex As String = "u"

        '--> Initialisation

        If lNuanceOK Then
            MyColor = Color.DarkOrchid
        Else
            MyColor = ColorNotPossible
        End If
        MyPen = New Pen(MyColor, 2)
        MyPenProf = New Pen(MyColor, 2)
        If EpProf > EpPlagesMax * (1 + EPSILONG) Then
            MyPenProf.DashStyle = Drawing2D.DashStyle.Dash
        End If
        If lFy Then SymbIndex = "y"

        '--> Traitement

        If lLegende Then
            Chaine = GetStringInUnit(EpProf, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, EpProf, 0, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
        End If

        AddLigne(MyGr, MyPenProf, EpProf, 0, EpProf, kFact * FyCalcul, RcParAff)
        If EpProf > EpPlagesMax * (1 + EPSILONG) Then
            AddLigne(MyGr, MyPenProf, EpPlagesMax, kFact * FyCalcul, EpProf, kFact * FyCalcul, RcParAff)
        End If

        If lLegende Then
            If lPRS Then
                Chaine = "tmax = " & GetStringInUnit(EpProf, Enu_TypeVariable.Dimension, 3, 1, True) & "   -  f" & SymbIndex & ",min = " & GetStringInUnit(FyCalcul, Enu_TypeVariable.SansType, 3, 0, False) & " MPa"
            Else
                Chaine = "t = " & GetStringInUnit(EpProf, Enu_TypeVariable.Dimension, 3, 1, True) & "   -  f" & SymbIndex & " = " & GetStringInUnit(FyCalcul, Enu_TypeVariable.SansType, 3, 0, False) & " MPa"
            End If
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, xBoni, zBoni, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
        End If

        MyPen.Dispose()
        MyPenProf.Dispose()

    End Sub


#End Region

#Region " Dessin symboles "

    Private Sub img_Symbol_Paint(sender As Object, e As PaintEventArgs) Handles img_Tw.Paint, img_Tfs.Paint, img_Tfi.Paint, img_Hw.Paint, img_Ht.Paint, img_Bfs.Paint, img_Bfi.Paint, img_Wplat.Paint, img_Tplat.Paint, img_Aft.Paint, img_Afb.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15
        Dim lEgal As Boolean

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True

        Select Case sender.name

            Case img_Afb.Name
                strSymbol = "A"
                strIndice = "fb"
                lEgal = False
            Case img_Aft.Name
                strSymbol = "/ A"
                strIndice = "ft"

            Case Me.img_Bfi.Name
                strSymbol = "b"
                strIndice = "i"
            Case Me.img_Bfs.Name
                strSymbol = "b"
                strIndice = "s"
            Case Me.img_Ht.Name
                strSymbol = "h"
                strIndice = "a"
            Case Me.img_Hw.Name
                strSymbol = "h"
                strIndice = "w"
            Case Me.img_Tfi.Name
                strSymbol = "t"
                strIndice = "fi"
            Case Me.img_Tfs.Name
                strSymbol = "t"
                strIndice = "fs"
            Case Me.img_Tw.Name
                strSymbol = "t"
                strIndice = "w"
            Case Me.img_Tplat.Name
                strSymbol = "t"
                strIndice = "p"
            Case Me.img_Wplat.Name
                strSymbol = "w"
                strIndice = "p"
        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolNormal, FontSymbolIndice, 1.0!, legal)

    End Sub

#End Region

#Region " Evènements "

    Private Sub SaisieDimensionsPlat(sender As Object, e As EventArgs) Handles txt_Wplat.TextChanged, txt_EpPlat.TextChanged

        If lBuild Then Exit Sub
        Dim Valeur As Decimal

        If VerificationSaisiePlat(sender, Valeur) Then
            Select Case sender.name
                Case Me.txt_EpPlat.Name
                    MySectionLoc.ProfilA.Plat_t = Valeur
                Case Me.txt_Wplat.Name
                    MySectionLoc.ProfilA.Plat_b = Valeur
            End Select
            Me.img_Section.Invalidate()
        End If

    End Sub

    Private Sub SaisieDimensions(sender As Object, e As EventArgs) _
        Handles txt_Tw.TextChanged, txt_Tfs.TextChanged, txt_Tfi.TextChanged, txt_Hw.TextChanged,
                txt_Ha.TextChanged, txt_Bfs.TextChanged, txt_Bfi.TextChanged
        If lBuild Then Exit Sub
        lBuild = True

        Dim Valeur As Decimal
        Dim Hcomp As Decimal
        Dim lSym As Boolean = (MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym)
        Dim lMAJHauteur As Boolean = False
        Dim lMAJRatioAf As Boolean = False
        Dim lPRSMonoS As Boolean = (MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym)

        If VerificationDonneesPRS(sender, Valeur) Then
            Select Case sender.name
                Case Me.txt_Ha.Name
                    lMAJHauteur = True
                    MySectionLoc.ProfilA.ha = Valeur
                    'Hcomp = Valeur - MySectionLoc.ProfilA.Tfi - MySectionLoc.ProfilA.Tfs
                    'Me.txt_Hw.Text = GetStringNoUnit(Hcomp, Enu_TypeVariable.Dimension)
                Case Me.txt_Hw.Name
                    lMAJHauteur = True
                    myHauteurHw = Valeur
                    'Hcomp = Valeur + MySectionLoc.ProfilA.Tfi + MySectionLoc.ProfilA.Tfs
                    'Me.txt_Ha.Text = GetStringNoUnit(Hcomp, Enu_TypeVariable.Dimension)
                    'MySectionLoc.ProfilA.ha = Hcomp
                    lMAJHauteur = True
                Case Me.txt_Tw.Name
                    MySectionLoc.ProfilA.Tw = Valeur
                Case Me.txt_Bfs.Name
                    MySectionLoc.ProfilA.Bfs = Valeur
                    lMAJRatioAf = lPRSMonoS
                    If lSym Then
                        MySectionLoc.ProfilA.Bfi = Valeur
                        Me.txt_Bfi.Text = Me.txt_Bfs.Text
                    End If
                Case Me.txt_Tfs.Name
                    MySectionLoc.ProfilA.Tfs = Valeur
                    If lSym Then
                        MySectionLoc.ProfilA.Tfi = Valeur
                        Me.txt_Tfi.Text = Me.txt_Tfs.Text
                    End If
                    lMAJHauteur = True
                    lMAJRatioAf = lPRSMonoS
                Case Me.txt_Bfi.Name
                    MySectionLoc.ProfilA.Bfi = Valeur
                    lMAJRatioAf = lPRSMonoS
                Case Me.txt_Tfi.Name
                    MySectionLoc.ProfilA.Tfi = Valeur
                    lMAJHauteur = True
                    lMAJRatioAf = lPRSMonoS
            End Select
        End If

        If lMAJHauteur Then
            Select Case DefinitionHauteur
                Case Enu_DefinitionH.HauteurAme
                    Hcomp = myHauteurHw + MySectionLoc.ProfilA.Tfi + MySectionLoc.ProfilA.Tfs
                    Me.txt_Ha.Text = GetStringNoUnit(Hcomp, Enu_TypeVariable.Dimension)
                    MySectionLoc.ProfilA.ha = Hcomp
                Case Enu_DefinitionH.HauteurTotale
                    Hcomp = MySectionLoc.ProfilA.ha - MySectionLoc.ProfilA.Tfi - MySectionLoc.ProfilA.Tfs
                    Me.txt_Hw.Text = GetStringNoUnit(Hcomp, Enu_TypeVariable.Dimension)
            End Select
        End If

        If lMAJRatioAf Then MAJI_RatioAirePRS()

        Me.img_Section.Invalidate()
        Me.img_ReductionCurve.Invalidate()

        lBuild = False
    End Sub

    Private Function VerificationSaisiePlat(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean
        '-------------------------------------------------------------------------------------
        '   02/11/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Vérifie la validité de la saisie pour les dimensions d'un plat
        '-------------------------------------------------------------------------------------
        '   myTxt       [E] :   
        '   ValeurUI    [S] :   Valeur saisie dans les unités internes
        '-------------------------------------------------------------------------------------

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
        Dim lOK As Boolean = True

        Const TMINI As Decimal = 0.005
        Const TMAXI As Decimal = 0.05
        Const BFMINI As Decimal = 0.1
        Const BFMAXI As Decimal = 0.5


        Select Case MyTxt.Name
            Case Me.txt_EpPlat.Name
                ValMin = TMINI
                ValMax = TMAXI
            Case Me.txt_Wplat.Name
                ValMin = BFMINI
                ValMax = BFMAXI
        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin / kUnit, lValMax, ValMax / kUnit)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin / kUnit, lValMin, ValMax / kUnit, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOK = (iErreur = 0)
        Return lOK


    End Function

    ''' <summary>
    ''' Vérifie la saisie des données en cours, pour les dimensions des PRS
    ''' </summary>
    Private Function VerificationDonneesPRS(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        'Const HWMINI As Decimal = 0.2
        'Const TFMINI As Decimal = 0.006
        'Const TWMINI As Decimal = 0.003
        'Const HWMAXI As Decimal = 2
        'Const BFMINI As Decimal = 0.12
        'Const BFMAXI As Decimal = 0.5
        'Const EPMAXI As Decimal = 0.5

        '--> Déclaration
        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name
            Case Me.txt_Ha.Name
                'ValMin = HWMINI + 2 * TFMINI
                'ValMax = HWMAXI + 2 * TFMINI
                ValMin = OptionsScope.HwMin + 2 * OptionsScope.TfMin
                ValMax = OptionsScope.HwMax + 2 * OptionsScope.TfMin

            Case Me.txt_Hw.Name
                'ValMin = HWMINI
                'ValMax = HWMAXI
                ValMin = OptionsScope.HwMin
                ValMax = OptionsScope.HwMax

            Case Me.txt_Bfi.Name, Me.txt_Bfs.Name
                'ValMin = BFMINI
                'ValMax = BFMAXI

                ValMin = OptionsScope.BfMin
                ValMax = OptionsScope.BfMax

            Case Me.txt_Tfi.Name, Me.txt_Tfs.Name
                'ValMin = TFMINI
                'ValMax = EPMAXI
                ValMin = OptionsScope.TfMin
                ValMax = OptionsScope.TfMax


            Case Me.txt_Tw.Name
                'ValMin = TWMINI
                'ValMax = EPMAXI
                ValMin = OptionsScope.TwMin
                ValMax = OptionsScope.TwMax

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin / kUnit, lValMax, ValMax / kUnit)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin / kUnit, lValMin, ValMax / kUnit, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

    Private Sub ChoixTypeProfile(sender As Object, e As EventArgs) Handles rdb_PRS_symetrique.CheckedChanged, rdb_PRS.CheckedChanged, rdb_Lamine.CheckedChanged
        If lBuild Then Exit Sub

        If sender.checked = False Then Exit Sub 'Permet d'éviter une boucle infinie

        Select Case sender.name
            Case Me.rdb_Lamine.Name
                MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            Case Me.rdb_PRS.Name
                MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
            Case Me.rdb_PRS_symetrique.Name
                MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
        End Select

        MAJ_FonctionType()
        AfficherPoutreEnCours()
        Me.img_Section.Invalidate()
        Me.img_ReductionCurve.Invalidate()


    End Sub

    Private Sub MAJ_FonctionType()

        Select Case MySectionLoc.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                Me.TLpan_Gauche.RowStyles(3).Height = 0
                Me.TLpan_Gauche.RowStyles(2).Height = 290
                Me.TLpan_Gauche.RowStyles(4).Height = 0
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                Me.TLpan_Gauche.RowStyles(2).Height = 0
                Me.TLpan_Gauche.RowStyles(3).Height = 200
                Me.TLpan_Gauche.RowStyles(4).Height = 0
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                Me.TLpan_Gauche.RowStyles(2).Height = 0
                Me.TLpan_Gauche.RowStyles(3).Height = 200
                Me.TLpan_Gauche.RowStyles(4).Height = 30
        End Select

    End Sub

    Private Sub lst_GammeS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lst_GammeS.SelectedIndexChanged

        '--> Déclaration

        Dim NbProG As Integer
        Dim Gamme, Profile As String
        'Dim Nuance, Norme, Qualite As String

        '--> Traitement 

        If Not lBuild Then

            RemplissageGrilleProfile(Me.Grid_ProfilesSup, Me.lst_GammeS.Text, NbProG)

            lAvailPro = (NbProG > 0)

            MAJ_AvailablePro(False)

            If lAvailPro Then

                Gamme = Me.lst_GammeS.Text
                Profile = NettoieNomProfil(Me.Grid_ProfilesSup(0, 0).Value.ToString)        '==R16-007
                TransfertSaisieGridProfile(Gamme, Profile, MySectionLoc)

                lBuild = True
                AfficherPoutreEnCours()
                lBuild = False
                'MAJ_Aciers(Gamme, Profile)
                'SelectDefaultSteel(True)
                'GetAcierFromGrid()
                'MAJNuancesPossibles()
                '==R16-012
                'RemplirDelivery(Gamme, Profile)

            End If


        End If
        Me.img_Section.Invalidate()

        'MAJ_DonneesFinales()

    End Sub

    Private Function NettoieNomProfil(ByVal Profile As String) As String
        '----------------------------------------------------------------------
        '
        '   On supprime le caractère * devant le nom profilé le cas échéant
        '   Ce caractère est utilisé pour repérer les profilés non autorisés pour le logiciel (en mode expert)
        '   11/07/2016 - R16-007
        '
        '----------------------------------------------------------------------

        '--> Déclaration

        Dim FirstChar As String

        '--> Traitement

        FirstChar = Profile.Substring(0, 1)

        If FirstChar = STARCHAR Then
            Return Profile.Substring(1)
        Else
            Return Profile
        End If
    End Function

    Private Sub chk_DefinitionH_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Hw.CheckedChanged, chk_Ht.CheckedChanged

        If lBuild Then Exit Sub

        lBuild = True
        Select Case sender.name
            Case Me.chk_Ht.Name
                Me.chk_Hw.Checked = False
                DefinitionHauteur = Enu_DefinitionH.HauteurTotale
            Case Me.chk_Hw.Name
                Me.chk_Ht.Checked = False
                DefinitionHauteur = Enu_DefinitionH.HauteurAme
        End Select
        lBuild = False

        MAJ_DefinitionHauteur()

    End Sub

    Private Sub MAJ_DefinitionHauteur()

        'Select Case DefinitionHauteur
        '    Case Enu_DefinitionH.HauteurTotale
        '        Me.txt_Ha.BackColor = SystemColors.Window
        '        Me.txt_Hw.BackColor = SystemColors.ControlDark
        '        Me.lbl_Info.Text = str_InfoH(0)
        '    Case Enu_DefinitionH.HauteurAme
        '        Me.txt_Hw.BackColor = SystemColors.Window
        '        Me.txt_Ha.BackColor = SystemColors.ControlDark
        '        Me.lbl_Info.Text = str_InfoH(1)
        'End Select

        'Me.txt_Ha.ReadOnly = (DefinitionHauteur = Enu_DefinitionH.HauteurAme)
        'Me.txt_Hw.ReadOnly = (DefinitionHauteur = Enu_DefinitionH.HauteurTotale)

        PrepareTextBoxDipo(Me.txt_Ha, DefinitionHauteur = Enu_DefinitionH.HauteurTotale)
        PrepareTextBoxDipo(Me.txt_Hw, DefinitionHauteur = Enu_DefinitionH.HauteurAme)

        Select Case DefinitionHauteur
            Case Enu_DefinitionH.HauteurTotale
                Me.lbl_Info.Text = str_InfoH(0)
            Case Enu_DefinitionH.HauteurAme
                Me.lbl_Info.Text = str_InfoH(1)
        End Select

    End Sub

    Private Sub MAJI_RatioAirePRS()

        Dim RatioA As Decimal = MySectionLoc.ProfilA.AireFi / MySectionLoc.ProfilA.AireFs

        Dim lOK As Boolean = True

        If IsGreater(RatioA, OptionsScope.RapportAfMax) Then lOK = False
        If IsSmaller(RatioA, OptionsScope.RapportAfMin) Then lOK = False

        Me.txt_RatioAf.Text = GetStringInUnitN(RatioA, Enu_TypeVariable.SansType, 4, 3, NON_U, True)

        If lOK Then
            Me.txt_RatioAf.ForeColor = Me.txt_Tfs.ForeColor
            ErrorProviderRatioAf.Clear()
        Else
            Me.txt_RatioAf.ForeColor = CouleurErreur
            ErrorProviderRatioAf.SetError(Me.txt_RatioAf, ErreurRatioAf)
        End If

    End Sub

    Private Sub chk_Plat_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Plat.CheckedChanged

        If lBuild Then Exit Sub

        MySectionLoc.ProfilA.lPlat = Me.chk_Plat.Checked

        Me.img_Section.Invalidate()

        MAJI_Plats()

    End Sub


    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) Handles txt_Bfi.Leave, txt_Wplat.Leave, txt_Tw.Leave, txt_Tfs.Leave, txt_Tfi.Leave, txt_Hw.Leave, txt_Ha.Leave, txt_EpPlat.Leave, txt_Bfs.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Me.img_Section.Invalidate()
    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) Handles txt_Bfi.Enter, txt_Wplat.Enter, txt_Tw.Enter, txt_Tfs.Enter, txt_Tfi.Enter, txt_Hw.Enter, txt_Ha.Enter, txt_EpPlat.Enter, txt_Bfs.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.txt_Ha.Name
                iSelect = 0
            Case Me.txt_Bfs.Name
                iSelect = 1
            Case Me.txt_Tfs.Name
                iSelect = 2
            Case Me.txt_Bfi.Name
                iSelect = 3
            Case Me.txt_Tfi.Name
                iSelect = 4
            Case Me.txt_Tw.Name
                iSelect = 5
            Case Me.txt_Hw.Name
                iSelect = 7
            Case Me.txt_Wplat.Name
                iSelect = 20
            Case Me.txt_EpPlat.Name
                iSelect = 21

        End Select
        Me.img_Section.Invalidate()

        '       0   pour ha
        '       1   pour bfs ou b
        '       2   pour tfs ou tf
        '       3   pour bfi (PRS non sym)
        '       4   pour tfi (PRS non sym)
        '       5   pour tw
        '       6   pour r
        '       7   pour hw
        '       20  pour wp
        '       21  pour tp
    End Sub

    Private Sub cmb_NuancePlat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NuancePlat.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim inDice As Integer = Me.cmb_NuancePlat.SelectedIndex

        MySectionLoc.AcierPlat.Nuance = AcierPlats(inDice).Nuance
        MySectionLoc.AcierPlat.Qualite = AcierPlats(inDice).Qualite
        MySectionLoc.AcierPlat.Reduction = AcierPlats(inDice).Reduc

    End Sub

#End Region

#Region "   Gestion selection profile "

    Private Sub GestionChangeProfile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid_ProfilesSup.SelectionChanged

        If lBuild Then Exit Sub

        Dim indRow As Integer

        'If Me.Grid_ProfilesSup.SelectedCells.Count = 0 Then
        '    '-- On empeche un utilisateur de déselectionner
        '    Me.Grid_ProfilesSup(0, iLignePro - 1).Selected = True
        'End If

        indRow = sender.SelectedCells(0).RowIndex
        'iLignePro = indRow
        Dim iProfile As Integer = indRow

        Dim Gamme As String = Me.lst_GammeS.Text
        Dim Etiquette As String = NettoieNomProfil(Me.Grid_ProfilesSup(0, iProfile).Value.ToString)

        TransfertSaisieGridProfile(Gamme, Etiquette, MySectionLoc)

        lBuild = True
        AfficherPoutreEnCours()
        lBuild = False

        'MAJ_Aciers(Gamme, Etiquette)
        'SelectDefaultSteel(False)
        'GetAcierFromGrid()

        'RemplirDelivery(Gamme, Etiquette)

        'MAJ_DonneesFinales()
        Me.img_Section.Invalidate()

    End Sub

    Private Sub TransfertSaisieGridProfile(ByVal Gamme As String, ByVal Profile As String, ByRef MySection As cls_Section)

        MySectionLoc.ProfilA.Gamme = Gamme
        MySectionLoc.ProfilA.NomProfile = Profile

        MySectionLoc.ProfilA.ha = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
        MySectionLoc.ProfilA.Bfs = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MySectionLoc.ProfilA.Tfs = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MySectionLoc.ProfilA.Tw = MyCatalogue.Series(Gamme).Profiles(Profile).Tw
        MySectionLoc.ProfilA.Rcs = MyCatalogue.Series(Gamme).Profiles(Profile).Rc

        MySectionLoc.ProfilA.Bfi = MySectionLoc.ProfilA.Bfs
        MySectionLoc.ProfilA.Rci = MySectionLoc.ProfilA.Rcs
        MySectionLoc.ProfilA.Tfi = MySectionLoc.ProfilA.Tfs

        ReDim MySectionLoc.ProfilA.IndStandart(MyCatalogue.nbStandard)
        For i As Integer = 0 To MyCatalogue.nbStandard - 1
            MySectionLoc.ProfilA.IndStandart(i) = MyCatalogue.Series(Gamme).Profiles(Profile).IndStandart(i)
        Next

    End Sub

    Private Sub MAJ_Aciers(ByVal Serie As String, ByVal Profile As String)
        '------------------------------------------------------------------------------------------------
        '
        '   03/07/12 :  Création - V3.00
        '
        '------------------------------------------------------------------------------------------------
        '   
        '   Mise à jour du choix de l'acier en fonction du choix du profilé
        '   
        '------------------------------------------------------------------------------------------------
        '
        '   Serie, Profile  [E] :   Indentifion du profilé sélectionné
        '
        '------------------------------------------------------------------------------------------------
        Dim lPRS As Boolean = (MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym Or MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym)

        Remplir_GridAcier(Serie, Profile, OptionsDatabase.ChoiceSteel, lPRS)

    End Sub

    Private Sub MAJI_Plats()

        Dim lPlat As Boolean = Me.chk_Plat.Checked

        Me.lbl_NuancePlat.Visible = lPlat
        Me.lbl_Wplat.Visible = lPlat
        Me.lbl_Tplat.Visible = lPlat

        Me.cmb_NuancePlat.Visible = lPlat
        Me.txt_Wplat.Visible = lPlat
        Me.txt_EpPlat.Visible = lPlat

        Me.etq_UnitDim8.Visible = lPlat
        Me.etq_UnitDim9.Visible = lPlat

        Me.img_Tplat.Visible = lPlat
        Me.img_Wplat.Visible = lPlat

    End Sub

#End Region

#Region "   Conditions de livraison "

    'Private Sub RemplirDelivery(ByVal Serie As String, ByVal Profile As String)

    '    Dim iRow As Integer = 0

    '    Me.lbl_Delivery.Text = RemplaceDollar(strDeliveryConditions, Profile)
    '    Me.GridDelivery.Rows.Clear()

    '    'GridDelivery.AutoResizeRow(iRow - 1)

    '    For i As Integer = 1 To MyCatalogue.nbDelivery

    '        If MyCatalogue.Series(Serie).Profiles(Profile).IndDeliv(i - 1) = 1 Then

    '            GridDelivery.Rows.Add()
    '            iRow += 1
    '            GridDelivery(0, iRow - 1).Value = CStr(iRow)
    '            GridDelivery(1, iRow - 1).Value = MyCatalogue.Delivery(i - 1)(ILangueDelivery)
    '            GridDelivery(1, iRow - 1).Selected = False
    '        End If

    '    Next

    'End Sub

    'Private Sub PrepareGridDelivery()

    '    GridDelivery.Columns(1).CellTemplate.Style.WrapMode = DataGridViewTriState.True
    '    GridDelivery.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

    'End Sub

#End Region

#Region "   Mise à jour des données "

    Private Sub MAJ_AvailablePro(ByVal lCustom As Boolean)

        'Me.etq_NoAvailablePro.Visible = Not lAvailPro

        'If Not lCustom Then
        '    Me.Grid_ProfilesSup.Visible = lAvailPro

        '    Me.grp_DonneesFinales.Visible = lAvailPro
        '    Me.GridDelivery.Visible = lAvailPro
        '    Me.etq_Delivery.Visible = lAvailPro

        '    Me.etq_Grade.Visible = lAvailPro
        '    Me.etq_Qualite.Visible = lAvailPro
        '    Me.etq_ReductionCurve.Visible = lAvailPro
        '    Me.GridAciers.Visible = lAvailPro
        '    Me.img_ReductionCurve.Visible = lAvailPro
        'End If

    End Sub

#End Region

#Region "   Remplissage des Profiles en fonction de la Gamme "

    Sub RemplissageGrilleProfile(ByVal MyGrille As DataGridView, ByVal Gamme As String, ByRef NbProGrille As Integer)
        '-----------------------------------------------------------------------------------
        '
        '   Remplissage d'une grille avec tous les profilés d'une gamme
        '
        '-----------------------------------------------------------------------------------
        '   R16-007 : on retourne le nombre de profilés affichés dans la grille
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim lBuildBack As Boolean = lBuild
        Dim iPro As Integer = 0
        Dim Chaine As String
        Dim iColor As Integer = 0
        ' Dim BackColors() = {Color.LightGray, Color.Orange}
        Dim BackColors() = {Color.LightGray, Color.White}
        Dim ColorNA As Color = Color.Gray
        Dim lSoftProfile As Boolean
        Dim lAffiche As Boolean

        '--> Initialisation

        lBuild = True

        MyGrille.Rows.Clear()

        '--> Boucle sur tous les profilés de la gamme

        For Each kVs As KeyValuePair(Of String, Cls_SectionNew) In MyCatalogue.Series(Gamme).Profiles

            lSoftProfile = kVs.Value.lSoft

            lAffiche = (lSoftProfile Or Not OptionsDatabase.lSoftLimited) And EstCompatibleACB(kVs.Value)

            If lAffiche Then
                MyGrille.Rows.Add()
                iPro += 1
                MyGrille.Rows(iPro - 1).Height = 14

                If kVs.Value.lSoft Then
                    MyGrille.Rows(iPro - 1).Cells(0).Style.BackColor = BackColors(iColor)
                    MyGrille.Rows(iPro - 1).Cells(1).Style.BackColor = BackColors(iColor)
                Else
                    MyGrille.Rows(iPro - 1).Cells(0).Style.BackColor = ColorNA
                    MyGrille.Rows(iPro - 1).Cells(1).Style.BackColor = ColorNA
                End If
                'If kVs.Value.lSoft Then Chaine = "" Else Chaine = "*"
                If kVs.Value.lSoft Then Chaine = "" Else Chaine = STARCHAR
                Chaine = Chaine & kVs.Value.Etiquette
                MyGrille(0, iPro - 1).Value = Chaine
                'If (kVs.Value.IndHI = 1) Then
                '    MyGrille(1, iPro - 1).Value = strHISTAR
                'Else
                '    MyGrille(1, iPro - 1).Value = ""
                'End If

                iColor += 1
                If iColor > 1 Then iColor = 0

            End If

        Next kVs

        lBuild = lBuildBack
        NbProGrille = iPro

    End Sub

    Private Function EstCompatibleACB(ByVal Profile As Cls_SectionNew) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '
        '   10/12/12 :  Création - POM - V3.00
        '
        '---------------------------------------------------------------------------------------------------------
        '
        '   Indique si un profile vérifie les conditions minimales pour qu'il puisse exister une solution
        '
        '---------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------


        '-- Déclarations

        Dim lCompatible As Boolean = True



        Return lCompatible

    End Function






#End Region

#Region "   Gestion grille des aciers "

    Private Sub GridAciers_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridAciers.SelectionChanged

        If lBuild Then Exit Sub
        If Me.GridAciers.Rows.Count = 0 Then Exit Sub

        GetAcierFromGrid()

        MAJNuancesPossibles()

        ' MAJ_DonneesFinales()

        Me.img_Section.Invalidate()
        Me.img_ReductionCurve.Invalidate()

    End Sub

    Private Sub GetAcierFromGrid()
        '-------------------------------------------------------------------------------------------------------------------------
        '
        '   Récupération des données acier sélectionnées par l'utilisateur dans la grille
        '
        '-------------------------------------------------------------------------------------------------------------------------

        If GridAciers.SelectedCells.Count = 0 Then
            Me.GridAciers(0, iLigneAcier - 1).Selected = True
        End If

        Dim indRow As Integer = GridAciers.SelectedCells(0).RowIndex
        iLigneAcier = indRow

        Dim Nuance, Qualite, Norme As String

        Nuance = GridAciers(0, indRow).Value.ToString.Trim
        Qualite = GridAciers(1, indRow).Value.ToString.Trim
        Norme = GridAciers(2, indRow).Value.ToString.Trim

        TransfertGridAcier(Nuance, Qualite, Norme, MySectionLoc)

    End Sub

    Private Sub TransfertGridAcier(ByVal Nuance As String, ByVal Qualite As String, ByVal Reduction As String, ByVal MySection As cls_Section)

        MySectionLoc.Acier.Nuance = Nuance
        MySectionLoc.Acier.Qualite = Qualite
        MySectionLoc.Acier.Reduction = Reduction

        MySectionLoc.Acier.EpMax = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).EpMax

        MySectionLoc.Acier.iBase = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).iBase
        MySectionLoc.Acier.iStandart = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).StIndex

        MySectionLoc.Acier.Plages.Clear()
        Dim MyPlage As cls_Acier.strucPlage
        For i As Integer = 0 To SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages.Count - 1
            MyPlage.Ep = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Ep
            MyPlage.Fy = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fy
            MyPlage.Fu = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fu
            MySectionLoc.Acier.Plages.Add(MyPlage)
        Next

        Dim iStd As Integer

        iStd = SteelBase.IndexStd.IndexOf(SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).StIndex)
        If iStd > -1 Then
            MySectionLoc.Acier.NormeProduit = SteelBase.NormeStd(iStd)
            MySectionLoc.Acier.iTabStandart = iStd
        End If
    End Sub

    Private Sub MAJNuancesPossibles()
        '-----------------------------------------------------------------------------------------------------
        '
        '   12/11/15 :  Création - V3.09 - POM
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   Gestion de l'affichage en fonction du choix de nuance
        '   Elimine la possibilité de sélectionner la nuance S235/S275 le cas échéant
        '
        '-----------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyNuance As String
        Dim lNuanceOK As Boolean

        '--> Traitement

        MyNuance = MySectionLoc.Acier.Nuance

        '--> La Nuance est elle autorisée

        lNuanceOK = IsMyNuancePossible(MyNuance)

        '--> Gestion de la disponibilité des boutons

        lNuancePossible = lNuanceOK Or LogicielOptions.lExpert

        Me.btn_OK.Enabled = lNuancePossible
        'If Me.cmd_Precedent.Visible Then Me.cmd_Precedent.Enabled = lNuancePossible

    End Sub

    Private Function IsMyNuancePossible(ByVal MyNuance As String) As Boolean
        '-----------------------------------------------------------------------------------------------------
        '
        '   12/11/15 :  Création - V3.09 - POM
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   Indique si une nuance sélectionnée est selectionnable par l'utilisateur
        '   Elimine la possibilité de sélectionner la nuance S235/S275 le cas échéant
        '
        '-----------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouve As Boolean
        Dim iGrad As Integer

        '--> Traitement

        lTrouve = False
        iGrad = -1

        If LogicielReglages.lNoS235 Then
            Do While (Not lTrouve) And (iGrad < NuancesExclues.GetUpperBound(0))
                iGrad += 1
                lTrouve = (MyNuance = NuancesExclues(iGrad))
            Loop
        End If

        Return (Not lTrouve)
    End Function

    Private Sub Remplir_GridAcier(ByVal Serie As String, ByVal Profile As String, ByVal ChoiceSteel As EnuChoiceAcier, Optional lPRS As Boolean = False)

        Dim lBuildBack As Boolean = lBuild
        lBuild = True

        Me.GridAciers.Rows.Clear()
        Dim GradeEnCours As String = ""
        Dim QualiteEncours As String = ""
        Dim lAvailable As Boolean = True
        Dim iPro As Integer = 0
        Dim lDisplay As Boolean
        Dim lIsNuanceCompatibleProfile As Boolean

        Dim nbSteels As Integer
        Dim MySteel As strucAcierLocal
        Dim SteelToScreen As New List(Of strucAcierLocal)

        nbSteels = 0

        For Each kvpGrade As KeyValuePair(Of String, strucGrade) In SteelBase.Grades

            For Each kvpQualite As KeyValuePair(Of String, strucQualite) In kvpGrade.Value.Qualites

                For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv

                    'lAvailable = (MyCatalogue.Series(Serie).Profiles(Profile).IndStandart(MyCatalogue.CorIndStd(kvpSteel.Value.StIndex) - 1) = 1)

                    'If lAvailable Or Not OptionsDataBase.lShowSteelAvailOnly Then

                    If lPRS Then
                        lDisplay = SteelIsToDisplayPRS(kvpGrade.Key, kvpQualite.Key, kvpSteel.Key)
                    Else
                        lDisplay = SteelIsToDisplay(Serie, Profile, kvpGrade.Key, kvpQualite.Key, kvpSteel.Key, ChoiceSteel, lIsNuanceCompatibleProfile)
                    End If

                    'If lDisplay Or lPRS Then
                    If lDisplay Then
                        MySteel.Nuance = kvpGrade.Key
                        MySteel.Qualite = kvpQualite.Key
                        MySteel.Reduc = kvpSteel.Key
                        MySteel.lAvailable = lIsNuanceCompatibleProfile Or lPRS
                        SteelToScreen.Add(MySteel)
                    End If

                Next

            Next

        Next

        Call DisplaySteelToScreen(SteelToScreen, nbSteels, ChoiceSteel)

        If nbSteels = 0 Then
            Me.img_ReductionCurve.Visible = False
            '  Me.Pan_Alerte.Visible = True
            Me.btn_OK.Enabled = False
        Else
            Me.img_ReductionCurve.Visible = True
            Me.img_ReductionCurve.Invalidate()
            'Me.Pan_Alerte.Visible = False
            Me.btn_OK.Enabled = True
        End If

        lBuild = lBuildBack

    End Sub

    Private Function SteelIsToDisplayPRS(ByVal Nuance As String, ByVal Qualite As String, ByVal Norm As String) As Boolean
        '------------------------------------------------------------------------------------------------------------------------------------------
        '   26/06/25 :  Création - POM - V1.00
        '------------------------------------------------------------------------------------------------------------------------------------------
        '   Indique si la nuance est à afficher, pour un PRS
        '------------------------------------------------------------------------------------------------------------------------------------------
        '
        '------------------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lisHistar As Boolean
        Dim lDisplay As Boolean = True
        Dim lAcierEC3 As Boolean
        Const QUALITEEC3 As String = "EC3"
        Dim lEC3OK As Boolean = True

        '--( Traitement

        lisHistar = (Nuance.Trim.ToUpper.Contains(lblHISTAR))
        lAcierEC3 = (Qualite.Trim.ToUpper = QUALITEEC3)

        If lIsHISTAR Then
            If (Not LogicielReglages.lHISTAR) And (Not LogicielOptions.lExpert) Then lDisplay = False
        End If

        'If OptionsDatabase.lNoSteelLowThick Then
        '    lCompatible = EpMax <= SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Norm).EpMax * (1.00001)
        'Else
        '    lCompatible = True
        'End If

        If lAcierEC3 Then
            lEC3OK = LogicielOptions.lExpert Or LogicielReglages.lEC3
        End If

        Return (lDisplay And lEC3OK)
    End Function

    Private Function SteelIsToDisplay(ByVal Serie As String, ByVal Profile As String,
                                      ByVal Nuance As String, ByVal Qualite As String, ByVal Norm As String, ByVal ChoiceAcier As EnuChoiceAcier,
                                      ByRef lIsNuanceCompatibleProfile As Boolean) As Boolean
        '------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   06/12/12 :  Création - POM - V3.00
        '
        '------------------------------------------------------------------------------------------------------------------------------------------
        '   Indique si la nuance est à afficher, pour un laminé
        '------------------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim EpMax As Double
        Dim IndStd() As Short
        Dim nbStd As Integer

        '--> Initialisation

        EpMax = Math.Max(MyCatalogue.Series(Serie).Profiles(Profile).Tf, MyCatalogue.Series(Serie).Profiles(Profile).Tw)
        nbStd = MyCatalogue.Series(Serie).Profiles(Profile).IndStandart.GetUpperBound(0)
        ReDim IndStd(nbStd)
        For i As Integer = 0 To nbStd
            IndStd(i) = CInt(MyCatalogue.Series(Serie).Profiles(Profile).IndStandart(i))
        Next

        '--> Calcul

        Return SteelisCompatibleToProfile(EpMax, IndStd, SteelBase, MyCatalogue.CorIndStd, Nuance, Qualite, Norm, ChoiceAcier, lIsNuanceCompatibleProfile)

    End Function

    Private Sub DisplaySteelToScreen(ByVal Steels As List(Of strucAcierLocal), ByRef nbSteels As Integer, ByVal Choice As EnuChoiceAcier)
        '-----------------------------------------------------------------------------------------------------
        '
        '   20/12/12 :  Création - V3.01 - POM
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   AffichageOptFeu à l'écran de la liste des aciers compatibles
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   Steels      [E] :   Liste des aciers potentiellement compatibles
        '   nbSteels    [S] :   Nombre d'acier finalement affichés
        '   Choice      [E] :   Stratégie d'affichage des aciers
        '
        '-----------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nDispo As Integer
        Dim lTous As Boolean
        Dim GradeEnCours As String, lNewGrade As Boolean
        Dim QualiteEncours As String, lNewQualite As Boolean
        Dim iRank As Integer

        '--> Initialisations

        nDispo = 0

        For i As Integer = 0 To Steels.Count - 1
            If Steels(i).lAvailable Then nDispo += 1
        Next

        lTous = (Choice <> EnuChoiceAcier.BaseIfNoStandardSteel) Or (Choice = EnuChoiceAcier.BaseIfNoStandardSteel And nDispo = 0)

        '--> AffichageOptFeu

        nbSteels = 0
        iRank = 0
        GradeEnCours = ""
        QualiteEncours = ""

        For i As Integer = 0 To Steels.Count - 1
            If lTous Or Steels(i).lAvailable Then
                lNewGrade = (Steels(i).Nuance <> GradeEnCours)
                lNewQualite = (Steels(i).Qualite <> QualiteEncours)
                AddLigneTableauAcier(Steels(i), iRank, lNewGrade, lNewQualite)
                nbSteels += 1
                GradeEnCours = Steels(i).Nuance
                QualiteEncours = Steels(i).Qualite
            End If
        Next

    End Sub

    Private Sub AddLigneTableauAcier(ByVal MySteel As strucAcierLocal, ByRef iRank As Integer,
                                     ByVal lNewGrade As Boolean, ByVal lNewQualite As Boolean)
        '-----------------------------------------------------------------------------------------------------
        '
        '   20/12/12 :  Création - V3.01 - POM
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   On rajoute un acier dans le tableau des aciers
        '
        '-----------------------------------------------------------------------------------------------------

        '--> On crée la ligne supplémentaire

        Me.GridAciers.Rows.Add()
        iRank += 1
        Me.GridAciers.Rows(iRank - 1).Height = 14

        '--> AffichageOptFeu de la nuance en fonction de nouvelle nuance ou pas ?

        If lNewGrade Then
            Me.GridAciers(0, iRank - 1).Value = MySteel.Nuance
            Me.GridAciers(0, iRank - 1).Style.ForeColor = ColorGrade
        Else
            Me.GridAciers(0, iRank - 1).Value = MySteel.Nuance
            Me.GridAciers(0, iRank - 1).Style.ForeColor = Me.GridAciers.BackgroundColor
        End If

        '--> AffichageOptFeu de la qualité

        If lNewGrade Or lNewQualite Then
            Me.GridAciers(1, iRank - 1).Value = MySteel.Qualite
            If MySteel.Qualite = "HISTAR" Then
                Me.GridAciers(1, iRank - 1).Style.ForeColor = ColorGridHI
            Else
                Me.GridAciers(1, iRank - 1).Style.ForeColor = Color.Black
            End If
        Else
            Me.GridAciers(1, iRank - 1).Value = MySteel.Qualite
            Me.GridAciers(1, iRank - 1).Style.ForeColor = Me.GridAciers.BackgroundColor
        End If

        '--> AffichageOptFeu de la courbe de réduction

        Me.GridAciers(2, iRank - 1).Value = MySteel.Reduc

        Dim ColorBack As Color

        If MySteel.lAvailable Then
            ColorBack = BClrCompatible
        Else
            ColorBack = BClrNotC
        End If
        For i As Integer = 0 To 2
            Me.GridAciers(i, iRank - 1).Style.BackColor = ColorBack
        Next

    End Sub

    Private Sub ExtraitValeursEnveloppeAciers(ByVal Nuance As String, ByVal Variable As EnuDrawProperty,
                                              ByRef EpMin As Double, ByRef EpMax As Double, ByRef VMax As Double)
        '------------------------------------------------------------------------------------------------------------------
        '
        '   03/07/12 :  Création - V300
        '
        '------------------------------------------------------------------------------------------------------------------
        '
        '   Extrait les valeurs enveloppes de la nuance sélectionnée
        '
        '------------------------------------------------------------------------------------------------------------------
        '
        '   
        '
        '------------------------------------------------------------------------------------------------------------------

        EpMin = 0
        EpMax = 0
        VMax = 0

        For Each kvpQualite As KeyValuePair(Of String, strucQualite) In SteelBase.Grades(Nuance).Qualites

            For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv
                If EpMin = 0 Then
                    EpMin = kvpSteel.Value.Plages(0).Ep
                Else
                    EpMin = Math.Min(EpMin, kvpSteel.Value.Plages(0).Ep)
                End If
                EpMax = Math.Max(EpMax, kvpSteel.Value.EpMax)

                For Each kVP As cls_Acier.strucPlage In kvpSteel.Value.Plages
                    Select Case Variable
                        Case EnuDrawProperty.Fu : VMax = Math.Max(VMax, kVP.Fu)
                        Case EnuDrawProperty.Fy : VMax = Math.Max(VMax, kVP.Fy)
                    End Select

                Next


            Next

        Next

    End Sub

    Private Sub RemplirCmbNuancesPlats()

        Dim MySteel As strucAcierLocal

        AcierPlats.Clear()

        For Each kvpGrade As KeyValuePair(Of String, strucGrade) In SteelBase.Grades

            For Each kvpQualite As KeyValuePair(Of String, strucQualite) In kvpGrade.Value.Qualites

                For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv

                    If isAcierPlat(kvpGrade.Key, kvpQualite.Key) Then

                        MySteel.Nuance = kvpGrade.Key
                        MySteel.Qualite = kvpQualite.Key
                        MySteel.Reduc = kvpSteel.Key

                        AcierPlats.Add(MySteel)

                    End If

                Next

            Next

        Next

        Me.cmb_NuancePlat.Items.Clear()

        For iAcier As Integer = 0 To AcierPlats.Count - 1

            Me.cmb_NuancePlat.Items.Add(AcierPlats(iAcier).Nuance & "/" & AcierPlats(iAcier).Qualite)

        Next

    End Sub

    Private Function isAcierPlat(Nuance As String, Qualite As String) As Boolean
        '-----------------------------------------------------------------------------------------------------------------------
        '   18/11/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------
        '   Indique si on prend en compte l'acier pour les plats de renfort
        '-----------------------------------------------------------------------------------------------------------------------
        '   Nuance      [E] :   Nuance
        '   Qualite     [E] :   Qualité
        '-----------------------------------------------------------------------------------------------------------------------

        Dim lOK As Boolean

        lOK = (Array.IndexOf(NuancesPlats, Nuance) >= 0)

        If lOK Then

            If Not ((Qualite.IndexOf("EC3") >= 0) Or (Qualite.IndexOf("JR") >= 0)) Then
                lOK = False
            End If

        End If

            Return lOK

    End Function



#End Region

#Region " Gestion affichage Fy Fu "

    Private Sub btn_FyFu_Click(sender As Object, e As EventArgs) Handles btn_FyFu.Click

        Select Case DrawProperty
            Case EnuDrawProperty.Fu : DrawProperty = EnuDrawProperty.Fy
            Case EnuDrawProperty.Fy : DrawProperty = EnuDrawProperty.Fu
        End Select
        'lDessinFy = Not lDessinFy

        AfficheBtnFyFu()

        Me.img_ReductionCurve.Invalidate()

    End Sub

#End Region


End Class