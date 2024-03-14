Public Class cls_Flux


    '=========================================================================================================
    '   CLASSE POUR LE CALCUL DES FLUX DE CISAILLEMENT AUX INTERFACES
    '=========================================================================================================

#Region " Attributs "

    Dim MStatic() As Decimal
    Dim iDeb, iFin As Integer
    Dim pInertieY As Decimal
    Public Const NbPTS As Integer = 3

#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub

#End Region

#Region " Routines pour le calcul des flux de cisaillement des poutres acier "

    '=== POUR LE CALCUL DES SOUDURES DES PRS

    Public Sub InitialiseCalculAcier(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation du calcul des flux de cisaillement longi dans les soudures
        '   des poutres PRS acier
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '-----------------------------------------------------------------------------------

        '--( Déclaration

        Dim zANE As Decimal

        '--( Traitement

        iDeb = 1
        iFin = 2

        ReDim MStatic(NbPTS - 1)

        myBeam.Section.ProfilA.ProprietesElastiques(1, zANE, Me.pInertieY)

        With myBeam.Section.ProfilA
            MStatic(1) = .Bfs * .Tfs * (zANE - (- .Tfs / 2))
            MStatic(2) = .Bfi * .Tfi * (zANE - (- .ha + .Tfi / 2))
        End With

    End Sub

    Public Sub CalculFluxChargesACIER(MyPoutre As cls_Poutre, ByRef FluxF(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des flux de cisaillement longitudinal issues de tous les cas de charges
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   Tau         [S] :   Table des contraintes de cisaillement (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbCas As Integer = MyPoutre.ChargesA.Count
        Dim lAcierNonEnrob As Boolean
        Dim iCas As Integer

        '--> Initialisation

        ReDim FluxF(NbCas - 1, NbPTS - 1, NbNodes - 1, 1)

        lAcierNonEnrob = (MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul)

        '--> Boucle sur tous les cas de charges

        For iCas = 0 To NbCas - 1

            If MyPoutre.ChargesA(iCas).lRunCalcul Then

                Me.CalculFluxFSectionsAcier(MyPoutre, iCas, FluxF)

            End If

        Next
    End Sub

    Private Sub CalculFluxFSectionsAcier(MyPoutre As cls_Poutre, iCas As Integer, ByRef FluxF(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calcul des flux de cisaillement longitudinal pour un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   iCas        [E] :   Indice du cas de charge traité (qui a été calculé par EF)
        '   FluxF       [S] :   Table des flux de cisaillement longi pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin As Integer
        Dim VEd As Decimal

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                For iPts = iDeb To iFin

                    VEd = MyPoutre.ChargesA(iCas).VZ(iNode, k)
                    FluxF(iCas, iPts, iNode, k) = VEd * Me.MStatic(iPts) / (Me.pInertieY) / kConvMPaPa

                Next

            Next

        Next

    End Sub


#End Region

End Class
