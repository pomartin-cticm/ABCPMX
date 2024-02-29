Public Class cls_VerificationsMixtes

    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES MIXTES
    '=========================================================================================================

#Region " Attributs "

    Private ConvSigneT As Decimal                   ' Convention de signe pour les contraintes de traction

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereMV As cls_Critere                 ' Résistance à l'interacion MV
    Public CritereVb As cls_Critere                 ' Resistance voilement par cisaillement

    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereSigmaC As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton de la dalle
    Public CritereSigmaArmaC As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures de la dalle
    Public CritereSigmaE As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton d'enrobage
    Public CritereSigmaArmaE As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures d'enrobage
    Public CritereTauA As cls_Critere               ' Critère de contrainte de cisaillement élastique
    Public CritereSigmaVM As cls_Critere            ' Critère de contrainte élastique équivalente de Von Mises

    Public RhoV As Decimal(,)                       ' Coefficient d'interaction : 1er indice: indice de la combinaison, 2eme indice: indice du noeud

    Public lCalculPlastic As Boolean                ' Indique si le dimensionnement est suivant la théorie plastique

    Public DegConnex(,) As Decimal = Nothing        ' Degré de connexion : 1er indice : travée, 2eme indice : 0 pour M>0 et 1 pour M<0
    Public DegConnexMin() As Decimal = Nothing      ' Degré minimal de connexion en moment positif (indice de la travée)

    Public ShearB As strucShearBuckling             ' Paramètres du voilement par cisaillement

    '==( Classe pour le calcul des contraintes de cisaillement en calcul élastique imposé

    Dim Tau As cls_Tau

#End Region

#Region " Constructeurs "

    Public Sub New()
        lCalculPlastic = False
        Me.ConvSigneT = cls_PointsSigma.CONVSIGNETRACTION
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, nbCombi As Integer, IndDerniereT As Integer)

        Me.CritereM = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereV = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereVb = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereMV = New cls_Critere(NbNodes, nbCombi, IndDerniereT)

    End Sub

    Private Sub InitialiseCriteresVM(NbNodes As Integer, lEnrob As Boolean, nbCombi As Integer, IndDerniereT As Integer)
        '-------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '-------------------------------------------------------------------
        '   Initialisation des critères pour les contraintes normales
        '-------------------------------------------------------------------

        Me.CritereSigmaA = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereTauA = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaVM = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaC = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        Me.CritereSigmaArmaC = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        If lEnrob Then
            Me.CritereSigmaE = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
            Me.CritereSigmaArmaE = New cls_Critere(NbNodes, nbCombi, IndDerniereT)
        End If

    End Sub

#End Region

#Region " Gestion globale de la vérification "

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
        Dim VplRd As Decimal                            ' Effort tranchant résistant (a priori constant le long de la poutre)
        Dim VbRd As Decimal                             ' Résistance au voilement par cisaillement (a priori constant le long de la poutre)
        Dim lTwoAdjacentCantilevers As Boolean          ' indique la présence de deux travées adjacentes en consoles (True) ou non
        Dim MplRdPlus() As Decimal = {0}                ' Moments plastiques positifs
        Dim MplRdMoins() As Decimal = {0}               ' Moments plastiques négatifs
        Dim zANPPlus() As Decimal = {0}                 ' Position des ANP sous moment > 0
        Dim zANPMoins() As Decimal = {0}                ' Position des ANP sous moment < 0
        Dim zANE(,) As Decimal = Nothing                ' Position des ANE sous moment 
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

        Dim SigmaP(,,,) As Decimal = Nothing        ' Contraintes normales dans l'hypothèse d'un moment positif
        Dim SigmaM(,,,) As Decimal = Nothing        ' Contraintes normales dans l'hypothèse d'un moment négatif
        Dim SigmaELU(,,) As Decimal = Nothing       ' Contraintes normales sous 1 combinaison ELU
        Dim TauELU(,,) As Decimal = Nothing             ' Contraintes de cisaillement sous 1 combinaison ELU
        Dim TauCas(,,,) As Decimal = Nothing            ' Contraintes de cisaillement pour les cas de charges
        Dim lRetraitElastique As Boolean = True

        Dim lClasse3, lClasse4 As Boolean           ' Indique si présence d'au moins une section de classe 3 ou de classe 4
        Dim DeltaRd() As List(Of Decimal) = Nothing

        Dim zANP(,) As Decimal = Nothing                ' Position ANP, tenant compte de MEd et du degré de connexion
        Dim zANPMV(,) As Decimal = Nothing                ' Position ANP, tenant compte de MEd, du degré de connexion et de l'interaction avec l'effort tranchant 
        Dim MplRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de MEd et du degré de connexion
        Dim MVRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de MEd, du degré de connexion et de l'interaction avec l'effort tranchant 

        Dim EpsilonW As Decimal
        Dim lEnrob As Boolean
        Dim lCont As Boolean
        Dim lCombiClass3 As Boolean                 ' Indique s'il existe au moins une combinaison avec classe 3
        Dim lCombiClass4 As Boolean                 ' Indique s'il existe au moins une combinaison avec classe 4
        Dim lFirst As Boolean = True

        '--> Initialisations

        lCombiClass3 = False
        lCombiClass4 = False

        '# Degré de connexion

        Me.InitialiseDegreConnexion(myBeam.IndiceDerniereTravee)

        '# Critères

        Me.InitialiseCriteres(myBeam.Nodes.nbNodes, cls_Poutre.nbCombELU, myBeam.IndiceDerniereTravee)
        Me.InitialiseRhoV(cls_Poutre.nbCombELU, myBeam.Nodes.nbNodes)

        '# Largeurs participantes

        myBeam.MaillageBeff(lSimple, False, Beff)

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

        '# Moments plastiques 

        myBeam.MaillagePropPlastiquesMixtes(Beff, 1, True, MplRdPlus, zANPPlus)
        myBeam.MaillagePropPlastiquesMixtes(Beff, -1, True, MplRdMoins, zANPMoins)

        '# Propriétés élastiques

        '# Calcul des contraintes normales élastiques pour les cas de charges

        myBeam.PtsSigma.Initialise(myBeam)
        myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaP)
        myBeam.PtsSigma.CalculContraintesCharges(myBeam, -1, SigmaM)

        '# Calcul des contraintes de cisaillement pour un calcul élastique
        If myBeam.Param.lElasticDesignVM Then
            Me.Tau = New cls_Tau(myBeam.Section.typeSection)
            Me.Tau.Initialise(myBeam.Section.ProfilA)
            Me.Tau.CalculContraintesCharges(myBeam, TauCas)
        End If

        '--> Boucle sur les combinaisons

        lCont = True
        Me.lCalculPlastic = (Not myBeam.Param.lElasticDesignVM) And (Not myBeam.Param.lElasticDesignCl3)

        Do While lCont

            lFirst = True
            For iCombi = 0 To myBeam.CombiA_ELU.nbCombi - 1

                '# Combinaisons des moments, efforts tranchants

                myBeam.CombiA_ELU.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, Not Me.lCalculPlastic)

                '# Combinaison des efforts tranchants

                myBeam.CombiA_ELU.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, Not Me.lCalculPlastic)

                '# Combinaison des contraintes normales élastiques

                ' On prend en compte le retrait dans le cas d'un calcul élastique
                myBeam.CombiA_ELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, myBeam.PtsSigma.zPos.Count, myBeam.Nodes.nbNodes,
                                                     myBeam.ChargesA, MEd, SigmaP, SigmaM, lRetraitElastique, SigmaELU)
                myBeam.PtsSigma.AjusteContraintes(myBeam, SigmaELU)

                '# Combinaison des contraintes de cisaillement élastiques

                If myBeam.Param.lElasticDesignVM Then
                    myBeam.CombiA_ELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, Me.Tau.MStatic.Count, myBeam.Nodes.nbNodes,
                                                         myBeam.ChargesA, TauCas, lRetraitElastique, TauELU)
                End If

                '# Position de l'ANE en fonction des contraintes dans le profilé

                myBeam.RechercheANEFromSigma(SigmaELU, MEd, myBeam.Nodes.nbNodes, zANE)

                '# Analyse du diagramme de moment

                myBeam.AnalyseDiagrammeMoments(MEd, iNodeMmax, Mmax, xMZero, lTraveeMomNeg)

                '# Calcul des propriétés plastiques le long de la barre,
                ' avec prise en compte de la connection,
                ' sans prise en compte de la réduction induit par l'effort tranchant 

                myBeam.MaillageRConnexion(xMZero, DeltaRd)

                Me.MaillageProprietesPlastiques(iCombi, myBeam, MEd, DeltaRd, Beff, zANP, MplRd)

                '# Classes des sections

                'Me.CalculeClasseSectionsMaillage(myBeam, MEd, zANE, zANPPlus, zANPMoins, ClasseSection, lClasse3, lClasse4)
                Me.CalculeClasseSectionsMaillage(myBeam, MEd, zANE, zANP, ClasseSection, lClasse3, lClasse4)
                lCombiClass3 = lCombiClass3 And lClasse3
                lCombiClass4 = lCombiClass4 And lClasse4

                '# Degré de connexion

                If Me.lCalculPlastic And (Not (lClasse3 Or lClasse4)) Then
                    Me.CheckDegreConnexion(myBeam, DeltaRd, iNodeMmax)
                End If

                '# Vérification sous moment fléchissant

                Me.RunCritereMoments(myBeam, lFirst, iCombi, Me.lCalculPlastic, lClasse3, MEd, SigmaELU, MplRd)

                '# Vérification sous effort tranchant

                Me.RunCritereTranchants(myBeam, iCombi, VEd, VplRd)

                '# Vérification au voilement par cisaillement

                'If myBeam.Section.IsVoilementParCisaillement(myBeam.Param.EtaW) Then Me.RunCritereVoilementCisaillement(myBeam, iCombi, VEd, VbRd)
                If Me.ShearB.lCheckRequired Then Me.RunCritereVoilementCisaillement(myBeam, iCombi, VEd, VbRd)

                '# Calcul du critère d'intéraction rhoV

                Me.CalculRhoV(iCombi, myBeam)

                '# Calcul des propriétés plastiques le long de la barre,
                ' avec prise en compte de la connection,
                ' sans prise en compte de la réduction induit par l'effort tranchant 

                Me.MaillageProprietesPlastiques(iCombi, myBeam, MEd, DeltaRd, Beff, zANPMV, MVRd, Me.RhoV)

                '# Vérification sous interaction MV

                Me.RunCriteresInteractionMV(myBeam, iCombi, MEd, MVRd)

            Next

            '# Doit on refaire un boucle sur les combinaisons

            If Me.lCalculPlastic Then
                If lCombiClass3 Then
                    lCont = True
                    Me.lCalculPlastic = False
                Else
                    lCont = False
                End If
            Else
                lCont = False
            End If

        Loop

    End Sub

    Private Sub MaillageProprietesPlastiques(iCombi As Integer, MyPoutre As cls_Poutre, MEd(,) As Decimal, DeltaRd() As List(Of Decimal), bEff() As Decimal,
                                             ByRef pzANP(,) As Decimal, ByRef pMPlRd(,) As Decimal, Optional rhoV As Decimal(,) = Nothing)
        '----------------------------------------------------------------------------------------------------------
        '   02/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastique le long de la barre en fonction de 
        '   du moment sollicitant et du degré de connection
        '----------------------------------------------------------------------------------------------------------
        '   myBeam        [E] :   Poutre traitée
        '   MEd             [E] :   Diagramme de moment aux ELU
        '   DeltaRd         [E] :   Cumul des résistance des PRd entre les sections et les points de moment nul
        '   bEff            [E] :   Largeur efficace de dalle
        '   RhoV            [E] :   Coefficient pour l'interaction MV
        '   pzANP           [S] :   position ANP
        '   pMplRd          [S] :   moment plastique (en fonction du signe de MEd)
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

        '--> Initialisation

        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee
        ReDim pzANP(NbNodes - 1, 1)
        ReDim pMPlRd(NbNodes - 1, 1)

        '--> Traitement

        For iTravee = iTravDeb To iTravFin

            iNodeDeb = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeFin = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

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
                MyPoutre.Section.ProprietesPlastiquesMixteMyyEta(Signe, True, MyPoutre.Param.Gamma, rhoVLoc,
                                                                 bEff(iNode), DeltaRd(iTravee)(iNode - iNodeDeb), MyPoutre.Dalle, pzANP(iNode, kDeb), pMPlRd(iNode, kDeb))

                If kfin > kDeb Then
                    pzANP(iNode, kfin) = pzANP(iNode, kDeb)
                    pMPlRd(iNode, kfin) = pMPlRd(iNode, kDeb)
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

        '--> Initialisation

        ReDim ClasseSection(MyPoutre.Nodes.nbNodes - 1, 1)

        '--> Boucle sur les noeuds

        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1
            For k = 0 To 1
                If MEd(iNode, k) > 0 Then
                    ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANPPlus(iNode), zANE(iNode, k), True, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1, MyPoutre.Dalle.t_d)
                Else
                    ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANPMoins(iNode), zANE(iNode, k), False, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1, MyPoutre.Dalle.t_d)
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

        '--> Initialisation

        ReDim ClasseSection(MyPoutre.Nodes.nbNodes - 1, 1)

        '--> Boucle sur les noeuds

        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = MyPoutre.Nodes.nbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANP(iNode, k), zANE(iNode, k), True, MyPoutre.Section.lSlimFloor, MyPoutre.Section.lEnrobage, lGeneration1, MyPoutre.Dalle.t_d)

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

#Region " Critères de vérification "

    'Private Sub RunCritereMoments(myBeam As cls_Poutre, iCombi As Integer, lClasse3 As Boolean,
    '                              MEd(,) As Decimal, SigmaELU(,,) As Decimal, MplRdP() As Decimal, MplRdM() As Decimal)
    '    '----------------------------------------------------------------------------------------------------------
    '    '   25/10/23 :  Création - POM
    '    '----------------------------------------------------------------------------------------------------------
    '    '   Vérification aux ELU de la résistance au moment fléchissant 
    '    '----------------------------------------------------------------------------------------------------------
    '    '   myBeam[E] :   Poutre traitée
    '    '   iCombi  [E] :   Indice de la combinaison
    '    '   lClasse3[E] :   Indique si présence de section de classe 3
    '    '   MEd     [E] :   Table des moments fléchissants le long de la barre
    '    '   SigmaELU[E] :   Contraintes normales aux ELU
    '    '   MplRdP  [E] :   Table des moments plastiques > 0 le long de la barre
    '    '   MplRdM  [E] :   Table des moments plastiques < 0 le long de la barre
    '    '----------------------------------------------------------------------------------------------------------

    '    '--> Critère de résistance en flexion

    '    If myBeam.Param.lElasticDesignVM Then
    '        '# Résistance élastique VM imposée
    '        Me.InitialiseCriteresVM(myBeam.Nodes.nbNodes, myBeam.lEnrobage, cls_Poutre.nbCombELU, myBeam.IndiceDerniereTravee)
    '        RunCritereFlexionResistanceElastiqueVM(myBeam, iCombi, SigmaELU)
    '    ElseIf lClasse3 Then
    '        '# Présence d'au moins une section de classe 3
    '        RunCritereMomentsElastiques(myBeam, iCombi, MEd)
    '    Else
    '        '# Résistance plastique possible
    '        RunCriteresMomentsPlastiques(myBeam, iCombi, MEd, MplRdP, MplRdM)
    '    End If
    'End Sub

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

        '--> Critère de résistance en flexion

        If MyPoutre.Param.lElasticDesignVM Then
            '# Résistance élastique VM imposée
            If lFirst Then Me.InitialiseCriteresVM(MyPoutre.Nodes.nbNodes, MyPoutre.lEnrobage, cls_Poutre.nbCombELU, MyPoutre.IndiceDerniereTravee)
            RunCritereFlexionResistanceElastiqueVM(MyPoutre, iCombi, SigmaELU)
        ElseIf (lClasse3 Or Not lCalculPlastic) Then
            '# Présence d'au moins une section de classe 3,
            '# ou cas d'un calcul élastique imposée par la présence de section de classe 3
            'RunCritereMomentsElastiques(myBeam, iCombi, MEd)
            If lFirst Then Me.InitialiseCriteresVM(MyPoutre.Nodes.nbNodes, MyPoutre.lEnrobage, cls_Poutre.nbCombELU, MyPoutre.IndiceDerniereTravee)
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

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal

        Dim Fcd, Fck As Decimal
        Dim Fsk, Fsd As Decimal
        Dim Fecd, Feck As Decimal

        Dim iPro0 As Integer = myBeam.PtsSigma.iProfile(0)
        Dim iDal0 As Integer = myBeam.PtsSigma.iBetonDalle(0)
        Dim iEnrob0 As Integer = myBeam.PtsSigma.iBetonEnrob(0)
        Dim iArma0 As Integer = myBeam.PtsSigma.iArmaDalle(0)

        Dim lEnrob As Boolean = myBeam.lEnrobage
        Dim lMixte As Boolean = myBeam.lMixte

        '--> Initialisation

        FySup = myBeam.Section.FySup
        FydSup = FySup / myBeam.Param.Gamma.GammaM0
        FyW = myBeam.Section.FyW
        FydW = FyW / myBeam.Param.Gamma.GammaM0
        FyInf = myBeam.Section.FyInf
        FydInf = FyInf / myBeam.Param.Gamma.GammaM0

        Fck = myBeam.Dalle.beton.Fck
        Fcd = Fck / myBeam.Param.Gamma.GammaC

        Feck = myBeam.Section.Enrobage.Beton.Fck
        Fecd = Feck / myBeam.Param.Gamma.GammaC

        Fsk = myBeam.Dalle.AcierArmatures.FsK
        Fsd = Fsk / myBeam.Param.Gamma.GammaS

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then
            RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
            RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
            RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
            RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydInf, FydW), Me.CritereSigmaA)
            RunCritereFlexionVonM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.CritereSigmaA)
        End If

        '# Contraintes dans le béton d'enrobage

        If lEnrob And (iEnrob0 > -1) Then
            RunCritereFlexionVonM(myBeam, iCombi, iDal0 + 0, SigmaELU, Fecd, Me.CritereSigmaC, -Me.ConvSigneT)
            RunCritereFlexionVonM(myBeam, iCombi, iDal0 + 1, SigmaELU, Fecd, Me.CritereSigmaC, -Me.ConvSigneT)
        End If

        '# Contraintes dans les armatures d'enrobage

        If lEnrob Then

        End If

        '# Contraintes dans le béton de la dalle

        If lMixte And (iDal0 > -1) Then
            RunCritereFlexionVonM(myBeam, iCombi, iDal0 + 0, SigmaELU, Fcd, Me.CritereSigmaC, -Me.ConvSigneT)
        End If

        '# Contraintes dans les armatures de la dalle

        If lMixte And (myBeam.NbTravees > 1) And (myBeam.PtsSigma.iArmaDalle(1) > -1) Then
            Dim mySigneS As Decimal
            If myBeam.Param.lCompressionArma Then mySigneS = 0 Else mySigneS = Me.ConvSigneT

            For iArma = myBeam.PtsSigma.iArmaDalle(0) To myBeam.PtsSigma.iArmaDalle(1)
                RunCritereFlexionVonM(myBeam, iCombi, iArma, SigmaELU, Fsd, Me.CritereSigmaArmaC, mySigneS)
            Next

        End If

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

    Private Sub RunCritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VplRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
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

    Private Sub InitialiseDegreConnexion(iTravFin As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des tableaux de degré de connexion
        '----------------------------------------------------------------------------------------------------------
        '   iTravFin    [E] :   Indice de la dernière travée
        '----------------------------------------------------------------------------------------------------------


        ReDim DegConnex(iTravFin, 1)
        ReDim DegConnexMin(iTravFin)

        For i As Integer = 0 To iTravFin
            DegConnex(i, 0) = -1
            DegConnex(i, 1) = -1
        Next

    End Sub

    Private Sub CheckDegreConnexion(MyPoutre As cls_Poutre, DeltaRd() As List(Of Decimal), iNodeMmax() As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre mixte acier béton - dégré de connexion
        '----------------------------------------------------------------------------------------------------------
        '   DeltaRd     [E] :   Somme des PRd entre les points du maillage et les points de moments nuls
        '   iNodeMMax   [E] :   Indice des neouds de moment >0 max
        '   DegConnex   [E] :   Degré de connexion, par travée, en moment >0 et moment <0
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NArmaDalle, NDalle, NProfile, NEnrobage, NArmaEnrobage As Decimal
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

        '--> Initialisation

        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee

        '##ZZZ A compléter dans le cas des profilés enrobés
        NProfile = MyPoutre.Section.ResistanceTractionProfile(gammaM0)
        If MyPoutre.Section.lEnrobage Then
            NEnrobage = MyPoutre.Section.NResistanceCompressionEnrobage(gammaC)
            NArmaEnrobage = MyPoutre.Section.NResistanceArmaturesEnrobage(gammaS)
        End If

        AfSup = MyPoutre.Section.ProfilA.AireFs
        AfInf = MyPoutre.Section.ProfilA.AireFi
        Fy = Math.Max(MyPoutre.Section.FySup, MyPoutre.Section.FyInf)

        '--> Boucle sur les travées

        '# Travée console gauche (moment négatif)

        If MyPoutre.lTraveeConsoleGauche Then
            iNode = MyPoutre.Nodes.iNodeExtTrav(0, 1)
            Beff = MyPoutre.BeffDalle(MyPoutre.LongueurTravee(0), 0, lSimple, False)
            NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
            NConnex = Math.Min(NArmaDalle, NProfile + NEnrobage)
            'DegConnex(0, 1) = DeltaRd(0)(iNode) / NConnex
            EnregistreDegreConnex(DegConnex(0, 1), DeltaRd(0)(iNode) / NConnex)
            DegConnex(0, 0) = -1
        End If

        '# Travée console droite (moment négatif)

        If MyPoutre.lTraveeConsoleDroite Then

            iNode = MyPoutre.Nodes.iNodeExtTrav(iTravFin, 0)
            Beff = MyPoutre.BeffDalle(0, iTravFin, lSimple, False)
            NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
            NConnex = Math.Min(NArmaDalle, NProfile + NEnrobage)
            'DegConnex(iTravFin, 1) = DeltaRd(iTravFin)(0) / NConnex
            EnregistreDegreConnex(DegConnex(iTravFin, 1), DeltaRd(iTravFin)(0) / NConnex)
            DegConnex(iTravFin, 0) = -1
        End If

        '# Boucle sur les travées intermédiaires

        For iTravee = 1 To MyPoutre.NombreTraveesDeuxAppuis
            iNode0 = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)

            '# Appui gauche
            If iTravee > iTravDeb Then
                '# Cas d'un appui gauche avec continuité => On suppose un moment négatif
                Beff = MyPoutre.BeffDalle(0, iTravee, lSimple, False)
                NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
                NConnex = Math.Min(NArmaDalle, NProfile + NEnrobage)
                'DegConnex(iTravee, 1) = DeltaRd(iTravee)(0) / NConnex
                EnregistreDegreConnex(DegConnex(iTravee, 1), DeltaRd(iTravee)(0) / NConnex)
            End If

            '# En travée

            '---| Degré de connexion en zone de moment positif
            Beff = MyPoutre.BeffDalle(MyPoutre.Nodes.xTravee(iNodeMmax(iTravee)), iTravee, lSimple, False)
            NDalle = MyPoutre.Dalle.NResistanceCompressionDalle(Beff, gammaC)
            NConnex = Math.Min(NDalle, NProfile + NArmaEnrobage)
            'DegConnex(iTravee, 0) = DeltaRd(iTravee)(iNodeMmax(iTravee) - iNode0) / NConnex
            EnregistreDegreConnex(DegConnex(iTravee, 0), DeltaRd(iTravee)(iNodeMmax(iTravee) - iNode0) / NConnex)

            '---| Degré de connexion mini en zone de moment positif
            Le = MyPoutre.LongueurTravee(iTravee)
            If iTravee > iTravDeb Then Le -= 0.15 * Le
            If iTravee < iTravFin Then Le -= 0.15 * Le
            DegConnexMin(iTravee) = Me.EtaMinFlanges(Fy, Le, AfSup, AfInf)

            '# Appui droite
            If iTravee < iTravFin Then
                '# Cas d'un appui gauche avec continuité => On suppose un moment négatif
                iNode = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)
                Beff = MyPoutre.BeffDalle(MyPoutre.LongueurTravee(iTravee), iTravee, lSimple, False)
                NArmaDalle = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
                NConnex = Math.Min(NArmaDalle, NProfile + NEnrobage)
                'If (iTravee > iTravDeb) Then
                '    DegConnex(iTravee, 1) = Math.Min(DegConnex(iTravee, 1), DeltaRd(iTravee)(iNode - iNode0) / NConnex)
                'Else
                '    DegConnex(iTravee, 1) = DeltaRd(iTravee)(iNode - iNode0) / NConnex
                'End If
                EnregistreDegreConnex(DegConnex(iTravee, 1), DeltaRd(iTravee)(iNode - iNode0) / NConnex)
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

    Private Function EtaMin() As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Valeur minimale du Degré minimal de connexion pour une poute mixte 
        '   d'après formules (6.12) et (6.14) de la NF EN 1994-1-1
        '----------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------

        Const ETAMINREF As Decimal = 0.4

        Return ETAMINREF

    End Function

    Public Function EtaMinEqualFlanges(Fy As Decimal, Le As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Degré minimal de connexion pour une poute mixte à semelles égales
        '   d'après formule (6.12) de la NF EN 1994-1-1
        '----------------------------------------------------------------------------------------------------------
        '   Fy      [E] :   Limite d'élasticité
        '   Le      [E) :   Distance entre points de moments nuls
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Eta0 As Decimal

        '--> Traitement

        If IsGreater(Le, 25) Then
            Eta0 = 1
        Else
            Eta0 = Math.Max(EtaMin, (1 - 355 / Fy * (0.75 - 0.03 * Le)))
        End If
        Return Eta0

    End Function

    Public Function EtaMinInEqualFlanges3(Fy As Decimal, Le As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Degré minimal de connexion pour une poute mixte à semelles inégales, la semelle inf ayant une aire = 3 x aire semelle sup
        '   d'après formule (6.14) de la NF EN 1994-1-1
        '----------------------------------------------------------------------------------------------------------
        '   Fy      [E] :   Limite d'élasticité
        '   Le      [E) :   Distance entre points de moments nuls
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Eta0 As Decimal

        '--> Traitement

        If IsGreater(Le, 20) Then
            Eta0 = 1
        Else
            Eta0 = Math.Max(EtaMin, (1 - 355 / Fy * (0.3 - 0.015 * Le)))
        End If
        Return Eta0

    End Function

    Public Function EtaMinFlanges(Fy As Decimal, Le As Decimal, AfSup As Decimal, AfInf As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Degré minimal de connexion pour une poute mixte à semelles égales
        '   d'après formule (6.14) de la NF EN 1994-1-1
        '----------------------------------------------------------------------------------------------------------
        '   Fy      [E] :   Limite d'élasticité
        '   Le      [E] :   Distance entre points de moments nuls
        '   AfSup   [E] :   Aire de la semelle supérieure
        '   AfInf   [E] :   Aire de la semelle inférieure
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim RatioAire As Decimal = AfInf / AfSup
        Dim Eta As Decimal = -1
        Dim EtaEqualF As Decimal
        Dim EtaInEqualF As Decimal

        '--> Traitement hors domaine application

        If IsGreater(RatioAire, 3) Or IsSmaller(RatioAire, 1) Then
            MsgBox("Wrong ratio of flanges areas", MsgBoxStyle.Critical, "cls_VerificationsMixtes/EtaMinFlanges")
            Return Eta
        End If

        '--> Traitement normal

        EtaInEqualF = EtaMinInEqualFlanges3(Fy, Le)
        EtaEqualF = EtaMinEqualFlanges(Fy, Le)

        Eta = EtaEqualF + (EtaInEqualF - EtaEqualF) / 2 * (RatioAire - 1)
        Return Eta
    End Function

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



End Class
