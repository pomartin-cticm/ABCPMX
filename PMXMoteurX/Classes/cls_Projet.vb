Imports System.Collections.Specialized.BitVector32
Imports System.IO
Imports System.Net.Mime.MediaTypeNames

Public Class cls_Projet

#Region " Déclarations "
    Const BkINDENT As String = "IDENTIFICATION"
    Const BkPOUTRE As String = "BEAM"
    Const BkSECTION As String = "SECTION"
    Const BkPROFILA As String = "PROFIL"
    Const BkACIERP As String = "ACIER_PROFILA"
    Const BkENROBAGE As String = "ENCASEMENT"
    Const BkARMAENROB As String = "ENCASEMENT_REINFORCEMENT"
    Const BkACIERARMAE As String = "ACIER_ARMATURE_ENROBAGE_PROFILA"
    Const BkDALLE As String = "SLAB"
    Const BkARMADALLE As String = "SLAB_REINFORCEMENT"
    Const BkACIERARMADALLE As String = "ACIER_ARMATURE_DALLE"
    Const BkBAC As String = "STEELDECK"
    Const BkCOFRADAL As String = "COFRADAL"
    Const BkBETONDALLE As String = "SLABCONCRETE"
    Const BkMAINTIENS As String = "MAINTIENS"
    Const BkMAINTIENBAC As String = "DECKRESTRAINT"
    Const BkBETONENROB As String = "ENCASEMENT_CONCRETE"

    Const BkGOUJON As String = "STUD"
    Const BkARMATRANS As String = "CONNECTEUR_DALLE_ARMATURE"

    Const BkOPTIONS As String = "OPTIONS"
    Const BkGAMMA As String = "GAMMA"
    Const BkHIVOSS As String = "HIVOSS"

#End Region

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

#Region " Fonctions support pour écriture du fichier de sauvegarde projet "

    Private Sub SaveFileBlocPoutre(myBeam As cls_Poutre, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc POUTRE
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkPOUTRE)
        AjouteLigneFrmt(Lines, "BeaIden", myBeam.BeamID)
        AjouteLigneFrmt(Lines, "Comment", myBeam.Commentaire)
        AjouteLigneFrmt(Lines, "LCantilever", myBeam.lTraveeConsoleGauche)
        AjouteLigneFrmt(Lines, "RCantilever", myBeam.lTraveeConsoleDroite)
        AjouteLigneFrmt(Lines, "LContSlab", myBeam.lDalleContinueGauche)
        AjouteLigneFrmt(Lines, "RContSlab", myBeam.lDalleContinueDroite)
        AjouteLigneFrmt(Lines, "LSlabOpen", myBeam.lTremieGauche)
        AjouteLigneFrmt(Lines, "RSlabOpen", myBeam.lTremieDroite)

        AjouteLigneFrmt(Lines, "NbSpan", myBeam.NombreTraveesDeuxAppuis)
        AjouteLigneFrmt(Lines, "SpanLengths", ConvertListDecimalToString(myBeam.LongueurTravee))

        Dim listTypTravee(myBeam.TypTravee.Count - 1) As Integer

        For i As Integer = 0 To listTypTravee.Count - 1
            listTypTravee(i) = myBeam.TypTravee(i)
        Next

        AjouteLigneFrmt(Lines, "SpanType", ConvertListIntegerToString(listTypTravee))
        AjouteLigneFrmt(Lines, "Propping", myBeam.TypeEtaiement)
        AjouteLigneFrmt(Lines, "LeftPropping", myBeam.lEtaisConsoleGauche)
        AjouteLigneFrmt(Lines, "RightPropping", myBeam.lEtaisConsoleDroite)
        AjouteLigneFrmt(Lines, "NbPropping", myBeam.NbEtaiement)

        AjouteLigneFrmt(Lines, "TypeRestraint", myBeam.TypeMaintien)

        AjouteLigneFrmt(Lines, "LSpacing", myBeam.EntraxeD1)
        AjouteLigneFrmt(Lines, "RSpacing", myBeam.EntraxeD2)
        AjouteLigneFrmt(Lines, "LDSlOp", myBeam.DistanceDsl1)
        AjouteLigneFrmt(Lines, "RDSlOp", myBeam.DistanceDsl2)

        AjouteLigneFrmt(Lines, "Intermediate", myBeam.lIntermediaire)
        AjouteLigneFrmt(Lines, "lDefSpan", myBeam.lDefautPortee)
        AjouteLigneFrmt(Lines, "lDefEncas", myBeam.lDefautEnrobage)
        AjouteLigneFrmt(Lines, "lDefProp", myBeam.lDefautEtaiement)
        AjouteLigneFrmt(Lines, "lDefSlab", myBeam.lDefautDalle)

        If myBeam.lMixte Then
            AjouteLigneFrmt(Lines, "lAutoDesign", myBeam.lAutomaticDesign)
            AjouteLigneFrmt(Lines, "ZoneNb", ConvertListIntegerToString(myBeam.NombreZones))
            AjouteLigneFrmt(Lines, "ZoneLengths", ConvertListDecimalToString(myBeam.LongueurZone))
            AjouteLigneFrmt(Lines, "ZoneSpacings", ConvertListDecimalToString(myBeam.EspacementZone))
            AjouteLigneFrmt(Lines, "ZoneTroughSp", ConvertListIntegerToString(myBeam.Espacement_Bac_TransZone))
            AjouteLigneFrmt(Lines, "ZoneNy", ConvertListIntegerToString(myBeam.NombreGoujonsTransv))
        End If

        AjouteLigneFrmt(Lines, "ULSlComb", ConvertListBooleanToString(myBeam.lCombELU))
        AjouteLigneFrmt(Lines, "ULSCoef", ConvertListDecimalToString(myBeam.CoefCombELU))
        AjouteLigneFrmt(Lines, "SLSlComb", ConvertListBooleanToString(myBeam.lCombELS))
        AjouteLigneFrmt(Lines, "SLSCoef", ConvertListDecimalToString(myBeam.CoefCombELS))
        AjouteLigneFrmt(Lines, "FLSlComb", ConvertListBooleanToString(myBeam.lCombFeu))
        AjouteLigneFrmt(Lines, "FLSCoef", ConvertListDecimalToString(myBeam.CoefCombFeu))

        AjouteLigneFrmt(Lines, "CULSlComb", ConvertListBooleanToString(myBeam.lCombELCSRules))
        AjouteLigneFrmt(Lines, "CULSCoef", ConvertListDecimalToString(myBeam.CoefCombELCU))
        AjouteLigneFrmt(Lines, "CSLSlComb", ConvertListBooleanToString(myBeam.lCombELCSRules))
        AjouteLigneFrmt(Lines, "CSLSCoef", ConvertListDecimalToString(myBeam.CoefCombELCS))

        Lines.Add("")

    End Sub

    Private Sub SaveFileBlocMaintienBac(MaintienBac As cls_MaintienBac, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif au maitien par le bac
        '-------------------------------------------------------------------------------------

        With MaintienBac

            Lines.Add("BLOCK " & BkMAINTIENBAC)

            AjouteLigneFrmt(Lines, "NT", CStr(.nt))
            AjouteLigneFrmt(Lines, "M", CStr(.m))
            AjouteLigneFrmt(Lines, "Transition", (.Transition))
            AjouteLigneFrmt(Lines, "FixNervuresMod", (.FixNervuresMod))
            AjouteLigneFrmt(Lines, "FixnervuresTyp", (.FixnervuresTyp))
            AjouteLigneFrmt(Lines, "EC", (.ec))
            AjouteLigneFrmt(Lines, "FixCoutureType", (.FixCoutureType))
            AjouteLigneFrmt(Lines, "lMaintienBac", (.lMaintienBac))
            AjouteLigneFrmt(Lines, "lTheta", (.lTheta))

        End With
    End Sub

    Private Sub SaveFileBlocDalle(myDalle As cls_Dalle, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif à la dalle
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkDALLE)

        With myDalle

            AjouteLigneFrmt(Lines, "Type", .type)
            AjouteLigneFrmt(Lines, "td", .Ep_td)
            AjouteLigneFrmt(Lines, "th", .Ep_th)
            AjouteLigneFrmt(Lines, "preDalle_ep", .preDalle_ep)
            AjouteLigneFrmt(Lines, "preDalle_tjoi", .preDalle_tjoint)
        End With

        Lines.Add("")

    End Sub

    Private Sub SaveFileBlocBac(myBac As cls_Bac, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif au bac
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkBAC)

        With myBac
            AjouteLigneFrmt(Lines, "Etiquette", .Etiquette)
            AjouteLigneFrmt(Lines, "Producteur", .Producteur)
            AjouteLigneFrmt(Lines, "lDatabase", .lDatabase)

            AjouteLigneFrmt(Lines, "h_rs", .h_rs)
            AjouteLigneFrmt(Lines, "h_p", .Hp)
            AjouteLigneFrmt(Lines, "b_b", .Bb)
            AjouteLigneFrmt(Lines, "b_t", .Bt)
            AjouteLigneFrmt(Lines, "e_p", .Ep)
            AjouteLigneFrmt(Lines, "tp", .Tp)
            AjouteLigneFrmt(Lines, "orientation", .Orientation)
            AjouteLigneFrmt(Lines, "msurf", .msurf)
            AjouteLigneFrmt(Lines, "fyp", .fyp)
            AjouteLigneFrmt(Lines, "LargeurModule", .LargeurModule)
            AjouteLigneFrmt(Lines, "Ieff", .Ieff)
            AjouteLigneFrmt(Lines, "lPreperce", .lPreperce)
            AjouteLigneFrmt(Lines, "AppuiT", .AppuiT)
            AjouteLigneFrmt(Lines, "Appuil", .AppuiL)
        End With

        Lines.Add("")

    End Sub

    Private Sub SaveFileBlocCofraDalle(myCofraDal As cls_Cofradal, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif au plancher préfabrique Cofradal
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkCOFRADAL)

        With myCofraDal
            AjouteLigneFrmt(Lines, "Nom", .nom)
            AjouteLigneFrmt(Lines, "Dp", .dp)
            AjouteLigneFrmt(Lines, "Msurf", .msurf)
            AjouteLigneFrmt(Lines, "lCustom", .lCustom)
        End With

        Lines.Add("")

    End Sub

    Private Sub SaveFileBlocOptions(myParam As cls_OptionsCalcul, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif aux options de calcul
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkOPTIONS)

        With myParam
            AjouteLigneFrmt(Lines, "RH", .RH)
            AjouteLigneFrmt(Lines, "Norme", .Norme)
            AjouteLigneFrmt(Lines, "EtaW", .EtaW)
            AjouteLigneFrmt(Lines, "lLarEffSimp", .lLargeurEfficaceSimplifiee)
            AjouteLigneFrmt(Lines, "lCompArma", .lCompressionArma)
            AjouteLigneFrmt(Lines, "dMaxNodes", .dMaxNodes)
            AjouteLigneFrmt(Lines, "nbMinNodesTr", .nbMinNodesTravee)
            AjouteLigneFrmt(Lines, "nbMinNodesCo", .nbMinNodesConsole)
            AjouteLigneFrmt(Lines, "epsSH", .EpsilonSH)

            AjouteLigneFrmt(Lines, "lRetraitEnr", .lRetraitEnrobage)
            AjouteLigneFrmt(Lines, "ArmaYoung", .ArmaYoung)
            AjouteLigneFrmt(Lines, "Gravite", .GraviteG)
            AjouteLigneFrmt(Lines, "PsiLPerm", .PsiLPermanent)
            AjouteLigneFrmt(Lines, "PsiLRetrait", .PsiLRetrait)

            AjouteLigneFrmt(Lines, "AgeT0G1_0", .AgeT0G1(0))
            AjouteLigneFrmt(Lines, "AgeT0G1_1", .AgeT0G1(1))
            AjouteLigneFrmt(Lines, "AgeT0G2_0", .AgeT0G2(0))
            AjouteLigneFrmt(Lines, "AgeT0G2_1", .AgeT0G2(1))
            AjouteLigneFrmt(Lines, "AgeT0SH_0", .AgeT0SH(0))
            AjouteLigneFrmt(Lines, "AgeT0SH_1", .AgeT0SH(1))

            AjouteLigneFrmt(Lines, "AgeTCalc", .AgeT)

            AjouteLigneFrmt(Lines, "lElasticDesign", .lElasticDesignVM)
            AjouteLigneFrmt(Lines, "lMaitFissure", .lMaitriseFissuration)

        End With

        Lines.Add("")

    End Sub

    Private Sub SaveFileBlocProfile(myProfil As cls_ProfilA, mySteel As cls_Acier, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif au profilé acier
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkPROFILA)

        With myProfil
            AjouteLigneFrmt(Lines, "Serie", .Gamme)
            AjouteLigneFrmt(Lines, "Name", .NomProfile)
            AjouteLigneFrmt(Lines, "Type", .typeProfileAcier)
            AjouteLigneFrmt(Lines, "ha", .ha)
            AjouteLigneFrmt(Lines, "hb", .hb)
            AjouteLigneFrmt(Lines, "bfs", .Bfs)
            AjouteLigneFrmt(Lines, "tfs", .Tfs)
            AjouteLigneFrmt(Lines, "bfi", .Bfi)
            AjouteLigneFrmt(Lines, "tfi", .Tfi)
            AjouteLigneFrmt(Lines, "rcs", .Rcs)
            AjouteLigneFrmt(Lines, "rci", .Rci)
            AjouteLigneFrmt(Lines, "tw", .Tw)
            AjouteLigneFrmt(Lines, "Welda", .aW)
            AjouteLigneFrmt(Lines, "PlatB", .Plat_b)
            AjouteLigneFrmt(Lines, "PlatT", .Plat_t)
            'AjouteLigneFrmt(Lines, "IndStand", ConvertListShortToString(.IndStandart))
            'If .IndDeliv IsNot Nothing Then
            '    AjouteLigneFrmt(Lines, "IndDeliv", ConvertListShortToString(.IndDeliv))
            'End If
        End With

        With mySteel
            AjouteLigneFrmt(Lines, "Grade", .Nuance)
            AjouteLigneFrmt(Lines, "QualitY", .Qualite)
            AjouteLigneFrmt(Lines, "Reduction", .Reduction)
            AjouteLigneFrmt(Lines, "Standart", .NormeProduit)
        End With

        Lines.Add("")

    End Sub

    Private Sub SaveFileBlocSection(mySection As cls_Section, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif à la section
        '-------------------------------------------------------------------------------------

        Lines.Add("BLOCK " & BkSECTION)

        With mySection
            AjouteLigneFrmt(Lines, "Name", .Nom)
            AjouteLigneFrmt(Lines, "SlabDefined", .lDalleBeton)
            AjouteLigneFrmt(Lines, "Database", .lDatabase)
            AjouteLigneFrmt(Lines, "Type", .TypeSection)
            AjouteLigneFrmt(Lines, "UserDefined", .lUser)
            AjouteLigneFrmt(Lines, "f_y_fs", .f_y.fs)
            AjouteLigneFrmt(Lines, "f_y_w", .f_y.w)
            AjouteLigneFrmt(Lines, "f_y_fi", .f_y.fi)
        End With
        Lines.Add("")

    End Sub




    Private Sub SaveFileBlocGamma(myGamma As cls_Gamma, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif aux paramètres Gamma
        '-------------------------------------------------------------------------------------

        With myGamma
            Lines.Add("BLOCK " & BkGAMMA)

            AjouteLigneFrmt(Lines, "GammaM0", .GammaM0)
            AjouteLigneFrmt(Lines, "GammaM1", .GammaM1)
            AjouteLigneFrmt(Lines, "GammaM2", .GammaM2)
            AjouteLigneFrmt(Lines, "GammaC", .GammaC)
            AjouteLigneFrmt(Lines, "GammaVs", .GammaVs)
            AjouteLigneFrmt(Lines, "GammaVc", .GammaVc)
            AjouteLigneFrmt(Lines, "lGammaVuni", .lGammaV_unique)

            AjouteLigneFrmt(Lines, "GammaS", .GammaS)
            AjouteLigneFrmt(Lines, "GammaP", .GammaP)

            AjouteLigneFrmt(Lines, "GammaM_fi", .GammaM_fi)
            AjouteLigneFrmt(Lines, "GammaS_fi", .GammaS_fi)
            AjouteLigneFrmt(Lines, "GammaV_fi", .GammaV_fi)
            AjouteLigneFrmt(Lines, "GammaC_fi", .GammaC_fi)

            Lines.Add("")

            AjouteLigneFrmt(Lines, "GammaG_sup", .GammaG_sup)
            AjouteLigneFrmt(Lines, "GammaG_inf", .GammaG_inf)
            AjouteLigneFrmt(Lines, "GammaQ", .GammaQ)
            AjouteLigneFrmt(Lines, "Psi0_Q1", .Psi0_Q1)
            AjouteLigneFrmt(Lines, "Psi1_Q1", .Psi1_Q1)
            AjouteLigneFrmt(Lines, "Psi2_Q1", .Psi2_Q1)
            AjouteLigneFrmt(Lines, "Psi0_Q2", .Psi0_Q2)
            AjouteLigneFrmt(Lines, "Psi1_Q2", .Psi1_Q2)
            AjouteLigneFrmt(Lines, "Psi2_Q2", .Psi2_Q2)

            Lines.Add("")
        End With


    End Sub

    Private Sub SaveFileBlocHivoss(myHivoss As cls_MethodHivoss, ByRef Lines As List(Of String))
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Ecriture du bloc relatif aux paramètres du calcul Hivoss
        '-------------------------------------------------------------------------------------

        With myHivoss
            Lines.Add("BLOCK " & BkHIVOSS)

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

    End Sub



#End Region

#Region " Ecriture / Lecture - Fichier "

    Public Sub SaveFile(ByRef Lines As List(Of String), ByVal version As String)
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

        Lines.Add("BLOCK " & BkINDENT)
        AjouteLigneFrmt(Lines, "Utilisateur", Me.Utilisateur)
        AjouteLigneFrmt(Lines, "Entreprise", Me.Entreprise)
        AjouteLigneFrmt(Lines, "NomProjet", Me.Nom)

        'Lines.Add("   Utilisateur   = " & Me.Utilisateur)
        'Lines.Add("   Entreprise    = " & Me.Entreprise)
        'Lines.Add("   NomProjet     = " & Me.Nom)
        Lines.Add("")

        '==[ Nuances d'acier utilisateur ]=================================================================
        Dim nuancesSave As New List(Of String)


        '==[ Classe Poutre ]=================================================================
        For Each pTre As cls_Poutre In Me.Poutres

            'With pTre

            SaveFileBlocPoutre(pTre, Lines)

            '==[ Classe Maintien ]=================================================================
            For i As Integer = 0 To pTre.Maintiens.Count - 1
                For Each maint In pTre.Maintiens(i)

                    ' If maint IsNot Nothing Then
                    With maint

                        Lines.Add("BLOCK " & bkMAINTIENS)
                        AjouteLigneFrmt(Lines, "indTravee", CStr(i))
                        AjouteLigneFrmt(Lines, "xLoc", .x_Loc)
                        AjouteLigneFrmt(Lines, "lMaintSemSup", .lMaintienSemelleSup)
                        AjouteLigneFrmt(Lines, "lMaintSemInf", .lMaintienSemelleInf)

                        Lines.Add("")
                    End With
                    ' End If

                Next
            Next

            '==[ Classe maintien par le bac ]==================================================

            If pTre.lMixte Then
                SaveFileBlocMaintienBac(pTre.MaintienBac, Lines)
            End If

            '==[ Bloc Section ]=================================================================



            '==[ Bloc ProfilA ]=================================================================

            SaveFileBlocProfile(pTre.Section.ProfilA, pTre.Section.Acier, Lines)

            '==[ Classe Section ]=================================================================
            With pTre.Section



            End With


            '==[ Classe Enrobage Partiel ProfilA ]=================================================================
            With .Enrobage
                Lines.Add("BLOCK " & BkENROBAGE)

                AjouteLigneFrmt(Lines, "lArmaConst", .lArmaConst)
                AjouteLigneFrmt(Lines, "ConstPhi", .ConstPhi)
                AjouteLigneFrmt(Lines, "Ratio_bc", .Ratio_bc)
                AjouteLigneFrmt(Lines, "Etrier_Type", .Etriers_Type)
                AjouteLigneFrmt(Lines, "Etrier_Phi", .Etriers_Phi)
                AjouteLigneFrmt(Lines, "Etrier_CY", .Etriers_EnrobageY)
                AjouteLigneFrmt(Lines, "Etrier_CZ", .Etriers_EnrobageZ)

                Lines.Add("")

                '==[ Classe Armature Enrobage Partiel ProfilA ]=================================================================
                Lines.Add("BLOCK " & BkARMAENROB)

                For i As Integer = 0 To .LitArma.Length - 1
                    With .LitArma(i)
                        AjouteLigneFrmt(Lines, "indLit", CStr(i))
                        AjouteLigneFrmt(Lines, "PhiExt", .PhiExt)
                        AjouteLigneFrmt(Lines, "NbExt", .NbExt)
                        AjouteLigneFrmt(Lines, "lActExt", .lActiveExt)
                        AjouteLigneFrmt(Lines, "PhiMil", .PhiMil)
                        AjouteLigneFrmt(Lines, "NbMil", .NbMil)
                        AjouteLigneFrmt(Lines, "PhiInt", .PhiInt)
                        AjouteLigneFrmt(Lines, "NbInt", .NbInt)
                        AjouteLigneFrmt(Lines, "lActInt", .lActiveInt)
                        If i = 1 Then
                            AjouteLigneFrmt(Lines, "zPosRatio", .zPosRatio)
                        End If

                        Lines.Add("")
                    End With
                Next
            End With

            '==[ Classe Acier Armature Enrobage Partiel ProfilA ]=================================================================
            Lines.Add("BLOCK " & BkACIERARMAE)
                    With .AcierArmatures
                        AjouteLigneFrmt(Lines, "Classe", .Classe)
                        AjouteLigneFrmt(Lines, "FsK", .FsK)
                        AjouteLigneFrmt(Lines, "Es", .Es)

                        Lines.Add("")
                    End With

                    '==[ Classe Béton Enrobage Partiel ProfilA ]=================================================================
                    Lines.Add("BLOCK " & BkBETONENROB)
                    With .Beton

                        AjouteLigneFrmt(Lines, "Leger", .lLeger)
                        AjouteLigneFrmt(Lines, "Classe", .Classe)
                        AjouteLigneFrmt(Lines, "RhoC", .RhoC)
                        AjouteLigneFrmt(Lines, "Fck", .Fck)
                        AjouteLigneFrmt(Lines, "Fcm", .Fcm)
                        AjouteLigneFrmt(Lines, "Fctm", .Fctm)
                        AjouteLigneFrmt(Lines, "Ecm", .Ecm)
                        AjouteLigneFrmt(Lines, "lCrackLimit", .lCrackingLimitation)
                        AjouteLigneFrmt(Lines, "wk_max", .wk_max)

                        Lines.Add("")
                    End With
                End With
            End With

            '==[ Classe Dalle ]=================================================================

            SaveFileBlocDalle(pTre.Dalle, Lines)

            '==[ Classe Bac Dalle ]=================================================================

            Select Case pTre.Dalle.type
                Case cls_Dalle.Enum_TypeDalle.Mixte
                    SaveFileBlocBac(pTre.Dalle.Bac, Lines)
                Case cls_Dalle.Enum_TypeDalle.PlancherPrefabrique
                    SaveFileBlocCofraDalle(pTre.Dalle.Cofradal, Lines)
            End Select

            With pTre.Dalle


                '==[ Classe Béton Dalle ]=================================================================
                With .beton
                    Lines.Add("BLOCK " & BkBETONDALLE)

                    AjouteLigneFrmt(Lines, "Leger", .lLeger)
                    AjouteLigneFrmt(Lines, "Classe", .Classe)
                    AjouteLigneFrmt(Lines, "RhoC", .RhoC)
                    AjouteLigneFrmt(Lines, "Fck", .Fck)
                    AjouteLigneFrmt(Lines, "Fcm", .Fcm)
                    AjouteLigneFrmt(Lines, "Fctm", .Fctm)
                    AjouteLigneFrmt(Lines, "Ecm", .Ecm)
                    AjouteLigneFrmt(Lines, "lCrackLimit", .lCrackingLimitation)
                    AjouteLigneFrmt(Lines, "wk_max", .wk_max)

                    Lines.Add("")
                End With

                '==[ Classe Armature Dalle ]=================================================================
                For Each arma_longi As Cls_Armatures_Longi In .LitArma
                    With arma_longi
                        If pTre.Dalle.LitArma.IndexOf(arma_longi) = 0 Or (.lActive And pTre.Dalle.LitArma.IndexOf(arma_longi) = 1) Then
                            Lines.Add("BLOCK " & bkarmadalle)
                            Lines.Add("   indLit         =  " & pTre.Dalle.LitArma.IndexOf(arma_longi))
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
                    Lines.Add("BLOCK " & BkACIERARMADALLE)

                    Lines.Add("   Classe         =  " & .Classe)
                    Lines.Add("   Fsk            =  " & .FsK)
                    Lines.Add("   Es             =  " & .Es)
                    Lines.Add("")
                End With

                '==[ Classe Connecteur Goujon Dalle ]=================================================================
                With .Goujons
                    Lines.Add("BLOCK " & BkGOUJON)

                    Lines.Add("   nom            =  " & .nom)
                    Lines.Add("   hsc            =  " & .hsc)
                    Lines.Add("   d              =  " & .d)
                    Lines.Add("   fy              =  " & .Fy)
                    Lines.Add("   fu              =  " & .Fu)
                    Lines.Add("")
                End With

                '==[ Classe Connecteur Armature Dalle ]=================================================================
                With .ConnecteurArmature
                    Lines.Add("BLOCK " & BkARMATRANS)

                    Lines.Add("   ds            =  " & .ds)

                    Lines.Add("")
                End With

                '==[ Classe Acier Connecteur Armature Dalle ]=================================================================
                With .ConnecteurArmature.Acier
                    Lines.Add("BLOCK ACIER_CONNECTEUR_DALLE_ARMATURE")

                    Lines.Add("   Classe         =  " & .Classe)
                    Lines.Add("   Fsk            =  " & .FsK)
                    Lines.Add("   Es             =  " & .Es)
                    Lines.Add("")
                End With

            End With

            '==[ Classe Options Calculs ]=================================================================

            SaveFileBlocOptions(pTre.Param, Lines)

            '==[ Classe Gamma ]=================================================================

            SaveFileBlocGamma(pTre.Param.Gamma, Lines)

            '==[ Classe Hivoss ]=================================================================

            SaveFileBlocHivoss(pTre.Hivoss, Lines)

            '==[ Classe ChargementU ]=================================================================
            For Each elemnts As KeyValuePair(Of String, cls_ChargementUtilisateur) In pTre.ChargesU
                For i As Integer = pTre.IndicePremiereTravee To pTre.IndiceDerniereTravee
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

        Next


    End Sub

    Private Sub AjouteLigneFrmt(ByRef Lines As List(Of String), Cle As String, Argument As String, Optional MARGE As Integer = 3)
        '-------------------------------------------------------------------------------------------------------------------------------
        '   03/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------------------------------
        '   Ajout d'une ligne Cle + argument, avec un format commun
        '-------------------------------------------------------------------------------------------------------------------------------
        '   Lines       [E/S] : Lignes dans lesquelles on ajoute une nouvelle ligne
        '   Cle         [E] :   Clé
        '   Argument    [E] :
        '-------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration 

        'Const MARGE As Integer = 3
        Const EGAL As String = "= "
        Const posEGAL As Integer = 25

        Dim ChaineCle As String

        '--( Préparation

        ChaineCle = LSet(Space(MARGE) & Cle, posEGAL)
        Lines.Add(ChaineCle & EGAL & Argument)

    End Sub

    Public Shared Sub ReadLineFile(ByVal FileName As String, ByRef Lines As List(Of String), ByRef lOK As Boolean)
        '---------------------------------------------------------------------------------------------------------
        '   04/09/24 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des lignes d'un fichier projet
        '---------------------------------------------------------------------------------------------------------
        '   FileName    [E] :   Nom du fichier
        '   Lines       [S] :   Liste de lignes contenues dans le fichier
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim myLines As Cls_LinesOfFile

        '--( Récupération

        Try
            If File.Exists(FileName) Then
                myLines = New Cls_LinesOfFile(FileName)
                lOK = True

                Lines = myLines.Lines
                'For i = 0 To myLines.LineNumber - 1
                '    Lines.Add(myLines.Lines(i))
                'Next

            Else
                lOK = False
                MsgBox("Fichier n'existe pas | File not exist : " & FileName, MsgBoxStyle.Critical, "Cls_Projet/RecuperationFile")

            End If
        Catch ex As Exception
            lOK = False
        End Try

    End Sub

    Public Shared Sub AnalyseFichier(Lines As List(Of String), ByRef myBeams As List(Of String), ByRef IndLigneBeams As List(Of Integer))
        '---------------------------------------------------------------------------------------------------------
        '   04/09/24 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des lignes d'un fichier projet
        '---------------------------------------------------------------------------------------------------------
        '   Lines           [E] :   Lignes contenues dans le fichier
        '   myBeams         [S] :   Nom des poutres contenues dans le fichier
        '   IndLigneBeams   [S] :   Indice de la ligne ou débute chaque poutre
        '---------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim i As Integer
        Dim Mots() As String = Nothing
        Dim nbMots As Integer
        Dim MotCle As String = ""
        Dim Argument As String = ""
        Dim lChercheN As Boolean = False
        Dim iBlocB As Integer

        '--( Traitement

        myBeams.Clear()
        IndLigneBeams.Clear()

        For i = 0 To Lines.Count - 1

            DecomposeLine(Lines(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(6, Mots(1).Length)).ToUpper
                If nbMots > 1 Then Argument = Mots(2).ToUpper

                If MotCle = "BLOCK" And Argument = BkPOUTRE Then

                    lChercheN = True
                    iBlocB = i

                ElseIf lChercheN And MotCle = "BEAIDE" Then

                    If nbMots > 1 Then
                        Dim iStar As Integer = Lines(i).IndexOf("=")
                        If iStar > 0 Then
                            myBeams.Add(Lines(i).Substring(iStar + 1).Trim)

                        End If
                    Else
                        myBeams.Add("Beam no " & CStr(i))
                    End If
                    IndLigneBeams.Add(iBlocB)

                ElseIf lChercheN And MotCle = "BLOCK" And Not (Argument = BkPOUTRE) Then

                    lChercheN = False

                End If
            End If

        Next

    End Sub

    Public Sub AddBeamFromFile(Lines As List(Of String), NomCasChargesU() As String, iStart As Integer, iLineStop As Integer)
        '---------------------------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Ajout d'une nouvelle poutre dans le projet à partir d'un fichier
        '---------------------------------------------------------------------------------------------------------
        '   Lines           [E] :   Lignes contenues dans le fichier
        '   NomCasChargesU  [E] :   Nom des cas de charges utilisateur dans la langue interface
        '   iLineStop       [E] :   Indice de la ligne à ne pas dépasser
        '   iStart          [E] :   Indice de la ligne à partir de laquelle on charge la poutre
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Blocs As New List(Of String)
        Dim indBlocs As New List(Of Integer)

        '--( Identification des blocs à traiter

        RepereLignesBlocPoutre(Lines, Blocs, indBlocs, iStart)

        '--( Chargement de la poutre

        ChargePoutreDeFile(Lines, Blocs, indBlocs, NomCasChargesU, iLineStop)

    End Sub

    Private Sub RepereLignesBlocPoutre(Lines As List(Of String), ByRef Bloc As List(Of String), ByRef indBloc As List(Of Integer), Optional iStart As Integer = 0)
        '---------------------------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Identification des lignes d'une poutre contenant de bloc
        '---------------------------------------------------------------------------------------------------------
        '   Lignes      [E] :   Lignes extraites du fichier
        '   Bloc        [S] :   Liste des blocs définissant la poutre
        '   indBloc     [S] :   Indice des lignes définissant les blocs
        '   iStart      [E] :   Indice de la ligne d'où on démarre l'analyse
        '---------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iLine, nbLines As Integer

        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Dim lCont As Integer = True

        Dim lPoutre As Boolean = False

        '--( Initilisation

        nbLines = Lines.Count
        iLine = iStart - 1

        Do While lCont And iLine < nbLines - 1
            iLine += 1

            DecomposeLine(Lines(iLine), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                If MotCle = "BLOC" Then
                    If (Mots(2).ToUpper = BkPOUTRE) Then
                        If lPoutre Then
                            lCont = False
                        Else
                            lPoutre = True
                        End If

                        If lPoutre Then
                            Bloc.Add(BkPOUTRE)
                            indBloc.Add(iLine)
                        End If

                    Else
                        Bloc.Add(Mots(2))
                        indBloc.Add(iLine)
                    End If
                End If
            End If
        Loop

    End Sub

    Private Sub ChargePoutreDeFile(Lines As List(Of String), Blocs As List(Of String), indBlocs As List(Of Integer),
                                   NomCasChargesU() As String, iLineStop As Integer)
        '---------------------------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Ajout d'une nouvelle poutre dans le projet à partir d'un fichier
        '---------------------------------------------------------------------------------------------------------
        '   Lines           [E] :   Lignes contenues dans le fichier
        '   Blocs           [E] :   Liste des blocs à traiter
        '   indBlocs        [E] :   Indices de la poisition des blocs dans les lignes de fichier
        '   NomCasChargesU  [E] :   Nom des cas de charges utilisateur dans la langue interface
        '   iLineStop       [E] :   Indice de la ligne à ne pas dépasser
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iBloc, nbBlocs As Integer
        Dim iFin As Integer

        '--( Initialisation

        Me.Poutres.Add(New cls_Poutre(NomCasChargesU))
        nbBlocs = Blocs.Count

        '--( Boucle sur les blocs

        For iBloc = 0 To nbBlocs - 1

            If iBloc = nbBlocs - 1 Then iFin = iLineStop Else iFin = indBlocs(iBloc + 1) - 1

            Select Case Blocs(iBloc)
                Case BkPOUTRE

                    ReadBlocPoutre(Me.Poutres.Last, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkMAINTIENS
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim maintien_en_cours As New cls_Maintiens
                    Dim ind_travee As Integer
                    ReadBlocMaintiens(maintien_en_cours, ind_travee, Lines, indBlocs(iBloc) + 1, iFin)
                    ptre_en_cours.Maintiens(ind_travee).Add(maintien_en_cours)

                Case BkMAINTIENBAC

                    ReadBlocMaintienParLeBac(Me.Poutres.Last.MaintienBac, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkSECTION

                    ReadBlocSection(Me.Poutres.Last.Section, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkPROFILA

                    ReadBlocProfilA(Me.Poutres.Last.Section.ProfilA, Me.Poutres.Last.Section.Acier, Lines, indBlocs(iBloc) + 1, iFin)

                'Case BkACIERP

                    'ReadBlocAcierProfilA(Me.Poutres.Last.Section.Acier, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkENROBAGE

                    ReadBlocEnrobageProfilA(Me.Poutres.Last.Section.Enrobage, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkARMAENROB

                    ReadBlocArmatureEnrobageProfilA(Me.Poutres.Last.Section.Enrobage.LitArma, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkACIERARMAE

                    ReadBlocAcierArmatureEnrobageProfilA(Me.Poutres.Last.Section.Enrobage.AcierArmatures, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkBETONENROB

                    ReadBlocBetonEnrobageProfilA(Me.Poutres.Last.Section.Enrobage.Beton, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkDALLE

                    ReadBlocDalle(Me.Poutres.Last.Dalle, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkBETONDALLE

                    ReadBlocBetonDalle(Me.Poutres.Last.Dalle.beton, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkBAC

                    ReadBlocBacDalle(Me.Poutres.Last.Dalle.Bac, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkCOFRADAL

                    ReadBlocCofradal(Me.Poutres.Last.Dalle.Cofradal, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkARMADALLE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_dalle As New Cls_Armatures_Longi
                    Dim ind_travee As Integer
                    ReadBlocArmatureDalle(armature_dalle, ind_travee, Lines, indBlocs(iBloc) + 1, iFin)
                    ptre_en_cours.Dalle.LitArma(ind_travee) = armature_dalle

                Case BkACIERARMADALLE

                    ReadBlocAcierArmature(Me.Poutres.Last.Dalle.AcierArmatures, Lines, indBlocs(iBloc) + 1, iFin)

                Case Bkgoujon

                    ReadBlocConnecteurGoujonDalle(Me.Poutres.Last.Dalle.Goujons, Lines, indBlocs(iBloc) + 1, iFin)

                Case "ACIER_CONNECTEUR_DALLE_GOUJON"

                    ReadBlocAcierArmature(Me.Poutres.Last.Dalle.ConnecteurArmature.Acier, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkOPTIONS

                    ReadBlocOptionsCalculs(Me.Poutres.Last.Param, Lines, indBlocs(iBloc) + 1, iFin)

                Case BkGamma

                    ReadBlocGammaOptionsCalculs(Me.Poutres.Last.Param.Gamma, Lines, indBlocs(iBloc) + 1, iFin)

                Case "OPT_CALCULS_HIVOSS"

                    ReadBlocHivossOptionsCalculs(Me.Poutres.Last.Hivoss, Lines, indBlocs(iBloc) + 1, iFin)

                Case "CHGTU_QSURF"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim QSurf_en_cours As Decimal
                    Dim cle_dic As String = ""
                    Dim ind_travee As Integer
                    ReadBlocQSurf(QSurf_en_cours, cle_dic, ind_travee, Lines, indBlocs(iBloc) + 1, iFin)
                    ptre_en_cours.ChargesU(cle_dic).QSurf(ind_travee) = QSurf_en_cours

                Case "CHGTU_FORCE"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim force_en_cours As New cls_Force
                    Dim cle_dic As String = ""
                    Dim ind_travee As Integer
                    ReadBlocForce(force_en_cours, cle_dic, ind_travee, Lines, indBlocs(iBloc) + 1, iFin)
                    ptre_en_cours.ChargesU(cle_dic).Forces(ind_travee).Add(force_en_cours)

                Case "CHGTU_FREPAR"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim frepart_en_cours As New cls_ForceRepartie
                    Dim cle_dic As String = ""
                    Dim ind_travee As Integer
                    ReadBlocFRepartie(frepart_en_cours, cle_dic, ind_travee, Lines, indBlocs(iBloc) + 1, iFin)
                    ptre_en_cours.ChargesU(cle_dic).FReparties(ind_travee).Add(frepart_en_cours)

            End Select

        Next

    End Sub

    Public Sub ReadFile(ByVal FileName As String, ByVal str_warning_file As String, NomCasChargesU() As String)
        '---------------------------------------------------------------------------------------------------------
        '   09/08/23 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Initialisation d'un projet à partir d'un fichier de données
        '---------------------------------------------------------------------------------------------------------
        '   FileName        [E] :   Nom du fichier
        '   str_warning     [E] :   Message d'avertissement
        '   NomCasChargesU  [E] :   Nom des cas de charges utilisateur dans la langue interface
        '   lFR             [E] :   Indique si les messages de plantage sont en français
        '---------------------------------------------------------------------------------------------------------

        '--> Déclaration
        Dim Lines As Cls_LinesOfFile
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Dim ListeBlocIndex As New List(Of Integer)
        Dim ListeBlocCle As New List(Of String)
        Dim IndexFin As Integer
        Dim myMsg As String = ""

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

        'If Not ListeBlocCle.Contains(BkINDENT) And Not ListeBlocCle.Contains(BkPOUTRE) _
        'And Not ListeBlocCle.Contains(BkMAINTIENS) And Not ListeBlocCle.Contains(BkMAINTIENBAC) _
        'And Not ListeBlocCle.Contains(BkSECTION) And Not ListeBlocCle.Contains(BkPROFILA) _
        'And Not ListeBlocCle.Contains(BkACIERP) And Not ListeBlocCle.Contains(BkENROBAGE) _
        'And Not ListeBlocCle.Contains(BkARMAENROB) And Not ListeBlocCle.Contains(BkACIERARMAE) And Not ListeBlocCle.Contains(BkBETONENROB) _
        'And Not ListeBlocCle.Contains(BkDALLE) And Not ListeBlocCle.Contains(BkBETONDALLE) And Not ListeBlocCle.Contains(BkBAC) _
        'And Not ListeBlocCle.Contains(BkCOFRADAL) And Not ListeBlocCle.Contains(BkARMADALLE) And Not ListeBlocCle.Contains(BkACIERARMADALLE) _
        'And Not ListeBlocCle.Contains(BkGOUJON) And Not ListeBlocCle.Contains(BkARMATRANS) _
        'And Not ListeBlocCle.Contains("ACIER_CONNECTEUR_DALLE_ARMATURE") And Not ListeBlocCle.Contains(BkOPTIONS) _
        'And Not ListeBlocCle.Contains("OPT_CALCULS_GAMMA") And Not ListeBlocCle.Contains("OPT_CALCULS_HIVOSS") And Not ListeBlocCle.Contains("CHGTU_QSURF") And Not ListeBlocCle.Contains("CHGTU_FORCE") And Not ListeBlocCle.Contains("CHGTU_FREPAR") Then 'And Not ListeBlocCle.Contains("IDENTIFICATION") And Not ListeBlocCle.Contains("SECTION") 

        '    MsgBox("Fichier corrumpu | Corrupted file", MsgBoxStyle.Critical, "Cls_Projet/LectureFile")

        'End If

        Dim oldFichier As Boolean = False

        '--> Traitement des blocks

        For i = 0 To ListeBlocIndex.Count - 1
            If i = ListeBlocIndex.Count - 1 Then IndexFin = Lines.Lines.Count - 1 Else IndexFin = ListeBlocIndex(i + 1) - 1
            Select Case ListeBlocCle(i)

                Case BkINDENT
                    ReadBloc_Identification(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)

                Case BkPOUTRE
                    Dim ptre_en_cours As New cls_Poutre(NomCasChargesU)
                    ReadBlocPoutre(ptre_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    Me.Poutres.Add(ptre_en_cours)

                Case BkMAINTIENS
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim maintien_en_cours As New cls_Maintiens
                    Dim ind_travee As Integer
                    ReadBlocMaintiens(maintien_en_cours, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Maintiens(ind_travee).Add(maintien_en_cours)

                Case BkMAINTIENBAC
                    'Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    'Dim maintien_bac As New cls_MaintienBac
                    'ReadBlocMaintienParLeBac(maintien_bac, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    'ptre_en_cours.MaintienBac = maintien_bac
                    ReadBlocMaintienParLeBac(Me.Poutres.Last.MaintienBac, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)

                Case BkSECTION
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim section_en_cours As New cls_Section
                    ReadBlocSection(section_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section = section_en_cours

                Case BkPROFILA
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim profilA_en_cours As New cls_ProfilA
                    ReadBlocProfilA(profilA_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.ProfilA = profilA_en_cours

                Case BkACIERP
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_profilA As New cls_Acier
                    ReadBlocAcierProfilA(acier_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Acier = acier_profilA

                Case BkENROBAGE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim enrobage_profilA As New cls_Enrobage_Partiel
                    ReadBlocEnrobageProfilA(enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage = enrobage_profilA

                Case BkARMAENROB
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_enrobage_profilA(2) As cls_ArmatureEnrobage

                    For j As Integer = 0 To armature_enrobage_profilA.Length - 1
                        armature_enrobage_profilA(j) = New cls_ArmatureEnrobage
                    Next

                    ReadBlocArmatureEnrobageProfilA(armature_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage.LitArma = armature_enrobage_profilA

                Case BkACIERARMAE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_enrobage_profilA As New cls_AcierArmature
                    ReadBlocAcierArmatureEnrobageProfilA(acier_armature_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage.AcierArmatures = acier_armature_enrobage_profilA

                Case BkBETONENROB
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim beton_enrobage_profilA As New cls_Beton
                    ReadBlocBetonEnrobageProfilA(beton_enrobage_profilA, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Section.Enrobage.Beton = beton_enrobage_profilA

                Case BkDALLE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim dalle_en_cours As New cls_Dalle
                    ReadBlocDalle(dalle_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle = dalle_en_cours

                Case BkBETONDALLE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim beton_dalle As New cls_Beton
                    ReadBlocBetonDalle(beton_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.beton = beton_dalle

                Case BkBAC
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim bac_en_cours As New cls_Bac
                    ReadBlocBacDalle(bac_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Bac = bac_en_cours

                Case BkCOFRADAL
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim cofradal_en_cours As New cls_Cofradal
                    ReadBlocCofradal(cofradal_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Cofradal = cofradal_en_cours

                Case BkARMADALLE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim armature_dalle As New Cls_Armatures_Longi
                    Dim ind_travee As Integer
                    ReadBlocArmatureDalle(armature_dalle, ind_travee, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.LitArma(ind_travee) = armature_dalle

                Case BkACIERARMADALLE
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_dalle As New cls_AcierArmature
                    ReadBlocAcierArmature(acier_armature_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.AcierArmatures = acier_armature_dalle

                Case BkGOUJON
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim connecteur_dalle As New cls_GoujonSoude
                    ReadBlocConnecteurGoujonDalle(connecteur_dalle, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.Goujons = connecteur_dalle

                Case BkARMATRANS
                    MsgBox("Non couvert " & BkARMATRANS)

                Case "ACIER_CONNECTEUR_DALLE_GOUJON"
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim acier_armature_connecteur As New cls_AcierArmature
                    ReadBlocAcierArmature(acier_armature_connecteur, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Dalle.ConnecteurArmature.Acier = acier_armature_connecteur

                Case BkOPTIONS
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim opt_calculs_en_cours As New cls_OptionsCalcul
                    ReadBlocOptionsCalculs(opt_calculs_en_cours, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param = opt_calculs_en_cours

                Case BkGAMMA
                    Dim ptre_en_cours As cls_Poutre = Me.Poutres.Last
                    Dim gamma_opt_calculs As New cls_Gamma
                    ReadBlocGammaOptionsCalculs(gamma_opt_calculs, Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    ptre_en_cours.Param.Gamma = gamma_opt_calculs

                Case BkHIVOSS
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

    Private Sub ReadBlocPoutre(poutre_en_cours As cls_Poutre, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Lecture du bloc POUTRE
        '-------------------------------------------------------------------------------------
        '   myProfil    [S] :   Profilé à definir
        '   mySteel     [S] :   Acier à definir
        '   Lignes      [E] :   lignes extraites du fichier de données
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '-------------------------------------------------------------------------------------

        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 6

        '--> Traitement
        With poutre_en_cours
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "BEAIDE"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .BeamID = Lignes(i).Substring(iFirst - 1)
                            Else
                                .BeamID = ""
                            End If
                        Case "COMMEN"
                            If nbMots >= 2 Then
                                iFirst = InStr(Lignes(i), Mots(2))
                                .Commentaire = Lignes(i).Substring(iFirst - 1)
                            Else
                                .Commentaire = ""
                            End If
                        'Case "TYPESECTIO" : .TypeSection = Mots(nbMots)
                        Case "LCANTI" : .lTraveeConsoleGauche = Mots(nbMots)
                        Case "RCANTI" : .lTraveeConsoleDroite = Mots(nbMots)
                        Case "LCONTS" : .lDalleContinueGauche = Mots(nbMots)
                        Case "RCONTS" : .lDalleContinueDroite = Mots(nbMots)
                        Case "LSLABO" : .lTremieGauche = Mots(nbMots)
                        Case "RSLABO" : .lTremieDroite = Mots(nbMots)

                        Case "NBSPAN" : .NombreTraveesDeuxAppuis = TraiteReal(Mots(nbMots))
                        Case "SPANLE" : .LongueurTravee = ConvertStringToListDecimal(Mots(nbMots))

                        Case "SPANTY" : .TypTravee = ConvertStringToListInteger(Mots(nbMots))

                        Case "PROPPI" : .TypeEtaiement = TraiteReal(Mots(nbMots))
                        Case "LPROPP" : .lEtaisConsoleGauche = Mots(nbMots)
                        Case "RPROPP" : .lEtaisConsoleDroite = Mots(nbMots)
                        Case "NBPROP" : .NbEtaiement = Mots(nbMots)

                        Case "TYPERE" : .TypeMaintien = Mots(nbMots)
                        Case "LSPACI" : .EntraxeD1 = TraiteReal(Mots(nbMots))
                        Case "RSPACI" : .EntraxeD2 = TraiteReal(Mots(nbMots))
                        Case "LDSLOP" : .DistanceDsl1 = TraiteReal(Mots(nbMots))
                        Case "RDSPOP" : .DistanceDsl2 = TraiteReal(Mots(nbMots))

                        Case "INTERM" : .lIntermediaire = Mots(nbMots)
                        Case "LDEFSP" : .lDefautPortee = Mots(nbMots)
                        Case "LDEFEN" : .lDefautEnrobage = Mots(nbMots)
                        Case "LDEFPR" : .lDefautEtaiement = Mots(nbMots)
                        Case "LDEFSL" : .lDefautDalle = Mots(nbMots)

                        Case "LAUTOD" : .lAutomaticDesign = Mots(nbMots)
                        Case "ZONENB" : .NombreZones = ConvertStringToListInteger(Mots(nbMots))
                        Case "ZONELE" : .LongueurZone = ConvertStringToListDecimalDim2(Mots(nbMots))
                        Case "ZONESP" : .EspacementZone = ConvertStringToListDecimalDim2(Mots(nbMots))
                        Case "ZONETR" : .Espacement_Bac_TransZone = ConvertStringToListIntegerDim2(Mots(nbMots))
                        Case "ZONENY" : .NombreGoujonsTransv = ConvertStringToListIntegerDim2(Mots(nbMots))

                        Case "ULSLCO" : .lCombELU = ConvertStringToListBoolean(Mots(nbMots))
                        Case "ULSCOE" : .CoefCombELU = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "SLSLCO" : .lCombELS = ConvertStringToListBoolean(Mots(nbMots))
                        Case "SLSCOE" : .CoefCombELS = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "FLSLCO" : .lCombFeu = ConvertStringToListBoolean(Mots(nbMots))
                        Case "FLSCOE" : .CoefCombFeu = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "CULSLC" : .lCombELCURules = ConvertStringToListBoolean(Mots(nbMots))
                        Case "CULSCO" : .CoefCombELCU = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
                        Case "CSLSLC" : .lCombELCSRules = ConvertStringToListBoolean(Mots(nbMots))
                        Case "CSLSCO" : .CoefCombELCS = ConvertStringToListDecimalDim2Bis(Mots(nbMots))
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

    Private Sub ReadBlocSection(section As cls_Section, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Lecture du bloc Section
        '-------------------------------------------------------------------------------------
        '   section     [S] :   Section à definir
        '   Lignes      [E] :   lignes extraites du fichier de données
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '-------------------------------------------------------------------------------------


        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 6

        '--> Traitement
        With section
            For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "NAME" : .Nom = Mots(nbMots)
                        Case "SLABDE" : .lDalleBeton = Mots(nbMots)
                        Case "DATABA" : .lDatabase = Mots(nbMots)
                        Case "TYPE" : .TypeSection = Mots(nbMots)
                        Case "USERDE" : .lUser = Mots(nbMots)
                        Case "F_Y_FS" : .f_y.fs = TraiteReal(Mots(nbMots))
                        Case "F_Y_W" : .f_y.w = TraiteReal(Mots(nbMots))
                        Case "F_Y_FI" : .f_y.fi = TraiteReal(Mots(nbMots))
                        Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

        End With

    End Sub

    Private Sub ReadBlocProfilA(ByRef myProfil As cls_ProfilA, ByRef mySteel As cls_Acier,
                                ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '-------------------------------------------------------------------------------------
        '   04/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Lecture du bloc PROFILE
        '-------------------------------------------------------------------------------------
        '   myProfil    [S] :   Profilé à definir
        '   mySteel     [S] :   Acier à definir
        '   Lignes      [E] :   lignes extraites du fichier de données
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '-------------------------------------------------------------------------------------

        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i, iFirst As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 6

        '--> Traitement

        For i = Index0 To IndexFin
                DecomposeLine(Lignes(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                    Select Case MotCle
                        Case "SERIE"
                            iFirst = InStr(Lignes(i), Mots(2))
                        myProfil.Gamme = Lignes(i).Substring(iFirst - 1)
                    Case "NAME"
                            iFirst = InStr(Lignes(i), Mots(2))
                        myProfil.NomProfile = Lignes(i).Substring(iFirst - 1)
                    Case "HA" : myProfil.ha = TraiteReal(Mots(nbMots))
                    Case "HB" : myProfil.hb = TraiteReal(Mots(nbMots))
                    Case "BFS" : myProfil.Bfs = TraiteReal(Mots(nbMots))
                    Case "TFS" : myProfil.Tfs = TraiteReal(Mots(nbMots))
                    Case "BFI" : myProfil.Bfi = TraiteReal(Mots(nbMots))
                    Case "TFI" : myProfil.Tfi = TraiteReal(Mots(nbMots))
                    Case "RCS" : myProfil.Rcs = TraiteReal(Mots(nbMots))
                    Case "RCI" : myProfil.Rci = TraiteReal(Mots(nbMots))
                    Case "TW" : myProfil.Tw = TraiteReal(Mots(nbMots))
                    Case "WELDA" : myProfil.aW = TraiteReal(Mots(nbMots))
                    Case "TYPE" : myProfil.typeProfileAcier = Mots(nbMots)
                    Case "PLATB" : myProfil.Plat_b = TraiteReal(Mots(nbMots))
                    Case "PLATT" : myProfil.Plat_t = TraiteReal(Mots(nbMots))
                            'Case "INDDELIV" : .IndDeliv = ConvertStringToListShort(Mots(nbMots))
                            'Case "INDSTAND" : .IndStandart = ConvertStringToListShort(Mots(nbMots))

                    Case "GRADE" : mySteel.Nuance = Mots(2)
                    Case "QUALIT" : mySteel.Qualite = Mots(2)
                    Case "REDUCT" : mySteel.Reduction = Mots(2)
                    Case "STANDA" : mySteel.NormeProduit = Mots(2)

                    Case Else : MsgBox("Le mot clé/The keyword " & MotCle & " n'est pas traité/isn't treated")
                    End Select
                End If
            Next

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
                        'Case "BEFF" : .Beff = TraiteReal(Mots(nbMots))
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
    Private Sub ReadBlocAcierArmature(acier_armature_dalle As cls_AcierArmature, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
    ''' Lecture du bloc Connecteur_Dalle_Goujon
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocConnecteurGoujonDalle(connecteur_dalle As cls_GoujonSoude, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
    ''' Lecture du bloc Connecteur_Dalle_Armature
    ''' </summary>
    ''' <param name="Lignes">Liste de lignes contenant les paramètres</param>
    ''' <param name="Index0">indice du début de la lecture</param>
    ''' <param name="IndexFin">indice de la fin de la lecture</param>
    Private Sub ReadBlocConnecteurArmatureDalle(connecteur_dalle As Cls_ConnecteurArmature, ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
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
                        Case "DS" : .ds = TraiteReal(Mots(nbMots))
                            'Case "DHS" : .dhs = TraiteReal(Mots(nbMots))
                            'Case "AHV" : .ahv = TraiteReal(Mots(nbMots))
                            'Case "LS" : .Ls = TraiteReal(Mots(nbMots))

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
                MotCle = Mots(1).Substring(0, Math.Min(11, Mots(1).Length)).ToUpper


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
                        Case "GAMMAS_FI" : .GammaS_fi = TraiteReal(Mots(nbMots))
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
