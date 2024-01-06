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
    Public bp As Decimal                            ' Longueur d'un panneau élémentaire de bac

    Public nt As Integer                            ' Nombre total de panneaux élémentaire dans la direction perpendiculaire 
    Public m As Integer                             ' Nombre de travées couvertes par un panneau élémentaires (=> bp = m x d)

    Public Transition As Enu_Transition             ' Définit le type de transition entre panneaux voisins (dans la direction perp)

    Public FixNervuresMod As Enu_FixationNervures   ' Définit les modalités de fixations des nervures
    Public FixnervuresTyp As Enu_FixNervuresType    ' Définitit le type des fixations de nervures

    Public ec As Decimal                            ' Espacement des vis de coutures
    Public FixCoutureType As Enu_CoutureType        ' Définit le type des vis de couture

    Public lMaintienBac As Boolean                  ' Indique si on active le maintien par le bac

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.nt = 2
        Me.m = 2
        Transition = Enu_Transition.Aboutage
        FixNervuresMod = Enu_FixationNervures.Toutes
        FixnervuresTyp = Enu_FixNervuresType.Pistolet
        Me.ec = 0.5
        FixCoutureType = Enu_CoutureType.Vis
        lMaintienBac = False
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

        Select Case Me.FixNervuresMod
            Case Enu_FixationNervures.Toutes
                K = MyBac.CoefK1(lOK)
            Case Enu_FixationNervures.UneSurDeux
                K = MyBac.CoefK2(lOK)
        End Select

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
        Dim nC As Decimal = (EntraxeD / Me.ec) + 1
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

#Region " Outils divers "

    Public Function Clone() '--> Utilisé pour dupliquer 
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
