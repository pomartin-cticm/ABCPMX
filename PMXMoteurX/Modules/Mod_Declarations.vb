Public Module Mod_Declarations

#Region " Paramètres généraux logiciels "

    Public Const NomLogiciel As String = "ABCPMX"

#End Region

#Region " Enumérations,constantes et structures "

    Public Const kConvMPaPa As Decimal = 1000 ^ 2


#Region " Structures "

    Structure strucShearBuckling

        Dim ElancementW As Decimal          ' Elancement de l'âme
        Dim LimiteElancementW As Decimal    ' Limite d'élancement au dela de laquelle il faut vérifier le voilement par cisaillement
        Dim lCheckRequired As Boolean       ' Indique si la vérification de la résistance est requise

    End Structure

#End Region

#End Region

#Region " Tableau 7 EN 1994-1-1 "
    ''' <summary>
    ''' Contrainte maximale dans les aciers autorisée en fonction du diamètre des barres et de l'ouverture des fissures (cf. Tableau 7.1 de l'EC4)
    ''' Colonne 0 = Contrainte autorisée
    ''' Colonne 1 = Diamètre max quand wk,max = 0.4 mm
    ''' Colonne 2 = Diamètre max quand wk,max = 0.3 mm
    ''' Colonne 3 = Diamètre max quand wk,max = 0.2 mm
    ''' </summary>
    Private sigma_S1_Ds As Decimal(,) =
        {{160, 40 / 1000, 32 / 1000, 25 / 1000},
        {200, 32 / 1000, 25 / 1000, 16 / 1000},
        {240, 20 / 1000, 16 / 1000, 12 / 1000},
        {280, 16 / 1000, 12 / 1000, 8 / 1000},
        {320, 12 / 1000, 10 / 1000, 6 / 1000},
        {360, 10 / 1000, 8 / 1000, 5 / 1000},
        {400, 8 / 1000, 6 / 1000, 4 / 1000},
        {450, 6 / 1000, 5 / 1000, 4 / 1000}}

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="wk_max">Largeur d'ouverture maximale en mètre</param>
    ''' <param name="phi_max">Diamètre maximal des armatures en mètre</param>
    ''' <returns></returns>
    Public Function Get_sigma_S1_Ds(wk_max As Decimal, phi_max As Decimal)

        '------------------------------------------------------------------------------------------------------------------
        '   16/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Renvoi la contrainte admissible dans les aciers en fonction du diamètre max des aciers
        '   Recherche le diamètre dans le tableau supérieur à phi_max le plus proche
        '------------------------------------------------------------------------------------------------------------------
        '   wk_maw    [E] :   Largeur des fissures admissibles (0.2; 0.3 ou 0.4 mm)
        '   phi_max   [E] :   Diamètre max des aciers disposés dans la sectoin de béton
        '   sigma_S1  [S] :   Retourne la contrainte admissible dans les aciers
        '------------------------------------------------------------------------------------------------------------------

        Dim sigma_S1 As Decimal

        Dim i As Integer 'indice de ligne
        Dim j As Integer 'indice de colonne

        Select Case wk_max
            Case 0.4 / 1000
                j = 1

            Case 0.3 / 1000
                j = 2

            Case 0.2 / 1000
                j = 3

            Case Else
                Throw New Exception("Valeur wk_max hors limite: wk_max = 0.2, 0.3 ou 0.4 mm (variables d'entrée doit être en mètres)")

        End Select

        If phi_max > sigma_S1_Ds(0, j) Then Throw New Exception("Valeur Phi_max hors limite: Phi_max ne peut pas être supérieur à " & sigma_S1_Ds(0, j) & " (m)")

        For i = 0 To sigma_S1_Ds.Length - 1
            If phi_max > sigma_S1_Ds(i, j) Then
                sigma_S1 = sigma_S1_Ds(i - 1, 0)
                Exit For
            End If
        Next

        If i = sigma_S1_Ds.Length - 1 Then sigma_S1 = sigma_S1_Ds(i, 0)

        Return sigma_S1
    End Function


    ''' <summary>
    ''' Contrainte maximale dans les aciers autorisée en fonction de l'espacement  des barres et de l'ouverture des fissures (cf. Tableau 7.2 de l'EC4)
    ''' Colonne 0 = Contrainte autorisée
    ''' Colonne 1 = Espacement max quand wk,max = 0.4 mm
    ''' Colonne 2 = Espacement max quand wk,max = 0.3 mm
    ''' Colonne 3 = Espacement max quand wk,max = 0.2 mm
    ''' </summary>
    Private sigma_S1_es As Decimal(,) =
        {{160, 300 / 1000, 300 / 1000, 200 / 1000},
        {200, 300 / 1000, 250 / 1000, 150 / 1000},
        {240, 250 / 1000, 200 / 1000, 100 / 1000},
        {280, 200 / 1000, 150 / 1000, 50 / 1000},
        {320, 150 / 1000, 100 / 1000, 0},
        {360, 100 / 1000, 50 / 1000, 0}}

    Public Function Get_sigma_S1_es(wk_max As Decimal, es_max As Decimal)

        '------------------------------------------------------------------------------------------------------------------
        '   16/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Renvoi la contrainte admissible dans les aciers en fonction de l'espacement max des aciers
        '   Recherche le diamètre dans le tableau supérieur à phi_max le plus proche
        '------------------------------------------------------------------------------------------------------------------
        '   wk_maw    [E] :   Largeur des fissures admissibles (0.2; 0.3 ou 0.4 mm)
        '   es_max    [E] :   espacement max des aciers disposés dans la sectoin de béton
        '   sigma_S1  [S] :   Retourne la contrainte admissible dans les aciers
        '------------------------------------------------------------------------------------------------------------------

        Dim sigma_S1 As Decimal

        Dim i As Integer 'indice de ligne
        Dim j As Integer 'indice de colonne

        Select Case wk_max
            Case 0.4 / 1000
                j = 1

            Case 0.3 / 1000
                j = 2

            Case 0.2 / 1000
                j = 3

            Case Else
                Throw New Exception("Valeur wk_max hors limite: wk_max = 0.2, 0.3 ou 0.4 mm (variables d'entrée doit être en mètres)")

        End Select

        If es_max > sigma_S1_es(0, j) Then Throw New Exception("Valeur es_max hors limite: Phi_max ne peut pas être supérieur à " & sigma_S1_es(0, j) & " (m)")

        For i = 0 To sigma_S1_es.Length - 1
            If es_max > sigma_S1_es(i, j) Then
                sigma_S1 = sigma_S1_es(i - 1, 0)
                Exit For
            End If
        Next

        If i = sigma_S1_es.Length - 1 Then sigma_S1 = sigma_S1_es(i, 0)

        Return sigma_S1
    End Function

#End Region

#Region " Gestion noms chargements "

    Public NomChargements() As String           ' Nom des cas de charge utilisateur
    Public NomChargesA() As String              ' Nom des cas de charge analyse

#End Region


    '''' <summary>
    '''' Caractéristiques des goujons (valeurs reprises d'ACB+)
    '''' Colonne 0 = Nom du goujon
    '''' Colonne 1 = Diamètre d (en m)
    '''' Colonne 2 = Hauteur totale hsc (en m)
    '''' Colonne 3 = Limite d'élasticité fy (en MPa)
    '''' Colonne 4 = Résistance ultime fu (en MPa)
    '''' </summary>
    'Public goujons_database As (String, Decimal, Decimal, Decimal, Decimal)() =
    '    {("16-35", 16 / 1000, 35 / 1000, 350, 450),
    '    ("16-50", 16 / 1000, 50 / 1000, 350, 450),
    '    ("16-75", 16 / 1000, 75 / 1000, 350, 450),
    '    ("16-100", 16 / 1000, 100 / 1000, 350, 450),
    '    ("16-150", 16 / 1000, 150 / 1000, 350, 450),
    '    ("16-175", 16 / 1000, 175 / 1000, 350, 450),
    '    ("19-50", 19 / 1000, 50 / 1000, 350, 450),
    '    ("19-60", 19 / 1000, 60 / 1000, 350, 450),
    '    ("19-75", 19 / 1000, 75 / 1000, 350, 450),
    '    ("19-80", 19 / 1000, 80 / 1000, 350, 450),
    '    ("19-100", 19 / 1000, 100 / 1000, 350, 450),
    '    ("19-125", 19 / 1000, 125 / 1000, 350, 450),
    '    ("19-150", 19 / 1000, 150 / 1000, 350, 450),
    '    ("19-175", 19 / 1000, 175 / 1000, 350, 450),
    '    ("22-75", 22 / 1000, 75 / 1000, 350, 450),
    '    ("22-90", 22 / 1000, 90 / 1000, 350, 450),
    '    ("22-100", 22 / 1000, 100 / 1000, 350, 450),
    '    ("22-125", 22 / 1000, 125 / 1000, 350, 450),
    '    ("22-140", 22 / 1000, 140 / 1000, 350, 450),
    '    ("22-150", 22 / 1000, 150 / 1000, 350, 450),
    '    ("22-175", 22 / 1000, 175 / 1000, 350, 450),
    '    ("22-200", 22 / 1000, 200 / 1000, 350, 450),
    '    ("22-250", 22 / 1000, 250 / 1000, 350, 450)}

End Module
