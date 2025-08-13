Public Class cls_Tau

    '=========================================================================================================
    '   CLASSE POUR LE CALCUL DES CONTRAINTES DE CISAILLEMENT
    '=========================================================================================================

#Region " Attributs "

    Public MStatic() As Decimal     ' Moment statique des points où on calcule la contrainte de cisaillement (poutre acier uniquement)
    Public NbPts As Integer         ' Nombre de points où on calcule les contraintes de cisaillement (poutres mixtes)

    Private pInertieY As Decimal    ' Moment d'inertie du profilé

    Private pTw As Decimal          ' Epaisseur de l'âme du profilé

    Private zGAcier As Decimal      ' Position du cdg du profile acier

#End Region

#Region " Constructeurs+Initialisations "

    Public Sub New(TypeP As cls_Section.Enum_TypeSection)

        ReDim MStatic(2)

    End Sub

    Public Sub Initialise(myProfil As cls_ProfilA)
        '---------------------------------------------------------------------------------------------------------
        '   21/02/24 :  Création - POM - V1.00
        '---------------------------------------------------------------------------------------------------------
        '   Initialisation des moments statiques pour le calcul des contraintes de cisaillement
        '   Formules pour les profilés en acier
        '---------------------------------------------------------------------------------------------------------
        '   myProfil    [E] :   Profilé considéré
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim zANE As Decimal
        Dim InertieY As Decimal

        '--( Initialisation

        myProfil.ProprietesElastiques(1, zane, inertiey)

        '--( Calculs

        Me.MStatic(0) = myProfil.MomentStatiqueFSup(zANE)

        Me.MStatic(1) = myProfil.MomentStatiqueTeSup(zANE)

        Me.MStatic(2) = myProfil.MomentStatiqueFInf(zANE)

        Me.pInertieY = InertieY
        Me.pTw = myProfil.Tw

    End Sub

#End Region

#Region " Calcul des contraintes de cisaillement en poutres mixtes "

    Public Sub InitialisePoutreMixte(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des calculs des contraintes de cisaillement issues de tous les cas de charges
        '   Cas d'une poutre mixte 
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '-----------------------------------------------------------------------------------

        '--( Déclaration

        Dim zANE As Decimal
        Dim Inertie As Decimal

        '--( Initialisations

        Me.NbPts = 3

        MyPoutre.Section.ProfilA.ProprietesElastiques(1, zANE, Inertie)

        Me.zGAcier = zANE

        Me.pTw = MyPoutre.Section.ProfilA.Tw

    End Sub

    Public Sub CalculContraintesChargesMIXTE(MyPoutre As cls_Poutre, ByRef Tau(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes de cisaillement issues de tous les cas de charges
        '   Cas d'une poutre mixte 
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   Tau         [S] :   Table des contraintes de cisaillement (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim iCas As Integer
        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbCas As Integer = MyPoutre.ChargesA.Count
        Dim lCasMixte As Boolean
        Dim indTabElt As Integer = MyPoutre.ChargesA(iCas).IndElts(0)

        '--( Initialisation

        Me.InitialisePoutreMixte(MyPoutre)
        ReDim Tau(NbCas - 1, NbPts - 1, NbNodes - 1, 1)

        '--> Boucle sur tous les cas de charges

        For iCas = 0 To NbCas - 1

            lCasMixte = MyPoutre.Elements(indTabElt).lMixte

            'If MyPoutre.ChargesA(iCas).lRunCalcul And lCasMixte Then
            If MyPoutre.ChargesA(iCas).lRunCalcul Then

                Me.CalculContraintesSectionsMIXTE(MyPoutre, iCas, Tau)

            End If

        Next

    End Sub

    Private Sub CalculContraintesSectionsMIXTE(myPoutre As cls_Poutre, iCas As Integer, ByRef Tau(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes de cisaillement pour un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   iCas        [E] :   Indice du cas de charge traité (qui a été calculé par EF)
        '   Tau         [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = myPoutre.Nodes.nbNodes
        ' Dim NbPts As Integer = Me.MStatic.Count
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim VEd, InertieY, zANE As Decimal
        Dim MomStat As Decimal
        Dim indTabElt As Integer = myPoutre.ChargesA(iCas).IndElts(0)
        Dim nEqDalle As Decimal = myPoutre.Elements(indTabElt).nEqDalle

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                iElt = iNode - 1 + k

                For iPts = 0 To NbPts - 1

                    InertieY = myPoutre.Elements(indTabElt).InertieY(iElt)
                    zANE = myPoutre.Elements(indTabElt).zANE(iElt)

                    VEd = myPoutre.ChargesA(iCas).VZ(iNode, k)

                    MomStat = Me.MomentStatiqueMixte(myPoutre, iPts, nEqDalle, zANE)

                    Tau(iCas, iPts, iNode, k) = VEd * MomStat / (InertieY * Me.pTw) / kConvMPaPa

                Next

            Next

        Next

    End Sub

    Private Function MomentStatiqueMixte(myBeam As cls_Poutre, iPts As Integer, nEqDalle As Decimal, zANE As Decimal) As Decimal
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calcul du moment statique pour l'un des points de calcul 
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iPts        [E] :   Indice du point
        '   nEqDalle    [E] :   Coefficient d'équivalence acier / béton pour la dalle
        '   zANE        [E] :   Position du cdg de la section
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim MomStat As Decimal
        Dim zI, zII As Decimal
        Dim hW As Decimal

        '--( Traitement

        Select Case iPts
            Case 0
                '== Liaison semelle supérieure
                zI = -myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi / 2
                zII = -myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi
                MomStat = myBeam.Section.ProfilA.AireFi * (zANE - zI)

                hW = myBeam.Section.ProfilA.HauteurAmeHw
                MomStat += hW * pTw * (zANE - (zII + hW / 2))
                MomStat = Math.Abs(MomStat)

            Case 1
                '== CdG de la section acier
                zI = -myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi / 2
                zII = -myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi
                MomStat = myBeam.Section.ProfilA.AireFi * (zANE - zI)

                If Me.zGAcier > zII Then
                    hW = Math.Min(myBeam.Section.ProfilA.HauteurAmeHw, Me.zGAcier - zII)
                    MomStat += hW * pTw * (zANE - (zII + hW / 2))

                End If
                MomStat = Math.Abs(MomStat)

            Case 2
                '== Liaison de semelle inférieure
                zI = -myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi / 2
                MomStat = myBeam.Section.ProfilA.AireFi * Math.Abs(zANE - zi)

        End Select

        Return MomStat

    End Function

#End Region

#Region " Calcul des contraintes de cisaillement "

    Public Sub CalculContraintesCharges(MyPoutre As cls_Poutre, ByRef Tau(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes de cisaillement issues de tous les cas de charges
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   Tau         [S] :   Table des contraintes de cisaillement (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Me.NbPts = Me.MStatic.Count
        Dim NbCas As Integer = MyPoutre.ChargesA.Count
        Dim lAcierNonEnrob As Boolean
        Dim iCas As Integer

        '--> Initialisation

        ReDim Tau(NbCas - 1, NbPts - 1, NbNodes - 1, 1)

        lAcierNonEnrob = (MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul)

        '--> Boucle sur tous les cas de charges

        For iCas = 0 To NbCas - 1

            If MyPoutre.ChargesA(iCas).lRunCalcul Then

                If lAcierNonEnrob Then
                    'Me.CalculContraintesSectionsAcierNonEnrobees(MyPoutre, iCas, Sigma)
                Else
                    'Me.CalculContraintesGeneral(MyPoutre, Signe, iCas, Sigma)
                End If

                Me.CalculContraintesSectionsAcier(MyPoutre, iCas, Tau)

            End If

        Next
    End Sub

    Private Sub CalculContraintesSectionsAcier(MyPoutre As cls_Poutre, iCas As Integer, ByRef Tau(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes de cisaillement pour un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   iCas        [E] :   Indice du cas de charge traité (qui a été calculé par EF)
        '   Tau         [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        ' Dim NbPts As Integer = Me.MStatic.Count
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim VEd, InertieY As Decimal
        Dim indTabElt As Integer = MyPoutre.ChargesA(iCas).IndElts(0)

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                iElt = iNode - 1 + k

                For iPts = 0 To NbPts - 1

                    InertieY = MyPoutre.Elements(indTabElt).InertieY(iElt)

                    VEd = MyPoutre.ChargesA(iCas).VZ(iNode, k)
                    Tau(iCas, iPts, iNode, k) = VEd * Me.MStatic(iPts) / (Me.pInertieY * Me.pTw) / kConvMPaPa

                Next

            Next

        Next

    End Sub


#End Region


End Class
