Public Class cls_EurocodesFeu

#Region " Déclarations "

    Const kUnitMM As Decimal = 1000

#End Region

#Region " Coefficients de réduction des propriétés mécaniques en fonction de la température "

    Public Function ReducFyAcier(TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction de la limite d'élasticité de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myReduc As Decimal

        '--( Traitement

        myReduc = 1 - TempA / 1200

        Return myReduc
    End Function


    Public Function ReducEyAcier(TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction du module d'Young de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '--------------------------------------------------------------------------------------------------------------------------------

    End Function


    Public Function ReducFuAcier(TempA As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Coefficient de réduction de la résistance ultime à la traction de l'acier en fonction de la température
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier
        '--------------------------------------------------------------------------------------------------------------------------------

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

#End Region

#Region " Echauffement tabulé de la dalle "

    Public Sub PrepareMaillageDalleTabulee(EpDalle As Decimal, lGeneratUN As Decimal, ByRef nbTranches As Integer, ByRef EpTranches() As Decimal, ByRef zTranches() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Discrétisation de la dalle en tranches pour le calcul tabulé des températures
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   EpDalle     [E] :   Epaisseur de la dalle (considérée comme dalle pleine d'épaisseur constante)
        '   lGeneratUN  [E] :   Indique si première génération de l'Eurocode ou non
        '   nbTranches  [S] :   Nombre de tranches discrétisant la dalle
        '   EpTranches  [S] :   Epaisseur de chaque tranche
        '   zTranches   [S] :   Position z de la mi-epaisseur de chaque tranche
        '-------------------------------------------------------------------------------------------------------------------------------------------------



    End Sub

    Public Sub TemperatureDalleTabuleeGeneration1(TimeStep As Decimal, nbTranches As Integer, ByRef TempDalle() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création
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
        Dim TabTempR60() As Decimal = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110, 80, 60}
        Dim TabTempR90() As Decimal = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110, 80, 60}
        Dim TabTempR120() As Decimal = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110, 80, 60}
        Dim TabTempR180() As Decimal = {535, 470, 415, 350, 300, 250, 210, 180, 160, 140, 125, 110, 80, 60}
        Dim TabTempR240() As Decimal = {1200, 1200, 1200, 1200, 1200, 740, 700, 670, 645, 550, 520, 495, 395, 305}

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
        '   23/04/24 :  Création
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul des température de la dalle par la méthode tabulée
        '   Selon EN 1994-1-2:2024 Tableau B.6
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   TimeStep    [E] :   Temps de calcul
        '   nbTranches  [E] :   Nombre de tranches dans la dalle
        '   TempDalle   [S] :   Température dans chaque couche de la dalle
        '-------------------------------------------------------------------------------------------------------------------------------------------------

    End Sub

    Public Sub TemperatureDalleTabulee(TimeStep As Decimal, lGeneratUN As Decimal, nbTranches As Integer, ByRef TempDalle() As Decimal)
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul des température de la dalle par la méthode tabulée
        '-------------------------------------------------------------------------------------------------------------------------------------------------
        '   TimeStep    [E] :   Temps de calcul
        '   lGeneratUN  [E] :   Indique si première génération de l'Eurocode ou non
        '   nbTranches  [E] :   Nombre de tranches dans la dalle
        '   TempDalle   [S] :   Température dans chaque couche de la dalle
        '-------------------------------------------------------------------------------------------------------------------------------------------------


    End Sub

#End Region

#Region " Echauffement des parties en acier "

    Public Function DeltaTempAcierProtege(TempA As Decimal, TempG As Decimal, Massivete As Decimal, ksh As Decimal,
                                          TimeT As Decimal, DeltaT As Decimal, myParamFeu As cls_OptionsFeu) As Decimal
        '--------------------------------------------------------------------------------------------------------------------------------
        '   23/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------------
        '   Echauffement d'une partie en acier protégée sur un pas de temps DeltaT (selon EN 1993-1-2 § 4.2.5.2)
        '--------------------------------------------------------------------------------------------------------------------------------
        '   TempA       [E] :   Température de l'acier au début du pas de temps
        '   TempG       [E] :   Température des gaz au début du pas de temps
        '   Massivete   [E] :   Massiveté de la partie en acier protégée
        '   ksh         [E] :   Shadow factor
        '   TimeT       [E] :   Temps en secondes
        '   DeltaT      [E] :   Pas de temps en secondes
        '   myParamFeu  [E] :   Options de calcul au feu
        '--------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim DeltaTempA As Decimal
        Dim cA, RhoA As Decimal             ' Chaleur massique et masse volumique acier
        Dim LambdaP, RhoP, cP As Decimal    ' Conductivité thermique, chaleur massique et masse volumique matériau de protection   
        Dim Phi As Decimal
        Dim DeltaG As Decimal

        '--( Traitement

        cA = Me.ChaleurSpecifiqueAcier(TempA)
        RhoA = cls_Acier.RHOACIER

        LambdaP = myParamFeu.Protection_Conductivite
        cP = myParamFeu.Protection_ChaleurMassique
        RhoP = myParamFeu.Protection_MasseVol

        DeltaG = Me.TemperatureGazISO(TimeT + DeltaT) - TempG
        Phi = cP * RhoP / (cA * RhoA) * myParamFeu.EpProtection * Massivete

        DeltaTempA = LambdaP * Massivete / (cA * RhoA) / (1 + Phi / 3) * (TempG - TempA) * DeltaT - (Math.Exp(Phi / 10) - 1) * deltag

        '--( 

        Return DeltaTempA

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
        Dim FluxTherm, FluxConv As Decimal
        Dim EpsilonA As Decimal         ' Emissivité acier

        '--( Traitement

        cA = Me.ChaleurSpecifiqueAcier(TempA)
        RhoA = cls_Acier.RHOACIER
        FluxConv = myParamFeu.ConvectionCoef * (TempG - TempA)
        FluxTherm = Me.FluxRadiatif(TempA, TempG, EpsilonA, myParamFeu) + FluxConv
        EpsilonA = EmissiviteAcier(TempA, myParamFeu.TypeSurface)

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
        End Select

        Return myEpsilonA
    End Function

    Public Function FluxRadiatif(TempA As Decimal, TempG As Decimal, EpsilonA As Decimal, myParamFeu As cls_OptionsFeu) As Decimal
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

        EpsilonF = myParamFeu.EmissivityFire
        SigmaB = myParamFeu.BOLTZMANN
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

        aZero = 0.018 * (Tf * kUnitmm) + 0.7

        '--( Tableau F.4

        Select Case Time
            Case 30 : ReducKa = aZero * (1.12 - 84 / (Bc * kUnitmm) + Ha / Bc / 22) : KaMin = 0.5 : KaMax = 0.8
            Case 60 : ReducKa = aZero * (0.21 - 26 / (Bc * kUnitmm) + Ha / Bc / 24) : KaMin = 0.12 : KaMax = 0.4
            Case 90 : ReducKa = aZero * (0.12 - 17 / (Bc * kUnitmm) + Ha / Bc / 38) : KaMin = 0.06 : KaMax = 0.12
            Case 120 : ReducKa = aZero * (0.1 - 15 / (Bc * kUnitmm) + Ha / Bc / 40) : KaMin = 0.05 : KaMax = 0.1
            Case 180 : ReducKa = aZero * (0.03 - 3 / (Bc * kUnitmm) + Ha / Bc / 50) : KaMin = 0.03 : KaMax = 0.06
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
            HwlMin = HwlMinCas1(iStep)

        ElseIf IsGreaterOrEqual(RatioHsurB, 2) Then

            Coef_a1 = Me.TableauF3_A1_Cas2(iStep)
            Coef_a2 = Me.TableauF3_A2_Cas2(iStep)

            Hwl = Coef_a1 / Bc + Coef_a2 * Tw / (Bc * Ha)
            HwlMin = HwlMinCas2(iStep)

        Else

            Hwl = Me.TableauF3_Cas3(Time, Tw, Ha, Bc)
            HwlMin = HwlMinCas3(iStep)

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

        mykSh = 0.9 * numk / denomk

        Return mykSh


    End Function


#End Region

End Class
