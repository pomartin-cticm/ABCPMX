Public Class cls_CalculCritique

#Region " Calcul Alpha critique au déversement "

    Public Sub CalculAlphaCritique(myPoutre As cls_Poutre, CoefCombi As List(Of Decimal), MEd(,) As Decimal, lConstructionPhase As Boolean,
                                   ByRef AlphaCr As Decimal, ByRef lOK As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au déversement
        '----------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre traitée
        '   iCombi              [E] :   Indice de la combinaisons traitée
        '   MEd                 [E] :   Diagramme de flexion
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '   AlphaCr             [S] :   Alpha Critique
        '   lOK                 [S] :   Indique si le calcul s'est bien déroulé
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim i As Integer

        'Dim pDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim pDonnees As New CTICM_DATA_DLLS.DATA_DLLS
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
        'Dim CoefCombi As New List(Of Decimal)
        Dim iTravP As Integer, iTravD As Integer, iTrav As Integer
        Dim zTop As Decimal

        Dim lGeneration1 As Boolean = myPoutre.Param.lGeneration1

        '--> Initialisation

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
        If lEnrob Then nEqEc = myPoutre.Section.Enrobage.Beton.CoefficientEquivalenceCT(lGeneration1)
        myPoutre.Section.ProprietesElastiquesMyy_Usuel(1, True, myPoutre.Param.Gamma, nEqEc, zAne, pInertieY, mElRd, myPoutre.Param.lEnrobProp, False, True)
        myPoutre.Section.ProprietesElastiquesMzz(1, True, myPoutre.Param.Gamma, nEqEc, zAneZ, pInertieZ, mElRd, True)
        pInertieT = myPoutre.Section.InertieT(lGeneration1)
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

        '# Maintiens au déversement ponctuels

        GestionMaintienLT(myPoutre, pDonnees, paramLTB)

        '# Maintien par le bac en phase de construction

        If lConstructionPhase And myPoutre.lMaintienBacPossible Then
            If myPoutre.MaintienBac.lMaintienBac Then

                Dim EntraxeD As Decimal = myPoutre.EntraxeSolive
                Dim Sact As Decimal = myPoutre.MaintienBac.RigiditeShear(myPoutre.LongueurTravee(1), EntraxeD, myPoutre.Dalle.Bac, myPoutre.Section.Acier.EYoung)

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

                ' paramLTB.MaintienConV(0) = 0

                '# maintien en cisaillement par le bac (v')
                paramLTB.MaintienConVP(0) = Sact
                'paramLTB.MaintienConVP(0) = 9422 * 1000

                '# maintien en flexion par le bac (theta)
                paramLTB.MaintienConTheta(0) = kTheta
                ' paramLTB.MaintienConTheta(0) = 16.6 * 1000

                paramLTB.zMaintienConC(0) = -zAne - pzS
                'paramLTB.zMaintienConC(0) = zAne + pzS

            End If
        End If

        '# Chargements

        iTravP = myPoutre.IndicePremiereTravee
        iTravD = myPoutre.IndiceDerniereTravee
        zTop = -zAne - pzS
        'zTop = zAne + pzS
        Dim NbRep, iCharge As Integer
        Dim iCompteur As Integer = -1
        NbRep = 0
        Dim NbCharges As Integer = CoefCombi.Count
        Dim xo, xe As Double
        Dim qo, qe As Double
        Dim qSurf, Force As Decimal
        Dim LargeurI As Decimal = myPoutre.LargeurInfluence

        pDonnees.NbForcesPon = 0
        pDonnees.NbForcesRep = 0
        pDonnees.NbMoments = 0

        '-- Nombre de charges réparties et surfaciques, à transférer

        For iCharge = 0 To NbCharges - 1
            If Not IsEqual(CoefCombi(iCharge), 0) Then

                NbRep += myPoutre.ChargesA(iCharge).NombreChargesSurf(iTravP, iTravD) + myPoutre.ChargesA(iCharge).NombreFRep(iTravP, iTravD)

            End If
        Next

        pDonnees.InitialiseForcesRep(NbRep)

        For iCharge = 0 To CoefCombi.Count - 1
            If Not IsEqual(CoefCombi(iCharge), 0) Then
                For iTrav = iTravP To iTravD

                    xo = myPoutre.xPositionAppui(True, iTrav)
                    xe = myPoutre.xPositionAppui(False, iTrav)

                    '# Transfert des charges surfaciques

                    If Not IsEqual(myPoutre.ChargesA(iCharge).QSurf(iTrav), 0) Then
                        iCompteur += 1
                        qSurf = CoefCombi(iCharge) * myPoutre.ChargesA(iCharge).QSurf(iTrav) * LargeurI
                        pDonnees.AjouteForceRep(xo, xe, qSurf, qSurf, iCompteur, zTop)
                    End If

                    '# Transfert des charges réparties

                    For iForce = 0 To myPoutre.ChargesA(iCharge).FReparties(iTrav).Count - 1
                        If (Not (IsEqual(myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).Force(0), 0) And IsEqual(myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).Force(1), 0))) Then

                            iCompteur += 1

                            xo = myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).xPosG(0)
                            xe = myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).xPosG(1)
                            ' qo = myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).Force(0)
                            ' qe = myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).Force(1)
                            qo = CoefCombi(iCharge) * myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).Force(0) 'GUD: Rajout du coeff combi devant la charge linéique
                            qe = CoefCombi(iCharge) * myPoutre.ChargesA(iCharge).FReparties(iTrav)(iForce).Force(1) 'GUD: Rajout du coeff combi devant la charge linéique

                            pDonnees.AjouteForceRep(xo, xe, qo, qe, iCompteur, zTop)

                        End If
                    Next

                    '# Transfert des charges ponctuelles

                    For iForce = 0 To myPoutre.ChargesA(iCharge).Forces(iTrav).Count - 1

                        xo = myPoutre.ChargesA(iCharge).Forces(iTrav)(iForce).xPosG
                        'Force = myPoutre.ChargesA(iCharge).Forces(iTrav)(iForce).Force
                        Force = CoefCombi(iCharge) * myPoutre.ChargesA(iCharge).Forces(iTrav)(iForce).Force 'GUD: Rajout du coeff combi devant la charge ponctuelle
                        pDonnees.AjouteForceP(Force, xo, zTop)

                    Next
                Next
            End If
        Next

        '# Moments fléchissants

        'pDonnees.MomentFle = MEd  ' GUD: Désactivation de cette ligne : je ne sais pas pourquoi mais lcette ligne fait bugger le moteur de LTB

        For i = 0 To pDonnees.NbNodes - 2 'RAJOUT GUD: J'ai repris ce qu'avait fait Minh 
            pDonnees.MomentFle(i, 0) = MEd(i, 1)
            pDonnees.MomentFle(i, 1) = MEd(i + 1, 0)
        Next

        '--> Lancement du calcul LTBeamN

        Call MyDLL_LTB.CALCULER(pDonnees, paramLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '--> Exploitation des résultats

        AlphaCr = MyOutput_LTB.CoefCr
        lOK = (CodeError_LTB = 0)

        If Not lOK Then
            MsgBox(TextError_LTB, MsgBoxStyle.Critical, "cls_VerificationAcier/CalculAlphaCritique")
        End If

    End Sub

    Private Sub GestionMaintienLT(myBeam As cls_Poutre, ByRef pDonnees As CTICM_DATA_DLLS.DATA_DLLS,
                                  ByRef paramLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB)
        '----------------------------------------------------------------------------------------------------------
        '   24/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Introduction des maintiens au déversement dans les données de calcul 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   pDonnees    [S] :   Paramètres décrivant la poutre
        '   paramLTB    [S] :   Paramètres LTB du modèle de calcul
        '----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iTravee As Integer
        Dim iDebT, iFinT As Integer
        Dim cTheta, cVP, cThetaP As Decimal
        Dim lMaintien As Boolean
        Dim zMaintien As Decimal
        Dim x0 As Decimal

        Dim iNode, jM As Integer

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        paramLTB.NbMaintiensPon = 0

        '--( Traitement des noeuds sur appuis 

        '  On suppose que tous les noeuds sur appuis sont maintenus en deplacement latéral et en rotation de torsion

        For iNode = 0 To myBeam.Nodes.NbAppuis - 1
            AjouteMaintien(paramLTB, myBeam.Nodes.iNodeAppui(iNode), -1, -1, 0, 0)
        Next

        '--( Traitement des noeuds sur maintiens latéral

        cVP = 0
        cThetaP = 0

        For iTravee = iDebT To iFinT

            x0 = myBeam.xPositionAppui(True, iTravee)

            For jM = 0 To myBeam.Maintiens(iTravee).Count - 1

                lMaintien = True

                If myBeam.Maintiens(iTravee)(jM).lMaintienSemelleInf And myBeam.Maintiens(iTravee)(jM).lMaintienSemelleSup Then
                    cTheta = -1
                    zMaintien = 0
                ElseIf myBeam.Maintiens(iTravee)(jM).lMaintienSemelleInf Then
                    cTheta = 0
                    zMaintien = -myBeam.Section.ProfilA.ha / 2
                ElseIf myBeam.Maintiens(iTravee)(jM).lMaintienSemelleSup Then
                    cTheta = 0
                    zMaintien = +myBeam.Section.ProfilA.ha / 2
                Else
                    lMaintien = False
                End If

                iNode = myBeam.GetIndiceNoeudFromXglobal(x0 + myBeam.Maintiens(iTravee)(jM).x_Loc)

                '# Protection contre les erreurs
                If iNode = -1 Then
                    lMaintien = False
                    MsgBox("convergence error", MsgBoxStyle.Critical, "[cls_VerificationsAcier|GetIndiceNoeudFromXglobal]")
                End If

                If lMaintien Then _
                AjouteMaintien(paramLTB, iNode, -1, cTheta, cVP, cThetaP, zMaintien)
            Next

        Next
        'paramLTB.NbMaintiensPon = myBeam.Nodes.NbAppuis
        'ReDim paramLTB.iNodeMaintienPon(paramLTB.NbMaintiensPon - 1)
        'ReDim paramLTB.MaintienPonV(paramLTB.NbMaintiensPon - 1)
        'ReDim paramLTB.MaintienPonTheta(paramLTB.NbMaintiensPon - 1)
        'ReDim paramLTB.MaintienPonVP(paramLTB.NbMaintiensPon - 1)
        'ReDim paramLTB.MaintienPonThetaP(paramLTB.NbMaintiensPon - 1)
        'ReDim paramLTB.zMaintienPonC(paramLTB.NbMaintiensPon - 1)

        'For i = 0 To pDonnees.NbAppuis - 1
        '    paramLTB.iNodeMaintienPon(i) = myBeam.Nodes.iNodeAppui(i)
        '    paramLTB.MaintienPonV(i) = -1
        '    paramLTB.MaintienPonTheta(i) = -1
        'Next

    End Sub

    Private Sub ExtraireMaintiensLateraux(myPoutre As cls_Poutre, ByRef ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Préparation des paramètres de calcul relatifs aux maintiens latéraux
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
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

End Class
