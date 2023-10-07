Public Class cls_Critere

#Region " Attributs "

    Public Critere() As Decimal         ' Valeur du critere le long de la barre
    Public Action() As Decimal          ' Valeur de l'action donnant le critère max en un point donné
    Public Resistance() As Decimal      ' Valeur de l'action donnant le critère max en un point donné
    Public iCombiNodeM() As Integer     ' Indice de la combinaison donnant le critère max en un point donné

    Public CritereMax As Decimal        ' Valeur maximale du critere
    Public iCombiM As Integer           ' Indice de la combinaison donnant le critère max
    Public iNodeM As Integer            ' Indice du noeuds ou on obtient le critère max

    Public lDefini As Boolean           ' Indique si le critère a été utilisé

#End Region

#Region " Constructeur "

    Public Sub New(NbNodes As Integer)

        ReDim Critere(NbNodes - 1)
        ReDim Action(NbNodes - 1)
        ReDim Resistance(NbNodes - 1)
        ReDim iCombiNodeM(NbNodes - 1)

        iCombiM = -1
        iNodeM = -1
        CritereMax = 0
        lDefini = False

    End Sub

#End Region

#Region " Outils "

    Public Sub EnregistreCritere(iNode As Integer, iCombi As Integer, ValEd As Decimal, ValRd As Decimal)
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

        If pCrit > Me.Critere(iNode) Then
            Me.Critere(iNode) = pCrit
            Me.Action(iNode) = Math.Abs(ValEd)
            Me.Resistance(iNode) = Math.Abs(ValRd)
            Me.iCombiNodeM(iNode) = iCombi
        End If

        If pCrit > Me.CritereMax Then
            lDefini = True
            Me.CritereMax = pCrit
            Me.iCombiM = iCombi
            Me.iNodeM = iNode
        End If

    End Sub

#End Region

End Class
