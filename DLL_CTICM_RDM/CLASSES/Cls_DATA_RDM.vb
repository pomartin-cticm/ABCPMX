Public Class DATA_RDM

#Region " Attributs "

    Public Structure Struc_Output
        Dim VZ(,) As Decimal                    ' Effort tranchant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)
        Dim MYY(,) As Decimal                   ' Moment fléchissant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)

        Dim UZ() As Decimal                     ' Déplacement vertical du nœud i (0 à NbNodes-1)
        Dim ROTY() As Decimal                   ' Rotation du nœud i (0 à NbNodes-1)

        Dim RZ() As Decimal                     ' Réactions verticales aux nœuds support (0 à NbAppuis-1)
    End Structure

#End Region

End Class
