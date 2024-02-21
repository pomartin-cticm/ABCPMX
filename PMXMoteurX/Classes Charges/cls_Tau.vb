Public Class cls_Tau

    '=========================================================================================================
    '   CLASSE POUR LA DEFINITION DES POINTS OU SONT CALCULEES LES CONTRAINTES NORMALES
    '   ET POUR LE  CALCUL DE CES CONTRAINTES NORMALES
    '=========================================================================================================

#Region " Attributs "

    Public MStatic() As Decimal     ' Moment statique des points où on calcule la contrainte de cisaillement

    Private pInertieY As Decimal    ' Moment d'inertie du profilé

    Private pTw As Decimal          ' Epaisseur de l'âme du profilé

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

#Region " Calcul des contraintes de cisaillement "

    Public Sub CalculContraintesCharges(MyPoutre As cls_Poutre, ByRef Tau(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues de tous les cas de charges
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   Signe       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbPts As Integer = Me.MStatic.Count
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
        Dim NbPts As Integer = Me.MStatic.Count
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim VEd, InertieY As Decimal

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                For iPts = 0 To NbPts - 1

                    InertieY = MyPoutre.Elements(MyPoutre.ChargesA(iCas).IndElts).InertieY(iElt)

                    VEd = MyPoutre.ChargesA(iCas).VZ(iNode, k)
                    Tau(iCas, iPts, iNode, k) = VEd * Me.MStatic(iPts) / (Me.pInertieY * Me.pTw) / kConvMPaPa

                Next

            Next

        Next

    End Sub


#End Region


End Class
