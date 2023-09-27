Public Class cls_CombinaisonA

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
End Class
