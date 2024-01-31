Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Connecteur

    Dim NomCas() As String = {"G1", "G2", "Q", "QC"}

    <TestMethod()> Public Sub TestMethod_PRd_G1()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   02/08/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test de la résistance des connecteurs pour la première génération d'eurocodes
        '   Références : Exercice B5-1 stage MIX01
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        'Dim poutre As New cls_Poutre(NomCas)
        NomChargements = NomCas

        Dim poutre As New cls_Poutre()

        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        With poutre

            '--> Définition des caractéristiques de la poutre test 
            .Dalle.Connecteur.nom = "19-100"
            '.Dalle.Connecteur.Caracteristiques_Goujons() 'calcul de hsc, d, fy et fu une fois que le nom est renseigné

            .Param.Gamma.GammaVs = 1.25
            .Param.Gamma.GammaVc = 1.25

            .Dalle.beton.Classe = "C25/30"
            .Dalle.beton.Calcul_Proprietes()

            'CofraPlus 60
            .Dalle.Bac.Hp = 58 / 1000
            .Dalle.Bac.h_rs = 0 / 1000
            .Dalle.Bac.Ep = 207 / 1000
            .Dalle.Bac.Bt = 101 / 1000
            .Dalle.Bac.Bb = 62 / 1000
            .Dalle.Bac.Tp = 0.75 / 1000
            .Dalle.Bac.fyp = 350

        End With

        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        ' CALCUL GENERATION 1 de l'EC4
        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''

        '--> Calcul de la résistance en dalle béton pleine

        '# Résistance acier

        ValRef = 81660
        Dim PRd_s As Decimal = poutre.Dalle.Connecteur.PRdDallePleineG1G2Acier(poutre.Param.Gamma.GammaVs)
        'Dim tau_PRd_s As Decimal = (PRd_s - PRd_s_ref) / PRd_s_ref * 100

        DeltaV = (PRd_s - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Résistance béton

        ValRef = 74290
        Dim PRd_c As Decimal = poutre.Dalle.Connecteur.PRdDallePleineG1Beton(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVc)
        DeltaV = (PRd_c - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Résistance finale dalle pleine

        Dim PRd As Decimal = poutre.Dalle.Connecteur.PRdDallePleineG1(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Calcul dans le cas de la présence d'un bac acier parallèle

        '# kL

        Dim kl As Decimal

        ValRef = 0.6105
        kl = poutre.Dalle.Connecteur.CoefkL(poutre.Dalle.Bac)
        DeltaV = (kl - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# PRd

        ValRef = 45360
        PRd = poutre.Dalle.Connecteur.PRdBacParrallelleG1(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc, poutre.Dalle.Bac)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)


        '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire, avec un seul connecteur / nervure

        Dim nr As Integer = 1 'nombre de goujons transversaux

        '# kt

        ValRef = 0.7123
        Dim kt As Decimal = poutre.Dalle.Connecteur.CoefkT(nr, poutre.Dalle.Bac)
        DeltaV = (kt - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# PRd

        ValRef = 52920
        PRd = poutre.Dalle.Connecteur.PRdBacPerpendiculaireG1(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc, nr, poutre.Dalle.Bac)
        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)


        '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire, avec deux connecteurs / nervures

        nr = 2 'nombre de goujons transversaux

        '# kt

        ValRef = 0.5037
        kt = poutre.Dalle.Connecteur.CoefkT(nr, poutre.Dalle.Bac)
        DeltaV = (kt - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# PRd

        ValRef = 37420
        PRd = poutre.Dalle.Connecteur.PRdBacPerpendiculaireG1(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc, nr, poutre.Dalle.Bac)
        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

    <TestMethod()> Public Sub TestMethod_PRd_G2()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   02/08/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test de la résistance des connecteurs pour la seconde génération d'eurocodes
        '   Références : Exercice B5-1 stage MIX01
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        ' Dim poutre As New cls_Poutre(NomCas)
        Dim poutre As New cls_Poutre()

        Dim DeltaV, ValRef As Decimal
        Const DeltaVMAx As Decimal = 1 / 1000

        With poutre

            '--> Définition des caractéristiques de la poutre test 
            .Dalle.Connecteur.nom = "19-100"
            '.Dalle.Connecteur.Caracteristiques_Goujons() 'calcul de hsc, d, fy et fu une fois que le nom est renseigné

            .Param.Gamma.GammaVs = 1.25
            .Param.Gamma.GammaVc = 1.25

            .Dalle.beton.Classe = "C25/30"
            .Dalle.beton.Calcul_Proprietes()

            'CofraPlus 60
            .Dalle.Bac.Hp = 58 / 1000
            .Dalle.Bac.h_rs = 0 / 1000
            .Dalle.Bac.Ep = 207 / 1000
            .Dalle.Bac.Bt = 101 / 1000
            .Dalle.Bac.Bb = 62 / 1000
            .Dalle.Bac.Tp = 0.75 / 1000
            .Dalle.Bac.fyp = 350

        End With

        Dim PRd_s, PRd_c, PRd As Decimal
        Dim kt, PRd_t_ref, PRd_t As Decimal
        Dim nr As Integer = 1
        Dim tau_PRd_t As Decimal

        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        ' CALCUL GENERATION 2 de l'EC4 (calcul classique)
        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''

        '--> Calcul pour le cas d'une dalle béton pleine

        '# Valeur acier

        ValRef = 81660
        PRd_s = poutre.Dalle.Connecteur.PRdDallePleineG1G2Acier(poutre.Param.Gamma.GammaVs)
        DeltaV = (PRd_s - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# Valeur béton

        poutre.Dalle.Connecteur.kcc = 0.8

        ValRef = 59440
        PRd_c = poutre.Dalle.Connecteur.PRdDallePleineG2Beton(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVc)
        DeltaV = (PRd_c - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        ValRef = 59440
        PRd = poutre.Dalle.Connecteur.PRdDallePleineG2(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc)

        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Calcul dans le cas de la présence d'un bac acier parallèle

        ValRef = 36290

        PRd = poutre.Dalle.Connecteur.PRdBacParrallelleG2(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc, poutre.Dalle.Bac)
        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire (CALCUL CLASSIQUE)

        PRd_t = PRd * kt
        PRd_t_ref = poutre.Dalle.Connecteur.PRdBacPerpendiculaireG2(poutre.Dalle.beton.Fck, poutre.Dalle.beton.Ecm, poutre.Param.Gamma.GammaVs, poutre.Param.Gamma.GammaVc, nr, poutre.Dalle.Bac)
        tau_PRd_t = (PRd_t - PRd_t_ref) / PRd_t_ref * 100

        'Assert.IsTrue(Math.Abs(tau_PRd_t) <= PCLim)

        '=== ANNEXE G =====================================================================================================

        '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire (CALCUL selon l'ANNEXE G)

        '# équation acier
        ValRef = 59200
        PRd_s = poutre.Dalle.Connecteur.PRdBacPerpendiculaireG2_AnnexeG_Acier(poutre.Param.Gamma.GammaVs)
        DeltaV = (PRd_s - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# équation béton

        ValRef = 28860
        PRd_c = poutre.Dalle.Connecteur.PRdBacPerpendiculaireG2_AnnexeG_Beton(poutre, nr, poutre.Param.Gamma.GammaVc)
        DeltaV = (PRd_c - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

        '# PRd final

        PRd = poutre.Dalle.Connecteur.PRdBacPerpendiculaireG2_AnnexeG(poutre, nr, poutre.Param.Gamma.GammaVc, poutre.Param.Gamma.GammaVs)
        DeltaV = (PRd - ValRef) / ValRef

        Assert.IsTrue(Math.Abs(DeltaV) <= DeltaVMAx)

    End Sub

End Class