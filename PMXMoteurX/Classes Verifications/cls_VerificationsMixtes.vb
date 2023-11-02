Public Class cls_VerificationsMixtes

    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES MIXTES
    '=========================================================================================================



#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereVb As cls_Critere                 ' Resistance voilement par cisaillement

    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereSigmaC As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton de la dalle
    Public CritereSigmaArmaC As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures de la dalle
    Public CritereSigmaE As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le béton d'enrobage
    Public CritereSigmaArmaE As cls_Critere         ' Critère de résistance en flexion  / Contrainte normale dans les armatures d'enrobage

#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer)

        Me.CritereM = New cls_Critere(NbNodes)
        Me.CritereV = New cls_Critere(NbNodes)
        Me.CritereVb = New cls_Critere(NbNodes)

    End Sub

    Private Sub InitialiseCriteresVM(NbNodes As Integer, lArma As Boolean)
        '-------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '-------------------------------------------------------------------
        '   Initialisation des critères pour les contraintes normales
        '-------------------------------------------------------------------

        Me.CritereSigmaA = New cls_Critere(NbNodes)
        Me.CritereSigmaC = New cls_Critere(NbNodes)
        Me.CritereSigmaE = New cls_Critere(NbNodes)
        If lArma Then
            Me.CritereSigmaArmaC = New cls_Critere(NbNodes)
            Me.CritereSigmaArmaE = New cls_Critere(NbNodes)
        End If

    End Sub

#End Region

#Region " Outils de vérification "

    Public Sub VerificationELU(MyPoutre As cls_Poutre)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre mixte acier béton
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre vérifiée
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iCombi As Integer
        Dim MEd(,), VEd(,) As Decimal
        Dim VplRd As Decimal                            ' Effort tranchant résistant (a priori constant le long de la poutre)
        Dim VbRd As Decimal                             ' Résistance au voilement par cisaillement (a priori constant le long de la poutre)
        Dim lTwoAdjacentCantilevers As Boolean          ' indique la présence de deux travées adjacentes en consoles (True) ou non
        Dim MplRdPlus() As Decimal = {0}                ' Moments plastiques positifs
        Dim MplRdMoins() As Decimal = {0}               ' Moments plastiques négatifs
        Dim zANPPlus() As Decimal = {0}                 ' Position des ANP sous moment > 0
        Dim zANPMoins() As Decimal = {0}                ' Position des ANP sous moment < 0
        Dim zANE(,) As Decimal = Nothing                ' Position des ANE sous moment 
        Const lCombiRetrait = False                     '#ALERTE Pour le moment, à pondérer plus tard
        Dim lRElastiqueImpose As Boolean = False        ' Vérification élastique imposée
        'Dim lRElastique As Boolean
        Dim ClasseSection(,) As Integer = Nothing       ' Tableau dimensions (NbNodes, 0 ou 1 pour gauche ou droite)
        Dim Beff() As Decimal = {0}                     ' Largeurs participantes de la dalle
        Dim lSimple As Boolean = False
        'Dim ClasseP(), ClasseM() As Integer             ' Tableau des classes de section en flexion poisitive et négative
        Dim lGeneration1 As Boolean = MyPoutre.Param.lGeneration1

        Dim iNodeMmax() As Integer, Mmax() As Decimal
        Dim xMZero(,) As Decimal = Nothing
        Dim lTraveeMomNeg() As Boolean = Nothing

        Const lRetraitElastique As Boolean = True
        Dim SigmaP(,,,) As Decimal = Nothing        ' Contraintes normales dans l'hypothèse d'un moment positif
        Dim SigmaM(,,,) As Decimal = Nothing        ' Contraintes normales dans l'hypothèse d'un moment négatif
        Dim SigmaELU(,,) As Decimal = Nothing       ' Contraintes normales sous 1 combinaison ELU

        Dim lClasse3, lClasse4 As Boolean           ' Indique si présence d'au moins une section de classe 3 ou de classe 4
        Dim DeltaRd() As List(Of Decimal) = Nothing
        Dim DegConnex(,) As Decimal = Nothing

        Dim zANP(,) As Decimal = Nothing                ' Position ANP, tenant compte de MEd et du degré de connexion
        Dim MplRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de MEd et du degré de connexion

        '--> Initialisations

        '# Critères

        Me.InitialiseCriteres(MyPoutre.Nodes.nbNodes)

        '# Largeurs participantes

        MyPoutre.MaillageBeff(lSimple, False, Beff)

        '# Tranchant résistant

        VplRd = MyPoutre.Section.VplRd(MyPoutre.Param.Gamma.GammaM0)

        '# Résistance au voilement par cisaillement

        lTwoAdjacentCantilevers = MyPoutre.lTraveeConsoleGauche And MyPoutre.lTraveeConsoleDroite

        VbRd = MyPoutre.Section.VbRd(MyPoutre.Param.Gamma.GammaM1, MyPoutre.Param.EtaW, lTwoAdjacentCantilevers)

        '# Moments plastiques

        MyPoutre.MaillagePropPlastiquesMixtes(Beff, 1, True, MplRdPlus, zANPPlus)
        MyPoutre.MaillagePropPlastiquesMixtes(Beff, 1, True, MplRdMoins, zANPMoins)

        '# Propriétés élastiques

        '# Calcul des contraintes élastiques pour les cas de charges

        MyPoutre.PtsSigma.Initialise(MyPoutre)
        MyPoutre.PtsSigma.CalculContraintesCharges(MyPoutre, 1, SigmaP)
        MyPoutre.PtsSigma.CalculContraintesCharges(MyPoutre, -1, SigmaM)

        '--> Boucle sur les combinaisons

        For iCombi = 0 To MyPoutre.CombiA_ELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            MyPoutre.CombiA_ELU.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, MEd, lCombiRetrait)

            '# Combinaison des efforts tranchants

            MyPoutre.CombiA_ELU.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, VEd, False)

            '# Combinaison des contraintes élastiques

            MyPoutre.CombiA_ELU.CombineContraintes(iCombi, MyPoutre.ChargesA.Count, MyPoutre.PtsSigma.zPos.Count, MyPoutre.Nodes.nbNodes,
                                                   MyPoutre.ChargesA, MEd, SigmaP, SigmaM, lretraitElastique, sigmaelu)

            '# Position de l'ANE en fonction des contraintes dans le profilé

            MyPoutre.RechercheANEFromSigma(SigmaELU, MEd, MyPoutre.Nodes.nbNodes, zane)

            '# Analyse du diagramme de moment

            MyPoutre.AnalyseDiagrammeMoments(MEd, iNodeMmax, Mmax, xMZero, lTraveeMomNeg)

            '# Calcul des propriétés plastiques le long de la barre

            MyPoutre.MaillageRConnexion(xMZero, DeltaRd)
            Me.MaillageProprietesPlastiques(MyPoutre, MEd, DeltaRd, Beff, zANP, mplrd)

            '# Classes des sections

            'Me.CalculeClasseSectionsMaillage(MyPoutre, MEd, zANE, zANPPlus, zANPMoins, ClasseSection, lClasse3, lClasse4)
            Me.CalculeClasseSectionsMaillage(MyPoutre, MEd, zANE, zANP, ClasseSection, lClasse3, lClasse4)

            '# Degré de connexion

            If Not (lClasse3 Or lClasse4) Then
                Me.CheckDegreConnexion(MyPoutre, DeltaRd, iNodeMmax, DegConnex)
            End If

            '# Vérification sous moment fléchissant

            Me.RunCritereMoments(MyPoutre, iCombi, lClasse3, MEd, SigmaELU, MplRdPlus, MplRdMoins)

            '# Vérification sous effort tranchant

            Me.RunCritereTranchants(MyPoutre, iCombi, VEd, VplRd)

            '# Vérification au voilement par cisaillement


            '# Vérification sous interaction MV


            '# 

        Next


    End Sub

    Private Sub MaillageProprietesPlastiques(MyPoutre As cls_Poutre, MEd(,) As Decimal, DeltaRd() As List(Of Decimal), bEff() As Decimal,
                                             ByRef pzANP(,) As Decimal, ByRef pMPlRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   02/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastique le long de la barre en fonction de 
        '   du moment sollicitant et du degré de connection
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '   MEd             [E] :   Diagramme de moment aux ELU
        '   DeltaRd         [E] :   Cumul des résistance des PRd entre les sections et les points de moment nul
        '   bEff            [E] :   Largeur efficace de dalle
        '   pzANP           [S] :   position ANP
        '   pMplRd          [S] :   moment plastique (en fonction du signe de MEd)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim iNode As Integer
        Dim iNode0 As Integer
        Dim kDeb, kfin, k As Integer
        Const RhoV As Decimal = 1

        '--> Initialisation

        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee
        ReDim pzANP(NbNodes - 1, 1)
        ReDim pMPlRd(NbNodes - 1, 1)

        '--> Traitement

        For iTravee = iTravDeb To iTravFin

            iNode0 = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)

            For iNode = iNode0 To MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)
                If iNode = iNode0 Then kDeb = 1 Else kDeb = 0
                If iNode = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1) Then kfin = 0 Else kfin = 1

                MyPoutre.Section.ProprietesPlastiquesMixteMyyEta(Math.Sign(MEd(iNode, kDeb)), True, MyPoutre.Param.Gamma, RhoV,
                                                                 bEff(iNode), DeltaRd(iTravee)(iNode0 + iNode), MyPoutre.Dalle, pzANP(iNode, kDeb), pMPlRd(iNode, kDeb))

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
        '   MyPoutre        [E] :   Poutre à traiter
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
                    ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANPPlus(iNode), zANE(iNode, k), True, lGeneration1, MyPoutre.Dalle.t_d)
                Else
                    ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANPMoins(iNode), zANE(iNode, k), False, lGeneration1, MyPoutre.Dalle.t_d)
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
        '   MyPoutre        [E] :   Poutre à traiter
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

                ClasseSection(iNode, k) = MyPoutre.Section.ClasseSection(zANP(iNode, k), zANE(iNode, k), True, lGeneration1, MyPoutre.Dalle.t_d)

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


    Private Sub RunCritereMoments(MyPoutre As cls_Poutre, iCombi As Integer, lClasse3 As Boolean,
                                  MEd(,) As Decimal, SigmaELU(,,) As Decimal, MplRdP() As Decimal, MplRdM() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant 
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   lClasse3[E] :   Indique si présence de section de classe 3
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   SigmaELU[E] :   Contraintes normales aux ELU
        '   MplRdP  [E] :   Table des moments plastiques > 0 le long de la barre
        '   MplRdM  [E] :   Table des moments plastiques < 0 le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Critère de résistance en flexion

        If MyPoutre.Param.lElasticDesign Then
            '# Résistance élastique VM imposée
            Me.InitialiseCriteresVM(MyPoutre.Nodes.nbNodes, MyPoutre.lEnrobage)
            RunCritereFlexionResistanceElastiqueVM(MyPoutre, iCombi, SigmaELU)
        ElseIf lClasse3 Then
            '# Présence d'au moins une section de classe 3
            RunCritereMomentsElastiques(MyPoutre, iCombi, MEd)
        Else
            '# Résistance plastique possible
            RunCriteresMomentsPlastiques(MyPoutre, iCombi, MEd, MplRdP, MplRdM)
        End If
    End Sub

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

        Dim Fcd, Fck As Decimal
        Dim Fecd, Feck As Decimal

        Dim iPro0 As Integer = MyPoutre.PtsSigma.iProfile(0)
        Dim iDal0 As Integer = MyPoutre.PtsSigma.iBetonDalle(0)
        Dim iEnrob0 As Integer = MyPoutre.PtsSigma.iBetonEnrob(0)

        '--> Initialisation

        FySup = MyPoutre.Section.FySup
        FydSup = FySup / MyPoutre.Param.Gamma.GammaM0
        FyW = MyPoutre.Section.FyW
        FydW = FyW / MyPoutre.Param.Gamma.GammaM0
        FyInf = MyPoutre.Section.FyInf
        FydInf = FyInf / MyPoutre.Param.Gamma.GammaM0

        Fck = MyPoutre.Dalle.beton.Fck
        Fcd = Fck / MyPoutre.Param.Gamma.GammaC

        Feck = MyPoutre.Section.Enrobage.Beton.Fck
        Fecd = Feck / MyPoutre.Param.Gamma.GammaC

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydInf, FydW), Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.CritereSigmaA)
        End If

        '# Contraintes dans le béton d'enrobage

        If MyPoutre.lEnrobage And (iEnrob0 > -1) Then
            RunCritereFlexionVM(MyPoutre, iCombi, iDal0 + 0, SigmaELU, Fecd, Me.CritereSigmaC)
            RunCritereFlexionVM(MyPoutre, iCombi, iDal0 + 1, SigmaELU, Fecd, Me.CritereSigmaC)
        End If

        '# Contraintes dans les armatures d'enrobage

        If MyPoutre.lEnrobage Then

        End If

        '# Contraintes dans le béton de la dalle

        If MyPoutre.lMixte And (iDal0 > -1) Then
            RunCritereFlexionVM(MyPoutre, iCombi, iDal0 + 0, SigmaELU, Fcd, Me.CritereSigmaE)
        End If

        '# Contraintes dans les armatures de la dalle

        If MyPoutre.lMixte And MyPoutre.NbTravees > 1 Then

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

        Dim iNode As Integer
        Dim Sigma As Decimal

        '--> Traitement

        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1

            If Math.Abs(SigmaELU(iPoint, iNode, 0)) > Math.Abs(SigmaELU(iPoint, iNode, 1)) Then
                Sigma = SigmaELU(iPoint, iNode, 0)
            Else
                Sigma = SigmaELU(iPoint, iNode, 1)
            End If

            MyCritereM.EnregistreCritere(iNode, iCombi, Sigma, SigmaU)

        Next


    End Sub

    Private Sub RunCritereMomentsElastiques(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal)

    End Sub

    Private Sub RunCriteresMomentsPlastiques(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, MplRdP() As Decimal, MplRdM() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant (critère de résistance plastique)
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MplRdP  [E] :   Table des moments plastiques > 0 le long de la barre
        '   MplRdM  [E] :   Table des moments plastiques < 0 le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Critere As Decimal
        Dim nbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iNode As Integer
        Const SIGNEM As Decimal = 1
        Dim MRd As Decimal
        Dim MEdMax As Decimal

        '--> Boucle sur les noeuds

        For iNode = 0 To nbNodes - 1

            If Math.Abs(MEd(iNode, 0)) > Math.Abs(MEd(iNode, 1)) Then
                MEdMax = MEd(iNode, 0)
            Else
                MEdMax = MEd(iNode, 1)
            End If

            If MEdMax * SIGNEM > 0 Then
                MRd = MplRdP(iNode)
            Else
                MRd = MplRdM(iNode)
            End If

            Me.CritereM.EnregistreCritere(iNode, iCombi, MEdMax, MRd)

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

        '--> Déclarations

        Dim Critere As Decimal
        Dim nbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iNode As Integer
        Dim VEdMax As Decimal

        '--> Boucle sur les noeuds

        For iNode = 0 To nbNodes - 1

            If Math.Abs(VEd(iNode, 0)) > Math.Abs(VEd(iNode, 1)) Then
                VEdMax = VEd(iNode, 0)
            Else
                VEdMax = VEd(iNode, 1)
            End If

            Me.CritereV.EnregistreCritere(iNode, iCombi, VEdMax, VplRd)

        Next

    End Sub

    Private Sub RunCritereVoilementCisaillement(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VbRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   VEd     [E] :   Table des efforts tranchants le long de la barre
        '   VRd     [E] :   Table des résistances au voilement par cisaillement le long de la barre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        'Dim Critere As Decimal
        Dim nbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iNode As Integer
        Dim VEdMax As Decimal

        '--> Boucle sur les noeuds

        For iNode = 0 To nbNodes - 1

            If Math.Abs(VEd(iNode, 0)) > Math.Abs(VEd(iNode, 1)) Then
                VEdMax = VEd(iNode, 0)
            Else
                VEdMax = VEd(iNode, 1)
            End If

            Me.CritereVb.EnregistreCritere(iNode, iCombi, VEdMax, VbRd)

        Next

    End Sub

#End Region

#Region " Degré de connexion "

    Private Sub CheckDegreConnexion(MyPoutre As cls_Poutre, DeltaRd() As List(Of Decimal), iNodeMmax() As Integer, ByRef DegConnex(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre mixte acier béton
        '----------------------------------------------------------------------------------------------------------
        '   DeltaRd     [E] :   Somme des PRd entre les points du maillage et les points de moments nuls
        '   iNodeMMax   [E] :   Indice des neouds de moment >0 max
        '   DegConnex   [E] :   Degré de connexion, par travée, en moment >0 et moment <0
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NArma, NDalle, NProfile As Decimal
        Dim NConnex As Decimal
        Dim iNode, iNode0 As Integer
        Dim Beff As Decimal
        Dim lSimple As Decimal = MyPoutre.Param.lLargeurEfficaceSimplifiee
        Dim gammaS As Decimal = MyPoutre.Param.Gamma.GammaS
        Dim gammaM0 As Decimal = MyPoutre.Param.Gamma.GammaM0
        Dim gammaC As Decimal = MyPoutre.Param.Gamma.GammaC
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer

        '--> Initialisation

        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee
        ReDim DegConnex(iTravFin, 1)

        '##ZZZ A compléter dans le cas des profilés enrobés
        NProfile = MyPoutre.Section.ResistanceTractionProfile(gammaM0)

        '--> Boucle sur les travées

        '# Travée console gauche

        If MyPoutre.lTraveeConsoleGauche Then
            iNode = MyPoutre.Nodes.iNodeExtTrav(0, 1)
            Beff = MyPoutre.BeffDalle(MyPoutre.LongueurTravee(0), 0, lSimple, False)
            NArma = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
            NConnex = Math.Min(NArma, NProfile)
            DegConnex(0, 1) = DeltaRd(0)(iNode) / NConnex
            DegConnex(0, 0) = -1
        End If

        '# Travée console droite

        If MyPoutre.lTraveeConsoleDroite Then

            iNode = MyPoutre.Nodes.iNodeExtTrav(iTravFin, 0)
            Beff = MyPoutre.BeffDalle(0, iTravFin, lSimple, False)
            NArma = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
            NConnex = Math.Min(NArma, NProfile)
            DegConnex(iTravFin, 1) = DeltaRd(iTravFin)(0) / NConnex
            DegConnex(iTravFin, 0) = -1
        End If

        '# Boucle sur les travées intermédiaires

        For iTravee = 1 To MyPoutre.NombreTraveesDeuxAppuis
            iNode0 = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)

            '# Appui gauche
            If iTravee > iTravDeb Then
                '# Cas d'un appui gauche avec continuité => On suppose un moment négatif
                Beff = MyPoutre.BeffDalle(0, iTravee, lSimple, False)
                NArma = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
                NConnex = Math.Min(NArma, NProfile)
                DegConnex(iTravee, 1) = DeltaRd(iTravee)(0) / NConnex
            End If

            '# En travée
            Beff = MyPoutre.BeffDalle(MyPoutre.Nodes.xTravee(iNodeMmax(iTravee)), iTravee, lSimple, False)
            NDalle = MyPoutre.Dalle.NResistanceCompressionDalle(Beff, gammaC)
            NConnex = Math.Min(NDalle, NProfile)
            DegConnex(iTravee, 0) = DeltaRd(iTravee)(iNodeMmax(iTravee) - iNode0) / NConnex

            '# Appui droite
            If iTravee < iTravFin Then
                '# Cas d'un appui gauche avec continuité => On suppose un moment négatif
                iNode = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)
                Beff = MyPoutre.BeffDalle(MyPoutre.LongueurTravee(iTravee), iTravee, lSimple, False)
                NArma = MyPoutre.Dalle.NResistanceArmatures(Beff, gammaS)
                NConnex = Math.Min(NArma, NProfile)
                If (iTravee > iTravDeb) Then
                    DegConnex(iTravee, 1) = Math.Min(DegConnex(iTravee, 1), DeltaRd(iTravee)(iNode - iNode0) / NConnex)
                Else
                    DegConnex(iTravee, 1) = DeltaRd(iTravee)(iNode - iNode0) / NConnex
                End If
            End If

        Next
    End Sub

#End Region


End Class
