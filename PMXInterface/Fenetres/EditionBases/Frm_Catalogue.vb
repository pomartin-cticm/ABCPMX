Imports System.IO
Imports PMXMoteur2

Public Class Frm_Catalogue

#Region "   Declaration  "

    Dim lBuild As Boolean

#End Region

#Region "   Variables  "

    Const RATIOHIGAMME As Double = 0.45
    Dim ColorGridHI As Color = Color.Blue

    'pour affichage series
    Dim tabUnitLongueur() As String = {"m", "cm", "mm"}
    Dim indUnitDimensions As Integer
    Dim strCustom As String
    'Dim MyCatalogue As StrucCatalogue
    Dim Series As Dictionary(Of String, StrucGamme)

    'pour affichage profile
    Dim NbProG As Integer
    Dim G_PP_loc As cls_Poutre.StructPoidsPropres

    'pour dessin de la section
    Dim iSelect As Integer = -1
    Dim kAdjust As Decimal = 0.95
    'Dim sWI, sHI As Single
    'Dim MyParAff As Struc_Affichage
    'Dim strHISTAR As String
    'Dim DecalLabel As Single        ' Decalage nécessaire pour l'affichage des épaisseurs de semelles
    Dim MyPoutreLoc As cls_Poutre
    Dim PoidsPropreLoc As cls_Poutre.StructPoidsPropres

    'Pour rouge en MouseEnter
    Dim type As String = ""

    '--> Passage vers Note du Catalogue *******************************************************************************
    Dim Propriete As New List(Of String)
    Dim Titre As New List(Of String)
    '******************************************************************************************************************

    Dim SizeFont As Single = SizeFontFrm
    Dim FontFrm As Font

#End Region

#Region "===Ouverture fenetre=================="

    Private Sub Frm_Catalogue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GestionLangue()
        InitialisationFenetre()
    End Sub

    Private Sub GestionLangue()
        '
        '   Gestion de la langue pour la fenetre
        '
        '------------------------------------------------------------------------------------------------

        '--> Chargement des blocs langues 

        Dim Bloc As New Dictionary(Of String, String)
        Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRMCATALOGUE")
        BlocLine.CreationBloc(Bloc)

        Try
            'Titre + Bouton
            Me.Text = Bloc("TITLE")
            Me.cmd_fermer.Text = Bloc("CLOSE")

            'GroupBox
            'Me.grp_Draw.Text = ""

            'Label multi-langue
            Me.etq_Profils.Text = Bloc("PROFIL")
            Me.etq_Gamme.Text = Bloc("SERIE")
            Me.etq_Dims.Text = Bloc("DIMENSIONS")
            Me.etq_Proprietes.Text = Bloc("PROPERTIES")
            'Me.etq_Unite.Text = Bloc("UNIT_DIM")

            'ComboBox
            Dim i As Integer
            For i = 0 To tabUnitLongueur.Count - 1
                'Me.cmb_Dimensions.Items.Add(tabUnitLongueur(i))
            Next
            indUnitDimensions = tabUnitLongueur.Count - 1
            'Me.cmb_Dimensions.SelectedIndex = indUnitDimensions

            'Texte Survol Propriétés
            Dim toolTip As New ToolTip()
            toolTip.SetToolTip(Me.lbl_G, Bloc("TOOLTIP_G"))
            toolTip.SetToolTip(Me.lbl_A, Bloc("TOOLTIP_A"))
            toolTip.SetToolTip(Me.img_Iy, Bloc("TOOLTIP_IY"))
            toolTip.SetToolTip(Me.img_Wely, Bloc("TOOLTIP_WELY"))
            toolTip.SetToolTip(Me.img_Wply, Bloc("TOOLTIP_WPLY"))
            toolTip.SetToolTip(Me.img_iy2, Bloc("TOOLTIP_IY2"))
            toolTip.SetToolTip(Me.img_Avz, Bloc("TOOLTIP_AVZ"))
            toolTip.SetToolTip(Me.img_Iz, Bloc("TOOLTIP_IZ"))
            toolTip.SetToolTip(Me.img_Welz, Bloc("TOOLTIP_WELZ"))
            toolTip.SetToolTip(Me.img_Wplz, Bloc("TOOLTIP_WPLZ"))
            toolTip.SetToolTip(Me.img_iz2, Bloc("TOOLTIP_IZ2"))
            toolTip.SetToolTip(Me.img_It, Bloc("TOOLTIP_IT"))

            'Texte Survol Dimensions
            toolTip.SetToolTip(Me.img_Ht, Bloc("TOOLTIP_HT"))
            toolTip.SetToolTip(Me.img_Bf, Bloc("TOOLTIP_BF"))
            toolTip.SetToolTip(Me.img_Tf, Bloc("TOOLTIP_TF"))
            toolTip.SetToolTip(Me.img_Tw, Bloc("TOOLTIP_TW"))
            toolTip.SetToolTip(Me.img_Rc, Bloc("TOOLTIP_RC"))

            '--> Note du Catalogue ********************************************************************
            'Bouton
            btn_Note.Text = Bloc("NOTE")

            'titre
            Titre.Add(Bloc("CARA"))
            Titre.Add(Bloc("DIMENSIONS"))
            Titre.Add(Bloc("PROPERTIES"))
            Titre.Add(Bloc("PROFIL"))
            Titre.Add(Bloc("SERIE"))
            Titre.Add(Bloc("VAL"))

            'Proprietes
            Propriete.Add(Bloc("TOOLTIP_G"))
            Propriete.Add(Bloc("TOOLTIP_A"))
            Propriete.Add(Bloc("TOOLTIP_IY"))
            Propriete.Add(Bloc("TOOLTIP_WELY"))
            Propriete.Add(Bloc("TOOLTIP_WPLY"))
            Propriete.Add(Bloc("TOOLTIP_IY2"))
            Propriete.Add(Bloc("TOOLTIP_AVZ"))
            Propriete.Add(Bloc("TOOLTIP_IZ"))
            Propriete.Add(Bloc("TOOLTIP_WELZ"))
            Propriete.Add(Bloc("TOOLTIP_WPLZ"))
            Propriete.Add(Bloc("TOOLTIP_IZ2"))
            Propriete.Add(Bloc("TOOLTIP_IT"))
            '****************************************************************************************

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
            Bloc.Clear()
        End Try

    End Sub

    Private Sub InitialisationFenetre()
        lBuild = True
        Me.Icon = Frm_PMX.Icon
        FontFrm = New Font(FontBase.Name, SizeFont)

        '--> 

        MyPoutreLoc = New cls_Poutre(NomChargements)
        MyPoutreLoc.Section.ProfilA.aW = 0
        MyPoutreLoc.Section.ProfilA.Plat_b = 0
        MyPoutreLoc.Section.ProfilA.Plat_t = 0
        MyPoutreLoc.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
        PoidsPropreLoc = MyPoutreLoc.ChargeRepartiePP()

        '--( Unités rayon de giration

        Me.etq_UniteRy.Text = "mm"
        Me.etq_UniteRz.Text = "mm"

        '--> Préparation catalogues
        'InitialiseCatalogue(FileACB.SectionsNew, MyCatalogue)

        '--> Look Grille Profils
        PrepareLookGrille(Me.Grid_ProfilesSup, Me.Col_HISTARSup, Me.Col_ListeSup, Me.lst_Gamme.BackColor, RATIOHIGAMME)

        '--> Remplissage des series
        RemplirSeries()

        '--> Initialisation des séries et des profilés sur la première valeur + affichage des stats
        lst_Gamme.SelectedIndex = 0
        RemplissageGrilleProfile(Me.Grid_ProfilesSup, Me.lst_Gamme.Text, NbProG)
        TransfertSaisieGridProfile(Me.lst_Gamme.Text, Me.Grid_ProfilesSup(0, 0).Value.ToString)

        lBuild = False
    End Sub

#End Region

#Region "   Remplissage des Séries "

    Private Sub RemplirSeries()
        '--> Remplissage des series à partir des données 
        Me.lst_Gamme.Items.Clear()
        For Each kvp As KeyValuePair(Of String, StrucGamme) In MyCatalogue.Series
            Me.lst_Gamme.Items.Add(kvp.Key)
        Next

    End Sub

#End Region

#Region "   Remplissage des Profilés "

    Private Sub lst_Gamme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lst_Gamme.SelectedIndexChanged
        If lBuild Then Exit Sub

        RemplissageGrilleProfile(Me.Grid_ProfilesSup, Me.lst_Gamme.Text, NbProG)

        '--> Initialisation au premier profilé de la liste pour la gamme séléctionnée
        TransfertSaisieGridProfile(Me.lst_Gamme.Text, Me.Grid_ProfilesSup(0, 0).Value.ToString)

    End Sub

    Sub RemplissageGrilleProfile(ByVal MyGrille As DataGridView, ByVal Gamme As String, ByRef NbProGrille As Integer)
        '-----------------------------------------------------------------------------------
        '
        '   Remplissage d'une grille avec tous les profilés d'une gamme
        '
        '-----------------------------------------------------------------------------------
        '   R16-004 : on retourne le nombre de profilés affichés dans la grille
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim lBuildBack As Boolean = lBuild
        Dim iPro As Integer = 0
        Dim Chaine As String
        Dim iColor As Integer = 0
        ' Dim BackColors() = {Color.LightGray, Color.Orange}
        Dim BackColors() As Color = {Color.LightGray, Color.White}
        Dim ColorNA As Color = Color.Gray
        Dim lSoftProfile As Boolean
        Dim lAffiche As Boolean

        '--> Initialisation
        lBuild = True
        NbProGrille = 0
        MyGrille.Rows.Clear()

        '--> Traitement
        For Each kVs As KeyValuePair(Of String, Cls_SectionNew) In MyCatalogue.Series(Gamme).Profiles

            lSoftProfile = kVs.Value.lSoft
            lAffiche = (lSoftProfile Or Not OptionsDatabase.lSoftLimited) 'And EstCompatibleANGELINA(kVs.Value)

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
                If kVs.Value.lSoft Then Chaine = "" Else Chaine = "*"
                Chaine = Chaine & kVs.Value.Etiquette
                MyGrille(0, iPro - 1).Value = Chaine

                iColor += 1
                If iColor > 1 Then iColor = 0
            End If

        Next kVs

        lBuild = lBuildBack
        NbProGrille = iPro

    End Sub

#End Region

#Region "   Look Grille "

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

#End Region

#Region "   Affichage Propriétés Section "

    Private Sub SelectionDuProfile(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid_ProfilesSup.SelectionChanged

        '--> Séléction du profile
        If lBuild Then Exit Sub

        Dim indRow As Integer = sender.SelectedCells(0).RowIndex
        Dim Gamme As String = Me.lst_Gamme.Text
        Dim Etiquette As String = Me.Grid_ProfilesSup(0, indRow).Value.ToString

        TransfertSaisieGridProfile(Gamme, Etiquette)
    End Sub

    Private Sub TransfertSaisieGridProfile(ByVal Gamme As String, ByVal Profile As String)
        '--------------------------------------------------------------------------------------------
        '
        '   Transfert de la saisie (click) d'un profilé dans la variable locale ad'hoc
        '
        '--------------------------------------------------------------------------------------------

        TransfertSaisieGridProfile(Gamme, Profile, MyPoutreLoc.Section)

    End Sub

    Private Sub TransfertSaisieGridProfile(ByVal Gamme As String, ByVal Profile As String, ByRef MySection As Cls_Section)
        '--> Affichage des stats de la section séléctionée
        lbl_val_Ht.Text = GetStringInUnitN(MyCatalogue.Series(Gamme).Profiles(Profile).Ht, Enu_TypeVariable.Dimension, 4, 3, True, True)
        lbl_val_Bf.Text = GetStringInUnitN(MyCatalogue.Series(Gamme).Profiles(Profile).Bf, Enu_TypeVariable.Dimension, 4, 3, True, True)
        lbl_val_Tf.Text = GetStringInUnitN(MyCatalogue.Series(Gamme).Profiles(Profile).Tf, Enu_TypeVariable.Dimension, 4, 3, True, True)
        lbl_val_Tw.Text = GetStringInUnitN(MyCatalogue.Series(Gamme).Profiles(Profile).Tw, Enu_TypeVariable.Dimension, 4, 3, True, True)
        lbl_val_Rc.Text = GetStringInUnitN(MyCatalogue.Series(Gamme).Profiles(Profile).Rc, Enu_TypeVariable.Dimension, 4, 3, True, True)

        '--> Mise à jour des paramètres des sections affichées
        MySection.ProfilA.ha = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
        MySection.ProfilA.Bfs = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MySection.ProfilA.Bfi = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MySection.ProfilA.Tfs = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MySection.ProfilA.Tfi = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MySection.ProfilA.Tw = MyCatalogue.Series(Gamme).Profiles(Profile).Tw
        MySection.ProfilA.Rcs = MyCatalogue.Series(Gamme).Profiles(Profile).Rc
        MySection.ProfilA.Rci = MyCatalogue.Series(Gamme).Profiles(Profile).Rc
        MySection.ProfilA.Gamme = Gamme
        MySection.ProfilA.NomProfile = Profile

        '--> Calcul des propriétés de la section à afficher

        PoidsPropreLoc = MyPoutreLoc.ChargeRepartiePP()
        MySection.ProfilA.InitialiseProprietes()

        '--> Affichage des propriétés de la section séléctionnée

        lbl_val_G.Text = GetStringInUnit(PoidsPropreLoc.qPP_ProfilAcier / MyPoutreLoc.Param.GraviteG, Enu_TypeVariable.SansType, 4, 3, False)
        lbl_val_A.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.Aire, Enu_TypeVariable.AireCM2, 4, 3, False)
        lbl_val_Iy.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.InertieY, Enu_TypeVariable.InertieCM4, 4, 3, False)
        lbl_val_Wely.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.ModuleWelY, Enu_TypeVariable.ModuleCM3, 4, 3, False)
        lbl_val_Wply.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.ModuleWplY, Enu_TypeVariable.ModuleCM3, 4, 3, False)
        lbl_val_iy2.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.RayonGirationiY, Enu_TypeVariable.LongueurCM, 4, 3, False)
        lbl_val_Avz.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.AireAv(ETA_Catalogue), Enu_TypeVariable.AireCM2, 4, 3, False)
        lbl_val_Iz.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.InertieZ, Enu_TypeVariable.InertieCM4, 4, 3, False)
        lbl_val_Welz.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.ModuleWelZ, Enu_TypeVariable.ModuleCM3, 4, 3, False)
        lbl_val_Wplz.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.ModuleWplz, Enu_TypeVariable.ModuleCM3, 4, 3, False)
        lbl_val_iz2.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.RayonGirationiZ, Enu_TypeVariable.LongueurCM, 4, 3, False)
        lbl_val_It.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.InertieT, Enu_TypeVariable.InertieCM4, 4, 3, False)

        '--> Mise à jour du dessin
        Me.img_Section.Invalidate()

    End Sub

#End Region

#Region "   Dessin de la section "

    Private Sub img_Section_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Section.Paint
        DessinProfileAcierN(e.Graphics, MyPoutreLoc.Section, Me.img_Section.ClientRectangle.Width, Me.img_Section.ClientRectangle.Height,
                            Fontfrm, kAdjust, True, False, iSelect)

    End Sub

#End Region

#Region "   MouseEnter d'une dimension -> Rouge sur le dessin | MouseLeave -> retour dessin normale "

    Private Sub Dimension_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        lbl_val_Ht.MouseEnter, img_Ht.MouseEnter,
        lbl_val_Bf.MouseEnter, img_Bf.MouseEnter,
        lbl_val_Tf.MouseEnter, img_Tf.MouseEnter,
        lbl_val_Tw.MouseEnter, img_Tw.MouseEnter,
        lbl_val_Rc.MouseEnter, img_Rc.MouseEnter

        '--> Affichage en rouge sur le dessin des flèches et valeurs de la dimension MouseEnter
        If sender.Equals(img_Ht) Or sender.Equals(lbl_val_Ht) Then
            type = "Ht"
        ElseIf sender.Equals(img_Bf) Or sender.Equals(lbl_val_Bf) Then
            type = "Bf"
        ElseIf sender.Equals(img_Tf) Or sender.Equals(lbl_val_Tf) Then
            type = "Tf"
        ElseIf sender.Equals(img_Tw) Or sender.Equals(lbl_val_Tw) Then
            type = "Tw"
        ElseIf sender.Equals(img_Rc) Or sender.Equals(lbl_val_Rc) Then
            type = "Rc"
        End If

        '--> Mise à jour du dessin
        Me.img_Section.Invalidate()
    End Sub

    Private Sub Dimension_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
         lbl_val_Ht.MouseLeave, img_Ht.MouseLeave,
         lbl_val_Bf.MouseLeave, img_Bf.MouseLeave,
         lbl_val_Tf.MouseLeave, img_Tf.MouseLeave,
         lbl_val_Tw.MouseLeave, img_Tw.MouseLeave,
         lbl_val_Rc.MouseLeave, img_Rc.MouseLeave
        '--> Retour à l'affichage normale du dessin (tout en noir)
        type = ""
        '--Mise à jour du dessin
        Me.img_Section.Invalidate()
    End Sub

#End Region

#Region "   Modification Unité "

    'Private Sub cmb_Unite_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim Indice As Short
    '    Dim Modif As Boolean = False

    '    Indice = CShort(Me.cmb_Dimensions.SelectedIndex)
    '    If Indice <> indUnitDimensions Then Modif = True
    '    indUnitDimensions = Indice

    '    If Modif Then
    '        Dim indRow As Integer = Me.Grid_ProfilesSup.SelectedCells(0).RowIndex
    '        Dim Gamme As String = Me.lst_Gamme.Text
    '        Dim Etiquette As String = Me.Grid_ProfilesSup(0, indRow).Value.ToString
    '        'GenereFichierINI(FileACB.INI)
    '        TransfertSaisieGridProfile(Gamme, Etiquette)
    '    End If
    'End Sub

#End Region

#Region "   Dessin Unité Propriétés "


    Private Sub PaintUnites(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) _
        Handles img_UniteAire.Paint, img_UniteAireV.Paint,
                img_UniteInertieYY.Paint, img_UniteInertieZZ.Paint, img_UniteInertieT.Paint,
                img_UniteWelYY.Paint, img_UniteWplYY.Paint,
                img_UniteWelZZ.Paint, img_UniteWplZZ.Paint ', img_UniteGirationZ.Paint, img_UniteGirationY.Paint
        '----------------------------------------------------------------------------------------
        '   24/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------
        '   Affiche des unités des propriétés
        '----------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------
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
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Centre
        Const kADJ As Single = 0.9

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = False
        Select Case sender.name

            Case Me.img_UniteAire.Name, Me.img_UniteAireV.Name
                strSymbol = "cm"
                strIndice = ""
            'Case Me.img_UniteGirationY.Name, img_UniteGirationZ.Name
            '    strSymbol = "mm"
            '    strIndice = ""
            Case Me.img_UniteInertieT.Name, Me.img_UniteInertieYY.Name, Me.img_UniteInertieZZ.Name
                strSymbol = "cm"
                strIndice = "4"
            Case Me.img_UniteWelYY.Name, Me.img_UniteWplYY.Name, Me.img_UniteWelZZ.Name, Me.img_UniteWplZZ.Name
                strSymbol = "cm"
                strIndice = "3"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, kADJ, lEgal, True)

    End Sub



#End Region

#Region "   Dessin Indice Propriétés et Dimensions"


    Private Sub PaintSymbols(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Tf.Paint, img_Ht.Paint, img_Bf.Paint,
                             img_Tw.Paint, img_Rc.Paint
        '----------------------------------------------------------------------------------------
        '   24/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------
        '   Affiche les symboles de dimensions
        '----------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------

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
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Centre
        Const kADJ As Single = 0.9

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = False
        Select Case sender.name

            Case Me.img_Ht.Name
                strSymbol = "h"
                strIndice = "t"
            Case Me.img_Bf.Name
                strSymbol = "b"
                strIndice = "f"
            Case Me.img_Tf.Name
                strSymbol = "t"
                strIndice = "f"
            Case Me.img_Tw.Name
                strSymbol = "t"
                strIndice = "w"
            Case Me.img_Rc.Name
                strSymbol = "r"
                strIndice = ""

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, kADJ, lEgal)

    End Sub

    Private Sub PaintSymbolProps(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Iy.Paint, img_Wely.Paint,
    img_Wply.Paint, img_iy2.Paint, img_Avz.Paint, img_Iz.Paint, img_Welz.Paint,
    img_Wplz.Paint, img_iz2.Paint, img_It.Paint
        '----------------------------------------------------------------------------------------
        '   24/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------
        '   Affiche les symboles de propriétés
        '----------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------

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
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Centre
        Const kADJ As Single = 0.9

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = False
        Select Case sender.name

            'Case Me.img_A.Name          ' Aire
            '    strSymbol = "A"
            '    strIndice = "g"

            Case Me.img_Iy.Name         ' Inertie / axe fort
                strSymbol = "I"
                strIndice = "y"
            Case Me.img_Wely.Name       ' Module élastique / axe fort
                strSymbol = "W"
                strIndice = "el,y"
            Case Me.img_Wply.Name       ' Module plastique / axe fort
                strSymbol = "W"
                strIndice = "pl,y"
            Case Me.img_iy2.Name        ' rayon de giration / axe fort
                strSymbol = "i"
                strIndice = "y"

            Case Me.img_Avz.Name        ' Aire de cisaillement
                strSymbol = "A"
                strIndice = "v"

            Case Me.img_Iz.Name         ' Inertie / axe faible
                strSymbol = "I"
                strIndice = "z"
            Case Me.img_Welz.Name       ' Module élastique / axe faible
                strSymbol = "W"
                strIndice = "el,z"
            Case Me.img_Wplz.Name       ' Module plastique / axe faible
                strSymbol = "W"
                strIndice = "pl,z"
            Case Me.img_iz2.Name        ' rayon de giration / axe faible
                strSymbol = "i"
                strIndice = "z"

            Case Me.img_It.Name         ' Inertie de torsion
                strSymbol = "I"
                strIndice = "t"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, kADJ, lEgal)

    End Sub



#End Region

#Region "   Edition Note catalogue "

    Private Sub btn_Note_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Note.Click
        '------------------------------------------------------------------------------------------------------------
        ' BD - 13/09/18
        '
        ' Création de la note du Catalogue avec toutes les informations
        '
        '------------------------------------------------------------------------------------------------------------

        '--> Déclaration + Recup des valeurs pour envoyer vers la note
        'Dim Proprietes As List(Of String)

        Dim ValDimensions As New List(Of Double)
        Dim ValProprietes As New List(Of String)
        Dim indRow As Integer = Me.Grid_ProfilesSup.SelectedCells(0).RowIndex
        Dim Gamme As String = Me.lst_Gamme.Text
        Dim Profile As String = Me.Grid_ProfilesSup(0, indRow).Value.ToString

        '--> Traitement

        ValDimensions.Add(MyCatalogue.Series(Gamme).Profiles(Profile).Ht)
        ValDimensions.Add(MyCatalogue.Series(Gamme).Profiles(Profile).Bf)
        ValDimensions.Add(MyCatalogue.Series(Gamme).Profiles(Profile).Tf)
        ValDimensions.Add(MyCatalogue.Series(Gamme).Profiles(Profile).Tw)
        ValDimensions.Add(MyCatalogue.Series(Gamme).Profiles(Profile).Rc)

        ValProprietes.Add(lbl_val_G.Text)
        ValProprietes.Add(lbl_val_A.Text)
        ValProprietes.Add(lbl_val_Iy.Text)
        ValProprietes.Add(lbl_val_Wely.Text)
        ValProprietes.Add(lbl_val_Wply.Text)
        ValProprietes.Add(lbl_val_iy2.Text)
        ValProprietes.Add(lbl_val_Avz.Text)
        ValProprietes.Add(lbl_val_Iz.Text)
        ValProprietes.Add(lbl_val_Welz.Text)
        ValProprietes.Add(lbl_val_Wplz.Text)
        ValProprietes.Add(lbl_val_iz2.Text)
        ValProprietes.Add(lbl_val_It.Text)

        AAA_EditionCATALOGUE(True, MyPoutreLoc.Section.ProfilA)


    End Sub

#End Region

#Region " POUBELLE "

    'Private Sub img_CM_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) _
    '    Handles img_AireProfile.Paint, img_AireCisaillementProfile.Paint,
    '    img_InertieYYProfiles.Paint, img_InertieZZProfile.Paint, img_InertieTorsionProfile.Paint,
    '    img_ModuleElastiqueYYProfile.Paint, img_ModulePlastiqueYYProfile.Paint, img_ModuleElastiqueZZProfile.Paint, img_ModulePlastiqueZZProfile.Paint
    '    '----------------------------------------------------------------------------------------
    '    '   10/09/18 :  Création - Version 1.00
    '    '----------------------------------------------------------------------------------------
    '    '   Associe l'image à la chaine puis lance la méthode pour dessiner
    '    '----------------------------------------------------------------------------------------
    '    Dim nb As String

    '    If sender.Equals(img_AireProfile) Or sender.Equals(img_AireCisaillementProfile) Then
    '        nb = "2"
    '        DrawCM(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, nb)
    '    ElseIf sender.Equals(img_InertieYYProfiles) Or sender.Equals(img_InertieZZProfile) Or sender.Equals(img_InertieTorsionProfile) Then
    '        nb = "4"
    '        DrawCM(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, nb)
    '    ElseIf sender.Equals(img_ModuleElastiqueYYProfile) Or sender.Equals(img_ModulePlastiqueYYProfile) _
    '    Or sender.Equals(img_ModuleElastiqueZZProfile) Or sender.Equals(img_ModulePlastiqueZZProfile) Then
    '        nb = "3"
    '        DrawCM(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, nb)
    '    End If
    'End Sub

    'Private Sub DrawCM(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single, ByVal nb As String)
    '    '----------------------------------------------------------------------------------------
    '    '   07/09/18 :  Création - Version 1.00
    '    '----------------------------------------------------------------------------------------
    '    '   Dessine cm^2 ou cm^3 ou cm^4
    '    '----------------------------------------------------------------------------------------
    '    Dim Chaine As String
    '    Dim xPen, yPen As Single
    '    Dim sCar, xDec, hDec, xDepart As Single
    '    Dim FontNormal As New Font(Me.lbl_A.Font.Name, 8.25)
    '    Dim FontExp As New Font(Me.lbl_A.Font.Name, 6.25)
    '    Const kMatch As Single = 0.97

    '    Chaine = "cm"
    '    sCar = MyGr.MeasureString(Chaine, FontNormal).Height
    '    xDepart = MyGr.MeasureString(Chaine + nb, FontNormal).Width '-->largeur de chaine + exp
    '    xDec = MyGr.MeasureString(Chaine, FontNormal).Width

    '    yPen = (sHI - sCar) / 2
    '    xPen = (sWI - xDepart) / 2 '-->Centrer horizontalement
    '    MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

    '    xPen += kMatch * xDec
    '    hDec = sCar / 4

    '    MyGr.DrawString(nb, FontExp, Brushes.Black, xPen, yPen - hDec)

    '    FontNormal.Dispose()
    '    FontExp.Dispose()
    'End Sub

    'Private Sub img_Indice_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Iy.Paint, img_Wely.Paint,
    '    img_Wply.Paint, img_iy2.Paint, img_Avz.Paint, img_Iz.Paint, img_Welz.Paint,
    '    img_Wplz.Paint, img_iz2.Paint, img_It.Paint, img_Ht.Paint, img_Bf.Paint,
    '    img_Tf.Paint, img_Tw.Paint, img_Rc.Paint
    '    '----------------------------------------------------------------------------------------
    '    '   10/09/18 :  Création - Version 1.00
    '    '----------------------------------------------------------------------------------------
    '    '   Associe l'image à la chaine puis lance la méthode pour dessiner
    '    '----------------------------------------------------------------------------------------

    '    Dim chaine As String
    '    Dim exp As String

    '    '--> Propriétés 
    '    If sender.Equals(img_Iy) Then
    '        chaine = "I"
    '        exp = "y"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Wely) Then
    '        chaine = "W"
    '        exp = "el.y"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Wply) Then
    '        chaine = "W"
    '        exp = "pl.y"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_iy2) Then
    '        chaine = "i"
    '        exp = "y"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Avz) Then
    '        chaine = "A"
    '        exp = "vz"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Iz) Then
    '        chaine = "I"
    '        exp = "z"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Welz) Then
    '        chaine = "W"
    '        exp = "el.z"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Wplz) Then
    '        chaine = "W"
    '        exp = "pl.z"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_iz2) Then
    '        chaine = "i"
    '        exp = "z"
    '        DrawIndice(e.Graphics, Me.img_AireProfile.ClientRectangle.Width, Me.img_AireProfile.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_It) Then
    '        chaine = "I"
    '        exp = "t"
    '        DrawIndice(e.Graphics, Me.img_Ht.ClientRectangle.Width, Me.img_Ht.ClientRectangle.Height, chaine, exp)

    '        '--> Dimensions
    '    ElseIf sender.Equals(img_Ht) Then
    '        chaine = "h"
    '        exp = "t"
    '        DrawIndice(e.Graphics, Me.img_Ht.ClientRectangle.Width, Me.img_Ht.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Bf) Then
    '        chaine = "b"
    '        exp = "f"
    '        DrawIndice(e.Graphics, Me.img_Ht.ClientRectangle.Width, Me.img_Ht.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Tf) Then
    '        chaine = "t"
    '        exp = "f"
    '        DrawIndice(e.Graphics, Me.img_Ht.ClientRectangle.Width, Me.img_Ht.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Tw) Then
    '        chaine = "t"
    '        exp = "w"
    '        DrawIndice(e.Graphics, Me.img_Ht.ClientRectangle.Width, Me.img_Ht.ClientRectangle.Height, chaine, exp)
    '    ElseIf sender.Equals(img_Rc) Then
    '        chaine = "r"
    '        exp = "c"
    '        DrawIndice(e.Graphics, Me.img_Ht.ClientRectangle.Width, Me.img_Ht.ClientRectangle.Height, chaine, exp)
    '    End If
    'End Sub

    'Private Sub DrawIndice(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single, ByVal Chaine As String, ByVal exp As String)
    '    '----------------------------------------------------------------------------------------
    '    '   10/09/18 :  Création - Version 1.00
    '    '----------------------------------------------------------------------------------------
    '    '   Dessine la chaine avec l'indice donné
    '    '----------------------------------------------------------------------------------------

    '    Dim xPen, yPen As Single
    '    Dim sCar, xDec, hDec, xDepart As Single
    '    Dim FontNormal As New Font(Me.lbl_A.Font.Name, 8.25) '--> taille d'écriture de la chaine
    '    Dim FontExp As New Font(Me.lbl_A.Font.Name, 7.25) '--> taille d'écriture de l'exposant
    '    Const kMatch As Single = 0.97

    '    sCar = MyGr.MeasureString(Chaine, FontNormal).Height '-->hauteur de l'écriture
    '    xDepart = MyGr.MeasureString(Chaine + exp, FontNormal).Width '-->largeur de chaine + exp
    '    xDec = MyGr.MeasureString(Chaine, FontNormal).Width '-->largeur de chaine

    '    yPen = (sHI - sCar) / 2 '-->Centrer verticalement
    '    xPen = (sWI - xDepart) / 2 '-->Centrer horizontalement
    '    MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

    '    '--> Décalage pour écrire l'indice en bas à droite
    '    xPen += kMatch * xDec
    '    hDec = sCar / 4
    '    MyGr.DrawString(exp, FontExp, Brushes.Black, xPen, yPen + hDec)

    '    FontNormal.Dispose()
    '    FontExp.Dispose()
    'End Sub

#End Region

End Class