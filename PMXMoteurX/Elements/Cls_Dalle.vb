Public Class Cls_Dalle

#Region " Constantes "

    Private Const THETAHDEFAULT As Decimal = 30             ' Angle inclinaison renformis
    Private Const WAPPUIPREDALLEDEFAUT As Decimal = 0.07    ' Largeur d'appui des prédalles sur la semelle    

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
    Private pTheta_h As Decimal

    ''' <summary>
    ''' Epaisseur de prédalle pour les dalles partiellement préfabriquées
    ''' </summary>
    Public preDalle_ep As Decimal

    ''' <summary>
    ''' Epaisseur du joint entre élément de prédalle
    ''' </summary>
    Public preDalle_tjoint As Decimal

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
    Public Bac As New Cls_Bac

    '''' <summary>
    '''' Armatures longitudinales supérieur
    '''' </summary>
    'Public arma_longi_sup As New Cls_Armatures_Longi

    '''' <summary>
    '''' Armatures longitudinales inférieur
    '''' </summary>
    'Public arma_longi_inf As New Cls_Armatures_Longi

    Public LitArma As New List(Of Cls_Armatures_Longi)

    ''' <summary>
    ''' Acier des armatures
    ''' </summary>
    Public AcierArmatures As New Cls_AcierArmature

    ''' <summary>
    ''' Connecteur acier-béton entre dalle et profilé
    ''' </summary>
    Public Connecteur As New Cls_Connecteur

#End Region

#Region " Enumérations "

    Public Enum Enum_TypeDalle
        Pleine
        Mixte
        Prefabriquee
    End Enum

#End Region

#Region " Fonction de calcul "

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Public Sub Calcul_Proprietes()

        'If lArma_Sup Then
        '    With arma_longi_sup
        '        .n_s = Beff / .EspBar
        '        .A_s = .n_s * Math.PI * .PhiS ^ 2 / 4
        '    End With
        'End If

        'If lArma_Inf Then
        '    With arma_longi_inf
        '        .n_s = Beff / .EspBar
        '        .A_s = .n_s * Math.PI * .PhiS ^ 2 / 4
        '    End With
        'End If

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
                Ac = Me.Beff * (Me.EpaisseurActive + Bac.Hp * Bac.LargeurBmoyenne / Bac.Ep)
                perimU = Me.Beff

        End Select

        MyH0 = 2 * Ac / perimU

        Return MyH0
    End Function


#End Region

#Region " Outils "

    ''' <summary>
    ''' Retourne la largeur d'appui d'une prédalle sur la semelle
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property wAppuiPreDalle
        Get
            Return WAPPUIPREDALLEDEFAUT
        End Get
    End Property

    ''' <summary>
    ''' Angle d'inclinaison bord du renformis, en radians
    ''' </summary>
    Public Property ThetaRd As Decimal
        Get
            Return Me.pTheta_h * Math.PI / 180
        End Get
        Set(value As Decimal)
            Me.pTheta_h = value
        End Set

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
            Dim MyzTop As Decimal

            MyzTop = Me.EpRenformis + Me.t_d

            If (Me.type = Enum_TypeDalle.Mixte) Then
                If (Me.Bac.lCofraplus220) Then MyzTop -= Me.Bac.Hp
            End If

            Return MyzTop
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
                    Select Case Me.Bac.Orientation
                        Case Cls_Bac.Enum_Orientation.Parallele
                            Ep = Me.t_d - Me.Bac.Hp
                        Case Cls_Bac.Enum_Orientation.Perpendiculaire
                            Ep = Me.t_d - Me.Bac.Hauteur_hpg
                    End Select
                Case Enum_TypeDalle.Pleine
                    Ep = Me.t_d
            End Select
            Return Ep
        End Get
    End Property

    ''' <summary>
    ''' Renvoie le nombre de lit d'armatures actif dans la dalle
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NbLitsArmaActifs As Integer
        Get
            Dim Nombre As Integer = 0
            For i As Integer = 0 To Me.LitArma.Count - 1
                If Me.LitArma(i).lActive Then Nombre += 1
            Next
            Return Nombre
        End Get
    End Property

#End Region

#Region " Constructeur "

    Sub New()

        Me.type = Enum_TypeDalle.Pleine

        Me.Beff = 1
        Me.t_d = 0.12
        Me.t_h = 0.04
        Me.preDalle_tjoint = 0.05
        Me.preDalle_ep = 0.06

        Me.lArma_Inf = True
        Me.lArma_Sup = True

        Me.pTheta_h = THETAHDEFAULT

        '--> création des deux lits d'armatures 
        Me.LitArma.Add(New Cls_Armatures_Longi)
        Me.LitArma.Add(New Cls_Armatures_Longi)

        Me.LitArma(1).lActive = False

        Me.LitArma(1).z_s = 0.045

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

        'With arma_longi_inf
        '    Lines.Add("   DABc          = " & .c_s)
        '    Lines.Add("   DABd          = " & .PhiS)
        '    Lines.Add("   DABn          = " & .n_s)
        '    Lines.Add("   DABz          = " & .z_s)
        '    Lines.Add("   DABe          = " & .EspBar)
        'End With

        'With arma_longi_sup
        '    Lines.Add("   DAHc          = " & .c_s)
        '    Lines.Add("   DAHd          = " & .PhiS)
        '    Lines.Add("   DAHn          = " & .n_s)
        '    Lines.Add("   DAHz          = " & .z_s)
        '    Lines.Add("   DAHe          = " & .EspBar)
        'End With

        '--> Bac acier
        Bac.EcrireFile(Lines)

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(DalleSource As Cls_Dalle, ByRef DalleCible As Cls_Dalle)

        DalleCible = DalleSource.Clone()

        DalleCible.beton = DalleSource.beton.Clone()
        DalleCible.Bac = DalleSource.Bac.Clone()

        DalleCible.LitArma = New List(Of Cls_Armatures_Longi)

        For Each armalongi As Cls_Armatures_Longi In DalleSource.LitArma
            Dim armalongi_loc As New Cls_Armatures_Longi()
            armalongi_loc = armalongi.Clone()
            DalleCible.LitArma.Add(armalongi_loc)
        Next

        DalleCible.AcierArmatures = DalleSource.AcierArmatures.Clone()
        DalleCible.Connecteur = DalleSource.Connecteur.Clone()
    End Sub

#End Region

End Class
