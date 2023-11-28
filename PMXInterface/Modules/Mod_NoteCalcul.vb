Imports System.Runtime.InteropServices
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
    Private Const TABEGAL As String = "\T55 = "
    Private Const TABAFF2 As String = " :\T35"
    Private Const TABAFF3 As String = " :\T20"

    '--> Taille tableau
    '#POM : Il n'y a jamais 2 tableaux pareils => A mettre au niveau de la routine
    Private Const LC1 As Decimal = 30        'taille colonne 1
    Private Const LC1_2 As Decimal = 20        'taille colonne 1
    Private Const LC2 As Decimal = 15        'taille colonne 2
    Private Const LC2_3 As Decimal = 12        'taille colonne entre 2 et 3
    Private Const LC3 As Decimal = 10        'taille colonne 3
    Private Const LC4 As Decimal = 5         'taille colonne 4
    Private Const HLIGNE As Decimal = 1.5  'taille ligne
    Private Const HLIGNE2 As Decimal = 1.7  'taille ligne
    Const HLIGNEENTETE As Single = 1.8

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

    'Paramètre de gestion d'affichage des valeurs pour la partie Analyse
    Const formatEFFORTS As String = "0.00"

#End Region

#Region "   Variables "
    Private strRacineELU As String
    Private strRacineELS As String
    Private strRacineELF As String

    Private Bloc As New Dictionary(Of String, String)
    Private BlocSP As New Dictionary(Of String, String)
    Private BlocAnalyse As New Dictionary(Of String, String)
    Private BlocELU As New Dictionary(Of String, String)
    Private BlocELS As New Dictionary(Of String, String)
    Private BlocHiVoss As New Dictionary(Of String, String)

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

        '--[ Chargement des blocs langues (GUD: Je le déplace ici car je vais avoir besoin d'un string pour les fonctions de calcul de RDM)

        InitialiseBlocNDC()

        '--[ Initialisations

        MyNote = New Cls_Rapport("Arial", 1.5, 3, 3)
        MyProjet.Poutres(MyProjet.IndEnCours).InitialisePoidsPropres()
        MyProjet.Poutres(MyProjet.IndEnCours).Initialise_CoefficientsCombinaisons()

        strRacineELU = Bloc("ULS")
        strRacineELS = Bloc("SLS")
        strRacineELF = Bloc("FLS")

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
        '   MyPrjt      [E] :   Projet traité
        '
        '----------------------------------------------------------------------------------------------

        'Dim lControleOK As Boolean

        lChapitreOutOfScope = False

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

        '--|=========================================
        '--| PROPRIETES DES SECTIONS
        '--|=========================================

        EditionProprietesSection(MyPrjt.Poutres(MyPrjt.IndEnCours))

        '--|=========================================
        '--| ANALYSE DE LA POUTRE
        '--|=========================================

        EditionAnalysePoutre(MyPrjt.Poutres(MyPrjt.IndEnCours))

        '--|=========================================
        '--| VERIFICATION DES CRITERES ELU
        '--|=========================================

        EditionVerificationsELU(MyPrjt.Poutres(MyPrjt.IndEnCours))

        '--|=========================================
        '--| VERIFICATION DES CRITERES ELS
        '--|=========================================

        EditionVerificationsELS(MyPrjt.Poutres(MyPrjt.IndEnCours))

    End Sub

    Private Sub InitialiseBlocNDC()
        '---------------------------------------------------------------------------------------------------
        '   19/06/23 :  Création - Version 1.00 - POM
        '---------------------------------------------------------------------------------------------------
        '   Initilisation des blocs de la NdC dans la langue d'édition de la Note
        '---------------------------------------------------------------------------------------------------

        Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_MAIN")
        BlocLine.CreationBloc(Bloc)

        BlocLine = New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_SECTIONPROP")
        BlocLine.CreationBloc(BlocSP)

        BlocLine = New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_ANALYSE")
        BlocLine.CreationBloc(BlocAnalyse)

        BlocLine = New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_VERIFICATIONSULS")
        BlocLine.CreationBloc(BlocELU)

        BlocLine = New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_VERIFICATIONSSLS")
        BlocLine.CreationBloc(BlocELS)

        BlocLine = New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_HIVOSS")
        BlocLine.CreationBloc(BlocHiVoss)

    End Sub

#End Region


#Region "***Edition des paramètres***"

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

        EditionParametresDalle(MyBeam)

        '--[ Maintiens latéraux

        EditionParametresMaintiens(MyBeam)

        '--[ Etaiement

        If MyBeam.lMixte Then EditionParametresEtaiement(MyBeam)

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
            Case cls_Section.Enum_TypeSection.AcierSeul
                AddLigneNDC(TABW2 & Bloc("CSTYPE") & TABAFF2 & "\G" & Bloc("NONCOMPOBEAM") & "\g")
            Case cls_Section.Enum_TypeSection.AcierSeulEnrobage
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

        If MyBeam.lMixte Then
            AddLigneNDC(TABW2 & Bloc("TYPEOFBEAM") & TABAFF2 & Bloc("COMPOSITE"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPEOFBEAM") & TABAFF2 & Bloc("NONCOMPOSITE"))
        End If

        If MyBeam.Dalle.lMixte Then
            AddLigneNDC(TABW2 & Bloc("TYPEOFSLAB") & TABAFF2 & Bloc("COMPOSITE"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPEOFSLAB") & TABAFF2 & Bloc("NONCOMPOSITE"))
        End If

        If MyBeam.Section.lEnrobage Then
            AddLigneNDC(TABW2 & Bloc("TYPEOFSTEELSECTION") & TABAFF2 & Bloc("PARTIALLY_ENCASED"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPEOFSTEELSECTION") & TABAFF2 & Bloc("NOT_PARTIALLY_ENCASED"))
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

        Dim lLamine As Boolean = MyBeam.Section.lLamine
        Dim lPRSSym As Boolean = (MyBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym)
        Dim lSymetric As Boolean = lLamine Or lPRSSym

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

        AddLigneNDC(TABW2 & Bloc("HS_PROFILE") & TABAFF & "h\-s\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ha, Enu_TypeVariable.Dimension, 3, -1, True))
        If lLamine Then
            AddLigneNDC(TABW2 & Bloc("BF_PROFILE") & TABAFF & "b\-f\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfs, Enu_TypeVariable.Dimension, 3, -1, True))
            AddLigneNDC(TABW2 & Bloc("TF_PROFILE") & TABAFF & "t\-f\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, -1, True))
        Else
            If lPRSSym Then
                AddLigneNDC(TABW2 & Bloc("BF_PROFILE") & TABAFF & "b\-fs\==b\_fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfs, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("TF_PROFILE") & TABAFF & "t\-fs\==t\_fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 4, 1, True))
            Else
                AddLigneNDC(TABW2 & Bloc("BFS_PROFILE") & TABAFF & "b\-fs\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfs, Enu_TypeVariable.Dimension, 3, -1, True))
                AddLigneNDC(TABW2 & Bloc("TFS_PROFILE") & TABAFF & "t\-fs\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, -1, True))
                AddLigneNDC(TABW2 & Bloc("BFI_PROFILE") & TABAFF & "b\-fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfi, Enu_TypeVariable.Dimension, 3, -1, True))
                AddLigneNDC(TABW2 & Bloc("TFI_PROFILE") & TABAFF & "t\-fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfi, Enu_TypeVariable.Dimension, 3, -1, True))
            End If
        End If
        AddLigneNDC(TABW2 & Bloc("HW_PROFILE") & TABAFF & "h\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & Bloc("DW_PROFILE") & TABAFF & "d\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.HauteurAmeDw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & Bloc("TW_PROFILE") & TABAFF & "t\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, -1, True))

        If lLamine Then
            AddLigneNDC(TABW2 & Bloc("RC_PROFILE") & TABAFF & "r" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Rcs, Enu_TypeVariable.Dimension, 2, -1, True))
            'AddLigneNDC(TABW2 & Bloc("RCI_PROFILE") & TABAFF & "r\-ci\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.r_ci, Enu_TypeVariable.Dimension, 4, 0, True))
        Else
            AddLigneNDC(TABW2 & Bloc("AWELD_PROFILE") & TABAFF & "a" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.aW, Enu_TypeVariable.Dimension, 2, -1, True))
        End If

        SauteLigne()

        MyBeam.Section.ProfilA.InitialiseProprietes()

        AddLigneNDC(TABW2 & Bloc("A_PROFILE") & TABAFF & "A" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Aire, Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & Bloc("AV_PROFILE") & TABAFF & "A\-v\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.AireAv, Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & Bloc("IY_PROFILE") & TABAFF & "I\-y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieY, Enu_TypeVariable.InertieCM4, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("IZ_PROFILE") & TABAFF & "I\-z\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieZ, Enu_TypeVariable.InertieCM4, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("WEL_PROFILE") & TABAFF & "W\-el,y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWelY, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        If lLamine Then
            AddLigneNDC(TABW2 & Bloc("WEL_PROFILE") & TABAFF & "W\-el,z\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWelZ, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        End If
        AddLigneNDC(TABW2 & Bloc("WPL_PROFILE") & TABAFF & "W\-pl\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWplY, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & Bloc("IT_PROFILE") & TABAFF & "I\-t\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieT, Enu_TypeVariable.InertieCM4, 4, 2, True))
        AddLigneNDC(TABW2 & Bloc("IW_PROFILE") & TABAFF & "I\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieW, Enu_TypeVariable.InertieWCM6, 4, 0, True))

        AddLigneNDC("\IMG PROFIL_ACIER 10 80 30 NoCadre")

        If nbLignes + 15 > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(3, Bloc("MATERIAL_PROFILE"))
        AddLigneNDC(TABW2 & Bloc("E_PROFILE") & TABAFF & "E" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.EYoung, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("RHO_PROFILE") & TABAFF & "\Sr\s" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.Rho, Enu_TypeVariable.SansType, 4, 0, True) & "kg/m\+3\=")
        AddLigneNDC(TABW2 & Bloc("GRADE_PROFILE") & TABAFF & MyBeam.Section.Acier.Nuance)
        AddLigneNDC(TABW2 & Bloc("STANDARD_PROFILE") & TABAFF & MyBeam.Section.Acier.Reduction)
        AddLigneNDC(TABW2 & Bloc("QUALITY_PROFILE") & TABAFF & MyBeam.Section.Acier.Qualite)
        If lLamine Then
        Else
            AddLigneNDC(TABW2 & Bloc("FYFS_PROFILE") & TABAFF & "f\-y,fs\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.f_y.fs, Enu_TypeVariable.Contrainte, 4, 0, True))
            AddLigneNDC(TABW2 & Bloc("FYW_PROFILE") & TABAFF & "f\-y,w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.f_y.w, Enu_TypeVariable.Contrainte, 4, 0, True))
            AddLigneNDC(TABW2 & Bloc("FYFI_PROFILE") & TABAFF & "f\-y,fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.f_y.fi, Enu_TypeVariable.Contrainte, 4, 0, True))
        End If

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

        AddLigneNDC(TABW2 & Bloc("RATIO_BC_PART_ENC") & TABAFF & "b\-c\=/b\-f\= = " & GetStringInUnit(MyBeam.Section.Enrobage.Ratio_bc, Enu_TypeVariable.SansType, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("BC_PART_ENC") & TABAFF & "b\-c\= = " & GetStringInUnit(MyBeam.Section.LargeurEnrobagePartielBc, Enu_TypeVariable.Dimension, 4, 0, True))


        '--> Béton

        AddTitreNdC(3, Bloc("CONCRETE_MATERIAL"))

        'If MyBeam.Section.enrobage_partiel.Beton.Type = cls_Beton.Enum_TypeBeton.Leger Then
        If MyBeam.Section.Enrobage.Beton.lLeger Then
            AddLigneNDC(TABW2 & Bloc("TYPE_CONCRETE") & TABAFF & Bloc("LIGHTCONCRETE"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPE_CONCRETE") & TABAFF & Bloc("NORMALCONCRETE"))
        End If

        AddLigneNDC(TABW2 & Bloc("CLASS_CONCRETE") & TABAFF & MyBeam.Section.Enrobage.Beton.Classe)
        AddLigneNDC(TABW2 & Bloc("FCK_CONCRETE") & TABAFF & "f\-ck\= = " & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Fck, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCM_CONCRETE") & TABAFF & "f\-cm\= = " & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Fcm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCTM_CONCRETE") & TABAFF & "f\-ctm\= = " & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Fctm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ECM_CONCRETE") & TABAFF & "E\-cm\= = " & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Ecm, Enu_TypeVariable.Contrainte, 4, 0, True))

        '--> Armatures

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

        For i As Integer = 0 To MyBeam.Section.Enrobage.LitArma.Count - 1
            InitialiseLigne(6, HLIGNE, True)
            Select Case i
                Case 0
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("TOP"))
                Case 1
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("MIDDLE"))
                Case 2
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, Bloc("BOTTOM"))
            End Select

            With MyBeam.Section.Enrobage.LitArma(i)

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
        AddLigneNDC(TABW2 & Bloc("CLASS_REINFORCEMENT") & TABAFF & MyBeam.Section.Enrobage.AcierArmatures.Classe)
        AddLigneNDC(TABW2 & Bloc("FYS_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Section.Enrobage.AcierArmatures.FsK, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ES_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Section.Enrobage.AcierArmatures.Es, Enu_TypeVariable.Contrainte, 4, 0, True))






        AddTitreNdC(3, Bloc("GEOM_TRANSV_REINF"))
        Select Case MyBeam.Section.Enrobage.Etriers_Type
            Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                AddLigneNDC(TABW2 & Bloc("STIRRUP_ARRANGEMENT") & TABAFF & Bloc("CLOSED_STIRRUPS"))
            Case cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                AddLigneNDC(TABW2 & Bloc("STIRRUP_ARRANGEMENT") & TABAFF & Bloc("WELDED_STIRRUPS"))
            Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant
                AddLigneNDC(TABW2 & Bloc("STIRRUP_ARRANGEMENT") & TABAFF & Bloc("THROUGH_STIRRUPS"))
        End Select
        AddLigneNDC(TABW2 & Bloc("DSI_LAYERS") & TABAFF & GetStringInUnit(MyBeam.Section.Enrobage.Etriers_Phi, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("HOR_COVERAGE") & TABAFF & "u\-y\= =" & GetStringInUnit(MyBeam.Section.Enrobage.Etriers_EnrobageY, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("VER_COVERAGE") & TABAFF & "u\-z\= =" & GetStringInUnit(MyBeam.Section.Enrobage.Etriers_EnrobageZ, Enu_TypeVariable.Dimension, 4, 0, True))


        AddLigneNDC("\IMG PARTIAL_ENCASEMENT 15 70 25 NoCadre")

    End Sub

    Private Sub EditionParametresDalle(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de la dalle
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("SLAB"))

        '--> Géométrie

        AddTitreNdC(3, Bloc("GEOMETRY_SLAB"))

        Select Case MyBeam.Dalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                AddLigneNDC(TABW2 & Bloc("TYPE_SLAB") & TABAFF & Bloc("SOLID_SLAB"))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_SLAB") & TABAFF & "t\-d\= = " & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_HAUNCH") & TABAFF & "t\-h\= = " & GetStringInUnit(MyBeam.Dalle.t_h, Enu_TypeVariable.Dimension, 4, 0, True))
            Case cls_Dalle.Enum_TypeDalle.Prefabriquee
                AddLigneNDC(TABW2 & Bloc("TYPE_SLAB") & TABAFF & Bloc("SOLID_SLAB_PRECAST"))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_SLAB") & TABAFF & "t\-d\= = " & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_PRECAST") & TABAFF & "t\-pc\= = " & GetStringInUnit(MyBeam.Dalle.preDalle_ep, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_JOINT") & TABAFF & "t\-j\= = " & GetStringInUnit(MyBeam.Dalle.preDalle_tjoint, Enu_TypeVariable.Dimension, 4, 0, True))
            Case cls_Dalle.Enum_TypeDalle.Mixte
                AddLigneNDC(TABW2 & Bloc("TYPE_SLAB") & TABAFF & Bloc("COMPOSITE_SLAB"))
                AddLigneNDC(TABW2 & Bloc("THICKNESS_SLAB") & TABAFF & "t\-d\= = " & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
        End Select

        '--> Béton de la dalle

        AddTitreNdC(3, Bloc("CONCRETE_MATERIAL"))

        'If MyBeam.Dalle.beton.Type = cls_Beton.Enum_TypeBeton.Leger Then
        If MyBeam.Dalle.beton.lLeger Then
            AddLigneNDC(TABW2 & Bloc("TYPE") & TABAFF & Bloc("LIGHTCONCRETE"))
        Else
            AddLigneNDC(TABW2 & Bloc("TYPE_CONCRETE") & TABAFF & Bloc("NORMALCONCRETE"))
        End If

        AddLigneNDC(TABW2 & Bloc("CLASS_CONCRETE") & TABAFF & MyBeam.Dalle.beton.Classe)
        AddLigneNDC(TABW2 & Bloc("FCK_CONCRETE") & TABAFF & "f\-ck\= = " & GetStringInUnit(MyBeam.Dalle.beton.Fck, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCM_CONCRETE") & TABAFF & "f\-cm\= = " & GetStringInUnit(MyBeam.Dalle.beton.Fcm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("FCTM_CONCRETE") & TABAFF & "f\-ctm\= = " & GetStringInUnit(MyBeam.Dalle.beton.Fctm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & Bloc("ECM_CONCRETE") & TABAFF & "E\-cm\= = " & GetStringInUnit(MyBeam.Dalle.beton.Ecm, Enu_TypeVariable.Contrainte, 4, 0, True))

        '--> Armatures longitudinales

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
        'AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        'AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        'AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        'AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")
        'AddLigneNDC(TABW2 & "ZZZZZ GUD: AJOUTER LA FIGURE QUAND ELLE SERA TERMINEE ZZZZZ")

        '--> Bac acier

        If MyBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            AddTitreNdC(3, Bloc("PROFILED_STEEL_SH"))

            If MyBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele Then
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

                AddLigneNDC(TABW2 & Bloc("TP_PSS") & TABAFF & "t\-p\= = " & GetStringInUnit(.Tp, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("EP_PSS") & TABAFF & "e\-p\= = " & GetStringInUnit(.Ep, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("HP_PSS") & TABAFF & "h\-p\= = " & GetStringInUnit(.Hp, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("HPG_PSS") & TABAFF & "h\-pg\= = " & GetStringInUnit(.Hauteur_hpg, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("BB_PSS") & TABAFF & "b\-b\= = " & GetStringInUnit(.Bb, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("BT_PSS") & TABAFF & "b\-t\= = " & GetStringInUnit(.Bt, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("MUP_PSS") & TABAFF & "\Sm\s\-p\= = " & GetStringInUnit(.msurf, Enu_TypeVariable.SansType, 4, 0, True) & "kg/m\+2\=")
                AddLigneNDC(TABW2 & Bloc("FP_PSS") & TABAFF & "f\-p\= = " & GetStringInUnit(.fyp, Enu_TypeVariable.Contrainte, 4, 0, True))
                AddLigneNDC(TABW2 & Bloc("IPU_PSS") & TABAFF & "I\-pu\= = " & GetStringInUnit(.Ieff, Enu_TypeVariable.Dimension, 4, 0, True) & "\+4\=/m")

                If .Orientation = cls_Bac.Enum_Orientation.Parallele Then
                    If .AppuiL = cls_Bac.EnuConfigLAppui.BacCoupe Then
                        AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("CUT_DECK"))
                    Else
                        AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("UNCUT_DECK"))
                    End If
                Else
                    Select Case .AppuiT
                        Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                            AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("CONTINU_PSS"))
                            If .lPreperce Then
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("PREPUNCHED_PSS"))
                            Else
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("THROUGH_DECK_PSS"))
                            End If
                        Case cls_Bac.EnuConfigTAppui.BetonSeulContinu
                            AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("PART_CONT_PSS"))
                            If .lPreperce Then
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("PREPUNCHED_PSS"))
                            Else
                                AddLigneNDC(TABW2 & Bloc("CONNECTION_OPT") & TABAFF & Bloc("THROUGH_DECK_PSS"))
                            End If
                        Case cls_Bac.EnuConfigTAppui.Discontinu
                            AddLigneNDC(TABW2 & Bloc("CONFIG_SUPPORT_PSS") & TABAFF & Bloc("NO_CONT_PSS"))
                    End Select

                End If

            End With
        End If

        Dim lConnection As Boolean = False

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul
                'Pas de connection entre le profilé et la dalle
            Case cls_Section.Enum_TypeSection.AcierSeulEnrobage
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
                        For j As Integer = 0 To .NombreZones(i) - 1
                            nbLigneSautePage += (HLIGNE - 0.2)
                        Next
                        If Not i = MyBeam.IndiceDerniereTravee Then
                            nbLigneSautePage += (HLIGNE - 0.2)
                        End If
                    Next
                    If nbLigneSautePage >= MAXLIGNEPPAG Then SautePage()

                    Dim lDalleMixteEtPerp As Boolean = False
                    If .Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And .Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
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
                        For j As Integer = 0 To .NombreZones(i) - 1

                            InitialiseLigne(nbColonne, HLIGNE, True)
                            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, i)
                            AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.ZoneLongueur(i, j), Enu_TypeVariable.Longueur, 4, 0, False))
                            AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NombreGoujonsTransv(i, j), Enu_TypeVariable.SansType, 4, 0, False))
                            If lDalleMixteEtPerp Then
                                If .ZoneEspacement_Bac_Trans(i, j) = 1 Then
                                    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.ZoneEspacement_Bac_Trans(i, j), Enu_TypeVariable.SansType, 4, 0, False) & " " & Bloc("RIB"))
                                Else
                                    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.ZoneEspacement_Bac_Trans(i, j), Enu_TypeVariable.SansType, 4, 0, False) & " " & Bloc("RIBS"))
                                    End
                                End If
                            End If
                            AddCellule(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.ZoneEspacement(i, j), Enu_TypeVariable.Dimension, 4, 0, False))


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

    Private Sub EditionParametresEtaiement(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition de l'étaiement d'une poutre mixte
        '----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbBesoinLignes As Integer = 5     ' A ajuster

        '--> Initialisation

        If NbBesoinLignes + nbLignes > MAXLIGNEPPAG Then
            SautePage()
        End If
        AddTitreNdC(2, Bloc("DATAPROPPING"))

        '--> Affichage de l'étaiement

        Select Case MyBeam.TypeEtaiement
            Case cls_Poutre.EnuTypeEtaiement.FullyPropped
                AddLigneNDC(TABW2 & Bloc("TYPE_PROPPING") & TABAFF & Bloc("FULLYPROPPED"))
            Case cls_Poutre.EnuTypeEtaiement.UnPropped
                AddLigneNDC(TABW2 & Bloc("TYPE_PROPPING") & TABAFF & Bloc("UNPROPPED"))
            Case cls_Poutre.EnuTypeEtaiement.PointPropped
                AddLigneNDC(TABW2 & Bloc("TYPE_PROPPING") & TABAFF & Bloc("POINTPROPPED"))

                If MyBeam.lTraveeConsoleGauche Then
                    If MyBeam.lEtaisConsoleGauche Then
                        AddLigneNDC(TABW2 & Bloc("ENDPROPPEDLEFTCANT") & TABAFF & Bloc("YES"))
                    Else
                        AddLigneNDC(TABW2 & Bloc("ENDPROPPEDLEFTCANT") & TABAFF & Bloc("NO"))
                    End If
                End If

                If MyBeam.lTraveeConsoleDroite Then
                    If MyBeam.lEtaisConsoleDroite Then
                        AddLigneNDC(TABW2 & Bloc("ENDPROPPEDRIGHTCANT") & TABAFF & Bloc("YES"))
                    Else
                        AddLigneNDC(TABW2 & Bloc("ENDPROPPEDRIGHTCANT") & TABAFF & Bloc("NO"))
                    End If
                End If

                If MyBeam.lTraveeConsoleGauche Or MyBeam.lTraveeConsoleDroite Then
                    AddLigneNDC(TABW2 & Bloc("NBPROPPINGWITHCANT") & TABAFF & MyBeam.NbEtaiement)
                Else
                    AddLigneNDC(TABW2 & Bloc("NBPROPPINGWITHOUTCANT") & TABAFF & MyBeam.NbEtaiement)
                End If

                If MyBeam.lEtaisSousProfileAcier Then
                    AddLigneNDC(TABW2 & Bloc("PROPPINGLOCATION") & TABAFF & Bloc("UNDERSTEEL"))
                Else
                    AddLigneNDC(TABW2 & Bloc("PROPPINGLOCATION") & TABAFF & Bloc("UNDERSLAB"))
                End If


        End Select

    End Sub

    Private Sub EditionParametresMaintiens(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des maintiens latéraux d'une poutre
        '----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbBesoinLignes As Integer
        Dim iTraveeDeb, iTraveeFin As Integer

        '--> Initialisation

        Select Case MyBeam.TypeMaintien
            Case cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained, cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
                NbBesoinLignes = 3
            Case cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
                NbBesoinLignes = 5 + MyBeam.NombreTotalMaintiensLateraux
        End Select

        If NbBesoinLignes + nbLignes > MAXLIGNEPPAG Then
            SautePage()
        End If

        '--> Affichage des maitiens latéraux

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

                iTraveeDeb = MyBeam.IndicePremiereTravee
                iTraveeFin = MyBeam.IndiceDerniereTravee

                For i As Integer = iTraveeDeb To iTraveeFin
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

                    'If i = MyBeam.IndiceDerniereTravee Then
                    '    lDerniereTravee = True
                    'Else
                    '    For j As Integer = i + 1 To MyBeam.IndiceDerniereTravee
                    '        If MyBeam.Maintiens(j).Count <> 0 Then
                    '            lDerniereTravee = False
                    '            Exit For
                    '        End If
                    '    Next
                    'End If
                    '==POM => eviter les Exit For 

                    If i < iTraveeFin Then
                        Dim j As Integer = i
                        Do While (lDerniereTravee) And (j < iTraveeFin)
                            j += 1
                            If (MyBeam.Maintiens(j).Count <> 0) Then lDerniereTravee = False
                        Loop
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

        '--> Déclaration 

        Dim MyGamma As cls_Gamma
        Const TABEGAL1 As String = "\T27= "
        Const TABVARL3 As String = "\T45"
        Const TABVARL4 As String = "\T70"
        Const TABEGAL2 As String = "\T52= "
        Const TABEGAL3 As String = "\T77= "

        'On n'affiche les coefficients au feu que si les combinaisons ad hoc sont définies '=== A PROGRAMMER
        Dim lFire As Boolean = True
        Dim ChaineFire As String = ""
        Dim ChaineSlab As String = ""

        '--> Initilisation 

        MyGamma = MyBeam.Param.Gamma.Clone

        '--> Traitement

        Dim NbBesoinLignes As Integer = 21    ' A ajuster

        '--> Initialisation

        If NbBesoinLignes + nbLignes > MAXLIGNEPPAG Then
            SautePage()
        End If

        AddTitreNdC(2, Bloc("GAMMA"))

        '# Charges

        AddTitreNdC(3, Bloc("LOADING_FACTORS"))
        AddLigneNDC(TABVAR2 & "\Sg\s\-G,sup\= " & TABEGAL1 & GetStringInUnit(MyGamma.GammaG_sup, Enu_TypeVariable.SansType, 3, 2, False))
        AddLigneNDC(TABVAR2 & "\Sg\s\-G,inf\= " & TABEGAL1 & GetStringInUnit(MyGamma.GammaG_inf, Enu_TypeVariable.SansType, 3, 2, False))
        AddLigneNDC(TABVAR2 & "\Sg\s\-Q\= " & TABEGAL1 & GetStringInUnit(MyGamma.GammaQ, Enu_TypeVariable.SansType, 3, 2, False))

        '# Coefficients de combinaison

        AddTitreNdC(3, Bloc("COMBINATION_FACTORS"))
        AddLigneNDC(TABVAR2 & "\Sy\s\-0,Q1\= " & TABEGAL1 & MyGamma.Psi0_Q1 & TABVARL3 & "\Sy\s\-0,Q2\= " & TABEGAL2 & MyGamma.Psi0_Q2)
        AddLigneNDC(TABVAR2 & "\Sy\s\-1,Q1\= " & TABEGAL1 & MyGamma.Psi1_Q1 & TABVARL3 & "\Sy\s\-1,Q2\= " & TABEGAL2 & MyGamma.Psi1_Q2)
        AddLigneNDC(TABVAR2 & "\Sy\s\-2,Q1\= " & TABEGAL1 & MyGamma.Psi2_Q1 & TABVARL3 & "\Sy\s\-2,Q2\= " & TABEGAL2 & MyGamma.Psi2_Q2)

        '# Résistances

        AddTitreNdC(3, Bloc("RESISTANCEFACTORS"))

        If lFire Then
            ChaineFire = TABVARL4 & Bloc("FIRE_RES_FACTORS")
        End If
        AddLigneNDC(TABVAR2 & Bloc("STEEL_RES_FACTORS") & TABVARL3 & Bloc("SLAB_RES_FACTORS") & ChaineFire)

        If lFire Then
            ChaineFire = TABVARL4 & "\Sg\s\-M,fi\=" & TABEGAL3 & GetStringInUnit(MyGamma.GammaM_fi, Enu_TypeVariable.SansType, 3, 2, False)
        End If
        AddLigneNDC(TABVAR2 & "\Sg\s\-M0\=" & TABEGAL1 & GetStringInUnit(MyGamma.GammaM0, Enu_TypeVariable.SansType, 3, 2, False) _
                  & TABVARL3 & "\Sg\s\-C\= " & TABEGAL2 & GetStringInUnit(MyGamma.GammaC, Enu_TypeVariable.SansType, 3, 2, False) _
                  & ChaineFire)

        If lFire Then
            ChaineFire = TABVARL4 & "\Sg\s\-C,fi\=" & TABEGAL3 & GetStringInUnit(MyGamma.GammaC_fi, Enu_TypeVariable.SansType, 3, 2, False)
        End If
        AddLigneNDC(TABVAR2 & "\Sg\s\-M1\=" & TABEGAL1 & GetStringInUnit(MyGamma.GammaM1, Enu_TypeVariable.SansType, 3, 2, False) _
                  & TABVARL3 & "\Sg\s\-C\= " & TABEGAL2 & GetStringInUnit(MyGamma.GammaS, Enu_TypeVariable.SansType, 3, 2, False) _
                  & ChaineFire)

        If lFire Then
            ChaineFire = TABVARL4 & "\Sg\s\-v,fi\=" & TABEGAL3 & GetStringInUnit(MyGamma.GammaV_fi, Enu_TypeVariable.SansType, 3, 2, False)
        End If
        If MyGamma.lGammaV_unique Then
            ChaineSlab = "\Sg\s\-V\="
        Else
            ChaineSlab = "\Sg\s\-Vs\="
        End If
        AddLigneNDC(TABVAR2 & "\Sg\s\-M2\=" & TABEGAL1 & GetStringInUnit(MyGamma.GammaM2, Enu_TypeVariable.SansType, 3, 2, False) _
                  & TABVARL3 & ChaineSlab & TABEGAL2 & GetStringInUnit(MyGamma.GammaVs, Enu_TypeVariable.SansType, 3, 2, False) _
                  & ChaineFire)

        If Not MyGamma.lGammaV_unique Then
            AddLigneNDC(TABVARL3 & "\Sg\s\-Vc\=" & TABEGAL2 & GetStringInUnit(MyGamma.GammaVc, Enu_TypeVariable.SansType, 3, 2, False))
        End If

        AddLigneNDC(TABVARL3 & "\Sg\s\-P\=" & TABEGAL2 & GetStringInUnit(MyGamma.GammaP, Enu_TypeVariable.SansType, 3, 2, False))

    End Sub

    'Private Sub EditionParametresCoefGammaOLD(ByVal MyBeam As cls_Poutre)
    '    '----------------------------------------------------------------------------------------------
    '    '   10/07/23 :  Création - Version 1.00 - POM
    '    '----------------------------------------------------------------------------------------------
    '    '   Edition des coefficients partiels
    '    '----------------------------------------------------------------------------------------------

    '    '--> Déclaration 

    '    Dim MyGamma As cls_Gamma
    '    Const TABEGAL1 As String = "\T30="
    '    '--> Initilisation 

    '    MyGamma = MyBeam.Param.Gamma.Clone

    '    '--> Traitement

    '    SautePage()

    '    AddTitreNdC(2, Bloc("GAMMA"))

    '    AddTitreNdC(3, Bloc("LOADING_FACTORS"))
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-G,sup\= " & TABEGAL & MyGamma.GammaG_sup)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-G,inf\= " & TABEGAL & MyGamma.GammaG_inf)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-Q\= " & TABEGAL & MyGamma.GammaQ)

    '    AddTitreNdC(3, Bloc("COMBINATION_FACTORS_Q1"))
    '    AddLigneNDC(TABVAR2 & "\Sy\s\-0,Q1\= " & TABEGAL & MyGamma.Psi0_Q1)
    '    AddLigneNDC(TABVAR2 & "\Sy\s\-1,Q1\= " & TABEGAL & MyGamma.Psi1_Q1)
    '    AddLigneNDC(TABVAR2 & "\Sy\s\-2,Q1\= " & TABEGAL & MyGamma.Psi2_Q1)

    '    AddTitreNdC(3, Bloc("COMBINATION_FACTORS_Q2"))
    '    AddLigneNDC(TABVAR2 & "\Sy\s\-0,Q2\= = " & MyGamma.Psi0_Q2)
    '    AddLigneNDC(TABVAR2 & "\Sy\s\-1,Q2\= = " & MyGamma.Psi1_Q2)
    '    AddLigneNDC(TABVAR2 & "\Sy\s\-2,Q2\= = " & MyGamma.Psi2_Q2)

    '    AddTitreNdC(3, Bloc("STEEL_RES_FACTORS"))
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-M0\= = " & MyGamma.GammaM0)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-M1\= = " & MyGamma.GammaM1)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-M2\= = " & MyGamma.GammaM2)

    '    AddTitreNdC(3, Bloc("SLAB_RES_FACTORS"))
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-C\= = " & MyGamma.GammaC)
    '    If MyGamma.lGammaV_unique Then
    '        AddLigneNDC(TABVAR2 & "\Sg\s\-V\= = " & MyGamma.GammaVs)
    '    Else
    '        AddLigneNDC(TABVAR2 & "\Sg\s\-Vs\= = " & MyGamma.GammaVs)
    '        AddLigneNDC(TABVAR2 & "\Sg\s\-Vc\= = " & MyGamma.GammaVc)
    '    End If
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-S\= = " & MyGamma.GammaS)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-P\= = " & MyGamma.GammaP)

    '    AddTitreNdC(3, Bloc("FIRE_RES_FACTORS"))
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-M,fi\= = " & MyGamma.GammaM_fi)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-C,fi\= = " & MyGamma.GammaC_fi)
    '    AddLigneNDC(TABVAR2 & "\Sg\s\-V,fi\= = " & MyGamma.GammaV_fi)

    'End Sub

    Private Sub EditionParametresChargement(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------
        Dim NbBesoinLignes As Integer = 5     ' A ajuster

        '--> Initialisation

        If NbBesoinLignes + nbLignes > MAXLIGNEPPAG Then
            SautePage()
        End If

        AddTitreNdC(2, Bloc("LOADS"))

    End Sub

    Private Sub EditionParametresCombinaisons(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------

        Dim NbBesoinLignes As Integer = 5     ' A ajuster

        '--> Initialisation

        If NbBesoinLignes + nbLignes > MAXLIGNEPPAG Then
            SautePage()
        End If

        AddTitreNdC(2, Bloc("COMBINATIONS"))

    End Sub

#End Region

#Region "***Edition propriétés des sections***"

    Private Sub EditionProprietesSection(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   16/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lMixte As Boolean = MyBeam.Section.lMixte

        '--> Initialisation

        SautePage()

        AddTitreNdC(1, BlocSP("SECTIONSPROPERTIES"))

        '--> Traitement

        If lMixte Then
            EditionProprietesSectionPoutreMixte(MyBeam)
        Else
        End If

    End Sub

    Private Sub EditionProprietesSectionPoutreMixte(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   16/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une poutre mixte
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NCOL As Integer, LargCol(1) As Integer, PosTab As Integer

        '--> Initialisation du tableau

        NCOL = 8
        LargCol(0) = 15
        LargCol(1) = 10
        PosTab = 5

        InitialiseTableauPropSectionMixte(NCOL, LargCol, PosTab)

        '--> Console gauche

        If MyBeam.lTraveeConsoleGauche Then
            EditionPropSectionsPMixteConsole(MyBeam, True, NCOL, LargCol, PosTab)
        End If

        '--> Travées centrales

        For i = 1 To MyBeam.NombreTraveesDeuxAppuis
            EditionPropSectionsPMixteTravee(MyBeam, i, NCOL, LargCol, PosTab)
        Next

        '--> Console droite

        If MyBeam.lTraveeConsoleDroite Then
            EditionPropSectionsPMixteConsole(MyBeam, False, NCOL, LargCol, PosTab)
        End If

        '--> Fin du tableau

        FinTableau()
    End Sub

    Private Sub EditionPropSectionsPMixteTravee(MyBeam As cls_Poutre, iTravee As Integer, NCOL As Integer, LargCol() As Integer, PosTab As Integer)
        '-------------------------------------------------------------------------------------------
        '   16/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une travée d'un poutre mixte
        '-------------------------------------------------------------------------------------------

        Dim bEff As Decimal
        Dim LTrav As Decimal
        Dim mSign As Decimal
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim Inertie As Decimal
        Dim myBord As Integer = Bordures.Tous
        Dim ChaineT, ChaineS As String
        Dim n0 As Decimal

        '--> Initialisation

        If MyBeam.NombreTraveesDeuxAppuis = 1 Then
            ChaineT = "travée principale"
        Else
            ChaineT = "travée " & CStr(iTravee)
        End If

        '--> Sur appui gauche

        bEff = MyBeam.BeffDalle(0, iTravee, False, False)
        n0 = MyBeam.Dalle.beton.CoefficientEquivalenceCT
        If iTravee = 1 And (Not MyBeam.lTraveeConsoleGauche) Then
            mSign = 1
            ChaineS = "Appui  G (M>0)"
        Else
            mSign = -1
            ChaineS = "Appui  G (M<0)"
        End If

        MyBeam.Section.ProprietesPlastiquesMyy(mSign, True, MyBeam.Param.Gamma, n0, zANP, MplRd)
        MyBeam.Section.ProprietesElastiquesMyy(mSign, True, MyBeam.Param.Gamma, 1, zANE, Inertie, MelRd)

        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, ChaineT)
        AddCellule(LargCol(0), myBord, PositionTexteInCell.Centre, ChaineS)
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(bEff, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANP, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(Inertie, Enu_TypeVariable.InertieCM4, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))

        '--> à mi travée

        bEff = MyBeam.BeffDalle(MyBeam.LongueurTravee(iTravee) / 2, iTravee, False, False)
        mSign = 1
        ChaineS = "Mi travée (M>0)"
        MyBeam.Section.ProprietesPlastiquesMyy(mSign, True, MyBeam.Param.Gamma, n0, zANP, MplRd)
        MyBeam.Section.ProprietesElastiquesMyy(mSign, True, MyBeam.Param.Gamma, 1, zANE, Inertie, MelRd)

        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, "")
        AddCellule(LargCol(0), myBord, PositionTexteInCell.Centre, ChaineS)
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(bEff, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANP, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(Inertie, Enu_TypeVariable.InertieCM4, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))

        '--> Sur appui gauche

        bEff = MyBeam.BeffDalle(MyBeam.LongueurTravee(iTravee), iTravee, False, False)
        n0 = MyBeam.Dalle.beton.CoefficientEquivalenceCT
        If iTravee = MyBeam.NombreTraveesDeuxAppuis And (Not MyBeam.lTraveeConsoleDroite) Then
            mSign = 1
            ChaineS = "Appui  D (M>0)"
        Else
            mSign = -1
            ChaineS = "Appui  D (M<0)"
        End If

        MyBeam.Section.ProprietesPlastiquesMyy(mSign, True, MyBeam.Param.Gamma, n0, zANP, MplRd)
        MyBeam.Section.ProprietesElastiquesMyy(mSign, True, MyBeam.Param.Gamma, 1, zANE, Inertie, MelRd)

        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, ChaineT)
        AddCellule(LargCol(0), myBord, PositionTexteInCell.Centre, ChaineS)
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(bEff, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANP, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(Inertie, Enu_TypeVariable.InertieCM4, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))

    End Sub

    Private Sub EditionPropSectionsPMixteConsole(MyBeam As cls_Poutre, lGauche As Boolean, NCOL As Integer, LargCol() As Integer, PosTab As Integer)
        '-------------------------------------------------------------------------------------------
        '   16/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour la console d'un poutre mixte
        '-------------------------------------------------------------------------------------------
        '   lGauche     [E] :   Indique si console gauche
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee As Integer
        Dim bEff As Decimal
        Dim LTrav As Decimal
        Const mSign As Decimal = -1
        Dim zANP, MplRd As Decimal
        Dim zANE, MelRd As Decimal
        Dim Inertie As Decimal
        Dim myBord As Integer = Bordures.Tous
        Dim ChaineConsole As String

        '--> Initialisation du tableau

        'NCOL = 7
        'LargCol(0) = 15
        'LargCol(1) = 10
        'PosTab = 10

        'InitialiseTableauPropSectionMixte(NCOL, LargCol, PosTab)

        '--> Calcul des propriétés de la section

        If lGauche Then
            iTravee = MyBeam.IndicePremiereTravee
            ChaineConsole = "Console Gauche"
        Else
            iTravee = MyBeam.IndiceDerniereTravee
            ChaineConsole = "Console droite"
        End If
        LTrav = MyBeam.LongueurTravee(iTravee)
        bEff = MyBeam.BeffDalle(LTrav / 2, iTravee, False, False)

        MyBeam.Section.ProprietesPlastiquesMyy(mSign, True, MyBeam.Param.Gamma, 0, zANP, MplRd)
        MyBeam.Section.ProprietesElastiquesMyy(mSign, True, MyBeam.Param.Gamma, 1, zANE, Inertie, MelRd)

        '--> Propriétés sur toutes les sections

        InitialiseLigne(NCOL, HLIGNE)

        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, ChaineConsole)
        AddCellule(LargCol(0), myBord, PositionTexteInCell.Centre, "All sections (M<0)")
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(bEff, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANP, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(Inertie, Enu_TypeVariable.InertieCM4, 4, 4, False))
        AddCellule(LargCol(1), myBord, PositionTexteInCell.Centre, GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 4, False))

        ''--> Fin du tableau

        'FinTableau()

    End Sub

    Private Sub InitialiseTableauPropSectionMixte(NCOL As Integer, LargCol() As Integer, PosTab As Integer)
        '-------------------------------------------------------------------------------------------
        '   16/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entete du tableau pour les propriétés de sections pour la console d'un poutre mixte
        '-------------------------------------------------------------------------------------------

        Dim myBord As Integer

        AddLigneNDC("\TABLEAU " & PosTab)
        InitialiseLigne(NCOL, HLIGNEENTETE, True)

        myBord = Bordures.Gauche + Bordures.Droite + Bordures.Haut

        AddCellule(LargCol(1), Bordures.Aucun, PositionTexteInCell.Centre, "")
        AddCellule(LargCol(0), Bordures.Aucun, PositionTexteInCell.Centre, "")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "b\-eff\=")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "z\-pl\=")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "M\-pl,Rd\=")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "z\-el\=")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "I\-yy\=")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "M\-el,Rd\=")

        myBord = Bordures.Gauche + Bordures.Droite + Bordures.Bas

        AddCellule(LargCol(1), Bordures.Aucun, PositionTexteInCell.Centre, "")
        AddCellule(LargCol(0), Bordures.Aucun, PositionTexteInCell.Centre, "")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "(" & LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment) & ")")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "(" & LogicielInfo.Unit_Inerties(LogicielOptions.IndUnitInerties) & ")")
        AddCelluleFond(LargCol(1), myBord, PositionTexteInCell.Centre, "(" & LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment) & ")")

    End Sub

#End Region

#Region " Edition des analyses (M+V) / cas de charge et combinaisons "

    Private Sub EditionAnalysePoutre(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   10/11/23 :  Création - GUD
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        SautePage()

        AddTitreNdC(1, BlocAnalyse("ANALYSIS"))

        '--> Analyses par cas de charge

        AddTitreNdC(2, BlocAnalyse("ELEMNTRY_LC"))

        For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count - 1

            '--> On affiche le cas de charge uniquement si le cas de charge est disponible
            If MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).lRunCalcul Then
                EditionAnalyseChargeA(MyProjet.Poutres(MyProjet.IndEnCours), MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i))
                SautePage()
            End If
        Next

        '--> Analyses par combinaisons ELU

        AddTitreNdC(2, BlocAnalyse("ELEMNTRY_ULS"))

        For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU.nbCombi - 1

            EditionAnalyseCombiELU(MyProjet.Poutres(MyProjet.IndEnCours), i)

        Next


    End Sub

    Private Sub EditionAnalyseCombiELU(myPoutre As cls_Poutre, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Indice de la poutre
        '   iCombi      [E] :   Indice de la combinaison ELU
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim lMultispan As Boolean
        Dim NCol, PosTab As Integer
        Dim iTravee, i As Integer
        Dim iNodeO, iNodeE As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim iTraveeAffichee As Integer = 1
        Dim iCompteur As Integer = 0
        Dim NbLignesMax() As Integer = {25, 30}
        Dim iTab As Integer = 0

        '--> Initialisation

        lRetrait = True
        lMultispan = (myPoutre.NbTravees > 1)
        iTravDeb = myPoutre.IndicePremiereTravee
        iTravFin = myPoutre.IndiceDerniereTravee

        '--> Affichage de la combinaison

        AffichageCombinaisonCharge(myPoutre, myPoutre.CombiA_ELU, "ELU_0" & CStr(iCombi), iCombi, lRetrait)

        '--> Calcul des M et V

        myPoutre.CombiA_ELU.CombineMoments(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, lRetrait)
        myPoutre.CombiA_ELU.CombineEffortsT(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, lRetrait)

        '--> Affichage de la combinaison

        '# Entête

        EnteteTableauAnalyseCombi(lMultispan, NCol, PosTab)

        '# Tableau

        For iTravee = iTravDeb To iTravFin
            iNodeO = myPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeE = myPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            '=== Extrémité gauche

            If iTravee = iTravDeb Then
                LigneTableauMVCombiExtremite(lMultispan, True, NCol, PosTab, i, iTraveeAffichee, myPoutre.Nodes.xTravee(i), myPoutre.Nodes.xGlobal(i),
                                             VEd(0, 1), MEd(0, 1))
                iCompteur += 1
            End If

            '=== Lignes intermédiaires

            For i = iNodeO + 1 To iNodeE - 1

                iCompteur += 1

                If iCompteur > NbLignesMax(iTab) Then
                    FinTableau()
                    iCompteur = 0
                    iTab = 1
                    SautePage()
                    EnteteTableauAnalyseCombi(lMultispan, NCol, PosTab)
                End If

                LigneTableauMVCombi(lMultispan, NCol, PosTab, i, iTraveeAffichee, myPoutre.Nodes.xTravee(i), myPoutre.Nodes.xGlobal(i),
                                        VEd(i, 0), VEd(i, 1), MEd(i, 0), MEd(i, 1))
            Next

            '=== Appui droite

            If iTravee = iTravFin Then
                LigneTableauMVCombiExtremite(lMultispan, False, NCol, PosTab, iNodeE, iTraveeAffichee, myPoutre.Nodes.xTravee(iNodeE), myPoutre.Nodes.xGlobal(iNodeE),
                                             VEd(iNodeE, 0), MEd(iNodeE, 0))
            Else
                LigneTableauMVCombiAppui(NCol, PosTab, iNodeE, iTraveeAffichee, myPoutre.Nodes.xTravee(i), myPoutre.Nodes.xGlobal(i),
                                         VEd(i, 0), VEd(i, 1), MEd(i, 0), MEd(i, 1))
            End If
            iCompteur += 1
            iTraveeAffichee += 1
        Next

        '# Fin du Tableau

        FinTableau()

    End Sub

    Private Sub LigneTableauMVCombiExtremite(lMultiSpan As Boolean, lGauche As Boolean, NCol As Integer, Pos As Integer,
                                             iNode As Integer, iTravee As Integer, xPosT As Decimal, xPosG As Decimal,
                                             VEd As Decimal, MEd As Decimal)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage d'une ligne de tableau MV - cas noeud d'extremité de la poutre
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   lGauche     [E] :   Indique si extremité gauche ou droite de la poutre
        '   NCol        [E] :   Nombre de colonnes
        '   Pos         [E] :   Position du tableau / bord gauche
        '   iNode       [E] :   Indice du noeud
        '   iTravee     [E] :   Indice de la travée
        '   xposG,xPosT [E] :   Position globale et dans la travée du noeud
        '   VEd         [E] :   Valeur de l'effort tranchant
        '   MEd         [E] :   Valeur du moment fléchissant
        '-------------------------------------------------------------------------------------------

        InitialiseLigne(NCol, HLIGNE, True)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, iNode)

        '# Position et travée

        If lMultiSpan Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, iTravee)

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, xPosT)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, xPosG)
        Else

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, xPosG)

        End If

        '# Effort tranchant

        If lGauche Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEd, Enu_TypeVariable.Effort, 3, 2, False))
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEd, Enu_TypeVariable.Effort, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
        End If

        If lGauche Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEd, Enu_TypeVariable.Effort, 3, 2, False))
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEd, Enu_TypeVariable.Effort, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
        End If

    End Sub

    Private Sub LigneTableauMVCombiAppui(NCol As Integer, Pos As Integer,
                                         iNode As Integer, iTravee As Integer, xPosT As Decimal, xPosG As Decimal,
                                         VEdG As Decimal, VEdd As Decimal, MEdG As Decimal, MEdD As Decimal)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage d'une ligne de tableau MV - cas d'un noeud sur appui intermédiaire
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [E] :   Nombre de colonnes
        '   Pos         [E] :   Position du tableau / bord gauche
        '   iNode       [E] :   Indice du noeud
        '   iTravee     [E] :   Indice de la travée (celle de gauche)
        '   xposG,xPosT [E] :   Position globale et dans la travée du noeud
        '   VEdG, VEdD  [E] :   Valeur des efforts tranchants
        '   MEdG, MEdD  [E] :   Valeur des moments fléchissants
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pNColLigne As Integer
        Dim lOneM As Boolean
        Dim lOneV As Boolean

        '--> Initialisation

        lOneM = IsEqual(MEdD, MEdG)
        lOneV = IsEqual(VEdd, VEdG)

        pNColLigne = NCol - 2
        If Not lOneM Then pNColLigne += 1
        If Not lOneV Then pNColLigne += 1

        InitialiseLigne(pNColLigne, HLIGNE, True)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iNode + 1))

        '# Position et travée

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee) & " / " & CStr(iTravee + 1))

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False) & " / 0")
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))

        '# Effort tranchant

        If lOneV Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEdG, Enu_TypeVariable.Effort, 3, 2, False))
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEdG, Enu_TypeVariable.Effort, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEdd, Enu_TypeVariable.Effort, 3, 2, False))
        End If

        '# Moment fléchissant

        If lOneM Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MEdG, Enu_TypeVariable.Moment, 3, 2, False))
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MEdG, Enu_TypeVariable.Moment, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MEdD, Enu_TypeVariable.Moment, 3, 2, False))
        End If


    End Sub

    Private Sub LigneTableauMVCombi(lMultiSpan As Boolean, NCol As Integer, Pos As Integer,
                                    iNode As Integer, iTravee As Integer, xPosG As Decimal, xPosT As Decimal,
                                    VEdG As Decimal, VEdd As Decimal, MEdG As Decimal, MEdD As Decimal)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage d'une ligne de tableau MV - cas général
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [E] :   Nombre de colonnes
        '   Pos         [E] :   Position du tableau / bord gauche
        '   iNode       [E] :   Indice du noeud
        '   iTravee     [E] :   Indice de la travée
        '   xposG,xPosT [E] :   Position globale et dans la travée du noeud
        '   VEdG, VEdD  [E] :   Valeur des efforts tranchants
        '   MEdG, MEdD  [E] :   Valeur des moments fléchissants
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pNColLigne As Integer
        Dim lOneM As Boolean
        Dim lOneV As Boolean

        '--> Initialisation

        lOneM = IsEqual(MEdD, MEdG)
        lOneV = IsEqual(VEdd, VEdG)

        pNColLigne = NCol - 2
        If Not lOneM Then pNColLigne += 1
        If Not lOneV Then pNColLigne += 1

        InitialiseLigne(pNColLigne, HLIGNE, True)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iNode))

        '# Position et travée

        If lMultiSpan Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee))

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))
        Else

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))

        End If

        '# Effort tranchant

        If lOneV Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEdG, Enu_TypeVariable.Effort, 3, 2, False))
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEdG, Enu_TypeVariable.Effort, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(VEdd, Enu_TypeVariable.Effort, 3, 2, False))
        End If

        '# Moment fléchissant

        If lOneM Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MEdG, Enu_TypeVariable.Moment, 3, 2, False))
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MEdG, Enu_TypeVariable.Moment, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MEdD, Enu_TypeVariable.Moment, 3, 2, False))
        End If

    End Sub

    Private Sub EnteteTableauAnalyseCombi(lMultiSpan As Boolean, ByRef NCol As Integer, ByRef Pos As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entete du tableau pour l'affichage des sollicitations sous combi ELU
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [S] :   Nombre de colonnes
        '   Pos         [S] :   Position du tableau / bord gauche
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim IndGauche As String = IndiceGaucheDroite(True)
        Dim IndDroite As String = IndiceGaucheDroite(False)

        '--> Initialisation

        If lMultiSpan Then
            NCol = 8
            Pos = 10
        Else
            NCol = 6
            Pos = 20
        End If

        AddLigneNDC("\TABLEAU " & CStr(Pos))

        InitialiseLigne(NCol, HLIGNEENTETE, True)
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocAnalyse("NODE"))
        If lMultiSpan Then
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocAnalyse("SPAN"))
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x\-" & BlocAnalyse("SPAN") & "\= (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x\-" & BlocAnalyse("GLOBAL") & "\= (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        Else
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        End If
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "V\-" & IndGauche & "\= (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & ")")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "V\-" & IndDroite & "\= (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & ")")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "M\-" & IndGauche & "\= (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitMoment) & "." & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "M\-" & IndDroite & "\= (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitMoment) & "." & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")

    End Sub

    Private Function IndiceGaucheDroite(lGauche) As String
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Définition l'indice gauche/droite pour la note de calcul, en fonction de la version CTICM ou AM
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim myIndiceGD As String

        '--> Traitement

        If LogicielInfo.Maitre = EnuMaitre.ArcelorMittal Then

            If lGauche Then myIndiceGD = "L" Else myIndiceGD = "R"

        Else

            If lGauche Then myIndiceGD = "g" Else myIndiceGD = "d"

        End If

        Return myIndiceGD
    End Function

    Private Sub AffichageCombinaisonCharge(myPoutre As cls_Poutre, myCombi As cls_Combinaisons, TitreCombi As String, iCombi As Integer, lRetrait As Boolean)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Indice de la poutre
        '   myCombi     [E] :   Combinaison à afficher
        '   TitreCombi  [E] :   Titre de la combinaison
        '   iCombi      [E] :   Indice de la combinaison
        '   lRetrait    [E] :   Prise en compte ou non du retrait
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Chaine As String
        Dim jCdc As Integer
        Dim sPlus As String = ""
        Dim lFirst As Boolean = True
        Dim lAffiche As Boolean

        '--> Initialisation

        'Chaine = TitreCombi & " = "
        Chaine = myCombi.Symbole(iCombi) & " = "

        For jCdc = 0 To myPoutre.ChargesA.Count - 1

            lAffiche = lRetrait Or ((Not lRetrait) And (myPoutre.ChargesA(jCdc).Type <> cls_CasDeCharge.EnuType.Retrait))

            If (Not IsEqual(myCombi.CoefCombi(iCombi)(jCdc), 0)) And lAffiche Then

                Chaine += sPlus & GetStringInUnit(myCombi.CoefCombi(iCombi)(jCdc), Enu_TypeVariable.SansType, 3, 2, False) & " " & myPoutre.ChargesA(jCdc).Symbol

                If lFirst Then
                    sPlus = " + "
                    lFirst = False
                End If

            End If

        Next

        '--> Affichage

        AddTitreNdC(3, myCombi.Symbole(iCombi))
        AddLigneNDC(TABW2 & Chaine)
        SauteLigne()

    End Sub

    Private Sub EditionAnalyseChargeA(MyPoutreLoc As cls_Poutre, ChargeA As cls_CasDeCharge)
        '-------------------------------------------------------------------------------------------
        '   10/11/23 :  Création - GUD
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour un cas de charge
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim lMultispan As Boolean
        Dim NCol, PosTab As Integer
        Dim iTravee, i As Integer
        Dim iNodeO, iNodeE As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim iTraveeAffichee As Integer = 1
        Dim iCompteur As Integer = 0
        Dim NbLignesMax() As Integer = {25, 30}
        Dim iTab As Integer = 0

        '--> Initialisation

        lMultispan = (MyPoutreLoc.NbTravees > 1)
        iTravDeb = MyPoutreLoc.IndicePremiereTravee
        iTravFin = MyPoutreLoc.IndiceDerniereTravee

        '--> Affichage de la combinaison

        AddTitreNdC(3, ChargeA.Symbol & " :" & ChargeA.Nom)

        If Not ChargeA.lRunCalcul Then
            AddLigneNDC(TABW2 & BlocAnalyse("NOTCALCULATION"))
            Exit Sub
        End If

        '--> Affichage de la combinaison

        '# Entête

        EnteteTableauAnalyseCombi(lMultispan, NCol, PosTab)

        '# Tableau

        For iTravee = iTravDeb To iTravFin
            iNodeO = MyPoutreLoc.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeE = MyPoutreLoc.Nodes.iNodeExtTrav(iTravee, 1)

            '=== Extrémité gauche

            If iTravee = iTravDeb Then
                LigneTableauMVCombiExtremite(lMultispan, True, NCol, PosTab, i, iTraveeAffichee, MyPoutreLoc.Nodes.xTravee(i), MyPoutreLoc.Nodes.xGlobal(i),
                                            ChargeA.VZ(0, 1), ChargeA.MYY(0, 1))
                iCompteur += 1
            End If

            '=== Lignes intermédiaires

            For i = iNodeO + 1 To iNodeE - 1

                iCompteur += 1

                If iCompteur > NbLignesMax(iTab) Then
                    FinTableau()
                    iCompteur = 0
                    iTab = 1
                    SautePage()
                    EnteteTableauAnalyseCombi(lMultispan, NCol, PosTab)
                End If

                LigneTableauMVCombi(lMultispan, NCol, PosTab, i, iTraveeAffichee, MyPoutreLoc.Nodes.xTravee(i), MyPoutreLoc.Nodes.xGlobal(i),
                                        ChargeA.VZ(i, 0), ChargeA.VZ(i, 1), ChargeA.MYY(i, 0), ChargeA.MYY(i, 1))

            Next

            '=== Appui droite

            If iTravee = iTravFin Then
                LigneTableauMVCombiExtremite(lMultispan, False, NCol, PosTab, iNodeE, iTraveeAffichee, MyPoutreLoc.Nodes.xTravee(iNodeE), MyPoutreLoc.Nodes.xGlobal(iNodeE),
                                             ChargeA.VZ(iNodeE, 0), ChargeA.MYY(iNodeE, 0))
            Else
                LigneTableauMVCombiAppui(NCol, PosTab, iNodeE, iTraveeAffichee, MyPoutreLoc.Nodes.xTravee(i), MyPoutreLoc.Nodes.xGlobal(i),
                                         ChargeA.VZ(i, 0), ChargeA.VZ(i, 1), ChargeA.MYY(i, 0), ChargeA.MYY(i, 1))
            End If
            iCompteur += 1
            iTraveeAffichee += 1
        Next

        '# Fin du Tableau

        FinTableau()
    End Sub

    Private Function IndiceTravee(Node As Integer, iNodeAppui As Integer()) As Integer()
        '-------------------------------------------------------------------------------------------
        '   10/11/23 :  Création - GUD
        '-------------------------------------------------------------------------------------------
        '   Permet de renvoyer l'indice de la travée à laquelle appartient le noeud 
        '   Dans le cas où le noeud appartient à deux travées, l'indice de la travée renvoyée est celle de gauche (sauf pour le tout premier noeud)
        '-------------------------------------------------------------------------------------------

        Dim indTravee(1) As Integer

        For j As Integer = 0 To iNodeAppui.Count - 1 'On ne commence pas à l'indice 0 exprès car l'indice de la travée du premier noeud est 1
            If Node <= iNodeAppui(j) Then
                indTravee(0) = j + 1
                If Node = iNodeAppui(j) And j <> iNodeAppui.Count - 1 Then
                    indTravee(1) = indTravee(0) + 1
                Else
                    indTravee(1) = indTravee(0)
                End If
                Return indTravee
            End If
        Next

    End Function

#End Region

#Region "***Edition vérifications ELU***"

    Private Sub EditionVerificationsELU(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   12/10/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des vérifications ELU (en phase finale pour les poutres mixtes)
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        SautePage()

        If MyBeam.lMixte Then
            AddTitreNdC(1, BlocELU("ULS_CHECKS_FINAL"))
        Else
            AddTitreNdC(1, BlocELU("ULS_CHECKS"))
        End If

        If Not MyBeam.VerificationsELUDispo(MyBeam.lMixte) Then Exit Sub

        '--> Traitement

        '# Synthèse des critères

        EditionVerificationsELUSummary(MyBeam)

        '# Calcul détaillé des critères sous combinaisons ELU

        EditionVerificationsELUCombi(MyBeam)

        '# Poutres mixtes : ferraillage transversal

        If MyBeam.lMixte Then
            EditionFerraillageTransversal(MyBeam)
        End If

    End Sub

    Private Sub EditionFerraillageTransversal(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Détail du calcul des armatures transversales
        '-------------------------------------------------------------------------------------------

        AddTitreNdC(2, BlocELU("CRITERIA_TRANSREBAR"))



    End Sub

    Private Sub EditionVerificationsELUSummary(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Synthèse des critères ELU
        '-------------------------------------------------------------------------------------------

        '--> Titre

        AddTitreNdC(2, BlocELU("CRITERIA_SUM"))

        AddLigneNDC(TABW2 & BlocELU("INFO_S") & "   " & BlocELU("INFO_NS"))
        SauteLigne()

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                EditionVerificationsELUSummaryMIXTE(MyBeam, 0)
        End Select

        Exit Sub
        AddLigneNDC(TABW2 & BlocELU("M_CRITERIA") & TABAFF & "\SG\s\-M\=" & TABEGAL & 0)
        AddLigneNDC(TABW2 & BlocELU("V_CRITERIA") & TABAFF & "\SG\s\-V\=" & TABEGAL & 0)
        AddLigneNDC(TABW2 & BlocELU("MV_CRITERIA") & TABAFF & "\SG\s\-MV\=" & TABEGAL & 0)
        AddLigneNDC(TABW2 & BlocELU("LTB_CRTIERIA") & TABAFF & "\SG\s\-LT\=" & TABEGAL & 0)
        AddLigneNDC(TABW2 & BlocELU("REINF_CRITERIA") & TABAFF & "\Sr\s\-s\=" & TABEGAL & 0)


        If Not MyBeam.Section.lSlimFloor = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then AddLigneNDC(TABW2 & BlocELU("LOWPLATE_SLIMFLOOR_CRITERIA") & TABAFF & "\SG\s\-q\=" & TABEGAL & 0)
        If Not MyBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then AddLigneNDC(TABW2 & BlocELU("WELD_CRITERIA") & TABAFF & "a\-w\=" & TABEGAL & 0)
        If Not MyBeam.Section.lSlimFloor = cls_ProfilA.Enum_TypeSectionAcier.Lamine Then AddLigneNDC(TABW2 & BlocELU("WELD_CRITERIA") & TABAFF & "a\-u\=" & TABEGAL & 0)




    End Sub

    Private Sub EditionVerificationsELUSummaryMIXTE(MyBeam As cls_Poutre, iVerif As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Synthèse des critères ELU pour une poutre mixte (avec ou sans enrobage)
        '-------------------------------------------------------------------------------------------

        If MyBeam.Param.lElasticDesign Then
            '--> Calcul élastique imposé

            AddLigneNDC(TABW2 & "Calcul élastique imposé")

        Else

            If MyBeam.VerifMixte(iVerif).lCalculPlastic Then
                '--> Calcul Plastique

                AddLigneNDC(TABW2 & "Calcul plastique")
                'AddLigneNDC(TABW2 & BlocELU("M_CRITERIA") & TABAFF & "\SG\s\-M\=" & TABEGAL & 0)
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereM, "\SG\s\-M\=", BlocELU("M_CRITERIA"))
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereV, "\SG\s\-V\=", BlocELU("V_CRITERIA"))

            Else

                '--> Calcul élastique classe 3

                AddLigneNDC(TABW2 & "Calcul élastique (classe 3)")

                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereM, "\SG\s\-M\=", BlocELU("M_CRITERIA"))
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereV, "\SG\s\-V\=", BlocELU("V_CRITERIA"))


            End If
        End If




    End Sub

    Private Sub AfficheSyntheseCritere(Critere As cls_Critere, Symbol As String, Titre As String)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage de la synthèse d'un critère
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim strGras, strFinGras As String
        Dim TABOK As String = "\T85"
        Dim TABInfo As String = "\T70"
        Dim strOK As String
        Dim Valeur As Decimal = Critere.CritereMax

        '--> Initialisation

        If IsGreater(Valeur, 1) Then
            strGras = "\G"
            strFinGras = "\g"
            strOK = ">1   NS"
        Else
            strGras = ""
            strFinGras = ""
            strOK = "<= 1  S"
        End If

        '--> Affichage

        AddLigneNDC(TABW2 & Titre & TABAFF & strGras &
                    Symbol & TABEGAL & GetStringInUnit(Valeur, Enu_TypeVariable.SansType, 3, 2, False) &
                    strFinGras & TABInfo & "(N" & CStr(Critere.iNodeM + 1) & "/" & strRacineELU & "_" & CStr(Critere.iCombiM + 1) & ")" & strGras & TABOK & strOK & strFinGras)


    End Sub


    Private Sub EnteteTableauCriteresELU(MyBeam As cls_Poutre, ByRef NCOL As Integer)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entête du tableau des critères ELU par combinaison
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim PostTab As Integer = 20
        Dim lMultiSpan As Boolean = (MyBeam.NbTravees > 1)
        Dim lElastic As Boolean = (MyBeam.Param.lElasticDesign)

        '--> Initialisation

        NCOL = 3
        If lMultiSpan Then NCOL += 1
        If lElastic Then
            If MyBeam.lMixte Then
                If lMultiSpan Then NCOL += 2 Else NCOL += 1
            End If
            If MyBeam.lEnrobage Then NCOL += 2
        End If

        AddLigneNDC("\TABLEAU " & CStr(PostTab))

        '--> Entête

        InitialiseLigne(NCOL, HLIGNEENTETE, False)

        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "Combi")
        If lMultiSpan Then
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocELU("SPAN"))
        End If

        If lElastic Then
            '# Contraintes acier
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\-s\s,a\=")
            If MyBeam.lMixte Then
                '# Contraintes dalle béton
                AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\-s\s,c\=")
                If lMultiSpan Then
                    '# Contraintes armatures
                    AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\-s\s,s\=")
                End If
            End If

            If MyBeam.lEnrobage Then
                '# Contraintes béton enrobage
                AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\-s\s,ce\=")
                '# Contraintes armatures enrobage
                AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\-s\s,se\=")
            End If

            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\-t\s,a\=")

        Else
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-M\=")
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-V\=")
        End If

    End Sub

    Private Sub AffichageTableauCriteresELU(MyBeam As cls_Poutre, lMultiSpan As Boolean, ByRef NCOL As Integer, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage dans le tableau des critères ELU des résultats pour une combinaison
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTraveeDeb As Integer = MyBeam.IndicePremiereTravee
        Dim iTraveeFin As Integer = MyBeam.IndiceDerniereTravee
        Dim MyBordures(iTraveeFin) As Integer
        Dim i As Integer
        Dim lElastic As Boolean = MyBeam.Param.lElasticDesign
        Dim iNodeD, iNodeF As Integer
        Dim lMixte As Boolean = MyBeam.lMixte
        Const iVerif As Integer = 0

        '--> Initialisation

        For i = iTraveeDeb To iTraveeFin
            MyBordures(i) = Bordures.Gauche + Bordures.Droite
        Next
        MyBordures(0) += Bordures.Haut
        MyBordures(MyBordures.GetUpperBound(0)) += Bordures.Bas

        '--> Traitement

        For i = iTraveeDeb To iTraveeFin
            iNodeD = MyBeam.Nodes.iNodeExtTrav(i, 0)
            iNodeF = MyBeam.Nodes.iNodeExtTrav(i, 1)

            InitialiseLigne(NCOL, HLIGNE, False)
            AddCellule(LC3, MyBordures(i), PositionTexteInCell.Centre, MyBeam.CombiA_ELU.Symbole(iCombi))
            If lMultiSpan Then
                AddCellule(LC3, MyBordures(i), PositionTexteInCell.Centre, CStr(i + 1))
            End If
            If lElastic Then
            Else
                '** Affichage de GammaM
                If lMixte Then
                    AffichageCritereELU(MyBeam.VerifMixte(iVerif).CritereM, iNodeD, iNodeF, MyBordures(i))
                Else
                    AffichageCritereELU(MyBeam.VerifAcier(iVerif).CritereM, iNodeD, iNodeF, MyBordures(i))
                End If
                '** Affichage de GammaV
                If lMixte Then
                    AffichageCritereELU(MyBeam.VerifMixte(iVerif).CritereV, iNodeD, iNodeF, MyBordures(i))
                Else
                    AffichageCritereELU(MyBeam.VerifAcier(iVerif).CritereV, iNodeD, iNodeF, MyBordures(i))
                End If
            End If
        Next

    End Sub

    Private Sub AffichageCritereELU(Critere As cls_Critere, iNode1 As Integer, iNode2 As Integer, vBordure As Integer)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Extraction et affichage de la valeur d'un critère sur une travée
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim ValCrit As Decimal
        Dim iNodeM As Integer
        Dim lMaxi As Boolean

        Dim StyleG As String = ""
        Dim StyleGFin As String = ""

        '--> Initialisation

        Critere.EnveloppeCritereTravee(iNode1, iNode2, ValCrit, iNodeM)
        lMaxi = IsEqual(ValCrit, Critere.CritereMax)
        If lMaxi Then
            StyleG = "\G"
            StyleGFin = "\g"
        End If

        '--> Affichage

        AddCellule(LC3, vBordure, PositionTexteInCell.Centre, StyleG & GetStringInUnit(ValCrit, Enu_TypeVariable.SansType, 3, 2, False) & " (N" & CStr(iNodeM) & ")" & StyleGFin)

    End Sub

    Private Sub EditionVerificationsELUCombi(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage détaillé des critères ELU par combinaison
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NCOL As Integer
        Dim iCombi As Integer

        '--> Initialisation

        SautePage()

        AddTitreNdC(2, BlocELU("ULS_COMBI_CHECK"))

        '--> Tableau

        EnteteTableauCriteresELU(MyBeam, ncol)

        For iCombi = 0 To MyBeam.CombiA_ELU.nbCombi - 1
            AffichageTableauCriteresELU(MyBeam, (MyBeam.NbTravees > 1), NCOL, iCombi)
        Next

        FinTableau()

        Exit Sub


        For i = 0 To MyBeam.CombiA_ELU.nbCombi - 1
            AddTitreNdC(3, BlocELU("ULS_COMBIS") & " " & MyBeam.CombiA_ELU.Symbole(i))

        Next

        SauteLigne()

        AddLigneNDC("\TABLEAU 18")

        InitialiseLigne(6, HLIGNE, True)
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocELU("SECTION"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocELU("SPAN"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x(m)GUD")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-M\=")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-V\=")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-MV\=")

        InitialiseLigne(6, HLIGNE, True)
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocELU("SECTION"))
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocELU("SPAN"))
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x(m)GUD")
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-M\=")
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-V\=")
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-MV\=")



        FinTableau()
    End Sub
#End Region

#Region "***Edition des calculs aux ELS***"

    Private Sub EditionVerificationsELS(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des vérifications ELS (en phase finale pour les poutres mixtes)
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        SautePage()

        AddTitreNdC(1, BlocELS("SLS_CHECKS"))

        '# Edition des flèches

        EditionELSFleches(MyBeam)

        '# Edtion des fréquences propres

        EditionELSFrequencesPropres(MyBeam)

        '# Edition de la méthode Hivoss

        If MyBeam.Param.HivossParam.lHivossMethod Then
            EditionMethodeHivoss(MyBeam)
        End If
    End Sub

    Private Sub EditionELSFleches(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des flèches 
        '-------------------------------------------------------------------------------------------

        AddTitreNdC(2, BlocELS("DEFLECTIONS"))

        '--> Flèches par cas de charge

        AddTitreNdC(3, BlocELS("DEFLECTIONS_LOADCASES"))

        EditionSLSFlechesParCdC(MyBeam)

        '--> Flèches par combinaison

        AddTitreNdC(3, BlocELS("DEFLECTIONS_COMBI"))

        EditionSLSFlechesParCombi(MyBeam)

    End Sub

    '=== EDITION DES FREQUENCES PROPRES ==========================================================================================

    Private Sub EditionELSFrequencesPropres(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des flèches 
        '-------------------------------------------------------------------------------------------

        AddTitreNdC(2, BlocELS("EIGENFREQUENCIES"))

    End Sub


    '=== EDITION DES FLECHES ==========================================================================================

    Private Sub EditionSLSFlechesParCombi(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des flèches par combinaison
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMultispan As Boolean = (MyBeam.NbTravees > 1)
        Dim NCOL As Integer
        Dim LargCol() As Single = Nothing

        '--> Initialisation

        If MyBeam.CombiA_ELS.nbCombi = 0 Then Exit Sub

        '--> Tableau Entête

        EnteteTableauFlecheCdC(lMultispan, NCOL, LargCol, True)

        '--> Boucle sur les cas de charge

        For jCombi As Integer = 0 To MyBeam.CombiA_ELS.nbCombi - 1
            'If MyBeam.ChargesA(jCdc).lRunCalcul Then

            If nbLignes + MyBeam.NbTravees * 1.5 > MAXLIGNEPPAG Then
                FinTableau()
                SautePage()
                EnteteTableauFlecheCdC(lMultispan, NCOL, LargCol, True)
            End If

            LigneTableauFlecheCombi(MyBeam, jCombi, lMultispan, NCOL, LargCol)

            'End If
        Next

        FinTableau()
    End Sub

    Private Sub LigneTableauFlecheCombi(MyBeam As cls_Poutre, iCombi As Integer, lMultiSpan As Boolean, NCOL As Integer, LargCol() As Single)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ligne pour le tableau des flèches par cdc
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyBordures(MyBeam.IndiceDerniereTravee) As Integer
        Dim iTraveeDeb As Integer = MyBeam.IndicePremiereTravee
        Dim iTraveeFin As Integer = MyBeam.IndiceDerniereTravee
        Dim FlechesMax() As Decimal = Nothing
        Dim iCell As Integer
        Dim RatioX As Decimal
        Dim ChaineRatioX As String
        Dim UZCombi() As Decimal = Nothing
        Const lCombiRetrait As Boolean = True

        '--> Initialisations

        For i As Integer = iTraveeDeb To iTraveeFin
            MyBordures(i) = Bordures.Gauche + Bordures.Droite
        Next
        MyBordures(iTraveeDeb) += Bordures.Haut
        MyBordures(iTraveeFin) += Bordures.Bas

        MyBeam.CombiA_ELS.CombineFleches(iCombi, MyBeam.Nodes.nbNodes, MyBeam.ChargesA, UZCombi, lCombiRetrait)

        ExtraireFlecheEnveloppes(UZCombi, iTraveeDeb, iTraveeFin, MyBeam.Nodes.iNodeExtTrav, FlechesMax)

        '--> Traitement

        For i As Integer = iTraveeDeb To iTraveeFin
            iCell = 0
            InitialiseLigne(NCOL, HLIGNE)
            If i = iTraveeDeb Then
                AddCellule(LargCol(0), MyBordures(i), PositionTexteInCell.Gauche, MyBeam.CombiA_ELS.Symbole(iCombi))
            Else
                AddCellule(LargCol(0), MyBordures(i), PositionTexteInCell.Centre, "")
            End If
            If lMultiSpan Then
                AddCellule(LargCol(1), MyBordures(i), PositionTexteInCell.Centre, CStr(i + 1))
                iCell = 1
            End If
            AddCellule(LargCol(iCell + 1), MyBordures(i) - Bordures.Droite, PositionTexteInCell.Gauche, GetStringInUnit(-FlechesMax(i), Enu_TypeVariable.Dimension, 3, 3, True))
            If Math.Abs(FlechesMax(i)) > 0 Then
                RatioX = Math.Abs(MyBeam.LongueurTravee(i) / FlechesMax(i))
                ChaineRatioX = "(L/" & GetStringInUnit(RatioX, Enu_TypeVariable.SansType, 3, 0, False) & ")"
            Else
                ChaineRatioX = ""
            End If
            AddCellule(LargCol(iCell + 2), MyBordures(i) - Bordures.Gauche, PositionTexteInCell.Gauche, ChaineRatioX)

        Next
    End Sub

    Private Sub EditionSLSFlechesParCdC(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des flèches par cas de charge
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMultispan As Boolean = (MyBeam.NbTravees > 1)
        Dim NCOL As Integer
        Dim LargCol() As Single = Nothing

        '--> Tableau Entête

        EnteteTableauFlecheCdC(lMultispan, NCOL, LargCol, False)

        '--> Boucle sur les cas de charge

        For jCdc As Integer = 0 To MyBeam.ChargesA.Count - 1
            If MyBeam.ChargesA(jCdc).lRunCalcul Then

                If nbLignes + MyBeam.NbTravees * 1.5 > MAXLIGNEPPAG Then
                    FinTableau()
                    SautePage()
                    EnteteTableauFlecheCdC(lMultispan, NCOL, LargCol, False)
                End If

                LigneTableauFlecheCdc(MyBeam, jCdc, lMultispan, NCOL, LargCol)

            End If
        Next

        FinTableau()
    End Sub

    Private Sub LigneTableauFlecheCdc(MyBeam As cls_Poutre, iCase As Integer, lMultiSpan As Boolean, NCOL As Integer, LargCol() As Single)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ligne pour le tableau des flèches par cdc
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyBordures(MyBeam.IndiceDerniereTravee) As Integer
        Dim iTraveeDeb As Integer = MyBeam.IndicePremiereTravee
        Dim iTraveeFin As Integer = MyBeam.IndiceDerniereTravee
        Dim FlechesMax() As Decimal
        Dim iCell As Integer
        Dim RatioX As Decimal
        Dim ChaineRatioX As String

        '--> Initialisations

        For i As Integer = iTraveeDeb To iTraveeFin
            MyBordures(i) = Bordures.Gauche + Bordures.Droite
        Next
        MyBordures(iTraveeDeb) += Bordures.Haut
        MyBordures(iTraveeFin) += Bordures.Bas

        ExtraireFlecheEnveloppes(MyBeam.ChargesA(iCase).UZ, iTraveeDeb, iTraveeFin, MyBeam.Nodes.iNodeExtTrav, FlechesMax)

        '--> Traitement

        For i As Integer = iTraveeDeb To iTraveeFin
            iCell = 0
            InitialiseLigne(NCOL, HLIGNE)
            If i = iTraveeDeb Then
                AddCellule(LargCol(0), MyBordures(i), PositionTexteInCell.Gauche, MyBeam.ChargesA(iCase).Nom & " (" & MyBeam.ChargesA(iCase).Symbol & ")")
            Else
                AddCellule(LargCol(0), MyBordures(i), PositionTexteInCell.Centre, "")
            End If
            If lMultiSpan Then
                AddCellule(LargCol(1), MyBordures(i), PositionTexteInCell.Centre, CStr(i + 1))
                iCell = 1
            End If
            AddCellule(LargCol(iCell + 1), MyBordures(i) - Bordures.Droite, PositionTexteInCell.Gauche, GetStringInUnit(-FlechesMax(i), Enu_TypeVariable.Dimension, 3, 3, True))
            If Math.Abs(FlechesMax(i)) > 0 Then
                RatioX = Math.Abs(MyBeam.LongueurTravee(i) / FlechesMax(i))
                ChaineRatioX = "(L/" & GetStringInUnit(RatioX, Enu_TypeVariable.SansType, 3, 0, False) & ")"
            Else
                ChaineRatioX = ""
            End If
            AddCellule(LargCol(iCell + 2), MyBordures(i) - Bordures.Gauche, PositionTexteInCell.Gauche, ChaineRatioX)

        Next
    End Sub

    Private Sub ExtraireFlecheEnveloppes(UZ() As Decimal, iTravDeb As Integer, iTravFin As Integer, IndiceNoteT(,) As Integer, ByRef FlechesMaxi() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Recherche des valeurs de flèches maxi par travée
        '-------------------------------------------------------------------------------------------
        '   UZ          [E] :   Tableau des flèches par noeuds
        '   iTravDeb    [E] :   Indice de la première travée
        '   iTravFin    [E] :   Indice de la dernière travée
        '   IndiceNoteT [E] :   Indice des noeuds aux extrémités des travées
        '   FlechesMaxi [S] :   Flèches maxi par travée
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim jTravee As Integer
        Dim iNode As Integer

        '--> Initialisation

        ReDim FlechesMaxi(iTravFin)

        '--> Boucle sur les travées

        For jTravee = iTravDeb To iTravFin
            FlechesMaxi(jTravee) = UZ(IndiceNoteT(jTravee, 0))
            For iNode = IndiceNoteT(jTravee, 0) + 1 To IndiceNoteT(jTravee, 1)

                If IsGreater(Math.Abs(UZ(iNode)), Math.Abs(FlechesMaxi(jTravee))) Then

                    FlechesMaxi(jTravee) = UZ(iNode)

                End If

            Next
        Next

    End Sub

    Private Sub EnteteTableauFlecheCdC(lMultiSpan As Boolean, ByRef NCOL As Integer, ByRef LargCol() As Single, lCombi As Boolean)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition de l'entete pour le tableau des flèches par cdc
        '-------------------------------------------------------------------------------------------
        '   lMutliSpan  [E] :   Indique si poutre multitravée
        '   NCOL        [S] :   Indique si tableau pour les cas de charge ou pour les combinaisons
        '   LargCol     [S] :   Largeurs des colonnes du tableau
        '   LCombi      [E] :   Indique si teableau pour les combinaisons ou les cas de charges
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Pos As Integer
        Dim iCell As Integer = 0

        '--> Initialisation

        NCOL = 3
        If lMultiSpan Then NCOL += 1

        Pos = 10

        AddLigneNDC("\TABLEAU " & CStr(Pos))

        ReDim LargCol(NCOL - 1)
        LargCol(0) = CSng(35)
        LargCol(1) = CSng(LC3)

        LargCol(NCOL - 2) = CSng(LC3)
        LargCol(NCOL - 1) = CSng(15)

        '--> Entete

        InitialiseLigne(NCOL, HLIGNEENTETE)
        If lCombi Then
            AddCelluleFond(LargCol(0), Bordures.Tous, PositionTexteInCell.Gauche, BlocELS("COMBINATION"))
        Else
            AddCelluleFond(LargCol(0), Bordures.Tous, PositionTexteInCell.Gauche, BlocELS("LOADCASE"))
        End If
        If lMultiSpan Then
            AddCelluleFond(LargCol(1), Bordures.Tous, PositionTexteInCell.Gauche, BlocELS("SPAN"))
            iCell = 1
        End If
        AddCelluleFond(LargCol(iCell + 1), Bordures.Tous - Bordures.Droite, PositionTexteInCell.Gauche, BlocELS("DEFLECTIONS"))
        AddCelluleFond(LargCol(iCell + 2), Bordures.Tous - Bordures.Gauche, PositionTexteInCell.Centre, "")


    End Sub

#End Region


#Region "   Edition ELS méthode HIVOSS "

    Private Sub EditionMethodeHivoss(ByVal MyBeam As cls_Poutre) ', ByVal MyFreq(,) As Double, ByVal FlechesCasElem(,) As Double)
        '----------------------------------------------------------------------------------------------
        '   23/11/23 :  Création - Version 1 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des résultats de la méthode Hivoss
        '----------------------------------------------------------------------------------------------
        '   MyBeam          [E] :   Poutre traitée
        '   MyFreq          [E] :   Table des frequences propres pour les combinaisons de masse
        '   FlecheCasElem   [E] :   Table des flèches verticales sous charges élémentaires
        '----------------------------------------------------------------------------------------------

        '--[ Déclarations

        Dim AllFloorVibration As New Dictionary(Of Integer, strHivossTable)
        Const TABVAR As String = " :\T45"
        Const DFORMAT As String = "0"
        Dim Frequency, ModalMass As Decimal
        Dim HResult As String
        Dim HVal As Decimal
        Dim Reactions() As Decimal
        Dim IndConfort As Integer
        Dim TableConfort(2) As String
        Dim TableUsage As New List(Of String)
        Dim lDefini() As Boolean
        Dim lMixte As Boolean
        Dim FreqDalle As Decimal
        Dim FreqBeam As Decimal
        Dim MySymb As String

        '--[ Titre

        SautePage()
        AddTitreNdC(2, BlocHiVoss("HIVOSSTITLE"))

        '--[ Initialisations

        lMixte = MyBeam.lMixte

        TableConfort(0) = BlocHiVoss("CRECOMMENDED")
        TableConfort(1) = BlocHiVoss("CCRITICAL")
        TableConfort(2) = BlocHiVoss("CNOTRECOMMENDED")
        TableUsage.Clear()
        TableUsage.Add(BlocHiVoss("UCRITICAL"))
        TableUsage.Add(BlocHiVoss("UHOSPITAL"))
        TableUsage.Add(BlocHiVoss("USCHOOL"))
        TableUsage.Add(BlocHiVoss("URESIDENTIAL"))
        TableUsage.Add(BlocHiVoss("UOFFICE"))
        TableUsage.Add(BlocHiVoss("UMEETING"))
        TableUsage.Add(BlocHiVoss("USENIOR"))
        TableUsage.Add(BlocHiVoss("UHOTEL"))
        TableUsage.Add(BlocHiVoss("UINDUSTRIAL"))
        TableUsage.Add(BlocHiVoss("USPORTS"))

        MyBeam.Param.HivossParam.CalculAmortissement()

        ChargerValeursHivoss(AllFloorVibration)

        'Frequency = CDec(MyFreq(MyBeam.HivossParam.IndCombiQ, MyBeam.HivossParam.IndChargeQ))
        'ModalMass = ModalMasses(MyBeam)

        ''--[ Prise en compte de la fréquence propre de dalle pour les poutres mixtes:

        'If lMixte And MyBeam.HivossParam.lFreqDalle And InfoACB.lExpert Then
        '    FrequenceDalle(MyBeam, FreqDalle)
        '    FreqBeam = Frequency
        '    Frequency = CDec(1 / Math.Sqrt(1 / FreqBeam ^ 2 + 1 / FreqDalle ^ 2))
        'End If

        ''--[ Affichages des données

        'AddLigneNDC(TABW2 & BlocELS("USAGE") & TABVAR & TableUsage(MyBeam.HivossParam.IndUsage))
        'SauteLigne()
        'AddLigneNDC(TABW2 & BlocELS("DSTRUC") & TABVAR & "D1 = " & Format(MyBeam.HivossParam.Amortissement(1), DFORMAT) & " %")
        'AddLigneNDC(TABW2 & BlocELS("DFURNITURE") & TABVAR & "D2 = " & Format(MyBeam.HivossParam.Amortissement(2), DFORMAT) & " %")
        'AddLigneNDC(TABW2 & BlocELS("DFINISHING") & TABVAR & "D3 = " & Format(MyBeam.HivossParam.Amortissement(3), DFORMAT) & " %")
        'AddLigneNDC(TABW2 & BlocELS("DTOTAL") & TABVAR & "D = " & Format(MyBeam.HivossParam.Amortissement(0), DFORMAT) & " %")

        'SauteLigne()

        'AddLigneNDC(TABW2 & BlocELS("COMBIMASS") & TABVAR & "G + 0." & Format(MyBeam.HivossParam.IndCombiQ, DFORMAT) & " Q" & Format(MyBeam.HivossParam.IndChargeQ + 1, "0"))

        'MyBeam.ChargementsDefinis(lDefini)
        'If MyBeam.HivossParam.IndCombiQ > 0 Then
        '    If lDefini(MyBeam.HivossParam.IndChargeQ + 1) Then
        '        If Not (FlecheMaxQ(FlechesCasElem, MyBeam.nSec, MyBeam.HivossParam.IndChargeQ + 1) > 0) Then
        '            AddLigneNDC(TABW2 & RemplaceDollar(BlocELS("WARNNOQ3"), CStr(MyBeam.HivossParam.IndChargeQ + 1)))
        '            AddLigneNDC(TABW2 & BlocELS("WARNNOQ2"))
        '        End If
        '    Else
        '        AddLigneNDC(TABW2 & RemplaceDollar(BlocELS("WARNNOQ1"), CStr(MyBeam.HivossParam.IndChargeQ + 1)))
        '        AddLigneNDC(TABW2 & BlocELS("WARNNOQ2"))
        '    End If
        'End If
        ''--[ Affichage des fréquences propres et de la masse modale

        'SauteLigne()
        'If lMixte And MyBeam.HivossParam.lFreqDalle And InfoACB.lExpert Then
        '    AddLigneNDC(TABW2 & BlocELS("EIGENFB") & TABVAR & GetStringInUnit(FreqBeam, Enu_TypeVariable.Frequence, 3, 1, True))
        '    AddLigneNDC(TABW2 & BlocELS("EIGENFS") & TABVAR & GetStringInUnit(FreqDalle, Enu_TypeVariable.Frequence, 3, 1, True))
        '    AddLigneNDC(TABW2 & BlocELS("EIGENFC") & TABVAR & GetStringInUnit(Frequency, Enu_TypeVariable.Frequence, 3, 1, True))
        'Else
        '    AddLigneNDC(TABW2 & BlocELS("EIGENF") & TABVAR & GetStringInUnit(Frequency, Enu_TypeVariable.Frequence, 3, 1, True))
        'End If
        'SauteLigne()
        'AddLigneNDC(TABW2 & BlocELS("MODALMASS") & TABVAR & GetStringInUnit(ModalMass, Enu_TypeVariable.SansDimension, 3, 0, False) & " kg")

        ''--[ Calcul Hivoss

        'CalculMethodHivoss(AllFloorVibration, CInt(MyBeam.HivossParam.Amortissement(0)), CDec(Frequency), CDec(ModalMass), HResult, HVal)
        'IndConfort = HivossConfortAssessment(MyBeam, HResult)

        'SauteLigne()
        'If HResult = "A" Then
        '    MySymb = "<"
        'ElseIf HResult = "!" Then
        '    MySymb = ">"
        'Else
        '    MySymb = "="
        'End If

        'AddLigneNDC(TABW2 & BlocELS("OSRMS") & TABVAR & "OS-RMS\-90\= " & MySymb & " " & GetStringInUnit(HVal, Enu_TypeVariable.SansDimension, 3, 1, False) & " m/s")
        'AddLigneNDC(TABW2 & BlocELS("CPERCEPTION") & TABVAR & HResult)
        'AddLigneNDC(TABW2 & BlocELS("COMFORTASS") & TABVAR & TableConfort(IndConfort))

        'SautePage()

        'MyNote.AddLigneInRapport("\IMG HIVOSS 5 85 80 NoCadre " _
        '                       & Format(MyBeam.HivossParam.Amortissement(0), DFORMAT) & " " _
        '                       & Format(Frequency, "0.00") & " " _
        '                       & Format(ModalMass, "0.00") & " " _
        '                       & BlocELS("DAMPING"))

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
        '   20/01/09 :  Création - Version 1.00 Beta 3 - POM
        '----------------------------------------------------------------------------------------
        '   Ajoute une ligne dans la note de calcul
        '   La ligne doit être suivie d'un nombre de ligne imposée dans la même page
        '----------------------------------------------------------------------------------------
        '   Ligne           [E] :   Texte à rajouter dans la note de calcul
        '   NbMiniLignes    [E] :   Nombre minimal de lignes devant figurer sous le texte,
        '                           dans la même page
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

    '###############################################################################################################################################

#Region " Edition du modèle de calcul "

    Public Sub ABB_EditeModeleCalcul(MyPoutre As cls_Poutre, indTabElt As Integer)
        '---------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Edition du modèle EF d'un cas de charge
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '   indTabElt       [E] :   Indice de la table d'éléments associée au modèle EF
        '---------------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        MyNote = New Cls_Rapport("Arial", 1.5, 3, 3)

        '--> Génération de la note

        ABB_GenereNoteModeleCalcul(MyPoutre, indTabElt)

        '--> Ouverture de la fenêtre
        Frm_NoteCalcul.ShowDialog()
        Frm_NoteCalcul.Dispose()
        '--> Liberation de la note
        MyNote.Dispose()

    End Sub

    Private Sub ABB_GenereNoteModeleCalcul(MyPoutre As cls_Poutre, indTabElt As Integer)
        '---------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Edition du modèle EF d'un cas de charge
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '   indTabElt       [E] :   Indice de la table d'éléments associée au modèle EF
        '---------------------------------------------------------------------------------------------------

        SautePage()

        '--> Type de calcul (prise en compte de la dalle)

        If MyPoutre.lMixte Or MyPoutre.lEnrobage Then

            Model_EditAssumptions(MyPoutre, indTabElt)

        End If

        '--> Maillage noeud

        Model_EditNodes(MyPoutre)

        '--> Eléments

        Model_EditElements(MyPoutre, indTabElt)

    End Sub

    Private Sub Model_EditElements(MyPoutre As cls_Poutre, indTabElt As Integer)
        '---------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Edition des éléments du modèle EF d'un cas de charge 
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '   indTabElt       [E] :   Indice de la table d'éléments associée au modèle EF
        '---------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTravDeb, iTravFin As Integer
        Dim lMultispan As Boolean
        Dim NCol, PosTab As Integer
        Dim iTravee, i As Integer
        Dim iEltO, iEltE As Integer
        Dim iCompteur As Integer = 0
        Dim NbLignesMax() As Integer = {25, 35}
        Dim iTab As Integer = 0
        Dim Ai, Iyi As Decimal

        '--> Initialisation

        If (nbLignes > MAXLIGNEPPAG - 10) Or (nbLignes > NbLignesMax(1) - 5) Then
            SautePage()
        End If
        lMultispan = (MyPoutre.NbTravees > 1)
        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee
        NbLignesMax(0) = NbLignesMax(1) - nbLignes

        '--> Titre du paragraphe

        AddTitreNdC(1, "Elements")

        '--> Liste des noeuds

        '# Entête

        Model_EnteteTableauElements(lMultispan, NCol, PosTab)

        '# Tableau

        For iTravee = iTravDeb To iTravFin
            iEltO = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iEltE = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1) - 1

            For i = iEltO To iEltE
                iCompteur += 1

                If iCompteur > NbLignesMax(iTab) Then
                    FinTableau()
                    iCompteur = 0
                    iTab = 1
                    SautePage()
                    Model_EnteteTableauElements(lMultispan, NCol, PosTab)
                End If

                Ai = MyPoutre.Elements(indTabElt).Aire(i)
                Iyi = MyPoutre.Elements(indTabElt).InertieY(i)

                Model_LigneTableauElement(lMultispan, NCol, PosTab, i, iTravee, ai, iyi)

            Next

        Next

        FinTableau()

    End Sub

    Private Sub Model_EditNodes(MyPoutre As cls_Poutre)
        '---------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Edition des noeuds du modèle EF d'un cas de charge 
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '---------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTravDeb, iTravFin As Integer
        Dim lMultispan As Boolean
        Dim NCol, PosTab As Integer
        Dim iTravee, i As Integer
        Dim iNodeO, iNodeE As Integer
        Dim iCompteur As Integer = 0
        Dim NbLignesMax() As Integer = {25, 30}
        Dim iTab As Integer = 0

        '--> Initialisation

        lMultispan = (MyPoutre.NbTravees > 1)
        iTravDeb = MyPoutre.IndicePremiereTravee
        iTravFin = MyPoutre.IndiceDerniereTravee
        NbLignesMax(0) = NbLignesMax(1) - nbLignes

        '--> Titre

        AddTitreNdC(1, "Nodes")

        '--> Liste des noeuds

        '# Entête

        Model_EnteteTableauNoeuds(lMultispan, NCol, PosTab)

        '# Tableau

        For iTravee = iTravDeb To iTravFin
            iNodeO = MyPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeE = MyPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            '=== Extrémité gauche

            If iTravee = iTravDeb Then
                Model_LigneTableauNoeuds(lMultispan, NCol, PosTab, 0, iTravee, MyPoutre.Nodes.xTravee(0), MyPoutre.Nodes.xGlobal(0))
            End If

            '=== Lignes intermédiaires

            For i = iNodeO + 1 To iNodeE - 1

                iCompteur += 1

                If iCompteur > NbLignesMax(iTab) Then
                    FinTableau()
                    iCompteur = 0
                    iTab = 1
                    SautePage()
                    Model_EnteteTableauNoeuds(lMultispan, NCol, PosTab)
                End If

                Model_LigneTableauNoeuds(lMultispan, NCol, PosTab, i, iTravee, MyPoutre.Nodes.xTravee(i), MyPoutre.Nodes.xGlobal(i))

            Next

            '=== Appui droite

            Model_LigneTableauNoeuds(lMultispan, NCol, PosTab, iNodeE, iTravee,
                                     MyPoutre.Nodes.xTravee(iNodeE), MyPoutre.Nodes.xGlobal(iNodeE), Not (iTravee = iTravFin))

            iCompteur += 1
        Next

        FinTableau()

    End Sub

    Private Sub Model_LigneTableauNoeuds(lMultiSpan As Boolean, NCol As Integer, Pos As Integer,
                                         iNode As Integer, iTravee As Integer, xPosG As Decimal, xPosT As Decimal, Optional lAppuiInter As Boolean = False)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage d'une ligne de du tableau des noeuds du maillage EF
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [E] :   Nombre de colonnes
        '   Pos         [E] :   Position du tableau / bord gauche
        '   iNode       [E] :   Indice du noeud
        '   iTravee     [E] :   Indice de la travée
        '   xposG,xPosT [E] :   Position globale et dans la travée du noeud
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        InitialiseLigne(NCol, HLIGNE, True)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iNode + 1))

        '# Position et travée

        If lMultiSpan Then
            If lAppuiInter Then
                AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee + 1) & " / " & CStr(iTravee + 2))
                AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False) & " / 0")
            Else
                AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee + 1))
                AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))
            End If

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))
        Else

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))

        End If

    End Sub

    Private Sub Model_LigneTableauElement(lMultiSpan As Boolean, NCol As Integer, Pos As Integer,
                                          iElt As Integer, iTravee As Integer, Ai As Decimal, Iyi As Decimal)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage d'une ligne de du tableau des noeuds du maillage EF
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [E] :   Nombre de colonnes
        '   Pos         [E] :   Position du tableau / bord gauche
        '   iElt        [E] :   Indice de l'élément
        '   iTravee     [E] :   Indice de la travée
        '   Ai, Iyi     [E] :   Aire et inertie de l'élément
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        InitialiseLigne(NCol, HLIGNE, True)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iElt + 1))
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "N" & CStr(iElt + 1) & "-N" & CStr(iElt + 2))

        '# Travée

        If lMultiSpan Then

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee + 1))

        End If

        '# Propriétés

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(Ai, Enu_TypeVariable.AireCM2, 4, 3, False))
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(Iyi, Enu_TypeVariable.InertieCM4, 4, 3, False))

    End Sub

    Private Sub Model_EnteteTableauNoeuds(lMultiSpan As Boolean, ByRef NCol As Integer, ByRef Pos As Integer)
        '-------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entete du tableau pour l'affichage des noeuds du modèle EF
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [S] :   Nombre de colonnes
        '   Pos         [S] :   Position du tableau / bord gauche
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        Pos = 10
        If lMultiSpan Then NCol = 4 Else NCol = 2

        AddLigneNDC("\TABLEAU " & CStr(Pos))

        InitialiseLigne(NCol, HLIGNEENTETE, True)
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "NODE")
        If lMultiSpan Then
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "SPAN")
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x\-span\= (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x\-global\= (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        Else
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        End If

    End Sub

    Private Sub Model_EnteteTableauElements(lMultiSpan As Boolean, ByRef NCol As Integer, ByRef Pos As Integer)
        '-------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entete du tableau pour l'affichage des noeuds du modèle EF
        '-------------------------------------------------------------------------------------------
        '   lMultiSpan  [E] :   Indique si plusieurs travées
        '   NCol        [S] :   Nombre de colonnes
        '   Pos         [S] :   Position du tableau / bord gauche
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Initialisation

        Pos = 10
        If lMultiSpan Then NCol = 5 Else NCol = 4

        AddLigneNDC("\TABLEAU " & CStr(Pos))

        InitialiseLigne(NCol, HLIGNEENTETE, True)
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "ELEMENT")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "Connec.")
        If lMultiSpan Then
            AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "SPAN")
        End If
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "Ai (cm\+2\=)")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "Iyi (cm4)")

    End Sub

    Private Sub Model_EditAssumptions(MyPoutre As cls_Poutre, indTabElt As Integer)
        '---------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Edition du modèle EF d'un cas de charge - Hypothèses de calcul (poutres mixtes)
        '---------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '   indTabElt       [E] :   Indice de la table d'éléments associée au modèle EF
        '---------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nEqDalle As Decimal = MyPoutre.Elements(indTabElt).nEqDalle
        Dim nEqEnrob As Decimal = MyPoutre.Elements(indTabElt).nEqEnrob
        Const TAB20 As String = "\T20"
        Const TAB50 As String = "\T50"

        '--> Titre

        AddTitreNdC(1, "Assumptions")

        '--> Hypothèses relatives à la dalle

        If MyPoutre.lMixte Then
            AddTitreNdC(2, "Slab")
            If MyPoutre.Elements(indTabElt).lMixte Then
                AddLigneNDC(TAB20 & "Composite action")
                AddLigneNDC(TAB20 & "Modular ratio:" & TAB50 & "n = " & GetStringInUnit(nEqDalle, Enu_TypeVariable.SansType, 3, 2, False))
                If MyPoutre.Param.lLargeurEfficaceSimplifiee Then
                    AddLigneNDC(TAB20 & "Effective width:" & TAB50 & "according to EN 1994-1-1 § 5.4.1.2 (4)")
                Else
                    AddLigneNDC(TAB20 & "Effective width:" & TAB50 & "according to EN 1994-1-1 Figure 5.1")
                End If

            Else
                AddLigneNDC(TAB20 & "The slab is not taken into account (no composite action)")
            End If
        End If

        '--> Hypothèses relatives à l'enrobage partiel

        If MyPoutre.lEnrobage Then
            AddTitreNdC(2, "Partial encasement")
            AddLigneNDC(TAB20 & "Composite action")
            AddLigneNDC(TAB20 & "Modular ratio:" & TAB50 & "n = " & GetStringInUnit(nEqEnrob, Enu_TypeVariable.SansType, 3, 2, False))
        End If

    End Sub

#End Region



End Module
