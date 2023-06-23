Public Class cls_Maintiens



#Region "Variables"

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




    Sub New(x_Loc As Decimal, lMaintienSemelleSup As Boolean, lMaintienSemelleInf As Boolean, lMaintienSelectionne As Boolean)
        Me.x_Loc = x_Loc
        Me.lMaintienSemelleSup = lMaintienSemelleSup
        Me.lMaintienSemelleInf = lMaintienSemelleInf
        Me.lMaintienSelectionne = lMaintienSelectionne
    End Sub


    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
