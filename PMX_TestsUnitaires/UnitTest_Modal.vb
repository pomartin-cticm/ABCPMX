Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting


<TestClass()> Public Class UnitTest_Modal
    '========================================================================================================================================
    '   CLASSE POUR LES TESTS UNITAITRES DES PROJETS CTICM_RDM
    '========================================================================================================================================

    <TestMethod()> Public Sub TestMethodAnalyseModale_Uniforme()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST03 (voir VALIDATION)
        ' Poutre sur appuis simple - Charge uniformément répartie
        '
        '=======================================

        Const RHOACIER As Decimal = 7850

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim MyAire As Decimal = 53.81 / 100 ^ 2
        Dim MyInertie As Decimal = 8356.1 / 100 ^ 4

        Dim L As Decimal = 10       ' m
        With MyDonnees
            .EYOUNG = 2100000 * 10 ^ 6
            .PESANTEUR = 9.81

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
                .Aire(i) = MyAire
                .InertieY(i) = MyInertie
            Next

            'Appuis
            .NbAppuis = 2
            .iNodeAppui = {0, 50}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0

            .NbMoments = 0

            .NbForcesRep = 1
            ReDim .xForceRep(0, 1)
            ReDim .ForceRep(0, 1)
            .xForceRep(0, 0) = 0
            .xForceRep(0, 1) = L

            .ForceRep(0, 0) = MyAire * .PESANTEUR * RHOACIER
            .ForceRep(0, 1) = MyAire * .PESANTEUR * RHOACIER

        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyDonnees
            ReDim MyOutput_MOD_REF.FreqProp(0)
            Dim Masse As Decimal = MyAire * L * RHOACIER
            MyOutput_MOD_REF.FreqProp(0) = 1 / (2 / Math.PI * Math.Sqrt(Masse * L ^ 3 / (.EYOUNG * MyInertie)))

            Assert.IsTrue(IsEqual(MyOutput_MOD_REF.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST03()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST03 (voir VALIDATION)
        ' Poutre sur appuis simple - Charge ponctuelle au milieu d'un element fini
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 1000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000
            .PESANTEUR = 9.81 * 100 'cm/s2

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
            .iNodeAppui = {0, 50}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 1
            ReDim .xForcePon(0)
            ReDim .ForcePon(0)
            .xForcePon(0) = 370         'cm
            .ForcePon(0) = 2000         'daN            

            .NbMoments = 0

            .NbForcesRep = 0
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyDonnees
            ReDim MyOutput_MOD_REF.FreqProp(0)
            MyOutput_MOD_REF.FreqProp(0) = 1 / ((.ForcePon(0) / .PESANTEUR * .xForcePon(0) ^ 2 * (L - .xForcePon(0)) ^ 2 / 3 / .EYOUNG / .InertieY(0) / L) ^ 0.5 * 2 * Math.PI)

            Assert.IsTrue(IsEqual(MyOutput_MOD_REF.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST03USI()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST03 (voir VALIDATION)
        ' Poutre sur appuis simple - Charge ponctuelle au milieu d'un element fini
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 10     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000 * 10 ^ 6
            .PESANTEUR = 9.81 'm/s2

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
                .Aire(i) = 53.81 / 100 ^ 2      'cm2
                .InertieY(i) = 8356.1 / 100 ^ 4  'cm4
            Next

            'Appuis
            .NbAppuis = 2
            .iNodeAppui = {0, 50}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 1
            ReDim .xForcePon(0)
            ReDim .ForcePon(0)
            .xForcePon(0) = 370 / 100       'cm
            .ForcePon(0) = 2000 * 10        'daN            

            .NbMoments = 0

            .NbForcesRep = 0
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyDonnees
            ReDim MyOutput_MOD_REF.FreqProp(0)
            MyOutput_MOD_REF.FreqProp(0) = 1 / ((.ForcePon(0) / .PESANTEUR * .xForcePon(0) ^ 2 * (L - .xForcePon(0)) ^ 2 / 3 / .EYOUNG / .InertieY(0) / L) ^ 0.5 * 2 * Math.PI)

            Assert.IsTrue(IsEqual(MyOutput_MOD_REF.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST04()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST04 (voir VALIDATION)
        ' Poutre sur appuis simple - Charge ponctuelle au noeud
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 1000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000
            .PESANTEUR = 9.81 * 100 'cm/s2

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
            .iNodeAppui = {0, 50}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 1
            ReDim .xForcePon(0)
            ReDim .ForcePon(0)
            .xForcePon(0) = 500         'cm
            .ForcePon(0) = 2000         'daN            

            .NbMoments = 0

            .NbForcesRep = 0
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyDonnees
            ReDim MyOutput_MOD_REF.FreqProp(0)
            MyOutput_MOD_REF.FreqProp(0) = 1 / ((.ForcePon(0) / .PESANTEUR * .xForcePon(0) ^ 2 * (L - .xForcePon(0)) ^ 2 / 3 / .EYOUNG / .InertieY(0) / L) ^ 0.5 * 2 * Math.PI)
            Assert.IsTrue(IsEqual(MyOutput_MOD_REF.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST05()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST05 (voir VALIDATION)
        ' Poutre sur appuis - Charge triangulaire
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 1000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000
            .PESANTEUR = 9.81 * 100 'cm/s2

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
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbMoments = 0

            .NbForcesRep = 1
            ReDim .xForceRep(0, 1)
            ReDim .ForceRep(0, 1)
            .xForceRep(0, 0) = 0
            .xForceRep(0, 1) = L
            .ForceRep(0, 0) = 0.4143827       'daN/cm
            .ForceRep(0, 1) = 10.4143827       'daN/cm
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyOutput_MOD_REF
            ReDim .FreqProp(0)
            .FreqProp(0) = 2.790655     'SCIA
            Assert.IsTrue(IsEqual(.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST06()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST06
        ' Poutre sur appuis avec console
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 1000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000
            .PESANTEUR = 9.81 * 100 'cm/s2

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
            .NbAppuis = 2
            .iNodeAppui = {20, 80}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbMoments = 0

            .NbForcesRep = 1
            ReDim .xForceRep(0, 1)
            ReDim .ForceRep(0, 1)
            .xForceRep(0, 0) = 0
            .xForceRep(0, 1) = L
            .ForceRep(0, 0) = 20.4143827       'daN/cm
            .ForceRep(0, 1) = 20.4143827       'daN/cm
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyOutput_MOD_REF
            ReDim .FreqProp(0)
            .FreqProp(0) = 3.204432     'SCIA
            Assert.IsTrue(IsEqual(.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST07()

        '=======================================
        '
        ' 10/10/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST07
        ' Poutre sur 3 appuis avec console
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 1000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000
            .PESANTEUR = 9.81 * 100 'cm/s2

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
            .NbAppuis = 3
            .iNodeAppui = {0, 50, 90}
            .lAppuiArticule = {False, False, False}

            .NbForcesPon = 0
            .NbMoments = 0

            .NbForcesRep = 1
            ReDim .xForceRep(0, 1)
            ReDim .ForceRep(0, 1)
            .xForceRep(0, 0) = 0
            .xForceRep(0, 1) = L
            .ForceRep(0, 0) = 100.4143827       'daN/cm
            .ForceRep(0, 1) = 100.4143827       'daN/cm
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyOutput_MOD_REF
            ReDim .FreqProp(0)
            .FreqProp(0) = 2.986559     'SCIA
            Assert.IsTrue(IsEqual(.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub

    <TestMethod()> Public Sub TestMethodAnalyseModale_TEST08()

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

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing

        Dim L As Decimal = 2000     'Longueur totale de la barre en cm
        With MyDonnees
            .EYOUNG = 2100000
            .PESANTEUR = 9.81 * 100 'cm/s2

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
            .ForceRep(0, 0) = 200.4143827       'daN/cm
            .ForceRep(0, 1) = 200.4143827       'daN/cm
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLLMOD As New CTICM_MODAL.CALCUL_MODAL
        Dim MyOutput_MOD As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        Dim CodeError_MOD As Integer
        Dim TextError_MOD As String = String.Empty

        Call MyDLLMOD.CALCULER(MyDonnees, 1, MyOutput_MOD, CodeError_MOD, TextError_MOD)

        '=== VALEURS DE REFERENCE ===
        Dim MyOutput_MOD_REF As CTICM_MODAL.DATA_MODAL.Struc_Output = Nothing
        With MyOutput_MOD_REF
            ReDim .FreqProp(0)
            .FreqProp(0) = 1.841145     'SCIA
            Assert.IsTrue(IsEqual(.FreqProp(0), MyOutput_MOD.FreqProp(0)))

        End With
    End Sub
End Class