Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_Cls_Poutre

#Region " Déclarations et attributs "

    Dim NomCharges() As String = {"G1", "G2", "Q", "QC"}

#End Region

    <TestMethod()> Public Sub TestMethod_EffectiveWidth()
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




            Dim beff_gauche As Decimal = beta_1 * be1

            Dim beff_ref_gauche As Decimal = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            Dim tau_beff_gauche As Decimal = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1) 'Vérification avec le modèle de calcul classique

            beff_ref_gauche = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1) 'Vérification avec le modèle simplifié pour la vérification des sections

            beff_ref_gauche = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_ref_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1) 'Vérification avec le modèle simplifié pour l'analyse de la poutre

            '----

            Dim beff_droite As Decimal = beta_2 * be2

            Dim beff_ref_droite As Decimal = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            Dim tau_beff_droite As Decimal = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1) 'Vérification avec le modèle de calcul classique

            beff_ref_droite = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1) 'Vérification avec le modèle simplifié pour la vérification des sections

            beff_ref_droite = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_ref_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1) 'Vérification avec le modèle simplifié pour l'analyse de la poutre

            '----

            Dim beff_totale As Decimal = beta_1 * be1 + beta_2 * be2

            Dim beff_ref_totale As Decimal = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            Dim tau_beff_totale As Decimal = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1) 'Vérification avec le modèle de calcul classique

            beff_ref_totale = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1) 'Vérification avec le modèle simplifié pour la vérification des sections

            beff_ref_totale = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_ref_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1) 'Vérification avec le modèle simplifié pour l'analyse de la poutre














            'Calcul de la largeur participante pour la console gauche à mi-travée


            x = .LongueurTravee(0) / 2

            beff_ref_gauche = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1) 'Vérification avec le modèle de calcul classique

            beff_ref_gauche = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1) 'Vérification avec le modèle simplifié pour la vérification des sections

            beff_ref_gauche = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1) 'Vérification avec le modèle simplifié pour l'analyse de la poutre


            '----

            beff_ref_droite = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1) 'Vérification avec le modèle de calcul classique

            beff_ref_droite = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1) 'Vérification avec le modèle simplifié pour la vérification des sections

            beff_ref_droite = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1) 'Vérification avec le modèle simplifié pour l'analyse de la poutre


            '----

            beff_ref_totale = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1) 'Vérification avec le modèle de calcul classique

            beff_ref_totale = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1) 'Vérification avec le modèle simplifié pour la vérification des sections

            beff_ref_totale = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1) 'Vérification avec le modèle simplifié pour l'analyse de la poutre











            'Calcul de la largeur participante au droit de l'appui A (Vérification avec le modèle classique)

            x = .LongueurTravee(0)

            beff_ref_gauche = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            x = 0

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            Dim beff_a_gauche As Decimal = beff_gauche 'stockage de la valeur sur appui A pour plus tard




            '----

            x = .LongueurTravee(0)

            beff_ref_droite = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            x = 0

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            Dim beff_a_droite As Decimal = beff_droite 'stockage de la valeur sur appui A pour plus tard





            '----


            x = .LongueurTravee(0)

            beff_ref_totale = .BeffDalle(x, 0, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)


            x = 0

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            Dim beff_a_totale As Decimal = beff_totale 'stockage de la valeur sur appui A pour plus tard


















            'Calcul à mi-travée (Vérification avec le modèle classique)

            x = .LongueurTravee(1) / 2

            Le = 0.85 * .LongueurTravee(1)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = 1
            beta_2 = 1

            beff_gauche = beta_1 * be1

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            Dim beff_m_gauche As Decimal = beff_gauche 'stockage de la valeur à mi travée pour + tard


            '-----

            beff_droite = beta_2 * be2

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            Dim beff_m_droite As Decimal = beff_droite 'stockage de la valeur à mi travée pour + tard


            '-----


            beff_totale = beta_1 * be1 + beta_2 * be2

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            Dim beff_m_totale As Decimal = beff_totale 'stockage de la valeur à mi travée pour + tard














            'calcul au droit de l'appui B (Vérification avec le modèle classique)

            x = .LongueurTravee(1)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            beff_gauche = beta_1 * be1

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            Dim beff_b_gauche As Decimal = beff_gauche 'stockage de la valeur sur appui B pour plus tard

            '-----

            beff_droite = beta_2 * be2

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            Dim beff_b_droite As Decimal = beff_droite 'stockage de la valeur sur appui B pour plus tard


            '-----

            beff_totale = beta_1 * be1 + beta_2 * be2

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            Dim beff_b_totale As Decimal = beff_totale 'stockage de la valeur sur appui B pour plus tard












            'calcul à L/10 (Vérification avec le modèle classique)

            x = .LongueurTravee(1) / 10


            beff_gauche = beff_a_gauche + (beff_m_gauche - beff_a_gauche) / 0.25 * 0.1

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            '------


            beff_droite = beff_a_droite + (beff_m_droite - beff_a_droite) / 0.25 * 0.1

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            '------



            beff_totale = beff_a_totale + (beff_m_totale - beff_a_totale) / 0.25 * 0.1

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)













            'calcul à 9L/10 (Vérification avec le modèle classique)

            x = 9 * .LongueurTravee(1) / 10

            beff_gauche = beff_m_gauche + (beff_b_gauche - beff_m_gauche) / 0.25 * (0.9 - 0.75)

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            '--------


            beff_droite = beff_m_droite + (beff_b_droite - beff_m_droite) / 0.25 * (0.9 - 0.75)

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            '--------

            beff_totale = beff_m_totale + (beff_b_totale - beff_m_totale) / 0.25 * (0.9 - 0.75)

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)













            'Vérification avec le modèle simplifié pour la vérification des sections transversales

            x = .LongueurTravee(0)

            beff_gauche = beff_a_gauche
            beff_ref_gauche = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            x = 0

            beff_gauche = beff_a_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_gauche = beff_a_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_gauche = beff_b_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)





            '------






            x = .LongueurTravee(0)

            beff_droite = beff_a_droite
            beff_ref_droite = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            x = 0

            beff_droite = beff_a_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_droite = beff_a_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_droite = beff_b_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)







            '------



            x = .LongueurTravee(0)

            beff_totale = beff_a_totale
            beff_ref_totale = .BeffDalle(x, 0, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)


            x = 0

            beff_totale = beff_a_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_totale = beff_a_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_totale = beff_b_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)









            'Vérification avec le modèle simplifié pour l'analyse de la poutre


            x = .LongueurTravee(0)

            beff_gauche = beff_a_gauche
            beff_ref_gauche = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            x = 0

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            '------


            x = .LongueurTravee(0)

            beff_droite = beff_a_droite
            beff_ref_droite = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            x = 0

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)



            '------

            x = .LongueurTravee(0)

            beff_totale = beff_a_totale
            beff_ref_totale = .BeffDalle(x, 0, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)


            x = 0

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)













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

            Const PCLim As Decimal = 1








            'Calcul de la largeur participante au droit de l'appui A

            x = 0

            Le = 0.85 * .LongueurTravee(1)

            b1 = Math.Min(.EntraxeD1 / 2, .DistanceDsl1)
            b2 = .EntraxeD2 / 2

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)



            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)



            beff_gauche = beta_1 * be1

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)


            beff_a_gauche = beff_gauche 'stockage de la valeur sur appui A pour plus tard

            '------



            beff_droite = beta_2 * be2

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            beff_a_droite = beff_droite 'stockage de la valeur sur appui A pour plus tard




            '------


            beff_totale = beta_1 * be1 + beta_2 * be2

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)


            beff_a_totale = beff_totale 'stockage de la valeur sur appui A pour plus tard








            'Calcul à mi-travée 

            x = .LongueurTravee(1) / 2

            Le = 0.85 * .LongueurTravee(1)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = 1
            beta_2 = 1


            beff_gauche = beta_1 * be1

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            beff_m_gauche = beff_gauche 'stockage de la valeur à mi travée pour + tard




            '-----


            beff_droite = beta_2 * be2

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            beff_m_droite = beff_droite 'stockage de la valeur à mi travée pour + tard




            '----


            beff_totale = beta_1 * be1 + beta_2 * be2

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            beff_m_totale = beff_totale 'stockage de la valeur à mi travée pour + tard











            'calcul au droit de l'appui B

            x = .LongueurTravee(1)

            Le = 2 * .LongueurTravee(2)

            be1 = Math.Min(b1, Le / 8)
            be2 = Math.Min(b2, Le / 8)

            beta_1 = Math.Min(1, 0.55 + 0.025 * Le / be1)
            beta_2 = Math.Min(1, 0.55 + 0.025 * Le / be2)

            beff_gauche = beta_1 * be1

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            beff_b_gauche = beff_gauche 'stockage de la valeur sur appui B pour plus tard


            '-----

            beff_droite = beta_2 * be2

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            beff_b_droite = beff_droite 'stockage de la valeur sur appui B pour plus tard



            '-----

            beff_totale = beta_1 * be1 + beta_2 * be2

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            beff_b_totale = beff_totale 'stockage de la valeur sur appui B pour plus tard








            'calcul à L/10

            x = .LongueurTravee(1) / 10

            beff_gauche = beff_a_gauche + (beff_m_gauche - beff_a_gauche) / 0.25 * 0.1


            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)


            '-----


            beff_droite = beff_a_droite + (beff_m_droite - beff_a_droite) / 0.25 * 0.1

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)



            '-----

            beff_totale = beff_a_totale + (beff_m_totale - beff_a_totale) / 0.25 * 0.1

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)





            'calcul à 9L/10

            x = 9 * .LongueurTravee(1) / 10

            beff_gauche = beff_m_gauche + (beff_b_gauche - beff_m_gauche) / 0.25 * (0.9 - 0.75)

            beff_ref_gauche = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)



            '-------

            beff_droite = beff_m_droite + (beff_b_droite - beff_m_droite) / 0.25 * (0.9 - 0.75)

            beff_ref_droite = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)



            '-------

            beff_totale = beff_m_totale + (beff_b_totale - beff_m_totale) / 0.25 * (0.9 - 0.75)

            beff_ref_totale = .BeffDalle(x, 1, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)





            'calcul au droit de l'appui B, coté console droite 

            x = 0

            beff_gauche = beff_b_gauche

            beff_ref_gauche = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)


            '------

            beff_droite = beff_b_droite

            beff_ref_droite = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)



            '------

            beff_totale = beff_b_totale

            beff_ref_totale = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)




            'calcul au droit de la mi travée de la console droite

            x = .LongueurTravee(2) / 2

            beff_ref_gauche = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)



            '-----

            beff_ref_droite = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '-----

            beff_ref_totale = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)





            'calcul au droit du bord libre de la travée droite

            x = .LongueurTravee(2)

            beff_ref_gauche = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)


            '-----

            beff_ref_droite = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '-----


            beff_ref_totale = .BeffDalle(x, 2, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)





            'Vérification avec le modèle simplifié pour la vérification des sections transversales

            x = 0

            beff_gauche = beff_a_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_gauche = beff_a_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_gauche = beff_b_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = .LongueurTravee(1)

            beff_gauche = beff_b_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)






            '--------



            x = 0

            beff_droite = beff_a_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_droite = beff_a_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_droite = beff_b_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = .LongueurTravee(1)

            beff_droite = beff_b_droite
            beff_ref_droite = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)








            '--------

            x = 0

            beff_totale = beff_a_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_totale = beff_a_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_totale = beff_b_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = .LongueurTravee(1)

            beff_totale = beff_b_totale
            beff_ref_totale = .BeffDalle(x, 1, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)







            'calcul au droit de l'appui B, coté console droite 

            x = 0

            beff_gauche = beff_b_gauche

            beff_ref_gauche = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)




            '-----


            x = 0

            beff_droite = beff_b_droite

            beff_ref_droite = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)




            '-----


            x = 0

            beff_totale = beff_b_totale

            beff_ref_totale = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)







            'calcul au droit de la mi travée de la console droite

            x = .LongueurTravee(2) / 2

            beff_ref_gauche = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)


            '-------


            x = .LongueurTravee(2) / 2

            beff_ref_droite = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '-------

            x = .LongueurTravee(2) / 2

            beff_ref_totale = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)





            'calcul au droit du bord libre de la travée droite

            x = .LongueurTravee(2)

            beff_ref_gauche = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)

            '-----


            x = .LongueurTravee(2)

            beff_ref_droite = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)

            '-----

            x = .LongueurTravee(2)

            beff_ref_totale = .BeffDalle(x, 2, True, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)






            'Vérification avec le modèle simplifié pour l'analyse de la poutre

            x = 0

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)

            x = .LongueurTravee(1)

            beff_gauche = beff_m_gauche
            beff_ref_gauche = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100

            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= 1)



            '-------


            x = 0

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)

            x = .LongueurTravee(1)

            beff_droite = beff_m_droite
            beff_ref_droite = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100

            Assert.IsTrue(Math.Abs(tau_beff_droite) <= 1)


            '-------


            x = 0

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.149 * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = 0.151 * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.151) * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = (1 - 0.149) * .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)

            x = .LongueurTravee(1)

            beff_totale = beff_m_totale
            beff_ref_totale = .BeffDalle(x, 1, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100

            Assert.IsTrue(Math.Abs(tau_beff_totale) <= 1)







            'calcul au droit de l'appui B, coté console droite 

            x = 0

            beff_gauche = beff_b_gauche

            beff_ref_gauche = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)


            '--------


            x = 0

            beff_droite = beff_b_droite

            beff_ref_droite = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '--------

            x = 0

            beff_totale = beff_b_totale

            beff_ref_totale = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)







            'calcul au droit de la mi travée de la console droite

            x = .LongueurTravee(2) / 2

            beff_ref_droite = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '-------


            x = .LongueurTravee(2) / 2

            beff_ref_droite = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '-------

            x = .LongueurTravee(2) / 2

            beff_ref_totale = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)




            'calcul au droit du bord libre de la travée droite

            x = .LongueurTravee(2)

            beff_ref_gauche = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
            tau_beff_gauche = (beff_gauche - beff_ref_gauche) / beff_ref_gauche * 100
            Assert.IsTrue(Math.Abs(tau_beff_gauche) <= PCLim)




            '------

            x = .LongueurTravee(2)

            beff_ref_droite = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
            tau_beff_droite = (beff_droite - beff_ref_droite) / beff_ref_droite * 100
            Assert.IsTrue(Math.Abs(tau_beff_droite) <= PCLim)


            '------

            x = .LongueurTravee(2)

            beff_ref_totale = .BeffDalle(x, 2, True, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
            tau_beff_totale = (beff_totale - beff_ref_totale) / beff_ref_totale * 100
            Assert.IsTrue(Math.Abs(tau_beff_totale) <= PCLim)

        End With

    End Sub

End Class