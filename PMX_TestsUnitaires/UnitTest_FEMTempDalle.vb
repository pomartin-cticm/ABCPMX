Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PMXMoteur2


<TestClass()> Public Class UnitTest_FEMTempDalle

    '=====================================================================================================================================
    '   TU Pour les calculs numériques d'échauffement de la dalle
    '=====================================================================================================================================

    <TestMethod()> Public Sub TU_TempDalle_01()

        '--( Déclarations

        Dim myDalle As New cls_Dalle

        Dim FEMDalle As New cls_EchauffementDalleFEM    ' Moteur de calcul de l'échauffement de la dalle

        Const ElMax As Decimal = 0.01
        Dim nbMailles As Integer
        Dim tMailles() As Decimal = Nothing
        Dim tempMailles() As Decimal = Nothing
        Dim DeltaT As Decimal = 1
        Dim TimeT As Decimal = 0
        Dim lCont As Boolean = True
        Dim EN_Feu As New cls_EurocodesFeu
        Dim TempG As Decimal
        Const TempRef As Decimal = 20
        Const AlphaC As Decimal = 25
        Const AlphaCC As Decimal = 4
        Const EpsilonF As Decimal = 1
        Const TeneurU As Decimal = 0
        Const EpsilonC As Decimal = 0.7
        Dim TimeTarget As Decimal

        Dim lNormal As Boolean = True
        Const RhoC As Decimal = 2300
        Dim lRhoCVar, lANFrance, lGeneration1 As Boolean

        lRhoCVar = False
        lANFrance = False
        lGeneration1 = True

        Dim TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}

        Dim TempDalleStep(,) As Decimal

        Dim ValRef, ValCal As Decimal

        '--( Construction du TU

        myDalle.Ep_td = 0.18

        FEMDalle.PrepareMaillageDalleFEM(myDalle.Ep_td, ElMax, nbMailles, tMailles)

        ReDim tempMailles(nbMailles - 1)
        For i As Integer = 0 To nbMailles - 1
            tempMailles(i) = TempRef
        Next

        TimeTarget = TimeSteps.Last * kConvMinSec

        ReDim TempDalleStep(TimeSteps.GetUpperBound(0), 1)

        Dim iStep As Integer = 0

        '--( Calculs

        Do While lCont
            TimeT += DeltaT

            '# Température des gaz chauds

            TempG = EN_Feu.TemperatureGazISO(TimeT)

            '# Calcul des températures de la dalle

            FEMDalle.Calcul_thermique_Dalle_beton(myDalle.Ep_td, nbMailles, tMailles, DeltaT, tempMailles,
                                                  TempG, TempRef, AlphaC, AlphaCC, TeneurU, EpsilonF,
                                                  cls_OptionsFeu.BOLTZMANN, EpsilonC, lNormal,
                                                  RhoC, lRhoCVar, lANFrance, lGeneration1)


            lCont = IsSmaller(TimeT, TimeTarget, 10 ^ (-4))

            If IsEqual(TimeT, TimeSteps(iStep) * kConvMinSec, 10 ^ (-4)) Then
                TempDalleStep(iStep, 0) = tempMailles(0)
                TempDalleStep(iStep, 1) = tempMailles.Last
                iStep += 1
            End If

        Loop

        '--( Vérifications - Températures de la maille inférieure

        '# Température à R30
        ValRef = 601.8
        ValCal = TempDalleStep(0, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R60
        ValRef = 774.0
        ValCal = TempDalleStep(1, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R90
        ValRef = 863.2
        ValCal = TempDalleStep(2, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R120
        ValRef = 923.2
        ValCal = TempDalleStep(3, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R180
        ValRef = 1004.9
        ValCal = TempDalleStep(4, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

    End Sub


    <TestMethod()> Public Sub TU_TempDalle_08()

        '--( Déclarations

        Dim myDalle As New cls_Dalle

        Dim FEMDalle As New cls_EchauffementDalleFEM    ' Moteur de calcul de l'échauffement de la dalle

        Const ElMax As Decimal = 0.01
        Dim nbMailles As Integer
        Dim tMailles() As Decimal = Nothing
        Dim tempMailles() As Decimal = Nothing
        Dim DeltaT As Decimal = 1
        Dim TimeT As Decimal = 0
        Dim lCont As Boolean = True
        Dim EN_Feu As New cls_EurocodesFeu
        Dim TempG As Decimal
        Const TempRef As Decimal = 20
        Const AlphaC As Decimal = 25
        Const AlphaCC As Decimal = 4
        Const EpsilonF As Decimal = 1
        Const TeneurU As Decimal = 1.5
        Const EpsilonC As Decimal = 0.7
        Dim TimeTarget As Decimal

        Dim lNormal As Boolean = True
        Const RhoC As Decimal = 2300
        Dim lRhoCVar, lANFrance, lGeneration1 As Boolean

        lRhoCVar = True
        lANFrance = False
        lGeneration1 = False

        Dim TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}

        Dim TempDalleStep(,) As Decimal

        Dim ValRef, ValCal As Decimal

        '--( Construction du TU

        myDalle.Ep_td = 0.18

        FEMDalle.PrepareMaillageDalleFEM(myDalle.Ep_td, ElMax, nbMailles, tMailles)

        ReDim tempMailles(nbMailles - 1)
        For i As Integer = 0 To nbMailles - 1
            tempMailles(i) = TempRef
        Next

        TimeTarget = TimeSteps.Last * kConvMinSec

        ReDim TempDalleStep(TimeSteps.GetUpperBound(0), 1)

        Dim iStep As Integer = 0

        '--( Calculs

        Do While lCont
            TimeT += DeltaT

            '# Température des gaz chauds

            TempG = EN_Feu.TemperatureGazISO(TimeT)

            '# Calcul des températures de la dalle

            FEMDalle.Calcul_thermique_Dalle_beton(myDalle.Ep_td, nbMailles, tMailles, DeltaT, tempMailles,
                                                  TempG, TempRef, AlphaC, AlphaCC, TeneurU, EpsilonF,
                                                  cls_OptionsFeu.BOLTZMANN, EpsilonC, lNormal,
                                                  RhoC, lRhoCVar, lANFrance, lGeneration1)


            lCont = IsSmaller(TimeT, TimeTarget, 10 ^ (-4))

            If IsEqual(TimeT, TimeSteps(iStep) * kConvMinSec, 10 ^ (-4)) Then
                TempDalleStep(iStep, 0) = tempMailles(0)
                TempDalleStep(iStep, 1) = tempMailles.Last
                iStep += 1
            End If

        Loop

        '--( Vérifications - Températures de la maille inférieure

        '# Température à R30
        ValRef = 598.7
        ValCal = TempDalleStep(0, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R60
        ValRef = 773.4
        ValCal = TempDalleStep(1, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R90
        ValRef = 863.7
        ValCal = TempDalleStep(2, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R120
        ValRef = 924.2
        ValCal = TempDalleStep(3, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

        '# Température à R180
        ValRef = 1006.0
        ValCal = TempDalleStep(4, 0)
        Assert.IsTrue(IsEqual(ValCal, ValRef))

    End Sub

End Class