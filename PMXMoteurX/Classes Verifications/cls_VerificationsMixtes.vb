Public Class cls_VerificationsMixtes

    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES MIXTES
    '=========================================================================================================



#Region " Attributs "

    Public CritereM As cls_Critere
    Public CritereV As cls_Critere

#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer)

        Me.CritereM = New cls_Critere(NbNodes)
        Me.CritereV = New cls_Critere(NbNodes)

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
        Dim MplRdPlus() As Decimal = {0}                ' Moments plastiques positifs
        Dim MplRdMoins() As Decimal = {0}               ' Moments plastiques négatifs
        Dim zANPPlus() As Decimal = {0}                 ' Position des ANP sous moment > 0
        Dim zANPMoins() As Decimal = {0}                ' Position des ANP sous moment < 0
        Const lCombiRetrait = False                     '#ALERTE Pour le moment, à pondérer plus tard
        Dim lRElastiqueImpose As Boolean = False        ' Vérification élastique imposée
        Dim lRElastique As Boolean
        Dim ClasseSection(,) As Integer                 ' Tableau dimensions (NbNodes, 0 ou 1 pour moments positifs et négatifs resp.)
        Dim Beff() As Decimal = {0}                     ' Largeurs participantes de la dalle
        Dim lSimple As Boolean = False

        '--> Initialisations

        '# Critères

        Me.InitialiseCriteres(MyPoutre.Nodes.nbNodes)

        '# Largeurs participantes

        MyPoutre.MaillageBeff(lSimple, False, Beff)

        '# Moments plastiques

        MyPoutre.MaillagePropPlastiquesMixtes(Beff, 1, True, MplRdPlus, zANPPlus)
        MyPoutre.MaillagePropPlastiquesMixtes(Beff, 1, True, MplRdMoins, zANPMoins)

        '# Propriétés élastiques

        '# Classes de la section


        '--> Boucle sur les combinaisons

        For iCombi = 0 To MyPoutre.CombiA_ELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            MyPoutre.CombiA_ELU.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, MEd, lCombiRetrait)

            '# Combinaison des efforts tranchants

            MyPoutre.CombiA_ELU.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, VEd, False)

            '# Vérification sous moment fléchissant

            '# Vérification sous effort tranchant

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

        For iNode = 0 To nbNodes

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

    Private Sub CritereTranchants()

    End Sub

#End Region

End Class
