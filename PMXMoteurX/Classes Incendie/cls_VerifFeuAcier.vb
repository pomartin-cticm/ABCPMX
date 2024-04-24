Public Class cls_VerifFeuAcier

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                   ' Résistance à l'interacion MV
    Public CritereLTB() As cls_Critere                  ' Résistance au déversement

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps

    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Public TempAStep() As Decimal                       ' Température de l'acier pour les Steps

#End Region


#Region " Constructeurs "

    Public Sub New()
        Me.NbStep = cls_VerifFeuAcier.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
    End Sub

    Private Sub InitialiseClassPourCalcul(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '----------------------------------------------------------------------------------------------------------

        ReDim CritereM(Me.NbStep - 1)
        ReDim CritereV(Me.NbStep - 1)
        ReDim CritereMV(Me.NbStep - 1)
        ReDim CritereLTB(Me.NbStep - 1)

        For i As Integer = 0 To Me.NbStep - 1
            Me.CritereM(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereLTB(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Next

        ReDim TempAStep(Me.NbStep - 1)

    End Sub

#End Region


#Region "===Gestion de la classe==="

    Public Sub Z_VerifFeu(myBeam As cls_Poutre)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Gestion des calculs au feu pour les poutres acier non enrobées
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbCombiELU As Integer

        Dim TimeT As Decimal = 0
        Dim TimeTarget As Decimal
        Dim iSTep As Integer
        Dim DeltaT As Decimal
        Dim lCont As Boolean

        Dim TempG As Decimal                    ' Température des gaz chauds
        Dim TempA As Decimal                    ' Température de la section en acier

        Dim kReducY As Decimal                  ' Coefficient réduction limite d'élasticité en fct température de la section en acier

        Dim Massivete As Decimal                ' Massiveté de la section
        Dim kSh As Decimal                      ' Facteur d'ombre de la section

        Dim iCombi As Integer

        Dim EN_Feu As New cls_EurocodesFeu
        Dim lProtege As Boolean
        Dim lSsExposee As Boolean               ' Indique si la semelle supérieure est exposée au feu

        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing

        Dim VRd0, MplRd0, MelRd0 As Decimal     ' Résistance de la section à température ambiante
        Dim zANP0, zANE0 As Decimal
        Dim MplRdFeu() As Decimal               ' Moments résistants plastiques aux Time Steps
        Dim MelRdFeu() As Decimal               ' Moments résistants élastiques aux Time Steps
        Dim VplRdFeu() As Decimal               ' Efforts tranchant résistants plastiques aux Time Steps

        Dim ClasseP, ClasseM As Integer         ' Classe des sections sous moment >0 et <0

        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1

        '--( Initialisation

        TempG = myBeam.ParamFeu.TempRef
        TempA = myBeam.ParamFeu.TempRef

        DeltaT = myBeam.ParamFeu.DeltaTCalcul
        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        lProtege = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
        Massivete = EN_Feu.MassiveteSectionAcier(myBeam.Section.ProfilA, lSsExposee)

        If Not lProtege Then
            kSh = 0.9 * Massivete / EN_Feu.MassiveteSectionAcierBox(myBeam.Section.ProfilA, lSsExposee)
        End If

        '# Initialisation des Tableaux

        ReDim MplRdFeu(Me.NbStep - 1)
        ReDim MelRdFeu(Me.NbStep - 1)
        ReDim VplRdFeu(Me.NbStep - 1)
        Me.InitialiseClassPourCalcul(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee)

        '# Propriétés à froid

        myBeam.ProprietesVerifAcier(True, MplRd0, zANP0, MelRd0, zANE0)

        '# Classes de la section

        '    La classe des sections ne dépend pas du chargement (il n'y a pas d'effort axial) ni des contraintes.
        '    On classe donc les sections une fois pour toute, en dehors de la boucle sur les combinaisons de calcul

        ClasseP = myBeam.Section.ClasseSection(zANP0, zANE0, True, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1, 0, True)
        ClasseM = myBeam.Section.ClasseSection(zANP0, zANE0, False, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1, 0, True)

        '--( Boucle sur TimeSteps

        For iSTep = 0 To Me.NbStep - 1

            TimeTarget = cls_VerifFeuAcier.TimeSteps(iSTep) * kConvMinSec
            lCont = IsSmaller(TimeT, TimeTarget)

            Do While lCont

                '# Boucle sur le temps jusqu'à obtenir la durée cible

                TimeT += DeltaT

                '# Température des gaz chauds

                TempG = EN_Feu.TemperatureGazISO(TimeT)

                '# Calcul de l'échauffement de la section sur le pas de temps

                If lProtege Then
                    TempA += EN_Feu.DeltaTempAcierProtege(TempA, TempG, Massivete, kSh, TimeT, DeltaT, myBeam.ParamFeu)
                Else
                    TempA += EN_Feu.DeltaTempAcierNonProtege(TempA, TempG, Massivete, kSh, DeltaT, myBeam.ParamFeu)
                End If

                '# 

                lCont = IsSmaller(TimeT, TimeTarget)

            Loop

            TempAStep(iSTep) = TempA

            '# Réduction des propriétés de l'acier en fct de la température

            kReducY = EN_Feu.ReducFyAcier(TempA)

            '# Résistance de la section 

            VplRdFeu(iSTep) = kReducY * VRd0
            MplRdFeu(iSTep) = kReducY * MplRd0
            MelRdFeu(iSTep) = kReducY * MelRd0

        Next

        '# Boucle sur les combinaisons de calcul pour vérifications

        For iCombi = 0 To nbCombiELU - 1

            '## Combinaisons des moments

            myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '## Combinaison des efforts tranchants

            myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            For iSTep = 0 To Me.NbStep - 1
                '## Vérification en flexion

                RunCritereFlexionAcier(myBeam, iCombi, iSTep, MEd, MplRdFeu(iSTep), MelRdFeu(iSTep), ClasseP, ClasseM)

                '## Vérification à l'effort tranchant

                'RunCriteresEffortTranchant(myBeam, iCombi, iSTep, VEd, VRd)

                '## Vérification interaction MV

                '## Vérification résistance au déversement



            Next

        Next

        '--( Recherche de la durée de résistance au feu

        DureeResistanceAuFeu()

    End Sub

    Private Sub DureeResistanceAuFeu()
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Recherche du pas de calcul pour lequel tous les critères sont OK
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lResist As Boolean

        '--( Traitement

        RStep = NbStep - 1
        lResist = IsResistanceAuFeuOK(RStep)

        Do While (Not lResist) And (Me.RStep >= 0)
            Me.RStep -= 1
            If Me.RStep >= 0 Then lResist = IsResistanceAuFeuOK(RStep)
        Loop

    End Sub

    Private Function IsResistanceAuFeuOK(iStep As Integer) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Recherche du pas de calcul pour lequel tous les critères sont OK
        '--------------------------------------------------------------------------------------------------------------------------
        '   iStep      [E] :   Indice du pas de temps de calcul
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lOK As Boolean = True

        '--( Vérifications

        If IsGreater(Me.CritereM(iStep).CritereMax, 1) Then lOK = False
        If IsGreater(Me.CritereV(iStep).CritereMax, 1) Then lOK = False

        Return lOK

    End Function

#End Region

#Region " Vérification de la résistance en section "

    Private Sub RunCritereFlexionAcier(MyPoutre As cls_Poutre, iCombi As Integer, iStep As Integer, MEd(,) As Decimal,
                                       MplRd As Decimal, MelRd As Decimal, ClasseP As Integer, ClasseM As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de calcul
        '   MEd         [E] :   Table des moments fléchissants le long de la barre
        '   MplRd       [E] :   Moment résitant plastique
        '   MelRd       [E] :   Moment élastique
        '   ClasseP     [E] :   Classe de la section sous moment positif
        '   ClasseM     [E] :   Classe de la section sous moment négatif
        '   lClasse4    [S] :   Indique qu'au moins une des sections est de classe 4
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim MRdP, MRdM, MRd As Decimal
        Const SIGNEM As Decimal = 1

        '--> Initialisation

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        Select Case ClasseP
            Case 1, 2
                MRdP = MplRd
            Case 3
                MRdP = MelRd
        End Select
        Select Case ClasseM
            Case 1, 2
                MRdm = MplRd
            Case 3
                MRdm = MelRd
        End Select

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    If MEd(iNode, k) * SIGNEM > 0 Then
                        MRd = MRdP
                    Else
                        MRd = MRdM
                    End If

                    Me.CritereM(iStep).EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRd)

                Next
            Next
        Next

    End Sub



#End Region


End Class
