Public Class DATA_MODAL

#Region " Attributs "

    Public Structure Struc_Output
        Dim FreqProp() As Decimal              'frequences propres (0 à nbModes-1)
        Dim VectProp(,) As Decimal             'vecteurs propres (0 à nbModes-1,0 à NbNodes-1)        
        Dim MasseTot As Decimal               'Masse totale 
        Dim MasseMod() As Decimal             'Masse modale (0 à nbModes-1)                        
        Dim MasseGen() As Decimal             'Masse généralisée (0 à nbModes-1)                        
    End Structure

#End Region

End Class
