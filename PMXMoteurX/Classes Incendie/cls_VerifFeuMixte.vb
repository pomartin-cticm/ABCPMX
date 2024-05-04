Public Class cls_VerifFeuMixte

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}              ' En minutes

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                   ' Résistance à l'interacion MV

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps

    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Public TempFsStep() As Decimal                      ' Température de la semelle supérieure pour les Steps
    Public TempFiStep() As Decimal                      ' Température de la semelle inférieure pour les Steps
    Public TempWStep() As Decimal                       ' Température de l'âme pour les Steps
    Public TempVStep() As Decimal                       ' Température des connecteurs pour les Steps

    Public TempDalleStep(,) As Decimal                  ' Température des deux faces de la dalle pour les Steps

    Public ElancementW As Decimal                       ' Elancement de l'âme 
    Public ElancementWMax As Decimal                    ' Limite d'elancement de l'âme pour le voilement par cisaillement

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.NbStep = cls_VerifFeuEnrobe.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
    End Sub

    Private Sub InitialiseClassePourCalcul(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer)
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

        ReDim TempFsStep(Me.NbStep - 1)
        ReDim TempFiStep(Me.NbStep - 1)
        ReDim TempWStep(Me.NbStep - 1)
        ReDim TempVStep(Me.NbStep - 1)

        ReDim TempDalleStep(Me.NbStep - 1, 1)

    End Sub

#End Region

#Region "===Gestion de la classe==="

    Public Sub Z_VerifFeu(myBeam As cls_Poutre)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Gestion des calculs au feu pour les poutres mixtes non enrobées de béton
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
        Dim TempFs As Decimal                   ' Température de la semelle supérieure
        Dim TempFi As Decimal                   ' Température de la semelle inférieure
        Dim TempW As Decimal                    ' Température de l'âme

        Dim kReducYFs As Decimal                ' Coefficient réduction limite d'élasticité en fct température de la semelle sup
        Dim kReducYFi As Decimal                ' Coefficient réduction limite d'élasticité en fct température de la semelle inf
        Dim kReducYW As Decimal                 ' Coefficient réduction limite d'élasticité en fct température de l'âme

        Dim MassivFs As Decimal                 ' Massiveté de la semelle sup
        Dim MassivFi As Decimal                 ' Massiveté de la semelle inf
        Dim MassivW As Decimal                  ' Massiveté de l'âme
        Dim MassivS As Decimal                  ' Massiveté de la section complète
        Dim kSh As Decimal                      ' Facteur d'ombre de la section

        Dim iCombi As Integer

        Dim EN_Feu As New cls_EurocodesFeu
        Dim lProtege As Boolean
        Dim lSsExposee As Boolean               ' Indique si la semelle supérieure est exposée au feu

        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing

        ' Dim MplRdFeu() As Decimal               ' Moments résistants plastiques aux Time Steps
        ' Dim MelRdFeu() As Decimal               ' Moments résistants élastiques aux Time Steps
        Dim VplRdFeu() As Decimal               ' Efforts tranchant résistants plastiques aux Time Steps

        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim lSimple As Boolean = myBeam.Param.lLargeurEfficaceSimplifiee
        Dim lBoard As Boolean = myBeam.ParamFeu.lProtectionBoard

        Dim MplRdP() As Decimal = Nothing       ' Moments résistants plastiques le long de la barre
        Dim zANPP() As Decimal = Nothing        ' Positions ANP le long de la barre
        Dim MplRdM() As Decimal = Nothing       ' Moments résistants plastiques le long de la barre
        Dim zANPM() As Decimal = Nothing        ' Positions ANP le long de la barre
        Dim bEff() As Decimal = Nothing         ' Largeur efficace de la dalle

        Dim VRd0 As Decimal

        '--( Paramètres pour la discrétisation de la dalle

        Dim NbTranches As Integer               ' Nombre de tranches discrétisant la dalle
        Dim EpTranche() As Decimal = Nothing    ' Epaisseur de chaque tranche (indice 0 pour la tranche inférieure)
        Dim zTranche() As Decimal = Nothing     ' Position z de la fibre moyenne de chaque tranche
        Dim TempCTranche() As Decimal = Nothing ' Température de chaque tranche
        Dim EpDalle As Decimal                  ' Epaisseur de dalle constante utilisée pour le calcul

        Dim TempCStep As New List(Of Decimal()) ' Temperature pour une time step dans chaque couche

        '--( Paramètres analyse diagramme moments

        Dim iNodeMmax() As Integer = Nothing
        Dim Mmax() As Decimal = Nothing
        Dim xMZero(,) As Decimal = Nothing
        Dim lTraveeMomNeg() As Boolean = Nothing
        Dim DeltaRd() As List(Of Decimal) = Nothing

        '--( Initialisation

        TempG = myBeam.ParamFeu.TempRef
        TempFs = myBeam.ParamFeu.TempRef
        TempFi = myBeam.ParamFeu.TempRef
        TempW = myBeam.ParamFeu.TempRef

        DeltaT = myBeam.ParamFeu.DeltaTCalcul
        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        lProtege = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
        MassivFs = EN_Feu.MassiveteSemelleSup(myBeam.Section.ProfilA, lSsExposee)
        MassivFi = EN_Feu.MassiveteSemelleInf(myBeam.Section.ProfilA)
        MassivW = EN_Feu.MassiveteAme(myBeam.Section.ProfilA)
        MassivS = EN_Feu.MassiveteSectionAcierBoardP(myBeam.Section.ProfilA)

        If Not lProtege Then
            kSh = EN_Feu.kShMixte(myBeam.Section.ProfilA)
        End If

        ' ReDim MplRdFeu(Me.NbStep - 1)
        ' ReDim MelRdFeu(Me.NbStep - 1)
        ReDim VplRdFeu(Me.NbStep - 1)

        Me.InitialiseClassePourCalcul(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee)

        If myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            EpDalle = EN_Feu.EpaisseurEfficaceDalleMixte(myBeam.Dalle.Ep_td, myBeam.Dalle.Bac)
        Else
            EpDalle = myBeam.Dalle.EpaisseurActive
        End If

        myBeam.MaillageBeff(lSimple, False, bEff)

        VRd0 = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM_fi)

        '--( Préparation du maillage de la dalle

        EN_Feu.PrepareMaillageDalleTabulee(EpDalle, myBeam.Param.lGeneration1, NbTranches, EpTranche, zTranche)

        '--( Boucle sur TimeSteps

        For iSTep = 0 To Me.NbStep - 1

            TimeTarget = cls_VerifFeuAcier.TimeSteps(iSTep) * kConvMinSec
            lCont = IsSmaller(TimeT, TimeTarget)

            Do While lCont

                '# Boucle sur le temps jusqu'à obtenir la durée cible

                TimeT += DeltaT

                '# Température des gaz chauds

                TempG = EN_Feu.TemperatureGazISO(TimeT)

                '# Calcul de l'échauffement de la section acier sur le pas de temps

                If lProtege Then
                    If lBoard Then
                        TempFs += EN_Feu.DeltaTempAcierProtege(TempFs, TempG, MassivS, kSh, TimeT, DeltaT, myBeam.ParamFeu)
                        TempFi = TempFs
                        TempW = TempFs
                    Else
                        TempFs += EN_Feu.DeltaTempAcierProtege(TempFs, TempG, MassivFs, kSh, TimeT, DeltaT, myBeam.ParamFeu)
                        TempFi += EN_Feu.DeltaTempAcierProtege(TempFi, TempG, MassivFs, kSh, TimeT, DeltaT, myBeam.ParamFeu)
                        TempW += EN_Feu.DeltaTempAcierProtege(TempW, TempG, MassivFs, kSh, TimeT, DeltaT, myBeam.ParamFeu)
                    End If
                Else
                    TempFs += EN_Feu.DeltaTempAcierNonProtege(TempFs, TempG, MassivFs, kSh, DeltaT, myBeam.ParamFeu)
                    TempFi += EN_Feu.DeltaTempAcierNonProtege(TempFi, TempG, MassivFi, kSh, DeltaT, myBeam.ParamFeu)
                    TempW += EN_Feu.DeltaTempAcierNonProtege(TempW, TempG, MassivW, kSh, DeltaT, myBeam.ParamFeu)
                End If

                '# Calcul de l'échauffement de la dalle sur le pas de temps (cas de la méthode numérique)

                If myBeam.ParamFeu.lDalleFEM Then

                End If

                '# 

                lCont = IsSmaller(TimeT, TimeTarget)

            Loop

            '# Récupération des températures dans les parties en acier

            TempFsStep(iSTep) = TempFs
            TempFiStep(iSTep) = TempFi
            TempWStep(iSTep) = TempW

            '# Calcul des températures dans le béton dans le cas de la méthode tabulée

            If Not myBeam.ParamFeu.lDalleFEM Then
                EN_Feu.TemperatureDalleTabulee(TimeSteps(iSTep), myBeam.Param.lGeneration1, NbTranches, TempCTranche)
            End If

            '# Récupération de la température dans la dalle

            TempCStep.Add(TempCTranche)

            '# Réduction des propriétés de l'acier en fct de la température

            kReducYFs = EN_Feu.ReducFyAcier(TempFs)
            kReducYFi = EN_Feu.ReducFyAcier(TempFi)
            kReducYW = EN_Feu.ReducFyAcier(TempW)

            '# Résistance de la section 

            VplRdFeu(iSTep) = kReducYW * VRd0

        Next

        '# Boucle sur les combinaisons de calcul pour vérifications

        For iCombi = 0 To nbCombiELU - 1

            '## Combinaisons des moments

            myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '## Combinaison des efforts tranchants

            myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '# Analyse du diagramme de moment

            myBeam.AnalyseDiagrammeMoments(MEd, iNodeMmax, Mmax, xMZero, lTraveeMomNeg)

            '# Calcul des propriétés plastiques le long de la barre,
            ' avec prise en compte de la connection,
            ' sans prise en compte de la réduction induit par l'effort tranchant 

            myBeam.MaillageRConnexion(xMZero, DeltaRd)

            '## Classification

            For iSTep = 0 To Me.NbStep - 1
                '## Calculs des moments plastiques en fct de la température

                MaillagePropPlastiquesMixtes(myBeam, bEff, 1, True, lGeneration1, False,
                                             TempFsStep(iSTep), TempFiStep(iSTep), TempWStep(iSTep),
                                             NbTranches, zTranche, EpTranche, TempCStep(iSTep), MplRdP, zANPP)

                '## Vérification en flexion

                RunCritereFlexionMixte(myBeam, iCombi, iSTep, MEd, MplRdP, MplRdM)

                '## Vérification à l'effort tranchant

                RunCritereEffortTranchant(myBeam, iCombi, iSTep, VEd, VplRdFeu(iSTep))


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

#Region " Propriétés des sections mixtes en fonction de la température "

    'Private Sub MomentPlastiquePlus(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
    '                                Beff As Decimal, reducKyFs As Decimal, reducKyFi As Decimal, reducKyW As Decimal,
    '                                ByRef MplRd As Decimal, ByRef zANP As Decimal)
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   18/04/24 :  Création - POM
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   Calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   mySection   [E] :   Section
    '    '   myDalle     [E] :   Dalle
    '    '   myOptions   [E] :   Options de calcul à l'incendie
    '    '   Gammas      [E] :   Coefficients partiels
    '    '   lMixte      [E] :   Indique si mixité avec la dalle
    '    '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
    '    '   reducKyFs   [E] :   Réduction de la limite d'élasticité de la semelle sup
    '    '   reducKyFi   [E] :   Réduction de la limite d'élasticité de la semelle inf
    '    '   reducKyW    [E] :   Réduction de la limite d'élasticité de l'âme
    '    '   MplRd       [S] :   Moment plastique de calcul
    '    '   zANP        [S] :   Position de l'ANP
    '    '--------------------------------------------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim myModele As New cls_ModeleP
    '    Dim Hw As Decimal
    '    Dim lLamine As Boolean = mySection.lLamine

    '    Const RhoV As Decimal = 0
    '    Dim FySup, FyInf, FyW As Decimal
    '    Const Signe As Decimal = 1
    '    Const lValeurRd As Boolean = True
    '    Const lMixte As Boolean = True

    '    '--> Initialisation

    '    Hw = mySection.ProfilA.HauteurAmeHw
    '    FySup = mySection.FySup
    '    FyInf = mySection.FyInf
    '    FyW = mySection.FyW

    '    '--> Modélisation du profilé acier

    '    myModele.MaillageProfileA_YY(Gammas.GammaM_fi, RhoV, mySection.ProfilA, reducKyFs * FySup, reducKyFi * FyInf, reducKyW * FyW, 0)

    '    ''--> Dalle béton

    '    If lMixte And (Beff > 0) Then

    '        '# Dalle 

    '        '    MaillageDalleMPlus(myDalle, myOptions, Gammas, mySection.ProfilA.Bfs, Beff, Time, myModele)

    '    End If

    '    '--> Recherche de l'axe neutre plastique

    '    myModele.RechercheANP(Signe, zANP, lValeurRd)

    '    '--> Moment plastique

    '    MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    'End Sub

    Private Sub MaillagePropPlastiquesMixtes(myBeam As cls_Poutre, Beff() As Decimal, Signe As Decimal, lValRd As Boolean,
                                             lGen1 As Boolean, lApplyBeta As Boolean, TempFs As Decimal, TempW As Decimal, TempFi As Decimal,
                                             NbTranches As Integer, zTran() As Decimal, eTran() As Decimal, TempC() As Decimal,
                                             ByRef MplRd() As Decimal, ByRef zANP() As Decimal)
        '---------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul des moments plastiques le long de la poutre (sur les noeuds du modèle)
        '---------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   Beff        [E] :   Largeur participante de dalle
        '   Signe       [E] :   Signe du moment à considérer
        '   lValRd      [E] :   Indique si valeurs de calcul
        '   lGen1       [E] :   Indique si génération 1 des eurocodes
        '   lApplyBeta  [E] :   Indique si on applique la réduction beta en moment >0
        '   TempFs      [E] :   Température dans la semelle supérieure
        '   TempFi      [E] :   Température dans la semelle inférieure
        '   TempW       [E] :   Température de l'âme
        '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   zTran       [E] :   Position de chaque tranche
        '   eTran       [E] :   Epaisseur de chaque tranche
        '   TempC       [E] :   Température de chaque tranche
        '   MplRd       [S] :   Table des moments plastiques au droit des noeuds du modèle
        '   zANP        [S] :   Table des position des ANP
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim BeffPrec As Decimal = -1
        Dim iNode As Integer
        Dim Eta As Decimal = 1      '#ALERTE : à adapter sur chaque section
        'Dim Beta As Decimal
        Dim myEN1994 As New cls_Eurocodes
        Dim zTop As Decimal
        Dim lGene1 As Boolean = myBeam.Param.lGeneration1
        Dim Nuance As String = myBeam.Section.Acier.Nuance
        ' Dim lOKPl As Boolean
        Dim kReducYFs As Decimal                ' Coefficient réduction limite d'élasticité en fct température de la semelle sup
        Dim kReducYFi As Decimal                ' Coefficient réduction limite d'élasticité en fct température de la semelle inf
        Dim kReducYW As Decimal                 ' Coefficient réduction limite d'élasticité en fct température de l'âme
        Dim kReducC() As Decimal                ' Coefficient de réduction de Fc dans chaque tranche
        Dim EN_Feu As New cls_EurocodesFeu
        Dim lBetonL As Boolean = myBeam.Dalle.beton.lLeger

        '--> Initialisation

        ReDim MplRd(myBeam.Nodes.nbNodes - 1)
        ReDim zANP(myBeam.Nodes.nbNodes - 1)
        zTop = myBeam.Dalle.zTop

        kReducYFs = EN_Feu.ReducFyAcier(TempFs)
        kReducYFi = EN_Feu.ReducFyAcier(TempFi)
        kReducYW = EN_Feu.ReducFyAcier(TempW)

        ReDim kReducC(NbTranches - 1)

        For iTr As Integer = 0 To NbTranches - 1
            kReducC(iTr) = EN_Feu.ReducFckBeton(TempC(iTr), lBetonL)
        Next

        '--> Boucle sur les noeuds pour récupérer le moment plastique 

        For iNode = 0 To myBeam.Nodes.nbNodes - 1

            If IsEqual(Beff(iNode), BeffPrec) Then
                MplRd(iNode) = MplRd(iNode - 1)
                zANP(iNode) = zANP(iNode - 1)
            Else
                If Signe > 0 Then
                    Me.MomentPlastiquePlus(myBeam.Section, myBeam.Dalle, myBeam.ParamFeu, myBeam.Param.Gamma,
                                           Beff(iNode), kReducYFs, kReducYFi, kReducYW,
                                           NbTranches, zTran, eTran, kReducC, MplRd(iNode), zANP(iNode))
                Else
                End If

                BeffPrec = Beff(iNode)

            End If

        Next

    End Sub

    Private Sub MomentPlastiquePlus(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                    Beff As Decimal, reducKyFs As Decimal, reducKyFi As Decimal, reducKyW As Decimal,
                                    NbTranches As Integer, zTran() As Decimal, eTran() As Decimal, kRedCTr() As Decimal,
                                    ByRef MplRd As Decimal, ByRef zANP As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '--------------------------------------------------------------------------------------------------------------------------
        '   mySection   [E] :   Section
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   lMixte      [E] :   Indique si mixité avec la dalle
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   reducKyFs   [E] :   Réduction de la limite d'élasticité de la semelle sup
        '   reducKyFi   [E] :   Réduction de la limite d'élasticité de la semelle inf
        '   reducKyW    [E] :   Réduction de la limite d'élasticité de l'âme
        '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   zTran       [E] :   Position de chaque tranche
        '   eTran       [E] :   Epaisseur de chaque tranche
        '   kRedCTr     [E] :   Réduction de Fc dans chaque tranche
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
        Const lMixte As Boolean = True
        Dim nEqDalle As Decimal = 1

        '--> Initialisation

        Hw = mySection.ProfilA.HauteurAmeHw
        FySup = mySection.FySup
        FyInf = mySection.FyInf
        FyW = mySection.FyW

        '--> Modélisation du profilé acier

        myModele.MaillageProfileA_YY(Gammas.GammaM_fi, RhoV, mySection.ProfilA, reducKyFs * FySup, reducKyFi * FyInf, reducKyW * FyW, 0)

        ''--> Dalle béton

        If lMixte And (Beff > 0) Then

            '# Dalle 

            myModele.MaillageDalleTranches(Gammas.GammaC_fi, myOptions.AlphaSlab, Beff, nEqDalle, myDalle, NbTranches, zTran, eTran, kRedCTr)

        End If

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

#End Region


#Region " Vérification de la résistance en section "

    Private Sub RunCritereFlexionMixte(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer, MEd(,) As Decimal,
                                       MplRdP() As Decimal, MplRdM() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant d'une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de calcul
        '   MEd         [E] :   Table des moments fléchissants le long de la barre
        '   MplRdP      [E] :   Moments résitants plastiques sous M>0
        '   MPlRdM      [E] :   Moments résitants plastiques sous M<0
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim MRd As Decimal
        Const SIGNEM As Decimal = 1

        '--> Initialisation

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

                    If MEd(iNode, k) * SIGNEM > 0 Then
                        MRd = MplRdP(iNode)
                    Else
                        MRd = MplRdM(iNode)
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

#End Region

End Class
