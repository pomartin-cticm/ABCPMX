'Option Strict On

Imports System.Collections.Specialized.BitVector32
Imports System.Drawing.Drawing2D
Imports PMXMoteur2

Module Mod_Dessins

#Region " Variables locales "

    Dim MyPenContour As New Pen(Color.Black, 1)

    Public Const lCONTOURCOTE As Boolean = False

#End Region

#Region " Dessins pour la définiton de la dalle (FRM_DALLEN) "

    Public Sub DessineDalle(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyDalle As Cls_Dalle,
                            MySection As cls_Section, iSelect As Integer,
                            ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   MyDalle     [E] :   Dalle à dessiner
        '   MySection   [E] :   Section à laquelle la dalle est rattachée
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------
        '   iSelect:    0 épaisseur de la dalle
        '               1 épaisseur renformis
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double
        Dim lMixte, lEnrob, lLamine As Boolean
        Dim Beff As Decimal
        Dim BeffG, BeffD As Decimal

        Dim ColorLocalEtriers As Color = CouleurArmaNormal
        Dim ColorLocalArma(2) As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurAcier As Color = CouleurAcierNormal
        'Dim pColorLocalArma(2, 2) As Color
        Dim ColorArmatures(1) As Color
        Const kADJUST As Decimal = 0.95
        Const zREF As Decimal = 0
        Dim Ha, Bfs As Decimal
        Dim lCote As Boolean = True

        '--> Initialisation

        lMixte = MyProjet.Poutres(MyProjet.IndEnCours).Section.lMixte
        lEnrob = MyProjet.Poutres(MyProjet.IndEnCours).Section.lEnrobage
        lLamine = MyProjet.Poutres(MyProjet.IndEnCours).Section.lLamine

        ' A REVOIR ====
        Beff = LargeurDalleDessin(MySection.ProfilA)
        BeffG = Beff / 2
        BeffD = Beff / 2

        Ha = MySection.ProfilA.ha
        Bfs = MySection.ProfilA.b_fs

        '--> Preparation de la zone d'affichage - Calcul de ParAff
        dCar = MyDalle.EpaisseurActive / 5

        xMin = -Beff / 2 - dCar
        xMax = Beff / 2

        yMin = -MySection.ProfilA.ha
        yMax = MyDalle.zTop + dCar

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Préparation des Pinceaux utilisés dans le dessin

        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcier)
        ' Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        ' Béton prefabriqué
        Dim myBrushPref As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.LightGray, CouleurBeton)
        ' Etriers
        Dim myBrushE As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalEtriers, ColorLocalEtriers)
        ' Armatures de l'enrobage
        Dim myBrushArmaE As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurArmaNormal)
        ' Etriers
        Dim myBrushA(1) As Brush

        Select Case iSelect
            Case 100, 101, 102, 103
                ColorArmatures(0) = CouleurArmaSelect
                ColorArmatures(1) = CouleurArmaNormal
            Case 200, 201, 202, 203
                ColorArmatures(1) = CouleurArmaSelect
                ColorArmatures(0) = CouleurArmaNormal
            Case Else
                ColorArmatures(0) = CouleurArmaNormal
                ColorArmatures(1) = CouleurArmaNormal
        End Select
        'myBrushA(0) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.LightGray, ColorArmatures(0))
        myBrushA(0) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorArmatures(0), ColorArmatures(0))
        myBrushA(1) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorArmatures(1), ColorArmatures(1))

        '--> Dessin de béton

        If lEnrob Then _
        DessinEnrobagePartielBeton(myGr, MySection.ProfilA, MySection.enrobage_partiel.Ratio_bc, MyParAff, myBrushB)

        '--> Dessin de la section acier

        DessinProfileMetal(myGr, MySection.ProfilA, myBrushP, MyParAff, zREF)

        '--> Dessin des étriers

        If lEnrob Then _
        DessinEtriers(myGr, MySection.ProfilA, MySection.enrobage_partiel, MyParAff, myBrushE, zREF)

        '--> Dessins des connecteurs

        '--> Dessin de la dalle

        'If lMixte Then 'Mise en commentaire GUD

        '# Dalle béton

        Select Case MyDalle.type
                Case Cls_Dalle.Enum_TypeDalle.Pleine
                    DessinDallePleine(myGr, MyDalle, Ha, Bfs, MyParAff, myBrushB, Beff)
                Case Cls_Dalle.Enum_TypeDalle.Mixte
                    Select Case MyDalle.Bac.orientation
                        Case Cls_Bac.Enum_Orientation.Parallele
                            DessineDalleMixteParallele(myGr, MyDalle, Ha, Bfs, MyParAff, myBrushB, Beff)
                        Case Cls_Bac.Enum_Orientation.Perpendiculaire

                    End Select
                    'DessinDalleMixte(myGr, Section.dalle, BeffRed, Section.ha, Section.b_fs, zREF, MyParAff, myBrushB)
                Case Cls_Dalle.Enum_TypeDalle.Prefabriquee
                    DessinDallePreFab(myGr, MyDalle, Ha, Bfs, MyParAff, myBrushB, myBrushPref, Beff)
            End Select

            '# Armatures

            DessinLitArmaDalle(myGr, MyDalle, Beff, 0, MySection.ProfilA.ha, iSelect, MyParAff, myBrushA(0))
            DessinLitArmaDalle(myGr, MyDalle, Beff, 1, MySection.ProfilA.ha, iSelect, MyParAff, myBrushA(1))

        'End If

        If lCote Then

            DessinCoteFrmDalle(myGr, MyDalle, MySection, iSelect, MyParAff, dCar, BeffG, BeffD)

        End If
    End Sub

    Private Sub DessinCoteFrmDalle(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, MySection As cls_Section, iSelect As Integer,
                                   MyParAffA As Struc_Affichage, dCar As Decimal, BeffG As Decimal, BeffD As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   07/07/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin des cotes de la dalle
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   MyDalle     [E] :   Dalle à dessiner
        '   MySection   [E] :   Section à laquelle la dalle est rattachée
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   MyParAffA   [E] :   Paramètres d'affichage
        '   dCar        [E] :   Dimension pour l'affichage
        '   bEffG, BEffD[E] :   Largeur de dalle représentée à gauche et à droite
        '-----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xCoteZ As Decimal = -BeffG - dCar
        Dim xe, ye As Decimal
        Dim xo, yo As Decimal
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color

        Dim lContour As Boolean = lCONTOURCOTE
        Dim MyFontNormal As Font = FontBase

        Dim Chaine As String

        '--> Cotations

        '# Hauteur de la section

        MyColor = StyleCouleur(iSelect, -2)
        MyPen.Color = MyColor

        yo = -MySection.ProfilA.ha
        ye = 0

        AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, MyParAffA, True, True)
        Chaine = GetStringNoUnit(MySection.ProfilA.ha, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, (yo + ye) / 2, MyParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

    End Sub

    Private Sub DessinLitArmaDalle(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, BeffRed As Decimal, iArma As Integer,
                                   Ha As Decimal, iSelect As Integer,
                                   MyParAffA As Struc_Affichage, MyBrushArma As Brush)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'un lit d'armatures
        '---------------------------------------------------------------------------------------------------------------------------

        Dim xBOne As Decimal

        DessinLitArmaDalle(MyGr, MyDalle, BeffRed, iArma, Ha, iSelect, MyParAffA, MyBrushArma, xBOne)

    End Sub
    Private Sub DessinLitArmaDalle(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, BeffRed As Decimal, iArma As Integer,
                                   Ha As Decimal, iSelect As Integer,
                                   MyParAffA As Struc_Affichage, MyBrushArma As Brush, ByRef xBOne As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   09/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'un lit d'armatures
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   Dalle
        '   BeffRed     [E] :   Largeur de dalle représentée à l'écran
        '   iArma       [E] :   Indice du lit d'armature
        '   Ha          [E] :   Hauteur du profilé
        '   iSelect     [E] :   Indice de la cote sélectionnée
        '   MyParAffA   [E] :   Paramètres d'affichage
        '   MyBrishArma [E] :   Pinceau
        '   xBone       [S] :   
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Beff, Td, Th As Decimal
        Dim PhiS, EspBar, Zs As Decimal
        Dim nbBar As Integer
        Dim xc, yc As Single
        Dim zTop As Decimal
        Dim lCote As Boolean
        Dim xe, ye As Decimal
        Dim xo, yo As Decimal
        Dim dCar As Decimal

        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color

        Dim lContour As Boolean = lCONTOURCOTE
        Dim MyFontNormal As Font = FontBase

        Dim Chaine As String

        '--> Initialisation

        Beff = BeffRed   ' MyDalle.Beff
        Td = MyDalle.t_d
        PhiS = MyDalle.LitArma(iArma).PhiS
        Zs = MyDalle.LitArma(iArma).z_s
        EspBar = MyDalle.LitArma(iArma).EspBar
        Th = MyDalle.EpRenformis
        zTop = MyDalle.zTop

        lCote = (iSelect > (iArma + 1) * 100) And (iSelect < (iArma + 1) * 100 + 99)

        dCar = MyDalle.EpaisseurActive / 5

        '--> Dessin

        If MyDalle.LitArma(iArma).lActive Then

            If (Beff > 2 * EspBar) Or (Beff < EspBar) Then

                nbBar = Math.Floor((Beff) / (2 * EspBar))
                xc = 0
                yc = zTop - Zs
                AddCerclePlein(MyGr, MyBrushArma, xc, yc, PhiS, MyParAffA, True)

                For i As Integer = 1 To nbBar
                    AddCerclePlein(MyGr, MyBrushArma, xc + (i) * EspBar, yc, PhiS, MyParAffA, True)
                    AddCerclePlein(MyGr, MyBrushArma, xc - (i) * EspBar, yc, PhiS, MyParAffA, True)
                Next

                xBOne = 0 - Math.Min(1, nbBar) * EspBar
            Else

                nbBar = Math.Floor((Beff) / (EspBar))

                For i As Integer = 1 To nbBar

                    xc = -(nbBar - 1) * EspBar / 2 + (i - 1) * EspBar
                    yc = zTop - Zs

                    AddCerclePlein(MyGr, MyBrushArma, xc, yc, PhiS, MyParAffA, True)

                Next

                xBOne = -(nbBar - 1) * EspBar / 2 + (Math.Floor(nbBar / 2) - 1) * EspBar
            End If

        End If

        If lCote Then

            '# diametre

            MyColor = StyleCouleur(iSelect, (iArma + 1) * 100 + 1)
            MyPen.Color = MyColor

            xo = xBOne
            xe = xo

            yo = zTop - Zs - PhiS / 2
            ye = yo - dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffA, True, False)

            yo = zTop - Zs + PhiS / 2
            ye = zTop + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffA, True, False)
            Chaine = GetStringNoUnit(PhiS, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '# Espacement

            MyColor = StyleCouleur(iSelect, (iArma + 1) * 100 + 2)
            MyPen.Color = MyColor

            xo = xBOne
            xe = xo + EspBar

            yo = zTop - Zs - PhiS / 2 - dCar
            ye = yo

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffA, True, True)
            Chaine = GetStringNoUnit(EspBar, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), ye, MyParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '# Position

            MyColor = StyleCouleur(iSelect, (iArma + 1) * 100 + 3)
            MyPen.Color = MyColor

            xo = xBOne + EspBar
            xe = xo + PhiS * 1.5

            yo = zTop - Zs
            ye = yo

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAffA)

            xo = xBOne + EspBar + PhiS * 1.25
            xe = xo

            yo = zTop - Zs
            ye = yo - dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffA, True, False)

            yo = zTop - Zs
            ye = zTop

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAffA)

            yo = zTop + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAffA, False, True)
            Chaine = GetStringNoUnit(Zs, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yo, MyParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

    Private Function LargeurDalleDessin(Profile As cls_ProfilA) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Renvoie la largeur de dalle à représenter
        '-----------------------------------------------------------------------------------------------
        '   
        '-----------------------------------------------------------------------------------------------


        '--> Initialisation

        Dim Diagonale As Decimal = Math.Sqrt((Profile.b_fi + Profile.b_fs) ^ 2 / 4 + Profile.ha ^ 2)
        Dim BfMax As Decimal = Math.Max(Profile.b_fs, Profile.b_fi)
        Dim BeffRed As Decimal

        '--> Traitement

        BeffRed = 2 * Diagonale ' Math.Min(MySection.dalle.Beff, 1.5 * Diagonale)

        Return BeffRed

    End Function

#End Region

#Region "Dessins pour la définition du bac (FRM_BACN)"

    Public Sub DessineBac(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, kAdjust As Double, MyBac As Cls_Bac,
                          ByVal EpDalle As Double, ByRef iCote As Integer,
                          ByVal lCotation As Boolean, ByVal lCotEpTot As Boolean,
                          ByVal lTitre As Boolean,
                          ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
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
        'Dim DeltaX As Double
        Dim sDecal As Double
        Dim tDecal As Double
        Dim dCar As Double

        Dim lRaidSup As Boolean

        Dim i As Integer

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim nbOndes As Integer = 5

        Dim lUn As Boolean = True

        '--> Initialisation

        lRaidSup = MyBac.HasRaidisseurSup
        If lUn Then
            dCar = Math.Sqrt(MyBac.h_p ^ 2 + MyBac.e_p ^ 2) / 16
        Else
            dCar = (MyBac.e_p + MyBac.b_b) / 2
        End If

        '--> Preparation de la zone d'affichage - Calcul de ParAff

        If lUn Then
            xMin = -MyBac.e_p / 2
            xMax = MyBac.e_p / 2
        Else
            xMin = 0
            xMax = MyBac.LargeurModule
        End If
        'nbOndes = Math.Floor(MyBac.LargeurModule / MyBac.e_p)
        yMin = 0
        yMax = MyBac.Hauteur_hpg
        If lCotation Then
            yMin = -dCar
            yMax += dCar
        End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Calcul des points du pourtour de la dalle

        If lRaidSup Then
            MyBac.PrepareContourBacRaidi1Nervure(xPts, yPts, nbPts)
        Else
            MyBac.PrepareContourBacSimple1Nervure(xPts, yPts, nbPts)
        End If

        '--> Remplissage contour

        'RemplirZone(myGr, myBrushDalle, xPts, yPts, nbPts, MyParAff, False)
        'ContourZone(myGr, New Pen(BlueAM), xPts, yPts, nbPts, MyParAff, True)

        If lUn Then
            RemplirZone(myGr, myBrushBac, xPts, yPts, nbPts, MyParAff, True)
        Else
            ContourZone(myGr, New Pen(BlueAM), xPts, yPts, nbPts, MyParAff, False)
        End If


        If lCotation Then

            CotationBacUn(myGr, MyParAff, MyBac, dCar, iCote)

        End If


    End Sub

    Private Sub CotationBacUn(ByRef myGr As Graphics, MyParAffC As Struc_Affichage, MyBac As Cls_Bac, dCar As Decimal, iSelect As Integer)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Cotation d'une nervure de bac
        '-----------------------------------------------------------------------------------------------
        '   iSelect :   1 : hg
        '               2 : hpg
        '               3 : ep
        '               4 : bt
        '               5 : bb
        '               6 : tp
        '-----------------------------------------------------------------------------------------------

        '--> Déclarations
        Dim MyPen As New Pen(Color.Black, 1)
        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim MyPenNormal As New Pen(ColorNonSelect, 1)
        Dim MyPenSelect As New Pen(ColorSelect, 1)
        Dim MyFontNormal As Font = FontBase
        Dim MyColor As Color
        Dim Chaine As String
        Dim lAffSymbol As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        Dim eP, hP, bbP, btP, tP, hPg As Decimal
        Const kTP As Decimal = 2
        Dim lRaid As Boolean = MyBac.HasRaidisseurSup

        '--> Initialisation

        eP = MyBac.e_p
        hP = MyBac.h_p
        btP = MyBac.b_t
        bbP = MyBac.b_b
        tP = MyBac.tp * kTP
        hPg = MyBac.Hauteur_hpg

        '--> Cotes

        '#  ep

        MyColor = StyleCouleur(iSelect, 3)
        MyPen.Color = MyColor

        xo = -eP / 2
        xe = eP / 2

        yo = -dCar
        ye = -dCar

        AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, True)

        If lAffSymbol Then Chaine = "ep" Else Chaine = GetStringNoUnit(eP, Enu_TypeVariable.Dimension)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, ye, MyParAffC, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# bb

        MyColor = StyleCouleur(iSelect, 5)
        MyPen.Color = MyColor

        xo = -bbP / 2
        xe = -xo

        yo = 0 + dCar / 2
        ye = 0 + dCar / 2

        AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, True)

        If lAffSymbol Then Chaine = "bb" Else Chaine = GetStringNoUnit(bbP, Enu_TypeVariable.Dimension)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, (yo + ye) / 2, MyParAffC, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# bt

        MyColor = StyleCouleur(iSelect, 4)
        MyPen.Color = MyColor

        xo = -btP / 2
        xe = -xo

        yo = hP + dCar / 2
        ye = hP + dCar / 2

        AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, True)

        If lAffSymbol Then Chaine = "bt" Else Chaine = GetStringNoUnit(btP, Enu_TypeVariable.Dimension)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, (yo + ye) / 2, MyParAffC, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)


        '# hp

        MyColor = StyleCouleur(iSelect, 1)
        MyPen.Color = MyColor

        xo = -eP / 2 - dCar
        xe = xo

        yo = 0
        ye = hP

        AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, True)

        If lAffSymbol Then Chaine = "hp" Else Chaine = GetStringNoUnit(hP, Enu_TypeVariable.Dimension)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, (yo + ye) / 2, MyParAffC, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# hpg

        If lRaid Then
            MyColor = StyleCouleur(iSelect, 2)
            MyPen.Color = MyColor

            xo = +eP / 2 + dCar
            xe = xo

            yo = 0
            ye = hPg

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, True)

            If lAffSymbol Then Chaine = "hpg" Else Chaine = GetStringNoUnit(hPg, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, (yo + ye) / 2, MyParAffC, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
        End If

        '# tp

        MyColor = StyleCouleur(iSelect, 6)
        MyPen.Color = MyColor

        xo = eP / 2 - (eP - btP) / 4
        xe = xo

        yo = hP - tP / 2
        ye = yo - dCar / 2

        AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, False)

        yo = hP + tP / 2
        ye = hPg + dCar / 2

        AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAffC, True, False)

        If lAffSymbol Then Chaine = "tp" Else Chaine = GetStringNoUnit(tP, Enu_TypeVariable.Dimension)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, ye, MyParAffC, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

    End Sub

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
        Dim myBrushP As Brush
        Dim myBrushB As Brush
        Dim myBrushE As Brush

        Dim myBrushASup(2) As Brush
        Dim myBrushAMid(2) As Brush
        Dim myBrushAInf(2) As Brush

        If xLeft <> 0 Or yTop <> 0 Then
            ' Profilé
            myBrushP = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurAcier, CouleurAcier)
            ' Béton
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
            ' Etriers
            myBrushE = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalEtriers, ColorLocalEtriers)

            ' Armatures 
            For j = 0 To 2
                'myBrushA(i) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalArma(i), ColorLocalArma(i))
                myBrushASup(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(0, j), pColorLocalArma(0, j))
                myBrushAMid(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(1, j), pColorLocalArma(1, j))
                myBrushAInf(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(2, j), pColorLocalArma(2, j))
            Next
        Else
            ' Profilé
            myBrushP = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcier)
            ' Béton
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
            ' Etriers
            myBrushE = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalEtriers, ColorLocalEtriers)

            ' Armatures 
            For j = 0 To 2
                'myBrushA(i) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalArma(i), ColorLocalArma(i))
                myBrushASup(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(0, j), pColorLocalArma(0, j))
                myBrushAMid(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(1, j), pColorLocalArma(1, j))
                myBrushAInf(j) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), pColorLocalArma(2, j), pColorLocalArma(2, j))
            Next
        End If


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

        Dim myBrushG As Brush

        If xLeft <> 0 Or yTop <> 0 Then
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, Color.DarkGray)
        Else
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, CouleurAcierNormal)
        End If




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
        '   iSelect     101 : entraxe à gauche
        '               102 : entraxe à droite
        '               103 : trémie gauche
        '               104 : trémie droite
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

        'If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Préparation des Pinceaux utilisés dans le dessin

        CouleurAcier = CouleurAcierNormal
        CouleurBeton = CouleurBetonNormal
        CouleurTremie = CouleurTremieNormal
        CouleurArma = CouleurArmaNormal
        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurAcier, CouleurAcier)
        Dim myBrushPSel As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.Red, Color.Red)
        'Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        'Trémie
        Dim myBrushT As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurTremie, CouleurTremie)
        ' Armatures
        Dim myBrushA As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurArma, CouleurArma)

        '--> Affichage

        DessinFrmCoupeStandard(MyGr, MyPoutre, iSelect, dCar, hMaxProfile, MyParAff, myBrushP, myBrushPSel, myBrushB, myBrushT, myBrushA)

    End Sub

    Private Sub DessinFrmCoupeStandard(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                       iSelect As Integer, dCar As Decimal, hMaxProfile As Decimal,
                                       MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushPSel As Brush, myBrushB As Brush, myBrushT As Brush, myBrushA As Brush)
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
        '   iSelect     101 : entraxe à gauche
        '               102 : entraxe à droite
        '               103 : trémie gauche
        '               104 : trémie droite
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

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushPSel, MyParaff1, ZREF)


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
            xo = -1.5 * MyPoutre.EntraxeD1
            xe = 1.5 * MyPoutre.EntraxeD2
        Else
            xo = -MyPoutre.EntraxeD1
            xe = 1.5 * MyPoutre.EntraxeD2
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
        Dim yCoteS As Decimal = MyPoutre.Dalle.zTop + dCar
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

        If lAffSymbol Then Chaine = "D1" Else Chaine = GetStringInUnit(MyPoutre.EntraxeD1, Enu_TypeVariable.Longueur, 4, 2, False)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '--> Entraxe à droite

        MyColor = StyleCouleur(iSelect, 102)
        MyPen.Color = MyColor

        xo = 0
        xe = MyPoutre.EntraxeD2

        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParaff1, True, True)

        If lAffSymbol Then Chaine = "D2" Else Chaine = GetStringInUnit(MyPoutre.EntraxeD2, Enu_TypeVariable.Longueur, 4, 2, False)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '--> Trémie Gauche

        If MyPoutre.lTremieGauche Then
            MyColor = StyleCouleur(iSelect, 103)
            MyPen.Color = MyColor

            xo = 0
            xe = -MyPoutre.DistanceDsl1

            AddFleche(MyGr, MyPen, xo, yCoteS, xe, yCoteS, MyParaff1, True, True)

            If lAffSymbol Then Chaine = "Dsl1" Else Chaine = GetStringInUnit(MyPoutre.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCoteS, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

        '--> Trémie Droite

        If MyPoutre.lTremieDroite Then
            MyColor = StyleCouleur(iSelect, 104)
            MyPen.Color = MyColor

            xo = 0
            xe = MyPoutre.DistanceDsl2

            AddFleche(MyGr, MyPen, xo, yCoteS, xe, yCoteS, MyParaff1, True, True)

            If lAffSymbol Then Chaine = "Dsl2" Else Chaine = GetStringInUnit(MyPoutre.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCoteS, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If




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
        Dim myBrushB As Brush
        If xLeft <> 0 Or yTop <> 0 Then
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.Gray, Color.Gray)
        Else
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, Color.Gray)
        End If
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

                If lAffSymbol Then Chaine = "Lg" Else Chaine = GetStringInUnit(MyPoutre.LongueurTravee(0), Enu_TypeVariable.Longueur, 4, 2, False)
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

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnit(MyPoutre.LongueurTravee(i), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            Next

            ' Travée console droite

            If MyPoutre.lTraveeConsoleDroite Then

                MyColor = StyleCouleur(iSelect, 99)
                MyPen.Color = MyColor

                xo = xe
                xe += MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Ld" Else Chaine = GetStringInUnit(MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True

            End If

            ' Longueur totale si plusieurs travées

            If lTotal Then

                xo = 0
                xe = LongueurPoutre

                AddFleche(MyGr, MyPen, xo, yCote - dCar, xe, yCote - dCar, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnit(LongueurPoutre, Enu_TypeVariable.Longueur, 4, 2, False)
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

#Region "Dessins pour la connection (FRM_CONNECTION)"

    Public Sub DessinFrmConnection_Connecteurs(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, kAdjust As Double, MyPoutreLoc As cls_Poutre,
                          ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '   MyPoutreLoc [E] :   Poutre locale        '   
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage

        Dim ColorPen As Color = Color.Blue
        Dim ColorRedPen As Color = Color.Red

        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurAcier As Color = CouleurAcierNormal
        Dim CouleurConnect As Color = CouleurConnecteurNormal

        Dim myBrushBac As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.LightGray, Color.DarkGray)
        Dim myBrushBeton As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.Gray, CouleurBeton)
        Dim myBrushProfilA As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), CouleurAcier, CouleurAcier)
        Dim myBrushConnecteur As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), CouleurConnect, CouleurConnect)

        Dim MyPenBrush As New SolidBrush(ColorPen)
        Dim MyPenRedBrush As New SolidBrush(ColorRedPen)
        Dim MyPen As New Pen(ColorPen)
        Dim MyPenRed As New Pen(ColorRedPen)
        Dim MyFontNormal As Font = FontBase

        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Decimal

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        '--> Initialisation
        dCar = MyPoutreLoc.Dalle.t_d

        '--> Dessin des éléments

        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele Then

            '--> Dessin du bac acier

            With MyPoutreLoc.Dalle.Bac

                '--> Preparation de la zone d'affichage - Calcul de ParAff
                xMin = - .e_p / 2
                xMax = .e_p / 2

                yMin = -MyPoutreLoc.Section.ProfilA.t_fs
                yMax = Math.Max(MyPoutreLoc.Dalle.t_d, MyPoutreLoc.Dalle.Connecteur.hsc)

                ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

                '--> Calcul des points du pourtour du bac
                .PrepareContourBacSimple1Nervure(xPts, yPts, nbPts)

                '--> Remplissage contour
                RemplirZone(myGr, myBrushBac, xPts, yPts, nbPts, MyParAff, True)
            End With

            '--> Dessin de la dalle béton

            '--> Calcul des points du pourtour de la dalle

            Dim xPts_Dalle(xPts.Length / 2 + 1) As Single
            Dim yPts_Dalle(xPts.Length / 2 + 1) As Single

            For i As Integer = 0 To xPts.Length / 2 - 1
                xPts_Dalle(i) = xPts(i)
                yPts_Dalle(i) = yPts(i)
            Next
            xPts_Dalle(xPts.Length / 2) = xPts(xPts.Length / 2 - 1)
            xPts_Dalle(xPts.Length / 2 + 1) = xPts(0)

            yPts_Dalle(xPts.Length / 2) = MyPoutreLoc.Dalle.t_d
            yPts_Dalle(xPts.Length / 2 + 1) = MyPoutreLoc.Dalle.t_d

            nbPts = xPts_Dalle.Length

            '--> Remplissage contour
            RemplirZone(myGr, myBrushBeton, xPts_Dalle, yPts_Dalle, nbPts, MyParAff, True)

            '--> Dessin du goujon

            Dim xGoujon As Decimal = 0
            Dim yGoujon As Decimal = yPts(yPts.Length / 4)

            'Dessin du corps du goujon
            AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - MyPoutreLoc.Dalle.Connecteur.d / 2, yGoujon, xGoujon + MyPoutreLoc.Dalle.Connecteur.d / 2, yGoujon + MyPoutreLoc.Dalle.Connecteur.hsc, MyParAff, True, True)
            'Dessin de la tete du goujon
            Dim dTete, hTete As Decimal
            MyPoutreLoc.Dalle.Connecteur.DimensionsTete(dTete, hTete)
            AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - dTete / 2, yGoujon + MyPoutreLoc.Dalle.Connecteur.hsc - hTete, xGoujon + dTete, yGoujon + MyPoutreLoc.Dalle.Connecteur.hsc, MyParAff, True, True)

            'Dessin de la semelle supérieure et de l'âme de la poutre
            Dim xSemelleSup As Decimal = 0
            Dim ySemelleSup As Decimal = yPts(3 * yPts.Length / 4)
            AddRectanglePlein(myGr, myBrushProfilA, MyPenContour, xSemelleSup - MyPoutreLoc.Section.ProfilA.b_fs / 2 / 2, ySemelleSup - MyPoutreLoc.Section.ProfilA.t_fs, xSemelleSup + MyPoutreLoc.Section.ProfilA.b_fs / 2 / 2, ySemelleSup, MyParAff, True, True)

            Dim xAme As Decimal = xSemelleSup
            Dim yAme As Decimal = ySemelleSup - MyPoutreLoc.Section.ProfilA.t_fs
            AddRectanglePlein(myGr, myBrushProfilA, MyPenContour, xAme - MyPoutreLoc.Section.ProfilA.t_w / 2, yAme - MyPoutreLoc.Section.ProfilA.HauteurAmeHw, xAme + MyPoutreLoc.Section.ProfilA.t_w / 2, yAme, MyParAff, True, True)

        Else

            With MyPoutreLoc.Section.ProfilA

                '--> Preparation de la zone d'affichage - Calcul de ParAff
                xMin = - .b_fs
                xMax = .b_fs

                yMin = -MyPoutreLoc.Section.ProfilA.t_fs
                yMax = Math.Max(MyPoutreLoc.Dalle.t_d, MyPoutreLoc.Dalle.Connecteur.hsc)

                ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

            End With
            '--> Dessin de la dalle béton

            Dim xBeton As Decimal = 0
            Dim yBeton As Decimal = 0

            AddRectanglePlein(myGr, myBrushBeton, MyPenContour, xBeton - MyPoutreLoc.Section.ProfilA.b_fs, yBeton, xBeton + MyPoutreLoc.Section.ProfilA.b_fs, yBeton + MyPoutreLoc.Dalle.t_d, MyParAff, True, True)

            '--> Dessin du goujon

            Dim xGoujon As Decimal = 0
            Dim yGoujon As Decimal = 0

            'Dessin du corps du goujon
            AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - MyPoutreLoc.Dalle.Connecteur.d / 2, yGoujon, xGoujon + MyPoutreLoc.Dalle.Connecteur.d / 2, yGoujon + MyPoutreLoc.Dalle.Connecteur.hsc, MyParAff, True, True)
            'Dessin de la tete du goujon
            Dim dTete, hTete As Decimal
            MyPoutreLoc.Dalle.Connecteur.DimensionsTete(dTete, hTete)

            AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - dTete / 2, yGoujon + MyPoutreLoc.Dalle.Connecteur.hsc - hTete, xGoujon + dTete / 2, yGoujon + MyPoutreLoc.Dalle.Connecteur.hsc, MyParAff, True, True)

            '--> Dessin de la semelle supérieure et de l'âme de la poutre

            Dim xSemelleSup As Decimal = 0
            Dim ySemelleSup As Decimal = 0
            AddRectanglePlein(myGr, myBrushProfilA, MyPenContour, xSemelleSup - MyPoutreLoc.Section.ProfilA.b_fs / 2 / 2, ySemelleSup - MyPoutreLoc.Section.ProfilA.t_fs, xSemelleSup + MyPoutreLoc.Section.ProfilA.b_fs / 2 / 2, ySemelleSup, MyParAff, True, True)

            Dim xAme As Decimal = xSemelleSup
            Dim yAme As Decimal = ySemelleSup - MyPoutreLoc.Section.ProfilA.t_fs
            AddRectanglePlein(myGr, myBrushProfilA, MyPenContour, xAme - MyPoutreLoc.Section.ProfilA.t_w / 2, yAme - MyPoutreLoc.Section.ProfilA.HauteurAmeHw, xAme + MyPoutreLoc.Section.ProfilA.t_w / 2, yAme, MyParAff, True, True)

        End If

    End Sub

    Public Sub DessinFrmConnection_Connection(MyGr As Graphics, MyPoutre As cls_Poutre,
                                ByVal pWi As Decimal, ByVal pHi As Decimal,
                                kAdjust As Double, indTravee As Integer, ByVal lCote As Boolean, strStuds As String,
                                ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/07/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des travées dans la fenêtre portées
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyPoutre    [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   indTravee   [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '   strStuds       [E] :   Indique la traduction associée au mot "goujons"
        '------------------------------------------------------------------------------------------------------------------


        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim MyParAff As Struc_Affichage
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        'Dim LongueurTotalePoutre As Decimal
        Dim LongueurTravee As Decimal
        Dim LargeurSemelle As Decimal
        Dim NombreGoujonsTrans(2) As Integer
        Dim LongueurZones(2) As Decimal
        Dim NombreGoujonsLongiZone(2) As Integer
        Dim NombreZones As Integer
        Dim DiametreGoujons As Decimal
        Dim EspaceLongiGoujons As Decimal
        Dim MyBrushA As New SolidBrush(Color.LightBlue)
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Dim CouleurConnecteur As Color = Color.White
        Dim myBrushC As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurConnecteur)
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Initialisations

        LongueurTravee = MyPoutre.PORTEEDEFAUT
        LargeurSemelle = LongueurTravee / 8.5
        If Not MyPoutre.lAutomaticDesign Then
            For i As Integer = 0 To 2
                NombreGoujonsTrans(i) = MyPoutre.NombreGoujonsTransv(indTravee, i)
                LongueurZones(i) = MyPoutre.Longueur_Zone(indTravee, i) / MyPoutre.LongueurTravee(indTravee) * LongueurTravee
                NombreGoujonsLongiZone(i) = 0.75 * MyPoutre.Longueur_Zone(indTravee, i) / MyPoutre.Espacement(indTravee, i)
                NombreZones = MyPoutre.NombreZone(indTravee)
            Next
        Else
            NombreGoujonsTrans(0) = 1
            LongueurZones(0) = LongueurTravee
            NombreGoujonsLongiZone(0) = 0.75 * MyPoutre.Longueur_Zone(indTravee, 0) / 0.2
            NombreZones = 1
        End If


        DiametreGoujons = LargeurSemelle / 15

        dCar = Math.Sqrt(LongueurTravee ^ 2 + LargeurSemelle ^ 2) / 20
        'dCarApp = HauteurPoutre / 2

        '--> Initialisation des paramètres d'affichage

        xMin = 0 '- dCar / 4
        xMax = LongueurTravee '+ dCar / 2
        yMin = -LargeurSemelle / 2 - dCar / 2 '- dCarApp
        yMax = LargeurSemelle / 2 + dCar / 2

        If MyPoutre.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la semelle de la poutre 

        xo = 0 - dCar / 2
        xe = LongueurTravee + dCar / 2


        yo = -LargeurSemelle / 2
        ye = LargeurSemelle / 2


        AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        'Représentation des goujons sur la semelle supérieure


        For i As Integer = 0 To NombreZones - 1
            xo = 0
            yo = 0
            For j As Integer = 0 To i - 1
                xo += LongueurZones(j)
            Next
            EspaceLongiGoujons = LongueurZones(i) / (NombreGoujonsLongiZone(i))
            For j As Integer = 1 To NombreGoujonsLongiZone(i)
                xo += EspaceLongiGoujons
                yo = 0
                For k As Integer = 1 To NombreGoujonsTrans(i)
                    yo = -LargeurSemelle / 2 + k * LargeurSemelle / (NombreGoujonsTrans(i) + 1)
                    AddCerclePlein(MyGr, myBrushC, xo, yo, DiametreGoujons, MyParAff, True)
                Next
            Next
        Next


        '=== COTES =======================================================

        If lCote Then

            xo = 0
            xe = 0

            For i As Integer = 0 To NombreZones - 1

                xo = xe
                xe += LongueurZones(i)

                'On dessinne la côte inférieure qui donne la longueur de la zone étudiée 
                Dim yCote As Decimal = -LargeurSemelle / 2 - dCar

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                'If lAffSymbol Then Chaine = "L" Else Chaine = GetStringNoUnit(MyPoutre.Longueur_Zone(indTravee, i), Enu_TypeVariable.Longueur)
                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnit(MyPoutre.Longueur_Zone(indTravee, i), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(Color.Black), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                'On dessinne la côte supérieure qui donne le nombre de goujons disposés sur la zone étudiée 
                yCote = LargeurSemelle / 2 + dCar

                AddFleche(MyGr, New Pen(Color.Red), xo, yCote, xe, yCote, MyParAff, True, True)
                Chaine = GetStringNoUnit(Math.Floor(MyPoutre.Longueur_Zone(indTravee, i) / MyPoutre.Espacement(indTravee, i)), Enu_TypeVariable.SansType) & " " & strStuds
                AddTexteFond(MyGr, New SolidBrush(Color.Red), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            Next


        End If

    End Sub

#End Region

#Region "Dessin pour les maintiens (FRM_MAINTIENS)"

    Public Sub DessinFrmMaintiens(MyGr As Graphics, MyPoutre As cls_Poutre,
                                ByVal pWi As Decimal, ByVal pHi As Decimal,
                                kAdjust As Double, iSelect As Integer, lCote As Boolean, ByRef positionCotesInferieuresDessin(,) As Decimal, ByRef positionMaintiensDessin(,) As Decimal, ByRef EpaisseurSemelleDessin As Decimal,
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
        '   xSouris     [E] :   Abscisse de la souris dans l'image
        '   ySouris     [E] :   Ordonnée de la souris dans l'image
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
        Dim MyBrushPoigneeNonSelectionnee As New SolidBrush(Color.Yellow)
        Dim MyBrushPoigneeSelectionnee As New SolidBrush(Color.Orange)
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
        dCarApp = HauteurPoutre / 4

        '--> Initialisation des paramètres d'affichage

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

        EpaisseurSemelleDessin = Math.Abs(YEcran(MyParAff, EpaisseurSemelle) - YEcran(MyParAff, 0)) 'Pas utile pour la suite du module, c'est uniquement pour enregistrer cette donnée pour être réutilisée dans le Frm_Maintien

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

            'If i = 1 Then
            '    If MyPoutre.TypeMaintien(i) = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
            '        xo += EpaisseurSemelle / 2
            '    End If

            '    If MyPoutre.lTraveeConsoleGauche Then
            '        If MyPoutre.TypeMaintien(i - 1) = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
            '            xo -= EpaisseurSemelle / 2
            '        End If
            '    End If

            'ElseIf i = MyPoutre.IndiceTraveeConsoleDroite Then
            '    If MyPoutre.TypeMaintien(i - 1) = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
            '        xo -= EpaisseurSemelle / 2
            '    End If

            '    If MyPoutre.lTraveeConsoleDroite Then
            '        If MyPoutre.TypeMaintien(i) = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
            '            xo += EpaisseurSemelle / 2
            '        End If
            '    End If
            'End If

            If i = 1 Then
                If MyPoutre.TypeMaintien = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                    xo += EpaisseurSemelle / 2
                End If

                If MyPoutre.lTraveeConsoleGauche Then
                    If MyPoutre.TypeMaintien = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                        xo -= EpaisseurSemelle / 2
                    End If
                End If

            ElseIf i = MyPoutre.IndiceTraveeConsoleDroite Then
                If MyPoutre.TypeMaintien = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                    xo -= EpaisseurSemelle / 2
                End If

                If MyPoutre.lTraveeConsoleDroite Then
                    If MyPoutre.TypeMaintien = MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                        xo += EpaisseurSemelle / 2
                    End If
                End If
            End If


            yo = EpaisseurSemelle / 2
            MyBrushMaintienSup = MyBrushSemelleBloquee
            AddCerclePlein(MyGr, MyBrushMaintienInf, xo, yo, EpaisseurSemelle, MyParAff, True)

            yo = HauteurPoutre - EpaisseurSemelle / 2
            MyBrushMaintienInf = MyBrushSemelleBloquee
            AddCerclePlein(MyGr, MyBrushMaintienSup, xo, yo, EpaisseurSemelle, MyParAff, True)


        Next

        '--> Représentation des maintiens latéraux 

        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee

            Select Case MyPoutre.TypeMaintien
                Case MyPoutre.EnuTypeMaintiensPoutre.NotRestrained
                    lCote = False

                Case MyPoutre.EnuTypeMaintiensPoutre.FullyRestrained

                    lCote = False

                    MyBrushMaintienSup = MyBrushSemelleBloquee
                    MyBrushMaintienInf = MyBrushSemelleBloquee

                    xo = 0
                    Select Case i
                        Case 0
                            xe = LongueurConsoleGauche

                        Case MyPoutre.IndiceTraveeConsoleDroite
                            xe = LongueurConsoleGauche
                            For j As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1
                                xe += LongueurTravee
                            Next

                            xo = xe
                            xe += LongueurConsoleDroite

                        Case Else
                            xe = LongueurConsoleGauche
                            For j As Integer = 1 To i - 1
                                xe += LongueurTravee
                            Next

                            xo = xe
                            xe += LongueurTravee

                    End Select

                    yo = HauteurPoutre - EpaisseurSemelle
                    ye = HauteurPoutre

                    AddRectanglePlein(MyGr, MyBrushMaintienSup, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

                    yo = 0
                    ye = EpaisseurSemelle

                    AddRectanglePlein(MyGr, MyBrushMaintienSup, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)


                Case MyPoutre.EnuTypeMaintiensPoutre.PointRestrained


                    For Each maintiens As cls_Maintiens In MyPoutre.Maintiens(i)

                        If maintiens.lMaintienSemelleSup Then
                            MyBrushMaintienSup = MyBrushSemelleBloquee
                        Else
                            MyBrushMaintienSup = MyBrushSemelleNonBloquee
                        End If

                        If maintiens.lMaintienSemelleInf Then
                            MyBrushMaintienInf = MyBrushSemelleBloquee
                        Else
                            MyBrushMaintienInf = MyBrushSemelleNonBloquee
                        End If

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
                                xo += maintiens.x_Loc / MyPoutre.LongueurTravee(i) * LongueurTravee

                        End Select

                        yo = EpaisseurSemelle / 2
                        AddCerclePlein(MyGr, MyBrushMaintienInf, xo, yo, EpaisseurSemelle, MyParAff, True)

                        yo = HauteurPoutre - EpaisseurSemelle / 2
                        AddCerclePlein(MyGr, MyBrushMaintienSup, xo, yo, EpaisseurSemelle, MyParAff, True)

                        '--> Dessin ligne verticale

                        AddLigne(MyGr, xo, EpaisseurSemelle, xo, HauteurPoutre - EpaisseurSemelle, MyParAff)

                        '--> Dessin poignée

                        yo = HauteurPoutre / 2 - EpaisseurSemelle / 2
                        ye = yo + EpaisseurSemelle

                        xo -= EpaisseurSemelle / 2
                        xe = xo + EpaisseurSemelle

                        If maintiens.lMaintienSelectionne Then
                            AddRectanglePlein(MyGr, MyBrushPoigneeSelectionnee, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
                        Else
                            AddRectanglePlein(MyGr, MyBrushPoigneeNonSelectionnee, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
                        End If

                    Next

            End Select
        Next

        '=== COTES =======================================================

        If lCote Then

            positionCotesInferieuresDessin = Nothing

            If iSelect = 99 Then
                ReDim positionCotesInferieuresDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).Count, 1)
                ReDim positionMaintiensDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).Count - 1, 2)

            Else
                ReDim positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).Count, 1)
                ReDim positionMaintiensDessin(MyPoutre.Maintiens(iSelect).Count - 1, 2)
            End If


            Dim yCote As Decimal = -dCar - dCarApp

            MyPen.Color = Color.Black
            MyColor = Color.Black

            Select Case iSelect
                Case 0

                    For Each maintiens In MyPoutre.Maintiens(iSelect)

                        Dim indice_Maintien_en_cours As Integer = MyPoutre.Maintiens(iSelect).IndexOf(maintiens)

                        If indice_Maintien_en_cours = 0 Then
                            xo = 0
                            xe = MyPoutre.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurConsoleGauche

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleGauche * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        Else
                            xo = MyPoutre.Maintiens(iSelect)(indice_Maintien_en_cours - 1).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurConsoleGauche
                            xe = MyPoutre.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurConsoleGauche

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleGauche * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        End If

                        positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                        positionMaintiensDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, xe)
                        positionMaintiensDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, 0)
                        positionMaintiensDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 2) = Mod_OutilsGraph.YEcran(MyParAff, HauteurPoutre)

                    Next

                    If MyPoutre.Maintiens(iSelect).Count <> 0 Then
                        xo = MyPoutre.Maintiens(iSelect)(MyPoutre.Maintiens(iSelect).Count - 1).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurConsoleGauche
                        xe = LongueurConsoleGauche
                    End If

                    AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                    Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleGauche * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                    positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).Count, 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                    positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).Count, 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                Case 99

                    For Each maintiens In MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite)

                        Dim indice_Maintien_en_cours As Integer = MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).IndexOf(maintiens)

                        xo = LongueurConsoleGauche
                        For i As Integer = 1 To MyPoutre.IndiceDerniereTravee - 1
                            xo += LongueurTravee
                        Next

                        If indice_Maintien_en_cours = 0 Then
                            xe = xo + MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite)(indice_Maintien_en_cours).x_Loc / MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite) * LongueurConsoleDroite

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleDroite * MyPoutre.LongueurTravee(MyPoutre.IndiceDerniereTravee), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        Else
                            xe = xo + MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite)(indice_Maintien_en_cours).x_Loc / MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite) * LongueurConsoleDroite
                            xo += MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite)(indice_Maintien_en_cours - 1).x_Loc / MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite) * LongueurConsoleDroite

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleDroite * MyPoutre.LongueurTravee(MyPoutre.IndiceDerniereTravee), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        End If

                        positionCotesInferieuresDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                        positionMaintiensDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, xe)
                        positionMaintiensDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, 0)
                        positionMaintiensDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).IndexOf(maintiens), 2) = Mod_OutilsGraph.YEcran(MyParAff, HauteurPoutre)

                    Next

                    If MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).Count <> 0 Then
                        xo = LongueurConsoleGauche
                        For i As Integer = 1 To MyPoutre.IndiceDerniereTravee - 1
                            xo += LongueurTravee
                        Next
                        xe = xo
                        xo += MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite)(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).Count - 1).x_Loc / MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite) * LongueurConsoleDroite
                        xe += LongueurConsoleDroite

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                        Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleDroite * MyPoutre.LongueurTravee(MyPoutre.IndiceDerniereTravee), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        positionCotesInferieuresDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).Count, 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(MyPoutre.Maintiens(MyPoutre.IndiceTraveeConsoleDroite).Count, 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)



                    End If

                Case Else
                    ' Travées principales

                    For Each maintiens In MyPoutre.Maintiens(iSelect)

                        Dim indice_Maintien_en_cours As Integer = MyPoutre.Maintiens(iSelect).IndexOf(maintiens)

                        If indice_Maintien_en_cours = 0 Then
                            xo = LongueurConsoleGauche
                            xe = LongueurConsoleGauche + MyPoutre.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurTravee

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                        Else
                            xo = LongueurConsoleGauche + MyPoutre.Maintiens(iSelect)(indice_Maintien_en_cours - 1).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurTravee
                            xe = LongueurConsoleGauche + MyPoutre.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurTravee

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                        End If

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                        Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                        positionMaintiensDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, xe)
                        positionMaintiensDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, 0)
                        positionMaintiensDessin(MyPoutre.Maintiens(iSelect).IndexOf(maintiens), 2) = Mod_OutilsGraph.YEcran(MyParAff, HauteurPoutre)

                    Next

                    If MyPoutre.Maintiens(iSelect).Count <> 0 Then
                        xo = LongueurConsoleGauche + MyPoutre.Maintiens(iSelect)(MyPoutre.Maintiens(iSelect).Count - 1).x_Loc / MyPoutre.LongueurTravee(iSelect) * LongueurTravee
                        xe = LongueurConsoleGauche + LongueurTravee

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                        Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * MyPoutre.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).Count, 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(MyPoutre.Maintiens(iSelect).Count, 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                    End If

            End Select

        End If

    End Sub

    Public Sub GestionClickDownMousse(MyPoutre As cls_Poutre,
                                ByVal pWi As Decimal, ByVal pHi As Decimal,
                                kAdjust As Double, iSelect As Integer, indiceTravee As Integer, Optional xSouris As Decimal = 0, Optional ySouris As Decimal = 0, Optional lMouseOnPoigneeMaintien As Boolean = False,
                                ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)

        '------------------------------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Gere le click down de la souris dans le dessin
        '------------------------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xSouris     [E] :   Abscisse de la souris dans l'image
        '   ySouris     [E] :   Ordonnée de la souris dans l'image
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   indiceTravee[E] :   Indique l'indice de la travée selectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '------------------------------------------------------------------------------------------------------------------
        '   iSelect     0  : console gauche
        '               i  : travée sur 2 appui no i
        '               99 : console droite
        '------------------------------------------------------------------------------------------------------------------

        '-->Déclaration

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar, dCarApp As Decimal
        Dim MyParAff As Struc_Affichage
        Dim LongueurPoutre, LongueurTravee, LongueurConsoleGauche, LongueurConsoleDroite, HauteurPoutre, EpaisseurSemelle, RayonConge As Decimal

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


        'Distance de la souris, par rapport à l'appui gauche, dans l'univers de la poutre 
        Dim xSourisUnivers As Decimal = Mod_OutilsGraph.XUnivers(MyParAff, xSouris) - xMin
        Dim ySourisUnivers As Decimal = Mod_OutilsGraph.YUnivers(MyParAff, ySouris)


        'Permet de modifier si une semelle est maintenue ou non; ou de sélectionner un maintien pour le déplacer
        'La modification opère si on clique dans la zone du maintien dessiné

        Dim xMaintienUnivers As Decimal
        Dim yCote As Decimal = -dCar - dCarApp
        Dim xo As Decimal = 0
        Dim xe As Decimal = 0

        For Each maintiens As cls_Maintiens In MyPoutre.Maintiens(indiceTravee)

            Select Case iSelect
                Case 0
                    xMaintienUnivers = maintiens.x_Loc / MyPoutre.LongueurTravee(indiceTravee) * LongueurConsoleGauche
                Case 99
                    xMaintienUnivers = maintiens.x_Loc / MyPoutre.LongueurTravee(indiceTravee) * LongueurConsoleDroite
                Case Else
                    xMaintienUnivers = maintiens.x_Loc / MyPoutre.LongueurTravee(indiceTravee) * LongueurTravee

            End Select

            If Math.Abs(xSourisUnivers - xMaintienUnivers) <= HauteurPoutre / 2 Then
                If Math.Abs(ySourisUnivers - EpaisseurSemelle / 2) <= HauteurPoutre / 4 Then 'Maintien de la semelle inf selectionné
                    If Not lMouseOnPoigneeMaintien Then
                        maintiens.lMaintienSemelleInf = Not maintiens.lMaintienSemelleInf
                        If maintiens.lMaintienSemelleInf = False Then maintiens.lMaintienSemelleSup = True 'permet d'imposer qu'au moins 1 des 2 maintiens soit bloqué
                    End If
                ElseIf Math.Abs(ySourisUnivers - (HauteurPoutre - EpaisseurSemelle / 2)) <= HauteurPoutre / 4 Then 'Maintien de la semelle sup sélectionné
                    If Not lMouseOnPoigneeMaintien Then
                        maintiens.lMaintienSemelleSup = Not maintiens.lMaintienSemelleSup
                        If maintiens.lMaintienSemelleSup = False Then maintiens.lMaintienSemelleInf = True
                    End If
                ElseIf Math.Abs(ySourisUnivers - HauteurPoutre / 2) <= HauteurPoutre / 2 Then 'Poignée centrale sélectionnée
                    If lMouseOnPoigneeMaintien Then
                        maintiens.lMaintienSelectionne = True 'permet d'imposer qu'au moins 1 des 2 maintiens soit bloqué
                        MyPoutre.pIndiceMaintienSelectionne = MyPoutre.Maintiens(indiceTravee).IndexOf(maintiens)
                    End If
                End If

            End If

        Next

    End Sub

    Public Sub DeplacementMaintienSemelle(MyPoutre As cls_Poutre,
                                ByVal pWi As Decimal, ByVal pHi As Decimal,
                                kAdjust As Double, iSelect As Integer, indiceTravee As Integer, Optional xSouris As Decimal = 0,
                                ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)

        '------------------------------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des travées dans la fenêtre maintiens latéraux
        '------------------------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xSouris     [E] :   Abscisse de la souris dans l'image
        '   ySouris     [E] :   Ordonnée de la souris dans l'image
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   indiceTravee[E] :   Indice de la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '------------------------------------------------------------------------------------------------------------------
        '   iSelect     0  : console gauche
        '               i  : travée sur 2 appui no i
        '               99 : console droite
        '------------------------------------------------------------------------------------------------------------------

        If MyPoutre.Maintiens(indiceTravee)(MyPoutre.pIndiceMaintienSelectionne).lMaintienSelectionne Then

            '-->Déclaration

            Dim xMin, xMax As Decimal
            Dim yMin, yMax As Decimal
            Dim dCar, dCarApp As Decimal
            Dim MyParAff As Struc_Affichage
            Dim LongueurPoutre, LongueurTravee, LongueurConsoleGauche, LongueurConsoleDroite, HauteurPoutre, EpaisseurSemelle, RayonConge As Decimal

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


            'Distance de la souris, par rapport à l'appui gauche, dans l'univers de la poutre 
            Dim xSourisUnivers As Decimal = Mod_OutilsGraph.XUnivers(MyParAff, xSouris) - xMin


            'Permet de modifier si une semelle est maintenue ou non; ou de sélectionner un maintien pour le déplacer
            'La modification opère si on clique dans la zone du maintien dessiné

            Dim x_Loc_min As Decimal
            Dim x_Loc_max As Decimal
            Dim x_Loc_local As Decimal

            Select Case iSelect
                Case 0
                    x_Loc_local = xSourisUnivers * MyPoutre.LongueurTravee(indiceTravee) / LongueurConsoleGauche
                Case 99
                    x_Loc_local = xSourisUnivers * MyPoutre.LongueurTravee(indiceTravee) / LongueurConsoleDroite
                Case Else
                    x_Loc_local = xSourisUnivers * MyPoutre.LongueurTravee(indiceTravee) / LongueurTravee
            End Select

            If MyPoutre.pIndiceMaintienSelectionne = 0 Then
                x_Loc_min = 0 + EpaisseurSemelle
            Else
                x_Loc_min = MyPoutre.Maintiens(indiceTravee)(MyPoutre.pIndiceMaintienSelectionne - 1).x_Loc + EpaisseurSemelle
            End If

            If MyPoutre.pIndiceMaintienSelectionne = MyPoutre.Maintiens(indiceTravee).Count - 1 Then
                x_Loc_max = MyPoutre.LongueurTravee(indiceTravee) - EpaisseurSemelle
            Else
                x_Loc_max = MyPoutre.Maintiens(indiceTravee)(MyPoutre.pIndiceMaintienSelectionne + 1).x_Loc - EpaisseurSemelle
            End If

            x_Loc_local = Math.Max(x_Loc_local, x_Loc_min)
            x_Loc_local = Math.Min(x_Loc_local, x_Loc_max)

            MyPoutre.Maintiens(indiceTravee)(MyPoutre.pIndiceMaintienSelectionne).x_Loc = x_Loc_local

        End If

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

        If lPointProp And MyPoutre.NbPropping <> 0 Then

            For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1
                For j As Integer = 1 To MyPoutre.NbPropping
                    xo = MyPoutre.xPositionAppui(True, i) + j * MyPoutre.LongueurTravee(i) / (MyPoutre.NbPropping + 1)
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
                    For j As Integer = 1 To MyPoutre.NbPropping + 1

                        xo = xe
                        xe += MyPoutre.LongueurTravee(i) / (MyPoutre.NbPropping + 1)

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                        If lAffSymbol Then Chaine = "Lpp" Else Chaine = GetStringNoUnit(MyPoutre.LongueurTravee(i) / (MyPoutre.NbPropping + 1), Enu_TypeVariable.Longueur)
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

    Private Sub DessineDalleMixteParallele(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                           MyBrushDP As Brush, BeffRed As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   29/06/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervure parallèle
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   BeffRed     [E] :   Largeur de dalle réduite pour le dessin 
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts(), yPts() As Single
        Dim nbPts As Integer

        '--> Contour

        PrepareContourDalleMixteParallel(MyDalle, Bfs, BeffRed / 2, BeffRed / 2, xPts, yPts, nbPts)

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, True, True)

    End Sub

    Private Sub PrepareContourDalleMixteParallelOLD(ByVal MyDalle As Cls_Dalle, Bfs As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle mixte/ nervures parallèles
        '---------------------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, yo As Single
        Dim eP, hP, bt, bb, hPg As Decimal
        Dim bEff As Decimal
        Dim lCont As Boolean = True
        Dim xPos As Decimal
        Dim DeltaB As Decimal
        Dim xStart As Decimal
        Dim lRaid As Boolean

        '--> Initialisaiton

        nbPts = 0
        eP = MyDalle.Bac.e_p
        hP = MyDalle.Bac.h_p
        hPg = MyDalle.Bac.Hauteur_hpg
        bb = MyDalle.Bac.b_b
        bt = MyDalle.Bac.b_t
        bEff = MyDalle.Beff
        DeltaB = (bt - bb) / 2

        lRaid = (((hPg - hP) / hP) > 0.05)

        If (MyDalle.Bac.AppuiL = Cls_Bac.EnuConfigLAppui.BacCoupe) Then
            xStart = Bfs / 2
        Else
            xStart = bb / 2
        End If

        '--> on commence le contour par le côté droit inférieur

        '# point de départ 

        xo = xStart
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xPos = xo
        lCont = (xPos + eP < bEff / 2)

        '# boucle sur toutes les nervures complètes

        Do While lCont

            xo += DeltaB
            yo = hP

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            If lRaid Then

            Else
                xo += eP - bt

                AjoutePoint(xo, yo, xPts, yPts, nbPts)
            End If

            xo += DeltaB
            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = xPos + eP

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xPos += eP

            lCont = (xPos + eP < bEff / 2)
        Loop

        '# recherche de l'intersection dans la dernière nervure à droite

        '--> Recherche de l'intersection

        If (xPos + DeltaB >= bEff / 2) Then
            'Intersection au droit de la partie remontante

            xo = bEff / 2
            yo = hP * (bEff / 2 - xPos) / DeltaB

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

        Else
            lCont = True
            xo += DeltaB
            yo = hP

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xPos = xo
        End If
        If lCont Then
            If (xPos + eP - bt >= bEff / 2) Then
                'Intersection au droit de la partie sup
                lCont = False

                xo = bEff / 2
                yo = hP

                AjoutePoint(xo, yo, xPts, yPts, nbPts)

            Else
                lCont = True
                xo += eP - bt
                yo = hP

                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                xPos = xo
            End If

            If lCont Then

                If (xPos + DeltaB >= bEff / 2) Then
                    'Intersection au droit de la partie descendante
                    lCont = False

                    xo = bEff / 2
                    yo = hP - hP * (bEff / 2 - xPos) / DeltaB

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                Else

                    lCont = True
                    xo += DeltaB
                    yo = 0

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xPos = xo

                End If

                If lCont Then
                    'Il ne reste plus que l'intersection dans la partie inférieure restante

                    xo = bEff / 2
                    yo = 0

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                End If

            End If

        End If

        '--> Partie sup de la dalle

        xo = bEff / 2
        yo = MyDalle.t_d

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Par symétrie, partie gauche de la dalle

        Dim n0 As Integer = nbPts

        For i = n0 - 1 To 0 Step -1

            AjoutePoint(-xPts(i), yPts(i), xPts, yPts, nbPts)

        Next

    End Sub

    Private Sub PrepareContourDalleMixteParallel(ByVal MyDalle As Cls_Dalle, Bfs As Decimal, BeffG As Decimal, BeffD As Decimal,
                                                 ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle mixte/ nervures parallèles
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle     [E] :   Classe dalle
        '   Bfs         [E] :   Largeur de la semelle sup
        '   BeffG       [E] :   Largeur de la dalle représentée à l'écran sur le côté gauche
        '   BeffD       [E] :   Largeur de la dalle représentée à l'écran sur le côté droite
        '   xPts, yPts  [S] :   Coordonnées de points définissant le contour
        '   nbPts       [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPtsG(), yPtsG() As Single
        Dim nbPtsG As Integer
        Dim xo, yo As Single

        '--> Initialisaiton

        nbPts = 0

        '--> on commence le contour par le côté droit inférieur

        PrepareContourDalleMixteParalleInfDroite(MyDalle, Bfs, BeffD, False, xPts, yPts, nbPts)

        '--> Partie supérieure de la dalle

        xo = BeffD
        yo = MyDalle.t_d
        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -BeffG
        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Partie gauche de la dalle

        PrepareContourDalleMixteParalleInfDroite(MyDalle, Bfs, BeffG, True, xPtsG, yPtsG, nbPtsG)

        For i = nbPtsG - 1 To 0 Step -1

            AjoutePoint(-xPtsG(i), yPtsG(i), xPts, yPts, nbPts)

        Next

    End Sub

    Private Sub PrepareContourDalleMixteParalleInfDroite(ByVal MyDalle As Cls_Dalle, Bfs As Decimal, BeffD As Decimal, lGauche As Boolean,
                                                         ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour de la partie inférieur droite
        '   d'une dalle mixte à nervure parallèle
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle     [E] :   Classe dalle
        '   BeffD       [E] :   Largeur de la dalle représentée à l'écran sur le côté droite
        '   lGauche     [E] :   Indique si partie gauche ou droite
        '   Bfs         [E] :   Largeur de la semelle sup
        '   xPts, yPts  [S] :   Coordonnées de points définissant le contour
        '   nbPts       [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------
        '   Nota : on utilise cette routine pour le côté gauche par un mirroir / y
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, yo As Single
        Dim eP, hP, bt, bb, hPg As Decimal
        Dim lCont As Boolean = True
        Dim xPos As Decimal
        Dim DeltaB As Decimal
        Dim xStart As Decimal
        Dim lRaid As Boolean
        Dim bSupBac, dXRaid As Decimal
        Dim bbRaid, btRaid As Decimal

        '--> Initialisaiton

        nbPts = 0
        eP = MyDalle.Bac.e_p
        hP = MyDalle.Bac.h_p
        hPg = MyDalle.Bac.Hauteur_hpg
        bb = MyDalle.Bac.b_b
        bt = MyDalle.Bac.b_t
        DeltaB = (bt - bb) / 2

        lRaid = (((hPg - hP) / hP) > 0.05)
        If lRaid Then
            bSupBac = eP - bt
            bbRaid = Cls_Bac.RATIOB1R * bSupBac
            btRaid = Cls_Bac.RATIOB2R * bSupBac
            dXRaid = btRaid - bbRaid
        End If

        If (MyDalle.Bac.AppuiL = Cls_Bac.EnuConfigLAppui.BacCoupe) Then
            xStart = Bfs / 2
        Else
            xStart = bb / 2
        End If
        '# point de départ 

        xo = xStart
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xPos = xo
        lCont = (xPos + eP < BeffD)

        '# boucle sur toutes les nervures complètes

        Do While lCont

            xo += DeltaB
            yo = hP

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            If lRaid Then
                xo += bSupBac / 2 - bbRaid / 2
                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                xo -= dXRaid / 2
                yo = hPg
                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                xo += btRaid
                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                xo -= dXRaid / 2
                yo = hP
                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                xo += bSupBac / 2 - bbRaid / 2
                AjoutePoint(xo, yo, xPts, yPts, nbPts)
            Else
                xo += eP - bt

                AjoutePoint(xo, yo, xPts, yPts, nbPts)
            End If

            xo += DeltaB
            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = xPos + eP

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xPos += eP

            lCont = (xPos + eP < BeffD / 2)
        Loop

        '# recherche de l'intersection dans la dernière nervure à droite

        '--> Recherche de l'intersection

        If (xPos + DeltaB >= BeffD) Then
            'Intersection au droit de la partie remontante

            xo = BeffD
            yo = hP * (BeffD - xPos) / DeltaB

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

        Else
            lCont = True
            xo += DeltaB
            yo = hP

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xPos = xo
        End If
        If lCont Then
            If lRaid Then
                If (xPos + (eP - bt - bbRaid) / 2 >= BeffD) Then
                    'Intersection au droit de la partie sup du bac, à gauche du raidisseur
                    lCont = False

                    xo = BeffD
                    yo = hP
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)
                Else
                    lCont = True
                    xo += (eP - bt - bbRaid) / 2
                    yo = hP
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xo += -dXRaid / 2
                    yo = hPg
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xPos = xo
                End If

                If (xPos + (btRaid) >= BeffD) Then
                    'Intersection au droit de la partie sup du raidisseur
                    lCont = False

                    xo = BeffD
                    yo = hPg
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)
                Else
                    lCont = True
                    xo += btRaid
                    yo = hPg
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xo += -dXRaid / 2
                    yo = hP
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xPos = xo
                End If

                If (xPos + (eP - bt - bbRaid) / 2 >= BeffD) Then
                    'Intersection au droit de la partie sup du bac, à droite du raidisseur
                    lCont = False

                    xo = BeffD
                    yo = hP
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)
                Else
                    lCont = True
                    xo += (eP - bt - bbRaid) / 2
                    yo = hP
                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xPos = xo
                End If

            Else
                If (xPos + eP - bt >= BeffD) Then
                    'Intersection au droit de la partie sup
                    lCont = False

                    xo = BeffD
                    yo = hP

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                Else
                    lCont = True
                    xo += eP - bt
                    yo = hP

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xPos = xo
                End If
            End If


            If lCont Then

                If (xPos + DeltaB >= BeffD) Then
                    'Intersection au droit de la partie descendante
                    lCont = False

                    xo = BeffD
                    yo = hP - hP * (BeffD - xPos) / DeltaB

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                Else

                    lCont = True
                    xo += DeltaB
                    yo = 0

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                    xPos = xo

                End If

                If lCont Then
                    'Il ne reste plus que l'intersection dans la partie inférieure restante

                    xo = BeffD
                    yo = 0

                    AjoutePoint(xo, yo, xPts, yPts, nbPts)

                End If

            End If

        End If

    End Sub

    Private Sub DessinDallePreFab(ByRef MyGr As Graphics, MyDalle As Cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                  MyBrushDP As Brush, MyBrushPref As Brush,
                                  Optional BeffRed As Decimal = -1)
        '---------------------------------------------------------------------------------------------------------------------------
        '   29/06/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle pleine partiellement préfabriquée
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   MyBrushPref [E] :   Pinceau pour le remplissage de la prédalle
        '   BeffRed     [E] :   Largeur de dalle réduite pour le dessin (si -1, on prend la largeur complète)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Td As Decimal = MyDalle.t_d
        Dim Tj As Decimal = MyDalle.preDalle_ep - MyDalle.preDalle_tjoint
        Dim dCar As Decimal = (MyDalle.t_d) / 5
        Dim lDalleRed As Boolean
        Dim BeffDes As Decimal = MyDalle.Beff
        Dim MyPen As New Pen(Color.Black, 1)
        Dim wApp As Decimal = MyDalle.wAppuiPreDalle
        Dim pred_ep As Decimal = MyDalle.preDalle_ep
        Dim xo, xe As Decimal

        '--> Initialisation

        If BeffRed = -1 Then
            lDalleRed = False
        Else
            lDalleRed = (BeffRed < MyDalle.Beff)
        End If
        If lDalleRed Then BeffDes = BeffRed

        '--> Affichage de la dalle pleine (nécessairement sans renformis)

        AddRectanglePlein(MyGr, MyBrushDP, MyPen, -BeffDes / 2, 0, BeffDes / 2, Td, MyParAffA, True, False)

        '--> Affichage des deux prédalles

        xe = -Bfs / 2 + wApp
        xo = -BeffDes / 2
        AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, 0, xe, pred_ep, MyParAffA, True, False)
        AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)
        AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)

        xe = +Bfs / 2 - wApp
        xo = +BeffDes / 2
        AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, 0, xe, pred_ep, MyParAffA, True, False)
        AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)
        AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)

        '--> Joint

        Dim CouleurJ As Color = Color.Linen
        xe = -Bfs / 2 + wApp
        xo = -BeffDes / 2
        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)
        xe = +Bfs / 2 - wApp
        xo = +BeffDes / 2
        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)

        '--> Finitions

        xo = -BeffDes / 2
        xe = -BeffDes / 2
        AddLigne(MyGr, xe, 0, xe, 0, MyParAffA)
        AddLigne(MyGr, xe, Td, xe, Td, MyParAffA)

        '--> Fin

        MyPen.Dispose()
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

    Public Function StyleCouleur(iSelect As Integer, iRef As Integer) As Color
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
