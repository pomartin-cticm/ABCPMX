Imports System.Security.Policy
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class UnitTest_ClsPoutre_EltsFinis

    '========================================================================================================================================
    '   CLASSE POUR LES ANALYSES ELTS FINIS DANS L'OBJET POUTRE
    '========================================================================================================================================

#Region " Déclarations et attributs "

    Dim NomCharges() As String = {"G1", "G2", "Q", "QC"}

#End Region

#Region " TU pour les poutres acier "

    <TestMethod()> Public Sub TU_Poutre2AppuisAcier()
        '---------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM 
        '---------------------------------------------------------------------------------------
        '   Test unitaire pour une poutre acier simple, analyse EF
        '---------------------------------------------------------------------------------------

        '--> Déclarations

        NomChargements = NomCharges

        Dim lOk As Boolean
        ' Dim pPoutre As New cls_Poutre(NomCharges)
        Dim pPoutre As New cls_Poutre()
        Const ChargeRep As Decimal = 10000
        Dim Longueur As Decimal = 10
        Dim Aire, zANE, zANP, InertieY, MelRd, MplRd As Decimal
        Dim ValRef As Decimal
        Dim MomMax, VMax, fMax, Rz As Decimal
        Const kConvMPaPa As Decimal = 10 ^ 6

        '--> Définition des caractéristiques de la poutre test

        With pPoutre

            .lTraveeConsoleGauche = False
            .lTraveeConsoleDroite = False

            .EntraxeD1 = 3
            .lTremieGauche = False


            .EntraxeD2 = 2
            .lTremieDroite = False

            .LongueurTravee(1) = Longueur

            pPoutre.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul

        End With

        '--> Définition des caractéristiques de la section

        '# IPE 300

        pPoutre.Section.ProfilA.GenereProfileIPE300()

        '--> Définition d'une chargement Q, charge uniformément répartie

        GenereChargeUniformementRepartie(1, 1, pPoutre.LongueurTravee, ChargeRep, pPoutre.ChargesU("G1"))

        '--> Génération des noeuds de calcul

        pPoutre.PrepareNodesN()

        '--> Préparation du modèle EF

        '# Maillage

        pPoutre.Analyse = New cls_AnalyseEFinis(pPoutre.Section.Acier.EYoung, pPoutre.Param.GraviteG, pPoutre.Nodes)

        '# Appuis

        pPoutre.Analyse.Appuis(pPoutre.Nodes, False)

        '# Propriétés des éléments

        pPoutre.Section.ProfilA.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)
        Aire = pPoutre.Section.ProfilA.Aire

        pPoutre.Analyse.AttribuerProprietesConstantes(InertieY, Aire)

        '# Chargements

        pPoutre.Analyse.TransfertChargementU(pPoutre.ChargesU("G1"), 1, 1, pPoutre.LongueurTravee, pPoutre.largeurinfluence)

        '--> Calcul EF

        pPoutre.Analyse.RunRDM(lOk)

        '========= TESTS SUR LES RESULTATS DES CALCULS EF ==============================

        '# Moment de flexion maxi

        MomMax = pPoutre.Analyse.MomentMax
        ValRef = ChargeRep * Longueur ^ 2 / 8

        Assert.IsTrue(IsEqual(MomMax, ValRef))

        '# Effort tranchant maxi

        VMax = pPoutre.Analyse.TranchantMax
        ValRef = ChargeRep * Longueur / 2
        Assert.IsTrue(IsEqual(VMax, ValRef))

        '# Flèche maxi

        fMax = pPoutre.Analyse.FlecheMaxAbs
        ValRef = 5 / 384 * ChargeRep * Longueur ^ 4 / (pPoutre.Section.Acier.EYoung * InertieY * kConvMPaPa)
        Assert.IsTrue(IsEqual(fMax, ValRef))

        '# Réactions

        Rz = pPoutre.Analyse.Reaction(0)
        ValRef = ChargeRep * Longueur / 2
        Assert.IsTrue(IsEqual(Rz, ValRef))
    End Sub


    <TestMethod()> Public Sub TU_Poutre2AppuisAcierChargeConcentree()
        '---------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM 
        '---------------------------------------------------------------------------------------
        '   Test unitaire pour une poutre acier simple, analyse EF
        '---------------------------------------------------------------------------------------

        '--> Déclarations

        NomChargements = NomCharges

        Dim lOk As Boolean
        'Dim pPoutre As New cls_Poutre(NomCharges)
        Dim pPoutre As New cls_Poutre()
        Const pForce As Decimal = 10000

        Dim Longueur As Decimal = 8
        Dim Aire, zANE, zANP, InertieY, MelRd, MplRd As Decimal
        Dim ValRef As Decimal
        Dim MomMax, VMax, fMax, Rz As Decimal
        Const kConvMPaPa As Decimal = 10 ^ 6

        '--> Définition des caractéristiques de la poutre test

        With pPoutre

            .lTraveeConsoleGauche = False
            .lTraveeConsoleDroite = False

            .EntraxeD1 = 3
            .lTremieGauche = False


            .EntraxeD2 = 2
            .lTremieDroite = False

            .LongueurTravee(1) = Longueur

            pPoutre.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul

        End With

        '--> Définition des caractéristiques de la section

        '# IPE 300

        pPoutre.Section.ProfilA.GenereProfileIPE300()

        '--> Définition d'une chargement Q, charge uniformément répartie

        GenereChargeConcentree(1, 0, Longueur / 2, pForce, pPoutre.ChargesU("Q1"))

        '--> Génération des noeuds de calcul

        pPoutre.PrepareNodesN()

        '--> Préparation du modèle EF

        '# Maillage

        pPoutre.Analyse = New cls_AnalyseEFinis(pPoutre.Section.Acier.EYoung, pPoutre.Param.GraviteG, pPoutre.Nodes)

        '# Appuis

        pPoutre.Analyse.Appuis(pPoutre.Nodes, False)

        '# Propriétés des éléments

        pPoutre.Section.ProfilA.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)
        Aire = pPoutre.Section.ProfilA.Aire

        pPoutre.Analyse.AttribuerProprietesConstantes(InertieY, Aire)

        '# Chargements

        pPoutre.Analyse.TransfertChargementU(pPoutre.ChargesU("Q1"), 1, 1, pPoutre.LongueurTravee, pPoutre.LargeurInfluence)

        '--> Calcul EF

        pPoutre.Analyse.RunRDM(lOk)

        '========= TESTS SUR LES RESULTATS DES CALCULS EF ==============================

        '# Moment de flexion maxi

        MomMax = pPoutre.Analyse.MomentMax
        ValRef = pForce * Longueur / 4

        Assert.IsTrue(IsEqual(MomMax, ValRef))

        '# Effort tranchant maxi

        VMax = pPoutre.Analyse.TranchantMax
        ValRef = pForce / 2
        Assert.IsTrue(IsEqual(VMax, ValRef))

        '# Flèche maxi

        fMax = pPoutre.Analyse.FlecheMaxAbs
        ValRef = pForce * Longueur ^ 3 / (48 * pPoutre.Section.Acier.EYoung * InertieY * kConvMPaPa)

        Assert.IsTrue(IsEqual(fMax, ValRef))

        '# Réactions

        Rz = pPoutre.Analyse.Reaction(0)
        ValRef = pForce / 2
        Assert.IsTrue(IsEqual(Rz, ValRef))

    End Sub

    <TestMethod()> Public Sub TU_Poutre2AppuisAcierChargeSurfacique()
        '---------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM 
        '---------------------------------------------------------------------------------------
        '   Test unitaire pour une poutre acier simple, analyse EF
        '---------------------------------------------------------------------------------------

        '--> Déclarations

        NomChargements = NomCharges

        Dim lOk As Boolean
        'Dim pPoutre As New cls_Poutre(NomCharges)
        Dim pPoutre As New cls_Poutre()
        Const QSurf As Decimal = 10000 / 5
        Dim ChargeRep As Decimal
        Dim Longueur As Decimal = 8
        Dim Aire, zANE, zANP, InertieY, MelRd, MplRd As Decimal
        Dim ValRef As Decimal
        Dim MomMax, VMax, fMax, Rz As Decimal
        Const kConvMPaPa As Decimal = 10 ^ 6

        '--> Définition des caractéristiques de la poutre test

        With pPoutre

            .lTraveeConsoleGauche = False
            .lTraveeConsoleDroite = False

            .EntraxeD1 = 3
            .lTremieGauche = False


            .EntraxeD2 = 2
            .lTremieDroite = False

            .LongueurTravee(1) = Longueur

            pPoutre.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul

        End With

        ChargeRep = QSurf * (pPoutre.EntraxeD1 + pPoutre.EntraxeD2) / 2

        '--> Définition des caractéristiques de la section

        '# IPE 300

        pPoutre.Section.ProfilA.GenereProfileIPE300()

        '--> Définition d'une chargement Q, charge uniformément répartie

        GenereChargeSurfacique(1, 1, QSurf, pPoutre.ChargesU("Q1"))

        '--> Génération des noeuds de calcul

        pPoutre.PrepareNodesN()

        '--> Préparation du modèle EF

        '# Maillage

        pPoutre.Analyse = New cls_AnalyseEFinis(pPoutre.Section.Acier.EYoung, pPoutre.Param.GraviteG, pPoutre.Nodes)

        '# Appuis

        pPoutre.Analyse.Appuis(pPoutre.Nodes, False)

        '# Propriétés des éléments

        pPoutre.Section.ProfilA.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)
        Aire = pPoutre.Section.ProfilA.Aire

        pPoutre.Analyse.AttribuerProprietesConstantes(InertieY, Aire)

        '# Chargements

        pPoutre.Analyse.TransfertChargementU(pPoutre.ChargesU("Q1"), 1, 1, pPoutre.LongueurTravee, pPoutre.LargeurInfluence)

        '--> Calcul EF

        pPoutre.Analyse.RunRDM(lOk)

        '========= TESTS SUR LES RESULTATS DES CALCULS EF ==============================

        '# Moment de flexion maxi

        MomMax = pPoutre.Analyse.MomentMax
        ValRef = ChargeRep * Longueur ^ 2 / 8

        Assert.IsTrue(IsEqual(MomMax, ValRef))

        '# Effort tranchant maxi

        VMax = pPoutre.Analyse.TranchantMax
        ValRef = ChargeRep * Longueur / 2
        Assert.IsTrue(IsEqual(VMax, ValRef))

        '# Flèche maxi

        fMax = pPoutre.Analyse.FlecheMaxAbs
        ValRef = 5 / 384 * ChargeRep * Longueur ^ 4 / (pPoutre.Section.Acier.EYoung * InertieY * kConvMPaPa)
        Assert.IsTrue(IsEqual(fMax, ValRef))

        '# Réactions

        Rz = pPoutre.Analyse.Reaction(0)
        ValRef = ChargeRep * Longueur / 2
        Assert.IsTrue(IsEqual(Rz, ValRef))
    End Sub

#End Region

#Region " TU pour les poutres mixtes "



#End Region

End Class