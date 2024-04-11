Public Class cls_Dalle

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
    ''' lageur efficace de la dalle ??
    ''' </summary>
    Public Beff As Decimal

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

#Region " Propriétés "
    ''' <summary>
    '''  surface par unité de largeur (m²/m)
    ''' </summary>
    ''' <param name="dc">largeur de calcul de l'aire</param>
    ''' <param name="bfs">largeur de la semelle supérieure</param>
    ''' <returns></returns>
    Public Function Aire(dc As Decimal, bfs As Decimal) As Decimal
        Dim Ac As Decimal

        If Me.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            Dim tc As Decimal
            tc = Me.t_d - Me.Bac.Hp
            Ac = dc * tc * (1 + Me.Bac.LargeurBmoyenne * Me.Bac.Hp / (Me.Bac.Ep * tc))

        Else 'dalle pleine, avec ou sans dalle préfa
            Ac = dc * t_d + t_h * (bfs + t_h * Math.Tan(ThetaRd) / 2)

        End If

        Return Ac

    End Function

#End Region

#Region " Elements de la dalle "

    ''' <summary>
    ''' béton de la dalle
    ''' </summary>
    Public beton As New cls_Beton

    ''' <summary>
    ''' Bac acier de la dalle
    ''' que si dalle mixte
    ''' </summary>
    Public Bac As New cls_Bac

    ''' <summary>
    ''' Cofradal (utile pour les slimfloors)
    ''' </summary>
    Public Cofradal As New cls_Cofradal

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
    Public AcierArmatures As New cls_AcierArmature

    ''' <summary>
    ''' Connecteur acier-béton entre dalle et profilé
    ''' </summary>
    Public Connecteur As New cls_Connecteur

#End Region

#Region " Enumérations "

    Public Enum Enum_TypeDalle
        Pleine
        Mixte
        PartiellementPrefabriquee
        CompletementPrefabriquee
    End Enum

#End Region

#Region " Propriétés "

    Public Function DiametreMaxiArma() As Decimal
        '---------------------------------------------------------------------------------------
        '   17/05/2023 :    Création - POM
        '---------------------------------------------------------------------------------------
        '   Renvoie le diamètre maxi des armatures dans la dalle
        '---------------------------------------------------------------------------------------

        Dim myDia As Decimal

        For i As Integer = 0 To Me.LitArma.Count - 1
            If Me.LitArma(i).lActive Then
                myDia = Math.Max(myDia, Me.LitArma(i).PhiS)
            End If
        Next

        Return myDia

    End Function


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
                'perimU = 2 * Me.Beff - Bfs +  Me.t_h / Math.Cos(ThetaRd) * (1 - Math.Sin(ThetaRd)) 
                perimU = 2 * Me.Beff - Bfs + 2 * Me.t_h / Math.Cos(ThetaRd) * (1 - Math.Sin(ThetaRd)) 'GUD: Rajout du *2 devant le Me.th/math.cos ... -> A vérifier car je me suis basé sur la formule (65) du MT

            Case Enum_TypeDalle.Mixte 'Ne faut-il pas différencier le cas du bac perpendiculaire et // ?
                If Me.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele Then
                    Ac = Me.Beff * (Me.EpaisseurActive + Bac.Hp * Bac.LargeurBmoyenne / Bac.Ep)
                    perimU = Me.Beff
                Else 'RAJOUT GUD: j'ai rajouter le IF + les formules du ELSE
                    Ac = Me.Beff * Me.EpaisseurActive
                    perimU = Me.Beff
                End If
            Case Enum_TypeDalle.PartiellementPrefabriquee 'Rajout GUD: il manquait ce cas (à mon avis il vaut mieux différencier ce cas de la dalle pleine, au cas où la valeur de theta n'aurait pas été initialisée à 0 pour le cas de la dalle préfa)
                Ac = Me.Beff * Me.t_d
                perimU = 2 * Me.Beff - Bfs

        End Select

        MyH0 = 2 * Ac / perimU

        Return MyH0
    End Function

    Public Function NResistanceArmatures(Beff As Decimal, GammaS As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------
        '   31/10/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul de la résistance à la traction des armatures de la dalle
        '---------------------------------------------------------------------------------------------
        '   Beff    [E] :   Largeur participante de la dalle
        '   GammaS  [E] :   Coefficient partiel pour les armatures
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pNArma As Decimal = 0

        '--> Calcul

        For i As Integer = 0 To Me.LitArma.Count - 1
            If Me.LitArma(i).lActive Then
                pNArma += Me.LitArma(i).AireParULargeur * Beff * Me.AcierArmatures.FsK / GammaS
            End If
        Next

        '--> Fin

        Return pNArma * kConvMPaPa

    End Function

    Public Function AireUnitArmaturesLongi() As Decimal
        '---------------------------------------------------------------------------------------------
        '   21/03/24 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul de l'aire par unité de longueur des armatures de la dalle
        '---------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim AireAs As Decimal = 0

        '--> Calcul

        For i As Integer = 0 To Me.LitArma.Count - 1
            If Me.LitArma(i).lActive Then
                AireAs += Me.LitArma(i).AireParULargeur
            End If
        Next

        '--> Fin

        Return AireAs

    End Function

    Public Function NResistanceCompressionDalle(Beff As Decimal, GammaC As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------
        '   31/10/23 :  Création - POM
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

        pNDalle = Beff * Me.EpaisseurActive * Me.beton.Fck * kDalle / GammaC

        '--> Fin

        Return pNDalle * kConvMPaPa
    End Function

#End Region

#Region " Outils "

    ''' <summary>
    ''' Indique si la dalle est mixte
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property lMixte As Boolean
        Get
            Return (Me.type = Enum_TypeDalle.Mixte)
        End Get
    End Property


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
                        Case cls_Bac.Enum_Orientation.Parallele
                            Ep = Me.t_d - Me.Bac.Hp
                        Case cls_Bac.Enum_Orientation.Perpendiculaire
                            Ep = Me.t_d - Me.Bac.Hauteur_hpg
                    End Select
                Case Enum_TypeDalle.Pleine
                    Ep = Me.t_d
                Case Enum_TypeDalle.PartiellementPrefabriquee
                    Ep = Me.t_d - Me.preDalle_ep + Me.preDalle_tjoint
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

#Region " Outils pour la méthode Hivoss "

    Public Function FrequenceDalle(LPoutre As Decimal, PorteeDalle As Decimal, LargInfluence As Decimal, MasseProfile As Decimal, G As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   01/12/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Calcul de la frequence propre de la dalle
        '---------------------------------------------------------------------------------------------------
        '   LPoutre     [E] :   Longueur de la poutre
        '   PorteeDalle [E] :   Portée de la dalle
        '   LargInfleunce[E] :  Largeur d'influence des charges sur la dalle
        '   MasseProfile[E] :   Masse du profilé acier
        '---------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim RatioTsL As Decimal
        Dim Mu As Decimal
        Dim EIx As Decimal
        Dim Delta As Decimal
        Dim MyFreq As Decimal
        Dim pQ As Decimal
        Dim Surface As Decimal

        '--> Initialisation

        RatioTsL = PorteeDalle / LPoutre
        Surface = LPoutre * LargInfluence

        ' MassesToCharges(MyBeam, Masses)
        '==== A COMPLETER

        Mu = 1 '(Masses(0) - MasseProfile + MyBeam.HivossParam.IndCombiQ / 10 * Masses(MyBeam.HivossParam.IndChargeQ + 1)) / Surface

        '--> Calcul inertie dalle / unite de longueur

        EIx = cls_Acier.EYACIER * kConvMPaPa * Me.InertieTransversaleH(Me.beton.CoefficientEquivalenceCT)

        '--> Flèche de la dalle sous charges Gravitaires

        pQ = Mu * G * PorteeDalle
        Delta = 5 / 384 * pQ * PorteeDalle ^ 3 / EIx
        Const kMM As Decimal = 1000

        '--> Résultat final

        MyFreq = 18 / Math.Sqrt(Delta * kMM)

        Return MyFreq
    End Function


    Public Function InertieTransversaleH(nEq As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   23/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Calcul de l'inertie transversale homogénéisée (par unité de largeur)
        '---------------------------------------------------------------------------------------------------
        '   nEq     [E] :   Coefficient d'équivalence
        '---------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pInertieH As Decimal

        '--> Calcul

        Select Case Me.type
            Case Enum_TypeDalle.Pleine, Enum_TypeDalle.PartiellementPrefabriquee
                pInertieH = Me.t_d ^ 3 / 12
            Case Enum_TypeDalle.Mixte
                Select Case Me.Bac.Orientation
                    Case cls_Bac.Enum_Orientation.Parallele
                        pInertieH = Me.EpaisseurActive ^ 3 / 12
                    Case cls_Bac.Enum_Orientation.Perpendiculaire
                        pInertieH = Me.InertieDalleMixteT
                End Select
        End Select

        Return pInertieH / nEq

    End Function

    Private Function InertieDalleMixteT() As Decimal
        '-------------------------------------------------------------------------------------------------
        '   23/11/23 :  Création - POM - V1
        '-------------------------------------------------------------------------------------------------
        '   Calcul de l'inertie d'une dalle mixte dans le sens transversal (partie béton seul)
        '-------------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Aa(2) As Decimal
        Dim Zz(2) As Decimal
        Dim Ii(2) As Decimal
        Dim nbNerv As Decimal
        Dim bMin, bMax As Decimal
        Dim MStat As Decimal = 0
        Dim Aire As Decimal = 0
        Dim i As Integer
        Dim Inertie As Decimal = 0
        Dim zG As Decimal
        Dim pTc As Decimal = Me.EpaisseurActive

        '--> Initialisations

        bMin = Math.Min(Me.Bac.Bb, Me.Bac.Bt)
        bMax = Math.Max(Me.Bac.Bb, Me.Bac.Bt)

        '   Partie pleine de la dalle

        Aa(0) = pTc
        Ii(0) = (pTc) ^ 3 / 12
        Zz(0) = pTc / 2

        '   Nervures partie centrale

        nbNerv = 1 / Me.Bac.Ep
        Aa(1) = nbNerv * bMin * Me.Bac.Hp
        Zz(1) = Me.Bac.Hp / 2
        Ii(1) = nbNerv * bMin * Me.Bac.Hp ^ 3 / 12

        '   Nervures partie triangulaire

        Aa(2) = nbNerv * (bMax - bMin) / 2 * Me.Bac.Hp
        Ii(2) = nbNerv * 2 * (bMax - bMin) / 2 * Me.Bac.Hp ^ 3 / 36
        If (Me.Bac.Bb > Me.Bac.Bt) Then
            Zz(2) = pTc + Me.Bac.Hp / 3
        Else
            Zz(2) = Me.t_d - Me.Bac.Hp / 3
        End If

        '--> Calcul

        For i = 0 To 2
            MStat += Aa(i) * Zz(i)
            Aire += Aa(i)
            Inertie += Ii(i)
        Next

        zG = MStat / Aire

        For i = 0 To 2
            Inertie += Aa(i) * (Zz(i) - zG) ^ 2
        Next

        Return Inertie
    End Function

#End Region

#Region " Constructeur "

    Sub New()

        Me.type = Enum_TypeDalle.Pleine

        Me.Beff = 1
        Me.t_d = 0.12
        Me.t_h = 0.04
        Me.preDalle_tjoint = 0.05
        Me.preDalle_ep = 0.06

        'Me.lArma_Inf = True
        'Me.lArma_Sup = True

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
        'Lines.Add("   DAInf         = " & lArma_Inf)
        'Lines.Add("   DAsup         = " & lArma_Sup)
        ' Lines.Add("   Df_y          = " & acier_armature)

        With beton
            Lines.Add("   DBType        = " & .lLeger)
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

    Public Shared Sub DeepClone(DalleSource As cls_Dalle, ByRef DalleCible As cls_Dalle)

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
