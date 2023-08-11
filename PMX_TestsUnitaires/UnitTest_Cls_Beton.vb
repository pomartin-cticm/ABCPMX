Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Beton

    <TestMethod()> Public Sub TestUnit_Method_CoefficientEquivalenceCas1()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des fonctions de calcul des coefficients d'équivalence acier-béton C25 et RH 50%
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyBet As New Cls_Beton
        Dim n0 As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim RH, h0, PsiL As Decimal
        Dim time_t, time_t0 As Decimal

        '--> Initialisation

        MyBet.Classe = Cls_Beton.TabClasseBeton(1)      '"C25/30"
        MyBet.lLeger = False
        MyBet.Calcul_Proprietes()

        '--> Test des propriétés du béton

        '# Fck

        ValRef = 25
        DeltaV = (MyBet.Fck - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Fcm 

        ValRef = 25 + 8
        DeltaV = (MyBet.Fcm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Ecm

        ValRef = 31476
        DeltaV = (MyBet.Ecm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# fctm

        ValRef = 2.565
        DeltaV = (MyBet.Fctm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Test valeur court terme du coefficient d'équivalence

        n0 = MyBet.CoefficientEquivalence(80, 0.1, 1, 1, 0)

        ValRef = 6.6718
        DeltaV = (n0 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des paramètres intermédiaires

        RH = 50
        PsiL = 1.1
        h0 = 0.062
        time_t = 18250
        time_t0 = 30

        '# PhiRH

        ValRef = 2.263
        DeltaV = (MyBet.PhiRH(RH, h0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Betafcm

        ValRef = 2.925
        DeltaV = (MyBet.BetaFcm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Beta t0

        ValRef = 0.482
        DeltaV = (MyBet.Beta_t0(time_t0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# BetaH

        ValRef = 343
        DeltaV = (MyBet.BetaH(RH, h0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# BetaC(t,t0)

        ValRef = 0.9944
        DeltaV = (MyBet.BetaC_tt0(RH, h0, time_t, time_t0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# n

        ValRef = 29.96
        DeltaV = (MyBet.CoefficientEquivalence(RH, h0, time_t, time_t0, PsiL) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)


    End Sub

    <TestMethod()> Public Sub TestUnit_Method_CoefficientEquivalenceCas2()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des fonctions de calcul des coefficients d'équivalence acier-béton (C40 et RH 80%)
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyBet As New Cls_Beton
        Dim n0 As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim RH, h0, PsiL As Decimal
        Dim time_t, time_t0 As Decimal

        '--> Initialisation

        MyBet.Classe = Cls_Beton.TabClasseBeton(4)      '"C40/30"
        MyBet.lLeger = False
        MyBet.Calcul_Proprietes()

        '--> Test des propriétés du béton

        '# Fck

        ValRef = 40
        DeltaV = (MyBet.Fck - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Fcm 

        ValRef = 40 + 8
        DeltaV = (MyBet.Fcm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Ecm

        ValRef = 35220
        DeltaV = (MyBet.Ecm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# fctm

        ValRef = 3.509
        DeltaV = (MyBet.Fctm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Test valeur court terme du coefficient d'équivalence

        n0 = MyBet.CoefficientEquivalence(80, 0.1, 1, 1, 0)

        ValRef = 5.9624
        DeltaV = (n0 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des paramètres intermédiaires

        RH = 80
        PsiL = 1.1
        h0 = 0.062
        time_t = 18250
        time_t0 = 25

        '# PhiRH

        ValRef = 1.319
        DeltaV = (MyBet.PhiRH(RH, h0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Betafcm

        ValRef = 2.425
        DeltaV = (MyBet.BetaFcm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Beta t0

        ValRef = 0.499
        DeltaV = (MyBet.Beta_t0(time_t0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# BetaH

        ValRef = 351.1
        DeltaV = (MyBet.BetaH(RH, h0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# BetaC(t,t0)

        ValRef = 0.9943
        DeltaV = (MyBet.BetaC_tt0(RH, h0, time_t, time_t0) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# n

        ValRef = 16.37
        DeltaV = (MyBet.CoefficientEquivalence(RH, h0, time_t, time_t0, PsiL) - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub


    <TestMethod()> Public Sub TestUnit_BetonLeger()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   11/08/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un béton léger
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyBet As New Cls_Beton
        Dim n0 As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim RH, h0, PsiL As Decimal
        Dim time_t, time_t0 As Decimal

        '--> Initialisation

        MyBet.Classe = Cls_Beton.TabClasseBetonLeger(2)      '"LC30/33"
        MyBet.lLeger = True
        MyBet.RhoC = 1500
        MyBet.Calcul_Proprietes()

        '--> Test des propriétés du béton

        '# Fck

        ValRef = 30
        DeltaV = (MyBet.Fck - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Fcm 

        ValRef = 30 + 8
        DeltaV = (MyBet.Fcm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Ecm

        ValRef = 15265
        DeltaV = (MyBet.Ecm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# fctm

        ValRef = 2.343
        DeltaV = (MyBet.Fctm - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

End Class