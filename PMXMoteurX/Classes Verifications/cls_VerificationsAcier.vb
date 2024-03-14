Imports System.Security.Cryptography

Public Class cls_VerificationsAcier


    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES ACIER
    '=========================================================================================================

#Region " Structures "

#End Region

#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereVb As cls_Critere                 ' Resistance voilement par cisaillement
    Public CritereMV As cls_Critere                 ' Résistance à l'interacion MV
    Public CritereMVb As cls_Critere                ' Résistance à l'interacion M+voilement par cisaillement
    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereSigmaE As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton d'enrobage
    Public CritereSigmaArmaE As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures d'enrobage
    Public CritereTauA As cls_Critere               ' Critère de contrainte de cisaillement élastique
    Public CritereSigmaVM As cls_Critere            ' Critère de contrainte élastique équivalente de Von Mises
    Public CritereLTB As cls_Critere                ' Critère pour le déversement

    Public RhoV As Decimal(,)                       ' Coefficient d'interaction : 1er indice: indice de la combinaison, 2eme indice: indice du noeud

    Public lCalculPlastic As Boolean                ' Indique si le dimensionnement est suivant la théorie plastique

    Public AlphaCrLTB() As Decimal                  ' Alpha critique pour le déversement élastique
    Public McrLTB(,) As Decimal                     ' Moment critique pour le déversement (en travée)

    Public ShearB As strucShearBuckling             ' Paramètres du voilement par cisaillement

    '==( Classe pour le calcul des contraintes de cisaillement en calcul élastique imposé

    Dim Tau As cls_Tau

    '==( Classe pour le caclul des flux de cisaillement dans les soudures des PRS

    Dim FluxF As cls_Flux

    Public GorgesSoudures(1) As Decimal             ' Gorge des soudures ame semelles pour les sections PRS

#End Region

#Region " Constructeurs "

    Public Sub New()
        lCalculPlastic = True
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer, lElastic As Boolean, lElastiTau As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '   lElastic    [E] :   Cas d'un dimensionnement élastique VM en flexion
        '   lElasticTau [E] :   Cas d'un dimensionnement élastique VM en cisaillement
        '----------------------------------------------------------------------------------------------------------

        Me.CritereM = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereMV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereMVb = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereVb = New cls_Critere(NbNodes, NbCombi, IndDerniereT)

        Me.CritereLTB = New cls_Critere(IndDerniereT + 1, NbCombi, IndDerniereT)

        ReDim AlphaCrLTB(NbCombi - 1)
        ReDim McrLTB(NbCombi - 1, IndDerniereT)

        If lElastic Then
            Me.CritereSigmaA = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereSigmaE = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereSigmaArmaE = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        End If

        If lElastiTau Then
            Me.CritereTauA = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereSigmaVM = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        End If

    End Sub

    Private Sub InitialiseCriteresVM(NbNodes As Integer, lArma As Boolean, nbCombi As Integer, IndDerniereT As Integer)
        '-------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '-------------------------------------------------------------------
        '   Initialisation des critères pour les contraintes normales
        '-------------------------------------------------------------------

        Me.CritereSigmaA = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaE = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        If lArma Then
            Me.CritereSigmaArmaE = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        End If

    End Sub

#End Region

#Region " Outils de vérification "

    Public Sub Z_VerificationELU(myBeam As cls_Poutre, lConstructionPhase As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam            [E] :   Poutre vérifiée
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim VplRd As Decimal                            ' Résistance plastique au cisaillement
        Dim VbRd As Decimal                             ' Résistance au voilement par cisaillement (a priori constant le long de la poutre)
        Dim VRd As Decimal                              ' Résistance à l'effort tranchant (soit plastique, soit voilement)
        Dim lTwoAdjacentCantilevers As Boolean          ' indique la présence de deux travées adjacentes en consoles (True) ou non
        Dim iCombi As Integer
        Dim combiELU As New cls_Combinaisons
        Dim nbCombiELU As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim MplRd, zANP, MfRd As Decimal
        Dim zANPMV(,) As Decimal = Nothing                ' Position ANP, tenant compte de l'interaction avec l'effort tranchant 
        Dim MVRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de l'interaction avec l'effort tranchant 
        Dim MelRd, zANE As Decimal
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim ClasseP, ClasseM As Integer 'Classes de la section en flexion positive et négative
        Dim lClasse4 As Boolean
        ' Dim lSigma As Boolean
        Dim SigmaELU(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELU
        Dim SigmaCas(,,,) As Decimal = Nothing          ' Contraintes normales pour les cas de charges
        Dim TauELU(,,) As Decimal = Nothing             ' Contraintes de cisaillement sous 1 combinaison ELU
        Dim TauCas(,,,) As Decimal = Nothing            ' Contraintes de cisaillement pour les cas de charges
        Dim FluxCas(,,,) As Decimal = Nothing           ' Flux de cisaillement dans les soudures de PRS par cas de charges
        Dim FluxELU(,,) As Decimal = Nothing            ' Flux de cisaillement dans les soudures de PRS aux ELU
        Dim lRetraitElastique As Boolean = True
        Dim lVerifElastic As Boolean                    ' Indique si on doit effectuer une verification élastique des sections
        Dim EpsilonW As Decimal
        Dim lEnrob As Boolean = myBeam.lEnrobage
        Dim lproPRS As Boolean = Not myBeam.Section.lLamine

        '--> Initialisations

        '# Critères

        If lConstructionPhase Then
            nbCombiELU = myBeam.CombiA_ELCU.nbCombi     ' cls_Poutre.nbCombELUConstruction
            combiELU = myBeam.CombiA_ELCU
        Else
            nbCombiELU = myBeam.CombiA_ELU.nbCombi      'cls_Poutre.nbCombELU
            combiELU = myBeam.CombiA_ELU
        End If

        Me.InitialiseRhoV(nbCombiELU, myBeam.Nodes.nbNodes)
        Me.InitialiseCriteresVM(myBeam.Nodes.nbNodes, myBeam.lEnrobage, nbCombiELU, myBeam.IndiceDerniereTravee)

        '# Tranchant résistant

        VplRd = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM0)

        '# Résistance au voilement par cisaillement

        lTwoAdjacentCantilevers = myBeam.lTraveeConsoleGauche And myBeam.lTraveeConsoleDroite

        VbRd = myBeam.Section.VbRd(myBeam.Param.Gamma.GammaM1, myBeam.Param.EtaW, lTwoAdjacentCantilevers)

        EpsilonW = Math.Sqrt(235 / myBeam.Section.FyW)
        Me.ShearB.ElancementW = myBeam.Section.ProfilA.ElancementAme
        If lEnrob Then
            Me.ShearB.LimiteElancementW = 124 * EpsilonW
        Else
            Me.ShearB.LimiteElancementW = 72 * EpsilonW / myBeam.Param.EtaW
        End If
        Me.ShearB.lCheckRequired = IsGreater(Me.ShearB.ElancementW, Me.ShearB.LimiteElancementW)

        '# Résistance à l'effort tranchant

        If Me.ShearB.lCheckRequired Then
            VRd = Math.Min(VplRd, VbRd)
            '# Calcul de MfRd 

            myBeam.ProprieteVerifAcierMfRd(True, MfRd)
        Else
            VRd = VplRd
        End If


        '# Propriétés

        myBeam.ProprietesVerifAcier(True, MplRd, zANP, MelRd, zANE)

        '# Classes de la section

        '    La classe des sections ne dépend pas du chargement (il n'y a pas d'effort axial) ni des contraintes.
        '    On classe donc les sections une fois pour toute, en dehors de la boucle sur les combinaisons de calcul

        ClasseP = myBeam.Section.ClasseSection(zANP, zANE, True, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1)
        ClasseM = myBeam.Section.ClasseSection(zANP, zANE, False, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1)

        '# Type de vérification pour les sections

        lVerifElastic = myBeam.Param.lElasticDesignVM Or (ClasseP > 2)
        If myBeam.lMultiSpan Then
            '# dans le cas d'une poutre à plusieurs travées, on prend aussi en compte la classe de section en flexion négative
            lVerifElastic = lVerifElastic Or (ClasseM > 2)
        End If
        Me.lCalculPlastic = Not lVerifElastic

        '# Initialisation des critères dépendant du type de vérification

        Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee, lVerifElastic, myBeam.Param.lElasticDesignVM)

        '# Contraintes normales

        If lVerifElastic Then
            myBeam.PtsSigma.Initialise(myBeam)
            myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaCas)
        End If

        '# Contraintes de cisaillement
        If myBeam.Param.lElasticDesignVM Then
            Me.Tau = New cls_Tau(myBeam.Section.typeSection)
            Me.Tau.Initialise(myBeam.Section.ProfilA)
            Me.Tau.CalculContraintesCharges(myBeam, TauCas)
        End If

        '# Flux de cisaillement des PRS
        If lproPRS Then
            Me.FluxF = New cls_Flux
            Me.FluxF.InitialiseCalculAcier(myBeam)
            Me.FluxF.CalculFluxChargesACIER(myBeam, FluxCas)
        End If

        '--> Boucle sur les combinaisons

        For iCombi = 0 To combiELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            combiELU.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '# Combinaison des efforts tranchants

            combiELU.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '# Combinaisons des contraintes

            If lVerifElastic Then
                '( Contraintes normales
                combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, myBeam.PtsSigma.zPos.Count, myBeam.Nodes.nbNodes,
                                                    myBeam.ChargesA, SigmaCas, lRetraitElastique, SigmaELU)
                myBeam.PtsSigma.AjusteContraintes(myBeam, SigmaELU)

                '( Contraintes de cisaillement
                If myBeam.Param.lElasticDesignVM Then
                    combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, Me.Tau.MStatic.Count, myBeam.Nodes.nbNodes,
                                                        myBeam.ChargesA, TauCas, lRetraitElastique, TauELU)
                End If
            End If
            If lproPRS Then
                combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, cls_Flux.NbPTS, myBeam.Nodes.nbNodes,
                                                    myBeam.ChargesA, FluxCas, lRetraitElastique, FluxELU)
            End If

            '# Vérification sous moment fléchissant

            If lVerifElastic Then
                RunCritereFlexionResistanceElastiqueVM(myBeam, iCombi, SigmaELU)
            Else
                Me.RunCritereFlexionAcier(myBeam, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lClasse4)
            End If

            If myBeam.Param.lElasticDesignVM Then 'calcul élastique imposé 

                '# Vérification sous effot tranchant
                Me.RunCritereCisaillementResistanceElastiqueVM(myBeam, iCombi, TauELU)

                '# Vérification sous interaction MV
                Me.RunCritereInteractionMVElastiqueVonMises(myBeam, iCombi, SigmaELU, TauELU)

            Else 'calcul plastique, même pour les sections de classe 3, si le calcul élastique n'est pas imposé

                '# Vérification sous effort tranchant

                Me.RunCritereTranchants(myBeam, iCombi, VEd, VplRd)

                '# Vérification au voilement par cisaillement

                If Me.ShearB.lCheckRequired Then Me.RunCritereVoilementCisaillement(myBeam, iCombi, VEd, VbRd)

                '# Traitement de l'interaction MV en fonction de la sensibilité au voilement par cisaillement

                If Me.ShearB.lCheckRequired Then

                    '# Vérification sous interaction MVb

                    Me.RunCriteresInteractionMVb(myBeam, iCombi, VbRd, MEd, VEd, mfrd, MplRd)

                Else
                    '# Calcul du critère d'intéraction rhoV

                    Me.CalculRhoV(iCombi, myBeam)

                    '# Propriétés avec prise en compte de l'interaction MV

                    myBeam.ProprietesVerifMVAcier(iCombi, myBeam, True, MVRd, zANPMV, Me.RhoV)

                    '# Vérification sous interaction MV

                    Me.RunCriteresInteractionMV(myBeam, iCombi, MEd, MVRd)

                End If

            End If

            '# Vérification au déversement

            Me.RunCritereDeversement(myBeam, iCombi, MEd, lVerifElastic, lConstructionPhase)

            '# Dimensionnement des soudures de PRS

            If lproPRS Then
                Me.RunDimensionSouduresAmeSemelle(myBeam, iCombi, FluxELU, Me.GorgesSoudures)
            End If
        Next

    End Sub

#End Region

#Region " Calcul des soudures âme semelle "

    Private Sub RunDimensionSouduresAmeSemelle(myBeam As cls_Poutre, iCombi As Integer, FluxELU(,,) As Decimal, ByRef Gorges() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des gorges de soudure pour les flux de cisaillement ELU
        '----------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre traitée
        '   iCombi              [E] :   Indice de la combinaison
        '   FluxELU             [E] :   Table des flux de cisaillement longi le long de la poutre
        '   Gorges              [S] :   Gorge des soudures
        '----------------------------------------------------------------------------------------------------------

        '(NbPts - 1, NbNodes - 1, 1)

        '--( Déclaration

        Const iDEB As Integer = 1       ' Semelle sup
        Const iFIN As Integer = 2       ' Semelle inf

        Dim iSoud, iTrav As Integer
        Dim iDebTrav As Integer = myBeam.IndicePremiereTravee
        Dim iFinTrav As Integer = myBeam.IndiceDerniereTravee
        Dim iDebNod, iFinNod, iNode As Integer
        Dim kDeb, kFin, k As Integer
        Dim GammaM2 As Decimal = myBeam.Param.Gamma.GammaM2
        Dim BetaW As Decimal = 1      '== APROGRaMMER
        Dim Fu() As Decimal = {myBeam.Section.Acier.LimiteFu(Math.Max(myBeam.Section.ProfilA.Tfs, myBeam.Section.ProfilA.Tw)),
                               myBeam.Section.Acier.LimiteFu(Math.Max(myBeam.Section.ProfilA.Tfi, myBeam.Section.ProfilA.Tw))}

        '--( Calcul

        For iTrav = iDebTrav To iFinTrav

            iDebNod = myBeam.Nodes.iNodeExtTrav(iTrav, 0)
            iFinNod = myBeam.Nodes.iNodeExtTrav(iTrav, 1)

            For iNode = iDebNod To iFinNod
                If iNode = iDebNod Then kDeb = 1 Else kDeb = 0
                If iNode = iFinNod Then kFin = 0 Else kFin = 1

                For k = kDeb To kFin

                    For iSoud = iDEB To iFIN

                        Gorges(iSoud - 1) = Math.Max(Gorges(iSoud - 1), CalculSoudure(FluxELU(iSoud, iNode, k), GammaM2, BetaW, Fu(iSoud - 1)))

                    Next

                Next

            Next


        Next

    End Sub

    Private Function CalculSoudure(myFlux As Decimal, GammaM2 As Decimal, BetaW As Decimal, Fu As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des gorges de soudure pour les flux de cisaillement ELU
        '----------------------------------------------------------------------------------------------------------
        '   myFlux              [E] :   Flux de cisaillement
        '   GammaM2             [E] :   Coefficient partiel
        '   BetaW               [E] :   Coefficient BetaW selon EN 1993-1-8 pour le calcul des soudures
        '   Fu                  [E] :   Résistance ultime à la traction
        '----------------------------------------------------------------------------------------------------------

        Dim Aw As Decimal

        Aw = Math.Sqrt(3) / 2 * Math.Abs(myFlux) / (Fu * kConvMPaPa) * BetaW * GammaM2

        Return Aw
    End Function


#End Region

#Region " Vérifications de la résistance au déversement "

    Private Sub RunCritereDeversement(myPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, lSigma As Boolean,
                                      lConstructionPhase As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au déversement
        '----------------------------------------------------------------------------------------------------------
        '   myBeam            [E] :   Poutre traitée
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

        '--> Calcul Alpha Critique

        CalculAlphaCritique(myPoutre, iCombi, MEd, lConstructionPhase, AlphaCr, lOK)
        Me.AlphaCrLTB(iCombi) = AlphaCr

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
            McrLTB(iCombi, iTrav) = Mcr

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
        '   myBeam            [E] :   Poutre traitée
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
        Dim CoefCombi As New List(Of Decimal)
        Dim iTravP As Integer, iTravD As Integer, iTrav As Integer
        Dim zTop As Decimal

        '--> Initialisation

        If lConstructionPhase Then
            CoefCombi = myPoutre.CombiA_ELCU.CoefCombi(iCombi) 'RAJOUT GUD POUR DEBBUG
            'CoefCombi = myPoutre.CoefCombELCU(iCombi)
        Else
            'CoefCombi = myPoutre.CoefCombELU(iCombi)
            CoefCombi = myPoutre.CombiA_ELU.CoefCombi(iCombi)
        End If

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
        myPoutre.Section.ProprietesElastiquesMyy(1, True, myPoutre.Param.Gamma, nEqEc, zAne, pInertieY, mElRd, False, True)
        myPoutre.Section.ProprietesElastiquesMzz(1, True, myPoutre.Param.Gamma, nEqEc, zAneZ, pInertieZ, mElRd, True)
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

    Private Sub ExtraireMaintiensLateraux(myPoutre As cls_Poutre, ByRef ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Préparation des paramètres de calcul relatifs aux maintiens latéraux
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
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

    Private Sub RunCritereFlexionResistanceElastiqueVM(myBeam As cls_Poutre, iCombi As Integer, SigmaELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   SigmaELU[E] :   Contraintes normales aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal
        Dim Fck, Fcd As Decimal
        Dim Fsk, Fsd As Decimal

        Dim iPro0 As Integer = myBeam.PtsSigma.iProfile(0)
        Dim iBetonE0 As Integer = myBeam.PtsSigma.iBetonEnrob(0)
        Dim iArmaE0 As Integer = myBeam.PtsSigma.iArmaEnrob(0)

        Dim lEnrob As Boolean = myBeam.lEnrobage

        '--> Initialisation

        FySup = myBeam.Section.FySup
        FydSup = FySup / myBeam.Param.Gamma.GammaM0
        FyW = myBeam.Section.FyW
        FydW = FyW / myBeam.Param.Gamma.GammaM0
        FyInf = myBeam.Section.FyInf
        FydInf = FyInf / myBeam.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then
            '( Contrainte face externe de la semelle supérieure
            RunCritereFlexionVM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
            '( Contrainte face interne de la semelle supérieure
            RunCritereFlexionVM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
            '( Contrainte CdG de la section
            RunCritereFlexionVM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
            '( Contrainte face interne de la semelle inférieure
            RunCritereFlexionVM(myBeam, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydInf, FydW), Me.CritereSigmaA)
            '( Contrainte face externe de la semelle inférieure
            RunCritereFlexionVM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.CritereSigmaA)

        End If

        '# Contraintes dans l'enrobage

        If lEnrob And (iBetonE0 > -1) Then

            '# Limite de contraintes
            Fck = myBeam.Section.Enrobage.Beton.Fck
            Fcd = Fck / myBeam.Param.Gamma.GammaC
            Fsk = myBeam.Section.Enrobage.AcierArmatures.FsK
            Fsd = Fsk / myBeam.Param.Gamma.GammaS

            '# Contrainte dans le béton d'enrobage (face supérieure puis face inférieure)
            RunCritereFlexionVM(myBeam, iCombi, iBetonE0 + 0, SigmaELU, Fcd, Me.CritereSigmaE)
            RunCritereFlexionVM(myBeam, iCombi, iBetonE0 + 1, SigmaELU, Fcd, Me.CritereSigmaE)

            '# Contrainte dans les lits d'armatures (3 lits)
            For iArma As Int16 = 0 To 2
                If myBeam.Section.Enrobage.LitArma(iArma).NbTotalBarresActives > 0 Then
                    RunCritereFlexionVM(myBeam, iCombi, iArmaE0 + iArma, SigmaELU, Fsd, Me.CritereSigmaArmaE)
                End If
            Next

        End If


        '# Enveloppe de résistance en flexion

        EnveloppeResistanceFlexionElastique(myBeam, iCombi)

    End Sub

    Private Sub EnveloppeResistanceFlexionElastique(myBeam As cls_Poutre, iCombi As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   13/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Récupère le critère VM dimensionnant en flexion
        '----------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------

        '--< Déclarations

        Dim iTravDeb As Integer, iTravFin As Integer

        '--< Initialisations

        iTravDeb = myBeam.IndicePremiereTravee
        iTravFin = myBeam.IndiceDerniereTravee

        '--< Contraintes dans le profilé acier

        Me.CritereM.CritereMax = Me.CritereSigmaA.CritereMax
        Me.CritereM.iCombiM = Me.CritereSigmaA.iCombiM
        Me.CritereM.iNodeM = Me.CritereSigmaA.iNodeM

        For i As Integer = iTravDeb To iTravFin
            Me.CritereM.CritereCombiT(iCombi, i) = Me.CritereSigmaA.CritereCombiT(iCombi, i)
            Me.CritereM.CritereCombiN(iCombi, i) = Me.CritereSigmaA.CritereCombiN(iCombi, i)
        Next

        '--< Contraintes béton et armatures d'enrobage

        If myBeam.lEnrobage Then
            If (myBeam.PtsSigma.iBetonEnrob(0) > -1) Then
                Me.CritereM.EnveloppeCritereCombi(Me.CritereSigmaE, iCombi, iTravDeb, iTravFin)
            End If
            If (myBeam.PtsSigma.iArmaEnrob(0) > -1) Then
                Me.CritereM.EnveloppeCritereCombi(Me.CritereSigmaArmaE, iCombi, iTravDeb, iTravFin)
            End If
        End If
    End Sub

    Private Sub RunCritereFlexionVM(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal,
                                    SigmaU As Decimal, MyCritereM As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VonMises en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
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

    End Sub

    Private Sub RunCritereFlexionAcier(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal,
                                    MplRd As Decimal, MelRd As Decimal, ClasseP As Integer, ClasseM As Integer, ByRef lClasse4 As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
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

    Private Sub RunCritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VRd As Decimal, Optional lBuckling As Boolean = False)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   VEd     [E] :   Table des efforts tranchants le long de la barre
        '   VRd     [E] :   Effort tranchant résistant (plastique ou voilement) de la barre
        '   lBukling[E] :   Indique si critere de résistance au voilement par cisaillement
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
                    If lBuckling Then
                        Me.CritereVb.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
                    Else
                        Me.CritereV.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereVoilementCisaillement(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VbRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   VEd     [E] :   Table des efforts tranchants le long de la barre
        '   VRd     [E] :   Table des résistances au voilement par cisaillement le long de la barre
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
                    Me.CritereVb.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VbRd)
                Next
            Next
        Next

    End Sub

    Private Sub RunCriteresInteractionMV(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, MVRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   23/01/2023 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'interaction MV (critère de résistance plastique)
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MplRd   [E] :   Table des moments plastiques le long de la barre (calculés en fonction du signe de MEd)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        'Dim Sigma As Decimal
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
                    Me.CritereMV.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MVRd(iNode, k))
                Next
            Next
        Next


    End Sub

    Private Sub RunCriteresInteractionMVb(MyPoutre As cls_Poutre, iCombi As Integer, VbRd As Decimal,
                                          MEd(,) As Decimal, VEd(,) As Decimal, MfRd As Decimal, MplRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   23/01/2023 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'interaction MVb
        '   selon EN 1993-1-5 § 7.1
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   MEd         [E] :   Table des moments fléchissants le long de la barre
        '   VEd         [E] :   Table des efforts tranchants le long de la barre
        '   VbRd        [E] :   Effort tranchant résistant - voilement par cisaillement 
        '   MfRd        [E] :   Moments résistant plastique, en ignorant l'âme
        '   MplRd       [E] :   Moments plastique 
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer

        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim GammaMVb As Decimal

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


                    If IsGreater(Math.Abs(VEd(iNode, k)), 0.5 * VbRd) _
                    And IsGreater(Math.Abs(MEd(iNode, k)), MfRd) Then

                        GammaMVb = (Math.Abs(MEd(iNode, k)) / MplRd) _
                                 + (1 - MfRd / MplRd) * (2 * Math.Abs(VEd(iNode, k)) / VbRd - 1) ^ 2

                    Else
                        GammaMVb = 0
                    End If

                    Me.CritereMVb.EnregistreCritere(iNode, iCombi, iTravee, GammaMVb, 1)


                Next
            Next
        Next


    End Sub


#End Region

#Region " Calcul coefficient d'interaction RhoV "

    Private Sub InitialiseRhoV(NbCombi As Integer, NbNodes As Integer)
        ReDim Me.RhoV(NbCombi - 1, NbNodes - 1)
    End Sub

    ''' <summary>
    ''' Fonction qui calcul le coefficient d'interaction en fonction du critèreV = VEd/VRd
    ''' </summary>
    ''' <param name="iCombi">indice de la combinaison en cours</param>
    ''' <param name="MyPoutre">poutre en cours</param>
    Public Sub CalculRhoV(iCombi As Integer, MyPoutre As cls_Poutre)
        '--> Déclaration

        Dim rhoV As Decimal
        Dim critereV As Decimal
        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                critereV = Me.CritereV.Critere(iNode)

                If critereV >= 1 Then
                    rhoV = 1
                ElseIf critereV <= 0.5 Then
                    rhoV = 0
                Else
                    rhoV = (2 * critereV - 1) ^ 2
                End If

                Me.RhoV(iCombi, iNode) = rhoV
            Next
        Next
    End Sub

#End Region

#Region " Vérification des contraintes élastiques de cisaillement "

    Private Sub RunCritereCisaillementResistanceElastiqueVM(MyPoutre As cls_Poutre, iCombi As Integer, TauELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en cisaillement par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   TauELU  [E] :   Contraintes de cisaillement aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim TauY, FyW As Decimal

        Dim nbPts As Integer = Me.Tau.MStatic.Count

        '--> Initialisation

        FyW = MyPoutre.Section.FyW
        TauY = FyW / MyPoutre.Param.Gamma.GammaM0 / Math.Sqrt(3)

        '--> Calculs

        '# Contraintes de cisaillement dans l'âme du profilé

        If (nbPts > 0) Then
            '( Contrainte face interne de la semelle supérieure
            RunCritereFlexionVM(MyPoutre, iCombi, 0, TauELU, TauY, Me.CritereTauA)
            '( Contrainte CdG de la section
            RunCritereFlexionVM(MyPoutre, iCombi, 1, TauELU, TauY, Me.CritereTauA)
            '( Contrainte face interne de la semelle inférieure
            RunCritereFlexionVM(MyPoutre, iCombi, 2, TauELU, TauY, Me.CritereTauA)
        End If

    End Sub

    Private Sub RunCritereTranchantElastic(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, TauELU(,,) As Decimal,
                                           TauU As Decimal, MyCritereV As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en cisaillement par les critères de VonMises en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint      [E] :   Indice du point de calcul des contraintes
        '   TauELU      [E] :   Contraintes de cisaillement aux ELU
        '   TauU        [E] :   Valeur ultime de la contrainte de cisaillement au point iPoint
        '   CritereM    [E] :   Critere de la contrainte de flexion
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
                    MyCritereV.EnregistreCritere(iNode, iCombi, iTravee, TauELU(iPoint, iNode, k), TauU)
                Next
            Next
        Next

    End Sub

#End Region


#Region " Vérification des contraintes élastiques équivalentes de Von Mises "

    Private Sub RunCritereInteractionMVElastiqueVonMises(MyPoutre As cls_Poutre, iCombi As Integer, SigmaELU(,,) As Decimal, TauELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en cisaillement par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   TauELU      [E] :   Contraintes de cisaillement aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal

        Dim nbPts As Integer = Me.Tau.MStatic.Count

        '--> Initialisation

        FySup = MyPoutre.Section.FySup
        FydSup = FySup / MyPoutre.Param.Gamma.GammaM0
        FyW = MyPoutre.Section.FyW
        FydW = FyW / MyPoutre.Param.Gamma.GammaM0
        FyInf = MyPoutre.Section.FyInf
        FydInf = FyInf / MyPoutre.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes de cisaillement dans l'âme du profilé

        If (nbPts > 0) Then
            '( Contrainte face interne de la semelle supérieure
            RunCritereInteractionMVElastic(MyPoutre, iCombi, 0, SigmaELU, TauELU, FydW, Me.CritereSigmaVM)
            '( Contrainte CdG de la section
            RunCritereInteractionMVElastic(MyPoutre, iCombi, 1, SigmaELU, TauELU, FydW, Me.CritereSigmaVM)
            '( Contrainte face interne de la semelle inférieure
            RunCritereInteractionMVElastic(MyPoutre, iCombi, 2, SigmaELU, TauELU, FydW, Me.CritereSigmaVM)
        End If

    End Sub

    Private Sub RunCritereInteractionMVElastic(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal, TauELU(,,) As Decimal,
                                               SigmaU As Decimal, MyCritereSigmaEqVM As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance contrainte équivalente de VonMises en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint      [E] :   Indice du point de calcul des contraintes
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   TauELU      [E] :   Contraintes de cisaillement aux ELU
        '   SigmaU      [E] :   Valeur ultime de la contrainte équivalente VM au point iPoint
        '   CritereM    [E] :   Critere de la contrainte de flexion
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim SigmaEq As Decimal
        Const iDecal As Integer = 1

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
                    SigmaEq = Math.Sqrt(SigmaELU(iPoint + iDecal, iNode, k) ^ 2 + 3 * TauELU(iPoint, iNode, k) ^ 2)

                    MyCritereSigmaEqVM.EnregistreCritere(iNode, iCombi, iTravee, SigmaEq, SigmaU)
                Next
            Next
        Next

    End Sub

#End Region

End Class
