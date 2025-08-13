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

#Region " Routines pour le calcul des flux de cisaillement des poutres mixtes "

    Public Sub InitialiseCalculMixte(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation du calcul des flux de cisaillement longi dans les soudures
        '   des poutres PRS mixtes
        '   et à l'interface dalle/semelle
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre
        '-----------------------------------------------------------------------------------


        iDeb = 0

        If myBeam.Section.lLamine Then
            iFin = 0
        Else
            iFin = 2
        End If

        'Indice 0 pour la connexion
        'Indice 1 pour la semelle supérieure
        'Indice 2 pour la semelle inférieure

    End Sub

    Public Sub CalculFluxChargesMIXTE(MyPoutre As cls_Poutre, bEff() As Decimal, ByRef FluxF(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   14/03/24 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des flux de cisaillement longitudinal issues de tous les cas de charges
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   bEff        [E] :   Largeur participante le long de la dalle
        '   FluxF       [S] :   Table des flux de cisaillement (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbCas As Integer = MyPoutre.ChargesA.Count
        Dim iCas As Integer
        'Dim lCasMixte As Boolean

        '--> Initialisation

        ReDim FluxF(NbCas - 1, NbPTS - 1, NbNodes - 1, 1)

        '--> Boucle sur tous les cas de charges

        For iCas = 0 To NbCas - 1

            'lCasMixte = MyPoutre.Elements(MyPoutre.ChargesA(iCas).IndElts).lMixte -> variable utilisée dans la fonction CalculFluxSectionMixte

            'If MyPoutre.ChargesA(iCas).lRunCalcul And lCasMixte Then 'Modif GUD: il faut lancer la routine 
            If MyPoutre.ChargesA(iCas).lRunCalcul Then

                Me.CalculFluxFSectionsMixte(MyPoutre, bEff, iCas, FluxF)

            End If

        Next
    End Sub

    Private Sub CalculFluxFSectionsMixte(myBeam As cls_Poutre, bEff() As Decimal, iCas As Integer, ByRef FluxF(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calcul des flux de cisaillement longitudinal pour un cas de charge
        '-----------------------------------------------------------------------------------
        '   myBeam    [E] :   Poutre traitée
        '   bEff        [E] :   Largeur participante le long de la dalle
        '   iCas        [E] :   Indice du cas de charge traité (qui a été calculé par EF)
        '   FluxF       [S] :   Table des flux de cisaillement longi pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = myBeam.Nodes.nbNodes
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin As Integer
        Dim VEd As Decimal
        Dim InertieY As Decimal
        Dim iElt As Integer
        Dim nEqDalle As Decimal
        Dim MomStat As Decimal
        Dim zANE As Decimal
        Dim lCasMixte As Boolean
        Dim IndElts As Integer           ' Indice du tableau des propriétés à considérer pour le calcul

        '-> Initialisation

        IndElts = myBeam.ChargesA(iCas).IndElts(0)
        nEqDalle = myBeam.Elements(IndElts).nEqDalle
        lCasMixte = myBeam.Elements(IndElts).lMixte

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                iElt = iNode - 1 + k

                For iPts = iDeb To iFin

                    If iPts = iDeb And Not lCasMixte Then
                        FluxF(iCas, iPts, iNode, k) = 0 'dans le cas non mixte, il n'y a pas de flux de cisaillement dans les connecteurs mais il y en a quand même au droit des soudures
                    Else
                        InertieY = myBeam.Elements(IndElts).InertieY(iElt)
                        zANE = myBeam.Elements(IndElts).zANE(iElt)


                        MomStat = Me.MomentStatiqueMixte(myBeam, iPts, nEqDalle, bEff(iNode), zANE)

                        VEd = myBeam.ChargesA(iCas).VZ(iNode, k)
                        FluxF(iCas, iPts, iNode, k) = VEd * MomStat / InertieY
                    End If

                Next

            Next

        Next

    End Sub

    Private Function MomentStatiqueMixte(myBeam As cls_Poutre, iPts As Integer, nEqDalle As Decimal, mybEff As Decimal, zANE As Decimal) As Decimal
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calcul du moment statique pour l'un des points de calcul 
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iPts        [E] :   Indice du point : (0 pour la connexion, 1 seme sup, 2 sem inf)
        '   nEqDalle    [E] :   Coefficient d'équivalence acier / béton pour la dalle
        '   mybEff      [E] :   Largeur efficace de la dalle
        '   zANE        [E] :   Position du cdg de la section
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim MomStat As Decimal
        Dim zI, zII As Decimal
        'Dim hW As Decimal
        Dim Tc As Decimal

        '--( Traitement

        Select Case iPts
            Case 0
                '== Connexion
                Tc = myBeam.Dalle.EpaisseurActive
                zI = myBeam.Dalle.zTop - Tc / 2
                MomStat = mybEff * Tc / nEqDalle * (zANE - zI)

            Case 1
                '== Liaison semelle supérieure
                'Tc = myBeam.Dalle.EpaisseurActive
                'zI = myBeam.Dalle.zTop - Tc / 2
                'MomStat = mybEff * Tc / nEqDalle * (zANE - zI)

                'zII = -myBeam.Section.ProfilA.Tfs / 2
                'MomStat += myBeam.Section.ProfilA.AireFs * (zANE - zI)

                '--> Calcul en considérant la partie inférieure , ce qui permet d'éviter de faire intervenir la dalle et la division par nEq dans le cas non mixte
                zI = myBeam.Section.ProfilA.zRefAraseSup - myBeam.Section.ProfilA.Tfs - myBeam.Section.ProfilA.HauteurAmeHw / 2
                MomStat = myBeam.Section.ProfilA.HauteurAmeHw * myBeam.Section.ProfilA.Tw * (zANE - zI)

                zII = myBeam.Section.ProfilA.zRefAraseSup - myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi / 2
                MomStat += myBeam.Section.ProfilA.AireFi * (zANE - zII)

            Case 2
                '== Liaison de semelle inférieure
                zI = myBeam.Section.ProfilA.zRefAraseSup - myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi / 2
                MomStat = myBeam.Section.ProfilA.AireFi * (zANE - zI)

        End Select

        Return MomStat

    End Function

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
        '   myBeam    [E] :   Poutre traitée
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
        '   myBeam    [E] :   Poutre traitée
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
                    FluxF(iCas, iPts, iNode, k) = VEd * Me.MStatic(iPts) / (Me.pInertieY)

                Next

            Next

        Next

    End Sub


#End Region

End Class
