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

    Public Function Get_Phi_Max() As Decimal
        Dim phi_max As Decimal
        If NbExt <> 0 And PhiExt > phi_max Then phi_max = PhiExt
        If NbMil <> 0 And PhiMil > phi_max Then phi_max = PhiMil
        If NbInt <> 0 And PhiInt > phi_max Then phi_max = PhiInt

        Return phi_max

    End Function

    Public Function NbBarres(iPos As Integer) As Integer
        '--------------------------------------------------------------------------------
        '   12/07/23 :  Création - POM
        '--------------------------------------------------------------------------------
        ' Retourne le nombre de barres dans une grappe
        '--------------------------------------------------------------------------------
        '   iPos    [E] :   0 pour la grappe exterieure
        '                   1 pour la grappe intermédiaire
        '                   2 pour la grappe interieure
        '--------------------------------------------------------------------------------

        Dim NbB As Integer

        Select Case iPos
            Case 0 : NbB = Me.NbExt
            Case 1 : NbB = Me.NbMil
            Case 2 : NbB = Me.NbInt
        End Select

        Return NbB
    End Function


    Public Function PhiBarre(iPos As Integer) As Decimal
        '--------------------------------------------------------------------------------
        '   12/07/23 :  Création - POM
        '--------------------------------------------------------------------------------
        ' Retourne le diamètre de barres dans une grappe
        '--------------------------------------------------------------------------------
        '   iPos    [E] :   0 pour la grappe exterieure
        '                   1 pour la grappe intermédiaire
        '                   2 pour la grappe interieure
        '--------------------------------------------------------------------------------

        Dim Phi As Decimal

        Select Case iPos
            Case 0 : Phi = Me.PhiExt
            Case 1 : Phi = Me.PhiMil
            Case 2 : Phi = Me.PhiInt
        End Select

        Return Phi
    End Function

#End Region

#Region "Fonctions de copie"
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function
#End Region

End Class
