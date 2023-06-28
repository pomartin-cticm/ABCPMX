Imports PMXMoteur2

Module Mod_Declarations

#Region " Variables globales environnement du logiciel "

    Public LogicielFichiers As Struc_Fichiers
    Public LogicielRep As Struc_RepertoireLogiciel
    Public LogicielInfo As Struc_InformationLogiciel
    Public LogicielOptions As Struc_OptionsLogiciel

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

        ''' <summary>
        ''' Fichier de base de données des icones
        ''' </summary>
        Public Icone As String

    End Structure

    Public Structure Struc_RepertoireLogiciel

        ''' <summary>
        ''' Répertoire de configuration
        ''' </summary>
        Public RepertoireConfig As String

        ''' <summary>
        ''' Répertoire d'installation
        ''' </summary>
        Public RepertoireInstall As String

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

        Public Gamma As Cls_Gamma

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
    End Structure

#End Region

#Region " Enumérations "

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
    End Enum

    Public Enum EnuFenetres
        Accueil
        Portees
        Entraxes
        Dalle
        DalleN
        Section
        Enrobage
        Connexion
        Maintiens
        Etaiement
        Chargements
        Combinaisons
        Options
        Hivoss
        Main
    End Enum

    Public iFrmAppel As EnuFenetres

#End Region

#Region " Constantes et valeurs par défaut "

    Public Const PORTEEMIN As Decimal = 5
    Public Const PORTEEMAX As Decimal = 25

    Public Const ENTRAXEMIN As Decimal = 0.5
    Public Const ENTRAXEMAX As Decimal = 10

    Public Const CONSOLEMIN As Decimal = 0.5
    Public Const RATIOCONSOLEMAX As Decimal = 0.3

    Public Const NBPROPPINGMIN As Integer = 0
    Public Const NBPROPPINGMAX As Integer = 5

    Public Const NBRESTRAINMIN As Integer = 0
    Public Const NBRESTRAINMAX As Integer = 5



#End Region

#Region " Paramètres de STYLE "

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

    Public CouleurReadOnly As Color = GrayAM     ' SystemColors.ControlDark

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


End Module
