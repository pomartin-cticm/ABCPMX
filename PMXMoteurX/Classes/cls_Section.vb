Imports System.Collections.Specialized.BitVector32
Imports System.Runtime.CompilerServices

Public Class cls_Section

#Region " Enumérations et structures "

    Structure strucAcierLocal
        Dim Nuance As String
        Dim Qualite As String
        Dim Reduc As String
        Dim lAvailable As Boolean
    End Structure

    Public Enum Enum_TypeSection
        Acier           ' Section acier
        AcierEnrobage   ' Section acier avec enrobage partiel
        Mixte           ' Section mixte acier-béton
        MixteEnrobage   ' Section mixte acier-béton avec enrobage partiel
        SFB             ' Section Slim floor SFB non mixte
        SFBmixte        ' Section Slim floor SFB mixte
        IFB_A           ' Section Slim floor IFB-A non mixte
        IFB_Amixte      ' Section Slim floor IFB-A mixte
        IFB_B           ' Section Slim floor IFB-B non mixte
        IFB_Bmixte      ' Section Slim floor IFB-B mixte
        SAB             ' Section Slim floor SAB non mixte
        SABmixte        ' Section Slim floor SAB mixte
    End Enum

#End Region

#Region " Déclarations "

    Structure strucResultats

    End Structure

#End Region

#Region " Attributs "

    ''' <summary>
    ''' Nom de la section
    ''' </summary>
    Public Nom As String

    ''' <summary>
    ''' Indique si l'utilisateur a défini une dalle de béton
    ''' </summary>
    Public lDalleBeton As Boolean

    ''' <summary>
    ''' Indique si la section est définie par une base de données
    ''' </summary>
    Public lDatabase As Boolean

    ''' <summary>
    ''' Type de la section
    ''' </summary>
    Public typeSection As Enum_TypeSection

#End Region

#Region " Elements de la section "

    '''' <summary>
    '''' Profilé acier
    '''' </summary>
    'Public pProfil As New cls_Profil

    Public ProfilA As New cls_ProfilA

    ''' <summary>
    ''' Acier de la section
    ''' </summary>
    Public Acier As New Cls_Acier

    ''' <summary>
    ''' Enrobage partiel de la section
    ''' </summary>
    Public enrobage_partiel As New Cls_Enrobage_Partiel

    ''' <summary>
    ''' Dalle béton de la poutre            ' A SUPPRIMER ?
    ''' </summary>
    'Public Dalle As New Cls_Dalle

#End Region

#Region " Autres attributs "

    ''' <summary>
    ''' Indique si on modélise les armatures par un cercle concentré
    ''' </summary>
    Public lArmaturesConcentrees As Boolean

#End Region

#Region " Propriétés plastiques de la section "

    Public Sub ProprietesPlastiquesMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As Cls_Gamma, RhoV As Decimal,
                                       ByRef zANP As Decimal, ByRef MplRd As Decimal,
                                       Optional bEff As Decimal = 0, Optional Eta As Decimal = 1)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques en flexion simple de la section / axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   zANP        [S] :   Position axe neutre plastique
        '   MplRd       [S] :   Moment plastique
        '   bEff        [E] :   Largeur efficace de la dalle (si secion mixte)
        '   Eta         [E] :   Degré de connexion (si section mixte)
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Dim LargeurC, EpaisseurC, FdC As Decimal
        Dim nEqEc As Decimal

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.ProfilA.AireFs, Me.ProfilA.t_fs, -Me.ProfilA.t_fs / 2, 1, 1, 1, Me.FySup, 1, Gammas.GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.ProfilA.t_w, Hw, -Me.ProfilA.t_fs - Hw / 2, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.ProfilA.AireFi, Me.ProfilA.t_fi, -Me.ProfilA.ha + Me.ProfilA.t_fi / 2, 1, 1, 1, Me.FyInf, 1, Gammas.GammaM0)

        If lLamine Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.ProfilA.r_cs, -Me.ProfilA.t_fs, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.ProfilA.r_ci, -Me.ProfilA.ha + Me.ProfilA.t_fs, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '# Béton d'enrobage

        If Me.lEnrobage Then

            LargeurC = (Me.LargeurEnrobagePartielBc - Me.ProfilA.t_w)
            EpaisseurC = Me.ProfilA.HauteurAmeHw
            FdC = Me.enrobage_partiel.Beton.Fck

            MyModele.AddMaille(LargeurC * EpaisseurC, EpaisseurC, -Me.ProfilA.ha / 2, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.Rectangulaire)

            'Pour les profilés laminés, on doit retirer du béton la parties correspondant aux congés

            If lLamine Then

                '# Congés supérieurs

                MyModele.AddMailleConges(Me.ProfilA.r_cs, -Me.ProfilA.t_fs, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.CongeSup, -1)

                '# Congés supérieurs

                MyModele.AddMailleConges(Me.ProfilA.r_ci, -Me.ProfilA.ha + Me.ProfilA.t_fs, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.CongeInf, -1)

            End If
        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then

            Dim zArma, PhiA As Decimal
            Dim iPos, iBarre As Integer
            Dim NbBarres As Integer
            Dim Fsk As Decimal = Me.enrobage_partiel.AcierArmatures.FsK
            Dim ArmaNeq As Decimal = Cls_Acier.EYACIER / Me.enrobage_partiel.AcierArmatures.Es
            Const DELTACArma As Decimal = 0 ' pour le le moment on néglige les armatures comprimées
            Const NBMA As Integer = 2
            nEqEc = Cls_Acier.EYACIER / Me.enrobage_partiel.AcierArmatures.Es

            For iArma As Integer = 0 To 2

                For iPos = 0 To 2

                    NbBarres = Me.enrobage_partiel.LitArma(iArma).NbBarres(iPos)

                    For iBarre = 1 To NbBarres
                        zArma = Me.zPosArmaEnrobage(iArma, iPos, iBarre)
                        PhiA = Me.enrobage_partiel.LitArma(iArma).PhiBarre(iPos)

                        MyModele.AddMailleCirculaire(PhiA / 2, zArma, 1, DELTACArma, ArmaNeq, Fsk, 1, Gammas.GammaS, NBMA, Cls_Maille.EnuTypeMaille.Circulaire)

                    Next

                Next

            Next

        End If

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Public Sub ProprietesPlastiquesMixteMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As Cls_Gamma, RhoV As Decimal,
                                            bEff As Decimal, Eta As Decimal, MyDalle As Cls_Dalle,
                                            ByRef zANP As Decimal, ByRef MplRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques en flexion simple de la section / axe fort prenant en compte la mixité avec la dalle
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   RhoV        [E] :   Coefficient pour l'interaction MV
        '   bEff        [E] :   Largeur efficace de la dalle (si secion mixte)
        '   Eta         [E] :   Degré de connexion (si section mixte)
        '   MyDalle     [E] :   Dalle
        '   zANP        [S] :   Position axe neutre plastique
        '   MplRd       [S] :   Moment plastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Dim LargeurC, EpaisseurC, FdC As Decimal
        Dim nEqEc As Decimal = 1            ' On Applique 1 car calcul plastique
        Const nEqD As Decimal = 1           ' Idem
        Dim Aire As Decimal

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.ProfilA.AireFs, Me.ProfilA.t_fs, -Me.ProfilA.t_fs / 2, 1, 1, 1, Me.FySup, 1, Gammas.GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.ProfilA.t_w, Hw, -Me.ProfilA.t_fs - Hw / 2, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.ProfilA.AireFi, Me.ProfilA.t_fi, -Me.ProfilA.ha + Me.ProfilA.t_fi / 2, 1, 1, 1, Me.FyInf, 1, Gammas.GammaM0)

        If lLamine Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.ProfilA.r_cs, -Me.ProfilA.t_fs, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.ProfilA.r_ci, -Me.ProfilA.ha + Me.ProfilA.t_fs, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '# Béton d'enrobage

        If Me.lEnrobage Then

            LargeurC = (Me.LargeurEnrobagePartielBc - Me.ProfilA.t_w)
            EpaisseurC = Me.ProfilA.HauteurAmeHw
            FdC = Me.enrobage_partiel.Beton.Fck

            MyModele.AddMaille(LargeurC * EpaisseurC, EpaisseurC, -Me.ProfilA.ha / 2, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.Rectangulaire)

            'Pour les profilés laminés, on doit retirer du béton la parties correspondant aux congés

            If lLamine Then

                '# Congés supérieurs

                MyModele.AddMailleConges(Me.ProfilA.r_cs, -Me.ProfilA.t_fs, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.CongeSup, -1)

                '# Congés supérieurs

                MyModele.AddMailleConges(Me.ProfilA.r_ci, -Me.ProfilA.ha + Me.ProfilA.t_fs, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.CongeInf, -1)

            End If
        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then

            Dim zArma, PhiA As Decimal
            Dim iPos, iBarre As Integer
            Dim NbBarres As Integer
            Dim Fsk As Decimal = Me.enrobage_partiel.AcierArmatures.FsK
            Dim ArmaNeq As Decimal = Cls_Acier.EYACIER / Me.enrobage_partiel.AcierArmatures.Es
            Const DELTACArma As Decimal = 0 ' pour le le moment on néglige les armatures comprimées
            Const NBMA As Integer = 2
            'nEqEc = Cls_Acier.EYACIER / Me.enrobage_partiel.AcierArmatures.Es

            For iArma As Integer = 0 To 2

                For iPos = 0 To 2

                    NbBarres = Me.enrobage_partiel.LitArma(iArma).NbBarres(iPos)

                    For iBarre = 1 To NbBarres
                        zArma = Me.zPosArmaEnrobage(iArma, iPos, iBarre)
                        PhiA = Me.enrobage_partiel.LitArma(iArma).PhiBarre(iPos)

                        MyModele.AddMailleCirculaire(PhiA / 2, zArma, 1, DELTACArma, ArmaNeq, Fsk, 1, Gammas.GammaS, NBMA, Cls_Maille.EnuTypeMaille.Circulaire)

                    Next

                Next

            Next

        End If

        '--> Dalle béton

        If lMixte And (bEff > 0) Then

            Dim Tc As Decimal = MyDalle.EpaisseurActive
            Aire = bEff * Tc
            MyModele.AddMaille(Aire, Tc, MyDalle.zTop - Tc / 2, 0, 1, nEqD, MyDalle.beton.Fck, 0.85, Gammas.GammaC)

        End If

        '--> Recherche de l'axe neutre plastique

        MyModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

#End Region

#Region " Propriétés élastiques de la section "

    Public Sub ProprietesElastiquesMyy(Signe As Decimal, lValeurRd As Boolean, Gammas As Cls_Gamma, nEqEc As Decimal,
                                       ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef MelRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe fort
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   nEqEc       [E] :   Coefficient d'équivalence acier béton pour l'enrobage partiel
        '   zANE        [E] :   Position axe neutre élastique
        '   MelRd       [E] :   Moment élastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        Dim LargeurC, EpaisseurC, FdC As Decimal

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.ProfilA.AireFs, Me.ProfilA.t_fs, -Me.ProfilA.t_fs / 2, 1, 1, 1, Me.FySup, 1, Gammas.GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.ProfilA.t_w, Hw, -Me.ProfilA.t_fs - Hw / 2, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.ProfilA.AireFi, Me.ProfilA.t_fi, -Me.ProfilA.ha + Me.ProfilA.t_fi / 2, 1, 1, 1, Me.FyInf, 1, Gammas.GammaM0)

        If lLamine Then

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.ProfilA.r_cs, -Me.ProfilA.t_fs, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

            '# Congés supérieurs

            MyModele.AddMailleConges(Me.ProfilA.r_ci, -Me.ProfilA.ha + Me.ProfilA.t_fs, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '# Béton d'enrobage

        If Me.lEnrobage Then

            LargeurC = (Me.LargeurEnrobagePartielBc - Me.ProfilA.t_w)
            EpaisseurC = Me.ProfilA.HauteurAmeHw
            FdC = Me.enrobage_partiel.Beton.Fck

            MyModele.AddMaille(LargeurC * EpaisseurC, EpaisseurC, -Me.ProfilA.ha / 2, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.Rectangulaire)

            'Pour les profilés laminés, on doit retirer du béton la parties correspondant aux congés

            If lLamine Then

                '# Congés supérieurs

                MyModele.AddMailleConges(Me.ProfilA.r_cs, -Me.ProfilA.t_fs, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.CongeSup, -1)

                '# Congés supérieurs

                MyModele.AddMailleConges(Me.ProfilA.r_ci, -Me.ProfilA.ha + Me.ProfilA.t_fs, 0, 1, nEqEc, FdC, 0.85, Gammas.GammaC, Cls_Maille.EnuTypeMaille.CongeInf, -1)

            End If
        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then

            Dim zArma, PhiA As Decimal
            Dim iPos, iBarre As Integer
            Dim NbBarres As Integer
            Dim Fsk As Decimal = Me.enrobage_partiel.AcierArmatures.FsK
            Dim ArmaNeq As Decimal = Cls_Acier.EYACIER / Me.enrobage_partiel.AcierArmatures.Es
            Const DELTACArma As Decimal = 0 ' pour le le moment on néglige les armatures comprimées
            Const NBMA As Integer = 2

            For iArma As Integer = 0 To 2

                For iPos = 0 To 2

                    NbBarres = Me.enrobage_partiel.LitArma(iArma).NbBarres(iPos)

                    For iBarre = 1 To NbBarres
                        zArma = Me.zPosArmaEnrobage(iArma, iPos, iBarre)
                        PhiA = Me.enrobage_partiel.LitArma(iArma).PhiBarre(iPos)

                        MyModele.AddMailleCirculaire(PhiA / 2, zArma, 1, DELTACArma, ArmaNeq, Fsk, 0.85, Gammas.GammaS, NBMA, Cls_Maille.EnuTypeMaille.Circulaire)

                    Next

                Next

            Next

        End If

        '--> Dalle béton

        If lMixte Then

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieY = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment plastique

        'MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Public Sub ProprietesElastiquesMzz(Signe As Decimal, lValeurRd As Boolean, Gammas As Cls_Gamma, ByRef zANE As Decimal, ByRef InertieZ As Decimal, ByRef MelRd As Decimal)
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en flexion simple de la section, par rapport à l'axe faible
        '-------------------------------------------------------------------------------------------------------------------
        '   Signe       [E] :   Signe du moment
        '   lValeurRd   [E] :   Vrai si valeur de calcul, faux si valeur caractéristique
        '   Gammas      [E] :   Coefficients partiels
        '   zANE        [S] :   Position axe neutre élastique
        '   MelRd       [S] :   Moment élastique
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = Me.lLamine
        Const RhoV As Decimal = 0
        Dim Rc As Decimal = (Me.ProfilA.r_cs + Me.ProfilA.r_ci) / 2

        '--> Initialisation

        Hw = Me.ProfilA.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        MyModele.AddMaille(Me.ProfilA.AireFs, Me.ProfilA.b_fs, 0, 1, 1, 1, Me.FySup, 1, Gammas.GammaM0)

        '# Âme

        MyModele.AddMaille(Hw * Me.ProfilA.t_w, Me.ProfilA.t_w, 0, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0)

        '# Semelle inférieure

        MyModele.AddMaille(Me.ProfilA.AireFi, Me.ProfilA.b_fi, 0, 1, 1, 1, Me.FyInf, 1, Gammas.GammaM0)

        If lLamine Then

            '# Congés supérieurs (c'est à dire, côté gauche)

            MyModele.AddMailleConges(Rc, -Me.ProfilA.t_w / 2, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeSup)

            '# Congés supérieurs (c'est à dire, côté droite)

            MyModele.AddMailleConges(Rc, +Me.ProfilA.t_w / 2, 1, 1, 1, Me.FyW, (1 - RhoV), Gammas.GammaM0, Cls_Maille.EnuTypeMaille.CongeInf)

        End If

        '# Béton d'enrobage

        If Me.lEnrobage Then


        End If

        '# Armatures de l'enrobage

        If Me.lEnrobage Then


        End If

        '--> Dalle béton

        If lMixte Then

        End If

        '--> Recherche de l'axe neutre élastique

        MyModele.RechercheANE(Signe, zANE)

        '--> Calcul de l'inertie

        InertieZ = MyModele.InertieFlexion(Signe, zANE)

        '--> Moment plastique

        'MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub
    Public Function InertieT()
        '-------------------------------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques en torsion de la section
        '   Pour un profilé acier avec enrobage, on utilise la formule du guide "Déversement des poutres en acier"
        '   Pas de prise en compte de la dalle
        '-------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pInertieT As Decimal
        Dim pInertieTEnrob As Decimal
        Dim nEq As Decimal
        Dim Gc, Ga As Decimal
        Dim hW, Bc As Decimal

        '--> Inertie de torsion du profilé acier seul

        pInertieT = Me.ProfilA.InertieT

        '--> Pour l'enrobage, on ajoute la contribution du béton d'enrobage, avec la formule du guide "Déversement des poutres en acier", page 56 formule (4.19)

        If Me.lEnrobage Then

            nEq = Me.enrobage_partiel.Beton.CoefficientEquivalenceCT
            hW = Me.ProfilA.HauteurAmeHw
            Bc = Me.LargeurEnrobagePartielBc

            Gc = 0.3 * Cls_Acier.EYACIER / nEq
            Ga = Me.Acier.ModuleG

            pInertieTEnrob = 1 / 3 * (1 - 0.63 * Bc / hW) * hW * Bc ^ 3

            pInertieT += 0.1 * pInertieTEnrob * Gc / Ga

        End If

        Return pInertieT

    End Function
#End Region

#Region " Propiétés générales de la section "

    ''' <summary>
    ''' Indique si la section comprend un enrobage partiel
    ''' </summary>
    Public ReadOnly Property lEnrobage As Boolean
        Get
            Return (Me.typeSection = Enum_TypeSection.AcierEnrobage) Or (Me.typeSection = Enum_TypeSection.MixteEnrobage)
        End Get
    End Property

    ''' <summary>
    ''' Indique si la section comprend une dalle connectée au profilé
    ''' </summary>
    Public ReadOnly Property lMixte As Boolean
        Get
            Return (Me.typeSection = Enum_TypeSection.Mixte) Or (Me.typeSection = Enum_TypeSection.MixteEnrobage)
        End Get
    End Property

    ''' <summary>
    ''' Indique si la section comprend un profilé laminé
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property lLamine As Boolean
        Get
            Return (Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine)
        End Get
    End Property

    Public ReadOnly Property LargeurEnrobagePartielBc As Decimal
        Get
            Return Me.ProfilA.b_fs * Me.enrobage_partiel.Ratio_bc
        End Get
    End Property

    Public Sub ProprietesElastiquesEtPlastiques(Signe As Decimal, nEqEc As Decimal, nEqDal As Decimal, lValeurCalcul As Boolean)

        Dim zANP, zANE, InertieY, MplRd As Decimal

        'CalProprietes(Me, Signe, nEqEc, nEqDal, lValeurCalcul, zANE, InertieY, zANP, MplRd)

        'Me.Resultats.InertieY = InertieY
        'Me.Resultats.zANE = zANE
        'Me.Resultats.zANP = zANP
        'Me.Resultats.MplRd = MplRd

    End Sub

    Public Resultats As strucResultats

    ''' <summary>
    ''' Limite d'élasticité de la semelle supérieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FySup As Decimal
        Get
            Return Me.Acier.LimiteFy(Me.ProfilA.t_fs)
        End Get
    End Property

    ''' <summary>
    ''' Limite d'élasticité de la semelle inférieure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FyInf As Decimal
        Get
            Return Me.Acier.LimiteFy(Me.ProfilA.t_fi)
        End Get
    End Property

    ''' <summary>
    ''' Limite d'élasticité de l'âme
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FyW As Decimal
        Get
            Return Me.Acier.LimiteFy(Me.ProfilA.t_w)
        End Get
    End Property

    Public Function VRd(GammaM0 As Decimal) As Decimal

        Dim MyVRd As Decimal = 0

        Select Case Me.typeSection
            Case Enum_TypeSection.Acier, Enum_TypeSection.AcierEnrobage, Enum_TypeSection.Mixte, Enum_TypeSection.MixteEnrobage
                MyVRd = Me.AireAv * Me.FyW / GammaM0 * kConvMPaPa

        End Select

        Return MyVRd
    End Function

    Public ReadOnly Property AireAv As Decimal
        Get
            Return Me.ProfilA.AireAv
        End Get
    End Property

    Public Function RhoInteractionMV(VEd As Decimal, GammaM0 As Decimal) As Decimal

        Dim Rho As Decimal
        Dim VRd As Decimal = Me.VRd(GammaM0)

        Dim VEdAbs As Decimal = Math.Abs(VEd)

        If VEdAbs > 0.5 * VRd Then
            Rho = Math.Min(1, (2 * VEd / VRd - 1) ^ 2)
        Else
            Rho = 0
        End If

        Return Rho
    End Function

    'Public ReadOnly Property RhoVCalcul As Decimal
    '    Get
    '        Dim Rho As Decimal = 0
    '        If Me.Param.lInterActionMV Then Rho = Me.RhoInteractionMV(Me.Param.VEd)
    '        Return Rho
    '    End Get
    'End Property

#End Region

#Region " Fonctions de calcul "

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Private Sub Calcul_Proprietes()

        'A_fs = b_fs * t_fs
        'A_fi = b_fi * t_fi

    End Sub

    Public Function Calcul_Armatures_Minimales_Enrobage_Partiel()
        '--------------------------------------------------------------------------------------
        '   24/06/23 : Création - GuD
        '--------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------
        'Définition des variables locales
        Dim ks As Decimal
        Dim kc As Decimal
        Dim k As Decimal
        Dim fct_eff As Decimal
        Dim Act As Decimal
        Dim sigma_s As Decimal
        Dim As_min As Decimal

        ks = 0.9
        kc = 0.6
        k = 0.8
        fct_eff = enrobage_partiel.Beton.Fctm
        Act = enrobage_partiel.Ratio_bc * ProfilA.b_fs * ProfilA.HauteurAmeHw

        If enrobage_partiel.Beton.lCrackingLimitation Then
            Dim phi_max As Decimal = enrobage_partiel.Get_Phi_Max()
            sigma_s = Mod_Declarations.Get_sigma_S1_Ds(enrobage_partiel.Beton.wk_max, phi_max)
        Else
            sigma_s = enrobage_partiel.AcierArmatures.FsK
        End If

        As_min = ks * kc * k * fct_eff * Act / sigma_s

        Return As_min

    End Function

    ''' <summary>
    ''' Lancement de toutes les fonctions de calcul
    ''' </summary>
    Public Sub Lancement_Calcul(Param As Cls_OptionsCalcul, dalle As Cls_Dalle)

        Calcul_Proprietes()

        Const E As Decimal = 210000 * 10 ^ (6)

        'enrobage
        If Me.lEnrobage Then

            enrobage_partiel.Calcul_Proprietes()
            enrobage_partiel.Beton.Calcul_Proprietes()

            Dim h_w As Decimal = ProfilA.HauteurAmeHw
            'Param.Prop_Elastique_Enrobage.h_0 = 2 * (h_w * (enrobage_partiel.b_c - ProfilA.t_w) - (4 - Math.PI) * ProfilA.r_cs ^ 2) / (2 * h_w)
            Param.Prop_Elastique_Enrobage.h_0 = 2 * (h_w * (enrobage_partiel.Get_b_c(ProfilA.b_fs) - ProfilA.t_w) - (4 - Math.PI) * ProfilA.r_cs ^ 2) / (2 * h_w)
            Param.Prop_Elastique_Enrobage.Calcul_Coeff(E, enrobage_partiel.Beton.Fcm, enrobage_partiel.Beton.Ecm)

        End If

        'dalle
        If lDalleBeton Then

            dalle.Calcul_Proprietes()
            dalle.beton.Calcul_Proprietes()

            If dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte Then
                Param.Prop_Elastique_Dalle.h_0 = 2 * (dalle.t_d - dalle.Bac.h_p)
            Else ' dalle pleine
                Param.Prop_Elastique_Dalle.h_0 = dalle.t_d
            End If
            Param.Prop_Elastique_Dalle.Calcul_Coeff(E, dalle.beton.Fcm, dalle.beton.Ecm)

        End If

    End Sub

#End Region

#Region " Fonctions de cpoie "

    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub DeepClone(ByVal SectionSource As cls_Section, ByRef SectionCible As cls_Section)
        SectionCible = SectionSource.Clone

        SectionSource.ProfilA.DeepClone(SectionSource.ProfilA, SectionCible.ProfilA)
        SectionSource.enrobage_partiel.DeepClone(SectionSource.enrobage_partiel, SectionCible.enrobage_partiel)

        SectionCible.Acier = SectionSource.Acier.Clone

    End Sub

    ''' <summary>*
    ''' Fonction de clone à utiliser
    ''' </summary>
    ''' <param name="s_origine"></param>
    ''' <param name="s_destination"></param>
    Public Shared Sub CloneSection(ByVal s_origine As cls_Section, ByRef s_destination As cls_Section)
        s_destination = s_origine.Clone()
        s_destination.Acier = s_origine.Acier.Clone()

        s_destination.enrobage_partiel = s_origine.enrobage_partiel.Clone()
        s_destination.enrobage_partiel.Beton = s_origine.enrobage_partiel.Beton.Clone()

        's_destination.dalle = s_origine.dalle.Clone()
        's_destination.dalle.beton = s_origine.dalle.beton.Clone()
        's_destination.dalle.arma_longi_inf = s_origine.dalle.arma_longi_inf.Clone()
        's_destination.dalle.arma_longi_sup = s_origine.dalle.arma_longi_sup.Clone()
        's_destination.dalle.bac_acier = s_origine.dalle.bac_acier.Clone()

        's_destination.Param = s_origine.Param.Clone()
        's_destination.Param.Prop_Elastique_Enrobage = s_origine.Param.Prop_Elastique_Enrobage.Clone()
        's_destination.Param.Prop_Elastique_Dalle = s_origine.Param.Prop_Elastique_Dalle.Clone()

    End Sub

#End Region

#Region " Constructeurs "

    Sub New()
    End Sub

    Sub New(ByVal nom As String, ByVal typeSection As Enum_TypeSection)

        Me.Nom = nom
        Me.typeSection = typeSection

        Me.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

        '--> Par défaut définition utilisateur de la section
        Me.lDatabase = True

        '--> Section par défaut peu importe le type


    End Sub

    Sub New(ByVal nom As String, ByVal typeSection As Enum_TypeSection, Nuance As String, Qualite As String, Reduction As String, MyPlages As List(Of Cls_Acier.strucPlage))

        Me.Nom = nom
        Me.typeSection = typeSection

        '--> Par défaut définition utilisateur de la section
        Me.lDatabase = True
        Me.Acier.Nuance = Nuance
        Me.Acier.Qualite = Qualite
        Me.Acier.Reduction = Reduction
        Me.Acier.Plages.Clear()
        For i As Integer = 0 To MyPlages.Count - 1
            Me.Acier.Plages.Add(MyPlages(i))
        Next

    End Sub

#End Region

#Region " Ecriture/Lecture  - Fichier "

    Public Sub EcrireFile(ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   Ecriture des attributs pour enregistrement dans un fichier 
        '   --> 20/02/20 v.1 
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK SECTION")
        '--> Attributs pour l'interface
        Lines.Add("   Nom           = " & Me.Nom)
        Lines.Add("   lEnrobage     = " & Me.lEnrobage)
        Lines.Add("   lDalle        = " & Me.lDalleBeton)
        Lines.Add("   lDatabase     = " & Me.lDatabase)
        'Lines.Add("   DB_Gamme      = " & Me.Gamme)
        'Lines.Add("   DB_Profile    = " & Me.NomProfile)
        ''--> Géométrie
        'Lines.Add("   Type          = " & Me.typeSection)
        'Lines.Add("   H             = " & Me.ha)
        ''Lines.Add("   H_W           = " & Me.h_w)
        'Lines.Add("   T_W           = " & Me.t_w)
        'Lines.Add("   B_FS          = " & Me.b_fs)
        'Lines.Add("   T_FS          = " & Me.t_fs)
        'Lines.Add("   B_FI          = " & Me.b_fi)
        'Lines.Add("   T_FI          = " & Me.t_fi)
        'Lines.Add("   A             = " & Me.a)
        'Lines.Add("   R             = " & Me.r_cs)

        '--> Acier
        Me.Acier.EcrireFile(Lines)

        '--> Enrobage
        Me.enrobage_partiel.EcrireFile(Lines)

        ''--> Dalle de béton
        'Me.dalle.EcrireFile(Lines)

        ''--> Options de calcul
        'Me.Param.EcrireFile(Lines)

        Lines.Add("")

    End Sub

    Public Sub LectureFile(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        Try
            '--> Traitement
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                    Select Case MotCle

                        Case "NOM"
                            Me.Nom = ""
                            For z = 2 To nbMots
                                If z = nbMots Then
                                    Me.Nom += Mots(z)
                                Else
                                    Me.Nom += Mots(z) + " "
                                End If
                            Next
                        'Case "LENR" : Me.lEnrobagePartiel = Mots(nbMots)
                        Case "LDAL" : Me.lDalleBeton = Mots(nbMots)
                        Case "LDAT" : Me.lDatabase = Mots(nbMots)
                        'Case "DB_G" : Me.Gamme = Mots(nbMots)
                        'Case "DB_P" : Me.NomProfile = Mots(nbMots)
                            '--> Géométrie
                        Case "TYPE" : Me.typeSection = Mots(nbMots)
                        'Case "H" : Me.ha = Mots(nbMots)
                        ''Case "H_W" : Me.h_w = Mots(nbMots)
                        'Case "T_W" : Me.t_w = Mots(nbMots)
                        'Case "B_FS" : Me.b_fs = Mots(nbMots)
                        'Case "T_FS" : Me.t_fs = Mots(nbMots)
                        'Case "B_FI" : Me.b_fi = Mots(nbMots)
                        'Case "T_FI" : Me.t_fi = Mots(nbMots)
                        'Case "A" : Me.a = Mots(nbMots)
                        'Case "R" : Me.r_cs = Mots(nbMots)
                            '--> Acier
                        Case "NUAN"
                            Me.Acier.Nuance = ""
                            For z = 2 To nbMots
                                If z = nbMots Then
                                    Me.Acier.Nuance += Mots(z)
                                Else
                                    Me.Acier.Nuance += Mots(z) + " "
                                End If
                            Next
                        Case "QUAL" : Me.Acier.Qualite = Mots(nbMots)
                        Case "FYW" : Me.Acier.f_y.w = Mots(nbMots)
                        Case "FYFS" : Me.Acier.f_y.fs = Mots(nbMots)
                        Case "FYFI" : Me.Acier.f_y.fi = Mots(nbMots)
                            '--> Enrobage
                        'Case "EB_C" : Me.enrobage_partiel.b_c = Mots(nbMots)
                       ' Case "EF_Y" : Me.enrobage_partiel.acier_armature = Mots(nbMots)
                            '--> Béton enrobage
                        Case "EBTY" : Me.enrobage_partiel.Beton.Type = Mots(nbMots)
                        Case "EBCL" : Me.enrobage_partiel.Beton.Classe = Mots(nbMots)
                        Case "EBFC" : Me.enrobage_partiel.Beton.Fck = Mots(nbMots)


                    End Select
                End If

            Next
        Catch ex As Exception
            MsgBox("Erreur lecture fichier pmx - données corrompues" & Chr(10) & "Error read file pmx - corrupt data", MsgBoxStyle.Critical, "Cls_Section/LectureFile")
        End Try

    End Sub

#End Region

#Region " Outils "

    Public Sub InitialisePositionArmaturesEnrobage()
        '--------------------------------------------------------------------------------------------
        '   26/04/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Positionnement des armatures de la section d'enrobage
        '--------------------------------------------------------------------------------------------

        With Me.enrobage_partiel

            .LitsArmaOLD(0).zArma = -Me.ProfilA.ha + Me.ProfilA.t_fi + .Etriers_EnrobageZ + .Etriers_Phi + .LitsArmaOLD(0).Phi / 2

            .LitsArmaOLD(2).zArma = -Me.ProfilA.t_fs - .Etriers_EnrobageZ - .Etriers_Phi - .LitsArmaOLD(2).Phi / 2

            .LitsArmaOLD(1).zArma = (.LitsArmaOLD(0).zArma + .LitsArmaOLD(2).zArma) / 2

        End With

    End Sub

    ''' <summary>
    ''' Renvoie la position z d'un lit d'armature dans l'enrobage
    ''' </summary>
    ''' <param name="iArma"></param>
    ''' <returns></returns>
    Public Function zPositionLitArmaEnrobage(iArma As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   12/06/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie la position z d'un lit d'armature de l'enrobage partiel (z pondéré)
        '------------------------------------------------------------------------------------------------------------
        '   iArma   [E] :   Indice du lit d'armature (0: inférieur / 1: central / 2: supérieur)
        '------------------------------------------------------------------------------------------------------------

        Dim zPos As Decimal
        Dim DeltaZ As Double

        DeltaZ = Me.enrobage_partiel.LitArma(iArma).NbExt * Me.enrobage_partiel.LitArma(iArma).PhiExt ^ 3 / 8
        DeltaZ += Me.enrobage_partiel.LitArma(iArma).NbMil * Me.enrobage_partiel.LitArma(iArma).PhiMil ^ 3 / 8
        DeltaZ += Me.enrobage_partiel.LitArma(iArma).NbInt * Me.enrobage_partiel.LitArma(iArma).PhiInt ^ 3 / 8

        DeltaZ = DeltaZ * Math.PI / Me.enrobage_partiel.LitArma(iArma).Aire

        Select Case iArma
            Case 0
                zPos = +DeltaZ - Me.ProfilA.ha + Me.ProfilA.t_fi _
                     + Me.enrobage_partiel.Etriers_EnrobageZ + Me.enrobage_partiel.Etriers_Phi

            Case 1
                zPos = -Me.enrobage_partiel.LitArma(iArma).zPosRatio * Me.ProfilA.ha
            Case 2
                zPos = -DeltaZ - Me.ProfilA.t_fs _
                     - Me.enrobage_partiel.Etriers_EnrobageZ - Me.enrobage_partiel.Etriers_Phi
        End Select

        Return zPos
    End Function

    Public Function zPosArmaEnrobage(iArma As Integer, iPos As Integer, iBarre As Integer) As Decimal
        '--------------------------------------------------------------------------------------------
        '   12/06/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Renvoie la position z d'une barre d'armature longi de l'enrobage
        '--------------------------------------------------------------------------------------------
        '   iArma       [E] :   Indique lit d'armature (0: inférieur/ 1: milieu/ 2: supérieur)
        '   iPos        [E] :   Position des barres (0: extérieur/ 1: centre / 2: intérieur)
        '   iBarre      [E] :   Indice de la barre dans la grappe, de 1 à 3
        '--------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim zPos As Decimal
        Dim PhiA, PhiE As Decimal
        Dim Uz As Decimal

        '--> Intialisations

        Uz = Me.enrobage_partiel.Etriers_EnrobageZ
        PhiE = Me.enrobage_partiel.Etriers_Phi
        Select Case iPos
            Case 0 : PhiA = Me.enrobage_partiel.LitArma(iArma).PhiExt
            Case 1 : PhiA = Me.enrobage_partiel.LitArma(iArma).PhiMil
            Case 2 : PhiA = Me.enrobage_partiel.LitArma(iArma).PhiInt
        End Select
        '--> Traitement

        Select Case iArma

            Case 0
                '-- LIT INFERIEUR---------------------------
                zPos = -Me.ProfilA.ha + Me.ProfilA.t_fi + Uz + PhiE + PhiA / 2
                If iBarre = 3 Then zPos += PhiA * Math.Sqrt(3) / 2
            Case 1
                '-- LIT CENTRAL ----------------------------
                zPos = zPositionLitArmaEnrobage(1)
            Case 2
                '-- LIT SUPERIEUR---------------------------
                zPos = -Me.ProfilA.t_fs - Uz - PhiE - PhiA / 2
                If iBarre = 3 Then zPos -= PhiA * Math.Sqrt(3) / 2
        End Select

        Return zPos
    End Function

#End Region

#Region " Calcul de propriétés élastiques "

    Public Function MomentElastique(Inertie As Decimal, zANE As Decimal, Moment As Decimal, Optional lCarac As Boolean = False) As Decimal
        '-----------------------------------------------------------------------------------------------------------------
        '   29/04/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Calcul du moment élastique de la section
        '-----------------------------------------------------------------------------------------------------------------
        '   Inertie     [E] :   Moment d'inertie de la section
        '   zANE        [E] :   Position ANE élastique
        '   Moment      [E] :   Signe du moment
        '   lCarac      [E] :   Indique si valeur caractéristique du moment
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim kUlt As Decimal
        Dim Sigma, SigmaLim As Decimal
        Dim GammaM0 As Decimal

        '--> Initialisation

        GammaM0 = 1

        '--> Section acier

        '# Semelle supérieure

        Sigma = Moment / Inertie * (zANE)
        SigmaLim = Me.FySup / GammaM0
        kUlt = Math.Abs(SigmaLim / Sigma)

        '# Semelle inférieure

        Sigma = Moment / Inertie * (zANE + Me.ProfilA.ha)
        SigmaLim = Me.FyInf / GammaM0
        kUlt = Math.Min(Math.Abs(SigmaLim / Sigma), kUlt)

        '--> Enrobage partiel

        If Me.lEnrobage Then

        End If

        '--> Dalle

        If Me.lMixte Then

        End If

        '--> Fin

        Return kUlt * Moment * kConvMPaPa

    End Function

#End Region

#Region "   Recherche d'un acier compatible dans la base de données "

    Public Sub AssocieAcierCompatible(ByVal FileSteels As String, ByVal FileProfiles As String, ByRef lTrouve As Boolean)
        '--------------------------------------------------------------------------------
        '
        '   06/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Associe à une profilé le premier acier compatible dans la base de données
        '
        '--------------------------------------------------------------------------------
        '
        '   FileSteels      [E] :   Nom du fichier binaire base de données de aciers
        '   FileProfiles    [E] :   Nom du fichier binaire base de données des profilés
        '
        '   lTrouve         [S] :   Indique si on a pu trouver un acier compatible
        '
        '--------------------------------------------------------------------------------
        '
        '   On prend le premier acier S355 disponible
        '   et si on ne le trouve pas, le premier acier tout court
        '
        '--------------------------------------------------------------------------------

        'Dim iAcier As Integer
        'Dim MySteels As New List(Of strucAcierLocal)
        'Dim SteelBase As strucBaseAciers
        'Dim iStd As Short

        'Me.ExtraireAciersCompatibles(FileSteels, FileProfiles, MySteels, SteelBase)

        'Me.AnalyseAciersListe(MySteels, True, Cls_Acier.NUANCEDEFAULT, lTrouve, iAcier)

        'If Not lTrouve Then
        '    Me.AnalyseAciersListe(MySteels, False, "", lTrouve, iAcier)
        'End If

        'If lTrouve Then
        '    Me.Acier.Nuance = MySteels(iAcier).Nuance
        '    Me.Acier.Qualite = MySteels(iAcier).Qualite
        '    Me.Acier.Reduction = MySteels(iAcier).Reduc
        '    Me.Acier.EpMax = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).EpMax
        '    Me.Acier.iBase = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).iBase
        '    Me.Acier.iStandart = SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).StIndex

        '    '==V4.00
        '    iStd = SteelBase.IndexStd.IndexOf(SteelBase.Grades(Me.Acier.Nuance).Qualites(Me.Acier.Qualite).ReductionCurv(Me.Acier.Reduction).StIndex)
        '    Me.Acier.iTabStandart = iStd

        '    Me.Acier.Plages.Clear()

        '    For i As Integer = 0 To SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).Plages.Count - 1
        '        Me.Acier.Plages.Add(SteelBase.Grades(MySteels(iAcier).Nuance).Qualites(MySteels(iAcier).Qualite).ReductionCurv(MySteels(iAcier).Reduc).Plages(i))
        '    Next
        'End If
    End Sub

    Private Sub AnalyseAciersListe(ByVal MySteels As List(Of strucAcierLocal), ByVal lImposedGrade As Boolean,
                                   ByVal MyGrade As String, ByRef lTrouve As Boolean, ByRef iAcier As Integer)
        '--------------------------------------------------------------------------------
        '
        '   21/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Extrait tous les aciers compatibles avec un profilé 
        '
        '--------------------------------------------------------------------------------
        '
        '   FileSteels      [E] :   Nom du fichier binaire base de données de aciers
        '   FileProfiles    [E] :   Nom du fichier binaire base de données des profilés
        '
        '--------------------------------------------------------------------------------

        iAcier = -1

        lTrouve = False

        Do While (Not lTrouve) And iAcier < MySteels.Count - 1
            iAcier += 1
            If lImposedGrade Then
                lTrouve = (MySteels(iAcier).Nuance.Trim.ToUpper = MyGrade.ToUpper.Trim)
            Else
                lTrouve = True
            End If
        Loop
    End Sub

    Private Sub ExtraireAciersCompatibles(ByVal FileSteels As String, ByVal FileProfiles As String,
                                          ByVal MySteels As List(Of strucAcierLocal), CorIndStd As Dictionary(Of Short, Short))
        '--------------------------------------------------------------------------------
        '
        '   21/12/12 :  Création - POM - V3.00
        '
        '--------------------------------------------------------------------------------
        '
        '   Extrait tous les aciers compatibles avec un profilé 
        '
        '--------------------------------------------------------------------------------
        '
        '   MySteels        [E] :   Liste des aciers
        '   lImposedGrade   [E] :   Indique si une nuance est imposée ou pas
        '   MyGrade         [E] :   Nuance eventuellement imposée
        '
        '   lTrouve         [S] :   Indique si on a pu trouver un acier compatible
        '
        '
        '--------------------------------------------------------------------------------

        'Dim CorIndStd As Dictionary(Of Short, Short)
        'Dim lCompatible As Boolean
        'Dim EpMax As Double
        'Dim lIsNuanceCompatibleProfile As Boolean
        'Dim lAdd As Boolean
        'Dim SteelLoc As strucAcierLocal
        'Dim ListeSteel As New List(Of strucAcierLocal)
        'Dim nbComp As Integer

        ''--> Initialisation

        ''InitialiseBaseAciers(FileSteels, MyConst.NFACCES, SteelBase)
        'EpMax = Math.Max(Math.Max(Me.ProfilA.t_fi, Me.ProfilA.t_fs), Me.ProfilA.t_w)
        ''GetTabCorrespondanceIndiceStandart(FileProfiles, CorIndStd)
        'MySteels.Clear()
        'ListeSteel.Clear()
        'nbComp = 0

        ''--> Boucle sur les aciers de la base

        'For Each kvpGrade As KeyValuePair(Of String, strucGrade) In SteelBase.Grades

        '    For Each kvpQualite As KeyValuePair(Of String, strucQualite) In kvpGrade.Value.Qualites

        '        For Each kvpSteel As KeyValuePair(Of String, strucReduction) In kvpQualite.Value.ReductionCurv

        '            lCompatible = SteelIsToCompatibleToProfile(EpMax, Me.iStandard, SteelBase, CorIndStd, kvpGrade.Key, kvpQualite.Key, kvpSteel.Key, OptionsDataBase.ChoiceSteel, lIsNuanceCompatibleProfile)

        '            If lCompatible Then
        '                SteelLoc.Nuance = kvpGrade.Key
        '                SteelLoc.Qualite = kvpQualite.Key
        '                SteelLoc.Reduc = kvpSteel.Key
        '                SteelLoc.lAvailable = lIsNuanceCompatibleProfile
        '                ListeSteel.Add(SteelLoc)
        '                If lIsNuanceCompatibleProfile Then nbComp += 1
        '            End If

        '        Next
        '    Next
        'Next

        'For i As Integer = 0 To ListeSteel.Count - 1
        '    If Not ListeSteel(i).lAvailable Then
        '        If (OptionsDataBase.ChoiceSteel = EnuChoiceAcier.BaseIfNoStandardSteel) Then
        '            lAdd = (nbComp = 0)
        '        Else
        '            lAdd = True
        '        End If
        '    Else
        '        lAdd = True
        '    End If
        '    If lAdd Then
        '        MySteels.Add(ListeSteel(i))
        '    End If
        'Next
    End Sub

#End Region

End Class
