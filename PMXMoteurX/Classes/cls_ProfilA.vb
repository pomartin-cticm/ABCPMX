Imports System.Security.Policy

Public Class cls_ProfilA

#Region " Attributs "

    Public Enum Enum_TypeSectionAcier
        Lamine                          ' Profilé laminé
        PRS_Mono_Sym                    ' Section PRS bi-symétrique
        PRS_Bi_Sym                      ' Section PRS mono-symétrique
        LamineSlimSAB                   ' Section slim floor à base de profilé laminé
        LamineSlimIFBA                  ' Section slim floor à base de profilé laminé
        LamineSlimIFBB                  ' Section slim floor à base de profilé laminé
        LamineSlimSFB                   ' Section slim floor à base de profilé laminé
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
    Private pModuleWplY As Decimal          ' Module de flexion plastique / axe fort
    Private pModuleWelY As Decimal          ' Module de flexion élastique / axe fort
    Private pModuleWplZ As Decimal          ' Module de flexion plastique / axe faible
    Private pModuleWelZ As Decimal          ' Module de flexion élastique / axe faible

    Private pzCdG As Decimal                ' Position du CdG
    Private pzS As Decimal                  ' Position du centre de cisaillement / cdg

#End Region

#Region " Géométrie de la section acier "

    Public ha As Decimal                            ' hauteur totale de la section métallique (m)
    Public hb As Decimal                            ' hauteur du profilé de base (pour les laminés) (m)

    Public Bfs As Decimal                           ' largeur de la semelle supérieure (m)
    Public Tfs As Decimal                           ' épaisseur de la semelle supérieure (m)

    Public Bfi As Decimal                           ' largeur de la semelle inférieure (m)
    Public Tfi As Decimal                           ' épaisseur de la semelle inférieure (m)

    Public Rcs As Decimal                           ' rayon du congé de raccordement supérieur (m)
    Public Rci As Decimal                           ' rayon du congé de raccordement inférieur (m)

    Public Tw As Decimal                            ' épaisseur de l'âme (m)

    Public aW As Decimal                            ' épaisseur de la gorge des cordons de soudure (m)

    Public typeProfileAcier As Enum_TypeSectionAcier ' Type de la section du profilé métallique

    Public Plat_b As Decimal                        ' Largeur du plat utilisé avec les slim floor
    Public Plat_t As Decimal                        ' Epaisseur du plat utilisé avec les slim floor

    Public IndDeliv() As Short                      ' Indices conditions de livraison
    Public IndStandart() As Short                   ' Indices normes acier compatibles

#End Region

#Region " Propriétés "

    Public Sub InitialiseSoudureMini(ByRef Gorges() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des gorges de soudure mini d'après les recommandations CNC2M/BNCM 0175
        '----------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre traitée
        '   Gorges              [S] :   Gorge mini des soudures
        '----------------------------------------------------------------------------------------------------------

        Gorges(0) = Me.EpaisseurMinSoudureAmeSemelle(True)
        Gorges(1) = Me.EpaisseurMinSoudureAmeSemelle(True)


    End Sub

    Public Function EpaisseurMinSoudureAmeSemelle(lSup) As Decimal
        '-------------------------------------------------------------------------------------------------------------------------------
        '   22/02/24 :  Création POM
        '-------------------------------------------------------------------------------------------------------------------------------
        '   Calcule de l'épaisseur mini des cordons âme/semelle d'après BNCM N 0175
        '-------------------------------------------------------------------------------------------------------------------------------
        '   lSup    [E] :   Indique si semelle sup ou inf
        '-------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Const EPMINIGORGE As Decimal = 0.003            ' 3 mm mini
        Dim tMax As Decimal
        Const kUnit As Decimal = 1000                   ' Pour les calculs en mm

        '--( Initialisation

        If lSup Then
            tMax = Math.Max(Me.Tfs, Me.Tw)
        Else
            tMax = Math.Max(Me.Tfi, Me.Tw)
        End If

        '--( Calcul

        Return Math.Max(EPMINIGORGE, (Math.Sqrt(tMax * kUnit) - 0.5) / kUnit)

    End Function

    Public ReadOnly Property ElancementAme As Decimal
        '-------------------------------------------------------------------------------------------------------------------------------
        '   22/02/24 :  Création POM
        '-------------------------------------------------------------------------------------------------------------------------------
        '   Calcule l'élancement de l'âme pour le voilement par cisaillement
        '-------------------------------------------------------------------------------------------------------------------------------
        Get
            Return Me.HauteurAmeHw / Me.Tw
        End Get
    End Property


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

            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym, Enum_TypeSectionAcier.LamineSlimSAB
                    pAire = Me.AireFi + Me.AireFs + Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rci ^ 2 + Me.Rcs ^ 2) / 2
                Case Enum_TypeSectionAcier.LamineSlimSFB
                    pAire = Me.AireFi + Me.AireFs + Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rci ^ 2 + Me.Rcs ^ 2) / 2 + Me.Plat_b * Me.Plat_t
                Case Enum_TypeSectionAcier.LamineSlimIFBA
                    pAire = Me.AireFs + Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rcs ^ 2) / 2 + Me.Plat_b * Me.Plat_t
                Case Enum_TypeSectionAcier.LamineSlimIFBB
                    pAire = Me.AireFi + Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rci ^ 2) / 2 + Me.Plat_b * Me.Plat_t
            End Select


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

            ''''=== Formule du catalogue AM V 2008

            ''pInertieT = 2 / 3 * (Bf - 0.63 * Tf) * Tf ^ 3 _
            ''          + 1 / 3 * Hw * Tw ^ 3 _
            ''          + 2 * Tw / Tf * (0.145 + 0.1 * Rc / Tf) * (((Rc + Tw / 2) ^ 2 + (Rc + Tf) ^ 2 - Rc ^ 2) / (2 * Rc + Tf)) ^ 4

            ''''=== Formule du guide CTICM sur le déversement, Annexe A1

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
                Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.LamineSlimSAB, Enum_TypeSectionAcier.LamineSlimSFB
                    Av = Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rcs ^ 2 + Me.Rci ^ 2) / 2 _
                       + Me.Tfs * (2 * Me.Rcs + Me.Tw) / 2 _
                       + Me.Tfi * (2 * Me.Rci + Me.Tw) / 2
                Case Enum_TypeSectionAcier.PRS_Mono_Sym, Enum_TypeSectionAcier.PRS_Bi_Sym
                    Av = Me.HauteurAmeHw * Me.Tw
                Case Enum_TypeSectionAcier.LamineSlimIFBA
                    Av = Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rcs ^ 2) / 2 _
                       + Me.Tfs * (2 * Me.Rcs + Me.Tw) / 2
                Case Enum_TypeSectionAcier.LamineSlimIFBB
                    Av = Me.HauteurAmeHw * Me.Tw + (4 - Math.PI) * (Me.Rci ^ 2) / 2 _
                       + Me.Tfi * (2 * Me.Rci + Me.Tw) / 2
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
            Dim MyHw As Decimal = 0
            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Mono_Sym, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.LamineSlimSAB, Enum_TypeSectionAcier.LamineSlimSFB
                    MyHw = Me.ha - Me.Tfs - Me.Tfi
                Case Enum_TypeSectionAcier.LamineSlimIFBA
                    MyHw = Me.ha - Me.Tfs - Me.Plat_t
                Case Enum_TypeSectionAcier.LamineSlimIFBB
                    MyHw = Me.ha - Me.Plat_t - Me.Tfi
            End Select
            Return MyHw
        End Get
    End Property

    ''' <summary>
    ''' Hauteur de l'âme, congés de raccordement exclus
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HauteurAmeDw As Decimal
        Get
            Dim MyDw As Decimal
            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Mono_Sym, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.LamineSlimSAB, Enum_TypeSectionAcier.LamineSlimSFB
                    MyDw = Me.ha - Me.Tfs - Me.Tfi - Me.Rci - Me.Rcs
                Case Enum_TypeSectionAcier.LamineSlimIFBA
                    MyDw = Me.ha - Me.Tfs - Me.Rcs - Me.Plat_t
                Case Enum_TypeSectionAcier.LamineSlimIFBB
                    MyDw = Me.ha - Me.Plat_t - Me.Tfi - Me.Rci
            End Select
            Return MyDw
        End Get
    End Property

    ''' <summary>
    ''' Ordonnée de la fibre supérieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property zRefAraseSup As Decimal
        Get
            Dim myZ As Decimal = 0
            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym
                    myZ = 0
                Case Enum_TypeSectionAcier.LamineSlimSFB
                    myZ = Me.hb
                Case Enum_TypeSectionAcier.LamineSlimIFBA
                    myZ = Me.ha - Me.Plat_t
                Case Enum_TypeSectionAcier.LamineSlimIFBB
                    myZ = Me.ha - Me.Tfi
                Case Enum_TypeSectionAcier.LamineSlimSAB
                    myZ = Me.hb - Me.Tfi
            End Select
            Return myZ
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
    ''' Moment d'inertie de la semelle supérieure / yy
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieYFs As Decimal
        Get
            Return Me.Bfs * Me.Tfs ^ 3 / 12
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie de la semelle supérieure / zz
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieZFs As Decimal
        Get
            Return Me.Bfs ^ 3 * Me.Tfs / 12
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie de la semelle inférieure / yy
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieYFi As Decimal
        Get
            Return Me.Bfi * Me.Tfi ^ 3 / 12
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie de la semelle inférieure / zz
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieZFi As Decimal
        Get
            Return Me.Bfi ^ 3 * Me.Tfi / 12
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie de l'âme / yy
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieYW As Decimal
        Get
            Return Me.HauteurAmeHw ^ 3 * Me.Tw / 12
        End Get
    End Property

    ''' <summary>
    ''' Moment d'inertie de l'âme / zz
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieZW As Decimal
        Get
            Return Me.HauteurAmeHw * Me.Tw ^ 3 / 12
        End Get
    End Property


    ''' <summary>
    ''' Aire du plat soudé dans le cas d'un profilé SFB, IFB-A et IFB-B
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AirePlat As Decimal
        Get
            Return Me.Plat_b * Me.Plat_t
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

    ''' <summary>
    ''' Module de flexion plastique du profilé selon l'axe z-z
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ModuleWplz As Decimal
        Get
            Return pModuleWplZ
        End Get
    End Property

    ''' <summary>
    ''' Rayon de giration du profilé selon l'axe Y-Y
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RayonGirationiY As Decimal
        Get
            Dim rayon As Decimal
            rayon = Math.Sqrt(Me.InertieY / Me.Aire)

            Return rayon
        End Get
    End Property

    ''' <summary>
    ''' Rayon de giration du profilé selon l'axe Z-Z
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RayonGirationiZ As Decimal
        Get
            Dim rayon As Decimal
            rayon = Math.Sqrt(Me.InertieZ / Me.Aire)

            Return rayon
        End Get
    End Property

    ''' <summary>
    ''' Rayon de giration polaire du profilé 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RayonGirationPolaire As Decimal
        Get
            Dim rayon As Decimal
            rayon = Math.Sqrt((Me.pInertieY + Me.pInertieZ) / Me.Aire + Me.pzS ^ 2)

            Return rayon
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

        Dim zANE, zANP As Decimal
        Dim MelRd, MplRd As Decimal
        Dim Bfm As Decimal = Math.Max(Me.Bfi, Me.Bfs)

        '--> Calcul

        Me.ModuleFlexionYY(Me.pModuleWelY, Me.pModuleWplY)

        ProprietesMyy(1, False, 1, zANE, Me.pInertieY, MelRd, zANP, MplRd)
        Me.pzS = Me.PositionCentreS_MonoSym(zANE)

        Me.pzCdG = zANE

        Me.ModuleFlexionZZ(Me.pModuleWelZ, Me.pModuleWplZ)
        ProprietesMzz(False, 1, zANE, Me.pInertieZ, MelRd, zANP, MplRd)


    End Sub

    Public ReadOnly Property zCdG As Decimal
        Get
            Dim zANE As Decimal
            'Dim zANE, zANP As Decimal
            'Dim MelRd, MplRd As Decimal
            'ProprietesMyy(1, False, 1, zANE, Me.pInertieY, MelRd, zANP, MplRd)
            Me.ProprietesElastiques(1, zANE, Me.pInertieY)
            Return zANE
        End Get
    End Property

    Private Function InertieConge(Rayon As Decimal) As Decimal
        '-------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------
        '   Calcul de l'inertie d'un congé de raccordement (1 SEUL)
        '-------------------------------------------------------------
        '   Rayon   [E] :   Rayon du congé
        '-------------------------------------------------------------

        Dim PI As Decimal = Math.PI

        Return (1 / 3 - PI / 16 - 1 / (9 * (4 - PI))) * Rayon ^ 4

    End Function

    Public Function PositionCentreS() As Decimal
        '-------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------
        '   Calcul de la position du centre de cisaillement par rapport cdg
        '-------------------------------------------------------------
        '-------------------------------------------------------------

        Dim zS As Decimal

        Select Case Me.typeProfileAcier
            Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym
                zS = 0
                zS = PositionCentreS_MonoSym()
            Case Else

        End Select

        Return zS
    End Function

    Private Function PositionCentreS_MonoSym() As Decimal
        '-------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------
        '   Calcul de la position du centre de cisaillement par rapport au cdg
        '   Pour une section monosymétrique
        '-------------------------------------------------------------
        '-------------------------------------------------------------

        Return PositionCentreS_MonoSym(Me.zCdG)

    End Function

    Private Function PositionCentreS_MonoSym(zPos_CdG As Decimal) As Decimal
        '-------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------
        '   Calcul de la position du centre de cisaillement par rapport cdg
        '   Pour une section monosymétrique
        '-------------------------------------------------------------
        '   zPos_CdG    [E] :   Position du cdg
        '-------------------------------------------------------------

        '--> Déclaration

        Dim zS As Decimal
        Dim IneZ As Decimal
        Dim izfS, izfI As Decimal
        Dim zfS, zfI As Decimal
        Dim izW, zW As Decimal
        Dim irS, irI As Decimal
        Dim zrS, zrI As Decimal
        Dim arS, arI As Decimal
        Dim vrS, vrI As Decimal
        Dim PI As Decimal
        Dim zG As Decimal = zPos_CdG

        '--> Calcul

        PI = Math.PI

        arS = (4 - PI) * Me.Rcs ^ 2 / 4
        arI = (4 - PI) * Me.Rci ^ 2 / 4

        vrS = (1 - 2 / (3 * (4 - PI))) * Me.Rcs
        vrI = (1 - 2 / (3 * (4 - PI))) * Me.Rci

        izfS = Me.InertieZFs
        izfI = Me.InertieZFi
        izW = Me.InertieZW
        irS = Me.InertieConge(Me.Rcs)
        irI = Me.InertieConge(Me.Rci)

        zfS = -Me.Tfs / 2 - zG
        zfI = -Me.ha + Me.Tfi / 2 - zG
        zW = -Me.Tfs - Me.HauteurAmeHw / 2 - zG
        zrS = -Me.Tfs - vrS - zG
        zrI = -Me.ha + Me.Tfi + vrI - zG

        IneZ = izfS + izfI + izW + 2 * irS + 2 * irI + 2 * arS * (Me.Tw / 2 + vrS) ^ 2 + 2 * arI * (Me.Tw / 2 + vrI) ^ 2
        zS = (izfS * zfS + izfI * zfI + izW * zW + 2 * irS * zrS + 2 * irI * zrI) / IneZ

        Return zS

    End Function

    Public Function BetaZ() As Decimal
        '-------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------
        '   Calcul du coefficient de Wagner pour
        '   un profilé laminé ou une section PRS quelconque
        '   D'après Annexe du guide déversement
        '-------------------------------------------------------------

        '-------------------------------------------------------------

        '--> Déclaration

        Dim zANE, vInertieY, MelRd, zANP, MplRd As Decimal
        Dim zS As Decimal
        Dim Wagner As Decimal
        Dim Numerateur As Decimal

        Dim izfS, izfI As Decimal
        Dim zfS, zfI As Decimal
        Dim izW, zW, awW, iyW As Decimal
        Dim irS, irI As Decimal
        Dim zrS, zrI As Decimal
        Dim arS, arI As Decimal
        Dim vrS, vrI As Decimal

        Dim AfS, AfI As Decimal
        Dim iyfS, iyfI As Decimal

        Dim PI As Decimal = Math.PI

        '--> Propriétés YY

        Me.ProprietesMyy(1, False, 1, zANE, vInertieY, MelRd, zANP, MplRd)

        '--> Centre de cisaillement

        zS = Me.PositionCentreS_MonoSym(zANE)

        '--> Propriétés des semelles

        AfS = Me.AireFs
        iyfS = Me.InertieYFs
        izfS = Me.InertieZFs
        zfS = -Me.Tfs / 2 - zANE

        AfI = Me.AireFi
        iyfI = Me.InertieYFi
        izfI = Me.InertieZFi
        zfI = -Me.ha + Me.Tfi / 2 - zANE

        '--> Propriétés de l'âme

        awW = Me.HauteurAmeHw * Me.Tw
        iyW = Me.InertieYW
        izW = Me.InertieZW
        zW = -Me.Tfs - Me.HauteurAmeHw / 2 - zANE

        '--> Propriétés de congés de raccordement

        vrS = (1 - 2 / (3 * (4 - PI))) * Me.Rcs
        vrI = (1 - 2 / (3 * (4 - PI))) * Me.Rci

        arS = (4 - PI) * Me.Rcs ^ 2 / 4
        arI = (4 - PI) * Me.Rci ^ 2 / 4
        irS = Me.InertieConge(Me.Rcs)
        irI = Me.InertieConge(Me.Rci)

        zrS = -Me.Tfs - vrS - zANE
        zrI = -Me.ha + Me.Tfi + vrI - zANE

        '--> Formule finale

        Numerateur = zfS * (izfS + AfS * zfS ^ 2 + 3 * iyfS)
        Numerateur += zfI * (izfI + AfI * zfI ^ 2 + 3 * iyfI)
        Numerateur += zW * (izW + awW * zW ^ 2 + 3 * iyW)
        Numerateur += 2 * zrS * (4 * irS + arS * zrS ^ 2)
        Numerateur += 2 * zrI * (4 * irI + arI * zrI ^ 2)

        Wagner = Numerateur / (2 * vInertieY) - zS
        Return Wagner
    End Function

    Public Function RayonGirationPolaireCalcul() As Decimal
        '-------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------
        '   Calcul du rayon de giration polaire du profilé
        '-------------------------------------------------------------

        '-->  Déclaration

        Dim iZero As Decimal
        Dim vInertieY, vInertieZ As Decimal
        Dim vAire As Decimal
        Dim zANE_Y, zANE_Z As Decimal
        Dim zANP_Y As Decimal
        Dim MelRd_Y, MelRd_Z As Decimal
        Dim MplRd_Y, MplRd_Z As Decimal

        '--> Calcul

        Me.ProprietesMyy(1, False, 1, zANE_Y, vInertieY, MelRd_Y, zANP_Y, MplRd_Y)
        Me.ProprietesMzz(False, 1, zANE_Z, vInertieZ, MelRd_Z, zANE_Z, MplRd_Z)
        vAire = Me.Aire

        iZero = Math.Sqrt((vInertieY + vInertieZ) / vAire + Me.PositionCentreS_MonoSym(zANE_Y) ^ 2)
        Return iZero

    End Function

    Public Function MomentStatiqueFSup(zG As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------
        '   21/02/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Calcul du moment statique de la semelle supérieure / cdg
        '-----------------------------------------------------------------------------------------------------------
        '   zG      [E] :   Position du cdg
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mStat As Decimal

        '--( Calcul

        mStat = Me.Bfs * Me.Tfs * (-Me.Tfs / 2 - zG)

        Return mStat

    End Function

    Public Function MomentStatiqueFInf(zG As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------
        '   21/02/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Calcul du moment statique de la semelle inférieure / cdg
        '-----------------------------------------------------------------------------------------------------------
        '   zG      [E] :   Position du cdg
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mStat As Decimal

        '--( Calcul

        mStat = Me.Bfi * Me.Tfi * (zG - (-Me.ha + Me.Tfi / 2))

        Return mStat

    End Function

    Public Function MomentStatiqueTeSup(zG As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------
        '   21/02/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Calcul du moment statique de la semelle inférieure / cdg
        '-----------------------------------------------------------------------------------------------------------
        '   zG      [E] :   Position du cdg
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mStat As Decimal

        '--( Calcul

        mStat = Me.MomentStatiqueFSup(zG) + (zG - Me.Tfs) ^ 2 * Me.Tw / 2

        Return mStat
    End Function

#End Region

#Region " Périmètres section et massiveté "

    ''' <summary>
    ''' Calcul du périmetre développé par la section (utile pour le calcul de la surface de peinture)
    ''' </summary>
    ''' <param name="lAvecFaceSup">indique si on prend en compte la face supérieure de la semelle sup ou non</param>
    ''' <returns></returns>
    Public Function PerimetreSection(lAvecFaceSup As Boolean) As Decimal
        Dim perimetre As Decimal = 0

        Select Case Me.typeProfileAcier
            Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.LamineSlimSAB
                perimetre = Me.PerimetreProfilsLamineUsuels(lAvecFaceSup)
            Case Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym
                perimetre = Me.PerimetreProfilsPRS(lAvecFaceSup)
            Case Enum_TypeSectionAcier.LamineSlimSFB
                perimetre = Me.PerimetreProfilsSFB(lAvecFaceSup)
            Case Enum_TypeSectionAcier.LamineSlimIFBA
                perimetre = Me.PerimetreProfilsIFB_A(lAvecFaceSup)
            Case Enum_TypeSectionAcier.LamineSlimIFBB
                perimetre = Me.PerimetreProfilsIFB_B(lAvecFaceSup)
        End Select

        Return perimetre
    End Function

    ''' <summary>
    ''' Calcul du périmetre développé par une section laminé usuelle ou SAB
    ''' </summary>
    ''' <param name="lAvecFaceSup">indique si on prend en compte la face supérieure de la semelle sup ou non</param>
    ''' <returns></returns>
    Private Function PerimetreProfilsLamineUsuels(lAvecFaceSup As Boolean) As Decimal
        Dim perimetre As Decimal = 0

        If lAvecFaceSup Then perimetre += Me.Bfs 'ajout de la face extérieure de la semelle supérieure

        perimetre += 2 * Me.Tfs 'ajout des 2 épaisseurs de la semelle sup
        perimetre += Me.Bfs - 2 * Me.Rcs - Me.Tw 'ajout de la face intérieure de la semelle sup
        perimetre += 2 * Math.PI * Me.Rcs / 2 'ajout congés sup
        perimetre += 2 * (HauteurAmeHw - Me.Rcs - Me.Rci) 'ajout de l'ame
        perimetre += 2 * Math.PI * Me.Rci / 2 'ajout congés inf
        perimetre += Me.Bfi - 2 * Me.Rci - Me.Tw 'ajout de la face intérieure de la semelle inf
        perimetre += 2 * Me.Tfi 'ajout des 2 épaisseurs de la semelle inf
        perimetre += Me.Bfi

        Return perimetre

    End Function

    ''' <summary>
    ''' Calcul du périmetre développé par une section PRS
    ''' </summary>
    ''' <param name="lAvecFaceSup">indique si on prend en compte la face supérieure de la semelle sup ou non</param>
    ''' <returns></returns>
    Private Function PerimetreProfilsPRS(lAvecFaceSup As Boolean) As Decimal
        Dim perimetre As Decimal = 0

        If lAvecFaceSup Then perimetre += Me.Bfs 'ajout de la face extérieure de la semelle supérieure

        perimetre += 2 * Me.Tfs 'ajout des 2 épaisseurs de la semelle sup
        perimetre += Me.Bfs - 2 * Math.Sqrt(2) * Me.aW - Me.Tw 'ajout de la face intérieure de la semelle sup
        perimetre += 4 * Me.aW     'ajout soudure sup
        perimetre += 2 * (HauteurAmeHw - 2 * Math.Sqrt(2) * Me.aW)  'ajout de l'ame
        perimetre += 4 * Me.aW 'ajout soudures inf
        perimetre += Me.Bfi - 2 * Math.Sqrt(2) * Me.aW - Me.Tw 'ajout de la face intérieure de la semelle inf
        perimetre += 2 * Me.Tfi 'ajout des 2 épaisseurs de la semelle inf
        perimetre += Me.Bfi 'ajout de la face inférieure

        Return perimetre

    End Function

    ''' <summary>
    ''' Calcul du périmetre développé par une section SFB
    ''' </summary>
    ''' <param name="lAvecFaceSup">indique si on prend en compte la face supérieure de la semelle sup ou non</param>
    ''' <returns></returns>
    Private Function PerimetreProfilsSFB(lAvecFaceSup As Boolean) As Decimal
        Dim perimetre As Decimal = 0

        If lAvecFaceSup Then perimetre += Me.Bfs 'ajout de la face extérieure de la semelle supérieure

        perimetre += 2 * Me.Tfs 'ajout des 2 épaisseurs de la semelle sup
        perimetre += Me.Bfs - 2 * Me.Rcs - Me.Tw 'ajout de la face intérieure de la semelle sup
        perimetre += 2 * Math.PI * Me.Rcs / 2 'ajout congés sup
        perimetre += 2 * (HauteurAmeHw - Me.Rcs - Me.Rci) 'ajout de l'ame
        perimetre += 2 * Math.PI * Me.Rci / 2 'ajout congés inf
        perimetre += Me.Bfi - 2 * Me.Rci - Me.Tw 'ajout de la face intérieure de la semelle inf
        perimetre += 2 * (Me.Tfi - Math.Sqrt(2) * Me.aW)  'ajout des 2 épaisseurs de la semelle inf
        perimetre += 4 * Me.aW 'ajout des soudures inf
        perimetre += Me.Plat_b - Me.Bfi - 2 * Math.Sqrt(2) * Me.aW 'ajout de la face intérieure du plat soudé
        perimetre += 2 * Me.Plat_t 'ajout des 2 épaisseurs du plat soudé
        perimetre += Me.Plat_b 'ajout de la face extérieure du plat soudé

        Return perimetre
    End Function

    ''' <summary>
    ''' Calcul du périmetre développé par une section IFB type A
    ''' </summary>
    ''' <param name="lAvecFaceSup">indique si on prend en compte la face supérieure de la semelle sup ou non</param>
    ''' <returns></returns>
    Private Function PerimetreProfilsIFB_A(lAvecFaceSup As Boolean) As Decimal
        Dim perimetre As Decimal = 0

        If lAvecFaceSup Then perimetre += Me.Bfs 'ajout de la face extérieure de la semelle supérieure

        perimetre += 2 * Me.Tfs 'ajout des 2 épaisseurs de la semelle sup
        perimetre += Me.Bfs - 2 * Me.Rcs - Me.Tw 'ajout de la face intérieure de la semelle sup
        perimetre += 2 * Math.PI * Me.Rcs / 2 'ajout congés sup
        perimetre += 2 * (HauteurAmeHw - Me.Rcs - Math.Sqrt(2) * Me.aW) 'ajout de l'ame
        perimetre += 4 * Me.aW 'ajout congés inf
        perimetre += Me.Plat_b - 2 * Math.Sqrt(2) * Me.aW - Me.Tw 'ajout de la face intérieure du plat soudé 
        perimetre += 2 * Me.Plat_t 'ajout des 2 épaisseurs du plat soudé
        perimetre += Plat_b 'ajout de la face extérieure du plat soudé

        Return perimetre
    End Function

    ''' <summary>
    ''' Calcul du périmetre développé par une section IFB type B
    ''' </summary>
    ''' <param name="lAvecFaceSup">indique si on prend en compte la face supérieure de la semelle sup ou non</param>
    ''' <returns></returns>
    Private Function PerimetreProfilsIFB_B(lAvecFaceSup As Boolean) As Decimal
        Dim perimetre As Decimal = 0

        If lAvecFaceSup Then perimetre += Me.Plat_b 'ajout de la face extérieure de la semelle supérieure

        perimetre += 2 * Me.Plat_t 'ajout des 2 épaisseurs du plat soudé
        perimetre += Me.Plat_b - 2 * Math.Sqrt(2) * Me.aW - Me.Tw 'ajout de la face intérieure du plat soudé 
        perimetre += 4 * Me.aW 'ajout des soudures sup
        perimetre += 2 * (HauteurAmeHw - Math.Sqrt(2) * Me.aW - Me.Rci) 'ajout de l'ame
        perimetre += 2 * Math.PI * Me.Rci / 2 'ajout congés inf
        perimetre += Me.Bfi - 2 * Me.Rci - Me.Tw 'ajout de la face intérieure de la semelle inf
        perimetre += Me.Bfi

        Return perimetre
    End Function

    Public Function Massivete(lAvecFaceSup As Boolean) As Decimal
        Dim massiveteLoc As Decimal

        massiveteLoc = Me.PerimetreSection(lAvecFaceSup) / Me.Aire

        Return massiveteLoc

    End Function

#End Region

#Region " Propriétés flexion "

#Region " Propriétés axe YY "

    Public Sub ModuleFlexionYY(ByRef WelYY As Decimal, ByRef WplYY As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul du module élastique de flexion du profilé, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Inertie, zANE, MelRd, zANP, MplRd As Decimal
        Dim zSup, zInf As Decimal
        Const Fy As Decimal = 235

        '--> Initialisation

        Select Case Me.typeProfileAcier
            Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym
                zSup = 0
                zInf = -Me.ha
            Case Enum_TypeSectionAcier.LamineSlimSFB
                zSup = Me.hb
                zInf = -Me.Plat_t
            Case Enum_TypeSectionAcier.LamineSlimIFBA
                zSup = Me.ha - Me.Plat_t
                zInf = -Me.Plat_t
            Case Enum_TypeSectionAcier.LamineSlimIFBB
                zSup = Me.ha - Me.Tfi
                zInf = -Me.Tfi
            Case Enum_TypeSectionAcier.LamineSlimSAB
                zSup = Me.hb - Me.Tfi
                zInf = -Me.Tfi
        End Select

        '--> Calculs

        Me.ProprietesMyy(1, True, 1, zANE, Inertie, MelRd, zANP, MplRd)

        WelYY = Inertie / Math.Max(Math.Abs(zSup - zANE), Math.Abs(zANE - zInf))
        WplYY = MplRd / (Fy * kConvMPaPa)


    End Sub

    Public Sub ProprietesElastiques(Signe As Decimal, ByRef zANE As Decimal, ByRef InertieY As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   21/02/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '   Uniquement I et z
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   zANE        [S] :   Position axe neutre élastique
        '   InertieY    [S] :   Moment d'inertie
        '-------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim MelRd, MplRd As Decimal
        Dim zANP As Decimal

        '--( Calcul des propriétés

        Me.ProprietesMyy(Signe, False, 1, zANE, InertieY, MelRd, zANP, MplRd, False)

    End Sub

    Public Sub ProprietesMyy(Signe As Decimal, lValeurRd As Boolean, GammaM0 As Decimal,
                             ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef MelRd As Decimal,
                             ByRef zANP As Decimal, ByRef MplRd As Decimal, Optional lPlastic As Boolean = True)
        '-------------------------------------------------------------------------------------------------------------------
        '   26/04/24 :  Modif - GUD
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques et plastiques en flexion simple de la section, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   GammaM0     [E] :   Coefficient partiel pour l'acier
        '   zANE        [S] :   Position axe neutre élastique
        '   InertieY    [S] :   Moment d'inertie
        '   MelRd       [S] :   Moment élastique
        '   zANP        [S] :   Position de l'axe neutre plastique
        '   MplRd       [S] :   Momemnt plastique
        '   lPlastic    [E] :   Indique si on calcule les propriétés plastiques
        '-------------------------------------------------------------------------------------------------------------------


        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim zRef As Decimal = Me.zRefAraseSup 'Cote de l'arase supérieure de la semelle supérieure du profilé 
        'Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        Const Fy As Decimal = 235

        '--> Initialisation

        Hw = Me.HauteurAmeHw

        '--> Modélisation du profilé acier

        MyModele.MaillageProfileA_YY(GammaM0, RhoV, Me, Fy, Fy, Fy, Fy)

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieY, lValeurRd)

        If lPlastic Then

            '--> Recherche de l'axe neutre plastique

            MyModele.RechercheANP(Signe, zANP, lValeurRd)

            '--> Moment plastique

            MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

        End If

    End Sub

#End Region

#Region " Propriétés axe ZZ "

    Private Sub ModuleFlexionZZ(ByRef WelZZ As Decimal, ByRef WplZZ As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul du module élastique de flexion du profilé, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Inertie, zANE, MelRd, zANP, MplRd As Decimal
        Dim zSup, zInf As Decimal
        Const Fy As Decimal = 235

        '--> Initialisation

        Select Case Me.typeProfileAcier
            Case Enum_TypeSectionAcier.Lamine, Enum_TypeSectionAcier.PRS_Bi_Sym, Enum_TypeSectionAcier.PRS_Mono_Sym
                zSup = Math.Abs(Math.Max(Me.Bfs, Me.Bfi) / 2)
                zInf = -zSup
            Case Enum_TypeSectionAcier.LamineSlimSFB
                zSup = Math.Abs(Math.Max(Me.Plat_b, Math.Max(Me.Bfs, Me.Bfi)) / 2)
                zInf = -zSup
            Case Enum_TypeSectionAcier.LamineSlimIFBA
                zSup = Math.Abs(Math.Max(Me.Bfs, Me.Plat_b) / 2)
                zInf = -zSup
            Case Enum_TypeSectionAcier.LamineSlimIFBB
                zSup = Math.Abs(Math.Max(Me.Bfi, Me.Plat_b) / 2)
                zInf = -zSup
            Case Enum_TypeSectionAcier.LamineSlimSAB
                zSup = Math.Abs(Math.Max(Me.Bfs, Me.Bfi) / 2)
                zInf = -zSup
        End Select

        '--> Calculs

        Me.ProprietesMzz(True, 1, zANE, Inertie, MelRd, zANP, MplRd)

        WelZZ = Inertie / Math.Max(Math.Abs(zSup - zANE), Math.Abs(zANE - zInf))
        WplZZ = MplRd / (Fy * kConvMPaPa)

    End Sub

    Public Sub ProprietesMzz(lValeurRd As Boolean, GammaM0 As Decimal,
                                        ByRef zANE As Decimal, ByRef InertieZ As Decimal, ByRef MelRd As Decimal,
                                        ByRef zANP As Decimal, ByRef MplRd As Decimal)
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

        Dim MyModele As New cls_ModeleP
        Const Fy As Decimal = 235
        Const Signe As Decimal = 1
        Const RhoV As Decimal = 0

        '--> Initialisation

        MyModele.MaillageProfileA_ZZ(GammaM0, RhoV, Me, Fy, Fy, Fy, Fy)

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieZ = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment élastique

        MelRd = MyModele.MomentElastique(Signe, zANE, InertieZ, lValeurRd)

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

#End Region

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.typeProfileAcier = Enum_TypeSectionAcier.Lamine

        '--> Section par défaut peu importe le type
        Me.hb = 0.3
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

        Me.ha = Me.hb + Me.Plat_t
    End Sub

    Public Sub GenererProfileHEB300()
        Me.hb = 0.3
        'Me.h_w = 0.27
        Me.Tw = 0.011
        Me.Bfi = 0.3
        Me.Bfs = 0.3
        Me.Tfi = 0.019
        Me.Tfs = 0.019
        Me.Rcs = 0.027
        Me.Rci = 0.027
        Me.aW = 0.01

        Me.Gamme = "HE"
        Me.NomProfile = "HE 300 B"

        Me.Plat_b = 0
        Me.Plat_t = 0

        Me.ha = Me.hb + Me.Plat_t
    End Sub

    Public Sub GenereProfileIPE300()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        '   Génération d'un profilé laminé IPE 300
        '----------------------------------------------------------------------------------------------------------------------------------

        Me.ha = 0.3
        Me.Bfi = 0.15
        Me.Bfs = 0.15
        Me.Tfi = 0.0107
        Me.Tfs = 0.0107
        Me.Tw = 0.0071
        Me.Rci = 0.015
        Me.Rcs = 0.015
        Me.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

    End Sub


    Public Sub GenereProfileIPE500()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   04/05/24 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        '   Génération d'un profilé laminé IPE 500
        '----------------------------------------------------------------------------------------------------------------------------------

        Me.ha = 0.5
        Me.Bfi = 0.2
        Me.Bfs = 0.2
        Me.Tfi = 0.016
        Me.Tfs = 0.016
        Me.Tw = 0.0102
        Me.Rci = 0.021
        Me.Rcs = 0.021
        Me.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

    End Sub
#End Region

#Region " Fonctions de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub DeepClone(ByVal ProfilASource As cls_ProfilA, ByRef ProfilACible As cls_ProfilA)
        ProfilACible = ProfilASource.Clone()

        If ProfilASource.IndDeliv IsNot Nothing Then
            ReDim ProfilACible.IndDeliv(ProfilASource.IndDeliv.GetUpperBound(0))
            ProfilACible.IndDeliv = ProfilASource.IndDeliv.Clone
        End If

        ReDim ProfilACible.IndStandart(ProfilASource.IndStandart.GetUpperBound(0))
        ProfilACible.IndStandart = ProfilASource.IndStandart.Clone
    End Sub

    Public Shared Sub DeepCopie(ProfilSource As cls_ProfilA, ByRef ProfilCible As cls_ProfilA)

        ProfilCible = ProfilSource.Clone

        If Not (ProfilSource.IndDeliv Is Nothing) Then ProfilCible.IndDeliv = ProfilSource.IndDeliv.Clone
        If (ProfilSource.IndStandart.GetUpperBound(0) > 0) Then ProfilCible.IndStandart = ProfilSource.IndStandart.Clone

    End Sub

#End Region

End Class
