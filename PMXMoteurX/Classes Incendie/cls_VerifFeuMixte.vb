Public Class cls_VerifFeuMixte

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}              ' En minutes

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereVb() As cls_Critere                   ' Resistance effort tranchant voilement cisaillement
    Public CritereMV() As cls_Critere                   ' Résistance à l'interacion MV

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps

    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Public TempFsStep() As Decimal                      ' Température de la semelle supérieure pour les Steps
    Public TempFiStep() As Decimal                      ' Température de la semelle inférieure pour les Steps
    Public TempWStep() As Decimal                       ' Température de l'âme pour les Steps
    Public TempVStep() As Decimal                       ' Température des connecteurs pour les Steps (partie acier)
    Public TempVcStep() As Decimal                      ' Température des connecteurs pour les Steps (partie béton)

    Public TempDalleStep(,) As Decimal                  ' Température des deux faces de la dalle pour les Steps
    Public TempArmaStep()() As Decimal                  ' Température des lits d'armature

    Public ElancementW As Decimal                       ' Elancement de l'âme 
    Public ElancementWMax As Decimal                    ' Limite d'elancement de l'âme pour le voilement par cisaillement

    Dim iTraArmaAxe() As Integer                        ' Donne la tranche de la dalle dans laquelle se trouve chaque axe des armatures
    Dim kTempArma(,) As Decimal                         ' Proportion de la température de la tranche à considérer pour le calcul de la température moyenne de l'armature

    Dim MethodTempArma As cls_OptionsFeu.enuTypeInterpoleTempArma

    Public TempFSInter As List(Of Decimal)              ' Températures de la semelle sup (ou de la section complète) pour les intervalles de temps
    Public TempFIInter As List(Of Decimal)              ' Températures de la semelle inf pour les intervalles de temps
    Public TempWInter As List(Of Decimal)               ' Températures de l'âme pour les intervalles de temps
    Public TempDInter(1) As List(Of Decimal)            ' Températures aux fibres extrêmes de la dalle pour les intervalles de temps
    Public TimeInter As Decimal = 120                   ' Intervalle de temps (en secondes) pour l'enregistrement de TempA

#End Region

#Region " Constructeurs "

    Public Sub New(pMethodTempArma As cls_OptionsFeu.enuTypeInterpoleTempArma)
        Me.NbStep = cls_VerifFeuMixte.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
        MethodTempArma = pMethodTempArma
    End Sub

    Private Sub InitialiseClassePourCalcul(lMulti As Boolean, NbNodes As Integer, NbCombi As Integer,
                                           IndDerniereT As Integer, nbArma As Integer, lDalleFEM As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   lMulti      [E] :   Indique si poutre à multiple travée
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '   NbArma      [E] :   Nombre de lits d'armatures
        '   lDalleFEM   [E] :   Indique si calcul échauffement par FEM
        '----------------------------------------------------------------------------------------------------------

        ReDim CritereM(Me.NbStep - 1)
        ReDim CritereV(Me.NbStep - 1)
        ReDim CritereVb(Me.NbStep - 1)
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
        ReDim TempVcStep(Me.NbStep - 1)

        ReDim TempDalleStep(Me.NbStep - 1, 1)

        ReDim TempArmaStep(Me.NbStep - 1)
        For i As Integer = 0 To Me.NbStep - 1
            ReDim TempArmaStep(i)(nbArma - 1)
        Next

        Me.TempFSInter = New List(Of Decimal)
        Me.TempFIInter = New List(Of Decimal)
        Me.TempWInter = New List(Of Decimal)

        If lDalleFEM Then
            Me.TempDInter(0) = New List(Of Decimal)
            Me.TempDInter(1) = New List(Of Decimal)
        End If

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

        Dim MplRdP(,) As Decimal = Nothing      ' Moments résistants plastiques le long de la barre
        Dim zANPP(,) As Decimal = Nothing       ' Positions ANP le long de la barre
        Dim MplRdM(,) As Decimal = Nothing      ' Moments résistants plastiques le long de la barre
        Dim zANPM(,) As Decimal = Nothing       ' Positions ANP le long de la barre
        Dim bEff(,) As Decimal = Nothing        ' Largeur efficace de la dalle

        Dim VRd0 As Decimal

        Dim lMulti As Boolean = myBeam.lMultiSpan
        Dim nbArma As Integer = myBeam.Dalle.LitArma.Count
        Dim iArma As Integer

        '--( Paramètres pour la discrétisation de la dalle

        Dim NbTranches As Integer               ' Nombre de tranches discrétisant la dalle
        Dim EpTranche() As Decimal = Nothing    ' Epaisseur de chaque tranche (indice 0 pour la tranche inférieure)
        Dim zTranche() As Decimal = Nothing     ' Position z de la fibre moyenne de chaque tranche
        Dim TempCTranche() As Decimal = Nothing ' Température de chaque tranche
        Dim EpDalle As Decimal                  ' Epaisseur de dalle constante utilisée pour le calcul

        Dim TempCStep As New List(Of Decimal()) ' Temperature pour une time step dans chaque couche

        Dim FEMDalle As New cls_EchauffementDalleFEM    ' Moteur de calcul de l'échauffement de la dalle
        Dim ConvC, ConvCC As Decimal
        Dim TempRef As Decimal                  ' Température de référence, à t=0s
        Dim TeneurU As Decimal                  ' Teneur en eau
        Dim EpsilonF, EpsilonC As Decimal       ' Emissivités feu et béton
        Dim lNormal As Boolean                  ' Indice si béton normal
        Dim lRhoCVar As Boolean                 ' Indique si Rhoc varie avec la température
        Dim RhoC As Decimal                     ' Masse volumique du béton à froid
        Dim lANFrance As Boolean                ' Indique si prise en compte AN française de l'EN 1994-1-2:2005

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
        TempRef = myBeam.ParamFeu.TempRef

        lSsExposee = EN_Feu.SemelleSupExposee(myBeam)
        DeltaT = myBeam.ParamFeu.DeltaTCalcul
        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        lProtege = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
        MassivFs = EN_Feu.MassiveteSemelleSup(myBeam.Section.ProfilA, lSsExposee)
        MassivFi = EN_Feu.MassiveteSemelleInf(myBeam.Section.ProfilA)
        MassivW = EN_Feu.MassiveteAme(myBeam.Section.ProfilA)
        MassivS = EN_Feu.MassiveteSectionAcierBoardP(myBeam.Section.ProfilA)

        Me.TimeInter = CInt((Me.TimeInter / DeltaT)) * DeltaT

        If Not lProtege Then
            kSh = EN_Feu.kShMixte(myBeam.Section.ProfilA)
        End If

        ' ReDim MplRdFeu(Me.NbStep - 1)
        ' ReDim MelRdFeu(Me.NbStep - 1)
        ReDim VplRdFeu(Me.NbStep - 1)

        Me.InitialiseClassePourCalcul(lMulti, myBeam.Nodes.nbNodes, nbCombiELU,
                                      myBeam.IndiceDerniereTravee, nbArma, myBeam.ParamFeu.lDalleFEM)

        If myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            EpDalle = EN_Feu.EpaisseurEfficaceDalleMixte(myBeam.Dalle.Ep_td, myBeam.Dalle.Bac)
        Else
            EpDalle = myBeam.Dalle.EpaisseurActive
        End If

        myBeam.MaillageBeff(lSimple, False, bEff)

        VRd0 = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM_fi, myBeam.Param.EtaW)

        '--( Préparation du maillage de la dalle

        If myBeam.ParamFeu.lDalleFEM Then
            FEMDalle.PrepareMaillageDalleFEM(EpDalle, myBeam.ParamFeu.tDalleEFmax, NbTranches, EpTranche)
        Else
            EN_Feu.PrepareMaillageDalleTabulee(EpDalle, myBeam.Param.lGeneration1, NbTranches, EpTranche, zTranche)
        End If
        If lMulti Then InitialiseCalculTempArma(myBeam.Dalle, NbTranches, EpTranche)

        If myBeam.ParamFeu.lDalleFEM Then
            ReDim TempCTranche(NbTranches - 1)
            For i = 0 To NbTranches - 1
                TempCTranche(i) = TempRef
            Next
            ConvC = myBeam.ParamFeu.ConvectionCoef
            ConvCC = myBeam.ParamFeu.ConvectionCoefDalle
            EpsilonF = myBeam.ParamFeu.EmissivityFire
            EpsilonC = myBeam.ParamFeu.EmissivityC
            lRhoCVar = myBeam.ParamFeu.lRhoCvar
            RhoC = myBeam.Dalle.beton.RhoC
            lANFrance = myBeam.ParamFeu.lANFrance
        End If
        lNormal = Not myBeam.Dalle.beton.lLeger

        '--( Boucle sur TimeSteps

        For iSTep = 0 To Me.NbStep - 1

            TimeTarget = cls_VerifFeuMixte.TimeSteps(iSTep) * kConvMinSec
            lCont = IsSmaller(TimeT, TimeTarget)

            Do While lCont

                '# Boucle sur le temps jusqu'à obtenir la durée cible

                TimeT += DeltaT

                '# Température des gaz chauds

                TempG = EN_Feu.TemperatureGazISO(TimeT)

                '# Calcul de l'échauffement de la section acier sur le pas de temps

                If lProtege Then
                    If lBoard Then
                        TempFs += EN_Feu.DeltaTempAcierProtege(TempFs, TempG, MassivS, TimeT, DeltaT, myBeam.ParamFeu)
                        TempFi = TempFs
                        TempW = TempFs
                    Else
                        TempFs += EN_Feu.DeltaTempAcierProtege(TempFs, TempG, MassivFs, TimeT, DeltaT, myBeam.ParamFeu)
                        TempFi += EN_Feu.DeltaTempAcierProtege(TempFi, TempG, MassivFi, TimeT, DeltaT, myBeam.ParamFeu)
                        TempW += EN_Feu.DeltaTempAcierProtege(TempW, TempG, MassivW, TimeT, DeltaT, myBeam.ParamFeu)
                    End If
                Else
                    TempFs += EN_Feu.DeltaTempAcierNonProtege(TempFs, TempG, MassivFs, kSh, DeltaT, myBeam.ParamFeu)
                    TempFi += EN_Feu.DeltaTempAcierNonProtege(TempFi, TempG, MassivFi, kSh, DeltaT, myBeam.ParamFeu)
                    TempW += EN_Feu.DeltaTempAcierNonProtege(TempW, TempG, MassivW, kSh, DeltaT, myBeam.ParamFeu)
                End If

                '# Calcul de l'échauffement de la dalle sur le pas de temps (cas de la méthode numérique)

                If myBeam.ParamFeu.lDalleFEM Then
                    FEMDalle.Calcul_thermique_Dalle_beton(EpDalle, NbTranches, EpTranche, DeltaT, TempCTranche,
                                                          TempG, TempRef, ConvC, ConvCC, TeneurU, EpsilonF,
                                                          cls_OptionsFeu.BOLTZMANN, EpsilonC, lNormal,
                                                          rhoc, lRhoCVar, lANFrance, lGeneration1)
                End If

                '# 

                lCont = IsSmaller(TimeT, TimeTarget, 10 ^ (-4))

                If IsEqual(TimeT Mod Me.TimeInter, 0) Then
                    Me.TempFSInter.Add(TempFs)
                    If Not (lProtege And lBoard) Then
                        Me.TempFIInter.Add(TempFi)
                        Me.TempWInter.Add(TempW)
                    End If
                    If myBeam.ParamFeu.lDalleFEM Then
                        Me.TempDInter(0).Add(TempCTranche(0))
                        Me.TempDInter(1).Add(TempCTranche(NbTranches - 1))
                    End If
                End If

            Loop

            '# Récupération des températures dans les parties en acier

            TempFsStep(iSTep) = TempFs
            TempFiStep(iSTep) = TempFi
            TempWStep(iSTep) = TempW
            TempVStep(iSTep) = Math.Max(0.8 * TempFs, myBeam.ParamFeu.TempRef)
            TempVcStep(iSTep) = Math.Max(0.4 * TempFs, myBeam.ParamFeu.TempRef)

            '# Calcul des températures dans le béton dans le cas de la méthode tabulée

            If Not myBeam.ParamFeu.lDalleFEM Then
                EN_Feu.TemperatureDalleTabulee(TimeSteps(iSTep), myBeam.Param.lGeneration1, NbTranches, TempCTranche)
            End If

            '# Récupération de la température dans la dalle

            TempDalleStep(iSTep, 0) = TempCTranche(0)
            TempDalleStep(iSTep, 1) = TempCTranche(NbTranches - 1)
            TempCStep.Add(TempCTranche)

            '# Récupération de la température des armatures

            If lMulti Then
                For iArma = 0 To nbArma - 1
                    TempArmaStep(iSTep)(iArma) = TemperatureLitArma(myBeam, iArma, NbTranches, EpTranche, TempCTranche)
                Next
            End If

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

                MaillagePropPlastiquesMixtes(myBeam, bEff, DeltaRd, 1, lGeneration1, False,
                                             TempFsStep(iSTep), TempFiStep(iSTep), TempWStep(iSTep),
                                             NbTranches, zTranche, EpTranche, TempCStep(iSTep), TempArmaStep(iSTep), MplRdP, zANPP)
                MaillagePropPlastiquesMixtes(myBeam, bEff, DeltaRd, -1, lGeneration1, False,
                                             TempFsStep(iSTep), TempFiStep(iSTep), TempWStep(iSTep),
                                             NbTranches, zTranche, EpTranche, TempCStep(iSTep), TempArmaStep(iSTep), MplRdM, zANPM)

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

    Private Sub MaillagePropPlastiquesMixtes(myBeam As cls_Poutre, Beff(,) As Decimal, DeltaRd() As List(Of Decimal), Signe As Decimal, lValRd As Boolean,
                                             lGen1 As Boolean, TempFs As Decimal, TempW As Decimal, TempFi As Decimal,
                                             NbTranches As Integer, zTran() As Decimal, eTran() As Decimal, TempC() As Decimal, TempS() As Decimal,
                                             ByRef MplRd(,) As Decimal, ByRef zANP(,) As Decimal)
        '---------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul des moments plastiques le long de la poutre (sur les noeuds du modèle)
        '---------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   Beff        [E] :   Largeur participante de dalle
        '   DeltaRd     [E] :   Table des valeurs cumulées de la résistance de la connexion le long de la poutre
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
        '   TempS       [E] :   Températures dans les lits d'armature
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
        Dim kReducS() As Decimal                ' Coefficient de réduction pour l'acier des armatures de la dalle
        Dim EN_Feu As New cls_EurocodesFeu
        Dim lBetonL As Boolean = myBeam.Dalle.beton.lLeger
        Dim myDeltaPRd As Decimal

        Dim iTravee As Integer
        Dim iTravD, iTravF As Integer
        Dim iNodeDeb, iNodeFin As Integer
        Dim kDeb, kFin As Integer
        Dim iArma As Integer

        Dim nbLits As Integer = myBeam.Dalle.LitArma.Count
        'Dim ThetaS As Decimal'

        '--> Initialisation

        ReDim MplRd(myBeam.Nodes.nbNodes - 1, 1)
        ReDim zANP(myBeam.Nodes.nbNodes - 1, 1)
        zTop = myBeam.Dalle.zTop

        '# Coeff de réduction pour le profilé
        kReducYFs = EN_Feu.ReducFyAcier(TempFs)
        kReducYFi = EN_Feu.ReducFyAcier(TempFi)
        kReducYW = EN_Feu.ReducFyAcier(TempW)

        '# Coeff de réduction pour le béton de la dalle
        ReDim kReducC(NbTranches - 1)

        For iTr As Integer = 0 To NbTranches - 1
            kReducC(iTr) = EN_Feu.ReducFckBeton(TempC(iTr), lBetonL)
        Next

        '# Coeff de réduction pour les armatures
        ReDim kReducS(nbLits - 1)
        For iArma = 0 To nbLits - 1
            '            ThetaS = Me.TemperatureLitArma(myBeam, iArma, NbTranches, eTran, TempC)
            kReducS(iArma) = EN_Feu.ReducFskArmatures(TempS(iArma), myBeam.ParamFeu.lArmaFormeeAFroid)
        Next

        '--> Boucle sur les noeuds pour récupérer le moment plastique 

        iTravD = myBeam.IndicePremiereTravee
        iTravF = myBeam.IndiceDerniereTravee

        For iTravee = iTravD To iTravF
            iNodeDeb = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeFin = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iNodeDeb To iNodeFin
                If iNode = iNodeDeb Then kDeb = 1 Else kDeb = 0
                If iNode = iNodeFin Then kFin = 0 Else kFin = 1

                myDeltaPRd = DeltaRd(iTravee)(iNode - iNodeDeb)

                For k = kDeb To kFin

                    If Signe > 0 Then
                        Me.MomentPlastiquePlus(myBeam.Section, myBeam.Dalle, myBeam.ParamFeu, myBeam.Param.Gamma,
                                               Beff(iNode, k), myDeltaPRd, kReducYFs, kReducYFi, kReducYW,
                                               NbTranches, zTran, eTran, kReducC, MplRd(iNode, k), zANP(iNode, k))
                    Else

                        Me.MomentPlastiqueMoins(myBeam.Section, myBeam.Dalle, myBeam.ParamFeu, myBeam.Param.Gamma,
                                                Beff(iNode, k), kReducYFs, kReducYFi, kReducYW, kReducS, MplRd(iNode, k), zANP(iNode, k))

                    End If

                Next
            Next

        Next

    End Sub

    Private Sub MomentPlastiqueMoins(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                     Beff As Decimal, reducKyFs As Decimal, reducKyFi As Decimal, reducKyW As Decimal,
                                     kReducS() As Decimal, ByRef MplRd As Decimal, ByRef zANP As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul du moment plastique négatif sous incendie d'une section sans enrobage partiel
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
        '   kReducS     [E] :   Réduction de la limite d'élasticité dans les armatures
        '   MplRd       [S] :   Moment plastique de calcul
        '   zANP        [S] :   Position de l'ANP
        '--------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim myModele As New cls_ModeleP

        Const RhoV As Decimal = 0
        Dim FySup, FyInf, FyW As Decimal
        Dim Hw As Decimal
        Const lMixte As Boolean = True
        Dim zArma, Ztop As Decimal
        Const Signe As Decimal = -1
        Const lValeurRd As Boolean = True

        '--> Initialisation

        Hw = mySection.ProfilA.HauteurAmeHw
        FySup = mySection.FySup
        FyInf = mySection.FyInf
        FyW = mySection.FyW
        Ztop = myDalle.zTop

        '--> Modélisation du profilé acier

        myModele.MaillageProfileA_YY(Gammas.GammaM_fi, RhoV, mySection.ProfilA, reducKyFs * FySup, reducKyFi * FyInf, reducKyW * FyW, 0)

        '--> Dalle béton

        If lMixte And (Beff > 0) Then

            'En moments négatifs, on ne prend pas en compte la dalle

        End If

        '--> Armatures

        If lMixte And (Beff > 0) Then

            For iArma = 0 To myDalle.LitArma.Count - 1

                zArma = Ztop - myDalle.LitArma(iArma).z_s
                myModele.MaillageLitArmaDalle_YY(Gammas.GammaS_fi, Beff, myDalle, iArma, zArma, kReducS(iArma) * myDalle.AcierArmatures.FsK)
            Next

        End If

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Private Sub MomentPlastiquePlus(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                    Beff As Decimal, DeltaPRd As Decimal, reducKyFs As Decimal, reducKyFi As Decimal, reducKyW As Decimal,
                                    NbTranches As Integer, zTran() As Decimal, eTran() As Decimal, kRedCTr() As Decimal,
                                    ByRef MplRd As Decimal, ByRef zANP As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul du moment plastique positif sous incendie d'une section sans enrobage partiel
        '--------------------------------------------------------------------------------------------------------------------------
        '   mySection   [E] :   Section
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   lMixte      [E] :   Indique si mixité avec la dalle
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   DeltaPRd    [E] :   Résistance cumulée de la connexion
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

        '--> Dalle béton

        If lMixte And (Beff > 0) Then

            '# Dalle 

            myModele.MaillageDalleTranches_ETA(Gammas.GammaC_fi, myOptions.AlphaSlab, Beff, nEqDalle, myDalle, DeltaPRd, NbTranches, zTran, eTran, kRedCTr)

        End If

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Private Function TemperatureLitArma(myBeam As cls_Poutre, iArma As Integer,
                                        NbTranches As Integer, eTran() As Decimal, TempC() As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la température d'un lit d'armature dans la dalle
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iArma       [E] :   Inidice du lit d'armature
        '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   eTran       [E] :   Epaisseur de chaque tranche
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iMethod As Integer = 1
        Dim ThetaS As Decimal

        '--( Initialisation

        Select Case iMethod
            Case 0 : ThetaS = Me.TemperatureLitArmaAlAxe(myBeam.Dalle, iArma, NbTranches, eTran, TempC)
            Case 1 : ThetaS = Me.TemperatureLitArmaMoyenne(myBeam.Dalle, iArma, NbTranches, eTran, TempC)
            Case 2 : ThetaS = Me.TemperatureLitArmaAlAxe(myBeam.Dalle, iArma, NbTranches, eTran, TempC)
        End Select

        Return ThetaS

    End Function

    Private Function TemperatureLitArmaMoyenne(myDalle As cls_Dalle, iArma As Integer,
                                               NbTranches As Integer, eTran() As Decimal, TempC() As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la température d'un lit d'armature dans la dalle
        '   Valeur moyenne
        '   Cette routine doit impérativement avoir été précédée par l'initialisation  InitialiseCalculTempArmaMoyenne
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iArma       [E] :   Inidice du lit d'armature
        '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   zTran       [E] :   Position de chaque tranche
        '   eTran       [E] :   Epaisseur de chaque tranche
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iTr As Integer

        Dim ThetaS As Decimal

        '--( Température moyenne

        ThetaS = 0
        For iTr = 0 To NbTranches - 1
            ThetaS += TempC(iTr) * Me.kTempArma(iArma, iTr)
        Next

        '--( Fin

        Return ThetaS

    End Function

    Private Function TemperatureLitArmaAlAxe(myDalle As cls_Dalle, iArma As Integer,
                                             NbTranches As Integer, eTran() As Decimal, TempC() As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la température d'un lit d'armature dans la dalle
        '   Mesurée à l'axe de l'armature ou à la fibre inf de l'armature (pour température maxi)
        '   Cette routine doit impérativement avoir été précédée par l'initialisation  InitialiseCalculTempArmaAxe
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iArma       [E] :   Inidice du lit d'armature
        '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   zTran       [E] :   Position de chaque tranche
        '   eTran       [E] :   Epaisseur de chaque tranche
        '--------------------------------------------------------------------------------------------------------------------------

        Dim ThetaS As Decimal

        '--( Déclaration

        ThetaS = TempC(Me.iTraArmaAxe(iArma))

        Return ThetaS

    End Function

    Private Sub InitialiseCalculTempArma(myDalle As cls_Dalle, nbTranches As Integer, eTran() As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Préparation du calcul des températures dans les armatures
        '-----------------------------------------------------------------------------------------------------------
        '   myDalle     [E] :   Dalle
        '   nbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   eTran       [E] :   Epaisseur des tranches
        '-----------------------------------------------------------------------------------------------------------

        Select Case MethodTempArma
            Case cls_OptionsFeu.enuTypeInterpoleTempArma.Axe
                Me.InitialiseCalculTempArmaAxe(myDalle, nbTranches, eTran)
            Case cls_OptionsFeu.enuTypeInterpoleTempArma.Maximale
                Me.InitialiseCalculTempArmaAxe(myDalle, nbTranches, eTran, True)
            Case cls_OptionsFeu.enuTypeInterpoleTempArma.Moyenne
                Me.InitialiseCalculTempArmaMoyenne(myDalle, nbTranches, eTran)
        End Select

    End Sub

    Private Sub InitialiseCalculTempArmaMoyenne(myDalle As cls_Dalle, NbTranches As Integer, eTran() As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Préparation du calcul des températures dans les armatures
        '   Dans le cas d'un calcul de la température moyenne de la barre (en fct des tranches recoupées)
        '-----------------------------------------------------------------------------------------------------------
        '   myDalle     [E] :   Dalle
        '   nbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   eTran       [E] :   Epaisseur des tranches
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iArma, nbArma As Integer

        Dim iTr As Integer
        Dim AireS As Decimal            ' Aire d'un barre d'armature
        Dim AsTranche() As Decimal      ' Aire d'une barre comprise dans chacune des tranches
        Dim AsTopTranche() As Decimal   ' Aire d'une barre comprise au dessus de la frontière inférieure de la tranche

        Dim zBord As Decimal
        Dim PhiS As Decimal
        Dim zSmin, zSmax As Decimal
        Dim zTop As Decimal
        Dim zArma, DeltaZ As Decimal
        Dim AnglePhi As Decimal

        Dim Acum As Decimal

        '--( Initialisation

        nbArma = myDalle.LitArma.Count

        zTop = myDalle.zTop
        zBord = zTop
        ReDim AsTranche(NbTranches - 1)
        ReDim AsTopTranche(NbTranches - 1)
        ReDim Me.kTempArma(nbArma - 1, NbTranches - 1)

        '--( Boucle sur les lit d'armature

        For iArma = 0 To nbArma - 1
            PhiS = myDalle.LitArma(iArma).PhiS
            AireS = Math.PI * PhiS ^ 2 / 4
            zArma = zTop - myDalle.LitArma(iArma).z_s
            zSmin = zArma - PhiS / 2
            zSmax = zSmin + PhiS
            zBord = zTop

            '##( Recherche des aires d'armatures situées au dessus des frontières de tranches

            For iTr = NbTranches - 1 To 0 Step -1
                zBord -= eTran(iTr)

                If IsSmaller(zBord, zSmax) Then
                    '# La frontière de la tranche est sous la partie sup de l'armature

                    If IsSmallerOrEqual(zBord, zSmin) Then
                        '# La frontière est entièrement sous l'armature
                        AsTopTranche(iTr) = AireS
                    Else
                        '# La tranche comprend seulement une partie de la barre

                        DeltaZ = zBord - zArma
                        AnglePhi = 2 * Math.Acos(DeltaZ / (PhiS / 2))

                        AsTopTranche(iTr) = PhiS ^ 2 * (AnglePhi - Math.Sin(AnglePhi)) / 8

                    End If

                Else
                    '# Aucune partie de l'armature n'est située au dessus de la frontière inf de la tranche
                    AsTopTranche(iTr) = 0
                End If

            Next

            '##( Recherche des aires d'armatures dans chaque tranches

            AsTranche(NbTranches - 1) = AsTopTranche(NbTranches - 1)
            For i As Integer = NbTranches - 2 To 0 Step -1
                AsTranche(i) = AsTopTranche(i)
                For j As Integer = NbTranches - 1 To i + 1 Step -1
                    AsTranche(i) -= AsTranche(j)
                Next
            Next

            '##( Pour contrôle, cumul des aire

            Acum = AsTranche(0)
            For i As Integer = 1 To NbTranches - 1
                Acum += AsTranche(i)
            Next
            If Not IsEqual(Acum, AireS) Then GestionErreur("cls_VerifFeuMixte", "InitialiseCalculTempArmaMoyenne", "Acum<>AireS")

            '##( Coefficient moyen

            For iTr = 0 To NbTranches - 1

                Me.kTempArma(iArma, iTr) = AsTranche(iTr) / AireS

            Next

        Next

    End Sub

    Private Sub InitialiseCalculTempArmaAxe(myDalle As cls_Dalle, NbTranches As Integer, eTran() As Decimal, Optional lMax As Boolean = False)
        '-----------------------------------------------------------------------------------------------------------
        '   08/05/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Préparation du calcul des températures dans les armatures
        '   Dans le cas d'un calcul de la température à l'axe des barres
        '-----------------------------------------------------------------------------------------------------------
        '   myDalle     [E] :   Dalle
        '   nbTranches  [E] :   Nombre de tranches discrétisant la dalle
        '   eTran       [E] :   Epaisseur des tranches
        '   lMax        [E] :   Indique si calcul de la température maxi, auquel cas on cherche le zmini
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iArma, nbArma As Integer
        Dim lCont, lTrouve As Boolean
        Dim zBord, zTop As Decimal
        Dim iTr, iTrArma As Integer
        Dim zArma As Decimal

        '---( Initialisation

        nbArma = myDalle.LitArma.Count
        ReDim Me.iTraArmaAxe(nbArma - 1)
        zTop = myDalle.zTop

        '--( Traitement

        For iArma = 0 To nbArma - 1
            zArma = zTop - myDalle.LitArma(iArma).z_s
            If lMax Then zArma -= myDalle.LitArma(iArma).PhiS / 2
            zBord = zTop
            iTr = NbTranches - 1
            lCont = True
            lTrouve = False
            Do While lCont
                zBord -= eTran(iTr)
                If IsEqual(zBord, zArma) Then
                    iTrArma = iTr - 1
                    lTrouve = True
                ElseIf IsSmaller(zBord, zArma) Then
                    iTrArma = iTr
                    lTrouve = True
                End If
                If Not lTrouve Then
                    iTr -= 1
                    lCont = (iTr >= 0)
                Else
                    lCont = False
                End If
            Loop

            If lTrouve Then Me.iTraArmaAxe(iArma) = iTrArma
        Next

    End Sub

#End Region


#Region " Vérification de la résistance en section "

    Private Sub RunCritereFlexionMixte(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer, MEd(,) As Decimal,
                                       MplRdP(,) As Decimal, MplRdM(,) As Decimal)
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
                        MRd = MplRdP(iNode, k)
                    Else
                        MRd = MplRdM(iNode, k)
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

#Region " Poubelle "

    'Private Function TemperatureLitArmaMoyenne(myDalle As cls_Dalle, iArma As Integer,
    '                                           NbTranches As Integer, eTran() As Decimal, TempC() As Decimal) As Decimal
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   08/05/24 :  Création - POM
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   Calcul de la température d'un lit d'armature dans la dalle
    '    '   Valeur moyenne
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   myBeam      [E] :   Poutre
    '    '   iArma       [E] :   Inidice du lit d'armature
    '    '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
    '    '   zTran       [E] :   Position de chaque tranche
    '    '   eTran       [E] :   Epaisseur de chaque tranche
    '    '--------------------------------------------------------------------------------------------------------------------------

    '    '--( Déclaration

    '    Dim iTr As Integer
    '    Dim AireS As Decimal            ' Aire d'un barre d'armature
    '    Dim AsTranche() As Decimal      ' Aire d'une barre comprise dans chacune des tranches
    '    Dim AsTopTranche() As Decimal   ' Aire d'une barre comprise au dessus de la frontière inférieure de la tranche

    '    Dim zBord As Decimal
    '    Dim PhiS As Decimal
    '    Dim zSmin, zSmax As Decimal
    '    Dim zTop As Decimal
    '    Dim zArma, DeltaZ As Decimal
    '    Dim AnglePhi As Decimal

    '    Dim ThetaS As Decimal
    '    Dim Acum As Decimal

    '    '--( Initialisation

    '    zTop = myDalle.zTop
    '    zBord = zTop
    '    ReDim AsTranche(NbTranches - 1)
    '    ReDim AsTopTranche(NbTranches - 1)
    '    PhiS = myDalle.LitArma(iArma).PhiS
    '    zArma = zTop - myDalle.LitArma(iArma).z_s
    '    zSmin = zArma - PhiS / 2
    '    zSmax = zSmin + PhiS
    '    AireS = Math.PI * PhiS ^ 2 / 4

    '    '--( Recherche des aires d'armatures situées au dessus des frontières de tranches

    '    For iTr = NbTranches - 1 To 0 Step -1
    '        zBord -= eTran(iTr)

    '        If IsSmaller(zBord, zSmax) Then
    '            '# La frontière de la tranche est sous la partie sup de l'armature

    '            If IsSmallerOrEqual(zBord, zSmin) Then
    '                '# La frontière est entièrement sous l'armature
    '                AsTopTranche(iTr) = AireS
    '            Else
    '                '# La tranche comprend seulement une partie de la barre

    '                DeltaZ = zBord - zArma
    '                AnglePhi = 2 * Math.Acos(DeltaZ / (PhiS / 2))

    '                AsTopTranche(iTr) = PhiS ^ 2 * (AnglePhi - Math.Sin(AnglePhi)) / 8

    '            End If

    '        Else
    '            '# Aucune partie de l'armature n'est située au dessus de la frontière inf de la tranche
    '            AsTopTranche(iTr) = 0
    '        End If

    '    Next

    '    '--( Recherche des aires d'armatures dans chaque tranches

    '    AsTranche(NbTranches - 1) = AsTopTranche(NbTranches - 1)
    '    For i As Integer = NbTranches - 2 To 0 Step -1
    '        AsTranche(i) = AsTopTranche(i)
    '        For j As Integer = NbTranches - 1 To i + 1 Step -1
    '            AsTranche(i) -= AsTranche(j)
    '        Next
    '    Next

    '    '--( Pour contrôle, cumul des aire

    '    Acum = AsTranche(0)
    '    For i As Integer = 1 To NbTranches - 1
    '        Acum += AsTranche(i)
    '    Next

    '    '--( Température moyenne

    '    ThetaS = 0
    '    For iTr = 0 To NbTranches - 1
    '        ThetaS += TempC(iTr) * AsTranche(iTr)
    '    Next
    '    ThetaS = ThetaS / AireS

    '    '--( Fin

    '    Return ThetaS

    'End Function

    'Private Function TemperatureLitArmaAlAxe(myDalle As cls_Dalle, iArma As Integer,
    '                                        NbTranches As Integer, eTran() As Decimal, TempC() As Decimal) As Decimal
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   08/05/24 :  Création - POM
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   Calcul de la température d'un lit d'armature dans la dalle
    '    '   Mesurée à l'axe de l'armature
    '    '--------------------------------------------------------------------------------------------------------------------------
    '    '   myBeam      [E] :   Poutre
    '    '   iArma       [E] :   Inidice du lit d'armature
    '    '   NbTranches  [E] :   Nombre de tranches discrétisant la dalle
    '    '   zTran       [E] :   Position de chaque tranche
    '    '   eTran       [E] :   Epaisseur de chaque tranche
    '    '--------------------------------------------------------------------------------------------------------------------------

    '    '--( Déclaration

    '    Dim zTop As Decimal
    '    Dim zArma As Decimal
    '    Dim PhiS As Decimal

    '    Dim lCont, lTrouve As Boolean
    '    Dim iTr As Integer
    '    Dim zBord As Decimal
    '    Dim iTrArma As Integer
    '    Dim ThetaS As Decimal

    '    '--( Initialisation

    '    zTop = myDalle.zTop
    '    zArma = myDalle.LitArma(iArma).z_s
    '    PhiS = myDalle.LitArma(iArma).PhiS

    '    '--( Température à l'axe 

    '    zBord = zTop
    '    iTr = NbTranches - 1
    '    lCont = True
    '    lTrouve = False
    '    Do While lCont
    '        zBord -= eTran(iTr)
    '        If IsEqual(zBord, zArma) Then
    '            iTrArma = iTr - 1
    '            lTrouve = True
    '        ElseIf IsSmaller(zBord, zArma) Then
    '            iTrArma = iTr
    '            lTrouve = True
    '        End If
    '        If Not lTrouve Then
    '            iTr -= 1
    '            lCont = (iTr >= 0)
    '        Else
    '            lCont = False
    '        End If
    '    Loop
    '    If lTrouve Then
    '        ThetaS = TempC(iTrArma)
    '    End If

    '    Return ThetaS

    'End Function

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

#End Region

End Class
