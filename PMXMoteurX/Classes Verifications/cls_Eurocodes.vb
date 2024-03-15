Public Class cls_Eurocodes

    '====================================================================================================================
    '   FONCTIONS ET METHODES EUROCODE
    '====================================================================================================================

#Region " Attributs "


#End Region

#Region " Constructeurs "

    Public Sub New()

    End Sub

#End Region

#Region " Fonctions pour le voilement par cisaillement "

    Public Function ReductionShearBuckling(LambdaBw As Decimal, EtaW As Decimal, lMontantRigid As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   19/12/2023 :    Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   COURBES DE réduction du voilement par cisaillement selon EN 1993-1-5 § 5
        '----------------------------------------------------------------------------------------------------------------
        '   LambdaBw        [E] :   Elancement réduit
        '   EtaW            [E] :   Coefficient Eta
        '   lMontantRigid   [E] :   Indique si on utilise la colonne montant rigide du Tablea 5.3 dans EN 1993-1-5 
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim KhiW As Decimal

        '--> Calculs

        If IsSmallerOrEqual(LambdaBw, 0.83 / EtaW) Then
            KhiW = EtaW
        ElseIf IsSmallerOrEqual(LambdaBw, 1.08) Then
            KhiW = 0.83 / LambdaBw
        Else 'lambda_w>1.08
            If lMontantRigid Then
                KhiW = 1.37 / (0.7 + LambdaBw)
            Else
                KhiW = 0.83 / LambdaBw
            End If
        End If

        Return KhiW
    End Function

#End Region

#Region " Fonctions pour le déversement "

    Public Function ReductionDeversement(AlphaLT As Decimal, LambdaB As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   19/12/2023 :    Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   COURBES DE DEVERSEMENT selon EN 1993-1-1 § 6.3.2.2
        '----------------------------------------------------------------------------------------------------------------
        '   AlphaLT     [E] :   Facteur d'imperfection
        '   LambdaB     [E] :   Elancement réduit
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim KhiLT As Decimal
        Dim PhiLT As Decimal

        '--> Calcul

        PhiLT = 0.5 * (1 + AlphaLT * (LambdaB - 0.2) + LambdaB ^ 2)
        KhiLT = Math.Min(1, 1 / (PhiLT + Math.Sqrt(PhiLT ^ 2 - LambdaB ^ 2)))

        Return KhiLT

    End Function

    Public Function GetAlphaLTFromProfil(myProfil As cls_ProfilA) As Decimal
        '----------------------------------------------------------------------------------------------------------------
        '   19/12/2023 :    Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Renvoie le coefficient AlphaLT à prendre en compte dans les courbes de déversement
        '   D'après Tableau 6.4 de l'EN 1993-1-1
        '----------------------------------------------------------------------------------------------------------------
        '   AlphaLT     [E] :   Facteur d'imperfection
        '   LambdaB     [E] :   Elancement réduit
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim AlphaLT As Decimal
        Dim RatioHsurB As Decimal = myProfil.ha / Math.Min(myProfil.Bfs, myProfil.Bfi)

        Select Case myProfil.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                If IsSmallerOrEqual(RatioHsurB, 2) Then AlphaLT = 0.21 Else AlphaLT = 0.34
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                If IsSmallerOrEqual(RatioHsurB, 2) Then AlphaLT = 0.49 Else AlphaLT = 0.76
            Case Else
                AlphaLT = 0.76
        End Select

        Return AlphaLT

    End Function

#End Region

#Region " Calcul des soudures "

    Public Function CalculSoudure(myFlux As Decimal, GammaM2 As Decimal, BetaW As Decimal, Fu As Decimal) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul des gorges de soudure pour les flux de cisaillement ELU
        '----------------------------------------------------------------------------------------------------------
        '   myFlux              [E] :   Flux de cisaillement
        '   GammaM2             [E] :   Coefficient partiel
        '   BetaW               [E] :   Coefficient BetaW selon EN 1993-1-8 pour le calcul des soudures
        '   Fu                  [E] :   Résistance ultime à la traction
        '----------------------------------------------------------------------------------------------------------

        Dim Aw As Decimal

        Aw = Math.Sqrt(3) / 2 * Math.Abs(myFlux) / (Fu * kConvMPaPa) * BetaW * GammaM2

        Return Aw
    End Function

#End Region

End Class
