Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Poutre

    <TestMethod()> Public Sub TestMethod_EffectiveWidth()

        'Test de la fonction qui calcul la largeur de la dalle participante à une position donnée 


        Dim poutre As New cls_Poutre()

        'Définition des caractéristiques de la poutre test

        With poutre
            .lTraveeConsoleGauche = True
            .lTraveeConsoleDroite = False

            .EntraxeD1 = 3
            .DistanceDsl1 = 3 / 2 'on considère qu'il n'y a pas d'ouverture à gauche

            .EntraxeD2 = 4
            .DistanceDsl2 = 1 'on prend pour hyp qu'il y a une ouverture à droite dont le bord est situé à 1 m

            .LongueurTravee(0) = 3
            .LongueurTravee(1) = 8

            'Calcul de la largeur participante pour la console gauche à mi-travée



            Dim b1 As Decimal = Math.Max(.EntraxeD1 / 2, .DistanceDsl1)
            Dim b2 As Decimal = Math.Max(.EntraxeD2 / 2, .DistanceDsl2)

            Dim be1 As Decimal = Math.Min(b1, 2 * .LongueurTravee(0) / 8)
            Dim be2 As Decimal = Math.Min(b2, 2 * .LongueurTravee(0) / 8)

            Dim beff_m As Decimal = be1 + be2

            Dim beff_ref As Decimal = .EffectiveWidth(.LongueurTravee(0) / 2, .EnuTypeTravee.ConsoleGauche, False, False)
            Dim tau_beff As Decimal = (beff_m - beff_ref) / beff_ref * 100
            Assert.IsTrue(tau_beff <= 1)

            '--> A discuter avec POM


            'Calcul de la largeur participante pour l'appui A



        End With

    End Sub

End Class