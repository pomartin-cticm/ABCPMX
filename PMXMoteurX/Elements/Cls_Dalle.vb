Public Class Cls_Dalle

#Region " Constantes "

    Private Const THETAHDEFAULT As Decimal = 30        ' Angle inclinaison renformis

#End Region

#Region " Attributs "

    ''' <summary>
    ''' Type de béton
    ''' </summary>
    Public type As Enum_TypeDalle

    ''' <summary>
    ''' épaisseur totale de la dalle (hors renformis)
    ''' </summary>
    Public t_d As Decimal

    ''' <summary>
    ''' épaisseur du renformis
    ''' </summary>
    Public t_h As Decimal

    ''' <summary>
    ''' lageur efficace de la dalle
    ''' </summary>
    Public Beff As Decimal

    ''' <summary>
    ''' Indique si une armature inférieur est présente
    ''' </summary>
    Public lArma_Inf As Boolean

    ''' <summary>
    ''' Indique si une armature supérieur est présente
    ''' </summary>
    Public lArma_Sup As Boolean

    '''' <summary>
    '''' acier de l'armature
    '''' </summary>
    'Public acier_armature As Decimal

    ''' <summary>
    ''' angle / verticale du bord des renformis
    ''' </summary>
    Public Theta_h As Decimal

#End Region

#Region " Elements de la dalle "

    ''' <summary>
    ''' béton de la dalle
    ''' </summary>
    Public beton As New Cls_Beton

    ''' <summary>
    ''' Bac acier de la dalle
    ''' que si dalle mixte
    ''' </summary>
    Public bac_acier As New Cls_Bac_Acier

    ''' <summary>
    ''' Armatures longitudinales supérieur
    ''' </summary>
    Public arma_longi_sup As New Cls_Armatures_Longi

    ''' <summary>
    ''' Armatures longitudinales inférieur
    ''' </summary>
    Public arma_longi_inf As New Cls_Armatures_Longi

    Public LitArma As New List(Of Cls_Armatures_Longi)

    ''' <summary>
    ''' Acier des armatures
    ''' </summary>
    Public AcierArmatures As New Cls_AcierArmature

#End Region

#Region " Enumérations "

    Public Enum Enum_TypeDalle
        Pleine
        Mixte
    End Enum

#End Region

#Region " Fonction de calcul "

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Public Sub Calcul_Proprietes()

        If lArma_Sup Then
            With arma_longi_sup
                .n_s = Beff / .EspBar
                .A_s = .n_s * Math.PI * .PhiS ^ 2 / 4
            End With
        End If

        If lArma_Inf Then
            With arma_longi_inf
                .n_s = Beff / .EspBar
                .A_s = .n_s * Math.PI * .PhiS ^ 2 / 4
            End With
        End If

    End Sub

    Public Function NotionalSizeH0(Bfs As Decimal) As Decimal
        '---------------------------------------------------------------------------------------
        '   17/05/2023 :    Création - POM
        '---------------------------------------------------------------------------------------
        '   Renvoie la dimension h0 d'une dalle
        '---------------------------------------------------------------------------------------
        '   Bfs     [E] :   Largeur de semelle supérieure
        '---------------------------------------------------------------------------------------

        Dim MyH0 As Decimal
        Dim Ac, perimU As Decimal

        Select Case Me.type
            Case Enum_TypeDalle.Pleine
                Ac = Me.Beff * Me.t_d + Me.t_h * (Bfs + Me.t_h * Math.Tan(Me.ThetaRd))
                perimU = 2 * Me.Beff - Bfs + Me.t_h / Math.Cos(ThetaRd) * (1 - Math.Sin(ThetaRd))

            Case Enum_TypeDalle.Mixte
                Ac = Me.Beff * (Me.EpaisseurActive + bac_acier.h_p * bac_acier.LargeurBmoyenne / bac_acier.e_p)
                perimU = Me.Beff

        End Select

        MyH0 = 2 * Ac / perimU

        Return MyH0
    End Function


#End Region

#Region " Outils "

    ''' <summary>
    ''' Angle d'inclinaison bord du renformis, en radians
    ''' </summary>
    Public ReadOnly Property ThetaRd As Decimal
        Get
            Return Me.Theta_h * Math.PI / 180
        End Get

    End Property

    ''' <summary>
    ''' Renvoie l'épaisseur de renformis à considérer (0 si dalle mixte)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EpRenformis As Decimal
        Get
            Dim EpR As Decimal
            Select Case Me.type
                Case Enum_TypeDalle.Mixte : EpR = 0
                Case Enum_TypeDalle.Pleine : EpR = Me.t_h
            End Select
            Return EpR
        End Get
    End Property

    ''' <summary>
    ''' Position Z de la face supérieure de la dalle béton
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property zTop As Decimal
        Get
            Return Me.EpRenformis + Me.t_d
        End Get
    End Property

    ''' <summary>
    ''' Renvoie l'épaisseur de la dalle mobilisable
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EpaisseurActive As Decimal
        Get
            Dim Ep As Decimal
            Select Case Me.type
                Case Enum_TypeDalle.Mixte
                    Select Case Me.bac_acier.orientation
                        Case Cls_Bac_Acier.Enum_Orientation.Parallele
                            Ep = Me.t_d - Me.bac_acier.h_p
                        Case Cls_Bac_Acier.Enum_Orientation.Perpendiculaire
                            Ep = Me.t_d - Me.bac_acier.h_pg
                    End Select
                Case Enum_TypeDalle.Pleine
                    Ep = Me.t_d
            End Select
            Return Ep
        End Get
    End Property

#End Region

#Region " Constructeur "

    Sub New()

        Me.type = Enum_TypeDalle.Pleine

        Me.Beff = 1
        Me.t_d = 0.12
        Me.t_h = 0.04

        Me.lArma_Inf = True
        Me.lArma_Sup = True

        'Me.acier_armature = 500 * 10 ^ 6

        Me.arma_longi_inf.EspBar = 0.2
        Me.arma_longi_inf.n_s = 5
        Me.arma_longi_inf.z_s = 0.015
        Me.arma_longi_inf.c_s = 0.013

        Me.arma_longi_sup.EspBar = 0.2
        Me.arma_longi_sup.n_s = 5
        Me.arma_longi_sup.z_s = 0.015
        Me.arma_longi_sup.c_s = 0.013

        Me.Theta_h = THETAHDEFAULT

        '--> création des deux lits d'armatures 
        Me.LitArma.Add(New Cls_Armatures_Longi)
        Me.LitArma.Add(New Cls_Armatures_Longi)

        Me.LitArma(1).lActive = False

    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    Public Sub EcrireFile(ByRef Lines As List(Of String))

        Lines.Add("   DType         = " & type)
        Lines.Add("   DL_d          = " & Beff)
        Lines.Add("   Dt_d          = " & t_d)
        Lines.Add("   DAInf         = " & lArma_Inf)
        Lines.Add("   DAsup         = " & lArma_Sup)
        ' Lines.Add("   Df_y          = " & acier_armature)

        With beton
            Lines.Add("   DBType        = " & .Type)
            Lines.Add("   DBClasse      = " & .Classe)
            Lines.Add("   DBFck         = " & .Fck)
        End With

        With arma_longi_inf
            Lines.Add("   DABc          = " & .c_s)
            Lines.Add("   DABd          = " & .PhiS)
            Lines.Add("   DABn          = " & .n_s)
            Lines.Add("   DABz          = " & .z_s)
            Lines.Add("   DABe          = " & .EspBar)
        End With

        With arma_longi_sup
            Lines.Add("   DAHc          = " & .c_s)
            Lines.Add("   DAHd          = " & .PhiS)
            Lines.Add("   DAHn          = " & .n_s)
            Lines.Add("   DAHz          = " & .z_s)
            Lines.Add("   DAHe          = " & .EspBar)
        End With

        '--> Bac acier
        bac_acier.EcrireFile(Lines)

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(DalleSource As Cls_Dalle, ByRef DalleCible As Cls_Dalle)

        DalleCible = DalleSource.Clone

    End Sub

#End Region

End Class
