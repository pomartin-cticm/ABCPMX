Imports System.Collections.Specialized.BitVector32

Public Class cls_VerifFeuEnrobe

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180}

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                   ' Résistance à l'interacion MV

    Private NbStep As Integer

    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.NbStep = cls_VerifFeuEnrobe.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer)
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

        For i As Integer = 0 To Me.NbStep - 1
            Me.CritereM(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Next

    End Sub

#End Region

#Region "===Gestion de la classe==="

    Public Sub Z_VerifFeu(myBeam As cls_Poutre)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Gestion des calculs au feu pour les poutres à sections partielement enrobées de béton (acier ou mixte)
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbCombiELU As Integer
        Dim iStep, iCombi As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim MRdPos(,) As Decimal = Nothing
        Dim MRdNeg(,) As Decimal = Nothing
        Dim zPos(,) As Decimal = Nothing
        Dim zNeg(,) As Decimal = Nothing
        Dim Beff(,) As Decimal = Nothing
        Dim VRd As Decimal

        '--( Initialisation

        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee)
        myBeam.MaillageBeff(myBeam.Param.lLargeurEfficaceSimplifiee, False, Beff)

        '--( Boucle et calcul sur chaque time step

        For iStep = 0 To Me.NbStep - 1

            '# Calcul des résistances plastiques selon Annex F de EN 1994-1-2

            MaillagePropPlastiquesMixtes(myBeam, Beff, 1, cls_VerifFeuEnrobe.TimeSteps(iStep), MRdPos, zPos)
            MaillagePropPlastiquesMixtes(myBeam, Beff, -1, cls_VerifFeuEnrobe.TimeSteps(iStep), MRdNeg, zNeg)

            VRd = ResistanceEffortTranchant(myBeam.Section.ProfilA, myBeam.Section.FyW, myBeam.Param.EtaW, myBeam.Section.Enrobage.Ratio_bc,
                                            myBeam.Param.Gamma.GammaM_fi, cls_VerifFeuEnrobe.TimeSteps(iStep))

            '# Boucle sur les combinaisons de calcul

            For iCombi = 0 To nbCombiELU - 1

                '## Combinaisons des moments

                myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

                '## Combinaison des efforts tranchants

                myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

                '## Vérification en flexion

                RunCriteresMomentsPlastiques(myBeam, iCombi, iStep, MEd, MRdPos, MRdNeg)

                '## Vérification à l'effort tranchant

                RunCriteresEffortTranchant(myBeam, iCombi, iStep, VEd, VRd)

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

#Region " Routine de vérification "

    Private Sub RunCriteresMomentsPlastiques(MyPoutre As cls_Poutre, iCombi As Integer, iStep As Integer, MEd(,) As Decimal, MRdPos(,) As Decimal, MRdNeg(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   19/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant (critère de résistance plastique)
        '----------------------------------------------------------------------------------------------------------
        '   myBeam  [E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   iStep   [E] :   Indice du pas de calcul en temps
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MRdPos  [E] :   Table des moments plastiques le long de la barre, sous moments positifs
        '   MRdNeg  [E] :   Table des moments plastiques le long de la barre, sous moments négatifs
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
                    If IsGreaterOrEqual(MEd(iNode, k), 0) Then
                        Me.CritereM(iStep).EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRdPos(iNode, k))
                    Else
                        Me.CritereM(iStep).EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRdNeg(iNode, k))
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub RunCriteresEffortTranchant(MyPoutre As cls_Poutre, iCombi As Integer, iStep As Integer, VEd(,) As Decimal, VRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   19/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam  [E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   iStep   [E] :   Indice du pas de calcul en temps
        '   VEd     [E] :   Table des efforts tranchants le long de la barre
        '   VRd     [E] :   Effort tranchant résistant pour le temp iStep
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

                    Me.CritereV(iStep).EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)

                Next
            Next
        Next

    End Sub

#End Region

#Region " Propriétés en cisaillement "

    Private Function ResistanceEffortTranchant(myProfile As cls_ProfilA, FyW As Decimal, Eta As Decimal,
                                               Ratio_Bc As Decimal, GammaM As Decimal, Time As Decimal) As Decimal
        '---------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '---------------------------------------------------------------------------------------
        '   Calcul de l'effort tranchant résistant
        '---------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   FyW         [E] :   Limite d'élasticité de l'âme
        '   Eta         [E] :   Coefficient Eta pour la résistance au cisaillement
        '   RatioBc     [E] :   Ratio définissant la largeur de béton de l'enrobage partiel
        '   GammaM      [E] :   Coefficient partiel
        '   Time        [E] :   Temps du calcul
        '---------------------------------------------------------------------------------------

        '--( Déclaration

        Dim VRd As Decimal
        Dim ReducKa As Decimal
        Dim Hw As Decimal = myProfile.HauteurAmeHw
        Dim Hwl As Decimal
        Dim kRedV As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu
        Dim myBc As Decimal = myProfile.Bfs * Ratio_Bc

        '--( Calcul

        ReducKa = EN1994_1_2.AnnexF_ReducFyInf(Time, myProfile.Tfs, myProfile.ha, myBc)
        Hwl = EN1994_1_2.AnnexF_HauteurHwInf(Time, myProfile.Tw, myProfile.ha, myBc, Hw)

        kRedV = 1 + Hwl * (ReducKa - 1) / (2 * Hw)

        VRd = FyW / (GammaM * Math.Sqrt(3)) * myProfile.AireAv(Eta) * kRedV * kConvMPaPa

        Return VRd

    End Function


#End Region

#Region " Calcul des propriétés en flexion selon l'annexe F "

    Private Sub MaillagePropPlastiquesMixtes(myBeam As cls_Poutre, Beff(,) As Decimal, Signe As Decimal, Time As Decimal, ByRef MplRd(,) As Decimal, ByRef zANP(,) As Decimal)
        '------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des moments plastiques le long de la poutre (sur les noeuds du modèle)
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   Beff        [E] :   Largeur participante de dalle
        '   Signe       [E] :   Signe du moment à considérer
        '   Time        [E] :   Temps de calcul
        '   MplRd       [S] :   Table des moments plastiques au droit des noeuds du modèle
        '   zANP        [S] :   Table des position des ANP
        '------------------------------------------------------------------------------

        '--> Déclaration

        Dim BeffPrec As Decimal = -1
        Dim iNode As Integer
        Dim Eta As Decimal = 1      '#ALERTE : à adapter sur chaque section
        Dim k, kDeb, kFin As Integer
        Dim lMixte As Boolean

        '--> Initialisation

        ReDim MplRd(myBeam.Nodes.nbNodes - 1, 1)
        ReDim zANP(myBeam.Nodes.nbNodes - 1, 1)
        lMixte = myBeam.lMixte

        '--> Boucle sur les noeuds

        For iNode = 0 To myBeam.Nodes.nbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = myBeam.Nodes.nbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin
                If IsEqual(Beff(iNode, k), BeffPrec) Then
                    MplRd(iNode, k) = MplRd(iNode - 1, k)
                    zANP(iNode, k) = zANP(iNode - 1, k)
                Else
                    If Signe > 0 Then
                        Me.MomentPlastiquePlus(myBeam.Section, myBeam.Dalle, myBeam.ParamFeu, myBeam.Param.Gamma, lMixte, Beff(iNode, k), Time, MplRd(iNode, k), zANP(iNode, k))
                    Else
                        Me.MomentPlastiqueMoins(myBeam.Section, myBeam.Dalle, myBeam.ParamFeu, myBeam.Param.Gamma, Beff(iNode, k), Time, MplRd(iNode, k), zANP(iNode, k))
                    End If
                    BeffPrec = Beff(iNode, k)
                End If
            Next



        Next

    End Sub

    Private Sub MomentPlastiquePlus(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                    lMixte As Boolean, Beff As Decimal, Time As Decimal, ByRef MplRd As Decimal, ByRef zANP As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul du moment plastique positif sous incendie d'une section avec enrobage partiel (selon l'annexe F)
        '--------------------------------------------------------------------------------------------------------------------------
        '   mySection   [E] :   Section
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   lMixte      [E] :   Indique si mixité avec la dalle
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   Time        [E] :   Temps du calcul
        '   MplRd       [S] :   Moment plastique de calcul
        '   zANP        [S] :   Position de l'ANP
        '--------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim myModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = mySection.lLamine

        Const RhoV As Decimal = 0
        Dim FySup, FyInf, FyW As Decimal
        Const Signe As Decimal = 1
        Const lValeurRd As Boolean = True

        '--> Initialisation

        Hw = mySection.ProfilA.HauteurAmeHw
        FySup = mySection.FySup
        FyInf = mySection.FyInf
        FyW = mySection.FyW

        '--> Modélisation du profilé acier

        MaillageProfileAMPlus(mySection.ProfilA, Gammas, Time, RhoV, mySection.Enrobage.Ratio_bc, FySup, FyInf, FyW, myModele)

        '# Béton d'enrobage

        ' Dans l'annexe F, le béton d'enrobage est négligé dans le calcul à l'incendie sous moment positif

        '# Armatures de l'enrobage

        MaillageArmaEnrobMPlusMoins(mySection, myOptions, Gammas, Time, myModele)

        ''--> Dalle béton

        If lMixte And (Beff > 0) Then

            '# Dalle 

            MaillageDalleMPlus(myDalle, myOptions, Gammas, mySection.ProfilA.Bfs, Beff, Time, myModele)

        End If

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Private Sub MaillageArmaEnrobMPlusMoins(mySection As cls_Section, myOptions As cls_OptionsFeu, Gammas As cls_Gamma, Time As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   19/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage des armatures de l'enrobage en vue 
        '   du calcul du moment plastique positif ou négatif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   Gammas      [E] :   Coefficients partiels
        '   Time        [E] :   Temps du calcul
        '   RatioBc     [E] :   Ratio largeur Bc / largeur Bf
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim zArma, PhiA As Decimal
        Dim iPos, iBarre As Integer
        Dim NbBarres As Integer
        Dim Fsk As Decimal = mySection.Enrobage.AcierArmatures.FsK
        Dim ArmaNeq As Decimal = cls_Acier.EYACIER / mySection.Enrobage.AcierArmatures.Es
        Dim DeltaCArma As Decimal = 0
        Const NBMA As Integer = 2       ' Car symétrie des deux chambres

        Dim lActif As Boolean

        Dim EN1994_1_2 As New cls_EurocodesFeu
        Dim kReducKr As Decimal

        Dim iStep As Integer
        Dim Ha, Bc, Tw As Decimal
        Dim uBord, uSemel As Decimal

        '--( Initialisation

        iStep = Array.IndexOf(cls_VerifFeuEnrobe.TimeSteps, Time)

        '# Prise en compte des armatures comprimées

        If myOptions.lArmaCompression Then DeltaCArma = 1

        '--( Boucle sur les lits d'armature

        For iArma As Integer = 0 To 2

            For iPos = 0 To 2

                NbBarres = mySection.Enrobage.LitArma(iArma).NbBarres(iPos)

                lActif = mySection.Enrobage.LitArma(iArma).lBarreActive(iArma, iPos)

                If lActif Then

                    Ha = mySection.ProfilA.ha
                    Bc = mySection.ProfilA.Bfs * mySection.Enrobage.Ratio_bc
                    Tw = mySection.ProfilA.Tw

                    uBord = mySection.Enrobage.DistanceArmaBord(iArma, iPos, mySection.ProfilA.Bfs, mySection.ProfilA.Tw)

                    For iBarre = 1 To NbBarres

                        zArma = mySection.zPosArmaEnrobage(iArma, iPos, iBarre)
                        PhiA = mySection.Enrobage.LitArma(iArma).PhiBarre(iPos)

                        uSemel = zArma - (-Ha + mySection.ProfilA.Tfi)

                        kReducKr = EN1994_1_2.AnnexF_ReductionKrArmaEnrob(iStep, Ha, Bc, Tw, uBord, uSemel)

                        myModele.AddMailleCirculaire(PhiA / 2, zArma, 1, DeltaCArma, ArmaNeq, kReducKr * Fsk, 1, Gammas.GammaS_fi, NBMA, cls_Maille.EnuTypeMaille.Circulaire)

                    Next
                End If

            Next

        Next

    End Sub

    Private Sub MaillageDalleMPlus(myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma, Bfs As Decimal, Beff As Decimal, Time As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage de la dalle béton en vue 
        '   du calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   Time        [E] :   Temps du calcul
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Tc As Decimal = myDalle.EpaisseurActive
        Dim Aire As Decimal
        Dim HcFi, TcR, HcEff As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu
        Const nEqDalle As Decimal = 1       '(Calcul plastique)

        '--( Initialisation

        HcFi = EN1994_1_2.AnnexF_ReductionDalle(Time)
        Select Case myDalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine, cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                HcFi = Math.Min(Tc, HcFi)
                TcR = Tc - HcFi
            Case cls_Dalle.Enum_TypeDalle.Mixte
                Select Case myDalle.Bac.Orientation
                    Case cls_Bac.Enum_Orientation.Perpendiculaire
                        Select Case myDalle.Bac.AppuiT
                            Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus, cls_Bac.EnuConfigTAppui.BetonSeulContinu
                                If myDalle.Bac.lNervuresOuvertes Then
                                    HcFi = Math.Min(Tc, HcFi)
                                    TcR = Tc - HcFi
                                Else
                                    HcFi = Math.Min(myDalle.Bac.Hp, HcFi)
                                    TcR = myDalle.Ep_td - HcFi
                                End If
                            Case cls_Bac.EnuConfigTAppui.Discontinu
                                HcFi = Math.Min(Tc, HcFi)
                                TcR = Tc - HcFi
                        End Select
                    Case cls_Bac.Enum_Orientation.Parallele
                        If myDalle.Bac.lNervuresOuvertes Then
                            Tc = HcEff - myDalle.Bac.Hp
                            HcFi = Math.Min(Tc, HcFi)
                            TcR = Tc - HcFi
                        Else
                            HcFi = Math.Min(Tc, HcFi)
                            TcR = Tc - HcFi
                        End If
                End Select
        End Select

        '--> Maillage

        '# Partie supérieure
        Aire = Beff * TcR
        myModele.AddMaille(Aire, TcR, myDalle.zTop - TcR / 2, 0, 1, nEqDalle, myDalle.beton.Fck, myOptions.AlphaSlab, Gammas.GammaC_fi)

        '# Partie inférieure

        Aire = Bfs * HcFi
        myModele.AddMaille(Aire, HcFi, myDalle.zTop - TcR - HcFi / 2, 0, 1, nEqDalle, myDalle.beton.Fck, myOptions.AlphaSlab, Gammas.GammaC_fi)

    End Sub

    Private Sub MaillageProfileAMPlus(myProfile As cls_ProfilA, Gammas As cls_Gamma, Time As Decimal, RhoV As Decimal, Ratio_Bc As Decimal,
                                      FySup As Decimal, FyInf As Decimal, FyW As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier en vue 
        '   du calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   Gammas      [E] :   Coefficients partiels
        '   Time        [E] :   Temps du calcul
        '   RatioBc     [E] :   Ratio largeur Bc / largeur Bf
        '   FySup       [E] :   Limite d'élasticité de la semelle sup
        '   FyInf       [E] :   Limite d'élasticité de la semelle inf
        '   FyW         [E] :   Limite d'élasticité de l'âme
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Hw As Decimal
        Dim zRef As Decimal = myProfile.zRefAraseSup 'Cote de l'arase supérieure de la semelle supérieure du profilé 
        Dim LargBfs As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu
        Dim myBc As Decimal
        Dim ReducKa As Decimal
        Dim Hwl, Hwh As Decimal
        Dim Hwi, zHwi, Fywi As Decimal
        Const RatioHwi As Decimal = 1
        Dim NbHwi, iHwi As Integer
        Dim kcG, zcG As Decimal
        'Dim gammaM_fi As Decimal

        '--> Initialisation

        Hw = myProfile.HauteurAmeHw
        myBc = Ratio_Bc * Math.Min(myProfile.Bfs, myProfile.Bfi)
        ReducKa = EN1994_1_2.AnnexF_ReducFyInf(Time, myProfile.Tfs, myProfile.ha, myBc)
        kcG = (10 - 3 * Math.PI) / (12 - 3 * Math.PI)

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        LargBfs = myProfile.Bfs - 2 * EN1994_1_2.AnnexF_ReductionLargeurBfs(Time, myProfile.Tfs, myProfile.Bfs, myBc)
        myModele.AddMaille(LargBfs * myProfile.Tfs, myProfile.Tfs, zRef - myProfile.Tfs / 2, 1, 1, 1, FySup, 1, Gammas.GammaM_fi)

        '# Âme

        Hwl = EN1994_1_2.AnnexF_HauteurHwInf(Time, myProfile.Tw, myProfile.ha, myProfile.Bfs * Ratio_Bc, Hw)
        Hwh = Hw - Hwl

        '### Partie supérieure
        If IsGreater(Hwh, 0) Then
            myModele.AddMaille(Hwh * myProfile.Tw, Hwh, zRef - myProfile.Tfs - Hwh / 2, 1, 1, 1, FyW, (1 - RhoV), Gammas.GammaM_fi)
        End If

        '### Partie inférieure ' On discretise car la limite d'élasticité varie avec la position
        Hwi = myProfile.Tw * RatioHwi
        NbHwi = (Math.Ceiling(Hwl / Hwi))
        Hwi = Hwl / NbHwi
        For iHwi = 1 To NbHwi
            zHwi = (iHwi - 0.5) * Hwi
            Fywi = FyW * (1 - zHwi / Hwl * (1 - ReducKa))
            myModele.AddMaille(Hwi * myProfile.Tw, Hwi, zRef - myProfile.Tfs - Hwh - Hwi / 2, 1, 1, 1, Fywi, (1 - RhoV), Gammas.GammaM_fi)
        Next

        '# Semelle inférieure

        myModele.AddMaille(myProfile.AireFi, myProfile.Tfi, zRef - myProfile.ha + myProfile.Tfi / 2, 1, 1, 1, ReducKa * FyInf, 1, Gammas.GammaM_fi)

        If myProfile.Rcs > 0 Then

            '# Congés supérieurs

            If IsGreater(Hwh, kcG * myProfile.Rcs) Then
                '--| Cas où les congés sup sont dans la partie Hwh
                Fywi = FyW
            Else
                '--| Cas où les congés sup sont dans la partie Hwl
                zcG = kcG * myProfile.Rcs - Hwh
                Fywi = FyW * (1 - zcG * (1 - ReducKa))
            End If

            myModele.AddMailleConges(myProfile.Rcs, zRef - myProfile.Tfs, 1, 1, 1, Fywi, (1 - RhoV), Gammas.GammaM_fi, cls_Maille.EnuTypeMaille.CongeSup)

        End If

        If myProfile.Rci > 0 Then

            '# Congés inférieurs

            If IsGreater(Hwl, kcG * myProfile.Rci) Then
                '--| Cas où les congés sup sont dans la partie Hwl
                zcG = Hwl - kcG * myProfile.Rci
                Fywi = FyW * (1 - zcG * (1 - ReducKa))
            Else
                '--| Cas où les congés sup sont dans la partie Hwh
                Fywi = FyW
            End If

            myModele.AddMailleConges(myProfile.Rci, zRef - myProfile.ha + myProfile.Tfi, 1, 1, 1, Fywi, (1 - RhoV), Gammas.GammaM_fi, cls_Maille.EnuTypeMaille.CongeInf)

        End If

    End Sub

    Private Sub MomentPlastiqueMoins(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                     Beff As Decimal, Time As Decimal, ByRef MplRd As Decimal, ByRef zANP As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul du moment plastique négatif sous incendie d'une section avec enrobage partiel (selon l'annexe F)
        '--------------------------------------------------------------------------------------------------------------------------
        '   mySection   [E] :   Section
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   Time        [E] :   Temps du calcul
        '   MplRd       [S] :   Moment plastique de calcul
        '   zANP        [S] :   Position de l'ANP
        '--------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim myModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = mySection.lLamine

        Const RhoV As Decimal = 0
        Dim FySup, FyInf, FyW As Decimal
        Const Signe As Decimal = 1
        Const lValeurRd As Boolean = True

        '--> Initialisation

        Hw = mySection.ProfilA.HauteurAmeHw
        FySup = mySection.FySup
        FyInf = mySection.FyInf
        FyW = mySection.FyW

        '--> Modélisation du profilé acier

        MaillageProfileAMMoins(mySection.ProfilA, Gammas, Time, mySection.Enrobage.Ratio_bc, FySup, myModele)

        '# Béton d'enrobage

        MaillageEnrobageMMoins(mySection, myOptions, Gammas, Time, myModele)

        '# Armatures de l'enrobage

        MaillageArmaEnrobMPlusMoins(mySection, myOptions, Gammas, Time, myModele)

        '# Dalle béton

        '--> Sans objet car on considère que la dalle est tendue donc inactive

        '# Armatures dalle béton 

        MaillageArmaturesDalleMMoins(myDalle, myOptions, Gammas, mySection.ProfilA.Bfs, mySection.ProfilA.Bfi, Beff, Time, myModele)

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Private Sub MaillageEnrobageMMoins(mySection As cls_Section, myOptions As cls_OptionsFeu, Gammas As cls_Gamma, Time As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   19/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage du béton d'enrobage en vue 
        '   du calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   Gammas      [E] :   Coefficients partiels
        '   Time        [E] :   Temps du calcul
        '   RatioBc     [E] :   Ratio largeur Bc / largeur Bf
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        Dim LargeurC, bc, EpaisseurC, FdC, DeltaT, neq As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu

        '--> Initialisation

        bc = mySection.LargeurEnrobagePartielBc
        LargeurC = Math.Max(EN1994_1_2.AnnexF_bcr(Time, bc) - mySection.ProfilA.Tw, 0)
        EpaisseurC = Math.Max(mySection.ProfilA.HauteurAmeHw - EN1994_1_2.AnnexF_hcfi(Time, mySection.ProfilA.ha, bc), 0)
        FdC = mySection.Enrobage.Beton.Fck
        DeltaT = 0
        neq = 1 '-> calcul plastique ici

        myModele.AddMaille(LargeurC * EpaisseurC, EpaisseurC, -mySection.ProfilA.Tfs - EpaisseurC / 2, DeltaT, 1, neq, FdC, 1, Gammas.GammaC_fi, cls_Maille.EnuTypeMaille.Rectangulaire)

        'Pour les profilés laminés, on doit retirer du béton la parties correspondant aux congés

        If mySection.lLamine Then

            '# Congés supérieurs

            myModele.AddMailleConges(mySection.ProfilA.Rcs, -mySection.ProfilA.Tfs, DeltaT, 1, neq, FdC, 1, Gammas.GammaC_fi, cls_Maille.EnuTypeMaille.CongeSup, -1)

            '# Congés supérieurs

            '--> sans objet

        End If
    End Sub

    Private Sub MaillageArmaturesDalleMMoins(myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma, Bfs As Decimal, Bfi As Decimal, Beff As Decimal, Time As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage de la dalle béton en vue 
        '   du calcul du moment plastique négatif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   Bfi         [E] :   Largeur de la semelle inférieure
        '   Time        [E] :   Temps du calcul
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Fsk As Decimal = myDalle.AcierArmatures.FsK
        Dim ksd, u As Decimal
        Dim b As Decimal = Math.Min(Bfs, Bfi)
        Dim zArma, PhiA As Decimal
        Dim Ztop As Decimal
        Dim NbBarres As Integer
        Dim ArmaNeq As Decimal = cls_Acier.EYACIER / myDalle.AcierArmatures.Es
        Dim DeltaCArma As Decimal = 0 'Pour le moment on néglige les armatures comprimées

        Dim EN1994_1_2 As New cls_EurocodesFeu

        Dim iStep As Integer

        '--( Initialisation

        iStep = Array.IndexOf(cls_VerifFeuEnrobe.TimeSteps, Time)

        Ztop = myDalle.zTop

        '# Prise en compte des armatures comprimées

        If myOptions.lArmaCompression Then DeltaCArma = 1

        '--( Boucle sur les lits d'armature

        For iLit As Integer = 0 To myDalle.LitArma.Count - 1
            If myDalle.LitArma(iLit).lActive Then 'on modélise le lit uniquement s'il est actif 

                If myDalle.LitArma(iLit).EspBar <> 0 Then 'sécurité pour éviter les divisions par 0
                    NbBarres = Math.Min(Beff, 3 * b) / myDalle.LitArma(iLit).EspBar
                Else
                    NbBarres = 0
                End If

                PhiA = myDalle.LitArma(iLit).PhiS

                zArma = Ztop - myDalle.LitArma(iLit).z_s

                u = Math.Min(zArma, myDalle.Ep_td - zArma)

                ksd = EN1994_1_2.AnnexF_ksd(Time, u)

                myModele.AddMailleCirculaire(PhiA / 2, zArma, 1, DeltaCArma, ArmaNeq, ksd * Fsk, 1, Gammas.GammaS_fi, NbBarres, cls_Maille.EnuTypeMaille.Circulaire)
            End If

        Next

    End Sub

    Private Sub MaillageProfileAMMoins(myProfile As cls_ProfilA, Gammas As cls_Gamma, Time As Decimal, Ratio_Bc As Decimal,
                                      FySup As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   02/05/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier en vue 
        '   du calcul du moment plastique négatif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   Gammas      [E] :   Coefficients partiels
        '   Time        [E] :   Temps du calcul
        '   RatioBc     [E] :   Ratio largeur Bc / largeur Bf
        '   FySup       [E] :   Limite d'élasticité de la semelle sup
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim zRef As Decimal = myProfile.zRefAraseSup 'Cote de l'arase supérieure de la semelle supérieure du profilé 
        Dim LargBfs As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu
        Dim myBc As Decimal

        '--> Initialisation

        myBc = Ratio_Bc * Math.Min(myProfile.Bfs, myProfile.Bfi)

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        LargBfs = myProfile.Bfs - 2 * EN1994_1_2.AnnexF_ReductionLargeurBfs(Time, myProfile.Tfs, myProfile.Bfs, myBc)
        myModele.AddMaille(LargBfs * myProfile.Tfs, myProfile.Tfs, zRef - myProfile.Tfs / 2, 1, 1, 1, FySup, 1, Gammas.GammaM_fi)

        '# Âme + Semelle inférieure

        '--> non prise en compte en cas de flexion négative

    End Sub


#End Region


End Class
