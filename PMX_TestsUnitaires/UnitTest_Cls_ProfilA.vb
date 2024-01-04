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
        Dim ValRef, Valeur As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim zANE, Inertie, Wel, MelRd, zANP, MplRd, Wpl As Decimal

        '--> Initialisation

        '# IPE 300

        MyProfil.GenereProfileIPE300()

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

        MyProfil.ProprietesMyy(1, True, 1, zANE, Inertie, MelRd, zANP, MplRd)
        ValRef = 8356 * 10 ^ (-8)

        Assert.IsTrue(IsEqual(Inertie, ValRef))

        '# Module de flexion élastique / yy

        ValRef = 557.0 * 10 ^ (-6)

        MyProfil.ModuleFlexionYY(Wel, Wpl)

        Assert.IsTrue(IsEqual(Wel, ValRef))

        '# Module de flexion plastique / yy

        ValRef = 628.3 * 10 ^ (-6)

        Assert.IsTrue(IsEqual(Wpl, ValRef))

        '# Inertie de flexion zz

        MyProfil.InitialiseProprietes()
        Inertie = MyProfil.InertieZ

        ValRef = 603.8 * 10 ^ (-8)

        Assert.IsTrue(IsEqual(Inertie, ValRef, DeltaVMAx * 2))  ' (0,2%)

        '# Module de flexion élastique / zz

        ValRef = 80.5 * 10 ^ (-6)
        Valeur = MyProfil.ModuleWelZ

        Assert.IsTrue(IsEqual(MyProfil.ModuleWelZ, ValRef, DeltaVMAx * 2))  ' (0,2%)

        '# Position du centre de cisaillement
        ValRef = 0
        Valeur = MyProfil.PositionCentreS

        Assert.IsTrue(IsEqual(Valeur, ValRef))

        '# Rayon de giration polaire

        ValRef = 128.6 / 1000
        Valeur = MyProfil.RayonGirationPolaireCalcul

        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx * 4))       '(0,4 %)

        '# Coefficient de Wagner

        ValRef = 0
        Valeur = MyProfil.BetaZ

        Assert.IsTrue(IsEqual(Valeur, ValRef))

    End Sub



#End Region

#Region " Profilés PRS "

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierMonoSym()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' 
        '   Références : section acier de l'article RCM 3/2021
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim zANE, zANP, InertieY, MelRd, MplRd, Wel, Wpl As Decimal
        Dim Valeur As Decimal

        '--> Initialisation

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

        MyProfil.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)

        ValRef = 150394 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = -358.1 * 10 ^ (-3)
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# module élastique flexion

        ValRef = 4200.1 * 10 ^ (-6)

        MyProfil.ModuleFlexionYY(Wel, Wpl)
        DeltaV = (Wel - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position du centre de cisaillement

        ValRef = -9.615 / 100
        Valeur = MyProfil.PositionCentreS

        Assert.IsTrue(IsEqual(Valeur, ValRef))

        '# Rayon de giration polaire

        ValRef = 263.5 / 1000
        Valeur = MyProfil.RayonGirationPolaireCalcul

        Assert.IsTrue(IsEqual(Valeur, ValRef))

        '# Coefficient de Wagner

        ValRef = 154.6 / 1000
        Valeur = MyProfil.BetaZ

        Assert.IsTrue(IsEqual(Valeur, ValRef))

    End Sub

#End Region

#Region " Profilés Slim floors "

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierSlimFloorSFB()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   13/11/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier slimfloor SFB
        '   Références : fichier Excel créé par Stéphan BARTHE 
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA

        '--> Initialisation

        '# Slimfloor IFB_A --> profilé issu d'un IPE 300

        MyProfil.ha = 312 / 1000
        MyProfil.hb = 300 / 1000

        MyProfil.Plat_b = 250 / 1000
        MyProfil.Plat_t = 12 / 1000

        MyProfil.Bfs = 150 / 1000
        MyProfil.Tfs = 10.7 / 1000

        MyProfil.Bfi = 150 / 1000
        MyProfil.Tfi = 10.7 / 1000

        MyProfil.Tw = 7.1 / 1000
        MyProfil.Rci = 15 / 1000
        MyProfil.Rcs = 15 / 1000
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

        '# Aire de cisaillement

        'ValRef = 25.7 * 10 ^ (-4)
        'DeltaV = (MyProfil.AireAv - ValRef) / ValRef
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# Aire 

        'ValRef = 83.75 * 10 ^ (-4)
        'DeltaV = (MyProfil.Aire - ValRef) / ValRef
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# Inertie YY

        'MyProfil.ProprietesElastiquesMyy(1, True, 1, zANE, InertieY, MelRd)

        'ValRef = 13051.554 * 10 ^ (-8)
        'DeltaV = (InertieY - ValRef) / ValRef
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# Position ANE

        'ValRef = 94.27 * 10 ^ (-3)
        'DeltaV = (zANE - ValRef) / ValRef
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# module élastique flexion élastique selon l'axe YY

        'ValRef = 634.723 * 10 ^ (-6)
        'DeltaV = (MyProfil.ModuleFlexionElastiqueYY - ValRef) / ValRef
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# Inertie ZZ

        'MyProfil.ProprietesElastiquesMzz(True, 1, zANE, InertieZ, MelRd)

        'ValRef = 2163.975 * 10 ^ (-8)
        'DeltaV = (InertieZ - ValRef) / ValRef
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# Position ANE

        'ValRef = 0 * 10 ^ (-3)
        'DeltaV = (zANE - ValRef)
        'Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierSlimFloorIFB_A()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   13/11/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier slimfloor IFB-A
        '   Références : fichier Excel créé par Stéphan BARTHE 
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 2 / 1000
        Dim zANE, zANP, InertieY, InertieZ, MelRd, MplRd, WelY, WplY As Decimal

        '--> Initialisation

        '# Slimfloor IFB_A --> profilé issu d'un IPE 300

        MyProfil.ha = 282 / 1000
        MyProfil.hb = 300 / 1000

        MyProfil.Plat_b = 250 / 1000
        MyProfil.Plat_t = 12 / 1000

        MyProfil.Bfs = 150 / 1000
        MyProfil.Tfs = 10.7 / 1000

        MyProfil.Tfi = 0 / 1000

        MyProfil.Tw = 7.1 / 1000
        MyProfil.Rci = 0 / 1000
        MyProfil.Rcs = 15 / 1000
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

        '# Aire de cisaillement

        ValRef = ((2 * 15 + 7.1) * 10.7 / 2 + 203.1 + 244 * 7.1) * 10 ^ (-6)
        DeltaV = (MyProfil.AireAv - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Aire 

        ValRef = 65.466 * 10 ^ (-4)
        DeltaV = (MyProfil.Aire - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie YY

        MyProfil.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)

        ValRef = 9160.707 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = (114.52 - 12) * 10 ^ (-3)
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# module élastique flexion élastique

        ValRef = 546.989 * 10 ^ (-6)

        MyProfil.ModuleFlexionYY(WelY, WplY)

        DeltaV = (WelY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie ZZ

        MyProfil.ProprietesMzz(True, 1, zANE, InertieZ, MelRd, zANP, MplRd)

        ValRef = 1864.789 * 10 ^ (-8)
        DeltaV = (InertieZ - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = 0 * 10 ^ (-3)
        DeltaV = (zANE - ValRef)
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierSlimFloorIFB_B()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   13/11/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier slimfloor IFB-B
        '   Références : fichier Excel créé par Stéphan BARTHE 
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 2 / 1000
        Dim zANE, zANP, InertieY, InertieZ, MelRd, MplRd, Wel, Wpl As Decimal

        '--> Initialisation

        '# Slimfloor IFB_A --> profilé issu d'un IPE 300

        MyProfil.ha = 295 / 1000
        MyProfil.hb = 300 / 1000

        MyProfil.Plat_b = 75 / 1000
        MyProfil.Plat_t = 25 / 1000

        MyProfil.Bfs = 0 / 1000
        MyProfil.Tfs = 0 / 1000

        MyProfil.Bfi = 150 / 1000
        MyProfil.Tfi = 10.7 / 1000

        MyProfil.Tw = 7.1 / 1000
        MyProfil.Rci = 15 / 1000
        MyProfil.Rcs = 0 / 1000
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB

        '# Aire de cisaillement

        ValRef = ((2 * 15 + 7.1) * 10.7 / 2 + 203.1 + 244 * 7.1) * 10 ^ (-6)
        DeltaV = (MyProfil.AireAv - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Aire 

        ValRef = 54.216 * 10 ^ (-4)
        DeltaV = (MyProfil.Aire - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie YY

        MyProfil.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)

        ValRef = 7891.553 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = (147.2 - 10.7) * 10 ^ (-3)
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# module élastique flexion élastique

        ValRef = 533.949 * 10 ^ (-6)

        MyProfil.ModuleFlexionYY(Wel, Wpl)
        DeltaV = (Wel - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie ZZ

        MyProfil.ProprietesMzz(True, 1, zANE, InertieZ, MelRd, zANP, MplRd)

        ValRef = 390.179 * 10 ^ (-8)
        DeltaV = (InertieZ - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = 0 * 10 ^ (-3)
        DeltaV = (zANE - ValRef)
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierSlimFloorSAB()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   13/11/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier slimfloor SAB
        '   Références : fichier Excel créé par Stéphan BARTHE 
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 4 / 1000
        Dim zANE, zANP, InertieY, InertieZ, MelRd, MplRd, Wel, Wpl As Decimal

        '--> Initialisation

        '# Slimfloor SAB --> profilé issu d'un IPE 300

        MyProfil.ha = 300 / 1000
        MyProfil.hb = 300 / 1000
        MyProfil.Bfi = 150 / 1000
        MyProfil.Bfs = 75 / 1000
        MyProfil.Tfi = 10.7 / 1000
        MyProfil.Tfs = 10.7 / 1000
        MyProfil.Tw = 7.1 / 1000
        MyProfil.Rci = 15 / 1000
        MyProfil.Rcs = 15 / 1000
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

        '# Aire de cisaillement

        ValRef = 25.7 * 10 ^ (-4)
        DeltaV = (MyProfil.AireAv - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Aire 

        ValRef = 45.867 * 10 ^ (-4)
        DeltaV = (MyProfil.Aire - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie YY

        MyProfil.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)

        ValRef = 6397 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = (124.7 - 10.7) * 10 ^ (-3)
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# module élastique flexion élastique

        ValRef = 364.893 * 10 ^ (-6)

        MyProfil.ModuleFlexionYY(Wel, Wpl)
        DeltaV = (Wel - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie ZZ

        MyProfil.ProprietesMzz(True, 1, zANE, InertieZ, MelRd, zANP, MplRd)

        ValRef = 340.54 * 10 ^ (-8)
        DeltaV = (InertieZ - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Position ANE

        ValRef = 0 * 10 ^ (-3)
        DeltaV = (zANE - ValRef)
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

#End Region


End Class