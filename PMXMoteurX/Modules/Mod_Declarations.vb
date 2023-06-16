Module Mod_Declarations

#Region " Constantes "
    ''' <summary>
    ''' Contrainte maximale dans les aciers autorisée en fonction du diamètre des barres et de l'ouverture des fissures (cf. Tableau 7.1 de l'EC4)
    ''' Colonne 0 = Contrainte autorisée
    ''' Colonne 1 = Diamètre max quand wk,max = 0.4 mm
    ''' Colonne 2 = Diamètre max quand wk,max = 0.3 mm
    ''' Colonne 3 = Diamètre max quand wk,max = 0.2 mm
    ''' </summary>
    Public sigma_S1_Ds As Decimal(,) =
        {{160, 40 / 1000, 32 / 1000, 25 / 1000},
        {200, 32 / 1000, 25 / 1000, 16 / 1000},
        {240, 20 / 1000, 16 / 1000, 12 / 1000},
        {280, 16 / 1000, 12 / 1000, 8 / 1000},
        {320, 12 / 1000, 10 / 1000, 6 / 1000},
        {360, 10 / 1000, 8 / 1000, 5 / 1000},
        {400, 8 / 1000, 6 / 1000, 4 / 1000},
        {450, 6 / 1000, 5 / 1000, 0}}

    ''' <summary>
    ''' Contrainte maximale dans les aciers autorisée en fonction de l'espacement  des barres et de l'ouverture des fissures (cf. Tableau 7.2 de l'EC4)
    ''' Colonne 0 = Contrainte autorisée
    ''' Colonne 1 = Espacement max quand wk,max = 0.4 mm
    ''' Colonne 2 = Espacement max quand wk,max = 0.3 mm
    ''' Colonne 3 = Espacement max quand wk,max = 0.2 mm
    ''' </summary>
    Public sigma_S1_es As Decimal(,) =
        {{160, 300 / 1000, 300 / 1000, 200 / 1000},
        {200, 300 / 1000, 250 / 1000, 150 / 1000},
        {240, 250 / 1000, 200 / 1000, 100 / 1000},
        {280, 200 / 1000, 150 / 1000, 50 / 1000},
        {320, 150 / 1000, 100 / 1000, 0},
        {360, 100 / 1000, 50 / 1000, 0}}

#End Region




End Module
