Public Class cls_EchauffementSlimFEM
    '===== CLASSE POUR LE CALCUL NUMERIQUE DE L'ECHAUFFEMENT D'UNE POUTRE DE PLANCHER MINCE ======

#Region " Solveur Numérique "

    Public Sub MAJ_Matrice_calcul(ByVal My_mesh As cls_MaillageSlimFloor, ByRef My_temp(,) As Double)

        '---------------------------------------------------------------------------------------------------------
        '   21/04/2026 :  Création par GiB
        '---------------------------------------------------------------------------------------------------------
        '   Actualisation de la matrice sur laquelle sont calculés les incréments de température
        '---------------------------------------------------------------------------------------------------------
        '   My_mesh     [E] :   classe de maillage d'une poutre de plancher mince
        '   My_temp     [E] :   matrice sur laquelle sont calculés les incréments de temps
        '---------------------------------------------------------------------------------------------------------

        '--) Declaration
        Dim i_ As Integer
        Dim j_ As Integer
        Dim i_y As Integer

        '--) Traitement
        For i_ = My_mesh.ind_2 To My_mesh.ind_1
            i_y = i_ - My_mesh.ind_2
            For j_ = 0 To My_mesh.nb_cells_z - 1
                My_temp(i_y, j_) = My_mesh.Tab_mesh_temp(i_, j_)
            Next
        Next

    End Sub

    Public Function Increment_Temps(TimeT As Decimal) As Decimal
        '---------------------------------------------------------------------------------------------------------
        '   21/04/26 :  Création par GiB)
        '---------------------------------------------------------------------------------------------------------
        '   Incrément de temps variable
        '---------------------------------------------------------------------------------------------------------
        '   TimeT       [E] :   Temps actuel
        '   lInter      [E] :   Poutre intérmédiaire ou pas
        '---------------------------------------------------------------------------------------------------------

        '--) Declaration et initialisation
        Dim DeltaT As Decimal = 0.2

        '--) Traitement
        If IsGreaterOrEqual(TimeT, 600.0) AndAlso IsSmaller(TimeT, 900.0) Then
            DeltaT = 0.25
        ElseIf IsGreaterOrEqual(TimeT, 900.0) Then
            If IsSmaller(TimeT, 2700.0) Then
                DeltaT = 0.3
            ElseIf IsSmaller(TimeT, 3600.0) Then
                DeltaT = 0.4
            ElseIf IsSmaller(TimeT, 7200.0) Then
                DeltaT = 0.5
            Else
                DeltaT = 0.6
            End If
        End If

        Return DeltaT

    End Function

    Public Sub Symetrie_echauffement(ByRef My_mesh As cls_MaillageSlimFloor)

        '---------------------------------------------------------------------------------------------------------
        '   21/04/2026 :  Création par GiB
        '---------------------------------------------------------------------------------------------------------
        '   Remplissage de la moitié gauche de la matrice de température par symétrie
        '---------------------------------------------------------------------------------------------------------
        '   My_mesh     [E] :   classe de maillage d'une poutre de plancher mince
        '---------------------------------------------------------------------------------------------------------

        Dim nbY As Integer = My_mesh.nb_cells_y
        Dim UpperBY As Integer = nbY - 1

        With My_mesh

            If Not IsEqual(.ind_0, .ind_2) Then

                '--) Declaration
                Dim i_ As Integer
                Dim ii_ As Integer
                Dim j_ As Integer

                '--) Traitement
                For j_ = 0 To .nb_cells_z - 1
                    For i_ = .ind_0 To .ind_2 - 1
                        'ii_ = .Tab_mesh_y.GetUpperBound(0) - i_
                        ii_ = UpperBY - i_
                        .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(ii_, j_)
                    Next
                Next
            End If

        End With

    End Sub

    Public Sub Constance_echauffement_gauche(ByRef My_mesh As cls_MaillageSlimFloor)

        '---------------------------------------------------------------------------------------------------------
        '   21/04/2026 :  Création par GiB
        '---------------------------------------------------------------------------------------------------------
        '   Remplissage de la moitié gauche de la matrice de température au-delà de delta_y
        '---------------------------------------------------------------------------------------------------------
        '   My_mesh     [E] :   classe de maillage d'une poutre de plancher mince
        '---------------------------------------------------------------------------------------------------------

        With My_mesh

            If IsGreater(.ind_0, .Tab_mesh_y.GetLowerBound(0)) Then

                '--) Declaration
                Dim i_ As Integer
                Dim j_ As Integer

                '--) Traitement
                For j_ = 0 To .nb_cells_z - 1
                    For i_ = 0 To .ind_0 - 1
                        .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(.ind_0, j_)
                    Next
                Next

            End If

        End With

    End Sub

    Public Sub Constance_echauffement_droite(ByRef My_mesh As cls_MaillageSlimFloor)

        '---------------------------------------------------------------------------------------------------------
        '   21/04/2026 :  Création par GiB
        '---------------------------------------------------------------------------------------------------------
        '   Remplissage de la moitié droite de la matrice de température au-delà de delta_y
        '---------------------------------------------------------------------------------------------------------
        '   My_mesh     [E] :   classe de maillage d'une poutre de plancher mince
        '---------------------------------------------------------------------------------------------------------

        With My_mesh

            If IsSmaller(.ind_1, .Tab_mesh_y.GetUpperBound(0)) Then

                '--) Declaration
                Dim i_ As Integer
                Dim j_ As Integer

                '--) Traitement
                For j_ = 0 To .nb_cells_z - 1
                    For i_ = .ind_1 + 1 To .nb_cells_y - 1
                        .Tab_mesh_temp(i_, j_) = .Tab_mesh_temp(.ind_1, j_)
                    Next
                Next

            End If

        End With

    End Sub

    Public Sub Echauffement_vide(ByRef My_mesh As cls_MaillageSlimFloor, ByVal TempG As Double)

        '---------------------------------------------------------------------------------------------------------
        '   21/04/2026 :  Création par GiB
        '---------------------------------------------------------------------------------------------------------
        '   Affectation de température des gaz chauds aux mailles du vide correspondantes
        '---------------------------------------------------------------------------------------------------------
        '   My_mesh     [E] :   classe de maillage d'une poutre de plancher mince
        '   TempG       [E] :   température des gaz chauds (°C)
        '---------------------------------------------------------------------------------------------------------

        With My_mesh

            '--) Declaration
            Dim i_ As Integer
            Dim j_ As Integer

            '--) Traitement
            For j_ = 0 To .nb_cells_z - 1
                For i_ = 0 To .nb_cells_y - 1
                    If .Tab_mesh_mat(i_, j_) = cls_MaillageSlimFloor.MATVIDEOUVERT Then
                        .Tab_mesh_temp(i_, j_) = TempG
                    End If
                Next
            Next

        End With

    End Sub

    Public Function Valeur_emissivite(val_mat As Integer, val_mat_a_0 As Integer, val_mat_a_1 As Integer, val_mat_c As Integer, val_temp As Double,
                                     myparamfeu As cls_OptionsFeu, EN_feu As cls_EurocodesFeu) As Double

        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de l'émissivité de surface d'une face extérieure de maille
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   val_mat         [E] :   numéro du matériau de la maille (i,j)
        '   val_mat_a_0     [E] :   numéro du matériau de l'acier du plat ou de la semelle inférieur(e)
        '   val_mat_a_1     [E] :   numéro du matériau de l'acier d'armature
        '   val_mat_c       [E] :   numéro du matériau du béton
        '   myparamfeu      [E] :   classe pour gérer les options de calcul au feu
        '   EN_feu          [E] :   classe de valeurs et de fonctions Eurocodes
        '   val_epsilon     [S] :émissivité de surface
        '---------------------------------------------------------------------------------------------------------------------------------------

        '--) Declaration
        Dim val_epsilon As Double

        '--) Traitement
        If IsGreaterOrEqual(val_mat, val_mat_a_0) AndAlso IsSmallerOrEqual(val_mat, val_mat_a_1) Then
            val_epsilon = EN_feu.EmissiviteAcier(val_temp, myparamfeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Galvanise)
        ElseIf IsEqual(val_mat, val_mat_c) Then
            val_epsilon = myparamfeu.EmissivityC
        End If

        Return val_epsilon

    End Function

    Public Sub Valeur_densites_flux(TempG As Double, TempG_Kpui4 As Double, TempRef_Kpui4 As Double, Temp_ As Double, myparamfeu As cls_OptionsFeu,
                                    prop_expo As Boolean, prop_noex As Boolean, val_epsilon As Double,
                                    ByRef h_net_ce As Double, ByRef h_net_re As Double, ByRef h_net_de As Double,
                                    ByRef h_net_cn As Double, ByRef h_net_rn As Double, ByRef h_net_dn As Double)

        '---------------------------------------------------------------------------------------------------------------------------------------
        '   21/04/2026 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul des densités de flux thermique sur une face extérieure de maille
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   TempG           [E] :   température des gaz chauds (°C)
        '   TempG_Kpui4     [E] :   température des gaz chauds (°K) à la puissance 4
        '   TempRef_Kpui4   [E] :   température de référence (°K) à la puissance 4
        '   Temp_           [E] :   température de la maille (°C)
        '   myparamfeu      [E] :   classe pour gérer les options de calcul au feu
        '   prop_expo       [E] :   face exposée --> true
        '   prop_noex       [E] :   face non exposée --> true
        '   val_epsilon     [E] :   émissivité de surface de la face extérieure

        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Paramètres en sortie:
        '   h_net_ce        [S] :   densité de flux convectif sur une face exposée (W/m²)
        '   h_net_re        [S] :   densité de flux radiatif sur une face exposée (W/m²)
        '   h_net_ce        [S] :   densité totale de flux sur une face exposée (W/m²)
        '   h_net_cn        [S] :   densité de flux convectif sur une face non exposée (W/m²)
        '   h_net_rn        [S] :   densité de flux radiatif sur une face non exposée (W/m²)
        '   h_net_dn        [S] :   densité totale de flux sur une face non exposée (W/m²)
        '---------------------------------------------------------------------------------------------------------------------------------------

        Dim Temp_K As Double = Temp_ + cls_OptionsFeu.DELTAKELVIN
        Dim Temp_Kpui4 As Double = Puissance4(Temp_K)

        If prop_expo Then
            'Face exposee

            With myparamfeu
                h_net_ce = .ConvectionCoef * (TempG - Temp_)    'densite de flux convectif
                h_net_re = .EmissivityFire * val_epsilon * cls_OptionsFeu.BOLTZMANN * (TempG_Kpui4 - Temp_Kpui4)    'densite de flux radiatif
                h_net_de = h_net_ce + h_net_re  'densite de flux net
            End With

        ElseIf prop_noex Then
            'Face non exposee

            With myparamfeu
                h_net_cn = .ConvectionCoefDalle * (myparamfeu.TempRef - Temp_)    'densite de flux convectif
                'h_net_rn = .EmissivityFire * val_epsilon * cls_OptionsFeu.BOLTZMANN * ((myparamfeu.TempRef + 273.0) ^ 4 - Temp_Kpui4)    'densite de flux radiatif
                h_net_rn = .EmissivityFire * val_epsilon * cls_OptionsFeu.BOLTZMANN * (TempRef_Kpui4 - Temp_Kpui4)    'densite de flux radiatif
                h_net_dn = h_net_cn + h_net_rn  'densite de flux net
            End With

        End If

    End Sub

    Private Function Puissance4(x As Double) As Double
        Dim x2 As Double = x * x
        Return x2 * x2
    End Function


    Public Function Valeur_conductivite_thermique(val_mat As Integer, val_mat_a_0 As Integer, val_mat_a_1 As Integer, val_mat_c As Integer,
                                                  val_temp As Double,
                                                  lNormal As Boolean, lANFrance As Boolean, lGeneration1 As Boolean, EN_feu As cls_EurocodesFeu) As Double


        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la conductivité thermique d'une maille
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   val_mat         [E] :   numéro du matériau de la maille (i,j)
        '   val_mat_a_0     [E] :   numéro du matériau de l'acier du plat soudé
        '   val_mat_a_1     [E] :   numéro du matériau de l'acier d'armature
        '   val_mat_c       [E] :   numéro du matériau du béton
        '   val_temp        [E] :   numéro du matériau du béton
        '   lNormal         [E] :   type de béton : normal ou léger
        '   lANFrance       [E] :   application de l'Annexe Nationale Française
        '   lGeneration1    [E] :   1re génération d'Eurocodes (True) ou 2e généaration d'Eurocodes (False)
        '   EN_feu          [E] :   classe de valeurs et de fonctions Eurocodes
        '   val_lambda      [S] :   conductivité thermique
        '---------------------------------------------------------------------------------------------------------------------------------------

        '--) Declaration
        Dim val_lambda As Double

        '--) Traitement
        If IsGreaterOrEqual(val_mat, val_mat_a_0) AndAlso IsSmallerOrEqual(val_mat, val_mat_a_1) Then  'acier
            val_lambda = EN_feu.Conductivite_thermique_acier(val_temp)
        ElseIf IsEqual(val_mat, val_mat_c) Then 'beton
            val_lambda = EN_feu.Conductivite_thermique_beton(lNormal, lANFrance, lGeneration1, val_temp)
        End If

        Return val_lambda

    End Function

    Public Function Valeur_masse_volumique(val_mat As Integer, val_mat_a_0 As Integer, val_mat_a_1 As Integer, val_mat_c As Integer,
                                  val_temp As Double,
                                  lNormal As Boolean, lRhoCVariable As Boolean, lGeneration1 As Boolean, RhoC As Double, EN_feu As cls_EurocodesFeu) As Double

        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la conductivité thermique d'une maille
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   val_mat         [E] :   numéro du matériau de la maille (i,j)
        '   val_mat_a_0     [E] :   numéro du matériau de l'acier du plat soudé
        '   val_mat_a_1     [E] :   numéro du matériau de l'acier d'armature
        '   val_mat_c       [E] :   numéro du matériau du béton
        '   val_temp        [E] :   numéro du matériau du béton
        '   lNormal         [E] :   type de béton : normal (True) ou léger (False)
        '   lRhocVariable   [E] :   masse volumique variable
        '   lGeneration1    [E] :   1re génération d'Eurocodes (True) ou 2e généaration d'Eurocodes (False)
        '   RhoC            [E] :   masse volumique du béton à température normale
        '   EN_feu          [E] :   classe de valeurs et de fonctions Eurocodes
        '   val_rho         [S] :   masse volumique
        '---------------------------------------------------------------------------------------------------------------------------------------

        '--) Declaration
        Dim val_rho As Double

        '--) Traitement
        If IsGreaterOrEqual(val_mat, val_mat_a_0) AndAlso IsSmallerOrEqual(val_mat, val_mat_a_1) Then  'acier
            val_rho = cls_Acier.RHOACIER
        ElseIf IsEqual(val_mat, val_mat_c) Then 'beton
            val_rho = EN_feu.Masse_volumique_beton(lNormal, lRhoCVariable, lGeneration1, RhoC, val_temp)
        End If

        Return val_rho

    End Function

    Public Function Valeur_chaleur_specifique(val_mat As Integer, val_mat_a_0 As Integer, val_mat_a_1 As Integer, val_mat_c As Integer,
                                  val_temp As Double,
                                  lNormal As Boolean, lGeneration1 As Boolean, val_U As Double, EN_feu As cls_EurocodesFeu) As Double

        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la chaleur spécifique d'une maille
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   val_mat         [E] :   numéro du matériau de la maille (i,j)
        '   val_mat_a_0     [E] :   numéro du matériau de l'acier du plat soudé
        '   val_mat_a_1     [E] :   numéro du matériau de l'acier d'armature
        '   val_mat_c       [E] :   numéro du matériau du béton
        '   val_temp        [E] :   numéro du matériau du béton
        '   lNormal         [E] :   type de béton : normal (True) ou léger (False)
        '   lGeneration1    [E] :   1re génération d'Eurocodes (True) ou 2e généaration d'Eurocodes (False)
        '   val_U           [E] :   teneur en eau du béton normal
        '   EN_feu          [E] :   classe de valeurs et de fonctions Eurocodes
        '   val_cp          [S] :   chaleur spécifique
        '---------------------------------------------------------------------------------------------------------------------------------------

        '--) Declaration
        Dim val_cp As Double

        '--) Traitement
        If IsGreaterOrEqual(val_mat, val_mat_a_0) AndAlso IsSmallerOrEqual(val_mat, val_mat_a_1) Then  'acier
            val_cp = EN_feu.ChaleurSpecifiqueAcier(val_temp)
        ElseIf IsEqual(val_mat, val_mat_c) Then 'beton
            val_cp = EN_feu.Chaleur_specifique_beton(lNormal, val_U, lGeneration1, val_temp)
        End If

        Return val_cp

    End Function

    Public Sub Calcul_thermique_Poutre_plancher_mince(ByRef my_Mesh As cls_MaillageSlimFloor, myParamFeu As cls_OptionsFeu,
                                                      my_Time As Double, DeltaT As Double, lTargetT As Boolean, TempG As Double,
                                                      val_U As Double, lNormal As Boolean, lANFrance As Boolean,
                                                      lGeneration1 As Boolean, RhoC As Double, lRhoCVariable As Boolean,
                                                      ByRef Temp_0(,) As Double, My_expo(,) As Boolean, My_nonexpo(,) As Boolean)
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - GiB
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Calcul de l'échauffement d'une dalle en béton exposée à l'incendie normalisé en sous-face
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   my_Mesh         [E] :   Classe de caractéristiques du maillage (taille, matériaux et températures)
        '   My_fire         [E] :   Classe de caractéristiques du calcul au feu
        '   My_time         [E] :   Temps actuel du calcul (s) (étape précécente, t=my_Time-DeltaT)
        '   DeltaT          [E] :   Pas de temps du calcul (s)
        '   lTargetT        [E] :   indique si le temps de calcul est inférieur à la valeur cible (true) ou s'il correspond à la valeur cible (false)
        '   TempG           [E] :   Température des gaz chauds au contact de la face exposée de la dalle (°C)
        '   val_U           [E] :   taux d'humidite (%)
        '   lNormal         [E] :   indique si béton NC (true) ou LC (false)
        '   lGeneration1    [E] :   indique si génération 1 ou génération 2 des Eurocodes
        '   lANFrance       [E] :   indique si on utilise l’Annexe Nationale française de l’EN 1994-1-2
        '   RhoC            [E] :   masse volumique du béton (à froid)                              (kg/m3)
        '   lRhoCVariable   [E] :   indique si on utilise la formule de rhoc de l’EN 1994-1-2 variant en fonction de la température, pour un béton normal.
        '                           Si non, on utilise la valeur constante du paramètre RhoC.
        '                           Pour le béton léger, on utilise toujours RhoC.                        (kg/m3)
        '   Temp_0          [E] :   températures à l'instant t
        '   My_expo         [E] :   face(s) exposee(s) de la maille
        '   My_noexo        [E] :   face non exposee de la maille

        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Paramètre en sortie:
        '   My_mesh.Tab_mesh_temp(i,j)       Température dans chaque maille (i,j) de la poutre, au temps t + DeltaT  (°C)
        '---------------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim EN_Feu As New cls_EurocodesFeu
        Dim TempG_K As Double = TempG + cls_OptionsFeu.DELTAKELVIN

        Dim TempG_Kpui4 As Double = Puissance4(TempG_K)
        Dim TempRef_Kpui4 As Double = Puissance4(myParamFeu.TempRef + cls_OptionsFeu.DELTAKELVIN)

        '%% Temperature d'une maille
        Dim th_ As Double   'GiB 20/04/2026

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

        Dim i_ As Integer, j_ As Integer, i_mat As Integer, j_mat As Integer, ii_ As Integer, jj_ As Integer, i_y As Integer, ii_y As Integer
        Dim prop_expo As Boolean
        Dim prop_noex As Boolean

        Dim NbY As Integer = my_Mesh.nb_cells_y
        Dim NbZ As Integer = my_Mesh.nb_cells_z

        '--( Caches locaux pour éviter les appels multiples à des propriétés de la classe my_Mesh
        Dim tabMat = my_Mesh.Tab_mesh_mat
        Dim tabY = my_Mesh.Tab_mesh_y
        Dim tabZ = my_Mesh.Tab_mesh_z
        Dim UpperBz As Integer = Temp_0.GetUpperBound(1)
        Dim LowerBz As Integer = Temp_0.GetLowerBound(1)
        Dim Ind_1 As Integer = my_Mesh.ind_1
        Dim Ind_2 As Integer = my_Mesh.ind_2
        Dim TempRef As Double = Convert.ToDouble(myParamFeu.TempRef)

        '--( 

        Dim i_mat_a_0 As Integer
        Dim i_mat_a_1 As Integer
        Dim i_mat_c As Integer
        Dim i_mat_v_0 As Integer

        '--( Initialisation 

        i_mat_a_0 = cls_MaillageSlimFloor.MATACIERPLAT
        i_mat_a_1 = cls_MaillageSlimFloor.MATARMA
        i_mat_c = cls_MaillageSlimFloor.MATBETON
        i_mat_v_0 = cls_MaillageSlimFloor.MATVIDEOUVERT

        '--( Boucle sur les mailles entre ind_0 et ind_1 pour calculer la temperature de chacune à l'instant val_t + val_DeltaT

        For j_ = 0 To NbZ - 1

            For i_ = Ind_2 To Ind_1

                'q_lb : échange entre (i_-1, j_) et (i_,j_) : "left boundary"
                'q_tb : échange entre (i_, j_-1) et (i_,j_) : "top boundary"
                'q_bb : échange entre (i_, j_-1) et (i_,j_) : "bottom boundary"
                'q_rb : échange entre (i_+1, j_) et (i_,j_) : "right boundary"

                'q_y : q_lb + q_rb
                'q_z : q_bb + q_tb

                '%%Numero de materiau de la maille (i_,j_)
                'i_mat = my_Mesh.Tab_mesh_mat(i_, j_)
                i_mat = tabMat(i_, j_)

                '%%Conditions d'exposition au feu
                'prop_expo = False
                If IsGreaterOrEqual(i_mat, i_mat_a_0) Then

                    '%%Initialisation de valeurs
                    val_dth = 0.0
                    dth_lb = 0.0 : dth_bb = 0.0 : dth_tb = 0.0 : dth_rb = 0.0
                    h_lb_1 = 0.0 : h_bb_1 = 0.0 : h_tb_1 = 0.0 : h_rb_1 = 0.0
                    h_lb_2 = 0.0 : h_bb_2 = 0.0 : h_tb_2 = 0.0 : h_rb_2 = 0.0

                    'Indice de ligne des tableaux Temp_0 et Temp_1
                    'i_y = i_ - my_Mesh.ind_2
                    i_y = i_ - Ind_2

                    prop_expo = My_expo(i_y, j_)
                    prop_noex = My_nonexpo(i_y, j_)

                    'Faces exterieures de la maille
                    If prop_expo Or prop_noex Then
                        'Emissivite de surface
                        val_epsilon = Me.Valeur_emissivite(i_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(i_y, j_), myParamFeu, EN_Feu)

                        'Densités de flux net
                        Me.Valeur_densites_flux(TempG, TempG_Kpui4, TempRef_Kpui4, Temp_0(i_y, j_), myParamFeu,
                                                prop_expo, prop_noex, val_epsilon,
                                                h_net_ce, h_net_re, h_net_de, h_net_cn, h_net_rn, h_net_dn)
                    End If

                    '1er terme du denominateur de q_lb : face de gauche de la maille (i_,j_)
                    'If IsEqual(i_, my_Mesh.ind_2) Then
                    If IsEqual(i_, Ind_2) Then

                        dth_lb = 0.0
                        h_lb_1 = 0.0

                    Else

                        ii_ = i_ - 1
                        ii_y = i_y - 1

                        'j_mat = my_Mesh.Tab_mesh_mat(ii_, j_)
                        j_mat = tabMat(ii_, j_)

                        If prop_expo AndAlso j_mat = i_mat_v_0 Then        '$ face exposee

                            dth_lb = TempG - Temp_0(i_y, j_)         '$ ecart de temperature entre les gaz chauds et la maille (i_-1,j_)
                            h_lb_1 = dth_lb / h_net_de

                        ElseIf IsGreaterOrEqual(j_mat, i_mat_a_0) Then

                            dth_lb = Temp_0(ii_y, j_) - Temp_0(i_y, j_) 'ecart de temperature entre les mailles (i_-1,j_) et (i_,j_)
                            lambda_Lb = Me.Valeur_conductivite_thermique(j_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(ii_y, j_), lNormal, lANFrance, lGeneration1, EN_Feu)
                            'h_lb_1 = 0.5 * my_Mesh.Tab_mesh_y(ii_) / lambda_Lb
                            h_lb_1 = 0.5 * tabY(ii_) / lambda_Lb

                        End If

                    End If

                    '1er terme du denominateur de q_bb : face inferieure de la maille (i_,j_)
                    jj_ = j_ - 1
                    'If prop_expo AndAlso (IsEqual(j_, Temp_0.GetLowerBound(1)) OrElse IsEqual(my_Mesh.Tab_mesh_mat(i_, jj_), i_mat_v_0)) Then
                    If prop_expo AndAlso (IsEqual(j_, LowerBz) OrElse IsEqual(tabMat(i_, jj_), i_mat_v_0)) Then
                        dth_bb = TempG - Temp_0(i_y, j_) 'ecart de temperature entre les gaz chauds et la maille (i_,j_)
                        h_bb_1 = dth_bb / h_net_de
                        'ElseIf IsGreaterOrEqual(my_Mesh.Tab_mesh_mat(i_, jj_), i_mat_a_0) Then
                    ElseIf IsGreaterOrEqual(tabMat(i_, jj_), i_mat_a_0) Then
                        dth_bb = Temp_0(i_y, jj_) - Temp_0(i_y, j_) 'ecart de temperature entre les mailles (i_,j_-1) et (i_,j_)
                        'j_mat = my_Mesh.Tab_mesh_mat(i_, jj_)
                        j_mat = tabMat(i_, jj_)
                        lambda_Bb = Me.Valeur_conductivite_thermique(j_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(i_y, jj_), lNormal, lANFrance, lGeneration1, EN_Feu)
                        'h_bb_1 = 0.5 * my_Mesh.Tab_mesh_z(jj_) / lambda_Bb
                        h_bb_1 = 0.5 * tabZ(jj_) / lambda_Bb
                    End If

                    '1er terme du denominateur de q_tb : face superieure de la maille (i_,j_)
                    'If IsSmaller(j_, Temp_0.GetUpperBound(1)) Then
                    If IsSmaller(j_, UpperBz) Then

                        jj_ = j_ + 1
                        'j_mat = my_Mesh.Tab_mesh_mat(i_, jj_)
                        j_mat = tabMat(i_, jj_)

                        If prop_expo AndAlso IsEqual(j_mat, i_mat_v_0) Then    'face superieure exposee
                            dth_tb = TempG - Temp_0(i_y, j_) 'ecart de temperature entre les gaz chauds et la maille (i_,j_)
                            h_tb_1 = dth_tb / h_net_de
                        ElseIf IsGreaterOrEqual(j_mat, i_mat_a_0) Then
                            dth_tb = Temp_0(i_y, jj_) - Temp_0(i_y, j_) 'ecart de temperature entre les mailles (i_,j_+1) et (i_,j_)
                            lambda_Tb = Me.Valeur_conductivite_thermique(j_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(i_y, jj_), lNormal, lANFrance, lGeneration1, EN_Feu)
                            'h_tb_1 = 0.5 * my_Mesh.Tab_mesh_z(jj_) / lambda_Tb
                            h_tb_1 = 0.5 * tabZ(jj_) / lambda_Tb
                        End If

                    ElseIf prop_noex Then    'face non exposee
                        dth_tb = TempRef - Temp_0(i_y, j_) 'ecart de temperature entre l'air ambiant et la maille (i_,j_)
                        If IsGreater(Math.Abs(dth_tb), 0) Then
                            h_tb_1 = dth_tb / h_net_dn
                        End If
                    End If

                    '1er terme du denominateur de q_rb : face de droite de la maille (i_,j_)
                    'If IsSmaller(i_, my_Mesh.ind_1) Then
                    If IsSmaller(i_, Ind_1) Then

                        ii_ = i_ + 1
                        ii_y = i_y + 1
                        'j_mat = my_Mesh.Tab_mesh_mat(ii_, j_)
                        j_mat = tabMat(ii_, j_)

                        If prop_expo AndAlso IsEqual(j_mat, i_mat_v_0) Then
                            dth_rb = TempG - Temp_0(i_y, j_) 'ecart de temperature entre les gaz chauds et la maille (i_,j_)
                            h_rb_1 = dth_rb / h_net_de

                        ElseIf IsGreaterOrEqual(j_mat, i_mat_a_0) Then
                            dth_rb = Temp_0(ii_y, j_) - Temp_0(i_y, j_) 'ecart de temperature entre les mailles (i_+1,j_) et (i_,j_)
                            lambda_Rb = Me.Valeur_conductivite_thermique(j_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(ii_y, jj_), lNormal, lANFrance, lGeneration1, EN_Feu)
                            'h_rb_1 = 0.5 * my_Mesh.Tab_mesh_y(ii_) / lambda_Rb
                            h_rb_1 = 0.5 * tabY(ii_) / lambda_Rb
                        End If
                    Else
                        dth_rb = 0.0
                        h_rb_1 = 0.0
                    End If

                    '2e terme du denominateur de qi
                    lambda_ = Me.Valeur_conductivite_thermique(i_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(i_y, j_), lNormal, lANFrance, lGeneration1, EN_Feu)
                    rho_ = Me.Valeur_masse_volumique(i_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(i_y, j_), lNormal, lRhoCVariable, lGeneration1, RhoC, EN_Feu)
                    cp_ = Me.Valeur_chaleur_specifique(i_mat, i_mat_a_0, i_mat_a_1, i_mat_c, Temp_0(i_y, j_), lNormal, lGeneration1, val_U, EN_Feu)

                    'h_lb_2 = 0.5 * my_Mesh.Tab_mesh_y(i_) / lambda_
                    'h_bb_2 = 0.5 * my_Mesh.Tab_mesh_z(j_) / lambda_
                    'h_tb_2 = 0.5 * my_Mesh.Tab_mesh_z(j_) / lambda_
                    'h_rb_2 = 0.5 * my_Mesh.Tab_mesh_y(i_) / lambda_
                    h_lb_2 = 0.5 * tabY(i_) / lambda_
                    h_bb_2 = 0.5 * tabZ(j_) / lambda_
                    h_tb_2 = 0.5 * tabZ(j_) / lambda_
                    h_rb_2 = 0.5 * tabY(i_) / lambda_

                    cst_lb = dth_lb / (h_lb_1 + h_lb_2)
                    cst_bb = dth_bb / (h_bb_1 + h_bb_2)
                    cst_tb = dth_tb / (h_tb_1 + h_tb_2)
                    cst_rb = dth_rb / (h_rb_1 + h_rb_2)

                    'q_lb = my_Mesh.Tab_mesh_y(i_) * DeltaT * cst_lb
                    'q_bb = my_Mesh.Tab_mesh_z(j_) * DeltaT * cst_bb
                    'q_tb = my_Mesh.Tab_mesh_z(j_) * DeltaT * cst_tb
                    'q_rb = my_Mesh.Tab_mesh_y(i_) * DeltaT * cst_rb
                    q_lb = tabY(i_) * DeltaT * cst_lb
                    q_bb = tabZ(j_) * DeltaT * cst_bb
                    q_tb = tabZ(j_) * DeltaT * cst_tb
                    q_rb = tabY(i_) * DeltaT * cst_rb

                    q_y = q_lb + q_rb
                    q_z = q_bb + q_tb

                    rho_cp = rho_ * cp_
                    'k_1 = rho_cp * my_Mesh.Tab_mesh_y(i_) * my_Mesh.Tab_mesh_z(j_)
                    k_1 = rho_cp * tabY(i_) * tabZ(j_)
                    val_dth = (q_y + q_z) / k_1

                    'GiB 01/12/2025 : température supérieure ou égale à 20°C !
                    th_ = Math.Max(TempRef, Temp_0(i_y, j_) + val_dth)

                    'If IsGreater(th_, TempG) AndAlso IsEqual(TempG, EN_Feu.TemperatureGazISO(my_Time)) Then
                    If IsGreater(th_, TempG) Then
                        th_ = Math.Min(th_, TempG)
                    End If

                    my_Mesh.Tab_mesh_temp(i_, j_) = th_

                End If

            Next

        Next

        'Affectation éventuelle de températures des mailles hors [ind_0;ind_1] en fin de calcul

        If Not lTargetT Then

            'Remplissage des mailles non calculées
            Me.Symetrie_echauffement(my_Mesh)
            Me.Constance_echauffement_gauche(my_Mesh)
            Me.Constance_echauffement_droite(my_Mesh)
            Me.Echauffement_vide(my_Mesh, TempG)

        End If

    End Sub

#End Region

End Class
