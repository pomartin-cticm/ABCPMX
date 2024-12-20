Public Class cls_CasDeCharge

    '#### CLASSE POUR LA CALCUL D'UN CHARGEMENT PAR LE LOGICIEL ######

#Region " Attributs "

    Enum EnuType
        Exploitation
        Permanente
        Construction
        Retrait
    End Enum

    Enum EnuEtatDalle
        Mixte
        Acier
    End Enum

    '--> Identifications

    Public Nom As String                                ' Dénomination dans la langue utilisateur du cas de charge
    Public Symbol As String                             ' Symbole

    '--> Paramètres de modélisation

    Public IndElts As Integer                           ' Indice de la table BeamElements contenant les propriétés des barres
    Public Type As EnuType                              ' Type de chargement
    Public EtatDalle As EnuEtatDalle                    ' Indique l'état de la dalle pour le cas de charge (acier = pas de mixité avec la dalle)

    '--> Charges

    Public QSurf() As Decimal                           ' Charge par unité de surface sur chaque travée
    Public Forces() As List(Of cls_Force)               ' Liste des efforts ponctuels sur chaque travée
    Public FReparties() As List(Of cls_ForceRepartie)   ' Liste des charges réparties sur chaque travée
    Public Moments() As List(Of cls_Moment)             ' Liste des moments ponctuels sur chaque travée (a priori, pour définir le retrait)

    '--> Résultats de l'analyse

    Public VZ(,) As Decimal                             ' Effort tranchant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)
    Public MYY(,) As Decimal                            ' Moment fléchissant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)

    Public UZ() As Decimal                              ' Déplacement vertical du nœud i (0 à NbNodes-1)
    Public UZEta() As Decimal                           ' Déplacement vertical du nœud i (0 à NbNodes-1), avec prise en compte du degré de connexion
    Public ROTY() As Decimal                            ' Rotation du nœud i (0 à NbNodes-1)

    Public RZ() As Decimal                              ' Réactions verticales aux nœuds support (0 à NbAppuis-1)

    Public lRunCalcul As Boolean                        ' Indique sir le calcul a été effectué

    'Public Sigma(,,) As Decimal                        ' Contraintes normales aux points de calcul

    '--> Gestion des cas de charges "Shadow", permettant le calcul de la flèche prenant en compte le degré de connexion

    Public lShadow As Boolean                           ' Indique si le cas de charge est un shadow, c'est à dire le double d'un vrai cas de charge (double permettant le calcul de la flèche eta)
    Public iShadow As Integer                           ' Pour les cas de charges shadow, indice du cas de charges réel dont il est la doublure
    '                                                     La flèche d'un cas de charge shadow est stockée dans la table UZEta du cas réel

#End Region

#Region " Constructeurs "

    Public Sub New(pNom As String, pSymbol As String, IndShadow As Integer, IndEltShadow As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   03/02/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Initialisation d'un cas de charge shadow
        '-----------------------------------------------------------------------------------------------------------
        '   pNom        [E] :   Nom du cas de charge (selon langue interface)
        '   pSymbol     [E] :   Symbol du cas de charge (indépendant de la langue)
        '   IndShadow   [E] :   Indice du cas de charge dédoublé
        '   IndEltShadow[E] :   Indice de la table de propriétés des éléments associées au cas de charge
        '-----------------------------------------------------------------------------------------------------------

        Me.Nom = pNom
        Me.Symbol = pSymbol

        Me.lShadow = True
        Me.IndElts = IndEltShadow
        Me.iShadow = IndShadow

        Me.UZEta = Nothing

    End Sub

    Public Sub New(pNom As String, pSymbol As String, IndiceElts As Integer, iTrav0 As Integer, NbTrav As Integer,
                   pType As EnuType, pEtatDalle As EnuEtatDalle)
        '-----------------------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Initialisation d'un cas de charge normal
        '-----------------------------------------------------------------------------------------------------------
        '   pNom        [E] :   Nom du cas de charge (selon langue interface)
        '   pSymbol     [E] :   Symbol du cas de charge (indépendant de la langue)
        '   IndiceElts  [E] :   Indice de la table de propriétés des éléments associées au cas de charge
        '   NbTrav      [E] :   Nombre de travées dans la poutre (pour le dimensionnement des tableaux)
        '   iTrav0      [E] :   Indice de la première travée
        '   pType       [E] :   Type du chargement
        '   pEtatDalle  [E] :   Etat de la dalle pour le chargement
        '-----------------------------------------------------------------------------------------------------------

        Me.Nom = pNom
        Me.Symbol = pSymbol

        Me.IndElts = IndiceElts

        ReDim Me.QSurf(NbTrav + iTrav0 - 1)
        ReDim Me.Forces(NbTrav + iTrav0 - 1)
        ReDim Me.Moments(NbTrav + iTrav0 - 1)
        ReDim Me.FReparties(NbTrav + iTrav0 - 1)

        For i As Integer = iTrav0 To iTrav0 + NbTrav - 1
            Me.Forces(i) = New List(Of cls_Force)
            Me.Moments(i) = New List(Of cls_Moment)
            Me.FReparties(i) = New List(Of cls_ForceRepartie)
        Next

        Me.lRunCalcul = False

        Me.Type = pType
        Me.EtatDalle = pEtatDalle

        Me.lShadow = False
        Me.iShadow = -1

        Me.UZEta = Nothing
    End Sub

#End Region

#Region " Outils "

    Public ReadOnly Property lDispoFlechesEta As Boolean
        '--------------------------------------------------------------------------------------------------------------
        '   03/02/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------
        '   Indique si les flèches ETA sont disponibles
        '--------------------------------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------------------------------
        Get
            Return Not IsNothing(Me.UZEta)
        End Get
    End Property

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

        If Not Me.lShadow Then
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
        End If

        Return lNonNul
    End Function

    Public Sub RecupereFlecheEta(myUz() As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   03/02/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Stocke les flèches dépendant de la connexion, issues du calcul EF
        '-----------------------------------------------------------------------------------------------------------
        '   myUz        [E] :   Flèches de la poutre (prenant en compte la raideurs des connecteurs
        '-----------------------------------------------------------------------------------------------------------

        Me.UZEta = myUz.Clone

    End Sub

    Public Sub RecupereResultats(myVz(,) As Decimal, myMy(,) As Decimal, myUz() As Decimal, myRotY() As Decimal, myRz() As Decimal)
        '-----------------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Stocke les résultats issus du calcul EF
        '-----------------------------------------------------------------------------------------------------------
        '   myVz        [E] :   Efforts tranchants
        '   myMy        [E] :   Moments fléchissants
        '   myUz        [E] :   Flèches de la poutre
        '   myRotY      [E] :   Rotations des noeuds
        '   myRz        [E] :   Réactions aux appuis
        '-----------------------------------------------------------------------------------------------------------

        Me.VZ = myVz.Clone
        Me.MYY = myMy.Clone
        Me.ROTY = myRotY.Clone
        Me.UZ = myUz.Clone
        Me.RZ = myRz.Clone

        Me.lRunCalcul = True

    End Sub

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

    Public Function NombreChargesSurf(iTravP As Integer, iTravD As Integer) As Integer
        '-----------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------
        '   Renvoie le nombre de charges uniformément réparties dans le cas de charge (sur toutes les travées)
        '-----------------------------------------------------------------------------------------------------
        '   iTravP, iTravP [E] :    Indices de la première et de la dernière travée
        '-----------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbCharges As Integer = 0

        '--> Boucle sur les travées

        For iTrav As Integer = iTravP To iTravD

            If (Not IsEqual(Me.QSurf(iTrav), 0)) Then NbCharges += 1

        Next

        '--> Fin

        Return NbCharges

    End Function

    Public Function EffortPmax(iTravP As Integer, iTravD As Integer) As Decimal
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie la valeur maximale des charges ponctuelles
        '-----------------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice de la première travée
        '   iTravD      [E] :   Indice de la dernière travée
        '-----------------------------------------------------------------------------------------------------------

        Dim Force As Decimal = 0

        For iTrav As Integer = iTravP To iTravD

            For i As Integer = 0 To Me.Forces(iTrav).Count - 1

                Force = Math.Max(Force, Math.Abs(Me.Forces(iTrav)(i).Force))

            Next

        Next

        Return Force
    End Function

    Public Function EffortRepMax(iTravP As Integer, iTravD As Integer) As Decimal
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie la valeur maximale des charges réparties 
        '-----------------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice de la première travée
        '   iTravD      [E] :   Indice de la dernière travée
        '-----------------------------------------------------------------------------------------------------------

        Dim Force As Decimal = 0

        For iTrav As Integer = iTravP To iTravD

            For i As Integer = 0 To Me.FReparties(iTrav).Count - 1

                Force = Math.Max(Force, Math.Abs(Me.FReparties(iTrav)(i).Force(0)))
                Force = Math.Max(Force, Math.Abs(Me.FReparties(iTrav)(i).Force(1)))

            Next

        Next

        Return Force
    End Function

#End Region

#Region " Valeurs enveloppes "


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

    Public Sub EnveloppesTranchants(ByRef Vmax As Decimal, ByRef iNodeMax As Integer, ByRef Vmin As Decimal, ByRef iNodeMin As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les efforts tranchants enveloppes issues des résultats du calcul EF
        '-----------------------------------------------------------------------------------------------------------
        '   VMax        [E] :   Valeur max de V
        '   VMin        [E] :   Valeur min de V
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer

        '--> Calcul

        If Me.lRunCalcul Then
            NbNodes = Me.UZ.GetUpperBound(0) + 1
            Mod_Outils.EnveloppeTableauEfforts(Me.VZ, NbNodes, Vmax, Vmin, iNodeMax, iNodeMin)
        End If

    End Sub

    Public Sub EnveloppesMoments(ByRef Mmax As Decimal, ByRef iNodeMax As Integer, ByRef Mmin As Decimal, ByRef iNodeMin As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les moments enveloppes issues des résultats du calcul EF
        '-----------------------------------------------------------------------------------------------------------
        '   MMax        [S] :   Valeur max de la flèche
        '   MMin        [S] :   Valeur min de la flèche
        '   iNodeMax    [S] :   Indice du noeud avec le moment maxi
        '   iNodeMin    [S] :   Indice du noeud avec le moment mini
        '-----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer

        '--> Calcul

        If Me.lRunCalcul Then
            NbNodes = Me.UZ.GetUpperBound(0) + 1
            Mod_Outils.EnveloppeTableauEfforts(Me.MYY, NbNodes, Mmax, Mmin, iNodeMax, iNodeMin)
        End If

    End Sub

    Public Sub EnveloppesMomentsParTravee(myBeam As cls_Poutre, ByRef Menv(,) As Decimal, ByRef iMnode(,) As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   20/12/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les moments enveloppes issues des résultats du calcul EF / par travée
        '-----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   Menv        [S] :   Tableau des moments enveloppes (indice 1 : travée, indice 2 : 0 pour max et 1 pour min)
        '   iMnode      [S] :   Tableau des noeuds où sont atteints les valeurs enveloppes
        '-----------------------------------------------------------------------------------------------------------

        '--> Calcul

        If Me.lRunCalcul Then
            EnveloppeTableau2DparTravee(myBeam, Me.MYY, Menv, iMnode)
        End If

    End Sub

    Public Sub EnveloppesTranchantsParTravee(myBeam As cls_Poutre, ByRef Venv(,) As Decimal, ByRef iVnode(,) As Integer)
        '-----------------------------------------------------------------------------------------------------------
        '   20/12/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Renvoie les moments enveloppes issues des résultats du calcul EF / par travée
        '-----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   Menv        [S] :   Tableau des moments enveloppes (indice 1 : travée, indice 2 : 0 pour max et 1 pour min)
        '   iMnode      [S] :   Tableau des noeuds où sont atteints les valeurs enveloppes
        '-----------------------------------------------------------------------------------------------------------

        '--> Calcul

        If Me.lRunCalcul Then
            EnveloppeTableau2DparTravee(myBeam, Me.VZ, Venv, iVnode)
        End If

    End Sub

#End Region


End Class
