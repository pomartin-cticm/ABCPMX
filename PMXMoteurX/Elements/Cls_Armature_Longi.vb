Public Class Cls_Armatures_Longi

#Region " Attributs "

    ''' <summary>
    ''' espacement entre barres
    ''' </summary>
    Public EspBar As Decimal

    ''' <summary>
    ''' diamètre d'une barre
    ''' </summary>
    Public PhiS As Decimal

    ''' <summary>
    ''' position du lit d'aramature, par rapport à la fibre supérieure de la dalle
    ''' </summary>
    Public z_s As Decimal

    ''' <summary>
    ''' nombre de barres dans un lit d’armatures 
    ''' </summary>
    Public n_s As Integer

    ''' <summary>
    ''' Espace entre la barre et le bord de l'élément - Valeur pour l'interface
    ''' </summary>
    Public c_s As Decimal

    ''' <summary>
    ''' Indique si le lit d'armatures est activé
    ''' </summary>
    Public lActive As Boolean

#End Region

#Region " Propiétés "

    ''' <summary>
    ''' Aire (m2)
    ''' </summary>
    'Public A_s As Decimal

    ''' <summary>
    ''' Aire d'armature par unité de largeur de dalle
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AireParULargeur As Decimal
        Get
            Return Math.PI * Me.PhiS ^ 2 / (4 * Me.EspBar)
        End Get
    End Property

#End Region

#Region " Constructeur "

    Sub New()

        'Me.n_s = 0
        Me.PhiS = 0.012
        Me.z_s = 0.032
        Me.c_s = 0.015
        Me.EspBar = 0.2
        Me.lActive = True

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
