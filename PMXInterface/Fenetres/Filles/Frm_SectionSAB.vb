Imports System.IO
Imports PMXMoteur2

Public Class Frm_SectionSAB
#Region " Constantes et structures "
    Const STARCHAR As String = "*"
    Const RATIOHIGAMME As Double = 0.45
    Dim ColorGridHI As Color = Color.Blue
    Const EPSILONG As Double = 0.0001
    Const iFRMSECTION As Integer = 4

    Dim HSLIMMAX As Decimal = OptionsSlimFloor.hslimmax 'limite le choix des profilés à ceux dont la hauteur et inférieure ou égale à 650 mm
    Const BFMAXI As Decimal = 0.5
    Const EPMAXI As Decimal = 0.5
    Dim BAPPMIN As Decimal = OptionsSlimFloor.bappmin
    Dim TPINFMIN As Decimal = OptionsSlimFloor.tpinfmin
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
    'Dim myHauteurHw As Decimal

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
    'Dim str_InfoH(1) As String

    '---- Gestion du cas où aucun profilé n'est disponible pour une série
    Dim lAvailPro As Boolean = True

    '---- Nuance S235/S275 autorisée (V3.09)
    Dim lNuancePossible As Boolean
    Dim NuancesExclues() As String = {"S235", "S275"}

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_SectionSectionSFB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
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
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SABSECTIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ===============================================================

                Me.Text = Bloc("TITLE")
                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("OK")

                '=== PROFILE ===============================================================

                Me.lbl_ParentProfile.Text = Bloc("PROFILE")
                Me.lbl_Gamme.Text = Bloc("SERIAL")
                Me.lbl_Profiles.Text = Bloc("PROFILE")


                Me.lbl_SABSection.Text = Bloc("SABSECTION")
                Me.lbl_Width.Text = Bloc("WIDTH")


                '=== STEEL ===============================================================

                Me.lbl_Acier.Text = Bloc("STEEL")
                Me.lbl_Grade.Text = Bloc("STEELGRADE")
                Me.lbl_Qualite.Text = Bloc("QUALITY")
                Me.lbl_ReductionCurve.Text = Bloc("REDUCTIONCURVE")

                '=== CHAINES =============================================================
                strDeliveryConditions = Bloc("DELIVERYCOND")

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

        Dim Ratio1, Ratio2 As Single

        Me.img_Section.Dock = DockStyle.Fill
        Me.img_ReductionCurve.Dock = DockStyle.Fill

        '== Preparation des options disponibles en fonctions du maitre d'ouvrage

        Select Case LogicielInfo.Maitre
            Case EnuMaitre.ArcelorMittal
                If Not LogicielOptions.lExpert Then Me.TLpan_Gauche.RowStyles(1).Height = 0
            Case EnuMaitre.CTICM
                Me.GridDelivery.Visible = True ' LogicielOptions.lExpert
        End Select

        '== Transfert vers variable locale

        cls_Section.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Section, MySectionLoc)

        BClrCompatible = Me.lst_GammeS.BackColor

        '== Préparation des listes

        RemplirSeries()
        InitialiseLngDeliveryIndex()

        Ratio1 = Me.lbl_Grade.Width / (Me.lbl_Grade.Width + Me.lbl_Qualite.Width + Me.lbl_ReductionCurve.Width)
        Ratio2 = Me.lbl_Qualite.Width / (Me.lbl_Grade.Width + Me.lbl_Qualite.Width + Me.lbl_ReductionCurve.Width)

        PrepareLookGrille(Me.Grid_ProfilesSup, Me.Col_HISTARSup, Me.Col_ListeSup, Me.lst_GammeS.BackColor, RATIOHIGAMME)
        PrepareLookGrille(Me.GridAciers, Me.Col_Grade, Me.Col_Qualite, Me.Col_ReductionCurve, Me.lst_GammeS.BackColor, Ratio1, Ratio2)
        PrepareLookGrille(Me.GridDelivery, Me.Col_Index, Me.Col_Message, Me.lst_GammeS.BackColor, 0.1)
        PrepareGridDelivery()

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1SAB.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_ParentProfile.BackColor = CouleurBackBandeaux
        Me.lbl_ParentProfile.ForeColor = CouleurForeBandeaux

        Me.lbl_SABSection.BackColor = CouleurBackBandeaux
        Me.lbl_SABSection.ForeColor = CouleurForeBandeaux

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherPoutreEnCours()

        AfficherProfileLamineEnCours()
        AfficherSemelleSuperieureEnCours()
    End Sub

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

            RemplirDelivery(MyGam, MyProf)

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
            Else 'on selectionne la dernière ligne par défaut 
                Me.GridAciers(0, Me.GridAciers.Rows.Count - 1).Selected = True
            End If
            GetAcierFromGrid()
        Else

        End If


    End Sub

    Private Sub AfficherSemelleSuperieureEnCours()
        Me.txt_bfs.Text = GetStringInUnit(MySectionLoc.ProfilA.Bfs, Enu_TypeVariable.Dimension, 4, 1, False)
    End Sub

    Private Sub RemplirSeries()

        '--> Remplissage des series

        Me.lst_GammeS.Items.Clear()
        For Each kvp As KeyValuePair(Of String, StrucGamme) In MyCatalogue.Series
            Me.lst_GammeS.Items.Add(kvp.Key)
        Next

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
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        'If MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.typeProfileAcier <> MySectionLoc.ProfilA.typeProfileAcier Then
        '    MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.typeProfileAcier = MySectionLoc.ProfilA.typeProfileAcier
        '    lModif = True
        'End If

        GereTransfertValeur(MySectionLoc.ProfilA.ha, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.ha, lModif)
        GereTransfertValeur(MySectionLoc.ProfilA.hb, MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.hb, lModif)
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

            MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Plages.Clear()
            Dim MyPlage As cls_Acier.strucPlage
            Dim Nuance, Qualite, Reduction As String

            Nuance = MySectionLoc.Acier.Nuance
            Qualite = MySectionLoc.Acier.Qualite
            Reduction = MySectionLoc.Acier.Reduction

            For i As Integer = 0 To SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages.Count - 1
                MyPlage.Ep = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Ep
                MyPlage.Fy = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fy
                MyPlage.Fu = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fu
                MyProjet.Poutres(MyProjet.IndEnCours).Section.Acier.Plages.Add(MyPlage)
            Next
        End If

        Dim lAcierModifieSPD As Boolean = False

        GereTransfertValeur(MySectionLoc.AcierSPD.Nuance, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierSPD.Nuance, lAcierModifieSPD)
        GereTransfertValeur(MySectionLoc.AcierSPD.Qualite, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierSPD.Qualite, lAcierModifieSPD)
        GereTransfertValeur(MySectionLoc.AcierSPD.NormeProduit, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierSPD.NormeProduit, lAcierModifieSPD)
        GereTransfertValeur(MySectionLoc.AcierSPD.Reduction, MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierSPD.Reduction, lAcierModifieSPD)

        If lAcierModifieSPD Then
            lModif = True

            MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierSPD.Plages.Clear()
            Dim MyPlageSPD As cls_Acier.strucPlage
            Dim NuanceSPD, QualiteSPD, ReductionSPD As String

            NuanceSPD = MySectionLoc.AcierSPD.Nuance
            QualiteSPD = MySectionLoc.AcierSPD.Qualite
            ReductionSPD = MySectionLoc.AcierSPD.Reduction

            For i As Integer = 0 To SteelBase.Grades(NuanceSPD).Qualites(QualiteSPD).ReductionCurv(ReductionSPD).Plages.Count - 1
                MyPlageSPD.Ep = SteelBase.Grades(NuanceSPD).Qualites(QualiteSPD).ReductionCurv(ReductionSPD).Plages(i).Ep
                MyPlageSPD.Fy = SteelBase.Grades(NuanceSPD).Qualites(QualiteSPD).ReductionCurv(ReductionSPD).Plages(i).Fy
                MyPlageSPD.Fu = SteelBase.Grades(NuanceSPD).Qualites(QualiteSPD).ReductionCurv(ReductionSPD).Plages(i).Fu
                MyProjet.Poutres(MyProjet.IndEnCours).Section.AcierSPD.Plages.Add(MyPlageSPD)
            Next
        End If

        With MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA ' --> Sécurité supplémentaire pour s'assurer que les valeurs qui n'ont pas de sens restent égales à 0
            Select Case .typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    .hb = .ha
                    .aW = 0
                    .Plat_b = 0
                    .Plat_t = 0
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



#End Region

#Region " DESSINS "

    Private Sub img_Section_Paint(sender As Object, e As PaintEventArgs) Handles img_Section.Paint

        DessinProfileAcier(e.Graphics, MySectionLoc, Me.img_Section.ClientRectangle.Width, Me.img_Section.ClientRectangle.Height,
                           FontBase, kAdjust, True, False, iSelect)

    End Sub

    Private Sub img_ReductionCurve_Paint(sender As Object, e As PaintEventArgs) Handles img_ReductionCurve.Paint

        DessinPropAcier(e.Graphics, Me.img_ReductionCurve.ClientRectangle.Height, Me.img_ReductionCurve.ClientRectangle.Width, True)

    End Sub

    Sub DessinPropAcier(ByVal MyGr As Graphics, ByVal sHI As Single, ByVal sWI As Single, ByVal lNuanceOK As Boolean)
        '----------------------------------------------------------------------------------------------
        '
        '   23/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Affichage graphique de la section dans la fenêtre
        '
        '----------------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   sHI, sWI        [E] :   Dimensions du PictureBox
        '   lNuanceOK       [E] :   Indique si l'utilisateur en mode normal peut sélectionner cette nuance
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
        'Dim EpPlatSoude, FyPlatSoude As Double

        Dim ColorPen As Color = Color.Black
        Dim ColorExclu As Color = ColorNotPossible
        Dim ColorNormal As Color
        Dim ColorSelect As Color

        Dim MyPen As Pen 'New Pen(ColorPen)
        Dim MyBrush As Brush 'New SolidBrush(ColorPen)
        Dim MyFont As New Font(FontBase.Name, 8, FontStyle.Bold)
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

        EpProfile = Math.Max(MySectionLoc.ProfilA.Tw, MySectionLoc.ProfilA.Tfs)
        FyPro = MySectionLoc.Acier.LimiteFy(EpProfile)

        EpPlagesMax = MySectionLoc.Acier.EpMax

        '--( Initialisation

        If Me.GridAciers.Rows.Count <= 0 Then Exit Sub

        iSteel = Me.GridAciers.SelectedCells(0).RowIndex
        Nuance = Me.GridAciers(0, iSteel).Value.ToString
        Qualite = Me.GridAciers(1, iSteel).Value.ToString
        Norme = Me.GridAciers(2, iSteel).Value.ToString

        ExtraitValeursEnveloppeAciers(Nuance, DrawProperty, EpMin, EpMax, VMax)

        EpMax = Math.Max(EpMax, EpProfile)

        kFact = EpMax / VMax * sHI / sWI
        xMin = 0
        xMax = EpMax
        yMin = 0
        yMax = VMax * kFact

        ParametresAffichage(RCParAff, xMin, yMin, xMax - xMin, yMax - yMin, sWI, sHI)

        Dim ChaineFy As String = "fy (MPa)"
        Dim hBoni As Single = MyGr.MeasureString(ChaineFy, MyFont).Height
        Dim ChaineT As String = "t (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")"
        Dim wBoni As Single = MyGr.MeasureString(ChaineT, MyFont).Width

        yMax += hBoni / RCParAff.CRed
        xMax += wBoni / RCParAff.CRed
        yMin -= 2 * hBoni / RCParAff.CRed
        'xMin -= wBoni / RCParAff.CRed

        ParametresAffichage(RCParAff, xMin, yMin, xMax - xMin, yMax - yMin, sWI, sHI)

        '--( Dessin des axes

        AddFleche(MyGr, MyPen, 0, 0, xMax, 0, RCParAff, False, True)
        AddFleche(MyGr, MyPen, 0, 0, 0, yMax, RCParAff, False, True)

        AddTexte(MyGr, MyBrush, ChaineFy, MyFont, 0, yMax, RCParAff, HorizontalAlignment.Right, VerticalAlignement.Middle)
        AddTexte(MyGr, MyBrush, ChaineT, MyFont, xMax, 0, RCParAff, HorizontalAlignment.Left, VerticalAlignement.Bottom)

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
                    DrawReductionCurve(MyGr, RCParAff, kFact, lSelect, kvpSteel.Value.EpMax, kvpSteel.Value.Plages, MyColor, iEp, lNuanceOK)
                End If
            Next

        Next

        '--( Représentation de la position du profilé dans la courbe de réduction

        zBoni = YUnivers(RCParAff, sHI)
        xBoni = XUnivers(RCParAff, sWI / 2)

        DrawEpEtFyCalcul(MyGr, RCParAff, kFact, EpPlagesMax, EpProfile, FyPro, xBoni, zBoni, MyFont, lNuanceOK)

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

        MyFont.Dispose()
        MyPen.Dispose()
        MyBrush.Dispose()
        MyFontTitre.Dispose()
        MyBrushTitre.Dispose()
    End Sub

    Private Sub DrawReductionCurve(ByVal MyGr As Graphics, ByVal RcParAff As Struc_Affichage, ByVal kFact As Double, ByVal lSelect As Boolean,
                                   ByVal EpMax As Double, ByVal Plages As List(Of cls_Acier.strucPlage), ByVal MyColor As Color, ByVal iEp As Integer, ByVal lNuanceOK As Boolean)
        '----------------------------------------------------------------------------------------------
        '
        '   21/09/12 :  Création - Version 3.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Affichage de l'épaisseur max et de fy calcul
        '
        '----------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   RcParAff    [E] :   Paramètre d'affichage
        '   kFact       [E] :   Facteur d'affichage des valeurs fy
        '
        '----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbPlages As Integer
        Dim Chaine As String

        Dim MyPen As New Pen(MyColor, iEp)
        Dim MyPenBlack As Pen 'New Pen(Color.Black, 0.75)
        Dim MyColorBlack As Color

        '--> Initialisation

        NbPlages = Plages.Count
        If lNuanceOK Then
            MyPenBlack = New Pen(Color.Black)
            MyColorBlack = Color.Black
        Else
            MyPenBlack = New Pen(ColorNotPossible)
            MyColorBlack = ColorNotPossible
        End If

        '--> Traitement

        If lSelect Then

            For i As Integer = 0 To NbPlages - 2

                AddLigne(MyGr, MyPen, Plages(i).Ep, kFact * Plages(i).Fy, Plages(i + 1).Ep, kFact * Plages(i).Fy, RcParAff)
                AddLigne(MyGr, MyPenBlack, Plages(i + 1).Ep, kFact * Plages(i).Fy, Plages(i + 1).Ep, kFact * Plages(i + 1).Fy, RcParAff)

            Next
            AddLigne(MyGr, MyPen, Plages(NbPlages - 1).Ep, kFact * Plages(NbPlages - 1).Fy, EpMax, kFact * Plages(NbPlages - 1).Fy, RcParAff)


            For i As Integer = 0 To NbPlages - 1

                AddLigne(MyGr, MyPenBlack, Plages(i).Ep, 0, Plages(i).Ep, kFact * Plages(i).Fy, RcParAff)

            Next
            AddLigne(MyGr, MyPenBlack, EpMax, 0, EpMax, kFact * Plages(NbPlages - 1).Fy, RcParAff)

        End If

        '--> Cotation

        If lSelect Then
            For i As Integer = 0 To NbPlages - 2
                'Chaine = GetStringInUnit(Plages(i).Fy, Enu_TypeVariable.Contrainte, 3, 0, False)
                Chaine = GetStringInUnit(Plages(i).Fy, Enu_TypeVariable.SansType, 3, 0, False)
                AddTexte(MyGr, New SolidBrush(MyColor), Chaine, FontBase, (Plages(i).Ep + Plages(i + 1).Ep) / 2, kFact * Plages(i).Fy, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            Next
            Chaine = GetStringInUnit(Plages(NbPlages - 1).Fy, Enu_TypeVariable.SansType, 3, 0, False)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, FontBase, (EpMax + Plages(NbPlages - 1).Ep) / 2, kFact * Plages(NbPlages - 1).Fy, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            For i As Integer = 0 To NbPlages - 1
                Chaine = GetStringInUnit(Plages(i).Ep, Enu_TypeVariable.Dimension, 3, 0, False)
                AddTexte(MyGr, New SolidBrush(MyColorBlack), Chaine, FontBase, Plages(i).Ep, 0, RcParAff, HorizontalAlignment.Right, VerticalAlignement.Top)
            Next
            Chaine = GetStringInUnit(EpMax, Enu_TypeVariable.Dimension, 3, 0, False)
            AddTexte(MyGr, New SolidBrush(MyColorBlack), Chaine, FontBase, EpMax, 0, RcParAff, HorizontalAlignment.Right, VerticalAlignement.Top)
        End If

    End Sub

    Private Sub DrawEpEtFyCalcul(ByVal MyGr As Graphics, ByVal RcParAff As Struc_Affichage, ByVal kFact As Double,
                                 ByVal EpPlagesMax As Double, ByVal EpProf As Double, ByVal FyCalcul As Double, ByVal xBoni As Double, ByVal zBoni As Double,
                                 ByVal MyFont As Font, ByVal lNuanceOK As Boolean, Optional ByVal lLegende As Boolean = True, Optional ByVal lPRS As Boolean = False)
        '----------------------------------------------------------------------------------------------
        '
        '   21/09/12 :  Création - Version 3.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Affichage de l'épaisseur max et de fy calcul
        '
        '----------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   RcParAff    [E] :   Paramètre d'affichage
        '   kFact       [E] :   Facteur d'affichage des valeurs fy
        '   
        '   EpPlagesMax [E] :   Epaisseur maximale des plages de la courbe de réduction'
        '   EpProf      [E] :   Epaisseur de profilé pris en compte pour les calcul
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

        '--> Traitement

        If lLegende Then
            Chaine = GetStringInUnit(EpProf, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, MyFont, EpProf, 0, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
        End If

        AddLigne(MyGr, MyPenProf, EpProf, 0, EpProf, kFact * FyCalcul, RcParAff)
        If EpProf > EpPlagesMax * (1 + EPSILONG) Then
            AddLigne(MyGr, MyPenProf, EpPlagesMax, kFact * FyCalcul, EpProf, kFact * FyCalcul, RcParAff)
        End If

        If lLegende Then
            If lPRS Then
                Chaine = "tmax = " & GetStringInUnit(EpProf, Enu_TypeVariable.Dimension, 3, 1, True) & "   -  fy,min = " & GetStringInUnit(FyCalcul, Enu_TypeVariable.SansType, 3, 0, False) & " MPa"
            Else
                Chaine = "t = " & GetStringInUnit(EpProf, Enu_TypeVariable.Dimension, 3, 1, True) & "   -  fy = " & GetStringInUnit(FyCalcul, Enu_TypeVariable.SansType, 3, 0, False) & " MPa"
            End If
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, MyFont, xBoni, zBoni, RcParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
        End If

        MyPen.Dispose()
        MyPenProf.Dispose()

    End Sub


#End Region

#Region " Dessin symboles "

    Private Sub img_Symbol_Paint(sender As Object, e As PaintEventArgs) Handles img_bfs.Paint

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

            Case Me.img_bfs.Name
                strSymbol = "b"
                strIndice = "fs"
        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolNormal, FontSymbolIndice, 1.0!, True)

    End Sub

#End Region

#Region " Evènements "

    Private Sub SaisieDimensions(sender As Object, e As EventArgs) Handles txt_bfs.TextChanged
        If lBuild Then Exit Sub
        lBuild = True

        Dim Valeur As Decimal
        Dim lMAJHauteur As Boolean = False

        If VerificationDonnees(sender, Valeur) Then
            Select Case sender.name
                Case Me.txt_bfs.Name
                    MySectionLoc.ProfilA.Bfs = Valeur
            End Select
        End If

        Me.img_Section.Invalidate()
        Me.img_ReductionCurve.Invalidate()

        lBuild = False
    End Sub

    ''' <summary>
    ''' Vérifie la saisie des données en cours, pour les dimensions des PRS
    ''' </summary>
    Private Function VerificationDonnees(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '--> Déclaration
        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
        Dim BFmin, BFmax As Decimal

        '--> Initialisation
        BFmin = 10 * MySectionLoc.ProfilA.Tfs

        If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
            BFmax = MySectionLoc.ProfilA.Bfi - 2 * BAPPMIN
        Else
            BFmax = MySectionLoc.ProfilA.Bfi - BAPPMIN
        End If
        BFmax = Math.Min(BFmax, BFMAXI)

        Select Case MyTxt.Name
            Case Me.txt_bfs.Name
                ValMin = BFmin
                ValMax = BFmax
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

    Private Sub ChoixTypeProfile(sender As Object, e As EventArgs)
        If lBuild Then Exit Sub

        If sender.checked = False Then Exit Sub 'Permet d'éviter une boucle infinie

        AfficherPoutreEnCours()
        Me.img_Section.Invalidate()
        Me.img_ReductionCurve.Invalidate()


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

            'MAJ_AvailablePro(False)

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

        'AfficherSemelleSuperieureEnCours()
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

        MySectionLoc.ProfilA.hb = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
        MySectionLoc.ProfilA.ha = MySectionLoc.ProfilA.hb
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

        MAJ_GridAcier(Serie, Profile, OptionsDatabase.ChoiceSteel, lPRS)

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

    'Private Sub MAJ_AvailablePro(ByVal lCustom As Boolean)

    '    'Me.etq_NoAvailablePro.Visible = Not lAvailPro

    '    'If Not lCustom Then
    '    '    Me.Grid_ProfilesSup.Visible = lAvailPro

    '    '    Me.grp_DonneesFinales.Visible = lAvailPro
    '    '    Me.GridDelivery.Visible = lAvailPro
    '    '    Me.etq_Delivery.Visible = lAvailPro

    '    '    Me.etq_Grade.Visible = lAvailPro
    '    '    Me.etq_Qualite.Visible = lAvailPro
    '    '    Me.etq_ReductionCurve.Visible = lAvailPro
    '    '    Me.GridAciers.Visible = lAvailPro
    '    '    Me.img_ReductionCurve.Visible = lAvailPro
    '    'End If

    'End Sub

#End Region

#Region "   Remplissage des Profiles en fonction de la Gamme "

    Sub RemplissageGrilleProfile(ByVal MyGrille As DataGridView, ByVal Gamme As String, ByRef NbProGrille As Integer)
        '-----------------------------------------------------------------------------------
        '
        '   Remplissage d'une grille avec tous les profilés d'une gamme
        '   Note GUD: ici, on filtre les profilés dont la hauteur est supérieure à 650 mm
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

            lAffiche = (lSoftProfile Or Not OptionsDatabase.lSoftLimited) And EstCompatibleABCPMX(kVs.Value)

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

    Private Function EstCompatibleABCPMX(ByVal Profile As Cls_SectionNew) As Boolean
        '---------------------------------------------------------------------------------------------------------
        '
        '   02/04/24 :  Création - GUD - V1.00
        '
        '---------------------------------------------------------------------------------------------------------
        '
        '   Indique si un profile vérifie les conditions pour être affiché
        '
        '---------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------


        '-- Déclarations

        Dim lCompatible As Boolean = True

        'If Profile.Ht > HSLIMMAX Then lCompatible = False

        Return lCompatible

    End Function

#End Region

#Region "   Gestion grille des aciers "

    Private Sub GridAciers_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridAciers.SelectionChanged

        If lBuild Then Exit Sub
        If Me.GridAciers.Rows.Count = 0 Then Exit Sub

        GetAcierFromGrid()

        MAJNuancesPossibles(MySectionLoc.Acier)

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

        TransfertGridAcier(Nuance, Qualite, Norme, MySectionLoc.Acier)

    End Sub

    Private Sub TransfertGridAcier(ByVal Nuance As String, ByVal Qualite As String, ByVal Reduction As String, ByRef AcierLoc As cls_Acier)

        AcierLoc.Nuance = Nuance
        AcierLoc.Qualite = Qualite
        AcierLoc.Reduction = Reduction

        AcierLoc.EpMax = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).EpMax

        AcierLoc.iBase = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).iBase
        AcierLoc.iStandart = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).StIndex

        AcierLoc.Plages.Clear()
        Dim MyPlage As cls_Acier.strucPlage
        For i As Integer = 0 To SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages.Count - 1
            MyPlage.Ep = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Ep
            MyPlage.Fy = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fy
            MyPlage.Fu = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fu
            AcierLoc.Plages.Add(MyPlage)
        Next

        Dim iStd As Integer

        iStd = SteelBase.IndexStd.IndexOf(SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).StIndex)
        If iStd > -1 Then
            AcierLoc.NormeProduit = SteelBase.NormeStd(iStd)
            AcierLoc.iTabStandart = iStd
        End If
    End Sub

    Private Sub MAJNuancesPossibles(ByVal AcierLoc As cls_Acier)
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

        MyNuance = AcierLoc.Nuance

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

        If LogicielOptions.lNoS235 Then
            Do While (Not lTrouve) And (iGrad < NuancesExclues.GetUpperBound(0))
                iGrad += 1
                lTrouve = (MyNuance = NuancesExclues(iGrad))
            Loop
        End If

        Return (Not lTrouve)
    End Function

    Private Sub MAJ_GridAcier(ByVal Serie As String, ByVal Profile As String, ByVal ChoiceSteel As EnuChoiceAcier, Optional lPRS As Boolean = False)

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

                    If Not lPRS Then lDisplay = SteelIsToDisplay(Serie, Profile, kvpGrade.Key, kvpQualite.Key, kvpSteel.Key, ChoiceSteel, lIsNuanceCompatibleProfile)

                    If lDisplay Or lPRS Then
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

    Private Function SteelIsToDisplay(ByVal Serie As String, ByVal Profile As String,
                                      ByVal Nuance As String, ByVal Qualite As String, ByVal Norm As String, ByVal ChoiceAcier As EnuChoiceAcier,
                                      ByRef lIsNuanceCompatibleProfile As Boolean) As Boolean
        '------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   06/12/12 :  Création - POM - V3.00
        '
        '------------------------------------------------------------------------------------------------------------------------------------------
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

        Return SteelIsToCompatibleToProfile(EpMax, IndStd, SteelBase, MyCatalogue.CorIndStd, Nuance, Qualite, Norm, ChoiceAcier, lIsNuanceCompatibleProfile)

    End Function

    Private Sub DisplaySteelToScreen(ByVal Steels As List(Of strucAcierLocal), ByRef nbSteels As Integer, ByVal Choice As EnuChoiceAcier)
        '-----------------------------------------------------------------------------------------------------
        '
        '   20/12/12 :  Création - V3.01 - POM
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   Affichage à l'écran de la liste des aciers compatibles
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

        '--> Affichage

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

        '--> Affichage de la nuance en fonction de nouvelle nuance ou pas ?

        If lNewGrade Then
            Me.GridAciers(0, iRank - 1).Value = MySteel.Nuance
            Me.GridAciers(0, iRank - 1).Style.ForeColor = ColorGrade
        Else
            Me.GridAciers(0, iRank - 1).Value = MySteel.Nuance
            Me.GridAciers(0, iRank - 1).Style.ForeColor = Me.GridAciers.BackgroundColor
        End If

        '--> Affichage de la qualité

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

        '--> Affichage de la courbe de réduction

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

#End Region

End Class