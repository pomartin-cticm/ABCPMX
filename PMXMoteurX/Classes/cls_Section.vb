Imports System.Collections.Specialized.BitVector32

Public Class cls_Section


#Region " Enumérations "

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
    ''' Dalle béton de la poutre
    ''' </summary>
    Public Dalle As New Cls_Dalle

#End Region

#Region " Autres attributs "

    ''' <summary>
    ''' Indique si on modélise les armatures par un cercle concentré
    ''' </summary>
    Public lArmaturesConcentrees As Boolean

#End Region

#Region " Propiétés de la section "

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

    ''' <summary>
    ''' Lancement de toutes les fonctions de calcul
    ''' </summary>
    Public Sub Lancement_Calcul(Param As Cls_OptionsCalcul, dalle As Cls_Dalle)

        Calcul_Proprietes()

        Const E As Decimal = 210000 * 10 ^ (6)

        'enrobage
        If Me.lEnrobage Then

            enrobage_partiel.Calcul_Proprietes()
            enrobage_partiel.beton.Calcul_Proprietes()

            Dim h_w As Decimal = ProfilA.HauteurAmeHw
            Param.Prop_Elastique_Enrobage.h_0 = 2 * (h_w * (enrobage_partiel.b_c - ProfilA.t_w) - (4 - Math.PI) * ProfilA.r_cs ^ 2) / (2 * h_w)
            Param.Prop_Elastique_Enrobage.Calcul_Coeff(E, enrobage_partiel.beton.Fcm, enrobage_partiel.beton.Ecm)

        End If

        'dalle
        If lDalleBeton Then

            dalle.Calcul_Proprietes()
            dalle.beton.Calcul_Proprietes()

            If dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte Then
                Param.Prop_Elastique_Dalle.h_0 = 2 * (dalle.t_d - dalle.bac_acier.h_p)
            Else ' dalle pleine
                Param.Prop_Elastique_Dalle.h_0 = dalle.t_d
            End If
            Param.Prop_Elastique_Dalle.Calcul_Coeff(E, dalle.beton.Fcm, dalle.beton.Ecm)

        End If

    End Sub

#End Region

#Region " Copy de la section "

    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    ''' <summary>
    ''' Fonction de clone à utiliser
    ''' </summary>
    ''' <param name="s_origine"></param>
    ''' <param name="s_destination"></param>
    Public Shared Sub CloneSection(ByVal s_origine As cls_Section, ByRef s_destination As cls_Section)

        s_destination = s_origine.Clone()
        s_destination.acier = s_origine.acier.Clone()

        s_destination.enrobage_partiel = s_origine.enrobage_partiel.Clone()
        s_destination.enrobage_partiel.beton = s_origine.enrobage_partiel.beton.Clone()
        s_destination.enrobage_partiel.arma_longi_inf = s_origine.enrobage_partiel.arma_longi_inf.Clone()
        s_destination.enrobage_partiel.arma_longi_sup = s_origine.enrobage_partiel.arma_longi_sup.Clone()

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

        Me.acier.Nuance = Nuance
        Me.acier.Qualite = Qualite
        Me.acier.Reduction = Reduction

        Me.acier.Plages.Clear()
        For i As Integer = 0 To MyPlages.Count - 1
            Me.acier.Plages.Add(MyPlages(i))
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
        Me.acier.EcrireFile(Lines)

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
                            Me.acier.Nuance = ""
                            For z = 2 To nbMots
                                If z = nbMots Then
                                    Me.acier.Nuance += Mots(z)
                                Else
                                    Me.acier.Nuance += Mots(z) + " "
                                End If
                            Next
                        Case "QUAL" : Me.acier.Qualite = Mots(nbMots)
                        Case "FYW" : Me.acier.f_y.w = Mots(nbMots)
                        Case "FYFS" : Me.acier.f_y.fs = Mots(nbMots)
                        Case "FYFI" : Me.acier.f_y.fi = Mots(nbMots)
                            '--> Enrobage
                        Case "EB_C" : Me.enrobage_partiel.b_c = Mots(nbMots)
                       ' Case "EF_Y" : Me.enrobage_partiel.acier_armature = Mots(nbMots)
                            '--> Béton enrobage
                        Case "EBTY" : Me.enrobage_partiel.beton.Type = Mots(nbMots)
                        Case "EBCL" : Me.enrobage_partiel.beton.Classe = Mots(nbMots)
                        Case "EBFC" : Me.enrobage_partiel.beton.Fck = Mots(nbMots)
                            '--> Armature Inf enrobage
                        Case "EABC" : Me.enrobage_partiel.arma_longi_inf.c_s = Mots(nbMots)
                        Case "EABD" : Me.enrobage_partiel.arma_longi_inf.PhiS = Mots(nbMots)
                        Case "EABN" : Me.enrobage_partiel.arma_longi_inf.n_s = Mots(nbMots)
                        Case "EABE" : Me.enrobage_partiel.arma_longi_inf.EspBar = Mots(nbMots)
                        Case "EABZ" : Me.enrobage_partiel.arma_longi_inf.z_s = Mots(nbMots)
                              '--> Armature Sup enrobage
                        Case "EAHC" : Me.enrobage_partiel.arma_longi_sup.c_s = Mots(nbMots)
                        Case "EAHD" : Me.enrobage_partiel.arma_longi_sup.PhiS = Mots(nbMots)
                        Case "EAHN" : Me.enrobage_partiel.arma_longi_sup.n_s = Mots(nbMots)
                        Case "EAHE" : Me.enrobage_partiel.arma_longi_sup.EspBar = Mots(nbMots)
                        Case "EAHZ" : Me.enrobage_partiel.arma_longi_sup.z_s = Mots(nbMots)

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

            .LitsArma(0).zArma = -Me.ProfilA.ha + Me.ProfilA.t_fi + .Etriers_EnrobageZ + .Etriers_Phi + .LitsArma(0).Phi / 2

            .LitsArma(2).zArma = -Me.ProfilA.t_fs - .Etriers_EnrobageZ - .Etriers_Phi - .LitsArma(2).Phi / 2

            .LitsArma(1).zArma = (.LitsArma(0).zArma + .LitsArma(2).zArma) / 2

        End With

    End Sub



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


End Class
