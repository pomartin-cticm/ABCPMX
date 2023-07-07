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

        LogicielRep.RepertoireInstall = Application.StartupPath
        LogicielRep.RepertoireConfig = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version


        InitialiseDebug()

        '--> Répertoires

        If Not IO.Directory.Exists(LogicielRep.RepertoireConfig) Then 'R22-001
            IO.Directory.CreateDirectory(LogicielRep.RepertoireConfig)
        End If

        '--> Langues

        LogicielInfo.ListeLangue = {"English", "Français"}
        LogicielInfo.ListeLangueNDC = {"English", "Français"}

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

            '--> Fichiers récents
            LogicielFichiers.RecentFiles = New List(Of String)
            If My.Settings.RecentFiles IsNot Nothing Then
                For i = 0 To My.Settings.RecentFiles.Count - 1
                    LogicielFichiers.RecentFiles.Add(My.Settings.RecentFiles(i))
                Next
            End If

            '--> Fichiers
            LogicielFichiers.Base_Sections = LogicielRep.RepertoireConfig & "\" & RacProfile & ExtensionBase
            LogicielFichiers.Base_Aciers = LogicielRep.RepertoireConfig & "\" & RacAcier & ExtensionBase
            LogicielFichiers.Base_Goujons = LogicielRep.RepertoireConfig & "\" & LogicielInfo.Racine & "_" & RacGoujons & ExtensionBase
            LogicielFichiers.Base_Bacs = LogicielRep.RepertoireConfig & "\" & LogicielInfo.Racine & "_" & RacBacs & ExtensionBase

            '--> Base de données
            InitialisationBasesDonnees()
            '--> Récupération des données de la database dans le catalogue (aciers et profilés)
            InitialiseCatalogue(LogicielFichiers.Base_Sections, MyCatalogue)
            InitialiseBaseAciers(LogicielFichiers.Base_Aciers, SteelBase)

            '--> Bacs acier
            LireBaseBacs(BaseBacs)

            '--> Gamma coefficients partiels

            LogicielOptions.Gamma = New Cls_Gamma

            LogicielOptions.Gamma.GammaM0 = My.Settings.GammaM0
            LogicielOptions.Gamma.GammaM1 = My.Settings.GammaM1
            LogicielOptions.Gamma.GammaM2 = My.Settings.GammaM2

            LogicielOptions.Gamma.GammaC = My.Settings.GammaC
            LogicielOptions.Gamma.GammaV = My.Settings.GammaV
            LogicielOptions.Gamma.GammaVs = My.Settings.GammaVs
            LogicielOptions.Gamma.GammaVp = My.Settings.GammaVp
            LogicielOptions.Gamma.lGammaV_unique = My.Settings.lGammaV_unique
            LogicielOptions.Gamma.GammaS = My.Settings.GammaS
            LogicielOptions.Gamma.GammaP = My.Settings.GammaP

            LogicielOptions.Gamma.GammaM_fi = My.Settings.GammaM_fi
            LogicielOptions.Gamma.GammaC_fi = My.Settings.GammaC_fi
            LogicielOptions.Gamma.GammaV_fi = My.Settings.GammaV_fi

            LogicielOptions.Gamma.GammaG_sup = My.Settings.GammaG_sup
            LogicielOptions.Gamma.GammaG_inf = My.Settings.GammaG_inf
            LogicielOptions.Gamma.GammaQ = My.Settings.GammaQ

            LogicielOptions.Gamma.Psi0 = My.Settings.Psi0
            LogicielOptions.Gamma.Psi1 = My.Settings.Psi1
            LogicielOptions.Gamma.Psi2 = My.Settings.Psi2


        Catch ex As Exception

        End Try

        LogicielOptions.lFenetres = True

        '--> MAJ des noms de fichiers langue

        UpdateLNGFileName()
        UpdateLNGFileName_NDC()

        '--> MAJ du nom fichier icones

        UpdateIconesFileName()

    End Sub

    <Conditional("DEBUG")> Private Sub InitialiseDebug()
        lDebug = True
        LogicielOptions.lDebug = True
    End Sub

    Public Sub InitialisationBasesDonnees()

        Dim FichierSource As String

        '--> Options de la base de données
        OptionsDatabase.lNewBase = True
        OptionsDatabase.lSoftLimited = True
        OptionsDatabase.FiltreSoft = "ACB+"
        OptionsDatabase.lShowSteelAvailOnly = True
        OptionsDatabase.ChoiceSteel = EnuChoiceAcier.BaseIfNoStandardSteel
        OptionsDatabase.lNoSteelLowThick = True
        OptionsDatabase.lSaveConfig = False
        OptionsDatabase.lShowEC3 = True

        '--> Fichier pour les profilés

        If Not File.Exists(LogicielFichiers.Base_Sections) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.RepertoireInstall & "\..\..\" & RepBase & "\" & RacProfile & ExtensionBase
            Else
                FichierSource = LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacProfile & ExtensionBase
            End If
            ' File.Copy(LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacProfile & ExtensionBase, LogicielFichiers.Database_Section)
            File.Copy(FichierSource, LogicielFichiers.Base_Sections)

        End If

        '--> Fichier pour les aciers
        If Not File.Exists(LogicielFichiers.Base_Aciers) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.RepertoireInstall & "\..\..\" & RepBase & "\" & RacAcier & ExtensionBase
            Else
                FichierSource = LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacAcier & ExtensionBase
            End If
            'File.Copy(LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacAcier & ExtensionBase, LogicielFichiers.Database_Aciers)
            File.Copy(FichierSource, LogicielFichiers.Base_Aciers)
        End If

        '--> Fichiers pour les bacs
        If Not File.Exists(LogicielFichiers.Base_Bacs) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.RepertoireInstall & "\..\..\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacBacs & ExtensionBase
            Else
                FichierSource = LogicielRep.RepertoireInstall & "\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacBacs & ExtensionBase
            End If
            File.Copy(FichierSource, LogicielFichiers.Base_Bacs)
        End If

        '--> Fichiers pour les goujons soudés
        If Not File.Exists(LogicielFichiers.Base_Goujons) Then

            If LogicielOptions.lDebug Then
                FichierSource = LogicielRep.RepertoireInstall & "\..\..\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacGoujons & ExtensionBase
            Else
                FichierSource = LogicielRep.RepertoireInstall & "\" & RepBase & "\" & LogicielInfo.Racine & "_" & RacGoujons & ExtensionBase
            End If
            File.Copy(FichierSource, LogicielFichiers.Base_Goujons)

        End If

    End Sub

#End Region

#Region " Initialisation de la poutre "

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

        If MyPoutre.Section.lLamine Then
            TransfertProfileDeBase(MyPoutre.Section.ProfilA, MyPoutre.Section.ProfilA.Gamme, MyPoutre.Section.ProfilA.NomProfile, lOK)
            AssocieAcierCompatible(MyPoutre, LogicielFichiers.Base_Aciers, LogicielFichiers.Base_Sections, lTrouve)
        Else
        End If


    End Sub

    Public Sub InitialiseBacDeBase(ByRef MyBac As Cls_Bac, ByRef lTrouve As Boolean)
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

                Dim kvp As KeyValuePair(Of String, Cls_Bac) = BaseBacs.First

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
        MyProfile.b_fs = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MyProfile.b_fi = MyCatalogue.Series(Gamme).Profiles(Profile).Bf
        MyProfile.t_fs = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MyProfile.t_fi = MyCatalogue.Series(Gamme).Profiles(Profile).Tf
        MyProfile.t_w = MyCatalogue.Series(Gamme).Profiles(Profile).Tw
        MyProfile.r_ci = MyCatalogue.Series(Gamme).Profiles(Profile).Rc
        MyProfile.r_cs = MyCatalogue.Series(Gamme).Profiles(Profile).Rc

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
        Dim EpMax As Decimal = Math.Max(MyPoutre.Section.ProfilA.t_fs, MyPoutre.Section.ProfilA.t_w)

        ExtraireAciersCompatibles(EpMax, MyPoutre.Section.ProfilA.IndStandart, MySteels)

        AnalyseAciersListe(MySteels, True, Cls_Acier.NUANCEDEFAULT, lTrouve, iAcier)

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
    Public Sub UpdateLNGFileName()

        If (LogicielInfo.ListeLangue.Count > 0) AndAlso (LogicielOptions.IndLangue >= 0) AndAlso (LogicielOptions.IndLangue < LogicielInfo.ListeLangue.Count) Then

            If lDebug Then
                LogicielFichiers.Langue = LogicielRep.RepertoireInstall & "\..\..\Langues\" & LogicielInfo.Racine & "_" &
                                          LogicielInfo.ListeLangue(LogicielOptions.IndLangue).Substring(0, 2).ToUpper & ".lng"

            Else
                LogicielFichiers.Langue = LogicielRep.RepertoireInstall & "\Langues\" & LogicielInfo.Racine & "_" &
                                          LogicielInfo.ListeLangue(LogicielOptions.IndLangue).Substring(0, 2).ToUpper & ".lng"

            End If

        Else
            LogicielFichiers.Langue = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' Mise à jour du nom du fichier langue Note de calcul
    ''' </summary>
    Public Sub UpdateLNGFileName_NDC()

        If (LogicielInfo.ListeLangueNDC.Count > 0) AndAlso (LogicielOptions.IndLangueNDC >= 0) AndAlso (LogicielOptions.IndLangueNDC < LogicielInfo.ListeLangueNDC.Count) Then

            If lDebug Then
                LogicielFichiers.LangueNDC = LogicielRep.RepertoireInstall & "\..\..\Langues\" & LogicielInfo.Racine & "_" _
                                           & LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC).Substring(0, 2).ToUpper & ".lng"
            Else
                LogicielFichiers.LangueNDC = LogicielRep.RepertoireInstall & "\Langues\" & LogicielInfo.Racine & "_" _
                                           & LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC).Substring(0, 2).ToUpper & ".lng"
            End If

        Else
            LogicielFichiers.Langue = String.Empty
        End If

    End Sub

#End Region

#Region "Gestion Icones"

    ''' <summary>
    ''' Mise à jour du nom du fichier icones (à discuter)
    ''' </summary>
    Public Sub UpdateIconesFileName()


        If lDebug Then
            LogicielFichiers.Icone = LogicielRep.RepertoireInstall & "\..\..\Icones\"

        Else
            LogicielFichiers.Langue = LogicielRep.RepertoireInstall & "\Icones\"

        End If

    End Sub

#End Region

End Module
