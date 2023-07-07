Public Class Cls_Gamma

#Region " Attributs "

    Public GammaM0 As Decimal
    Public GammaM1 As Decimal
    Public GammaM2 As Decimal

    Public GammaC As Decimal
    Public GammaV As Decimal
    Public GammaVs As Decimal
    Public GammaVp As Decimal
    Public lGammaV_unique As Boolean 'indique si on considère un seul gammaV (=True) ou alors est ce qu'on fait la différence avec GammaVs et GammaVp (=False)
    Public GammaS As Decimal
    Public GammaP As Decimal

    Public GammaM_fi As Decimal
    Public GammaC_fi As Decimal
    Public GammaV_fi As Decimal

    Public GammaG_sup As Decimal
    Public GammaG_inf As Decimal
    Public GammaQ As Decimal

    Public Psi0 As Decimal
    Public Psi1 As Decimal
    Public Psi2 As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.GammaM0 = 1.0
        Me.GammaM1 = 1.0
        Me.GammaM2 = 1.25

        Me.GammaC = 1.5
        Me.GammaV = 1.25
        Me.GammaS = 1.15
        Me.GammaP = 1.0

        Me.GammaVs = 1.25
        Me.GammaVp = 1.25

        Me.lGammaV_unique = True

        Me.GammaM_fi = 1.0
        Me.GammaC_fi = 1.0
        Me.GammaV_fi = 1.0

        Me.GammaG_sup = 1.35
        Me.GammaG_inf = 1.0
        Me.GammaQ = 1.5

        Me.Psi0 = 0.7
        Me.Psi1 = 0.5
        Me.Psi2 = 0.3

    End Sub

#End Region

End Class
