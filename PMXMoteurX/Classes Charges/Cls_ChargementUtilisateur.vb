Public Class cls_ChargementUtilisateur

    '#### CLASSE POUR LA DEFINITION D'UN CHARGEMENT PAR L'UTILISATEUR ######
    '#### CONCERNE : QC, G2, Q1 et Q2  (G1 ?)

#Region " Attributs "
    Public Titre As String                             ' Titre du cas de charge 

    Public QSurf() As Decimal                           ' Charge par unité de surface sur chaque travée
    Public WSurf() As Decimal                           ' Largeur d'application de la charge surfacique sur chaque travée
    Public Forces() As List(Of cls_Force)               ' Liste des efforts ponctuels sur chaque travée
    Public FReparties() As List(Of cls_ForceRepartie)   ' Liste des charges réparties sur chaque travée

#End Region

#Region " Constructeurs "

    Public Sub New(pTitre As String, IndiceDerniereTravee As Integer)

        Me.Titre = pTitre

        ReDim Me.QSurf(IndiceDerniereTravee)
        ReDim Me.WSurf(IndiceDerniereTravee)
        ReDim Me.Forces(IndiceDerniereTravee)
        ReDim Me.FReparties(IndiceDerniereTravee)

        For i As Integer = 0 To IndiceDerniereTravee
            Forces(i) = New List(Of cls_Force)
            FReparties(i) = New List(Of cls_ForceRepartie)
        Next

    End Sub


#End Region

#Region " Fonction de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(ByVal ChargementSource As cls_ChargementUtilisateur, ByRef ChargementCible As cls_ChargementUtilisateur)

        ReDim ChargementCible.QSurf(ChargementSource.QSurf.Length - 1)
        ChargementCible.QSurf = ChargementSource.QSurf.Clone()

        ReDim ChargementCible.Forces(ChargementSource.Forces.Length - 1)
        For i As Integer = 0 To ChargementSource.Forces.Length - 1
            For Each force As cls_Force In ChargementSource.Forces(i)
                ChargementCible.Forces(i).Add(force.Clone())
            Next
        Next

        ReDim ChargementCible.FReparties(ChargementSource.FReparties.Length - 1)
        For i As Integer = 0 To ChargementSource.FReparties.Length - 1
            For Each force As cls_ForceRepartie In ChargementSource.FReparties(i)
                ChargementCible.FReparties(i).Add(force.Clone())
            Next
        Next
    End Sub

#End Region


End Class
