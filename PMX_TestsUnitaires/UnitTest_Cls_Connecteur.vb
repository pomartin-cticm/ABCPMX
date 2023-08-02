Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Connecteur

    <TestMethod()> Public Sub TestMethod_PRd_G1()

        Dim poutre As New cls_Poutre()



        'Définition des caractéristiques de la poutre test 

        With poutre

            .Dalle.Connecteur.nom = "16-100"
            .Dalle.Connecteur.Caracteristiques_Goujons() 'calcul de hsc, d, fy et fu une fois que le nom est renseigné

            .Param.Gamma.GammaVs = 1.12
            .Param.Gamma.GammaVc = 1.785

            .Dalle.beton.Classe = "C25/30"
            .Dalle.beton.Calcul_Proprietes()

            'CofraPlus 80.1.13
            .Dalle.Bac.h_p = 80 / 1000
            .Dalle.Bac.h_rs = 14 / 1000
            .Dalle.Bac.e_p = 270 / 1000
            .Dalle.Bac.b_t = 153 / 1000
            .Dalle.Bac.b_b = 87 / 1000
            .Dalle.Bac.tp = 1.13 / 1000
            .Dalle.Bac.fyp = 350



            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''
            ' CALCUL GENERATION 1 de l'EC4
            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''



            '--> Calcul pour le cas d'une dalle béton pleine
            Dim PRd_s As Decimal = 0.8 * .Dalle.Connecteur.Fu * Math.PI * .Dalle.Connecteur.d ^ 2 / (4 * .Param.Gamma.GammaVs) * 10 ^ 6
            Dim PRd_s_ref As Decimal = .Dalle.Connecteur.PRdDallePleineG1G2Acier(.Param.Gamma.GammaVs)
            Dim tau_PRd_s As Decimal = (PRd_s - PRd_s_ref) / PRd_s_ref * 100

            Const PCLim As Decimal = 1

            Assert.IsTrue(Math.Abs(tau_PRd_s) <= PCLim)

            Dim alpha As Decimal

            If .Dalle.Connecteur.hsc / .Dalle.Connecteur.d <= 4 Then
                alpha = 0.2 * (.Dalle.Connecteur.hsc / .Dalle.Connecteur.d + 1)
            Else
                alpha = 1
            End If

            Dim PRd_c As Decimal = 0.29 * alpha * .Dalle.Connecteur.d ^ 2 * Math.Sqrt(.Dalle.beton.Fck * .Dalle.beton.Ecm) / .Param.Gamma.GammaVc * 10 ^ 6
            Dim PRd_c_ref As Decimal = .Dalle.Connecteur.PRdDallePleineG1Beton(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVc)
            Dim tau_PRd_c As Decimal = (PRd_c - PRd_c_ref) / PRd_c_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_c) <= PCLim)

            Dim PRd As Decimal = Math.Min(PRd_c, PRd_s)
            Dim PRd_ref As Decimal = .Dalle.Connecteur.PRdDallePleineG1(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc)

            Dim tau_PRd As Decimal = (PRd - PRd_ref) / PRd_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd) <= PCLim)

            '--> Calcul dans le cas de la présence d'un bac acier parallèle

            Dim kl As Decimal = 0.6 * .Dalle.Bac.LargeurB0 / .Dalle.Bac.h_p * (.Dalle.Connecteur.hsc / .Dalle.Bac.h_p - 1)

            Dim PRd_l As Decimal = PRd * kl
            Dim PRd_l_ref As Decimal = .Dalle.Connecteur.PRdBacParrallelleG1(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc, .Dalle.Bac)
            Dim tau_PRd_l As Decimal = (PRd_l - PRd_l_ref) / PRd_l_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_l) <= PCLim)

            '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire

            Dim nr As Integer = 2 'nombre de goujons transversaux

            Dim kt As Decimal = (0.7 / Math.Sqrt(nr)) * (.Dalle.Bac.LargeurB0 / .Dalle.Bac.h_p) * (.Dalle.Connecteur.hsc / .Dalle.Bac.h_p - 1)

            Dim PRd_t As Decimal = PRd * kt
            Dim PRd_t_ref As Decimal = .Dalle.Connecteur.PRdBacPerpendiculaireG1(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc, nr, .Dalle.Bac)
            Dim tau_PRd_t As Decimal = (PRd_t - PRd_t_ref) / PRd_t_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_t) <= PCLim)


            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''
            ' CALCUL GENERATION 2 de l'EC4 (calcul classique)
            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''

            '--> Calcul pour le cas d'une dalle béton pleine
            PRd_s = 0.8 * .Dalle.Connecteur.Fu * Math.PI * .Dalle.Connecteur.d ^ 2 / (4 * .Param.Gamma.GammaVs) * 10 ^ 6
            PRd_s_ref = .Dalle.Connecteur.PRdDallePleineG1G2Acier(.Param.Gamma.GammaVs)
            tau_PRd_s = (PRd_s - PRd_s_ref) / PRd_s_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_s) <= PCLim)

            .Dalle.Connecteur.kcc = 0.8

            PRd_c = 0.29 * .Dalle.Connecteur.kcc * .Dalle.Connecteur.d ^ 2 * Math.Sqrt(.Dalle.beton.Fck * .Dalle.beton.Ecm) / .Param.Gamma.GammaVc * 10 ^ 6
            PRd_c_ref = .Dalle.Connecteur.PRdDallePleineG2Beton(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVc)
            tau_PRd_c = (PRd_c - PRd_c_ref) / PRd_c_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_c) <= PCLim)

            PRd = Math.Min(PRd_c, PRd_s)
            PRd_ref = .Dalle.Connecteur.PRdDallePleineG2(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc)

            tau_PRd = (PRd - PRd_ref) / PRd_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd) <= PCLim)

            '--> Calcul dans le cas de la présence d'un bac acier parallèle

            PRd_l = PRd * kl
            PRd_l_ref = .Dalle.Connecteur.PRdBacParrallelleG2(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc, .Dalle.Bac)
            tau_PRd_l = (PRd_l - PRd_l_ref) / PRd_l_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_l) <= PCLim)

            '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire (CALCUL CLASSIQUE)

            PRd_t = PRd * kt
            PRd_t_ref = .Dalle.Connecteur.PRdBacPerpendiculaireG2(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc, nr, .Dalle.Bac)
            tau_PRd_t = (PRd_t - PRd_t_ref) / PRd_t_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_t) <= PCLim)

            '--> Calcul dans le cas de la présence d'un bac acier perpendiculaire (CALCUL selon l'ANNEXE G)

            Dim PRd_t_s As Decimal = 0.58 * .Dalle.Connecteur.Fu * Math.PI * .Dalle.Connecteur.d ^ 2 / (4 * .Param.Gamma.GammaVs) * 10 ^ 6 'N
            Dim PRd_t_s_ref As Decimal = .Dalle.Connecteur.PRdBacPerpendiculaireG2_AnnexeG_Acier(.Param.Gamma.GammaVs)
            Dim tau_Prd_t_s = (PRd_t_s - PRd_t_s_ref) / PRd_t_s_ref * 100

            Assert.IsTrue(Math.Abs(tau_Prd_t_s) <= PCLim)

            Dim PRd_t_c As Decimal

            Dim C2 As Decimal = Math.Max(1, Math.Min(1.35, 1.85 * .Dalle.Bac.h_p / .Dalle.Bac.LargeurB0))
            Dim hA As Decimal = .Dalle.Connecteur.hsc - .Dalle.Bac.h_p

            Dim ny, sy As Decimal

            If nr = 1 Then
                ny = 2
                sy = 0
            Else
                ny = Math.Min(1 + (hA - 2 * .Dalle.Connecteur.d) / (0.52 * .Dalle.Connecteur.d), 2)
                sy = 4 * .Dalle.Connecteur.d
            End If

            Dim Wsc As Decimal = (2.4 * .Dalle.Connecteur.hsc + (nr - 1) * sy) * (.Dalle.Bac.b_t ^ 2 / 6)
            Dim Mpl_sc As Decimal = (1 / 6) * .Dalle.Connecteur.Fu * .Dalle.Connecteur.d ^ 3 * 10 ^ 6

            .Dalle.Bac.lPreperce = False

            Dim ku As Decimal

            If .Dalle.Bac.lPreperce = False And .Dalle.Bac.tp >= 1 / 1000 Then
                ku = 1.25
            Else
                ku = 1
            End If

            PRd_t_c = .Dalle.Connecteur.kcc * C2 * ku / .Param.Gamma.GammaVc * (.Dalle.beton.Fctk_005 * Wsc / (.Dalle.Bac.h_p * nr) * 10 ^ 6 + ny * Mpl_sc / (0.82 * .Dalle.Bac.h_p - .Dalle.Connecteur.d / 2))
            Dim PRd_t_c_ref As Decimal = .Dalle.Connecteur.PRdBacPerpendiculaireG2_AnnexeG_Beton(poutre, nr, .Param.Gamma.GammaVc)
            Dim tau_PRd_t_c As Decimal = (PRd_t_c - PRd_t_c_ref) / PRd_t_c_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_t_c) <= PCLim)

        End With

    End Sub

End Class