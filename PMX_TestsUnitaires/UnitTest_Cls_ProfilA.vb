Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_ProfilA

    '========================================================================================================================================
    '   CLASSE POUR LES PROPRIETES DES PROFILES ACIER
    '========================================================================================================================================

#Region " Profiles laminés "


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

        GenereProfileIPE300(MyProfil)

        '# Aire de cisaillement

        ValRef = 25.7 * 10 ^ (-4)

        Assert.IsTrue(IsEqual(MyProfil.AireAv, ValRef))

        '# Hauteur d'âme entre semelles

        ValRef = 278.6 * 10 ^ (-3)

        Assert.IsTrue(IsEqual(MyProfil.HauteurAmeHw, ValRef))

        '# Hauteur d'âme entre congés

        ValRef = 248.6 * 10 ^ (-3)

        Assert.IsTrue(IsEqual(MyProfil.HauteurAmeDw, ValRef))

        '# Aire de la semelle supérieure

        ValRef = 1605 * 10 ^ (-6)

        Assert.IsTrue(IsEqual(MyProfil.AireFs, ValRef))

        '# Aire de la section

        ValRef = 5380 * 10 ^ (-6)

        Assert.IsTrue(IsEqual(MyProfil.Aire, ValRef))

        '# Inertie de torsion

        ' ValRef = 20.1 * 10 ^ (-8)      ' Changement de formule
        ValRef = 19.91 * 10 ^ (-8)

        Assert.IsTrue(IsEqual(MyProfil.InertieT, ValRef, DeltaVMAx * 10)) '1%

        '# Inertie de gauchissement

        ValRef = 126 * 10 ^ (-9)

        Assert.IsTrue(IsEqual(MyProfil.InertieW, ValRef))

        '# Inertie de flexion YY

        MyProfil.ProprietesElastiquesMyy(1, True, 1, zANE, Inertie, MelRd)
        ValRef = 8356 * 10 ^ (-8)

        Assert.IsTrue(IsEqual(Inertie, ValRef))

        '# Module de flexion élastique / yy

        ValRef = 557.0 * 10 ^ (-6)

        Assert.IsTrue(IsEqual(MyProfil.ModuleFlexionElastiqueYY, ValRef))

        '# Module de flexion plastique / yy

        ValRef = 628.3 * 10 ^ (-6)

        Assert.IsTrue(IsEqual(MyProfil.ModuleFlexionPlastiqueYY, ValRef))

        '# Inertie de flexion zz

        MyProfil.InitialiseProprietes()
        Inertie = MyProfil.InertieZ

        ValRef = 603.8 * 10 ^ (-8)

        Assert.IsTrue(IsEqual(Inertie, ValRef, DeltaVMAx * 2))  ' (0,2%)

        '# Module de flexion élastique / zz

        ValRef = 80.5 * 10 ^ (-6)
        Valeur = MyProfil.ModuleWelZ

        Assert.IsTrue(IsEqual(MyProfil.ModuleWelZ, ValRef, DeltaVMAx * 2))  ' (0,2%)

    End Sub



#End Region

#Region " Profilés PRS "

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

        ValRef = 76.5 * 10 ^ (-4)
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

#End Region

#Region " Profilés Slim floors "



#End Region


End Class