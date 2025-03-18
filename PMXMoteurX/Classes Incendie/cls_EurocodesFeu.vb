Public Class cls_EurocodesFeu

#Region " Déclarations "

    Const kUnitMM As Decimal = 1000

#End Region

#Region " Coefficients de réduction des propriétés mécaniques en fonction de la température "

    Private TableReductionFacteurAcier(,) As Decimal = '{temperature ; reduc fy ; reduc E ; reduc fu}
        {
        {20, 1.0, 1.0, 1.25},
        {100, 1.0, 1.0, 1.25},
        {200, 1.0, 0.9, 1.25},
        {300, 1.0, 0.8, 1.25},
        {400, 1.0, 0.7, 1.0},
        {500, 0.78, 0.6, 0.78},
        {600, 0.47, 0.31, 0.47},
        {700, 0.23, 0.13, 0.23},
        {800, 0.11, 0.09, 0.11},
        {900, 0.06, 0.0675, 0.06},
        {1000, 0.04, 0.045, 0.04},
        {1100, 0.02, 0.0225, 0.02},
        {1200, 0.00, 0.000, 0.00}
        }

    Private TableReductionFacteurBeton(,) As Decimal = '{temperature ; reduc fck pour béton normal ; reduc fck pour béton léger}
        {
        {20, 1.0, 1.0},
        {100, 1.0, 1.0},
        {200, 0.95, 1.0},
        {300, 0.85, 1.0},
        {400, 0.75, 0.88},
        {500, 0.6, 0.76},
        {600, 0.45, 0.64},
        {700, 0.3, 0.52},
        {800, 0.15, 0.4},
        {900, 0.08, 0.28},
        {1000, 0.04, 0.16},
        {1100, 0.01, 0.04},
        {1200, 0, 0}
        }

    Private TableReductionFacteurArmatures(,) As Decimal = '{temperature ; reduc fsk pour acier formé à froid ; reduc fsk pour acier laminé à chaud}
        {
        {20, 1.0, 1.0},
        {100, 1.0, 1.0},
        {200, 1.0, 1.0},
        {300, 1.0, 1.0},
        {400, 0.94, 1.0},
        {500, 0.67, 0.78},
        {600, 0.4, 0.47},
        {700, 0.12, 0.23},
        {800, 0.11, 0.11},
        {900, 0.08, 0.06},
        {1000, 0.05, 0.04},
        {1100, 0.03, 0.02},
        {1200, 0, 0}
        }

    Private Function Recherche_TableReductionFacteur(ByVal TableReductionFacteur As Decimal(,), ByVal TempA As Decimal, indColonne As Integer) As Decimal
        Dim myReduc As Decimal = 0
        Dim indLigne As Integer = 0
        Dim lTrouve As Boolean = False

        TempA = Math.Max(TempA, TableReductionFacteur(0, 0))
        TempA = Math.Min(TempA, TableReductionFacteur(TableReductionFacteur.GetUpperBound(0), 0))

        Dim test As Integer = TableReductionFacteur.GetUpperBound(0)
        Dim testbus As Decimal = TableReductionFacteur(TableReductionFacteur.GetUpperBound(0), 0)

        '--( Traitement

        Do
            lTrouve = TempA <= TableReductionFacteur(indLigne, 0)
            If Not lTrouve Then indLigne += 1
        Loop While Not lTrouve And indLigne <= TableReductionFacteur.GetUpperBound(0)

        If indLigne = 0 Then
            myReduc = TableReductionFacteur(indLigne, indColonne)
        Else
            myReduc = TableReductionFacteur(indLigne - 1, indColonne) + (TempA - TableReductionFacteur(indLigne - 1, 0)) / (TableReductionFacteur(indLigne, 0) - TableReductionFacteur(indLigne - 1, 0)) * (TableReductionFacteur(indLigne, indColonne) - TableReductionFacteur(indLigne - 1, indColonne))
        End If

        Return myReduc
    End Function

    Public Function ReducFyAcier(ByVal TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction de la limite d'élasticité de l'acier en fonction de la température ky,theta
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim indColonneReducFy As Integer = 1

        Return Recherche_TableReductionFacteur(TableReductionFacteurAcier, TempA, indColonneReducFy)

    End Function


    Public Function ReducEyAcier(TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction du module d'Young de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim indColonneReducEy As Integer = 2

        Return Recherche_TableReductionFacteur(TableReductionFacteurAcier, TempA, indColonneReducEy)

    End Function

    Public Function ReducFuAcier(TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction de la résistance ultime à la traction de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim indColonneReducFu As Integer = 3

        Return Recherche_TableReductionFacteur(TableReductionFacteurAcier, TempA, indColonneReducFu)

    End Function

    Public Function ReducFckBeton(TempA As Decimal, lBetonLeger As Boolean) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction de la résistance ultime à la traction de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '   lBetonLeger [E] :   Indique si on est en présence d'un béton léger (True) ou non (False)
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim indColonneReducFck As Integer

        If Not lBetonLeger Then
            indColonneReducFck = 1
        Else
            indColonneReducFck = 2
        End If

        Return Recherche_TableReductionFacteur(TableReductionFacteurBeton, TempA, indColonneReducFck)

    End Function

    Public Function ReducFskArmatures(TempA As Decimal, lArmatureFormeeAFroid As Boolean) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction de la résistance ultime à la traction de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '   lBetonLeger [E] :   Indique si on est en présence d'un béton léger (True) ou non (False)
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim indColonneReducFsk As Integer

        If lArmatureFormeeAFroid Then
            indColonneReducFsk = 1
        Else
            indColonneReducFsk = 2
        End If

        Return Recherche_TableReductionFacteur(TableReductionFacteurArmatures, TempA, indColonneReducFsk)

    End Function

#End Region

#Region " Courbe feu iso "

    Public Function TemperatureGazISO(TimeT As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Courbe ISO des gaz chauds (selon EN 1991-1-2 3.2.1 (1))
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TimeT       [E] :   Temps auquel on calcule la température en secondes
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim myTemp As Decimal

        myTemp = 20 + 345 * Math.Log10(8 * TimeT / kConvMinSec + 1)

        Return myTemp

    End Function

    Public Function TemperatureGazVoid(TimeT As Decimal, CRed1 As Decimal, CRed2 As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   13/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Courbe des gaz chauds dans les creux d'ondes non remplis
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TimeT       [E] :   Temps auquel on calcule la température en secondes
        '   CRed1, CRed2[E] :   Paramètres pour le calcul de la température
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim myTemp As Decimal
        Const Time40mn As Decimal = 60 * 40
        Dim Temp40 As Decimal = 20 + 345 * CRed1 * Math.Log10(321)
        Dim Temp120 As Decimal = 20 + 345 * CRed2 * Math.Log10(961)

        If IsSmallerOrEqual(TimeT, Time40mn) Then
            myTemp = 20 + 345 * CRed1 * Math.Log10(8 * TimeT / kConvMinSec + 1)
        Else

            myTemp = temp40 + (temp120 - temp40) / 80 * (TimeT / kConvMinSec - 40)
        End If


        Return myTemp

    End Function

#End Region

#Region " Echauffement tabulé de la dalle "

    Public Function EpaisseurEfficaceDalleMixte(td As Decimal, myBac As cls_Bac) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   25/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Renvoie l'épaisseur efficace d'une dalle mixte selon EN 1994-1-2:2005; D.4(1)
        '----------------------------------------------------------------------------------------------------------------
        '   td      [E] :   Epaisseur totale de la dalle
        '   myBac   [E] :   Bac
        '----------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim hEff As Decimal

        Dim h1, h2 As Decimal
        Dim L1, L2, L3 As Decimal

        '--( Initialisation

        h2 = myBac.Hp
        h1 = td - h2
        L1 = myBac.Bt
        L2 = myBac.Bb
        L3 = myBac.Ep - L1

        '--( Calcul

        If IsGreater(h1, 0.04) Then
            '# Cas épaisseur de dalle au dessus bac > 40 mm (cela doit etre le cas général)
            If IsSmallerOrEqual(h2 / h1, 1.5) Then
                hEff = h1 + 0.5 * h2 * (L1 + L2) / (L1 + L3)
            Else
                hEff = h1 * (1 + 0.75 * (L1 + L2) / (L1 + L3))
            End If
        Else
            '# Pas possible en dessous de 40 mm
            MsgBox("stiffness of the slab above the deck is less than 40 mm [cls_EurocodeFeu/EpaisseurEfficaceDalleMixte]")
        End If

        Return hEff

    End Function

    Public Sub PrepareMaillageDalleTabulee(EpDalle As Decimal, lGeneratUN As Decimal, ByRef nbTranches As Integer, ByRef EpTranches() As Decimal, ByRef zTranches() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - GUD
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Discrétisation de la dalle en tranches pour le calcul tabulé des températures
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   EpDalle     [E] :   Epaisseur de la dalle (considérée comme dalle pleine d'épaisseur constante)
        '   lGeneratUN  [E] :   Indique si première génération de l'Eurocode ou non
        '   nbTranches  [S] :   Nombre de tranches discrétisant la dalle
        '   EpTranches  [S] :   Epaisseur de chaque tranche
        '   zTranches   [S] :   Position z de la mi-epaisseur de chaque tranche
        '-------------------------------------------------------------------------------------------------------------------------------------------------

        If lGeneratUN Then
            If EpDalle <= 60 / 1000 Then 'épaisseur des tranches constante sur les 60 premiers mm

                If Math.Floor(EpDalle * 1000 / 5) = EpDalle * 1000 / 5 Then
                    nbTranches = Math.Floor(EpDalle * 1000 / 5)  'division euclidienne 
                Else
                    nbTranches = Math.Floor(EpDalle * 1000 / 5) + 1 'division euclidienne 
                End If

                ReDim EpTranches(nbTranches - 1)

                For i As Integer = 0 To nbTranches - 2
                    EpTranches(i) = 5 / 1000
                Next
                EpTranches(nbTranches - 1) = EpDalle - (nbTranches - 1) * 5 / 1000

            ElseIf EpDalle <= 80 / 1000 Then
                nbTranches = 13

                ReDim EpTranches(nbTranches - 1)

                For i As Integer = 0 To nbTranches - 2
                    EpTranches(i) = 5 / 1000
                Next
                EpTranches(nbTranches - 1) = EpDalle - (nbTranches - 1) * 5 / 1000

            Else
                nbTranches = 14

                ReDim EpTranches(nbTranches - 1)

                For i As Integer = 0 To nbTranches - 3
                    EpTranches(i) = 5 / 1000
                Next

                EpTranches(nbTranches - 2) = 20 / 1000

                EpTranches(nbTranches - 1) = EpDalle - (nbTranches - 2) * 5 / 1000 - 20 / 1000
            End If

        Else

            If EpDalle <= 2.5 / 1000 Then

                nbTranches = 1

                ReDim EpTranches(nbTranches - 1)

                EpTranches(0) = EpDalle

            ElseIf EpDalle <= 10 / 1000 Then

                nbTranches = 2

                ReDim EpTranches(nbTranches - 1)

                EpTranches(0) = 2.5 / 1000
                EpTranches(1) = EpDalle - 2.5 / 1000

            Else

                If Math.Floor(EpDalle * 1000 / 10) = EpDalle * 1000 / 10 Then
                    nbTranches = Math.Floor(EpDalle * 1000 / 10) + 1  'division euclidienne 
                Else
                    nbTranches = Math.Floor(EpDalle * 1000 / 10) + 2 'division euclidienne 
                End If

                nbTranches = Math.Min(nbTranches, 16) '16 tranches max

                ReDim EpTranches(nbTranches - 1)

                EpTranches(0) = 2.5 / 1000
                EpTranches(1) = 7.5 / 1000

                For i As Integer = 2 To nbTranches - 2
                    EpTranches(i) = 10 / 1000
                Next

                EpTranches(nbTranches - 1) = EpDalle - (nbTranches - 2) * 10 / 1000

            End If
        End If

        ReDim zTranches(nbTranches - 1)

        zTranches(0) = EpTranches(0) / 2

        For i As Integer = 1 To nbTranches - 1
            zTranches(i) = zTranches(i - 1) + (EpTranches(i - 1) + EpTranches(i)) / 2
        Next

    End Sub

    Public Sub TemperatureDalleTabuleeGeneration1(TimeStep As Decimal, nbTranches As Integer, ByRef TempDalle() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - GUD
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul des température de la dalle par la méthode tabulée
        '   Selon EN 1994-1-2:2005 Tableau D.5
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   TimeStep    [E] :   Temps de calcul
        '   nbTranches  [E] :   Nombre de tranches dans la dalle
        '   TempDalle   [S] :   Température dans chaque couche de la dalle
        '-------------------------------------------------------------------------------------------------------------------------------------------------

        '--( Initialisation - Déclaration

        Dim TabTempR30() As Decimal = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110, 80, 60}
        Dim TabTempR60() As Decimal = {705, 642, 581, 525, 469, 421, 374, 327, 289, 250, 200, 175, 140, 100}
        Dim TabTempR90() As Decimal = {1200, 738, 681, 627, 571, 519, 473, 428, 387, 345, 294, 271, 220, 160}
        Dim TabTempR120() As Decimal = {1200, 1200, 754, 697, 642, 591, 542, 493, 454, 415, 369, 342, 270, 210}
        Dim TabTempR180() As Decimal = {1200, 1200, 1200, 1200, 738, 689, 635, 590, 549, 508, 469, 430, 330, 260}
        Dim TabTempR240() As Decimal = {1200, 1200, 1200, 1200, 1200, 740, 700, 670, 645, 550, 520, 495, 395, 305} '1200 correspond à la valeur max quand le tableau n'indique pas de valeur dans l EC

        Dim TabTempC() As Decimal = Nothing

        '--( Sélection de la table

        Select Case TimeStep
            Case 30 : TabTempC = TabTempR30
            Case 60 : TabTempC = TabTempR60
            Case 90 : TabTempC = TabTempR90
            Case 120 : TabTempC = TabTempR120
            Case 180 : TabTempC = TabTempR180
            Case 240 : TabTempC = TabTempR240
        End Select

        '--( Transfert des valeurs

        ReDim TempDalle(nbTranches - 1)

        For iTn As Integer = 0 To nbTranches - 1

            TempDalle(iTn) = TabTempC(iTn)

        Next

    End Sub

    Public Sub TemperatureDalleTabuleeGeneration2(TimeStep As Decimal, nbTranches As Integer, ByRef TempDalle() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - GUD
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul des température de la dalle par la méthode tabulée
        '   Selon EN 1994-1-2:2024 Tableau B.6
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   TimeStep    [E] :   Temps de calcul
        '   nbTranches  [E] :   Nombre de tranches dans la dalle
        '   TempDalle   [S] :   Température dans chaque couche de la dalle
        '-------------------------------------------------------------------------------------------------------------------------------------------------

        '--( Initialisation - Déclaration

        Dim TabTempR30() As Decimal = {675, 513, 363, 260, 187, 135, 101, 76, 59, 46, 37, 31, 27, 24, 23, 22}
        Dim TabTempR60() As Decimal = {831, 684, 531, 418, 331, 263, 209, 166, 133, 108, 89, 73, 61, 51, 44, 38}
        Dim TabTempR90() As Decimal = {912, 777, 629, 514, 423, 349, 290, 241, 200, 166, 138, 117, 100, 86, 74, 65}
        Dim TabTempR120() As Decimal = {967, 842, 698, 583, 491, 415, 352, 300, 256, 218, 186, 159, 137, 119, 105, 94}
        Dim TabTempR180() As Decimal = {1042, 932, 797, 685, 591, 514, 448, 392, 344, 303, 267, 236, 209, 186, 166, 149}
        Dim TabTempR240() As Decimal = {1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200, 1200} ' Le calcul de R240 n'est pas applicable pour cette génération

        Dim TabTempC() As Decimal = Nothing

        '--( Sélection de la table

        Select Case TimeStep
            Case 30 : TabTempC = TabTempR30
            Case 60 : TabTempC = TabTempR60
            Case 90 : TabTempC = TabTempR90
            Case 120 : TabTempC = TabTempR120
            Case 180 : TabTempC = TabTempR180
            Case 240 : TabTempC = TabTempR240
        End Select

        '--( Transfert des valeurs

        ReDim TempDalle(nbTranches - 1)

        For iTn As Integer = 0 To nbTranches - 1

            TempDalle(iTn) = TabTempC(iTn)

        Next

    End Sub

    Public Sub TemperatureDalleTabulee(TimeStep As Decimal, lGeneratUN As Decimal, nbTranches As Integer, ByRef TempDalle() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - GUD
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul des température de la dalle par la méthode tabulée
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   TimeStep    [E] :   Temps de calcul
        '   lGeneratUN  [E] :   Indique si première génération de l'Eurocode (True) ou non (False)
        '   nbTranches  [E] :   Nombre de tranches dans la dalle
        '   TempDalle   [S] :   Température dans chaque couche de la dalle
        '-------------------------------------------------------------------------------------------------------------------------------------------------

        If lGeneratUN Then
            TemperatureDalleTabuleeGeneration1(TimeStep, nbTranches, TempDalle)
        Else
            TemperatureDalleTabuleeGeneration2(TimeStep, nbTranches, TempDalle)
        End If

    End Sub

#End Region

#Region " Echauffement des parties en acier protégées avec creux d'ondes non remplis "

    Public Function DeltaTempSemSupCreuxOnde(TempA As Decimal, TempGvoid As Decimal, Massivete As Decimal,
                                             TimeT As Decimal, DeltaT As Decimal, Cred1 As Decimal, Cred2 As Decimal, myParamFeu As cls_OptionsFeu) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   13/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Echauffement de la semelle supérieure protégée en présence de croux d'ondes non remplis
        '   sur un pas de temps DeltaT (selon EN 1993-1-2 § 4.2.5.2)
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier au début du pas de temps
        '   TempGvoid   [E] :   Température des gaz au début du pas de temps, dans la cavité
        '   Massivete   [E] :   Massiveté de la partie en acier protégée
        '   TimeT       [E] :   Temps en secondes
        '   DeltaT      [E] :   Pas de temps en secondes
        '   myParamFeu  [E] :   Options de calcul au feu
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim DeltaTempA As Decimal
        Dim cA, RhoA As Decimal                 ' Chaleur massique et masse volumique acier
        Dim FluxTherm, FluxConv, FluxRad As Decimal
        Dim TempG As Decimal
        Dim EpsilonA As Decimal

        '--( Traitement

        cA = Me.ChaleurSpecifiqueAcier(TempA)
        RhoA = cls_Acier.RHOACIER
        EpsilonA = EmissiviteAcier(TempA, myParamFeu.TypeSurface)

        If IsEqual(Cred2, 1) Then
            TempG = Me.TemperatureGazISO(TimeT)
        Else
            TempG = Me.TemperatureGazVoid(TimeT, Cred1, Cred2)
        End If

        FluxConv = myParamFeu.ConvectionCoef * (TempG - TempA)
        FluxRad = Me.FluxRadiatif(TempA, TempG, EpsilonA, myParamFeu, True)
        FluxTherm = FluxRad + FluxConv

        DeltaTempA = Massivete / (cA * RhoA) * DeltaT * FluxTherm

        '--( 

        Return Math.Max(0, DeltaTempA)

    End Function


    Public Function PhiVoid(myBac As cls_Bac, Bfs As Decimal, dp As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   13/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du paramètre Phi Cavité pour une onde de bac non remplies avec semelle protégée
        '--------------------------------------------------------------------------------------------------------------------------------
        '   myBac       [E] :   Bac de la dalle mixte
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   dp          [E] :   Epaisseur de la protection feu
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim Phi As Decimal
        Dim R1, R2 As Decimal

        R1 = 2 * myBac.Hp / (Bfs + 2 * dp)
        R2 = (myBac.Ep + myBac.Bb - 2 * myBac.Bt) / (Bfs + 2 * dp)

        Phi = 4 * Math.Atan(R1) * Math.Atan(R2)

        Return Phi

    End Function

    Public Function CoefRed1(PhiV As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du CRed1 
        '--------------------------------------------------------------------------------------------------------------------------------
        '   PhiV        [E] :   Coefficient de vue des creux d'ondes
        '--------------------------------------------------------------------------------------------------------------------------------

        Return Math.Max(0, 0.125 * Math.Log(PhiV) + 0.71)

    End Function

    Public Function CoefRed2(PhiV As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du CRed2 
        '--------------------------------------------------------------------------------------------------------------------------------
        '   PhiV        [E] :   Coefficient de vue des creux d'ondes
        '--------------------------------------------------------------------------------------------------------------------------------

        Return Math.Min(1, 0.11 * Math.Log(PhiV) + 0.8)

    End Function

    Public Function TemperatureTheta0(TempFI As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la temperature Theta0 à partir de laquelle on modifie la température de la semelle inférieure quand creux d'ondes
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempFI       [E] :   Température de la semelle inférieure
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim Theta0 As Decimal

        'Select Case True
        '    Case IsSmallerOrEqual(TempFI, 450)
        '        Theta0 = 400
        '    Case IsSmallerOrEqual(TempFI, 550) And IsGreater(TempFI, 450)
        '        Theta0 = Me.Interpole(450, 550, 400, 450, TempFI)
        '    Case IsSmallerOrEqual(TempFI, 650) And IsGreater(TempFI, 550)
        '        Theta0 = Me.Interpole(550, 650, 450, 500, TempFI)
        '    Case IsGreater(TempFI, 650)
        '        Theta0 = 500
        'End Select

        Theta0 = Me.ExtraireValeurTableauCoefModTi({400, 450, 550}, TempFI)

        Return Theta0

    End Function

    Private Function ExtraireValeurTableauCoefModTi(TabValeur() As Decimal, TempFi As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la temperature Theta0 à partir de laquelle on modifie la température de la semelle inférieure quand creux d'ondes
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TabValeur   [E] :   Tableau de valeurs à extraire (0 pour 450°C, 1 pour 550°C, 2 pour 650°C)
        '   TempFI      [E] :   Température de la semelle inférieure
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myVal As Decimal

        '--( Traitement

        Select Case True
            Case IsSmallerOrEqual(TempFi, 450)
                myVal = TabValeur(0)
            Case IsSmallerOrEqual(TempFi, 550) And IsGreater(TempFi, 450)
                myVal = Me.Interpole(450, 550, TabValeur(0), TabValeur(1), TempFi)
            Case IsSmallerOrEqual(TempFi, 650) And IsGreater(TempFi, 550)
                myVal = Me.Interpole(550, 650, TabValeur(1), TabValeur(2), TempFi)
            Case IsGreater(TempFi, 650)
                myVal = TabValeur(2)
        End Select

        Return myVal
    End Function


    Public Function TemperatureFiCorrigee(TempFi As Decimal, TempFs As Decimal, Theta0 As Decimal, Hpro As Decimal, iStep As Integer) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la température corrigée pour la semelle inférieure, dans le cas de la méthode du creux d'onde non rempli
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempFi      [E] :   Température initialie de la semelle inférieure
        '   TempFs      [E] :   Température de la semelle supérieure exposé aux gaz de la cavité
        '   Theta0      [E] :   Température seuil
        '   Hpro        [E] :   Hauteur du profilé
        '   iStep       [E] :   Indice de la durée d'incendie à prendre en compte
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim TempFiMod As Decimal
        Dim aTheta As Decimal

        '--( Calculs

        aTheta = Me.CoefATheta(TempFi, Hpro, iStep)

        TempFiMod = TempFi * (1 + (TempFs - Theta0) * aTheta)

        Return TempFiMod

    End Function


    Public Function CoefATheta(TempFi As Decimal, Hpro As Decimal, iStep As Integer) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de correction pour la température corrigée pour la semelle inférieure, dans le cas de la méthode du creux d'onde non rempli
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempFi      [E] :   Température initialie de la semelle inférieure
        '   Hpro        [E] :   Hauteur du profilé
        '   iStep       [E] :   Indice de la durée d'incendie à prendre en compte
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim ValH1(2) As Decimal
        Dim ValH2(2) As Decimal

        Dim nbHPro As Decimal

        Dim myATheta As Decimal
        Dim pCoef(1) As Decimal

        '--( Initialisation du tableau de valeurs

        Dim TabATheta(,,) As Decimal =
       {
            {
                {0.2, 1, 1.4, 0.65, 0},
                {0.2, 2, 3.2, 1.8, 0.75},
                {0.2, 3, 4.6, 2.8, 1.5},
                {0.2, 4, 5.6, 3.8, 2.0}
            },
            {
                {0.3, 1, 0, 0, 0},
                {0.3, 2, 1, 0, 0},
                {0.3, 3, 2, 1.1, 0.65},
                {0.3, 4, 2.6, 1.6, 0.88}
            },
            {
                {0.4, 1, 0, 0, 0},
                {0.4, 2, 0, 0, 0},
                {0.4, 3, 0.6, 0, 0},
                {0.4, 4, 1.2, 0.76, 0.35}
            },
            {
                {0.5, 1, 0, 0, 0},
                {0.5, 2, 0, 0, 0},
                {0.5, 3, 0, 0, 0},
                {0.5, 4, 0, 0, 0}
            }
       }


        '--( Initialisation

        nbHPro = TabATheta.GetUpperBound(0)

        '--( Traitement

        If IsSmaller(Hpro, TabATheta(0, 0, 0)) Then

            '== Cas d'une hauteur inférieure à la première des hauteurs traitées (200 mm)
            ValH1(0) = TabATheta(0, iStep, 2)
            ValH1(1) = TabATheta(0, iStep, 3)
            ValH1(2) = TabATheta(0, iStep, 4)

            myATheta = ExtraireValeurTableauCoefModTi(ValH1, TempFi)

        ElseIf IsGreaterOrEqual(Hpro, TabATheta(nbHPro, 0, 0)) Then

            '== Cas d'une hauteur supérieure ou égale à la dernière des hauteurs traitées (500 mm)

            ValH1(0) = TabATheta(nbHPro, iStep, 2)
            ValH1(1) = TabATheta(nbHPro, iStep, 3)
            ValH1(2) = TabATheta(nbHPro, iStep, 4)

            myATheta = ExtraireValeurTableauCoefModTi(ValH1, TempFi)

        Else
            '== Cas entre les deux

            Dim iHp As Integer = 0
            Dim lCont As Boolean

            lCont = (iHp < nbHPro - 1) And (IsGreaterOrEqual(Hpro, TabATheta(iHp + 1, 0, 0)))

            Do While lCont
                iHp += 1

                lCont = (iHp < nbHPro - 1) And (IsGreaterOrEqual(Hpro, TabATheta(iHp + 1, 0, 0)))
            Loop

            For i As Integer = 0 To 2
                ValH1(i) = TabATheta(iHp, iStep, i + 2)
                ValH2(i) = TabATheta(iHp + 1, iStep, i + 2)
            Next

            pCoef(0) = Me.ExtraireValeurTableauCoefModTi(ValH1, TempFi)
            pCoef(1) = Me.ExtraireValeurTableauCoefModTi(ValH2, TempFi)

            CoefATheta = Me.Interpole(TabATheta(iHp, 0, 0), TabATheta(iHp + 1, 0, 0), pCoef(0), pCoef(1), Hpro)

        End If

        Return CoefATheta / 10 ^ 4

    End Function

    Private Function Interpole(x1 As Decimal, x2 As Decimal, y1 As Decimal, y2 As Decimal, x As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   14/03/25 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Fonction d'interpolation linéaire
        '--------------------------------------------------------------------------------------------------------------------------------
        '   x1,x2       [E] :   Abscisses de référence
        '   y1,y2       [E] :   Valeurs du résultat pour les abscisses de référence
        '   x           [E] :   Abscisse ou on cherche le résultat
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim Result As Decimal

        Result = y1 + (y2 - y1) / (x2 - x1) * (x2 - x)

        Return Result

    End Function

#End Region

#Region " Echauffement des parties en acier "

    Public Function DeltaTempAcierProtege(TempA As Decimal, TempG As Decimal, Massivete As Decimal,
                                          TimeT As Decimal, DeltaT As Decimal, myParamFeu As cls_OptionsFeu) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Echauffement d'une partie en acier protégée sur un pas de temps DeltaT (selon EN 1993-1-2 § 4.2.5.2)
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier au début du pas de temps
        '   TempG       [E] :   Température des gaz au début du pas de temps
        '   Massivete   [E] :   Massiveté de la partie en acier protégée
        '   TimeT       [E] :   Temps en secondes
        '   DeltaT      [E] :   Pas de temps en secondes
        '   myParamFeu  [E] :   Options de calcul au feu
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim DeltaTempA As Decimal
        Dim cA, RhoA As Decimal                 ' Chaleur massique et masse volumique acier
        Dim LambdaP, RhoP, cP As Decimal        ' Conductivité thermique, chaleur massique et masse volumique matériau de protection   
        Dim Phi As Decimal
        Dim DeltaG As Decimal
        Dim TempGdT As Decimal
        Dim dP As Decimal

        '--( Traitement

        cA = Me.ChaleurSpecifiqueAcier(TempA)
        RhoA = cls_Acier.RHOACIER

        LambdaP = myParamFeu.Protection_Conductivite
        cP = myParamFeu.Protection_ChaleurMassique
        RhoP = myParamFeu.Protection_MasseVol
        dP = myParamFeu.EpProtection

        TempGdT = Me.TemperatureGazISO(TimeT - DeltaT)

        DeltaG = TempG - TempGdT
        Phi = cP * RhoP / (cA * RhoA) * myParamFeu.EpProtection * Massivete

        DeltaTempA = LambdaP * Massivete / (cA * RhoA * dP) / (1 + Phi / 3) * (TempG - TempA) * DeltaT - (Math.Exp(Phi / 10) - 1) * DeltaG

        '--( 

        Return Math.Max(0, DeltaTempA)

    End Function


    Public Function DeltaTempAcierNonProtege(TempA As Decimal, TempG As Decimal, Massivete As Decimal, ksh As Decimal,
                                             DeltaT As Decimal, myParamFeu As cls_OptionsFeu) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Echauffement d'une partie en acier non protégée sur un pas de temps DeltaT (selon EN 1993-1-2 § 4.2.5.1)
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier au début du pas de temps
        '   TempG       [E] :   Température des gaz au début du pas de temps
        '   Massivete   [E] :   Massiveté de la partie en acier
        '   ksh         [E] :   Shadow factor
        '   DeltaT      [E] :   Pas de temps en secondes
        '   myParamFeu  [E] :   Options de calcul au feu
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim DeltaTempA As Decimal
        Dim cA, RhoA As Decimal         ' Chaleur massique et masse volumique acier
        Dim FluxTherm, FluxConv, FluxRad As Decimal
        Dim EpsilonA As Decimal         ' Emissivité acier

        '--( Traitement

        cA = Me.ChaleurSpecifiqueAcier(TempA)
        RhoA = cls_Acier.RHOACIER
        EpsilonA = EmissiviteAcier(TempA, myParamFeu.TypeSurface)
        FluxConv = myParamFeu.ConvectionCoef * (TempG - TempA)
        FluxRad = Me.FluxRadiatif(TempA, TempG, EpsilonA, myParamFeu)
        FluxTherm = FluxRad + FluxConv

        DeltaTempA = (ksh * Massivete) / (cA * RhoA) * DeltaT * FluxTherm

        '--( 

        Return DeltaTempA
    End Function

    Public Function ChaleurSpecifiqueAcier(TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Echauffement d'une partie en acier non protégée sur un pas de temps DeltaT (selon EN 1993-1-2 § 3.4.1.2)
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier au début du pas de temps
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim myCa As Decimal

        '--( Traitement
        If IsSmaller(TempA, 600) Then
            myCa = 425 + 0.773 * TempA - 1.69 / 10 ^ 3 * TempA ^ 2 + 2.22 / 10 ^ 6 * TempA ^ 3
        ElseIf IsSmaller(TempA, 735) Then
            myCa = 665.999 + 13002 / (738 - TempA)
        ElseIf IsSmaller(TempA, 900) Then
            myCa = 545 + 17820 / (TempA - 731)
        Else
            myCa = 650
        End If
        Return myCa
    End Function

    Public Function EmissiviteAcier(TempA As Decimal, typeSurf As cls_OptionsFeu.enu_TypeSurface) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Emissivité de l'acier en fonction de la température et de l'état de surface
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '   typeSurf    [E] :   Type de surface, acier nu ou galvanisé
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim myEpsilonA As Decimal

        Select Case typeSurf
            Case cls_OptionsFeu.enu_TypeSurface.AcierNu
                myEpsilonA = 0.7
            Case cls_OptionsFeu.enu_TypeSurface.Galvanise
                If IsSmaller(TempA, 500) Then
                    myEpsilonA = 0.35
                Else
                    myEpsilonA = 0.7
                End If
            Case Else
                myEpsilonA = 0.7
        End Select

        Return myEpsilonA
    End Function

    Public Function FluxRadiatif(TempA As Decimal, TempG As Decimal, EpsilonA As Decimal, myParamFeu As cls_OptionsFeu, Optional lCreuxOndes As Boolean = False) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du flux radiatif sur une partie en acier non protégée 
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier au début du pas de temps
        '   TempG       [E] :   Température des gaz au début du pas de temps
        '   EpsilonA    [E] :   Emissivité de l'acier
        '   myParamFeu  [E] :   Options de calcul au feu
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim myFlux As Decimal
        Dim EpsilonF, Phi As Decimal
        Dim SigmaB As Decimal
        Const TREFK As Decimal = 273

        '--( Initialisation 

        If lCreuxOndes Then
            EpsilonF = myParamFeu.EmissivityFireCreuxO
        Else
            EpsilonF = myParamFeu.EmissivityFire
        End If
        SigmaB = cls_OptionsFeu.BOLTZMANN
        Phi = myParamFeu.PhiViewFactor

        '--( Traitement

        myFlux = Phi * EpsilonF * EpsilonA * SigmaB * ((TempG + TREFK) ^ 4 - (TempA + TREFK) ^ 4)

        Return myFlux

    End Function

#End Region

#Region " Application Annex F EN 1994-1-2 pour les sections à enrobage partiel "

    Public Function AnnexF_ReductionLargeurBfs(Time As Decimal, tf As Decimal, bf As Decimal, bc As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la réduction de largeur de la semelle supérieure
        '   Suivant Tableau F.2 de la NF EN 1994-1-2:2005
        '------------------------------------------------------------------------------------------------------------------------------
        '   Time        [E] :   Temps de calcul
        '   tf          [E] :   Epaisseur de semelle
        '   bf          [E] :   Largeur de semelle
        '   bc          [E] :   largeur d'enrobage
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Bfire As Decimal

        '--( Traitement

        Bfire = tf / 2 + (bf - bc) / 2

        Select Case Time
            Case 60 : Bfire += 10 / kUnitMM
            Case 90 : Bfire += 30 / kUnitMM
            Case 120 : Bfire += 40 / kUnitMM
            Case 180 : Bfire += 60 / kUnitMM
        End Select

        Return Bfire
    End Function

    Public Function AnnexF_ReducFyInf(Time As Decimal, Tf As Decimal, Ha As Decimal, Bc As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la réduction de la limite d'élasticité de la semelle inférieure
        '   Suivant Tableau F.4 de la NF EN 1994-1-2:2005
        '------------------------------------------------------------------------------------------------------------------------------
        '   Time        [E] :   Temps de calcul
        '   tf          [E] :   Epaisseur de semelle
        '   ha          [E] :   Hauteur du profilé
        '   bc          [E] :   largeur d'enrobage
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim ReducKa As Decimal
        Dim aZero As Decimal
        Dim KaMin, KaMax As Decimal

        '--( Initialisation

        aZero = 0.018 * (Tf * kUnitMM) + 0.7

        '--( Tableau F.4

        Select Case Time
            Case 30 : ReducKa = aZero * (1.12 - 84 / (Bc * kUnitMM) + Ha / Bc / 22) : KaMin = 0.5 : KaMax = 0.8
            Case 60 : ReducKa = aZero * (0.21 - 26 / (Bc * kUnitMM) + Ha / Bc / 24) : KaMin = 0.12 : KaMax = 0.4
            Case 90 : ReducKa = aZero * (0.12 - 17 / (Bc * kUnitMM) + Ha / Bc / 38) : KaMin = 0.06 : KaMax = 0.12
            Case 120 : ReducKa = aZero * (0.1 - 15 / (Bc * kUnitMM) + Ha / Bc / 40) : KaMin = 0.05 : KaMax = 0.1
            Case 180 : ReducKa = aZero * (0.03 - 3 / (Bc * kUnitMM) + Ha / Bc / 50) : KaMin = 0.03 : KaMax = 0.06
        End Select

        Return Math.Max(KaMin, Math.Min(ReducKa, KaMax))

    End Function

    Public Function AnnexF_HauteurHwInf(Time As Decimal, Tw As Decimal, Ha As Decimal, Bc As Decimal, Hw As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la hauteur inférieure d'âme
        '   Suivant Tableau F.3 de la NF EN 1994-1-2:2005
        '------------------------------------------------------------------------------------------------------------------------------
        '   Time        [E] :   Temps de calcul
        '   tw          [E] :   Epaisseur de l'âme
        '   ha          [E] :   Hauteur du profilé
        '   bc          [E] :   largeur d'enrobage
        '   Hw          [E] :   Hauteur de l'âme
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim RatioHsurB As Decimal
        Dim Hwl As Decimal
        Dim Coef_a1, Coef_a2 As Decimal
        Dim iStep As Integer
        Dim HwlMinCas1() As Decimal = {20, 30, 40, 45, 55}
        Dim HwlMinCas2() As Decimal = {20, 30, 40, 45, 55}
        Dim HwlMinCas3() As Decimal = {20, 30, 40, 45, 55}
        Dim HwlMin, HwlMax As Decimal

        '--( Initialisations

        RatioHsurB = Ha / Bc
        iStep = Array.IndexOf(cls_VerifFeuEnrobe.TimeSteps, Time)
        HwlMax = Hw

        '--( Traitement

        If IsSmallerOrEqual(RatioHsurB, 1) Then

            Coef_a1 = Me.TableauF3_A1_Cas1(iStep)
            Coef_a2 = Me.TableauF3_A2_Cas1(iStep)

            Hwl = Coef_a1 / Bc + Coef_a2 * Tw / (Bc * Ha)
            HwlMin = HwlMinCas1(iStep) / 1000

        ElseIf IsGreaterOrEqual(RatioHsurB, 2) Then

            Coef_a1 = Me.TableauF3_A1_Cas2(iStep)
            Coef_a2 = Me.TableauF3_A2_Cas2(iStep)

            Hwl = Coef_a1 / Bc + Coef_a2 * Tw / (Bc * Ha)
            HwlMin = HwlMinCas2(iStep) / 1000

        Else

            Hwl = Me.TableauF3_Cas3(Time, Tw, Ha, Bc)
            HwlMin = HwlMinCas3(iStep) / 1000

        End If

        Return Math.Min(HwlMax, Math.Max(Hwl, HwlMin))

    End Function

    Private Function TableauF3_Cas3(Time As Decimal, Tw As Decimal, Ha As Decimal, Bc As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la hauteur inférieure d'âme
        '   Suivant Tableau F.3 de la NF EN 1994-1-2:2005
        '   Cas où le ratio H/Bc est compris entre 1 et 2
        '------------------------------------------------------------------------------------------------------------------------------
        '   Time        [E] :   Temps de calcul
        '   tw          [E] :   Epaisseur de l'âme
        '   ha          [E] :   Hauteur du profilé
        '   bc          [E] :   largeur d'enrobage
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Hwl As Decimal

        '--( Traitement

        Select Case Time
            Case 30 : Hwl = (3600 / Bc) / kUnitMM ^ 2
            Case 60 : Hwl = (9500 / Bc + 20000 * Tw / (Bc * Ha) * (2 - Ha / Bc)) / kUnitMM ^ 2
            Case 90 : Hwl = (14000 / Bc + Tw / (Bc * Ha) * (75000 + 85000 * (2 - Ha / Bc))) / kUnitMM ^ 2
            Case 120 : Hwl = (23000 / Bc + Tw / (Bc * Ha) * (110000 + 70000 * (2 - Ha / Bc))) / kUnitMM ^ 2
            Case 180 : Hwl = (35000 / Bc + Tw / (Bc * Ha) * (250000 + 150000 * (2 - Ha / Bc))) / kUnitMM ^ 2
        End Select

        Return Hwl

    End Function

    Private Function TableauF3_A1_Cas1(iStep As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient A_1
        '   Suivant Tableau F.3 de la NF EN 1994-1-2:2005
        '   dans le cas où H/Bc <= 1
        '------------------------------------------------------------------------------------------------------------------------------
        '   iStep        [E] :   Temps de calcul
        '------------------------------------------------------------------------------------------------------------------------------

        Dim TabA1() As Decimal = {3600, 9500, 14000, 23000, 35000}

        Return TabA1(iStep) / kUnitMM ^ 2

    End Function

    Private Function TableauF3_A2_Cas1(iStep As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient A_2
        '   Suivant Tableau F.3 de la NF EN 1994-1-2:2005
        '   dans le cas où H/Bc <= 1
        '------------------------------------------------------------------------------------------------------------------------------
        '   iStep        [E] :   Temps de calcul
        '------------------------------------------------------------------------------------------------------------------------------

        Dim TabA2() As Decimal = {0, 20000, 160000, 180000, 400000}

        Return TabA2(iStep) / kUnitMM ^ 2

    End Function

    Private Function TableauF3_A1_Cas2(iStep As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient A_1
        '   Suivant Tableau F.3 de la NF EN 1994-1-2:2005
        '   dans le cas où H/Bc >= 2
        '------------------------------------------------------------------------------------------------------------------------------
        '   iStep        [E] :   Temps de calcul
        '------------------------------------------------------------------------------------------------------------------------------

        Dim TabA1() As Decimal = {3600, 9500, 14000, 23000, 35000}

        Return TabA1(iStep) / kUnitMM ^ 2

    End Function

    Private Function TableauF3_A2_Cas2(iStep As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient A_2
        '   Suivant Tableau F.3 de la NF EN 1994-1-2:2005
        '   dans le cas où H/Bc >= 2
        '------------------------------------------------------------------------------------------------------------------------------
        '   iStep        [E] :   Temps de calcul
        '------------------------------------------------------------------------------------------------------------------------------

        Dim TabA2() As Decimal = {0, 0, 75000, 110000, 250000}

        Return TabA2(iStep) / kUnitMM ^ 2

    End Function

    Public Function AnnexF_ReductionDalle(Time As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la réduction d'épaisseur de la dalle
        '   Suivant Tableau F.1 de la NF EN 1994-1-2:2005
        '------------------------------------------------------------------------------------------------------------------------------
        '   Time        [E] :   Temps de calcul
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim HcFi As Decimal
        Dim TabF1() As Decimal = {10, 20, 30, 40, 55}
        Dim iStep As Integer

        '--( Traitement

        iStep = Array.IndexOf(cls_VerifFeuEnrobe.TimeSteps, Time)

        HcFi = TabF1(iStep) / kUnitMM

        Return HcFi

    End Function

    Public Function AnnexF_ReductionKrArmaEnrob(iStep As Integer, Ha As Decimal, Bc As Decimal, Tw As Decimal, uBord As Decimal, uSemel As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   19/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la réduction de limite d'élasticité
        '   Suivant Tableau F.5 de la NF EN 1994-1-2:2005
        '------------------------------------------------------------------------------------------------------------------------------
        '   iStep       [E] :   Indice du temps de calcul
        '   Ha, Bc, Tw  [E] :   Hauteur du profilé, largeur de l'enrobage, épaisseur de l'âme
        '   uBord       [E] :   Distance de l'axe de l'armature au bord du béton
        '   uSemel      [E] :   Distance de l'axe de l'armature à la face interne de la semelle inférieure
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim kReducKr As Decimal
        Dim uDistMM As Decimal
        Dim CoefA3, CoefA4, CoefA5 As Decimal
        Dim Am, Vol As Decimal
        Const KrMin As Decimal = 0.1
        Const KrMax As Decimal = 1

        '--( Calcul

        Am = 2 * Ha + Bc
        Vol = Ha * Bc

        uDistMM = kUnitMM * (uBord ^ (-1) + uSemel ^ (-1) + (Bc - Tw - uBord) ^ (-1)) ^ (-1)

        CoefA3 = TableauF5_CoefficientA(3, iStep)
        CoefA4 = TableauF5_CoefficientA(4, iStep)
        CoefA5 = TableauF5_CoefficientA(5, iStep)

        kReducKr = (uDistMM * CoefA3 + CoefA4) * CoefA5 / Math.Sqrt(Am / Vol / kUnitMM)

        Return Math.Max(KrMin, Math.Min(KrMax, kReducKr))

    End Function

    Private Function TableauF5_CoefficientA(iCoefA As Integer, iStep As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   19/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Extraction des coefs Ai Suivant Tableau F.5 de la NF EN 1994-1-2:2005
        '------------------------------------------------------------------------------------------------------------------------------
        '   iCoefA      [E] :   Indice du coefficient A
        '   iStep       [E] :   Indice du temps de calcul
        '------------------------------------------------------------------------------------------------------------------------------

        Dim myCoefA As Decimal

        Dim tabA3() As Decimal = {0.062, 0.034, 0.026, 0.026, 0.024}
        Dim tabA4() As Decimal = {0.16, -0.04, -0.154, -0.284, -0.562}
        Dim tabA5() As Decimal = {0.126, 0.101, 0.09, 0.082, 0.076}

        Select Case iCoefA
            Case 3 : myCoefA = tabA3(iStep)
            Case 4 : myCoefA = tabA4(iStep)
            Case 5 : myCoefA = tabA5(iStep)
        End Select
        Return myCoefA
    End Function

    Public Function AnnexF_bcr(Time As Integer, bc As Decimal) As Decimal
        Dim bcfi, bcr As Decimal

        bcfi = Me.AnnexF_bcfi(Time, bc)

        bcr = Math.Max(bc - 2 * bcfi, 0)

        Return bcr

    End Function

    Private Function AnnexF_bcfi(Time As Integer, bc As Decimal) As Decimal
        '--( Déclaration

        Dim bcfi As Decimal

        '--( Traitement

        Select Case Time
            Case 30 : bcfi = 25 / kUnitMM
            Case 60 : bcfi = Math.Max((60 - 0.15 * bc * kUnitMM), 30) / kUnitMM
            Case 90 : bcfi = Math.Max((70 - 0.1 * bc * kUnitMM), 35) / kUnitMM
            Case 120 : bcfi = Math.Max((75 - 0.1 * bc * kUnitMM), 45) / kUnitMM
            Case 180 : bcfi = Math.Max((85 - 0.1 * bc * kUnitMM), 55) / kUnitMM
        End Select

        Return bcfi
    End Function

    Public Function AnnexF_hcfi(Time As Integer, ha As Integer, bc As Decimal) As Decimal
        '--( Déclaration

        Dim hcfi As Decimal

        '--( Traitement

        Select Case Time
            Case 30 : hcfi = 25 / kUnitMM
            Case 60 : hcfi = Math.Max((165 - 0.4 * bc * kUnitMM - 8 * ha / bc), 30) / kUnitMM
            Case 90 : hcfi = Math.Max((220 - 0.5 * bc * kUnitMM - 8 * ha / bc), 45) / kUnitMM
            Case 120 : hcfi = Math.Max((290 - 0.6 * bc * kUnitMM - 10 * ha / bc), 55) / kUnitMM
            Case 180 : hcfi = Math.Max((360 - 0.7 * bc * kUnitMM - 10 * ha / bc), 65) / kUnitMM
        End Select

        Return hcfi
    End Function

    ''' <summary>
    ''' Fonction qui renvoi le coefficient minorateur appliqué aux armatures de la dalle béton dans le cas de l'annexe F avec M<0
    ''' </summary>
    ''' <param name="Time">Durée du feu en minutes</param>
    ''' <param name="u">Distance du lit d'armatures à la face la plus proche de la dalle béton</param>
    ''' <returns></returns>
    Public Function AnnexF_ksd(Time As Integer, u As Decimal)
        '--( Déclaration
        Dim ksd As Decimal

        '--( Traitement

        Select Case Time
            Case 30 : ksd = 1
            Case 60 : ksd = 0.022 * u * kUnitMM + 0.34
            Case 90 : ksd = 0.0275 * u * kUnitMM - 0.1
            Case 120 : ksd = 0.022 * u * kUnitMM - 0.2
            Case 180 : ksd = 0.018 * u * kUnitMM - 0.26
        End Select

        ksd = Math.Max(ksd, 0) 'on minore par 0
        ksd = Math.Min(ksd, 1) 'on majore par 1

        Return ksd
    End Function
#End Region

#Region " Exposition de la semelle supérieure et autres fonctions "

    Public Function NombreTimeStepsIncendie(myBeam As cls_Poutre) As Integer
        '-----------------------------------------------------------------------------------------------------------------
        '   16/03/25 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Indique le nombre de durées d'exposition prises en compte dans le calcul incendie
        '-----------------------------------------------------------------------------------------------------------------       
        '   
        '-----------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim NbStepsCalcul As Integer

        If Me.MethodeCreuxOnde(myBeam) Then
            NbStepsCalcul = Array.IndexOf(cls_VerifFeuMixte.TimeSteps, CDec(120), 0) + 1
        Else
            NbStepsCalcul = cls_VerifFeuMixte.TimeSteps.GetUpperBound(0) + 1
        End If

        Return NbStepsCalcul

    End Function

    Public Function MethodeCreuxOnde(myBeam As cls_Poutre) As Boolean
        '------------------------------------------------------------------------------------------------------------------------------
        '   22/10/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Indique si on applique la méthode du creux d'onde
        '------------------------------------------------------------------------------------------------------------------------------
        '   myBeam  [E] :   Poutre traitée
        '------------------------------------------------------------------------------------------------------------------------------

        Return myBeam.Dalle.lMixte And myBeam.Dalle.Bac.lPerpendiculaire _
                                   And (myBeam.Dalle.Bac.AppuiT <> cls_Bac.EnuConfigTAppui.Discontinu) And (Not myBeam.ParamFeu.lCreuxProteges) _
                                   And (myBeam.ParamFeu.lProtectionPaint Or myBeam.ParamFeu.lProtectionSpray)
    End Function

    Public Function SemelleSupExposee(myBeam As cls_Poutre) As Boolean
        '------------------------------------------------------------------------------------------------------------------------------
        '   22/10/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Indique si la semelle supérieure est exposée au feu
        '------------------------------------------------------------------------------------------------------------------------------
        '   myBeam  [E] :   Poutre traitée
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lExpo As Boolean
        Dim Cr As Decimal

        '--( Traitement

        If myBeam.Dalle.lMixte Then

            Select Case myBeam.Dalle.Bac.Orientation
                Case cls_Bac.Enum_Orientation.Parallele
                    Cr = myBeam.Dalle.Bac.Bb / myBeam.Section.ProfilA.Bfs

                Case cls_Bac.Enum_Orientation.Perpendiculaire
                    Select Case myBeam.Dalle.Bac.AppuiT
                        Case cls_Bac.EnuConfigTAppui.Discontinu
                            Cr = 1
                        Case Else
                            Cr = myBeam.Dalle.Bac.Bb / myBeam.Section.ProfilA.Bfs
                    End Select

            End Select

            lExpo = IsGreaterOrEqual(Cr, 0.85)

        Else

            lExpo = False

        End If

        Return lExpo
    End Function

#End Region

#Region " Massiveté des sections "

    Public Function MassiveteSemelleInf(myProfil As cls_ProfilA) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une semelle inférieure
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Aire, Peri As Decimal

        '--( Calculs

        Aire = myProfil.AireFi
        Peri = 2 * (myProfil.Bfi + myProfil.Tfi)

        Return (Peri / Aire)

    End Function

    Public Function MassiveteSemelleSup(myProfil As cls_ProfilA, lSemSupExposee As Boolean) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une semelle supérieure
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '   lSemSupExposee  [E] :   Indique si on considère la semelle sup comme exposée ou non
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Aire, Peri As Decimal

        '--( Calculs

        Aire = myProfil.AireFs
        Peri = myProfil.Bfs + 2 * myProfil.Tfs

        If lSemSupExposee Then Peri += myProfil.Bfs

        Return (Peri / Aire)

    End Function

    Public Function MassiveteSemelleSupCreuxOndes(myBac As cls_Bac, myProfil As cls_ProfilA) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une semelle supérieure
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '   myBac           [E] :   Bac du plancher mixte
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim MassiV As Decimal

        '--( Calculs

        MassiV = (2 * myBac.Bt + myBac.Bb - myBac.Ep) / ((myBac.Bt + myBac.Bb) * myProfil.Tfs)

        Return (MassiV)

    End Function

    Public Function MassiveteAme(myProfil As cls_ProfilA) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté de l'âme
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Aire, Peri As Decimal

        '--( Calculs

        Aire = myProfil.Aire - myProfil.AireFi - myProfil.AireFs
        Peri = 2 * (myProfil.HauteurAmeDw) + Math.PI * (myProfil.Rci + myProfil.Rcs)

        Return (Peri / Aire)

    End Function

    Public Function MassiveteSectionAcier(myProfil As cls_ProfilA, lSemSupExposee As Boolean) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une section acier seule
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '   lSemSupExposee  [E] :   Indique si on considère la semelle sup comme exposée ou non
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Aire, Peri As Decimal

        '--( Calculs

        Aire = myProfil.Aire
        Peri = myProfil.Bfs + 2 * (myProfil.ha + myProfil.Bfi - myProfil.Tw) + (Math.PI - 3) * (myProfil.Rci + myProfil.Rcs)

        If lSemSupExposee Then
            Peri += myProfil.Bfs
        End If

        Return (Peri / Aire)

    End Function

    Public Function MassiveteSectionAcierBoardP(myProfil As cls_ProfilA) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une section acier seule avec protection par panneaux
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Aire, Peri As Decimal

        '--( Calculs

        Aire = myProfil.Aire
        Peri = Math.Max(myProfil.Bfi, myProfil.Bfs) + 2 * myProfil.ha

        Return (Peri / Aire)

    End Function

    Public Function MassiveteSectionAcierBox(myProfil As cls_ProfilA, lSemSupExposee As Boolean) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une section acier seule boxée
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '   lSemSupExposee  [E] :   Indique si on considère la semelle sup comme exposée ou non
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Aire, Peri As Decimal
        Dim BfMax As Decimal

        '--( Calculs

        Aire = myProfil.Aire
        BfMax = Math.Max(+myProfil.Bfi, myProfil.Bfs)
        Peri = BfMax + 2 * myProfil.ha

        If lSemSupExposee Then
            Peri += BfMax
        End If

        Return (Peri / Aire)

    End Function

#End Region

#Region " Facteur de vue "

    Public Function kShMixte(myProfil As cls_ProfilA) As Decimal
        '------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la massiveté d'une section acier seule boxée
        '------------------------------------------------------------------------------------------------------------------------------
        '   myProfil        [E] :   Profilé
        '------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim mykSh As Decimal
        Dim NumK, DenomK As Decimal
        Dim Hw As Decimal = myProfil.HauteurAmeHw

        '--(  Calcul

        NumK = myProfil.Tfi + myProfil.Tfs + myProfil.Bfi / 2 + Math.Sqrt(Hw ^ 2 + 0.25 * (myProfil.Bfs - myProfil.Bfi) ^ 2)
        DenomK = Hw + myProfil.Bfi + myProfil.Bfs / 2 + myProfil.Tfi + myProfil.Tfs - myProfil.Tw

        mykSh = 0.9 * NumK / DenomK

        Return mykSh


    End Function


#End Region

#Region " Propriétés fonction de la température "

    Public Function Masse_volumique_beton(ByVal lNormal As Boolean, ByVal lVariable As Boolean,
                                          ByVal lGeneration1 As Boolean, ByVal Rho0 As Decimal, ByVal Theta As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------------------
        '   15/05/24 :  Création - GiB
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Masse volumique du béton selon EN 1994-1-2:2005 3.4 (2)
        '                               ou prEN 1994-1-2:2024
        '-----------------------------------------------------------------------------------------------------------------------------
        '   lNormal         [E] :   type de beton : true (NC) ou false (LC)
        '   lVariable       [E] :   variation en fonction de la temperature : true (variable) ou false (valeur constante) 
        '   lGeneration1    [E] :   génération 1 (true) ou 2 (false) de l'EN 1994-1-2 
        '   Rho0            [E] :   masse volumique du béton à 20°C
        '   Theta           [E] :   temperature du béton  (°C)
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Rho_c As Decimal

        '--( Traitement 

        If (Not lNormal) Or (Not lVariable) Then
            '## beton léger ou valeur constante de rho
            Rho_c = Rho0
        ElseIf lGeneration1 Then
            '## beton normal avec variation selon la 1re génération des Eurocodes (EN 1994-1-2:2005 3.4 (2))
            Rho_c = 2354.0 - 23.47 * Theta / 100.0
        ElseIf Theta >= 20.0 Then
            '## béton NC : 2e génération des Eurocodes (prEN 1994-1-2:2024 )
            If Theta <= 115.0# Then
                Rho_c = Rho0
            ElseIf Theta <= 200.0 Then
                Rho_c = Rho0 * (1 - 0.02 * (Theta - 115.0) / 85.0#)
            ElseIf Theta <= 400.0 Then
                Rho_c = Rho0 * (0.98 - 0.03 * (Theta - 200.0) / 200.0)
            ElseIf Theta <= 1200.0 Then
                Rho_c = Rho0 * (0.95 - 0.07 * (Theta - 400.0) / 800.0)
            End If

        End If

        Return Rho_c

    End Function


    Public Function Conductivite_thermique_beton(ByVal lNormal As Boolean, lANF As Boolean, ByVal lGene1 As Boolean, ByVal ThetaC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------------------
        '   15/05/24 :  Création - GiB
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Conductivite thermique du beton selon NF EN 1994-1-2
        '   Génération 1 : EN 1994-1-2:2005 3.3.2 (8) et 3.3.3 (3)
        '   Génération 2 : prEN 1994-1-2:2024 xxx
        '-----------------------------------------------------------------------------------------------------------------------------
        '   lNormal     [E] :   type de beton : true (NC) ou false (LC)
        '   lANF        [E] :   courbe du beton : true (ANF) ou false (limite superieure)
        '   lGene1      [E] :   génération 1 (true) ou 2 (false) de l'EN 1994-1-2 
        '   ThetaC      [E] :   temperature (°C)
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim LambdaC As Decimal
        Dim ValSup As Decimal
        Dim ValInf As Decimal
        Dim Pente As Decimal
        Dim Ordo As Decimal

        '--( Traitement

        If lNormal Then
            '## beton Normal

            ValSup = 2.0 - 0.2451 * ThetaC / 100.0 + 0.0107 * (ThetaC / 100.0) ^ 2
            ValInf = 1.36 - 0.136 * ThetaC / 100.0 + 0.0057 * (ThetaC / 100.0) ^ 2
            If (lGene1 AndAlso Not lANF) Or ThetaC <= 140.0 Then    'limite superieure
                LambdaC = ValSup
            ElseIf ThetaC >= 160.0 AndAlso ThetaC <= 1200.0 AndAlso (lANF Or Not lGene1) Then       'ANF
                LambdaC = ValInf
            Else  'Annexe Nationale Francaise
                Pente = (ValInf - ValSup) / (160.0 - 140.0)
                Ordo = ValSup - Pente * 140.0
                LambdaC = Pente * ThetaC + Ordo
            End If

        Else
            '## beton léger 1re et 2e générations des Eurocodes

            If ThetaC >= 20.0 AndAlso ThetaC <= 800.0 Then
                LambdaC = 1 - ThetaC / 1600.0
            ElseIf ThetaC > 800.0 AndAlso ThetaC <= 1200.0 Then
                LambdaC = 0.5
            End If
        End If

        Return LambdaC

    End Function

    Public Function Chaleur_specifique_beton(ByVal lNormal As Boolean, ByVal val_u As Decimal,
                                             ByVal lGene1 As Boolean, ByVal ThetaC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------------------
        '   15/05/24 :  Création - GiB
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Chaleur specifique du beton selon NF EN 1994-1-2
        '   génération 1 : EN 1994-1-2:2005 3.3.2 (4) ou 3.3.3 (2)
        '   génération 2 : prEN 1994-1-2:2024 
        '-----------------------------------------------------------------------------------------------------------------------------
        '   lNormal     [E] :   type de beton : true (NC) ou false (LC)
        '   lGene1      [E] :   génération 1 (true) ou 2 (false) de l'EN 1994-1-2 
        '   ThetaC      [E] :   temperature (°C)
        '   val_u       [E] :   taux d'humidite (%)
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim cp_c As Decimal
        Dim val_cc As Decimal
        Dim Pente As Decimal
        Dim Ordo As Decimal

        '   Valeurs du pic
        '   1re generation :    3 valeurs de teneur en eau pour lesquelles le pic est precise : 0%, 3% et 10%
        '   2e generation :     4 valeurs de teneur en eau pour lesquelles le pic est precise : 0%, 1.5%, 3% et 10%

        '--( Traitement

        If (Not lNormal) AndAlso lGene1 Then
            '## beton léger 1re generation des Eurocodes

            cp_c = 840.0

        Else
            '## beton normal 1re generation ou beton NC ou LC 2e generation

            If Math.Abs(val_u) <= 0.0001 Then
                val_cc = 900.0
            ElseIf (Not lGene1) AndAlso Math.Abs(val_u - 1.5) <= 0.0001 Then '2e génération des Eurocodes : 1.5%
                val_cc = 1470.0
            ElseIf Math.Abs(val_u - 3.0) <= 0.0001 Then
                val_cc = 2020.0
            ElseIf Math.Abs(val_u - 10.0) <= 0.0001 Then
                val_cc = 5600.0
            ElseIf val_u > 0.0 AndAlso val_u < 3.0 Then   'teneur en eau inferieure à 3%
                If lGene1 Then    '1re generation
                    val_cc = 900.0 + (2020.0 - 900.0) * val_u / 3.0
                ElseIf val_u < 1.5 Then '2e generation : teneur en eau inferieure a 1.5%
                    val_cc = 900.0 + (1470.0 - 900.0) * val_u / 1.5
                Else '2e generation : teneur en eau superieure comprise entre 1.5% et 3%
                    val_cc = 1470.0 + (2020.0 - 1470.0) * (val_u - 1.5) / (3.0 - 1.5)
                End If
            ElseIf val_u > 3.0 AndAlso val_u < 10.0 Then      'teneur en eau comprise entre 3% et 10%
                val_cc = 2020.0 + (5600.0 - 2020.0) * (val_u - 3.0) / (10.0 - 3.0)
            End If

            '### Variation en fonction du pic et de la temperature
            If ThetaC <= 100.0 Then
                cp_c = 900.0
            ElseIf ThetaC <= 200.0 Then
                If ThetaC <= 115.0 Then
                    If lGene1 OrElse ThetaC <= 100.001 Then
                        If lGene1 Then
                            Pente = (900.0 - val_cc) / (100.0 - 115.0)
                        Else
                            Pente = (900.0 - val_cc) / (100.001 - 100.0)
                        End If
                        Ordo = 900.0 - Pente * 100.0
                        cp_c = Pente * ThetaC + Ordo
                    Else
                        cp_c = val_cc
                    End If
                Else
                    Pente = (1000.0 - val_cc) / (200.0 - 115.0)
                    Ordo = 1000.0 - Pente * 200.0
                    cp_c = Pente * ThetaC + Ordo
                End If
            ElseIf ThetaC <= 400.0 Then
                cp_c = 1000.0 + (ThetaC - 200.0) / 2.0
            ElseIf ThetaC <= 1200.0 Then
                cp_c = 1100.0
            End If

        End If

        Return cp_c

    End Function

#End Region

#Region " Déversement incendie "

    Public Function AlphaLT(FyAcier As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/10/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient d'imperfection pour le déversement à l'incendie
        '   Selon EN 1993-1-2, 4.2.3.3 (5)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   FyAcier     [E] :   Limite d'élasticité
        '-----------------------------------------------------------------------------------------------------------------------------

        Return 0.65 * Math.Sqrt(235 / FyAcier)

    End Function

    Public Function KhiLTFire(AlphaLTfi As Decimal, LambdaBfi As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/10/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction pour le déversement à l'incendie
        '   Selon EN 1993-1-2, 4.2.3.3 (5)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   LambdaBfi       [E] :   Elancement réduit
        '   AlphaLTfi       [E] :   Coefficient d'imperfection
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim PhiLT, KhiLT As Decimal

        '--( Calcul

        PhiLT = 0.5 * (1 + AlphaLTfi * LambdaBfi + LambdaBfi ^ 2)
        KhiLT = Math.Min(1, 1 / (PhiLT + Math.Sqrt(PhiLT ^ 2 - LambdaBfi ^ 2)))


        Return KhiLT
    End Function


#End Region
End Class
