Imports System.Security.Cryptography

Public Class cls_VerificationsAcier


    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES ACIER
    '=========================================================================================================


#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereVb As cls_Critere                 ' Resistance voilement par cisaillement
    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereLTB As cls_Critere

    Public lCalculPlastic As Boolean                ' Indique si le dimensionnement est suivant la théorie plastique

    Public AlphaCrLTB As Decimal                    ' Alpha critique pour le déversement élastique
    Public McrLTB() As Decimal                      ' Moment critique pour le déversement (en travée)

#End Region

#Region " Constructeurs "

    Public Sub New()
        lCalculPlastic = True
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer, lElastic As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '   lElastic    [E] :   Cas d'un dimensionnement élastique VM
        '----------------------------------------------------------------------------------------------------------

        If lElastic Then
            Me.CritereSigmaA = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Else
            Me.CritereM = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        End If
        Me.CritereV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereVb = New cls_Critere(NbNodes, NbCombi, IndDerniereT)

        Me.CritereLTB = New cls_Critere(IndDerniereT + 1, NbCombi, IndDerniereT)

    End Sub

#End Region

#Region " Outils de vérification "

    Public Sub Z_VerificationELU(MyPoutre As cls_Poutre, lConstructionPhase As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre            [E] :   Poutre vérifiée
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim VplRd As Decimal
        Dim iCombi As Integer
        Dim combiELU As New cls_Combinaisons
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim MplRd, zANP As Decimal
        Dim MelRd, zANE As Decimal
        Dim lGeneration1 As Boolean = MyPoutre.Param.lGeneration1
        Dim ClasseP, ClasseM As Integer 'Classes de la section en flexion positive et négative
        Dim lClasse4 As Boolean
        Dim lSigma As Boolean
        Dim SigmaELU(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELU
        Dim SigmaCas(,,,) As Decimal = Nothing          ' Contraintes normales pour les cas de charges
        Dim lRetraitElastique As Boolean = True

        '--> Initialisations

        '# Critères

        Me.InitialiseCriteres(MyPoutre.Nodes.nbNodes, cls_Poutre.nbCombELU, MyPoutre.IndiceDerniereTravee, MyPoutre.Param.lElasticDesign)

        '# Tranchant résistant

        VplRd = MyPoutre.Section.VplRd(MyPoutre.Param.Gamma.GammaM0)

        '# Propriétés

        MyPoutre.ProprietesVerifAcier(True, MplRd, zANP, MelRd, zANE)

        '# Classes de la section

        ClasseP = MyPoutre.Section.ClasseSection(zANP, zANE, True, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1)
        ClasseM = MyPoutre.Section.ClasseSection(zANP, zANE, False, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1)

        '# Contraintes

        lSigma = MyPoutre.Param.lElasticDesign Or (ClasseP > 2) Or (ClasseM > 2)
        lSigma = True       ' EN phase debug
        If lSigma Then
            MyPoutre.PtsSigma.Initialise(MyPoutre)
            MyPoutre.PtsSigma.CalculContraintesCharges(MyPoutre, 1, SigmaCas)
        End If

        '--> Boucle sur les combinaisons

        If lConstructionPhase Then
            combiELU = MyPoutre.CombiA_ELCU
        Else
            combiELU = MyPoutre.CombiA_ELU
        End If

        For iCombi = 0 To combiELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            combiELU.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, MEd, False)

            '# Combinaison des efforts tranchants

            combiELU.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, VEd, False)

            '# Combinaisons des contraintes

            If lSigma Then
                combiELU.CombineContraintes(iCombi, MyPoutre.ChargesA.Count, MyPoutre.PtsSigma.zPos.Count, MyPoutre.Nodes.nbNodes,
                                                       MyPoutre.ChargesA, SigmaCas, lRetraitElastique, SigmaELU)
            End If

            '# Vérification sous moment fléchissant

            If MyPoutre.Param.lElasticDesign Then
                RunCritereFlexionResistanceElastiqueVM(MyPoutre, iCombi, SigmaELU)
            Else
                Me.RunCritereFlexionAcier(MyPoutre, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lClasse4)
            End If

            '# Vérification sous effort tranchant

            If MyPoutre.Param.lElasticDesign Then
            Else
                Me.RunCritereTranchants(MyPoutre, iCombi, VEd, VplRd)
            End If

            '# Vérification au déversement

            Me.RunCritereDeversement(MyPoutre, iCombi, MEd, lSigma, lConstructionPhase)

        Next

    End Sub

#End Region

#Region " Vérifications de la résistance au déversement "

    Private Sub RunCritereDeversement(myPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, lSigma As Boolean,
                                      lConstructionPhase As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au déversement
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre            [E] :   Poutre traitée
        '   iCombi              [E] :   Indice de la combinaison
        '   MEd                 [E] :   Table des moments fléchissants le long de la poutre
        '   lSigma              [E] :   Indique si calcul élastique
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim AlphaCr As Decimal
        Dim lOK As Boolean
        Dim iDebTrav As Integer = myPoutre.IndicePremiereTravee
        Dim iFinTrav As Integer = myPoutre.IndiceDerniereTravee
        Dim iTrav, iNode As Integer
        Dim iDebNod, iFinNod As Integer
        Dim MEdmax, Mcr, MRk As Decimal
        Dim MbRd, KhiLT, LambdaBLT, AlphaLT As Decimal
        Dim EN1993 As New cls_Eurocodes
        Dim zANE, InertieY As Decimal
        Dim nEqEc As Decimal

        '--> Initialisation

        ReDim Me.McrLTB(myPoutre.IndiceDerniereTravee)

        '--> Calcul Alpha Critique

        CalculAlphaCritique(myPoutre, iCombi, MEd, lConstructionPhase, AlphaCr, lOK)
        Me.AlphaCrLTB = AlphaCr

        '--> Résistance caractéristique

        If lSigma Then
            '=== ZZZ
            nEqEc = myPoutre.Section.Enrobage.Beton.CoefficientEquivalenceCT
            myPoutre.Section.ProprietesElastiquesMyy(1, False, myPoutre.Param.Gamma, nEqEc, zANE, InertieY, MRk)
        Else
            myPoutre.Section.ProprietesPlastiquesMyy(1, False, myPoutre.Param.Gamma, 0, zANE, MRk)
        End If

        '--> Vérification par travée

        For iTrav = iDebTrav To iFinTrav

            iDebNod = myPoutre.Nodes.iNodeExtTrav(iTrav, 0)
            iFinNod = myPoutre.Nodes.iNodeExtTrav(iTrav, 1)

            '# Moment maxi dans la travée

            MEdmax = Math.Max(Math.Abs(MEd(iDebNod, 1)), Math.Abs(MEd(iFinNod, 0)))

            For iNode = iDebNod + 1 To iFinNod - 1
                For k = 0 To 1
                    MEdmax = Math.Max(MEdmax, Math.Abs(MEd(iNode, k)))
                Next
            Next

            '# Moment critique

            Mcr = AlphaCr * MEdmax
            McrLTB(iTrav) = Mcr

            '# Elancement réduit

            LambdaBLT = Math.Sqrt(MRk / Mcr)

            '# Coefficient de réduction

            AlphaLT = EN1993.GetAlphaLTFromProfil(myPoutre.Section.ProfilA)
            KhiLT = EN1993.ReductionDeversement(AlphaLT, LambdaBLT)

            '# Résistance

            MbRd = KhiLT * MRk / myPoutre.Param.Gamma.GammaM1

            '# Critere

            Me.CritereLTB.EnregistreCritere(iTrav, iCombi, iTrav, MEdmax, MbRd)

        Next

    End Sub

    Private Sub CalculAlphaCritique(myPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, lConstructionPhase As Boolean,
                                    ByRef AlphaCr As Decimal, ByRef lOK As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au déversement
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre            [E] :   Poutre traitée
        '   iCombi              [E] :   Indice de la combinaisons traitée
        '   MEd                 [E] :   Diagramme de flexion
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '   AlphaCr             [S] :   Alpha Critique
        '   lOK                 [S] :   Indique si le calcul s'est bien déroulé
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim i As Integer

        Dim pDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim paramLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty
        Dim pAire, pInertieY, pInertieZ, pInertieW, pInertieT As Decimal
        Dim nEqEc, zAne, mElRd, zAneZ As Decimal
        Dim lEnrob As Boolean = myPoutre.lEnrobage
        Dim rGirPolaire As Decimal
        Dim pzS, pBetaZ As Decimal

        '--> Préparation des données pour le calcul LTBeamN

        pDonnees.EYOUNG = cls_Acier.EYACIER * kConvMPaPa
        pDonnees.GSHEAR = cls_Acier.EYACIER * kConvMPaPa / 2 / (1 + cls_Acier.NU)

        '# Noeuds 

        pDonnees.NbNodes = myPoutre.Nodes.nbNodes

        '# Position des noeuds EF

        ReDim pDonnees.xNode(pDonnees.NbNodes - 1)
        For i = 0 To pDonnees.NbNodes - 1
            pDonnees.xNode(i) = myPoutre.Nodes.xGlobal(i)
        Next

        '# Elements

        '---[ Dimensionnement des tableaux

        ReDim pDonnees.Aire(pDonnees.NbNodes - 2)
        ReDim pDonnees.InertieY(pDonnees.NbNodes - 2)
        ReDim pDonnees.RayGirPol(pDonnees.NbNodes - 2)
        ReDim pDonnees.InertieT(pDonnees.NbNodes - 2)
        ReDim pDonnees.InertieZ(pDonnees.NbNodes - 2)
        ReDim pDonnees.InertieW(pDonnees.NbNodes - 2)
        ReDim pDonnees.PositionCG(pDonnees.NbNodes - 2)
        ReDim pDonnees.CoefBetaZ(pDonnees.NbNodes - 2)
        ReDim pDonnees.MomentFle(pDonnees.NbNodes - 2, 1)

        '---[ Propriétés élémentaires

        pAire = myPoutre.Section.ProfilA.Aire
        If lEnrob Then nEqEc = myPoutre.Section.Enrobage.Beton.CoefficientEquivalenceCT
        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, nEqEc, zAne, pInertieY, mElRd)
        myPoutre.Section.ProprietesElastiquesMzz(1, True, myPoutre.Param.Gamma, nEqEc, zAneZ, pInertieZ, mElRd)
        pInertieT = myPoutre.Section.InertieT
        pInertieW = myPoutre.Section.ProfilA.InertieW
        rGirPolaire = myPoutre.Section.ProfilA.RayonGirationPolaireCalcul
        pzS = myPoutre.Section.ProfilA.PositionCentreS
        pBetaZ = myPoutre.Section.ProfilA.BetaZ

        '---[ Remplissage des tableaux

        For i = 0 To pDonnees.NbNodes - 2
            pDonnees.Aire(i) = pAire
            pDonnees.InertieY(i) = pInertieY
            pDonnees.RayGirPol(i) = rGirPolaire
            pDonnees.InertieT(i) = pInertieT
            pDonnees.InertieZ(i) = pInertieZ
            pDonnees.InertieW(i) = pInertieW
            pDonnees.CoefBetaZ(i) = pBetaZ
            pDonnees.PositionCG(i) = pzS
        Next

        '# Appuis de la poutre

        pDonnees.NbAppuis = myPoutre.Nodes.NbAppuis
        ReDim pDonnees.iNodeAppui(pDonnees.NbAppuis - 1)
        ReDim pDonnees.lAppuiArticule(pDonnees.NbAppuis - 1)

        For i = 0 To pDonnees.NbAppuis - 1
            pDonnees.iNodeAppui(i) = myPoutre.Nodes.iNodeAppui(i)
            pDonnees.lAppuiArticule(i) = False
        Next

        '# Maintiens au déversement

        paramLTB.NbMaintiensPon = myPoutre.Nodes.NbAppuis
        ReDim paramLTB.iNodeMaintienPon(paramLTB.NbMaintiensPon - 1)
        ReDim paramLTB.MaintienPonV(paramLTB.NbMaintiensPon - 1)
        ReDim paramLTB.MaintienPonTheta(paramLTB.NbMaintiensPon - 1)
        ReDim paramLTB.MaintienPonVP(paramLTB.NbMaintiensPon - 1)
        ReDim paramLTB.MaintienPonThetaP(paramLTB.NbMaintiensPon - 1)
        ReDim paramLTB.zMaintienPonC(paramLTB.NbMaintiensPon - 1)

        For i = 0 To pDonnees.NbAppuis - 1
            paramLTB.iNodeMaintienPon(i) = myPoutre.Nodes.iNodeAppui(i)
            paramLTB.MaintienPonV(i) = -1
            paramLTB.MaintienPonTheta(i) = -1
        Next

        pDonnees.NbForcesPon = 0
        pDonnees.NbForcesRep = 0
        pDonnees.NbMoments = 0

        '# Maintien par le bac en phase de construction

        If lConstructionPhase And myPoutre.lMixte Then
            Dim lEtaiement As Boolean = (myPoutre.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.FullyPropped)
            If Not lEtaiement And myPoutre.MaintienBac.lMaintienBac Then

                Dim EntraxeD As Decimal = myPoutre.EntraxeSolive
                Dim Sact As Decimal = myPoutre.MaintienBac.RigiditeShear(myPoutre.LongueurTravee(1), entraxed, myPoutre.Dalle.Bac, myPoutre.Section.Acier.EYoung)

                Dim kTheta, kThetaA, kThetaC As Decimal
                Dim bFs As Decimal = myPoutre.Section.ProfilA.Bfs

                If myPoutre.MaintienBac.lTheta Then
                    kThetaA = myPoutre.Dalle.Bac.RigiditeFlexionnelleA(myPoutre.MaintienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes, bFs)
                    kThetaC = myPoutre.Dalle.Bac.RigiditeFlexionnelleC(EntraxeD, myPoutre.lIntermediaire)
                    kTheta = 1 / (1 / kThetaA + 1 / kThetaC)
                Else
                    kTheta = 0
                End If

                paramLTB.NbMaintiensCon = 1

                ReDim paramLTB.iNodeMaintienCon(paramLTB.NbMaintiensCon - 1, 1)
                ReDim paramLTB.MaintienConV(paramLTB.NbMaintiensCon - 1)
                ReDim paramLTB.MaintienConVP(paramLTB.NbMaintiensCon - 1)
                ReDim paramLTB.MaintienConTheta(paramLTB.NbMaintiensCon - 1)
                ReDim paramLTB.zMaintienConC(paramLTB.NbMaintiensCon - 1)

                paramLTB.iNodeMaintienCon(0, 0) = 0
                paramLTB.iNodeMaintienCon(0, 1) = pDonnees.NbNodes - 1

                paramLTB.MaintienConV(0) = 0
                '# maintien en cisaillement par le bac (v')
                paramLTB.MaintienConVP(0) = Sact
                '# maintien en flexion par le bac (theta)
                paramLTB.MaintienConTheta(0) = kTheta

                paramLTB.zMaintienConC(0) = myPoutre.Section.ProfilA.hb / 2

            End If
        End If

        '# Chargements

        '**** A AJOUTER

        'ReDim .ForcePon(.NbForcesPon - 1)
        'ReDim .xForcePon(.NbForcesPon - 1)
        'ReDim .zForcePonC(.NbForcesPon - 1)
        '.ForcePon(0) = 10 * 1000        'N
        '.xForcePon(0) = 4.875
        '.zForcePonC(0) = 0.0
        '.ForcePon(1) = 10 * 1000        'N
        '.xForcePon(1) = 14.75
        '.zForcePonC(1) = 0.0

        '# Moments fléchissants

        'For j = 0 To pDonnees.NbNodes - 1

        '    For k = 0 To 1
        '        pDonnees.MomentFle(j, k) = MEd(j, k)
        '    Next

        'Next
        pDonnees.MomentFle = MEd

        '--> Lancement du calcul LTBeamN

        Call MyDLL_LTB.CALCULER(pDonnees, paramLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '--> Exploitation des résultats

        AlphaCr = MyOutput_LTB.CoefCr
        lOK = (CodeError_LTB = 0)

        If Not lOK Then
            MsgBox(TextError_LTB, MsgBoxStyle.Critical, "cls_VerificationAcier/CalculAlphaCritique")
        End If

    End Sub

    Private Sub ExtraireMaintiensLateraux(myPoutre As cls_Poutre, ByRef ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Préparation des paramètres de calcul relatifs aux maintiens latéraux
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   paramLTB    [S] :   Paramètres pour le calcul LTB
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iNode, jM As Integer
        Dim iTravee As Integer
        Dim iDebT, iFinT As Integer
        Dim cTheta, cVP, cThetaP As Decimal
        Dim lMaintien As Boolean
        Dim zMaintien As Decimal
        Dim x0 As Decimal

        '--> Initialisation

        iDebT = myPoutre.IndicePremiereTravee
        iFinT = myPoutre.IndiceDerniereTravee

        '=== APPUIS SIMPLES

        ParamLTB.NbMaintiensPon = myPoutre.Nodes.NbAppuis
        ReDim ParamLTB.iNodeMaintienPon(myPoutre.Nodes.NbAppuis - 1)
        ReDim ParamLTB.MaintienPonV(myPoutre.Nodes.NbAppuis - 1)
        ReDim ParamLTB.MaintienPonThetaP(myPoutre.Nodes.NbAppuis - 1)
        ReDim ParamLTB.MaintienPonTheta(myPoutre.Nodes.NbAppuis - 1)
        ReDim ParamLTB.MaintienPonVP(myPoutre.Nodes.NbAppuis - 1)
        ReDim ParamLTB.zMaintienPonC(myPoutre.Nodes.NbAppuis - 1)

        For iNode = 0 To myPoutre.Nodes.NbAppuis - 1
            ParamLTB.iNodeMaintienPon(iNode) = myPoutre.Nodes.iNodeAppui(iNode)
            ParamLTB.MaintienPonV(iNode) = -1
            ParamLTB.MaintienPonTheta(iNode) = -1
        Next

        '=== AUTRES MAINTIENS

        cVP = 0
        cThetaP = 0

        For iTravee = iDebT To iFinT

            x0 = myPoutre.xPositionAppui(True, iTravee)

            For jM = 0 To myPoutre.NbMaintiens(iTravee)

                lMaintien = True

                If myPoutre.Maintiens(iTravee)(jM).lMaintienSemelleInf And myPoutre.Maintiens(iTravee)(jM).lMaintienSemelleSup Then
                    cTheta = -1
                    zMaintien = 0
                ElseIf myPoutre.Maintiens(iTravee)(jM).lMaintienSemelleInf Then
                    cTheta = 0
                    zMaintien = -myPoutre.Section.ProfilA.ha / 2
                ElseIf myPoutre.Maintiens(iTravee)(jM).lMaintienSemelleSup Then
                    cTheta = 0
                    zMaintien = +myPoutre.Section.ProfilA.ha / 2
                Else
                    lMaintien = False
                End If

                iNode = myPoutre.GetIndiceNoeudFromXglobal(x0 + myPoutre.Maintiens(iTravee)(jM).x_Loc)

                '# Protection contre les erreurs
                If iNode = -1 Then
                    lMaintien = False
                    MsgBox("convergence error", MsgBoxStyle.Critical, "[cls_VerificationsAcier|GetIndiceNoeudFromXglobal]")
                End If

                If lMaintien Then _
                AjouteMaintien(ParamLTB, iNode, -1, cTheta, cVP, cThetaP, zMaintien)
            Next

        Next
    End Sub

    Private Sub AjouteMaintien(ByRef paramLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB,
                               iNode As Integer, condV As Decimal, condTheta As Decimal, condVP As Decimal, condThetaP As Decimal, Optional zMaintien As Decimal = 0)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Ajout d'une considtion de maintien latéral
        '----------------------------------------------------------------------------------------------------------
        '   paramLTB        [S] :   Paramètres LTB
        '   iNode           [E] :   Indice du noeud avec le blocage
        '   condV, condVP   [E] :   Conditions de maintien V et V prime
        '   condTheta       [E] :   Condition de maintien theta
        '   condThetaP      [E] :   Condition de maintien theta P
        '   zMaintien       [E] :   Position du maintien (par défaut cdg du profilé)
        '----------------------------------------------------------------------------------------------------------

        '--> Initialisation

        paramLTB.NbMaintiensPon += 1

        ReDim Preserve paramLTB.iNodeMaintienPon(paramLTB.NbMaintiensPon - 1)
        ReDim Preserve paramLTB.MaintienPonV(paramLTB.NbMaintiensPon - 1)
        ReDim Preserve paramLTB.MaintienPonThetaP(paramLTB.NbMaintiensPon - 1)
        ReDim Preserve paramLTB.MaintienPonTheta(paramLTB.NbMaintiensPon - 1)
        ReDim Preserve paramLTB.MaintienPonVP(paramLTB.NbMaintiensPon - 1)
        ReDim Preserve paramLTB.zMaintienPonC(paramLTB.NbMaintiensPon - 1)

        '--> Remplissage des valeurs

        paramLTB.iNodeMaintienPon(paramLTB.NbMaintiensPon - 1) = iNode

        paramLTB.MaintienPonV(paramLTB.NbMaintiensPon - 1) = condV
        paramLTB.MaintienPonThetaP(paramLTB.NbMaintiensPon - 1) = condThetaP
        paramLTB.MaintienPonTheta(paramLTB.NbMaintiensPon - 1) = condTheta
        paramLTB.MaintienPonVP(paramLTB.NbMaintiensPon - 1) = condVP
        paramLTB.zMaintienPonC(paramLTB.NbMaintiensPon - 1) = zMaintien

    End Sub


#End Region

#Region " Vérifications d'une poutre acier sans enrobage "

    Private Sub RunCritereFlexionResistanceElastiqueVM(MyPoutre As cls_Poutre, iCombi As Integer, SigmaELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   SigmaELU[E] :   Contraintes normales aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal

        Dim iPro0 As Integer = MyPoutre.PtsSigma.iProfile(0)

        '--> Initialisation

        FySup = MyPoutre.Section.FySup
        FydSup = FySup / MyPoutre.Param.Gamma.GammaM0
        FyW = MyPoutre.Section.FyW
        FydW = FyW / MyPoutre.Param.Gamma.GammaM0
        FyInf = MyPoutre.Section.FyInf
        FydInf = FyInf / MyPoutre.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydInf, FydW), Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.CritereSigmaA)
        End If

    End Sub

    Private Sub RunCritereFlexionVM(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal,
                                    SigmaU As Decimal, MyCritereM As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   iPoint  [E] :   Indice du point de calcul des contraintes
        '   SigmaELU[E] :   Contraintes normales aux ELU
        '   SigmaU  [E] :   Valeur ultime de la contrainte normale au point iPoint
        '   CritereM[E] :   Critere de la contrainte de flexion
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1
                For k = iDebK To iFinK
                    MyCritereM.EnregistreCritere(iNode, iCombi, iTravee, SigmaELU(iPoint, iNode, k), SigmaU)
                Next
            Next
        Next

        ''--> Déclaration

        'Dim iNode As Integer
        'Dim Sigma As Decimal

        ''--> Traitement

        'For iNode = 0 To MyPoutre.Nodes.nbNodes - 1

        '    If Math.Abs(SigmaELU(iPoint, iNode, 0)) > Math.Abs(SigmaELU(iPoint, iNode, 1)) Then
        '        Sigma = SigmaELU(iPoint, iNode, 0)
        '    Else
        '        Sigma = SigmaELU(iPoint, iNode, 1)
        '    End If

        '    MyCritereM.EnregistreCritere(iNode, iCombi, Sigma, SigmaU)

        'Next

    End Sub

    Private Sub RunCritereFlexionAcier(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal,
                                    MplRd As Decimal, MelRd As Decimal, ClasseP As Integer, ClasseM As Integer, ByRef lClasse4 As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MplRd   [E] :   Moment résitant plastique
        '   MelRd   [E] :   Moment élastique
        '   ClasseP [E] :   Classe de la section en flexion positive
        '   ClasseM [E] :   Classe de la section en flexion négative
        '   lClasse4[S] :   Indique qu'au moins une des sections est de classe 4
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Const SIGNEM As Decimal = 1
        Dim MRd As Decimal

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        lClasse4 = False

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    If MEd(iNode, k) * SIGNEM > 0 Then

                        Select Case ClasseP
                            Case 1, 2
                                MRd = MplRd
                            Case 3
                                MRd = MelRd
                            Case 4
                                lClasse4 = True
                                'lOk = False
                        End Select

                    Else

                        Select Case ClasseM
                            Case 1, 2
                                MRd = MplRd
                            Case 3
                                MRd = MelRd
                            Case 4
                                lClasse4 = True
                                'lOk = False
                        End Select

                    End If

                    Me.CritereM.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRd)
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VplRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   VEd     [E] :   Table des efforts tranchants le long de la barre
        '   VplRd   [E] :   Table des efforts tranchants résistant plastique le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK
                    Me.CritereV.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VplRd)
                Next
            Next
        Next

    End Sub

#End Region



End Class
