Public Class cls_MaillageSlimFloor

#Region " Déclarations "

	Const DIMDEF As Integer = 10000                     ' Dimension initiale des tableaux de maillage

	Public Const BMAXDAL As Double = 0.4                ' Largeur limite de dalle pour prise en compte d'un effet 2D dans le calcul thermique (m)

	Public Const MATVIDEFERME As Integer = -2           ' vide d'une cavite fermee (espace entre le mur et le profile metallique d'une poutre de rive)
	Public Const MATVIDEOUVERT As Integer = -1          ' vide d'une cavite ouverte
	Public Const MATACIERPLAT As Integer = 0            ' acier de construction du plat soudé
	Public Const MATACIERSEMI As Integer = 1            ' acier de construction de la semelle inférieure
	Public Const MATACIERAME As Integer = 2             ' acier de construction de l'ame
	Public Const MATACIERSEMS As Integer = 3            ' acier de construction de la semelle supérieure
	Public Const MATACIERSOUD As Integer = 4            ' acier de soudure du plat
	Public Const MATARMA As Integer = 5                 ' acier d'armature
	Public Const MATBETON As Integer = 6                ' béton de la dalle

#End Region

#Region " Attributs "

	Public nb_cells_y As Integer                ' nombre de mailles suivant l'axe fort
    Public nb_cells_z As Integer                ' nombre de mailles suivant l'axe faible
    Public Tab_mesh_y() As Double               ' densité du maillage (taille de la maille (i,j)) suivant l'axe y       (m)
    Public Tab_mesh_z() As Double               ' densité du maillage (taille de la maille (i,j)) suivant l'axe z       (m)
    Public Tab_mesh_cent_y(,) As Double         ' abscisse du centre de chaque maille (i,j)    (m)
    Public Tab_mesh_cent_z(,) As Double         ' ordonnée du centre de chaque maille (i,j)    (m)
    Public Tab_mesh_mat(,) As Integer           ' numéro de matériau de chaque maille (i,j)
    Public Tab_mesh_temp(,) As Double           ' tempérarature de chaque maille (i,j) à un instant donné
    Public ind_0 As Integer                     ' indice de la maille suivant l'axe fort à partir de laquelle effectuer le calcul de transfert thermique
    Public ind_1 As Integer                     ' indice de la maille suivant l'axe fort jusqu'à laquelle effectuer le calcul de transfert thermique
    Public ind_2 As Integer                     ' indice de la maille suivant l'axe fort à droite de l'axe de symétrie de l'âme

	Public y_min, y_max As Double

#End Region

#Region " Constructeur "

	Public Sub New()

    End Sub

#End Region

#Region " Préparation du maillage "

	Public Sub Creation_maillage_2D_poutre_plancher_mince(myProfil As cls_ProfilA, myDalle As cls_Dalle, paramF As cls_OptionsFeu,
													  bEffG As Decimal, bEffD As Decimal, lInter As Boolean, bApp As Decimal)
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   13/11/25 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Creation du maillage d'une section de poutre de plancher mince pour un calcul thermique par differences finies
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   myProfil        [E] :   Profilé métallique de la poutre
		'   myDalle         [E] :   Dalle en béton
		'   bEffG           [E] :   Largeur efficace de la dalle à gauche de l'âme de la poutre (m)  
		'   bEffD           [E] :   Largeur efficace de la dalle à droite de l'âme de la poutre (m)  
		'   lInter          [E] :   Indique si poutre intermédiaire ou poutre de rive (True/False) 
		'   bApp            [E] :   Largeur d'appui de la dalle sur la poutre (m)
		'---------------------------------------------------------------------------------------------------------------------------------------

		'--( Variables particulières

		Dim bAppG As Double = bApp                                             ' Largeur d'appui de la dalle à gauche 
		Dim bAppD As Double = bApp                                             ' Largeur d'appui de la dalle à droite 

		'--( Déclaration des variables

		Dim i_mat As Integer
		Dim n_dec As Integer, n_dec_y As Integer, n_dec_z As Integer            ' nombre de plans de coupe du maillage
		Dim val_bs_eq As Double                                                 ' côté des barres d'armature conduisant à une aire équivalente
		Dim val_size As Double                                                  ' densité (constante) du maillage
		Dim k_dens_y As Double                                                    ' coefficient d'amplification d'un maillage progressif suivant l'axe fort
		Dim k_dens_z As Double                                                    ' coefficient d'amplification d'un maillage progressif suivant l'axe faible
		Dim val_dens_y As Double                                                ' densité (initiale) d'un maillage progressif suivant l'axe fort
		Dim val_dens_z As Double                                                ' densité (initiale) d'un maillage progressif suivant l'axe faible
		Dim val_bw As Double                                                    ' gorge de soudure dans le modèle
		Dim Tab_dec(0 To 100) As Double
		Dim Tab_y(0 To 100) As Double                                           ' abscisse des plans de coupe du maillage
		Dim Tab_z(0 To 100) As Double                                           ' ordonnee des plans de coupe du maillage

		Dim y_0 As Double, z_0 As Double                                        ' abscisses et ordonnées de l'angle inférieur gauche de la section transversale
		Dim yp_1 As Double, yp_2 As Double, zp_1 As Double, zp_2 As Double      ' abscisses et ordonnées extrémales du plat soudé
		Dim yfi_1 As Double, yfi_2 As Double, zfi_1 As Double, zfi_2 As Double  ' abscisses et ordonnées extrémales de la semelle inférieure
		Dim yfs_1 As Double, yfs_2 As Double, zfs_1 As Double, zfs_2 As Double  ' abscisses et ordonnées extrémales de la semelle supérieure
		Dim yw_1 As Double, yw_2 As Double, zw_1 As Double, zw_2 As Double      ' abscisses et ordonnées extrémales de l'âme
		Dim ys_1 As Double, ys_2 As Double, ys_3 As Double, ys_4 As Double      ' abscisses extrémales des deux barres d'armature à gauche de l'âme
		Dim ys_5 As Double, ys_6 As Double, ys_7 As Double, ys_8 As Double      ' abscisses extrémales des deux barres d'armature à droite de l'âme
		Dim zs_1 As Double, zs_2 As Double, zs_3 As Double, zs_4 As Double      ' ordonnées extrémales des deux barres d'armature à gauche de l'âme
		Dim zs_5 As Double, zs_6 As Double, zs_7 As Double, zs_8 As Double      ' ordonnées extrémales des deux barres d'armature à droite de l'âme
		Dim ywd_1 As Double, ywd_2 As Double, zwd_1 As Double, zwd_2 As Double  ' abscisses et ordonnées extrémales de la soudure de gauche d'un plat inférieur
		Dim ywd_3 As Double, ywd_4 As Double, zwd_3 As Double, zwd_4 As Double  ' abscisses et ordonnées extrémales de la soudure de droite d'un plat inférieur
		Dim yv_1 As Double, yv_2 As Double, zv_1 As Double, zv_2 As Double      ' abscisses et ordonnées extrémales du vide de gauche 
		Dim yv_3 As Double, yv_4 As Double, zv_3 As Double, zv_4 As Double      ' abscisses et ordonnées extrémales du vide de droite 

		Dim prop_encl_open As Boolean   'nature ouverte ou fermee de la cavite entre le mur et le profile metallique d'une poutre de rive
		'Dim prop_ep_acier As Boolean    'maillage sur l'épaisseur d'une paroi en acier 

		Dim y_min As Double, y_max As Double, delta_y As Double
		Dim y_a_min As Double, y_a_max As Double    'abscisses extremales du plat ou de la semelle inferieur(e)

		Dim lSFB As Boolean
		Dim lSAB As Boolean
		Dim lIFB_A As Boolean
		Dim lIFB_B As Boolean
		Dim lDalMixte As Boolean
		Dim lPredalle As Boolean
		Dim lPlancherPrefa As Boolean   'GiB 18/11/2025 : ajout

		'--( Initilisation des variables

		lSFB = (myProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB)
		lSAB = (myProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB)
		lIFB_A = (myProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA)
		lIFB_B = (myProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB)
		lDalMixte = myDalle.lMixte
		lPredalle = myDalle.lPrefaPredalle
		lPlancherPrefa = myDalle.lPlancherPrefabriquee   'GiB 18/11/2025 : ajout

		'--( Dimension des tableaux

		ReDim Me.Tab_mesh_y(0 To DIMDEF)
		ReDim Me.Tab_mesh_z(0 To DIMDEF)
		ReDim Me.Tab_mesh_mat(0 To DIMDEF, 0 To DIMDEF)

		'--( CALCUL DES COORDONNEES DES POINTS D'INTERSECTION DES PLANS DE COUPE

		'GiB 19/11/2025 : augmentation de la largeur de gauche de la dalle d'une poutre de rive
		If (Not lInter) AndAlso IsSmallerOrEqual(bEffG, 0) Then
			If lSFB OrElse lIFB_A Then  'SFB ou IFB-A : demi-semelle supérieure
				bEffG = Math.Max(bEffG, 0.5 * myProfil.Bfs)
			ElseIf lIFB_B OrElse lSAB Then  'IFB-B ou SAB : demi-semelle inférieure
				bEffG = Math.Max(bEffG, 0.5 * myProfil.Bfi)
			End If
			bAppG = 0.0 'largeur d'appui de la dalle : nulle à gauche
		End If

		y_0 = -bEffG
		If (lSFB Or lIFB_A) Then
			z_0 = -myProfil.Plat_t
		Else
			z_0 = -myProfil.Tfi
		End If

		yp_1 = y_0 : yp_2 = y_0 : zp_1 = z_0 : zp_2 = z_0
		yfi_1 = y_0 : yfi_2 = y_0 : zfi_1 = z_0 : zfi_2 = z_0
		yfs_1 = y_0 : yfs_2 = y_0 : zfs_1 = z_0 : zfs_2 = z_0
		yw_1 = y_0 : yw_2 = y_0 : zw_1 = z_0 : zw_2 = z_0
		ys_1 = y_0 : ys_2 = y_0 : ys_3 = y_0 : ys_4 = y_0
		ys_5 = y_0 : ys_6 = y_0 : ys_7 = y_0 : ys_8 = y_0
		zs_1 = z_0 : zs_2 = z_0 : zs_3 = z_0 : zs_4 = z_0
		zs_5 = z_0 : zs_6 = z_0 : zs_7 = z_0 : zs_8 = z_0
		ywd_1 = y_0 : ywd_2 = y_0 : zwd_1 = z_0 : zwd_2 = z_0
		ywd_3 = y_0 : ywd_4 = y_0 : zwd_3 = z_0 : zwd_4 = z_0
		yv_1 = y_0 : yv_2 = y_0 : zv_1 = z_0 : zv_2 = z_0
		yv_3 = y_0 : yv_4 = y_0 : zv_3 = z_0 : zv_4 = z_0
		y_min = y_0 : y_max = y_0 + bEffG + bEffD

		delta_y = paramF.bEffect2D                          ' Taille limite de dalle modélisée de part et d'autre de l'âme
		k_dens_y = 1.1                                        ' coefficient d'amplification de la densité d'un mailalge progressif

		'====== TRAITEMENT PROFILE METALLIQUE ======

		'**( Traitement du Plat soudé eventuel
		If Not lSAB Then   'SFB ou IFB

			yp_1 = y_0 + bEffG

			If lInter Or lIFB_B Then            ' poutre intérieure ou IFB-B
				yp_1 -= 0.5 * myProfil.Plat_b
			ElseIf (Not lInter) Then            ' poutre de rive
				yp_1 -= 0.5 * myProfil.Bfs
			End If

			zp_1 = z_0

			If lIFB_B Then
				zp_1 += myProfil.ha - myProfil.Plat_t
			End If

			yp_2 = yp_1 + myProfil.Plat_b
			zp_2 = zp_1 + myProfil.Plat_t

		End If

		'**( Semelle inférieure éventuelle

		If Not lIFB_A Then   'SFB, IFB-B ou SAB

			yfi_1 = y_0 + bEffG - 0.5 * myProfil.Bfi
			zfi_1 = z_0

			If lSFB Then    'SFB
				zfi_1 += myProfil.Plat_t
			End If

			yfi_2 = yfi_1 + myProfil.Bfi
			zfi_2 = zfi_1 + myProfil.Tfi

			If lSAB Or lIFB_B Then
				y_min = yfi_1 - 0.5 * myProfil.Bfi
			End If

		End If

		'**( Semelle supérieure éventuelle
		If (Not lIFB_B) Then   'SFB, IFB-A ou SAB

			yfs_1 = y_0 + bEffG
			If lInter OrElse (lSFB Or lIFB_A) Then  'poutre intérieure ou SFB ou IFB-A
				yfs_1 -= 0.5 * myProfil.Bfs
			Else    'SAB de rive
				yfs_1 -= 0.5 * myProfil.Bfi
			End If
			zfs_1 = z_0 + myProfil.ha - myProfil.Tfs

			yfs_2 = yfs_1 + myProfil.Bfs
			zfs_2 = zfs_1 + myProfil.Tfs

		End If

		'**( Âme
		If (Not lIFB_A) Then 'SFB, IFB-B ou SAB
			yw_1 = yfi_1 + 0.5 * myProfil.Bfi - 0.5 * myProfil.Tw
			zw_1 = zfi_2
		Else    'IFB-A
			yw_1 = yfs_1 + 0.5 * myProfil.Bfs - 0.5 * myProfil.Tw
			zw_1 = zp_2
		End If
		yw_2 = yw_1 + myProfil.Tw

		If (Not lIFB_B) Then    ' SFB, IFB-A ou SAB
			zw_2 = zfs_1
		Else                    ' IFB-B
			zw_2 = zp_1
		End If

		'**( Soudures du plat inférieur : SFB ou IFB-A

		val_bw = 0.0
		If lSFB OrElse lIFB_A Then

			If lSFB Then
				val_bw = 0.5 * myProfil.Tfi
				ywd_1 = yfi_1
				ywd_3 = yfi_2
			Else
				val_bw = 0.5 * myProfil.Plat_t
				ywd_1 = yw_1
				ywd_3 = yw_2
			End If

			'GiB 18/11/2025 : ajout d'une condition
			If Not (lSFB AndAlso (Not lInter)) Then 'hors SFB de rive
				ywd_1 -= val_bw
				ywd_2 = ywd_1 + val_bw
			End If

			ywd_4 = ywd_3 + val_bw

			If lSFB Then    ' SFB
				zwd_1 = zfi_1
			Else            ' IFB-A
				zwd_1 = zw_1
			End If
			zwd_2 = zwd_1 + val_bw

			zwd_3 = zwd_1
			zwd_4 = zwd_2

		End If

		'**( Intervalle de calcul suivant l'axe fort

		If (lSFB Or lIFB_A) Then 'plat soudé inférieur
			If lInter Then 'poutre intérieure
				y_min = y_0 + bEffG - myProfil.Plat_b
				y_max = y_min + 2 * myProfil.Plat_b
			Else 'poutre de rive
				y_min = y_0 + bEffG - myProfil.Bfs
				y_max = y_min + 2 * myProfil.Bfs
			End If

		Else 'semelle inférieure en talon

			y_min = y_0 + bEffG - myProfil.Bfi
			y_max = y_min + 2 * myProfil.Bfi

		End If


		y_min = Math.Max(y_min, y_0)
		y_max = Math.Min(y_max, y_0 + bEffG + bEffD)

		'GiB 19/11/2025 : abscisses extrémales des parois en acier
		If lSFB OrElse lIFB_A Then  'SFB ou IFB-A : abscisses du plat inferieur
			y_a_min = yp_1
			y_a_max = yp_2
		Else  'IFB-B ou SAB : abscisses du plat inferieur
			y_a_min = yfi_1
			y_a_max = yfi_2
		End If

		'====== FIN TRAITEMENT PROFILE METALLIQUE ======
		'====== TRAITEMENT DALLE =======================

		'**( Barres d'armature

		val_bs_eq = 0.0

		If (myDalle.ArmaSlimFeu.lBarre And (myDalle.ArmaSlimFeu.NbBarres > 0)) Then

			Dim PhiS As Decimal = myDalle.ArmaSlimFeu.Diametre
			Dim UsZ As Decimal = myDalle.ArmaSlimFeu.zPos               ' distance à l'axe (verticale) des barres par rapport à la face inférieure de la dalle
			Dim UwY As Decimal = myDalle.ArmaSlimFeu.xPos               ' distance à l'axe (horizontale) des barres par rapport à l'âme du profilé métallique
			Dim UcY As Decimal = PhiS                                   ' entraxe des barres (horizontal) du même coté de l'âme du profil métlalique si ns = 2

			val_bs_eq = 0.5 * PhiS * Math.Sqrt(Math.Acos(-1.0))

			If (lSFB Or lIFB_A) Then        ' SFB ou IFB-A
				zs_1 = zfi_1
			Else
				zs_1 = zfi_2                ' IFB-B ou SAB
			End If
			zs_1 += UsZ - 0.5 * val_bs_eq
			zs_2 = zs_1 + val_bs_eq

			zs_3 = zs_1
			zs_4 = zs_2

			If (myDalle.ArmaSlimFeu.NbBarres = 2) Then

				If Not ((Not lInter) AndAlso (Not myDalle.lRiveRemplie)) Then

					ys_1 = yw_1 - UwY - UcY - 0.5 * val_bs_eq
					ys_2 = ys_1 + val_bs_eq

					ys_3 = yw_1 - UwY - 0.5 * val_bs_eq
					ys_4 = ys_3 + val_bs_eq

				End If

				ys_5 = yw_2 + UwY - 0.5 * val_bs_eq
				ys_6 = ys_5 + val_bs_eq

				ys_7 = yw_2 + UwY + UcY - 0.5 * val_bs_eq
				ys_8 = ys_7 + val_bs_eq

				zs_5 = zs_1
				zs_6 = zs_2
				zs_7 = zs_1
				zs_8 = zs_2

			Else

				If Not ((Not lInter) AndAlso (Not myDalle.lRiveRemplie)) Then
					ys_1 = yw_1 - UwY - 0.5 * val_bs_eq
					ys_2 = ys_1 + val_bs_eq
				End If

				ys_3 = yw_2 + UwY - 0.5 * val_bs_eq
				ys_4 = ys_3 + val_bs_eq

			End If

		End If

		' Vides 

		yv_1 = y_0
		zv_1 = z_0

		yv_4 = y_0 + bEffG + bEffD
		zv_3 = z_0

		If (lSFB Or lIFB_A) Then        ' SFB, IFB-A ou SAB
			yv_2 = yp_1
			yv_3 = yp_2
			zv_2 = zp_2
		Else                                    ' IFB-B
			yv_2 = yfi_1
			yv_3 = yfi_2
			zv_2 = zfi_2
		End If

		If lPredalle Then               ' prédalle
			zv_2 += +myDalle.preDalle_ep
		ElseIf lDalMixte Then   'dalle mixte
			zv_2 += +myDalle.Bac.Hp
		ElseIf lPlancherPrefa Then   'GiB 18/11/2025 : dalle prefabriquee
			zv_2 += myDalle.Cofradal.dp
		End If

		yv_2 += bAppG
		yv_3 -= bAppD
		zv_4 = zv_2

		'====== FIN TRAITEMENT DALLE ===================

		'--( CALCUL DE LA DENSITE DU MAILLAGE

		val_size = myProfil.Tw

		If IsGreater(myProfil.Tfi, 0) Then
			val_size = Math.Min(val_size, 0.5 * myProfil.Tfi)
		End If

		If IsGreater(myProfil.Tfs, 0) Then
			val_size = Math.Min(val_size, 0.5 * myProfil.Tfs)
		End If

		If IsGreater(myProfil.Plat_t, 0) Then
			val_size = Math.Min(val_size, 0.5 * myProfil.Plat_t)
		End If

		'--( CALCUL DES ABSCISSES DES PLANS DE COUPE PARALELLES A L'AXE FAIBLE

		Tab_dec(0) = y_0
		Tab_dec(1) = yv_1 : Tab_dec(2) = yv_2 : Tab_dec(3) = yv_3 : Tab_dec(4) = yv_4
		Tab_dec(5) = yp_1 : Tab_dec(6) = yp_2
		Tab_dec(7) = yfi_1 : Tab_dec(8) = yfi_2
		Tab_dec(9) = yw_1 : Tab_dec(10) = yw_2
		Tab_dec(11) = yfs_1 : Tab_dec(12) = yfs_2
		Tab_dec(13) = ywd_1 : Tab_dec(14) = ywd_2 : Tab_dec(15) = ywd_3 : Tab_dec(16) = ywd_4
		Tab_dec(17) = ys_1 : Tab_dec(18) = ys_2 : Tab_dec(19) = ys_3 : Tab_dec(20) = ys_4
		Tab_dec(21) = ys_5 : Tab_dec(22) = ys_6 : Tab_dec(23) = ys_7 : Tab_dec(24) = ys_8
		n_dec = 24
		Decoupes_bornes_y(bEffG, bEffD, delta_y, yw_1, yw_2, n_dec, Tab_dec)
		Finalisation_Decoupes(n_dec, n_dec_y, Tab_dec, Tab_y)

		'Tri par ordre croissant
		ReDim Preserve Tab_y(0 To n_dec_y - 1)
		Array.Sort(Tab_y)

		'--(DENSITE DU MAILLAGE SUIVANT L'AXE FORT
		Maillage_progressif_y(n_dec_y, yw_1, yw_2, y_a_min, y_a_max, delta_y, k_dens_y, val_dens_y, val_size, Tab_y)

		'-- Ajustement de la taille des tableaux

		ReDim Preserve Me.Tab_mesh_y(0 To Me.nb_cells_y - 1)

		'-------------------------------------------------------------------------------------------------------------------------------
		' Intervalle de calcul de transfert thermique
		' Le calcul est effectué uniquement sur une portion de -400 mm à 400 mm de l'axe faible du profilé métallique
		' En dehors de cet intervalle, le gradient de température sur l'épaisseur de la dalle peut être considéré comme identique 
		'-------------------------------------------------------------------------------------------------------------------------------

		Borne_inferieure_calcul(y_min, yw_1, yw_2, delta_y, y_0)
		Borne_inferieure_modifiee_calcul(yw_1, yw_2, delta_y, y_0, lInter, bEffG, bEffD)
		Borne_superieure_calcul(y_max, yw_1, yw_2, delta_y, y_0, bEffG, bEffD)

		'--( CALCUL DES ORDONNEES DES PLANS DE COUPE PARALLELES A L'AXE FORT

		ReDim Tab_dec(0 To 100)
		Tab_dec(0) = z_0
		Tab_dec(1) = zv_1 : Tab_dec(2) = zv_2 : Tab_dec(3) = zv_3 : Tab_dec(4) = zv_4
		Tab_dec(5) = zp_1 : Tab_dec(6) = zp_2
		Tab_dec(7) = zfi_1 : Tab_dec(8) = zfi_2
		Tab_dec(9) = zw_1 : Tab_dec(10) = zw_2
		Tab_dec(11) = zfs_1 : Tab_dec(12) = zfs_2
		Tab_dec(13) = zwd_1 : Tab_dec(14) = zwd_2 : Tab_dec(15) = zwd_3 : Tab_dec(16) = zwd_4
		Tab_dec(17) = zs_1 : Tab_dec(18) = zs_2 : Tab_dec(19) = zs_3 : Tab_dec(20) = zs_4
		Tab_dec(21) = zs_6 : Tab_dec(22) = zs_6 : Tab_dec(23) = zs_7 : Tab_dec(24) = zs_8
		Tab_dec(25) = Tab_dec(0) + myDalle.Ep_td

		If lSFB Or lIFB_A Then
			Tab_dec(25) += myProfil.Plat_t
		Else
			Tab_dec(25) += myProfil.Tfi
		End If

		n_dec = 25
		Finalisation_Decoupes(n_dec, n_dec_z, Tab_dec, Tab_z)

		'--( Tri par ordre croissant
		ReDim Preserve Tab_z(0 To n_dec_z - 1)
		Array.Sort(Tab_z)

		'--( DENSITE DU MAILLAGE SUIVANT L'AXE FAIBLE

		k_dens_z = 1.1
		Maillage_progressif_z(n_dec_z, val_size, k_dens_z, val_dens_z, Tab_z)
		ReDim Preserve Me.Tab_mesh_z(0 To Me.nb_cells_z - 1)

		'--( CALCUL DES COORDONNEES DU CENTRE DE CHAQUE MAILLE

		Coordonnees_centre_maille(Tab_y, Tab_z)

		'--( ATTRIBUTION DES MATERIAUX

		'Const MATVIDEFERME As Integer = -2          ' vide d'une cavite fermee (espace entre le mur et le profile metallique d'une poutre de rive
		'Const MATVIDEOUVERT As Integer = -1         ' vide d'une cavite ouverte
		'Const MATACIERPLAT As Integer = 0           ' acier de construction du plat soudé
		'Const MATACIERSEMI As Integer = 1           ' acier de construction de la semelle inférieure
		'Const MATACIERAME As Integer = 2            ' acier de construction de l'ame
		'Const MATACIERSEMS As Integer = 3           ' acier de construction de la semelle supérieure
		'Const MATACIERSOUD As Integer = 4           ' acier de soudure du plat
		'Const MATARMA As Integer = 5                ' acier d'armature
		'Const MATBETON As Integer = 6               ' béton de la dalle

		Initialisation_materiaux(MATBETON)

		'Vides de part et d'autre du plat inférieur d'une SFB ou d'une IFB-A ou de la semelle inférieure d'une IFB-B ou d'une SAB
		i_mat = MATVIDEOUVERT

		If lSFB Or lIFB_A Then                          ' SFB ou IFB-A

			If lSFB AndAlso (Not lInter) Then      ' SFB de rive
				Call Affectation_materiau_maillage(i_mat, y_0, yv_1, ywd_1, z_0, zp_1, zp_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			Else
				Call Affectation_materiau_maillage(i_mat, y_0, yv_1, yp_2, z_0, zp_1, zp_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			End If
			Call Affectation_materiau_maillage(i_mat, y_0, yp_2, yv_4, z_0, zp_1, zp_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

		Else

			Call Affectation_materiau_maillage(i_mat, y_0, yv_1, yv_2, z_0, zfi_1, zfi_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			Call Affectation_materiau_maillage(i_mat, y_0, yv_3, yv_4, z_0, zfi_1, zfi_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

		End If

		'--( Vide d'une poutre de rive entre le mur et le profile metallique

		If (Not lInter) AndAlso (Not myDalle.lRiveRemplie) Then

			prop_encl_open = (lSFB AndAlso IsSmaller(y_0, yp_1 - val_bw)) _
				  OrElse (lIFB_A AndAlso IsSmaller(y_0, yp_1)) _
				  OrElse ((lIFB_B Or lSAB) AndAlso IsSmaller(y_0, yfi_1))
			If prop_encl_open Then
				i_mat = MATVIDEOUVERT   ' cavite ouverte
			Else
				i_mat = MATVIDEFERME    ' cavite fermee
			End If

			'%% Entre la semelle inferieure d'un SFB, d'un IFB-B ou d'un SAB et le mur
			If (Not lIFB_A) AndAlso IsSmaller(y_0, yfi_1) Then
				Call Affectation_materiau_maillage(i_mat, y_0, y_0, yfi_1, z_0, zfi_1, zfi_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			End If

			'%% Entre l'âme et le mur
			Call Affectation_materiau_maillage(i_mat, y_0, y_0, yw_1, z_0, zw_1, zw_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

			'%% Entre la semelle superieure d'un SFB, d'un IFB-A ou d'un SAB ou le plat soude d'un IFB-B et le mur
			If (Not lIFB_B) AndAlso IsSmaller(y_0, yfs_1) Then
				Call Affectation_materiau_maillage(i_mat, y_0, y_0, yfs_1, z_0, zfs_1, zfs_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			ElseIf lIFB_B AndAlso IsSmaller(y_0, yp_1) Then
				Call Affectation_materiau_maillage(i_mat, y_0, y_0, yp_1, z_0, zp_1, zp_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			End If

		End If

		'%% Creux d'onde du bac acier d'une dalle mixte

		If lDalMixte And IsGreater(myDalle.Bac.Hp, 0) Then

			i_mat = MATVIDEOUVERT
			If lSFB Or lIFB_A Then 'SFB ou IFB-A

				If lInter OrElse (myDalle.lRiveRemplie AndAlso IsGreater(bAppG, 0)) Then              ' poutre interieure ou poutre de rive avec dalle en rive
					Call Affectation_materiau_maillage(i_mat, y_0, yv_1, yv_2, z_0, zp_2, zv_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
				End If

				Call Affectation_materiau_maillage(i_mat, y_0, yv_3, yv_4, z_0, zp_2, zv_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

			Else 'IFB-B ou SAB

				If lInter OrElse (myDalle.lRiveRemplie AndAlso IsGreater(bAppG, 0)) Then   'poutre interieure ou poutre de rive avec dalle en rive
					Call Affectation_materiau_maillage(i_mat, y_0, yv_1, yv_2, z_0, zfi_2, zv_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
				End If

				Call Affectation_materiau_maillage(i_mat, y_0, yv_3, yv_4, z_0, zfi_2, zv_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

			End If

		End If

		'%%Plat inférieur d'une SFB ou d'une IFB-A ou plat supérieur d'une IFB-B

		If Not lSAB Then

			i_mat = MATACIERPLAT
			If lSFB AndAlso (Not lInter) Then 'SFB de rive
				Call Affectation_materiau_maillage(i_mat, y_0, ywd_1, yp_2, z_0, zp_1, zp_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			Else    'SFB intérieure, IFB-A ou IFB-B
				Call Affectation_materiau_maillage(i_mat, y_0, yp_1, yp_2, z_0, zp_1, zp_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			End If

		End If

		'%% Semelle inférieure d'une SFB, IFA-B ou SAB
		If Not lIFB_A Then
			i_mat = MATACIERSEMI
			Call Affectation_materiau_maillage(i_mat, y_0, yfi_1, yfi_2, z_0, zfi_1, zfi_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
		End If

		'%% Âme
		i_mat = MATACIERAME
		Call Affectation_materiau_maillage(i_mat, y_0, yw_1, yw_2, z_0, zw_1, zw_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

		'%% Semelle supérieure d'une SFB, IFB-A ou SAB
		If Not lIFB_B Then
			i_mat = MATACIERSEMS
			Call Affectation_materiau_maillage(i_mat, y_0, yfs_1, yfs_2, z_0, zfs_1, zfs_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
		End If

		'%% Cordons de soudure d'une SFB ou d'une IFB-A
		If (lSFB Or lIFB_A) Then
			i_mat = MATACIERSOUD
			Call Affectation_materiau_maillage(i_mat, y_0, ywd_1, ywd_2, z_0, zwd_1, zwd_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			Call Affectation_materiau_maillage(i_mat, y_0, ywd_3, ywd_4, z_0, zwd_3, zwd_4, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
		End If

		'%% Barres d'armature

		If (myDalle.ArmaSlimFeu.lBarre And (myDalle.ArmaSlimFeu.NbBarres > 0)) Then

			'GiB 19/11/2025 
			i_mat = MATARMA
			If IsGreater(Math.Abs(ys_1 - ys_2), 0) Then
				Call Affectation_materiau_maillage(i_mat, y_0, ys_1, ys_2, z_0, zs_1, zs_2, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			End If
			Call Affectation_materiau_maillage(i_mat, y_0, ys_3, ys_4, z_0, zs_3, zs_4, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)

			If myDalle.ArmaSlimFeu.NbBarres = 2 Then
				Call Affectation_materiau_maillage(i_mat, y_0, ys_5, ys_6, z_0, zs_5, zs_6, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
				Call Affectation_materiau_maillage(i_mat, y_0, ys_7, ys_8, z_0, zs_7, zs_8, Me.Tab_mesh_y, Me.Tab_mesh_z, Me.Tab_mesh_mat)
			End If

		End If

	End Sub

	Public Sub Affectation_materiau_maillage(val_mat As Integer, val_y0 As Double, val_y1 As Double, val_y2 As Double,
											  val_z0 As Double, val_z1 As Double, val_z2 As Double,
											  Tab_1() As Double, Tab_2() As Double, ByRef Tab_3(,) As Integer)
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   13/11/25 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Affectation d'un numéro de matériau à une maille (i,j) en fonction des coordonnées de cette maille
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   val_mat         [E] :   numéro de matériau à affecter (-2 à 6)
		'   val_y0          [E] :   abscisse de l'origine du répère (i,j)
		'   val_y1, val_y2  [E] :   abscisses minimale et maximale de la maille (i,j)
		'   val_z0          [E] :   ordonnée de l'origine du répère (i,j)
		'   val_z1, val_z2  [E] :   ordonnées minimale et maximale de la maille (i,j)
		'   Tab_1, Tab_2    [E] :   tableaux de densité du maillage suivant les abscisses et les ordonnées
		'   Tab_3           [E/S] : tableau de matériau du maillage suivant les abscisses et les ordonnées
		'---------------------------------------------------------------------------------------------------------------------------------------

		'--( Déclaration
		Dim i_ As Integer, j_ As Integer
		Dim val_y As Double, val_z As Double

		'--( Initialisation
		val_y = val_y0

		'--( Traitement
		For i_ = Tab_1.GetLowerBound(0) To Tab_1.GetUpperBound(0)

			val_y += Tab_1(i_)

			If IsGreater(val_y, val_y1) AndAlso IsSmallerOrEqual(val_y, val_y2) Then
				val_z = val_z0

				For j_ = Tab_2.GetLowerBound(0) To Tab_2.GetUpperBound(0)
					val_z += Tab_2(j_)
					If IsGreater(val_z, val_z1, 0.0001) AndAlso IsSmallerOrEqual(val_z, val_z2, 0.0001) Then
						Tab_3(i_, j_) = val_mat
					ElseIf IsGreaterOrEqual(val_z, val_z2) Then
						Exit For
					End If
				Next

			ElseIf IsGreaterOrEqual(val_y, val_y2) Then

				Exit For

			End If

		Next

	End Sub

	Public Function Nombre_mailles_maillage_progressif(val_dist As Double, val_size_0 As Double, val_fact As Double) As Integer

		'--------------------------------------------------------------------------------------------------------------------------------
		'   19/11/2025 :  Création - GiB
		'--------------------------------------------------------------------------------------------------------------------------------
		'   Calcul du nombre de mailles sur une distance en considérant un maillage 1D progressif
		'--------------------------------------------------------------------------------------------------------------------------------
		'   val_dist       [E] :   distance à mailler
		'   val_size       [E] :   densité constante par défaut
		'   val_fact       [E] :   coefficient d'amplification de la densité du maillage
		'   val_nb         [S] :   nombre de mailles à taille progressive
		'--------------------------------------------------------------------------------------------------------------------------------

		'--( Déclarations
		Dim val_t As Double
		Dim val_nb As Integer

		'--( Initialisation
		val_t = 0.0
		val_nb = 0

		'--( Boucle
		Do While val_t < val_dist
			val_t += val_size_0 * (val_fact ^ val_nb)
			val_nb += 1
		Loop

		Return val_nb

	End Function

	Public Function Densite_maillage_progressif(val_dist As Double, val_size_0 As Double, val_fact As Double, val_nb As Integer) As Double

		'--------------------------------------------------------------------------------------------------------------------------------
		'   19/11/2025 :  Création - GiB
		'--------------------------------------------------------------------------------------------------------------------------------
		'   Calcul de la taille initiale des mailles sur une distance en considérant un maillage 1D progressif
		'--------------------------------------------------------------------------------------------------------------------------------
		'   val_dist		[E] :   distance à mailler
		'   val_size_0		[E] :   densité constante par défaut
		'   val_fact		[E] :   coefficient d'amplification de la densité du maillage
		'   val_nb			[E] :   nombre de mailles à taille progressive
		'   val_dens		[S] :   densité initiale
		'--------------------------------------------------------------------------------------------------------------------------------

		'--( Déclarations
		Dim i_ As Integer
		Dim val_t As Double
		Dim val_dens As Double

		'--( Initialisation
		val_dens = val_size_0

		'--( Boucle
		Do
			val_t = 0.0
			For i_ = 1 To val_nb
				val_t += val_dens * (val_fact ^ (i_ - 1))
			Next

			If IsGreater(val_t, val_dist) Then
				val_dens -= 0.0001
			Else
				Exit Do
			End If
		Loop

		Return val_dens

	End Function

	Public Function Face_exposee(val_i As Integer, val_j As Integer, val_ind As Integer, mat_0 As Integer, My_temp(,) As Double, My_mat(,) As Integer) As Boolean

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   20/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Vérification du caractère exposé d'au moins une face d'une maille
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   val_i       [E] :   1er indice de la maille 
		'   val_j       [E] :   2e indice de la maille
		'   val_ind     [E] :   indice de la maille finale de la matrice du maillge complet
		'   mat_0       [E] :   numéro du matériau représentant les gaz chauds
		'   My_temp     [E] :   matrice de température des mailles
		'   My_mat      [E] :   marice des numéros de matériau des mailles
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Paramètre en sortie:
		'   l_Expo       face exposée (True) ou à l'intérieur du maillage (False)
		'---------------------------------------------------------------------------------------------------------------------------------------

		Dim l_Expo As Boolean = False
		If IsSmaller(val_j, My_temp.GetUpperBound(1)) AndAlso IsEqual(My_mat(val_i, val_j + 1), mat_0) Then
			l_Expo = True
		End If

		If (Not l_Expo) AndAlso (IsEqual(val_j, My_temp.GetLowerBound(1)) OrElse IsEqual(My_mat(val_i, val_j - 1), mat_0)) Then
			l_Expo = True
		End If

		If (Not l_Expo) AndAlso (IsGreater(val_i, My_temp.GetLowerBound(0) + val_ind) AndAlso IsEqual(My_mat(val_i - 1, val_j), mat_0)) Then
			l_Expo = True
		End If

		If (Not l_Expo) AndAlso (IsSmaller(val_i, My_temp.GetUpperBound(0) + val_ind) AndAlso IsEqual(My_mat(val_i + 1, val_j), mat_0)) Then
			l_Expo = True
		End If

		Return l_Expo

	End Function

	Public Function Face_non_exposee(val_j As Integer, My_temp(,) As Double) As Boolean

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   20/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Vérification du caractère non exposé d'une face d'une maille
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   val_j       [E] :   2e indice de la maille
		'   My_temp     [E] :   matrice de température des mailles
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Paramètre en sortie:
		'   l_NoEx       face non exposée (True) ou à l'intérieur du maillage (False)
		'---------------------------------------------------------------------------------------------------------------------------------------

		Dim l_NoEx As Boolean = False
		If IsEqual(val_j, My_temp.GetUpperBound(1)) Then
			l_NoEx = True
		End If

		Return l_NoEx

	End Function

	Public Sub Faces_exterieures(ByRef My_expo(,) As Boolean, ByRef My_noexpo(,) As Boolean, ByVal My_temp(,) As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   20/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Remplissage de la matrice de face extérieure de maille
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   My_expo		[E] :   matrice de faces exposées
		'   My_noexpo	[E] :   matrice de faces non exposées
		'   My_temp		[E] :   matrice de température des mailles
		'------------------------------------------------------------------------------------------

		Dim i_ As Integer, i_y As Integer, j_ As Integer, j_ind As Integer

		For i_ = Me.ind_2 To Me.ind_1
			i_y = i_ - Me.ind_2
			For j_ind = 0 To Me.nb_cells_z - 1
				My_expo(i_y, j_ind) = Me.Face_exposee(i_, j_, Me.ind_2, Me.MATVIDEOUVERT, My_temp, Me.Tab_mesh_mat)
				My_noexpo(i_y, j_ind) = Me.Face_non_exposee(j_, My_temp)
			Next
		Next
	End Sub

	Public Sub Decoupes_bornes_y(bEffG As Double, bEffD As Double, delta_y As Double, yw_1 As Double, yw_2 As Double, ByRef n_dec As Integer, ByRef Tab_dec() As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Découpes aux bornes de l'intervalle de calcul thermique
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   beffG		[E] :   largeur de la dalle à gauche de l'axe faible (m)
		'   My_noexpo	[E] :   largeur de la dalle à droite de l'axe faible (m)
		'   delta_y		[E] :   largeur de la zone calculée dans l'analyse de transfert thermique à droite de l'axe faible (m)
		'   yw_1		[E] :   abscisse initiale de la première soudure (m)
		'   yw_2		[E] :   abscisse finale de la première soudure (m)
		'   n_dec		[S] :   nombre de découpes
		'   Tab_dec		[S] :   abscisse des découpes (m)
		'   
		'------------------------------------------------------------------------------------------

		If IsGreater(bEffG, delta_y) Then
			n_dec += 1
			Tab_dec(n_dec) = 0.5 * (yw_1 + yw_2) - delta_y
		End If
		If IsGreater(bEffD, delta_y) Then
			n_dec += 1
			Tab_dec(n_dec) = 0.5 * (yw_1 + yw_2) + delta_y
		End If

	End Sub

	Public Sub Finalisation_Decoupes(ByVal n_dec As Integer, ByRef n_dec_yz As Integer, ByVal Tab_dec() As Double, ByRef Tab_yz() As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Finalisation des découpes de la section transversale suivant l'un ou l'autre des axes
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   n_dec		[E] :   nombre de découpes initiales
		'   n_dec_yz	[E] :   nombre de découpes finales
		'   Tab_dec		[E] :   abscisse ou ordonnée des découpes initiales (m)
		'   Tab_yz		[E] :   abscisse ou ordonnée  des découpes finales (m)
		'------------------------------------------------------------------------------------------

		'--( Declaration
		Dim i_ As Integer, j_ As Integer, k_ As Integer

		'--( Initialisation
		n_dec_yz = 0

		'--( Traitement
		For i_ = 0 To n_dec

			If i_ = 0 Then
				Tab_yz(i_) = Tab_dec(i_)
				n_dec_yz += 1
			Else
				k_ = 0
				For j_ = 0 To n_dec_yz - 1
					'GiB 21/04/2025 : correction pour éviter des doublons de coordonnées
					'If Tab_z(j_) = Tab_dec(i_) Then
					If IsEqual(Tab_yz(j_), Tab_dec(i_), 0.0001) Then
						k_ += 1
						Exit For
					End If
				Next
				If k_ = 0 Then
					Tab_yz(n_dec_yz) = Tab_dec(i_)
					n_dec_yz += 1
				End If
			End If

		Next

	End Sub

	Public Sub Maillage_progressif_y(n_dec_y As Integer, yw_1 As Double, yw_2 As Double, y_a_min As Double, y_a_max As Double,
									delta_y As Double, k_dens_y As Double, val_dens_y As Double, val_size As Double, Tab_y() As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Maillage progressif de la section transversale suivant l'axe fort
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   n_dec_y			[E] :   nombre de découpes
		'   yw_1			[E] :   abscisse initiale de la première soudure (m)
		'   yw_2			[E] :   abscisse finale de la première soudure (m)
		'   y_a_min			[E] :   abscisse minimale des parois en acier (m)
		'   y_a_max			[E] :   abscisse maximale des parois en acier (m)
		'   delta_y			[E] :   largeur de la zone calculée dans l'analyse de transfert thermique à droite de l'axe faible (m)
		'   k_dens_y		[E] :   coefficient d'amplification de la densité du maillage
		'   val_dens_y		[E] :   densité initiale (m)
		'   val_size		[E] :   densité du maillage (m)
		'   Tab_y			[E] :   abscisses des découpes (m)
		'   Tab_mesh_y		[S] :   matrice des abscisses des bords de maille (m)
		'   nb_cells_y		[S] :   nombre de mailles
		'------------------------------------------------------------------------------------------

		'--( Declaration
		Dim i_ As Integer, j_ As Integer, k_ As Integer, l_ As Integer

		'--( Initialisation
		l_ = -1
		Me.nb_cells_y = 0

		'--( Traitement
		For i_ = 1 To n_dec_y - 1

			If IsSmallerOrEqual(Tab_y(i_), 0.5 * (yw_1 + yw_2) - delta_y) Then  'une seule maille à gauche de la borne inférieure de l'intervalle de calcul thermique

				l_ += 1
				j_ = 1
				Me.Tab_mesh_y(l_) = Tab_y(i_) - Tab_y(i_ - 1)

			ElseIf IsSmallerOrEqual(Tab_y(i_), y_a_min) Then    'maillage progressif dans l'intervalle de calcul thermique, à gauche de la zone englobant la partie en acier

				j_ = Math.Max(1, Nombre_mailles_maillage_progressif(Tab_y(i_) - Tab_y(i_ - 1), val_size, k_dens_y))
				val_dens_y = Densite_maillage_progressif(Tab_y(i_) - Tab_y(i_ - 1), val_size, k_dens_y, j_)
				If j_ = 1 Then
					l_ += 1
					Me.Tab_mesh_y(l_) = Tab_y(i_) - Tab_y(i_ - 1)
				Else
					For k_ = 1 To j_ - 1
						l_ += 1
						Me.Tab_mesh_y(l_ + j_ - 2 * k_ + 1) = val_dens_y * (k_dens_y ^ (k_ - 1))
					Next

					l_ += 1
					Me.Tab_mesh_y(l_ - j_ + 1) = Tab_y(i_) - Tab_y(i_ - 1)
					For k_ = 1 To j_ - 1
						Me.Tab_mesh_y(l_ - j_ + 1) -= Me.Tab_mesh_y(l_ - j_ + 1 + k_)
					Next

				End If

			ElseIf IsSmallerOrEqual(Tab_y(i_), y_a_max) Then    'maillage constant dans la zone englobant la partie en acier

				j_ = Math.Max(1, CInt((Tab_y(i_) - Tab_y(i_ - 1)) / val_size))

				If IsSmallerOrEqual(Math.Abs(Tab_y(i_ - 1) - yw_1), 0) AndAlso IsSmallerOrEqual(Math.Abs(Tab_y(i_) - yw_2), 0) AndAlso (j_ = 1) Then    '2 mailles sur l'épaisseur de l'âme
					j_ = 2
				End If

				For k_ = 1 To j_
					l_ += 1
					Me.Tab_mesh_y(l_) = (Tab_y(i_) - Tab_y(i_ - 1)) / j_
				Next

			ElseIf IsSmallerOrEqual(Tab_y(i_), 0.5 * (yw_1 + yw_2) + delta_y) Then         'maillage progressif dans l'intervalle de calcul thermique, à droite de la zone englobant la partie en acier

				j_ = Math.Max(1, Nombre_mailles_maillage_progressif(Tab_y(i_) - Tab_y(i_ - 1), val_size, k_dens_y))
				val_dens_y = Densite_maillage_progressif(Tab_y(i_) - Tab_y(i_ - 1), val_size, k_dens_y, j_)
				If j_ = 1 Then
					l_ += 1
					Me.Tab_mesh_y(l_) = Tab_y(i_) - Tab_y(i_ - 1)
				Else
					For k_ = 1 To j_ - 1
						l_ += 1
						Me.Tab_mesh_y(l_) = val_dens_y * (k_dens_y ^ (k_ - 1))
					Next
					l_ += 1
					Me.Tab_mesh_y(l_) = Tab_y(i_) - Tab_y(i_ - 1)
					For k_ = 1 To j_ - 1
						Me.Tab_mesh_y(l_) -= Me.Tab_mesh_y(l_ - k_)
					Next

				End If

			Else  'une seule maille à gauche de la borne supérieure de l'intervalle de calcul thermique

				l_ += 1
				j_ = 1
				Me.Tab_mesh_y(l_) = Tab_y(i_) - Tab_y(i_ - 1)

			End If

			Me.nb_cells_y += j_

		Next

	End Sub

	Public Sub Maillage_progressif_z(n_dec_z As Integer, val_size As Double, k_dens_z As Double, val_dens_z As Double, Tab_z() As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Maillage progressif de la section transversale suivant l'axe faible
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   n_dec_z			[E] :   nombre de découpes
		'   val_size		[E] :   densité du maillage (m)
		'   k_dens_z		[E] :   coefficient d'amplification de la densité du maillage
		'   val_dens_z		[E]	:	 densité initiale
		'   Tab_z			[E] :   ordonnées des découpes (m)
		'   Tab_mesh_z		[S] :   matrice des ordonnées des bords de maille (m)
		'   nb_cells_z		[S] :   nombre de mailles
		'------------------------------------------------------------------------------------------

		'--( Declaration
		Dim i_ As Integer, j_ As Integer, k_ As Integer, l_ As Integer

		'--( Initialisation
		l_ = -1
		Me.nb_cells_z = 0

		'--( Traitement
		For i_ = 1 To n_dec_z - 1

			If IsSmaller(i_, n_dec_z - 1) Then
				j_ = Math.Max(1, CInt((Tab_z(i_) - Tab_z(i_ - 1)) / val_size))
				For k_ = 1 To j_
					l_ += 1
					Me.Tab_mesh_z(l_) = (Tab_z(i_) - Tab_z(i_ - 1)) / j_
				Next
			Else

				j_ = Math.Max(1, Nombre_mailles_maillage_progressif(Tab_z(i_) - Tab_z(i_ - 1), val_size, k_dens_z))
				val_dens_z = Densite_maillage_progressif(Tab_z(i_) - Tab_z(i_ - 1), val_size, k_dens_z, j_)

				If j_ = 1 Then
					l_ += 1
					Me.Tab_mesh_z(l_) = Tab_z(i_) - Tab_z(i_ - 1)
				Else
					For k_ = 1 To j_ - 1
						l_ += 1
						Me.Tab_mesh_z(l_) = val_dens_z * (k_dens_z ^ (k_ - 1))
					Next
					l_ += 1
					Me.Tab_mesh_z(l_) = Tab_z(i_) - Tab_z(i_ - 1)
					For k_ = 1 To j_ - 1
						Me.Tab_mesh_z(l_) -= Me.Tab_mesh_z(l_ - k_)
					Next

				End If

			End If
			Me.nb_cells_z += j_

		Next

	End Sub

	Public Sub Coordonnees_centre_maille(Tab_y() As Double, Tab_z() As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Calcul des coordonnées du centre de chaque maille
		'---------------------------------------------------------------------------------------------------------------------------------------

		'   Tab_y			[E] :   ordonnées des découpes (m)
		'   Tab_z			[E] :   ordonnées des découpes (m)
		'------------------------------------------------------------------------------------------

		'--( Declaration
		Dim i_ As Integer, j_ As Integer

		'--( Initialisation
		ReDim Me.Tab_mesh_cent_y(0 To Me.nb_cells_y - 1, 0 To Me.nb_cells_z - 1)
		ReDim Me.Tab_mesh_cent_z(0 To Me.nb_cells_y - 1, 0 To Me.nb_cells_z - 1)

		'--( Traitement
		For i_ = 0 To Me.nb_cells_y - 1

			For j_ = 0 To Me.nb_cells_z - 1

				If i_ = 0 Then
					Me.Tab_mesh_cent_y(i_, j_) = Tab_y(0) + 0.5 * Me.Tab_mesh_y(i_)
				Else
					Me.Tab_mesh_cent_y(i_, j_) = Me.Tab_mesh_cent_y(i_ - 1, j_) + 0.5 * (Me.Tab_mesh_y(i_ - 1) + Me.Tab_mesh_y(i_))
				End If

				If j_ = 0 Then
					Me.Tab_mesh_cent_z(i_, j_) = Tab_z(0) + 0.5 * Me.Tab_mesh_z(j_)
				Else
					Me.Tab_mesh_cent_z(i_, j_) = Me.Tab_mesh_cent_z(i_, j_ - 1) + 0.5 * (Me.Tab_mesh_z(j_ - 1) + Me.Tab_mesh_z(j_))
				End If

			Next

		Next

	End Sub

	Public Sub Borne_inferieure_calcul(ByRef y_min As Double, yw_1 As Double, yw_2 As Double, delta_y As Double, y_0 As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Indice de la maille de la borne inférieure de l'intervalle de calcul
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   y_min		[E] :   abscisse minimale du maillage (m)
		'   yw_1		[E] :   abscisse initiale de la première soudure (m)
		'   yw_2		[E] :   abscisse finale de la première soudure (m)
		'   delta_y		[E] :   largeur de la zone calculée dans l'analyse de transfert thermique à droite de l'axe faible (m)
		'   y_0			[S] :   abscisse minimale du maillage (m)
		'   ind_0		[S] :   indice de la borne inférieure
		'   
		'------------------------------------------------------------------------------------------

		'--( Declaration
		Dim i_ As Integer

		'--( Initialisation
		Me.ind_0 = Me.Tab_mesh_y.GetLowerBound(0)

		'--( Traitement
		y_min = Math.Min(y_min, 0.5 * (yw_1 + yw_2) - delta_y)
		If y_min > y_0 Then

			Dim y_ As Single : y_ = y_0

			For i_ = 0 To Me.nb_cells_y - 1
				y_ += Me.Tab_mesh_y(i_)
				If IsGreaterOrEqual(y_, y_min) AndAlso IsGreater(i_, 0) Then
					Me.ind_0 = i_
					Exit For
				End If
			Next

		End If

	End Sub

	Public Sub Borne_inferieure_modifiee_calcul(yw_1 As Double, yw_2 As Double, delta_y As Double, y_0 As Double,
											  lInter As Boolean, beffG As Double, beffD As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Indice de la maille de la borne inférieure à modifier pour une poutre intérieure dont la largeur de la dalle est supérieure à delta_y de chaque côté de l'âme
		'---------------------------------------------------------------------------------------------------------------------------------------

		'   yw_1		[E] :   abscisse initiale de la première soudure (m)
		'   yw_2		[E] :   abscisse finale de la première soudure (m)
		'   delta_y		[E] :   largeur de la zone calculée dans l'analyse de transfert thermique à droite de l'axe faible (m)
		'   y_0			[S] :   abscisse minimale du maillage (m)
		'   lInter		[E] :   poutre intérieure si True
		'   beffG		[E] :   largeur de dalle à gauche de l'axe faible (m)
		'   beffD		[E] :   largeur de dalle à droite de l'axe faible (m)
		'   ind_2		[S] :   indice de la borne inférieure
		'   
		'------------------------------------------------------------------------------------------

		'--( Initialisation
		Me.ind_2 = Me.ind_0

		'--( Traitement
		If lInter AndAlso IsGreater(beffG, delta_y) AndAlso IsGreater(beffD, delta_y) Then
			Dim y_ As Single : y_ = y_0

			For i_ As Integer = 0 To Me.nb_cells_y - 1
				y_ += Me.Tab_mesh_y(i_)
				If IsGreaterOrEqual(y_, 0.5 * (yw_1 + yw_2)) AndAlso IsGreater(i_, 0) Then
					Me.ind_2 = i_ + 1
					Exit For
				End If
			Next
		End If

	End Sub

	Public Sub Borne_superieure_calcul(ByRef y_max As Double, yw_1 As Double, yw_2 As Double, delta_y As Double, y_0 As Double, beffG As Double, beffD As Double)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Indice de la maille de la borne inférieure de l'intervalle de calcul
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   y_max		[E] :   abscisse maximale du maillage (m)
		'   yw_1		[E] :   abscisse initiale de la première soudure (m)
		'   yw_2		[E] :   abscisse finale de la première soudure (m)
		'   delta_y		[E] :   largeur de la zone calculée dans l'analyse de transfert thermique à droite de l'axe faible (m)
		'   y_0			[S] :   abscisse minimale du maillage (m)
		'   beffG		[E] :   largeur de dalle à gauche de l'axe faible (m)
		'   beffD		[E] :   largeur de dalle à droite de l'axe faible (m)
		'   ind_1		[S] :   indice de la borne inférieure
		'   
		'------------------------------------------------------------------------------------------

		'--( Initialisation
		Me.ind_1 = Me.Tab_mesh_y.GetUpperBound(0)

		'--( Traitement
		y_max = Math.Max(y_max, 0.5 * (yw_1 + yw_2) + delta_y)

		If y_max < y_0 + beffG + beffD Then

			Dim y_ As Single = y_0

			For i_ As Integer = 0 To Me.nb_cells_y - 1
				y_ += Me.Tab_mesh_y(i_)
				If IsGreaterOrEqual(y_, y_max) Then
					Me.ind_1 = i_
					If IsGreater(y_, y_max, 0.0001) AndAlso IsSmaller(y_ - Me.Tab_mesh_y(i_), y_max, 0.0001) Then
						Me.ind_1 -= 1
					End If
					Exit For
				End If
			Next

		End If

	End Sub

	Public Sub Initialisation_materiaux(MATBETON As Integer)

		'---------------------------------------------------------------------------------------------------------------------------------------
		'   21/04/2026 :  Création - GiB
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   Initialisation de la matrice matériaux
		'---------------------------------------------------------------------------------------------------------------------------------------
		'   MATBETON	[E] :   numéro du matériau "béton"
		'   
		'------------------------------------------------------------------------------------------

		'--( Initialisation
		ReDim Me.Tab_mesh_mat(0 To Me.nb_cells_y - 1, 0 To Me.nb_cells_z - 1)

		'--( Traitement
		For i_ As Integer = 0 To Me.nb_cells_y - 1
			For j_ As Integer = 0 To Me.nb_cells_z - 1
				Me.Tab_mesh_mat(i_, j_) = MATBETON
			Next
		Next

	End Sub

#End Region

#Region " Initialisation du calcul Feu "

	Public Sub InitialiseTemp(Temp0 As Double)
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   14/11/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Initialisation de la table des températures du maillage
        '---------------------------------------------------------------------------------------------------------------------------------------
        '   Temp0      [E] :   Température initiale (°C)
        '---------------------------------------------------------------------------------------------------------------------------------------

        ReDim Me.Tab_mesh_temp(0 To nb_cells_y - 1, 0 To nb_cells_z - 1)

        For i As Integer = 0 To nb_cells_y - 1
            For j As Integer = 0 To nb_cells_z - 1
                Me.Tab_mesh_temp(i, j) = Temp0
            Next
        Next

    End Sub

#End Region

#Region "Incrément de temps de calcul"

	Public Function IncrementTemps(TimeT As Decimal, lInter As Boolean) As Decimal
		'---------------------------------------------------------------------------------------------------------
		'   10/04/26 :  Création
		'---------------------------------------------------------------------------------------------------------
		'   Incrément de temps variable
		'---------------------------------------------------------------------------------------------------------
		'   TimeT       [E] :   Temps actuel
		'   lInter      [E] :   Poutre intérmédiaire ou pas
		'---------------------------------------------------------------------------------------------------------

		'--( Declaration et initialisation
		Dim DeltaT As Decimal = 0.2

		If IsGreaterOrEqual(TimeT, 600.0) AndAlso IsSmaller(TimeT, 900.0) Then
			DeltaT = 0.25
		ElseIf IsGreaterOrEqual(TimeT, 900.0) Then
			If lInter Then   'poutre intérieure
				If IsSmaller(TimeT, 1200.0) Then
					DeltaT = 0.3
				ElseIf IsSmaller(TimeT, 1800.0) Then
					DeltaT = 0.4
				ElseIf IsSmaller(TimeT, 3600.0) Then
					DeltaT = 0.5
				ElseIf IsSmaller(TimeT, 5400.0) Then
					DeltaT = 0.6
				ElseIf IsSmaller(TimeT, 7200.0) Then
					DeltaT = 0.75
				Else
					DeltaT = 1.0
				End If
			ElseIf IsSmaller(TimeT, 2700.0) Then
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

#End Region

#Region " Outils "

	'Public ReadOnly Property indMATVIDEOUVERT As Integer
	'       Get
	'           Return MATVIDEOUVERT
	'       End Get
	'   End Property


#End Region


End Class
