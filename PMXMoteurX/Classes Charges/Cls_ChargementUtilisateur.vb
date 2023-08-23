Public Class Cls_ChargementUtilisateur

    '#### CLASSE POUR LA DEFINITION D'UN CHARGEMENT PAR L'UTILISATEUR ######
    '#### CONCERNE : QC, G2, Q1 et Q2  (G1 ?)

#Region " Attributs "
    Public Titre As String                             ' Titre du cas de charge 

    Public QSurf() As Decimal                           ' Charge par unité de surface sur chaque travée
    Public Forces() As List(Of Cls_Force)               ' Liste des efforts ponctuels sur chaque travée
    Public FReparties() As List(Of Cls_ForceRepartie)   ' Liste des charges réparties sur chaque travée

#End Region

#Region " Constructeurs "

    Public Sub New(pTitre As String, IndiceDerniereTravee As Integer)

        Me.Titre = pTitre

        ReDim Me.QSurf(IndiceDerniereTravee)
        ReDim Me.Forces(IndiceDerniereTravee)
        ReDim Me.FReparties(IndiceDerniereTravee)

        For i As Integer = 0 To IndiceDerniereTravee
            Forces(i) = New List(Of Cls_Force)
            FReparties(i) = New List(Of Cls_ForceRepartie)
        Next

    End Sub


#End Region


End Class
