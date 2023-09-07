Public Class Cls_CasDeCharge

    '#### CLASSE POUR LA CALCUL D'UN CHARGEMENT PAR LE LOGICIEL ######

#Region " Attributs "

    '--> Identifications

    Public Nom As String                                ' Dénomination dans la langue utilisateur du cas de charge
    Public Symbol As String                             ' Symbole

    '--> Paramètres de modélisation

    Public IndElts As Integer                           ' Indice de la table BeamElements contenant les propriétés des barres

    '--> Charges

    Public QSurf() As Decimal                           ' Charge par unité de surface sur chaque travée
    Public Forces() As List(Of Cls_Force)               ' Liste des efforts ponctuels sur chaque travée
    Public FReparties() As List(Of Cls_ForceRepartie)   ' Liste des charges réparties sur chaque travée

    '--> Résultats de l'analyse

    Public VZ(,) As Decimal                             'Effort tranchant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)
    Public MYY(,) As Decimal                            'Moment fléchissant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)

    Public UZ() As Decimal                              'Déplacement vertical du nœud i (0 à NbNodes-1)
    Public ROTY() As Decimal                            'Rotation du nœud i (0 à NbNodes-1)

    Public RZ() As Decimal                              'Réactions verticales aux nœuds support (0 à NbAppuis-1)

#End Region

#Region " Constructeurs "

    Public Sub New(pNom As String, pSymbol As String, IndiceElts As Integer)

        Me.Nom = pNom
        Me.Symbol = pSymbol

        Me.IndElts = IndiceElts
    End Sub

#End Region

End Class
