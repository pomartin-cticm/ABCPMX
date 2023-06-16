Imports PMXMoteur2
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class Frm_Dalle

#Region " Déclarations "

    Public Enum Enu_VariablesBac
        Aucune
        b1
        b2
        h
        hpg
        e
    End Enum

#End Region

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim strType(2) As String

    Dim ClasseBeton() As String = Cls_Beton.TabClasseBeton

    Dim MyDalleLoc As New Cls_Dalle
    Dim MyDicoBacs As New Dictionary(Of String, Cls_Bac)
    Dim ProducteursBacs As New List(Of String)

    Const iFRMDALLE As Integer = 4

    Dim SANSPROD As String = "SANS"

    Dim strAM As String = "ArcelorMittal"
    Dim strTous As String
    Dim lChoixBacReduit As Boolean

    Dim VariableBac As Enu_VariablesBac = Enu_VariablesBac.Aucune
#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Dalle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialisationVariablesLocales()
        PreparerFenetre()
        AfficherPoutreEnCours()
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

                '=== BETON ========================================================================

                Me.lbl_Beton.Text = Bloc("CONCRETE")

                '=== ARMATURES ====================================================================

                Me.lbl_General.Text = Bloc("REBARS")

                '=== BAC ==========================================================================

                Me.lbl_General.Text = Bloc("SHEETING")
                Me.rdb_BacBase.Text = Bloc("SHEETBASE")
                Me.rdb_BacCustom.Text = Bloc("SHEETCUSTOM")
                Me.lbl_Producteur.Text = Bloc("SHEETCOMPANY")
                strTous = Bloc("ALL")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub InitialisationVariablesLocales()

        Cls_Dalle.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Dalle, MyDalleLoc)

        LireBaseBacs(MyDicoBacs)

        InitialiseProducteursBacs()

    End Sub

    Private Sub InitialiseProducteursBacs()

        For Each kvp As KeyValuePair(Of String, Cls_Bac) In MyDicoBacs

            Dim MyProd As String
            If kvp.Value.Producteur = "" Then
                MyProd = SANSPROD
            Else
                MyProd = kvp.Value.Producteur
            End If

            If Not (ProducteursBacs.Contains(MyProd)) Then

                ProducteursBacs.Add(MyProd)

            End If

        Next

    End Sub

    Private Sub PreparerFenetre()

        Me.img_Dalle.Dock = DockStyle.Fill

        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboAvecTableau(Me.cmb_ClasseBetonEnrobage, ClasseBeton)

        RemplirComboProducteur()
        PrepareLookGrille(Me.Grid_Bac, Me.Col_ListeSup, Me.pan_Bac.BackColor)
        RemplirGrilleBac(Me.Grid_Bac)

    End Sub

    Sub PrepareLookGrille(ByVal MyGrille As DataGridView, ByVal ColListe As DataGridViewTextBoxColumn, ByVal BackColor As Color)
        '--------------------------------------------------------------------------
        '
        '   Préparation du "Look" de la grille
        '
        '--------------------------------------------------------------------------
        '
        '   MyGrille    [E] :   Grille à préparer
        '   ColListe
        '   BackColor   [E] :   Color de fond à appliquer à la grille
        '
        '--------------------------------------------------------------------------

        MyGrille.CellBorderStyle = DataGridViewCellBorderStyle.None

        MyGrille.DefaultCellStyle.Padding = New Padding(0)
        MyGrille.Font = New Font(MyGrille.Font.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        MyGrille.BorderStyle = BorderStyle.Fixed3D
        MyGrille.BackgroundColor = BackColor

        ColListe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ColListe.Width = CInt(MyGrille.Width * (1))

    End Sub

    Private Sub RemplirGrilleBac(MyGrille As DataGridView)
        '-----------------------------------------------------------------------------------
        '   Remplissage d'une grille avec tous les profilés d'une gamme
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim lBuildBack As Boolean = lBuild
        Dim iPro As Integer = 0
        Dim Chaine As String
        Dim iColor As Integer = 0
        ' Dim BackColors() = {Color.LightGray, Color.Orange}
        Dim BackColors() = {Color.LightGray, Color.White}
        Dim ColorNA As Color = Color.Gray
        Dim ProducteurBac As String
        Dim ProducteurImpose As String = ""
        Dim lAffiche, lTous As Boolean

        '--> Initialisation

        lBuild = True

        lTous = (Not lChoixBacReduit) And (Me.cmb_Producteur.SelectedIndex = 0)

        If Me.cmb_Producteur.SelectedIndex > 0 Then
            ProducteurImpose = Me.ProducteursBacs(Me.cmb_Producteur.SelectedIndex - 1)
        End If

        MyGrille.Rows.Clear()

        '--> Boucle sur tous les profilés de la gamme

        For Each kVs As KeyValuePair(Of String, Cls_Bac) In MyDicoBacs

            ProducteurBac = kVs.Value.Producteur

            lAffiche = (ProducteurBac = ProducteurImpose) Or lTous

            If lAffiche Then
                MyGrille.Rows.Add()
                iPro += 1
                MyGrille.Rows(iPro - 1).Height = 14


                MyGrille.Rows(iPro - 1).Cells(0).Style.BackColor = BackColors(iColor)
                'MyGrille.Rows(iPro - 1).Cells(1).Style.BackColor = BackColors(iColor)

                Chaine = kVs.Value.Etiquette
                MyGrille(0, iPro - 1).Value = Chaine

                iColor += 1
                If iColor > 1 Then iColor = 0

            End If

        Next kVs

        lBuild = lBuildBack
        'NbProGrille = iPro

    End Sub

    Private Sub RemplirComboProducteur()

        If LogicielInfo.Maitre = EnuMaitre.CTICM Then
            lChoixBacReduit = False
        ElseIf LogicielInfo.Maitre = EnuMaitre.ArcelorMittal Then
            If LogicielOptions.lExpert Then
                lChoixBacReduit = False
            Else
                lChoixBacReduit = (ProducteursBacs.Contains(strAM))
            End If
        End If

        Me.cmb_Producteur.Items.Clear()

        If lChoixBacReduit Then
            Me.cmb_Producteur.Items.Add(strAM)
        Else
            Me.cmb_Producteur.Items.Add(strTous)
            For i As Integer = 0 To Me.ProducteursBacs.Count - 1
                Me.cmb_Producteur.Items.Add(ProducteursBacs(i))
            Next
        End If

        Me.cmb_Producteur.SelectedIndex = 0

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)
        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)
    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux
        Me.lbl_Bac.BackColor = CouleurBackBandeaux
        Me.lbl_Bac.ForeColor = CouleurForeBandeaux
        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux
        Me.lbl_Armatures.BackColor = CouleurBackBandeaux
        Me.lbl_Armatures.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherPoutreEnCours()

        '--> Type de dalle

        Select Case MyDalleLoc.type
            Case Cls_Dalle.Enum_TypeDalle.Pleine
                Me.cmb_TypeDalle.SelectedIndex = 0
            Case Cls_Dalle.Enum_TypeDalle.Mixte
                Me.cmb_TypeDalle.SelectedIndex = 1
            Case Cls_Dalle.Enum_TypeDalle.Prefabriquee
                Me.cmb_TypeDalle.SelectedIndex = 2
        End Select

        '--> Epaisseur

        Me.txt_Hd.Text = GetStringNoUnit(MyDalleLoc.t_d, Enu_TypeVariable.Dimension)

        '--> Béton

        Dim Chaine As String
        Chaine = MyDalleLoc.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) Then
            Me.cmb_ClasseBetonEnrobage.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        Else
            Me.cmb_ClasseBetonEnrobage.SelectedIndex = 0
        End If

        MAJI_ProprietesBeton()

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

            MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMDALLE)
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Type ----------------------------------------------------------------------------------------------------

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type <> MyDalleLoc.type) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type = MyDalleLoc.type
        End If

        '-- Dimensions de la dalle ----------------------------------------------------------------------------------

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_d <> MyDalleLoc.t_d) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_d = MyDalleLoc.t_d
        End If
        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_h <> MyDalleLoc.t_h) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_h = MyDalleLoc.t_h
        End If

        '-- Béton ---------------------------------------------------------------------------------------------------

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe <> MyDalleLoc.beton.Classe) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe = MyDalleLoc.beton.Classe
        End If

    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Dalle_Paint(sender As Object, e As PaintEventArgs) Handles img_Dalle.Paint

    End Sub

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        DessineBac(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, MyDalleLoc.bac_acier,
                   MyDalleLoc.t_d, VariableBac, True, True, True, False, 0, 0)
    End Sub

    Public Sub DessineBac(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyBac As Cls_Bac,
                          ByVal EpDalle As Double, ByRef VariableBac As Enu_VariablesBac,
                          ByVal lCotation As Boolean, ByVal lCotEpTot As Boolean,
                          ByVal lTitre As Boolean, ByVal lMemb As Boolean, ByVal tfSup As Double, ByVal hMax As Double,
                          ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '
        '   01/06/07 :  Version 1.00
        '   26/10/21 :  Version 4.01 Beta 8 - Introduction raidisseurs supérieurs
        '
        '-----------------------------------------------------------------------------------------------
        '
        '   Dessin du Bac Acier
        '
        '-----------------------------------------------------------------------------------------------
        '
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   Img         [E] :   Image dans laquelle on dessine
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
        '
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage

        Dim ColorPen As Color = Color.Blue
        Dim ColorRedPen As Color = Color.Red

        Dim myBrushDalle As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.LightGray, Color.DarkGray)
        Dim MyPenBrush As New SolidBrush(ColorPen)
        Dim MyPenRedBrush As New SolidBrush(ColorRedPen)
        Dim MyPen As New Pen(ColorPen)
        Dim MyPenRed As New Pen(ColorRedPen)
        Dim MyFontNormal As Font = FontBase

        Dim xMin, yMin, xMax, yMax As Double
        'Dim DeltaX As Double
        Dim sDecal As Double
        Dim tDecal As Double

        Dim lRaidSup As Boolean

        Dim i As Integer

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim nbOndes As Integer

        '--> Initialisation

        lRaidSup = MyBac.HasRaidisseurSup

        '--> Preparation de la zone d'affichage - Calcul de ParAff

        sDecal = Math.Min(EpDalle / 5, MyBac.h_p / 2)
        tDecal = sDecal / 5

        xMin = 0
        If lMemb Then yMin = -tfSup Else yMin = 0
        If lCotation Then yMin -= 2 * sDecal
        'xMax = nbOndes * Me.e
        xMax = MyBac.LargeurModule
        nbondes = Math.Floor(MyBac.LargeurModule / MyBac.e_p)
        If lCotation And lCotEpTot Then xMax = xMax + 2 * sDecal
        yMax = Math.Max(EpDalle, hMax)

        If lTitre Then yMax += 2 * EpDalle / 10

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop)

        '--> Calcul des points du pourtour de la dalle

        If lRaidSup Then
            MyBac.PrepareContourDalleBacRaidi(nbOndes, EpDalle, xPts, yPts, nbPts)
        Else
            MyBac.PrepareContourDalleBacSimple(nbOndes, EpDalle, xPts, yPts, nbPts)
        End If


        '--> Remplissage contour

        RemplirZone(myGr, myBrushDalle, xPts, yPts, nbPts, MyParAff, False)

        'For i = 0 To nbOndes * 4
        For i = 0 To nbPts - 4
            AddLigne(myGr, xPts(i), yPts(i), xPts(i + 1), yPts(i + 1), MyParAff)
        Next i
        AddLigne(myGr, 0, EpDalle, nbOndes * MyBac.e_p, EpDalle, MyParAff)

        If lCotation Then

            Dim MyFont As New Font("Arial", 8)

            '--> Cotation b1

            If VariableBac = Enu_VariablesBac.b1 Then
                AddFleche(myGr, MyPenRed, 3 * MyBac.e_p / 2 - MyBac.b_b / 2, -sDecal, 3 * MyBac.e_p / 2 + MyBac.b_b / 2, -sDecal, MyParAff, True, True)
                AddTexte(myGr, MyPenRedBrush, "b1", MyFontNormal, 3 * MyBac.e_p / 2, -sDecal - tDecal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            Else
                AddFleche(myGr, MyPen, 3 * MyBac.e_p / 2 - MyBac.b_b / 2, -sDecal, 3 * MyBac.e_p / 2 + MyBac.b_b / 2, -sDecal, MyParAff, True, True)
                AddTexte(myGr, MyPenBrush, "b1", MyFontNormal, 3 * MyBac.e_p / 2, -sDecal - tDecal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If

            '--> Cotation b2

            If VariableBac = Enu_VariablesBac.b2 Then
                AddFleche(myGr, MyPenRed, 3 * MyBac.e_p / 2 - MyBac.b_t / 2, MyBac.h_p + sDecal, 3 * MyBac.e_p / 2 + MyBac.b_t / 2, MyBac.h_p + sDecal, MyParAff, True, True)
                AddTexte(myGr, MyPenRedBrush, "b2", MyFontNormal, 3 * MyBac.e_p / 2, MyBac.h_p + sDecal + tDecal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            Else
                AddFleche(myGr, MyPen, 3 * MyBac.e_p / 2 - MyBac.b_t / 2, MyBac.h_p + sDecal, 3 * MyBac.e_p / 2 + MyBac.b_t / 2, MyBac.h_p + sDecal, MyParAff, True, True)
                AddTexte(myGr, MyPenBrush, "b2", MyFontNormal, 3 * MyBac.e_p / 2, MyBac.h_p + sDecal + tDecal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            End If

            '--> Cotation h

            Dim bMax As Decimal = Math.Max(MyBac.b_t, MyBac.b_b)
            If VariableBac = Enu_VariablesBac.h Then
                AddFleche(myGr, MyPenRed, MyBac.e_p / 2 + bMax / 2 + sDecal, 0, MyBac.e_p / 2 + bMax / 2 + sDecal, MyBac.h_p, MyParAff, True, True)
                AddTexte(myGr, MyPenRedBrush, "h", MyFontNormal, MyBac.e_p / 2 + bMax / 2 + sDecal + tDecal, MyBac.h_p / 2, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
            Else
                AddFleche(myGr, MyPen, MyBac.e_p / 2 + bMax / 2 + sDecal, 0, MyBac.e_p / 2 + bMax / 2 + sDecal, MyBac.h_p, MyParAff, True, True)
                AddTexte(myGr, MyPenBrush, "h", MyFontNormal, MyBac.e_p / 2 + bMax / 2 + sDecal + tDecal, MyBac.h_p / 2, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
            End If

            '--> Cotation hpg

            If lRaidSup Then
                Dim hpg As Double = MyBac.Hauteur_hpg
                Dim xCote As Double = 2 * MyBac.e_p
                If nbOndes <= 2 Then xCote = 0
                If VariableBac = Enu_VariablesBac.hpg Then
                    AddFleche(myGr, MyPenRed, xCote, 0, xCote, hpg, MyParAff, True, True)
                    AddTexte(myGr, MyPenRedBrush, "hpg", MyFontNormal, xCote + tDecal, hpg / 2, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
                Else
                    AddFleche(myGr, MyPen, xCote, 0, xCote, hpg, MyParAff, True, True)
                    AddTexte(myGr, MyPenBrush, "hpg", MyFontNormal, xCote + tDecal, hpg / 2, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
                End If
            End If

            '--> Cotation e

            If VariableBac = Enu_VariablesBac.e Then
                AddFleche(myGr, MyPenRed, 0, -sDecal, MyBac.e_p, -sDecal, MyParAff, True, True)
                AddTexte(myGr, MyPenRedBrush, "e", MyFontNormal, MyBac.e_p / 2, -sDecal - tDecal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            Else
                AddFleche(myGr, MyPen, 0, -sDecal, MyBac.e_p, -sDecal, MyParAff, True, True)
                AddTexte(myGr, MyPenBrush, "e", MyFontNormal, MyBac.e_p / 2, -sDecal - tDecal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If

            '--> Cotation E

            If lCotEpTot Then
                AddFleche(myGr, MyPen, nbOndes * MyBac.e_p + sDecal, 0, nbOndes * MyBac.e_p + sDecal, EpDalle, MyParAff, True, True)
                AddTexte(myGr, MyPenBrush, "E", MyFontNormal, nbOndes * MyBac.e_p + sDecal, EpDalle / 2, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
            End If

            MyFont.Dispose()
        End If

        '--> Titre du bac

        If lTitre Then
            AddTexte(myGr, MyPenBrush, MyBac.Etiquette, MyFontNormal, nbOndes * MyBac.e_p / 2, EpDalle * 11 / 10, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
        End If

        '--> Membrure

        If lMemb Then
            Dim myBrushSup As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.DarkGray, Color.DarkBlue)

            AddRectanglePlein(myGr, myBrushSup, Pens.Black, 0, 0, nbOndes * MyBac.e_p, -tfSup, MyParAff, True, False)
            AddLigne(myGr, 0, 0, nbOndes * MyBac.e_p, 0, MyParAff)
            AddLigne(myGr, 0, -tfSup, nbOndes * MyBac.e_p, -tfSup, MyParAff)

            myBrushSup.Dispose()
        End If

        '--> Liberation des Font, Pen et Brush

        myBrushDalle.Dispose()
        MyPenBrush.Dispose()
        MyPen.Dispose()
        MyPenRedBrush.Dispose()
        MyPenRed.Dispose()

    End Sub

#End Region

#Region " Evènements "

    Private Sub cmb_Producteur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Producteur.SelectedIndexChanged
        RemplirGrilleBac(Me.Grid_Bac)
    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Prefabriquee
        End Select

        Me.img_Dalle.Invalidate()

    End Sub

    Private Sub cmb_ClasseBetonEnrobage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBetonEnrobage.SelectedIndexChanged

        If lBuild Then Exit Sub

        MyDalleLoc.beton.Classe = Me.ClasseBeton(Me.cmb_ClasseBetonEnrobage.SelectedIndex)

        MAJI_ProprietesBeton()
        Me.img_Dalle.Invalidate()

    End Sub

    Private Sub MAJI_ProprietesBeton()

        MyDalleLoc.beton.Calcul_Proprietes()

        Me.txt_Fck.Text = GetStringNoUnit(MyDalleLoc.beton.Fck, Enu_TypeVariable.Contrainte)
        Me.txt_Ecm.Text = GetStringNoUnit(MyDalleLoc.beton.Ecm, Enu_TypeVariable.ModuleY)

    End Sub






#End Region

End Class