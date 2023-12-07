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

    Public Structure struc_DonneesLTB
        Dim NbMaintiensPon As Integer               'Nombre de maintiens ponctuels
        Dim iNodeMaintienPon() As Integer           'Numero du noeud où se trouve le maintien ponctuel j (0 à NbMaintienPon-1)
        Dim MaintienPonV() As Decimal               'Maintien ponctuel en déplacement latéral v (0 à NbMaintienPon-1)   : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
        Dim MaintienPonVP() As Decimal              'Maintien ponctuel en rotation latéral v' (0 à NbMaintienPon-1)     : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
        Dim MaintienPonTheta() As Decimal           'Maintien ponctuel en torsion theta (0 à NbMaintienPon-1)           : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
        Dim MaintienPonThetaP() As Decimal          'Maintien ponctuel en gauchissement theta' (0 à NbMaintienPon-1)    : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
        Dim zMaintienPonC() As Decimal              'Position verticale du maintien ponctuel j (0 à NbMaintienPon-1)
    End Structure

#End Region

End Class
