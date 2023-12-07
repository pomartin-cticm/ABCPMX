Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest_LTBeamN
    <TestMethod()> Public Sub TestMethodLTB_TEST50()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST50 (voir VALIDATION)
        ' I doublement symétrique, poutre continue 2 travées, 2 charges ponctuelles
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 19.5     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next

            'Maintien ponctuel

            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 87 * 0.0001          'm2
                .InertieY(i) = 13835 * 0.00000001   'm4
                .RayGirPol(i) = 13.49 * 0.01   'm
                .InertieT(i) = 52.65 * 0.00000001   'm4
                .InertieZ(i) = 2002.3 * 0.00000001   'm4
                .InertieW(i) = 406582 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                
            Next

            'Appui
            .NbAppuis = 3
            .iNodeAppui = {0, 50, 100}
            .lAppuiArticule = {False, False, False}

            .NbForcesPon = 2
            .NbForcesRep = 0
            .NbMoments = 0

            ReDim .ForcePon(.NbForcesPon - 1)
            ReDim .xForcePon(.NbForcesPon - 1)
            ReDim .zForcePonC(.NbForcesPon - 1)
            .ForcePon(0) = 10 * 1000        'N
            .xForcePon(0) = 4.875
            .zForcePonC(0) = 0.0
            .ForcePon(1) = 10 * 1000        'N
            .xForcePon(1) = 14.75
            .zForcePonC(1) = 0.0
        End With

        '# Paramètres LTB

        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 50
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1


        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 14.852
        Dim DeltaV As Double = (MyOutput_LTB.CoefCr - MuiCrRef) / MuiCrRef

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
        'Assert.IsTrue(IsEqual(DeltaV, 0, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST51()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST51 (voir VALIDATION)
        ' I doublement symétrique, poutre continue 2 travées, 2 charges uniformes
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 19.5     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 87 * 0.0001          'm2
                .InertieY(i) = 13835 * 0.00000001   'm4
                .RayGirPol(i) = 13.49 * 0.01   'm
                .InertieT(i) = 52.65 * 0.00000001   'm4
                .InertieZ(i) = 2002.3 * 0.00000001   'm4
                .InertieW(i) = 406582 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                
            Next

            'Appui
            .NbAppuis = 3
            .iNodeAppui = {0, 50, 100}
            .lAppuiArticule = {False, False, False}

            .NbForcesPon = 0
            .NbForcesRep = 2
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 1000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 1000        'N/m
            .xForceRep(0, 1) = L / 4

            .ForceRep(1, 0) = 3000.0        'N/m
            .xForceRep(1, 0) = L / 4
            .zForceRepC(1) = 0.0
            .ForceRep(1, 1) = 3000        'N/m
            .xForceRep(1, 1) = L
        End With

        '# Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 50
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 9.4102

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST55()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST55 (voir VALIDATION)        
        ' I mono symétrique, poutre continue 2 travées, charge ponctuelle
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 19.5     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 75.3 * 0.0001          'm2
                .InertieY(i) = 11170 * 0.00000001   'm4
                .RayGirPol(i) = 13.76 * 0.01   'm
                .InertieT(i) = 39.42 * 0.00000001   'm4
                .InertieZ(i) = 1339.8 * 0.00000001   'm4
                .InertieW(i) = 207123 * 0.000000000001   'm6
                .CoefBetaZ(i) = 6.392 * 0.01      'm                
                .PositionCG(i) = -4.828 * 0.01      'm                
            Next

            'Appui
            .NbAppuis = 3
            .iNodeAppui = {0, 50, 100}
            .lAppuiArticule = {False, False, False}

            .NbForcesPon = 1
            .NbForcesRep = 0
            .NbMoments = 0

            ReDim .ForcePon(.NbForcesPon - 1)
            ReDim .xForcePon(.NbForcesPon - 1)
            ReDim .zForcePonC(.NbForcesPon - 1)
            .ForcePon(0) = 10 * 1000        'N
            .xForcePon(0) = L / 4
            .zForcePonC(0) = 0.0
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 50
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 8.0646

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST56()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST56 (voir VALIDATION)
        ' I mono symétrique, poutre continue 2 travées, charge uniforme
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 19.5     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 75.3 * 0.0001          'm2
                .InertieY(i) = 11170 * 0.00000001   'm4
                .RayGirPol(i) = 13.76 * 0.01   'm
                .InertieT(i) = 39.42 * 0.00000001   'm4
                .InertieZ(i) = 1339.8 * 0.00000001   'm4
                .InertieW(i) = 207123 * 0.000000000001   'm6
                .CoefBetaZ(i) = 6.392 * 0.01      'm                
                .PositionCG(i) = -4.828 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 3
            .iNodeAppui = {0, 50, 100}
            .lAppuiArticule = {False, False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L / 2
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 50
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 4.7196

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST80()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST80 (voir VALIDATION)
        ' I doublement symétrique, poutre simple, charge uniforme, 1 maintien (v,theta) ponctuel au milieu
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 164.15 * 0.0001          'm2
                .InertieY(i) = 71846 * 0.00000001   'm4
                .RayGirPol(i) = 21.91 * 0.01   'm
                .InertieT(i) = 154.57 * 0.00000001   'm4
                .InertieZ(i) = 6959 * 0.00000001   'm4
                .InertieW(i) = 4025000.0 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        'Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 50
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 6.9718

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST84_1()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST84-1 (voir VALIDATION)
        ' I doublement symétrique, poutre simple, charge uniforme, 1 maintien (v) ponctuel a L/4 & semelle sup
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 164.15 * 0.0001          'm2
                .InertieY(i) = 71846 * 0.00000001   'm4
                .RayGirPol(i) = 21.91 * 0.01   'm
                .InertieT(i) = 154.57 * 0.00000001   'm4
                .InertieZ(i) = 6959 * 0.00000001   'm4
                .InertieW(i) = 4025000.0 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 25
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.zMaintienPonC(1) = 0.2405

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 5.1207

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST84_2()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST84-2 (voir VALIDATION)
        ' I doublement symétrique, poutre simple, charge uniforme, 1 maintien (v) ponctuel a L/4
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 164.15 * 0.0001          'm2
                .InertieY(i) = 71846 * 0.00000001   'm4
                .RayGirPol(i) = 21.91 * 0.01   'm
                .InertieT(i) = 154.57 * 0.00000001   'm4
                .InertieZ(i) = 6959 * 0.00000001   'm4
                .InertieW(i) = 4025000.0 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        '* Maintien ponctuel
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 25
        ParamLTB.MaintienPonV(1) = -1

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 4.7467

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST84_3()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST84-2 (voir VALIDATION)
        ' I doublement symétrique, poutre simple, charge uniforme, 1 maintien (v) ponctuel a L/4 & semelle inf
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 164.15 * 0.0001          'm2
                .InertieY(i) = 71846 * 0.00000001   'm4
                .RayGirPol(i) = 21.91 * 0.01   'm
                .InertieT(i) = 154.57 * 0.00000001   'm4
                .InertieZ(i) = 6959 * 0.00000001   'm4
                .InertieW(i) = 4025000.0 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 3
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 25
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.zMaintienPonC(1) = -0.2405

        ParamLTB.iNodeMaintienPon(2) = 100
        ParamLTB.MaintienPonV(2) = -1
        ParamLTB.MaintienPonTheta(2) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 2.9876

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST90()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST09 (voir VALIDATION)
        ' I doublement symétrique, poutre simple, charge uniforme, 1 maintien (v) continu au centre C
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Maintien continu
            .NbMaintiensCon = 1
            ReDim .iNodeMaintienCon(.NbMaintiensCon - 1, 1)
            ReDim .MaintienConV(.NbMaintiensCon - 1)
            ReDim .MaintienConTheta(.NbMaintiensCon - 1)
            ReDim .MaintienConVP(.NbMaintiensCon - 1)
            ReDim .zMaintienConC(.NbMaintiensCon - 1)

            .iNodeMaintienCon(0, 0) = 0
            .iNodeMaintienCon(0, 1) = 100
            .MaintienConV(0) = 2.0 * 1000.0        'N/m/m

            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 164.15 * 0.0001          'm2
                .InertieY(i) = 71846 * 0.00000001   'm4
                .RayGirPol(i) = 21.91 * 0.01   'm
                .InertieT(i) = 154.57 * 0.00000001   'm4
                .InertieZ(i) = 6959 * 0.00000001   'm4
                .InertieW(i) = 4025000.0 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0            'Centre C
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 2
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB .MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 100
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 2.5775

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST91()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST91 (voir VALIDATION)
        ' I doublement symétrique, poutre simple, charge uniforme, 1 maintien (v) continu sur la semelle sup
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next

            'Maintien continu
            .NbMaintiensCon = 1
            ReDim .iNodeMaintienCon(.NbMaintiensCon - 1, 1)
            ReDim .MaintienConV(.NbMaintiensCon - 1)
            ReDim .MaintienConTheta(.NbMaintiensCon - 1)
            ReDim .MaintienConVP(.NbMaintiensCon - 1)
            ReDim .zMaintienConC(.NbMaintiensCon - 1)

            .iNodeMaintienCon(0, 0) = 0
            .iNodeMaintienCon(0, 1) = 100
            .MaintienConV(0) = 0.75 * 1000.0        'N.m/m
            .zMaintienConC(0) = 0.2405              'Semelle sup

            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 164.15 * 0.0001          'm2
                .InertieY(i) = 71846 * 0.00000001   'm4
                .RayGirPol(i) = 21.91 * 0.01   'm
                .InertieT(i) = 154.57 * 0.00000001   'm4
                .InertieZ(i) = 6959 * 0.00000001   'm4
                .InertieW(i) = 4025000.0 * 0.000000000001   'm6
                .CoefBetaZ(i) = 0.0 * 0.01      'm                
                .PositionCG(i) = 0.0 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 2
        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 100
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 2.5482

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

    <TestMethod()> Public Sub TestMethodLTB_TEST92()

        '=======================================
        '
        ' 08/11/2023 : TMN
        '
        '=======================================
        '
        ' Cas de test de référence TEST92 (voir VALIDATION)
        ' I mono symétrique, poutre simple, charge uniforme, 1 maintien (v) continu sur la semelle sup
        '
        '=======================================

        Dim MyDonnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
        Dim ParamLTB As CTICM_LTB.DATA_LTB.struc_DonneesLTB = Nothing

        Dim L As Decimal = 18     'Longueur totale de la barre
        With MyDonnees
            .EYOUNG = 210000 * 1000000.0
            .GSHEAR = .EYOUNG / 2 / (1 + 0.3)

            .NbNodes = 101

            'Position des noeuds EF
            ReDim .xNode(.NbNodes - 1)
            For i = 0 To .NbNodes - 1
                .xNode(i) = i * L / (.NbNodes - 1)
            Next



            'Maintien continu
            .NbMaintiensCon = 1
            ReDim .iNodeMaintienCon(.NbMaintiensCon - 1, 1)
            ReDim .MaintienConV(.NbMaintiensCon - 1)
            ReDim .MaintienConTheta(.NbMaintiensCon - 1)
            ReDim .MaintienConVP(.NbMaintiensCon - 1)
            ReDim .zMaintienConC(.NbMaintiensCon - 1)

            .iNodeMaintienCon(0, 0) = 0
            .iNodeMaintienCon(0, 1) = 100
            .MaintienConV(0) = 0.75 * 1000.0        'N.m/m
            .zMaintienConC(0) = 9.352649 / 100             'Semelle sup

            'Elements            
            ReDim .Aire(.NbNodes - 2)
            ReDim .InertieY(.NbNodes - 2)
            ReDim .RayGirPol(.NbNodes - 2)
            ReDim .InertieT(.NbNodes - 2)
            ReDim .InertieZ(.NbNodes - 2)
            ReDim .InertieW(.NbNodes - 2)
            ReDim .PositionCG(.NbNodes - 2)
            ReDim .CoefBetaZ(.NbNodes - 2)
            ReDim .MomentFle(.NbNodes - 2, 1)
            For i = 0 To .NbNodes - 2
                .Aire(i) = 138.45 * 0.0001          'm2
                .InertieY(i) = 54499 * 0.00000001   'm4
                .RayGirPol(i) = 23.4506226 * 0.01   'm
                .InertieT(i) = 112.482635 * 0.00000001   'm4
                .InertieZ(i) = 4212.318 * 0.00000001   'm4
                .InertieW(i) = 1405820.25 * 0.000000000001   'm6
                .CoefBetaZ(i) = -14.33183 * 0.01      'm                
                .PositionCG(i) = 11.219327 * 0.01      'm                    
            Next

            'Appui
            .NbAppuis = 2
            .iNodeAppui = {0, 100}
            .lAppuiArticule = {False, False}

            .NbForcesPon = 0
            .NbForcesRep = 1
            .NbMoments = 0

            ReDim .ForceRep(.NbForcesRep - 1, 1)
            ReDim .xForceRep(.NbForcesRep - 1, 1)
            ReDim .zForceRepC(.NbForcesRep - 1)
            .ForceRep(0, 0) = 3000.0        'N/m
            .xForceRep(0, 0) = 0.0
            .zForceRepC(0) = 0.0
            .ForceRep(0, 1) = 3000        'N/m
            .xForceRep(0, 1) = L
        End With

        '* Maintiens ponctuels
        ParamLTB.NbMaintiensPon = 2

        ReDim ParamLTB.iNodeMaintienPon(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonV(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonTheta(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonVP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.MaintienPonThetaP(ParamLTB.NbMaintiensPon - 1)
        ReDim ParamLTB.zMaintienPonC(ParamLTB.NbMaintiensPon - 1)

        ParamLTB.iNodeMaintienPon(0) = 0
        ParamLTB.MaintienPonV(0) = -1
        ParamLTB.MaintienPonTheta(0) = -1

        ParamLTB.iNodeMaintienPon(1) = 100
        ParamLTB.MaintienPonV(1) = -1
        ParamLTB.MaintienPonTheta(1) = -1

        '=== LANCER LE CALCUL RDM POUR AVOIR LE DIAGRAMME DE MOMENT ===
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty

        Call MyDLLRDM.CALCULER(MyDonnees, MyOutput_RDM, CodeError_RDM, TextError_RDM)

        'Moments fléchissants
        With MyDonnees
            For i = 0 To .NbNodes - 2
                .MomentFle(i, 0) = MyOutput_RDM.MYY(i, 1)
                .MomentFle(i, 1) = MyOutput_RDM.MYY(i + 1, 0)
            Next
        End With

        '=== LANCER LE CALCUL ===
        Dim MyDLL_LTB As New CTICM_LTB.CALCUL_LTB
        Dim MyOutput_LTB As CTICM_LTB.DATA_LTB.Struc_Output = Nothing
        Dim CodeError_LTB As Integer
        Dim TextError_LTB As String = String.Empty

        Call MyDLL_LTB.CALCULER(MyDonnees, ParamLTB, MyOutput_LTB, CodeError_LTB, TextError_LTB)

        '=== VALEURS DE REFERENCE ANSYS ===
        Dim MuiCrRef As Double = 1.8389

        Assert.IsTrue(IsEqual(MuiCrRef, MyOutput_LTB.CoefCr, 0.01))     '<1%
    End Sub

End Class