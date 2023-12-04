Imports System.IO
Imports System.Runtime.CompilerServices
Imports PMXMoteur2

Module Mod_Demarrage

#Region " Déclarations "

    Dim lDebug As Boolean = False

    Structure strucAcierLocal
        Dim Nuance As String
        Dim Qualite As String
        Dim Reduc As String
        Dim lAvailable As Boolean
    End Structure

#End Region

#Region "===DEMARRAGE==="

    Public Sub Main()

        'MsgBox("Hello PMX")

        Application.EnableVisualStyles()

        InitialiseLogiciel()

        Frm_PMX.ShowDialog()

    End Sub

    Public Sub InitialiseLogiciel()
        '---------------------------------------------------------------------------------------------------------------
        '   25/05/2023 :    POM - Création
        '---------------------------------------------------------------------------------------------------------------
        '   Initialisation générale des paramètres du logiciel ABCPMX-II
        '---------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------


        '--> Récupération des informations générales du logociel - Non modifiable par l'utilisateur

        LogicielInfo.NomLogiciel = "ABCPMX-II"
        LogicielInfo.Version = "1.0"
        LogicielInfo.Extension = "pmx"
        LogicielInfo.Racine = "ABCPMX"

        LogicielInfo.Maitre = EnuMaitre.CTICM
        LogicielOptions.lNoS235 = (LogicielInfo.Maitre = EnuMaitre.ArcelorMittal)
        LogicielOptions.lDebug = False

        LogicielRep.Install = Application.StartupPath
        LogicielRep.Config = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version

        LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Gamma
        LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General

        InitialiseDebug()

        '--> Répertoires

        '# répertoire configuration
        If Not IO.Directory.Exists(LogicielRep.Config) Then 'R22-001
            IO.Directory.CreateDirectory(LogicielRep.Config)
        End If

        '# répertoires de travail
        LogicielRep.TravailDefaut = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        LogicielRep.lTravailDefaut = False      'Utilisation du dernier fichier ouvert
        LogicielRep.Travail = LogicielRep.TravailDefaut
        'If Not Directory.Exists(RepACB.WorkData) Then Directory.CreateDirectory(RepACB.WorkData)

        '--> Langues

        InitialiseLangues()

        '--> Options de la NdC

        InitialiseOptionsNdC()

        '--> Normes

        LogicielInfo.ListeNorme = {"EN 1994-1-1", "prEN 1994-1-1"} 'Normes

        '--> Unités

        LogicielInfo.Unit_Longueur = {"mm", "cm", "m"}              'Unités des longueurs/dimensions - mm pour les dimensions et m pour les longueurs par défaut (interne m)
        LogicielInfo.Transfert_Longueur = {0.001, 0.01, 1}
        LogicielInfo.Format_Longueur = {"0.0", "0.00", "0.000"}
        LogicielInfo.NbDigitMax_Longueur = {1, 2, 3}

        LogicielInfo.Unit_Effort = {"N", "daN", "kN"}               'Unités des efforts - kN par défaut     (interne N)
        LogicielInfo.Transfert_Effort = {1, 10, 1000}
        LogicielInfo.Format_Effort = {"0.0", "0.00", "0.0"}
        LogicielInfo.NbDigitMax_Effort = {0, 1, 3}

        LogicielInfo.Unit_Moment = {"N.m", "daN.m", "kN.m"}         'Unités des moments - kN.m par défaut   (interne Nm)
        LogicielInfo.Transfert_Moment = {1, 10, 1000}
        LogicielInfo.Format_Moment = {"0.0", "0.00", "0.0"}
        LogicielInfo.NbDigitMax_Longueur = {0, 1, 3}

        LogicielInfo.Unit_Inerties = {"mm4", "cm4", "m4"}           'Unités des inerties - cm4 par défaut    (interne m4)
        LogicielInfo.Transfert_Inerties = {0.001 ^ 4, 0.01 ^ 4, 1 ^ 4}
        LogicielInfo.Format_Inerties = {"0.0", "0.00", "0.0000"}
        LogicielInfo.NbDigitMax_Inerties = {0, 0, 6}

        LogicielInfo.Unit_Contraintes = {"P", "kPa", "MPa"}         'Unités des contraintes - MPa par défaut (interne MPa)
        LogicielInfo.Transfert_Contraintes = {0.000001, 0.0001, 1}
        LogicielInfo.Format_Contraintes = {"0", "0", "0.0"}
        LogicielInfo.NbDigitMax_Contraintes = {1, 2, 3}

        LogicielInfo.Unit_ModulesY = {"MPa", "GPa"}                 'Unités des modules d'élasticité - GPa par défaut (interne MPa)
        LogicielInfo.Transfert_ModulesY = {1, 1000}
        LogicielInfo.Format_ModulesY = {"0", "0.0"}
        LogicielInfo.NbDigitMax_ModulesY = {0, 3}

        '--> Récupération des options du logiciel - modifiable par l'utilisateur
        Try
            '===> Options générales <============================================================================================

            '--> Mode Expert
            LogicielOptions.lExpert = My.Settings.lExpertMode

            '--> Langues
            LogicielOptions.IndLangue = Array.IndexOf(LogicielInfo.ListeLangue, LogicielInfo.ListeLangue(My.Settings.IndLangue))
            LogicielOptions.IndLangueNDC = Array.IndexOf(LogicielInfo.ListeLangueNDC, LogicielInfo.ListeLangueNDC(My.Settings.IndLangueNDC))

            '--> Unités
            '# Dimensions Longueurs
            LogicielOptions.IndUnitDimension = Array.IndexOf(LogicielInfo.Unit_Longueur, LogicielInfo.Unit_Longueur(My.Settings.indUnitDimension))
            LogicielOptions.IndUnitLongueur = Array.IndexOf(LogicielInfo.Unit_Longueur, LogicielInfo.Unit_Longueur(My.Settings.indUnitLongueur))

            '# Contraintes et module d'élasticité
            LogicielOptions.IndUnitContraintes = Array.IndexOf(LogicielInfo.Unit_Contraintes, LogicielInfo.Unit_Contraintes(My.Settings.indUnitContraintes))
            LogicielOptions.IndUnitModulesY = Array.IndexOf(LogicielInfo.Unit_ModulesY, LogicielInfo.Unit_ModulesY(My.Settings.indUnitModuleY))

            '# Efforts et moments
            LogicielOptions.IndUnitEffort = Array.IndexOf(LogicielInfo.Unit_Effort, LogicielInfo.Unit_Effort(My.Settings.indUnitEffort))
            LogicielOptions.IndUnitMoment = Array.IndexOf(LogicielInfo.Unit_Moment, LogicielInfo.Unit_Moment(My.Settings.indUnitMoment))

            '--> Fichiers récents
            LogicielFichiers.RecentFiles = New List(Of String)
            If My.Settings.RecentFiles IsNot Nothing Then
                For i = 0 To My.Settings.RecentFiles.Count - 1
                    LogicielFichiers.RecentFiles.Add(My.Settings.RecentFiles(i))
                Next
            End If

            '--> Fichiers
            LogicielFichiers.Base_Sections = LogicielRep.Config & "\" & RacProfile & ExtensionBase
            LogicielFichiers.Base_Aciers = LogicielRep.Config & "\" & RacAcier & ExtensionBase
            LogicielFichiers.Base_Goujons = LogicielRep.Config & "\" & LogicielInfo.Racine & "_" & RacGoujons & ExtensionBase
            LogicielFichiers.Base_Goujons_Perso = LogicielRep.Config & "\" & LogicielInfo.Racine & "_" & RacGoujons & "Custom" & ExtensionBase
            LogicielFichiers.Base_Bacs = LogicielRep.Config & "\" & LogicielInfo.Racine & "_" & RacBacs & ExtensionBase

            '--> Base de données
            InitialisationBasesDonnees()
            '--> Récupération des données de la database dans le catalogue (aciers et profilés)
            InitialiseCatalogueProfiles(LogicielFichiers.Base_Sections, MyCatalogue)
            InitialiseBaseAciers(LogicielFichiers.Base_Aciers, SteelBase)

            '--> Bacs acier
            LireBaseBacs(BaseBacs)

            '--> Connecteurs
            'LireBaseGoujons(LogicielFichiers.Base_Goujons, BaseGoujons)
            GetDataBaseStuds(BaseGoujons)

            '--> Gamma coefficients partiels

            LogicielOptions.Gamma = New cls_Gamma

            LogicielOptions.Gamma.GammaM0 = My.Settings.GammaMZero
            LogicielOptions.Gamma.GammaM1 = My.Settings.GammaM1
            LogicielOptions.Gamma.GammaM2 = My.Settings.GammaM2

            LogicielOptions.Gamma.GammaC = My.Settings.GammaC
            LogicielOptions.Gamma.GammaVs = My.Settings.GammaVs
            LogicielOptions.Gamma.GammaVc = My.Settings.GammaVc
            'LogicielOptions.Gamma.GammaVp = My.Settings.GammaVp
            LogicielOptions.Gamma.lGammaV_unique = My.Settings.lGammaV_unique
            LogicielOptions.Gamma.GammaS = My.Settings.GammaS
            LogicielOptions.Gamma.GammaP = My.Settings.GammaP

            LogicielOptions.Gamma.GammaM_fi = My.Settings.GammaM_fi
            LogicielOptions.Gamma.GammaC_fi = My.Settings.GammaC_fi
            LogicielOptions.Gamma.GammaV_fi = My.Settings.GammaV_fi

            LogicielOptions.Gamma.GammaG_sup = My.Settings.GammaG_sup
            LogicielOptions.Gamma.GammaG_inf = My.Settings.GammaG_inf
            LogicielOptions.Gamma.GammaQ = My.Settings.GammaQ

            LogicielOptions.Gamma.Psi0_Q1 = My.Settings.Psi0
            LogicielOptions.Gamma.Psi1_Q1 = My.Settings.Psi1
            LogicielOptions.Gamma.Psi2_Q1 = My.Settings.Psi2

            LogicielOptions.Gamma.Psi0_Q2 = My.Settings.Psi0
            LogicielOptions.Gamma.Psi1_Q2 = My.Settings.Psi1
            LogicielOptions.Gamma.Psi2_Q2 = My.Settings.Psi2

            '--> Coefficient pour le voilement par cisaillement de la poutre acier

            LogicielOptions.EtaW = My.Settings.EtaW

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Mod_Demarrage/InitialiseLogiciel")
        End Try

        LogicielOptions.lFenetres = True
        LogicielInfo.DetailNDC = Enum_NiveauDetailNDC.Complete

        '--> Options du domaine d'application et options de calcul

        InitialiseOptionsScope()
        InitialiseOptionsCalcul()

        '--> MAJ des noms de fichiers langue

        InitialiseLNGFileName(LogicielOptions.IndLangue, LogicielFichiers.Langue)
        InitialiseLNGFileName_NDC()

        ' '--> MAJ du nom fichier icones

        'UpdateIconesFileName()

        '--> Initialisation répertoires

        InitialiseRepImage()

    End Sub

    <Conditional("DEBUG")> Private Sub InitialiseDebug()
        lDebug = True
        LogicielOptions.lDebug = True
    End Sub

    Private Sub InitialiseLangues()
        '--------------------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------------
        '   Initialisation des langues disponibles
        '--------------------------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyRep As String = LogicielRep.Install
        Dim NbLangues As Integer

        '--> Initialisation

        LogicielInfo.ListeLangue = {"English", "Français"}
        LogicielInfo.ListeLangueNDC = {"English", "Français"}

        If lDebug Then MyRep = MyRep & "\..\..\Langues"

        '--> Récupération des langues disponibles

        InitialiseLangue(MyRep, "ABCPMX", LogicielInfo.ListeLangue, nblangues)
        If NbLangues = 0 Then
            MsgBox("Erreur fichiers langues non disponibles | Error language files missing", MsgBoxStyle.Critical, "Mod_Demmarage/InitialiseLangues")
            Stop
        End If

        InitialiseLangue(MyRep, "ABCPMX_NdC", LogicielInfo.ListeLangueNDC, NbLangues)
        If NbLangues = 0 Then
            MsgBox("Erreur fichiers langues NdC non disponibles | Error NdC language files missing", MsgBoxStyle.Critical, "Mod_Demmarage/InitialiseLangues")
            Stop
        End If


    End Sub

    Private Sub InitialiseLangue(RepInstall As String, Racine As String, ByRef ListeLangue() As String, ByRef nbLangues As Integer)
        '--------------------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------------
        '   Initialisation des langues disponibles
        '--------------------------------------------------------------------------------------------------------
        '   RepInstall  [E] :   Répertoire d'installation
        '   Racine      [E] :   Racine du fichier langue
        '   ListeLangue [S] :   Tableau des langues disponibles dans le répertoire d'installation
        '   nbLangues   [S] :   Nombre de langues disponibles
        '--------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim tabLangues As New List(Of String)
        Dim tabAbb As New List(Of String)

        '--> Récupération des langues disponibles 

        RechercheLangue(RepInstall, Racine, tabLangues, tabAbb)

        nbLangues = tabLangues.Count

        If nbLangues > 0 Then
            ReDim ListeLangue(nbLangues - 1)
            For i As Integer = 0 To nbLangues - 1
                ListeLangue(i) = tabLangues(i)
            Next
        End If

    End Sub

    Private Sub InitialiseOptionsDatabases()
        '--> Options de la base de données
        OptionsDatabase.lNewBase = True
        OptionsDatabase.lSoftLimited = True
        OptionsDatabase.FiltreSoft = "ABC"
        OptionsDatabase.lShowSteelAvailOnly = True
        OptionsDatabase.ChoiceSteel = EnuChoiceAcier.BaseIfNoStandardSteel
        OptionsDatabase.lNoSteelLowThick = True
        OptionsDatabase.lSaveConfig = False
        OptionsDatabase.lShowEC3 = True
    End Sub

    Public Sub InitialisationBasesDonnees()

        Dim FichierSource As String

        InitialiseOptionsDatabases()

        '--> Fichier pour les profilés

        If Not File.Exists(LogicielFichiers.Base_Sections) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.Install & "\..\..\" & RepBase & "\" & RacProfile & ExtensionBase
            Else
                FichierSource = LogicielRep.Install & "\" & RepBase & "\" & RacProfile & ExtensionBase
            End If
            ' File.Copy(LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacProfile & ExtensionBase, LogicielFichiers.Database_Section)
            File.Copy(FichierSource, LogicielFichiers.Base_Sections)

        End If

        '--> Fichier pour les aciers
        If Not File.Exists(LogicielFichiers.Base_Aciers) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.Install & "\..\..\" & RepBase & "\" & RacAcier & ExtensionBase
            Else
                FichierSource = LogicielRep.Install & "\" & RepBase & "\" & RacAcier & ExtensionBase
            End If
            'File.Copy(LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacAcier & ExtensionBase, LogicielFichiers.Database_Aciers)
            File.Copy(FichierSource, LogicielFichiers.Base_Aciers)
        End If

        '--> Fichiers pour les bacs
        If Not File.Exists(LogicielFichiers.Base_Bacs) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.Install & "\..\..\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacBacs & ExtensionBase
            Else
                FichierSource = LogicielRep.Install & "\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacBacs & ExtensionBase
            End If
            File.Copy(FichierSource, LogicielFichiers.Base_Bacs)
        End If

        '--> Fichiers pour les goujons soudés
        If Not File.Exists(LogicielFichiers.Base_Goujons) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.Install & "\..\..\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacGoujons & ExtensionBase
            Else
                FichierSource = LogicielRep.Install & "\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacGoujons & ExtensionBase
            End If
            File.Copy(FichierSource, LogicielFichiers.Base_Goujons)

        End If

    End Sub

    Private Sub InitialiseRepImage()
        '--------------------------------------------------------------------------------------------
        '   10/07/2023 :    Création - POM
        '--------------------------------------------------------------------------------------------
        '   Initialisation du répertoire images
        '--------------------------------------------------------------------------------------------

        If lDebug Then
            LogicielRep.Images = LogicielRep.Install & "\..\..\Images"
        Else
            LogicielRep.Images = LogicielRep.Install & "\Images"
        End If

    End Sub

#End Region

#Region " Initialisation de la poutre "

    Public Sub InitialiseDalleDefault(ByRef myDalle As cls_Dalle)
        '--------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM - V1.00
        '--------------------------------------------------------------------------------
        '   Initialisation des paramètres de la dalle
        '--------------------------------------------------------------------------------
        '   myDalle         [E/S] :   Dalle à initialiser
        '--------------------------------------------------------------------------------

        myDalle.type = cls_Dalle.Enum_TypeDalle.Mixte
        myDalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire

    End Sub

    Public Sub InitialiseOptionsCalculPoutre(myPoutre As cls_Poutre)
        '--------------------------------------------------------------------------------
        '   14/06/23 :  Création - POM - V1.00
        '--------------------------------------------------------------------------------
        '   Initialisation des paramètres de calcul d'une poutre (avant lancement des calculs)
        '--------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre à initialiser
        '--------------------------------------------------------------------------------

        ' A COMPLETER


        myPoutre.Param.dMaxNodes = OptionsCalcul.dMaxNodes
        myPoutre.Param.nbMinNodesConsole = OptionsCalcul.nbMinNodesConsole
        myPoutre.Param.nbMinNodesTravee = OptionsCalcul.nbMinNodesTravee

    End Sub

    Public Sub InitialisePoutreDeBases(MyPoutre As cls_Poutre, ByRef lOK As Boolean)
        '--------------------------------------------------------------------------------
        '   14/06/23 :  Création - POM - V1.00
        '--------------------------------------------------------------------------------
        '   Initialisation d'une poutre à partir des paramètres des bases
        '   Laminés : section et acier
        '   PRS :   acier
        '--------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre à initialiser
        '
        '   lOK             [S] :   Indique si on a pu trouver un acier compatible
        '--------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouve As Boolean

        '--> Traitement

        TransfertProfileDeBase(MyPoutre.Section.ProfilA, MyPoutre.Section.ProfilA.Gamme, MyPoutre.Section.ProfilA.NomProfile, lOK)


        'AJOUT GUD
        ' --> Sécurité supplémentaire pour s'assurer que les valeurs qui n'ont pas de sens restent égales à 0

        Dim ha_loc, hb_loc, bfs_loc, tfs_loc, rcs_loc, bfi_loc, tfi_loc, rci_loc, tw_loc, aw_loc, plat_b_loc, plat_t_loc As Decimal
        With MyPoutre.Section.ProfilA
            ha_loc = .ha
            hb_loc = .hb
            bfs_loc = .Bfs
            tfs_loc = .Tfs
            rcs_loc = .Rcs
            bfi_loc = .Bfi
            tfi_loc = .Tfi
            rci_loc = .Rci
            tw_loc = .Tw
            aw_loc = .aW
            plat_b_loc = .Plat_b
            plat_t_loc = .Plat_t
        End With


        With MyPoutre.Section.ProfilA
            Select Case .typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    .ha = ha_loc
                    .hb = hb_loc
                    .Bfs = bfs_loc
                    .Tfs = tfs_loc
                    .Rcs = rcs_loc
                    .Bfi = bfi_loc
                    .Tfi = tfi_loc
                    .Rci = rci_loc
                    .Tw = tw_loc
                    .aW = 0
                    .Plat_b = 0
                    .Plat_t = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                    .ha = ha_loc
                    .hb = 0
                    .Bfs = bfs_loc / 2
                    .Tfs = tfs_loc
                    .Rcs = 0
                    .Bfi = bfi_loc
                    .Tfi = tfi_loc
                    .Rci = 0
                    .Tw = tw_loc
                    .aW = Math.Floor(tw_loc / 2)
                    .Plat_b = 0
                    .Plat_t = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                    .ha = ha_loc
                    .hb = 0
                    .Bfs = bfs_loc
                    .Tfs = tfs_loc
                    .Rcs = 0
                    .Bfi = bfi_loc
                    .Tfi = tfi_loc
                    .Rci = 0
                    .Tw = tw_loc
                    .aW = Math.Floor(tw_loc / 2)
                    .Plat_b = 0
                    .Plat_t = 0
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    .ha = ha_loc
                    .hb = hb_loc
                    .Bfs = bfs_loc
                    .Tfs = tfs_loc
                    .Rcs = rcs_loc
                    .Bfi = bfi_loc
                    .Tfi = tfi_loc
                    .Rci = rci_loc
                    .Tw = tw_loc
                    .aW = 0
                    .Plat_b = bfi_loc * 1.5
                    .Plat_t = tfi_loc
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    .ha = 0.7 * ha_loc
                    .hb = hb_loc
                    .Bfs = bfs_loc
                    .Tfs = tfs_loc
                    .Rcs = rcs_loc
                    .Bfi = 0
                    .Tfi = 0
                    .Rci = 0
                    .Tw = tw_loc
                    .aW = Math.Floor(tw_loc / 2)
                    .Plat_b = bfi_loc * 1.5
                    .Plat_t = tfi_loc
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    .ha = 0.7 * ha_loc
                    .hb = hb_loc
                    .Bfs = 0
                    .Tfs = 0
                    .Rcs = 0
                    .Bfi = bfi_loc
                    .Tfi = tfi_loc
                    .Rci = rci_loc
                    .Tw = tw_loc
                    .aW = Math.Floor(tw_loc / 2)
                    .Plat_b = bfs_loc * 0.75
                    .Plat_t = tfs_loc
            End Select

        End With

        'Fin Ajout GUD

        AssocieAcierCompatible(MyPoutre, LogicielFichiers.Base_Aciers, LogicielFichiers.Base_Sections, lTrouve)

        '# Cela ne marche pas !!!!
        'MyPoutre.Param.Gamma = LogicielOptions.Gamma.Clone

        MyPoutre.Dalle.ThetaRd = OptionsScope.ThetaH

    End Sub

    Public Sub InitialiseGoujonDeBase(ByRef MyG As cls_Connecteur, ByRef lTrouve As Boolean)
        '--------------------------------------------------------------------------------
        '   09/08/23 :  Création - POM - V1.00
        '--------------------------------------------------------------------------------
        '   Initialisation d'un goujon à partir de la base de données goujon
        '--------------------------------------------------------------------------------
        '   MyG             [E/S] : Goujon à initialiser
        '   lTrouve         [S] :   Indique si on a pu initialiser
        '--------------------------------------------------------------------------------

        '--> Declaration

        Dim nbStud, iStud As Integer

        '--> Initialisation

        lTrouve = False
        iStud = -1

        If BaseGoujons Is Nothing Then
            nbStud = 0
        Else
            nbStud = BaseGoujons.Count
        End If

        '--> Recherche du goujons dans la liste

        Do While (Not lTrouve) And (iStud < nbStud - 1)
            iStud += 1
            lTrouve = (BaseGoujons(iStud).nom = MyG.nom)
        Loop

        '--> Traitement de la recherche

        If Not lTrouve Then
            'Si on n'a pas trouvé le connecteur recherché, on prend le premier dans la liste (si elle existe)
            iStud = 0
            If nbStud > 0 Then lTrouve = True
        End If

        If lTrouve Then
            MyG.nom = BaseGoujons(iStud).nom
            MyG.d = BaseGoujons(iStud).d
            MyG.hsc = BaseGoujons(iStud).hsc
            MyG.Fy = BaseGoujons(iStud).Fy
            MyG.Fu = BaseGoujons(iStud).Fu
        End If
    End Sub

    Public Sub InitialiseBacDeBase(ByRef MyBac As cls_Bac, ByRef lTrouve As Boolean)
        '--------------------------------------------------------------------------------
        '   25/06/23 :  Création - POM - V1.00
        '--------------------------------------------------------------------------------
        '   Initialisation d'un bac à partir de la base de données bac
        '--------------------------------------------------------------------------------
        '   MyBac           [E] :   Bac à initialiser
        '
        '   lOK             [S] :   Indique si on a pu initialiser
        '--------------------------------------------------------------------------------

        If MyBac.lDatabase Then

            If BaseBacs.ContainsKey(MyBac.Etiquette) Then

                MyBac = BaseBacs(MyBac.Etiquette).Clone
                lTrouve = True

            Else

                Dim kvp As KeyValuePair(Of String, cls_Bac) = BaseBacs.First

                MyBac = BaseBacs(kvp.Key).Clone

                lTrouve = False
            End If

        End If

    End Sub

    Private Sub TransfertProfileDeBase(MyProfile As cls_ProfilA, ByVal Gamme As String, ByVal Profile As String, ByRef lOK As Boolean)
        '----------------------------------------------------------------------------
        '
        '   03/07/12 :  Création - Version 3.00
        '
        '----------------------------------------------------------------------------
        '
        '   Récupération d'un profilé dans la nouvelle base de données
        '
        '----------------------------------------------------------------------------
        '
        '   BaseFile    [E] :   Nom du fichier base de données
        '   Gamme       [E] :   Gamme du profilé
        '   Profile     [E] :   Nom du profile
        '   lOK         [S] :   Indique si récupération OK
        '
        '----------------------------------------------------------------------------

        MyProfile.ha = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
        MyProfile.hb = MyCatalogue.Series(Gamme).Profiles(Profile).Ht
        MyProfile.Bfs = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MyProfile.Bfi = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MyProfile.Tfs = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MyProfile.Tfi = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MyProfile.Tw = MyCatalogue.Series(Gamme).Profiles(Profile).Tw
        MyProfile.Rci = MyCatalogue.Series(Gamme).Profiles(Profile).Rc
        MyProfile.Rcs = MyCatalogue.Series(Gamme).Profiles(Profile).Rc

        ReDim MyProfile.IndStandart(MyCatalogue.nbStandard)
        For i As Integer = 0 To MyCatalogue.nbStandard - 1
            MyProfile.IndStandart(i) = MyCatalogue.Series(Gamme).Profiles(Profile).IndStandart(i)
        Next

    End Sub

    Public Sub AssocieAcierCompatible(MyPoutre As cls_Poutre, ByVal FileSteels As String, ByVal FileProfiles As String, ByRef lTrouve As Boolean)
        '--------------------------------------------------------------------------------
        '
        '   06/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Associe à une profilé le premier acier compatible dans la base de données
        '
        '--------------------------------------------------------------------------------
        '
        '   MyPoutre        [E] :   Poutre à initialiser
        '   FileSteels      [E] :   Nom du fichier binaire base de données de aciers
        '   FileProfiles    [E] :   Nom du fichier binaire base de données des profilés
        '
        '   lTrouve         [S] :   Indique si on a pu trouver un acier compatible
        '
        '--------------------------------------------------------------------------------
        '
        '   On prend le premier acier S355 disponible
        '   et si on ne le trouve pas, le premier acier tout court
        '
        '--------------------------------------------------------------------------------

        '--> Déclarations

        Dim iAcier As Integer
        Dim MySteels As New List(Of strucAcierLocal)
        Dim iStd As Short
        Dim EpMax As Decimal = Math.Max(MyPoutre.Section.ProfilA.Tfs, MyPoutre.Section.ProfilA.Tw)

        ExtraireAciersCompatibles(EpMax, MyPoutre.Section.ProfilA.IndStandart, MySteels)

        AnalyseAciersListe(MySteels, True, cls_Acier.NUANCEDEFAULT, lTrouve, iAcier)

        If Not lTrouve Then
            AnalyseAciersListe(MySteels, False, "", lTrouve, iAcier)
        End If

        If lTrouve Then
            MyPoutre.Section.Acier.Nuance = MySteels(iAcier).Nuance
            MyPoutre.Section.Acier.Qualite = MySteels(iAcier).Qualite
            MyPoutre.Section.Acier.Reduction = MySteels(iAcier).Reduc
            MyPoutre.Section.Acier.EpMax = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).EpMax
            MyPoutre.Section.Acier.iBase = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).iBase
            MyPoutre.Section.Acier.iStandart = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).StIndex

            '==V4.00
            iStd = SteelBase.IndexStd.IndexOf(SteelBase.Grades(MyPoutre.Section.Acier.Nuance).Qualites(MyPoutre.Section.Acier.Qualite).ReductionCurv(MyPoutre.Section.Acier.Reduction).StIndex)
            MyPoutre.Section.Acier.iTabStandart = iStd

            MyPoutre.Section.Acier.Plages.Clear()

            For i As Integer = 0 To SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).Plages.Count - 1
                MyPoutre.Section.Acier.Plages.Add(SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).Plages(i))
            Next
        End If
    End Sub

    Private Sub AnalyseAciersListe(ByVal MySteels As List(Of strucAcierLocal), ByVal lImposedGrade As Boolean,
                                   ByVal MyGrade As String, ByRef lTrouve As Boolean, ByRef iAcier As Integer)
        '--------------------------------------------------------------------------------
        '
        '   21/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Extrait tous les aciers compatibles avec un profilé 
        '
        '--------------------------------------------------------------------------------
        '
        '   FileSteels      [E] :   Nom du fichier binaire base de données de aciers
        '   FileProfiles    [E] :   Nom du fichier binaire base de données des profilés
        '
        '--------------------------------------------------------------------------------

        iAcier = -1

        lTrouve = False

        Do While (Not lTrouve) And iAcier < MySteels.Count - 1
            iAcier += 1
            If lImposedGrade Then
                lTrouve = (MySteels(iAcier).Nuance.Trim.ToUpper = MyGrade.ToUpper.Trim)
            Else
                lTrouve = True
            End If
        Loop
    End Sub

    Private Sub ExtraireAciersCompatibles(EpMax As Decimal, iStandard() As Short, ByRef MySteels As List(Of strucAcierLocal))
        '--------------------------------------------------------------------------------
        '
        '   21/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Extrait tous les aciers compatibles avec un profilé 
        '
        '--------------------------------------------------------------------------------
        '
        '   EpMax           [E] :   Epaisseur max du profilé
        '   iStandard       [E] :   Indice de la norme du profilé
        '   MySteels        [S] :   Liste des aciers compatibles
        '
        '--------------------------------------------------------------------------------

        Dim CorIndStd As Dictionary(Of Short, Short) = Nothing
        Dim lCompatible As Boolean
        Dim lIsNuanceCompatibleProfile As Boolean
        Dim lAdd As Boolean
        Dim SteelLoc As strucAcierLocal
        Dim ListeSteel As New List(Of strucAcierLocal)
        Dim nbComp As Integer

        '--> Initialisation

        GetTabCorrespondanceIndiceStandart(LogicielFichiers.Base_Sections, CorIndStd)
        MySteels.Clear()
        ListeSteel.Clear()
        nbComp = 0

        '--> Boucle sur les aciers de la base

        For Each kvpGrade As KeyValuePair(Of String, strucGrade) In SteelBase.Grades

            For Each kvpQualite As KeyValuePair(Of String, strucQualite) In kvpGrade.Value.Qualites

                For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv

                    lCompatible = SteelIsToCompatibleToProfile(EpMax, iStandard, SteelBase, CorIndStd, kvpGrade.Key, kvpQualite.Key, kvpSteel.Key, OptionsDatabase.ChoiceSteel, lIsNuanceCompatibleProfile)

                    If lCompatible Then
                        SteelLoc.Nuance = kvpGrade.Key
                        SteelLoc.Qualite = kvpQualite.Key
                        SteelLoc.Reduc = kvpSteel.Key
                        SteelLoc.lAvailable = lIsNuanceCompatibleProfile
                        ListeSteel.Add(SteelLoc)
                        If lIsNuanceCompatibleProfile Then nbComp += 1
                    End If

                Next
            Next
        Next

        For i As Integer = 0 To ListeSteel.Count - 1
            If Not ListeSteel(i).lAvailable Then
                If (OptionsDatabase.ChoiceSteel = EnuChoiceAcier.BaseIfNoStandardSteel) Then
                    lAdd = (nbComp = 0)
                Else
                    lAdd = True
                End If
            Else
                lAdd = True
            End If
            If lAdd Then
                MySteels.Add(ListeSteel(i))
            End If
        Next
    End Sub

#End Region

#Region " Gestion Langue "

    ''' <summary>
    ''' Mise à jour du nom du fichier langue Interface
    ''' </summary>
    Public Sub InitialiseLNGFileName(IndLangue As Integer, ByRef FichierLangue As String)
        '-----------------------------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Initialisation du fichier langue en fct du choix utilisateur
        '-----------------------------------------------------------------------------------------------------------------
        '   IndLangue       [E] :   Indice de la langue (choisie par l'utilisateur ou option logiciel)
        '   FichierLangue   [S] :   Fichier langue pour l'interface
        '-----------------------------------------------------------------------------------------------------------------

        If (LogicielInfo.ListeLangue.Count > 0) AndAlso (IndLangue >= 0) AndAlso (IndLangue < LogicielInfo.ListeLangue.Count) Then

            If LogicielOptions.lDebug Then
                FichierLangue = LogicielRep.Install & "\..\..\Langues\" & LogicielInfo.Racine & "_" &
                                LogicielInfo.ListeLangue(IndLangue).Substring(0, 2).ToUpper & EXTENSIONLANGUE

            Else
                FichierLangue = LogicielRep.Install & "\Langues\" & LogicielInfo.Racine & "_" &
                                LogicielInfo.ListeLangue(IndLangue).Substring(0, 2).ToUpper & EXTENSIONLANGUE

            End If

        Else
            FichierLangue = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' Mise à jour du nom du fichier langue Note de calcul
    ''' </summary>
    Public Sub InitialiseLNGFileName_NDC()

        If (LogicielInfo.ListeLangueNDC.Count > 0) AndAlso (LogicielOptions.IndLangueNDC >= 0) AndAlso (LogicielOptions.IndLangueNDC < LogicielInfo.ListeLangueNDC.Count) Then

            If lDebug Then
                LogicielFichiers.LangueNDC = LogicielRep.Install & "\..\..\Langues\" & LogicielInfo.Racine & "_NDC_" _
                                           & LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC).Substring(0, 2).ToUpper & EXTENSIONLANGUE
            Else
                LogicielFichiers.LangueNDC = LogicielRep.Install & "\Langues\" & LogicielInfo.Racine & "_NDC_" _
                                           & LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC).Substring(0, 2).ToUpper & EXTENSIONLANGUE
            End If

        Else
            LogicielFichiers.Langue = String.Empty
        End If

    End Sub

#End Region

    '#Region " Gestion Icones "

    '    ''' <summary>
    '    ''' Mise à jour du nom du fichier icones (à discuter)
    '    ''' </summary>
    '    Public Sub UpdateIconesFileName()


    '====================
    'Inutile et faux :: ce n'est pas un fichier mais un répertoire
    '                   il y a déjà un repertoire pour cela 'images'
    '====================

    '        If lDebug Then
    '            LogicielFichiers.Icone = LogicielRep.Install & "\..\..\Icones\"
    '        Else
    '            LogicielFichiers.Icone = LogicielRep.Install & "\Icones\"
    '        End If

    '    End Sub

    '#End Region

End Module
