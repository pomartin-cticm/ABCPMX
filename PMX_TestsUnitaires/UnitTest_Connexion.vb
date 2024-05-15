Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2
Imports PMXInterface

<TestClass()> Public Class UnitTest_Connexion


#Region " Déclarations et attributs "

    Dim NomCharges() As String = {"G1", "G2", "Q", "QC"}

#End Region

#Region " TU pour le degré min de connexion "

    <TestMethod()> Public Sub TU_DegreMinConnexion()

        Dim EtaMin, Valref As Decimal

        'Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "TU", NomCharges)
        NomChargements = NomCharges

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
        LogicielOptions.Gamma.GammaS_fi = 1
        LogicielOptions.Gamma.GammaC_fi = 1
        'LogicielOptions.Gamma.GammaS_fi = 1
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

        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "TU", LogicielOptions, OptionsCalcul)

        ReDim myPoutre.VerifMixte(0)
        myPoutre.VerifMixte(0) = New cls_VerificationsMixtes()

        EtaMin = myPoutre.VerifMixte(0).EtaMinEqualFlanges(235, 10)
        Valref = 0.4

        Assert.IsTrue(IsEqual(EtaMin, valref))

        EtaMin = myPoutre.VerifMixte(0).EtaMinInEqualFlanges3(275, 10)
        Valref = 0.8064

        Assert.IsTrue(IsEqual(EtaMin, Valref))


    End Sub

#End Region



End Class