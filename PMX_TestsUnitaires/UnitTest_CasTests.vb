Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_CasTests

    <TestMethod()> Public Sub Test_RCM_2018_2()

        'Cas test issu de la revue RCM (2008-2):
        '
        '
        '"Calcul d'une poutre mixte sur appuis simples suivant l'EN 1994-1-1"

        'Initialisation
        Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
        Dim myPoutre As New cls_Poutre(NomCas)

        ' --> Renseignement des données de l'article 

        'GEOMETRIE
        myPoutre.lTraveeConsoleGauche = False
        myPoutre.lTraveeConsoleDroite = False
        myPoutre.LongueurTravee(myPoutre.IndicePremiereTravee) = 14 '14 m
        myPoutre.lTremieGauche = False
        myPoutre.lTremieDroite = False
        myPoutre.lIntermediaire = True
        myPoutre.EntraxeD1 = 3
        myPoutre.EntraxeD2 = 3

        With myPoutre.Section.ProfilA
            .typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine
            .ha = 450 / 1000
            .Bfs = 190 / 1000
            .Bfi = .Bfs
            .Tfs = 14.6 / 1000
            .Tfi = .Tfs
            .Rcs = 21 / 1000
            .Rci = .Rcs
            .InitialiseProprietes()
        End With

        With myPoutre.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .t_d = 120 / 1000
        End With

        With myPoutre.Dalle.Bac
            .Hp = 58 / 1000
            .h_rs = 0
            .Tp = 0.75 / 1000
            .Bb = 62 / 1000
            .Bt = 101 / 1000
            .Orientation = .Enum_Orientation.Perpendiculaire
        End With

        With myPoutre.Dalle.Connecteur
            .hsc = 100 / 1000
            .d = 19 / 1000
        End With

        'MATERIAUX
        With myPoutre.Section.Acier
            .f_y.fs = 275
            .f_y.w = 275
            .f_y.fi = 275
        End With

        With myPoutre.Dalle.beton
            .Classe = "C25/30"
        End With

        myPoutre.Dalle.Connecteur.Fu = 450

        myPoutre.Dalle.Bac.fyp = 450

        'CHARGES
        myPoutre.ChargesU("G2").QSurf(myPoutre.IndicePremiereTravee) = 1.4 * 1000
        myPoutre.ChargesU("Q1").QSurf(myPoutre.IndicePremiereTravee) = 2.5 * 1000
        myPoutre.ChargesU("QC").QSurf(myPoutre.IndicePremiereTravee) = 0.5 * 1000
        myPoutre.ChargesU("QC").FReparties(myPoutre.IndicePremiereTravee).Add(New cls_ForceRepartie(12.5, 1 * 3, 15.5, 1 * 3, 12.5))

        'COEFFICIENTS PARTIELS
        myPoutre.lCombELU(0) = True
        myPoutre.CoefCombELU(0)(0) = 1.35
        myPoutre.CoefCombELU(0)(1) = 1.35
        myPoutre.CoefCombELU(0)(2) = 1.5
        myPoutre.CoefCombELU(0)(3) = 1.5

        With myPoutre.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

    End Sub

End Class