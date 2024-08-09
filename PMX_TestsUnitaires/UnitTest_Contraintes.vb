Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports CTICM_RDM
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Contraintes


#Region " Déclarations et attributs "

    Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
    Const DeltaVMAx As Decimal = 1 / 1000
    Dim NomCasA() As String = {"G", "G1", "G1PP", "G1C", "G2", "Q", "Conf", "SHC", "SHE", "QC"}

#End Region

#Region " Poutre acier "

    <TestMethod()> Public Sub TestContraintesSectionAcier()

        '=======================================
        '
        ' 26/10/2023 : POM
        '
        '=======================================
        '
        ' Poutre sur 2 appuis - Section acier IPE300 - Charge uniformément répartie
        '
        '=======================================

        '--> Déclaration

        Dim TU_poutre As New cls_Poutre(NomCas)

        Const Portee As Decimal = 8
        Const Q1 As Decimal = 1000
        Const GammaQ As Decimal = 1.5

        Dim SigmaCdC(,,,) As Decimal = Nothing      ' Contraintes par cas de charge
        Dim SigmaELU(,,) As Decimal = Nothing       ' Contraintes ELU
        Const lRetraitEl As Boolean = False
        Dim Med(,) As Decimal = Nothing             ' Moments aux ELU
        Const iCombi As Integer = 0
        Dim Wel As Decimal
        Dim Mmax, Mmin As Decimal
        Dim iNodeMax, iNodeMin As Integer

        '--> Initialisation - Préparation de la poutre test

        TU_poutre.lTraveeConsoleGauche = False
        TU_poutre.lTraveeConsoleDroite = False

        TU_poutre.LongueurTravee(1) = Portee

        '# IPE 300

        TU_poutre.Section.ProfilA.ha = 0.3
        TU_poutre.Section.ProfilA.Bfi = 0.15
        TU_poutre.Section.ProfilA.Bfs = 0.15
        TU_poutre.Section.ProfilA.Tfi = 0.0107
        TU_poutre.Section.ProfilA.Tfs = 0.0107
        TU_poutre.Section.ProfilA.Tw = 0.0071
        TU_poutre.Section.ProfilA.Rci = 0.015
        TU_poutre.Section.ProfilA.Rcs = 0.015
        TU_poutre.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '# Chargement Q1

        TU_poutre.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, Q1, Portee, Q1, 0))

        '# Combinaisons ELU

        TU_poutre.lCombELU(0) = False
        TU_poutre.lCombELU(1) = False
        TU_poutre.lCombELU(2) = False
        TU_poutre.lCombELU(3) = True
        TU_poutre.lCombELU(4) = False

        TU_poutre.CoefCombELU(3)(0) = 0
        TU_poutre.CoefCombELU(3)(1) = 1.5
        TU_poutre.CoefCombELU(3)(2) = 0
        TU_poutre.CoefCombELU(3)(3) = 0
        TU_poutre.CoefCombELU(3)(4) = 0

        '--> Calculs

        TU_poutre.InitialiseCalculs(NomCasA)
        TU_poutre.AAA_CalculMNVInternesN()
        TU_poutre.InitialiseCombiA(cls_Poutre.nbCombELU, TU_poutre.lCombELU, TU_poutre.CoefCombELU, "ELU", TU_poutre.CombiA_ELU)

        TU_poutre.CombiA_ELU.CombineMoments(iCombi, TU_poutre.Nodes.nbNodes, TU_poutre.ChargesA, Med, lRetraitEl)

        TU_poutre.PtsSigma.Initialise(TU_poutre)
        TU_poutre.PtsSigma.CalculContraintesCharges(TU_poutre, 1, SigmaCdC)
        TU_poutre.CombiA_ELU.CombineContraintes(iCombi, TU_poutre.ChargesA.Count, TU_poutre.PtsSigma.zPos.Count, TU_poutre.Nodes.nbNodes,
                                                        TU_poutre.ChargesA, Med, SigmaCdC, SigmaCdC, lRetraitEl, SigmaELU)

        '--> Vérification du moment max et des contraintes correspondantes

        Dim MmaxRef As Decimal = Q1 * Portee ^ 2 / 8
        MmaxRef = 8000

        TU_poutre.ChargesA(1).EnveloppesMoments(Mmax, iNodeMax, Mmin, iNodeMin)

        Assert.IsTrue(IsEqual(Mmax, MmaxRef))

        Dim SigmaSup As Decimal = SigmaELU(0, iNodeMax, 0)
        Dim SigmaInf As Decimal = SigmaELU(4, iNodeMax, 0)

        Wel = 557.1

        Dim SigmaRef As Decimal = GammaQ * Q1 * Portee ^ 2 / 8 / Wel

        Assert.IsTrue(IsEqual(SigmaSup, -SigmaRef))
        Assert.IsTrue(IsEqual(SigmaInf, SigmaRef))

    End Sub

#End Region





End Class