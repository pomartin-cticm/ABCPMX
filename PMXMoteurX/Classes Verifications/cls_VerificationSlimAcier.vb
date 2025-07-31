Public Class cls_VerificationSlimAcier
    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES SLIMFLOOR ACIER
    '=========================================================================================================

#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereMV As cls_Critere                 ' Résistance à l'interacion MV
    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereTauA As cls_Critere               ' Critère de contrainte de cisaillement élastique
    Public CritereSigmaVM As cls_Critere            ' Critère de contrainte élastique équivalente de Von Mises

    Public RhoV As Decimal(,)                       ' Coefficient d'interaction : 1er indice: indice de la combinaison, 2eme indice: indice du noeud

    '== Pour tous les coefficents de réduction ci-dessous, : 1er indice: indice de la combinaison, 2eme indice: indice du noeud
    Public Psi_fi As Decimal(,)                     ' Coefficient de réduction de la semelle inférieure (aire, méthode 1)
    Public rho_t_fi As Decimal(,)                   ' Coefficient de réduction de la semelle inférieure (épaisseur, méthode 2)
    Public Psi_y_fi As Decimal(,)                   ' Coefficient de réduction de la semelle inférieure (limite d'élasticité, méthode 3)
    Public Psi_spd As Decimal(,)                    ' Coefficient de réduction du plat soudé inférieur (aire, méthode 1)
    Public rho_t_spd As Decimal(,)                  ' Coefficient de réduction du plat soudé inférieur (épaisseur, méthode 2)
    Public Psi_y_spd As Decimal(,)                  ' Coefficient de réduction du plat soudé inférieur (limite d'élasticité, méthode 3)

    Public methodeReduction As MethodeReductionPlatSlimFloor

    Enum MethodeReductionPlatSlimFloor
        methode1_ReducAire
        methode2_ReducEpaisseur
        methode3_ReducLimiteElasticite
    End Enum

    Public lCalculPlastic As Boolean                ' Indique si le dimensionnement est suivant la théorie plastique

    'Public AlphaCrLTB() As Decimal                  ' Alpha critique pour le déversement élastique
    'Public McrLTB(,) As Decimal                     ' Moment critique pour le déversement (en travée)

    'Public ShearB As strucShearBuckling             ' Paramètres du voilement par cisaillement

    '==( Classe pour le calcul des contraintes de cisaillement en calcul élastique imposé

    Dim Tau As cls_Tau

    '==( Classe pour le caclul des flux de cisaillement dans les soudures des PRS

    Dim FluxF As cls_Flux

    Public GorgesSoudures() As Decimal             ' Gorge des soudures ame semelles pour les sections PRS
    Public GorgesSouduresMini() As Decimal         ' Gorge mini des soudures ame semelles pour les sections PRS

#End Region

#Region " Constructeurs "

    Public Sub New()
        lCalculPlastic = True
        methodeReduction = MethodeReductionPlatSlimFloor.methode3_ReducLimiteElasticite
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
        Me.CritereV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)

        If lElastic Then
            Me.CritereSigmaA = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
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

    End Sub

#End Region

#Region "===Outils de vérification==="

    Public Sub Z_VerificationELU(myBeam As cls_Poutre, lConstructionPhase As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre acier slim floor
        '----------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre vérifiée
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim VplRd As Decimal                            ' Résistance plastique au cisaillement
        Dim VRd As Decimal                              ' Résistance à l'effort tranchant (soit plastique, soit voilement)
        Dim iCombi As Integer
        Dim combiELU As New cls_Combinaisons
        Dim nbCombiELU As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim QEd() As Decimal = Nothing                  ' tableau des forces nodales
        Dim MplRd(,) As Decimal = Nothing
        Dim zANP(,) As Decimal = Nothing
        Dim MelRd(,) As Decimal = Nothing
        Dim zANE(,) As Decimal = Nothing
        Dim zANPMV(,) As Decimal = Nothing                ' Position ANP, tenant compte de l'interaction avec l'effort tranchant 
        Dim MVRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de l'interaction avec l'effort tranchant 
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim ClasseP, ClasseM As Integer 'Classes de la section en flexion positive et négative
        Dim lClasse4 As Boolean
        Dim InertieY_SectionBrute, zANE_SectionBrute, MelRd_SectionBrute, zANP_SectionBrute, MplRd_SectionBrute As Decimal 'Position des axes neutres uniquement pour le calcul de la classe 
        Dim SigmaELU(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELU
        Dim SigmaCas(,,,) As Decimal = Nothing          ' Contraintes normales pour les cas de charges
        Dim TauELU(,,) As Decimal = Nothing             ' Contraintes de cisaillement sous 1 combinaison ELU
        Dim TauCas(,,,) As Decimal = Nothing            ' Contraintes de cisaillement pour les cas de charges
        Dim FluxCas(,,,) As Decimal = Nothing           ' Flux de cisaillement dans les soudures de PRS par cas de charges
        Dim FluxELU(,,) As Decimal = Nothing            ' Flux de cisaillement dans les soudures de PRS aux ELU
        Dim lSoudure As Boolean                         ' Indique si un calcul de soudure est nécessaire 
        Dim lRetraitElastique As Boolean = True
        Dim lVerifElastic As Boolean                    ' Indique si on doit effectuer une verification élastique des sections
        Const lWEB As Boolean = True
        Dim lRec As Boolean

        '--> Initialisations

        '# Critères

        If lConstructionPhase Then
            nbCombiELU = myBeam.CombiA_ELCU.nbCombi     ' cls_Poutre.nbCombELUConstruction
            combiELU = myBeam.CombiA_ELCU
        Else
            nbCombiELU = myBeam.CombiA_ELU.nbCombi      'cls_Poutre.nbCombELU
            combiELU = myBeam.CombiA_ELU
        End If

        Me.InitialiseCoeffReduc(nbCombiELU, myBeam.Nodes.nbNodes)
        Me.InitialiseCriteresVM(myBeam.Nodes.nbNodes, myBeam.lEnrobage, nbCombiELU, myBeam.IndiceDerniereTravee)
        Me.InitialiseRhoV(nbCombiELU, myBeam.Nodes.nbNodes)

        '# Tranchant résistant

        VplRd = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM0, myBeam.Param.EtaW)

        '# Résistance à l'effort tranchant

        VRd = VplRd

        '# Propriétés élastiques section brutes

        myBeam.Section.ProprietesElastiquesMyy_Slim(1, True, myBeam.Param.Gamma, zANE_SectionBrute, InertieY_SectionBrute, MelRd_SectionBrute)

        '# Propriétés plastiques section brutes

        myBeam.Section.ProprietesPlastiquesMyy_Slim(1, True, myBeam.Param.Gamma, 0, zANP_SectionBrute, MplRd_SectionBrute)

        '# Classes de la section

        Dim lBeton As Boolean = myBeam.Section.lSlimFloor And (Not lConstructionPhase)

        ClasseP = myBeam.Section.ClasseSection(zANP_SectionBrute, zANE_SectionBrute, True, lBeton, False, lGeneration1, False, False, lWEB, lRec)
        ' ClasseM = myBeam.Section.ClasseSection(zANP_SectionBrute, zANE_SectionBrute, False, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1, False, False, lWEB, lrec)

        '# Type de vérification pour les sections

        lVerifElastic = myBeam.Param.lElasticDesignVM Or (ClasseP > 2)

        'If myBeam.lMultiSpan Then
        '    '# dans le cas d'une poutre à plusieurs travées, on prend aussi en compte la classe de section en flexion négative
        '    lVerifElastic = lVerifElastic Or (ClasseM > 2)
        'End If
        Me.lCalculPlastic = Not lVerifElastic

        '--> Boucle sur les combinaisons

        For iCombi = 0 To combiELU.nbCombi - 1

            '# Combinaisons des moments

            combiELU.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '# Combinaison des efforts tranchants

            combiELU.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '# Recupération des efforts nodaux à partir des tranchants combinés

            combiELU.RecupererEffortsNodauxPonderees(myBeam.Nodes.nbNodes, VEd, QEd)

            '# Calcul des coefficients de réduction 

            CalculCoefficiensReduction(iCombi, myBeam, QEd) 'GUD: penser à faire la différence en fonction des largeurs participantes 

            '# Propriétés réduites

            myBeam.ProprietesVerifSlimFloorAcier(iCombi, myBeam, True, MplRd, zANP, MelRd, zANE,
                                                 Psi_fi, rho_t_fi, Psi_y_fi, Psi_spd, rho_t_spd, Psi_y_spd)

            '# Initialisation des critères dépendant du type de vérification

            Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee, lVerifElastic, myBeam.Param.lElasticDesignVM)

            '# Contraintes normales

            If lVerifElastic Then
                myBeam.PtsSigma.Initialise(myBeam)
                myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaCas)
            End If

            '# Contraintes de cisaillement
            If myBeam.Param.lElasticDesignVM Then
                Me.Tau = New cls_Tau(myBeam.Section.TypeSection)
                Me.Tau.Initialise(myBeam.Section.ProfilA)
                Me.Tau.CalculContraintesCharges(myBeam, TauCas)
            End If

            '# Flux de cisaillement des PRS

            With myBeam.Section.ProfilA

                lSoudure = False

                Select Case .typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                        lSoudure = True
                        ReDim GorgesSouduresMini(1)
                        ReDim GorgesSoudures(1)

                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                        lSoudure = True
                        ReDim GorgesSouduresMini(0)
                        ReDim GorgesSoudures(0)

                End Select

                If lSoudure Then
                    myBeam.Section.ProfilA.InitialiseSoudureMini(Me.GorgesSouduresMini)
                    Me.FluxF = New cls_Flux
                    Me.FluxF.InitialiseCalculAcier(myBeam)
                    Me.FluxF.CalculFluxChargesACIER(myBeam, FluxCas)
                End If

            End With

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

            '# Vérification sous moment fléchissant

            If lVerifElastic Then
                RunCritereFlexionResistanceElastiqueVM(myBeam, iCombi, SigmaELU)
            Else
                Me.RunCritereFlexionAcier(myBeam, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lClasse4)
            End If

            If myBeam.Param.lElasticDesignVM Then 'calcul élastique imposé 

                '# Vérification sous effot tranchants
                Me.RunCritereCisaillementResistanceElastiqueVM(myBeam, iCombi, TauELU)

                '# Vérification sous interaction MV
                Me.RunCritereInteractionMVElastiqueVonMises(myBeam, iCombi, SigmaELU, TauELU)

            Else 'calcul plastique, même pour les sections de classe 3, si le calcul élastique n'est pas imposé

                '# Vérification sous effort tranchant

                Me.RunCritereTranchants(myBeam, iCombi, VEd, VplRd)

                '# Vérification au voilement par cisaillement

                '--> Sans objet

                '# Traitement de l'interaction MV en fonction de la sensibilité au voilement par cisaillement

                '# Calcul du critère d'intéraction rhoV

                Me.CalculRhoV(iCombi, myBeam)

                '# Propriétés avec prise en compte de l'interaction MV

                myBeam.ProprietesVerifMVAcier(iCombi, True, MVRd, zANPMV, Me.RhoV)

                '# Vérification sous interaction MV

                Me.RunCriteresInteractionMV(myBeam, iCombi, MEd, MVRd)

            End If

            '# Vérification au déversement

            'Sans objet

            '# Dimensionnement des soudures de PRS

            If lSoudure Then
                'Me.RunDimensionSouduresAmeSemelle(myBeam, iCombi, FluxELU, Me.GorgesSoudures)
            End If

        Next

    End Sub

#End Region

#Region " Calcul coefficient de réduction plat inférieur "

    Private Sub InitialiseCoeffReduc(NbCombi As Integer, NbNodes As Integer)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Initialisation des tableaux pour le calcul des coefficients de réduction
        '   liés à la flexion transversale des semelles inférieures de slim
        '-----------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------
        ReDim Me.Psi_fi(NbCombi - 1, NbNodes - 1)
        ReDim Me.rho_t_fi(NbCombi - 1, NbNodes - 1)
        ReDim Me.Psi_y_fi(NbCombi - 1, NbNodes - 1)

        ReDim Me.Psi_spd(NbCombi - 1, NbNodes - 1)
        ReDim Me.rho_t_spd(NbCombi - 1, NbNodes - 1)
        ReDim Me.Psi_y_spd(NbCombi - 1, NbNodes - 1)
    End Sub

    Public Sub CalculCoefficiensReduction(iCombi As Integer, myPoutre As cls_Poutre, QEd() As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul des coefficients de réduction liés à la flexion transversale des semelles inférieures de slim
        '-----------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------

        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim deltaX As Decimal = myPoutre.LongueurTotale / myPoutre.Nodes.nbNodes

        Dim q, dapp, dbt, gammaM0 As Decimal

        '--> Déclaration

        iDebT = myPoutre.IndicePremiereTravee
        iFinT = myPoutre.IndiceDerniereTravee

        gammaM0 = myPoutre.Param.Gamma.GammaM0

        'calcul de dapp
        If Me.methodeReduction = MethodeReductionPlatSlimFloor.methode1_ReducAire Then
            dapp = 40 / 1000 '40 mm
        Else
            If myPoutre.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine Then
                Select Case myPoutre.Section.ProfilA.typeProfileAcier
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                        dapp = (myPoutre.Section.ProfilA.Plat_b - myPoutre.Section.ProfilA.Bfi) / 3
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                        dapp = (myPoutre.Section.ProfilA.Plat_b - myPoutre.Section.ProfilA.Bfs) / 3
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                        dapp = (myPoutre.Section.ProfilA.Bfi - myPoutre.Section.ProfilA.Plat_b) / 3
                    Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                        dapp = (myPoutre.Section.ProfilA.Bfi - myPoutre.Section.ProfilA.Bfs) / 3
                End Select
            Else
                dapp = (2 / 3) * OptionsSlimFloor.Bappmin
            End If
        End If

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myPoutre.Section.ProfilA

                    '== Suggestion pour DeltaX (POM)
                    If iNode = 0 Then
                        deltaX = (myPoutre.Nodes.xGlobal(iNode + 1) - myPoutre.Nodes.xGlobal(iNode)) / 2
                    ElseIf iNode = myPoutre.Nodes.nbNodes - 1 Then
                        deltaX = (myPoutre.Nodes.xGlobal(iNode) - myPoutre.Nodes.xGlobal(iNode - 1)) / 2
                    Else
                        deltaX = (myPoutre.Nodes.xGlobal(iNode + 1) - myPoutre.Nodes.xGlobal(iNode - 1)) / 2
                    End If
                    '====

                    q = QEd(iNode) / deltaX

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            Select Case Me.methodeReduction
                                Case MethodeReductionPlatSlimFloor.methode1_ReducAire
                                    Psi_spd(iCombi, iNode) = CalculPsi(q, .Plat_b, .Plat_t, myPoutre.Section.FySpd, .Plat_b - 2 * dapp, .Bfi, gammaM0)
                                    Psi_fi(iCombi, iNode) = CalculPsi(q, .Bfi, .Tfi, myPoutre.Section.FyInf, .Bfi, .Tw, gammaM0)

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case MethodeReductionPlatSlimFloor.methode2_ReducEpaisseur

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = CalculRhot(q, (.Plat_b - .Bfi) / 2 - dapp, .Plat_t, myPoutre.Section.FySpd, gammaM0)
                                    rho_t_fi(iCombi, iNode) = CalculRhot(q, (.Bfi - .Tw - .Rci) / 2 - dapp, .Tfi, myPoutre.Section.FyInf, gammaM0)

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case MethodeReductionPlatSlimFloor.methode3_ReducLimiteElasticite
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = CalculPsiY(q, (.Plat_b - .Bfi) / 2 - dapp, .Plat_t, myPoutre.Section.FySpd, gammaM0)
                                    Psi_y_fi(iCombi, iNode) = CalculPsiY(q, (.Bfi - .Tw - .Rci) / 2 - dapp, .Tfi, myPoutre.Section.FyInf, gammaM0)

                            End Select

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            Select Case Me.methodeReduction
                                Case MethodeReductionPlatSlimFloor.methode1_ReducAire
                                    Psi_spd(iCombi, iNode) = CalculPsi(q, .Plat_b, .Plat_t, myPoutre.Section.FySpd, .Plat_b - 2 * dapp, .Tw, gammaM0)
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case MethodeReductionPlatSlimFloor.methode2_ReducEpaisseur

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = CalculRhot(q, (.Plat_b - .Tw) / 2 - dapp, .Plat_t, myPoutre.Section.FySpd, gammaM0)
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case MethodeReductionPlatSlimFloor.methode3_ReducLimiteElasticite
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = CalculPsiY(q, (.Plat_b - .Tw) / 2 - dapp, .Plat_t, myPoutre.Section.FySpd, gammaM0)
                                    Psi_y_fi(iCombi, iNode) = 1

                            End Select

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            Select Case Me.methodeReduction
                                Case MethodeReductionPlatSlimFloor.methode1_ReducAire
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = CalculPsi(q, .Bfi, .Tfi, myPoutre.Section.FyInf, .Bfi - 2 * dapp, .Tw, gammaM0)

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case MethodeReductionPlatSlimFloor.methode2_ReducEpaisseur

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = CalculRhot(q, (.Bfi - .Tw - .Rci) / 2 - dapp, .Tfi, myPoutre.Section.FyInf, gammaM0)

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case MethodeReductionPlatSlimFloor.methode3_ReducLimiteElasticite
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = CalculPsiY(q, (.Bfi - .Tw - .Rci) / 2 - dapp, .Tfi, myPoutre.Section.FyInf, gammaM0)

                            End Select

                    End Select

                End With

            Next
        Next

    End Sub

    ''' <summary>
    ''' Fonction qui calcul le coefficient de réduction de l'aire du plat inférieur
    ''' </summary>
    ''' <param name="q">charge linéique</param>
    ''' <param name="b">largeur du plat</param>
    ''' <param name="t">épaisseur du plat</param>
    ''' <param name="fy">nuance d'acier du plat</param>
    ''' <param name="e1">distance entre les charges appliquées</param>
    ''' <param name="e2">distance entre les appuis</param>
    ''' <param name="gammaM0">coefficient partiel gammaM0</param>
    ''' <returns></returns>
    Private Function CalculPsi(q As Decimal, b As Decimal, t As Decimal, fy As Decimal,
                               e1 As Decimal, e2 As Decimal, gammaM0 As Decimal) As Decimal
        Dim psi, mu, lambda As Decimal

        mu = (e1 - e2) * q * gammaM0 / (t ^ 2 * fy)

        lambda = 1 - Math.Sqrt(1 - mu)

        'psi = 1 - (mu ^ 2 * t * 3 * Math.Sqrt(3) + lambda * mu * (2 * e1 + e2) - lambda ^ 2 * (e1 - e2)) / (6 * mu * b)
        psi = 1 - (mu * t * 3 * Math.Sqrt(3) + lambda * (2 * e1 + e2)) / (6 * b) - lambda ^ 2 * (e1 - e2) / (6 * mu * b)

        psi = Math.Max(psi, 0) 'minoration par 0 au cas où
        psi = Math.Min(psi, 1) 'majoration par 1 au cas où

        Return psi
    End Function

    ''' <summary>
    ''' Fonction qui calcul le coefficient de réduction de l'épaisseur du plat inférieur
    ''' </summary>
    ''' <param name="q">charge linéique</param>
    ''' <param name="dbt">bras de levier de la charge</param>
    ''' <param name="t">épaisseur du plat inférieur</param>
    ''' <param name="fy">nuance d'acier du plat inférieur</param>
    ''' <param name="gammaM0">coefficient partiel gammaM0</param>
    ''' <returns></returns>
    Private Function CalculRhot(q As Decimal, dbt As Decimal, t As Decimal, fy As Decimal, gammaM0 As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '   xx/xx/24 : Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction de la limite d'élasticité du plat support de slim floor
        '   d'après COSFB technical specifications
        '------------------------------------------------------------------------------------------------------------------
        '   q       [E] :   Charge sur le plat
        '   dbt     [E] :   Bras de levier de la charge
        '   t       [E] :   Epaisseur du plat
        '   GammaM0 [E] :   GammaM0
        '------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mxbt_Ed, mxbt_Rd, eta_m, rho_t As Decimal

        '--( Initialisation

        mxbt_Ed = q * dbt
        mxbt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0)

        '--( Calcul

        eta_m = Math.Min(Math.Abs(mxbt_Ed / mxbt_Rd), 1)

        rho_t = (1 / 2) * (1 + Math.Sqrt(1 - eta_m))

        'rho_t = Math.Max(rho_t, 0) 'minoration par 0 au cas où
        'rho_t = Math.Max(rho_t, 1) 'majoration par 1 au cas où

        Return rho_t
    End Function

    Private Function CalculPsiY(q As Decimal, dbt As Decimal, t As Decimal, fy As Decimal, gammaM0 As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '   xx/xx/24 : Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction de la limite d'élasticité du plat support de slim floor
        '   d'après annexe I de l'EN 1994-1-1:2025
        '------------------------------------------------------------------------------------------------------------------
        '   q       [E] :   Charge sur le plat
        '   dbt     [E] :   Bras de levier de la charge
        '   t       [E] :   Epaisseur du plat
        '   GammaM0 [E] :   GammaM0
        '------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mybt_Ed, mybt_Rd, eta_m, psiY As Decimal

        '--( Initialisation

        mybt_Ed = q * dbt
        mybt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0)

        '--( Calcul

        eta_m = Math.Min(Math.Abs(mybt_Ed / mybt_Rd), 1)

        psiY = (eta_m - Math.Sqrt(eta_m ^ 2 - 16 * eta_m + 16)) / (2 * (eta_m - 2))

        'psiY = Math.Max(psiY, 0) 'minoration par 0 au cas où
        'psiY = Math.Min(psiY, 1) 'majoration par 1 au cas où

        Return psiY

    End Function

#End Region

#Region " Calcul des soudures "

    '--> A COMPLETER

#End Region

#Region " Vérification de la poutre acier "

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
                                       MplRd As Decimal(,), MelRd As Decimal(,), ClasseP As Integer, ClasseM As Integer, ByRef lClasse4 As Boolean)
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
                                MRd = MplRd(iNode, k)
                            Case 3
                                MRd = MelRd(iNode, k)
                            Case 4
                                lClasse4 = True
                                'lOk = False
                        End Select

                    Else

                        Select Case ClasseM
                            Case 1, 2
                                MRd = MplRd(iNode, k)
                            Case 3
                                MRd = MelRd(iNode, k)
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

    Private Sub RunCritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VRd As Decimal)
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
                    Me.CritereV.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
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
