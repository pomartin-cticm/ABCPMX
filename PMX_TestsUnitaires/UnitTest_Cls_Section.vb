Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Section

    <TestMethod()> Public Sub TestUnit_ProprietesSectionAcierLamine()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section acier avec profilé laminé
        '   Références : Catalogue AM
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MySection As New cls_Section
        Dim MyGamma As New Cls_Gamma
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim InertieY, InertieZ As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        '--> Initialisations

        MySection.typeSection = cls_Section.Enum_TypeSection.Acier

        '# IPE 300

        MySection.ProfilA.ha = 0.3
        MySection.ProfilA.Bfi = 0.15
        MySection.ProfilA.Bfs = 0.15
        MySection.ProfilA.Tfi = 0.0107
        MySection.ProfilA.Tfs = 0.0107
        MySection.ProfilA.Tw = 0.0071
        MySection.ProfilA.Rci = 0.015
        MySection.ProfilA.Rcs = 0.015
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '# Acier S355 M/ML

        MySection.Acier.InitialiseAcierS355MML()

        '# Gamma

        MyGamma.GammaM0 = 1

        '--> Tests des propriétés plastiques

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.15
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 355 * 628
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique des semelles seules

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 1, zANP, MplRd)

        ValRef = 355 * (15 * 1.07) * (30 - 1.07)
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Effort tranchant plastique

        ''ValRef = 355 / Math.Sqrt(3) * 2570
        ''DeltaV = (MySection.vplrd - ValRef) / ValRef
        ''Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe YY

        MySection.ProprietesElastiquesMyy(1, True, MyGamma, 1, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = -0.15
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Y

        ValRef = 8356 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment élastique

        ValRef = 197759
        DeltaV = (MelRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe ZZ

        MySection.ProprietesElastiquesMzz(1, True, MyGamma, zANE, Inertiez, MelRd)

        '# Position ANE

        ValRef = 0
        DeltaV = (zANE - ValRef) / MySection.ProfilA.ha
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Z

        ValRef = 604 * 10 ^ (-8)
        DeltaV = (InertieZ - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

    <TestMethod()> Public Sub TestUnit_ProprietesSectionAcierEnrobeeLamine()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   12/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section acier + enrobage partiel avec profilé laminé
        '   Références : article RCM 2023/3
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MySection As New cls_Section
        Dim MyGamma As New Cls_Gamma
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim InertieY, InertieZ As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim nEqEc As Decimal

        '--> Initialisations

        MySection.typeSection = cls_Section.Enum_TypeSection.Acier

        '# IPE 500A

        MySection.ProfilA.ha = 0.497
        MySection.ProfilA.Bfi = 0.2
        MySection.ProfilA.Bfs = 0.2
        MySection.ProfilA.Tfi = 0.0145
        MySection.ProfilA.Tfs = 0.0145
        MySection.ProfilA.Tw = 0.0084
        MySection.ProfilA.Rci = 0.021
        MySection.ProfilA.Rcs = 0.021
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '# Acier S355 M/ML

        MySection.Acier.InitialiseAcierS355MML()

        '# Gamma

        MyGamma.GammaM0 = 1

        '=== ACIER SEUL ===============================================================================

        '--> Tests des propriétés plastiques du profilés acier seul

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.497 / 2
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 355 * 1946
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique des semelles seules

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 1, zANP, MplRd)

        ValRef = 355 * (20 * 1.45) * (49.7 - 1.45)
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe YY du profilé acier seul

        MySection.ProprietesElastiquesMyy(1, False, MyGamma, 1, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = -0.497 / 2
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Y

        ValRef = 42930 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment élastique

        ValRef = 613286
        DeltaV = (MelRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests autres propriétés

        '# Inertie de torsion

        'ValRef = 62.8 * 10 ^ (-8)  ' Changement de formules
        ValRef = 64.28 * 10 ^ (-8)
        DeltaV = (MySection.InertieT - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '=== ACIER + ENROBAGE ============================================================================

        '--> Définition de l'enrobage partiel

        MySection.typeSection = cls_Section.Enum_TypeSection.AcierEnrobage

        MySection.enrobage_partiel.Ratio_bc = 1
        MySection.enrobage_partiel.Beton.Classe = "C25/30"
        MySection.enrobage_partiel.Beton.Calcul_Proprietes()

        '--> Définition du lit d'armature supérieure

        MySection.enrobage_partiel.AcierArmatures.Classe = "B500"

        MySection.enrobage_partiel.LitArma(2).NbExt = 1
        MySection.enrobage_partiel.LitArma(2).NbMil = 0
        MySection.enrobage_partiel.LitArma(2).NbInt = 1
        MySection.enrobage_partiel.LitArma(2).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(2).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(2).PhiMil = 0.008

        '--> Définition du lit d'armature intermédiaire

        MySection.enrobage_partiel.LitArma(1).NbExt = 0
        MySection.enrobage_partiel.LitArma(1).NbMil = 0
        MySection.enrobage_partiel.LitArma(1).NbInt = 0
        MySection.enrobage_partiel.LitArma(1).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(1).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(1).PhiMil = 0.008

        '--> Définition du lit d'armature inférieur

        MySection.enrobage_partiel.LitArma(0).NbExt = 1
        MySection.enrobage_partiel.LitArma(0).NbMil = 0
        MySection.enrobage_partiel.LitArma(0).NbInt = 1
        MySection.enrobage_partiel.LitArma(0).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(0).PhiInt = 0.012
        MySection.enrobage_partiel.LitArma(0).PhiMil = 0.00

        '--> Définition des étriers

        MySection.enrobage_partiel.Etriers_Phi = 0.006
        MySection.enrobage_partiel.Etriers_EnrobageZ = 0.06 - 0.006 * 2

        '--> Coefficient d'équivalence court terme du béton

        nEqEc = MySection.enrobage_partiel.Beton.CoefficientEquivalence(50, 1, 1, 1, 0)

        '--> Tests des propriétés élastiques / axe YY du profilé acier avec l'enrobage, en flexion positive

        MySection.ProprietesElastiquesMyy(1, True, MyGamma, nEqEc, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = -0.2052
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 30)       '3%

        '# Inertie Y

        ValRef = 52814 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 30)       '3%

        '# Moment élastique Rd

        ValRef = 307957
        DeltaV = (MelRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 10)       '1%

        '# Moment élastique Rk

        MySection.ProprietesElastiquesMyy(1, False, MyGamma, nEqEc, zANE, InertieY, MelRd)
        ValRef = 461936
        DeltaV = (MelRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 10)       '1%

        '--> Tests autres propriétés

        '# Coefficient d'équivalent CT

        ValRef = 6.672
        DeltaV = (MySection.enrobage_partiel.Beton.CoefficientEquivalenceCT - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie de torsion

        ' Valeur de référence légèrement <> de celle de l'article, car n0 est calculé avec Ecm obtenu par la formule, et non la valeur tabulée
        ValRef = 1131 * 10 ^ (-8)
        DeltaV = (MySection.InertieT - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés plastiques / axe YY du profilé acier avec l'enrobage, en flexion positive

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.192
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 775.4 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Mêmes calculs en valeurs caractéristiques

        MySection.ProprietesPlastiquesMyy(1, False, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.1702
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 799.1 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> TESTS en supprimant les armatures

        MySection.enrobage_partiel.LitArma(2).NbExt = 0
        MySection.enrobage_partiel.LitArma(2).NbMil = 0
        MySection.enrobage_partiel.LitArma(2).NbInt = 0
        MySection.enrobage_partiel.LitArma(2).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(2).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(2).PhiMil = 0.008

        '--> Définition du lit d'armature intermédiaire

        MySection.enrobage_partiel.LitArma(1).NbExt = 0
        MySection.enrobage_partiel.LitArma(1).NbMil = 0
        MySection.enrobage_partiel.LitArma(1).NbInt = 0
        MySection.enrobage_partiel.LitArma(1).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(1).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(1).PhiMil = 0.008

        '--> Définition du lit d'armature inférieur

        MySection.enrobage_partiel.LitArma(0).NbExt = 0
        MySection.enrobage_partiel.LitArma(0).NbMil = 0
        MySection.enrobage_partiel.LitArma(0).NbInt = 0
        MySection.enrobage_partiel.LitArma(0).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(0).PhiInt = 0.012
        MySection.enrobage_partiel.LitArma(0).PhiMil = 0.00

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.1756
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 741.5 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub


    <TestMethod()> Public Sub TestUnit_ProprietesSectionAcierEnrobeeLamineSpecial()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   12/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section acier + enrobage partiel avec profilé laminé
        '   Références : article RCM 2023/3
        '   Sans les armatures et sans les congés
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MySection As New cls_Section
        Dim MyGamma As New Cls_Gamma
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim InertieY, InertieZ As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim nEqEc As Decimal

        '--> Initialisations

        MySection.typeSection = cls_Section.Enum_TypeSection.Acier

        '# IPE 500A

        MySection.ProfilA.ha = 0.497
        MySection.ProfilA.Bfi = 0.2
        MySection.ProfilA.Bfs = 0.2
        MySection.ProfilA.Tfi = 0.0145
        MySection.ProfilA.Tfs = 0.0145
        MySection.ProfilA.Tw = 0.0084
        MySection.ProfilA.Rci = 0 '.021
        MySection.ProfilA.Rcs = 0 '.021
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym

        '# Acier S355 M/ML

        MySection.Acier.InitialiseAcierS355MML()

        '# Gamma

        MyGamma.GammaM0 = 1


        '=== ACIER + ENROBAGE ============================================================================

        '--> Définition de l'enrobage partiel

        MySection.typeSection = cls_Section.Enum_TypeSection.AcierEnrobage

        MySection.enrobage_partiel.Ratio_bc = 1
        MySection.enrobage_partiel.Beton.Classe = "C25/30"
        MySection.enrobage_partiel.Beton.Calcul_Proprietes()

        '--> Définition du lit d'armature supérieure

        MySection.enrobage_partiel.AcierArmatures.Classe = "B500"

        MySection.enrobage_partiel.LitArma(2).NbExt = 0
        MySection.enrobage_partiel.LitArma(2).NbMil = 0
        MySection.enrobage_partiel.LitArma(2).NbInt = 0
        MySection.enrobage_partiel.LitArma(2).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(2).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(2).PhiMil = 0.008

        '--> Définition du lit d'armature intermédiaire

        MySection.enrobage_partiel.LitArma(1).NbExt = 0
        MySection.enrobage_partiel.LitArma(1).NbMil = 0
        MySection.enrobage_partiel.LitArma(1).NbInt = 0
        MySection.enrobage_partiel.LitArma(1).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(1).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(1).PhiMil = 0.008

        '--> Définition du lit d'armature inférieur

        MySection.enrobage_partiel.LitArma(0).NbExt = 0
        MySection.enrobage_partiel.LitArma(0).NbMil = 0
        MySection.enrobage_partiel.LitArma(0).NbInt = 0
        MySection.enrobage_partiel.LitArma(0).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(0).PhiInt = 0.012
        MySection.enrobage_partiel.LitArma(0).PhiMil = 0.00

        '--> Définition des étriers

        MySection.enrobage_partiel.Etriers_Phi = 0.006
        MySection.enrobage_partiel.Etriers_EnrobageZ = 0.06 - 0.006 * 2

        '--> Coefficient d'équivalence court terme du béton

        nEqEc = MySection.enrobage_partiel.Beton.CoefficientEquivalence(50, 1, 1, 1, 0)

        '--> Tests des propriétés élastiques / axe YY du profilé acier avec l'enrobage, en flexion positive



        '--> Tests des propriétés plastiques / axe YY du profilé acier avec l'enrobage, en flexion positive

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.1753
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 711.1 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '=== ACIER + ENROBAGE + CONGES ============================================================================

        MySection.ProfilA.Rci = 0.021
        MySection.ProfilA.Rcs = 0.021
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '--> Tests des propriétés plastiques / axe YY du profilé acier avec l'enrobage, en flexion positive

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.1756
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 741.5 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '=== ACIER + ENROBAGE + SANS CONGES + 1 lit inférieur ============================================================================

        MySection.ProfilA.Rci = 0
        MySection.ProfilA.Rcs = 0
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym

        MySection.enrobage_partiel.LitArma(0).NbExt = 0
        MySection.enrobage_partiel.LitArma(0).NbMil = 0
        MySection.enrobage_partiel.LitArma(0).NbInt = 1
        MySection.enrobage_partiel.LitArma(0).PhiExt = 0.008
        MySection.enrobage_partiel.LitArma(0).PhiInt = 0.012
        MySection.enrobage_partiel.LitArma(0).PhiMil = 0.00

        '--> Tests des propriétés plastiques / axe YY du profilé acier avec l'enrobage, en flexion positive

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.1866
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 734.8 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

    <TestMethod()> Public Sub TestUnit_ProprietesSectionAcierMonoSym()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   13/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section acier avec profilé laminé
        '   Références : section acier de l'article RCM 3/2021
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MySection As New cls_Section
        Dim MyGamma As New Cls_Gamma
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim InertieY, InertieZ As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        '--> Initialisations

        MySection.typeSection = cls_Section.Enum_TypeSection.Acier

        '# IPE 300

        MySection.ProfilA.ha = 0.575
        MySection.ProfilA.Bfi = 0.35
        MySection.ProfilA.Bfs = 0.25
        MySection.ProfilA.Tfi = 0.04
        MySection.ProfilA.Tfs = 0.025
        MySection.ProfilA.Tw = 0.015
        MySection.ProfilA.Rci = 0.00
        MySection.ProfilA.Rcs = 0.00
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym

        '# Acier S355 M/ML

        MySection.Acier.InitialiseAcierS355MML()

        '# Gamma

        MyGamma.GammaM0 = 1

        '--> Tests des propriétés plastiques

        MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 0, zANP, MplRd)

        '# Position ANP

        ValRef = -0.5311
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique

        ValRef = 1915.67 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe YY

        MySection.ProprietesElastiquesMyy(1, True, MyGamma, 1, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = -0.3581
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Y

        ValRef = 150394 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe ZZ

        MySection.ProprietesElastiquesMzz(1, True, MyGamma, zANE, InertieZ, MelRd)

        '# Position ANE

        ValRef = 0
        DeltaV = (zANE - ValRef) / MySection.ProfilA.ha
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Z

        ValRef = 17561 * 10 ^ (-8)
        DeltaV = (InertieZ - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)


    End Sub


    <TestMethod()> Public Sub TestUnit_ProprietesSectionMixteLamine()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section mixte avec profilé laminé
        '   Références : article RCM 2018-2
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MySection As New cls_Section
        Dim MyGamma As New Cls_Gamma
        Dim MyDalle As New Cls_Dalle
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim InertieY, InertieZ As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim bEff, Eta As Decimal

        '--> Initialisations

        MySection.typeSection = cls_Section.Enum_TypeSection.Mixte

        '# IPE 450

        MySection.ProfilA.ha = 0.45
        MySection.ProfilA.Bfi = 0.19
        MySection.ProfilA.Bfs = 0.19
        MySection.ProfilA.Tfi = 0.0146
        MySection.ProfilA.Tfs = 0.0146
        MySection.ProfilA.Tw = 0.0094
        MySection.ProfilA.Rci = 0.021
        MySection.ProfilA.Rcs = 0.021
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '# Acier S275 M/ML

        MySection.Acier.InitialiseAcierS275JR()

        '# Gamma

        MyGamma.GammaM0 = 1
        MyGamma.GammaC = 1.5

        '# Dalle

        bEff = 3
        Eta = 1
        MyDalle.beton.Classe = "C25/30"
        MyDalle.beton.Calcul_Proprietes()
        MyDalle.type = Cls_Dalle.Enum_TypeDalle.Mixte
        MyDalle.t_d = 0.12
        MyDalle.Bac.InitialiseCofraPlus60()
        MyDalle.Bac.Orientation = Cls_Bac.Enum_Orientation.Perpendiculaire

        '--> Tests des propriétés plastiques

        MySection.ProprietesPlastiquesMixteMyy(1, True, MyGamma, 0, beff, eta, mydalle, zANP, MplRd)

        '# Position ANP

        ValRef = -0.00079
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 50)           ' 5%

        '# Moment plastique

        ValRef = 845.9 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Moment plastique des semelles seules

        ''MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 1, zANP, MplRd)

        ''ValRef = 355 * (15 * 1.07) * (30 - 1.07)
        ''DeltaV = (MplRd - ValRef) / ValRef
        ''Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe YY

        '=== Court terme
        Dim n0 As Decimal = 6.77
        MySection.ProprietesElastiquesMixteMyy(1, True, MyGamma, 1, n0, bEff, MyDalle, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = MyDalle.t_d - 0.114
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 20)   ' 2%

        '# Inertie Y

        ValRef = 106266 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '=== Long terme

        n0 = 3 * n0
        MySection.ProprietesElastiquesMixteMyy(1, True, MyGamma, 1, n0, bEff, MyDalle, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = MyDalle.t_d - 0.194
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 20)   ' 2%

        '# Inertie Y

        ValRef = 80885 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub


    <TestMethod()> Public Sub TestUnit_ProprietesSectionMixteMonosym()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section mixte avec profilé PRS monosym
        '   Références : article RCM 2021-3
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MySection As New cls_Section
        Dim MyGamma As New Cls_Gamma
        Dim MyDalle As New Cls_Dalle
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim InertieY, InertieZ As Decimal
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000
        Dim bEff, Eta As Decimal

        '--> Initialisations

        MySection.typeSection = cls_Section.Enum_TypeSection.Mixte

        '# Monosym

        MySection.ProfilA.ha = 0.51 + 0.025 + 0.04
        MySection.ProfilA.Bfi = 0.35
        MySection.ProfilA.Bfs = 0.25
        MySection.ProfilA.Tfi = 0.04
        MySection.ProfilA.Tfs = 0.025
        MySection.ProfilA.Tw = 0.015
        MySection.ProfilA.Rci = 0.0
        MySection.ProfilA.Rcs = 0.0
        MySection.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym

        '# Acier S355

        MySection.Acier.InitialiseAcierS355MML()

        '# Gamma

        MyGamma.GammaM0 = 1
        MyGamma.GammaC = 1.5

        '# Dalle

        bEff = 2.65
        Eta = 1
        MyDalle.beton.Classe = "C30/37"
        MyDalle.beton.Calcul_Proprietes()
        MyDalle.type = Cls_Dalle.Enum_TypeDalle.Pleine
        MyDalle.t_d = 0.2
        MyDalle.t_h = 0

        '--> Tests des propriétés plastiques

        MySection.ProprietesPlastiquesMixteMyy(1, True, MyGamma, 0, bEff, Eta, MyDalle, zANP, MplRd)

        '# Position ANP

        ValRef = -0.004
        DeltaV = (zANP - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 50)           ' 5%

        '# Moment plastique

        ValRef = 4368 * 10 ^ 3
        DeltaV = (MplRd - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''Exit Sub
        ''# Moment plastique des semelles seules

        ''MySection.ProprietesPlastiquesMyy(1, True, MyGamma, 1, zANP, MplRd)

        ''ValRef = 355 * (15 * 1.07) * (30 - 1.07)
        ''DeltaV = (MplRd - ValRef) / ValRef
        ''Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ''# Effort tranchant plastique

        ''''ValRef = 355 / Math.Sqrt(3) * 2570
        ''''DeltaV = (MySection.vplrd - ValRef) / ValRef
        ''''Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe YY

        Exit Sub
        Dim n0 As Decimal = 6
        MySection.ProprietesElastiquesMixteMyy(1, True, MyGamma, 1, n0, bEff, MyDalle, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = -0.15
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Y

        ValRef = 8356 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests des propriétés élastiques / axe ZZ

        MySection.ProprietesElastiquesMzz(1, True, MyGamma, zANE, InertieZ, MelRd)

        '# Position ANE

        ValRef = 0
        DeltaV = (zANE - ValRef) / MySection.ProfilA.ha
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Z

        ValRef = 604 * 10 ^ (-8)
        DeltaV = (InertieZ - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

End Class