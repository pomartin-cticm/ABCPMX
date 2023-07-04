Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Poutre

    <TestMethod()> Public Sub TestMethod_EffectiveWidth()

        'Test de la fonction qui calcul la largeur de la dalle participante à une position donnée 




        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        'TEST DE LA CONFIGURATION 1
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------



        Dim poutre As New cls_Poutre()

        'Définition des caractéristiques de la poutre test

        With poutre
            .lTraveeConsoleGauche = True
            .lTraveeConsoleDroite = False

            .EntraxeD1 = 3
            .lTremieGauche = False 'on considère qu'il n'y a pas d'ouverture à gauche


            .EntraxeD2 = 4
            .lTremieDroite = True
            .DistanceDsl2 = 1 'on prend pour hyp qu'il y a une ouverture à droite dont le bord est situé à 1 m

            .LongueurTravee(0) = 3
            .LongueurTravee(1) = 8



            'Calcul de la largeur participante pour la console gauche au droit du bord libre

            Dim x As Decimal = 0

            Dim b1 As Decimal = .EntraxeD1
            Dim b2 As Decimal = Math.Min(.EntraxeD2 / 2, .DistanceDsl2)

            Dim Le As Decimal = 2 * .LongueurTravee(0)

            Dim be1 As Decimal = Math.Min(b1, Le / 8)
            Dim be2 As Decimal = Math.Min(b2, Le / 8)

            Dim beta_1 As Decimal = Math.Min(1, 0.55 + 0.025 * Le / be1)
            Dim beta_2 As Decimal = Math.Min(1, 0.55 + 0.025 * Le / be2)

            Dim beff As Decimal = beta_1 * be1 + beta_2 * be2

            Dim beff_ref As Decimal = .EffectiveWidth(x, 0, False, False)
            Dim tau_beff As Decimal = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)



            'Calcul de la largeur participante pour la console gauche à mi-travée

            x = .LongueurTravee(0) / 2

            beff_ref = .EffectiveWidth(x, 0, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100

            Assert.IsTrue(Math.Abs(tau_beff) <= 1)



            'Calcul de la largeur participante au droit de l'appui A

            x = .LongueurTravee(0)

            beff_ref = .EffectiveWidth(x, 0, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100

            Assert.IsTrue(Math.Abs(tau_beff) <= 1)


            x = 0

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100

            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            Dim beff_a As Decimal = beff 'stockage de la valeur sur appui A pour plus tard

            'Calcul à mi-travée 

            x = .LongueurTravee(1) / 2

            Le = 0.85 * .LongueurTravee(1)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = 1
            beta_2 = 1

            beff = beta_1 * be1 + beta_2 * be2

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            Dim beff_m As Decimal = beff 'stockage de la valeur à mi travée pour + tard

            'calcul au droit de l'appui B

            x = .LongueurTravee(1)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            beff = beta_1 * be1 + beta_2 * be2

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            Dim beff_b As Decimal = beff 'stockage de la valeur sur appui B pour plus tard

            'calcul à L/10

            x = .LongueurTravee(1) / 10

            beff = beff_a + (beff_m - beff_a) / 0.25 * 0.1

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            'calcul à 9L/10

            x = 9 * .LongueurTravee(1) / 10

            beff = beff_m + (beff_b - beff_m) / 0.25 * (0.9 - 0.75)

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)




            '----------------------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------------------
            'TEST DE LA CONFIGURATION 2
            '----------------------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------------------

            .lTraveeConsoleGauche = False
            .lTraveeConsoleDroite = True

            .EntraxeD1 = 4
            .lTremieGauche = True
            .DistanceDsl1 = 1


            .EntraxeD2 = 3
            .lTremieDroite = False

            .LongueurTravee(0) = 3
            .LongueurTravee(1) = 8
            .LongueurTravee(2) = 5



            'Calcul de la largeur participante au droit de l'appui A

            x = 0

            Le = 0.85 * .LongueurTravee(1)

            b1 = Math.Min(.EntraxeD1 / 2, .DistanceDsl1)
            b2 = .EntraxeD2 / 2

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)



            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            beff = beta_1 * be1 + beta_2 * be2

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100

            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            beff_a = beff 'stockage de la valeur sur appui A pour plus tard

            'Calcul à mi-travée 

            x = .LongueurTravee(1) / 2

            Le = 0.85 * .LongueurTravee(1)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = 1
            beta_2 = 1

            beff = beta_1 * be1 + beta_2 * be2

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            beff_m = beff 'stockage de la valeur à mi travée pour + tard


            'calcul au droit de l'appui B

            x = .LongueurTravee(1)

            Le = 2 * .LongueurTravee(2)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            beff = beta_1 * be1 + beta_2 * be2

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            beff_b = beff 'stockage de la valeur sur appui B pour plus tard

            'calcul à L/10

            x = .LongueurTravee(1) / 10

            beff = beff_a + (beff_m - beff_a) / 0.25 * 0.1

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            'calcul à 9L/10

            x = 9 * .LongueurTravee(1) / 10

            beff = beff_m + (beff_b - beff_m) / 0.25 * (0.9 - 0.75)

            beff_ref = .EffectiveWidth(x, 1, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            'calcul au droit de l'appui B, coté console droite 

            x = 0

            beff = beff_b

            beff_ref = .EffectiveWidth(x, 2, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            'calcul au droit de la mi travée de la console droite

            x = .LongueurTravee(2) / 2

            beff_ref = .EffectiveWidth(x, 2, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

            'calcul au droit du bord libre de la travée droite

            x = .LongueurTravee(2)

            beff_ref = .EffectiveWidth(x, 2, False, False)
            tau_beff = (beff - beff_ref) / beff_ref * 100
            Assert.IsTrue(Math.Abs(tau_beff) <= 1)

        End With

    End Sub

End Class