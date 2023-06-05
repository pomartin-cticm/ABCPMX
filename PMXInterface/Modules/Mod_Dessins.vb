'Option Strict On

Imports System.Collections.Specialized.BitVector32
Imports System.Drawing.Drawing2D
Imports PMXMoteur2

Module Mod_Dessins

#Region " Variables locales "

    Dim MyPenContour As New Pen(Color.Black, 1)

    Const lCONTOURCOTE As Boolean = False


#End Region

#Region " Dessins en coupe pour les entraxes (FRM_PORTEE) "

    Public Sub DessinFrmCoupe(MyGr As Graphics, MyPoutre As cls_Poutre,
                              ByVal pWi As Decimal, ByVal pHi As Decimal,
                              kAdjust As Double, iSelect As Integer, lCote As Boolean,
                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   05/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du plancher en coupe
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   section     [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim MyParAff As Struc_Affichage

        Dim Largeur, Hauteur As Decimal
        Dim LargeurBord As Decimal
        Dim CouleurAcier As Color
        Dim CouleurBeton As Color
        Dim CouleurArma As Color
        Dim hMaxProfile As Decimal = MyPoutre.HauteurMaxiProfiles

        '--> Intialisations

        Largeur = MyPoutre.EntraxeD1 + MyPoutre.EntraxeD2
        Hauteur = MyPoutre.HauteurTotale
        dCar = Math.Sqrt(Largeur ^ 2 + Hauteur ^ 2) / 10
        LargeurBord = Largeur / 5

        '--> Initialisation des paramètres d'affichage

        If MyPoutre.lIntermediaire Then
            xMin = -MyPoutre.EntraxeD1 - LargeurBord
            xMax = MyPoutre.EntraxeD2 + LargeurBord
        End If

        yMin = -dCar - hMaxProfile
        yMax = MyPoutre.Dalle.zTop + dCar

        If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Préparation des Pinceaux utilisés dans le dessin

        CouleurAcier = CouleurAcierNormal
        CouleurBeton = CouleurBetonNormal
        CouleurArma = CouleurArmaNormal
        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcier)
        'Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        ' Armatures
        Dim myBrushA As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurArma, CouleurArma)

        '--> Affichage

        DessinFrmCoupeStandard(MyGr, MyPoutre, iSelect, dCar, hMaxProfile, MyParAff, myBrushP, myBrushB, myBrushA)

    End Sub

    Private Sub DessinFrmCoupeStandard(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                       iSelect As Integer, dCar As Decimal, hMaxProfile As Decimal,
                                       MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   05/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du plancher en coupe
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la section a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------
        '   Z de référence : selon specifications
        '   iSelect     10 : entraxe à gauche
        '               11 : entraxe à droite
        '               12 : trémie gauche
        '               13 : trémie droite
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations


        Dim IndS As Integer = 1         ' Indice de travée pour représentation des sections
        Dim lEnrob As Boolean = MyPoutre.Sections(IndS).lEnrobage
        Const ZREF As Decimal = 0

        '--> Affichage de la section principale

        '# Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, MyPoutre.Sections(IndS), MyParaff1, myBrushB)

        '# Dessin de la section acier

        DessinProfileMetal(MyGr, MyPoutre.Sections(IndS).ProfilA, myBrushP, MyParaff1, ZREF)


        '--> Affichage de la voisine à gauche

        If MyPoutre.lIntermediaire Then

            '# Dessin de béton d'enrobage

            If lEnrob Then _
            DessinEnrobagePartielBeton(MyGr, MyPoutre.Sections(IndS), MyParaff1, myBrushB, -MyPoutre.EntraxeD1)

            '# Dessin de la section acier

            DessinProfileMetal(MyGr, MyPoutre.Sections(IndS).ProfilA, myBrushP, MyParaff1, ZREF, -MyPoutre.EntraxeD1)

        End If

        '--> Affichage de la voisine à droite

        '# Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, MyPoutre.Sections(IndS), MyParaff1, myBrushB, +MyPoutre.EntraxeD2)

        '# Dessin de la section acier

        DessinProfileMetal(MyGr, MyPoutre.Sections(IndS).ProfilA, myBrushP, MyParaff1, ZREF, MyPoutre.EntraxeD2)

        '--> Affichage de la dalle béton




        '--> Représentation des trémies



        '--> Affichage des cotes

        DessinFrmCoupeCotes(MyGr, MyPoutre, MyParaff1, iSelect, dCar, hMaxProfile)

    End Sub

    Private Sub DessinFrmCoupeCotes(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre, MyParaff1 As Struc_Affichage,
                                    iSelect As Integer, dCar As Decimal, hMaxProfile As Decimal)
        '------------------------------------------------------------------------------------------------------------------
        '   05/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des cotes du plancher en coupe
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   dCar        [E] :   Dimension caractéristique
        '   hMaxProfile [E] :   Hauteur maxi des profilés
        '------------------------------------------------------------------------------------------------------------------
        '   Z de référence : selon specifications
        '   iSelect     101 : entraxe à gauche
        '               102 : entraxe à droite
        '               103 : trémie gauche
        '               104 : trémie droite
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xo, yo As Decimal
        Dim xe, ye As Decimal

        Dim yCote As Decimal = -hMaxProfile - dCar
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = FontBase
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Entraxe à gauche

        MyColor = StyleCouleur(iSelect, 101)
        MyPen.Color = MyColor

        xo = -MyPoutre.EntraxeD1
        xe = 0

        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParaff1, True, True)

        If lAffSymbol Then Chaine = "D1" Else Chaine = GetStringNoUnit(MyPoutre.EntraxeD1, Enu_TypeVariable.Longueur)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '--> Entraxe à droite

        MyColor = StyleCouleur(iSelect, 102)
        MyPen.Color = MyColor

        xo = 0
        xe = MyPoutre.EntraxeD2

        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParaff1, True, True)

        If lAffSymbol Then Chaine = "D2" Else Chaine = GetStringNoUnit(MyPoutre.EntraxeD2, Enu_TypeVariable.Longueur)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '--> Trémie Gauche

        '--> Trémie Droite



    End Sub

    Private Sub DessinFrmCoupeSFB()

    End Sub

#End Region

#Region " Dessins pour la portée (FRM_PORTEE) "

    Public Sub DessinFrmPortee(MyGr As Graphics, MyPoutre As cls_Poutre,
                                ByVal pWi As Decimal, ByVal pHi As Decimal,
                                kAdjust As Double, iSelect As Integer, lCote As Boolean,
                                ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   02/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des travées dans la fenêtre portées
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyPoutre    [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '------------------------------------------------------------------------------------------------------------------
        '   iSelect     0  : console gauche
        '               i  : travée sur 2 appui no i
        '               99 : console droite
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar, dCarApp As Decimal
        Dim MyParAff As Struc_Affichage
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim Longueur, Hauteur As Decimal
        Dim MyBrush As New SolidBrush(Color.LightGray)
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Initialisations

        Longueur = MyPoutre.LongueurTotale
        Hauteur = MyPoutre.HauteurTotale
        dCar = Math.Sqrt(Longueur ^ 2 + Hauteur ^ 2) / 20
        dCarApp = Hauteur / 2

        '--> Initialisation des paramètres d'affichage

        xMin = 0
        xMax = Longueur
        yMin = -dCar - dCarApp
        yMax = Hauteur + dCar

        If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre

        xe = 0
        yo = 0
        ye = Hauteur

        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee

            xo = xe
            xe = xo + MyPoutre.LongueurTravee(i)

            AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Next

        '--> Représentation des appuis

        For i As Integer = 1 To MyPoutre.NombreTraveesDeuxAppuis

            xo = MyPoutre.xPositionAppui(True, i)
            DessineAppui(MyGr, xo, dCarApp, MyParAff)

        Next

        xo = MyPoutre.xPositionAppui(False, MyPoutre.NombreTraveesDeuxAppuis)
        DessineAppui(MyGr, xo, dCarApp, MyParAff)

        '=== COTES =======================================================

        If lCote Then

            Dim yCote As Decimal = -dCar - dCarApp

            xo = 0
            xe = 0

            ' Travée console gauche

            If MyPoutre.lTraveeConsoleGauche Then

                MyColor = StyleCouleur(iSelect, 0)
                MyPen.Color = MyColor

                xo = 0
                xe = MyPoutre.LongueurTravee(0)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Lg" Else Chaine = GetStringNoUnit(MyPoutre.LongueurTravee(0), Enu_TypeVariable.Longueur)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True
            End If

            ' Travées principales

            For i As Integer = 1 To MyPoutre.NombreTraveesDeuxAppuis

                MyColor = StyleCouleur(iSelect, i)
                MyPen.Color = MyColor

                xo = xe
                xe += MyPoutre.LongueurTravee(i)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringNoUnit(MyPoutre.LongueurTravee(i), Enu_TypeVariable.Longueur)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            Next

            ' Travée console droite

            If MyPoutre.lTraveeConsoleDroite Then

                MyColor = StyleCouleur(iSelect, 99)
                MyPen.Color = MyColor

                xo = xe
                xe += MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Ld" Else Chaine = GetStringNoUnit(MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True

            End If

            ' Longueur totale si plusieurs travées

            If lTotal Then

                xo = 0
                xe = Longueur

                AddFleche(MyGr, MyPen, xo, yCote - dCar, xe, yCote - dCar, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringNoUnit(Longueur, Enu_TypeVariable.Longueur)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote - dCar, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

        End If

    End Sub

    Private Sub DessineAppui(MyGr As Graphics, xPos As Decimal, dCar As Decimal, MyParAff As Struc_Affichage)
        '------------------------------------------------------------------------------------------------------------------
        '   02/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Dessin d'un appui de travée
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   xPos        [E] :   Position de l'appui
        '   dCar        [E] :   Dimension caractéristique
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xPts(), yPts() As Single
        Dim nbPts As Integer
        Dim MyBrushAp As New SolidBrush(Color.DarkGreen)

        '--> Initialisations

        PrepareContourAppui(xPos, dCar, xPts, yPts, nbPts)

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPts, yPts, nbPts, MyParAff, True, True)

    End Sub

    Private Sub PrepareContourAppui(xPos As Decimal, dCar As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle pleine
        '---------------------------------------------------------------------------------------------------------------------------
        '   xPos        [E] :   Position de l'appui
        '   dCar        [E] :   Dimension caractéristique
        '   xPts, yPts  [S] :   Coordonnées de points définissant le contour
        '   nbPts       [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, yo As Single

        '--> Initialisaiton

        nbPts = 0

        '--> Contour

        xo = xPos
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos - dCar / 2
        yo = -dCar

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos + dCar / 2
        yo = -dCar

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

#End Region

#Region " Dessins pour le choix des sections "

    Public Sub DessinFrmTypeSection(ByRef MyGr As Graphics, ByVal MySection As cls_Section, MyDalle As Cls_Dalle,
                                    ByVal pWi As Decimal, ByVal pHi As Decimal, ByVal MyFont As Font,
                                    kAdjust As Double, lSelect As Boolean,
                                    ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du type de section dans la fenêtre choix de type de section
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   section     [E] :   Section à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   MyFont      [E] :   
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim BfMax As Decimal
        'Dim lMixte, lEnrob As Boolean
        Const lDalleRed As Boolean = False
        Dim BeffRed As Decimal
        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim MyParAff As Struc_Affichage
        Dim CouleurAcier As Color
        Dim CouleurBeton As Color
        Dim CouleurArma As Color

        '--> Initialisations

        BfMax = Math.Max(MySection.ProfilA.b_fs, MySection.ProfilA.b_fi)

        '--> Couleur

        If lSelect Then
            CouleurAcier = CouleurAcierSelect
            CouleurBeton = CouleurBetonSelect
            CouleurArma = CouleurArmaSelect
        Else
            CouleurAcier = CouleurAcierNormal
            CouleurBeton = CouleurBetonNormal
            CouleurArma = CouleurArmaNormal
        End If

        '--> Initialisation des paramètres d'affichage

        GenereDimensionsEnveloppes(MySection, MyDalle, lDalleRed, xMin, xMax, yMin, yMax, dCar, BeffRed, 0)

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Préparation des Pinceaux utilisés dans le dessin

        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcier)
        'Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        ' Armatures
        Dim myBrushA As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurArma, CouleurArma)

        '--> Renvoi vers les routines de dessin en fonction du type

        Select Case MySection.typeSection
            Case cls_Section.Enum_TypeSection.Acier, cls_Section.Enum_TypeSection.AcierEnrobage,
                 cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage

                DessinFrmTypeSectionStandard(MyGr, MySection, MyDalle, MyParAff, BeffRed, myBrushP, myBrushB, myBrushA)

            Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte

                DessinFrmTypeSFB(MyGr, MySection, MyDalle, MyParAff, myBrushP, myBrushB, myBrushA)

        End Select

        '--> Fin

        myBrushP.Dispose()
        myBrushB.Dispose()
        myBrushA.Dispose()

    End Sub

    Private Sub DessinFrmTypeSFB(ByRef MyGr As Graphics, ByVal MySection As cls_Section, MyDalle As Cls_Dalle,
                                 MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du type de section dans la fenêtre choix de type de section
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la section a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------
        '   Position z = 0 : Fibre inférieur du profilé, hors le plat
        '------------------------------------------------------------------------------------------------------------------

        Dim ZREF As Decimal = MySection.ProfilA.ha
        Dim lMixte As Boolean = (MySection.typeSection = cls_Section.Enum_TypeSection.SFBmixte)

        '--> Dessin de la dalle pour un SFB mixte

        If lMixte Then
            DessinDalleSlimFloor(MyGr, MyDalle, MySection.ProfilA.ha, MyParaff1, myBrushB)
        End If

        '--> Dessin de la section acier

        DessinProfileMetal(MyGr, MySection.ProfilA, myBrushP, MyParaff1, ZREF)

        '--> Dessin du plat

        DessinPlat(MyGr, MySection.ProfilA, myBrushP, MyParaff1, -MySection.ProfilA.Plat_t)

    End Sub

    Private Sub DessinFrmTypeSectionStandard(ByRef MyGr As Graphics, ByVal MySection As cls_Section, MyDalle As Cls_Dalle,
                                             MyParaff1 As Struc_Affichage, BeffRed As Decimal,
                                             myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du type de section dans la fenêtre choix de type de section
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la section a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMixte, lEnrob As Boolean
        Const ZREF As Decimal = 0

        '--> Initialisation

        lMixte = MySection.lMixte
        lEnrob = MySection.lEnrobage

        '--> Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, MySection, MyParaff1, myBrushB)

        '--> Dessin de la section acier

        DessinProfileMetal(MyGr, MySection.ProfilA, myBrushP, MyParaff1, ZREF)

        '--> Dessin des étriers : On ne représente pas les étriers pour la définition du type

        '--> Dessin des armatures longitudinales

        If lEnrob Then
            DessinArmaLongiEnrobage(MyGr, MySection, MyParaff1, myBrushA, 0)
            'DessinArmaLongiEnrobage(MyGr, MySection, MyParAff, myBrushA, 1)
            DessinArmaLongiEnrobage(MyGr, MySection, MyParaff1, myBrushA, 2)
        End If


        '--> Dessin de la dalle (toujours une dalle pleine)

        If lMixte Then

            '# Dalle béton

            Select Case MyDalle.type
                Case Cls_Dalle.Enum_TypeDalle.Pleine
                    DessinDallePleine(MyGr, MyDalle, MySection.ProfilA.ha, MySection.ProfilA.b_fs, MyParaff1, myBrushB, BeffRed)
                    'Case Cls_Dalle.Enum_TypeDalle.Mixte
                    '    DessinDalleMixte(MyGr, Section.dalle, BeffRed, Section.ha, Section.b_fs, ZREF, MyParAff, myBrushB)
            End Select

            '# Armatures

            'DessinLitArmaDalle(MyGr, MySection.Dalle, BeffRed, 0, MySection.ProfilA.ha, MyParAff, myBrushA)
            'DessinLitArmaDalle(MyGr, MySection.Dalle, BeffRed, 1, MySection.ProfilA.ha, MyParAff, myBrushA)

        End If

    End Sub

#End Region

#Region " Outils pour le dessin de la dalle "

    Private Sub DessinDalleSlimFloor(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, Ha As Decimal, MyParAffloc As Struc_Affichage, MyBrushB As Brush)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/06/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle pleine pour slim floor
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique        
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushB    [E] :   Pinceau pour le remplissage de la dalle
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xe, ye As Double
        Dim xo, yo As Double

        Dim MyPenContour As New Pen(Color.Black, 1)

        '--> Tracé

        xe = -MyDalle.Beff / 2
        xo = -xe
        ye = Ha + MyDalle.t_d
        yo = 0

        AddRectanglePlein(MyGr, MyBrushB, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

    End Sub


    Private Sub DessinDallePleine(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage, MyBrushDP As Brush,
                                  Optional BeffRed As Decimal = -1)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle pleine (avec renformis)
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   zRef        [E] :   Position de référence pour l'axe z (z0), comptée à partir fibre sup du profilé
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   BeffRed     [E] :   Largeur de dalle réduite pour le dessin (si -1, on prend la largeur complète)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim xPts(), yPts() As Single
        Dim nbPts As Integer
        Dim lDalleRed As Boolean
        Dim dCar As Decimal = (MyDalle.t_h + MyDalle.t_d) / 5
        Dim BeffDes As Decimal = MyDalle.Beff
        Dim MyPenDot As New Pen(Color.Black, 0.75)

        '--> Initialisation

        If BeffRed = -1 Then
            lDalleRed = False
        Else
            lDalleRed = (BeffRed < MyDalle.Beff)
        End If
        If lDalleRed Then BeffDes = BeffRed

        MyPenDot.DashStyle = DashStyle.Custom
        MyPenDot.DashPattern = New Single() {4.0F, 6.0F}

        '--> Préparation des points

        PrepareContourDallePleine(MyDalle, BeffDes, Bfs, xPts, yPts, nbPts)
        'DecalePts(yPts, nbPts, -zRef)

        '--> Affichage

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, Not lDalleRed, True)

        If lDalleRed Then
            Dim xo, yo As Decimal
            Dim xe, ye As Decimal
            Dim Th As Decimal = MyDalle.EpRenformis

            xo = -BeffRed / 2
            xe = -xo
            yo = MyDalle.zTop
            ye = yo

            AddLigne(MyGr, xo, yo, xe, ye, MyParAffA)

            PrepareLigneFaceInfDallePleine(MyDalle, BeffDes, Bfs, xPts, yPts, nbPts)
            'DecalePts(yPts, nbPts, Ha / 2)

            AddLignePolyG(MyGr, xPts, yPts, nbPts, MyParAffA)

            xo = -BeffRed / 2
            xe = xo
            yo = Th - dCar
            ye = MyDalle.zTop + dCar

            AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAffA)

            xo = BeffRed / 2
            xe = xo

            AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAffA)

        End If

        MyPenContour.Dispose()
        MyPenDot.Dispose()
    End Sub

    Private Sub PrepareContourDallePleine(ByVal MyDalle As Cls_Dalle, BeffDes As Decimal, Bfs As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle pleine
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle     [E] :   Classe dalle
        '   BeffDes     [E] :   Largeur de la dalle représentée à l'écran
        '   Bfs         [E] :   Largeur de la semelle sup
        '   xPts, yPts  [S] :   Coordonnées de points définissant le contour
        '   nbPts       [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, yo As Single

        '--> Initialisaiton

        nbPts = 0

        '--> Contour

        xo = Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(Bfs / 2 + MyDalle.t_h * Math.Tan(MyDalle.ThetaRd))
        yo = MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = BeffDes / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        yo = MyDalle.t_d + MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -BeffDes / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        yo = MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(-Bfs / 2 - MyDalle.t_h * Math.Tan(MyDalle.ThetaRd))
        yo = MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

    Private Sub PrepareLigneFaceInfDallePleine(ByVal MyDalle As Cls_Dalle, BeffDes As Decimal, Bfs As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   31/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle pleine
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle     [E] :   Classe dalle
        '   BeffDes     [E] :   Largeur de la dalle représentée à l'écran
        '   Bfs         [E] :   Largeur de la semelle sup
        '   xPts, yPts  [S] :   Coordonnées de points définissant le contour
        '   nbPts       [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, yo As Single

        '--> Initialisaiton

        nbPts = 0

        '--> Ligne polygonale

        xo = -BeffDes / 2
        yo = MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(-Bfs / 2 - MyDalle.t_h * Math.Tan(MyDalle.ThetaRd))
        yo = MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(Bfs / 2 + MyDalle.t_h * Math.Tan(MyDalle.ThetaRd))
        yo = MyDalle.t_h

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = BeffDes / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

#End Region

#Region " Outils pour le dessin des profilés acier "

    Private Sub DessinPlat(MyGr As Graphics, MyProfil As cls_ProfilA, MyBrush As Brush, MyParAffloc As Struc_Affichage, zPlat As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   01/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin du profilé métallique
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zPlat       [E] :   Position de la fibre basse du plat
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xe, ye As Double
        Dim xo, yo As Double

        Dim MyPenContour As New Pen(Color.Black, 1)

        '--> Tracé

        xe = MyProfil.Plat_b / 2
        xo = -MyProfil.Plat_b / 2
        ye = zPlat + MyProfil.Plat_t
        yo = zPlat

        AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

    End Sub

    Private Sub DessinProfileMetal(MyGr As Graphics, MyProfil As cls_ProfilA, MyBrush As Brush, MyParAffloc As Struc_Affichage,
                                   zRef As Decimal, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   01/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin du profilé métallique
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre supérieure de la section acier)
        '   xPos        [E] :   Position x de la section représentée
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        Dim xe, ye As Double
        Dim xo, yo As Double

        Dim MyPenContour As New Pen(Color.Black, 1)

        '--> En fonction du type de section

        Select Case MyProfil.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                PrepareContourLamine(MyProfil, xPts, yPts, nbPts)
                DecalePts(yPts, nbPts, zRef)
                If Math.Abs(xPos) > 0 Then
                    DecalePts(xPts, nbPts, xPos)
                End If
                RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts, MyParAffloc, True)

            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym

                '-< Semelle supérieure >-

                xe = xPos + MyProfil.b_fs / 2
                xo = xPos + MyProfil.b_fs / 2
                ye = -zRef                              ' Section.ha / 2
                yo = -MyProfil.t_fs - zRef               ' Section.ha / 2 - Section.t_fs

                AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

                '-< Semelle inférieure >-

                xe = xPos + MyProfil.b_fi / 2
                xo = xPos - MyProfil.b_fi / 2
                ye = -MyProfil.ha - zRef                  '   - Section.ha / 2
                yo = -MyProfil.ha + MyProfil.t_fi - zRef

                AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

                '-< Âme >-

                xe = xPos + MyProfil.t_w / 2
                xo = xPos - MyProfil.t_w / 2
                ye = -MyProfil.t_fs - zRef
                yo = -MyProfil.ha - zRef + MyProfil.t_fi

                AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

        End Select
    End Sub

    Private Sub PrepareContourLamine(myProfil As cls_ProfilA, ByRef xPts() As Single, ByRef yPts() As Single,
                                     ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   01/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Prépare le contour d'un profilé laminé
        '   Coordonnées y : par rapport à la fibre supérieure
        '---------------------------------------------------------------------------------------------------------------------------        
        '   myProfil    [E] :   Profilé affiché
        '   xPts, yPts  [S] :   Coordonnées du contour
        '   nbPts       [S] :   Nombre de points du contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Initialisation

        ReDim xPts(35)
        ReDim yPts(35)
        nbPts = 36

        '--> Contour

        With myProfil

            xPts(0) = .t_w / 2
            yPts(0) = - .ha / 2

            Dim DeltaA As Double = Math.PI / 10

            For i = 1 To 6
                xPts(i) = .t_w / 2 + .r_cs * (1 + CSng(Math.Cos(Math.PI - (i - 1) * DeltaA)))
                yPts(i) = - .t_fs - .r_cs + .r_cs * CSng(Math.Sin(Math.PI - (i - 1) * DeltaA))
            Next

            xPts(7) = .b_fs / 2
            yPts(7) = - .t_fs

            xPts(8) = .b_fs / 2
            yPts(8) = 0

            For i = 9 To 17
                xPts(i) = -xPts(17 - i)
                yPts(i) = yPts(17 - i)
            Next

            For i = 18 To 35
                xPts(i) = xPts(35 - i)
                yPts(i) = - .ha - yPts(35 - i)
            Next

        End With

    End Sub

#End Region

#Region " Outils pour le dessin du béton d'enrobage (y compris les armatures) "

    Private Sub DessinEnrobagePartielBeton(ByRef MyGr As Graphics, ByVal MySection As cls_Section,
                                           MyParAff As Struc_Affichage, MyBrushBp As Brush, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section affichée
        '   MyParAff    [E] :   Paramètres d'affichage
        '   
        '---------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xe, ye As Decimal
        Dim xo, yo As Decimal
        Dim MyPenContour As New Pen(Color.Black, 1)

        '--> Dessin bloc béton

        xe = xPos + MySection.ProfilA.b_fi / 2 * MySection.enrobage_partiel.Ratio_bc
        xo = xPos - MySection.ProfilA.b_fi / 2 * MySection.enrobage_partiel.Ratio_bc
        ye = -MySection.ProfilA.ha + MySection.ProfilA.t_fi
        yo = -MySection.ProfilA.t_fs

        AddRectanglePlein(MyGr, MyBrushBp, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

    End Sub

    Private Sub DessinArmaLongiEnrobage(ByRef MyGr As Graphics, ByVal MySection As cls_Section,
                                        MyParAffloc As Struc_Affichage, MyBrushA As Brush, iArma As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   section     [E] :   Section à représenter
        '   zRef        [E] :   Position de référence pour l'axe z (z0), comptée à partir fibre sup du profilé
        '   MyParAffloc [E] :   Paramètres d'affichage   
        '   MyBrushA    [E] :   Pinceau pour le remplissage de la dalle
        '   iArma       [E] :   indice du lit d'armature
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)

        Dim xo, yo, xe, xi As Decimal
        Dim Signe As Decimal = 1
        Dim uYInterne As Decimal
        Dim Delta As Decimal
        Dim DeltaY(1) As Decimal

        '--> Initialisation

        Select Case iArma
            Case 0
                yo = -MySection.ProfilA.ha + MySection.ProfilA.t_fi + MySection.enrobage_partiel.Etriers_EnrobageZ + MySection.enrobage_partiel.Etriers_Phi + MySection.enrobage_partiel.LitsArma(iArma).Phi / 2
            Case 1
                yo = -MySection.ProfilA.ha / 2
            Case 2
                yo = -MySection.ProfilA.t_fs - MySection.enrobage_partiel.Etriers_EnrobageZ - MySection.enrobage_partiel.Etriers_Phi - MySection.enrobage_partiel.LitsArma(iArma).Phi / 2
        End Select
        Select Case MySection.enrobage_partiel.Etriers_Type
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.Cadre : uYInterne = MySection.enrobage_partiel.Etriers_EnrobageY
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant : uYInterne = 0
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude : uYInterne = 0
        End Select
        DeltaY(1) = 0
        If (MySection.enrobage_partiel.Etriers_Type = Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant) Then
            DeltaY(0) = MySection.enrobage_partiel.Etriers_Phi / 5
        Else
            DeltaY(0) = 0
        End If

        '--> Boucle sur les armatures du lit

        For jChambre As Integer = 0 To 1

            xo = Signe * (MySection.ProfilA.b_fs * MySection.enrobage_partiel.Ratio_bc / 2 - MySection.enrobage_partiel.Etriers_EnrobageY - MySection.enrobage_partiel.Etriers_Phi - MySection.enrobage_partiel.LitsArma(iArma).Phi / 2)
            xe = Signe * (MySection.ProfilA.t_w / 2 + uYInterne + MySection.enrobage_partiel.Etriers_Phi + MySection.enrobage_partiel.LitsArma(iArma).Phi / 2)

            If MySection.enrobage_partiel.LitsArma(iArma).nbArma = 1 Then
                Delta = 1
            Else
                Delta = (xe - xo) / (MySection.enrobage_partiel.LitsArma(iArma).nbArma - 1)
            End If
            For i As Integer = 1 To MySection.enrobage_partiel.LitsArma(iArma).nbArma

                xi = xo + Delta * (i - 1)

                AddCerclePlein(MyGr, MyBrushA, xi, yo + DeltaY(jChambre), MySection.enrobage_partiel.LitsArma(iArma).Phi, MyParAffloc, True)

            Next

            Signe = -Signe

        Next

    End Sub


#End Region
#Region "=====OUTILS GENERAUX======"

    Private Function StyleCouleur(iSelect As Integer, iRef As Integer) As Color
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        Dim MyColor As Color
        '--> Déclaration
        If iSelect = iRef Then

            MyColor = ColorSelect
        Else

            MyColor = ColorNonSelect
        End If
        Return MyColor
    End Function

    Private Sub GenereDimensionsEnveloppes(MySection As cls_Section, MyDalle As Cls_Dalle, lDalleReduite As Boolean,
                                           ByRef xMin As Decimal, ByRef xMax As Decimal, ByRef yMin As Decimal, ByRef yMax As Decimal,
                                           ByRef dCar As Decimal, ByRef BeffRed As Decimal, Optional zRef As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   03/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Génère les dimensions enveloppes d'une section à représenter
        '---------------------------------------------------------------------------------------------------------------------------
        '   MySection       [E] :   Section à afficher
        '   lMixte          [E] :   Indique si on affiche la dalle, le cas échéant
        '   lDalleReduite   [E] :   Indique si on représente une largeur de dalle réduite ou la largeur complète (dans ce dernier, cela écrase le dessin à l'écran)
        '   xMin, xMax      [S] :   Valeurs x min et max de la section
        '   yMin, yMax      [S] :   Valeurs y min et max de la section
        '   BeffRed         [S] :   Largeur réduite de la dalle pour représentation à l'écran
        '   dCar            [S] :   Dimension caractéristique utilisée pour les décalages
        '   BeffRed         [S] :   Largeur de dalle utilisée pour l'affichage
        '   zRef            [E] :   Position de référence / fibre supérieure du profilé métallique
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Initialisation

        Dim Diagonale As Decimal = Math.Sqrt((MySection.ProfilA.b_fi + MySection.ProfilA.b_fs) ^ 2 / 4 + MySection.ProfilA.ha ^ 2)
        Dim BfMax As Decimal = Math.Max(MySection.ProfilA.b_fs, MySection.ProfilA.b_fi)

        '--> Traitement

        Select Case MySection.typeSection
            Case cls_Section.Enum_TypeSection.Acier, cls_Section.Enum_TypeSection.AcierEnrobage,
                 cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                yMin = -zRef - MySection.ProfilA.ha
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = -zRef

                If MySection.lMixte Then
                    If lDalleReduite Then BeffRed = Math.Min(MyDalle.Beff, 1.5 * Diagonale) Else BeffRed = MyDalle.Beff
                    yMax += MyDalle.t_d + MyDalle.EpRenformis
                    xMin = -Math.Max(BeffRed / 2, BfMax)
                    xMax = -xMin
                    dCar = Math.Max(Math.Sqrt(((MySection.ProfilA.ha + MyDalle.zTop) ^ 2 + (MySection.ProfilA.b_fs + MySection.ProfilA.b_fi) ^ 2)), BeffRed) / 20
                Else
                    dCar = Math.Sqrt((MySection.ProfilA.ha ^ 2 + (MySection.ProfilA.b_fs + MySection.ProfilA.b_fi) ^ 2)) / 20
                End If

            Case cls_Section.Enum_TypeSection.SFB
                yMin = -MySection.ProfilA.Plat_t
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = MySection.ProfilA.ha
            Case cls_Section.Enum_TypeSection.SFBmixte
                yMin = -MySection.ProfilA.Plat_t
                xMin = -MyDalle.Beff / 2
                xMax = -xMin
                yMax = MySection.ProfilA.ha + MyDalle.t_d

        End Select

    End Sub

    ''' <summary>
    ''' Ajoute un point dans un tableau de points
    ''' </summary>
    ''' <param name="x">        [E] Coordonnée x du point ajouté       </param>
    ''' <param name="y">        [E] Coordonnée y du point ajouté       </param>
    ''' <param name="xPts">     [S] Table des coordonnées x            </param>
    ''' <param name="yPts">     [S] Table des coordonnées y            </param>
    ''' <param name="nbPts">    [S] Nombre de points dans les tables    </param>
    Private Sub AjoutePoint(x As Single, y As Single, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   31/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------


        nbPts += 1
        If nbPts = 1 Then
            ReDim xPts(0)
            ReDim yPts(0)
        Else
            ReDim Preserve xPts(nbPts - 1)
            ReDim Preserve yPts(nbPts - 1)
        End If
        xPts(nbPts - 1) = x
        yPts(nbPts - 1) = y

    End Sub

    Private Sub DecalePts(ByRef cPts() As Single, nbPts As Integer, Delta As Single)
        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To nbPts - 1
            cPts(i) += Delta
        Next


    End Sub

#End Region


End Module
