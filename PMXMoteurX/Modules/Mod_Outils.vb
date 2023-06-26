Module Mod_Outils

#Region " Outils Divers "
    Public Sub AjoutePoint(x As Single, y As Single, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '---------------------------------------------------------------------------------------------------------------------------
        '   31/05/23    :   Création - POM
        '---------------------------------------------------------------------------------------------------------------------------


        nbPts += 1
        If nbPts = 1 Then
            ReDim xPts(0)
            ReDim yPts(0)
        Else
            ReDim Preserve xPts(nbPts - 1)
            ReDim Preserve yPts(nbPts - 1)
        End If
        xPts(nbPts - 1) = x
        yPts(nbPts - 1) = y

    End Sub

#End Region




End Module
