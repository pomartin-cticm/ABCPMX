Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXInterface
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_CasTestsPoutreAcier

    <TestMethod()> Public Sub TestMethod1()

        ' Vérification d'une poutre acier seule (cas test de base)
        ' Vérification selon l'EN 1993-1-1

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

#End Region

#Region "Initialisation de la poutre"

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.AcierSeul, "")
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

#End Region

#Region "Renseignement des données de l'article"

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(0) = 3 'console gauche
        myPoutre.LongueurTravee(1) = 12 'travée centrale
        myPoutre.LongueurTravee(2) = 3 'console droite
        myPoutre.lTremieGauche = True
        myPoutre.lTremieDroite = True
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 3
        myPoutre.EntraxeD2 = 3

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            .ha = 500 / 1000
            .Tw = 10.2 / 1000
            .Bfs = 200 / 1000
            .Bfi = .Bfs
            .Tfs = 16 / 1000
            .Tfi = .Tfs
            .Rcs = 21 / 1000
            .Rci = .Rcs
            '.InitialiseProprietes()
        End With

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .t_d = 120 / 1000
        End With

        'For i As Integer = 0 To myPoutre.Dalle.LitArma.Count - 1 'on ne prend pas en compte les armatures dans le calcul dans l'exemple traité 
        '    myPoutre.Dalle.LitArma(i).lActive = False
        'Next

        'With myPoutre.Dalle.Bac
        '    .Hp = 58 / 1000
        '    .h_rs = 0
        '    .Tp = 0.75 / 1000
        '    .Bb = 62 / 1000
        '    .Bt = 101 / 1000
        '    .Ep = 207 / 1000
        '    .Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
        '    .AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus 'permet de prendre en compte le bac pour le calcul des armatures transversales
        'End With

        'With myPoutre.Dalle.Connecteur
        '    .hsc = 100 / 1000
        '    .d = 19 / 1000
        'End With

        'myPoutre.NombreZones(myPoutre.IndicePremiereTravee) = 1
        'myPoutre.NombreGoujonsTransv(myPoutre.IndicePremiereTravee, 0) = 1
        'myPoutre.Espacement_Bac_TransZone(myPoutre.IndicePremiereTravee, 0) = 1
        'myPoutre.EspacementZone(myPoutre.IndicePremiereTravee, 0) = 0.207
        'myPoutre.lAutomaticDesign = False

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

        'With myPoutre.Dalle.beton
        '    .Classe = "C25/30"
        '    .Ecm = 31000
        'End With

        'myPoutre.Dalle.Connecteur.Fu = 450

        'myPoutre.Dalle.Bac.msurf = 8.53 '8.53 kg/m2
        'myPoutre.Dalle.Bac.fyp = 350

        'CHARGES
        myPoutre.InitialisePoidsPropres()
        For i As Integer = 0 To 2
            For Each elmnt In myPoutre.ChargesU("G1").FReparties(i)
                elmnt.Force(0) = 0
                elmnt.Force(1) = 0
            Next
            myPoutre.ChargesU("G1").QSurf(i) = 2 * 1000
            myPoutre.ChargesU("Q1").QSurf(i) = 3 * 1000
        Next


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
        Select Case myPoutre.TypeSection
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

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, False) 'Poutre seul durant la phase de construction
        ' myPoutre.VerifMixte(0).Z_VerificationELU(myPoutre) 'Poutre mixte

#End Region

#Region "Verification de l'analyse de la poutre"

        'VERIFICATION DES EFFORTS A L'ELU

        Valeur = MEdMax
        ValRef = 388.8 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdMax
        ValRef = 129.6 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la classification de la section (ELU)"

        Valeur = myPoutre.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la résistance à la flexion (ELU)"

        'A L'ELU

        Dim zANE, MRk As Decimal
        myPoutre.Section.ProprietesPlastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, MRk)

        Valeur = MRk
        ValRef = 604.3543 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifAcier(0).CritereM.Resistance(iNodeMMax)
        ValRef = 604.3543 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (779.4 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereM.CritereMax
        ValRef = 0.6433 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion


#End Region

#Region "Vérification de la résistance à l'effort tranchant (ELU)"

        'A L'ELU

        Valeur = myPoutre.Section.VplRd(myPoutre.Param.Gamma.GammaM0)
        ValRef = 818.015 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à l'effort tranchant

        Valeur = myPoutre.VerifAcier(0).CritereV.CritereMax
        ValRef = 0.1584
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELU As Decimal
        If ValRef <= 0.5 Then
            rhoVELU = 0
        ElseIf ValRef >= 1 Then
            rhoVELU = 1
        Else
            rhoVELU = (2 * 0.1584 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
        End If

#End Region

#Region "Vérification de la résistance au voilement (ELU)"

        Assert.IsTrue(myPoutre.Section.IsVoilementParCisaillement(myPoutre.Param.EtaW) = False) '--> Vérification de la résistance au voilement non nécessaire 
#End Region

#Region "Verification de la résistance à l'interaction MV"

        '--> Sans objet, on doit retrouver les mêmes résultats que pour la résistance à la flexion simple


        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, rhoVELU, zANE, MRk)

        Valeur = MRk
        ValRef = 604.3543 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifMixte(0).CritereMV.Resistance(iNodeMMax)
        ValRef = 604.3543 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (834.6 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifMixte(0).CritereMV.CritereMax
        ValRef = 0.6433 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region
    End Sub

End Class