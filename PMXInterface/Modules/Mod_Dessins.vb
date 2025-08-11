'Option Strict On

Imports System.Collections.Specialized.BitVector32
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports PMXInterface.Cls_Rapport
'Imports PMXInterface.Mod_MethodeHivoss
Imports PMXMoteur2

Public Module Mod_Dessins

#Region " Variables locales "

    Dim MyPenContour As New Pen(Color.Black, 1)

    Public Const lCONTOURCOTE As Boolean = False

    Const DELTATHETA As Decimal = 25
    Const DELTATime As Decimal = 5 * 60

    Dim ColorNotPossible As Color = Color.LightGray


#End Region

#Region " Dessins pour la fenetre principale (FRM_MAIN) "
    Public Sub DessinFrmMain_Coupe(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, myBeam As cls_Poutre,
                                   lZoomPlus As Boolean, lCotation As Boolean, lIdentification As Boolean,
                                   Company As String, Projet As String, strPRS As String, strPlat As String, myFont As Font, ColorFond As Color,
                                   ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   pWi, pHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre à dessiner
        '   lZoomPlus   [E] :   Indique si zoom
        '   lCotation   [E] :   Indique si affichage cotation
        '   lIdentificat[E] :   Indique si affichage de l'identification
        '   strPRS      [E] :   Message pour mySection soudée
        '   strPlat     [E] :   Message pour plat
        '   myFont      [E] :   Police à utiliser pour l'affichage
        '   ColorFond   [E] :   Couleur du fond sur lequel on dessine
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------
        '   iSelect:    0 épaisseur de la dalle
        '               1 épaisseur renformis
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim mySection As cls_Section = myBeam.Section
        Dim myDalle As cls_Dalle = myBeam.Dalle

        Dim myParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double
        Dim lMixte, lEnrob, lLamine, lSlimfloor As Boolean
        Dim EntraxeTot As Decimal
        Dim EntraxeD1, EntraxeD2, EntraxeMax As Decimal

        Dim ColorLocalEtriers As Color = CouleurArmaNormal
        Dim ColorLocalArma(2) As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurAcier As Color = CouleurAcierNormal
        Dim CouleurConnecteur As Color = CouleurConnecteurNormal
        'Dim pColorLocalArma(2, 2) As Color
        Dim ColorArmatures(1) As Color
        Const kADJUST As Decimal = 0.95
        Dim zREF As Decimal = 0
        Dim Ha, Bfs, Bfi As Decimal
        Dim lCote As Boolean = True
        Dim lCofraplus220 As Boolean

        'Partie qui concene la cotation 
        Dim Chaine As String = ""
        Dim xo_cotes, xe_cotes, yo_cotes, ye_cotes As Decimal
        Dim MyPen As New Pen(Color.Black, 1) 'Pen utilise pour les fleches/cotations 
        Dim myFontNormal As Font = myFont
        Dim lContour As Boolean = lCONTOURCOTE
        Dim CouleurTremie As Color = CouleurTremieNormal
        Dim myBrushT As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurTremie, CouleurTremie)
        Dim yInfTremieG, ySupTremieG, yInfTremieD, ySupTremieD As Decimal
        Dim dCote As Decimal

        '==============================================================================================
        '   Z = 0 est toujours situé sur la fibre inférieure de la dalle
        '==============================================================================================

        '--> Initialisation

        lMixte = mySection.lMixte
        lEnrob = mySection.lEnrobage
        lLamine = mySection.lLamine
        lCofraplus220 = myDalle.Bac.lCofraplus220
        lSlimfloor = mySection.lSlimFloor

        EntraxeD1 = myBeam.EntraxeD1
        EntraxeD2 = myBeam.EntraxeD2
        EntraxeTot = EntraxeD1 + EntraxeD2
        EntraxeMax = Math.Max(EntraxeD1, EntraxeD2)

        Ha = mySection.ProfilA.ha
        Bfs = mySection.ProfilA.Bfs
        Bfi = mySection.ProfilA.Bfi

        '--> Preparation de la zone d'affichage - Calcul de ParAff

        dCar = Math.Sqrt(EntraxeTot ^ 2 + (Ha + myDalle.zTop) ^ 2) / 10
        dCote = Math.Sqrt((Bfs + Bfi) ^ 2 / 4 + (Ha + myDalle.zTop) ^ 2) / 4

        If lZoomPlus Then
            xMin = -EntraxeMax / 4
            xMax = EntraxeMax / 4

            If lSlimfloor Then
                yMin = -mySection.ProfilA.Plat_t - 0.5 * dCar
            Else
                yMin = -mySection.ProfilA.ha - dCote
            End If
            yMax = myDalle.zTop + dCar * 0.5
        Else
            xMin = -EntraxeMax - dCar
            xMax = EntraxeMax + dCar

            If lSlimfloor Then
                yMin = -mySection.ProfilA.Plat_t - 1.5 * dCar
            Else
                yMin = -mySection.ProfilA.ha - 1.5 * dCar
            End If
            yMax = myDalle.zTop + dCar
        End If

        ParametresAffichage(myParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

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
        ' Connecteurs
        Dim myBrushC As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurConnecteur)

        ColorArmatures(0) = CouleurArmaNormal
        ColorArmatures(1) = CouleurArmaNormal

        myBrushA(0) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorArmatures(0), ColorArmatures(0))
        myBrushA(1) = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorArmatures(1), ColorArmatures(1))

        '--> Dessin de la dalle

        'If lMixte Then 

        '# Dalle béton

        Select Case myDalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine

                DessineDallePleine_Frm_Main(myGr, myBeam, Ha, Bfs, myParAff, myBrushB, EntraxeD2, myBeam.lIntermediaire, mySection.ProfilA, EntraxeD1, EntraxeMax)

            Case cls_Dalle.Enum_TypeDalle.Mixte
                Select Case myDalle.Bac.Orientation
                    Case cls_Bac.Enum_Orientation.Parallele
                        DessineDalleMixteParallele_Frm_Main(myGr, myDalle, Ha, Bfs, myParAff, myBrushB, EntraxeD2, myBeam.lIntermediaire, EntraxeD1, EntraxeMax)
                    Case cls_Bac.Enum_Orientation.Perpendiculaire
                        If lCofraplus220 Then
                            DessineDalleMixtePerpendiculaireCfp220_Frm_Main(myGr, myBeam, myParAff, myBrushB, EntraxeD2, myBeam.lIntermediaire, EntraxeD1, EntraxeMax)
                        Else
                            DessineDalleMixtePerpendiculaire_Frm_Main(myGr, myDalle, Ha, Bfs, myParAff, myBrushB, EntraxeD2, myBeam.lIntermediaire, mySection, myBeam.LargeurDalleDispo, EntraxeD1, EntraxeMax)
                        End If
                End Select

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                DessinDallePreFab_Frm_Main(myGr, myDalle, Ha, Bfs, myParAff, myBrushB, myBrushPref, EntraxeD2, myBeam.lIntermediaire, mySection.ProfilA, EntraxeD1, EntraxeMax)
            Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                DessinDalleComplementePrefa_Frm_Main(myGr, myDalle, Ha, Bfs, myParAff, myBrushB, myBrushPref, EntraxeD2, myBeam.lIntermediaire, mySection.ProfilA, EntraxeD1, EntraxeMax)
        End Select

        ''# Armatures

        If lSlimfloor Then
            If myBeam.Dalle.ArmaSlimFeu.lBarre Then
                Dim Tw As Decimal = myBeam.Section.ProfilA.Tw
                If myBeam.lIntermediaire Then
                    DessinLitArmaDalleFeuSlim(myGr, myDalle, myParAff, myBrushA(0), -myBeam.EntraxeD1, Tw)
                End If
                DessinLitArmaDalleFeuSlim(myGr, myDalle, myParAff, myBrushA(0), 0, Tw)
                DessinLitArmaDalleFeuSlim(myGr, myDalle, myParAff, myBrushA(0), myBeam.EntraxeD2, Tw)
            End If
        Else
            DessinLitArmaDalle_Frm_Main(myGr, myDalle, 0, mySection.ProfilA.ha, myParAff, myBrushA(0), EntraxeD2, myBeam.lIntermediaire, EntraxeD1, EntraxeMax)
            DessinLitArmaDalle_Frm_Main(myGr, myDalle, 1, mySection.ProfilA.ha, myParAff, myBrushA(0), EntraxeD2, myBeam.lIntermediaire, EntraxeD1, EntraxeMax)
        End If

        '--> Dessins des connecteurs

        If myBeam.lMixte Then DessinConnecteurs_Frm_Main(myGr, myBeam, myParAff, myBrushC, EntraxeD2, myBeam.lIntermediaire, EntraxeD1)

        '--> Dessin de la poutre de gauche

        If myBeam.lIntermediaire Then

            DessinFrmMainCoupeProfile(myGr, mySection, lEnrob, -EntraxeD1, False, zREF, myParAff, myBrushB, myBrushE, myBrushP)

        End If

        '--> Dessin de la poutre calculée 

        DessinFrmMainCoupeProfile(myGr, mySection, lEnrob, 0, True, zREF, myParAff, myBrushB, myBrushE, myBrushP, myBeam.lIntermediaire)

        '--> Dessin de la poutre droite 

        DessinFrmMainCoupeProfile(myGr, mySection, lEnrob, EntraxeD2, False, zREF, myParAff, myBrushB, myBrushE, myBrushP)

        '--> Dessin des cotations pour les profilés

        If lCotation Then

            DessinFrmMain_Cotation_Entraxes(myBeam, myGr, myParAff, MyPen, myFontNormal, EntraxeD1, EntraxeD2, dCote, lZoomPlus)

            If lSlimfloor Then
                DessinFrmMain_Cotation_SectionSlimF(myBeam, myGr, myParAff, MyPen, myFontNormal, dCote, dCar, lZoomPlus, myBrushB, ColorFond)
            Else
                DessinFrmMain_Cotation_SectionStandard(myBeam, myGr, myParAff, MyPen, myFontNormal, dCote, dCar, lZoomPlus)
            End If

        End If

        '--( Affichage du nom du profilé

        'If (Not MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym Or Not MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym) And lCotation Then
        If lCotation Then

            DessinFrmMain_LabelProfiles(myBeam, myGr, myParAff, MyPen, myFontNormal, lZoomPlus, dCar, strPRS, strPlat, zREF)

        End If

        '--> Dessin tremies

        If myBeam.lTremieGauche Or myBeam.lTremieDroite Then
            If lCofraplus220 Then
                yInfTremieG = zREF - myDalle.Bac.Hp - mySection.ProfilA.Tfs
                ySupTremieG = zREF + myBeam.Dalle.Ep_td + mySection.ProfilA.Tfs

                yInfTremieD = yInfTremieG
                ySupTremieD = ySupTremieG
            ElseIf myDalle.type = cls_Dalle.Enum_TypeDalle.Pleine Then
                If myBeam.DistanceDsl1 <= myBeam.Section.ProfilA.Bfs / 2 + myDalle.Ep_th * Math.Tan(myDalle.ThetaRd) Then
                    yInfTremieG = zREF
                    ySupTremieG = zREF + myBeam.Dalle.Ep_td + myBeam.Dalle.Ep_th + mySection.ProfilA.Tfs
                Else
                    yInfTremieG = zREF + myBeam.Dalle.Ep_th - mySection.ProfilA.Tfs
                    ySupTremieG = zREF + myBeam.Dalle.Ep_td + myBeam.Dalle.Ep_th + mySection.ProfilA.Tfs
                End If

                If myBeam.DistanceDsl2 <= myBeam.Section.ProfilA.Bfs / 2 + myDalle.Ep_th * Math.Tan(myDalle.ThetaRd) Then
                    yInfTremieD = zREF
                    ySupTremieD = zREF + myBeam.Dalle.Ep_td + myBeam.Dalle.Ep_th + mySection.ProfilA.Tfs
                Else
                    yInfTremieD = zREF + myBeam.Dalle.Ep_th - mySection.ProfilA.Tfs
                    ySupTremieD = zREF + myBeam.Dalle.Ep_td + myBeam.Dalle.Ep_th + mySection.ProfilA.Tfs
                End If
            Else
                yInfTremieG = zREF - mySection.ProfilA.Tfs
                ySupTremieG = zREF + myBeam.Dalle.Ep_td + mySection.ProfilA.Tfs

                yInfTremieD = yInfTremieG
                ySupTremieD = ySupTremieG
            End If


        End If

        '--> Trémie gauche

        If myBeam.lTremieGauche Then
            AddRectanglePlein(myGr, myBrushT, MyPenContour, -myBeam.EntraxeD1 + myBeam.DistanceDsl1, yInfTremieG, -myBeam.DistanceDsl1, ySupTremieG, myParAff, True, True, True, False, False)

            If lCotation And Not lZoomPlus Then

                'cotation de l'axe au bord
                xo_cotes = -myBeam.DistanceDsl1
                xe_cotes = 0
                yo_cotes = ySupTremieG + dCar / 4
                ye_cotes = yo_cotes

                AddFleche(myGr, MyPen, xo_cotes, yo_cotes, xe_cotes, ye_cotes, myParAff, True, True)
                Chaine = GetStringNoUnit(myBeam.DistanceDsl1, Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyPen.Color), Chaine, myFontNormal, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)


                'cotation de la tremie gauche
                xo_cotes = -EntraxeD1 + myBeam.DistanceDsl1
                xe_cotes = -myBeam.DistanceDsl1

                AddFleche(myGr, MyPen, xo_cotes, yo_cotes, xe_cotes, ye_cotes, myParAff, True, True)
                Chaine = GetStringNoUnit(Math.Abs(xe_cotes - xo_cotes), Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyPen.Color), Chaine, myFontNormal, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

        End If

        '--> Trémie droite  

        If myBeam.lTremieDroite Then
            AddRectanglePlein(myGr, myBrushT, MyPenContour, myBeam.DistanceDsl2, yInfTremieD, myBeam.EntraxeD2 - myBeam.DistanceDsl2, ySupTremieD, myParAff, True, True, True, False, False)

            If lCotation And Not lZoomPlus Then

                'cotationde l'axe au bord droit 
                xo_cotes = 0
                xe_cotes = myBeam.DistanceDsl2
                yo_cotes = ySupTremieD + dCar / 4
                ye_cotes = yo_cotes

                AddFleche(myGr, MyPen, xo_cotes, yo_cotes, xe_cotes, ye_cotes, myParAff, True, True)
                Chaine = GetStringNoUnit(myBeam.DistanceDsl2, Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyPen.Color), Chaine, myFontNormal, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                'cotation de la tremie gauche
                xo_cotes = myBeam.DistanceDsl2
                xe_cotes = EntraxeD2 - myBeam.DistanceDsl2

                AddFleche(myGr, MyPen, xo_cotes, yo_cotes, xe_cotes, ye_cotes, myParAff, True, True)
                Chaine = GetStringNoUnit(Math.Abs(xe_cotes - xo_cotes), Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyPen.Color), Chaine, myFontNormal, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)


            End If

        End If

        'Affichage nom du goujon disposé, le cas écheant

        If myBeam.lMixte And lCotation Then
            'Cotation
            'If lZoomPlus Then
            '    xo_cotes = -myBeam.Section.ProfilA.Bfs / 2 - dCar / 4
            'Else
            '    xo_cotes = -myBeam.Section.ProfilA.Bfs / 2 - dCar / 2
            'End If
            'xe_cotes = xo_cotes

            'If lZoomPlus Then
            '    yo_cotes = zREF - myBeam.Section.ProfilA.Tfs - dCar / 4
            'Else
            '    yo_cotes = zREF - myBeam.Section.ProfilA.Tfs - dCar / 4
            'End If

            xo_cotes = 0
            xe_cotes = xo_cotes

            yo_cotes = zREF + myBeam.Dalle.Ep_td + dCar / 8
            ye_cotes = yo_cotes

            Chaine = myBeam.Dalle.Goujons.nom
            'AddTexteFond(myGr, New SolidBrush(MyPen.Color), Chaine, MyFontNormal, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            AddTexte(myGr, New SolidBrush(MyPen.Color), Chaine, myFontNormal, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
        End If

        ' Identification (19/11/24)

        If lIdentification Then DessinIdentification(myGr, myBeam, Company, Projet, myFontNormal)

    End Sub

    Private Sub DessinFrmMain_LabelProfiles(myBeam As cls_Poutre, myGr As Graphics, myParAff As Struc_Affichage,
                                            myPen As Pen, myFont As Font, lZoomPlus As Boolean, dCar As Decimal,
                                            strPRS As String, strPlat As String, ZRef As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   21/07/25 :  Version 1.10 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des labels des profilés pour la fenêtre principale - Cas des sections standard
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre à dessiner
        '   myParAff    [E] :   Paramètres d'affichage  
        '   myPen       [E] :   Pen à utiliser pour les fleches/cotations    
        '   myFont      [E] :   Police à utiliser pour l'affichage  
        '   EntraxeD1   [E] :   Entraxe de la poutre à gauche
        '   EntraxeD2   [E] :   Entraxe de la poutre à droite
        '   dCote       [E] :   Décalage pour les cotations 
        '   dCar        [E] :   Dimension caracteristique pour le dessin
        '   strPRS      [E] :   Label pour PRS
        '   strPlat     [E] :   Label pour un plat
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim horAlignement As HorizontalAlignment
        Dim lContour As Boolean = lCONTOURCOTE

        Dim lSlimfloor As Boolean
        Dim Chaine As String = ""
        Dim xo_cotes, xe_cotes, yo_cotes, ye_cotes As Decimal
        Dim lLamine, lPlat As Boolean

        '--( Initialisation

        lSlimfloor = myBeam.Section.lSlimFloor
        lLamine = myBeam.Section.lLamine
        lPlat = myBeam.Section.ProfilA.lPlat And (Not IsEqual(myBeam.Section.ProfilA.Plat_t, 0D)) And (Not IsEqual(myBeam.Section.ProfilA.Plat_b, 0D))

        '--( Traitement

        If lSlimfloor Then
            horAlignement = HorizontalAlignment.Left
            xo_cotes = Math.Max(myBeam.Section.ProfilA.Bfs / 2, Math.Max(myBeam.Section.ProfilA.Bfi / 2, myBeam.Section.ProfilA.Plat_b / 2))
            xo_cotes = myBeam.Section.LargeurPlatInfSlim / 2
            If lZoomPlus Then
                xo_cotes += dCar / 10
            Else
                xo_cotes += dCar / 8
            End If
            xe_cotes = xo_cotes
            yo_cotes = 0 ' myBeam.Section.zInf
            ye_cotes = yo_cotes + myBeam.Section.ProfilA.ha

            Chaine = myBeam.Section.ProfilA.NomProfile

            Select Case myBeam.Section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    Chaine += " (SFB)"
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    Chaine += " (IFB-A)"
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    Chaine += " (IFB-B)"
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    Chaine += " (SAB)"
            End Select

        Else
            xo_cotes = myBeam.Section.ProfilA.Bfs / 2
            If lZoomPlus Then
                xo_cotes += dCar / 10
            Else
                xo_cotes += dCar / 8
            End If
            xe_cotes = xo_cotes
            yo_cotes = ZRef
            ye_cotes = yo_cotes - myBeam.Section.ProfilA.ha

            If lLamine Then
                Chaine = myBeam.Section.ProfilA.NomProfile
            Else
                Chaine = strPRS
            End If

            horAlignement = HorizontalAlignment.Left

        End If

        '--( Affichage du label du profilé

        AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, horAlignement, VerticalAlignement.Middle, New SolidBrush(Color.Transparent), myPen, lContour)

        '--( Ajout des dimensions du plat (la cas échéant)

        If lSlimfloor Then

            Select Case myBeam.Section.TypeSection
                Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte,
                     cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Bmixte

                    Chaine = strPlat & " (" &
                         GetStringInUnitN(myBeam.Section.ProfilA.Plat_b, Enu_TypeVariable.Dimension, 4, 3, NON_U, True) & " x " &
                         GetStringInUnitN(myBeam.Section.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 4, 3, NON_U, True) & ")"

                    xo_cotes = myBeam.Section.LargeurPlatInfSlim / 2
                    If lZoomPlus Then
                        xo_cotes += dCar / 10
                    Else
                        xo_cotes += dCar / 8
                    End If

                    yo_cotes = myBeam.Section.zInf + myBeam.Section.ProfilA.Plat_t / 2

                    AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xo_cotes, yo_cotes, myParAff, horAlignement, VerticalAlignement.Top, New SolidBrush(Color.Transparent), myPen, lContour)

            End Select

        Else

            If lPlat Then

                Chaine = strPlat & " (" &
                         GetStringInUnitN(myBeam.Section.ProfilA.Plat_b, Enu_TypeVariable.Dimension, 4, 3, NON_U, True) & " x " &
                         GetStringInUnitN(myBeam.Section.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 4, 3, NON_U, True) & ")"

                xo_cotes = myBeam.Section.ProfilA.Bfs / 2
                If lZoomPlus Then
                    xo_cotes += dCar / 10
                Else
                    xo_cotes += dCar / 8
                End If

                yo_cotes = ZRef - myBeam.Section.ProfilA.ha - myBeam.Section.ProfilA.Plat_t / 2

                AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xo_cotes, yo_cotes, myParAff, horAlignement, VerticalAlignement.Middle, New SolidBrush(Color.Transparent), myPen, lContour)

            End If

        End If

    End Sub

    Private Sub DessinFrmMain_Cotation_SectionSlimF(myBeam As cls_Poutre, myGr As Graphics, myParAff As Struc_Affichage, myPen As Pen, myFont As Font,
                                                    dCote As Decimal, dCar As Decimal, lZoomPlus As Boolean, myBrushB As Brush, ColorFond As Color)
        '-----------------------------------------------------------------------------------------------
        '   21/07/25 :  Version 1.10 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des cotations pour la fenêtre principale - Cas des sections slim floor
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre à dessiner
        '   myParAff    [E] :   Paramètres d'affichage  
        '   myPen       [E] :   Pen à utiliser pour les fleches/cotations    
        '   myFont      [E] :   Police à utiliser pour l'affichage  
        '   EntraxeD1   [E] :   Entraxe de la poutre à gauche
        '   EntraxeD2   [E] :   Entraxe de la poutre à droite
        '   dCote       [E] :   Décalage pour les cotations 
        '   dCar        [E] :   Dimension caracteristique pour le dessin
        '   lZoomPlus   [E] :   Indique si zoom
        '   myBrushB    [E] :   Pinceau pour le béton (dalle)
        '   ColorFond   [E] :   Couleur du fond sur lequel on dessine
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Chaine As String = ""
        Dim Ha As Decimal = myBeam.Section.ProfilA.ha
        Dim lContour As Boolean = lCONTOURCOTE
        Dim zTop, zBot As Decimal
        Dim bPlatInf As Decimal
        Dim lInter As Boolean
        Dim myBrushFondTexte As Brush
        Dim kStep As Decimal
        Dim xPlatInfG As Decimal
        Dim Bfi, Bfs As Decimal
        Dim lSlimF As Boolean
        Dim hPro As Decimal

        '--( Initialisation

        zTop = myBeam.Dalle.zTop
        zBot = myBeam.Section.zInf
        bPlatInf = myBeam.Section.LargeurPlatInfSlim
        Bfi = myBeam.Section.ProfilA.Bfi
        Bfs = myBeam.Section.ProfilA.Bfs
        hPro = myBeam.Section.ProfilA.ha

        lInter = myBeam.lIntermediaire
        lSlimF = myBeam.lSlimFloor

        If lInter Then
            myBrushFondTexte = myBrushB
            kStep = 1.5
            xPlatInfG = -bPlatInf / 2
        Else
            myBrushFondTexte = New SolidBrush(ColorFond)
            kStep = 2
            xPlatInfG = myBeam.Section.xBordDalleRive(myBeam.EntraxeD1)
        End If

        '--( Traits de référence 

        AddLigne(myGr, myPen, -kStep * dCar, zBot, 1.15 * xPlatInfG, zBot, myParAff)

        If (Not lInter) And lSlimF Then

            AddLigne(myGr, myPen, -kStep * dCar, zTop, 1.15 * xPlatInfG, zTop, myParAff)
            AddLigne(myGr, myPen, -1 * dCar, zBot + hPro, 1.15 * xPlatInfG, zBot + hPro, myParAff)

        End If

        '-- Hauteur du profilé

        'Dim xVcote As Decimal = EntraxeD2 - 2 * myBeam.Section.ProfilA.BfMax
        Dim xVcote As Decimal = 0 - 1 * dCar

        AddFleche(myGr, myPen, xVcote, zBot, xVcote, zBot + hPro, myParAff, True, True)
        Chaine = GetStringInUnitN(hPro, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        'AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, hPro / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), myPen, lContour)
        AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, hPro / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle,
                     myBrushFondTexte, myPen, lContour)

        '-- Hauteur totale

        xVcote = 0 - kStep * dCar

        AddFleche(myGr, myPen, xVcote, zBot, xVcote, zTop, myParAff, True, True)
        Chaine = GetStringInUnitN(zTop - zBot, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, (zTop + zBot) / 2, myParAff,
                     HorizontalAlignment.Center, VerticalAlignement.Middle, myBrushFondTexte, myPen, lContour)

        '-- Hauteur de la partie préfabriquée


        '-- Hauteur enrobage semelle


        myBrushFondTexte.Dispose()
    End Sub

    Private Sub DessinFrmMain_Cotation_Entraxes(myBeam As cls_Poutre, myGr As Graphics, myParAff As Struc_Affichage, myPen As Pen, myFont As Font,
                                                EntraxeD1 As Decimal, EntraxeD2 As Decimal, dCote As Decimal, lZoomPlus As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   21/07/25 :  Version 1.10 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des cotations des entraxes pour la fenêtre principale - Cas général
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre à dessiner
        '   myParAff    [E] :   Paramètres d'affichage  
        '   myPen       [E] :   Pen à utiliser pour les fleches/cotations    
        '   myFont      [E] :   Police à utiliser pour l'affichage  
        '   EntraxeD1   [E] :   Entraxe de la poutre à gauche
        '   EntraxeD2   [E] :   Entraxe de la poutre à droite
        '   dCote       [E] :   Décalage pour les cotations 
        '   dCar        [E] :   Dimension caracteristique pour le dessin
        '   lZoomPlus   [E] :   Indique si zoom
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xo_cotes, xe_cotes, yo_cotes, ye_cotes As Decimal
        Dim Chaine As String = ""
        Dim Ha As Decimal = myBeam.Section.ProfilA.ha
        Dim lContour As Boolean = lCONTOURCOTE

        '--( Cotation de la poutre à gauche, ou distance au bord de la dalle

        If (Not lZoomPlus) Then


            xo_cotes = -EntraxeD1
            xe_cotes = 0

            yo_cotes = -Ha - dCote

            ye_cotes = yo_cotes

            AddFleche(myGr, myPen, xo_cotes, yo_cotes, xe_cotes, ye_cotes, myParAff, True, True)
            Chaine = GetStringNoUnit(EntraxeD1, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), myPen, lContour)

        End If

        '--( Cotation de la poutre à droite

        If (Not lZoomPlus) Then
            xo_cotes = EntraxeD2
            xe_cotes = 0

            yo_cotes = -Ha - dCote
            ye_cotes = yo_cotes

            AddFleche(myGr, myPen, xo_cotes, yo_cotes, xe_cotes, ye_cotes, myParAff, True, True)
            Chaine = GetStringNoUnit(EntraxeD2, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, (xo_cotes + xe_cotes) / 2, (yo_cotes + ye_cotes) / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), myPen, lContour)
        End If

    End Sub

    Private Sub DessinFrmMain_Cotation_SectionStandard(myBeam As cls_Poutre, myGr As Graphics, myParAff As Struc_Affichage, myPen As Pen, myFont As Font,
                                                       dCote As Decimal, dCar As Decimal, lZoomPlus As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   21/07/25 :  Version 1.10 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des cotations pour la fenêtre principale - Cas des sections standard
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre à dessiner
        '   myParAff    [E] :   Paramètres d'affichage  
        '   myPen       [E] :   Pen à utiliser pour les fleches/cotations    
        '   myFont      [E] :   Police à utiliser pour l'affichage  
        '   EntraxeD1   [E] :   Entraxe de la poutre à gauche
        '   EntraxeD2   [E] :   Entraxe de la poutre à droite
        '   dCote       [E] :   Décalage pour les cotations 
        '   dCar        [E] :   Dimension caracteristique pour le dessin
        '   lZoomPlus   [E] :   Indique si zoom
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Chaine As String = ""
        Dim Ha As Decimal = myBeam.Section.ProfilA.ha
        Dim lContour As Boolean = lCONTOURCOTE
        Dim lPlat As Boolean = myBeam.Section.ProfilA.lPlat And (Not IsEqual(myBeam.Section.ProfilA.Plat_t, 0D)) And (Not IsEqual(myBeam.Section.ProfilA.Plat_b, 0D))
        Dim tPlat As Decimal

        '--( Initialisation

        If lPlat Then
            tPlat = myBeam.Section.ProfilA.Plat_t
        Else
            tPlat = 0D
        End If

        '-- Hauteur du profilé

        Dim hPro As Decimal = myBeam.Section.ProfilA.ha
        'Dim xVcote As Decimal = EntraxeD2 - 2 * myBeam.Section.ProfilA.BfMax
        Dim xVcote As Decimal = 0 - 1 * dCar

        AddFleche(myGr, myPen, xVcote, 0, xVcote, -hPro, myParAff, True, True)
        Chaine = GetStringInUnitN(hPro, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, -hPro / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), myPen, lContour)

        '-- Plat

        If lPlat Then

            AddLigne(myGr, myPen, xVcote, -hPro, xVcote, -hPro - tPlat, myParAff)
            AddFleche(myGr, myPen, xVcote, -hPro - tPlat * 1.05, xVcote, -hPro - tPlat, myParAff, False, True)
            Chaine = GetStringInUnitN(tPlat, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
            AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, -hPro - tPlat * 1.1, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), myPen, lContour)

        End If

        '-- Hauteur de la dalle

        Dim hDalle As Decimal = myBeam.Dalle.zTop
        Dim DeltaZ As Decimal = dCar / 5

        AddLigne(myGr, xVcote, 0, xVcote, hDalle, myParAff)
        AddFleche(myGr, myPen, xVcote, hDalle, xVcote, hDalle + DeltaZ, myParAff, True, False)

        Chaine = GetStringInUnitN(hDalle, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        AddTexte(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, hDalle + DeltaZ, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top, lContour, myPen)

        '-- Hauteur totale

        xVcote = 0 - 1.5 * dCar

        AddFleche(myGr, myPen, xVcote, -hPro - tPlat, xVcote, +hDalle, myParAff, True, True)
        Chaine = GetStringInUnitN(hPro + hDalle + tPlat, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        AddTexteFond(myGr, New SolidBrush(myPen.Color), Chaine, myFont, xVcote, -hPro / 2, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), myPen, lContour)

    End Sub



    Private Sub DessinIdentification(myGr As Graphics, myBeam As cls_Poutre, Company As String, Projet As String, myFont As Font)
        '---------------------------------------------------------------------------------------------------------------------------
        '   19/11/24    :   Création - POM 
        '---------------------------------------------------------------------------------------------------------------------------
        '   Affiche l'identifcation de la poutre en haut à gauche du dessin
        '---------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre
        '---------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Chaine As String
        Dim myBrush As New SolidBrush(Color.Black)

        Dim dCar As Single = myGr.MeasureString("X", myFont).Height

        '--( Compagnie

        Chaine = Company
        myGr.DrawString(Chaine, myFont, myBrush, dCar, dCar)

        '--( Projet / poutre

        Chaine = Projet & " / " & myBeam.BeamID
        myGr.DrawString(Chaine, myFont, myBrush, dCar, 2.5 * dCar)

        myBrush.Dispose()

    End Sub

    Private Sub DessinFrmMainCoupeProfile(myGr As Graphics, mySection As cls_Section, lEnrob As Boolean,
                                          yPos As Decimal, lPrincipal As Boolean, zRef As Decimal,
                                          myParAff As Struc_Affichage, myBrushB As Brush, myBrushE As Brush, myBrushP As Brush,
                                          Optional lInter As Boolean = True)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - POM 
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'un profilé - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   mySection   [E] :   Section du profilé
        '   lEnrob      [E] :   Indique si profilé partiellement enrobé
        '   lPrincipal  [E] :   Indique si profilé étudié
        '   yPos        [E] :   Position du profilé
        '   zRef        [E] :   Position z de référence
        '   lInter      [E] :   Indique si poutre intermédiaire (ou sinon de rive)
        '   myParAff    [E] :   Paramètres d'affichage
        '   myBrushB    [E] :   Pinceau pour le béton d'enrobage
        '   myBrushE    [E] :   Pinceau pour les armatures d'enrobage
        '   myBrushP    [E] :   Pinceau pour le profilé
        '---------------------------------------------------------------------------------------------------------------------------

        '--( Traitement de l'enrobage

        If lEnrob Then
            'Dessin du béton
            DessinEnrobagePartielBeton(myGr, mySection.ProfilA, mySection.Enrobage.Ratio_bc, myParAff, myBrushB, yPos)
            'Dessin des étriers
            DessinEtriers(myGr, mySection.ProfilA, mySection.Enrobage, myParAff, myBrushE, zRef, yPos)
        End If

        '--( Dessin de la mySection acier

        DessinProfileMetal(myGr, mySection.ProfilA, myBrushP, myParAff, zRef, Not lInter, yPos)

    End Sub

    Private Sub DessineDallePleine_Frm_Main(ByRef MyGr As Graphics, myPoutre As cls_Poutre, Ha As Decimal, Bfs As Decimal, myParAffA As Struc_Affichage, myBrushDP As Brush,
                                           EntraxeD2 As Decimal, lIntermediaire As Boolean, profilA As cls_ProfilA,
                                           Optional EntraxeD1 As Decimal = 0, Optional dCar As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD 
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle pleine (avec renformis) - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   zRef        [E] :   Position de référence pour l'axe z (z0), comptée à partir fibre sup du profilé
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] : Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        'Dim dCar As Decimal = (MyDalle.Ep_th + MyDalle.Ep_td) / 5
        Dim BeffDes As Decimal = myPoutre.LargeurDalleDispo
        Dim myPenDot As New Pen(Color.Black, 0.75)

        '--> Initialisation

        'If BeffRed = -1 Then
        '    lDalleRed = False
        'Else
        '    lDalleRed = (BeffRed < MyDalle.Beff)
        'End If
        'If lDalleRed Then BeffDes = BeffRed

        myPenDot.DashStyle = DashStyle.Custom
        myPenDot.DashPattern = New Single() {4.0F, 6.0F}

        '--> Préparation des points

        PrepareContourDallePleine_Frm_Main(myPoutre.Dalle, myPoutre.Section, BeffDes, Bfs, xPts, yPts, nbPts, EntraxeD1, EntraxeD2, lIntermediaire, dCar)

        '--> Affichage

        RemplirZone(MyGr, myBrushDP, xPts, yPts, nbPts, myParAffA, True, True)

        MyPenContour.Dispose()
        myPenDot.Dispose()
    End Sub

    Private Sub PrepareContourDallePleine_Frm_Main(ByVal myDalle As cls_Dalle, mySection As cls_Section, BeffDes As Decimal, Bfs As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer,
                                                   EntraxeD1 As Decimal, EntraxeD2 As Decimal, lIntermediaire As Boolean, dCar As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle pleine - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle         [E] :   Classe dalle
        '   BeffDes         [E] :   Largeur de la dalle représentée à l'écran
        '   Bfs             [E] :   Largeur de la semelle sup
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] : Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '   xPts, yPts      [S] :   Coordonnées de points définissant le contour
        '   nbPts           [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, yo As Single
        Dim EpTh As Decimal = myDalle.Ep_th
        Dim lSlimF As Boolean = mySection.lSlimFloor
        Dim lRiveP As Boolean = myDalle.lRiveRemplie

        '--> Initialisaiton

        nbPts = 0

        '--> Contour

        If lIntermediaire Then
            xo = -EntraxeD1 - dCar
        Else
            xo = mySection.xBordDalleRive(EntraxeD1)
        End If

        yo = EpTh + myDalle.Ep_td

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        If lSlimF And (Not lIntermediaire) And Not (lRiveP) Then

            Dim zSemMi As Decimal = mySection.zSemSup - mySection.EpPlatSup / 2

            yo = zSemMi
            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = 0
            AjoutePoint(xo, yo, xPts, yPts, nbPts)

        End If

        yo = EpTh

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        If lIntermediaire Then

            xo = CSng(-Bfs / 2 - EpTh * Math.Tan(myDalle.ThetaRd)) - EntraxeD1
            yo = EpTh

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = -Bfs / 2 - EntraxeD1
            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = Bfs / 2 - EntraxeD1
            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = CSng(Bfs / 2 + EpTh * Math.Tan(myDalle.ThetaRd)) - EntraxeD1
            yo = EpTh

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

        End If

        xo = CSng(-Bfs / 2 - EpTh * Math.Tan(myDalle.ThetaRd))

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = Bfs / 2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(Bfs / 2 + myDalle.Ep_th * Math.Tan(myDalle.ThetaRd))
        yo = myDalle.Ep_th

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -CSng(Bfs / 2 + myDalle.Ep_th * Math.Tan(myDalle.ThetaRd)) + EntraxeD2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -Bfs / 2 + EntraxeD2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = Bfs / 2 + EntraxeD2

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(Bfs / 2 + myDalle.Ep_th * Math.Tan(myDalle.ThetaRd)) + EntraxeD2
        yo = myDalle.Ep_th

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = EntraxeD2 + dCar

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        yo = myDalle.Ep_td + myDalle.Ep_th

        AjoutePoint(xo, yo, xPts, yPts, nbPts)


    End Sub

    Private Sub DessinDallePreFab_Frm_Main(ByRef MyGr As Graphics, MyDalle As cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                  MyBrushDP As Brush, MyBrushPref As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean, profilA As cls_ProfilA,
                                                   Optional EntraxeD1 As Decimal = 0, Optional dCar As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle pleine partiellement préfabriquée - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   MyBrushPref [E] :   Pinceau pour le remplissage de la prédalle
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] : Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Td As Decimal = MyDalle.Ep_td
        Dim Tj As Decimal = MyDalle.preDalle_ep - MyDalle.preDalle_tjoint

        Dim MyPen As New Pen(Color.Black, 1)
        Dim wApp As Decimal = MyDalle.wAppuiPreDalle
        Dim LargeurProfilA As Decimal
        Dim pred_ep As Decimal = MyDalle.preDalle_ep
        Dim xo, xe As Decimal
        Dim CouleurJ As Color = Color.Linen

        '--> Initialisation 
        Select Case profilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                LargeurProfilA = profilA.Bfs
                xo = -EntraxeD1
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                LargeurProfilA = profilA.Plat_b
                wApp = Math.Min(wApp, 0.7 * (profilA.Plat_b - profilA.Bfi) / 2)
                xo = -profilA.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                LargeurProfilA = profilA.Plat_b
                xo = -profilA.Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                LargeurProfilA = profilA.Bfi
                xo = -profilA.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                LargeurProfilA = profilA.Bfi
                xo = -profilA.Bfi / 2
        End Select

        '--> AffichageOptFeu de la dalle pleine (nécessairement sans renformis)

        If lIntermediaire Then xo = -EntraxeD1 - dCar

        xe = EntraxeD2 + dCar

        AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, 0, xe, Td, MyParAffA, True, False)

        '--> AffichageOptFeu des prédalles

        If lIntermediaire Then

            xo = -EntraxeD1 - dCar
            xe = -EntraxeD1 - LargeurProfilA / 2 + wApp

            AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, Tj, xe, pred_ep, MyParAffA, True, False)
            AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)

            AddLigne(MyGr, xo, Tj, xe, Tj, MyParAffA)
            AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)
            AddLigne(MyGr, xo, 0, xo, pred_ep, MyParAffA)
            AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)

        End If

        If lIntermediaire Then xo = -EntraxeD1 + LargeurProfilA / 2 - wApp
        xe = -LargeurProfilA / 2 + wApp

        If lIntermediaire Or (profilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Or profilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym Or profilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym) Then

            AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, Tj, xe, pred_ep, MyParAffA, True, False)
            AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)

            AddLigne(MyGr, xo, Tj, xe, Tj, MyParAffA)
            AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)
            AddLigne(MyGr, xo, 0, xo, pred_ep, MyParAffA)
            AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)

        Else 'poutre d'extermité ET slimfloor
            AddLigne(MyGr, xo, 0, xo, Td, MyParAffA)
        End If



        xe = +LargeurProfilA / 2 - wApp
        xo = EntraxeD2 - LargeurProfilA / 2 + wApp

        AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, Tj, xe, pred_ep, MyParAffA, True, False)
        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)

        AddLigne(MyGr, xo, Tj, xe, Tj, MyParAffA)
        AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)
        AddLigne(MyGr, xo, 0, xo, pred_ep, MyParAffA)
        AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)


        xe = EntraxeD2 + dCar
        xo = EntraxeD2 + LargeurProfilA / 2 - wApp

        AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, Tj, xe, pred_ep, MyParAffA, True, False)
        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)

        AddLigne(MyGr, xo, Tj, xe, Tj, MyParAffA)
        AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)
        AddLigne(MyGr, xo, 0, xo, pred_ep, MyParAffA)
        AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)



        '--> Finitions

        If lIntermediaire Then
            xo = -EntraxeD1 - dCar
            xe = EntraxeD2 + dCar
        Else
            Select Case profilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                    xo = -EntraxeD1
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    xo = -profilA.Bfi / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    xo = -profilA.Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    xo = -profilA.Bfi / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    xo = -profilA.Bfi / 2
            End Select
            xe = EntraxeD2 + dCar
        End If
        AddLigne(MyGr, xo, 0, xe, 0, MyParAffA)
        AddLigne(MyGr, xo, Td, xe, Td, MyParAffA)

        If Not lIntermediaire Then AddLigne(MyGr, xo, 0, xo, Td, MyParAffA)


        '--> Fin

        MyPen.Dispose()
    End Sub

    Private Sub DessinDalleComplementePrefa_Frm_Main(ByRef MyGr As Graphics, MyDalle As cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                  MyBrushDP As Brush, MyBrushPref As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean, profilA As cls_ProfilA,
                                                   Optional EntraxeD1 As Decimal = 0, Optional dCar As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle pleine partiellement préfabriquée - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   MyBrushPref [E] :   Pinceau pour le remplissage de la prédalle
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] : Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Td As Decimal = MyDalle.Ep_td
        Dim Dp As Decimal = MyDalle.Cofradal.dp

        Dim MyPen As New Pen(Color.Black, 1)
        Dim wApp As Decimal = MyDalle.wAppuiPreDalle
        Dim LargeurProfilA As Decimal
        Dim xo, xe As Decimal
        Dim CouleurJ As Color = Color.Linen

        '--> Initialisation 
        Select Case profilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                LargeurProfilA = profilA.Bfs
                xo = -EntraxeD1
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                LargeurProfilA = profilA.Plat_b
                wApp = Math.Min(wApp, 0.7 * (profilA.Plat_b - profilA.Bfi) / 2)
                xo = -profilA.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                LargeurProfilA = profilA.Plat_b
                xo = -profilA.Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                LargeurProfilA = profilA.Bfi
                xo = -profilA.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                LargeurProfilA = profilA.Bfi
                xo = -profilA.Bfi / 2
        End Select

        '--> AffichageOptFeu de la dalle pleine (nécessairement sans renformis)

        If lIntermediaire Then xo = -EntraxeD1 - dCar

        xe = EntraxeD2 + dCar

        AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, 0, xe, Td, MyParAffA, True, False)

        '--> AffichageOptFeu des prédalles

        If lIntermediaire Then

            xo = -EntraxeD1 - dCar
            xe = -EntraxeD1 - LargeurProfilA / 2 + wApp

            AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Dp, MyParAffA, False)

            AddLigne(MyGr, xo, Dp, xe, Dp, MyParAffA)
            AddLigne(MyGr, xo, 0, xo, Dp, MyParAffA)
            AddLigne(MyGr, xe, 0, xe, Dp, MyParAffA)

        End If

        If lIntermediaire Then xo = -EntraxeD1 + LargeurProfilA / 2 - wApp
        xe = -LargeurProfilA / 2 + wApp

        If lIntermediaire Or (profilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Or profilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym Or profilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym) Then

            AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Dp, MyParAffA, False)

            AddLigne(MyGr, xo, Dp, xe, Dp, MyParAffA)
            AddLigne(MyGr, xo, 0, xo, Dp, MyParAffA)
            AddLigne(MyGr, xe, 0, xe, Dp, MyParAffA)

        Else 'poutre d'extermité ET slimfloor
            AddLigne(MyGr, xo, 0, xo, Td, MyParAffA)
        End If



        xe = +LargeurProfilA / 2 - wApp
        xo = EntraxeD2 - LargeurProfilA / 2 + wApp

        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Dp, MyParAffA, False)

        AddLigne(MyGr, xo, Dp, xe, Dp, MyParAffA)
        AddLigne(MyGr, xo, 0, xo, Dp, MyParAffA)
        AddLigne(MyGr, xe, 0, xe, Dp, MyParAffA)


        xe = EntraxeD2 + dCar
        xo = EntraxeD2 + LargeurProfilA / 2 - wApp

        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Dp, MyParAffA, False)

        AddLigne(MyGr, xo, Dp, xe, Dp, MyParAffA)
        AddLigne(MyGr, xo, 0, xo, Dp, MyParAffA)
        AddLigne(MyGr, xe, 0, xe, Dp, MyParAffA)



        '--> Finitions

        If lIntermediaire Then
            xo = -EntraxeD1 - dCar
            xe = EntraxeD2 + dCar
        Else
            Select Case profilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                    xo = -EntraxeD1
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    xo = -profilA.Bfi / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    xo = -profilA.Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    xo = -profilA.Bfi / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    xo = -profilA.Bfi / 2
            End Select
            xe = EntraxeD2 + dCar
        End If
        AddLigne(MyGr, xo, 0, xe, 0, MyParAffA)
        AddLigne(MyGr, xo, Td, xe, Td, MyParAffA)

        If Not lIntermediaire Then AddLigne(MyGr, xo, 0, xo, Td, MyParAffA)


        '--> Fin

        MyPen.Dispose()
    End Sub

    Private Sub DessinLitArmaDalleFeuSlim(ByRef myGr As Graphics, myDalle As cls_Dalle, myParAffA As Struc_Affichage,
                                          myBrushA As Brush, xPro As Decimal, tW As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation des barres d'armatures pour la résistance au feu des slims floors - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   myDalle     [E] :   Dalle
        '   myParAffA   [E] :   Paramètres d'affichage
        '   myBrushA    [E] :   Pinceau pour les armatures
        '   xPro        [E] :   Position du profilé
        '   tW          [E] :   epaisseur de l'âme
        '---------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim kCote() As Decimal = {1, -1}

        Dim xRef As Decimal = tW / 2

        '--( Dessin des barres

        With myDalle.ArmaSlimFeu
            For iBarre As Integer = 1 To myDalle.ArmaSlimFeu.NbBarres

                For jCote As Integer = 0 To 1

                    AddCerclePlein(myGr, myBrushA, xPro + kCote(jCote) * (xRef + .xPos + (iBarre - 1) * .Diametre), .zPos,
                                   .Diametre, myParAffA, True)

                Next

            Next
        End With

    End Sub

    Private Sub DessinLitArmaDalle_Frm_Main(ByRef MyGr As Graphics, MyDalle As cls_Dalle, iArma As Integer,
                                            Ha As Decimal, MyParAffA As Struc_Affichage, MyBrushArma As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean,
                                            Optional EntraxeD1 As Decimal = 0, Optional dCar As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'un lit d'armatures - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   Dalle
        '   BeffRed     [E] :   Largeur de dalle représentée à l'écran
        '   iArma       [E] :   Indice du lit d'armature
        '   Ha          [E] :   Hauteur du profilé
        '   iSelect     [E] :   Indice de la cote sélectionnée
        '   MyParAffA   [E] :   Paramètres d'affichage
        '   MyBrishArma [E] :   Pinceau
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] : Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '   xBone       [S] :   
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Beff, Td, Th As Decimal
        Dim PhiS, EspBar, Zs As Decimal
        Dim nbBar As Integer
        Dim xc, yc As Single
        Dim zTop As Decimal

        Dim MyPen As New Pen(Color.Black, 1)

        Dim lContour As Boolean = lCONTOURCOTE
        Dim MyFontNormal As Font = FontBase

        Dim xLeft As Decimal

        '--> Initialisation

        Td = MyDalle.Ep_td
        PhiS = MyDalle.LitArma(iArma).PhiS
        Zs = MyDalle.LitArma(iArma).z_s
        EspBar = MyDalle.LitArma(iArma).EspBar
        Th = MyDalle.EpRenformis
        zTop = MyDalle.zTop

        If lIntermediaire Then
            Beff = EntraxeD1 + EntraxeD2 + 2 * dCar
            xLeft = -Math.Floor((EntraxeD1 + dCar) / EspBar) * EspBar - EspBar / 2

        Else
            Beff = EntraxeD1 + EntraxeD2 + dCar
            xLeft = -Math.Floor(EntraxeD1 / EspBar) * EspBar + EspBar / 2
        End If

        '--> Dessin

        If MyDalle.LitArma(iArma).lActive Then

            nbBar = Math.Floor((Beff) / (EspBar))

            For i As Integer = 1 To nbBar

                xc = xLeft + (i - 1) * EspBar

                yc = zTop - Zs


                If Not (i = 1 And Not lIntermediaire And Math.Abs(EntraxeD1 + xc) <= PhiS) Then
                    AddCerclePlein(MyGr, MyBrushArma, xc, yc, PhiS, MyParAffA, True)
                End If



            Next

        End If

    End Sub

    Private Sub DessineDalleMixteParallele_Frm_Main(ByRef MyGr As Graphics, MyDalle As cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                                    MyBrushDP As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean,
                                                    Optional EntraxeD1 As Decimal = 0, Optional dCar As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervures parallèles à la poutre - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics
        '   MyDalle         [E] :   
        '   Ha              [E] :   Hauteur du profilé métallique
        '   Bfs             [E] :   Largeur de la semelle supérieure
        '   MyParAffA       [E] :   Paramètres d'affichage   
        '   MyBrushDP       [E] :   Pinceau pour le remplissage de la dalle
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] :   Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        '--> Contour

        PrepareContourDalleMixteParallel_Frm_MainN(MyDalle, Bfs, xPts, yPts, nbPts, EntraxeD2, lIntermediaire, EntraxeD1, dCar)

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, True, True)

    End Sub

    Private Sub PrepareContourDalleMixteParallel_Frm_MainN(ByVal myDalle As cls_Dalle, Bfs As Decimal,
                                                           ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer,
                                                           EntraxeD2 As Decimal, lIntermediaire As Boolean,
                                                           EntraxeD1 As Decimal, dCar As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   19/12/24 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle mixte/ nervures parallèles - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle         [E] :   Classe dalle
        '   Bfs             [E] :   Largeur de la semelle sup
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] :   Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '   xPts, yPts      [S] :   Coordonnées de points définissant le contour
        '   nbPts           [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim AxePos(1) As Decimal
        Dim wExtMin(1) As Decimal
        Dim wExt() As Decimal = Nothing
        Dim zTop As Decimal = myDalle.zTop
        Dim xGauche As Decimal

        '--( Initialisation

        nbPts = 0

        '--( Préparation des points pour la partie entre la poutre traitée et la poutre à droite

        'AxePos(0) = 0
        'AxePos(1) = EntraxeD2
        wExtMin = {0, 0}
        AxePos = {0, EntraxeD2}

        AjoutePointsCoutourDalleMixteParallelEntreAxes(myDalle.Bac, AxePos, wExtMin, wExt, xPts, yPts, nbPts)

        '--( Travée suivante à droite

        AxePos = {EntraxeD2, EntraxeD2 + dCar}
        wExtMin = {wExt(1), wExt(1)}

        AjoutePointsCoutourDalleMixteParallelEntreAxes(myDalle.Bac, AxePos, wExtMin, wExt, xPts, yPts, nbPts)

        '--( Bouclage de la dalle par le haut

        AjoutePoint(EntraxeD2 + dCar, zTop, xPts, yPts, nbPts)

        If lIntermediaire Then
            xGauche = -EntraxeD1 - dCar
        Else
            xGauche = -EntraxeD1
        End If

        AjoutePoint(xGauche, zTop, xPts, yPts, nbPts)

        '--( Travée à gauche de la solive gauche si poutre intermédiaire

        If lIntermediaire Then
            AxePos = {-EntraxeD1 - dCar, -EntraxeD1}
            wExtMin = {0, 0}
            AjoutePointsCoutourDalleMixteParallelEntreAxes(myDalle.Bac, AxePos, wExtMin, wExt, xPts, yPts, nbPts)

        End If

        '--( Travée gauche

        AxePos = {-EntraxeD1, 0}
        wExtMin = {0, 0}
        AjoutePointsCoutourDalleMixteParallelEntreAxes(myDalle.Bac, AxePos, wExtMin, wExt, xPts, yPts, nbPts)

    End Sub

    Private Sub AjoutePointsCoutourDalleMixteParallelEntreAxes(myBac As cls_Bac,
                                                               AxePos() As Decimal, wExtMin() As Decimal, ByRef wExt() As Decimal,
                                                               ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   19/12/24 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Ajouts des points définissant le contour d'une dalle mixte/ nervures parallèles entre deux axes
        '---------------------------------------------------------------------------------------------------------------------------
        '   myBac           [E] :   Paramètres du bac
        '   AxePos          [E] :   Positions des 2 axes entre lesquels on dessine les points
        '   wExtMin         [E] :   Largeur mini de la demi-onde aux extremités à gauche et à droite
        '   wExt            [S] :   Largeur de la demi onde aux extremités à gauche et à droite
        '   xPts, yPts      [S] :   Coordonnées de points définissant le contour
        '   nbPts           [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Bb As Decimal = myBac.Bb
        Dim Bt As Decimal = myBac.Bt
        Dim Ep As Decimal = myBac.Ep
        Dim Hp As Decimal = myBac.Hp
        Dim Hrs As Decimal = myBac.h_rs
        Dim Br As Decimal = Ep - Bt
        Dim BrB As Decimal = Ep - Bb

        Dim Distance, dN As Decimal
        Dim xDecal(1) As Decimal
        Dim NbOndes As Integer
        Dim wAdd, DeltaB As Decimal
        Dim x0 As Decimal
        Dim lRaid As Boolean = Not IsEqual(Hrs, 0)
        ReDim wExt(1)
        Dim BbRaid, BtRaid, DeltaBraid As Decimal
        Dim xCentre As Decimal

        '--( Préparation - calcul du nombre d'ondes

        xDecal(0) = Math.Max(0, wExtMin(0) - Bb / 2)
        xDecal(1) = Math.Max(0, wExtMin(1) - Bb / 2)
        Distance = AxePos(1) - AxePos(0) - xDecal(0) - xDecal(1)

        NbOndes = Math.Floor(Distance / Ep)

        dN = NbOndes * Ep
        wAdd = (Distance - dN) / 2

        x0 = AxePos(0) + wAdd + xDecal(0)
        DeltaB = (Bt - Bb) / 2
        wExt(0) = wAdd + xDecal(0) + Bb / 2
        wExt(1) = wAdd + xDecal(1) + Bb / 2

        If lRaid Then
            BbRaid = cls_Bac.RATIOB1R * Br
            BtRaid = cls_Bac.RATIOB2R * Br
            DeltaBraid = (Br - BbRaid) / 2
        End If

        '--( Ajout des points

        AjoutePoint(AxePos(0), 0, xPts, yPts, nbPts)

        For i As Integer = 0 To NbOndes - 1

            xCentre = x0 + (i + 0.5) * Ep

            AjoutePoint(xCentre - BrB / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCentre - Br / 2, Hp, xPts, yPts, nbPts)
            If lRaid Then
                AjoutePoint(xCentre - BbRaid / 2, Hp, xPts, yPts, nbPts)
                AjoutePoint(xCentre - BtRaid / 2, Hp + Hrs, xPts, yPts, nbPts)
                AjoutePoint(xCentre + BtRaid / 2, Hp + Hrs, xPts, yPts, nbPts)
                AjoutePoint(xCentre + BbRaid / 2, Hp, xPts, yPts, nbPts)
            End If
            AjoutePoint(xCentre + Br / 2, Hp, xPts, yPts, nbPts)
            AjoutePoint(xCentre + BrB / 2, 0, xPts, yPts, nbPts)

        Next

        AjoutePoint(AxePos(1), 0, xPts, yPts, nbPts)
    End Sub

    Private Sub PrepareContourDalleMixteParallel_Frm_Main(ByVal MyDalle As cls_Dalle, Bfs As Decimal,
                                                          ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer,
                                                          EntraxeD2 As Decimal, lIntermediaire As Boolean,
                                                          EntraxeD1 As Decimal, dCar As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une dalle mixte/ nervures parallèles - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyDalle         [E] :   Classe dalle
        '   Bfs             [E] :   Largeur de la semelle sup
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] :   Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '   xPts, yPts      [S] :   Coordonnées de points définissant le contour
        '   nbPts           [S] :   Nombre de points dans le contour
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPtsG() As Single = Nothing
        Dim yPtsG() As Single = Nothing
        Dim nbPtsG As Integer
        Dim xo, yo As Single
        Dim decalBac As Decimal

        '--> Initialisaiton

        nbPts = 0

        Dim BeffG As Decimal
        Dim BeffD As Decimal

        If lIntermediaire Then
            BeffG = EntraxeD1 + dCar
            BeffD = EntraxeD2 + dCar
        Else
            BeffG = EntraxeD1
            BeffD = EntraxeD2 + dCar
        End If

        decalBac = EntraxeD2 - Math.Floor(EntraxeD2 / MyDalle.Bac.Ep) * MyDalle.Bac.Ep

        '--> on commence le contour par le côté droit inférieur

        PrepareContourDalleMixteParalleInfDroite(MyDalle, Bfs, BeffD, False, xPts, yPts, nbPts, decalBac)

        '--> Partie supérieure de la dalle

        xo = BeffD
        yo = MyDalle.Ep_td
        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -BeffG
        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Partie gauche de la dalle

        If lIntermediaire Then

        Else

        End If

        PrepareContourDalleMixteParalleInfDroite(MyDalle, Bfs, BeffG, True, xPtsG, yPtsG, nbPtsG, decalBac)

        For i = nbPtsG - 1 To 0 Step -1

            AjoutePoint(-xPtsG(i), yPtsG(i), xPts, yPts, nbPts)

        Next

    End Sub

    Private Sub DessineDalleMixtePerpendiculaire_Frm_Main(
                       ByRef myGr As Graphics, MyDalle As cls_Dalle, Ha As Decimal, Bfs As Decimal,
                       myParAffA As Struc_Affichage, myBrushDP As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean,
                       mySection As cls_Section, LargeurDalleDispo As Decimal, EntraxeD1 As Decimal, dCar As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervures perpendiculaires à la poutre pour le Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics
        '   MyDalle         [E] :   
        '   Ha              [E] :   Hauteur du profilé métallique
        '   Bfs             [E] :   Largeur de la semelle supérieure
        '   MyParAffA       [E] :   Paramètres d'affichage   
        '   MyBrushDP       [E] :   Pinceau pour le remplissage de la dalle
        '   EntraxeD1       [E] :   EntraxeD1 avec la poutre/bord gauche
        '   EntraxeD2       [E] :   EntraxeD2 avec la poutre de droite
        '   dCar            [E] :   Grandeur utilisée pour faire déborder le dessin de la dalle à gauche et à droite de cette valeur
        '   lIntermediare   [E] :   Indique si la poutre est une poutre intermédiare (True) ou non (False)
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim Td As Decimal = MyDalle.Ep_td
        Dim Hp As Decimal = MyDalle.Bac.Hp
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim wApp As Decimal = MyDalle.Bac.wAppui
        Dim LargeurProfilA As Decimal
        Dim BeffDes As Decimal = LargeurDalleDispo
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim lSlimF As Boolean = mySection.lSlimFloor

        '--> Initialisation 

        Select Case mySection.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                LargeurProfilA = mySection.ProfilA.Bfs
                xo = -EntraxeD1
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                LargeurProfilA = mySection.ProfilA.Plat_b
                wApp = Math.Min(wApp, 0.7 * (mySection.ProfilA.Plat_b - mySection.ProfilA.Bfi) / 2)
                xo = -mySection.ProfilA.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                LargeurProfilA = mySection.ProfilA.Plat_b
                xo = -mySection.ProfilA.Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                LargeurProfilA = mySection.ProfilA.Bfi
                xo = -mySection.ProfilA.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                LargeurProfilA = mySection.ProfilA.Bfi
                xo = -mySection.ProfilA.Bfi / 2
        End Select

        If lIntermediaire Then xo = -EntraxeD1 - dCar

        xe = EntraxeD2 + dCar
        yo = 0
        ye = Td

        '--> Dessins

        '# béton

        'PrepareContourDallePleine_Frm_Main(MyDalle, mySection, BeffDes, Bfs, xPts, yPts, nbPts, EntraxeD1, EntraxeD2, lIntermediaire, dCar)

        AddRectanglePlein(myGr, myBrushDP, MyPenContour, xo, 0, xe, Td, myParAffA, True, False)
        'RemplirZone(myGr, myBrushDP, xPts, yPts, nbPts, myParAffA, False)

        '# Traits dalle

        AddLigne(myGr, MyPenContour, xo, yo, xe, yo, myParAffA)
        AddLigne(myGr, MyPenContour, xo, ye, xe, ye, myParAffA)

        '# Représentation des traits pour le bac

        Select Case MyDalle.Bac.AppuiT
            Case cls_Bac.EnuConfigTAppui.Discontinu

                yo = 0
                ye = Hp

                If lIntermediaire Then
                    xo = -EntraxeD1 - dCar + LargeurProfilA / 2 - wApp
                    xe = -dCar - LargeurProfilA / 2 + wApp

                    AddLigne(myGr, MyPenContour, xo, ye, xe, ye, myParAffA)
                    AddLigne(myGr, MyPenContour, xo, 0, xo, ye, myParAffA)
                    AddLigne(myGr, MyPenContour, xe, 0, xe, ye, myParAffA)

                    xo = -EntraxeD1 + LargeurProfilA / 2 - wApp
                    xe = -LargeurProfilA / 2 + wApp

                    AddLigne(myGr, MyPenContour, xo, ye, xe, ye, myParAffA)
                    AddLigne(myGr, MyPenContour, xo, 0, xo, ye, myParAffA)
                    AddLigne(myGr, MyPenContour, xe, 0, xe, ye, myParAffA)

                Else

                    If Not lSlimF Then
                        xo = -EntraxeD1
                        xe = -LargeurProfilA / 2 + wApp

                        AddLigne(myGr, MyPenContour, xo, ye, xe, ye, myParAffA)
                        AddLigne(myGr, MyPenContour, xo, 0, xo, Td, myParAffA)
                        AddLigne(myGr, MyPenContour, xe, 0, xe, ye, myParAffA)

                    Else
                        AddLigne(myGr, MyPenContour, xo, 0, xo, Td, myParAffA)
                    End If

                End If

                xo = LargeurProfilA / 2 - wApp
                xe = EntraxeD2 - LargeurProfilA / 2 + wApp
                AddLigne(myGr, MyPenContour, xo, ye, xe, ye, myParAffA)
                AddLigne(myGr, MyPenContour, xo, 0, xo, ye, myParAffA)
                AddLigne(myGr, MyPenContour, xe, 0, xe, ye, myParAffA)

                xo = EntraxeD2 + LargeurProfilA / 2 - wApp
                xe = EntraxeD2 + dCar - LargeurProfilA / 2 + wApp
                AddLigne(myGr, MyPenContour, xo, ye, xe, ye, myParAffA)
                AddLigne(myGr, MyPenContour, xo, 0, xo, ye, myParAffA)
                AddLigne(myGr, MyPenContour, xe, 0, xe, ye, myParAffA)


            Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                AddLigne(myGr, MyPenContour, xo, Hp, xe, Hp, myParAffA)
                If Not lIntermediaire Then AddLigne(myGr, MyPenContour, -EntraxeD1, 0, -EntraxeD1, Td, myParAffA)

            Case cls_Bac.EnuConfigTAppui.BetonSeulContinu
                AddLigne(myGr, MyPenContour, xo, Hp, xe, Hp, myParAffA)
                AddLigne(myGr, MyPenContour, 0, 0, 0, Hp, myParAffA)
                If lIntermediaire Then
                    AddLigne(myGr, MyPenContour, -EntraxeD1, 0, -EntraxeD1, Hp, myParAffA)
                Else
                    AddLigne(myGr, MyPenContour, -EntraxeD1, 0, -EntraxeD1, Td, myParAffA)
                End If
                AddLigne(myGr, MyPenContour, EntraxeD2, 0, EntraxeD2, Hp, myParAffA)

        End Select

        '--> Fin

        MyPenContour.Dispose()
    End Sub

    Private Sub DessineDalleMixtePerpendiculaireCfp220_Frm_Main(ByRef MyGr As Graphics, MyPoutre As cls_Poutre, MyParAffA As Struc_Affichage,
                                                       MyBrushDP As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean,
                                                   Optional EntraxeD1 As Decimal = 0, Optional dCar As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   10/11/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervures perpendiculaires à la poutre - Cas particulier bac Cofraplus 220 - Frm_Main
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   MyProfil    [E] :   Géométrie du rofilé métallique
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   BeffRed     [E] :   Largeur de dalle réduite pour le dessin 
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPenContour As New Pen(Color.Black, 1)

        Dim Hp As Decimal = MyPoutre.Dalle.Bac.Hp
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim Bfs As Decimal = MyPoutre.Section.ProfilA.Bfs
        Dim zTop As Decimal = MyPoutre.Dalle.zTop
        Dim xo, xe As Decimal
        Dim wApp As Decimal = MyPoutre.Dalle.Bac.wAppui

        '--> Préparation du contour de la dalle

        nbPts = 0
        AjoutePoint(-Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(Bfs / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(EntraxeD2 - Bfs / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(EntraxeD2 - Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(EntraxeD2 + Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(EntraxeD2 + Bfs / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(EntraxeD2 + dCar, -Hp, xPts, yPts, nbPts)
        AjoutePoint(EntraxeD2 + dCar, zTop, xPts, yPts, nbPts)
        If lIntermediaire Then
            AjoutePoint(-EntraxeD1 - dCar, zTop, xPts, yPts, nbPts)
            AjoutePoint(-EntraxeD1 - dCar, -Hp, xPts, yPts, nbPts)
            AjoutePoint(-EntraxeD1 - Bfs / 2, -Hp, xPts, yPts, nbPts)
            AjoutePoint(-EntraxeD1 - Bfs / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(-EntraxeD1 + Bfs / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(-EntraxeD1 + Bfs / 2, -Hp, xPts, yPts, nbPts)

        Else
            AjoutePoint(-EntraxeD1, zTop, xPts, yPts, nbPts)
            AjoutePoint(-EntraxeD1, -Hp, xPts, yPts, nbPts)
        End If

        AjoutePoint(-Bfs / 2, -Hp, xPts, yPts, nbPts)
        '--> Dessin de la dalle

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, False, True)

        If lIntermediaire Then
            xo = -EntraxeD1 - dCar
        Else
            xo = -EntraxeD1
        End If
        xe = EntraxeD2 + dCar
        AddLigne(MyGr, MyPenContour, xo, zTop, xe, zTop, MyParAffA)
        AddLigne(MyGr, MyPenContour, xo, 0, xe, 0, MyParAffA)

        If lIntermediaire Then

            xo = -EntraxeD1 - dCar
            xe = -EntraxeD1 - Bfs / 2
            AddLigne(MyGr, MyPenContour, xo, -Hp, xe, -Hp, MyParAffA)
            AddLigne(MyGr, MyPenContour, xo, -Hp, xo, 0, MyParAffA)
            AddLigne(MyGr, MyPenContour, xe, -Hp, xe, 0, MyParAffA)

            xo = -EntraxeD1 + Bfs / 2
            xe = -Bfs / 2
            AddLigne(MyGr, MyPenContour, xo, -Hp, xe, -Hp, MyParAffA)
            AddLigne(MyGr, MyPenContour, xo, -Hp, xo, 0, MyParAffA)
            AddLigne(MyGr, MyPenContour, xe, -Hp, xe, 0, MyParAffA)
        Else
            xo = -EntraxeD1
            xe = -Bfs / 2
            AddLigne(MyGr, MyPenContour, xo, -Hp, xe, -Hp, MyParAffA)
            AddLigne(MyGr, MyPenContour, xo, -Hp, xo, zTop, MyParAffA)
            AddLigne(MyGr, MyPenContour, xe, -Hp, xe, 0, MyParAffA)
        End If

        xo = Bfs / 2
        xe = EntraxeD2 - Bfs / 2
        AddLigne(MyGr, MyPenContour, xo, -Hp, xe, -Hp, MyParAffA)
        AddLigne(MyGr, MyPenContour, xo, -Hp, xo, 0, MyParAffA)
        AddLigne(MyGr, MyPenContour, xe, -Hp, xe, 0, MyParAffA)

        xo = EntraxeD2 + Bfs / 2
        xe = EntraxeD2 + dCar
        AddLigne(MyGr, MyPenContour, xo, -Hp, xe, -Hp, MyParAffA)
        AddLigne(MyGr, MyPenContour, xo, -Hp, xo, 0, MyParAffA)
        AddLigne(MyGr, MyPenContour, xe, -Hp, xe, 0, MyParAffA)

        ''--> Dessin du bac

        'AddLigne(MyGr, MyPenContour, -xo, 0, -xe, 0, MyParAffA)
        'AddLigne(MyGr, MyPenContour, xo, 0, xe, 0, MyParAffA)

    End Sub

    Public Sub DessinConnecteurs_Frm_Main(ByRef myGr As Graphics, myBeam As cls_Poutre, myParAffA As Struc_Affichage,
                                          MyBrushConnecteur As Brush, EntraxeD2 As Decimal, lIntermediaire As Boolean,
                                          Optional EntraxeD1 As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------

        '   myBeam      [E] :   Poutre locale        '   
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim lGoujonV, lGoujonH, lArma As Boolean
        Dim lMixte As Boolean
        Dim lSlim As Boolean = myBeam.lSlimFloor

        '--( Initialisation

        lMixte = myBeam.lMixte
        lGoujonV = (lSlim And myBeam.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup) Or Not lSlim
        lGoujonH = (lSlim And myBeam.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme)
        lArma = (lSlim And myBeam.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.ArmatureAme)

        '--> Dessin des goujons verticaux

        If lGoujonV Then
            Dessin_FrmMain_GoujonsSemSup(myGr, myBeam, MyBrushConnecteur, myParAffA, EntraxeD1, EntraxeD2)
        ElseIf lGoujonH Then
            Dessin_FrmMain_GoujonsAme(myGr, myBeam, MyBrushConnecteur, myParAffA, EntraxeD1, EntraxeD2)
        ElseIf lArma Then
            Dessin_FrmMain_ConnectionArma(myGr, myBeam, MyBrushConnecteur, myParAffA, EntraxeD1, EntraxeD2)
        End If

    End Sub

    Private Sub Dessin_FrmMain_ConnectionArma(ByRef myGr As Graphics, myBeam As cls_Poutre, myBrushS As Brush,
                                              myParAff As Struc_Affichage, EntraxeD1 As Decimal, EntraxeD2 As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   25/07/25 :  Version 1.20 - Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des connections par armature, pour les slim floor, dans la fenêtre principale
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine les goujons
        '   myBrushS    [E] :   Pinceau pour le goujon
        '   myParAff    [E] :   Paramètres d'affichage
        '   EntraxeDi   [E] :   Entraxes des poutres à gauche (1) et à droite (2)
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim PhiS As Decimal
        Dim aHv As Decimal          ' Distance de l'axe des armatures à la sousface des semelles sup
        Dim Ls As Decimal           ' Longeur des armatures
        Dim tw As Decimal
        Dim zTop As Decimal
        Const kArma As Decimal = 0.8

        '--( Initialisation

        PhiS = myBeam.Dalle.ConnecteurArmature.ds
        aHv = myBeam.Section.EpSemSupPlusCongesSup * 1.2 + PhiS / 2
        Ls = (EntraxeD1 + EntraxeD2) / 2
        tw = myBeam.Section.ProfilA.Tw
        zTop = myBeam.Section.zSemSup

        '--( Dessin mySection à gauche

        If myBeam.lIntermediaire Then
            Dessin_ArmaConnec(myGr, myBeam.Dalle.ConnecteurArmature, -EntraxeD1 - tw / 2, zTop - aHv, kArma * Ls / 2, myBrushS, myParAff, True)
            Dessin_ArmaConnec(myGr, myBeam.Dalle.ConnecteurArmature, -EntraxeD1 + tw / 2, zTop - aHv, kArma * EntraxeD1 / 2, myBrushS, myParAff, False)
        End If

        Dessin_ArmaConnec(myGr, myBeam.Dalle.ConnecteurArmature, -tw / 2, zTop - aHv, kArma * EntraxeD1 / 2, myBrushS, myParAff, True)
        Dessin_ArmaConnec(myGr, myBeam.Dalle.ConnecteurArmature, +tw / 2, zTop - aHv, kArma * EntraxeD2 / 2, myBrushS, myParAff, False)

        Dessin_ArmaConnec(myGr, myBeam.Dalle.ConnecteurArmature, EntraxeD2 - tw / 2, zTop - aHv, kArma * EntraxeD2 / 2, myBrushS, myParAff, True)
        Dessin_ArmaConnec(myGr, myBeam.Dalle.ConnecteurArmature, EntraxeD2 + tw / 2, zTop - aHv, kArma * Ls / 2, myBrushS, myParAff, False)

    End Sub

    Private Sub Dessin_FrmMain_GoujonsAme(ByRef myGr As Graphics, myBeam As cls_Poutre, myBrushS As Brush,
                                          myParAff As Struc_Affichage, EntraxeD1 As Decimal, EntraxeD2 As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   25/07/25 :  Version 1.20 - Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des goujons sur l'âme, pour les slim floor, dans la fenêtre principale
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine les goujons
        '   myBrushS    [E] :   Pinceau pour le goujon
        '   myParAff    [E] :   Paramètres d'affichage
        '   EntraxeDi   [E] :   Entraxes des poutres à gauche (1) et à droite (2)
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim zGoujon As Decimal
        Dim tw As Decimal

        '--( Initialisation

        zGoujon = myBeam.Section.ProfilA.ha / 2
        tw = myBeam.Section.ProfilA.Tw

        '--( Dessin mySection à gauche

        If myBeam.lIntermediaire Then
            Dessin_GoujonHorizontal(myGr, myBeam.Dalle.Goujons, -EntraxeD1 - tw / 2, zGoujon, myBrushS, myParAff, True)
            Dessin_GoujonHorizontal(myGr, myBeam.Dalle.Goujons, -EntraxeD1 + tw / 2, zGoujon, myBrushS, myParAff, False)
        End If

        '--( Dessin mySection principale

        Dessin_GoujonHorizontal(myGr, myBeam.Dalle.Goujons, -tw / 2, zGoujon, myBrushS, myParAff, True)
        Dessin_GoujonHorizontal(myGr, myBeam.Dalle.Goujons, tw / 2, zGoujon, myBrushS, myParAff, False)

        '--( Dessin Section à droite

        Dessin_GoujonHorizontal(myGr, myBeam.Dalle.Goujons, EntraxeD2 - tw / 2, zGoujon, myBrushS, myParAff, True)
        Dessin_GoujonHorizontal(myGr, myBeam.Dalle.Goujons, EntraxeD2 + tw / 2, zGoujon, myBrushS, myParAff, False)

    End Sub

    Private Sub Dessin_FrmMain_GoujonsSemSup(ByRef myGr As Graphics, myBeam As cls_Poutre, myBrushS As Brush,
                                             myParAff As Struc_Affichage, EntraxeD1 As Decimal, EntraxeD2 As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   25/07/25 :  Version 1.20 - Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin des goujons verticaux dans la fenêtre principale
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine les goujons
        '   myBrushS    [E] :   Pinceau pour le goujon
        '   myParAff    [E] :   Paramètres d'affichage
        '   EntraxeDi   [E] :   Entraxes des poutres à gauche (1) et à droite (2)
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim zGoujon As Decimal
        Dim lSlim As Boolean = myBeam.lSlimFloor

        '--( Initialisation

        If lSlim Then
            zGoujon = myBeam.Section.zSemSup
        Else
            zGoujon = 0
        End If

        '--( Dessin mySection à gauche

        If myBeam.lIntermediaire Then

            Dessin_GoujonVertical(myGr, myBeam.Dalle.Goujons, -EntraxeD1, zGoujon, myBrushS, myParAff)

        End If

        '--( Connecteur mySection principale

        Dessin_GoujonVertical(myGr, myBeam.Dalle.Goujons, 0, zGoujon, myBrushS, myParAff)

        '--( Connecteur mySection à droite

        Dessin_GoujonVertical(myGr, myBeam.Dalle.Goujons, EntraxeD2, zGoujon, myBrushS, myParAff)

    End Sub

    Private Sub Dessin_ArmaConnec(ByRef myGr As Graphics, myStud As Cls_ConnecteurArmature,
                                  xArma As Decimal, zArma As Decimal, LArma As Decimal,
                                  myBrushS As Brush, myParAff As Struc_Affichage, lGauche As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   25/07/25 :  Version 1.20 - Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'une armature utilisée comme connecteur
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myStud      [E] :   Goujon à dessiner
        '   xStud,zStud [E] :   Position de la base du goujon
        '   myBrushS    [E] :   Pinceau pour le goujon
        '   myParAff    [E] :   Paramètres d'affichage
        '   lGauche     [E] :   Indique si la tête est à gauche ou à droite du dessin
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations 

        Dim kGauche As Decimal = 1
        Dim PhiS As Decimal

        If lGauche Then kGauche = -1
        PhiS = myStud.ds

        '--( Dessin

        AddRectanglePlein(myGr, myBrushS, MyPenContour, xArma, zArma - PhiS / 2,
                                                        xArma + kGauche * LArma, zArma + PhiS / 2, myParAff, True, False)

        AddLigne(myGr, MyPenContour, xArma, zArma - PhiS / 2, xArma + kGauche * LArma, zArma - PhiS / 2, myParAff)
        AddLigne(myGr, MyPenContour, xArma, zArma + PhiS / 2, xArma + kGauche * LArma, zArma + PhiS / 2, myParAff)

    End Sub

    Private Sub Dessin_GoujonHorizontal(ByRef myGr As Graphics, myStud As cls_GoujonSoude, xStud As Decimal, zStud As Decimal,
                                        myBrushS As Brush, myParAff As Struc_Affichage, lGauche As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   25/07/25 :  Version 1.20 - Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'un goujon soudé sur l'âme (donc horizontal)
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myStud      [E] :   Goujon à dessiner
        '   xStud,zStud [E] :   Position de la base du goujon
        '   myBrushS    [E] :   Pinceau pour le goujon
        '   myParAff    [E] :   Paramètres d'affichage
        '   lGauche     [E] :   Indique si la tête est à gauche ou à droite du dessin
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations 

        Dim dTete, hTete As Decimal
        Dim kGauche As Decimal = 1

        If lGauche Then kGauche = -1

        '--( Dessin du corps du goujon

        AddRectanglePlein(myGr, myBrushS, MyPenContour, xStud, zStud - myStud.d / 2,
                                                        xStud + kGauche * myStud.hsc, zStud + myStud.d / 2, myParAff, True, True)

        '--( Dessin de la tete du goujon
        myStud.DimensionsTete(dTete, hTete)
        AddRectanglePlein(myGr, myBrushS, MyPenContour, xStud + kGauche * (myStud.hsc - hTete), zStud - dTete / 2,
                                                        xStud + kGauche * myStud.hsc, zStud + dTete / 2, myParAff, True, True)
    End Sub

    Private Sub Dessin_GoujonVertical(ByRef myGr As Graphics, myStud As cls_GoujonSoude, xStud As Decimal, zStud As Decimal,
                                      myBrushS As Brush, myParAff As Struc_Affichage,
                                      Optional lSup As Boolean = True,
                                      Optional kEch As Decimal = 1)
        '-----------------------------------------------------------------------------------------------
        '   25/07/25 :  Version 1.20 - Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'un goujon soudé vertical
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myStud      [E] :   Goujon à dessiner
        '   xStud,zStud [E] :   Position de la base du goujon
        '   myBrushS    [E] :   Pinceau pour le goujon
        '   myParAff    [E] :   Paramètres d'affichage
        '   lSup        [E] :   Indique si goujon au dessus
        '   kEch        [E] :   Coefficient d'échelle pour le dessin
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations 

        Dim dTete, hTete As Decimal
        Dim kTop As Decimal = 1

        If Not lSup Then kTop = -1

        '--( Dessin du corps du goujon
        AddRectanglePlein(myGr, myBrushS, MyPenContour, xStud - myStud.d / 2, zStud,
                                                        xStud + myStud.d / 2, zStud + kEch * kTop * myStud.hsc, myParAff, True, True)

        '--( Dessin de la tete du goujon
        myStud.DimensionsTete(dTete, hTete)
        AddRectanglePlein(myGr, myBrushS, MyPenContour, xStud - dTete / 2, zStud + kEch * kTop * (myStud.hsc - hTete),
                                                        xStud + dTete / 2, zStud + kEch * kTop * myStud.hsc, myParAff, True, True)


    End Sub


#End Region

#Region " Dessins pour la définiton de la dalle (FRM_DALLEN) "

    Public Sub DessineBacTout(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyBac As cls_Bac,
                              ByVal lTitre As Boolean, kAdjust As Decimal,
                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '   EpDalle     [E] :   Epaisseur de la dalle béton
        '   VariableBac [E] :   Parametre du bac sélectionné (pour affichage en rouge)
        '   nbOndes     [E] :   Nombre d'ondes sur lequel on représente le bac
        '   lCotation   [E] :   Indique si on met les cotations sur le dessin
        '   lCotEpTot   [E] :   Indique si cotation epaisseur bac+dalle
        '   lTitre      [E] :   Indique si affichage du titre du bac
        '   ParAff      [S] :   Paramètres d'AffichageOptFeu
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
        Dim dCar As Double

        Dim lRaidSup As Boolean

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim nbOndes As Integer = 5

        Dim MyPenBac As New Pen(BleuCTICM, 2)

        '--> Initialisation

        lRaidSup = MyBac.HasRaidisseurSup

        dCar = (MyBac.Ep + MyBac.Bb) / 2

        '--> Preparation de la zone d'affichage - Calcul de ParAff


        xMin = 0
        xMax = MyBac.LargeurModule

        yMin = 0
        yMax = MyBac.Hp

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Calcul des points du pourtour de la dalle

        If lRaidSup Then
            MyBac.PrepareContourModuleBacRaidi(xPts, yPts, nbPts)
        Else
            MyBac.PrepareContourModuleBacSimple(xPts, yPts, nbPts)
        End If

        '--> Remplissage contour

        'ContourZone(myGr, New Pen(BlueAM), xPts, yPts, nbPts, MyParAff, True)

        ContourZone(myGr, MyPenBac, xPts, yPts, nbPts, MyParAff, True)

        ''--> Liberation des Font, Pen et Brush

        MyPenBac.Dispose()

    End Sub

    Public Sub DessineDalleFrmDalleSlimFloor _
                           (ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, myBeam As cls_Poutre, myFont As Font,
                            lIntermediaire As Boolean, iSelect As Integer, strMsg() As String, ByVal lCote As Boolean,
                            ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/25 :  Version 1.20
        '-----------------------------------------------------------------------------------------------
        '   Dessin de la dalle dans la fenêtre Frm_DalleSlimFloorN
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre dont la dalle est à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   strMsg      [E] :   Messages issus du fichier langue
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double
        Dim lMixte, lLamine As Boolean
        Dim Beff As Decimal
        Dim BeffG, BeffD As Decimal

        Dim ColorLocalEtriers As Color = CouleurArmaNormal
        Dim ColorLocalArma(2) As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurAcier As Color = CouleurAcierNormal
        'Dim pColorLocalArma(2, 2) As Color
        Dim ColorArmatures(1) As Color
        Const kADJUST As Decimal = 0.95
        Dim zREF As Decimal = 0
        Dim Ha, Bfs As Decimal
        'Dim lCote As Boolean = True
        Dim lCofraplus220 As Boolean
        Dim LargeurProfil As Decimal

        '--> Initialisation

        lMixte = myBeam.Section.lMixte
        lLamine = True
        lCofraplus220 = myBeam.Dalle.Bac.lCofraplus220

        Beff = LargeurDalleDessin(myBeam.Section.ProfilA)
        BeffG = Beff / 2
        BeffD = Beff / 2

        Ha = myBeam.Section.ProfilA.ha

        If lIntermediaire Or Not myBeam.Section.lSlimFloor Then
            Bfs = myBeam.Section.ProfilA.Bfs
            LargeurProfil = Bfs
        Else
            Select Case myBeam.Section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                    Bfs = myBeam.Section.ProfilA.Bfs
                    LargeurProfil = Bfs
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    Bfs = myBeam.Section.ProfilA.Bfs
                    LargeurProfil = myBeam.Section.ProfilA.Plat_b - Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    Bfs = myBeam.Section.ProfilA.Bfs
                    LargeurProfil = myBeam.Section.ProfilA.Plat_b - Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    Bfs = myBeam.Section.ProfilA.Bfi
                    LargeurProfil = Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    Bfs = myBeam.Section.ProfilA.Bfi
                    LargeurProfil = Bfs / 2
            End Select
        End If

        '--> Preparation de la zone d'affichage - Calcul de ParAff
        dCar = Math.Sqrt(Beff ^ 2 + (Ha + myBeam.Dalle.zTop) ^ 2) / 10

        If lIntermediaire Or Not myBeam.Section.lSlimFloor Then
            xMin = -Beff / 2 - 2 * dCar
            xMax = -xMin
        Else
            xMin = -Bfs / 2 - 2 * dCar
            xMax = Beff / 2 + 2 * dCar
        End If

        If lIntermediaire Then
            yMin = -myBeam.Section.ProfilA.Plat_t
            yMax = myBeam.Dalle.zTop + dCar
        Else
            yMin = -myBeam.Section.ProfilA.Plat_t - 0.8 * dCar
            yMax = myBeam.Dalle.zTop + 0.8 * dCar
        End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Préparation des Pinceaux utilisés dans le dessin

        '* Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurAcier, CouleurAcier)
        '* Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        '* Béton prefabriqué
        Dim myBrushPref As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        '* Cofradal
        Dim myBrushCofra As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurCofradal, CouleurCofradal)

        '--> Dessin de la dalle

        '# Dalle béton

        DessineDalleDalle(myGr, myBeam, MyParAff, myBrushB, myBrushPref, myBrushCofra, Beff, Bfs)

        '# Armatures

        DessinArmaFeuDalle(myGr, myBeam, FontBase, strMsg, lIntermediaire, iSelect, MyParAff, dCar, CouleurArmaNormal, CouleurArmaSelect)

        '--> Dessin de la mySection acier

        Dim lDessinRive As Boolean = (Not lIntermediaire) And myBeam.Section.lSlimFloor

        DessinProfileMetal(myGr, myBeam.Section.ProfilA, myBrushP, MyParAff, zREF, lDessinRive, 0)

        '--( Cotes 

        If lCote Then DessineDalleFrmSlim_Cote(myGr, myBeam, myFont, strMsg, lIntermediaire, iSelect, MyParAff, BeffG, BeffD, dCar,
                                               CouleurArmaNormal, CouleurArmaSelect, CouleurBeton, CouleurCofradal)

        '--( 

        myBrushP.Dispose()
        myBrushB.Dispose()
        myBrushPref.Dispose()
        myBrushCofra.Dispose()

    End Sub

    Private Sub DessineDalleFrmSlim_Cote(ByRef MyGr As Graphics, myBeam As cls_Poutre, myFont As Font, strMsg() As String,
                                         lIntermediaire As Boolean, iSelect As Integer,
                                         myParAffA As Struc_Affichage,
                                         bEffG As Decimal, bEffD As Decimal, dCar As Decimal,
                                         ColorArma As Color, ColorArmaSel As Color, ColorB As Color, ColorPrefab As Color)
        '-----------------------------------------------------------------------------------------------
        '   08/08/25 :  Version 1.20
        '-----------------------------------------------------------------------------------------------
        '   Dessin des cotes pour la dalle des slmi floors
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont la dalle est à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   strMsg      [E] :   Messages d'information dans le fichier langue
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   MyParAffA   [E] :   Paramètres d'affichage
        '   bEffG       [E] :   Largeur de dalle représentée à gauche
        '   dCar        [E] :   Dimension pour l'affichage
        '   ColorArma   [E] :   Couleur pour les armatures
        '   ColorArmaSel[E] :   Couleur pour les armatures sélectionnées
        '   ColorB      [E] :   Couleur pour le béton
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Const SELECT_NO As Integer = -99
        Const SELECT_EPDALLED As Integer = 1
        Const SELECT_EPDALLEC As Integer = 2
        Const SELECT_EPPREDAL As Integer = 3
        Const SELECT_EPPREJNT As Integer = 4
        Const SELECT_EPPREFAB As Integer = 5
        Const SELECT_NOMPREFAB As Integer = 6

        Const SELECT_RFEUXPOS As Integer = 101
        Const SELECT_RFEUYPOS As Integer = 102
        Const SELECT_RFEUDIA As Integer = 103
        Const SELECT_RFEUNB As Integer = 104
        Const SELECT_BETON As Integer = 1000
        Const SELECT_RFEUFSK As Integer = 1001

        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Dim Chaine As String
        Dim lContour As Boolean = lCONTOURCOTE
        Dim xe, ye As Decimal
        Dim xo, yo As Decimal

        Dim xRef As Decimal = myBeam.Section.ProfilA.Tw / 2

        Dim xGaucheD As Decimal
        Dim xCoteZ As Decimal
        Dim kDir As Decimal

        '--( Initialisation

        If lIntermediaire Then
            xGaucheD = -bEffG
        Else
            xGaucheD = myBeam.Section.PositionXgaucheSlim(lIntermediaire)
        End If

        '--( Dimensions

        '** Epaisseur totale TD de la dalle

        '# Hauteur de la dalle 

        MyColor = StyleCouleur(iSelect, SELECT_EPDALLED)
        MyPen.Color = MyColor

        yo = 0
        ye = myBeam.Dalle.zTop
        xCoteZ = xGaucheD - dCar

        AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
        Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '** Epaisseur totale

        MyColor = StyleCouleur(iSelect, SELECT_NO)
        MyPen.Color = MyColor

        xCoteZ -= 0.5 * dCar
        yo = -myBeam.Section.EpPlatInf

        AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
        Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '    If Not lIntermediaire And myBeam.Section.lSlimFloor Then xCoteZ = LargeurProfil + 0.3 * dCar

        '** Traitement dalle mixte

        If myBeam.Dalle.lMixte Then

            '** Epaisseur TC au dessus du bac

            MyColor = StyleCouleur(iSelect, SELECT_EPDALLEC)
            MyPen.Color = MyColor

            If lIntermediaire Then
                xCoteZ = xGaucheD + 0.5 * dCar
            Else
                xCoteZ = bEffD - 0.5 * dCar
            End If

            yo = myBeam.Dalle.Bac.Hp
            ye = myBeam.Dalle.zTop

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Middle,
                         New SolidBrush(ColorB), MyPen, lContour)

            '** Epaisseur du bac

            MyColor = StyleCouleur(iSelect, SELECT_NO)
            MyPen.Color = MyColor

            ye = 0

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(myBeam.Dalle.Bac.Hp, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Middle,
                         New SolidBrush(ColorB), MyPen, lContour)

        End If

        '** Traitement plancher préfa

        If myBeam.Dalle.lPlancherPrefabriquee Then

            If lIntermediaire Then
                xCoteZ = xGaucheD + 0.5 * dCar
                kDir = +1
            Else
                xCoteZ = bEffD - 0.5 * dCar
                kDir = -1
            End If

            '** Epaisseur partie supérieure du béton

            MyColor = StyleCouleur(iSelect, SELECT_NO)
            MyPen.Color = MyColor

            yo = myBeam.Dalle.Cofradal.dp
            ye = myBeam.Dalle.zTop

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Middle,
                         New SolidBrush(ColorB), MyPen, lContour)

            '** Epaisseur partie préfabriquée

            MyColor = StyleCouleur(iSelect, SELECT_EPPREFAB)
            MyPen.Color = MyColor

            yo = myBeam.Dalle.Cofradal.dp
            ye = 0

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(myBeam.Dalle.Cofradal.dp, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Middle,
                         New SolidBrush(ColorPrefab), MyPen, lContour)

            '** Nom du plancher

            MyColor = StyleCouleur(iSelect, SELECT_NOMPREFAB)
            MyPen.Color = MyColor
            Dim wApp As Decimal = myBeam.Dalle.wAppuiPreDalle
            Dim wPlat As Decimal = myBeam.Section.LargeurPlatInfSlim
            Dim dDecal As Decimal = dCar / 20

            yo = myBeam.Dalle.Cofradal.dp - dDecal
            xo = wPlat / 2 - wApp + dDecal

            Chaine = myBeam.Dalle.Cofradal.Nom
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xo, yo, myParAffA,
                         HorizontalAlignment.Left, VerticalAlignement.Top,
                         New SolidBrush(ColorPrefab), MyPen, lContour)

        End If

        '** Traitement dalle avec prédalle

        If myBeam.Dalle.lPrefaPredalle Then

            If lIntermediaire Then
                xCoteZ = xGaucheD + 0.5 * dCar
                kDir = +1
            Else
                xCoteZ = bEffD - 0.5 * dCar
                kDir = -1
            End If

            '** Epaisseur partie supérieure du béton

            MyColor = StyleCouleur(iSelect, SELECT_NO)
            MyPen.Color = MyColor

            yo = myBeam.Dalle.preDalle_ep
            ye = myBeam.Dalle.zTop

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Middle,
                         New SolidBrush(ColorB), MyPen, lContour)

            '** Epaisseur partie préfabriquée

            MyColor = StyleCouleur(iSelect, SELECT_EPPREDAL)
            MyPen.Color = MyColor

            yo = myBeam.Dalle.preDalle_ep
            ye = 0

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(myBeam.Dalle.preDalle_ep, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, (yo + ye) / 2, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Middle,
                         New SolidBrush(ColorB), MyPen, lContour)

            '** Epaisseur joint

            MyColor = StyleCouleur(iSelect, SELECT_EPPREJNT)
            MyPen.Color = MyColor

            xCoteZ += kDir * 0.5 * dCar

            yo = myBeam.Dalle.preDalle_ep
            ye = yo - myBeam.Dalle.preDalle_tjoint

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)

            ye = yo
            yo += 0.25 * dCar

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, False, False)

            Chaine = GetStringNoUnit(myBeam.Dalle.preDalle_tjoint, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xCoteZ, yo, myParAffA,
                         HorizontalAlignment.Center, VerticalAlignement.Bottom,
                         New SolidBrush(ColorB), MyPen, lContour)

        End If

        '--( Dessin des cotes pour les armatures

        '** Diamètre

        If (iSelect = SELECT_RFEUDIA) Then

            Dim xc, yc As Decimal
            Dim R2S2 As Decimal = Math.Sqrt(2) / 2
            Dim Dia As Decimal = myBeam.Dalle.ArmaSlimFeu.Diametre
            Dim dFlec As Decimal = dCar / 4

            MyColor = StyleCouleur(iSelect, SELECT_RFEUDIA)
            MyPen.Color = MyColor

            xc = xRef + myBeam.Dalle.ArmaSlimFeu.xPos
            yc = myBeam.Dalle.ArmaSlimFeu.zPos

            xo = xc + Dia * R2S2 / 2
            yo = yc + Dia * R2S2 / 2

            xe = xo + dFlec
            ye = yo + dFlec

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, myParAffA, True, False)

            Chaine = GetStringNoUnit(myBeam.Dalle.ArmaSlimFeu.Diametre, Enu_TypeVariable.Dimension)
            'AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xe, (yo + ye) / 2, myParAffA, HorizontalAlignment.Right, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, xe, ye, myParAffA, HorizontalAlignment.Left, VerticalAlignement.Top, False, MyPen)

            xo = xc - Dia * R2S2 / 2
            yo = yc - Dia * R2S2 / 2

            xe = xo - dFlec
            ye = yo - dFlec

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, myParAffA, True, False)

        End If

        '** Position X

        If (iSelect = SELECT_RFEUXPOS) Or (iSelect = SELECT_RFEUYPOS) Then

            MyColor = StyleCouleur(iSelect, SELECT_RFEUXPOS)
            MyPen.Color = MyColor

            xo = xRef
            yo = myBeam.Dalle.ArmaSlimFeu.zPos

            xe = xo + myBeam.Dalle.ArmaSlimFeu.xPos
            ye = yo

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, myParAffA, True, True)

            xo = xe
            xe += dCar / 3

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, myParAffA, False, False)

            Chaine = GetStringNoUnit(myBeam.Dalle.ArmaSlimFeu.xPos, Enu_TypeVariable.Dimension)
            'AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xe, (yo + ye) / 2, myParAffA, HorizontalAlignment.Right, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, xe, (yo + ye) / 2, myParAffA, HorizontalAlignment.Left, VerticalAlignement.Middle, False, MyPen)

        End If

        '** Position Y

        If (iSelect = SELECT_RFEUXPOS) Or (iSelect = SELECT_RFEUYPOS) Then

            MyColor = StyleCouleur(iSelect, SELECT_RFEUYPOS)
            MyPen.Color = MyColor

            xo = xRef + myBeam.Dalle.ArmaSlimFeu.xPos
            ye = myBeam.Dalle.ArmaSlimFeu.zPos

            xe = xo
            yo = 0

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, myParAffA, True, True)

            yo = ye
            ye += dCar / 3

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, myParAffA, False, False)

            Chaine = GetStringNoUnit(myBeam.Dalle.ArmaSlimFeu.zPos, Enu_TypeVariable.Dimension)
            'AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xe, (yo + ye) / 2, myParAffA, HorizontalAlignment.Right, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            AddTexte(MyGr, New SolidBrush(MyColor), Chaine, myFont, xe, ye, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Top, False, MyPen)

        End If

        '--( Informations matériau

        '# Béton

        If iSelect = SELECT_BETON Then

            AfficheInfoBeton(MyGr, myParAffA, strMsg, myBeam.Dalle.beton, myFont, 0, ye)

        End If


        '# Acier d'armature

        If iSelect = SELECT_RFEUFSK Then

            Dim xPos, yPos As Decimal
            xPos = xRef + myBeam.Dalle.ArmaSlimFeu.xPos + 2 * myBeam.Dalle.ArmaSlimFeu.Diametre
            yPos = myBeam.Dalle.ArmaSlimFeu.zPos

            AfficheInfoAcierArma(MyGr, myParAffA, strMsg, myBeam.Dalle.AcierArmatures, myFont, xPos, yPos)
        End If

    End Sub

    Private Sub DessineDalleDalle(myGr As Graphics, myBeam As cls_Poutre, myParafD As Struc_Affichage,
                                  myBrushB As Brush, myBrushPref As Brush, myBrushCofra As Brush, Beff As Decimal, Bfs As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   11/08/25 :  Version 1.20
        '-----------------------------------------------------------------------------------------------
        '   Dessin de la dalle dans la fenêtre Frm_DalleSlimFloorN
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont la dalle est à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   myParafD    [E] :   Parametres d'affichage
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushPref [E] :   Pinceau pour les parties préfa en béton
        '   myBrushCofra[E] :   Pinceau pour les bacs cofraplus220
        '   Beff        [E] :   Largeur d'affichage
        '   
        '-----------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------

        '--( Déclaration Initialisation

        Dim lIntermediaire As Boolean = myBeam.lIntermediaire
        Dim Ha As Decimal
        Ha = myBeam.Section.ProfilA.ha
        Dim lCofraplus220 As Boolean = myBeam.Dalle.Bac.lCofraplus220

        '--( Traitement

        Select Case myBeam.Dalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine

                DessinDallePleine(myGr, myBeam, lIntermediaire, Ha, Bfs, myParafD, myBrushB, Beff)

            Case cls_Dalle.Enum_TypeDalle.Mixte
                Select Case myBeam.Dalle.Bac.Orientation
                    Case cls_Bac.Enum_Orientation.Parallele

                        DessineDalleMixteParallele(myGr, myBeam.Dalle, Ha, Bfs, myParafD, myBrushB, Beff)

                    Case cls_Bac.Enum_Orientation.Perpendiculaire
                        If lCofraplus220 Then

                            DessineDalleMixtePerpendiculaireCfp220(myGr, myBeam, myParafD, myBrushB, Beff)

                        Else

                            DessineDalleMixtePerpendiculaire(myGr, myBeam, lIntermediaire, Ha, Bfs, myParafD, myBrushB, Beff)

                        End If
                End Select

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee

                DessinDallePreFab(myGr, myBeam, lIntermediaire, Ha, Bfs, myParafD, myBrushB, myBrushPref, Beff)

            Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique

                DessinDalleCompletementPrefa(myGr, myBeam, lIntermediaire, Ha, Bfs, myParafD, myBrushB, myBrushCofra, Beff)

        End Select

    End Sub

    Public Sub DessineDalle(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, myBeam As cls_Poutre, myFont As Font,
                            lIntermediaire As Boolean, iSelect As Integer, strMsg() As String, ByVal lCote As Boolean,
                            ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre dont la dalle est à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   strMsg      [E] :   Messages issus du fichier langue
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
        Dim CouleurCofradal As Color = Color.Linen
        Dim CouleurAcier As Color = CouleurAcierNormal
        'Dim pColorLocalArma(2, 2) As Color
        Dim ColorArmatures(1) As Color
        Const kADJUST As Decimal = 0.95
        Dim zREF As Decimal = 0
        Dim Ha, Bfs As Decimal
        'Dim lCote As Boolean = True
        Dim lCofraplus220 As Boolean
        Dim LargeurProfil As Decimal

        '--> Initialisation

        lMixte = myBeam.Section.lMixte
        lEnrob = myBeam.Section.lEnrobage
        lLamine = myBeam.Section.lLamine
        lCofraplus220 = myBeam.Dalle.Bac.lCofraplus220

        ' A REVOIR ====
        Beff = LargeurDalleDessin(myBeam.Section.ProfilA)
        BeffG = Beff / 2
        BeffD = Beff / 2

        Ha = myBeam.Section.ProfilA.ha

        If lIntermediaire Or Not myBeam.Section.lSlimFloor Then
            Bfs = myBeam.Section.ProfilA.Bfs
            LargeurProfil = Bfs
        Else
            Select Case myBeam.Section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                    Bfs = myBeam.Section.ProfilA.Bfs
                    LargeurProfil = Bfs
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    Bfs = myBeam.Section.ProfilA.Bfs
                    LargeurProfil = myBeam.Section.ProfilA.Plat_b - Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    Bfs = myBeam.Section.ProfilA.Bfs
                    LargeurProfil = myBeam.Section.ProfilA.Plat_b - Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    Bfs = myBeam.Section.ProfilA.Bfi
                    LargeurProfil = Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    Bfs = myBeam.Section.ProfilA.Bfi
                    LargeurProfil = Bfs / 2
            End Select
        End If

        '--> Preparation de la zone d'affichage - Calcul de ParAff
        dCar = Math.Sqrt(Beff ^ 2 + (Ha + myBeam.Dalle.zTop) ^ 2) / 10

        If lIntermediaire Or Not myBeam.Section.lSlimFloor Then
            xMin = -Beff / 2 - 2 * dCar
            xMax = -xMin
        Else
            xMin = -Bfs / 2 - 2 * dCar
            xMax = Beff / 2 + 2 * dCar
        End If

        If myBeam.Section.lSlimFloor Then
            If lIntermediaire Then
                yMin = -myBeam.Section.ProfilA.Plat_t
                yMax = myBeam.Dalle.zTop + dCar
            Else
                yMin = -myBeam.Section.ProfilA.Plat_t - 0.8 * dCar
                yMax = myBeam.Dalle.zTop + 0.8 * dCar
            End If
        Else
            yMin = -myBeam.Section.ProfilA.ha
            yMax = myBeam.Dalle.zTop + dCar
        End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Préparation des Pinceaux utilisés dans le dessin

        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurAcier, CouleurAcier)
        ' Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        ' Béton prefabriqué
        Dim myBrushPref As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        ' Cofradal
        Dim myBrushCofra As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurCofradal, CouleurCofradal)
        ' Etriers
        Dim myBrushE As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), ColorLocalEtriers, ColorLocalEtriers)
        ' Armatures de l'enrobage
        Dim myBrushArmaE As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurArmaNormal, CouleurArmaNormal)
        ' Etriers
        Dim myBrushA(1) As Brush

        Select Case iSelect
            Case 100, 101, 102, 103, 1001
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

        '--> Dessin de béton d'enrobage, le cas échéant

        If lEnrob Then _
        DessinEnrobagePartielBeton(myGr, myBeam.Section.ProfilA, myBeam.Section.Enrobage.Ratio_bc, MyParAff, myBrushB)

        '--> Dessin de la dalle

        '# Dalle béton

        DessineDalleDalle(myGr, myBeam, MyParAff, myBrushB, myBrushPref, myBrushCofra, Beff, Bfs)

        'Select Case myBeam.Dalle.type
        '    Case cls_Dalle.Enum_TypeDalle.Pleine

        '        DessinDallePleine(myGr, myBeam, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, Beff)

        '    Case cls_Dalle.Enum_TypeDalle.Mixte
        '        Select Case myBeam.Dalle.Bac.Orientation
        '            Case cls_Bac.Enum_Orientation.Parallele

        '                DessineDalleMixteParallele(myGr, myBeam.Dalle, Ha, Bfs, MyParAff, myBrushB, Beff)

        '            Case cls_Bac.Enum_Orientation.Perpendiculaire
        '                If lCofraplus220 Then

        '                    DessineDalleMixtePerpendiculaireCfp220(myGr, myBeam, MyParAff, myBrushB, Beff)

        '                Else

        '                    DessineDalleMixtePerpendiculaire(myGr, myBeam, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, Beff)

        '                End If
        '        End Select

        '    Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee

        '        DessinDallePreFab(myGr, myBeam, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, myBrushPref, Beff)

        '    Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique

        '        DessinDalleCompletementPrefa(myGr, myBeam, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, myBrushCofra, Beff)

        'End Select

        '# Armatures

        DessinLitArmaDalle(myGr, myBeam.Dalle, Beff, 0, myBeam.Section.ProfilA.ha, iSelect, MyParAff, myBrushA(0))
        DessinLitArmaDalle(myGr, myBeam.Dalle, Beff, 1, myBeam.Section.ProfilA.ha, iSelect, MyParAff, myBrushA(1))

        '--> Dessin de la mySection acier

        Dim lDessinRive As Boolean = (Not lIntermediaire) And myBeam.Section.lSlimFloor

        DessinProfileMetal(myGr, myBeam.Section.ProfilA, myBrushP, MyParAff, zREF, lDessinRive, 0)

        '--> Dessin des étriers

        If lEnrob Then _
        DessinEtriers(myGr, myBeam.Section.ProfilA, myBeam.Section.Enrobage, MyParAff, myBrushE, zREF)

        If lCote Then

            If Not lIntermediaire And myBeam.Section.lSlimFloor Then BeffG = Bfs / 2
            DessinCoteFrmDalle(myGr, myBeam, myFont, lIntermediaire, LargeurProfil, iSelect, MyParAff, dCar, BeffG, BeffD, strMsg, zREF, lCofraplus220)

        End If
    End Sub

    Private Sub DessinArmaFeuDalle(ByRef MyGr As Graphics, myBeam As cls_Poutre, myFont As Font, strMsg() As String,
                                   lIntermediaire As Boolean, iSelect As Integer,
                                   myParAffA As Struc_Affichage, dCar As Decimal, ColorArma As Color, ColorArmaSel As Color)
        '-----------------------------------------------------------------------------------------------
        '   08/08/25 :  Version 1.20
        '-----------------------------------------------------------------------------------------------
        '   Dessin des armatures longitudinales pour le calcul au feu des slims floors
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont la dalle est à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   strMsg      [E] :   Messages d'information dans le fichier langue
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   MyParAffA   [E] :   Paramètres d'affichage
        '   dCar        [E] :   Dimension pour l'affichage
        '   ColorArma   [E] :   Couleur pour les armatures
        '   ColorArmaSel[E] :   Couleur pour les armatures sélectionnées
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim myBrushA As SolidBrush                  'New SolidBrush(ColorArma)
        Dim kCote() As Decimal = {1, -1}
        'Const lCOTE As Boolean = True

        Const SELECT_RFEUXPOS As Integer = 101
        Const SELECT_RFEUYPOS As Integer = 102
        Const SELECT_RFEUDIA As Integer = 103
        Const SELECT_RFEUNB As Integer = 104
        ' Const SELECT_RFEUFSK As Integer = 1001

        Dim ColorA As Color = ColorArma
        Dim MyPen As New Pen(Color.Black, 1)
        '  Dim MyColor As Color
        '  Dim Chaine As String
        Dim lContour As Boolean = lCONTOURCOTE
        ' Dim xe, ye As Decimal
        ' Dim xo, yo As Decimal

        Dim xRef As Decimal = myBeam.Section.ProfilA.Tw / 2

        '--( 

        If myBeam.lSlimFloor And myBeam.Dalle.ArmaSlimFeu.lBarre Then

            '--( Initialisation

            If (iSelect = SELECT_RFEUXPOS) Or (iSelect = SELECT_RFEUDIA) _
            Or (iSelect = SELECT_RFEUYPOS) Or (iSelect = SELECT_RFEUNB) Then
                ColorA = ColorArmaSel
            End If
            myBrushA = New SolidBrush(ColorA)

            '--( Dessin des armatures

            With myBeam.Dalle.ArmaSlimFeu
                For iBarre As Integer = 1 To myBeam.Dalle.ArmaSlimFeu.NbBarres

                    For jCote As Integer = 0 To 1

                        AddCerclePlein(MyGr, myBrushA, kCote(jCote) * (xRef + .xPos + (iBarre - 1) * .Diametre), .zPos, .Diametre, myParAffA, True)

                    Next

                Next
            End With

            '--( Fin

            myBrushA.Dispose()
            MyPen.Dispose()
        End If
    End Sub

    Private Sub DessinCoteFrmDalle(ByRef MyGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                                   lIntermediaire As Boolean, LargeurProfil As Decimal, iSelect As Integer,
                                   myParAffA As Struc_Affichage, dCar As Decimal,
                                   BeffG As Decimal, BeffD As Decimal, strMsg() As String, ZREF As Decimal, lCofraplus220 As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   07/07/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin des cotes de la dalle
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont la dalle est à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   MyParAffA   [E] :   Paramètres d'affichage
        '   dCar        [E] :   Dimension pour l'affichage
        '   bEffG, BEffD[E] :   Largeur de dalle représentée à gauche et à droite
        '   strMsg      [E] :   Messages issus du fichier langue
        '   lCofraplus220[E] :  Indique si bac CofraPlus 220 le cas échéant
        '-----------------------------------------------------------------------------------------------
        '   iSelect:    0 : hauteur totale de dalle
        '               1 : renformis
        '               2 : epaisseur dalle au dessus du bac
        '              10 ! epaisseur de la prédalle
        '              11 ! épaisseur du joint
        '            1000 : béton dalle  
        '            1001 : acier armature
        '-----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xCoteZ As Decimal = -BeffG - dCar
        Dim ye As Decimal
        Dim xo, yo As Decimal
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color

        Dim lContour As Boolean = lCONTOURCOTE
        Dim MyFontNormal As Font = myFont

        Dim Chaine As String = ""
        Dim lDalleMixte As Boolean = (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte)
        Dim dCar2 As Decimal = myBeam.Dalle.Bac.Hp / 2

        '--> Cotations

        '# Hauteur de la Section acier

        MyColor = StyleCouleur(iSelect, -2)
        MyPen.Color = MyColor

        If myBeam.Section.lSlimFloor Then
            yo = ZREF
            ye = 0
        Else
            yo = -myBeam.Section.ProfilA.ha
            ye = 0
        End If

        Dim xCoteProfile As Decimal
        If myBeam.Section.lSlimFloor Then
            xCoteProfile = BeffD + dCar
        Else
            xCoteProfile = xCoteZ
        End If

        AddFleche(MyGr, MyPen, xCoteProfile, yo, xCoteProfile, ye, myParAffA, True, True)

        Select Case myBeam.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                Chaine = GetStringNoUnit(myBeam.Section.ProfilA.ha, Enu_TypeVariable.Dimension)
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                Chaine = GetStringNoUnit(myBeam.Section.ProfilA.hb, Enu_TypeVariable.Dimension)
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                Chaine = GetStringNoUnit(myBeam.Section.ProfilA.ha - myBeam.Section.ProfilA.Plat_t, Enu_TypeVariable.Dimension)
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                Chaine = GetStringNoUnit(myBeam.Section.ProfilA.ha - myBeam.Section.ProfilA.Tfi, Enu_TypeVariable.Dimension)
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                Chaine = GetStringNoUnit(myBeam.Section.ProfilA.hb, Enu_TypeVariable.Dimension)
        End Select
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteProfile, (yo + ye) / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '# Hauteur de la dalle 

        MyColor = StyleCouleur(iSelect, 0)
        MyPen.Color = MyColor

        yo = myBeam.Dalle.EpRenformis
        ye = myBeam.Dalle.zTop

        AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
        Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, (yo + ye) / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        If Not lIntermediaire And myBeam.Section.lSlimFloor Then xCoteZ = LargeurProfil + 0.3 * dCar

        '# Hauteur du renformis

        If (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine) And (myBeam.Dalle.Ep_th > 0) Then

            MyColor = StyleCouleur(iSelect, 1)
            MyPen.Color = MyColor

            yo = myBeam.Dalle.EpRenformis
            ye = 0

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(Math.Abs(ye - yo), Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, (yo + ye) / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

        '# Prédalle

        If (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee) And (myBeam.Dalle.preDalle_ep > 0) Then

            MyColor = StyleCouleur(iSelect, 10)
            MyPen.Color = MyColor

            yo = 0
            ye = myBeam.Dalle.preDalle_ep

            If Not lIntermediaire And myBeam.Section.lSlimFloor Then
                xCoteZ = LargeurProfil + 1.0 * dCar
            Else
                xCoteZ = -BeffG + 0.5 * dCar
            End If

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            AddLigne(MyGr, MyPen, xCoteZ, ye, xCoteZ, yo - dCar / 2, myParAffA)
            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, yo - dCar2 / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            If (myBeam.Dalle.preDalle_tjoint > 0) Then
                MyColor = StyleCouleur(iSelect, 11)
                MyPen.Color = MyColor

                yo = myBeam.Dalle.preDalle_ep
                ye = myBeam.Dalle.preDalle_ep - myBeam.Dalle.preDalle_tjoint

                If Not lIntermediaire And myBeam.Section.lSlimFloor Then
                    xCoteZ = LargeurProfil + 0.5 * dCar
                Else
                    xCoteZ = -BeffG + 1.0 * dCar
                End If

                AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
                AddLigne(MyGr, MyPen, xCoteZ, yo, xCoteZ, 0 - dCar / 2, myParAffA)
                Chaine = GetStringNoUnit(Math.Abs(ye - yo), Enu_TypeVariable.Dimension)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, 0 - dCar2 / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

        End If

        '# Cofradal

        If myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.PlancherPrefabrique Then
            MyColor = StyleCouleur(iSelect, 11)
            MyPen.Color = MyColor

            yo = 0
            ye = myBeam.Dalle.Cofradal.dp

            If Not lIntermediaire And myBeam.Section.lSlimFloor Then
                xCoteZ = LargeurProfil + 0.5 * dCar
            Else
                xCoteZ = -BeffG + 1.0 * dCar
            End If

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            AddLigne(MyGr, MyPen, xCoteZ, yo, xCoteZ, 0 - dCar / 2, myParAffA)
            Chaine = GetStringNoUnit(Math.Abs(ye - yo), Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, 0 - dCar2 / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

        '# Dalle mixte

        If lDalleMixte Then

            '# Epaisseur au dessus du bac
            MyColor = StyleCouleur(iSelect, 2)
            MyPen.Color = MyColor

            If Not lIntermediaire And myBeam.Section.lSlimFloor Then
                xCoteZ = LargeurProfil + 0.3 * dCar
            Else
                xCoteZ = -BeffG + dCar
            End If

            If lCofraplus220 Then
                yo = 0
                ye = myBeam.Dalle.zTop
            Else
                yo = myBeam.Dalle.Bac.Hp
                ye = myBeam.Dalle.zTop
            End If

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            AddLigne(MyGr, MyPen, xCoteZ, ye, xCoteZ, ye + dCar / 2, myParAffA)

            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, ye + dCar2 / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '# Epaisseur du bac

            MyColor = StyleCouleur(iSelect, -2)
            MyPen.Color = MyColor

            If lCofraplus220 Then
                ye = 0
                yo = -myBeam.Dalle.Bac.Hp
            Else
                yo = 0
                ye = myBeam.Dalle.Bac.Hp
            End If

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            AddLigne(MyGr, MyPen, xCoteZ, yo - dCar / 2, xCoteZ, yo, myParAffA)

            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, yo - dCar2 / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

        '# Hauteur totale de la Section + dalle

        If Not myBeam.Section.lSlimFloor Then

            MyColor = StyleCouleur(iSelect, -2)
            MyPen.Color = MyColor

            xCoteZ = -BeffG - 2 * dCar

            yo = -myBeam.Section.ProfilA.ha
            ye = myBeam.Dalle.zTop

            AddFleche(MyGr, MyPen, xCoteZ, yo, xCoteZ, ye, myParAffA, True, True)
            Chaine = GetStringNoUnit(ye - yo, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCoteZ, (yo + ye) / 2, myParAffA, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)


        End If

        '# Béton de la dalle

        If iSelect = 1000 Then

            'MyColor = StyleCouleur(iSelect, 1000)
            'MyPen.Color = MyColor

            'Dim lsChaine As New List(Of String)
            'Dim strBeton As String = strMsg(0).Trim & " "      ' "Concrete "

            'lsChaine.Clear()
            'lsChaine.Add(strBeton & myBeam.Dalle.beton.Classe)
            'lsChaine.Add("fck" & " = " & GetStringInUnit(myBeam.Dalle.beton.Fck, Enu_TypeVariable.Contrainte, 3, 1, True))
            'lsChaine.Add("Ecm" & " = " & GetStringInUnit(myBeam.Dalle.beton.Ecm, Enu_TypeVariable.ModuleY, 3, 1, True))
            'lsChaine.Add("n0" & " = " & GetStringInUnit(cls_Acier.EYACIER / myBeam.Dalle.beton.Ecm, Enu_TypeVariable.SansType, 3, 2, True))
            'lsChaine.Add("RhoC" & " = " & GetStringInUnit(myBeam.Dalle.beton.RhoC, Enu_TypeVariable.SansType, 3, 2, False) & " kg/m3")

            'xo = 0

            'AddTabTextFond(MyGr, New SolidBrush(MyColor), lsChaine, MyFontNormal, xo, ye, myParAffA, HorizontalAlignment.Left, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen)

            AfficheInfoBeton(MyGr, myParAffA, strMsg, myBeam.Dalle.beton, myFont, 0, ye)

        End If

        '# Acier d'armature

        If iSelect = 1001 Then

            Dim xPos, yPos As Decimal
            xPos = 0
            yPos = myBeam.Dalle.zTop - myBeam.Dalle.LitArma(0).z_s - 1.5 * myBeam.Dalle.LitArma(0).PhiS

            AfficheInfoAcierArma(MyGr, myParAffA, strMsg, myBeam.Dalle.AcierArmatures, myFont, xPos, yPos)

        End If

    End Sub

    Private Sub AfficheInfoBeton(myGr As Graphics, myParaffI As Struc_Affichage, strMsg() As String,
                                 myBeton As cls_Beton, myFont As Font, xPos As Decimal, yPos As Decimal)
        '---------------------------------------------------------------------------------------------------------------------
        '   11/08/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------
        '   Affichage des informations relative à l'acier des armatures dans un dessin de dalle
        '---------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   myParaffI   [E] :   Paramètres d'affichage
        '   strMsg      [E] :   messages
        '   myAcierA    [E] :   Acier d'armature dont on affiche les infos
        '   myFont      [E] :
        '   xPos,yPos   [E] :   Position du message d'info dans le dessin
        '---------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Dim xo, yo As Decimal

        MyColor = StyleCouleur(1, 1)
        MyPen.Color = MyColor

        Dim lsChaine As New List(Of String)
        Dim strBeton As String = strMsg(0).Trim & " "      ' "Concrete "

        lsChaine.Clear()
        lsChaine.Add(strBeton & myBeton.Classe)
        lsChaine.Add("fck" & " = " & GetStringInUnit(myBeton.Fck, Enu_TypeVariable.Contrainte, 3, 1, True))
        lsChaine.Add("Ecm" & " = " & GetStringInUnit(myBeton.Ecm, Enu_TypeVariable.ModuleY, 3, 1, True))
        lsChaine.Add("n0" & " = " & GetStringInUnit(cls_Acier.EYACIER / myBeton.Ecm, Enu_TypeVariable.SansType, 3, 2, True))
        lsChaine.Add("RhoC" & " = " & GetStringInUnit(myBeton.RhoC, Enu_TypeVariable.SansType, 3, 2, False) & " kg/m3")

        xo = 0

        AddTabTextFond(myGr, New SolidBrush(MyColor), lsChaine, myFont, xPos, yPos, myParaffI, HorizontalAlignment.Left, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen)

    End Sub

    Private Sub AfficheInfoAcierArma(myGr As Graphics, myParaffI As Struc_Affichage, strMsg() As String,
                                     myAcierA As cls_AcierArmature, myFont As Font, xPos As Decimal, yPos As Decimal)
        '---------------------------------------------------------------------------------------------------------------------
        '   11/08/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------
        '   Affichage des informations relative à l'acier des armatures dans un dessin de dalle
        '---------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   myParaffI   [E] :   Paramètres d'affichage
        '   strMsg      [E] :   messages
        '   myAcierA    [E] :   Acier d'armature dont on affiche les infos
        '   myFont      [E] :
        '   xPos,yPos   [E] :   Position du message d'info dans le dessin
        '---------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Dim xo, yo As Decimal

        '--(

        MyColor = StyleCouleur(1, 1)
        MyPen.Color = MyColor

        Dim lsChaine As New List(Of String)
        Dim strRebar As String = strMsg(1).Trim & " "       ' "Steel reinforcement "

        lsChaine.Clear()
        lsChaine.Add(strRebar & myAcierA.Classe)
        lsChaine.Add("fsk" & " = " & GetStringInUnit(myAcierA.FsK, Enu_TypeVariable.Contrainte, 3, 1, True))
        lsChaine.Add("Es" & " = " & GetStringInUnit(myAcierA.Es, Enu_TypeVariable.ModuleY, 3, 1, True))

        xo = xPos
        yo = yPos

        AddTabTextFond(myGr, New SolidBrush(MyColor), lsChaine, myFont, xo, yo, myParaffI, HorizontalAlignment.Center, HorizontalAlignment.Left, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen)


    End Sub

    Private Sub DessinLitArmaDalle(ByRef MyGr As Graphics, MyDalle As cls_Dalle, BeffRed As Decimal, iArma As Integer,
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
    Private Sub DessinLitArmaDalle(ByRef MyGr As Graphics, MyDalle As cls_Dalle, BeffRed As Decimal, iArma As Integer,
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
        Td = MyDalle.Ep_td
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

        Dim Diagonale As Decimal = Math.Sqrt((Profile.Bfi + Profile.Bfs) ^ 2 / 4 + Profile.ha ^ 2)
        Dim BfMax As Decimal = Math.Max(Profile.Bfs, Profile.Bfi)
        Dim BeffRed As Decimal

        '--> Traitement

        BeffRed = 2 * Diagonale ' Math.Min(MySection.dalle.Beff, 1.5 * Diagonale)

        Return BeffRed

    End Function

#End Region

#Region " Dessins pour la définition du bac (FRM_BACN) "

    Public Sub DessineBac(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, kAdjust As Double, ByVal myFont As Font,
                          ByVal MyBac As cls_Bac,
                          ByVal EpDalle As Double, ByRef iCote As Integer,
                          ByVal lCotation As Boolean, ByVal lCotEpTot As Boolean,
                          ByVal lTitre As Boolean,
                          ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier (un seul module)
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   Img         [E] :   Image dans laquelle on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBac       [E] :   Bac à dessiner
        '   myFont      [E] :   Police de caractères 
        '   EpDalle     [E] :   Epaisseur de la dalle béton
        '   iCote       [E] :   Indice de la cote sélectionnée (?)
        '   VariableBac [E] :   Parametre du bac sélectionné (pour affichage en rouge)
        '   lCotation   [E] :   Indique si on met les cotations sur le dessin
        '   lCotEpTot   [E] :   Indique si cotation epaisseur bac+dalle
        '   lTitre      [E] :   Indique si affichage du titre du bac
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
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
        Dim MyFontNormal As New Font(myFont.Name, myFont.Size)

        Dim xMin, yMin, xMax, yMax As Double

        Dim dCar As Double

        Dim lRaidSup As Boolean

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim nbOndes As Integer = 5

        Dim lUn As Boolean = True

        '--> Initialisation

        lRaidSup = MyBac.HasRaidisseurSup
        If lUn Then
            dCar = Math.Sqrt(MyBac.Hp ^ 2 + MyBac.Ep ^ 2) / 16
        Else
            dCar = (MyBac.Ep + MyBac.Bb) / 2
        End If

        '--> Preparation de la zone d'affichage - Calcul de ParAff

        If lUn Then
            xMin = -MyBac.Ep / 2
            xMax = MyBac.Ep / 2
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

            CotationBacUn(myGr, MyParAff, myFont, MyBac, dCar, iCote)

        End If


    End Sub

    Private Sub CotationBacUn(ByRef myGr As Graphics, MyParAffC As Struc_Affichage, myFont As Font,
                              MyBac As cls_Bac, dCar As Decimal, iSelect As Integer)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Cotation d'une nervure de bac
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   myParAffC   [E] :   Paramètres d'affichage
        '   myFont      [E] :   Police
        '   myBac       [E] :   Bac
        '   dCar        [E] :   Dimension caractéristique
        '   iSelect     [E] :   Indice de la variable sélectionnée
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
        Dim MyFontNormal As New Font(myFont.Name, myFont.Size)
        Dim MyColor As Color
        Dim Chaine As String
        Dim lAffSymbol As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        Dim eP, hP, bbP, btP, tP, hPg As Decimal
        Const kTP As Decimal = 2
        Dim lRaid As Boolean = MyBac.HasRaidisseurSup

        '--> Initialisation

        eP = MyBac.Ep
        hP = MyBac.Hp
        btP = MyBac.Bt
        bbP = MyBac.Bb
        tP = MyBac.Tp * kTP
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

        yo = 0 + dCar / 2 * 1.05
        ye = 0 + dCar / 2 * 1.05

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

        '# Fin

        MyPenNormal.Dispose()
        MyPenSelect.Dispose()
        MyFontNormal.Dispose()
        MyPen.Dispose()
    End Sub

#End Region

#Region " Dessins pour la dfiniton de l'enrobage (FRM_ENROBAGE) "

    ''' <summary>
    ''' Dessin réactif de la mySection acier et de l'enrobage partiel
    ''' </summary>
    Public Sub DessinFrmEnrobage(ByRef MyGr As Graphics, ByVal section As cls_Section, MyEnrob As cls_Enrobage_Partiel,
                                 ByVal pWi As Decimal, ByVal pHi As Decimal,
                                 kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                 ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de l'enrobage partiel
        '   Représente la mySection acier + l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   mySection     [E] :
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
        'Profile = mySection.ProfilA.Clone
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
        xMin = -Math.Max(Profile.Bfs, Profile.Bfi) / 2
        xMax = -xMin
        yMax = 0

        'If lCote Then
        dCar = Math.Sqrt((Profile.ha ^ 2 + (Profile.Bfs + Profile.Bfi) ^ 2)) / 20
        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAffE, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Dessin de béton

        DessinEnrobagePartielBeton(MyGr, section.ProfilA, MyEnrob.Ratio_bc, MyParAffE, myBrushB)

        '--> Dessin de la mySection acier

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

    Private Sub DessinCoteFrmEnrobage(ByRef MyGr As Graphics, ByVal section As cls_Section, enrobage As cls_Enrobage_Partiel,
                                      iSelect As Integer, zRef As Decimal, dCar As Decimal,
                                      lAffSymbol As Boolean, MyParAffLoc As Struc_Affichage)
        '---------------------------------------------------------------------------------------------------------------------------
        '   05/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de l'enrobage partiel
        '   Représente la cotation mySection acier + l'enrobage partiel
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
        Bc = section.ProfilA.Bfs * enrobage.Ratio_bc
        Uz = enrobage.Etriers_EnrobageZ
        Bf = section.ProfilA.Bfs
        Tw = section.ProfilA.Tw
        Rc = section.ProfilA.Rcs
        Tf = section.ProfilA.Tfs

        '--> AffichageOptFeu des cotes

        '# Bc

        If iSelect = 0 Then
            MyColor = StyleCouleur(iSelect, 0)
            MyPen.Color = MyColor

            xo = -Bc / 2
            xe = xo - dCar / 2
            yo = -(section.ProfilA.ha / 2 - section.ProfilA.Tfs - section.ProfilA.Rcs) * 0.8
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
        yo = -(section.ProfilA.ha / 2 - section.ProfilA.Tfs - section.ProfilA.Rcs) * 0.6
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
        yo = -(section.ProfilA.ha / 2 - section.ProfilA.Tfs - section.ProfilA.Rcs) * 0.4
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
        xo = section.ProfilA.Bfi / 2
        xe = -xo

        If lAffSymbol Then
            If lLam Then Chaine = "b" Else Chaine = "bfi"
        Else
            Chaine = GetStringNoUnit(section.ProfilA.Bfi, Enu_TypeVariable.Dimension)
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

            xo = section.ProfilA.Bfi / 2 - Uy - PhiE - enrobage.LitArma(iArma).PhiExt / 2
            xe = section.ProfilA.Bfi / 2 + dCar
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
    Private Sub DessinEtriers(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
                              MyParAffloc As Struc_Affichage, MyBrushE As Brush, zRef As Decimal, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu des étriers dans le béton de l'enrobage partiel
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   
        '   profile     [E] :   profilé
        '   enrobage    [E] :   enrobage
        '   MyParAffloc [E] :   Paramètres d'affichage
        '   MyBrushE    [E] :   Pinceau pour le remplissage des étriers
        '   zRef        [E] :   Position z de référence (par rapport à la fibre supérieure de la semelle sup)
        '---------------------------------------------------------------------------------------------------------------------------

        Select Case enrobage.Etriers_Type
            Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                DessinEtriersCadre(MyGr, profile, enrobage, MyParAffloc, MyBrushE, xPos)
            Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant, cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                DessinEtriersCadreSouT(MyGr, profile, enrobage, MyParAffloc, MyBrushE, zRef, xPos)
        End Select

    End Sub

    ''' <summary>
    ''' Dessine les etriers soudés
    ''' </summary>
    Private Sub DessinEtriersCadreSouT(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
                                       MyParAffloc As Struc_Affichage, MyBrushE As Brush, zRef As Decimal, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu des étriers dans le béton de l'enrobage partiel
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
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim lSoude As Boolean = (enrobage.Etriers_Type = cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude)

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
    Private Sub DessinEtriersCadre(ByRef MyGr As Graphics, ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
                                   MyParAffloc As Struc_Affichage, MyBrushE As Brush, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu des étriers dans le béton de l'enrobage partiel
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

        Dim nbPts As Integer
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim xPtsP() As Single = Nothing
        Dim yPtsP() As Single = Nothing
        Dim nbPtsP As Integer

        '--> Préparation du contour des étriers

        PrepareContourEtriersP(profile, enrobage, xPtsP, yPtsP, nbPtsP, xPos)
        PrepareContourEtriersG(profile, enrobage, xPts, yPts, nbPts, xPos)

        '--> Dessin Contour côté gauche

        RemplirZone(MyGr, MyBrushE, xPtsP, yPtsP, nbPtsP, MyParAffloc, True, True)
        RemplirZone(MyGr, MyBrushE, xPts, yPts, nbPts, MyParAffloc, True, True)

        '--> Dessin Contour côté droit par symétrie

        MirroirPts(xPts, nbPts, xPos)
        MirroirPts(xPtsP, nbPtsP, xPos)
        RemplirZone(MyGr, MyBrushE, xPtsP, yPtsP, nbPtsP, MyParAffloc, True, True)
        RemplirZone(MyGr, MyBrushE, xPts, yPts, nbPts, MyParAffloc, True, True)

    End Sub

    Private Sub MirroirPts(ByRef cPts() As Single, nbPts As Integer, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '   09/11/23    :   Modif GuD: Ajout de la variable xPos pour avoir une symétrie par rapport à l'axe de l'âme 
        '---------------------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To nbPts - 1
            cPts(i) = 2 * xPos - cPts(i)
        Next

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier traversant
    ''' </summary>
    Private Sub PrepareContourEtriersTravers(ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
                                             ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)

        '---------------------------------------------------------------------------------------------------------------------------
        '   20/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DiaCourbureSup As Decimal = enrobage.LitArma(2).PhiExt  'enrobage.LitsArmaOLD(2).Phi
        Dim DiaCourbureInf As Decimal = enrobage.LitArma(0).PhiExt  'enrobage.LitsArmaOLD(0).Phi
        Dim LongueurRetour As Decimal = 5 * enrobage.Etriers_Phi
        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.Bfs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.Tw
        Dim Tf As Decimal = profile.Tfs
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
    Private Sub PrepareContourEtriersSoudes(ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
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

        Dim DiaCourbureSup As Decimal = enrobage.LitArma(2).PhiExt       ' enrobage.LitsArmaOLD(2).Phi
        Dim DiaCourbureInf As Decimal = enrobage.LitArma(0).PhiExt       ' enrobage.LitsArmaOLD(0).Phi
        Dim LongueurRetour As Decimal = 5 * enrobage.Etriers_Phi
        Dim xc, yc As Single
        Dim xo, yo As Single

        Dim Bf As Decimal = profile.Bfs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.Tw
        Dim Tf As Decimal = profile.Tfs
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
    Private Sub PrepareContourEtriersP(ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
                                       ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer, Optional xPos As Decimal = 0)
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

        Dim Bf As Decimal = profile.Bfs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.Tw
        Dim Tf As Decimal = profile.Tfs
        Dim Uy As Decimal = enrobage.Etriers_EnrobageY
        Dim Uz As Decimal = enrobage.Etriers_EnrobageZ
        Dim Bc As Decimal = enrobage.Ratio_bc * Bf
        Dim PhiEtrier As Decimal = enrobage.Etriers_Phi

        Dim RayonC As Decimal

        '--> Contour Intérieur

        RayonC = DiaCourbureSup / 2
        xc = -Tw / 2 - Uy - (DiaCourbureSup) / 2 - PhiEtrier + xPos
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
    Private Sub PrepareContourEtriersG(ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
                                       ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer, Optional xPos As Decimal = 0)
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

        Dim Bf As Decimal = profile.Bfs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.Tw
        Dim Tf As Decimal = profile.Tfs
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

        xc = -Tw / 2 - UyInt - RayonC - PhiEtrier + xPos
        yc = -Tf - Uz - RayonC - PhiEtrier

        xo = xc + Math.Sqrt(2) / 2 * (RayonC - LongueurRetour)
        yo = yc - Math.Sqrt(2) / 2 * (RayonC + LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        AjouteArcCercle(xc, yc, RayonC, -45, 90, 1, xPts, yPts, nbPts)

        '=# Armature supérieure extérieure

        RayonC = DiaCourbureSupExt / 2
        xc = -Bc / 2 + Uy + RayonC + PhiEtrier + xPos
        yc = -Tf - Uz - RayonC - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 90, 180, 1, xPts, yPts, nbPts)

        '=# Armature inférieure extérieure

        RayonC = DiaCourbureInfExt / 2
        xc = -Bc / 2 + Uy + RayonC + PhiEtrier + xPos
        yc = -Ht + Tf + Uz + RayonC + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, 1, xPts, yPts, nbPts)

        '=# Armature inférieure intérieure

        RayonC = DiaCourbureInfInt / 2

        xc = -Tw / 2 - UyInt - RayonC - PhiEtrier + xPos
        yc = -Ht + Tf + Uz + RayonC + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 270, 360, 1, xPts, yPts, nbPts)

        '=# Armature supérieure intérieure

        RayonC = DiaCourbureSupInt / 2 + PhiEtrier
        xc = -Tw / 2 - UyInt - DiaCourbureSupInt / 2 - PhiEtrier + xPos
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
        xc = -Tw / 2 - UyInt - DiaCourbureInfInt / 2 - PhiEtrier + xPos
        yc = -Ht + Tf + Uz + DiaCourbureInfInt / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 270, 360, -1, xPts, yPts, nbPts)

        '=# Armature inférieure extérieure

        RayonC = DiaCourbureInfExt / 2 + PhiEtrier
        xc = -Bc / 2 + Uy + (DiaCourbureInfExt) / 2 + PhiEtrier + xPos
        yc = -Ht + Tf + Uz + DiaCourbureInfExt / 2 + PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 180, 270, -1, xPts, yPts, nbPts)

        '=# Armature supérieure extérieure

        RayonC = DiaCourbureSupExt / 2 + PhiEtrier
        xc = -Bc / 2 + Uy + (DiaCourbureSupExt) / 2 + PhiEtrier + xPos
        yc = -Tf - Uz - DiaCourbureSupExt / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, 90, 180, -1, xPts, yPts, nbPts)

        '=# Armature supérieure intérieure

        RayonC = DiaCourbureSupInt / 2 + PhiEtrier
        xc = -Tw / 2 - Uy - DiaCourbureSupInt / 2 - PhiEtrier + xPos
        yc = -Tf - Uz - DiaCourbureSupInt / 2 - PhiEtrier

        AjouteArcCercle(xc, yc, RayonC, -45, 90, -1, xPts, yPts, nbPts)

        xo = xc + Math.Sqrt(2) / 2 * (RayonC - LongueurRetour)
        yo = yc - Math.Sqrt(2) / 2 * (RayonC + LongueurRetour)

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

    ''' <summary>
    ''' Préparation des points définissant le contour d'un étrier
    ''' </summary>
    Private Sub PrepareContourEtriers(ByVal profile As cls_ProfilA, enrobage As cls_Enrobage_Partiel,
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

        Dim Bf As Decimal = profile.Bfs
        Dim Ht As Decimal = profile.ha
        Dim Tw As Decimal = profile.Tw
        Dim Tf As Decimal = profile.Tfs
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
        Dim DeltaAlpha As Single
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

#Region " Dessins pour le choix du profilé (FRM_SECTIONACIER) "

    Public Sub DessinProfileAcierN(ByRef MyGr As Graphics, ByVal mySec As cls_Section,
                                   ByVal pWi As Decimal, ByVal pHi As Decimal, ByVal myFont As Font,
                                   kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                   ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   07/08/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la mySection acier
        '---------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   mySec       [E] :   Section à dessiner
        '   pWi, pHi    [E] :   Dimensions de l'image conteneur
        '   kAdjust     [E] :   Paramètre ajustement de la taille de l'image
        '   myFont      [E] :   Police pour l'affichage des infos
        '   lCote       [E] :   Indique si affichage cotation
        '   lAffSymbol  [E] :   Indique si affichage des symboles ou des valeurs
        '   iSelect     [E] :   Indique si parmètre selectionné
        '   lBox        [E] :   Indique si affichage texte avec fond en couleur
        '   xLeft, yTop [E] :   Paramètres de positionnement pour affichage dans NdC
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
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
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim lLam As Boolean = (mySec.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine)
        Dim lPlat As Boolean = lLam And mySec.ProfilA.lPlatRenfort

        Dim zRef As Decimal = 0

        Dim MyParAff As Struc_Affichage
        Dim lContour As Boolean = lCONTOURCOTE
        Dim myBrushF As SolidBrush

        '--> Préparation Pinceau dégradé

        Dim myBrushG As Brush

        If xLeft <> 0 Or yTop <> 0 Then
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, Color.DarkGray)
        Else
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcierNormal)
        End If

        '--> Initialisation des paramètres d'affichage

        yMin = -mySec.ProfilA.ha
        If lPlat Then yMin -= mySec.ProfilA.Plat_t

        xMin = -Math.Max(mySec.ProfilA.Bfs, mySec.ProfilA.Bfi) / 2

        xMax = -xMin
        yMax = 0

        dCar = Math.Sqrt((mySec.ProfilA.ha ^ 2 + (2 * xMin) ^ 2)) / 20

        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar

        If lPlat Then yMin -= dCar

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Dessin de la mySection acier

        DessinProfileMetal(MyGr, mySec.ProfilA, myBrushG, MyParAff, zRef)

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

            '-- H --=====================================================================================

            yo = -mySec.ProfilA.ha
            ye = 0
            xo = -Math.Max(mySec.ProfilA.Bfs, mySec.ProfilA.Bfi) / 2 - dCar
            xe = xo

            MyColor = StyleCouleur(iSelect, 0)
            MyPen.Color = MyColor

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(mySec.ProfilA.ha, Enu_TypeVariable.Dimension)

            myBrushF = New SolidBrush(MyColor)

            'If lBox Then
            AddTexteFond(MyGr, myBrushF, Chaine, myFont, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            'Else
            'AddTexte(MyGr, myBrushF, Chaine, myFont, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
            'End If

            '-- Bfi --=====================================================================================

            Dim iRef As Integer

            Select Case mySec.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym : iRef = 1
                Case Else : iRef = 3
            End Select
            MyColor = StyleCouleur(iSelect, iRef)
            MyPen.Color = MyColor

            If lLam And lPlat Then
                yo = dCar
            Else
                yo = -mySec.ProfilA.ha - dCar
            End If
            xo = mySec.ProfilA.Bfi / 2

            ye = yo
            xe = -xo

            If lAffSymbol Then
                If lLam Then
                    Chaine = "b"

                Else
                    Chaine = "bfi"
                End If
            Else
                Chaine = GetStringNoUnit(mySec.ProfilA.Bfi, Enu_TypeVariable.Dimension)
            End If

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)

            myBrushF = New SolidBrush(MyColor)

            'If lBox Then
            AddTexteFond(MyGr, myBrushF, Chaine, myFont, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            'Else
            '    AddTexte(MyGr, myBrushF, Chaine, myFont, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
            'End If

            '-- wp et tp --================================================================================

            If lLam And lPlat Then

                '# wp
                iRef = 20
                MyColor = StyleCouleur(iSelect, iRef)
                MyPen.Color = MyColor

                yo = -mySec.ProfilA.ha - mySec.ProfilA.Plat_t - dCar
                ye = yo

                xo = mySec.ProfilA.Plat_b / 2
                xe = -xo

                If lAffSymbol Then

                    Chaine = "wp"

                Else
                    Chaine = GetStringNoUnit(mySec.ProfilA.Plat_b, Enu_TypeVariable.Dimension)
                End If
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)

                myBrushF = New SolidBrush(MyColor)

                AddTexteFond(MyGr, myBrushF, Chaine, myFont, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)


                '# tp
                iRef = 21
                MyColor = StyleCouleur(iSelect, iRef)
                MyPen.Color = MyColor

                xo = 1 / 2 * mySec.ProfilA.Plat_b / 4 * 3
                xe = xo

                If lAffSymbol Then

                    Chaine = "tp"

                Else
                    Chaine = GetStringNoUnit(mySec.ProfilA.Plat_t, Enu_TypeVariable.Dimension)
                End If

                yo = -mySec.ProfilA.ha - mySec.ProfilA.Plat_t
                ye = -mySec.ProfilA.ha
                AddLigne(MyGr, xo, yo, xe, ye, MyParAff)

                ye = yo - 2 * dCar
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

                myBrushF = New SolidBrush(MyColor)

                AddTexteFond(MyGr, myBrushF, Chaine, myFont, (xo + xe) / 2, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- Bfs --=====================================================================================

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 1)
                MyPen.Color = MyColor

                yo = 0 + dCar
                ye = yo
                xo = mySec.ProfilA.Bfs / 2
                xe = -xo
                If lAffSymbol Then Chaine = "bfs" Else Chaine = GetStringNoUnit(mySec.ProfilA.Bfs, Enu_TypeVariable.Dimension)
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)

                myBrushF = New SolidBrush(MyColor)

                'If lBox Then
                AddTexteFond(MyGr, myBrushF, Chaine, myFont, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                'Else
                '    AddTexte(MyGr, myBrushF, Chaine, myFont, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
                'End If

            End If

            '-- Hw --=====================================================================================

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 7)
                MyPen.Color = MyColor

                yo = 0 - mySec.ProfilA.Tfs
                ye = -mySec.ProfilA.ha + mySec.ProfilA.Tfi

                'PRS
                xo = -Math.Min(mySec.ProfilA.Bfs, mySec.ProfilA.Bfi) / 2 + dCar
                xe = xo

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                If lAffSymbol Then Chaine = "hw" Else Chaine = GetStringNoUnit(mySec.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension)

                myBrushF = New SolidBrush(MyColor)

                'If lBox Then
                AddTexteFond(MyGr, myBrushF, Chaine, myFont, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                'Else
                '    AddTexte(MyGr, myBrushF, Chaine, myFont, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
                'End If
            End If

            '-- Tfi --=====================================================================================

            Select Case mySec.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym : iRef = 2
                Case Else : iRef = 4
            End Select
            MyColor = StyleCouleur(iSelect, iRef)
            MyPen.Color = MyColor

            yo = -mySec.ProfilA.ha - dCar / 2
            ye = -mySec.ProfilA.ha

            xo = mySec.ProfilA.Bfi / 2 - dCar
            xe = xo
            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo = -mySec.ProfilA.ha + mySec.ProfilA.Tfi
            ye = yo + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then
                If lLam Then Chaine = "tf" Else Chaine = "tfi"
            Else
                Chaine = GetStringInUnit(mySec.ProfilA.Tfi, Enu_TypeVariable.Dimension, 3, 1, False)
            End If

            myBrushF = New SolidBrush(MyColor)

            'If lBox Then
            '    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            'Else
            AddTexte(MyGr, myBrushF, Chaine, myFont, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            'End If

            '-- Tfs --=====================================================================================

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 2)
                MyPen.Color = MyColor

                yo = 0 + dCar / 2
                ye = 0
                xo = mySec.ProfilA.Bfs / 4 + dCar / 2
                xe = xo
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                yo = 0 - mySec.ProfilA.Tfs
                ye = yo - dCar
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "tfs" Else Chaine = GetStringInUnit(mySec.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, 1, False)

                myBrushF = New SolidBrush(MyColor)

                'If lBox Then
                '    AddTexteFond(MyGr, myBrushF, Chaine, myFont, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                'Else
                AddTexte(MyGr, myBrushF, Chaine, myFont, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
                'End If

            End If

            '-- R --=====================================================================================

            If lLam Then

                MyColor = StyleCouleur(iSelect, 6)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                yo = 0 - mySec.ProfilA.Tfs - mySec.ProfilA.Rcs * (1 - kProj)
                ye = yo - dCar * kProj

                xo = mySec.ProfilA.Tw / 2 + mySec.ProfilA.Rcs * (1 - kProj)
                xe = xo + dCar * kProj

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(mySec.ProfilA.Rcs, Enu_TypeVariable.Dimension)

                myBrushF = New SolidBrush(MyColor)

                'If lBox Then
                '    AddTexteFond(MyGr, myBrushF, Chaine, myFont, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                'Else
                AddTexte(MyGr, myBrushF, Chaine, myFont, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
                'End If

            End If

            '-- tw --=====================================================================================

            MyColor = StyleCouleur(iSelect, 5)
            MyPen.Color = MyColor

            xo = -mySec.ProfilA.Tw / 2
            xe = xo - dCar / 2
            yo = -Math.Max(mySec.ProfilA.Tfs + mySec.ProfilA.Rcs + 2 * dCar, mySec.ProfilA.ha / 4)
            ye = yo

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = mySec.ProfilA.Tw / 2
            xe = xo + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(mySec.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, 1, False)

            myBrushF = New SolidBrush(MyColor)

            'If lBox Then
            '    AddTexteFond(MyGr, myBrushF, Chaine, myFont, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            'Else
            AddTexte(MyGr, myBrushF, Chaine, myFont, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
            'End If

        End If

    End Sub

    Public Sub DessinProfileAcier(ByRef MyGr As Graphics, ByVal section As cls_Section,
                                  ByVal Width As Decimal, ByVal Height As Decimal, ByVal MyFont As Font,
                                  kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                  ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la mySection acier
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

        If section.lSlimFloor Then
            Select Case section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    xMin = -Math.Max(section.ProfilA.Bfs, Math.Max(section.ProfilA.Bfi, section.ProfilA.Plat_b)) / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    xMin = -Math.Max(section.ProfilA.Bfs, section.ProfilA.Plat_b) / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    xMin = -Math.Max(section.ProfilA.Bfi, section.ProfilA.Plat_b) / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    xMin = -Math.Max(section.ProfilA.Bfs, section.ProfilA.Bfi) / 2
            End Select
        Else
            xMin = -Math.Max(section.ProfilA.Bfs, section.ProfilA.Bfi) / 2
        End If

        xMax = -xMin
        yMax = 0

        dCar = Math.Sqrt((section.ProfilA.ha ^ 2 + (2 * xMin) ^ 2)) / 20

        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, Width, Height, xLeft, yTop, kAdjust)

        '--> Dessin de la mySection acier

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
            xo = -Math.Max(section.ProfilA.Bfs, section.ProfilA.Bfi) / 2 - dCar
            xe = xo

            MyColor = StyleCouleur(iSelect, 0)
            MyPen.Color = MyColor

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(section.ProfilA.ha, Enu_TypeVariable.Dimension)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfi --

            Dim iRef As Int16

            If Not section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Then

                Select Case section.ProfilA.typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym : iRef = 1
                    Case Else : iRef = 3
                End Select
                MyColor = StyleCouleur(iSelect, iRef)
                MyPen.Color = MyColor

                yo = -section.ProfilA.ha - dCar
                xo = section.ProfilA.Bfi / 2

                ye = yo
                xe = -xo

                If lAffSymbol Then
                    If lLam Then
                        Chaine = "b"

                    Else
                        Chaine = "bfi"
                    End If
                Else
                    Chaine = GetStringNoUnit(section.ProfilA.Bfi, Enu_TypeVariable.Dimension)
                End If

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- Bfs --

            If Not section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB Then
                If Not lLam Then

                    MyColor = StyleCouleur(iSelect, 1)
                    MyPen.Color = MyColor

                    yo = 0 + dCar
                    ye = yo
                    xo = section.ProfilA.Bfs / 2
                    xe = -xo
                    If lAffSymbol Then Chaine = "bfs" Else Chaine = GetStringNoUnit(section.ProfilA.Bfs, Enu_TypeVariable.Dimension)
                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                End If
            End If

            '--Bp--

            If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Or section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Or section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB Then
                'présence d'un plat soudé 

                If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Or section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Then 'plat soudé en partie inférieure
                    yo = -section.ProfilA.ha - 2 * dCar
                    xo = section.ProfilA.Plat_b / 2

                    ye = yo
                    xe = -xo

                    If lAffSymbol Then
                        Chaine = "bp"
                    Else
                        Chaine = GetStringNoUnit(section.ProfilA.Plat_b, Enu_TypeVariable.Dimension)
                    End If

                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                Else
                    yo = dCar
                    xo = section.ProfilA.Plat_b / 2

                    ye = yo
                    xe = -xo

                    If lAffSymbol Then
                        Chaine = "bp"
                    Else
                        Chaine = GetStringNoUnit(section.ProfilA.Plat_b, Enu_TypeVariable.Dimension)
                    End If

                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                End If
            End If


            '-- Hw --

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 7)
                MyPen.Color = MyColor

                If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB Then
                    yo = 0 - section.ProfilA.Plat_t
                Else
                    yo = 0 - section.ProfilA.Tfs
                End If

                If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Then
                    ye = -section.ProfilA.ha + section.ProfilA.Tfi + section.ProfilA.Plat_t
                ElseIf section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Then
                    ye = -section.ProfilA.ha + section.ProfilA.Plat_t
                Else
                    ye = -section.ProfilA.ha + section.ProfilA.Tfi
                End If

                Select Case section.ProfilA.typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                        xo = -Math.Min(section.ProfilA.Bfs, section.ProfilA.Bfi) / 2 + dCar
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                        xo = -Math.Min(section.ProfilA.Bfs, section.ProfilA.Plat_b) / 2 + dCar
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                        xo = -Math.Min(section.ProfilA.Plat_b, section.ProfilA.Bfi) / 2 + dCar
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                        xo = -Math.Min(section.ProfilA.Bfs, section.ProfilA.Bfi) / 2 + dCar
                    Case Else 'PRS
                        xo = -Math.Min(section.ProfilA.Bfs, section.ProfilA.Bfi) / 2 + dCar
                End Select


                xe = xo

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                If lAffSymbol Then Chaine = "hw" Else Chaine = GetStringNoUnit(section.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            End If

            '-- Tfi --

            If Not section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Then

                Select Case section.ProfilA.typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym : iRef = 2
                    Case Else : iRef = 4
                End Select
                MyColor = StyleCouleur(iSelect, iRef)
                MyPen.Color = MyColor

                If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Then
                    yo = -section.ProfilA.ha + section.ProfilA.Plat_t - dCar / 2
                    ye = -section.ProfilA.ha + section.ProfilA.Plat_t
                Else
                    yo = -section.ProfilA.ha - dCar / 2
                    ye = -section.ProfilA.ha
                End If

                xo = section.ProfilA.Bfi / 2 - dCar
                xe = xo
                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Then
                    yo = -section.ProfilA.ha + section.ProfilA.Plat_t + section.ProfilA.Tfi
                    ye = yo + dCar
                Else
                    yo = -section.ProfilA.ha + section.ProfilA.Tfi
                    ye = yo + dCar
                End If

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then
                    If lLam Then Chaine = "tf" Else Chaine = "tfi"
                Else
                    Chaine = GetStringInUnit(section.ProfilA.Tfi, Enu_TypeVariable.Dimension, 3, 1, False)
                End If
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- Tfs --

            If Not section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB Then

                If Not lLam Then

                    MyColor = StyleCouleur(iSelect, 2)
                    MyPen.Color = MyColor

                    yo = 0 + dCar / 2
                    ye = 0
                    xo = section.ProfilA.Bfs / 4 + dCar / 2
                    xe = xo
                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                    yo = 0 - section.ProfilA.Tfs
                    ye = yo - dCar
                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                    If lAffSymbol Then Chaine = "tfs" Else Chaine = GetStringInUnit(section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, 1, False)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                End If

            End If

            '--tp--

            If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Or section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Or section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB Then
                'présence d'un plat soudé 

                If section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB Or section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA Then 'plat soudé en partie inférieure
                    yo = -section.ProfilA.ha - dCar / 2
                    ye = -section.ProfilA.ha

                    xo = section.ProfilA.Plat_b / 2 - dCar
                    xe = xo

                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                    yo = -section.ProfilA.ha + section.ProfilA.Plat_t
                    ye = yo + dCar

                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                    If lAffSymbol Then
                        If lLam Then Chaine = "tp" Else Chaine = "tp"
                    Else
                        Chaine = GetStringInUnit(section.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 3, 1, False)
                    End If
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                Else
                    yo = dCar / 2
                    ye = 0

                    xo = section.ProfilA.Plat_b / 2 - dCar
                    xe = xo

                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                    yo = -section.ProfilA.Plat_t
                    ye = yo - dCar

                    AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

                    ye -= dCar

                    If lAffSymbol Then
                        If lLam Then Chaine = "tp" Else Chaine = "tp"
                    Else
                        Chaine = GetStringInUnit(section.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 3, 1, False)
                    End If
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                End If

            End If

            '-- R --

            If lLam Then

                MyColor = StyleCouleur(iSelect, 6)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                yo = 0 - section.ProfilA.Tfs - section.ProfilA.Rcs * (1 - kProj)
                ye = yo - dCar * kProj

                xo = section.ProfilA.Tw / 2 + section.ProfilA.Rcs * (1 - kProj)
                xe = xo + dCar * kProj

                AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(section.ProfilA.Rcs, Enu_TypeVariable.Dimension)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- tw --

            MyColor = StyleCouleur(iSelect, 5)
            MyPen.Color = MyColor

            xo = -section.ProfilA.Tw / 2
            xe = xo - dCar / 2
            yo = -Math.Max(section.ProfilA.Tfs + section.ProfilA.Rcs + 2 * dCar, section.ProfilA.ha / 4)
            ye = yo

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = section.ProfilA.Tw / 2
            xe = xo + dCar

            AddFleche(MyGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(section.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

    Public Sub DessinProfileSFBAcier(ByRef myGr As Graphics, ByVal mySection As cls_Section,
                                     ByVal Width As Decimal, ByVal Height As Decimal, ByVal myFont As Font,
                                     kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                     ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la mySection acier
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '       10 pour tp
        '       11 pour bp
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim lLam As Boolean = True

        Dim zRef As Decimal = 0

        Dim MyParAff As Struc_Affichage
        Dim lContour As Boolean = lCONTOURCOTE

        Dim zFSup As Decimal = mySection.ProfilA.hb

        '--> Préparation Pinceau dégradé

        Dim myBrushG As Brush

        If xLeft <> 0 Or yTop <> 0 Then
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, Color.DarkGray)
        Else
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, CouleurAcierNormal)
        End If

        '--> Initialisation des paramètres d'affichage

        If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
            xMin = -mySection.ProfilA.Plat_b / 2
            xMax = -xMin
        Else
            xMin = -mySection.ProfilA.Bfs / 2
            xMax = xMin + mySection.ProfilA.Plat_b
        End If

        yMin = -mySection.ProfilA.Plat_t
        yMax = mySection.ProfilA.hb

        dCar = Math.Sqrt((mySection.ProfilA.ha ^ 2 + (mySection.ProfilA.Plat_b) ^ 2)) / 20

        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, Width, Height, xLeft, yTop, kAdjust)

        '--> Dessin de la mySection acier

        DessinProfileMetal(myGr, mySection.ProfilA, myBrushG, MyParAff, zRef, Not MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire, 0)

        '--> Cotation

        If lCote Then

            Dim MyPen As New Pen(Color.Black, 1)
            Dim xo, yo As Double
            Dim xe, ye As Double
            Dim MyPenNormal As New Pen(ColorNonSelect, 1)
            Dim MyPenSelect As New Pen(ColorSelect, 1)
            'Dim MyFontNormal As Font = New Font(FontBase.Name, SizeFontFrm)
            Dim MyFontNormal As Font = myFont
            Dim MyColor As Color
            Dim Chaine As String

            '-- H --

            yo = zFSup - mySection.ProfilA.ha
            ye = zFSup

            If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
                xo = -mySection.ProfilA.Plat_b / 2 - dCar
                xe = xo
            Else
                xo = -mySection.ProfilA.Bfs / 2 - 2 * dCar
                xe = xo
            End If

            MyColor = StyleCouleur(iSelect, 0)
            MyPen.Color = MyColor

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(mySection.ProfilA.ha, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Hb --

            yo = zFSup - mySection.ProfilA.hb
            ye = zFSup

            If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
                xo = -(mySection.ProfilA.Plat_b + mySection.ProfilA.Bfi) / 4
                xe = xo
            Else
                xo = mySection.ProfilA.Bfs / 2 + dCar
                xe = xo
            End If

            MyColor = StyleCouleur(iSelect, 99)
            MyPen.Color = MyColor

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "hb" Else Chaine = GetStringNoUnit(mySection.ProfilA.hb, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)


            '-- Bf --

            yo = zFSup + dCar
            ye = yo
            xo = -mySection.ProfilA.Bfs / 2
            xe = -xo
            MyColor = StyleCouleur(iSelect, 99)
            MyPen.Color = MyColor

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "b" Else Chaine = GetStringNoUnit(mySection.ProfilA.Bfs, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bp --

            Dim iRef As Int16 = 11

            MyColor = StyleCouleur(iSelect, iRef)
            MyPen.Color = MyColor

            Select Case mySection.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA 'cas où on a un plat soudé dont la largeur est supérieure aux largeur des semelles
                    yo = -mySection.ProfilA.ha - dCar + zFSup

                    If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
                        xo = -mySection.ProfilA.Plat_b / 2
                    Else
                        xo = -mySection.ProfilA.Bfi / 2
                    End If

                Case Else
                    yo = -mySection.ProfilA.ha - dCar
                    xo = -mySection.ProfilA.Bfi / 2

            End Select

            ye = yo
            xe = xo + mySection.ProfilA.Plat_b

            If lAffSymbol Then
                If lLam Then
                    Chaine = "b"

                Else
                    Chaine = "bfi"
                End If
            Else
                Chaine = GetStringNoUnit(mySection.ProfilA.Plat_b, Enu_TypeVariable.Dimension)
            End If

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tp --

            iRef = 10
            MyColor = StyleCouleur(iSelect, iRef)
            MyPen.Color = MyColor

            yo = -mySection.ProfilA.ha - dCar / 2 + zFSup
            ye = -mySection.ProfilA.ha + zFSup
            xo = mySection.ProfilA.Bfi / 2 + (mySection.ProfilA.Plat_b - mySection.ProfilA.Bfi) / 4
            xe = xo
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo = -mySection.ProfilA.ha + mySection.ProfilA.Plat_t + zFSup
            ye = yo + dCar
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then
                If lLam Then Chaine = "tf" Else Chaine = "tfi"
            Else
                Chaine = GetStringInUnit(mySection.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 3, 1, False)
            End If
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tfs --

            If Not lLam Then

                MyColor = StyleCouleur(iSelect, 2)
                MyPen.Color = MyColor

                yo = 0 + dCar / 2 + zFSup
                ye = 0 + zFSup
                xo = mySection.ProfilA.Bfs / 2 - dCar
                xe = xo
                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

                yo = 0 - mySection.ProfilA.Tfs + zFSup
                ye = yo - dCar
                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "tfs" Else Chaine = GetStringInUnit(mySection.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, 1, False)
                AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- R --

            If lLam Then

                MyColor = StyleCouleur(iSelect, 6)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                yo = zFSup - mySection.ProfilA.Tfs - mySection.ProfilA.Rcs * (1 - kProj)
                ye = yo - dCar * kProj

                xo = mySection.ProfilA.Tw / 2 + mySection.ProfilA.Rcs * (1 - kProj)
                xe = xo + dCar * kProj

                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(mySection.ProfilA.Rcs, Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- tw --

            MyColor = StyleCouleur(iSelect, 5)
            MyPen.Color = MyColor

            xo = -mySection.ProfilA.Tw / 2
            xe = xo - dCar / 2
            yo = -(mySection.ProfilA.ha / 2 - mySection.ProfilA.Tfs - mySection.ProfilA.Rcs) * 0.8 + zFSup
            ye = yo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = mySection.ProfilA.Tw / 2
            xe = xo + dCar

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(mySection.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

    Public Sub DessinProfileIFB_A_Acier(ByRef myGr As Graphics, ByVal mySection As cls_Section, lInter As Boolean,
                                        ByVal sWi As Decimal, ByVal sHi As Decimal, ByVal myFont As Font,
                                        kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                        ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la section IFB
        '---------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   mySection   [E] :   Section à dessiner
        '   lInter      [E] :   Indique si poutre intermédiaire ou poutre de rive
        '   sWi, sHi    [E] :   Dimension de l'objet dans lequel on dessine
        '   myFont      [E] :
        '   kAdjust     [E] :   Facteur d'ajustement d'échelle (A pour utiliser la zone à 100%)
        '   lCote       [E] :   Indique si on affiche la cotation
        '   lAffSymbol  [E] :   Indique si on affiche les cotations avec des symboles ou des valeurs numériques
        '   iSelect     [E] :   Indice du paramètre sélectionné (-1 si rien de sélectionné)
        '   xLeft, yTop [E] :   Position de la zone de dessin (dans la NdC)
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '       0 pour ha
        '       1 pour hw
        '       2 pour bp
        '       3 pour tp
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Const iSELECT_HA As Integer = 0
        Const iSELECT_HW As Integer = 1
        Const iSELECT_BP As Integer = 2
        Const iSELECT_TP As Integer = 3
        Const iSELECT_NO As Integer = 99

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Dim lLam As Boolean = True

        Dim zRef As Decimal = 0

        Dim MyParAff As Struc_Affichage
        Dim lContour As Boolean = lCONTOURCOTE

        Dim zCote As Decimal = mySection.ProfilA.ha - mySection.ProfilA.Plat_t

        '--> Préparation Pinceau dégradé

        Dim myBrushG As Brush

        If xLeft <> 0 Or yTop <> 0 Then
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(sHi, sWi), Color.DarkGray, Color.DarkGray)
        Else
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(sHi, sWi), Color.DarkGray, CouleurAcierNormal)
        End If

        '--> Initialisation des paramètres d'affichage

        dCar = Math.Sqrt((mySection.ProfilA.ha ^ 2 + (mySection.ProfilA.Plat_b) ^ 2)) / 20

        If lInter Then
            xMin = -mySection.ProfilA.Plat_b / 2
            xMax = -xMin
        Else
            xMin = -mySection.ProfilA.Bfs / 2 - 2 * dCar
            xMax = mySection.ProfilA.Plat_b - mySection.ProfilA.Bfs / 2
        End If


        yMin = -mySection.ProfilA.Plat_t
        yMax = mySection.ProfilA.ha - mySection.ProfilA.Plat_t

        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, sHi, sWi, xLeft, yTop, kAdjust)

        '--> Dessin de la mySection acier

        DessinProfileMetal(myGr, mySection.ProfilA, myBrushG, MyParAff, zRef, Not lInter, 0)

        '--> Cotation

        If lCote Then

            Dim MyPen As New Pen(Color.Black, 1)
            Dim xo, yo As Double
            Dim xe, ye As Double
            Dim MyPenNormal As New Pen(ColorNonSelect, 1)
            Dim MyPenSelect As New Pen(ColorSelect, 1)
            'Dim MyFontNormal As Font = FontBase
            Dim MyFontNormal As Font = myFont
            Dim MyColor As Color
            Dim Chaine As String

            '-- H --

            yo = -mySection.ProfilA.ha + zCote
            ye = +zCote

            If lInter Then
                xo = -mySection.ProfilA.Plat_b / 2 - dCar
                xe = xo
            Else
                xo = -mySection.ProfilA.Bfs / 2 - dCar
                xe = xo
            End If

            MyColor = StyleCouleur(iSelect, iSELECT_HA)
            MyPen.Color = MyColor

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(mySection.ProfilA.ha, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bp --

            MyColor = StyleCouleur(iSelect, iSELECT_BP)
            MyPen.Color = MyColor


            yo = -mySection.ProfilA.ha - dCar + zCote

            If lInter Then
                xo = -mySection.ProfilA.Plat_b / 2
            Else
                xo = -mySection.ProfilA.Bfs / 2
            End If

            xe = xo + mySection.ProfilA.Plat_b

            ye = yo


            If lAffSymbol Then
                If lLam Then
                    Chaine = "b"

                Else
                    Chaine = "bfi"
                End If
            Else
                Chaine = GetStringNoUnit(mySection.ProfilA.Plat_b, Enu_TypeVariable.Dimension)
            End If

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfs --

            MyColor = StyleCouleur(iSelect, 1)
            MyPen.Color = MyColor

            yo = 0 + dCar + zCote
            ye = yo
            xo = mySection.ProfilA.Bfs / 2
            xe = -xo
            If lAffSymbol Then Chaine = "bfs" Else Chaine = GetStringNoUnit(mySection.ProfilA.Bfs, Enu_TypeVariable.Dimension)
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Hw --

            MyColor = StyleCouleur(iSelect, iSELECT_HW)
            MyPen.Color = MyColor

            yo = 0 - mySection.ProfilA.Tfs + zCote
            ye = -mySection.ProfilA.ha + mySection.ProfilA.Plat_t + zCote
            xo = -Math.Min(mySection.ProfilA.Bfs, mySection.ProfilA.Plat_b) / 2 + dCar
            xe = xo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "hw" Else Chaine = GetStringNoUnit(mySection.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Hauteur du Té --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = zCote
            ye = 0
            If lInter Then
                xo = -mySection.ProfilA.Bfs / 2 - (mySection.ProfilA.Plat_b - mySection.ProfilA.Bfs) / 4
            Else
                xo = mySection.ProfilA.Bfs / 2 + Math.Max(dCar, (mySection.ProfilA.Plat_b - mySection.ProfilA.Bfs) / 6)
            End If
            xe = xo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "hte" Else Chaine = GetStringNoUnit(zCote, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tp --

            MyColor = StyleCouleur(iSelect, iSELECT_TP)
            MyPen.Color = MyColor

            yo = -mySection.ProfilA.ha - dCar / 2 + zCote
            ye = -mySection.ProfilA.ha + zCote
            xo = mySection.ProfilA.Bfs / 2 + (mySection.ProfilA.Plat_b - mySection.ProfilA.Bfs) / 4
            xe = xo
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo = -mySection.ProfilA.ha + mySection.ProfilA.Plat_t + zCote
            ye = yo + dCar
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then
                If lLam Then Chaine = "tf" Else Chaine = "tfi"
            Else
                Chaine = GetStringInUnit(mySection.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 3, 1, False)
            End If
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tfs --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = 0 + dCar / 2 + zCote
            ye = 0 + zCote
            xo = mySection.ProfilA.Bfs / 2 - dCar
            xe = xo
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo = 0 - mySection.ProfilA.Tfs + zCote
            ye = yo - dCar
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tfs" Else Chaine = GetStringInUnit(mySection.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- R --

            If lLam Then

                MyColor = StyleCouleur(iSelect, iSELECT_NO)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                yo = 0 - mySection.ProfilA.Tfs - mySection.ProfilA.Rcs * (1 - kProj) + zCote
                ye = yo - dCar * kProj

                Const kSign As Decimal = -1
                xo = kSign * (mySection.ProfilA.Tw / 2 + mySection.ProfilA.Rcs * (1 - kProj))
                xe = xo + kSign * dCar * kProj

                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(mySection.ProfilA.Rcs, Enu_TypeVariable.Dimension)

                AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Right, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- tw --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            xo = -mySection.ProfilA.Tw / 2
            xe = xo - dCar / 2
            yo = -(mySection.ProfilA.ha / 2 - mySection.ProfilA.Tfs - mySection.ProfilA.Rcs) * 0.8 + zCote
            ye = yo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = mySection.ProfilA.Tw / 2
            xe = xo + dCar

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(mySection.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

    Public Sub DessinProfileIFB_B_Acier(ByRef myGr As Graphics, ByVal mySection As cls_Section,
                                        ByVal Width As Decimal, ByVal Height As Decimal, ByVal MyFont As Font,
                                        kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                        ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la Section acier IFB
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '       0 pour ha
        '       1 pour hw
        '       2 pour bp
        '       3 pour tp
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Const iSELECT_HA As Integer = 0
        Const iSELECT_HW As Integer = 1
        Const iSELECT_BP As Integer = 2
        Const iSELECT_TP As Integer = 3
        Const iSELECT_NO As Integer = 99

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Const lLam As Boolean = True

        Dim zRef As Decimal = 0

        Dim MyParAff As Struc_Affichage
        Dim lContour As Boolean = lCONTOURCOTE

        Dim zCote As Decimal = mySection.ProfilA.ha - mySection.ProfilA.Tfi

        '--> Préparation Pinceau dégradé

        Dim myBrushG As Brush

        If xLeft <> 0 Or yTop <> 0 Then
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, Color.DarkGray)
        Else
            myBrushG = New LinearGradientBrush(New PointF(0, 0), New PointF(Height, Width), Color.DarkGray, CouleurAcierNormal)
        End If

        '--> Initialisation des paramètres d'affichage

        xMin = -mySection.ProfilA.Bfi / 2
        xMax = -xMin

        yMin = -mySection.ProfilA.Tfi
        yMax = yMin + mySection.ProfilA.ha

        dCar = Math.Sqrt((mySection.ProfilA.ha ^ 2 + (mySection.ProfilA.Bfi) ^ 2)) / 20

        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, Width, Height, xLeft, yTop, kAdjust)

        '--> Dessin de la mySection acier

        DessinProfileMetal(myGr, mySection.ProfilA, myBrushG, MyParAff, zRef)

        '--> Cotation

        If lCote Then

            Dim MyPen As New Pen(Color.Black, 1)
            Dim xo, yo As Double
            Dim xe, ye As Double
            Dim MyPenNormal As New Pen(ColorNonSelect, 1)
            Dim MyPenSelect As New Pen(ColorSelect, 1)
            'Dim MyFontNormal As Font = FontBase
            Dim MyFontNormal As Font = MyFont
            Dim MyColor As Color
            Dim Chaine As String

            '-- H --

            yo = -mySection.ProfilA.ha + zCote
            ye = 0 + zCote

            xo = -mySection.ProfilA.Bfi / 2 - dCar
            xe = xo

            MyColor = StyleCouleur(iSelect, iSELECT_HA)
            MyPen.Color = MyColor

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(mySection.ProfilA.ha, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bp --

            MyColor = StyleCouleur(iSelect, iSELECT_BP)
            MyPen.Color = MyColor

            yo = dCar + zCote
            ye = yo

            xo = -mySection.ProfilA.Plat_b / 2
            xe = xo + mySection.ProfilA.Plat_b


            If lAffSymbol Then
                If lLam Then
                    Chaine = "b"

                Else
                    Chaine = "bfi"
                End If
            Else
                Chaine = GetStringNoUnit(mySection.ProfilA.Plat_b, Enu_TypeVariable.Dimension)
            End If

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfi --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = -mySection.ProfilA.ha - dCar + zCote
            ye = yo
            xo = mySection.ProfilA.Bfi / 2
            xe = -xo
            If lAffSymbol Then Chaine = "bfi" Else Chaine = GetStringNoUnit(mySection.ProfilA.Bfi, Enu_TypeVariable.Dimension)
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Hw --

            'If Not lLam Then

            MyColor = StyleCouleur(iSelect, iSELECT_HW)
            MyPen.Color = MyColor

            yo = 0 - mySection.ProfilA.Plat_t + zCote
            ye = -mySection.ProfilA.ha + mySection.ProfilA.Tfi + zCote
            xo = -Math.Min(mySection.ProfilA.Bfi, mySection.ProfilA.Plat_b) / 2 + dCar
            xe = xo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "hw" Else Chaine = GetStringNoUnit(mySection.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            'End If

            '-- Hauteur du Té --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = 0 - mySection.ProfilA.Plat_t + zCote
            ye = -mySection.ProfilA.ha + zCote
            xo = +Math.Max(mySection.ProfilA.Bfi, mySection.ProfilA.Plat_b) / 2 + dCar
            xe = xo
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "hte" Else Chaine = GetStringNoUnit(mySection.ProfilA.ha - mySection.ProfilA.Plat_t, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tp --

            MyColor = StyleCouleur(iSelect, iSELECT_TP)
            MyPen.Color = MyColor

            xo = 3 * mySection.ProfilA.Plat_b / 8
            xe = xo
            yo = 0 + zCote
            ye = yo + dCar / 2
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            yo = -mySection.ProfilA.Plat_t - dCar / 2 + zCote
            ye = -mySection.ProfilA.Plat_t + zCote

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo -= dCar
            ye = yo

            If lAffSymbol Then
                If lLam Then Chaine = "tf" Else Chaine = "tfi"
            Else
                Chaine = GetStringInUnit(mySection.ProfilA.Plat_t, Enu_TypeVariable.Dimension, 3, 1, False)
            End If
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tfi --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            xo = mySection.ProfilA.Bfi / 2 - dCar
            xe = xo

            yo = -mySection.ProfilA.ha + zCote
            ye = yo - dCar
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            yo = -mySection.ProfilA.ha + mySection.ProfilA.Tfi + dCar / 2 + zCote
            ye = -mySection.ProfilA.ha + mySection.ProfilA.Tfi + zCote
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, False, True)

            yo += dCar
            ye = yo

            If lAffSymbol Then Chaine = "tfi" Else Chaine = GetStringInUnit(mySection.ProfilA.Tfi, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- R --

            If lLam Then

                MyColor = StyleCouleur(iSelect, iSELECT_NO)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                'yo = 0 - mySection.ProfilA.Tfs - mySection.ProfilA.Rcs * (1 - kProj) + zCote
                'ye = yo - dCar * kProj
                yo = mySection.ProfilA.Rci * (1 - kProj)
                ye = yo + dCar * kProj

                xo = mySection.ProfilA.Tw / 2 + mySection.ProfilA.Rci * (1 - kProj)
                xe = xo + dCar * kProj

                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(mySection.ProfilA.Rci, Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- tw --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            xo = -mySection.ProfilA.Tw / 2
            xe = xo - dCar / 2
            yo = -(mySection.ProfilA.ha / 2 - mySection.ProfilA.Tfs - mySection.ProfilA.Rcs) * 0.8 + zCote
            ye = yo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = mySection.ProfilA.Tw / 2
            xe = xo + dCar

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(mySection.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

    Public Sub DessinProfileSAB_Acier(ByRef myGr As Graphics, ByVal mySection As cls_Section,
                                      ByVal Width As Decimal, ByVal Height As Decimal, ByVal MyFont As Font,
                                      kAdjust As Double, lCote As Boolean, lAffSymbol As Boolean, iSelect As Integer,
                                      ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   17/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Utilisé pour la fenêtre définition de la Section acier SAB
        '---------------------------------------------------------------------------------------------------------------------------
        '   Valeurs de iSelect: 
        '       0 pour bfs
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Const iSELECT_BFS As Integer = 0
        Const iSELECT_NO As Integer = 99

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar As Decimal
        Const lLam As Boolean = True

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
        yMin = -mySection.ProfilA.Tfi
        xMin = -Math.Max(mySection.ProfilA.Bfs, mySection.ProfilA.Bfi) / 2

        xMax = -xMin
        yMax = mySection.ProfilA.ha - mySection.ProfilA.Tfi

        dCar = Math.Sqrt((mySection.ProfilA.ha ^ 2 + (2 * xMin) ^ 2)) / 20

        yMin -= dCar
        yMax += dCar
        xMax += dCar
        xMin -= dCar
        'End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, Width, Height, xLeft, yTop, kAdjust)

        '--> Dessin de la mySection acier

        DessinProfileMetal(myGr, mySection.ProfilA, myBrushG, MyParAff, zRef)

        '--> Cotation

        If lCote Then

            Dim MyPen As New Pen(Color.Black, 1)
            Dim xo, yo As Double
            Dim xe, ye As Double
            Dim MyPenNormal As New Pen(ColorNonSelect, 1)
            Dim MyPenSelect As New Pen(ColorSelect, 1)
            'Dim MyFontNormal As Font = FontBase
            Dim MyFontNormal As Font = MyFont
            Dim MyColor As Color
            Dim Chaine As String

            '-- H --

            yo = mySection.ProfilA.ha - mySection.ProfilA.Tfi
            ye = -mySection.ProfilA.Tfi
            xo = -Math.Max(mySection.ProfilA.Bfs, mySection.ProfilA.Bfi) / 2 - dCar
            xe = xo

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "ha" Else Chaine = GetStringNoUnit(mySection.ProfilA.ha, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfi --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = -mySection.ProfilA.Tfi - dCar
            xo = mySection.ProfilA.Bfi / 2

            ye = yo
            xe = -xo

            If lAffSymbol Then
                Chaine = "b"

            Else
            Chaine = GetStringNoUnit(mySection.ProfilA.Bfi, Enu_TypeVariable.Dimension)
            End If

                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
                AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Bfs --

            MyColor = StyleCouleur(iSelect, iSELECT_BFS)
            MyPen.Color = MyColor

            yo = mySection.ProfilA.ha - mySection.ProfilA.Tfi + dCar
            ye = yo
            xo = mySection.ProfilA.Bfs / 2
            xe = -xo
            If lAffSymbol Then Chaine = "bfs" Else Chaine = GetStringNoUnit(mySection.ProfilA.Bfs, Enu_TypeVariable.Dimension)
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Hw --

            Dim Hw As Decimal = mySection.ProfilA.HauteurAmeHw

            MyColor = StyleCouleur(iSelect, 7)
                MyPen.Color = MyColor

            yo = 0
            ye = mySection.ProfilA.ha - mySection.ProfilA.Tfs - mySection.ProfilA.Tfi
            xo = -(mySection.ProfilA.Bfs / 4 + mySection.ProfilA.Tw / 4 + mySection.ProfilA.Rcs / 2)
            xe = xo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, True)
            If lAffSymbol Then Chaine = "hw" Else Chaine = GetStringNoUnit(Hw, Enu_TypeVariable.Dimension)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tfi --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = -mySection.ProfilA.Tfi
            ye = yo - dCar / 2

            xo = mySection.ProfilA.Bfi / 2 - dCar
            xe = xo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            yo = 0
            ye = yo + dCar

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)


            If lAffSymbol Then
                If lLam Then Chaine = "tf" Else Chaine = "tfi"
            Else
                Chaine = GetStringInUnit(mySection.ProfilA.Tfi, Enu_TypeVariable.Dimension, 3, 1, False)
            End If
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- Tfs --

            MyColor = StyleCouleur(iSelect, iSELECT_NO)
            MyPen.Color = MyColor

            yo = mySection.ProfilA.ha - mySection.ProfilA.Tfi
            ye = yo + dCar / 2
            xo = mySection.ProfilA.Bfs / 2 - dCar / 5
            xe = xo
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            yo = yo - mySection.ProfilA.Tfs
            ye = yo - dCar
            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            If lAffSymbol Then Chaine = "tfs" Else Chaine = GetStringInUnit(mySection.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xo, ye, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            '-- R --

            If lLam Then

                MyColor = StyleCouleur(iSelect, iSELECT_NO)
                MyPen.Color = MyColor

                Dim kProj As Decimal = Math.Sqrt(2) / 2

                yo = mySection.ProfilA.Rci * (1 - kProj)
                ye = yo + dCar * kProj

                xo = mySection.ProfilA.Tw / 2 + mySection.ProfilA.Rci * (1 - kProj)
                xe = xo + dCar * kProj

                AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
                If lAffSymbol Then Chaine = "r" Else Chaine = GetStringNoUnit(mySection.ProfilA.Rcs, Enu_TypeVariable.Dimension)
                AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

            '-- tw --

            MyColor = StyleCouleur(iSelect, 5)
            MyPen.Color = MyColor

            xo = -mySection.ProfilA.Tw / 2
            xe = xo - dCar / 2
            yo = Hw / 2
            ye = yo

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)

            xo = mySection.ProfilA.Tw / 2
            xe = xo + dCar

            AddFleche(myGr, MyPen, xo, yo, xe, ye, MyParAff, True, False)
            If lAffSymbol Then Chaine = "tw" Else Chaine = GetStringInUnit(mySection.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, 1, False)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

#End Region

#Region " Dessins en coupe pour les entraxes (FRM_PORTEE) "

    Public Sub DessinFrmCoupe(MyGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                              ByVal pWi As Decimal, ByVal pHi As Decimal,
                              kAdjust As Double, iSelect As Integer, lCote As Boolean,
                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   05/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du plancher en coupe
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police à utiliser pour les cotations
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
        Dim hMaxProfile As Decimal = myBeam.HauteurMaxiProfiles

        '--> Intialisations

        Largeur = myBeam.EntraxeD1 + myBeam.EntraxeD2
        Hauteur = myBeam.HauteurTotale
        dCar = Math.Sqrt(Largeur ^ 2 + Hauteur ^ 2) / 10
        LargeurBord = Largeur / 5

        '--> Initialisation des paramètres d'affichage

        'If myBeam.lIntermediaire Then
        xMin = -myBeam.EntraxeD1 - LargeurBord
        xMax = myBeam.EntraxeD2 + LargeurBord
        'End If

        If myBeam.Section.lSlimFloor Then
            yMin = -dCar
        Else
            yMin = -dCar - hMaxProfile
        End If
        yMax = myBeam.Dalle.zTop + dCar

        'If myBeam.NbTravees > 1 Then yMin -= dCar
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

        DessinFrmCoupeStandard(MyGr, myBeam, myFont, iSelect, dCar, hMaxProfile, MyParAff, myBrushP, myBrushPSel, myBrushB, myBrushT, myBrushA)

        '--> Fin

        myBrushA.Dispose()
        myBrushB.Dispose()
        myBrushT.Dispose()
        myBrushP.Dispose()
        myBrushPSel.Dispose()

    End Sub

    Private Sub DessinFrmCoupeStandard(ByRef MyGr As Graphics, ByVal myBeam As cls_Poutre, myFont As Font,
                                       iSelect As Integer, dCar As Decimal, hMaxProfile As Decimal,
                                       MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushPSel As Brush,
                                       myBrushB As Brush, myBrushT As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   05/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage du plancher en coupe
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Section à dessiner
        '   myFont      [E] :   Police à utiliser pour les cotations
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la mySection a été selectionnée
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
        Dim lEnrob As Boolean = myBeam.Section.lEnrobage
        Dim ZREF As Decimal

        'Select Case myBeam.Section.ProfilA.typeProfileAcier
        '    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
        '        ZREF = myBeam.Section.ProfilA.hb
        '    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
        '        ZREF = myBeam.Section.ProfilA.ha - myBeam.Section.ProfilA.Plat_t
        '    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
        '        ZREF = myBeam.Section.ProfilA.ha - myBeam.Section.ProfilA.Tfi
        '    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
        '        ZREF = myBeam.Section.ProfilA.ha - myBeam.Section.ProfilA.Tfi
        '    Case Else
        '        ZREF = 0
        'End Select

        '--> Affichage de la dalle béton

        If myBeam.lIntermediaire Then
            xo = -1.5 * myBeam.EntraxeD1
            xe = 1.5 * myBeam.EntraxeD2
        Else
            Select Case myBeam.Section.ProfilA.typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    xo = -myBeam.Section.ProfilA.Bfi / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    xo = -myBeam.Section.ProfilA.Bfs / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    xo = -myBeam.Section.ProfilA.Bfi / 2
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    xo = -myBeam.Section.ProfilA.Bfi / 2
                Case Else
                    xo = -myBeam.EntraxeD1
            End Select

            xe = 1.5 * myBeam.EntraxeD2
        End If

        yo = 0
        ye = myBeam.Dalle.Ep_td

        AddRectanglePlein(MyGr, myBrushB, MyPenContour, xo, yo, xe, ye, MyParaff1, True, False)

        '# Contours de la dalle

        AddLigne(MyGr, MyPenContour, xo, yo, xe, yo, MyParaff1)
        AddLigne(MyGr, MyPenContour, xo, ye, xe, ye, MyParaff1)

        If Not myBeam.lIntermediaire Then
            AddLigne(MyGr, MyPenContour, xo, yo, xo, ye, MyParaff1)
        End If

        '--> Affichage de la mySection principale

        '# Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, myBeam.Section, MyParaff1, myBrushB)

        '# Dessin de la mySection acier principale

        Dim lDrawRive As Boolean = False
        If Not myBeam.lIntermediaire And myBeam.Section.lSlimFloor Then
            lDrawRive = True
        End If

        DessinProfileMetal(MyGr, myBeam.Section.ProfilA, myBrushPSel, MyParaff1, ZREF, lDrawRive, 0)

        '--> Affichage de la voisine à gauche

        '# Dessin de béton d'enrobage

        If myBeam.lIntermediaire Then

            If lEnrob Then _
            DessinEnrobagePartielBeton(MyGr, myBeam.Section, MyParaff1, myBrushB, -myBeam.EntraxeD1)

            '# Dessin de la mySection acier

            DessinProfileMetal(MyGr, myBeam.Section.ProfilA, myBrushP, MyParaff1, ZREF, False, -myBeam.EntraxeD1)

        End If

        '--> Affichage de la voisine à droite

        '# Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, myBeam.Section, MyParaff1, myBrushB, +myBeam.EntraxeD2)

        '# Dessin de la mySection acier

        DessinProfileMetal(MyGr, myBeam.Section.ProfilA, myBrushP, MyParaff1, ZREF, False, myBeam.EntraxeD2)

        '--> Représentation des trémies

        If myBeam.lIntermediaire Then
            If myBeam.lTremieGauche Then
                xo = -myBeam.EntraxeD1 + myBeam.DistanceDsl1
                xe = -myBeam.DistanceDsl1
                yo = 0
                ye = myBeam.Dalle.Ep_td

                AddRectanglePlein(MyGr, myBrushT, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)
            End If

            If myBeam.lTremieDroite Then
                xo = myBeam.DistanceDsl2
                xe = myBeam.EntraxeD2 - myBeam.DistanceDsl2
                yo = 0
                ye = myBeam.Dalle.Ep_td

                AddRectanglePlein(MyGr, myBrushT, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)
            End If
        Else 'Poutre de rive
            If myBeam.lTremieDroite Then
                xo = myBeam.DistanceDsl2
                xe = myBeam.EntraxeD2 - myBeam.DistanceDsl2
                yo = 0
                ye = myBeam.Dalle.Ep_td

                AddRectanglePlein(MyGr, myBrushT, MyPenContour, xo, yo, xe, ye, MyParaff1, True, True)

            End If

        End If

        '--> Affichage des cotes

        DessinFrmCoupeCotes(MyGr, myBeam, myFont, MyParaff1, iSelect, dCar, hMaxProfile)

    End Sub

    Private Sub DessinFrmCoupeCotes(ByRef MyGr As Graphics, ByVal myBeam As cls_Poutre, myFont As Font, MyParaff1 As Struc_Affichage,
                                    iSelect As Integer, dCar As Decimal, hMaxProfile As Decimal)
        '------------------------------------------------------------------------------------------------------------------
        '   05/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des cotes du plancher en coupe
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police à utiliser pour les cotations
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

        Dim xo, xe As Decimal

        Dim yCote As Decimal = -hMaxProfile - dCar
        Dim yCoteS As Decimal = myBeam.Dalle.zTop + dCar
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyColor As Color
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim myFontNormal As New Font(myFont.Name, myFont.Size)
        Dim lContour As Boolean = lCONTOURCOTE

        If myBeam.Section.lSlimFloor Then
            yCote = -dCar
            yCoteS = myBeam.Dalle.zTop + dCar
        Else
            yCote = -hMaxProfile - dCar
            yCoteS = myBeam.Dalle.zTop + dCar
        End If

        '--> Entraxe à gauche

        If myBeam.lIntermediaire Or Not myBeam.Section.lSlimFloor Then

            MyColor = StyleCouleur(iSelect, 101)
            MyPen.Color = MyColor

            xo = -myBeam.EntraxeD1
            xe = 0

            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParaff1, True, True)

            If lAffSymbol Then Chaine = "D1" Else Chaine = GetStringInUnit(myBeam.EntraxeD1, Enu_TypeVariable.Longueur, 4, 2, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFontNormal, 0.5 * (xo + xe), yCote, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If
        '--> Entraxe à droite

        MyColor = StyleCouleur(iSelect, 102)
        MyPen.Color = MyColor

        xo = 0
        xe = myBeam.EntraxeD2

        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParaff1, True, True)

        If lAffSymbol Then Chaine = "D2" Else Chaine = GetStringInUnit(myBeam.EntraxeD2, Enu_TypeVariable.Longueur, 4, 2, False)
        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFontNormal, 0.5 * (xo + xe), yCote, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        '--> Trémie Gauche

        If myBeam.lTremieGauche Then
            MyColor = StyleCouleur(iSelect, 103)
            MyPen.Color = MyColor

            xo = 0
            xe = -myBeam.DistanceDsl1

            AddFleche(MyGr, MyPen, xo, yCoteS, xe, yCoteS, MyParaff1, True, True)

            If lAffSymbol Then Chaine = "Dsl1" Else Chaine = GetStringInUnit(myBeam.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFontNormal, 0.5 * (xo + xe), yCoteS, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

        '--> Trémie Droite

        If myBeam.lTremieDroite Then
            MyColor = StyleCouleur(iSelect, 104)
            MyPen.Color = MyColor

            xo = 0
            xe = myBeam.DistanceDsl2

            AddFleche(MyGr, MyPen, xo, yCoteS, xe, yCoteS, MyParaff1, True, True)

            If lAffSymbol Then Chaine = "Dsl2" Else Chaine = GetStringInUnit(myBeam.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, False)
            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFontNormal, 0.5 * (xo + xe), yCoteS, MyParaff1, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

        End If

    End Sub

    Private Sub DessinFrmCoupeSFB()

    End Sub

#End Region

#Region " Dessins pour la portée (FRM_PORTEE) "

    Public Sub DessinFrmPortee(MyGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                               ByVal pWi As Decimal, ByVal pHi As Decimal,
                               kAdjust As Double, iSelect As Integer, lCote As Boolean,
                               ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   02/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Affichage des travées dans la fenêtre portées
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police à utiliser pour les cotations
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
        ' Dim MyBrushA As New SolidBrush(Color.LightGray)
        Dim MyBrushA As New SolidBrush(CouleurProfile)
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyPenC As New Pen(Color.DarkGray, 1)
        Dim MyColor As Color
        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim myBrushB As Brush
        If xLeft <> 0 Or yTop <> 0 Then
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.Gray, Color.Gray)
        Else
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurDalle, Color.Gray)
        End If
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        'Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE
        Dim lMixte As Boolean = myBeam.lMixte

        '--> Initialisations

        LongueurPoutre = myBeam.LongueurTotale
        HauteurPoutre = myBeam.HauteurTotale
        LongueurDalle = myBeam.LongueurTotale
        HauteurDalle = myBeam.Dalle.Ep_td
        dCar = Math.Sqrt(LongueurPoutre ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = HauteurPoutre / 2

        '--> Initialisation des paramètres d'affichage

        xMin = 0
        xMax = LongueurPoutre
        yMin = -dCar - dCarApp
        yMax = HauteurPoutre + dCar

        If myBeam.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre 

        xe = 0
        yo = 0
        ye = HauteurPoutre

        For i As Integer = myBeam.IndicePremiereTravee To myBeam.IndiceDerniereTravee

            xo = xe
            xe = xo + myBeam.LongueurTravee(i)

            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Next

        '--> Représentation des appuis

        For i As Integer = 1 To myBeam.NombreTraveesDeuxAppuis

            xo = myBeam.xPositionAppui(True, i)
            DessineAppui(MyGr, xo, dCarApp, MyParAff)

        Next

        xo = myBeam.xPositionAppui(False, myBeam.NombreTraveesDeuxAppuis)
        DessineAppui(MyGr, xo, dCarApp, MyParAff)

        '--> Représentation de la dalle

        If Not myBeam.Section.lSlimFloor Then
            xo = 0
            yo = HauteurPoutre

            xe = LongueurDalle
            ye = HauteurPoutre + HauteurDalle

            AddRectanglePlein(MyGr, myBrushB, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        End If

        '--> Représentation de la continuité de dalle

        If lMixte Then
            If (Not myBeam.lTraveeConsoleGauche) And myBeam.lDalleContinueGauche Then

                xo = 0
                xe = -dCarApp / 2

                AddLigne(MyGr, MyPenC, xo, yo, xe, yo, MyParAff)
                AddLigne(MyGr, MyPenC, xo, ye, xe, ye, MyParAff)

            End If

            If (Not myBeam.lTraveeConsoleDroite) And myBeam.lDalleContinueDroite Then

                xo = LongueurDalle
                xe = LongueurDalle + dCarApp / 2

                AddLigne(MyGr, MyPenC, xo, yo, xe, yo, MyParAff)
                AddLigne(MyGr, MyPenC, xo, ye, xe, ye, MyParAff)

            End If

        End If

        '=== COTES =======================================================

        If lCote Then

            Dim yCote As Decimal = -dCar - dCarApp

            xo = 0
            xe = 0

            ' Travée console gauche

            If myBeam.lTraveeConsoleGauche Then

                MyColor = StyleCouleur(iSelect, 0)
                MyPen.Color = MyColor

                xo = 0
                xe = myBeam.LongueurTravee(0)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Lg" Else Chaine = GetStringInUnit(myBeam.LongueurTravee(0), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True
            End If

            ' Travées principales

            For i As Integer = 1 To myBeam.NombreTraveesDeuxAppuis

                MyColor = StyleCouleur(iSelect, i)
                MyPen.Color = MyColor

                xo = xe
                xe += myBeam.LongueurTravee(i)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnit(myBeam.LongueurTravee(i), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            Next

            ' Travée console droite

            If myBeam.lTraveeConsoleDroite Then

                MyColor = StyleCouleur(iSelect, 99)
                MyPen.Color = MyColor

                xo = xe
                xe += myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Ld" Else Chaine = GetStringInUnit(myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True

            End If

            ' Longueur totale si plusieurs travées

            If lTotal Then

                xo = 0
                xe = LongueurPoutre

                AddFleche(MyGr, MyPen, xo, yCote - dCar, xe, yCote - dCar, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnit(LongueurPoutre, Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, myFont, 0.5 * (xo + xe), yCote - dCar, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

            End If

        End If

        MyPen.Dispose()
        MyPenC.Dispose()
        MyBrushA.Dispose()
        myBrushB.Dispose()

    End Sub

    Public Sub DessineAppui(MyGr As Graphics, xPos As Decimal, dCar As Decimal, MyParAff As Struc_Affichage, Optional yPos As Decimal = 0)
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

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim MyBrushAp As New SolidBrush(CouleurAppui)

        '--> Initialisations

        PrepareContourAppui(xPos, dCar, xPts, yPts, nbPts, yPos)

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPts, yPts, nbPts, MyParAff, True, True)

    End Sub

    Private Sub PrepareContourAppui(xPos As Decimal, dCar As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer, Optional yPos As Decimal = 0)
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
        yo = yPos '0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos - dCar / 2
        yo = yPos - dCar

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos + dCar / 2
        yo = yPos - dCar

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub

#End Region

#Region "Dessins pour la connection (FRM_CONNECTION)"

    Public Sub DessinFrmConnection_Connecteurs(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, kAdjust As Double,
                                               myBeam As cls_Poutre,
                                               ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'une vignette montrant un zoom sur le connecteur
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '   myBeam      [E] :   Poutre dont on dessine la connexion 
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim myParAff As Struc_Affichage

        Dim ColorPen As Color = Color.Blue
        Dim ColorRedPen As Color = Color.Red

        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurAcier As Color = CouleurAcierNormal
        Dim CouleurConnect As Color = CouleurConnecteurNormal

        Dim myBrushBac As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.LightGray, Color.DarkGray)
        Dim myBrushBeton As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.Gray, CouleurBeton)
        'Dim myBrushBac As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.Gray, CouleurBeton)
        Dim myBrushProfilA As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), CouleurAcier, CouleurAcier)
        Dim myBrushConnecteur As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), CouleurConnect, CouleurConnect)

        Dim MyPenBrush As New SolidBrush(ColorPen)
        Dim MyPenRedBrush As New SolidBrush(ColorRedPen)
        Dim MyPen As New Pen(ColorPen)
        Dim MyPenRed As New Pen(ColorRedPen)
        Dim MyFontNormal As Font = FontBase
        Dim myPenDash As New Pen(Color.Black, 0.75)

        'Dim xPts() As Single = Nothing
        'Dim yPts() As Single = Nothing
        'Dim nbPts As Integer
        Dim lMixte As Boolean = (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte)
        Dim lParallel As Boolean = (myBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele)
        Dim lCofraplus220 As Boolean = myBeam.Dalle.Bac.lCofraplus220
        'Dim lNervureContinue As Boolean = Not (myBeam.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu)
        Dim Bfs, Tfs, Tw, zTop As Decimal
        Dim xo As Decimal
        Dim Chaine As String
        Dim dGouj, hSc As Decimal
        Dim xCote As Decimal

        '--( Initialisation

        Bfs = myBeam.Section.ProfilA.Bfs
        Tfs = myBeam.Section.ProfilA.Tfs
        Tw = myBeam.Section.ProfilA.Tw
        zTop = myBeam.Dalle.zTop
        dGouj = myBeam.Dalle.Goujons.d
        hSc = myBeam.Dalle.Goujons.hsc
        xCote = -0.75 * Bfs

        '--( Préparation des paramètres d'affichage

        DessinFrmConnection_Connecteurs_myParAff(myBeam, pWi, pHi, kAdjust, xLeft, yTop, myParAff)

        '--( Dessin de la dalle

        If lMixte Then
            If lParallel Then

                DessinFrmConnection_Connecteurs_DalleMixteParallel(myGr, myParAff, myBeam, 2 * myBeam.Dalle.Bac.Ep, myBrushBeton, myBrushBac, myPenDash)
                xCote = -0.75 * myBeam.Dalle.Bac.Ep

            ElseIf lCofraplus220 Then

                DessinFrmConnection_Connecteurs_DalleMixteCofraplus220(myGr, myParAff, myBeam, 2 * Bfs, myBrushBeton, myBrushBac, myPenDash)

            Else

                DessinFrmConnection_Connecteurs_DalleMixteTrans(myGr, myParAff, myBeam, 2 * Bfs, myBrushBeton, myBrushBac, myPenDash)

            End If
        Else

            DessinFrmConnection_Connecteurs_DallePleine(myGr, myParAff, myBeam, 2 * Bfs, myBrushBeton)

        End If

        '--( Dessin du connecteur

        Dim xGoujon As Decimal = 0
        Dim yGoujon As Decimal = 0

        'Dessin du corps du goujon
        AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - dGouj / 2, yGoujon, xGoujon + dGouj / 2, yGoujon + hSc, myParAff, True, True)
        'Dessin de la tete du goujon
        Dim dTete, hTete As Decimal
        myBeam.Dalle.Goujons.DimensionsTete(dTete, hTete)

        AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - dTete / 2, yGoujon + hSc - hTete, xGoujon + dTete / 2, yGoujon + hSc, myParAff, True, True)

        '--( Dessin de la semelle supérieure et de l'âme de la poutre

        Dim xSemelleSup As Decimal = 0
        Dim ySemelleSup As Decimal = 0
        AddRectanglePlein(myGr, myBrushProfilA, MyPenContour, xSemelleSup - Bfs / 2, ySemelleSup - Tfs, xSemelleSup + Bfs / 2, ySemelleSup, myParAff, True, True)

        Dim xAme As Decimal = xSemelleSup
        Dim yAme As Decimal = ySemelleSup - Tfs
        AddRectanglePlein(myGr, myBrushProfilA, MyPenContour, xAme - Tw / 2, yAme - myBeam.Section.ProfilA.HauteurAmeHw, xAme + Tw / 2, yAme, myParAff, True, True)

        '--( Cotation

        AddFleche(myGr, MyPenContour, xCote, 0, xCote, zTop, myParAff, True, True)

        Chaine = GetStringInUnitN(zTop, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)

        AddTexte(myGr, New SolidBrush(Color.Black), Chaine, MyFontNormal, xCote, 0.5 * zTop, myParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)

    End Sub

    Private Sub DessinFrmConnection_Connecteurs_DalleMixteTrans(myGr As Graphics, myParAff As Struc_Affichage, myBeam As cls_Poutre, bDalle As Decimal,
                                                                myBrushBeton As Brush, myBrushBac As Brush, myPenBac As Pen)
        '-----------------------------------------------------------------------------------------------
        '   18/12/24 :  Version 1.00 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'une dalle mixte transversale pour la vignette montrant un zoom sur le connecteur
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine la connexion 
        '   myParAff    [E] :   Paramètres d'affichage
        '   bDalle      [E] :   Largeur de la dalle représentée
        '   myBrushBeton[E] :   Pinceau utilisé pour le béton
        '   myBrushBac  [E] :   Pinceau utilisé pour le bac
        '   myPenBac    [E] :   Stylo pour le contour du bac
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lNervCont As Boolean = Not (myBeam.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu)
        Dim zTop As Decimal = myBeam.Dalle.zTop
        Dim Hp As Decimal = myBeam.Dalle.Bac.Hp
        Dim Bfs As Decimal = myBeam.Section.ProfilA.Bfs
        Const bApp As Decimal = 0.05

        '--( Dalle pleine

        AddRectanglePlein(myGr, myBrushBeton, MyPenContour, -bDalle / 2, 0, bDalle / 2, myBeam.Dalle.Ep_td, myParAff, True, False)

        '--( Bacs

        If lNervCont Then

            AddRectanglePlein(myGr, myBrushBac, MyPenContour, -bDalle / 2, 0, +bDalle / 2, Hp, myParAff, True, False)
            AddLigne(myGr, myPenBac, -bDalle / 2, Hp, bDalle / 2, Hp, myParAff)

        Else

            AddRectanglePlein(myGr, myBrushBac, MyPenContour, -bDalle / 2, 0, -Bfs / 2 + bApp, Hp, myParAff, True, False)
            AddRectanglePlein(myGr, myBrushBac, MyPenContour, bDalle / 2, 0, +Bfs / 2 - bApp, Hp, myParAff, True, False)
            AddLigne(myGr, myPenBac, -Bfs / 2 + bApp, 0, -Bfs / 2 + bApp, Hp, myParAff)
            AddLigne(myGr, myPenBac, -Bfs / 2 + bApp, Hp, -bDalle / 2, Hp, myParAff)
            AddLigne(myGr, myPenBac, Bfs / 2 - bApp, 0, Bfs / 2 - bApp, Hp, myParAff)
            AddLigne(myGr, myPenBac, Bfs / 2 - bApp, Hp, bDalle / 2, Hp, myParAff)

        End If

        '--( compléments lignes de la dalle

        AddLigne(myGr, MyPenContour, -bDalle / 2, zTop, bDalle / 2, zTop, myParAff)
        AddLigne(myGr, MyPenContour, -bDalle / 2, 0, bDalle / 2, 0, myParAff)

    End Sub

    Private Sub DessinFrmConnection_Connecteurs_DallePleine(myGr As Graphics, myParAff As Struc_Affichage, myBeam As cls_Poutre, bDalle As Decimal,
                                                            myBrushBeton As Brush)
        '-----------------------------------------------------------------------------------------------
        '   18/12/24 :  Version 1.00 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'une dalle pleine pour la vignette montrant un zoom sur le connecteur
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine la connexion 
        '   myParAff    [E] :   Paramètres d'affichage
        '   bDalle      [E] :   Largeur de la dalle représentée
        '   myBrushBeton[E] :   Pinceau utilisé pour le béton
        '-----------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lRenformis As Boolean = Not IsEqual(myBeam.Dalle.EpRenformis, 0)
        Dim zTop As Decimal = myBeam.Dalle.zTop

        '--( Traitement

        If lRenformis Then

            ' == Dalle pleine avec renformis

            '----| Déclarations locales 
            Dim xPts() As Single = Nothing
            Dim yPts() As Single = Nothing
            Dim nbPts As Integer
            Dim Bfs As Decimal
            Dim Th, xTh As Decimal
            Dim ThetaRd As Decimal

            '----| Initialisations

            Bfs = myBeam.Section.ProfilA.Bfs
            Th = myBeam.Dalle.Ep_th
            ThetaRd = myBeam.Dalle.ThetaRd
            xTh = Th * Math.Tan(ThetaRd)

            nbPts = 0
            AjoutePoint(-Bfs / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(-Bfs / 2 - xTh, Th, xPts, yPts, nbPts)
            AjoutePoint(-bDalle / 2, Th, xPts, yPts, nbPts)
            AjoutePoint(-bDalle / 2, zTop, xPts, yPts, nbPts)
            AjoutePoint(bDalle / 2, zTop, xPts, yPts, nbPts)
            AjoutePoint(bDalle / 2, Th, xPts, yPts, nbPts)
            AjoutePoint(Bfs / 2 + xTh, Th, xPts, yPts, nbPts)
            AjoutePoint(Bfs / 2, 0, xPts, yPts, nbPts)

            RemplirZone(myGr, myBrushBeton, xPts, yPts, nbPts, myParAff, False)

            AddLigne(myGr, MyPenContour, -bDalle / 2, zTop, bDalle / 2, zTop, myParAff)

            AddLigne(myGr, MyPenContour, -bDalle / 2, Th, -Bfs / 2 - xTh, Th, myParAff)
            AddLigne(myGr, MyPenContour, bDalle / 2, Th, Bfs / 2 + xTh, Th, myParAff)

            AddLigne(myGr, MyPenContour, -Bfs / 2, 0, -Bfs / 2 - xTh, Th, myParAff)
            AddLigne(myGr, MyPenContour, Bfs / 2, 0, Bfs / 2 + xTh, Th, myParAff)

            AddLigne(myGr, MyPenContour, Bfs / 2, 0, -Bfs / 2, 0, myParAff)

        Else

            ' == Dalle pleine standart

            AddRectanglePlein(myGr, myBrushBeton, MyPenContour, -bDalle / 2, 0, bDalle / 2, myBeam.Dalle.Ep_td, myParAff, True, False)

            '--( compléments lignes de la dalle

            AddLigne(myGr, MyPenContour, -bDalle / 2, zTop, bDalle / 2, zTop, myParAff)
            AddLigne(myGr, MyPenContour, -bDalle / 2, 0, bDalle / 2, 0, myParAff)

        End If


    End Sub

    Private Sub DessinFrmConnection_Connecteurs_DalleMixteParallel(myGr As Graphics, myParAff As Struc_Affichage, myBeam As cls_Poutre, bDalle As Decimal,
                                                                   myBrushBeton As Brush, myBrushBac As Brush, myPenBac As Pen)
        '-----------------------------------------------------------------------------------------------
        '   18/12/24 :  Version 1.00 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'une dalle mixte parallèle pour la vignette montrant un zoom sur le connecteur
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine la connexion 
        '   myParAff    [E] :   Paramètres d'affichage
        '   bDalle      [E] :   Largeur de la dalle représentée
        '   myBrushBeton[E] :   Pinceau utilisé pour le béton
        '   myBrushBac  [E] :   Pinceau utilisé pour le bac
        '   myPenBac    [E] :   Stylo pour le contour du bac
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim zTop As Decimal = myBeam.Dalle.zTop
        Dim EpBac As Decimal = myBeam.Dalle.Bac.Ep

        '--( Dessin du bac

        '--> Calcul des points du pourtour du bac
        myBeam.Dalle.Bac.PrepareContourBacSimple1Nervure(xPts, yPts, nbPts)

        '--> Remplissage contour
        RemplirZone(myGr, myBrushBac, xPts, yPts, nbPts, myParAff, True)

        '--( Dessin de la dalle béton

        '--> Calcul des points du pourtour de la dalle

        Dim xPts_Dalle(xPts.Length / 2 + 1) As Single
        Dim yPts_Dalle(xPts.Length / 2 + 1) As Single

        For i As Integer = 0 To xPts.Length / 2 - 1
            xPts_Dalle(i) = xPts(i)
            yPts_Dalle(i) = yPts(i)
        Next
        xPts_Dalle(xPts.Length / 2) = xPts(xPts.Length / 2 - 1)
        xPts_Dalle(xPts.Length / 2 + 1) = xPts(0)

        yPts_Dalle(xPts.Length / 2) = myBeam.Dalle.Ep_td
        yPts_Dalle(xPts.Length / 2 + 1) = myBeam.Dalle.Ep_td

        nbPts = xPts_Dalle.Length

        '--> Remplissage contour

        RemplirZone(myGr, myBrushBeton, xPts_Dalle, yPts_Dalle, nbPts, myParAff, False)

        '--( Traits de finition

        AddLigne(myGr, MyPenContour, -EpBac / 2, zTop, +EpBac / 2, zTop, myParAff)

    End Sub

    Private Sub DessinFrmConnection_Connecteurs_DalleMixteCofraplus220(myGr As Graphics, myParAff As Struc_Affichage, myBeam As cls_Poutre, bDalle As Decimal,
                                                                       myBrushBeton As Brush, myBrushBac As Brush, myPenBac As Pen)
        '-----------------------------------------------------------------------------------------------
        '   18/12/24 :  Version 1.00 - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin d'une dalle mixte avec Cofraplus 220 pour la vignette montrant un zoom sur le connecteur
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myBeam      [E] :   Poutre dont on dessine la connexion 
        '   myParAff    [E] :   Paramètres d'affichage
        '   bDalle      [E] :   Largeur de la dalle représentée
        '   myBrushBeton[E] :   Pinceau utilisé pour le béton
        '   myBrushBac  [E] :   Pinceau utilisé pour le bac
        '   myPenBac    [E] :   Stylo pour le contour du bac
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim zTop As Decimal
        Dim Bfs, Hp As Decimal
        Dim xo As Decimal

        '--( Initialisation

        zTop = myBeam.Dalle.zTop
        Bfs = myBeam.Section.ProfilA.Bfs
        Hp = myBeam.Dalle.Bac.Hp

        '--( Préparation du contour de la dalle

        nbPts = 0
        AjoutePoint(-Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(Bfs / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(bDalle / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(bDalle / 2, zTop, xPts, yPts, nbPts)
        AjoutePoint(-bDalle / 2, zTop, xPts, yPts, nbPts)
        AjoutePoint(-bDalle / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(-Bfs / 2, -Hp, xPts, yPts, nbPts)

        RemplirZone(myGr, myBrushBeton, xPts, yPts, nbPts, myParAff, False, True)

        '--( Partie bacs

        AddRectanglePlein(myGr, myBrushBac, MyPenContour, -bDalle / 2, 0, -Bfs / 2, -Hp, myParAff, True, False)
        AddRectanglePlein(myGr, myBrushBac, MyPenContour, bDalle / 2, 0, +Bfs / 2, -Hp, myParAff, True, False)

        '--( Ligne de contour

        AddLigne(myGr, MyPenContour, -bDalle / 2, zTop, bDalle / 2, zTop, myParAff)

        xo = Bfs / 2
        AddLigne(myGr, MyPenContour, xo, 0, xo, -Hp, myParAff)
        AddLigne(myGr, MyPenContour, -xo, 0, -xo, -Hp, myParAff)
        AddLigne(myGr, MyPenContour, xo, 0, -xo, 0, myParAff)

        AddLigne(myGr, myPenBac, -bDalle / 2, 0, -Bfs / 2, 0, myParAff)
        AddLigne(myGr, myPenBac, bDalle / 2, 0, Bfs / 2, 0, myParAff)

    End Sub

    Private Sub DessinFrmConnection_Connecteurs_myParAff(myBeam As cls_Poutre, pWi As Single, pHi As Single, kAdjust As Double,
                                                         xLeft As Decimal, yTop As Decimal, ByRef myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/12/24 :  Version 1.00 - POM
        '-----------------------------------------------------------------------------------------------
        '   Prépare le myParAff utilisé dans DessinFrmConnection_Connecteurs
        '   Dessin d'une vignette montrant un zoom sur le connecteur
        '-----------------------------------------------------------------------------------------------
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '   myBeam      [E] :   Poutre dont on dessine la connexion 
        '   myParAff    [S] :   Paramètres d'affichage
        '-----------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim xMin, yMin, xMax, yMax As Double
        Dim lMixte As Boolean = (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte)
        Dim lParallel As Boolean = (myBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele)

        '--( Valeurs enveloppes

        If lMixte And lParallel Then

            xMax = myBeam.Dalle.Bac.Ep / 2

        ElseIf lMixte Then

            xMax = myBeam.Section.ProfilA.Bfs

        Else

            xMax = myBeam.Section.ProfilA.Bfs + myBeam.Dalle.Ep_th * Math.Tan(myBeam.Dalle.ThetaRd)

        End If
        xMin = -xMax

        yMin = -myBeam.Section.ProfilA.Tfs
        yMax = Math.Max(myBeam.Dalle.zTop, myBeam.Dalle.Goujons.hsc)

        '--( Preparation de myparaff

        ParametresAffichage(myParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

    End Sub

    Public Sub DessinFrmConnection_Connection(MyGr As Graphics, MyPoutre As cls_Poutre, myFont As Font,
                                              ByVal pWi As Decimal, ByVal pHi As Decimal,
                                              kAdjust As Double, indTravee As Integer, ByVal lCote As Boolean, strStuds As String,
                                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/07/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Dessin de la connexion le long d'une poutre normale
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   indTravee   [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '   strStuds    [E] :   Indique la traduction associée au mot "goujons"
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

        'Dim CouleurConnecteur As Color = Color.White
        Dim CouleurConnecteur As Color = CouleurConnecteurNormal
        Dim myBrushC As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurConnecteur)
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = myFont
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Initialisations

        LongueurTravee = cls_Poutre.PORTEEDEFAUT
        LargeurSemelle = LongueurTravee / 8.5
        If Not MyPoutre.lAutomaticDesign Then
            NombreZones = MyPoutre.NombreZones(indTravee)
            For i As Integer = 0 To 2
                NombreGoujonsTrans(i) = MyPoutre.NrTransZone(indTravee, i)
                LongueurZones(i) = MyPoutre.LongueurZone(indTravee, i) / MyPoutre.LongueurTravee(indTravee) * LongueurTravee
                NombreGoujonsLongiZone(i) = 0.75 * MyPoutre.LongueurZone(indTravee, i) / MyPoutre.EspacementZone(indTravee, i)
            Next
        Else
            NombreGoujonsTrans(0) = 1
            LongueurZones(0) = LongueurTravee
            NombreGoujonsLongiZone(0) = 0.75 * MyPoutre.LongueurZone(indTravee, 0) / 0.2
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

        xo = 0 '- dCar / 2
        xe = LongueurTravee ' + dCar / 2


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
                    If Not (xo >= LongueurTravee - DiametreGoujons Or xo <= DiametreGoujons) Then
                        yo = -LargeurSemelle / 2 + k * LargeurSemelle / (NombreGoujonsTrans(i) + 1)
                        AddCerclePlein(MyGr, myBrushC, xo, yo, DiametreGoujons, MyParAff, True)
                    End If

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
                'If lAffSymbol Then Chaine = "L" Else Chaine = GetStringNoUnit(myBeam.Longueur_Zone(indTravee, i), Enu_TypeVariable.Longueur)
                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnitN(MyPoutre.LongueurZone(indTravee, i), Enu_TypeVariable.Longueur, 4, 2, NON_U, True)
                AddTexteFond(MyGr, New SolidBrush(Color.Black), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                'On dessinne la côte supérieure qui donne le nombre de goujons disposés sur la zone étudiée 
                yCote = LargeurSemelle / 2 + dCar

                AddFleche(MyGr, New Pen(Color.Red), xo, yCote, xe, yCote, MyParAff, True, True)
                'Chaine = GetStringNoUnit(Math.Floor(myBeam.ZoneLongueur(indTravee, i) / myBeam.ZoneEspacement(indTravee, i)), Enu_TypeVariable.SansType) & " " & strStuds
                Chaine = GetStringNoUnit(MyPoutre.NombreGoujonTotParZone(indTravee, i), Enu_TypeVariable.SansType) & " " & strStuds
                AddTexteFond(MyGr, New SolidBrush(Color.Red), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            Next

        End If

    End Sub

#End Region

#Region " Dessins pour Frm_ConnectionSlimConnexion "

    Public Sub DessinFrmConnection_ConnectionSlim(myGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                                                  ByVal pWi As Decimal, ByVal pHi As Decimal,
                                                  kAdjust As Double, indTravee As Integer, ByVal lCote As Boolean, strStuds As String,
                                                  ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/07/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Dessin de la connexion le long d'une poutre slim floor
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   indTravee   [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '   strStuds    [E] :   Indique la traduction associée au mot "goujons"
        '------------------------------------------------------------------------------------------------------------------

        Select Case myBeam.Dalle.typeConnecteur
            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                DessinFrmConnection_Connection(myGr, myBeam, myFont, pWi, pHi, kAdjust, indTravee, lCote, strStuds, xLeft, yTop)
            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
                DessinFrmConnectionSlim_ConnexAme(myGr, myBeam, myFont, pWi, pHi, kAdjust, indTravee, lCote, strStuds, False, xLeft, yTop)
            Case cls_Dalle.Enum_TypeConnecteur.ArmatureAme
                DessinFrmConnectionSlim_ConnexAme(myGr, myBeam, myFont, pWi, pHi, kAdjust, indTravee, lCote, strStuds, True, xLeft, yTop)
        End Select

    End Sub

    Private Sub DessinFrmConnectionSlim_ConnexAme(myGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                                                  ByVal pWi As Decimal, ByVal pHi As Decimal,
                                                  kAdjust As Double, indTravee As Integer, ByVal lCote As Boolean, strStuds As String,
                                                  lArma As Boolean, ByVal xLeft As Decimal, ByVal yTop As Decimal)
        '------------------------------------------------------------------------------------------------------------------
        '   28/07/25 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Dessin de la connexion le long d'une poutre slim floor, cas de la connexion par goujons soudés sur l'âme
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police pour les cotes
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   indTravee   [E] :   Indique quel est la travée sélectionnée
        '   lCote       [E] :   Indique si affichage de la cote
        '   strStuds    [E] :   Indique la traduction associée au mot "goujons"
        '   lArma       [E] :   Indique si connexion par des armatures dans l'âme
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
        Dim Bfi As Decimal
        Dim BPlat As Decimal
        Dim tW As Decimal
        Dim Largeur As Decimal
        Dim NombreGoujonsTrans(2) As Integer
        Dim LongueurZones(2) As Decimal
        Dim NombreGoujonsLongiZone(2) As Integer
        Dim NombreZones As Integer
        Dim DiametreGoujons As Decimal
        Dim EspaceLongiGoujons As Decimal
        Dim MyBrushA As New SolidBrush(Color.LightBlue)
        Dim MyPen As New Pen(Color.Black, 1)

        'Dim CouleurConnecteur As Color = Color.White
        Dim CouleurConnecteur As Color = CouleurConnecteurNormal

        Dim myBrushC As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurConnecteur)
        Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = myFont
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        Dim lPlatInf, lIFB_A, lIFB_B, lSAB, lSFB, lSem As Boolean
        Dim kEchB As Decimal

        '--> Initialisations

        lSFB = myBeam.Section.lSlimFloor_SFB
        lSAB = myBeam.Section.lSlimFloor_SAB
        lIFB_A = myBeam.Section.lSlimFloor_IFB_A
        lIFB_B = myBeam.Section.lSlimFloor_IFB_B
        lPlatInf = lSFB Or lIFB_A
        lSem = Not lIFB_B

        LongueurTravee = myBeam.LongueurTravee(indTravee)

        Bfi = myBeam.Section.ProfilA.Bfi
        tW = myBeam.Section.ProfilA.Tw
        BPlat = myBeam.Section.ProfilA.Plat_b

        Largeur = Math.Max(Bfi, BPlat)

        NombreZones = myBeam.NombreZones(indTravee)
        For i As Integer = 0 To 2
            NombreGoujonsTrans(i) = myBeam.NrTransZone(indTravee, i)
            LongueurZones(i) = myBeam.LongueurZone(indTravee, i) / myBeam.LongueurTravee(indTravee) * LongueurTravee
            NombreGoujonsLongiZone(i) = 0.75 * myBeam.LongueurZone(indTravee, i) / myBeam.EspacementZone(indTravee, i)
        Next i
        dCar = Math.Sqrt(LongueurTravee ^ 2 + Largeur ^ 2) / 20

        '--( Paramètres d'affichage

        kEchB = 1 / 10 * LongueurTravee / Largeur

        xMin = 0
        xMax = LongueurTravee
        yMin = -kEchB * Largeur / 2 - dCar / 2
        yMax = kEchB * Largeur / 2 + dCar / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--( Dessin du plat inférieur, le cas échéant

        xo = 0
        xe = LongueurTravee
        If lPlatInf Then
            yo = kEchB * BPlat / 2
            ye = -kEchB * BPlat / 2
            AddRectanglePlein(myGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        End If

        '--( Dessin de la semelle inférieure, le cas échéant

        If lPlatInf Then
            xo = 0
            xe = LongueurTravee
            yo = kEchB * Bfi / 2
            ye = -kEchB * Bfi / 2
            AddRectanglePlein(myGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        End If


        '--( Représentation des goujons soudés sur l'âme

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

                If Not (xo >= LongueurTravee - DiametreGoujons Or xo <= DiametreGoujons) Then

                    If lArma Then
                        AddLigne(myGr, New Pen(CouleurConnecteur), xo, kEchB * (tW / 2), xo, kEchB * (Largeur / 2) * 1.15, MyParAff)
                        AddLigne(myGr, New Pen(CouleurConnecteur), xo, -kEchB * (tW / 2), xo, -kEchB * (Largeur / 2) * 1.15, MyParAff)
                    Else
                        Dessin_GoujonVertical(myGr, myBeam.Dalle.Goujons, xo, kEchB * (tW / 2), myBrushC, MyParAff, True, kEchB)
                        Dessin_GoujonVertical(myGr, myBeam.Dalle.Goujons, xo, -kEchB * (tW / 2), myBrushC, MyParAff, False, kEchB)
                    End If

                End If

            Next
        Next

        '--( Dessin de l'âme

        xo = 0
        xe = LongueurTravee
        yo = kEchB * tW / 2
        ye = -kEchB * tW / 2
        AddRectanglePlein(myGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

        '--( Cotes

        If lCote Then

            xo = 0
            xe = 0

            For i As Integer = 0 To NombreZones - 1

                xo = xe
                xe += LongueurZones(i)

                'On dessinne la côte inférieure qui donne la longueur de la zone étudiée 
                Dim yCote As Decimal = -kEchB * Largeur / 2 - dCar

                AddFleche(myGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "L" Else Chaine = GetStringInUnit(myBeam.LongueurZone(indTravee, i), Enu_TypeVariable.Longueur, 4, 2, False)
                AddTexteFond(myGr, New SolidBrush(Color.Black), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                'On dessinne la côte supérieure qui donne le nombre de goujons disposés sur la zone étudiée 
                yCote = kEchB * Largeur / 2 + dCar

                AddFleche(myGr, New Pen(Color.Red), xo, yCote, xe, yCote, MyParAff, True, True)
                'Chaine = GetStringNoUnit(Math.Floor(myBeam.ZoneLongueur(indTravee, i) / myBeam.ZoneEspacement(indTravee, i)), Enu_TypeVariable.SansType) & " " & strStuds
                Chaine = "2 x " & GetStringNoUnit(myBeam.NombreGoujonTotParZone(indTravee, i), Enu_TypeVariable.SansType) & " " & strStuds
                AddTexteFond(myGr, New SolidBrush(Color.Red), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
            Next

        End If

    End Sub

#End Region

#Region "Dessins pour la connection (FRM_CONNECTIONSLIMFLOOR)"

    Public Sub DessineDalleConnectionSlimfloor(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                             lIntermediaire As Boolean, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
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

        Dim CouleurBeton As Color = CouleurBetonNormal
        Dim CouleurCofradal As Color = Color.Linen
        Dim CouleurAcier As Color = CouleurAcierNormal
        Dim CouleurConnect As Color = CouleurConnecteurNormal
        Dim ColorArmatures As Color = Color.Red
        Const kADJUST As Decimal = 0.95
        Dim zREF As Decimal = 0
        Dim Ha, Bfs As Decimal
        'Dim lCote As Boolean = True
        Dim lCofraplus220 As Boolean
        Dim LargeurProfil As Decimal

        '--> Initialisation

        lMixte = MyPoutre.Section.lMixte
        lEnrob = MyPoutre.Section.lEnrobage
        lLamine = MyPoutre.Section.lLamine
        lCofraplus220 = MyPoutre.Dalle.Bac.lCofraplus220

        Beff = 2 * LargeurDalleDessin(MyPoutre.Section.ProfilA)

        Ha = MyPoutre.Section.ProfilA.ha

        Select Case MyPoutre.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                Bfs = MyPoutre.Section.ProfilA.Bfs
                LargeurProfil = Bfs
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                Bfs = MyPoutre.Section.ProfilA.Bfs
                LargeurProfil = MyPoutre.Section.ProfilA.Plat_b - Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                Bfs = MyPoutre.Section.ProfilA.Bfs
                LargeurProfil = MyPoutre.Section.ProfilA.Plat_b - Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                Bfs = MyPoutre.Section.ProfilA.Bfi
                LargeurProfil = Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                Bfs = MyPoutre.Section.ProfilA.Bfi
                LargeurProfil = Bfs / 2
        End Select

        Select Case MyPoutre.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                zREF = MyPoutre.Section.ProfilA.hb
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                zREF = MyPoutre.Section.ProfilA.ha - MyPoutre.Section.ProfilA.Plat_t
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                zREF = MyPoutre.Section.ProfilA.ha - MyPoutre.Section.ProfilA.Tfi
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                zREF = MyPoutre.Section.ProfilA.ha - MyPoutre.Section.ProfilA.Tfi
            Case Else
                zREF = 0
        End Select

        '--> Preparation de la zone d'affichage - Calcul de ParAff
        dCar = Math.Sqrt(Beff ^ 2 + (Ha + MyPoutre.Dalle.zTop) ^ 2) / 10

        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            xMin = -Beff / 4
            xMax = -xMin
        Else
            xMin = -Bfs / 4
            xMax = Beff / 4
        End If


        If lIntermediaire Then
            yMin = -MyPoutre.Section.ProfilA.Plat_t
            yMax = MyPoutre.Dalle.zTop
        Else
            yMin = -MyPoutre.Section.ProfilA.Plat_t
            yMax = MyPoutre.Dalle.zTop
        End If

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Préparation des Pinceaux utilisés dans le dessin

        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurAcier, CouleurAcier)
        ' Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        ' Béton prefabriqué
        Dim myBrushPref As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurBeton, CouleurBeton)
        ' Cofradal
        Dim myBrushCofra As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurCofradal, CouleurCofradal)
        'Connecteurs
        Dim myBrushConnecteur As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), CouleurConnect, CouleurConnect)
        'Armatures
        Dim myBrushArmatures As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), ColorArmatures, ColorArmatures)

        '--> Dessin de la dalle

        Select Case MyPoutre.Dalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                DessinDallePleine(myGr, MyPoutre, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, Beff)
            Case cls_Dalle.Enum_TypeDalle.Mixte
                Select Case MyPoutre.Dalle.Bac.Orientation
                    Case cls_Bac.Enum_Orientation.Parallele
                        DessineDalleMixteParallele(myGr, MyPoutre.Dalle, Ha, Bfs, MyParAff, myBrushB, Beff)
                    Case cls_Bac.Enum_Orientation.Perpendiculaire
                        If lCofraplus220 Then
                            DessineDalleMixtePerpendiculaireCfp220(myGr, MyPoutre, MyParAff, myBrushB, Beff)
                        Else
                            DessineDalleMixtePerpendiculaire(myGr, MyPoutre, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, Beff)
                        End If
                End Select

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                DessinDallePreFab(myGr, MyPoutre, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, myBrushPref, Beff)

            Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                DessinDalleCompletementPrefa(myGr, MyPoutre, lIntermediaire, Ha, Bfs, MyParAff, myBrushB, myBrushCofra, Beff)

        End Select

        '--> Dessin de la mySection acier

        DessinProfileMetal(myGr, MyPoutre.Section.ProfilA, myBrushP, MyParAff, zREF, 0, Not lIntermediaire)

        '--> Dessin du goujon

        'Initialisation
        Dim Espacement_Trans_MIN As Decimal
        Dim NbGoujonsTrans As Integer = MyPoutre.NrTransZone(1, 0)

        If MyPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            Espacement_Trans_MIN = 4 * MyPoutre.Dalle.Goujons.d
        Else 'dalle pleine ou préfa
            Espacement_Trans_MIN = 2.5 * MyPoutre.Dalle.Goujons.d
        End If

        Dim xGoujon As Decimal = 0
        Dim yGoujon As Decimal = 0

        Dim dTete, hTete As Decimal
        MyPoutre.Dalle.Goujons.DimensionsTete(dTete, hTete)

        'Dessin

        Select Case MyPoutre.Dalle.typeConnecteur
            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                xGoujon = -(NbGoujonsTrans - 1) / 2 * Espacement_Trans_MIN
                yGoujon = zREF

                For i As Integer = 1 To NbGoujonsTrans
                    'Dessin du corps du goujon
                    AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - MyPoutre.Dalle.Goujons.d / 2, yGoujon, xGoujon + MyPoutre.Dalle.Goujons.d / 2, yGoujon + MyPoutre.Dalle.Goujons.hsc, MyParAff, True, True)

                    'Dessin de la tete du goujon
                    AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, xGoujon - dTete / 2, yGoujon + MyPoutre.Dalle.Goujons.hsc - hTete, xGoujon + dTete / 2, yGoujon + MyPoutre.Dalle.Goujons.hsc, MyParAff, True, True)

                    xGoujon += Espacement_Trans_MIN
                Next

            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
                xGoujon = MyPoutre.Section.ProfilA.Tw / 2

                Select Case MyPoutre.Section.ProfilA.typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                        yGoujon = zREF - MyPoutre.Section.ProfilA.Tfs - MyPoutre.Section.ProfilA.Rcs - MyPoutre.Section.ProfilA.HauteurAmeDw / 4
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                        yGoujon = zREF - MyPoutre.Section.ProfilA.Plat_t - MyPoutre.Section.ProfilA.HauteurAmeDw / 4
                End Select

                For iSigne As Integer = -1 To 1 Step 2
                    'Dessin du corps du goujon
                    AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, iSigne * xGoujon, yGoujon - MyPoutre.Dalle.Goujons.d / 2, iSigne * (xGoujon + MyPoutre.Dalle.Goujons.hsc), yGoujon + MyPoutre.Dalle.Goujons.d / 2, MyParAff, True, True)

                    'Dessin de la tete du goujon
                    AddRectanglePlein(myGr, myBrushConnecteur, MyPenContour, iSigne * (xGoujon + MyPoutre.Dalle.Goujons.hsc), yGoujon - dTete / 2, iSigne * (xGoujon + MyPoutre.Dalle.Goujons.hsc + hTete), yGoujon + dTete / 2, MyParAff, True, True)

                Next

            Case cls_Dalle.Enum_TypeConnecteur.ArmatureAme
                xGoujon = MyPoutre.Section.ProfilA.Tw / 2

                Select Case MyPoutre.Section.ProfilA.typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                        yGoujon = zREF - MyPoutre.Section.ProfilA.Tfs - MyPoutre.Section.ProfilA.Rcs - MyPoutre.Section.ProfilA.HauteurAmeDw / 4
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                        yGoujon = zREF - MyPoutre.Section.ProfilA.Plat_t - MyPoutre.Section.ProfilA.HauteurAmeDw / 4
                End Select

                For iSigne As Integer = -1 To 1 Step 2
                    'Dessin du corps du goujon
                    AddRectanglePlein(myGr, myBrushArmatures, MyPenContour, iSigne * xGoujon, yGoujon - MyPoutre.Dalle.ConnecteurArmature.ds / 2, iSigne * (xGoujon + 1.5 * LargeurProfil), yGoujon + MyPoutre.Dalle.ConnecteurArmature.ds / 2, MyParAff, True, True)
                Next

        End Select


    End Sub

#End Region

#Region " Dessin pour les maintiens (FRM_MAINTIENS) "

    Public Sub DessinFrmMaintiens(MyGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                                  ByVal pWi As Decimal, ByVal pHi As Decimal,
                                  kAdjust As Double, iSelect As Integer, lCote As Boolean, ByRef positionCotesInferieuresDessin(,) As Decimal, ByRef positionMaintiensDessin(,) As Decimal, ByRef EpaisseurSemelleDessin As Decimal,
                                  ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/06/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu des travées dans la fenêtre maintiens latéraux
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
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
        'Const lAffSymbol As Boolean = False
        Dim Chaine As String
        Dim MyFontNormal As Font = myFont
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        '--> Initialisations

        LongueurPoutre = myBeam.LongueurTotale
        LongueurTravee = cls_Poutre.PORTEEDEFAUT / 1.5
        If myBeam.lTraveeConsoleGauche Then LongueurConsoleGauche = LongueurTravee / 2
        If myBeam.lTraveeConsoleDroite Then LongueurConsoleDroite = LongueurTravee / 2
        HauteurPoutre = myBeam.HauteurTotale
        EpaisseurSemelle = HauteurPoutre / 10
        RayonConge = EpaisseurSemelle / 2
        'LongueurDalle = myBeam.LongueurTotale
        'HauteurDalle = myBeam.Dalle.Ep_td
        dCar = Math.Sqrt(LongueurTravee ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = HauteurPoutre / 4

        '--> Initialisation des paramètres d'affichage

        Select Case iSelect
            Case 0
                xMin = 0
                xMax = LongueurConsoleGauche

            Case 99
                xMin = LongueurConsoleGauche
                For i As Integer = 1 To myBeam.IndiceDerniereTravee - 1
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

        'If myBeam.NbTravees > 1 Then yMin -= dCar
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

        For i As Integer = 1 To myBeam.IndiceTraveeConsoleDroite - 1

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

        For i As Integer = 1 To myBeam.IndiceTraveeConsoleDroite

            xo = LongueurConsoleGauche + (i - 1) * LongueurTravee

            DessineAppui(MyGr, xo, dCarApp, MyParAff)

            If i = 1 Then
                If myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                    xo += EpaisseurSemelle / 2
                End If

                If myBeam.lTraveeConsoleGauche Then
                    If myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                        xo -= EpaisseurSemelle / 2
                    End If
                End If

            ElseIf i = myBeam.IndiceTraveeConsoleDroite Then
                If myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained Then
                    xo -= EpaisseurSemelle / 2
                End If

                If myBeam.lTraveeConsoleDroite Then
                    If myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained Then
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

        For i As Integer = myBeam.IndicePremiereTravee To myBeam.IndiceDerniereTravee

            Select Case myBeam.TypeMaintien
                Case cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
                    lCote = False

                Case cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained

                    lCote = False

                    MyBrushMaintienSup = MyBrushSemelleBloquee
                    MyBrushMaintienInf = MyBrushSemelleBloquee

                    xo = 0
                    Select Case i
                        Case 0
                            xe = LongueurConsoleGauche

                        Case myBeam.IndiceTraveeConsoleDroite
                            xe = LongueurConsoleGauche
                            For j As Integer = 1 To myBeam.IndiceTraveeConsoleDroite - 1
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

                Case cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained

                    For Each maintiens As cls_Maintiens In myBeam.Maintiens(i)

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
                                xo = maintiens.x_Loc / myBeam.LongueurTravee(myBeam.IndicePremiereTravee) * LongueurConsoleGauche

                            Case myBeam.IndiceTraveeConsoleDroite
                                xo = LongueurConsoleGauche
                                For j As Integer = 1 To myBeam.IndiceTraveeConsoleDroite - 1
                                    xo += LongueurTravee
                                Next
                                xo += maintiens.x_Loc / myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite) * LongueurConsoleDroite

                            Case Else
                                xo = LongueurConsoleGauche
                                For j As Integer = 1 To i - 1
                                    xo += LongueurTravee
                                Next
                                xo += maintiens.x_Loc / myBeam.LongueurTravee(i) * LongueurTravee

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
                ReDim positionCotesInferieuresDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).Count, 1)
                ReDim positionMaintiensDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).Count - 1, 2)

            Else
                ReDim positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).Count, 1)
                ReDim positionMaintiensDessin(myBeam.Maintiens(iSelect).Count - 1, 2)
            End If


            Dim yCote As Decimal = -dCar - dCarApp

            MyPen.Color = Color.Black
            MyColor = Color.Black

            Select Case iSelect
                Case 0

                    For Each maintiens In myBeam.Maintiens(iSelect)

                        Dim indice_Maintien_en_cours As Integer = myBeam.Maintiens(iSelect).IndexOf(maintiens)

                        If indice_Maintien_en_cours = 0 Then
                            xo = 0
                            xe = myBeam.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurConsoleGauche

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleGauche * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        Else
                            xo = myBeam.Maintiens(iSelect)(indice_Maintien_en_cours - 1).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurConsoleGauche
                            xe = myBeam.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurConsoleGauche

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleGauche * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        End If

                        positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                        positionMaintiensDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, xe)
                        positionMaintiensDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, 0)
                        positionMaintiensDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 2) = Mod_OutilsGraph.YEcran(MyParAff, HauteurPoutre)

                    Next

                    If myBeam.Maintiens(iSelect).Count <> 0 Then
                        xo = myBeam.Maintiens(iSelect)(myBeam.Maintiens(iSelect).Count - 1).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurConsoleGauche
                        xe = LongueurConsoleGauche
                    End If

                    AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                    Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleGauche * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                    positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).Count, 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                    positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).Count, 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                Case 99

                    For Each maintiens In myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite)

                        Dim indice_Maintien_en_cours As Integer = myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).IndexOf(maintiens)

                        xo = LongueurConsoleGauche
                        For i As Integer = 1 To myBeam.IndiceDerniereTravee - 1
                            xo += LongueurTravee
                        Next

                        If indice_Maintien_en_cours = 0 Then
                            xe = xo + myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite)(indice_Maintien_en_cours).x_Loc / myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite) * LongueurConsoleDroite

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleDroite * myBeam.LongueurTravee(myBeam.IndiceDerniereTravee), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        Else
                            xe = xo + myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite)(indice_Maintien_en_cours).x_Loc / myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite) * LongueurConsoleDroite
                            xo += myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite)(indice_Maintien_en_cours - 1).x_Loc / myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite) * LongueurConsoleDroite

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleDroite * myBeam.LongueurTravee(myBeam.IndiceDerniereTravee), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        End If

                        positionCotesInferieuresDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                        positionMaintiensDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, xe)
                        positionMaintiensDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, 0)
                        positionMaintiensDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).IndexOf(maintiens), 2) = Mod_OutilsGraph.YEcran(MyParAff, HauteurPoutre)

                    Next

                    If myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).Count <> 0 Then
                        xo = LongueurConsoleGauche
                        For i As Integer = 1 To myBeam.IndiceDerniereTravee - 1
                            xo += LongueurTravee
                        Next
                        xe = xo
                        xo += myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite)(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).Count - 1).x_Loc / myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite) * LongueurConsoleDroite
                        xe += LongueurConsoleDroite

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                        Chaine = GetStringNoUnit((xe - xo) / LongueurConsoleDroite * myBeam.LongueurTravee(myBeam.IndiceDerniereTravee), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        positionCotesInferieuresDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).Count, 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(myBeam.Maintiens(myBeam.IndiceTraveeConsoleDroite).Count, 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)



                    End If

                Case Else
                    ' Travées principales

                    For Each maintiens In myBeam.Maintiens(iSelect)

                        Dim indice_Maintien_en_cours As Integer = myBeam.Maintiens(iSelect).IndexOf(maintiens)

                        If indice_Maintien_en_cours = 0 Then
                            xo = LongueurConsoleGauche
                            xe = LongueurConsoleGauche + myBeam.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurTravee

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                        Else
                            xo = LongueurConsoleGauche + myBeam.Maintiens(iSelect)(indice_Maintien_en_cours - 1).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurTravee
                            xe = LongueurConsoleGauche + myBeam.Maintiens(iSelect)(indice_Maintien_en_cours).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurTravee

                            AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                            Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                            AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)
                        End If

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                        Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

                        positionMaintiensDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 0) = Mod_OutilsGraph.XEcran(MyParAff, xe)
                        positionMaintiensDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 1) = Mod_OutilsGraph.YEcran(MyParAff, 0)
                        positionMaintiensDessin(myBeam.Maintiens(iSelect).IndexOf(maintiens), 2) = Mod_OutilsGraph.YEcran(MyParAff, HauteurPoutre)

                    Next

                    If myBeam.Maintiens(iSelect).Count <> 0 Then
                        xo = LongueurConsoleGauche + myBeam.Maintiens(iSelect)(myBeam.Maintiens(iSelect).Count - 1).x_Loc / myBeam.LongueurTravee(iSelect) * LongueurTravee
                        xe = LongueurConsoleGauche + LongueurTravee

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)
                        Chaine = GetStringNoUnit((xe - xo) / LongueurTravee * myBeam.LongueurTravee(iSelect), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                        positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).Count, 0) = Mod_OutilsGraph.XEcran(MyParAff, 0.5 * (xo + xe))
                        positionCotesInferieuresDessin(myBeam.Maintiens(iSelect).Count, 1) = Mod_OutilsGraph.YEcran(MyParAff, yCote)

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
        '   myBeam    [E] :   Poutre à dessiner
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
        LongueurTravee = cls_Poutre.PORTEEDEFAUT / 1.5
        If MyPoutre.lTraveeConsoleGauche Then LongueurConsoleGauche = LongueurTravee / 2
        If MyPoutre.lTraveeConsoleDroite Then LongueurConsoleDroite = LongueurTravee / 2
        HauteurPoutre = MyPoutre.HauteurTotale
        EpaisseurSemelle = HauteurPoutre / 10
        RayonConge = EpaisseurSemelle / 2
        'LongueurDalle = myBeam.LongueurTotale
        'HauteurDalle = myBeam.Dalle.Ep_td
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

        'If myBeam.NbTravees > 1 Then yMin -= dCar
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
        '   AffichageOptFeu des travées dans la fenêtre maintiens latéraux
        '------------------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre à dessiner
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
            LongueurTravee = cls_Poutre.PORTEEDEFAUT / 1.5
            If MyPoutre.lTraveeConsoleGauche Then LongueurConsoleGauche = LongueurTravee / 2
            If MyPoutre.lTraveeConsoleDroite Then LongueurConsoleDroite = LongueurTravee / 2
            HauteurPoutre = MyPoutre.HauteurTotale
            EpaisseurSemelle = HauteurPoutre / 10
            RayonConge = EpaisseurSemelle / 2
            'LongueurDalle = myBeam.LongueurTotale
            'HauteurDalle = myBeam.Dalle.Ep_td
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

            'If myBeam.NbTravees > 1 Then yMin -= dCar
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

#Region " Dessins pour la définition de l'étaiement (FRM_ETAIEMENT) "

    Public Sub DessinFrmEtaiement(MyGr As Graphics, myBeam As cls_Poutre, myFont As Font,
                                  ByVal pWi As Decimal, ByVal pHi As Decimal,
                                  kAdjust As Double, lCote As Boolean,
                                  ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   08/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du plancher en longitudinal
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam      [E] :   Poutre à dessiner
        '   myFont      [E] :   Police de caractères
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
        Dim yPosEtais As Decimal
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
        Dim MyFontNormal As Font = myFont
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE
        Dim lPointProp As Boolean = (myBeam.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.PointPropped)

        '--> Initialisations

        LongueurPoutre = myBeam.LongueurTotale
        HauteurPoutre = myBeam.HauteurTotale
        LongueurDalle = myBeam.LongueurTotale
        HauteurDalle = myBeam.Dalle.Ep_td
        dCar = Math.Sqrt(LongueurPoutre ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = HauteurPoutre / 2

        '--> Initialisation des paramètres d'affichage

        xMin = 0
        xMax = LongueurPoutre
        yMin = -dCar - dCarApp
        yMax = HauteurPoutre + dCar

        If myBeam.lEtaisSousProfileAcier Then
            yPosEtais = 0
        Else
            yPosEtais = HauteurPoutre
        End If

        If myBeam.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre 

        xe = 0
        yo = 0
        ye = HauteurPoutre

        For i As Integer = myBeam.IndicePremiereTravee To myBeam.IndiceDerniereTravee

            xo = xe
            xe = xo + myBeam.LongueurTravee(i)

            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Next

        '--> Représentation des appuis

        For i As Integer = 1 To myBeam.NombreTraveesDeuxAppuis

            xo = myBeam.xPositionAppui(True, i)
            DessineAppui(MyGr, xo, dCarApp, MyParAff)

        Next

        xo = myBeam.xPositionAppui(False, myBeam.NombreTraveesDeuxAppuis)
        DessineAppui(MyGr, xo, dCarApp, MyParAff)

        '--> Représentation des étais d'extrémité

        If lPointProp And myBeam.lEtaisConsoleGauche Then

            If myBeam.lTraveeConsoleGauche Then
                xo = myBeam.xPositionAppui(True, 0)
                DessineEtais(MyGr, xo, yPosEtais, dCarApp, MyParAff)
            End If

        End If

        If lPointProp And myBeam.lEtaisConsoleDroite Then

            If myBeam.lTraveeConsoleDroite Then
                xo = myBeam.xPositionAppui(False, myBeam.IndiceDerniereTravee)
                DessineEtais(MyGr, xo, yPosEtais, 0.75 * dCarApp, MyParAff)
            End If

        End If

        '--> Représentation des étais intermédiaires

        If lPointProp And myBeam.NbEtaiement <> 0 Then

            For i As Integer = 1 To myBeam.IndiceTraveeConsoleDroite - 1
                For j As Integer = 1 To myBeam.NbEtaiement
                    xo = myBeam.xPositionAppui(True, i) + j * myBeam.LongueurTravee(i) / (myBeam.NbEtaiement + 1)
                    DessineEtais(MyGr, xo, yPosEtais, 0.75 * dCarApp, MyParAff)
                Next
            Next
        End If

        '--> Représentation des appuis continus (POM)

        Dim Longueur, LongueurMax, DeltaX As Decimal
        Dim NbPts As Integer

        If (myBeam.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.FullyPropped) Then
            LongueurMax = myBeam.LongueurTraveeMax
            DeltaX = LongueurMax / 25

            For i As Integer = myBeam.IndicePremiereTravee To myBeam.IndiceDerniereTravee

                xo = myBeam.xPositionAppui(True, i)
                Longueur = myBeam.LongueurTravee(i)

                NbPts = Math.Floor(Longueur / DeltaX) - 1

                For j As Integer = 1 To NbPts
                    xe = xo + (j) * DeltaX

                    DessineEtais(MyGr, xe, 0, dCarApp / 2, MyParAff)
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

            If myBeam.lTraveeConsoleGauche Then

                MyColor = StyleCouleur(iSelect, 0)
                MyPen.Color = MyColor

                xo = 0
                xe = myBeam.LongueurTravee(0)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Lg" Else Chaine = GetStringNoUnit(myBeam.LongueurTravee(0), Enu_TypeVariable.Longueur)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True
            End If

            ' Travées principales

            For i As Integer = 1 To myBeam.NombreTraveesDeuxAppuis

                xe = myBeam.xPositionAppui(True, i)
                MyColor = StyleCouleur(iSelect, i)
                MyPen.Color = MyColor

                If lPointProp Then
                    For j As Integer = 1 To myBeam.NbEtaiement + 1

                        xo = xe
                        xe += myBeam.LongueurTravee(i) / (myBeam.NbEtaiement + 1)

                        AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                        If lAffSymbol Then Chaine = "Lpp" Else Chaine = GetStringNoUnit(myBeam.LongueurTravee(i) / (myBeam.NbEtaiement + 1), Enu_TypeVariable.Longueur)
                        AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                    Next
                Else
                    xo = xe
                    xe += myBeam.LongueurTravee(i)

                    AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                    If lAffSymbol Then Chaine = "Lpp" Else Chaine = GetStringNoUnit(myBeam.LongueurTravee(i), Enu_TypeVariable.Longueur)
                    AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                End If

            Next

            ' Travée console droite

            If myBeam.lTraveeConsoleDroite Then

                MyColor = StyleCouleur(iSelect, 99)
                MyPen.Color = MyColor

                xo = xe
                xe += myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite)

                AddFleche(MyGr, MyPen, xo, yCote, xe, yCote, MyParAff, True, True)

                If lAffSymbol Then Chaine = "Ld" Else Chaine = GetStringNoUnit(myBeam.LongueurTravee(myBeam.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur)
                AddTexteFond(MyGr, New SolidBrush(MyColor), Chaine, MyFontNormal, 0.5 * (xo + xe), yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPen, lContour)

                lTotal = True

            End If

        End If


    End Sub

    Private Sub DessineEtais(MyGr As Graphics, xPos As Decimal, yPos As Decimal, dCar As Decimal, MyParAff As Struc_Affichage)
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

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim MyBrushAp As New SolidBrush(Color.DarkRed)

        '--> Initialisations

        PrepareContourAppui(xPos, dCar, xPts, yPts, nbPts)

        For i As Integer = 0 To yPts.Length - 1
            yPts(i) += yPos
        Next

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPts, yPts, nbPts, MyParAff, True, True)

    End Sub

#End Region

#Region " Dessins pour le chargement (FRM_CHARGEMENT) "

    Public Sub DessinFrmChargement(MyGr As Graphics, MyPoutre As cls_Poutre,
                                   ByVal pWi As Decimal, ByVal pHi As Decimal,
                                   kAdjust As Double, iSelect As Integer, ByVal traveeEnCours As Integer, ByVal chargeEnCours As String,
                                   ByVal Optional iFPonctSelect As Integer = -1, ByVal Optional iFReparSelect As Integer = -1,
                                   ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/06/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu des travées dans la fenêtre maintiens latéraux
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam    [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xSouris     [E] :   Abscisse de la souris dans l'image
        '   ySouris     [E] :   Ordonnée de la souris dans l'image
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   
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
        Dim LongueurPoutre, LongueurTravee, LongueurConsoleGauche, LongueurConsoleDroite, HauteurPoutre As Decimal
        'Dim LongueurDalle, HauteurDalle As Decimal
        Dim MyBrushA As New SolidBrush(Color.LightGray)
        Dim MyBrushSelectTravee As SolidBrush
        If MyPoutre.lTraveeConsoleGauche Or MyPoutre.lTraveeConsoleDroite Then
            MyBrushSelectTravee = New SolidBrush(Color.LightSalmon)
        Else
            MyBrushSelectTravee = MyBrushA
        End If
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyPenDot As New Pen(Color.Black, 1) With {
            .DashStyle = DashStyle.Dash
        }

        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        Dim lSelect As Boolean = False 'Permet d'indiquer + loin si la charge qui est dessinée est sélectionnée dans la fenetre Frm_Chargement 

        '--> Initialisations

        LongueurPoutre = MyPoutre.LongueurTotale
        LongueurTravee = cls_Poutre.PORTEEDEFAUT / 1.5
        If MyPoutre.lTraveeConsoleGauche Then
            LongueurConsoleGauche = LongueurTravee * MyPoutre.LongueurTravee(0) / MyPoutre.LongueurTravee(1)
        Else
            LongueurConsoleGauche = 0
        End If
        If MyPoutre.lTraveeConsoleDroite Then
            LongueurConsoleDroite = LongueurTravee * MyPoutre.LongueurTravee(MyPoutre.IndiceTraveeConsoleDroite) / MyPoutre.LongueurTravee(1)
        Else
            LongueurConsoleDroite = 0
        End If
        HauteurPoutre = MyPoutre.Section.ProfilA.ha + MyPoutre.Dalle.zTop 'LongueurTravee / 70
        dCar = Math.Sqrt(LongueurTravee ^ 2 + HauteurPoutre ^ 2) / 20
        dCarApp = Math.Min(HauteurPoutre, LongueurTravee / 30)

        '--> Initialisation des paramètres d'affichage

        'Select Case iSelect
        '    Case 0
        '        xMin = 0
        '        xMax = LongueurConsoleGauche

        '    Case 99
        '        xMin = LongueurConsoleGauche
        '        For i As Integer = 1 To myBeam.IndiceDerniereTravee - 1
        '            xMin += LongueurTravee
        '        Next
        '        xMax = xMin + LongueurConsoleDroite

        '    Case Else
        '        xMin = LongueurConsoleGauche
        '        For i As Integer = 1 To iSelect - 1
        '            xMin += LongueurTravee
        '        Next
        '        xMax = xMin + LongueurTravee

        'End Select

        xMin = 0

        xMin = LongueurConsoleGauche
        For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1
            xMin += LongueurTravee
        Next
        xMax = xMin + LongueurConsoleDroite

        xMin = 0


        yMin = dCarApp + 0.6 * dCar
        yMax = HauteurPoutre + dCar

        'If myBeam.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre 

        xe = 0
        yo = 0
        ye = HauteurPoutre

        xo = 0
        xe = LongueurConsoleGauche

        If traveeEnCours = 0 Then
            AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Else
            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        End If


        'If traveeEnCours = 0 Then
        '    AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo - dCar, yo - dCar, xe + dCar, ye + dCar, MyParAff, True, True)
        'End If

        For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite - 1

            yo = 0
            ye = HauteurPoutre

            xo = xe
            xe = xo + LongueurTravee

            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)

            If traveeEnCours = i Then
                AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
            Else
                AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
            End If

            'If traveeEnCours = i Then
            '    AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo - dCar, yo - dCar, xe + dCar, ye + dCar, MyParAff, True, True)
            'End If

        Next

        yo = 0
        ye = HauteurPoutre

        xo = xe
        xe = xo + LongueurConsoleDroite

        If traveeEnCours = MyPoutre.IndiceTraveeConsoleDroite Then
            AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        Else
            AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, ye, MyParAff, True, True)
        End If

        'If traveeEnCours = myBeam.IndiceTraveeConsoleDroite Then
        '    AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo - dCar, yo - dCar, xe + dCar, ye + dCar, MyParAff, True, True)
        'End If


        '--> Représentation interface dalle-profile
        xo = 0
        yo = MyPoutre.Section.ProfilA.ha
        xe = LongueurPoutre
        ye = yo
        AddLigne(MyGr, xo, yo, xe, ye, MyParAff)

        '--> Représentation des appuis et les maintiens associés

        For i As Integer = 1 To MyPoutre.IndiceTraveeConsoleDroite

            xo = LongueurConsoleGauche + (i - 1) * LongueurTravee

            DessineAppui(MyGr, xo, dCarApp, MyParAff)

        Next

        '--> Représentation des efforts

        Dim xPosRelative As Decimal
        Dim xPosRelativeGauche, xPosRelativeDroite As Decimal

        '--> Représentation des efforts non sélectionnées

        For i As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee
            xo = 0
            For j As Integer = MyPoutre.IndicePremiereTravee To i - 1
                Select Case j
                    Case 0
                        xo += LongueurConsoleGauche
                    Case MyPoutre.IndiceTraveeConsoleDroite
                        xo += LongueurConsoleDroite
                    Case Else
                        xo += LongueurTravee
                End Select
            Next

            '--> Représentation des forces ponctuelles
            For Each force As cls_Force In MyPoutre.ChargesU(chargeEnCours).Forces(i)
                Select Case i
                    Case 0
                        xPosRelative = xo + force.xPosT / MyPoutre.LongueurTravee(i) * LongueurConsoleGauche
                    Case MyPoutre.IndiceTraveeConsoleDroite
                        xPosRelative = xo + force.xPosT / MyPoutre.LongueurTravee(i) * LongueurConsoleDroite
                    Case Else
                        xPosRelative = xo + force.xPosT / MyPoutre.LongueurTravee(i) * LongueurTravee
                End Select
                lSelect = MyPoutre.ChargesU(chargeEnCours).Forces(i).IndexOf(force) = iFPonctSelect
                If Not lSelect Then DessinForcePonctuelle(MyGr, xPosRelative, HauteurPoutre, dCar, MyParAff, lSelect)
            Next


            '--> Représentation des forces réparties
            For Each force As cls_ForceRepartie In MyPoutre.ChargesU(chargeEnCours).FReparties(i)
                Select Case i
                    Case 0
                        xPosRelativeGauche = xo + force.xPosT(0) / MyPoutre.LongueurTravee(i) * LongueurConsoleGauche
                        xPosRelativeDroite = xo + force.xPosT(1) / MyPoutre.LongueurTravee(i) * LongueurConsoleGauche
                    Case MyPoutre.IndiceTraveeConsoleDroite
                        xPosRelativeGauche = xo + force.xPosT(0) / MyPoutre.LongueurTravee(i) * LongueurConsoleDroite
                        xPosRelativeDroite = xo + force.xPosT(1) / MyPoutre.LongueurTravee(i) * LongueurConsoleDroite
                    Case Else
                        xPosRelativeGauche = xo + force.xPosT(0) / MyPoutre.LongueurTravee(i) * LongueurTravee
                        xPosRelativeDroite = xo + force.xPosT(1) / MyPoutre.LongueurTravee(i) * LongueurTravee
                End Select
                If i = traveeEnCours Then
                    lSelect = MyPoutre.ChargesU(chargeEnCours).FReparties(i).IndexOf(force) = iFReparSelect
                Else
                    lSelect = False
                End If

                If Not lSelect Then DessinForceRepartie(MyGr, xPosRelativeGauche, HauteurPoutre, force.Force(0), xPosRelativeDroite, HauteurPoutre, force.Force(1), dCar * 0.5, dCar, MyParAff, lSelect)
            Next

        Next

        '--> Représentation des efforts sélectionnés (on le fait en dernier car sinon masqué par les charges non sélectionnées)

        xo = 0
        For j As Integer = MyPoutre.IndicePremiereTravee To traveeEnCours - 1
            Select Case j
                Case 0
                    xo += LongueurConsoleGauche
                Case MyPoutre.IndiceTraveeConsoleDroite
                    xo += LongueurConsoleDroite
                Case Else
                    xo += LongueurTravee
            End Select
        Next

        '--> Représentation des forces ponctuelles
        If iFPonctSelect <> -1 Then 'Permet de dessiner la force sélectionnée en dernier pour que cette dernière soit visible
            Select Case traveeEnCours
                Case 0
                    xPosRelative = xo + MyPoutre.ChargesU(chargeEnCours).Forces(traveeEnCours)(iFPonctSelect).xPosT / MyPoutre.LongueurTravee(traveeEnCours) * LongueurConsoleGauche
                Case MyPoutre.IndiceTraveeConsoleDroite
                    xPosRelative = xo + MyPoutre.ChargesU(chargeEnCours).Forces(traveeEnCours)(iFPonctSelect).xPosT / MyPoutre.LongueurTravee(traveeEnCours) * LongueurConsoleDroite
                Case Else
                    xPosRelative = xo + MyPoutre.ChargesU(chargeEnCours).Forces(traveeEnCours)(iFPonctSelect).xPosT / MyPoutre.LongueurTravee(traveeEnCours) * LongueurTravee
            End Select
            DessinForcePonctuelle(MyGr, xPosRelative, HauteurPoutre, dCar, MyParAff, True)
        End If

        '--> Représentation des forces réparties

        If iFReparSelect <> -1 Then 'Permet de dessiner la force sélectionnée en dernier pour que cette dernière soit visible
            Select Case traveeEnCours
                Case 0
                    xPosRelativeGauche = xo + MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).xPosT(0) / MyPoutre.LongueurTravee(traveeEnCours) * LongueurConsoleGauche
                    xPosRelativeDroite = xo + MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).xPosT(1) / MyPoutre.LongueurTravee(traveeEnCours) * LongueurConsoleGauche
                Case MyPoutre.IndiceTraveeConsoleDroite
                    xPosRelativeGauche = xo + MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).xPosT(0) / MyPoutre.LongueurTravee(traveeEnCours) * LongueurConsoleDroite
                    xPosRelativeDroite = xo + MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).xPosT(1) / MyPoutre.LongueurTravee(traveeEnCours) * LongueurConsoleDroite
                Case Else
                    xPosRelativeGauche = xo + MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).xPosT(0) / MyPoutre.LongueurTravee(traveeEnCours) * LongueurTravee
                    xPosRelativeDroite = xo + MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).xPosT(1) / MyPoutre.LongueurTravee(traveeEnCours) * LongueurTravee
            End Select
            DessinForceRepartie(MyGr, xPosRelativeGauche, HauteurPoutre, MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).Force(0), xPosRelativeDroite, HauteurPoutre, MyPoutre.ChargesU(chargeEnCours).FReparties(traveeEnCours)(iFReparSelect).Force(1), dCar * 0.5, dCar, MyParAff, True)
        End If





    End Sub


    Public Sub DessinFrmChargementN(MyGr As Graphics, MyPoutre As cls_Poutre,
                                    ByVal pWi As Decimal, ByVal pHi As Decimal,
                                    kAdjust As Double, ByVal traveeEnCours As Integer, traveeMouse As Integer, ByVal chargeEnCours As String,
                                    ByRef vParAff As Struc_Affichage,
                                    ByVal Optional iFPonctSelect As Integer = -1, ByVal Optional iFReparSelect As Integer = -1,
                                    ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   21/06/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu des travées dans la fenêtre maintiens latéraux
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myBeam    [E] :   Poutre à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   xSouris     [E] :   Abscisse de la souris dans l'image
        '   ySouris     [E] :   Ordonnée de la souris dans l'image
        '   iSelect     [E] :   Indique quel est la travée sélectionnée
        '   
        '------------------------------------------------------------------------------------------------------------------
        '   iSelect     0  : console gauche
        '               i  : travée sur 2 appui no i
        '               99 : console droite
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar, dCarApp As Decimal

        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim LongueurPoutre, HauteurPoutre As Decimal

        Dim MyBrushA As New SolidBrush(CouleurProfile)
        Dim myBrushB As Brush
        If xLeft <> 0 Or yTop <> 0 Then
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.Gray, Color.Gray)
        Else
            'myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, Color.Gray)
            myBrushB = New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurDalle, Color.Gray)
        End If

        Dim MyBrushSelectTravee As SolidBrush
        If MyPoutre.lTraveeConsoleGauche Or MyPoutre.lTraveeConsoleDroite Then
            MyBrushSelectTravee = New SolidBrush(CouleurTraveeSelect)
        Else
            MyBrushSelectTravee = MyBrushA
        End If
        Dim myBrushMouse As New SolidBrush(CouleurTraveeMouse)

        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyPenDot As New Pen(Color.Black, 1) With {
            .DashStyle = DashStyle.Dash
        }

        Dim MyFontNormal As Font = FontBase
        Dim lTotal As Boolean = False
        Dim lContour As Boolean = lCONTOURCOTE

        Dim lSelect As Boolean = False 'Permet d'indiquer + loin si la charge qui est dessinée est sélectionnée dans la fenetre Frm_Chargement 
        Dim zDalle, zSem As Decimal
        Dim iTravee As Integer
        Dim DeltaT As Decimal

        '--> Initialisations

        LongueurPoutre = MyPoutre.LongueurTotale

        HauteurPoutre = MyPoutre.Section.ProfilA.ha + MyPoutre.Dalle.zTop 'LongueurTravee / 70
        DeltaT = HauteurPoutre / 10

        dCar = Math.Sqrt(LongueurPoutre ^ 2 + HauteurPoutre ^ 2) / 12                                       ' Pour la représentation des efforts
        dCarApp = Math.Min(Math.Sqrt(LongueurPoutre ^ 2 + HauteurPoutre ^ 2) / 25, HauteurPoutre / 2)       ' Pour la représentation des appuis

        zDalle = MyPoutre.Dalle.zTop
        zSem = -MyPoutre.Section.ProfilA.ha

        '--> Initialisation des paramètres d'affichage

        xMin = 0

        xMax = LongueurPoutre

        yMin = -MyPoutre.Section.ProfilA.ha - dCarApp '- 0.6 * dCar
        yMax = MyPoutre.Dalle.zTop + dCar

        'If myBeam.NbTravees > 1 Then yMin -= dCar
        ParametresAffichage(vParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation de la poutre 

        Dim iDebT As Integer = MyPoutre.IndicePremiereTravee
        Dim iFinT As Integer = MyPoutre.IndiceDerniereTravee
        Dim xCum As Decimal = 0

        yo = zSem
        ye = zDalle

        For iTravee = iDebT To iFinT

            xo = xCum
            xe = xo + MyPoutre.LongueurTravee(iTravee)

            If (iTravee = traveeEnCours) And (MyPoutre.NbTravees > 1) Then
                AddRectanglePlein(MyGr, MyBrushSelectTravee, MyPenContour, xo, yo, xe, ye, vParAff, True, True)
            Else
                AddRectanglePlein(MyGr, MyBrushA, MyPenContour, xo, yo, xe, 0, vParAff, True, True)
                AddRectanglePlein(MyGr, myBrushB, MyPenContour, xo, 0, xe, ye, vParAff, True, True)
            End If

            If (MyPoutre.NbTravees > 1) And (iTravee = traveeMouse) Then
                AddRectanglePlein(MyGr, myBrushMouse, MyPenContour, xo + DeltaT, yo + DeltaT, xe - DeltaT, ye - DeltaT, vParAff, True, True)
            End If

            xCum = xe
        Next

        '--> Représentation interface dalle-profile

        xo = 0
        xe = LongueurPoutre

        AddLigne(MyGr, xo, 0, xe, 0, vParAff)

        '--> Représentation des appuis et les maintiens associés

        xo = MyPoutre.xPositionAppui(True, 1)
        DessineAppui(MyGr, xo, dCarApp, vParAff, zSem)

        For iTravee = 1 To MyPoutre.NombreTraveesDeuxAppuis
            xo = MyPoutre.xPositionAppui(False, 1)
            DessineAppui(MyGr, xo, dCarApp, vParAff, zSem)
        Next

        '--> Représentation des efforts

        Dim xPosRelative As Decimal

        '# Efforts non sélectionnés

        xo = 0
        For iTravee = iDebT To iFinT

            '[ Forces ponctuelles

            For Each force As cls_Force In MyPoutre.ChargesU(chargeEnCours).Forces(iTravee)
                xPosRelative = xo + force.xPosT
                lSelect = MyPoutre.ChargesU(chargeEnCours).Forces(iTravee).IndexOf(force) = iFPonctSelect
                If Not lSelect Then DessinForcePonctuelle(MyGr, xPosRelative, zDalle, dCar, vParAff, lSelect)
            Next

            '[ Forces réparties

            For Each force As cls_ForceRepartie In MyPoutre.ChargesU(chargeEnCours).FReparties(iTravee)
                If iTravee = traveeEnCours Then
                    lSelect = MyPoutre.ChargesU(chargeEnCours).FReparties(iTravee).IndexOf(force) = iFReparSelect
                Else
                    lSelect = False
                End If

                If Not lSelect Then DessinForceRepartie(MyGr, xo + force.xPosT(0), zDalle, force.Force(0), xo + force.xPosT(1), zDalle,
                                                        force.Force(1), dCar * 0.5, dCar, vParAff, lSelect)
            Next

            xo += MyPoutre.LongueurTravee(iTravee)
        Next

        '# Efforts  sélectionnés

        xo = 0
        For iTravee = iDebT To iFinT

            '[ Forces ponctuelles

            For Each force As cls_Force In MyPoutre.ChargesU(chargeEnCours).Forces(iTravee)
                xPosRelative = xo + force.xPosT
                lSelect = MyPoutre.ChargesU(chargeEnCours).Forces(iTravee).IndexOf(force) = iFPonctSelect
                If lSelect Then DessinForcePonctuelle(MyGr, xPosRelative, zDalle, dCar, vParAff, lSelect)
            Next

            '[ Forces réparties

            For Each force As cls_ForceRepartie In MyPoutre.ChargesU(chargeEnCours).FReparties(iTravee)
                If iTravee = traveeEnCours Then
                    lSelect = MyPoutre.ChargesU(chargeEnCours).FReparties(iTravee).IndexOf(force) = iFReparSelect
                Else
                    lSelect = False
                End If

                If lSelect Then DessinForceRepartie(MyGr, xo + force.xPosT(0), zDalle, force.Force(0), xo + force.xPosT(1), zDalle,
                                                    force.Force(1), dCar * 0.5, dCar, vParAff, lSelect)
            Next

            xo += MyPoutre.LongueurTravee(iTravee)
        Next


    End Sub

    Public Sub DessinForcePonctuelle(MyGr As Graphics, xPos As Decimal, yPos As Decimal, dCar As Decimal, MyParAff As Struc_Affichage, Optional lSelect As Boolean = False)
        '------------------------------------------------------------------------------------------------------------------
        '   12/09/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Dessin d'une flèche pour représenter une force ponctuelle 
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   xPos        [E] :   Position de la charge
        '   dCar        [E] :   Dimension caractéristique
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim MyBrushAp As SolidBrush

        If lSelect Then
            MyBrushAp = New SolidBrush(ColorForceSelect)
        Else
            MyBrushAp = New SolidBrush(Color.White)
        End If

        'Définition des variables locales pour les dimensions de la fleche

        Dim HauteurExt, HauteurInt, LargeurInt, LargeurExt As Decimal

        HauteurExt = 2.7 * dCar / 2.7
        HauteurInt = 0.8 * dCar / 2.7
        LargeurInt = 0.4 * dCar / 2.7
        LargeurExt = 1.1 * dCar / 2.7

        '--> Initialisations

        PrepareContourForcePonctuelle(xPos, yPos, HauteurExt, HauteurInt, LargeurInt, LargeurExt, xPts, yPts, nbPts)

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPts, yPts, nbPts, MyParAff, True, True)

    End Sub

    Public Sub DessinForceRepartie(MyGr As Graphics, xPosGauche As Decimal, yPosGauche As Decimal, ChargeLinGauche As Decimal, xPosDroite As Decimal, yPosDroite As Decimal, ChargeLinDroite As Decimal, dCar As Decimal, pasFleche As Decimal, MyParAff As Struc_Affichage, Optional lSelect As Boolean = False)
        '------------------------------------------------------------------------------------------------------------------
        '   12/09/23 :  Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Dessin d'une flèche pour représenter une force ponctuelle 
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   xPos        [E] :   Position de la charge
        '   dCar        [E] :   Dimension caractéristique
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xPtsGauche() As Single = Nothing
        Dim yPtsGauche() As Single = Nothing
        Dim nbPtsGauche As Integer
        Dim MyBrushAp As SolidBrush
        Dim MyPen As Pen

        If lSelect Then
            MyBrushAp = New SolidBrush(Color.DarkRed)
            MyPen = New Pen(Color.DarkRed)
        Else
            MyBrushAp = New SolidBrush(Color.White)
            MyPen = New Pen(Color.DarkRed)
        End If

        Dim HauteurExtMax, HauteurExtMin, HauteurExtGauche, HauteurExtDroite, HauteurInt, LargeurInt, LargeurExt As Decimal

        HauteurExtMax = 5.0 * dCar / 5
        HauteurExtMin = 1 * dCar / 5

        If ChargeLinGauche = 0 And ChargeLinDroite = 0 Then
            HauteurExtGauche = 0
            HauteurExtDroite = 0
        ElseIf ChargeLinGauche <= ChargeLinDroite Then
            HauteurExtGauche = Math.Max(ChargeLinGauche / ChargeLinDroite * HauteurExtMax, HauteurExtMin)
            HauteurExtDroite = HauteurExtMax
        Else
            HauteurExtGauche = HauteurExtMax
            HauteurExtDroite = Math.Max(ChargeLinDroite / ChargeLinGauche * HauteurExtMax, HauteurExtMin)
        End If
        HauteurInt = 0.8 * dCar / 3
        LargeurInt = 0.4 * dCar / 3
        LargeurExt = 1.1 * dCar / 3

        '--> Initialisations

        PrepareContourForcePonctuelle(xPosGauche, yPosGauche, HauteurExtGauche, HauteurInt, LargeurInt, LargeurExt, xPtsGauche, yPtsGauche, nbPtsGauche)

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPtsGauche, yPtsGauche, nbPtsGauche, MyParAff, True, True)

        '--> Initialisations

        Dim xPtsDroite() As Single = Nothing
        Dim yPtsDroite() As Single = Nothing
        Dim nbPtsDroite As Integer

        PrepareContourForcePonctuelle(xPosDroite, yPosDroite, HauteurExtDroite, HauteurInt, LargeurInt, LargeurExt, xPtsDroite, yPtsDroite, nbPtsDroite)

        '--> Dessin

        RemplirZone(MyGr, MyBrushAp, xPtsDroite, yPtsDroite, nbPtsDroite, MyParAff, True, True)

        '--> Dessin des fleches réparties

        Dim HauteurLoc As Decimal

        For x As Decimal = xPosGauche + pasFleche To xPosDroite Step pasFleche
            HauteurLoc = HauteurExtGauche + (HauteurExtDroite - HauteurExtGauche) / (xPosDroite - xPosGauche) * (x - xPosGauche)
            AddFleche(MyGr, MyPen, x, yPosGauche, x, HauteurLoc + yPosGauche, MyParAff, True, False)
        Next

        AddLigne(MyGr, MyPen, xPosGauche, HauteurExtGauche + yPosGauche, xPosDroite, HauteurExtDroite + yPosGauche, MyParAff)


    End Sub

    Private Sub PrepareContourForcePonctuelle(xPos As Decimal, yPos As Decimal, HauteurExt As Decimal, HauteurInt As Decimal, LargeurInt As Decimal, LargeurExt As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   12/09/23    :   Création - GUD
        '---------------------------------------------------------------------------------------------------------------------------
        '   Préparaton des points définissant le contour d'une flèche pour représenter une force ponctuelle
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
        yo = yPos


        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos - LargeurExt / 2
        yo = yPos + HauteurInt

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos - LargeurInt / 2
        yo = yPos + HauteurInt

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos - LargeurInt / 2
        yo = yPos + HauteurExt

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos + LargeurInt / 2
        yo = yPos + HauteurExt

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos + LargeurInt / 2
        yo = yPos + HauteurInt

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = xPos + LargeurExt / 2
        yo = yPos + HauteurInt

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

    End Sub


#End Region

#Region " Dessins pour le choix des sections (FRM_AJOUTEPP) "

    Public Sub DessinFrmTypeSection(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre, lIntermediaire As Boolean,
                                    ByVal pWi As Decimal, ByVal pHi As Decimal, ByVal MyFont As Font,
                                    kAdjust As Double, lSelect As Boolean, lDispo As Boolean, strNonDispo As String,
                                    ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du type de mySection dans la fenêtre choix de type de mySection
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   mySection     [E] :   Section à dessiner
        '   pWi, pHi    [E] :   Dimensions del'objet dans lequel on dessine
        '   MyFont      [E] :   
        '   kAdjust     [E] :   Paramètre d'ajustement de l'échelle (1 pour plein écran)
        '   lSelect     [E] :   Indique si mySection sélectionnée
        '   lDispo      [E] :   Indique si mySection disponible
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
        Dim CouleurNonDispo As Color = Color.LightGray

        '--> Initialisations

        BfMax = Math.Max(MyPoutre.Section.ProfilA.Bfs, MyPoutre.Section.ProfilA.Bfi)

        '--> Couleur

        If lDispo Then
            If lSelect Then
                CouleurAcier = CouleurAcierSelect
                CouleurBeton = CouleurBetonSelect
                CouleurArma = CouleurArmaSelect
            Else
                CouleurAcier = CouleurAcierNormal
                CouleurBeton = CouleurBetonNormal
                CouleurArma = CouleurArmaNormal
            End If
        Else
            CouleurAcier = CouleurNonDispo
            CouleurBeton = CouleurNonDispo
            CouleurArma = CouleurNonDispo
        End If

        '--> Initialisation des paramètres d'affichage

        GenereDimensionsEnveloppes(MyPoutre, lDalleRed, xMin, xMax, yMin, yMax, dCar, BeffRed, 0)

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Préparation des Pinceaux utilisés dans le dessin

        ' Profilé
        Dim myBrushP As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurAcier)
        'Béton
        Dim myBrushB As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), Color.DarkGray, CouleurBeton)
        ' Armatures
        Dim myBrushA As New LinearGradientBrush(New PointF(0, 0), New PointF(pHi, pWi), CouleurArma, CouleurArma)

        '--> Renvoi vers les routines de dessin en fonction du type

        Select Case MyPoutre.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage,
                 cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage

                DessinFrmTypeSectionStandard(MyGr, MyPoutre, lIntermediaire, MyParAff, BeffRed, myBrushP, myBrushB, myBrushA)

            Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte

                DessinFrmTypeSFB(MyGr, MyPoutre, MyParAff, myBrushP, myBrushB, myBrushA)

            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte

                DessinFrmTypeIFB_A(MyGr, MyPoutre, MyParAff, myBrushP, myBrushB, myBrushA)

            Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte

                DessinFrmTypeIFB_B(MyGr, MyPoutre, MyParAff, myBrushP, myBrushB, myBrushA)

            Case cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SABmixte
                DessinFrmTypeSAB(MyGr, MyPoutre, MyParAff, myBrushP, myBrushB, myBrushA)

        End Select

        '--> Gestion des sections non disponibles

        If Not lDispo Then
            Dim myColor As Color
            Dim Chaine As String = ""

            If lSelect Then
                myColor = ColorSelect
                Chaine = strNonDispo
            Else
                myColor = ColorNonSelect
            End If

            AddTexte(MyGr, New SolidBrush(myColor), Chaine, MyFont, (xMin + xMax) / 2, (yMin + yMax) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)

        End If

        '--> Fin

        myBrushP.Dispose()
        myBrushB.Dispose()
        myBrushA.Dispose()

    End Sub

    Private Sub DessinFrmTypeSAB(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                 MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)

        '------------------------------------------------------------------------------------------------------------------
        '   09/06/23 :  Création - FuD
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du type de mySection dans la fenêtre choix de type de mySection
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la mySection a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------
        '   Position z = 0 : Fibre inférieur du profilé, hors le plat
        '------------------------------------------------------------------------------------------------------------------

        Dim ZREF As Decimal = MyPoutre.Section.ProfilA.Tfi
        Dim lMixte As Boolean = (MyPoutre.Section.TypeSection = cls_Section.Enum_TypeSection.SABmixte)

        '--> Dessin de la dalle pour un SFB mixte

        If lMixte Then
            DessinDalleSlimFloor(MyGr, MyPoutre, MyPoutre.Section.ProfilA.ha, MyParaff1, myBrushB, ZREF)
        End If

        '--> Dessin de la mySection acier
        ZREF = MyPoutre.Section.ProfilA.ha
        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF)


    End Sub

    Private Sub DessinFrmTypeSFB(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                 MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du type de mySection dans la fenêtre choix de type de mySection
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la mySection a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------
        '   Position z = 0 : Fibre inférieur du profilé, hors le plat
        '------------------------------------------------------------------------------------------------------------------

        Dim ZREF As Decimal = MyPoutre.Section.ProfilA.ha
        Dim lMixte As Boolean = (MyPoutre.Section.TypeSection = cls_Section.Enum_TypeSection.SFBmixte)

        '--> Dessin de la dalle pour un SFB mixte

        If lMixte Then
            DessinDalleSlimFloor(MyGr, MyPoutre, MyPoutre.Section.ProfilA.ha, MyParaff1, myBrushB)
        End If

        '--> Dessin de la mySection acier

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF)

        '--> Dessin du plat

        DessinPlat(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, -MyPoutre.Section.ProfilA.Plat_t)

    End Sub

    Private Sub DessinFrmTypeIFB_A(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                 MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du type de mySection dans la fenêtre choix de type de mySection
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la mySection a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------
        '   Position z = 0 : Fibre inférieur du profilé, hors le plat
        '------------------------------------------------------------------------------------------------------------------

        Dim ZREF As Decimal = MyPoutre.Section.ProfilA.ha
        Dim lMixte As Boolean = (MyPoutre.Section.TypeSection = cls_Section.Enum_TypeSection.IFB_Amixte)

        '--> Dessin de la dalle pour un SFB mixte

        If lMixte Then
            DessinDalleSlimFloor(MyGr, MyPoutre, MyPoutre.Section.ProfilA.ha, MyParaff1, myBrushB)
        End If

        '--> Dessin de la mySection acier

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF)

        '--> Dessin du plat

        DessinPlat(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, -MyPoutre.Section.ProfilA.Plat_t)

    End Sub

    Private Sub DessinFrmTypeIFB_B(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre,
                                 MyParaff1 As Struc_Affichage, myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du type de mySection dans la fenêtre choix de type de mySection
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la mySection a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------
        '   Position z = 0 : Fibre inférieur du profilé, hors le plat
        '------------------------------------------------------------------------------------------------------------------

        Dim ZREF As Decimal = MyPoutre.Section.ProfilA.ha - MyPoutre.Section.ProfilA.Tfi
        Dim lMixte As Boolean = (MyPoutre.Section.TypeSection = cls_Section.Enum_TypeSection.IFB_Bmixte)

        '--> Dessin de la dalle pour un SFB mixte

        If lMixte Then
            DessinDalleSlimFloor(MyGr, MyPoutre, MyPoutre.Section.ProfilA.ha, MyParaff1, myBrushB)
        End If

        '--> Dessin de la mySection acier

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF)

        '--> Dessin du plat

        DessinPlat(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, MyPoutre.Section.ProfilA.ha - MyPoutre.Section.ProfilA.Plat_t)

    End Sub

    Private Sub DessinFrmTypeSectionStandard(ByRef MyGr As Graphics, ByVal MyPoutre As cls_Poutre, lIntermediaire As Boolean,
                                             MyParaff1 As Struc_Affichage, BeffRed As Decimal,
                                             myBrushP As Brush, myBrushB As Brush, myBrushA As Brush)
        '------------------------------------------------------------------------------------------------------------------
        '   31/05/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   AffichageOptFeu du type de mySection dans la fenêtre choix de type de mySection
        '------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MySection   [E] :   Section à dessiner
        '   MyParaff1   [E] :   Paramètres d'affichage
        '   lSelect     [E] :   Indique si la mySection a été selectionnée
        '   myBrushP    [E] :   Pinceau pour le profilé acier
        '   myBrushB    [E] :   Pinceau pour le béton
        '   myBrushA    [E] :   Pinceau pour les armatures
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMixte, lEnrob As Boolean
        Const ZREF As Decimal = 0

        '--> Initialisation

        lMixte = MyPoutre.Section.lMixte
        lEnrob = MyPoutre.Section.lEnrobage

        '--> Dessin de béton d'enrobage

        If lEnrob Then _
        DessinEnrobagePartielBeton(MyGr, MyPoutre.Section, MyParaff1, myBrushB)

        '--> Dessin de la mySection acier

        DessinProfileMetal(MyGr, MyPoutre.Section.ProfilA, myBrushP, MyParaff1, ZREF)

        '--> Dessin des étriers : On ne représente pas les étriers pour la définition du type

        '--> Dessin des armatures longitudinales

        If lEnrob Then
            DessinArmaLongiEnrobage(MyGr, MyPoutre.Section, MyParaff1, myBrushA, 0)
            'DessinArmaLongiEnrobage(MyGr, MySection, MyParAff, myBrushA, 1)
            DessinArmaLongiEnrobage(MyGr, MyPoutre.Section, MyParaff1, myBrushA, 2)
        End If


        '--> Dessin de la dalle (toujours une dalle pleine)

        If lMixte Then

            '# Dalle béton

            Select Case MyPoutre.Dalle.type
                Case cls_Dalle.Enum_TypeDalle.Pleine
                    DessinDallePleine(MyGr, MyPoutre, lIntermediaire, MyPoutre.Section.ProfilA.ha, MyPoutre.Section.ProfilA.Bfs, MyParaff1, myBrushB, BeffRed)
                    'Case Cls_Dalle.Enum_TypeDalle.Mixte
                    '    DessinDalleMixte(MyGr, Section.dalle, BeffRed, Section.ha, Section.b_fs, ZREF, MyParAff, myBrushB)
            End Select

            '# Armatures

            'DessinLitArmaDalle(MyGr, MySection.Dalle, BeffRed, 0, MySection.ProfilA.ha, MyParAff, myBrushA)
            'DessinLitArmaDalle(MyGr, MySection.Dalle, BeffRed, 1, MySection.ProfilA.ha, MyParAff, myBrushA)

        End If

    End Sub

#End Region

#Region "Dessin pour les cas de charges (FRM_PPCasDeCharge)"

    'Public Sub DessineRDM(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, myBeam As cls_Poutre,
    '                      iCas As Integer, lDef As Boolean, lMom As Boolean, lTranchant As Boolean, lNum As Boolean, lInertie As Boolean,
    '                      lChargement As Boolean, lEchLocal As Boolean, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
    Public Sub DessineRDM(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                           iCas As Integer, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des diagrammes de sollicitations par cas de charge
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam    [E] :   Poutre à dessiner
        '   iCas        [E] :   Cas de charge à afficher
        '   lDef        [E] :   Indique si affichage des déformées
        '   lMom        [E] :   Indique si affichage du diagramme de moment
        '   lTranchant  [E] :   Indique si affichage du diagramme de tranchant
        '   lNum        [E] :   Indique si affichage des numéros de noeuds
        '   lInertie    [E] :   Indique si affichage des inerties
        '   lChargement [E] :   Indique si affichage du chargement
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double = 0
        Const kADJUST As Decimal = 0.95
        Dim Longueur As Decimal = MyPoutre.LongueurTotale

        Dim EcartZ As Decimal = Longueur * pHi / pWi
        Dim DiaNode As Decimal = Longueur / 200
        Dim dApp As Decimal = Longueur / 50
        Dim kEch, kEchM As Decimal
        Dim lResult As Boolean '= myBeam.ChargesA(iCas).lRunCalcul
        Const SigneM As Decimal = -1
        Const SigneV As Decimal = -1
        Dim MyFontNum As New Font("Arial", 7)
        Dim MyFontLegende As New Font("Arial", 10, FontStyle.Bold)
        Dim Chaine, ChaineMin, ChaineMax As String
        Dim MyPenB As New SolidBrush(Color.Gray)


        Dim ColorDef = Color.DarkOrange
        Dim ColorDiagM = Color.DarkRed
        Dim ColorDiagV = Color.DarkBlue

        Dim ColorPoutre As Color = Color.DarkGray

        Dim MyPenPoutre As New Pen(ColorPoutre)
        Dim MyPenSelect As New Pen(ColorSelect, 2)
        Dim MyPen As Pen

        Dim CasDeCharge As cls_CasDeCharge = MyPoutre.ChargesA(iCas)
        Dim fMin, fMax, VMin, VMax, MMin, MMax As Decimal
        Dim iNodeVMax, iNodeVMin, iNodeMMax, iNodeMMin As Integer


        '--> Initialisation

        lResult = MyPoutre.ChargesA(iCas).lRunCalcul

        Dim MyBrushN As New SolidBrush(Color.White)
        Dim MyPenDef As New Pen(ColorDef)
        Dim MyPenM As New Pen(Color.Blue)

        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -EcartZ / 2
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        dCar = 0.8 * EcartZ / 2





        With MyPoutre.ChargesA(iCas)


            '--> AffichageOptFeu de la poutre

            AddLigne(myGr, MyPenPoutre, 0, 0, Longueur, 0, MyParAff)

            '--> AffichageOptFeu des noeuds

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                MyPen = MyPenPoutre
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, DiaNode, MyParAff, True, MyPen)
                If OptionsDiagrammesCDC.lDessNumeros Then
                    Chaine = "N" & CStr(iNode + 1)
                    AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(iNode), 0, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
                End If
            Next

            '--> AffichageOptFeu des appuis

            Dim indAppuis() As Integer = Nothing
            Dim NbApp As Integer
            Dim lEtais As Boolean = (iCas = MyPoutre.IndiceCasG1PP)
            MyPoutre.ExtraireIndiceNoeudsAppuis(lEtais, indAppuis, NbApp)

            For iApp As Integer = 0 To NbApp - 1
                DessineAppui(myGr, MyPoutre.Nodes.xGlobal(indAppuis(iApp)), dApp, MyParAff)
            Next

            '--> AffichageOptFeu légende couleurs V et M

            If OptionsDiagrammesCDC.lDessMoment Or OptionsDiagrammesCDC.lDessEffortT Then
                Dim longueurRectangle, hauteurRectangle As Decimal
                longueurRectangle = dCar / 6
                hauteurRectangle = longueurRectangle / 6
                Dim xo_legende, yo_legende, xe_legende, ye_legende As Decimal

                If OptionsDiagrammesCDC.lDessEffortT Then
                    If OptionsDiagrammesCDC.lDessMoment Then 'légende de V et M
                        'Texte
                        xo_legende = Longueur + dCar / 4
                        yo_legende = -dCar / 8 + hauteurRectangle / 2

                        AddTexte(myGr, New SolidBrush(ColorDiagV), "V", MyFontLegende, xo_legende, yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Effort tranchant
                        AddTexte(myGr, New SolidBrush(ColorDiagM), "M", MyFontLegende, xo_legende, -yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Moment fléchissant 

                        'Rectangle
                        xo_legende = Longueur + dCar / 3
                        xe_legende = xo_legende + longueurRectangle
                        yo_legende = -dCar / 8
                        ye_legende = yo_legende + hauteurRectangle

                        AddRectanglePlein(myGr, ColorDiagV, xo_legende, yo_legende, xe_legende, ye_legende, MyParAff, True) 'Effort tranchant
                        AddRectanglePlein(myGr, ColorDiagM, xo_legende, -yo_legende, xe_legende, -ye_legende, MyParAff, True) 'Moment fléchissant
                    Else 'légende de V uniquement
                        'Texte
                        xo_legende = Longueur + dCar / 4
                        yo_legende = 0

                        AddTexte(myGr, New SolidBrush(ColorDiagV), "V", MyFontLegende, xo_legende, yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Effort tranchant

                        'Rectangle
                        xo_legende = Longueur + dCar / 3
                        xe_legende = xo_legende + longueurRectangle
                        yo_legende = 0
                        ye_legende = yo_legende + hauteurRectangle

                        AddRectanglePlein(myGr, ColorDiagV, xo_legende, yo_legende, xe_legende, ye_legende, MyParAff, True) 'Effort tranchant
                    End If
                Else 'légende de M uniquement 
                    'Texte
                    xo_legende = Longueur + dCar / 4
                    yo_legende = 0

                    AddTexte(myGr, New SolidBrush(ColorDiagM), "M", MyFontLegende, xo_legende, yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Effort tranchant

                    'Rectangle
                    xo_legende = Longueur + dCar / 3
                    xe_legende = xo_legende + longueurRectangle
                    yo_legende = 0
                    ye_legende = yo_legende + hauteurRectangle

                    AddRectanglePlein(myGr, ColorDiagM, xo_legende, yo_legende, xe_legende, ye_legende, MyParAff, True) 'Effort tranchant
                End If
            End If

            '--> Déformée

            Dim Uz As Decimal
            Dim xo, xe, yo, ye As Decimal

            If lResult And OptionsDiagrammesCDC.lDessDeformee Then

                .EnveloppesFleche(fMax, fMin)

                kEch = CoefEchelleDessin(fMin, fMax, EcartZ / 2)

                For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
                    xo = MyPoutre.Nodes.xGlobal(iNode)
                    xe = MyPoutre.Nodes.xGlobal(iNode + 1)
                    yo = .UZ(iNode) * kEch
                    ye = .UZ(iNode + 1) * kEch
                    AddLigne(myGr, MyPenDef, xo, yo, xe, ye, MyParAff)
                Next

                For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                    Uz = .UZ(iNode)
                    AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), kEch * Uz, DiaNode, MyParAff, True, MyPenDef)
                Next

            End If

            '--> Représentation du chargement

            If OptionsDiagrammesCDC.lDessCharges Then
                DessineChargement(myGr, MyPoutre.ChargesA(iCas), MyPoutre.IndicePremiereTravee, MyPoutre.IndiceDerniereTravee, kEch, dCar,
                                      MyPoutre.Nodes.xGlobal, MyPoutre.Nodes.nbNodes, MyParAff)
            End If

            '--> Inerties

            If OptionsDiagrammesCDC.lDessInerties Then
                DessineProp(myGr, MyPoutre, iCas, dCar, MyParAff)
            End If

            '--> Diagramme de Moments de flexion

            If OptionsDiagrammesCDC.lDessMoment And lResult Then

                .EnveloppesMoments(MMax, iNodeMMax, MMin, iNodeMMin)

                kEchM = CoefEchelleDessin(MMin, MMax, EcartZ / 2) * SigneM

                ChaineMin = GetStringInUnit(MMin, Enu_TypeVariable.Moment, 4, 2, True)
                ChaineMax = GetStringInUnit(MMax, Enu_TypeVariable.Moment, 4, 2, True)

                DessineDiagrammeRDM(myGr, MyPoutre, MyPoutre.ChargesA(iCas).MYY, kEchM, ColorDiagM, MyParAff, OptionsDiagrammesCDC.lDessValEnv, iNodeMMin, iNodeMMax, ChaineMin, ChaineMax, MMin, MMax)

            End If

            '--> Diagramme de efforts tranchants

            If OptionsDiagrammesCDC.lDessEffortT And lResult Then

                .EnveloppesTranchants(VMax, iNodeVMax, VMin, iNodeMMin)

                kEchM = CoefEchelleDessin(VMin, VMax, EcartZ / 2) * SigneV

                ChaineMin = GetStringInUnit(VMin, Enu_TypeVariable.Effort, 4, 2, True)
                ChaineMax = GetStringInUnit(VMax, Enu_TypeVariable.Effort, 4, 2, True)

                DessineDiagrammeRDM(myGr, MyPoutre, MyPoutre.ChargesA(iCas).VZ, kEchM, ColorDiagV, MyParAff, OptionsDiagrammesCDC.lDessValEnv, iNodeVMin, iNodeVMax, ChaineMin, ChaineMax, VMin, VMax)

            End If

        End With

    End Sub

    Public Sub DessineRDMCombi(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                               iCombi As Integer, typeCombo As String, ByVal lRetraitELU As Boolean, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des diagrammes pour une combinaisons ELU
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre à dessiner
        '   iCas        [E] :   Cas de charge à afficher
        '   lDef        [E] :   Indique si affichage des déformées
        '   lMom        [E] :   Indique si affichage du diagramme de moment
        '   lTranchant  [E] :   Indique si affichage du diagramme de tranchant
        '   lNum        [E] :   Indique si affichage des numéros de noeuds
        '   lInertie    [E] :   Indique si affichage des inerties
        '   lChargement [E] :   Indique si affichage du chargement
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double = 0
        Const kADJUST As Decimal = 0.95
        Dim Longueur As Decimal = MyPoutre.LongueurTotale

        Dim EcartZ As Decimal = Longueur * pHi / pWi
        Dim DiaNode As Decimal = Longueur / 200
        Dim dApp As Decimal = Longueur / 50
        Dim kEch, kEchM As Decimal
        ' Dim lResult As Boolean '= myBeam.ChargesA(iCas).lRunCalcul
        Const SigneM As Decimal = -1
        Const SigneV As Decimal = -1
        Dim MyFontNum As New Font("Arial", 7)
        Dim MyFontLegende As New Font("Arial", 10, FontStyle.Bold)
        Dim Chaine, ChaineMin, ChaineMax As String
        Dim MyPenB As New SolidBrush(Color.Gray)
        'Dim valMin, valMax As Decimal

        Dim ColorDef = Color.DarkOrange
        Dim ColorDiagM = Color.DarkRed
        Dim ColorDiagV = Color.DarkBlue

        Dim iNodeMax As Integer

        Dim ColorPoutre As Color = Color.DarkGray

        Dim MyPenPoutre As New Pen(ColorPoutre)
        Dim MyPenSelect As New Pen(ColorSelect, 2)
        Dim MyPen As Pen

        Dim Combinaison As New cls_Combinaisons
        Dim OptionsDiagrammes As struc_OptionsDiagrammes

        Dim fMin, fMax, VMin, VMax, MMin, MMax As Decimal
        Dim f() As Decimal = Nothing
        Dim V(,) As Decimal = Nothing
        Dim M(,) As Decimal = Nothing

        Dim iNodeVMax, iNodeVMin, iNodeMMax, iNodeMMin As Integer

        Select Case typeCombo
            Case "ELU"
                Combinaison = MyPoutre.CombiA_ELU
                OptionsDiagrammes = OptionsDiagrammesELU
            Case "ELUC"
                Combinaison = MyPoutre.CombiA_ELCU
                OptionsDiagrammes = OptionsDiagrammesELU
            Case "ELF"
                Combinaison = MyPoutre.CombiA_ELF
                OptionsDiagrammes = OptionsDiagrammesELF
            Case "ELS"
                Combinaison = MyPoutre.CombiA_ELS
                OptionsDiagrammes = OptionsDiagrammesELS
            Case "ELSC"
                Combinaison = MyPoutre.CombiA_ELCS
                OptionsDiagrammes = OptionsDiagrammesELS
        End Select



        '--> Initialisation

        Dim MyBrushN As New SolidBrush(Color.White)
        Dim MyPenDef As New Pen(ColorDef)
        Dim MyPenM As New Pen(Color.Blue)

        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -EcartZ / 2
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        dCar = 0.8 * EcartZ / 2






        '--> AffichageOptFeu de la poutre

        AddLigne(myGr, MyPenPoutre, 0, 0, Longueur, 0, MyParAff)

        '--> AffichageOptFeu des noeuds

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            If iNode = iNodeMax Then
                MyPen = MyPenSelect
            Else
                MyPen = MyPenPoutre
            End If
            AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, DiaNode, MyParAff, True, MyPen)
            If OptionsDiagrammes.lDessNumeros Then
                Chaine = "N" & CStr(iNode + 1)
                AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(iNode), 0, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If
        Next

        '--> AffichageOptFeu des appuis

        Dim indAppuis() As Integer = Nothing
        Dim NbApp As Integer
        MyPoutre.ExtraireIndiceNoeudsAppuis(False, indAppuis, NbApp)

        For iApp As Integer = 0 To NbApp - 1
            DessineAppui(myGr, MyPoutre.Nodes.xGlobal(indAppuis(iApp)), dApp, MyParAff)
        Next

        '--> AffichageOptFeu légende couleurs V et M

        If OptionsDiagrammes.lDessMoment Or OptionsDiagrammes.lDessEffortT Then
            Dim longueurRectangle, hauteurRectangle As Decimal
            longueurRectangle = dCar / 6
            hauteurRectangle = longueurRectangle / 6
            Dim xo_legende, yo_legende, xe_legende, ye_legende As Decimal

            If OptionsDiagrammes.lDessEffortT Then
                If OptionsDiagrammes.lDessMoment Then 'légende de V et M
                    'Texte
                    xo_legende = Longueur + dCar / 4
                    yo_legende = -dCar / 8 + hauteurRectangle / 2

                    AddTexte(myGr, New SolidBrush(ColorDiagV), "V", MyFontLegende, xo_legende, yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Effort tranchant
                    AddTexte(myGr, New SolidBrush(ColorDiagM), "M", MyFontLegende, xo_legende, -yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Moment fléchissant 

                    'Rectangle
                    xo_legende = Longueur + dCar / 3
                    xe_legende = xo_legende + longueurRectangle
                    yo_legende = -dCar / 8
                    ye_legende = yo_legende + hauteurRectangle

                    AddRectanglePlein(myGr, ColorDiagV, xo_legende, yo_legende, xe_legende, ye_legende, MyParAff, True) 'Effort tranchant
                    AddRectanglePlein(myGr, ColorDiagM, xo_legende, -yo_legende, xe_legende, -ye_legende, MyParAff, True) 'Moment fléchissant
                Else 'légende de V uniquement
                    'Texte
                    xo_legende = Longueur + dCar / 4
                    yo_legende = 0

                    AddTexte(myGr, New SolidBrush(ColorDiagV), "V", MyFontLegende, xo_legende, yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Effort tranchant

                    'Rectangle
                    xo_legende = Longueur + dCar / 3
                    xe_legende = xo_legende + longueurRectangle
                    yo_legende = 0
                    ye_legende = yo_legende + hauteurRectangle

                    AddRectanglePlein(myGr, ColorDiagV, xo_legende, yo_legende, xe_legende, ye_legende, MyParAff, True) 'Effort tranchant
                End If
            Else 'légende de M uniquement 
                'Texte
                xo_legende = Longueur + dCar / 4
                yo_legende = 0

                AddTexte(myGr, New SolidBrush(ColorDiagM), "M", MyFontLegende, xo_legende, yo_legende, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle) 'Effort tranchant

                'Rectangle
                xo_legende = Longueur + dCar / 3
                xe_legende = xo_legende + longueurRectangle
                yo_legende = 0
                ye_legende = yo_legende + hauteurRectangle

                AddRectanglePlein(myGr, ColorDiagM, xo_legende, yo_legende, xe_legende, ye_legende, MyParAff, True) 'Effort tranchant
            End If
        End If

        '--> Déformée

        Dim xo, xe, yo, ye As Decimal

        If (typeCombo = "ELS" Or typeCombo = "ELSC") And OptionsDiagrammes.lDessDeformee Then 'And lResult  Then

            Combinaison.CombineFleches(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, f, True)

            kEch = CoefEchelleDessin(fMin, fMax, EcartZ / 2)

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
                xo = MyPoutre.Nodes.xGlobal(iNode)
                xe = MyPoutre.Nodes.xGlobal(iNode + 1)
                yo = f(iNode) * kEch
                ye = f(iNode + 1) * kEch
                AddLigne(myGr, MyPenDef, xo, yo, xe, ye, MyParAff)
            Next

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), kEch * f(iNode), DiaNode, MyParAff, True, MyPenDef)
            Next

        End If

        '--> Diagramme de Moments de flexion

        If OptionsDiagrammes.lDessMoment Then 'And lResult Then

            Combinaison.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, M, lRetraitELU)
            PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(M, M.GetUpperBound(0) + 1, MMax, MMin, iNodeMMax, iNodeMMin)

            kEchM = CoefEchelleDessin(MMin, MMax, EcartZ / 2) * SigneM

            ChaineMin = GetStringInUnit(MMin, Enu_TypeVariable.Moment, 4, 2, True)
            ChaineMax = GetStringInUnit(MMax, Enu_TypeVariable.Moment, 4, 2, True)

            DessineDiagrammeRDM(myGr, MyPoutre, M, kEchM, ColorDiagM, MyParAff, OptionsDiagrammes.lDessValEnv, iNodeMMin, iNodeMMax, ChaineMin, ChaineMax, MMin, MMax)

        End If

        '--> Diagramme de efforts tranchants

        If OptionsDiagrammes.lDessEffortT Then 'And lResult Then

            Combinaison.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, V, lRetraitELU)
            PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(V, V.GetUpperBound(0) + 1, VMax, VMin, iNodeVMax, iNodeVMin)

            ChaineMin = GetStringInUnit(VMin, Enu_TypeVariable.Effort, 4, 2, True)
            ChaineMax = GetStringInUnit(VMax, Enu_TypeVariable.Effort, 4, 2, True)

            kEchM = CoefEchelleDessin(VMin, VMax, EcartZ / 2) * SigneV


            DessineDiagrammeRDM(myGr, MyPoutre, V, kEchM, ColorDiagV, MyParAff, OptionsDiagrammes.lDessValEnv, iNodeVMin, iNodeVMax, ChaineMin, ChaineMax, VMin, VMax)

        End If

    End Sub

    'Private Function CoefEchelleDessin(RmaxG As Decimal, RminG As Decimal, RmaxL As Decimal, RminL As Decimal, lDessEchLocal As Boolean, dCar As Decimal, Optional Epsilon As Decimal = 0.001) As Decimal
    '    '-----------------------------------------------------------------------------------------------
    '    '   18/09/23 :  Version 1.00
    '    '-----------------------------------------------------------------------------------------------
    '    '   Représentation d'un diagramme moment ou effort tranchant
    '    '-----------------------------------------------------------------------------------------------
    '    '   RmaxG, RminG    [E] :   Valeurs min et max de la variable obtenues pour tous les cas de charge
    '    '   RmaxL, RminL    [E] :   Valeurs min et max de la variable obtenues pour le cas de charge traité
    '    '   myParAff        [E] :   Paramètre affichage
    '    '-----------------------------------------------------------------------------------------------

    '    '--> Déclaration

    '    Dim kEch As Decimal
    '    Dim valMin, valMax As Decimal

    '    '--> Traitement

    '    If lDessEchLocal Then
    '        valMin = RminL
    '        valMax = RmaxL
    '    Else
    '        valMin = RminG
    '        valMax = RmaxG
    '    End If

    '    If IsEqual(Math.Abs(valMin), 0, Epsilon) And (IsEqual(valMax, 0, Epsilon)) Then
    '        kEch = 1
    '    Else
    '        kEch = dCar / (Math.Max(Math.Abs(valMin), valMax))
    '    End If

    '    Return kEch
    'End Function

    Private Function CoefEchelleDessin(valMin As Decimal, valMax As Decimal, dCar As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   04/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Retourne le coefficient d'échelle à utiliser pour un diagramme
        '-----------------------------------------------------------------------------------------------
        '   valMin      [E] :   valeur mini du diagramme
        '   valMax      [E] :   valeur maxi du diagramme
        '   dCar        [E] :   valeur caractéristique pour l'affichage du diagramme
        '-----------------------------------------------------------------------------------------------

        Dim kEch As Decimal
        Dim valAbsMax As Decimal

        valAbsMax = Math.Max(Math.Abs(valMin), valMax)
        If IsEqual(valAbsMax, 0) Then kEch = 1 Else kEch = dCar / valAbsMax

        Return kEch
    End Function


    'Private Sub DessineDiagrammeRDM(myGr As Graphics, myPoutre As cls_Poutre,
    '                                Courbe(,) As Decimal, kEchC As Decimal, CouleurC As Color, myParAff As Struc_Affichage)
    '    '-----------------------------------------------------------------------------------------------
    '    '   18/09/23 :  Version 1.00
    '    '-----------------------------------------------------------------------------------------------
    '    '   Représentation d'un diagramme moment ou effort tranchant
    '    '-----------------------------------------------------------------------------------------------
    '    '   myGr        [E] :   Graphics dans lequel on dessine
    '    '   MyChargeA   [E] :   Cas de charge
    '    '   myParAff    [E] :   Paramètre affichage
    '    '-----------------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim xo, xe, yo, ye As Decimal
    '    Dim myPenC As New Pen(CouleurC, 1.5)

    '    '--> AffichageOptFeu

    '    xo = 0
    '    xe = 0
    '    yo = 0
    '    ye = Courbe(0, 1) * kEchC
    '    If Not IsEqual(yo, ye) Then
    '        AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)
    '    End If

    '    For iNode As Integer = 0 To myPoutre.Nodes.nbNodes - 2
    '        xo = myPoutre.Nodes.xGlobal(iNode)
    '        xe = myPoutre.Nodes.xGlobal(iNode + 1)
    '        yo = Courbe(iNode, 1) * kEchC
    '        ye = Courbe(iNode + 1, 0) * kEchC
    '        AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)

    '        If iNode < myPoutre.Nodes.nbNodes - 2 Then
    '            yo = Courbe(iNode + 1, 1) * kEchC
    '            If Not IsEqual(yo, ye) Then
    '                AddLigne(myGr, myPenC, xe, yo, xe, ye, myParAff)
    '            End If
    '        End If

    '    Next

    '    xe = myPoutre.LongueurTotale
    '    yo = 0
    '    ye = Courbe(myPoutre.Nodes.nbNodes - 1, 0) * kEchC

    '    If Not IsEqual(yo, ye) Then
    '        AddLigne(myGr, myPenC, xe, yo, xe, ye, myParAff)
    '    End If

    'End Sub

    Private Sub DessineChargement(ByRef myGr As Graphics, MyChargeA As cls_CasDeCharge, iTravD As Integer, iTravF As Integer,
                                  kEchDef As Decimal, dCar As Decimal, xSec() As Decimal, NbSec As Integer, myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/09/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation du chargement
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   MyChargeA   [E] :   Cas de charge
        '   iTravD      [E] :   Indice première travée
        '   iTravF      [E] :   Indice dernière travée
        '   kEchDef     [E] :   Facteur d'échelle pour les déformée
        '   dCar        [E] :   
        '   xSec        [E] :   Position des noeuds
        '   NbSec       [E] ;   Nombre de noeuds
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPos, yPos As Decimal
        Dim dFleche As Decimal
        Dim ForceMax, FRepMax As Decimal
        Dim kEchF As Decimal
        Dim iTrav, j As Integer
        Dim kEchR As Decimal
        Dim CouleurC As Color = Color.DarkGreen
        Dim CouleurR As Color = Color.DimGray

        '--> Calcul des coefficients d'échelle

        ForceMax = MyChargeA.EffortPmax(iTravD, iTravF)
        If IsEqual(ForceMax, 0) Then
            kEchF = 1
        Else
            kEchF = dCar / ForceMax / 2
        End If

        FRepMax = MyChargeA.EffortRepMax(iTravD, iTravF)
        If IsEqual(FRepMax, 0) Then
            kEchR = 1
        Else
            kEchR = dCar / FRepMax / 2
        End If

        '--> Représentation des efforts

        For iTrav = iTravD To iTravF

            For j = 0 To MyChargeA.Forces(iTrav).Count - 1

                xPos = MyChargeA.Forces(iTrav)(j).xPosG
                yPos = 0
                dFleche = MyChargeA.Forces(iTrav)(j).Force * kEchF
                'DessinForcePonctuelle(myGr, xPos, yPos, dFleche, myParAff)
                AddFlecheForce(myGr, xPos, yPos, dFleche / 5, dFleche, 0, CouleurR, CouleurC, True, False, True, myParAff)
            Next

        Next

        '--> Représentation des moments

        For iTrav = iTravD To iTravF
            For j = 0 To MyChargeA.Moments(iTrav).Count - 1

                xPos = MyChargeA.Moments(iTrav)(j).xPosG
                yPos = 0
                '                dFleche = MyChargeA.Forces(iTrav)(j).Force * kFleche

                AddFlecheMoment(myGr, xPos, yPos, MyChargeA.Moments(iTrav)(j).Moment > 0, dCar / 5, CouleurR, CouleurC, True, myParAff)
            Next
        Next

        '--> Représentation des charges réparties

        For iTrav = iTravD To iTravF
            For j = 0 To MyChargeA.FReparties(iTrav).Count - 1
                DessineForceRep(myGr, MyChargeA.FReparties(iTrav)(j).xPosG, MyChargeA.FReparties(iTrav)(j).Force, kEchR, xSec, NbSec, CouleurC, myParAff)
            Next
        Next

    End Sub

    Private Sub DessineForceRep(ByRef myGr As Graphics, xPos() As Decimal, ForceR() As Decimal, kEch As Decimal,
                                xSec() As Decimal, NbSec As Integer, CouleurC As Color, myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/09/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation d'une force répartie
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   xPos        [E] :   Position des extrémités du chargement
        '   ForceR      [E] :   Force aux extrémités du chargement (par Unité de L)
        '   xSec        [E] :   Position des noeuds
        '   NbSec       [E] ;   Nombre de noeuds
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim myPen As New Pen(CouleurC, 0.75)
        Const EPSX As Decimal = 0.5
        Dim qR As Decimal

        '--> AffichageOptFeu

        xo = xPos(0)
        xe = xo
        yo = 0
        ye = kEch * ForceR(0)

        AddFleche(myGr, myPen, xo, yo, xe, ye, myParAff, True, False)

        xo = xPos(1)
        xe = xo
        yo = 0
        ye = kEch * ForceR(1)

        AddFleche(myGr, myPen, xo, yo, xe, ye, myParAff, True, False)

        xo = xPos(0)
        xe = xPos(1)
        yo = kEch * ForceR(0)
        ye = kEch * ForceR(1)

        AddLigne(myGr, myPen, xo, yo, xe, ye, myParAff)

        '--> Tracé sur les noeuds

        For i As Integer = 0 To NbSec - 1
            If IsGreater(xSec(i), xPos(0), EPSX) And IsSmaller(xSec(i), xPos(1), EPSX) Then
                qR = ForceR(0) + (ForceR(1) - ForceR(0)) / (xPos(1) - xPos(0)) * (xSec(i) - xPos(0))
                AddFleche(myGr, myPen, xSec(i), 0, xSec(i), qR * kEch, myParAff, True, False)
            End If
        Next

    End Sub

    Private Sub DessineProp(ByRef myGr As Graphics, MyPoutre As cls_Poutre, iCas As Integer,
                            dCar As Decimal, myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation du chargement
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   MyChargeA   [E] :   Cas de charge
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iElts As Integer
        Dim InertieMax As Decimal
        Dim i As Integer
        Dim kIne As Decimal
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Const kUnitI As Decimal = 10 ^ 8
        Dim MyFontNum As New Font("Arial", 7)
        Dim Chaine As String
        Dim MyPenB As New SolidBrush(Color.Gray)

        '--> Initialisation

        iElts = MyPoutre.ChargesA(iCas).IndElts

        InertieMax = MyPoutre.Elements(iElts).InertieY(0) * kUnitI

        For i = 1 To MyPoutre.Nodes.nbNodes - 2
            InertieMax = Math.Max(InertieMax, MyPoutre.Elements(iElts).InertieY(i) * kUnitI)
        Next

        If IsEqual(InertieMax, 0) Then kIne = 1 Else kIne = dCar / InertieMax

        '--> Représentation de l'inertie

        For i = 0 To MyPoutre.Nodes.nbNodes - 2

            xo = MyPoutre.Nodes.xGlobal(i)
            xe = MyPoutre.Nodes.xGlobal(i + 1)
            yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
            ye = yo
            AddLigne(myGr, xo, yo, xe, ye, myParAff)

        Next

        '>> Valeurs

        '# Extrémités

        i = 0
        Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
        yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
        AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Top)

        i = MyPoutre.Nodes.nbNodes - 2
        Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
        yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
        AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i + 1), yo, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

        '# Travée

        If MyPoutre.lTraveeConsoleGauche Then

            i = MyPoutre.Nodes.iNodeExtTrav(1, 0)
            Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
            yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
            AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Top)

        End If

        If MyPoutre.lTraveeConsoleDroite Then

            i = MyPoutre.Nodes.iNodeExtTrav(1, 1)
            Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i - 1), Enu_TypeVariable.InertieCM4, 4, 0, False)
            yo = MyPoutre.Elements(iElts).InertieY(i - 1) * kIne * kUnitI
            AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

        End If

        '# Mi travée

        i = (MyPoutre.Nodes.iNodeExtTrav(1, 0) + MyPoutre.Nodes.iNodeExtTrav(1, 1)) / 2
        Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
        yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
        AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Top)

    End Sub

    Private Sub AddFlecheForce(ByVal MyGr As Graphics, ByVal xForce As Double, ByVal yForce As Double,
                               ByVal dCarac As Double, ByVal hFleche As Double, ByVal Alpha As Double,
                               ByVal Couleur As Color, CouleurContour As Color, ByVal lRemplissage As Boolean,
                               ByVal lSelected As Boolean, ByVal lPositif As Boolean, myParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------------------------------
        '   19/09/23 :  Création - POM - V1.0
        '----------------------------------------------------------------------------------------------------------------
        '   Dessin d'un flèche représentant une force ponctuelle
        '----------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xForce,yForce   [E] :   Position de la flèche
        '   dCarac          [E] :   Dimension caracteristique
        '   hFleche         [E] :   Hauteur de la flèche
        '   Alpha           [E] :   Inclinaison de la flèche / axe vertical
        '   Couleur         [E] :   Couleur d'affichage (remplissage)
        '   lRemplissage    [E] :   Indique si remplissage de la flèche
        '   lSelected       [E] :   Indique si fleche selectionnee on non
        '   lPositif        [E] :   Indique le sens de representation de l'effort
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations 

        Dim myBrush As LinearGradientBrush
        Dim DeltaAlpha As Double = 0
        Dim DeltaY As Double = 0
        Dim DeltaX As Double
        If Not lPositif Then
            DeltaAlpha = Math.PI
            DeltaY = hFleche * (Math.Cos(Alpha))
            DeltaX = hFleche * (Math.Sin(Alpha))
        End If
        Dim CosA As Double = (Math.Cos(Alpha + DeltaAlpha))
        Dim SinA As Double = (Math.Sin(Alpha + DeltaAlpha))
        Dim ColorDeg As Color = Color.Cornsilk

        '--> Préparation du pinceau

        If lSelected Then
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xForce + DeltaX + dCarac * CosA), YEcran(myParAff, yForce + DeltaY + dCarac * SinA)),
                                              New PointF(XEcran(myParAff, xForce + DeltaX - dCarac * CosA + hFleche * SinA), YEcran(myParAff, yForce + DeltaY + hFleche * CosA + dCarac * SinA)), Color.Red, ColorDeg)
        Else
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xForce + DeltaX - dCarac * CosA + hFleche * SinA), YEcran(myParAff, yForce + DeltaY + hFleche * CosA + dCarac * SinA)),
                                              New PointF(XEcran(myParAff, xForce + DeltaX + dCarac * CosA), YEcran(myParAff, yForce + DeltaY + dCarac * SinA)), ColorDeg, Couleur)
        End If

        '--> Dessin de la flèche

        AddFlecheGeneral(MyGr, myBrush, xForce + DeltaX, yForce + DeltaY, hFleche, dCarac, dCarac, 2 * dCarac, Alpha + DeltaAlpha, myParAff, True, lRemplissage, False, CouleurContour)

        myBrush.Dispose()
    End Sub

    Private Sub AddFlecheMomentGeneral(ByVal MyGr As Graphics,
                                       ByVal xPos As Double, ByVal yPos As Double, ByVal Angle As Double,
                                       ByVal lGauche As Boolean, ByVal lPositif As Boolean,
                                       ByVal dCarac As Double, ByVal Couleur As Color, myParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------------------------------
        '   19/09/23 :  Création - POM - V1.0
        '----------------------------------------------------------------------------------------------------------------
        '   Dessin d'un flèche représentant un moment aux extrémités
        '----------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xPos,yPos       [E] :   Position de l'extremite de la poutre concernée
        '   Angle           [E] :   Inclinaison supplémentaire de la flèche
        '   lGauche         [E] :   Indique si extremité gauche ou droite
        '   lPositif        [E] :   Inidique si Moment positif
        '   dCarac          [E] :   Dimension caractéristique
        '   Couleur         [E] :   Couleur de remplissage de la flèche
        '----------------------------------------------------------------------------------------------------------------

        Dim myBrush As LinearGradientBrush
        Dim xCentre, yCentre As Double
        Dim AlphaO, AlphaE As Double
        Dim ColorDeg As Color = Color.Cornsilk

        yCentre = yPos
        If lGauche Then
            xCentre = xPos - dCarac * 0.75
            If lPositif Then
                AlphaE = 3 * Math.PI / 4 + Angle
                AlphaO = 5 * Math.PI / 4 + Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2 - dCarac / 3)), ColorDeg, Couleur)
            Else
                AlphaE = 5 * Math.PI / 4 + Angle
                AlphaO = 3 * Math.PI / 4 + Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2 + dCarac / 3)), ColorDeg, Couleur)
            End If
        Else
            xCentre = xPos + dCarac * 0.75
            If lPositif Then
                AlphaE = -Math.PI / 4 - Angle
                AlphaO = Math.PI / 4 - Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2 + dCarac / 3)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2)), Couleur, ColorDeg)
            Else
                AlphaE = Math.PI / 4 - Angle
                AlphaO = -Math.PI / 4 - Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2 - dCarac / 3)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2)), Couleur, ColorDeg)
            End If
        End If

        AddFlecheCourbe(MyGr, myBrush, xCentre, yCentre, AlphaO, AlphaE, dCarac / 2, dCarac / 4, dCarac / 3, dCarac / 2, myParAff, True, True, Color.Black)

        myBrush.Dispose()

    End Sub

    Private Sub AddFlecheMoment(ByVal MyGr As Graphics, ByVal xPos As Double, ByVal yRef As Double,
                                ByVal lPositif As Boolean,
                                ByVal dCarac As Double, ByVal Couleur As Color, CouleurContour As Color, lRemplissage As Boolean, myParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------------------------------
        '   19/09/23 :  Création - POM - V1.0
        '----------------------------------------------------------------------------------------------------------------
        '   Dessin d'un flèche représentant un moment aux extrémités
        '----------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xPos            [E] :   Position de la fleche
        '   yRef            [E] :   Position y de la fleche
        '   lPositif        [E] :   Indique si moment positif
        '   dCarac          [E] :   Dimension caracteristique
        '   Couleur         [E] :   Couleur de remplissage de la flèche
        '   CouleurContour  [E] :   Couleur de contour de la flèche
        '   lRemplissage    [E] :   
        '   myParAff        [E] :
        '----------------------------------------------------------------------------------------------------------------

        Dim myBrush As LinearGradientBrush
        Dim xRef As Double
        Dim AlphaO, AlphaE As Double
        Dim ColorDeg As Color = Color.Cornsilk

        'If lGauche Then
        'xRef = -dCarac * 0.75
        If lPositif Then
            AlphaE = 3 * Math.PI / 2 'Math.PI / 4
            AlphaO = 5 * Math.PI / 2 '3 * Math.PI / 4
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef + dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef - dCarac / 2 - dCarac / 3)), ColorDeg, Couleur)
        Else
            AlphaE = -Math.PI / 2
            AlphaO = -3 * Math.PI / 2
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef - dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef + dCarac / 2 + dCarac / 3)), ColorDeg, Couleur)
        End If

        AddFlecheCourbe(MyGr, myBrush, xPos, yRef, AlphaO, AlphaE, dCarac / 2, dCarac / 4, dCarac / 3, dCarac / 2, myParAff, True, lRemplissage, CouleurContour)

        myBrush.Dispose()
    End Sub

    Sub AddFlecheCourbe(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                        ByVal xP As Double, ByVal yP As Double,
                        ByVal AlphaO As Double, ByVal AlphaE As Double,
                        ByVal RayInt As Double, ByVal Epb As Double,
                        ByVal HPointe As Double, ByVal EpPointe As Double,
                        ByRef ParAff As Struc_Affichage,
                        ByVal lContour As Boolean, ByVal lRemplissage As Boolean, CouleurContour As Color)
        '-------------------------------------------------------------------------------------------------
        '   13/02/08 :  Création - Version 1.00
        '-------------------------------------------------------------------------------------------------
        '
        '   Ajout d'une flèche courbe pour représenter un moemnt
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   MyBrush         [E] :   Pinceau pour le remplissage de la flèche
        '   xP, yP          [E] :   Coordonnées du centre
        '   Alpha0          [E] :   Angle origine
        '   AlphaE          [E] :   Angle extremite
        '   RayInt          [E] :   Rayon intérieur 
        '   Epb             [E] :   Largeur de la jambe de la flèche
        '   HPoint          [E] :   Hauteur de l'extremite de la flèche
        '   EpPointe        [E] :   Largeur de l'extremite de la flèche
        '   ParAff          [E] :   Paramètres de l'affichage
        '   lContour        [E] :   Indique si la routine appelante requiert le tracé du contour de la flèche
        '   lRemplissage    [E] :   Indique si remplissage
        '
        '-------------------------------------------------------------------------------------------------

        'LA POINTE DE LA FLECHE EST DU COTE ORIGINE

        Const nDiv As Integer = 8
        Dim DeltaA As Double = (AlphaE - AlphaO) / (nDiv - 1)
        Dim RayExt As Double = RayInt + Epb

        Dim x1, x2, y1, y2 As Double
        Dim x12, y12 As Double
        Dim Norme As Double

        Dim PtsFleche(2 * nDiv + 2) As Point

        For i As Integer = 0 To nDiv - 1
            PtsFleche(i).X = CInt(XEcran(ParAff, xP + RayInt * (Math.Cos(AlphaO + i * DeltaA))))
            PtsFleche(i).Y = CInt(YEcran(ParAff, yP + RayInt * (Math.Sin(AlphaO + i * DeltaA))))
            PtsFleche(i + nDiv).X = CInt(XEcran(ParAff, xP + RayExt * (Math.Cos(AlphaE - i * DeltaA))))
            PtsFleche(i + nDiv).Y = CInt(YEcran(ParAff, yP + RayExt * (Math.Sin(AlphaE - i * DeltaA))))
        Next

        Dim d As Double = (EpPointe - Epb) / 2

        x1 = XEcran(ParAff, xP + (RayExt + d) * (Math.Cos(AlphaO)))
        x2 = XEcran(ParAff, xP + (RayInt - d) * (Math.Cos(AlphaO)))
        y1 = YEcran(ParAff, yP + (RayExt + d) * (Math.Sin(AlphaO)))
        y2 = YEcran(ParAff, yP + (RayInt - d) * (Math.Sin(AlphaO)))

        PtsFleche(2 * nDiv).X = CInt(x1)
        PtsFleche(2 * nDiv).Y = CInt(y1)
        PtsFleche(2 * nDiv + 2).X = CInt(x2)
        PtsFleche(2 * nDiv + 2).Y = CInt(y2)

        x12 = x2 - x1
        y12 = y2 - y1
        Norme = (Math.Sqrt(x12 ^ 2 + y12 ^ 2))
        Dim kSigne As Double = 1
        If AlphaE < AlphaO Then kSigne = -1

        PtsFleche(2 * nDiv + 1).X = CInt(x1 + x12 / 2 + kSigne * y12 / Norme * HPointe * ParAff.CRed)
        PtsFleche(2 * nDiv + 1).Y = CInt(y1 + y12 / 2 - kSigne * x12 / Norme * HPointe * ParAff.CRed)

        If lRemplissage Then MyGr.FillPolygon(MyBrush, PtsFleche)

        If lContour Then MyGr.DrawPolygon(New Pen(CouleurContour), PtsFleche)

    End Sub

    Sub AddFlecheGeneral(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                         ByVal xP As Single, ByVal yP As Single,
                         ByVal Ht As Single, ByVal Epb As Single,
                         ByVal HPointe As Single, ByVal EpPointe As Single,
                         ByVal AlphaF As Single, ByRef ParAff As Struc_Affichage,
                         ByVal lContour As Boolean, ByVal lRemplissage As Boolean, ByVal lAxe As Boolean, CouleurContour As Color)
        '-------------------------------------------------------------------------------------------------
        '
        '   10/02/08 :  Création - Version 1.00
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   Ajout d'une flèche selon un vecteur directeur
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   MyBrush         [E] :   Pinceau pour le remplissage de la flèche
        '   xP, yP          [E] :   Coordonnées de la pointe de la flèche
        '   Ht              [E] :   Hauteur de la flèche (coordonnée poutre)
        '   Epb             [E] :   Largeur de la jambe de la flèche
        '   HPoint          [E] :   Hauteur de l'extremite de la flèche
        '   EpPointe        [E] :   Largeur de l'extremite de la flèche
        '   AlphaF          [E] :   Orientation de la fleche (angle de rotation trigo / axe vertical)
        '   ParAff          [E] :   Paramètres de l'affichage
        '   lContour        [E] :   Indique si la routine appelante requiert le tracé du contour de la flèche
        '   lRemplissage    [E] :   Indique si remplissage
        '   lAxe            [E] :   Indique si le tracé de l'axe est requis !!!
        '
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

        If AlphaF <> 0 Then
            Dim CosinusA As Single = CSng(Math.Cos(AlphaF))
            Dim SinusA As Single = CSng(Math.Sin(AlphaF))
            Dim xNew, yNew As Single
            For i As Integer = 1 To 6
                xNew = PtsFleche(0).X + (PtsFleche(i).X - PtsFleche(0).X) * CosinusA - (PtsFleche(i).Y - PtsFleche(0).Y) * SinusA
                yNew = PtsFleche(0).Y + (PtsFleche(i).X - PtsFleche(0).X) * SinusA + (PtsFleche(i).Y - PtsFleche(0).Y) * CosinusA
                PtsFleche(i).X = CInt(xNew)
                PtsFleche(i).Y = CInt(yNew)
            Next
        End If

        If lRemplissage Then MyGr.FillPolygon(MyBrush, PtsFleche)

        If lContour Then MyGr.DrawPolygon(New Pen(CouleurContour), PtsFleche)

        Dim xo, yo, xe, ye As Single

        If lAxe Then
            xo = XEcran(ParAff, xP)
            yo = YEcran(ParAff, yP)
            xe = xo
            ye = YEcran(ParAff, yP + Ht)
            MyGr.DrawLine(Pens.Red, xo, yo, xe, ye)
        End If

    End Sub

#End Region

#Region " Outils pour le dessin de la dalle "

    Private Sub DessineDalleMixtePerpendiculaireCfp220(ByRef MyGr As Graphics, myBeam As cls_Poutre, MyParAffA As Struc_Affichage,
                                                       MyBrushDP As Brush, BeffRed As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   08/08/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervures perpendiculaires à la poutre - Cas particulier bac Cofraplus 220
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   MyProfil    [E] :   Géométrie du rofilé métallique
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   BeffRed     [E] :   Largeur de dalle réduite pour le dessin 
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPenContour As New Pen(Color.Black, 1)

        Dim Hp As Decimal = myBeam.Dalle.Bac.Hp
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim Bfs As Decimal = myBeam.Section.ProfilA.Bfs
        Dim zTop As Decimal = myBeam.Dalle.zTop
        Dim xo, xe As Decimal
        Dim wApp As Decimal = myBeam.Dalle.Bac.wAppui

        '--> Préparation du contour de la dalle

        nbPts = 0
        AjoutePoint(-Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(Bfs / 2, 0, xPts, yPts, nbPts)
        AjoutePoint(Bfs / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(BeffRed / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(BeffRed / 2, zTop, xPts, yPts, nbPts)
        AjoutePoint(-BeffRed / 2, zTop, xPts, yPts, nbPts)
        AjoutePoint(-BeffRed / 2, -Hp, xPts, yPts, nbPts)
        AjoutePoint(-Bfs / 2, -Hp, xPts, yPts, nbPts)

        '--> Dessin de la dalle

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, False, True)

        xo = -BeffRed / 2
        xe = BeffRed / 2
        AddLigne(MyGr, MyPenContour, xo, zTop, xe, zTop, MyParAffA)

        xo = BeffRed / 2
        xe = Bfs / 2
        AddLigne(MyGr, MyPenContour, -xo, -Hp, -xe, -Hp, MyParAffA)
        AddLigne(MyGr, MyPenContour, -xe, -Hp, -xe, 0, MyParAffA)
        AddLigne(MyGr, MyPenContour, xo, -Hp, xe, -Hp, MyParAffA)
        AddLigne(MyGr, MyPenContour, xe, -Hp, xe, 0, MyParAffA)

        '--> Dessin du bac

        AddLigne(MyGr, MyPenContour, -xo, 0, -xe, 0, MyParAffA)
        AddLigne(MyGr, MyPenContour, xo, 0, xe, 0, MyParAffA)

    End Sub

    Private Sub DessineDalleMixtePerpendiculaire(ByRef MyGr As Graphics, MyPoutre As cls_Poutre, lIntermediaire As Boolean, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                                 MyBrushDP As Brush, BeffRed As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   29/06/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervures perpendiculaires à la poutre
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   MyDalle     [E] :   
        '   Ha          [E] :   Hauteur du profilé métallique
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   MyParAffA   [E] :   Paramètres d'affichage   
        '   MyBrushDP   [E] :   Pinceau pour le remplissage de la dalle
        '   BeffRed     [E] :   Largeur de dalle réduite pour le dessin 
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim MyPenDot As New Pen(Color.Black, 0.75)
        Dim dCar As Decimal = (MyPoutre.Dalle.Ep_th + MyPoutre.Dalle.Ep_td) / 5
        Dim Td As Decimal = MyPoutre.Dalle.Ep_td
        Dim Hp As Decimal = MyPoutre.Dalle.Bac.Hp
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim wApp As Decimal = MyPoutre.Dalle.Bac.wAppui
        Dim lSlimF As Boolean = MyPoutre.Section.lSlimFloor
        Dim lRiveP As Boolean = MyPoutre.Dalle.lRiveRemplie
        Dim LargeurProfilA As Decimal

        MyPenDot.DashStyle = DashStyle.Custom
        MyPenDot.DashPattern = New Single() {4.0F, 6.0F}

        Select Case MyPoutre.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                LargeurProfilA = MyPoutre.Section.ProfilA.Bfs
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                LargeurProfilA = MyPoutre.Section.ProfilA.Plat_b
                wApp = Math.Min(wApp, 0.7 * (MyPoutre.Section.ProfilA.Plat_b - MyPoutre.Section.ProfilA.Bfi) / 2)
                'xo = -MyProfil.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                LargeurProfilA = MyPoutre.Section.ProfilA.Plat_b
                'xo = -MyProfil.Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                LargeurProfilA = MyPoutre.Section.ProfilA.Bfi
                'xo = -MyProfil.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                LargeurProfilA = MyPoutre.Section.ProfilA.Bfi
                'xo = -MyProfil.Bfi / 2
        End Select

        '--> Dessins

        Dim xDalG, xDalD As Decimal
        Dim yDalI, yDalS As Decimal
        Dim zSemSup As Decimal = MyPoutre.Section.zSemSup

        If lIntermediaire Or Not lSlimF Then
            xDalG = -BeffRed / 2
        Else
            xDalG = -Bfs / 2
        End If
        xDalD = BeffRed / 2
        yDalI = 0
        yDalS = Td

        If lSlimF And (Not lIntermediaire) And (Not lRiveP) Then

            AddRectanglePlein(MyGr, MyBrushDP, MyPenContour, 0, yDalI, xDalD, yDalS, MyParAffA, True, False)

            xo = xDalG
            xe = 0
            yo = zSemSup
            ye = Td
            AddRectanglePlein(MyGr, MyBrushDP, MyPenContour, xo, yo, xe, ye, MyParAffA, True, False)

        Else

            AddRectanglePlein(MyGr, MyBrushDP, MyPenContour, xDalG, yDalI, xDalD, yDalS, MyParAffA, True, False)

        End If

        '# Traits dalle

        AddLigne(MyGr, MyPenContour, xDalG, yDalI, xDalD, yDalI, MyParAffA)
        AddLigne(MyGr, MyPenContour, xDalG, yDalS, xDalD, yDalS, MyParAffA)

        If lIntermediaire Or (Not lSlimF) Then
            'AddLigne(MyGr, MyPenDot, xo, yo - dCar, xo, ye + dCar, MyParAffA)
            AddLigne(MyGr, MyPenDot, xDalG, yDalI - dCar, xDalG, yDalS + dCar, MyParAffA)
        Else
            If lRiveP Then
                AddLigne(MyGr, MyPenContour, xDalG, yDalI, xDalG, yDalS, MyParAffA)
            Else
                AddLigne(MyGr, MyPenContour, xDalG, zSemSup, xDalG, yDalS, MyParAffA)
            End If
        End If
        AddLigne(MyGr, MyPenDot, xDalD, yDalI - dCar, xDalD, yDalS + dCar, MyParAffA)

        '# Représentation des traits pour le bac

        Select Case MyPoutre.Dalle.Bac.AppuiT
            Case cls_Bac.EnuConfigTAppui.Discontinu

                If lIntermediaire Or (Not lSlimF) Then

                    xo = BeffRed / 2
                    xe = LargeurProfilA / 2 - wApp
                    yo = 0
                    ye = Hp

                    AddLigne(MyGr, MyPenContour, -xo, ye, -xe, ye, MyParAffA)
                    AddLigne(MyGr, MyPenContour, -xe, 0, -xe, ye, MyParAffA)

                    AddLigne(MyGr, MyPenContour, xo, ye, xe, ye, MyParAffA)
                    AddLigne(MyGr, MyPenContour, xe, 0, xe, ye, MyParAffA)

                Else

                    xo = BeffRed / 2
                    xe = LargeurProfilA / 2 - wApp
                    yo = 0
                    ye = Hp

                    AddLigne(MyGr, MyPenContour, xo, ye, xe, ye, MyParAffA)
                    AddLigne(MyGr, MyPenContour, xe, 0, xe, ye, MyParAffA)

                End If

            Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus

                AddLigne(MyGr, MyPenContour, xDalG, Hp, xDalD, Hp, MyParAffA)

            Case cls_Bac.EnuConfigTAppui.BetonSeulContinu

                AddLigne(MyGr, MyPenContour, xDalG, Hp, xDalD, Hp, MyParAffA)
                AddLigne(MyGr, MyPenContour, 0, 0, 0, Hp, MyParAffA)

        End Select

        '--> Fin

        MyPenContour.Dispose()
        MyPenDot.Dispose()
    End Sub

    Private Sub DessinDalleSlimFloor(ByRef MyGr As Graphics, MyPoutre As cls_Poutre, Ha As Decimal, MyParAffloc As Struc_Affichage, MyBrushB As Brush, Optional ZREF As Decimal = 0)
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

        xe = -MyPoutre.LargeurDalleDispo / 2
        xo = -xe
        ye = Ha + MyPoutre.Dalle.Ep_td
        yo = ZREF

        AddRectanglePlein(MyGr, MyBrushB, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

    End Sub

    Private Sub DessineDalleMixteParallele(ByRef MyGr As Graphics, MyDalle As cls_Dalle, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
                                           MyBrushDP As Brush, BeffRed As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   29/06/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte avec nervures parallèles à la poutre
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

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        '--> Contour

        PrepareContourDalleMixteParallel(MyDalle, Bfs, BeffRed / 2, BeffRed / 2, xPts, yPts, nbPts)

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, True, True)

    End Sub

    Private Sub PrepareContourDalleMixteParallelOLD(ByVal MyPoutre As cls_Poutre, Bfs As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
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
        eP = MyPoutre.Dalle.Bac.Ep
        hP = MyPoutre.Dalle.Bac.Hp
        hPg = MyPoutre.Dalle.Bac.Hauteur_hpg
        bb = MyPoutre.Dalle.Bac.Bb
        bt = MyPoutre.Dalle.Bac.Bt
        bEff = MyPoutre.LargeurDalleDispo
        DeltaB = (bt - bb) / 2

        lRaid = (((hPg - hP) / hP) > 0.05)

        If (MyPoutre.Dalle.Bac.AppuiL = cls_Bac.EnuConfigLAppui.BacCoupe) Then
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
        yo = MyPoutre.Dalle.Ep_td

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Par symétrie, partie gauche de la dalle

        Dim n0 As Integer = nbPts

        For i = n0 - 1 To 0 Step -1

            AjoutePoint(-xPts(i), yPts(i), xPts, yPts, nbPts)

        Next

    End Sub

    Private Sub PrepareContourDalleMixteParallel(ByVal MyDalle As cls_Dalle, Bfs As Decimal, BeffG As Decimal, BeffD As Decimal,
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

        Dim xPtsG() As Single = Nothing
        Dim yPtsG() As Single = Nothing
        Dim nbPtsG As Integer
        Dim xo, yo As Single

        '--> Initialisaiton

        nbPts = 0

        '--> on commence le contour par le côté droit inférieur

        PrepareContourDalleMixteParalleInfDroite(MyDalle, Bfs, BeffD, False, xPts, yPts, nbPts)

        '--> Partie supérieure de la dalle

        xo = BeffD
        yo = MyDalle.Ep_td
        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -BeffG
        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        '--> Partie gauche de la dalle

        PrepareContourDalleMixteParalleInfDroite(MyDalle, Bfs, BeffG, True, xPtsG, yPtsG, nbPtsG)

        For i = nbPtsG - 1 To 0 Step -1

            AjoutePoint(-xPtsG(i), yPtsG(i), xPts, yPts, nbPts)

        Next

    End Sub

    Private Sub PrepareContourDalleMixteParalleInfDroite(ByVal MyDalle As cls_Dalle, Bfs As Decimal, BeffD As Decimal, lGauche As Boolean,
                                                         ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer, Optional decalBac As Decimal = 0)
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
        '   lFrm_Main   [E] :   Indique si la fonction est appelée pour le dessin du Frm_Main (True) ou non (False)  
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
        eP = MyDalle.Bac.Ep
        hP = MyDalle.Bac.Hp
        hPg = MyDalle.Bac.Hauteur_hpg
        bb = MyDalle.Bac.Bb
        bt = MyDalle.Bac.Bt
        DeltaB = (bt - bb) / 2

        lRaid = (((hPg - hP) / hP) > 0.05)
        If lRaid Then
            bSupBac = eP - bt
            bbRaid = cls_Bac.RATIOB1R * bSupBac
            btRaid = cls_Bac.RATIOB2R * bSupBac
            dXRaid = btRaid - bbRaid
        End If

        If (MyDalle.Bac.AppuiL = cls_Bac.EnuConfigLAppui.BacCoupe) Then
            xStart = Bfs / 2
        Else
            xStart = bb / 2 + decalBac
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

            lCont = (xPos + eP < BeffD)
            'lCont = (xPos + eP < BeffD / 2)
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

    Private Sub DessinDallePreFab(ByRef MyGr As Graphics, MyPoutre As cls_Poutre, lIntermediaire As Boolean, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
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

        Dim Td As Decimal = MyPoutre.Dalle.Ep_td
        Dim Tj As Decimal = MyPoutre.Dalle.preDalle_ep - MyPoutre.Dalle.preDalle_tjoint
        Dim dCar As Decimal = (MyPoutre.Dalle.Ep_td) / 5
        Dim lDalleRed As Boolean
        Dim BeffDes As Decimal = MyPoutre.LargeurDalleDispo
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyPenDot As New Pen(Color.Black, 0.75)
        Dim wApp As Decimal = MyPoutre.Dalle.wAppuiPreDalle
        Dim pred_ep As Decimal = MyPoutre.Dalle.preDalle_ep
        Dim xo, xe As Decimal
        Dim LargeurProfilA As Decimal

        '--> Initialisation

        MyPenDot.DashStyle = DashStyle.Custom
        MyPenDot.DashPattern = New Single() {4.0F, 6.0F}

        Select Case MyPoutre.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                LargeurProfilA = MyPoutre.Section.ProfilA.Bfs
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                LargeurProfilA = MyPoutre.Section.ProfilA.Plat_b
                wApp = Math.Min(wApp, 0.7 * (MyPoutre.Section.ProfilA.Plat_b - MyPoutre.Section.ProfilA.Bfi) / 2)
                'xo = -MyProfil.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                LargeurProfilA = MyPoutre.Section.ProfilA.Plat_b
                'xo = -MyProfil.Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                LargeurProfilA = MyPoutre.Section.ProfilA.Bfi
                'xo = -MyProfil.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                LargeurProfilA = MyPoutre.Section.ProfilA.Bfi
                'xo = -MyProfil.Bfi / 2
        End Select

        If BeffRed = -1 Then
            lDalleRed = False
        Else
            lDalleRed = (BeffRed < MyPoutre.LargeurDalleDispo)
        End If
        If lDalleRed Then BeffDes = BeffRed

        '--> AffichageOptFeu de la dalle pleine (nécessairement sans renformis)

        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            xo = -BeffDes / 2
            xe = -xo
        Else
            xo = -Bfs / 2
            xe = BeffDes / 2
        End If
        AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, 0, xe, Td, MyParAffA, True, False)

        '--> AffichageOptFeu des deux prédalles

        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            xe = -LargeurProfilA / 2 + wApp
            xo = -BeffDes / 2
            AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, 0, xe, pred_ep, MyParAffA, True, False)
            AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)
            AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)

        End If

        xe = +LargeurProfilA / 2 - wApp
        xo = +BeffDes / 2
        AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, 0, xe, pred_ep, MyParAffA, True, False)
        AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)
        AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)

        '--> Joint

        Dim CouleurJ As Color = Color.Linen

        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            xe = -LargeurProfilA / 2 + wApp
            xo = -BeffDes / 2
            AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)
        End If

        xe = +LargeurProfilA / 2 - wApp
        xo = +BeffDes / 2
        AddRectanglePlein(MyGr, CouleurJ, xo, 0, xe, Tj, MyParAffA, False)

        '--> Finitions

        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            xo = -BeffDes / 2
        Else
            xo = -Bfs / 2
        End If
        xe = BeffDes / 2
        AddLigne(MyGr, xo, 0, xe, 0, MyParAffA)
        AddLigne(MyGr, xo, Td, xe, Td, MyParAffA)

        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            AddLigne(MyGr, xo, Tj, -LargeurProfilA / 2 + wApp, Tj, MyParAffA)
            AddLigne(MyGr, -LargeurProfilA / 2 + wApp, 0, -LargeurProfilA / 2 + wApp, pred_ep, MyParAffA)
        End If

        AddLigne(MyGr, xe, Tj, +LargeurProfilA / 2 - wApp, Tj, MyParAffA)
        AddLigne(MyGr, +LargeurProfilA / 2 - wApp, 0, +LargeurProfilA / 2 - wApp, pred_ep, MyParAffA)





        If lIntermediaire Or Not MyPoutre.Section.lSlimFloor Then
            xo = -BeffDes / 2
            xe = -xo

            AddLigne(MyGr, MyPenDot, xo, 0 - dCar, xo, Td + dCar, MyParAffA)
            AddLigne(MyGr, MyPenDot, xe, 0 - dCar, xe, Td + dCar, MyParAffA)
        Else
            xo = -Bfs / 2
            xe = BeffDes / 2

            AddLigne(MyGr, MyPenContour, xo, 0, xo, Td, MyParAffA)
            AddLigne(MyGr, MyPenDot, xe, 0 - dCar, xe, Td + dCar, MyParAffA)
        End If


        '--> Fin

        MyPen.Dispose()
    End Sub

    Private Sub DessinDalleCompletementPrefa(ByRef MyGr As Graphics, myPoutre As cls_Poutre, lIntermediaire As Boolean, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage,
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

        Dim Td As Decimal = myPoutre.Dalle.Ep_td
        'Dim Tj As Decimal = Mypoutre.dalle.preDalle_ep - Mypoutre.dalle.preDalle_tjoint
        Dim dCar As Decimal = (myPoutre.Dalle.Ep_td) / 5
        Dim lDalleRed As Boolean
        Dim BeffDes As Decimal = myPoutre.LargeurDalleDispo
        Dim MyPen As New Pen(Color.Black, 1)
        Dim MyPenDot As New Pen(Color.Black, 0.75)
        Dim wApp As Decimal = myPoutre.Dalle.wAppuiPreDalle
        Dim pred_ep As Decimal = myPoutre.Dalle.Cofradal.dp
        Dim xo, xe As Decimal
        Dim LargeurProfilA As Decimal
        Dim lSlimF As Boolean = myPoutre.Section.lSlimFloor
        Dim lRiveP As Boolean = myPoutre.Dalle.lRiveRemplie
        Dim zSemS As Decimal = myPoutre.Section.zSemSup

        '--> Initialisation

        MyPenDot.DashStyle = DashStyle.Custom
        MyPenDot.DashPattern = New Single() {4.0F, 6.0F}

        Select Case myPoutre.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                LargeurProfilA = myPoutre.Section.ProfilA.Bfs
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                LargeurProfilA = myPoutre.Section.ProfilA.Plat_b
                wApp = Math.Min(wApp, 0.7 * (myPoutre.Section.ProfilA.Plat_b - myPoutre.Section.ProfilA.Bfi) / 2)
                'xo = -MyProfil.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                LargeurProfilA = myPoutre.Section.ProfilA.Plat_b
                'xo = -MyProfil.Bfs / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                LargeurProfilA = myPoutre.Section.ProfilA.Bfi
                'xo = -MyProfil.Bfi / 2
            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                LargeurProfilA = myPoutre.Section.ProfilA.Bfi
                'xo = -MyProfil.Bfi / 2
        End Select

        If BeffRed = -1 Then
            lDalleRed = False
        Else
            lDalleRed = (BeffRed < myPoutre.LargeurDalleDispo)
        End If
        If lDalleRed Then BeffDes = BeffRed

        '--> Affichage de la dalle pleine (nécessairement sans renformis)

        If lIntermediaire Or Not lSlimF Then
            xo = -BeffDes / 2
            xe = -xo
            AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, 0, xe, Td, MyParAffA, True, False)
        ElseIf lRiveP Then
            xo = -Bfs / 2
            xe = BeffDes / 2
            AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, 0, xe, Td, MyParAffA, True, False)
        Else
            xo = 0
            xe = BeffDes / 2
            AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, 0, xe, Td, MyParAffA, True, False)
            xo = -Bfs / 2
            xe = 0
            AddRectanglePlein(MyGr, MyBrushDP, MyPen, xo, zSemS, xe, Td, MyParAffA, True, False)
        End If

        '--> Affichage des deux prédalles

        If lIntermediaire Or Not lSlimF Then
            xe = -LargeurProfilA / 2 + wApp
            xo = -BeffDes / 2
            AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, 0, xe, pred_ep, MyParAffA, True, False)
            AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)
            AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)
        End If

        xe = +LargeurProfilA / 2 - wApp
        xo = +BeffDes / 2
        AddRectanglePlein(MyGr, MyBrushPref, MyPen, xo, 0, xe, pred_ep, MyParAffA, True, False)
        AddLigne(MyGr, xe, 0, xe, pred_ep, MyParAffA)
        AddLigne(MyGr, xo, pred_ep, xe, pred_ep, MyParAffA)

        '--> Finitions traits horizontaux

        If lIntermediaire Or Not lSlimF Then
            xo = -BeffDes / 2
            xe = BeffDes / 2
            AddLigne(MyGr, -LargeurProfilA / 2 + wApp, 0, -LargeurProfilA / 2 + wApp, pred_ep, MyParAffA)
        Else
            xo = -Bfs / 2
            xe = BeffDes / 2
        End If
        AddLigne(MyGr, xo, 0, xe, 0, MyParAffA)
        AddLigne(MyGr, xo, Td, xe, Td, MyParAffA)

        AddLigne(MyGr, +LargeurProfilA / 2 - wApp, 0, +LargeurProfilA / 2 - wApp, pred_ep, MyParAffA)

        '--( Traits aux extrémités

        If lIntermediaire Or Not lSlimF Then
            xo = -BeffDes / 2
            xe = BeffDes / 2
        Else
            xo = -Bfs / 2
            xe = BeffDes / 2
        End If

        If lIntermediaire Or Not lSlimF Then
            AddLigne(MyGr, MyPenDot, xo, 0 - dCar, xo, Td + dCar, MyParAffA)
        ElseIf lRiveP Then
            AddLigne(MyGr, MyPenContour, xo, 0, xo, Td, MyParAffA)
        Else
            AddLigne(MyGr, MyPenContour, xo, zSemS, xo, Td, MyParAffA)
        End If
        AddLigne(MyGr, MyPenDot, xe, 0 - dCar, xe, Td + dCar, MyParAffA)

        '--> Fin

        MyPen.Dispose()
    End Sub

    Private Sub DessinDallePleine(ByRef MyGr As Graphics, myBeam As cls_Poutre, lIntermediaire As Boolean, Ha As Decimal, Bfs As Decimal, MyParAffA As Struc_Affichage, MyBrushDP As Brush,
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
        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim lDalleRed As Boolean
        Dim dCar As Decimal = (myBeam.Dalle.Ep_th + myBeam.Dalle.Ep_td) / 5
        Dim BeffDes As Decimal = myBeam.LargeurDalleDispo
        Dim MyPenDot As New Pen(Color.Black, 0.75)

        '--> Initialisation

        If BeffRed = -1 Then
            lDalleRed = False
        Else
            lDalleRed = (BeffRed < myBeam.LargeurDalleDispo)
        End If
        If lDalleRed Then BeffDes = BeffRed

        MyPenDot.DashStyle = DashStyle.Custom
        MyPenDot.DashPattern = New Single() {4.0F, 6.0F}

        '--> Préparation des points

        PrepareContourDallePleine(myBeam, lIntermediaire, BeffDes, Bfs, xPts, yPts, nbPts)

        '--> Affichage

        RemplirZone(MyGr, MyBrushDP, xPts, yPts, nbPts, MyParAffA, Not lDalleRed, True)

        If lDalleRed Then
            Dim xo, yo As Decimal
            Dim xe, ye As Decimal
            Dim Th As Decimal = myBeam.Dalle.EpRenformis

            If lIntermediaire Or Not myBeam.Section.lSlimFloor Then

                '** CAS GENERAL

                xo = -BeffRed / 2
                xe = -xo
                yo = myBeam.Dalle.zTop
                ye = yo

                AddLigne(MyGr, xo, yo, xe, ye, MyParAffA)

                PrepareLigneFaceInfDallePleine(myBeam.Dalle, BeffDes, Bfs, xPts, yPts, nbPts)
                'DecalePts(yPts, nbPts, Ha / 2)

                AddLignePolyG(MyGr, xPts, yPts, nbPts, MyParAffA)

                xo = -BeffRed / 2
                xe = xo
                yo = Th - dCar
                ye = myBeam.Dalle.zTop + dCar

                AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAffA)

                xo = BeffRed / 2
                xe = xo

                AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAffA)

            Else

                '** CAS Slimfloor ET poutre de rive

                xo = -Bfs / 2
                xe = BeffRed / 2
                yo = myBeam.Dalle.zTop
                ye = yo

                AddLigne(MyGr, xo, yo, xe, ye, MyParAffA)

                xo = -Bfs / 2
                xe = BeffRed / 2
                yo = 0
                ye = yo

                AddLigne(MyGr, xo, yo, xe, ye, MyParAffA)

                xo = -Bfs / 2
                xe = xo
                ye = myBeam.Dalle.zTop

                If myBeam.Dalle.lRiveRemplie Then
                    yo = Th
                Else
                    yo = myBeam.Section.zSemSup
                End If
                AddLigne(MyGr, MyPenContour, xo, yo, xe, ye, MyParAffA)

                xo = BeffRed / 2
                xe = xo

                yo = Th - dCar
                ye = myBeam.Dalle.zTop + dCar

                AddLigne(MyGr, MyPenDot, xo, yo, xe, ye, MyParAffA)
            End If

        End If

        MyPenContour.Dispose()
        MyPenDot.Dispose()
    End Sub

    Private Sub PrepareContourDallePleine(ByVal MyPoutre As cls_Poutre, lIntermediare As Boolean, BeffDes As Decimal, Bfs As Decimal,
                                          ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
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

        If lIntermediare Or Not MyPoutre.Section.lSlimFloor Then

            '== CAS GENERAL ==========================================================

            xo = Bfs / 2
            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = CSng(Bfs / 2 + MyPoutre.Dalle.Ep_th * Math.Tan(MyPoutre.Dalle.ThetaRd))
            yo = MyPoutre.Dalle.Ep_th

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = BeffDes / 2

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            yo = MyPoutre.Dalle.Ep_td + MyPoutre.Dalle.Ep_th

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = -BeffDes / 2

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            yo = MyPoutre.Dalle.Ep_th

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = CSng(-Bfs / 2 - MyPoutre.Dalle.Ep_th * Math.Tan(MyPoutre.Dalle.ThetaRd))
            yo = MyPoutre.Dalle.Ep_th

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = -Bfs / 2
            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

        Else

            Dim lRiveP As Boolean = MyPoutre.Dalle.lRiveRemplie

            '=== CAS D'UNE SLIM FLOOR EN RIVE

            If lRiveP Then

                xo = -Bfs / 2
                yo = 0

                AjoutePoint(xo, yo, xPts, yPts, nbPts)

            Else

                xo = 0
                yo = 0

                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                yo = MyPoutre.Section.zSemSup - MyPoutre.Section.Epplatsup / 2

                AjoutePoint(xo, yo, xPts, yPts, nbPts)

                xo = -Bfs / 2

                AjoutePoint(xo, yo, xPts, yPts, nbPts)

            End If


            yo = MyPoutre.Dalle.Ep_td

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            xo = BeffDes / 2

            AjoutePoint(xo, yo, xPts, yPts, nbPts)

            yo = 0

            AjoutePoint(xo, yo, xPts, yPts, nbPts)
        End If


    End Sub


    Private Sub PrepareLigneFaceInfDallePleine(ByVal MyDalle As cls_Dalle, BeffDes As Decimal, Bfs As Decimal, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
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
        yo = MyDalle.Ep_th

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(-Bfs / 2 - MyDalle.Ep_th * Math.Tan(MyDalle.ThetaRd))
        yo = MyDalle.Ep_th

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = -Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = Bfs / 2
        yo = 0

        AjoutePoint(xo, yo, xPts, yPts, nbPts)

        xo = CSng(Bfs / 2 + MyDalle.Ep_th * Math.Tan(MyDalle.ThetaRd))
        yo = MyDalle.Ep_th

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
                                   zRef As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   01/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin du profilé métallique
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre supérieure de la mySection acier)
        '---------------------------------------------------------------------------------------------------------------------------

        DessinProfileMetal(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, False, 0)

    End Sub

    Private Sub DessinProfileMetal(MyGr As Graphics, MyProfil As cls_ProfilA, MyBrush As Brush, MyParAffloc As Struc_Affichage,
                                   zRef As Decimal, lRive As Boolean, xPos As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------
        '   01/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin du profilé métallique
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre supérieure de la mySection acier pour les sections standard, support de dalle pour les slim floor)
        '   lRive       [E] :   Profilé métallique en rive de dalle 
        '   xPos        [E] :   Position x de la mySection représentée
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)

        '--> En fonction du type de mySection

        Select Case MyProfil.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                DessinProfileLamine(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, xPos)

            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                DessinProfilePRS(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, xPos)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                DessinSlimSFB(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, xPos, lRive)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                DessinSlimIFBA(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, xPos, lRive)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                DessinSlimIFBB(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, xPos, lRive)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                DessinSlimSAB(MyGr, MyProfil, MyBrush, MyParAffloc, zRef, xPos, lRive)

        End Select
    End Sub

    Private Sub DessinSlimSAB(myGr As Graphics, myProfil As cls_ProfilA, myBrush As Brush, myParAffloc As Struc_Affichage,
                              zRef As Decimal, xPos As Decimal, lRive As Boolean)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/11/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin d'un profilé Slim floor SAB
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence
        '   xPos        [E] :   Position x de la mySection représentée
        '   lExtremite  [E] :   Poutre en extremité de dalle ?
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        '--( Dessin

        PrepareContourLamine(myProfil, xPts, yPts, nbPts, lRive)
        DecalePts(yPts, nbPts, zRef + myProfil.hb - myProfil.Tfi)
        If Math.Abs(xPos) > 0 Then
            DecalePts(xPts, nbPts, xPos)
        End If
        RemplirZone(myGr, myBrush, xPts, yPts, nbPts, myParAffloc, True)

    End Sub

    Private Sub DessinSlimIFBB(myGr As Graphics, myProfil As cls_ProfilA, myBrush As Brush, myParAffloc As Struc_Affichage,
                               zRef As Decimal, xPos As Decimal, lExtremite As Boolean)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/11/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin d'un profilé Slim floor IFB-B
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence 
        '   xPos        [E] :   Position x de la mySection représentée
        '   lExtremite  [E] :   Poutre en extremité de dalle ?
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        Dim xe, ye As Double
        Dim xo, yo As Double

        '--( Dessin

        PrepareContourIFB_B(myProfil, xPts, yPts, nbPts)
        DecalePts(yPts, nbPts, zRef + myProfil.ha - myProfil.Tfi)
        If Math.Abs(xPos) > 0 Then
            DecalePts(xPts, nbPts, xPos)
        End If
        RemplirZone(myGr, myBrush, xPts, yPts, nbPts, myParAffloc, True)

        'Dessin du plat soudé sup

        xo = xPos - myProfil.Plat_b / 2

        xe = xo + myProfil.Plat_b
        yo = zRef + myProfil.ha - myProfil.Tfi
        ye = yo - myProfil.Plat_t

        AddRectanglePlein(myGr, myBrush, MyPenContour, xo, yo, xe, ye, myParAffloc, True, True)

    End Sub

    Private Sub DessinSlimIFBA(myGr As Graphics, myProfil As cls_ProfilA, myBrush As Brush, myParAffloc As Struc_Affichage,
                               zRef As Decimal, xPos As Decimal, lExtremite As Boolean)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/11/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin d'un profilé Slim floor IFB-A
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre supérieure de la mySection acier)
        '   xPos        [E] :   Position x de la mySection représentée
        '   lExtremite  [E] :   Poutre en extremité de dalle ?
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        Dim xe, ye As Double
        Dim xo, yo As Double

        '--( Dessin

        PrepareContourIFB_A(myProfil, xPts, yPts, nbPts)
        DecalePts(yPts, nbPts, zRef + myProfil.ha - myProfil.Plat_t)
        If Math.Abs(xPos) > 0 Then
            DecalePts(xPts, nbPts, xPos)
        End If
        RemplirZone(myGr, myBrush, xPts, yPts, nbPts, myParAffloc, True)

        'Dessin du plat soudé inf

        If MyProjet.Poutres.Count = 0 Then
            xo = xPos - myProfil.Plat_b / 2
        Else
            If Not lExtremite Then
                xo = xPos - myProfil.Plat_b / 2
            Else
                xo = xPos - myProfil.Bfs / 2
            End If
        End If

        xe = xo + myProfil.Plat_b
        ye = zRef
        yo = zRef - myProfil.Plat_t

        AddRectanglePlein(myGr, myBrush, MyPenContour, xo, yo, xe, ye, myParAffloc, True, True)

    End Sub

    Private Sub DessinSlimSFB(myGr As Graphics, myProfil As cls_ProfilA, myBrush As Brush, myParAffloc As Struc_Affichage,
                              zRef As Decimal, xPos As Decimal, lExtremite As Boolean)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/11/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin d'un profilé Slim floor SFB
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre inférieure du profilé)
        '   xPos        [E] :   Position x de la mySection représentée
        '   lExtremite  [E] :   Poutre en extremité de dalle ?
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        Dim xe, ye As Double
        Dim xo, yo As Double

        '--( Dessin

        PrepareContourSFB(myProfil, xPts, yPts, nbPts)
        DecalePts(yPts, nbPts, zRef + myProfil.hb)
        If Math.Abs(xPos) > 0 Then
            DecalePts(xPts, nbPts, xPos)
        End If
        RemplirZone(myGr, myBrush, xPts, yPts, nbPts, myParAffloc, True)

        'Dessin du plat soudé inf

        If MyProjet.Poutres.Count = 0 Then
            xo = xPos - myProfil.Plat_b / 2
        Else
            If Not lExtremite Then
                xo = xPos - myProfil.Plat_b / 2
            Else
                xo = xPos - myProfil.Bfi / 2
            End If
        End If

        xe = xo + myProfil.Plat_b
        ye = zRef
        yo = zRef - myProfil.Plat_t

        AddRectanglePlein(myGr, myBrush, MyPenContour, xo, yo, xe, ye, myParAffloc, True, True)

    End Sub

    Private Sub DessinProfilePRS(MyGr As Graphics, MyProfil As cls_ProfilA, MyBrush As Brush, MyParAffloc As Struc_Affichage,
                                 zRef As Decimal, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/11/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin d'un profilé PRS
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre supérieure de la mySection acier)
        '   xPos        [E] :   Position x de la mySection représentée
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xe, ye As Double
        Dim xo, yo As Double

        '-< Semelle supérieure >-

        xe = xPos + MyProfil.Bfs / 2
        xo = xPos - MyProfil.Bfs / 2
        ye = -zRef
        yo = -MyProfil.Tfs - zRef

        AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

        '-< Semelle inférieure >-

        xe = xPos + MyProfil.Bfi / 2
        xo = xPos - MyProfil.Bfi / 2
        ye = -MyProfil.ha - zRef                  '   - Section.ha / 2
        yo = -MyProfil.ha + MyProfil.Tfi - zRef

        AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

        '-< Âme >-

        xe = xPos + MyProfil.Tw / 2
        xo = xPos - MyProfil.Tw / 2
        ye = -MyProfil.Tfs - zRef
        yo = -MyProfil.ha - zRef + MyProfil.Tfi

        AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

    End Sub


    Private Sub DessinProfileLamine(MyGr As Graphics, MyProfil As cls_ProfilA, MyBrush As Brush, MyParAffloc As Struc_Affichage,
                                    zRef As Decimal, Optional xPos As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   02/11/24    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Dessin du profilé laminé
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics d'affichage
        '   MyProfil    [E] :   Profilé métallique affiché
        '   MyBrush     [E] :   Pinceau utilisé pour le remplissage
        '   MyParrffloc [E] :   Paramètres d'affichage
        '   zRef        [E] :   z de reférence (0 pour la fibre supérieure de la mySection acier)
        '   xPos        [E] :   Position x de la mySection représentée
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer

        Dim xe, ye As Double
        Dim xo, yo As Double

        '--> Tracé du profilé laminé

        PrepareContourLamine(MyProfil, xPts, yPts, nbPts, False)
        DecalePts(yPts, nbPts, zRef)
        If Math.Abs(xPos) > 0 Then
            DecalePts(xPts, nbPts, xPos)
        End If
        RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts, MyParAffloc, True)

        '--> Tracé du plat, le cas échéant

        If MyProfil.lPlatRenfort Then

            xo = xPos - MyProfil.Plat_b / 2

            xe = xo + MyProfil.Plat_b
            ye = zRef - MyProfil.hb
            yo = zRef - MyProfil.hb - MyProfil.Plat_t

            AddRectanglePlein(MyGr, MyBrush, MyPenContour, xo, yo, xe, ye, MyParAffloc, True, True)

        End If

    End Sub

    Private Sub PrepareContourLamine(myProfil As cls_ProfilA, ByRef xPts() As Single, ByRef yPts() As Single,
                                     ByRef nbPts As Integer, lRepresentationPoutreExtremite As Boolean)
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

        Dim lDessinPoutreIntermediaire As Boolean

        If MyProjet.Poutres.Count = 0 Then
            lDessinPoutreIntermediaire = True
        Else
            If myProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB And lRepresentationPoutreExtremite Then
                lDessinPoutreIntermediaire = False
            Else
                lDessinPoutreIntermediaire = True
            End If
        End If

        '--> Contour

        With myProfil

            If lDessinPoutreIntermediaire Then

                xPts(0) = .Tw / 2
                yPts(0) = - .ha / 2

                Dim DeltaA As Double = Math.PI / 10

                For i = 1 To 6
                    xPts(i) = .Tw / 2 + .Rcs * (1 + CSng(Math.Cos(Math.PI - (i - 1) * DeltaA)))
                    yPts(i) = - .Tfs - .Rcs + .Rcs * CSng(Math.Sin(Math.PI - (i - 1) * DeltaA))
                Next

                xPts(7) = .Bfs / 2
                yPts(7) = - .Tfs

                xPts(8) = .Bfs / 2
                yPts(8) = 0

                For i = 9 To 17
                    xPts(i) = -xPts(17 - i)
                    yPts(i) = yPts(17 - i)
                Next

                For i = 18 To 23
                    xPts(i) = - .Tw / 2 - .Rci * (1 + CSng(Math.Cos(Math.PI - (i - 18) * DeltaA)))
                    yPts(i) = - .ha + .Tfi + .Rci - .Rci * CSng(Math.Sin(Math.PI - (i - 18) * DeltaA))
                Next

                xPts(24) = - .Bfi / 2
                yPts(24) = - .ha + .Tfi

                xPts(25) = - .Bfi / 2
                yPts(25) = - .ha

                For i = 26 To 35
                    xPts(i) = -xPts(51 - i)
                    yPts(i) = yPts(51 - i)
                Next

            Else

                xPts(0) = .Tw / 2
                yPts(0) = - .ha / 2

                Dim DeltaA As Double = Math.PI / 10

                For i = 1 To 6
                    xPts(i) = .Tw / 2 + .Rcs * (1 + CSng(Math.Cos(Math.PI - (i - 1) * DeltaA)))
                    yPts(i) = - .Tfs - .Rcs + .Rcs * CSng(Math.Sin(Math.PI - (i - 1) * DeltaA))
                Next

                xPts(7) = .Bfs - .Bfi / 2
                yPts(7) = - .Tfs

                xPts(8) = .Bfs - .Bfi / 2
                yPts(8) = 0

                xPts(9) = xPts(8) - .Bfs
                yPts(9) = 0

                xPts(10) = xPts(9)
                yPts(10) = - .Tfs

                For i = 11 To 17
                    xPts(i) = -xPts(17 - i)
                    yPts(i) = yPts(17 - i)
                Next

                For i = 18 To 23
                    xPts(i) = - .Tw / 2 - .Rci * (1 + CSng(Math.Cos(Math.PI - (i - 18) * DeltaA)))
                    yPts(i) = - .ha + .Tfi + .Rci - .Rci * CSng(Math.Sin(Math.PI - (i - 18) * DeltaA))
                Next

                xPts(24) = - .Bfi / 2
                yPts(24) = - .ha + .Tfi

                xPts(25) = - .Bfi / 2
                yPts(25) = - .ha

                For i = 26 To 35
                    xPts(i) = -xPts(51 - i)
                    yPts(i) = yPts(51 - i)
                Next

            End If

        End With

    End Sub

    Private Sub PrepareContourSFB(myProfil As cls_ProfilA, ByRef xPts() As Single,
                                  ByRef yPts() As Single, ByRef nbPts As Integer)
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

            xPts(0) = .Tw / 2
            yPts(0) = - .hb / 2

            Dim DeltaA As Double = Math.PI / 10

            For i = 1 To 6
                xPts(i) = .Tw / 2 + .Rcs * (1 + CSng(Math.Cos(Math.PI - (i - 1) * DeltaA)))
                yPts(i) = - .Tfs - .Rcs + .Rcs * CSng(Math.Sin(Math.PI - (i - 1) * DeltaA))
            Next

            xPts(7) = .Bfs / 2
            yPts(7) = - .Tfs

            xPts(8) = .Bfs / 2
            yPts(8) = 0

            For i = 9 To 17
                xPts(i) = -xPts(17 - i)
                yPts(i) = yPts(17 - i)
            Next

            For i = 18 To 23
                xPts(i) = - .Tw / 2 - .Rci * (1 + CSng(Math.Cos(Math.PI - (i - 18) * DeltaA)))
                yPts(i) = - .hb + .Tfi + .Rci - .Rci * CSng(Math.Sin(Math.PI - (i - 18) * DeltaA))
            Next

            xPts(24) = - .Bfi / 2
            yPts(24) = - .hb + .Tfi

            xPts(25) = - .Bfi / 2
            yPts(25) = - .hb

            For i = 26 To 35
                xPts(i) = -xPts(51 - i)
                yPts(i) = yPts(51 - i)
            Next

        End With

    End Sub

    Private Sub PrepareContourIFB_A(myProfil As cls_ProfilA, ByRef xPts() As Single, ByRef yPts() As Single,
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

        ReDim xPts(17)
        ReDim yPts(17)
        nbPts = 18


        '--> Contour

        With myProfil

            xPts(0) = .Tw / 2
            yPts(0) = - .ha + .Plat_t

            Dim DeltaA As Double = Math.PI / 10

            For i = 1 To 6
                xPts(i) = .Tw / 2 + .Rcs * (1 + CSng(Math.Cos(Math.PI - (i - 1) * DeltaA)))
                yPts(i) = - .Tfs - .Rcs + .Rcs * CSng(Math.Sin(Math.PI - (i - 1) * DeltaA))
            Next

            xPts(7) = .Bfs / 2
            yPts(7) = - .Tfs

            xPts(8) = .Bfs / 2
            yPts(8) = 0

            For i = 9 To 17
                xPts(i) = -xPts(17 - i)
                yPts(i) = yPts(17 - i)
            Next

        End With

    End Sub

    Private Sub PrepareContourIFB_B(myProfil As cls_ProfilA, ByRef xPts() As Single, ByRef yPts() As Single,
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

        ReDim xPts(17)
        ReDim yPts(17)
        nbPts = 18


        '--> Contour

        With myProfil

            xPts(0) = .Tw / 2
            yPts(0) = - .Plat_t

            Dim DeltaA As Double = Math.PI / 10

            For i = 1 To 6
                xPts(i) = .Tw / 2 + .Rci * (1 + CSng(Math.Cos(Math.PI - (i - 1) * DeltaA)))
                yPts(i) = - .ha + .Tfi + .Rci - .Rci * CSng(Math.Sin(Math.PI - (i - 1) * DeltaA))
            Next

            xPts(7) = .Bfi / 2
            yPts(7) = - .ha + .Tfi

            xPts(8) = .Bfi / 2
            yPts(8) = - .ha

            For i = 9 To 17
                xPts(i) = -xPts(17 - i)
                yPts(i) = yPts(17 - i)
            Next

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

        xe = xPos + profile.Bfi / 2 * MyRatioBc
        xo = xPos - profile.Bfi / 2 * MyRatioBc
        ye = -profile.ha + profile.Tfi
        yo = -profile.Tfs

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

        DessinEnrobagePartielBeton(MyGr, MySection.ProfilA, MySection.Enrobage.Ratio_bc, MyParAff, MyBrushBp, xPos)

    End Sub

    Private Sub DessinArmaLongiEnrobage(ByRef MyGr As Graphics, ByVal MySection As cls_Section,
                                        MyParAffloc As Struc_Affichage, MyBrushA As Brush, iArma As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   mySection     [E] :   Section à représenter
        '   zRef        [E] :   Position de référence pour l'axe z (z0), comptée à partir fibre sup du profilé
        '   MyParAffloc [E] :   Paramètres d'affichage   
        '   MyBrushA    [E] :   Pinceau pour le remplissage de la dalle
        '   iArma       [E] :   indice du lit d'armature
        '---------------------------------------------------------------------------------------------------------------------------

        DessinArmaLongiEnrobage(MyGr, MySection.ProfilA, MySection.Enrobage, MyParAffloc, MyBrushA, iArma)

    End Sub


    Private Sub DessinArmaLongiEnrobageN(ByRef MyGr As Graphics, ByVal MySection As cls_Section, MyEnrob As cls_Enrobage_Partiel,
                                         MyParAffloc As Struc_Affichage, MyBrushA() As Brush, iArma As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   mySection     [E] :   Section à représenter
        '   zRef        [E] :   Position de référence pour l'axe z (z0), comptée à partir fibre sup du profilé
        '   MyParAffloc [E] :   Paramètres d'affichage   
        '   MyBrushA    [E] :   Pinceau pour le remplissage des armatures actives
        '   iArma       [E] :   indice du lit d'armature
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPenContour As New Pen(Color.Black, 1)
        Dim xo, yo As Decimal
        Dim Signe As Decimal = 1
        Dim uYInterne As Decimal
        Dim Bfs, Tw As Decimal
        Dim PhiA As Decimal
        Dim Uy, Uz, EtriersPhi As Decimal
        Dim kPos() As Decimal = {0, -0.5, 0.5, 0}
        Dim kPosE() As Decimal = {0, 0, 1, 0.5}
        Dim lActif As Boolean
        Dim MyBrush As Brush
        Dim MyBrushNonActif As New SolidBrush(Color.LightGray)

        '--> Initialisation

        Select Case MyEnrob.Etriers_Type
            Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre : uYInterne = MyEnrob.Etriers_EnrobageYinterne
            Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant : uYInterne = 0
            Case cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude : uYInterne = 0
        End Select

        Uy = MyEnrob.Etriers_EnrobageY
        Uz = MyEnrob.Etriers_EnrobageZ
        EtriersPhi = MyEnrob.Etriers_Phi
        Bfs = MySection.ProfilA.Bfs
        Tw = MySection.ProfilA.Tw

        '--> Armatures extérieures

        PhiA = MyEnrob.LitArma(iArma).PhiExt

        lActif = (iArma = 1) Or (MyEnrob.LitArma(iArma).NbExt > 1) Or (MyEnrob.LitArma(iArma).lActiveExt)
        If lActif Then MyBrush = MyBrushA(0) Else MyBrush = MyBrushNonActif

        For jChambre As Integer = 0 To 1

            For i As Integer = 1 To MyEnrob.LitArma(iArma).NbExt

                xo = Signe * (Bfs * MyEnrob.Ratio_bc / 2 - Uy - EtriersPhi - PhiA / 2)
                yo = MySection.zPosArmaEnrobage(iArma, 0, i)

                AddCerclePlein(MyGr, MyBrush, xo - Signe * kPosE(i) * PhiA, yo, PhiA, MyParAffloc, True)

            Next

            Signe = -Signe

        Next

        '--> Armatures intérieures

        PhiA = MyEnrob.LitArma(iArma).PhiInt
        lActif = (iArma = 1) Or (MyEnrob.LitArma(iArma).NbInt > 1) Or (MyEnrob.LitArma(iArma).lActiveInt)
        If lActif Then MyBrush = MyBrushA(2) Else MyBrush = MyBrushNonActif

        If MyEnrob.LitArma(iArma).NbInt = 1 Then kPos(1) = 0 Else kPos(1) = -0.5

        For jChambre As Integer = 0 To 1

            For i As Integer = 1 To MyEnrob.LitArma(iArma).NbInt

                yo = MySection.zPosArmaEnrobage(iArma, 2, i)
                xo = Signe * (Tw / 2 + uYInterne + EtriersPhi + PhiA / 2)

                AddCerclePlein(MyGr, MyBrush, xo + Signe * kPosE(i) * PhiA, yo, PhiA, MyParAffloc, True)

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

    Private Sub DessinArmaLongiEnrobage(ByRef MyGr As Graphics, ByVal MyProfil As cls_ProfilA, MyEnrob As cls_Enrobage_Partiel,
                                        MyParAffloc As Struc_Affichage, MyBrushA As Brush, iArma As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   18/04/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'une dalle mixte
        '---------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   mySection     [E] :   Section à représenter
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
                ' yo = -MyProfil.ha + MyProfil.Tfi + MyEnrob.Etriers_EnrobageZ + MyEnrob.Etriers_Phi + MyEnrob.LitsArmaOLD(iArma).Phi / 2
                yo = -MyProfil.ha + MyProfil.Tfi + MyEnrob.Etriers_EnrobageZ + MyEnrob.Etriers_Phi + MyEnrob.LitArma(iArma).PhiExt / 2
            Case 1
                yo = -MyProfil.ha / 2
            Case 2
                'yo = -MyProfil.Tfs - MyEnrob.Etriers_EnrobageZ - MyEnrob.Etriers_Phi - MyEnrob.LitsArmaOLD(iArma).Phi / 2
                yo = -MyProfil.Tfs - MyEnrob.Etriers_EnrobageZ - MyEnrob.Etriers_Phi - MyEnrob.LitArma(iArma).PhiExt / 2
        End Select
        Select Case MyEnrob.Etriers_Type
            Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre : uYInterne = MyEnrob.Etriers_EnrobageY
            Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant : uYInterne = 0
            Case cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude : uYInterne = 0
        End Select
        DeltaY(1) = 0
        If (MyEnrob.Etriers_Type = cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant) Then
            DeltaY(0) = MyEnrob.Etriers_Phi / 5
        Else
            DeltaY(0) = 0
        End If

        '--> Boucle sur les armatures du lit

        For jChambre As Integer = 0 To 1

            'xo = Signe * (MyProfil.Bfs * MyEnrob.Ratio_bc / 2 - MyEnrob.Etriers_EnrobageY - MyEnrob.Etriers_Phi - MyEnrob.LitsArmaOLD(iArma).Phi / 2)
            'xe = Signe * (MyProfil.Tw / 2 + uYInterne + MyEnrob.Etriers_Phi + MyEnrob.LitsArmaOLD(iArma).Phi / 2)
            xo = Signe * (MyProfil.Bfs * MyEnrob.Ratio_bc / 2 - MyEnrob.Etriers_EnrobageY - MyEnrob.Etriers_Phi - MyEnrob.LitArma(iArma).PhiExt / 2)
            xe = Signe * (MyProfil.Tw / 2 + uYInterne + MyEnrob.Etriers_Phi + MyEnrob.LitArma(iArma).PhiExt / 2)

            'If MyEnrob.LitsArmaOLD(iArma).nbArma = 1 Then
            Delta = 1
            'Else
            '    Delta = (xe - xo) / (MyEnrob.LitsArmaOLD(iArma).nbArma - 1)
            'End If
            For i As Integer = 1 To 1 ' MyEnrob.LitsArmaOLD(iArma).nbArma

                xi = xo + Delta * (i - 1)

                'AddCerclePlein(MyGr, MyBrushA, xi, yo + DeltaY(jChambre), MyEnrob.LitsArmaOLD(iArma).Phi, MyParAffloc, True)
                AddCerclePlein(MyGr, MyBrushA, xi, yo + DeltaY(jChambre), MyEnrob.LitArma(iArma).PhiExt, MyParAffloc, True)

            Next

            Signe = -Signe

        Next

    End Sub

#End Region

#Region " Outils généraux pour les diagrammes "


    Public Sub DessineDiagrammeRDM(myGr As Graphics, myPoutre As cls_Poutre,
                                    Courbe(,) As Decimal, kEchC As Decimal, CouleurC As Color, myParAff As Struc_Affichage,
                               Optional ByVal lAffValEnv As Boolean = False, Optional ByVal iNodeMin As Integer = 0, Optional ByVal iNodeMax As Integer = 0, Optional ByVal ChaineMin As String = "", Optional ByVal ChaineMax As String = "", Optional ByVal ValMin As Decimal = 0, Optional ByVal ValMax As Decimal = 0, Optional ByVal dCar As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   18/09/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation d'un diagramme moment ou effort tranchant
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   MyChargeA   [E] :   Cas de charge
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xo, xe, yo, ye As Decimal
        Dim myPenC As New Pen(CouleurC, 1.5)
        Dim MyPenB As New SolidBrush(Color.Gray)
        Dim MyFontNum As New Font("Arial", 7)

        '--> AffichageOptFeu

        xo = 0
        xe = 0
        yo = 0
        ye = Courbe(0, 1) * kEchC
        If Not IsEqual(yo, ye) Then
            AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)
        End If

        For iNode As Integer = 0 To myPoutre.Nodes.nbNodes - 2
            xo = myPoutre.Nodes.xGlobal(iNode)
            xe = myPoutre.Nodes.xGlobal(iNode + 1)
            yo = Courbe(iNode, 1) * kEchC
            ye = Courbe(iNode + 1, 0) * kEchC
            AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)

            If iNode < myPoutre.Nodes.nbNodes - 2 Then
                yo = Courbe(iNode + 1, 1) * kEchC
                If Not IsEqual(yo, ye) Then
                    AddLigne(myGr, myPenC, xe, yo, xe, ye, myParAff)
                End If
            End If

            If lAffValEnv Then
                If iNodeMin = iNode Then
                    If IsEqual(yo, ValMin) Then
                        AddTexte(myGr, MyPenB, ChaineMin, MyFontNum, xo, yo - dCar, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
                    Else
                        AddTexte(myGr, MyPenB, ChaineMin, MyFontNum, xo, ye - dCar, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
                    End If

                End If

                If iNodeMax = iNode Then
                    If IsEqual(yo, ValMin) Then
                        AddTexte(myGr, MyPenB, ChaineMax, MyFontNum, xo, yo - dCar, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
                    Else
                        AddTexte(myGr, MyPenB, ChaineMax, MyFontNum, xo, ye - dCar, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
                    End If
                End If
            End If
        Next

        xe = myPoutre.LongueurTotale
        yo = 0
        ye = Courbe(myPoutre.Nodes.nbNodes - 1, 0) * kEchC

        If Not IsEqual(yo, ye) Then
            AddLigne(myGr, myPenC, xe, yo, xe, ye, myParAff)
        End If

        If lAffValEnv Then
            If iNodeMin = myPoutre.Nodes.nbNodes - 1 Then
                AddTexte(myGr, MyPenB, ChaineMin, MyFontNum, xo, ye - dCar, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If

            If iNodeMax = myPoutre.Nodes.nbNodes - 1 Then
                AddTexte(myGr, MyPenB, ChaineMax, MyFontNum, xo, ye - dCar, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If
        End If

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

    Private Sub GenereDimensionsEnveloppes(MyPoutre As cls_Poutre, lDalleReduite As Boolean,
                                           ByRef xMin As Decimal, ByRef xMax As Decimal, ByRef yMin As Decimal, ByRef yMax As Decimal,
                                           ByRef dCar As Decimal, ByRef BeffRed As Decimal, Optional zRef As Decimal = 0)
        '---------------------------------------------------------------------------------------------------------------------------
        '   03/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Génère les dimensions enveloppes d'une mySection à représenter
        '---------------------------------------------------------------------------------------------------------------------------
        '   MySection       [E] :   Section à afficher
        '   lMixte          [E] :   Indique si on affiche la dalle, le cas échéant
        '   lDalleReduite   [E] :   Indique si on représente une largeur de dalle réduite ou la largeur complète (dans ce dernier, cela écrase le dessin à l'écran)
        '   xMin, xMax      [S] :   Valeurs x min et max de la mySection
        '   yMin, yMax      [S] :   Valeurs y min et max de la mySection
        '   BeffRed         [S] :   Largeur réduite de la dalle pour représentation à l'écran
        '   dCar            [S] :   Dimension caractéristique utilisée pour les décalages
        '   BeffRed         [S] :   Largeur de dalle utilisée pour l'affichage
        '   zRef            [E] :   Position de référence / fibre supérieure du profilé métallique
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Initialisation

        Dim Diagonale As Decimal = Math.Sqrt((MyPoutre.Section.ProfilA.Bfi + MyPoutre.Section.ProfilA.Bfs) ^ 2 / 4 + MyPoutre.Section.ProfilA.ha ^ 2)
        Dim BfMax As Decimal = Math.Max(MyPoutre.Section.ProfilA.Bfs, MyPoutre.Section.ProfilA.Bfi)

        '--> Traitement

        Select Case MyPoutre.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage,
                 cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                yMin = -zRef - MyPoutre.Section.ProfilA.ha
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = -zRef

                If MyPoutre.Section.lMixte Then
                    If lDalleReduite Then BeffRed = Math.Min(MyPoutre.LargeurDalleDispo, 1.5 * Diagonale) Else BeffRed = MyPoutre.LargeurDalleDispo
                    yMax += MyPoutre.Dalle.Ep_td + MyPoutre.Dalle.EpRenformis
                    xMin = -Math.Max(BeffRed / 2, BfMax)
                    xMax = -xMin
                    dCar = Math.Max(Math.Sqrt(((MyPoutre.Section.ProfilA.ha + MyPoutre.Dalle.zTop) ^ 2 + (MyPoutre.Section.ProfilA.Bfs + MyPoutre.Section.ProfilA.Bfi) ^ 2)), BeffRed) / 20
                Else
                    dCar = Math.Sqrt((MyPoutre.Section.ProfilA.ha ^ 2 + (MyPoutre.Section.ProfilA.Bfs + MyPoutre.Section.ProfilA.Bfi) ^ 2)) / 20
                End If

            Case cls_Section.Enum_TypeSection.SFB
                yMin = -MyPoutre.Section.ProfilA.Plat_t
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = MyPoutre.Section.ProfilA.ha
            Case cls_Section.Enum_TypeSection.SFBmixte
                yMin = -MyPoutre.Section.ProfilA.Plat_t
                xMin = -MyPoutre.LargeurDalleDispo / 2
                xMax = -xMin
                yMax = MyPoutre.Section.ProfilA.ha + MyPoutre.Dalle.Ep_td

            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_B
                yMin = -MyPoutre.Section.ProfilA.Plat_t
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = MyPoutre.Section.ProfilA.ha

            Case cls_Section.Enum_TypeSection.IFB_Amixte, cls_Section.Enum_TypeSection.IFB_Bmixte
                yMin = -MyPoutre.Section.ProfilA.Plat_t
                xMin = -MyPoutre.LargeurDalleDispo / 2
                xMax = -xMin
                yMax = MyPoutre.Section.ProfilA.ha + MyPoutre.Dalle.Ep_td

            Case cls_Section.Enum_TypeSection.SAB
                yMin = 0
                xMin = -BfMax / 2
                xMax = -xMin
                yMax = MyPoutre.Section.ProfilA.ha

            Case cls_Section.Enum_TypeSection.SABmixte
                yMin = 0
                xMin = -MyPoutre.LargeurDalleDispo / 2
                xMax = -xMin
                yMax = MyPoutre.Section.ProfilA.ha + MyPoutre.Dalle.Ep_td

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

#Region " Affichage d'expressions "

    Public Sub DrawExpression(ByVal MyGr As Graphics, ByVal MyBrush As Brush,
                              ByVal Expression As String, ByVal pWI As Single, ByVal pHI As Single,
                              ByVal FontNormal As Font, Alignement As Enu_AlignementH)
        '----------------------------------------------------------------------------------------
        '   03/11/23 :  Création - LeT (créé pour le logiciel TORSION)
        '----------------------------------------------------------------------------------------
        '   AffichageOptFeu d'une expréssion complète contenant plusieurs indices
        '   Les indices sont encadrés par des "\-"
        '   Les fractions sont encadrés par des "\f"
        '   Le numérateur et le dénominateur des fractions sont séparés par "\d"
        '----------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   MyBrush     [E] :   Pinceau pour écrire
        '   Expression  [E] :   Expression à dessiner
        '   xPen, yPen  [E] :   Position du stylo pour écrire
        '   Alignement  [E] :   Alignement de l'expression
        '   FontNormal  [E] :   Police de caractère normale
        '   FontIndice  [E] :   Police de caractère pour les indices
        '   DrawEgal    [E] :   Dessin du signe = à la fin
        '----------------------------------------------------------------------------------------


        '--> Déclarations

        Dim Keys() As String = {"g", "G", "N", "n", "I", "i", "+", "-", "=", "S", "s"}
        Dim kAdjust As Single = 0.75

        Dim xPos As Single
        Dim yPos As Single
        Dim expressionW As Single
        Dim expressionH As Single = MyGr.MeasureString("X", FontNormal).Height
        Dim expressionClean As String = Expression
        Dim lSymbol As Boolean, lIndice As Boolean, lGras As Boolean, lItalic As Boolean

        '--> Initialisation

        lSymbol = False
        lIndice = False
        lGras = False
        lItalic = False

        For i As Integer = 0 To Keys.GetUpperBound(0)
            expressionClean = Replace(expressionClean, "\" & Keys(i), "")
        Next

        expressionW = LongueurExpression(MyGr, expressionClean, FontNormal)

        Select Case Alignement
            Case Enu_AlignementH.Gauche
                'xPos = xPen - expressionWidth
                xPos = pWI * (1 - kAdjust) / 2
            Case Enu_AlignementH.Droite
                xPos = pWI * (kAdjust + 1) / 2 - expressionW
            Case Enu_AlignementH.Centre
                xPos = pWI / 2 - expressionW / 2
        End Select

        yPos = pHI / 2 - expressionH / 2


        DrawLigneExpression(MyGr, xPos, yPos, Expression, MyBrush, FontNormal, lSymbol, lIndice, lGras, lItalic)


    End Sub

    Private Sub GetMyPolice(FontNormalName As String, TaillePolice As Single, lSymbol As Boolean, lIndice As Boolean, lGras As Boolean, lItalic As Boolean,
                            ByRef PoliceEnCours As Font, ByRef YDecal As Single)
        '----------------------------------------------------------------------------------
        '   04/12/23 :  Creation - POM - Version 1.00
        '----------------------------------------------------------------------------------
        '   Reglage de la police pour l'affichage d'une expression
        '----------------------------------------------------------------------------------

        '--> Déclarations

        Dim StyleEnCours As FontStyle
        Dim NomPolice As String = ""
        Dim DeltaT As Single
        'Const TaillePolice As Single = 8

        '--> Réglages

        StyleEnCours = FontStyle.Regular
        If lGras Then StyleEnCours = FontStyle.Bold
        If lItalic Then StyleEnCours = StyleEnCours Or FontStyle.Italic
        If lSymbol Then NomPolice = "Symbol" Else NomPolice = FontNormalName

        If lIndice Then
            DeltaT = 1
            YDecal = +5 * TaillePolice / 10
        Else
            DeltaT = 0
            YDecal = 0
        End If

        PoliceEnCours = New Font(NomPolice, (TaillePolice - DeltaT), StyleEnCours)

    End Sub

    Private Sub DrawLigneExpression(ByRef MyGr As Graphics, xPen As Single, yPen As Single, ByRef Expression As String,
                                    ByRef MyBrush As System.Drawing.Brush, MyFont As Font,
                                    ByRef lSymbol As Boolean, ByRef lIndice As Boolean, ByRef lGras As Boolean, ByRef lItalic As Boolean)
        '----------------------------------------------------------------------------------
        '   04/12/23 :  Creation - POM - Version 1.00
        '----------------------------------------------------------------------------------
        '   AffichageOptFeu d'une expression
        '----------------------------------------------------------------------------------
        '   Expression  [E] :   Ligne à afficher
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   MyBrush     [E] :   Pinceau avec lequel on affiche
        '   
        '----------------------------------------------------------------------------------

        '--> Déclarations

        Const AntiSlash As Char = "\"c

        Dim Indice As Integer
        Dim Mot, Cle As String
        Dim Length As Integer = Expression.Length
        'Dim MyText As String

        ' HLIGNE = 0
        Dim Police As Font = Nothing
        Dim YDecal As Single

        '--[ Recherche Mot Clé (repéré par antislash)

        Indice = Expression.IndexOf(AntiSlash)

        If Indice = -1 Then
            '--[ Pas de mot clé : on écrit directement toute la ligne
            GetMyPolice(MyFont.Name, MyFont.Size, lSymbol, lIndice, lGras, lItalic, Police, YDecal)
            MyGr.DrawString(Expression, Police, MyBrush, xPen, yPen + YDecal)

        Else
            '--[ Au moins un mot clé dans la ligne
            '--> Si le mot clé n'est pas au début, on écrit ce qu'il y a avant
            If Indice > 0 Then
                GetMyPolice(MyFont.Name, MyFont.Size, lSymbol, lIndice, lGras, lItalic, Police, YDecal)
                Mot = Expression.Substring(0, Indice)
                MyGr.DrawString(Mot, Police, MyBrush, xPen, yPen + YDecal)

                xPen += MyGr.MeasureString(Mot, Police, New PointF(0, 0), StringFormat.GenericTypographic).Width
            End If

            '--> Traitement du mot clé

            Cle = Expression.Substring(Indice + 1, Math.Min(1, Length - Indice - 1))
            Dim lSuite As Boolean   'Indicateur pour savoir si il faut traiter la suite comme une ligne
            Dim iMotCle As Integer
            lSuite = (Length - Indice - 1) > 0
            iMotCle = 1

            Select Case Cle


                Case "G" : lGras = True      'Mise en gras
                Case "g" : lGras = False     'Fin du gras
                Case "I" : lItalic = True    'Mise en italique
                Case "i" : lItalic = False   'Fin Italique
                Case "S" : lSymbol = True    'Police Symbol
                Case "s" : lSymbol = False   'Police Normale
                Case "=" : lIndice = False
                'Case "+" : Me.StatutPolice = Enu_StatutPolice.Exposant
                Case "-" : lIndice = True
                Case "N", "n" : lSymbol = False : lGras = False : lItalic = False

            End Select

            If lSuite Then
                DrawLigneExpression(MyGr, xPen, yPen, Expression.Substring(Indice + 1 + iMotCle), MyBrush, MyFont, lSymbol, lIndice, lGras, lItalic)
            End If
        End If

    End Sub



    Private Function LongueurExpression(ByVal MyGr As Graphics, ByVal Expression As String,
                                        ByVal FontNormal As Font, Optional ByVal DrawEgal As Boolean = False) As Single
        '----------------------------------------------------------------------------------------
        '   20/09/23 :  Création - LeT (créé pour le logiciel TORSION)
        '----------------------------------------------------------------------------------------
        '   Fonction qui retourne la longueur d'une expression
        '----------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   Expression  [E] :   Expression à dessiner
        '   FontNormal  [E] :   Police de caractère normale
        '   DrawEgal    [E] :   Dessin du signe = à la fin de l'expression
        '----------------------------------------------------------------------------------------

        If DrawEgal Then
            Return MyGr.MeasureString(Expression, FontNormal).Width + MyGr.MeasureString("= ", FontNormal).Width
        Else
            Return MyGr.MeasureString(Expression, FontNormal).Width
        End If

    End Function

#End Region

#Region " Dessins des courbes HIVOSS "

    '--> Paramètres du dessin
    Private Colors As Color() = {Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.DarkBlue, Color.White}
    Private Coeff As Decimal = 3.5          'Coeff multiplicateur sur les ordonnées (Hz) 
    Private nbFrontières As Integer = 6     'Nombre de lignes frontière


    Public Const MASSMODMIN As Decimal = 100
    Public Const MASSMODMAX As Decimal = 100000

    ''' <summary>
    ''' Représentation de l'abaque Hivoss dans la NdC
    ''' </summary>
    ''' <param name="MyBeam">[E] Poutre traitée</param>
    ''' <param name="MyGr">[E] Graphics</param>
    ''' <param name="xLeft">[E] Position gauche du dessin</param>
    ''' <param name="yTop">[E] Position top du dessin</param>
    ''' <param name="Width">[E] Largeur du dessin</param>
    ''' <param name="Height">[E] Hauteur du dessin</param>
    ''' <param name="MyDamp">[E] Amortissement</param>
    ''' <param name="MyFreq">[E] Frequence propre</param>
    ''' <param name="MyMass">{E] Masse modale</param>
    ''' <remarks></remarks>
    Public Sub DessinCourbeHivoss(MyHivoss As cls_MethodHivoss, ByVal MyGr As Graphics,
                                  ByVal xLeft As Single, ByVal yTop As Single,
                                  ByVal Width As Single, ByVal Height As Single,
                                  ByVal MyDamp As Integer, ByVal MyFreq As Decimal, ByVal MyMass As Decimal, ByVal strDamp As String)
        '--------------------------------------------------------------------------------------------------
        '   04/12/23 :  Création - Version 1.00 - POM (repris de ACB+)
        '--------------------------------------------------------------------------------------------------
        '   Représentation d'un abaque Hivoss dans la NdC
        '--------------------------------------------------------------------------------------------------
        '   MyBeam          [E] :   Poutre représentée dans la Note de Calcul
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xLeft, yTop     [E] :   Position haut gauche du dessin
        '   Width, Height   [E] :   Largeur et hauteur du dessin
        '
        '   MyDamp          [E] :   Amortissement
        '   MyFreq          [E] :   Fréquence propre
        '   MyMass          [E] :   Masse modale de la poutre
        '--------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim i, j As Integer

        Dim MyFont As Font
        Dim AllFloorVibration As New Dictionary(Of Integer, cls_MethodHivoss.strHivossTable)

        Dim MyParAff As Struc_Affichage
        Dim yMin, yMax, xMin, xMax As Double
        Dim kECH As Single = 1
        Const kDec As Double = 1.25

        Dim nbPoints As Integer    'Nombre de points par ligne frontière
        Dim DonneeY() As Decimal                            'Ensemble des points y (Hz)
        Dim DonneeX As New List(Of Decimal())
        Dim MyPen As New Pen(Color.Black)
        Dim MyPenbrush As New SolidBrush(Color.Black)
        Dim MyBrush As SolidBrush
        Dim dCar, Decal As Double
        Dim Balise() As Decimal = {100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, 100000}
        Dim xPts(), yPts() As Single
        Dim nbPts As Integer
        Dim LimitesIntervalles As Decimal() = Nothing   '--> Constantes des frontières
        Dim LettresIntervalles As Char() = Nothing      '--> Catégories

        '--> Intialisation

        MyFont = New Font("Arial", 8.25, FontStyle.Regular)
        MyHivoss.ChargerValeursHivoss(AllFloorVibration)
        LimitesIntervalles = MyHivoss.LimitesIntervalles
        LettresIntervalles = MyHivoss.LettresIntervalles

        nbPoints = AllFloorVibration(MyDamp).nbLigne
        ReDim DonneeY(nbPoints - 1)

        dCar = (((Math.Log10(100000) - Math.Log10(100)) ^ 2 + (Math.Log10(20) - Math.Log10(1)) ^ 2) ^ 0.5) / 10
        Decal = dCar / 2

        '--> Récupération des données

        MyHivoss.RecupereDonnees(MyDamp, nbPoints, DonneeX, DonneeY)
        TraitementDonnees(nbPoints, DonneeX, DonneeY)

        '--> Paramètres d'affichage 

        xMin = Math.Log10(100)
        xMax = Math.Log10(100000)
        yMin = Math.Log10(1) * Coeff - Decal * (kDec + 0.5)
        yMax = Math.Log10(20) * Coeff

        ParametresAffichage(MyParAff, xMin, yMin, (xMax - xMin) / kECH, (yMax - yMin) / kECH, Width, Height, xLeft, yTop)

        '==> Dessin Frontières <=======================================================================================

        '--> Zone A (blanc)

        MyBrush = New SolidBrush(Colors(6))

        nbPts = 4
        xPts = New Single() {100, 100, 100000, 100000}
        yPts = New Single() {1, 20, 20, 1}

        PreparePoints(nbPts, xPts, yPts)

        RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts, MyParAff, True)

        '--> Zona B à !
        For i = nbFrontières - 1 To 0 Step -1   '--> boucle sur les frontières

            MyBrush = New SolidBrush(Colors(i))

            nbPts = nbPoints + 2
            ReDim xPts(nbPts - 1)
            ReDim yPts(nbPts - 1)

            xPts(0) = 100 : yPts(0) = 20
            xPts(1) = 100 : yPts(1) = 1
            For j = 0 To nbPoints - 1
                xPts(j + 2) = DonneeX.Item(j)(i)
                yPts(j + 2) = DonneeY(j)
            Next
            PreparePoints(nbPts, xPts, yPts)

            RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts, MyParAff, True)

        Next

        '===> Dessin Quadrillage <=====================================================================================

        '==> Traits horizontaux Fréquences propres (1 à 20 Hz)

        For i = 1 To 20 Step 1
            AddLigne(MyGr, MyPen, Math.Log10(100), Math.Log10(i) * Coeff, Math.Log10(100000), Math.Log10(i) * Coeff, MyParAff)
            AddTexte(MyGr, MyPenbrush, i.ToString, MyFont, Math.Log10(100) - Decal / 2.5, Math.Log10(i) * Coeff, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
        Next

        '==> Traits verticaux Masses modales

        '--> 200 à 1000
        For i = 200 To 1000 Step 100

            AddLigne(MyGr, MyPen, Math.Log10(i), Math.Log10(1) * Coeff, Math.Log10(i), Math.Log10(20) * Coeff, MyParAff)

        Next

        '--> 2000 à 10000

        For i = 2000 To 10000 Step 1000

            AddLigne(MyGr, MyPen, Math.Log10(i), Math.Log10(1) * Coeff, Math.Log10(i), Math.Log10(20) * Coeff, MyParAff)

        Next

        '--> 20000 à 90000

        For i = 20000 To 90000 Step 10000

            AddLigne(MyGr, MyPen, Math.Log10(i), Math.Log10(1) * Coeff, Math.Log10(i), Math.Log10(20) * Coeff, MyParAff)

        Next

        For i = 0 To Balise.GetUpperBound(0)
            AddTexte(MyGr, MyPenbrush, Balise(i).ToString, MyFont, Math.Log10(Balise(i)), Math.Log10(1) * Coeff - Decal / 2.5, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
        Next

        '==> Dessin Légende <==========================================================================================

        Dim d As Decimal = (Math.Log10(100000) - Math.Log10(100)) / 7
        nbPts = 5
        ReDim xPts(nbPts - 1)
        ReDim yPts(nbPts - 1)
        Dim FontColors() As Color = {Color.White, Color.Black, Color.Black, Color.Black, Color.White, Color.White, Color.Black}
        Dim MyFontLeg = New Font("Arial", 8.25, FontStyle.Bold)

        For i = 0 To 6

            xPts(0) = Math.Log10(100) + d * i : yPts(0) = Math.Log10(1) * Coeff - Decal * kDec
            xPts(1) = Math.Log10(100) + d * (i + 1) : yPts(1) = Math.Log10(1) * Coeff - Decal * kDec
            xPts(2) = Math.Log10(100) + d * (i + 1) : yPts(2) = Math.Log10(1) * Coeff - Decal * (kDec + 0.5)
            xPts(3) = Math.Log10(100) + d * i : yPts(3) = Math.Log10(1) * Coeff - Decal * (kDec + 0.5)
            xPts(4) = Math.Log10(100) + d * i : yPts(4) = Math.Log10(1) * Coeff - Decal * kDec

            MyBrush = New SolidBrush(Colors(i))

            RemplirZone(MyGr, MyBrush, xPts, yPts, nbPts, MyParAff, True)

            AddTexte(MyGr, New SolidBrush(FontColors(i)), LettresIntervalles(i), MyFontLeg, Math.Log10(100) + (d * (i + 1)) - d / 2, Math.Log10(1) * Coeff - Decal * kDec, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)

            If i <> 6 Then
                AddTexte(MyGr, MyPenbrush, LimitesIntervalles(i), MyFont, Math.Log10(100) + (d * (i + 1)), Math.Log10(1) * Coeff - Decal * (kDec + 0.5), MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
            End If

        Next

        AddTexte(MyGr, MyPenbrush, "OS-RMS90 (m/s)", MyFontLeg, Math.Log10(100), Math.Log10(1) * Coeff - Decal * (kDec + 0.5), MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)

        AddTexte(MyGr, MyPenbrush, "f (Hz)", MyFontLeg, Math.Log10(100), Math.Log10(20) * Coeff + 0.75 * Decal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)
        AddTexte(MyGr, MyPenbrush, "m (kg)", MyFontLeg, Math.Log10(100000), Math.Log10(1) * Coeff - 0.75 * Decal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)

        '==> TITRE AMORTISSEMENT <======================================================================================

        Dim xo, xe, yo, ye As Single
        Dim Titre As String = strDamp & " = " & MyDamp.ToString & "%"
        Dim LargeurT As Single = MyGr.MeasureString(Titre, MyFontLeg).Width / MyParAff.CRed
        Dim HauteurT As Single = MyGr.MeasureString(Titre, MyFontLeg).Height / MyParAff.CRed
        Const kCoefL As Single = 1.33
        Dim Marge As Single = LargeurT / 1.5

        xo = (Math.Log10(MASSMODMIN) + Math.Log10(MASSMODMAX)) / 2 - LargeurT * kCoefL / 2 - Marge
        xe = (Math.Log10(MASSMODMIN) + Math.Log10(MASSMODMAX)) / 2 + LargeurT * kCoefL / 2 + Marge
        yo = Math.Log10(20) * Coeff + 0.75 * Decal + HauteurT * kCoefL / 2
        ye = Math.Log10(20) * Coeff + 0.75 * Decal - HauteurT * kCoefL / 2
        AddRectanglePlein(MyGr, Color.Gray, xo, yo, xe, ye, MyParAff, False)
        AddTexte(MyGr, New SolidBrush(Color.White), Titre, MyFontLeg, (Math.Log10(MASSMODMIN) + Math.Log10(MASSMODMAX)) / 2, Math.Log10(20) * Coeff + 0.75 * Decal, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)

        '==> Dessin Point donné <=======================================================================================

        MyPen.Width = 2

        Dim Frequence As Decimal = Math.Min(20, Math.Max(2, MyFreq))
        Dim Masse As Decimal = Math.Min(MASSMODMAX, Math.Max(MASSMODMIN, MyMass))

        AddLigne(MyGr, MyPen, Math.Log10(Masse), Math.Log10(Frequence) * Coeff - Decal / 4, Math.Log10(Masse), Math.Log10(Frequence) * Coeff + Decal / 4, MyParAff)
        AddLigne(MyGr, MyPen, Math.Log10(Masse) - Decal / 4, Math.Log10(Frequence) * Coeff, Math.Log10(Masse) + Decal / 4, Math.Log10(Frequence) * Coeff, MyParAff)
        AddCercle(MyGr, MyPen, Math.Log10(Masse), Math.Log10(Frequence) * Coeff, Decal / 2, MyParAff)

    End Sub

    Private Sub TraitementDonnees(ByVal nbPoints As Integer, ByRef DonneeX As List(Of Decimal()), ByRef DonneeY() As Decimal)

        Dim i As Integer

        '--> Modification des données pour le dessin
        For i = 0 To nbPoints - 1

            ' -1 au débout = 100
            Dim y As Integer = 0
            If DonneeX.Item(i)(0) = -1 Then
                While DonneeX.Item(i)(y) = -1 And y < 6
                    DonneeX.Item(i)(y) = 100
                    y += 1
                End While
            End If

            ' -1 à la fin = 100000
            Dim z As Integer = 5
            If DonneeX.Item(i)(5) = -1 Then
                While DonneeX.Item(i)(z) = -1 And z >= 0
                    DonneeX.Item(i)(z) = 100000
                    z -= 1
                End While
            End If

        Next

    End Sub
    Private Sub PreparePoints(ByVal nbPts As Integer, ByRef xPts() As Single, ByRef yPts() As Single)

        For i As Integer = 0 To nbPts - 1
            xPts(i) = CSng(Math.Log10(xPts(i)))
            yPts(i) = CSng(Math.Log10(yPts(i)) * Coeff)
        Next

    End Sub

#End Region

#Region " Dessins des calculs au feu "

    Public Sub DessineCourbeEchauffement(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single,
                                         myBeam As cls_Poutre,
                                         ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des courbes de températures gaz et structure
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre à dessiner
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul
                DessineCourbeEchauffementAcier(myGr, pWi, pHi, myBeam, xLeft, yTop)
            Case cls_Section.Enum_TypeSection.Mixte
                DessineCourbeEchauffementMixte(myGr, pWi, pHi, myBeam, xLeft, yTop)
        End Select

    End Sub

    Private Sub InitialiseCourbesEchauffement(ByVal pWi As Single, ByVal pHi As Single,
                                              ByRef myParAff As Struc_Affichage, ByRef LargD As Decimal, ByRef HautD As Decimal,
                                              ByRef dCar As Decimal,
                                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Initialisation du dessin des courbes de températures gaz et acier
        '-----------------------------------------------------------------------------------------------
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myParAff    [S] :   Paramètres d'affichage
        '   LargD       [S] :   Dimension de référence - largeur du diagramme
        '   HautD       [S] :   Dimension de référence - hauteur du diagramme
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xMin, yMin, xMax, yMax As Double
        Const kADJUST As Decimal = 0.95

        Const Alpha As Decimal = 3 / 4            'Rapport Hauteur/largeur

        '--( Initialisation

        LargD = 1000
        HautD = Alpha * LargD
        dCar = Math.Sqrt(LargD ^ 2 + HautD ^ 2) / 15

        xMin = 0 - dCar
        xMax = LargD + dCar

        yMin = -dCar
        yMax = HautD + dCar

        ParametresAffichage(myParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

    End Sub

    Private Sub DessineAxesQCourbesTemperatures(ByRef myGr As Graphics, ByRef myParAff As Struc_Affichage,
                                                LargD As Decimal, HautD As Decimal, dCar As Decimal, FontAxe As Font,
                                                TimeSteps() As Decimal, ColorQ As Color, kConvX As Decimal,
                                                tabTemp() As Decimal, tabTempLabel() As Decimal, kConvY As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des axes et quadrillages pour les courbes de températures
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myParAff    [E] :   Paramètres du dessin
        '   LargD, HautD[E] :   Dimensions de la zone du dessin
        '   dCar        [E] :   Dimension de référence
        '   FontAxe     [E] :   Police pour l'affichage des axes
        '   TimeSteps   [E] :   Table des valeurs de temps en minutes ou l'on place un quadrillage vertical
        '   ColorQ      [E] :   Couleur utilisée pour le quadrillage
        '   kConvX      [E] :   Facteur de conversion des unités / x
        '   tabTemp     [E] :   Table des températures pour lesquelles on trave le quadrillage en température
        '   tabTempLabel[E] :   Table des températures pour lesquelles on affiche une valeur sur l'axe pour le quadrillage en température
        '   kConvY      [E] :   Facteur de conversion des unités / y
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim myPen As New Pen(Color.Black)
        Dim Chaine As String = ""
        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim myPenQ As New Pen(ColorQ)

        '--( Axes

        AddFleche(myGr, myPen, 0, 0, LargD + dCar, 0, myParAff, False, True)
        AddFleche(myGr, myPen, 0, 0, 0, HautD + dCar, myParAff, False, True)

        Chaine = "t (min)"
        AddTexte(myGr, New SolidBrush(Color.Black), Chaine, FontAxe, LargD + dCar, 0, myParAff, HorizontalAlignment.Left, VerticalAlignement.Bottom)
        Chaine = "temp (°C)"
        AddTexte(myGr, New SolidBrush(Color.Black), Chaine, FontAxe, 0, HautD + dCar, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

        '--( Quadrillage temps

        For i As Integer = 0 To TimeSteps.Count - 1

            xo = TimeSteps(i) * 60 * kConvX
            yo = 0
            ye = HautD

            AddLigne(myGr, myPenQ, xo, yo, xo, ye, myParAff)

            Chaine = CStr(cls_VerifFeuAcier.TimeSteps(i))

            AddTexte(myGr, New SolidBrush(Color.Gray), Chaine, FontAxe, xo, yo, myParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)
        Next

        '--( Quadrillage températures

        For i As Integer = 0 To tabTemp.GetUpperBound(0)

            xo = 0
            xe = LargD

            yo = tabTemp(i) * kConvY
            ye = yo

            AddLigne(myGr, myPenQ, xo, yo, xe, ye, myParAff)

            If tabTempLabel.Contains(tabTemp(i)) Then
                Chaine = CStr(tabTemp(i))

                AddTexte(myGr, New SolidBrush(Color.Gray), Chaine, FontAxe, xo, yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Middle)

            End If

        Next

        '--( Fin

        myPen.Dispose()
    End Sub

    Private Sub DessineCourbeTempGaz(ByRef myGr As Graphics, ByRef myParAff As Struc_Affichage, myFont As Font,
                                     kConvX As Decimal, kConvY As Decimal, ColorG As Color, indG As Integer,
                                     TimeMax As Decimal, tPosInd As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation de la courbe de température des gaz
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myParAff    [E] :   Paramètres du dessin
        '   kConvX      [E] :   Facteur de conversion / x
        '   kConvY      [E] :   Facteur de conversion / y
        '   ColorG      [E] :   Couleur de la courbe des gaz
        '   indG        [E] :   Indice de la courbe pour la légende
        '   tPosInd     [E] :   Temps au droit duquel on place l'indice de la courbe en min (si -1, on place à l'extrémité droite)   
        '   TimeMax     [E] :   Durée max d'expo en secondes 
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim ENFeu As New cls_EurocodesFeu
        Dim ThetaG(1) As Decimal
        Dim ThetaA(1) As Decimal

        Dim myPenG As New Pen(ColorG)

        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim t(1) As Decimal
        Dim TimeMaxMin As Decimal = TimeMax / 60
        Dim ThetaInd As Decimal

        '--( Courbe des gaz

        t(0) = 0
        ThetaG(0) = ENFeu.TemperatureGazISO(0)
        For i As Integer = 0 To CInt(TimeMaxMin - 1)

            t(1) = (i + 1) * 60
            ThetaG(1) = ENFeu.TemperatureGazISO(CDec(t(1)))

            xo = t(0) * kConvX
            xe = t(1) * kConvX
            yo = ThetaG(0) * kConvY
            ye = ThetaG(1) * kConvY

            AddLigne(myGr, myPenG, xo, yo, xe, ye, myParAff)

            t(0) = t(1)
            ThetaG(0) = ThetaG(1)

        Next

        If tPosInd = -1 Then
            AddTexte(myGr, New SolidBrush(ColorG), CStr(indG), myFont, xe, ye, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)
        Else

            ThetaInd = ENFeu.TemperatureGazISO(CDec(tPosInd * 60))
            xo = tPosInd * kConvX * 60
            yo = ThetaInd * kConvY

            xe = (tPosInd * 60 - DELTATime) * kConvX
            ye = (ThetaInd + DELTATHETA) * kConvY

            AddLigne(myGr, myPenG, xo, yo, xe, ye, myParAff)
            AddTexte(myGr, New SolidBrush(ColorG), CStr(indG), myFont, xe, ye, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)

        End If

    End Sub

    Private Sub DessineCourbeTempGazVoid(ByRef myGr As Graphics, ByRef myParAff As Struc_Affichage, myFont As Font,
                                         kConvX As Decimal, kConvY As Decimal, ColorG As Color, indG As Integer, TimeMax As Decimal,
                                         tPosInd As Decimal, CRed1 As Decimal, CRed2 As Decimal)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation de la courbe de température des gaz dans les cavités des creux d'ondes
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myParAff    [E] :   Paramètres du dessin
        '   kConvX      [E] :   Facteur de conversion / x
        '   kConvY      [E] :   Facteur de conversion / y
        '   ColorG      [E] :   Couleur de la courbe des gaz
        '   indG        [E] :   Indice de la courbe pour la légende
        '   TimeMax     [E] :   Durée max d'expo en secondes
        '   tPosInd     [E] :   Temps au droit duquel on place l'indice de la courbe en min (si -1, on place à l'extrémité droite)   
        '   CRed1,CRed2 [E] :   Coefficient pour le calcul des températures
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim ENFeu As New cls_EurocodesFeu
        Dim ThetaG(1) As Decimal
        Dim ThetaA(1) As Decimal
        Dim ThetaInd As Decimal

        Dim myPenG As New Pen(ColorG)

        Dim xo, yo As Double
        Dim xe, ye As Double
        Dim t(1) As Decimal
        Dim TimeMaxMin As Decimal = TimeMax / 60

        '--( Courbe des gaz

        t(0) = 0
        ThetaG(0) = ENFeu.TemperatureGazVoid(0, CRed1, CRed2)
        For i As Integer = 0 To CInt(TimeMaxMin - 1)

            t(1) = (i + 1) * 60
            ThetaG(1) = ENFeu.TemperatureGazVoid(CDec(t(1)), CRed1, CRed2)

            xo = t(0) * kConvX
            xe = t(1) * kConvX
            yo = ThetaG(0) * kConvY
            ye = ThetaG(1) * kConvY

            AddLigne(myGr, myPenG, xo, yo, xe, ye, myParAff)

            t(0) = t(1)
            ThetaG(0) = ThetaG(1)

        Next

        If tPosInd = -1 Then
            AddTexte(myGr, New SolidBrush(ColorG), CStr(indG), myFont, xe, ye, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)
        Else
            ThetaInd = ENFeu.TemperatureGazVoid(CDec(tPosInd * 60), CRed1, CRed2)
            xo = tPosInd * kConvX * 60
            yo = ThetaInd * kConvY

            xe = (tPosInd * 60 - DELTATime) * kConvX
            ye = (ThetaInd + DELTATHETA) * kConvY

            AddLigne(myGr, myPenG, xo, yo, xe, ye, myParAff)
            AddTexte(myGr, New SolidBrush(ColorG), CStr(indG), myFont, xe, ye, myParAff, HorizontalAlignment.Center, VerticalAlignement.Top)

        End If
    End Sub

    Private Sub DessineCourbeTempElt(ByRef myGr As Graphics, ByRef myParAff As Struc_Affichage, myFont As Font,
                                     kConvX As Decimal, kConvY As Decimal, ColorC As Color, indC As Integer,
                                     TempRef As Decimal, TempInter As List(Of Decimal), TimeInter As Decimal,
                                     TimeIndSec As Decimal, lDessus As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation de la courbe de température d'un élément
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myParAff    [E] :   Paramètres du dessin
        '   kConvX      [E] :   Facteur de conversion / x
        '   kConvY      [E] :   Facteur de conversion / y
        '   ColorC      [E] :   Couleur de la courbe
        '   indC        [E] :   Indice de la courbe pour la légende
        '   TempRef     [E] :   Température à t = 0
        '   TempInter   [E] :   Liste des températures à tracer
        '   TimeInter   [E] :   Intervalle de temps entre les température de la liste (en secondes)
        '   TimeIndSec  [E] :   Temps (en secondes) au droit duquel on place l'indice de la courbe en secondes (si -1, on place à l'extrémité droite)
        '   lDessus     [E] :   Si True, on place l'indice au dessus de la courbe, sinon en dessous 
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim t(1) As Decimal
        Dim Theta(1) As Decimal
        Dim myPenC As New Pen(ColorC)

        Dim xo, yo As Double
        Dim xe, ye As Double

        Dim lTrouve As Boolean = False
        Dim tInd, TempInd As Decimal
        Dim VAlign As VerticalAlignement
        Dim kSigne As Decimal

        '-( Traitement

        t(0) = 0
        Theta(0) = TempRef

        For i As Integer = 0 To TempInter.Count - 1

            t(1) = (i + 1) * TimeInter
            Theta(1) = TempInter(i)

            xo = t(0) * kConvX
            xe = t(1) * kConvX
            yo = Theta(0) * kConvY
            ye = Theta(1) * kConvY

            AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)

            If (Not lTrouve) And (t(1) > TimeIndSec) Then
                lTrouve = True
                tInd = t(1)
                TempInd = Theta(1)
            End If

            t(0) = t(1)
            Theta(0) = Theta(1)

        Next

        If TimeIndSec = -1 Then
            AddTexte(myGr, New SolidBrush(ColorC), CStr(indC), myFont, xe, ye, myParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
        Else

            If lDessus Then
                VAlign = VerticalAlignement.Top
                kSigne = 1
            Else
                VAlign = VerticalAlignement.Bottom
                kSigne = -1
            End If

            xo = tInd * kConvX
            yo = TempInd * kConvY

            xe = (tInd - kSigne * DELTATime) * kConvX
            ye = (TempInd + kSigne * DELTATHETA) * kConvY

            AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)
            AddTexte(myGr, New SolidBrush(ColorC), CStr(indC), myFont, xe, ye, myParAff, HorizontalAlignment.Center, VAlign)

        End If

    End Sub

    Private Sub DessineCourbeTempDalleTab(ByRef myGr As Graphics, ByRef myParAff As Struc_Affichage, myFont As Font,
                                          kConvX As Decimal, kConvY As Decimal, ColorC As Color, indC As Integer,
                                          TempRef As Decimal, TimeSteps() As Decimal,
                                          TempDStep(,) As Decimal, NbSteps As Integer, IndtPosInd As Integer)
        '-----------------------------------------------------------------------------------------------
        '   24/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation de la courbe de température de la dalle
        '   Obtenues avec la méthode tabulée
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   myParAff    [E] :   Paramètres du dessin
        '   kConvX      [E] :   Facteur de conversion / x
        '   kConvY      [E] :   Facteur de conversion / y
        '   ColorC      [E] :   Couleur de la courbe
        '   indC        [E] :   Indice de la courbe pour la légende
        '   TempRef     [E] :   Température à t = 0
        '   TimeSteps   [E] :   Tableau des temps auxquels la température a été calculée (en minutes)
        '   TempDStep   [E] :   Tabeau des températures sur les deux faces de la dalle
        '   NbSteps     [E] :   Nombre de durées d'exposition prises en compte dans la courbe
        '   indtPosInd  [E] :   Indice du Temps au droit duquel on place l'indice de la courbe en min (si -1, on place à l'extrémité droite) 
        '-----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim t(1) As Decimal
        Dim Theta(1) As Decimal
        Dim myPenC As New Pen(ColorC)

        Dim xo, yo As Double
        Dim xe, ye As Double

        Dim ThetaInd As Decimal
        Dim kSigne As Decimal
        Dim VAlign() As VerticalAlignement = {VerticalAlignement.Bottom, VerticalAlignement.Top}

        '-( Traitement

        For k As Integer = 0 To 1

            t(0) = 0
            Theta(0) = TempRef

            'For i As Integer = 0 To TimeSteps.GetUpperBound(0)
            For i As Integer = 0 To NbSteps - 1

                t(1) = TimeSteps(i) * 60
                Theta(1) = TempDStep(i, k)

                xo = t(0) * kConvX
                xe = t(1) * kConvX
                yo = Theta(0) * kConvY
                ye = Theta(1) * kConvY

                AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)

                t(0) = t(1)
                Theta(0) = Theta(1)

            Next

            If IndtPosInd = -1 Then
                AddTexte(myGr, New SolidBrush(ColorC), CStr(indC + k), myFont, xe, ye, myParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
            Else

                kSigne = CDec(k * 2 - 1)
                ThetaInd = TempDStep(IndtPosInd, k)
                xo = TimeSteps(IndtPosInd) * 60 * kConvX
                yo = ThetaInd * kConvY

                xe = (TimeSteps(IndtPosInd) * 60 - kSigne * DELTATime) * kConvX
                ye = (ThetaInd + kSigne * DELTATHETA) * kConvY

                AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)
                AddTexte(myGr, New SolidBrush(ColorC), CStr(indC + k), myFont, xe, ye, myParAff, HorizontalAlignment.Center, VAlign(k))

            End If

        Next

    End Sub

    Public Sub DessineCourbeEchauffementMixte(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single,
                                              myBeam As cls_Poutre,
                                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des courbes de températures gaz et acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre à dessiner
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        '--( Declarations

        Dim MyParAff As Struc_Affichage
        Dim HautD, LargD As Decimal
        Dim dCar As Decimal
        Dim kConvX, kConvY As Decimal
        Dim ColorQ As Color = Color.LightGray
        Dim ColorG As Color = Color.DarkRed
        Dim ColorA As Color = Color.DarkBlue
        Dim ColorB As Color = Color.DarkOrange
        Dim myPenG As New Pen(ColorG)
        Dim myPenA As New Pen(ColorA)
        Dim myPenQ As New Pen(ColorQ)

        Dim TimeMax As Decimal

        Dim TempMax As Decimal
        Dim FontAxe As New Font(FontBase.Name, 7)

        Dim tabTemp() As Decimal = {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200}
        Dim tabTempLabel() As Decimal = {100, 300, 500, 700, 900, 1100, 1200}

        Dim lBoard As Boolean = myBeam.ParamFeu.lProtectionBoard
        Dim lProtege As Boolean = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)

        Dim indB As Integer

        Dim EN_Feu As New cls_EurocodesFeu
        Dim NbSteps As Integer
        Dim lMethCO As Boolean
        Dim tR30 As Decimal = cls_VerifFeuMixte.TimeSteps(0)

        Const kUnitS As Decimal = 60

        '--( Initialisations

        InitialiseCourbesEchauffement(pWi, pHi, MyParAff, LargD, HautD, dCar, xLeft, yTop)

        NbSteps = EN_Feu.NombreTimeStepsIncendie(myBeam)
        lMethCO = EN_Feu.MethodeCreuxOnde(myBeam)

        'TimeMax = Math.Max((myBeam.VerifFeuMixte.TempFSInter.Count * myBeam.VerifFeuMixte.TimeInter), cls_VerifFeuMixte.TimeSteps.Last * 60)
        TimeMax = Math.Max((myBeam.VerifFeuMixte.TempFSInter.Count * myBeam.VerifFeuMixte.TimeInter), cls_VerifFeuMixte.TimeSteps(NbSteps - 1) * kUnitS)

        TempMax = tabTemp.Max

        kConvX = LargD / TimeMax
        kConvY = HautD / TempMax

        '--( Axes et quadrillage

        DessineAxesQCourbesTemperatures(myGr, MyParAff, LargD, HautD, dCar, FontAxe,
                                        cls_VerifFeuAcier.TimeSteps, ColorQ, kConvX, tabTemp, tabTempLabel, kConvY)

        '--( Tracé de la courbe de température des gaz

        indB += 1
        DessineCourbeTempGaz(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorG, indB, TimeMax, tR30)

        '--( Tracé de la courbe de température des gaz dans les cavités

        If lMethCO Then
            Dim PhiVoid As Decimal
            Dim CRed(1) As Decimal
            PhiVoid = EN_Feu.PhiVoid(myBeam.Dalle.Bac, myBeam.Section.ProfilA.Bfs, myBeam.ParamFeu.EpProtection)
            CRed(0) = EN_Feu.CoefRed1(PhiVoid)
            CRed(1) = EN_Feu.CoefRed2(PhiVoid)

            DessineCourbeTempGazVoid(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorG, 2, TimeMax, tR30, CRed(0), CRed(1))
            indB = 2
        End If

        '--( Tracé des courbes de températures de l'acier

        If (lProtege And lBoard) Then
            '## Teméprature de la mySection
            indB += 1
            DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorA, indB,
                                 myBeam.ParamFeu.TempRef, myBeam.VerifFeuMixte.TempFSInter, myBeam.VerifFeuMixte.TimeInter, cls_VerifFeuMixte.TimeSteps(1) * kUnitS, True)

        Else
            '## Température de la semelle sup
            indB += 1
            DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorA, indB,
                                 myBeam.ParamFeu.TempRef, myBeam.VerifFeuMixte.TempFSInter, myBeam.VerifFeuMixte.TimeInter, cls_VerifFeuMixte.TimeSteps(0) * kUnitS, False)
            '## Température de la semelle inf
            indB += 1
            DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorA, indB,
                                 myBeam.ParamFeu.TempRef, myBeam.VerifFeuMixte.TempFIInter, myBeam.VerifFeuMixte.TimeInter, cls_VerifFeuMixte.TimeSteps(0) / 2 * kUnitS, True)
            '## Température de l'âme
            indB += 1
            DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorA, indB,
                                 myBeam.ParamFeu.TempRef, myBeam.VerifFeuMixte.TempWInter, myBeam.VerifFeuMixte.TimeInter, cls_VerifFeuMixte.TimeSteps(0) / 2 * kUnitS, True)

        End If

        '--( Tracé des courbes température dans la dalle

        If myBeam.ParamFeu.lDalleFEM Then
            '## Température de la dalle
            '### Fibre inférieure
            indB += 1
            DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorB, indB,
                                 myBeam.ParamFeu.TempRef, myBeam.VerifFeuMixte.TempDInter(0), myBeam.VerifFeuMixte.TimeInter, cls_VerifFeuMixte.TimeSteps(1) * kUnitS, True)

            '### Fibre supérieure
            indB += 1
            DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorB, indB,
                                 myBeam.ParamFeu.TempRef, myBeam.VerifFeuMixte.TempDInter(1), myBeam.VerifFeuMixte.TimeInter, cls_VerifFeuMixte.TimeSteps(1) * kUnitS, False)

        Else
            indB += 1
            DessineCourbeTempDalleTab(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorB, indB,
                                      myBeam.ParamFeu.TempRef, cls_VerifFeuMixte.TimeSteps, myBeam.VerifFeuMixte.TempDalleStep, NbSteps, 1)
        End If

        '--( Fin

        myPenG.Dispose()
        myPenQ.Dispose()
        myPenA.Dispose()
        FontAxe.Dispose()

    End Sub

    Public Sub DessineCourbeEchauffementAcier(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single,
                                              myBeam As cls_Poutre,
                                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   22/10/24 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des courbes de températures gaz et acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   myBeam      [E] :   Poutre à dessiner
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        '--( Declarations

        Dim MyParAff As Struc_Affichage
        Dim HautD, LargD As Decimal
        Dim dCar As Decimal
        Dim kConvX, kConvY As Decimal
        Dim ColorQ As Color = Color.LightGray
        Dim ColorG As Color = Color.DarkRed
        Dim ColorA As Color = Color.DarkBlue
        Dim myPenG As New Pen(ColorG)
        Dim myPenA As New Pen(ColorA)
        Dim myPenQ As New Pen(ColorQ)

        Dim TimeMax As Decimal
        Dim TempMax As Decimal
        Dim FontAxe As New Font(FontBase.Name, 7)

        Dim tabTemp() As Decimal = {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200}
        Dim tabTempLabel() As Decimal = {100, 300, 500, 700, 900, 1100, 1200}

        Dim tR30 As Decimal = cls_VerifFeuAcier.TimeSteps(0)
        Const kUnitS As Decimal = 60

        '--( Initialisations

        InitialiseCourbesEchauffement(pWi, pHi, MyParAff, LargD, HautD, dCar, xLeft, yTop)

        TimeMax = Math.Max((myBeam.VerifFeuAcier.TempAInter.Count * myBeam.VerifFeuAcier.TimeInter), cls_VerifFeuAcier.TimeSteps.Last * kUnitS)

        TempMax = tabTemp.Max

        kConvX = LargD / TimeMax
        kConvY = HautD / TempMax

        '--( Axes et quadrillage

        DessineAxesQCourbesTemperatures(myGr, MyParAff, LargD, HautD, dCar, FontAxe,
                                        cls_VerifFeuAcier.TimeSteps, ColorQ, kConvX, tabTemp, tabTempLabel, kConvY)

        '--( Tracé de la courbe de température des gaz

        DessineCourbeTempGaz(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorG, 1, TimeMax, tr30)

        '--( Tracé de la courbe de température de l'acier

        DessineCourbeTempElt(myGr, MyParAff, FontAxe, kConvX, kConvY, ColorA, 2,
                             myBeam.ParamFeu.TempRef, myBeam.VerifFeuAcier.TempAInter, myBeam.VerifFeuAcier.TimeInter, cls_VerifFeuAcier.TimeSteps(1) * kUnitS, True)

        '--( Fin

        myPenG.Dispose()
        myPenQ.Dispose()
        myPenA.Dispose()
        FontAxe.Dispose()

    End Sub

    'Public Sub DessineCourbeEchauffementAcierOld(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single,
    '                                          myBeam As cls_Poutre,
    '                                          ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
    '    '-----------------------------------------------------------------------------------------------
    '    '   22/10/24 :  Version 1.00
    '    '-----------------------------------------------------------------------------------------------
    '    '   Représentation des courbes de températures gaz et acier
    '    '-----------------------------------------------------------------------------------------------
    '    '   myGr        [E] :   Graphics dans lequel on dessine
    '    '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
    '    '   myBeam      [E] :   Poutre à dessiner
    '    '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
    '    '-----------------------------------------------------------------------------------------------

    '    '--( Declarations

    '    Dim MyParAff As Struc_Affichage
    '    Dim xMin, yMin, xMax, yMax As Double
    '    Dim Largeur As Decimal = 240
    '    Dim HauteurT As Decimal

    '    Dim dCar As Decimal = Largeur / 10
    '    Const Alpha As Decimal = 3 / 4            'Rapport Hauteur/largeur
    '    Dim Hauteur As Decimal = Alpha * Largeur

    '    Dim kConvY As Decimal
    '    Dim kConvX As Decimal = Largeur / (myBeam.VerifFeuAcier.TempAInter.Count * myBeam.VerifFeuAcier.TimeInter)

    '    Dim myPen As New Pen(Color.Black)
    '    Dim ENFeu As New cls_EurocodesFeu
    '    Dim ThetaG(1) As Decimal
    '    Dim ThetaA(1) As Decimal
    '    Dim ColorQ As Color = Color.LightGray
    '    Dim ColorG As Color = Color.DarkRed
    '    Dim ColorA As Color = Color.DarkBlue
    '    Dim myPenG As New Pen(ColorG)
    '    Dim myPenA As New Pen(ColorA)
    '    Dim myPenQ As New Pen(ColorQ)

    '    Dim xo, yo As Double
    '    Dim xe, ye As Double
    '    Dim t(1) As Decimal

    '    Dim Chaine As String = ""
    '    Dim FontAxe As New Font(FontBase.Name, 7)

    '    Dim tabTemp() As Decimal = {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200}
    '    Dim tabTempLabel() As Decimal = {100, 300, 500, 700, 900, 1100, 1200}

    '    '--( Initialisation

    '    HauteurT = Math.Max((myBeam.VerifFeuAcier.TempAInter.Max), ENFeu.TemperatureGazISO(240 * 60))
    '    HauteurT = Math.Max(HauteurT, tabTemp.Max)

    '    kConvY = Hauteur / HauteurT

    '    xMin = 0 - dCar
    '    xMax = Largeur + dCar

    '    yMin = -dCar
    '    yMax = Hauteur + dCar

    '    ' ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

    '    '--( Axes

    '    AddFleche(myGr, myPen, 0, 0, Largeur + dCar, 0, MyParAff, False, True)
    '    AddFleche(myGr, myPen, 0, 0, 0, Hauteur + dCar, MyParAff, False, True)

    '    Chaine = "t (min)"
    '    AddTexte(myGr, New SolidBrush(Color.Black), Chaine, FontAxe, Largeur + dCar, 0, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Bottom)
    '    Chaine = "temp (°C)"
    '    AddTexte(myGr, New SolidBrush(Color.Black), Chaine, FontAxe, 0, Hauteur + dCar, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

    '    '--( Quadrillage

    '    For i As Integer = 0 To cls_VerifFeuAcier.TimeSteps.Count - 1

    '        xo = cls_VerifFeuAcier.TimeSteps(i) * 60 * kConvX
    '        yo = 0
    '        ye = Hauteur

    '        AddLigne(myGr, myPenQ, xo, yo, xo, ye, MyParAff)

    '        Chaine = CStr(cls_VerifFeuAcier.TimeSteps(i))

    '        AddTexte(myGr, New SolidBrush(Color.Gray), Chaine, FontAxe, xo, yo, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Bottom)

    '    Next

    '    For i As Integer = 0 To tabTemp.GetUpperBound(0)

    '        xo = 0
    '        xe = Largeur

    '        yo = tabTemp(i) * kConvY
    '        ye = yo

    '        AddLigne(myGr, myPenQ, xo, yo, xe, ye, MyParAff)

    '        If tabTempLabel.Contains(tabTemp(i)) Then
    '            Chaine = CStr(tabTemp(i))

    '            AddTexte(myGr, New SolidBrush(Color.Gray), Chaine, FontAxe, xo, yo, MyParAff, HorizontalAlignment.Right, VerticalAlignement.Middle)

    '        End If

    '    Next

    '    '--( Tracé de la courbe des gaz

    '    t(0) = 0
    '    ThetaG(0) = ENFeu.TemperatureGazISO(0)
    '    For i As Integer = 0 To 239

    '        t(1) = (i + 1) * 60
    '        ThetaG(1) = ENFeu.TemperatureGazISO(CDec(t(1)))

    '        xo = t(0) * kConvX
    '        xe = t(1) * kConvX
    '        yo = ThetaG(0) * kConvY
    '        ye = ThetaG(1) * kConvY

    '        AddLigne(myGr, myPenG, xo, yo, xe, ye, MyParAff)

    '        t(0) = t(1)
    '        ThetaG(0) = ThetaG(1)

    '    Next

    '    AddTexte(myGr, New SolidBrush(ColorG), "1", FontAxe, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

    '    '--( Tracé de la température de l'acier

    '    t(0) = 0
    '    ThetaA(0) = myBeam.ParamFeu.TempRef
    '    For i As Integer = 0 To myBeam.VerifFeuAcier.TempAInter.Count - 1

    '        t(1) = (i + 1) * myBeam.VerifFeuAcier.TimeInter
    '        ThetaA(1) = myBeam.VerifFeuAcier.TempAInter(i)

    '        xo = t(0) * kConvX
    '        xe = t(1) * kConvX
    '        yo = ThetaA(0) * kConvY
    '        ye = ThetaA(1) * kConvY

    '        'If i = 119 Then
    '        '    t(0) = t(1)
    '        'End If

    '        AddLigne(myGr, myPenA, xo, yo, xe, ye, MyParAff)

    '        t(0) = t(1)
    '        ThetaA(0) = ThetaA(1)

    '    Next

    '    AddTexte(myGr, New SolidBrush(ColorA), "2", FontAxe, xe, ye, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Bottom)

    '    AddTexte(myGr, New SolidBrush(ColorG), "1: " & labelDessin("GAZ"), FontAxe, 0, -dCar / 2, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)
    '    AddTexte(myGr, New SolidBrush(ColorA), "2: " & labelDessin("STEELPROFILE"), FontAxe, 0, -dCar, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)

    'End Sub

#End Region

#Region " Dessin des propriétés de l'acier dans les fenêtres Frm_Section... "

    Public Sub DessinProprietesAcier(ByVal MyGr As Graphics, ByVal sHI As Single, ByVal sWI As Single,
                                     ByVal lNuanceOK As Boolean, lFy As Boolean, mySection As cls_Section)
        '----------------------------------------------------------------------------------------------
        '   22/07/25 :  Création - Version 1.20 - POM
        '----------------------------------------------------------------------------------------------
        '   Graphique des propriétés fy ou fu de l'acier
        '----------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   sHI, sWI        [E] :   Dimensions du PictureBox
        '   lNuanceOK       [E] :   Indique si l'utilisateur en mode normal peut sélectionner cette nuance
        '   lFy             [E] :   Indique si affichage fy ou fu
        '----------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim RCParAff As Struc_Affichage
        Dim xMin, xMax, yMin, yMax As Double
        Dim Nuance, Norme, Qualite As String
        'Dim iSteel As Integer
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
        Dim lLamine As Boolean = mySection.lLamine
        Dim lSlim As Boolean = mySection.lSlimFloor
        Dim lSlimIFBB As Boolean = mySection.lSlimFloor_IFB_B

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

        If lLamine Or lSlim Then
            If lSlimIFBB Then
                EpProfile = Math.Max(mySection.ProfilA.Tw, mySection.ProfilA.Tfi)
            Else
                EpProfile = Math.Max(mySection.ProfilA.Tw, mySection.ProfilA.Tfs)
            End If
            If lFy Then
                FyPro = mySection.Acier.LimiteFy(EpProfile)
            Else
                FyPro = mySection.Acier.LimiteFu(EpProfile)
            End If
        Else
            EpPRSfs = mySection.ProfilA.Tfs
            EpPRSw = mySection.ProfilA.Tw
            EpPRSfi = mySection.ProfilA.Tfi
            EpPRSMax = Math.Max(EpPRSfs, Math.Max(EpPRSfi, EpPRSw))

            If lFy Then
                FyPRSfs = mySection.Acier.LimiteFy(EpPRSfs)
                FyPRSw = mySection.Acier.LimiteFy(EpPRSw)
                FyPRSfi = mySection.Acier.LimiteFy(EpPRSfi)
                FyPRSMin = mySection.Acier.LimiteFy(EpPRSMax)
            Else
                FyPRSfs = mySection.Acier.LimiteFu(EpPRSfs)
                FyPRSw = mySection.Acier.LimiteFu(EpPRSw)
                FyPRSfi = mySection.Acier.LimiteFu(EpPRSfi)
                FyPRSMin = mySection.Acier.LimiteFu(EpPRSMax)
            End If

        End If

        EpPlagesMax = mySection.Acier.EpMax

        '--( Initialisation

        Const kEch As Decimal = 0.92

        'If Me.GridAciers.Rows.Count <= 0 Then Exit Sub

        'iSteel = Me.GridAciers.SelectedCells(0).RowIndex
        Nuance = mySection.Acier.Nuance
        Qualite = mySection.Acier.Qualite
        Norme = mySection.Acier.NormeProduit

        ExtraitValeursEnveloppeAciers(Nuance, lFy, EpMin, EpMax, VMax)

        If mySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
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

                'If Qualite = kvpQualite.Key And Norme = kvpSteel.Key Then
                If Qualite = kvpQualite.Key And Norme.Contains(kvpSteel.Key) Then
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

        If lLamine Or lSlim Then
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
        '   22/07/25 :  Création - Version 1.20 - POM
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

    Private Sub DrawEpEtFyCalcul(ByVal MyGr As Graphics, ByVal RcParAff As Struc_Affichage, ByVal kFact As Double,
                                 ByVal EpPlagesMax As Double, ByVal EpProf As Double, ByVal FyCalcul As Double,
                                 ByVal xBoni As Double, ByVal zBoni As Double,
                                 ByVal myFont As Font, ByVal lNuanceOK As Boolean, lFy As Boolean,
                                 Optional ByVal lLegende As Boolean = True, Optional ByVal lPRS As Boolean = False)
        '----------------------------------------------------------------------------------------------
        '   22/07/25 :  Création - Version 1.20 - POM
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
        Const EPSILONG As Double = 0.0001

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

    Private Sub ExtraitValeursEnveloppeAciers(ByVal Nuance As String, lFy As Boolean,
                                              ByRef EpMin As Double, ByRef EpMax As Double, ByRef VMax As Double)
        '------------------------------------------------------------------------------------------------------------------
        '   22/07/25 :  Création - Version 1.20 - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Extrait les valeurs enveloppes de la nuance sélectionnée
        '------------------------------------------------------------------------------------------------------------------
        '   Nuance  [E] :   Nuance d'acier sélectionnée
        '   lFy     [E] :   Indique si on extrait les valeurs fy ou fu
        '   EpMin   [S] :   Epaisseur minimale associée à la nuance
        '   EpMax   [S] :   Epaisseur maximale associée à la nuance
        '   VMax    [S] :   Valeur maximale de fy ou fu associée à la nuance
        '------------------------------------------------------------------------------------------------------------------

        '--( Initialisations 

        EpMin = 0
        EpMax = 0
        VMax = 0

        '--( Boucle sur les nuances de la base de données

        For Each kvpQualite As KeyValuePair(Of String, strucQualite) In SteelBase.Grades(Nuance).Qualites

            For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv
                If EpMin = 0 Then
                    EpMin = kvpSteel.Value.Plages(0).Ep
                Else
                    EpMin = Math.Min(EpMin, kvpSteel.Value.Plages(0).Ep)
                End If
                EpMax = Math.Max(EpMax, kvpSteel.Value.EpMax)

                For Each kVP As cls_Acier.strucPlage In kvpSteel.Value.Plages

                    If lFy Then
                        VMax = Math.Max(VMax, kVP.Fy)
                    Else
                        VMax = Math.Max(VMax, kVP.Fu)
                    End If

                Next

            Next

        Next

    End Sub

#End Region

End Module
