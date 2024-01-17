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

        'InitialiseDebug()

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
        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "", NomCas)
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

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

        With myPoutre.Dalle.Bac
            .Hp = 58 / 1000
            .h_rs = 0
            .Tp = 0.75 / 1000
            .Bb = 62 / 1000
            .Bt = 101 / 1000
            .Orientation = .Enum_Orientation.Perpendiculaire
        End With

        With myPoutre.Dalle.Connecteur
            .hsc = 100 / 1000
            .d = 19 / 1000
        End With

        myPoutre.NombreZones(myPoutre.IndicePremiereTravee) = 1
        myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0) = 2
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
        myPoutre.Dalle.Bac.fyp = 450

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

        myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre)

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
        ' --> Vérification de la classification de la section 
        '---------------------------------------------------
        '---------------------------------------------------

        Valeur = myPoutre.Section.ClasseSectionCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification de la résistance des connecteurs
        '---------------------------------------------------
        '---------------------------------------------------

        Valeur = myPoutre.Dalle.Connecteur.PRdDallePleineG1G2Acier(myPoutre.Param.Gamma.GammaVs)
        ValRef = 81.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = myPoutre.Dalle.Connecteur.PRdDallePleineG1Beton(myPoutre.Dalle.beton.Fck, 31000, myPoutre.Param.Gamma.GammaVc)
        ValRef = 73.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Calcul avec 1 connecteur par onde 
        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine,
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         1, myPoutre.Dalle.beton.Fck,
                                                         31000, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 52.5 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Calcul avec 2 connecteurs par ondes
        Valeur = myPoutre.Dalle.Connecteur.ResistancePRd(myPoutre.Param.lGeneration1, myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine,
                                                         myPoutre.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire, myPoutre.Dalle.Bac,
                                                         myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0), myPoutre.Dalle.beton.Fck,
                                                         31000, myPoutre.Param.Gamma.GammaVs, myPoutre.Param.Gamma.GammaVc)
        ValRef = 37.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))



        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification du degré de connexion minimal
        '---------------------------------------------------
        '---------------------------------------------------

        Valeur = myPoutre.VerifMixte(0).DegConnexMin(myPoutre.IndicePremiereTravee)
        ValRef = 0.574
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))



        '---------------------------------------------------
        '---------------------------------------------------
        ' --> Vérification du dimensionnement de la connexion 
        '---------------------------------------------------
        '---------------------------------------------------

        Valeur = myPoutre.Section.ResistanceTractionProfile(myPoutre.Param.Gamma.GammaM0)
        ValRef = 2718 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Dim Beff As Decimal
        Dim lSimple As Boolean = True 'booléen qui indique qu'on va utiliser le modèle simplifié pour le calcul de beff
        Beff = myPoutre.BeffDalle(myPoutre.Nodes.xTravee(iNodeMMax), myPoutre.IndicePremiereTravee, lSimple, False)
        Valeur = myPoutre.Dalle.NResistanceCompressionDalle(Beff, myPoutre.Param.Gamma.GammaC)
        ValRef = 2635 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = myPoutre.VerifMixte(0).DegConnex(myPoutre.IndicePremiereTravee, 0)
        ValRef = 2 * (7 / 0.207) * 37.4 * 1000 / (2636 * 1000) '/!\ J'ai corrigé la valeur de l'article car la valeur de PRd n'est pas exactement la même du fait que la valeur de Ecm n'est pas identique
        '   (31 GPa dans l'article est directement calculée dans le logiciel) + la valeur du nombre de connecteurs n'est pas identique non plus (arrondi au premier entier inférieur dans 
        ' l'article et on garde la valeur décimale dans le logiciel). Au final, on a un eta = 0.93 dans l'article et 0.96 avec le logiciel
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
    End Sub




End Class