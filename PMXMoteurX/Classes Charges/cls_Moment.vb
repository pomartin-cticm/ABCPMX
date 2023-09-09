Public Class cls_Moment

    '#### CLASSE POUR LA DEFINITION D'UN MOMENT PONCTUEL ######


#Region " Attributs "

    Public Moment As Decimal      ' Valeur de la force (>0 => gravitaire)
    Public xPosT As Decimal      ' Position par rapport à l'appui gauche de la travée
    Public xPosG As Decimal      ' Position par rapport à l'extrémité gauche de la poutre

#End Region

#Region " Constructeurs "

    Public Sub New(pxPosT As Decimal, pMoment As Decimal)
        xPosT = pxPosT
        Moment = pMoment
    End Sub

#End Region

#Region " Fonctions de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region


End Class
