'Imports PropMix_Engine.Cls_Section

Public Class cls_OptionsCalcul


#Region " Enumérations et constantes "

    Public Shared tabRH() As Decimal = {50, 80}
    Public Shared tabWk() As Decimal = {0.4, 0.3, 0.2}
    Public Shared tabGraviteG() As Decimal = {9.81, 10}

    Const DELTAD_DEF As Decimal = 0.0005            ' Glissement du connecteur à 0,7 PRk (cf. prEN 1994-1-1 B.2.5 (5))

    'Public Shared t0Retrait As Decimal = 1

    Enum Enu_Normes
        EurocodesG1
        EurocodesG2
    End Enum

    Enum Enu_MReducPlatSlim
        M1_ReducAire
        M2_ReducEpaisseur
        M3_ReducLimiteElasticite
    End Enum

#End Region

#Region " Attributs "

    Public RH As Decimal                            ' Humidité pour le calcul du béton

    Public Gamma As cls_Gamma                       ' Coefficients partiels pour le calcul
    'Public HivossParam As cls_OptionsHivoss        ' Coefficients pour le calcul dynamique définits dans la Frm_Hivoss
    Public Norme As Enu_Normes                      ' Norme de calcul

    Public EtaW As Decimal                          ' Valeur utilisée dans le calcul du voilement par cisaillement de l'âme des profilés métalliques

    Public lLargeurEfficaceSimplifiee As Boolean    ' Largeur efficace de la dalle béton selon modèle simplifié
    Public lCompressionArma As Boolean              ' Indique si l'on prend en compte les armatures comprimées dans le calcul des propriétés de section
    Public lEnrobProp As Boolean                    ' Indique si on prend en compte l'enrobage partiel dans le calcul des propriétés de section à froid
    Public dMaxNodes As Decimal                     ' Distance maximale entre deux noeuds
    Public nbMinNodesTravee As Integer              ' Nombre mini de noeuds par travée normale
    Public nbMinNodesConsole As Integer             ' Nombre mini de noeuds par travée console

    Public EpsilonSH As Decimal                     ' Valeur de la déformation du béton pour le calcul du retrait
    Public lRetraitEnrobage As Boolean              ' Indique si on applique le retrait du béton à l'enrobage partiel
    Public ArmaYoung As Decimal                     ' Limite d'élasticité des armatures

    Public GraviteG As Decimal                      ' Accélération gravité

    Public lPsi2LongTerm As Boolean                 ' Indique si on considère la fraction Psi2 des charges d'exploitation comme un effet de long terme
    Public PsiLPermanent As Decimal                 ' Coefficient de fluage pour les charges permanentes
    Public PsiLRetrait As Decimal                   ' Coefficient de fluage pour les charges de retrait
    Public AgeT0G1() As Decimal                     ' Age au chargement du béton, cas de charge G1, 0 pour la dalle, 1 pour l'enrobage
    Public AgeT0G2() As Decimal                     ' Age au chargement du béton, cas de charge G2, 0 pour la dalle, 1 pour l'enrobage
    Public AgeT0SH() As Decimal                     ' Age au chargement du béton, cas de charge SH, 0 pour la dalle, 1 pour l'enrobage

    Public AgeT As Decimal                          ' Age du béton au temps du calcul

    Public lElasticDesignVM As Boolean              ' Indique quand un dimensionnement élastique est imposé avec le critère de Von Mises
    Public lElasticDesignCl3 As Boolean             ' Indique quand un dimensionnement élastique est imposé avec les critères des sections de classe 3

    Public lFlechesETA As Boolean                   ' Indique pour les poutres mixtes si on calcule la flèche en prenant en compte la raideur des connecteurs
    Public DeltaD As Decimal                        ' Valeur du glissement du connecteur pour une charge de PRd

    Public lMaitriseFissuration As Boolean          ' Indique si on effectue le calcul de maitrise de la fissuration
    Public FissureWk As Decimal                     ' Valeur maxi d'ouverture des fissures, en cas de maitrise de la fissuration

    Public MethodReducPlatSlim As Enu_MReducPlatSlim ' Méthode de reduction des propriétés du support de dalle dans une section slim floor, pour tenir compte de la flexion transversale

#End Region

#Region " Attributs à trier "



#End Region

#Region " Constructeurs "

    Sub New()

        '--> Coefficients partiels par défaut
        Gamma = New cls_Gamma

        ''--> Coefficients pour le calcul en dynamique
        'HivossParam = New cls_MethodHivoss

        Me.RH = tabRH(0)

        Me.Norme = Enu_Normes.EurocodesG1

        '--> Paramètres de discrétisation

        Me.dMaxNodes = 0.1
        Me.nbMinNodesTravee = 100
        Me.nbMinNodesConsole = 50

        Me.lLargeurEfficaceSimplifiee = False
        Me.lCompressionArma = False

        Me.EpsilonSH = 325 * 10 ^ -6
        Me.lRetraitEnrobage = False
        Me.ArmaYoung = 210000

        Me.GraviteG = tabGraviteG(0)

        Me.lPsi2LongTerm = True

        Me.PsiLPermanent = 1.1
        Me.PsiLRetrait = 0.55

        Me.AgeT0G1 = {28, 56}
        Me.AgeT0G2 = {28, 56}
        Me.AgeT0SH = {1, 1}

        Me.AgeT = 50 * 365

        Me.lElasticDesignVM = False
        Me.lElasticDesignCl3 = False

        Me.EtaW = 1.2

        Me.lFlechesETA = True
        Me.DeltaD = cls_OptionsCalcul.DELTAD_DEF

        '-- Maitrise de la fissuration
        Me.lMaitriseFissuration = False
        Me.FissureWk = cls_OptionsCalcul.tabWk(0)

        Me.lEnrobProp = True

        '-- Réduction des slim floor : par défaut, méthode de l'Annexe I des EN G2
        Me.MethodReducPlatSlim = Enu_MReducPlatSlim.M3_ReducLimiteElasticite
    End Sub

#End Region

#Region " Outils "

    Public ReadOnly Property lGeneration1 As Boolean
        Get
            Return (Me.Norme = Enu_Normes.EurocodesG1)
        End Get
    End Property

#End Region

#Region " Ecriture Fichier "

    'GUD --> Déplacé dans la fonction d'enregistrement de la Cls_Poutre 

    '''' <summary>
    '''' Ecriture des attributs pour enregistrement dans un fichier 
    '''' </summary>
    '''' <param name="Lines">Lignes d'écriture</param>
    'Public Sub SaveFile(ByRef Lines As List(Of String))

    '    'Lines.Add("   CGM0          = " & Gamma_M0)
    '    'Lines.Add("   CGC           = " & Gamma_C)
    '    'Lines.Add("   CGS           = " & Gamma_S)
    '    Lines.Add("   CEta          = " & Eta)

    '    Lines.Add("   CNegFlexion   = " & lCalcul_Flexion_Negative)
    '    Lines.Add("   CPosFlexion   = " & lCalcul_Flexion_Positive)

    '    Lines.Add("   CExplCharges  = " & lChargesExploitation)
    '    Lines.Add("   CRetrait      = " & lChargesRetrait)
    '    Lines.Add("   CPermCharges  = " & lChargesPermanentes)

    '    With Prop_Elastique_Enrobage
    '        Lines.Add("   PERH          = " & .RH)
    '        Lines.Add("   PEH0          = " & .h_0)
    '        Lines.Add("   PEType        = " & .type_def_t)
    '        Lines.Add("   PEt           = " & .t)
    '        Lines.Add("   PERt0         = " & .R_t_0)
    '        Lines.Add("   PEPt0         = " & .CP_t_0)
    '        Lines.Add("   PEEn_L        = " & .CE_n_L)
    '        Lines.Add("   PERn_L        = " & .R_n_L)
    '        Lines.Add("   PEPn_L        = " & .CP_n_L)
    '    End With

    '    With Prop_Elastique_Dalle
    '        Lines.Add("   PDRH          = " & .RH)
    '        Lines.Add("   PDH0          = " & .h_0)
    '        Lines.Add("   PDType        = " & .type_def_t)
    '        Lines.Add("   PDt           = " & .t)
    '        Lines.Add("   PDRt0         = " & .R_t_0)
    '        Lines.Add("   PDPt0         = " & .CP_t_0)
    '        Lines.Add("   PDEn_L        = " & .CE_n_L)
    '        Lines.Add("   PDRn_L        = " & .R_n_L)
    '        Lines.Add("   PDPn_L        = " & .CP_n_L)
    '    End With

    'End Sub

#End Region

#Region " Fonction de copie "

    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(ByVal OptionsCalculsSource As cls_OptionsCalcul, ByRef OptionsCalculsCible As cls_OptionsCalcul)
        OptionsCalculsCible = OptionsCalculsSource.Clone()
        OptionsCalculsCible.Gamma = OptionsCalculsSource.Gamma.Clone
    End Sub

#End Region

End Class
