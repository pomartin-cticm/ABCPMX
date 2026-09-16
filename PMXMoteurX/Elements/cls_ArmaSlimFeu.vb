Public Class cls_ArmaSlimFeu

#Region " Attributs "

    ''' <summary>
    ''' Indique si présence d'armature longitudinale pour le calcul au feu des slims floors
    ''' </summary>
    Public lBarre As Boolean

    ''' <summary>
    ''' Nombre de barres de part et d'autre de l'âme
    ''' </summary>
    Public NbBarres As Integer

    ''' <summary>
    ''' Diametre des barres
    ''' </summary>
    Public Diametre As Decimal

    ''' <summary>
    ''' Position x et z
    ''' </summary>
    Public xPos, zPos As Decimal

#End Region

#Region " Constructeur "

    Public Sub New()

        lBarre = False
        Diametre = 0.012
        NbBarres = 1

        xPos = 0.1
        zPos = 0.1

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function CopieFrom(ByVal p_Arma As cls_ArmaSlimFeu) As cls_ArmaSlimFeu
        Me.lBarre = p_Arma.lBarre
        Me.NbBarres = p_Arma.NbBarres
        Me.Diametre = p_Arma.Diametre
        Me.xPos = p_Arma.xPos
        Me.zPos = p_Arma.zPos
        Return Me
    End Function

#End Region


End Class
