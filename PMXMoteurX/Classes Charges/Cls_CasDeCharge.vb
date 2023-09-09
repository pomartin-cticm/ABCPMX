Public Class cls_CasDeCharge

    '#### CLASSE POUR LA CALCUL D'UN CHARGEMENT PAR LE LOGICIEL ######

#Region " Attributs "

    '--> Identifications

    Public Nom As String                                ' Dénomination dans la langue utilisateur du cas de charge
    Public Symbol As String                             ' Symbole

    '--> Paramètres de modélisation

    Public IndElts As Integer                           ' Indice de la table BeamElements contenant les propriétés des barres

    '--> Charges

    Public QSurf() As Decimal                           ' Charge par unité de surface sur chaque travée
    Public Forces() As List(Of cls_Force)               ' Liste des efforts ponctuels sur chaque travée
    Public FReparties() As List(Of cls_ForceRepartie)   ' Liste des charges réparties sur chaque travée
    Public Moments() As List(Of cls_Moment)             ' Liste des moments ponctuels sur chaque travée (a priori, pour définir le retrait)

    '--> Résultats de l'analyse

    Public VZ(,) As Decimal                             ' Effort tranchant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)
    Public MYY(,) As Decimal                            ' Moment fléchissant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)

    Public UZ() As Decimal                              ' Déplacement vertical du nœud i (0 à NbNodes-1)
    Public ROTY() As Decimal                            ' Rotation du nœud i (0 à NbNodes-1)

    Public RZ() As Decimal                              ' Réactions verticales aux nœuds support (0 à NbAppuis-1)

    Public lRunCalcul As Boolean                        ' Indique sir le calcul a été effectué
#End Region

#Region " Constructeurs "

    Public Sub New(pNom As String, pSymbol As String, IndiceElts As Integer, iTrav0 As Integer, NbTrav As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Initialisation du cas de charge
        '-----------------------------------------------------------------------------------------------------------
        '   pNom        [E] :   Nom du cas de charge (selon langue interface)
        '   pSymbol     [E] :   Symbol du cas de charge (indépendant de la langue)
        '   IndiceElts  [E] :   Indice de la table de propriétés des éléments associées au cas de charge
        '   NbTrav      [E] :   Nombre de travées dans la poutre (pour le dimensionnement des tableaux)
        '   iTrav0      [E] :   Indice de la première travée
        '-----------------------------------------------------------------------------------------------------------

        Me.Nom = pNom
        Me.Symbol = pSymbol

        Me.IndElts = IndiceElts

        ReDim Me.QSurf(NbTrav + iTrav0)
        ReDim Me.Forces(NbTrav + iTrav0)
        ReDim Me.Moments(NbTrav + iTrav0)
        ReDim Me.FReparties(NbTrav + iTrav0)

        For i As Integer = iTrav0 To iTrav0 + NbTrav
            Me.Forces(i) = New List(Of cls_Force)
            Me.Moments(i) = New List(Of cls_Moment)
            Me.FReparties(i) = New List(Of cls_ForceRepartie)
        Next

        Me.lRunCalcul = False
    End Sub

#End Region

#Region " Outils "

    Public Function EstNonNul(iTravP As Integer, iTravD As Integer) As Boolean
        '-----------------------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Indique si le cas de charge est non nul (c'est à dire qu'il exite au moins une charge non nulle)
        '-----------------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice de la première travée
        '   iTravD      [E] :   Indice de la dernière travée
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lNonNul As Boolean = False
        Dim iTrav, iCharg As Integer

        For iTrav = iTravP To iTravD
            If Not IsEqual(Me.QSurf(iTrav), 0) Then lNonNul = True
            For iCharg = 0 To Me.Forces(iTrav).Count - 1
                If Not IsEqual(Me.Forces(iTrav)(iCharg).Force, 0) Then lNonNul = True
            Next
            For iCharg = 0 To Me.Moments(iTrav).Count - 1
                If Not IsEqual(Me.Moments(iTrav)(iCharg).Moment, 0) Then lNonNul = True
            Next
            For iCharg = 0 To Me.FReparties(iTrav).Count - 1
                If Not IsEqual(Me.FReparties(iTrav)(iCharg).Force(0), 0) Then lNonNul = True
                If Not IsEqual(Me.FReparties(iTrav)(iCharg).Force(1), 0) Then lNonNul = True
            Next
        Next


        Return lNonNul
    End Function

    Public Sub RecupereResultats(MyResults As CTICM_RDM.DATA_RDM.Struc_Output, NbNodes As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Stocke les résultats issus du calcul EF
        '-----------------------------------------------------------------------------------------------------------

        'ReDim Me.VZ(NbNodes - 1, 1)
        'ReDim Me.MYY(NbNodes - 1, 1)
        'ReDim Me.UZ(NbNodes - 1)

        Me.VZ = MyResults.VZ.Clone
        Me.MYY = MyResults.MYY.Clone
        Me.ROTY = MyResults.ROTY.Clone
        Me.UZ = MyResults.UZ.Clone
        Me.RZ = MyResults.RZ.Clone

        Me.lRunCalcul = True

    End Sub

    Public Function FlecheMax() As Decimal
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie la flèche max (valeur absolue) issue des résultats du calcul EF
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Fleche As Decimal = 0
        Dim NbNodes As Integer

        '--> Traitement

        If Me.lRunCalcul Then

            NbNodes = Me.UZ.GetUpperBound(0) + 1
            Fleche = Math.Abs(Me.UZ(0))
            For iNode As Integer = 1 To NbNodes - 1
                Fleche = Math.Max(Math.Abs(UZ(iNode)), Fleche)
            Next

        End If

        Return Fleche

    End Function

    Public Sub EnveloppesFleche(ByRef fMax As Decimal, ByRef fMin As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les flèches enveloppes issues des résultats du calcul EF
        '-----------------------------------------------------------------------------------------------------------
        '   fMax        [S] :   Valeur max de la flèche
        '   fMin        [S] :   Valeur min de la flèche
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer

        '--> Traitement

        If Me.lRunCalcul Then

            NbNodes = Me.UZ.GetUpperBound(0) + 1
            fMax = Me.UZ(0)
            fMin = Me.UZ(0)
            For iNode As Integer = 1 To NbNodes - 1
                fMax = Math.Max(UZ(iNode), fMax)
                fMin = Math.Min(UZ(iNode), fMin)
            Next

        End If

    End Sub

    Public Sub EnveloppesMoments(ByRef Mmax As Decimal, ByRef Mmin As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les flèches enveloppes issues des résultats du calcul EF
        '-----------------------------------------------------------------------------------------------------------
        '   fMax        [E] :   Valeur max de la flèche
        '   fMin        [E] :   Valeur min de la flèche
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer

        '--> Calcul

        If Me.lRunCalcul Then
            NbNodes = Me.UZ.GetUpperBound(0) + 1
            Me.EnveloppeTableau(Me.MYY, NbNodes, Mmax, Mmin)
        End If

    End Sub

    Private Sub EnveloppeTableau(MyTab(,) As Decimal, NbNodes As Integer, ByRef ValMax As Decimal, ByRef ValMin As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les valeurs enveloppes d'un tableau à 2 dimensions
        '-----------------------------------------------------------------------------------------------------------
        '   MyTab       [E] :   Tableau à traiter
        '   NbNodes     [E] :   Dimension 1 du tableau
        '   ValMax      [S] :   Valeur max du tableau
        '   ValMin      [S] :   Valeur min du tableau
        '-----------------------------------------------------------------------------------------------------------

        ValMax = Math.Max(MyTab(0, 1), MyTab(NbNodes - 1, 0))
        ValMin = Math.Min(MyTab(0, 1), MyTab(NbNodes - 1, 0))

        For i As Integer = 1 To NbNodes - 2
            For j = 0 To 1
                ValMax = Math.Max(MyTab(i, j), ValMax)
                ValMin = Math.Min(MyTab(i, j), ValMin)
            Next
        Next

    End Sub

    Public Function NombreFRep(iTravP As Integer, iTravD As Integer) As Integer
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie le nombre total de forces réparties dans le cas de charge
        '-----------------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice de la première travée
        '   iTravD      [E] :   Indice de la dernière travée
        '-----------------------------------------------------------------------------------------------------------

        Dim Nombre As Integer = 0

        For iTrav = iTravP To iTravD
            Nombre += Me.FReparties(iTrav).Count
        Next

        Return Nombre
    End Function

#End Region

End Class
