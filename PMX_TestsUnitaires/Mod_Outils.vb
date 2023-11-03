Module Mod_Outils

#Region " Outils de COMPARaison "

    Public Const DeltaVMAx As Decimal = 1 / 1000

    Public Function IsEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        If Math.Abs(b) <= EPS Then
            'AVEC DIMENSION
            Return Math.Abs(a) <= EPS
        Else
            'ATTENTION : Lorsqu'on compare la fraction (PAS DE DIMENSION), il faut utiliser 0.001
            Return Math.Abs(a / b - 1) <= 0.001
        End If
    End Function

    Private Function IsGreater(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return (Not IsEqual(a, b, EPS)) AndAlso (a > b)
    End Function

    Private Function IsGreaterOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a > b)

    End Function

    Private Function IsSmaller(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------
        Dim lIsSmaller As Boolean = (Not IsEqual(a, b, EPS)) AndAlso (a < b)

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return lIsSmaller
    End Function

    Private Function IsSmallerOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean

        '------------------------------------------
        ' 29/11/2013
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a < b)
    End Function

#End Region
End Module
