Public Class cls_EchauffementDalleFEM

    '===== CLASSE POUR LE CALCUL NUMERIQUE DE L'ECHAUFFEMENT D'UNE DALLE ======

#Region " Constructeur "

    Public Sub New()

    End Sub

#End Region

#Region " Maillage "

    Public Sub PrepareMaillageDalleFEM(tDalle As Decimal, EpEltMax As Decimal, ByRef nbLayers As Integer, ByRef tLayers() As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   28/10/24 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Préparation du maillage pour un calcul EF
        '-----------------------------------------------------------------------------------------------------------------------------
        '   tDalle      [E] :   Epaisseur totale de la dalle
        '   EpElMax     [E] :   Epaisseur maximale d'une maille
        '   nbLayers    [S] :   Nombre de mailles
        '   tLayers     [S] :   Tableau des épaisseurs de maille
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim EpMaille As Decimal

        '--( Nombre de mailles

        nbLayers = Math.Floor(tDalle / EpEltMax)

        If Not IsEqual(tDalle, EpEltMax * nbLayers) Then nbLayers += 1

        '--( Epaisseur des mailles courantes (arrondi au mm près)

        EpMaille = Math.Floor(tDalle / nbLayers * 1000) / 1000

        '--( Tableau des épaisseurs

        ReDim tLayers(nbLayers - 1)

        For i = 0 To nbLayers - 2
            tLayers(i) = EpMaille
        Next

        tLayers(nbLayers - 1) = tDalle - (nbLayers - 1) * EpMaille
    End Sub

#End Region

#Region " Procédure numérique par différence finie "

    Sub Calcul_thermique_Dalle_beton(tDalle As Decimal, nbLayers As Integer, tLayers() As Decimal, DeltaT As Decimal,
                                     ByRef ThetaC() As Decimal,
                                     ThetaG As Decimal, ThetaR As Decimal, AlphaCInf As Decimal, AlphaCSup As Decimal,
                                     val_U As Decimal, EpsilonF As Decimal, SigmaSB As Decimal, EpsilonC As Decimal,
                                     lNormal As Boolean, RhoC As Decimal, lRhoCVariable As Boolean,
                                     lANFrance As Boolean, lGeneration1 As Boolean)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   15/05/24 :  Création - GiB
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul de l'échauffement d'une dalle en béton exposée à l'incendie normalisé en sous-face
        '   Par la méthode des différences finies
        '-----------------------------------------------------------------------------------------------------------------------------
        '   tDalle              [E] :   épaisseur de la dalle                                       (m)
        '   NbLayers            [E] :   nombre de couche discrétisant la dalle
        '   tLayers(i)          [E] :   épaisseur individuelle des couches                          (m)
        '                                   de i = 0 à NbLayers-1, i = 0 pour la couche inférieure
        '   DeltaT              [E] :   incrément de temps                                          (s)
        '   ThetaC(i)               :   température dans chaque couche i du béton,        (°C)
        '                       [E] :       au temps t en entrée
        '                       [S] :       au temps t + DeltaT  en sortie 
        '   ThetaG              [E] :   température des gaz chauds sous la dalle au temps val_t + DeltaT  (°C)
        '   ThetaR              [E] :   température de référence au-dessus de la dalle              (°C)
        '   AlphaCInf           [E] :   coefficient de convection pour la face exposée              (W/m2 K)
        '   AlphaCSup           [E] :   coefficient de convection pour la face non exposée          (W/m2 K)
        '   val_U               [E] :   teneur en eau du béton (entre 0 et 10)                      (%)
        '   EpsilonF            [E] :   émissivité du feu
        '   SigmaSB             [E] :   constante de Stefan-Boltzmann
        '   EpsilonC            [E] :   émissivité de surface du béton
        '   lNormal             [E] :   indique si béton NC (true) ou LC (false)
        '   RhoC                [E] :   masse volumique du béton (à froid)                              (kg/m3)
        '   lRhoCVariable       [E] :   indique si on utilise la formule de rhoc de l’EN 1994-1-2 variant en fonction de la température, pour un béton normal.
        '                               Si non, on utilise la valeur constante du paramètre RhoC.
        '                               Pour le béton léger, on utilise toujours RhoC.
        '   lANFrance           [E] :   indique si on utilise l’Annexe Nationale française de l’EN 1994-1-2
        '   lGeneration1        [E] :   indique si génération 1 ou génération 2 des Eurocodes
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        'Temperature des mailles
        Dim Temp_0(0 To nbLayers - 1) As Single, Temp_1(0 To nbLayers - 1) As Single

        'Parametres variables de la boucle de calcul
        Dim rho_ As Single          ' masse volumique du materiau d'une maille a un instant donne
        Dim cp_ As Single           ' chaleur specifique du materiau d'une maille a un instant donne
        Dim rho_cp As Single        ' produit rho * cp du materiau d'une maille a un instant donne
        Dim lambda_ As Single       ' conductivite thermique du materiau d'une maille a un instant donne
        Dim val_dth As Single       ' increment de temperature d'une maille pendant DeltaT
        Dim h_net_ce As Single      ' densite de flux convectif sur les faces exposees a un instant donne
        Dim h_net_re As Single      ' densite de flux radiatif sur les faces exposees a un instant donne
        Dim h_net_de As Single      ' densite totale de flux sur les faces exposees a un instant donne
        Dim h_net_cn As Single      ' densite de flux convectif sur les faces non exposees a un instant donne
        Dim h_net_rn As Single      ' densite de flux radiatif sur les faces non exposees a un instant donne
        Dim h_net_dn As Single      ' densite totale de flux sur les faces non exposees a un instant donne

        Dim dth_1 As Single, dth_2 As Single
        Dim h_11 As Single, h_21 As Single, lambda_11 As Single
        Dim h_12 As Single, h_22 As Single, lambda_21 As Single
        Dim k_1 As Single
        Dim q_1 As Single, q_2 As Single, q_z As Single
        Dim cst_1 As Single, cst_2 As Single

        Dim i As Integer
        Dim EN1994_12 As New cls_EurocodesFeu

        'Temperature des couches de beton à l'instant val_t
        For i = 0 To nbLayers - 1
            Temp_0(i) = ThetaC(i)
            Temp_1(i) = Temp_0(i)
        Next

        'Boucle sur les couches pour calculer la temperature de chacune à l'instant val_t + DeltaT
        For i = 0 To nbLayers - 1

            'Initialisation de valeurs
            val_dth = 0
            dth_1 = 0 : dth_2 = 0
            h_11 = 0 : h_21 = 0
            h_12 = 0 : h_22 = 0

            'Caracteristiques thermiques du beton
            rho_ = EN1994_12.Masse_volumique_beton(lNormal, lRhoCVariable, lGeneration1, RhoC, Temp_0(i))
            cp_ = EN1994_12.Chaleur_specifique_beton(lNormal, val_U, lGeneration1, Temp_0(i))
            rho_cp = rho_ * cp_
            lambda_ = EN1994_12.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i))

            'Convection et rayonnement sur les faces inf et sup de la dalle
            If i = 0 Then
                'Face exposee
                h_net_ce = AlphaCInf * (ThetaG - Temp_0(i))    'densite de flux convectif
                h_net_re = EpsilonF * EpsilonC * SigmaSB * ((ThetaG + 273.0) ^ 4 - (Temp_0(i) + 273.0) ^ 4)    'densite de flux radiatif
                h_net_de = h_net_ce + h_net_re  'densite de flux net

            ElseIf i = nbLayers - 1 Then
                'Face non exposee
                h_net_cn = AlphaCSup * (ThetaR - Temp_0(i))    'densite de flux convectif
                'h_net_rn = EpsilonF * EpsilonC * SigmaSB * ((ThetaR + 273.0) ^ 4 - (Temp_0(i) + 273.0) ^ 4)       'densite de flux radiatif
                h_net_rn = EpsilonC * SigmaSB * ((ThetaR + 273.0) ^ 4 - (Temp_0(i) + 273.0) ^ 4)                   'densite de flux radiatif
                h_net_dn = h_net_cn + h_net_rn  'densite de flux net

            End If


            'Face inferieure de la couche i
            If i = 0 Then                              'Exposee au feu

                dth_1 = ThetaG - Temp_0(i)             'ecart de temperature entre les gaz chauds et la maille i
                h_11 = dth_1 / h_net_de

            Else  'Interieure

                dth_1 = Temp_0(i - 1) - Temp_0(i)     'ecart de temperature entre les mailles i-1 et i
                h_11 = 0.5 * tLayers(i - 1)            'demi-epaisseur de la couche i-1, sur laquelle se produit de la conduction entre les deux mailles
                lambda_11 = EN1994_12.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i - 1))
                h_11 = h_11 / lambda_11

            End If

            'Face superieure de la couche i
            If i < nbLayers - 1 Then  'Interieure

                dth_2 = Temp_0(i + 1) - Temp_0(i)     'ecart de temperature entre les mailles i+1 et i
                h_21 = 0.5 * tLayers(i + 1)            'demi-epaisseur de la couche i+1, sur laquelle se produit de la conduction entre les deux mailles
                lambda_21 = EN1994_12.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i + 1))
                h_21 = h_21 / lambda_21

            Else  'Non exposee au feu

                dth_2 = ThetaR - Temp_0(i)             'ecart de temperature entre l'air ambiant et la maille i
                If Math.Abs(dth_2) > 0.0001 Then
                    h_21 = dth_2 / h_net_dn
                End If

            End If

            h_12 = 0.5 * tLayers(i) / lambda_          'resistance thermique sur la demi-epaisseur inferieure de la maille i
            h_22 = 0.5 * tLayers(i) / lambda_          'resistance thermique sur la demi-epaisseur superieure de la maille i

            '=== Unité de h : (m2K)/W

            cst_1 = 1 / (h_11 + h_12)                   '== Unité de cst: W/(m2K)
            cst_2 = 1 / (h_21 + h_22)

            'unité                   K s W / (mK)  = Ws/m

            q_1 = dth_1 * DeltaT * cst_1        'energie fournie par la maille superieure ou l'air a temperature ambiante
            q_2 = dth_2 * DeltaT * cst_2        'energie fournie par la maille inferieure ou les gaz chauds

            q_z = q_1 + q_2                     'energie fournie a la maille

            '=== Unité rho : kg/m3
            '=== Unité cp  : J / (kgK) = Ws/(kgK)
            '=== Unité k_1 : kg/m3 * Ws * m / (kgK) = Ws/(m2K)

            k_1 = rho_cp * tLayers(i)          'energie interne de la maille i par increment de temperature

            '=== Unité de dth :       Ws/(m2) / (Ws/(m2K))= K

            val_dth = q_z / k_1
            Temp_1(i) = Temp_0(i) + val_dth

            ThetaC(i) = Temp_1(i)

        Next

    End Sub

    Public Sub Calcul_thermique_Dalle_betonOLD(tDalle As Decimal, NbLayers As Integer, tLayers() As Decimal,
                                            DeltaT As Decimal, ByRef ThetaC() As Decimal,
                                            ThetaG As Decimal, ThetaR As Decimal, AlphaCInf As Decimal, AlphaCSup As Decimal,
                                            val_U As Decimal, EpsilonF As Decimal, SigmaSB As Decimal, EpsilonC As Decimal,
                                            lNormal As Boolean, RhoC As Decimal, lRhoCVariable As Boolean,
                                            lANFrance As Boolean, lGeneration1 As Boolean)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   15/05/24 :  Création - GiB
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul de l'échauffement d'une dalle en béton exposée à l'incendie normalisé en sous-face
        '   Par la méthode des différences finies
        '-----------------------------------------------------------------------------------------------------------------------------
        '   tDalle              [E] :   épaisseur de la dalle                                       (m)
        '   NbLayers            [E] :   nombre de couche discrétisant la dalle
        '   tLayers(i)          [E] :   épaisseur individuelle des couches                          (m)
        '                   de i = 0 à NbLayers-1, i = 0 pour la couche inférieure
        '   DeltaT              [E] :   incrément de temps                                          (s)
        '   ThetaC(i)           [E/S] : température dans chaque couche i du béton,        (°C)
        '                               au temps t en entrée
        '                               au temps t + DeltaT  en sortie 
        '   ThetaG              [E] :   température des gaz chauds sous la dalle au temps val_t + DeltaT  (°C)
        '   ThetaR              [E] :   température de référence au-dessus de la dalle              (°C)
        '   AlphaCInf           [E] :   coefficient de convection pour la face exposée              (W/m2 K)
        '   AlphaCSup           [E] :   coefficient de convection pour la face non exposée          (W/m2 K)
        '   val_U_              [E] :   teneur en eau du béton (entre 0 et 10)                      (%)
        '   EpsilonF            [E] :   émissivité du feu
        '   SigmaSB             [E] :   constante de Stefan-Boltzmann
        '   EpsilonC            [E] :   émissivité de surface du béton
        '   lNormal             [E] :   indique si béton NC (true) ou LC (false)
        '   RhoC                [E] :   masse volumique du béton (à froid)                              (kg/m3)
        '   lRhoCVariable       [E] :   indique si on utilise la formule de rhoc de l’EN 1994-1-2 variant en fonction de la température, pour un béton normal.
        '                               Si non, on utilise la valeur constante du paramètre RhoC.
        '                               Pour le béton léger, on utilise toujours RhoC.
        '   lANFrance           [E] :   indique si on utilise l’Annexe Nationale française de l’EN 1994-1-2
        '   lGeneration1        [E] :   indique si génération 1 ou génération 2 des Eurocodes
        '-----------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        'Largeur des mailles de la dalle en beton
        Const val_b As Decimal = 0.005

        'Temperature des mailles
        Dim Temp_0(0 To NbLayers - 1) As Decimal
        Dim Temp_1(0 To NbLayers - 1) As Decimal

        'Parametres variables de la boucle de calcul
        ' Dim val_dt As Single          ' increment de temps
        Dim RhoT As Decimal             ' masse volumique du materiau d'une maille a un instant donne
        Dim cpT As Decimal              ' chaleur specifique du materiau d'une maille a un instant donne
        Dim RhoCpT As Decimal           ' produit rho * cp du materiau d'une maille a un instant donne
        Dim LambdaT As Decimal          ' conductivite thermique du materiau d'une maille a un instant donne
        Dim val_dth As Decimal          ' increment de temperature d'une maille pendant DeltaT
        Dim h_net_ce As Decimal         ' densite de flux convectif sur les faces exposees a un instant donne
        Dim h_net_re As Decimal         ' densite de flux radiatif sur les faces exposees a un instant donne
        Dim h_net_de As Decimal         ' densite totale de flux sur les faces exposees a un instant donne
        Dim h_net_cn As Decimal         ' densite de flux convectif sur les faces non exposees a un instant donne
        Dim h_net_rn As Decimal         ' densite de flux radiatif sur les faces non exposees a un instant donne
        Dim h_net_dn As Decimal         ' densite totale de flux sur les faces non exposees a un instant donne

        Dim dth_1 As Decimal, dth_2 As Decimal
        Dim h_11 As Decimal, h_21 As Decimal, Lambda_11 As Decimal
        Dim h_12 As Decimal, h_22 As Decimal, Lambda_21 As Decimal
        Dim k_1 As Decimal
        Dim q_1 As Decimal, q_2 As Decimal, q_z As Decimal
        Dim cst_1 As Decimal, cst_2 As Decimal

        Dim i As Integer

        Dim EN1994_12 As New cls_EurocodesFeu

        '--( Temperature des couches de beton à l'instant val_t

        For i = 0 To NbLayers - 1
            Temp_0(i) = ThetaC(i)
            Temp_1(i) = Temp_0(i)
        Next

        '--( Boucle sur les couches pour calculer la temperature de chacune à l'instant val_t + DeltaT

        For i = 0 To NbLayers - 1

            '# Initialisation de valeurs
            val_dth = 0
            dth_1 = 0 : dth_2 = 0
            h_11 = 0 : h_21 = 0
            h_12 = 0 : h_22 = 0

            '# Caracteristiques thermiques du beton
            RhoT = EN1994_12.Masse_volumique_beton(lNormal, lRhoCVariable, lGeneration1, RhoC, Temp_0(i))
            cpT = EN1994_12.Chaleur_specifique_beton(lNormal, val_U, lGeneration1, Temp_0(i))
            RhoCpT = RhoT * cpT
            LambdaT = EN1994_12.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i))

            '# Convection et rayonnement sur les faces inf et sup de la dalle
            If i = 0 Then
                '## Face exposee
                h_net_ce = AlphaCInf * (ThetaG - Temp_0(i))     ' densite de flux convectif
                h_net_re = EpsilonF * EpsilonC * SigmaSB * ((ThetaG + 273.0) ^ 4 - (Temp_0(i) + 273.0) ^ 4)    'densite de flux radiatif
                h_net_de = h_net_ce + h_net_re                  ' densite de flux net

            ElseIf i = NbLayers - 1 Then
                '## Face non exposee
                h_net_cn = AlphaCSup * (ThetaR - Temp_0(i))     ' densite de flux convectif
                h_net_rn = EpsilonF * EpsilonC * SigmaSB * ((ThetaR + 273.0) ^ 4 - (Temp_0(i) + 273.0) ^ 4)    'densite de flux radiatif
                h_net_dn = h_net_cn + h_net_rn                  ' densite de flux net

            End If

            'Face inferieure de la couche i
            If i = 0 Then  'Exposee au feu

                dth_1 = ThetaG - Temp_0(i)                      ' ecart de temperature entre les gaz chauds et la maille i
                h_11 = dth_1 / h_net_de

            Else  'Interieure

                dth_1 = Temp_0(i - 1) - Temp_0(i)               ' ecart de temperature entre les mailles i-1 et i
                h_11 = 0.5 * tLayers(i - 1)                     ' demi-epaisseur de la couche i-1, sur laquelle se produit de la conduction entre les deux mailles
                Lambda_11 = EN1994_12.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i - 1))
                h_11 = h_11 / Lambda_11

            End If

            'Face superieure de la couche i
            If i < NbLayers - 1 Then  'Interieure

                dth_2 = Temp_0(i + 1) - Temp_0(i)               ' ecart de temperature entre les mailles i+1 et i
                h_21 = 0.5 * tLayers(i + 1)                     ' demi-epaisseur de la couche i+1, sur laquelle se produit de la conduction entre les deux mailles
                Lambda_21 = EN1994_12.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i + 1))
                h_21 = h_21 / Lambda_21

            Else  'Non exposee au feu

                dth_2 = ThetaR - Temp_0(i)                      ' ecart de temperature entre l'air ambiant et la maille i
                If Math.Abs(dth_2) > 0.0001 Then
                    h_21 = dth_2 / h_net_dn
                End If

            End If

            h_12 = 0.5 * tLayers(i) / LambdaT
            h_22 = 0.5 * tLayers(i) / LambdaT

            cst_1 = tLayers(i) / (h_11 + h_12)
            cst_2 = tLayers(i) / (h_21 + h_22)

            q_1 = dth_1 * DeltaT * cst_1
            q_2 = dth_2 * DeltaT * cst_2

            q_z = q_1 + q_2

            k_1 = RhoCpT * val_b * tLayers(i)
            val_dth = q_z / k_1
            Temp_1(i) = Temp_0(i) + val_dth

            ThetaC(i) = Temp_1(i)

        Next

    End Sub


#End Region

End Class
