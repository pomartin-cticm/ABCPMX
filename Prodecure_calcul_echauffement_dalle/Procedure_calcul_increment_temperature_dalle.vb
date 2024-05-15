 Sub Calcul_thermique_Dalle_beton(val_tdalle As Single, val_NbLayer As Integer, val_tLayer() As Single, val_DeltaT As Single, val_ThetaC() As Single,
                                     val_ThetaG As Single, val_ThetaR As Single, val_AlphaCInf As Single, val_AlphaCSup As Single, val_U As Single, val_EpsilonF As Single, val_SigmaSB As Single, val_EpsilonC As Single,
                                     val_lNormal As Boolean, val_RhoC As Single, val_lRhoCVariable As Boolean, val_lANFrance As Boolean, val_lGeneration1 As Boolean)

        'Calcul de l'échauffement d'une dalle en béton exposée à l'incendie normalisé en sous-face
        'Paramètres d'entrée:
        '   val_tdalle          epaisseur de la dalle                                       (m)
        '   val_NbLayer         nombre de couche discrétisant la dalle
        '   val_tLayer(i)       Épaisseur individuelle des couches                          (m)
        '                   de i = 0 à NbLayer-1, i = 0 pour la couche inférieure
        '   val_t               instant de la boucle de calcul                              (s)
        '   val_DeltaT          incrément de temps                                          (s)
        '   val_ThetaC(i)       température dans chaque couche i du béton, au temps t       (°C)
        '   val_ThetaG          température des gaz chauds sous la dalle au temps val_t + val_DeltaT  (°C)
        '   val_ThetaR          température de référence au-dessus de la dalle              (°C)
        '   val_AlphaCInf       coefficient de convection pour la face exposée              (W/m2 K)
        '   val_AlphaCSup       coefficient de convection pour la face non exposée          (W/m2 K)
        '   val_U_              teneur en eau du béton (entre 0 et 10)                      (%)
        '   val_EpsilonF        émissivité du feu
        '   val_SigmaSB         constante de Stefan-Boltzmann
        '   val_EpsilonC        émissivité de surface du béton
        '   val_lNormal         indique si béton NC (true) ou LC (false)
        '   val_RhoC            masse volumique du béton (à froid)                              (kg/m3)
        '   val_lRhoCVariable   indique si on utilise la formule de rhoc de l’EN 1994-1-2 variant en fonction de la température, pour un béton normal.
        '                   Si non, on utilise la valeur constante du paramètre RhoC.
        '                   Pour le béton léger, on utilise toujours RhoC.
        '   val_lANFrance       indique si on utilise l’Annexe Nationale française de l’EN 1994-1-2
        '   val_LGeneration1    indique si génération 1 ou génération 2 des Eurocodes
        '
        'Paramètre en sortie:
        '   val_ThetaC(i)       Température dans chaque couche i du béton, au temps t + DeltaT  (°C)


        'Largeur des mailles de la dalle en beton
        Dim val_b As Single : val_b = 0.005

        'Temperature des mailles
        Dim Temp_0(0 To val_NbLayer - 1) As Single, Temp_1(0 To val_NbLayer - 1) As Single

        'Parametres variables de la boucle de calcul
        Dim val_dt As Single    'increment de temps
        Dim rho_ As Single     'masse volumique du materiau d'une maille a un instant donne
        Dim cp_ As Single      'chaleur specifique du materiau d'une maille a un instant donne
        Dim rho_cp As Single    'produit rho * cp du materiau d'une maille a un instant donne
        Dim lambda_ As Single   'conductivite thermique du materiau d'une maille a un instant donne
        Dim val_dth As Single   'increment de temperature d'une maille pendant val_DeltaT
        Dim h_net_ce As Single   'densite de flux convectif sur les faces exposees a un instant donne
        Dim h_net_re As Single   'densite de flux radiatif sur les faces exposees a un instant donne
        Dim h_net_de As Single   'densite totale de flux sur les faces exposees a un instant donne
        Dim h_net_cn As Single   'densite de flux convectif sur les faces non exposees a un instant donne
        Dim h_net_rn As Single   'densite de flux radiatif sur les faces non exposees a un instant donne
        Dim h_net_dn As Single   'densite totale de flux sur les faces non exposees a un instant donne

        Dim dth_1 As Single, dth_2 As Single
        Dim h_11 As Single, h_21 As Single, lambda_11 As Single
        Dim h_12 As Single, h_22 As Single, lambda_21 As Single
        Dim k_1 As Single
        Dim q_1 As Single, q_2 As Single, q_z As Single
        Dim cst_1 As Single, cst_2 As Single

        Dim i_ As Integer

        'Temperature des couches de beton à l'instant val_t
        For i_ = 0 To val_NbLayer - 1
            Temp_0(i_) = val_ThetaC(i_)
            Temp_1(i_) = Temp_0(i_)
        Next

        'Boucle sur les couches pour calculer la temperature de chacune à l'instant val_t + val_DeltaT
        For i_ = 0 To val_NbLayer - 1

            'Initialisation de valeurs
            val_dth = 0
            dth_1 = 0 : dth_2 = 0
            h_11 = 0 : h_21 = 0
            h_12 = 0 : h_22 = 0

            'Caracteristiques thermiques du beton
            rho_ = Masse_volumique_beton(val_lNormal, val_lRhoCVariable, val_lGeneration1, val_RhoC, Temp_0(i_))
            cp_ = Chaleur_specifique_beton(val_lNormal, val_U, val_lGeneration1, Temp_0(i_))
            rho_cp = rho_ * cp_
            lambda_ = Conductivite_thermique_beton(val_lNormal, val_lANFrance, val_lGeneration1, Temp_0(i_))

            'Convection et rayonnement sur les faces inf et sup de la dalle
            If i_ = 0 Then
                'Face exposee
                h_net_ce = val_AlphaCInf * (val_ThetaG - Temp_0(i_))    'densite de flux convectif
                h_net_re = val_EpsilonF * val_EpsilonC * val_SigmaSB * ((val_ThetaG + 273.0) ^ 4 - (Temp_0(i_) + 273.0) ^ 4)    'densite de flux radiatif
                h_net_de = h_net_ce + h_net_re  'densite de flux net

            ElseIf i_ = val_NbLayer - 1 Then
                'Face non exposee
                h_net_cn = val_AlphaCSup * (val_ThetaR - Temp_0(i_))    'densite de flux convectif
                h_net_rn = val_EpsilonF * val_EpsilonC * val_SigmaSB * ((val_ThetaR + 273.0) ^ 4 - (Temp_0(i_) + 273.0) ^ 4)    'densite de flux radiatif
                h_net_dn = h_net_cn + h_net_rn  'densite de flux net

            End If


            'Face inferieure de la couche i_
            If i_ = 0 Then  'Exposee au feu

                dth_1 = val_ThetaG - Temp_0(i_) 'ecart de temperature entre les gaz chauds et la maille i_
                h_11 = dth_1 / h_net_de

            Else  'Interieure

                dth_1 = Temp_0(i_ - 1) - Temp_0(i_) 'ecart de temperature entre les mailles i_-1 et i_
                h_11 = 0.5 * val_tLayer(i_ - 1)     'demi-epaisseur de la couche i_-1, sur laquelle se produit de la conduction entre les deux mailles
                lambda_11 = Conductivite_thermique_beton(val_lNormal, val_lANFrance, val_lGeneration1, Temp_0(i_ - 1))
                h_11 = h_11 / lambda_11

            End If

            'Face superieure de la couche i_
            If i_ < val_NbLayer - 1 Then  'Interieure

                dth_2 = Temp_0(i_ + 1) - Temp_0(i_) 'ecart de temperature entre les mailles i_+1 et i_
                h_21 = 0.5 * val_tLayer(i_ + 1)     'demi-epaisseur de la couche i_+1, sur laquelle se produit de la conduction entre les deux mailles
                lambda_21 = Conductivite_thermique_beton(val_lNormal, val_lANFrance, val_lGeneration1, Temp_0(i_ + 1))
                h_21 = h_21 / lambda_21

            Else  'Non exposee au feu

                dth_2 = val_ThetaR - Temp_0(i_) 'ecart de temperature entre l'air ambiant et la maille i_
                If Math.Abs(dth_2) > 0.0001 Then
                    h_21 = dth_2 / h_net_dn
                End If

            End If

            h_12 = 0.5 * val_tLayer(i_) / lambda_
            h_22 = 0.5 * val_tLayer(i_) / lambda_

            cst_1 = val_tLayer(i_) / (h_11 + h_12)
            cst_2 = val_tLayer(i_) / (h_21 + h_22)

            q_1 = dth_1 * val_DeltaT * cst_1
            q_2 = dth_2 * val_DeltaT * cst_2

            q_z = q_1 + q_2

            k_1 = rho_cp * val_b * val_tLayer(i_)
            val_dth = q_z / k_1
            Temp_1(i_) = Temp_0(i_) + val_dth

            val_ThetaC(i_) = Temp_1(i_)

        Next

    End Sub

#Region "Proprietes thermiques"
    Function Masse_volumique_beton(ByVal val_bet As Boolean, ByVal val_var As Boolean, ByVal val_gen1 As Boolean, ByVal val_rho As Single, ByVal val_th As Single) As Single

        'Masse volumique du béton selon EN 1994-1-2
        'val_bet : type de beton : true (NC) ou false (LC)
        'val_var : variation en fonction de la temperature : true (variable) ou false (valeur constante) 
        'val_gen1 : génération 1 (true) ou 2 (false) de l'EN 1994-1-2 
        'val_rho : masse volumique du béton à 20°C
        'val_th : temperature (°C)

        Dim rho_c As Single
        If (Not val_bet) Or (Not val_var) Then 'beton LC ou valeur constante
            rho_c = val_rho
        ElseIf val_gen1 Then    'beton NC : 1re génération des Eurocodes
            rho_c = 2354.0 - 23.47 * val_th / 100.0
        ElseIf val_th >= 20.0 Then    'béton NC : 2e génération des Eurocodes
            If val_th <= 115.0# Then
                rho_c = val_rho
            ElseIf val_th <= 200.0 Then
                rho_c = val_rho * (1 - 0.02 * (val_th - 115.0) / 85.0#)
            ElseIf val_th <= 400.0 Then
                rho_c = val_rho * (0.98 - 0.03 * (val_th - 200.0) / 200.0)
            ElseIf val_th <= 1200.0 Then
                rho_c = val_rho * (0.95 - 0.07 * (val_th - 400.0) / 800.0)
            End If

        End If

        Return rho_c

    End Function


    Function Conductivite_thermique_beton(ByVal val_bet As Boolean, val_ANF As Boolean, ByVal val_gen1 As Boolean, ByVal val_th As Single) As Single

        'Conductivite thermique du beton selon NF EN 1994-1-2
        'val_bet : type de beton : true (NC) ou false (LC)
        'val_ANF : courbe du beton : true (ANF) ou false (limite superieure)
        'val_gen1 : génération 1 (true) ou 2 (false) de l'EN 1994-1-2 
        'val_th : temperature (°C)

        Dim lambda_c As Single
        Dim val_sup As Single
        Dim val_inf As Single
        Dim val_pente As Single
        Dim val_ordo As Single

        If val_bet Then 'beton NC

            val_sup = 2.0 - 0.2451 * val_th / 100.0 + 0.0107 * (val_th / 100.0) ^ 2
            val_inf = 1.36 - 0.136 * val_th / 100.0 + 0.0057 * (val_th / 100.0) ^ 2
            If (val_gen1 AndAlso Not val_ANF) Or val_th <= 140.0 Then    'limite superieure
                lambda_c = val_sup
            ElseIf val_th >= 160.0 AndAlso val_th <= 1200.0 AndAlso (val_ANF Or Not val_gen1) Then       'ANF
                lambda_c = val_inf
            Else  'ANF
                val_pente = (val_inf - val_sup) / (160.0 - 140.0)
                val_ordo = val_sup - val_pente * 140.0
                lambda_c = val_pente * val_th + val_ordo
            End If

        Else    'beton LC 1re et 2e générations des Eurocodes
            If val_th >= 20.0 AndAlso val_th <= 800.0 Then
                lambda_c = 1 - val_th / 1600.0
            ElseIf val_th > 800.0 AndAlso val_th <= 1200.0 Then
                lambda_c = 0.5
            End If
        End If

        Return lambda_c

    End Function

    Function Chaleur_specifique_beton(ByVal val_bet As Boolean, ByVal val_u As Single, ByVal val_gen1 As Single, ByVal val_th As Single) As Single

        'Chaleur specifique du beton selon NF EN 1994-1-2
        'val_bet : type de beton : true (NC) ou false (LC)
        'val_gen1 : génération 1 (true) ou 2 (false) de l'EN 1994-1-2 
        'val_th : temperature (°C)
        'val_u : taux d'humidite (%)

        Dim cp_c As Single
        Dim val_cc As Single
        Dim val_pente As Single
        Dim val_ordo As Single

        'Valeurs du pic
        '1re generation : 3 valeurs de teneur en eau pour lesquelles le pic est precise : 0%, 3% et 10%
        '2e generation : 4 valeurs de teneur en eau pour lesquelles le pic est precise : 0%, 1.5%, 3% et 10%

        If (Not val_bet) AndAlso val_gen1 Then  'beton LC 1re generation des Eurocodes

            cp_c = 840.0

        Else    'beton NC 1re generation ou beton NC ou LC 2e generation

            If Math.Abs(val_u) <= 0.0001 Then
                val_cc = 900.0
            ElseIf (Not val_gen1) AndAlso Math.Abs(val_u - 1.5) <= 0.0001 Then '2e génération des Eurocodes : 1.5%
                val_cc = 1470.0
            ElseIf Math.Abs(val_u - 3.0) <= 0.0001 Then
                val_cc = 2020.0
            ElseIf Math.Abs(val_u - 10.0) <= 0.0001 Then
                val_cc = 5600.0
            ElseIf val_u > 0.0 AndAlso val_u < 3.0 Then   'teneur en eau inferieure à 3%
                If val_gen1 Then    '1re generation
                    val_cc = 900.0 + (2020.0 - 900.0) * val_u / 3.0
                ElseIf val_u < 1.5 Then '2e generation : teneur en eau inferieure a 1.5%
                    val_cc = 900.0 + (1470.0 - 900.0) * val_u / 1.5
                Else '2e generation : teneur en eau superieure comprise entre 1.5% et 3%
                    val_cc = 1470.0 + (2020.0 - 1470.0) * (val_u - 1.5) / (3.0 - 1.5)
                End If
            ElseIf val_u > 3.0 AndAlso val_u < 10.0 Then      'teneur en eau comprise entre 3% et 10%
                val_cc = 2020.0 + (5600.0 - 2020.0) * (val_u - 3.0) / (10.0 - 3.0)
            End If

            'Variation en fonction du pic et de la temperature
            If val_th <= 100.0 Then
                cp_c = 900.0
            ElseIf val_th <= 200.0 Then
                If val_th <= 115.0 Then
                    If val_gen1 OrElse val_th <= 100.001 Then
                        If val_gen1 Then
                            val_pente = (900.0 - val_cc) / (100.0 - 115.0)
                        Else
                            val_pente = (900.0 - val_cc) / (100.001 - 100.0)
                        End If
                        val_ordo = 900.0 - val_pente * 100.0
                        cp_c = val_pente * val_th + val_ordo
                    Else
                        cp_c = val_cc
                    End If
                Else
                    val_pente = (1000.0 - val_cc) / (200.0 - 115.0)
                    val_ordo = 1000.0 - val_pente * 200.0
                    cp_c = val_pente * val_th + val_ordo
                End If
            ElseIf val_th <= 400.0 Then
                cp_c = 1000.0 + (val_th - 200.0) / 2.0
            ElseIf val_th <= 1200.0 Then
                cp_c = 1100.0
            End If

        End If

        Return cp_c

    End Function

#End Region