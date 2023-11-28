
Public Class cls_Connecteur

#Region " Attributs "

    ''' <summary>
    ''' Nom du goujon
    ''' </summary>
    Public nom As String

    ''' <summary>
    ''' Hauteur nominale
    ''' </summary>
    Public hsc As Decimal

    ''' <summary>
    ''' Hauteur de la tete du goujon 
    ''' </summary>
    Public h_tete As Decimal

    ''' <summary>
    ''' Diamètre du corps du goujon
    ''' </summary>
    Public d As Decimal

    ''' <summary>
    ''' Diametre de la tete du boulon
    ''' </summary>
    Public d_tete As Decimal

    ''' <summary>
    ''' Facteur de relaxation à définir par l'utilisateur pour la 2eme génération de l'EC4 uniquement
    ''' </summary>
    Public kcc As Decimal

    ''' <summary>
    ''' Limite d'élasticité
    ''' </summary>
    Public Fy As Decimal

    ''' <summary>
    ''' Résistance ultime à la traction
    ''' </summary>
    Public Fu As Decimal

    ''' <summary>
    ''' Indique si le goujon 
    ''' </summary>
    Public lCustom As Boolean = False 'indique si provient de la base personnelle

    Public lGoujonModifie As Boolean = False 'indique si le goujon a été modifié depuis la fenetre Frm_AddGoujon, permet de mettre a jour l'affichage du Frm_EditGoujon si besoin

    Public ReadOnly Property IndiceDataBase As Integer
        Get
            'For i As Integer = 0 To goujons_database.Length - 1
            '    If nom = goujons_database(i).Item1 Then Return i
            'Next
        End Get
    End Property

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.nom = "19-100"
        Me.kcc = 1
        'Caracteristiques_Goujons()
        Me.d = 0.019
        Me.hsc = 0.1
        Me.Fu = 450
        Me.Fy = 350
    End Sub

    Public Sub New(ByVal Nom As String, ByVal hsc As Double, ByVal PhiTige As Double,
                   ByVal PhiTete As Double, ByVal HTete As Double, ByVal fy As Double, ByVal fu As Double)

        '---------------------------------------------------------------------------------------
        '
        '   Constructeur pour connecteur soudé
        '
        '---------------------------------------------------------------------------------------

        Me.nom = Nom
        Me.hsc = hsc
        Me.h_tete = HTete
        Me.d_tete = PhiTete
        Me.d = PhiTige
        Me.Fu = fu
        Me.Fy = fy
    End Sub

#End Region

#Region " Fonction de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

#Region " Outils DataBase "
    ''' <summary>
    ''' Fonction qui renvoi la liste des noms des goujons disponibles dans la DataBase du Mod_Declaration (utile pour le Frm_Connexion)
    ''' </summary>
    ''' <returns></returns>
    'Public Function Get_ListName_GoujonDatabase() As String()
    '    Dim listName As String()
    '    ReDim listName(goujons_database.Length - 1)

    '    For i As Integer = 0 To goujons_database.Length - 1
    '        listName(i) = goujons_database(i).Item1
    '    Next

    '    Return listName

    'End Function

    Public Sub Caracteristiques_Goujons()
        Dim ind_en_cours As Integer = IndiceDataBase

        'Me.d = goujons_database(ind_en_cours).Item2
        'Me.hsc = goujons_database(ind_en_cours).Item3
        'Me.Fy = goujons_database(ind_en_cours).Item4
        'Me.Fu = goujons_database(ind_en_cours).Item5

    End Sub

#End Region

#Region " Outils divers "

    Public Sub DimensionsTete(ByRef d2 As Decimal, ByRef hk As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   07/08/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Renvoie les dimensions de la tête d'un connecteur
        '---------------------------------------------------------------------------------------------------------
        '   d2      [S] :   Diamètre de la tête
        '   hk      [S] :   Hauteur de la tête
        '---------------------------------------------------------------------------------------------------------

        Const kUnit As Decimal = 0.001

        Select Case CInt(Me.d * 1000)
            Case 16
                d2 = 32 * kUnit
                hk = 8 * kUnit
            Case 19
                d2 = 32 * kUnit
                hk = 10 * kUnit
            Case 22
                d2 = 35 * kUnit
                hk = 10 * kUnit
            Case 25
                d2 = 40 * kUnit
                hk = 12 * kUnit
        End Select

    End Sub

#End Region

#Region " Résistance connecteur Dalle Pleine Génération 1 "

    '--> 1ere GENERATION
    Public Function PRdDallePleineG1(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------
        '   TU : 

        '--> Déclaration

        Dim PRdC, PRdS As Decimal

        '--> Calcul

        PRdS = PRdDallePleineG1G2Acier(GammaVS)
        PRdC = PRdDallePleineG1Beton(Fck, Ecm, GammaVC)

        '--> Fin

        Return Math.Min(PRdC, PRdS)

    End Function

    '--> 1ere et 2eme GENERATIONS

    Public Function PRdDallePleineG1G2Acier(GammaVS As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation acier (6.18 de EN 1994-1-1)
        '   La formule est valable pour la 1ere génération et la 2eme génération 
        '-----------------------------------------------------------------------------------------------------------------
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '-----------------------------------------------------------------------------------------------------------------
        '   TU :
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal

        '--> Calcul

        PRd = 0.8 * Me.Fu * Math.PI * Me.d ^ 2 / (4 * GammaVS) * kConvMPaPa

        Return PRd
    End Function

    '--> 1ere GENERATION
    Public Function PRdDallePleineG1Beton(Fck As Decimal, Ecm As Decimal, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation béton (6.19 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal

        '--> Calcul

        PRd = 0.29 * Me.Alpha * Me.d ^ 2 * Math.Sqrt(Fck * Ecm) / GammaVC * kConvMPaPa

        Return PRd
    End Function

    Public ReadOnly Property Alpha As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation béton
        '   Coefficient de réduction Alpha dans l'équation béton (Eq(6.19) de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        Get
            Dim pAlpha As Decimal

            If hsc / d > 4 Then
                pAlpha = 1
            Else
                pAlpha = 0.2 * (hsc / d - 1)
            End If

            Return pAlpha
        End Get
    End Property

#End Region

#Region " Résistance connecteur Dalle Pleine Génération 2  "

    '--> 2eme GENERATION
    Public Function PRdDallePleineG2(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   18/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------
        '   TU : 

        '--> Déclaration

        Dim PRdC, PRdS As Decimal

        '--> Calcul

        PRdS = PRdDallePleineG1G2Acier(GammaVS)
        PRdC = PRdDallePleineG2Beton(Fck, Ecm, GammaVC)

        '--> Fin

        Return Math.Min(PRdC, PRdS)

    End Function

    '-->2eme GENERATION
    Public Function PRdDallePleineG2Beton(Fck As Decimal, Ecm As Decimal, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   18/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation béton (6.19 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal

        '--> Calcul

        PRd = 0.29 * Me.kcc * Me.d ^ 2 * Math.Sqrt(Fck * Ecm) / GammaVC * kConvMPaPa

        Return PRd
    End Function

#End Region

#Region " Résistance connecteur dalle mixte avec Bac perpendiculaire "

    '--> 1ere GENERATION

    Public Function CoefkT(nr As Decimal, MyBac As cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction kT pour un bac perpendiculaire
        '   (§ 6.6.5.1 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   nr      [E] :   Nombre de connecteurs / rangée (perp à l'axe de la poutre)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MykT As Decimal
        Dim b0 As Decimal = MyBac.LargeurB0
        Dim hP As Decimal = MyBac.Hp

        '--> Calcul

        MykT = 0.7 / Math.Sqrt(nr) * b0 / hP * (Me.hsc / hP - 1)

        Return Math.Min(MykT, Me.kTMax(nr, MyBac)) 'Corr GuD: Math.Max -> Math.Min

    End Function

    Public Function kTMax(nr As Decimal, MyBac As cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction kT pour un bac perpendiculaire
        '   (§ 6.6.5.1 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   nr      [E] :   Nombre de connecteurs / rangée (perp à l'axe de la poutre)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim myKtMax As Decimal

        '--> Calcul

        Select Case nr
            Case 1
                If MyBac.lPreperce Then
                    myKtMax = 0.75
                Else
                    If MyBac.Tp <= 0.001 Then
                        myKtMax = 0.85
                    Else
                        myKtMax = 1
                    End If
                End If
            Case 2
                If MyBac.lPreperce Then
                    myKtMax = 0.6
                Else
                    If MyBac.Tp <= 0.001 Then
                        myKtMax = 0.7
                    Else
                        myKtMax = 0.8
                    End If
                End If
        End Select

        Return myKtMax

    End Function

    Public Function PRdBacPerpendiculaireG1(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal, nr As Decimal, MyBac As cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   17/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine 
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '   nr      [E] :   Nombre de connecteurs / rangée (perp à l'axe de la poutre)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd, kt As Decimal

        '--> Calcul

        PRd = PRdDallePleineG1(Fck, Ecm, GammaVS, GammaVC)
        kt = CoefkT(nr, MyBac)

        '--> Fin

        Return kt * PRd

    End Function

    '--> 2eme GENERATION

    Public Function PRdBacPerpendiculaireG2(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal, nr As Decimal, MyBac As cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   17/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine 
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '   nr      [E] :   Nombre de connecteurs / rangée (perp à l'axe de la poutre)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd, kt As Decimal

        '--> Calcul

        PRd = PRdDallePleineG2(Fck, Ecm, GammaVS, GammaVC)
        kt = CoefkT(nr, MyBac)

        '--> Fin

        Return kt * PRd

    End Function

    Public Function PRdBacPerpendiculaireG2_AnnexeG(MyPoutre As cls_Poutre, nr As Integer, GammaVC As Decimal, GammaVS As Decimal)
        '-----------------------------------------------------------------------------------------------------------------
        '   02/08/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance PRd avec un bac perpendiculaire lorsque les conditions ne sont pas réunis -> Annexe G
        '-----------------------------------------------------------------------------------------------------------------
        '   GammaVS     [E] :   Coefficient partiel pour la première équation (acier)
        '   MyDalle     [E] :   Dalle béton
        '   nr          [E] :   Nombre de goujons disposés transversalement au droit du goujon
        '   GammaVC     [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------


        '--> Déclaration

        Dim PRdC, PRdS As Decimal

        '--> Calcul

        PRdS = PRdBacPerpendiculaireG2_AnnexeG_Acier(GammaVS)
        PRdC = PRdBacPerpendiculaireG2_AnnexeG_Beton(MyPoutre, nr, GammaVC)

        '--> Fin

        Return Math.Min(PRdC, PRdS)
    End Function

    Public Function PRdBacPerpendiculaireG2_AnnexeG_Acier(GammaVS As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   18/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance PRd,s avec un bac perpendiculaire lorsque les conditions ne sont pas réunis -> Annexe G
        '-----------------------------------------------------------------------------------------------------------------
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '-----------------------------------------------------------------------------------------------------------------
        '   TU :
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal

        '--> Calcul

        PRd = 0.58 * Me.Fu * Math.PI * Me.d ^ 2 / (4 * GammaVS) * kConvMPaPa

        Return PRd
    End Function

    Public Function PRdBacPerpendiculaireG2_AnnexeG_Beton(MyPoutre As cls_Poutre, nr As Integer, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   18/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance PRd,c avec un bac perpendiculaire lorsque les conditions ne sont pas réunis -> Annexe G
        '-----------------------------------------------------------------------------------------------------------------
        '   MyDalle     [E] :   Dalle béton
        '   nr          [E] :   Nombre de goujons disposés transversalement au droit du goujon
        '   GammaVC     [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal
        Dim hA, dp, C2, C2_min, C2_max, Wsc, Mpl_sc, sy, ku As Decimal
        Dim ny As Decimal

        '--> Calcul

        hA = hsc - MyPoutre.Dalle.Bac.Hp
        dp = 0.82 * MyPoutre.Dalle.Bac.Hp - d / 2

        C2_min = 1
        C2_max = 1.35
        C2 = 1.85 * MyPoutre.Dalle.Bac.Hp / MyPoutre.Dalle.Bac.LargeurB0
        C2 = Math.Max(C2, C2_min)
        C2 = Math.Min(C2, C2_max)

        If nr = 1 Then
            ny = 2
            sy = 0
        Else
            ny = Math.Min(1 + (hA - 2 * Me.d) / (0.52 * Me.d), 2)
            sy = 4 * d
        End If

        If MyPoutre.Dalle.Bac.lPreperce = False And MyPoutre.Dalle.Bac.Tp >= 0.001 Then
            ku = 1.25
        Else
            ku = 1
        End If

        Wsc = MyPoutre.Dalle.Bac.Bt ^ 2 / 6 * (2.4 * Me.hsc + (nr - 1) * sy) 'm3
        Mpl_sc = (1 / 6) * Me.Fu * Me.d ^ 3 * kConvMPaPa  'Valeur en N.m

        PRd = Me.kcc * C2 * ku / GammaVC * (MyPoutre.Dalle.beton.Fctk_005 * kConvMPaPa * Wsc / (MyPoutre.Dalle.Bac.Hp * nr) + ny * Mpl_sc / dp) 'N

        Return PRd
    End Function

#End Region

#Region " Résistance dalle mixte avec Bac parrallèle "
    '--> 1ere GENERATION
    Public Function PRdBacParrallelleG1(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal, MyBac As cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   17/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance avec un bac parallèle / Génération 1
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd, kl As Decimal

        '--> Calcul

        PRd = PRdDallePleineG1(Fck, Ecm, GammaVS, GammaVC)
        kl = CoefkL(MyBac)

        '--> Fin

        Return kl * PRd

    End Function

    '--> 2eme GENERATION
    Public Function PRdBacParrallelleG2(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal, MyBac As cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   17/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance avec un bac parallèle / Génération 2
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd, kl As Decimal

        '--> Calcul

        PRd = PRdDallePleineG2(Fck, Ecm, GammaVS, GammaVC)
        kl = CoefkL(MyBac)

        '--> Fin

        Return kl * PRd

    End Function

    Public Function CoefkL(MyBac As cls_Bac)
        '-----------------------------------------------------------------------------------------------------------------
        '   18/07/23 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction kL pour un bac parrallèles
        '   (§ 6.6.4.1 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim b0 As Decimal = MyBac.LargeurB0
        Dim hP As Decimal = MyBac.Hp
        Dim kLMax As Decimal = 1
        Dim kL As Decimal = 0.6 * b0 / hP * (hsc / hP - 1)

        Return Math.Min(kL, kLMax)

    End Function

#End Region

#Region " Resistance du connecteur - Fonction globale "

    Public Function ResistancePRd(lGeneration1 As Boolean, lDallePleine As Boolean, lPerpendiculaire As Boolean,
                                  MyBac As cls_Bac, nR As Integer,
                                  Fck As Decimal, Ecm As Decimal, gammaVS As Decimal, gammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   31/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Fonction générale pour retourner la résistance du connecteur
        '-----------------------------------------------------------------------------------------------------------------
        '   lGeneration1    [E] :   Indique si première ou seconde génération de l'EN
        '   lDallePleine    [E] :   Indique si dalle pleine ou dalle mixte
        '   lPerpendiculaire[E] :   Indique si bac perpendiculaire, pour les dalles mixtes
        '   MyBac           [E] :   Bac pour les dalles mixtes
        '   nR              [E] :   Nombre de connecteur par rangée
        '   FcK, Ecm        [E] :   Résistance à la compression et module élastique du béton
        '   gammaVS         [E] :   Coefficient partiel pour le terme lié à l'acier du connecteur
        '   gammaVC         [E] :   Coefficient partiel pour le terme lié au béton
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pPRd As Decimal = 0

        '--> Traitement

        If lGeneration1 Then
            '# Génération 1 de l'EN 

            If lDallePleine Then
                pPRd = Me.PRdDallePleineG1(Fck, Ecm, gammaVS, gammaVC)
            Else
                If lPerpendiculaire Then
                    pPRd = Me.PRdBacPerpendiculaireG1(Fck, Ecm, gammaVS, gammaVC, nR, MyBac)
                Else
                    pPRd = Me.PRdBacParrallelleG1(Fck, Ecm, gammaVS, gammaVC, MyBac)
                End If
            End If

        Else
            '# Génération 2 de l'EN 

            If lDallePleine Then
                pPRd = Me.PRdDallePleineG2(Fck, Ecm, gammaVS, gammaVC)
            Else
                If lPerpendiculaire Then

                    If Me.lAnnexeG(MyBac) Then
                        'pPRd = Me.PRdBacPerpendiculaireG2_AnnexeG(, nR, gammaVC, gammaVS)
                    Else
                        pPRd = Me.PRdBacPerpendiculaireG2(Fck, Ecm, gammaVS, gammaVC, nR, MyBac)
                    End If
                Else
                    pPRd = Me.PRdBacParrallelleG2(Fck, Ecm, gammaVS, gammaVC, MyBac)
                End If
            End If

        End If

        '--> Fin

        Return pPRd

    End Function

    Public Function lAnnexeG(MyBac As cls_Bac) As Boolean
        '-----------------------------------------------------------------------------------------------------------------
        '   31/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Indique si l'annexe G doit être utilisée pour le calcul du PRd, dans le cas d'une dalle mixte en Génération 2
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim plAnnexG As Boolean = False
        Const EkAnnexG As Decimal = 0.06
        Dim hAAnnexG As Decimal = 2.7 * Me.d

        '--> Traitement

        If MyBac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
            If MyBac.lNervuresOuvertes Then
                If IsSmaller(MyBac.LargeurBmoyenne, EkAnnexG) Then plAnnexG = True
                If IsSmaller(Me.hsc - MyBac.Hp, hAAnnexG) Then plAnnexG = True
            End If
        End If

        '--> Fin

        Return plAnnexG

    End Function

#End Region

End Class
