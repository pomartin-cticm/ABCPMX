Imports CTICM_DATA_DLLS
Imports CTICM_MODAL

Public Class cls_AnalyseModale

    '========================================================================================================================================
    '   CLASSE POUR EFFECTUER L'ANALYSE MODALE DE LA POUTRE
    '========================================================================================================================================

#Region " Attributs "

    Dim pFrequence As Decimal
    Dim pPeriod As Decimal
    Dim pErrorText As String
    Dim pErrorCode As Integer

    Dim pMassTotal As Decimal       ' Masse totale prise en compte pour l'analyse modale
    Dim pMassModal As Decimal       ' Masse modale
    Dim pMassGeneral As Decimal     ' Masse généralisée

    Dim pDeformee() As Decimal      ' Deformée modale

#End Region

#Region " Constructeur "

    Public Sub New()

    End Sub

#End Region

#Region " Resultats "

    Public ReadOnly Property Frequence
        Get
            Return pFrequence
        End Get
    End Property

    Public ReadOnly Property Periode
        Get
            Return pPeriod
        End Get
    End Property

    Public ReadOnly Property ErrorCode As Integer
        Get
            Return pErrorCode
        End Get
    End Property

    Public ReadOnly Property ErrorMsg As String
        Get
            Return pErrorText
        End Get
    End Property

    Public ReadOnly Property MassModal As Decimal
        Get
            Return Me.pMassModal
        End Get
    End Property

    Public ReadOnly Property MassGeneral As Decimal
        Get
            Return Me.pMassGeneral
        End Get
    End Property

    Public ReadOnly Property MassTotal As Decimal
        Get
            Return Me.pMassTotal
        End Get
    End Property

    Public ReadOnly Property Deformee As Decimal()
        Get
            Return pDeformee
        End Get
    End Property

#End Region

#Region " Calculs "

    Public Sub Analyse(MyPoutre As cls_Poutre, RatioQ As Decimal, IndiceQ As Integer)
        '---------------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Analyse modale de la poutre
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre à calculer
        '   RatioQ      [E] :   Ratio sur les charges Q
        '   IndiceQ     [E] :   Indice de la charge d'exploitation Q1 ou Q2 à prendre en compte (-1) si non pris en compte de Q
        '---------------------------------------------------------------------------------------------------
        '   L'analyse modale est effectuée pour les masses associées aux cas de charges :
        '       G + G2 + RatioQ x Q(IndiceQ)
        '---------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim DonneesEF As New CTICM_DATA_DLLS.DATA_DLLS
        Dim lAppuisOK As Boolean
        Dim nEqDalleCT As Decimal
        Dim nEqEnrobCT As Decimal
        Dim SigneM() As Decimal = Nothing
        Dim pMyElts As cls_Poutre.strucBeamElements = Nothing
        Dim lGene1 As Boolean = MyPoutre.Param.lGeneration1

        '--> Initialisations

        nEqDalleCT = MyPoutre.Dalle.beton.CoefficientEquivalenceCT(lGene1)
        nEqEnrobCT = MyPoutre.Section.Enrobage.Beton.CoefficientEquivalenceCT(lGene1)

        MyPoutre.PrepareNodesN()
        MyPoutre.InitialiseSigneMoment(SigneM)

        DonneesEF.PESANTEUR = MyPoutre.Param.GraviteG
        DonneesEF.EYOUNG = MyPoutre.Section.Acier.EYoung * kConvMPaPa

        '--> Préparation du modèle EF

        MyPoutre.PrepareModeleEF(DonneesEF, lAppuisOK)

        '--> Appuis

        MyPoutre.PrepareAppuisModeleEF(False, DonneesEF)

        '--> Propriétés des éléments

        MyPoutre.MaillageProprietesElements(MyPoutre.lMixte, nEqDalleCT, nEqEnrobCT, SigneM, pMyElts)
        Me.TransfertProprietesElts(pMyElts, DonneesEF)

        '--> Préparation des charges

        Me.PreparationCharges(MyPoutre, RatioQ, IndiceQ, DonneesEF)

        '--> Calcul modal

        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        'Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(DonneesEF, 1, MyOutput_MOD, Me.pErrorCode, TextError_MOD)

        '--> Capture des résultats

        Me.pErrorText = TextError_MOD
        If Me.pErrorCode = 0 Then
            Me.pFrequence = MyOutput_MOD.FreqProp(0)
            If Me.pFrequence > 0 Then
                Me.pPeriod = 1 / Me.pFrequence
            End If

            Me.pMassModal = MyOutput_MOD.MasseMod(0)
            Me.pMassGeneral = MyOutput_MOD.MasseGen(0)
            Me.pMassTotal = MyOutput_MOD.MasseTot

            ReDim Me.pDeformee(MyPoutre.Nodes.nbNodes - 1)

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                Me.pDeformee(iNode) = MyOutput_MOD.VectProp(0, iNode)
            Next

        Else

        End If

    End Sub

    Private Sub TransfertProprietesElts(pMyElts As cls_Poutre.strucBeamElements, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS)
        '---------------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------

        '--> Transfert des propriétés de section

        For i As Integer = 0 To pDonneesEF.NbNodes - 2
            pDonneesEF.Aire(i) = pMyElts.Aire(i)
            pDonneesEF.InertieY(i) = pMyElts.InertieY(i)
        Next

    End Sub

    Private Sub PreparationCharges(MyPoutre As cls_Poutre, RatioQ As Decimal, IndiceQ As Integer, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS)
        '---------------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Analyse modale de la poutre
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre à calculer
        '   RatioQ      [E] :   Ratio sur les charges Q
        '   IndiceQ     [E] :   Indice de la charge d'exploitation Q1 ou Q2 à prendre en compte (-1) si non pris en compte de Q
        '   pDonneesEF  [S] :   Construction du maillage EF et de son chargement
        '---------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravP, iTravD As Integer
        ' Dim labelQ() As String = {"Q1", "Q2"}
        Dim labelCharges() As String = {"G1", "G2", "Q1", "Q2"}
        Dim NbCharges As Integer = labelCharges.GetUpperBound(0) + 1
        Dim lCalcul(NbCharges - 1) As Boolean                          ' Indique les charges définies
        Dim NombreForceReparties As Integer = 0
        Dim i As Integer
        Dim CompteurRep As Integer = -1
        Dim RatioC() As Decimal = {1, 1, 0, 0}                         ' Indique les pondérations de charges

        '--> Initialisation

        iTravP = MyPoutre.IndicePremiereTravee
        iTravD = MyPoutre.IndiceDerniereTravee

        If IndiceQ > 0 Then
            RatioC(1 + IndiceQ) = RatioQ
        End If

        For i = 0 To NbCharges - 1
            lCalcul(i) = MyPoutre.ChargesU(labelCharges(i)).EstDefinie And (Not IsEqual(RatioC(i), 0))
            If lCalcul(i) Then NombreForceReparties += MyPoutre.ChargesU(labelCharges(i)).NombreForceReparties(iTravP, iTravD) _
                                                     + MyPoutre.ChargesU(labelCharges(i)).NombreChargesSurf(iTravP, iTravD)
        Next

        pDonneesEF.NbForcesRep = NombreForceReparties
        If NombreForceReparties > 0 Then
            ReDim pDonneesEF.ForceRep(NombreForceReparties - 1, 1)
            ReDim pDonneesEF.xForceRep(NombreForceReparties - 1, 1)
        End If

        '--> Transfert des charges

        For i = 0 To NbCharges - 1
            If lCalcul(i) Then
                TransfertChargementU(iTravP, iTravD, MyPoutre.ChargesU(labelCharges(i)), MyPoutre.LongueurTravee, MyPoutre.LargeurInfluence, pDonneesEF, CompteurRep, RatioC(i))
            End If
        Next

        ''# Poids propre

        'TransfertChargementU(iTravP, iTravD, MyPoutre.ChargesU("G1"), pDonneesEF, CompteurRep)

        ''# Autres charges permanentes

        'If MyPoutre.ChargesU("G2").EstDefinie Then
        '    TransfertChargementU(iTravP, iTravD, MyPoutre.ChargesU("G2"), pDonneesEF, CompteurRep)
        'End If

        ''# Charges d'exploitation

        'If IndiceQ > 0 Then
        '    If MyPoutre.ChargesU(labelQ(IndiceQ - 1)).EstDefinie Then
        '        TransfertChargementU(iTravP, iTravD, MyPoutre.ChargesU(labelQ(IndiceQ - 1)), pDonneesEF, CompteurRep, RatioQ)
        '    End If
        'End If

    End Sub

    Private Sub TransfertChargementU(iTravP As Integer, iTravD As Integer, MyChargesU As cls_ChargementUtilisateur, LongueurT() As Decimal, LargeurDinf As Decimal,
                                     ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS, ByRef pCompteurRep As Integer, Optional Ratio As Decimal = 1)
        '---------------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Analyse modale de la poutre
        '---------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice de la première travée
        '   iTravD      [E] :   Indice de la dernière travée
        '   MyChargesU  [E] :   Chargement utilisateur à transférer
        '   LongueurT   [E] :   Longueur des travées
        '   LargeurDinf [E] :   Largeur d'influence des charges sur la dalle
        '   pDonneesEF  [S] :   Construction du maillage EF et de son chargement
        '   pCompterRep [E/S] : Compteur des forces reparties
        '   Ratio       [E] :   Pondération du cas de charge
        '---------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTrav As Integer
        Dim i As Integer
        Dim xo, xe, qSurf As Decimal

        '--> Initialisation

        xo = 0
        xe = 0

        '--> Boucle sur les travées

        For iTrav = iTravP To iTravD

            '# Préparation

            xe += LongueurT(iTrav)

            '# Charges surfaciques

            If Not IsEqual(MyChargesU.QSurf(iTrav), 0) Then
                qSurf = LargeurDinf * MyChargesU.QSurf(iTrav) * Ratio
                Me.AjouteForceRep(xo, xe, qSurf, qSurf, pDonneesEF, pCompteurRep)
            End If

            '# Charges linéiques

            For i = 0 To MyChargesU.FReparties(iTrav).Count - 1

                Me.AjouteForceRep(MyChargesU.FReparties(iTrav)(i).xPosG(0), MyChargesU.FReparties(iTrav)(i).xPosG(1),
                                  MyChargesU.FReparties(iTrav)(i).Force(0) * Ratio,
                                  MyChargesU.FReparties(iTrav)(i).Force(1) * Ratio, pDonneesEF, pCompteurRep)

            Next

            '# Charges ponctuelles

            For i = 0 To MyChargesU.Forces(iTrav).Count - 1

                Me.AjouteForce(MyChargesU.Forces(iTrav)(i).xPosG, MyChargesU.Forces(iTrav)(i).Force * Ratio, pDonneesEF)

            Next

            xo = xe

        Next

    End Sub


#End Region

#Region " Outils "

    Private Sub AjouteForceRep(xo As Decimal, xe As Decimal, qo As Decimal, qe As Decimal,
                               ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS, ByRef pComptRep As Integer)
        '-------------------------------------------------------------------------------------
        '   09/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Ajout d'une force répartie dans les paramètres préparatoires au calcul EF
        '-------------------------------------------------------------------------------------
        '   xo          [E] :   Position gauche de la force répartie
        '   xe          [E] :   Position droite de la force répartie
        '   qo          [E] :   Valeur à gauche de la force répartie
        '   qe          [E] :   Valeur à droite de la force répartie
        '   pDonneesEF  [S] :   Donnes pour le calcul EF
        '   pComptRep   [E/S] : Compteur de forces réparties
        '-------------------------------------------------------------------------------------

        pComptRep += 1

        pDonneesEF.xForceRep(pComptRep, 0) = xo
        pDonneesEF.xForceRep(pComptRep, 1) = xe
        pDonneesEF.ForceRep(pComptRep, 0) = qo
        pDonneesEF.ForceRep(pComptRep, 1) = qe

    End Sub

    Private Sub AjouteForce(xFor As Decimal, Force As Decimal, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS)
        '-------------------------------------------------------------------------------------
        '   18/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Ajout d'un effort vertical dans les paramètres préparatoires au calcul EF
        '-------------------------------------------------------------------------------------
        '   xFor        [E] :   Position de la force
        '   Force       [E] :   Valeur de la force
        '   pDonneesEF  [S] :   Donnes pour le calcul EF
        '-------------------------------------------------------------------------------------

        pDonneesEF.NbForcesPon += 1
        If pDonneesEF.NbForcesPon = 1 Then
            ReDim pDonneesEF.ForcePon(pDonneesEF.NbForcesPon - 1)
            ReDim pDonneesEF.xForcePon(pDonneesEF.NbForcesPon - 1)
        Else
            ReDim Preserve pDonneesEF.ForcePon(pDonneesEF.NbForcesPon - 1)
            ReDim Preserve pDonneesEF.xForcePon(pDonneesEF.NbForcesPon - 1)
        End If

        pDonneesEF.ForcePon(pDonneesEF.NbForcesPon - 1) = Force
        pDonneesEF.xForcePon(pDonneesEF.NbForcesPon - 1) = xFor

    End Sub

#End Region

End Class
