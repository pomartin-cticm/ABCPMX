Public Class Cls_AcierArmature

#Region " Constantes "

    Public Shared tabClasseAcierArma() As String = {"B450", "B500"}
    Private TabFsk() As Decimal = {450, 500}
    Public Shared tabYoungArma() As Decimal = {200000, 205000, 210000}

#End Region

#Region " Attributs "

    ''' <summary>
    ''' Classe des aciers d'armatures
    ''' </summary>
    Public Classe As String

    ''' <summary>
    ''' Limite d'élasticité caractéristique
    ''' </summary>
    Public FsK As Decimal

    ''' <summary>
    ''' Module d'élasticité
    ''' </summary>
    Public Es As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.Classe = "B500"
        Me.Es = Cls_Acier.EY
        MAJProprietes()
    End Sub

#End Region

#Region " Outils "

    Public Sub MAJProprietes()

        Me.FsK = Me.TabFsk(Array.IndexOf(TabClasseAcierArma, Me.Classe))

    End Sub

#End Region

End Class
