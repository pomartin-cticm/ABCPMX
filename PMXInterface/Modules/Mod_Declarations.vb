Imports PMXMoteur2

Module Mod_Declarations

#Region " Variables globales environnement du logiciel "

    Public LogicielFichiers As Struc_Fichiers
    Public LogicielRep As Struc_RepertoireLogiciel
    Public LogicielInfo As Struc_InformationLogiciel
    Public LogicielOptions As Struc_OptionsLogiciel
    Public OptionsScope As Struc_OptionsScope
    Public LocalOptionsScope As Struc_OptionsScope          ' Pour la saisie des paramètres dans la fenêtre des options de calcul
    Public OptionsCalcul As Struc_OptionsCalcul
    Public LocalOptionsCalcul As Struc_OptionsCalcul        ' Pour la saisie des paramètres dans la fenêtre des options de calcul

#End Region

#Region " Parametrage fichiers "

    Public Const ExtensionBase As String = ".dtb"
    Public Const RepBase As String = "DataBases"
    Public Const RacProfile As String = "AM_HRProfiles"
    Public Const RacAcier As String = "AM_HRSteels"
    Public Const RacGoujons As String = "Studs"
    Public Const RacBacs As String = "Sheetings V3"

#End Region

#Region " Structures "

    Enum EnuMaitre
        CTICM
        ArcelorMittal
    End Enum

    Public Structure Struc_Fichiers

        ''' <summary>
        ''' Fichier langue pour l'interface
        ''' </summary>
        Public Langue As String

        ''' <summary>
        ''' Fichier langue pour la note de calcul
        ''' </summary>
        Public LangueNDC As String

        ''' <summary>
        ''' Liste des 10 derniers fichiers ouverts
        ''' </summary>
        Public RecentFiles As List(Of String)

        ''' <summary>
        ''' Fichier de base de données des sections
        ''' </summary>
        Public Base_Sections As String

        ''' <summary>
        ''' Fichier de base de données des bacs acier
        ''' </summary>
        Public Base_Bacs As String

        ''' <summary>
        ''' Fichier de base de données des aciers
        ''' </summary>
        Public Base_Aciers As String

        ''' <summary>
        ''' Fichier de base de données des goujons soudés
        ''' </summary>
        Public Base_Goujons As String

        '''' <summary>
        '''' Fichier de base de données des icones
        '''' </summary>
        'Public Icone As String

    End Structure

    Public Structure Struc_RepertoireLogiciel

        ''' <summary>
        ''' Répertoire de configuration
        ''' </summary>
        Public Config As String

        ''' <summary>
        ''' Répertoire d'installation
        ''' </summary>
        Public Install As String

        ''' <summary>
        ''' Répertoire des images à charger
        ''' </summary>
        Public Images As String

        ''' <summary>
        ''' Répertoire de travail
        ''' </summary>
        Public Travail As String

        ''' <summary>
        ''' Répertoire de travail par défaut
        ''' </summary>
        Public TravailDefaut As String

        ''' <summary>
        ''' Indique si le répertoire de travail est celui par défaut ou le dernier utilisé
        ''' </summary>
        Public lTravailDefaut As Boolean

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

        Public UserName As String                   'Nom de l'utilisateur
        Public CompanyName As String                'Nom de l'entreprise

        Public RepertoireTravail As String          'Répertoire de l'espace de travail
        Public lRepTravailDefault As Boolean        'Répertoire de travail par défaut ou le dernier utilisé

        Public lUpdateStart As Boolean              'Vérification des mises à jour au démarrage du logiciel

        Public lFenetres As Boolean                 'Fenêtres indépendantes
        Public lNoS235 As Boolean                   'Indique si on applique le filtre empechant la sélection de nuance S235/S275 en mode normal 

        Public Gamma As cls_Gamma

    End Structure

    Public Structure Struc_InformationLogiciel

        Public Maitre As EnuMaitre

        ''' <summary>
        ''' Nom du logiciel
        ''' </summary>
        Public NomLogiciel As String

        Public Racine As String

        ''' <summary>
        ''' Version du logiciel
        ''' </summary>
        Public Version As String

        ''' <summary>
        ''' Extension des fichiers du logiciel
        ''' </summary>
        Public Extension As String

        ''' <summary>
        ''' Liste des langues du logiciel
        ''' </summary>
        Public ListeLangue() As String

        ''' <summary>
        ''' Liste des langues de la NDC
        ''' </summary>
        Public ListeLangueNDC() As String

        ''' <summary>
        ''' Liste des normes utilisées de calcul
        ''' </summary>
        Public ListeNorme() As String

        '--> Unités de longueur
        Public Unit_Longueur() As String              'pour les textes
        Public Transfert_Longueur() As Decimal        'pour les conversions
        Public Format_Longueur() As String            'pour l'affichage - précision
        Public NbDigitMax_Longueur() As Integer     'nombre maxi de decimale pour l'affichage         

        '--> Unités d'effort
        Public Unit_Effort() As String              'pour les textes
        Public Transfert_Effort() As Decimal        'pour les conversions
        Public Format_Effort() As String            'pour l'affichage - précision
        Public NbDigitMax_Effort() As Integer       'nombre maxi de decimale pour l'affichage    

        '--> Unités de moment
        Public Unit_Moment() As String              'pour les textes
        Public Transfert_Moment() As Decimal        'pour les conversions
        Public Format_Moment() As String            'pour l'affichage - précision
        Public NbDigitMax_Moment() As Integer       'nombre maxi de decimale pour l'affichage    

        '--> Unités pour les inerties
        Public Unit_Inerties() As String            'pour les textes
        Public Transfert_Inerties() As Decimal      'pour les conversions
        Public Format_Inerties() As String          'pour l'affichage - précision
        Public NbDigitMax_Inerties() As Integer     'nombre maxi de decimale pour l'affichage    

        '--> Unités pour les contraintes
        Public Unit_Contraintes() As String         'pour les textes
        Public Transfert_Contraintes() As Decimal   'pour les conversions
        Public Format_Contraintes() As String       'pour l'affichage - précision
        Public NbDigitMax_Contraintes() As Integer  'nombre maxi de decimale pour l'affichage    

        '--> Unités pour les modules d'élasticité
        Public Unit_ModulesY() As String            'pour les textes
        Public Transfert_ModulesY() As Decimal      'pour les conversions
        Public Format_ModulesY() As String          'pour l'affichage - précision
        Public NbDigitMax_ModulesY() As Integer     'nombre maxi de decimale pour l'affichage    

        'Les unites internes pour les contraintes sont en MPa

        Public DetailNDC As Enum_NiveauDetailNDC    'Niveau de détails de la NDC - synthèse et complète

    End Structure

    Public Structure Struc_OptionsScope         ' Options définissant le domaine d'application du logiciel
        Public PorteeMin As Decimal             ' Portée minimale d'une travée normale
        Public PorteeMax As Decimal             ' Portée maximale d'une travée normale
        Public PorteeConsoleMin As Decimal      ' Portée minimale d'une travée en console
        Public RatioPorteeConsoleMax As Decimal ' Valeur maxi du ratio portée console / porte travée adjacente
        Public NbMaxiEtaisP As Integer          ' Nombre maxi d'étais ponctuels / travée normale
        'Public EpDalleMin As Decimal            ' Epaisseur minimale de dalle
        Public ThetaH As Decimal                ' Angle d'inclinaison / verticale des parois d'un renformis
        Public EpDallePleineMin As Decimal      ' Epaisseur de dalle plein mini
        Public RatioEpRenformisMax As Decimal   ' Ratio épaisseur maxi d'un renformis
        Public RatioEpPredalleMax As Decimal    ' Ratio epaisseur maxi d'une prédalle
        Public EpDalleMixteMin As Decimal       ' Epaisseur de dalle mixte mini (au dessus du bac)
        Public RhoCBetonLegerMax As Decimal     ' Masse volumique maximale d'un béton léger
        Public RhoCBetonLegerMin As Decimal     ' Masse volumique minimale d'un béton léger
    End Structure

    Public Enum Enu_Normes
        Eurocodes_G1
        Eurocodes_G2
    End Enum

    Public Structure Struc_OptionsCalcul                ' Options de calcul --------------------------------
        Public Norme As cls_OptionsCalcul.Enu_Normes    ' Norme de calcul
        Public lLargeurEfficaceSimplifiee As Boolean    ' Largeur efficace de la dalle béton selon modèle simplifié
        Public lCompressionArma As Boolean              ' Indique si l'on prend en compte les armatures comprimées dans le calcul des propriétés de section
        Public dMaxNodes As Decimal                     ' Distance maximale entre deux noeuds
        Public nbMinNodesTravee As Integer              ' Nombre mini de noeuds par travée normale
        Public nbMinNodesConsole As Integer             ' Nombre mini de noeuds par travée console
        Public EsArmatures As Decimal                   ' Module d'Young des barres d'armature
    End Structure

    Public Sub InitialiseOptionsScope()
        '-----------------------------------------------------------------------------
        '   09/08/23 :  Création
        '-----------------------------------------------------------------------------
        '   Initialisation des paramètres définissant le domaine d'appication du logiciel
        '-----------------------------------------------------------------------------

        OptionsScope.PorteeMin = PORTEEMIN
        OptionsScope.PorteeMax = PORTEEMAX
        OptionsScope.PorteeConsoleMin = CONSOLEMIN
        OptionsScope.RatioPorteeConsoleMax = RATIOCONSOLEMAX

        OptionsScope.NbMaxiEtaisP = NBPROPPINGMAX

        'OptionsScope.EpDalleMin = 0.05
        OptionsScope.ThetaH = THETAHDEFAULT
        OptionsScope.EpDallePleineMin = EPDALLEMINIDEFAUT
        OptionsScope.RatioEpRenformisMax = RATIOEPRENFORMISMAXDEFAUT
        OptionsScope.EpDalleMixteMin = EPDALLEMIXTEMINIDEFAUT

        OptionsScope.RhoCBetonLegerMax = 2200
        OptionsScope.RhoCBetonLegerMin = 800

        OptionsScope.RatioEpPredalleMax = 0.5

    End Sub

    Public Sub InitialiseOptionsCalcul()

        OptionsCalcul.Norme = Enu_Normes.Eurocodes_G1
        OptionsCalcul.lCompressionArma = False
        OptionsCalcul.lLargeurEfficaceSimplifiee = False

        OptionsCalcul.dMaxNodes = 1
        OptionsCalcul.nbMinNodesTravee = 9
        OptionsCalcul.nbMinNodesConsole = 1

        OptionsCalcul.EsArmatures = 210 * 10 ^ 3

    End Sub

#End Region

#Region " Enumérations "

    Public Enum Enu_AffichageEL
        ELS
        ELU
        ELF
        Construction
    End Enum

    Public AffichageEL As Enu_AffichageEL = Enu_AffichageEL.ELU

    ''' <summary>
    ''' Type de variable à afficher
    ''' </summary>
    Public Enum Enu_TypeVariable
        Longueur
        Dimension
        Effort
        Moment
        Inertie
        ContrainteMPa
        ContrainteGPa
        SansType
        InertieCM4
        Millimetre
        AireLongueurNDC
        Contrainte
        ModuleY
        AireCM2
        ModuleCM3
        InertieWCM6
    End Enum

    Public Enum EnuFenetres
        PPCasDeCharge
        PPCombinaison
        Chargements
        Combinaisons
        Connexion
        Dalle
        DalleN
        Enrobage
        Entraxes
        Etaiement
        Gamma
        Hivoss
        Identification
        LargeurEfficace
        Main
        Maintiens
        Options
        Portees
        Section
    End Enum

    Public iFrmAppel As EnuFenetres

#End Region

#Region " Constantes et valeurs par défaut "

    Private Const EPDALLEMINIDEFAUT As Decimal = 0.1       ' Epaisseur mini de dalle pleine =10 cm
    Private Const RATIOEPRENFORMISMAXDEFAUT As Decimal = 0.4
    Private Const EPDALLEMIXTEMINIDEFAUT As Decimal = 0.05 ' Epaisseur de dalle mixte mini 5 cm au dessus du bac

    Private Const THETAHDEFAULT As Decimal = 30             ' Angle inclinaison renformis

    Private Const PORTEEMIN As Decimal = 5
    Private Const PORTEEMAX As Decimal = 25

    Public Const ENTRAXEMIN As Decimal = 0.5
    Public Const ENTRAXEMAX As Decimal = 10

    Private Const CONSOLEMIN As Decimal = 0.5
    Private Const RATIOCONSOLEMAX As Decimal = 0.3

    Public Const NBPROPPINGMIN As Integer = 0
    Private Const NBPROPPINGMAX As Integer = 5

    Public Const NBRESTRAINMIN As Integer = 0
    Public Const NBRESTRAINMAX As Integer = 5

    Public Const GAMMA_ACTION_MIN As Decimal = 0
    Public Const GAMMA_ACTION_MAX As Decimal = 2

    Public Const PSI_COMBINAISON_MIN As Decimal = 0
    Public Const PSI_COMBINAISON_MAX As Decimal = 1

    Public Const GAMMA_RESISTANCE_MIN As Decimal = 1        ' Comment on divise par gamma, autant éviter les valeurs nulles !
    Public Const GAMMA_RESISTANCE_MAX As Decimal = 2

#End Region

#Region " Paramétrage Logiciel "

    Public Const EXTENSIONLANGUE As String = ".LNG"

#End Region

#Region " Paramètres de STYLE (Couleurs...) "

    '== Palette CTICM
    Public BleuCTICM As Color = Color.FromArgb(0, 90, 161)
    Public GrisCTICM As Color = Color.FromArgb(156, 169, 171)

    '== Palette AM
    Public OrangeAM As Color = Color.FromArgb(255, 65, 10)
    Public GrisFonceAM As Color = Color.FromArgb(105, 105, 105)
    Public GrayAM As Color = Color.FromArgb(197, 188, 164)
    Public PaleGrayAM As Color = Color.FromArgb(220, 212, 194)
    Public PurpleAM As Color = Color.FromArgb(134, 95, 127)
    Public BlueAM As Color = Color.FromArgb(92, 127, 146)
    Public LightBlueAM As Color = Color.FromArgb(157, 177, 201)
    Public LightGreenAM As Color = Color.FromArgb(186, 196, 140)
    Public GreenAM As Color = Color.FromArgb(112, 164, 137)
    Public TamAM As Color = Color.FromArgb(200, 143, 66)

    Public CouleurBackBandeaux As Color = BlueAM     ' SystemColors.ControlDarkDark
    Public CouleurForeBandeaux As Color = SystemColors.ControlLightLight

    Public CouleurReadOnly As Color = SystemColors.Control       'PaleGrayAM     ' SystemColors.ControlDark

    Public LargeurColonneSaisie As Integer = 250

    Public ColorSelect As Color = Color.DarkRed
    Public ColorNonSelect As Color = Color.DarkSlateGray

    Public FontSymbolNormal As New Font("Arial", 8.25)
    Public FontSymbolIndice As New Font("Arial", 6.25)
    Public FontSymbolGrec As New Font("Symbol", 8.25)

    Public FontBase As New Font("Arial", 8.25)

    Public CouleurAcierNormal As Color = Color.DarkSlateGray
    Public CouleurAcierSelect As Color = BleuCTICM
    Public CouleurBetonNormal As Color = Color.LightGray
    Public CouleurBetonSelect As Color = Color.DarkSlateGray
    Public CouleurTremieNormal As Color = Color.White
    Public CouleurArmaNormal As Color = Color.LightSlateGray
    Public CouleurArmaSelect As Color = Color.DarkOrange

    Public CouleurConnecteurNormal As Color = Color.DarkOrange

    Public ColorFixe As Color = Color.LightGray

    Public ColorForceSelect As Color = OrangeAM


#End Region

#Region " Variables globales "

    Public MyProjet As New cls_Projet
    'Public Project As New List(Of Cls_Projet)
    'Public ProjetEnCours As Integer = 0


    Public ErreurCapacite_LNG As String
    Public ErreurNonNul_LNG As String
    Public ErreurNonNum_LNG As String
    Public ErreurHorsBornes_LNG As String

    '--> Séparateur dans les fichiers
    Public SEPARATEURS() As String = {" ", "=", ";"}

#End Region

#Region " Note de calcul "

    Public MyNote As Cls_Rapport

    Public Enum PositionTexteInCell
        Centre
        Gauche
        Droite
    End Enum

    Public ClePos As New Dictionary(Of PositionTexteInCell, String)

    Public Enum Enum_NiveauDetailNDC
        'UltraSynthese
        Synthese
        Complete
        'Detaillee
        'DessinPoutre
    End Enum

    Public SEPARATEURS_NDC() As String = {" "}

    Public Class Bordures
        Public Const Aucun As Integer = 0
        Public Const Gauche As Integer = 1
        Public Const Haut As Integer = 2
        Public Const Droite As Integer = 4
        Public Const Bas As Integer = 8
        Public Const Tous As Integer = 15
    End Class

    Public Const RAPPORTA4 As Single = 297.0! / 210.0!

#End Region

#Region " Controle de l'affichage "

    Public Enum Enu_OptionsCalcul
        Calcul
        Gamma
        Scope
        Incendie
    End Enum

    Public Enum Enu_OptionsLogiciel
        General
        Directories
        Units
        Databases
        Expert
    End Enum

    Public Structure strucLastIndexWindow
        Dim OptionsCalcul As Enu_OptionsCalcul
        Dim OptionsLogiciel As Enu_OptionsLogiciel
    End Structure

    Public LastIndexW As strucLastIndexWindow

#End Region

End Module
