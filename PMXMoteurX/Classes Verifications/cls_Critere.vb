Public Class cls_Critere

#Region " Attributs "

    Public Critere() As Decimal         ' Valeur du critere le long de la barre pour la combinaison en cours
    Public Action() As Decimal          ' Valeur de l'action donnant le critère max en un point donné pour la combinaison en cours
    Public Resistance() As Decimal      ' Valeur de l'action donnant le critère max en un point donné pour la combinaison en cours
    Public iCombiNodeM() As Integer     ' Indice de la combinaison donnant le critère max en un point donné

    Public CritereMax As Decimal        ' Valeur maximale du critere
    Public iCombiM As Integer           ' Indice de la combinaison donnant le critère max
    Public iNodeM As Integer            ' Indice du noeuds ou on obtient le critère max

    Public lDefini As Boolean           ' Indique si le critère a été utilisé

    Public CritereCombiT(,) As Decimal  ' Table donnant la valeur maxi du critère par combinaison et par travée (indice 1 : combi/indice 2: travée)
    Public CritereCombiN(,) As Decimal  ' Table donnant l'indice du noeud où le critere maxi par combinaison et par travée est obtenu

#End Region

#Region " Constructeur "

    Public Sub New(NbNodes As Integer, nbCombi As Integer, indDerniereT As Integer)

        ReDim Critere(NbNodes - 1)
        ReDim Action(NbNodes - 1)
        ReDim Resistance(NbNodes - 1)
        ReDim iCombiNodeM(NbNodes - 1)

        iCombiM = -1
        iNodeM = -1
        CritereMax = 0
        lDefini = False

        ReDim CritereCombiT(nbCombi - 1, indDerniereT)
        ReDim CritereCombiN(nbCombi - 1, indDerniereT)

    End Sub

#End Region

#Region " Outils "

    Public Sub EnregistreCritere(iNode As Integer, iCombi As Integer, iTravee As Integer, ValEd As Decimal, ValRd As Decimal)
        '--------------------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------------
        '   Gere la valeur d'un critère au droit d'un noeud
        '--------------------------------------------------------------------------------------------------------
        '   iNode   [E] :   Indice du noeud
        '   iCombi  [E] :   Indice de la combinaison
        '   ValEd   [E] :   Valeur de l'effet
        '   ValRd   [E] :   Valeur de la résistance
        '--------------------------------------------------------------------------------------------------------

        '--> Déclaration 

        Dim pCrit As Decimal

        '--> Initialisation, calcul du critere

        If IsEqual(ValRd, 0) Then
            pCrit = 9999
        Else
            pCrit = Math.Abs(ValEd / ValRd)
        End If

        '# Valeur maximale au noeud

        If pCrit > Me.Critere(iNode) Then
            Me.Critere(iNode) = pCrit
            Me.Action(iNode) = Math.Abs(ValEd)
            Me.Resistance(iNode) = Math.Abs(ValRd)
            Me.iCombiNodeM(iNode) = iCombi
        End If

        '# Valeur maximale du critere

        If pCrit > Me.CritereMax Then
            lDefini = True
            Me.CritereMax = pCrit
            Me.iCombiM = iCombi
            Me.iNodeM = iNode
        End If

        '# Valeur maximale du critere pour la combi et la travée

        If pCrit > Me.CritereCombiT(iCombi, iTravee) Then
            Me.CritereCombiT(iCombi, iTravee) = pCrit
            Me.CritereCombiN(iCombi, iTravee) = iNode
        End If

    End Sub

    Public Sub EnveloppeCritereTravee(iNode1 As Integer, iNode2 As Integer, ByRef CritMax As Decimal, ByRef NodeMax As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------------
        '   Renvoie la valeur max du critère pour une travée (entre deux noeuds)
        '---------------------------------------------------------------------------------------------------------------------------
        '   iNode1, iNode2  [E] :   Noeuds limite du tronçon à explorer
        '   CritMax         [S] :   Valeur maxi du critère sur le tronçon
        '   NodeMax         [S] :   Indice du noeud donnant la valeur maxi
        '---------------------------------------------------------------------------------------------------------------------------

        '--> Initialisation

        CritMax = Me.Critere(iNode1)
        NodeMax = iNode1

        '--> Recherche

        For i As Integer = iNode1 + 1 To iNode2
            If Me.Critere(i) > CritMax Then
                CritMax = Me.Critere(i)
                NodeMax = i
            End If
        Next

    End Sub

#End Region

#Region " Outils "

    Public Sub EnveloppeCritereCombi(myCritere As cls_Critere, iCombi As Integer, iTravDeb As Integer, iTravFin As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   13/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Comparaison avec un autre critère
        '   On établit l'enveloppe des valeurs pour les critères par travée et combi
        '   ainsi que pour la valeur maxi
        '----------------------------------------------------------------------------------------------------------
        '   myCritere   [E] :   Critère à comparer
        '   iCombi      [E] :   Indique de la combi traitée
        '   iTravDeb    [E] :   Indice de la première travée
        '   iTravFin    [E] :   Indice de la dernière travée
        '----------------------------------------------------------------------------------------------------------

        If IsGreater(myCritere.CritereMax, Me.CritereMax) Then
            Me.CritereMax = myCritere.CritereMax
            Me.iCombiM = myCritere.iCombiM
            Me.iNodeM = myCritere.iNodeM
        End If

        For i As Integer = iTravDeb To iTravFin
            If IsGreater(myCritere.CritereCombiT(iCombi, i), Me.CritereCombiT(iCombi, i)) Then
                Me.CritereCombiT(iCombi, i) = myCritere.CritereCombiT(iCombi, i)
                Me.CritereCombiN(iCombi, i) = myCritere.CritereCombiN(iCombi, i)
            End If
        Next

    End Sub



#End Region

End Class
