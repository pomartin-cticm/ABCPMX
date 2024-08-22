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

#Region " Calcul des armatures anti-fissuration "

    Public Function CoefficientKc(myBeam As cls_Poutre, lAppGauche As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Renvoie la valeur du coefficient kc selon formule (7.2), selon § 7.4.2 de l'EN 1994-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E]
        '   lAppGauche  [E] :   Indique si calcul sur l'appui gauche
        '----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myKc As Decimal
        Dim Hc, z0 As Decimal
        Dim NeqEnrob, NeqDalle As Decimal
        Dim bEff As Decimal
        Dim zANE As Decimal
        Dim InertieY As Decimal

        '--( Calcul

        If lAppGauche Then
            bEff = myBeam.BeffDalle(0, 1, myBeam.Param.lLargeurEfficaceSimplifiee, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
        Else
            bEff = myBeam.BeffDalle(myBeam.LongueurTravee(1), 1, myBeam.Param.lLargeurEfficaceSimplifiee, True, cls_Poutre.EnuTypeLargeurParticipante.Totale)
        End If
        NeqEnrob = myBeam.Section.Enrobage.Beton.CoefficientEquivalenceCT
        NeqDalle = myBeam.Dalle.beton.CoefficientEquivalenceCT

        Hc = myBeam.Dalle.EpaisseurActive
        InertieY = myBeam.Section.InertieYY(1, True, myBeam.Param.Gamma, NeqEnrob, NeqDalle, True, bEff, myBeam.Dalle, zANE)

        z0 = myBeam.Dalle.zTop - Hc / 2 - zANE

        myKc = Math.Min(1, 1 / (1 + Hc / (2 * z0)) + 0.3)

        Return myKc

    End Function

    Public Function ExEspacementMaxFromTableau72(Wk As Decimal, mySigma As Decimal, ByRef lOK As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcule l'espacement maxi à partir de la contrainte en utilisant le tableau 7.2 de l'EN 1994-1-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   Wk          [E] :   Ouverture de fissure
        '   mySigma     [E] :   Contrainte de traction dans les armatures
        '   lOK         [S] :   Indique si le diamètre est dans le domaine d'application du tableau
        '----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim EspM As Decimal = 0
        Dim tabSigma() As Decimal = Nothing
        Dim tabEsp() As Decimal = Nothing
        Dim nbVal As Integer

        '--( Initialisation du tableau

        Me.InitialiseTableau72(Wk, tabSigma, tabesp, nbVal)

        '--( Calcul

        ExtraireValMaxTableau71or72(tabSigma, tabEsp, nbVal, mySigma, EspM, lOK)

        Return EspM
    End Function

    Public Function ExDiametreMaxFromTableau71(Wk As Decimal, mySigma As Decimal, ByRef lOK As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcule le diamètre maxi à partir de la contraintee en utilisant le tableau 7.1 de l'EN 1994-1-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   Wk          [E] :   Ouverture de fissure
        '   mySigma     [E] :   Contrainte de traction dans les armatures
        '   lOK         [S] :   Indique si le diamètre est dans le domaine d'application du tableau
        '----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim DiaM As Decimal = 0
        Dim tabSigma() As Decimal = Nothing
        Dim tabDiam() As Decimal = Nothing
        Dim nbVal As Integer

        '--( Initialisation du tableau

        Me.InitialiseTableau71(Wk, tabSigma, tabDiam, nbVal)

        '--( Calcul

        ExtraireValMaxTableau71or72(tabSigma, tabDiam, nbVal, mySigma, DiaM, lOK)

        Return DiaM

    End Function

    Public Function ExContrainteFromTableau71(Wk As Decimal, myDia As Decimal, ByRef lOK As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcule la contrainte à partir du diamètre en utilisant le tableau 7.1 de l'EN 1994-1-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   Wk          [E] :   Ouverture de fissure
        '   myDia       [E] :   Diamètre des armatures
        '   lOK         [S] :   Indique si le diamètre est dans le domaine d'application du tableau
        '----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim SigmaS As Decimal
        Dim tabSigma() As Decimal = Nothing
        Dim tabDiam() As Decimal = Nothing
        Dim nbVal As Integer

        '--( Initialisation du tableau

        Me.InitialiseTableau71(Wk, tabSigma, tabDiam, nbVal)

        '--( Calcul

        Me.ExtraireSigmaTableau71(tabSigma, tabDiam, nbVal, myDia, SigmaS, lOK)

        Return SigmaS

    End Function

    Private Sub ExtraireValMaxTableau71or72(tabSigma() As Decimal, tabVal() As Decimal, nbVal As Integer, mySigma As Decimal, ByRef ValM As Decimal, ByRef lOK As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcule le diamètre maxi à partir de la contrainte en utilisant le tableau 7.1 de l'EN 1994-1-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   tabSigma    [E] :   Colonne des contraintes du tableau 7.1 ou 7.2
        '   tabVal      [E] :   Colonne des diamètres du tableau 7.1 ou espacement du tableau 7.2
        '   nbVal       [E] :   nombre de valeurs dans le tableau
        '   ValM        [S] :   Diamètre ou espacement max des armatures dans la dalle (obtenu à partir de la contrainte)
        '   mySigma     [E] :   Contrainte à considérer
        '   lOK         [S] :   Indique si le diamètre est dans le domaine d'application du tableau
        '----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lCont As Boolean
        Dim i As Integer

        '--( Initialisations

        lOK = False
        i = -1
        lCont = True

        Do While lCont

            i += 1
            If IsSmallerOrEqual(mySigma, tabSigma(i)) Then
                lOK = True
                ValM = tabVal(i)
            End If

            lCont = (i < nbVal - 1) And Not lOK
        Loop

        If lCont Then ValM = -1

    End Sub

    Private Sub ExtraireSigmaTableau71(tabSigma() As Decimal, tabDia() As Decimal, nbVal As Integer, myDia As Decimal, ByRef SigmaS As Decimal, ByRef lOK As Boolean)
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcule la contrainte à partir du diamètre en utilisant le tableau 7.1 de l'EN 1994-1-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   tabSigma    [E] :   Colonne des diamètres du tableau 7.1
        '   tabDia      [E] :   Colonne des diamètres du tableau 7.1
        '   nbVal       [E] :   nombre de valeurs dans le tableau
        '   myDia       [E] :   Diamètre des armatures dans la dalle (à partir duquel on extrait la contrainte)
        '   SigmaS      [S] :   Contrainte à considérer
        '   lOK         [S] :   Indique si le diamètre est dans le domaine d'application du tableau
        '----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lCont As Boolean
        Dim i As Integer

        '--( Initialisations

        lOK = True

        '--( Traitement

        If IsGreater(myDia, tabDia(0)) Then
            lOK = False
            SigmaS = tabSigma(0)
        ElseIf IsSmallerOrEqual(myDia, tabDia(nbVal - 1)) Then
            SigmaS = tabSigma(nbVal - 1)
        Else
            i = 0
            lCont = Not (IsSmaller(myDia, tabDia(i + 1)))
            Do While lCont And (i < nbVal - 2)
                i += 1
                lCont = Not (IsSmaller(myDia, tabDia(i + 1)))
            Loop

            SigmaS = tabSigma(i) + (tabSigma(i + 1) - tabSigma(i)) / (tabDia(i + 1) - tabDia(i)) * (myDia - tabDia(i))
        End If

    End Sub

    Private Sub InitialiseTableau71(Wk As Decimal, ByRef Sigma() As Decimal, ByRef Diametre() As Decimal, ByRef nbVal As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialise le tableau 7.1 de l'EN 1994-1-1:2005 
        '----------------------------------------------------------------------------------------------------------
        '   Wk          [E] :   Ouverture de fissure
        '   Sigma       [S] :   Colonne des contraintes
        '   Diametre    [S] :   Colonne des diamètres
        '   nbVal       [S] :   nombre de valeurs dans le tableau
        '----------------------------------------------------------------------------------------------------------

        If IsEqual(Wk, cls_OptionsCalcul.tabWk(0)) Then
            Sigma = {160, 200, 240, 280, 320, 360, 400, 450}
            Diametre = {40, 32, 20, 16, 12, 10, 8, 6}
        ElseIf IsEqual(Wk, cls_OptionsCalcul.tabWk(1)) Then
            Sigma = {160, 200, 240, 280, 320, 360, 400, 450}
            Diametre = {32, 25, 16, 12, 10, 8, 6, 5}
        ElseIf IsEqual(Wk, cls_OptionsCalcul.tabWk(2)) Then
            Sigma = {160, 200, 240, 280, 320, 360}
            Diametre = {25, 16, 12, 8, 6, 5, 4}
        End If

        nbVal = Diametre.GetUpperBound(0) + 1
        For i As Integer = 0 To nbVal - 1
            Diametre(i) = Diametre(i) / 1000
        Next

    End Sub

    Private Sub InitialiseTableau72(Wk As Decimal, ByRef Sigma() As Decimal, ByRef Esp() As Decimal, ByRef nbVal As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialise le tableau 7.2 de l'EN 1994-1-1:2005 
        '----------------------------------------------------------------------------------------------------------
        '   Wk          [E] :   Ouverture de fissure
        '   Sigma       [S] :   Colonne des contraintes
        '   Esp         [S] :   Colonne des espacements de barres
        '   nbVal       [S] :   nombre de valeurs dans le tableau
        '----------------------------------------------------------------------------------------------------------

        If IsEqual(Wk, cls_OptionsCalcul.tabWk(0)) Then
            Sigma = {160, 200, 240, 280, 320, 360}
            Esp = {300, 300, 250, 200, 150, 100}
        ElseIf IsEqual(Wk, cls_OptionsCalcul.tabWk(1)) Then
            Sigma = {160, 200, 240, 280, 320, 360}
            Esp = {300, 250, 200, 150, 100, 50}
        ElseIf IsEqual(Wk, cls_OptionsCalcul.tabWk(2)) Then
            Sigma = {160, 200, 240, 280}
            Esp = {200, 150, 100, 50}
        End If

        nbVal = Esp.GetUpperBound(0) + 1
        For i As Integer = 0 To nbVal - 1
            Esp(i) = Esp(i) / 1000
        Next

    End Sub

#End Region

#Region " Coefficient Beta pour moment plastique positif "

    Public Function BetaFactor1(zpl As Decimal, Ht As Decimal, Nuance As String, ByRef lOKPl As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction Beta pour le moment plastique d'une section mixte
        '   selon EN 1994-1-1:2005, 6.2.1.2 (2) 
        '----------------------------------------------------------------------------------------------------------------------
        '   zpl     [E] :   Position z de l'axe neutre
        '   Ht      [E] :   Hauteur totale de la section
        '   Nuance  [E] :   Nuance de l'acier
        '   lOKPl   [S] :   Indique si on peut faire du calcul plastique
        '----------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myBeta As Decimal
        Dim RatioZsurH As Decimal
        Dim pNuanceFy As String

        '--( Initialisation

        lOKPl = True
        RatioZsurH = zpl / Ht
        pNuanceFy = Me.NuanceFY(Nuance)

        '--( Calcul

        Select Case pNuanceFy
            Case "420", "460"
                If IsSmallerOrEqual(RatioZsurH, 0.15) Then
                    myBeta = 1
                ElseIf IsSmallerOrEqual(RatioZsurH, 0.4) Then
                    myBeta = 1 - (0.15 / 0.25) * (RatioZsurH - 0.15)
                Else
                    myBeta = -1
                    lOKPl = False
                End If
            Case Else
                myBeta = 1
        End Select

        Return myBeta
    End Function

    Public Function BetaFactor2(zpl As Decimal, Ht As Decimal, Nuance As String, ByRef lOKPl As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction Beta pour le moment plastique d'une section mixte
        '   selon EN 1994-1-1:2024, Figure 8.3 
        '----------------------------------------------------------------------------------------------------------------------
        '   zpl     [E] :   Position z de l'axe neutre
        '   Ht      [E] :   Hauteur totale de la section
        '   Nuance  [E] :   Nuance de l'acier
        '   lOKPl   [S] :   Indique si on peut faire du calcul plastique
        '----------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myBeta As Decimal
        Dim RatioZsurH As Decimal
        Dim Beta() As Decimal = Nothing
        Dim Alpha() As Decimal = Nothing
        Dim pNuanceFy As String

        '--( Initialisation

        lOKPl = True
        RatioZsurH = zpl / Ht
        pNuanceFy = Me.NuanceFY(Nuance)

        Select Case pNuanceFy
            Case "235"
                Beta = {1, 0.95}
            Case "275"
                Beta = {1, 0.95}
            Case "355"
                Beta = {1, 0.93}
            Case "420", "460"
                Beta = {1, 0.9}
        End Select

        Alpha = Me.InitialiseAlphaFactors(pNuanceFy)

        '--( Calcul

        If IsSmallerOrEqual(RatioZsurH, Alpha(0)) Then
            myBeta = 1
        ElseIf IsSmallerOrEqual(RatioZsurH, Alpha(1)) Then
            myBeta = Beta(0) - (Beta(1) - Beta(0)) / (Alpha(1) - Alpha(0)) * (RatioZsurH - Alpha(0))
        Else
            myBeta = -1
            lOKPl = False
        End If

        Return myBeta
    End Function

    Public Function ReductionFactorBeta(zpl As Decimal, Ht As Decimal, Nuance As String, lGen1 As Boolean, ByRef lOKPl As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction Beta pour le moment plastique d'une section mixte
        '   selon EN 1994-1-1:2024 (génération 2) ou EN 1994-1-1:2005 (génération 1)
        '----------------------------------------------------------------------------------------------------------------------
        '   zpl     [E] :   Position z de l'axe neutre
        '   Ht      [E] :   Hauteur totale de la section
        '   Nuance  [E] :   Nuance de l'acier
        '   lGen1   [E] :   Indique si génération 1 des EN
        '   lOKPl   [S] :   Indique si on peut faire du calcul plastique
        '----------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myBeta As Decimal

        '--( Traitement

        If lGen1 Then
            myBeta = BetaFactor1(zpl, Ht, Nuance, lOKPl)
        Else
            myBeta = BetaFactor2(zpl, Ht, Nuance, lOKPl)
        End If

        Return myBeta

    End Function

    Public Function IsBetaApplicable(Nuance As String, lGen1 As Boolean) As Boolean
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Indique les cas où le cofficient Beta pour le moment plastique d'une section mixte est applicable
        '   selon EN 1994-1-1:2024 (génération 2) ou EN 1994-1-1:2005 (génération 1)
        '----------------------------------------------------------------------------------------------------------------------
        '   Nuance  [E] :   Nuance de l'acier
        '   lGen1   [E] :   Indique si génération 1 des EN
        '----------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lApp As Boolean = True

        '--( Traitement

        If lGen1 Then
            Select Case Me.NuanceFY(Nuance)
                Case "235", "275", "355" : lApp = False
            End Select
        End If

        Return lApp

    End Function

    Public Function LimiteZsurHplastic(Nuance As String, lGen1 As Boolean) As Decimal
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Renvoie la limite de z / H pour pouvoir effectuer un calcul plastique
        '   selon EN 1994-1-1:2024 (génération 2) ou EN 1994-1-1:2005 (génération 1)
        '----------------------------------------------------------------------------------------------------------------------
        '   Nuance  [E] :   Nuance de l'acier
        '   lGen1   [E] :   Indique si génération 1 des EN
        '----------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myLim As Decimal
        Dim Alpha() As Decimal = Nothing

        '--( Traitement

        If lGen1 Then
            myLim = 0.4
        Else
            Alpha = Me.InitialiseAlphaFactors(Me.NuanceFY(Nuance))

            myLim = Alpha(1)
        End If

        Return myLim
    End Function

    Private Function InitialiseAlphaFactors(NuanceLoc As String) As Decimal()
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Initialisation du tableau des alpha permettant de décrire les morceaux de la courbe Beta
        '   Alpha(0) donne la valeur de z sur H maxi pour Beta = 1
        '   Alpha(1) donne la valeur de z sur H maxi pour le calcul plastique
        '   selon EN 1994-1-1:2024 (génération 2) 
        '----------------------------------------------------------------------------------------------------------------------
        '   NuanceLoc   [E] :   Nuance de l'acier
        '----------------------------------------------------------------------------------------------------------------------

        Dim Alpha() As Decimal = Nothing

        Select Case NuanceLoc
            Case "235"
                Alpha = {0.2, 0.6}
            Case "275"
                Alpha = {0.2, 0.5}
            Case "355"
                Alpha = {0.2, 0.45}
            Case "420", "460"
                Alpha = {0.15, 0.4}
        End Select

        Return Alpha
    End Function

    Private Function NuanceFY(Nuance As String) As String
        '----------------------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Retourne la nuance constituée uniquement des caractères indiquant la limite d'élasticité
        '   Permet d'identifier les nuances HISTAR
        '----------------------------------------------------------------------------------------------------------------------
        '   Nuance  [E] :   Nuance de l'acier
        '----------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim TabNuanceFY() As String = {"235", "275", "355", "420", "460"}
        Dim lCont As Boolean
        Dim iGrade As Integer
        Dim nbN As Integer = TabNuanceFY.GetUpperBound(0) + 1
        Dim myNuance As String = ""

        '--( Traitement

        iGrade = 0
        lCont = (Not Nuance.Contains(TabNuanceFY(iGrade))) And (iGrade + 1 < nbN)

        Do While lCont
            iGrade += 1
            lCont = (Not Nuance.Contains(TabNuanceFY(iGrade))) And (iGrade + 1 < nbN)
        Loop

        If lCont Then
            GestionErreur("cls_Eurocodes", "NuanceFY", "Cannot find grade " & Nuance)
        Else
            myNuance = TabNuanceFY(iGrade)
        End If

        Return myNuance
    End Function

#End Region

End Class
