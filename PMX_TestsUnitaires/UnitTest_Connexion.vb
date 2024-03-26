Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Connexion


#Region " Déclarations et attributs "

    Dim NomCharges() As String = {"G1", "G2", "Q", "QC"}

#End Region

#Region " TU pour le degré min de connexion "

    <TestMethod()> Public Sub TU_DegreMinConnexion()

        Dim EtaMin, Valref As Decimal

        'Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "TU", NomCharges)
        NomChargements = NomCharges
        Dim myPoutre As New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, "TU", New Struc_OptionsLogiciel, New Struc_OptionsCalcul)

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