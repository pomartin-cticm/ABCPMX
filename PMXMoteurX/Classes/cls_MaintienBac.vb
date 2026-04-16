Imports System.Security.Cryptography
Imports Microsoft.VisualBasic.Logging

Public Class cls_MaintienBac

    '##########################################################################################################
    '#  CLASSE POUR GERER LE MAINTIEN DES POUTRES PAR LE BAC (PERPENDICULAIRE)
    '##########################################################################################################

#Region " Déclarations "

    Public Enum Enu_Transition
        Emboitement
        Aboutage
        Adistance
    End Enum

    Public Enum Enu_FixationNervures
        Toutes
        UneSurDeux
    End Enum

    Public Enum Enu_FixNervuresType
        VisNormale
        VisNeoprene
        Pistolet
    End Enum

    Public Enum Enu_CoutureType
        Vis
        Rivet
    End Enum

    Const kUnitSlip As Decimal = 1 / 1000 ^ 2       ' Facteur de conversion pour passer de mm/kN à m/N

#End Region

#Region " Attributs "

    Public ap As Decimal                            ' Largeur d'un panneau élémentaire de bac (normalement définie dans la base des bacs)
    Public bp As Decimal                            ' Longueur d'un panneau élémentaire de bac (normalement définie dans la base des bacs)

    Public nt As Integer                            ' Nombre total de panneaux élémentaire dans la direction perpendiculaire 
    Public m As Integer                             ' Nombre de travées couvertes par un panneau élémentaires (=> bp = m x d)

    Public Transition As Enu_Transition             ' Définit le type de transition entre panneaux voisins (dans la direction perp)

    Public FixNervuresMod As Enu_FixationNervures   ' Définit les modalités de fixations des nervures
    Public FixNervuresTyp As Enu_FixNervuresType    ' Définitit le type des fixations de nervures
    Public dFpNerv As Decimal                       ' Diamètre de la fixation nervure / solive

    Public ec As Decimal                            ' Espacement des vis de coutures
    Public FixCoutureType As Enu_CoutureType        ' Définit le type des vis de couture
    Public dFsCouture As Decimal                    ' Diamètre des vis de couture

    Public lMaintienBac As Boolean                  ' Indique si on active le maintien par le bac
    Public lTheta As Boolean                        ' Indique si on prend en compte la rigidité de flexion du bac pour le calcul au déversement

    Public Tpr As Decimal                           ' Epaisseur de revêtement du bac

    Public KUser As Decimal                         ' Coefficient K pour la flexibilité en distorsion, définie par l'utilisateur dans le cas des bacs à nervures rentrantes

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.nt = 2
        Me.m = 2
        Transition = Enu_Transition.Aboutage
        FixNervuresMod = Enu_FixationNervures.Toutes
        FixnervuresTyp = Enu_FixNervuresType.Pistolet
        dFpNerv = 4 / 1000
        dFsCouture = 4.8 / 1000
        Me.ec = 0.5
        FixCoutureType = Enu_CoutureType.Vis
        lMaintienBac = False
        lTheta = False
        Me.Tpr = 0
    End Sub

#End Region

#Region " Outils de calcul "

    Public Function FixNervuresSlip() As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le glissement des fixations de nervures
        '--------------------------------------------------------------------------------------------

        Return FixNervuresSlip(Me.FixnervuresTyp)

    End Function

    Public Function FixNervuresSlip(MyType As Enu_FixNervuresType) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le glissement des fixations de nervures
        '--------------------------------------------------------------------------------------------


        Dim Slip As Decimal

        Select Case MyType
            Case Enu_FixNervuresType.VisNormale : Slip = 0.15
            Case Enu_FixNervuresType.VisNeoprene : Slip = 0.35
            Case Enu_FixNervuresType.Pistolet : Slip = 0.1
        End Select

        Return Slip * kUnitSlip
    End Function

    Public Function FixCoutureSlip() As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le glissement des fixations de couture
        '--------------------------------------------------------------------------------------------

        Return FixCoutureSlip(Me.FixCoutureType)
    End Function

    Public Function FixCoutureSlip(MyType As cls_MaintienBac.Enu_CoutureType) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le glissement des fixations de couture
        '--------------------------------------------------------------------------------------------

        Dim Slip As Decimal

        Select Case MyType
            Case Enu_CoutureType.Vis : Slip = 0.25
            Case Enu_CoutureType.Rivet : Slip = 0.3
        End Select

        Return Slip * kUnitSlip
    End Function

    Public ReadOnly Property Alpha5 As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le coefficient Alpha 5
        '--------------------------------------------------------------------------------------------
        Get
            Dim MyCoef As Decimal
            Dim lEmboitement As Boolean = (Me.Transition = Enu_Transition.Emboitement)

            If lEmboitement Then
                Select Case Me.nt
                    Case 1, 2
                        MyCoef = 1
                    Case 3
                        MyCoef = 0.9
                    Case 4
                        MyCoef = 0.8
                    Case Else
                        MyCoef = 0.7
                End Select
            Else
                MyCoef = 1
            End If


            Return (MyCoef / Me.m)

        End Get
    End Property

    Public Function LongueurPanneau(entraxeD As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------------------------
        '   Renvoie la longueur d'un panneau individuel de bac
        '-------------------------------------------------------------------------------
        '   EntraxeD    [E] :   Entraxe entre les poutres
        '-------------------------------------------------------------------------------

        Return Me.m * entraxeD
    End Function

    Public Function LargeurPlancher(entraxeD As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '-------------------------------------------------------------------------------
        '   Renvoie la largeur d'un plancher
        '-------------------------------------------------------------------------------
        '   EntraxeD    [E] :   Entraxe entre les poutres
        '-------------------------------------------------------------------------------

        Return Me.nt * Me.LongueurPanneau(entraxeD)

    End Function

#End Region

#Region " Calcul des flexibilités selon CECM n88 "

    Public Function RigiditeShear(Longueur As Decimal, EntraxeD As Decimal, MyBac As cls_Bac, EYoung As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la rigidité en cisaillement procurée par le maintien du bac
        '--------------------------------------------------------------------------------------------
        '   Longueur    [E] :   Longueur de la poutre
        '   EntraxeD    [E] :   Entraxe des poutres
        '   MyBac       [E] :   Bac
        '   EYoung      [E] :   Module d'Young acier
        '--------------------------------------------------------------------------------------------

        '--( Déclaration 

        Dim Sact As Decimal
        Dim Poisson As Decimal = cls_Acier.NU
        Dim cCumul As Decimal

        '--( Calculs

        Dim c11 As Decimal = Me.Flexibilite_C11_DistorsionBac(Longueur, EntraxeD, MyBac, EYoung)
        Dim c12 As Decimal = Me.Flexibilite_C12_Shear(Longueur, EntraxeD, MyBac, EYoung, Poisson)
        Dim c21 As Decimal = Me.Flexibilite_C21_BeamFasteners(Longueur, EntraxeD, MyBac.Ep)
        Dim c22 As Decimal = Me.Flexibilite_C22_SeamFastener(Longueur, EntraxeD, MyBac)

        cCumul = c11 + c12 + c21 + c22

        Sact = Longueur / cCumul

        Return Sact

    End Function

    Public Function Flexibilite_C11_DistorsionBac(Longueur As Decimal, EntraxeD As Decimal, MyBac As cls_Bac, EYoung As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la flexibilité C1,1 du bac en distorsion
        '--------------------------------------------------------------------------------------------
        '   Longueur    [E] :   Longueur de la poutre
        '   EntraxeD    [E] :   Entraxe des poutres
        '   MyBac       [E] :   Bac
        '   EYoung      [E] :   Module d'Young acier
        '--------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim c11 As Decimal
        Dim K As Decimal

        '--> Calculs

        K = Me.CoefficientK(MyBac)

        c11 = Longueur * Me.Alpha5 * K * (MyBac.Ep / MyBac.Tp) ^ 2.5 / (EntraxeD ^ 2 * EYoung * kConvMPaPa)

        Return c11
    End Function

    Public Function CoefficientK(MyBac As cls_Bac) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le coefficient de rigidité K
        '--------------------------------------------------------------------------------------------

        '   MyBac       [E] :   Bac

        '--------------------------------------------------------------------------------------------

        Dim K As Decimal
        Dim lOK As Boolean

        If MyBac.lNervuresOuvertes Then

            '--( Pour un bac à nervures ouvertes, le coefficient K peut être obtenu d'après le guide CECM n°88

            Select Case Me.FixNervuresMod
                Case Enu_FixationNervures.Toutes
                    K = MyBac.CoefK1(lOK)
                Case Enu_FixationNervures.UneSurDeux
                    K = MyBac.CoefK2(lOK)
            End Select

        Else
            '--( Pour un bac à nervures rentrantes, le coefficient K est défini par l'utilisateur
            K = Me.KUser
        End If
        Return K
    End Function

    Public Function Flexibilite_C12_Shear(Longueur As Decimal, EntraxeD As Decimal, MyBac As cls_Bac, EYoung As Decimal, Poisson As Decimal)
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la flexibilité C1,2 du bac en cisaillement
        '--------------------------------------------------------------------------------------------
        '   Longueur    [E] :   Longueur de la poutre
        '   EntraxeD    [E] :   Entraxe des poutres
        '   MyBac       [E] :   Bac
        '   EYoung      [E] :   Module d'Young acier
        '--------------------------------------------------------------------------------------------

        Dim c12 As Decimal

        c12 = 2 * Longueur * (1 + Poisson) * (1 + 2 * MyBac.Hp / MyBac.Ep) / (EYoung * kConvMPaPa * MyBac.Tp * EntraxeD)

        Return c12
    End Function

    Public Function EntraxeLongi(ep As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie l'entraxe entre fixation sur la poutre
        '--------------------------------------------------------------------------------------------
        '   ep          [E] :   Entraxe des nervures
        '--------------------------------------------------------------------------------------------

        Dim eL As Decimal

        Select Case Me.FixNervuresMod
            Case Enu_FixationNervures.Toutes : eL = ep
            Case Enu_FixationNervures.UneSurDeux : eL = 2 * ep
        End Select

        Return eL
    End Function

    Public Function Flexibilite_C21_BeamFasteners(PorteeL As Decimal, EntraxeD As Decimal, epBac As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la flexibilité C2,1 des fixations du bac sur la poutre
        '--------------------------------------------------------------------------------------------
        '   PorteeL     [E] :   Longueur de la poutre
        '   EntraxeD    [E] :   Entraxe des poutres
        '   epBac       [E] :   Entraxe des nervures
        '--------------------------------------------------------------------------------------------

        Dim c21 As Decimal

        c21 = 2 * PorteeL * Me.EntraxeLongi(epBac) * Me.FixNervuresSlip / EntraxeD ^ 2

        Return c21

    End Function

    Public Function Flexibilite_C22_SeamFastener(PorteeL As Decimal, EntraxeD As Decimal, MyBac As cls_Bac) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la flexibilité C2,2 des fixations decouture du bac 
        '--------------------------------------------------------------------------------------------
        '   PorteeL     [E] :   Longueur de la poutre
        '   EntraxeD    [E] :   Entraxe des poutres
        '   MyBac       [E] :   Bac
        '--------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim c22 As Decimal

        Dim sS As Decimal = Me.FixCoutureSlip
        Dim sP As Decimal = Me.FixNervuresSlip
        Dim nL As Decimal = PorteeL / MyBac.LargeurModule
        Dim nC As Decimal = (EntraxeD / Me.ec) - 1 'GuD: Correction selon la formule de l'article (avant +1)
        Dim nF As Integer = NbFixationNf(MyBac)

        c22 = sS * sP * (nL - 1) / (nC * sP + Me.Beta1(nF) * sS)

        Return c22
    End Function

    Public Function NbFixationNf(MyBac As cls_Bac) As Integer
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le coefficient Beta1 - cf CCM no 88 
        '--------------------------------------------------------------------------------------------
        '   MyBac       [E] :   Bac
        '--------------------------------------------------------------------------------------------

        Dim NbRibs As Integer = Math.Floor(MyBac.LargeurModule / MyBac.Ep)
        Dim nF As Integer

        If Me.FixNervuresMod = Enu_FixationNervures.Toutes Then
            nF = NbRibs
        Else
            If (NbRibs Mod 2 = 0) Then
                nF = NbRibs / 2
            Else
                nF = NbRibs / 2 + 1
            End If
        End If

        Return nF
    End Function

    Public Function Beta1(nf As Integer) As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le coefficient Beta1 - cf CCM no 88 
        '--------------------------------------------------------------------------------------------
        '   nf          [E] :   Nombre de fixation sur la largeur d'un bac (2 mini)
        '--------------------------------------------------------------------------------------------

        Dim TabBeta1() As Decimal = {1, 1, 1, 1, 1.04, 1.13, 1.22, 1.33, 1.45, 1.56, 1.68}

        Dim Indice As Integer

        If nf > 10 Then Indice = 10 Else Indice = nf

        Return TabBeta1(Indice)
    End Function


#End Region

#Region " Résistances du bac pour le maintien au déversement (CECM 88) "

    Public Function ResistanceVbRd(PorteeL As Decimal, EntraxeD As Decimal, myBac As cls_Bac, GammaP As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   287/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance du panneau due aux fixations
        '---------------------------------------------------------------------------------------------------
        '   EntraxeD    [E] :   Entraxe des solives (= portée du bac)
        '   myBac       [E] :   Bac procurant le maintien latéral
        '   GammaP      [E] :   Coefficient partiel du bac
        '---------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim VbRd As Decimal
        Dim VbRdG As Decimal
        Dim VbRdL As Decimal
        Dim TpRed As Decimal
        Dim Br As Decimal
        Dim EpsilonP As Decimal

        '--( Intialisations

        TpRed = myBac.Tp - Me.Tpr
        Br = myBac.Ep - myBac.Bt
        EpsilonP = Math.Sqrt(235 / myBac.Fyp)
        VbRdG = Me.ResistanceVbRdGlobal(PorteeL, EntraxeD, myBac, GammaP)

        '--( Calculs

        If IsSmallerOrEqual(Br / TpRed, 86.7 * EpsilonP) Then
            VbRd = VbRdG
        Else
            VbRdL = Me.ResistanceVbRdLocal(PorteeL, myBac, GammaP)

            VbRd = VbRdG * VbRdL / (VbRdG + VbRdL)
        End If

        Return VbRd
    End Function

    Public Function ResistanceVbRdGlobal(PorteeL As Decimal, EntraxeD As Decimal, myBac As cls_Bac, GammaP As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   287/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance du panneau due aux fixations
        '---------------------------------------------------------------------------------------------------
        '   PorteeL     [E] :   Portée de la solive maitenue
        '   EntraxeD    [E] :   Entraxe des solives (= portée du bac)
        '   myBac       [E] :   Bac procurant le maintien latéral
        '   GammaP      [E] :   Coefficient partiel du bac
        '---------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim VbRdG As Decimal
        Dim Dx, Dy As Decimal
        Dim PerimU As Decimal
        Dim TpRed As Decimal
        Dim Ip As Decimal

        '--( Intialisations

        TpRed = myBac.Tp - Me.Tpr
        PerimU = myBac.Bb + myBac.Ep - myBac.Bt + Math.Sqrt(4 * myBac.Hp ^ 2 + (myBac.Bt - myBac.Bb) ^ 2)
        Ip = myBac.Ieff * myBac.Ep

        Dx = cls_Acier.EYACIER * TpRed ^ 3 * myBac.Ep / (12 * (1 - cls_Acier.NU) * PerimU)
        Dy = cls_Acier.EYACIER * Ip / myBac.Ep

        '--( Calculs

        VbRdG = Me.FonctionKnG * PorteeL / EntraxeD ^ 2 * Dx ^ 0.25 * Dy ^ 0.75 * kConvMPaPa / GammaP

        Return VbRdG

    End Function

    Public Function ResistanceVbRdLocal(PorteeL As Decimal, myBac As cls_Bac, GammaP As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   287/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance du panneau due aux fixations
        '---------------------------------------------------------------------------------------------------
        '   PorteeL     [E] :   Portée de la solive maitenue
        '   EntraxeD    [E] :   Entraxe des solives (= portée du bac)
        '   myBac       [E] :   Bac procurant le maintien latéral
        '   GammaP      [E] :   Coefficient partiel du bac
        '---------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim VbRdL As Decimal
        Dim TpRed As Decimal

        '--( Intialisations

        TpRed = myBac.Tp - Me.Tpr


        '--( Calculs

        VbRdL = 4.83 * cls_Acier.EYACIER * (TpRed / (myBac.Ep - myBac.Bt)) ^ 2 * PorteeL * TpRed * kConvMPaPa / GammaP

        Return VbRdL

    End Function

    Public Function ResistanceFpEnRd(EntraxeD As Decimal, myBac As cls_Bac, GammaP As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   27/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance du panneau due aux fixations
        '---------------------------------------------------------------------------------------------------
        '   EntraxeD    [E] :   Entraxe des solives (= portée du bac)
        '   myBac       [E] :   Bac procurant le maintien latéral
        '   GammaP      [E] :   Coefficient partiel du bac
        '---------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim FpEndRd As Decimal
        Dim locTp As Decimal

        '--( Initialisation

        locTp = myBac.Tp - Me.Tpr

        '--( Calculs

        FpEndRd = Me.FonctionKn * EntraxeD * (locTp ^ 1.5) / (myBac.Ep ^ 0.5) * myBac.Fyp / GammaP

        Return FpEndRd * kConvMPaPa

    End Function

    Private Function FonctionKn() As Decimal
        '---------------------------------------------------------------------------------------------------
        '   28/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Calcul du coefficient kn pour la résistance à l'extrémité du bac
        '---------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------

        Dim kn As Decimal

        Select Case Me.FixNervuresMod
            Case Enu_FixationNervures.Toutes
                kn = 0.9
            Case Enu_FixationNervures.UneSurDeux
                kn = 0.3
        End Select

        Return kn

    End Function
    Private Function FonctionKnG() As Decimal
        '---------------------------------------------------------------------------------------------------
        '   28/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Calcul du coefficient kn pour la résistance à l'extrémité du bac
        '---------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------

        Dim knG As Decimal

        Select Case Me.FixNervuresMod
            Case Enu_FixationNervures.Toutes
                knG = 28.8
            Case Enu_FixationNervures.UneSurDeux
                knG = 14.4
        End Select

        Return knG

    End Function

    Public Function ResistanceFpRd(Tnom As Decimal, Fup As Decimal, GammaM2 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   27/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance individuelle d'une fixation bac-solive
        '---------------------------------------------------------------------------------------------------
        '   Tnom        [E]:    Epaisseur nominale du bac
        '   Fup         [E] :   Résistance ultime à la traction du bac
        '   GammaM2     [E] :   Coefficient partiel
        '---------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim FpRd As Decimal
        Dim Alpha As Decimal
        Dim diametre As Decimal = Me.dFpNerv
        Dim Tp As Decimal
        Const UnMM As Decimal = 1 / 1000

        '--( Initialisation

        Tp = Tnom - Me.Tpr

        '--( Calcul

        Select Case Me.FixNervuresTyp
            Case Enu_FixNervuresType.Pistolet

                '*** d'après tableau 8.3 de l'EN 1993-1-13

                FpRd = 3.2 * diametre * Tp * Fup / GammaM2

            Case Enu_FixNervuresType.VisNeoprene, Enu_FixNervuresType.VisNormale

                '*** d'après tableau 8.2 de l'EN 1993-1-13
                '=== On suppose que l'épaisseur de la semelle est toujours supérieure à 2.5 fois l'épaisseur du bac

                If IsSmaller(Tp, UnMM) Then
                    Alpha = Math.Min(2.1, 3.2 * Math.Sqrt(Tp / Me.dFpNerv))
                Else
                    Alpha = 2.1
                End If

                FpRd = Alpha * diametre * Tp * Fup / GammaM2

        End Select

        Return FpRd * kConvMPaPa

    End Function

    Public Function ResistanceFsRd(Tnom As Decimal, Fup As Decimal, GammaM2 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   27/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance individuelle d'une fixation de couture
        '---------------------------------------------------------------------------------------------------
        '   Tnom        [E]:    Epaisseur nominale du bac
        '   Fup         [E] :   Résistance ultime à la traction du bac
        '   GammaM2     [E] :   Coefficient partiel
        '---------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim FsRd As Decimal
        Dim Alpha As Decimal = Math.Min(2.1, 3.2 * Math.Sqrt(Tpr / Me.dFsCouture))
        Dim Tp As Decimal

        '--( Initialisation

        Tp = Tnom - Me.Tpr
        Alpha = Math.Min(2.1, 3.2 * Math.Sqrt(Tp / Me.dFsCouture))

        '--( Calcul

        '*** d'après tableau 8.1 ou 8.2 de l'EN 1993-1-13

        FsRd = Alpha * Me.dFsCouture * Tp * Fup / GammaM2

        Return FsRd * kConvMPaPa

    End Function

    Public Function ResistanceVmRd(EntraxeD As Decimal, myBac As cls_Bac, GammaM2 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------
        '   27/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Résistance du panneau due aux fixations
        '---------------------------------------------------------------------------------------------------
        '   EntraxeD    [E] :   Entraxe des solives (= portée du bac)
        '   myBac       [E] :   Bac procurant le maintien latéral
        '   GammaM2     [E] :   Coefficient partiel
        '---------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim VmRd, FsRd, FpRd As Decimal
        Dim pNc, pNf As Integer
        Dim pBeta1, pBeta3 As Decimal
        Dim Tnom As Decimal = myBac.Tp

        '--( Initialisations

        FsRd = Me.ResistanceFsRd(Tnom, myBac.Fup, GammaM2)
        FpRd = Me.ResistanceFpRd(Tnom, myBac.Fup, GammaM2)
        pNf = NbFixationNf(myBac)
        pBeta1 = Me.Beta1(pNf)
        pBeta3 = (pNf - 1) / pNf

        pNc = Math.Floor(EntraxeD / Me.ec) - 1

        '--( Calcul

        VmRd = pNc * FsRd + pBeta1 / pBeta3 * FpRd

        Return VmRd

    End Function

#End Region

#Region " Outils divers "

    Public Function Clone() '--> Utilisé pour dupliquer 
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
