Imports Microsoft.VisualBasic.Logging

Public Class cls_VerifFeuAcier

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereVb() As cls_Critere                   ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                   ' Résistance à l'interacion MV
    Public CritereLTB() As cls_Critere                  ' Résistance au déversement

    Dim AlphaCrLTB(,) As Decimal                        ' Alpha critique au déversement pour chaque step et chaque combi

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps

    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Public TempAStep() As Decimal                       ' Température de l'acier pour les Steps

    Public ElancementW As Decimal                       ' Elancement de l'âme 
    Public ElancementWMax As Decimal                    ' Limite d'elancement de l'âme pour le voilement par cisaillement

    Public TempAInter As List(Of Decimal)               ' Températures de l'acier pour les intervalles de temps
    Public TimeInter As Decimal = 120                   ' Intervalle de temps (en secondes) pour l'enregistrement de TempA

#End Region

#Region " Constructeurs et Initialisation "

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
        ReDim CritereVb(Me.NbStep - 1)
        ReDim CritereMV(Me.NbStep - 1)
        ReDim CritereLTB(Me.NbStep - 1)
        ReDim AlphaCrLTB(Me.NbStep - 1, NbCombi)

        For i As Integer = 0 To Me.NbStep - 1
            Me.CritereM(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereVb(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereLTB(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Next

        ReDim TempAStep(Me.NbStep - 1)

        Me.TempAInter = New List(Of Decimal)

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
        Dim kReducE As Decimal                  ' Coefficient réduction module Young en fct température de la section en acier

        Dim Massivete As Decimal                ' Massiveté de la section
        Dim kSh As Decimal                      ' Facteur d'ombre de la section

        Dim iCombi As Integer

        Dim EN_Feu As New cls_EurocodesFeu
        Dim lProtege As Boolean
        Dim lSsExposee As Boolean               ' Indique si la semelle supérieure est exposée au feu

        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing

        Dim VRd0, MplRd0, MelRd0 As Decimal     ' Résistance de la section à température ambiante
        Dim MplRk0, MelRk0 As Decimal           ' Résistances caractéristiques de la section à température ambiante
        Dim zANP0, zANE0 As Decimal
        Dim MplRdFeu() As Decimal               ' Moments résistants plastiques aux Time Steps
        Dim MelRdFeu() As Decimal               ' Moments résistants élastiques aux Time Steps
        Dim VplRdFeu() As Decimal               ' Efforts tranchant résistants plastiques aux Time Steps
        Dim VbRdFeu() As Decimal                ' Efforts tranchant résistants pour le voilement aux Time Steps

        Dim ClasseP, ClasseM As Integer         ' Classe des sections sous moment >0 et <0

        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1

        Dim AlphaCr As Decimal
        Dim lOK As Boolean

        Dim lMontantR As Boolean = myBeam.lTraveeConsoleGauche And myBeam.lTraveeConsoleDroite
        Dim RatioGammaM As Decimal
        Dim FyAcier As Decimal

        'Dim pTimeR As Decimal

        '--( Initialisation

        TempG = myBeam.ParamFeu.TempRef
        TempA = myBeam.ParamFeu.TempRef

        DeltaT = myBeam.ParamFeu.DeltaTCalcul
        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        lSsExposee = EN_Feu.SemelleSupExposee(myBeam)
        lProtege = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
        Massivete = EN_Feu.MassiveteSectionAcier(myBeam.Section.ProfilA, lSsExposee)

        FyAcier = myBeam.Section.FyMin

        If Not lProtege Then
            'kSh = 0.9 * Massivete / EN_Feu.MassiveteSectionAcierBox(myBeam.Section.ProfilA, lSsExposee)
            kSh = 0.9 * EN_Feu.MassiveteSectionAcierBox(myBeam.Section.ProfilA, lSsExposee) / Massivete
        End If

        '# Initialisation des Tableaux

        ReDim MplRdFeu(Me.NbStep - 1)
        ReDim MelRdFeu(Me.NbStep - 1)
        ReDim VplRdFeu(Me.NbStep - 1)
        ReDim VbRdFeu(Me.NbStep - 1)
        Me.InitialiseClassPourCalcul(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee)

        '# Propriétés à froid

        RatioGammaM = myBeam.Param.Gamma.GammaM0 / myBeam.Param.Gamma.GammaM_fi
        myBeam.ProprietesVerifAcier(True, MplRd0, zANP0, MelRd0, zANE0)
        VRd0 = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM_fi, myBeam.Param.EtaW)
        MplRk0 = MplRd0 * myBeam.Param.Gamma.GammaM0
        MelRk0 = MelRd0 * myBeam.Param.Gamma.GammaM0
        MplRd0 *= RatioGammaM
        MelRd0 *= RatioGammaM

        '# Classes de la section

        '    La classe des sections ne dépend pas du chargement (il n'y a pas d'effort axial) ni des contraintes.
        '    On classe donc les sections une fois pour toute, en dehors de la boucle sur les combinaisons de calcul

        ClasseP = myBeam.Section.ClasseSection(zANP0, zANE0, True, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1, 0, True)
        ClasseM = myBeam.Section.ClasseSection(zANP0, zANE0, False, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1, 0, True)

        Me.TimeInter = CInt((Me.TimeInter / DeltaT)) * DeltaT

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

                'If IsEqual(TimeT, 14400, 1 / 100000) Then
                '    TempA = TempA
                'End If

                lCont = IsSmaller(TimeT, TimeTarget, 10 ^ (-5))

                If IsEqual(TimeT Mod Me.TimeInter, 0) Then
                    Me.TempAInter.Add(TempA)
                    'pTimeR = TimeT
                End If
            Loop

            TempAStep(iSTep) = TempA

            '# Réduction des propriétés de l'acier en fct de la température

            kReducY = EN_Feu.ReducFyAcier(TempA)
            kReducE = EN_Feu.ReducEyAcier(TempAStep(iSTep))

            '# Résistance de la section 

            VplRdFeu(iSTep) = kReducY * VRd0
            MplRdFeu(iSTep) = kReducY * MplRd0
            MelRdFeu(iSTep) = kReducY * MelRd0
            VbRdFeu(iSTep) = myBeam.Section.VbRdFeu(myBeam.Param.Gamma.GammaM_fi, myBeam.Param.EtaW, lMontantR, kReducY, kReducE)

        Next

        '# Boucle sur les combinaisons de calcul pour vérifications

        For iCombi = 0 To nbCombiELU - 1

            '## Combinaisons des moments

            myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '## Combinaison des efforts tranchants

            myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '## Alpha critique

            CalculAlphaCritiqueN(myBeam, iCombi, MEd, AlphaCr, lOK)

            For iSTep = 0 To Me.NbStep - 1
                '## Vérification en flexion

                RunCritereFlexionAcier(myBeam, iCombi, iSTep, MEd, MplRdFeu(iSTep), MelRdFeu(iSTep), ClasseP, ClasseM)

                '## Vérification à l'effort tranchant

                RunCritereEffortTranchant(myBeam, iCombi, iSTep, VEd, VplRdFeu(iSTep))

                '## Vérification résistance au déversement

                RunCritereEffortTranchantVoilement(myBeam, iCombi, iSTep, VEd, VbRdFeu(iSTep))

                '## Vérification interaction MV

                '## Vérification au voilement par cisaillement

                kReducY = EN_Feu.ReducFyAcier(TempAStep(iSTep))
                kReducE = EN_Feu.ReducEyAcier(TempAStep(iSTep))

                RunCritereDeversement(myBeam, iCombi, iSTep, MEd, AlphaCr, kReducY, kReducE, MplRk0, MelRk0, ClasseP, FyAcier)

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
        If IsGreater(Me.CritereLTB(iStep).CritereMax, 1) Then lOK = False

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
                MRdM = MplRd
            Case 3
                MRdM = MelRd
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

    Private Sub RunCritereEffortTranchant(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer, VEd(,) As Decimal, VRd As Decimal, Optional lBuckling As Boolean = False)
        '----------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de calcul
        '   VEd         [E] :   Table des efforts tranchants le long de la barre
        '   VRd         [E] :   Effort tranchant résistant (plastique ou voilement) de la barre
        '   lBukling    [E] :   Indique si critere de résistance au voilement par cisaillement
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
                    If lBuckling Then
                        'Me.CritereVb.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
                    Else
                        Me.CritereV(iStep).EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereEffortTranchantVoilement(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer, VEd(,) As Decimal, VbRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant pour le voilement
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de calcul
        '   VEd         [E] :   Table des efforts tranchants le long de la barre
        '   VRd         [E] :   Effort tranchant résistant (plastique ou voilement) de la barre
        '   lBukling    [E] :   Indique si critere de résistance au voilement par cisaillement
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        Me.ElancementW = myBeam.Section.ProfilA.ElancementAme
        Me.ElancementWMax = 72 * myBeam.Section.Epsilon_W * 0.85 / myBeam.Param.EtaW

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    Me.CritereVb(iStep).EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VbRd)

                Next
            Next
        Next

    End Sub

#End Region

#Region " Vérifications de la résistance au déversement "

    Private Sub RunCritereDeversement(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer, MEd(,) As Decimal,
                                      AlphaCr As Decimal, kReducY As Decimal, kReducE As Decimal,
                                      MplRk As Decimal, MelRk As Decimal, ClasseP As Decimal, FyAcier As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU incendie de la résistance au déversement
        '----------------------------------------------------------------------------------------------------------
        '   myBeam          [E] :   Poutre traitée
        '   iCombi          [E] :   Indice de la combinaison
        '   iStep           [E] :   Indice du pas de calcul
        '   MEd             [E] :   Table des moments fléchissants le long de la poutre
        '   AlphaCr         [E] :   Coefficient d'amplification critique (ne dépend pas du time step)
        '   kReducY         [E] :   Coefficient de réduction de fy / température    
        '   kReducE         [E] :   Coefficient de réduction de E / température    
        '   MplRk           [E] :   Moment plastique caratéristique de la section à froid
        '   MelRk           [E] :   Moment élastique caratéristique de la section à froid
        '   ClasseP         [E] :   Classe de la section sous moment > 0
        '   FyAcier         [E] :   Limite d'élasticité de la section
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebTrav As Integer = myBeam.IndicePremiereTravee
        Dim iFinTrav As Integer = myBeam.IndiceDerniereTravee
        Dim iTrav, iNode As Integer
        Dim iDebNod, iFinNod As Integer
        Dim MEdmax, Mcr As Decimal
        Dim MbRd, KhiLT, LambdaBLT, AlphaLT As Decimal
        'Dim EN1993 As New cls_Eurocodes
        Dim ENfeu As New cls_EurocodesFeu
        'Dim zANE, InertieY As Decimal
        'Dim nEqEc As Decimal
        Dim GammaMFi As Decimal
        Dim MRk As Decimal

        '--> Initialisation

        GammaMFi = myBeam.Param.Gamma.GammaM_fi
        Select Case ClasseP
            Case 1, 2 : MRk = MplRk
            Case 3 : MRk = MelRk
            Case 4
        End Select

        '--> Calcul Alpha Critique

        Me.AlphaCrLTB(iStep, iCombi) = AlphaCr

        '--> Vérification par travée

        For iTrav = iDebTrav To iFinTrav

            iDebNod = myBeam.Nodes.iNodeExtTrav(iTrav, 0)
            iFinNod = myBeam.Nodes.iNodeExtTrav(iTrav, 1)

            '# Moment maxi dans la travée

            MEdmax = Math.Max(Math.Abs(MEd(iDebNod, 1)), Math.Abs(MEd(iFinNod, 0)))

            For iNode = iDebNod + 1 To iFinNod - 1
                For k = 0 To 1
                    MEdmax = Math.Max(MEdmax, Math.Abs(MEd(iNode, k)))
                Next
            Next

            '# Moment critique

            Mcr = AlphaCr * MEdmax

            If IsGreater(kReducE, 0) Then
                '# Elancement réduit

                LambdaBLT = Math.Sqrt((kReducY * MRk) / (kReducE * Mcr))

                '# Coefficient de réduction

                AlphaLT = ENfeu.AlphaLT(FyAcier)
                KhiLT = ENfeu.KhiLTFire(AlphaLT, LambdaBLT)

            Else
                KhiLT = 0
            End If

            '# Résistance

            MbRd = kReducY * KhiLT * MRk / GammaMFi

            '# Critere

            Me.CritereLTB(iStep).EnregistreCritere(iTrav, iCombi, iTrav, MEdmax, MbRd)

        Next

    End Sub

    Private Sub CalculAlphaCritiqueN(myPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal,
                                     ByRef AlphaCr As Decimal, ByRef lOK As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient critique au déversement
        '----------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre traitée
        '   iCombi              [E] :   Indice de la combinaisons traitée
        '   MEd                 [E] :   Diagramme de flexion
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '   AlphaCr             [S] :   Alpha Critique
        '   lOK                 [S] :   Indique si le calcul s'est bien déroulé
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim CoefCombi As New List(Of Decimal)
        Dim myAlphaCr As New cls_CalculCritique

        '--> Initialisation

        CoefCombi = myPoutre.CombiA_ELF.CoefCombi(iCombi)

        myAlphaCr.CalculAlphaCritique(myPoutre, CoefCombi, MEd, False, AlphaCr, lOK)

    End Sub

#End Region

End Class
