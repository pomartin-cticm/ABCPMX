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
    Public b_fs As Decimal

    ''' <summary>
    ''' épaisseur de la semelle supérieure (m)
    ''' </summary>
    Public t_fs As Decimal

    ''' <summary>
    ''' largeur de la semelle inférieure (m)
    ''' </summary>
    Public b_fi As Decimal

    ''' <summary>
    ''' épaisseur de la semelle inférieure (m)
    ''' </summary>
    Public t_fi As Decimal

    ''' <summary>
    ''' rayon du congé de raccordement supérieur (m)
    ''' </summary>
    Public r_cs As Decimal

    ''' <summary>
    ''' rayon du congé de raccordement inférieur (m)
    ''' </summary>
    Public r_ci As Decimal

    ''' <summary>
    ''' hauteur totale de l’âme, mesurée entre le nu intérieur des semelles (m)
    ''' </summary>
    Public h_w As Decimal

    ''' <summary>
    ''' épaisseur de l'âme (m)
    ''' </summary>
    Public t_w As Decimal

    ''' <summary>
    ''' épaisseur de la gorge des cordons de soudure (m)
    ''' </summary>
    Public a As Decimal

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

            pAire = Me.AireFi + Me.AireFs + Me.HauteurAmeHw * Me.t_w + (4 - Math.PI) * (Me.r_ci ^ 2 + Me.r_cs ^ 2) / 2

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
                    Dim Bf As Decimal = Me.b_fs
                    Dim Tf As Decimal = Me.t_fs

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
                    Dim Bf As Decimal = Me.b_fs
                    Dim Tf As Decimal = Me.t_fs
                    Dim Tw As Decimal = Me.t_w
                    Dim Rc As Decimal = Me.r_cs
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

                    Itfs = 1 / 3 * (1 - 0.63 * Me.t_fs / Me.b_fs * (1 - Me.t_fs ^ 4 / 12 / Me.b_fs ^ 4)) * Me.b_fs * Me.t_fs ^ 3
                    Itfi = 1 / 3 * (1 - 0.63 * Me.t_fi / Me.b_fi * (1 - Me.t_fi ^ 4 / 12 / Me.b_fi ^ 4)) * Me.b_fi * Me.t_fi ^ 3
                    Itw = 1 / 3 * Me.HauteurAmeHw * Me.t_w ^ 3
                    Itrs = Me.t_w / Me.t_fs * (0.1 * Me.r_cs / Me.t_fs + 0.15) * (((Me.t_fs + Me.r_cs) ^ 2 + Me.t_w * (Me.r_cs + Me.t_w / 4)) / (2 * Me.r_cs + Me.t_fs)) ^ 4
                    Itri = Me.t_w / Me.t_fi * (0.1 * Me.r_ci / Me.t_fi + 0.15) * (((Me.t_fi + Me.r_ci) ^ 2 + Me.t_w * (Me.r_ci + Me.t_w / 4)) / (2 * Me.r_ci + Me.t_fi)) ^ 4

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
                    Av = Me.HauteurAmeHw * Me.t_w + (4 - Math.PI) * (Me.r_cs ^ 2 + Me.r_ci ^ 2) / 2 _
                       + Me.t_fs * (2 * Me.r_cs + Me.t_w) / 2 _
                       + Me.t_fi * (2 * Me.r_ci + Me.t_w) / 2
                Case Else
                    Av = Me.HauteurAmeHw * Me.t_w
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
            Return Me.ha - Me.t_fs - Me.t_fi
        End Get
    End Property

    ''' <summary>
    ''' Hauteur de l'âme, congés de raccordement exclus
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HauteurAmeDw As Decimal
        Get
            Return Me.ha - Me.t_fs - Me.t_fi - Me.r_ci - Me.r_cs
        End Get
    End Property

    ''' <summary>
    ''' Aire de la semelle supérieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AireFs As Decimal
        Get
            Return Me.b_fs * Me.t_fs
        End Get
    End Property

    ''' <summary>
    ''' Aire de la semelle inférieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AireFi As Decimal
        Get
            Return Me.b_fi * Me.t_fi
        End Get
    End Property

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

        MyModele.AddMaille(Me.AireFs, Me.t_fs, -Me.t_fs / 2, 1, 1, 1, Fy, 1, GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.t_w, Hw, -Me.t_fs - Hw / 2, 1, 1, 1, Fy, (1 - RhoV), GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.AireFi, Me.t_fi, -Me.ha + Me.t_fi / 2, 1, 1, 1, Fy, 1, GammaM0)

        If Me.r_cs > 0 Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.r_cs, -Me.t_fs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

        End If
        If Me.r_ci > 0 Then

            '# Congés inférieurs

            MyModele.AddMailleConges(Me.r_ci, -Me.ha + Me.t_fs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

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

        Me.ProprietesElastiquesMyy(1, True, 1, zANE, Inertie, melrd)

        Wel = Inertie / Math.Max(Math.Abs(zANE), Math.Abs(-Me.ha - zANE))

        Return Wel

    End Function

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

        MyModele.AddMaille(Me.AireFs, Me.t_fs, -Me.t_fs / 2, 1, 1, 1, Fy, 1, GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.t_w, Hw, -Me.t_fs - Hw / 2, 1, 1, 1, Fy, (1 - RhoV), GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.AireFi, Me.t_fi, -Me.ha + Me.t_fi / 2, 1, 1, 1, Fy, 1, GammaM0)

        If Me.r_cs > 0 Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.r_cs, -Me.t_fs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

        End If
        If Me.r_ci > 0 Then

            '# Congés inférieurs

            MyModele.AddMailleConges(Me.r_ci, -Me.ha + Me.t_fs, 1, 1, 1, Fy, (1 - RhoV), GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        'MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.typeProfileAcier = Enum_TypeSectionAcier.Lamine

        '--> Section par défaut peu importe le type
        Me.ha = 0.3
        'Me.h_w = 0.27
        Me.t_w = 0.01
        Me.b_fi = 0.24
        Me.b_fs = 0.24
        Me.t_fi = 0.015
        Me.t_fs = 0.015
        Me.r_cs = 0.01
        Me.r_ci = 0.01
        Me.a = 0.01

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

    Public Shared Sub DeepCopie(ProfilSource As cls_ProfilA, ByRef ProfilCible As cls_ProfilA)

        ProfilCible = ProfilSource.Clone

        If Not (ProfilSource.IndDeliv Is Nothing) Then ProfilCible.IndDeliv = ProfilSource.IndDeliv.Clone
        If (ProfilSource.IndStandart.GetUpperBound(0) > 0) Then ProfilCible.IndStandart = ProfilSource.IndStandart.Clone

    End Sub

#End Region

End Class
