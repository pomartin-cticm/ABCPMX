Public Class cls_VerificationsSlimMixtes

    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES MIXTES
    '=========================================================================================================

#Region " Attributs "

    Private ConvSigneT As Decimal                   ' Convention de signe pour les contraintes de traction

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereMY As cls_Critere                 ' Resistance à la flexion transversale 

    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereMV As cls_Critere                 ' Résistance à l'interacion MV

    'Public CritereVb As cls_Critere                 ' Pas de voilement par cisaillement pour les slimfloors 
    'Public CritereMVb As cls_Critere                ' Pas d'interaction flexion + voilement par cisaillement

    Public CritereConnex As cls_Critere             ' Resistance de la connexion en calcul élastique

    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereSigmaPl As cls_Critere            ' Critère de résistance en flexion  / Contrainte normale dans le plat
    Public CritereSigmaC As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton de la dalle
    'Public CritereSigmaArmaC As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures de la dalle
    'Public CritereSigmaE As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton d'enrobage
    'Public CritereSigmaArmaE As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures d'enrobage
    Public CritereTauA As cls_Critere               ' Critère de contrainte de cisaillement élastique
    Public CritereSigmaVM As cls_Critere            ' Critère de contrainte élastique équivalente de Von Mises

    Public RhoV As Decimal(,)                       ' Coefficient d'interaction : 1er indice: indice de la combinaison, 2eme indice: indice du noeud

    '== Pour tous les coefficents de réduction ci-dessous, : 1er indice: indice de la combinaison, 2eme indice: indice du noeud
    Public PsiAfi As Decimal(,)                     ' Coefficient de réduction de la semelle inférieure (aire, méthode 1)
    Public RhoTfi As Decimal(,)                     ' Coefficient de réduction de la semelle inférieure (épaisseur, méthode 2)
    Public PsiYfi As Decimal(,)                     ' Coefficient de réduction de la semelle inférieure (limite d'élasticité, méthode 3)
    Public PsiAspd As Decimal(,)                    ' Coefficient de réduction du plat soudé inférieur (aire, méthode 1)
    Public RhoTspd As Decimal(,)                    ' Coefficient de réduction du plat soudé inférieur (épaisseur, méthode 2)
    Public PsiYspd As Decimal(,)                    ' Coefficient de réduction du plat soudé inférieur (limite d'élasticité, méthode 3)

    Public methodeReduction As MethodeReductionPlatSlimFloor

    Enum MethodeReductionPlatSlimFloor
        methode1_ReducAire
        methode2_ReducEpaisseur
        methode3_ReducLimiteElasticite
    End Enum

    Public lCalculPlastic As Boolean                ' Indique si le dimensionnement est suivant la théorie plastique
    Public lBetaPlasticOK As Boolean                ' Indique si le calcul plastique possible et compatible avec domaine d'application de la figure 8.3 EN 1994-1-1:2024

    Public DegConnex(,,) As Decimal = Nothing       ' Degré de connexion : 1er indice : combinaison; 2eme indice : travée, 3eme indice : 0 pour M>0 et 1 pour M<0
    Public DegConnexMin() As Decimal = Nothing      ' Degré minimal de connexion en moment positif (indice de la travée)

    'Public ShearB As strucShearBuckling             ' Pas de voilement par cisaillement

    '==( Classe pour le calcul des contraintes de cisaillement en calcul élastique imposé

    Dim Tau As cls_Tau

    '==( Classe pour le caclul des flux de cisaillement dans les soudures des PRS et dans la connection

    Dim FluxF As cls_Flux

    Public GorgesSoudures() As Decimal             ' Gorge des soudures ame semelles pour les sections PRS
    Public GorgesSouduresMini() As Decimal         ' Gorge mini des soudures ame semelles pour les sections PRS

#End Region

#Region " Attributs pour le calcul des armatures transversales"

    ''' <summary>
    ''' Contrainte tangentielle / zone de flexion positive (True) ou négative (False) / Type de surface de ruine 
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public TauEd(,,) As Decimal

    ''' <summary>
    ''' Angle de la bielle de compression EN RADIAN / zone de flexion positive (True) ou négative (False) / Type de surface de ruine 
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public Thetaf(,,) As Decimal

    ''' <summary>
    ''' Angle min de la bielle de compression EN RADIAN (dépend de si la zone se situe en flexion positive ou négative)
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public Thetaf_min(,) As Decimal

    ''' <summary>
    ''' Vérification de la bielle de compression 
    ''' (GUD: pour l'instant je mets ici l'attribut car le critère est constant le long d'une zone de connexion. A voir s'il faut le déplacer dans la classe vérification)
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public Gamma_sf(,,) As Decimal

    ''' <summary>
    ''' Aire par unité de longueur des armatures transversales / zone de flexion positive (True) ou négative (False) / Type de surface de ruine 
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>

    Public As_s_transv(,,) As Decimal
#End Region

#Region " Constructeurs "

    Public Sub New()

        lCalculPlastic = False
        Me.ConvSigneT = cls_PointsSigma.CONVSIGNETRACTION
        Me.lBetaPlasticOK = True
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, nbCombi As Integer, IndDerniereT As Integer)

        Me.CritereM = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereMY = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereV = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        'Me.CritereVb = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereMV = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        'Me.CritereMVb = New cls_Critere(NbNodes, nbCombi, IndDerniereT)

        ReDim TauEd(IndDerniereT, 2, 2)
        ReDim Thetaf(IndDerniereT, 2, 2)
        ReDim Thetaf_min(IndDerniereT, 2)
        ReDim Gamma_sf(IndDerniereT, 2, 2)
        ReDim As_s_transv(IndDerniereT, 2, 2)

    End Sub

    Private Sub InitialiseCriteresVM(NbNodes As Integer, nbCombi As Integer, IndDerniereT As Integer)
        '-------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '-------------------------------------------------------------------
        '   Initialisation des critères pour les contraintes normales
        '-------------------------------------------------------------------

        Me.CritereSigmaA = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaPl = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        'Me.CritereTauA = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaVM = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaC = New cls_Critere(NbNodes, nbCombi, IndDerniereT)

        Me.CritereConnex = New cls_Critere(NbNodes, nbCombi, IndDerniereT)

    End Sub

#End Region

#Region "===Gestion globale de la vérification==="

    Public Sub Z_VerificationELU(myBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre mixte acier béton
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre vérifiée
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iCombi As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim QEd() As Decimal = Nothing                  ' tableau des forces nodales
        Dim QSupEd() As Decimal = Nothing               ' tableau des forces nodales / unité longueur

        Dim VplRd As Decimal                            ' Effort tranchant résistant (a priori constant le long de la poutre)
        'Dim VbRd As Decimal                             ' Résistance au voilement par cisaillement (a priori constant le long de la poutre)
        'Dim lTwoAdjacentCantilevers As Boolean          ' indique la présence de deux travées adjacentes en consoles (True) ou non
        Dim zANE(,) As Decimal = Nothing                ' Position des ANE sous moment 
        Dim zANP(,) As Decimal = Nothing                ' Position ANP, tenant compte de MEd et du degré de connexion
        Dim zANPMV(,) As Decimal = Nothing              ' Position ANP, tenant compte de MEd, du degré de connexion et de l'interaction avec l'effort tranchant 
        Dim MplRd(,) As Decimal = Nothing               ' Moments plastiques, tenant compte de MEd et du degré de connexion
        Dim MVRd(,) As Decimal = Nothing                ' Moments plastiques, tenant compte de MEd, du degré de connexion et de l'interaction avec l'effort tranchant 
        Dim MfRd(,) As Decimal = Nothing                ' Moments plastiques, des semelles seules

        Dim lRElastiqueImpose As Boolean = False        ' Vérification élastique imposée
        'Dim lRElastique As Boolean
        Dim ClasseSection(,) As Integer = Nothing       ' Tableau dimensions (NbNodes, 0 ou 1 pour gauche ou droite)
        Dim Beff() As Decimal = {0}                     ' Largeurs participantes de la dalle
        Dim lSimple As Boolean = False
        '   Dim ClasseP(), ClasseM() As Integer         ' Tableau des classes de section en flexion poisitive et négative
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1

        Dim iNodeMmax() As Integer = Nothing
        Dim Mmax() As Decimal = Nothing
        Dim xMZero(,) As Decimal = Nothing
        Dim lTraveeMomNeg() As Boolean = Nothing

        Dim SigmaP(,,,) As Decimal = Nothing            ' Contraintes normales dans l'hypothèse d'un moment positif
        Dim SigmaM(,,,) As Decimal = Nothing            ' Contraintes normales dans l'hypothèse d'un moment négatif
        Dim SigmaELU(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELU
        Dim TauELU(,,) As Decimal = Nothing             ' Contraintes de cisaillement sous 1 combinaison ELU
        Dim TauCas(,,,) As Decimal = Nothing            ' Contraintes de cisaillement pour les cas de charges
        Dim lRetraitElastique As Boolean = True

        Dim lClasse3, lClasse4 As Boolean               ' Indique si présence d'au moins une section de classe 3 ou de classe 4
        Dim DeltaRd() As List(Of Decimal) = Nothing

        'Dim EpsilonW As Decimal
        'Dim lEnrob As Boolean
        Dim lCont As Boolean
        Dim lCombiClass3 As Boolean                     ' Indique s'il existe au moins une combinaison avec classe 3
        Dim lCombiClass4 As Boolean                     ' Indique s'il existe au moins une combinaison avec classe 4
        Dim lFirst As Boolean = True
        Dim nbCombi As Integer
        'Dim lproPRS As Boolean = Not myBeam.Section.lLamine

        Dim FluxCas(,,,) As Decimal = Nothing           ' Flux de cisaillement dans les soudures de PRS et dans la connection par cas de charges
        Dim FluxELU(,,) As Decimal = Nothing            ' Flux de cisaillement dans les soudures de PRS et dans la connection aux ELU
        Dim FluxRd(,) As Decimal = Nothing              ' Résistance de la connexion / u longueur le long de la barre
        Dim lPlastOK() As Boolean = {True, True}

        Dim CSlim As New cls_CalculSlim

        Dim lPlasticBeta As Boolean = True              ' Indique si le calcul plastique est possible d'après la définition du coefficient beta

        '--> Initialisations

        lCombiClass3 = False
        lCombiClass4 = False
        'lEnrob = myBeam.lEnrobage
        nbCombi = myBeam.CombiA_ELU.nbCombi

        '# Degré de connexion

        Me.InitialiseDegreConnexion(nbCombi, myBeam.IndiceDerniereTravee)

        '# Critères

        Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombi, myBeam.IndiceDerniereTravee)
        Me.InitialiseRhoV(nbCombi, myBeam.Nodes.nbNodes)

        '# Largeurs participantes

        lSimple = myBeam.Param.lLargeurEfficaceSimplifiee
        myBeam.MaillageBeff(lSimple, False, Beff)

        '# Tableaux

        Me.InitialiseCoeffReduc(myBeam.CombiA_ELU.nbCombi, myBeam.Nodes.nbNodes)

        '# Tranchant résistant

        VplRd = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM0, myBeam.Param.EtaW)

        '# Résistance au voilement par cisaillement
        '--> sans objet

        '# Moments plastiques 

        'myBeam.MaillagePropPlastiquesMixtes(Beff, 1, True, lGeneration1, True, MplRdPlus, zANPPlus, lPlastOK(0))
        'myBeam.MaillagePropPlastiquesMixtes(Beff, -1, True, lGeneration1, False, MplRdMoins, zANPMoins, lPlastOK(1))

        '# Calcul des contraintes normales élastiques pour les cas de charges

        myBeam.PtsSigma.Initialise(myBeam, False)
        myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaP)
        myBeam.PtsSigma.CalculContraintesCharges(myBeam, -1, SigmaM)

        '# Calcul des contraintes de cisaillement pour un calcul élastique
        If myBeam.Param.lElasticDesignVM Then
            Me.Tau = New cls_Tau(myBeam.Section.TypeSection)
            Me.Tau.CalculContraintesChargesMIXTE(myBeam, TauCas)
        End If

        '   On calcule le flux de cisaillement tout le temps, car on ne sait pas si on va en avoir besoin, si section de classe 4
        Me.FluxF = New cls_Flux
        Me.FluxF.InitialiseCalculMixte(myBeam)
        Me.FluxF.CalculFluxChargesMIXTE(myBeam, Beff, FluxCas)

        '# Résistance élastique de la connexion / u longueur
        Me.InitialiseCriteresVM(myBeam.Nodes.nbNodes, nbCombi, myBeam.IndiceDerniereTravee)
        Me.InitialiseResistanceElastiqueConnexion(myBeam, FluxRd)

        '--> Boucle sur les combinaisons

        lCont = True
        Me.lCalculPlastic = (Not myBeam.Param.lElasticDesignVM) And (Not myBeam.Param.lElasticDesignCl3)
        Me.lBetaPlasticOK = True

        Do While lCont

            lFirst = True
            For iCombi = 0 To myBeam.CombiA_ELU.nbCombi - 1

                '# Combinaisons des moments, efforts tranchants

                myBeam.CombiA_ELU.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, Not Me.lCalculPlastic)

                '# Combinaison des efforts tranchants

                myBeam.CombiA_ELU.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, Not Me.lCalculPlastic)

                '# Recupération des efforts nodaux à partir des tranchants combinés

                myBeam.CombiA_ELU.RecupererEffortsNodauxPonderees(myBeam.Nodes, VEd, QEd, QSupEd)

                '# Calcul des coefficients de réduction 

                CSlim.CalculCoefficientsReduction(iCombi, myBeam, QEd, PsiAfi, RhoTfi, PsiYfi, PsiAspd, RhoTspd, PsiYspd)

                '# Combinaison des contraintes normales élastiques

                ' On prend en compte le retrait dans le cas d'un calcul élastique
                myBeam.CombiA_ELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, myBeam.PtsSigma.zPos.Count, myBeam.Nodes.nbNodes,
                                                     myBeam.ChargesA, MEd, SigmaP, SigmaM, lRetraitElastique, SigmaELU)
                myBeam.PtsSigma.AjusteContraintes(myBeam, SigmaELU)

                '# Combinaison des contraintes de cisaillement élastiques

                If myBeam.Param.lElasticDesignVM Then
                    myBeam.CombiA_ELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, Me.Tau.NbPts, myBeam.Nodes.nbNodes,
                                                         myBeam.ChargesA, TauCas, lRetraitElastique, TauELU)
                End If

                '# Combinaison des flux de cisaillement si calcul élastique (pour la connexion) ou si PRS (pour les soudures)

                If Not Me.lCalculPlastic Then
                    myBeam.CombiA_ELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, cls_Flux.NbPTS, myBeam.Nodes.nbNodes,
                                                         myBeam.ChargesA, FluxCas, lRetraitElastique, FluxELU)
                End If

                '# Position de l'ANE en fonction des contraintes dans le profilé

                myBeam.RechercheANEFromSigma(SigmaELU, MEd, myBeam.Nodes.nbNodes, zANE)

                '# Analyse du diagramme de moment

                myBeam.AnalyseDiagrammeMoments(MEd, iNodeMmax, Mmax, xMZero, lTraveeMomNeg)

                '# Calcul des propriétés plastiques le long de la barre,
                ' avec prise en compte de la connection,
                ' sans prise en compte de la réduction induit par l'effort tranchant 

                myBeam.MaillageRConnexion(xMZero, DeltaRd)

                Me.MaillageProprietesPlastiques(iCombi, myBeam, MEd, DeltaRd, Beff, zANP, MplRd, lPlasticBeta)

                '# Classes des sections

                'Me.CalculeClasseSectionsMaillage(myBeam, MEd, zANE, zANPPlus, zANPMoins, ClasseSection, lClasse3, lClasse4)
                Me.CalculeClasseSectionsMaillage(myBeam, MEd, zANE, zANP, ClasseSection, lClasse3, lClasse4)
                'lCombiClass3 = lCombiClass3 And lClasse3
                'lCombiClass4 = lCombiClass4 And lClasse4

                '/!\ MODIF GUD: A VERIFIER
                lCombiClass3 = lCombiClass3 Or lClasse3
                lCombiClass4 = lCombiClass4 Or lClasse4

                '# Degré de connexion

                If Me.lCalculPlastic And (Not (lClasse3 Or lClasse4)) Then
                    Me.CheckDegreConnexion(myBeam, DeltaRd, iNodeMmax, iCombi)
                End If

                '# Vérification de la connexion en calcul élastique

                If (Not Me.lCalculPlastic) Then
                    Me.RunCalculElastiqueConnexion(myBeam, iCombi, FluxELU, FluxRd)
                End If

                '# Vérification sous moment fléchissant

                Me.RunCritereMoments(myBeam, lFirst, iCombi, Me.lCalculPlastic, lClasse3, MEd, SigmaELU, MplRd)

                '# Vérification de la flexion locale

                CSlim.RunCritereResistancePlastiquePlatY_N(myBeam, iCombi, QSupEd, Me.CritereMY)

                '# Vérification sous effort tranchant

                If myBeam.Param.lElasticDesignVM Then
                    '# Vérification élastique du cisaillement dans l'âme du profilé
                    Me.RunCritereCisaillementElastic(myBeam, iCombi, TauELU)
                    '# Contrainte equivalente de VM
                    Me.RunCritereInteractionMVElastiqueVonMises(myBeam, iCombi, SigmaELU, TauELU)
                Else
                    '# Critère de résistance plastique
                    CSlim.RunCritereTranchants(myBeam, iCombi, VEd, VplRd, Me.CritereV)
                End If

                '# Vérification du voilement par cisaillement

                '--> Sans objet 

                '# Vérification résistance interaction MV

                '# Calcul du critère d'interaction rhoV

                Me.CalculRhoV(iCombi, myBeam)

                '# Calcul des propriétés plastiques le long de la barre,
                ' avec prise en compte de la connection,
                ' sans prise en compte de la réduction induit par l'effort tranchant 

                Me.MaillageProprietesPlastiques(iCombi, myBeam, MEd, DeltaRd, Beff, zANPMV, MVRd, lPlasticBeta, Me.RhoV)

                '# Vérification sous interaction MV

                Me.RunCriteresInteractionMV(myBeam, iCombi, MEd, MVRd)

                '# Calcul des soudures 

                ' /!\ A COMPLETER

            Next

            '# Doit on refaire un boucle sur les combinaisons

            If Me.lCalculPlastic Then
                If lCombiClass3 Then
                    lCont = True
                    Me.lCalculPlastic = False
                ElseIf (Not lPlasticBeta) Then
                    lCont = True
                    Me.lCalculPlastic = False
                    Me.lBetaPlasticOK = False
                Else
                    lCont = False
                End If
            Else
                lCont = False
            End If

        Loop

        '# Calcul armatures transversales

        ' Me.CalculArmaturesTransversales(myBeam, 0)


    End Sub

    Private Sub MaillageProprietesPlastiques(iCombi As Integer, myBeam As cls_Poutre, MEd(,) As Decimal, DeltaRd() As List(Of Decimal), bEff() As Decimal,
                                             ByRef pzANP(,) As Decimal, ByRef pMPlRd(,) As Decimal, ByRef lPlastic As Boolean, Optional rhoV As Decimal(,) = Nothing)
        '----------------------------------------------------------------------------------------------------------
        '   02/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques le long de la barre en fonction de 
        '   du moment sollicitant et du degré de connection
        '----------------------------------------------------------------------------------------------------------
        '   myBeam          [E] :   Poutre traitée
        '   MEd             [E] :   Diagramme de moment aux ELU
        '   DeltaRd         [E] :   Cumul des résistance des PRd entre les sections et les points de moment nul
        '   bEff            [E] :   Largeur efficace de dalle
        '   RhoV            [E] :   Coefficient pour l'interaction MV
        '   pzANP           [S] :   position ANP
        '   pMplRd          [S] :   moment plastique (en fonction du signe de MEd)
        '   lPlastic        [S] :   Indique si le calcul est possible en théorie plastique (True) ou élastique (False)
        '                           Renvoie faux quand le coefficient z/Hht est en dehors du domaine d'application
        '                           de la figure 8.3 dans EN 1994-1-1:2024
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = myBeam.Nodes.nbNodes
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim iNode As Integer
        Dim iNodeDeb, iNodeFin As Integer
        Dim kDeb, kfin As Integer
        Dim rhoVLoc As Decimal
        Dim Signe As Decimal
        Dim Beta As Decimal
        Dim myEN1994 As New cls_Eurocodes
        Dim zTop As Decimal
        Dim lGene1 As Boolean = False
        Dim Nuance As String = myBeam.Section.Acier.Nuance
        Dim lOKPl As Boolean   '== PARAMETRE A GERER

        '--> Initialisation

        iTravDeb = myBeam.IndicePremiereTravee
        iTravFin = myBeam.IndiceDerniereTravee
        ReDim pzANP(NbNodes - 1, 1)
        ReDim pMPlRd(NbNodes - 1, 1)
        zTop = myBeam.Dalle.zTop

        '--> Traitement

        For iTravee = iTravDeb To iTravFin

            iNodeDeb = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeFin = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iNodeDeb To iNodeFin
                If iNode = iNodeDeb Then kDeb = 1 Else kDeb = 0
                If iNode = iNodeFin Then kfin = 0 Else kfin = 1

                If IsEqual(MEd(iNode, kDeb), 0) Then Signe = 1 Else Signe = Math.Sign(MEd(iNode, kDeb))

                If rhoV Is Nothing Then
                    rhoVLoc = 0
                Else
                    rhoVLoc = rhoV(iCombi, iNode)
                End If

                'myBeam.Section.ProprietesPlastiquesMixteMyyEta(Signe, True, myBeam.Param.Gamma, RhoV,
                '                                                 bEff(iNode), DeltaRd(iTravee)(iNodeDeb + iNode), myBeam.Dalle, pzANP(iNode, kDeb), pMPlRd(iNode, kDeb))
                myBeam.Section.ProprietesPlastiquesMixteMyyEta(Signe, True, myBeam.Param.Gamma, rhoVLoc,
                                                               bEff(iNode), DeltaRd(iTravee)(iNode - iNodeDeb),
                                                               myBeam.Dalle, True, pzANP(iNode, kDeb), pMPlRd(iNode, kDeb))

                Beta = myEN1994.ReductionFactorBeta(zTop - pzANP(iNode, kDeb), myBeam.HauteurTotaleSectionMixte, Nuance, lGene1, lOKPl)

                lPlastic = lPlastic And lOKPl

                If kfin > kDeb Then
                    pzANP(iNode, kfin) = pzANP(iNode, kDeb)
                    pMPlRd(iNode, kfin) = Beta * pMPlRd(iNode, kDeb)
                End If
            Next

        Next

    End Sub

    Private Sub MaillageProprietesMfRd(MyPoutre As cls_Poutre, MEd(,) As Decimal, DeltaRd() As List(Of Decimal), bEff() As Decimal, ByRef pMfRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   02/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques MfRd le long de la barre en fonction de 
        '   du moment sollicitant et du degré de connection
        '   MfRd : Section ne comprennant que les semelles, ce qui revient à calculer avec RhoV=1
        '----------------------------------------------------------------------------------------------------------
        '   myBeam          [E] :   Poutre traitée
        '   MEd             [E] :   Diagramme de moment aux ELU
        '   DeltaRd         [E] :   Cumul des résistance des PRd entre les sections et les points de moment nul
        '   bEff            [E] :   Largeur efficace de dalle
        '   pMfRd           [S] :   moments plastiques des semelles seules (en fonction du signe de MEd)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim iNode As Integer
        Dim iNodeDeb, iNodeFin As Integer
        Dim kDeb, kfin As Integer
        Dim rhoVLoc As Decimal
        Dim Signe As Decimal

        Dim zANP As Decimal

        '--> Initialisation

        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee

        ReDim pMfRd(NbNodes - 1, 1)

        '--> Traitement

        For iTravee = iTravDeb To iTravFin

            iNodeDeb = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeFin = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iNodeDeb To iNodeFin
                If iNode = iNodeDeb Then kDeb = 1 Else kDeb = 0
                If iNode = iNodeFin Then kfin = 0 Else kfin = 1

                If IsEqual(MEd(iNode, kDeb), 0) Then Signe = 1 Else Signe = Math.Sign(MEd(iNode, kDeb))

                '== On supprime la résistance de l'âme
                rhoVLoc = 1
                '==

                MyPoutre.Section.ProprietesPlastiquesMixteMyyEta(Signe, True, MyPoutre.Param.Gamma, rhoVLoc,
                                                                 bEff(iNode), DeltaRd(iTravee)(iNode - iNodeDeb), MyPoutre.Dalle, True, zANP, pMfRd(iNode, kDeb))

                If kfin > kDeb Then
                    pMfRd(iNode, kfin) = pMfRd(iNode, kDeb)
                End If
            Next

        Next

    End Sub

    Private Sub CalculeClasseSectionsMaillage(MyPoutre As cls_Poutre, MEd(,) As Decimal, zANE(,) As Decimal,
                                              zANPPlus() As Decimal, zANPMoins() As Decimal, ByRef ClasseSection(,) As Integer,
                                              ByRef lClasse3 As Boolean, ByRef lClasse4 As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   02/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la calsse des sections le long du maillage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam        [E] :   Poutre à traiter
        '   MEd             [E] :   Diagramme des moments aux ELU
        '   zANPPlus        [E] :   Positions des ANP plastiques sous moments > 0
        '   zANPMoins       [E] :   Positions des ANP plastiques sous moments < 0
        '   zANE            [E] :   Position de l'ANE au droit du noeud
        '   ClasseSection   [S] :   Classe des sections (calculée en fonction du signe de MEd)
        '   lClasse3        [S] :   Indique si au moins une des sections est de classe 3
        '   lClasse4        [S] :   Indique si au moins une des sections est de classe 4
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lGeneration1 As Boolean = MyPoutre.Param.lGeneration1
        Const lWEB As Boolean = True
        Dim lRec As Boolean

        '--> Initialisation

        ReDim ClasseSection(MyPoutre.Nodes.nbNodes - 1, 1)

        '--> Boucle sur les noeuds

        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1
            For k = 0 To 1
                If MEd(iNode, k) > 0 Then
                    ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANPPlus(iNode), zANE(iNode, k), True, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1, False, False, lWEB, lRec, MyPoutre.Dalle.Ep_td)
                Else
                    ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANPMoins(iNode), zANE(iNode, k), False, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1, False, False, lWEB, lRec, MyPoutre.Dalle.Ep_td)
                End If
            Next
        Next

        '# Controle de la classe des sections

        lClasse3 = False
        lClasse4 = False
        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1
            For k = 0 To 1
                If ClasseSection(iNode, k) = 3 Then lClasse3 = True
                If ClasseSection(iNode, k) = 4 Then lClasse4 = True
            Next
        Next

    End Sub

    Private Sub CalculeClasseSectionsMaillage(MyPoutre As cls_Poutre, MEd(,) As Decimal, zANE(,) As Decimal,
                                              zANP(,) As Decimal, ByRef ClasseSection(,) As Integer,
                                              ByRef lClasse3 As Boolean, ByRef lClasse4 As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   02/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de la calsse des sections le long du maillage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam        [E] :   Poutre à traiter
        '   MEd             [E] :   Diagramme des moments aux ELU
        '   zANP            [E] :   Positions des ANP plastiques sous le moment ELU
        '   zANE            [E] :   Position de l'ANE au droit du noeud
        '   ClasseSection   [S] :   Classe des sections (calculée en fonction du signe de MEd)
        '   lClasse3        [S] :   Indique si au moins une des sections est de classe 3
        '   lClasse4        [S] :   Indique si au moins une des sections est de classe 4
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lGeneration1 As Boolean = MyPoutre.Param.lGeneration1
        Dim kDeb, kFin As Integer
        Const lWEB As Boolean = True
        Dim lRec As Boolean

        '--> Initialisation

        ReDim ClasseSection(MyPoutre.Nodes.nbNodes - 1, 1)

        '--> Boucle sur les noeuds

        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = MyPoutre.Nodes.nbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANP(iNode, k), zANE(iNode, k), True, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1, False, False, lWEB, lrec, MyPoutre.Dalle.Ep_td)

            Next
        Next

        '# Controle de la classe des sections

        lClasse3 = False
        lClasse4 = False
        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1
            For k = 0 To 1
                If ClasseSection(iNode, k) = 3 Then lClasse3 = True
                If ClasseSection(iNode, k) = 4 Then lClasse4 = True
            Next
        Next

    End Sub

#End Region

#Region " Calcul élastique de la connexion "

    Private Sub InitialiseResistanceElastiqueConnexion(myBeam As cls_Poutre, ByRef FluxRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   15/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation de la résistance de la connexion 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam          [E] :   Poutre traitée
        '   FluxRd          [S] :   Résistance de la connexion / u longueur le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iTravee, iDebT, iFinT As Integer
        Dim iNode, k As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iZone As Integer
        Dim sCum, xNode As Decimal
        Dim xFinZone(,) As Decimal = myBeam.xFinZoneTravee
        Dim myFluxRd As Decimal
        Dim xAppui As Decimal

        '--( Initialisation

        ReDim FluxRd(myBeam.Nodes.nbNodes - 1, 1)
        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--( Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)
            iZone = 0
            sCum = xFinZone(iTravee, iZone)
            myFluxRd = myBeam.FluxRdZone(iTravee, iZone)
            xAppui = myBeam.xPositionAppui(True, iTravee)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                xNode = myBeam.Nodes.xGlobal(iNode)

                If IsGreater(xNode, sCum) Then
                    iZone += 1
                    sCum = xFinZone(iTravee, iZone) + xAppui

                    myFluxRd = myBeam.FluxRdZone(iTravee, iZone)

                End If

                For k = iDebK To iFinK
                    FluxRd(iNode, k) = myFluxRd
                Next
            Next
        Next

    End Sub

    Private Sub RunCalculElastiqueConnexion(myBeam As cls_Poutre, iCombi As Integer, FluxELU(,,) As Decimal, FluxRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   15/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance de la connexion 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam          [E] :   Poutre traitée
        '   iCombi          [E] :   Indice de la combinaison
        '   FluxELU         [E] :   Flux de cisaillement dans la connexion le long de la barre
        '   FluxRd          [E] :   Résistance de la connexion / u longueur le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Const iPTZERO As Integer = 0
        Dim myfluxELU, myFluxRd As Decimal

        '--> Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    myfluxELU = FluxELU(iPTZERO, iNode, k)
                    myFluxRd = FluxRd(iNode, k)

                    Me.CritereConnex.EnregistreCritere(iNode, iCombi, iTravee, myfluxELU, myFluxRd)

                Next
            Next
        Next

    End Sub

#End Region

#Region " Calcul des soudures âme semelle "

    Private Sub RunDimensionSouduresAmeSemelle(myBeam As cls_Poutre, iCombi As Integer, FluxELU(,,) As Decimal, ByRef Gorges() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
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
        Dim myEN1993 As New cls_Eurocodes

        '--( Calcul

        For iTrav = iDebTrav To iFinTrav

            iDebNod = myBeam.Nodes.iNodeExtTrav(iTrav, 0)
            iFinNod = myBeam.Nodes.iNodeExtTrav(iTrav, 1)

            For iNode = iDebNod To iFinNod
                If iNode = iDebNod Then kDeb = 1 Else kDeb = 0
                If iNode = iFinNod Then kFin = 0 Else kFin = 1

                For k = kDeb To kFin

                    For iSoud = iDEB To iFIN

                        Gorges(iSoud - 1) = Math.Max(Gorges(iSoud - 1), myEN1993.CalculSoudure(FluxELU(iSoud, iNode, k), GammaM2, BetaW, Fu(iSoud - 1)))

                    Next

                Next

            Next


        Next

    End Sub


#End Region

#Region " Calcul des armatures transversales "

    ''' <summary>
    ''' Calcul la contrainte tangentielle induite par les connecteurs 
    ''' </summary>
    Sub CalculArmaturesTransversales(myBeam As cls_Poutre, iVerif As Integer)
        '------------------------------------------------------------------------------------------------------------------
        '    17/11/23 : Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul la contrainte tangentielle induite par les connecteurs
        '------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iVerif      [E] :   Indice du bloc de vérification 
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration
        Dim nr As Integer
        'Dim PRd As Decimal
        'Dim sx As Decimal
        Dim lGeneration1 As Boolean
        Dim lDallePleine As Boolean
        Dim lPerp As Boolean
        Dim Ecm, Fck, Fcd, nu As Decimal
        Dim fypd, fsd As Decimal
        Dim gammaVs, gammaVc As Decimal
        Dim v_x_Ed As Decimal
        Dim k_sf_aa_sA, k_sf_bb_sA, k_sf_dd_sA As Decimal   'Definition des coefficients lorsque l'on se trouve au droit de l'appui A (voir Figure 5.1 de l'EC4 et §5.1 des specifications techniques)
        Dim k_sf_aa_m, k_sf_bb_m, k_sf_dd_m As Decimal      'Definition des coefficients lorsque l'on se trouve à mi-travee (voir Figure 5.1 de l'EC4 et §5.1 des specifications techniques)
        Dim k_sf_aa_sB, k_sf_bb_sB, k_sf_dd_sB As Decimal   'Definition des coefficients lorsque l'on se trouve au droit de l'appui B (voir Figure 5.1 de l'EC4 et §5.1 des specifications techniques)
        Dim hf_aa, hf_bb, hf_dd As Decimal
        Dim b0, b0min As Decimal
        Dim LargeurParticipante(0, 0) As Decimal
        Dim be1, be2, beta1, beta2, bem, bes As Decimal
        Dim k_bacPE1 As Decimal                             'coefficient qui indique la présence du bac acier (=1) ou non (=0)
        Dim xDebutZoneLoc, xFinZoneLoc As Decimal(,)
        Dim lSupportA, lSupportB, lMiTravee As Boolean      'sera utile pour + tard, permet de savoir si la zone de connection etudiee empiete sur la zone de support A, B ou mi-travee (selon la Figure 5.1 de l'EC4)
        Dim thetaf_min_pos, thetaf_min_neg, thetaf_max As Decimal 'angle min de la bielle de compression en fonction de si on se trouve en zone de flexion positive ou négative 

        '--> Initialisation
        lGeneration1 = myBeam.Param.lGeneration1
        lDallePleine = (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine) Or (myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee)
        lPerp = (myBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)
        Ecm = myBeam.Dalle.beton.Ecm
        Fck = myBeam.Dalle.beton.Fck
        Fcd = myBeam.Dalle.beton.Fck / myBeam.Param.Gamma.GammaC
        fypd = myBeam.Dalle.Bac.fyp / myBeam.Param.Gamma.GammaP
        fsd = myBeam.Dalle.AcierArmatures.FsK / myBeam.Param.Gamma.GammaS
        If myBeam.Param.lGeneration1 Then
            nu = 0.6 * (1 - Fck / 250)
        Else
            nu = 0.5
        End If
        gammaVs = myBeam.Param.Gamma.GammaVs
        gammaVc = myBeam.Param.Gamma.GammaVc
        LargeurParticipante(0, 0) = 0 'initialisation avec une valeur quelconque pour pas que le tableau soit considéré commyPoutre Nothing dans la fonction BeffDalle

        thetaf_min_pos = 27 / 180 * Math.PI 'angle min de la bielle en zone de flexion positive
        thetaf_min_neg = 36 / 180 * Math.PI 'angle min  de la bielle en zone de flexion négative
        thetaf_max = Math.PI / 4

        If myBeam.Dalle.lMixte Then
            b0min = 4 * myBeam.Dalle.Goujons.d
        Else
            b0min = 2.5 * myBeam.Dalle.Goujons.d
        End If

        'Stockage local des abscisses afin de ne pas faire tourner plusieurs fois les calculs des proprietes myBeam.xDebutZone et myBeam.xFinZone
        xDebutZoneLoc = myBeam.xDebutZoneTravee
        xFinZoneLoc = myBeam.xFinZoneTravee

        For i_travee As Integer = myBeam.IndicePremiereTravee To myBeam.IndiceDerniereTravee
            For j_zone As Integer = 0 To myBeam.NombreZones(i_travee) - 1

                '---
                'CALCUL DE LA CONTRAINTE TANGENTIELLE
                '---

                nr = myBeam.NrTransZone(i_travee, j_zone)
                'PRd = myBeam.Dalle.Connecteur.ResistancePRd(lGeneration1, lDallePleine, lPerp, myBeam.Dalle.Bac, nr, Fck, Ecm, gammaVs, gammaVc)
                'sx = myBeam.EspacemyPoutrentZone(i_travee, j_zone)
                'v_x_Ed = nr * PRd / sx
                v_x_Ed = myBeam.FluxRdZone(i_travee, j_zone)

                b0 = (nr - 1) * b0min

                myBeam.BeffDalle(myBeam.LongueurTravee(i_travee) / 2, i_travee, False, False, cls_Poutre.EnuTypeLargeurParticipante.Totale, LargeurParticipante)

                'Calcul de hf qui correspond à la longueur developpe de la surface de ruine 
                hf_aa = myBeam.Dalle.EpaisseurActive
                If nr = 1 Then
                    hf_bb = 2 * myBeam.Dalle.Goujons.hsc + myBeam.Dalle.Goujons.d 'GUD: a confirmyPoutrer avec les corrections apportées dans le MT 
                Else
                    hf_bb = 2 * myBeam.Dalle.Goujons.hsc + b0
                End If
                hf_dd = b0 + 2 * (myBeam.Section.ProfilA.Bfs - b0 + myBeam.Dalle.Goujons.hsc * Math.Tan(myBeam.Dalle.ThetaRd)) / Math.Sqrt(1 + Math.Tan(myBeam.Dalle.ThetaRd) ^ 2)

                Select Case i_travee
                    Case 0 'on est dans le cas de la console gauche

                        'Les consoles sont forcemyPoutrent en flexion négative, on calcul uniquemyPoutrent le coefficient au droit de l'appui A
                        be1 = LargeurParticipante(0, 0)
                        be2 = LargeurParticipante(1, 0)
                        beta1 = LargeurParticipante(2, 0)
                        beta2 = LargeurParticipante(3, 0)
                        bes = LargeurParticipante(5, 0)

                        k_sf_aa_sA = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sA = 1
                        k_sf_dd_sA = 1

                        myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0) = k_sf_aa_sA * v_x_Ed / hf_aa
                        myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1) = k_sf_bb_sA * v_x_Ed / hf_bb
                        myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2) = k_sf_dd_sA * v_x_Ed / hf_dd

                        myBeam.VerifMixte(iVerif).Thetaf_min(i_travee, j_zone) = thetaf_min_neg

                    Case myBeam.IndiceTraveeConsoleDroite 'on est dans le cas de la console droite 

                        'Les consoles sont forcemyPoutrent en flexion négative, on calcul uniquemyPoutrent le coefficient au droit de l'appui B
                        be1 = LargeurParticipante(0, 2)
                        be2 = LargeurParticipante(1, 2)
                        beta1 = LargeurParticipante(2, 2)
                        beta2 = LargeurParticipante(3, 2)
                        bes = LargeurParticipante(5, 2)

                        k_sf_aa_sB = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sB = 1
                        k_sf_dd_sB = 1

                        myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0) = k_sf_aa_sB * v_x_Ed / hf_aa
                        myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1) = k_sf_bb_sB * v_x_Ed / hf_bb
                        myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2) = k_sf_dd_sB * v_x_Ed / hf_dd

                        myBeam.VerifMixte(iVerif).Thetaf_min(i_travee, j_zone) = thetaf_min_neg

                    Case Else 'on est dans le cas d'une travée centrale

                        'Calcul des coefficients k_sf au droit de l'appui A
                        be1 = LargeurParticipante(0, 0)
                        be2 = LargeurParticipante(1, 0)
                        beta1 = LargeurParticipante(2, 0)
                        beta2 = LargeurParticipante(3, 0)
                        bes = LargeurParticipante(5, 0)

                        k_sf_aa_sA = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sA = 1
                        k_sf_dd_sA = 1

                        'Calcul des coefficients k_sf a mi travee
                        be1 = LargeurParticipante(0, 1)
                        be2 = LargeurParticipante(1, 1)
                        bem = LargeurParticipante(5, 1)

                        k_sf_aa_m = Math.Max((be1 - b0 / 2) / bem, (be2 - b0 / 2) / bem)
                        k_sf_bb_m = 1
                        k_sf_dd_m = 1

                        'Calcul des coefficients k_sf au droit de l'appui B
                        be1 = LargeurParticipante(0, 2)
                        be2 = LargeurParticipante(1, 2)
                        beta1 = LargeurParticipante(2, 2)
                        beta2 = LargeurParticipante(3, 2)
                        bes = LargeurParticipante(5, 2)

                        k_sf_aa_sB = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sB = 1
                        k_sf_dd_sB = 1

                        If myBeam.lTraveeConsoleGauche Then
                            If myBeam.lTraveeConsoleDroite Then 'Presence de console a gauche ET a droite
                                Select Case myBeam.xDebutZoneTravee(i_travee, j_zone)
                                    Case <= myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence avant L/4
                                        Select Case myBeam.xFinZoneTravee(i_travee, j_zone)
                                            Case <= myBeam.LongueurTravee(i_travee) / 4 'la zone etudiée commyPoutrence et finie avant L/4
                                                lSupportA = True
                                                lMiTravee = False
                                                lSupportB = False
                                            Case <= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence avant L/4 et finie entre L/4 et 3L/4
                                                lSupportA = True
                                                lMiTravee = True
                                                lSupportB = False
                                            Case >= 3 * myBeam.LongueurTravee(i_travee) / 4 'la eone étudiée commyPoutrence avant L/4 et finie après 3L/4
                                                lSupportA = True
                                                lMiTravee = True
                                                lSupportB = True
                                        End Select
                                    Case <= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence après L/4 et avant 3L/4
                                        Select Case myBeam.xFinZoneTravee(i_travee, j_zone) 'le cas ou la zone finie avant L/4 n a pas de sens et n est pas étudiée 
                                            Case <= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence et finie entre L/4 et 3L/4 
                                                lSupportA = False
                                                lMiTravee = True
                                                lSupportB = False
                                            Case >= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence entre L/4 et 3L/4 et finie après 3L/4
                                                lSupportA = False
                                                lMiTravee = True
                                                lSupportB = True
                                        End Select
                                    Case >= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone finie nécessairemyPoutrent après 3L/4 donc pas besoin de boucle 
                                        lSupportA = False
                                        lMiTravee = False
                                        lSupportB = True
                                End Select

                            Else 'Presence de console a gauche uniquemyPoutrent
                                lSupportB = False 'il n'y a pas de console a droite, ce qui fait qu'il ne peut pas y avoir de zone de momyPoutrent négatif proche de l appui de droite 

                                Select Case myBeam.xDebutZoneTravee(i_travee, j_zone)
                                    Case <= myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence avant L/4
                                        Select Case myBeam.xFinZoneTravee(i_travee, j_zone)
                                            Case <= myBeam.LongueurTravee(i_travee) / 4 'la zone etudiée commyPoutrence et finie avant L/4
                                                lSupportA = True
                                                lMiTravee = False
                                            Case >= myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence avant L/4 et finie après L/4
                                                lSupportA = True
                                                lMiTravee = True
                                        End Select
                                    Case >= myBeam.LongueurTravee(i_travee) / 4 'la zone  commyPoutrence et finie nécessairemyPoutrent après L/4 donc pas besoin de boucle 
                                        lSupportA = False
                                        lMiTravee = True
                                End Select
                            End If
                        Else
                            If myBeam.lTraveeConsoleDroite Then 'Presence de console a droite uniquemyPoutrent
                                lSupportA = False 'il n'y a pas de console a gauche, ce qui fait qu'il ne peut pas y avoir de zone de momyPoutrent négatif proche de l appui de droite 

                                Select Case myBeam.xDebutZoneTravee(i_travee, j_zone)
                                    Case <= 3 * myBeam.LongueurTravee(i_travee) / 4
                                        Select Case myBeam.xFinZoneTravee(i_travee, j_zone)
                                            Case <= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone etudiée commyPoutrence et finie avant 3L/4
                                                lMiTravee = True
                                                lSupportB = False
                                            Case >= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone étudiée commyPoutrence avant 3L/4 et finie après 3L/4
                                                lMiTravee = True
                                                lSupportB = True
                                        End Select
                                    Case >= 3 * myBeam.LongueurTravee(i_travee) / 4 'la zone  commyPoutrence et finie nécessairemyPoutrent après 3L/4 donc pas besoin de boucle 
                                        lMiTravee = False
                                        lSupportB = True
                                End Select
                            Else 'Aucune console, la zone étudiée se trouve nécessairemyPoutrent en zone de flexion positive 
                                lSupportA = False
                                lMiTravee = True
                                lSupportB = False
                            End If
                        End If

                        If lSupportA Then
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0) = k_sf_aa_sA * v_x_Ed / hf_aa
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1) = k_sf_bb_sA * v_x_Ed / hf_bb
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2) = k_sf_dd_sA * v_x_Ed / hf_dd
                        End If

                        If lMiTravee Then
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0) = Math.Max(myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0), k_sf_aa_m * v_x_Ed / hf_aa)
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1) = Math.Max(myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1), k_sf_bb_m * v_x_Ed / hf_bb)
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2) = Math.Max(myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2), k_sf_dd_m * v_x_Ed / hf_dd)
                        End If

                        If lSupportB Then
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0) = Math.Max(myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0), k_sf_aa_sB * v_x_Ed / hf_aa)
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1) = Math.Max(myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1), k_sf_bb_sB * v_x_Ed / hf_bb)
                            myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2) = Math.Max(myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2), k_sf_dd_sB * v_x_Ed / hf_dd)
                        End If

                        If lSupportA Or lSupportB Then 'la zone de connection étudiée traverse au moins une zone de flexion négative
                            myBeam.VerifMixte(iVerif).Thetaf_min(i_travee, j_zone) = thetaf_min_neg
                        Else 'la zone de connection étudiée est entièremyPoutrent en zone de flexion comprimée 
                            myBeam.VerifMixte(iVerif).Thetaf_min(i_travee, j_zone) = thetaf_min_pos
                        End If

                End Select

                '---
                'CALCUL DE L'ANGLE DE LA BIELLE DE COMPRESSION ET DE LA QUANTITE D'ARMATURE PAR UNITE DE LONGUEUR NECESSAIRE
                '---

                Dim TauEd_max As Decimal = nu * Fcd / 2

                For k_ruine As Integer = 0 To 2
                    'Conversion de Pa a MPa des contraintes tangentielles
                    myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, k_ruine) /= kConvMPaPa

                    If myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, k_ruine) <= TauEd_max Then
                        myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine) = 0.5 * Math.Asin(2 * myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, k_ruine) / (nu * Fcd))
                    Else 'la contrainte tangentielle est trop importante, la bielle de compression n'est pas vérifiée. On considère alors l'angle de la bielle max pour la suite du calcul 
                        myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine) = thetaf_max
                    End If

                    myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine) = Math.Min(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine), thetaf_max)
                    myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine) = Math.Max(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine), myBeam.VerifMixte(iVerif).Thetaf_min(i_travee, j_zone))

                    'Calcul du critère de vérification de la bielle de compression
                    myBeam.VerifMixte(iVerif).Gamma_sf(i_travee, j_zone, k_ruine) = myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, k_ruine) / (nu * Fcd * Math.Sin(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine)) * Math.Cos(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, k_ruine)))

                Next

                If myBeam.Dalle.lMixte And myBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire And myBeam.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus Then
                    k_bacPE1 = 1
                Else
                    k_bacPE1 = 0
                End If

                myBeam.VerifMixte(iVerif).As_s_transv(i_travee, j_zone, 0) = Math.Max((myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 0) * hf_aa * Math.Tan(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, 0)) - k_bacPE1 * myBeam.Dalle.Bac.Ape * fypd) / fsd, 0)
                myBeam.VerifMixte(iVerif).As_s_transv(i_travee, j_zone, 1) = Math.Max((myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 1) * hf_bb * Math.Tan(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, 1)) - k_bacPE1 * myBeam.Dalle.Bac.Ape * fypd) / fsd, 0)
                myBeam.VerifMixte(iVerif).As_s_transv(i_travee, j_zone, 2) = Math.Max((myBeam.VerifMixte(iVerif).TauEd(i_travee, j_zone, 2) * hf_dd * Math.Tan(myBeam.VerifMixte(iVerif).Thetaf(i_travee, j_zone, 2)) - k_bacPE1 * myBeam.Dalle.Bac.Ape * fypd) / fsd, 0)


            Next
        Next



    End Sub

#End Region

#Region " Critères de vérification "

    Private Sub RunCritereMoments(MyPoutre As cls_Poutre, ByRef lFirst As Boolean, iCombi As Integer, lPlastique As Boolean, lClasse3 As Boolean,
                                  MEd(,) As Decimal, SigmaELU(,,) As Decimal, MplRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   lFirst      [E] :   Indique si premier appel
        '   iCombi      [E] :   Indice de la combinaison
        '   lClasse3    [E] :   Indique si présence de section de classe 3
        '   lPlastique  [E] :   Indique si contexte de calcul plastique 
        '   MEd         [E] :   Table des moments fléchissants le long de la barre
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   MplRdP      [E] :   Table des moments plastiques > 0 le long de la barre
        '   MplRdM      [E] :   Table des moments plastiques < 0 le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbCombi As Integer = MyPoutre.CombiA_ELU.nbCombi

        '--> Critère de résistance en flexion

        If MyPoutre.Param.lElasticDesignVM Then
            '# Résistance élastique VM imposée
            RunCritereFlexionResistanceElastiqueVM(MyPoutre, iCombi, SigmaELU)
        ElseIf (lClasse3 Or Not lCalculPlastic) Then
            '# Présence d'au moins une section de classe 3,
            '# ou cas d'un calcul élastique imposée par la présence de section de classe 3
            'RunCritereMomentsElastiques(myBeam, iCombi, MEd)
            RunCritereFlexionResistanceElastiqueVM(MyPoutre, iCombi, SigmaELU)
        Else
            '# Résistance plastique possible
            RunCriteresMomentsPlastiques(MyPoutre, iCombi, MEd, MplRd)
        End If
        lFirst = False
    End Sub

    Private Sub RunCritereFlexionResistanceElastiqueVM(myBeam As cls_Poutre, iCombi As Integer, SigmaELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '----------------------------------------------------------------------------------------------------------

        '  Public PsiYspd As Decimal(,)                    ' Coefficient de réduction du plat soudé inférieur (limite d'élasticité, méthode 3)

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal
        Dim FyPl, FydPl As Decimal
        Dim FydPsi, FydPlPsi As Decimal

        Dim Fcd, Fck As Decimal
        Dim Fsk, Fsd As Decimal
        Dim Fecd, Feck As Decimal

        Dim iPro0 As Integer = myBeam.PtsSigma.iProfile(0)
        Dim iDal0 As Integer = myBeam.PtsSigma.iBetonDalle(0)
        Dim iEnrob0 As Integer = myBeam.PtsSigma.iBetonEnrob(0)
        Dim iArmaE0 As Integer = myBeam.PtsSigma.iArmaEnrob(0)
        Dim iArma0 As Integer = myBeam.PtsSigma.iArmaDalle(0)

        'Dim lEnrob As Boolean = myBeam.lEnrobage
        Dim lMixte As Boolean = myBeam.lMixte

        '--> Initialisation

        FySup = myBeam.Section.FySup
        FydSup = FySup / myBeam.Param.Gamma.GammaM0
        FyW = myBeam.Section.FyW
        FydW = FyW / myBeam.Param.Gamma.GammaM0
        FyInf = myBeam.Section.FyInf
        FydInf = FyInf / myBeam.Param.Gamma.GammaM0

        FyPl = myBeam.Section.FySpd
        FydPl = FyPl / myBeam.Param.Gamma.GammaM0

        Fck = myBeam.Dalle.Beton.Fck
        Fcd = Fck / myBeam.Param.Gamma.GammaC

        Feck = myBeam.Section.Enrobage.Beton.Fck
        Fecd = Feck / myBeam.Param.Gamma.GammaC

        Fsk = myBeam.Dalle.AcierArmatures.FsK
        Fsd = Fsk / myBeam.Param.Gamma.GammaS

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then

            Select Case myBeam.Section.TypeSection
                Case cls_Section.Enum_TypeSection.SFBmixte
                    '$$ Fibre supérieure
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
                    '$$ Interface âme/semelle supérieure
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
                    '$$ CdG
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
                    '$$ Interface âme/semelle inférieure
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydW, Me.CritereSigmaA)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydInf, Me.PsiYfi, Me.CritereSigmaA)
                    '$$ Interface semelle inférieure/Plat
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.PsiYfi, Me.CritereSigmaA)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydPl, Me.PsiYspd, Me.CritereSigmaPl)
                    '$$ Fibre inférieure (plat)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 5, SigmaELU, FydPl, Me.PsiYspd, Me.CritereSigmaPl)

                Case cls_Section.Enum_TypeSection.IFB_Amixte

                    '$$ Fibre supérieure (semelle)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
                    '$$ Interface âme/semelle supérieure
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
                    '$$ CdG
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
                    '$$ Interface âme/plat inférieur
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydW, Me.CritereSigmaA)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydPl, Me.PsiYspd, Me.CritereSigmaPl)
                    '$$ Fibre inférieure (plat)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydPl, Me.PsiYspd, Me.CritereSigmaPl)

                Case cls_Section.Enum_TypeSection.IFB_Bmixte

                    '$$ Fibre supérieure (plat)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydPl, Me.CritereSigmaPl)
                    '$$ Interface âme/semelle supérieure
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 1, SigmaELU, FydW, Me.CritereSigmaA)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 1, SigmaELU, FydPl, Me.CritereSigmaPl)
                    '$$ CdG
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
                    '$$ Interface âme/semelle inférieur
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydW, Me.CritereSigmaA)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydInf, Me.PsiYfi, Me.CritereSigmaA)
                    '$$ Fibre inférieure (semelle)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.PsiYfi, Me.CritereSigmaA)

                Case cls_Section.Enum_TypeSection.SABmixte

                    '$$ Fibre supérieure (semelle)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
                    '$$ Interface âme/semelle supérieure
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
                    '$$ CdG
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
                    '$$ Interface âme/semelle inférieur
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydW, Me.CritereSigmaA)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, FydInf, Me.PsiYfi, Me.CritereSigmaA)
                    '$$ Fibre inférieure (semelle)
                    RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.PsiYfi, Me.CritereSigmaA)

            End Select

        End If

        '# Contraintes dans le béton de la dalle

        If lMixte And (iDal0 > -1) Then
            RunCritereFlexionVonM(myBeam, iCombi, iDal0 + 0, SigmaELU, Fcd, Me.CritereSigmaC, -Me.ConvSigneT)
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

        '--< Contraintes dans le béton

        Me.CritereM.EnveloppeCritereCombi(Me.CritereSigmaC, iCombi, iTravDeb, iTravFin)

    End Sub

    Private Sub RunCritereFlexionVonM(myBeam As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal,
                                      SigmaU As Decimal, MyCritereM As cls_Critere, Optional signeS As Decimal = 0)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint      [E] :   Indice du point de calcul des contraintes
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   SigmaU      [E] :   Valeur ultime de la contrainte normale au point iPoint
        '   CritereM    [E] :   Critere de la contrainte de flexion
        '   SigneS      [E] :   Indique le signe de contraintes à prendre en compte
        '                       0 = on applique le critère quel que soit le signe de la contrainte (traction et compression)
        '                       1 = on applique le critère uniquement si contrainte positive
        '                       -1 = on applique le critère uniquement si contrainte négative
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Déclaration

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK
                    If (signeS = 0) _
                    Or ((signeS = 1) And IsGreater(SigmaELU(iPoint, iNode, k), 0)) _
                    Or ((signeS = -1) And IsSmaller(SigmaELU(iPoint, iNode, k), 0)) Then
                        MyCritereM.EnregistreCritere(iNode, iCombi, iTravee, SigmaELU(iPoint, iNode, k), SigmaU)
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereFlexionVonM(myBeam As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal,
                                      SigmaU As Decimal, PsiY(,) As Decimal, MyCritereM As cls_Critere, Optional signeS As Decimal = 0)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint      [E] :   Indice du point de calcul des contraintes
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   SigmaU      [E] :   Valeur ultime de la contrainte normale au point iPoint
        '   PsiY        [E] :   Coefficient de réduction de la limite SigmaU
        '   CritereM    [E] :   Critere de la contrainte de flexion
        '   SigneS      [E] :   Indique le signe de contraintes à prendre en compte
        '                       0 = on applique le critère quel que soit le signe de la contrainte (traction et compression)
        '                       1 = on applique le critère uniquement si contrainte positive
        '                       -1 = on applique le critère uniquement si contrainte négative
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Déclaration

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK
                    If (signeS = 0) _
                    Or ((signeS = 1) And IsGreater(SigmaELU(iPoint, iNode, k), 0)) _
                    Or ((signeS = -1) And IsSmaller(SigmaELU(iPoint, iNode, k), 0)) Then
                        MyCritereM.EnregistreCritere(iNode, iCombi, iTravee, SigmaELU(iPoint, iNode, k), PsiY(iCombi, iNode) * SigmaU)
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereMomentsElastiques(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal)

    End Sub

    Private Sub RunCriteresMomentsPlastiques(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, MplRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant (critère de résistance plastique)
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
                    Me.CritereM.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MplRd(iNode, k))
                Next
            Next
        Next

    End Sub

    Private Sub RunCriteresMomentsPlastiques(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, MplRdP() As Decimal, MplRdM() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant (critère de résistance plastique)
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MplRdP  [E] :   Table des moments plastiques > 0 le long de la barre
        '   MplRdM  [E] :   Table des moments plastiques < 0 le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        'Dim Sigma As Decimal
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Const SIGNEM As Decimal = 1
        Dim MRd As Decimal

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
                    If MEd(iNode, k) * SIGNEM > 0 Then
                        MRd = MplRdP(iNode)
                    Else
                        MRd = MplRdM(iNode)
                    End If
                    Me.CritereM.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRd)
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereCisaillementElastic(myBeam As cls_Poutre, iCombi As Integer, TauELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance aux contraintes de cisaillement 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam  [E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   TauELU  [E] :   Table des contraintes de cisallement le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim TauY, FyW As Decimal

        Dim nbPts As Integer = Me.Tau.MStatic.Count

        '--> Initialisation

        FyW = myBeam.Section.FyW
        TauY = FyW / myBeam.Param.Gamma.GammaM0 / Math.Sqrt(3)

        '--> Calculs

        '# Contraintes de cisaillement dans l'âme du profilé

        If (nbPts > 0) Then
            '( Contrainte face interne de la semelle supérieure
            RunCritereFlexionVonM(myBeam, iCombi, 0, TauELU, TauY, Me.CritereTauA)
            '( Contrainte CdG de la section
            RunCritereFlexionVonM(myBeam, iCombi, 1, TauELU, TauY, Me.CritereTauA)
            '( Contrainte face interne de la semelle inférieure
            RunCritereFlexionVonM(myBeam, iCombi, 2, TauELU, TauY, Me.CritereTauA)
        End If

    End Sub

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


    'Private Sub RunCritereTranchants(myBeam As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VplRd As Decimal)
    '    '----------------------------------------------------------------------------------------------------------
    '    '   10/10/23 :  Création - GUD
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Vérification aux ELU de la résistance à l'effort tranchant 
    '    '----------------------------------------------------------------------------------------------------------
    '    '   myBeam  [E] :   Poutre traitée
    '    '   iCombi  [E] :   Indice de la combinaison
    '    '   VEd     [E] :   Table des efforts tranchants le long de la barre
    '    '   VplRd   [E] :   Table des efforts tranchants résistant plastique le long de la barre
    '    '----------------------------------------------------------------------------------------------------------

    '    '--> Déclaration

    '    Dim iNode, k As Integer
    '    Dim iTravee, iDebT, iFinT As Integer
    '    Dim iDebN, iFinN As Integer
    '    Dim iDebK, iFinK As Integer

    '    '--> Déclaration

    '    iDebT = myBeam.IndicePremiereTravee
    '    iFinT = myBeam.IndiceDerniereTravee

    '    '--> Traitement

    '    For iTravee = iDebT To iFinT
    '        iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
    '        iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

    '        For iNode = iDebN To iFinN
    '            If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
    '            If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

    '            For k = iDebK To iFinK
    '                Me.CritereV.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VplRd)
    '            Next
    '        Next
    '    Next

    'End Sub

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

#Region " Degré de connexion "

    Private Sub InitialiseDegreConnexion(nbCombi As Integer, iTravFin As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des tableaux de degré de connexion
        '----------------------------------------------------------------------------------------------------------
        '   nbCombi     [E] :   Nombre de combinaisons
        '   iTravFin    [E] :   Indice de la dernière travée
        '----------------------------------------------------------------------------------------------------------

        ReDim DegConnex(nbCombi - 1, iTravFin, 1)
        ReDim DegConnexMin(iTravFin)

        'For i As Integer = 0 To iTravFin
        '    DegConnex(i, 0) = -1
        '    DegConnex(i, 1) = -1
        'Next
        For icombi As Integer = 0 To nbCombi - 1
            For i As Integer = 0 To iTravFin
                DegConnex(icombi, i, 0) = -1
                DegConnex(icombi, i, 1) = -1
            Next
        Next
    End Sub

    Private Sub InitialiseAireSemelles(myBeam As cls_Poutre, ByRef AfSup As Decimal, ByRef AfInf As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   10/07/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des aires des semelles supérieures et inférieures du profilé slim floor
        '----------------------------------------------------------------------------------------------------------

        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.IFB_Amixte
                AfSup = myBeam.Section.ProfilA.AireFs
                AfInf = myBeam.Section.ProfilA.AirePlat
            Case cls_Section.Enum_TypeSection.IFB_Bmixte
                AfSup = myBeam.Section.ProfilA.AirePlat
                AfInf = myBeam.Section.ProfilA.AireFi
            Case cls_Section.Enum_TypeSection.SABmixte
                AfSup = myBeam.Section.ProfilA.AireFs
                AfInf = myBeam.Section.ProfilA.AireFi
            Case cls_Section.Enum_TypeSection.SFBmixte
                AfSup = myBeam.Section.ProfilA.AireFs
                AfInf = myBeam.Section.ProfilA.AireFi + myBeam.Section.ProfilA.AirePlat
        End Select

    End Sub

    Private Sub CheckDegreConnexion(MyPoutre As cls_Poutre, DeltaRd() As List(Of Decimal),
                                    iNodeMmax() As Integer, iCombi As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre mixte acier béton - dégré de connexion
        '----------------------------------------------------------------------------------------------------------
        '   DeltaRd     [E] :   Somme des PRd entre les points du maillage et les points de moments nuls
        '   iNodeMMax   [E] :   Indice des neouds de moment >0 max
        '   DegConnex   [E] :   Degré de connexion, par travée, en moment >0 et moment <0
        '   iCombi      [E] :   Indice de la combinaison    
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NArmaDalle, NDalle, NProfile As Decimal
        Dim NConnex As Decimal
        Dim iNode, iNode0 As Integer
        Dim Beff As Decimal
        Dim lSimple As Decimal = MyPoutre.Param.lLargeurEfficaceSimplifiee
        Dim gammaS As Decimal = MyPoutre.Param.Gamma.GammaS
        Dim gammaM0 As Decimal = MyPoutre.Param.Gamma.GammaM0
        Dim gammaC As Decimal = MyPoutre.Param.Gamma.GammaC
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim Fy As Decimal ' = Math.Max(myBeam)
        Dim AfSup, AfInf As Decimal
        Dim Le As Decimal
        Dim myEN1994 As New cls_Eurocodes

        '--> Initialisation

        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee

        NProfile = MyPoutre.Section.ResistanceTractionProfile(gammaM0)

        'AfSup = MyPoutre.Section.ProfilA.AireFs
        'AfInf = MyPoutre.Section.ProfilA.AireFi
        Me.InitialiseAireSemelles(MyPoutre, AfSup, AfInf)
        Fy = Math.Max(MyPoutre.Section.FySup, MyPoutre.Section.FyInf)

        '--> Boucle sur les travées

        '# Travée console gauche (moment négatif)

        If MyPoutre.lTraveeConsoleGauche Then
            iNode = MyPoutre.Nodes.iNodeExtTrav(0, 1)
            Beff = MyPoutre.BeffDalle(MyPoutre.LongueurTravee(0), 0, lSimple, False)
            NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
            NConnex = Math.Min(NArmaDalle, NProfile)
            'DegConnex(0, 1) = DeltaRd(0)(iNode) / NConnex
            EnregistreDegreConnex(DegConnex(iCombi, 0, 1), DeltaRd(0)(iNode) / NConnex)
            DegConnex(iCombi, 0, 0) = -1
        End If

        '# Travée console droite (moment négatif)

        If MyPoutre.lTraveeConsoleDroite Then

            iNode = MyPoutre.Nodes.iNodeExtTrav(iTravFin, 0)
            Beff = MyPoutre.BeffDalle(0, iTravFin, lSimple, False)
            NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
            NConnex = Math.Min(NArmaDalle, NProfile)
            'DegConnex(iTravFin, 1) = DeltaRd(iTravFin)(0) / NConnex
            EnregistreDegreConnex(DegConnex(iCombi, iTravFin, 1), DeltaRd(iTravFin)(0) / NConnex)
            DegConnex(iCombi, iTravFin, 0) = -1
        End If

        '# Boucle sur les travées intermédiaires

        For iTravee = 1 To MyPoutre.NombreTraveesDeuxAppuis
            iNode0 = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)

            '# Appui gauche
            If iTravee > iTravDeb Then
                '# Cas d'un appui gauche avec continuité => On suppose un moment négatif
                Beff = MyPoutre.BeffDalle(0, iTravee, lSimple, False)
                NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
                NConnex = Math.Min(NArmaDalle, NProfile)
                'DegConnex(iTravee, 1) = DeltaRd(iTravee)(0) / NConnex
                EnregistreDegreConnex(DegConnex(iCombi, iTravee, 1), DeltaRd(iTravee)(0) / NConnex)
            End If

            '# En travée

            '---| Degré de connexion en zone de moment positif
            Beff = MyPoutre.BeffDalle(MyPoutre.Nodes.xTravee(iNodeMmax(iTravee)), iTravee, lSimple, False)
            NDalle = MyPoutre.Dalle.NResistanceCompressionDalle(Beff, gammaC)
            NConnex = Math.Min(NDalle, NProfile)
            'DegConnex(iTravee, 0) = DeltaRd(iTravee)(iNodeMmax(iTravee) - iNode0) / NConnex
            EnregistreDegreConnex(DegConnex(iCombi, iTravee, 0), DeltaRd(iTravee)(iNodeMmax(iTravee) - iNode0) / NConnex)

            '---| Degré de connexion mini en zone de moment positif
            Le = MyPoutre.LongueurTravee(iTravee)
            If iTravee > iTravDeb Then Le -= 0.15 * Le
            If iTravee < iTravFin Then Le -= 0.15 * Le
            'DegConnexMin(iTravee) = Me.EtaMinFlanges(Fy, Le, AfSup, AfInf)
            DegConnexMin(iTravee) = myEN1994.EtaMinFlanges(Fy, Le, AfSup, AfInf, True)

            '# Appui droite
            If iTravee < iTravFin Then
                '# Cas d'un appui gauche avec continuité => On suppose un moment négatif
                iNode = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)
                Beff = MyPoutre.BeffDalle(MyPoutre.LongueurTravee(iTravee), iTravee, lSimple, False)
                NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
                NConnex = Math.Min(NArmaDalle, NProfile)
                'If (iTravee > iTravDeb) Then
                '    DegConnex(iTravee, 1) = Math.Min(DegConnex(iTravee, 1), DeltaRd(iTravee)(iNode - iNode0) / NConnex)
                'Else
                '    DegConnex(iTravee, 1) = DeltaRd(iTravee)(iNode - iNode0) / NConnex
                'End If
                EnregistreDegreConnex(DegConnex(iCombi, iTravee, 1), DeltaRd(iTravee)(iNode - iNode0) / NConnex)
            End If

        Next
    End Sub

    Private Sub EnregistreDegreConnex(ByRef ValTable As Decimal, ValCalcul As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Enregistrement d'une valeur de degré de connexion
        '----------------------------------------------------------------------------------------------------------
        '   ValTable        [E/S] : Valeur Tableau où on enregistre
        '   ValCalcul       [E] :   Valeur calculée à traiter
        '----------------------------------------------------------------------------------------------------------

        If ValTable = -1 Then
            ValTable = ValCalcul
        Else
            ValTable = Math.Min(ValTable, ValCalcul)
        End If

    End Sub

    Public Function EtaEnveloppe(nbCombi As Integer, iTravee As Integer) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   22/08/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Extrait le degré de connexion de la poutre le plus défavorable
        '   Uniquement en moment > 0
        '----------------------------------------------------------------------------------------------------------------
        '   nbCombi     [E] :   Nombre de combinaisons ELU traitée
        '   iTravee     [E] :   Indice de la travée
        '----------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Eta As Decimal = Me.DegConnex(0, iTravee, 0)

        '--( Boucle sur les combinaisons

        For iCombi As Integer = 1 To nbCombi - 1
            Eta = Math.Min(Eta, Me.DegConnex(iCombi, iTravee, 0))
        Next

        Return Eta

    End Function

#End Region

#Region " OLDs "

    'Private Function EtaMin() As Decimal
    '    '----------------------------------------------------------------------------------------------------------
    '    '   14/12/23 :  Création - POM
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Valeur minimale du Degré minimal de connexion pour une poute mixte 
    '    '   d'après formules (6.12) et (6.14) de la NF EN 1994-1-1
    '    '----------------------------------------------------------------------------------------------------------
    '    '----------------------------------------------------------------------------------------------------------

    '    Const ETAMINREF As Decimal = 0.4

    '    Return ETAMINREF

    'End Function

    'Public Function EtaMinEqualFlanges(Fy As Decimal, Le As Decimal) As Decimal
    '    '----------------------------------------------------------------------------------------------------------
    '    '   14/12/23 :  Création - POM
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Degré minimal de connexion pour une poute mixte à semelles égales
    '    '   d'après formule (6.12) de la NF EN 1994-1-1
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Fy      [E] :   Limite d'élasticité
    '    '   Le      [E) :   Distance entre points de moments nuls
    '    '----------------------------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim Eta0 As Decimal

    '    '--> Traitement

    '    If IsGreater(Le, 25) Then
    '        Eta0 = 1
    '    Else
    '        Eta0 = Math.Max(EtaMin, (1 - 355 / Fy * (0.75 - 0.03 * Le)))
    '    End If
    '    Return Eta0

    'End Function

    'Public Function EtaMinInEqualFlanges3(Fy As Decimal, Le As Decimal) As Decimal
    '    '----------------------------------------------------------------------------------------------------------
    '    '   14/12/23 :  Création - POM
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Degré minimal de connexion pour une poute mixte à semelles inégales, la semelle inf ayant une aire = 3 x aire semelle sup
    '    '   d'après formule (6.14) de la NF EN 1994-1-1
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Fy      [E] :   Limite d'élasticité
    '    '   Le      [E) :   Distance entre points de moments nuls
    '    '----------------------------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim Eta0 As Decimal

    '    '--> Traitement

    '    If IsGreater(Le, 20) Then
    '        Eta0 = 1
    '    Else
    '        Eta0 = Math.Max(EtaMin, (1 - 355 / Fy * (0.3 - 0.015 * Le)))
    '    End If
    '    Return Eta0

    'End Function

    'Public Function EtaMinFlanges(Fy As Decimal, Le As Decimal, AfSup As Decimal, AfInf As Decimal) As Decimal
    '    '----------------------------------------------------------------------------------------------------------
    '    '   14/12/23 :  Création - POM
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Degré minimal de connexion pour une poute mixte à semelles égales
    '    '   d'après formule (6.14) de la NF EN 1994-1-1
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Fy      [E] :   Limite d'élasticité
    '    '   Le      [E] :   Distance entre points de moments nuls
    '    '   AfSup   [E] :   Aire de la semelle supérieure
    '    '   AfInf   [E] :   Aire de la semelle inférieure
    '    '----------------------------------------------------------------------------------------------------------

    '    '--> Déclaration

    '    Dim RatioAire As Decimal = AfInf / AfSup
    '    Dim Eta As Decimal = -1
    '    Dim EtaEqualF As Decimal
    '    Dim EtaInEqualF As Decimal

    '    '--> Traitement hors domaine application

    '    If IsSmaller(RatioAire, 1) Then
    '        '--< Traitement hors domaine application >
    '        MsgBox("Wrong ratio of flanges areas", MsgBoxStyle.Critical, "cls_VerificationsMixtes/EtaMinFlanges")

    '    ElseIf IsGreater(RatioAire, 3) Then
    '        '--< Traitement ratio semelles > 3 >

    '        ' Dans ce cas, on applique la formule (I.3) de l'annexe I de la NF EN 1994-1-1:2024



    '    Else

    '        '--> Traitement normal

    '        EtaInEqualF = EtaMinInEqualFlanges3(Fy, Le)
    '        EtaEqualF = EtaMinEqualFlanges(Fy, Le)

    '        Eta = EtaEqualF + (EtaInEqualF - EtaEqualF) / 2 * (RatioAire - 1)

    '    End If


    '    Return Eta
    'End Function

#End Region

#Region " Outils "

    Public Function CriteresInitialises() As Boolean
        '----------------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Indique si les critères ont bien été initialisés (et sont donc exploitables)
        '----------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lOK As Boolean = True

        '--> Traitement

        If Me.CritereV Is Nothing Then lOK = False
        If Me.CritereM Is Nothing Then lOK = False
        'If Me.CritereVb Is Nothing Then lOK = False
        'If Me.CritereV Is Nothing Then lOK = False

        Return lOK
    End Function


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

        ReDim Me.PsiAfi(NbCombi - 1, NbNodes - 1)
        ReDim Me.RhoTfi(NbCombi - 1, NbNodes - 1)
        ReDim Me.PsiYfi(NbCombi - 1, NbNodes - 1)

        ReDim Me.PsiAspd(NbCombi - 1, NbNodes - 1)
        ReDim Me.RhoTspd(NbCombi - 1, NbNodes - 1)
        ReDim Me.PsiYspd(NbCombi - 1, NbNodes - 1)

    End Sub

#End Region

End Class
