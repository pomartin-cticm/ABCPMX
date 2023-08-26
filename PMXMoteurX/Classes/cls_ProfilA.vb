Imports System.Security.Policy

Public Class cls_ProfilA

#Region " Attributs "

    Public Enum Enum_TypeSectionAcier
        Lamine          ' Profilé laminé
        PRS_Mono_Sym    ' Section PRS bi-symétrique
        PRS_Bi_Sym      ' Section PRS mono-symétrique
    End Enum

    ''' <summary>
    ''' Gamma du profilé de la base de données
    ''' </summary>
    Public Gamme As String

    ''' <summary>
    ''' Nom du profilé de la gamme de la base de données
    ''' </summary>
    Public NomProfile As String

    Private pInertieY As Decimal            ' Inertie de flexion / axe fort
    Private pInertieZ As Decimal            ' Inertie de flexion / axe faible
    Private pModuleWplY As Decimal           ' Module de flexion plastique / axe fort
    Private pModuleWelY As Decimal           ' Module de flexion élastique / axe fort
    Private pModuleWelZ As Decimal           ' Module de flexion élastique / axe faible

#End Region

#Region " Géométrie de la section acier "

    ''' <summary>
    ''' hauteur totale de la section métallique (m)
    ''' </summary>
    Public ha As Decimal

    ''' <summary>
    ''' hauteur du profilé de base (pour les laminés) (m)
    ''' </summary>
    Public hb As Decimal

    ''' <summary>
    ''' largeur de la semelle supérieure (m)
    ''' </summary>
    Public Bfs As Decimal

    ''' <summary>
    ''' épaisseur de la semelle supérieure (m)
    ''' </summary>
    Public Tfs As Decimal

    ''' <summary>
    ''' largeur de la semelle inférieure (m)
    ''' </summary>
    Public Bfi As Decimal

    ''' <summary>
    ''' épaisseur de la semelle inférieure (m)
    ''' </summary>
    Public Tfi As Decimal

    ''' <summary>
    ''' rayon du congé de raccordement supérieur (m)
    ''' </summary>
    Public Rcs As Decimal

    ''' <summary>
    ''' rayon du congé de raccordement inférieur (m)
    ''' </summary>
    Public Rci As Decimal

    ''' <summary>
    ''' hauteur totale de l’âme, mesurée entre le nu intérieur des semelles (m)
    ''' </summary>
    'Private h_w As Decimal

    ''' <summary>
    ''' épaisseur de l'âme (m)
    ''' </summary>
    Public Tw As Decimal

    ''' <summary>
    ''' épaisseur de la gorge des cordons de soudure (m)
    ''' </summary>
    Public aW As Decimal

    ''' <summary>
    ''' Type de la section du profilé métallique
    ''' </summary>
    Public typeProfileAcier As Enum_TypeSectionAcier

    ''' <summary>
    ''' Largeur du plat utilisé avec les slim floor
    ''' </summary>
    Public Plat_b As Decimal

    ''' <summary>
    ''' Epaisseur du plat utilisé avec les slim floor
    ''' </summary>
    Public Plat_t As Decimal

    ''' <summary>
    ''' Indices conditions de livraison
    ''' </summary>
    Public IndDeliv() As Short

    ''' <summary>
    ''' Indices normes acier compatibles
    ''' </summary>
    Public IndStandart() As Short

#End Region

#Region " Propriétés "
    ''' <summary>
    ''' Aire de la section du profilé
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Aire As Decimal
        '-------------------------------------------------------------------------------------------------------------------------------
        '   12/07/23 :  Création POM
        '-------------------------------------------------------------------------------------------------------------------------------
        Get
            Dim pAire As Decimal

            pAire = Me.AireFi + Me.AireFs + Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rci ^ 2 + Me.Rcs ^ 2) / 2

            Return pAire
        End Get
    End Property

    ''' <summary>
    ''' Inertie de gauchissement
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieW As Decimal
        '-------------------------------------------------------------------------------------------------------------------------------
        '   12/07/23 :  Création POM
        '-------------------------------------------------------------------------------------------------------------------------------
        Get
            '--> Déclaration

            Dim pInertieW As Decimal

            '--> Calcul

            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine
                    Dim Bf As Decimal = Me.Bfs
                    Dim Tf As Decimal = Me.Tfs

                    pInertieW = Tf * Bf ^ 3 / 24 * (ha - Tf) ^ 2

                Case Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym
            End Select

            '--> Fin

            Return pInertieW
        End Get
    End Property

    ''' <summary>
    ''' Intertie de torsion du profilé
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieT As Decimal
        '-------------------------------------------------------------------------------------------------------------------------------
        '   12/07/23 :  Création POM
        '-------------------------------------------------------------------------------------------------------------------------------
        '   Inertie de torsion du profilé
        '-------------------------------------------------------------------------------------------------------------------------------

        Get

            '--> Déclaration

            Dim pInertieT As Decimal

            '--> Calcul

            Select Case Me.typeProfileAcier
                '====================================================================================================
                '== LAMINES                                                                                         =
                '====================================================================================================
                Case Enum_TypeSectionAcier.Lamine
                    Dim Bf As Decimal = Me.Bfs
                    Dim Tf As Decimal = Me.Tfs
                    Dim Tw As Decimal = Me.Tw
                    Dim Rc As Decimal = Me.Rcs
                    Dim Hw As Decimal = Me.HauteurAmeHw

                    Dim Alpha1 As Decimal
                    Dim DiaD1 As Decimal

                    ''=== Formume Annexe B.2.2 de l apublication P385 du SCI (formule de Darwish)

                    Alpha1 = -0.042 + 0.2204 * Tw / Tf + 0.1355 * Rc / Tf - 0.0865 * Rc * Tw / Tf ^ 2 - 0.0725 * (Tw / Tf) ^ 2

                    DiaD1 = ((Tf + Rc) ^ 2 + (Rc + Tw / 2) ^ 2 - Rc ^ 2) / (2 * Rc + Tf)

                    pInertieT = 2 / 3 * (Bf - 0.63 * Tf) * Tf ^ 3 _
                              + 1 / 3 * Hw * Tw ^ 3 _
                              + 2 * Alpha1 * DiaD1 ^ 4

                '====================================================================================================
                '== PRS                                                                                             =
                '====================================================================================================

                Case Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym

                    '=== Formule du guide CTICM sur le déversement, Annexe A2

                    Dim Itfs, Itfi As Decimal
                    Dim Itrs, Itri As Decimal
                    Dim Itw As Decimal

                    Itfs = 1 / 3 * (1 - 0.63 * Me.Tfs / Me.Bfs * (1 - Me.Tfs ^ 4 / 12 / Me.Bfs ^ 4)) * Me.Bfs * Me.Tfs ^ 3
                    Itfi = 1 / 3 * (1 - 0.63 * Me.Tfi / Me.Bfi * (1 - Me.Tfi ^ 4 / 12 / Me.Bfi ^ 4)) * Me.Bfi * Me.Tfi ^ 3
                    Itw = 1 / 3 * Me.HauteurAmeHw * Me.Tw ^ 3
                    Itrs = Me.Tw / Me.Tfs * (0.1 * Me.Rcs / Me.Tfs + 0.15) * (((Me.Tfs + Me.Rcs) ^ 2 + Me.Tw * (Me.Rcs + Me.Tw / 4)) / (2 * Me.Rcs + Me.Tfs)) ^ 4
                    Itri = Me.Tw / Me.Tfi * (0.1 * Me.Rci / Me.Tfi + 0.15) * (((Me.Tfi + Me.Rci) ^ 2 + Me.Tw * (Me.Rci + Me.Tw / 4)) / (2 * Me.Rci + Me.Tfi)) ^ 4

                    pInertieT = Itfs + Itfi + Itw + Itrs + Itri

            End Select

            '--> Fin

            Return pInertieT

            '''=== Formule du catalogue AM V 2008

            ''pInertieT = 2 / 3 * (Bf - 0.63 * Tf) * Tf ^ 3 _
            ''          + 1 / 3 * Hw * Tw ^ 3 _
            ''          + 2 * Tw / Tf * (0.145 + 0.1 * Rc / Tf) * (((Rc + Tw / 2) ^ 2 + (Rc + Tf) ^ 2 - Rc ^ 2) / (2 * Rc + Tf)) ^ 4

            '''=== Formule du guide CTICM sur le déversement, Annexe A1

            ''pInertieT = 2 / 3 * (1 - 0.63 * Tf / Bf * (1 - Tf ^ 4 / 12 / Bf ^ 4)) * Bf * Tf ^ 3 _
            ''          + 1 / 3 * Hw * Tw ^ 3 _
            ''          + 2 * Tw / Tf * (0.1 * Rc / Tf + 0.15) * (((Tf + Rc) ^ 2 + Tw * (Rc + Tw / 4)) / (2 * Rc + Tf)) ^ 4

        End Get
    End Property

    ''' <summary>
    ''' Aire de cisaillement
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AireAv As Decimal
        Get
            Dim Av As Decimal = 0
            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine
                    Av = Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rcs ^ 2 + Me.Rci ^ 2) / 2 _
                       + Me.Tfs * (2 * Me.Rcs + Me.Tw) / 2 _
                       + Me.Tfi * (2 * Me.Rci + Me.Tw) / 2
                Case Else
                    Av = Me.HauteurAmeHw * Me.Tw
            End Select
            Return Av
        End Get
    End Property

    ''' <summary>
    ''' Hauteur de l'âme entre le nu intérieur des semelles
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HauteurAmeHw As Decimal
        Get
            Return Me.ha - Me.Tfs - Me.Tfi
        End Get
    End Property

    ''' <summary>
    ''' Hauteur de l'âme, congés de raccordement exclus
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HauteurAmeDw As Decimal
        Get
            Return Me.ha - Me.Tfs - Me.Tfi - Me.Rci - Me.Rcs
        End Get
    End Property

    ''' <summary>
    ''' Aire de la semelle supérieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AireFs As Decimal
        Get
            Return Me.Bfs * Me.Tfs
        End Get
    End Property

    ''' <summary>
    ''' Aire de la semelle inférieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AireFi As Decimal
        Get
            Return Me.Bfi * Me.Tfi
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie du profilé selon l'axe y-y
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieY As Decimal
        Get
            Return pInertieY
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie du profilé selon l'axe z-z
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieZ As Decimal
        Get
            Return pInertieZ
        End Get
    End Property

    ''' <summary>
    ''' Module de flexion élastique du profilé selon l'axe y-y
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ModuleWelY As Decimal
        Get
            Return pModuleWelY
        End Get
    End Property

    ''' <summary>
    ''' Module de flexion élastique du profilé selon l'axe z-z
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ModuleWelZ As Decimal
        Get
            Return pModuleWelZ
        End Get
    End Property

    ''' <summary>
    ''' Module de flexion plastique du profilé selon l'axe y-y
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ModuleWplY As Decimal
        Get
            Return pModuleWplY
        End Get
    End Property

    Public Sub InitialiseProprietes()
        '-------------------------------------------------------------
        '   11/08/23 :  Création - POM
        '-------------------------------------------------------------
        '   Initialisation des propriétés calculées avec la routine itérative (flexion)
        '-------------------------------------------------------------
        '-------------------------------------------------------------

        '--> Déclaration

        Dim zAN As Decimal
        Dim MRd As Decimal
        Dim Bfm As Decimal = Math.Max(Me.Bfi, Me.Bfs)

        '--> Calcul

        Me.pModuleWplY = ModuleFlexionPlastiqueYY()

        ProprietesElastiquesMyy(1, False, 1, zAN, Me.pInertieY, MRd)

        Me.pModuleWelY = Me.pInertieY / Math.Max(Math.Abs(zAN), Math.Abs(-Me.ha - zAN))

        ProprietesElastiquesMzz(False, 1, zAN, Me.pInertieZ, MRd)

        Me.pModuleWelZ = Me.pInertieZ / Math.Max(Bfm / 2 - zAN, zAN + Bfm / 2)

    End Sub

#End Region

#Region " Propriétés plastiques en flexion "

    Public Function ModuleFlexionPlastiqueYY() As Decimal
        '-------------------------------------------------------------------------------------------------------------------
        '   13/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul du module plastique du profilé, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        'Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        Const Fy As Decimal = 235
        Const GammaM0 As Decimal = 1
        Const Signe As Decimal = 1
        Dim zANP, MplRd, Wpl As Decimal
        Const lValeurRd As Decimal = True

        '--> Initialisation

        Hw = Me.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.AireFs, Me.Tfs, -Me.Tfs / 2, 1, 1, 1, Fy, 1, GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.Tw, Hw, -Me.Tfs - Hw / 2, 1, 1, 1, Fy, (1 - RhoV), GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.AireFi, Me.Tfi, -Me.ha + Me.Tfi / 2, 1, 1, 1, Fy, 1, GammaM0)

        If Me.Rcs > 0 Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.Rcs, -Me.Tfs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

        End If
        If Me.Rci > 0 Then

            '# Congés inférieurs

            MyModele.AddMailleConges(Me.Rci, -Me.ha + Me.Tfs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

        '--> Module plastique

        Wpl = MplRd / (Fy * kConvMPaPa)

        Return Wpl
    End Function

#End Region

#Region " Propriétés élastiques en flexion "

    Public Function ModuleFlexionElastiqueYY()
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul du module élastique de flexion du profilé, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Inertie, zANE, MelRd As Decimal
        Dim Wel As Decimal
        Const Fy As Decimal = 235

        '--> Calculs

        Me.ProprietesElastiquesMyy(1, True, 1, zANE, Inertie, MelRd)

        Wel = Inertie / Math.Max(Math.Abs(zANE), Math.Abs(-Me.ha - zANE))

        Return Wel

    End Function

    Public Sub ProprietesElastiquesMzz(lValeurRd As Boolean, GammaM0 As Decimal,
                                       ByRef zANE As Decimal, ByRef InertieZ As Decimal, ByRef MelRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe faible
        '-------------------------------------------------------------------------------------------------------------------
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   nEqEc       [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   zANE        [E] :   Position axe neutre élastique
        '   MelRd       [E] :   Moment élastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Const Fy As Decimal = 235
        Const Signe As Decimal = 1
        Dim lLamine As Boolean = (Me.typeProfileAcier = Enum_TypeSectionAcier.Lamine)

        '--> Initialisation

        Hw = Me.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.AireFs, Me.Bfs, 0, 1, 1, 1, Fy, 1, GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.Tw, Me.Tw, 0, 1, 1, 1, Fy, 1, GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.AireFi, Me.Bfi, 0, 1, 1, 1, Fy, 1, GammaM0)

        If lLamine Then

            '# Congés sous l'âme

            MyModele.AddMailleConges(Me.Rcs, -Me.Tw / 2, 1, 1, 1, Fy, 1, GammaM0, Cls_Maille.EnuTypeMaille.CongeSup, 0.5)
            MyModele.AddMailleConges(Me.Rci, -Me.Tw / 2, 1, 1, 1, Fy, 1, GammaM0, Cls_Maille.EnuTypeMaille.CongeSup, 0.5)

            '# Congés au dessus de l'âme

            MyModele.AddMailleConges(Me.Rcs, +Me.Tw / 2, 1, 1, 1, Fy, 1, GammaM0, Cls_Maille.EnuTypeMaille.CongeInf, 0.5)
            MyModele.AddMailleConges(Me.Rci, +Me.Tw / 2, 1, 1, 1, Fy, 1, GammaM0, Cls_Maille.EnuTypeMaille.CongeInf, 0.5)

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieZ = MyModele.InertieFlexion(Signe, zANE)

    End Sub

    Public Sub ProprietesElastiquesMyy(Signe As Decimal, lValeurRd As Boolean, GammaM0 As Decimal,
                                       ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef MelRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   13/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   nEqEc       [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   zANE        [E] :   Position axe neutre élastique
        '   MelRd       [E] :   Moment élastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        'Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        Const Fy As Decimal = 235

        '--> Initialisation

        Hw = Me.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.AireFs, Me.Tfs, -Me.Tfs / 2, 1, 1, 1, Fy, 1, GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.Tw, Hw, -Me.Tfs - Hw / 2, 1, 1, 1, Fy, (1 - RhoV), GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.AireFi, Me.Tfi, -Me.ha + Me.Tfi / 2, 1, 1, 1, Fy, 1, GammaM0)

        If Me.Rcs > 0 Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.Rcs, -Me.Tfs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

        End If
        If Me.Rci > 0 Then

            '# Congés inférieurs

            MyModele.AddMailleConges(Me.Rci, -Me.ha + Me.Tfs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieY, lValeurRd)

    End Sub

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.typeProfileAcier = Enum_TypeSectionAcier.Lamine

        '--> Section par défaut peu importe le type
        Me.ha = 0.3
        'Me.h_w = 0.27
        Me.Tw = 0.01
        Me.Bfi = 0.24
        Me.Bfs = 0.24
        Me.Tfi = 0.015
        Me.Tfs = 0.015
        Me.Rcs = 0.01
        Me.Rci = 0.01
        Me.aW = 0.01

        Me.Gamme = "IPE"
        Me.NomProfile = "IPE 300"

        Me.Plat_b = 0.35
        Me.Plat_t = 0.01
    End Sub

#End Region

#Region " Fonctions de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Function DeepClone(ByVal ProfilASource As cls_ProfilA, ByRef ProfilACible As cls_ProfilA)
        ProfilACible = ProfilASource.Clone()

        ReDim ProfilACible.IndDeliv(ProfilASource.IndDeliv.GetUpperBound(0))
        ProfilACible.IndDeliv = ProfilASource.IndDeliv.Clone

        ReDim ProfilACible.IndStandart(ProfilASource.IndStandart.GetUpperBound(0))
        ProfilACible.IndStandart = ProfilASource.IndStandart.Clone
    End Function

    Public Shared Sub DeepCopie(ProfilSource As cls_ProfilA, ByRef ProfilCible As cls_ProfilA)

        ProfilCible = ProfilSource.Clone

        If Not (ProfilSource.IndDeliv Is Nothing) Then ProfilCible.IndDeliv = ProfilSource.IndDeliv.Clone
        If (ProfilSource.IndStandart.GetUpperBound(0) > 0) Then ProfilCible.IndStandart = ProfilSource.IndStandart.Clone

    End Sub

#End Region

End Class
