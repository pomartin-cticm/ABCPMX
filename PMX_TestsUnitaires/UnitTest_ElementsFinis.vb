Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports CTICM_RDM


<TestClass()> Public Class UnitTest_ElementsFinis

    <TestMethod()> Public Sub TestMethodElementsFinis_TEST01()

        '=======================================
        '
        ' 05/09/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST01 (voir VALIDATION)
        ' Poutre sur 2 appuis - Moment ponctuel au milieu d'une barre EF
        '
        '=======================================

        Dim MyDonnees As New CTICM_DATA_DLLS.DATA_DLLS

        Dim L As Decimal = 1000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000

            .NbNodes = 51

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next

            'Elements
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 53.81        'cm2
                .InertieY(i) = 8356.1   'cm4
            Next

            'Appuis
            .NbAppuis = 2
            .iNodeAppui = {0, .NbNodes - 1}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 0

            'Moment ponctuel au milieu d'une barre EF
            .NbMoments = 1
            ReDim .Moment(.NbMoments - 1)
            ReDim .xMoment(.NbMoments - 1)
            .Moment(0) = 100000     'daN.cm
            .xMoment(0) = 110       'cm

        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_RDM_REF As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        With MyOutput_RDM_REF
            .RZ = {100, -100}       'déjà en daN
            For i = 0 To MyDonnees.NbAppuis - 1
                Assert.IsTrue(IsEqual(.RZ(i), MyOutput_RDM.RZ(i)))
            Next

            '.UZ = {0, 0.026, 0.052, 0.079, 0.105, 0.132, 0.158, 0.183, 0.206, 0.227, 0.246, 0.263, 0.279, 0.292, 0.305, 0.315, 0.324, 0.331, 0.337, 0.341, 0.344, 0.346, 0.346, 0.345, 0.342, 0.339, 0.334, 0.329, 0.322, 0.314, 0.305, 0.296, 0.285, 0.274, 0.262, 0.249, 0.235, 0.221, 0.207, 0.191, 0.175, 0.159, 0.143, 0.126, 0.108, 0.091, 0.073, 0.055, 0.037, 0.018, 0}
            '.ROTY = {0.001307, 0.001308, 0.001312, 0.001317, 0.001325, 0.001336, 0.001291, 0.001192, 0.001095, 0.001001, 0.000908, 0.000818, 0.00073, 0.000645, 0.000562, 0.000481, 0.000402, 0.000326, 0.000252, 0.00018, 0.00011, 0.000043, -0.000022, -0.000084, -0.000145, -0.000203, -0.000259, -0.000312, -0.000364, -0.000413, -0.000459, -0.000504, -0.000546, -0.000586, -0.000624, -0.000659, -0.000692, -0.000723, -0.000751, -0.000777, -0.000801, -0.000823, -0.000842, -0.000859, -0.000874, -0.000887, -0.000897, -0.000905, -0.000911, -0.000914, 0}
            'For i = 0 To MyDonnees.NbNodes - 1
            '    Assert.IsTrue(IsEqual(.UZ(i), MyOutput_RDM.UZ(i)))
            '    Assert.IsTrue(IsEqual(.ROTY(i), MyOutput_RDM.ROTY(i)))
            'Next

            'Attention aux dimensions de VZ et MYY : inverse par rapport à celles de la DLL pour faciliter l'introduction des valeurs de réference
            'Attention aussi à la conversion des unités : VZ [kN], MYY [kN.m]
            .VZ = {{0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
                    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0}}
            .MYY = {{0, 0.2, 0.4, 0.6, 0.8, 1, -8.8, -8.6, -8.4, -8.2, -8, -7.8, -7.6, -7.4, -7.2, -7, -6.8, -6.6, -6.4, -6.2, -6, -5.8, -5.6, -5.4, -5.2, -5, -4.8, -4.6, -4.4, -4.2, -4, -3.8, -3.6, -3.4, -3.2, -3, -2.8, -2.6, -2.4, -2.2, -2, -1.8, -1.6, -1.4, -1.2, -1, -0.8, -0.6, -0.4, -0.2, 0},
                    {0, 0.2, 0.4, 0.6, 0.8, 1, -8.8, -8.6, -8.4, -8.2, -8, -7.8, -7.6, -7.4, -7.2, -7, -6.8, -6.6, -6.4, -6.2, -6, -5.8, -5.6, -5.4, -5.2, -5, -4.8, -4.6, -4.4, -4.2, -4, -3.8, -3.6, -3.4, -3.2, -3, -2.8, -2.6, -2.4, -2.2, -2, -1.8, -1.6, -1.4, -1.2, -1, -0.8, -0.6, -0.4, -0.2, 0}}

            For i = 0 To MyDonnees.NbNodes - 1
                For j = 0 To 1
                    Assert.IsTrue(IsEqual(.VZ(j, i) * 100, MyOutput_RDM.VZ(i, j)))          'en daN
                    Assert.IsTrue(IsEqual(.MYY(j, i) * 10000, MyOutput_RDM.MYY(i, j)))      'en daN.m
                Next
            Next

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodElementsFinis_TEST08()

        '=======================================
        '
        ' 05/09/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST08 (voir VALIDATION)
        ' Poutre sur 5 appuis avec articulation - Charge répartie
        '
        '=======================================

        Dim MyDonnees As New CTICM_DATA_DLLS.DATA_DLLS

        Dim L As Decimal = 2000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next

            'Elements
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 53.81        'cm2
                .InertieY(i) = 8356.1   'cm4
            Next

            'Appuis
            .NbAppuis = 5
            .iNodeAppui = {0, 25, 50, 75, 100}
            .lAppuiArticule = {False, False, True, False, False}        'Articulation à l'appui 3            

            .NbForcesPon = 0
            .NbMoments = 0

            .NbForcesRep = 1
            ReDim .xForceRep(0, 1)
            ReDim .ForceRep(0, 1)
            .xForceRep(0, 0) = 0
            .xForceRep(0, 1) = L
            .ForceRep(0, 0) = 200       'daN/cm
            .ForceRep(0, 1) = 200       'daN/cm
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_RDM_REF As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        With MyOutput_RDM_REF
            .RZ = {375, 1250, 750, 1250, 375}       'en kN
            For i = 0 To MyDonnees.NbAppuis - 1
                Assert.IsTrue(IsEqual(.RZ(i) * 100, MyOutput_RDM.RZ(i)))        'en daN
            Next

            'Attention aux dimensions de VZ et MYY : inverse par rapport à celles de la DLL pour faciliter l'introduction des valeurs de réference
            'Attention aussi à la conversion des unités : VZ [kN], MYY [kN.m]
            .VZ = {{0, -335, -295, -255, -215, -175, -135, -95, -55, -15, 25, 65, 105, 145, 185, 225, 265, 305, 345, 385, 425, 465, 505, 545, 585, 625, -585, -545, -505, -465, -425, -385, -345, -305, -265, -225, -185, -145, -105, -65, -25, 15, 55, 95, 135, 175, 215, 255, 295, 335, 375, -335, -295, -255, -215, -175, -135, -95, -55, -15, 25, 65, 105, 145, 185, 225, 265, 305, 345, 385, 425, 465, 505, 545, 585, 625, -585, -545, -505, -465, -425, -385, -345, -305, -265, -225, -185, -145, -105, -65, -25, 15, 55, 95, 135, 175, 215, 255, 295, 335, 375},
                    {-375, -335, -295, -255, -215, -175, -135, -95, -55, -15, 25, 65, 105, 145, 185, 225, 265, 305, 345, 385, 425, 465, 505, 545, 585, -625, -585, -545, -505, -465, -425, -385, -345, -305, -265, -225, -185, -145, -105, -65, -25, 15, 55, 95, 135, 175, 215, 255, 295, 335, -375, -335, -295, -255, -215, -175, -135, -95, -55, -15, 25, 65, 105, 145, 185, 225, 265, 305, 345, 385, 425, 465, 505, 545, 585, -625, -585, -545, -505, -465, -425, -385, -345, -305, -265, -225, -185, -145, -105, -65, -25, 15, 55, 95, 135, 175, 215, 255, 295, 335, 0}}
            .MYY = {{0, 71, 134, 189, 236, 275, 306, 329, 344, 351, 350, 341, 324, 299, 266, 225, 176, 119, 54, -19, -100, -189, -286, -391, -504, -625, -504, -391, -286, -189, -100, -19, 54, 119, 176, 225, 266, 299, 324, 341, 350, 351, 344, 329, 306, 275, 236, 189, 134, 71, 0, 71, 134, 189, 236, 275, 306, 329, 344, 351, 350, 341, 324, 299, 266, 225, 176, 119, 54, -19, -100, -189, -286, -391, -504, -625, -504, -391, -286, -189, -100, -19, 54, 119, 176, 225, 266, 299, 324, 341, 350, 351, 344, 329, 306, 275, 236, 189, 134, 71, 0},
                    {0, 71, 134, 189, 236, 275, 306, 329, 344, 351, 350, 341, 324, 299, 266, 225, 176, 119, 54, -19, -100, -189, -286, -391, -504, -625, -504, -391, -286, -189, -100, -19, 54, 119, 176, 225, 266, 299, 324, 341, 350, 351, 344, 329, 306, 275, 236, 189, 134, 71, 0, 71, 134, 189, 236, 275, 306, 329, 344, 351, 350, 341, 324, 299, 266, 225, 176, 119, 54, -19, -100, -189, -286, -391, -504, -625, -504, -391, -286, -189, -100, -19, 54, 119, 176, 225, 266, 299, 324, 341, 350, 351, 344, 329, 306, 275, 236, 189, 134, 71, 0}}

            For i = 0 To MyDonnees.NbNodes - 1
                For j = 0 To 1
                    Assert.IsTrue(IsEqual(.VZ(j, i) * 100, MyOutput_RDM.VZ(i, j)))          'en daN
                    Assert.IsTrue(IsEqual(.MYY(j, i) * 10000, MyOutput_RDM.MYY(i, j)))      'en daN.m
                Next
            Next

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodElementsFinis_TESTxx()
        '=======================================
        '
        ' 05/09/2023 : POM
        '
        '=======================================
        '
        ' Poutre 2 appuis - Effort vertical à mi travée
        '
        '=======================================

        Dim MyDonnees As New CTICM_DATA_DLLS.DATA_DLLS

        Dim L As Decimal = 10     'Longueur totale de la barre en m
        Dim Force As Decimal = 1000

        With MyDonnees
            .EYOUNG = 2100000 * 10 ^ 6

            .NbNodes = 21

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next

            'Elements
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)

            '# IPE 300
            For i = 0 To .NbNodes - 2
                .Aire(i) = 69 * 10 ^ -4
                .InertieY(i) = 9800 * 10 ^ -8
            Next

            'Appuis
            .NbAppuis = 2
            .iNodeAppui = {0, .NbNodes - 1}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 1
            .NbForcesRep = 0
            .NbMoments = 0

            ReDim .ForcePon(.NbForcesPon - 1)
            ReDim .xForcePon(.NbForcesPon - 1)
            .ForcePon(0) = Force
            .xForcePon(0) = L / 2

        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        '# Controle réaction aux appuis

        Assert.IsTrue(IsEqual(Force / 2, MyOutput_RDM.RZ(0)))
        Assert.IsTrue(IsEqual(Force / 2, MyOutput_RDM.RZ(1)))

        '# Moment à mi-travée

        Assert.IsTrue(IsEqual(Force * L / 4, MyOutput_RDM.MYY((MyDonnees.NbNodes - 1) / 2, 1)))

        '# Effort tranchant sur appui

        Assert.IsTrue(IsEqual(-Force / 2, MyOutput_RDM.VZ(0, 1)))

        '# Flèche à mi-travée

        Assert.IsTrue(IsEqual(Force * L ^ 2 / (48 * MyDonnees.EYOUNG * MyDonnees.InertieY(0)), MyOutput_RDM.UZ(11)))

    End Sub




End Class