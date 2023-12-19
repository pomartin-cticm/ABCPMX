Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_cls_Eurocodes

#Region " Déversement "

    <TestMethod()> Public Sub Test_FonctionsDeversementLamine()

        Dim myProfil As New cls_ProfilA
        Dim EN1993 As New cls_Eurocodes
        Dim Valeur, ValRef As Decimal

        myProfil.GenereProfileIPE300()


        Valeur = EN1993.ReductionDeversement(EN1993.GetAlphaLTFromProfil(myProfil), 0.455)
        ValRef = 0.904

        Assert.IsTrue(Valeur, ValRef)

    End Sub


#End Region

#Region " Voilement par cisaillement "


    <TestMethod()> Public Sub Test_FonctionsShearBuckling()

        Dim EN1993 As New cls_Eurocodes
        Dim Valeur, ValRef As Decimal


        Valeur = EN1993.ReductionShearBuckling(2.73, 1, False)
        ValRef = 0.35

        Assert.IsTrue(Valeur, ValRef)

        Valeur = EN1993.ReductionShearBuckling(2.73, 1, True)
        ValRef = 0.399

        Assert.IsTrue(Valeur, ValRef)


    End Sub


#End Region

End Class