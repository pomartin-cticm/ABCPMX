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
        Dim lRElastique As Boolean
        Dim ClasseSection(,) As Integer                 ' Tableau dimensions (NbNodes, 0 ou 1 pour gauche ou droite)
        Dim Beff() As Decimal = {0}                     ' Largeurs participantes de la dalle
        Dim lSimple As Boolean = False
        'Dim ClasseP(), ClasseM() As Integer             ' Tableau des classes de section en flexion poisitive et négative
        Dim lGeneration1 As Boolean = (MyPoutre.Param.Norme = MyPoutre.Param.Enu_Normes.EurocodesG1)

        Dim iNodeMmax() As Integer, Mmax() As Decimal
        Dim xMZero(,) As Decimal = Nothing
        Dim lTraveeMomNeg() As Boolean

        Const lRetraitElastique As Boolean = True
        Dim SigmaP(,,,) As Decimal = Nothing        ' Contraintes normales dans l'hypothèse d'un moment positif
        Dim SigmaM(,,,) As Decimal = Nothing        ' Contraintes normales dans l'hypothèse d'un moment négatif
        Dim SigmaELU(,,) As Decimal = Nothing       ' Contraintes normales sous 1 combinaison ELU

        Dim lClasse3, lClasse4 As Boolean           ' Indique si présence d'au moins une section de classe 3 ou de classe 4

        '--> Initialisations

        ReDim ClasseSection(MyPoutre.Nodes.nbNodes - 1, 1)

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

            '# Classes des sections

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

            '# Analyse du diagramme de moment

            MyPoutre.AnalyseDiagrammeMoments(MEd, iNodeMmax, Mmax, xMZero, lTraveeMomNeg)

            '# Degré de connexion




            '# Vérification sous moment fléchissant

            Me.RunCritereMoments(MyPoutre, iCombi, lClasse3, MEd, SigmaELU, MplRdPlus, MplRdMoins)
            'Me.RunCriteresMomentsPlastiques(MyPoutre, iCombi, MEd, MplRdPlus, MplRdMoins)

            '# Vérification sous effort tranchant

            Me.RunCritereTranchants(MyPoutre, iCombi, VEd, VplRd)

            '# Vérification au voilement par cisaillement


            '# Vérification sous interaction MV


            '# 

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

End Class
