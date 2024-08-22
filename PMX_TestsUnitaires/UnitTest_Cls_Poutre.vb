Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Poutre

#Region " Déclarations et attributs "

    Dim NomCharges() As String = {"G1", "G2", "Q", "QC"}

#End Region

    <TestMethod()> Public Sub TestMethod_BEffDalle01()
        '----------------------------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création GUD
        '----------------------------------------------------------------------------------------------------------------------------------
        ' Test de la fonction qui calcule les largeurs de la dalle participante à une position donnée 
        '----------------------------------------------------------------------------------------------------------------------------------

        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        'TEST DE LA CONFIGURATION 1 : 
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------

        'Dim poutre As New cls_Poutre(NomCharges)

        Dim poutre As New cls_Poutre(NomCharges)

        Dim ValRef, ValCal As Decimal

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

            '##################################################################################################################
            '# Calcul de la largeur participante pour la console gauche au droit du bord libre (extremité de la console)
            '##################################################################################################################

            Dim xPos As Decimal = 0

            Dim b1 As Decimal = .EntraxeD1
            Dim b2 As Decimal = Math.Min(.EntraxeD2 / 2, .DistanceDsl2)

            Dim Le As Decimal = 2 * .LongueurTravee(0)

            Dim be1 As Decimal = Math.Min(b1, Le / 8)
            Dim be2 As Decimal = Math.Min(b2, Le / 8)

            Dim beta_1 As Decimal = Math.Min(1, 0.55 + 0.025 * Le / be1)
            Dim beta_2 As Decimal = Math.Min(1, 0.55 + 0.025 * Le / be2)

            '== On n'applique pas le coefficient beta dans le cas de la partie en console
            ' Dim beff_gauche As Decimal = beta_1 * be1
            Dim beff_gauche As Decimal = be1

            '-- Côté gauche

            ' Vérification avec le modèle de calcul classique

            ValRef = be1
            ValCal = .BeffDalle(xPos, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour la vérification des sections

            ValRef = be1
            ValCal = .BeffDalle(xPos, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour l'analyse de la poutre

            ValRef = be1
            ValCal = .BeffDalle(xPos, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '-- Côté droite

            ' Vérification avec le modèle de calcul classique

            ValRef = be2
            ValCal = .BeffDalle(xPos, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour la vérification des sections

            ValRef = be2
            ValCal = .BeffDalle(xPos, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour l'analyse de la poutre

            ValRef = be2
            ValCal = .BeffDalle(xPos, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '---- Largeur totale

            '-- Côté droite

            ' Vérification avec le modèle de calcul classique

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour la vérification des sections

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour l'analyse de la poutre

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '##################################################################################################################
            'Calcul de la largeur participante pour la console gauche à mi-travée
            '##################################################################################################################


            xPos = .LongueurTravee(0) / 2

            '---- Largeur totale

            ' Vérification avec le modèle de calcul classique

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour la vérification des sections

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour l'analyse de la poutre

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))



            '##################################################################################################################
            'Calcul de la largeur participante au droit de l'appui A (Vérification avec le modèle classique)
            '##################################################################################################################

            xPos = .LongueurTravee(0)

            Dim be1A, be2A As Decimal

            be1A = be1
            be2A = be2

            '---- Largeur totale

            ' Vérification avec le modèle de calcul classique

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(0, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            ' Vérification avec le modèle simplifié pour la vérification des sections

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(0, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ' Vérification avec le modèle simplifié pour l'analyse de la poutre

            ValRef = be1 + be2
            ValCal = .BeffDalle(xPos, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(0, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '##################################################################################################################
            'Calcul à mi-longueur de la travée principale (Vérification avec le modèle classique)
            '##################################################################################################################

            Dim be1m, be2m As Decimal

            xPos = .LongueurTravee(1) / 2

            Le = 0.85 * .LongueurTravee(1)

            be1m = Math.Min(b1, Le / 8)
            be2m = Math.Min(b2, Le / 8)

            ValRef = be1m
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be2m
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '-----

            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '##################################################################################################################
            'calcul au droit de l'appui B (Vérification avec le modèle classique)
            '##################################################################################################################

            Dim be1B, be2B As Decimal

            xPos = .LongueurTravee(1)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1m)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2m)

            be1B = beta_1 * be1m
            be2B = beta_2 * be2m

            ValRef = be1B
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '-----

            ValRef = be2B
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '-----

            ValRef = be1B + be2B
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '##################################################################################################################
            'calcul à L/10 (Vérification avec le modèle classique)
            '##################################################################################################################

            xPos = .LongueurTravee(1) / 10

            ValRef = be1A + (be1m - be1A) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '------

            ValRef = be2A + (be2m - be2A) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '------

            ValRef = be1A + be2A + (be2m + be1m - be1A - be2A) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '##################################################################################################################
            'calcul à 9L/10 (Vérification avec le modèle classique)
            '##################################################################################################################

            xPos = 9 * .LongueurTravee(1) / 10

            ValRef = be1B + (be1m - be1B) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '------

            ValRef = be2B + (be2m - be2B) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '------

            ValRef = be1B + be2B + (be2m + be1m - be1B - be2B) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '##################################################################################################################
            'Vérification avec le modèle simplifié pour la vérification des sections transversales
            '##################################################################################################################

            xPos = .LongueurTravee(0)

            ValRef = be1A + be2A
            ValCal = .BeffDalle(xPos, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            xPos = 0

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = 0.149 * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = 0.151 * .LongueurTravee(1)

            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = (1 - 0.151) * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = (1 - 0.149) * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            '##################################################################################################################
            'Vérification avec le modèle simplifié pour l'analyse de la poutre
            '##################################################################################################################

            xPos = .LongueurTravee(0)

            ValRef = be1A + be2A
            ValCal = .BeffDalle(xPos, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = 0
            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            xPos = 0.149 * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = 0.151 * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = (1 - 0.151) * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = (1 - 0.149) * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '------


        End With

    End Sub

    <TestMethod()> Public Sub TestMethod_BEffDalle02()

        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        'TEST DE LA CONFIGURATION 2
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------------------------

        Dim poutre As New cls_Poutre(NomCharges)

        Dim ValRef, ValCal As Decimal
        Dim xPos As Decimal = 0

        Dim b1 As Decimal
        Dim b2 As Decimal

        Dim Le As Decimal

        Dim be1 As Decimal
        Dim be2 As Decimal

        Dim beta_1 As Decimal
        Dim beta_2 As Decimal

        'Définition des caractéristiques de la poutre test

        With poutre

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

            Const PCLim As Decimal = 1

            'Calcul de la largeur participante au droit de l'appui A

            xPos = 0

            Le = 0.85 * .LongueurTravee(1)

            b1 = Math.Min(.EntraxeD1 / 2, .DistanceDsl1)
            b2 = .EntraxeD2 / 2

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            '###############################################################################################################
            '# TRAVEE PRINCIPALE
            '###############################################################################################################

            '--( Appui gauche (extrémité)

            Dim be1A, be2A As Decimal
            Dim be1B, be2B As Decimal
            Dim be1m, be2m As Decimal

            be1A = beta_1 * be1
            be2A = beta_2 * be2
            be1m = be1
            be2m = be2

            ValRef = be1A
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be2A
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be1A + be2A
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '------

            '--( Calcul à mi-travée 

            xPos = .LongueurTravee(1) / 2

            ValRef = be1m
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be2m
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '--( calcul au droit de l'appui B (appui de la console)

            xPos = .LongueurTravee(1)

            Le = 2 * .LongueurTravee(2)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            'On n'applique pas le coefficient Beta ici
            be1B = be1
            be2B = be2

            ValRef = be1B
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be2B
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be1B + be2B
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '--( calcul à L/10

            xPos = .LongueurTravee(1) / 10

            ValRef = be1A + be2A + (be2m + be1m - be1A - be2A) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '-----

            '--( calcul à 9L/10

            xPos = 9 * .LongueurTravee(1) / 10

            ValRef = be1B + be2B + (be2m + be1m - be1B - be2B) / 0.25 * 0.1
            ValCal = .BeffDalle(xPos, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))



            '###############################################################################################################
            '# CONSOLE A DROITE
            '###############################################################################################################

            '--( calcul au droit de l'appui B, coté console droite 

            xPos = 0

            ValRef = be1B
            ValCal = .BeffDalle(xPos, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.aGauche)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be2B
            ValCal = .BeffDalle(xPos, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.aDroite)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValRef = be1B + be2B
            ValCal = .BeffDalle(xPos, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '--( calcul à mi travée de la console droite

            ValRef = be1B + be2B
            ValCal = .BeffDalle(xPos, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '--( calcul à l'extrémité de la console

            xPos = .LongueurTravee(2)

            ValRef = be1B + be2B
            ValCal = .BeffDalle(xPos, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            ValCal = .BeffDalle(xPos, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            '--( Vérification avec le modèle simplifié pour la vérification des sections transversales

            xPos = 0.149 * .LongueurTravee(1)
            ValRef = be1m + be2m

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

            xPos = 0.151 * .LongueurTravee(1)
            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            xPos = (1 - 0.151) * .LongueurTravee(1)

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))


            xPos = (1 - 0.149) * .LongueurTravee(1)
            ValRef = be1B + be2B

            ValCal = .BeffDalle(xPos, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))
            ValRef = be1m + be2m
            ValCal = .BeffDalle(xPos, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
            Assert.IsTrue(IsEqual(ValCal, ValRef))

        End With

    End Sub

End Class