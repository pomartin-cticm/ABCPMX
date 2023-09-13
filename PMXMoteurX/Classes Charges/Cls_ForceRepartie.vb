Public Class cls_ForceRepartie

    '#### CLASSE POUR LA DEFINITION D'UNE CHARGE PONCTUELLE ######

#Region " Attributs "

    Public Force(1) As Decimal      ' Valeur de la force (>0 => gravitaire)
    Public xPosT(1) As Decimal      ' Position par rapport à l'appui gauche de la travée
    Public xGaucheT As Decimal      ' Position de l'appui gauche à l'extrémité gauche de la poutre

    'Public xPosG(1) As Decimal      ' Position par rapport à l'extrémité gauche de la poutre

#End Region

#Region " Constructeurs "

    Public Sub New(pxPosTG As Decimal, pForceG As Decimal, pxPosTD As Decimal, pForceD As Decimal, pxGaucheT As Decimal)

        xGaucheT = pxGaucheT

        xPosT(0) = pxPosTG
        'xPosG(0) = xGaucheT + pxPosTG
        Force(0) = pForceG

        xPosT(1) = pxPosTD
        'xPosG(1) = xGaucheT + pxPosTD
        Force(1) = pForceD

    End Sub

#End Region

#Region " Fonction de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

#Region "Propriétés"
    ''' <summary>
    ''' /!\ NE PAS DEPASSER L'INDICE DU TABLEAU RENVOYE /!\
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property xPosG As Decimal() ' Position par rapport à l'extrémité gauche de la poutre 
        Get
            Dim loc_array(1) As Decimal
            loc_array(0) = xGaucheT + xPosT(0)
            loc_array(1) = xGaucheT + xPosT(1)
            Return loc_array
        End Get
    End Property

#End Region
End Class
