'####################################################################################################################################
'# FONCTIONS POUR LE CALCUL DES SLIMS FLOORS (ACIER OU MIXTES)
'####################################################################################################################################


Public Class cls_CalculSlim

#Region " Calcul des coefficients de flexion locale  "

    Public Sub CalculCoefficientsReduction(iCombi As Integer, myPoutre As cls_Poutre, QEd() As Decimal,
                                           ByRef PsiAfi(,) As Decimal, ByRef RhoTfi(,) As Decimal, ByRef PsiYfi(,) As Decimal,
                                           ByRef PsiAspd(,) As Decimal, ByRef RhoTspd(,) As Decimal, ByRef PsiYspd(,) As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul des coefficients de réduction liés à la flexion transversale des semelles inférieures de slim
        '-----------------------------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison
        '   myPoutre    [E] :   Poutre étudiée
        '   QEd         [E] :   Efforts nodaux de la poutre
        '
        '   PsiYfi      [S] :   Réduction de la limite d'élasticité pour la semelle inférieure, tenant compte de la flexion locale
        '   PsiYspd     [S] :   Réduction de la limite d'élasticité pour le plat inférieur, tenant compte de la flexion locale
        '
        '   PsiAfi      [S] :   Réduction de l'aire de la semelle inférieure, tenant compte de la flexion locale
        '   PsiAspd     [S] :   Réduction de l'aire du plat inférieur, tenant compte de la flexion locale
        '
        '   PhoTfi      [S] :   Réduction de l'épaisseur de la semelle inférieure, tenant compte de la flexion locale
        '   PhoTspd     [S] :   Réduction de l'épaisseur du plat inférieur, tenant compte de la flexion locale
        '-----------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim deltaX As Decimal = myPoutre.LongueurTotale / myPoutre.Nodes.nbNodes

        Dim q, dApp, GammaM0 As Decimal
        Dim dbtFi, dbtPlat As Decimal

        Dim FyPlat, fyInf As Decimal

        Dim kChargeQ As Decimal

        Dim EN1994 As New cls_Eurocodes

        '--> Initialisation

        iDebT = myPoutre.IndicePremiereTravee
        iFinT = myPoutre.IndiceDerniereTravee

        GammaM0 = myPoutre.Param.Gamma.GammaM0

        dApp = myPoutre.SlimLargeurAppui
        'Me.BrasLevier(myPoutre, dApp, dbtFi, dbtPlat)
        myPoutre.Section.SlimBrasLevier(dApp, dbtFi, dbtPlat)

        FyPlat = myPoutre.Section.FySpd
        fyInf = myPoutre.Section.FyInf

        kChargeQ = Me.CoefficientCharge(myPoutre)

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myPoutre.Section.ProfilA

                    '== Suggestion pour DeltaX (POM)
                    If iNode = 0 Then
                        deltaX = (myPoutre.Nodes.xGlobal(iNode + 1) - myPoutre.Nodes.xGlobal(iNode)) / 2
                    ElseIf iNode = myPoutre.Nodes.nbNodes - 1 Then
                        deltaX = (myPoutre.Nodes.xGlobal(iNode) - myPoutre.Nodes.xGlobal(iNode - 1)) / 2
                    Else
                        deltaX = (myPoutre.Nodes.xGlobal(iNode + 1) - myPoutre.Nodes.xGlobal(iNode - 1)) / 2
                    End If
                    '====

                    q = kChargeQ * QEd(iNode) / deltaX

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            Select Case myPoutre.Param.MethodReducPlatSlim
                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire
                                    PsiAspd(iCombi, iNode) = CalculPsi(q, .Plat_b, .Plat_t, FyPlat, .Plat_b - 2 * dApp, .Bfi, GammaM0)
                                    PsiAfi(iCombi, iNode) = CalculPsi(q, .Bfi, .Tfi, fyInf, .Bfi, .Tw, GammaM0)

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M2_ReducEpaisseur

                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = CalculRhot(q, dbtPlat, .Plat_t, FyPlat, GammaM0)
                                    RhoTfi(iCombi, iNode) = CalculRhot(q, dbtFi, .Tfi, fyInf, GammaM0)

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M3_ReducLimiteElasticite      ' C'est l'option choisie par l'EN 1994-1-1 - G2

                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = EN1994.SlimCalculPsiY(q, dbtPlat, .Plat_t, FyPlat, GammaM0)
                                    PsiYfi(iCombi, iNode) = EN1994.SlimCalculPsiY(q, dbtFi, .Tfi, fyInf, GammaM0)

                            End Select

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            Select Case myPoutre.Param.MethodReducPlatSlim
                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire
                                    PsiAspd(iCombi, iNode) = Me.CalculPsi(q, .Plat_b, .Plat_t, FyPlat, .Plat_b - 2 * dApp, .Tw, GammaM0)
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M2_ReducEpaisseur

                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = Me.CalculRhot(q, dbtPlat, .Plat_t, FyPlat, GammaM0)
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M3_ReducLimiteElasticite
                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = EN1994.SlimCalculPsiY(q, dbtPlat, .Plat_t, FyPlat, GammaM0)
                                    PsiYfi(iCombi, iNode) = 1

                            End Select

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            Select Case myPoutre.Param.MethodReducPlatSlim
                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M1_ReducAire
                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = Me.CalculPsi(q, .Bfi, .Tfi, fyInf, .Bfi - 2 * dApp, .Tw, GammaM0)

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M2_ReducEpaisseur

                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = Me.CalculRhot(q, dbtFi, .Tfi, fyInf, GammaM0)

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = 1

                                Case cls_OptionsCalcul.Enu_MReducPlatSlim.M3_ReducLimiteElasticite
                                    PsiAspd(iCombi, iNode) = 1
                                    PsiAfi(iCombi, iNode) = 1

                                    RhoTspd(iCombi, iNode) = 1
                                    RhoTfi(iCombi, iNode) = 1

                                    PsiYspd(iCombi, iNode) = 1
                                    PsiYfi(iCombi, iNode) = EN1994.SlimCalculPsiY(q, dbtFi, .Tfi, fyInf, GammaM0)

                            End Select

                    End Select

                End With

            Next
        Next

    End Sub

#End Region

#Region " Coefficients de réduction pour le plat inférieur, en situation d'incendie "

    Public Sub CalculCoefficientsReductionFeu(iCombi As Integer, myBeam As cls_Poutre, NbStep As Integer, QEd() As Decimal,
                                              TempPl()() As Decimal, TempFi()() As Decimal,
                                              ByRef PsiY_spd(,) As Decimal, ByRef PsiY_fi(,) As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul des coefficients de réduction liés à la flexion transversale des semelles inférieures de slim
        '   UNIQUEMENT AVEC LA METHODE EN1994
        '-----------------------------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison
        '   myBeam      [E] :   Poutre étudiée
        '   NbStep      [E] :   Nombre de durées incendie calculées
        '   QEd         [E] :   Efforts nodaux de la poutre
        '   TempPl      [E] :   Températures du plat inférieur
        '   TempFi      [E] :   Températures de la semelle inférieure
        '   PsiY_spd    [S] :   Coefficient de réduction pour la limite d'élasticité du plat
        '   PsiY_fi     [S] :   Coefficient de réduction pour la limite d'élasticité de la semelle inférieure
        '-----------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iStep As Integer
        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim deltaX As Decimal = myBeam.LongueurTotale / myBeam.Nodes.nbNodes

        Dim q, dApp, GammaM0 As Decimal
        Dim dbtFi, dbtPlat As Decimal

        Dim FyPlat, fyInf As Decimal

        Dim kChargeQ As Decimal

        Dim EN1994 As New cls_Eurocodes
        Dim ENFeu As New cls_EurocodesFeu

        Dim kReducPl(NbStep - 1) As Decimal
        Dim kReducFi(NbStep - 1) As Decimal

        '--> Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = myBeam.SlimLargeurAppui
        myBeam.Section.SlimBrasLevier(dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        fyInf = myBeam.Section.FyInf

        kChargeQ = CoefficientCharge(myBeam)

        ReDim PsiY_fi(NbStep - 1, myBeam.Nodes.nbNodes - 1)
        ReDim PsiY_spd(NbStep - 1, myBeam.Nodes.nbNodes - 1)

        For iStep = 0 To NbStep - 1
            kReducPl(iStep) = ENFeu.ReducFyAcier(TempPl(iStep)(1))
            kReducFi(iStep) = ENFeu.ReducFyAcier(TempFi(iStep)(1))
        Next

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    '== Suggestion pour DeltaX (POM)
                    If iNode = 0 Then
                        deltaX = (myBeam.Nodes.xGlobal(iNode + 1) - myBeam.Nodes.xGlobal(iNode)) / 2
                    ElseIf iNode = myBeam.Nodes.nbNodes - 1 Then
                        deltaX = (myBeam.Nodes.xGlobal(iNode) - myBeam.Nodes.xGlobal(iNode - 1)) / 2
                    Else
                        deltaX = (myBeam.Nodes.xGlobal(iNode + 1) - myBeam.Nodes.xGlobal(iNode - 1)) / 2
                    End If
                    '====

                    q = kChargeQ * QEd(iNode) / deltaX

                    For iStep = 0 To NbStep - 1
                        Select Case .typeProfileAcier
                            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                                PsiY_spd(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtPlat, .Plat_t, kReducPl(iStep) * FyPlat, GammaM0)
                                PsiY_fi(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtFi, .Tfi, kReducFi(iStep) * fyInf, GammaM0)

                            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                                PsiY_spd(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtPlat, .Plat_t, kReducPl(iStep) * FyPlat, GammaM0)
                                PsiY_fi(iStep, iNode) = 1

                            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                                PsiY_spd(iStep, iNode) = 1
                                PsiY_fi(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtFi, .Tfi, kReducFi(iStep) * fyInf, GammaM0)

                        End Select
                    Next

                End With

            Next
        Next

    End Sub

#End Region

#Region " Fonctions de base "

    ''' <summary>
    ''' Fonction qui calcul le coefficient de réduction de l'aire du plat inférieur
    ''' </summary>
    ''' <param name="q">charge linéique</param>
    ''' <param name="b">largeur du plat</param>
    ''' <param name="t">épaisseur du plat</param>
    ''' <param name="fy">nuance d'acier du plat</param>
    ''' <param name="e1">distance entre les charges appliquées</param>
    ''' <param name="e2">distance entre les appuis</param>
    ''' <param name="gammaM0">coefficient partiel gammaM0</param>
    ''' <returns></returns>
    Private Function CalculPsi(q As Decimal, b As Decimal, t As Decimal, fy As Decimal,
                               e1 As Decimal, e2 As Decimal, gammaM0 As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '   01/01/24 : Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction de la limite d'élasticité du plat support de slim floor
        '   d'après COSFB technical specifications
        '------------------------------------------------------------------------------------------------------------------
        '   q       [E] :   Charge sur le plat
        '   b       [E] :
        '   fy      [E] :   Limite d'élasticité 
        '   t       [E] :   Epaisseur du plat
        '   GammaM0 [E] :   Coefficient partiel GammaM0
        '------------------------------------------------------------------------------------------------------------------

        Dim psi, mu, lambda As Decimal

        mu = (e1 - e2) * q * gammaM0 / (t ^ 2 * fy * kConvMPaPa)

        lambda = 1 - Math.Sqrt(1 - mu)

        'psi = 1 - (mu ^ 2 * t * 3 * Math.Sqrt(3) + lambda * mu * (2 * e1 + e2) - lambda ^ 2 * (e1 - e2)) / (6 * mu * b)
        psi = 1 - (mu * t * 3 * Math.Sqrt(3) + lambda * (2 * e1 + e2)) / (6 * b) - lambda ^ 2 * (e1 - e2) / (6 * mu * b)

        psi = Math.Max(psi, 0) 'minoration par 0 au cas où
        psi = Math.Min(psi, 1) 'majoration par 1 au cas où

        Return psi
    End Function

    ''' <summary>
    ''' Fonction qui calcul le coefficient de réduction de l'épaisseur du plat inférieur
    ''' </summary>
    ''' <param name="q">charge linéique</param>
    ''' <param name="dbt">bras de levier de la charge</param>
    ''' <param name="t">épaisseur du plat inférieur</param>
    ''' <param name="fy">nuance d'acier du plat inférieur</param>
    ''' <param name="gammaM0">coefficient partiel gammaM0</param>
    ''' <returns></returns>
    Private Function CalculRhot(q As Decimal, dbt As Decimal, t As Decimal, fy As Decimal, gammaM0 As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '   01/01/24 : Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du coefficient de réduction de la limite d'élasticité du plat support de slim floor
        '   d'après COSFB technical specifications
        '------------------------------------------------------------------------------------------------------------------
        '   q       [E] :   Charge sur le plat
        '   dbt     [E] :   Bras de levier de la charge
        '   t       [E] :   Epaisseur du plat
        '   GammaM0 [E] :   Coefficient partiel GammaM0
        '------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim mxbt_Ed, mxbt_Rd, eta_m, rho_t As Decimal

        '--( Initialisation

        mxbt_Ed = q * dbt
        mxbt_Rd = 1.2 * t ^ 2 * fy / (6 * gammaM0) * kConvMPaPa

        '--( Calcul

        eta_m = Math.Min(Math.Abs(mxbt_Ed / mxbt_Rd), 1)

        rho_t = (1 / 2) * (1 + Math.Sqrt(1 - eta_m))

        Return rho_t
    End Function

    Public Function CoefficientCharge(myBeam As cls_Poutre) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   10/10/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Renvoie la proportion de charge reprise de part et d'autre
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '----------------------------------------------------------------------------------------------------------

        Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        dGauche = myBeam.EntraxeD1
        dDroite = myBeam.EntraxeD2
        If myBeam.lIntermediaire Then
            kCote = Math.Max(dGauche, dDroite) / (dGauche + dDroite)
        Else
            kCote = Math.Max(2 * dGauche, dDroite) / (2 * dGauche + dDroite)
        End If

        Return kCote

    End Function

#End Region

#Region " Vérifications flexion locale "

    Public Sub RunCritereResistancePlastiquePlatY_N(myBeam As cls_Poutre, iCombi As Integer, qsupEd() As Decimal, ByRef myCritereMy As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   01/08/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance élasto-plastique des plats supports
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   qsupEd      [E] :   Charge répartie linéique agissant DES 2 COTES
        '   myCritereMy [S] :   Critère de résistance à la flexion locale
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iNode As Integer
        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        'Dim DeltaX As Decimal
        Dim qLin As Decimal
        Dim myFiEd, myFiRd As Decimal
        Dim myPlEd, myPlRd As Decimal
        Dim GammaM0 As Decimal
        Dim FyPlat, FyInf As Decimal
        Dim dApp, dbtFi, dbtPlat As Decimal
        Const kPlast As Decimal = 1.2
        Dim tPl, tFi As Decimal
        '  Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        '--( Initialisation

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = myBeam.SlimLargeurAppui
        myBeam.Section.SlimBrasLevier(dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        FyInf = myBeam.Section.FyInf

        tFi = myBeam.Section.ProfilA.Tfi
        tPl = myBeam.Section.ProfilA.Plat_t

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        kCote = Me.CoefficientCharge(myBeam)

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    qLin = qsupEd(iNode) * kCote

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            myFiEd = qLin * dbtFi
                            myFiRd = kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            myCritereMy.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            myCritereMy.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            myPlEd = qLin * dbtPlat
                            myPlRd = kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            myCritereMy.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            myFiEd = qLin * dbtFi
                            myFiRd = kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            myCritereMy.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                    End Select

                End With

            Next
        Next

    End Sub

#End Region

#Region " Vérification résistance effort tranchant "

    Public Sub RunCritereTranchants(MyPoutre As cls_Poutre, iCombi As Integer, VEd(,) As Decimal, VRd As Decimal, ByRef myCritereV As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   10/10/23 :  Création - GUD
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance à l'effort tranchant 
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   VEd         [E] :   Table des efforts tranchants le long de la barre
        '   VRd         [E] :   Effort tranchant résistant (plastique ou voilement) de la barre
        '   myCritereV  [S] :   Critère de résistance à l'effort tranchant
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode, k As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer

        '--> Déclaration

        iDebT = MyPoutre.IndicePremiereTravee
        iFinT = MyPoutre.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN
                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK
                    myCritereV.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRd)
                Next
            Next
        Next

    End Sub

#End Region

#Region " Températures enveloppes des différentes parties du maillage "

    Public Sub TemperatureStepMinMax(Maillage As cls_MaillageSlimFloor, TempMailStep(,,) As Decimal, iStep As Integer,
                                     ByRef TempBeton() As Decimal, ByRef TempAme() As Decimal,
                                     ByRef TempSemInf() As Decimal, ByRef TempSemSup() As Decimal,
                                     ByRef TempPlat() As Decimal, ByRef TempSoud() As Decimal, ByRef TempArma() As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des températures min et max du maillage au temps iStep
        '---------------------------------------------------------------------------------------------------------
        '   Maillage    [E] :   Maillage et résultats du calcul
        '   TempMailStep[E] :   Températures dans les mailles pour tous steps de calcul (iStep, iX, iY)
        '   iStep       [E] :   Indice du pas de temps traité
        '   TempBeton   [S] :   Tableau des températures min et max des éléments béton
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iMailY As Integer
        Dim iMailZ As Integer
        Dim nbY As Integer = Maillage.nb_cells_y
        Dim nbZ As Integer = Maillage.nb_cells_z

        '--( Initialisation

        ReDim TempBeton(1)
        ReDim TempAme(1)
        ReDim TempArma(1)
        ReDim TempPlat(1)
        ReDim TempSemInf(1)
        ReDim TempSemSup(1)
        ReDim TempSoud(1)

        '--( Traitement

        For iMailY = 0 To nbY - 1
            For iMailZ = 0 To nbZ - 1

                Select Case Maillage.Tab_mesh_mat(iMailY, iMailZ)
                    Case cls_MaillageSlimFloor.MATBETON

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempBeton)

                    Case cls_MaillageSlimFloor.MATACIERAME

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempAme)

                    Case cls_MaillageSlimFloor.MATACIERSEMI

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempSemInf)

                    Case cls_MaillageSlimFloor.MATACIERSEMS

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempSemSup)

                    Case cls_MaillageSlimFloor.MATACIERPLAT

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempPlat)

                    Case cls_MaillageSlimFloor.MATACIERSOUD

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempSoud)

                    Case cls_MaillageSlimFloor.MATARMA

                        TraitementTempMaille(TempMailStep, iStep, iMailY, iMailZ, TempArma)

                End Select

            Next
        Next

    End Sub

    Private Sub TraitementTempMaille(TempMailStep(,,) As Decimal, iStep As Integer, iMailY As Integer, iMailZ As Integer, ByRef TempPart() As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   29/06/26 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des températures min et max du maillage au temps iStep, pour une maille particulière
        '---------------------------------------------------------------------------------------------------------
        '   TempMailStep    [E] :   Températures dans les mailles pour tous steps de calcul (iStep, iX, iY)
        '   iStep           [E] :   Indice du pas de temps traité
        '   iMailY, iMailZ  [E] :   Indices de la maille traitée
        '   TempPart        [S] :   Tableau des températures min et max des éléments du matériau traité
        '---------------------------------------------------------------------------------------------------------

        If TempPart(0) = 0 And TempPart(1) = 0 Then
            TempPart(0) = TempMailStep(iStep, iMailY, iMailZ)
            TempPart(1) = TempMailStep(iStep, iMailY, iMailZ)
        Else
            If TempMailStep(iStep, iMailY, iMailZ) < TempPart(0) Then
                TempPart(0) = TempMailStep(iStep, iMailY, iMailZ)
            End If
            If TempMailStep(iStep, iMailY, iMailZ) > TempPart(1) Then
                TempPart(1) = TempMailStep(iStep, iMailY, iMailZ)
            End If
        End If

    End Sub

#End Region

#Region " Résistance à l'effort tranchant d'une slim floor sous incendie (à chaud) "

    Public Sub RunCritereResistanceTranchantFEU(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer,
                                                VEd(,) As Decimal, VRdFi() As Decimal, ByRef myCritVstep As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion des sections sous températures
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   VEd         [E] :   Efforts tranchants aux noeuds
        '   VRdFi       [E] :   Table des résistances à l'effort tranchant (0 à NbStep-1)
        '   myCritVstep [S] :   Critere en effort tranchant (classe)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iTravee, iNode As Integer

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    myCritVstep.EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRdFi(iStep))

                Next
            Next
        Next

    End Sub

    Public Sub CalculVbRdFeuSlimAcier(myBeam As cls_Poutre, Maillage As cls_MaillageSlimFloor, TempMailStep(,,) As Decimal, nbStep As Integer, ByRef VbRdFi() As Decimal)
        '------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul de la résistance plastique VbRd en fct de l'échauffement
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   Maillage    [E] :   Maillage de la section slim floor pour le calcul de l'échauffement
        '   TempMailStep[E] :   Températures dans les mailles pour tous steps de calcul (iStep, iX, iY)
        '   nbStep      [E] :   Nombre de durées incendie traitées
        '   VbRdFi      [S] :   Résistances plastiques VbRd en fct de la durée d'exposit
        '------------------------------------------------------------------------------

        '--( Déclarations

        Dim iStep As Integer
        Dim MailAme As New List(Of Integer())
        Dim AireAme As Decimal() = Nothing
        Dim NbW As Integer
        Dim iW As Integer
        Dim kReducY As Decimal
        Dim myTempW As Decimal
        Dim ENFeu As New cls_EurocodesFeu
        Dim RAC3 As Decimal = Math.Sqrt(3)
        Dim FyW As Decimal
        Dim GammaMFi As Decimal

        '--( Initialisations

        ReDim VbRdFi(nbStep - 1)
        ReperageMaillesAme(Maillage, MailAme)
        NbW = MailAme.Count
        CalAireMaillesAme(Maillage, MailAme, AireAme)

        GammaMFi = myBeam.Param.Gamma.GammaM_fi
        FyW = myBeam.Section.FyW

        '-- Récupération des mailles de l'âme

        '--( Calculs

        For iStep = 0 To nbStep - 1

            For iW = 0 To NbW - 1

                myTempW = TempMailStep(iStep, MailAme(iW)(0), MailAme(iW)(1))
                kReducY = ENFeu.ReducFyAcier(myTempW)

                VbRdFi(iStep) += kReducY * AireAme(iW) * FyW / RAC3 / GammaMFi * kConvMPaPa

            Next

        Next

    End Sub

    Private Sub CalAireMaillesAme(Maillage As cls_MaillageSlimFloor, mailWeb As List(Of Integer()), ByRef AireWeb() As Decimal)
        '------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des aires des mailles de l'âme
        '------------------------------------------------------------------------------
        '   Maillage    [E] :   Maillage de la section slim floor pour le calcul de l'échauffement
        '   MailWeb     [E] :   Liste des indices Y,Z des mailles de l'âme
        '   AireWeb     [S] :   Tables des aires de chacune des mailles de l'âme
        '------------------------------------------------------------------------------

        '--( Déclaration

        Dim nbMailW As Integer = mailWeb.Count
        Dim iW As Integer

        '--( Initialisation

        ReDim AireWeb(nbMailW - 1)

        '--( Traitement

        For iW = 0 To nbMailW - 1

            AireWeb(iW) = Maillage.Tab_mesh_y(mailWeb(iW)(0)) * Maillage.Tab_mesh_y(mailWeb(iW)(1))

        Next

    End Sub

    Private Sub ReperageMaillesAme(Maillage As cls_MaillageSlimFloor, ByRef MailWeb As List(Of Integer()))
        '------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Récupération des mailles de l'âme
        '------------------------------------------------------------------------------
        '   Maillage    [E] :   Maillage de la section slim floor pour le calcul de l'échauffement
        '   MailWeb     [S] :   Liste des indices Y,Z des mailles de l'âme
        '------------------------------------------------------------------------------

        '--( Déclarations

        Dim iY, iZ As Integer
        Dim Maillage_NbY As Integer = Maillage.nb_cells_y
        Dim Maillage_NbZ As Integer = Maillage.nb_cells_z

        '--( Boucles sur les mailles

        For iY = 0 To Maillage_NbY - 1
            For iZ = 0 To Maillage_NbZ - 1

                If Maillage.Tab_mesh_mat(iY, iZ) = cls_MaillageSlimFloor.MATACIERAME Then
                    'myMail = {iY, iZ}
                    'MailWeb.Add(myMail)
                    MailWeb.Add({iY, iZ})
                End If

            Next
        Next

    End Sub

#End Region

#Region " Résistance en flexion à chaud (FEU) "

    Public Sub RunCritereResistanceFlexionFEU(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer,
                                              MEd(,) As Decimal, MRd(,) As Decimal, myCritMstep As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion des sections sous incendie
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   MEd         [E] :   Moments aux noeuds
        '   MRd         [E] :   Table des résistances (istep,inode)
        '   myCritMstep [S] :   Critere en flexion (classe)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iTravee, iNode As Integer

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    myCritMstep.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRd(iStep, iNode))

                Next
            Next
        Next

    End Sub

    Public Sub ProprietesFlexionFeuSlimAcier(iCombi As Integer, myBeam As cls_Poutre, NbStep As Integer, lValRd As Boolean,
                                             ByRef MplRd(,) As Decimal, ByRef zANP(,) As Decimal,
                                             PsiY_fi(,) As Decimal, PsiY_spd(,) As Decimal)
        '------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques  le long de la barre en fonction de 
        '   du chargement et de la température
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   indice de la combi en cours 
        '   NbStep      [E] :   Nombre de durées incendie à traiter
        '   lValRd      [E] :   indique si valeur de calcul (True) ou non (False)
        '   MplRd       [E] :   Moment plastique résistant
        '   zANP        [E] :   Axe neutre plastique
        '   Psi_fi      [E] :   Tables de réduction de fy pour la semelle inf (tenant compte de la flexion transversale)
        '   Psi_spd     [E] :   Tables de réduction de fy pour le plat (tenant compte de la flexion transversale)
        '------------------------------------------------------------------------------

        '--> Déclaration

        'Dim myModele As cls_ModeleP

        Dim NbNodes As Integer = myBeam.Nodes.nbNodes
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer       ' Par principe, en fait toutes les poutres sont sans consoles
        Dim iNode As Integer
        Dim iNodeDeb, iNodeFin As Integer
        Dim kDeb, kfin As Integer
        Dim lEdge As Boolean = Not myBeam.lIntermediaire
        Dim lCalcul As Boolean

        '--> Initialisation

        iTravDeb = myBeam.IndicePremiereTravee
        iTravFin = myBeam.IndiceDerniereTravee

        ReDim zANP(NbStep - 1, NbNodes - 1)
        ReDim MplRd(NbStep - 1, NbNodes - 1)

        '--> Traitement

        For iStep As Integer = 0 To NbStep - 1

            For iTravee = iTravDeb To iTravFin

                iNodeDeb = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
                iNodeFin = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

                For iNode = iNodeDeb To iNodeFin
                    If iNode = iNodeDeb Then kDeb = 1 Else kDeb = 0
                    If iNode = iNodeFin Then kfin = 0 Else kfin = 1

                    If iNode = iNodeDeb Then
                        lCalcul = True
                    Else

                        lCalcul = (Not IsEqual(PsiY_fi(iStep, iNode), PsiY_fi(iStep, iNode - 1))) _
                              Or ((Not IsEqual(PsiY_spd(iStep, iNode), PsiY_spd(iStep, iNode - 1))))

                    End If

                    If lCalcul Then

                        Me.ProprietePlastiqueMSlimFeu(myBeam, iStep, PsiY_fi(iStep, iNode), PsiY_spd(iStep, iNode),
                                                      zANP(iStep, iNode), MplRd(iStep, iNode))

                    Else

                        zANP(iStep, iNode) = zANP(iStep, iNode - 1)
                        MplRd(iStep, iNode) = MplRd(iStep, iNode - 1)

                    End If

                Next

            Next

        Next
    End Sub

    Public Sub ProprietePlastiqueMSlimFeu(myBeam As cls_Poutre, iStep As Integer, myPsiY_fi As Decimal, myPsiY_spd As Decimal,
                                          ByRef zANP As Decimal, ByRef MplRd As Decimal, Optional RhoV As Decimal = 0)
        '------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques  le long de la barre en fonction de 
        '   du chargement et de la température
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iStep       [E] :   indice du champ de température à prendre en compte
        '   MplRd       [S] :   Moment plastique résistant
        '   zANP        [S] :   Axe neutre plastique
        '   myPsi_fi    [E] :   Coef de réduction de fy pour la semelle inf (tenant compte de la flexion transversale)
        '   myPsi_spd   [E] :   Coef de réduction de fy pour le plat (tenant compte de la flexion transversale)
        '   RhoV        [E] :   Coefficient d'interaction V
        '------------------------------------------------------------------------------

        '--> Déclaration

        Dim myModele As New cls_ModeleP
        Const SIGNE As Decimal = 1
        Const lValRd As Boolean = True

        '--( Initialisation

        myModele.MaillageSlimThermique(myBeam, iStep, RhoV, myPsiY_fi, myPsiY_spd)

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(SIGNE, zANP, lValRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(SIGNE, zANP, lValRd)

    End Sub

#End Region

#Region " Résistance flexion locale à chaud "

    Public Sub RunCritereResistancePlastiquePlatY_FEU(myBeam As cls_Poutre, iCombi As Integer, qsupEd() As Decimal,
                                                      iStep As Integer, TempPl As Decimal, TempFi As Decimal, ByRef myCritMYstep As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   29/06/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance élasto-plastique des plats supports, à l'incendie
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   qsupEd      [E] :   Charge répartie linéique agissant DES 2 COTES
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   TempPl      [E] :   Température maxi dans le plat
        '   TempFi      [E] :   Température maxi dans la semelle inférieure
        '   myCritMY    [S] :   Classe gérant le critère de flexion locale
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iNode As Integer
        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        'Dim DeltaX As Decimal
        Dim qLin As Decimal
        Dim myFiEd, myFiRd As Decimal
        Dim myPlEd, myPlRd As Decimal
        Dim GammaM0 As Decimal
        Dim FyPlat, FyInf As Decimal
        Dim dApp, dbtFi, dbtPlat As Decimal
        Const kPlast As Decimal = 1.2
        Dim tPl, tFi As Decimal
        '  Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        Dim ENFeu As New cls_EurocodesFeu
        Dim CSlim As New cls_CalculSlim
        Dim kReducFi As Decimal
        Dim kReducPl As Decimal

        '--( Initialisation

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = myBeam.SlimLargeurAppui
        myBeam.Section.SlimBrasLevier(dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        FyInf = myBeam.Section.FyInf

        tFi = myBeam.Section.ProfilA.Tfi
        tPl = myBeam.Section.ProfilA.Plat_t

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        kCote = CSlim.CoefficientCharge(myBeam)

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    qLin = qsupEd(iNode) * kCote

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            kReducFi = ENFeu.ReducFyAcier(TempFi)
                            myFiEd = qLin * dbtFi
                            myFiRd = kReducFi * kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            myCritMYstep.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                            kReducPl = ENFeu.ReducFyAcier(TempPl)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kReducPl * kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            myCritMYstep.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            kReducPl = ENFeu.ReducFyAcier(TempPl)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kReducPl * kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            myCritMYstep.EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            kReducFi = ENFeu.ReducFyAcier(TempFi)

                            myFiEd = qLin * dbtFi
                            myFiRd = kReducFi * kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            myCritMYstep.EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                    End Select

                End With

            Next
        Next

    End Sub

#End Region

#Region " Vérification de la résistance interaction MV à chaud "

    Public Sub RunCritereInteractionMV_FEU(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer,
                                           MEd(,) As Decimal, VEd(,) As Decimal, VRdFi() As Decimal,
                                           PsiY_fi(,) As Decimal, PsiY_spd(,) As Decimal,
                                           myCritMVstep As cls_Critere)
        '----------------------------------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion des sections sous températures
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   MEd         [E] :   Moments aux noeuds
        '   VEd         [E] :   Efforts tranchants aux noeuds
        '   VRdFi       [E] :   Table des résistances à l'effort tranchant (0 à NbStep-1)
        '   Psi_fi      [E] :   Tables de réduction de fy pour la semelle inf (tenant compte de la flexion transversale)
        '   Psi_spd     [E] :   Tables de réduction de fy pour le plat (tenant compte de la flexion transversale)
        '   myCritMVstep[S] :   Critère MV à renseigner (classe)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iTravee, iNode As Integer
        Dim Eta As Decimal
        Dim zANP As Decimal
        Dim MplVRd As Decimal
        Dim RhoV As Decimal
        Dim CSlim As New cls_CalculSlim

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                Eta = Math.Abs(VEd(iNode, iDebK) / VRdFi(iStep))
                If iFinK > iDebK Then Eta = Math.Max(Eta, Math.Abs(VEd(iNode, iFinK) / VRdFi(iStep)))

                If IsGreater(Eta, 0.5) Then
                    RhoV = Math.Min(1, (2 * Eta - 1) ^ 2)
                Else
                    RhoV = 0
                End If

                CSlim.ProprietePlastiqueMSlimFeu(myBeam, iStep, PsiY_fi(iStep, iNode), PsiY_spd(iStep, iNode),
                                                 zANP, MplVRd, RhoV)

                myCritMVstep.EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, iDebK), MplVRd)

            Next
        Next

    End Sub

#End Region

#Region " Lecture Récupération des champs thermiques dans le fichier de données "

    Public Sub RecupereMaillage(myBeam As cls_Poutre, ByRef Maillage As cls_MaillageSlimFloor)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Regénération du maillage pour le calcul thermique
        '---------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iBeam       [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   FileNameP   [E] :   Nom du fichier de sauvegarde du projet
        '   lTemp       [E] :   Indique si les températures doivent être récupérées ou non (utile pour la méthode d'échauffement qui n'a pas besoin de récupérer les températures)
        '   Maillage    [S] :   Maillage qui est récupéré
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim bEffG, bEffD, bApp As Decimal

        '--( Initialisation des variables

        Maillage = New cls_MaillageSlimFloor

        ParamBeffMaillage(myBeam, bEffG, bEffD, bApp)

        '--( On commence par regénérer le maillage

        Maillage.Creation_maillage_2D_poutre_plancher_mince(myBeam.Section.ProfilA, myBeam.Dalle, myBeam.ParamFeu, bEffG, bEffD, myBeam.lIntermediaire, bApp)

    End Sub

    Public Sub ParamBeffMaillage(myBeam As cls_Poutre, ByRef bEffG As Decimal, ByRef bEffD As Decimal, ByRef bApp As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Largeur de béton à droite et à gauhe de la section et pas du maillage
        '---------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   bEffG       [S] :   Largeur à gauche
        '   bEffD       [S] :   Largeur à droite
        '   bApp        [S] :   
        '---------------------------------------------------------------------------------------------------------

        If myBeam.lIntermediaire Then
            bEffG = myBeam.EntraxeD1 / 2
        Else
            bEffG = myBeam.EntraxeD1
        End If
        bEffD = myBeam.EntraxeD2 / 2
        bApp = 0.05

    End Sub

    Public Sub ChargerTemperatures(myBeam As cls_Poutre, iBeam As Integer, FileNameP As String,
                                   nbY As Integer, nbZ As Integer, ByRef TempMailStep(,,) As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des lignes dans le fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iBeam       [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   FileNameP   [E] :   Nom du fichier de sauvegarde du projet
        '   nbY         [E] :   Nombre de mailles / y dans le maillage
        '   nbZ         [E] :   Nombre de mailles / z dans le maillage
        '---------------------------------------------------------------------------------------------------------

        Dim indPosPoutres As List(Of Integer) = Nothing
        Dim Lines As List(Of String) = Nothing

        RecupereLinesFiles(FileNameP, Lines, indPosPoutres)

        LireChargerTemperatures(Lines, indPosPoutres, iBeam, nbY, nbZ, TempMailStep)

    End Sub

    Private Sub LireChargerTemperatures(Lines As List(Of String), iLPoutres As List(Of Integer), iBeam As Integer,
                                        nbY As Integer, nbZ As Integer, ByRef TempMailStep(,,) As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Chargement des températures du maillage à partir des lignes du fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   Lines       [E] :   Lignes du fichier de sauvegarde du projet
        '   iLPoutres   [E] :   Indice des lignes ou demarrent chaque poutre du projet
        '   iBeam       [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   nbY         [E] :   Nombre de mailles / y dans le maillage
        '   nbZ         [E] :   Nombre de mailles / z dans le maillage
        '   TempMailStep[S] :   Table des températures par step et par maille
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iStart As Integer
        Dim iStop As Integer
        Dim Blocs As New List(Of String)
        Dim indBlocs As New List(Of Integer)
        Dim pprj As New cls_Projet

        '--( Initialisation des variables
        If iBeam = iLPoutres.Count - 1 Then
            iStart = iLPoutres(iBeam)
            iStop = Lines.Count - 1
        Else
            iStart = iLPoutres(iBeam)
            iStop = iLPoutres(iBeam + 1) - 1
        End If

        pprj.RepereLignesBlocPoutre(Lines, Blocs, indBlocs, iStart)

        '--( Déclarations

        Dim iBloc, nbBlocs As Integer
        Dim iFin As Integer
        Dim MotCle As String
        Dim NbCar As Integer = pprj.NomBlocThermiqueR.Length
        Dim lOK As Boolean = False
        Dim StepR As String

        Dim BkCALCULTH As String = pprj.NomBlocCalculThermique.ToUpper
        Dim BkTHR As String = pprj.NomBlocThermiqueR.ToUpper

        '--( Initialisation

        nbBlocs = Blocs.Count

        '--( Boucle sur les blocs

        For iBloc = 0 To nbBlocs - 1

            If iBloc = nbBlocs - 1 Then iFin = iStop Else iFin = indBlocs(iBloc + 1) - 1

            If Blocs(iBloc) = BkCALCULTH Then
                '# Recupération et contrôle du nombre de mailles en Y et Z du maillage thermique

                ReadBlocCalculTh(Lines, indBlocs(iBloc), iFin, lOK, nbY, nbZ)

            Else

                MotCle = Blocs(iBloc).Substring(0, Math.Min(NbCar, Blocs(iBloc).Length)).ToUpper

                If (MotCle = BkTHR.ToUpper) And lOK Then

                    StepR = Blocs(iBloc).Substring(BkTHR.Length).Trim

                    ReadBlocThermiqueR(Lines, indBlocs(iBloc), iFin, StepR.Substring(1), nbZ, TempMailStep)

                End If

            End If

        Next

    End Sub

    Private Sub ReadBlocThermiqueR(Lines As List(Of String), Index0 As Integer, IndexFin As Integer, StepR As String,
                                   nbZ As Integer, ByRef TempMailStep(,,) As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Lecture du bloc de températures du maillage à l'instant de temps StepR
        '---------------------------------------------------------------------------------------------------------
        '   Lines       [E] :   Lignes du fichier de sauvegarde du projet
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '   StepR       [E] :   Durée cible du pas de temps traité
        '   nbZ         [E] :   Nombre de mailles / z dans le maillage
        '   TempMailStep[S] :   Table des températures par step et par maille
        '---------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i, j As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 6

        Dim iColumnY As Integer
        Dim iMailleZ As Integer
        Dim iStep As Integer

        '--( Initialisation

        iStep = Array.IndexOf(cls_VerifFeuSlimAcier.TimeSteps, CDec(Val(StepR)))

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lines(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "COLUMN"

                        iColumnY = CInt(Val(Mots(2)))

                        For j = 3 To nbMots

                            iMailleZ = j - 3

                            If iMailleZ < nbZ Then
                                TempMailStep(iStep, iColumnY, iMailleZ) = CDec((Mots(j)))
                            End If

                        Next

                End Select

            End If
        Next

    End Sub

    Private Sub ReadBlocCalculTh(ByVal Lignes As List(Of String),
                                 ByVal Index0 As Integer, ByVal IndexFin As Integer, ByRef lOk As Boolean,
                                 NbY As Integer, NbZ As Integer)
        '-------------------------------------------------------------------------------------
        '   05/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Lecture du bloc BETON
        '-------------------------------------------------------------------------------------
        '   myBeton     [S] :   Béton à definir
        '   Lignes      [E] :   lignes extraites du fichier de données
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '   nbY         [E] :   Nombre de mailles / y dans le maillage
        '   nbZ         [E] :   Nombre de mailles / z dans le maillage
        '-------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 4

        Dim pNbY As Integer = -1
        Dim pNbz As Integer = -1

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "NB_Y" : pNbY = CInt(Val(Mots(nbMots)))
                    Case "NB_Z" : pNbz = CInt(Val(Mots(nbMots)))
                End Select

            End If
        Next

        '--( Contrôle des données

        lOk = True

        If pNbY <> NbY Then lOk = False
        If pNbz <> NbZ Then lOk = False

    End Sub

    Private Sub RecupereLinesFiles(FileNameP As String, ByRef Lines As List(Of String), ByRef iPoutres As List(Of Integer))
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des lignes dans le fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   FileNameP   [E] :   Nom du fichier de sauvegarde du projet
        '   Lines       [S] :   
        '   iPoutres    [S] :
        '---------------------------------------------------------------------------------------------------------

        '--( Déclaration des variables

        Dim Poutres As New List(Of String)          ' Nom des poutres contenues dans le fichier

        '--( Initialisation des variables

        iPoutres = New List(Of Integer)        ' Indice des poutres contenures dans le fichier
        Lines = New List(Of String)

        '--( Déclaration

        Dim lOK As Boolean

        '--( Récupération des lignes du projet

        cls_Projet.ReadLineFile(FileNameP, Lines, lOK)

        '--( Traitement

        cls_Projet.AnalyseFichier(Lines, Poutres, iPoutres)

    End Sub

#End Region


End Class
