Public Class cls_Maintiens

#Region "Enumération"
    Enum EnuPositionMaintienSection
        SemelleSup
        SemelleInf
        DeuxSemelles
    End Enum

#End Region

#Region "Variables"

    ''' <summary>
    ''' Position du maintiens
    ''' </summary>
    Public x_Loc As Decimal

    ''' <summary>
    ''' Position du maintiens
    ''' </summary>
    Public PositionMaintien As EnuPositionMaintienSection


    Sub New(x_Loc As Decimal, PositionMaintien As EnuPositionMaintienSection)
        Me.x_Loc = x_Loc
        Me.PositionMaintien = PositionMaintien
    End Sub

#End Region

End Class
