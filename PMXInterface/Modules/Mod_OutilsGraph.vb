Imports System.Drawing
Imports System.Drawing.Drawing2D

#Region "Déclarations générales"

Public Class ConstantesDessin3D
    Public Shared ROTDEFAUT As Integer = 0
    Public Shared ROTTOUT As Integer = 1
    Public Shared ROTUTIL As Integer = 2
    Public Shared RAPPORTCADRE As Single = 0.9
End Class

Public Enum Enu_AlignementH
    Centre
    Gauche
    Droite
End Enum

Public Structure Struc_Affichage
    Dim uOri, vOri As Double
    Dim CRed As Double
    Dim XpMin, YpMin As Double
End Structure

#End Region

Module Mod_OutilsGraph

#Region "   Textes "

    Public Enum VerticalAlignement
        Bottom
        Middle
        Top
    End Enum

    Sub AddTexte(ByRef MyGr As Graphics, ByVal MyPenBrush As Brush, ByVal Chaine As String, ByVal MyFont As Font, _
             ByVal xo As Double, ByVal yo As Double, ByVal ParAff As struc_Affichage, _
             ByVal HAlign As HorizontalAlignment, ByVal VAlign As VerticalAlignement)
        AddTexte(MyGr, MyPenBrush, Chaine, MyFont, xo, yo, ParAff, HAlign, VAlign, False, Pens.Black)
    End Sub

    Sub AddTexte(ByRef MyGr As Graphics, ByVal MyPenBrush As Brush, ByVal Chaine As String, ByVal MyFont As Font, _
                 ByVal xo As Double, ByVal yo As Double, ByVal ParAff As struc_Affichage, _
                 ByVal HAlign As HorizontalAlignment, ByVal VAlign As VerticalAlignement, _
                 ByVal lEntoure As Boolean, ByVal MyPen As Pen)
        '-------------------------------------------------------------------------------------
        '
        '   Dessin d'un texte dans un Graphics
        '
        '-------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics recevant le dessin
        '   MyPenBrush  [E] :
        '   Chaine      [E] :   Chaine de caractères à afficher
        '   MyFont      [E] :   Police de caractères pour l'affichage de la chaine
        '   xo, yo      [E] :   Position de la chaine (Coordonnées de l'objet)
        '   ParAff      [E] :   Paramètres de l'affichage
        '   HAlign      [E] :   Positionnement horizontal par rapport à xo
        '   VAlign      [E] :   Positionnement vertical par rapport à yo
        '   lEntoure    [E] :   Indique si on entoure le texte
        '   MyPen       [E] :   Stylo pour dessine l'entourage
        '
        '-------------------------------------------------------------------------------------

        Dim xEo As Single = xEcran(ParAff, xo)
        Dim yEo As Single = yEcran(ParAff, yo)
        Dim xDecal, yDecal As Single
        Const FLOU As Single = 2

        Dim SizeChaine As SizeF = MyGr.MeasureString(Chaine, MyFont)

        Select Case HAlign
            Case HorizontalAlignment.Center
                xDecal = SizeChaine.Width / 2
            Case HorizontalAlignment.Right
                xDecal = 0
            Case HorizontalAlignment.Left
                xDecal = SizeChaine.Width
        End Select
        Select Case VAlign
            Case VerticalAlignement.Top
                yDecal = SizeChaine.Height
            Case VerticalAlignement.Middle
                yDecal = SizeChaine.Height / 2
            Case VerticalAlignement.Bottom
                yDecal = 0
        End Select

        If lEntoure Then
            MyGr.DrawRectangle(MyPen, xEo - xDecal - FLOU, yEo - yDecal, SizeChaine.Width + 2 * FLOU, SizeChaine.Height)
        End If
        MyGr.DrawString(Chaine, MyFont, MyPenBrush, xEo - xDecal, yEo - yDecal)
    End Sub

    Sub AddTexteFond(ByRef MyGr As Graphics, ByVal MyPenBrush As Brush, ByVal Chaine As String, ByVal MyFont As Font,
                     ByVal xo As Single, ByVal yo As Single, ByVal ParAff As Struc_Affichage,
                     ByVal HAlign As HorizontalAlignment, ByVal VAlign As VerticalAlignement,
                     ByVal MyBrushFond As Brush, ByVal MyPen As Pen, Optional lContour As Boolean = True)
        '-------------------------------------------------------------------------------------
        '
        '   Dessin d'un texte sur un fond et entouré dans un Graphics
        '
        '-------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics recevant le dessin
        '   MyPenBrush  [E] :
        '   Chaine      [E] :   Chaine de caractères à afficher
        '   MyFont      [E] :   Police de caractères pour l'affichage de la chaine
        '   xo, yo      [E] :   Position de la chaine (Coordonnées de l'objet)
        '   ParAff      [E] :   Paramètres de l'affichage
        '   HAlign      [E] :   Positionnement horizontal par rapport à xo
        '   VAlign      [E] :   Positionnement vertical par rapport à yo
        '   MyBrushFond [E] :   Pinceau pour le fond
        '   MyPen       [E] :   Stylo pour dessiner l'entourage
        '
        '-------------------------------------------------------------------------------------

        Dim xEo As Single = XEcran(ParAff, xo)
        Dim yEo As Single = YEcran(ParAff, yo)
        Dim xDecal, yDecal As Single
        Const FLOU As Single = 2

        Dim SizeChaine As SizeF = MyGr.MeasureString(Chaine, MyFont)

        Select Case HAlign
            Case HorizontalAlignment.Center
                xDecal = SizeChaine.Width / 2
            Case HorizontalAlignment.Left
                xDecal = 0
            Case HorizontalAlignment.Right
                xDecal = SizeChaine.Width
        End Select
        Select Case VAlign
            Case VerticalAlignement.Bottom
                yDecal = SizeChaine.Height
            Case VerticalAlignement.Middle
                yDecal = SizeChaine.Height / 2
            Case VerticalAlignement.Top
                yDecal = 0
        End Select

        MyGr.FillRectangle(MyBrushFond, xEo - xDecal - FLOU, yEo - yDecal, SizeChaine.Width + 2 * FLOU, SizeChaine.Height)
        If lContour Then MyGr.DrawRectangle(MyPen, xEo - xDecal - FLOU, yEo - yDecal, SizeChaine.Width + 2 * FLOU, SizeChaine.Height)

        MyGr.DrawString(Chaine, MyFont, MyPenBrush, xEo - xDecal, yEo - yDecal)
    End Sub

    Public Sub AddTabTextFond(ByRef MyGr As Graphics, ByVal MyPenBrush As Brush, ByVal Chaine As List(Of String), ByVal MyFont As Font,
                              ByVal xo As Single, ByVal yo As Single, ByVal ParAff As Struc_Affichage,
                              ByVal HAlignCadre As HorizontalAlignment, ByVal HAlignTexte As HorizontalAlignment, ByVal VAlign As VerticalAlignement,
                              ByVal MyBrushFond As Brush, ByVal MyPen As Pen)
        '-------------------------------------------------------------------------------------
        '   Dessin d'un texte sur un fond et entouré dans un Graphics
        '-------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics recevant le dessin
        '   MyPenBrush  [E] :
        '   Chaine      [E] :   Table de chaines de caractères à afficher
        '   MyFont      [E] :   Police de caractères pour l'affichage de la chaine
        '   xo, yo      [E] :   Position de la chaine (Coordonnées de l'objet)
        '   ParAff      [E] :   Paramètres de l'affichage
        '   HAlign      [E] :   Positionnement horizontal par rapport à xo
        '   VAlign      [E] :   Positionnement vertical par rapport à yo
        '   MyBrushFond [E] :   Pinceau pour le fond
        '   MyPen       [E] :   Stylo pour dessiner l'entourage
        '
        '-------------------------------------------------------------------------------------

        '--> Déclaration et initialisation

        Dim xEo As Single = XEcran(ParAff, xo)
        Dim yEo As Single = YEcran(ParAff, yo)
        Dim xDecal, yDecal As Single
        Dim xG, yG, xText, yText As Single
        Const FLOU As Single = 2
        Dim kX, yCum As Single

        Dim SizeChaine() As SizeF
        Dim Wmax, Hmax As Single
        Dim NbChaine As Integer
        Dim i As Integer

        '--> Initialisation

        NbChaine = Chaine.Count
        If NbChaine < 1 Then Exit Sub
        ReDim SizeChaine(NbChaine - 1)

        For i = 0 To Chaine.Count - 1
            SizeChaine(i) = MyGr.MeasureString(Chaine(i), MyFont)
            Wmax = Math.Max(SizeChaine(i).Width, Wmax)
            Hmax += SizeChaine(i).Height
        Next

        '--> Representation du cadre

        Select Case HAlignCadre
            Case HorizontalAlignment.Center
                xDecal = Wmax / 2
            Case HorizontalAlignment.Left
                xDecal = 0
            Case HorizontalAlignment.Right
                xDecal = Wmax
        End Select
        Select Case HAlignTexte
            Case HorizontalAlignment.Center
                kX = 0.5
            Case HorizontalAlignment.Left
                kX = 0
            Case HorizontalAlignment.Right
                kX = 1
        End Select
        Select Case VAlign
            Case VerticalAlignement.Bottom
                yDecal = 0
            Case VerticalAlignement.Middle
                yDecal = Hmax / 2
            Case VerticalAlignement.Top
                yDecal = Hmax
        End Select

        xG = xEo - xDecal
        yG = yEo - yDecal

        MyGr.FillRectangle(MyBrushFond, xEo - xDecal - FLOU, yEo - yDecal, Wmax + 2 * FLOU, Hmax)
        MyGr.DrawRectangle(MyPen, xEo - xDecal - FLOU, yEo - yDecal, Wmax + 2 * FLOU, Hmax)

        '--> Affichage des textes dans le cadre

        yCum = 0
        For i = 0 To NbChaine - 1
            yText = yG + yCum
            xText = xG + (Wmax - SizeChaine(i).Width) * kX
            MyGr.DrawString(Chaine(i), MyFont, MyPenBrush, xText, yText)
            yCum += SizeChaine(i).Height
        Next


    End Sub


    Sub AddTexteVertical(ByRef MyGr As Graphics, ByVal MyPenBrush As Brush, ByVal Chaine As String, ByVal MyFont As Font, _
                         ByVal xo As Double, ByVal yo As Double, ByVal ParAff As struc_Affichage, _
                         ByVal HAlign As HorizontalAlignment, ByVal VAlign As VerticalAlignement)
        '-------------------------------------------------------------------------------------
        '
        '   Dessin d'un texte dans un Graphics
        '
        '-------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics recevant le dessin
        '   MyPenBrush  [E] :
        '   Chaine      [E] :   Chaine de caractères à afficher
        '   MyFont      [E] :   Police de caractères pour l'affichage de la chaine
        '   xo, yo      [E] :   Position de la chaine (Coordonnées de l'objet)
        '   ParAff      [E] :   Paramètres de l'affichage
        '   HAlign      [E] :   Positionnement horizontal par rapport à xo
        '   VAlign      [E] :   Positionnement vertical par rapport à yo
        '   lEntoure    [E] :   Indique si on entoure le texte
        '   MyPen       [E] :   Stylo pour dessine l'entourage
        '
        '-------------------------------------------------------------------------------------

        Dim xEo As Single = xEcran(ParAff, xo)
        Dim yEo As Single = yEcran(ParAff, yo)
        Dim xDecal, yDecal As Single
        'Const FLOU As Single = 2

        Dim drawFormat As New System.Drawing.StringFormat With {
            .FormatFlags = StringFormatFlags.DirectionVertical
        }

        Dim SizeChaine As SizeF = MyGr.MeasureString(Chaine, MyFont)

        Select Case HAlign
            Case HorizontalAlignment.Center
                yDecal = SizeChaine.Width / 2
            Case HorizontalAlignment.Left
                yDecal = 0
            Case HorizontalAlignment.Right
                yDecal = SizeChaine.Width
        End Select
        Select Case VAlign
            Case VerticalAlignement.Bottom
                xDecal = 0
            Case VerticalAlignement.Middle
                xDecal = +SizeChaine.Height / 2
            Case VerticalAlignement.Top
                xDecal = +SizeChaine.Height
        End Select

        'If lEntoure Then
        '    MyGr.DrawRectangle(MyPen, xEo - xDecal - FLOU, yEo - yDecal, SizeChaine.Width + 2 * FLOU, SizeChaine.Height)
        'End If
        MyGr.DrawString(Chaine, MyFont, MyPenBrush, xEo - xDecal, yEo - yDecal, drawFormat)
    End Sub

#End Region

#Region "   Cercles "

    Sub AddCercleTronque(ByVal MyGr As Graphics, ByVal MyBrush As Brush, ByVal MyHatch As Brush, _
                         ByVal xC As Double, ByVal yC As Double, ByVal Diametre As Double, _
                         ByVal xCoupe As Double, ByVal lGauche As Boolean, ByVal MyParAff As struc_Affichage)
        '----------------------------------------------------------------------------------------
        '
        '   02/02/09 :  Création - Version 1.00 Beta 5 - POM
        '
        '----------------------------------------------------------------------------------------
        '
        '   Affichage de l'ouverture rebouchée aux extrémités
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau couleur de fond
        '   MyHatch     [E] :   Pinceau pour les Hachures
        '   xC,yC       [E] :   Coordonnées du centre
        '   Diametre    [E] :   Diametre du cercle
        '   xCoupe      [E] :   Abscisse de la coupure
        '   lGauche     [E] :   Indique si montant gauche ou droite
        '   MyParAff    [E] :   Parametres d'affichage
        '
        '----------------------------------------------------------------------------------------

        Dim xPts() As Single
        Dim yPts() As Single
        Dim Rayon As Double = Diametre / 2
        Dim UnDegre As Double = Math.PI / 180
        Dim Alpha1, Alpha2 As Double
        Dim Amplitude As Double
        Dim nbPts As Integer
        Const NBPTSMIN As Integer = 5
        Dim DeltaAlpha As Double

        If (Math.Abs(xCoupe - xC) > Rayon) Then
            Dim lDessine As Boolean
            If lGauche Then
                lDessine = (xCoupe < xC)
            Else
                lDessine = (xCoupe > xC)
            End If

            If lDessine Then
                AddCerclePlein(MyGr, MyBrush, xC, yC, Diametre, MyParAff, True)
                AddCerclePlein(MyGr, MyHatch, xC, yC, Diametre, MyParAff, False)
            End If
            Exit Sub
        End If

        '--[ Recherche des angles correspondant à la coupure

        Alpha1 = Math.Acos((xCoupe - xC) / Rayon)
        Alpha2 = -Alpha1

        '--[ Nombre de points de discrétisation

        If lGauche Then Amplitude = 2 * Alpha1 Else Amplitude = 2 * (Math.PI - Alpha1)

        nbPts = Math.Max(NBPTSMIN, CInt(Math.Floor(Amplitude / (5 * UnDegre))) + 1)

        ReDim xPts(nbPts)
        ReDim yPts(nbPts)

        '--[ Calcul du contour de la zone

        DeltaAlpha = Amplitude / nbPts
        If lGauche Then DeltaAlpha = -DeltaAlpha

        For i As Integer = 0 To nbPts

            xPts(i) = CSng(xC + Rayon * Math.Cos(i * DeltaAlpha + Alpha1))
            yPts(i) = CSng(yC + Rayon * Math.Sin(i * DeltaAlpha + Alpha1))

        Next

        '--[ Affichage de la zone

        RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts + 1, MyParAff, True)
        RemplirZone(MyGr, MyHatch, xPts, yPts, nbPts + 1, MyParAff, True)
    End Sub

    Sub AddCercleTronqueCourbe(ByVal MyGr As Graphics, ByVal MyBrush As Brush, ByVal MyHatch As Brush, _
                               ByVal AlphaC As Double, ByVal RCourbure As Double, ByVal Diametre As Double, _
                               ByVal AlphaCoupe As Double, ByVal lGauche As Boolean, ByVal MyParAff As struc_Affichage)
        '----------------------------------------------------------------------------------------
        '
        '   02/02/09 :  Création - Version 1.00 Beta 5 - POM
        '
        '----------------------------------------------------------------------------------------
        '
        '   Affichage de l'ouverture rebouchée aux extrémités d'une poutre courbe
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau couleur de fond
        '   MyHatch     [E] :   Pinceau pour les Hachures
        '   AlphaC      [E] :   Coordonnée angulaire du centre de l'ouverture
        '   RCourbure   [E] :   Rayon de courbure de la poutre
        '   Diametre    [E] :   Diametre du cercle
        '   AlphaCoupe  [E] :   Coordonnée angulaire de la coupure
        '   lGauche     [E] :   Indique si montant gauche ou droite
        '   MyParAff    [E] :   Parametres d'affichage
        '
        '----------------------------------------------------------------------------------------

        Dim xPts() As Single
        Dim yPts() As Single
        Dim Rayon As Double = Diametre / 2
        Dim UnDegre As Double = Math.PI / 180
        Dim Alpha1, Alpha2 As Double
        Dim Amplitude As Double
        Dim nbPts As Integer
        Const NBPTSMIN As Integer = 5
        Dim DeltaAlpha As Double
        Dim xC, yC, uC, vC As Double
        Dim xCoupe, yCoupe As Double

        '--[ Initisalisations

        xC = RCourbure * Math.Sin(AlphaC)
        yC = RCourbure * Math.Cos(AlphaC)

        xCoupe = RCourbure * Math.Sin(AlphaCoupe)
        yCoupe = RCourbure * Math.Cos(AlphaCoupe)

        'Coordonnées de la coupe dans le repere de l'ouverture

        uC = (xCoupe - xC) * Math.Cos(AlphaC) - (yCoupe - yC) * Math.Sin(AlphaC)
        vC = (xCoupe - xC) * Math.Sin(AlphaC) + (yCoupe - yC) * Math.Cos(AlphaC)

        If ((uC ^ 2 + vC ^ 2) > Rayon ^ 2) Then Exit Sub

        '--[ Recherche des angles correspondant à la coupure

        Alpha1 = Math.Acos(uC / Rayon)
        Alpha2 = -Alpha1

        '--[ Nombre de points de discrétisation

        If lGauche Then Amplitude = 2 * Alpha1 Else Amplitude = 2 * (Math.PI - Alpha1)

        nbPts = Math.Max(NBPTSMIN, CInt(Math.Floor(Amplitude / (5 * UnDegre))) + 1)

        ReDim xPts(nbPts)
        ReDim yPts(nbPts)

        '--[ Calcul du contour de la zone

        DeltaAlpha = Amplitude / nbPts
        If lGauche Then DeltaAlpha = -DeltaAlpha

        For i As Integer = 0 To nbPts

            xPts(i) = CSng(xC + Rayon * Math.Cos(i * DeltaAlpha + Alpha1 - AlphaC))
            yPts(i) = CSng(yC + Rayon * Math.Sin(i * DeltaAlpha + Alpha1 - AlphaC))

        Next

        '--[ Affichage de la zone

        RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts + 1, MyParAff, True)
        RemplirZone(MyGr, MyHatch, xPts, yPts, nbPts + 1, MyParAff, True)
    End Sub

    Sub AddCerclePlein(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                       ByVal xC As Double, ByVal yC As Double,
                       ByVal Diametre As Double,
                       ByVal ParAff As struc_Affichage, ByVal lContour As Boolean)
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'un cercle
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau 
        '   xC,yC       [E] :   Coordonnées du centre
        '   Diametre    [E] :   Diametre du cercle
        '   ParAff      [E] :   Paramètres d'affichage
        '   lContour    [E] :   Indique si l'on dessine le contour
        '
        '----------------------------------------------------------------------------------------

        Dim xEo As Single = xEcran(ParAff, xC)
        Dim yEo As Single = yEcran(ParAff, yC)
        Dim DiaE As Single = CSng(ParAff.CRed * Diametre)

        MyGr.FillEllipse(MyBrush, xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE)

        If lContour Then MyGr.DrawEllipse(Pens.Black, xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE)

    End Sub

    Sub AddCerclePlein(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                       ByVal xC As Double, ByVal yC As Double,
                       ByVal Diametre As Double,
                       ByVal ParAff As Struc_Affichage, ByVal lContour As Boolean, ByVal pen As Pen)
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'un cercle
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau 
        '   xC,yC       [E] :   Coordonnées du centre
        '   Diametre    [E] :   Diametre du cercle
        '   ParAff      [E] :   Paramètres d'affichage
        '   lContour    [E] :   Indique si l'on dessine le contour
        '
        '----------------------------------------------------------------------------------------

        Dim xEo As Single = XEcran(ParAff, xC)
        Dim yEo As Single = YEcran(ParAff, yC)
        Dim DiaE As Single = CSng(ParAff.CRed * Diametre)

        MyGr.FillEllipse(MyBrush, xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE)

        If lContour Then MyGr.DrawEllipse(pen, xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE)

    End Sub

    Sub AddArc(ByRef MyGr As Graphics, ByVal MyBrush As Brush, ByVal Epai As Integer, ByVal xC As Decimal, ByVal yC As Decimal,
                       ByVal Diametre As Decimal, ByVal angleD As Decimal, ByVal angleA As Decimal,
                       ByVal ParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'un arc
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau 
        '   Epai     [E] :   Epaisseur du trait 
        '   xC,yC       [E] :   Coordonnées du centre
        '   Diametre    [E] :   Diametre du cercle
        '   angleD      [E] :   Angle de départ de l'arc
        '   angleA      [E] :   Angle d'arrivé de l'arc
        '   ParAff      [E] :   Paramètres d'affichage
        '   lContour    [E] :   Indique si l'on dessine le contour
        '
        '----------------------------------------------------------------------------------------

        Dim xEo As Decimal = XEcran(ParAff, xC)
        Dim yEo As Decimal = YEcran(ParAff, yC)
        Dim DiaE As Decimal = (ParAff.CRed * Diametre)
        Dim EpaiE As Decimal = (ParAff.CRed * Epai) / 2

        MyGr.DrawArc(New Pen(MyBrush, EpaiE), xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE, angleD, angleA)

    End Sub

    Sub AddCercleContour(ByRef MyGr As Graphics, ByVal MyBrush As Brush, ByVal ColorRemplissage As Color,
                         ByVal xC As Double, ByVal yC As Double, ByVal Diametre As Double,
                         EpaisseurContour As Decimal, ByVal ParAff As Struc_Affichage, ByVal lContour As Boolean)
        '----------------------------------------------------------------------------------------
        '
        '   Affichage du tour d'un cercle 
        '   --> Ajout BD - 09/04/2020
        '----------------------------------------------------------------------------------------
        '
        '   MyGr                [E] :   Graphics dans lequel on dessine
        '   MyBrush             [E] :   Pinceau 
        '   ColorRemplissage    [E] :   Couleur de remplissage du trou du cercle 
        '   xC,yC               [E] :   Coordonnées du centre
        '   Diametre            [E] :   Diametre du cercle
        '   EpaisseurContour    [E] :   Epaisseur du contour du cercle
        '   ParAff              [E] :   Paramètres d'affichage
        '   lContour            [E] :   Indique si l'on dessine le contour
        '
        '----------------------------------------------------------------------------------------

        Dim xEo As Single = XEcran(ParAff, xC)
        Dim yEo As Single = YEcran(ParAff, yC)
        Dim DiaE As Single = CSng(ParAff.CRed * Diametre)

        '--> Dessin du cercle complet = AddCerclePlein
        MyGr.FillEllipse(MyBrush, xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE)

        'Trait contour extérieur
        If lContour Then MyGr.DrawEllipse(Pens.Black, xEo - DiaE / 2, yEo - DiaE / 2, DiaE, DiaE)

        '--> Diamètre du deuxième cercle (DiametreCercle - EpaisseurContour)
        Dim DiaE2 As Single = CSng(ParAff.CRed * (Diametre - EpaisseurContour * 2))

        MyGr.FillEllipse(New SolidBrush(ColorRemplissage), xEo - DiaE2 / 2, yEo - DiaE2 / 2, DiaE2, DiaE2)

        'Trait contour intérieur
        If lContour Then MyGr.DrawEllipse(Pens.Black, xEo - DiaE2 / 2, yEo - DiaE2 / 2, DiaE2, DiaE2)

    End Sub

#End Region

#Region "   Rectangles "


    Sub AddRectanglePlein(ByRef MyGr As Graphics, ByVal Color As Color,
                          ByVal xo As Double, ByVal yo As Double,
                          ByVal xe As Double, ByVal ye As Double,
                          ByVal ParAff As Struc_Affichage, ByVal lContour As Boolean)

        Dim MyBrush As New SolidBrush(Color)

        AddRectanglePlein(MyGr, MyBrush, Pens.Black, xo, yo, xe, ye, ParAff, True, lContour)

        MyBrush.Dispose()
    End Sub
    Sub AddRectanglePlein(ByRef MyGr As Graphics,
                          ByVal MyBrush As Brush, ByVal MyPen As Pen,
                          ByVal xo As Double, ByVal yo As Double,
                          ByVal xe As Double, ByVal ye As Double,
                          ByVal ParAff As Struc_Affichage,
                          ByVal lRemplissage As Boolean, ByVal lContour As Boolean)
        '-----------------------------------------------------------------------------------------------------
        '
        '   11/02/08 :  Creation - v1.00
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   Affichage d'un rectangle plein
        '
        '-----------------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics recevant le dessin
        '   MyBrush     [E] :   Pinceau pour le remplissage
        '   MyPen       [E] :   Stylo pour le contour
        '   xo, yo      [E] :   Coordonnées du premier point definissant le rectangle
        '   xe, ye      [E] :   Coordonnées du second point definissant le rectangle
        '   ParAff      [E] :   Paramètres de l'affichage
        '   lRemplissage[E] :   Indique si remplissage
        '   lContour    [E] :   Indique si contour
        '
        '----------------------------------------------------------------------------------------------------

        Dim xEo As Single = XEcran(ParAff, xo)
        Dim xEe As Single = XEcran(ParAff, xe)
        Dim yEo As Single = YEcran(ParAff, yo)
        Dim yEe As Single = YEcran(ParAff, ye)

        If lRemplissage Then MyGr.FillRectangle(MyBrush, Math.Min(xEo, xEe), Math.Min(yEo, yEe), Math.Abs(xEe - xEo), Math.Abs(yEe - yEo))

        If lContour Then MyGr.DrawRectangle(MyPen, Math.Min(xEo, xEe), Math.Min(yEo, yEe), Math.Abs(xEe - xEo), Math.Abs(yEe - yEo))

    End Sub

#End Region

#Region " Lignes "

    ''' <summary>
    ''' Affichage à l'écran d'une ligne
    ''' </summary>
    ''' <param name="MyGr">     [E] Graphics                                                        </param>
    ''' <param name="xo">       [E] Coordonnées x origine de la ligne dans le repère de l'objet   </param>
    ''' <param name="yo">       [E] Coordonnées y origine de la ligne dans le repère de l'objet   </param>
    ''' <param name="xe">       [E] Coordonnées x extremite de la ligne dans le repère de l'objet </param>
    ''' <param name="ye">       [E] Coordonnées y extremite de la ligne dans le repère de l'objet </param>
    ''' <param name="ParAff">   [E] Paramètres d'affichage                                        </param>
    Sub AddLigne(ByRef MyGr As Graphics,
                 ByVal xo As Double, ByVal yo As Double,
                 ByVal xe As Double, ByVal ye As Double, ByVal ParAff As Struc_Affichage)

        Dim xEo As Single = XEcran(ParAff, xo)
        Dim xEe As Single = XEcran(ParAff, xe)
        Dim yEo As Single = YEcran(ParAff, yo)
        Dim yEe As Single = YEcran(ParAff, ye)

        MyGr.DrawLine(Pens.Black, xEo, yEo, xEe, yEe)

    End Sub

    Public Sub AddLignePolyG(ByRef MyGr As Graphics, xPts() As Single, yPts() As Single, nbPts As Integer, ByVal ParAff As Struc_Affichage)

        Dim xEo As Single
        Dim xEe As Single
        Dim yEo As Single
        Dim yEe As Single

        For i As Integer = 0 To nbPts - 2

            xEo = XEcran(ParAff, xPts(i))
            xEe = XEcran(ParAff, xPts(i + 1))
            yEo = YEcran(ParAff, yPts(i))
            yEe = YEcran(ParAff, yPts(i + 1))

            MyGr.DrawLine(Pens.Black, xEo, yEo, xEe, yEe)

        Next

    End Sub

    Sub AddLigne(ByRef MyGr As Graphics, ByRef MyPen As Pen,
             ByVal xo As Double, ByVal yo As Double,
             ByVal xe As Double, ByVal ye As Double, ByVal ParAff As Struc_Affichage)

        Dim xEo As Single = XEcran(ParAff, xo)
        Dim xEe As Single = XEcran(ParAff, xe)
        Dim yEo As Single = YEcran(ParAff, yo)
        Dim yEe As Single = YEcran(ParAff, ye)

        MyGr.DrawLine(MyPen, xEo, yEo, xEe, yEe)

    End Sub

#End Region

#Region " Flèches "

    Sub AddFlechePleinVert(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                       ByVal xP As Double, ByVal yP As Double,
                       ByVal Ht As Double, ByVal Epb As Double,
                       ByVal HPointe As Double, ByVal EpPointe As Double,
                       ByRef ParAff As Struc_Affichage, ByVal lContour As Boolean,
                       ByVal lAxe As Boolean)
        '-------------------------------------------------------------------------------------------------
        '   07/08/07 :  Création - Version 1.00
        '-------------------------------------------------------------------------------------------------
        '   Ajout d'une flèche verticale
        '-------------------------------------------------------------------------------------------------
        '   MyGr    [E] :   Graphics dans lequel on dessine
        '   MyBrush [E] :   Pinceau pour le remplissage de la flèche
        '   xP, yP  [E] :   Coordonnées de la pointe de la flèche
        '   Ht      [E] :   Hauteur de la flèche (coordonnée poutre)
        '   Epb     [E] :   Largeur de la jambe de la flèche
        '   HPoint  [E] :   Hauteur de l'extremite de la flèche
        '   EpPointe[E] :   Largeur de l'extremite de la flèche
        '   ParAff  [E] :   Paramètres de l'affichage
        '   lContour[E] :   Indique si la routine appelante requiert le tracé du contour de la flèche
        '   lAxe    [E] :   Indique si le tracé de l'axe est requis !!!
        '-------------------------------------------------------------------------------------------------

        Dim PtsFleche(6) As Point

        PtsFleche(0).X = CInt(XEcran(ParAff, xP))
        PtsFleche(0).Y = CInt(YEcran(ParAff, yP))
        PtsFleche(1).X = CInt(XEcran(ParAff, xP + EpPointe / 2))
        PtsFleche(1).Y = CInt(YEcran(ParAff, yP + HPointe))
        PtsFleche(2).X = CInt(XEcran(ParAff, xP + Epb / 2))
        PtsFleche(2).Y = CInt(YEcran(ParAff, yP + HPointe))
        PtsFleche(3).X = CInt(XEcran(ParAff, xP + Epb / 2))
        PtsFleche(3).Y = CInt(YEcran(ParAff, yP + Ht))
        PtsFleche(4).X = CInt(XEcran(ParAff, xP - Epb / 2))
        PtsFleche(4).Y = CInt(YEcran(ParAff, yP + Ht))
        PtsFleche(5).X = CInt(XEcran(ParAff, xP - Epb / 2))
        PtsFleche(5).Y = CInt(YEcran(ParAff, yP + HPointe))
        PtsFleche(6).X = CInt(XEcran(ParAff, xP - EpPointe / 2))
        PtsFleche(6).Y = CInt(YEcran(ParAff, yP + HPointe))

        MyGr.FillPolygon(MyBrush, PtsFleche)

        If lContour Then MyGr.DrawPolygon(Pens.Black, PtsFleche)

        Dim xo, yo, xe, ye As Single

        If lAxe Then
            xo = XEcran(ParAff, xP)
            yo = YEcran(ParAff, yP)
            xe = xo
            ye = YEcran(ParAff, yP + Ht)
            MyGr.DrawLine(Pens.Red, xo, yo, xe, ye)
        End If

    End Sub

    Sub AddFlechePleinHori(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                       ByVal xP As Double, ByVal yP As Double,
                       ByVal Ht As Double, ByVal Epb As Double,
                       ByVal HPointe As Double, ByVal EpPointe As Double,
                       ByRef ParAff As Struc_Affichage, ByVal lContour As Boolean,
                       ByVal lAxe As Boolean)
        '-------------------------------------------------------------------------------------------------
        '   07/08/07 :  Création - Version 1.00
        '-------------------------------------------------------------------------------------------------
        '   Ajout d'une flèche horizontale
        '-------------------------------------------------------------------------------------------------
        '   MyGr    [E] :   Graphics dans lequel on dessine
        '   MyBrush [E] :   Pinceau pour le remplissage de la flèche
        '   xP, yP  [E] :   Coordonnées de la pointe de la flèche
        '   Ht      [E] :   Hauteur de la flèche (coordonnée poutre)
        '   Epb     [E] :   Largeur de la jambe de la flèche
        '   HPoint  [E] :   Hauteur de l'extremite de la flèche
        '   EpPointe[E] :   Largeur de l'extremite de la flèche
        '   ParAff  [E] :   Paramètres de l'affichage
        '   lContour[E] :   Indique si la routine appelante requiert le tracé du contour de la flèche
        '   lAxe    [E] :   Indique si le tracé de l'axe est requis !!!
        '-------------------------------------------------------------------------------------------------

        Dim PtsFleche(6) As Point

        PtsFleche(0).X = CInt(XEcran(ParAff, xP))
        PtsFleche(0).Y = CInt(YEcran(ParAff, yP))
        PtsFleche(1).X = CInt(XEcran(ParAff, xP + HPointe))
        PtsFleche(1).Y = CInt(YEcran(ParAff, yP + EpPointe / 2))
        PtsFleche(2).X = CInt(XEcran(ParAff, xP + HPointe))
        PtsFleche(2).Y = CInt(YEcran(ParAff, yP + Epb / 2))
        PtsFleche(3).X = CInt(XEcran(ParAff, xP + Ht))
        PtsFleche(3).Y = CInt(YEcran(ParAff, yP + Epb / 2))
        PtsFleche(4).X = CInt(XEcran(ParAff, xP + Ht))
        PtsFleche(4).Y = CInt(YEcran(ParAff, yP - Epb / 2))
        PtsFleche(5).X = CInt(XEcran(ParAff, xP + HPointe))
        PtsFleche(5).Y = CInt(YEcran(ParAff, yP - Epb / 2))
        PtsFleche(6).X = CInt(XEcran(ParAff, xP + HPointe))
        PtsFleche(6).Y = CInt(YEcran(ParAff, yP - EpPointe / 2))

        MyGr.FillPolygon(MyBrush, PtsFleche)

        If lContour Then MyGr.DrawPolygon(Pens.Black, PtsFleche)

        Dim xo, yo, xe, ye As Single

        If lAxe Then
            xo = XEcran(ParAff, xP)
            yo = YEcran(ParAff, yP)
            xe = xo
            ye = YEcran(ParAff, yP + Ht)
            MyGr.DrawLine(Pens.Red, xo, yo, xe, ye)
        End If

    End Sub

    Sub AddFleche(ByRef MyGr As Graphics, ByVal MyPen As Pen, ByVal xo As Double, ByVal yo As Double,
                  ByVal xe As Double, ByVal ye As Double, ByRef ParAff As Struc_Affichage,
                  ByVal lOrigine As Boolean, ByVal lExtremite As Boolean)

        Dim xlo, ylo, xle, yle As Single
        xlo = XEcran(ParAff, xo)
        ylo = YEcran(ParAff, yo)
        xle = XEcran(ParAff, xe)
        yle = YEcran(ParAff, ye)

        MyGr.DrawLine(MyPen, xlo, ylo, xle, yle)

        '--> Caracteristiques vectorielles

        Dim sLFleche As Single
        sLFleche = CSng(Math.Sqrt((xle - xlo) ^ 2 + (yle - ylo) ^ 2))
        Dim ProdScal As Single
        ProdScal = (xle - xlo) * (CSng(1.967) - xlo) + (yle - ylo) * (CSng(1.17) - ylo)

        If sLFleche = 0 Then Exit Sub

        Dim VIx, VIy, VJx, VJy As Single, normeVj As Single

        VIx = (xle - xlo) / sLFleche
        VIy = (yle - ylo) / sLFleche
        VJx = CSng((1.967 - xlo) - ProdScal / sLFleche ^ 2 * (xle - xlo))
        VJy = CSng((1.17 - ylo) - ProdScal / sLFleche ^ 2 * (yle - ylo))
        normeVj = CSng(Math.Sqrt((VJx ^ 2 + VJy ^ 2)))

        VJx /= normeVj
        VJy /= normeVj

        '--> Ailes de la fleche

        Dim VAx, VAy As Double, sFaile As Single
        Dim cosinus As Double, Pente As Single
        Pente = 0.45
        sFaile = 5

        cosinus = (1 / (1 + Pente ^ 2)) ^ 0.5

        VAx = sFaile * cosinus * (-VIx + Pente * VJx)
        VAy = sFaile * cosinus * (-VIy + Pente * VJy)

        Dim kSigne As Single = 1
        If lExtremite Then
            MyGr.DrawLine(MyPen, xle, yle, xle + kSigne * CSng(VAx), yle + kSigne * CSng(VAy))
        End If
        If lOrigine Then
            MyGr.DrawLine(MyPen, xlo, ylo, xlo - kSigne * CSng(VAx), ylo - kSigne * CSng(VAy))
        End If

        VAx = -sFaile * cosinus * (VIx + Pente * VJx)
        VAy = -sFaile * cosinus * (VIy + Pente * VJy)

        If lExtremite Then
            MyGr.DrawLine(MyPen, xle, yle, xle + kSigne * CSng(VAx), yle + kSigne * CSng(VAy))
        End If
        If lOrigine Then
            MyGr.DrawLine(MyPen, xlo, ylo, xlo - kSigne * CSng(VAx), ylo - kSigne * CSng(VAy))
        End If

    End Sub

#End Region

#Region " Symboles "

    '''' <summary>
    '''' Fonction qui retourne la longueur d'un symbole (+indice) d'équation
    '''' </summary>
    '''' <param name="MyGr">Graphics dans lequel on dessine</param>
    '''' <param name="Symbol">Symbole à dessiner</param>
    '''' <param name="Indice">Indice du Symbole</param>
    '''' <param name="lGrec">Indice si symbole de l'alphabet grec</param>
    '''' <param name="FontNormal">Police de caractère normale</param>
    '''' <param name="FontSymbol">Police de caractère pour les symboles grecs</param>
    '''' <param name="FontIndice">Police de caractère pour les indices</param>
    '''' <param name="kAdjust">Ajustement de la position de l'indice</param>
    '''' <param name="DrawEgal">Dessin du signe = à la fin</param>
    '''' <returns>Longueur du symbole</returns>
    'Public Function LongueurChaine(ByVal MyGr As Graphics, ByVal Symbol As String,
    '                               ByVal Indice As String, ByVal lGrec As Boolean,
    '                               ByVal FontNormal As Font, ByVal FontSymbol As Font,
    '                               ByVal FontIndice As Font, ByVal kAdjust As Single, Optional ByVal DrawEgal As Boolean = False) As Single

    '    If lGrec Then
    '        If DrawEgal Then
    '            Return MyGr.MeasureString(Symbol, FontSymbol).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width + MyGr.MeasureString("=", FontNormal).Width
    '        Else
    '            Return MyGr.MeasureString(Symbol, FontSymbol).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width
    '        End If
    '    Else
    '        If DrawEgal Then
    '            Return MyGr.MeasureString(Symbol, FontNormal).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width + MyGr.MeasureString("=", FontNormal).Width
    '        Else
    '            Return MyGr.MeasureString(Symbol, FontNormal).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width
    '        End If
    '    End If

    'End Function

    Public Sub DrawSymbol_Dessin(ByVal MyGr As Graphics, ByVal BrushEcrire As Brush, ByVal Symbol As String,
                                 ByVal Indice As String, ByVal xo As Single, ByVal yo As Single,
                                 ByVal lGrec As Boolean, ByVal ParAff As Struc_Affichage,
                                 ByVal HAlign As HorizontalAlignment, ByVal VAlign As VerticalAlignement,
                                 ByVal FontNormal As Font, ByVal FontSymbol As Font, ByVal FontIndice As Font,
                                 ByVal kAdjust As Single)
        '----------------------------------------------------------------------------------------
        '
        '   21/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'un symbole (+indice) d'équation
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   MyBrush         [E] :   Pinceau pour écrire
        '   Symbol          [E] :   Symbole à dessiner
        '   Indice          [E] :   Indice du Symbole
        '   xo, yo          [E] :   Position de départ du stylo pour écrire
        '   yDecal          [E] :   Décalage de hauteur pour les indices
        '   HAlign, VAlign  [E] :   Alignement vertical et horizontal du symbole
        '   FontNormal      [E] :   Police de caractère normale
        '   FontSymbol      [E] :   Police de caractère pour les symboles grecs
        '   FontIndice      [E] :   Police de caractère pour les indices
        '   kAdjust         [E] :   Ajustement de la position de l'indice
        '
        '----------------------------------------------------------------------------------------

        Dim xEo As Single = XEcran(ParAff, xo)
        Dim yEo As Single = YEcran(ParAff, yo)
        Dim xDecal, yDecal As Single

        Dim LongueurString As Single = LongueurChaine(MyGr, Symbol, Indice, lGrec, FontNormal, FontSymbol, FontIndice, kAdjust)
        Dim HauteurString As Single = MyGr.MeasureString("X", FontNormal).Height

        Select Case HAlign
            Case HorizontalAlignment.Center
                xDecal = LongueurString / 2
            Case HorizontalAlignment.Right
                xDecal = 0
            Case HorizontalAlignment.Left
                xDecal = LongueurString
        End Select
        Select Case VAlign
            Case VerticalAlignement.Top
                yDecal = HauteurString
            Case VerticalAlignement.Middle
                yDecal = HauteurString / 2
            Case VerticalAlignement.Bottom
                yDecal = 0
        End Select

        If lGrec Then
            MyGr.DrawString(Symbol, FontSymbol, BrushEcrire, xEo - xDecal, yEo - yDecal)
            xDecal -= MyGr.MeasureString(Symbol, FontSymbol).Width - kAdjust * MyGr.MeasureString(" ", FontSymbol).Width
        Else
            MyGr.DrawString(Symbol, FontNormal, BrushEcrire, xEo - xDecal, yEo - yDecal)
            xDecal -= MyGr.MeasureString(Symbol, FontNormal).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width
        End If

        Dim DecalIndice As Single = HauteurString / 3

        MyGr.DrawString(Indice, FontIndice, BrushEcrire, xEo - xDecal, yEo - yDecal + DecalIndice)

    End Sub

    'Public Sub DrawSymbol(ByVal MyGr As Graphics, ByVal BrushEcrire As Brush,
    '                      ByVal Symbol As String, ByVal Indice As String,
    '                      ByVal xPen As Single, ByVal yPen As Single, ByVal yDecal As Single,
    '                      ByVal lGrec As Boolean, ByVal Alignement As Enu_Alignement,
    '                      ByVal FontNormal As Font, ByVal FontSymbol As Font,
    '                      ByVal FontIndice As Font, ByVal kAdjust As Single)
    '    '----------------------------------------------------------------------------------------
    '    '
    '    '   21/02/08 :  Création - Version 1.00
    '    '
    '    '----------------------------------------------------------------------------------------
    '    '
    '    '   Affichage d'un symbole (+indice) d'équation
    '    '
    '    '----------------------------------------------------------------------------------------
    '    '
    '    '   MyGr        [E] :   Graphics dans lequel on dessine
    '    '   MyBrush     [E] :   Pinceau pour écrire
    '    '   Symbol      [E] :   Symbole à dessiner
    '    '   Indice      [E] :   Indice du Symbole
    '    '   xPen, yPen  [E/S] :   Position du Stylo pour écrire
    '    '   yDecal      [E] :   Décalage de hauteur pour les indices
    '    '   lGrec       [E] :   Indice si symbole de l'alphabet grec
    '    '   Alignement  [E] :   Alignement du symbole
    '    '   FontNormal  [E] :   Police de caractère normale
    '    '   FontSymbol  [E] :   Police de caractère pour les symboles grecs
    '    '   FontIndice  [E] :   Police de caractère pour les indices
    '    '   kAdjust     [E] :   Ajustement de la position de l'indice
    '    '
    '    '----------------------------------------------------------------------------------------

    '    Dim Longueur As Single = LongueurChaine(MyGr, Symbol, Indice, lGrec,
    '                                            FontNormal, FontSymbol, FontIndice)
    '    Dim xStar As Single

    '    Select Case Alignement
    '        Case Enu_Alignement.Gauche
    '            xStar = xPen
    '        Case Enu_Alignement.Droite
    '            xStar = xPen - Longueur
    '        Case Enu_Alignement.Centre
    '            xStar = xPen - Longueur / 2
    '    End Select

    '    'Const kFact As Single = 1.0!

    '    If lGrec Then
    '        MyGr.DrawString(Symbol, FontSymbol, BrushEcrire, xStar, yPen)
    '        xStar += MyGr.MeasureString(Symbol, FontSymbol).Width - kAdjust * MyGr.MeasureString(" ", FontSymbol).Width
    '    Else
    '        MyGr.DrawString(Symbol, FontNormal, BrushEcrire, xStar, yPen)
    '        xStar += MyGr.MeasureString(Symbol, FontNormal).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width
    '    End If

    '    MyGr.DrawString(Indice, FontIndice, BrushEcrire, xStar, yPen + yDecal)

    'End Sub

    'Public Function LongueurChaine(ByVal MyGr As Graphics, ByVal Symbol As String,
    '                               ByVal Indice As String, ByVal lGrec As Boolean,
    '                               ByVal FontNormal As Font, ByVal FontSymbol As Font,
    '                               ByVal FontIndice As Font) As Single
    '    '----------------------------------------------------------------------------------------
    '    '
    '    '   21/02/08 :  Création - Version 1.00
    '    '
    '    '----------------------------------------------------------------------------------------
    '    '
    '    '   Fonction qui retourne la longueur d'un symbole (+indice) d'équation
    '    '
    '    '----------------------------------------------------------------------------------------
    '    '
    '    '   MyGr        [E] :   Graphics dans lequel on dessine
    '    '   Symbol      [E] :   Symbole à dessiner
    '    '   Indice      [E] :   Indice du Symbole
    '    '   lGrec       [E] :   Indice si symbole de l'alphabet grec
    '    '   FontNormal  [E] :   Police de caractère normale
    '    '   FontSymbol  [E] :   Police de caractère pour les symboles grecs
    '    '   FontIndice  [E] :   Police de caractère pour les indices
    '    '
    '    '----------------------------------------------------------------------------------------

    '    If lGrec Then
    '        Return MyGr.MeasureString(Symbol, FontSymbol).Width + MyGr.MeasureString(Indice, FontIndice).Width
    '    Else
    '        Return MyGr.MeasureString(Symbol, FontNormal).Width + MyGr.MeasureString(Indice, FontIndice).Width
    '    End If

    'End Function

#End Region

#Region " Remplir une zone quelconque "


    Public Sub RemplirZone(ByVal MyGr As Graphics, ByVal MyBrush As Brush,
                           ByVal xPts() As Single, ByVal yPts() As Single, ByVal nbPts As Integer,
                           ByVal parAff As Struc_Affichage, ByVal lContour As Boolean, Optional lRemplissage As Boolean = True)
        Dim PointsZone(nbPts - 1) As PointF


        For i As Integer = 0 To nbPts - 1
                PointsZone(i).X = (XEcran(parAff, xPts(i)))
                PointsZone(i).Y = (YEcran(parAff, yPts(i)))
            Next

        If lRemplissage Then
            MyGr.FillPolygon(MyBrush, PointsZone)
        End If

        If lContour Then
            MyGr.DrawPolygon(Pens.Black, PointsZone)
        End If

    End Sub

    Public Sub RemplirZone(ByVal MyGr As Graphics, ByVal MyBrush As Brush,
                           ByVal xPts() As Single, ByVal yPts() As Single, ByVal nbPts As Integer,
                           ByVal parAff As Struc_Affichage, ByVal lContour As Boolean, ByVal pen As Pen)
        Dim PointsZone(nbPts - 1) As PointF

        For i As Integer = 0 To nbPts - 1
            PointsZone(i).X = (XEcran(parAff, xPts(i)))
            PointsZone(i).Y = (YEcran(parAff, yPts(i)))
        Next

        MyGr.FillPolygon(MyBrush, PointsZone)

        If lContour Then
            MyGr.DrawPolygon(pen, PointsZone)
        End If
    End Sub

    Public Sub TraiteZone(ByVal MyGr As Graphics, ByVal MyBrush As Brush, ByVal MyPen As Pen, _
                          ByVal xPts As List(Of Single), ByVal yPts As List(Of Single), _
                          ByVal parAff As struc_Affichage, _
                          ByVal lContour As Boolean, ByVal lRemplissage As Boolean)
        '-------------------------------------------------------------------------------------
        '
        '   Affichage d'une zone définie par une liste de points dans un graphics
        '
        '-------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau pour le remplissage
        '   lRemplissage[E] :   Indique si l'on remplit la zone (avec le pinceau)
        '   MyPen       [E] :   Stylo pour le contour
        '   lContour    [E] :   Indique si l'on trace le contour (avec le stylo)
        '   xPts, yPts  [E] :   Liste des points formant le contour fermé de la zone à traiter
        '                       (Dans le repère de l'objet dessiné)
        '   MyParAff    [E] :   Paramètres d'affichage
        '
        '-------------------------------------------------------------------------------------

        Dim PointsZone(xPts.Count - 1) As PointF

        For i As Integer = 0 To xPts.Count - 1
            PointsZone(i).X = (xEcran(parAff, xPts(i)))
            PointsZone(i).Y = (yEcran(parAff, yPts(i)))
        Next

        If lRemplissage Then MyGr.FillPolygon(MyBrush, PointsZone)
        If lContour Then MyGr.DrawPolygon(MyPen, PointsZone)

    End Sub

    Public Sub ContourZone(ByVal MyGr As Graphics, ByVal MyPen As Pen,
                           ByVal xPts() As Single, ByVal yPts() As Single, ByVal nbPts As Integer,
                           ByVal parAff As Struc_Affichage, Optional lOuvert As Boolean = False)

        Dim PointsZone(nbPts - 1) As PointF

        For i As Integer = 0 To nbPts - 1
            PointsZone(i).X = (XEcran(parAff, xPts(i)))
            PointsZone(i).Y = (YEcran(parAff, yPts(i)))
        Next

        If lOuvert Then
            MyGr.DrawLines(MyPen, PointsZone)
        Else
            MyGr.DrawPolygon(MyPen, PointsZone)
        End If
    End Sub

#End Region

#Region " Fonctions de calcul des coordonnées à l'écran "

    Public Function XEcran(ByVal ParAff As Struc_Affichage, ByVal xReel As Double) As Single
        '
        '   Retourne les coordonnées à l'écran d'une corrodonnées réelle
        '
        Return CSng(ParAff.uOri + ParAff.CRed * (xReel - ParAff.XpMin))
    End Function

    Public Function YEcran(ByVal ParAff As Struc_Affichage, ByVal yReel As Double) As Single
        '
        '   Retourne les coordonnées à l'écran d'une corrodonnées réelle
        '
        Return CSng(ParAff.vOri - ParAff.CRed * (yReel - ParAff.YpMin))
    End Function

    Public Function XUnivers(ByVal ParAff As Struc_Affichage, ByVal xSouris As Integer) As Decimal
        '
        '   Retourne les coordonnées réelle d'une coordonnée à l'écran
        '
        Return ParAff.XpMin + (xSouris - ParAff.uOri) / ParAff.CRed

    End Function

    Public Function YUnivers(ByVal ParAff As Struc_Affichage, ByVal ySouris As Integer) As Decimal
        '
        '   Retourne les coordonnées réelle d'une coordonnée à l'écran
        '
        Return ParAff.YpMin - (ySouris - ParAff.vOri) / ParAff.CRed

    End Function

#End Region

#Region " Affichage du logo Logiciel "

    ''' <summary>
    ''' Affichage du nom logiciel - Page de garde + En haut à gauche
    ''' 22/11/19 - MODIF BD pour Innno3DJoints
    ''' </summary>
    ''' <param name="MyGr">Graphics dans lequel on affiche</param>
    ''' <param name="TaillePolice">Taille de la police pour la version</param>
    ''' <param name="xPos">Position x du texte principal</param>
    ''' <param name="yPos">Position y du texte principal</param>
    ''' <param name="PetitLogo">Indique si il s'agit du petit logo en haut à gauche</param>
    ''' <param name="largeurDispo">Largeur de la pictureBox de la NDC</param>
    Public Sub DrawLogoLogiciel(ByVal MyGr As Graphics, ByVal TaillePolice As Integer, ByVal xPos As Single, ByVal yPos As Single,
                                ByVal PetitLogo As Boolean, ByVal largeurDispo As Single)

        '--[ Déclarations
        Dim BrushBleuCTICM As New SolidBrush(BleuCTICM)
        Dim BrushGrisCTICM As New SolidBrush(GrisCTICM)
        Dim MyFontVersion As New Font("Arial", TaillePolice, CType(3, FontStyle))
        Dim MyFontTitre As New Font("Arial", TaillePolice * 5, CType(1, FontStyle))
        Dim xChaine As Single
        Dim wTotal As Single

        '--[ Nom du logiciel ou Icone 
        Dim width As Single
        Dim height As Single

        If PetitLogo Then
            width = largeurDispo / 20
            height = largeurDispo / 20
            'MyGr.DrawImage(Frm_NoteCalcul.Btn_Logo.Image, xPos - width / 2, yPos, width, height)
        Else
            wTotal = MyGr.MeasureString(LogicielInfo.NomLogiciel, MyFontTitre).Width
            height = MyGr.MeasureString(LogicielInfo.NomLogiciel, MyFontTitre).Height
            xChaine = xPos - wTotal / 2
            Dim premierePartieMot As Single = MyGr.MeasureString(LogicielInfo.NomLogiciel.Substring(0, 3), MyFontTitre).Width
            MyGr.DrawString(LogicielInfo.NomLogiciel.Substring(0, 4), MyFontTitre, BrushBleuCTICM, xChaine, yPos)
            MyGr.DrawString(" Mix", MyFontTitre, BrushGrisCTICM, xChaine + premierePartieMot, yPos)
        End If

        '--[ Version
        Dim txtVersion As String

        If PetitLogo Then
            txtVersion = "PMX " & LogicielInfo.Version
        Else
            txtVersion = LogicielInfo.Version
        End If

        wTotal = MyGr.MeasureString(txtVersion, MyFontVersion).Width
        xChaine = xPos - wTotal / 2

        'Ombre de la version
        MyGr.DrawString(txtVersion, MyFontVersion, Brushes.LightGray, xChaine + 1, yPos + height - 0.5)
        'Ecriture de la version
        MyGr.DrawString(txtVersion, MyFontVersion, BrushGrisCTICM, xChaine, yPos + height)

        '--[ Libérations des objets de dessin
        BrushBleuCTICM.Dispose()
        BrushGrisCTICM.Dispose()

    End Sub

#End Region

#Region " Parametres d'affichage "

    ''' <summary>
    ''' Initialisation des paramètres d'affichage
    ''' </summary>
    ''' <param name="ParAff"></param>
    ''' <param name="xPtGauche">Point le plus à gauche du dessin</param>
    ''' <param name="yPtBas">Point le plus en bas du dessin</param>
    ''' <param name="xLongueur">Longueur total du dessin</param>
    ''' <param name="yHauteur">Hauteur totale du dessin</param>
    ''' <param name="sWI">Largeur en pixel du dessin</param>
    ''' <param name="sHI">Hauteur en pixel du dessin</param>
    Public Sub ParametresAffichage _
        (ByRef ParAff As Struc_Affichage, ByVal xPtGauche As Double, ByVal yPtBas As Double,
         ByVal xLongueur As Double, ByVal yHauteur As Double, ByVal sWI As Single, ByVal sHI As Single)
        '
        '   06/03/07 : Création - Version 1.00
        '
        '---------------------------------------------------------------------------
        '
        '   Calcul des paramètres de l'image
        '
        '---------------------------------------------------------------------------
        '
        '   ParAff      : [S]   Parametres d'affichage
        '
        '   xPtGauche   : [E]   Coordonnee X du point Haut Gauche
        '   yPtDroit    : [E]   Coordonnee Y du point Haut Gauche
        '   xLarg       : [E]   Largeur du dessin 2D
        '   yHaut       : [E]   Hauteur du dessin 2D
        '   sWI, sHI    : [E]   Largeur et hauteur de la zone d'affichage
        '
        '---------------------------------------------------------------------------

        ParAff.XpMin = xPtGauche
        ParAff.YpMin = yPtBas

        Dim CRedX, CRedY As Double

        If xLongueur = 0 Then CRedX = 1 Else CRedX = sWI / xLongueur
        If yHauteur = 0 Then CRedY = 1 Else CRedY = sHI / yHauteur

        If CRedX < CRedY Then
            ParAff.CRed = ConstantesDessin3D.RAPPORTCADRE * CRedX
        Else
            ParAff.CRed = ConstantesDessin3D.RAPPORTCADRE * CRedY
        End If

        ParAff.uOri = (sWI - ParAff.CRed * xLongueur) / 2
        ParAff.vOri = (sHI + ParAff.CRed * yHauteur) / 2

    End Sub

    ''' <summary>
    ''' Calcul des paramètres de l'image
    ''' 06/03/07 : Création - Version 1.00
    ''' </summary>
    ''' <param name="ParAff">[E/S] Parametres d'affichage</param>
    ''' <param name="xPtGauche">Coordonnee X du point Bas Gauche</param>
    ''' <param name="yPtBas">Coordonnee Y du point Bas Gauche</param>
    ''' <param name="xLarg">Largeur du dessin 2D</param>
    ''' <param name="yHaut">Hauteur du dessin 2D</param>
    ''' <param name="sWI">Largeur  de la zone d'affichage</param>
    ''' <param name="sHI">Hauteur de la zone d'affichage</param>
    ''' <param name="xLeft">Position Gauche du dessin dans son graphics</param>
    ''' <param name="yTop">Position Haut du dessin dans son graphics</param>
    Public Sub ParametresAffichage _
        (ByRef ParAff As Struc_Affichage, ByVal xPtGauche As Double, ByVal yPtBas As Double,
         ByVal xLarg As Double, ByVal yHaut As Double, ByVal sWI As Single, ByVal sHI As Single,
         ByVal xLeft As Double, ByVal yTop As Double, Optional kRed As Double = 1)

        ParAff.XpMin = xPtGauche
        ParAff.YpMin = yPtBas

        Dim CRedX, CRedY As Double

        If xLarg = 0 Then CRedX = 1 Else CRedX = sWI / xLarg
        If yHaut = 0 Then CRedY = 1 Else CRedY = sHI / yHaut

        If CRedX < CRedY Then
            ParAff.CRed = ConstantesDessin3D.RAPPORTCADRE * CRedX * kRed
        Else
            ParAff.CRed = ConstantesDessin3D.RAPPORTCADRE * CRedY * kRed
        End If

        ParAff.uOri = xLeft + (sWI - ParAff.CRed * xLarg) / 2
        ParAff.vOri = yTop + (sHI + ParAff.CRed * yHaut) / 2

    End Sub

#End Region



End Module
