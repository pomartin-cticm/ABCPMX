Public Class cls_VerificationSlimAcier
    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION DES POUTRES SLIMFLOOR ACIER
    '=========================================================================================================

#Region " Attributs "

    Public CritereM As cls_Critere                  ' Resistance à la flexion
    Public CritereMY As cls_Critere                 ' Resistance à la flexion transversale dans le cas d'un calcul élastique
    Public CritereV As cls_Critere                  ' Resistance effort tranchant
    Public CritereMV As cls_Critere                 ' Résistance à l'interacion MV
    Public CritereSigmaA As cls_Critere             ' Critère de résistance en flexion  / Contrainte normale dans le profilé
    Public CritereTauA As cls_Critere               ' Critère de contrainte de cisaillement élastique
    Public CritereSigmaVM As cls_Critere            ' Critère de contrainte élastique équivalente de Von Mises

    Public RhoV As Decimal(,)                       ' Coefficient d'interaction : 1er indice: indice de la combinaison, 2eme indice: indice du noeud

    '== Pour tous les coefficents de réduction ci-dessous, : 1er indice: indice de la combinaison, 2eme indice: indice du noeud
    Public Psi_fi As Decimal(,)                     ' Coefficient de réduction de la semelle inférieure (aire, méthode 1)
    Public rho_t_fi As Decimal(,)                   ' Coefficient de réduction de la semelle inférieure (épaisseur, méthode 2)
    Public Psi_y_fi As Decimal(,)                   ' Coefficient de réduction de la semelle inférieure (limite d'élasticité, méthode 3)
    Public Psi_spd As Decimal(,)                    ' Coefficient de réduction du plat soudé inférieur (aire, méthode 1)
    Public rho_t_spd As Decimal(,)                  ' Coefficient de réduction du plat soudé inférieur (épaisseur, méthode 2)
    Public Psi_y_spd As Decimal(,)                  ' Coefficient de réduction du plat soudé inférieur (limite d'élasticité, méthode 3)

    Public lCalculPlastic As Boolean                ' Indique si le dimensionnement est suivant la théorie plastique

    '==( Classe pour le calcul des contraintes de cisaillement en calcul élastique imposé

    Dim Tau As cls_Tau

    '==( Classe pour le caclul des flux de cisaillement dans les soudures des PRS

    Dim FluxF As cls_Flux

    '==( Tableaux pour le calcul des contraintes locales dans les supports de dalle

    '==( Gorge de la soudure plat / section

    Public aWPlat As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New()
        lCalculPlastic = True
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer, lElastic As Boolean, lElastiTau As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '   lElastic    [E] :   Cas d'un dimensionnement élastique VM en flexion
        '   lElasticTau [E] :   Cas d'un dimensionnement élastique VM en cisaillement
        '----------------------------------------------------------------------------------------------------------

        Me.CritereM = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereMV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereV = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Me.CritereMY = New cls_Critere(NbNodes, NbCombi, IndDerniereT)

        If lElastic Then
            InitialiseCriteresVM(NbNodes, NbCombi, IndDerniereT)
        End If

        If lElastiTau Then
            Me.CritereTauA = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereSigmaVM = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        End If

    End Sub

    Private Sub InitialiseCriteresVM(NbNodes As Integer, nbCombi As Integer, IndDerniereT As Integer)
        '-------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '-------------------------------------------------------------------
        '   Initialisation des critères pour les contraintes normales
        '-------------------------------------------------------------------

        Me.CritereSigmaA = New cls_Critere(NbNodes, nbCombi, IndDerniereT)

    End Sub

    Private Sub DimensionTableauxContraintesLocales(nbNodes As Integer, ByRef SigmaY(,) As Decimal, ByRef TauY(,) As Decimal)
        '-------------------------------------------------------------------
        '   31/07/25 :  Création - POM
        '-------------------------------------------------------------------
        '   Dimension des tableaux de contraintes locales dans les plats support de dalle
        '-------------------------------------------------------------------

        ReDim SigmaY(nbNodes - 1, 1)
        ReDim TauY(nbNodes - 1, 1)

    End Sub

#End Region

#Region " Calcul des contraintes de locales "

    Private Sub ContraintesLocales(myBeam As cls_Poutre, QLinEd() As Decimal, ByRef SigmaY(,) As Decimal, ByRef TauY(,) As Decimal)
        '-------------------------------------------------------------------
        '   31/07/25 :  Création - POM
        '-------------------------------------------------------------------
        '   Calcul des contraintes locales dans les plats
        '   support de la dalle
        '-------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   QLinEd      [E] :   Charges locales réparties aux noeuds
        '   SigmaY      [S] :   Contrainte de flexion transversale dans les plats
        '   TauY        [S] :   Contrainte de cisaillement transversale dans les plats
        '-------------------------------------------------------------------

        '--( Déclaration

        Dim lRive As Boolean
        Dim kQloc As Decimal
        Dim iNode As Integer
        Dim tFi, tPlat As Decimal
        Dim WFi, WPlat As Decimal
        Dim dbtFi, dbtPlat As Decimal
        Dim dApp As Decimal
        Dim kFi, kPlat As Decimal

        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        'lRive = Not myBeam.lIntermediaire
        'If lRive Then kQloc = 1 Else kQloc = 1 / 2
        kQloc = CoefficientCharge(myBeam)

        dApp = Me.LargeurAppui(myBeam)
        Me.BrasLevier(myBeam, dApp, dbtFi, dbtPlat)

        tPlat = myBeam.Section.ProfilA.Plat_t
        tFi = myBeam.Section.ProfilA.Tfi

        WPlat = tPlat ^ 2 / 6
        WFi = tFi ^ 2 / 6

        Select Case myBeam.Section.TypeSection
            Case cls_Section.Enum_TypeSection.IFB_A
                '** Contraintes dans la semelle inférieure
                kFi = 0

                '** Contraintes dans le plat
                kPlat = 1

            Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.SAB
                '** Contraintes dans la semelle inférieure
                kFi = 1

                '** Contraintes dans le plat
                kPlat = 0

            Case cls_Section.Enum_TypeSection.SFB

                '** Contraintes dans la semelle inférieure
                kFi = 1

                '** Contraintes dans le plat
                kPlat = 1

        End Select

        '--( Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                '** Contraintes dans la semelle inférieure

                SigmaY(iNode, 0) = kFi * kQloc * QLinEd(iNode) * dbtFi / WFi / kConvMPaPa
                TauY(iNode, 0) = kFi * 3 / 2 * kQloc * QLinEd(iNode) / tFi / kConvMPaPa

                '** Contraintes dans le plat

                SigmaY(iNode, 1) = kPlat * kQloc * QLinEd(iNode) * dbtPlat / WPlat / kConvMPaPa
                TauY(iNode, 1) = kPlat * 3 / 2 * kQloc * QLinEd(iNode) / tPlat / kConvMPaPa

            Next
        Next

    End Sub


#End Region

#Region "===Outils de vérification==="

    Public Sub Z_VerificationELU(myBeam As cls_Poutre, lConstructionPhase As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU d'une poutre acier slim floor
        '----------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre vérifiée
        '   lConstructionPhase  [E] :   Indique si vérification d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim VplRd As Decimal                            ' Résistance plastique au cisaillement
        Dim VRd As Decimal                              ' Résistance à l'effort tranchant (soit plastique, soit voilement)
        Dim iCombi As Integer
        Dim combiELU As New cls_Combinaisons
        Dim nbCombiELU As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim QEd() As Decimal = Nothing                  ' tableau des forces nodales
        Dim QSupEd() As Decimal = Nothing               ' tableau des forces nodales / unité longueur
        Dim MplRd(,) As Decimal = Nothing
        Dim zANP(,) As Decimal = Nothing
        Dim MelRd(,) As Decimal = Nothing
        Dim zANE(,) As Decimal = Nothing
        Dim zANPMV(,) As Decimal = Nothing                ' Position ANP, tenant compte de l'interaction avec l'effort tranchant 
        Dim MVRd(,) As Decimal = Nothing               ' Moment plastique, tenant compte de l'interaction avec l'effort tranchant 
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim ClasseP, ClasseM As Integer 'Classes de la section en flexion positive et négative
        Dim lClasse4 As Boolean
        Dim InertieY_SectionBrute, zANE_SectionBrute, MelRd_SectionBrute, zANP_SectionBrute, MplRd_SectionBrute As Decimal 'Position des axes neutres uniquement pour le calcul de la classe 
        Dim SigmaELU(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELU
        Dim SigmaCas(,,,) As Decimal = Nothing          ' Contraintes normales pour les cas de charges
        Dim TauELU(,,) As Decimal = Nothing             ' Contraintes de cisaillement sous 1 combinaison ELU
        Dim TauCas(,,,) As Decimal = Nothing            ' Contraintes de cisaillement pour les cas de charges
        Dim FluxCas(,,,) As Decimal = Nothing           ' Flux de cisaillement dans les soudures de PRS par cas de charges
        Dim FluxELU(,,) As Decimal = Nothing            ' Flux de cisaillement dans les soudures de PRS aux ELU
        Dim lSoudure As Boolean                         ' Indique si un calcul de soudure est nécessaire 

        '**     **  Dans ces tableaux, indice 2 iNode, indice 3 0 pour la semelle, 1 pour le plat

        Dim SigmaY(,) As Decimal = Nothing              ' Contraintes locale de flexion dans les plats supports de dalle
        Dim TauY(,) As Decimal = Nothing                ' Contrainte locale de cisaillement dans les plats support de dalle


        Dim lRetraitElastique As Boolean = True
        Dim lVerifElastic As Boolean                    ' Indique si on doit effectuer une verification élastique des sections
        Const lWEB As Boolean = True
        Dim lRec As Boolean

        '--> Initialisations

        '# Critères

        If lConstructionPhase Then
            nbCombiELU = myBeam.CombiA_ELCU.nbCombi     ' cls_Poutre.nbCombELUConstruction
            combiELU = myBeam.CombiA_ELCU
        Else
            nbCombiELU = myBeam.CombiA_ELU.nbCombi      'cls_Poutre.nbCombELU
            combiELU = myBeam.CombiA_ELU
        End If

        Me.InitialiseCoeffReduc(nbCombiELU, myBeam.Nodes.nbNodes)
        Me.InitialiseRhoV(nbCombiELU, myBeam.Nodes.nbNodes)

        '# Tranchant résistant

        VplRd = myBeam.Section.VplRd(myBeam.Param.Gamma.GammaM0, myBeam.Param.EtaW)

        '# Résistance à l'effort tranchant

        VRd = VplRd

        '# Propriétés élastiques section brutes

        myBeam.Section.ProprietesElastiquesMyy_Slim(1, True, myBeam.Param.Gamma, zANE_SectionBrute, InertieY_SectionBrute, MelRd_SectionBrute)

        '# Propriétés plastiques section brutes

        myBeam.Section.ProprietesPlastiquesMyy_Slim(1, True, myBeam.Param.Gamma, 0, zANP_SectionBrute, MplRd_SectionBrute)

        '# Classes de la section

        Dim lBeton As Boolean = myBeam.Section.lSlimFloor And (Not lConstructionPhase)

        ClasseP = myBeam.Section.ClasseSection(zANP_SectionBrute, zANE_SectionBrute, True, lBeton, False, lGeneration1, False, False, lWEB, lRec)
        ' ClasseM = myBeam.Section.ClasseSection(zANP_SectionBrute, zANE_SectionBrute, False, myBeam.Section.lSlimFloor, myBeam.Section.lEnrobage, lGeneration1, False, False, lWEB, lrec)

        '# Type de vérification pour les sections

        lVerifElastic = myBeam.Param.lElasticDesignVM Or (ClasseP > 2)

        'If myBeam.lMultiSpan Then
        '    '# dans le cas d'une poutre à plusieurs travées, on prend aussi en compte la classe de section en flexion négative
        '    lVerifElastic = lVerifElastic Or (ClasseM > 2)
        'End If
        Me.lCalculPlastic = Not lVerifElastic

        If lVerifElastic Then DimensionTableauxContraintesLocales(myBeam.Nodes.nbNodes, SigmaY, TauY)

        '# Initialisation des critères dépendant du type de vérification

        Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee, lVerifElastic, myBeam.Param.lElasticDesignVM)

        '# Contraintes normales

        If lVerifElastic Then
            myBeam.PtsSigma.Initialise(myBeam)
            myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaCas)
        End If

        '# Contraintes de cisaillement
        If myBeam.Param.lElasticDesignVM Then
            Me.Tau = New cls_Tau(myBeam.Section.TypeSection)
            Me.Tau.Initialise(myBeam.Section.ProfilA)
            Me.Tau.CalculContraintesCharges(myBeam, TauCas)
        End If

        ''# Flux de cisaillement des PRS

        'With myBeam.Section.ProfilA

        '    lSoudure = False

        '    Select Case .typeProfileAcier
        '        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
        '            lSoudure = True
        '            ReDim GorgesSouduresMini(1)
        '            ReDim GorgesSoudures(1)

        '        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
        '            lSoudure = True
        '            ReDim GorgesSouduresMini(0)
        '            ReDim GorgesSoudures(0)

        '    End Select

        '    If lSoudure Then
        '        myBeam.Section.ProfilA.InitialiseSoudureMini(Me.GorgesSouduresMini)
        '        Me.FluxF = New cls_Flux
        '        Me.FluxF.InitialiseCalculAcier(myBeam)
        '        Me.FluxF.CalculFluxChargesACIER(myBeam, FluxCas)
        '    End If

        'End With

        '--> Boucle sur les combinaisons

        For iCombi = 0 To combiELU.nbCombi - 1

            '# Combinaisons des moments

            combiELU.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '# Combinaison des efforts tranchants

            combiELU.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '# Recupération des efforts nodaux à partir des tranchants combinés

            combiELU.RecupererEffortsNodauxPonderees(myBeam.Nodes, VEd, QEd, QSupEd)

            '# Calcul des coefficients de réduction 

            CalculCoefficientsReduction(iCombi, myBeam, QEd) 'GUD: penser à faire la différence en fonction des largeurs participantes 

            '# Propriétés réduites

            myBeam.ProprietesVerifSlimFloorAcier(iCombi, myBeam, True, MplRd, zANP, MelRd, zANE,
                                                 Psi_fi, rho_t_fi, Psi_y_fi, Psi_spd, rho_t_spd, Psi_y_spd)

            '# Combinaisons des contraintes

            If lVerifElastic Then
                '( Contraintes normales
                combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, myBeam.PtsSigma.zPos.Count, myBeam.Nodes.nbNodes,
                                                    myBeam.ChargesA, SigmaCas, lRetraitElastique, SigmaELU)

                '( Contraintes de cisaillement
                If myBeam.Param.lElasticDesignVM Then
                    combiELU.CombineContraintes(iCombi, myBeam.ChargesA.Count, Me.Tau.MStatic.Count, myBeam.Nodes.nbNodes,
                                                        myBeam.ChargesA, TauCas, lRetraitElastique, TauELU)
                End If

                '( Contraintes locales dans les plats supports

                ContraintesLocales(myBeam, QSupEd, SigmaY, TauY)

            End If

            '# Vérification sous moment fléchissant

            If lVerifElastic Then
                RunCritereFlexionResistanceElastiqueVM(myBeam, iCombi, SigmaELU)
                RunCritereResistanceElastiquePlatY(myBeam, iCombi, SigmaELU, SigmaY, TauY)
            Else
                Me.RunCritereFlexionAcier(myBeam, iCombi, MEd, MplRd, MelRd, ClasseP, ClasseM, lClasse4)
                Me.RunCritereResistancePlastiquePlatY_N(myBeam, iCombi, QSupEd)
                'Me.RunCritereResistancePlastiquePlatY(myBeam, iCombi, QEd)
            End If

            If myBeam.Param.lElasticDesignVM Then 'calcul élastique imposé 

                '# Vérification sous effot tranchants
                Me.RunCritereCisaillementResistanceElastiqueVM(myBeam, iCombi, TauELU)

                '# Vérification sous interaction MV
                Me.RunCritereInteractionMVElastiqueVonMises(myBeam, iCombi, SigmaELU, TauELU)

            Else 'calcul plastique, même pour les sections de classe 3, si le calcul élastique n'est pas imposé

                '# Vérification sous effort tranchant

                Me.RunCritereTranchants(myBeam, iCombi, VEd, VplRd)

                '# Vérification au voilement par cisaillement

                '--> Sans objet

                '# Traitement de l'interaction MV en fonction de la sensibilité au voilement par cisaillement

                '# Calcul du critère d'intéraction rhoV

                Me.CalculRhoV(iCombi, myBeam)

                '# Propriétés avec prise en compte de l'interaction MV

                myBeam.ProprietesVerifMVAcier(iCombi, True, MVRd, zANPMV, Me.RhoV)

                '# Vérification sous interaction MV

                Me.RunCriteresInteractionMV(myBeam, iCombi, MEd, MVRd)

            End If

            '# Vérification au déversement

            'Sans objet

            '# Dimensionnement des soudures plat / section

            Me.CalculSoudures(myBeam, iCombi, QSupEd, VEd, zANE_SectionBrute, InertieY_SectionBrute)

        Next

    End Sub

#End Region

#Region " Largeurs d'appui et bras de levier "

    Private Function LargeurAppui(myBeam As cls_Poutre) As Decimal
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Renvoie la distance entre le centre des charges sur l'appui et le bord de l'appui
        '-----------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim dApp As Decimal
        Dim lDallePleine As Boolean = myBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine

        '--( Traitement

        If myBeam.Param.MethodReducPlatSlim = cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire Then
            dApp = 40 / 1000        '== 40 mm
        Else
            If lDallePleine Then
                dApp = myBeam.Section.LargeurAppuiSlimDallePleine
            Else
                dApp = (2 / 3) * OptionsSlimFloor.Bappmin
            End If
        End If

        Return dApp

    End Function

    Private Sub BrasLevier(myBeam As cls_Poutre, dApp As Decimal, ByRef dbtFi As Decimal, ByRef dbtPlat As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul des bras de levier entre point d'application charge locale et point de calcul des contraintes
        '-----------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre étudiée
        '   dApp        [E] :   Largeur de l'appui de la dalle
        '   dbtFi       [S] :   Bras de levier pour la semelle inférieure (le cas échéant)
        '   dbtPlat     [S] :   Bras de levier pour le plat inférieur (le cas échéant)
        '-----------------------------------------------------------------------------------------------------------------------------

        With myBeam.Section
            Select Case .TypeSection
                Case cls_Section.Enum_TypeSection.IFB_A
                    With .ProfilA
                        dbtFi = 0
                        dbtPlat = (.Plat_b - .Tw) / 2 - dApp
                    End With
                Case cls_Section.Enum_TypeSection.IFB_B, cls_Section.Enum_TypeSection.SAB
                    With .ProfilA
                        dbtFi = (.Bfi - .Tw) / 2 - .Rci - dApp
                        dbtPlat = 0
                    End With
                Case cls_Section.Enum_TypeSection.SFB
                    With .ProfilA
                        dbtFi = (.Bfi - .Tw) / 2 - .Rci
                        dbtPlat = (.Plat_b - .Bfi) / 2 - dApp
                    End With
            End Select
        End With

    End Sub


#End Region

#Region " Calcul coefficient de réduction plat inférieur "

    Private Sub InitialiseCoeffReduc(NbCombi As Integer, NbNodes As Integer)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Initialisation des tableaux pour le calcul des coefficients de réduction
        '   liés à la flexion transversale des semelles inférieures de slim
        '-----------------------------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------------------------
        ReDim Me.Psi_fi(NbCombi - 1, NbNodes - 1)
        ReDim Me.rho_t_fi(NbCombi - 1, NbNodes - 1)
        ReDim Me.Psi_y_fi(NbCombi - 1, NbNodes - 1)

        ReDim Me.Psi_spd(NbCombi - 1, NbNodes - 1)
        ReDim Me.rho_t_spd(NbCombi - 1, NbNodes - 1)
        ReDim Me.Psi_y_spd(NbCombi - 1, NbNodes - 1)
    End Sub

    Public Sub CalculCoefficientsReduction(iCombi As Integer, myPoutre As cls_Poutre, QEd() As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul des coefficients de réduction liés à la flexion transversale des semelles inférieures de slim
        '-----------------------------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison
        '   myPoutre    [E] :   Poutre étudiée
        '   QEd         [E] :   Efforts nodaux de la poutre
        '-----------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim deltaX As Decimal = myPoutre.LongueurTotale / myPoutre.Nodes.nbNodes

        Dim q, dApp, GammaM0 As Decimal
        Dim dbtFi, dbtPlat As Decimal

        Dim FyPlat, fyInf As Decimal

        Dim kCote2, kChargeQ As Decimal

        '--> Initialisation

        iDebT = myPoutre.IndicePremiereTravee
        iFinT = myPoutre.IndiceDerniereTravee

        gammaM0 = myPoutre.Param.Gamma.GammaM0

        dApp = Me.LargeurAppui(myPoutre)
        Me.BrasLevier(myPoutre, dApp, dbtFi, dbtPlat)

        FyPlat = myPoutre.Section.FySpd
        fyInf = myPoutre.Section.FyInf

        'If myPoutre.lIntermediaire Then
        '    kCote2 = myPoutre.EntraxeD2 / (myPoutre.EntraxeD1 + myPoutre.EntraxeD2)
        'Else
        '    kCote2 = 1
        'End If
        'kChargeQ = Math.Max(kCote2, 1 - kCote2)
        kChargeQ = CoefficientCharge(myPoutre)

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myPoutre.Section.ProfilA

                    '== Suggestion pour DeltaX (POM)
                    If iNode = 0 Then
                        deltaX = (myPoutre.Nodes.xGlobal(iNode + 1) - myPoutre.Nodes.xGlobal(iNode)) / 2
                    ElseIf iNode = myPoutre.Nodes.nbNodes - 1 Then
                        deltaX = (myPoutre.Nodes.xGlobal(iNode) - myPoutre.Nodes.xGlobal(iNode - 1)) / 2
                    Else
                        deltaX = (myPoutre.Nodes.xGlobal(iNode + 1) - myPoutre.Nodes.xGlobal(iNode - 1)) / 2
                    End If
                    '====

                    q = kChargeQ * QEd(iNode) / deltaX

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            Select Case myPoutre.Param.MethodReducPlatSlim
                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire
                                    Psi_spd(iCombi, iNode) = CalculPsi(q, .Plat_b, .Plat_t, FyPlat, .Plat_b - 2 * dApp, .Bfi, gammaM0)
                                    Psi_fi(iCombi, iNode) = CalculPsi(q, .Bfi, .Tfi, fyInf, .Bfi, .Tw, gammaM0)

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M2_ReducEpaisseur

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = CalculRhot(q, dbtPlat, .Plat_t, FyPlat, gammaM0)
                                    rho_t_fi(iCombi, iNode) = CalculRhot(q, dbtFi, .Tfi, fyInf, gammaM0)

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M3_ReducLimiteElasticite

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = CalculPsiY(q, dbtPlat, .Plat_t, FyPlat, gammaM0)
                                    Psi_y_fi(iCombi, iNode) = CalculPsiY(q, dbtFi, .Tfi, fyInf, gammaM0)

                            End Select

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            Select Case myPoutre.Param.MethodReducPlatSlim
                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire
                                    Psi_spd(iCombi, iNode) = CalculPsi(q, .Plat_b, .Plat_t, FyPlat, .Plat_b - 2 * dApp, .Tw, gammaM0)
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M2_ReducEpaisseur

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = CalculRhot(q, dbtPlat, .Plat_t, FyPlat, gammaM0)
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M3_ReducLimiteElasticite
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = CalculPsiY(q, dbtPlat, .Plat_t, FyPlat, gammaM0)
                                    Psi_y_fi(iCombi, iNode) = 1

                            End Select

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            Select Case myPoutre.Param.MethodReducPlatSlim
                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = CalculPsi(q, .Bfi, .Tfi, fyInf, .Bfi - 2 * dApp, .Tw, gammaM0)

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M2_ReducEpaisseur

                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = CalculRhot(q, dbtFi, .Tfi, fyInf, gammaM0)

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M3_ReducLimiteElasticite
                                    Psi_spd(iCombi, iNode) = 1
                                    Psi_fi(iCombi, iNode) = 1

                                    rho_t_spd(iCombi, iNode) = 1
                                    rho_t_fi(iCombi, iNode) = 1

                                    Psi_y_spd(iCombi, iNode) = 1
                                    Psi_y_fi(iCombi, iNode) = CalculPsiY(q, dbtFi, .Tfi, fyInf, gammaM0)

                            End Select

                    End Select

                End With

            Next
        Next

    End Sub

    ''' <summary>
    ''' Fonction qui calcul le coefficient de réduction de l'aire du plat inférieur
    ''' </summary>
    ''' <param name="q">charge linéique</param>
    ''' <param name="b">largeur du plat</param>
    ''' <param name="t">épaisseur du plat</param>
    ''' <param name="fy">nuance d'acier du plat</param>
    ''' <param name="e1">distance entre les charges appliquées</param>
    ''' <param name="e2">distance entre les appuis</param>
    ''' <param name="gammaM0">coefficient partiel gammaM0</param>
    ''' <returns></returns>
    Private Function CalculPsi(q As Decimal, b As Decimal, t As Decimal, fy As Decimal,
                               e1 As Decimal, e2 As Decimal, gammaM0 As Decimal) As Decimal
        Dim psi, mu, lambda As Decimal

        mu = (e1 - e2) * q * gammaM0 / (t ^ 2 * fy * kConvMPaPa)

        lambda = 1 - Math.Sqrt(1 - mu)

        'psi = 1 - (mu ^ 2 * t * 3 * Math.Sqrt(3) + lambda * mu * (2 * e1 + e2) - lambda ^ 2 * (e1 - e2)) / (6 * mu * b)
        psi = 1 - (mu * t * 3 * Math.Sqrt(3) + lambda * (2 * e1 + e2)) / (6 * b) - lambda ^ 2 * (e1 - e2) / (6 * mu * b)

        psi = Math.Max(psi, 0) 'minoration par 0 au cas où
        psi = Math.Min(psi, 1) 'majoration par 1 au cas où

        Return psi
    End Function

    ''' <summary>
    ''' Fonction qui calcul le coefficient de réduction de l'épaisseur du plat inférieur
    ''' </summary>
    ''' <param name="q">charge linéique</param>
    ''' <param name="dbt">bras de levier de la charge</param>
    ''' <param name="t">épaisseur du plat inférieur</param>
    ''' <param name="fy">nuance d'acier du plat inférieur</param>
    ''' <param name="gammaM0">coefficient partiel gammaM0</param>
    ''' <returns></returns>
    Private Function CalculRhot(q As Decimal, dbt As Decimal, t As Decimal, fy As Decimal, gammaM0 As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '   xx/xx/24 : Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction de la limite d'élasticité du plat support de slim floor
        '   d'après COSFB technical specifications
        '------------------------------------------------------------------------------------------------------------------
        '   q       [E] :   Charge sur le plat
        '   dbt     [E] :   Bras de levier de la charge
        '   t       [E] :   Epaisseur du plat
        '   GammaM0 [E] :   GammaM0
        '------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mxbt_Ed, mxbt_Rd, eta_m, rho_t As Decimal

        '--( Initialisation

        mxbt_Ed = q * dbt
        mxbt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0) * kConvMPaPa

        '--( Calcul

        eta_m = Math.Min(Math.Abs(mxbt_Ed / mxbt_Rd), 1)

        rho_t = (1 / 2) * (1 + Math.Sqrt(1 - eta_m))

        'rho_t = Math.Max(rho_t, 0) 'minoration par 0 au cas où
        'rho_t = Math.Max(rho_t, 1) 'majoration par 1 au cas où

        Return rho_t
    End Function

    Private Function CalculPsiY(q As Decimal, dbt As Decimal, t As Decimal, fy As Decimal, gammaM0 As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '   xx/xx/24 : Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction de la limite d'élasticité du plat support de slim floor
        '   d'après annexe I de l'EN 1994-1-1:2025
        '------------------------------------------------------------------------------------------------------------------
        '   q       [E] :   Charge sur le plat
        '   dbt     [E] :   Bras de levier de la charge
        '   t       [E] :   Epaisseur du plat
        '   GammaM0 [E] :   GammaM0
        '------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mybt_Ed, mybt_Rd, eta_m, psiY As Decimal

        '--( Initialisation

        mybt_Ed = q * dbt
        mybt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0) * kConvMPaPa

        '--( Calcul

        eta_m = Math.Min(Math.Abs(mybt_Ed / mybt_Rd), 1)

        psiY = (eta_m - Math.Sqrt(eta_m ^ 2 - 16 * eta_m + 16)) / (2 * (eta_m - 2))

        'psiY = Math.Max(psiY, 0) 'minoration par 0 au cas où
        'psiY = Math.Min(psiY, 1) 'majoration par 1 au cas où

        Return psiY

    End Function

#End Region

#Region " Calcul des soudures "

    Private Sub CalculSoudures(myBeam As cls_Poutre, iCombi As Integer, qLinEd() As Decimal, VEd(,) As Decimal, zANE As Decimal, InertieY As Decimal)
        '-----------------------------------------------------------------------------------
        '   08/10/25 : Création - POM - V1.2
        '-----------------------------------------------------------------------------------
        '   Calcul des soudures plats / section
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iCombi      [E] :   Indice de la combinaison traitée
        '   qLinEd      [E] :   Effort linéique agissant DES 2 COTES sur les plats
        '   VEd         [E] :   Efforts tranchants  le long de la poutre
        '   zANE        [E] :   Position AN elastique de la section
        '   InertieY    [E] :   Inertie de la section
        '-----------------------------------------------------------------------------------

        '--( Traitement en fonction du type de section

        Select Case True
            Case myBeam.Section.lSlimFloor_SFB
                Me.CalculSouduresSFB(myBeam, iCombi, qLinEd, VEd, zANE, InertieY)
            Case myBeam.Section.lSlimFloor_IFB_A
                Me.CalculSouduresIFB_A(myBeam, iCombi, qLinEd, VEd, zANE, InertieY)
            Case myBeam.Section.lSlimFloor_IFB_B
                Me.CalculSouduresIFB_B(myBeam, iCombi, qLinEd, VEd, zANE, InertieY)
        End Select

    End Sub

    Private Sub CalculSouduresIFB_A(myBeam As cls_Poutre, iCombi As Integer, qWLinEd() As Decimal, VEd(,) As Decimal,
                                    zANE As Decimal, InertieY As Decimal)
        '-----------------------------------------------------------------------------------
        '   08/10/25 : Création - POM - V1.2
        '-----------------------------------------------------------------------------------
        '   Calcul des soudures plats / section pour une slim IFB_A
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iCombi      [E] :   Indice de la combinaison traitée
        '   qLinEd      [E] :   Effort linéique agissant DES 2 COTES sur les plats
        '   VEd         [E] :   Efforts tranchants  le long de la poutre
        '   zANE        [E] :   Position AN elastique de la section
        '   InertieY    [E] :   Inertie de la section
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbNodes As Integer = myBeam.Nodes.nbNodes
        Dim GammaM2 As Decimal = myBeam.Param.Gamma.GammaM2
        Dim Fu As Decimal
        Dim BetaW As Decimal
        Dim tPl, tW As Decimal
        Dim e1, e2p As Decimal
        Dim kW2 As Decimal
        Dim qLEd As Decimal
        Const kUnitMM As Decimal = 1 / 1000
        Dim tMax As Decimal
        Dim MomSp As Decimal

        '--( Initialisation

        tPl = myBeam.Section.ProfilA.Plat_t
        tW = myBeam.Section.ProfilA.Tw

        Fu = Math.Min(myBeam.Section.Acier.LimiteFu(tW), myBeam.Section.AcierPlat.LimiteFu(tPl))
        BetaW = Math.Max(myBeam.Section.Acier.BetaW, myBeam.Section.AcierPlat.BetaW)

        e1 = myBeam.Section.ProfilA.Plat_b
        e2p = myBeam.Section.ProfilA.Tw

        If myBeam.lIntermediaire Then
            kW2 = myBeam.EntraxeD2 / (myBeam.EntraxeD1 + myBeam.EntraxeD2)
        Else
            kW2 = 1
        End If

        MomSp = myBeam.Section.ProfilA.AirePlat * (zANE + tPl / 2)

        '--( Calcul

        tMax = Math.Max(tW, tPl)

        Me.aWPlat = Math.Max(3 * kUnitMM, (Math.Sqrt(tMax / kUnitMM) - 0.5) * kUnitMM)

        For i As Integer = 0 To nbNodes - 1
            qLEd = Math.Max(Math.Abs(VEd(i, 0)), Math.Abs(VEd(i, 1))) * MomSp / InertieY
            Me.aWPlat = Math.Max(Me.aWPlat, CalculSoudureSlimFloor(BetaW, Fu, GammaM2, qWLinEd(iCombi), qLEd, e1, e2p, kW2))
        Next
    End Sub

    Private Sub CalculSouduresIFB_B(myBeam As cls_Poutre, iCombi As Integer, qWLinEd() As Decimal, VEd(,) As Decimal,
                                    zANE As Decimal, InertieY As Decimal)
        '-----------------------------------------------------------------------------------
        '   08/10/25 : Création - POM - V1.2
        '-----------------------------------------------------------------------------------
        '   Calcul des soudures plats / section pour une slim IFB_B
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iCombi      [E] :   Indice de la combinaison traitée
        '   qLinEd      [E] :   Effort linéique agissant DES 2 COTES sur les plats
        '   VEd         [E] :   Efforts tranchants  le long de la poutre
        '   zANE        [E] :   Position AN elastique de la section
        '   InertieY    [E] :   Inertie de la section
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbNodes As Integer = myBeam.Nodes.nbNodes
        Dim GammaM2 As Decimal = myBeam.Param.Gamma.GammaM2
        Dim Fu As Decimal
        Dim BetaW As Decimal
        Dim tPl, tW As Decimal
        Dim e1, e2p As Decimal
        Dim kW2 As Decimal
        Dim qLEd As Decimal
        Const kUnitMM As Decimal = 1 / 1000
        Dim tMax As Decimal
        Dim MomSp As Decimal

        '--( Initialisation

        tPl = myBeam.Section.ProfilA.Plat_t
        tW = myBeam.Section.ProfilA.Tw

        Fu = Math.Min(myBeam.Section.Acier.LimiteFu(tW), myBeam.Section.AcierPlat.LimiteFu(tPl))
        BetaW = Math.Max(myBeam.Section.Acier.BetaW, myBeam.Section.AcierPlat.BetaW)

        e1 = myBeam.Section.ProfilA.Plat_b
        e2p = myBeam.Section.ProfilA.Tw

        If myBeam.lIntermediaire Then
            kW2 = myBeam.EntraxeD2 / (myBeam.EntraxeD1 + myBeam.EntraxeD2)
        Else
            kW2 = 1
        End If

        MomSp = myBeam.Section.ProfilA.AirePlat * (myBeam.Section.zSemSup - tPl / 2 - zANE)

        '--( Calcul

        tMax = Math.Max(tW, tPl)

        Me.aWPlat = Math.Max(3 * kUnitMM, (Math.Sqrt(tMax / kUnitMM) - 0.5) * kUnitMM)

        For i As Integer = 0 To nbNodes - 1
            qLEd = Math.Max(Math.Abs(VEd(i, 0)), Math.Abs(VEd(i, 1))) * MomSp / InertieY
            Me.aWPlat = Math.Max(Me.aWPlat, CalculSoudureSlimFloor(BetaW, Fu, GammaM2, 0, qLEd, e1, e2p, kW2))
        Next

    End Sub

    Private Sub CalculSouduresSFB(myBeam As cls_Poutre, iCombi As Integer, qWLinEd() As Decimal, VEd(,) As Decimal,
                                  zANE As Decimal, InertieY As Decimal)
        '-----------------------------------------------------------------------------------
        '   08/10/25 : Création - POM - V1.2
        '-----------------------------------------------------------------------------------
        '   Calcul des soudures plats / section pour une slim SFB
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '   iCombi      [E] :   Indice de la combinaison traitée
        '   qLinEd      [E] :   Effort linéique agissant DES 2 COTES sur les plats
        '   VEd         [E] :   Efforts tranchants  le long de la poutre
        '   zANE        [E] :   Position AN elastique de la section
        '   InertieY    [E] :   Inertie de la section
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbNodes As Integer = myBeam.Nodes.nbNodes
        Dim GammaM2 As Decimal = myBeam.Param.Gamma.GammaM2
        Dim Fu As Decimal
        Dim BetaW As Decimal
        Dim tPl, tFi As Decimal
        Dim e1, e2p As Decimal
        Dim kW2 As Decimal
        Dim qLEd As Decimal
        Const kUnitMM As Decimal = 1 / 1000
        Dim tMax As Decimal
        Dim MomSp As Decimal

        '--( Initialisation

        tPl = myBeam.Section.ProfilA.Plat_t
        tFi = myBeam.Section.ProfilA.Tfi

        Fu = Math.Min(myBeam.Section.Acier.LimiteFu(tFi), myBeam.Section.AcierPlat.LimiteFu(tPl))
        BetaW = Math.Max(myBeam.Section.Acier.BetaW, myBeam.Section.AcierPlat.BetaW)

        e1 = myBeam.Section.ProfilA.Plat_b
        e2p = myBeam.Section.ProfilA.Bfi

        If myBeam.lIntermediaire Then
            kW2 = myBeam.EntraxeD2 / (myBeam.EntraxeD1 + myBeam.EntraxeD2)
        Else
            kW2 = 1
        End If

        MomSp = myBeam.Section.ProfilA.AirePlat * (zANE + tPl / 2)

        '--( Calcul

        tMax = Math.Max(tFi, tPl)

        Me.aWPlat = Math.Max(3 * kUnitMM, (Math.Sqrt(tMax / kUnitMM) - 0.5) * kUnitMM)

        For i As Integer = 0 To nbNodes - 1
            qLEd = Math.Max(Math.Abs(VEd(i, 0)), Math.Abs(VEd(i, 1))) * MomSp / InertieY
            Me.aWPlat = Math.Max(Me.aWPlat, CalculSoudureSlimFloor(BetaW, Fu, GammaM2, qWLinEd(iCombi), qLEd, e1, e2p, kW2))
        Next

    End Sub

    Private Function CalculSoudureSlimFloor(BetaW As Decimal, Fu As Decimal, GammaM2 As Decimal,
                                            qWEd As Decimal, qLEd As Decimal, e1 As Decimal, e2p As Decimal, kW2 As Decimal) As Decimal
        '-----------------------------------------------------------------------------------
        '   08/10/25 : Création - POM - V1.2
        '-----------------------------------------------------------------------------------
        '   Calcul des soudures plats / section pour une slim floor (formule générale commune)
        '-----------------------------------------------------------------------------------
        '   BetaW       [E] :   Coefficient de calcul des soudures
        '   Fu          [E] :   Limite ultime de la nuance d'acier
        '   GammaM2     [E] :   Coefficient partiel de sécurité pour les soudures
        '   qWEd        [E] :   Efforts linéiques agissant de part et d'autre de la section
        '   qLEd        [E] :   Flux de cisaillement dans la soudure
        '   e1          [E] :   Distance entre les charges appliquées
        '   e2p         [E] :   Distance entre les appuis (soudures)
        '   kW2         [E] :   Par des efforts qW qui passe sur le côté 2 (droite)
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim aW As Decimal
        Dim Rac2 As Decimal = Math.Sqrt(2)
        Dim qW1, qW2 As Decimal
        Dim q1, q2 As Decimal
        Dim qL As Decimal = qLEd
        Dim qW As Decimal

        '--( Calcul des charges

        q2 = kW2 * qWEd
        q1 = qWEd - q2
        qW1 = 0.5 * (q1 * (e1 + e2p) + q2 * (e1 - e2p)) / e2p
        qW2 = qWEd - qW1
        qW = Math.Max(qW1, qW2)

        '--( Calcul de la soudure

        aW = BetaW * GammaM2 / (Fu * kConvMPaPa * Rac2) * Math.Sqrt(qW ^ 2 + 3 * (qW + Rac2 * qL) ^ 2)

        Return aW

    End Function

#End Region

#Region " Vérification de la poutre acier "

    Private Function CoefficientCharge(myBeam As cls_Poutre) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   10/10/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Renvoie la proportion de charge reprise de part et d'autre
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '----------------------------------------------------------------------------------------------------------

        Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        dGauche = myBeam.EntraxeD1
        dDroite = myBeam.EntraxeD2
        If myBeam.lIntermediaire Then
            kCote = Math.Max(dGauche, dDroite) / (dGauche + dDroite)
        Else
            kCote = Math.Max(2 * dGauche, dDroite) / (2 * dGauche + dDroite)
        End If

        Return kCote

    End Function

    Private Sub RunCritereResistancePlastiquePlatY_N(myBeam As cls_Poutre, iCombi As Integer, qsupEd() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   01/08/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance élasto-plastique des plats supports
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   qsupEd      [E] :   Charge répartie linéique agissant DES 2 COTES
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iNode As Integer
        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        'Dim DeltaX As Decimal
        Dim qLin As Decimal
        Dim myFiEd, myFiRd As Decimal
        Dim myPlEd, myPlRd As Decimal
        Dim GammaM0 As Decimal
        Dim FyPlat, FyInf As Decimal
        Dim dApp, dbtFi, dbtPlat As Decimal
        Const kPlast As Decimal = 1.2
        Dim tPl, tFi As Decimal
        Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        '--( Initialisation

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = Me.LargeurAppui(myBeam)
        Me.BrasLevier(myBeam, dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        FyInf = myBeam.Section.FyInf

        tFi = myBeam.Section.ProfilA.Tfi
        tPl = myBeam.Section.ProfilA.Plat_t

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        'dGauche = myBeam.EntraxeD1
        'dDroite = myBeam.EntraxeD2
        'If myBeam.lIntermediaire Then
        '    kCote = Math.Max(dGauche, dDroite) / (dGauche + dDroite)
        'Else
        '    kCote = Math.Max(2 * dGauche, dDroite) / (2 * dGauche + dDroite)
        'End If

        kCote = CoefficientCharge(myBeam)

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    qLin = qsupEd(iNode) * kCote

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            myFiEd = qLin * dbtFi
                            myFiRd = kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            myPlEd = qLin * dbtPlat
                            myPlRd = kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            myFiEd = qLin * dbtFi
                            myFiRd = kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                    End Select

                End With

            Next
        Next

    End Sub


    Private Sub RunCritereResistancePlastiquePlatY(myBeam As cls_Poutre, iCombi As Integer, QEd() As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   01/08/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance élasto-plastique des plats supports
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   QEd         [E] :   Efforts nodaux de la poutre
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iNode As Integer
        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim DeltaX As Decimal
        Dim qLin As Decimal
        Dim myFiEd, myFiRd As Decimal
        Dim myPlEd, myPlRd As Decimal
        Dim GammaM0 As Decimal
        Dim FyPlat, FyInf As Decimal
        Dim dApp, dbtFi, dbtPlat As Decimal
        Const kPlast As Decimal = 1.2
        Dim tPl, tFi As Decimal

        '--( Initialisation

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = Me.LargeurAppui(myBeam)
        Me.BrasLevier(myBeam, dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        FyInf = myBeam.Section.FyInf

        tFi = myBeam.Section.ProfilA.Tfi
        tPl = myBeam.Section.ProfilA.Plat_t

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    '== Calcul de DeltaX
                    If iNode = 0 Then
                        DeltaX = (myBeam.Nodes.xGlobal(iNode + 1) - myBeam.Nodes.xGlobal(iNode)) / 2
                    ElseIf iNode = myBeam.Nodes.nbNodes - 1 Then
                        DeltaX = (myBeam.Nodes.xGlobal(iNode) - myBeam.Nodes.xGlobal(iNode - 1)) / 2
                    Else
                        DeltaX = (myBeam.Nodes.xGlobal(iNode + 1) - myBeam.Nodes.xGlobal(iNode - 1)) / 2
                    End If
                    '====

                    qLin = QEd(iNode) / DeltaX

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            myFiEd = qLin * dbtFi
                            myFiRd = kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            myPlEd = qLin * dbtPlat
                            myPlRd = kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            myFiEd = qLin * dbtFi
                            myFiRd = kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                    End Select

                End With

            Next
        Next

    End Sub

    Private Sub RunCritereResistanceElastiquePlatY(myBeam As cls_Poutre, iCombi As Integer,
                                                   SigmaELU(,,) As Decimal, SigmaY(,) As Decimal, TauY(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   01/08/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance élastique VM des plats supports par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   SigmaY      [E] :   Contraintes flexion transversale
        '   TauY        [E] :   Contrainte de cisaillement transversal
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim FydInf, FyInf As Decimal
        Dim FydPlat, FyPlat As Decimal

        Dim iPro0 As Integer = myBeam.PtsSigma.iProfile(0)
        Const iPLAT As Integer = 1
        Const iFINF As Integer = 0

        '--> Initialisation

        FyInf = myBeam.Section.FyInf
        FydInf = FyInf / myBeam.Param.Gamma.GammaM0

        FyPlat = myBeam.Section.FySpd
        FydPlat = FyPlat / myBeam.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then

            Select Case myBeam.Section.TypeSection
                Case cls_Section.Enum_TypeSection.IFB_A '==================================================================

                    ' Face supérieure du plat inférieur
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 3, 1, 0, iPLAT, 1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Mi épaisseur du plat inférieur
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 4, 0.5, 0.5, iPLAT, 0, 1.5,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Face inférieure du plat inférieur
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 4, iPro0 + 4, 1, 0, iPLAT, -1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                Case cls_Section.Enum_TypeSection.IFB_B '==================================================================

                    ' Face supérieure de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 3, 1, 0, iFINF, 1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Mi épaisseur de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 4, 0.5, 0.5, iFINF, 0, 1.5,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Face inférieure de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 4, iPro0 + 4, 1, 0, iFINF, -1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)


                Case cls_Section.Enum_TypeSection.SAB   '==================================================================

                    ' Face supérieure de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 3, 1, 0, iFINF, 1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Mi épaisseur de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 4, 0.5, 0.5, iFINF, 0, 1.5,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Face inférieure de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 4, iPro0 + 4, 1, 0, iFINF, -1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                Case cls_Section.Enum_TypeSection.SFB   '==================================================================

                    ' Face supérieure de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 3, 1, 0, iFINF, 1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Mi épaisseur de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 3, iPro0 + 4, 0.5, 0.5, iFINF, 0, 1.5,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Face inférieure de la semelle inférieure
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 4, iPro0 + 4, 1, 0, iFINF, -1, 0,
                                             SigmaELU, SigmaY, TauY, FydInf, Me.CritereMY)

                    ' Face supérieure du plat
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 4, iPro0 + 4, 1, 0, iPLAT, 1, 0,
                                             SigmaELU, SigmaY, TauY, FydPlat, Me.CritereMY)

                    ' Mi épaisseur du plat
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 4, iPro0 + 5, 0.5, 0.5, iPLAT, 0, 1.5,
                                             SigmaELU, SigmaY, TauY, FydPlat, Me.CritereMY)

                    ' Face inférieure du plat
                    RunCritereFlexionTransVM(myBeam, iCombi, iPro0 + 5, iPro0 + 5, 1, 0, iPLAT, -1, 0,
                                             SigmaELU, SigmaY, TauY, FydPlat, Me.CritereMY)

            End Select
        End If

    End Sub

    Private Sub RunCritereFlexionResistanceElastiqueVM(myBeam As cls_Poutre, iCombi As Integer, SigmaELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal
        Dim FydPlat, FyPlat As Decimal

        Dim iPro0 As Integer = myBeam.PtsSigma.iProfile(0)

        '--> Initialisation

        FySup = myBeam.Section.FySup
        FydSup = FySup / myBeam.Param.Gamma.GammaM0
        FyW = myBeam.Section.FyW
        FydW = FyW / myBeam.Param.Gamma.GammaM0
        FyInf = myBeam.Section.FyInf
        FydInf = FyInf / myBeam.Param.Gamma.GammaM0

        FyPlat = myBeam.Section.FySpd
        FydPlat = FyPlat / myBeam.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes dans le profilé

        If (iPro0 > -1) Then

            Select Case myBeam.Section.TypeSection
                Case cls_Section.Enum_TypeSection.IFB_A '==================================================================

                    '( Point 1 - Contrainte face externe de la semelle supérieure
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)

                    '( Point 2 - Contrainte face interne de la semelle supérieure
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)

                    '( Point 3 - Contrainte CdG de la section
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)

                    '( Point 4 - Contrainte face interne de la semelle inférieure (ici un plat)
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydPlat, FydW), Me.CritereSigmaA)

                    '( Point 5 - Contrainte face externe de la semelle inférieure (ici un plat)
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydPlat, Me.CritereSigmaA)

                Case cls_Section.Enum_TypeSection.IFB_B '==================================================================
                Case cls_Section.Enum_TypeSection.SAB   '==================================================================


                Case cls_Section.Enum_TypeSection.SFB   '==================================================================

                    '( Point 1 - Contrainte face externe de la semelle supérieure
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 0, SigmaELU, FydSup, Me.CritereSigmaA)

                    '( Point 2 - Contrainte face interne de la semelle supérieure
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 1, SigmaELU, Math.Min(FydSup, FydW), Me.CritereSigmaA)

                    '( Point 3 - Contrainte CdG de la section
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 2, SigmaELU, FydW, Me.CritereSigmaA)

                    '( Point 4 - Contrainte face interne de la semelle inférieure
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 3, SigmaELU, Math.Min(FydInf, FydW), Me.CritereSigmaA)

                    '( Point 5 - Contrainte face externe de la semelle inférieure
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 4, SigmaELU, FydInf, Me.CritereSigmaA)

                    '( Point 6 - Contrainte face externe du plat inférieur 
                    RunCritereFlexionVM(myBeam, iCombi, iPro0 + 5, SigmaELU, FydInf, Me.CritereSigmaA)

            End Select

        End If

        '# Enveloppe de résistance en flexion

        EnveloppeResistanceFlexionElastique(myBeam, iCombi)

    End Sub

    Private Sub EnveloppeResistanceFlexionElastique(myBeam As cls_Poutre, iCombi As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   13/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Récupère le critère VM dimensionnant en flexion
        '----------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------

        '--< Déclarations

        Dim iTravDeb As Integer, iTravFin As Integer

        '--< Initialisations

        iTravDeb = myBeam.IndicePremiereTravee
        iTravFin = myBeam.IndiceDerniereTravee

        '--< Contraintes dans le profilé acier

        Me.CritereM.CritereMax = Me.CritereSigmaA.CritereMax
        Me.CritereM.iCombiM = Me.CritereSigmaA.iCombiM
        Me.CritereM.iNodeM = Me.CritereSigmaA.iNodeM

        For i As Integer = iTravDeb To iTravFin
            Me.CritereM.CritereCombiT(iCombi, i) = Me.CritereSigmaA.CritereCombiT(iCombi, i)
            Me.CritereM.CritereCombiN(iCombi, i) = Me.CritereSigmaA.CritereCombiN(iCombi, i)
        Next

    End Sub

    Private Sub RunCritereFlexionTransVM(myBeam As cls_Poutre, iCombi As Integer,
                                         iPoint1 As Integer, iPoint2 As Integer, kPoint1 As Decimal, kPoint2 As Decimal,
                                         iPlat As Integer, kSigmaY As Decimal, kTauY As Decimal,
                                         SigmaELU(,,) As Decimal, SigmaY(,) As Decimal, TauY(,) As Decimal,
                                         SigmaU As Decimal, myCrit As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   01/08/25 :  Création - POM - V1.2
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la contrainte de VM locale dans les plats supports de dalle
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint1     [E] :   Indice du point de calcul des contraintes (n°1)
        '   iPoint2     [E] :   Indice du point de calcul des contraintes (n°2)
        '   kPoint1     [E] :   Coef de pondération pour la contrainte du point de calcul n°1
        '   kPoint2     [E] :   Coef de pondération pour la contrainte du point de calcul n°2
        '   iPlat       [E] :   Indice du plat concerné (0 semelle inf, 1 plat)
        '   kSigmaY     [E] :   Coef de pondération de la contrainte sigmaY dans le calcul de la contrainte de VM
        '   kTauY       [E] :   Coef de pondération de la contrainte tauY dans le calcul de la contrainte de VM
        '   SigmaELU    [E] :   Contraintes normales X aux ELU, pour la flexion principale
        '   SigmaY      [E] :   Contraintes de flexion locale dans les plats supports
        '   TauY        [E] :   Contraintes de cisaillement locale dans les plats supports
        '   SigmaU      [E] :   Valeur ultime de la contrainte normale au point iPoint
        '   myCrit      [E] :   Critere pour la contrainte equivalente
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        Dim pTau, pSigmaX, pSigmaY As Decimal
        Dim pSigmaEq As Decimal

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

                    pSigmaX = kPoint1 * SigmaELU(iPoint1, iNode, k) + kPoint2 * SigmaELU(iPoint2, iNode, k)
                    pSigmaY = kSigmaY * SigmaY(iNode, iPlat)
                    pTau = kTauY * TauY(iNode, iPlat)

                    pSigmaEq = Math.Sqrt(pSigmaX ^ 2 + pSigmaY ^ 2 - pSigmaX * pSigmaY + 3 * pTau ^ 2)

                    myCrit.EnregistreCritere(iNode, iCombi, iTravee, pSigmaEq, SigmaU)

                Next
            Next
        Next
    End Sub

    Private Sub RunCritereFlexionVM(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal,
                                    SigmaU As Decimal, MyCritereM As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion par les critères de VonMises en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam  [E] :   Poutre traitée
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

            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1
                For k = iDebK To iFinK
                    MyCritereM.EnregistreCritere(iNode, iCombi, iTravee, SigmaELU(iPoint, iNode, k), SigmaU)
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereFlexionAcier(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal,
                                       MplRd As Decimal(,), MelRd As Decimal(,), ClasseP As Integer, ClasseM As Integer, ByRef lClasse4 As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance au moment fléchissant d'une poutre acier
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
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

            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    If IsGreaterOrEqual(MEd(iNode, k) * SIGNEM, 0) Then

                        Select Case ClasseP
                            Case 1, 2
                                MRd = MplRd(iNode, k)
                            Case 3
                                MRd = MelRd(iNode, k)
                            Case 4
                                lClasse4 = True
                                'lOk = False
                        End Select

                    Else

                        '*** Eliminer ce cas
                        Select Case ClasseM
                            Case 1, 2
                                MRd = MplRd(iNode, k)
                            Case 3
                                MRd = MelRd(iNode, k)
                            Case 4
                                lClasse4 = True
                                'lOk = False
                        End Select

                    End If

                    Me.CritereM.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRd)
                Next
            Next
        Next

    End Sub

    Private Sub RunCritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VRd As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   VEd     [E] :   Table des efforts tranchants le long de la barre
        '   VRd     [E] :   Effort tranchant résistant (plastique ou voilement) de la barre
        '   lBukling[E] :   Indique si critere de résistance au voilement par cisaillement
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
                    Me.CritereV.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
                Next
            Next
        Next

    End Sub

    Private Sub RunCriteresInteractionMV(MyPoutre As cls_Poutre, iCombi As Integer, MEd(,) As Decimal, MVRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   23/01/2023 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'interaction MV (critère de résistance plastique)
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   MEd     [E] :   Table des moments fléchissants le long de la barre
        '   MplRd   [E] :   Table des moments plastiques le long de la barre (calculés en fonction du signe de MEd)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        'Dim Sigma As Decimal
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
                    Me.CritereMV.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MVRd(iNode, k))
                Next
            Next
        Next


    End Sub

#End Region

#Region " Calcul coefficient d'interaction RhoV "

    Private Sub InitialiseRhoV(NbCombi As Integer, NbNodes As Integer)
        ReDim Me.RhoV(NbCombi - 1, NbNodes - 1)
    End Sub

    ''' <summary>
    ''' Fonction qui calcul le coefficient d'interaction en fonction du critèreV = VEd/VRd
    ''' </summary>
    ''' <param name="iCombi">indice de la combinaison en cours</param>
    ''' <param name="MyPoutre">poutre en cours</param>
    Public Sub CalculRhoV(iCombi As Integer, MyPoutre As cls_Poutre)
        '--> Déclaration

        Dim rhoV As Decimal
        Dim critereV As Decimal
        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                critereV = Me.CritereV.Critere(iNode)

                If critereV >= 1 Then
                    rhoV = 1
                ElseIf critereV <= 0.5 Then
                    rhoV = 0
                Else
                    rhoV = (2 * critereV - 1) ^ 2
                End If

                Me.RhoV(iCombi, iNode) = rhoV
            Next
        Next
    End Sub

#End Region

#Region " Vérification des contraintes élastiques de cisaillement "

    Private Sub RunCritereCisaillementResistanceElastiqueVM(MyPoutre As cls_Poutre, iCombi As Integer, TauELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en cisaillement par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam[E] :   Poutre traitée
        '   iCombi  [E] :   Indice de la combinaison
        '   TauELU  [E] :   Contraintes de cisaillement aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim TauY, FyW As Decimal

        Dim nbPts As Integer = Me.Tau.MStatic.Count

        '--> Initialisation

        FyW = MyPoutre.Section.FyW
        TauY = FyW / MyPoutre.Param.Gamma.GammaM0 / Math.Sqrt(3)

        '--> Calculs

        '# Contraintes de cisaillement dans l'âme du profilé

        If (nbPts > 0) Then
            '( Contrainte face interne de la semelle supérieure
            RunCritereFlexionVM(MyPoutre, iCombi, 0, TauELU, TauY, Me.CritereTauA)
            '( Contrainte CdG de la section
            RunCritereFlexionVM(MyPoutre, iCombi, 1, TauELU, TauY, Me.CritereTauA)
            '( Contrainte face interne de la semelle inférieure
            RunCritereFlexionVM(MyPoutre, iCombi, 2, TauELU, TauY, Me.CritereTauA)
        End If

    End Sub

    Private Sub RunCritereTranchantElastic(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, TauELU(,,) As Decimal,
                                           TauU As Decimal, MyCritereV As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en cisaillement par les critères de VonMises en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint      [E] :   Indice du point de calcul des contraintes
        '   TauELU      [E] :   Contraintes de cisaillement aux ELU
        '   TauU        [E] :   Valeur ultime de la contrainte de cisaillement au point iPoint
        '   CritereM    [E] :   Critere de la contrainte de flexion
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
                    MyCritereV.EnregistreCritere(iNode, iCombi, iTravee, TauELU(iPoint, iNode, k), TauU)
                Next
            Next
        Next

    End Sub

#End Region

#Region " Vérification des contraintes élastiques équivalentes de Von Mises "

    Private Sub RunCritereInteractionMVElastiqueVonMises(MyPoutre As cls_Poutre, iCombi As Integer, SigmaELU(,,) As Decimal, TauELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en cisaillement par les critères de VM
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   TauELU      [E] :   Contraintes de cisaillement aux ELU
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim FydSup, FySup As Decimal
        Dim FydW, FyW As Decimal
        Dim FydInf, FyInf As Decimal

        Dim nbPts As Integer = Me.Tau.MStatic.Count

        '--> Initialisation

        FySup = MyPoutre.Section.FySup
        FydSup = FySup / MyPoutre.Param.Gamma.GammaM0
        FyW = MyPoutre.Section.FyW
        FydW = FyW / MyPoutre.Param.Gamma.GammaM0
        FyInf = MyPoutre.Section.FyInf
        FydInf = FyInf / MyPoutre.Param.Gamma.GammaM0

        '--> Calculs

        '# Contraintes de cisaillement dans l'âme du profilé

        If (nbPts > 0) Then
            '( Contrainte face interne de la semelle supérieure
            RunCritereInteractionMVElastic(MyPoutre, iCombi, 0, SigmaELU, TauELU, FydW, Me.CritereSigmaVM)
            '( Contrainte CdG de la section
            RunCritereInteractionMVElastic(MyPoutre, iCombi, 1, SigmaELU, TauELU, FydW, Me.CritereSigmaVM)
            '( Contrainte face interne de la semelle inférieure
            RunCritereInteractionMVElastic(MyPoutre, iCombi, 2, SigmaELU, TauELU, FydW, Me.CritereSigmaVM)
        End If

    End Sub

    Private Sub RunCritereInteractionMVElastic(MyPoutre As cls_Poutre, iCombi As Integer, iPoint As Integer, SigmaELU(,,) As Decimal, TauELU(,,) As Decimal,
                                               SigmaU As Decimal, MyCritereSigmaEqVM As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance contrainte équivalente de VonMises en un point de calcul de section
        '----------------------------------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iPoint      [E] :   Indice du point de calcul des contraintes
        '   SigmaELU    [E] :   Contraintes normales aux ELU
        '   TauELU      [E] :   Contraintes de cisaillement aux ELU
        '   SigmaU      [E] :   Valeur ultime de la contrainte équivalente VM au point iPoint
        '   CritereM    [E] :   Critere de la contrainte de flexion
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim SigmaEq As Decimal
        Const iDecal As Integer = 1

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
                    SigmaEq = Math.Sqrt(SigmaELU(iPoint + iDecal, iNode, k) ^ 2 + 3 * TauELU(iPoint, iNode, k) ^ 2)

                    MyCritereSigmaEqVM.EnregistreCritere(iNode, iCombi, iTravee, SigmaEq, SigmaU)
                Next
            Next
        Next

    End Sub

#End Region

End Class
