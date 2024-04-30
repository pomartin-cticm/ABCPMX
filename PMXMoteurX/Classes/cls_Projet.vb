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
    ''' Chemin du fichier du projet déjà enregistré
    ''' </summary>
    Public FileName As String

    ''' <summary>
    ''' Indique si le projet est modifié après sauvegarde  
    ''' </summary>
    'Public Property lModif As Boolean
    '    Set(value As Boolean)
    '        For i As Integer = 0 To Me.Poutres.Count - 1
    '            Me.Poutres(i).lPoutreModifiee = value
    '        Next
    '    End Set
    '    Get
    '        Dim retour As Boolean = False
    '        For i As Integer = 0 To Me.Poutres.Count - 1
    '            retour = retour Or Me.Poutres(i).lPoutreModifiee
    '        Next
    '        Return retour
    '    End Get
    'End Property

    ''' <summary>
    ''' Indique si le projet est déjà enregistré
    ''' </summary>
    Public Property lSaved As Boolean
        Set(value As Boolean)
            For i As Integer = 0 To Me.Poutres.Count - 1
                Me.Poutres(i).lDonneesSauvees = value
            Next
        End Set
        Get
            Dim retour As Boolean = True
            For i As Integer = 0 To Me.Poutres.Count - 1
                retour = retour And Me.Poutres(i).lDonneesSauvees
            Next
            Return retour
        End Get
    End Property

    ''' <summary>
    ''' Indique si le projet est déjà enregistré
    ''' </summary>
    Public Property lNouvellePoutre As Boolean
        Set(value As Boolean)
            For i As Integer = 0 To Me.Poutres.Count - 1
                Me.Poutres(i).lNouvellePoutre = value
            Next
        End Set
        Get
            Dim retour As Boolean = True
            For i As Integer = 0 To Me.Poutres.Count - 1
                retour = retour And Me.Poutres(i).lNouvellePoutre
            Next
            Return retour
        End Get
    End Property



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
        'Me.Nom = ""
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
        Lines.Add("'       /!\   Don't modify this file   /!\")
        Lines.Add("'       /!\ Ne pas modifier ce fichier /!\")
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("")

        '==[ Identification ]=================================================================
        Lines.Add("BLOCK IDENTIFICATION")
        Lines.Add("   Utilisateur   = " & Me.Utilisateur)
        Lines.Add("   Entreprise    = " & Me.Entreprise)
        Lines.Add("   NomProjet     = " & Me.Nom)
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
                Lines.Add("   BeaIden           =  " & .BeamID)
                Lines.Add("   Commentaire       =  " & .Commentaire)
                'Lines.Add("   TypeSection   =  " & .TypeSection)
                Lines.Add("   ConsoleGauche  =  " & .lTraveeConsoleGauche)
                Lines.Add("   ConsoleDroite  =  " & .lTraveeConsoleDroite)
                Lines.Add("   DalleContGauche  =  " & .lDalleContinueGauche)
                Lines.Add("   DalleContDroite  =  " & .lDalleContinueDroite)
                Lines.Add("   lTremieGauche  =  " & .lTremieGauche)
                Lines.Add("   lTremieDroite  =  " & .lTremieDroite)
                Lines.Add("   NbTravee       =  " & .NombreTraveesDeuxAppuis)
                Lines.Add("   LongueurTravee =  " & ConvertListDecimalToString(.LongueurTravee))

                Dim listTypTravee(.TypTravee.Count - 1) As Integer

                For i As Integer = 0 To listTypTravee.Count - 1
                    listTypTravee(i) = .TypTravee(i)
                Next
                Lines.Add("   TypeTravee     =  " & ConvertListIntegerToString(listTypTravee))

                Lines.Add("   TypeEtaiement  =  " & .TypeEtaiement)
                Lines.Add("   EtaisConsG     =  " & .lEtaisConsoleGauche)
                Lines.Add("   EtaisConsD  =  " & .lEtaisConsoleDroite)
                Lines.Add("   NbPropping     =  " & .NbEtaiement)
                'Lines.Add("   NbRestrain     =  " & ConvertListIntegerToString(.NbMaintiens))

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
                'Lines.Add("   lDonneesSauv   =  " & .lDonneesSauvees)
                'Lines.Add("   lNouvPoutre    =  " & .NouvellePoutre)
                If .lMixte Then
                    Lines.Add("   lAutoDesign    =  " & .lAutomaticDesign)
                    Lines.Add("   NombreZones    =  " & ConvertListIntegerToString(.NombreZones))
                    Lines.Add("   LongueurZone    =  " & ConvertListDecimalToString(.LongueurZone))
                    Lines.Add("   EspaZone    =  " & ConvertListDecimalToString(.EspacementZone))
                    Lines.Add("   EspaBacTransZone    =  " & ConvertListIntegerToString(.Espacement_Bac_TransZone))
                    Lines.Add("   NbGoujonTrans    =  " & ConvertListIntegerToString(.NombreGoujonsTransv))
                End If

                Lines.Add("   lCombELU    =  " & ConvertListBooleanToString(.lCombELU))
                Lines.Add("   CoefELU    =  " & ConvertListDecimalToString(.CoefCombELU))
                Lines.Add("   lCombELS    =  " & ConvertListBooleanToString(.lCombELS))
                Lines.Add("   CoefELS    =  " & ConvertListDecimalToString(.CoefCombELS))
                Lines.Add("   lCombELF    =  " & ConvertListBooleanToString(.lCombFeu))
                Lines.Add("   CoefELF    =  " & ConvertListDecimalToString(.CoefCombFeu))
                Lines.Add("   lCombELCU    =  " & ConvertListBooleanToString(.lCombELCURules))
                Lines.Add("   CoefELCU    =  " & ConvertListDecimalToString(.CoefCombELCU))
                Lines.Add("   lCombELCS    =  " & ConvertListBooleanToString(.lCombELCSRules))
                Lines.Add("   CoefELCS    =  " & ConvertListDecimalToString(.CoefCombELCS))

                Lines.Add("")



                '==[ Classe Maintien ]=================================================================
                For i As Integer = 0 To .Maintiens.Count - 1
                    For Each maint In .Maintiens(i)

                        ' If maint IsNot Nothing Then
                        With maint

                            Lines.Add("BLOCK MAINTIENS")
                            Lines.Add("   indTravee      =  " & i)
                            Lines.Add("   xloc           =  " & .x_Loc)
                            Lines.Add("   lMaintSemSup   =  " & .lMaintienSemelleSup)
                            Lines.Add("   lMaintSemInf   =  " & .lMaintienSemelleInf)
                            Lines.Add("")
                        End With
                        ' End If

                    Next
                Next

                '==[ Classe maintien par le bac ]==================================================

                With .MaintienBac
                    Lines.Add("BLOCK MAINT_BAC")

                    'Lines.Add("   AP            =  " & .ap)
                    'Lines.Add("   BP            =  " & .bp)
                    Lines.Add("   NT            =  " & .nt)
                    Lines.Add("   M            =  " & .m)
                    Lines.Add("   Transition            =  " & .Transition)
                    Lines.Add("   FixNervuresMod            =  " & .FixNervuresMod)
                    Lines.Add("   FixnervuresTyp            =  " & .FixnervuresTyp)
                    Lines.Add("   EC            =  " & .ec)
                    Lines.Add("   FixCoutureType            =  " & .FixCoutureType)
                    Lines.Add("   lMaintienBac            =  " & .lMaintienBac)
                    Lines.Add("   lTheta            =  " & .lTheta)
                End With



                '==[ Classe Section ]=================================================================
                With .Section
                    Lines.Add("BLOCK SECTION")

                    Lines.Add("   Nom            =  " & .Nom)
                    Lines.Add("   lDalleBeton    =  " & .lDalleBeton)
                    Lines.Add("   lDatabase      =  " & .lDatabase)
                    Lines.Add("   typeSection    =  " & .TypeSection)
                    Lines.Add("   lUser      =  " & .lUser)
                    Lines.Add("   f_y_fs         =  " & .f_y.fs)
                    Lines.Add("   f_y_w          =  " & .f_y.w)
                    Lines.Add("   f_y_fi         =  " & .f_y.fi)
                    Lines.Add("")

                    '==[ Classe ProfilA ]=================================================================
                    With .ProfilA
                        Lines.Add("BLOCK PROFILA")
                        Lines.Add("   Gamme          =  " & .Gamme)

                        Lines.Add("   NomProfile     =  " & .NomProfile)
                        Lines.Add("   ha             =  " & .ha)
                        Lines.Add("   hb             =  " & .hb)
                        Lines.Add("   bfs            =  " & .Bfs)
                        Lines.Add("   tfs            =  " & .Tfs)
                        Lines.Add("   bfi            =  " & .Bfi)
                        Lines.Add("   tfi            =  " & .Tfi)
                        Lines.Add("   rcs            =  " & .Rcs)
                        Lines.Add("   rci            =  " & .Rci)
                        'Lines.Add("   hw             =  " & .h_w)
                        Lines.Add("   tw             =  " & .Tw)
                        Lines.Add("   soudure_a      =  " & .aW)
                        Lines.Add("   typeProfil     =  " & .typeProfileAcier)
                        Lines.Add("   Platb          =  " & .Plat_b)
                        Lines.Add("   Platt          =  " & .Plat_t)
                        If .IndDeliv IsNot Nothing Then Lines.Add("   IndDeliv       =  " & ConvertListShortToString(.IndDeliv))
                        Lines.Add("   IndStand       =  " & ConvertListShortToString(.IndStandart))
                        Lines.Add("")

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
                        'Lines.Add("   lUser      =  " & .lUser)
                        'Lines.Add("   f_y_fs         =  " & .f_y.fs)
                        'Lines.Add("   f_y_w          =  " & .f_y.w)
                        'Lines.Add("   f_y_fi         =  " & .f_y.fi)
                        Lines.Add("")
                    End With


                    '==[ Classe Enrobage Partiel ProfilA ]=================================================================
                    With .Enrobage
                        Lines.Add("BLOCK ENROBAGE_PROFILA")

                        Lines.Add("   lArmaConst     =  " & .lArmaConst)
                        Lines.Add("   ConstPhi       =  " & .ConstPhi)
                        Lines.Add("   Ratio_bc       =  " & .Ratio_bc)
                        Lines.Add("   Etrier_Type    =  " & .Etriers_Type)
                        Lines.Add("   Etrier_Phi     =  " & .Etriers_Phi)
                        Lines.Add("   Etrier_CY  =  " & .Etriers_EnrobageY)
                        Lines.Add("   Etrier_CZ  =  " & .Etriers_EnrobageZ)
                        Lines.Add("")

                        '==[ Classe Armature Enrobage Partiel ProfilA ]=================================================================
                        Lines.Add("BLOCK ARMATURE_ENROBAGE_PROFILA")

                        For i As Integer = 0 To .LitArma.Length - 1
                            With .LitArma(i)
                                Lines.Add("   indLit         =  " & i)
                                Lines.Add("   PhiExt         =  " & .PhiExt)
                                Lines.Add("   NbExt          =  " & .NbExt)
                                Lines.Add("   lActExt          =  " & .lActiveExt)
                                Lines.Add("   PhiMil         =  " & .PhiMil)
                                Lines.Add("   NbMil          =  " & .NbMil)
                                Lines.Add("   PhiInt         =  " & .PhiInt)
                                Lines.Add("   NbInt          =  " & .NbInt)
                                Lines.Add("   lActInt          =  " & .lActiveInt)
                                If i = 1 Then Lines.Add("   zPosRatio      =  " & .zPosRatio)
                                Lines.Add("")
                            End With
                        Next

                        '==[ Classe Acier Armature Enrobage Partiel ProfilA ]=================================================================
                        Lines.Add("BLOCK ACIER_ARMATURE_ENROBAGE_PROFILA")
                        With .AcierArmatures
                            Lines.Add("   Classe         =  " & .Classe)
                            Lines.Add("   FsK            =  " & .FsK)
                            Lines.Add("   Es             =  " & .Es)
                            Lines.Add("")
                        End With

                        '==[ Classe Béton Enrobage Partiel ProfilA ]=================================================================
                        Lines.Add("BLOCK BETON_ENROBAGE_PROFILA")
                        With .Beton
                            Lines.Add("   Leger          =  " & .lLeger)
                            Lines.Add("   Classe         =  " & .Classe)
                            Lines.Add("   RhoC         =  " & .RhoC)
                            Lines.Add("   Fck            =  " & .Fck)
                            Lines.Add("   Fcm            =  " & .Fcm)
                            Lines.Add("   Fctm           =  " & .Fctm)
                            Lines.Add("   Ecm            =  " & .Ecm)
                            Lines.Add("   lCrackLimit    =  " & .lCrackingLimitation)
                            Lines.Add("   wk_max         =  " & .wk_max)
                            Lines.Add("")
                        End With
                    End With
                End With

                '==[ Classe Dalle ]=================================================================
                With .Dalle
                    Lines.Add("BLOCK DALLE")

                    Lines.Add("   Type           =  " & .type)
                    Lines.Add("   td             =  " & .Ep_td)
                    Lines.Add("   th             =  " & .Ep_th)
                    Lines.Add("   Beff           =  " & .Beff)
                    'Lines.Add("   lArmInf        =  " & .lArma_Inf)
                    'Lines.Add("   lArmSup        =  " & .lArma_Sup)
                    Lines.Add("   preDalle_ep    =  " & .preDalle_ep)
                    Lines.Add("   preDalle_tjoi  =  " & .preDalle_tjoint)
                    'Lines.Add("   theta_h        =  " & .pTheta_h)
                    Lines.Add("")

                    '==[ Classe Béton Dalle ]=================================================================
                    With .beton
                        Lines.Add("BLOCK BETON_DALLE")

                        Lines.Add("   Leger           =  " & .lLeger)
                        Lines.Add("   Classe         =  " & .Classe)
                        Lines.Add("   RhoC         =  " & .RhoC)
                        Lines.Add("   Fck            =  " & .Fck)
                        Lines.Add("   Fcm            =  " & .Fck)
                        Lines.Add("   Fctm           =  " & .Fctm)
                        Lines.Add("   Ecm            =  " & .Ecm)
                        Lines.Add("   lCrackLimit    =  " & .lCrackingLimitation)
                        Lines.Add("   wk_max         =  " & .wk_max)
                        Lines.Add("")
                    End With

                    '==[ Classe Bac Dalle ]=================================================================
                    With .Bac

                        Lines.Add("BLOCK BAC_DALLE")

                        Lines.Add("   Etiquette      =  " & .Etiquette)
                        Lines.Add("   Producteur     =  " & .Producteur)
                        Lines.Add("   lDatabase      =  " & .lDatabase)
                        Lines.Add("   h_rs           =  " & .h_rs)
                        Lines.Add("   h_p            =  " & .Hp)
                        Lines.Add("   b_b            =  " & .Bb)
                        Lines.Add("   b_t            =  " & .Bt)
                        Lines.Add("   e_p            =  " & .Ep)
                        Lines.Add("   tp             =  " & .Tp)
                        Lines.Add("   orientation    =  " & .Orientation)
                        Lines.Add("   msurf          =  " & .msurf)
                        Lines.Add("   fyp            =  " & .fyp)
                        Lines.Add("   LargeurModule  =  " & .LargeurModule)
                        Lines.Add("   Ieff           =  " & .Ieff)
                        Lines.Add("   lPreperce      =  " & .lPreperce)
                        Lines.Add("   AppuiT         =  " & .AppuiT)
                        Lines.Add("   AppuiL         =  " & .AppuiL)
                        Lines.Add("")
                    End With

                    '==[ Classe Cofradal Dalle ]=================================================================
                    With .Cofradal

                        Lines.Add("BLOCK COFRADAL")

                        Lines.Add("   Nom      =  " & .nom)
                        Lines.Add("   Dp      =  " & .dp)
                        Lines.Add("   Msurf      =  " & .msurf)
                        Lines.Add("   lCustom      =  " & .lCustom)

                    End With

                    '==[ Classe Armature Dalle ]=================================================================
                    For Each arma_longi As Cls_Armatures_Longi In .LitArma
                        With arma_longi
                            If ptre.Dalle.LitArma.IndexOf(arma_longi) = 0 Or (.lActive And ptre.Dalle.LitArma.IndexOf(arma_longi) = 1) Then
                                Lines.Add("BLOCK ARMATURE_DALLE")
                                Lines.Add("   indLit         =  " & ptre.Dalle.LitArma.IndexOf(arma_longi))
                                Lines.Add("   EspBar         =  " & .EspBar)
                                Lines.Add("   PhiS           =  " & .PhiS)
                                Lines.Add("   z_s            =  " & .z_s)
                                Lines.Add("   n_s            =  " & .n_s)
                                'Lines.Add("   c_s            =  " & .c_s)
                                Lines.Add("   lActive        =  " & .lActive)
                                Lines.Add("")
                            End If
                        End With
                    Next

                    '==[ Classe Acier Armature Dalle ]=================================================================
                    With .AcierArmatures
                        Lines.Add("BLOCK ACIER_ARMATURE_DALLE")

                        Lines.Add("   Classe         =  " & .Classe)
                        Lines.Add("   Fsk            =  " & .FsK)
                        Lines.Add("   Es             =  " & .Es)
                        Lines.Add("")
                    End With


                    '==[ Classe Connecteur Dalle ]=================================================================
                    With .Connecteur
                        Lines.Add("BLOCK CONNECTEUR_DALLE")

                        Lines.Add("   nom            =  " & .nom)
                        Lines.Add("   hsc            =  " & .hsc)
                        Lines.Add("   d              =  " & .d)
                        Lines.Add("   fy              =  " & .Fy)
                        Lines.Add("   fu              =  " & .Fu)
                        Lines.Add("")
                    End With
                End With

                '==[ Classe Options Calculs ]=================================================================
                With .Param

                    Lines.Add("BLOCK OPT_CALCULS")
                    Lines.Add("   RH            = " & .RH)
                    Lines.Add("   Norme            = " & .Norme)
                    Lines.Add("   EtaW            = " & .EtaW)
                    Lines.Add("   lLarEffSimp            = " & .lLargeurEfficaceSimplifiee)
                    Lines.Add("   lCompArma            = " & .lCompressionArma)
                    Lines.Add("   dMaxNodes            = " & .dMaxNodes)
                    Lines.Add("   nbMinNodesTr            = " & .nbMinNodesTravee)
                    Lines.Add("   nbMinNodesCo            = " & .nbMinNodesConsole)
                    Lines.Add("   epsSH            = " & .EpsilonSH)
                    Lines.Add("   lRetraitEnr            = " & .lRetraitEnrobage)
                    Lines.Add("   ArmaYoung            = " & .ArmaYoung)
                    Lines.Add("   Gravite            = " & .GraviteG)
                    Lines.Add("   PsiLPerm            = " & .PsiLPermanent)
                    Lines.Add("   PsiLRetrait            = " & .PsiLRetrait)
                    Lines.Add("   AgeT0G1_0            = " & .AgeT0G1(0))
                    Lines.Add("   AgeT0G1_1            = " & .AgeT0G1(1))
                    Lines.Add("   AgeT0G2_0            = " & .AgeT0G2(0))
                    Lines.Add("   AgeT0G2_1            = " & .AgeT0G2(1))
                    Lines.Add("   AgeT0SH_0            = " & .AgeT0SH(0))
                    Lines.Add("   AgeT0SH_1            = " & .AgeT0SH(1))
                    Lines.Add("   AgeTCalc            = " & .AgeT)
                    Lines.Add("   lElasticDesign            = " & .lElasticDesignVM)
                    Lines.Add("   lMaitFissure            = " & .lMaitriseFissuration)


                    Lines.Add("")

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
                        Lines.Add("")
                    End With

                End With

                '==[ Classe Options Calculs Feu ]=================================================================
                With .ParamFeu

                    Lines.Add("BLOCK OPT_FEU")
                    Lines.Add("   TempRef            = " & .TempRef)
                    Lines.Add("   EmissFire            = " & .EmissivityFire)
                    Lines.Add("   EmissSteel            = " & .EmissivitySteel)
                    Lines.Add("   ConvCoef            = " & .ConvectionCoef)
                    Lines.Add("   ConvCoefDalle            = " & .ConvectionCoefDalle)
                    Lines.Add("   PhiViewFactor            = " & .PhiViewFactor)
                    Lines.Add("   lHeatingSlabEF            = " & .lHeatingSlabEF)
                    Lines.Add("   tDalleEFmax            = " & .tDalleEFmax)
                    Lines.Add("   ksh            = " & .ksh)
                    Lines.Add("   AlphaSlab            = " & .AlphaSlab)
                    Lines.Add("   lArmaCompression            = " & .lArmaCompression)
                    Lines.Add("   lArmaFormeeAFroid            = " & .lArmaFormeeAFroid)
                    Lines.Add("   lCalculFeu            = " & .lCalcuFeu)
                    Lines.Add("   lDalleFEM            = " & .lDalleFEM)
                    Lines.Add("   lReducConcrStr            = " & .lReductionConcreteStrenght)
                    Lines.Add("   TypeSurface            = " & .TypeSurface)
                    Lines.Add("   Protection            = " & .Protection)
                    Lines.Add("   EpProtection            = " & .EpProtection)
                    Lines.Add("   CustomLambdaP            = " & .CustomLambdaP)
                    Lines.Add("   DeltaCalcul            = " & .DeltaTCalcul)
                    Lines.Add("")

                End With

                '==[ Classe Hivoss ]=================================================================
                With .Hivoss
                        Lines.Add("BLOCK OPT_CALCULS_HIVOSS")

                        Lines.Add("   lHivossMethod = " & .lHivossMethod)
                        Lines.Add("   RatioQ        = " & .ratioQ)
                        Lines.Add("   ChoixQ        = " & .choixQ)
                        Lines.Add("   UtilPlancher  = " & .UtilisationPlancher)
                        Lines.Add("   lFreqDalle  = " & .lFreqDalle)
                        Lines.Add("   Mobilier      = " & .Mobilier)
                        Lines.Add("   lFauxPlafond  = " & .lFauxPlafond)
                        Lines.Add("   lChappeFlottante  = " & .lChappeFlottante)
                        Lines.Add("   AmortD1       = " & .AmortiStructure_D1)
                        Lines.Add("   AmortD2       = " & .AmortiMobilier_D2)
                        Lines.Add("   AmortD3       = " & .AmortiFinition_D3)
                        Lines.Add("   AmortDtot     = " & .AmortiTotal_Dtot)
                        Lines.Add("")
                    End With

                    '==[ Classe ChargementU ]=================================================================
                    For Each elemnts As KeyValuePair(Of String, cls_ChargementUtilisateur) In .ChargesU
                        For i As Integer = .IndicePremiereTravee To .IndiceDerniereTravee
                            Lines.Add("BLOCK CHGTU_QSURF")
                            Lines.Add("   CleDic      =  " & elemnts.Key)
                            Lines.Add("   indTravee      =  " & i)
                            Lines.Add("   QSurf      =  " & elemnts.Value.QSurf(i))
                            Lines.Add("")

                            For Each force As cls_Force In elemnts.Value.Forces(i)
                                Lines.Add("BLOCK CHGTU_FORCE")
                                Lines.Add("   CleDic      =  " & elemnts.Key)
                                Lines.Add("   indTravee      =  " & i)
                                Lines.Add("   Force      =  " & force.Force)
                                Lines.Add("   xPosT      =  " & force.xPosT)
                                Lines.Add("   xGaucheT   =  " & force.xGaucheT)
                                Lines.Add("")
                            Next

                            For Each frepart As cls_ForceRepartie In elemnts.Value.FReparties(i)
                                If Not (elemnts.Value.FReparties(i).IndexOf(frepart) = 0 And elemnts.Key = cls_Poutre.KEYPP) Then 'le premier cas de charge de FRepartie concerne le PP que l'on ne veut pas enregistrer car calculer automatiquement
                                    Lines.Add("BLOCK CHGTU_FREPAR")
                                    Lines.Add("   CleDic      =  " & elemnts.Key)
                                    Lines.Add("   indTravee      =  " & i)
                                    Lines.Add("   F0      =  " & frepart.Force(0))
                                    Lines.Add("   F1      =  " & frepart.Force(1))
                                    Lines.Add("   xPosT0      =  " & frepart.xPosT(0))
                                    Lines.Add("   xPosT1      =  " & frepart.xPosT(1))
                                    Lines.Add("   xGaucheT      =  " & frepart.xGaucheT)
                                    Lines.Add("")
                                End If
                            Next
                        Next
                    Next

                End With
        Next


    End Sub

    Public Sub RecuperationFile(ByVal FileName As String, ByVal str_warning_file As String)
        '---------------------------------------------------------------------------------------------------------
        '   09/08/23 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Initialisation d'un projet à partir d'un fichier de données
        '---------------------------------------------------------------------------------------------------------
        '   FileName    [E] :   Nom du fichier
        '   str_warning [E] :   Message d'avertissement
        '---------------------------------------------------------------------------------------------------------

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
            Me.lSaved = True
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

        If Not ListeBlocCle.Contains("IDENTIFICATION") And Not ListeBlocCle.Contains("POUTRE") And Not ListeBlocCle.Contains("MAINTIENS") And Not ListeBlocCle.Contains("MAINT_BAC") And Not ListeBlocCle.Contains("SECTION") And
           Not ListeBlocCle.Contains("PROFILA") And Not ListeBlocCle.Contains("ACIER_PROFILA") And Not ListeBlocCle.Contains("ENROBAGE_PROFILA") And Not ListeBlocCle.Contains("ARMATURE_ENROBAGE_PROFILA") And
           Not ListeBlocCle.Contains("ACIER_ARMATURE_ENROBAGE_PROFILA") And Not ListeBlocCle.Contains("BETON_ENROBAGE_PROFILA") And Not ListeBlocCle.Contains("DALLE") And Not ListeBlocCle.Contains("BETON_DALLE") And
           Not ListeBlocCle.Contains("BAC_DALLE") And Not ListeBlocCle.Contains("COFRADAL") And Not ListeBlocCle.Contains("ARMATURE_DALLE") And Not ListeBlocCle.Contains("ACIER_ARMATURE_DALLE") And
           Not ListeBlocCle.Contains("CONNECTEUR_DALLE") And Not ListeBlocCle.Contains("OPT_CALCULS") And Not ListeBlocCle.Contains("OPT_CALCULS_GAMMA") And Not ListeBlocCle.Contains("OPT_FEU") And
           Not ListeBlocCle.Contains("OPT_CALCULS_HIVOSS") And Not ListeBlocCle.Contains("CHGTU_QSURF") And Not ListeBlocCle.Contains("CHGTU_FORCE") And Not ListeBlocCle.Contains("CHGTU_FREPAR") Then '

            MsgBox("Fichier corrumpu | Corrupted file", MsgBoxStyle.Critical, "Cls_Projet/LectureFile")

        End If

        Dim oldFichier As Boolean = False
        'Dim NomCas() As String = {"G1", "G2", "Q", "QC"}

        '--> Traitement des blocks

        For i = 0 To ListeBlocIndex.Count - 1
            If i = ListeBlocIndex.Count - 1 Then IndexFin = Lines.Lines.Count - 1 Else IndexFin = ListeBlocIndex(i + 1) - 1
            Select Case ListeBlocCle(i)

                Case "IDENTIFICATION"
                    ReadBloc_Identification(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)

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

                Case "MAINT_BAC"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim maintien_bac As New cls_MaintienBac
                    ReadBlocMaintienParLeBac(maintien_bac, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.MaintienBac = maintien_bac


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
                    Dim acier_profilA As New cls_Acier
                    ReadBlocAcierProfilA(acier_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Acier = acier_profilA

                Case "ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim enrobage_profilA As New cls_Enrobage_Partiel
                    ReadBlocEnrobageProfilA(enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage = enrobage_profilA

                Case "ARMATURE_ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_enrobage_profilA(2) As cls_ArmatureEnrobage

                    For j As Integer = 0 To armature_enrobage_profilA.Length - 1
                        armature_enrobage_profilA(j) = New cls_ArmatureEnrobage
                    Next

                    ReadBlocArmatureEnrobageProfilA(armature_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage.LitArma = armature_enrobage_profilA

                Case "ACIER_ARMATURE_ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_enrobage_profilA As New cls_AcierArmature
                    ReadBlocAcierArmatureEnrobageProfilA(acier_armature_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage.AcierArmatures = acier_armature_enrobage_profilA

                Case "BETON_ENROBAGE_PROFILA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim beton_enrobage_profilA As New cls_Beton
                    ReadBlocBetonEnrobageProfilA(beton_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage.Beton = beton_enrobage_profilA

                Case "DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim dalle_en_cours As New cls_Dalle
                    ReadBlocDalle(dalle_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle = dalle_en_cours

                Case "BETON_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim beton_dalle As New cls_Beton
                    ReadBlocBetonDalle(beton_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.beton = beton_dalle

                Case "BAC_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim bac_en_cours As New cls_Bac
                    ReadBlocBacDalle(bac_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Bac = bac_en_cours

                Case "COFRADAL"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim cofradal_en_cours As New cls_Cofradal
                    ReadBlocCofradal(cofradal_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Cofradal = cofradal_en_cours

                Case "ARMATURE_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_dalle As New Cls_Armatures_Longi
                    Dim ind_travee As Integer
                    ReadBlocArmatureDalle(armature_dalle, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.LitArma(ind_travee) = armature_dalle

                Case "ACIER_ARMATURE_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_dalle As New cls_AcierArmature
                    ReadBlocAcierArmatureDalle(acier_armature_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.AcierArmatures = acier_armature_dalle

                Case "CONNECTEUR_DALLE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim connecteur_dalle As New cls_Connecteur
                    ReadBlocConnecteurDalle(connecteur_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Connecteur = connecteur_dalle

                Case "OPT_CALCULS"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim opt_calculs_en_cours As New cls_OptionsCalcul
                    ReadBlocOptionsCalculs(opt_calculs_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param = opt_calculs_en_cours


                'Case "OPT_CALCULS_PROP_ELAST_ENROBAGE"
                '    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                '    Dim prop_elast_enrob_opt_calculs As Cls_Prop_Elastique
                '    ReadBlocPropElastEnrobageOptionsCalculs(prop_elast_enrob_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                '    ptre_en_cours.Param.Prop_Elastique_Enrobage = prop_elast_enrob_opt_calculs

                'Case "OPT_CALCULS_PROP_ELAST_DALLE"
                '    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                '    Dim prop_elast_dalle_opt_calculs As Cls_Prop_Elastique
                '    ReadBlocPropElastDalleOptionsCalculs(prop_elast_dalle_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                '    ptre_en_cours.Param.Prop_Elastique_Dalle = prop_elast_dalle_opt_calculs

                Case "OPT_CALCULS_GAMMA"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim gamma_opt_calculs As New cls_Gamma
                    ReadBlocGammaOptionsCalculs(gamma_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param.Gamma = gamma_opt_calculs

                Case "OPT_FEU"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim opt_calculs_en_cours As New cls_OptionsFeu
                    ReadBlocOptionsFeu(opt_calculs_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.ParamFeu = opt_calculs_en_cours

                Case "OPT_CALCULS_HIVOSS"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim hivoss_opt_calculs As New cls_MethodHivoss
                    ReadBlocHivossOptionsCalculs(hivoss_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Hivoss = hivoss_opt_calculs

                Case "CHGTU_QSURF"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim QSurf_en_cours As Decimal
                    Dim cle_dic As String = ""
                    Dim ind_travee As Integer
                    ReadBlocQSurf(QSurf_en_cours, cle_dic, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.ChargesU(cle_dic).QSurf(ind_travee) = QSurf_en_cours

                Case "CHGTU_FORCE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim force_en_cours As New cls_Force
                    Dim cle_dic As String = ""
                    Dim ind_travee As Integer
                    ReadBlocForce(force_en_cours, cle_dic, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.ChargesU(cle_dic).Forces(ind_travee).Add(force_en_cours)

                Case "CHGTU_FREPAR"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim frepart_en_cours As New cls_ForceRepartie
                    Dim cle_dic As String = ""
                    Dim ind_travee As Integer
                    ReadBlocFRepartie(frepart_en_cours, cle_dic, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.ChargesU(cle_dic).FReparties(ind_travee).Add(frepart_en_cours)

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
            MsgBox(str_warning_file, MsgBoxStyle.Critical, NomLogiciel)
        End If

    End Sub

    Private Sub ReadBloc_Identification(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                    Case "NOMP"
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
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With poutre_en_cours
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "BEAIDEN"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .BeamID = Lignes(i).Substring(iFirst - 1)
                            Else
                                .BeamID = ""
                            End If
                        Case "COMMENTAIR"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .Commentaire = Lignes(i).Substring(iFirst - 1)
                            Else
                                .Commentaire = ""
                            End If
                        'Case "TYPESECTIO" : .TypeSection = Mots(nbMots)
                        Case "CONSOLEGAU" : .lTraveeConsoleGauche = Mots(nbMots)
                        Case "CONSOLEDRO" : .lTraveeConsoleDroite = Mots(nbMots)
                        Case "DALLECONTG" : .lDalleContinueGauche = Mots(nbMots)
                        Case "DALLECONTD" : .lDalleContinueDroite = Mots(nbMots)
                        Case "LTREMIEGAU" : .lTremieGauche = Mots(nbMots)
                        Case "LTREMIEDRO" : .lTremieDroite = Mots(nbMots)
                        Case "NBTRAVEE" : .NombreTraveesDeuxAppuis = TraiteReal(Mots(nbMots))
                        Case "LONGUEURTR" : .LongueurTravee = ConvertStringToListDecimal(Mots(nbMots))
                        Case "TYPETRAVEE" : .TypTravee = ConvertStringToListInteger(Mots(nbMots))
                        Case "TYPEETAIEM" : .TypeEtaiement = TraiteReal(Mots(nbMots))
                        Case "ETAISCONSG" : .lEtaisConsoleGauche = Mots(nbMots)
                        Case "ETAISCONSD" : .lEtaisConsoleDroite = Mots(nbMots)
                        Case "NBPROPPING" : .NbEtaiement = Mots(nbMots)
                        'Case "NBRESTRAIN" : .NbMaintiens = ConvertStringToListInteger(Mots(nbMots))
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
                        'Case "LDONNEESSA" : .lDonneesSauvees = Mots(nbMots)
                        'Case "LNOUVPOUTR" : .NouvellePoutre = Mots(nbMots)
                        Case "LAUTODESIG" : .lAutomaticDesign = Mots(nbMots)
                        Case "NOMBREZONE" : .NombreZones = ConvertStringToListInteger(Mots(nbMots))
                        Case "LONGUEURZO" : .LongueurZone = ConvertStringToListDecimalDim2(Mots(nbMots))
                        Case "ESPAZONE" : .EspacementZone = ConvertStringToListDecimalDim2(Mots(nbMots))
                        Case "ESPABACTRA" : .Espacement_Bac_TransZone = ConvertStringToListIntegerDim2(Mots(nbMots))
                        Case "NBGOUJONTR" : .NombreGoujonsTransv = ConvertStringToListIntegerDim2(Mots(nbMots))
                        Case "LCOMBELU" : .lCombELU = ConvertStringToListBoolean(Mots(nbMots))
                        Case "COEFELU" : .CoefCombELU = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "LCOMBELS" : .lCombELS = ConvertStringToListBoolean(Mots(nbMots))
                        Case "COEFELS" : .CoefCombELS = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "LCOMBELF" : .lCombFeu = ConvertStringToListBoolean(Mots(nbMots))
                        Case "COEFELF" : .CoefCombFeu = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "LCOMBELCU" : .lCombELCURules = ConvertStringToListBoolean(Mots(nbMots))
                        Case "COEFELCU" : .CoefCombELCU = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "LCOMBELCS" : .lCombELCSRules = ConvertStringToListBoolean(Mots(nbMots))
                        Case "COEFELCS" : .CoefCombELCS = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
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
    ''' Lecture du bloc Maintien
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocMaintienParLeBac(maintien_bac As cls_MaintienBac, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With maintien_bac
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(14, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        'Case "AP" : .ap = TraiteReal(Mots(nbMots))
                        'Case "BP" : .bp = TraiteReal(Mots(nbMots))
                        Case "NT" : .nt = TraiteReal(Mots(nbMots))
                        Case "M" : .m = TraiteReal(Mots(nbMots))
                        Case "TRANSITION" : .Transition = TraiteReal(Mots(nbMots))
                        Case "FIXNERVURESMOD" : .FixNervuresMod = TraiteReal(Mots(nbMots))
                        Case "FIXNERVURESTYP" : .FixnervuresTyp = TraiteReal(Mots(nbMots))
                        Case "EC" : .ec = TraiteReal(Mots(nbMots))
                        Case "FIXCOUTURETYPE" : .FixCoutureType = TraiteReal(Mots(nbMots))
                        Case "LMAINTIENBAC" : .lMaintienBac = Mots(nbMots)
                        Case "LTHETA" : .lTheta = Mots(nbMots)
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
                        Case "TYPESECTIO" : .TypeSection = Mots(nbMots)
                        Case "LUSER" : .lUser = Mots(nbMots)
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
    ''' Lecture du bloc ProfilA
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocProfilA(profilA As cls_ProfilA, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With profilA
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "GAMME"
                            iFirst = InStr(Lignes(i), Mots(2))
                            .Gamme = Lignes(i).Substring(iFirst - 1)
                        Case "NOMPROFILE"
                            iFirst = InStr(Lignes(i), Mots(2))
                            .NomProfile = Lignes(i).Substring(iFirst - 1)
                        Case "HA" : .ha = TraiteReal(Mots(nbMots))
                        Case "HB" : .hb = TraiteReal(Mots(nbMots))
                        Case "BFS" : .Bfs = TraiteReal(Mots(nbMots))
                        Case "TFS" : .Tfs = TraiteReal(Mots(nbMots))
                        Case "BFI" : .Bfi = TraiteReal(Mots(nbMots))
                        Case "TFI" : .Tfi = TraiteReal(Mots(nbMots))
                        Case "RCS" : .Rcs = TraiteReal(Mots(nbMots))
                        Case "RCI" : .Rci = TraiteReal(Mots(nbMots))
                        'Case "HW" : .h_w = TraiteReal(Mots(nbMots))
                        Case "TW" : .Tw = TraiteReal(Mots(nbMots))
                        Case "SOUDURE_A" : .aW = TraiteReal(Mots(nbMots))
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
    Private Sub ReadBlocAcierProfilA(acier_profilA As cls_Acier, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        With acier_profilA
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "NUANCE"
                            iFirst = InStr(Lignes(i), Mots(2))
                            .Nuance = Lignes(i).Substring(iFirst - 1)
                        Case "QUALITE"
                            iFirst = InStr(Lignes(i), Mots(2))
                            .Qualite = Lignes(i).Substring(iFirst - 1)
                        Case "REDUCTION"
                            iFirst = InStr(Lignes(i), Mots(2))
                            .Reduction = Lignes(i).Substring(iFirst - 1)
                        Case "NORMEPRODU"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .NormeProduit = Lignes(i).Substring(iFirst - 1)
                            Else
                                .NormeProduit = ""
                            End If
                        Case "EPMAX" : .EpMax = TraiteReal(Mots(nbMots))
                        Case "IBASE" : .iBase = TraiteReal(Mots(nbMots))
                        Case "ITABSTAND" : .iTabStandart = TraiteReal(Mots(nbMots))
                        Case "ISTANDARD" : .iStandart = TraiteReal(Mots(nbMots))
                            'Case "LUSER" : .lUser = Mots(nbMots)
                            'Case "F_Y_FS" : .f_y.fs = TraiteReal(Mots(nbMots))
                            'Case "F_Y_W" : .f_y.w = TraiteReal(Mots(nbMots))
                            'Case "F_Y_FI" : .f_y.fi = TraiteReal(Mots(nbMots))
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
    Private Sub ReadBlocEnrobageProfilA(enrobage_profilA As cls_Enrobage_Partiel, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "RATIO_BC" : .Ratio_bc = TraiteReal(Mots(nbMots))
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
    Private Sub ReadBlocArmatureEnrobageProfilA(armature_enrobage_profilA() As cls_ArmatureEnrobage, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                            Case "LACTEXT" : .lActiveExt = Mots(nbMots)
                            Case "PHIMIL" : .PhiMil = TraiteReal(Mots(nbMots))
                            Case "NBMIL" : .NbMil = TraiteReal(Mots(nbMots))
                            Case "PHIINT" : .PhiInt = TraiteReal(Mots(nbMots))
                            Case "NBINT" : .NbInt = TraiteReal(Mots(nbMots))
                            Case "LACTINT" : .lActiveInt = Mots(nbMots)
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
    Private Sub ReadBlocAcierArmatureEnrobageProfilA(acier_armature_enrobage_profilA As cls_AcierArmature, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
    Private Sub ReadBlocBetonEnrobageProfilA(beton_enrobage_profilA As cls_Beton, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "LEGER" : .lLeger = Mots(nbMots)
                        Case "CLASSE" : .Classe = Mots(nbMots)
                        Case "RHOC" : .RhoC = Mots(nbMots)
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
    Private Sub ReadBlocDalle(dalle_en_cours As cls_Dalle, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "TD" : .Ep_td = TraiteReal(Mots(nbMots))
                        Case "TH" : .Ep_th = TraiteReal(Mots(nbMots))
                        Case "BEFF" : .Beff = TraiteReal(Mots(nbMots))
                        'Case "LARMINF" : .lArma_Inf = Mots(nbMots)
                        'Case "LARMSUP" : .lArma_Sup = Mots(nbMots)
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
    Private Sub ReadBlocBetonDalle(beton_dalle As cls_Beton, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "LEGER" : .lLeger = Mots(nbMots)
                        Case "CLASSE" : .Classe = Mots(nbMots)
                        Case "RHOC" : .RhoC = Mots(nbMots)
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
    Private Sub ReadBlocBacDalle(bac_dalle As cls_Bac, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With bac_dalle
                    Select Case MotCle
                        Case "ETIQUETTE"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .Etiquette = Lignes(i).Substring(iFirst - 1)
                            Else
                                .Etiquette = ""
                            End If
                        Case "PRODUCTEUR"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .Producteur = Lignes(i).Substring(iFirst - 1)
                            Else
                                .Producteur = ""
                            End If
                        Case "LDATABASE" : .lDatabase = Mots(nbMots)
                        Case "H_RS" : .h_rs = TraiteReal(Mots(nbMots))
                        Case "H_P" : .Hp = TraiteReal(Mots(nbMots))
                        Case "B_B" : .Bb = TraiteReal(Mots(nbMots))
                        Case "B_T" : .Bt = TraiteReal(Mots(nbMots))
                        Case "E_P" : .Ep = TraiteReal(Mots(nbMots))
                        Case "TP" : .Tp = TraiteReal(Mots(nbMots))
                        Case "ORIENTATIO" : .Orientation = Mots(nbMots)
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
    ''' Lecture du bloc COFRADAL
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocCofradal(cofradal As cls_Cofradal, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With cofradal
                    Select Case MotCle
                        Case "NOM"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .nom = Lignes(i).Substring(iFirst - 1)
                            Else
                                .nom = ""
                            End If
                        Case "DP" : .dp = Mots(nbMots)
                        Case "MSURF" : .msurf = TraiteReal(Mots(nbMots))
                        Case "LCUSTOM" : .lCustom = TraiteReal(Mots(nbMots))

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
    Private Sub ReadBlocArmatureDalle(armature_dalle As Cls_Armatures_Longi, ByRef ind_travee As Integer, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "INDLIT" : ind_travee = TraiteReal(Mots(nbMots))
                        Case "ESPBAR" : .EspBar = TraiteReal(Mots(nbMots))
                        Case "PHIS" : .PhiS = TraiteReal(Mots(nbMots))
                        Case "Z_S" : .z_s = TraiteReal(Mots(nbMots))
                        Case "N_S" : .n_s = TraiteReal(Mots(nbMots))
                        'Case "C_S" : .c_s = TraiteReal(Mots(nbMots))
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
    Private Sub ReadBlocAcierArmatureDalle(acier_armature_dalle As cls_AcierArmature, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
    Private Sub ReadBlocConnecteurDalle(connecteur_dalle As cls_Connecteur, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


                With connecteur_dalle
                    Select Case MotCle
                        Case "NOM"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .nom = Lignes(i).Substring(iFirst - 1)
                            Else
                                .nom = ""
                            End If
                        Case "HSC" : .hsc = TraiteReal(Mots(nbMots))
                        Case "D" : .d = TraiteReal(Mots(nbMots))
                        Case "FY" : .Fy = TraiteReal(Mots(nbMots))
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
    Private Sub ReadBlocOptionsCalculs(opt_calculs_en_cours As cls_OptionsCalcul, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(14, Mots(1).Length)).ToUpper


                With opt_calculs_en_cours
                    Select Case MotCle

                        Case "RH" : .RH = TraiteReal(Mots(nbMots))
                        Case "NORME" : .Norme = Mots(nbMots)
                        Case "ETAW" : .EtaW = TraiteReal(Mots(nbMots))
                        Case "LLAREFFSIMP" : .lLargeurEfficaceSimplifiee = Mots(nbMots)
                        Case "LCOMPARMA" : .lCompressionArma = Mots(nbMots)
                        Case "DMAXNODES" : .dMaxNodes = TraiteReal(Mots(nbMots))
                        Case "NBMINNODESTR" : .nbMinNodesTravee = TraiteReal(Mots(nbMots))
                        Case "NBMINNODESCO" : .nbMinNodesConsole = TraiteReal(Mots(nbMots))
                        Case "EPSSH" : .EpsilonSH = TraiteReal(Mots(nbMots))
                        Case "LRETRAITENR" : .lRetraitEnrobage = Mots(nbMots)
                        Case "ARMAYOUNG" : .ArmaYoung = TraiteReal(Mots(nbMots))
                        Case "GRAVITE" : .GraviteG = TraiteReal(Mots(nbMots))
                        Case "PSILPERM" : .PsiLPermanent = TraiteReal(Mots(nbMots))
                        Case "PSILRETRAIT" : .PsiLRetrait = TraiteReal(Mots(nbMots))
                        Case "AGET0G1_0" : .AgeT0G1(0) = TraiteReal(Mots(nbMots))
                        Case "AGET0G1_1" : .AgeT0G1(1) = TraiteReal(Mots(nbMots))
                        Case "AGET0G2_0" : .AgeT0G2(0) = TraiteReal(Mots(nbMots))
                        Case "AGET0G2_1" : .AgeT0G2(1) = TraiteReal(Mots(nbMots))
                        Case "AGET0SH_0" : .AgeT0SH(0) = TraiteReal(Mots(nbMots))
                        Case "AGET0SH_1" : .AgeT0SH(1) = TraiteReal(Mots(nbMots))
                        Case "AGETCALC" : .AgeT = TraiteReal(Mots(nbMots))
                        Case "LELASTICDESIGN" : .lElasticDesignVM = Mots(nbMots)
                        Case "LMAITFISSURE" : .lMaitriseFissuration = Mots(nbMots)
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End With

            End If
        Next


    End Sub

    '''' <summary>
    '''' Lecture du bloc Prop_Elast_Enrobage_Opt_Calculs
    '''' </summary>
    '''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    '''' <param name="Index0">indice du début de la lecture</param>
    '''' <param name="IndexFin">indice de la fin de la lecture</param>
    'Private Sub ReadBlocPropElastEnrobageOptionsCalculs(prop_elast_enrobage_opt_calculs As Cls_Prop_Elastique, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
    '    '==> Lecture du fichier pour initialiser les attributs

    '    '--> Déclaration
    '    Dim i As Integer
    '    Dim Mots(0) As String, nbMots As Integer
    '    Dim MotCle As String


    '    '--> Traitement
    '    For i = Index0 To IndexFin
    '        DecomposeLine(Lignes(i), Mots, nbMots)

    '        If nbMots > 0 Then
    '            MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


    '            With prop_elast_enrobage_opt_calculs
    '                Select Case MotCle
    '                    Case "PEEN_L" : .CE_n_L = TraiteReal(Mots(nbMots))
    '                    Case "PERH" : .RH = TraiteReal(Mots(nbMots))
    '                    Case "PETYPE" : .type_def_t = Mots(nbMots)
    '                    Case "PET" : .t = TraiteReal(Mots(nbMots))
    '                    Case "PEH0" : .h_0 = TraiteReal(Mots(nbMots))
    '                    Case "PERT0" : .R_t_0 = TraiteReal(Mots(nbMots))
    '                    Case "PERN_L" : .R_n_L = TraiteReal(Mots(nbMots))
    '                    Case "PEPT0" : .CP_t_0 = TraiteReal(Mots(nbMots))
    '                    Case "PEPN_L" : .CP_n_L = TraiteReal(Mots(nbMots))
    '                    Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
    '                End Select
    '            End With

    '        End If
    '    Next

    'End Sub

    '''' <summary>
    '''' Lecture du bloc Prop_Elast_Dalle_Opt_Calculs
    '''' </summary>
    '''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    '''' <param name="Index0">indice du début de la lecture</param>
    '''' <param name="IndexFin">indice de la fin de la lecture</param>
    'Private Sub ReadBlocPropElastDalleOptionsCalculs(prop_elast_dalle_opt_calculs As Cls_Prop_Elastique, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
    '    '==> Lecture du fichier pour initialiser les attributs

    '    '--> Déclaration
    '    Dim i As Integer
    '    Dim Mots(0) As String, nbMots As Integer
    '    Dim MotCle As String


    '    '--> Traitement
    '    For i = Index0 To IndexFin
    '        DecomposeLine(Lignes(i), Mots, nbMots)

    '        If nbMots > 0 Then
    '            MotCle = Mots(1).Substring(0, Math.Min(10, Mots(1).Length)).ToUpper


    '            With prop_elast_dalle_opt_calculs
    '                Select Case MotCle
    '                    Case "PDEN_L" : .CE_n_L = TraiteReal(Mots(nbMots))
    '                    Case "PDRH" : .RH = TraiteReal(Mots(nbMots))
    '                    Case "PDTYPE" : .type_def_t = Mots(nbMots)
    '                    Case "PDT" : .t = TraiteReal(Mots(nbMots))
    '                    Case "PDH0" : .h_0 = TraiteReal(Mots(nbMots))
    '                    Case "PDRT0" : .R_t_0 = TraiteReal(Mots(nbMots))
    '                    Case "PDRN_L" : .R_n_L = TraiteReal(Mots(nbMots))
    '                    Case "PDPT0" : .CP_t_0 = TraiteReal(Mots(nbMots))
    '                    Case "PDPN_L" : .CP_n_L = TraiteReal(Mots(nbMots))
    '                    Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
    '                End Select
    '            End With

    '        End If
    '    Next

    'End Sub

    ''' <summary>
    ''' Lecture du bloc Opt_Calculs_Gamma
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocGammaOptionsCalculs(gamma_opt_calculs As cls_Gamma, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
    ''' Lecture du bloc Opt_Calculs
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocOptionsFeu(opt_calculs_en_cours As cls_OptionsFeu, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String


        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(17, Mots(1).Length)).ToUpper


                With opt_calculs_en_cours
                    Select Case MotCle

                        Case "TEMPREF" : .TempRef = TraiteReal(Mots(nbMots))
                        Case "EMISSFIRE" : .EmissivityFire = TraiteReal(Mots(nbMots))
                        Case "EMISSSTEEL" : .EmissivitySteel = TraiteReal(Mots(nbMots))
                        Case "CONVCOEF" : .ConvectionCoef = TraiteReal(Mots(nbMots))
                        Case "CONVCOEFDALLE" : .ConvectionCoefDalle = TraiteReal(Mots(nbMots))
                        Case "PHIVIEWFACTOR" : .PhiViewFactor = TraiteReal(Mots(nbMots))
                        Case "LHEATINGSLABEF" : .lHeatingSlabEF = Mots(nbMots)
                        Case "TDALLEEFMAX" : .tDalleEFmax = TraiteReal(Mots(nbMots))
                        Case "KSH" : .ksh = TraiteReal(Mots(nbMots))
                        Case "ALPHASLAB" : .AlphaSlab = TraiteReal(Mots(nbMots))
                        Case "LARMACOMPRESSION" : .lArmaCompression = Mots(nbMots)
                        Case "LARMAFORMEEAFROID" : .lArmaFormeeAFroid = Mots(nbMots)
                        Case "LCALCULFEU" : .lCalcuFeu = Mots(nbMots)
                        Case "LDALLEFEM" : .lDalleFEM = Mots(nbMots)
                        Case "LREDUCCONCRSTR" : .lReductionConcreteStrenght = Mots(nbMots)
                        Case "TYPESURFACE" : .TypeSurface = Mots(nbMots)
                        Case "PROTECTION" : .Protection = Mots(nbMots)
                        Case "EPPROTECTION" : .EpProtection = TraiteReal(Mots(nbMots))
                        Case "CUSTOMLAMBDAP" : .CustomLambdaP = TraiteReal(Mots(nbMots))
                        Case "DELTACALCUL" : .DeltaTCalcul = TraiteReal(Mots(nbMots))

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
    Private Sub ReadBlocHivossOptionsCalculs(hivoss_opt_calculs As cls_MethodHivoss, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "UTILPLANCH" : .UtilisationPlancher = Mots(nbMots)
                        Case "LFREQDALLE" : .lFreqDalle = Mots(nbMots)
                        Case "MOBILIER" : .Mobilier = Mots(nbMots)
                        Case "LFAUXPLAFO" : .lFauxPlafond = Mots(nbMots)
                        Case "LCHAPPEFLO" : .lChappeFlottante = Mots(nbMots)
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


    ''' <summary>
    ''' Lecture du bloc QSurf
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocQSurf(ByRef QSurf_en_cours As Decimal, ByRef cle_dictionnaire As String, ByRef ind_travee As Integer, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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

                Select Case MotCle
                    Case "CLEDIC" : cle_dictionnaire = Mots(nbMots)
                    Case "INDTRAVEE" : ind_travee = TraiteReal(Mots(nbMots))
                    Case "QSURF" : QSurf_en_cours = TraiteReal(Mots(nbMots))
                    Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                End Select

            End If
        Next

    End Sub

    ''' <summary>
    ''' Lecture du bloc Force
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocForce(force_en_cours As cls_Force, ByRef cle_dictionnaire As String, ByRef ind_travee As Integer, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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

                Select Case MotCle
                    Case "CLEDIC" : cle_dictionnaire = Mots(nbMots)
                    Case "INDTRAVEE" : ind_travee = TraiteReal(Mots(nbMots))
                    Case "FORCE" : force_en_cours.Force = TraiteReal(Mots(nbMots))
                    Case "XPOST" : force_en_cours.xPosT = TraiteReal(Mots(nbMots))
                    Case "XGAUCHET" : force_en_cours.xGaucheT = TraiteReal(Mots(nbMots))
                    Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                End Select

            End If
        Next

    End Sub


    ''' <summary>
    ''' Lecture du bloc FRepartie
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocFRepartie(frepartie_en_cours As cls_ForceRepartie, ByRef cle_dictionnaire As String, ByRef ind_travee As Integer, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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

                Select Case MotCle
                    Case "CLEDIC" : cle_dictionnaire = Mots(nbMots)
                    Case "INDTRAVEE" : ind_travee = TraiteReal(Mots(nbMots))
                    Case "F0" : frepartie_en_cours.Force(0) = TraiteReal(Mots(nbMots))
                    Case "F1" : frepartie_en_cours.Force(1) = TraiteReal(Mots(nbMots))
                    Case "XPOST0" : frepartie_en_cours.xPosT(0) = TraiteReal(Mots(nbMots))
                    Case "XPOST1" : frepartie_en_cours.xPosT(1) = TraiteReal(Mots(nbMots))
                    Case "XGAUCHET" : frepartie_en_cours.xGaucheT = TraiteReal(Mots(nbMots))
                    Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                End Select
            End If
        Next

    End Sub


#End Region




End Class
