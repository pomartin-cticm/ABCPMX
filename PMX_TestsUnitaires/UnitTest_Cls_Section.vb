Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Section

    <TestMethod()> Public Sub TestUnit_ProprietesSectionAcierLamine()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés élastiques et plastiques d'une section acier avec profilé laminé
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
        MySection.ProfilA.b_fi = 0.15
        MySection.ProfilA.b_fs = 0.15
        MySection.ProfilA.t_fi = 0.0107
        MySection.ProfilA.t_fs = 0.0107
        MySection.ProfilA.t_w = 0.0071
        MySection.ProfilA.r_ci = 0.015
        MySection.ProfilA.r_cs = 0.015
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
        MySection.ProfilA.b_fi = 0.2
        MySection.ProfilA.b_fs = 0.2
        MySection.ProfilA.t_fi = 0.0145
        MySection.ProfilA.t_fs = 0.0145
        MySection.ProfilA.t_w = 0.0084
        MySection.ProfilA.r_ci = 0.021
        MySection.ProfilA.r_cs = 0.021
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

        MySection.ProprietesElastiquesMyy(1, True, MyGamma, 1, zANE, InertieY, MelRd)

        '# Position ANE

        ValRef = -0.497 / 2
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Y

        ValRef = 42930 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Tests autres propriétés

        '# Inertie de torsion

        ValRef = 62.8 * 10 ^ (-8)
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
        MySection.enrobage_partiel.LitArma(0).PhiInt = 0.008
        MySection.enrobage_partiel.LitArma(0).PhiMil = 0.012

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

        '--> Tests autres propriétés

        '# Coefficient d'équivalent CT

        ValRef = 6.672
        DeltaV = (MySection.enrobage_partiel.Beton.CoefficientEquivalenceCT - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie de torsion

        ' Valeur de référence légèrement <> de celle de l'article, car n0 est calculé avec Ecm obtenu par la formule, et non la valeur tabulée
        ValRef = 1129 * 10 ^ (-8)
        DeltaV = (MySection.InertieT - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub
End Class