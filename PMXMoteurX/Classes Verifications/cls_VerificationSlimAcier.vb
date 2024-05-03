Public Class cls_VerificationSlimAcier
    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES SLIMFLOOR ACIER
    '=========================================================================================================

#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    'Public CritereVb As cls_Critere                 ' Pas de voilement par cisaillement
    Public CritereMV As cls_Critere                 ' Résistance à l'interacion MV
    'Public CritereMVb As cls_Critere                ' Résistance à l'interacion M+voilement par cisaillement
    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereTauA As cls_Critere               ' Critère de contrainte de cisaillement élastique
    Public CritereSigmaVM As cls_Critere            ' Critère de contrainte élastique équivalente de Von Mises
    'Public CritereLTB As cls_Critere                ' Pas de déversement

    Public Psi, rho_t, Psi_y As Decimal(,)                       ' Coefficient de réduction : 1er indice: indice de la combinaison, 2eme indice: indice du noeud

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

    Public GorgesSoudures(1) As Decimal             ' Gorge des soudures ame semelles pour les sections PRS
    Public GorgesSouduresMini(1) As Decimal         ' Gorge mini des soudures ame semelles pour les sections PRS

#End Region

#Region " Constructeurs "

    Public Sub New()
        lCalculPlastic = True
        methodeReduction = MethodeReductionPlatSlimFloor.methode1_ReducAire
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
        '   Vérification aux ELU d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam            [E] :   Poutre vérifiée
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim VplRd As Decimal                            ' Résistance plastique au cisaillement
        'Dim VbRd As Decimal                             ' Résistance au voilement par cisaillement (a priori constant le long de la poutre)
        Dim VRd As Decimal                              ' Résistance à l'effort tranchant (soit plastique, soit voilement)
        'Dim lTwoAdjacentCantilevers As Boolean          ' indique la présence de deux travées adjacentes en consoles (True) ou non
        Dim iCombi As Integer
        Dim combiELU As New cls_Combinaisons
        Dim nbCombiELU As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim MplRd(,), zANP(,) As Decimal
        Dim zANPMV(,) As Decimal = Nothing                ' Position ANP, tenant compte de l'interaction avec l'effort tranchant 
        Dim MVRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de l'interaction avec l'effort tranchant 
        Dim MelRd(,), zANE(,) As Decimal
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
        'Dim lEnrob As Boolean = myBeam.lEnrobage
        'Dim lproPRS As Boolean = Not myBeam.Section.lLamine

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

        '# Tranchant résistant

        VplRd = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM0)

        '# Résistance au voilement par cisaillement

        'sans objet pour les slimfloors

        '# Résistance à l'effort tranchant

        VRd = VplRd






        ''# Propriétés

        'myBeam.ProprietesVerifAcier(True, MplRd, zANP, MelRd, zANE)

        ''# Classes de la section

        ''    La classe des sections ne dépend pas du chargement (il n'y a pas d'effort axial) ni des contraintes.
        ''    On classe donc les sections une fois pour toute, en dehors de la boucle sur les combinaisons de calcul

        'ClasseP = myBeam.Section.ClasseSection(zANP, zANE, True, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1)
        'ClasseM = myBeam.Section.ClasseSection(zANP, zANE, False, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1)

        ''# Type de vérification pour les sections

        'lVerifElastic = myBeam.Param.lElasticDesignVM Or (ClasseP > 2)
        'If myBeam.lMultiSpan Then
        '    '# dans le cas d'une poutre à plusieurs travées, on prend aussi en compte la classe de section en flexion négative
        '    lVerifElastic = lVerifElastic Or (ClasseM > 2)
        'End If
        'Me.lCalculPlastic = Not lVerifElastic

        ''# Initialisation des critères dépendant du type de vérification

        'Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee, lVerifElastic, myBeam.Param.lElasticDesignVM)

        ''# Contraintes normales

        'If lVerifElastic Then
        '    myBeam.PtsSigma.Initialise(myBeam)
        '    myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaCas)
        'End If

        ''# Contraintes de cisaillement
        'If myBeam.Param.lElasticDesignVM Then
        '    Me.Tau = New cls_Tau(myBeam.Section.TypeSection)
        '    Me.Tau.Initialise(myBeam.Section.ProfilA)
        '    Me.Tau.CalculContraintesCharges(myBeam, TauCas)
        'End If

        ''# Flux de cisaillement des PRS
        'If lproPRS Then
        '    myBeam.Section.ProfilA.InitialiseSoudureMini(Me.GorgesSouduresMini)
        '    Me.FluxF = New cls_Flux
        '    Me.FluxF.InitialiseCalculAcier(myBeam)
        '    Me.FluxF.CalculFluxChargesACIER(myBeam, FluxCas)
        'End If

        ''--> Boucle sur les combinaisons

        'For iCombi = 0 To combiELU.nbCombi - 1

        '    '# Combinaisons des moments

        '    combiELU.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

        '    '# Combinaison des efforts tranchants

        '    combiELU.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

        '    '# Combinaisons des contraintes

        '    If lVerifElastic Then
        '        '( Contraintes normales
        '        combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, myBeam.PtsSigma.zPos.Count, myBeam.Nodes.nbNodes,
        '                                            myBeam.ChargesA, SigmaCas, lRetraitElastique, SigmaELU)
        '        myBeam.PtsSigma.AjusteContraintes(myBeam, SigmaELU)

        '        '( Contraintes de cisaillement
        '        If myBeam.Param.lElasticDesignVM Then
        '            combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, Me.Tau.MStatic.Count, myBeam.Nodes.nbNodes,
        '                                                myBeam.ChargesA, TauCas, lRetraitElastique, TauELU)
        '        End If
        '    End If
        '    If lproPRS Then
        '        combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, cls_Flux.NbPTS, myBeam.Nodes.nbNodes,
        '                                            myBeam.ChargesA, FluxCas, lRetraitElastique, FluxELU)
        '    End If

        '    '# Vérification sous moment fléchissant

        '    If lVerifElastic Then
        '        RunCritereFlexionResistanceElastiqueVM(myBeam, iCombi, SigmaELU)
        '    Else
        '        Me.RunCritereFlexionAcier(myBeam, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lClasse4)
        '    End If

        '    If myBeam.Param.lElasticDesignVM Then 'calcul élastique imposé 

        '        '# Vérification sous effot tranchant
        '        Me.RunCritereCisaillementResistanceElastiqueVM(myBeam, iCombi, TauELU)

        '        '# Vérification sous interaction MV
        '        Me.RunCritereInteractionMVElastiqueVonMises(myBeam, iCombi, SigmaELU, TauELU)

        '    Else 'calcul plastique, même pour les sections de classe 3, si le calcul élastique n'est pas imposé

        '        '# Vérification sous effort tranchant

        '        Me.RunCritereTranchants(myBeam, iCombi, VEd, VplRd)

        '        '# Vérification au voilement par cisaillement

        '        If Me.ShearB.lCheckRequired Then Me.RunCritereVoilementCisaillement(myBeam, iCombi, VEd, VbRd)

        '        '# Traitement de l'interaction MV en fonction de la sensibilité au voilement par cisaillement

        '        If Me.ShearB.lCheckRequired Then

        '            '# Vérification sous interaction MVb

        '            Me.RunCriteresInteractionMVb(myBeam, iCombi, VbRd, MEd, VEd, MfRd, MplRd)

        '        Else
        '            '# Calcul du critère d'intéraction rhoV

        '            Me.CalculRhoV(iCombi, myBeam)

        '            '# Propriétés avec prise en compte de l'interaction MV

        '            myBeam.ProprietesVerifMVAcier(iCombi, myBeam, True, MVRd, zANPMV, Me.RhoV)

        '            '# Vérification sous interaction MV

        '            Me.RunCriteresInteractionMV(myBeam, iCombi, MEd, MVRd)

        '        End If

        '    End If

        '    '# Vérification au déversement

        '    Me.RunCritereDeversement(myBeam, iCombi, MEd, lVerifElastic, lConstructionPhase)

        '    '# Dimensionnement des soudures de PRS

        '    If lproPRS Then
        '        Me.RunDimensionSouduresAmeSemelle(myBeam, iCombi, FluxELU, Me.GorgesSoudures)
        '    End If
        'Next

    End Sub

#End Region

#Region " Calcul coefficient de réduction plat inférieur "

    Private Sub InitialiseCoeffReduc(NbCombi As Integer, NbNodes As Integer)
        ReDim Me.Psi(NbCombi - 1, NbNodes - 1)
        ReDim Me.rho_t(NbCombi - 1, NbNodes - 1)
        ReDim Me.Psi_y(NbCombi - 1, NbNodes - 1)
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

        psi = 1 - (mu ^ 2 * t * 3 * Math.Sqrt(3) + lambda * mu * (2 * e1 + e2) - lambda ^ 2 * (e1 - e2)) / (6 * mu * b)

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
        Dim mxbt_Ed, mxbt_Rd, eta_m, rho_t As Decimal

        mxbt_Ed = q * dbt
        mxbt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0)

        eta_m = mxbt_Ed / mxbt_Rd

        rho_t = (1 / 2) * (1 + Math.Sqrt(1 - eta_m))

        rho_t = Math.Max(rho_t, 0) 'minoration par 0 au cas où
        rho_t = Math.Max(rho_t, 1) 'majoration par 1 au cas où

        Return rho_t
    End Function

    Private Function CalculPsiY(q As Decimal, dbt As Decimal, t As Decimal, fy As Decimal, gammaM0 As Decimal) As Decimal
        Dim mybt_Ed, mybt_Rd, eta_m, psiY As Decimal

        mybt_Ed = q * dbt
        mybt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0)

        eta_m = mybt_Ed / mybt_Rd

        psiY = (eta_m - Math.Sqrt(eta_m ^ 2 - 16 * eta_m + 16)) / (2 * (eta_m - 2))

        psiY = Math.Max(psiY, 0) 'minoration par 0 au cas où
        psiY = Math.Min(psiY, 1) 'majoration par 1 au cas où

        Return psiY

    End Function

#End Region

End Class
