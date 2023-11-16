Public Class cls_ForceRepartie

    '#### CLASSE POUR LA DEFINITION D'UNE CHARGE PONCTUELLE ######

#Region " Attributs "

    Public Force(1) As Decimal      ' Valeur de la force (>0 => gravitaire)
    Public xPosT(1) As Decimal      ' Position par rapport à l'appui gauche de la travée
    Public xGaucheT As Decimal      ' Position de l'appui gauche à l'extrémité gauche de la poutre

    'Public xPosG(1) As Decimal      ' Position par rapport à l'extrémité gauche de la poutre

#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub
    Public Sub New(pxPosTG As Decimal, pForceG As Decimal, pxPosTD As Decimal, pForceD As Decimal, pxGaucheT As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------
        '   
        '-----------------------------------------------------------------------------------------------------------------------        
        '-----------------------------------------------------------------------------------------------------------------------
        '   pxPosTG     [E] :   Position gauche dans la travée du chargement
        '   pxPosTD     [E] :   Position droite dans la travée du chargement
        '   pForceG     [E] :   Force à gauche
        '   pForceD     [E] :   Force à droite
        '   pxGaucheT   [E] :   Position / repere general de l'appui gauche de la travée
        '-----------------------------------------------------------------------------------------------------------------------
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
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Function DeepClone(ByVal ForceRepartieSource As cls_ForceRepartie, ByRef ForceRepartieCible As cls_ForceRepartie)
        ForceRepartieCible.Force(0) = ForceRepartieSource.Force(0)
        ForceRepartieCible.Force(1) = ForceRepartieSource.Force(1)

        ForceRepartieCible.xPosT(0) = ForceRepartieSource.xPosT(0)
        ForceRepartieCible.xPosT(1) = ForceRepartieSource.xPosT(1)

        ForceRepartieCible.xGaucheT = ForceRepartieSource.xGaucheT

    End Function

#End Region

#Region " Propriétés "
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
