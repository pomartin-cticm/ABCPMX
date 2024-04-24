Imports PMXMoteur2

Public Module Mod_Declarations

#Region " Variables globales environnement du logiciel "

    Public LogicielFichiers As Struc_Fichiers
    Public LogicielRep As Struc_RepertoireLogiciel
    Public LogicielInfo As Struc_InformationLogiciel
    Public LogicielOptions As Struc_OptionsLogiciel
    Public OptionsScope As Struc_OptionsScope
    Public LocalOptionsScope As Struc_OptionsScope          ' Pour la saisie des paramètres dans la fenêtre des options de calcul
    Public OptionsCalcul As Struc_OptionsCalcul
    Public LocalOptionsCalcul As Struc_OptionsCalcul        ' Pour la saisie des paramètres dans la fenêtre des options de calcul
    Public OptionsSlimFloor As Struc_OptionsSlimFloor
    Public LocalOptionsSlimFloor As Struc_OptionsSlimFloor  ' Pour la saisie des paramètres dans la fenetre des options de calcul 

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
        ''' Fichier de base de données des connecteurs personnalisés
        ''' </summary>
        Public Base_Goujons_Perso As String

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
        ''' Année de la dernière version deployee
        ''' </summary>
        Public AnneeVersion As String

        ''' <summary>
        ''' Adresse mail du support à contacter
        ''' </summary>
        Public MailSupport As String

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

        '--> Unités pour les modules de flexion
        Public Unit_ModuleW() As String            'pour les textes
        Public Transfert_ModuleW() As Decimal      'pour les conversions
        Public Format_ModuleW() As String          'pour l'affichage - précision
        Public NbDigitMax_ModuleW() As Integer     'nombre maxi de decimale pour l'affichage   

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

    Public Structure Struc_OptionsSlimFloor
        Public hslimmax As Decimal 'hauteur maximale des sections slimfloors
        Public bappmin As Decimal 'Largeur d'appui min à respecter
        Public tpinfmin As Decimal 'Epaisseur min des plats soudés
    End Structure

    Public Enum Enu_Normes
        Eurocodes_G1
        Eurocodes_G2
    End Enum

    Public Sub InitialiseOptionsScope()
        '-----------------------------------------------------------------------------
        '   09/08/23 :  Création
        '-----------------------------------------------------------------------------
        '   Initialisation des paramètres définissant le domaine d'appication du logiciel
        '-----------------------------------------------------------------------------

        OptionsScope.PorteeMin = My.Settings.PorteeMin
        OptionsScope.PorteeMax = My.Settings.PorteeMax
        OptionsScope.PorteeConsoleMin = My.Settings.PorteeConsoleMin
        OptionsScope.RatioPorteeConsoleMax = My.Settings.RatioPorteeConsoleMax

        OptionsScope.NbMaxiEtaisP = My.Settings.NbMaxiEtaisP

        'OptionsScope.EpDalleMin = 0.05
        OptionsScope.ThetaH = My.Settings.ThetaH
        OptionsScope.EpDallePleineMin = My.Settings.EpDallePleineMin
        OptionsScope.RatioEpRenformisMax = My.Settings.RatioEpRenformisMax
        OptionsScope.EpDalleMixteMin = My.Settings.EpDalleMixteMin

        OptionsScope.RhoCBetonLegerMax = My.Settings.RhoCBetonLegerMax
        OptionsScope.RhoCBetonLegerMin = My.Settings.RhoCBetonLegerMin

        OptionsScope.RatioEpPredalleMax = My.Settings.RatioEpPredalleMax

    End Sub

    Public Sub InitialiseOptionsCalcul()

        OptionsCalcul.Norme = My.Settings.Norme
        OptionsCalcul.lCompressionArma = My.Settings.lCompressionArma
        OptionsCalcul.lLargeurEfficaceSimplifiee = My.Settings.lLargeurEfficaceSimplifiee

        OptionsCalcul.dMaxNodes = My.Settings.dMaxNodes
        OptionsCalcul.nbMinNodesTravee = My.Settings.nbMinNodesTravee
        OptionsCalcul.nbMinNodesConsole = My.Settings.nbMinNodesConsole

        OptionsCalcul.EsArmatures = My.Settings.EsArmatures
        OptionsCalcul.DeltaCDev = My.Settings.deltaCDev

        OptionsCalcul.PsiLPermanent = My.Settings.PsiLPermanent
        OptionsCalcul.PsiLRetrait = My.Settings.PsiLRetrait

        ReDim OptionsCalcul.TimeT0G1(1)
        ReDim OptionsCalcul.TimeT0G2(1)
        ReDim OptionsCalcul.TimeT0SH(1)

        OptionsCalcul.TimeT0G1(0) = My.Settings.AgeT0G1_Dalle
        OptionsCalcul.TimeT0G1(1) = My.Settings.AgeT0G1_Enrobage
        OptionsCalcul.TimeT0G2(0) = My.Settings.AgeT0G2_Dalle
        OptionsCalcul.TimeT0G2(1) = My.Settings.AgeT0G2_Enrobage
        OptionsCalcul.TimeT0SH(0) = My.Settings.AgeT0SH_Dalle
        OptionsCalcul.TimeT0SH(1) = My.Settings.AgeT0SH_Enrobage

        OptionsCalcul.EtaW = My.Settings.EtaW

        OptionsSlimFloor.hslimmax = My.Settings.hslimmax
        OptionsSlimFloor.bappmin = My.Settings.bappmin
        OptionsSlimFloor.tpinfmin = My.Settings.tpinfmin

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
        AireCM2
        AireMM2
        AireLongueurNDC
        Contrainte
        ContrainteGPa
        ContrainteMPa
        Dimension
        Effort
        ForceRepartie
        ChargeSurfacique
        Frequence
        Inertie
        InertieCM4
        InertieWCM6
        Longueur
        LongueurCM
        Millimetre
        ModuleCM3
        ModuleY
        Moment
        Rigidite
        SansType
        Temperature
    End Enum

    Public Enum EnuFenetres
        About
        Chargements
        Combinaisons
        Connexion
        Dalle
        DalleN
        DalleSlimFloor
        EditBac
        EditGoujons
        EditSection
        Enrobage
        Entraxes
        Etaiement
        Gamma
        Hivoss
        Identification
        Incendie
        Main
        Maintiens
        Options
        OptionsCalculPoutre
        OptionsIncendie
        Portees
        PPCasDeCharge
        PPCombinaison
        PPHivoss
        PPLargeurEfficace
        PPModePropre
        PPVerifications
        SectionAcier
        SectionSFB
        SectionIFB
        SectionSAB
        Test
    End Enum

    Public iFrmAppel As EnuFenetres

    Public Enum enu_ComWindow
        OK
        Cancel
    End Enum

    Public ComWindow As enu_ComWindow

#End Region

#Region " Constantes et valeurs par défaut "

    'Private Const EPDALLEMINIDEFAUT As Decimal = 0.1       ' Epaisseur mini de dalle pleine =10 cm
    'Private Const RATIOEPRENFORMISMAXDEFAUT As Decimal = 0.4
    'Private Const EPDALLEMIXTEMINIDEFAUT As Decimal = 0.05 ' Epaisseur de dalle mixte mini 5 cm au dessus du bac

    'Private Const THETAHDEFAULT As Decimal = 30             ' Angle inclinaison renformis

    'Private Const PORTEEMIN As Decimal = 5
    'Private Const PORTEEMAX As Decimal = 25

    Public Const ENTRAXEMIN As Decimal = 0.5
    Public Const ENTRAXEMAX As Decimal = 10

    'Private Const CONSOLEMIN As Decimal = 0.5
    'Private Const RATIOCONSOLEMAX As Decimal = 0.3

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

    Public Const DIASTUDMIN As Double = 0.016       ' Diametre minimal des goujons
    Public Const DIASTUDMAX As Double = 0.022       ' Diametre maximal des goujons

    Public Const DELTASAISIE As Double = 0.001       ' Tolérance sur les bornes de saisie  (issu d'ACB+)

#End Region

#Region " Paramétrage Logiciel "

    Public Const EXTENSIONLANGUE As String = ".LNG"

    Public Const EMAIL_CTICM As String = "support.logiciels@cticm.com"
    Public Const EMAIL_ARCELORMITTAL As String = "Steligence.engineering@arcelormittal.com"

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

    Public CouleurTraveeSelect As Color = Color.DarkOrange 'OrangeAM
    Public CouleurTraveeMouse As Color = OrangeAM
    Public CouleurAppui As Color = GreenAM
    Public CouleurProfile As Color = Color.LightGray
    Public CouleurDalle As Color = Color.DarkGray


    Public MyOrange As Color = Color.FromArgb(231, 62, 1)
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

    Public SEPDECIMAL As String = Mid(Format(1.1, "0.0"), 2, 1)
#End Region

#Region " Note de calcul "

    Public Structure struc_OptionsNdC
        Dim lShowHivossCurve As Boolean         ' Indique si affichage des courbes Hivoss
        Dim lDispFMLoadCase As Boolean          ' Indique si affichage des sollicitations par cas de charge
        Dim lDispFMDiagrams As Boolean          ' Indique si affichage des diagrammes de sollicitations
        Dim lDispFM_ULS As Boolean              ' Indique si affichage des sollicitations sous ELU
        Dim lDispFM_SLS As Boolean              ' Indique si affichage des sollicitations sous ELS
        Dim lDispFM_FLS As Boolean              ' Indique si affichage des sollicitations sous ELU fatigue
        Dim lDispSigmaCharges As Boolean        ' Indique si affichage des contraintes par cas de charge
        Dim lDispMelPoutreMixte As Boolean      ' Indique si affichage des moments élastiques pour les poutres mixtes
    End Structure

    Public OptionsNdC As struc_OptionsNdC

    Public Structure struc_OptionsDiagrammes
        Dim lDessNumeros As Boolean
        Dim lDessCharges As Boolean
        Dim lDessInerties As Boolean
        Dim lDessEffortT As Boolean
        Dim lDessMoment As Boolean
        Dim lDessDeformee As Boolean
        Dim lDessValEnv As Boolean
    End Structure

    Public OptionsDiagrammesCDC As struc_OptionsDiagrammes
    Public OptionsDiagrammesELU As struc_OptionsDiagrammes
    Public OptionsDiagrammesELF As struc_OptionsDiagrammes
    Public OptionsDiagrammesELS As struc_OptionsDiagrammes


    Public Sub InitialiseOptionsNdC()
        With OptionsNdC
            .lShowHivossCurve = My.Settings.lNdCCourbeHivoss
            .lDispFMLoadCase = My.Settings.lNdCDispLoadCase
            .lDispFMDiagrams = My.Settings.lNdCShowDiagram
            .lDispFM_ULS = My.Settings.lNdCDispFM_ELU
            .lDispFM_SLS = My.Settings.lNdCDispFM_ELS
            .lDispFM_FLS = My.Settings.lNdCDispFM_ELF
            .lDispSigmaCharges = My.Settings.lNdCDispSigmaCharges
        End With

        With OptionsDiagrammesCDC
            .lDessNumeros = My.Settings.lDessNumeros_CDC
            .lDessCharges = My.Settings.lDessCharges_CDC
            .lDessInerties = My.Settings.lDessInerties_CDC
            .lDessEffortT = My.Settings.lDessEffortT_CDC
            .lDessMoment = My.Settings.lDessMoment_CDC
            .lDessDeformee = My.Settings.lDessDeformee_CDC
            .lDessValEnv = My.Settings.lDessValEnv_CDC
        End With

        With OptionsDiagrammesELU
            .lDessNumeros = My.Settings.lDessNumeros_ELU
            .lDessCharges = My.Settings.lDessCharges_ELU
            .lDessInerties = My.Settings.lDessInerties_ELU
            .lDessEffortT = My.Settings.lDessEffortT_ELU
            .lDessMoment = My.Settings.lDessMoment_ELU
            .lDessDeformee = My.Settings.lDessDeformee_ELU
            .lDessValEnv = My.Settings.lDessValEnv_ELU
        End With

        With OptionsDiagrammesELF
            .lDessNumeros = My.Settings.lDessNumeros_ELF
            .lDessCharges = My.Settings.lDessCharges_ELF
            .lDessInerties = My.Settings.lDessInerties_ELF
            .lDessEffortT = My.Settings.lDessEffortT_ELF
            .lDessMoment = My.Settings.lDessMoment_ELF
            .lDessDeformee = My.Settings.lDessDeformee_ELF
            .lDessValEnv = My.Settings.lDessValEnv_ELF
        End With

        With OptionsDiagrammesELS
            .lDessNumeros = My.Settings.lDessNumeros_ELS
            .lDessCharges = My.Settings.lDessCharges_ELS
            .lDessInerties = My.Settings.lDessInerties_ELS
            .lDessEffortT = My.Settings.lDessEffortT_ELS
            .lDessMoment = My.Settings.lDessMoment_ELS
            .lDessDeformee = My.Settings.lDessDeformee_ELS
            .lDessValEnv = My.Settings.lDessValEnv_ELS
        End With

    End Sub

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
        Slimfloor
        Incendie
    End Enum

    Public Enum Enu_OptionsLogiciel
        General
        Directories
        Units
        Databases
        Expert
        NoteCalcul
    End Enum

    Public Structure strucLastIndexWindow
        Dim OptionsCalcul As Enu_OptionsCalcul
        Dim OptionsLogiciel As Enu_OptionsLogiciel
    End Structure

    Public LastIndexW As strucLastIndexWindow

#End Region

#Region " Gestion des langues "

    'Public NomChargements() As String           ' Nom des cas de charge utilisateur
    'Public NomChargesA() As String              ' Nom des cas de charge analyse

#End Region

End Module
