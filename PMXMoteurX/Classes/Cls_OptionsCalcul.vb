'Imports PropMix_Engine.Cls_Section

Public Class cls_OptionsCalcul


#Region " Enumérations et constantes "

    Public Shared tabRH() As Decimal = {50, 80}

    'Public Shared t0Retrait As Decimal = 1

    Enum Enu_Normes
        EurocodesG1
        EurocodesG2
    End Enum

#End Region

#Region " Attributs "

    Public RH As Decimal                            ' Humidité pour le calcul du béton

    Public Gamma As cls_Gamma                       ' Coefficients partiels pour le calcul
    Public HivossParam As cls_OptionsHivoss         ' Coefficients pour le calcul dynamique définits dans la Frm_Hivoss
    Public Norme As Enu_Normes                      ' Norme de calcul

    Public EtaW As Decimal                           ' Valeur utilisée dans le calcul du voilement par cisaillement de l'âme des profilés métalliques

    Public lLargeurEfficaceSimplifiee As Boolean    ' Largeur efficace de la dalle béton selon modèle simplifié
    Public lCompressionArma As Boolean              ' Indique si l'on prend en compte les armatures comprimées dans le calcul des propriétés de section
    Public dMaxNodes As Decimal                     ' Distance maximale entre deux noeuds
    Public nbMinNodesTravee As Integer              ' Nombre mini de noeuds par travée normale
    Public nbMinNodesConsole As Integer             ' Nombre mini de noeuds par travée console

    Public EpsilonSH As Decimal                     ' Valeur de la déformation du béton pour le calcul du retrait

    Public GraviteG As Decimal                      ' Accélération gravité

    Public PsiLPermanent As Decimal                 ' Coefficient de fluage pour les charges permanentes
    Public PsiLRetrait As Decimal                   ' Coefficient de fluage pour les charges de retrait
    Public TimeT0G1() As Decimal                    ' Age au chargement du béton, cas de charge G1, 0 pour la dalle, 1 pour l'enrobage
    Public TimeT0G2() As Decimal                    ' Age au chargement du béton, cas de charge G2, 0 pour la dalle, 1 pour l'enrobage
    Public TimeT0SH() As Decimal                    ' Age au chargement du béton, cas de charge SH, 0 pour la dalle, 1 pour l'enrobage

    Public TimeT As Decimal                         ' Age du béton au temps du calcul

    Public lElasticDesign As Decimal                ' Indique quand un dimensionnement élastique est imposé

#End Region

#Region " Attributs à trier "

    '=== C'est déjà dans la cls_AcierArmatures
    '''' <summary>
    '''' Module d'young pour les armatures
    '''' </summary>
    'Public ArmaYoung As Decimal

    ''' <summary>
    ''' Indique si prise en compte des armatures comprimées
    ''' </summary>
    Public lArmaComprimee As Boolean

    ''' <summary>
    ''' Indique si on prend en compte le renformis dans le calcul des propriétés
    ''' </summary>
    Public lRenformis As Boolean

    ''' <summary>
    ''' Degré de connexion ??????
    ''' </summary>
    Public Eta As Decimal

    ''' <summary>
    ''' Indique si on prend en compte l'interaction MV pour le calcul plastique
    ''' </summary>
    Public lInterActionMV As Boolean

    ''' <summary>
    ''' Effort tranchant pour la prise en compte de l'interaction MV
    ''' </summary>
    Public VEd As Decimal

    ''' <summary>
    ''' Calcul en flexion positive
    ''' </summary>
    Public lCalcul_Flexion_Positive As Boolean

    ''' <summary>
    ''' Calcul en flexion négative
    ''' </summary>
    Public lCalcul_Flexion_Negative As Boolean

    ''' <summary>
    ''' Prise en compte ou non des charges permanentes pour le calcul élastique (coef d'équivalence)
    ''' </summary>
    Public lChargesPermanentes As Boolean

    ''' <summary>
    ''' Prise en compte ou non du retrait pour le calcul élastique (coef d'équivalence)
    ''' </summary>
    Public lChargesRetrait As Boolean

    ''' <summary>
    ''' Prise en compte ou non des charges d'exploitation pour le calcul élastique (coef d'équivalence)
    ''' </summary>
    Public lChargesExploitation As Boolean

    ''' <summary>
    ''' Prise en compte d'une valeur personnalisée du coef d'équivalence
    ''' </summary>
    Public lChargesCustom As Boolean

    ''' <summary>
    ''' Valeur personnalisée du coefficient d'équivalence
    ''' </summary>
    Public NeqCustom As Decimal

    '''' <summary>
    '''' Propriétés élastiques par rapport au béton de l'enrobage
    '''' </summary>
    'Public Prop_Elastique_Enrobage As New Cls_Prop_Elastique

    '''' <summary>
    '''' Propriétés élastiques par rapport au béton de la dalle
    '''' </summary>
    'Public Prop_Elastique_Dalle As New Cls_Prop_Elastique



    ''' <summary>
    ''' Temps de premier chargement des charges permanentes (0 pour la dalle, 1 pour l'enrobage)
    ''' </summary>
    Public t0Permanentes(1) As Decimal



#End Region

#Region " Constructeurs "

    Sub New()

        '--> Coefficients partiels par défaut
        Gamma = New cls_Gamma

        '--> Coefficients pour le calcul en dynamique
        HivossParam = New cls_OptionsHivoss

        Me.RH = tabRH(0)

        Me.Norme = Enu_Normes.EurocodesG1

        '--> Paramètres de discrétisation

        Me.dMaxNodes = 0.5
        Me.nbMinNodesTravee = 10
        Me.nbMinNodesConsole = 5

        Me.lLargeurEfficaceSimplifiee = False
        Me.lArmaComprimee = False

        Me.EpsilonSH = 325 * 10 ^ -6

        Me.GraviteG = 9.81

        Me.PsiLPermanent = 1.1
        Me.PsiLRetrait = 0.55

        Me.TimeT0G1 = {28, 56}
        Me.TimeT0G2 = {28, 56}
        Me.TimeT0SH = {1, 1}

        Me.TimeT = 50 * 365

        Me.lElasticDesign = False

        Exit Sub


        '--> Paramètres de calcul par défaut
        Me.lCalcul_Flexion_Positive = True
        Me.lCalcul_Flexion_Negative = False

        '--> Degré de connexion
        Me.Eta = 1

        ''--> Module d'young des armatures
        'Me.ArmaYoung = Cls_Acier.EYACIER

        '--> Armatures comprimées
        Me.lArmaComprimee = False

        '--> Renformis 
        Me.lRenformis = False

        '--> Interaction MV
        Me.lInterActionMV = False
        Me.VEd = 0

        '--> Paramètres pour les coefficients d'équivalence acier béton
        Me.RH = tabRH(0)
        Me.t0Permanentes(0) = 28
        Me.t0Permanentes(1) = 28
        Me.lChargesRetrait = False
        Me.lChargesCustom = False
        Me.lChargesExploitation = True
        Me.lChargesPermanentes = True
        Me.NeqCustom = 7

        Me.Norme = Enu_Normes.EurocodesG1

    End Sub

#End Region

#Region " Outils "



#End Region

#Region " Ecriture Fichier "

    'GUD --> Déplacé dans la fonction d'enregistrement de la Cls_Poutre 

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    'Public Sub EcrireFile(ByRef Lines As List(Of String))

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

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
