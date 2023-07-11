Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_ProfilA

    <TestMethod()> Public Sub TestUnit_ProprietesProfileAcierLamine()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test des propriétés d'un profilé acier
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyProfil As New cls_ProfilA
        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        '--> Initialisation

        '# IPE 300

        MyProfil.ha = 0.3
        MyProfil.b_fi = 0.15
        MyProfil.b_fs = 0.15
        MyProfil.t_fi = 0.0107
        MyProfil.t_fs = 0.0107
        MyProfil.t_w = 0.0071
        MyProfil.r_ci = 0.015
        MyProfil.r_cs = 0.015
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

    End Sub

End Class