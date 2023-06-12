Public Class Cls_ArmatureEnrobage

#Region " Constantes "

    Const PhiDEF As Decimal = 0.008

#End Region

#Region " Attributs "

    ''' <summary>
    ''' Diamètre des barres placées à l'extérieur
    ''' </summary>
    Public PhiExt As Decimal

    ''' <summary>
    ''' Nombre de barres placées à l'extérieur / chambre
    ''' </summary>
    Public NbExt As Decimal

    ''' <summary>
    ''' Diamètre des barres placées au centre
    ''' </summary>
    Public PhiMil As Decimal

    ''' <summary>
    ''' Nombre de barres placées au centre / chambre
    ''' </summary>
    Public NbMil As Decimal

    ''' <summary>
    ''' Diamètre des barres placées à l'extérieur
    ''' </summary>
    Public PhiInt As Decimal

    ''' <summary>
    ''' Nombre de barres placées à l'extérieur / chambre
    ''' </summary>
    Public NbInt As Decimal

    ''' <summary>
    ''' Position z du lit (utilisé pour le lit central uniquement)
    ''' </summary>
    Public zPosRatio As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New()
        PhiExt = PhiDEF
        NbExt = 1

        PhiMil = PhiDEF
        NbMil = 0

        PhiInt = PhiDEF
        NbInt = 1

        Me.zPosRatio = 0.5
    End Sub

#End Region

#Region " Fonctions "

    Public ReadOnly Property Aire
        Get
            Dim pAire As Decimal

            pAire = NbExt * Math.PI * PhiExt ^ 2 / 4
            pAire += NbMil * Math.PI * PhiMil ^ 2 / 4
            pAire += NbInt * Math.PI * PhiInt ^ 2 / 4

            Return pAire
        End Get
    End Property

#End Region

End Class
