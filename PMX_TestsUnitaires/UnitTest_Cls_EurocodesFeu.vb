Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_EurocodesFeu

    ''' <summary>
    ''' Test de la méthode renvoyant le coefficient de reduction a prendre en compte pour fy
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_ReducFyAcier()

        Dim TempA, ReducFyAcier, ReducFyAcierRef, TauReduc, TauRef As Decimal
        Dim EurocodeFeu As New cls_EurocodesFeu

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducFyAcierRef = 1.0
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducFyAcierRef = 1.0
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducFyAcierRef = 1.0
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducFyAcierRef = 0.47
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducFyAcierRef = 0.398
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducFyAcierRef = 0.23
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducFyAcierRef = 0.02
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducFyAcierRef = 0.006
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef) / ReducFyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducFyAcierRef = 0
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducFyAcierRef = 0
        ReducFyAcier = EurocodeFeu.ReducFyAcier(TempA)
        TauReduc = Math.Abs((ReducFyAcier - ReducFyAcierRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

    End Sub

    ''' <summary>
    ''' Test de la méthode renvoyant le coefficient de réduction à prendre en compte pour E
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_ReducEyAcier()

        Dim TempA, ReducEyAcier, ReducEyAcierRef, TauReduc, TauRef As Decimal
        Dim EurocodeFeu As New cls_EurocodesFeu

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducEyAcierRef = 1.0
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducEyAcierRef = 1.0
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducEyAcierRef = 1.0
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducEyAcierRef = 0.31
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducEyAcierRef = 0.256
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducEyAcierRef = 0.13
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducEyAcierRef = 0.0225
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducEyAcierRef = 0.00675
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef) / ReducEyAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducEyAcierRef = 0
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducEyAcierRef = 0
        ReducEyAcier = EurocodeFeu.ReducEyAcier(TempA)
        TauReduc = Math.Abs((ReducEyAcier - ReducEyAcierRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

    End Sub

    ''' <summary>
    ''' Test de la méthode renvoyant le coefficient de réduction à prendre en compte pour fu
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_ReducFuAcier()

        Dim TempA, ReducFuAcier, ReducFuAcierRef, TauReduc, TauRef As Decimal
        Dim EurocodeFeu As New cls_EurocodesFeu

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducFuAcierRef = 1.25
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducFuAcierRef = 1.25
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducFuAcierRef = 1.25
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducFuAcierRef = 0.47
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducFuAcierRef = 0.398
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducFuAcierRef = 0.23
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducFuAcierRef = 0.02
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducFuAcierRef = 0.006
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef) / ReducFuAcierRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducFuAcierRef = 0
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducFuAcierRef = 0
        ReducFuAcier = EurocodeFeu.ReducFuAcier(TempA)
        TauReduc = Math.Abs((ReducFuAcier - ReducFuAcierRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

    End Sub

    ''' <summary>
    ''' Test de la méthode renvoyant le coefficient de réduction à prendre en compte pour fck
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_ReducFckBeton()

        Dim TempA, ReducFckBeton, ReducFckBetonRef, TauReduc, TauRef As Decimal
        Dim EurocodeFeu As New cls_EurocodesFeu
        Dim lBetonLeger As Boolean = True
        Dim lBetonNormal As Boolean = False

        '-------------------------------
        '-------------------------------

        ' TEST AVEC DU BETON NORMAL 

        '-------------------------------
        '-------------------------------

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducFckBetonRef = 1.0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducFckBetonRef = 1.0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducFckBetonRef = 1.0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducFckBetonRef = 0.45
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducFckBetonRef = 0.405
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducFckBetonRef = 0.3
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducFckBetonRef = 0.01
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducFckBetonRef = 0.003
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducFckBetonRef = 0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducFckBetonRef = 0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonNormal)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)










        '-------------------------------
        '-------------------------------

        ' TEST AVEC DU BETON LEGER 

        '-------------------------------
        '-------------------------------

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducFckBetonRef = 1.0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducFckBetonRef = 1.0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducFckBetonRef = 1.0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducFckBetonRef = 0.64
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducFckBetonRef = 0.604
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducFckBetonRef = 0.52
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducFckBetonRef = 0.04
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducFckBetonRef = 0.012
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef) / ReducFckBetonRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducFckBetonRef = 0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducFckBetonRef = 0
        ReducFckBeton = EurocodeFeu.ReducFckBeton(TempA, lBetonLeger)
        TauReduc = Math.Abs((ReducFckBeton - ReducFckBetonRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

    End Sub

    ''' <summary>
    ''' Test de la méthode renvoyant le coefficient de réduction à prendre en compte pour fck
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_ReducFskArmatures()

        Dim TempA, ReducFskArmatures, ReducFskArmaturesRef, TauReduc, TauRef As Decimal
        Dim EurocodeFeu As New cls_EurocodesFeu
        Dim lArmaturesFormeesAFroid As Boolean = True
        Dim lArmaturesLamineesAChaud As Boolean = False

        '-------------------------------
        '-------------------------------

        ' TEST AVEC ARMATURES FORMEES A FROID

        '-------------------------------
        '-------------------------------

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducFskArmaturesRef = 1.0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducFskArmaturesRef = 1.0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducFskArmaturesRef = 1.0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducFskArmaturesRef = 0.4
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducFskArmaturesRef = 0.316
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducFskArmaturesRef = 0.12
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducFskArmaturesRef = 0.03
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducFskArmaturesRef = 0.009
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducFskArmaturesRef = 0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducFskArmaturesRef = 0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesFormeesAFroid)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)










        '-------------------------------
        '-------------------------------

        ' TEST AVEC ARMATURES LAMINEES A CHAUD

        '-------------------------------
        '-------------------------------

        TauRef = 1 / 100

        'Test quand Temp< 20°
        TempA = 12
        ReducFskArmaturesRef = 1.0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 20°
        TempA = 20
        ReducFskArmaturesRef = 1.0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 30°
        TempA = 20
        ReducFskArmaturesRef = 1.0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 600°
        TempA = 600
        ReducFskArmaturesRef = 0.47
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 630°
        TempA = 630
        ReducFskArmaturesRef = 0.398
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 700°
        TempA = 700
        ReducFskArmaturesRef = 0.23
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1100°
        TempA = 1100
        ReducFskArmaturesRef = 0.02
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1170°
        TempA = 1170
        ReducFskArmaturesRef = 0.006
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef) / ReducFskArmaturesRef)

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp = 1200°
        TempA = 1200
        ReducFskArmaturesRef = 0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

        'Test quand Temp > 1200°
        TempA = 1250
        ReducFskArmaturesRef = 0
        ReducFskArmatures = EurocodeFeu.ReducFskArmatures(TempA, lArmaturesLamineesAChaud)
        TauReduc = Math.Abs((ReducFskArmatures - ReducFskArmaturesRef)) 'Le dénominateur est égal à 0 

        Assert.IsTrue(TauReduc <= TauRef)

    End Sub

    ''' <summary>
    ''' Test de la méthode qui prépare le maillage de la dalle 
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_PrepareMaillageDalleTabulee()

        Dim EpDalle, EpTranches(), EpTranchesRef(), zTranches(), zTranchesRef(), Tau, TauRef As Decimal
        Dim nbTranches, nbTranchesRef As Integer
        Dim EurocodeFeu As New cls_EurocodesFeu
        Dim lGenerationUN As Boolean = True
        Dim lGenerationDEUX As Boolean = False

        TauRef = 1 / 10000
        EpTranches = Nothing
        zTranches = Nothing
        zTranchesRef = Nothing
        EpTranchesRef = Nothing

        '-------------------------------
        '-------------------------------

        ' TEST AVEC LA PREMIERE GENERATION

        '-------------------------------
        '-------------------------------

        'Test avec une epaisseur = 1.3 mm 

        EpDalle = 1.3 / 1000
        nbTranchesRef = 1
        EpTranchesRef = {1.3 / 1000}
        zTranchesRef = {1.3 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau<TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 4.9 mm 

        EpDalle = 4.9 / 1000
        nbTranchesRef = 1
        EpTranchesRef = {4.9 / 1000}
        zTranchesRef = {4.9 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 5.0 mm 

        EpDalle = 5 / 1000
        nbTranchesRef = 1
        EpTranchesRef = {5 / 1000}
        zTranchesRef = {5 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 5.1 mm 

        EpDalle = 5.1 / 1000
        nbTranchesRef = 2
        EpTranchesRef = {5.0 / 1000, 0.1 / 1000}
        zTranchesRef = {5.0 / 1000, 5.1 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 38 mm 

        EpDalle = 38 / 1000
        nbTranchesRef = 8
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 3 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 38 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 40 mm 

        EpDalle = 40 / 1000
        nbTranchesRef = 8
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 40 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 63 mm 

        EpDalle = 63 / 1000
        nbTranchesRef = 13
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 3 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 40 / 1000, 45 / 1000, 50 / 1000, 55 / 1000, 60 / 1000, 63 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 79 mm 

        EpDalle = 79 / 1000
        nbTranchesRef = 13
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 19 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 40 / 1000, 45 / 1000, 50 / 1000, 55 / 1000, 60 / 1000, 79 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 80 mm 

        EpDalle = 80 / 1000
        nbTranchesRef = 13
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 20 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 40 / 1000, 45 / 1000, 50 / 1000, 55 / 1000, 60 / 1000, 80 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 81 mm 

        EpDalle = 81 / 1000
        nbTranchesRef = 14
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 20 / 1000, 1 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 40 / 1000, 45 / 1000, 50 / 1000, 55 / 1000, 60 / 1000, 80 / 1000, 81 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 163 mm 

        EpDalle = 163 / 1000
        nbTranchesRef = 14
        EpTranchesRef = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 20 / 1000, 83 / 1000}
        zTranchesRef = {5.0 / 1000, 10 / 1000, 15 / 1000, 20 / 1000, 25 / 1000, 30 / 1000, 35 / 1000, 40 / 1000, 45 / 1000, 50 / 1000, 55 / 1000, 60 / 1000, 80 / 1000, 163 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationUN, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next







        '-------------------------------
        '-------------------------------

        ' TEST AVEC LA DEUXIEME GENERATION

        '-------------------------------
        '-------------------------------

        'Test avec une epaisseur = 1.3 mm 

        EpDalle = 1.1 / 1000
        nbTranchesRef = 1
        EpTranchesRef = {1.1 / 1000}
        zTranchesRef = {1.1 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next


        'Test avec une epaisseur = 2.4 mm 

        EpDalle = 2.4 / 1000
        nbTranchesRef = 1
        EpTranchesRef = {2.4 / 1000}
        zTranchesRef = {2.4 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 2.5 mm 

        EpDalle = 2.5 / 1000
        nbTranchesRef = 1
        EpTranchesRef = {2.5 / 1000}
        zTranchesRef = {2.5 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 2.7 mm 

        EpDalle = 2.7 / 1000
        nbTranchesRef = 2
        EpTranchesRef = {2.5 / 1000, 0.2 / 1000}
        zTranchesRef = {2.5 / 1000, 2.7 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 9.9 mm 

        EpDalle = 9.9 / 1000
        nbTranchesRef = 2
        EpTranchesRef = {2.5 / 1000, 7.4 / 1000}
        zTranchesRef = {2.5 / 1000, 9.9 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 10 mm 

        EpDalle = 10 / 1000
        nbTranchesRef = 2
        EpTranchesRef = {2.5 / 1000, 7.5 / 1000}
        zTranchesRef = {2.5 / 1000, 10 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 12 mm 

        EpDalle = 12 / 1000
        nbTranchesRef = 3
        EpTranchesRef = {2.5 / 1000, 7.5 / 1000, 2 / 1000}
        zTranchesRef = {2.5 / 1000, 10 / 1000, 12 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 94 mm 

        EpDalle = 94 / 1000
        nbTranchesRef = 11
        EpTranchesRef = {2.5 / 1000, 7.5 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 4 / 1000}
        zTranchesRef = {2.5 / 1000, 10 / 1000, 20 / 1000, 30 / 1000, 40 / 1000, 50 / 1000, 60 / 1000, 70 / 1000, 80 / 1000, 90 / 1000, 94 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 147 mm 

        EpDalle = 147 / 1000
        nbTranchesRef = 16
        EpTranchesRef = {2.5 / 1000, 7.5 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 7 / 1000}
        zTranchesRef = {2.5 / 1000, 10 / 1000, 20 / 1000, 30 / 1000, 40 / 1000, 50 / 1000, 60 / 1000, 70 / 1000, 80 / 1000, 90 / 1000, 100 / 1000, 110 / 1000, 120 / 1000, 130 / 1000, 140 / 1000, 147 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 150 mm 

        EpDalle = 150 / 1000
        nbTranchesRef = 16
        EpTranchesRef = {2.5 / 1000, 7.5 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000}
        zTranchesRef = {2.5 / 1000, 10 / 1000, 20 / 1000, 30 / 1000, 40 / 1000, 50 / 1000, 60 / 1000, 70 / 1000, 80 / 1000, 90 / 1000, 100 / 1000, 110 / 1000, 120 / 1000, 130 / 1000, 140 / 1000, 150 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test avec une epaisseur = 230 mm 

        EpDalle = 230 / 1000
        nbTranchesRef = 16
        EpTranchesRef = {2.5 / 1000, 7.5 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 90 / 1000}
        zTranchesRef = {2.5 / 1000, 10 / 1000, 20 / 1000, 30 / 1000, 40 / 1000, 50 / 1000, 60 / 1000, 70 / 1000, 80 / 1000, 90 / 1000, 100 / 1000, 110 / 1000, 120 / 1000, 130 / 1000, 140 / 1000, 230 / 1000}

        EurocodeFeu.PrepareMaillageDalleTabulee(EpDalle, lGenerationDEUX, nbTranches, EpTranches, zTranches)

        Assert.IsTrue(nbTranches = nbTranchesRef)

        Assert.IsTrue(EpTranches.Length = EpTranchesRef.Length)
        For i As Integer = 0 To EpTranches.Length - 1
            Tau = Math.Abs((EpTranches(i) - EpTranchesRef(i)) / EpTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        Assert.IsTrue(zTranches.Length = zTranchesRef.Length)
        For i As Integer = 0 To zTranches.Length - 1
            Tau = Math.Abs((zTranches(i) - zTranchesRef(i)) / zTranches(i))
            Assert.IsTrue(Tau < TauRef)
        Next


    End Sub

    ''' <summary>
    ''' Test de la méthode qui renvoi la temperature de la dalle
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_TemperatureDalleTabulee()
        Dim EpDalle, EpTranches(), TempDalle(), TempDalleRef(), TimeStep, Tau, TauRef As Decimal
        Dim nbTranches As Integer
        Dim EurocodeFeu As New cls_EurocodesFeu
        Dim lGenerationUN As Boolean = True
        Dim lGenerationDEUX As Boolean = False

        TauRef = 1 / 10000

        '-------------------------------
        '-------------------------------

        ' TEST AVEC LA PREMIERE GENERATION

        '-------------------------------
        '-------------------------------

        '--> on définit une dalle de 163 mm d'épaisseur 

        EpDalle = 163 / 1000
        nbTranches = 14
        EpTranches = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 20 / 1000, 83 / 1000}

        'Test R30

        TimeStep = 30

        TempDalleRef = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110, 80, 60}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R60

        TimeStep = 60

        TempDalleRef = {705, 642, 581, 525, 469, 421, 374, 327, 289, 250, 200, 175, 140, 100}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R90

        TimeStep = 90

        TempDalleRef = {1200, 738, 681, 627, 571, 519, 473, 428, 387, 345, 294, 271, 220, 160}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R120

        TimeStep = 120

        TempDalleRef = {1200, 1200, 754, 697, 642, 591, 542, 493, 454, 415, 369, 342, 270, 210}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R180

        TimeStep = 180

        TempDalleRef = {1200, 1200, 1200, 1200, 738, 689, 635, 590, 549, 508, 469, 430, 330, 260}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R240

        TimeStep = 240

        TempDalleRef = {1200, 1200, 1200, 1200, 1200, 740, 700, 670, 645, 550, 520, 495, 395, 305}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next


        '--> On définit une dalle de  57 mm d'épaisseur 

        EpDalle = 57 / 1000
        nbTranches = 12
        EpTranches = {5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 5.0 / 1000, 2.0 / 1000}


        'Test R30

        TimeStep = 30

        TempDalleRef = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R60

        TimeStep = 60

        TempDalleRef = {705, 642, 581, 525, 469, 421, 374, 327, 289, 250, 200, 175}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R90

        TimeStep = 90

        TempDalleRef = {1200, 738, 681, 627, 571, 519, 473, 428, 387, 345, 294, 271}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R120

        TimeStep = 120

        TempDalleRef = {1200, 1200, 754, 697, 642, 591, 542, 493, 454, 415, 369, 342}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R180

        TimeStep = 180

        TempDalleRef = {1200, 1200, 1200, 1200, 738, 689, 635, 590, 549, 508, 469, 430}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R240

        TimeStep = 240

        TempDalleRef = {1200, 1200, 1200, 1200, 1200, 740, 700, 670, 645, 550, 520, 495}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationUN, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next


        '-------------------------------
        '-------------------------------

        ' TEST AVEC LA DEUXIEME GENERATION

        '-------------------------------
        '-------------------------------

        '--> on définit une dalle de 230 mm d'épaisseur 

        EpDalle = 230 / 1000
        nbTranches = 16
        EpTranches = {2.5 / 1000, 7.5 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 90 / 1000}

        'Test R30

        TimeStep = 30

        TempDalleRef = {675, 513, 363, 260, 187, 135, 101, 76, 59, 46, 37, 31, 27, 24, 23, 22}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R60

        TimeStep = 60

        TempDalleRef = {831, 684, 531, 418, 331, 263, 209, 166, 133, 108, 89, 73, 61, 51, 44, 38}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R90

        TimeStep = 90

        TempDalleRef = {912, 777, 629, 514, 423, 349, 290, 241, 200, 166, 138, 117, 100, 86, 74, 65}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R120

        TimeStep = 120

        TempDalleRef = {967, 842, 698, 583, 491, 415, 352, 300, 256, 218, 186, 159, 137, 119, 105, 94}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R180

        TimeStep = 180

        TempDalleRef = {1042, 932, 797, 685, 591, 514, 448, 392, 344, 303, 267, 236, 209, 186, 166, 149}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R240

        TimeStep = 240

        TempDalleRef = {1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next



        '--> on définit une dalle de 94 mm d'épaisseur 

        EpDalle = 94 / 1000
        nbTranches = 11
        EpTranches = {2.5 / 1000, 7.5 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 10 / 1000, 4 / 1000}

        'Test R30

        TimeStep = 30

        TempDalleRef = {675, 513, 363, 260, 187, 135, 101, 76, 59, 46, 37}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R60

        TimeStep = 60

        TempDalleRef = {831, 684, 531, 418, 331, 263, 209, 166, 133, 108, 89}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R90

        TimeStep = 90

        TempDalleRef = {912, 777, 629, 514, 423, 349, 290, 241, 200, 166, 138}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R120

        TimeStep = 120

        TempDalleRef = {967, 842, 698, 583, 491, 415, 352, 300, 256, 218, 186}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R180

        TimeStep = 180

        TempDalleRef = {1042, 932, 797, 685, 591, 514, 448, 392, 344, 303, 267}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next

        'Test R240

        TimeStep = 240

        TempDalleRef = {1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200}

        EurocodeFeu.TemperatureDalleTabulee(TimeStep, lGenerationDEUX, nbTranches, TempDalle)

        Assert.IsTrue(TempDalle.Length = TempDalleRef.Length)
        For i As Integer = 0 To TempDalle.Length - 1
            Tau = Math.Abs((TempDalle(i) - TempDalleRef(i)) / TempDalle(i))
            Assert.IsTrue(Tau < TauRef)
        Next
    End Sub

End Class