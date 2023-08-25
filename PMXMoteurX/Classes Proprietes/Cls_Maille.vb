Imports System.Security.Policy

Public Class Cls_Maille

#Region " Structures et définitions "
    Enum EnuTypeMaille
        Rectangulaire
        Circulaire
        CercleConcentre
        DemiCercle
        CongeSup
        CongeInf
    End Enum

#End Region

#Region " Paramètres de la classe "

    ''' <summary>
    ''' Aire de la maille
    ''' </summary>
    Public Aire As Decimal         ' Aire de la maille

    ''' <summary>
    ''' Nombre d'aires représentés par cette maille
    ''' </summary>
    Public Nombre As Integer

    ''' <summary>
    ''' Epaisseur pour les mailles rectangulaires
    ''' </summary>
    Public t As Decimal            ' Epaisseur 

    ''' <summary>
    ''' Rayon pour les mailles sphériques ou demi sphériques,
    ''' </summary>
    Public Rayon As Decimal

    Public xPos As Decimal          ' Position X du CdG
    ''' <summary>
    ''' Position Z
    ''' </summary>
    Public zPos As Decimal         ' Position Y cdg

    ''' <summary>
    ''' Coefficient d'équivalence
    ''' </summary>
    Public n As Decimal            ' Coefficient d'équivalence

    ''' <summary>
    ''' Indicateur pour la compression (0 si inactif en compression)
    ''' </summary>
    Public DeltaC As Decimal       ' Indicateur pour la compression (0 si inactif)

    ''' <summary>
    ''' Indicateur pour la traction (0 si inactif en traction)
    ''' </summary>
    Public DeltaT As Decimal       ' Indicateur pour la traction (0 si inactif)

    Private DeltaFunction(1) As Decimal

    ''' <summary>
    ''' Résistance caracteristique
    ''' </summary>
    Public Fk As Decimal           ' Résistance caracteristique

    ''''' <summary>
    ''''' Contrainte limite en comportement élastique
    ''''' </summary>
    ''Public Fel As Decimal           ' Contrainte limite en comportement élastique

    ''''' <summary>
    ''''' Contrainte limite en comportement plastique
    ''''' </summary>
    ''Public Fpl As Decimal           ' Contrainte limite en comportement plastique

    'Private tabF(1) As Decimal

    ''' <summary>
    ''' Effort normal plastique
    ''' </summary>
    Public Npl As Decimal          ' Effort normal plastique

    ''' <summary>
    ''' Type de maille
    ''' </summary>
    Public TypeM As EnuTypeMaille    ' Type de maille

    ''' <summary>
    ''' Coefficient partiel => 0 pour valeur de calcul et 1 pour valeur caractéristique
    ''' </summary>
    Private partialFactor(1) As Decimal

    ''' <summary>
    ''' Coefficient appliqué à la limite d'élasticité pour les analyses plastiques
    ''' </summary>
    Private kAnPlFy As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New(Aire As Decimal, t As Decimal, Rayon As Decimal, x As Decimal, y As Decimal, nEq As Decimal,
                   fk As Decimal, kPl As Decimal, Gamma As Decimal, DeltaC As Decimal, DeltaT As Decimal, Nombre As Integer, TypeMaille As EnuTypeMaille)

        Me.Aire = Aire
        Me.t = t
        Me.zPos = y
        Me.xPos = x
        Me.n = nEq
        Me.DeltaC = DeltaC
        Me.DeltaT = DeltaT
        Me.TypeM = TypeMaille
        Me.Fk = fk
        Me.kAnPlFy = kPl
        Me.DeltaFunction(1) = Me.DeltaC
        Me.DeltaFunction(0) = Me.DeltaT
        Me.Rayon = Rayon
        Me.Nombre = Nombre
        'Me.tabF(0) = Me.Fk
        'Me.tabF(1) = Me.kAnPlFy * Me.Fk
        Me.partialFactor(0) = Gamma
        Me.partialFactor(1) = 1

    End Sub

#End Region

#Region "  Outils calcul propriétés plastiques "

    Public Function ResultantePlastiqueM(Signe As Decimal, zAxe As Decimal, IndiceF As Integer) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Recherche de la résultante plastique d'un maillage (moment fléchissant)
        '-------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment pour le diagramme duquel on recherche la résultante
        '   zAxe        [S] :   Position de l'axe de référence
        '   IndiceF     [E] :   Indice : 0 pour résistance caractéristique et 1 pour résistance de calcul
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim DeltaZ As Decimal
        Dim p As Decimal                        'p est la proportion de la maille située AU-DESSUS de l'Axe

        ' Dim DeltaZP, DeltaZM As Decimal
        Dim DeltaAp, DeltaAm As Decimal

        Dim Result As Decimal

        Dim AireS, AireI As Decimal
        Dim zGs, zGi As Decimal
        Dim Phi As Decimal

        '--> Traitement

        DeltaZ = Me.zPos - zAxe
        p = Me.ProportionAuDessusZ(zAxe)
        DeltaAp = DeltaFunction(1 / 2 * (1 + Signe))
        DeltaAm = DeltaFunction(1 / 2 * (1 - Signe))

        Select Case Me.TypeM
            Case EnuTypeMaille.Rectangulaire

                Result = Me.Aire * (DeltaZ * (p * DeltaAp - (1 - p) * DeltaAm) + Me.t / 2 * (1 - p) * p * (DeltaAp + DeltaAm))

            Case EnuTypeMaille.CercleConcentre

                Result = Me.Aire * (DeltaZ * (p * DeltaAp - (1 - p) * DeltaAm) + Me.t / 2 * (1 - p) * p * (DeltaAp + DeltaAm))

            Case EnuTypeMaille.Circulaire

                DecoupeCerclePlein(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Result = (AireS * DeltaAp * (zGs + DeltaZ) - AireI * DeltaAm * (zGi + DeltaZ))

            Case EnuTypeMaille.CongeSup

                DecoupeCongesSup(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Result = (AireS * DeltaAp * (Me.zPos + zGs - zAxe - Me.Rayon) - AireI * DeltaAm * (Me.zPos + zGi - zAxe - Me.Rayon))

            Case EnuTypeMaille.CongeInf

                DecoupeCongesInf(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Result = (AireS * DeltaAp * (Me.zPos - zGs - zAxe + Me.Rayon) - AireI * DeltaAm * (Me.zPos - zGi - zAxe + Me.Rayon))

        End Select

        Return Result * Me.Nombre * kConvMPaPa * Me.kAnPlFy * Me.Fk / Me.partialFactor(IndiceF)

    End Function

    Public Function ResultantePlastiqueN(Signe As Decimal, zAxe As Decimal, IndiceF As Integer) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Recherche de la résultante plastique d'un maillage (effort axial)
        '-------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment pour le diagramme duquel on recherche la résultante
        '   zAxe        [S] :   Position de l'axe de référence
        '   IndiceF     [E] :   Indice : 0 pour résistance caractéristique et 1 pour résistance de calcul
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim DeltaZ As Decimal
        Dim p As Decimal                        'p est la proportion de la maille située AU-DESSUS de l'Axe

        ' Dim DeltaZP, DeltaZM As Decimal
        Dim DeltaAp, DeltaAm As Decimal

        Dim Result As Decimal

        Dim AireS, AireI As Decimal
        Dim zGs, zGi As Decimal
        Dim Phi As Decimal
        Const kConvMPatoPa As Decimal = 1000 ^ 2

        '--> Traitement

        DeltaZ = Me.zPos - zAxe
        p = Me.ProportionAuDessusZ(zAxe)
        DeltaAp = DeltaFunction(1 / 2 * (1 + Signe))
        DeltaAm = DeltaFunction(1 / 2 * (1 - Signe))

        Select Case Me.TypeM
            Case EnuTypeMaille.Rectangulaire

                Result = Me.Aire * (p * DeltaAp - (1 - p) * DeltaAm) * kConvMPatoPa

            Case EnuTypeMaille.CercleConcentre

                Result = Me.Aire * (p * DeltaAp - (1 - p) * DeltaAm) * kConvMPatoPa

            Case EnuTypeMaille.Circulaire

                DecoupeCerclePlein(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Result = (AireS * DeltaAp - AireI * DeltaAm) * kConvMPatoPa

            Case EnuTypeMaille.CongeSup

                DecoupeCongesSup(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Result = (AireS * DeltaAp - AireI * DeltaAm) * kConvMPatoPa

            Case EnuTypeMaille.CongeInf

                DecoupeCongesInf(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Result = (AireS * DeltaAp - AireI * DeltaAm) * kConvMPatoPa

        End Select

        Return Result * Me.Nombre * Me.kAnPlFy * Me.Fk / Me.partialFactor(IndiceF)

    End Function

#End Region

#Region "  Outils calcul propriétés élastiques "

    Public Sub MomentElastique(Signe As Decimal, zAxe As Decimal, Inertie As Decimal, IndexG As Integer, ByRef Mel As Decimal, ByRef lActive As Decimal)
        '-------------------------------------------------------------------------------
        '   24/08/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Calcul du moment élastique de la maille
        '-------------------------------------------------------------------------------
        '   Inertie     [E] :   Inertie de la section
        '   zAXE        [E] :   Position axe de référence
        '   Signe       [E] :   Signe du moment
        '   IndexG      [E] :   Index pour le coefficient partiel
        '   Mel         [S] :   Valeur du moment élastique
        '   lActive     [S] :   Indique si une valeur a pu être calculée
        '-------------------------------------------------------------------------------

        '--> Déclarations

        Dim vBord As Decimal
        Dim j As Integer
        Dim MelI As Decimal
        Dim lActiveI As Decimal

        '--> Initialisation 

        lActive = False
        Mel = 0

        '--> Traitement

        If Me.Nombre > 0 Then
            Select Case Me.TypeM
                Case EnuTypeMaille.Rectangulaire

                    For j = 0 To 1
                        vBord = Me.zPos + Me.t / 2 - j * t - zAxe
                        Mel_vBord(Signe, vBord, Inertie, IndexG, MelI, lActiveI)
                        If lActiveI Then
                            If lActive Then
                                Mel = Signe * Math.Min(Math.Abs(Mel), Math.Abs(MelI))
                            Else
                                Mel = MelI
                                lActive = True
                            End If
                        End If

                    Next

                Case EnuTypeMaille.Circulaire

                    For j = 0 To 1
                        vBord = Me.zPos + Me.Rayon / 2 - j * Rayon - zAxe
                        Mel_vBord(Signe, vBord, Inertie, IndexG, MelI, lActiveI)
                        If lActiveI Then
                            If lActive Then
                                Mel = Signe * Math.Min(Math.Abs(Mel), Math.Abs(MelI))
                            Else
                                Mel = MelI
                                lActive = True
                            End If
                        End If

                    Next

                Case EnuTypeMaille.CercleConcentre

                    vBord = Me.zPos - zAxe
                    Mel_vBord(Signe, vBord, Inertie, IndexG, MelI, lActiveI)
                    If lActiveI Then
                        If lActive Then
                            Mel = Signe * Math.Min(Math.Abs(Mel), Math.Abs(MelI))
                        Else
                            Mel = MelI
                            lActive = True
                        End If
                    End If

                Case EnuTypeMaille.CongeInf
                    For j = 0 To 1
                        vBord = Me.zPos + j * Me.Rayon - zAxe
                        Mel_vBord(Signe, vBord, Inertie, IndexG, MelI, lActiveI)
                        If lActiveI Then
                            If lActive Then
                                Mel = Signe * Math.Min(Math.Abs(Mel), Math.Abs(MelI))
                            Else
                                Mel = MelI
                                lActive = True
                            End If
                        End If

                    Next

                Case EnuTypeMaille.CongeSup

                    For j = 0 To 1
                        vBord = Me.zPos - j * Me.Rayon - zAxe
                        Mel_vBord(Signe, vBord, Inertie, IndexG, MelI, lActiveI)
                        If lActiveI Then
                            If lActive Then
                                Mel = Signe * Math.Min(Math.Abs(Mel), Math.Abs(MelI))
                            Else
                                Mel = MelI
                                lActive = True
                            End If
                        End If

                    Next

            End Select

        End If

    End Sub

    Private Sub Mel_vBord(Signe As Decimal, vBord As Decimal, Inertie As Decimal, IndexG As Integer, ByRef Meli As Decimal, ByRef lActive As Boolean)
        '-------------------------------------------------------------------------------
        '   24/08/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Calcul du moment élastique de la maille avec une distance v au bord de maille connue
        '-------------------------------------------------------------------------------
        '   Inertie     [E] :   Inertie de la section
        '   vBord       [E] :   Distance au bord de maille traité (v non nul)
        '   Signe       [E] :   Signe du moment
        '   IndexG      [E] :   Index pour le coefficient partiel
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim SigneSigma As Decimal
        Const EpsilonV As Decimal = 0.00001

        '--> Initialisation

        lActive = False
        Meli = 0

        '--> Traitement

        If Math.Abs(vBord) > EpsilonV Then
            SigneSigma = Math.Sign(Signe * vBord)

            If DeltaFunction(1 / 2 * (1 + SigneSigma)) > 0 Then
                'Cas où le bord de la maille est activée

                Meli = Signe * Me.Fk * Inertie * Me.n / (vBord * Me.partialFactor(IndexG))
                lActive = True

            End If
        End If

    End Sub


    ''' <summary>
    ''' Moment statique de la maille
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zAxe">     [S] Position ANE                </param>
    ''' <returns></returns>
    Public Function MomentStatique(Signe As Decimal, zAxe As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Recherche du moment statique d'un maillage
        '-------------------------------------------------------------------------------
        '   zAxe        [S] :   Position de l'axe de référence
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim DeltaZ As Decimal
        Dim p As Decimal                        'p est la proportion de la maille située AU-DESSUS de l'Axe

        Dim DeltaZP, DeltaZM As Decimal
        Dim DeltaAp, DeltaAm As Decimal

        Dim Mstat As Decimal

        Dim AireS, AireI As Decimal
        Dim zGs, zGi As Decimal
        Dim Phi As Decimal

        '--> Initialisation

        Mstat = 0

        '--> Traitement

        DeltaZ = Me.zPos - zAxe
        p = Me.ProportionAuDessusZ(zAxe)
        DeltaAp = DeltaFunction(1 / 2 * (1 + Signe))
        DeltaAm = DeltaFunction(1 / 2 * (1 - Signe))

        Select Case Me.TypeM
            Case EnuTypeMaille.Rectangulaire

                'p = Math.Max(Math.Min(1 / 2 + DeltaZ / Me.t, 1), 0)

                DeltaZP = DeltaZ + Me.t * (1 - p) / 2
                DeltaZM = DeltaZ - Me.t * (p) / 2

                Mstat = Me.Aire / Me.n * (DeltaZ * (p * DeltaAp + (1 - p) * DeltaAm) + Me.t / 2 * (1 - p) * p * (DeltaAp - DeltaAm))

            Case EnuTypeMaille.CercleConcentre

                'If DeltaZ > 0 Then p = 1 Else p = 0

                DeltaZP = DeltaZ + Me.t * (1 - p) / 2
                DeltaZM = DeltaZ - Me.t * (p) / 2

                Mstat = Me.Aire / Me.n * (DeltaZ * (p * DeltaAp + (1 - p) * DeltaAm))

            Case EnuTypeMaille.Circulaire

                DecoupeCerclePlein(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Mstat = (AireS * (Me.zPos + zGs - zAxe) * DeltaAp + AireI * (Me.zPos + zGi - zAxe) * DeltaAm) / Me.n

            Case EnuTypeMaille.CongeSup

                DecoupeCongesSup(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Mstat = (AireS * (Me.zPos + zGs - zAxe - Me.Rayon) * DeltaAp + AireI * (Me.zPos + zGi - zAxe - Me.Rayon) * DeltaAm) / Me.n

            Case EnuTypeMaille.CongeInf

                DecoupeCongesInf(Me.Rayon, p, AireS, zGs, AireI, zGi, Phi)

                Mstat = (AireS * (Me.zPos - zGs - zAxe + Me.Rayon) * DeltaAp + AireI * (Me.zPos - zGi - zAxe + Me.Rayon) * DeltaAm) / Me.n

        End Select

        Return Mstat * Me.Nombre

    End Function

    Private Sub DecoupeCongesInf(Rayon As Decimal, PropP As Decimal,
                                 ByRef AireT As Decimal, ByRef zGt As Decimal, ByRef AireB As Decimal, ByRef zGb As Decimal, ByRef Phi As Decimal)
        '-------------------------------------------------------------------------------------------
        '   26/04/2023 :    Création - POM
        '-------------------------------------------------------------------------------------------
        '   Calcul des parties d'un cercle plein de part et d'autre d'un cercle
        '-------------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon du cercle
        '   PropP       [E] :   Proportion du dercle au dessus de l'axe (en hauteur)
        '   AireT, zGt  [S] :   Aire et CdG / centre du cercle de la partie au dessus de l'axe
        '   AireB, zGb  [S] :   Aire et CdG / centre du cercle de la partie en dessous de l'axe
        '   Phi         [S] :   Angle définissant l'ouverture de la séparation entre les deux parties
        '-------------------------------------------------------------------------------------------
        '   zGt et zGb sont les distances entre le point de reference et les cdg des aires sup et inf
        '   Les aires sup et inf sont les aires des parties situées resp. au dessus et en dessous de l'Axe
        '   L'axe de reférence des éléments congés est l'axe de raccord avec la semelle
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim SinCube, SinPhi As Decimal
        Const EpsilonP As Decimal = 10 ^ (-6)

        '--> Calculs

        Phi = 2 * Math.Acos(PropP)
        SinPhi = Math.Sin(Phi)
        SinCube = (Math.Sin(Phi / 2)) ^ 3

        If PropP < EpsilonP Then
            zGt = Rayon : AireT = 0
        Else
            AireT = Rayon ^ 2 * (4 * PropP - Math.PI + Phi - SinPhi) / 2
            zGt = 2 * Rayon * (PropP ^ 2 - 2 / 3 * (1 - SinCube)) / (4 * PropP - Math.PI + Phi - SinPhi)
        End If

        If PropP > 1 - EpsilonP Then
            zGb = 0 : AireB = 0
        Else
            AireB = Rayon ^ 2 * (4 * (1 - PropP) - Phi + SinPhi) / 2
            zGb = 2 * Rayon * ((1 - PropP ^ 2) - 2 / 3 * SinCube) / (4 * (1 - PropP) - Phi + SinPhi)
        End If


    End Sub

    Private Sub DecoupeCongesSup(Rayon As Decimal, PropP As Decimal,
                                 ByRef AireT As Decimal, ByRef zGt As Decimal, ByRef AireB As Decimal, ByRef zGb As Decimal, ByRef Phi As Decimal)
        '-------------------------------------------------------------------------------------------
        '   26/04/2023 :    Création - POM
        '-------------------------------------------------------------------------------------------
        '   Calcul des parties d'un cercle plein de part et d'autre d'un cercle
        '-------------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon du cercle
        '   PropP       [E] :   Proportion du dercle au dessus de l'axe (en hauteur)
        '   AireT, zGt  [S] :   Aire et CdG / centre du cercle de la partie au dessus de l'axe
        '   AireB, zGb  [S] :   Aire et CdG / centre du cercle de la partie en dessous de l'axe
        '   Phi         [S] :   Angle définissant l'ouverture de la séparation entre les deux parties
        '-------------------------------------------------------------------------------------------
        '   zGt et zGb sont les distances entre le point de reference et les cdg des aires sup et inf
        '   Les aires sup et inf sont les aires des parties situées resp. au dessus et en dessous de l'Axe
        '   L'axe de reférence des éléments congés est l'axe de raccord avec la semelle
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim SinCube, SinPhi As Decimal
        Const EpsilonP As Decimal = 10 ^ (-6)

        '--> Calculs

        Phi = 2 * Math.Acos(1 - PropP)
        SinPhi = Math.Sin(Phi)
        SinCube = (Math.Sin(Phi / 2)) ^ 3

        If PropP < EpsilonP Then
            zGt = Rayon : AireT = 0
        Else
            AireT = Rayon ^ 2 * (4 * PropP - Phi + SinPhi) / 2
            zGt = 2 * Rayon * ((2 - PropP) * PropP - 2 / 3 * SinCube) / (4 * PropP - Phi + SinPhi)
        End If

        If PropP > 1 - EpsilonP Then
            zGb = 0 : AireB = 0
        Else
            AireB = Rayon ^ 2 * (4 * (1 - PropP) - Math.PI + Phi - SinPhi) / 2
            zGb = 2 * Rayon * ((1 - PropP) ^ 2 - 2 / 3 * (1 - SinCube)) / (4 * (1 - PropP) - Math.PI + Phi - SinPhi)
        End If

    End Sub

    Private Sub DecoupeCerclePlein(Rayon As Decimal, ProportionP As Decimal,
                                   ByRef AireS As Decimal, ByRef zGs As Decimal, ByRef AireI As Decimal, ByRef zGi As Decimal, ByRef Phi As Decimal)
        '-------------------------------------------------------------------------------------------
        '   26/04/2023 :    Création - POM
        '-------------------------------------------------------------------------------------------
        '   Calcul des parties d'un cercle plein de part et d'autre d'un cercle
        '-------------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon du cercle
        '   ProportionP [E] :   Proportion du dercle au dessus de l'axe (en hauteur)
        '   AireS, zGs  [S] :   Aire et CdG / centre du cercle de la partie au dessus de l'axe
        '   AireI, zGi  [S] :   Aire et CdG / centre du cercle de la partie en dessous de l'axe
        '   Phi         [S] :   Angle définissant l'ouverture de la séparation entre les deux parties
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        'Dim Aire As Decimal

        '--> Calculs

        Phi = 2 * Math.Acos(1 - 2 * ProportionP)

        AireS = Rayon ^ 2 / 2 * (Phi - Math.Sin(Phi))

        If Phi = 0 Then
            zGs = Rayon
        Else
            zGs = 4 / 3 * (Rayon * (Math.Sin(Phi / 2)) ^ 3) / (Phi - Math.Sin(Phi))
        End If

        AireI = Math.PI * Rayon ^ 2 - AireS

        If AireI = 0 Then
            zGi = -Rayon
        Else
            zGi = -zGs * AireS / AireI
        End If

    End Sub

    ''' <summary>
    ''' Retourne l'inertie YY de la maille
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment     </param>
    ''' <param name="zAxe">     [E] Position z de l'axe </param>
    ''' <returns></returns>
    Public Function InertieFlexionY(Signe As Decimal, zAxe As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Calcul de l'Inertie d'un maillage
        '-------------------------------------------------------------------------------
        '   Inertie     [S] :   Inertie de la section
        '   zAXE        [E] :   Position axe de référence
        '   Moment      [E] :   Signe du moment
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim Iy As Double
        Dim DeltaZ As Double

        Dim DeltaAp As Double
        Dim DeltaAm As Double

        Dim AireT, AireB As Decimal
        Dim IypT, IypB, IyS As Decimal
        Dim zGt, zGb As Decimal
        Dim Phi As Decimal

        '--> Initialisations

        Iy = 0

        Dim p As Double
        Dim DeltaZP As Double
        Dim DeltaZM As Double

        '--> Calcul

        DeltaZ = Me.zPos - zAxe
        p = Me.ProportionAuDessusZ(zAxe)
        DeltaAp = Me.DeltaFunction(1 / 2 * (1 + Signe))
        DeltaAm = Me.DeltaFunction(1 / 2 * (1 - Signe))

        Select Case Me.TypeM
            Case EnuTypeMaille.Rectangulaire

                'p = Math.Max(Math.Min(1 / 2 + DeltaZ / Me.t, 1), 0)

                DeltaZP = DeltaZ + Me.t * (1 - p) / 2
                DeltaZM = DeltaZ - Me.t * (p) / 2


                Iy = Me.Aire / Me.n * (DeltaAp * p + DeltaAm * (1 - p)) * DeltaZ ^ 2

                Iy = Me.Aire / Me.n * (DeltaAp * p * (DeltaZP ^ 2 + p ^ 2 * Me.t ^ 2 / 12) _
                                     + DeltaAm * (1 - p) * (DeltaZM ^ 2 + (1 - p) ^ 2 * Me.t ^ 2 / 12))

            Case EnuTypeMaille.CercleConcentre

                Iy = Me.Aire / Me.n

            Case EnuTypeMaille.Circulaire

                DecoupeCerclePlein(Me.Rayon, p, AireT, zGt, AireB, zGb, Phi)

                IyS = (Rayon ^ 4) * (Phi - 0.5 * Math.Sin(2 * Phi)) / 8
                IypT = IyS - AireT * (zGt) ^ 2
                IypB = Math.PI * (Rayon ^ 4) / 4 - IyS - AireB * (zGb) ^ 2

                Iy = (IypT + AireT * (Me.zPos + zGt - zAxe) ^ 2) * DeltaAp + (IypB + AireB * (Me.zPos + zGb - zAxe) ^ 2) * DeltaAm

            Case EnuTypeMaille.CongeSup

                DecoupeCongesSup(Me.Rayon, p, AireT, zGt, AireB, zGb, Phi)

                IypT = Me.Rayon ^ 4 * (2 * p * (p ^ 2 / 3 - p + 1) - (Phi - 0.5 * Math.Sin(2 * Phi)) / 8) - AireT * zGt ^ 2
                IypB = Me.Rayon ^ 4 * (2 / 3 * (1 - p) ^ 3 - (Math.PI - Phi + 0.5 * Math.Sin(2 * Phi)) / 8) - AireB * zGb ^ 2
                Iy = (IypT + AireT * (Me.zPos + zGt - zAxe - Me.Rayon) ^ 2) * DeltaAp + (IypB + AireB * (Me.zPos + zGb - zAxe - Me.Rayon) ^ 2) * DeltaAm

            Case EnuTypeMaille.CongeInf

                DecoupeCongesInf(Me.Rayon, p, AireT, zGt, AireB, zGb, Phi)

                IypT = Me.Rayon ^ 4 * (2 * p ^ 3 / 3 - (Math.PI - Phi + 0.5 * Math.Sin(2 * Phi)) / 8) - AireT * zGt ^ 2
                IypB = Me.Rayon ^ 4 * (2 / 3 * (1 - p) * (p ^ 2 + p + 1) - (Phi - 0.5 * Math.Sin(2 * Phi)) / 8) - AireB * zGb ^ 2
                Iy = (IypT + AireT * (Me.zPos - zGt - zAxe + Me.Rayon) ^ 2) * DeltaAp + (IypB + AireB * (Me.zPos - zGb - zAxe + Me.Rayon) ^ 2) * DeltaAm

        End Select
        Return Iy * Me.Nombre
    End Function



#End Region

#Region " Outils divers "

    ''' <summary>
    ''' Renvoie la proportion de la maille au dessus d'un axe
    ''' </summary>
    ''' <param name="zAxe"> [E] Position de l'axe</param>
    ''' <returns></returns>
    Private Function ProportionAuDessusZ(zAxe As Decimal) As Decimal

        Dim MyP As Decimal
        Dim DeltaZ As Double

        DeltaZ = Me.zPos - zAxe

        Select Case Me.TypeM
            Case EnuTypeMaille.Rectangulaire

                MyP = Math.Max(Math.Min(1 / 2 + DeltaZ / Me.t, 1), 0)

            Case EnuTypeMaille.CercleConcentre

                If DeltaZ > 0 Then MyP = 1 Else MyP = 0

            Case EnuTypeMaille.Circulaire

                MyP = Math.Max(Math.Min(1 / 2 + DeltaZ / (2 * Me.Rayon), 1), 0)

            Case EnuTypeMaille.CongeSup

                MyP = Math.Max(Math.Min(DeltaZ / Me.Rayon, 1), 0)

            Case EnuTypeMaille.CongeInf

                'MyP = Math.Max(Math.Min(1 - DeltaZ / Me.Rayon, 1), 0)
                MyP = Math.Max(Math.Min(1 + DeltaZ / Me.Rayon, 1), 0)

        End Select

        Return MyP
    End Function

    ''' <summary>
    ''' Renvoie la positions Z max d'une maille
    ''' </summary>
    Public Function ExtremaZMax() As Decimal
        Dim zMax As Decimal
        Select Case Me.TypeM
            Case EnuTypeMaille.CercleConcentre
                zMax = Me.zPos
            Case EnuTypeMaille.Circulaire
                zMax = Me.zPos + Me.Rayon / 2
            Case EnuTypeMaille.CongeInf
                zMax = Me.zPos + Me.Rayon
            Case EnuTypeMaille.CongeSup
                zMax = Me.zPos
            Case EnuTypeMaille.Rectangulaire
                zMax = Me.zPos + Me.t / 2
        End Select
        Return zMax
    End Function

    ''' <summary>
    ''' Renvoie la positions Z min d'une maille
    ''' </summary>
    Public Function ExtremaZMin() As Decimal
        Dim zMin As Decimal
        Select Case Me.TypeM
            Case EnuTypeMaille.CercleConcentre
                zMin = Me.zPos
            Case EnuTypeMaille.Circulaire
                zMin = Me.zPos - Me.Rayon / 2
            Case EnuTypeMaille.CongeInf
                zMin = Me.zPos
            Case EnuTypeMaille.CongeSup
                zMin = Me.zPos - Me.Rayon
            Case EnuTypeMaille.Rectangulaire
                zMin = Me.zPos - Me.t / 2
        End Select
        Return zMin
    End Function

#End Region

End Class
