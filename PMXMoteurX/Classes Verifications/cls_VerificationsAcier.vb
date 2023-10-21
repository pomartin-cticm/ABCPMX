Public Class cls_VerificationsAcier


    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES ACIER
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
        '   Vérification aux ELU d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre vérifiée
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim VplRd As Decimal
        Dim iCombi As Integer
        Dim MEd(,), VEd(,) As Decimal
        Dim MplRd, zANP As Decimal
        Dim MelRd, zANE As Decimal
        Dim lGeneration1 As Boolean = (MyPoutre.Param.Norme = MyPoutre.Param.Enu_Normes.EurocodesG1)
        Dim ClasseP, ClasseM As Integer 'Classes de la section en flexion positive et négative
        Dim lClasse4 As Boolean
        Dim lSigma As Boolean

        '--> Initialisations

        '# Critères

        Me.InitialiseCriteres(MyPoutre.Nodes.nbNodes)

        '# Tranchant résistant

        VplRd = MyPoutre.Section.VplRd(MyPoutre.Param.Gamma.GammaM0)

        '# Propriétés

        MyPoutre.ProprietesVerifAcier(True, MplRd, zANP, MelRd, zANE)

        '# Classes de la section

        ClasseP = MyPoutre.Section.ClasseSection(zANP, zANE, True, lGeneration1)
        ClasseM = MyPoutre.Section.ClasseSection(zANP, zANE, False, lGeneration1)

        '# Contraintes

        lSigma = MyPoutre.Param.lElasticDesign Or (ClasseP > 2) Or (ClasseM > 2)
        lSigma = True       ' EN phase debug
        If lSigma Then
            MyPoutre.PtsSigma.Initialise(MyPoutre)

        End If


        '--> Boucle sur les combinaisons

        For iCombi = 0 To MyPoutre.CombiA_ELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            MyPoutre.CombiA_ELU.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, MEd, False)

            '# Combinaison des efforts tranchants

            MyPoutre.CombiA_ELU.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, VEd, False)

            '# Vérification sous moment fléchissant

            If MyPoutre.Param.lElasticDesign Then

            Else
                Me.CritereFlexionAcier(MyPoutre, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lclasse4)
            End If

            ' Me.CriteresMomentsPlastiques(MyPoutre, iCombi, MEd, MplRdPlus, MplRdMoins)

        Next

    End Sub

#End Region

#Region " Vérifications d'une poutre acier sans enrobage "

    Private Sub CritereFlexionAcier(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal,
                                    MplRd As Decimal, MelRd As Decimal, ClasseP As Integer, ClasseM As Integer, ByRef lClasse4 As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   MyPoutre[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MplRd   [E] :   Moment résitant plastique
        '   MelRd   [E] :   Moment élastique
        '   ClasseP [E] :   Classe de la section en flexion positive
        '   ClasseM [E] :   Classe de la section en flexion négative
        '   lClasse4[S] :   Indique qu'au moins une des sections est de classe 4
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        'Dim Critere As Decimal
        Dim nbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iNode As Integer
        Const SIGNEM As Decimal = 1
        Dim MRd As Decimal
        Dim MEdMax As Decimal
        Dim lOk As Boolean

        '--> Initialisation

        lClasse4 = False

        '--> Boucle sur les noeuds

        For iNode = 0 To nbNodes - 1

            If Math.Abs(MEd(iNode, 0)) > Math.Abs(MEd(iNode, 1)) Then
                MEdMax = MEd(iNode, 0)
            Else
                MEdMax = MEd(iNode, 1)
            End If

            lOk = True
            If MEdMax * SIGNEM > 0 Then

                Select Case ClasseP
                    Case 1, 2
                        MRd = MplRd
                    Case 3
                        MRd = MelRd
                    Case 4
                        lClasse4 = True
                        lOk = False
                End Select

            Else

                Select Case ClasseM
                    Case 1, 2
                        MRd = MplRd
                    Case 3
                        MRd = MelRd
                    Case 4
                        lClasse4 = True
                        lOk = False
                End Select

            End If

            Me.CritereM.EnregistreCritere(iNode, iCombi, MEdMax, MRd)

        Next
    End Sub

#End Region



End Class
