Public Class cls_ChargementUtilisateur

    '#### CLASSE POUR LA DEFINITION D'UN CHARGEMENT PAR L'UTILISATEUR ######
    '#### CONCERNE : QC, G2, Q1 et Q2  (G1 ?)

#Region " Attributs "
    Public Titre As String                             ' Titre du cas de charge 

    Public QSurf() As Decimal                           ' Charge par unité de surface sur chaque travée
    'Public WSurf() As Decimal                           ' Largeur d'application de la charge surfacique sur chaque travée
    Public Forces() As List(Of cls_Force)               ' Liste des efforts ponctuels sur chaque travée
    Public FReparties() As List(Of cls_ForceRepartie)   ' Liste des charges réparties sur chaque travée

#End Region

#Region " Constructeurs "

    Public Sub New(pTitre As String, IndiceDerniereTravee As Integer)

        Me.Titre = pTitre

        ReDim Me.QSurf(IndiceDerniereTravee + 1)
        'ReDim Me.WSurf(IndiceDerniereTravee + 1)
        ReDim Me.Forces(IndiceDerniereTravee + 1)
        ReDim Me.FReparties(IndiceDerniereTravee + 1)

        For i As Integer = 0 To IndiceDerniereTravee + 1
            Forces(i) = New List(Of cls_Force)
            FReparties(i) = New List(Of cls_ForceRepartie)
        Next


        '== Je ne comprends pas le plus 1
    End Sub

#End Region

#Region " Outils, functions et propriétés "

    Public Function EstMultiTravee(iTravD As Integer, iTravF As Integer) As Boolean
        '--------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Indique si le chargement est appliqué sur plusieurs travées
        '--------------------------------------------------------------------------------------------
        '   iTravD      [E] :   Indice de la première travée
        '   iTravE      [E] :   Indice de la dernière travée
        '--------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lMulti As Boolean
        Dim lChargeT() As Boolean
        Dim iT0, iT1 As Integer
        Dim Compteur As Integer = 0

        '-->

        If iTravD = iTravF Then
            lMulti = False
        Else
            iT0 = Math.Min(iTravD, iTravF)
            iT1 = Math.Max(iTravD, iTravF)
            ReDim lChargeT(iT1)

            For iTrav As Integer = iT0 To iT1
                lChargeT(iTrav) = EstTraveeChargee(iTrav)
                If lChargeT(iTrav) Then Compteur += 1
            Next
            lMulti = (Compteur > 1) And (lChargeT(1))
        End If

        Return lMulti
    End Function

    Private Function EstTraveeChargee(iTrav As Integer) As Boolean
        '--------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Indique si un chargement est appliquée sur une travée
        '--------------------------------------------------------------------------------------------
        '   iTrav       [E] :   Indice de la travée
        '--------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lCharge As Boolean = False
        Dim i As Integer

        '--> On cherche si la travée est chargée

        If Not IsEqual(Math.Abs(Me.QSurf(iTrav)), 0) Then lCharge = True

        For i = 0 To Me.Forces(iTrav).Count - 1
            If Not IsEqual(Math.Abs(Me.Forces(iTrav)(i).Force), 0) Then lCharge = True
        Next

        For i = 0 To Me.FReparties(iTrav).Count - 1
            If Not IsEqual(Math.Abs(Me.FReparties(iTrav)(i).Force(0)), 0) Then lCharge = True
            If Not IsEqual(Math.Abs(Me.FReparties(iTrav)(i).Force(1)), 0) Then lCharge = True
        Next
        Return lCharge
    End Function

    ''' <summary>
    ''' Indique si au moins une charge est définie
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EstDefinie As Boolean

        Get
            Dim lDefini As Boolean = False
            Dim iForc As Integer
            Dim NbTravees As Integer = Me.QSurf.GetUpperBound(0)

            For iTrav As Integer = 0 To NbTravees

                If Not IsEqual(Math.Abs(Me.QSurf(iTrav)), 0) Then lDefini = True

                For iForc = 0 To Me.Forces(iTrav).Count - 1
                    If Not IsEqual(Forces(iTrav)(iForc).Force, 0) Then lDefini = True
                Next

                For iForc = 0 To Me.FReparties(iTrav).Count - 1
                    If (Not IsEqual(FReparties(iTrav)(iForc).Force(0), 0)) _
                    Or (Not IsEqual(FReparties(iTrav)(iForc).Force(1), 0)) Then lDefini = True
                Next

            Next

            Return lDefini
        End Get
    End Property

    Public Function NombreForceReparties(iTravP As Integer, iTravD As Integer) As Integer
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
            NbCharges += Me.FReparties(iTrav).Count
        Next

        '--> Fin

        Return NbCharges

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

#End Region

#Region " Fonction de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(ByVal ChargementSource As cls_ChargementUtilisateur, ByRef ChargementCible As cls_ChargementUtilisateur)

        ChargementCible = ChargementSource.Clone()

        ReDim ChargementCible.QSurf(ChargementSource.QSurf.Length - 1)
        ChargementCible.QSurf = ChargementSource.QSurf.Clone()

        ReDim ChargementCible.Forces(ChargementSource.Forces.Length - 1)
        For i As Integer = 0 To ChargementSource.Forces.Length - 1
            ChargementCible.Forces(i) = New List(Of cls_Force)
            For Each force As cls_Force In ChargementSource.Forces(i)
                ChargementCible.Forces(i).Add(force.Clone())
            Next
        Next

        ReDim ChargementCible.FReparties(ChargementSource.FReparties.Length - 1)
        For i As Integer = 0 To ChargementSource.FReparties.Length - 1
            ChargementCible.FReparties(i) = New List(Of cls_ForceRepartie)
            For Each force As cls_ForceRepartie In ChargementSource.FReparties(i)
                Dim force_loc As New cls_ForceRepartie(0, 0, 0, 0, 0)
                force_loc.DeepClone(force, force_loc)
                ChargementCible.FReparties(i).Add(force_loc)
            Next
        Next
    End Sub

#End Region


End Class
