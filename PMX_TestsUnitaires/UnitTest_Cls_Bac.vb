Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Bac

    <TestMethod()> Public Sub TestUnit_FonctionK1K2()

        Dim MyBac As New Cls_Bac

        Dim K1, K2 As Decimal
        Dim lOk As Boolean

        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        '# 1 er bac fictif

        MyBac.Ep = 0.2
        MyBac.Hp = 0.04
        MyBac.Bt = 0.06
        MyBac.Bb = 0.06

        K1 = MyBac.CoefK1(lok)
        K2 = MyBac.CoefK2(lOk)

        ValRef = 0.199
        DeltaV = (K1 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ValRef = 0.206
        DeltaV = (K2 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# 2 eme bac fictif

        MyBac.Hp = 0.05

        K1 = MyBac.CoefK1(lOk)
        K2 = MyBac.CoefK2(lOk)

        ValRef = 0.2935
        DeltaV = (K1 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ValRef = 0.304
        DeltaV = (K2 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Cofraplus 60

        MyBac = New Cls_Bac

        MyBac.Ep = 0.207
        MyBac.Hp = 0.058
        MyBac.Bt = 0.101
        MyBac.Bb = 0.062

        K1 = MyBac.CoefK1(lOk)
        K2 = MyBac.CoefK2(lOk)

        ValRef = 0.176
        DeltaV = (K1 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx * 2)        ' 0,2%

        ValRef = 1.499
        DeltaV = (K2 - ValRef) / ValRef
        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

End Class