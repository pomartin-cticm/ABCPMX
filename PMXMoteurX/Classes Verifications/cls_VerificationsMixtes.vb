Public Class cls_VerificationsMixtes

    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES MIXTES
    '=========================================================================================================



#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereVb As cls_Critere                 ' Resistance voilement par cisaillement

#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer)

        Me.CritereM = New cls_Critere(NbNodes)
        Me.CritereV = New cls_Critere(NbNodes)
        Me.CritereVb = New cls_Critere(NbNodes)

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
        Dim VplRd As Decimal                        ' Effort tranchant résistant (a priori constant le long de la poutre)
        Dim VbRd As Decimal                          ' Résistance au voilement par cisaillement (a priori constant le long de la poutre)
        Dim lTwoAdjacentCantilevers As Boolean          ' indique la présence de deux travées adjacentes en consoles (True) ou non
        Dim MplRdPlus() As Decimal = {0}                ' Moments plastiques positifs
        Dim MplRdMoins() As Decimal = {0}               ' Moments plastiques négatifs
        Dim zANPPlus() As Decimal = {0}                 ' Position des ANP sous moment > 0
        Dim zANPMoins() As Decimal = {0}                ' Position des ANP sous moment < 0
        Dim zANEPlus() As Decimal = {0}                 ' Position des ANE sous moment > 0
        Dim zANEMoins() As Decimal = {0}                ' Position des ANE sous moment < 0
        Const lCombiRetrait = False                     '#ALERTE Pour le moment, à pondérer plus tard
        Dim lRElastiqueImpose As Boolean = False        ' Vérification élastique imposée
        Dim lRElastique As Boolean
        Dim ClasseSection(,) As Integer                 ' Tableau dimensions (NbNodes, 0 ou 1 pour moments positifs et négatifs resp.)
        Dim Beff() As Decimal = {0}                     ' Largeurs participantes de la dalle
        Dim lSimple As Boolean = False
        'Dim ClasseP(), ClasseM() As Integer             ' Tableau des classes de section en flexion poisitive et négative
        Dim lGeneration1 As Boolean = (MyPoutre.Param.Norme = MyPoutre.Param.Enu_Normes.EurocodesG1)

        Dim iNodeMmax() As Integer, Mmax() As Decimal
        Dim xMZero(,) As Decimal = Nothing
        Dim lTraveeMomNeg() As Boolean

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

        '# Classes de la section

        ReDim ClasseSection(MyPoutre.Nodes.nbNodes - 1, 1)

        For iNode = 0 To MyPoutre.Nodes.nbNodes - 1
            ' ClasseSection(iNode, 0) = MyPoutre.Section.ClasseSection(zANPPlus(iNode), zANEPlus(iNode), True,lGeneration1, MyPoutre.Dalle.t_d)
            ' ClasseSection(iNode, 1) = MyPoutre.Section.ClasseSection(zANPMoins(iNode), zANEMoins(iNode), False, lGeneration1, MyPoutre.Dalle.t_d)
        Next

        '--> Boucle sur les combinaisons

        For iCombi = 0 To MyPoutre.CombiA_ELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            MyPoutre.CombiA_ELU.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, MEd, lCombiRetrait)

            '# Combinaison des efforts tranchants

            MyPoutre.CombiA_ELU.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, VEd, False)

            '# Controle de la classe des sections


            '# Analyse du diagramme de moment

            MyPoutre.AnalyseDiagrammeMoments(MEd, iNodeMmax, Mmax, xMZero, lTraveeMomNeg)

            '# Degré de connexion




            '# Vérification sous moment fléchissant

            Me.CriteresMomentsPlastiques(MyPoutre, iCombi, MEd, MplRdPlus, MplRdMoins)

            '# Vérification sous effort tranchant

            Me.CritereTranchants(MyPoutre, iCombi, VEd, VplRd)

            '# Vérification au voilement par cisaillement


            '# Vérification sous interaction MV



        Next


    End Sub

    Private Sub CriteresMomentsPlastiques(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, MplRdP() As Decimal, MplRdM() As Decimal)
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

    Private Sub CritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VplRd As Decimal)
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

    Private Sub CritereVoilementCisaillement(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VbRd As Decimal)
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

            Me.CritereVb.EnregistreCritere(iNode, iCombi, VEdMax, VbRd)

        Next

    End Sub

#End Region

End Class
