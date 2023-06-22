'Option Strict On

Imports System.Collections.Specialized.BitVector32
Imports System.Drawing.Drawing2D
Imports PMXMoteur2

Module Mod_Dessins

#Region " Variables locales "

    Dim MyPenContour As New Pen(Color.Black, 1)

    Const lCONTOURCOTE As Boolean = False

#End Region

#Region " Dessins pour la dfiniton de l'enrobage (FRM_ENROBAGE) "

    ''' <summary>
    ''' Dessin réactif de la section acier et de l'enrobage partiel
    ''' </summary>
    Public Sub DessinFrmEnrobage(ByRef MyGr As Graphics, ByVal section As cls_Section, MyEnrob As Cls_Enrobage_Partiel,
                                 ByVal pWi As Decimal, ByVal pHi As Decimal,
                                 kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                 ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de l'enrobage partiel
        '   Représente la section acier + l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   section     [E] :
        '   MyEnro      [E] :   
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   lCote       [E] :   Indique si affichage de la cote
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '   0 - Bc
        '   1 - Phi Etriers
        '   2 - Uy
        '   3 - Uz
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyParAffE As Struc_Affichage

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim Profile As New cls_ProfilA
        'Profile = section.ProfilA.Clone
        cls_ProfilA.DeepCopie(section.ProfilA, Profile)
        Dim lLam As Boolean = (Profile.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine)

        Dim ColorLocalEtriers As Color = CouleurArmaNormal      'ColorEtriers
        Dim ColorLocalArma(2) As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurAcier As Color = CouleurAcierNormal
        Dim pColorLocalArma(2, 2) As Color

        Dim zREF As Decimal = 0
        Dim i, j As Integer

        '--> Initialisation des couleurs

        For i = 0 To 2
            For j = 0 To 2
                pColorLocalArma(i, j) = CouleurArmaNormal
            Next
        Next

        Select Case iSelect
            Case 0
                CouleurBeton = CouleurBetonSelect
                CouleurAcier = CouleurAcierSelect
            Case 1, 2, 3, 4 : ColorLocalEtriers = CouleurArmaSelect

            Case 5, 7
                For j = 0 To 2
                    pColorLocalArma(1, j) = CouleurArmaSelect
                Next
            Case 6
                For j = 0 To 2
                    pColorLocalArma(0, j) = CouleurArmaSelect
                Next
            Case 8
                For j = 0 To 2
                    pColorLocalArma(2, j) = CouleurArmaSelect
                Next

            Case 10, 20
                pColorLocalArma(0, 0) = CouleurArmaSelect

            Case 11, 21
                pColorLocalArma(0, 1) = CouleurArmaSelect

            Case 12, 22
                pColorLocalArma(0, 2) = CouleurArmaSelect

            Case 13, 23
                pColorLocalArma(1, 0) = CouleurArmaSelect

            Case 14, 24
                pColorLocalArma(1, 1) = CouleurArmaSelect

            Case 15, 25
                pColorLocalArma(1, 2) = CouleurArmaSelect

            Case 16, 26
                pColorLocalArma(2, 0) = CouleurArmaSelect

            Case 17, 27
                pColorLocalArma(2, 1) = CouleurArmaSelect

            Case 18, 28
                pColorLocalArma(2, 2) = CouleurArmaSelect

        End Select
        For i = 0 To 2
            ColorLocalArma(i) = CouleurArmaNormal
        Next

        '--> Préparation des Pinceaux utilisés dans le dessin

        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcier)
        ' Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        ' Etriers
        Dim myBrushE As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalEtriers, ColorLocalEtriers)
        ' Armatures 
        Dim myBrushASup(2) As Brush
        Dim myBrushAMid(2) As Brush
        Dim myBrushAInf(2) As Brush
        For j = 0 To 2
            'myBrushA(i) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalArma(i), ColorLocalArma(i))
            myBrushASup(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(0, j), pColorLocalArma(0, j))
            myBrushAMid(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(1, j), pColorLocalArma(1, j))
            myBrushAInf(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(2, j), pColorLocalArma(2, j))

        Next

        '--> Initialisation des paramètres d'affichage

        yMin = -Profile.ha
        xMin = -Math.Max(Profile.b_fs, Profile.b_fi) / 2
        xMax = -xMin
        yMax = 0

        'If lCote Then
        dCar = Math.Sqrt((Profile.ha ^ 2 + (Profile.b_fs + Profile.b_fi) ^ 2)) / 20
        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAffE, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Dessin de béton

        DessinEnrobagePartielBeton(MyGr, section.ProfilA, MyEnrob.Ratio_bc, MyParAffE, myBrushB)

        '--> Dessin de la section acier

        DessinProfileMetal(MyGr, Profile, myBrushP, MyParAffE, zREF)

        '--> Dessin des étriers

        DessinEtriers(MyGr, section.ProfilA, MyEnrob, MyParAffE, myBrushE, zREF)

        '--> Dessin des armatures longitudinales

        DessinArmaLongiEnrobageN(MyGr, section, MyEnrob, MyParAffE, myBrushASup, 0)

        DessinArmaLongiEnrobageN(MyGr, section, MyEnrob, MyParAffE, myBrushAMid, 1)

        DessinArmaLongiEnrobageN(MyGr, section, MyEnrob, MyParAffE, myBrushAInf, 2)

        '--> Cotation

        If lCote Then
            DessinCoteFrmEnrobage(MyGr, section, MyEnrob, iSelect, zREF, dCar, lAffSymbol, MyParAffE)
        End If

        '--> Fin

        myBrushP.Dispose()
        myBrushE.Dispose()
        myBrushB.Dispose()

    End Sub

    Private Sub DessinCoteFrmEnrobage(ByRef MyGr As Graphics, ByVal section As cls_Section, enrobage As Cls_Enrobage_Partiel,
                                      iSelect As Integer, zRef As Decimal, dCar As Decimal,
                                      lAffSymbol As Boolean, MyParAffLoc As Struc_Affichage)
        '---------------------------------------------------------------------------------------------------------------------------
        '   05/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de l'enrobage partiel
        '   Représente la cotation section acier + l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '   0 - Bc
        '   1 - Phi Etriers
        '   2 - Uy
        '   3 - Uz
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPen As New Pen(Color.Black, 1)
        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim MyPenNormal As New Pen(ColorNonSelect, 1)
        Dim MyPenSelect As New Pen(ColorSelect, 1)
        Dim MyFontNormal As Font = FontBase
        Dim MyColor As Color
        Dim Chaine As String
        Dim Bc, Uy, Uz, PhiE As Decimal
        Dim Bf, Tw, Tf, Rc As Decimal
        Dim lLam As Boolean = section.lLamine
        Dim lContour As Boolean = lCONTOURCOTE
        Dim iArma As Integer

        '--> Initialisation

        Uy = enrobage.Etriers_EnrobageY
        PhiE = enrobage.Etriers_Phi
        Bc = section.ProfilA.b_fs * enrobage.Ratio_bc
        Uz = enrobage.Etriers_EnrobageZ
        Bf = section.ProfilA.b_fs
        Tw = section.ProfilA.t_w
        Rc = section.ProfilA.r_cs
        Tf = section.ProfilA.t_fs

        '--> Affichage des cotes

        '# Bc

        If iSelect = 0 Then
            MyColor = StyleCouleur(iSelect, 0)
            MyPen.Color = MyColor

            xo = -Bc / 2
            xe = xo - dCar / 2
            yo = -(section.ProfilA.ha / 2 - section.ProfilA.t_fs - section.ProfilA.r_cs) * 0.8
            ye = yo

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)

            xo = Bc / 2
            xe = xo + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)
            If lAffSymbol Then Chaine = "bc" Else Chaine = GetStringNoUnit(Bc, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAffLoc, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
        End If

        '# PhiEtriers

        MyColor = StyleCouleur(iSelect, 1)
        MyPen.Color = MyColor

        xo = -Bc / 2 + Uy + PhiE
        xe = xo + dCar / 2
        yo = -(section.ProfilA.ha / 2 - section.ProfilA.t_fs - section.ProfilA.r_cs) * 0.6
        ye = yo
        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)

        xo = -Bc / 2 + Uy
        xe = -Bc / 2 - dCar

        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)
        If lAffSymbol Then Chaine = "Phie" Else Chaine = GetStringNoUnit(PhiE, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAffLoc, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# UyEtriers

        MyColor = StyleCouleur(iSelect, 2)
        MyPen.Color = MyColor

        xo = -Bc / 2 + Uy
        xe = xo + dCar / 2
        yo = -(section.ProfilA.ha / 2 - section.ProfilA.t_fs - section.ProfilA.r_cs) * 0.4
        ye = yo
        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)

        xo = -Bc / 2
        xe = -Bc / 2 - dCar

        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)
        If lAffSymbol Then Chaine = "uy" Else Chaine = GetStringNoUnit(Uy, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAffLoc, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# UzEtries

        MyColor = StyleCouleur(iSelect, 3)
        MyPen.Color = MyColor

        xo = -Tw / 2 - Rc - ((Bf - Tw) / 2 - Rc) / 2
        xe = xo
        yo = -Uz - Tf
        ye = yo - dCar / 2

        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)

        yo = -Tf
        ye = yo + dCar

        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, False)
        If lAffSymbol Then Chaine = "uz" Else Chaine = GetStringNoUnit(Uz, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAffLoc, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# Bf

        MyColor = StyleCouleur(0, -1)
        MyPen.Color = MyColor

        yo = -section.ProfilA.ha - dCar
        ye = yo
        xo = section.ProfilA.b_fi / 2
        xe = -xo

        If lAffSymbol Then
            If lLam Then Chaine = "b" Else Chaine = "bfi"
        Else
            Chaine = GetStringNoUnit(section.ProfilA.b_fi, Enu_TypeVariable.Dimension)
        End If

        AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, True)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAffLoc, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# Position z du lit central

        If (iSelect = 5) Or (iSelect = 105) Or (iSelect = 115) Then

            Select Case iSelect
                Case 105 : iArma = 0
                Case 5 : iArma = 1
                Case 115 : iArma = 2
            End Select
            Dim zPos As Decimal = section.zPositionLitArmaEnrobage(iArma)

            MyColor = StyleCouleur(iSelect, iSelect)
            MyPen.Color = MyColor

            xo = section.ProfilA.b_fi / 2 - Uy - PhiE - enrobage.LitArma(iArma).PhiExt / 2
            xe = section.ProfilA.b_fi / 2 + dCar
            yo = zPos
            ye = zPos

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc)

            xo = xe
            ye = 0
            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffLoc, True, True)

            If lAffSymbol Then
                Chaine = "z"
            Else
                Chaine = GetStringNoUnit(Math.Abs(zPos), Enu_TypeVariable.Dimension)
            End If

            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAffLoc, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
        End If

        '# Propriétés acier

        If iSelect = 200 Then
            MyColor = StyleCouleur(iSelect, 200)
            MyPen.Color = MyColor
        End If

        '# Propriétés béton

        If iSelect = 201 Then
            MyColor = StyleCouleur(iSelect, 201)
            MyPen.Color = MyColor
        End If

    End Sub

#End Region

#Region " Outils pour le dessin des étriers "

    ''' <summary>
    ''' Dessine les etriers
    ''' </summary>
    Private Sub DessinEtriers(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                              MyParAffloc As Struc_Affichage, MyBrushE As Brush, zRef As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Affichage des étriers dans le béton de l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   MyParAffloc [E] :   Paramètres d'affichage
        '   MyBrushE    [E] :   Pinceau pour le remplissage des étriers
        '   zRef        [E] :   Position z de référence (par rapport à la fibre supérieure de la semelle sup)
        '---------------------------------------------------------------------------------------------------------------------------

        Select Case enrobage.Etriers_Type
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                DessinEtriersCadre(MyGr, profile, enrobage, MyParAffloc, MyBrushE)
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant, Cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                DessinEtriersCadreSouT(MyGr, profile, enrobage, MyParAffloc, MyBrushE, zRef)
        End Select

    End Sub

    ''' <summary>
    ''' Dessine les etriers soudés
    ''' </summary>
    Private Sub DessinEtriersCadreSouT(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                       MyParAffloc As Struc_Affichage, MyBrushE As Brush, zRef As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Affichage des étriers dans le béton de l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   MyParAffloc [E] :   Paramètres d'affichage
        '   MyBrushE    [E] :   Pinceau pour le remplissage des étriers
        '   zRef        [E] :   Position z de référence (par rapport à la fibre supérieure de la semelle sup)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim xPts(), yPts() As Single
        Dim nbPts As Integer
        Dim lSoude As Boolean = (enrobage.Etriers_Type = Cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude)

        '--> Préparation du contour des étriers

        If lSoude Then
            PrepareContourEtriersSoudes(profile, enrobage, xPts, yPts, nbPts)
        Else
            PrepareContourEtriersTravers(profile, enrobage, xPts, yPts, nbPts)
        End If
        DecalePts(yPts, nbPts, -profile.ha / 2 - zRef)

        '--> Dessin Contour côté gauche

        RemplirZone(MyGr, MyBrushE, xPts, yPts, nbPts, MyParAffloc, True, True)

        '--> Dessin Contour côté droit par symétrie

        MirroirPts(xPts, nbPts)
        If Not lSoude Then DecalePts(yPts, nbPts, enrobage.Etriers_Phi / 5)
        RemplirZone(MyGr, MyBrushE, xPts, yPts, nbPts, MyParAffloc, True, True)

    End Sub

    ''' <summary>
    ''' Dessine les etriers en cadres normaux
    ''' </summary>
    Private Sub DessinEtriersCadre(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                   MyParAffloc As Struc_Affichage, MyBrushE As Brush)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Affichage des étriers dans le béton de l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   MyParAffloc [E] :   Paramètres d'affichage
        '   MyBrushE    [E] :   Pinceau pour le remplissage des étriers
        '   zRef        [E] :   Position z de référence (par rapport à la fibre supérieure de la semelle sup)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim xPts(), yPts() As Single
        Dim nbPts As Integer
        Dim xPtsP(), yPtsP() As Single
        Dim nbPtsP As Integer

        '--> Préparation du contour des étriers

        PrepareContourEtriersP(profile, enrobage, xPtsP, yPtsP, nbPtsP)
        PrepareContourEtriersG(profile, enrobage, xPts, yPts, nbPts)

        '--> Dessin Contour côté gauche

        RemplirZone(MyGr, MyBrushE, xPtsP, yPtsP, nbPtsP, MyParAffloc, True, True)
        RemplirZone(MyGr, MyBrushE, xPts, yPts, nbPts, MyParAffloc, True, True)

        '--> Dessin Contour côté droit par symétrie

        MirroirPts(xPts, nbPts)
        MirroirPts(xPtsP, nbPtsP)
        RemplirZone(MyGr, MyBrushE, xPtsP, yPtsP, nbPtsP, MyParAffloc, True, True)
        RemplirZone(MyGr, MyBrushE, xPts, yPts, nbPts, MyParAffloc, True, True)

    End Sub

    Private Sub MirroirPts(ByRef cPts() As Single, nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To nbPts - 1
            cPts(i) = -cPts(i)
        Next

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier traversant
    ''' </summary>
    Private Sub PrepareContourEtriersTravers(ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                             ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)

        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DiaCourbureSup As Decimal = enrobage.LitsArmaOLD(2).Phi
        Dim DiaCourbureInf As Decimal = enrobage.LitsArmaOLD(0).Phi
        Dim LongueurRetour As Decimal = 5 * enrobage.Etriers_Phi
        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.b_fs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.t_w
        Dim Tf As Decimal = profile.t_fs
        Dim Uy As Decimal = enrobage.Etriers_EnrobageY
        Dim Uz As Decimal = enrobage.Etriers_EnrobageZ
        Dim Bc As Decimal = enrobage.Ratio_bc * Bf
        Dim PhiEtrier As Decimal = enrobage.Etriers_Phi

        Dim RayonC As Decimal

        '--> Contour Intérieur

        RayonC = DiaCourbureSup / 2

        xc = -Tw / 2 - DiaCourbureSup / 2 - PhiEtrier
        yc = Ht / 2 - Tf - Uz - DiaCourbureSup / 2 - PhiEtrier

        xo = Tw / 2 + 0.25 * (Bf - Tw)
        yo = yc + DiaCourbureSup / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xc = -Bc / 2 + Uy + DiaCourbureSup / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 90, 180, 1, xPts, yPts, nbPts)

        RayonC = DiaCourbureInf / 2
        xc = -Bc / 2 + Uy + DiaCourbureInf / 2 + PhiEtrier
        yc = -Ht / 2 + Tf + Uz + DiaCourbureInf / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, 1, xPts, yPts, nbPts)

        xc = -Tw / 2 - DiaCourbureInf / 2 - PhiEtrier

        xo = Tw / 2 + 0.25 * (Bf - Tw)
        yo = yc - DiaCourbureInf / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Contour externe

        yo = yc - DiaCourbureInf / 2 - PhiEtrier

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xc = -Bc / 2 + Uy + (DiaCourbureInf) / 2 + PhiEtrier
        RayonC = DiaCourbureInf / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, -1, xPts, yPts, nbPts)

        RayonC = DiaCourbureSup / 2 + PhiEtrier
        xc = -Bc / 2 + Uy + (DiaCourbureSup) / 2 + PhiEtrier
        yc = (Ht / 2 - Tf - Uz - DiaCourbureSup / 2 - PhiEtrier)

        AjouteArcCercle(xc, yc, RayonC, 90, 180, -1, xPts, yPts, nbPts)

        xc = -Tw / 2 - DiaCourbureSup / 2 - PhiEtrier

        xo = Tw / 2 + 0.25 * (Bf - Tw)
        yo = yc + PhiEtrier + DiaCourbureSup / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier soude
    ''' </summary>
    Private Sub PrepareContourEtriersSoudes(ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                            ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Contour pour les étriers soudés
        '---------------------------------------------------------------------------------------------------------------------------
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   xPts, yPts  [S] :   Tables des points décrivant le contour
        '   nbPts       [S] :   Nombre de points décrivant le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DiaCourbureSup As Decimal = enrobage.LitsArmaOLD(2).Phi
        Dim DiaCourbureInf As Decimal = enrobage.LitsArmaOLD(0).Phi
        Dim LongueurRetour As Decimal = 5 * enrobage.Etriers_Phi
        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.b_fs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.t_w
        Dim Tf As Decimal = profile.t_fs
        Dim Uy As Decimal = enrobage.Etriers_EnrobageY
        Dim Uz As Decimal = enrobage.Etriers_EnrobageZ
        Dim Bc As Decimal = enrobage.Ratio_bc * Bf
        Dim PhiEtrier As Decimal = enrobage.Etriers_Phi

        Dim RayonC As Decimal

        '--> Contour Intérieur

        RayonC = DiaCourbureSup / 2

        xc = -Tw / 2 - DiaCourbureSup / 2 - PhiEtrier
        yc = Ht / 2 - Tf - Uz - DiaCourbureSup / 2 - PhiEtrier

        xo = xc + RayonC
        yo = yc - LongueurRetour

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        AjouteArcCercle(xc, yc, RayonC, 0, 90, 1, xPts, yPts, nbPts)

        xc = -Bc / 2 + Uy + DiaCourbureSup / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 90, 180, 1, xPts, yPts, nbPts)

        RayonC = DiaCourbureInf / 2
        xc = -Bc / 2 + Uy + DiaCourbureInf / 2 + PhiEtrier
        yc = -Ht / 2 + Tf + Uz + DiaCourbureInf / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, 1, xPts, yPts, nbPts)

        xc = -Tw / 2 - DiaCourbureInf / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 270, 360, 1, xPts, yPts, nbPts)

        xo = xc + RayonC
        yo = yc + LongueurRetour

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Contour extérieur 

        RayonC = DiaCourbureInf / 2 + PhiEtrier

        xo = xc + RayonC

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        AjouteArcCercle(xc, yc, RayonC, 270, 360, -1, xPts, yPts, nbPts)

        xc = -Bc / 2 + Uy + (DiaCourbureInf) / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, -1, xPts, yPts, nbPts)

        RayonC = DiaCourbureSup / 2 + PhiEtrier
        xc = -Bc / 2 + Uy + (DiaCourbureSup) / 2 + PhiEtrier
        yc = (Ht / 2 - Tf - Uz - DiaCourbureSup / 2 - PhiEtrier)

        AjouteArcCercle(xc, yc, RayonC, 90, 180, -1, xPts, yPts, nbPts)

        xc = -Tw / 2 - DiaCourbureSup / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 0, 90, -1, xPts, yPts, nbPts)

        xo = xc + RayonC
        yo = yc - LongueurRetour

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier, tronçon secondaire
    ''' </summary>
    Private Sub PrepareContourEtriersP(ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                       ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Contour pour les étriers soudés - Petite partie en retour supérieur intérieur
        '---------------------------------------------------------------------------------------------------------------------------
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   xPts, yPts  [S] :   Tables des points décrivant le contour
        '   nbPts       [S] :   Nombre de points décrivant le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DiaCourbureSup As Decimal = enrobage.LitArma(2).PhiInt

        Dim LongueurRetour As Decimal = 3 * enrobage.Etriers_Phi

        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.b_fs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.t_w
        Dim Tf As Decimal = profile.t_fs
        Dim Uy As Decimal = enrobage.Etriers_EnrobageY
        Dim Uz As Decimal = enrobage.Etriers_EnrobageZ
        Dim Bc As Decimal = enrobage.Ratio_bc * Bf
        Dim PhiEtrier As Decimal = enrobage.Etriers_Phi

        Dim RayonC As Decimal

        '--> Contour Intérieur

        RayonC = DiaCourbureSup / 2
        xc = -Tw / 2 - Uy - (DiaCourbureSup) / 2 - PhiEtrier
        yc = -Tf - Uz - DiaCourbureSup / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 0, 135, 1, xPts, yPts, nbPts)

        xo = xc - Math.Sqrt(2) / 2 * (RayonC + LongueurRetour)
        yo = yc + Math.Sqrt(2) / 2 * (RayonC - LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Contour extérieur

        RayonC = DiaCourbureSup / 2 + PhiEtrier

        xo = xc - Math.Sqrt(2) / 2 * (RayonC + LongueurRetour)
        yo = yc + Math.Sqrt(2) / 2 * (RayonC - LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        AjouteArcCercle(xc, yc, RayonC, 0, 135, -1, xPts, yPts, nbPts)

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier, tronçon principal
    ''' </summary>
    Private Sub PrepareContourEtriersG(ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                       ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Contour du cadre - Partie principale
        '---------------------------------------------------------------------------------------------------------------------------
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   xPts, yPts  [S] :   Tables des points décrivant le contour
        '   nbPts       [S] :   Nombre de points décrivant le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DiaCourbureSupExt As Decimal = enrobage.LitArma(2).PhiExt
        Dim DiaCourbureSupInt As Decimal = enrobage.LitArma(2).PhiInt
        Dim DiaCourbureInfExt As Decimal = enrobage.LitArma(0).PhiExt
        Dim DiaCourbureInfInt As Decimal = enrobage.LitArma(0).PhiInt
        Dim LongueurRetour As Decimal = 3 * enrobage.Etriers_Phi
        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.b_fs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.t_w
        Dim Tf As Decimal = profile.t_fs
        Dim Uy As Decimal = enrobage.Etriers_EnrobageY
        Dim UyInt As Decimal = enrobage.Etriers_EnrobageYinterne
        Dim Uz As Decimal = enrobage.Etriers_EnrobageZ
        Dim Bc As Decimal = enrobage.Ratio_bc * Bf
        Dim PhiEtrier As Decimal = enrobage.Etriers_Phi

        Dim RayonC As Decimal

        '--> Initialisation

        nbPts = 0

        '--> Contour interieur <-----------------------------------------------------------

        '=# Armature supérieure intérieure

        RayonC = DiaCourbureSupInt / 2

        xc = -Tw / 2 - UyInt - RayonC - PhiEtrier
        yc = -Tf - Uz - RayonC - PhiEtrier

        xo = xc + Math.Sqrt(2) / 2 * (RayonC - LongueurRetour)
        yo = yc - Math.Sqrt(2) / 2 * (RayonC + LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        AjouteArcCercle(xc, yc, RayonC, -45, 90, 1, xPts, yPts, nbPts)

        '=# Armature supérieure extérieure

        RayonC = DiaCourbureSupExt / 2
        xc = -Bc / 2 + Uy + RayonC + PhiEtrier
        yc = -Tf - Uz - RayonC - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 90, 180, 1, xPts, yPts, nbPts)

        '=# Armature inférieure extérieure

        RayonC = DiaCourbureInfExt / 2
        xc = -Bc / 2 + Uy + RayonC + PhiEtrier
        yc = -Ht + Tf + Uz + RayonC + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, 1, xPts, yPts, nbPts)

        '=# Armature inférieure intérieure

        RayonC = DiaCourbureInfInt / 2

        xc = -Tw / 2 - UyInt - RayonC - PhiEtrier
        yc = -Ht + Tf + Uz + RayonC + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 270, 360, 1, xPts, yPts, nbPts)

        '=# Armature supérieure intérieure

        RayonC = DiaCourbureSupInt / 2 + PhiEtrier
        xc = -Tw / 2 - UyInt - DiaCourbureSupInt / 2 - PhiEtrier
        yc = -Tf - Uz - DiaCourbureSupInt / 2 - PhiEtrier

        Dim Racine2 As Decimal = Math.Sqrt(2)

        If PhiEtrier < (DiaCourbureSupInt / (2 * Racine2) * (2 - Racine2)) Then
            'Cas 1 : intersection entièrement comprise dans l'arc de cercle extérieur
            Dim Theta As Single = Math.Acos(DiaCourbureSupInt / (2 * (DiaCourbureSupInt / 2 + PhiEtrier)))
            AjouteArcCercle(xc, yc, RayonC, 0, -Theta * 180 / Math.PI, 1, xPts, yPts, nbPts)
        Else
            'Cas 2 : intersection en dehors de l'arc de cercle extérieur
            xo = xc + DiaCourbureSupInt / 2
            yo = yc + DiaCourbureSupInt / 2 - Racine2 * (DiaCourbureSupInt / 2 + PhiEtrier)

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            AjouteArcCercle(xc, yc, RayonC, 0, -45, 1, xPts, yPts, nbPts)
        End If

        '--> Contour extérieur en sens inverse <----------------------------------------------------------------------

        '=# Armature inférieure intérieure

        RayonC = DiaCourbureInfInt / 2 + PhiEtrier
        xc = -Tw / 2 - UyInt - DiaCourbureInfInt / 2 - PhiEtrier
        yc = -Ht + Tf + Uz + DiaCourbureInfInt / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 270, 360, -1, xPts, yPts, nbPts)

        '=# Armature inférieure extérieure

        RayonC = DiaCourbureInfExt / 2 + PhiEtrier
        xc = -Bc / 2 + Uy + (DiaCourbureInfExt) / 2 + PhiEtrier
        yc = -Ht + Tf + Uz + DiaCourbureInfExt / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, -1, xPts, yPts, nbPts)

        '=# Armature supérieure extérieure

        RayonC = DiaCourbureSupExt / 2 + PhiEtrier
        xc = -Bc / 2 + Uy + (DiaCourbureSupExt) / 2 + PhiEtrier
        yc = -Tf - Uz - DiaCourbureSupExt / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 90, 180, -1, xPts, yPts, nbPts)

        '=# Armature supérieure intérieure

        RayonC = DiaCourbureSupInt / 2 + PhiEtrier
        xc = -Tw / 2 - Uy - DiaCourbureSupInt / 2 - PhiEtrier
        yc = -Tf - Uz - DiaCourbureSupInt / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, -45, 90, -1, xPts, yPts, nbPts)

        xo = xc + Math.Sqrt(2) / 2 * (RayonC - LongueurRetour)
        yo = yc - Math.Sqrt(2) / 2 * (RayonC + LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier
    ''' </summary>
    Private Sub PrepareContourEtriers(ByVal profile As cls_ProfilA, enrobage As Cls_Enrobage_Partiel,
                                      ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer, Optional lPartiel As Boolean = False)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DiaCourbureSup As Decimal = 0.008
        Dim DiaCourbureInf As Decimal = 0.008
        Dim LongueurRetour As Decimal = 3 * enrobage.Etriers_Phi
        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.b_fs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.t_w
        Dim Tf As Decimal = profile.t_fs
        Dim Uy As Decimal = enrobage.Etriers_EnrobageY
        Dim Uz As Decimal = enrobage.Etriers_EnrobageZ
        Dim Bc As Decimal = enrobage.Ratio_bc * Bf
        Dim PhiEtrier As Decimal = enrobage.Etriers_Phi

        Dim DiametreC As Decimal

        '--> Initialisation

        nbPts = 0

        '--> Contour interieur

        DiametreC = DiaCourbureSup

        xc = -Tw / 2 - Uy - DiaCourbureSup / 2
        yc = Ht / 2 - Tf - Uz - DiaCourbureSup / 2

        xo = xc + Math.Sqrt(2) / 2 * (DiametreC / 2 - LongueurRetour)
        yo = yc - Math.Sqrt(2) / 2 * (DiametreC / 2 + LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        AjouteArcCercle(xc, yc, DiametreC / 2, -45, 90, 1, xPts, yPts, nbPts)

        xc = -Bc / 2 + Uy + DiaCourbureSup / 2

        AjouteArcCercle(xc, yc, DiametreC / 2, 90, 180, 1, xPts, yPts, nbPts)

        If Not lPartiel Then
            DiametreC = DiaCourbureInf
            yc = -(Ht / 2 - Tf - Uz - DiaCourbureInf / 2)

            AjouteArcCercle(xc, yc, DiametreC / 2, 180, 270, 1, xPts, yPts, nbPts)

            xc = -Tw / 2 - Uy - DiaCourbureInf / 2

            AjouteArcCercle(xc, yc, DiametreC / 2, 270, 360, 1, xPts, yPts, nbPts)

            DiametreC = DiaCourbureSup
            yc = Ht / 2 - Tf - Uz - DiaCourbureSup / 2

            AjouteArcCercle(xc, yc, DiametreC / 2, 0, 135, 1, xPts, yPts, nbPts)

            xo = xc - Math.Sqrt(2) / 2 * (DiametreC / 2 + LongueurRetour)
            yo = yc + Math.Sqrt(2) / 2 * (DiametreC / 2 - LongueurRetour)

            AjoutePoint(xo, yo, xPts, yPts, nbPts)
        End If

        '--> Contour extérieur en sens inverse 

        DiametreC = DiaCourbureSup + PhiEtrier
        xc = -Tw / 2 - Uy - (DiaCourbureSup) / 2
        yc = Ht / 2 - Tf - Uz - (DiaCourbureSup) / 2

        xo = xc - Math.Sqrt(2) / 2 * (DiametreC / 2 + LongueurRetour)
        yo = yc + Math.Sqrt(2) / 2 * (DiametreC / 2 - LongueurRetour)

        If Not lPartiel Then
            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            AjouteArcCercle(xc, yc, DiametreC / 2, 0, 135, -1, xPts, yPts, nbPts)

            DiametreC = DiaCourbureInf + PhiEtrier
            yc = -(Ht / 2 - Tf - Uz - DiaCourbureInf / 2)

            AjouteArcCercle(xc, yc, DiametreC / 2, 270, 360, -1, xPts, yPts, nbPts)

            xc = -Bc / 2 + Uy + (DiaCourbureInf) / 2

            AjouteArcCercle(xc, yc, DiametreC / 2, 180, 270, -1, xPts, yPts, nbPts)
        End If

        xc = -Bc / 2 + Uy + (DiaCourbureInf) / 2
        DiametreC = DiaCourbureSup + PhiEtrier
        yc = (Ht / 2 - Tf - Uz - DiaCourbureSup / 2)

        AjouteArcCercle(xc, yc, DiametreC / 2, 90, 180, -1, xPts, yPts, nbPts)

        xc = -Tw / 2 - Uy - DiaCourbureSup / 2

        AjouteArcCercle(xc, yc, DiametreC / 2, -45, 90, -1, xPts, yPts, nbPts)

        xo = xc + Math.Sqrt(2) / 2 * (DiametreC / 2 - LongueurRetour)
        yo = yc - Math.Sqrt(2) / 2 * (DiametreC / 2 + LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

    Private Sub AjouteArcCercle(xC As Single, yC As Single, Rayon As Single,
                                Alpha1 As Single, Alpha2 As Single, Sens As Single,
                                ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nDis As Integer
        Dim DeltaAlpha, AlphaInt As Single
        Const UnitAlpha As Single = 5     ' Decoupage par 5°
        Dim AlphaMax, AlphaMin As Single
        Dim kConv As Single = Math.PI / 180
        Dim xo, yo As Single
        Dim AlphaDeb As Single

        '--> Initialisation

        AlphaMax = Math.Max(Alpha1, Alpha2)
        AlphaMin = Math.Min(Alpha1, Alpha2)

        ''If Sens = -1 Then
        ''    AlphaInt = AlphaMax
        ''    AlphaMax = AlphaMin + 360
        ''    AlphaMin = AlphaInt
        ''End If

        nDis = Math.Floor((AlphaMax - AlphaMin) / UnitAlpha)
        DeltaAlpha = (AlphaMax - AlphaMin) / nDis
        AlphaDeb = AlphaMin
        If Sens < 0 Then AlphaDeb = AlphaMax

        For i As Integer = 0 To nDis
            xo = xC + Rayon * Math.Cos((AlphaDeb + Sens * i * DeltaAlpha) * kConv)
            yo = yC + Rayon * Math.Sin((AlphaDeb + Sens * i * DeltaAlpha) * kConv)
            AjoutePoint(xo, yo, xPts, yPts, nbPts)
        Next
    End Sub

#End Region

#Region " Dessins pour le choix du profilé (FRM_SECTIONACIERSTANDARD) "

    Public Sub DessinProfileAcier(ByRef MyGr As Graphics, ByVal section As cls_Section,
                                  ByVal Width As Decimal, ByVal Height As Decimal, ByVal MyFont As Font,
                                  kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                  ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la section acier
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '       0 pour ha
        '       1 pour bfs ou b
        '       2 pour tfs ou tf
        '       3 pour bfi (PRS non sym)
        '       4 pour tfi (PRS non sym)
        '       5 pour tw
        '       6 pour r
        '       7 pour hw
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim lLam As Boolean = (section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine)

        Dim zRef As Decimal = 0

        Dim MyParAff As Struc_Affichage
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Préparation Pinceau dégradé

        Dim myBrushG As New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, CouleurAcierNormal)

        '--> Initialisation des paramètres d'affichage

        yMin = -section.ProfilA.ha
        xMin = -Math.Max(section.ProfilA.b_fs, section.ProfilA.b_fi) / 2
        xMax = -xMin
        yMax = 0

        'If lCote Then
        dCar = Math.Sqrt((section.ProfilA.ha ^ 2 + (section.ProfilA.b_fs + section.ProfilA.b_fi) ^ 2)) / 20
        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, Width, Height, xLeft, yTop, kAdjust)

        '--> Dessin de la section acier

        DessinProfileMetal(MyGr, section.ProfilA, myBrushG, MyParAff, zRef)

        '--> Cotation

        If lCote Then

            Dim MyPen As New Pen(Color.Black, 1)
            Dim xo, yo As Double
            Dim xe, ye As Double
            Dim MyPenNormal As New Pen(ColorNonSelect, 1)
            Dim MyPenSelect As New Pen(ColorSelect, 1)
            Dim MyFontNormal As Font = FontBase
            Dim MyColor As Color
            Dim Chaine As String

            '-- H --

            yo = -section.ProfilA.ha
            ye = 0
            xo = -Math.Max(section.ProfilA.b_fs, section.ProfilA.b_fi) / 2 - dCar
            xe = xo

            MyColor = StyleCouleur(iSelect, 0)
            MyPen.Color = MyColor

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(section.ProfilA.ha, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfi --

            Dim iRef As Int16
            Select Case section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym : iRef = 1
                Case Else : iRef = 3
            End Select
            MyColor = StyleCouleur(iSelect, iRef)
            MyPen.Color = MyColor

            yo = -section.ProfilA.ha - dCar
            ye = yo
            xo = section.ProfilA.b_fi / 2
            xe = -xo

            If lAffSymbol Then
                If lLam Then
                    Chaine = "b"

                Else
                    Chaine = "bfi"
                End If
            Else
                Chaine = GetStringNoUnit(section.ProfilA.b_fi, Enu_TypeVariable.Dimension)
            End If

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfs --

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 1)
                MyPen.Color = MyColor

                yo = 0 + dCar
                ye = yo
                xo = section.ProfilA.b_fs / 2
                xe = -xo
                If lAffSymbol Then Chaine = "bfs" Else Chaine = GetStringNoUnit(section.ProfilA.b_fs, Enu_TypeVariable.Dimension)
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- Hw --

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 7)
                MyPen.Color = MyColor

                yo = 0 - section.ProfilA.t_fs
                ye = -section.ProfilA.ha + section.ProfilA.t_fi
                xo = -Math.Min(section.ProfilA.b_fs, section.ProfilA.b_fi) / 2 + dCar
                xe = xo

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                If lAffSymbol Then Chaine = "hw" Else Chaine = GetStringNoUnit(section.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            End If

            '-- Tfi --

            Select Case section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym : iRef = 2
                Case Else : iRef = 4
            End Select
            MyColor = StyleCouleur(iSelect, iRef)
            MyPen.Color = MyColor

            yo = -section.ProfilA.ha - dCar / 2
            ye = -section.ProfilA.ha
            xo = section.ProfilA.b_fi / 2 - dCar
            xe = xo
            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo = -section.ProfilA.ha + section.ProfilA.t_fi
            ye = yo + dCar
            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then
                If lLam Then Chaine = "tf" Else Chaine = "tfi"
            Else
                Chaine = GetStringInUnit(section.ProfilA.t_fi, Enu_TypeVariable.Dimension, 3, 1, False)
            End If
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tfs --

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 2)
                MyPen.Color = MyColor

                yo = 0 + dCar / 2
                ye = 0
                xo = section.ProfilA.b_fs / 2 - dCar
                xe = xo
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                yo = 0 - section.ProfilA.t_fs
                ye = yo - dCar
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "tfs" Else Chaine = GetStringInUnit(section.ProfilA.t_fs, Enu_TypeVariable.Dimension, 3, 1, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- R --

            If lLam Then

                MyColor = StyleCouleur(iSelect, 6)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                yo = 0 - section.ProfilA.t_fs - section.ProfilA.r_cs * (1 - kProj)
                ye = yo - dCar * kProj

                xo = section.ProfilA.t_w / 2 + section.ProfilA.r_cs * (1 - kProj)
                xe = xo + dCar * kProj

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(section.ProfilA.r_cs, Enu_TypeVariable.Dimension)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- tw --

            MyColor = StyleCouleur(iSelect, 5)
            MyPen.Color = MyColor

            xo = -section.ProfilA.t_w / 2
            xe = xo - dCar / 2
            yo = -(section.ProfilA.ha / 2 - section.ProfilA.t_fs - section.ProfilA.r_cs) * 0.8
            ye = yo

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = section.ProfilA.t_w / 2
            xe = xo + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(section.ProfilA.t_w, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

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
        Dim CouleurTremie As Color
        Dim CouleurArma As Color
        Dim hMaxProfile As Decimal = MyPoutre.HauteurMaxiProfiles

        '--> Intialisations

        Largeur = MyPoutre.EntraxeD1 + MyPoutre.EntraxeD2
        Hauteur = MyPoutre.HauteurTotale
        dCar = Math.Sqrt(Largeur ^ 2 + Hauteur ^ 2) / 10
        LargeurBord = Largeur / 5

        '--> Initialisation des paramètres d'affichage

        'If MyPoutre.lIntermediaire Then
        xMin = -MyPoutre.EntraxeD1 - LargeurBord
        xMax = MyPoutre.EntraxeD2 + LargeurBord
        'End If

        yMin = -dCar - hMaxProfile
        yMax = MyPoutre.Dalle.zTop + dCar

        If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Préparation des Pinceaux utilisés dans le dessin

        CouleurAcier = CouleurAcierNormal
        CouleurBeton = CouleurBetonNormal
        CouleurTremie = CouleurTremieNormal
        CouleurArma = CouleurArmaNormal
        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurAcier, CouleurAcier)
        'Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        'Trémie
        Dim myBrushT As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurTremie, CouleurTremie)
        ' Armatures
        Dim myBrushA As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurArma, CouleurArma)

        '--> Affichage

        DessinFrmCoupeStandard(MyGr, MyPoutre, iSelect, dCar, hMaxProfile, MyParAff, myBrushP, myBrushB, myBrushT, myBrushA)

    End Sub

    Private Sub DessinFrmCoupeStandard(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                       iSelect As Integer, dCar As Decimal, hMaxProfile As Decimal,
                                       MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushT As Brush, myBrushA As Brush)
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

        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        ' Dim IndS As Integer = 1         ' Indice de travée pour représentation des sections
        Dim lEnrob As Boolean = MyPoutre.Section.lEnrobage
        Const ZREF As Decimal = 0

        '--> Affichage de la section principale

        '# Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, MyPoutre.Section, MyParaff1, myBrushB)

        '# Dessin de la section acier

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF)


        '--> Affichage de la voisine à gauche

        '# Dessin de béton d'enrobage

        If MyPoutre.lIntermediaire Then

            If lEnrob Then _
            DessinEnrobagePartielBeton(MyGr, MyPoutre.Section, MyParaff1, myBrushB, -MyPoutre.EntraxeD1)

            '# Dessin de la section acier

            DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF, -MyPoutre.EntraxeD1)

        End If

        '--> Affichage de la voisine à droite

        '# Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, MyPoutre.Section, MyParaff1, myBrushB, +MyPoutre.EntraxeD2)

        '# Dessin de la section acier

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF, MyPoutre.EntraxeD2)

        '--> Affichage de la dalle béton

        If MyPoutre.lIntermediaire Then
            xo = -3 * MyPoutre.EntraxeD1
            xe = 3 * MyPoutre.EntraxeD2
        Else
            xo = -MyPoutre.EntraxeD1
            xe = 3 * MyPoutre.EntraxeD2
        End If

        yo = 0
        ye = MyPoutre.Dalle.t_d

        AddRectanglePlein(MyGr, myBrushB, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)


        '--> Représentation des trémies

        If MyPoutre.lIntermediaire Then
            If MyPoutre.lTremieGauche Then
                xo = -MyPoutre.EntraxeD1 + MyPoutre.DistanceDsl1
                xe = -MyPoutre.DistanceDsl1
                yo = 0
                ye = MyPoutre.Dalle.t_d

                AddRectanglePlein(MyGr, myBrushT, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)
            End If

            If MyPoutre.lTremieDroite Then
                xo = MyPoutre.DistanceDsl2
                xe = MyPoutre.EntraxeD2 - MyPoutre.DistanceDsl2
                yo = 0
                ye = MyPoutre.Dalle.t_d

                AddRectanglePlein(MyGr, myBrushT, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)
            End If
        Else 'Poutre de rive
            If MyPoutre.lTremieDroite Then
                xo = MyPoutre.DistanceDsl2
                xe = MyPoutre.EntraxeD2 - MyPoutre.DistanceDsl2
                yo = 0
                ye = MyPoutre.Dalle.t_d

                AddRectanglePlein(MyGr, myBrushT, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)

            End If

        End If

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
        Dim LongueurPoutre, HauteurPoutre As Decimal
        Dim LongueurDalle, HauteurDalle As Decimal
        Dim MyBrushA As New SolidBrush(Color.LightGray)
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Initialisations

        LongueurPoutre = MyPoutre.LongueurTotale
        HauteurPoutre = MyPoutre.HauteurTotale
        LongueurDalle = MyPoutre.LongueurTotale
        HauteurDalle = MyPoutre.Dalle.t_d
        dCar = Math.Sqrt(LongueurPoutre ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = HauteurPoutre / 2

        '--> Initialisation des paramètres d'affichage

        xMin = 0
        xMax = LongueurPoutre
        yMin = -dCar - dCarApp
        yMax = HauteurPoutre + dCar

        If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre 

        xe = 0
        yo = 0
        ye = HauteurPoutre

        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee

            xo = xe
            xe = xo + MyPoutre.LongueurTravee(i)

            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Next

        '--> Représentation des appuis

        For i As Integer = 1 To MyPoutre.NombreTraveesDeuxAppuis

            xo = MyPoutre.xPositionAppui(True, i)
            DessineAppui(MyGr, xo, dCarApp, MyParAff)

        Next

        xo = MyPoutre.xPositionAppui(False, MyPoutre.NombreTraveesDeuxAppuis)
        DessineAppui(MyGr, xo, dCarApp, MyParAff)

        '--> Représentation de la dalle

        xo = 0
        yo = HauteurPoutre

        xe = LongueurDalle
        ye = HauteurPoutre + HauteurDalle

        AddRectanglePlein(MyGr, myBrushB, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

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
                xe = LongueurPoutre

                AddFleche(MyGr, MyPen, xo, yCote - dCar, xe, yCote - dCar, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringNoUnit(LongueurPoutre, Enu_TypeVariable.Longueur)
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

#Region "Dessin pour les maintiens (FRM_MAINTIENS)"

    Public Sub DessinFrmMaintiens(MyGr As Graphics, MyPoutre As cls_Poutre,
                                ByVal pWi As Decimal, ByVal pHi As Decimal,
                                kAdjust As Double, iSelect As Integer, lCote As Boolean,
                                ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/06/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des travées dans la fenêtre maintiens latéraux
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
        Dim LongueurPoutre, LongueurTravee, LongueurConsoleGauche, LongueurConsoleDroite, HauteurPoutre, EpaisseurSemelle, RayonConge As Decimal
        'Dim LongueurDalle, HauteurDalle As Decimal
        Dim MyBrushA As New SolidBrush(Color.LightGray)
        Dim MyBrushSemelleBloquee As New SolidBrush(Color.Red)
        Dim MyBrushSemelleNonBloquee As New SolidBrush(Color.Green)
        Dim MyBrushMaintienSup As New SolidBrush(Color.Red)
        Dim MyBrushMaintienInf As New SolidBrush(Color.Red)
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyPenDot As New Pen(Color.Black, 1) With {
            .DashStyle = DashStyle.Dash
        }
        Dim MyColor As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Initialisations

        LongueurPoutre = MyPoutre.LongueurTotale
        LongueurTravee = MyPoutre.PORTEEDEFAUT / 1.5
        If MyPoutre.lTraveeConsoleGauche Then LongueurConsoleGauche = LongueurTravee / 2
        If MyPoutre.lTraveeConsoleDroite Then LongueurConsoleDroite = LongueurTravee / 2
        HauteurPoutre = MyPoutre.HauteurTotale
        EpaisseurSemelle = HauteurPoutre / 10
        RayonConge = EpaisseurSemelle / 2
        'LongueurDalle = MyPoutre.LongueurTotale
        'HauteurDalle = MyPoutre.Dalle.t_d
        dCar = Math.Sqrt(LongueurTravee ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = HauteurPoutre / 2

        '--> Initialisation des paramètres d'affichage

        'Select Case iSelect
        '    Case 0
        '        xMin = 0
        '        xMax = MyPoutre.LongueurTravee(iSelect)

        '    Case 99
        '        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee - 1
        '            xMin += MyPoutre.LongueurTravee(i)
        '        Next
        '        xMax = xMin + MyPoutre.LongueurTravee(MyPoutre.IndiceDerniereTravee)

        '    Case Else
        '        For i As Integer = MyPoutre.IndicePremiereTravee To iSelect - 1
        '            xMin += MyPoutre.LongueurTravee(i)
        '        Next
        '        xMax = xMin + MyPoutre.LongueurTravee(iSelect)

        'End Select

        Select Case iSelect
            Case 0
                xMin = 0
                xMax = LongueurConsoleGauche

            Case 99
                xMin = LongueurConsoleGauche
                For i As Integer = 1 To MyPoutre.IndiceDerniereTravee - 1
                    xMin += LongueurTravee
                Next
                xMax = xMin + LongueurConsoleDroite

            Case Else
                xMin = LongueurConsoleGauche
                For i As Integer = 1 To iSelect - 1
                    xMin += LongueurTravee
                Next
                xMax = xMin + LongueurTravee

        End Select

        yMin = -dCar - dCarApp
        yMax = HauteurPoutre + dCar

        'If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)



        '--> Représentation de la poutre 

        xe = 0
        yo = 0
        ye = HauteurPoutre

        xo = 0
        xe = LongueurConsoleGauche
        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        yo = 0
        ye = EpaisseurSemelle
        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        yo = HauteurPoutre - EpaisseurSemelle
        ye = HauteurPoutre
        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        yo = EpaisseurSemelle + RayonConge
        ye = yo
        AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAff)

        yo = HauteurPoutre - RayonConge - EpaisseurSemelle
        ye = yo
        AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAff)

        For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1

            yo = 0
            ye = HauteurPoutre

            xo = xe
            xe = xo + LongueurTravee

            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

            yo = 0
            ye = EpaisseurSemelle
            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

            yo = HauteurPoutre - EpaisseurSemelle
            ye = HauteurPoutre
            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

            yo = EpaisseurSemelle + RayonConge
            ye = yo
            AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAff)

            yo = HauteurPoutre - RayonConge - EpaisseurSemelle
            ye = yo
            AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAff)
        Next

        yo = 0
        ye = HauteurPoutre

        xo = xe
        xe = xo + LongueurConsoleDroite
        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        yo = 0
        ye = EpaisseurSemelle
        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        yo = HauteurPoutre - EpaisseurSemelle
        ye = HauteurPoutre
        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        yo = EpaisseurSemelle + RayonConge
        ye = yo
        AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAff)

        yo = HauteurPoutre - RayonConge - EpaisseurSemelle
        ye = yo
        AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAff)

        '--> Représentation des appuis et les maintiens associés

        For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite

            xo = LongueurConsoleGauche + (i - 1) * LongueurTravee

            DessineAppui(MyGr, xo, dCarApp, MyParAff)

            yo = EpaisseurSemelle / 2
            MyBrushMaintienSup = MyBrushSemelleBloquee
            AddCerclePlein(MyGr, MyBrushMaintienInf, xo, yo, EpaisseurSemelle, MyParAff, True)

            yo = HauteurPoutre - EpaisseurSemelle / 2
            MyBrushMaintienInf = MyBrushSemelleBloquee
            AddCerclePlein(MyGr, MyBrushMaintienSup, xo, yo, EpaisseurSemelle, MyParAff, True)

        Next

        '--> Représentation des maintiens latéraux

        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee
            For Each maintiens As cls_Maintiens In MyPoutre.Maintiens(i)
                Select Case maintiens.PositionMaintien
                    Case maintiens.EnuPositionMaintienSection.SemelleSup
                        MyBrushMaintienSup = MyBrushSemelleBloquee
                        MyBrushMaintienInf = MyBrushSemelleNonBloquee

                    Case maintiens.EnuPositionMaintienSection.SemelleInf
                        MyBrushMaintienSup = MyBrushSemelleNonBloquee
                        MyBrushMaintienInf = MyBrushSemelleBloquee

                    Case maintiens.EnuPositionMaintienSection.DeuxSemelles
                        MyBrushMaintienSup = MyBrushSemelleBloquee
                        MyBrushMaintienInf = MyBrushSemelleBloquee

                End Select

                Select Case i
                    Case 0
                        xo = maintiens.x_Loc / MyPoutre.LongueurTravee(MyPoutre.IndicePremiereTravee) * LongueurConsoleGauche

                    Case MyPoutre.IndiceTraveeConsoleDroite
                        xo = LongueurConsoleGauche
                        For j As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1
                            xo += LongueurTravee
                        Next
                        xo += maintiens.x_Loc / MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite) * LongueurConsoleDroite

                    Case Else
                        xo = LongueurConsoleGauche
                        For j As Integer = 1 To i - 1
                            xo += LongueurTravee
                        Next
                        xo += maintiens.x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurTravee



                End Select

                yo = EpaisseurSemelle / 2
                AddCerclePlein(MyGr, MyBrushMaintienInf, xo, yo, EpaisseurSemelle, MyParAff, True)

                yo = HauteurPoutre - EpaisseurSemelle / 2
                AddCerclePlein(MyGr, MyBrushMaintienSup, xo, yo, EpaisseurSemelle, MyParAff, True)

            Next
        Next

    End Sub

#End Region

#Region " Dessins pour la définition de l'étaiement (FRM_ETAIEMENT)"

    Public Sub DessinFrmEtaiement(MyGr As Graphics, MyPoutre As cls_Poutre,
                                  ByVal pWi As Decimal, ByVal pHi As Decimal,
                                  kAdjust As Double, lCote As Boolean,
                                  ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   08/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du plancher en longitudinal
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   section     [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iSelect As Integer = -1
        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar, dCarApp As Decimal
        Dim MyParAff As Struc_Affichage
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim LongueurPoutre, HauteurPoutre As Decimal
        Dim LongueurDalle, HauteurDalle As Decimal
        Dim MyBrushA As New SolidBrush(Color.LightGray)
        Dim MyBrushE As New SolidBrush(Color.LightGray)
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE
        Dim lPointProp As Boolean = (MyPoutre.TypeEtaiement = MyPoutre.EnuTypeEtaiement.PointPropped)

        '--> Initialisations

        LongueurPoutre = MyPoutre.LongueurTotale
        HauteurPoutre = MyPoutre.HauteurTotale
        LongueurDalle = MyPoutre.LongueurTotale
        HauteurDalle = MyPoutre.Dalle.t_d
        dCar = Math.Sqrt(LongueurPoutre ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = HauteurPoutre / 2

        '--> Initialisation des paramètres d'affichage

        xMin = 0
        xMax = LongueurPoutre
        yMin = -dCar - dCarApp
        yMax = HauteurPoutre + dCar

        If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre 

        xe = 0
        yo = 0
        ye = HauteurPoutre

        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee

            xo = xe
            xe = xo + MyPoutre.LongueurTravee(i)

            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Next

        '--> Représentation des appuis

        For i As Integer = 1 To MyPoutre.NombreTraveesDeuxAppuis

            xo = MyPoutre.xPositionAppui(True, i)
            DessineAppui(MyGr, xo, dCarApp, MyParAff)

        Next

        xo = MyPoutre.xPositionAppui(False, MyPoutre.NombreTraveesDeuxAppuis)
        DessineAppui(MyGr, xo, dCarApp, MyParAff)

        '--> Représentation des étais d'extrémité

        If lPointProp And MyPoutre.lEtaisConsoleGauche Then

            If MyPoutre.lTraveeConsoleGauche Then
                xo = MyPoutre.xPositionAppui(True, 0)
                DessineEtais(MyGr, xo, dCarApp, MyParAff)
            End If

        End If

        If lPointProp And MyPoutre.lEtaisConsoleDroite Then

            If MyPoutre.lTraveeConsoleDroite Then
                xo = MyPoutre.xPositionAppui(False, MyPoutre.IndiceDerniereTravee)
                DessineEtais(MyGr, xo, 0.75 * dCarApp, MyParAff)
            End If

        End If

        '--> Représentation des étais intermédiaires

        If lPointProp And MyPoutre.pNbPropping <> 0 Then

            For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1
                For j As Integer = 1 To MyPoutre.pNbPropping
                    xo = MyPoutre.xPositionAppui(True, i) + j * MyPoutre.LongueurTravee(i) / (MyPoutre.pNbPropping + 1)
                    DessineEtais(MyGr, xo, 0.75 * dCarApp, MyParAff)
                Next
            Next
        End If

        '--> Représentation des appuis continus (POM)

        Dim Longueur, LongueurMax, DeltaX As Decimal
        Dim NbPts As Integer

        If (MyPoutre.TypeEtaiement = MyPoutre.EnuTypeEtaiement.FullyPropped) Then
            LongueurMax = MyPoutre.LongueurTraveeMax
            DeltaX = LongueurMax / 25

            For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee

                xo = MyPoutre.xPositionAppui(True, i)
                Longueur = MyPoutre.LongueurTravee(i)

                NbPts = Math.Floor(Longueur / DeltaX) - 1

                For j As Integer = 1 To NbPts
                    xe = xo + (j) * DeltaX

                    DessineEtais(MyGr, xe, dCarApp / 2, MyParAff)
                Next
            Next
        End If

        '--> Représentation de la dalle

        xo = 0
        yo = HauteurPoutre

        xe = LongueurDalle
        ye = HauteurPoutre + HauteurDalle

        AddRectanglePlein(MyGr, myBrushB, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

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

                xe = MyPoutre.xPositionAppui(True, i)
                MyColor = StyleCouleur(iSelect, i)
                MyPen.Color = MyColor

                If lPointProp Then
                    For j As Integer = 1 To MyPoutre.pNbPropping + 1

                        xo = xe
                        xe += MyPoutre.LongueurTravee(i) / (MyPoutre.pNbPropping + 1)

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                        If lAffSymbol Then Chaine = "Lpp" Else Chaine = GetStringNoUnit(MyPoutre.LongueurTravee(i) / (MyPoutre.pNbPropping + 1), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                    Next
                Else
                    xo = xe
                    xe += MyPoutre.LongueurTravee(i)

                    AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                    If lAffSymbol Then Chaine = "Lpp" Else Chaine = GetStringNoUnit(MyPoutre.LongueurTravee(i), Enu_TypeVariable.Longueur)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                End If

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

        End If


    End Sub

    Private Sub DessineEtais(MyGr As Graphics, xPos As Decimal, dCar As Decimal, MyParAff As Struc_Affichage)
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
        Dim MyBrushAp As New SolidBrush(Color.DarkRed)

        '--> Initialisations

        PrepareContourAppui(xPos, dCar, xPts, yPts, nbPts)

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPts, yPts, nbPts, MyParAff, True, True)

    End Sub

#End Region

#Region " Dessins pour le choix des sections (FRM_AJOUTEPP) "

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

            Case cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SABmixte
                DessinFrmTypeSAB(MyGr, MySection, MyDalle, MyParAff, myBrushP, myBrushB, myBrushA)

        End Select

        '--> Fin

        myBrushP.Dispose()
        myBrushB.Dispose()
        myBrushA.Dispose()

    End Sub

    Private Sub DessinFrmTypeSAB(ByRef MyGr As Graphics, ByVal MySection As cls_Section, MyDalle As Cls_Dalle,
                                 MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)

        '------------------------------------------------------------------------------------------------------------------
        '   09/06/23 :  Création - FuD
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

        Dim ZREF As Decimal = MySection.ProfilA.t_fi
        Dim lMixte As Boolean = (MySection.typeSection = cls_Section.Enum_TypeSection.SABmixte)

        '--> Dessin de la dalle pour un SFB mixte

        If lMixte Then
            DessinDalleSlimFloor(MyGr, MyDalle, MySection.ProfilA.ha, MyParaff1, myBrushB, ZREF)
        End If

        '--> Dessin de la section acier
        ZREF = MySection.ProfilA.ha
        DessinProfileMetal(MyGr, MySection.ProfilA, myBrushP, MyParaff1, ZREF)


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

    Private Sub DessinDalleSlimFloor(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, Ha As Decimal, MyParAffloc As Struc_Affichage, MyBrushB As Brush, Optional ZREF As Decimal = 0)
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
        yo = ZREF

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
                xo = xPos - MyProfil.b_fs / 2
                ye = -zRef
                yo = -MyProfil.t_fs - zRef

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

            For i = 18 To 23
                xPts(i) = - .t_w / 2 - .r_ci * (1 + CSng(Math.Cos(Math.PI - (i - 18) * DeltaA)))
                yPts(i) = - .ha + .t_fi + .r_ci - .r_ci * CSng(Math.Sin(Math.PI - (i - 18) * DeltaA))
            Next

            xPts(24) = - .b_fi / 2
            yPts(24) = - .ha + .t_fi

            xPts(25) = - .b_fi / 2
            yPts(25) = - .ha

            For i = 26 To 35
                xPts(i) = -xPts(51 - i)
                yPts(i) = yPts(51 - i)
            Next

            'For i = 18 To 35
            '    xPts(i) = xPts(35 - i)
            '    yPts(i) = - .ha - yPts(35 - i)
            'Next

        End With

    End Sub

#End Region

#Region " Outils pour le dessin du béton d'enrobage (y compris les armatures) "

    Private Sub DessinEnrobagePartielBeton(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, MyRatioBc As Decimal,
                                           MyParAff As Struc_Affichage, MyBrushBp As Brush, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section affichée
        '   
        '   MyParAff    [E] :   Paramètres d'affichage
        '   MyBrushBp   [E] :   Pinceau
        '---------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xe, ye As Decimal
        Dim xo, yo As Decimal
        Dim MyPenContour As New Pen(Color.Black, 1)

        '--> Dessin bloc béton

        xe = xPos + profile.b_fi / 2 * MyRatioBc
        xo = xPos - profile.b_fi / 2 * MyRatioBc
        ye = -profile.ha + profile.t_fi
        yo = -profile.t_fs

        AddRectanglePlein(MyGr, MyBrushBp, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

    End Sub

    Private Sub DessinEnrobagePartielBeton(ByRef MyGr As Graphics, ByVal MySection As cls_Section,
                                           MyParAff As Struc_Affichage, MyBrushBp As Brush, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section affichée
        '   MyParAff    [E] :   Paramètres d'affichage
        '   MyBrushBp   [E] :   Pinceau
        '---------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------

        DessinEnrobagePartielBeton(MyGr, MySection.ProfilA, MySection.enrobage_partiel.Ratio_bc, MyParAff, MyBrushBp, xPos)

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

        DessinArmaLongiEnrobage(MyGr, MySection.ProfilA, MySection.enrobage_partiel, MyParAffloc, MyBrushA, iArma)

    End Sub


    Private Sub DessinArmaLongiEnrobageN(ByRef MyGr As Graphics, ByVal MySection As cls_Section, MyEnrob As Cls_Enrobage_Partiel,
                                        MyParAffloc As Struc_Affichage, MyBrushA() As Brush, iArma As Integer)
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
        Dim Bfs, Tw As Decimal
        Dim PhiA As Decimal
        Dim Uy, Uz, EtriersPhi As Decimal
        Dim kPos() As Decimal = {0, -0.5, 0.5, 0}
        Dim kPosE() As Decimal = {0, 0, 1, 0.5}

        '--> Initialisation

        Select Case MyEnrob.Etriers_Type
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.Cadre : uYInterne = MyEnrob.Etriers_EnrobageYinterne
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant : uYInterne = 0
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude : uYInterne = 0
        End Select

        Uy = MyEnrob.Etriers_EnrobageY
        Uz = MyEnrob.Etriers_EnrobageZ
        EtriersPhi = MyEnrob.Etriers_Phi
        Bfs = MySection.ProfilA.b_fs
        Tw = MySection.ProfilA.t_w

        '--> Armatures extérieures

        PhiA = MyEnrob.LitArma(iArma).PhiExt

        For jChambre As Integer = 0 To 1

            For i As Integer = 1 To MyEnrob.LitArma(iArma).NbExt

                xo = Signe * (Bfs * MyEnrob.Ratio_bc / 2 - Uy - EtriersPhi - PhiA / 2)
                yo = MySection.zPosArmaEnrobage(iArma, 0, i)

                AddCerclePlein(MyGr, MyBrushA(0), xo - Signe * kPosE(i) * PhiA, yo, PhiA, MyParAffloc, True)

            Next

            Signe = -Signe

        Next

        '--> Armatures intérieures

        PhiA = MyEnrob.LitArma(iArma).PhiInt

        If MyEnrob.LitArma(iArma).NbInt = 1 Then kPos(1) = 0 Else kPos(1) = -0.5

        For jChambre As Integer = 0 To 1

            For i As Integer = 1 To MyEnrob.LitArma(iArma).NbInt

                yo = MySection.zPosArmaEnrobage(iArma, 2, i)
                xo = Signe * (Tw / 2 + uYInterne + EtriersPhi + PhiA / 2)

                AddCerclePlein(MyGr, MyBrushA(2), xo + Signe * kPosE(i) * PhiA, yo, PhiA, MyParAffloc, True)

            Next

            Signe = -Signe

        Next

        '--> Armatures centrales

        PhiA = MyEnrob.LitArma(iArma).PhiMil

        If MyEnrob.LitArma(iArma).NbMil = 1 Then kPos(1) = 0 Else kPos(1) = -0.5

        For jChambre As Integer = 0 To 1

            xo = Signe * ((Tw / 2 + uYInterne + EtriersPhi) + (Bfs * MyEnrob.Ratio_bc / 2 - Uy - EtriersPhi)) / 2

            For i As Integer = 1 To MyEnrob.LitArma(iArma).NbMil

                yo = MySection.zPosArmaEnrobage(iArma, 1, i)
                AddCerclePlein(MyGr, MyBrushA(1), xo + Signe * kPos(i) * PhiA, yo, PhiA, MyParAffloc, True)

            Next

            Signe = -Signe

        Next

    End Sub

    Private Sub DessinArmaLongiEnrobage(ByRef MyGr As Graphics, ByVal MyProfil As cls_ProfilA, MyEnrob As Cls_Enrobage_Partiel,
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
                yo = -MyProfil.ha + MyProfil.t_fi + MyEnrob.Etriers_EnrobageZ + MyEnrob.Etriers_Phi + MyEnrob.LitsArmaOLD(iArma).Phi / 2
            Case 1
                yo = -MyProfil.ha / 2
            Case 2
                yo = -MyProfil.t_fs - MyEnrob.Etriers_EnrobageZ - MyEnrob.Etriers_Phi - MyEnrob.LitsArmaOLD(iArma).Phi / 2
        End Select
        Select Case MyEnrob.Etriers_Type
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.Cadre : uYInterne = MyEnrob.Etriers_EnrobageY
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant : uYInterne = 0
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude : uYInterne = 0
        End Select
        DeltaY(1) = 0
        If (MyEnrob.Etriers_Type = Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant) Then
            DeltaY(0) = MyEnrob.Etriers_Phi / 5
        Else
            DeltaY(0) = 0
        End If

        '--> Boucle sur les armatures du lit

        For jChambre As Integer = 0 To 1

            xo = Signe * (MyProfil.b_fs * MyEnrob.Ratio_bc / 2 - MyEnrob.Etriers_EnrobageY - MyEnrob.Etriers_Phi - MyEnrob.LitsArmaOLD(iArma).Phi / 2)
            xe = Signe * (MyProfil.t_w / 2 + uYInterne + MyEnrob.Etriers_Phi + MyEnrob.LitsArmaOLD(iArma).Phi / 2)

            If MyEnrob.LitsArmaOLD(iArma).nbArma = 1 Then
                Delta = 1
            Else
                Delta = (xe - xo) / (MyEnrob.LitsArmaOLD(iArma).nbArma - 1)
            End If
            For i As Integer = 1 To MyEnrob.LitsArmaOLD(iArma).nbArma

                xi = xo + Delta * (i - 1)

                AddCerclePlein(MyGr, MyBrushA, xi, yo + DeltaY(jChambre), MyEnrob.LitsArmaOLD(iArma).Phi, MyParAffloc, True)

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

            Case cls_Section.Enum_TypeSection.SAB
                yMin = 0
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = MySection.ProfilA.ha

            Case cls_Section.Enum_TypeSection.SABmixte
                yMin = 0
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
