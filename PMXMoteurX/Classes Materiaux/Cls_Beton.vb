Public Class Cls_Beton

#Region " Tableaux partagés "

    Public Shared TabClasseBeton() As String = {"C20/25", "C25/30", "C30/37", "C35/45", "C40/50", "C45/55", "C50/60"}

    Const kUnitMM As Decimal = 1000

#End Region



#Region " Attributs "

    ''' <summary>
    ''' Type de béton
    ''' </summary>
    Public Type As Enum_TypeBeton

    ''' <summary>
    ''' classe du béton
    ''' </summary>
    Public Classe As String

    ''' <summary>
    ''' résistance caractéristique à la compression du béton, d’après les valeurs du Tableau 3, en fonction de la classe de béton (Pa)
    ''' </summary>
    Public Fck As Decimal

    ''' <summary>
    ''' résistance moyenne à la compression (Pa)
    ''' </summary>
    Public Fcm As Decimal

    ''' <summary>
    ''' module sécant du béton
    ''' </summary>
    Public Ecm As Decimal

#End Region

#Region " Enumérations "

    Public Enum Enum_TypeBeton
        Normal
        Leger
    End Enum

#End Region

#Region " Fonction de calcul "

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Public Sub Calcul_Proprietes()

        Fck = GetFckDeClasse()
        Fcm = Fck + 8
        Ecm = (22 * (Fcm / 10) ^ 0.3) * 10 ^ 3

    End Sub

    Private Function GetFckDeClasse() As Decimal

        Dim iPoint As Integer = Me.Classe.IndexOf("/") 'InStr(Me.Classe, "/")
        Dim Chaine As String

        If iPoint >= 0 Then
            Chaine = Me.Classe.Substring(1, iPoint - 1)
            Return CDec(Chaine)
        End If

    End Function

    ''' <summary>
    ''' Calcul du coefficient d'équivalence acier-béton
    ''' </summary>
    ''' <returns></returns>
    Public Function CoefficientEquivalence(RH As Decimal, h0 As Decimal, t As Decimal, t0 As Decimal, PsiL As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   17/05/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient d'équivalence acier béton
        '---------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim n0 As Decimal
        Dim Phi_tt0 As Decimal
        Dim MyBetaC_tt0 As Decimal
        Dim Phi0 As Decimal
        Dim MyPhiRH, MyBetaFcm, MyBeta_t0 As Decimal
        Dim MyBetaH As Decimal

        '--> Initialisation

        Me.Calcul_Proprietes()

        '--> Calculs

        n0 = Cls_Acier.EY / Ecm

        MyBetaFcm = Me.BetaFcm
        MyBeta_t0 = Me.Beta_t0(t0)
        MyPhiRH = Me.PhiRH(RH, h0)

        Phi0 = MyPhiRH * MyBetaFcm * MyBeta_t0

        MyBetaH = Me.BetaH(RH, h0)
        MyBetaC_tt0 = Me.BetaC_tt0(RH, h0, t, t0)

        Phi_tt0 = Phi0 * MyBetaC_tt0

        '--> Fin

        Return n0 * (1 + PsiL * Phi_tt0)

    End Function

    Public Function BetaC_tt0(RH As Decimal, h0 As Decimal, t As Decimal, t0 As Decimal) As Decimal

        Dim MyBetaC As Decimal

        MyBetaC = ((t - t0) / (BetaH(RH, h0) + t - t0)) ^ 0.3

        Return MyBetaC

    End Function

    Public Function BetaH(RH As Decimal, h0 As Decimal) As Decimal
        Dim MyBetaH As Decimal
        Dim h0MM As Decimal = h0 * kunitmm

        If Me.Fcm <= 35 Then
            MyBetaH = Math.Min(1500, 1.5 * (1 + (3 * RH / 250) ^ 18) * h0MM + 250)
        Else
            Dim MyAlpha3 As Decimal
            MyAlpha3 = Me.AlphaFactors(3)

            MyBetaH = Math.Min(1500 * MyAlpha3, 1.5 * (1 + (3 * RH / 250) ^ 18) * h0MM + 250 * MyAlpha3)
        End If
        Return MyBetaH
    End Function

    Private Function AlphaFactors(Indice As Integer) As Decimal
        '------------------------------------------------------------------------
        '   Renvoie les valeurs des coefficients Alpha1, 2 et 3
        '------------------------------------------------------------------------

        Dim Alpha As Decimal
        Dim Exposant As Decimal

        Select Case Indice
            Case 1 : Exposant = 0.7
            Case 2 : Exposant = 0.2
            Case 3 : Exposant = 0.5
        End Select

        Alpha = (35 / Me.Fcm) ^ Exposant

        Return Alpha

    End Function

    Public Function PhiRH(RH As Decimal, h0 As Decimal) As Decimal
        Dim MyRH As Decimal
        Dim h0MM As Decimal = h0 * kUnitMM

        If Me.Fcm <= 35 Then
            MyRH = 1 + (1 - RH / 100) / (0.1 * h0MM ^ (1 / 3))
        Else
            Dim MyAlpha1, MyAlpha2 As Decimal
            MyAlpha1 = Me.AlphaFactors(1)
            MyAlpha2 = Me.AlphaFactors(2)
            MyRH = MyAlpha2 * (1 + MyAlpha1 * (1 - RH / 100) / (0.1 * h0MM ^ (1 / 3)))
        End If
        Return MyRH
    End Function

    Public Function BetaFcm() As Decimal
        Return (16.8 / Math.Sqrt(Me.Fcm))
    End Function

    Public Function Beta_t0(t0 As Decimal) As Decimal
        Return (1 / (1 + t0 ^ 0.2))
    End Function

#End Region

#Region " Constructeur "

    Sub New()

        Me.Type = Enum_TypeBeton.Normal
        Me.Classe = "C20/25"
        Me.Fck = 20
        Calcul_Proprietes()

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
