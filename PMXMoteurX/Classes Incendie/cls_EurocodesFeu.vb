Public Class cls_EurocodesFeu

#Region " Déclarations "

    Const kUnitMM As Decimal = 1000

#End Region

#Region " Coefficients de réduction des propriétés mécaniques en fonction de la température "




#End Region


#Region " Courbe feu iso "



#End Region

#Region " Echauffement tabulé de la dalle "



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


#End Region


End Class
