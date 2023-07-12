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
        Get
            Dim pAire As Decimal

            pAire = Me.AireFi + Me.AireFs + Me.HauteurAmeHw * Me.t_w + (4 - Math.PI) * (Me.r_ci ^ 2 + Me.r_cs ^ 2) / 2

            Return pAire
        End Get
    End Property

    ''' <summary>
    ''' Intertie de torsion du profilé
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InertieT As Decimal
        Get

            '--> Déclaration

            Dim pInertieT As Decimal

            '--> Calcul

            Select Case Me.typeProfileAcier
                Case Enum_TypeSectionAcier.Lamine
                    Dim Bf As Decimal = Me.b_fs
                    Dim Tf As Decimal = Me.t_fs
                    Dim Tw As Decimal = Me.t_w
                    Dim Rc As Decimal = Me.r_cs
                    Dim Hw As Decimal = Me.HauteurAmeHw

                    'pInertieT = 2.0 * Bf * Tf ^ 3 * (1.0 - 0.63 * Tf / Bf * (1.0# - (Tf / Bf) ^ 4 / 12.0#)) _
                    '               + Tw ^ 3 * Hw / 3.0# _
                    '              + 2.0# * Tw / Tf * (0.1# * Rc / Tf + 0.15#) * ((Tf + Rc) ^ 2 + Tw * (Rc + Tw / 4.0#)) ^ 4 / (2.0# * Rc + Tf) ^ 4
                    pInertieT = 2 / 3 * (Bf - 0.63 * Tf) * Tf ^ 3 _
                              + 1 / 3 * Hw * Tw ^ 3 _
                              + 2 * Tw / Tf * (0.145 + 0.1 * Rc / Tf) * (((Rc + Tw / 2) ^ 2 + (Rc + Tf) ^ 2 - Rc ^ 2) / (2 * Rc + Tf)) ^ 4

            End Select


            '--> Fin

            Return pInertieT

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
