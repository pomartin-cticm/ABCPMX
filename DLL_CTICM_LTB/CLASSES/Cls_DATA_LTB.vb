Public Class DATA_LTB

#Region " Attributs "

    Public Structure Struc_Output
        Dim CoefCr As Decimal                   'Coefficient de charge critique
        Dim MomentCr As Decimal                 'Moment critique de déversement
        Dim V() As Decimal                   'Déplacement latéral v (0 à NbNodes -1) du mode de déversement 1
        Dim VP() As Decimal                  'Rotation autour de l’axe vertical v’ (0 à NbNodes -1) du mode de déversement 1
        Dim Theta() As Decimal               'Rotation de torsion theta (0 à NbNodes -1) du mode de déversement 1
        Dim ThetaP() As Decimal                 'Gauchissement thetap (0 à NbNodes -1) du mode de déversement 1
    End Structure

#End Region

End Class
