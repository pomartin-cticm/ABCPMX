Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports PMXMoteur2

Module Mod_NoteCalcul

#Region "   Déclarations "

    '--> Tabulation
    Private Const TABW1 As String = "\TW1"
    Private Const TABW2 As String = "\TW2"


    Private Const TABVAR As String = "\T10"
    Private Const TABVAR1 As String = "\T15"
    Private Const TABVAR2 As String = "\T20"
    Private Const TABVAR3 As String = "\T25"

    Private Const TABAFF As String = " :\T50"
    Private Const TABAFF2 As String = " :\T35"
    Private Const TABAFF3 As String = " :\T20"

    '--> Taille tableau
    Private Const LC1 As Decimal = 30        'taille colonne 1
    Private Const LC1_2 As Decimal = 20        'taille colonne 1
    Private Const LC2 As Decimal = 15        'taille colonne 2
    Private Const LC2_3 As Decimal = 12        'taille colonne entre 2 et 3
    Private Const LC3 As Decimal = 10        'taille colonne 3
    Private Const LC4 As Decimal = 5         'taille colonne 4
    Private Const HLIGNE As Decimal = 1.5  'taille ligne
    Private Const HLIGNE2 As Decimal = 1.7  'taille ligne

    'Nom de l'imprimante virtuelle pour créer un PDF
    Public Const PrintPDFName As String = "Microsoft Print to PDF"

    '--------------------------------------------------------------------------
    '   Paramètres de la fenêtre NDC - Ajout BD - 05/02/20
    '--------------------------------------------------------------------------
    '--> initialisé avec des valeurs par défaut
    '--> permet de garder la même interface entre deux ouvertures de la NDC
    '--------------------------------------------------------------------------

    'Largeur de la fenêtre NDC
    Public WidthForm As Integer = 750
    'Hauteur de la fenêtre NDC
    Public HeightForm As Integer = SystemInformation.WorkingArea.Height - 10
    'Position de la fenêtre NDC
    Public LocationForm As Point = New Point(SystemInformation.WorkingArea.Width / 2 - WidthForm / 2, SystemInformation.WorkingArea.Height / 2 - HeightForm / 2)
    'Vrai si volet de navigation activé
    Public lNavPane As Boolean = True
    'Largeur du volet de navigation
    Public WidthNavPane As Integer = 200
    'Vrai si fenêre maximisé
    Public lMaximised As Boolean = False
    'Valeur du zoom
    Public ZoomValue As Integer = 100

#End Region

#Region "   Variables "

    Private Bloc As New Dictionary(Of String, String)

    Private ReadOnly IndTableau As Integer = 0
    Private ReadOnly IndFigure As Integer = 0
    Private nbLignes As Decimal = 0
    Private Const MAXLIGNEPPAG As Integer = 56

    Private lChapitreOutOfScope As Boolean = False

#End Region

#Region "==>GESTION D'ENSEMBLE<================================================= "

    Public Sub AAA_EditionNOTEdeCALCUL(ByVal lEdite As Boolean)
        '----------------------------------------------------------------------------------------------
        '
        '   17/01/08 :  Création - Version 1.00 - POM
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Routine générale pilotant la Création et l'Edition de la Note de Calcul
        '
        '----------------------------------------------------------------------------------------------
        '
        '                       Ou si elle est deja ouverte (lCreation=False)
        '
        '----------------------------------------------------------------------------------------------
        '
        '   lEdite      [E] :   Indique si on ouvre la fenetre ou pas
        '
        '----------------------------------------------------------------------------------------------

        '--[ Initialisations

        MyNote = New Cls_Rapport("Arial", 1.5, 3, 3)

        '--[ Création de la Note

        AAA_GenereNOTEdeCALCUL(MyProjet)

        '--[ Edition de la Note dans l'Editeur

        If lEdite Then
            '--> Ouverture de la fenêtre
            Frm_NoteCalcul.ShowDialog()
            Frm_NoteCalcul.Dispose()
            '--> Liberation de la note
            MyNote.Dispose()
        End If


    End Sub

    Public Sub AAA_GenereNOTEdeCALCUL(ByVal MyPrjt As cls_Projet)
        '----------------------------------------------------------------------------------------------
        '
        '   17/01/08 :  Création - Version 1.00 - POM
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Routine générale pilotant la Création et l'Edition de la Note de Calcul
        '
        '----------------------------------------------------------------------------------------------
        '
        '   prjt        [E] :   Projet traité
        '
        '----------------------------------------------------------------------------------------------

        'Dim lControleOK As Boolean

        lChapitreOutOfScope = False

        '--[ Chargement des blocs langues

        InitialiseBlocNDC()

        '--[ Initialisation

        MyNote.Clear()

        'Initialiser les indices
        For I = 0 To 2
            MyNote.IndTitre(I) = 0
        Next

        If MyPrjt.Entreprise.Trim = "" Then
            MyNote.EntetePrincipal = MyPrjt.Utilisateur
        Else
            MyNote.EntetePrincipal = MyPrjt.Entreprise & " - " & MyPrjt.Utilisateur
        End If
        MyNote.EnteteSecond = MyPrjt.Nom

        MyNote.EtiquetteLigne(0, 0) = Bloc("USER")
        MyNote.EtiquetteLigne(1, 0) = Bloc("SOCIETE")
        MyNote.EtiquetteLigne(2, 0) = Bloc("PROJET")

        Const DPTS As String = ":  "
        MyNote.EtiquetteLigne(0, 1) = DPTS & MyPrjt.Utilisateur
        MyNote.EtiquetteLigne(1, 1) = DPTS & MyPrjt.Entreprise
        MyNote.EtiquetteLigne(2, 1) = DPTS & MyPrjt.Nom

        MyNote.FootNote = Bloc("FOOTNOTE")

        '--|=========================================
        '--| PAGE DE GARDE
        '--|=========================================

        EditionPageDeGarde(MyPrjt.Nom)

        '--|=========================================
        '--| PARAMETRES
        '--|=========================================
        '--[ Paramètres

        EditionParametres(MyPrjt.Poutres(MyPrjt.IndEnCours))

    End Sub

    Private Sub InitialiseBlocNDC()
        '---------------------------------------------------------------------------------------------------
        '
        '   28/02/09 :  Création - Version 1.00 B5 - POM
        '
        '---------------------------------------------------------------------------------------------------
        '
        '   Initilisation des blocs de la NdC dans la langue d'édition de la Note
        '
        '---------------------------------------------------------------------------------------------------

        Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_MAIN")
        BlocLine.CreationBloc(Bloc)

    End Sub

#End Region

#Region " Edition des paramètres "

    Private Sub EditionParametres(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres d'une poutre
        '----------------------------------------------------------------------------------------------

        '--[ Déclarations

        Dim lOut As Boolean = False

        '--[ Paramètres de base

        EditionParametresBase(MyBeam)

        '--[ Poutre hors domaine d'application

        'lOut = MyBeam.EstDansDomaineApplication
        If lOut Then EditionHorsDomaine(MyBeam)

        '--[ Section

        EditionParametresSection(MyBeam)

        '--[ Enrobage partiel

        If MyBeam.lEnrobage Then EditionParametresEnrobagePartiel(MyBeam)

        '--[ Dalle

        EditionParametresSlab(MyBeam)

        '--[ Maintiens latéraux

        EditionParametresMaintiens(MyBeam)

        '--[ Chargements

        EditionParametresChargement(MyBeam)

        '--[ Coefficients gamma

        EditionParametresCoefGamma(MyBeam)

        '--[ Coefficients Combinaisons

        EditionParametresCombinaisons(MyBeam)

    End Sub

    Private Sub EditionParametresBase(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base d'une poutre
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(1, Bloc("PARAMETERS"))

        '--[ Avertissement si Mode Expert

        If LogicielOptions.lExpert Then
            AddLigneNDC(TABW1 & "\G" & Bloc("WEXPERT") & "\g")
            AddLigneNDC(TABW1 & "\G" & Bloc("WEXPERT2") & "\g")
        End If

        '--[ Paramètres Généraux

        AddTitreNdC(2, Bloc("MAINP"))

        AddTitreNdC(3, Bloc("LONGIPARAMETERS"))

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.Acier
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOBEAM") & "\g")
            Case cls_Section.Enum_TypeSection.AcierEnrobage
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOBEAMPARTENCAS") & "\g")
            Case cls_Section.Enum_TypeSection.Mixte
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("COMPOBEAM") & "\g")
            Case cls_Section.Enum_TypeSection.MixteEnrobage
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("COMPOBEAMPARTENCAS") & "\g")
            Case cls_Section.Enum_TypeSection.SFB
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOSFB") & "\g")
            Case cls_Section.Enum_TypeSection.SFBmixte
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("COMPOSFB") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_A
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOIFB_A") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_Amixte
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("COMPOIFB_A") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_B
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOIFB_B") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_Bmixte
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("COMPOIFB_B") & "\g")
            Case cls_Section.Enum_TypeSection.SAB
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOSAB") & "\g")
            Case cls_Section.Enum_TypeSection.SABmixte
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("COMPOSAB") & "\g")
        End Select

        AddLigneNDC(TABW2 & Bloc("LENGHTBEAM") & TABAFF2 & "L\-tot\= = " & GetStringInUnit(MyBeam.LongueurTotale, Enu_TypeVariable.Longueur, 4, 2, True))
        AddLigneNDC(TABW2 & Bloc("NBTOTSPAN") & TABAFF2 & GetStringInUnit(MyBeam.NbTravees, Enu_TypeVariable.SansType, 2, 0, True))


        ' -->Tableau récapitulatif de la poutre 
        If MyBeam.NbTravees > 1 Then
            AddLigneNDC(TABW2 & Bloc("BEAMCHAR"))
            SauteLigne()
            AddLigneNDC("\TABLEAU 18")
            InitialiseLigne(5, HLIGNE, True)
            AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, "i")
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("TYPE_BEAM"))
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LENGHT") & "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LEFTSUPPORT"))
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("RIGHTSUPPORT"))

            Dim strTypTravee As String

            For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
                InitialiseLigne(5, HLIGNE, True)
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, i)
                Select Case MyBeam.TypTravee(i)
                    Case cls_Poutre.EnuTypeTravee.ConsoleGauche
                        strTypTravee = Bloc("LEFTCANT")
                    Case cls_Poutre.EnuTypeTravee.DeuxAppuis
                        strTypTravee = Bloc("SPAN")
                    Case cls_Poutre.EnuTypeTravee.ConsoleDroite
                        strTypTravee = Bloc("RIGHTCANT")
                End Select
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, strTypTravee)
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, MyBeam.LongueurTravee(i))
                Select Case MyBeam.TypTravee(i)
                    Case cls_Poutre.EnuTypeTravee.ConsoleGauche
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("FREE"))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("PINNED"))
                    Case cls_Poutre.EnuTypeTravee.DeuxAppuis
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("PINNED"))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("PINNED"))
                    Case cls_Poutre.EnuTypeTravee.ConsoleDroite
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("PINNED"))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("FREE"))
                End Select

            Next

            FinTableau()

        Else
            AddLigneNDC(TABW2 & Bloc("RIGHTSUPPORT") & TABAFF2 & Bloc("PINNED"))
            AddLigneNDC(TABW2 & Bloc("LEFTSUPPORT") & TABAFF2 & Bloc("PINNED"))

        End If

        'Ajout dessin de la poutre en cours
        AddLigneNDC("\IMG PORTEE 2 95 13 NoCadre")
        SauteLigne()

        '--[ Position de la poutre
        AddTitreNdC(3, Bloc("TRANSPARAMETERS"))
        If MyBeam.lIntermediaire Then
            AddLigneNDC(TABW2 & Bloc("BEAMPOSITION") & TABAFF2 & Bloc("INTERMEDIATEBEAM"))
        Else
            AddLigneNDC(TABW2 & Bloc("BEAMPOSITION") & TABAFF2 & Bloc("EDGEBEAM"))
        End If

        If MyBeam.lMixte And (MyBeam.lTremieGauche Or MyBeam.lTremieDroite) Then SauteLigne()

        AddLigneNDC(TABW2 & Bloc("LEFTSPACING") & TABAFF2 & "d\-1\= = " & GetStringInUnit(MyBeam.EntraxeD1, Enu_TypeVariable.Longueur, 4, 2, True))
        If MyBeam.lMixte Then
            If MyBeam.lTremieGauche Then
                AddLigneNDC(TABW2 & Bloc("ISLEFTOPENING") & TABAFF2 & Bloc("YES"))
                AddLigneNDC(TABW2 & Bloc("DISTLEFTOPENING") & TABAFF2 & "d\-sl,1\= = " & GetStringInUnit(MyBeam.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, True))
                'AddLigneNDC(TABW2 & Bloc("WIDTHLEFTOPENING") & TABAFF2 & GetStringInUnit(MyBeam.EntraxeD1 - 2 * MyBeam.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, True))
            Else
                AddLigneNDC(TABW2 & Bloc("ISLEFTOPENING") & TABAFF2 & Bloc("NO"))
            End If
        End If

        If MyBeam.lMixte And (MyBeam.lTremieGauche Or MyBeam.lTremieDroite) Then SauteLigne()

        AddLigneNDC(TABW2 & Bloc("RIGHTSPACING") & TABAFF2 & "d\-2\= = " & GetStringInUnit(MyBeam.EntraxeD2, Enu_TypeVariable.Longueur, 4, 2, True))
        If MyBeam.lMixte Then
            If MyBeam.lTremieDroite Then
                AddLigneNDC(TABW2 & Bloc("ISRIGHTOPENING") & TABAFF2 & Bloc("YES"))
                AddLigneNDC(TABW2 & Bloc("DISTRIGHTOPENING") & TABAFF2 & "d\-sl,2\= = " & GetStringInUnit(MyBeam.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, True))
                'AddLigneNDC(TABW2 & Bloc("WIDTHRIGHTOPENING") & TABAFF2 & GetStringInUnit(MyBeam.EntraxeD2 - 2 * MyBeam.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, True))
            Else
                AddLigneNDC(TABW2 & Bloc("ISRIGHTOPENING") & TABAFF2 & Bloc("NO"))
            End If
        End If

        'Ajout dessin de la position de la poutre

        If MyBeam.lMixte And (MyBeam.lTremieGauche Or MyBeam.lTremieDroite) Then SauteLigne()

        If MyBeam.lIntermediaire Then
            AddLigneNDC("\IMG PORTEE_COUPE 10 80 15 NoCadre")
        Else
            AddLigneNDC("\IMG PORTEE_COUPE 20 60 10 NoCadre")
        End If
    End Sub

    Private Sub EditionHorsDomaine(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Traitement d'une poutre hors du domaine d'application
        '----------------------------------------------------------------------------------------------

        ' A COMPLETER PLUS TARD
    End Sub

    Private Sub EditionParametresSection(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base d'une section
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("SPROFILE"))
        AddTitreNdC(3, Bloc("GENERAL"))
        Select Case MyBeam.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                AddLigneNDC(TABW2 & Bloc("STYPE") & TABAFF & Bloc("HOTROLLED"))
                AddLigneNDC(TABW2 & Bloc("SNAME") & TABAFF & MyBeam.Section.ProfilA.NomProfile)
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                AddLigneNDC(TABW2 & Bloc("STYPE") & TABAFF & Bloc("PRS_BI_SYM"))
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                AddLigneNDC(TABW2 & Bloc("STYPE") & TABAFF & Bloc("PRS_MONO_SYM"))
        End Select

        SauteLigne()

        AddTitreNdC(3, Bloc("CHAR_PROFILE"))

        AddLigneNDC(TABW2 & Bloc("HS_PROFILE") & TABAFF & "h\-s\= = " & GetStringInUnit(MyBeam.Section.ProfilA.ha, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("BFS_PROFILE") & TABAFF & "b\-fs\= = " & GetStringInUnit(MyBeam.Section.ProfilA.b_fs, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("TFS_PROFILE") & TABAFF & "t\-fs\= = " & GetStringInUnit(MyBeam.Section.ProfilA.t_fs, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("BFI_PROFILE") & TABAFF & "b\-fi\= = " & GetStringInUnit(MyBeam.Section.ProfilA.b_fi, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("TFI_PROFILE") & TABAFF & "t\-fi\= = " & GetStringInUnit(MyBeam.Section.ProfilA.t_fi, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("HW_PROFILE") & TABAFF & "h\-w\= = " & GetStringInUnit(MyBeam.Section.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("DW_PROFILE") & TABAFF & "d\-w\= = " & GetStringInUnit(MyBeam.Section.ProfilA.HauteurAmeDw, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("TW_PROFILE") & TABAFF & "t\-w\= = " & GetStringInUnit(MyBeam.Section.ProfilA.t_w, Enu_TypeVariable.Dimension, 4, 0, True))

        If MyBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then
            AddLigneNDC(TABW2 & Bloc("RCS_PROFILE") & TABAFF & "r\-cs\= = " & GetStringInUnit(MyBeam.Section.ProfilA.r_cs, Enu_TypeVariable.Dimension, 4, 0, True))
            AddLigneNDC(TABW2 & Bloc("RCI_PROFILE") & TABAFF & "r\-ci\= = " & GetStringInUnit(MyBeam.Section.ProfilA.r_ci, Enu_TypeVariable.Dimension, 4, 0, True))
        Else
            AddLigneNDC(TABW2 & Bloc("AWELD_PROFILE") & TABAFF & "a = " & GetStringInUnit(MyBeam.Section.ProfilA.a, Enu_TypeVariable.Dimension, 4, 0, True))
        End If

        SauteLigne()

        AddLigneNDC(TABW2 & Bloc("A_PROFILE") & TABAFF & "A = " & GetStringInUnit(MyBeam.Section.ProfilA.Aire, Enu_TypeVariable.Dimension, 4, 0, True) & "\+2\=")
        AddLigneNDC(TABW2 & Bloc("AV_PROFILE") & TABAFF & "A\-v\= = " & GetStringInUnit(MyBeam.Section.ProfilA.AireAv, Enu_TypeVariable.Dimension, 4, 0, True) & "\+2\=")
        AddLigneNDC(TABW2 & Bloc("IY_PROFILE") & TABAFF & "I\-y\= = " & GetStringInUnit(MyBeam.Section.ProfilA.InertieY, Enu_TypeVariable.Dimension, 4, 0, True) & "\+4\=")
        AddLigneNDC(TABW2 & Bloc("IZ_PROFILE") & TABAFF & "I\-z\= = " & GetStringInUnit(MyBeam.Section.ProfilA.InertieZ, Enu_TypeVariable.Dimension, 4, 0, True) & "\+4\=")
        AddLigneNDC(TABW2 & Bloc("WEL_PROFILE") & TABAFF & "W\-el\= = " & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWelY, Enu_TypeVariable.Dimension, 4, 0, True) & "\+3\=")
        AddLigneNDC(TABW2 & Bloc("WPL_PROFILE") & TABAFF & "W\-pl\= = " & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWplY, Enu_TypeVariable.Dimension, 4, 0, True) & "\+3\=")
        AddLigneNDC(TABW2 & Bloc("IT_PROFILE") & TABAFF & "I\-t\= = " & GetStringInUnit(MyBeam.Section.ProfilA.InertieT, Enu_TypeVariable.Dimension, 4, 0, True) & "\+4\=")
        AddLigneNDC(TABW2 & Bloc("IW_PROFILE") & TABAFF & "I\-w\= = " & GetStringInUnit(MyBeam.Section.ProfilA.InertieW, Enu_TypeVariable.Dimension, 4, 0, True) & "\+6\=")

        AddLigneNDC("\IMG PROFIL_ACIER 10 80 30 NoCadre")

        If nbLignes + 30 > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(3, Bloc("MATERIAL_PROFILE"))
        AddLigneNDC(TABW2 & Bloc("E_PROFILE") & TABAFF & "E = " & GetStringInUnit(MyBeam.Section.Acier.EYoung, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("RHO_PROFILE") & TABAFF & "\Sr\s = " & GetStringInUnit(MyBeam.Section.Acier.Rho, Enu_TypeVariable.SansType, 4, 0, True) & "kg/m\+3\=")
        AddLigneNDC(TABW2 & Bloc("GRADE_PROFILE") & TABAFF & MyBeam.Section.Acier.Nuance)
        AddLigneNDC(TABW2 & Bloc("STANDARD_PROFILE") & TABAFF & MyBeam.Section.Acier.Reduction)
        AddLigneNDC(TABW2 & Bloc("QUALITY_PROFILE") & TABAFF & MyBeam.Section.Acier.Qualite)
        AddLigneNDC(TABW2 & Bloc("FYFS_PROFILE") & TABAFF & "f\-y,fs\= = " & GetStringInUnit(MyBeam.Section.Acier.f_y.fs, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FYW_PROFILE") & TABAFF & "f\-y,w\= = " & GetStringInUnit(MyBeam.Section.Acier.f_y.w, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FYFI_PROFILE") & TABAFF & "f\-y,fi\= = " & GetStringInUnit(MyBeam.Section.Acier.f_y.fi, Enu_TypeVariable.Contrainte, 4, 0, True))

    End Sub

    Private Sub EditionParametresEnrobagePartiel(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   28/07/23 :  Création - Version 1.00 - GUD
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base de l'enrobage partiel d'une section
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("PARTIAL_ENCASEMENT"))

        AddTitreNdC(3, Bloc("GEOMETRY_PART_ENC"))

        AddLigneNDC(TABW2 & Bloc("RATIO_BC_PART_ENC") & TABAFF & "b\-c\=/b\-f\= = " & GetStringInUnit(MyBeam.Section.enrobage_partiel.Ratio_bc, Enu_TypeVariable.SansType, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("BC_PART_ENC") & TABAFF & "b\-c\= = " & GetStringInUnit(MyBeam.Section.LargeurEnrobagePartielBc, Enu_TypeVariable.Dimension, 4, 0, True))





        AddTitreNdC(3, Bloc("CONCRETE_MATERIAL"))

        If MyBeam.Section.enrobage_partiel.Beton.Type = Cls_Beton.Enum_TypeBeton.Leger Then
            AddLigneNDC(TABW2 & Bloc("TYPE_CONCRETE") & TABAFF & Bloc("LIGHTCONCRETE"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPE") & TABAFF & Bloc("NORMALCONCRETE"))
        End If

        AddLigneNDC(TABW2 & Bloc("CLASS_CONCRETE") & TABAFF & MyBeam.Section.enrobage_partiel.Beton.Classe)
        AddLigneNDC(TABW2 & Bloc("FCK_CONCRETE") & TABAFF & "f\-ck\= = " & GetStringInUnit(MyBeam.Section.enrobage_partiel.Beton.Fck, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCM_CONCRETE") & TABAFF & "f\-cm\= = " & GetStringInUnit(MyBeam.Section.enrobage_partiel.Beton.Fcm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCTM_CONCRETE") & TABAFF & "f\-ctm\= = " & GetStringInUnit(MyBeam.Section.enrobage_partiel.Beton.Fctm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ECM_CONCRETE") & TABAFF & "E\-cm\= = " & GetStringInUnit(MyBeam.Section.enrobage_partiel.Beton.Ecm, Enu_TypeVariable.Contrainte, 4, 0, True))






        AddTitreNdC(3, Bloc("GEOM_LONGI_REINF"))

        AddLigneNDC(TABW2 & Bloc("GEOM_LONGI_REINF") & " :")
        SauteLigne()

        AddLigneNDC("\TABLEAU 18")



        InitialiseLigne(6, HLIGNE, True)


        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LAYER"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "z\-s\=")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("EXTERIOR"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("MIDDLE"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("INTERIOR"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "A\-s\=")

        For i As Integer = 0 To MyBeam.Section.enrobage_partiel.LitArma.Count - 1
            InitialiseLigne(6, HLIGNE, True)
            Select Case i
                Case 0
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("TOP"))
                Case 1
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("MIDDLE"))
                Case 2
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("BOTTOM"))
            End Select

            With MyBeam.Section.enrobage_partiel.LitArma(i)

                If .NbExt = 0 And .NbMil = 0 And .NbInt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.zPosRatio, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbExt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("NONE"))
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NbExt, Enu_TypeVariable.SansType, 4, 0, False) & " x " & GetStringInUnit(.PhiExt, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbMil = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("NONE"))
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NbMil, Enu_TypeVariable.SansType, 4, 0, False) & " x " & GetStringInUnit(.PhiMil, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbInt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("NONE"))
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NbInt, Enu_TypeVariable.SansType, 4, 0, False) & " x " & GetStringInUnit(.PhiInt, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbExt = 0 And .NbMil = 0 And .NbInt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Aire, Enu_TypeVariable.Dimension, 4, 0, True) & "\+2\=")
                End If
            End With
        Next
        FinTableau()

        AddLigneNDC(TABW2 & Bloc("WITH") & " :")
        AddLigneNDC(TABW2 & "z\-s\= : " & TABVAR1 & Bloc("ZS_REINF"))
        AddLigneNDC(TABW2 & "A\-s\= : " & TABVAR1 & Bloc("AS_REINF"))






        AddTitreNdC(3, Bloc("MATERIAL_LONGI_REINF"))
        AddLigneNDC(TABW2 & Bloc("CLASS_REINFORCEMENT") & TABAFF & MyBeam.Section.enrobage_partiel.AcierArmatures.Classe)
        AddLigneNDC(TABW2 & Bloc("FYS_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Section.enrobage_partiel.AcierArmatures.FsK, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ES_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Section.enrobage_partiel.AcierArmatures.Es, Enu_TypeVariable.Contrainte, 4, 0, True))






        AddTitreNdC(3, Bloc("GEOM_TRANSV_REINF"))
        Select Case MyBeam.Section.enrobage_partiel.Etriers_Type
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                AddLigneNDC(TABW2 & Bloc("STIRRUP_ARRANGEMENT") & TABAFF & Bloc("CLOSED_STIRRUPS"))
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                AddLigneNDC(TABW2 & Bloc("STIRRUP_ARRANGEMENT") & TABAFF & Bloc("WELDED_STIRRUPS"))
            Case Cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant
                AddLigneNDC(TABW2 & Bloc("STIRRUP_ARRANGEMENT") & TABAFF & Bloc("THROUGH_STIRRUPS"))
        End Select
        AddLigneNDC(TABW2 & Bloc("DSI_LAYERS") & TABAFF & GetStringInUnit(MyBeam.Section.enrobage_partiel.Etriers_Phi, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("HOR_COVERAGE") & TABAFF & "u\-y\= =" & GetStringInUnit(MyBeam.Section.enrobage_partiel.Etriers_EnrobageY, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("VER_COVERAGE") & TABAFF & "u\-z\= =" & GetStringInUnit(MyBeam.Section.enrobage_partiel.Etriers_EnrobageZ, Enu_TypeVariable.Dimension, 4, 0, True))


        AddLigneNDC("\IMG PARTIAL_ENCASEMENT 15 70 25 NoCadre")

    End Sub

    Private Sub EditionParametresSlab(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base d'une section
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("SLAB"))

        AddTitreNdC(3, Bloc("GEOMETRY_SLAB"))

        Select Case MyBeam.Dalle.type
            Case Cls_Dalle.Enum_TypeDalle.Pleine
                AddLigneNDC(TABW2 & Bloc("TYPE_SLAB") & TABAFF & Bloc("SOLID_SLAB"))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_SLAB") & TABAFF & "t\-d\= = " & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_HAUNCH") & TABAFF & "t\-h\= = " & GetStringInUnit(MyBeam.Dalle.t_h, Enu_TypeVariable.Dimension, 4, 0, True))
            Case Cls_Dalle.Enum_TypeDalle.Prefabriquee
                AddLigneNDC(TABW2 & Bloc("TYPE_SLAB") & TABAFF & Bloc("SOLID_SLAB_PRECAST"))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_SLAB") & TABAFF & "t\-d\= = " & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_PRECAST") & TABAFF & "t\-pc\= = " & GetStringInUnit(MyBeam.Dalle.preDalle_ep, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_JOINT") & TABAFF & "t\-j\= = " & GetStringInUnit(MyBeam.Dalle.preDalle_tjoint, Enu_TypeVariable.Dimension, 4, 0, True))
            Case Cls_Dalle.Enum_TypeDalle.Mixte
                AddLigneNDC(TABW2 & Bloc("TYPE_SLAB") & TABAFF & Bloc("COMPOSITE_SLAB"))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_SLAB") & TABAFF & "t\-d\= = " & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
        End Select





        AddTitreNdC(3, Bloc("CONCRETE_MATERIAL"))

        If MyBeam.Dalle.beton.Type = Cls_Beton.Enum_TypeBeton.Leger Then
            AddLigneNDC(TABW2 & Bloc("TYPE") & TABAFF & Bloc("LIGHTCONCRETE"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPE_CONCRETE") & TABAFF & Bloc("NORMALCONCRETE"))
        End If

        AddLigneNDC(TABW2 & Bloc("CLASS_CONCRETE") & TABAFF & MyBeam.Dalle.beton.Classe)
        AddLigneNDC(TABW2 & Bloc("FCK_CONCRETE") & TABAFF & "f\-ck\= = " & GetStringInUnit(MyBeam.Dalle.beton.Fck, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCM_CONCRETE") & TABAFF & "f\-cm\= = " & GetStringInUnit(MyBeam.Dalle.beton.Fcm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCTM_CONCRETE") & TABAFF & "f\-ctm\= = " & GetStringInUnit(MyBeam.Dalle.beton.Fctm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ECM_CONCRETE") & TABAFF & "E\-cm\= = " & GetStringInUnit(MyBeam.Dalle.beton.Ecm, Enu_TypeVariable.Contrainte, 4, 0, True))






        AddTitreNdC(3, Bloc("LONGI_REINFORCEMENTS"))

        AddLigneNDC(TABW2 & Bloc("NB_LAYERS") & TABAFF & GetStringInUnit(MyBeam.Dalle.NbLitsArmaActifs, Enu_TypeVariable.SansType, 4, 0, True))
        SauteLigne()

        AddLigneNDC("\TABLEAU 18")
        InitialiseLigne(5, HLIGNE, True)
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, "i")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "e\-si\=" & "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "d\-si\=" & "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "z\-si\=" & "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "A\-si\=" & "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & "\+2\=/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")

        For i As Integer = 0 To MyBeam.Dalle.NbLitsArmaActifs - 1
            If MyBeam.Dalle.LitArma(i).lActive Then
                InitialiseLigne(5, HLIGNE, True)
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, i + 1)
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.Dalle.LitArma(i).EspBar, Enu_TypeVariable.Dimension, 4, 0, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.Dalle.LitArma(i).PhiS, Enu_TypeVariable.Dimension, 4, 0, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.Dalle.LitArma(i).z_s, Enu_TypeVariable.Dimension, 4, 0, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.Dalle.LitArma(i).A_s, Enu_TypeVariable.Dimension, 4, 0, False))
            End If
        Next

        FinTableau()

        AddLigneNDC(TABW2 & Bloc("WITH"))
        AddLigneNDC(TABW2 & "e\-si\= : " & TABVAR1 & Bloc("ESI_LAYERS"))
        AddLigneNDC(TABW2 & "d\-si\= : " & TABVAR1 & Bloc("DSI_LAYERS"))
        AddLigneNDC(TABW2 & "z\-si\= : " & TABVAR1 & Bloc("ZSI_LAYERS"))
        AddLigneNDC(TABW2 & "A\-si\= : " & TABVAR1 & Bloc("ASI_LAYERS"))




        AddTitreNdC(3, Bloc("MATERIAL_LONGI_REINF"))
        AddLigneNDC(TABW2 & Bloc("CLASS_REINFORCEMENT") & TABAFF & MyBeam.Dalle.AcierArmatures.Classe)
        AddLigneNDC(TABW2 & Bloc("FYS_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Dalle.AcierArmatures.FsK, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ES_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Dalle.AcierArmatures.Es, Enu_TypeVariable.Contrainte, 4, 0, True))

        SauteLigne()
        AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")

        If MyBeam.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte Then
            AddTitreNdC(3, Bloc("PROFILED_STEEL_SH"))

            If MyBeam.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele Then
                AddLigneNDC(TABW2 & Bloc("ORIENTATION_SHEET") & TABAFF & Bloc("LONGITUDINAL"))
            Else
                AddLigneNDC(TABW2 & Bloc("ORIENTATION_SHEET") & TABAFF & Bloc("TRANSVERSAL"))
            End If

            If MyBeam.Dalle.Bac.lDatabase Then
                AddLigneNDC(TABW2 & Bloc("PSS_FROM") & TABAFF & Bloc("DATABASE"))
                AddLigneNDC(TABW2 & Bloc("SOCIETE") & TABAFF & MyBeam.Dalle.Bac.Producteur)
                AddLigneNDC(TABW2 & Bloc("NAME_PSS") & TABAFF & MyBeam.Dalle.Bac.Etiquette)
            Else
                AddLigneNDC(TABW2 & Bloc("PSS_FROM") & TABAFF & Bloc("DIMENSIONS_PSS"))
            End If

            SauteLigne()

            AddLigneNDC(TABW2 & Bloc("CHAR_PSS"))

            With MyBeam.Dalle.Bac

                AddLigneNDC(TABW2 & Bloc("TP_PSS") & TABAFF & "t\-p\= = " & GetStringInUnit(.tp, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("EP_PSS") & TABAFF & "e\-p\= = " & GetStringInUnit(.e_p, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("HP_PSS") & TABAFF & "h\-p\= = " & GetStringInUnit(.h_p, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("HPG_PSS") & TABAFF & "h\-pg\= = " & GetStringInUnit(.Hauteur_hpg, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("BB_PSS") & TABAFF & "b\-b\= = " & GetStringInUnit(.b_b, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("BT_PSS") & TABAFF & "b\-t\= = " & GetStringInUnit(.b_t, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("MUP_PSS") & TABAFF & "\Sm\s\-p\= = " & GetStringInUnit(.msurf, Enu_TypeVariable.SansType, 4, 0, True) & "kg/m\+2\=")
                AddLigneNDC(TABW2 & Bloc("FP_PSS") & TABAFF & "f\-p\= = " & GetStringInUnit(.fyp, Enu_TypeVariable.Contrainte, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("IPU_PSS") & TABAFF & "I\-pu\= = " & GetStringInUnit(.Ieff, Enu_TypeVariable.Dimension, 4, 0, True) & "\+4\=/m")

                If .orientation = Cls_Bac.Enum_Orientation.Parallele Then
                    If .AppuiL = Cls_Bac.EnuConfigLAppui.BacCoupe Then
                        AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("CUT_DECK"))
                    Else
                        AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("UNCUT_DECK"))
                    End If
                Else
                    Select Case .AppuiT
                        Case Cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                            AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("CONTINU_PSS"))
                            If .lPreperce Then
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("PREPUNCHED_PSS"))
                            Else
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("THROUGH_DECK_PSS"))
                            End If
                        Case Cls_Bac.EnuConfigTAppui.BetonSeulContinu
                            AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("PART_CONT_PSS"))
                            If .lPreperce Then
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("PREPUNCHED_PSS"))
                            Else
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("THROUGH_DECK_PSS"))
                            End If
                        Case Cls_Bac.EnuConfigTAppui.Discontinu
                            AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("NO_CONT_PSS"))
                    End Select

                End If

            End With
        End If

        Dim lConnection As Boolean = False

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.Acier
                'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.AcierEnrobage
                  'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.Mixte
                lConnection = True
            Case cls_Section.Enum_TypeSection.MixteEnrobage
                lConnection = True
            Case cls_Section.Enum_TypeSection.SFB
                  'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.SFBmixte
                lConnection = True
            Case cls_Section.Enum_TypeSection.IFB_A
                  'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.IFB_Amixte
                lConnection = True
            Case cls_Section.Enum_TypeSection.IFB_B
                  'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.IFB_Bmixte
                lConnection = True
            Case cls_Section.Enum_TypeSection.SAB
                  'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.SABmixte
                lConnection = True
        End Select

        If lConnection Then

            AddTitreNdC(3, Bloc("CONNECTORS"))

            With MyBeam.Dalle.Connecteur
                AddLigneNDC(TABW2 & Bloc("NAME_CONNECTORS") & TABAFF & .nom)
                AddLigneNDC(TABW2 & Bloc("HSC_CONNECTORS") & TABAFF & "h\-sc\= = " & GetStringInUnit(.hsc, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("D_CONNECTORS") & TABAFF & "d = " & GetStringInUnit(.d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("FYSC_CONNECTORS") & TABAFF & "f\-ysc\= = " & GetStringInUnit(.Fy, Enu_TypeVariable.Contrainte, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("FUSC_CONNECTORS") & TABAFF & "f\-usc\= = " & GetStringInUnit(.Fu, Enu_TypeVariable.Contrainte, 4, 0, True))
            End With

            AddTitreNdC(3, Bloc("CONNECTION_ARR"))

            With MyBeam

                If .lAutomaticDesign Then
                    AddLigneNDC(TABW2 & Bloc("AUTOMATIC_DESIGN") & TABAFF & Bloc("YES"))
                Else
                    AddLigneNDC(TABW2 & Bloc("AUTOMATIC_DESIGN") & TABAFF & Bloc("NO"))

                    SauteLigne()

                    'Calcul si on a besoin d'effectuer un saut de page au préalable
                    Dim nbLigneSautePage As Integer = nbLignes

                    nbLigneSautePage += (HLIGNE - 0.2)

                    For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
                        For j As Integer = 0 To .NombreZone(i) - 1
                            nbLigneSautePage += (HLIGNE - 0.2)
                        Next
                        If Not i = MyBeam.IndiceDerniereTravee Then
                            nbLigneSautePage += (HLIGNE - 0.2)
                        End If
                    Next
                    If nbLigneSautePage >= MAXLIGNEPPAG Then SautePage()

                    Dim lDalleMixteEtPerp As Boolean = False
                        If .Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And .Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire Then
                            lDalleMixteEtPerp = True
                        End If

                        Dim nbColonne As Integer

                        If lDalleMixteEtPerp Then
                            AddLigneNDC("\TABLEAU 15")
                            nbColonne = 5

                        Else
                            AddLigneNDC("\TABLEAU 20")
                            nbColonne = 4
                        End If

                        InitialiseLigne(nbColonne, HLIGNE, True)

                        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("SPAN"))
                        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LENGHT_ZONE") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
                        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("ROW_NUMBER"))
                        If lDalleMixteEtPerp Then
                            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("RIB_DISPOSITION"))
                        End If
                        AddCelluleFond(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LONGI_SPACING") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")



                        Dim lDerniereTravee As Boolean 'Permet de gérer la séparation par une ligne grise entre deux travées comportant des maintiens latéraux consévutives


                        For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
                            For j As Integer = 0 To .NombreZone(i) - 1

                                InitialiseLigne(nbColonne, HLIGNE, True)
                                AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, i)
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Longueur_Zone(i, j), Enu_TypeVariable.Longueur, 4, 0, False))
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NombreGoujonsTransv(i, j), Enu_TypeVariable.SansType, 4, 0, False))
                                If lDalleMixteEtPerp Then
                                    If .Espacement_Bac_Trans(i, j) = 1 Then
                                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Espacement_Bac_Trans(i, j), Enu_TypeVariable.SansType, 4, 0, False) & " " & Bloc("RIB"))
                                    Else
                                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Espacement_Bac_Trans(i, j), Enu_TypeVariable.SansType, 4, 0, False) & " " & Bloc("RIBS"))
                                        End
                                    End If
                                End If
                                AddCellule(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Espacement(i, j), Enu_TypeVariable.Dimension, 4, 0, False))


                            Next

                            If Not i = MyBeam.IndiceDerniereTravee Then
                                InitialiseLigne(1, HLIGNE, True)
                                AddCelluleFond(LC3 + (nbColonne - 2) * LC2 + LC1_2, Bordures.Tous, PositionTexteInCell.Centre, "")
                            End If

                        Next

                        FinTableau()
                    End If

            End With
        End If

    End Sub

    Private Sub EditionParametresMaintiens(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des maintiens latéraux d'une poutre
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("LATERALR"))

        Select Case MyBeam.TypeMaintien
            Case cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained
                AddLigneNDC(TABW2 & Bloc("TYPE_RES") & TABAFF & Bloc("FULLY_RES"))
            Case cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
                AddLigneNDC(TABW2 & Bloc("TYPE_RES") & TABAFF & Bloc("NO_INTER_RES"))
            Case cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
                AddLigneNDC(TABW2 & Bloc("TYPE_RES") & TABAFF & Bloc("POINT_RES"))

                SauteLigne()
                AddLigneNDC("\TABLEAU 29")
                InitialiseLigne(3, HLIGNE, True)
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("SPAN"))
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LATERALR"))

                Dim lDerniereTravee As Boolean 'Permet de gérer la séparation par une ligne grise entre deux travées comportant des maintiens latéraux consévutives


                For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
                    For Each maintien In MyBeam.Maintiens(i)
                        InitialiseLigne(3, HLIGNE, True)
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, i)
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(maintien.x_Loc, Enu_TypeVariable.Longueur, 4, 0, False))
                        If maintien.lMaintienSemelleInf And maintien.lMaintienSemelleSup Then
                            AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("BOTH_FLANGES"))
                        Else
                            If maintien.lMaintienSemelleInf Then
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("LOWER_FLANGE"))
                            Else
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, Bloc("UPPER_FLANGE"))
                            End If
                        End If
                    Next


                    lDerniereTravee = True

                    If i = MyBeam.IndiceDerniereTravee Then
                        lDerniereTravee = True
                    Else
                        For j As Integer = i + 1 To MyBeam.IndiceDerniereTravee
                            If MyBeam.Maintiens(j).Count <> 0 Then
                                lDerniereTravee = False
                                Exit For
                            End If
                        Next
                    End If

                    If Not lDerniereTravee Then
                        InitialiseLigne(1, HLIGNE, True)
                        AddCelluleFond(3 * LC2, Bordures.Tous, PositionTexteInCell.Centre, "")
                    End If

                Next

                FinTableau()

                AddLigneNDC(TABW2 & Bloc("WITH"))
                AddLigneNDC(TABW2 & "x : " & Bloc("X_LOC_RES"))




        End Select

    End Sub

    Private Sub EditionParametresCoefGamma(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------

        SautePage()

        With MyBeam.Param.Gamma

            AddTitreNdC(2, Bloc("GAMMA"))

            AddTitreNdC(3, Bloc("LOADING_FACTORS"))
            AddLigneNDC(TABVAR2 & "\Sg\s\-G,sup\= = " & .GammaG_sup)
            AddLigneNDC(TABVAR2 & "\Sg\s\-G,inf\= = " & .GammaG_inf)
            AddLigneNDC(TABVAR2 & "\Sg\s\-Q\= = " & .GammaQ)

            AddTitreNdC(3, Bloc("COMBINATION_FACTORS_Q1"))
            AddLigneNDC(TABVAR2 & "\Sy\s\-0,Q1\= = " & .Psi0_Q1)
            AddLigneNDC(TABVAR2 & "\Sy\s\-1,Q1\= = " & .Psi1_Q1)
            AddLigneNDC(TABVAR2 & "\Sy\s\-2,Q1\= = " & .Psi2_Q1)

            AddTitreNdC(3, Bloc("COMBINATION_FACTORS_Q2"))
            AddLigneNDC(TABVAR2 & "\Sy\s\-0,Q2\= = " & .Psi0_Q2)
            AddLigneNDC(TABVAR2 & "\Sy\s\-1,Q2\= = " & .Psi1_Q2)
            AddLigneNDC(TABVAR2 & "\Sy\s\-2,Q2\= = " & .Psi2_Q2)

            AddTitreNdC(3, Bloc("STEEL_RES_FACTORS"))
            AddLigneNDC(TABVAR2 & "\Sg\s\-M0\= = " & .GammaM0)
            AddLigneNDC(TABVAR2 & "\Sg\s\-M1\= = " & .GammaM1)
            AddLigneNDC(TABVAR2 & "\Sg\s\-M2\= = " & .GammaM2)

            AddTitreNdC(3, Bloc("SLAB_RES_FACTORS"))
            AddLigneNDC(TABVAR2 & "\Sg\s\-C\= = " & .GammaC)
            If .lGammaV_unique Then
                AddLigneNDC(TABVAR2 & "\Sg\s\-V\= = " & .GammaVs)
            Else
                AddLigneNDC(TABVAR2 & "\Sg\s\-Vs\= = " & .GammaVs)
                AddLigneNDC(TABVAR2 & "\Sg\s\-Vc\= = " & .GammaVc)
            End If
            AddLigneNDC(TABVAR2 & "\Sg\s\-S\= = " & .GammaS)
            AddLigneNDC(TABVAR2 & "\Sg\s\-P\= = " & .GammaP)

            AddTitreNdC(3, Bloc("FIRE_RES_FACTORS"))
            AddLigneNDC(TABVAR2 & "\Sg\s\-M,fi\= = " & .GammaM_fi)
            AddLigneNDC(TABVAR2 & "\Sg\s\-C,fi\= = " & .GammaC_fi)
            AddLigneNDC(TABVAR2 & "\Sg\s\-V,fi\= = " & .GammaV_fi)

        End With

    End Sub

    Private Sub EditionParametresChargement(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("LOADS"))

    End Sub

    Private Sub EditionParametresCombinaisons(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("COMBINATIONS"))

    End Sub

#End Region

#Region "   Page de garde "

    Private Sub EditionPageDeGarde(ByVal projectName As String)
        '---------------------------------------------------------------------------------------------
        '
        '   22/10/19 : Création BD
        '
        '---------------------------------------------------------------------------------------------
        '
        '   Edition de la page de garde
        '
        '---------------------------------------------------------------------------------------------
        '
        '   projectName  [E] :   nom du projet
        '
        '---------------------------------------------------------------------------------------------

        SautePage() 'création de la première page

        SauteLigne()
        SauteLigne()
        SauteLigne()
        SauteLigne()


        AddTitreDoc(0, Bloc("TITLE").ToUpper)
        AddTitreDoc(1, projectName)

        SauteLigne()
        SauteLigne()
        SauteLigne()
        SauteLigne()
        SauteLigne()
        SauteLigne()

        '--> Logo du CTICM
        MyNote.AddLigneInRapport("\IMG CTICM 40 20 0 NoCadre")

        MyNote.lPageGarde = True

    End Sub

#End Region

#Region " Outils pour l'édition de la note de calcul "

    Private Sub AddTitreDoc(ByVal Niveau As Integer, ByVal Titre As String)
        MyNote.SauteLigne()
        MyNote.AddLigneInRapport("\TI" & CStr(Niveau) & Titre)
        nbLignes += 2 * (2 - Niveau)
        If MyNote.NombrePages = 1 Then MyNote.lPageGarde = True
    End Sub

    Public Sub AddTitreNdC(ByVal Niveau As Integer, ByVal Titre As String)
        MyNote.SauteLigne()
        nbLignes += 2 + CSng(1 / Niveau)
        If nbLignes > MAXLIGNEPPAG Then SautePage()

        Dim Numerotation As String

        MyNote.IndTitre(Niveau - 1) += 1

        For I = Niveau To 2
            MyNote.IndTitre(I) = 0
        Next

        Numerotation = MyNote.GetNumerotationTitre(Niveau) & " - "
        MyNote.AddLigneInRapport("\W0" & CStr(Niveau) & Numerotation & Titre)

    End Sub

    ''' <summary>
    ''' Ajout d'une cellule dans un tableau (Ajout GuD: Récupéré du logiciel EcliX)
    ''' 22/01/08 :  Création - Version 1.00
    ''' </summary>
    ''' <param name="Largeur">Largeur de la cellule (comme pourcentage de la largeur du graphics)</param>
    ''' <param name="Bordure">Définit les bordures de la cellules</param>
    ''' <param name="Position">Position du texte dans la cellule</param>
    ''' <param name="Chaine">Texte à écrire dans la cellule</param>
    Private Sub AddCellule(ByVal Largeur As Decimal, ByVal Bordure As Integer,
                           ByVal Position As PositionTexteInCell, ByVal Chaine As String)

        MyNote.AddLigneInRapport("CELL " & Largeur.ToString & " " & Bordure.ToString & " " & ClePos(Position) & " :" & Chaine)

    End Sub

    ''' <summary>
    ''' Ajout d'une cellule dans un tableau avec un fond coloré (Ajout GuD: Récupéré du logiciel EcliX)
    ''' 22/01/08 :  Création - Version 1.00
    ''' </summary>
    ''' <param name="Largeur">Largeur de la cellule (comme pourcentage de la largeur du graphics)</param>
    ''' <param name="Bordure">Définit les bordures de la cellules</param>
    ''' <param name="Position">Position du texte dans la cellule</param>
    ''' <param name="Chaine">Texte à écrire dans la cellule</param>
    Private Sub AddCelluleFond(ByVal Largeur As Single, ByVal Bordure As Integer,
                               ByVal Position As PositionTexteInCell, ByVal Chaine As String)

        MyNote.AddLigneInRapport("CELF " & Largeur.ToString & " " & Bordure.ToString & " " & ClePos(Position) & " :" & Chaine)

    End Sub

    ''' <summary>
    ''' Ajout d'une cellule dans un tableau avec un fond coloré (Ajout GuD: Récupéré du logiciel EcliX)
    ''' 19/07/21 - couleur de fond modifié si valeur correcte ou non
    ''' </summary>
    ''' <param name="Largeur">Largeur de la cellule (comme pourcentage de la largeur du graphics)</param>
    ''' <param name="Bordure">Définit les bordures de la cellules</param>
    ''' <param name="Position">Position du texte dans la cellule</param>
    ''' <param name="Chaine">Texte à écrire dans la cellule</param>
    ''' <param name="correct">Indique si la valeur est correcte ou non</param>
    Private Sub AddCelluleFond(ByVal Largeur As Single, ByVal Bordure As Integer,
                               ByVal Position As PositionTexteInCell, ByVal Chaine As String, ByVal correct As Boolean)

        If correct Then
            MyNote.AddLigneInRapport("CELC " & Largeur.ToString & " " & Bordure.ToString & " " & ClePos(Position) & " :" & Chaine)
        Else 'erreur
            MyNote.AddLigneInRapport("CELE " & Largeur.ToString & " " & Bordure.ToString & " " & ClePos(Position) & " :" & Chaine)
        End If

    End Sub

    ''' <summary>
    ''' Initialise une nouvelle ligne de tableau (Ajout GuD: Récupéré du logiciel EcliX)
    ''' 22/01/08 : Création - Version 1.00
    ''' 19/07/21 : Modif BeD - si tableau peut dépasser la page alors sautPage() 
    ''' </summary>
    ''' <param name="NombreCellules">Nombre de cellules de la ligne</param>
    ''' <param name="hLigne">Hauteur des cellules de la ligne (multiplicateur de la hauteur de police standard)</param>
    ''' <param name="tableauOutPage">Indique si le tableau va déborder de la page actuelle</param>
    ''' <param name="texteTableau">Texte pour créer le tableau de la page N+1 (\Tableau 20 par exemple)</param>
    Private Sub InitialiseLigne(ByVal NombreCellules As Integer, ByVal hLigne As Single, ByVal Optional tableauOutPage As Boolean = False, ByVal Optional texteTableau As String = "")

        If tableauOutPage And nbLignes > MAXLIGNEPPAG Then
            FinTableau()                        'fin du tableau de la page N
            SautePage()
            AddLigneNDC(texteTableau, False)    'début tableau de la page N+1
        End If

        MyNote.AddLigneInRapport("LTAB " & NombreCellules.ToString & " " & hLigne.ToString)

        nbLignes += hLigne - 0.2 'ajustement des lignes

    End Sub

    Private Sub SautePage()
        MyNote.SautePage()
        nbLignes = 0
    End Sub

    Private Sub AddLigneNDC(ByVal Ligne As String, ByVal NbMiniLignes As Integer)
        '----------------------------------------------------------------------------------------
        '
        '   20/01/09 :  Création - Version 1.00 Beta 3 - POM
        '
        '----------------------------------------------------------------------------------------
        '
        '   Ajoute une ligne dans la note de calcul
        '   La ligne doit être suivie d'un nombre de ligne imposée dans la même page
        '
        '----------------------------------------------------------------------------------------
        '
        '   Ligne           [E] :   Texte à rajouter dans la note de calcul
        '   NbMiniLignes    [E] :   Nombre minimal de lignes devant figurer sous le texte,
        '                           dans la même page
        '
        '----------------------------------------------------------------------------------------

        nbLignes += 1
        If nbLignes + NbMiniLignes > MAXLIGNEPPAG Then SautePage()
        MyNote.AddLigneInRapport(Ligne)

    End Sub

    ''' <summary>
    '''  Ajoute une ligne dans la note de calcul
    '''  xx/yy/08 :  Création - Version 1.00 - POM
    '''  19/07/21 Modif BeD - gestion des lignes invisibles
    ''' </summary>
    ''' <param name="Ligne">Texte à rajouter dans la note de calcul</param>
    Public Sub AddLigneNDC(ByVal Ligne As String, ByVal Optional ajoutLigne As Boolean = True)

        If ajoutLigne Then nbLignes += 1
        If nbLignes > MAXLIGNEPPAG Then SautePage()
        MyNote.AddLigneInRapport(Ligne)

    End Sub

    Private Sub AddligneNoRetour(ByVal Ligne As String)
        MyNote.AddLigneInRapport(Ligne & "\NOS")
    End Sub

    Private Sub SauteLigne()
        MyNote.SauteLigne()
        nbLignes += 1
    End Sub

    Private Sub FinTableau()
        MyNote.AddLigneInRapport("\ETA")
    End Sub

#End Region

#Region " Autres outils "

    Public Function ChiffresRomains(ByVal Nombre As Integer) As String
        '------------------------------------------------------------------------------------------------------------------------
        '
        '   19/12/10 - Création - POM - V1.00
        '
        '------------------------------------------------------------------------------------------------------------------------

        Dim strNombre As String
        strNombre = CStr(Nombre).Trim

        Dim preNombre As New String("0", 4 - strNombre.Length)

        strNombre = preNombre & strNombre

        ' -- > Remplissage des blancs
        ' -- > Test de la validité

        ' Déclaration < --
        Dim strUnitesArabe As String = String.Empty
        Dim strDizainesArabe As String = String.Empty
        Dim strCentainesArabe As String = String.Empty
        Dim strMilliersArabe As String = String.Empty
        Dim strUnitesRomain As String = String.Empty
        Dim strDizainesRomain As String = String.Empty
        Dim strCentainesRomain As String = String.Empty
        Dim strMilliersRomain As String = String.Empty
        Dim strNombreRomain As String = String.Empty
        ' -- > Déclarations

        ' Décomposition du nombre < --
        strUnitesArabe = Right(strNombre, 1)
        strDizainesArabe = Mid(strNombre, 3, 1)
        strCentainesArabe = Mid(strNombre, 2, 1)
        strMilliersArabe = Left(strNombre, 1)
        ' -- > Décomposition du nombre

        ' Unités < --
        If strUnitesArabe = "0" Then
            strUnitesRomain = String.Empty
        End If
        If Not (Val(strUnitesArabe) > 3 OrElse Val(strUnitesArabe) < 1) Then
            strUnitesRomain = New String("I", Val(strUnitesArabe))
        End If
        If strUnitesArabe = 4 Then
            strUnitesRomain = "IV"
        End If
        If Not (Val(strUnitesArabe) > 8 OrElse Val(strUnitesArabe) < 5) Then
            On Error Resume Next
            strUnitesRomain = "V" & New String("I", (Val(strUnitesArabe) - 5))
        End If
        If Val(strUnitesArabe) = 9 Then
            strUnitesRomain = "IX"
        End If
        ' -- > Unités

        ' Dizaines < --
        If strDizainesArabe = "0" Then
            strDizainesRomain = ""
        End If
        If Not (Val(strDizainesArabe) > 3 OrElse Val(strDizainesArabe) < 1) Then
            strDizainesRomain = New String("X", Val(strDizainesArabe))
        End If
        If strDizainesArabe = 4 Then
            strDizainesRomain = "XL"
        End If
        If Not (Val(strDizainesArabe) > 8 OrElse Val(strDizainesArabe) < 5) Then
            On Error Resume Next
            strDizainesRomain = "L" & New String("X", (Val(strDizainesArabe) - 5))
        End If
        If Val(strDizainesArabe) = 9 Then
            strDizainesRomain = "XC"
        End If
        ' -- > Dizaines

        ' Centaines < --
        If strCentainesArabe = "0" Then
            strCentainesRomain = ""
        End If
        If Not (Val(strCentainesArabe) > 3 OrElse Val(strCentainesArabe) < 1) Then
            strCentainesRomain = New String("C", Val(strCentainesArabe))
        End If
        If strCentainesArabe = 4 Then
            strCentainesRomain = "CD"
        End If
        If Not (Val(strCentainesArabe) > 8 OrElse Val(strCentainesArabe) < 5) Then
            On Error Resume Next
            strCentainesRomain = "D" & New String("C", (Val(strCentainesArabe) - 5))
        End If
        If Val(strCentainesArabe) = 9 Then
            strCentainesRomain = "CM"
        End If
        ' -- > Centaines

        ' Milliers < --
        If strMilliersArabe = "0" Then
            strMilliersRomain = ""
        End If
        If Not (Val(strMilliersArabe) > 3 OrElse Val(strMilliersArabe) < 1) Then
            strMilliersRomain = New String("M", Val(strMilliersArabe))
        End If
        If strMilliersArabe > 3 Then
            MsgBox("Overflow.", vbCritical + vbOKOnly, "Error")
        End If
        ' -- > Milliers

        ' Retour < --
        strNombreRomain = strMilliersRomain & strCentainesRomain & strDizainesRomain & strUnitesRomain


        Return strNombreRomain


    End Function

#End Region

End Module
