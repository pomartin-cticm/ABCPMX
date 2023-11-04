
Imports CTICM_DATA_DLLS

Public Class cls_AnalyseEFinis
    '========================================================================================================================================
    '   CLASSE POUR EFFECTUER L'ANALYSE ELEMENTS FINIS DE LA POUTRE
    '========================================================================================================================================

#Region " Attributs "

    Dim pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees

    Dim pResults_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
    Dim CodeError_RDM As Integer
    Dim TextError_RDM As String = String.Empty

#End Region


#Region " Constructeurs "

    Public Sub New(EYoung As Decimal, GraviteG As Decimal, Nodes As cls_Poutre.strucBeamNodes)
        PrepareModele(EYoung, GraviteG, Nodes)
    End Sub

#End Region

#Region " Executer le calcul "

    Public Sub RunRDM(ByRef lOk As Boolean)

        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM

        MyDLLRDM.CALCULER(pDonneesEF, pResults_RDM, CodeError_RDM, TextError_RDM)

        lOk = (CodeError_RDM = 0)

    End Sub

    Public ReadOnly Property Moments As Decimal(,)
        Get
            Return Me.pResults_RDM.MYY
        End Get
    End Property

    Public ReadOnly Property MomentMax As Decimal
        Get
            Dim MomMax As Decimal = pResults_RDM.MYY(0, 1)
            For iNode As Integer = 1 To pDonneesEF.NbNodes - 2
                For k = 0 To 1
                    MomMax = Math.Max(MomMax, pResults_RDM.MYY(iNode, k))
                Next
            Next
            MomMax = Math.Max(MomMax, pResults_RDM.MYY(pDonneesEF.NbNodes - 1, 0))

            Return MomMax
        End Get
    End Property

    Public ReadOnly Property TranchantMax As Decimal
        Get
            Dim VMax As Decimal = pResults_RDM.VZ(0, 1)
            For iNode As Integer = 1 To pDonneesEF.NbNodes - 2
                For k = 0 To 1
                    VMax = Math.Max(VMax, pResults_RDM.VZ(iNode, k))
                Next
            Next
            VMax = Math.Max(VMax, pResults_RDM.VZ(pDonneesEF.NbNodes - 1, 0))

            Return VMax
        End Get
    End Property

    Public ReadOnly Property FlecheMax As Decimal
        Get
            Dim fMax As Decimal = pResults_RDM.UZ(0)
            For iNode As Integer = 1 To pDonneesEF.NbNodes - 1

                fMax = Math.Max(fMax, pResults_RDM.UZ(iNode))

            Next

            Return fMax
        End Get
    End Property

    Public ReadOnly Property FlecheMaxAbs As Decimal
        Get
            Dim fMax As Decimal = Math.Abs(pResults_RDM.UZ(0))
            For iNode As Integer = 1 To pDonneesEF.NbNodes - 1

                fMax = Math.Max(fMax, Math.Abs(pResults_RDM.UZ(iNode)))

            Next

            Return fMax
        End Get
    End Property

    Public Function Reaction(iAppui As Integer) As Decimal
        Return pResults_RDM.RZ(iAppui)
    End Function

#End Region

#Region " Chargements "

    Public Sub TransfertChargementU(MyChargU As cls_ChargementUtilisateur, iTravP As Integer, iTravD As Integer, LongueurT() As Decimal, LargeurD As Decimal)
        '---------------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Transfert d'un chargement utilisateur vers le modèle EF
        '---------------------------------------------------------------------------------------------
        '   MyChargU    [E] :   Chargement utilisateur à calculer
        '   iTravP      [E] :   Indice première travée
        '   iTravD      [E] :   Indice dernière travée
        '   LongueurT   [E] :   Table des longueur de travées
        '   LargeurD    [E] :   Largeur sur laquelle s'appliquent les charges surfaciques
        '---------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iForce, iTrav As Integer
        Dim NbForceRep As Integer = 0
        Dim iCompteur As Integer = -1
        Dim xo, xe, qo, qe As Decimal
        Dim Cumul As Decimal = 0

        '--> Initialisations

        pDonneesEF.NbForcesPon = 0
        pDonneesEF.NbForcesRep = 0

        NbForceRep = MyChargU.NombreForceReparties(iTravP, iTravD) + MyChargU.NombreChargesSurf(iTravP, iTravD)

        pDonneesEF.NbForcesRep = NbForceRep
        If NbForceRep > 0 Then
            ReDim pDonneesEF.ForceRep(NbForceRep - 1, 1)
            ReDim pDonneesEF.xForceRep(NbForceRep - 1, 1)
        End If

        '--> Transfert 

        For iTrav = iTravP To iTravD
            '# Transfert des charges ponctuelles

            For iForce = 0 To MyChargU.Forces(iTrav).Count - 1

                AjouteForce(MyChargU.Forces(iTrav)(iForce).xPosG, MyChargU.Forces(iTrav)(iForce).Force)

            Next

            '# Transfert des charges réparties

            For iForce = 0 To MyChargU.FReparties(iTrav).Count - 1

                xo = MyChargU.FReparties(iTrav)(iForce).xPosG(0)
                xe = MyChargU.FReparties(iTrav)(iForce).xPosG(1)
                qo = MyChargU.FReparties(iTrav)(iForce).Force(0)
                qe = MyChargU.FReparties(iTrav)(iForce).Force(1)

                AjouteForceRep(xo, xe, qo, qe, iCompteur)

            Next

            '# Charges surfaciques

            If Not IsEqual(MyChargU.QSurf(iTrav), 0) Then

                xo = Cumul
                xe = xo + LongueurT(iTrav)
                qo = MyChargU.QSurf(iTrav) * LargeurD
                qe = qo

                AjouteForceRep(xo, xe, qo, qe, iCompteur)

            End If

            Cumul += LongueurT(iTrav)

        Next



    End Sub


    Private Sub AjouteForceRep(xo As Decimal, xe As Decimal, qo As Decimal, qe As Decimal, ByRef pComptRep As Integer)
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

    Private Sub AjouteForce(xFor As Decimal, Force As Decimal)
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


#Region " Initialisation du modèle "

    Public Sub PrepareModele(EYoung As Decimal, GraviteG As Decimal, Nodes As cls_Poutre.strucBeamNodes)
        '-------------------------------------------------------------------------------------
        '   04/11/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation du modele EF avant lancement des calculs (hors propriétés éléments et chargements)
        '-------------------------------------------------------------------------------------
        '   EYoung      [E] :   Module d'Young
        '   GraviteG    [E] :   
        '   Nodes       [E] :   Maillage des noeuds de la poutre
        '-------------------------------------------------------------------------------------

        '--> Propriétés générale

        pDonneesEF.EYOUNG = EYoung * kConvMPaPa
        pDonneesEF.PESANTEUR = GraviteG

        '--> Maillage (neouds)

        PrepareNoeudsModeleEF(Nodes)

        '--> Tableaux pour les éléments

        ReDim pDonneesEF.Aire(pDonneesEF.NbNodes - 2)
        ReDim pDonneesEF.InertieY(pDonneesEF.NbNodes - 2)

    End Sub

    Private Sub PrepareNoeudsModeleEF(Nodes As cls_Poutre.strucBeamNodes)
        '-------------------------------------------------------------------------------------
        '   07/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation du modele EF avant lancement des calculs (hors propriétés éléments et chargements)
        '-------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        '--> Définition des noeuds

        '# nombre de noeuds

        pDonneesEF.NbNodes = Nodes.nbNodes

        '# Position des noeuds EF
        ReDim pDonneesEF.xNode(pDonneesEF.NbNodes - 1)
        For i = 0 To pDonneesEF.NbNodes - 1
            pDonneesEF.xNode(i) = Nodes.xGlobal(i)
        Next

    End Sub

    Public Sub Appuis(Nodes As cls_Poutre.strucBeamNodes, lEtais As Boolean)
        '-------------------------------------------------------------------------------------
        '   20/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation des appuis du modele EF avant lancement des calculs
        '   en prenant en compte les appuis des étais, le cas échéant        
        '-------------------------------------------------------------------------------------
        '   Nodes   [E] :   Définition des sections de calcul de la poutre
        '   lEtais  [E] :   Indique si on ajoute les appuis des étais dans le modèle
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbApp As Integer
        Dim indAppuis() As Integer = Nothing

        '--> Initialisation

        ExtraireIndiceNoeudsAppuis(Nodes, lEtais, indAppuis, NbApp)

        pDonneesEF.NbAppuis = NbApp

        ReDim pDonneesEF.iNodeAppui(pDonneesEF.NbAppuis - 1)
        ReDim pDonneesEF.lAppuiArticule(pDonneesEF.NbAppuis - 1)

        '--> Traitement

        For i As Integer = 0 To pDonneesEF.NbAppuis - 1
            pDonneesEF.iNodeAppui(i) = indAppuis(i)
            pDonneesEF.lAppuiArticule(i) = False
        Next

    End Sub

    Private Sub ExtraireIndiceNoeudsAppuis(Nodes As cls_Poutre.strucBeamNodes, lEtais As Boolean, ByRef indAppuis() As Integer, ByRef NbApp As Integer)
        '-------------------------------------------------------------------------------------
        '   20/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation des appuis du modele EF avant lancement des calculs
        '   en prenant en compte les appuis des étais, le cas échéant
        '-------------------------------------------------------------------------------------
        '   lEtais      [E] :   INdique si on ajoute les étais ponctuels comme appuis 
        '   indAppuis   [S] :   Liste des indices des noeuds appuyés
        '   NbApp       [S] :   Nombre d'appuis dans le modèle
        '-------------------------------------------------------------------------------------

        '--> Nombre d'appuis

        If lEtais Then
            NbApp = Nodes.NbAppuis + Nodes.NbEtais
        Else
            NbApp = Nodes.NbAppuis
        End If

        ReDim indAppuis(NbApp - 1)

        '--> Appuis des travées

        For i As Integer = 0 To Nodes.NbAppuis - 1
            indAppuis(i) = Nodes.iNodeAppui(i)
        Next

        '--> Etais

        If lEtais Then
            For i As Integer = 0 To Nodes.NbEtais - 1
                indAppuis(Nodes.NbAppuis + i) = Nodes.iNodeEtais(i)
            Next
            Array.Sort(indAppuis)
        End If
    End Sub

    Public Sub AttribuerProprietesConstantes(InertieY As Decimal, Aire As Decimal)
        '-------------------------------------------------------------------------------------
        '   04/11/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Attribue des propriétés d'éléments constantes le longe de la barre
        '-------------------------------------------------------------------------------------
        '   InertieY    [E] :   Inertie des éléments
        '   Aire        [E] :   Aire des éléments
        '-------------------------------------------------------------------------------------

        '--> Boucle sur les éléments

        For i As Integer = 0 To pDonneesEF.NbNodes - 2

            pDonneesEF.Aire(i) = Aire
            pDonneesEF.InertieY(i) = InertieY

        Next

    End Sub




    Private Sub Poubelle()

        ''--> Préparation du modèle EF

        'MyPoutre.PrepareModeleEF(DonneesEF, lAppuisOK)

        ''--> Appuis

        'MyPoutre.PrepareAppuisModeleEF(False, DonneesEF)

        ''# Appuis

        'lAppuisOK = False
        'If Me.Nodes.NbAppuis = 0 Then
        '    PrepareAppuisModeleEF(False, pDonneesEF)
        '    lAppuisOK = True
        'End If



    End Sub

#End Region

End Class
