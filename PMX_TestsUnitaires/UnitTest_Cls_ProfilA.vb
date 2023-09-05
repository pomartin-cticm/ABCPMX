Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_ProfilA

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierLamine()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier IPE 300
        '   Références : valeurs catalogue AM
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim zANE, Inertie, MelRd As Decimal

        '--> Initialisation

        '# IPE 300

        MyProfil.ha = 0.3
        MyProfil.Bfi = 0.15
        MyProfil.Bfs = 0.15
        MyProfil.Tfi = 0.0107
        MyProfil.Tfs = 0.0107
        MyProfil.Tw = 0.0071
        MyProfil.Rci = 0.015
        MyProfil.Rcs = 0.015
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '# Aire de cisaillement

        ValRef = 25.7 * 10 ^ (-4)
        DeltaV = (MyProfil.AireAv - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Hauteur d'âme entre semelles

        ValRef = 278.6 * 10 ^ (-3)
        DeltaV = (MyProfil.HauteurAmeHw - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Hauteur d'âme entre congés

        ValRef = 248.6 * 10 ^ (-3)
        DeltaV = (MyProfil.HauteurAmeDw - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Aire de la semelle supérieure

        ValRef = 1605 * 10 ^ (-6)
        DeltaV = (MyProfil.AireFs - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Aire de la section

        ValRef = 5380 * 10 ^ (-6)
        DeltaV = (MyProfil.Aire - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie de torsion

        ' ValRef = 20.1 * 10 ^ (-8)      ' Changement de formule
        ValRef = 19.91 * 10 ^ (-8)
        DeltaV = (MyProfil.InertieT - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 10)     '1%

        '# Inertie de gauchissement

        ValRef = 126 * 10 ^ (-9)
        DeltaV = (MyProfil.InertieW - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie de flexion YY

        MyProfil.ProprietesElastiquesMyy(1, True, 1, zANE, Inertie, MelRd)
        ValRef = 8356 * 10 ^ (-8)
        DeltaV = (Inertie - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Module de flexion élastique / yy

        ValRef = 557.0 * 10 ^ (-6)
        DeltaV = (MyProfil.ModuleFlexionElastiqueYY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Module de flexion plastique / yy

        ValRef = 628.3 * 10 ^ (-6)
        DeltaV = (MyProfil.ModuleFlexionPlastiqueYY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie de flexion zz

        MyProfil.InitialiseProprietes()
        Inertie = MyProfil.InertieZ

        ValRef = 603.8 * 10 ^ (-8)
        DeltaV = (Inertie - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 2)        ' (0,2%)

        '# Module de flexion élastique / zz

        ValRef = 80.5 * 10 ^ (-6)
        Valeur = MyProfil.ModuleWelZ
        DeltaV = (Valeur - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 2)        ' (0,2%)

    End Sub

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierMonoSym()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier IPE 300
        '   Références : section acier de l'article RCM 3/2021
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim zANE, InertieY, MelRd As Decimal

        '--> Initialisation

        '# IPE 300

        MyProfil.ha = 0.575
        MyProfil.Bfi = 0.35
        MyProfil.Bfs = 0.25
        MyProfil.Tfi = 0.04
        MyProfil.Tfs = 0.025
        MyProfil.Tw = 0.015
        MyProfil.Rci = 0.00
        MyProfil.Rcs = 0.00
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym

        '# Aire de cisaillement

        ValRef = 100 ' 76.5 * 10 ^ (-4)
        DeltaV = (MyProfil.AireAv - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Aire 

        ValRef = 279 * 10 ^ (-4)
        DeltaV = (MyProfil.Aire - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie YY

        MyProfil.ProprietesElastiquesMyy(1, True, 1, zANE, InertieY, MelRd)

        ValRef = 150394 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = -358.1 * 10 ^ (-3)
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# module élastique flexion

        ValRef = 4200.1 * 10 ^ (-6)
        DeltaV = (MyProfil.ModuleFlexionElastiqueYY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

End Class