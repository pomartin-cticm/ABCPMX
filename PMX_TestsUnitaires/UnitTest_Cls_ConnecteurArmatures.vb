Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_ConnecteurArmatures

    Dim NomCas() As String = {"G1", "G2", "Q", "QC"}

    <TestMethod()> Public Sub TestMethod_PRd()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   12/06/24 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test de la résistance des connecteurs réalisés avec des fers à béton
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim poutre As New cls_Poutre(NomCas)

        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        With poutre

            '--> Définition des caractéristiques de la poutre test 
            .Dalle.Goujons.nom = "19-100"
            '.Dalle.Connecteur.Caracteristiques_Goujons() 'calcul de hsc, d, fy et fu une fois que le nom est renseigné

            .Param.Gamma.GammaVs = 1.25
            .Param.Gamma.GammaVc = 1.25

            .Dalle.beton.Classe = "C25/30"
            .Dalle.beton.Calcul_Proprietes(True)

            .Dalle.ConnecteurArmature.ds = 23 / 1000

        End With

        '---------------------------------------------------------
        '---------------------------------------------------------
        '--> Calcul de la résistance selon "German Zulassung"
        '---------------------------------------------------------
        '---------------------------------------------------------

        Dim PRd As Decimal

        '--> TW = 5 MM 

        poutre.Section.ProfilA.Tw = 5 / 1000

            ValRef = 0

            PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

            DeltaV = (PRd - ValRef)

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)






        '-----

        '--> TW = 10 MM 

        poutre.Section.ProfilA.Tw = 10 / 1000

        poutre.Dalle.beton.Classe = "C20/25"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef)

        '-----

        poutre.Dalle.beton.Classe = "C25/30"

        ValRef = 117 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C30/37"

        ValRef = 125 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C35/45"

        ValRef = 135 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C40/50"

        ValRef = 122 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C45/55"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C50/60"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C55/67"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)









        '-----

        '--> TW = 20 MM 

        poutre.Section.ProfilA.Tw = 20 / 1000

        poutre.Dalle.beton.Classe = "C20/25"

        ValRef = 0

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef)

        '-----

        poutre.Dalle.beton.Classe = "C25/30"

        ValRef = 148 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C30/37"

        ValRef = 157 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C35/45"

        ValRef = 166 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C40/50"

        ValRef = 122 * 1000 / 1.25

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C45/55"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C50/60"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '-----

        poutre.Dalle.beton.Classe = "C55/67"

        PRd = poutre.Dalle.ConnecteurArmature.PRdGermanZulassung(poutre)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)





        '---------------------------------------------------------
        '---------------------------------------------------------
        '--> Calcul de la résistance selon l'annexe I de la prEN 1994-1-1
        '---------------------------------------------------------
        '---------------------------------------------------------

        PRd = poutre.Dalle.ConnecteurArmature.PRdAnnexI(poutre)

        ValRef = 95950

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

End Class