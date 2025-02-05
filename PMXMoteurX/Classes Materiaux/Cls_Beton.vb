Public Class cls_Beton

#Region " Constantes et Tableaux partagés "

    Public Shared TabClasseBeton() As String = {"C20/25", "C25/30", "C30/37", "C35/45", "C40/50", "C45/55", "C50/60", "C70/85"}
    Public Shared TabClasseBetonLeger() As String = {"LC20/22", "LC25/28", "LC30/33", "LC35/38", "LC40/44", "LC45/50", "LC50/55", "LC60/66"}

    Const kUnitMM As Decimal = 1000
    Const RHOCDEFAUT As Decimal = 2500

    Public Shared TabClasseCiment() As String = {"CS", "CN", "CR"}

#End Region

#Region " Attributs "

    Public Classe As String                         ' Classe du béton
    Public Ciment As String                         ' Classe du ciment

    Public Fck As Decimal                           ' résistance caractéristique à la compression du béton, d’après les valeurs du Tableau 3, en fonction de la classe de béton 

    Public Fctk_005 As Decimal                      ' fractile 5% de la résistance à la traction du béton (utilisé pour le calcul de la résistance d'un connecteur selon l'annexe G de l'EC4 G2)
    Public Fcm As Decimal                           ' résistance moyenne à la compression
    Public Fctm As Decimal                          ' résistance à la traction 
    Public Ecm As Decimal                           ' module sécant du béton

    Public lCrackingLimitation As Boolean           ' Indique si l'ouverture des fissures est controlé

    Public wk_max As Decimal                        ' Largeur d'ouverture maximale des fissures autorisée

    Public RhoC As Decimal                          ' Masse volumique (kg/m3)

    Public lLeger As Boolean                        ' Indique si béton léger

    Public kE As Decimal                            ' Coefficient pour le calcul de Ecm dans la génération 2 de l'EN 1992-1-1

#End Region

    '#Region " Enumérations "

    '    Public Enum Enum_TypeBeton
    '        Normal
    '        Leger
    '    End Enum

    '#End Region

#Region " Fonctions de calcul "

    ''' <summary>
    ''' Calcul des propriétés du béton
    ''' Initialisation des paramètres de la classe
    ''' </summary>
    Public Sub Calcul_Proprietes(lGeneration1 As Boolean)
        '----------------------------------------------------------------------------------
        '   05/02/25 :  Création
        '----------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------
        '   Propriétés du béton - Formules du Tableau 3.1 de l'EN 1992-1-1 (béton normal)
        '                         Formules du Tableau 11.3.1 pour le béton léger
        '----------------------------------------------------------------------------------
        '   lGeneration1    [E] :   Indique si génération 1 de l'EN 1992-1-1
        '----------------------------------------------------------------------------------

        If lGeneration1 Then Calcul_ProprietesG1() Else Calcul_Proprietesg2

    End Sub


    Private Sub Calcul_ProprietesG2()
        '----------------------------------------------------------------------------------
        '   05/02/25 :  Création
        '----------------------------------------------------------------------------------
        '   Calcul des propriétés du béton avec la génération 2 de l'EN 1992-1-1
        '----------------------------------------------------------------------------------

        '--> Déclarations

        Dim EtaE, Eta1 As Decimal

        '--> Traitement

        Fck = GetFckDeClasse()
        Fcm = Fck + 8

        If Fck < 50 Then
            Fctm = 0.3 * Fck ^ (2 / 3)
        Else
            Fctm = 1.1 * (Fck) ^ (1 / 3)
        End If
        If lLeger Then
            Eta1 = 0.4 + 0.6 * Math.Min(1, Me.RhoC / 2200)
            Fctm = Eta1 * Fctm
        End If

        Fctk_005 = 0.4 * Fctm

        Ecm = Me.kE * (Fcm) ^ (1 / 3)
        If Me.lLeger Then
            EtaE = Math.Min(1, (Me.RhoC / 2200) ^ 2)
            Ecm = EtaE * Ecm
        End If


    End Sub

    Private Sub Calcul_ProprietesG1()
        '----------------------------------------------------------------------------------
        '   01/06/23 :  Création
        '----------------------------------------------------------------------------------
        '   Calcul des propriétés du béton avec la génération 1 de l'EN 1992-1-1
        '----------------------------------------------------------------------------------
        '   Propriétés du béton - Formules du Tableau 3.1 de l'EN 1992-1-1 (béton normal)
        '                         Formules du Tableau 11.3.1 pour le béton léger
        '----------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------

        '--> Déclarations

        Dim EtaE, Eta1 As Decimal

        '--> Traitement

        Fck = GetFckDeClasse()
        Fcm = Fck + 8
        If Fck < 50 Then
            Fctm = 0.3 * Fck ^ (2 / 3)
        Else
            Fctm = 2.12 * Math.Log(1 + Fcm / 10)
        End If
        If lLeger Then
            Eta1 = 0.4 + 0.6 * Math.Min(1, Me.RhoC / 2200)
            Fctm = Eta1 * Fctm
        End If

        Fctk_005 = 0.4 * Fctm

        Ecm = (22 * (Fcm / 10) ^ 0.3) * 10 ^ 3
        If Me.lLeger Then
            EtaE = Math.Min(1, (Me.RhoC / 2200) ^ 2)
            Ecm = EtaE * Ecm
        End If

    End Sub


    Private Function GetFckDeClasse() As Decimal

        Dim iC As Integer = Me.Classe.IndexOf("C")
        Dim iPoint As Integer = Me.Classe.IndexOf("/") 'InStr(Me.Classe, "/")
        Dim Chaine As String
        Dim MyResult As Decimal = 0

        If iPoint >= 0 Then
            Chaine = Me.Classe.Substring(iC + 1, iPoint - 1 - iC)
            MyResult = CDec(Chaine)
        End If

        Return MyResult
    End Function

    ''' <summary>
    ''' Calcul du coefficient d'équivalence acier-béton à court terme
    ''' </summary>
    ''' <returns></returns>
    Public Function CoefficientEquivalenceCT(lGeneration1 As Boolean) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   12/07/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient d'équivalence acier béton à court terme
        '---------------------------------------------------------------------------------------------------------------
        '   lGeneration1    [E] :   Indique si génération 1 de l'EN 1992-1-1
        '---------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim n0 As Decimal

        '--> Initialisation

        Me.Calcul_Proprietes(lGeneration1)

        '--> Calculs

        n0 = cls_Acier.EYACIER / Ecm

        Return n0
    End Function

    ''' <summary>
    ''' Calcul du coefficient d'équivalence acier-béton
    ''' </summary>
    ''' <returns></returns>
    Public Function CoefficientEquivalence(RH As Decimal, h0 As Decimal, t As Decimal, t0 As Decimal, PsiL As Decimal, lGeneration1 As Boolean) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   05/02/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient d'équivalence acier béton
        '---------------------------------------------------------------------------------------------------------------
        '   RH              [E] :   Taux d'humidité relative
        '   h0              [E] :   Rayon moyen de l'élément en béton
        '   t, t0           [E] :   Ages du béton
        '   PsiL            [E] :   Multiplicateur de fluage
        '   lGeneration1    [E] :   Indique si calculs selon génération 1 des Eurocodes
        '---------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nL As Decimal

        '--( Traitement

        If lGeneration1 Then
            nL = CoefficientEquivalenceG1(RH, h0, t, t0, PsiL)
        Else
            nL = CoefficientEquivalenceG2(RH, h0, t, t0, PsiL)
        End If

        Return nL
    End Function


#End Region

#Region " Coef Equivalence Génération 2 "

    Private Function CoefficientEquivalenceG2(RH As Decimal, h0 As Decimal, t As Decimal, t0 As Decimal, PsiL As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   17/05/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient d'équivalence acier béton, pour la génération 2 des EN
        '---------------------------------------------------------------------------------------------------------------
        '   RH              [E] :   Taux d'humidité relative
        '   h0              [E] :   Rayon moyen de l'élément en béton
        '   t, t0           [E] :   Ages du béton
        '   PsiL            [E] :   Multiplicateur de fluage
        '---------------------------------------------------------------------------------------------------------------

        '--( Déclaraation

        Dim n0, nL As Decimal
        Dim Phi_tt0 As Decimal
        Dim pBetaBcFcm As Decimal
        Dim pBetaDcFcm As Decimal
        Dim pBetaBcT As Decimal
        Dim pBetaDcRH As Decimal
        Dim pBetaDcT0 As Decimal
        Dim pBetaDcTT0 As Decimal
        Dim t0Adj As Decimal

        Dim PhiBc As Decimal
        Dim PhiDc As Decimal

        '--> Initialisation

        Me.Calcul_Proprietes(False)
        t0Adj = Me.AgeAjuste(t0)

        '--( Calcul

        n0 = cls_Acier.EYACIER / Ecm

        pBetaBcFcm = Me.BetaBcFcm
        pBetaDcFcm = Me.BetaDcFcm
        pBetaBcT = Me.BetaBcT(t, t0, t0Adj)
        pBetaDcRH = Me.BetaDcRH(RH, h0)
        pBetaDcT0 = Me.BetaDcT0(t0Adj)
        pBetaDcTT0 = Me.BetaDcTT0(h0, t, t0, t0Adj)

        PhiBc = pBetaBcFcm * pBetaBcT
        PhiDc = pBetaDcFcm * pBetaDcRH * pBetaDcT0 * pBetaDcTT0

        Phi_tt0 = PhiBc + PhiDc

        nL = n0 * (1 + PsiL * Phi_tt0)

        Return nL
    End Function

    Public Function BetaDcTT0(h0 As Decimal, t As Decimal, t0 As Decimal, t0Adj As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient BetaDc,t-t0,
        '   D'après formule (B.13) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Dim DeltaT As Decimal = t - t0
        Dim pBetaH As Decimal = Me.BetaHG2(h0)
        Dim GammaT As Decimal = GammaT0(t0Adj)

        Return (DeltaT / (pBetaH + DeltaT)) ^ gammat

    End Function

    Private Function GammaT0(t0Adj As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient GammaT,
        '   D'après formule (B.14) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Return 1 / (2.3 + 3.5 / Math.Sqrt(t0Adj))

    End Function

    Public Function BetaHG2(h0 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient BetaH,
        '   D'après formule (B.15) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Dim pAlphaCM As Decimal = Me.AlphaCM

        Return Math.Min(1500 * pAlphaCM, 1.5 * h0 + 250 * pAlphaCM)

    End Function

    Private Function AlphaCM() As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient AlphaFCM,
        '   D'après formule (B.16) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Return (35 / Me.Fcm) ^ 0.5
    End Function

    Public Function BetaDcT0(T0 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta Dc t0,
        '   D'après formule (B.12) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Return 1 / (1 + T0 ^ 0.2)
    End Function

    Public Function BetaDcRH(RH As Decimal, h0 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta Dc Fcm,
        '   D'après formule (B.11) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Dim h0MM As Decimal = h0 * kUnitMM


        Return (1 - RH / 100) / (0.1 * h0MM / 100) ^ (1 / 3)

    End Function

    Public Function BetaDcFcm() As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta Dc Fcm,
        '   D'après formule (B.10) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Return (412 / (Me.Fcm) ^ 1.4)

    End Function

    Public Function BetaBcT(t As Decimal, t0 As Decimal, tAdj As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta Bc t-t0,
        '   D'après formule (B.8) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Dim pBetaC As Decimal

        pBetaC = Math.Log((30 / tAdj + 0.035) ^ 2 * (t - t0) + 1)

        Return pBetaC

    End Function

    Public Function BetaBcFcm() As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta Bc Fcm,
        '   D'après formule (B.7) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Return (1.8 / (Me.Fcm) ^ 0.7)

    End Function

    Public Function AgeAjuste(t As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Age ajusté du béton,
        '   D'après formule (B.17) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Dim t0ad As Decimal
        Dim pAlphaSC As Decimal = Me.AlphaSc

        t0ad = t * (9 / (2 + t ^ 1.2) + 1) ^ pAlphaSC

        Return Math.Max(0.5, t0ad)

    End Function

    Public Function AlphaSc() As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Paramètre AlphaSc,
        '   D'après formule B.5 (4) de l'EN 1992-1-1: 2025
        '---------------------------------------------------------------------------------------------------------------

        Dim pAlpha As Decimal

        Select Case Me.Ciment
            Case TabClasseCiment(0) : pAlpha = -1
            Case TabClasseCiment(1) : pAlpha = 0
            Case TabClasseCiment(2) : pAlpha = 1
        End Select

        Return palpha

    End Function

#End Region

#Region " Coef Equivalence Génération 1 "

    Private Function CoefficientEquivalenceG1(RH As Decimal, h0 As Decimal, t As Decimal, t0 As Decimal, PsiL As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   17/05/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient d'équivalence acier béton, pour la génération 1 des EN
        '---------------------------------------------------------------------------------------------------------------
        '   RH              [E] :   Taux d'humidité relative
        '   h0              [E] :   Rayon moyen de l'élément en béton
        '   t, t0           [E] :   Ages du béton
        '   PsiL            [E] :   Multiplicateur de fluage
        '---------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim n0 As Decimal
        Dim Phi_tt0 As Decimal
        Dim MyBetaC_tt0 As Decimal
        Dim Phi0 As Decimal
        Dim MyPhiRH, MyBetaFcm, MyBeta_t0 As Decimal
        Dim MyBetaH As Decimal

        '--> Initialisation

        Me.Calcul_Proprietes(True)

        '--> Calculs

        n0 = cls_Acier.EYACIER / Ecm

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
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta c,
        '   D'après formule (B.7) de l'EN 1992-1-1: 2005
        '---------------------------------------------------------------------------------------------------------------


        Dim MyBetaC As Decimal

        MyBetaC = ((t - t0) / (BetaH(RH, h0) + t - t0)) ^ 0.3

        Return MyBetaC

    End Function

    Public Function BetaH(RH As Decimal, h0 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient BetaH,
        '   D'après formule (B.8a et b) de l'EN 1992-1-1: 2005
        '---------------------------------------------------------------------------------------------------------------

        Dim MyBetaH As Decimal
        Dim h0MM As Decimal = h0 * kUnitMM

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
        '   D'après formules (B.8c) de l'EN 1992-1-1: 2005
        '---------------------------------------------------------------------------------------------------------------


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
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Phi RH,
        '   D'après formules (B.3) de l'EN 1992-1-1: 2005
        '---------------------------------------------------------------------------------------------------------------

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
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta Fcm,
        '   D'après formule (B.4) de l'EN 1992-1-1: 2005
        '---------------------------------------------------------------------------------------------------------------

        Return (16.8 / Math.Sqrt(Me.Fcm))
    End Function

    Public Function Beta_t0(t0 As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------------
        '   Coefficient Beta(t0),
        '   D'après formule (B.5) de l'EN 1992-1-1: 2005
        '---------------------------------------------------------------------------------------------------------------

        Return (1 / (0.1 + t0 ^ 0.2))
    End Function


#End Region

#Region " Constructeur "

    Sub New()

        'Me.Type = Enum_TypeBeton.Normal
        Me.Classe = "C20/25"
        Me.Fck = 20
        Me.lCrackingLimitation = True
        Me.wk_max = 0.4 / 1000
        Me.RhoC = RHOCDEFAUT
        Me.lLeger = False

        Me.kE = 9500
        Me.Ciment = TabClasseCiment(1)

        Calcul_Proprietes(True)

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer la classe
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
