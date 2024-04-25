Imports System.Runtime.InteropServices.ComTypes

Public Class cls_ModeleP

#Region " Structures et définitions "

    Const BOUCLEMAX As Integer = 10000
    Const Tolerance As Decimal = 0.01
    Const ToleranceR As Decimal = 10
    Const ToleranceS As Decimal = 1

#End Region

#Region " Paramètres de la classe "

    ''' <summary>
    ''' nombre d'éléments dans le modèle de la section
    ''' </summary>
    Public nbElts As Integer

    ''' <summary>
    ''' Liste des mailles du modèle
    ''' </summary>
    Public Mailles As List(Of cls_Maille)

#End Region

#Region " Constructeur "

    Public Sub New()

        nbElts = 0
        Mailles = New List(Of cls_Maille)

    End Sub

#End Region

#Region " Outils de création du modèle "

    ''' <summary>
    ''' Ajout d'une maille rectangulaire dans le modèle
    ''' </summary>
    ''' <param name="Aire">     [E] Aire de la maille           </param>
    ''' <param name="Epaisseur">[E] Epaisseur de la maille      </param>
    ''' <param name="zPos">     [E] Position Z                  </param>
    ''' <param name="DeltaC">   [E] Indicateur compression      </param>
    ''' <param name="DeltaT">   [E] Indicateur traction         </param>
    ''' <param name="nEq">      [E] Coefficient d'équivalence   </param>
    ''' <param name="fk">       [E] Valeur caractéristique de la Limite de comportement élastique</param>
    Public Sub AddMaille(Aire As Decimal, Epaisseur As Decimal, zPos As Decimal, DeltaT As Decimal, DeltaC As Decimal, nEq As Decimal,
                         fk As Decimal, kPl As Decimal, Gamma As Decimal,
                         Optional TypeM As cls_Maille.EnuTypeMaille = cls_Maille.EnuTypeMaille.Rectangulaire)
        '------------------------------------------------------------------------------------
        '   15/04/2023 : Création - POM
        '------------------------------------------------------------------------------------
        '   Ajout d'une maille rectangle
        '------------------------------------------------------------------------------------
        '   Aire        [E] :   Aire de la maille
        '   Epaisseur   [E] :   Epaisseur de la maille
        '   zPos        [E] :   Position z du centre de la maille
        '   DeltaT      [E] :   Indicateur pour le comportement en traction
        '   DeltaC      [E] :   Indicateur pour le comportement en compression
        '   nEq         [E] :   Coefficient d'équivalence
        '   fk          [E] :   Valeur caractéristique ou nominale de la limite d'élasticité
        '   kPl         [E] :   Coefficient appliqué à fk pour le calcul de la résistance plastique
        '   Gamma       [E] :   Coefficient partiel
        '   TypeM       [E] :   Type de la maille, par défaut rectangle
        '------------------------------------------------------------------------------------

        Mailles.Add(New cls_Maille(Aire, Epaisseur, 0, 0, zPos, nEq, fk, kPl, Gamma, DeltaC, DeltaT, 1, TypeM))

    End Sub

    ''' <summary>
    ''' Ajout dans le modèle d'une maille circulaire
    ''' </summary>
    ''' <param name="Rayon">    [E] Rayon                       </param>
    ''' <param name="zPos">     [E] Position z du centre        </param>
    ''' <param name="DeltaC">   [E] Indice compression          </param>
    ''' <param name="DeltaT">   [E] Indice traction             </param>
    ''' <param name="nEq">      [E] Coefficient d'équivalence   </param>

    Public Sub AddMailleCirculaire(Rayon As Decimal, zPos As Decimal, DeltaT As Decimal, DeltaC As Decimal, nEq As Decimal,
                                   fk As Decimal, kPl As Decimal, Gamma As Decimal,
                                   Optional Nombre As Integer = 1, Optional TypeM As cls_Maille.EnuTypeMaille = cls_Maille.EnuTypeMaille.Circulaire)
        '------------------------------------------------------------------------------------
        '   26/04/2023 : Création - POM
        '------------------------------------------------------------------------------------
        '   Ajout d'une maille cercle
        '------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon de la maille
        '   zPos        [E] :   Position z du centre de la maille
        '   DeltaT      [E] :   Indicateur pour le comportement en traction
        '   DeltaC      [E] :   Indicateur pour le comportement en compression
        '   nEq         [E] :   Coefficient d'équivalence
        '   fk          [E] :   Valeur caractéristique ou nominale de la limite d'élasticité
        '   kPl         [E] :   Coefficient appliqué à fk pour le calcul de la résistance plastique
        '   Gamma       [E] :   Coefficient partiel
        '   Nombre      [E] :   Nombre de mailles créées
        '   TypeM       [E] :   Type de la maille, par défaut cercle
        '------------------------------------------------------------------------------------

        Mailles.Add(New cls_Maille(Math.PI * Rayon ^ 2, 0, Rayon, 0, zPos, nEq, fk, kPl, Gamma, DeltaC, DeltaT, Nombre, TypeM))

    End Sub

    Public Sub AddMailleConges(Rayon As Decimal, zPos As Decimal, DeltaT As Decimal, DeltaC As Decimal, nEq As Decimal,
                               fk As Decimal, kPl As Decimal, Gamma As Decimal,
                               TypeM As cls_Maille.EnuTypeMaille, Optional Nombre As Integer = 1)
        '------------------------------------------------------------------------------------
        '   26/04/2023 : Création - POM
        '------------------------------------------------------------------------------------
        '   Ajout d'une maille congés de raccordement
        '------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon de la maille
        '   zPos        [E] :   Position z du centre de la maille
        '   DeltaT      [E] :   Indicateur pour le comportement en traction
        '   DeltaC      [E] :   Indicateur pour le comportement en compression
        '   nEq         [E] :   Coefficient d'équivalence
        '   fk          [E] :   Valeur caractéristique ou nominale de la limite d'élasticité
        '   kPl         [E] :   Coefficient appliqué à fk pour le calcul de la résistance plastique
        '   Nombre      [E] :   Nombre de mailles créées
        '   TypeM       [E] :   Type de la maille, par défaut cercle
        '------------------------------------------------------------------------------------

        Mailles.Add(New cls_Maille((4 - Math.PI) / 2 * Rayon ^ 2, 0, Rayon, 0, zPos, nEq, fk, kPl, Gamma, DeltaC, DeltaT, Nombre, TypeM))

    End Sub


#End Region

#Region " Outils "

    ''' <summary>
    ''' Renvoie les valeurs extrêmes des positions z dans le modèle
    ''' </summary>
    ''' <param name="zMin"> [S] valeur mini des z</param>
    ''' <param name="zMax"> [S] valeur maxi des z</param>
    Public Sub ExtremaZ(ByRef zMin As Decimal, ByRef zMax As Decimal)
        '------------------------------------------------------------------------------------
        '   15/04/2023 : Création - POM
        '------------------------------------------------------------------------------------

        '--> Boucle sur les mailles

        If Me.Mailles.Count > 0 Then

            zMin = Mailles(0).ExtremaZMin
            zMax = Mailles(0).ExtremaZMax

            For i As Integer = 1 To Mailles.Count - 1
                zMin = Math.Min(zMin, Mailles(i).ExtremaZMin)
                zMax = Math.Max(zMax, Mailles(i).ExtremaZMax)
            Next
        End If

    End Sub

#End Region

#Region " Outils de calcul pour les propriétés plastiques "

    Public Function CalculMomentPlastique(Signe As Decimal, zAxe As Decimal, lValeurCalcul As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------
        '   29/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul du moment plastique
        '-----------------------------------------------------------------------------------------
        '   Signe           [E] :   Signe du moment
        '   zAxe            [E] :   Position Axe Neutre Plastique
        '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Dim AddR As Decimal
        Dim ResultanteM As Decimal
        Dim IndexD As Integer

        '--> Initialisation

        ResultanteM = 0
        If lValeurCalcul Then IndexD = 0 Else IndexD = 1

        '--> Traitement

        For i = 0 To Me.Mailles.Count - 1

            AddR = Me.Mailles(i).ResultantePlastiqueM(Signe, zAxe, IndexD)
            ResultanteM += AddR

        Next i

        Return ResultanteM


    End Function

    Public Sub RechercheANP(Signe As Decimal, ByRef zANP As Decimal, lValeurCalcul As Boolean)
        '-----------------------------------------------------------------------------------------
        '   28/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANP 
        '-----------------------------------------------------------------------------------------
        '   Signe           [E] :   Signe du moment
        '   zANP            [S] :  Position Axe Neutre Plastique
        '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim zMin, zMax As Decimal
        Dim lCont As Boolean = True
        Dim Boucle As Integer = 0
        Dim Result As Decimal

        '--> Initialisation

        Me.ExtremaZ(zMin, zMax)

        '--> Recherche par dichotomie

        Do While lCont
            Boucle += 1
            zANP = (zMin + zMax) / 2
            Result = ResultantePlastiqueN(Signe, zANP, lValeurCalcul)
            If Result > 0 Then
                zMin = zANP
            Else
                zMax = zANP
            End If
            If Math.Abs((zMax - zMin) * 1000) < Tolerance Then lCont = False
            If Boucle > BOUCLEMAX Then lCont = False
            If (Math.Abs(Result) < ToleranceR) Then lCont = False
        Loop

    End Sub

    Private Function ResultantePlastiqueN(Signe As Decimal, zAxe As Decimal, lValeurCalcul As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------
        '   28/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANP 
        '-----------------------------------------------------------------------------------------
        '   Signe           [E] :   Signe du moment pour le duiagramme duquel on recherche la résultante
        '   zAxe            [E] :   Position de l'axe considéré
        '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Dim AddR As Decimal
        Dim Resultante As Decimal
        Dim IndexD As Integer

        '--> Initialisation

        Resultante = 0
        If lValeurCalcul Then IndexD = 0 Else IndexD = 1

        '--> Traitement

        For i = 0 To Me.Mailles.Count - 1

            AddR = Me.Mailles(i).ResultantePlastiqueN(Signe, zAxe, IndexD)
            Resultante += AddR

        Next i

        Return Resultante


    End Function
#End Region

#Region " Outils de calcul pour les propriétés élastiques "

    ''' <summary>
    ''' Recherche de la position de l'Axe Neutre Elastique par dichotomie 
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zANE">     [S] Position ANE                </param>
    Public Sub RechercheANE(Signe As Decimal, ByRef zANE As Decimal)
        '-----------------------------------------------------------------------------------------
        '   15/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANE 
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim zMin, zMax As Decimal
        Dim lCont As Boolean = True
        Dim Boucle As Integer = 0
        Dim Mst As Decimal

        '--> Initialisation

        Me.ExtremaZ(zMin, zMax)

        '--> Recherche par dichotomie

        Do While lCont
            Boucle += 1
            zANE = (zMin + zMax) / 2
            Mst = MomentStatique(Signe, zANE)
            If Mst > 0 Then
                zMin = zANE
            Else
                zMax = zANE
            End If
            If Math.Abs((zMax - zMin) * 1000) < Tolerance Then lCont = False
            If Boucle > BOUCLEMAX Then lCont = False
            If (Math.Abs(Mst * 1000 ^ 3) < ToleranceS) Then lCont = False
        Loop
    End Sub

    ''' <summary>
    ''' Calcul du moment statique cumulé de tous les éléments du modèle
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zAxe">     [S] Position ANE                </param>
    ''' <returns></returns>
    Private Function MomentStatique(Signe As Decimal, zAxe As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Recherche du moment statique d'un maillage
        '-------------------------------------------------------------------------------
        '   zAxe        [S] :   Position de l'axe de référence
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Dim AddM As Decimal
        Dim Mstat As Decimal

        '--> Initialisation

        Mstat = 0

        '--> Traitement

        For i = 0 To Me.Mailles.Count - 1

            AddM = Me.Mailles(i).MomentStatique(Signe, zAxe)
            Mstat += AddM

        Next i

        Return Mstat

    End Function

    ''' <summary>
    ''' Calcul du moment élastique
    ''' </summary>
    ''' <returns></returns>
    Public Function MomentElastique(Signe As Decimal, zAxe As Decimal, Inertie As Decimal, lValeurRd As Boolean) As Decimal
        '-------------------------------------------------------------------------------
        '   24/08/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Calcul du moment élastique d'un maillage
        '-------------------------------------------------------------------------------
        '   Inertie     [E] :   Inertie de la section
        '   zAXE        [E] :   Position axe de référence
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim i, IndexD As Integer
        Dim MelRd As Decimal = 0
        Dim MelI As Decimal
        Dim lActive, lResult As Boolean

        '--> Initialisation

        lResult = False
        If lValeurRd Then IndexD = 0 Else IndexD = 1

        '--> Boucle sur les mailles

        For i = 0 To Me.Mailles.Count - 1
            Me.Mailles(i).MomentElastique(Signe, zAxe, Inertie, IndexD, MelI, lActive)
            If lActive Then
                If lResult Then
                    MelRd = Signe * Math.Min(Math.Abs(MelRd), Math.Abs(MelI))
                Else
                    lResult = True
                    MelRd = MelI
                End If
            End If
        Next

        '--
        Return MelRd * kConvMPaPa
    End Function

    ''' <summary>
    ''' Calcul de l'inertie des éléments du modèle
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment     </param>
    ''' <param name="zAxe">     [E] Position z de l'axe </param>
    ''' <returns></returns>
    Public Function InertieFlexion(Signe As Decimal, zAxe As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Calcul de l'Inertie d'un maillage
        '-------------------------------------------------------------------------------
        '   Inertie     [S] :   Aire de la section
        '   zAXE        [E] :   Position axe de référence
        '   Moment      [E] :   Signe du moment
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer
        Dim Iy As Double

        Dim AddI As Double

        '--> Calcul

        Iy = 0

        For i = 0 To Me.Mailles.Count - 1

            AddI = Me.Mailles(i).InertieFlexionY(Signe, zAxe)

            Iy += AddI
        Next i

        Return Iy
    End Function



#End Region

#Region " Outils de modélisation - Profilés "

    Public Sub MaillageProfileUsuels_YY(GammaM As Decimal, RhoV As Decimal, MyProfil As cls_ProfilA,
                                        FySup As Decimal, FyInf As Decimal, FyW As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier usuels pour le calcul des propriétés / axe YY
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaM      [E] :   Coefficient partiel
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   MyProfil    [E]:    Profilé à modéliser
        '   FySup       [E] :   Limite d'élasticité semelle sup
        '   FyInf       [E] :   Limite d'élasticité semelle inf
        '   FyW         [E] :   Limite d'élasticité âme
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Hw As Decimal
        Dim zRef As Decimal = MyProfil.zRefAraseSup 'Cote de l'arase supérieure de la semelle supérieure du profilé 

        '--> Initialisation

        Hw = MyProfil.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        Me.AddMaille(MyProfil.AireFs, MyProfil.Tfs, zRef - MyProfil.Tfs / 2, 1, 1, 1, FySup, 1, GammaM)

        '# Âme

        Me.AddMaille(Hw * MyProfil.Tw, Hw, zRef - MyProfil.Tfs - Hw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM)

        '# Semelle inférieure

        Me.AddMaille(MyProfil.AireFi, MyProfil.Tfi, zRef - MyProfil.ha + MyProfil.Tfi / 2, 1, 1, 1, FyInf, 1, GammaM)

        If IsGreater(MyProfil.Rcs, 0) Then

            '# Congés supérieurs

            Me.AddMailleConges(MyProfil.Rcs, zRef - MyProfil.Tfs, 1, 1, 1, FyW, (1 - RhoV), GammaM, cls_Maille.EnuTypeMaille.CongeSup)

        End If

        If IsGreater(MyProfil.Rci, 0) Then

            '# Congés inférieurs

            Me.AddMailleConges(MyProfil.Rci, zRef - MyProfil.ha + MyProfil.Tfi, 1, 1, 1, FyW, (1 - RhoV), GammaM, cls_Maille.EnuTypeMaille.CongeInf)

        End If

    End Sub

    Public Sub MaillageProfileUsuels_ZZ(GammaM As Decimal, RhoV As Decimal, MyProfil As cls_ProfilA,
                                        FySup As Decimal, FyInf As Decimal, FyW As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier usuels pour le calcul des propriétés / axe YY
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaM      [E] :   Coefficient partiel
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   MyProfil    [E]:    Profilé à modéliser
        '   FySup       [E] :   Limite d'élasticité semelle sup
        '   FyInf       [E] :   Limite d'élasticité semelle inf
        '   FyW         [E] :   Limite d'élasticité âme
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Hw As Decimal
        Dim zRef As Decimal = MyProfil.zRefAraseSup             'Cote de l'arase supérieure de la semelle supérieure du profilé 

        '--> Initialisation

        Hw = MyProfil.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        Me.AddMaille(MyProfil.AireFs, MyProfil.Bfs, 0, 1, 1, 1, FySup, 1, GammaM)

        '# Âme

        Me.AddMaille(Hw * MyProfil.Tw, MyProfil.Tw, 0, 1, 1, 1, FyW, (1 - RhoV), GammaM)

        '# Semelle inférieure

        Me.AddMaille(MyProfil.AireFi, MyProfil.Bfi, 0, 1, 1, 1, FyInf, 1, GammaM)

        If IsGreater(MyProfil.Rcs, 0) Then

            '# Congés supérieurs

            Me.AddMailleConges(MyProfil.Rcs, -MyProfil.Tw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM, cls_Maille.EnuTypeMaille.CongeSup, 0.5)
            Me.AddMailleConges(MyProfil.Rcs, MyProfil.Tw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM, cls_Maille.EnuTypeMaille.CongeInf, 0.5)

        End If

        If IsGreater(MyProfil.Rci, 0) Then

            '# Congés inférieurs

            Me.AddMailleConges(MyProfil.Rci, -MyProfil.Tw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM, cls_Maille.EnuTypeMaille.CongeSup, 0.5)
            Me.AddMailleConges(MyProfil.Rci, MyProfil.Tw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM, cls_Maille.EnuTypeMaille.CongeInf, 0.5)

        End If

    End Sub

#End Region

#Region " Outils de modélisation - Dalle "

    Public Sub MaillageDalle_YY(GammaC As Decimal, bEff As Decimal, nEqDalle As Decimal, myDalle As cls_Dalle)
        '-------------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage de la dalle béton pour le calcul des propriétés / axe YY
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaC      [E] :   Coefficient partiel pour le béton
        '   bEff        [E] :   Largeur participante
        '   nEqDalle    [E] :   Coefficient d'équivalence pour le béton
        '   myDalle     [E] :   Dalle à mailler
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Tc As Decimal = myDalle.EpaisseurActive
        Dim Aire As Decimal

        '--> Maillage

        Aire = bEff * Tc
        Me.AddMaille(Aire, Tc, myDalle.zTop - Tc / 2, 0, 1, nEqDalle, myDalle.beton.Fck, 0.85, GammaC)

    End Sub

    Public Sub MaillageDalle_YYETA(GammaC As Decimal, bEff As Decimal, nEqDalle As Decimal, DeltaPRd As Decimal, myDalle As cls_Dalle)
        '-------------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage de la dalle béton pour le calcul des propriétés / axe YY
        '   prenant en compte le degré de connexion
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaC      [E] :   Coefficient partiel pour le béton
        '   bEff        [E] :   Largeur participante
        '   nEqDalle    [E] :   Coefficient d'équivalence pour le béton
        '   DeltaPRd    [E] :   Cumul de résistance des connecteurs jusqu'au point de moment nul
        '   MyDalle     [E] :   Dalle à mailler
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NArma As Decimal
        Dim Tc As Decimal = myDalle.EpaisseurActive
        Dim Aire As Decimal
        Dim kPlDalle As Decimal = 0.85

        '--> Initialisation

        NArma = myDalle.NResistanceCompressionDalle(bEff, GammaC)

        '--> Maillage

        Tc = Math.Min(NArma, DeltaPRd) / (bEff * kPlDalle * myDalle.beton.Fck * kConvMPaPa / GammaC)
        Aire = bEff * Tc
        MyModele.AddMaille(Aire, Tc, myDalle.zTop - Tc / 2, 0, 1, nEqDalle, myDalle.beton.Fck, 0.85, GammaC)

    End Sub

#End Region

#Region " Outils de modélisation - Enrobage partiel béton "

    Public Sub MaillageEnrobage_YY(GammaC As Decimal, nEq As Decimal, mySection As cls_Section, Optional ByVal lBetonTendu As Boolean = False)
        '-------------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier pour le calcul des propriétés / axe YY
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaC      [E] :   Coefficient partiel béton
        '   nEq         [E] :   Coefficient d'équivalence acier béton pour le béton d'enrobage
        '   mySection   [E] :   Section calculée
        '   lBetonTendu [E] :   Indique si on prend en compte le béton tendu (dans le cas d'un calcul de AlphaCri par exemple)
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim LargeurC, EpaisseurC, FdC, DeltaT As Decimal

        '--> Initialisation

        LargeurC = (mySection.LargeurEnrobagePartielBc - mySection.ProfilA.Tw)
        EpaisseurC = mySection.ProfilA.HauteurAmeHw
        FdC = mySection.Enrobage.Beton.Fck

        If lBetonTendu Then
            DeltaT = 1
        Else
            DeltaT = 0
        End If

        Me.AddMaille(LargeurC * EpaisseurC, EpaisseurC, -mySection.ProfilA.ha / 2, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.Rectangulaire)

        'Pour les profilés laminés, on doit retirer du béton la parties correspondant aux congés

        If mySection.lLamine Then

            '# Congés supérieurs

            Me.AddMailleConges(mySection.ProfilA.Rcs, -mySection.ProfilA.Tfs, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.CongeSup, -1)

            '# Congés supérieurs

            Me.AddMailleConges(mySection.ProfilA.Rci, -mySection.ProfilA.ha + mySection.ProfilA.Tfs, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.CongeInf, -1)

        End If

    End Sub

    Public Sub MaillageEnrobage_ZZ(GammaC As Decimal, nEq As Decimal, mySection As cls_Section, Optional ByVal lBetonTendu As Boolean = False)
        '-------------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier pour le calcul des propriétés / axe ZZ
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaC      [E] :   Coefficient partiel béton
        '   nEq         [E] :   Coefficient d'équivalence acier béton pour le béton d'enrobage
        '   mySection   [E] :   Section calculée
        '   lBetonTendu [E] :   Indique si on prend en compte le béton tendu (dans le cas d'un calcul de AlphaCri par exemple)
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim LargeurC, EpaisseurC, FdC, DeltaT As Decimal
        Dim Tw As Decimal = mySection.ProfilA.Tw

        '--> Initialisation

        LargeurC = (mySection.LargeurEnrobagePartielBc - Tw)
        EpaisseurC = mySection.ProfilA.HauteurAmeHw
        FdC = mySection.Enrobage.Beton.Fck

        If lBetonTendu Then
            DeltaT = 1
        Else
            DeltaT = 0
        End If

        Me.AddMaille(LargeurC * EpaisseurC / 2, LargeurC / 2, Tw / 2 + LargeurC / 4, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.Rectangulaire)
        Me.AddMaille(LargeurC * EpaisseurC / 2, LargeurC / 2, -Tw / 2 - LargeurC / 4, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.Rectangulaire)

        'Pour les profilés laminés, on doit retirer du béton la parties correspondant aux congés

        If mySection.lLamine Then

            '# Congés supérieurs (c'est à dire, côté gauche)

            Me.AddMailleConges(mySection.ProfilA.Rcs, -mySection.ProfilA.Tw / 2, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.CongeSup, -1)

            '# Congés supérieurs (c'est à dire, côté droite)

            Me.AddMailleConges(mySection.ProfilA.Rci, +mySection.ProfilA.Tw / 2, DeltaT, 1, nEq, FdC, 0.85, GammaC, cls_Maille.EnuTypeMaille.CongeInf, -1)

        End If

    End Sub

    Public Sub MaillageArmaturesEnrobage_YY(GammaS As Decimal, mySection As cls_Section)
        '-------------------------------------------------------------------------------------------------------------------
        '   24/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage du des armatures de l'enrobage pour le calcul des propriétés / axe YY
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaS      [E] :   Coefficient partiel pour les armatures
        '   mySection   [E] :   Section à traiter
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim zArma, PhiA As Decimal
        Dim iPos, iBarre As Integer
        Dim NbBarres As Integer
        Dim Fsk As Decimal = mySection.Enrobage.AcierArmatures.FsK
        Dim ArmaNeq As Decimal = cls_Acier.EYACIER / mySection.Enrobage.AcierArmatures.Es
        Const DELTACArma As Decimal = 0 ' pour le le moment on néglige les armatures comprimées
        Const NBMA As Integer = 2       ' Car symétrie des deux chambres
        'Dim lLitActif() As Boolean = {False, True, False}
        Dim lActif As Boolean

        '--> Boucle sur les lits d'armature

        For iArma As Integer = 0 To 2

            For iPos = 0 To 2

                NbBarres = mySection.Enrobage.LitArma(iArma).NbBarres(iPos)
                'lActif = lLitActif(iPos) Or Me.Enrobage.LitArma(iArma).lBarreActive(iArma, iPos)
                lActif = mySection.Enrobage.LitArma(iArma).lBarreActive(iArma, iPos)

                If lActif Then
                    For iBarre = 1 To NbBarres
                        zArma = mySection.zPosArmaEnrobage(iArma, iPos, iBarre)
                        PhiA = mySection.Enrobage.LitArma(iArma).PhiBarre(iPos)

                        Me.AddMailleCirculaire(PhiA / 2, zArma, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, NBMA, cls_Maille.EnuTypeMaille.Circulaire)

                    Next
                End If

            Next

        Next

    End Sub

    Public Sub MaillageArmaturesEnrobage_ZZ(GammaS As Decimal, mySection As cls_Section)
        '-------------------------------------------------------------------------------------------------------------------
        '   24/04/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Maillage du des armatures de l'enrobage pour le calcul des propriétés / axe ZZ
        '-------------------------------------------------------------------------------------------------------------------
        '   GammaS      [E] :   Coefficient partiel pour les armatures
        '   mySection   [E] :   Section à traiter
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PhiA As Decimal
        Dim iBarre As Integer
        Dim NbBarres As Integer
        Dim Fsk As Decimal = mySection.Enrobage.AcierArmatures.FsK
        Dim ArmaNeq As Decimal = cls_Acier.EYACIER / mySection.Enrobage.AcierArmatures.Es
        Const DELTACArma As Decimal = 0 ' pour le le moment on néglige les armatures comprimées

        Dim lLitActif() As Boolean = {False, True, False}
        Dim lActif As Boolean

        Dim yArmaExt As Decimal              'Position de la face interieure de la partie externe du cadre
        Dim yArmaInt As Decimal              'Position de la face interieure des armatures intérieures
        Dim yArmaMil As Decimal              'Position moyenne des armatures du milieu

        Dim yDecalArmaBord() As Decimal = {0.5, 1.5, 1}
        Dim yDecalArmaMil() As Decimal = {-0.5, +0.5}
        Dim kAdjustArmaMil() As Decimal = {0, 1}
        Dim Bf As Decimal = mySection.ProfilA.Bfs
        Dim Tw As Decimal = mySection.ProfilA.Tw
        Dim yBarre As Decimal

        '--> Initialisation

        yArmaExt = Bf * mySection.Enrobage.Ratio_bc / 2 - mySection.Enrobage.Etriers_EnrobageY - mySection.Enrobage.Etriers_Phi
        Select Case mySection.Enrobage.Etriers_Type
            Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                yArmaInt = Tw / 2 + mySection.Enrobage.Etriers_EnrobageY + mySection.Enrobage.Etriers_Phi
            Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant
                yArmaInt = Tw / 2
            Case cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                yArmaInt = Tw / 2 + mySection.Enrobage.Etriers_Phi
        End Select

        yArmaMil = (yArmaExt + yArmaInt) / 2

        '--> Boucle sur les lits d'armature

        For iArma As Integer = 0 To 2

            '# Armatures extérieures

            NbBarres = mySection.Enrobage.LitArma(iArma).NbExt
            lActif = lLitActif(iArma) Or mySection.Enrobage.LitArma(iArma).lActiveExt
            PhiA = mySection.Enrobage.LitArma(iArma).PhiExt

            If lActif And NbBarres > 0 Then

                For iBarre = 0 To NbBarres - 1
                    yBarre = yArmaExt - yDecalArmaBord(iBarre) * PhiA
                    Me.AddMailleCirculaire(PhiA / 2, yBarre, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, 1, cls_Maille.EnuTypeMaille.Circulaire)
                    Me.AddMailleCirculaire(PhiA / 2, -yBarre, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, 1, cls_Maille.EnuTypeMaille.Circulaire)
                Next

            End If

            '# Armatures du milieu

            If (iArma <> 1) Then
                NbBarres = mySection.Enrobage.LitArma(iArma).NbMil
                PhiA = mySection.Enrobage.LitArma(iArma).PhiMil

                For iBarre = 0 To NbBarres - 1
                    yBarre = yArmaMil + kAdjustArmaMil(NbBarres) * yDecalArmaMil(iBarre) * PhiA
                    Me.AddMailleCirculaire(PhiA / 2, yBarre, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, 1, cls_Maille.EnuTypeMaille.Circulaire)
                    Me.AddMailleCirculaire(PhiA / 2, -yBarre, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, 1, cls_Maille.EnuTypeMaille.Circulaire)
                Next
            End If


            '# Armatures internes

            NbBarres = mySection.Enrobage.LitArma(iArma).NbInt
            lActif = lLitActif(iArma) Or mySection.Enrobage.LitArma(iArma).lActiveInt
            PhiA = mySection.Enrobage.LitArma(iArma).PhiInt

            If lActif And NbBarres > 0 Then

                For iBarre = 0 To NbBarres - 1
                    yBarre = yArmaInt + yDecalArmaBord(iBarre) * PhiA
                    Me.AddMailleCirculaire(PhiA / 2, yBarre, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, 1, cls_Maille.EnuTypeMaille.Circulaire)
                    Me.AddMailleCirculaire(PhiA / 2, -yBarre, 1, DELTACArma, ArmaNeq, Fsk, 1, GammaS, 1, cls_Maille.EnuTypeMaille.Circulaire)
                Next

            End If

        Next
    End Sub

#End Region

End Class
