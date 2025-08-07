Public Class cls_Combinaisons

    '===========================================================================================================
    '   GESTIONS DES COMBINAISONS POUR L'ANALYSE
    '===========================================================================================================

#Region " Variables "

    Public Symbole() As String                  ' Symbole de la combinaison
    Public nbCombi As Integer                   ' Nombre de combinaisons 
    Public CoefCombi() As List(Of Decimal)      ' Tableau des coefficients de combinaisons

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.nbCombi = 0
        'Me.Symbole = ""
    End Sub

    Public Sub AjouteCombi(pSymb As String, TabCoef() As Decimal, nbCharges As Integer)

        '--> Initialisation du tableau

        Me.nbCombi += 1

        If nbCombi = 1 Then
            ReDim CoefCombi(nbCombi - 1)
            ReDim Symbole(nbCombi - 1)
        Else
            ReDim Preserve CoefCombi(nbCombi - 1)
            ReDim Preserve Symbole(nbCombi - 1)
        End If

        '--> Initialisation des valeurs

        CoefCombi(nbCombi - 1) = New List(Of Decimal)
        For i As Integer = 0 To nbCharges - 1
            CoefCombi(nbCombi - 1).Add(TabCoef(i))
        Next

        Me.Symbole(nbCombi - 1) = pSymb
    End Sub

#End Region

#Region " Outils "

    Public Sub CombineFleches(iCombi As Integer, nbNodes As Integer, ChargesA As List(Of cls_CasDeCharge), ByRef FlechesUZ() As Decimal,
                              lRetrait As Boolean, Optional lETA As Boolean = False)
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Combine les flèches
        '-----------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison à traiter
        '   nbNodes     [E] :   Nombre de noeuds dans la modélisation
        '   ChargesA    [E] :   Tableaux des cas de charges (qui doivent avoir été calculés auparavant
        '   FlechesUZ   [S] :   Table des flèches
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '   lETA        [E] :   Indique si flèche prenant en compte le glissement
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbCharges As Integer = ChargesA.Count
        Dim iCas, jNode As Integer
        Dim plETA As Boolean

        '--> Initialisation

        ReDim FlechesUZ(nbNodes - 1)

        '--> Combinaisons

        For iCas = 0 To nbCharges - 1
            If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                plETA = lETA And (Not IsNothing(ChargesA(iCas).UZEta))

                If plETA Then
                    For jNode = 0 To nbNodes - 1
                        FlechesUZ(jNode) += Me.CoefCombi(iCombi)(iCas) * ChargesA(iCas).UZEta(jNode)
                    Next
                Else
                    For jNode = 0 To nbNodes - 1
                        FlechesUZ(jNode) += Me.CoefCombi(iCombi)(iCas) * ChargesA(iCas).UZ(jNode)
                    Next
                End If


            End If

        Next

    End Sub

    Public Sub CombineContraintes(iCombi As Integer, NbCas As Integer, NbPts As Integer, nbNodes As Integer,
                                  ChargesA As List(Of cls_CasDeCharge), SigmaCas(,,,) As Decimal,
                                  lRetrait As Boolean, ByRef SigmaELU(,,) As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Combine les contraintes - Cas général pour les poutres mixtes avec prise en compte du signe du moment
        '-----------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison à traiter
        '   NbCas       [E] :   Nombre de cas de charges élémentaires
        '   NbPts       [E] ;   Nombre de points de calcul des contraintes
        '   nbNodes     [E] :   Nombre de noeuds dans la modélisation
        '   ChargesA    [E] :   Tableaux des cas de charges (qui doivent avoir été calculés auparavant
        '   SigmaCas    [E] :   Table des contraintes élastiques sous cas de charges élémentaires
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '   SigmaELU    [S] :   Table des contraintes combinées
        '-----------------------------------------------------------------------------------------------------------
        '   Dimensions de SigmaCas          :    (NbCas - 1, NbPts - 1, NbNodes - 1, 1)
        '   Dimensions de SigmaELU         :    (NbPts - 1, NbNodes - 1, 1)
        '-----------------------------------------------------------------------------------------------------------

        '--> Initialisation

        ReDim SigmaELU(NbPts - 1, nbNodes - 1, 1)

        '--> Combinaisons

        For jNode = 0 To nbNodes - 1
            For k = 0 To 1

                For iCas = 0 To NbCas - 1
                    If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                        For iPts = 0 To NbPts - 1
                            SigmaELU(iPts, jNode, k) += Me.CoefCombi(iCombi)(iCas) * SigmaCas(iCas, iPts, jNode, k)
                        Next

                    End If
                Next

            Next
        Next

    End Sub

    Public Sub CombineContraintes(iCombi As Integer, NbCas As Integer, NbPts As Integer, nbNodes As Integer, ChargesA As List(Of cls_CasDeCharge),
                                  Med(,) As Decimal, SigmaP(,,,) As Decimal, SigmaM(,,,) As Decimal,
                                  lRetrait As Boolean, ByRef SigmaELU(,,) As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Combine les contraintes - Cas général pour les poutres mixtes avec prise en compte du signe du moment
        '-----------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison à traiter
        '   NbCas       [E] :   Nombre de cas de charges élémentaires
        '   NbPts       [E] ;   Nombre de points de calcul des contraintes
        '   nbNodes     [E] :   Nombre de noeuds dans la modélisation
        '   ChargesA    [E] :   Tableaux des cas de charges (qui doivent avoir été calculés auparavant
        '   MEd         [E] :   Table des moments ELU (déjà combinés)
        '   SigmaP      [E] :   Table des contraintes élastiques sous cas de charges élémentaires, hyp de M > 0
        '   SigmaM      [E] :   Table des contraintes élastiques sous cas de charges élémentaires, hyp de M < 0
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '   SigmaELU    [S] :   Table des contraintes combinées
        '-----------------------------------------------------------------------------------------------------------
        '   Dimensions de SigmaP et SigmaM :    (NbCas - 1, NbPts - 1, NbNodes - 1, 1)
        '   Dimensions de SigmaELU         :    (NbPts - 1, NbNodes - 1, 1)
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iCas, jNode, iPts, k As Integer
        Const CONVSIGNE As Decimal = 1

        '--> Initialisation

        ReDim SigmaELU(NbPts - 1, nbNodes - 1, 1)

        '--> Combinaisons

        For jNode = 0 To nbNodes - 1
            For k = 0 To 1

                If CONVSIGNE * Med(jNode, k) > 0 Then
                    '== MOMENTS POSITIFS =====================================================================================
                    For iCas = 0 To NbCas - 1
                        If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                            For iPts = 0 To NbPts - 1
                                SigmaELU(iPts, jNode, k) += Me.CoefCombi(iCombi)(iCas) * SigmaP(iCas, iPts, jNode, k)
                            Next

                        End If
                    Next
                Else
                    '== MOMENTS NEGATIFS =====================================================================================
                    For iCas = 0 To NbCas - 1
                        If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                            For iPts = 0 To NbPts - 1
                                SigmaELU(iPts, jNode, k) += Me.CoefCombi(iCombi)(iCas) * SigmaM(iCas, iPts, jNode, k)
                            Next

                        End If
                    Next

                    '=========================================================================================================

                End If

            Next
        Next

    End Sub

    Public Sub CombineMoments(iCombi As Integer, nbNodes As Integer, ChargesA As List(Of cls_CasDeCharge), ByRef MomMy(,) As Decimal, Optional lRetrait As Boolean = True)
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Combine les moments
        '-----------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison à traiter
        '   nbNodes     [E] :   Nombre de noeuds dans la modélisation
        '   ChargesA    [E] :   Tableaux des cas de charges (qui doivent avoir été calculés auparavant
        '   MomMy       [S] :   Table des moments My
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbCharges As Integer = ChargesA.Count
        Dim iCas, jNode, k As Integer

        '--> Initialisation

        ReDim MomMy(nbNodes - 1, 1)

        '--> Combinaisons

        For iCas = 0 To nbCharges - 1
            If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                For jNode = 0 To nbNodes - 1
                    For k = 0 To 1
                        MomMy(jNode, k) += Me.CoefCombi(iCombi)(iCas) * ChargesA(iCas).MYY(jNode, k)
                    Next
                Next

            End If
        Next

    End Sub

    Public Sub CombineEffortsT(iCombi As Integer, nbNodes As Integer, ChargesA As List(Of cls_CasDeCharge), ByRef EffVz(,) As Decimal, Optional lRetrait As Boolean = True)
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Combine les flèches
        '-----------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison à traiter
        '   nbNodes     [E] :   Nombre de noeuds dans la modélisation
        '   ChargesA    [E] :   Tableaux des cas de charges (qui doivent avoir été calculés auparavant
        '   EffVz       [S] :   Table des efforts tranchants
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbCharges As Integer = ChargesA.Count
        Dim iCas, jNode, k As Integer

        '--> Initialisation

        ReDim EffVz(nbNodes - 1, 1)

        '--> Combinaisons

        For iCas = 0 To nbCharges - 1
            If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                For jNode = 0 To nbNodes - 1
                    For k = 0 To 1
                        EffVz(jNode, k) += Me.CoefCombi(iCombi)(iCas) * ChargesA(iCas).VZ(jNode, k)
                    Next
                Next

            End If
        Next

    End Sub

    Public Sub RecupererEffortsNodauxPonderees(myNodes As cls_Poutre.strucBeamNodes, Vz(,) As Decimal, ByRef Q() As Decimal, ByRef qLin() As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   06/05/24 :  Création - GUD
        '-----------------------------------------------------------------------------------------------------------
        '   Calcul les efforts nodaux d'un model EF a partir des tranchants
        '-----------------------------------------------------------------------------------------------------------
        '   nbNodes     [E] :   Nombre de noeuds
        '   Vz          [E] :   Table des efforts tranchants au droit de chaque noeud, issus du calcul
        '   Q           [S] :   Effort local associé à chaque noeud
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Q_lin_gauche, Q_lin_droite, Q_ponctuel As Decimal
        Dim DeltaX As Decimal
        Dim nbNodes As Integer

        '--> Initialisation

        nbNodes = myNodes.nbNodes
        ReDim Q(nbNodes - 1)
        ReDim qLin(nbNodes - 1)

        '--> Calcul 

        For jNode = 0 To nbNodes - 1
            If (jNode = 0) Then
                Q_lin_gauche = 0
                Q_lin_droite = (Vz(jNode + 1, 0) - Vz(jNode, 1)) / 2
                Q_ponctuel = 0
                DeltaX = (myNodes.xGlobal(jNode + 1) - myNodes.xGlobal(jNode)) / 2
            ElseIf (jNode = nbNodes - 1) Then
                Q_lin_gauche = (Vz(jNode, 0) - Vz(jNode - 1, 1)) / 2
                Q_lin_droite = 0
                Q_ponctuel = 0
                DeltaX = (myNodes.xGlobal(jNode) - myNodes.xGlobal(jNode - 1)) / 2
            Else
                Q_lin_gauche = (Vz(jNode, 0) - Vz(jNode - 1, 1)) / 2
                Q_lin_droite = (Vz(jNode + 1, 0) - Vz(jNode, 1)) / 2
                Q_ponctuel = Vz(jNode, 1) - Vz(jNode, 0)
                DeltaX = (myNodes.xGlobal(jNode + 1) - myNodes.xGlobal(jNode - 1)) / 2
            End If

            'If (jNode = 0) Or (jNode = nbNodes - 1) Then
            '    Q_ponctuel = 0
            'Else
            '    Q_ponctuel = Vz(jNode, 1) - Vz(jNode, 0)
            'End If

            'If jNode = nbNodes - 1 Then
            '    Q_lin_droite = 0
            'Else
            '    Q_lin_droite = (Vz(jNode + 1, 0) - Vz(jNode, 1)) / 2
            'End If

            Q(jNode) = Q_lin_gauche + Q_ponctuel + Q_lin_droite
            qLin(jNode) = Q(jNode) / DeltaX
        Next

    End Sub
    Public Sub CombineReactions(iCombi As Integer, nbAppuis As Integer, ChargesA As List(Of cls_CasDeCharge), ByRef ReacRz() As Decimal, Optional lRetrait As Boolean = True)
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Combine les flèches
        '-----------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison à traiter
        '   nbNodes     [E] :   Nombre de noeuds dans la modélisation
        '   ChargesA    [E] :   Tableaux des cas de charges (qui doivent avoir été calculés auparavant
        '   EffVz       [S] :   Table des efforts tranchants
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbCharges As Integer = ChargesA.Count
        Dim iCas, jNode As Integer

        '--> Initialisation

        ReDim ReacRz(nbAppuis - 1)

        '--> Combinaisons

        For iCas = 0 To nbCharges - 1
            If (Not IsEqual(Me.CoefCombi(iCombi)(iCas), 0)) And lCombineCas(ChargesA(iCas), lRetrait) Then

                For jNode = 0 To nbAppuis - 1

                    ReacRz(jNode) += Me.CoefCombi(iCombi)(iCas) * ChargesA(iCas).RZ(jNode)

                Next

            End If
        Next

    End Sub

    Private Function lCombineCas(MyChargesA As cls_CasDeCharge, lRetrait As Boolean) As Boolean
        '-----------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Indique si on combine le cas de charge
        '-----------------------------------------------------------------------------------------------------------
        '   MyChargesA  [E] :   Cas de charge
        '   lRetrait    [E] :   Indique si on prend en compte les charges de retrait
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lCombiCas As Boolean = False

        '--> Est ce que le cas a bien été calculé

        If MyChargesA.lRunCalcul Then
            '# Si oui, est ce un cas de type de retrait ?
            If MyChargesA.Type = cls_CasDeCharge.EnuType.Retrait Then
                lCombiCas = lRetrait
            Else
                lCombiCas = True
            End If
        End If

        Return lCombiCas
    End Function

#End Region

#Region " Fonctions de copie "

    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    'Public Shared Sub DeepClone(ByVal CombinaisonSource As cls_Combinaisons, ByRef CombinaisonCible As cls_Combinaisons)
    '    CombinaisonCible = CombinaisonSource.Clone()

    '    ReDim CombinaisonCible.Symbole(CombinaisonSource.Symbole.GetUpperBound(0))
    '    CombinaisonCible.Symbole = CombinaisonSource.Symbole.Clone

    '    For i As Integer = 0 To CombinaisonSource.CoefCombi.Length - 1
    '        CombinaisonCible.CoefCombi(i) = New List(Of Decimal)
    '        For Each elmnt In CombinaisonSource.CoefCombi(i)
    '            CombinaisonCible.CoefCombi(i).Add(elmnt)
    '        Next
    '    Next
    'End Sub

#End Region

End Class
