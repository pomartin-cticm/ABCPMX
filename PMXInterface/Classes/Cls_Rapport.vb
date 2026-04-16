Option Strict Off

Public Class Cls_Rapport
    Implements IDisposable

    '==========================================================================================================================================
    '   CLASSE POUR L'EDITION D'UNE NOTE 
    '==========================================================================================================================================
    '   NE PAS MODIFIER LA CLASSE - SAUF DRAWDESSIN
    '==========================================================================================================================================

#Region "   Déclarations"
    Enum Enu_StatutPolice
        Exposant
        Indice
        Normal
    End Enum

    Const RAPPORTA4 As Single = 297.0! / 210.0!

#End Region

#Region "   Variables de la classe Rapport"

    '--[ Environnement

    Private ReadOnly MyFamily As FontFamily
    Private ReadOnly MyFamilySymb As FontFamily
    Dim PoliceEnCours As Font
    Dim StyleEnCours As FontStyle

    Private ReadOnly DataBordure As Single               'Definition de la position du Cadre (en % de largeur)
    Private ReadOnly DataMargeL, DataMargeR As Integer   'Definition de la position des marges gauches et droites (e% de largeur)
    Dim BORDURE, MARGEL, MARGER As Single
    Private ReadOnly AntiSlash As Char = "\"c            'Caractère antislash
    Dim lInitEdition As Boolean = False

    '--[ Parametres de la Police

    Dim StatutPolice As Enu_StatutPolice = Enu_StatutPolice.Normal
    Dim lSymbol As Boolean = False
    Dim lGras As Boolean = False
    Dim lItalic As Boolean = False
    Dim lSouligne As Boolean = False
    Private ReadOnly MyPolice As String = "Arial"
    Dim PoliceEnCoursName As String = "Arial"
    Private ReadOnly TaillePolice As Single = 10

    '--[ Document

    Private ReadOnly MyPages As New Dictionary(Of Integer, List(Of String))
    Private ReadOnly nbPagesSommaire As Integer
    Private nbPages As Integer

    '--[ Variables locale pour l'affichage

    Dim XPen, YPen As Single
    Dim kECH As Single = 1          'Echelle d'impression
    Dim HLigne As Single = 0        'Hauteur d'une ligne à l'impression
    Dim kFont As Single = 1         'Echelle des polices de caractères
    Dim HInterLigne As Single       'Hauteur d'interligne
    Dim HIndiceExposant As Single   'Hauteur de décalage pour exposants et indices
    Dim YDecal As Single = 0
    Dim YBalise As Single = 0
    Dim HReference As Single = 0    'Hauteur de reference
    Dim lRetourCharriot As Boolean  'Indique si retour chariot à la fin de la ligne

    '--[ Variables physique de l'imprimante utilisée

    Dim HardMarginLeft, HardMarginRight, HardMarginTop, HardMarginBot As Single
    Dim OffSetX As Single

    '--[ Entetes et Pieds de page

    Public PoliceEnTete As Font
    Dim TailleEntete, TaillePied As Single
    Dim TailleLogo As Size
    Const iEpBordure As Integer = 2
    Const iEpTrait As Integer = 1
    Dim MyPenBordure As Pen
    Dim MyPenTrait As Pen

    Public EntetePrincipal, EnteteSecond As String

    Dim PoliceVersion As Font

    '--[ Ligne pour l'étiquette de la page de garde

    Public EtiquetteLigne(4, 1) As String

    '--[ Polices prédéfinies et autres objets

    Private ReadOnly PoliceTitre(2) As Font
    Private ReadOnly BrushTitre(2) As Brush
    Private ReadOnly PosTitre(2) As Integer
    Private ReadOnly PenTitre(0) As Pen

    Dim PoliceLegende As Font

    '--[ Variables

    Dim iLigne As Integer
    Dim iPageEnCours As Integer

    Private ReadOnly ColorCellFond As Color = Color.LightGray    'Couleur pour le fond des cellules
    Private ReadOnly ColorCellFond_Correct As Color = Color.PaleGreen    'Couleur pour le fond des cellules - Correct
    Private ReadOnly ColorCellFond_Erreur As Color = Color.IndianRed    'Couleur pour le fond des cellules - Faux

    Dim hTextRef As Single

    '--[ Date d'édition 

    Dim DateEdit As String

    '--[ Foot note
    Public FootNote As String

    '--[ Page de garde ?

    Public lPageGarde As Boolean = False

    '--[ Gestion des titres de chapitres

    Public IndTitre(2) As Integer
    Public lNumTitre() As Boolean = {True, True, False}

    'Pas de modifications de la classe sauf en drawdessin
    'Public DiagrammesNDC As New Cls_DiagrammeNDC 'indice du cas de charge à afficher (utile pour le dessin RDM dans la note de calcul)

#End Region

#Region "   Constructeur ========================================================================"

    Public Sub New(ByVal Police As String,
            ByVal Bordure As Single, ByVal MargeGauche As Integer,
            ByVal MargeDroite As Integer)

        '--> Definition de la Police en FontFamily

        Try
            MyFamily = New FontFamily(Police)
        Catch ex As Exception
            MyFamily = FontFamily.GenericSansSerif
        End Try
        Try
            MyFamilySymb = New FontFamily("Symbol")
        Catch ex As Exception
            MyFamilySymb = FontFamily.GenericSansSerif
        End Try

        '--> Initialisation des pages

        MyPages.Clear()
        nbPages = 0

        '--> Définition du cadre
        Me.DataBordure = Bordure
        Me.DataMargeL = MargeGauche
        Me.DataMargeR = MargeDroite

        '--> Police

        Me.MyPolice = Police

        '--> Initialisation de variables

        ClePos(PositionTexteInCell.Centre) = "CENTRE"
        ClePos(PositionTexteInCell.Droite) = "DROITE"
        ClePos(PositionTexteInCell.Gauche) = "GAUCHE"

    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose
        If lInitEdition Then
            MyPenBordure.Dispose()
            MyPenTrait.Dispose()
            PoliceEnTete.Dispose()
            PoliceTitre(0).Dispose()
            PoliceTitre(1).Dispose()
            PoliceTitre(2).Dispose()
            BrushTitre(0).Dispose()
            BrushTitre(1).Dispose()
            BrushTitre(2).Dispose()
            PenTitre(0).Dispose()
        End If
    End Sub

#End Region

#Region "   Propriétés "
    Public ReadOnly Property NombrePages() As Integer
        Get
            Return Me.MyPages.Count
        End Get
    End Property
#End Region

#Region "   Construction de la Note"

    Public Sub SautePage()
        '-------------------------------------------------------------
        '
        '   11/01/08 :  Creation - Version 1.0
        '
        '--------------------------------------------------------------------------------------------
        '
        '   Crée une nouvelle page au rapport
        '
        '-------------------------------------------------------------------------------------------

        MyPages.Add(nbPages, New List(Of String))
        nbPages += 1

    End Sub

    Public Sub SauteLigne()
        '-------------------------------------------------------------
        '
        '   18/01/08 :  Creation - Version 1.0
        '
        '--------------------------------------------------------------------------------------------
        '
        '   Saute une ligne
        '
        '-------------------------------------------------------------------------------------------

        MyPages(nbPages - 1).Add("")

    End Sub

    Public Sub AddLigneInRapport(ByVal Ligne As String)
        '-------------------------------------------------------------
        '
        '   11/01/08 :  Creation - Version 1.0
        '
        '--------------------------------------------------------------------------------------------
        '
        '   Ajoute une ligne dans la page en cours
        '
        '-------------------------------------------------------------------------------------------

        MyPages(nbPages - 1).Add(Ligne)

    End Sub

#End Region

#Region "   Initialisations "

    Private Sub GetHardMargins(ByRef MyGr As Graphics, ByVal sWi As Single)
        '-------------------------------------------------------------
        '
        '   17/01/08 :  Creation - Version 1.00
        '
        '-------------------------------------------------------------
        '
        '   Initialisation des marges physiques de l'imprimante
        '
        '-------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   sWi         [E] :   Largeur de la zone de dessin
        '
        '-------------------------------------------------------------

        HardMarginLeft = MyGr.VisibleClipBounds.Left
        HardMarginRight = sWi - MyGr.VisibleClipBounds.Left - MyGr.VisibleClipBounds.Width
        HardMarginTop = MyGr.VisibleClipBounds.Top
        HardMarginBot = RAPPORTA4 * sWi - MyGr.VisibleClipBounds.Top - MyGr.VisibleClipBounds.Height

        OffSetX = (HardMarginRight - HardMarginLeft) / 2

        kECH = MyGr.VisibleClipBounds.Width / sWi

    End Sub

    Private Sub GetBordure(ByVal sWi As Single)
        '-------------------------------------------------------------
        '
        '   17/01/08 :  Creation - Version 1.00
        '
        '-------------------------------------------------------------
        '
        '   Calcul de la bordure de la note
        '
        '-------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   sWi         [E] :   Largeur de la zone de dessin
        '   lImprimante [E] :   Indique si impression écran ou imprimante
        '
        '-------------------------------------------------------------

        BORDURE = (sWi * DataBordure / 100)

    End Sub

    Sub InitialisationAffichage(ByVal MyGr As Graphics, ByVal sWi As Single)
        '-------------------------------------------------------------
        '
        '   18/01/08 :  Creation - Version 1.00
        '
        '-------------------------------------------------------------
        '
        '   Initialisation de l'affichage de la note
        '
        '-------------------------------------------------------------
        '
        '
        '-------------------------------------------------------------

        kFont = MyGr.MeasureString("in principio creavit Deus caelum et terram", New Font(Me.MyPolice, 9)).Width / sWi
        hTextRef = MyGr.MeasureString("H", New Font(Me.MyPolice, 9)).Height

        kFont = 0.2721898 / kFont

        PoliceEnTete = New Font(Me.MyPolice, kFont * 9)
        TailleEntete = PoliceEnTete.GetHeight * 5
        TaillePied = PoliceEnTete.GetHeight * 2
        TailleLogo.Height = PoliceEnTete.GetHeight * 3
        '==V3.03
        TailleLogo.Width = TailleLogo.Height * 157 / 56
        PoliceVersion = New Font(Me.MyPolice, kFont * 10, FontStyle.Bold Or FontStyle.Italic)

        MyPenBordure = New System.Drawing.Pen(Color.Black, iEpBordure)
        MyPenTrait = New System.Drawing.Pen(Color.Black, iEpTrait)

        lInitEdition = True

        PoliceTitre(0) = New Font(Me.MyPolice, kFont * 12, FontStyle.Bold)
        PoliceTitre(1) = New Font(Me.MyPolice, kFont * 11, FontStyle.Bold Or FontStyle.Italic)
        PoliceTitre(2) = New Font(Me.MyPolice, kFont * 10, FontStyle.Bold Or FontStyle.Italic)

        BrushTitre(0) = New SolidBrush(BleuCTICM)
        BrushTitre(1) = New SolidBrush(BleuCTICM)
        BrushTitre(2) = New SolidBrush(GrisCTICM)

        PosTitre(0) = 5
        PosTitre(1) = 8
        PosTitre(2) = 11

        PenTitre(0) = New Pen(BleuCTICM, 2)

        PoliceLegende = New Font(Me.MyPolice, kFont * 9)

        HReference = PoliceEnTete.GetHeight
        HIndiceExposant = HReference / 10
        HInterLigne = HReference / 5

        DateEdit = Format(My.Computer.Clock.LocalTime.Date.Day, "00") & "/" & Format(My.Computer.Clock.LocalTime.Date.Month, "00") & "/" & My.Computer.Clock.LocalTime.Date.Year.ToString

    End Sub

    Public Sub Clear()
        '-----------------------------------------------------------------------------------
        '
        '   21/01/08 :  Création - Version 1.00 - POM
        '
        '-----------------------------------------------------------------------------------
        '
        '   Reinitialisation de la note
        '        
        '-----------------------------------------------------------------------------------

        Me.MyPages.Clear()
        nbPages = 0

    End Sub

#End Region

#Region "   Ecriture de la note de calcul dans un fichier ASCII"

    Public Sub EcrireNote(ByRef Lines As List(Of String))
        '------------------------------------------------------------------------
        '
        '   26/02/09 :  Création - Version 1.00 B5 - POM
        '
        '------------------------------------------------------------------------
        '
        '   Ecriture de la Note de Calcul dans un fichier ASCII
        '
        '------------------------------------------------------------------------
        '
        '   Lines   [S] :   Lignes du fichier ASCII
        '
        '------------------------------------------------------------------------

        '--[ Boucle sur les pages

        Dim iPageEnCours As Integer

        For iPageEnCours = 0 To Me.MyPages.Count - 1

            '--[ Boucles sur les lignes de la page

            iLigne = 0

            Do While iLigne < Me.MyPages(iPageEnCours).Count

                Me.EcrireLigne(Me.MyPages(iPageEnCours)(iLigne), True, Lines)
                iLigne += 1

            Loop

        Next


    End Sub

    Private Sub EcrireTableau(ByRef Lines As List(Of String), ByVal iPos As Integer)
        '------------------------------------------------------------------------------------------------
        '
        '   26/02/09 :  Création - Version 1.00 B5 - POM
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Ecriture d'une tableau de la note de calcul dans un fichier ASCII
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Lines       [S] :   Lignes du fichier ASCII
        '
        '------------------------------------------------------------------------------------------------

        '--[ Déclaration

        Dim lCont As Boolean
        Dim Mots() As String = New String() {}
        Dim nMots, nCells As Integer
        Dim Chaine, Chaine0 As String

        Chaine0 = ""
        For i As Integer = 0 To iPos
            Chaine0 &= " "
        Next

        '--[

        lCont = True

        Do While lCont

            iLigne += 1

            lCont = Not (Me.MyPages(iPageEnCours)(iLigne).Trim.ToUpper.Substring(0, 4) = "\ETA")

            If lCont Then

                DecomposeLine(Me.MyPages(iPageEnCours)(iLigne).Trim.ToUpper, SEPARATEURS, Mots, nMots)

                If nMots > 0 Then
                    If Mots(1).Substring(0, 4) = "LTAB" Then
                        nCells = CInt(Mots(2))
                        '--------------------------------------------------
                        '  Traitement du fond et des bords de ligne
                        '--------------------------------------------------
                        If nMots > 3 Then
                            'Ici traitement des couleurs de fond et des bordures, sans objet dans un fichier ASCII
                            Dim wLigne As Integer = CInt(Mots(5))

                            For j As Integer = 6 To nMots
                                Select Case Mots(j).ToUpper.Substring(0, 4)
                                    Case "BSUP"
                                        Chaine = Chaine0
                                        For i As Integer = 0 To wLigne
                                            Chaine &= "-"
                                        Next
                                        Lines.Add(Chaine)
                                    Case "BINF"
                                        'MyGr.DrawLine(Pens.Black, xPos, YPen + HLigne, xPos + wLigne, YPen + HLigne)
                                End Select
                            Next

                        End If

                        Lines.Add(Chaine0)
                        For i As Integer = 1 To nCells
                            iLigne += 1
                            EcrireCellule(Lines)
                        Next
                    End If

                End If
            End If
        Loop

    End Sub

    Private Sub EcrireCellule(ByRef Lines As List(Of String))
        '------------------------------------------------------------------------------------------------
        '
        '   26/02/09 :  Création - Version 1.00 B5 - POM
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Ecriture d'une tableau de la note de calcul dans un fichier ASCII
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Lines       [S] :   Lignes du fichier ASCII
        '
        '------------------------------------------------------------------------------------------------

        Dim Mots() As String = New String() {}
        Dim nMots As Integer
        Dim Largeur As Single
        Dim sWTexte, sHTexte As Single
        Dim BordureCellule As Single
        Dim Chaine As String
        Dim lGauche, lDroite As Boolean

        DecomposeLine(Me.MyPages(iPageEnCours)(iLigne).Trim, SEPARATEURS, Mots, nMots)

        If nMots >= 4 Then
            Largeur = Val(Mots(2))

            BordureCellule = CInt(Mots(3))

            '--[ Représentation des bordures

            lDroite = False
            lGauche = False
            If BordureCellule >= Bordures.Bas Then
                BordureCellule -= Bordures.Bas
            End If
            If BordureCellule >= Bordures.Droite Then
                BordureCellule -= Bordures.Droite
                lDroite = True
            End If
            If BordureCellule >= Bordures.Haut Then
            End If
            If BordureCellule >= Bordures.Gauche Then
                lGauche = True
            End If

            '--[ AffichageOptFeu du texte

            If nMots > 4 Then

                Dim Indice As Integer = Me.MyPages(iPageEnCours)(iLigne).Trim.IndexOf(":")

                Chaine = Me.MyPages(iPageEnCours)(iLigne).Trim.Substring(Indice + 1).Trim
                'sWTexte = MyGr.MeasureString(Chaine, PoliceEnCours).Width
                sWTexte = Chaine.Length

                Dim xPos, yPos As Single
                Select Case Mots(4)
                    Case ClePos(PositionTexteInCell.Centre)
                        xPos = XPen + (Largeur - sWTexte) / 2
                        yPos = YPen + (HLigne - sHTexte) / 2
                    Case ClePos(PositionTexteInCell.Droite)
                        xPos = XPen + Largeur - sWTexte - sHTexte / 4
                        yPos = YPen + (HLigne - sHTexte) / 2
                    Case ClePos(PositionTexteInCell.Gauche)
                        xPos = XPen + sHTexte / 2
                        yPos = YPen + (HLigne - sHTexte) / 2
                End Select

                Dim ChaineCell As String

                If lGauche Then ChaineCell = "|" Else ChaineCell = " "
                ChaineCell &= Me.CleanCellule(Chaine)

                For i As Integer = 1 To Largeur - sWTexte
                    ChaineCell &= " "
                Next
                If lDroite Then ChaineCell &= "|" Else ChaineCell &= " "

                Lines(Lines.Count - 1) = Lines(Lines.Count - 1) & ChaineCell

            End If

        End If

    End Sub

    Private Sub EcrireLigne(ByVal LigneNote As String, ByVal lNewLine As Boolean, ByRef Lines As List(Of String))
        '------------------------------------------------------------------------------------------------
        '
        '   26/02/09 :  Création - Version 1.00 B5 - POM
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Ecriture d'une ligne de la note de calcul dans un fichier ASCII
        '
        '------------------------------------------------------------------------------------------------
        '
        '   LigneNote   [E] :   Ligne de la note (avec ses codes de mise en page)
        '   lNewLine    [E] :   Indique si nouvelle ligne
        '
        '   Lines       [S] :   Lignes du fichier ASCII
        '
        '------------------------------------------------------------------------------------------------

        Dim Indice As Integer
        Dim Mot, Cle As String
        Dim Length As Integer = LigneNote.Length
        Dim lSauteligne As Boolean = lNewLine

        If LigneNote = "" Then Exit Sub

        HLigne = 0

        '--[ Recherche Mot Clé (repéré par antislash)

        Indice = LigneNote.IndexOf(AntiSlash)

        If Indice = -1 Then
            '--[ Pas de mot clé : on écrit directement toute la ligne
            If lNewLine Then
                Lines.Add(LigneNote)
            Else
                Lines(Lines.Count - 1) = Lines(Lines.Count - 1) & LigneNote
            End If
        Else
            '--[ Au moins un mot clé dans la ligne
            '--> Si le mot clé n'est pas au début, on écrit ce qu'il y a avant
            If Indice > 0 Then
                Mot = LigneNote.Substring(0, Indice)
                Me.EcrireLigne(Mot, lNewLine, Lines)
                lSauteligne = False
            End If

            '--> Traitement du mot clé

            Cle = LigneNote.Substring(Indice + 1, Math.Min(3, Length - Indice - 1)).ToUpper
            Dim lSuite As Boolean   'Indicateur pour savoir si il faut traiter la suite comme une ligne
            Dim iMotCle As Integer

            Select Case Cle
                Case "NOS"
                    lRetourCharriot = False
                    lSuite = True : iMotCle = 3
                Case "TAB"
                    lSuite = False : iMotCle = 3    'Debut d'un tableau
                    EcrireTableau(Lines, CInt(LigneNote.Trim.Substring(LigneNote.Trim.LastIndexOf(" ") + 1)))
                    'Me.DrawTableau(Ligne.Trim.Substring(Ligne.Trim.LastIndexOf(" ") + 1), MyGr, sWi)
                Case "SOM"
                    lSuite = True : iMotCle = 3
                Case "ETA"                        'Fin de tableau  
                    lSuite = False : iMotCle = 3
                Case "LUW"
                    lSuite = False : iMotCle = 3
                Case "BAL"                        'Balise de position YPen
                    lSuite = False
                    YBalise = YPen
                Case "RBA"                        'Retour sur balise
                    lSuite = False
                    YPen = YBalise
                Case "IMG"                          'AffichageOptFeu d'un dessin on ne traite pas
                    lSuite = False
                Case "DIA"
                    lSuite = False
                Case "IMF"                          'AffichageOptFeu d'un dessin à une position forcée
                    lSuite = False                  'On ne traite pas

                Case "TI0", "TI1"                        'Titre principal
                    lSuite = False
                    Me.EcrireLigne(LigneNote.Substring(Indice + 4), lSauteligne, Lines)
                    lSauteligne = False

                Case "W01", "W02", "W03"          'Titre niveau 1, 2 ou 3
                    lSuite = False
                    Me.EcrireLigne(LigneNote.Substring(Indice + 4).ToUpper, lSauteligne, Lines)
                    lSauteligne = False

                Case "TW1", "TW2", "TW3"        'Alignement sur titre 1, 2 ou 3
                    Dim Spacing As String = ""
                    For k As Integer = 1 To CInt(Cle.Substring(2))
                        Spacing &= "  "
                    Next
                    Me.EcrireLigne(Spacing, lSauteligne, Lines)
                    lSauteligne = False
                    lSuite = True
                    iMotCle = 3

                Case Else
                    Cle = LigneNote.Substring(Indice + 1, 1)
                    lSuite = True
                    iMotCle = 1
                    Select Case Cle
                        'Case "U" : Me.lSouligne = True  'Mise en souligné
                        'Case "u" : Me.lSouligne = False 'Fin du souligné
                        'Case "G" : Me.lGras = True      'Mise en gras
                        'Case "g" : Me.lGras = False     'Fin du gras
                        'Case "I" : Me.lItalic = True    'Mise en italique
                        'Case "i" : Me.lItalic = False   'Fin Italique
                        'Case "S" : Me.lSymbol = True    'Police Symbol
                        'Case "s" : Me.lSymbol = False   'Police Normale
                        'Case "=" : Me.StatutPolice = Enu_StatutPolice.Normal
                        'Case "+" : Me.StatutPolice = Enu_StatutPolice.Exposant
                        'Case "-"
                        '    Me.StatutPolice = Enu_StatutPolice.Indice
                        'Case "N", "n" : Me.lSymbol = False : Me.lGras = False : Me.lItalic = False
                        Case "T"    'Tabulation
                            lSuite = True
                            iMotCle = 3
                            Dim Spacing As String = "   "
                            'For k As Integer = 1 To CInt(LigneNote.Substring(Indice + 2, 2))
                            '    Spacing = Spacing & " "
                            'Next
                            Me.EcrireLigne(Spacing, lSauteligne, Lines)
                            lSauteligne = False
                        Case "C"
                        Case "L"    'Légende
                            lSuite = False
                            'Me.DrawLegende(Ligne.Substring(Indice + 4), MyGr, CInt(Ligne.Substring(Indice + 2, 2)), sWi, MyBrush)

                    End Select

            End Select
            If lSuite Then
                Me.EcrireLigne(LigneNote.Substring(Indice + 1 + iMotCle), lSauteligne, Lines)
            End If
        End If


    End Sub

    Private Function CleanCellule(ByVal Chaine As String) As String
        '-------------------------------------------------------------------------------------
        '
        '   26/02/09 :  Création - Version 1.00 B5 - PO
        '
        '-------------------------------------------------------------------------------------
        '
        '   Nettoie le contenu d'une cellule des caractères de mise en forme
        '
        '-------------------------------------------------------------------------------------
        '
        '   Chaine  [E] :   Contenu de la cellule
        '
        '-------------------------------------------------------------------------------------

        Dim Clean As String = ""
        Dim i As Integer

        i = 0

        Do While i < Chaine.Length

            If Chaine(i) = "\" Then
                i += 2
            Else
                Clean &= Chaine(i)
                i += 1
            End If

        Loop

        Return Clean
    End Function

#End Region

#Region "   Edition de la note ==============================================================================="

    Public Sub DrawPage(ByVal indPage As Integer, ByVal nbPageAffiche As Integer,
                        ByRef MyGr As Graphics,
                        ByRef MyBrush As System.Drawing.Brush,
                        ByVal lImprimante As Boolean, ByVal lFirst As Boolean,
                        ByVal sWi As Single, ByVal sHI As Single, ByVal YTop As Single)

        '----------------------------------------------------------------------------------
        '
        '   10/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu de l'environnement commun de toutes les pages
        '
        '----------------------------------------------------------------------------------
        '
        '   indPage     [E] :   Indice de la page à afficher
        '   lDeuxPages  [E] :   Indique si affichage deux pages ou une seule
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   MyBrush     [E] :   Pinceau avec lequel on affiche
        '   lImprimante [E] :   Indique si impression écran ou imprimante
        '   lFirst      [E] :   Indique si c'est la première page imprimée
        '   sWi,sHI     [E] :   Largeur et hauteur de référence pour l'affichage de la page
        '   YTop        [E] :   Position du Top de la feuille dans le Graphics
        '
        '----------------------------------------------------------------------------------

        GetMyPolice()
        If lFirst Then
            kECH = 1
            If lImprimante Then GetHardMargins(MyGr, sWi)
            GetBordure(sWi)
        End If
        DrawBackGround(MyGr, sWi, YTop, indPage)

        iPageEnCours = indPage
        MARGEL = CInt(sWi * DataMargeL / 100)
        MARGER = CInt(sWi * DataMargeR / 100)
        If Me.lPageGarde And indPage = 0 Then
            YPen = YTop + sWi * RAPPORTA4 / 3
        Else
            YPen = YTop + BORDURE + MARGEL + TailleEntete
        End If

        iLigne = 0
        lRetourCharriot = True

        Do While iLigne < Me.MyPages(indPage).Count
            If lRetourCharriot Then XPen = MARGEL Else lRetourCharriot = True
            DrawLigne(Me.MyPages(indPage)(iLigne), MyGr, MyBrush, lImprimante, sWi, sHI)
            If lRetourCharriot Then YPen += HLigne + HInterLigne
            iLigne += 1
        Loop
        For i As Integer = 0 To Me.MyPages(indPage).Count - 1
        Next

        '--> Dessin des autres pages affichables à l'écran
        If nbPageAffiche > 1 Then

            For cptPageAffiche = 1 To nbPageAffiche - 1
                If (indPage + cptPageAffiche < MyPages.Count) Then
                    DrawPage(indPage + cptPageAffiche, 1, MyGr, MyBrush, lImprimante, False, sWi, sHI, YTop + sWi * RAPPORTA4 * cptPageAffiche)
                    DrawSeparePages(MyGr, sWi, YTop + sWi * RAPPORTA4 * cptPageAffiche)
                End If
            Next

        End If

    End Sub

    Private Sub DrawBackGround(ByRef MyGr As Graphics, ByVal sWi As Single, ByVal YTop As Single, ByVal indPage As Integer)
        '-------------------------------------------------------------
        '
        '   10/01/08 :  Creation - Version 1.00
        '   06/01/16 :  Modification - V3.03 : AffichageOptFeu du logo custom - POM
        '
        '-------------------------------------------------------------
        '
        '   AffichageOptFeu de l'environnement commun de toutes les pages
        '
        '-------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   lImprimante [E] :   Indique si impression écran ou imprimante
        '   sWi         [E] :   Largeur de la zone de dessin
        '   YTop        [E] :   Position du haut de la page
        '   indPage     [E] :   Numéro de la page
        '
        '-------------------------------------------------------------

        '--[ Déclaration

        Dim xPos, yTrait, yPos As Single
        Dim Respiration As Single
        Dim Chaine As String
        Dim lNormal As Boolean
        Dim lDrawLogo As Boolean

        '--[ Initialisation

        lNormal = Not (Me.lPageGarde And indPage = 0)
        Respiration = (TailleEntete - TailleLogo.Height) / 2

        '--[ Cadre général

        '--> Rectangle de bordure - trait noir
        MyGr.DrawRectangle(MyPenBordure, kECH * BORDURE, kECH * (BORDURE + YTop),
                                       kECH * (sWi - 2 * BORDURE), kECH * (sWi * RAPPORTA4 - 2 * BORDURE))

        If lNormal Then

            '### Traitement d'une page normale

            'affichage du logo logiciel
            yTrait = kECH * (BORDURE + YTop + Respiration / 2)
            DrawLogoLogiciel(MyGr, PoliceEnCours.Size, kECH * (BORDURE + Respiration + TailleLogo.Width / 2), yTrait, True, sWi)

            'affichage de la ligne inférieure de l'entete
            yTrait = kECH * (BORDURE + TailleEntete + YTop)
            MyGr.DrawLine(MyPenTrait, kECH * BORDURE, yTrait, kECH * (sWi - BORDURE), yTrait)

            'affichage de la ligne verticale à droite du logo logiciel
            xPos = kECH * (TailleLogo.Width + BORDURE + 2 * Respiration)
            MyGr.DrawLine(MyPenTrait, xPos, kECH * (BORDURE + YTop), xPos, kECH * (BORDURE + YTop + TailleEntete))

            'affichage de l'entete supérieur
            xPos += kECH * Respiration
            yPos = kECH * (BORDURE + Respiration / 2 + YTop)
            MyGr.DrawString(EntetePrincipal, PoliceVersion, Brushes.Black, xPos, yPos)

            'ligne de trait entre les deux entetes
            yTrait = yPos + kECH * (Respiration / 2 + PoliceVersion.GetHeight)
            If lDrawLogo Then           '==V3.03
                MyGr.DrawLine(MyPenTrait, xPos - kECH * Respiration, yTrait, kECH * (sWi - BORDURE - TailleLogo.Width - 2 * Respiration), yTrait)
            Else
                MyGr.DrawLine(MyPenTrait, xPos - kECH * Respiration, yTrait, kECH * (sWi - BORDURE), yTrait)
            End If

            'affichage entete inférieur
            yPos = yTrait + kECH * ((TailleEntete - Respiration) / 2 - PoliceVersion.GetHeight)
            MyGr.DrawString(EnteteSecond, PoliceVersion, Brushes.Black, xPos, yPos)

        Else

            '### Traitement de la page de garde

            yTrait = kECH * (RAPPORTA4 * sWi - BORDURE - 4 * TaillePied + YTop)
            MyGr.DrawLine(MyPenTrait, kECH * BORDURE, yTrait, kECH * (sWi - BORDURE), yTrait)

            Dim xPos2, MaxW As Single
            xPos = kECH * (BORDURE + TaillePied / 4)
            MaxW = MyGr.MeasureString(Me.EtiquetteLigne(0, 0), PoliceEnTete).Width
            For i As Integer = 1 To 4
                MaxW = Math.Max(MaxW, MyGr.MeasureString(Me.EtiquetteLigne(i, 0), PoliceEnTete).Width)
            Next
            xPos2 = xPos + 1.1 * MaxW

            For i As Integer = 0 To 4
                yPos = yTrait + kECH * TaillePied / 4 + i * TaillePied
                MyGr.DrawString(Me.EtiquetteLigne(i, 0), PoliceEnTete, Brushes.Black, xPos, yPos)
                MyGr.DrawString(Me.EtiquetteLigne(i, 1), PoliceEnTete, Brushes.Black, xPos2, yPos)
            Next

            yPos = kECH * (YTop + sWi * RAPPORTA4 / 8)
            DrawLogoLogiciel(MyGr, PoliceEnCours.Size * 1.5, sWi / 2, yPos, False, sWi)

        End If

        yTrait = kECH * (RAPPORTA4 * sWi - BORDURE - TaillePied + YTop)
        MyGr.DrawLine(MyPenTrait, kECH * BORDURE, yTrait, kECH * (sWi - BORDURE), yTrait)

        '--[ AffichageOptFeu de la date

        yPos = yTrait + kECH * TaillePied / 4
        xPos = kECH * (BORDURE + TaillePied / 4)
        MyGr.DrawString(DateEdit, PoliceEnTete, Brushes.Black, xPos, yPos)

        '--[ AffichageOptFeu du pied de page  (V3.01 - 17/09/14)

        xPos = kECH * (BORDURE + 0.5 * (sWi - MyGr.MeasureString(FootNote, PoliceEnTete).Width))
        MyGr.DrawString(FootNote, PoliceEnTete, Brushes.Black, xPos, yPos)

        '--[ AffichageOptFeu du numéro de la page

        Chaine = CStr(indPage + 1) & " / " & CStr(Me.NombrePages)
        xPos = kECH * (sWi - BORDURE - (TaillePied / 4)) - MyGr.MeasureString(Chaine, PoliceEnTete).Width
        MyGr.DrawString(Chaine, PoliceEnTete, Brushes.Black, xPos, yPos)

    End Sub

    Private Sub GetMyPolice()
        '----------------------------------------------------------------------------------
        '
        '   14/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   Définit la Police A Utiliser (Police En cours)
        '   En fonction des parametres de polices
        '
        '----------------------------------------------------------------------------------

        Dim DeltaT As Single

        StyleEnCours = FontStyle.Regular
        If Me.lGras Then StyleEnCours = FontStyle.Bold
        If Me.lItalic Then StyleEnCours = StyleEnCours Or FontStyle.Italic
        If Me.lSouligne Then StyleEnCours = StyleEnCours Or FontStyle.Underline
        If Me.lSymbol Then PoliceEnCoursName = "Symbol" Else PoliceEnCoursName = MyPolice

        Select Case StatutPolice
            Case Enu_StatutPolice.Exposant
                YDecal = -HIndiceExposant
                DeltaT = 2
            Case Enu_StatutPolice.Indice
                YDecal = 5 * HIndiceExposant
                DeltaT = 2
            Case Enu_StatutPolice.Normal
                YDecal = 0
                DeltaT = 0
        End Select
        PoliceEnCours = New Font(PoliceEnCoursName, kFont * kECH * (TaillePolice - DeltaT), StyleEnCours)
        HLigne = Math.Max(HLigne, PoliceEnCours.GetHeight)

    End Sub

    Private Sub DrawSeparePages(ByVal MyGr As Graphics, ByVal sWi As Single, ByVal YSep As Single)
        '----------------------------------------------------------------------------------
        '
        '   10/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu de la ligne séparant deux pages
        '
        '----------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   
        '   lImprimante [E] :   Indique si l'on dessine dans une imprimante
        '   sWI         [E] :   Largeur de la zone de dessin
        '   YSep        [E] :   Position du trait de séparation
        '
        '----------------------------------------------------------------------------------

        Dim MyPen As New Pen(Color.DarkGray, 1)

        MyGr.DrawLine(MyPen, 0, YSep, sWi, YSep)

        MyPen.Dispose()

    End Sub

    Private Sub DrawLegende(ByVal Legende As String, ByRef MyGr As Graphics,
                            ByVal iCentre As Integer, ByVal sWi As Single, ByVal myBrush As Brush)
        '----------------------------------------------------------------------------------
        '
        '   18/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'une légende, centrée en iCentre % de la page
        '
        '----------------------------------------------------------------------------------
        '
        '   Legende     [E] :   Ligne à afficher
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   iCentre     [E] :   CEntrage de la légende
        '   sWi         [E] :   Largeur de référence pour l'affichage de la page
        '        '
        '----------------------------------------------------------------------------------

        Dim xPos As Single = iCentre / 100 * sWi

        xPos -= MyGr.MeasureString(Legende, PoliceLegende).Width / 2
        HLigne = PoliceLegende.GetHeight * 2

        YPen += PoliceLegende.GetHeight * 0.25

        MyGr.DrawString(Legende, PoliceLegende, myBrush, xPos, YPen)


    End Sub

    Private Sub DrawTitreDoc(ByRef Ligne As String, ByRef MyGr As Graphics,
                             ByVal iTitre As Integer, ByVal sWi As Single)
        '----------------------------------------------------------------------------------
        '
        '   03/07/08 :  Creation - Version 1.00 B2
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu du titre principal du document
        '
        '----------------------------------------------------------------------------------
        '
        '   Ligne       [E] :   Ligne à afficher
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   iTitre      [E] :   Niveau du titre
        '   sWi         [E] :   Largeur de référence pour l'affichage de la page
        '
        '----------------------------------------------------------------------------------

        Dim MyFont As Font
        Dim MyBrush As SolidBrush
        Dim xPos As Single

        If iTitre = 0 Then
            MyFont = New Font(Me.MyPolice, kFont * 20, FontStyle.Bold)
            MyBrush = New SolidBrush(BleuCTICM)
        Else
            MyFont = New Font(Me.MyPolice, kFont * 16, FontStyle.Bold)
            MyBrush = New SolidBrush(GrisCTICM)
        End If

        'Select Case iTitre
        '    Case 0
        '        MyFont = New Font(Me.MyPolice, kFont * 20, FontStyle.Bold)
        '        MyBrush = New SolidBrush(PurpleAM)
        '    Case 1
        '        MyFont = New Font(Me.MyPolice, kFont * 16, FontStyle.Bold)
        '        MyBrush = New SolidBrush(BlueAM)
        'End Select

        xPos = sWi / 2 - MyGr.MeasureString(Ligne, MyFont).Width / 2

        MyGr.DrawString(Ligne, MyFont, MyBrush, xPos, YPen)

        HLigne = MyFont.GetHeight * 2

        MyFont.Dispose()
        MyBrush.Dispose()
    End Sub

    Private Sub DrawTitre(ByRef Ligne As String, ByRef MyGr As Graphics,
                          ByVal iTitre As Integer, ByVal sWi As Single)
        '----------------------------------------------------------------------------------
        '
        '   18/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'un titre
        '
        '----------------------------------------------------------------------------------
        '
        '   Ligne       [E] :   Ligne à afficher
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   iTitre      [E] :   Niveau du titre
        '   sWi         [E] :   Largeur de référence pour l'affichage de la page
        '
        '   lImprimante [E] :   Indique si impression écran ou imprimante
        '   YTop        [E] :   Position du Top de la feuille dans le Graphics
        '   LLeft,LTop  [E] :   Position de la ligne à écrire
        '
        '----------------------------------------------------------------------------------

        Dim xPos As Single = PosTitre(iTitre - 1) / 100 * sWi

        MyGr.DrawString(Ligne, PoliceTitre(iTitre - 1), BrushTitre(iTitre - 1), xPos, YPen)

        If iTitre = 1 Then
            Dim yTrait As Single
            Dim xFin As Single

            xFin = xPos + MyGr.MeasureString(Ligne, PoliceTitre(iTitre - 1)).Width - MyGr.MeasureString(" ", PoliceTitre(iTitre - 1)).Width / 2
            yTrait = YPen + 1.1 * PoliceTitre(iTitre - 1).GetHeight

            MyGr.DrawLine(PenTitre(iTitre - 1), xPos, yTrait, xFin, yTrait)
        End If

        HLigne = PoliceTitre(iTitre - 1).GetHeight * 1.5

    End Sub

    Private Sub DrawDessin(ByRef MyGr As Graphics, ByVal Ligne As String,
                           ByVal sWi As Single, ByVal sHI As Single, ByRef lSauteLigne As Boolean)
        '----------------------------------------------------------------------------------
        '
        '   14/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'un dessin dans le rapport (balise \IMG)
        '
        '----------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   Ligne       [E] :   Chaine de caractères contenant les instructions du dessin
        '   sWi, sHI    [E] :   Largeur et hauteur de l'objet recevant le dessin
        '   lSauteLigne [E] :   Indique si saut de ligne à la fin du dessin
        '
        '----------------------------------------------------------------------------------

        '--> Déclarations

        Dim Mots() As String = New String() {}
        Dim nMots As Integer
        Dim xLeftImg As Single
        Dim sWiImg, sHiImg As Single

        '--> Initialisation

        lSauteLigne = True

        DecomposeLine(Ligne, SEPARATEURS_NDC, Mots, nMots)

        If Mots(nMots).ToUpper = "\NOS" Then lSauteLigne = False

        '--> Traitement

        If nMots < 4 Then
            'Gestion Erreur

        Else

            xLeftImg = CSng(Mots(2)) * kECH * sWi / 100
            sWiImg = CSng(Mots(3)) * kECH * sWi / 100
            sHiImg = CSng(Mots(4)) * kECH * sWi / 100 * RAPPORTA4

            Select Case Mots(1).ToUpper

                Case "ARCELORMITTAL"

                    Dim MyPicture As Image

                    MyPicture = Image.FromFile(LogicielRep.Images & "\Logo_ARCELORMITTAL.jpg")
                    sHiImg = sWiImg * MyPicture.Height / MyPicture.Width
                    MyGr.DrawImage(MyPicture, xLeftImg, YPen, sWiImg, sHiImg)
                    MyPicture.Dispose()

                Case "CTICM"

                    Dim MyPicture As Image
                    'MyPicture = Image.FromFile(My.Application.Info.DirectoryPath & "\CTICM_logo.jpg")
                    'MyPicture = Image.FromFile(LogicielRep.Images & "\CTICM_logo.jpg")
                    MyPicture = Image.FromFile(LogicielRep.Images & "\Logo_CTICM_HR.jpg")
                    sHiImg = sWiImg * MyPicture.Height / MyPicture.Width
                    MyGr.DrawImage(MyPicture, xLeftImg, YPen, sWiImg, sHiImg)
                    MyPicture.Dispose()

                Case "PORTEE"

                    DessinFrmPortee(MyGr, MyProjet.Poutres(MyProjet.IndEnCours), PoliceEnCours, sWiImg, sHiImg, 0.9, -1, True, xLeftImg, YPen)

                Case "PORTEE_COUPE"

                    DessinFrmCoupe(MyGr, MyProjet.Poutres(MyProjet.IndEnCours), PoliceEnCours, sWiImg, sHiImg, 0.9, -1, True, xLeftImg, YPen)

                Case "PROFIL_ACIER"

                    Const kADJ As Decimal = 0.9
                    Const lCOTE As Boolean = True
                    Const lAFFS As Boolean = False

                    With MyProjet.Poutres(MyProjet.IndEnCours)
                        If .lSlimFloor Then
                            Select Case .Section.TypeSection
                                Case PMXMoteur2.cls_Section.Enum_TypeSection.IFB_A, PMXMoteur2.cls_Section.Enum_TypeSection.IFB_Amixte
                                    DessinProfileIFB_A_Acier(MyGr, .Section, .lIntermediaire, sWiImg, sHiImg, PoliceEnCours,
                                                             kADJ, lCOTE, lAFFS, -1, xLeftImg, YPen)

                                Case PMXMoteur2.cls_Section.Enum_TypeSection.IFB_B, PMXMoteur2.cls_Section.Enum_TypeSection.IFB_Bmixte
                                    DessinProfileIFB_B_Acier(MyGr, .Section, sWiImg, sHiImg, PoliceEnCours,
                                                             kADJ, lCOTE, lAFFS, -1, xLeftImg, YPen)

                                Case PMXMoteur2.cls_Section.Enum_TypeSection.SAB, PMXMoteur2.cls_Section.Enum_TypeSection.SABmixte
                                    DessinProfileSAB_Acier(MyGr, .Section, sWiImg, sHiImg, PoliceEnCours,
                                                             kADJ, lCOTE, lAFFS, -1, xLeftImg, YPen)

                                Case PMXMoteur2.cls_Section.Enum_TypeSection.SFB, PMXMoteur2.cls_Section.Enum_TypeSection.SFBmixte
                                    DessinProfileSFBAcier(MyGr, MyProjet.Poutres(MyProjet.IndEnCours).Section, sWiImg, sHiImg,
                                                          PoliceEnCours, kADJ, lCOTE, lAFFS, -1, xLeftImg, YPen)
                            End Select
                        Else
                            DessinProfileAcierN(MyGr, MyProjet.Poutres(MyProjet.IndEnCours).Section, sWiImg, sHiImg,
                                                PoliceEnCours, 0.9, True, False, -1, xLeftImg, YPen)
                        End If
                    End With


                Case "PROFIL_ACIER_DATABASE"

                    Dim Gamme As String = Mots(6)
                    Dim Profile As String = String.Join(" ", From Mot In Mots Where Array.IndexOf(Mots, Mot) >= 6)
                    Dim myProfileLoc As New PMXMoteur2.cls_Section

                    myProfileLoc.ProfilA.ha = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
                    myProfileLoc.ProfilA.Bfs = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
                    myProfileLoc.ProfilA.Bfi = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
                    myProfileLoc.ProfilA.Tfs = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
                    myProfileLoc.ProfilA.Tfi = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
                    myProfileLoc.ProfilA.Tw = MyCatalogue.Series(Gamme).Profiles(Profile).Tw
                    myProfileLoc.ProfilA.Rcs = MyCatalogue.Series(Gamme).Profiles(Profile).Rc
                    myProfileLoc.ProfilA.Rci = MyCatalogue.Series(Gamme).Profiles(Profile).Rc

                    DessinProfileAcier(MyGr, myProfileLoc, sWiImg, sHiImg, PoliceEnCours, 0.9, True, False, -1, xLeftImg, YPen)

                Case "PARTIAL_ENCASEMENT"

                    DessinFrmEnrobage(MyGr, MyProjet.Poutres(MyProjet.IndEnCours).Section, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage, sWiImg, sHiImg, 0.9, False, False, -1, xLeftImg, YPen)

                Case "SLAB"

                    Dim msgDessin(1) As String
                    DessineDalle(MyGr, sWiImg, sHiImg, MyProjet.Poutres(MyProjet.IndEnCours), PoliceEnCours, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire, -1, msgDessin, False, xLeftImg, YPen)

                Case "HIVOSS"

                    Dim MyDamp As Integer
                    Dim MyFreq As Decimal
                    Dim MyMass As Decimal
                    Dim strDamp As String

                    MyDamp = CInt(TraiteReal(Mots(6)))
                    MyFreq = CDec(TraiteReal(Mots(7)))
                    MyMass = CDec(TraiteReal(Mots(8)))
                    strDamp = Mots(9)

                    DessinCourbeHivoss(MyProjet.Poutres(MyProjet.IndEnCours).Hivoss, MyGr, xLeftImg, YPen, sWiImg, sHiImg, MyDamp, MyFreq, MyMass, strDamp)

                Case "CORRECT"

                    sHiImg = sWiImg * Frm_NoteCalcul.Btn_Correct.Image.Height / Frm_NoteCalcul.Btn_Correct.Image.Width
                    MyGr.DrawImage(Frm_NoteCalcul.Btn_Correct.Image, xLeftImg, YPen, sWiImg, sHiImg)

                Case "ERROR"

                    sHiImg = sWiImg * Frm_NoteCalcul.Btn_Error.Image.Height / Frm_NoteCalcul.Btn_Error.Image.Width
                    MyGr.DrawImage(Frm_NoteCalcul.Btn_Error.Image, xLeftImg, YPen, sWiImg, sHiImg)

                Case "RDM_CHARGESA"

                    Dim indiceCdc As Integer

                    indiceCdc = CInt(TraiteReal(Mots(6)))

                    DessineRDM(MyGr, sWiImg, sHiImg, MyProjet.Poutres(MyProjet.IndEnCours), indiceCdc, xLeftImg, YPen)

                Case "RDM_COMBI"

                    Dim indiceCombi As Integer
                    Dim typeCombo As String
                    Dim lRetraitELU As Boolean

                    indiceCombi = CInt(TraiteReal(Mots(6)))
                    typeCombo = Mots(7)
                    lRetraitELU = Mots(8)

                    DessineRDMCombi(MyGr, sWiImg, sHiImg, MyProjet.Poutres(MyProjet.IndEnCours), indiceCombi, typeCombo, lRetraitELU, xLeftImg, YPen)

                Case "FIRE_HEATING"

                    ' Représentation des courbes d'échauffement

                    DessineCourbeEchauffement(MyGr, sWiImg, sHiImg, MyProjet.Poutres(MyProjet.IndEnCours), xLeftImg, YPen)

                Case "CHAMPSTH"

                    Dim iStep As Integer = CInt(Mots(nMots))

                    DessineChampThSlim(MyGr, sWiImg, sHiImg, MyProjet.Poutres(MyProjet.IndEnCours), iStep, xLeftImg, YPen)

                Case "CHAMPSTH_LEGEND"
                    DessinLegende(MyGr, sWiImg, sHiImg, xLeftImg, YPen)


            End Select

            If nMots > 4 Then
                If Mots(5).ToUpper.Substring(0, 4) = "CADR" Then
                    MyGr.DrawRectangle(Pens.Black, xLeftImg, YPen, sWiImg, sHiImg)
                End If
            End If

            If lSauteLigne Then YPen += sHiImg

        End If

    End Sub

    'Private Sub DrawDiagramme(ByRef MyGr As Graphics, ByVal Ligne As String,
    '                       ByVal sWi As Single, ByVal sHI As Single)
    '    '----------------------------------------------------------------------------------
    '    '
    '    '   13/12/23 :  Creation GUD - Version 1.00
    '    '
    '    '----------------------------------------------------------------------------------
    '    '
    '    '   AffichageOptFeu des diagrammes d'un cas de charge d'un dessin dans le rapport (balise \DIA)
    '    '
    '    '----------------------------------------------------------------------------------
    '    '
    '    '   MyGr        [E] :   Graphics dans lequel on affiche
    '    '   Ligne       [E] :   Chaine de caractères contenant les instructions du dessin
    '    '   sWi, sHI    [E] :   Largeur et hauteur de l'objet recevant le dessin
    '    '
    '    '----------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim Mots() As String = New String() {}
    '    Dim nMots As Integer
    '    Dim xLeftImg As Single
    '    Dim sWiImg, sHiImg As Single

    '    '--> Initialisation

    '    DecomposeLine(Ligne, SEPARATEURS_NDC, Mots, nMots)

    '    '--> Traitement

    '    If nMots < 4 Then
    '        'Gestion Erreur

    '    Else
    '        Me.DiagrammesNDC.indCasDeChargeNDC = CInt(Mots(2))
    '        xLeftImg = CSng(Mots(3)) * kECH * sWi / 100
    '        sWiImg = CSng(Mots(4)) * kECH * sWi / 100
    '        sHiImg = CSng(Mots(5)) * kECH * sWi / 100 * RAPPORTA4

    '        Select Case Mots(1).ToUpper

    '            Case "RDM_CHARGESA"
    '                DessineRDM(MyGr, sWiImg, sHiImg, MyProjet.Poutres(MyProjet.IndEnCours), Me.DiagrammesNDC, Me.DiagrammesNDC.indCasDeChargeNDC, xLeftImg, YPen)
    '        End Select

    '        If nMots > 5 Then
    '            If Mots(6).ToUpper.Substring(0, 4) = "CADR" Then
    '                MyGr.DrawRectangle(Pens.Black, xLeftImg, YPen, sWiImg, sHiImg)
    '            End If
    '        End If
    '        YPen += sHiImg

    '    End If

    'End Sub

    Private Sub DrawTableau(ByVal ChainePos As String, ByRef MyGr As Graphics,
                            ByVal sWi As Single)
        '----------------------------------------------------------------------------------
        '
        '   22/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'un tableau
        '
        '----------------------------------------------------------------------------------
        '
        '   ChainePos   [E] :   Chaine indiquant la position du tableau
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   sWi         [E] :   Largeur du graphics
        '
        '----------------------------------------------------------------------------------

        Dim xPos As Single = CInt(ChainePos) * sWi / 100
        Dim lCont As Boolean = True
        Dim Mots() As String = New String() {}
        Dim nMots, nCells As Integer
        Dim hLigne As Single

        Do While lCont

            iLigne += 1

            Dim test = Me.MyPages
            Dim test0 = Me.MyPages(iPageEnCours)
            Dim test1 = Me.MyPages(iPageEnCours)(iLigne)
            Dim test2 = Me.MyPages(iPageEnCours)(iLigne).Trim
            Dim test3 = Me.MyPages(iPageEnCours)(iLigne).Trim.ToUpper
            lCont = Not (Me.MyPages(iPageEnCours)(iLigne).Trim.ToUpper.Substring(0, 4) = "\ETA")

            If lCont Then

                XPen = xPos

                DecomposeLine(Me.MyPages(iPageEnCours)(iLigne).Trim.ToUpper, SEPARATEURS, Mots, nMots)

                If nMots > 0 Then
                    If Mots(1).Substring(0, 4) = "LTAB" Then
                        nCells = CInt(Mots(2))
                        hLigne = CSng(TraiteReal((Mots(3)))) * PoliceEnCours.GetHeight
                        '--------------------------------------------------
                        '  Traitement du fond et des bords de ligne
                        '--------------------------------------------------
                        If nMots > 3 Then

                            Dim wLigne As Single = kECH * CInt(Mots(5)) * sWi / 100
                            For j As Integer = 6 To nMots
                                Select Case Mots(j).ToUpper.Substring(0, 4)
                                    Case "FOND"
                                        MyGr.FillRectangle(New SolidBrush(ColorCellFond), xPos, YPen, wLigne, hLigne)
                                    Case "BSUP"
                                        MyGr.DrawLine(Pens.Black, xPos, YPen, xPos + wLigne, YPen)
                                    Case "BINF"
                                        MyGr.DrawLine(Pens.Black, xPos, YPen + hLigne, xPos + wLigne, YPen + hLigne)
                                End Select
                            Next
                        End If
                        For i As Integer = 1 To nCells
                            iLigne += 1
                            Me.DrawCellule(MyGr, kECH * sWi, hLigne)
                        Next
                        YPen += hLigne
                    Else
                    End If
                End If

            End If

        Loop

    End Sub

    Private Sub DrawCellule(ByRef MyGr As Graphics, ByVal sWi As Single, ByVal hLigne As Single)
        '----------------------------------------------------------------------------------
        '
        '   22/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'une cellule de tableau
        '
        '----------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   sWi         [E] :   Largeur du graphics
        '
        '----------------------------------------------------------------------------------

        Dim Mots() As String = New String() {}
        Dim nMots As Integer
        Dim Largeur As Single
        Dim sWTexte, sHTexte As Single
        Dim BordureCellule As Single
        Dim Chaine As String

        DecomposeLine(Me.MyPages(iPageEnCours)(iLigne).Trim, SEPARATEURS, Mots, nMots)


        If nMots >= 4 Then
            Largeur = Val(Mots(2)) * sWi / 100

            '--[ AffichageOptFeu d'un fond de cellule
            If Mots(1).ToUpper = "CELF" Then
                MyGr.FillRectangle(New SolidBrush(ColorCellFond), XPen, YPen, Largeur, hLigne)
            ElseIf Mots(1).ToUpper = "CELC" Then
                MyGr.FillRectangle(New SolidBrush(ColorCellFond_Correct), XPen, YPen, Largeur, hLigne)
            ElseIf Mots(1).ToUpper = "CELE" Then
                MyGr.FillRectangle(New SolidBrush(ColorCellFond_Erreur), XPen, YPen, Largeur, hLigne)
            End If

            BordureCellule = CInt(Mots(3))

            '--[ Représentation des bordures

            If BordureCellule >= Bordures.Bas Then
                MyGr.DrawLine(Pens.Black, XPen, YPen + hLigne, XPen + Largeur, YPen + hLigne)
                BordureCellule -= Bordures.Bas
            End If
            If BordureCellule >= Bordures.Droite Then
                MyGr.DrawLine(Pens.Black, XPen + Largeur, YPen + hLigne, XPen + Largeur, YPen)
                BordureCellule -= Bordures.Droite
            End If
            If BordureCellule >= Bordures.Haut Then
                MyGr.DrawLine(Pens.Black, XPen, YPen, XPen + Largeur, YPen)
                BordureCellule -= Bordures.Haut
            End If
            If BordureCellule >= Bordures.Gauche Then
                MyGr.DrawLine(Pens.Black, XPen, YPen, XPen, YPen + hLigne)
            End If

            '--[ AffichageOptFeu du texte

            If nMots > 4 Then

                Dim Indice As Integer = Me.MyPages(iPageEnCours)(iLigne).Trim.IndexOf(":")

                Chaine = Me.MyPages(iPageEnCours)(iLigne).Trim.Substring(Indice + 1).Trim
                'sWTexte = MyGr.MeasureString(Chaine, PoliceEnCours).Width
                sWTexte = LongueurCellule(MyGr, Chaine)
                sHTexte = MyGr.MeasureString("X", PoliceEnCours).Height

                Dim xPos, yPos As Single
                Select Case Mots(4)
                    Case ClePos(PositionTexteInCell.Centre)
                        xPos = XPen + (Largeur - sWTexte) / 2
                        yPos = YPen + (hLigne - sHTexte) / 2
                    Case ClePos(PositionTexteInCell.Droite)
                        xPos = XPen + Largeur - sWTexte - sHTexte / 4
                        yPos = YPen + (hLigne - sHTexte) / 2
                    Case ClePos(PositionTexteInCell.Gauche)
                        xPos = XPen + sHTexte / 2
                        yPos = YPen + (hLigne - sHTexte) / 2
                End Select
                'MyGr.DrawString(Chaine, PoliceEnCours, Brushes.Black, xPos, yPos)
                EcrireCellule(MyGr, Chaine, xPos, yPos)

            End If

            '--[ Deplacement du stylo

            XPen += Largeur
        Else
        End If

    End Sub

    Private Sub EcrireCellule(ByVal MyGr As Graphics, ByVal Chaine As String,
                              ByVal xPen As Single, ByVal yPen As Single)
        '----------------------------------------------------------------------------------
        '
        '   25/02/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu du contenu d'une cellule de tableau
        '
        '----------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   Chaine      [E] :   Chaine de caractères à afficher (contient les codes de mises en forme)
        '   xPen,yPen   [E] :   Position initiale du stylo
        '
        '----------------------------------------------------------------------------------

        Dim Indice As Integer
        Dim Mot, Cle As String

        '--[ Recherche Mot Clé (repéré par antislash)

        Indice = Chaine.IndexOf(AntiSlash)

        If Indice = -1 Then
            '--[ Pas de mot clé : on écrit directement toute la ligne
            GetMyPolice()
            MyGr.DrawString(Chaine, PoliceEnCours, Brushes.Black, xPen, yPen + YDecal)
        Else
            '--[ Au moins un mot clé dans la ligne
            '--> Si le mot clé n'est pas au début, on écrit ce qu'il y a avant
            If Indice > 0 Then
                GetMyPolice()
                Mot = Chaine.Substring(0, Indice)
                MyGr.DrawString(Mot, PoliceEnCours, Brushes.Black, xPen, yPen + YDecal)
                xPen += MyGr.MeasureString(Mot, PoliceEnCours).Width - MyGr.MeasureString(" ", PoliceEnCours).Width
            End If
            Cle = Chaine.Substring(Indice + 1, 1)
            Select Case Cle
                Case "G" : Me.lGras = True  'Mise en gras
                Case "g" : Me.lGras = False 'Fin du gras
                Case "I" : Me.lItalic = True    'Mise en italique
                Case "i" : Me.lItalic = False   'Fin Italique
                Case "S" : Me.lSymbol = True    'Police Symbol
                Case "s" : Me.lSymbol = False   'Police Normale
                Case "=" : Me.StatutPolice = Enu_StatutPolice.Normal
                Case "+" : Me.StatutPolice = Enu_StatutPolice.Exposant
                Case "-" : Me.StatutPolice = Enu_StatutPolice.Indice
                Case "N", "n" : Me.lSymbol = False : Me.lGras = False : Me.lItalic = False
            End Select
            EcrireCellule(MyGr, Chaine.Substring(Indice + 2), xPen, yPen)
        End If
    End Sub

    Private Function LongueurCellule(ByVal MyGr As Graphics, ByVal Chaine As String) As Integer
        '----------------------------------------------------------------------------------
        '
        '   25/02/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   Calcul de la longueur d'une chaine dans une cellule de tableau
        '
        '----------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   Chaine      [E] :   Chaine de caractères à afficher (contient les codes de mises en forme)
        '
        '----------------------------------------------------------------------------------

        Dim Indice As Integer
        Dim Mot, Cle As String
        Dim Longueur As Integer = 0

        '--[ Recherche Mot Clé (repéré par antislash)

        Indice = Chaine.IndexOf(AntiSlash)

        If Indice = -1 Then
            '--[ Pas de mot clé : on écrit directement toute la ligne
            GetMyPolice()
            Longueur = MyGr.MeasureString(Chaine, PoliceEnCours).Width
        Else
            '--[ Au moins un mot clé dans la ligne
            '--> Si le mot clé n'est pas au début, on écrit ce qu'il y a avant
            If Indice > 0 Then
                GetMyPolice()
                Mot = Chaine.Substring(0, Indice)
                Longueur += MyGr.MeasureString(Mot, PoliceEnCours).Width - MyGr.MeasureString(" ", PoliceEnCours).Width
            End If
            Cle = Chaine.Substring(Indice + 1, 1)
            Select Case Cle
                Case "G" : Me.lGras = True  'Mise en gras
                Case "g" : Me.lGras = False 'Fin du gras
                Case "I" : Me.lItalic = True    'Mise en italique
                Case "i" : Me.lItalic = False   'Fin Italique
                Case "S" : Me.lSymbol = True    'Police Symbol
                Case "s" : Me.lSymbol = False   'Police Normale
                Case "=" : Me.StatutPolice = Enu_StatutPolice.Normal
                Case "+" : Me.StatutPolice = Enu_StatutPolice.Exposant
                Case "-" : Me.StatutPolice = Enu_StatutPolice.Indice
                Case "N", "n" : Me.lSymbol = False : Me.lGras = False : Me.lItalic = False
            End Select
            Longueur += LongueurCellule(MyGr, Chaine.Substring(Indice + 2))
        End If
        Return Longueur
    End Function

    Private Sub DrawJustifiedText(ByVal MargeG As Single, ByVal MargeD As Single, ByVal MyText As String, ByRef MyGr As Graphics, ByRef MyBrush As System.Drawing.Brush,
                                  ByVal sWi As Single)
        '----------------------------------------------------------------------------------
        '
        '   14/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'un texte justifié entre deux marges
        '
        '----------------------------------------------------------------------------------
        '
        '   Ligne       [E] :   Ligne à afficher
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   MyBrush     [E] :   Pinceau avec lequel on affiche
        '   sWi,sHI     [E] :   Largeur et hauteur de référence pour l'affichage de la page
        '
        '----------------------------------------------------------------------------------

        '--> Déclaration

        Dim Separateurs() As String = {" "}
        Dim Mots() As String = New String() {}
        Dim nMots As Integer
        Dim LigneIndexes As New List(Of List(Of Integer))
        Dim LongueurMax, LongueurDispo, LoSp, LoSpUse As Single
        Dim i, j As Integer
        Dim TailleMot() As Single
        Dim iEncours As Integer
        Dim iMot1, iMot2 As Integer
        Dim TailleLigne As Single
        Dim iLast As Integer

        '--> Initialisation

        GetMyPolice()
        LongueurMax = sWi * (100 - MargeG - MargeD) / 100
        'LoSp = MyGr.MeasureString(" ", PoliceEnCours, New PointF(0, 0), StringFormat.GenericTypographic).Width
        LoSp = MyGr.MeasureString(" ", PoliceEnCours).Width

        '--> Décomposition de la ligne

        DecomposeLine(MyText, Separateurs, Mots, nMots)
        If nMots = 0 Then Exit Sub

        '--> Construction des lignes à imprimer

        LigneIndexes.Add(New List(Of Integer))
        iEncours = 0
        LongueurDispo = LongueurMax
        ReDim TailleMot(nMots)

        For i = 1 To nMots
            TailleMot(i) = MyGr.MeasureString(Mots(i), PoliceEnCours).Width

            If TailleMot(i) > LongueurDispo Then
                LigneIndexes.Add(New List(Of Integer))
                iEncours += 1
                LigneIndexes(iEncours).Add(i)
                LongueurDispo = LongueurMax - TailleMot(i) - LoSp
            Else
                LongueurDispo -= (TailleMot(i) + LoSp)
                LigneIndexes(iEncours).Add(i)
            End If
        Next

        '--> AffichageOptFeu des lignes justifiées

        iLast = LigneIndexes.Count - 1

        For i = 0 To iLast
            XPen = sWi * MargeG / 100
            iMot1 = LigneIndexes(i)(0)
            iMot2 = LigneIndexes(i)(LigneIndexes(i).Count - 1)
            TailleLigne = TailleMot(iMot1)
            For j = iMot1 + 1 To iMot2
                TailleLigne += TailleMot(j)
            Next
            If i = iLast Then
                LoSpUse = LoSp
            Else
                LoSpUse = (LongueurMax - TailleLigne) / (iMot2 - iMot1)
            End If

            For j = iMot1 To iMot2

                If j > iMot1 Then XPen += (LoSpUse + TailleMot(j - 1))

                MyGr.DrawString(Mots(j), PoliceEnCours, MyBrush, XPen, YPen + YDecal)

            Next

            YPen += HLigne + HInterLigne
        Next

    End Sub

    Private Sub DrawLigne(ByRef Ligne As String,
                          ByRef MyGr As Graphics, ByRef MyBrush As System.Drawing.Brush,
                          ByVal lImprimante As Boolean,
                          ByVal sWi As Single, ByVal sHI As Single)
        '----------------------------------------------------------------------------------
        '
        '   14/01/08 :  Creation - Version 1.00
        '
        '----------------------------------------------------------------------------------
        '
        '   AffichageOptFeu d'une ligne
        '
        '----------------------------------------------------------------------------------
        '
        '   Ligne       [E] :   Ligne à afficher
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   MyBrush     [E] :   Pinceau avec lequel on affiche
        '   lImprimante [E] :   Indique si impression écran ou imprimante
        '   sWi,sHI     [E] :   Largeur et hauteur de référence pour l'affichage de la page
        '   YTop        [E] :   Position du Top de la feuille dans le Graphics
        '
        '----------------------------------------------------------------------------------

        Dim Indice As Integer
        Dim Mot, Cle As String
        Dim Length As Integer = Ligne.Length
        Dim MyText As String

        HLigne = 0

        '--[ Recherche Mot Clé (repéré par antislash)

        Indice = Ligne.IndexOf(AntiSlash)

        If Indice = -1 Then
            '--[ Pas de mot clé : on écrit directement toute la ligne
            GetMyPolice()
            'DrawAxeEtiq(MyGr, LLeft, LTop)
            MyGr.DrawString(Ligne, PoliceEnCours, MyBrush, XPen, YPen + YDecal)
        Else
            '--[ Au moins un mot clé dans la ligne
            '--> Si le mot clé n'est pas au début, on écrit ce qu'il y a avant
            If Indice > 0 Then
                GetMyPolice()
                Mot = Ligne.Substring(0, Indice)
                MyGr.DrawString(Mot, PoliceEnCours, MyBrush, XPen, YPen + YDecal)
                'DrawAxeEtiq(MyGr, LLeft, LTop)
                'XPen += (MyGr.MeasureString(Mot, PoliceEnCours).Width - MyGr.MeasureString(" ", PoliceEnCours).Width) * kFont

                '===13/01/10 : fonction correcte de mesure de chaine de caractères - 
                '=== Récupéré sur http://www.vbfrance.com/codes/IMPRESSION-FACILE-EVITANT-BUGS-FRAMEWORK_50783.aspx
                'XPen += MyGr.MeasureString(Mot, PoliceEnCours).Width - MyGr.MeasureString(" ", PoliceEnCours).Width
                XPen += MyGr.MeasureString(Mot, PoliceEnCours, New PointF(0, 0), StringFormat.GenericTypographic).Width
            End If

            '--> Traitement du mot clé

            Cle = Ligne.Substring(Indice + 1, Math.Min(3, Length - Indice - 1)).ToUpper
            Dim lSuite As Boolean   'Indicateur pour savoir si il faut traiter la suite comme une ligne, c'est à dire, que l'on va à la ligne si c'est vrai
            Dim iMotCle As Integer

            Select Case Cle
                Case "NOS"
                    lRetourCharriot = False
                    lSuite = True : iMotCle = 3
                Case "TAB"
                    lSuite = False : iMotCle = 3    'Debut d'un tableau
                    Me.DrawTableau(Ligne.Trim.Substring(Ligne.Trim.LastIndexOf(" ") + 1), MyGr, sWi)
                Case "SOM"
                    lSuite = True : iMotCle = 3
                Case "ETA"                        'Fin de tableau  
                    lSuite = False : iMotCle = 3
                Case "LUW"
                    lSuite = False : iMotCle = 3
                Case "BAL"                        'Balise de position YPen
                    lSuite = False
                    YBalise = YPen
                Case "RBA"                        'Retour sur balise
                    lSuite = False
                    YPen = YBalise
                Case "IMG"                          'AffichageOptFeu d'un dessin
                    lSuite = False
                    Me.DrawDessin(MyGr, Ligne.Substring(4), sWi, sHI, lRetourCharriot)
                Case "IMF"                          'AffichageOptFeu d'un dessin à une position forcée
                    lSuite = False                  'sur la dernière balise
                    Dim YPenBack As Single = YPen
                    YPen = YBalise
                    Me.DrawDessin(MyGr, Ligne.Substring(4), sWi, sHI, lRetourCharriot)
                    YPen = Math.Max(YPenBack, YPen + HLigne)
                    HLigne = 0

                Case "TI0", "TI1"                        'Titre principal
                    lSuite = False
                    DrawTitreDoc(Ligne.Substring(Indice + 4), MyGr, CInt(Cle.Substring(2)), sWi)

                Case "W01", "W02", "W03"          'Titre niveau 1, 2 ou 3
                    lSuite = False
                    Me.DrawTitre(Ligne.Substring(Indice + 4), MyGr, CInt(Cle.Substring(1)), sWi)

                Case "TW1", "TW2", "TW3"        'Alignement sur titre 1, 2 ou 3
                    XPen = (PosTitre(CInt(Cle.Substring(2)) - 1) + 5) / 100 * sWi
                    lSuite = True
                    iMotCle = 3

                Case "JUY"
                    lSuite = False
                    Dim MargeG, MargeD As Single

                    MargeG = Val(Ligne.Substring(5, 2))
                    MargeD = Val(Ligne.Substring(8, 2))
                    MyText = Ligne.Substring(11)
                    DrawJustifiedText(MargeG, MargeD, MyText, MyGr, MyBrush, sWi)

                Case Else
                    Cle = Ligne.Substring(Indice + 1, 1)
                    lSuite = True
                    iMotCle = 1
                    Select Case Cle
                        Case "U" : Me.lSouligne = True  'Mise en souligné
                        Case "u" : Me.lSouligne = False 'Fin du souligné
                        Case "G" : Me.lGras = True      'Mise en gras
                        Case "g" : Me.lGras = False     'Fin du gras
                        Case "I" : Me.lItalic = True    'Mise en italique
                        Case "i" : Me.lItalic = False   'Fin Italique
                        Case "S" : Me.lSymbol = True    'Police Symbol
                        Case "s" : Me.lSymbol = False   'Police Normale
                        Case "=" : Me.StatutPolice = Enu_StatutPolice.Normal
                        Case "+" : Me.StatutPolice = Enu_StatutPolice.Exposant
                        Case "-" : Me.StatutPolice = Enu_StatutPolice.Indice
                        Case "N", "n" : Me.lSymbol = False : Me.lGras = False : Me.lItalic = False
                        Case "T"    'Tabulation
                            lSuite = True
                            iMotCle = 3
                            'XPen = (CInt(Ligne.Substring(Indice + 2, 2)) - 1) / 100 * sWi
                            'Fonction max : on ne revient pas en arriere
                            'XPen = Math.Max(XPen, (CInt(Ligne.Substring(Indice + 2, 2)) - 1) / 100 * sWi)
                            XPen = Math.Max(XPen + Me.HReference / 2, (CInt(Ligne.Substring(Indice + 2, 2)) - 1) / 100 * sWi)
                        Case "C"
                        Case "L"    'Légende
                            lSuite = False
                            Me.DrawLegende(Ligne.Substring(Indice + 4), MyGr, CInt(Ligne.Substring(Indice + 2, 2)), sWi, MyBrush)

                    End Select

            End Select
            If lSuite Then
                DrawLigne(Ligne.Substring(Indice + 1 + iMotCle), MyGr, MyBrush, lImprimante, sWi, sHI)
            End If
        End If

    End Sub

    Private Sub DrawAxeEtiq(ByRef MyGr As Graphics, ByRef LLeft As Single, ByVal LTop As Single)

        MyGr.DrawLine(Pens.DarkOrange, LLeft, LTop + 10, LLeft, LTop + 20)

    End Sub

#End Region

#Region "   Outils "

    Public Sub GetTitles(ByRef Titres() As String,
                         ByRef NiveauTitre() As Integer,
                         ByRef PageTitre() As Integer,
                         ByRef nbTitres As Integer)
        '------------------------------------------------------------------------------------------------
        '
        '   Fournit la liste des titres de la note
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Titres      [S] :   Table des titres
        '   NiveauTitre [S] :   Table des niveaux des titres
        '   PageTitre   [S] :   Table des pages des titres
        '   nbTitres    [S] :   Nombre de titres
        '
        '------------------------------------------------------------------------------------------------

        '--[ Déclarations

        Dim iPage As Integer
        Dim iLigne, nbLignes As Integer
        Dim iTitle As Integer

        '--[ Initialisation

        nbTitres = 0

        '--[ Traitement : boucle sur les pages et les lignes 

        For iPage = 0 To Me.nbPages - 1

            nbLignes = Me.MyPages(iPage).Count()
            For iLigne = 0 To nbLignes - 1
                If Me.MyPages(iPage).Item(iLigne).Length > 4 Then

                    iTitle = Me.IsTitle(Me.MyPages(iPage).Item(iLigne))
                    If iTitle <> -1 Then
                        nbTitres += 1
                        If nbTitres = 1 Then
                            ReDim Titres(nbTitres - 1)
                            ReDim NiveauTitre(nbTitres - 1)
                            ReDim PageTitre(nbTitres - 1)
                        Else
                            ReDim Preserve Titres(nbTitres - 1)
                            ReDim Preserve NiveauTitre(nbTitres - 1)
                            ReDim Preserve PageTitre(nbTitres - 1)
                        End If

                        Titres(nbTitres - 1) = Me.MyPages(iPage).Item(iLigne).Trim.Substring(iTitle)
                        NiveauTitre(nbTitres - 1) = Val(Me.MyPages(iPage).Item(iLigne).Trim.Substring(iTitle - 1, 1))
                        PageTitre(nbTitres - 1) = iPage

                    End If

                    If Me.MyPages(iPage).Item(iLigne).Trim.Substring(0, 3) = "\W0" Then
                    End If
                End If
            Next

        Next

    End Sub

    Private Function IsTitle(ByVal Ligne As String) As Integer
        '-------------------------------------------------------------------------------------
        '
        '   30/06/08 :  Création - Version 1.00
        '   04/05/10 :  Modification - R10-003
        '
        '-------------------------------------------------------------------------------------
        '
        '   Indique si une ligne de la note est un titre ou non
        '   R10-003 - Correction Bug
        '
        '-------------------------------------------------------------------------------------
        '
        '   Chaine  [E] :   Chaine testée
        '
        '-------------------------------------------------------------------------------------
        '
        '   Renvoie -1 si ce n'est pas un titre,
        '   Si c'est un titre renvoie la position du premier caractère non clé de la ligne
        '
        '-------------------------------------------------------------------------------------

        '--[ Déclarations

        Dim iTitle As Integer
        Dim nMax, nLen As Integer    '==R10-016

        '--[ Initialisation

        iTitle = -1
        nMax = Ligne.Length    '==R10-016

        '--[ Recherche d'un titre en \W0

        nLen = Math.Min(3, nMax)    '==R10-016
        If Ligne.Trim.Substring(0, nLen) = "\W0" Then

            iTitle = 4

        End If

        '--[ Recherche d'un titre en \NOS\W0

        nLen = Math.Min(7, nMax)    '==R10-016
        If Ligne.Trim.Substring(0, nLen) = "\NOS\W0" Then

            iTitle = 8

        End If

        Return iTitle

    End Function

#End Region

#Region "Numérotation automatique des chapitres"

    Public Function GetNumerotationTitre(ByVal Niveau As Integer) As String

        Dim Numero As String = String.Empty

        If Me.lNumTitre(Niveau - 1) Then
            For I = 1 To Niveau - 1
                Select Case I
                    Case 1
                        Numero = Me.GetNumerotationTitre(I)
                    Case Else
                        Numero = Numero & "." & Me.GetNumerotationTitre(I)
                End Select
            Next
            Select Case Niveau
                Case 1
                    Numero = ChiffresRomains(Me.IndTitre(0))
                Case Else
                    Numero = Numero & "." & CStr(Me.IndTitre(Niveau - 1))
            End Select
        End If

        Return Numero
    End Function

#End Region

End Class
