Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXInterface
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_CasTestsPoutreMixte

    <TestMethod()> Public Sub Test_RCM_2018_2()

        'Cas test issu de la revue RCM (2008-2):
        '
        '
        '"Calcul d'une poutre mixte sur appuis simples suivant l'EN 1994-1-1"

#Region "Initialisation du logiciel"

        'Note GUD: Partie reprise du module de demarrage car une partie des calculs nécessite d'avoir chargé les BDD, ce qui est fait à l'initialisation du logiciel 

        '--> Récupération des informations générales du logociel - Non modifiable par l'utilisateur

        LogicielInfo.NomLogiciel = "ABCPMX-II"
        LogicielInfo.Version = "1.0"
        LogicielInfo.AnneeVersion = "2024"
        LogicielInfo.MailSupport = "support.logiciels@cticm.com"
        LogicielInfo.Extension = "pmx"
        LogicielInfo.Racine = "ABCPMX"

        LogicielInfo.Maitre = EnuMaitre.CTICM
        LogicielOptions.lNoS235 = (LogicielInfo.Maitre = EnuMaitre.ArcelorMittal)
        LogicielOptions.lDebug = False

        LogicielRep.Config = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version

        LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Gamma
        LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General

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

        LogicielOptions.Gamma.GammaM0 = 1
        LogicielOptions.Gamma.GammaM1 = 1
        LogicielOptions.Gamma.GammaM2 = 1.25

        LogicielOptions.Gamma.GammaC = 1.5
        LogicielOptions.Gamma.GammaVs = 1.25
        LogicielOptions.Gamma.GammaVc = 1.25
        LogicielOptions.Gamma.lGammaV_unique = True
        LogicielOptions.Gamma.GammaS = 1.15
        LogicielOptions.Gamma.GammaP = 1

        LogicielOptions.Gamma.GammaM_fi = 1
        LogicielOptions.Gamma.GammaC_fi = 1
        LogicielOptions.Gamma.GammaV_fi = 1

        LogicielOptions.Gamma.GammaG_sup = 1.35
        LogicielOptions.Gamma.GammaG_inf = 1
        LogicielOptions.Gamma.GammaQ = 1.5

        LogicielOptions.Gamma.Psi0_Q1 = 0.7
        LogicielOptions.Gamma.Psi1_Q1 = 0.5
        LogicielOptions.Gamma.Psi2_Q1 = 0.3

        LogicielOptions.Gamma.Psi0_Q2 = 0.7
        LogicielOptions.Gamma.Psi1_Q2 = 0.5
        LogicielOptions.Gamma.Psi2_Q2 = 0.3

        '--> Options du domaine d'application et options de calcul

        InitialiseOptionsScope()
        InitialiseOptionsCalcul()

#End Region

#Region "Initialisation de la poutre"

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "", LogicielOptions, OptionsCalcul)
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

        Dim lOK, lTrouve As Boolean
        InitialisePoutreDeBases(myPoutre, lOK)
        InitialiseBacDeBase(myPoutre.Dalle.Bac, lTrouve)
        InitialiseGoujonDeBase(myPoutre.Dalle.Connecteur, lTrouve)

        InitialiseDalleDefault(myPoutre.Dalle, cls_ProfilA.Enum_TypeSectionAcier.Lamine)

        myPoutre.Initialise_CoefficientsCombinaisons()
        myPoutre.InitialisePoidsPropres()

#End Region

#Region "Renseignement des données de l'article"

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) = 14 '14 m
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 3
        myPoutre.EntraxeD2 = 3

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            .ha = 450 / 1000
            .Tw = 9.4 / 1000
            .Bfs = 190 / 1000
            .Bfi = .Bfs
            .Tfs = 14.6 / 1000
            .Tfi = .Tfs
            .Rcs = 21 / 1000
            .Rci = .Rcs
            .InitialiseProprietes()
        End With

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 120 / 1000
        End With

        For i As Integer = 0 To myPoutre.Dalle.LitArma.Count - 1 'on ne prend pas en compte les armatures dans le calcul dans l'exemple traité 
            myPoutre.Dalle.LitArma(i).lActive = False
        Next

        With myPoutre.Dalle.Bac
            .Hp = 58 / 1000
            .h_rs = 0
            .Tp = 0.75 / 1000
            .Bb = 62 / 1000
            .Bt = 101 / 1000
            .Ep = 207 / 1000
            .Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
            .AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus 'permet de prendre en compte le bac pour le calcul des armatures transversales
        End With

        With myPoutre.Dalle.Connecteur
            .hsc = 100 / 1000
            .d = 19 / 1000
        End With

        myPoutre.NombreZones(myPoutre.IndicePremiereTravee) = 1
        myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.Espacement_Bac_TransZone(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.EspacementZone(myPoutre.IndicePremiereTravee, 0) = 0.207
        myPoutre.LongueurZone(myPoutre.IndicePremiereTravee, 0) = myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee)
        myPoutre.lAutomaticDesign = False

        'MATERIAUX
        With myPoutre.Section.Acier
            .Nuance = "S275"
            .Qualite = "EC3"
            .NormeProduit = "EN 1993-1-1"
            .Reduction = "Table 3.1"

            .Plages.Clear()
            Dim MyPlage As cls_Acier.strucPlage
            For i As Integer = 0 To SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages.Count - 1
                MyPlage.Ep = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Ep
                MyPlage.Fy = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fy
                MyPlage.Fu = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fu
                .Plages.Add(MyPlage)
            Next
        End With

        With myPoutre.Dalle.beton
            .Classe = "C25/30"
            .Ecm = 31000
        End With

        myPoutre.Dalle.Connecteur.Fu = 450

        myPoutre.Dalle.Bac.msurf = 8.53 '8.53 kg/m2
        myPoutre.Dalle.Bac.fyp = 350

        'CHARGES
        myPoutre.InitialisePoidsPropres()
        myPoutre.ChargesU("G2").QSurf(myPoutre.IndicePremiereTravee) = 1.4 * 1000
        myPoutre.ChargesU("Q1").QSurf(myPoutre.IndicePremiereTravee) = 2.5 * 1000
        myPoutre.ChargesU("QC").QSurf(myPoutre.IndicePremiereTravee) = 0.5 * 1000
        myPoutre.ChargesU("QC").FReparties(myPoutre.IndicePremiereTravee).Add(New cls_ForceRepartie(14 / 2 - 3 / 2, 1 * 3 * 1000, 14 / 2 + 3 / 2, 1 * 3 * 1000, 0)) '1 kN/m2 répartie s/ 3mx3m et centré à mi-travée

        'COEFFICIENTS PARTIELS
        myPoutre.Initialise_CoefficientsCombinaisons() 'Initialise les coefficients par défaut 
        myPoutre.lCombELU(0) = True 'activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myPoutre.lCombELS(0) = True 'activation de la première combinaison ELS par défaut (G + Q)
        myPoutre.lCombELCURules(0) = True 'activation de la première combinaison ELU pendant la phase de construction activée 
        myPoutre.lCombELCSRules(0) = True 'activation de la première combinaison ELS pendant la phase de construction activée 

        With myPoutre.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

#End Region

#Region "Lancement des calculs"


        Dim NomChargesA(), strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        ReDim NomChargesA(9)
        NomChargesA(0) = "Permanent loads"
        NomChargesA(1) = "Self-weight"
        NomChargesA(2) = "Self weight with props"
        NomChargesA(3) = "Self weight without props"
        NomChargesA(4) = "Other permanent loads"
        NomChargesA(5) = "Live loads"
        NomChargesA(6) = "Conf. no"
        NomChargesA(7) = "Shrinkage of the slab"
        NomChargesA(8) = "Shrinkage of the encasement"
        NomChargesA(9) = "Construction loads"

        strRacineELU = "ULS"
        strRacineELS = "SLS"
        strRacineELF = "FLS"
        strRacineELUC = "ULS_C"
        strRacineELSC = "SLS_C"

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myPoutre.VerifAcier(0)
                myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myPoutre.VerifMixte(0)
                myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myPoutre.VerifAcier(0)
                    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myPoutre.InitialiseCalculs(NomChargesA)
        myPoutre.AAA_CalculMNVInternesN()
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELU, myPoutre.lCombELU, myPoutre.CoefCombELU, strRacineELU, myPoutre.CombiA_ELU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELS, myPoutre.lCombELS, myPoutre.CoefCombELS, strRacineELS, myPoutre.CombiA_ELS)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombFeu, myPoutre.lCombFeu, myPoutre.CoefCombFeu, strRacineELF, myPoutre.CombiA_ELF)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myPoutre.lCombELCURules, myPoutre.CoefCombELCU, strRacineELUC, myPoutre.CombiA_ELCU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myPoutre.lCombELCSRules, myPoutre.CoefCombELCS, strRacineELSC, myPoutre.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd(,) As Decimal = Nothing
        Dim MEdMax, MEdMin, iNodeMMin, iNodeMMax As Decimal

        Dim VEd(,) As Decimal = Nothing
        Dim VEdMax, VEdMin, iNodeVMin, iNodeVMax As Decimal

        myPoutre.CombiA_ELU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd, myPoutre.Nodes.nbNodes, MEdMax, MEdMin, iNodeMMax, iNodeMMin)
        EnveloppeTableauEfforts(VEd, myPoutre.Nodes.nbNodes, VEdMax, VEdMin, iNodeVMax, iNodeVMin)

        'COMBINAISON DES EFFORTS A L'ELU CONSTRUCTION
        Dim MEdConstruction(,) As Decimal = Nothing
        Dim MEdMaxConstruction, MEdMinConstruction, iNodeMMinConstruction, iNodeMMaxConstruction As Decimal

        Dim VEdConstruction(,) As Decimal = Nothing
        Dim VEdMaxConstruction, VEdMinConstruction, iNodeVMinConstruction, iNodeVMaxConstruction As Decimal

        myPoutre.CombiA_ELCU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEdConstruction, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELCU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEdConstruction, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEdConstruction, myPoutre.Nodes.nbNodes, MEdMaxConstruction, MEdMinConstruction, iNodeMMaxConstruction, iNodeMMinConstruction)
        EnveloppeTableauEfforts(VEdConstruction, myPoutre.Nodes.nbNodes, VEdMaxConstruction, VEdMinConstruction, iNodeVMaxConstruction, iNodeVMinConstruction)

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, True) 'Poutre seul durant la phase de construction
        myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre) 'Poutre mixte

#End Region

#Region "Verification de l'analyse de la poutre"

        'VERIFICATION DES EFFORTS A L'ELU

        Valeur = MEdMax
        ValRef = 654.395 * 10 ^ 3 'A NOTER: 655 kN.m est une valeur arrondie de l'article, une valeur plus proche (mais non exacte) serait 654.395 kN.m par exemple
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdMax
        ValRef = 187 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        'VERIFICATION DES EFFORTS A L'ELU CONSTRUCTION

        Valeur = MEdMaxConstruction
        ValRef = 337 * 10 ^ 3 'A NOTER: 655 kN.m est une valeur arrondie de l'article, une valeur plus proche (mais non exacte) serait 654.395 kN.m par exemple
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdMaxConstruction
        ValRef = 91 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la classification de la section (ELU)"

        Valeur = myPoutre.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la résistance des connecteurs (ELU)"

        Valeur = myPoutre.Dalle.Connecteur.PRdDallePleineG1G2Acier(myPoutre.Param.Gamma.GammaVs)
        ValRef = 81.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = myPoutre.Dalle.Connecteur.PRdDallePleineG1Beton(myPoutre.Dalle.beton.Fck, 31000, myPoutre.Param.Gamma.GammaVc)
        ValRef = 73.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Calcul avec 1 connecteur par onde 
        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine, 'VALEUR CORRIGEE avec Ecm = 31 GPA
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0), myPoutre.Dalle.beton.Fck,
                                                         31000, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 52.5 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine, 'VALEUR CORRIGEE avec Ecm = 31 GPA
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0), myPoutre.Dalle.beton.Fck,
                                                         myPoutre.Dalle.beton.Ecm, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 52.897 * 1000 'VALEUR CALCULEE à la main avec le vrai Ecm = 31.476 GPa
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Calcul avec 2 connecteurs par ondes
        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine,
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         2, myPoutre.Dalle.beton.Fck,
                                                         31000, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 37.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification du degré de connection minimal (ELU)"

        Valeur = myPoutre.VerifMixte(0).DegConnexMin(myPoutre.IndicePremiereTravee)
        ValRef = 0.574
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification du dimensionnement de la connection (ELU)"


        Valeur = myPoutre.Section.ResistanceTractionProfile(myPoutre.Param.Gamma.GammaM0)
        ValRef = 2718 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification de la valeur de Na,Rd

        Dim Beff As Decimal
        Dim lSimple As Boolean = True 'booléen qui indique qu'on va utiliser le modèle simplifié pour le calcul de beff
        Beff = myPoutre.BeffDalle(myPoutre.Nodes.xTravee(iNodeMMax), myPoutre.IndicePremiereTravee, lSimple, False)
        Valeur = myPoutre.Dalle.NResistanceCompressionDalle(Beff, myPoutre.Param.Gamma.GammaC)
        ValRef = 2635 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification de la valeur de Nc,Rd à mi travée

        Valeur = myPoutre.VerifMixte(0).DegConnex(myPoutre.IndicePremiereTravee, 0)
        ValRef = 1 * (7 / 0.207) * 52.516 * 1000 / (2636 * 1000) '= 0.674

        '/!\ J'ai corrigé la valeur de l'article car la valeur de PRd n'est pas exactement la même du fait que la valeur de Ecm n'est pas identique
        '   (31 GPa dans l'article est directement calculée dans le logiciel) + la valeur du nombre de connecteurs n'est pas identique non plus (arrondi au premier entier inférieur dans 
        ' l'article et on garde la valeur décimale dans le logiciel). Au final, on a un eta = 0.658 dans l'article et 0.674 avec le logiciel
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul du degré de connection 

#End Region

#Region "Vérification de la résistance à la flexion (ELU)"

        'A L'ELU

        Dim zANE, MRk As Decimal
        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, 0, zANE, MRk)

        Valeur = MRk
        ValRef = 468.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifMixte(0).CritereM.Resistance(iNodeMMax)
        ValRef = 779.4 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (779.4 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 3 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereM.CritereMax
        ValRef = 0.836 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

        'A L'ELU CONSTRUCTION

        Valeur = myPoutre.VerifAcier(0).CritereM.Resistance(iNodeMMaxConstruction)
        ValRef = 468.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

        Valeur = myPoutre.VerifAcier(0).CritereM.CritereMax
        ValRef = 0.721
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region "Vérification de la résistance à l'effort tranchant (ELU)"

        'A L'ELU

        Valeur = myPoutre.Section.VplRd(myPoutre.Param.Gamma.GammaM0)
        ValRef = 807 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à l'effort tranchant

        Valeur = myPoutre.VerifMixte(0).CritereV.CritereMax
        ValRef = 0.232
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELU As Decimal
        If ValRef <= 0.5 Then
            rhoVELU = 0
        ElseIf ValRef >= 1 Then
            rhoVELU = 1
        Else
            rhoVELU = (2 * 0.232 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
        End If

        'A L'ELU CONSTRUCTION

        Valeur = myPoutre.VerifAcier(0).CritereV.CritereMax
        ValRef = 0.113
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELCU As Decimal
        If ValRef <= 0.5 Then
            rhoVELCU = 0
        ElseIf ValRef >= 1 Then
            rhoVELCU = 1
        Else
            rhoVELCU = (2 * 0.232 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
        End If

#End Region

#Region "Vérification de la résistance au voilement (ELU)"

        Assert.IsTrue(myPoutre.Section.IsVoilementParCisaillement(myPoutre.Param.EtaW) = False) '--> Vérification de la résistance au voilement non nécessaire 
#End Region

#Region "Verification de la résistance à l'interaction MV"

        '--> Sans objet, on doit retrouver les mêmes résultats que pour la résistance à la flexion simple


        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, rhoVELU, zANE, MRk)

        Valeur = MRk
        ValRef = 468.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifMixte(0).CritereMV.Resistance(iNodeMMaxConstruction)
        ValRef = 779.4 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (834.6 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 3 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereMV.CritereMax
        ValRef = 0.836 'GUD: valeur recalculée pour les mêmes raisons que ci-dessus. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region " Vérification du dimensionnement des armatures transversales (ELU)"

        ' myPoutre.VerifMixte(0).CalculArmaturesTransversales()

        ''--> TauEd

        Valeur = 2.06 'Flux de cisaillement max transmis par la dalle de part et d'autre de la poutrelle (VALEUR RECALCULEE avec le vrai PRd = 52.897 kN et non 52.5 kN. Dans l'article, on a tauEd = 2.04 MPa)
        ValRef = myPoutre.VerifMixte(0).TauEd(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la contrainte tangentielle 

        ''--> Thetaf

        Valeur = 0.5 * Math.Asin(2 * 2.06 / (0.54 * 16.7)) ' 13.59° -> VALEUR RECALCULEE car dans l'article on considère conservativement theta = 45°
        Valeur = Math.Max(Valeur, 27 * Math.PI / 180) 'Borne inférieure
        Valeur = Math.Min(Valeur, 45 * Math.PI / 180) 'Borne supérieure
        ValRef = myPoutre.VerifMixte(0).Thetaf(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'angle de la bielle

        ''--> As,trans

        Valeur = 0 'le bac seul suffit à reprendre ces efforts
        ValRef = myPoutre.VerifMixte(0).As_s_transv(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul des armatures transversales

#End Region

#Region "Vérification des propriétés élastiques (ELS)"

        '--> Coefficient d'équivalence à court terme n0

        Dim n0 As Decimal = 210 / 31.476 '= 6.6717
        Valeur = n0 'calcul manuel car l'article n0 = 210/31
        ValRef = myPoutre.Dalle.beton.CoefficientEquivalenceCT()
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Vérification des calculs des coefficients d'équivalences à LT (50 ans)
        Dim ageT As Integer = 50 * 365 '50 ans, en jours
        Dim ageT0 As Integer = 28
        Dim h0 As Decimal = 2 * 62 / 1000
        Dim PHIrh As Decimal = 1 + (1 - 50 / 100) / (0.1 * (h0 * 1000) ^ (1 / 3))
        Dim betaFcm As Decimal = 16.8 / Math.Sqrt(25 + 8)
        Dim betaT0 As Decimal = 1 / (0.1 + ageT0 ^ 0.2)
        Dim phi0 As Decimal = PHIrh * betaFcm * betaT0
        Dim betaH As Decimal = Math.Min(1.5 * (1 + (0.012 * 50 / 100) ^ 18) * (h0 * 1000) + 250, 1500)
        Dim betaCTT0 As Decimal = ((ageT - ageT0) / (betaH + ageT - ageT0)) ^ 0.3
        Dim phiTT0 As Decimal = phi0 * betaCTT0

        '--> h0

        Valeur = h0
        ValRef = myPoutre.Dalle.NotionalSizeH0(myPoutre.Section.ProfilA.Bfs)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Phi RH

        Valeur = PHIrh
        ValRef = myPoutre.Dalle.beton.PhiRH(myPoutre.Param.RH, h0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '-->Beta fcm

        Valeur = betaFcm
        ValRef = myPoutre.Dalle.beton.BetaFcm()
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Beta t0

        Valeur = betaT0
        ValRef = myPoutre.Dalle.beton.Beta_t0(ageT0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> phi0 n'est pas évalué par une fonction à part entière

        '--> BetaH

        Valeur = betaH
        ValRef = myPoutre.Dalle.beton.BetaH(myPoutre.Param.RH, h0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> BetaC(t,t0)

        Valeur = betaCTT0
        ValRef = myPoutre.Dalle.beton.BetaC_tt0(myPoutre.Param.RH, h0, ageT, ageT0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> phi(t,t0) n'est pas évalué par une fonction à part entière

        '--> Coefficient d'équivalence LT CP nL

        Valeur = n0 * (1 + 1.1 * phiTT0) '27.5183638
        ValRef = myPoutre.Elements(0).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE nL

        Valeur = n0 '6.67
        ValRef = myPoutre.Elements(2).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE SH

        ageT0 = 1
        h0 = 2 * myPoutre.Dalle.EpaisseurActive
        PHIrh = 1 + (1 - 50 / 100) / (0.1 * (h0 * 1000) ^ (1 / 3))
        betaFcm = 16.8 / Math.Sqrt(25 + 8)
        betaT0 = 1 / (0.1 + ageT0 ^ 0.2)
        phi0 = PHIrh * betaFcm * betaT0
        betaH = Math.Min(1.5 * (1 + (0.012 * 50 / 100) ^ 18) * (h0 * 1000) + 250, 1500)
        betaCTT0 = ((ageT - ageT0) / (betaH + ageT - ageT0)) ^ 0.3
        phiTT0 = phi0 * betaCTT0

        Valeur = n0 * (1 + 0.55 * phiTT0) '26.0715
        ValRef = myPoutre.Elements(4).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Propriétés en phase de coulage, poutre non etayée

        Valeur = 33740 '33 740 cm4
        ValRef = myPoutre.Section.ProfilA.InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Dim InertieY, Mel As Decimal

        myPoutre.Section.ProprietesElastiquesAcierMyy(1, myPoutre.Param.Gamma, zANE, InertieY, MRk)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, 3 * n0, 0, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase mixte pour les actions court termes

        Valeur = 106266

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, 6.77, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = (120 - 114)
        ValRef = zANE * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase mixte pour les actions long termes

        Valeur = 80885

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, 20.3, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = (120 - 194)
        ValRef = zANE * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

#End Region

#Region "Vérification du calcul des fleches (ELS)"

        '--> Fleches due à G1

        Valeur = myPoutre.ChargesA(0).FlecheMax * 1000
        ValRef = 51.2
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à G2

        Valeur = myPoutre.ChargesA(1).FlecheMax * 1000
        'myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(0).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = 5 * 4.2 * 1000 * 14 ^ 4 / (384 * 210000 * 10 ^ 6 * 73534 * 10 ^ (-8)) * 1000 '13.6 mm
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q

        Valeur = myPoutre.ChargesA(2).FlecheMax * 1000
        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(2).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = 5 * 7.5 * 1000 * 14 ^ 4 / (384 * 210000 * 10 ^ 6 * 106571 * 10 ^ (-8)) * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleche due au retrait 

        Valeur = myPoutre.ChargesA(5).FlecheMax * 1000
        Dim NR, deltazG, Mr, deltaR As Decimal
        NR = 325 * 10 ^ (-6) * myPoutre.Section.Acier.EYoung / myPoutre.Elements(4).nEqDalle * Beff * myPoutre.Dalle.EpaisseurActive * 10 ^ 6 'N

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(4).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        deltazG = myPoutre.Dalle.Bac.Hp + myPoutre.Dalle.EpaisseurActive / 2 - zANE
        Mr = NR * deltazG
        deltaR = (Mr * myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) ^ 2) / (8 * myPoutre.Section.Acier.EYoung * 10 ^ 6 * InertieY)
        ValRef = deltaR * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

#Region "Fréquence propre (ELS)"

        myPoutre.Modal.Analyse(myPoutre, 0.2, myPoutre.Hivoss.IndexQ)
        Valeur = myPoutre.Modal.Frequence

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(2).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = Math.PI / 2 * Math.Sqrt(210 * InertieY * 10 ^ 8 * 9.81 / (1300 * 14 ^ 4))
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.Modal.MassTotal
        ValRef = 18500
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

    End Sub

    <TestMethod()> Public Sub Test_RCM_2022_3()

        'Cas test issu de la revue RCM (2022-3):
        '
        '
        '"Résistance au déversement d'une solive de plancher en phase de construction"

#Region "Initialisation du logiciel"

        'Note GUD: Partie reprise du module de demarrage car une partie des calculs nécessite d'avoir chargé les BDD, ce qui est fait à l'initialisation du logiciel 

        '--> Récupération des informations générales du logociel - Non modifiable par l'utilisateur

        LogicielInfo.NomLogiciel = "ABCPMX-II"
        LogicielInfo.Version = "1.0"
        LogicielInfo.AnneeVersion = "2024"
        LogicielInfo.MailSupport = "support.logiciels@cticm.com"
        LogicielInfo.Extension = "pmx"
        LogicielInfo.Racine = "ABCPMX"

        LogicielInfo.Maitre = EnuMaitre.CTICM
        LogicielOptions.lNoS235 = (LogicielInfo.Maitre = EnuMaitre.ArcelorMittal)
        LogicielOptions.lDebug = False

        LogicielRep.Config = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version

        LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Gamma
        LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General

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

        LogicielOptions.Gamma.GammaM0 = 1
        LogicielOptions.Gamma.GammaM1 = 1
        LogicielOptions.Gamma.GammaM2 = 1.25

        LogicielOptions.Gamma.GammaC = 1.5
        LogicielOptions.Gamma.GammaVs = 1.25
        LogicielOptions.Gamma.GammaVc = 1.25
        LogicielOptions.Gamma.lGammaV_unique = True
        LogicielOptions.Gamma.GammaS = 1.15
        LogicielOptions.Gamma.GammaP = 1

        LogicielOptions.Gamma.GammaM_fi = 1
        LogicielOptions.Gamma.GammaC_fi = 1
        LogicielOptions.Gamma.GammaV_fi = 1

        LogicielOptions.Gamma.GammaG_sup = 1.35
        LogicielOptions.Gamma.GammaG_inf = 1
        LogicielOptions.Gamma.GammaQ = 1.5

        LogicielOptions.Gamma.Psi0_Q1 = 0.7
        LogicielOptions.Gamma.Psi1_Q1 = 0.5
        LogicielOptions.Gamma.Psi2_Q1 = 0.3

        LogicielOptions.Gamma.Psi0_Q2 = 0.7
        LogicielOptions.Gamma.Psi1_Q2 = 0.5
        LogicielOptions.Gamma.Psi2_Q2 = 0.3

        '--> Options du domaine d'application et options de calcul

        InitialiseOptionsScope()
        InitialiseOptionsCalcul()

#End Region

#Region "Initialisation de la poutre"

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "", LogicielOptions, OptionsCalcul)
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

        Dim lOK, lTrouve As Boolean
        InitialisePoutreDeBases(myPoutre, lOK)
        InitialiseBacDeBase(myPoutre.Dalle.Bac, lTrouve)
        InitialiseGoujonDeBase(myPoutre.Dalle.Connecteur, lTrouve)

        InitialiseDalleDefault(myPoutre.Dalle, cls_ProfilA.Enum_TypeSectionAcier.Lamine)

        myPoutre.Initialise_CoefficientsCombinaisons()
        myPoutre.InitialisePoidsPropres()

#End Region

#Region "Renseignement des données de l'article"

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) = 14 '14 m
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 3
        myPoutre.EntraxeD2 = 3

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            .ha = 450 / 1000
            .Tw = 9.4 / 1000
            .Bfs = 190 / 1000
            .Bfi = .Bfs
            .Tfs = 14.6 / 1000
            .Tfi = .Tfs
            .Rcs = 21 / 1000
            .Rci = .Rcs
            .InitialiseProprietes()
        End With

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 120 / 1000
        End With

        For i As Integer = 0 To myPoutre.Dalle.LitArma.Count - 1 'on ne prend pas en compte les armatures dans le calcul dans l'exemple traité 
            myPoutre.Dalle.LitArma(i).lActive = False
        Next

        With myPoutre.Dalle.Bac
            .Hp = 58 / 1000
            .h_rs = 0
            .Tp = 0.75 / 1000
            .Bb = 62 / 1000
            .Bt = 101 / 1000
            .Ep = 207 / 1000
            .Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
            .AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus 'permet de prendre en compte le bac pour le calcul des armatures transversales
            .Ieff = 44.37 * 10 ^ (-8)
        End With

        With myPoutre.Dalle.Connecteur
            .hsc = 100 / 1000
            .d = 19 / 1000
        End With

        myPoutre.NombreZones(myPoutre.IndicePremiereTravee) = 1
        myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.Espacement_Bac_TransZone(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.EspacementZone(myPoutre.IndicePremiereTravee, 0) = 0.207
        myPoutre.lAutomaticDesign = False

        'MATERIAUX
        With myPoutre.Section.Acier
            .Nuance = "S275"
            .Qualite = "EC3"
            .NormeProduit = "EN 1993-1-1"
            .Reduction = "Table 3.1"

            .Plages.Clear()
            Dim MyPlage As cls_Acier.strucPlage
            For i As Integer = 0 To SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages.Count - 1
                MyPlage.Ep = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Ep
                MyPlage.Fy = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fy
                MyPlage.Fu = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fu
                .Plages.Add(MyPlage)
            Next
        End With

        With myPoutre.Dalle.beton
            .Classe = "C25/30"
            .Ecm = 31000
        End With

        myPoutre.Dalle.Connecteur.Fu = 450

        myPoutre.Dalle.Bac.msurf = 8.53 '8.53 kg/m2
        myPoutre.Dalle.Bac.fyp = 350

        'CHARGES
        myPoutre.InitialisePoidsPropres()
        'myPoutre.ChargesU("G2").QSurf(myPoutre.IndicePremiereTravee) = 1.4 * 1000
        'myPoutre.ChargesU("Q1").QSurf(myPoutre.IndicePremiereTravee) = 2.5 * 1000
        myPoutre.ChargesU("QC").QSurf(myPoutre.IndicePremiereTravee) = 0.5 * 1000
        myPoutre.ChargesU("QC").FReparties(myPoutre.IndicePremiereTravee).Add(New cls_ForceRepartie(14 / 2 - 3 / 2, 1 * 3 * 1000, 14 / 2 + 3 / 2, 1 * 3 * 1000, 0)) '1 kN/m2 répartie s/ 3mx3m et centré à mi-travée

        'COEFFICIENTS PARTIELS
        myPoutre.Initialise_CoefficientsCombinaisons() 'Initialise les coefficients par défaut 
        myPoutre.lCombELU(0) = True 'activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myPoutre.lCombELS(0) = True 'activation de la première combinaison ELS par défaut (G + Q)
        myPoutre.lCombELCURules(0) = True 'activation de la première combinaison ELU pendant la phase de construction activée 
        myPoutre.lCombELCSRules(0) = True 'activation de la première combinaison ELS pendant la phase de construction activée 

        With myPoutre.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

#End Region

#Region "Lancement des calculs"


        Dim NomChargesA(), strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        ReDim NomChargesA(9)
        NomChargesA(0) = "Permanent loads"
        NomChargesA(1) = "Self-weight"
        NomChargesA(2) = "Self weight with props"
        NomChargesA(3) = "Self weight without props"
        NomChargesA(4) = "Other permanent loads"
        NomChargesA(5) = "Live loads"
        NomChargesA(6) = "Conf. no"
        NomChargesA(7) = "Shrinkage of the slab"
        NomChargesA(8) = "Shrinkage of the encasement"
        NomChargesA(9) = "Construction loads"

        strRacineELU = "ULS"
        strRacineELS = "SLS"
        strRacineELF = "FLS"
        strRacineELUC = "ULS_C"
        strRacineELSC = "SLS_C"

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myPoutre.VerifAcier(0)
                myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myPoutre.VerifMixte(0)
                myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myPoutre.VerifAcier(0)
                    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myPoutre.InitialiseCalculs(NomChargesA)
        myPoutre.AAA_CalculMNVInternesN()
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELU, myPoutre.lCombELU, myPoutre.CoefCombELU, strRacineELU, myPoutre.CombiA_ELU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELS, myPoutre.lCombELS, myPoutre.CoefCombELS, strRacineELS, myPoutre.CombiA_ELS)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombFeu, myPoutre.lCombFeu, myPoutre.CoefCombFeu, strRacineELF, myPoutre.CombiA_ELF)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myPoutre.lCombELCURules, myPoutre.CoefCombELCU, strRacineELUC, myPoutre.CombiA_ELCU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myPoutre.lCombELCSRules, myPoutre.CoefCombELCS, strRacineELSC, myPoutre.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd(,) As Decimal = Nothing
        Dim MEdMax, MEdMin, iNodeMMin, iNodeMMax As Decimal

        Dim VEd(,) As Decimal = Nothing
        Dim VEdMax, VEdMin, iNodeVMin, iNodeVMax As Decimal

        myPoutre.CombiA_ELU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd, myPoutre.Nodes.nbNodes, MEdMax, MEdMin, iNodeMMax, iNodeMMin)
        EnveloppeTableauEfforts(VEd, myPoutre.Nodes.nbNodes, VEdMax, VEdMin, iNodeVMax, iNodeVMin)

        'COMBINAISON DES EFFORTS A L'ELU CONSTRUCTION
        Dim MEdConstruction(,) As Decimal = Nothing
        Dim MEdMaxConstruction, MEdMinConstruction, iNodeMMinConstruction, iNodeMMaxConstruction As Decimal

        Dim VEdConstruction(,) As Decimal = Nothing
        Dim VEdMaxConstruction, VEdMinConstruction, iNodeVMinConstruction, iNodeVMaxConstruction As Decimal

        myPoutre.CombiA_ELCU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEdConstruction, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELCU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEdConstruction, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEdConstruction, myPoutre.Nodes.nbNodes, MEdMaxConstruction, MEdMinConstruction, iNodeMMaxConstruction, iNodeMMinConstruction)
        EnveloppeTableauEfforts(VEdConstruction, myPoutre.Nodes.nbNodes, VEdMaxConstruction, VEdMinConstruction, iNodeVMaxConstruction, iNodeVMinConstruction)

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, True) 'Poutre seul durant la phase de construction
        'myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre) 'Poutre mixte

#End Region

#Region "Verification de l'analyse de la poutre"

        'VERIFICATION DES EFFORTS A L'ELU CONSTRUCTION

        Valeur = MEdMaxConstruction
        ValRef = 337 * 10 ^ 3 'A NOTER: 655 kN.m est une valeur arrondie de l'article, une valeur plus proche (mais non exacte) serait 654.395 kN.m par exemple
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la résistance au déversement SANS prise en compte du bac"

        Dim Mcr, MbRd As Decimal

        Mcr = 100 * 1000
        MbRd = 85.19 * 1000

        Valeur = myPoutre.VerifAcier(0).McrLTB(0, myPoutre.IndicePremiereTravee)
        ValRef = Mcr
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.VerifAcier(0).CritereLTB.Resistance(myPoutre.IndicePremiereTravee)
        ValRef = MbRd ' = 85.19 kN (dans l'article, on a 492.5 kN.m)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

#End Region

#Region "Vérification de la résistance au déversement AVEC prise en compte du bac"
        With myPoutre.MaintienBac
            .lMaintienBac = True
            .m = 2
            .nt = 2
            .Transition = cls_MaintienBac.Enu_Transition.Emboitement
            .FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes
            .FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.Pistolet
            .FixCoutureType = cls_MaintienBac.Enu_CoutureType.Vis
            .ec = 500 / 1000
            .lTheta = True
        End With

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, True) 'On relance les vérifications de la poutre acier avec les nouveaux paramètres

        Mcr = 3331 * 1000 'valeur recalculée avec LTBeamN et avec prise en compte de la charge locale à mi-travée
        MbRd = 437 * 1000

        Valeur = myPoutre.VerifAcier(0).McrLTB(0, myPoutre.IndicePremiereTravee)
        ValRef = Mcr
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.VerifAcier(0).CritereLTB.Resistance(myPoutre.IndicePremiereTravee)
        ValRef = MbRd ' = 85.19 kN (dans l'article, on a 492.5 kN.m)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

#End Region


    End Sub

    <TestMethod()> Public Sub Test_RCM_2023_3()

        'Cas test issu de la revue RCM (2023-3):
        '
        '
        '"Vérification d'une poutre mixte acier-béton enrobée pendant la phase de construction"

#Region "Initialisation du logiciel"

        'Note GUD: Partie reprise du module de demarrage car une partie des calculs nécessite d'avoir chargé les BDD, ce qui est fait à l'initialisation du logiciel 

        '--> Récupération des informations générales du logociel - Non modifiable par l'utilisateur

        LogicielInfo.NomLogiciel = "ABCPMX-II"
        LogicielInfo.Version = "1.0"
        LogicielInfo.AnneeVersion = "2024"
        LogicielInfo.MailSupport = "support.logiciels@cticm.com"
        LogicielInfo.Extension = "pmx"
        LogicielInfo.Racine = "ABCPMX"

        LogicielInfo.Maitre = EnuMaitre.CTICM
        LogicielOptions.lNoS235 = (LogicielInfo.Maitre = EnuMaitre.ArcelorMittal)
        LogicielOptions.lDebug = False

        LogicielRep.Config = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version

        LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Gamma
        LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General

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

        LogicielOptions.Gamma.GammaM0 = 1
        LogicielOptions.Gamma.GammaM1 = 1
        LogicielOptions.Gamma.GammaM2 = 1.25

        LogicielOptions.Gamma.GammaC = 1.5
        LogicielOptions.Gamma.GammaVs = 1.25
        LogicielOptions.Gamma.GammaVc = 1.25
        LogicielOptions.Gamma.lGammaV_unique = True
        LogicielOptions.Gamma.GammaS = 1.15
        LogicielOptions.Gamma.GammaP = 1

        LogicielOptions.Gamma.GammaM_fi = 1
        LogicielOptions.Gamma.GammaC_fi = 1
        LogicielOptions.Gamma.GammaV_fi = 1

        LogicielOptions.Gamma.GammaG_sup = 1.35
        LogicielOptions.Gamma.GammaG_inf = 1
        LogicielOptions.Gamma.GammaQ = 1.5

        LogicielOptions.Gamma.Psi0_Q1 = 0.7
        LogicielOptions.Gamma.Psi1_Q1 = 0.5
        LogicielOptions.Gamma.Psi2_Q1 = 0.3

        LogicielOptions.Gamma.Psi0_Q2 = 0.7
        LogicielOptions.Gamma.Psi1_Q2 = 0.5
        LogicielOptions.Gamma.Psi2_Q2 = 0.3

        '--> Options du domaine d'application et options de calcul

        InitialiseOptionsScope()
        InitialiseOptionsCalcul()

#End Region

#Region "Initialisation de la poutre"

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.AcierSeulEnrobage, "", LogicielOptions, OptionsCalcul)
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

        Dim lOK, lTrouve As Boolean
        InitialisePoutreDeBases(myPoutre, lOK)
        InitialiseBacDeBase(myPoutre.Dalle.Bac, lTrouve)
        InitialiseGoujonDeBase(myPoutre.Dalle.Connecteur, lTrouve)

        InitialiseDalleDefault(myPoutre.Dalle, cls_ProfilA.Enum_TypeSectionAcier.Lamine)

        myPoutre.Initialise_CoefficientsCombinaisons()
        myPoutre.InitialisePoidsPropres()

#End Region

#Region "Renseignement des données de l'article"

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) = 12.5 '12.5m
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 2.5
        myPoutre.EntraxeD2 = 2.5

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            .ha = 497 / 1000
            .Tw = 8.4 / 1000
            .Bfs = 200 / 1000
            .Bfi = .Bfs
            .Tfs = 14.5 / 1000
            .Tfi = .Tfs
            .Rcs = 21 / 1000
            .Rci = .Rcs
            .InitialiseProprietes()
        End With

        myPoutre.Section.typeSection = cls_Section.Enum_TypeSection.MixteEnrobage

        With myPoutre.Section.Enrobage
            .Beton.Classe = "C25/30"
            .Beton.Ecm = 31000

            .LitArma(2).PhiExt = 8 / 1000 'Lit supérieur
            .LitArma(2).NbExt = 1
            .LitArma(2).PhiMil = 0
            .LitArma(2).NbMil = 0
            .LitArma(2).PhiInt = 8 / 1000
            .LitArma(2).NbInt = 1
            .LitArma(2).zPosRatio = (14.5 + 40) / 497

            .LitArma(1).lActiveExt = False 'Lit intermédiaire
            .LitArma(1).lActiveInt = False

            .LitArma(0).PhiInt = 12 / 1000 'Lit inférieur
            .LitArma(0).NbInt = 1
            .LitArma(0).PhiMil = 0
            .LitArma(0).NbMil = 0
            .LitArma(0).PhiExt = 8 / 1000
            .LitArma(0).NbExt = 1
            .LitArma(0).zPosRatio = (497 - 14.5 - 60) / 497

            .Etriers_EnrobageY = 10 / 1000 'pas utile pour le calcul
            .Etriers_EnrobageZ = (60 - 12 / 2 - 6) / 1000 'dans l'article, les enrobages des lits sup et inf ne sont pas identiques.
            'Comme le lit sup est très probablement inactif, je renseigne l'enrobage inf

            .Ratio_bc = 1

            .Beton.RhoC = 25 / (9.81 * 10 ^ (-3)) 'Modification de la masse volumique du béton pour arrivée à une charge volumique de 25 kN/m3 (permet de retrouver les valeurs de l'article)
        End With

        myPoutre.Section.Enrobage.AcierArmatures.Classe = "B500"
        myPoutre.Section.Enrobage.AcierArmatures.MAJProprietes()

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 140 / 1000

            .beton.RhoC = 26 / (9.81 * 10 ^ (-3)) 'Modification de la masse volumique du béton pour arrivée à une charge volumique de 26 kN/m3 (permet de retrouver les valeurs de l'article)
        End With

        'For i As Integer = 0 To myPoutre.Dalle.LitArma.Count - 1 'on ne prend pas en compte les armatures dans le calcul dans l'exemple traité 
        '    myPoutre.Dalle.LitArma(i).lActive = False
        'Next

        With myPoutre.Dalle.Bac
            .Hp = 58 / 1000
            .h_rs = 0
            .Tp = 1 / 1000
            .Bb = 62 / 1000
            .Bt = 101 / 1000
            .Ep = 207 / 1000
            .Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
            .AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus 'permet de prendre en compte le bac pour le calcul des armatures transversales
        End With

        'With myPoutre.Dalle.Connecteur
        '    .hsc = 100 / 1000
        '    .d = 19 / 1000
        'End With

        myPoutre.NombreZones(myPoutre.IndicePremiereTravee) = 1
        myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.Espacement_Bac_TransZone(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.EspacementZone(myPoutre.IndicePremiereTravee, 0) = 0.207
        myPoutre.lAutomaticDesign = False

        'MATERIAUX
        With myPoutre.Section.Acier
            .Nuance = "S355"
            .Qualite = "EC3"
            .NormeProduit = "EN 1993-1-1"
            .Reduction = "Table 3.1"

            .Plages.Clear()
            Dim MyPlage As cls_Acier.strucPlage
            For i As Integer = 0 To SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages.Count - 1
                MyPlage.Ep = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Ep
                MyPlage.Fy = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fy
                MyPlage.Fu = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fu
                .Plages.Add(MyPlage)
            Next
        End With

        With myPoutre.Dalle.beton
            .Classe = "C25/30"
            .Ecm = 31000
        End With

        'myPoutre.Dalle.Connecteur.Fu = 450

        myPoutre.Dalle.Bac.msurf = 8.53 '8.53 kg/m2
        myPoutre.Dalle.Bac.fyp = 350

        'CHARGES
        myPoutre.InitialisePoidsPropres() '/!\ Les valeurs calculées par le logiciel ne sont pas exactement les mêmes que dans l'article. Elles seront recalculées à la main
        myPoutre.ChargesU("G1").QSurf(myPoutre.IndicePremiereTravee) = 26 * 0.00972 * 1000  ' *2.5 = 0.6318 kN/ml - > charge permanente supplémentaire induit par l'effet de marrre (cf article)
        myPoutre.ChargesU("QC").QSurf(myPoutre.IndicePremiereTravee) = 0.75 * 1000
        myPoutre.ChargesU("QC").FReparties(myPoutre.IndicePremiereTravee).Add(New cls_ForceRepartie(12.5 / 2 - 3 / 2, (2.925 - 1.875) * 1000, 12.5 / 2 + 3 / 2, (2.925 - 1.875) * 1000, 0)) '1 kN/m2 répartie s/ 3mx3m et centré à mi-travée

        'COEFFICIENTS PARTIELS
        myPoutre.Initialise_CoefficientsCombinaisons() 'Initialise les coefficients par défaut 
        myPoutre.lCombELU(0) = True 'activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myPoutre.lCombELS(0) = True 'activation de la première combinaison ELS par défaut (G + Q)
        myPoutre.lCombELCURules(0) = True 'activation de la première combinaison ELU pendant la phase de construction activée 
        myPoutre.lCombELCSRules(0) = True 'activation de la première combinaison ELS pendant la phase de construction activée 

        With myPoutre.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

#End Region

#Region "Lancement des calculs"


        Dim NomChargesA(), strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        ReDim NomChargesA(9)
        NomChargesA(0) = "Permanent loads"
        NomChargesA(1) = "Self-weight"
        NomChargesA(2) = "Self weight with props"
        NomChargesA(3) = "Self weight without props"
        NomChargesA(4) = "Other permanent loads"
        NomChargesA(5) = "Live loads"
        NomChargesA(6) = "Conf. no"
        NomChargesA(7) = "Shrinkage of the slab"
        NomChargesA(8) = "Shrinkage of the encasement"
        NomChargesA(9) = "Construction loads"

        strRacineELU = "ULS"
        strRacineELS = "SLS"
        strRacineELF = "FLS"
        strRacineELUC = "ULS_C"
        strRacineELSC = "SLS_C"

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myPoutre.VerifAcier(0)
                myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myPoutre.VerifMixte(0)
                myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myPoutre.VerifAcier(0)
                    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myPoutre.InitialiseCalculs(NomChargesA)
        myPoutre.AAA_CalculMNVInternesN()
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELU, myPoutre.lCombELU, myPoutre.CoefCombELU, strRacineELU, myPoutre.CombiA_ELU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELS, myPoutre.lCombELS, myPoutre.CoefCombELS, strRacineELS, myPoutre.CombiA_ELS)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombFeu, myPoutre.lCombFeu, myPoutre.CoefCombFeu, strRacineELF, myPoutre.CombiA_ELF)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myPoutre.lCombELCURules, myPoutre.CoefCombELCU, strRacineELUC, myPoutre.CombiA_ELCU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myPoutre.lCombELCSRules, myPoutre.CoefCombELCS, strRacineELSC, myPoutre.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU CONSTRUCTION
        Dim MEdConstruction(,) As Decimal = Nothing
        Dim MEdMaxConstruction, MEdMinConstruction, iNodeMMinConstruction, iNodeMMaxConstruction As Decimal

        Dim VEdConstruction(,) As Decimal = Nothing
        Dim VEdMaxConstruction, VEdMinConstruction, iNodeVMinConstruction, iNodeVMaxConstruction As Decimal

        myPoutre.CombiA_ELCU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEdConstruction, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELCU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEdConstruction, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEdConstruction, myPoutre.Nodes.nbNodes, MEdMaxConstruction, MEdMinConstruction, iNodeMMaxConstruction, iNodeMMinConstruction)
        EnveloppeTableauEfforts(VEdConstruction, myPoutre.Nodes.nbNodes, VEdMaxConstruction, VEdMinConstruction, iNodeVMaxConstruction, iNodeVMinConstruction)

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, True) 'Poutre seul durant la phase de construction
        'myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre) 'Vérification durant la phase finale -> Voir Cas test 2023 n°4

#End Region

#Region "Verification de l'analyse de la poutre"

        'VERIFICATION DES EFFORTS A L'ELU CONSTRUCTION

        Valeur = MEdMaxConstruction
        ValRef = 349.2 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdMaxConstruction
        ValRef = 109.98 * 10 ^ 3 'Valeur recalculée à la main, il y'a une coquille dans l'article 
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la classification de la section (ELU)"

        Valeur = myPoutre.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la résistance à la flexion (ELU)"

        'A L'ELU CONSTRUCTION

        Valeur = myPoutre.VerifAcier(0).CritereM.Resistance(iNodeMMaxConstruction)
        ValRef = 775.4 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

        Valeur = myPoutre.VerifAcier(0).CritereM.CritereMax
        ValRef = 0.45
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region "Vérification de la résistance à l'effort tranchant (ELU)"

        'A L'ELU CONSTRUCTION

        Valeur = myPoutre.VerifAcier(0).CritereV.Resistance(iNodeMMaxConstruction)
        ValRef = 1033 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx))

        Valeur = myPoutre.VerifAcier(0).CritereV.CritereMax
        ValRef = 109.98 / 1033 '= 0.10647 : valeur recalculée à la main
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELCU As Decimal
        If ValRef <= 0.5 Then
            rhoVELCU = 0
        ElseIf ValRef >= 1 Then
            rhoVELCU = 1
        Else
            rhoVELCU = (2 * 0.108 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
        End If

#End Region

#Region "Vérification de la résistance au voilement (ELU)"

        Assert.IsTrue(myPoutre.Section.IsVoilementParCisaillement(myPoutre.Param.EtaW) = False) '--> Vérification de la résistance au voilement non nécessaire 
#End Region

#Region "Verification de la résistance à l'interaction MV (ELU)"

        '--> Sans objet, on doit retrouver les mêmes résultats que pour la résistance à la flexion simple

        Valeur = myPoutre.VerifAcier(0).CritereMV.Resistance(iNodeMMaxConstruction)
        ValRef = 775.4 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

        Valeur = myPoutre.VerifAcier(0).CritereMV.CritereMax
        ValRef = 0.45
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region "Vérification de la résistance au déversement (ELU)"

        'Recalcul à la main du Mcr avec le logiciel LTBeam 

        Dim Mcr, lambda_LT, phi_LT, khi_LT, MbRd As Decimal

        Mcr = 913.77 * 1000 'cf fichier LTBeam dans le répertoire Manuel Validation
        'Pour info, la valeur calculée à la main dans l'article donne 903 kN.m.
        ' Par contre, l'article indique qu'avec LTBeam, on obtient une valeur de 900 kN.m, je ne comprends pas comment est obtenue cette valeur (A DISCUTER)

        lambda_LT = Math.Sqrt(799 * 1000 / Mcr)
        phi_LT = 0.5 * (1 + 0.34 * (lambda_LT - 0.2) + lambda_LT ^ 2)
        khi_LT = 1 / (phi_LT + Math.Sqrt(phi_LT ^ 2 - lambda_LT ^ 2))
        MbRd = khi_LT * 799 * 1000

        Valeur = myPoutre.VerifAcier(0).CritereLTB.Resistance(myPoutre.IndicePremiereTravee)
        ValRef = MbRd ' = 510 kN (dans l'article, on a 492.5 kN.m)
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

        Valeur = myPoutre.VerifAcier(0).CritereLTB.CritereMax
        ValRef = 349.2 * 1000 / MbRd '0.684 (dans l'article, on a 0.709)
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance au déversement

#End Region

#Region "Vérification des propriétés élastiques (ELS)"

        '        '--> Coefficient d'équivalence à court terme n0

        Dim n0 As Decimal = 210 / 31.476 '= 6.6717
        Valeur = n0 'calcul manuel car l'article n0 = 210/31
        ValRef = myPoutre.Section.Enrobage.Beton.CoefficientEquivalenceCT()
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase de coulage, poutre non etayée

        Valeur = 42930  '42 930 cm4
        ValRef = myPoutre.Section.ProfilA.InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Dim zANE, InertieY, Mel As Decimal

        myPoutre.Section.ProprietesElastiquesAcierMyy(1, myPoutre.Param.Gamma, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, Mel, True)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase mixte pour les actions court termes

        Valeur = 52814  '52 814 cm4

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 6.77, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = -205.2
        ValRef = zANE * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule


#End Region

#Region "Vérification du calcul des fleches (ELS)"

        '--> Fleches due à G1

        'Valeur = myPoutre.ChargesA(0).FlecheMax * 1000
        'ValRef = 30.6
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))  'GUD: Le calcul de la fleche G1 se fait avec les nEqLT. Il faudrait les faire avec nCT pour la phase construction (création d'un deuxieme cas G1?)

        '--> Fleches due à Q

        'Valeur = myPoutre.ChargesA(4).FlecheMax * 1000
        'ValRef = 6.5
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))


        'Discuté avec POM: Pas pertinent de donner les fleches à l'ELS Construction

#End Region


    End Sub

    <TestMethod()> Public Sub TestMethodLTB_RCM2023no3()

        '=======================================
        '
        ' 09/02/2024 : GUD: Test méthode LTB appliqué au cas RCM 2023 numero 3
        '
        '=======================================
        '
        ' Cas de test RCM 2023 n°3
        '
        '=======================================

        Dim MyDonnees As New CTICM_DATA_DLLS.DATA_DLLS
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 12.5     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 100 'Comme pour LTBeam

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 101.0975 * 0.0001          'm2
                .InertieY(i) = 65472.963207 * 0.00000001   'm4
                .RayGirPol(i) = 21.0670264 * 0.01   'm
                .InertieT(i) = 1130.500797 * 0.00000001   'm4
                .InertieZ(i) = 6608.12464 * 0.00000001   'm4
                .InertieW(i) = 1125230.20833 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, .NbNodes - 1}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 2
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = (1.35 * 10.67 + 1.5 * 1.875) * 1000 ' = 17 217 N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 248.5 / 1000
            .ForceRep(0, 1) = (1.35 * 10.67 + 1.5 * 1.875) * 1000 ' = 17 217 N/m
            .xForceRep(0, 1) = L

            .ForceRep(1, 0) = 1.5 * 1.05 * 1000        ' = 1575 N/m
            .xForceRep(1, 0) = L / 2 - 3 / 2
            .zForceRepC(1) = 248.5 / 1000
            .ForceRep(1, 1) = 1.5 * 1.05 * 1000           ' = 1575 N/m
            .xForceRep(1, 1) = L / 2 + 3 / 2
        End With

        'Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 2
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = MyDonnees.NbNodes - 1
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1


        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE LTBeam ===
        Dim MuiCrRef As Double = 2.6163

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%





        '=======================================
        '
        ' 09/02/2024 : GUD: Test méthode LTB
        '
        '=======================================
        '
        ' On reprend le meme cas test mais avec NbNode = 27 pour voir s'il y'a une grande différence (valeur par défaut dans le moteur de calcul)
        '
        '=======================================

        MyDonnees = New CTICM_DATA_DLLS.DATA_DLLS
        ParamLTB = Nothing

        'Dim L As Decimal = 12.5     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 27 '/!\ Valeur modifiée % au cas de dessus

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 101.0975 * 0.0001          'm2
                .InertieY(i) = 65472.963207 * 0.00000001   'm4
                .RayGirPol(i) = 21.0670264 * 0.01   'm
                .InertieT(i) = 1130.500797 * 0.00000001   'm4
                .InertieZ(i) = 6608.12464 * 0.00000001   'm4
                .InertieW(i) = 1125230.20833 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, .NbNodes - 1}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 2
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = (1.35 * 10.67 + 1.5 * 1.875) * 1000 ' = 17 217 N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 248.5 / 1000
            .ForceRep(0, 1) = (1.35 * 10.67 + 1.5 * 1.875) * 1000 ' = 17 217 N/m
            .xForceRep(0, 1) = L

            .ForceRep(1, 0) = 1.5 * 1.05 * 1000        ' = 1575 N/m
            .xForceRep(1, 0) = L / 2 - 3 / 2
            .zForceRepC(1) = 248.5 / 1000
            .ForceRep(1, 1) = 1.5 * 1.05 * 1000           ' = 1575 N/m
            .xForceRep(1, 1) = L / 2 + 3 / 2
        End With

        'Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 2
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = MyDonnees.NbNodes - 1
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1


        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        MyDLLRDM = New CTICM_RDM.CALCUL_RDM
        MyOutput_RDM = Nothing
        TextError_RDM = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        MyDLL_LTB = New CTICM_LTB.CALCUL_LTB
        MyOutput_LTB = Nothing
        TextError_LTB = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE LTBeam ===
        'Dim MuiCrRef As Double = 2.6163

        'MyOutput_LTB.CoefCr = 2.618488 -> augmentation du alpha cr de 0.0836 % (acceptable + le test ci-dessous est toujours valide)

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%

    End Sub

    <TestMethod()> Public Sub Test_RCM_2023_4()

        'Cas test issu de la revue RCM (2023-4):
        '
        '
        '"Vérification d'une poutre mixte acier-béton enrobée pendant la phase finale"

#Region "Initialisation du logiciel"

        'Note GUD: Partie reprise du module de demarrage car une partie des calculs nécessite d'avoir chargé les BDD, ce qui est fait à l'initialisation du logiciel 

        '--> Récupération des informations générales du logociel - Non modifiable par l'utilisateur

        LogicielInfo.NomLogiciel = "ABCPMX-II"
        LogicielInfo.Version = "1.0"
        LogicielInfo.AnneeVersion = "2024"
        LogicielInfo.MailSupport = "support.logiciels@cticm.com"
        LogicielInfo.Extension = "pmx"
        LogicielInfo.Racine = "ABCPMX"

        LogicielInfo.Maitre = EnuMaitre.CTICM
        LogicielOptions.lNoS235 = (LogicielInfo.Maitre = EnuMaitre.ArcelorMittal)
        LogicielOptions.lDebug = False

        LogicielRep.Config = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version

        LastIndexW.OptionsCalcul = Enu_OptionsCalcul.Gamma
        LastIndexW.OptionsLogiciel = Enu_OptionsLogiciel.General

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

        LogicielOptions.Gamma.GammaM0 = 1
        LogicielOptions.Gamma.GammaM1 = 1
        LogicielOptions.Gamma.GammaM2 = 1.25

        LogicielOptions.Gamma.GammaC = 1.5
        LogicielOptions.Gamma.GammaVs = 1.25
        LogicielOptions.Gamma.GammaVc = 1.25
        LogicielOptions.Gamma.lGammaV_unique = True
        LogicielOptions.Gamma.GammaS = 1.15
        LogicielOptions.Gamma.GammaP = 1

        LogicielOptions.Gamma.GammaM_fi = 1
        LogicielOptions.Gamma.GammaC_fi = 1
        LogicielOptions.Gamma.GammaV_fi = 1

        LogicielOptions.Gamma.GammaG_sup = 1.35
        LogicielOptions.Gamma.GammaG_inf = 1
        LogicielOptions.Gamma.GammaQ = 1.5

        LogicielOptions.Gamma.Psi0_Q1 = 0.7
        LogicielOptions.Gamma.Psi1_Q1 = 0.5
        LogicielOptions.Gamma.Psi2_Q1 = 0.3

        LogicielOptions.Gamma.Psi0_Q2 = 0.7
        LogicielOptions.Gamma.Psi1_Q2 = 0.5
        LogicielOptions.Gamma.Psi2_Q2 = 0.3

        '--> Options du domaine d'application et options de calcul

        InitialiseOptionsScope()
        InitialiseOptionsCalcul()

#End Region

#Region "Initialisation de la poutre"

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.MixteEnrobage, "", LogicielOptions, OptionsCalcul)
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

        Dim lOK, lTrouve As Boolean
        InitialisePoutreDeBases(myPoutre, lOK)
        InitialiseBacDeBase(myPoutre.Dalle.Bac, lTrouve)
        InitialiseGoujonDeBase(myPoutre.Dalle.Connecteur, lTrouve)

        InitialiseDalleDefault(myPoutre.Dalle, cls_ProfilA.Enum_TypeSectionAcier.Lamine)

        myPoutre.Initialise_CoefficientsCombinaisons()
        myPoutre.InitialisePoidsPropres()

#End Region

#Region "Renseignement des données de l'article"

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) = 12.5 '12.5m
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 2.5
        myPoutre.EntraxeD2 = 2.5

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            .ha = 497 / 1000
            .Tw = 8.4 / 1000
            .Bfs = 200 / 1000
            .Bfi = .Bfs
            .Tfs = 14.5 / 1000
            .Tfi = .Tfs
            .Rcs = 21 / 1000
            .Rci = .Rcs
            .InitialiseProprietes()
        End With

        myPoutre.Section.typeSection = cls_Section.Enum_TypeSection.MixteEnrobage

        With myPoutre.Section.Enrobage
            .Beton.Classe = "C25/30"
            .Beton.Ecm = 31000

            .LitArma(2).PhiExt = 8 / 1000 'Lit supérieur
            .LitArma(2).NbExt = 1
            .LitArma(2).PhiMil = 0
            .LitArma(2).NbMil = 0
            .LitArma(2).PhiInt = 8 / 1000
            .LitArma(2).NbInt = 1
            .LitArma(2).zPosRatio = (14.5 + 40) / 497
            .LitArma(2).lActiveExt = False
            .LitArma(2).lActiveInt = False

            .LitArma(1).lActiveExt = False 'Lit intermédiaire
            .LitArma(1).lActiveInt = False

            .LitArma(0).PhiInt = 12 / 1000 'Lit inférieur
            .LitArma(0).NbInt = 1
            .LitArma(0).PhiMil = 0
            .LitArma(0).NbMil = 0
            .LitArma(0).PhiExt = 8 / 1000
            .LitArma(0).NbExt = 1
            .LitArma(0).zPosRatio = (497 - 14.5 - 60) / 497
            .LitArma(0).lActiveExt = True
            .LitArma(0).lActiveInt = True

            .Etriers_EnrobageY = 10 / 1000 'pas utile pour le calcul
            .Etriers_EnrobageZ = (60 - 12 / 2 - 6) / 1000 'dans l'article, les enrobages des lits sup et inf ne sont pas identiques.
            'Comme le lit sup est très probablement inactif, je renseigne l'enrobage inf

            .Ratio_bc = 1

            .Beton.RhoC = 25 / (9.81 * 10 ^ (-3)) 'Modification de la masse volumique du béton pour arrivée à une charge volumique de 25 kN/m3 (permet de retrouver les valeurs de l'article)
        End With

        myPoutre.Section.Enrobage.AcierArmatures.Classe = "B500"
        myPoutre.Section.Enrobage.AcierArmatures.MAJProprietes()

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 140 / 1000

            .beton.RhoC = 25 / (9.81 * 10 ^ (-3)) 'Modification de la masse volumique du béton pour arrivée à une charge volumique de 25 kN/m3 (permet de retrouver les valeurs de l'article)
        End With

        For i As Integer = 0 To myPoutre.Dalle.LitArma.Count - 1 'on ne prend pas en compte les armatures dans le calcul dans l'exemple traité 
            myPoutre.Dalle.LitArma(i).lActive = False
        Next

        With myPoutre.Dalle.Bac
            .Hp = 58 / 1000
            .h_rs = 0
            .Tp = 1 / 1000
            .Bb = 62 / 1000
            .Bt = 101 / 1000
            .Ep = 207 / 1000
            .Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
            .AppuiT = cls_Bac.EnuConfigTAppui.BetonSeulContinu 'permet de prendre en compte le bac pour le calcul des armatures transversales
        End With

        With myPoutre.Dalle.Connecteur
            .hsc = 100 / 1000
            .d = 19 / 1000
        End With

        myPoutre.NombreZones(myPoutre.IndicePremiereTravee) = 1
        myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0) = 2
        myPoutre.Espacement_Bac_TransZone(myPoutre.IndicePremiereTravee, 0) = 1
        myPoutre.EspacementZone(myPoutre.IndicePremiereTravee, 0) = 0.207
        myPoutre.LongueurZone(myPoutre.IndicePremiereTravee, 0) = myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee)
        myPoutre.lAutomaticDesign = False

        'MATERIAUX
        With myPoutre.Section.Acier
            .Nuance = "S355"
            .Qualite = "EC3"
            .NormeProduit = "EN 1993-1-1"
            .Reduction = "Table 3.1"

            .Plages.Clear()
            Dim MyPlage As cls_Acier.strucPlage
            For i As Integer = 0 To SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages.Count - 1
                MyPlage.Ep = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Ep
                MyPlage.Fy = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fy
                MyPlage.Fu = SteelBase.Grades(.Nuance).Qualites(.Qualite).ReductionCurv(.Reduction).Plages(i).Fu
                .Plages.Add(MyPlage)
            Next
        End With

        With myPoutre.Dalle.beton
            .Classe = "C25/30"
            .Ecm = 31000
        End With

        myPoutre.Dalle.Connecteur.Fu = 450

        myPoutre.Dalle.Bac.msurf = 8.53 '8.53 kg/m2
        myPoutre.Dalle.Bac.fyp = 350

        'CHARGES
        myPoutre.InitialisePoidsPropres() '/!\ Les valeurs calculées par le logiciel ne sont pas exactement les mêmes que dans l'article. Elles seront recalculées à la main
        myPoutre.ChargesU("G1").QSurf(myPoutre.IndicePremiereTravee) = 25 * 0.0097 * 1000  ' *2.5 = 0.6318 kN/ml - > charge permanente supplémentaire induit par l'effet de marrre (cf article)
        myPoutre.ChargesU("G2").QSurf(myPoutre.IndicePremiereTravee) = 1 * 1000
        myPoutre.ChargesU("Q1").QSurf(myPoutre.IndicePremiereTravee) = 2.5 * 1000

        'COEFFICIENTS PARTIELS
        myPoutre.Initialise_CoefficientsCombinaisons() 'Initialise les coefficients par défaut 
        myPoutre.lCombELU(0) = True 'activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myPoutre.lCombELS(0) = True 'activation de la première combinaison ELS par défaut (G + Q)
        myPoutre.lCombELCURules(0) = False 'activation de la première combinaison ELU pendant la phase de construction activée 
        myPoutre.lCombELCSRules(0) = False 'activation de la première combinaison ELS pendant la phase de construction activée 

        With myPoutre.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

#End Region

#Region "Lancement des calculs"


        Dim NomChargesA(), strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        ReDim NomChargesA(9)
        NomChargesA(0) = "Permanent loads"
        NomChargesA(1) = "Self-weight"
        NomChargesA(2) = "Self weight with props"
        NomChargesA(3) = "Self weight without props"
        NomChargesA(4) = "Other permanent loads"
        NomChargesA(5) = "Live loads"
        NomChargesA(6) = "Conf. no"
        NomChargesA(7) = "Shrinkage of the slab"
        NomChargesA(8) = "Shrinkage of the encasement"
        NomChargesA(9) = "Construction loads"

        strRacineELU = "ULS"
        strRacineELS = "SLS"
        strRacineELF = "FLS"
        strRacineELUC = "ULS_C"
        strRacineELSC = "SLS_C"

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myPoutre.VerifAcier(0)
                myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myPoutre.VerifMixte(0)
                myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myPoutre.VerifAcier(0)
                    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myPoutre.InitialiseCalculs(NomChargesA)
        myPoutre.AAA_CalculMNVInternesN()
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELU, myPoutre.lCombELU, myPoutre.CoefCombELU, strRacineELU, myPoutre.CombiA_ELU)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombELS, myPoutre.lCombELS, myPoutre.CoefCombELS, strRacineELS, myPoutre.CombiA_ELS)
        myPoutre.InitialiseCombiA(cls_Poutre.nbCombFeu, myPoutre.lCombFeu, myPoutre.CoefCombFeu, strRacineELF, myPoutre.CombiA_ELF)
        'myPoutre.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myPoutre.lCombELCURules, myPoutre.CoefCombELCU, strRacineELUC, myPoutre.CombiA_ELCU)
        'myPoutre.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myPoutre.lCombELCSRules, myPoutre.CoefCombELCS, strRacineELSC, myPoutre.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd(,) As Decimal = Nothing
        Dim MEdMax, MEdMin, iNodeMMin, iNodeMMax As Decimal

        Dim VEd(,) As Decimal = Nothing
        Dim VEdMax, VEdMin, iNodeVMin, iNodeVMax As Decimal

        myPoutre.CombiA_ELU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd, myPoutre.Nodes.nbNodes, MEdMax, MEdMin, iNodeMMax, iNodeMMin)
        EnveloppeTableauEfforts(VEd, myPoutre.Nodes.nbNodes, VEdMax, VEdMin, iNodeVMax, iNodeVMin)

        'VERIFICATION DE LA POUTRE 

        'myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, True) 'Poutre seul durant la phase de construction
        myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre) 'Vérification durant la phase finale -> Voir Cas test 2023 n°4

#End Region

#Region "Verification de l'analyse de la poutre"

        'VERIFICATION DES EFFORTS A L'ELU

        Dim MG1max, VG1max, MG2max, VG2max, MQmax, VQmax As Decimal
        Dim MG1min, VG1min, MG2min, VG2min, MQmin, VQmin As Decimal
        Dim iNodeMax, iNodeMin As Integer

        'G1

        myPoutre.ChargesA(0).EnveloppesMoments(MG1max, iNodeMax, MG1min, iNodeMin)
        myPoutre.ChargesA(0).EnveloppesTranchants(VG1max, iNodeMax, VG1min, iNodeMin)

        Valeur = MG1max
        ValRef = 202.7 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VG1max
        ValRef = 64.875 * 10 ^ 3 'valeur calculée à la main
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        'G2

        myPoutre.ChargesA(1).EnveloppesMoments(MG2max, iNodeMax, MG2min, iNodeMin)
        myPoutre.ChargesA(1).EnveloppesTranchants(VG2max, iNodeMax, VG2min, iNodeMin)

        Valeur = MG2max
        ValRef = 48.8 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VG2max
        ValRef = 15.625 * 10 ^ 3 'valeur calculée à la main
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        'Q

        myPoutre.ChargesA(2).EnveloppesMoments(MQmax, iNodeMax, MQmin, iNodeMin)
        myPoutre.ChargesA(2).EnveloppesTranchants(VQmax, iNodeMax, VQmin, iNodeMin)

        Valeur = MQmax
        ValRef = 122.1 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VQmax
        ValRef = 39.1 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        'ELU

        Valeur = MEdMax
        ValRef = 522.6 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdMax
        ValRef = 167.269 * 10 ^ 3 'valeur recalculée à la main
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la classification de la section (ELU)"

        Valeur = myPoutre.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la résistance des connecteurs (ELU)"

        Valeur = myPoutre.Dalle.Connecteur.PRdDallePleineG1G2Acier(myPoutre.Param.Gamma.GammaVs)
        ValRef = 81.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = myPoutre.Dalle.Connecteur.PRdDallePleineG1Beton(myPoutre.Dalle.beton.Fck, 31000, myPoutre.Param.Gamma.GammaVc)
        ValRef = 73.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Calcul avec 2 connecteurs par ondes
        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine,
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         2, myPoutre.Dalle.beton.Fck,
                                                         31000, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 37.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine,
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         2, myPoutre.Dalle.beton.Fck,
                                                         myPoutre.Dalle.beton.Ecm, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 37.44 * 1000 'valeur recalculée à la main
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))


#End Region

#Region "Vérification du degré de connection minimal (ELU)"

        Valeur = myPoutre.VerifMixte(0).DegConnexMin(myPoutre.IndicePremiereTravee)
        ValRef = 0.625
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification du dimensionnement de la connection (ELU)"


        Valeur = myPoutre.Section.ResistanceTractionProfile(myPoutre.Param.Gamma.GammaM0)
        ValRef = 3589 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification de la valeur de Na,Rd

        Valeur = myPoutre.Section.NResistanceArmaturesEnrobage(myPoutre.Param.Gamma.GammaS)
        ValRef = 142.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification de la valeur de Ns

        Dim Beff As Decimal
        Dim lSimple As Boolean = True 'booléen qui indique qu'on va utiliser le modèle simplifié pour le calcul de beff
        Beff = myPoutre.BeffDalle(myPoutre.Nodes.xTravee(iNodeMMax), myPoutre.IndicePremiereTravee, lSimple, False)
        Valeur = myPoutre.Dalle.NResistanceCompressionDalle(Beff, myPoutre.Param.Gamma.GammaC)
        ValRef = 2904 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification de la valeur de Nc,Rd à mi travée

        Valeur = myPoutre.VerifMixte(0).DegConnex(myPoutre.IndicePremiereTravee, 0)
        ValRef = 30.19 * 2 * 37.1 / 2904 '= 0.771 -> Valeur recalculée à la main pour tenir compte de la linéarisation de la résistance des connecteurs
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

#Region "Vérification de la résistance à la flexion (ELU)"

        'A L'ELU

        Dim zANE, MRd As Decimal

        Valeur = myPoutre.VerifMixte(0).CritereM.Resistance(iNodeMMax)
        ValRef = 1186 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereM.CritereMax
        ValRef = 0.441
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region "Vérification de la résistance à l'effort tranchant (ELU)"

        'A L'ELU

        Valeur = myPoutre.Section.VplRd(myPoutre.Param.Gamma.GammaM0)
        ValRef = 1033 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à l'effort tranchant

        Valeur = myPoutre.VerifMixte(0).CritereV.CritereMax
        ValRef = 0.162
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELU As Decimal
        If ValRef <= 0.5 Then
            rhoVELU = 0
        ElseIf ValRef >= 1 Then
            rhoVELU = 1
        Else
            rhoVELU = (2 * 0.162 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
        End If



#End Region

#Region "Vérification de la résistance au voilement (ELU)"

        Assert.IsTrue(myPoutre.Section.IsVoilementParCisaillement(myPoutre.Param.EtaW) = False) '--> Vérification de la résistance au voilement non nécessaire 
#End Region

#Region "Verification de la résistance à l'interaction MV"

        '--> Sans objet, on doit retrouver les mêmes résultats que pour la résistance à la flexion simple


        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, rhoVELU, zANE, MRd)

        Valeur = myPoutre.VerifMixte(0).CritereMV.Resistance(iNodeMMax)
        ValRef = 1186 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereMV.CritereMax
        ValRef = 0.441 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region " Vérification du dimensionnement des armatures transversales (ELU)"

        'myPoutre.CalculArmaturesTransversales()

        '--> TauEd

        Dim b0, ksf As Decimal
        b0 = 4 * 19 / 1000
        ksf = (1.25 - b0 / 2) / 2.5

        Valeur = ksf * 2 * 37.44 / (0.207 * 82) 'Valeur recalculée avec la valeur correcte de PRd
        ValRef = myPoutre.VerifMixte(0).TauEd(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la contrainte tangentielle 

        '--> Thetaf

        Valeur = 0.5 * Math.Asin(2 * 2.06 / (0.54 * 16.7)) ' 13.59° -> VALEUR RECALCULEE car dans l'article on considère conservativement theta = 45°
        Valeur = Math.Max(Valeur, 27 * Math.PI / 180) 'Borne inférieure
        Valeur = Math.Min(Valeur, 45 * Math.PI / 180) 'Borne supérieure
        ValRef = myPoutre.VerifMixte(0).Thetaf(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'angle de la bielle

        '--> As,trans

        Valeur = 2.055 * 10 ^ (-4) 'valeur recalculée à la main
        ValRef = myPoutre.VerifMixte(0).As_s_transv(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul des armatures transversales

#End Region

#Region "Vérification des propriétés élastiques (ELS)"

        '--> Coefficient d'équivalence à court terme n0

        Dim n0 As Decimal = 210 / 31.476 '= 6.6717
        Valeur = n0 'calcul manuel car l'article n0 = 210/31
        ValRef = myPoutre.Dalle.beton.CoefficientEquivalenceCT()
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Vérification des calculs des coefficients d'équivalences à LT de la DALLE (50 ans)
        Dim ageT As Integer = 50 * 365 '50 ans, en jours
        Dim ageT0 As Integer = 28
        Dim h0 As Decimal = 164 / 1000
        Dim PHIrh As Decimal = 1 + (1 - 50 / 100) / (0.1 * (h0 * 1000) ^ (1 / 3))
        Dim betaFcm As Decimal = 16.8 / Math.Sqrt(25 + 8)
        Dim betaT0 As Decimal = 1 / (0.1 + ageT0 ^ 0.2)
        Dim phi0 As Decimal = PHIrh * betaFcm * betaT0
        Dim betaH As Decimal = Math.Min(1.5 * (1 + (0.012 * 50 / 100) ^ 18) * (h0 * 1000) + 250, 1500)
        Dim betaCTT0 As Decimal = ((ageT - ageT0) / (betaH + ageT - ageT0)) ^ 0.3
        Dim phiTT0 As Decimal = phi0 * betaCTT0

        '--> h0

        Valeur = h0
        ValRef = myPoutre.Dalle.NotionalSizeH0(myPoutre.Section.ProfilA.Bfs)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Phi RH

        Valeur = PHIrh
        ValRef = myPoutre.Dalle.beton.PhiRH(myPoutre.Param.RH, h0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '-->Beta fcm

        Valeur = betaFcm
        ValRef = myPoutre.Dalle.beton.BetaFcm()
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Beta t0

        Valeur = betaT0
        ValRef = myPoutre.Dalle.beton.Beta_t0(ageT0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> phi0 n'est pas évalué par une fonction à part entière

        '--> BetaH

        'Valeur = betaH
        'ValRef = myPoutre.Dalle.beton.BetaH(myPoutre.Param.RH, h0)
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> BetaC(t,t0)

        Valeur = betaCTT0
        ValRef = myPoutre.Dalle.beton.BetaC_tt0(myPoutre.Param.RH, h0, ageT, ageT0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> phi(t,t0) n'est pas évalué par une fonction à part entière

        phiTT0 = phi0 * betaCTT0

        '--> Coefficient d'équivalence LT CP nL

        Valeur = n0 * (1 + 1.1 * phiTT0) '27.5183638
        ValRef = myPoutre.Elements(0).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE nL

        Valeur = n0 '6.67
        ValRef = myPoutre.Elements(2).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE SH

        ageT = 50 * 365 '50 ans, en jours
        ageT0 = 1
        h0 = 164 / 1000
        PHIrh = 1 + (1 - 50 / 100) / (0.1 * (h0 * 1000) ^ (1 / 3))
        betaFcm = 16.8 / Math.Sqrt(25 + 8)
        betaT0 = 1 / (0.1 + ageT0 ^ 0.2)
        phi0 = PHIrh * betaFcm * betaT0
        betaH = Math.Min(1.5 * (1 + (0.012 * 50 / 100) ^ 18) * (h0 * 1000) + 250, 1500)
        betaCTT0 = ((ageT - ageT0) / (betaH + ageT - ageT0)) ^ 0.3
        phiTT0 = phi0 * betaCTT0

        Valeur = n0 * (1 + 0.55 * phiTT0) '26.0715
        ValRef = myPoutre.Elements(4).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Vérification des calculs des coefficients d'équivalences à LT de l'ENROBAGE (50 ans)
        ageT = 50 * 365 '50 ans, en jours
        ageT0 = 56
        h0 = (497 - 2 * 14.5) * (200 / 2 - 8.4 / 2) - 21 ^ 2 * (1 - Math.PI / 4) * 2  'calcul de l'air dans 1 chambre
        h0 /= (497 - 2 * 14.5) ' on divise par le perimetre en contact avec l'atmosphere 
        h0 *= 2
        h0 /= 1000
        PHIrh = 1 + (1 - 50 / 100) / (0.1 * (h0 * 1000) ^ (1 / 3))
        betaFcm = 16.8 / Math.Sqrt(25 + 8)
        betaT0 = 1 / (0.1 + ageT0 ^ 0.2)
        phi0 = PHIrh * betaFcm * betaT0
        betaH = Math.Min(1.5 * (1 + (0.012 * 50 / 100) ^ 18) * (h0 * 1000) + 250, 1500)
        betaCTT0 = ((ageT - ageT0) / (betaH + ageT - ageT0)) ^ 0.3
        phiTT0 = phi0 * betaCTT0

        '--> h0

        Valeur = h0
        ValRef = myPoutre.Section.NotionalSizeEnrobage
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Phi RH

        Valeur = PHIrh
        ValRef = myPoutre.Section.Enrobage.Beton.PhiRH(myPoutre.Param.RH, h0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '-->Beta fcm

        Valeur = betaFcm
        ValRef = myPoutre.Section.Enrobage.Beton.BetaFcm()
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Beta t0

        Valeur = betaT0
        ValRef = myPoutre.Section.Enrobage.Beton.Beta_t0(ageT0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> phi0 n'est pas évalué par une fonction à part entière

        '--> BetaH

        'Valeur = betaH
        'ValRef = myPoutre.Dalle.beton.BetaH(myPoutre.Param.RH, h0)
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> BetaC(t,t0)

        Valeur = betaCTT0
        ValRef = myPoutre.Section.Enrobage.Beton.BetaC_tt0(myPoutre.Param.RH, h0, ageT, ageT0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> phi(t,t0) n'est pas évalué par une fonction à part entière

        phiTT0 = phi0 * betaCTT0

        '--> Coefficient d'équivalence LT CP nL

        Valeur = n0 * (1 + 1.1 * phiTT0) '27.5183638
        ValRef = myPoutre.Elements(0).nEqEnrob
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE nL

        Valeur = n0 '6.67
        ValRef = myPoutre.Elements(2).nEqEnrob
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE SH

        ageT = 50 * 365 '50 ans, en jours
        ageT0 = 1
        h0 = 191 / 1000
        PHIrh = 1 + (1 - 50 / 100) / (0.1 * (h0 * 1000) ^ (1 / 3))
        betaFcm = 16.8 / Math.Sqrt(25 + 8)
        betaT0 = 1 / (0.1 + ageT0 ^ 0.2)
        phi0 = PHIrh * betaFcm * betaT0
        betaH = Math.Min(1.5 * (1 + (0.012 * 50 / 100) ^ 18) * (h0 * 1000) + 250, 1500)
        betaCTT0 = ((ageT - ageT0) / (betaH + ageT - ageT0)) ^ 0.3
        phiTT0 = phi0 * betaCTT0

        Valeur = n0 * (1 + 0.55 * phiTT0) '26.0715
        ValRef = myPoutre.Elements(4).nEqEnrob
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Propriétés en phase de coulage, poutre non etayée

        Valeur = 42930
        ValRef = myPoutre.Section.ProfilA.InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Dim InertieY, Mel As Decimal

        myPoutre.Section.ProprietesElastiquesAcierMyy(1, myPoutre.Param.Gamma, zANE, InertieY, MRd)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, Mel, True)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase mixte pour le cas de charge G2

        Valeur = 99139

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 24.3, 27.1, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = (-244 + 140)
        ValRef = zANE * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule 

        '--> Propriétés en phase mixte pour le cas de charge Q

        Valeur = 142271

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 6.77, 6.77, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = (-131.5 + 140)
        ValRef = zANE * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

#End Region

#Region "Vérification du calcul des fleches (ELS)"

        '--> Fleches due à G1
        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 23.68, 1, 0, myPoutre.Dalle, zANE, InertieY, Mel)

        Valeur = myPoutre.ChargesA(0).FlecheMax * 1000
        ValRef = 5 * 10.38 * (12.5 * 1000) ^ 4 / (384 * 210000 * InertieY * 10 ^ 12) '33.34 mm

        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à G2

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 23.68, 26.57, Beff, myPoutre.Dalle, zANE, InertieY, Mel)

        Valeur = myPoutre.ChargesA(1).FlecheMax * 1000
        ValRef = 5 * 2.5 * (12.5 * 1000) ^ 4 / (384 * 210000 * InertieY * 10 ^ 12) '3.8 mm
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 6.6718, 6.6718, Beff, myPoutre.Dalle, zANE, InertieY, Mel)

        Valeur = myPoutre.ChargesA(2).FlecheMax * 1000
        ValRef = 5 * 6.25 * (12.5 * 1000) ^ 4 / (384 * 210000 * InertieY * 10 ^ 12) '6.63 mm
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleche due au retrait 

        Valeur = myPoutre.ChargesA(5).FlecheMax * 1000
        Dim NR, deltazG, Mr, deltaR As Decimal
        NR = 325 * 10 ^ (-6) * myPoutre.Section.Acier.EYoung / myPoutre.Elements(4).nEqDalle * Beff * myPoutre.Dalle.EpaisseurActive * 10 ^ 6 'N

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 24.74, 25.19, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        deltazG = myPoutre.Dalle.Bac.Hp + myPoutre.Dalle.EpaisseurActive / 2 - zANE
        Mr = NR * deltazG
        deltaR = (Mr * myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) ^ 2) / (8 * myPoutre.Section.Acier.EYoung * 10 ^ 6 * InertieY)
        ValRef = deltaR * 1000 '10.02 mm
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

#Region "Fréquence propre (ELS)"

        myPoutre.Modal.Analyse(myPoutre, 0.2, myPoutre.Hivoss.IndexQ)
        Valeur = myPoutre.Modal.Frequence

        ValRef = 4.6
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region


    End Sub


End Class