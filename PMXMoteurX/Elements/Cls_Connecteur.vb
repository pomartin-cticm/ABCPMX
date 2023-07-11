
Public Class Cls_Connecteur

#Region " Attributs "

    ''' <summary>
    ''' Hauteur nominale
    ''' </summary>
    Public hsc As Decimal

    ''' <summary>
    ''' Diamètre
    ''' </summary>
    Public d As Decimal

    ''' <summary>
    ''' Résistance ultime à la traction
    ''' </summary>
    Public Fu As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.d = 0.019
        Me.hsc = 0.1
        Me.Fu = 450

    End Sub

#End Region

#Region " Outils de calcul "

    Public Function PRdDallePleineG1(Fck As Decimal, Ecm As Decimal, GammaVS As Decimal, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------
        '   TU : 

        '--> Déclaration

        Dim PRdC, PRdS As Decimal

        '--> Calcul

        PRdS = PRdDallePleineG1Acier(GammaVS)
        PRdC = PRdDallePleineG1Beton(Fck, Ecm, GammaVC)

        '--> Fin

        Return Math.Min(PRdC, PRdS)

    End Function

    Public Function PRdDallePleineG1Acier(GammaVS As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation acier (6.18 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   GammaVS [E] :   Coefficient partiel pour la première équation (acier)
        '-----------------------------------------------------------------------------------------------------------------
        '   TU :
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal

        '--> Calcul

        PRd = 0.8 * Me.Fu * Math.PI * Me.d ^ 2 / (4 * GammaVS) * kConvMPaPa

        Return PRd
    End Function

    Public Function PRdDallePleineG1Beton(Fck As Decimal, Ecm As Decimal, GammaVC As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation béton (6.19 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   Fck     [E] :   Résistance caractéristique à la compression du béton
        '   Ecm     [E] :   Module sécant du béton
        '   GammaVC [E] :   Coefficient partiel pour la seconde équation (béton)
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PRd As Decimal

        '--> Calcul

        PRd = 0.29 * Me.Alpha * Me.d ^ 2 * Math.Sqrt(Fck * Ecm) / GammaVC * kConvMPaPa

        Return PRd
    End Function

    Public ReadOnly Property Alpha As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul de la résistance en dalle pleine / Génération 1 / Equation béton
        '   Coefficient de réduction Alpha dans l'équation béton (Eq(6.19) de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        Get
            Dim pAlpha As Decimal

            If hsc / d > 4 Then
                pAlpha = 1
            Else
                pAlpha = 0.2 * (hsc / d - 1)
            End If

            Return pAlpha
        End Get
    End Property

    Public Function CoefkT(nr As Decimal, MyBac As Cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction kT pour un bac perpendiculaire
        '   (§ 6.6.5.1 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   nr      [E] :   Nombre de connecteurs / rangée (perp à l'axe de la poutre)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MykT As Decimal
        Dim b0 As Decimal = MyBac.LargeurB0
        Dim hP As Decimal = MyBac.h_p

        '--> Calcul

        MykT = 0.7 / Math.Sqrt(nr) * b0 / hP * (Me.hsc / hP - 1)

        Return Math.Max(MykT, Me.kTMax(nr, MyBac))

    End Function

    Public Function kTMax(nr As Decimal, MyBac As Cls_Bac) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction kT pour un bac perpendiculaire
        '   (§ 6.6.5.1 de EN 1994-1-1)
        '-----------------------------------------------------------------------------------------------------------------
        '   nr      [E] :   Nombre de connecteurs / rangée (perp à l'axe de la poutre)
        '   MyBac   [E] :   Bac acier
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim myKtMax As Decimal

        '--> Calcul

        Select Case nr
            Case 1
                If MyBac.lPreperce Then
                    myKtMax = 0.75
                Else
                    If MyBac.tp <= 0.001 Then
                        myKtMax = 0.85
                    Else
                        myKtMax = 1
                    End If
                End If
            Case 2
                If MyBac.lPreperce Then
                    myKtMax = 0.6
                Else
                    If MyBac.tp <= 0.001 Then
                        myKtMax = 0.7
                    Else
                        myKtMax = 0.8
                    End If
                End If
        End Select

        Return myKtMax

    End Function

#End Region

End Class
