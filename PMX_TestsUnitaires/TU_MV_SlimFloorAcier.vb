Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class TU_MV_SlimFloorAcier

#Region " Variables communes "

    Dim NomCas() As String = {"G1", "G2", "Q", "QC"}
    'Const DeltaVMAx As Decimal = 1 / 1000 'Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
    'Const DeltaCMAx As Decimal = 1 / 100 'Valeur utilisée pour comparer les valeurs des critères 

    Dim gNomChargesA() As String = {"Permanent loads",
                                    "Self-weight",
                                    "Self weight with props", "Self weight without props",
                                    "Other permanent loads",
                                    "Live loads", "Conf. no",
                                    "Shrinkage of the slab", "Shrinkage of the encasement",
                                    "Construction loads"}

    Const strRacineELU As String = "ULS"
    Const strRacineELS As String = "SLS"
    Const strRacineELF As String = "FLS"
    Const strRacineELUC As String = "ULS_C"
    Const strRacineELSC As String = "SLS_C"

#End Region

    <TestMethod()> Public Sub TU_MV_TESTSFS01()

#Region " Initialisation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal

        myBeam.Initialise_CoefficientsCombinaisons()
        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.SFB
        OptionsSlimFloor.Bappmin = 50 / 1000

#End Region

#Region " Renseignement des données "

        '# GEOMETRIE
        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False
        myBeam.LongueurTravee(1) = 6
        myBeam.lTremieGauche = False
        myBeam.lTremieDroite = False
        myBeam.lIntermediaire = True
        myBeam.EntraxeD1 = 2
        myBeam.EntraxeD2 = 2

        myBeam.Section.ProfilA.GenereProfileIPE300()
        myBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 350 / 1000
            .Ep_th = 0
        End With

        myBeam.Section.ProfilA.Plat_b = 300 / 1000
        myBeam.Section.ProfilA.Plat_t = 15 / 1000
        myBeam.Section.ProfilA.ha = myBeam.Section.ProfilA.hb + myBeam.Section.ProfilA.Plat_t

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355EC3()
        myBeam.Section.AcierPlat.InitialiseAcierS355EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()
        For i As Integer = 0 To 2
            myBeam.ChargesU("G1").QSurf(i) = 0
            myBeam.ChargesU("Q1").QSurf(i) = 20 / 2 * 1000
        Next

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 

        With myBeam.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

        myBeam.Param.EtaW = 1
        myBeam.Param.lEnrobProp = True

#End Region

#Region " Lancement des calculs "

        Dim NomChargesA() As String = gNomChargesA

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                'ReDim myPoutre.VerifAcier(0)
                'myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                'ReDim myPoutre.VerifMixte(0)
                'myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                'If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                '    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                '    ReDim myPoutre.VerifAcier(0)
                '    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                'End If
            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SFB
                ReDim myBeam.VerifSlimAcier(0)
                myBeam.VerifSlimAcier(0) = New cls_VerificationSlimAcier

        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd1(,) As Decimal = Nothing
        Dim MEd2(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMax2, MEdMin2, iNodeMMin2, iNodeMMax2 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEd2(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdMax2, VEdMin2, iNodeVMin2, iNodeVMax2 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELU.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        'myBeam.CombiA_ELU.CombineMoments(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd2, False)   ' Combinaison des moments pour la combinaison 1
        myBeam.CombiA_ELU.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0
        'myBeam.CombiA_ELU.CombineEffortsT(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd2, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        'EnveloppeTableauEfforts(MEd2, myBeam.Nodes.nbNodes, MEdMax2, MEdMin2, iNodeMMax2, iNodeMMin2)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)
        ' EnveloppeTableauEfforts(VEd2, myBeam.Nodes.nbNodes, VEdMax2, VEdMin2, iNodeVMax2, iNodeVMin2)

        MEdMiTravee = Math.Max(MEdMax1, MEdMax2)
        'MEdMiTravee = Math.Min(MEdMin1, MEdMin2)
        VEdAppui = Math.Max(VEdMax1, VEdMax2)

        'VERIFICATION DE LA POUTRE 

        myBeam.VerifSlimAcier(0).Z_VerificationELU(myBeam, False)

#End Region

#Region " VALIDATION : Analyse de la poutre "

        Valeur = MEdMiTravee
        ValRef = 234.8 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

        Valeur = VEdAppui
        ValRef = 156.5 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

#Region " VALIDATION : Résistances (ELU)"

        Dim zANE, MRd As Decimal
        myBeam.Section.ProprietesPlastiquesMyy_Slim(1, True, myBeam.Param.Gamma, 0, zANE, MRd)

        Valeur = MRd
        ValRef = 298.1 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef))              'Vérification du calcul de la résistance plastique à la flexion simple du profilé

        Valeur = myBeam.VerifSlimAcier(0).CritereM.CritereMax
        ValRef = 1.015
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion

        Valeur = myBeam.VerifSlimAcier(0).CritereV.CritereMax
        ValRef = 0.297
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à l'effort tranchant

        Valeur = myBeam.VerifSlimAcier(0).CritereMY.CritereMax
        ValRef = 0.504
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion locale

#End Region

#Region " VALIDATION : Flèches (ELS)"

        '--> Fleches due à G1

        Valeur = myBeam.ChargesA(0).FlecheMax * 1000
        ValRef = 9.14
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à Q1

        Valeur = myBeam.ChargesA(1).FlecheMax * 1000
        ValRef = 11.1
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

#End Region

    End Sub

    <TestMethod()> Public Sub TU_MV_TESTSFS01B()

#Region " Initialisation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal

        myBeam.Initialise_CoefficientsCombinaisons()
        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.SFB
        OptionsSlimFloor.Bappmin = 50 / 1000

#End Region

#Region " Renseignement des données "

        '# GEOMETRIE
        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False
        myBeam.LongueurTravee(1) = 6
        myBeam.lTremieGauche = False
        myBeam.lTremieDroite = False
        myBeam.lIntermediaire = True
        myBeam.EntraxeD1 = 2
        myBeam.EntraxeD2 = 2

        myBeam.Section.ProfilA.GenereProfileIPE300()
        myBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 350 / 1000
            .Ep_th = 0
        End With

        myBeam.Section.ProfilA.Plat_b = 300 / 1000
        myBeam.Section.ProfilA.Plat_t = 15 / 1000
        myBeam.Section.ProfilA.ha = myBeam.Section.ProfilA.hb + myBeam.Section.ProfilA.Plat_t

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS235EC3()
        myBeam.Section.AcierPlat.InitialiseAcierS235EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()
        For i As Integer = 0 To 2
            myBeam.ChargesU("G1").QSurf(i) = 0
            myBeam.ChargesU("Q1").QSurf(i) = 20 / 2 * 1000
        Next

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 

        With myBeam.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

        myBeam.Param.EtaW = 1
        myBeam.Param.lEnrobProp = True

#End Region

#Region " Lancement des calculs "

        Dim NomChargesA() As String = gNomChargesA

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                'ReDim myPoutre.VerifAcier(0)
                'myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                'ReDim myPoutre.VerifMixte(0)
                'myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                'If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                '    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                '    ReDim myPoutre.VerifAcier(0)
                '    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                'End If
            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SFB
                ReDim myBeam.VerifSlimAcier(0)
                myBeam.VerifSlimAcier(0) = New cls_VerificationSlimAcier

        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd1(,) As Decimal = Nothing
        Dim MEd2(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMax2, MEdMin2, iNodeMMin2, iNodeMMax2 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEd2(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdMax2, VEdMin2, iNodeVMin2, iNodeVMax2 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELU.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        'myBeam.CombiA_ELU.CombineMoments(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd2, False)   ' Combinaison des moments pour la combinaison 1
        myBeam.CombiA_ELU.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0
        'myBeam.CombiA_ELU.CombineEffortsT(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd2, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        'EnveloppeTableauEfforts(MEd2, myBeam.Nodes.nbNodes, MEdMax2, MEdMin2, iNodeMMax2, iNodeMMin2)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)
        ' EnveloppeTableauEfforts(VEd2, myBeam.Nodes.nbNodes, VEdMax2, VEdMin2, iNodeVMax2, iNodeVMin2)

        MEdMiTravee = Math.Max(MEdMax1, MEdMax2)
        'MEdMiTravee = Math.Min(MEdMin1, MEdMin2)
        VEdAppui = Math.Max(VEdMax1, VEdMax2)

        'VERIFICATION DE LA POUTRE 

        myBeam.VerifSlimAcier(0).Z_VerificationELU(myBeam, False)

#End Region

#Region " VALIDATION : Analyse de la poutre "

        Valeur = MEdMiTravee
        ValRef = 234.8 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

        Valeur = VEdAppui
        ValRef = 156.5 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

#Region " VALIDATION : Résistances (ELU)"

        Dim zANE, MRd As Decimal
        myBeam.Section.ProprietesPlastiquesMyy_Slim(1, True, myBeam.Param.Gamma, 0, zANE, MRd)

        Valeur = MRd
        ValRef = 197.3 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef))              'Vérification du calcul de la résistance plastique à la flexion simple du profilé

        Valeur = myBeam.VerifSlimAcier(0).CritereM.CritereMax
        ValRef = 1.196
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion

        Valeur = myBeam.VerifSlimAcier(0).CritereV.CritereMax
        ValRef = 0.449
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à l'effort tranchant

        Valeur = myBeam.VerifSlimAcier(0).CritereMY.CritereMax
        ValRef = 0.274
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion locale

#End Region

#Region " VALIDATION : Flèches (ELS)"

        '--> Fleches due à G1

        Valeur = myBeam.ChargesA(0).FlecheMax * 1000
        ValRef = 9.14
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à Q1

        Valeur = myBeam.ChargesA(1).FlecheMax * 1000
        ValRef = 11.1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

    End Sub

    <TestMethod()> Public Sub TU_MV_TESTSFS02()


#Region " Initialisation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal

        myBeam.Initialise_CoefficientsCombinaisons()
        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.IFB_A
        OptionsSlimFloor.Bappmin = 50 / 1000

#End Region

#Region " Renseignement des données "

        '# GEOMETRIE
        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False
        myBeam.LongueurTravee(1) = 8
        myBeam.lTremieGauche = False
        myBeam.lTremieDroite = False
        myBeam.lIntermediaire = True
        myBeam.EntraxeD1 = 1.8
        myBeam.EntraxeD2 = 1.8

        myBeam.Section.ProfilA.GenererProfileHEB400()
        myBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
            .Ep_td = 270 / 1000
            .Ep_th = 0
            .preDalle_ep = 50 / 1000
        End With

        myBeam.Section.ProfilA.Plat_b = 500 / 1000
        myBeam.Section.ProfilA.Plat_t = 20 / 1000
        myBeam.Section.ProfilA.ha = 250 / 1000

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355MML()
        myBeam.Section.AcierPlat.InitialiseAcierS355MML()

        '# CHARGES
        myBeam.InitialisePoidsPropres()
        For i As Integer = 0 To 2
            myBeam.ChargesU("G1").QSurf(i) = 0
            myBeam.ChargesU("Q1").QSurf(i) = 30 / 1.8 * 1000
        Next

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 

        With myBeam.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

        myBeam.Param.EtaW = 1
        myBeam.Param.lEnrobProp = True

#End Region

#Region " Lancement des calculs "

        Dim NomChargesA() As String = gNomChargesA

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                'ReDim myPoutre.VerifAcier(0)
                'myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                'ReDim myPoutre.VerifMixte(0)
                'myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                'If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                '    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                '    ReDim myPoutre.VerifAcier(0)
                '    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                'End If
            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SFB
                ReDim myBeam.VerifSlimAcier(0)
                myBeam.VerifSlimAcier(0) = New cls_VerificationSlimAcier

        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd1(,) As Decimal = Nothing
        Dim MEd2(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMax2, MEdMin2, iNodeMMin2, iNodeMMax2 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEd2(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdMax2, VEdMin2, iNodeVMin2, iNodeVMax2 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELU.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        'myBeam.CombiA_ELU.CombineMoments(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd2, False)   ' Combinaison des moments pour la combinaison 1
        myBeam.CombiA_ELU.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0
        'myBeam.CombiA_ELU.CombineEffortsT(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd2, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        'EnveloppeTableauEfforts(MEd2, myBeam.Nodes.nbNodes, MEdMax2, MEdMin2, iNodeMMax2, iNodeMMin2)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)
        ' EnveloppeTableauEfforts(VEd2, myBeam.Nodes.nbNodes, VEdMax2, VEdMin2, iNodeVMax2, iNodeVMin2)

        MEdMiTravee = Math.Max(MEdMax1, MEdMax2)
        'MEdMiTravee = Math.Min(MEdMin1, MEdMin2)
        VEdAppui = Math.Max(VEdMax1, VEdMax2)

        'VERIFICATION DE LA POUTRE 

        myBeam.VerifSlimAcier(0).Z_VerificationELU(myBeam, False)

#End Region

#Region " VALIDATION : Analyse de la poutre "

        Valeur = MEdMiTravee
        ValRef = 505.6 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

        Valeur = VEdAppui
        ValRef = 252.8 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

#Region " VALIDATION : Résistances (ELU)"

        Dim zANE, MRd As Decimal
        myBeam.Section.ProprietesPlastiquesMyy_Slim(1, True, myBeam.Param.Gamma, 0, zANE, MRd)

        Valeur = MRd
        ValRef = 695.9 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef))              'Vérification du calcul de la résistance plastique à la flexion simple du profilé

        Valeur = myBeam.VerifSlimAcier(0).CritereM.CritereMax
        ValRef = 0.736
        '  Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion

        Valeur = myBeam.VerifSlimAcier(0).CritereV.CritereMax
        ValRef = 0.325
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à l'effort tranchant

        Valeur = myBeam.VerifSlimAcier(0).CritereMY.CritereMax
        ValRef = 0.24
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion locale

#End Region

#Region " VALIDATION : Flèches (ELS)"

        '--> Fleches due à G1

        Valeur = myBeam.ChargesA(0).FlecheMax * 1000
        ValRef = 14.71
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à Q1

        Valeur = myBeam.ChargesA(1).FlecheMax * 1000
        ValRef = 32.74
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

    End Sub


    <TestMethod()> Public Sub TU_MV_TESTSFS03()

        '=================================================================================================================================
        ' Test Unitaire d'une slim floor acier IFB-B
        '=================================================================================================================================

#Region " Initialisation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal

        myBeam.Initialise_CoefficientsCombinaisons()
        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.IFB_B
        OptionsSlimFloor.Bappmin = 50 / 1000

#End Region

#Region " Renseignement des données "

        '# GEOMETRIE
        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False
        myBeam.LongueurTravee(1) = 7
        myBeam.lTremieGauche = False
        myBeam.lTremieDroite = False
        myBeam.lIntermediaire = True
        myBeam.EntraxeD1 = 1.5
        myBeam.EntraxeD2 = 1.5

        myBeam.Section.ProfilA.GenererProfileHEB300()
        myBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 270 / 1000
            .Ep_th = 0
            .preDalle_ep = 0 / 1000
        End With

        myBeam.Section.ProfilA.Plat_b = 150 / 1000
        myBeam.Section.ProfilA.Plat_t = 20 / 1000
        myBeam.Section.ProfilA.ha = 250 / 1000

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355MML()
        myBeam.Section.AcierPlat.InitialiseAcierS355MML()

        '# CHARGES
        myBeam.InitialisePoidsPropres()
        For i As Integer = 0 To 2
            myBeam.ChargesU("G1").QSurf(i) = 0
            myBeam.ChargesU("Q1").QSurf(i) = 15 / 1.5 * 1000
        Next

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 

        With myBeam.Param.Gamma
            .GammaM0 = 1
            .GammaM1 = 1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
        End With

        myBeam.Param.EtaW = 1
        myBeam.Param.lEnrobProp = True

#End Region

#Region " Lancement des calculs "

        Dim NomChargesA() As String = gNomChargesA

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                'ReDim myPoutre.VerifAcier(0)
                'myPoutre.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                'ReDim myPoutre.VerifMixte(0)
                'myPoutre.VerifMixte(0) = New cls_VerificationsMixtes
                'If myPoutre.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                '    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                '    ReDim myPoutre.VerifAcier(0)
                '    myPoutre.VerifAcier(0) = New cls_VerificationsAcier
                'End If
            Case cls_Section.Enum_TypeSection.IFB_A, cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.SAB, cls_Section.Enum_TypeSection.SFB
                ReDim myBeam.VerifSlimAcier(0)
                myBeam.VerifSlimAcier(0) = New cls_VerificationSlimAcier

        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU
        Dim MEd1(,) As Decimal = Nothing
        Dim MEd2(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMax2, MEdMin2, iNodeMMin2, iNodeMMax2 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEd2(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdMax2, VEdMin2, iNodeVMin2, iNodeVMax2 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELU.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        'myBeam.CombiA_ELU.CombineMoments(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd2, False)   ' Combinaison des moments pour la combinaison 1
        myBeam.CombiA_ELU.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0
        'myBeam.CombiA_ELU.CombineEffortsT(1, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd2, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        'EnveloppeTableauEfforts(MEd2, myBeam.Nodes.nbNodes, MEdMax2, MEdMin2, iNodeMMax2, iNodeMMin2)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)
        ' EnveloppeTableauEfforts(VEd2, myBeam.Nodes.nbNodes, VEdMax2, VEdMin2, iNodeVMax2, iNodeVMin2)

        MEdMiTravee = Math.Max(MEdMax1, MEdMax2)
        'MEdMiTravee = Math.Min(MEdMin1, MEdMin2)
        VEdAppui = Math.Max(VEdMax1, VEdMax2)

        'VERIFICATION DE LA POUTRE 

        myBeam.VerifSlimAcier(0).Z_VerificationELU(myBeam, False)

#End Region

#Region " VALIDATION : Analyse de la poutre "

        Valeur = MEdMiTravee
        ValRef = 227.2 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

        Valeur = VEdAppui
        ValRef = 129.8 * 10 ^ 3
        'Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

#Region " VALIDATION : Résistances (ELU)"

        Dim zANE, MRd As Decimal
        myBeam.Section.ProprietesPlastiquesMyy_Slim(1, True, myBeam.Param.Gamma, 0, zANE, MRd)

        Valeur = MRd
        ValRef = 332.5 * 1000
        Assert.IsTrue(IsEqual(Valeur, ValRef))              'Vérification du calcul de la résistance plastique à la flexion simple du profilé

        Valeur = myBeam.VerifSlimAcier(0).CritereM.CritereMax
        ValRef = 0.685
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion

        Valeur = myBeam.VerifSlimAcier(0).CritereV.CritereMax
        ValRef = 0.2
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à l'effort tranchant

        'Valeur = myBeam.VerifSlimAcier(0).CritereMY.CritereMax
        'ValRef = 0.175
        'Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))              'Vérification du critère de la résistance à la flexion locale

#End Region

#Region " VALIDATION : Flèches (ELS)"

        '--> Fleches due à G1

        Valeur = myBeam.ChargesA(0).FlecheMax * 1000
        ValRef = 13.732
        Assert.IsTrue(IsEqual(Valeur, ValRef, DeltaVMAx))

        '--> Fleches due à Q1

        Valeur = myBeam.ChargesA(1).FlecheMax * 1000
        ValRef = 19.06
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.01))

#End Region

    End Sub

End Class