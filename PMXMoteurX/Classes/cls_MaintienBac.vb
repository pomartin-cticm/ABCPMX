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
    End Sub

#End Region

#Region " Outils de calcul "

    Public ReadOnly Property FixNervuresSlip As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le glissement des fixations de nervures
        '--------------------------------------------------------------------------------------------

        Get
            Dim Slip As Decimal

            Select Case FixnervuresTyp
                Case Enu_FixNervuresType.VisNormale : Slip = 0.15
                Case Enu_FixNervuresType.VisNormale : Slip = 0.35
                Case Enu_FixNervuresType.Pistolet : Slip = 0.1
            End Select

            Return Slip * kUnitSlip
        End Get
    End Property

    Public ReadOnly Property FixCoutureSlip As Decimal
        '--------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie le glissement des fixations de couture
        '--------------------------------------------------------------------------------------------
        Get
            Dim Slip As Decimal

            Select Case FixCoutureType
                Case Enu_CoutureType.Vis : Slip = 0.25
                Case Enu_CoutureType.Rivet : Slip = 0.3
            End Select

            Return Slip * kUnitSlip
        End Get
    End Property

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
        Dim lOK As Boolean

        '--> Calculs

        Select Case Me.FixNervuresMod
            Case Enu_FixationNervures.Toutes
                K = MyBac.CoefK1(lOK)
            Case Enu_FixationNervures.UneSurDeux
                K = MyBac.CoefK2(lOK)
        End Select

        c11 = Longueur * Me.Alpha5 * K * (MyBac.Ep / MyBac.Tp) ^ 2.5 / (EntraxeD ^ 2 * EYoung)

        Return c11
    End Function

#End Region

#Region " Outils divers "

    Public Function Clone() '--> Utilisé pour dupliquer 
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
