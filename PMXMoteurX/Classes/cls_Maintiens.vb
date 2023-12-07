Public Class cls_Maintiens

#Region " Enumération "

#End Region

#Region " Variables "

    ''' <summary>
    ''' Position du maintiens
    ''' </summary>
    Public x_Loc As Decimal

    ''' <summary>
    ''' Etat du maintien de la semelle supérieure
    ''' </summary>
    Public lMaintienSemelleSup As Boolean

    ''' <summary>
    ''' Etat du maintien de la semelle inférieure
    ''' </summary>
    Public lMaintienSemelleInf As Boolean

    ''' <summary>
    ''' Indique si le maintien est selectionné ou non (utile pour le dessin)
    ''' </summary>
    Public lMaintienSelectionne As Boolean

#End Region

#Region " Constructeurs "
    Sub New()
        Me.New(0, False, False, False)
    End Sub


    Sub New(x_Loc As Decimal, lMaintienSemelleSup As Boolean, lMaintienSemelleInf As Boolean, lMaintienSelectionne As Boolean)
        Me.x_Loc = x_Loc
        Me.lMaintienSemelleSup = lMaintienSemelleSup
        Me.lMaintienSemelleInf = lMaintienSemelleInf
        Me.lMaintienSelectionne = lMaintienSelectionne
    End Sub

#End Region

#Region " Outils "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim maintiens = TryCast(obj, cls_Maintiens)
        Return maintiens IsNot Nothing AndAlso
               x_Loc = maintiens.x_Loc AndAlso
               lMaintienSemelleSup = maintiens.lMaintienSemelleSup AndAlso
               lMaintienSemelleInf = maintiens.lMaintienSemelleInf AndAlso
               lMaintienSelectionne = maintiens.lMaintienSelectionne
    End Function

    Public ReadOnly Property EstEfficace As Boolean
        Get
            Return Me.lMaintienSemelleInf Or Me.lMaintienSemelleSup
        End Get
    End Property



#End Region

End Class
