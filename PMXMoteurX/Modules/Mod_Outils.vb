Public Module Mod_Outils

#Region " Outils Divers "
    Public Sub AjoutePoint(x As Single, y As Single, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   31/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------


        nbPts += 1
        If nbPts = 1 Then
            ReDim xPts(0)
            ReDim yPts(0)
        Else
            ReDim Preserve xPts(nbPts - 1)
            ReDim Preserve yPts(nbPts - 1)
        End If
        xPts(nbPts - 1) = x
        yPts(nbPts - 1) = y

    End Sub

#End Region

#Region " Outils de COMPARaison "

    Const DeltaVMAx As Decimal = 0.001
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

    Public Function IsGreater(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return (Not IsEqual(a, b, EPS)) AndAlso (a > b)
    End Function

    Public Function IsGreaterOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a > b)

    End Function

    Public Function IsSmaller(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------
        Dim lIsSmaller As Boolean = (Not IsEqual(a, b, EPS)) AndAlso (a < b)

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return lIsSmaller
    End Function

    Public Function IsSmallerOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean

        '------------------------------------------
        ' 29/11/2013
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a < b)
    End Function

    '--> Mise en commentaire car a priori pas utilisé, à discuter

    'Public Sub EnveloppeTableauEfforts(MyTab(,) As Decimal, NbNodes As Integer, ByRef ValMax As Decimal, ByRef ValMin As Decimal)
    '    '-----------------------------------------------------------------------------------------------------------
    '    '   09/09/23 :  Création - POM
    '    '-----------------------------------------------------------------------------------------------------------
    '    '   Renvoie les valeurs enveloppes d'un tableau à 2 dimensions
    '    '-----------------------------------------------------------------------------------------------------------
    '    '   MyTab       [E] :   Tableau à traiter
    '    '   NbNodes     [E] :   Dimension 1 du tableau
    '    '   ValMax      [S] :   Valeur max du tableau
    '    '   ValMin      [S] :   Valeur min du tableau
    '    '-----------------------------------------------------------------------------------------------------------

    '    ValMax = Math.Max(MyTab(0, 1), MyTab(NbNodes - 1, 0))
    '    ValMin = Math.Min(MyTab(0, 1), MyTab(NbNodes - 1, 0))

    '    For i As Integer = 1 To NbNodes - 2
    '        For j = 0 To 1
    '            ValMax = Math.Max(MyTab(i, j), ValMax)
    '            ValMin = Math.Min(MyTab(i, j), ValMin)
    '        Next
    '    Next

    'End Sub

    Public Sub EnveloppeTableauEfforts(MyTab(,) As Decimal, NbNodes As Integer, ByRef ValMax As Decimal, ByRef ValMin As Decimal,
                                 ByRef iNodeValMax As Integer, ByRef iNodeValMin As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les valeurs enveloppes d'un tableau à 2 dimensions
        '-----------------------------------------------------------------------------------------------------------
        '   MyTab       [E] :   Tableau à traiter
        '   NbNodes     [E] :   Dimension 1 du tableau
        '   ValMax      [S] :   Valeur max du tableau
        '   ValMin      [S] :   Valeur min du tableau
        '   iNodeValMax [S] :   Indice du noeud pour la valeur maxi
        '   iNodeValMin [S] :   Indice du noeud pour la valeur mini
        '-----------------------------------------------------------------------------------------------------------

        'ValMax = Math.Max(MyTab(0, 1), MyTab(NbNodes - 1, 0))
        'ValMin = Math.Min(MyTab(0, 1), MyTab(NbNodes - 1, 0))
        'iNodeValMax = 0
        'iNodeValMin = 0

        If IsGreater(MyTab(0, 1), MyTab(NbNodes - 1, 0)) Then 'Ajout GUD : induit un BUG quand la val max se trouve au droit des appuis d extremités 
            ValMax = MyTab(0, 1)
            ValMin = MyTab(NbNodes - 1, 0)
            iNodeValMax = 0
            iNodeValMin = NbNodes - 1
        Else
            ValMax = MyTab(NbNodes - 1, 0)
            ValMin = MyTab(0, 1)
            iNodeValMax = NbNodes - 1
            iNodeValMin = 0
        End If
        For i As Integer = 1 To NbNodes - 2
            For j = 0 To 1
                If IsGreater(MyTab(i, j), ValMax) Then
                    ValMax = MyTab(i, j)
                    iNodeValMax = i
                End If
                If IsSmaller(MyTab(i, j), ValMin) Then
                    ValMin = MyTab(i, j)
                    iNodeValMin = i
                End If
            Next
        Next

    End Sub

#End Region

#Region " Gestion des erreurs "

    Public Sub GestionErreur(strMod As String, Routine As String, Erreur As String)
        '-----------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Gestion des erreurs captées par le logiciel
        '-----------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------

        MsgBox(Erreur & " [" & strMod & " | " & Routine & "]")

    End Sub



#End Region

End Module
