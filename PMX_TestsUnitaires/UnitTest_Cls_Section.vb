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
        Dim zANE, MelRd, InertieY As Decimal
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

        '# Acier S355

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

        '--> Tests des propriétés élastiques

        MySection.ProprietesElastiquesMyy(1, True, MyGamma, zANE, inertiey, MelRd)

        '# Position ANE

        ValRef = -0.15
        DeltaV = (zANE - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Inertie Y

        ValRef = 8356 * 10 ^ (-8)
        DeltaV = (InertieY - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)


    End Sub

End Class