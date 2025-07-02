Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2

<TestClass()> Public Class TU_MV_Incendie

#Region " Variables communes "

    Dim gNomChargesA() As String = {"Permanent loads",
                                    "Self-weight",
                                    "Self weight with props", "Self weight without props",
                                    "Other permanent loads",
                                    "Live loads", "Conf. no",
                                    "Shrinkage of the slab", "Shrinkage of the encasement",
                                    "Construction loads"}
    Dim NomCas() As String = {"G1", "G2", "Q", "QC"}

    Const DeltaVMAx As Decimal = 1 / 1000   ' Valeur utilisée pour comparer les valeurs entre elles (ex: aire, moments etc.)
    Const DeltaCMAx As Decimal = 1 / 100    ' Valeur utilisée pour comparer les valeurs des critères 

    Const strRacineELU As String = "ULS"
    Const strRacineELS As String = "SLS"
    Const strRacineELF As String = "FLS"
    Const strRacineELUC As String = "ULS_C"
    Const strRacineELSC As String = "SLS_C"

#End Region

    <TestMethod()> Public Sub TestMV_F01_PoutreAcierLamineeNonProtegee()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 9

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()


        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenereProfileIPE300()

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 2, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS275EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu


#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifAcier(0).Z_VerificationELU(myBeam, False)

        '# Incendie
        myBeam.VerifFeuAcier.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        Valeur = myBeam.VerifFeuAcier.TempAStep(0)
        ValRef = 798
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Valeur = myBeam.VerifFeuAcier.TempAStep(1)
        ValRef = 939.6
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Valeur = myBeam.VerifFeuAcier.TempAStep(2)
        ValRef = 1002.7
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Valeur = myBeam.VerifFeuAcier.TempAStep(3)
        ValRef = 1046.8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Valeur = myBeam.VerifFeuAcier.TempAStep(4)
        ValRef = 1108.5
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Valeur = myBeam.VerifFeuAcier.TempAStep(5)
        ValRef = 1151.9
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        Theta = myBeam.VerifFeuAcier.TempAStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.1118
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.091
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Theta = myBeam.VerifFeuAcier.TempAStep(1)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.052
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.05854
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Theta = myBeam.VerifFeuAcier.TempAStep(2)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.03942
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0443
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Theta = myBeam.VerifFeuAcier.TempAStep(3)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0306
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.03443
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Theta = myBeam.VerifFeuAcier.TempAStep(4)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0183
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.02058
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Theta = myBeam.VerifFeuAcier.TempAStep(5)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0096
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0108
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))


#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 2.619
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.4933
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 6.4355
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 5.63
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.0596
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 11.6575
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        iStep = 2

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 7.43
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.3987
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 15.388
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F01B_PoutreAcierLamineeGalvanisee()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 9

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenereProfileIPE300()

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 2, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS275EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise


#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifAcier(0).Z_VerificationELU(myBeam, False)

        '# Incendie
        myBeam.VerifFeuAcier.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        Valeur = myBeam.VerifFeuAcier.TempAStep(0)
        ValRef = 788
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Valeur = myBeam.VerifFeuAcier.TempAStep(1)
        ValRef = 939.6
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Valeur = myBeam.VerifFeuAcier.TempAStep(2)
        ValRef = 1002.7
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Valeur = myBeam.VerifFeuAcier.TempAStep(3)
        ValRef = 1046.8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Valeur = myBeam.VerifFeuAcier.TempAStep(4)
        ValRef = 1108.5
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Valeur = myBeam.VerifFeuAcier.TempAStep(5)
        ValRef = 1151.9
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        Theta = myBeam.VerifFeuAcier.TempAStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.124
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0945
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Theta = myBeam.VerifFeuAcier.TempAStep(1)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.052
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.05854
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Theta = myBeam.VerifFeuAcier.TempAStep(2)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.03942
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0443
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Theta = myBeam.VerifFeuAcier.TempAStep(3)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0306
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.03443
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Theta = myBeam.VerifFeuAcier.TempAStep(4)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0183
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.02058
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Theta = myBeam.VerifFeuAcier.TempAStep(5)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0096
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0108
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))


#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 2.36
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.4467
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 6.0218
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 5.63
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.0596
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 11.6575
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F02_PoutreAcierPRSProtegee_Flocage()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 12

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()


        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenerePRS(0.2, 0.012, 0.476, 0.008)

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 3, True, True, False))
        myBeam.Maintiens(1).Add(New cls_Maintiens(2 * Portee / 3, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
        myBeam.ParamFeu.Protection = cls_OptionsFeu.enu_TypeProtection.LowDensitySpray_Mineral
        myBeam.ParamFeu.EpProtection = 0.02

#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifAcier(0).Z_VerificationELU(myBeam, False)

        '# Incendie
        myBeam.VerifFeuAcier.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        Valeur = myBeam.VerifFeuAcier.TempAStep(0)
        ValRef = 256
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Valeur = myBeam.VerifFeuAcier.TempAStep(1)
        ValRef = 462
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Valeur = myBeam.VerifFeuAcier.TempAStep(2)
        ValRef = 608
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Valeur = myBeam.VerifFeuAcier.TempAStep(3)
        ValRef = 709
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Valeur = myBeam.VerifFeuAcier.TempAStep(4)
        ValRef = 829
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Valeur = myBeam.VerifFeuAcier.TempAStep(5)
        ValRef = 983
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        Theta = myBeam.VerifFeuAcier.TempAStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Theta = myBeam.VerifFeuAcier.TempAStep(1)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.862
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.637
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Theta = myBeam.VerifFeuAcier.TempAStep(2)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.451
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.295
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Theta = myBeam.VerifFeuAcier.TempAStep(3)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.22
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.127
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Theta = myBeam.VerifFeuAcier.TempAStep(4)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0956
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0835
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Theta = myBeam.VerifFeuAcier.TempAStep(5)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0434
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.049
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))


#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.177
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.03844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.05018
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.335
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.205
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.04457
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.06218
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.413
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        iStep = 2

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.393
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.0853
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.12635
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.841
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F02B_PoutreAcierPRSProtegee_Peinture()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 12

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()


        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenerePRS(0.2, 0.012, 0.476, 0.008)

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 3, True, True, False))
        myBeam.Maintiens(1).Add(New cls_Maintiens(2 * Portee / 3, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
        myBeam.ParamFeu.Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint
        myBeam.ParamFeu.EpProtection = 0.005
        myBeam.ParamFeu.CustomLambdaP = 0.01

#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifAcier(0).Z_VerificationELU(myBeam, False)

        '# Incendie
        myBeam.VerifFeuAcier.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        Valeur = myBeam.VerifFeuAcier.TempAStep(0)
        ValRef = 131
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Valeur = myBeam.VerifFeuAcier.TempAStep(1)
        ValRef = 245
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Valeur = myBeam.VerifFeuAcier.TempAStep(2)
        ValRef = 347
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Valeur = myBeam.VerifFeuAcier.TempAStep(3)
        ValRef = 436
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Valeur = myBeam.VerifFeuAcier.TempAStep(4)
        ValRef = 579
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Valeur = myBeam.VerifFeuAcier.TempAStep(5)
        ValRef = 684
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        Theta = myBeam.VerifFeuAcier.TempAStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.969
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Theta = myBeam.VerifFeuAcier.TempAStep(1)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.855
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Theta = myBeam.VerifFeuAcier.TempAStep(2)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.753
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Theta = myBeam.VerifFeuAcier.TempAStep(3)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.92
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.664
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Theta = myBeam.VerifFeuAcier.TempAStep(4)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.536
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.371
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Theta = myBeam.VerifFeuAcier.TempAStep(5)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.268
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.159
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.177
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.03844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.04683
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.316
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.177
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.03844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.04986
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.333
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        iStep = 2

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.177
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.03844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.05313
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.353
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        iStep = 3

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.192
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.04176
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.05898
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.392
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F02C_PoutreAcierPRSProtegee_Panneaux()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 12

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()


        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenerePRS(0.2, 0.012, 0.476, 0.008)

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 3, True, True, False))
        myBeam.Maintiens(1).Add(New cls_Maintiens(2 * Portee / 3, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
        myBeam.ParamFeu.Protection = cls_OptionsFeu.enu_TypeProtection.BoardsPlaster
        myBeam.ParamFeu.EpProtection = 0.025
        'myBeam.ParamFeu.CustomLambdaP = 0.01

#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifAcier(0).Z_VerificationELU(myBeam, False)

        '# Incendie
        myBeam.VerifFeuAcier.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        Valeur = myBeam.VerifFeuAcier.TempAStep(0)
        ValRef = 187.2
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Valeur = myBeam.VerifFeuAcier.TempAStep(1)
        ValRef = 381
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Valeur = myBeam.VerifFeuAcier.TempAStep(2)
        ValRef = 532.5
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Valeur = myBeam.VerifFeuAcier.TempAStep(3)
        ValRef = 646.5
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Valeur = myBeam.VerifFeuAcier.TempAStep(4)
        ValRef = 765.7
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Valeur = myBeam.VerifFeuAcier.TempAStep(5)
        ValRef = 918.8
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        Theta = myBeam.VerifFeuAcier.TempAStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.913
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        Theta = myBeam.VerifFeuAcier.TempAStep(1)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.719
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        Theta = myBeam.VerifFeuAcier.TempAStep(2)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.679
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.506
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        Theta = myBeam.VerifFeuAcier.TempAStep(3)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.358
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.226
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        Theta = myBeam.VerifFeuAcier.TempAStep(4)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.151
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.104
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R240
        Theta = myBeam.VerifFeuAcier.TempAStep(5)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.0563
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.0633
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.177
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.03844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.04825
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.324
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.177
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.03844
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.05437
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.362
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        iStep = 2

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.261
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.05658
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.07864
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 0.523
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R120
        iStep = 3

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 0.494
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.10725
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereVb(iStep).CritereMax
        ValRef = 0.16187
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 1.079
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F03_PoutreMixteLamineeNonProtegee()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 9

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.Mixte

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenereProfileIPE300()

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 2, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS275EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu


#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                myBeam.VerifFeuMixte = New cls_VerifFeuMixte(myBeam.ParamFeu.MethodTempArma)
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifMixte(0).Z_VerificationELU(myBeam)

        '# Incendie
        myBeam.VerifFeuMixte.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        '-- Semelle supérieure
        Valeur = myBeam.VerifFeuMixte.TempFsStep(0)
        ValRef = 734
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Valeur = myBeam.VerifFeuMixte.TempFiStep(0)
        ValRef = 803
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Valeur = myBeam.VerifFeuMixte.TempWStep(0)
        ValRef = 825
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(0, 0)
        ValRef = 535
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(0, 1)
        ValRef = 60
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur
        Valeur = myBeam.VerifFeuMixte.TempVStep(0)
        ValRef = 587
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuMixte.TempVcStep(0)
        ValRef = 294
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))


#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        '-- Semelle supérieure
        Theta = myBeam.VerifFeuMixte.TempFsStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.189
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.116
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Theta = myBeam.VerifFeuMixte.TempFiStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.109
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Theta = myBeam.VerifFeuMixte.TempWStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.098
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Theta = myBeam.VerifFeuMixte.TempDalleStep(0, 0)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.548
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempDalleStep(0, 1)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur

        Theta = myBeam.VerifFeuMixte.TempVStep(0)

        Valeur = ENFeu.ReducFuAcier(Theta)
        ValRef = 0.51
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempVcStep(0)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.856
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Exit Sub
#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 2.619
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.4933
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 6.4355
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 5.63
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.0596
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 11.6575
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        iStep = 2

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 7.43
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.3987
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 15.388
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F03B_PoutreMixteLamineeNonProtegeeGalva()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 9

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.Mixte

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenereProfileIPE300()

        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Pleine
            .Ep_td = 120 / 1000
            .Ep_th = 0
        End With

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 2, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS275EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        'myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.AcierNu
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise

#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA

        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                myBeam.VerifFeuMixte = New cls_VerifFeuMixte(myBeam.ParamFeu.MethodTempArma)
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifMixte(0).Z_VerificationELU(myBeam)

        '# Incendie
        myBeam.VerifFeuMixte.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures du Profilé "

        '# R30
        '-- Semelle supérieure
        Valeur = myBeam.VerifFeuMixte.TempFsStep(0)
        ValRef = 709
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Valeur = myBeam.VerifFeuMixte.TempFiStep(0)
        ValRef = 794
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Valeur = myBeam.VerifFeuMixte.TempWStep(0)
        ValRef = 823
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(0, 0)
        ValRef = 535
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(0, 1)
        ValRef = 60
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur
        Valeur = myBeam.VerifFeuMixte.TempVStep(0)
        ValRef = 567
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuMixte.TempVcStep(0)
        ValRef = 284
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        '# R30
        '-- Semelle supérieure
        Theta = myBeam.VerifFeuMixte.TempFsStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.217
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.126
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Theta = myBeam.VerifFeuMixte.TempFiStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.116
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Theta = myBeam.VerifFeuMixte.TempWStep(0)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.098
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Theta = myBeam.VerifFeuMixte.TempDalleStep(0, 0)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.548
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempDalleStep(0, 1)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur

        Theta = myBeam.VerifFeuMixte.TempVStep(0)

        Valeur = ENFeu.ReducFuAcier(Theta)
        ValRef = 0.568
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempVcStep(0)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.866
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Exit Sub
#End Region

#Region " VALIDATION : Critères de résistance "

        Dim iStep As Integer

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 2.619
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 0.4933
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 6.4355
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R60
        iStep = 1

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 5.63
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.0596
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 11.6575
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R90
        iStep = 2

        Valeur = myBeam.VerifFeuAcier.CritereM(iStep).CritereMax
        ValRef = 7.43
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereV(iStep).CritereMax
        ValRef = 1.3987
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuAcier.CritereLTB(iStep).CritereMax
        ValRef = 15.388
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub

    <TestMethod()> Public Sub TestMV_F04_PoutreAcierPRSProtegee_Flocage()

#Region " Preparation de la poutre "

        Dim myBeam As New cls_Poutre(NomCas)
        Dim ValRef, Valeur As Decimal
        Const Portee As Decimal = 14

        Dim qQ1 As Decimal = 5000

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.Mixte

        '# GEOMETRIE

        myBeam.lTraveeConsoleGauche = False
        myBeam.lTraveeConsoleDroite = False

        myBeam.LongueurTravee(1) = Portee     ' travée centrale

        myBeam.lIntermediaire = True

        myBeam.Section.ProfilA.GenerePRS(0.2, 0.02, 0.56, 0.01)

        '# DALLE
        With myBeam.Dalle
            .type = cls_Dalle.Enum_TypeDalle.Mixte
            .Ep_td = 140 / 1000
            .Ep_th = 0
            .Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
            .beton.Classe = "C30/37"
        End With

        '# CONNEXION
        With myBeam.Dalle.Goujons
            .d = 0.022
            .hsc = 0.125
        End With
        myBeam.InitialiseConnexionDefaut()
        myBeam.NrTransZone(1, 0) = 2

        '# MAINTIENS LATERAUX

        myBeam.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        myBeam.Maintiens(1).Add(New cls_Maintiens(1 * Portee / 3, True, True, False))
        myBeam.Maintiens(1).Add(New cls_Maintiens(2 * Portee / 3, True, True, False))

        '# MATERIAUX
        myBeam.Section.Acier.InitialiseAcierS355EC3()

        '# CHARGES
        myBeam.InitialisePoidsPropres()

        myBeam.ChargesU("Q1").FReparties(1).Add(New cls_ForceRepartie(0, qQ1, Portee, qQ1, 0))

        '# COEFFICIENTS PARTIELS
        myBeam.Initialise_CoefficientsCombinaisons()          ' Initialise les coefficients par défaut 
        myBeam.lCombELU(0) = True                             ' activation de la première combinaison ELU par défaut (1.35G + 1.5Q1+Psi0Q2)
        myBeam.lCombELU(1) = False                             ' activation de la seconde combinaison ELU par défaut (1.35G + 1.5Q2+psi0Q1)
        myBeam.lCombELS(0) = True                             ' activation de la première combinaison ELS par défaut (G + Q)
        myBeam.lCombELCURules(0) = False                      ' activation de la première combinaison ELU pendant la phase de construction activée 
        myBeam.lCombELCSRules(0) = False                      ' activation de la première combinaison ELS pendant la phase de construction activée 
        myBeam.lCombFeu(4) = True

        myBeam.CoefCombFeu(4)(0) = 0
        myBeam.CoefCombFeu(4)(1) = 1
        myBeam.CoefCombFeu(4)(2) = 0
        myBeam.CoefCombFeu(4)(3) = 0
        myBeam.CoefCombFeu(4)(4) = 0

        With myBeam.Param.Gamma
            .GammaG_sup = 1.4
            .GammaQ = 1.6
            .GammaM0 = 1.05
            .GammaM1 = 1.1
            .GammaC = 1.5
            .lGammaV_unique = True
            .GammaVc = 1.25
            .GammaVs = 1.25
            .Psi0_Q1 = 0.7
            .Psi0_Q2 = 0.7
            .GammaM_fi = 1
        End With

        myBeam.Param.EtaW = 1.0

        myBeam.Initialise_CoefficientsCombinaisons()

        myBeam.ParamFeu.lCalculFeu = True
        myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege
        myBeam.ParamFeu.Protection = cls_OptionsFeu.enu_TypeProtection.HighDensitySpray_PerliteCement
        myBeam.ParamFeu.EpProtection = 0.025

#End Region

#Region " Lancement des calculs "

        'Dim strRacineELU, strRacineELS, strRacineELF, strRacineELUC, strRacineELSC As String
        Dim NomChargesA() As String = gNomChargesA


        'INITIALISATION DES TABLEAUX DES VERIFICATION
        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim myBeam.VerifAcier(0)
                myBeam.VerifAcier(0) = New cls_VerificationsAcier
                myBeam.VerifFeuAcier = New cls_VerifFeuAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim myBeam.VerifMixte(0)
                myBeam.VerifMixte(0) = New cls_VerificationsMixtes
                myBeam.VerifFeuMixte = New cls_VerifFeuMixte(myBeam.ParamFeu.MethodTempArma)
                If myBeam.TypeEtaiement <> cls_Poutre.EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim myBeam.VerifAcier(0)
                    myBeam.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        'INITIALISATION DES CALCULS
        myBeam.InitialiseCalculs(NomChargesA)
        myBeam.AAA_CalculMNVInternesN()
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELU, myBeam.lCombELU, myBeam.CoefCombELU, strRacineELU, myBeam.CombiA_ELU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELS, myBeam.lCombELS, myBeam.CoefCombELS, strRacineELS, myBeam.CombiA_ELS)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombFeu, myBeam.lCombFeu, myBeam.CoefCombFeu, strRacineELF, myBeam.CombiA_ELF)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, myBeam.lCombELCURules, myBeam.CoefCombELCU, strRacineELUC, myBeam.CombiA_ELCU)
        myBeam.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, myBeam.lCombELCSRules, myBeam.CoefCombELCS, strRacineELSC, myBeam.CombiA_ELCS)

        'COMBINAISON DES EFFORTS A L'ELU Indencie
        Dim MEd1(,) As Decimal = Nothing
        Dim MEdMax1, MEdMin1, iNodeMMin1, iNodeMMax1 As Decimal
        Dim MEdMiTravee As Decimal

        Dim VEd1(,) As Decimal = Nothing
        Dim VEdMax1, VEdMin1, iNodeVMin1, iNodeVMax1 As Decimal
        Dim VEdAppui As Decimal

        myBeam.CombiA_ELF.CombineMoments(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd1, False)   ' Combinaison des moments pour la combinaison 0
        myBeam.CombiA_ELF.CombineEffortsT(0, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd1, False)  ' Combinaison des tranchants pour la combinaison 0

        EnveloppeTableauEfforts(MEd1, myBeam.Nodes.nbNodes, MEdMax1, MEdMin1, iNodeMMax1, iNodeMMin1)
        EnveloppeTableauEfforts(VEd1, myBeam.Nodes.nbNodes, VEdMax1, VEdMin1, iNodeVMax1, iNodeVMin1)

        MEdMiTravee = MEdMax1
        VEdAppui = VEdMax1

        'VERIFICATION DE LA POUTRE 

        '# ELU
        myBeam.VerifMixte(0).Z_VerificationELU(myBeam)

        '# Incendie
        myBeam.VerifFeuMixte.Z_VerifFeu(myBeam)

#End Region

#Region " VALIDATION : Températures "

        Dim iStep As Integer = 0
        Dim EN_Feu As New cls_EurocodesFeu

        '# R30
        '-- Semelle supérieure
        Valeur = myBeam.VerifFeuMixte.TempFsStep(iStep)
        ValRef = 92.1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Valeur = myBeam.VerifFeuMixte.TempFiStep(iStep)
        ValRef = 136.2
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Valeur = myBeam.VerifFeuMixte.TempWStep(iStep)
        ValRef = 194.3
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(iStep, 0)
        ValRef = 535
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(iStep, 1)
        ValRef = 60
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur
        Valeur = myBeam.VerifFeuMixte.TempVStep(iStep)
        ValRef = 74
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuMixte.TempVcStep(iStep)
        ValRef = 37
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))


        '# R180
        iStep = 4
        '-- Semelle supérieure
        Valeur = myBeam.VerifFeuMixte.TempFsStep(iStep)
        ValRef = 479
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Valeur = myBeam.VerifFeuMixte.TempFiStep(iStep)
        ValRef = 643.5
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Valeur = myBeam.VerifFeuMixte.TempWStep(iStep)
        ValRef = 750.6
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(iStep, 0)
        ValRef = Math.Min(1200, EN_Feu.TemperatureGazISO_Minutes(180))
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))
        Valeur = myBeam.VerifFeuMixte.TempDalleStep(iStep, 1)
        ValRef = 260
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur
        Valeur = myBeam.VerifFeuMixte.TempVStep(iStep)
        ValRef = 383
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuMixte.TempVcStep(iStep)
        ValRef = 192
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Coefficients de réduction "

        Dim Theta As Decimal
        Dim ENFeu As New cls_EurocodesFeu

        iStep = 0
        '# R30
        '-- Semelle supérieure
        Theta = myBeam.VerifFeuMixte.TempFsStep(iStep)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Theta = myBeam.VerifFeuMixte.TempFiStep(iStep)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Theta = myBeam.VerifFeuMixte.TempWStep(iStep)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Theta = myBeam.VerifFeuMixte.TempDalleStep(iStep, 0)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.548
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempDalleStep(iStep, 1)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur

        Theta = myBeam.VerifFeuMixte.TempVStep(iStep)

        Valeur = ENFeu.ReducFuAcier(Theta)
        ValRef = 1.25
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempVcStep(iStep)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 1
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))


        iStep = 4
        '# R180 ############################################################

        '-- Semelle supérieure
        Theta = myBeam.VerifFeuMixte.TempFsStep(iStep)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.826
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = ENFeu.ReducEyAcier(Theta)
        ValRef = 0.621
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Semelle inférieure
        Theta = myBeam.VerifFeuMixte.TempFiStep(iStep)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.366
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Âme
        Theta = myBeam.VerifFeuMixte.TempWStep(iStep)

        Valeur = ENFeu.ReducFyAcier(Theta)
        ValRef = 0.169
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Dalle
        Theta = myBeam.VerifFeuMixte.TempDalleStep(iStep, 0)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0
        '  Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempDalleStep(iStep, 1)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.89
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '-- Connecteur

        Theta = myBeam.VerifFeuMixte.TempVStep(iStep)

        Valeur = ENFeu.ReducFuAcier(Theta)
        ValRef = 1.042
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Theta = myBeam.VerifFeuMixte.TempVcStep(iStep)

        Valeur = ENFeu.ReducFckBeton(Theta, False, False)
        ValRef = 0.954
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

#Region " VALIDATION : Critères de résistance "

        '# R30
        iStep = 0

        Valeur = myBeam.VerifFeuMixte.CritereM(iStep).CritereMax
        ValRef = 0.0634
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuMixte.CritereV(iStep).CritereMax
        ValRef = 0.03049
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        '# R180
        iStep = 4

        Valeur = myBeam.VerifFeuMixte.CritereM(iStep).CritereMax
        ValRef = 0.184
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

        Valeur = myBeam.VerifFeuMixte.CritereV(iStep).CritereMax
        ValRef = 0.18
        Assert.IsTrue(IsEqual(Valeur, ValRef, 0.005))

#End Region

    End Sub





End Class