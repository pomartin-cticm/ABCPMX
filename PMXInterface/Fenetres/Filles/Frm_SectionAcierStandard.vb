Imports System.IO
Imports PMXMoteur2

Public Class Frm_SectionAcierStandard

#Region " Constantes "
    Const STARCHAR As String = "*"
    Const RATIOHIGAMME As Double = 0.45
    Dim ColorGridHI As Color = Color.Blue

#End Region

#Region " Variables "
    Dim lBuild As Boolean = True

    Dim MySectionLoc As New cls_Section

    Enum Enu_DefinitionH
        HauteurTotale
        HauteurAme
    End Enum
    Dim DefinitionHauteur As Enu_DefinitionH = Enu_DefinitionH.HauteurTotale

    '--> Gestion des dessins
    Dim iSelect As Integer = -1
    Dim kAdjust As Decimal = 0.95

    '--> Textes
    Dim ILangueDelivery As Integer = 0
    Dim strDeliveryConditions As String
    Dim str_InfoH(1) As String

    '---- Gestion du cas où aucun profilé n'est disponible pour une série

    Dim lAvailPro As Boolean = True

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_SectionAcierStandard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
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
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SECTIONACIERSTANDARD")
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

                Me.lbl_Gamme.Text = Bloc("SERIE")
                Me.lbl_Profiles.Text = Bloc("PROFILE")


                Me.lbl_Height.Text = Bloc("HEIGHT")
                Me.lbl_SemelleInf.Text = Bloc("LOWERF")
                Me.lbl_SemelleSup.Text = Bloc("UPPERF")
                Me.lbl_Ame.Text = Bloc("WEB")


                '=== STEEL ===============================================================

                Me.lbl_Acier.Text = Bloc("STEEL")
                Me.lbl_Grade.Text = Bloc("STEELGRADE")
                Me.lbl_Qualite.Text = Bloc("QUALITY")
                Me.lbl_ReductionCurve.Text = Bloc("REDUCTIONCURVE")


                '=== CHAINES =============================================================

                strDeliveryConditions = Bloc("DELIVERYCOND")
                str_InfoH(0) = Bloc("AUTOMATICHW")
                str_InfoH(1) = Bloc("AUTOMATICHA")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

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

        Me.img_Section.Dock = DockStyle.Fill

        '== Preparation des options disponibles en fonctions du maitre d'ouvrage

        Select Case LogicielInfo.Maitre
            Case EnuMaitre.ArcelorMittal
                If Not LogicielOptions.lExpert Then Me.TLpan_Gauche.RowStyles(1).Height = 0
            Case EnuMaitre.CTICM
                Me.GridDelivery.Visible = True ' LogicielOptions.lExpert
        End Select

        '== Transfert vers variable locale

        cls_Section.CloneSection(MyProjet.Poutres(MyProjet.IndEnCours).Sections(1), MySectionLoc)

        '== Préparation des listes

        RemplirSeries()
        InitialiseLngDeliveryIndex()

        PrepareLookGrille(Me.Grid_ProfilesSup, Me.Col_HISTARSup, Me.Col_ListeSup, Me.lst_GammeS.BackColor, RATIOHIGAMME)
        PrepareLookGrille(Me.GridDelivery, Me.Col_Index, Me.Col_Message, Me.lst_GammeS.BackColor, 0.1)
        PrepareGridDelivery()

        '== Parametrage PRS

        Select Case DefinitionHauteur
            Case Enu_DefinitionH.HauteurAme : Me.chk_Hw.Checked = True
            Case Enu_DefinitionH.HauteurTotale : Me.chk_Ht.Checked = True
        End Select
        MAJ_DefinitionHauteur()

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.Etq_UnitDim7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Section.BackColor = CouleurBackBandeaux
        Me.lbl_Section.ForeColor = CouleurForeBandeaux

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        'Me.pan_Gauche.AutoScroll = False
        Me.TLpan_Gauche.Height = 470

    End Sub

    Private Sub AfficherPoutreEnCours()

        Select Case MySectionLoc.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                Me.rdb_Lamine.Checked = True
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                Me.rdb_PRS_symetrique.Checked = True
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                Me.rdb_PRS.Checked = True
        End Select

        MAJ_FonctionType()

        Me.txt_Ha.Text = GetStringNoUnit(MySectionLoc.ProfilA.ha, Enu_TypeVariable.Dimension)
        Me.txt_Hw.Text = GetStringNoUnit(MySectionLoc.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension)

        Me.txt_Bfi.Text = GetStringNoUnit(MySectionLoc.ProfilA.b_fi, Enu_TypeVariable.Dimension)
        Me.txt_Bfs.Text = GetStringNoUnit(MySectionLoc.ProfilA.b_fs, Enu_TypeVariable.Dimension)
        Me.txt_Tfi.Text = GetStringNoUnit(MySectionLoc.ProfilA.t_fi, Enu_TypeVariable.Dimension)
        Me.txt_Tfs.Text = GetStringNoUnit(MySectionLoc.ProfilA.t_fs, Enu_TypeVariable.Dimension)
        Me.txt_Tw.Text = GetStringNoUnit(MySectionLoc.ProfilA.t_w, Enu_TypeVariable.Dimension)

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

#Region "===FERMETURE==="

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If ValideSaisieFenetre() Then

            Dim lModif As Boolean = False

            TransfertSaisie(lModif)

            If lModif Then

            End If

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

    End Sub


#End Region

#Region " DESSINS "

    Private Sub img_Section_Paint(sender As Object, e As PaintEventArgs) Handles img_Section.Paint

        DessinProfileAcier(e.Graphics, MySectionLoc, Me.img_Section.ClientRectangle.Width, Me.img_Section.ClientRectangle.Height,
                           FontBase, kAdjust, True, False, iSelect)

    End Sub




    Private Sub img_Symbol_Paint(sender As Object, e As PaintEventArgs) Handles img_Tw.Paint, img_Tfs.Paint, img_Tfi.Paint, img_Hw.Paint, img_Ht.Paint, img_Bfs.Paint, img_Bfi.Paint

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

        '--> Initialisation

        lIndice = False
        lGrec = False

        Select Case sender.name

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
        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolNormal, FontSymbolIndice, 1.0!, True)

    End Sub

#End Region

#Region " Evènements "

    Private Sub SaisieDimensions(sender As Object, e As EventArgs) Handles txt_Tw.TextChanged, txt_Tfs.TextChanged, txt_Tfi.TextChanged, txt_Hw.TextChanged, txt_Ha.TextChanged, txt_Bfs.TextChanged, txt_Bfi.TextChanged

    End Sub

    Private Sub ChoixTypeProfile(sender As Object, e As EventArgs) Handles rdb_PRS_symetrique.CheckedChanged, rdb_PRS.CheckedChanged, rdb_Lamine.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.rdb_Lamine.Name
                MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            Case Me.rdb_PRS.Name
                MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
            Case Me.rdb_PRS_symetrique.Name
                MySectionLoc.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
        End Select

        MAJ_FonctionType()
        Me.img_Section.Invalidate()

    End Sub

    Private Sub MAJ_FonctionType()

        Select Case MySectionLoc.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                Me.TLpan_Gauche.RowStyles(3).Height = 0
                Me.TLpan_Gauche.RowStyles(2).Height = 290

            Case Else
                Me.TLpan_Gauche.RowStyles(2).Height = 0
                Me.TLpan_Gauche.RowStyles(3).Height = 200


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

                'MAJ_Aciers(Gamme, Profile)
                'SelectDefaultSteel(True)
                'GetAcierFromGrid()
                'MAJNuancesPossibles()
                '==R16-012
                RemplirDelivery(Gamme, Profile)

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

        Select Case DefinitionHauteur
            Case Enu_DefinitionH.HauteurTotale
                Me.txt_Ha.BackColor = SystemColors.Window
                Me.txt_Hw.BackColor = SystemColors.ControlDark
                Me.lbl_Info.Text = str_InfoH(0)
            Case Enu_DefinitionH.HauteurAme
                Me.txt_Hw.BackColor = SystemColors.Window
                Me.txt_Ha.BackColor = SystemColors.ControlDark
                Me.lbl_Info.Text = str_InfoH(1)
        End Select

        Me.txt_Ha.ReadOnly = (DefinitionHauteur = Enu_DefinitionH.HauteurAme)
        Me.txt_Hw.ReadOnly = (DefinitionHauteur = Enu_DefinitionH.HauteurTotale)

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

        'MAJ_Aciers(Gamme, Etiquette)
        'SelectDefaultSteel(False)
        'GetAcierFromGrid()

        RemplirDelivery(Gamme, Etiquette)

        'MAJ_DonneesFinales()
        Me.img_Section.Invalidate()

    End Sub

    Private Sub TransfertSaisieGridProfile(ByVal Gamme As String, ByVal Profile As String, ByRef MySection As cls_Section)

        MySectionLoc.ProfilA.Gamme = Gamme
        MySectionLoc.ProfilA.NomProfile = Profile

        MySectionLoc.ProfilA.ha = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
        MySectionLoc.ProfilA.b_fs = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MySectionLoc.ProfilA.t_fs = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MySectionLoc.ProfilA.t_w = MyCatalogue.Series(Gamme).Profiles(Profile).Tw
        MySectionLoc.ProfilA.r_cs = MyCatalogue.Series(Gamme).Profiles(Profile).Rc

        MySectionLoc.ProfilA.b_fi = MySectionLoc.ProfilA.b_fs
        MySectionLoc.ProfilA.r_ci = MySectionLoc.ProfilA.r_cs
        MySectionLoc.ProfilA.t_fi = MySectionLoc.ProfilA.t_fs

        'For i As Integer = 0 To MyCatalogue.nbStandard - 1
        '    MySection.iStandard(i) = MyCatalogue.Series(Gamme).Profiles(Profile).IndStandart(i)
        'Next
    End Sub

#End Region

#Region "   Conditions de livraison "

    Private Sub RemplirDelivery(ByVal Serie As String, ByVal Profile As String)

        Dim iRow As Integer = 0

        Me.lbl_Delivery.Text = RemplaceDollar(strDeliveryConditions, Profile)
        Me.GridDelivery.Rows.Clear()

        'GridDelivery.AutoResizeRow(iRow - 1)

        For i As Integer = 1 To MyCatalogue.nbDelivery

            If MyCatalogue.Series(Serie).Profiles(Profile).IndDeliv(i - 1) = 1 Then

                GridDelivery.Rows.Add()
                iRow += 1
                GridDelivery(0, iRow - 1).Value = CStr(iRow)
                GridDelivery(1, iRow - 1).Value = MyCatalogue.Delivery(i - 1)(ILangueDelivery)
                GridDelivery(1, iRow - 1).Selected = False
            End If

        Next

    End Sub

    Private Sub PrepareGridDelivery()

        GridDelivery.Columns(1).CellTemplate.Style.WrapMode = DataGridViewTriState.True
        GridDelivery.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

    End Sub

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

            lAffiche = (lSoftProfile Or Not OptionsDatabase_Section.lSoftLimited) And EstCompatibleACB(kVs.Value)

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

End Class