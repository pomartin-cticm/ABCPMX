Module Mod_Tools

    Private Const EPS_Default As Double = 0.0001
#Region " COMPARE "

    Public Function IsEqual(ByVal a As Double, ByVal b As Double, Optional ByVal EPS As Double = EPS_Default) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        If Math.Abs(b) <= EPS Then
            'AVEC DIMENSION
            Return Math.Abs(a) <= EPS
        Else
            'ATTENTION : Lorsqu'on compare la fraction (PAS DE DIMENSION), il faut utiliser EPS_Default
            Return Math.Abs(a / b - 1) <= EPS_Default
        End If
    End Function

    Public Function IsGreater(ByVal a As Double, ByVal b As Double, Optional ByVal EPS As Double = EPS_Default) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return (Not IsEqual(a, b, EPS)) AndAlso (a > b)
    End Function

    Public Function IsGreaterOrEqual(ByVal a As Double, ByVal b As Double, Optional ByVal EPS As Double = EPS_Default) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a > b)

    End Function

    Public Function IsSmaller(ByVal a As Double, ByVal b As Double, Optional ByVal EPS As Double = EPS_Default) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------
        Dim lIsSmaller As Boolean = (Not IsEqual(a, b, EPS)) AndAlso (a < b)

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return lIsSmaller
    End Function

    Public Function IsSmallerOrEqual(ByVal a As Double, ByVal b As Double, Optional ByVal EPS As Double = EPS_Default) As Boolean

        '------------------------------------------
        ' 29/11/2013
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a < b)
    End Function

#End Region

End Module
