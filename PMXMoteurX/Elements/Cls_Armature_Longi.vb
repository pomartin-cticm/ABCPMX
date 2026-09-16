Public Class Cls_Armatures_Longi

#Region " Déclarations "

    Public Shared TabDiametres() As Decimal = {0.006, 0.008, 0.01, 0.012, 0.014, 0.016, 0.02, 0.025, 0.032}

#End Region

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
    ''' DISTANCE du lit d'aramature, par rapport à la fibre supérieure de la dalle
    ''' /!\ Valeur >0 /!\
    ''' </summary>
    Public z_s As Decimal

    ''' <summary>
    ''' nombre de barres dans un lit d’armatures 
    ''' </summary>
    Public n_s As Integer

    ''' <summary>
    ''' Indique si le lit d'armatures est activé
    ''' </summary>
    Public lActive As Boolean

#End Region

#Region " Propriétés "

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

    Public ReadOnly Property c_min As Decimal
        Get
            If Me.lActive Then
                Return Math.Min(10 / 1000, Me.PhiS)
            Else
                Return 10 / 1000
            End If
        End Get
    End Property

#Region " Constructeur "

    Sub New()

        'Me.n_s = 0
        Me.PhiS = 0.012
        Me.z_s = 0.032
        'Me.c_s = 0.015
        Me.EspBar = 0.2
        Me.lActive = True

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub CopieFrom(ByVal source As Cls_Armatures_Longi)
        Me.EspBar = source.EspBar
        Me.PhiS = source.PhiS
        Me.z_s = source.z_s
        Me.n_s = source.n_s
        Me.lActive = source.lActive
    End Sub

#End Region

End Class
