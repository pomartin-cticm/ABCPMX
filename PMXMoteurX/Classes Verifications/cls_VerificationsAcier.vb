Public Class cls_VerificationsAcier


    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES ACIER
    '=========================================================================================================




#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereVb As cls_Critere                 ' Resistance voilement par cisaillement
    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé

#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer, lElastic As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '   lElastic    [E] :   Cas d'un dimensionnement élastique VM
        '----------------------------------------------------------------------------------------------------------

        If lElastic Then
            Me.CritereSigmaA = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Else
            Me.CritereM = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        End If
        Me.CritereV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereVb = New cls_Critere(NbNodes, NbCombi, IndDerniereT)

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
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim MplRd, zANP As Decimal
        Dim MelRd, zANE As Decimal
        Dim lGeneration1 As Boolean = MyPoutre.Param.lGeneration1
        Dim ClasseP, ClasseM As Integer 'Classes de la section en flexion positive et négative
        Dim lClasse4 As Boolean
        Dim lSigma As Boolean
        Dim SigmaELU(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELU
        Dim SigmaCas(,,,) As Decimal = Nothing          ' Contraintes normales pour les cas de charges
        Dim lRetraitElastique As Boolean = True

        '--> Initialisations

        '# Critères

        Me.InitialiseCriteres(MyPoutre.Nodes.nbNodes, cls_Poutre.nbCombELU, MyPoutre.IndiceDerniereTravee, MyPoutre.Param.lElasticDesign)

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
            MyPoutre.PtsSigma.CalculContraintesCharges(MyPoutre, 1, SigmaCas)
        End If

        '--> Boucle sur les combinaisons

        For iCombi = 0 To MyPoutre.CombiA_ELU.nbCombi - 1

            '# Combinaisons des moments, efforts tranchants

            MyPoutre.CombiA_ELU.CombineMoments(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, MEd, False)

            '# Combinaison des efforts tranchants

            MyPoutre.CombiA_ELU.CombineEffortsT(iCombi, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, VEd, False)

            '# Combinaisons des contraintes

            If lSigma Then
                MyPoutre.CombiA_ELU.CombineContraintes(iCombi, MyPoutre.ChargesA.Count, MyPoutre.PtsSigma.zPos.Count, MyPoutre.Nodes.nbNodes,
                                                       MyPoutre.ChargesA, SigmaCas, lRetraitElastique, SigmaELU)
            End If

            '# Vérification sous moment fléchissant

            If MyPoutre.Param.lElasticDesign Then
                RunCritereFlexionResistanceElastiqueVM(MyPoutre, iCombi, SigmaELU)
            Else
                Me.RunCritereFlexionAcier(MyPoutre, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lClasse4)
            End If

            ' Me.CriteresMomentsPlastiques(MyPoutre, iCombi, MEd, MplRdPlus, MplRdMoins)

        Next

    End Sub

#End Region

#Region " Vérifications d'une poutre acier sans enrobage "


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

        Dim iPro0 As Integer = MyPoutre.PtsSigma.iProfile(0)

        '--> Initialisation

        FySup = MyPoutre.Section.FySup
        FydSup = FySup / MyPoutre.Param.Gamma.GammaM0
        FyW = MyPoutre.Section.FyW
        FydW = FyW / MyPoutre.Param.Gamma.GammaM0
        FyInf = MyPoutre.Section.FyInf
        FydInf = FyInf / MyPoutre.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydInf, FydW), Me.CritereSigmaA)
            RunCritereFlexionVM(MyPoutre, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.CritereSigmaA)
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

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            If iTravee = iDebT Then iDebK = 1 Else iDebK = 0
            If iTravee = iFinT Then iFinK = 0 Else iFinK = 1
            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                For k = iDebK To iFinK
                    MyCritereM.EnregistreCritere(iNode, iCombi, iTravee, SigmaELU(iPoint, iNode, k), SigmaU)
                Next
            Next
        Next

        ''--> Déclaration

        'Dim iNode As Integer
        'Dim Sigma As Decimal

        ''--> Traitement

        'For iNode = 0 To MyPoutre.Nodes.nbNodes - 1

        '    If Math.Abs(SigmaELU(iPoint, iNode, 0)) > Math.Abs(SigmaELU(iPoint, iNode, 1)) Then
        '        Sigma = SigmaELU(iPoint, iNode, 0)
        '    Else
        '        Sigma = SigmaELU(iPoint, iNode, 1)
        '    End If

        '    MyCritereM.EnregistreCritere(iNode, iCombi, Sigma, SigmaU)

        'Next

    End Sub

    Private Sub RunCritereFlexionAcier(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal,
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
            If iTravee = iDebT Then iDebK = 1 Else iDebK = 0
            If iTravee = iFinT Then iFinK = 0 Else iFinK = 1
            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
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

        ''--> Déclarations

        ''Dim Critere As Decimal
        'Dim nbNodes As Integer = MyPoutre.Nodes.nbNodes
        'Dim iNode As Integer
        'Dim MEdMax As Decimal
        'Dim lOk As Boolean

        ''--> Initialisation

        'lClasse4 = False

        ''--> Boucle sur les noeuds

        'For iNode = 0 To nbNodes - 1

        '    If Math.Abs(MEd(iNode, 0)) > Math.Abs(MEd(iNode, 1)) Then
        '        MEdMax = MEd(iNode, 0)
        '    Else
        '        MEdMax = MEd(iNode, 1)
        '    End If

        '    lOk = True
        '    If MEdMax * SIGNEM > 0 Then

        '        Select Case ClasseP
        '            Case 1, 2
        '                MRd = MplRd
        '            Case 3
        '                MRd = MelRd
        '            Case 4
        '                lClasse4 = True
        '                lOk = False
        '        End Select

        '    Else

        '        Select Case ClasseM
        '            Case 1, 2
        '                MRd = MplRd
        '            Case 3
        '                MRd = MelRd
        '            Case 4
        '                lClasse4 = True
        '                lOk = False
        '        End Select

        '    End If

        '    Me.CritereM.EnregistreCritere(iNode, iCombi, MEdMax, MRd)

        'Next
    End Sub

#End Region



End Class
