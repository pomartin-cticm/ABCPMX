Public Module Mod_DeclarationsX

#Region " Paramètres généraux logiciels "

    Public Const NomLogiciel As String = "ABCPMX"

#End Region

#Region " Variables globales "

    Public OptionsSlimFloor As Struc_OptionsSlimFloor
    Public LocalOptionsSlimFloor As Struc_OptionsSlimFloor  ' Pour la saisie des paramètres dans la fenetre des options de calcul 

#End Region

#Region " Enumérations,constantes et structures "

    Public Const kConvMPaPa As Decimal = 1000 ^ 2
    Public Const kConvMinSec As Decimal = 60

#Region " Structures "

    Structure strucShearBuckling

        Dim ElancementW As Decimal                  ' Elancement de l'âme
        Dim LimiteElancementW As Decimal            ' Limite d'élancement au dela de laquelle il faut vérifier le voilement par cisaillement
        Dim lCheckRequired As Boolean               ' Indique si la vérification de la résistance est requise

    End Structure

    Public Structure Struc_OptionsLogiciel

        Public lExpert As Boolean                   'Activation Mode Expert
        Public lDebug As Boolean                    'Fonctionnement en mode debug
        Public IndLangue As Integer                 'Indice de la langue de l'interface
        Public IndLangueNDC As Integer              'Indice de la langue de la note de calcul

        Public IndUnitLongueur As Integer           'Indice de l'unité de longueur utilisée
        Public IndUnitDimension As Integer          'Indice de l'unité de longueur utilisée
        Public IndUnitEffort As Integer             'Indice de l'unité d'effort utilisée
        Public IndUnitMoment As Integer             'Indice de l'unité de moment utilisée
        Public IndUnitInerties As Integer           'Indice de l'unité des inerties
        Public IndUnitContraintes As Integer        'Indice de l'unité des contraintes
        Public IndUnitModulesY As Integer           'Indice de l'unité des modules d'élasticité
        Public IndUnitModuleW As Integer            'Indice de l'unité des modules de flexion

        Public UserName As String                   'Nom de l'utilisateur
        Public CompanyName As String                'Nom de l'entreprise

        Public RepertoireTravail As String          'Répertoire de l'espace de travail
        Public lRepTravailDefault As Boolean        'Répertoire de travail par défaut ou le dernier utilisé

        'Public lUpdateStart As Boolean              'Vérification des mises à jour au démarrage du logiciel
        Public lControlWebVersion As Boolean        'Controle version logiciel au démarrage
        Public lControlWebFichier As Boolean        'Controle version base profilés au démarrage

        Public lFenetres As Boolean                 'Fenêtres indépendantes

        Public Gamma As cls_Gamma

        'Public EtaW As Decimal                      ' Valeur utilisée dans le calcul du voilement par cisaillement de l'âme des profilés métalliques

    End Structure

    Public Structure Struc_OptionsCalcul                ' Options de calcul --------------------------------
        Public Norme As cls_OptionsCalcul.Enu_Normes    ' Norme de calcul
        Public lLargeurEfficaceSimplifiee As Boolean    ' Largeur efficace de la dalle béton selon modèle simplifié
        Public lCompressionArma As Boolean              ' Indique si l'on prend en compte les armatures comprimées dans le calcul des propriétés de section
        Public dMaxNodes As Decimal                     ' Distance maximale entre deux noeuds
        Public nbMinNodesTravee As Integer              ' Nombre mini de noeuds par travée normale
        Public nbMinNodesConsole As Integer             ' Nombre mini de noeuds par travée console
        Public EsArmatures As Decimal                   ' Module d'Young des barres d'armature
        Public DeltaCDev As Decimal                     ' Marge de calcul pour les tolérances d'exécution
        Public PsiLPermanent As Decimal                 ' Coefficient de fluage pour les charges permanentes
        Public PsiLRetrait As Decimal                   ' Coefficient de fluage pour les charges de retrait
        Public TimeT0G1() As Decimal                    ' Temps au chargement du béton, cas de charge G1, 0 pour la dalle, 1 pour l'enrobage
        Public TimeT0G2() As Decimal                    ' Temps au chargement du béton, cas de charge G2, 0 pour la dalle, 1 pour l'enrobage
        Public TimeT0SH() As Decimal                    ' Temps au chargement du béton, cas de charge SH, 0 pour la dalle, 1 pour l'enrobage
        Public EtaW As Decimal                          ' Valeur utilisée dans le calcul du voilement par cisaillement de l'âme des profilés métalliques
    End Structure

    Public Structure Struc_OptionsSlimFloor
        Public Hslimmax As Decimal                      ' hauteur maximale des sections slimfloors
        Public Bappmin As Decimal                       ' Largeur d'appui min à respecter
        Public Tpinfmin As Decimal                      ' Epaisseur min des plats soudés
        Public Twcdmin As Decimal                       ' Epaisseur min des âmes pour une connexion par armatures
    End Structure

    Public Structure struc_OptionsFeu

        Public EmissiviteC As Decimal                   ' Emissivité du béton
        Public EmissiviteF As Decimal                   ' Emissivité du feu
        Public AlphaC As Decimal                        ' Coefficient de transfert thermique par convection
        Public AlphaCC As Decimal                       ' Coefficient de transfert thermique par convection au dessus de la dalle
        Public ksh As Decimal                           ' Coefficient de correction pour l'effet d'ombre
        Public Phi As Decimal                           ' Coefficient de forme
        Public TempRef As Decimal                       ' Température de référence

    End Structure

#End Region

#End Region

#Region " Tableau 7 EN 1994-1-1 "
    ''' <summary>
    ''' Contrainte maximale dans les aciers autorisée en fonction du diamètre des barres et de l'ouverture des fissures (cf. Tableau 7.1 de l'EC4)
    ''' Colonne 0 = Contrainte autorisée
    ''' Colonne 1 = Diamètre max quand wk,max = 0.4 mm
    ''' Colonne 2 = Diamètre max quand wk,max = 0.3 mm
    ''' Colonne 3 = Diamètre max quand wk,max = 0.2 mm
    ''' </summary>
    Private sigma_S1_Ds As Decimal(,) =
        {{160, 40 / 1000, 32 / 1000, 25 / 1000},
        {200, 32 / 1000, 25 / 1000, 16 / 1000},
        {240, 20 / 1000, 16 / 1000, 12 / 1000},
        {280, 16 / 1000, 12 / 1000, 8 / 1000},
        {320, 12 / 1000, 10 / 1000, 6 / 1000},
        {360, 10 / 1000, 8 / 1000, 5 / 1000},
        {400, 8 / 1000, 6 / 1000, 4 / 1000},
        {450, 6 / 1000, 5 / 1000, 4 / 1000}}

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="wk_max">Largeur d'ouverture maximale en mètre</param>
    ''' <param name="phi_max">Diamètre maximal des armatures en mètre</param>
    ''' <returns></returns>
    Public Function Get_sigma_S1_Ds(wk_max As Decimal, phi_max As Decimal)

        '------------------------------------------------------------------------------------------------------------------
        '   16/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Renvoi la contrainte admissible dans les aciers en fonction du diamètre max des aciers
        '   Recherche le diamètre dans le tableau supérieur à phi_max le plus proche
        '------------------------------------------------------------------------------------------------------------------
        '   wk_maw    [E] :   Largeur des fissures admissibles (0.2; 0.3 ou 0.4 mm)
        '   phi_max   [E] :   Diamètre max des aciers disposés dans la sectoin de béton
        '   sigma_S1  [S] :   Retourne la contrainte admissible dans les aciers
        '------------------------------------------------------------------------------------------------------------------

        Dim sigma_S1 As Decimal

        Dim i As Integer 'indice de ligne
        Dim j As Integer 'indice de colonne

        Select Case wk_max
            Case 0.4 / 1000
                j = 1

            Case 0.3 / 1000
                j = 2

            Case 0.2 / 1000
                j = 3

            Case Else
                Throw New Exception("Valeur wk_max hors limite: wk_max = 0.2, 0.3 ou 0.4 mm (variables d'entrée doit être en mètres)")

        End Select

        If phi_max > sigma_S1_Ds(0, j) Then Throw New Exception("Valeur Phi_max hors limite: Phi_max ne peut pas être supérieur à " & sigma_S1_Ds(0, j) & " (m)")

        For i = 0 To sigma_S1_Ds.Length - 1
            If phi_max > sigma_S1_Ds(i, j) Then
                sigma_S1 = sigma_S1_Ds(i - 1, 0)
                Exit For
            End If
        Next

        If i = sigma_S1_Ds.Length - 1 Then sigma_S1 = sigma_S1_Ds(i, 0)

        Return sigma_S1
    End Function


    ''' <summary>
    ''' Contrainte maximale dans les aciers autorisée en fonction de l'espacement  des barres et de l'ouverture des fissures (cf. Tableau 7.2 de l'EC4)
    ''' Colonne 0 = Contrainte autorisée
    ''' Colonne 1 = Espacement max quand wk,max = 0.4 mm
    ''' Colonne 2 = Espacement max quand wk,max = 0.3 mm
    ''' Colonne 3 = Espacement max quand wk,max = 0.2 mm
    ''' </summary>
    Private sigma_S1_es As Decimal(,) =
        {{160, 300 / 1000, 300 / 1000, 200 / 1000},
        {200, 300 / 1000, 250 / 1000, 150 / 1000},
        {240, 250 / 1000, 200 / 1000, 100 / 1000},
        {280, 200 / 1000, 150 / 1000, 50 / 1000},
        {320, 150 / 1000, 100 / 1000, 0},
        {360, 100 / 1000, 50 / 1000, 0}}

    Public Function Get_sigma_S1_es(wk_max As Decimal, es_max As Decimal)

        '------------------------------------------------------------------------------------------------------------------
        '   16/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Renvoi la contrainte admissible dans les aciers en fonction de l'espacement max des aciers
        '   Recherche le diamètre dans le tableau supérieur à phi_max le plus proche
        '------------------------------------------------------------------------------------------------------------------
        '   wk_maw    [E] :   Largeur des fissures admissibles (0.2; 0.3 ou 0.4 mm)
        '   es_max    [E] :   espacement max des aciers disposés dans la sectoin de béton
        '   sigma_S1  [S] :   Retourne la contrainte admissible dans les aciers
        '------------------------------------------------------------------------------------------------------------------

        Dim sigma_S1 As Decimal

        Dim i As Integer 'indice de ligne
        Dim j As Integer 'indice de colonne

        Select Case wk_max
            Case 0.4 / 1000
                j = 1

            Case 0.3 / 1000
                j = 2

            Case 0.2 / 1000
                j = 3

            Case Else
                Throw New Exception("Valeur wk_max hors limite: wk_max = 0.2, 0.3 ou 0.4 mm (variables d'entrée doit être en mètres)")

        End Select

        If es_max > sigma_S1_es(0, j) Then Throw New Exception("Valeur es_max hors limite: Phi_max ne peut pas être supérieur à " & sigma_S1_es(0, j) & " (m)")

        For i = 0 To sigma_S1_es.Length - 1
            If es_max > sigma_S1_es(i, j) Then
                sigma_S1 = sigma_S1_es(i - 1, 0)
                Exit For
            End If
        Next

        If i = sigma_S1_es.Length - 1 Then sigma_S1 = sigma_S1_es(i, 0)

        Return sigma_S1
    End Function

#End Region


#Region " Gestion noms chargements "

    Public NomChargements() As String           ' Nom des cas de charge utilisateur
    Public NomChargesA() As String              ' Nom des cas de charge analyse

    Public labelDessin As Dictionary(Of String, String)

#End Region



End Module
