Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXInterface.Mod_Declarations

Imports PMXMoteur2


<TestClass()> Public Class TU_MV_PoutresAcier

#Region " Variables communes "

    Dim gNomChargesA() As String = {"Permanent loads",
                                    "Self-weight",
                                    "Self weight with props", "Self weight without props",
                                    "Other permanent loads",
                                    "Live loads", "Conf. no",
                                    "Shrinkage of the slab", "Shrinkage of the encasement",
                                    "Construction loads"}

#End Region


    <TestMethod()> Public Sub TU_MV_TESTS01_PoutreAcierLamineConsole()

        ' Vérification d'une poutre acier seule (cas test de base)
        ' Vérification selon l'EN 1993-1-1

#Region " Initialisation de la poutre "

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre()
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000   ' Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100    ' Valeur utilisée pour comparer les valeurs des critères 

        myPoutre.Initialise_CoefficientsCombinaisons()

#End Region

#Region " Renseignement des données "

        myPoutre.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE
        myPoutre.lTraveeConsoleGauche = True
        myPoutre.lTraveeConsoleDroite = True
        myPoutre.LongueurTravee(0) = 3      ' console gauche
        myPoutre.LongueurTravee(1) = 12     ' travée centrale
        myPoutre.LongueurTravee(2) = 3      ' console droite
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 3
        myPoutre.EntraxeD2 = 3

        myPoutre.Section.ProfilA.GenereProfileIPE500()

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MATERIAUX
        myPoutre.Section.Acier.InitialiseAcierS275EC3()

        '# CHARGES
        myPoutre.InitialisePoidsPropres() 'Valeur calculée à la main: qPP = 9.72 kN/ml
        For i As Integer = 0 To 2
            myPoutre.ChargesU("G1").QSurf(i) = 2 * 1000
            myPoutre.ChargesU("Q1").QSurf(i) = 3 * 1000
        Next

        '# COEFFICIENTS PARTIELS
        myPoutre.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myPoutre.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myPoutre.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myPoutre.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myPoutre.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 

        With myPoutre.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

        myPoutre.Param.EtaW = 1
#End Region

#Region " Lancement des calculs "

        Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA

        strRacineELU = "ULS"
        strRacineELS = "SLS"
        strRacineELF = "FLS"
        strRacineELUC = "ULS_C"
        strRacineELSC = "SLS_C"

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myPoutre.Section.TypeSection
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
        Dim MEd1(,) As Decimal = Nothing
        Dim MEd2(,) As Decimal = Nothing
        Dim MEd3(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMax2, MEdMin2, iNodeMMin2, iNodeMMax2 As Decimal
        Dim MEdMax3, MEdMin3, iNodeMMin3, iNodeMMax3 As Decimal
        Dim MEdAppuiMax, MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEd2(,) As Decimal = Nothing
        Dim VEd3(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdMax2, VEdMin2, iNodeVMin2, iNodeVMax2 As Decimal
        Dim VEdMax3, VEdMin3, iNodeVMin3, iNodeVMax3 As Decimal
        Dim VEdAppui As Decimal

        myPoutre.CombiA_ELU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELU.CombineMoments(1, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd2, False)   ' Combinaison des moments pour la combinaison 1
        myPoutre.CombiA_ELU.CombineMoments(2, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd3, False)   ' Combinaison des moments pour la combinaison 2
        myPoutre.CombiA_ELU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(1, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd2, False)  ' Combinaison des tranchants pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(2, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd3, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myPoutre.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(MEd2, myPoutre.Nodes.nbNodes, MEdMax2, MEdMin2, iNodeMMax2, iNodeMMin2)
        EnveloppeTableauEfforts(MEd3, myPoutre.Nodes.nbNodes, MEdMax3, MEdMin3, iNodeMMax3, iNodeMMin3)
        EnveloppeTableauEfforts(VEd1, myPoutre.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)
        EnveloppeTableauEfforts(VEd2, myPoutre.Nodes.nbNodes, VEdMax2, VEdMin2, iNodeVMax2, iNodeVMin2)
        EnveloppeTableauEfforts(VEd3, myPoutre.Nodes.nbNodes, VEdMax3, VEdMin3, iNodeVMax3, iNodeVMin3)

        MEdAppuiMax = Math.Max(MEdMax1, Math.Max(MEdMax2, MEdMax3))
        MEdMiTravee = Math.Min(MEdMin1, Math.Min(MEdMin2, MEdMin3))
        VEdAppui = Math.Max(VEdMax1, Math.Max(VEdMax2, VEdMax3))

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, False)

#End Region

#Region " VALIDATION : Analyse de la poutre "

        Valeur = MEdAppuiMax
        ValRef = 529.497 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = MEdMiTravee
        ValRef = -156.249 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdAppui
        ValRef = 208.332 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region " VALIDATION : Classification de la section (ELU)"

        Valeur = myPoutre.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region " VALIDATION : Résistance à la flexion (ELU)"

        'A L'ELU

        'Propsection: Wpl = 2 197.652 cm3 

        Dim zANE, MRk As Decimal
        myPoutre.Section.ProprietesPlastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, MRk)

        Valeur = MRk
        ValRef = 604.3543 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifAcier(0).CritereM.Resistance(iNodeMMax1)
        ValRef = 604.3543 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifAcier(0).CritereM.CritereMax
        ValRef = 0.876 '529.497 / 604.354 = 0.876
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region " VALIDATION : Résistance au déversement"

        Dim Mcr, MbRd As Decimal

        Mcr = 170.57 * 1000
        MbRd = 85.19 * 1000

        Valeur = myPoutre.VerifAcier(0).McrLTB(0, 1)
        ValRef = Mcr
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Dim lambda, alpha, phi, khi As Decimal

        lambda = Math.Sqrt(MRk / Mcr)
        alpha = 0.34
        phi = 0.5 * (1 + alpha * (lambda - 0.2) + lambda ^ 2)
        khi = 1 / (phi + Math.Sqrt(phi ^ 2 - lambda ^ 2))
        MbRd = khi * MRk

        Valeur = myPoutre.VerifAcier(0).CritereLTB.Resistance(1)
        ValRef = MbRd ' = 85.19 kN (dans l'article, on a 492.5 kN.m)
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé acier seul

#End Region

#Region " VALIDATION : Résistance à l'effort tranchant (ELU)"

        'A L'ELU

        Valeur = myPoutre.Section.VplRd(myPoutre.Param.Gamma.GammaM0, myPoutre.Param.EtaW)
        ValRef = 950.564 * 1000 'Av  = 5 987 cm2 VplRd = 950.564 kN
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à l'effort tranchant

        Valeur = myPoutre.VerifAcier(0).CritereV.CritereMax
        ValRef = 0.2191667 '208.332/950.564 = 0.219.7
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELU As Decimal
        If ValRef <= 0.5 Then
            rhoVELU = 0
        ElseIf ValRef >= 1 Then
            rhoVELU = 1
        Else
            rhoVELU = (2 * 0.2191667 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
        End If

#End Region

#Region " VALIDATION : Résistance au voilement (ELU)"

        Assert.IsTrue(myPoutre.Section.IsVoilementParCisaillement(myPoutre.Param.EtaW) = False) '--> Vérification de la résistance au voilement non nécessaire 
#End Region

#Region " VALIDATION : Résistance à l'interaction MV"

        '--> Sans objet, on doit retrouver les mêmes résultats que pour la résistance à la flexion simple

        myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, rhoVELU, zANE, MRk)

        Valeur = MRk
        ValRef = 604.3543 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifAcier(0).CritereMV.Resistance(iNodeMMax1)
        ValRef = 604.3543 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (834.6 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifAcier(0).CritereMV.CritereMax
        ValRef = 0.876 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region " VALIDATION : Propriétés élastiques (ELS)"

        '--> Propriétés en phase de coulage, poutre non etayée

        Valeur = 48279 ' IY = 48 279.445 cm4
        ValRef = myPoutre.Section.ProfilA.InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Dim InertieY, Mel As Decimal

        'myPoutre.Section.ProprietesElastiquesAcierMyy(1, myPoutre.Param.Gamma, zANE, InertieY, MRk)
        'myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, MRk, True)
        'ValRef = InertieY * 10 ^ 8
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

#End Region

#Region " VALIDATION : Calcul des fleches (ELS)"

        '--> Fleches due à G1

        Valeur = myPoutre.ChargesA(0).FlecheMax * 1000
        ValRef = 29.35
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à Q1 #1

        Valeur = myPoutre.ChargesA(1).FlecheMax * 1000
        ValRef = 16.8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q1 #2

        Valeur = myPoutre.ChargesA(2).FlecheMax * 1000
        ValRef = 24.01
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q1 #3

        Valeur = myPoutre.ChargesA(3).FlecheMax * 1000
        ValRef = 8.103
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

#Region " VALIDATION : Fréquence propre (ELS)"

        myPoutre.Modal.Analyse(myPoutre, 0.2, myPoutre.Hivoss.IndexQ)
        Valeur = myPoutre.Modal.Frequence

        ValRef = 2.332 'Calcul réalisé avec RDM7 (/!\ le logiciel prend automatiquement en compte la masse du profilé, il faut le retirer, ce qui nous donne (15.72 - 0.769 + 0.2*9)*1000/9.81 = 1 707,5 kG/m /!\)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.Modal.MassTotal
        ValRef = 32146.8 '(15.72 + 0.2*9)*1000/9.81 * (3 + 12 + 3) = 32 146.8 kg
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

    End Sub

    <TestMethod()> Public Sub TU_MV_TESTS02_PoutreAcierPRS()

#Region " Initialisation de la poutre "

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre()
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000   ' Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100    ' Valeur utilisée pour comparer les valeurs des critères 
        Const Portee As Decimal = 15

        Dim qAdd, F As Decimal

        myPoutre.Initialise_CoefficientsCombinaisons()

#End Region

#Region " Renseignement des données "

        myPoutre.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE

        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False

        myPoutre.LongueurTravee(1) = Portee     ' travée centrale

        myPoutre.lIntermediaire = True

        myPoutre.Section.ProfilA.GenerePRS(0.3, 0.02, 0.8, 0.012)

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myPoutre.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myPoutre.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 3, True, True, False))
        myPoutre.Maintiens(1).Add(New cls_Maintiens(2 * Portee / 3, True, True, False))

        '# MATERIAUX
        myPoutre.Section.Acier.InitialiseAcierS355MML()

        '# CHARGES
        myPoutre.InitialisePoidsPropres() 'Valeur calculée à la main: qPP = 9.72 kN/ml

        qAdd = 12 * 10 ^ 3 - myPoutre.ChargesU("G1").FReparties(1)(0).Force(0)

        myPoutre.ChargesU("G1").FReparties(1).Add(New cls_ForceRepartie(0, qAdd, Portee, qAdd, 0))

        qAdd = 20 * 10 ^ 3
        myPoutre.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qAdd, Portee, qAdd, 0))

        F = 15 * 1000
        myPoutre.ChargesU("Q2").Forces(1).Add(New cls_Force(Portee / 2, F, 0))

        '# COEFFICIENTS PARTIELS
        myPoutre.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myPoutre.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myPoutre.lCombELU(1) = True                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myPoutre.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myPoutre.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myPoutre.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 

        With myPoutre.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
        End With

        myPoutre.Param.EtaW = 1.2

        myPoutre.Initialise_CoefficientsCombinaisons()

#End Region

#Region " Lancement des calculs "

        Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA

        strRacineELU = "ULS"
        strRacineELS = "SLS"
        strRacineELF = "FLS"
        strRacineELUC = "ULS_C"
        strRacineELSC = "SLS_C"

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myPoutre.Section.TypeSection
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
        Dim MEd1(,) As Decimal = Nothing
        Dim MEd2(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMax2, MEdMin2, iNodeMMin2, iNodeMMax2 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEd2(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdMax2, VEdMin2, iNodeVMin2, iNodeVMax2 As Decimal
        Dim VEdAppui As Decimal

        myPoutre.CombiA_ELU.CombineMoments(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myPoutre.CombiA_ELU.CombineMoments(1, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd2, False)   ' Combinaison des moments pour la combinaison 1
        myPoutre.CombiA_ELU.CombineEffortsT(0, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0
        myPoutre.CombiA_ELU.CombineEffortsT(1, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd2, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myPoutre.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(MEd2, myPoutre.Nodes.nbNodes, MEdMax2, MEdMin2, iNodeMMax2, iNodeMMin2)
        EnveloppeTableauEfforts(VEd1, myPoutre.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)
        EnveloppeTableauEfforts(VEd2, myPoutre.Nodes.nbNodes, VEdMax2, VEdMin2, iNodeVMax2, iNodeVMin2)

        MEdMiTravee = Math.Max(MEdMax1, MEdMax2)
        'MEdMiTravee = Math.Min(MEdMin1, MEdMin2)
        VEdAppui = Math.Max(VEdMax1, VEdMax2)

        'VERIFICATION DE LA POUTRE 

        myPoutre.VerifAcier(0).Z_VerificationELU(myPoutre, False)

#End Region

#Region " VALIDATION : Analyse de la poutre "

        Valeur = MEdMiTravee
        ValRef = 1435.5 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        Valeur = VEdAppui
        ValRef = 374.4 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region " VALIDATION : Résistance à la flexion (ELU)"

        Dim zANE, MRd As Decimal
        myPoutre.Section.ProprietesPlastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, MRd)

        Valeur = MRd
        ValRef = 2266 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))   'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifAcier(0).CritereM.Resistance(iNodeMMax1)
        ValRef = 2266 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))   'Vérification du calcul de la résistance à la flexion simple de la section

        Valeur = myPoutre.VerifAcier(0).CritereM.CritereMax
        ValRef = 0.634
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region " VALIDATION : Résistance à l'effort tranchant (ELU)"

        '-- Resistance plastique

        Valeur = myPoutre.VerifAcier(0).CritereV.Resistance(iNodeVMax1)
        ValRef = 2248.7 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))   'Vérification du calcul de la résistance à l'effort tranchant

        Valeur = myPoutre.VerifAcier(0).CritereV.CritereMax
        ValRef = 0.166
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        '-- Résistance voilement

        Valeur = myPoutre.VerifAcier(0).CritereVb.Resistance(iNodeVMax1)
        ValRef = 1565.65 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))   'Vérification du calcul de la résistance au voilement par cisaillement

        Valeur = myPoutre.VerifAcier(0).CritereVb.CritereMax
        ValRef = 0.239
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance au voilement par cisaillement

#End Region


#Region " VALIDATION : Déversement (ELU)"

        Valeur = myPoutre.VerifAcier(0).CritereLTB.CritereMax
        ValRef = 1.093
        'Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance au déversement
        Assert.IsTrue(IsEqual(Valeur, ValRef))                  'Vérification du critère de la résistance au déversement

        '-- Moment critique

        Valeur = myPoutre.VerifAcier(0).McrLTB(0, 1)
        ValRef = 4163 * 1000

        Assert.IsTrue(IsEqual(Valeur, ValRef))

#End Region


    End Sub


    <TestMethod()> Public Sub TU_MV_PoutreAcierPRS()

        ' Vérification d'un PRS (permet de véifier les formules vis-à-vis du voilement par cisaillement + les formules Cl3 + les formules quand Cl2 et calcul élastique imposé)
        ' Vérification selon l'EN 1993-1-1

        ' Vérification d'une poutre acier seule (cas test de base)
        ' Vérification selon l'EN 1993-1-1

#Region " Initialisation de la poutre"

        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        NomChargements = NomCas

        Dim myPoutre As New cls_Poutre()
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
        Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

        myPoutre.Initialise_CoefficientsCombinaisons()

#End Region

#Region " Renseignement des données"

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(0) = 0 'console gauche
        myPoutre.LongueurTravee(1) = 10 'travée centrale
        myPoutre.LongueurTravee(2) = 0 'console droite
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 3
        myPoutre.EntraxeD2 = 3

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
            .ha = (500 + 2 * 25) / 1000
            .Tw = 8 / 1000
            .Bfs = 650 / 1000
            .Bfi = .Bfs
            .Tfs = 25 / 1000
            .Tfi = .Tfs
            .Rcs = 0        'non pertinent mais permet de vérifier que ce n'est pas pris en compte dans le calcul
            .Rci = .Rcs
        End With

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        'MATERIAUX
        myPoutre.Section.Acier.InitialiseAcierS235EC3()

        'CHARGES
        myPoutre.InitialisePoidsPropres() 'Valeur calculée à la main: qPP = 11.63981020 kN/ml
        myPoutre.ChargesU("G1").Forces(1).Add(New cls_Force(5, 400 * 10 ^ 3, 0)) '100kN a mi travée 

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
        myPoutre.Param.EtaW = 1
#End Region

#Region " Lancement des calculs"

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
        Select Case myPoutre.Section.TypeSection
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

#Region " Verification de l'analyse de la poutre"

        'VERIFICATION DES EFFORTS A L'ELU

        Valeur = MEdMax
        ValRef = 1546.421797 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = VEdMax
        ValRef = 348.5687189 * 10 ^ 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

#Region "Vérification de la classification de la section (ELU)"

        Valeur = myPoutre.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, myPoutre.Param.lGeneration1)
        ValRef = 3
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

#Region "Vérification de la résistance à la flexion (ELU)"

        'A L'ELU

        'Propsection: Wel = 8 452.653 cm3 

        Dim zANE, MRk As Decimal
        'myPoutre.Section.ProprietesElastiquesAcierMyy(True, myPoutre.Param.Gamma, 0, zANE, MRk)
        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, MRk, True)

        Valeur = MRk
        ValRef = 1986.373455 * 1000 'MelRd
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.VerifAcier(0).CritereSigmaA.Action(iNodeMMax)
        ValRef = 182.9510781
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.VerifAcier(0).CritereSigmaA.Resistance(iNodeMMax)
        ValRef = 235
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.VerifAcier(0).CritereSigmaA.CritereMax
        ValRef = 0.7785151343 '1546.421797 / 1986.373455 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion


#End Region

#Region "Vérification de la résistance au déversement"

        Dim Mcr As Decimal

        Mcr = 9556.1 * 1000
        Valeur = myPoutre.VerifAcier(0).McrLTB(0, 1)
        ValRef = Mcr
        Exit Sub
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'GUD: arret du test ici, A COMPLETER + TARD

#End Region

#Region "Vérification de la résistance à l'effort tranchant (ELU)"

        'A L'ELU

        Valeur = myPoutre.Section.VplRd(myPoutre.Param.Gamma.GammaM0, myPoutre.Param.EtaW)
        ValRef = 950.564 * 1000 'Av  = 5 987 cm2 VplRd = 950.564 kN
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx)) 'Vérification du calcul de la résistance à l'effort tranchant

        Valeur = myPoutre.VerifAcier(0).CritereV.CritereMax
        ValRef = 0.2191667 '208.332/950.564 = 0.219.7
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à l'effort tranchant

        Dim rhoVELU As Decimal
        If ValRef <= 0.5 Then
            rhoVELU = 0
        ElseIf ValRef >= 1 Then
            rhoVELU = 1
        Else
            rhoVELU = (2 * 0.2191667 - 1) ^ 2 'Valeur calculée par rapport à la valeur de référence. Sera utile pour l'interacion MV
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
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de la résistance à la flexion simple du profilé 

        Valeur = myPoutre.VerifAcier(0).CritereMV.Resistance(iNodeMMax)
        ValRef = 604.3543 * 1000 'GUD: valeur recalculée car celle de l'article ne correspond pas tout a fait (834.6 kN.m) du fait que le NConnexion n'est pas identique
        Assert.IsTrue(IsEqual(Valeur, ValRef, 2 * DeltaVMAx)) 'Vérification du calcul de la résistance à la flexion simple de la section mixte 

        Valeur = myPoutre.VerifAcier(0).CritereMV.CritereMax
        ValRef = 0.876 'GUD: valeur recalculée pour les mêmes raisons que ci-dessu. Dans l'article, le critère est égal à 0.84 
        Assert.IsTrue(Math.Abs(Valeur - ValRef) <= DeltaCMAx) 'Vérification du critère de la résistance à la flexion

#End Region

#Region "Vérification des propriétés élastiques (ELS)"

        '--> Propriétés en phase de coulage, poutre non etayée

        Valeur = 48279 ' IY = 48 279.445 cm4
        ValRef = myPoutre.Section.ProfilA.InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        Dim InertieY, Mel As Decimal

        'myPoutre.Section.ProprietesElastiquesAcierMyy(1, myPoutre.Param.Gamma, zANE, InertieY, MRk)
        'myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, zANE, InertieY, MRk, True)
        'ValRef = InertieY * 10 ^ 8
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, 0, zANE, InertieY, Mel)
        ValRef = InertieY * 10 ^ 8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx)) 'Vérification du calcul de l'inertie de la poutre seule

#End Region

#Region "Vérification du calcul des fleches (ELS)"

        '--> Fleches due à G1

        Valeur = myPoutre.ChargesA(0).FlecheMax * 1000
        ValRef = 29.35
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à Q1 #1

        Valeur = myPoutre.ChargesA(1).FlecheMax * 1000
        ValRef = 16.8
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q1 #2

        Valeur = myPoutre.ChargesA(2).FlecheMax * 1000
        ValRef = 24.01
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        '--> Fleches due à Q1 #3

        Valeur = myPoutre.ChargesA(3).FlecheMax * 1000
        ValRef = 8.103
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region

#Region "Fréquence propre (ELS)"

        myPoutre.Modal.Analyse(myPoutre, 0.2, myPoutre.Hivoss.IndexQ)
        Valeur = myPoutre.Modal.Frequence

        ValRef = 2.332 'Calcul réalisé avec RDM7 (/!\ le logiciel prend automatiquement en compte la masse du profilé, il faut le retirer, ce qui nous donne (15.72 - 0.769 + 0.2*9)*1000/9.81 = 1 707,5 kG/m /!\)
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

        Valeur = myPoutre.Modal.MassTotal
        ValRef = 32146.8 '(15.72 + 0.2*9)*1000/9.81 * (3 + 12 + 3) = 32 146.8 kg
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaCMAx))

#End Region
    End Sub


End Class