Imports System.Collections.Specialized.BitVector32
Imports System.IO
Imports System.Net.Mime.MediaTypeNames

Public Class cls_Projet

#Region " Attributs "

    '--> Paramètres générals

    ''' <summary>
    ''' Nom du projet
    ''' </summary>
    Public Nom As String

    ''' <summary>
    ''' Utilisateur du projet
    ''' </summary>
    Public Utilisateur As String

    ''' <summary>
    ''' Entreprise du projet
    ''' </summary>
    Public Entreprise As String

    ''' <summary>
    ''' Poutres du projet
    ''' </summary>
    Public Poutres As New List(Of cls_Poutre)

    ''' <summary>
    ''' Indice de l'assemblage sélectionné dans la liste
    ''' </summary>
    Public IndEnCours As Integer

    ''' <summary>
    ''' Indique si le projet est modifié après sauvegarde  
    ''' </summary>
    Public lModif As Boolean

    ''' <summary>
    ''' Indique si le projet est déjà enregistré
    ''' </summary>
    Public lSave As Boolean

    ''' <summary>
    ''' Chemin du fichier du projet déjà enregistré
    ''' </summary>
    Public FileName As String

#End Region

#Region " Constructeurs "

    ''' <summary>
    ''' Constructeur vide
    ''' </summary>
    Sub New()

        IndEnCours = -1
        Me.Nom = ""
        Me.Utilisateur = ""
        Me.Entreprise = ""
        Me.FileName = ""

    End Sub

    ''' <summary>
    ''' Constructeur avec l'utilisateur et l'entreprise
    ''' </summary>
    ''' <param name="utilisateur"></param>
    ''' <param name="entreprise"></param>
    Sub New(ByVal utilisateur As String, ByVal entreprise As String)

        Me.Utilisateur = utilisateur
        Me.Entreprise = entreprise

        IndEnCours = -1

        Me.Nom = ""
        Me.FileName = ""

    End Sub

#End Region

#Region " Ecriture / Lecture - Fichier "

    Public Sub EcrireFile(ByRef Lines As List(Of String), ByVal version As String)
        '-------------------------------------------------------------------------------------
        '   Ecriture des attributs pour enregistrement dans un fichier 
        '-------------------------------------------------------------------------------------

        '==[ Entete ]=========================================================================
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("'PropMix software - CTICM - Version " & version)
        Lines.Add("'PROJECT USER FILE")
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("'       /!\    Don't edit this file    /!\")
        Lines.Add("'       /!\ Ne pas modifier ce fichier /!\")
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("")

        '==[ Identification ]=================================================================
        Lines.Add("BLOCK IDENTIFICATION")
        Lines.Add("   Utilisateur   = " & Me.Utilisateur)
        Lines.Add("   Entreprise    = " & Me.Entreprise)
        Lines.Add("   Nom           = " & Me.Nom)
        Lines.Add("")

        '==[ Nuances d'acier utilisateur ]=================================================================
        Dim nuancesSave As New List(Of String)

        'For Each s In List_Section

        '    If s.acier.qualite = "USER" And Not nuancesSave.Contains(s.acier.nuance) Then
        '        Lines.Add("BLOCK NUANCEACIER")
        '        Lines.Add("   Nuance        = " & s.acier.nuance)
        '        Lines.Add("   Fy            = " & s.acier.f_y.w)
        '        Lines.Add("")
        '        nuancesSave.Add(s.acier.nuance)
        '    End If

        'Next

        ''==[ Sections ]==================================================================================
        'For Each s In List_Section
        '    s.EcrireFile(Lines)
        'Next

        '==[ Classe Poutre ]=================================================================
        For Each ptre As cls_Poutre In Me.Poutres

            With ptre

                Lines.Add("BLOCK POUTRE")
                Lines.Add("   Label          =  " & .Label)
                Lines.Add("   TypeSection   =  " & .TypeSection)
                Lines.Add("   ConsoleGauche  =  " & .lTraveeConsoleGauche)
                Lines.Add("   ConsoleDroite  =  " & .lTraveeConsoleDroite)
                Lines.Add("   lTremieGauche  =  " & .lTremieGauche)
                Lines.Add("   lTremieDroite  =  " & .lTremieDroite)
                Lines.Add("   NbTravee       =  " & .NombreTraveesDeuxAppuis)
                Lines.Add("   LongueurTravee =  " & ConvertListToString(.LongueurTravee))

                Dim listTypTravee(.TypTravee.Count - 1) As Integer

                For i As Integer = 0 To listTypTravee.Count - 1
                    listTypTravee(i) = .TypTravee(i)
                Next
                Lines.Add("   TypeTravee     =  " & ConvertListIntegerToString(listTypTravee))

                Lines.Add("   TypeEtaiement  =  " & .TypeEtaiement)
                Lines.Add("   EtaisConsG     =  " & .lEtaisConsoleGauche)
                Lines.Add("   EtaisConsD  =  " & .lEtaisConsoleDroite)
                Lines.Add("   NbPropping     =  " & .NbPropping)
                Lines.Add("   NbRestrain     =  " & ConvertListIntegerToString(.NbRestrain))

                'Dim listTypeMaintien(.TypeMaintien.Count - 1) As Integer

                'For i As Integer = 0 To listTypeMaintien.Count - 1
                '    listTypeMaintien(i) = .TypeMaintien(i)
                'Next
                Lines.Add("   TypeMaintien   =  " & .TypeMaintien)

                Lines.Add("   D1             =  " & .EntraxeD1)
                Lines.Add("   D2             =  " & .EntraxeD2)
                Lines.Add("   Dsl1           =  " & .DistanceDsl1)
                Lines.Add("   Dsl2           =  " & .DistanceDsl2)
                Lines.Add("   lIntermediaire =  " & .lIntermediaire)
                Lines.Add("   lDefautPortee  =  " & .lDefautPortee)
                Lines.Add("   lDefautEnroba  =  " & .lDefautEnrobage)
                Lines.Add("   lDefautEtaiem  =  " & .lDefautEtaiement)
                Lines.Add("   lDefautDalle   =  " & .lDefautDalle)
                Lines.Add("   lDonneesSauv   =  " & .lDonneesSauvees)
                Lines.Add("   lNouvPoutre    =  " & .NouvellePoutre)

                '==[ Classe Maintien ]=================================================================
                For Each maint In .Maintiens
                    For i As Integer = 0 To maint.Count - 1

                        With maint(i)

                            Lines.Add("BLOCK MAINTIENTS")
                            Lines.Add("   indTravee      =  " & i)
                            Lines.Add("   xloc           =  " & .x_Loc)
                            Lines.Add("   lMaintSemSup   =  " & .lMaintienSemelleSup)
                            Lines.Add("   lMaintSemInf   =  " & .lMaintienSemelleInf)

                        End With

                    Next
                Next

                '==[ Classe Section ]=================================================================
                With .Section
                    Lines.Add("BLOCK SECTION")

                    Lines.Add("   Nom            =  " & .Nom)
                    Lines.Add("   lDalleBeton    =  " & .lDalleBeton)
                    Lines.Add("   lDatabase      =  " & .lDatabase)
                    Lines.Add("   typeSection    =  " & .typeSection)


                    '==[ Classe ProfilA ]=================================================================
                    With .ProfilA
                        Lines.Add("BLOCK PROFILA")

                        Lines.Add("   Gamme          =  " & .Gamme)
                        Lines.Add("   NomProfile     =  " & .NomProfile)
                        Lines.Add("   ha             =  " & .ha)
                        Lines.Add("   hb             =  " & .hb)
                        Lines.Add("   bfs            =  " & .b_fs)
                        Lines.Add("   tfs            =  " & .t_fs)
                        Lines.Add("   bfi            =  " & .b_fi)
                        Lines.Add("   tfi            =  " & .t_fi)
                        Lines.Add("   rcs            =  " & .r_cs)
                        Lines.Add("   rci            =  " & .r_ci)
                        'Lines.Add("   hw             =  " & .h_w)
                        Lines.Add("   tw             =  " & .t_w)
                        Lines.Add("   soudure_a      =  " & .a)
                        Lines.Add("   typeProfil     =  " & .typeProfileAcier)
                        Lines.Add("   Platb          =  " & .Plat_b)
                        Lines.Add("   Platt          =  " & .Plat_t)
                        Lines.Add("   IndDeliv       =  " & ConvertListShortToString(.IndDeliv))
                        Lines.Add("   IndStand       =  " & ConvertListShortToString(.IndStandart))

                    End With

                    '==[ Classe Acier ProfilA ]=================================================================
                    With .Acier
                        Lines.Add("BLOCK ACIER_PROFILA")

                        Lines.Add("   Nuance         =  " & .Nuance)
                        Lines.Add("   Qualite        =  " & .Qualite)
                        Lines.Add("   Reduction      =  " & .Reduction)
                        Lines.Add("   NormeProduit   =  " & .NormeProduit)
                        Lines.Add("   EpMax          =  " & .EpMax)
                        Lines.Add("   iBase          =  " & .iBase)
                        Lines.Add("   iTabStand      =  " & .iTabStandart)
                        Lines.Add("   iStandard      =  " & .iStandart)
                        Lines.Add("   f_y_fs         =  " & .f_y.fs)
                        Lines.Add("   f_y_w          =  " & .f_y.w)
                        Lines.Add("   f_y_fi         =  " & .f_y.fi)
                    End With


                    '==[ Classe Enrobage Partiel ProfilA ]=================================================================
                    With .enrobage_partiel
                        Lines.Add("BLOCK ENROBAGE_PROFILA")

                        Lines.Add("   lArmaConst     =  " & .lArmaConst)
                        Lines.Add("   ConstPhi       =  " & .ConstPhi)
                        Lines.Add("   Ratio_bc       =  " & .Ratio_bc)
                        Lines.Add("   Etrier_Type    =  " & .Etriers_Type)
                        Lines.Add("   Etrier_Phi     =  " & .Etriers_Phi)
                        Lines.Add("   Etrier_CY  =  " & .Etriers_EnrobageY)
                        Lines.Add("   Etrier_CZ  =  " & .Etriers_EnrobageZ)

                        '==[ Classe Armature Enrobage Partiel ProfilA ]=================================================================
                        Lines.Add("BLOCK ARMATURE_ENROBAGE_PROFILA")

                        For i As Integer = 0 To .LitArma.Length - 1
                            With .LitArma(i)
                                Lines.Add("   indLit         =  " & i)
                                Lines.Add("   PhiExt         =  " & .PhiExt)
                                Lines.Add("   NbExt          =  " & .NbExt)
                                Lines.Add("   PhiMil         =  " & .PhiMil)
                                Lines.Add("   NbMil          =  " & .NbMil)
                                Lines.Add("   PhiInt         =  " & .PhiInt)
                                Lines.Add("   NbInt          =  " & .NbInt)
                                If i = 1 Then Lines.Add("   zPosRatio      =  " & .zPosRatio)
                            End With
                        Next

                        '==[ Classe Acier Armature Enrobage Partiel ProfilA ]=================================================================
                        Lines.Add("BLOCK ACIER_ARMATURE_ENROBAGE_PROFILA")
                        With .AcierArmatures
                            Lines.Add("   Classe         =  " & .Classe)
                            Lines.Add("   FsK            =  " & .FsK)
                            Lines.Add("   Es             =  " & .Es)
                        End With

                        '==[ Classe Béton Enrobage Partiel ProfilA ]=================================================================
                        Lines.Add("BLOCK BETON_ENROBAGE_PROFILA")
                        With .Beton
                            Lines.Add("   Type           =  " & .Type)
                            Lines.Add("   Classe         =  " & .Classe)
                            Lines.Add("   Fck            =  " & .Fck)
                            Lines.Add("   Fcm            =  " & .Fcm)
                            Lines.Add("   Fctm           =  " & .Fctm)
                            Lines.Add("   Ecm            =  " & .Ecm)
                            Lines.Add("   lCrackLimit    =  " & .lCrackingLimitation)
                            Lines.Add("   wk_max         =  " & .wk_max)
                        End With
                    End With
                End With

                '==[ Classe Dalle ]=================================================================
                With .Dalle
                    Lines.Add("BLOCK DALLE")

                    Lines.Add("   Type           =  " & .type)
                    Lines.Add("   td             =  " & .t_d)
                    Lines.Add("   th             =  " & .t_h)
                    Lines.Add("   Beff           =  " & .Beff)
                    Lines.Add("   lArmInf        =  " & .lArma_Inf)
                    Lines.Add("   lArmSup        =  " & .lArma_Sup)
                    Lines.Add("   preDalle_ep    =  " & .preDalle_ep)
                    Lines.Add("   preDalle_tjoi  =  " & .preDalle_tjoint)
                    'Lines.Add("   theta_h        =  " & .pTheta_h)

                    '==[ Classe Béton Dalle ]=================================================================
                    With .beton
                        Lines.Add("BLOCK BETON_DALLE")

                        Lines.Add("   Type           =  " & .Type)
                        Lines.Add("   Classe         =  " & .Classe)
                        Lines.Add("   Fck            =  " & .Fck)
                        Lines.Add("   Fcm            =  " & .Fck)
                        Lines.Add("   Fctm           =  " & .Fctm)
                        Lines.Add("   Ecm            =  " & .Ecm)
                        Lines.Add("   lCrackLimit    =  " & .lCrackingLimitation)
                        Lines.Add("   wk_max         =  " & .wk_max)
                    End With

                    '==[ Classe Bac Dalle ]=================================================================
                    With .Bac
                        Lines.Add("BLOCK BAC_DALLE")

                        Lines.Add("   Etiquette      =  " & .Etiquette)
                        Lines.Add("   Producteur     =  " & .Producteur)
                        Lines.Add("   lDatabase      =  " & .lDatabase)
                        Lines.Add("   h_rs           =  " & .h_rs)
                        Lines.Add("   h_p            =  " & .h_p)
                        Lines.Add("   b_b            =  " & .b_b)
                        Lines.Add("   b_t            =  " & .b_t)
                        Lines.Add("   e_p            =  " & .e_p)
                        Lines.Add("   tp             =  " & .tp)
                        Lines.Add("   orientation    =  " & .orientation)
                        Lines.Add("   msurf          =  " & .msurf)
                        Lines.Add("   fyp            =  " & .fyp)
                        Lines.Add("   LargeurModule  =  " & .LargeurModule)
                        Lines.Add("   Ieff           =  " & .Ieff)
                        Lines.Add("   lPreperce      =  " & .lPreperce)
                        Lines.Add("   AppuiT         =  " & .AppuiT)
                        Lines.Add("   AppuiL         =  " & .AppuiL)
                    End With

                    '==[ Classe Armature Dalle ]=================================================================
                    For Each arma_longi As Cls_Armatures_Longi In .LitArma
                        With arma_longi
                            Lines.Add("BLOCK ARMATURE_DALLE")

                            Lines.Add("   EspBar         =  " & .EspBar)
                            Lines.Add("   PhiS           =  " & .PhiS)
                            Lines.Add("   z_s            =  " & .z_s)
                            Lines.Add("   n_s            =  " & .n_s)
                            Lines.Add("   c_s            =  " & .c_s)
                            Lines.Add("   lActive        =  " & .lActive)
                        End With
                    Next

                    '==[ Classe Acier Armature Dalle ]=================================================================
                    With .AcierArmatures
                        Lines.Add("BLOCK ACIER_ARMATURE_DALLE")

                        Lines.Add("   Classe         =  " & .Classe)
                        Lines.Add("   Fsk            =  " & .FsK)
                        Lines.Add("   Es             =  " & .Es)
                    End With


                    '==[ Classe Connecteur Dalle ]=================================================================
                    With .Connecteur
                        Lines.Add("BLOCK CONNECTEUR_DALLE")

                        Lines.Add("   hsc            =  " & .hsc)
                        Lines.Add("   d              =  " & .d)
                        Lines.Add("   fu              =  " & .Fu)
                    End With
                End With

                '==[ Classe Options Calculs ]=================================================================
                With .Param

                    Lines.Add("BLOCK OPT_CALCULS")

                    Lines.Add("   EArma         = " & .ArmaYoung)
                    Lines.Add("   lArmaComp     = " & .lArmaComprimee)
                    Lines.Add("   lRenformis    = " & .lRenformis)
                    Lines.Add("   Eta          = " & .Eta)
                    Lines.Add("   lInterMV      = " & .lInterActionMV)
                    Lines.Add("   VEd           = " & .VEd)
                    Lines.Add("   lPosFlexion   = " & .lCalcul_Flexion_Positive)
                    Lines.Add("   lNegFlexion   = " & .lCalcul_Flexion_Negative)

                    Lines.Add("   lPermCharges  = " & .lChargesPermanentes)
                    Lines.Add("   lRetrait      = " & .lChargesRetrait)
                    Lines.Add("   lExplCharges  = " & .lChargesExploitation)
                    Lines.Add("   lChargesCust  = " & .lChargesCustom)
                    Lines.Add("   NeqCustom     = " & .NeqCustom)
                    Lines.Add("   RH            = " & .RH)

                    Lines.Add("   t0Permanent   =  " & ConvertListToString(.t0Permanentes))

                    '==[ Classe Prop Elastique Enrobage ]=================================================================
                    With .Prop_Elastique_Enrobage
                        Lines.Add("BLOCK OPT_CALCULS_PROP_ELAST_ENROBAGE")

                        Lines.Add("   PEEn_L        = " & .CE_n_L)
                        Lines.Add("   PERH          = " & .RH)
                        Lines.Add("   PEType        = " & .type_def_t)
                        Lines.Add("   PEt           = " & .t)
                        Lines.Add("   PEH0          = " & .h_0)
                        Lines.Add("   PERt0         = " & .R_t_0)
                        Lines.Add("   PERn_L        = " & .R_n_L)
                        Lines.Add("   PEPt0         = " & .CP_t_0)
                        Lines.Add("   PEPn_L        = " & .CP_n_L)
                    End With

                    '==[ Classe Prop Elastique Dalle ]=================================================================
                    With .Prop_Elastique_Dalle
                        Lines.Add("BLOCK OPT_CALCULS_PROP_ELAST_DALLE")

                        Lines.Add("   PDEn_L        = " & .CE_n_L)
                        Lines.Add("   PDRH          = " & .RH)
                        Lines.Add("   PDType        = " & .type_def_t)
                        Lines.Add("   PDt           = " & .t)
                        Lines.Add("   PDH0          = " & .h_0)
                        Lines.Add("   PDRt0         = " & .R_t_0)
                        Lines.Add("   PDRn_L        = " & .R_n_L)
                        Lines.Add("   PDPt0         = " & .CP_t_0)
                        Lines.Add("   PDPn_L        = " & .CP_n_L)
                    End With

                    '==[ Classe Gamma ]=================================================================
                    With .Gamma
                        Lines.Add("BLOCK OPT_CALCULS_GAMMA")

                        Lines.Add("   GammaM0       = " & .GammaM0)
                        Lines.Add("   GammaM1       = " & .GammaM1)
                        Lines.Add("   GammaM2       = " & .GammaM2)
                        Lines.Add("   GammaC        = " & .GammaC)
                        Lines.Add("   GammaVs       = " & .GammaVs)
                        Lines.Add("   GammaVc       = " & .GammaVc)
                        Lines.Add("   lGammaVuni    = " & .lGammaV_unique)
                        Lines.Add("   GammaS        = " & .GammaS)
                        Lines.Add("   GammaP        = " & .GammaP)
                        Lines.Add("   GammaM_fi     = " & .GammaM_fi)
                        Lines.Add("   GammaC_fi     = " & .GammaC_fi)
                        Lines.Add("   GammaV_fi     = " & .GammaV_fi)
                        Lines.Add("   GammaG_sup    = " & .GammaG_sup)
                        Lines.Add("   GammaG_inf    = " & .GammaG_inf)
                        Lines.Add("   GammaQ        = " & .GammaQ)
                        Lines.Add("   Psi0_Q1       = " & .Psi0_Q1)
                        Lines.Add("   Psi1_Q1       = " & .Psi1_Q1)
                        Lines.Add("   Psi2_Q1       = " & .Psi2_Q1)
                        Lines.Add("   Psi0_Q2       = " & .Psi0_Q2)
                        Lines.Add("   Psi1_Q2       = " & .Psi1_Q2)
                        Lines.Add("   Psi2_Q2       = " & .Psi2_Q2)
                    End With

                    '==[ Classe Hivoss ]=================================================================
                    With .HivossParam
                        Lines.Add("BLOCK OPT_CALCULS_HIVOSS")

                        Lines.Add("   lHivossMethod = " & .lHivossMethod)
                        Lines.Add("   RatioQ        = " & .ratioQ)
                        Lines.Add("   ChoixQ        = " & .choixQ)
                        Lines.Add("   UtilPlancher  = " & .UtilisationPlancher)
                        Lines.Add("   Mobilier      = " & .Mobilier)
                        Lines.Add("   lFauxPlafond  = " & .lFauxPlafond)
                        Lines.Add("   AmortD1       = " & .AmortiStructure_D1)
                        Lines.Add("   AmortD2       = " & .AmortiMobilier_D2)
                        Lines.Add("   AmortD3       = " & .AmortiFinition_D3)
                        Lines.Add("   AmortDtot     = " & .AmortiTotal_Dtot)
                    End With
                End With
            End With
        Next





    End Sub

    Public Sub RecuperationFile(ByVal FileName As String, ByVal str_warning_file As String,
                                ByRef nuances As List(Of String), ByRef f_y As List(Of Integer))

        '--> Déclaration
        Dim Lines As Cls_LinesOfFile
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Dim ListeBlocIndex As New List(Of Integer)
        Dim ListeBlocCle As New List(Of String)
        Dim IndexFin As Integer

        '--> Initialisation
        If File.Exists(FileName) Then
            Lines = New Cls_LinesOfFile(FileName)
            Me.lSave = True
            Me.FileName = FileName
        Else
            MsgBox("Fichier n'existe pas | File not exist : " & FileName, MsgBoxStyle.Critical, "Cls_Projet/RecuperationFile")
            Exit Sub
        End If

        '--> Repère des mots clés BLOCK
        Try
            For i = 0 To Lines.LineNumber - 1

                DecomposeLine(Lines.Lines(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                    If MotCle = "BLOC" Then
                        ListeBlocIndex.Add(i)
                        ListeBlocCle.Add(Mots(2).ToUpper)
                    End If

                End If
            Next
        Catch ex As Exception
            MsgBox("Erreur lecture fichier | Error read file", MsgBoxStyle.Critical, "Cls_Project/RecuperationFile")
        End Try

        If Not ListeBlocCle.Contains("IDENTIFICATION") Or Not ListeBlocCle.Contains("POUTRE") Or Not ListeBlocCle.Contains("MAINTIENTS") Or Not ListeBlocCle.Contains("SECTION") Or
           Not ListeBlocCle.Contains("PROFILA") Or Not ListeBlocCle.Contains("ACIER_PROFILA") Or Not ListeBlocCle.Contains("ENROBAGE_PROFILA") Or Not ListeBlocCle.Contains("ARMATURE_ENROBAGE_PROFILA") Or Not ListeBlocCle.Contains("ACIER_ARMATURE_ENROBAGE_PROFILA") Or Not ListeBlocCle.Contains("BETON_ENROBAGE_PROFILA") Or
           Not ListeBlocCle.Contains("DALLE") Or Not ListeBlocCle.Contains("BETON_DALLE") Or Not ListeBlocCle.Contains("BAC_DALLE") Or Not ListeBlocCle.Contains("ARMATURE_DALLE") Or Not ListeBlocCle.Contains("ACIER_ARMATURE_DALLE") Or Not ListeBlocCle.Contains("CONNECTEUR_DALLE") Or
           Not ListeBlocCle.Contains("OPT_CALCULS") Or Not ListeBlocCle.Contains("OPT_CALCULS_PROP_ELAST_ENROBAGE") Or Not ListeBlocCle.Contains("OPT_CALCULS_PROP_ELAST_DALLE") Or Not ListeBlocCle.Contains("OPT_CALCULS_GAMMA") Or Not ListeBlocCle.Contains("OPT_CALCULS_HIVOSS") Then

            MsgBox("Fichier corrumpu | Corrupted file", MsgBoxStyle.Critical, "Cls_Projet/LectureFile")

        End If

        Dim oldFichier As Boolean = False

        '--> Traitement des blocks

        For i = 0 To ListeBlocIndex.Count - 1
            If i = ListeBlocIndex.Count - 1 Then IndexFin = Lines.Lines.Count - 1 Else IndexFin = ListeBlocIndex(i + 1)
            Select Case ListeBlocCle(i)

                Case "POUTRE"
                    Dim ptre_en_cours As New cls_Poutre
                    ReadBlocPoutre(ptre_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    Me.Poutres.Add(ptre_en_cours)

                Case "MAINTIENS"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim maintien_en_cours As New cls_Maintiens
                    Dim ind_travee As Integer
                    ReadBlocMaintiens(maintien_en_cours, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Maintiens(ind_travee).Add(maintien_en_cours)

                Case "SECTION"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim section_en_cours As New cls_Section
                    ReadBlocSection(section_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section = section_en_cours

                Case "PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim profilA_en_cours As New cls_ProfilA
                    ReadBlocProfilA(profilA_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.ProfilA = profilA_en_cours

                Case "ACIER_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_profilA As Cls_Acier
                    ReadBlocAcierProfilA(acier_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Acier = acier_profilA

                Case "ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim enrobage_profilA As Cls_Enrobage_Partiel
                    ReadBlocEnrobageProfilA(enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.enrobage_partiel = enrobage_profilA

                Case "ARMATURE_ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_enrobage_profilA(2) As Cls_ArmatureEnrobage
                    ReadBlocArmatureEnrobageProfilA(armature_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.enrobage_partiel.LitArma = armature_enrobage_profilA

                Case "ACIER_ARMATURE_ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_enrobage_profilA As Cls_AcierArmature
                    ReadBlocAcierArmatureEnrobageProfilA(acier_armature_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.enrobage_partiel.AcierArmatures = acier_armature_enrobage_profilA

                Case "BETON_ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim beton_enrobage_profilA As Cls_Beton
                    ReadBlocBetonEnrobageProfilA(beton_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.enrobage_partiel.Beton = beton_enrobage_profilA

                Case "DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim dalle_en_cours As Cls_Dalle
                    ReadBlocDalle(dalle_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle = dalle_en_cours

                Case "BETON_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim beton_dalle As Cls_Beton
                    ReadBlocBetonDalle(beton_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.beton = beton_dalle

                Case "BAC_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim bac_en_cours As Cls_Bac
                    ReadBlocBacDalle(bac_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Bac = bac_en_cours

                Case "ARMATURE_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_dalle As Cls_Armatures_Longi
                    ReadBlocArmatureDalle(armature_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.LitArma.Add(armature_dalle)

                Case "ACIER_ARMATURE_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_dalle As Cls_AcierArmature
                    ReadBlocAcierArmatureDalle(acier_armature_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.AcierArmatures = acier_armature_dalle

                Case "CONNECTEUR_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim connecteur_dalle As Cls_Connecteur
                    ReadBlocConnecteurDalle(connecteur_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Connecteur = connecteur_dalle

                Case "OPT_CALCULS"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim opt_calculs_en_cours As Cls_OptionsCalcul
                    ReadBlocOptionsCalculs(opt_calculs_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param = opt_calculs_en_cours


                Case "OPT_CALCULS_PROP_ELAST_ENROBAGE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim prop_elast_enrob_opt_calculs As Cls_Prop_Elastique
                    ReadBlocPropElastEnrobageOptionsCalculs(prop_elast_enrob_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param.Prop_Elastique_Enrobage = prop_elast_enrob_opt_calculs

                Case "OPT_CALCULS_PROP_ELAST_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim prop_elast_dalle_opt_calculs As Cls_Prop_Elastique
                    ReadBlocPropElastDalleOptionsCalculs(prop_elast_dalle_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param.Prop_Elastique_Dalle = prop_elast_dalle_opt_calculs

                Case "OPT_CALCULS_GAMMA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim gamma_opt_calculs As Cls_Gamma
                    ReadBlocGammaOptionsCalculs(gamma_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param.Gamma = gamma_opt_calculs

                Case "OPT_CALCULS_HIVOSS"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim hivoss_opt_calculs As cls_HivossParam
                    ReadBlocHivossOptionsCalculs(hivoss_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param.HivossParam = hivoss_opt_calculs


                    'Case "IDENTIFICATION"
                    '    Me.ReadBloc_Indentification(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)

                    'Case "NUANCEACIER"
                    '    Me.ReadBloc_SteelGrade(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin, nuances, f_y)

                    'Case "SECTION"

                    '    Dim s As New cls_Section
                    '    s.LectureFile(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    '    'List_Section.Add(s)

            End Select

        Next

        If oldFichier Then
            MsgBox(str_warning_file, MsgBoxStyle.Critical, "PropMix")
        End If

    End Sub

    Private Sub ReadBloc_Indentification(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "UTIL"
                        Me.Utilisateur = ""
                        For z = 2 To nbMots
                            If z = nbMots Then
                                Me.Utilisateur += Mots(z)
                            Else
                                Me.Utilisateur += Mots(z) + " "
                            End If
                        Next
                    Case "ENTR"
                        Me.Entreprise = ""
                        For z = 2 To nbMots
                            If z = nbMots Then
                                Me.Entreprise += Mots(z)
                            Else
                                Me.Entreprise += Mots(z) + " "
                            End If
                        Next
                    Case "NOM"
                        Me.Nom = ""
                        For z = 2 To nbMots
                            If z = nbMots Then
                                Me.Nom += Mots(z)
                            Else
                                Me.Nom += Mots(z) + " "
                            End If
                        Next
                End Select
            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Poutre
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocPoutre(poutre_en_cours As cls_Poutre, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With poutre_en_cours
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "LABEL" : .Label = Mots(nbMots)
                        Case "TYPESECTIO" : .TypeSection = Mots(nbMots)
                        Case "CONSOLEGAU" : .lTraveeConsoleGauche = Mots(nbMots)
                        Case "CONSOLEDRO" : .lTraveeConsoleGauche = Mots(nbMots)
                        Case "LTREMIEGAU" : .lTremieGauche = Mots(nbMots)
                        Case "LTREMIEDRO" : .lTremieDroite = Mots(nbMots)
                        Case "NBTRAVEE" : .NombreTraveesDeuxAppuis = TraiteReal(Mots(nbMots))
                        Case "LONGUEURTR" : .LongueurTravee = ConvertStringToList(Mots(nbMots))
                        Case "TYPETRAVEE" : .TypTravee = ConvertStringToListInteger(Mots(nbMots))
                        Case "TYPEETAIEM" : .TypeEtaiement = TraiteReal(Mots(nbMots))
                        Case "ETAISCONSG" : .lEtaisConsoleGauche = Mots(nbMots)
                        Case "ETAISCONSD" : .lEtaisConsoleDroite = Mots(nbMots)
                        Case "NBPROPPIN" : .NbPropping = Mots(nbMots)
                        Case "NBRESTRAIN" : .NbRestrain = ConvertStringToListInteger(Mots(nbMots))
                        Case "TYPEMAINTI" : .TypeMaintien = Mots(nbMots)
                        Case "D1" : .EntraxeD1 = TraiteReal(Mots(nbMots))
                        Case "D2" : .EntraxeD2 = TraiteReal(Mots(nbMots))
                        Case "DSL1" : .DistanceDsl1 = TraiteReal(Mots(nbMots))
                        Case "DSL2" : .DistanceDsl2 = TraiteReal(Mots(nbMots))
                        Case "LINTERMEDI" : .lIntermediaire = Mots(nbMots)
                        Case "LDEFAUTPOR" : .lDefautPortee = Mots(nbMots)
                        Case "LDEFAUTENR" : .lDefautEnrobage = Mots(nbMots)
                        Case "LDEFAUTETA" : .lDefautEtaiement = Mots(nbMots)
                        Case "LDEFAUTDAL" : .lDefautDalle = Mots(nbMots)
                        Case "LDONNEESSA" : .lDonneesSauvees = Mots(nbMots)
                        Case "LNOUVPOUTR" : .NouvellePoutre = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' Lecture du bloc Maintien
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocMaintiens(maintien_en_cours As cls_Maintiens, ByRef ind_travee As Integer, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With maintien_en_cours
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "INDTRAVEE" : ind_travee = TraiteReal(Mots(nbMots))
                        Case "XLOC" : .x_Loc = TraiteReal(Mots(nbMots))
                        Case "LMAINTSEMS" : .lMaintienSemelleSup = Mots(nbMots)
                        Case "LMAINTSEMI" : .lMaintienSemelleInf = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' Lecture du bloc Section
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocSection(section As cls_Section, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With section
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "NOM" : .Nom = Mots(nbMots)
                        Case "LDALLEBETO" : .lDalleBeton = Mots(nbMots)
                        Case "LDATABASE" : .lDatabase = Mots(nbMots)
                        Case "TYPESECTIO" : .typeSection = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' Lecture du bloc ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocProfilA(profilA As cls_ProfilA, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With profilA
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "GAMME" : .Gamme = Mots(nbMots)
                        Case "NOMPROFILE" : .NomProfile = Mots(nbMots)
                        Case "HA" : .ha = TraiteReal(Mots(nbMots))
                        Case "HB" : .hb = TraiteReal(Mots(nbMots))
                        Case "BFS" : .b_fs = TraiteReal(Mots(nbMots))
                        Case "BTS" : .t_fs = TraiteReal(Mots(nbMots))
                        Case "BFI" : .b_fi = TraiteReal(Mots(nbMots))
                        Case "TFI" : .t_fi = TraiteReal(Mots(nbMots))
                        Case "RCS" : .r_cs = TraiteReal(Mots(nbMots))
                        Case "RCI" : .r_ci = TraiteReal(Mots(nbMots))
                        'Case "HW" : .h_w = TraiteReal(Mots(nbMots))
                        Case "TW" : .t_w = TraiteReal(Mots(nbMots))
                        Case "SOUDUREA" : .a = TraiteReal(Mots(nbMots))
                        Case "TYPEPROFIL" : .typeProfileAcier = Mots(nbMots)
                        Case "PLATB" : .Plat_b = TraiteReal(Mots(nbMots))
                        Case "PLATT" : .Plat_t = TraiteReal(Mots(nbMots))
                        Case "INDDELIV" : .IndDeliv = ConvertStringToListShort(Mots(nbMots))
                        Case "INDSTAND" : .IndStandart = ConvertStringToListShort(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' Lecture du bloc Acier_ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocAcierProfilA(acier_profilA As Cls_Acier, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With acier_profilA
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "NUANCE" : .Nuance = Mots(nbMots)
                        Case "QUALITE" : .Qualite = Mots(nbMots)
                        Case "REDUCTION" : .Reduction = Mots(nbMots)
                        Case "NORMEPRODU" : .NormeProduit = Mots(nbMots)
                        Case "EPMAX" : .EpMax = TraiteReal(Mots(nbMots))
                        Case "IBASE" : .iBase = TraiteReal(Mots(nbMots))
                        Case "ITABSTAND" : .iTabStandart = TraiteReal(Mots(nbMots))
                        Case "ISTANDARD" : .iStandart = TraiteReal(Mots(nbMots))
                        Case "F_Y_FS" : .f_y.fs = TraiteReal(Mots(nbMots))
                        Case "F_Y_W" : .f_y.w = TraiteReal(Mots(nbMots))
                        Case "F_Y_FI" : .f_y.fi = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub


    ''' <summary>
    ''' Lecture du bloc Enrobage_ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocEnrobageProfilA(enrobage_profilA As Cls_Enrobage_Partiel, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With enrobage_profilA
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "LARMACONST" : .lArmaConst = Mots(nbMots)
                        Case "CONSTPHI" : .ConstPhi = TraiteReal(Mots(nbMots))
                        Case "RATIOBC" : .Ratio_bc = TraiteReal(Mots(nbMots))
                        Case "ETRIER_TYP" : .Etriers_Type = Mots(nbMots)
                        Case "ETRIER_PHI" : .Etriers_Phi = TraiteReal(Mots(nbMots))
                        Case "ETRIER_CY" : .Etriers_EnrobageY = TraiteReal(Mots(nbMots))
                        Case "ETRIER_CZ" : .Etriers_EnrobageZ = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' Lecture du bloc Enrobage_ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBloc_SteelGrade(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer,
                                    ByRef nuances As List(Of String), ByRef f_y As List(Of Integer))
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        Dim User_Nuance As String = ""
        Dim User_Fy As Integer

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "NUAN"
                        For z = 2 To nbMots
                            If z = nbMots Then
                                User_Nuance += Mots(z)
                            Else
                                User_Nuance += Mots(z) + " "
                            End If
                        Next
                    Case "FY" : User_Fy = Mots(nbMots)

                End Select
            End If
        Next

        '--> Ajout de la nuance si elle n'existe pas
        If Not nuances.Contains(User_Nuance) Then
            nuances.Add(User_Nuance)
            f_y.Add(User_Fy)
        End If

    End Sub

    ''' <summary>
    ''' Lecture du bloc Armature_Enrobage_ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocArmatureEnrobageProfilA(armature_enrobage_profilA() As Cls_ArmatureEnrobage, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        Dim ind_lit As Integer 'variable locale

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                If MotCle = "INDLIT" Then
                    ind_lit = TraiteReal(Mots(nbMots))
                Else
                    With armature_enrobage_profilA(ind_lit)
                        Select Case MotCle
                            Case "PHIEXT" : .PhiExt = TraiteReal(Mots(nbMots))
                            Case "NBEXT" : .NbExt = TraiteReal(Mots(nbMots))
                            Case "PHIMIL" : .PhiMil = TraiteReal(Mots(nbMots))
                            Case "NBMIL" : .NbMil = TraiteReal(Mots(nbMots))
                            Case "PHIINT" : .PhiInt = TraiteReal(Mots(nbMots))
                            Case "NBINT" : .NbInt = TraiteReal(Mots(nbMots))
                            Case "ZPOSRATIO" : .zPosRatio = TraiteReal(Mots(nbMots))
                            Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                        End Select
                    End With
                End If
            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Acier_Armature_Enrobage_ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocAcierArmatureEnrobageProfilA(acier_armature_enrobage_profilA As Cls_AcierArmature, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                With acier_armature_enrobage_profilA
                    Select Case MotCle
                        Case "CLASSE" : .Classe = Mots(nbMots)
                        Case "FSK" : .FsK = TraiteReal(Mots(nbMots))
                        Case "ES" : .Es = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub


    ''' <summary>
    ''' Lecture du bloc Beton_Enrobage_ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocBetonEnrobageProfilA(beton_enrobage_profilA As Cls_Beton, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With beton_enrobage_profilA
                    Select Case MotCle
                        Case "TYPE" : .Type = Mots(nbMots)
                        Case "CLASSE" : .Classe = Mots(nbMots)
                        Case "FCK" : .Fck = TraiteReal(Mots(nbMots))
                        Case "FCM" : .Fcm = TraiteReal(Mots(nbMots))
                        Case "FCTM" : .Fctm = TraiteReal(Mots(nbMots))
                        Case "ECM" : .Ecm = TraiteReal(Mots(nbMots))
                        Case "LCRACKLIMI" : .lCrackingLimitation = Mots(nbMots)
                        Case "WK_MAX" : .wk_max = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Dalle
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocDalle(dalle_en_cours As Cls_Dalle, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With dalle_en_cours
                    Select Case MotCle
                        Case "TYPE" : .type = Mots(nbMots)
                        Case "TD" : .t_d = TraiteReal(Mots(nbMots))
                        Case "TH" : .t_h = TraiteReal(Mots(nbMots))
                        Case "BEFF" : .Beff = TraiteReal(Mots(nbMots))
                        Case "LARMINF" : .lArma_Inf = Mots(nbMots)
                        Case "LARMSUP" : .lArma_Sup = Mots(nbMots)
                        Case "PREDALLE_E" : .preDalle_ep = TraiteReal(Mots(nbMots))
                        Case "PREDALLE_T" : .preDalle_tjoint = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next
    End Sub

    ''' <summary>
    ''' Lecture du bloc Beton_Dalle
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocBetonDalle(beton_dalle As Cls_Beton, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With beton_dalle
                    Select Case MotCle
                        Case "TYPE" : .Type = Mots(nbMots)
                        Case "CLASSE" : .Classe = Mots(nbMots)
                        Case "FCK" : .Fck = TraiteReal(Mots(nbMots))
                        Case "FCM" : .Fcm = TraiteReal(Mots(nbMots))
                        Case "FCTM" : .Fctm = TraiteReal(Mots(nbMots))
                        Case "ECM" : .Ecm = TraiteReal(Mots(nbMots))
                        Case "LCRACKLIMI" : .lCrackingLimitation = Mots(nbMots)
                        Case "WK_MAX" : .wk_max = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next
    End Sub

    ''' <summary>
    ''' Lecture du bloc Bac_Dalle
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocBacDalle(bac_dalle As Cls_Bac, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With bac_dalle
                    Select Case MotCle
                        Case "ETIQUETTE" : .Etiquette = Mots(nbMots)
                        Case "PRODUCTEUR" : .Producteur = Mots(nbMots)
                        Case "LDATABASE" : .lDatabase = Mots(nbMots)
                        Case "H_RS" : .h_rs = TraiteReal(Mots(nbMots))
                        Case "H_P" : .h_p = TraiteReal(Mots(nbMots))
                        Case "B_B" : .b_b = TraiteReal(Mots(nbMots))
                        Case "B_T" : .b_t = TraiteReal(Mots(nbMots))
                        Case "E_P" : .e_p = TraiteReal(Mots(nbMots))
                        Case "TP" : .tp = TraiteReal(Mots(nbMots))
                        Case "ORIENTATIO" : .orientation = Mots(nbMots)
                        Case "MSURF" : .msurf = TraiteReal(Mots(nbMots))
                        Case "FYP" : .fyp = TraiteReal(Mots(nbMots))
                        Case "LARGEURMOD" : .LargeurModule = TraiteReal(Mots(nbMots))
                        Case "IEFF" : .Ieff = TraiteReal(Mots(nbMots))
                        Case "LPREPERCE" : .lPreperce = Mots(nbMots)
                        Case "APPUIT" : .AppuiT = Mots(nbMots)
                        Case "APPUIL" : .AppuiL = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Armature_Dalle
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocArmatureDalle(armature_dalle As Cls_Armatures_Longi, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With armature_dalle
                    Select Case MotCle
                        Case "ESPBAR" : .EspBar = TraiteReal(Mots(nbMots))
                        Case "PHIS" : .PhiS = TraiteReal(Mots(nbMots))
                        Case "Z_S" : .z_s = TraiteReal(Mots(nbMots))
                        Case "N_S" : .n_s = TraiteReal(Mots(nbMots))
                        Case "c_S" : .c_s = TraiteReal(Mots(nbMots))
                        Case "LACTIVE" : .lActive = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Acier_Armature_Dalle
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocAcierArmatureDalle(acier_armature_dalle As Cls_AcierArmature, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With acier_armature_dalle
                    Select Case MotCle
                        Case "CLASSE" : .Classe = Mots(nbMots)
                        Case "FSK" : .FsK = TraiteReal(Mots(nbMots))
                        Case "ES" : .Es = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub


    ''' <summary>
    ''' Lecture du bloc Connecteur_Dalle
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocConnecteurDalle(connecteur_dalle As Cls_Connecteur, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With connecteur_dalle
                    Select Case MotCle
                        Case "HSC" : .hsc = TraiteReal(Mots(nbMots))
                        Case "D" : .d = TraiteReal(Mots(nbMots))
                        Case "FU" : .Fu = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Opt_Calculs
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocOptionsCalculs(opt_calculs_en_cours As Cls_OptionsCalcul, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With opt_calculs_en_cours
                    Select Case MotCle
                        Case "EARMA" : .ArmaYoung = TraiteReal(Mots(nbMots))
                        Case "LARMACOMPR" : .lArmaComprimee = Mots(nbMots)
                        Case "LRENFORMIS" : .lRenformis = Mots(nbMots)
                        Case "ETA" : .Eta = TraiteReal(Mots(nbMots))
                        Case "LINTERMV" : .lInterActionMV = Mots(nbMots)
                        Case "VED" : .VEd = TraiteReal(Mots(nbMots))
                        Case "LPOSFLEXIO" : .lCalcul_Flexion_Positive = Mots(nbMots)
                        Case "LNEGFLEXIO" : .lCalcul_Flexion_Negative = Mots(nbMots)
                        Case "LPERMCHARG" : .lChargesPermanentes = Mots(nbMots)
                        Case "LRETRAIT" : .lChargesRetrait = Mots(nbMots)
                        Case "LEXPLCHARG" : .lChargesRetrait = Mots(nbMots)
                        Case "LCHARGESCU" : .lChargesCustom = Mots(nbMots)
                        Case "NEQCUSTOM" : .NeqCustom = TraiteReal(Mots(nbMots))
                        Case "RH" : .RH = TraiteReal(Mots(nbMots))
                        Case "T0PERMANEN" : .t0Permanentes = ConvertStringToList(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next


    End Sub

    ''' <summary>
    ''' Lecture du bloc Prop_Elast_Enrobage_Opt_Calculs
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocPropElastEnrobageOptionsCalculs(prop_elast_enrobage_opt_calculs As Cls_Prop_Elastique, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With prop_elast_enrobage_opt_calculs
                    Select Case MotCle
                        Case "PEEN_L" : .CE_n_L = TraiteReal(Mots(nbMots))
                        Case "PERH" : .RH = TraiteReal(Mots(nbMots))
                        Case "PETYPE" : .type_def_t = Mots(nbMots)
                        Case "PET" : .t = TraiteReal(Mots(nbMots))
                        Case "PEH0" : .h_0 = TraiteReal(Mots(nbMots))
                        Case "PERT0" : .R_t_0 = TraiteReal(Mots(nbMots))
                        Case "PERN_L" : .R_n_L = TraiteReal(Mots(nbMots))
                        Case "PEPT0" : .CP_t_0 = TraiteReal(Mots(nbMots))
                        Case "PEPN_L" : .CP_n_L = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Prop_Elast_Dalle_Opt_Calculs
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocPropElastDalleOptionsCalculs(prop_elast_dalle_opt_calculs As Cls_Prop_Elastique, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With prop_elast_dalle_opt_calculs
                    Select Case MotCle
                        Case "PDEN_L" : .CE_n_L = TraiteReal(Mots(nbMots))
                        Case "PDRH" : .RH = TraiteReal(Mots(nbMots))
                        Case "PDTYPE" : .type_def_t = Mots(nbMots)
                        Case "PDT" : .t = TraiteReal(Mots(nbMots))
                        Case "PDH0" : .h_0 = TraiteReal(Mots(nbMots))
                        Case "PDRT0" : .R_t_0 = TraiteReal(Mots(nbMots))
                        Case "PDRN_L" : .R_n_L = TraiteReal(Mots(nbMots))
                        Case "PDPT0" : .CP_t_0 = TraiteReal(Mots(nbMots))
                        Case "PDPN_L" : .CP_n_L = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Opt_Calculs_Gamma
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocGammaOptionsCalculs(gamma_opt_calculs As Cls_Gamma, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With gamma_opt_calculs
                    Select Case MotCle
                        Case "GAMMAM0" : .GammaM0 = TraiteReal(Mots(nbMots))
                        Case "GAMMAM1" : .GammaM1 = TraiteReal(Mots(nbMots))
                        Case "GAMMAM2" : .GammaM2 = TraiteReal(Mots(nbMots))
                        Case "GAMMAC" : .GammaC = TraiteReal(Mots(nbMots))
                        Case "GAMMAVS" : .GammaVs = TraiteReal(Mots(nbMots))
                        Case "GAMMAVC" : .GammaVc = TraiteReal(Mots(nbMots))
                        Case "LGAMMAVUNI" : .lGammaV_unique = Mots(nbMots)
                        Case "GAMMAS" : .GammaS = TraiteReal(Mots(nbMots))
                        Case "GAMMAP" : .GammaP = TraiteReal(Mots(nbMots))
                        Case "GAMMAM_FI" : .GammaM_fi = TraiteReal(Mots(nbMots))
                        Case "GAMMAC_FI" : .GammaC_fi = TraiteReal(Mots(nbMots))
                        Case "GAMMAV_FI" : .GammaV_fi = TraiteReal(Mots(nbMots))
                        Case "GAMMAG_SUP" : .GammaG_sup = TraiteReal(Mots(nbMots))
                        Case "GAMMAG_INF" : .GammaG_inf = TraiteReal(Mots(nbMots))
                        Case "GAMMAQ" : .GammaQ = TraiteReal(Mots(nbMots))
                        Case "PSI0_Q1" : .Psi0_Q1 = TraiteReal(Mots(nbMots))
                        Case "PSI1_Q1" : .Psi1_Q1 = TraiteReal(Mots(nbMots))
                        Case "PSI2_Q1" : .Psi2_Q1 = TraiteReal(Mots(nbMots))
                        Case "PSI0_Q2" : .Psi0_Q2 = TraiteReal(Mots(nbMots))
                        Case "PSI1_Q2" : .Psi1_Q2 = TraiteReal(Mots(nbMots))
                        Case "PSI2_Q2" : .Psi2_Q2 = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Opt_Calculs_Gamma
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocHivossOptionsCalculs(hivoss_opt_calculs As cls_HivossParam, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With hivoss_opt_calculs
                    Select Case MotCle
                        Case "LHIVOSSMET" : .lHivossMethod = Mots(nbMots)
                        Case "RATIOQ" : .ratioQ = TraiteReal(Mots(nbMots))
                        Case "CHOIXQ" : .choixQ = Mots(nbMots)
                        Case "UTILISATIO" : .UtilisationPlancher = Mots(nbMots)
                        Case "MOBILIER" : .Mobilier = Mots(nbMots)
                        Case "LFAUXPLAFO" : .lFauxPlafond = Mots(nbMots)
                        Case "AMORTD1" : .AmortiStructure_D1 = Mots(nbMots)
                        Case "AMORTD2" : .AmortiMobilier_D2 = Mots(nbMots)
                        Case "AMORTD3" : .AmortiFinition_D3 = Mots(nbMots)
                        Case "AMORTDTOT" : .AmortiTotal_Dtot = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next

    End Sub


#End Region




End Class
