Public Class cls_Gamma

#Region " Attributs "

    Public GammaM0 As Decimal
    Public GammaM1 As Decimal
    Public GammaM2 As Decimal

    Public GammaC As Decimal
    Public GammaVs As Decimal
    Public GammaVc As Decimal
    'Public GammaVp As Decimal
    Public lGammaV_unique As Boolean 'indique si on considère un seul gammaV (=True) ou alors est ce qu'on fait la différence avec GammaVs et GammaVp (=False)
    Public GammaS As Decimal
    Public GammaP As Decimal

    Public GammaM_fi As Decimal 'for the resistance of structural steel (steel beams)
    Public GammaM_fi_a As Decimal 'for the resistance of structural steel (composite beams)
    Public GammaC_fi As Decimal ' for the resistance of the compressed concrete
    Public GammaV_fi As Decimal ' for the resistance of shear connectors
    Public GammaM_fi_s As Decimal ' for the resistance of reinforcement steel bars

    Public GammaG_sup As Decimal
    Public GammaG_inf As Decimal
    Public GammaQ As Decimal

    Public Psi0_Q1 As Decimal
    Public Psi1_Q1 As Decimal
    Public Psi2_Q1 As Decimal

    Public Psi0_Q2 As Decimal
    Public Psi1_Q2 As Decimal
    Public Psi2_Q2 As Decimal

#End Region

#Region " Constructeurs "

    Public Sub New()

        Me.GammaM0 = 1.0
        Me.GammaM1 = 1.0
        Me.GammaM2 = 1.25

        Me.GammaC = 1.5
        Me.GammaVs = 1.25
        Me.GammaVc = 1.25
        Me.GammaS = 1.15
        Me.GammaP = 1.0
        'Me.GammaVp = 1.25
        Me.lGammaV_unique = True

        Me.GammaM_fi = 1.0
        Me.GammaC_fi = 1.0
        Me.GammaV_fi = 1.0

        Me.GammaG_sup = 1.35
        Me.GammaG_inf = 1.0
        Me.GammaQ = 1.5

        Me.Psi0_Q1 = 0.7
        Me.Psi1_Q1 = 0.5
        Me.Psi2_Q1 = 0.3

        Me.Psi0_Q2 = 0.7
        Me.Psi1_Q2 = 0.5
        Me.Psi2_Q2 = 0.3

    End Sub

#End Region

#Region " Fonctions de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub TransfertFrom(MyGamma As cls_Gamma)

        Me.GammaM0 = MyGamma.GammaM0
        Me.GammaM1 = MyGamma.GammaM1
        Me.GammaM2 = MyGamma.GammaM2

        Me.GammaC = MyGamma.GammaC
        Me.GammaVs = MyGamma.GammaVs
        Me.GammaVc = MyGamma.GammaVc
        Me.GammaS = MyGamma.GammaS
        Me.GammaP = MyGamma.GammaP
        'Me.GammaVp = 1.25
        Me.lGammaV_unique = MyGamma.lGammaV_unique

        Me.GammaM_fi = MyGamma.GammaM_fi
        Me.GammaC_fi = MyGamma.GammaC_fi
        Me.GammaV_fi = MyGamma.GammaV_fi

        Me.GammaG_sup = MyGamma.GammaG_sup
        Me.GammaG_inf = MyGamma.GammaG_inf
        Me.GammaQ = MyGamma.GammaQ

        Me.Psi0_Q1 = MyGamma.Psi0_Q1
        Me.Psi1_Q1 = MyGamma.Psi1_Q1
        Me.Psi2_Q1 = MyGamma.Psi2_Q1

        Me.Psi0_Q2 = MyGamma.Psi0_Q2
        Me.Psi1_Q2 = MyGamma.Psi1_Q2
        Me.Psi2_Q2 = MyGamma.Psi2_Q2

    End Sub

#End Region

End Class
