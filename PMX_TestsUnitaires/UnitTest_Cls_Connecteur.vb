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
            Dim PRd_s As Decimal = 0.8 * .Dalle.Connecteur.Fu * Math.PI * .Dalle.Connecteur.d ^ 2 / (4 * .Param.Gamma.GammaVs) * 10 ^ 6
            Dim PRd_s_ref As Decimal = .Dalle.Connecteur.PRdDallePleineG1G2Acier(.Param.Gamma.GammaVs)
            Dim tau_PRd_s As Decimal = (PRd_s - PRd_s_ref) / PRd_s_ref * 100

            Const PCLim As Decimal = 1

            Assert.IsTrue(Math.Abs(tau_PRd_s) <= 1)

            Dim alpha As Decimal

            If .Dalle.Connecteur.hsc / .Dalle.Connecteur.d <= 4 Then
                alpha = 0.2 * (.Dalle.Connecteur.hsc / .Dalle.Connecteur.d + 1)
            Else
                alpha = 1
            End If

            Dim PRd_c As Decimal = 0.29 * alpha * .Dalle.Connecteur.d ^ 2 * Math.Sqrt(.Dalle.beton.Fck * .Dalle.beton.Ecm) / .Param.Gamma.GammaVc * 10 ^ 6
            Dim PRd_c_ref As Decimal = .Dalle.Connecteur.PRdDallePleineG1Beton(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVc)
            Dim tau_PRd_c As Decimal = (PRd_c - PRd_c_ref) / PRd_c_ref * 100

            Assert.IsTrue(Math.Abs(tau_PRd_c) <= 1)

            Dim PRd As Decimal = Math.Min(PRd_c, PRd_s)
            Dim PRd_ref As Decimal = .Dalle.Connecteur.PRdDallePleineG1(.Dalle.beton.Fck, .Dalle.beton.Ecm, .Param.Gamma.GammaVs, .Param.Gamma.GammaVc)
        End With

    End Sub

End Class