Public Class cls_EchauffementSlimFEM
    '===== CLASSE POUR LE CALCUL NUMERIQUE DE L'ECHAUFFEMENT D'UNE DALLE ======

#Region " Solveur Numérique "


    Public Sub Calcul_thermique_Poutre_plancher_mince(ByRef My_mesh As cls_MaillageSlimFloor, My_fire As cls_OptionsFeu,
                                                      My_time As Double, DeltaT As Double, TempG As Double, val_U As Decimal,
                                                      lNormal As Boolean, lANFrance As Boolean, lGeneration1 As Boolean)
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de l'échauffement d'une dalle en béton exposée à l'incendie normalisé en sous-face
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   my_Mesh         [E] :   Classe de caractéristiques du maillage (taille, matériaux et températures)
        '   My_fire         [E] :   Classe de caractéristiques du calcul au feu
        '   My_time         [E] :   Temps actuel du calcul (s)
        '   DeltaT          [E] :   Pas de temps du calcul (s)
        '   TempG           [E] :   Température des gaz chauds au contact de la face exposée de la dalle (°C)
        '   val_U           [E] :   taux d'humidite (%)
        '   lNormal         [E] :   indique si béton NC (true) ou LC (false)
        '   lGeneration1    [E] :   indique si génération 1 ou génération 2 des Eurocodes
        '   lANFrance       [E] :   indique si on utilise l’Annexe Nationale française de l’EN 1994-1-2
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Paramètre en sortie:
        '   My_mesh.Tab_mesh_temp(i,j)       Température dans chaque maille (i,j) de la poutre, au temps t + DeltaT  (°C)
        '---------------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim EN_Feu As New cls_EurocodesFeu

        '%% Temperature des mailles
        Dim Temp_0(0 To My_mesh.nb_cells_y - 1, 0 To My_mesh.nb_cells_z - 1) As Double
        Dim Temp_1(0 To My_mesh.nb_cells_y - 1, 0 To My_mesh.nb_cells_z - 1) As Double

        '%% Parametres variables de la boucle de calcul
        Dim rho_ As Double              ' masse volumique du materiau d'une maille a un instant donne
        Dim cp_ As Double               ' chaleur specifique du materiau d'une maille a un instant donne
        Dim rho_cp As Double            ' produit rho * cp du materiau d'une maille a un instant donne
        Dim lambda_ As Double           ' conductivite thermique du materiau d'une maille a un instant donne
        Dim val_dth As Double           ' increment de temperature d'une maille pendant val_DeltaT
        Dim h_net_ce As Double          ' densite de flux convectif sur les faces exposees a un instant donne
        Dim h_net_re As Double          ' densite de flux radiatif sur les faces exposees a un instant donne
        Dim h_net_de As Double          ' densite totale de flux sur les faces exposees a un instant donne
        Dim h_net_cn As Double          ' densite de flux convectif sur les faces non exposees a un instant donne
        Dim h_net_rn As Double          ' densite de flux radiatif sur les faces non exposees a un instant donne
        Dim h_net_dn As Double          ' densite totale de flux sur les faces non exposees a un instant donne

        Dim val_epsilon As Double       ' emissivite de surface d'une face exterieure de maille

        Dim dth_lb As Double, dth_bb As Double, dth_tb As Double, dth_rb As Double

        Dim h_lb_1 As Double, h_bb_1 As Double, h_tb_1 As Double, h_rb_1 As Double
        Dim h_lb_2 As Double, h_bb_2 As Double, h_tb_2 As Double, h_rb_2 As Double

        Dim lambda_Lb As Double, lambda_Bb As Double, lambda_Tb As Double, lambda_Rb As Double

        Dim k_1 As Double
        Dim q_lb As Double, q_bb As Double, q_tb As Double, q_rb As Double, q_y As Double, q_z As Double
        Dim cst_lb As Double, cst_bb As Double, cst_tb As Double, cst_rb As Double

        Dim i_ As Integer, j_ As Integer, i_mat As Integer, j_mat As Integer, ii_ As Integer, jj_ As Integer
        Dim prop_expo As Boolean
        Dim prop_noex As Boolean

        Dim NbY As Integer = My_mesh.nb_cells_y
        Dim NbZ As Integer = My_mesh.nb_cells_z

        Dim SigmaSB As Double = cls_OptionsFeu.BOLTZMANN

        '--( Temperature des mailles à l'instant val_t

        For i_ = 0 To NbY - 1
            For j_ = 0 To NbZ - 1
                Temp_0(i_, j_) = My_mesh.Tab_mesh_temp(i_, j_)
                Temp_1(i_, j_) = Temp_0(i_, j_)
            Next
        Next

        '--( Boucle sur les mailles entre ind_0 et ind_1 pour calculer la temperature de chacune à l'instant val_t + val_DeltaT

        For j_ = 0 To NbZ - 1

            For i_ = My_mesh.ind_0 To My_mesh.ind_1

                'q_lb : échange entre (i_-1, j_) et (i_,j_) : "left boundary"
                'q_tb : échange entre (i_, j_-1) et (i_,j_) : "top boundary"
                'q_bb : échange entre (i_, j_-1) et (i_,j_) : "bottom boundary"
                'q_rb : échange entre (i_+1, j_) et (i_,j_) : "right boundary"

                'q_y : q_lb + q_rb
                'q_z : q_bb + q_tb

                '%%Initialisation de valeurs
                val_dth = 0.0
                dth_lb = 0.0 : dth_bb = 0.0 : dth_tb = 0.0 : dth_rb = 0.0
                h_lb_1 = 0.0 : h_bb_1 = 0.0 : h_tb_1 = 0.0 : h_rb_1 = 0.0
                h_lb_2 = 0.0 : h_bb_2 = 0.0 : h_tb_2 = 0.0 : h_rb_2 = 0.0

                '%%Numero de materiau de la maille (i_,j_)
                i_mat = My_mesh.Tab_mesh_mat(i_, j_)

                '%%Conditions d'exposition au feu
                prop_expo = False
                If i_mat >= 0 Then

                    If j_ < Temp_0.GetUpperBound(1) AndAlso My_mesh.Tab_mesh_mat(i_, j_ + 1) = -1 Then   'face sup du talon 
                        prop_expo = True
                    End If

                    If j_ = 0 OrElse My_mesh.Tab_mesh_mat(i_, j_ - 1) = -1 Then   'face inf du talon ou de la dalle 
                        prop_expo = True
                    End If

                    If i_ > 0 AndAlso My_mesh.Tab_mesh_mat(i_ - 1, j_) = -1 Then   'face laterale gauche du talon ou de la dalle 
                        prop_expo = True
                    End If

                    If i_ < Temp_0.GetUpperBound(0) AndAlso My_mesh.Tab_mesh_mat(i_ + 1, j_) = -1 Then   'face laterale droite du talon ou de la dalle 
                        prop_expo = True
                    End If

                    prop_noex = False
                    If j_ = Temp_0.GetUpperBound(1) Then
                        prop_noex = True
                    End If

                    'Faces exterieures de la maille
                    If prop_expo Or prop_noex Then
                        'Emissivite de surface
                        If i_mat >= 0 AndAlso i_mat <= 5 Then 'acier de construction (0 : plat soudé, 1 : semelle inférieure, 2 : âme, 3 : semelle supérieure), de soudure (4) ou d'armature (5)  
                            'val_epsilon = My_materials.EpsilonA
                            'If My_materials.Acier_galva AndAlso Temp_0(i_, j_) <= 500.0 Then    'acier galvanisé à chaud échauffé à 500 °C ou moins
                            '    val_epsilon = My_materials.EpsilonA_galva
                            'End If
                            val_epsilon = EN_Feu.EmissiviteAcier(Temp_0(i_, j_), My_fire.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise)

                        ElseIf i_mat = 6 OrElse i_mat = 7 Then  'beton NC (6) ou LC (7)
                            val_epsilon = My_fire.EmissivityC 'My_materials.EpsilonC
                        End If

                        If prop_expo Then
                            'Face exposee
                            h_net_ce = My_fire.AlphaCInf * (TempG - Temp_0(i_, j_))    'densite de flux convectif
                            h_net_re = My_fire.EpsilonF * val_epsilon * SigmaSB * ((TempG + 273.0) ^ 4 - (Temp_0(i_, j_) + 273.0) ^ 4)    'densite de flux radiatif
                            h_net_de = h_net_ce + h_net_re  'densite de flux net

                        ElseIf prop_noex Then
                            'Face non exposee
                            h_net_cn = My_fire.AlphaCSup * (My_fire.TempRef - Temp_0(i_, j_))    'densite de flux convectif
                            h_net_rn = My_fire.EpsilonF * val_epsilon * SigmaSB * ((My_fire.TempRef + 273.0) ^ 4 - (Temp_0(i_, j_) + 273.0) ^ 4)    'densite de flux radiatif
                            h_net_dn = h_net_cn + h_net_rn  'densite de flux net

                        End If
                    End If

                    '1er terme du denominateur de q_lb : face de gauche de la maille (i_,j_)
                    If i_ = 0 Then
                        dth_lb = 0.0
                        h_lb_1 = 0.0
                    Else

                        ii_ = i_ - 1
                        j_mat = My_mesh.Tab_mesh_mat(ii_, j_)

                        If prop_expo AndAlso j_mat = -1 Then        '$ face exposee
                            dth_lb = TempG - Temp_0(i_, j_)         '$ ecart de temperature entre les gaz chauds et la maille (i_-1,j_)
                            h_lb_1 = dth_lb / h_net_de
                        ElseIf j_mat >= 0 Then

                            dth_lb = Temp_0(ii_, j_) - Temp_0(i_, j_) 'ecart de temperature entre les mailles (i_-1,j_) et (i_,j_)

                            '%% Conductivite thermique de la maille (i-1,j_)

                            If j_mat >= 0 AndAlso j_mat <= 5 Then 'acier de construction (0 : plat soudé, 1 : semelle inférieure, 2 : âme, 3 : semelle supérieure), de soudure (4) ou d'armature (5)  
                                lambda_Lb = Conductivite_thermique_acier(Temp_0(ii_, j_))
                            ElseIf j_mat = 6 OrElse j_mat = 7 Then  'beton NC (6) ou LC (7)
                                'lambda_Lb = Conductivite_thermique_beton(My_materials.lNormal, My_materials.lANFrance, My_materials.lGeneration1, Temp_0(ii_, j_))
                                lambda_Lb = EN_Feu.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(ii_, j_))
                            End If
                            h_lb_1 = 0.5 * My_mesh.Tab_mesh_y(ii_) / lambda_Lb
                        End If
                    End If

                    '1er terme du denominateur de q_bb : face inferieure de la maille (i_,j_)
                    jj_ = j_ - 1
                    If prop_expo AndAlso (j_ = 0 OrElse My_mesh.Tab_mesh_mat(i_, jj_) = -1) Then    'face exposee
                        dth_bb = TempG - Temp_0(i_, j_) 'ecart de temperature entre les gaz chauds et la maille (i_,j_)
                        h_bb_1 = dth_bb / h_net_de
                    ElseIf My_mesh.Tab_mesh_mat(i_, jj_) >= 0 Then
                        dth_bb = Temp_0(i_, jj_) - Temp_0(i_, j_) 'ecart de temperature entre les mailles (i_,j_-1) et (i_,j_)

                        'Conductivite thermique de la maille (i_,j_-1)
                        j_mat = My_mesh.Tab_mesh_mat(i_, jj_)
                        If j_mat >= 0 AndAlso j_mat <= 5 Then 'acier de construction (0 : plat soudé, 1 : semelle inférieure, 2 : âme, 3 : semelle supérieure), de soudure (4) ou d'armature (5)  
                            lambda_Bb = Conductivite_thermique_acier(Temp_0(i_, jj_))
                        ElseIf j_mat = 6 OrElse j_mat = 7 Then
                            'lambda_Bb = Conductivite_thermique_beton(My_materials.lNormal, My_materials.lANFrance, My_materials.lGeneration1, Temp_0(i_, jj_))
                            lambda_Bb = EN_Feu.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i_, jj_))
                        End If

                        h_bb_1 = 0.5 * My_mesh.Tab_mesh_z(jj_) / lambda_Bb
                    End If

                    '1er terme du denominateur de q_tb : face superieure de la maille (i_,j_)
                    If j_ < Temp_0.GetUpperBound(1) Then
                        jj_ = j_ + 1
                        j_mat = My_mesh.Tab_mesh_mat(i_, jj_)

                        If prop_expo AndAlso j_mat = -1 Then    'face superieure exposee
                            dth_tb = TempG - Temp_0(i_, j_) 'ecart de temperature entre les gaz chauds et la maille (i_,j_)
                            h_tb_1 = dth_tb / h_net_de
                        ElseIf j_mat >= 0 Then
                            dth_tb = Temp_0(i_, jj_) - Temp_0(i_, j_) 'ecart de temperature entre les mailles (i_,j_+1) et (i_,j_)

                            'Conductivite thermique de la maille (i_,j_+1)
                            If j_mat >= 0 AndAlso j_mat <= 5 Then 'acier de construction (0 : plat soudé, 1 : semelle inférieure, 2 : âme, 3 : semelle supérieure), de soudure (4) ou d'armature (5)  
                                lambda_Tb = Conductivite_thermique_acier(Temp_0(i_, jj_))
                            ElseIf j_mat = 6 OrElse j_mat = 7 Then  'beton NC (6) ou LC (7)
                                ' lambda_Tb = Conductivite_thermique_beton(My_materials.lNormal, My_materials.lANFrance, My_materials.lGeneration1, Temp_0(i_, jj_))
                                lambda_Tb = EN_Feu.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i_, jj_))
                            End If

                            h_tb_1 = 0.5 * My_mesh.Tab_mesh_z(jj_) / lambda_Tb
                        End If

                    ElseIf prop_noex Then    'face non exposee
                        dth_tb = My_fire.TempRef - Temp_0(i_, j_) 'ecart de temperature entre l'air ambiant et la maille (i_,j_)
                        'If Math.Abs(dth_tb) > val_ZERO Then
                        If IsGreater(Math.Abs(dth_tb), 0) Then
                            h_tb_1 = dth_tb / h_net_dn
                        End If
                    End If

                    '1er terme du denominateur de q_rb : face de droite de la maille (i_,j_)
                    If i_ < Temp_0.GetUpperBound(0) Then
                        ii_ = i_ + 1
                        j_mat = My_mesh.Tab_mesh_mat(ii_, j_)
                        If prop_expo AndAlso j_mat = -1 Then    'face exposee au feu
                            dth_rb = TempG - Temp_0(i_, j_) 'ecart de temperature entre les gaz chauds et la maille (i_,j_)
                            h_rb_1 = dth_rb / h_net_de
                        ElseIf j_mat >= 0 Then
                            dth_rb = Temp_0(ii_, j_) - Temp_0(i_, j_) 'ecart de temperature entre les mailles (i_+1,j_) et (i_,j_)

                            'Conductivite thermique de la maille (i_+1,j_)
                            If j_mat >= 0 AndAlso j_mat <= 5 Then 'acier de construction (0 : plat soudé, 1 : semelle inférieure, 2 : âme, 3 : semelle supérieure), de soudure (4) ou d'armature (5)  
                                lambda_Rb = Conductivite_thermique_acier(Temp_0(ii_, j_))
                            ElseIf j_mat = 6 OrElse j_mat = 7 Then  'beton NC (6) ou LC (7)
                                'lambda_Rb = Conductivite_thermique_beton(My_materials.lNormal, My_materials.lANFrance, My_materials.lGeneration1, Temp_0(ii_, j_))
                                lambda_Rb = EN_Feu.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(ii_, j_))
                            End If

                            h_rb_1 = 0.5 * My_mesh.Tab_mesh_y(ii_) / lambda_Rb
                        End If
                    Else
                        dth_rb = 0.0
                        h_rb_1 = 0.0
                    End If

                    '2e terme du denominateur de qi
                    If i_mat >= 0 AndAlso i_mat <= 5 Then 'acier de construction (0 : plat soudé, 1 : semelle inférieure, 2 : âme, 3 : semelle supérieure), de soudure (4) ou d'armature (5)  
                        lambda_ = Conductivite_thermique_acier(Temp_0(i_, j_))
                        rho_ = My_materials.Rho_A

                        'cp_ = Chaleur_specifique_acier(Temp_0(i_, j_))
                        cp_ = EN_Feu.ChaleurSpecifiqueAcier(Temp_0(i_, j_))

                    ElseIf i_mat = 6 OrElse i_mat = 7 Then  'beton NC (6) ou LC (7)
                        'lambda_ = Conductivite_thermique_beton(My_materials.lNormal, My_materials.lANFrance, My_materials.lGeneration1, Temp_0(i_, j_))
                        lambda_ = EN_Feu.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, Temp_0(i_, j_))

                        'rho_ = Masse_volumique_beton(My_materials.lNormal, My_materials.lRhoCVariable, My_materials.lGeneration1, My_materials.RhoC, Temp_0(i_, j_))
                        rho_ = EN_Feu.Masse_volumique_beton(lNormal, My_materials.lRhoCVariable, lGeneration1, My_materials.RhoC, Temp_0(i_, j_))

                        'cp_ = Chaleur_specifique_beton(My_materials.lNormal, My_materials.U_, My_materials.lGeneration1, Temp_0(i_, j_))
                        cp_ = EN_Feu.Chaleur_specifique_beton(lNormal, val_U, lGeneration1, Temp_0(i_, j_))

                    End If

                    h_lb_2 = 0.5 * My_mesh.Tab_mesh_y(i_) / lambda_
                    h_bb_2 = 0.5 * My_mesh.Tab_mesh_z(j_) / lambda_
                    h_tb_2 = 0.5 * My_mesh.Tab_mesh_z(j_) / lambda_
                    h_rb_2 = 0.5 * My_mesh.Tab_mesh_y(i_) / lambda_

                    cst_lb = dth_lb / (h_lb_1 + h_lb_2)
                    cst_bb = dth_bb / (h_bb_1 + h_bb_2)
                    cst_tb = dth_tb / (h_tb_1 + h_tb_2)
                    cst_rb = dth_rb / (h_rb_1 + h_rb_2)

                    q_lb = My_mesh.Tab_mesh_y(i_) * DeltaT * cst_lb
                    q_bb = My_mesh.Tab_mesh_z(j_) * DeltaT * cst_bb
                    q_tb = My_mesh.Tab_mesh_z(j_) * DeltaT * cst_tb
                    q_rb = My_mesh.Tab_mesh_y(i_) * DeltaT * cst_rb

                    q_y = q_lb + q_rb
                    q_z = q_bb + q_tb

                    rho_cp = rho_ * cp_
                    k_1 = rho_cp * My_mesh.Tab_mesh_y(i_) * My_mesh.Tab_mesh_z(j_)
                    val_dth = (q_y + q_z) / k_1

                    Temp_1(i_, j_) = Temp_0(i_, j_) + val_dth
                    My_mesh.Tab_mesh_temp(i_, j_) = Temp_1(i_, j_)

                End If

            Next

        Next

        'Affectation éventuelle de températures des mailles hors [ind_0;ind_1]
        With My_mesh

            If .ind_0 > 0 Then
                If My_time < My_fire.Duration Then
                    i_ = .ind_0 - 1 'colonne de mailles adjacentes a la borne inferieure de l'intervalle calculé
                    For j_ = 0 To .nb_cells_z - 1
                        .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(.ind_0, j_)
                    Next
                Else 'toutes les colonnes de mailles à gauche de la borne inferieure de l'intervalle calculé
                    For j_ = 0 To .nb_cells_z - 1
                        For i_ = 0 To .ind_0 - 1
                            .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(.ind_0, j_)
                        Next
                    Next
                End If
            End If

            If .ind_1 < .nb_cells_y - 1 Then
                If My_time < My_fire.Duration Then
                    i_ = .ind_1 + 1     'colonne de mailles adjacentes a la borne inferieure de l'intervalle calculé
                    For j_ = 0 To .nb_cells_z - 1
                        .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(.ind_1, j_)
                    Next
                Else        'toutes les colonnes de mailles à droite de la borne inferieure de l'intervalle calculé
                    For j_ = 0 To .nb_cells_z - 1
                        For i_ = .ind_1 + 1 To .nb_cells_y - 1
                            .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(.ind_1, j_)
                        Next
                    Next
                End If
            End If

        End With

    End Sub



#End Region



End Class
