Public Class Cls_ForceRepartie

    '#### CLASSE POUR LA DEFINITION D'UNE CHARGE PONCTUELLE ######

#Region " Attributs "

    Public Force(1) As Decimal      ' Valeur de la force (>0 => gravitaire)
    Public xPosT(1) As Decimal      ' Position par rapport à l'appui gauche de la travée
    Public xPosG(1) As Decimal      ' Position par rapport à l'extrémité gauche de la poutre

#End Region

#Region " Constructeurs "

    Public Sub New(pxPosTG As Decimal, pForceG As Decimal, pxPosTD As Decimal, pForceD As Decimal)
        xPosT(0) = pxPosTG
        Force(0) = pForceG
        xPosT(1) = pxPosTD
        Force(1) = pForceD
    End Sub

#End Region


End Class
