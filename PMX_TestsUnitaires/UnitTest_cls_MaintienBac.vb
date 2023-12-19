Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_cls_MaintienBac

    <TestMethod()> Public Sub TU_CofraPlus60()

        '-- Déclarations

        Dim myBac As New cls_Bac
        Dim myMaintien As New cls_MaintienBac
        Dim EntraxeD As Decimal = 3
        Dim porteeL As Decimal = 14

        Dim Valeur, ValRef As Decimal
        Dim lOk As Boolean
        Dim cCum As Decimal

        '-- Initialisations

        myBac.InitialiseCofraPlus60()
        myBac.Tp = 0.73 / 1000


        myMaintien.m = 2
        myMaintien.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes
        myMaintien.FixnervuresTyp = cls_MaintienBac.Enu_FixNervuresType.Pistolet
        myMaintien.FixCoutureType = cls_MaintienBac.Enu_CoutureType.Vis
        myMaintien.ec = 0.5

        '# Coefficient K

        Valeur = myBac.CoefK1(lok)
        ValRef = 0.176

        Assert.IsTrue(Valeur, ValRef)

        '# Alpha5

        Valeur = myMaintien.Alpha5
        ValRef = 0.5

        Assert.IsTrue(Valeur, ValRef)

        '# Beta1

        Valeur = myMaintien.Beta1(5)
        ValRef = 1.13

        Assert.IsTrue(Valeur, ValRef)

        '# c11

        Valeur = myMaintien.Flexibilite_C11_DistorsionBac(porteeL, EntraxeD, myBac, cls_Acier.EYACIER)
        ValRef = 0.883 / 10 ^ 6

        Assert.IsTrue(Valeur, ValRef)

        cCum = Valeur

        '# c12

        Valeur = myMaintien.Flexibilite_C12_Shear(porteeL, EntraxeD, myBac, cls_Acier.EYACIER, cls_Acier.NU)
        ValRef = 0.124 / 10 ^ 6

        Assert.IsTrue(Valeur, ValRef)

        cCum += Valeur

        '# c21

        Valeur = myMaintien.Flexibilite_C21_BeamFasteners(porteeL, EntraxeD, myBac.Ep)
        ValRef = 0.064 / 10 ^ 6

        Assert.IsTrue(Valeur, ValRef)

        cCum += Valeur

        '# c22

        Valeur = myMaintien.Flexibilite_C22_SeamFastener(porteeL, EntraxeD, myBac)
        ValRef = 0.415 / 10 ^ 6

        Assert.IsTrue(Valeur, ValRef)

        cCum += Valeur

        '# rigidité globale

        Valeur = porteeL / cCum
        ValRef = 9422 * 1000

        Assert.IsTrue(Valeur, ValRef)

        '# rigidité simplifiée

        Valeur = myBac.RigiditeCisaillementSimplifiee(EntraxeD)
        ValRef = 8999 * 1000

        Assert.IsTrue(Valeur, ValRef)

        '# rigidite flexionnelle du bac

        Valeur = myBac.RigiditeFlexionnelleC(EntraxeD, True)
        ValRef = 186.5 * 1000

        Assert.IsTrue(Valeur, ValRef)

        '# rigidité flexionnelle de fixation

        Valeur = myBac.RigiditeFlexionnelleA(myMaintien.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes, 0.19)
        ValRef = 18.16 * 1000

        Assert.IsTrue(Valeur, ValRef)

    End Sub

End Class