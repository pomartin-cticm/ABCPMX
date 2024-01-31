Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXInterface
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_CasTests

    <TestMethod()> Public Sub Test_RCM_2018_2()

        'Cas test issu de la revue RCM (2008-2):
        '
        '
        '"Calcul d'une poutre mixte sur appuis simples suivant l'EN 1994-1-1"




        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Initialisation du logiciel 
        '---------------------------------------------------
        '---------------------------------------------------

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


        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Initialisation de la poutre
        '---------------------------------------------------
        '---------------------------------------------------

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "")
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

        Dim lOK, lTrouve As Boolean
        InitialisePoutreDeBases(myPoutre, lOK)
        InitialiseBacDeBase(myPoutre.Dalle.Bac, lTrouve)
        InitialiseGoujonDeBase(myPoutre.Dalle.Connecteur, lTrouve)

        InitialiseDalleDefault(myPoutre.Dalle)

        myPoutre.Initialise_CoefficientsCombinaisons()
        myPoutre.InitialisePoidsPropres()




        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Renseignement des données de l'article 
        '---------------------------------------------------
        '---------------------------------------------------


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
            .t_d = 120 / 1000
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
            .Orientation = .Enum_Orientation.Perpendiculaire
            .AppuiT = .EnuConfigTAppui.NervureEtBacContinus 'permet de prendre en compte le bac pour le calcul des armatures transversales
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
        myPoutre.ChargesU("G2").QSurf(myPoutre.IndicePremiereTravee) = 1.4 * 1000
        myPoutre.ChargesU("Q1").QSurf(myPoutre.IndicePremiereTravee) = 2.5 * 1000
        myPoutre.ChargesU("QC").QSurf(myPoutre.IndicePremiereTravee) = 0.5 * 1000
        myPoutre.ChargesU("QC").FReparties(myPoutre.IndicePremiereTravee).Add(New cls_ForceRepartie(14 / 2 - 3 / 2, 1 * 3 * 1000, 14 / 2 + 3 / 2, 1 * 3 * 1000, 12.5)) '1 kN/m2 répartie s/ 3mx3m et centré à mi-travée

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

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Lancement des calculs  
        '---------------------------------------------------
        '---------------------------------------------------

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
        Select Case myPoutre.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myPoutre.VerifAcier(0)
                myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myPoutre.VerifMixte(0)
                myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                If myPoutre.TypeEtaiement <> myPoutre.EnuTypeEtaiement.FullyPropped Then
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
        Dim MEd(,) As Decimal
        Dim MEdMax, MEdMin, iNodeMMin, iNodeMMax As Decimal

        Dim VEd(,) As Decimal
        Dim VEdMax, VEdMin, iNodeVMin, iNodeVMax As Decimal

        myPoutre.CombiA_ELU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd, myPoutre.Nodes.nbNodes, MEdMax, MEdMin, iNodeMMax, iNodeMMin)
        EnveloppeTableauEfforts(VEd, myPoutre.Nodes.nbNodes, VEdMax, VEdMin, iNodeVMax, iNodeVMin)

        'COMBINAISON DES EFFORTS A L'ELU CONSTRUCTION
        Dim MEdConstruction(,) As Decimal
        Dim MEdMaxConstruction, MEdMinConstruction, iNodeMMinConstruction, iNodeMMaxConstruction As Decimal

        Dim VEdConstruction(,) As Decimal
        Dim VEdMaxConstruction, VEdMinConstruction, iNodeVMinConstruction, iNodeVMaxConstruction As Decimal

        myPoutre.CombiA_ELCU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEdConstruction, False) 'Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELCU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEdConstruction, False) 'Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEdConstruction, myPoutre.Nodes.nbNodes, MEdMaxConstruction, MEdMinConstruction, iNodeMMaxConstruction, iNodeMMinConstruction)
        EnveloppeTableauEfforts(VEdConstruction, myPoutre.Nodes.nbNodes, VEdMaxConstruction, VEdMinConstruction, iNodeVMaxConstruction, iNodeVMinConstruction)

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, True) 'Poutre seul durant la phase de construction
        myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre) 'Poutre mixte


        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de l'analyse de la poutre 
        '---------------------------------------------------
        '---------------------------------------------------

        'VERIFICATION DES EFFORTS A L'ELU

        Valeur = MEdMax
        ValRef = 655 * 10 ^ 3 'A NOTER: 655 kN.m est une valeur arrondie de l'article, une valeur plus proche (mais non exacte) serait 654.395 kN.m par exemple
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

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la classification de la section (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

        Valeur = myPoutre.Section.ClasseSectionCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la résistance des connecteurs (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

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



        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification du degré de connexion minimal (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

        Valeur = myPoutre.VerifMixte(0).DegConnexMin(myPoutre.IndicePremiereTravee)
        ValRef = 0.574
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))



        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification du dimensionnement de la connexion (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

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
        ValRef = 1 * (7 / 0.207) * 52.516 * 1000 / (2636 * 1000) '/!\ J'ai corrigé la valeur de l'article car la valeur de PRd n'est pas exactement la même du fait que la valeur de Ecm n'est pas identique
        '   (31 GPa dans l'article est directement calculée dans le logiciel) + la valeur du nombre de connecteurs n'est pas identique non plus (arrondi au premier entier inférieur dans 
        ' l'article et on garde la valeur décimale dans le logiciel). Au final, on a un eta = 0.658 dans l'article et 0.674 avec le logiciel
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul du degré de connection 

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la résistance à la flexion (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

        'A L'ELU

        Dim zANE, MRk As Decimal
        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, 0, zANE, MRk)

        Valeur = MRk
        ValRef = 468.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifMixte(0).CritereM.Resistance(iNodeMMax)
        ValRef = 783.29 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (779.4 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereM.CritereMax
        ValRef = 0.836 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

        'A L'ELU CONSTRUCTION

        Valeur = myPoutre.VerifAcier(0).CritereM.Resistance(iNodeMMax)
        ValRef = 468.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

        Valeur = myPoutre.VerifAcier(0).CritereM.CritereMax
        ValRef = 0.721
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la résistance à l'effort tranchant (ELU) 
        '---------------------------------------------------
        '---------------------------------------------------

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


        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la résistance au voilement (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

        Assert.IsTrue(myPoutre.Section.IsInteractionMV(myPoutre.Param.EtaW) = False) '--> Vérification de la résistance au voilement non nécessaire 

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la résistance à l'interaction MV (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

        '--> Sans objet, on doit retrouver les mêmes résultats que pour la résistance à la flexion simple


        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, rhoVELU, zANE, MRk)

        Valeur = MRk
        ValRef = 468.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifMixte(0).CritereMV.Resistance(iNodeMMax)
        ValRef = 783.29 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (834.6 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereMV.CritereMax
        ValRef = 0.836 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification du dimensionnement des armatures transversales (ELU)
        '---------------------------------------------------
        '---------------------------------------------------

        myPoutre.CalculArmaturesTransversales()

        '--> TauEd

        Valeur = 2.06 'Flux de cisaillement max transmis par la dalle de part et d'autre de la poutrelle (VALEUR RECALCULEE avec le vrai PRd = 52.897 kN et non 52.5 kN. Dans l'article, on a tauEd = 2.04 MPa)
        ValRef = myPoutre.TauEd(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la contrainte tangentielle 

        '--> Thetaf

        Valeur = 0.5 * Math.Asin(2 * 2.06 / (0.54 * 16.7)) 'VALEUR RECALCULEE car dans l'article on considère conservativement theta = 45°
        Valeur = Math.Max(Valeur, 27 * Math.PI / 180) 'Borne inférieure
        Valeur = Math.Min(Valeur, 45 * Math.PI / 180) 'Borne inférieure
        ValRef = myPoutre.Thetaf(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'angle de la bielle

        '--> As,trans

        Valeur = 0 'le bac seul suffit à reprendre ces efforts
        ValRef = myPoutre.As_s_transv(myPoutre.IndicePremiereTravee, 0, 0)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul des armatures transversales

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification des propriétés élastiques (ELS)
        '---------------------------------------------------
        '---------------------------------------------------

        '--> Coefficient d'équivalence à court terme n0

        Dim n0 As Decimal = 210 / 31.476
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

        Valeur = n0 * (1 + 1.1 * phiTT0)
        ValRef = myPoutre.Elements(0).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Coefficient d'équivalent CE nL

        Valeur = n0
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

        Valeur = n0 * (1 + 0.55 * phiTT0)
        ValRef = myPoutre.Elements(3).nEqDalle
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Propriétés en phase de coulage, poutre non etayée

        Valeur = 33740 * 10 ^ (-8) '33 740 cm4
        ValRef = myPoutre.Section.ProfilA.InertieY
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Dim InertieY, Mel As Decimal

        myPoutre.Section.ProprietesElastiquesAcierMyy(1, myPoutre.Param.Gamma, zANE, InertieY, MRk)
        ValRef = InertieY
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, Mel)
        ValRef = InertieY
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, 3 * n0, 0, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase mixte pour les actions court termes

        Valeur = 106266 * 10 ^ (-8)

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, 6.77, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = (120 - 114) / 1000
        ValRef = zANE
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '--> Propriétés en phase mixte pour les actions long termes

        Valeur = 80885 * 10 ^ (-8)

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, 20.3, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = InertieY
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Valeur = (120 - 194) / 1000
        ValRef = zANE
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification du calcul des fleches (ELS)
        '---------------------------------------------------
        '---------------------------------------------------

        '--> Fleches due à G1

        Valeur = myPoutre.ChargesA(0).FlecheMax
        ValRef = 51.2 / 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à G2

        Valeur = myPoutre.ChargesA(1).FlecheMax
        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(0).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = 5 * 4.2 * 1000 * 14 ^ 4 / (384 * 210000 * 10 ^ 6 * InertieY)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q

        Valeur = myPoutre.ChargesA(2).FlecheMax
        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(2).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = 5 * 7.5 * 1000 * 14 ^ 4 / (384 * 210000 * 10 ^ 6 * InertieY)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleche due au retrait 

        Valeur = myPoutre.ChargesA(5).FlecheMax
        Dim NR, deltazG, Mr, deltaR As Decimal
        NR = 325 * 10 ^ (-6) * myPoutre.Section.Acier.EYoung / myPoutre.Elements(3).nEqDalle * Beff * myPoutre.Dalle.EpaisseurActive * 10 ^ 6 'N

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(3).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        deltazG = myPoutre.Dalle.Bac.Hp + myPoutre.Dalle.EpaisseurActive / 2 - zANE
        Mr = NR * deltazG
        deltaR = (Mr * myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) ^ 2) / (8 * myPoutre.Section.Acier.EYoung * 10 ^ 6 * InertieY)
        ValRef = deltaR
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Fréquence propre (ELS)
        '---------------------------------------------------
        '---------------------------------------------------

        myPoutre.Modal.Analyse(myPoutre, 0.2, myPoutre.Hivoss.IndexQ)
        Valeur = myPoutre.Modal.Frequence

        myPoutre.Section.ProprietesElastiquesMixteMyy(1, True, myPoutre.Param.Gamma, 0, myPoutre.Elements(2).nEqDalle, Beff, myPoutre.Dalle, zANE, InertieY, Mel)
        ValRef = Math.PI / 2 * Math.Sqrt(210 * InertieY * 10 ^ 8 * 9.81 / (1300 * 14 ^ 4))
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))


        Valeur = myPoutre.Modal.MassTotal
        ValRef = 18500
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

    End Sub


End Class