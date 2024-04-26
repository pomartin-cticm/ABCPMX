Imports System.Collections.Specialized.BitVector32
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Security.Policy

Public Class cls_Section

#Region " Enumérations et structures "

    Structure strucAcierLocal
        Dim Nuance As String
        Dim Qualite As String
        Dim Reduc As String
        Dim lAvailable As Boolean
    End Structure

    Public Enum Enum_TypeSection
        AcierSeul           ' Section acier
        AcierSeulEnrobage   ' Section acier avec enrobage partiel
        Mixte           ' Section mixte acier-béton
        MixteEnrobage   ' Section mixte acier-béton avec enrobage partiel
        SFB             ' Section Slim floor SFB non mixte
        SFBmixte        ' Section Slim floor SFB mixte
        IFB_A           ' Section Slim floor IFB-A non mixte
        IFB_Amixte      ' Section Slim floor IFB-A mixte
        IFB_B           ' Section Slim floor IFB-B non mixte
        IFB_Bmixte      ' Section Slim floor IFB-B mixte
        SAB             ' Section Slim floor SAB non mixte
        SABmixte        ' Section Slim floor SAB mixte
    End Enum

#End Region

#Region " Déclarations "


#End Region

#Region " Attributs généraux "


    Public Nom As String                        ' Nom de la section

    Public lDalleBeton As Boolean               ' Indique si l'utilisateur a défini une dalle de béton
    Public lDatabase As Boolean                 ' Indique si la section est définie par une base de données

    Private m_typeSection As Enum_TypeSection      ' Type de section

    ''' <summary>
    ''' Type de section de la poutre
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeSection As Enum_TypeSection
        Get
            Return Me.m_typeSection
        End Get
        Set(value As Enum_TypeSection)
            Me.m_typeSection = value
        End Set
    End Property

#End Region

#Region " Attributs de définition de la section "

    Public ProfilA As New cls_ProfilA           ' Profilé métallique

    Public Acier As New cls_Acier               ' Acier du profilé
    Public AcierSPD As New cls_Acier            ' Acier de la plaque soudée

    ''' <summary>
    ''' Indique si les valeurs de fy sont renseignées directement par l'utilisateur (True) ou via les plages (False)
    ''' </summary>
    Public lUser As Boolean = False

    ''' <summary>
    ''' valeur nominale de la limite d'élasticité de la poutre (Pa = N/m²)
    ''' /!\ fysp n'est a priori utile que pour les slimfloors SFB (a discuter) /!\
    ''' </summary>
    Public f_y As (w As Decimal, fs As Decimal, fi As Decimal, spd As Decimal)

    Public Enrobage As New cls_Enrobage_Partiel ' Enrobage (pour les profilé enrobés)

#End Region

#Region " Propriétés (Méthodes) "

    ''' <summary>
    ''' Retourne la masse linéique du profilé (/!\ valeur retournée en kg/m /!\)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MassLineiqueProfilA As Decimal
        Get
            Dim mass As Decimal
            mass = Me.ProfilA.Aire * Me.Acier.Rho
            Return mass
        End Get
    End Property

    ''' <summary>
    ''' Renvoi la classe de la section acier seule en considérant que la section est soumise à un effort de compression pure ou une flexion pure
    ''' <param name="lCompressionPure">indique si on réalise le calcul en compression pure (True) ou en flexion pure (False)</param>
    ''' </summary>
    ''' <returns></returns>
    Public Function ClasseProfilAcierSeulCompressionPureFlexionPure(lCompressionPure As Boolean, lG1_EN As Boolean) As Integer

        'Déclaration
        Dim classeLoc As Integer
        Dim zAN As Decimal
        Dim lFlexionPositive As Boolean = True
        Dim lBetonSlimfloor As Boolean = False
        Dim lBetonEnrobage As Boolean = False

        'Initialisation
        If lCompressionPure Then
            zAN = -1000 * Me.ProfilA.ha
        Else
            zAN = Me.ProfilA.zCdG
        End If

        classeLoc = Me.ClasseSection(zAN, zAN, lFlexionPositive, lBetonSlimfloor, lBetonEnrobage, lG1_EN)

        Return classeLoc
    End Function

    Public Function NResistanceArmaturesEnrobage(GammaS As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------
        '   14/02/2024 :  Création - GUD
        '---------------------------------------------------------------------------------------------
        '   Calcul de la résistance à la traction des armatures du béton d'enrobage
        '---------------------------------------------------------------------------------------------
        '   GammaS  [E] :   Coefficient partiel pour les armatures
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pNArma As Decimal = 0

        '--> Calcul

        For i As Integer = 0 To Enrobage.LitArma.Count - 1
            If Enrobage.LitArma(i).lActiveExt Then pNArma += Enrobage.LitArma(i).NbExt * Math.PI * Enrobage.LitArma(i).PhiExt ^ 2 / 4 * Enrobage.AcierArmatures.FsK / GammaS
            pNArma += Enrobage.LitArma(i).NbMil * Math.PI * Enrobage.LitArma(i).PhiMil ^ 2 / 4 * Enrobage.AcierArmatures.FsK / GammaS
            If Enrobage.LitArma(i).lActiveInt Then pNArma += Enrobage.LitArma(i).NbInt * Math.PI * Enrobage.LitArma(i).PhiInt ^ 2 / 4 * Enrobage.AcierArmatures.FsK / GammaS
        Next

        '--> Fin

        Return 2 * pNArma * kConvMPaPa 'le x2 provient du fait que l'on a 2 chambres

    End Function

    Public Function NResistanceCompressionEnrobage(GammaC As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------
        '  14/02/2024 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul de la résistance à la compresion de la dalle
        ' ##ZZZ à compléter pour génération 2
        '---------------------------------------------------------------------------------------------
        '   Beff    [E] :   Largeur participante de la dalle
        '   GammaC  [E] :   Coefficient partiel pour le béton
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pNDalle As Decimal = 0
        Dim kDalle As Decimal = 0.85

        '--> Calcul

        pNDalle = Enrobage.Ratio_bc * Math.Min(Me.ProfilA.Bfs, Me.ProfilA.Bfi) * Me.Enrobage.Beton.Fck * kDalle / GammaC

        '--> Fin

        Return pNDalle * kConvMPaPa
    End Function

#End Region

#Region " Propriétés plastiques de la section "

    Public Sub ProprietesPlastiquesMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, RhoV As Decimal,
                                       ByRef zANP As Decimal, ByRef MplRd As Decimal, Optional ByVal lProfileAcierUniquement As Boolean = False)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques en flexion simple de la section / axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   zANP        [S] :   Position axe neutre plastique
        '   MplRd       [S] :   Moment plastique
        '   bEff        [E] :   Largeur efficace de la dalle (si secion mixte)
        '   Eta         [E] :   Degré de connexion (si section mixte)
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Dim nEqEc As Decimal = 1 'on applique 1 car calcul plastique 

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '# Béton d'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageEnrobage_YY(Gammas.GammaC, nEqEc, Me)

        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage And Not lProfileAcierUniquement Then

            MyModele.MaillageArmaturesEnrobage_YY(Gammas.GammaS, Me)

        End If

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub



    Public Sub ProprietesPlastiquesMixteMyyEta(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, RhoV As Decimal,
                                               bEff As Decimal, DeltaRd As Decimal, MyDalle As cls_Dalle,
                                               ByRef zANP As Decimal, ByRef MplRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques en flexion simple de la section / axe fort prenant en compte la mixité avec la dalle
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   bEff        [E] :   Largeur efficace de la dalle (si secion mixte)
        '   DeltaRd     [E] :   Cumul des résistance des connecteurs de la dalle jusqu'au point de moment nul (si section mixte)
        '   MyDalle     [E] :   Dalle
        '   zANP        [S] :   Position axe neutre plastique
        '   MplRd       [S] :   Moment plastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine

        Dim nEqEc As Decimal = 1            ' On Applique 1 car calcul plastique
        Const nEqD As Decimal = 1           ' Idem
        'Dim NPro As Decimal

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw
        'NPro = Me.ResistanceTractionProfile(Gammas.GammaM0)

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '# Béton d'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageEnrobage_YY(Gammas.GammaC, nEqEc, Me)

        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageArmaturesEnrobage_YY(Gammas.GammaS, Me)

        End If

        '--> Dalle béton

        If lMixte And (bEff > 0) Then

            '# Dalle 

            MyModele.MaillageDalle_YYETA(Gammas.GammaC, bEff, nEqD, DeltaRd, MyDalle)

            '# Armatures

            MyModele.MaillageArmaturesDalle_YY(Gammas.GammaS, bEff, MyDalle)

        End If

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub


    Public Sub ProprietesPlastiquesMixteMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, RhoV As Decimal,
                                            bEff As Decimal, MyDalle As cls_Dalle,
                                            ByRef zANP As Decimal, ByRef MplRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques en flexion simple de la section / axe fort prenant en compte la mixité avec la dalle
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   bEff        [E] :   Largeur efficace de la dalle (si secion mixte)
        '   Eta         [E] :   Degré de connexion (si section mixte)
        '   MyDalle     [E] :   Dalle
        '   zANP        [S] :   Position axe neutre plastique
        '   MplRd       [S] :   Moment plastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine

        Dim nEqEc As Decimal = 1            ' On Applique 1 car calcul plastique
        Const nEqD As Decimal = 1           ' Idem

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '# Béton d'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageEnrobage_YY(Gammas.GammaC, nEqEc, Me)

        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageArmaturesEnrobage_YY(Gammas.GammaS, Me)

        End If

        '--> Dalle béton

        If lMixte And (bEff > 0) Then

            '# Dalle 

            MyModele.MaillageDalle_YY(Gammas.GammaC, bEff, nEqD, MyDalle)

            '# Armatures

            MyModele.MaillageArmaturesDalle_YY(Gammas.GammaS, bEff, MyDalle)

        End If

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Public Function ResistanceTractionProfile(gammaM0 As Decimal) As Decimal
        '-------------------------------------------------------------------------------------------------------------------
        '   31/10/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance à la traction du profilé métallique seul
        '-------------------------------------------------------------------------------------------------------------------
        '   gammaM0     [E] :   Coefficient partiel pour l'acier
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pNPro As Decimal = 0
        Dim Afs, Afi, Aspd As Decimal

        '--> Initialisation

        Afs = Me.ProfilA.AireFs
        Afi = Me.ProfilA.AireFi
        Aspd = Me.ProfilA.AirePlat 'Ajout GuD pour couvrir le cas des slimfloor qui possèdent un plat soudé

        '--> Calcul

        pNPro = Afs * Me.FySup
        pNPro += Afi * Me.FyInf
        pNPro += Aspd * Me.FySpd
        pNPro += (Me.ProfilA.Aire - Afi - Afs) * Me.FyW

        '--> Fin

        Return pNPro / gammaM0 * kConvMPaPa

    End Function

#End Region

#Region " Propriétés élastiques de la section "

    Public Function InertieYYMixteSlip(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, nEqEc As Decimal,
                                       nEqDal As Decimal, Beff As Decimal, MyDalle As cls_Dalle,
                                       Le As Decimal, EYoung As Decimal, cStiff As Decimal, ByRef zANE As Decimal) As Decimal
        '-------------------------------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '   Propriétés pour une poutre mixte. Inertie prenant en compte le glissement de la connexion
        '   Selon Formule prEN 1994-1-1, 9.3.1 (5)
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   nEqEc       [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   lDalle      [E] :   Position axe neutre élastique
        '   nEqDal      [E] :   Coefficient d'équivalence acier béton pour la dalle
        '   Beff        [E] :   Largeur efficace de la dalle
        '   MyDalle     [E] :   Elément dalle
        '   Le          [E] :   Longueur entre points de moments nuls
        '   EYoung      [E] :   Module d'Young de l'acier
        '   cStiff      [E] :   Raideur de la connexion
        '-------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim InertieY, MelRd As Decimal
        Dim Iya, IycH As Decimal
        Dim IConnex As Decimal
        Dim AcEff, Aa, Ac, zcH As Decimal
        Dim Tc As Decimal
        Dim nEff As Decimal

        '--( Initialisation

        Tc = MyDalle.EpaisseurActive

        '--( Application

        Me.ProprietesElastiquesMyy(Signe, lValeurRd, Gammas, nEqEc, zANE, Iya, MelRd)
        IycH = Beff * Tc ^ 3 / 12 / nEqDal
        zcH = MyDalle.zTop - Tc / 2 - zANE
        Ac = Beff * Tc

        nEff = nEqDal * (1 + Math.PI ^ 2 * EYoung * kConvMPaPa * (Ac / nEqDal) / (Le ^ 2 * cStiff))

        Aa = Me.ProfilA.Aire
        AcEff = Ac / nEff

        IConnex = AcEff * Aa * zcH ^ 2 / (AcEff + Aa)

        InertieY = Iya + IycH + IConnex

        Return InertieY

    End Function

    Public Function InertieYY(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, nEqEc As Decimal,
                              lDalle As Boolean, nEqDal As Decimal, Beff As Decimal, MyDalle As cls_Dalle, ByRef zANE As Decimal) As Decimal
        '-------------------------------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   nEqEc       [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   lDalle      [E] :   Position axe neutre élastique
        '   nEqDal      [E] :   Coefficient d'équivalence acier béton pour la dalle
        '   Beff        [E] :   Largeur efficace de la dalle
        '   MyDalle     [E] :   Elément dalle
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim InertieY, MelRd As Decimal

        '--> Traitement

        If lDalle Then
            Me.ProprietesElastiquesMixteMyy(Signe, lValeurRd, Gammas, nEqEc, nEqDal, Beff, MyDalle, zANE, InertieY, MelRd)
        Else
            Me.ProprietesElastiquesMyy(Signe, lValeurRd, Gammas, nEqEc, zANE, InertieY, MelRd)
        End If

        Return InertieY

    End Function

    Public Sub ProprietesElastiquesAcierMyy(lValeurRd As Boolean, Gammas As cls_Gamma,
                                            ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef MelRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '   Pour une section acier, sans enrobage partiel
        '-------------------------------------------------------------------------------------------------------------------
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   zANE        [S] :   Position axe neutre élastique
        '   InertieY    [S] :   Inertie de flexion / axe fort
        '   MelRd       [S] :   Moment élastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        Const Signe As Decimal = 1

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieY, lValeurRd)

    End Sub

    Public Sub ProprietesElastiquesMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, nEqEc As Decimal,
                                       ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef MelRd As Decimal, Optional ByVal lProfileAcierUniquement As Boolean = False, Optional ByVal lCalculAlphaCr As Boolean = False)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '   ON NE PREND PAS EN COMPTE LA DALLE DANS LE CAS D'UNE SECTION MIXTE
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe                   [E] :   Signe du moment
        '   lValeurRd               [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas                  [E] :   Coefficients partiels
        '   nEqEc                   [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   zANE                    [S] :   Position axe neutre élastique
        '   InertieY                [S] :   Inertie de flexion / axe fort
        '   MelRd                   [S] :   Moment élastique
        '   lProfileAcierUniquement [E] :   Indique si on calcul les propriétés élastiques en ne tenant compte que du profilé acier (True) ou si on prend en compte également le béton d'enrobage (False)
        '   lCalculAlphaCr          [E] :   Indique si les propriétés élastiques selon l'axe ZZ sont utilisées pour le calcul de alpha critique (True) ou non (False)  
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        ' Dim LargeurC, EpaisseurC, FdC As Decimal

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '# Béton d'enrobage

        If Me.lEnrobage And Not lProfileAcierUniquement Then

            MyModele.MaillageEnrobage_YY(Gammas.GammaC, nEqEc, Me, lCalculAlphaCr)

        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage And Not lProfileAcierUniquement And Not lCalculAlphaCr Then

            MyModele.MaillageArmaturesEnrobage_YY(Gammas.GammaS, Me)

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieY, lValeurRd)

    End Sub

    Public Sub ProprietesElastiquesMixteMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, nEqEc As Decimal, nEqDalle As Decimal,
                                            bEff As Decimal, myDalle As cls_Dalle,
                                            ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef MelRd As Decimal,
                                            Optional lPriseEnCompteDalle As Boolean = True)
        '-------------------------------------------------------------------------------------------------------------------
        '   17/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort - Section mixte
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   nEqEc       [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   nEqDalle    [E] :   Coefficient d'équivalence acier béton pour la dalle
        '   bEff        [E] :   Largeur efficace de la dalle (si secion mixte)
        '   MyDalle     [E] :   Dalle
        '   zANE        [S] :   Position axe neutre élastique
        '   InertieY    [S] :   Inertie de flexion / y
        '   MelRd       [S] :   Moment élastique
        '   lPriseEnCompteDalle[E] :    Indique si pour une poutre mixte, on prend ou pas en compte la dalle
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        'Dim LargeurC, EpaisseurC, FdC As Decimal

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '# Béton d'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageEnrobage_YY(Gammas.GammaC, nEqEc, Me)

        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageArmaturesEnrobage_YY(Gammas.GammaS, Me)

        End If

        '--> Dalle béton

        If Me.lMixte And (bEff > 0) And lPriseEnCompteDalle Then

            '# Dalle

            MyModele.MaillageDalle_YY(Gammas.GammaC, bEff, nEqDalle, myDalle)

            '# Armatures

            MyModele.MaillageArmaturesDalle_YY(Gammas.GammaS, bEff, myDalle)

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieY, lValeurRd)

    End Sub

    Public Sub ProprietesElastiquesMzz(Signe As Decimal, lValeurRd As Boolean, Gammas As cls_Gamma, nEqEc As Decimal,
                                       ByRef zANE As Decimal, ByRef InertieZ As Decimal, ByRef MelRd As Decimal, Optional ByVal lCalculAlphaCr As Boolean = False)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe faible
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe               [E] :   Signe du moment
        '   lValeurRd           [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas              [E] :   Coefficients partiels
        '   lCalculAlphaCr      [E] :   Indique si les propriétés élastiques selon l'axe ZZ sont utilisées pour le calcul de alpha critique (True) ou non (False)
        '   zANE                [S] :   Position axe neutre élastique
        '   MelRd               [S] :   Moment élastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        'Dim Rc As Decimal = (Me.ProfilA.Rcs + Me.ProfilA.Rci) / 2

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_ZZ(Gammas.GammaM0, RhoV, ProfilA, FySup, FyInf, FyW, FySpd)

        '# Béton d'enrobage

        If Me.lEnrobage Then

            MyModele.MaillageEnrobage_ZZ(Gammas.GammaC, nEqEc, Me, lCalculAlphaCr)

        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage And Not lCalculAlphaCr Then

            MyModele.MaillageArmaturesEnrobage_ZZ(Gammas.GammaS, Me)

        End If

        '--> Dalle béton

        ' PAS DANS CETTE ROUTINE

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieZ = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieZ, lValeurRd)

    End Sub

    Public Function InertieTorsionProfileEnrobe(Neq As Decimal) As Decimal
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en torsion de la section avec profilé partiellent enrobé
        '   Pour un profilé acier avec enrobage, on utilise la formule du guide "Déversement des poutres en acier"
        '   Pas de prise en compte de la dalle
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pInertieT As Decimal
        Dim pInertieTEnrob As Decimal
        Dim Gc, Ga As Decimal
        Dim hW, Bc As Decimal

        '--> Inertie de torsion du profilé acier seul

        pInertieT = Me.ProfilA.InertieT

        '--> Pour l'enrobage, on ajoute la contribution du béton d'enrobage, avec la formule du guide "Déversement des poutres en acier", page 56 formule (4.19)

        hW = Me.ProfilA.HauteurAmeHw
        Bc = Me.LargeurEnrobagePartielBc

        Gc = 0.3 * cls_Acier.EYACIER / Neq
        Ga = Me.Acier.ModuleG

        pInertieTEnrob = 1 / 3 * (1 - 0.63 * Bc / hW) * hW * Bc ^ 3

        pInertieT += 0.1 * pInertieTEnrob * Gc / Ga

        Return pInertieT
    End Function

    Public Function InertieT() As Decimal
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en torsion de la section
        '   Pour un profilé acier avec enrobage, on utilise la formule du guide "Déversement des poutres en acier"
        '   Pas de prise en compte de la dalle
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pInertieT As Decimal
        Dim nEq As Decimal

        '--> Inertie de torsion du profilé acier seul

        pInertieT = Me.ProfilA.InertieT

        '--> Pour l'enrobage, on ajoute la contribution du béton d'enrobage, avec la formule du guide "Déversement des poutres en acier", page 56 formule (4.19)

        If Me.lEnrobage Then

            nEq = Me.Enrobage.Beton.CoefficientEquivalenceCT
            'hW = Me.ProfilA.HauteurAmeHw
            'Bc = Me.LargeurEnrobagePartielBc

            'Gc = 0.3 * cls_Acier.EYACIER / nEq
            'Ga = Me.Acier.ModuleG

            'pInertieTEnrob = 1 / 3 * (1 - 0.63 * Bc / hW) * hW * Bc ^ 3

            'pInertieT += 0.1 * pInertieTEnrob * Gc / Ga
            pInertieT = InertieTorsionProfileEnrobe(nEq)

        End If

        Return pInertieT

    End Function

#End Region

#Region " Propiétés générales de la section "

    ''' <summary>
    ''' Calcul et renvoi la hauteur d'enrobage du profilé acier dans le cas d'une slim floor
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property hec As Decimal
        Get
            Select Case Me.TypeSection
                Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte
                    Return Me.ProfilA.hb
                Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte
                    Return Me.ProfilA.ha - Me.ProfilA.Plat_t
                Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte
                    Return Me.ProfilA.ha - Me.ProfilA.Tfi
                Case cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SABmixte
                    Return Me.ProfilA.ha - Me.ProfilA.Tfi
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Indique si la section est slimfloor ou non
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property lSlimFloor As Boolean
        Get
            Return Not (Me.TypeSection = cls_Section.Enum_TypeSection.AcierSeul _
                     Or Me.TypeSection = cls_Section.Enum_TypeSection.AcierSeulEnrobage _
                     Or Me.TypeSection = cls_Section.Enum_TypeSection.Mixte _
                     Or Me.TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage)
        End Get
    End Property

    ''' <summary>
    ''' Indique si la section comprend un enrobage partiel
    ''' </summary>
    Public ReadOnly Property lEnrobage As Boolean
        Get
            Return (Me.TypeSection = Enum_TypeSection.AcierSeulEnrobage) Or (Me.TypeSection = Enum_TypeSection.MixteEnrobage)
        End Get
    End Property

    ''' <summary>
    ''' Indique si la section comprend une dalle connectée au profilé
    ''' </summary>
    Public ReadOnly Property lMixte As Boolean
        Get
            Return (Me.TypeSection = Enum_TypeSection.Mixte) Or (Me.TypeSection = Enum_TypeSection.MixteEnrobage) Or (Me.TypeSection = Enum_TypeSection.IFB_Amixte) Or (Me.TypeSection = Enum_TypeSection.IFB_Bmixte)
        End Get
    End Property

    ''' <summary>
    ''' Indique si la section comprend un profilé laminé
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property lLamine As Boolean
        Get
            Return (Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine) Or (Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB) Or (Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB)
        End Get
    End Property

    Public ReadOnly Property LargeurEnrobagePartielBc As Decimal
        Get
            Return Me.ProfilA.Bfs * Me.Enrobage.Ratio_bc
        End Get
    End Property

    Public Function NotionalSizeEnrobage() As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie le rayon moyen pour le calcul de fluage, pour le béton d'enrobage
        '------------------------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------------------------

        Return Me.AireEnrobagePartielAec / Me.ProfilA.HauteurAmeHw

    End Function

    Public ReadOnly Property AireEnrobagePartielAec As Decimal
        Get
            Dim Aec As Decimal
            If Me.lEnrobage Then
                Aec = Me.ProfilA.HauteurAmeHw * (Me.LargeurEnrobagePartielBc - ProfilA.Tw) - (4 - Math.PI) * ProfilA.Rcs ^ 2 / 2 - (4 - Math.PI) * ProfilA.Rci ^ 2 / 2
            Else
                Aec = 0
            End If

            Return Aec
        End Get
    End Property


    Public Function VplRd(GammaM0 As Decimal) As Decimal

        Dim MyVRd As Decimal = 0

        Select Case Me.TypeSection
            Case Enum_TypeSection.AcierSeul, Enum_TypeSection.AcierSeulEnrobage, Enum_TypeSection.Mixte, Enum_TypeSection.MixteEnrobage
                MyVRd = Me.AireAv * Me.FyW / (Math.Sqrt(3) * GammaM0) * kConvMPaPa

        End Select

        Return MyVRd
    End Function

    Public Function VbRd(GammaM1 As Decimal, EtaW As Decimal, lMontantRigid As Boolean) As Decimal
        '---------------------------------------------------------------------------------------------------------
        '   01/12/23 :  Création - GuD
        '---------------------------------------------------------------------------------------------------------
        '   Calcul de la résitance au voilement par cisaillement selon EN 1993-1-5
        '---------------------------------------------------------------------------------------------------------
        '   GammaM1         [E] :   GammaM1
        '   EtaW            [E] :   Eta
        '   lMontantRigid   [E] :   Indique si on peut utiliser la colonne montant rigide dans le Tablea 5.3 de l'EN 1993-1-5
        '---------------------------------------------------------------------------------------------------------
        '
        'lTwoAdjacentCantilevers: indique la présence de deux travées adjacentes en consoles (True) ou non

        '--> Déclaration

        Dim lambda_w As Decimal
        Dim k_tau As Decimal
        Dim epsilon_w As Decimal = Me.Epsilon_W
        Dim khi_w As Decimal
        Dim MyVbRd As Decimal
        Dim EN1993 As New cls_Eurocodes

        '--> Initialisation

        k_tau = 5.34
        lambda_w = (Me.ProfilA.HauteurAmeHw / Me.ProfilA.Tw) * (1 / (37.4 * epsilon_w * Math.Sqrt(k_tau)))

        khi_w = EN1993.ReductionShearBuckling(lambda_w, EtaW, lMontantRigid)

        MyVbRd = khi_w * Me.ProfilA.HauteurAmeHw * Me.ProfilA.Tw * Me.FyW / (Math.Sqrt(3) * GammaM1) * kConvMPaPa

        Return MyVbRd

    End Function

    Public Function VbRdFeu(GammaMFeu As Decimal, EtaW As Decimal, lMontantRigid As Boolean, kReducY As Decimal, kReducE As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Calcul de la résitance au voilement par cisaillement selon EN 1993-1-5 en situation d'incendie
        '---------------------------------------------------------------------------------------------------------
        '   GammaM1         [E] :   GammaM1
        '   EtaW            [E] :   Eta
        '   lMontantRigid   [E] :   Indique si on peut utiliser la colonne montant rigide dans le Tablea 5.3 de l'EN 1993-1-5
        '   kReducY, kReducE[E] :   Coefficients de réduction de fy et de E respectivement en fct de la température
        '---------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lambda_w As Decimal
        Dim k_tau As Decimal
        Dim epsilon_w As Decimal = Me.Epsilon_W * 0.85
        Dim khi_w As Decimal
        Dim MyVbRd As Decimal
        Dim EN1993 As New cls_Eurocodes

        '--> Initialisation

        k_tau = 5.34
        If IsGreater(kReducE, 0) Then
            lambda_w = (Me.ProfilA.HauteurAmeHw / Me.ProfilA.Tw) * (1 / (37.4 * epsilon_w * Math.Sqrt(k_tau))) * Math.Sqrt(kReducY / kReducE)

            khi_w = EN1993.ReductionShearBuckling(lambda_w, EtaW, lMontantRigid)
        Else
            khi_w = 0
        End If

        MyVbRd = khi_w * Me.ProfilA.HauteurAmeHw * Me.ProfilA.Tw * Me.FyW * kReducY / (Math.Sqrt(3) * GammaMFeu) * kConvMPaPa

        Return MyVbRd
    End Function

    Public ReadOnly Property AireAv As Decimal
        Get
            Return Me.ProfilA.AireAv
        End Get
    End Property

    Public Function RhoInteractionMV(VEd As Decimal, GammaM0 As Decimal) As Decimal

        Dim Rho As Decimal
        Dim VRd As Decimal = Me.VplRd(GammaM0)

        Dim VEdAbs As Decimal = Math.Abs(VEd)

        If VEdAbs > 0.5 * VRd Then
            Rho = Math.Min(1, (2 * VEd / VRd - 1) ^ 2)
        Else
            Rho = 0
        End If

        Return Rho
    End Function

    ''' <summary>
    ''' Fonction qui calcul si l'ame du profilé étudié est sensible au voilement par cisaillement (True) ou non (False)
    ''' </summary>
    ''' <param name="eta"> parametre eta, utile pour les profilés sans enrobage </param>
    ''' <returns></returns>
    Public Function IsVoilementParCisaillement(eta As Decimal) As Boolean
        Select Case Me.TypeSection
            Case Enum_TypeSection.AcierSeul, Enum_TypeSection.Mixte
                Return Not ((Me.ProfilA.HauteurAmeHw / Me.ProfilA.Tw) <= 72 * Me.Epsilon_W / eta)
            Case Enum_TypeSection.AcierSeulEnrobage, Enum_TypeSection.MixteEnrobage
                Return Not ((Me.ProfilA.HauteurAmeDw / Me.ProfilA.Tw) <= 124 * Me.Epsilon_W)
            Case Else 'slimfloor -> l'ame du profilé n'est pas sensible au voilement par cisaillement 
                Return False
        End Select
    End Function

    'Public ReadOnly Property RhoVCalcul As Decimal
    '    Get
    '        Dim Rho As Decimal = 0
    '        If Me.Param.lInterActionMV Then Rho = Me.RhoInteractionMV(Me.Param.VEd)
    '        Return Rho
    '    End Get
    'End Property

#End Region

#Region " Propriétés matériaux "

    ''' <summary>
    ''' Limite d'élasticité de la semelle supérieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FySup As Decimal
        Get
            Dim MyFy As Decimal

            If Me.lUser Then '--[ Acier défini directement par l'utilisateur
                MyFy = Me.f_y.fs

            Else '--[ Acier de la base de donnée : Recherche dans les plages
                If Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
                    MyFy = Me.Acier.LimiteFy(Math.Max(Me.ProfilA.Tfs, Me.ProfilA.Tw))
                Else
                    MyFy = Me.Acier.LimiteFy(Me.ProfilA.Tfs)
                End If
            End If


            Return MyFy
        End Get
    End Property

    ''' <summary>
    ''' Limite d'élasticité de la semelle inférieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FyInf As Decimal
        Get
            'Return Me.Acier.LimiteFy(Me.ProfilA.Tfi)
            Dim MyFy As Decimal

            If Me.lUser Then '--[ Acier défini directement par l'utilisateur
                MyFy = Me.f_y.fi

            Else '--[ Acier de la base de donnée : Recherche dans les plages
                If Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
                    MyFy = Me.Acier.LimiteFy(Math.Max(Me.ProfilA.Tfi, Me.ProfilA.Tw))
                Else
                    MyFy = Me.Acier.LimiteFy(Me.ProfilA.Tfi)
                End If
            End If

            Return MyFy
        End Get
    End Property

    ''' <summary>
    ''' Limite d'élasticité de l'âme
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FyW As Decimal
        Get
            Dim MyFy As Decimal

            If Me.lUser Then '--[ Acier défini directement par l'utilisateur
                MyFy = Me.f_y.w

            Else '--[ Acier de la base de donnée : Recherche dans les plages
                If Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
                    MyFy = Me.Acier.LimiteFy(Math.Max(Me.ProfilA.Tfs, Me.ProfilA.Tw))
                Else '--[ Acier de la base de donnée : Recherche dans les plages
                    MyFy = Me.Acier.LimiteFy(Me.ProfilA.Tw)
                End If
            End If

            Return MyFy
        End Get
    End Property

    Public ReadOnly Property FySpd As Decimal
        Get
            Dim MyFy As Decimal

            If Me.lUser Then '--[ Acier défini directement par l'utilisateur
                MyFy = Me.f_y.spd

            Else '--[ Acier de la base de donnée : Recherche dans les plages
                MyFy = Me.AcierSPD.LimiteFy(Me.ProfilA.Plat_t)
            End If

            Return MyFy
        End Get
    End Property

    Public ReadOnly Property Epsilon_Sup
        Get
            Return Me.Acier.get_epsilon(Me.FySup)
        End Get
    End Property

    Public ReadOnly Property Epsilon_W
        Get
            Return Me.Acier.get_epsilon(Me.FyW)
        End Get
    End Property

    Public ReadOnly Property Epsilon_Inf
        Get
            Return Me.Acier.get_epsilon(Me.FyInf)
        End Get
    End Property

    Public ReadOnly Property Epsilon_Spd
        Get
            Return Me.Acier.get_epsilon(Me.FySpd)
        End Get
    End Property

#End Region

#Region " Effet du béton tendu dans le calcul des contraintes "

    Public Function DeltaSigma(myDalle As cls_Dalle, bEff As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Calcul le supplément de contraintes dans les aramtures de la dalle dues à l'effet de rigidité du béton tendu
        '----------------------------------------------------------------------------------------------------------------
        '   bEff        [E] :   Largeur efficace de dalle
        '   myDalle     [E] :   Dalle traitée
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim DeltaS As Decimal
        Dim Fctm As Decimal
        Dim RauS As Decimal

        '--> Initialisation

        Fctm = myDalle.beton.Fctm
        RauS = myDalle.AireUnitArmaturesLongi / myDalle.EpaisseurActive

        '--> Calcul

        DeltaS = 0.4 * Fctm / (Me.AlphaSt(bEff, myDalle) * RauS)

        Return DeltaS

    End Function

    Public Function AlphaSt(bEff As Decimal, myDalle As cls_Dalle) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Calcul du ratio AlphaSt utilisé pour le supplément de contraintes
        '   dans les armatures de la dalle dues à l'effet de rigidité du béton tendu
        '----------------------------------------------------------------------------------------------------------------
        '   bEff        [E] :   Largeur efficace de dalle
        '   myDalle     [E] :   Dalle traitée
        '----------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim pdtAI, pdtAaIa As Decimal
        Dim Aire, InertieY As Decimal
        Dim myG As New cls_Gamma
        Dim nEqEc, nEqDalle As Decimal
        Dim zANE As Decimal, MelRd As Decimal

        '--( Initialisation

        nEqEc = Me.Enrobage.Beton.CoefficientEquivalenceCT
        nEqDalle = myDalle.beton.CoefficientEquivalenceCT

        '--( Calcul

        '# propriétés de la section en acier

        pdtAaIa = Me.ProfilA.Aire * Me.ProfilA.InertieY

        '# propriétés de la section mixte

        Me.ProprietesElastiquesMixteMyy(-1, False, myG, nEqEc, nEqDalle, bEff, myDalle, zANE, InertieY, MelRd)

        Aire = Me.ProfilA.Aire + myDalle.AireUnitArmaturesLongi * bEff * cls_Acier.EYACIER / myDalle.AcierArmatures.Es
        pdtAI = Aire * InertieY

        Return pdtAI / pdtAaIa

    End Function


#End Region

#Region " Fonctions de calcul "

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Private Sub Calcul_Proprietes()

        'A_fs = b_fs * t_fs
        'A_fi = b_fi * t_fi

    End Sub

    Public Function Calcul_Armatures_Minimales_Enrobage_Partiel()
        '--------------------------------------------------------------------------------------
        '   24/06/23 : Création - GuD
        '--------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------
        'Définition des variables locales
        Dim ks As Decimal
        Dim kc As Decimal
        Dim k As Decimal
        Dim fct_eff As Decimal
        Dim Act As Decimal
        Dim sigma_s As Decimal
        Dim As_min As Decimal

        ks = 0.9
        kc = 0.6
        k = 0.8
        fct_eff = Enrobage.Beton.Fctm
        Act = Enrobage.Ratio_bc * ProfilA.Bfs * ProfilA.HauteurAmeHw

        If Enrobage.Beton.lCrackingLimitation Then
            Dim phi_max As Decimal = Enrobage.Get_Phi_Max()
            sigma_s = Mod_Declarations.Get_sigma_S1_Ds(Enrobage.Beton.wk_max, phi_max)
        Else
            sigma_s = Enrobage.AcierArmatures.FsK
        End If

        As_min = ks * kc * k * fct_eff * Act / sigma_s

        Return As_min

    End Function

    '''' <summary>
    '''' Lancement de toutes les fonctions de calcul
    '''' </summary>
    'Public Sub Lancement_Calcul(Param As Cls_OptionsCalcul, dalle As Cls_Dalle)

    '    Calcul_Proprietes()

    '    Const E As Decimal = 210000 * 10 ^ (6)

    '    'enrobage
    '    If Me.lEnrobage Then

    '        enrobage_partiel.Calcul_Proprietes()
    '        enrobage_partiel.Beton.Calcul_Proprietes()

    '        Dim h_w As Decimal = ProfilA.HauteurAmeHw
    '        'Param.Prop_Elastique_Enrobage.h_0 = 2 * (h_w * (enrobage_partiel.b_c - ProfilA.t_w) - (4 - Math.PI) * ProfilA.r_cs ^ 2) / (2 * h_w)
    '        Param.Prop_Elastique_Enrobage.h_0 = 2 * (h_w * (enrobage_partiel.Get_b_c(ProfilA.Bfs) - ProfilA.Tw) - (4 - Math.PI) * ProfilA.Rcs ^ 2) / (2 * h_w)
    '        Param.Prop_Elastique_Enrobage.Calcul_Coeff(E, enrobage_partiel.Beton.Fcm, enrobage_partiel.Beton.Ecm)

    '    End If

    '    'dalle
    '    If lDalleBeton Then

    '        dalle.Calcul_Proprietes()
    '        dalle.beton.Calcul_Proprietes()

    '        If dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte Then
    '            Param.Prop_Elastique_Dalle.h_0 = 2 * (dalle.Ep_td - dalle.Bac.Hp)
    '        Else ' dalle pleine
    '            Param.Prop_Elastique_Dalle.h_0 = dalle.Ep_td
    '        End If
    '        Param.Prop_Elastique_Dalle.Calcul_Coeff(E, dalle.beton.Fcm, dalle.beton.Ecm)

    '    End If

    'End Sub

#End Region

#Region " Fonctions de copie "

    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(ByVal SectionSource As cls_Section, ByRef SectionCible As cls_Section)
        SectionCible = SectionSource.Clone

        SectionSource.ProfilA.DeepClone(SectionSource.ProfilA, SectionCible.ProfilA)
        SectionSource.Enrobage.DeepClone(SectionSource.Enrobage, SectionCible.Enrobage)
        SectionSource.Acier.Deepclone(SectionSource.Acier, SectionCible.Acier)
        SectionSource.AcierSPD.Deepclone(SectionSource.AcierSPD, SectionCible.AcierSPD)

    End Sub

    ''' <summary>*
    ''' Fonction de clone à utiliser
    ''' </summary>
    ''' <param name="s_origine"></param>
    ''' <param name="s_destination"></param>
    'Public Shared Sub CloneSection(ByVal s_origine As cls_Section, ByRef s_destination As cls_Section)
    '    s_destination = s_origine.Clone()
    '    s_destination.Acier = s_origine.Acier.Clone()

    '    s_destination.Enrobage = s_origine.Enrobage.Clone()
    '    s_destination.Enrobage.Beton = s_origine.Enrobage.Beton.Clone()

    '    's_destination.dalle = s_origine.dalle.Clone()
    '    's_destination.dalle.beton = s_origine.dalle.beton.Clone()
    '    's_destination.dalle.arma_longi_inf = s_origine.dalle.arma_longi_inf.Clone()
    '    's_destination.dalle.arma_longi_sup = s_origine.dalle.arma_longi_sup.Clone()
    '    's_destination.dalle.bac_acier = s_origine.dalle.bac_acier.Clone()

    '    's_destination.Param = s_origine.Param.Clone()
    '    's_destination.Param.Prop_Elastique_Enrobage = s_origine.Param.Prop_Elastique_Enrobage.Clone()
    '    's_destination.Param.Prop_Elastique_Dalle = s_origine.Param.Prop_Elastique_Dalle.Clone()

    'End Sub

#End Region

#Region " Classification section acier "

    ''' <summary>
    ''' Calcul de la classe d'une section acier (usuelle, enrobée ou slimfloor)
    ''' </summary>
    ''' <param name="zANP">position de l'axe neutre plastique (/!\ la position est donnée par rapport à l'axe nul de référence qui dépend de si on est sur une section slimfloor ou non /!\</param>
    ''' <param name="zANE">position de l'axe neutre élastique (/!\ la position est donnée par rapport à l'axe nul de référence qui dépend de si on est sur une section slimfloor ou non /!\</param>
    ''' <param name="lFlexionPositive">indique si on considère le calcul en considérant une flexion positive (qui comprime la semelle supérieure) ou non</param>
    ''' <param name="lBetonSlimfloor">indique si on réalise le calcul en considérant que c'est uen section slimfloor ou non</param>
    ''' <param name="lBetonEnrobage">indique si on réalise le calcul en considérant que la section est enrobée ou non</param>
    ''' <param name="lG1_EN">indique si on réalise le calcul en considérant la 1ere génération de l'eurocode ou non</param>
    ''' <param name="td">Optionel: indique l'épaisseur totale de la dalle (si pertinent)</param>
    ''' <returns></returns>
    Public Function ClasseSection(zANP As Decimal, zANE As Decimal, lFlexionPositive As Boolean,
                                  lBetonSlimfloor As Boolean, lBetonEnrobage As Boolean, lG1_EN As Boolean, Optional td As Decimal = 0,
                                  Optional lCalculFeu As Boolean = False) As Integer

        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une section acier usuelle (sans enrobage)
        '----------------------------------------------------------------------------------------------------------
        '   zANP                [E] :   Position de l'ANP (compté algébriquement depuis la face inférieure de la dalle béton)
        '   zANE                [E] :   Position de l'ANE (compté algébriquement depuis la face inférieure de la dalle béton)
        '   lFlexionPositive    [E] :   Indique si le calcul se fait en considérant une flexion positive (True) ou non (False)
        '   lG1_EN              [E] :   Indique si le calcul de la classe se fait selon les Eurocodes actuels (True) ou selon la deuxieme génération d'Eurocodes (False)
        '   td                  [E] :   Epaisseur totale de la dalle (hors renformis)
        '   lCalculFeu          [E] :   Indique si calcul au feu
        '----------------------------------------------------------------------------------------------------------

        Dim classeSemellesSup, classeAme, classeSemellesInf, classePlatInfSFB, classeSectionTotale As Integer
        Dim lSemelleSupComprimeeLoc, lSemelleInfComprimeeLoc As Boolean
        Dim cfsup, tfsup, cfinf, tfinf, cplat, tplat As Decimal
        Dim epsilon_fsup As Decimal = Epsilon_Sup
        Dim epsilon_finf As Decimal = Epsilon_Inf
        Dim epsilon_platSFB As Decimal = 0
        ' Dim alpha, psi As Decimal

        '---------------------------------------------
        '---------------------------------------------
        '### Calcul avec hypothèse répartition plastique
        '---------------------------------------------
        '---------------------------------------------

        ' --> Initialisation des variables locales 

        calcul_cf_tf(cfsup, tfsup, cfinf, tfinf, cplat, tplat) 'calcul les différentes valeurs de c et t pour la semelle sup, inf et le plat soudé (le cas échéant)

        ' --> Calcul classe semelle supérieure

        lSemelleSupComprimeeLoc = lSemelleSupComprimee(lFlexionPositive, zANP) 'On regarde si la semelle supérieure du profilé est comprimée ou non
        classeSemellesSup = ClasseSemelle(lSemelleSupComprimeeLoc, lBetonSlimfloor, lBetonEnrobage, cfsup, tfsup, epsilon_fsup) 'calcul la classe de la semelle sup en fonction de si elle est comprimée et du ratio c/t

        If lBetonSlimfloor Then 'reduction possible dans le cas où on a une section slimfloor et où on prend en compte le béton
            If Me.TypeSection = cls_Section.Enum_TypeSection.IFB_B Or Me.TypeSection = cls_Section.Enum_TypeSection.IFB_Bmixte Then
                If td - hec >= Math.Max(50 / 1000, Me.ProfilA.Plat_b / 6) Then classeSemellesSup = Math.Min(classeSemellesSup, 2)
            Else
                If td - hec >= Math.Max(50 / 1000, Me.ProfilA.Bfs / 6) Then classeSemellesSup = Math.Min(classeSemellesSup, 2)
            End If
        End If

        ' --> Calcul classe semelle inférieure

        lSemelleInfComprimeeLoc = Not lFlexionPositive 'on regarde si la semelle inférieure du profilé est comprimée
        classeSemellesInf = ClasseSemelle(lSemelleInfComprimeeLoc, lBetonSlimfloor, lBetonEnrobage, cfinf, tfinf, epsilon_finf) 'calcul la classe de la semelle sup en fonction de si elle est comprimée et du ratio c/t

        ' --> Calcul classe semelle plat inférieur dans le cas d'un SFB
        If Me.TypeSection = cls_Section.Enum_TypeSection.SFB Or Me.TypeSection = cls_Section.Enum_TypeSection.SFBmixte Then
            epsilon_platSFB = Me.Epsilon_Spd
            classePlatInfSFB = ClasseSemelle(lSemelleInfComprimeeLoc, lBetonSlimfloor, lBetonEnrobage, cplat, tplat, epsilon_platSFB) 'calcul la classe du plat soudé dans le cas des sections slimfloors en fonction de si elle est comprimée et du ratio c/t
        Else
            classePlatInfSFB = 0
        End If

        ' --> Calcul classe âme
        classeAme = Me.ClasseAme(lFlexionPositive, zANP, lG1_EN)

        ' --> Calcul classe section totale 
        classeSectionTotale = Math.Max(classeSemellesSup, Math.Max(classeSemellesInf, Math.Max(classePlatInfSFB, classeAme)))

        If classeSectionTotale = 3 Then

            '---------------------------------------------
            '---------------------------------------------
            '### Calcul avec hypothèse répartition élastique 
            '---------------------------------------------
            '---------------------------------------------

            ' --> Calcul classe semelle supérieure
            lSemelleSupComprimeeLoc = lSemelleSupComprimee(lFlexionPositive, zANE)
            classeSemellesSup = ClasseSemelle(lSemelleSupComprimeeLoc, lBetonSlimfloor, lBetonEnrobage, cfsup, tfsup, epsilon_fsup)

            ' --> Calcul classe semelle inférieure inchangé

            ' --> Calcul classe plat inférieur dans le cas d'une section SFB inchangé

            ' --> Calcul classe âme

            classeAme = Me.ClasseAme(lFlexionPositive, zANE, lG1_EN)

            ' --> Calcul classe section totale

            classeSectionTotale = Math.Max(classeSemellesSup, Math.Max(classeSemellesInf, Math.Max(classePlatInfSFB, classeAme)))
            classeSectionTotale = Math.Max(classeSectionTotale, 3)

        End If

        Return classeSectionTotale

    End Function

    ''' <summary>
    ''' Calcul les différentes valeurs de c et de t 
    ''' </summary>
    ''' <param name="cfsup">valeur c pour la semelle supérieure</param>
    ''' <param name="tfsup">valeur t pour la semelle sup (épaisseur ici)</param>
    ''' <param name="cfinf">valeur de c pour la semelle inférieure</param>
    ''' <param name="tfinf">valeur de t pour la semelle inférieure (épaisseur ici)</param>
    ''' <param name="cplat">valeur de c pour le plat soudé, le cas échéant (sinon la valeur 0 lui sera affectée)</param>
    ''' <param name="tplat">valeur de t pour le plat soudé, le cas échéant (sinon la valeur 0 lui sera affectée)</param>
    Public Sub calcul_cf_tf(ByRef cfsup As Decimal, ByRef tfsup As Decimal, ByRef cfinf As Decimal, ByRef tfinf As Decimal, ByRef cplat As Decimal, ByRef tplat As Decimal)
        With Me.ProfilA
            Select Case Me.typeSection
                Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte
                    cfsup = (.Bfs - .Tw) / 2 - .Rcs
                    tfsup = .Tfs
                    cfinf = (.Bfi - .Tw) / 2 - .Rci
                    tfinf = .Tfi
                    cplat = (.Plat_b - .Bfi) / 2
                    tplat = .Plat_t
                Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte
                    cfsup = (.Bfs - .Tw) / 2 - .Rcs
                    tfsup = .Tfs
                    cfinf = (.Plat_b - .Tw) / 2
                    tfinf = .Plat_t
                    cplat = 0
                    tplat = 0
                Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte
                    cfsup = (.Plat_b - .Tw) / 2
                    tfsup = .Plat_t
                    cfinf = (.Bfi - .Tw) / 2 - .Rci
                    tfinf = .Tfi
                    cplat = 0
                    tplat = 0
                Case cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SABmixte
                    cfsup = (.Bfs - .Tw) / 2 - .Rcs
                    tfsup = .Tfs
                    cfinf = (.Bfi - .Tw) / 2 - .Rci
                    tfinf = .Tfi
                    cplat = 0
                    tplat = 0
                Case Else
                    cfsup = (.Bfs - .Tw) / 2 - .Rcs
                    tfsup = .Tfs
                    cfinf = (.Bfi - .Tw) / 2 - .Rci
                    tfinf = .Tfi
                    cplat = 0
                    tplat = 0
            End Select
        End With
    End Sub

    ''' <summary>
    ''' Indique si la semelle supérieure du profilé est comprimé ou non 
    ''' Utilisé dans le cas du calcul de la classe de la section
    ''' </summary>
    ''' <param name="lFlexionPositive">indique si la section est soumise à une flexion positive (qui comprime la semelle suoérieure) ou non </param>
    ''' <param name="zAN">position de l'axe neutre</param>
    ''' <returns></returns>
    Public Function lSemelleSupComprimee(lFlexionPositive As Boolean, zAN As Decimal) As Boolean
        If lFlexionPositive Then
            If lSlimFloor Then
                Select Case Me.typeSection
                    Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte
                        lSemelleSupComprimee = zAN <= Me.hec - Me.ProfilA.Plat_t
                    Case Else
                        lSemelleSupComprimee = zAN <= Me.hec - Me.ProfilA.Tfs
                End Select
            Else
                lSemelleSupComprimee = zAN <= -Me.ProfilA.Tfs
            End If
        Else
            If lSlimFloor Then
                lSemelleSupComprimee = zAN >= Me.hec
            Else
                lSemelleSupComprimee = zAN >= -Me.ProfilA.Tfs
            End If
        End If
    End Function

    ''' <summary>
    ''' Calcul de la classe d'une semelle (paroi en console)
    ''' </summary>
    ''' <param name="lComprimee">paramètre qui indique si la semelle est comprimée ou non</param>
    ''' <param name="lBetonSlimfloor">paramètre si on prend en compte le béton dans le cas d'une section slimfloor</param>
    ''' <param name="lBetonEnrobage">paramètre si on prend en compte le béton dans le cas d'une section enrobée</param>
    ''' <param name="c">paramètre c</param>
    ''' <param name="t">épaisseur de la plaque</param>
    ''' <param name="epsilon_f">coefficient epsilon qui tient compte de la limite d'élasticité </param>
    ''' <returns></returns>
    Public Function ClasseSemelle(lComprimee As Boolean, lBetonSlimfloor As Boolean, lBetonEnrobage As Boolean, c As Decimal, t As Decimal, epsilon_f As Decimal) As Integer
        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une semelle (paroi en console)
        '----------------------------------------------------------------------------------------------------------
        '   lComprimee       [E] :   indique si la paroi est entierement comprimee (True) ou non (False)
        '   c                [E] :   hauteur de la paroi en console
        '   t                [E] :   epaisseur de la paroi en console
        '   epsilon          [E] :   epsilon de la paroi en console 
        '----------------------------------------------------------------------------------------------------------

        If lComprimee Then
            If lBetonSlimfloor Then 'on réalise le calcul en tenant compte du béton dans le cas d'une section slimfloor
                Return ClasseSemelleSlimFloorComprimee(c, t, epsilon_f)
            Else 'la section n'est pas slimfloor ou alors on ne considère pas l'effet du béton
                If lBetonEnrobage Then 'on réalise le calcul en tenant compte du béton d'enrobage
                    Return ClasseSemelleEnrobeComprimee(c, t, epsilon_f)
                Else 'la section n'est pas enrobée ou alors on ne tient pas compte de l'effet du béton dans le calcul 
                    Return ClasseSemelleConsoleComprimee(c, t, epsilon_f)
                End If
            End If
        Else
            Return 1
        End If


    End Function

    ''' <summary>
    ''' Calcul de la classe d'une semelle comprimée (paroi en console) dans le cas usuel
    ''' </summary>
    ''' <param name="c">paramètre c</param>
    ''' <param name="t">épaisseur de la plaque</param>
    ''' <param name="epsilon_f">coefficient epsilon qui tient compte de la limite d'élasticité</param>
    ''' <returns></returns>
    Public Function ClasseSemelleConsoleComprimee(c As Decimal, t As Decimal, epsilon_f As Decimal) As Integer
        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une ame flechie non enrobée, en considérant une répartition plastique des contraintes, selon la 1ere génération des Eurocodes
        '----------------------------------------------------------------------------------------------------------
        '   c                [E] :   hauteur de la paroi en console
        '   t                [E] :   epaisseur de la paroi en console
        '   epsilon          [E] :   epsilon de la paroi en console 
        '----------------------------------------------------------------------------------------------------------

        If t = 0 Then Return 4 'Permet d'éviter le bug quand t = 0

        Dim classeSemelle As Integer

        Select Case c / t
            Case <= 9 * epsilon_f
                classeSemelle = 1
            Case <= 10 * epsilon_f
                classeSemelle = 2
            Case <= 14 * epsilon_f
                classeSemelle = 3
            Case Else
                classeSemelle = 4
        End Select


        Return classeSemelle
    End Function

    ''' <summary>
    ''' Calcul de la classe d'une semelle comprimée (paroi en console) dans le cas d'une section enrobé
    ''' </summary>
    ''' <param name="c">paramètre c</param>
    ''' <param name="t">épaisseur de la plaque</param>
    ''' <param name="epsilon_f">coefficient epsilon qui tient compte de la limite d'élasticité </param>
    ''' <returns></returns>
    Public Function ClasseSemelleEnrobeComprimee(c As Decimal, t As Decimal, epsilon_f As Decimal) As Integer
        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une ame flechie non enrobée, en considérant une répartition plastique des contraintes, selon la 1ere génération des Eurocodes
        '----------------------------------------------------------------------------------------------------------
        '   c                [E] :   hauteur de la paroi en console
        '   t                [E] :   epaisseur de la paroi en console
        '   epsilon          [E] :   epsilon de la paroi en console 
        '----------------------------------------------------------------------------------------------------------

        If t = 0 Then Return 4 'Permet d'éviter le bug quand t = 0

        Dim classeSemelle As Integer

        Select Case c / t
            Case <= 9 * epsilon_f
                classeSemelle = 1
            Case <= 14 * epsilon_f
                classeSemelle = 2
            Case <= 20 * epsilon_f
                classeSemelle = 3
            Case Else
                classeSemelle = 4
        End Select


        Return classeSemelle
    End Function

    ''' <summary>
    ''' Calcul de la classe d'une semelle comprimée (paroi en console) dans le cas d'une section slimfloor
    ''' </summary>
    ''' <param name="c">paramètre c</param>
    ''' <param name="t">épaisseur de la plaque</param>
    ''' <param name="epsilon_f">coefficient epsilon qui tient compte de la limite d'élasticité </param>
    ''' <returns></returns>
    Public Function ClasseSemelleSlimFloorComprimee(c As Decimal, t As Decimal, epsilon_f As Decimal) As Integer
        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une ame flechie non enrobée, en considérant une répartition plastique des contraintes, selon la 1ere génération des Eurocodes
        '----------------------------------------------------------------------------------------------------------
        '   c                [E] :   hauteur de la paroi en console
        '   t                [E] :   epaisseur de la paroi en console
        '   epsilon          [E] :   epsilon de la paroi en console 
        '----------------------------------------------------------------------------------------------------------

        If t = 0 Then Return 4 'Permet d'éviter le bug quand t = 0

        Dim classeSemelle As Integer

        Select Case c / t
            Case <= 33 * epsilon_f
                classeSemelle = 1
            Case <= 38 * epsilon_f
                classeSemelle = 2
            Case <= 42 * epsilon_f
                classeSemelle = 3
            Case Else
                classeSemelle = 4
        End Select

        Return classeSemelle
    End Function

    ''' <summary>
    ''' Calcul la classe d'une ame
    ''' </summary>
    ''' <param name="lFlexionPositive"></param>
    ''' <param name="zAN"></param>
    ''' <param name="lG1_EN"></param>
    ''' <returns></returns>
    Public Function ClasseAme(lFlexionPositive As Boolean, zAN As Decimal, lG1_EN As Boolean) As Integer

        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une ame flechie 
        '----------------------------------------------------------------------------------------------------------
        '   lFlexionPositive    [E] :   Indique si le calcul se fait en considérant une flexion positive (True) ou non (False)
        '   SectionLoc          [E] :   Section locale à classer
        '   zAN                 [E] :   Position de l'Axe Neutre (compté algébriquement depuis la face supérieure du profilé)
        '   lG1_EN              [E] :   Indique si le calcul de la classe se fait selon les Eurocodes actuels (True) ou selon la deuxieme génération d'Eurocodes (False)
        '----------------------------------------------------------------------------------------------------------

        Dim epsilon_w As Decimal = Me.Epsilon_W
        Dim alpha, psi As Decimal
        Dim classeAmeLoc As Integer



        With Me.ProfilA

            '# Hypothese d'une répartition plastique

            If lAmeEntierementTendue(lFlexionPositive, zAN) Then
                classeAmeLoc = 1
            Else 'Ame au moins en partie comprimée 
                alpha = CalculAlpha(lFlexionPositive, zAN)
                classeAmeLoc = ClasseAmeFlechiePlastique(.HauteurAmeDw, .Tw, epsilon_w, alpha, lG1_EN)
            End If

            If classeAmeLoc >= 3 Then
                '# Hypothese d'une répartition élastique

                psi = CalculPsi(lFlexionPositive, zAN)
                classeAmeLoc = ClasseAmeFlechieElastique(.HauteurAmeDw, .Tw, epsilon_w, psi, lG1_EN)

            End If

        End With

        Return classeAmeLoc

    End Function

    ''' <summary>
    ''' Permet de savoir si l'ame du profile est entierement tendue ou non (nécessaire avant de faire le calcul de alpha ou de psi)
    ''' </summary>
    ''' <param name="lFlexionPositive">indique si on considère une flexion positive (qui comprime la semelle supérieure) ou non</param>
    ''' <param name="zAN">position de l'axe neutre</param>
    ''' <returns></returns>
    Public Function lAmeEntierementTendue(lFlexionPositive As Boolean, zAN As Decimal) As Boolean

        'Permet de savoir si l'ame flechie est entierement tendue ou non (nécessaire avant le calcul de alpha ou psi)
        With Me.ProfilA

            If lFlexionPositive Then
                If lSlimFloor Then
                    Select Case Me.typeSection
                        Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte
                            Return zAN >= Me.hec - .Plat_t
                        Case Else
                            Return zAN >= Me.hec - .Rcs - .Tfs
                    End Select
                Else 'section classique
                    Return zAN >= -(.Tfs + .Rcs)  'Ame entierement tendue
                End If
            Else 'flexion négative
                If lSlimFloor Then
                    Select Case Me.typeSection
                        Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte
                            Return zAN <= .Tfi + .Rci
                        Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte
                            Return zAN <= 0
                        Case Else
                            Return zAN <= .Rci
                    End Select
                Else
                    Return zAN <= -(.Tfs + .Rcs + .HauteurAmeDw)
                End If
            End If

        End With
    End Function

    Public Function lAmeEntierementComprimee(lFlexionPositive As Boolean, zAN As Decimal) As Boolean
        'Permet de savoir si l'ame flechie est entierement comprimee ou non (nécessaire pour le calcul de alpha)
        Return lAmeEntierementTendue(Not lFlexionPositive, zAN)
    End Function

    Public Function CalculAlpha(ByVal lFlexionPositive As Boolean, ByVal zAN As Decimal) As Decimal

        'Calcul de alpha. Suppose que l'ame est au moins en partie comprimée 

        Dim alpha As Decimal

        With Me.ProfilA

            If lAmeEntierementComprimee(lFlexionPositive, zAN) Then
                alpha = 1
            Else
                If lFlexionPositive Then

                    If lSlimFloor Then
                        Select Case Me.typeSection
                            Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte
                                alpha = (Me.hec - .Plat_t - zAN) / .HauteurAmeDw
                            Case Else
                                alpha = (Me.hec - .Tfs - .Rcs - zAN) / .HauteurAmeDw
                        End Select
                    Else 'section classique
                        alpha = (-zAN - .Tfs - .Rcs) / .HauteurAmeDw
                    End If
                Else
                    If lSlimFloor Then
                        Select Case Me.typeSection
                            Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte
                                alpha = (zAN - .Tfi - .Rci) / .HauteurAmeDw
                            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte
                                alpha = zAN / .HauteurAmeDw
                            Case Else
                                alpha = (zAN - .Rci) / .HauteurAmeDw
                        End Select
                    Else
                        alpha = (.ha - .Tfi - .Rci + zAN) / .HauteurAmeDw
                    End If
                End If
            End If

            Return alpha

        End With
    End Function

    Public Function CalculPsi(ByVal lFlexionPositive As Boolean, ByVal zAN As Decimal) As Decimal

        'Calcul de alpha. Suppose que l'ame est au moins en partie comprimée 

        Dim psi As Decimal

        With Me.ProfilA

            If lFlexionPositive Then

                If lSlimFloor Then
                    Select Case Me.typeSection
                        Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte
                            psi = (-zAN + .Tfi + .Rci) / (-zAN + Me.hec - .Tfs - .Rcs)
                        Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte
                            psi = (-zAN) / (-zAN + Me.hec - .Tfs - .Rcs)
                        Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte
                            psi = (-zAN + .Rci) / (-zAN + Me.hec - .Plat_t)
                        Case cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SABmixte
                            psi = (-zAN + .Rci) / (-zAN + Me.hec - .Tfs - .Rcs)
                    End Select
                Else 'section classique
                    psi = (-zAN - (.ha - .Tfi - .Rci)) / (-zAN - .Tfs - .Rcs)
                End If
            Else
                If lSlimFloor Then
                    Select Case Me.typeSection
                        Case cls_Section.Enum_TypeSection.SFB, cls_Section.Enum_TypeSection.SFBmixte

                            psi = (-zAN + Me.hec - .Tfs - .Rcs) / (-zAN + .Tfi + .Rci)

                        Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_Amixte

                            psi = (-zAN + Me.hec - .Tfs - .Rcs) / (-zAN)

                        Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.IFB_Bmixte

                            psi = (-zAN + Me.hec - .Plat_t) / (-zAN + .Rci)

                        Case cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SABmixte

                            psi = (-zAN + Me.hec - .Tfs - .Rcs) / (-zAN + .Rci)

                    End Select
                Else

                    psi = (-zAN - .Tfs - .Rcs) / (-zAN - (.ha - .Tfi - .Rci))

                End If
            End If

            Return psi

        End With
    End Function

    Public Function ClasseAmeFlechiePlastique(c As Decimal, t As Decimal, epsilon As Decimal, alpha As Decimal, lG1_EN As Boolean) As Integer
        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une ame flechie non enrobée, en considérant une répartition plastique des contraintes, selon la 1ere génération des Eurocodes
        '----------------------------------------------------------------------------------------------------------
        '   c                [E] :   hauteur de la paroi interne
        '   t                [E] :   epaisseur de la paroi interne
        '   epsilon          [E] :   epsilon de la paroi interne 
        '   alpha            [E] :   portion de la paroi interne comprimée 
        '----------------------------------------------------------------------------------------------------------
        Dim classe As Integer

        If lG1_EN Then
            If alpha > 0.5 Then
                Select Case c / t
                    Case <= 396 * epsilon / (13 * alpha - 1)
                        classe = 1
                    Case <= 456 * epsilon / (13 * alpha - 1)
                        classe = 2
                    Case Else
                        classe = 3
                End Select
            Else 'alpha <=0.5
                Select Case c / t
                    Case <= 36 * epsilon / alpha
                        classe = 1
                    Case <= 41.5 * epsilon / alpha
                        classe = 2
                    Case Else
                        classe = 3
                End Select
            End If
        Else 'deuxieme generation d'EC
            If alpha > 0.5 Then
                Select Case c / t
                    Case <= 126 * epsilon / (5.5 * alpha - 1)
                        classe = 1
                    Case <= 188 * epsilon / (6.53 * alpha - 1)
                        classe = 2
                    Case Else
                        classe = 3
                End Select
            Else 'alpha <=0.5
                Select Case c / t
                    Case <= 36 * epsilon / alpha
                        classe = 1
                    Case <= 41.5 * epsilon / alpha
                        classe = 2
                    Case Else
                        classe = 3
                End Select
            End If
        End If



        Return classe

    End Function

    Public Function ClasseAmeFlechieElastique(c As Decimal, t As Decimal, epsilon As Decimal, psi As Decimal, lG1_EN As Boolean) As Integer
        '----------------------------------------------------------------------------------------------------------
        '   11/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la classe d'une ame flechie non enrobée, en considérant une répartition élastique des contraintes, selon la 1ere génération des Eurocodes
        '----------------------------------------------------------------------------------------------------------
        '   c                [E] :   hauteur de la paroi interne
        '   t                [E] :   epaisseur de la paroi interne
        '   epsilon          [E] :   epsilon de la paroi interne 
        '   psi              [E] :   portion de la paroi interne comprimée 
        '----------------------------------------------------------------------------------------------------------
        Dim classe As Integer
        If lG1_EN Then
            If psi > -1 Then
                If c / t <= 42 * epsilon / (0.67 + 0.33 * psi) Then
                    classe = 3
                Else
                    classe = 4
                End If
            Else 'psi<=-1
                If c / t <= 62 * epsilon * (1 - psi) * Math.Sqrt(-psi) Then
                    classe = 3
                Else
                    classe = 4
                End If
            End If
        Else
            If psi > -1 Then
                If c / t <= 38 * epsilon / (0.608 + 0.343 * psi + 0.049 * psi ^ 2) Then
                    classe = 3
                Else
                    classe = 4
                End If
            Else 'psi<=-1
                If c / t <= 60.5 * epsilon * (1 - psi) Then
                    classe = 3
                Else
                    classe = 4
                End If
            End If
        End If


        Return classe

    End Function


#End Region

#Region " Constructeurs "

    Sub New()
        Me.f_y.w = 235
        Me.f_y.fs = 235
        Me.f_y.fi = 235
        Me.f_y.spd = 235
    End Sub

    'Sub New(ByVal nom As String, ByVal typeSection As Enum_TypeSection)

    '    Me.Nom = nom
    '    Me.typeSection = typeSection

    '    Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

    '    '--> Par défaut définition utilisateur de la section
    '    Me.lDatabase = True

    '    '--> Section par défaut peu importe le type


    'End Sub

    Sub New(ByVal nom As String, ByVal typeSection As Enum_TypeSection, Nuance As String, Qualite As String, Reduction As String, MyPlages As List(Of cls_Acier.strucPlage))

        Me.f_y.w = 235
        Me.f_y.fs = 235
        Me.f_y.fi = 235
        Me.f_y.spd = 235

        Me.Nom = nom
        Me.typeSection = typeSection

        '--> Par défaut définition utilisateur de la section
        Me.lDatabase = True
        Me.Acier.Nuance = Nuance
        Me.Acier.Qualite = Qualite
        Me.Acier.Reduction = Reduction
        Me.Acier.Plages.Clear()
        For i As Integer = 0 To MyPlages.Count - 1
            Me.Acier.Plages.Add(MyPlages(i))
        Next

    End Sub

#End Region

#Region " Ecriture/Lecture  - Fichier "

    'Public Sub EcrireFile(ByRef Lines As List(Of String))
    '    '-------------------------------------------------------------------------------------
    '    '   Ecriture des attributs pour enregistrement dans un fichier 
    '    '   --> 20/02/20 v.1 
    '    '-------------------------------------------------------------------------------------

    '    Lines.Add("BLOCK SECTION")
    '    '--> Attributs pour l'interface
    '    Lines.Add("   Nom           = " & Me.Nom)
    '    Lines.Add("   lEnrobage     = " & Me.lEnrobage)
    '    Lines.Add("   lDalle        = " & Me.lDalleBeton)
    '    Lines.Add("   lDatabase     = " & Me.lDatabase)
    '    'Lines.Add("   DB_Gamme      = " & Me.Gamme)
    '    'Lines.Add("   DB_Profile    = " & Me.NomProfile)
    '    ''--> Géométrie
    '    'Lines.Add("   Type          = " & Me.typeSection)
    '    'Lines.Add("   H             = " & Me.ha)
    '    ''Lines.Add("   H_W           = " & Me.h_w)
    '    'Lines.Add("   T_W           = " & Me.t_w)
    '    'Lines.Add("   B_FS          = " & Me.b_fs)
    '    'Lines.Add("   T_FS          = " & Me.t_fs)
    '    'Lines.Add("   B_FI          = " & Me.b_fi)
    '    'Lines.Add("   T_FI          = " & Me.t_fi)
    '    'Lines.Add("   A             = " & Me.a)
    '    'Lines.Add("   R             = " & Me.r_cs)

    '    '--> Acier
    '    Me.Acier.EcrireFile(Lines)

    '    '--> Enrobage
    '    Me.Enrobage.EcrireFile(Lines)

    '    ''--> Dalle de béton
    '    'Me.dalle.EcrireFile(Lines)

    '    ''--> Options de calcul
    '    'Me.Param.EcrireFile(Lines)

    '    Lines.Add("")

    'End Sub

    Public Sub LectureFile(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        Try
            '--> Traitement
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                    Select Case MotCle

                        Case "NOM"
                            Me.Nom = ""
                            For z = 2 To nbMots
                                If z = nbMots Then
                                    Me.Nom += Mots(z)
                                Else
                                    Me.Nom += Mots(z) + " "
                                End If
                            Next
                        'Case "LENR" : Me.lEnrobagePartiel = Mots(nbMots)
                        Case "LDAL" : Me.lDalleBeton = Mots(nbMots)
                        Case "LDAT" : Me.lDatabase = Mots(nbMots)
                        'Case "DB_G" : Me.Gamme = Mots(nbMots)
                        'Case "DB_P" : Me.NomProfile = Mots(nbMots)
                            '--> Géométrie
                        Case "TYPE" : Me.typeSection = Mots(nbMots)
                        'Case "H" : Me.ha = Mots(nbMots)
                        ''Case "H_W" : Me.h_w = Mots(nbMots)
                        'Case "T_W" : Me.t_w = Mots(nbMots)
                        'Case "B_FS" : Me.b_fs = Mots(nbMots)
                        'Case "T_FS" : Me.t_fs = Mots(nbMots)
                        'Case "B_FI" : Me.b_fi = Mots(nbMots)
                        'Case "T_FI" : Me.t_fi = Mots(nbMots)
                        'Case "A" : Me.a = Mots(nbMots)
                        'Case "R" : Me.r_cs = Mots(nbMots)
                            '--> Acier
                        Case "NUAN"
                            Me.Acier.Nuance = ""
                            For z = 2 To nbMots
                                If z = nbMots Then
                                    Me.Acier.Nuance += Mots(z)
                                Else
                                    Me.Acier.Nuance += Mots(z) + " "
                                End If
                            Next
                        Case "QUAL" : Me.Acier.Qualite = Mots(nbMots)
                        Case "LUSE" : Me.lUser = Mots(nbMots)
                        Case "FYW" : Me.f_y.w = Mots(nbMots)
                        Case "FYFS" : Me.f_y.fs = Mots(nbMots)
                        Case "FYFI" : Me.f_y.fi = Mots(nbMots)
                            '--> Enrobage
                        'Case "EB_C" : Me.enrobage_partiel.b_c = Mots(nbMots)
                       ' Case "EF_Y" : Me.enrobage_partiel.acier_armature = Mots(nbMots)
                            '--> Béton enrobage
                        Case "EBTY" : Me.Enrobage.Beton.lLeger = Mots(nbMots)
                        Case "EBCL" : Me.Enrobage.Beton.Classe = Mots(nbMots)
                        Case "EBFC" : Me.Enrobage.Beton.Fck = Mots(nbMots)


                    End Select
                End If

            Next
        Catch ex As Exception
            MsgBox("Erreur lecture fichier pmx - données corrompues" & Chr(10) & "Error read file pmx - corrupt data", MsgBoxStyle.Critical, "Cls_Section/LectureFile")
        End Try

    End Sub

#End Region

#Region " Outils "

    'Public Sub InitialisePositionArmaturesEnrobage()
    '    '--------------------------------------------------------------------------------------------
    '    '   26/04/23 :  Création - POM
    '    '--------------------------------------------------------------------------------------------
    '    '   Positionnement des armatures de la section d'enrobage
    '    '--------------------------------------------------------------------------------------------

    '    With Me.Enrobage

    '        .LitsArmaOLD(0).zArma = -Me.ProfilA.ha + Me.ProfilA.Tfi + .Etriers_EnrobageZ + .Etriers_Phi + .LitsArmaOLD(0).Phi / 2

    '        .LitsArmaOLD(2).zArma = -Me.ProfilA.Tfs - .Etriers_EnrobageZ - .Etriers_Phi - .LitsArmaOLD(2).Phi / 2

    '        .LitsArmaOLD(1).zArma = (.LitsArmaOLD(0).zArma + .LitsArmaOLD(2).zArma) / 2

    '    End With

    'End Sub

    ''' <summary>
    ''' Renvoie la position z d'un lit d'armature dans l'enrobage
    ''' </summary>
    ''' <param name="iArma"></param>
    ''' <returns></returns>
    Public Function zPositionLitArmaEnrobage(iArma As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   12/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie la position z d'un lit d'armature de l'enrobage partiel (z pondéré)
        '------------------------------------------------------------------------------------------------------------
        '   iArma   [E] :   Indice du lit d'armature (0: inférieur / 1: central / 2: supérieur)
        '------------------------------------------------------------------------------------------------------------

        Dim zPos As Decimal
        Dim DeltaZ As Double

        DeltaZ = Me.Enrobage.LitArma(iArma).NbExt * Me.Enrobage.LitArma(iArma).PhiExt ^ 3 / 8
        DeltaZ += Me.Enrobage.LitArma(iArma).NbMil * Me.Enrobage.LitArma(iArma).PhiMil ^ 3 / 8
        DeltaZ += Me.Enrobage.LitArma(iArma).NbInt * Me.Enrobage.LitArma(iArma).PhiInt ^ 3 / 8

        DeltaZ = DeltaZ * Math.PI / Me.Enrobage.LitArma(iArma).Aire

        Select Case iArma
            Case 0
                zPos = +DeltaZ - Me.ProfilA.ha + Me.ProfilA.Tfi _
                     + Me.Enrobage.Etriers_EnrobageZ + Me.Enrobage.Etriers_Phi

            Case 1
                zPos = -Me.Enrobage.LitArma(iArma).zPosRatio * Me.ProfilA.ha
            Case 2
                zPos = -DeltaZ - Me.ProfilA.Tfs _
                     - Me.Enrobage.Etriers_EnrobageZ - Me.Enrobage.Etriers_Phi
        End Select

        Return zPos
    End Function

    Public Function zPosArmaEnrobage(iArma As Integer, iPos As Integer, iBarre As Integer) As Decimal
        '--------------------------------------------------------------------------------------------
        '   12/06/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la position z d'une barre d'armature longi de l'enrobage
        '--------------------------------------------------------------------------------------------
        '   iArma       [E] :   Indique lit d'armature (0: inférieur/ 1: milieu/ 2: supérieur)
        '   iPos        [E] :   Position des barres (0: extérieur/ 1: centre / 2: intérieur)
        '   iBarre      [E] :   Indice de la barre dans la grappe, de 1 à 3
        '--------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim zPos As Decimal
        Dim PhiA, PhiE As Decimal
        Dim Uz As Decimal

        '--> Intialisations

        Uz = Me.Enrobage.Etriers_EnrobageZ
        PhiE = Me.Enrobage.Etriers_Phi
        Select Case iPos
            Case 0 : PhiA = Me.Enrobage.LitArma(iArma).PhiExt
            Case 1 : PhiA = Me.Enrobage.LitArma(iArma).PhiMil
            Case 2 : PhiA = Me.Enrobage.LitArma(iArma).PhiInt
        End Select
        '--> Traitement

        Select Case iArma

            Case 0
                '-- LIT INFERIEUR---------------------------
                zPos = -Me.ProfilA.ha + Me.ProfilA.Tfi + Uz + PhiE + PhiA / 2
                If iBarre = 3 Then zPos += PhiA * Math.Sqrt(3) / 2
            Case 1
                '-- LIT CENTRAL ----------------------------
                zPos = zPositionLitArmaEnrobage(1)
            Case 2
                '-- LIT SUPERIEUR---------------------------
                zPos = -Me.ProfilA.Tfs - Uz - PhiE - PhiA / 2
                If iBarre = 3 Then zPos -= PhiA * Math.Sqrt(3) / 2
        End Select

        Return zPos
    End Function

#End Region

#Region " Calcul de propriétés élastiques "

    Public Function MomentElastique(Inertie As Decimal, zANE As Decimal, Moment As Decimal, Optional lCarac As Boolean = False) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   29/04/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du moment élastique de la section
        '-----------------------------------------------------------------------------------------------------------------
        '   Inertie     [E] :   Moment d'inertie de la section
        '   zANE        [E] :   Position ANE élastique
        '   Moment      [E] :   Signe du moment
        '   lCarac      [E] :   Indique si valeur caractéristique du moment
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim kUlt As Decimal
        Dim Sigma, SigmaLim As Decimal
        Dim GammaM0 As Decimal

        '--> Initialisation

        GammaM0 = 1

        '--> Section acier

        '# Semelle supérieure

        Sigma = Moment / Inertie * (zANE)
        SigmaLim = Me.FySup / GammaM0
        kUlt = Math.Abs(SigmaLim / Sigma)

        '# Semelle inférieure

        Sigma = Moment / Inertie * (zANE + Me.ProfilA.ha)
        SigmaLim = Me.FyInf / GammaM0
        kUlt = Math.Min(Math.Abs(SigmaLim / Sigma), kUlt)

        '--> Enrobage partiel

        If Me.lEnrobage Then

        End If

        '--> Dalle

        If Me.lMixte Then

        End If

        '--> Fin

        Return kUlt * Moment * kConvMPaPa

    End Function

#End Region

#Region " Recherche d'un acier compatible dans la base de données "

    Public Sub AssocieAcierCompatible(ByVal FileSteels As String, ByVal FileProfiles As String, ByRef lTrouve As Boolean)
        '--------------------------------------------------------------------------------
        '
        '   06/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Associe à une profilé le premier acier compatible dans la base de données
        '
        '--------------------------------------------------------------------------------
        '
        '   FileSteels      [E] :   Nom du fichier binaire base de données de aciers
        '   FileProfiles    [E] :   Nom du fichier binaire base de données des profilés
        '
        '   lTrouve         [S] :   Indique si on a pu trouver un acier compatible
        '
        '--------------------------------------------------------------------------------
        '
        '   On prend le premier acier S355 disponible
        '   et si on ne le trouve pas, le premier acier tout court
        '
        '--------------------------------------------------------------------------------

        'Dim iAcier As Integer
        'Dim MySteels As New List(Of strucAcierLocal)
        'Dim SteelBase As strucBaseAciers
        'Dim iStd As Short

        'Me.ExtraireAciersCompatibles(FileSteels, FileProfiles, MySteels, SteelBase)

        'Me.AnalyseAciersListe(MySteels, True, Cls_Acier.NUANCEDEFAULT, lTrouve, iAcier)

        'If Not lTrouve Then
        '    Me.AnalyseAciersListe(MySteels, False, "", lTrouve, iAcier)
        'End If

        'If lTrouve Then
        '    Me.Acier.Nuance = MySteels(iAcier).Nuance
        '    Me.Acier.Qualite = MySteels(iAcier).Qualite
        '    Me.Acier.Reduction = MySteels(iAcier).Reduc
        '    Me.Acier.EpMax = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).EpMax
        '    Me.Acier.iBase = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).iBase
        '    Me.Acier.iStandart = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).StIndex

        '    '==V4.00
        '    iStd = SteelBase.IndexStd.IndexOf(SteelBase.Grades(Me.Acier.Nuance).Qualites(Me.Acier.Qualite).ReductionCurv(Me.Acier.Reduction).StIndex)
        '    Me.Acier.iTabStandart = iStd

        '    Me.Acier.Plages.Clear()

        '    For i As Integer = 0 To SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).Plages.Count - 1
        '        Me.Acier.Plages.Add(SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).Plages(i))
        '    Next
        'End If
    End Sub

    Private Sub AnalyseAciersListe(ByVal MySteels As List(Of strucAcierLocal), ByVal lImposedGrade As Boolean,
                                   ByVal MyGrade As String, ByRef lTrouve As Boolean, ByRef iAcier As Integer)
        '--------------------------------------------------------------------------------
        '
        '   21/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Extrait tous les aciers compatibles avec un profilé 
        '
        '--------------------------------------------------------------------------------
        '
        '   FileSteels      [E] :   Nom du fichier binaire base de données de aciers
        '   FileProfiles    [E] :   Nom du fichier binaire base de données des profilés
        '
        '--------------------------------------------------------------------------------

        iAcier = -1

        lTrouve = False

        Do While (Not lTrouve) And iAcier < MySteels.Count - 1
            iAcier += 1
            If lImposedGrade Then
                lTrouve = (MySteels(iAcier).Nuance.Trim.ToUpper = MyGrade.ToUpper.Trim)
            Else
                lTrouve = True
            End If
        Loop
    End Sub

    Private Sub ExtraireAciersCompatibles(ByVal FileSteels As String, ByVal FileProfiles As String,
                                          ByVal MySteels As List(Of strucAcierLocal), CorIndStd As Dictionary(Of Short, Short))
        '--------------------------------------------------------------------------------
        '
        '   21/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Extrait tous les aciers compatibles avec un profilé 
        '
        '--------------------------------------------------------------------------------
        '
        '   MySteels        [E] :   Liste des aciers
        '   lImposedGrade   [E] :   Indique si une nuance est imposée ou pas
        '   MyGrade         [E] :   Nuance eventuellement imposée
        '
        '   lTrouve         [S] :   Indique si on a pu trouver un acier compatible
        '
        '
        '--------------------------------------------------------------------------------

        'Dim CorIndStd As Dictionary(Of Short, Short)
        'Dim lCompatible As Boolean
        'Dim EpMax As Double
        'Dim lIsNuanceCompatibleProfile As Boolean
        'Dim lAdd As Boolean
        'Dim SteelLoc As strucAcierLocal
        'Dim ListeSteel As New List(Of strucAcierLocal)
        'Dim nbComp As Integer

        ''--> Initialisation

        ''InitialiseBaseAciers(FileSteels, MyConst.NFACCES, SteelBase)
        'EpMax = Math.Max(Math.Max(Me.ProfilA.t_fi, Me.ProfilA.t_fs), Me.ProfilA.t_w)
        ''GetTabCorrespondanceIndiceStandart(FileProfiles, CorIndStd)
        'MySteels.Clear()
        'ListeSteel.Clear()
        'nbComp = 0

        ''--> Boucle sur les aciers de la base

        'For Each kvpGrade As KeyValuePair(Of String, strucGrade) In SteelBase.Grades

        '    For Each kvpQualite As KeyValuePair(Of String, strucQualite) In kvpGrade.Value.Qualites

        '        For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv

        '            lCompatible = SteelIsToCompatibleToProfile(EpMax, Me.iStandard, SteelBase, CorIndStd, kvpGrade.Key, kvpQualite.Key, kvpSteel.Key, OptionsDataBase.ChoiceSteel, lIsNuanceCompatibleProfile)

        '            If lCompatible Then
        '                SteelLoc.Nuance = kvpGrade.Key
        '                SteelLoc.Qualite = kvpQualite.Key
        '                SteelLoc.Reduc = kvpSteel.Key
        '                SteelLoc.lAvailable = lIsNuanceCompatibleProfile
        '                ListeSteel.Add(SteelLoc)
        '                If lIsNuanceCompatibleProfile Then nbComp += 1
        '            End If

        '        Next
        '    Next
        'Next

        'For i As Integer = 0 To ListeSteel.Count - 1
        '    If Not ListeSteel(i).lAvailable Then
        '        If (OptionsDataBase.ChoiceSteel = EnuChoiceAcier.BaseIfNoStandardSteel) Then
        '            lAdd = (nbComp = 0)
        '        Else
        '            lAdd = True
        '        End If
        '    Else
        '        lAdd = True
        '    End If
        '    If lAdd Then
        '        MySteels.Add(ListeSteel(i))
        '    End If
        'Next
    End Sub

#End Region

End Class
