Imports System.Collections.Specialized.BitVector32
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports PMXMoteur2

Module Mod_NoteCalcul

#Region "   Déclarations "

    '--> Tabulation
    Private Const TABW1 As String = "\TW1"
    Private Const TABW2 As String = "\TW2"
    Private Const TABW3 As String = "\TW3"


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

    Private BlocG As New Dictionary(Of String, String)
    Private BlocSP As New Dictionary(Of String, String)
    Private BlocAnalyse As New Dictionary(Of String, String)
    Private BlocELU As New Dictionary(Of String, String)
    Private BlocELS As New Dictionary(Of String, String)
    Private BlocHiVoss As New Dictionary(Of String, String)

    Private ReadOnly IndTableau As Integer = 0
    Private ReadOnly IndFigure As Integer = 0
    Private nbLignes As Decimal = 0
    Private Const MAXLIGNEPPAG As Integer = 56
    Private Const EquivalenceLigneTableau As Decimal = 1.2

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
        'MyProjet.Poutres(MyProjet.IndEnCours).InitialisePoidsPropres()
        MyProjet.Poutres(MyProjet.IndEnCours).Initialise_CoefficientsCombinaisons()
        MyProjet.Poutres(MyProjet.IndEnCours).CalculArmaturesTransversales()

        strRacineELU = BlocG("ULS")
        strRacineELS = BlocG("SLS")
        strRacineELF = BlocG("FLS")

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

        MyNote.EtiquetteLigne(0, 0) = BlocG("USER")
        MyNote.EtiquetteLigne(1, 0) = BlocG("SOCIETE")
        MyNote.EtiquetteLigne(2, 0) = BlocG("PROJET")

        Const DPTS As String = ":  "
        MyNote.EtiquetteLigne(0, 1) = DPTS & MyPrjt.Utilisateur
        MyNote.EtiquetteLigne(1, 1) = DPTS & MyPrjt.Entreprise
        MyNote.EtiquetteLigne(2, 1) = DPTS & MyPrjt.Nom

        MyNote.FootNote = BlocG("FOOTNOTE")

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
        BlocLine.CreationBloc(BlocG)

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

        '--[ Maintiens par le bac en phsase de construction par le bac acier

        EditionParametresMaintienBac(MyBeam)

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

        AddTitreNdC(1, BlocG("PARAMETERS"))

        '--[ Avertissement si Mode Expert

        If LogicielOptions.lExpert Then
            AddLigneNDC(TABW1 & "\G" & BlocG("WEXPERT") & "\g")
            AddLigneNDC(TABW1 & "\G" & BlocG("WEXPERT2") & "\g")
        End If

        '--[ Paramètres Généraux

        AddTitreNdC(2, BlocG("MAINP"))

        AddTitreNdC(3, BlocG("LONGIPARAMETERS"))

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("NONCOMPOBEAM") & "\g")
            Case cls_Section.Enum_TypeSection.AcierSeulEnrobage
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("NONCOMPOBEAMPARTENCAS") & "\g")
            Case cls_Section.Enum_TypeSection.Mixte
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("COMPOBEAM") & "\g")
            Case cls_Section.Enum_TypeSection.MixteEnrobage
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("COMPOBEAMPARTENCAS") & "\g")
            Case cls_Section.Enum_TypeSection.SFB
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("NONCOMPOSFB") & "\g")
            Case cls_Section.Enum_TypeSection.SFBmixte
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("COMPOSFB") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_A
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("NONCOMPOIFB_A") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_Amixte
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("COMPOIFB_A") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_B
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("NONCOMPOIFB_B") & "\g")
            Case cls_Section.Enum_TypeSection.IFB_Bmixte
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("COMPOIFB_B") & "\g")
            Case cls_Section.Enum_TypeSection.SAB
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("NONCOMPOSAB") & "\g")
            Case cls_Section.Enum_TypeSection.SABmixte
                AddLigneNDC(TABW2 & BlocG("CSTYPE") & TABAFF2 & "\G" & BlocG("COMPOSAB") & "\g")
        End Select

        AddLigneNDC(TABW2 & BlocG("LENGTHBEAM") & TABAFF2 & "L\-tot\= = " & GetStringInUnit(MyBeam.LongueurTotale, Enu_TypeVariable.Longueur, 4, 2, True))
        AddLigneNDC(TABW2 & BlocG("NBTOTSPAN") & TABAFF2 & GetStringInUnit(MyBeam.NbTravees, Enu_TypeVariable.SansType, 2, 0, True))


        ' -->Tableau récapitulatif de la poutre 
        If MyBeam.NbTravees > 1 Then
            AddLigneNDC(TABW2 & BlocG("BEAMCHAR"))
            SauteLigne()
            AddLigneNDC("\TABLEAU 18")
            InitialiseLigne(5, HLIGNE, True)
            AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, "i")
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("TYPE_BEAM"))
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LENGHT") & "(" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LEFTSUPPORT"))
            AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("RIGHTSUPPORT"))

            Dim strTypTravee As String = ""
            Dim iTraveeAffichee As Integer = 1

            For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
                InitialiseLigne(5, HLIGNE, True)
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, iTraveeAffichee)
                Select Case MyBeam.TypTravee(i)
                    Case cls_Poutre.EnuTypeTravee.ConsoleGauche
                        strTypTravee = BlocG("LEFTCANT")
                    Case cls_Poutre.EnuTypeTravee.DeuxAppuis
                        strTypTravee = BlocG("SPAN")
                    Case cls_Poutre.EnuTypeTravee.ConsoleDroite
                        strTypTravee = BlocG("RIGHTCANT")
                End Select
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, strTypTravee)
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, MyBeam.LongueurTravee(i))
                Select Case MyBeam.TypTravee(i)
                    Case cls_Poutre.EnuTypeTravee.ConsoleGauche
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("FREE"))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("PINNED"))
                    Case cls_Poutre.EnuTypeTravee.DeuxAppuis
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("PINNED"))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("PINNED"))
                    Case cls_Poutre.EnuTypeTravee.ConsoleDroite
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("PINNED"))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("FREE"))
                End Select

                iTraveeAffichee += 1

            Next

            FinTableau()

        Else
            'AddLigneNDC(TABW2 & BlocG("RIGHTSUPPORT") & TABAFF2 & BlocG("PINNED"))
            'AddLigneNDC(TABW2 & BlocG("LEFTSUPPORT") & TABAFF2 & BlocG("PINNED"))

        End If

        If MyBeam.lMixte Then
            AddLigneNDC(TABW2 & BlocG("TYPEOFBEAM") & TABAFF2 & BlocG("COMPOSITE"))
        Else
            AddLigneNDC(TABW2 & BlocG("TYPEOFBEAM") & TABAFF2 & BlocG("NONCOMPOSITE"))
        End If

        If MyBeam.Dalle.lMixte Then
            AddLigneNDC(TABW2 & BlocG("TYPEOFSLAB") & TABAFF2 & BlocG("COMPOSITE"))
        Else
            AddLigneNDC(TABW2 & BlocG("TYPEOFSLAB") & TABAFF2 & BlocG("NONCOMPOSITE"))
        End If

        'If MyBeam.Section.lEnrobage Then
        '    AddLigneNDC(TABW2 & BlocG("TYPEOFSTEELSECTION") & TABAFF2 & BlocG("PARTIALLY_ENCASED"))
        'Else
        '    AddLigneNDC(TABW2 & BlocG("TYPEOFSTEELSECTION") & TABAFF2 & BlocG("NOT_PARTIALLY_ENCASED"))
        'End If

        'Ajout dessin de la poutre en cours
        AddLigneNDC("\IMG PORTEE 2 95 13 NoCadre")
        SauteLigne()

        '--[ Position de la poutre
        AddTitreNdC(3, BlocG("TRANSPARAMETERS"))
        If MyBeam.lIntermediaire Then
            AddLigneNDC(TABW2 & BlocG("BEAMPOSITION") & TABAFF2 & BlocG("INTERMEDIATEBEAM"))
        Else
            AddLigneNDC(TABW2 & BlocG("BEAMPOSITION") & TABAFF2 & BlocG("EDGEBEAM"))
        End If

        If MyBeam.lMixte And (MyBeam.lTremieGauche Or MyBeam.lTremieDroite) Then SauteLigne()

        AddLigneNDC(TABW2 & BlocG("LEFTSPACING") & TABAFF2 & "d\-1\= = " & GetStringInUnit(MyBeam.EntraxeD1, Enu_TypeVariable.Longueur, 4, 2, True))
        AddLigneNDC(TABW2 & BlocG("RIGHTSPACING") & TABAFF2 & "d\-2\= = " & GetStringInUnit(MyBeam.EntraxeD2, Enu_TypeVariable.Longueur, 4, 2, True))

        '==> POM

        If MyBeam.lMixte Then

            If MyBeam.lTremieGauche Then
                AddLigneNDC(TABW2 & BlocG("LEFTOPENING") & TABAFF2 & "d\-sl,1\= = " & GetStringInUnit(MyBeam.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, True))
            End If

            If MyBeam.lTremieDroite Then
                AddLigneNDC(TABW2 & BlocG("RIGHTOPENING") & TABAFF2 & "d\-sl,2\= = " & GetStringInUnit(MyBeam.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, True))
            End If

            If ((Not MyBeam.lTremieDroite) And (Not MyBeam.lTremieGauche)) Then
                AddLigneNDC(TABW2 & BlocG("NOSLABOPENING"))
            End If

        End If

        'If MyBeam.lMixte Then
        '    If MyBeam.lTremieGauche Then
        '        AddLigneNDC(TABW2 & BlocG("ISLEFTOPENING") & TABAFF2 & BlocG("YES"))
        '        AddLigneNDC(TABW2 & BlocG("DISTLEFTOPENING") & TABAFF2 & "d\-sl,1\= = " & GetStringInUnit(MyBeam.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, True))
        '        'AddLigneNDC(TABW2 & Bloc("WIDTHLEFTOPENING") & TABAFF2 & GetStringInUnit(MyBeam.EntraxeD1 - 2 * MyBeam.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, True))
        '    Else
        '        AddLigneNDC(TABW2 & BlocG("ISLEFTOPENING") & TABAFF2 & BlocG("NO"))
        '    End If
        'End If

        'If MyBeam.lMixte And (MyBeam.lTremieGauche Or MyBeam.lTremieDroite) Then SauteLigne()

        'If MyBeam.lMixte Then
        '    If MyBeam.lTremieDroite Then
        '        AddLigneNDC(TABW2 & BlocG("ISRIGHTOPENING") & TABAFF2 & BlocG("YES"))
        '        AddLigneNDC(TABW2 & BlocG("DISTRIGHTOPENING") & TABAFF2 & "d\-sl,2\= = " & GetStringInUnit(MyBeam.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, True))
        '        'AddLigneNDC(TABW2 & Bloc("WIDTHRIGHTOPENING") & TABAFF2 & GetStringInUnit(MyBeam.EntraxeD2 - 2 * MyBeam.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, True))
        '    Else
        '        AddLigneNDC(TABW2 & BlocG("ISRIGHTOPENING") & TABAFF2 & BlocG("NO"))
        '    End If
        'End If

        '==

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
        '   10/07/23 :  Création - Version 1.00 - POM+GUD
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base d'une section
        '----------------------------------------------------------------------------------------------

        Dim lLamine As Boolean = MyBeam.Section.lLamine
        Dim lPRSSym As Boolean = (MyBeam.Section.ProfilA.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym)
        Dim lSymetric As Boolean = lLamine Or lPRSSym

        SautePage()

        AddTitreNdC(2, BlocG("SPROFILE"))
        AddTitreNdC(3, BlocG("GENERAL"))
        Select Case MyBeam.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine
                AddLigneNDC(TABW2 & BlocG("STYPE") & TABAFF & BlocG("HOTROLLED"))
                AddLigneNDC(TABW2 & BlocG("SNAME") & TABAFF & MyBeam.Section.ProfilA.NomProfile)
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym
                AddLigneNDC(TABW2 & BlocG("STYPE") & TABAFF & BlocG("PRS_BI_SYM"))
            Case cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym
                AddLigneNDC(TABW2 & BlocG("STYPE") & TABAFF & BlocG("PRS_MONO_SYM"))
        End Select

        AddTitreNdC(3, BlocG("DIM_PROFILE"))

        AddLigneNDC(TABW2 & BlocG("HS_PROFILE") & TABAFF & "h\-s\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ha, Enu_TypeVariable.Dimension, 3, -1, True))
        If lLamine Then
            AddLigneNDC(TABW2 & BlocG("BF_PROFILE") & TABAFF & "b\-f\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfs, Enu_TypeVariable.Dimension, 3, -1, True))
            AddLigneNDC(TABW2 & BlocG("TF_PROFILE") & TABAFF & "t\-f\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, -1, True))
        Else
            If lPRSSym Then
                AddLigneNDC(TABW2 & BlocG("BF_PROFILE") & TABAFF & "b\-fs\==b\-fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfs, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("TF_PROFILE") & TABAFF & "t\-fs\==t\-fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 4, 1, True))
            Else
                AddLigneNDC(TABW2 & BlocG("BFS_PROFILE") & TABAFF & "b\-fs\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfs, Enu_TypeVariable.Dimension, 3, -1, True))
                AddLigneNDC(TABW2 & BlocG("TFS_PROFILE") & TABAFF & "t\-fs\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfs, Enu_TypeVariable.Dimension, 3, -1, True))
                AddLigneNDC(TABW2 & BlocG("BFI_PROFILE") & TABAFF & "b\-fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Bfi, Enu_TypeVariable.Dimension, 3, -1, True))
                AddLigneNDC(TABW2 & BlocG("TFI_PROFILE") & TABAFF & "t\-fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tfi, Enu_TypeVariable.Dimension, 3, -1, True))
            End If
        End If
        AddLigneNDC(TABW2 & BlocG("HW_PROFILE") & TABAFF & "h\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.HauteurAmeHw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("DW_PROFILE") & TABAFF & "d\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.HauteurAmeDw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("TW_PROFILE") & TABAFF & "t\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Tw, Enu_TypeVariable.Dimension, 3, -1, True))

        If lLamine Then
            AddLigneNDC(TABW2 & BlocG("RC_PROFILE") & TABAFF & "r" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Rcs, Enu_TypeVariable.Dimension, 2, -1, True))
            'AddLigneNDC(TABW2 & Bloc("RCI_PROFILE") & TABAFF & "r\-ci\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.r_ci, Enu_TypeVariable.Dimension, 4, 0, True))
        Else
            '== On n'affiche pas la gorge de soudure (non définie par l'utilisateur)
            'AddLigneNDC(TABW2 & BlocG("AWELD_PROFILE") & TABAFF & "a" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.aW, Enu_TypeVariable.Dimension, 2, -1, True))
        End If

        AddTitreNdC(3, BlocG("CHAR_PROFILE"))
        MyBeam.Section.ProfilA.InitialiseProprietes()

        AddLigneNDC(TABW2 & BlocG("A_PROFILE") & TABAFF & "A" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Aire, Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("AV_PROFILE") & TABAFF & "A\-v\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.AireAv, Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("IY_PROFILE") & TABAFF & "I\-y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieY, Enu_TypeVariable.InertieCM4, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("IZ_PROFILE") & TABAFF & "I\-z\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieZ, Enu_TypeVariable.InertieCM4, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("WEL_Y_PROFILE") & TABAFF & "W\-el,y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWelY, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        'If lLamine Then
        AddLigneNDC(TABW2 & BlocG("WEL_Z_PROFILE") & TABAFF & "W\-el,z\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWelZ, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        'End If
        AddLigneNDC(TABW2 & BlocG("WPL_Y_PROFILE") & TABAFF & "W\-pl,y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWplY, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("WPL_Z_PROFILE") & TABAFF & "W\-pl,z\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.ModuleWplz, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("IT_PROFILE") & TABAFF & "I\-t\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieT, Enu_TypeVariable.InertieCM4, 4, 2, True))
        AddLigneNDC(TABW2 & BlocG("IW_PROFILE") & TABAFF & "I\-w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.InertieW, Enu_TypeVariable.InertieWCM6, 4, 0, True))
        SauteLigne()
        AddLigneNDC(TABW2 & BlocG("MASS_LIN_PROFILE") & TABAFF & "m\-lin\=" & TABEGAL & GetStringInUnit(MyBeam.Section.MassLineiqueProfilA, Enu_TypeVariable.SansType, 4, 1, False) & " kg/ml")
        AddLigneNDC(TABW2 & BlocG("MASS_PROFILE") & TABAFF & "m" & TABEGAL & GetStringInUnit(MyBeam.MasseTotalePoutre, Enu_TypeVariable.SansType, 4, 1, False) & " kg")
        AddLigneNDC(TABW2 & BlocG("TOT_PAINT_SURF") & TABAFF & "S" & TABEGAL & GetStringInUnit(MyBeam.SurfacePeintureTotalePoutre(True), Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("PAINT_SURF") & TABAFF & "S" & TABEGAL & GetStringInUnit(MyBeam.SurfacePeintureTotalePoutre(False), Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("TOT_MASSIVENESS") & TABAFF & "M" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Massivete(True), Enu_TypeVariable.Longueur, 4, 1, True) & " \+-1\=")
        AddLigneNDC(TABW2 & BlocG("MASSIVENESS") & TABAFF & "M'" & TABEGAL & GetStringInUnit(MyBeam.Section.ProfilA.Massivete(False), Enu_TypeVariable.Longueur, 4, 1, True) & " \+-1\=")

        AddLigneNDC("\IMG PROFIL_ACIER 10 80 30 NoCadre")
        nbLignes += 15              ' Prise en compte des lignes occupées par le dessin de la section

        If nbLignes + 15 > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(3, BlocG("MATERIAL_PROFILE"))
        If MyBeam.Section.Acier.lUser Then
            AddLigneNDC(TABW2 & BlocG("GRADE_PROFILE") & TABAFF & BlocG("USER_DEF"))
        Else
            AddLigneNDC(TABW2 & BlocG("GRADE_PROFILE") & TABAFF & MyBeam.Section.Acier.Nuance & " " & MyBeam.Section.Acier.Qualite)
            AddLigneNDC(TABW2 & BlocG("STANDARD_PROFILE") & TABAFF & MyBeam.Section.Acier.Reduction)
        End If

        If lLamine Then
            AddLigneNDC(TABW2 & BlocG("FY_PROFILE") & TABAFF & "f\-y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.FySup, Enu_TypeVariable.Contrainte, 4, 0, True))
        Else
            AddLigneNDC(TABW2 & BlocG("FYFS_PROFILE") & TABAFF & "f\-y,fs\=" & TABEGAL & GetStringInUnit(MyBeam.Section.FySup, Enu_TypeVariable.Contrainte, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("FYW_PROFILE") & TABAFF & "f\-y,w\=" & TABEGAL & GetStringInUnit(MyBeam.Section.FyW, Enu_TypeVariable.Contrainte, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("FYFI_PROFILE") & TABAFF & "f\-y,fi\=" & TABEGAL & GetStringInUnit(MyBeam.Section.FyInf, Enu_TypeVariable.Contrainte, 4, 0, True))
        End If
        AddLigneNDC(TABW2 & BlocG("E_PROFILE") & TABAFF & "E" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.EYoung, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("RHO_PROFILE") & TABAFF & "\Sr\s" & TABEGAL & GetStringInUnit(MyBeam.Section.Acier.Rho, Enu_TypeVariable.SansType, 4, 0, True) & "kg/m\+3\=")

        Dim zANP, MplRd As Decimal
        Dim zANE, InertieY, MelRd As Decimal
        MyBeam.Section.ProprietesPlastiquesMyy(1, True, MyBeam.Param.Gamma, 0, zANP, MplRd, True)
        MyBeam.Section.ProprietesElastiquesMyy(1, True, MyBeam.Param.Gamma, 1, zANE, InertieY, MelRd, True)

        AddTitreNdC(3, BlocG("RESISTANCE_PROFILE"))
        AddLigneNDC(TABW2 & BlocG("TRAC_RES") & TABAFF & "N\-t,Rd\=" & TABEGAL & GetStringInUnit(MyBeam.Section.ResistanceTractionProfile(MyBeam.Param.Gamma.GammaM0), Enu_TypeVariable.Effort, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("SHEAR_RES") & TABAFF & "V\-pl,Rd\=" & TABEGAL & GetStringInUnit(MyBeam.Section.VplRd(MyBeam.Param.Gamma.GammaM0), Enu_TypeVariable.Effort, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("PLAS_MOM_RES") & TABAFF & "M\-pl,Rd\=" & TABEGAL & GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("ELAS_MOM_RES") & TABAFF & "M\-el,Rd\=" & TABEGAL & GetStringInUnit(MelRd, Enu_TypeVariable.Moment, 4, 0, True))
        SauteLigne()
        AddLigneNDC(TABW2 & BlocG("BENDING_CLASS") & TABAFF & GetStringInUnit(MyBeam.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(False, MyBeam.Param.lGeneration1), Enu_TypeVariable.SansType, 1, 0, False))
        AddLigneNDC(TABW2 & BlocG("COMPRESSION_CLASS") & TABAFF & GetStringInUnit(MyBeam.Section.ClasseProfilAcierSeulCompressionPureFlexionPure(True, MyBeam.Param.lGeneration1), Enu_TypeVariable.SansType, 1, 0, False))
        SauteLigne()
        If Not MyBeam.Section.lSlimFloor Then
            If MyBeam.Section.lEnrobage Then
                If Not MyBeam.Section.IsInteractionMV(MyBeam.Param.EtaW) Then
                    AddLigneNDC(TABW2 & BlocG("SHEAR_BUC_RES") & TABAFF & "d\-w\=/t\-w\= = " & GetStringInUnit((MyBeam.Section.ProfilA.HauteurAmeDw / MyBeam.Section.ProfilA.Tw), Enu_TypeVariable.SansType, 3, 1, False) & " ≤ 124\Se\s = " & GetStringInUnit(124 * MyBeam.Section.Epsilon_W, Enu_TypeVariable.SansType, 3, 1, False) & " : " & BlocG("NO_NEED_CHECK_WB"))
                Else
                    AddLigneNDC(TABW2 & BlocG("SHEAR_BUC_RES") & TABAFF & "d\-w\=/t\-w\= = " & GetStringInUnit((MyBeam.Section.ProfilA.HauteurAmeDw / MyBeam.Section.ProfilA.Tw), Enu_TypeVariable.SansType, 3, 1, False) & " > 124\Se\s = " & GetStringInUnit(124 * MyBeam.Section.Epsilon_W, Enu_TypeVariable.SansType, 3, 1, False) & " : " & BlocG("NEED_CHECK_WB"))
                End If
            Else
                If Not MyBeam.Section.IsInteractionMV(MyBeam.Param.EtaW) Then
                    AddLigneNDC(TABW2 & BlocG("SHEAR_BUC_RES") & TABAFF & "h\-w\=/t\-w\= = " & GetStringInUnit((MyBeam.Section.ProfilA.HauteurAmeDw / MyBeam.Section.ProfilA.Tw), Enu_TypeVariable.SansType, 3, 1, False) & " ≤ 72\Se\s/\Sh\s = " & GetStringInUnit(72 * MyBeam.Section.Epsilon_W / MyBeam.Param.EtaW, Enu_TypeVariable.SansType, 3, 1, False) & " : " & BlocG("NO_NEED_CHECK_WB"))
                Else
                    AddLigneNDC(TABW2 & BlocG("SHEAR_BUC_RES") & TABAFF & "h\-w\=/t\-w\= = " & TABEGAL & GetStringInUnit((MyBeam.Section.ProfilA.HauteurAmeDw / MyBeam.Section.ProfilA.Tw), Enu_TypeVariable.SansType, 3, 1, False) & " > 72\Se\s/\Sh\s = " & GetStringInUnit(72 * MyBeam.Section.Epsilon_W / MyBeam.Param.EtaW, Enu_TypeVariable.SansType, 3, 1, False) & " : " & BlocG("NEED_CHECK_WB"))
                End If
            End If

            Dim lTwoAdjacentCantilevers As Boolean
            lTwoAdjacentCantilevers = MyBeam.lTraveeConsoleGauche And MyBeam.lTraveeConsoleDroite
            If MyBeam.Section.IsInteractionMV(MyBeam.Param.EtaW) Then AddLigneNDC(TABW2 & BlocG("SHEAR_BUC") & TABAFF & "V\-b,Rd\=" & TABEGAL & GetStringInUnit(MyBeam.Section.VbRd(MyBeam.Param.Gamma.GammaM1, MyBeam.Param.EtaW, lTwoAdjacentCantilevers), Enu_TypeVariable.Effort, 4, 0, True))

        End If


    End Sub

    Private Sub EditionParametresEnrobagePartiel(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   28/07/23 :  Création - Version 1.00 - GUD
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base de l'enrobage partiel d'une section
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, BlocG("PARTIAL_ENCASEMENT"))

        AddTitreNdC(3, BlocG("GEOMETRY_PART_ENC"))

        AddLigneNDC(TABW2 & BlocG("RATIO_BC_PART_ENC") & TABAFF & "b\-c\=/b\-f\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Ratio_bc, Enu_TypeVariable.SansType, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("BC_PART_ENC") & TABAFF & "b\-c\=" & TABEGAL & GetStringInUnit(MyBeam.Section.LargeurEnrobagePartielBc, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("NOTIONALSIZE") & TABAFF & "h\-0\=" & TABEGAL & GetStringInUnit(MyBeam.Section.NotionalSizeEnrobage, Enu_TypeVariable.Dimension, 4, 0, True))


        '--> Béton

        AddTitreNdC(3, BlocG("CONCRETE_MATERIAL"))

        'If MyBeam.Section.enrobage_partiel.Beton.Type = cls_Beton.Enum_TypeBeton.Leger Then
        If MyBeam.Section.Enrobage.Beton.lLeger Then
            AddLigneNDC(TABW2 & BlocG("TYPE_CONCRETE") & TABAFF & BlocG("LIGHTCONCRETE"))
        Else
            AddLigneNDC(TABW2 & BlocG("TYPE_CONCRETE") & TABAFF & BlocG("NORMALCONCRETE"))
        End If

        AddLigneNDC(TABW2 & BlocG("CLASS_CONCRETE") & TABAFF & MyBeam.Section.Enrobage.Beton.Classe)
        AddLigneNDC(TABW2 & BlocG("FCK_CONCRETE") & TABAFF & "f\-ck\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Fck, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("FCM_CONCRETE") & TABAFF & "f\-cm\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Fcm, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("FCTM_CONCRETE") & TABAFF & "f\-ctm\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Fctm, Enu_TypeVariable.Contrainte, 4, 2, True))
        AddLigneNDC(TABW2 & BlocG("ECM_CONCRETE") & TABAFF & "E\-cm\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Beton.Ecm, Enu_TypeVariable.Contrainte, 4, 0, True))

        '--> Armatures

        AddTitreNdC(3, BlocG("GEOM_LONGI_REINF"))

        AddLigneNDC(TABW2 & BlocG("GEOM_LONGI_REINF") & " :")
        SauteLigne()

        AddLigneNDC("\TABLEAU 18")

        InitialiseLigne(6, HLIGNE, True)

        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LAYER"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "z\-s\=")
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("EXTERIOR"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("MIDDLE"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("INTERIOR"))
        AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, "A\-s\=")

        For i As Integer = 0 To MyBeam.Section.Enrobage.LitArma.Count - 1
            InitialiseLigne(6, HLIGNE, True)
            Select Case i
                Case 0
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("TOP"))
                Case 1
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("MIDDLE"))
                Case 2
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("BOTTOM"))
            End Select

            With MyBeam.Section.Enrobage.LitArma(i)

                If .NbExt = 0 And .NbMil = 0 And .NbInt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.zPosRatio, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbExt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("NONE"))
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NbExt, Enu_TypeVariable.SansType, 4, 0, False) & " x " & GetStringInUnit(.PhiExt, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbMil = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("NONE"))
                Else
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NbMil, Enu_TypeVariable.SansType, 4, 0, False) & " x " & GetStringInUnit(.PhiMil, Enu_TypeVariable.Dimension, 4, 0, True))
                End If

                If .NbInt = 0 Then
                    AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("NONE"))
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

        AddLigneNDC(TABW2 & BlocG("WITH") & " :")
        AddLigneNDC(TABW2 & "z\-s\= : " & TABVAR1 & BlocG("ZS_REINF"))
        AddLigneNDC(TABW2 & "A\-s\= : " & TABVAR1 & BlocG("AS_REINF"))



        AddTitreNdC(3, BlocG("MATERIAL_LONGI_REINF"))
        AddLigneNDC(TABW2 & BlocG("CLASS_REINFORCEMENT") & TABAFF & MyBeam.Section.Enrobage.AcierArmatures.Classe)
        AddLigneNDC(TABW2 & BlocG("FYS_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Section.Enrobage.AcierArmatures.FsK, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("ES_REINFORCEMENT") & TABAFF & GetStringInUnit(MyBeam.Section.Enrobage.AcierArmatures.Es, Enu_TypeVariable.Contrainte, 4, 0, True))






        AddTitreNdC(3, BlocG("GEOM_TRANSV_REINF"))
        Select Case MyBeam.Section.Enrobage.Etriers_Type
            Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                AddLigneNDC(TABW2 & BlocG("STIRRUP_ARRANGEMENT") & TABAFF & BlocG("CLOSED_STIRRUPS"))
            Case cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                AddLigneNDC(TABW2 & BlocG("STIRRUP_ARRANGEMENT") & TABAFF & BlocG("WELDED_STIRRUPS"))
            Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant
                AddLigneNDC(TABW2 & BlocG("STIRRUP_ARRANGEMENT") & TABAFF & BlocG("THROUGH_STIRRUPS"))
        End Select
        AddLigneNDC(TABW2 & BlocG("DSI_LAYERS") & TABAFF & GetStringInUnit(MyBeam.Section.Enrobage.Etriers_Phi, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("HOR_COVERAGE") & TABAFF & "u\-y\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Etriers_EnrobageY, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("VER_COVERAGE") & TABAFF & "u\-z\=" & TABEGAL & GetStringInUnit(MyBeam.Section.Enrobage.Etriers_EnrobageZ, Enu_TypeVariable.Dimension, 4, 0, True))

        AddLigneNDC("\IMG PARTIAL_ENCASEMENT 15 70 25 NoCadre")
        nbLignes += 15
    End Sub

    Private Sub EditionParametresDalle(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de la dalle
        '----------------------------------------------------------------------------------------------

        If nbLignes + 12 > MAXLIGNEPPAG Then _
        SautePage()

        '=== DALLE ===================================================================================================

        AddTitreNdC(2, BlocG("SLAB"))

        '--> Géométrie

        AddTitreNdC(3, BlocG("GEOMETRY_SLAB"))

        Select Case MyBeam.Dalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                AddLigneNDC(TABW2 & BlocG("TYPE_SLAB") & TABAFF & BlocG("SOLID_SLAB"))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_SLAB") & TABAFF & "t\-d\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_HAUNCH") & TABAFF & "t\-h\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.t_h, Enu_TypeVariable.Dimension, 4, 0, True))
            Case cls_Dalle.Enum_TypeDalle.Prefabriquee
                AddLigneNDC(TABW2 & BlocG("TYPE_SLAB") & TABAFF & BlocG("SOLID_SLAB_PRECAST"))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_SLAB") & TABAFF & "t\-d\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_PRECAST") & TABAFF & "t\-pc\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.preDalle_ep, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_JOINT") & TABAFF & "t\-j\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.preDalle_tjoint, Enu_TypeVariable.Dimension, 4, 0, True))
            Case cls_Dalle.Enum_TypeDalle.Mixte
                AddLigneNDC(TABW2 & BlocG("TYPE_SLAB") & TABAFF & BlocG("COMPOSITE_SLAB"))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_SLAB") & TABAFF & "t\-d\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.t_d, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("THICKNESS_ABOVE_DECK") & TABAFF & "t\-c\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.EpaisseurActive, Enu_TypeVariable.Dimension, 4, 0, True))
        End Select

        AddLigneNDC(TABW2 & BlocG("NOTIONALSIZE") & TABAFF & "h\-0\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.NotionalSizeH0(MyBeam.Section.ProfilA.Bfs), Enu_TypeVariable.Dimension, 4, 0, True))

        '--> Béton de la dalle

        AddTitreNdC(3, BlocG("CONCRETE_MATERIAL"))

        'If MyBeam.Dalle.beton.Type = cls_Beton.Enum_TypeBeton.Leger Then
        If MyBeam.Dalle.beton.lLeger Then
            AddLigneNDC(TABW2 & BlocG("TYPE") & TABAFF & BlocG("LIGHTCONCRETE"))
        Else
            AddLigneNDC(TABW2 & BlocG("TYPE_CONCRETE") & TABAFF & BlocG("NORMALCONCRETE"))
        End If

        AddLigneNDC(TABW2 & BlocG("CLASS_CONCRETE") & TABAFF & MyBeam.Dalle.beton.Classe)
        AddLigneNDC(TABW2 & BlocG("FCK_CONCRETE") & TABAFF & "f\-ck\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.beton.Fck, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("FCM_CONCRETE") & TABAFF & "f\-cm\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.beton.Fcm, Enu_TypeVariable.Contrainte, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("FCTM_CONCRETE") & TABAFF & "f\-ctm\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.beton.Fctm, Enu_TypeVariable.Contrainte, 4, 2, True))
        AddLigneNDC(TABW2 & BlocG("ECM_CONCRETE") & TABAFF & "E\-cm\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.beton.Ecm, Enu_TypeVariable.Contrainte, 4, 0, True))

        '=== Armatures longitudinales ======================================================================

        EditionParametresArmaLongiDalle(MyBeam)

        '=== Bac acier ==========================================================================

        If MyBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then

            EditionParametresBac(MyBeam)

        End If

        '=== CONNECTEURS & CONNEXION ===========================================================================================

        EditionParametresConnecteursEtConnexion(MyBeam)

    End Sub

    Private Sub EditionParametresArmaLongiDalle(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - Version 1.00 - GUD
        '----------------------------------------------------------------------------------------------
        '   Edition des armatures longi d'une dalle
        '----------------------------------------------------------------------------------------------

        AddTitreNdC(3, BlocG("LONGI_REINFORCEMENTS"))

        '--> Géométrie

        AddLigneNDC(TABW2 & BlocG("NB_LAYERS") & TABAFF & GetStringInUnit(MyBeam.Dalle.NbLitsArmaActifs, Enu_TypeVariable.SansType, 4, 0, True))
        SauteLigne()

        AddLigneNDC("\TABLEAU 18")
        InitialiseLigne(5, HLIGNEENTETE, True)
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
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.Dalle.LitArma(i).AireParULargeur, Enu_TypeVariable.AireMM2, 4, 0, False))
            End If
        Next

        FinTableau()

        AddLigneNDC(TABW2 & BlocG("WITH"))
        AddLigneNDC(TABW2 & "e\-si\= : " & TABVAR1 & BlocG("ESI_LAYERS"))
        AddLigneNDC(TABW2 & "d\-si\= : " & TABVAR1 & BlocG("DSI_LAYERS"))
        AddLigneNDC(TABW2 & "z\-si\= : " & TABVAR1 & BlocG("ZSI_LAYERS"))
        AddLigneNDC(TABW2 & "A\-si\= : " & TABVAR1 & BlocG("ASI_LAYERS"))


        '--> Acier

        AddTitreNdC(3, BlocG("MATERIAL_LONGI_REINF"))
        AddLigneNDC(TABW2 & BlocG("CLASS_REINFORCEMENT") & TABAFF & MyBeam.Dalle.AcierArmatures.Classe)
        AddLigneNDC(TABW2 & BlocG("FYS_REINFORCEMENT") & TABAFF & "f\-sk\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.AcierArmatures.FsK, Enu_TypeVariable.Contrainte, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("ES_REINFORCEMENT") & TABAFF & "E\-s\=" & TABEGAL & GetStringInUnit(MyBeam.Dalle.AcierArmatures.Es, Enu_TypeVariable.Contrainte, 4, 0, True))

        'SauteLigne()
        If nbLignes + 30 > MAXLIGNEPPAG Then SautePage()
        AddLigneNDC("\IMG SLAB 5 80 30 NoCadre")
        nbLignes += 30

    End Sub

    Private Sub EditionParametresBac(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - Version 1.00 - GUD
        '----------------------------------------------------------------------------------------------
        '   Edition d'un bac d'une dalle mixte
        '----------------------------------------------------------------------------------------------

        If nbLignes + 15 > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(3, BlocG("PROFILED_STEEL_SH"))

        If MyBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele Then
            AddLigneNDC(TABW2 & BlocG("ORIENTATION_SHEET") & TABAFF & BlocG("LONGITUDINAL"))
        Else
            AddLigneNDC(TABW2 & BlocG("ORIENTATION_SHEET") & TABAFF & BlocG("TRANSVERSAL"))
        End If

        If MyBeam.Dalle.Bac.lDatabase Then
            AddLigneNDC(TABW2 & BlocG("PSS_FROM") & TABAFF & BlocG("DATABASE"))
            'AddLigneNDC(TABW2 & BlocG("SOCIETE") & TABAFF & MyBeam.Dalle.Bac.Producteur)
            AddLigneNDC(TABW2 & BlocG("NAME_PSS") & TABAFF & MyBeam.Dalle.Bac.Etiquette)
            AddLigneNDC(TABW2 & BlocG("NAME_PSS") & TABAFF & MyBeam.Dalle.Bac.Producteur & " / " & MyBeam.Dalle.Bac.Etiquette)
        Else
            AddLigneNDC(TABW2 & BlocG("PSS_FROM") & TABAFF & BlocG("DIMENSIONS_PSS"))
        End If

        SauteLigne()

        AddLigneNDC(TABW2 & BlocG("CHAR_PSS"))

        With MyBeam.Dalle.Bac

            AddLigneNDC(TABW2 & BlocG("TP_PSS") & TABAFF & "t\-p\=" & TABEGAL & GetStringInUnit(.Tp, Enu_TypeVariable.Dimension, 4, 2, True))
            AddLigneNDC(TABW2 & BlocG("EP_PSS") & TABAFF & "e\-p\=" & TABEGAL & GetStringInUnit(.Ep, Enu_TypeVariable.Dimension, 4, 0, True))

            If .HasRaidisseurSup Then
                'AddLigneNDC(TABW2 & BlocG("HP_PSS") & TABAFF & "h\-p\= =" & TABEGAL & GetStringInUnit(.Hp, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("HP_RIB") & TABAFF & "h\-p\=" & TABEGAL & GetStringInUnit(.Hp, Enu_TypeVariable.Dimension, 4, 0, True))
                AddLigneNDC(TABW2 & BlocG("HPG_PSS") & TABAFF & "h\-pg\=" & TABEGAL & GetStringInUnit(.Hauteur_hpg, Enu_TypeVariable.Dimension, 4, 0, True))
            Else
                AddLigneNDC(TABW2 & BlocG("HP_RIB") & TABAFF & "h\-p\=" & TABEGAL & GetStringInUnit(.Hp, Enu_TypeVariable.Dimension, 4, 0, True))
            End If

            AddLigneNDC(TABW2 & BlocG("BB_PSS") & TABAFF & "b\-b\=" & TABEGAL & GetStringInUnit(.Bb, Enu_TypeVariable.Dimension, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("BT_PSS") & TABAFF & "b\-t\=" & TABEGAL & GetStringInUnit(.Bt, Enu_TypeVariable.Dimension, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("MUP_PSS") & TABAFF & "\Sm\s\-p\=" & TABEGAL & GetStringInUnit(.msurf, Enu_TypeVariable.SansType, 4, 2, True) & " kg/m\+2\=")
            AddLigneNDC(TABW2 & BlocG("FP_PSS") & TABAFF & "f\-p\=" & TABEGAL & GetStringInUnit(.fyp, Enu_TypeVariable.Contrainte, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("IPU_PSS") & TABAFF & "I\-pu\=" & TABEGAL & GetStringInUnit(.Ieff, Enu_TypeVariable.InertieCM4, 4, 2, True) & "/m")

            'If .Orientation = cls_Bac.Enum_Orientation.Parallele Then
            '    If .AppuiL = cls_Bac.EnuConfigLAppui.BacCoupe Then
            '        AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("CUT_DECK"))
            '    Else
            '        AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("UNCUT_DECK"))
            '    End If
            'Else
            If .Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
                Select Case .AppuiT
                    Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus

                        'AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("CONTINU_PSS"))
                        AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("CONTINUOUS_RIB"))
                        AddLigneNDC(TABAFF & BlocG("CONTINUOUS_DECK"))
                        If .lPreperce Then
                            AddLigneNDC(TABW2 & BlocG("CONNECTION_OPT") & TABAFF & BlocG("PREPUNCHED_PSS"))
                        Else
                            AddLigneNDC(TABW2 & BlocG("CONNECTION_OPT") & TABAFF & BlocG("THROUGH_DECK_PSS"))
                        End If
                    Case cls_Bac.EnuConfigTAppui.BetonSeulContinu
                        'AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("PART_CONT_PSS"))
                        AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("CONTINUOUS_RIB"))
                        AddLigneNDC(TABAFF & BlocG("NONCONTINUOUS_DECK"))
                        If .lPreperce Then
                            AddLigneNDC(TABW2 & BlocG("CONNECTION_OPT") & TABAFF & BlocG("PREPUNCHED_PSS"))
                        Else
                            AddLigneNDC(TABW2 & BlocG("CONNECTION_OPT") & TABAFF & BlocG("THROUGH_DECK_PSS"))
                        End If
                    Case cls_Bac.EnuConfigTAppui.Discontinu
                        AddLigneNDC(TABW2 & BlocG("CONFIG_SUPPORT_PSS") & TABAFF & BlocG("NO_CONT_PSS"))
                End Select
            End If
        End With

    End Sub

    Private Sub EditionParametresConnecteursEtConnexion(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition de la connexion et des connecteurs d'une poutre mixte
        '----------------------------------------------------------------------------------------------

        Dim lConnection As Boolean = False

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                lConnection = True
            Case cls_Section.Enum_TypeSection.SFBmixte, cls_Section.Enum_TypeSection.IFB_Amixte, cls_Section.Enum_TypeSection.IFB_Bmixte, cls_Section.Enum_TypeSection.SABmixte
                lConnection = True
        End Select

        If lConnection Then

            '=== CONNECTEURS =================================================================================================================

            EditionParametresConnecteurs(MyBeam)

            '=== CONNEXION =================================================================================================================

            EditionParametresConnexion(MyBeam)
        End If
    End Sub

    Private Sub EditionParametresConnecteurs(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition définition connecteurs
        '----------------------------------------------------------------------------------------------

        If nbLignes + 12 > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(3, BlocG("CONNECTORS"))

        With MyBeam.Dalle.Connecteur
            'AddLigneNDC(TABW2 & BlocG("NAME_CONNECTORS") & TABAFF & .nom)
            AddLigneNDC(TABW2 & BlocG("HSC_CONNECTORS") & TABAFF & "h\-sc\=" & TABEGAL & GetStringInUnit(.hsc, Enu_TypeVariable.Dimension, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("D_CONNECTORS") & TABAFF & "d " & TABEGAL & GetStringInUnit(.d, Enu_TypeVariable.Dimension, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("FYSC_CONNECTORS") & TABAFF & "f\-ysc\=" & TABEGAL & GetStringInUnit(.Fy, Enu_TypeVariable.Contrainte, 4, 0, True))
            AddLigneNDC(TABW2 & BlocG("FUSC_CONNECTORS") & TABAFF & "f\-usc\=" & TABEGAL & GetStringInUnit(.Fu, Enu_TypeVariable.Contrainte, 4, 0, True))

            Dim lGeneration1, lDallePleine, lPerp As Boolean 'Déclaration des variables locales qui serviront dans la fonction ResistancePRd
            Dim nr_min, nr_max As Integer
            Dim Ecm, Fck As Decimal
            Dim gammaVs, gammaVc As Decimal

            lGeneration1 = MyBeam.Param.lGeneration1
            lDallePleine = (MyBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine) Or (MyBeam.Dalle.type = cls_Dalle.Enum_TypeDalle.Prefabriquee)
            lPerp = (MyBeam.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)

            nr_min = MyBeam.nr_min
            nr_max = MyBeam.nr_max

            Ecm = MyBeam.Dalle.beton.Ecm
            Fck = MyBeam.Dalle.beton.Fck

            gammaVs = MyBeam.Param.Gamma.GammaVs
            gammaVc = MyBeam.Param.Gamma.GammaVc

            If lDallePleine Then
                AddLigneNDC(TABW2 & BlocG("PRD_CONNECTORS") & TABAFF & "P\-Rd\= =" & TABEGAL & GetStringInUnit(.ResistancePRd(lGeneration1, lDallePleine, lPerp, MyBeam.Dalle.Bac, nr_min, Fck, Ecm, gammaVs, gammaVc), Enu_TypeVariable.Effort, 4, 1, True))
            Else 'dalle mixte
                If lPerp Then
                    For nr_boucle As Integer = nr_min To nr_max
                        AddLigneNDC(TABW2 & BlocG("PRD_CONNECTORS") & TABAFF & "P\-Rd\=" & TABEGAL & GetStringInUnit(.ResistancePRd(lGeneration1, lDallePleine, lPerp, MyBeam.Dalle.Bac, nr_boucle, Fck, Ecm, gammaVs, gammaVc), Enu_TypeVariable.Effort, 4, 1, True) & " (n\-r\= = " & nr_boucle & ")")
                        ' AddLigneNDC(TABW2 & BlocG("KL_CONNECTORS") & TABAFF & "k\-t\= =" & TABEGAL & GetStringInUnit(.CoefkT(nr_boucle, MyBeam.Dalle.Bac), Enu_TypeVariable.SansType, 4, 0, True) & " (n\-r\= = " & nr_boucle & ")")
                        AddLigneNDC(TABW2 & BlocG("REDUCTIONFACTOR") & TABAFF & "k\-t\=" & TABEGAL & GetStringInUnit(.CoefkT(nr_boucle, MyBeam.Dalle.Bac), Enu_TypeVariable.SansType, 4, 2, True) & " (n\-r\= = " & nr_boucle & ")")
                    Next
                Else 'dalle parallèlle
                    AddLigneNDC(TABW2 & BlocG("PRD_CONNECTORS") & TABAFF & "P\-Rd\=" & TABEGAL & GetStringInUnit(.ResistancePRd(lGeneration1, lDallePleine, lPerp, MyBeam.Dalle.Bac, nr_min, Fck, Ecm, gammaVs, gammaVc), Enu_TypeVariable.Effort, 4, 1, True))
                    ' AddLigneNDC(TABW2 & BlocG("KL_CONNECTORS") & TABAFF & "k\-l\= =" & TABEGAL & GetStringInUnit(.CoefkL(MyBeam.Dalle.Bac), Enu_TypeVariable.SansType, 4, 0, True))
                    AddLigneNDC(TABW2 & BlocG("REDUCTIONFACTOR") & TABAFF & "k\-l\=" & TABEGAL & GetStringInUnit(.CoefkL(MyBeam.Dalle.Bac), Enu_TypeVariable.SansType, 4, 2, True))
                End If
            End If

        End With

    End Sub

    Private Sub EditionParametresConnexion(MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition définition connexion
        '----------------------------------------------------------------------------------------------

        If nbLignes + 12 > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(3, BlocG("CONNECTION_ARR"))

        With MyBeam

            If .lAutomaticDesign Then
                AddLigneNDC(TABW2 & BlocG("AUTOMATIC_DESIGN") & TABAFF & BlocG("YES"))
            Else
                'AddLigneNDC(TABW2 & BlocG("AUTOMATIC_DESIGN") & TABAFF & BlocG("NO"))

                'SauteLigne()

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

                InitialiseLigne(nbColonne, HLIGNEENTETE, True)

                AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocG("SPAN"))
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LENGHT_ZONE") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("ROW_NUMBER"))
                If lDalleMixteEtPerp Then
                    AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("RIB_DISPOSITION"))
                End If
                AddCelluleFond(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LONGI_SPACING") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")")

                '                    Dim lDerniereTravee As Boolean 'Permet de gérer la séparation par une ligne grise entre deux travées comportant des maintiens latéraux consévutives

                Dim iTraveeAffichee As Integer = 1

                For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
                    For j As Integer = 0 To .NombreZones(i) - 1

                        InitialiseLigne(nbColonne, HLIGNE, True)
                        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, iTraveeAffichee)
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.LongueurZone(i, j), Enu_TypeVariable.Longueur, 4, 0, False))
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.NombreGoujonsTransv(i, j), Enu_TypeVariable.SansType, 4, 0, False))
                        If lDalleMixteEtPerp Then
                            If .Espacement_Bac_TransZone(i, j) = 1 Then
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Espacement_Bac_TransZone(i, j), Enu_TypeVariable.SansType, 4, 0, False) & " " & BlocG("RIB"))
                            Else
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.Espacement_Bac_TransZone(i, j), Enu_TypeVariable.SansType, 4, 0, False) & " " & BlocG("RIBS"))
                                End
                            End If
                        End If
                        AddCellule(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(.EspacementZone(i, j), Enu_TypeVariable.Dimension, 4, 0, False))

                        iTraveeAffichee += 1

                    Next

                    If Not i = MyBeam.IndiceDerniereTravee Then
                        InitialiseLigne(1, HLIGNE, True)
                        AddCelluleFond(LC3 + (nbColonne - 2) * LC2 + LC1_2, Bordures.Tous, PositionTexteInCell.Centre, "")
                    End If

                Next

                FinTableau()
            End If

        End With

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
        AddTitreNdC(2, BlocG("DATAPROPPING"))

        '--> Affichage de l'étaiement

        Select Case MyBeam.TypeEtaiement
            Case cls_Poutre.EnuTypeEtaiement.FullyPropped
                AddLigneNDC(TABW2 & BlocG("TYPE_PROPPING") & TABAFF & BlocG("FULLYPROPPED"))
            Case cls_Poutre.EnuTypeEtaiement.UnPropped
                AddLigneNDC(TABW2 & BlocG("TYPE_PROPPING") & TABAFF & BlocG("UNPROPPED"))
            Case cls_Poutre.EnuTypeEtaiement.PointPropped
                AddLigneNDC(TABW2 & BlocG("TYPE_PROPPING") & TABAFF & BlocG("POINTPROPPED"))

                If MyBeam.lTraveeConsoleGauche Then
                    If MyBeam.lEtaisConsoleGauche Then
                        AddLigneNDC(TABW2 & BlocG("ENDPROPPEDLEFTCANT") & TABAFF & BlocG("YES"))
                    Else
                        AddLigneNDC(TABW2 & BlocG("ENDPROPPEDLEFTCANT") & TABAFF & BlocG("NO"))
                    End If
                End If

                If MyBeam.lTraveeConsoleDroite Then
                    If MyBeam.lEtaisConsoleDroite Then
                        AddLigneNDC(TABW2 & BlocG("ENDPROPPEDRIGHTCANT") & TABAFF & BlocG("YES"))
                    Else
                        AddLigneNDC(TABW2 & BlocG("ENDPROPPEDRIGHTCANT") & TABAFF & BlocG("NO"))
                    End If
                End If

                If MyBeam.lTraveeConsoleGauche Or MyBeam.lTraveeConsoleDroite Then
                    AddLigneNDC(TABW2 & BlocG("NBPROPPINGWITHCANT") & TABAFF & MyBeam.NbEtaiement)
                Else
                    AddLigneNDC(TABW2 & BlocG("NBPROPPINGWITHOUTCANT") & TABAFF & MyBeam.NbEtaiement)
                End If

                If MyBeam.lEtaisSousProfileAcier Then
                    AddLigneNDC(TABW2 & BlocG("PROPPINGLOCATION") & TABAFF & BlocG("UNDERSTEEL"))
                Else
                    AddLigneNDC(TABW2 & BlocG("PROPPINGLOCATION") & TABAFF & BlocG("UNDERSLAB"))
                End If


        End Select

    End Sub

    Private Sub EditionParametresMaintienBac(myBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   01/02/24 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des maintiens par le bac d'une poutre mixte en phase de construction
        '----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim ns As Integer
        Dim EntraxeD As Decimal = myBeam.EntraxeSolive
        Dim Chaine As String = ""
        Dim ChaineD As String = ""
        Dim kUnitSlip As Decimal = 1000 ^ 2
        Const kUnitFlex As Decimal = 10 ^ 6
        Const UNITFlex As String = " mm/kN"

        '--> Initialisation

        ns = myBeam.MaintienBac.m * myBeam.MaintienBac.nt + 1

        '--> Traitement

        If myBeam.lMaintienBacPossible And myBeam.MaintienBac.lMaintienBac Then

            AddTitreNdC(2, BlocG("SHEETRESTRAINT"))

            '--( Général

            AddLigneNDC(TABW2 & BlocG("SHEETNBTDIR") & TABAFF & "nt" & TABEGAL & CStr(myBeam.MaintienBac.nt))
            AddLigneNDC(TABW2 & BlocG("SHEETSPANNUMBER") & TABAFF & "m" & TABEGAL & CStr(myBeam.MaintienBac.m))
            AddLigneNDC(TABW2 & BlocG("SECONDBEAMSNB") & TABAFF & "ns" & TABEGAL & CStr(ns))
            AddLigneNDC(TABW2 & BlocG("SECONDBEAMSPACING") & TABAFF & "d" & TABEGAL & GetStringInUnit(EntraxeD, Enu_TypeVariable.Longueur, 3, 2, True))
            AddLigneNDC(TABW2 & BlocG("FLOORWIDTH") & TABAFF & "" & TABEGAL & GetStringInUnit((ns - 1) * EntraxeD, Enu_TypeVariable.Longueur, 3, 2, True))
            AddLigneNDC(TABW2 & BlocG("SHEETDIMENSIONS") & TABAFF & "ap" & TABEGAL & GetStringInUnit((myBeam.MaintienBac.m) * EntraxeD, Enu_TypeVariable.Longueur, 3, 2, True))
            AddLigneNDC(TABAFF & "bp" & TABEGAL & GetStringInUnit(myBeam.Dalle.Bac.LargeurModule, Enu_TypeVariable.Longueur, 3, 2, True))

            '--( Panneaux adjacent au support des poutres

            If myBeam.MaintienBac.nt > 1 Then
                Select Case myBeam.MaintienBac.Transition
                    Case cls_MaintienBac.Enu_Transition.Aboutage : Chaine = BlocG("ADJACENTSHEETNOGAP")
                    Case cls_MaintienBac.Enu_Transition.Adistance : Chaine = BlocG("ADJACENTSHEETGAP")
                    Case cls_MaintienBac.Enu_Transition.Emboitement : Chaine = BlocG("ADJACENTSHEETOVERLAP")
                End Select
                SauteLigne()
                AddLigneNDC(TABW2 & Chaine)
            End If

            '--( Fixations bac sur poutre

            SauteLigne()
            Select Case myBeam.MaintienBac.FixNervuresMod
                Case cls_MaintienBac.Enu_FixationNervures.Toutes : Chaine = BlocG("ALLRIBSAREFASTENED")
                Case cls_MaintienBac.Enu_FixationNervures.UneSurDeux : Chaine = BlocG("EVERY2RIBSAREFASTENED")
            End Select
            AddLigneNDC(TABW2 & Chaine)
            Select Case myBeam.MaintienBac.FixnervuresTyp
                Case cls_MaintienBac.Enu_FixNervuresType.Pistolet : Chaine = BlocG("FIREDPINS") : ChaineD = BlocG("FIXDIASPINS")
                Case cls_MaintienBac.Enu_FixNervuresType.VisNeoprene : Chaine = BlocG("SELFDSCREWSNORMALW") : ChaineD = BlocG("FIXDIASCREWS")
                Case cls_MaintienBac.Enu_FixNervuresType.VisNormale : Chaine = BlocG("SELFDSCREWSNEOPRENW") : ChaineD = BlocG("FIXDIASCREWS")
            End Select
            AddLigneNDC(TABW2 & BlocG("FASTENERTYPE") & TABAFF & Chaine)
            AddLigneNDC(TABW2 & BlocG("FIXDIAMETER") & TABAFF & ChaineD)
            AddLigneNDC(TABW2 & BlocG("FIXSLIP") & TABAFF & GetStringInUnit(myBeam.MaintienBac.FixNervuresSlip * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & UNITFlex)

            '--( Fixations de couture

            SauteLigne()
            Select Case myBeam.MaintienBac.FixCoutureType
                Case cls_MaintienBac.Enu_CoutureType.Rivet : Chaine = BlocG("SEAMRIVET") : ChaineD = BlocG("SEAMDIARIVET")
                Case cls_MaintienBac.Enu_CoutureType.Vis : Chaine = BlocG("SEAMSCREW") : ChaineD = BlocG("SEAMDIASCREW")
            End Select
            AddLigneNDC(TABW2 & BlocG("FASTENERTYPE") & TABAFF & Chaine)
            AddLigneNDC(TABW2 & BlocG("FIXDIAMETER") & TABAFF & ChaineD)
            AddLigneNDC(TABW2 & BlocG("FIXSLIP") & TABAFF & GetStringInUnit(myBeam.MaintienBac.FixCoutureSlip * kUnitSlip, Enu_TypeVariable.SansType, 3, 2, False) & UNITFlex)

            '--( Flexibilité et raideur en cisaillement

            Dim PorteeL As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1)
            Dim eYoung As Decimal = cls_Acier.EYACIER
            Dim Poisson As Decimal = cls_Acier.NU
            Dim c11 As Decimal = myBeam.MaintienBac.Flexibilite_C11_DistorsionBac(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung)
            Dim c12 As Decimal = myBeam.MaintienBac.Flexibilite_C12_Shear(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung, Poisson)
            Dim c21 As Decimal = myBeam.MaintienBac.Flexibilite_C21_BeamFasteners(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Ep)
            Dim c22 As Decimal = myBeam.MaintienBac.Flexibilite_C21_BeamFasteners(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Ep)

            Dim Sact, cCumul As Decimal

            SauteLigne()
            AddLigneNDC(TABW2 & BlocG("SHEARFLEXIBILITIES"))
            AddLigneNDC(TABAFF & "c11" & TABEGAL & GetStringInUnit(c11 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False) & UNITFlex)
            AddLigneNDC(TABAFF & "c12" & TABEGAL & GetStringInUnit(c12 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False) & UNITFlex)
            AddLigneNDC(TABAFF & "c21" & TABEGAL & GetStringInUnit(c21 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False) & UNITFlex)
            AddLigneNDC(TABAFF & "c22" & TABEGAL & GetStringInUnit(c22 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, False) & UNITFlex)

            cCumul = c11 + c12 + c21 + c22
            Sact = PorteeL / cCumul

            AddLigneNDC(TABAFF & "Sact" & TABEGAL & GetStringInUnit(Sact, Enu_TypeVariable.Rigidite, 4, 3, True))

            SauteLigne()

            If myBeam.MaintienBac.lTheta Then
                AddLigneNDC(TABW2 & BlocG("BENDINGSTIFFNESS"))

                Dim kTheta, kThetaA, kThetaC As Decimal
                Dim bFs As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Bfs

                kThetaA = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.RigiditeFlexionnelleA(myBeam.MaintienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes, bFs)
                kThetaC = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.RigiditeFlexionnelleC(EntraxeD, myBeam.lIntermediaire)
                kTheta = 1 / (1 / kThetaA + 1 / kThetaC)

                AddLigneNDC(TABAFF & "k\-\Sq\sA\=" & TABEGAL & GetStringInUnit(kThetaA, Enu_TypeVariable.Effort, 4, 3, True) & "m/m")
                AddLigneNDC(TABAFF & "k\-\Sq\sC\=" & TABEGAL & GetStringInUnit(kThetaC, Enu_TypeVariable.Effort, 4, 3, True) & "m/m")
                AddLigneNDC(TABAFF & "k\-\Sq\s\=" & TABEGAL & GetStringInUnit(kTheta, Enu_TypeVariable.Effort, 4, 3, True) & "m/m")

            Else
                AddLigneNDC(TABW2 & BlocG("NOBENDINGSTIFFNESS") & TABAFF & Chaine)
            End If
        End If

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

        AddTitreNdC(2, BlocG("LATERALR"))

        Select Case MyBeam.TypeMaintien
            Case cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained
                AddLigneNDC(TABW2 & BlocG("TYPE_RES") & TABAFF & BlocG("FULLY_RES"))
            Case cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
                AddLigneNDC(TABW2 & BlocG("TYPE_RES") & TABAFF & BlocG("NO_INTER_RES"))
            Case cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
                AddLigneNDC(TABW2 & BlocG("TYPE_RES") & TABAFF & BlocG("POINT_RES"))

                SauteLigne()
                AddLigneNDC("\TABLEAU 29")
                InitialiseLigne(3, HLIGNE, True)
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("SPAN"))
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
                AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LATERALR"))

                Dim lDerniereTravee As Boolean 'Permet de gérer la séparation par une ligne grise entre deux travées comportant des maintiens latéraux consévutives

                iTraveeDeb = MyBeam.IndicePremiereTravee
                iTraveeFin = MyBeam.IndiceDerniereTravee

                Dim iTraveeAffichee As Integer = 1

                For i As Integer = iTraveeDeb To iTraveeFin
                    For Each maintien In MyBeam.Maintiens(i)
                        InitialiseLigne(3, HLIGNE, True)
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, iTraveeAffichee)
                        AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(maintien.x_Loc, Enu_TypeVariable.Longueur, 4, 0, False))
                        If maintien.lMaintienSemelleInf And maintien.lMaintienSemelleSup Then
                            AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("BOTH_FLANGES"))
                        Else
                            If maintien.lMaintienSemelleInf Then
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("LOWER_FLANGE"))
                            Else
                                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("UPPER_FLANGE"))
                            End If
                        End If

                        iTraveeAffichee += 1

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

                AddLigneNDC(TABW2 & BlocG("WITH"))
                AddLigneNDC(TABW2 & "x : " & BlocG("X_LOC_RES"))

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

        AddTitreNdC(2, BlocG("GAMMA"))

        '# Charges

        AddTitreNdC(3, BlocG("LOADING_FACTORS"))
        AddLigneNDC(TABVAR2 & "\Sg\s\-G,sup\= " & TABEGAL1 & GetStringInUnit(MyGamma.GammaG_sup, Enu_TypeVariable.SansType, 3, 2, False))
        AddLigneNDC(TABVAR2 & "\Sg\s\-G,inf\= " & TABEGAL1 & GetStringInUnit(MyGamma.GammaG_inf, Enu_TypeVariable.SansType, 3, 2, False))
        AddLigneNDC(TABVAR2 & "\Sg\s\-Q\= " & TABEGAL1 & GetStringInUnit(MyGamma.GammaQ, Enu_TypeVariable.SansType, 3, 2, False))

        '# Coefficients de combinaison

        AddTitreNdC(3, BlocG("COMBINATION_FACTORS"))
        AddLigneNDC(TABVAR2 & "\Sy\s\-0,Q1\= " & TABEGAL1 & MyGamma.Psi0_Q1 & TABVARL3 & "\Sy\s\-0,Q2\= " & TABEGAL2 & MyGamma.Psi0_Q2)
        AddLigneNDC(TABVAR2 & "\Sy\s\-1,Q1\= " & TABEGAL1 & MyGamma.Psi1_Q1 & TABVARL3 & "\Sy\s\-1,Q2\= " & TABEGAL2 & MyGamma.Psi1_Q2)
        AddLigneNDC(TABVAR2 & "\Sy\s\-2,Q1\= " & TABEGAL1 & MyGamma.Psi2_Q1 & TABVARL3 & "\Sy\s\-2,Q2\= " & TABEGAL2 & MyGamma.Psi2_Q2)

        '# Résistances

        AddTitreNdC(3, BlocG("RESISTANCEFACTORS"))

        If lFire Then
            ChaineFire = TABVARL4 & BlocG("FIRE_RES_FACTORS")
        End If
        AddLigneNDC(TABVAR2 & BlocG("STEEL_RES_FACTORS") & TABVARL3 & BlocG("SLAB_RES_FACTORS") & ChaineFire)

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


    Private Sub EditionParametresChargement(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------

        Dim NbBesoinLignes As Integer = 15     ' A ajuster
        Dim lMultiSpan As Boolean = (MyBeam.NbTravees > 1)
        Dim iDebTrav, iFinTrav As Integer

        '--> Initialisation

        iDebTrav = MyBeam.IndicePremiereTravee
        iFinTrav = MyBeam.IndiceDerniereTravee

        If NbBesoinLignes + nbLignes > MAXLIGNEPPAG Then
            SautePage()
        End If

        AddTitreNdC(2, BlocG("LOADS"))

        AddLigneNDC(TABW2 & BlocG("LISTULOADS"))

        For Each elmnt As KeyValuePair(Of String, cls_ChargementUtilisateur) In MyBeam.ChargesU
            If elmnt.Value.EstDefinie Then
                AddTitreNdC(3, elmnt.Key & " : " & elmnt.Value.Titre)

                '=== Charges surfaciques =================================================================================================

                If Not IsEqual(elmnt.Value.NombreChargesSurf(MyBeam.IndicePremiereTravee, MyBeam.IndiceDerniereTravee), 0) Then

                    If lMultiSpan Then
                        EditionParametresTableauQsurfTravee(MyBeam, elmnt.Value)
                    Else
                        AddLigneNDC(TABW2 & "\G\I" & BlocG("QSURF") & "\i\g" & TABAFF & "Q =" & GetStringInUnit(elmnt.Value.QSurf(1), Enu_TypeVariable.ChargeSurfacique, 3, 2, True))
                    End If

                End If

                '=== Forces linéiques ==================================================================================================

                If elmnt.Value.NombreForceReparties(MyBeam.IndicePremiereTravee, MyBeam.IndiceDerniereTravee) > 0 Then
                    SauteLigne()
                    If nbLignes + 1.5 * (elmnt.Value.NombreForceReparties(MyBeam.IndicePremiereTravee, MyBeam.IndiceDerniereTravee) + 1) + 1 > MAXLIGNEPPAG Then SautePage()
                    AddLigneNDC(TABW2 & "\G\I" & BlocG("FREP") & "\i\g")

                    EditionParametresTableauForcesRep(iDebTrav, iFinTrav, elmnt.Value)

                End If


                '=== Forces ponctuelles ===================================================================================================

                If elmnt.Value.NombreForcePonctuelles(MyBeam.IndicePremiereTravee, MyBeam.IndiceDerniereTravee) > 0 Then

                    SauteLigne()
                    If nbLignes + 1.5 * (elmnt.Value.NombreForcePonctuelles(MyBeam.IndicePremiereTravee, MyBeam.IndiceDerniereTravee) + 1) + 1 > MAXLIGNEPPAG Then SautePage()
                    AddLigneNDC(TABW2 & "\G\I" & BlocG("FPONC") & "\i\g")

                    EditionParametresTableauForcesConcentrees(iDebTrav, iFinTrav, elmnt.Value)

                End If

            End If
        Next

    End Sub

    Private Sub EditionParametresTableauForcesConcentrees(iDebTrav As Integer, iFinTrav As Integer, MyChargeU As cls_ChargementUtilisateur)
        '----------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - Version 1.00 - GUD+POM
        '----------------------------------------------------------------------------------------------
        '   Edition du tableau des charges concentrées
        '----------------------------------------------------------------------------------------------

        '--> Déclaration 

        Dim lMultiS As Boolean = (iFinTrav > iDebTrav)
        Dim NCOL As Integer

        '--> Initialisation

        If lMultiS Then NCOL = 3 Else NCOL = 2

        AddLigneNDC("\TABLEAU 18")
        InitialiseLigne(NCOL, HLIGNEENTETE, True)
        If lMultiS Then _
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, BlocG("SPAN"))
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & ")")

        For iTravee As Integer = iDebTrav To iFinTrav
            'If MyChargeU.NombreForcePonctuelles(iTravee, iTravee) = 0 Then 'aucune charge définie sur la travée en cours 
            '    InitialiseLigne(3, HLIGNE, True)
            '    AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, iTravee)
            '    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            '    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            'Else 'au moins une charge linéique est définie pour la travée en cours
            For Each f_ponc As cls_Force In MyChargeU.Forces(iTravee)
                InitialiseLigne(NCOL, HLIGNE, True)
                If lMultiS Then _
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee + 1))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(f_ponc.xPosT, Enu_TypeVariable.Longueur, 3, 2, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(f_ponc.Force, Enu_TypeVariable.Effort, 3, 2, False))
            Next

            'End If
        Next

        FinTableau()
    End Sub

    Private Sub EditionParametresTableauForcesRep(iDebTrav As Integer, iFinTrav As Integer, MyChargeU As cls_ChargementUtilisateur)
        '----------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - Version 1.00 - GUD+POM
        '----------------------------------------------------------------------------------------------
        '   Edition du tableau des charges réparties
        '----------------------------------------------------------------------------------------------

        '--> Déclaration 

        Dim lMultiS As Boolean = (iFinTrav > iDebTrav)
        Dim NCOL As Integer

        '--> Initialisation

        If lMultiS Then NCOL = 5 Else NCOL = 4

        AddLigneNDC("\TABLEAU 18")
        InitialiseLigne(NCOL, HLIGNEENTETE, True)
        If lMultiS Then _
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, BlocG("SPAN"))
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")")

        For iTravee As Integer = iDebTrav To iFinTrav
            'If MyChargeU.NombreForceReparties(iTravee, iTravee) = 0 Then 'aucune charge définie sur la travée en cours 
            '    InitialiseLigne(5, HLIGNE, True)
            '    AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, iTravee)
            '    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            '    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            '    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            '    AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            'Else 'au moins une charge linéique est définie pour la travée en cours
            For Each f_rep As cls_ForceRepartie In MyChargeU.FReparties(iTravee)
                InitialiseLigne(NCOL, HLIGNE, True)
                If lMultiS Then _
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee + 1))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(f_rep.xPosT(0), Enu_TypeVariable.Longueur, 3, 2, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(f_rep.Force(0), Enu_TypeVariable.ForceRepartie, 3, 2, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(f_rep.xPosT(1), Enu_TypeVariable.Longueur, 3, 2, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(f_rep.Force(1), Enu_TypeVariable.ForceRepartie, 3, 2, False))
            Next

            'End If
        Next

        FinTableau()

    End Sub

    Private Sub EditionParametresTableauQsurfTravee(ByVal MyBeam As cls_Poutre, MyChargeU As cls_ChargementUtilisateur)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition du tableau des charges surfaciques par travée
        '----------------------------------------------------------------------------------------------

        If nbLignes + 1.5 * (MyChargeU.NombreChargesSurf(MyBeam.IndicePremiereTravee, MyBeam.IndiceDerniereTravee) + 1) + 1 > MAXLIGNEPPAG Then SautePage()
        AddLigneNDC(TABW2 & "\G\I" & BlocG("QSURF") & "\i\g")

        AddLigneNDC("\TABLEAU 18")

        InitialiseLigne(2, HLIGNEENTETE, True)
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, BlocG("SPAN"))
        'AddCelluleFond(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, BlocG("QSURF_VALUE") & " (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "\+2\=)")
        AddCelluleFond(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, "Q" & " (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "\+2\=)")

        For iTravee As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee

            InitialiseLigne(2, HLIGNE, True)
            AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, iTravee)

            'If MyChargeU.NombreChargesSurf(iTravee, iTravee) = 0 Then
            '    AddCellule(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, "-")
            'Else
            AddCellule(LC1_2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyChargeU.QSurf(iTravee), Enu_TypeVariable.ChargeSurfacique, 3, 2, False))
            'End If

        Next

        FinTableau()

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


        AddTitreNdC(2, BlocG("COMBINATIONS"))


        AddTitreNdC(3, BlocG("ULSTATES"))
        If MyBeam.GetNbCombi(MyBeam.lCombELU) = 0 Then
            AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
        Else
            EditionTableauCombinaison(MyBeam, MyBeam.lCombELU, MyBeam.CoefCombELU)
        End If



        AddTitreNdC(3, BlocG("SLSTATES"))
        If MyBeam.GetNbCombi(MyBeam.lCombELS) = 0 Then
            AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
        Else
            EditionTableauCombinaison(MyBeam, MyBeam.lCombELS, MyBeam.CoefCombELS)
        End If


        AddTitreNdC(3, BlocG("FLSTATES"))
        If MyBeam.GetNbCombi(MyBeam.lCombFeu) = 0 Then
            AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
        Else
            EditionTableauCombinaison(MyBeam, MyBeam.lCombFeu, MyBeam.CoefCombFeu)
        End If

        If MyBeam.lMixte Then
            If MyBeam.GetNbCombi(MyBeam.lCombELCURules) = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                AddTitreNdC(3, BlocG("ELCUSTATES"))
                EditionTableauCombinaison(MyBeam, MyBeam.lCombELCURules, MyBeam.CoefCombELCU)
            End If

            If MyBeam.GetNbCombi(MyBeam.lCombELCSRules) = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                AddTitreNdC(3, BlocG("ELCSSTATES"))
                EditionTableauCombinaison(MyBeam, MyBeam.lCombELCURules, MyBeam.CoefCombELCU)
            End If
        End If

    End Sub

    Private Sub EditionTableauCombinaison(ByVal MyBeam As cls_Poutre, ByVal lCombo As Boolean(), CoefComb As List(Of Decimal)())
        Dim strCombo As String = ""
        Dim strCoeff As String = ""
        Dim strSymbol As String = ""
        Dim strSymbolsVariables() As String = {"Q1", "Q2", "QC"}
        Dim lTrouve As Boolean = False
        Dim iCdC As Integer = 0

        Dim indice_g As Integer

        For i As Integer = 0 To lCombo.Count - 1
            If lCombo(i) Then
                indice_g = CoefComb(i).Count - 1
                'on commence par la CP en phase de construction (le code ci-dessous est une version contractée du code qui suit après)

                If Not CoefComb(i)(indice_g) = 0 Then
                    strCoeff = GetStringInUnit(CoefComb(i)(indice_g), Enu_TypeVariable.SansType, 3, 2, False)
                    strSymbol = " g "
                    strCombo += strCoeff + strSymbol
                End If

                'puis on boucle sur les autres cas de charges (cela permet d'afficher 1.35g + 1.5QC et non 1.5Qc + 1.35g)

                For j = 0 To CoefComb(i).Count - 2
                    If Not CoefComb(i)(j) = 0 Then

                        If strCombo = "" Then
                            strCoeff = GetStringInUnit(CoefComb(i)(j), Enu_TypeVariable.SansType, 3, 2, False)
                        Else
                            strCoeff = " + " & GetStringInUnit(CoefComb(i)(j), Enu_TypeVariable.SansType, 3, 2, False)
                        End If

                        Select Case j
                            Case 0
                                strSymbol = " G "
                            Case 1
                                strSymbol = " Q1 "
                            Case 2
                                strSymbol = " Q2 "
                            Case 3
                                strSymbol = " QC "
                                'Case Else
                                '    strSymbol = " g "
                        End Select

                        If strSymbolsVariables.Contains(strSymbol.Trim) Then 'permt de ne pas afficher le symbole d'un cas de charge sans charge
                            While Not lTrouve And iCdC < MyBeam.ChargesA.Count
                                If MyBeam.ChargesA(iCdC).Symbol = strSymbol.Trim Then
                                    lTrouve = True
                                    If MyBeam.ChargesA(iCdC).lRunCalcul Then strCombo += strCoeff + strSymbol
                                End If
                                iCdC += 1
                            End While
                        Else
                            strCombo += strCoeff + strSymbol
                        End If

                    End If
                Next

                AddLigneNDC(TABW2 & strCombo)

            End If
        Next
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

        Dim lMixte As Boolean = MyBeam.lMixte
        Dim lEnrob As Boolean = MyBeam.lEnrobage

        '--> Initialisation

        If lEnrob Or lMixte Then
            SautePage()

            AddTitreNdC(1, BlocSP("SECTIONSPROPERTIES"))
        End If

        '--> Traitement

        If lMixte Then
            EditionProprietesSectionPoutreMixteN(MyBeam)
        ElseIf lEnrob Then
            EditionProprietesSectionAcierEnrobee(MyBeam)
        End If

    End Sub

    Private Sub EditionProprietesSectionAcierEnrobee(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une poutre acier avec enrobage partiel
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NeqEnrob As List(Of Decimal) = Nothing
        Dim NCOL As Integer = 6

        Dim zANP, MplRd As Decimal
        Dim zANPk, MplRk As Decimal
        Dim strFormatNoteFin As String = "\i"
        Dim strFormatNote As String = "\I"

        '--> Récupération des Coefficients d'équivalence

        MyBeam.ExtraireNeqEnrobage(NeqEnrob)

        '--> Tri des valeurs

        NeqEnrob.Sort()

        '--> Propriétés élastiques

        AddTitreNdC(2, BlocSP("EPROPERTIES"))

        AddLigneNDC(TABW2 & strFormatNote & BlocSP("NOTES") & ":" & strFormatNoteFin)
        AddLigneNDC(TABW2 & strFormatNote & BlocSP("NOTE_LOCATIONZ") & strFormatNoteFin)

        AddTitreNdC(3, BlocSP("POSITIVEB"))

        '--> Calcul des propriétés sous M>0

        '# Entete

        EnteteTableauPropElastiqueAcierEnrobe(1, NCOL)

        '# Boucle sur les valeurs de q

        For iTab As Integer = 0 To NeqEnrob.Count - 1

            LigneTableauPropElastiqueAcierEnrobe(MyBeam.Section, MyBeam.Param.Gamma, NeqEnrob(iTab), 1, NCOL)

        Next

        '#
        FinTableau()

        If MyBeam.lMultiSpan Then
            AddTitreNdC(3, BlocSP("NEGATIVEB"))
            '--> Calcul des propriétés sous M<0

            '# Entete

            EnteteTableauPropElastiqueAcierEnrobe(-1, NCOL)

            '# Boucle sur les valeurs de q

            For iTab As Integer = 0 To NeqEnrob.Count - 1

                LigneTableauPropElastiqueAcierEnrobe(MyBeam.Section, MyBeam.Param.Gamma, NeqEnrob(iTab), -1, NCOL)

            Next

            '#
            FinTableau()

        End If

        '--> Propriétés plastiques

        AddTitreNdC(2, BlocSP("PPROPERTIES"))
        AddTitreNdC(3, BlocSP("POSITIVEB"))

        MyBeam.Section.ProprietesPlastiquesMyy(1, True, MyBeam.Param.Gamma, 0, zANP, MplRd)
        MyBeam.Section.ProprietesPlastiquesMyy(1, False, MyBeam.Param.Gamma, 0, zANPk, MplRk)

        AddLigneNDC(TABW2 & BlocSP("MPLASTIC") & TABAFF & "M\-pl,Rd\=" & TABEGAL & GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 0, True))
        AddLigneNDC(TABW2 & BlocSP("ZPNA") & TABAFF & "z\-pl\=" & TABEGAL & GetStringInUnit(-zANP, Enu_TypeVariable.Dimension, 4, 0, True))
        AddLigneNDC(TABW2 & BlocSP("MPLASTICK") & TABAFF & "M\-pl,Rk\=" & TABEGAL & GetStringInUnit(MplRk, Enu_TypeVariable.Moment, 4, 0, True))

        If MyBeam.lMultiSpan Then

            AddTitreNdC(3, BlocSP("NEGATIVEB"))

            MyBeam.Section.ProprietesPlastiquesMyy(-1, True, MyBeam.Param.Gamma, 0, zANP, MplRd)
            MyBeam.Section.ProprietesPlastiquesMyy(-1, False, MyBeam.Param.Gamma, 0, zANPk, MplRk)

            AddLigneNDC(TABW2 & BlocSP("MPLASTIC") & TABAFF & "M\-pl,Rd\=" & TABEGAL & GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 0, True))
            AddLigneNDC(TABW2 & BlocSP("ZPNA") & TABAFF & "z\-pl\=" & TABEGAL & GetStringInUnit(-zANP, Enu_TypeVariable.Dimension, 4, 0, True))
            AddLigneNDC(TABW2 & BlocSP("MPLASTICK") & TABAFF & "M\-pl,Rk\=" & TABEGAL & GetStringInUnit(MplRk, Enu_TypeVariable.Moment, 4, 0, True))

        End If

    End Sub

    Private Sub LigneTableauPropElastiqueAcierEnrobe(MySection As cls_Section, MyGammas As cls_Gamma, NEq As Decimal, SigneM As Decimal, ByRef NCOL As Integer)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ligne du tableau des propriétés de sections pour une poutre acier avec enrobage partiel
        '-------------------------------------------------------------------------------------------
        '--> Déclaration

        Const pLC1 As Single = 8
        Const pLC2 As Single = 10
        Dim zANE, InertieY, MelRd As Decimal
        Dim InertieT, InertieZ As Decimal
        Dim zANE2, MelRd2 As Decimal

        '--> Calculs

        MySection.ProprietesElastiquesMyy(SigneM, True, MyGammas, NEq, zANE, InertieY, MelRd)
        MySection.ProprietesElastiquesMzz(1, True, MyGammas, NEq, zANE2, InertieZ, MelRd2)

        InertieT = MySection.InertieTorsionProfileEnrobe(NEq)

        InitialiseLigneTableau(NCOL, HLIGNE)

        AddCellule(pLC1, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(NEq, Enu_TypeVariable.SansType, 3, 2, False))
        AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(InertieY, Enu_TypeVariable.InertieCM4, 4, 0, False))
        AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(-zANE, Enu_TypeVariable.Dimension, 3, 2, False))
        If SigneM > 0 Then
            AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(InertieT, Enu_TypeVariable.InertieCM4, 4, 0, False))
            AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(InertieZ, Enu_TypeVariable.InertieCM4, 4, 0, False))
        End If
        AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MelRd, Enu_TypeVariable.Moment, 4, 0, False))

    End Sub

    Private Sub EnteteTableauPropElastiqueAcierEnrobe(SigneM As Decimal, ByRef NCOL As Integer)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entete du tableau des propriétés de sections pour une poutre acier avec enrobage partiel
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Const pLC1 As Single = 8
        Const pLC2 As Single = 10

        '--> Initialisaiton

        If SigneM > 0 Then NCOL = 6 Else NCOL = 4

        '--> Entête

        AddLigneNDC("\TABLEAU 20", False)
        InitialiseLigneTableau(NCOL, HLIGNEENTETE)

        AddCelluleFond(pLC1, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "n")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "I\-el,y\=")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "z\-el,y\=")
        If SigneM > 0 Then
            AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "I\-t\=")
            AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "I\-z\=")
        End If
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "M\-el,Rd\=")

        InitialiseLigneTableau(NCOL, HLIGNEENTETE)

        AddCelluleFond(pLC1, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "cm\+4\=")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "mm")
        If SigneM > 0 Then
            AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "cm\+4\=")
            AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "cm\+4\=")
        End If
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment))

    End Sub

    Private Sub EditionProprietesSectionPoutreMixteN(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   15/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une poutre mixte
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lDalle() As Boolean = Nothing
        Dim NeqDalle() As Decimal = Nothing
        Dim NeqEnrob() As Decimal = Nothing
        Dim strFormatNoteFin As String = "\i"
        Dim strFormatNote As String = "\I"
        Dim lMultiSpan As Boolean = MyBeam.lMultiSpan
        Dim lEnrob As Boolean = MyBeam.lEnrobage
        Dim bEff As Decimal
        Dim zANP, MplRd As Decimal
        Dim zANPk, MplRk As Decimal

        '--> Récupération des coeff d'équivalence et état de la dalle

        MyBeam.ExtraireListeNeqDalleEnrobage(lDalle, NeqDalle, NeqEnrob)

        '--> Propriétés élastiques

        AddLigneNDC(TABW1 & strFormatNote & BlocSP("NOTES") & ":" & strFormatNoteFin)
        AddLigneNDC(TABW1 & strFormatNote & BlocSP("NOTE_LOCATIONZSLAB") & strFormatNoteFin)

        '--> Propriétés à mi travée principale

        If lMultiSpan Then
            AddTitreNdC(2, BlocSP("PROPERTIESMIDSPANMAIN"))
        Else
            AddTitreNdC(2, BlocSP("PROPERTIESMIDSPAN"))
        End If

        '# Largeur efficace

        bEff = MyBeam.BeffDalle(MyBeam.LongueurTravee(1) / 2, 1, OptionsCalcul.lLargeurEfficaceSimplifiee, False)
        AddLigneNDC(TABW2 & BlocSP("EFFECTIVEW") & TABAFF & "b\-eff\=" & TABEGAL & GetStringInUnit(bEff, Enu_TypeVariable.Longueur, 3, 2, True))

        '# Propriétés élastiques

        AddTitreNdC(3, BlocSP("EPROPERTIES"))

        EditionProprietesElastiquesSectionMixtePositiveB(MyBeam, bEff, lDalle.GetUpperBound(0) + 1, lDalle, NeqDalle, NeqEnrob)

        '# Propriétés plastiques

        AddTitreNdC(3, BlocSP("PPROPERTIES"))

        MyBeam.Section.ProprietesPlastiquesMixteMyy(1, True, MyBeam.Param.Gamma, 0, bEff, MyBeam.Dalle, zANP, MplRd)
        MyBeam.Section.ProprietesPlastiquesMixteMyy(1, False, MyBeam.Param.Gamma, 0, bEff, MyBeam.Dalle, zANPk, MplRk)

        AddLigneNDC(TABW2 & BlocSP("MPLASTIC") & TABAFF & "M\-pl,Rd\=" & TABEGAL & GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 4, 0, True))
        AddLigneNDC(TABW2 & BlocSP("ZPNA") & TABAFF & "z\-pl\=" & TABEGAL & GetStringInUnit(zANP, Enu_TypeVariable.Dimension, 4, 1, True))
        AddLigneNDC(TABW2 & BlocSP("MPLASTICK") & TABAFF & "M\-pl,Rk\=" & TABEGAL & GetStringInUnit(MplRk, Enu_TypeVariable.Moment, 4, 0, True))

        '--> Propriétés console gauche

        If MyBeam.lTraveeConsoleGauche Then

            AddTitreNdC(2, BlocSP("PROPERTIESLCANTILEVER"))

            EditionProprietesElastiquesSectionConsoleMixte(MyBeam, True, lDalle.GetUpperBound(0), lDalle, NeqEnrob)

        End If

        '--> Propriétés console droite

        If MyBeam.lTraveeConsoleDroite Then

            AddTitreNdC(2, BlocSP("PROPERTIESRCANTILEVER"))

            EditionProprietesElastiquesSectionConsoleMixte(MyBeam, False, lDalle.GetUpperBound(0), lDalle, NeqEnrob)

        End If


    End Sub

    Private Sub EditionProprietesElastiquesSectionConsoleMixte(MyBeam As cls_Poutre, lGauche As Boolean, nbTab As Integer,
                                                               lDalle() As Boolean, NeqEnrob() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   15/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une poutre mixte
        '   Edition pour une console en M<0
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :   Poutre
        '   lGauche     [E] :   Indique si travée gauche ou droite
        '   lConstruction[E] :  Indique s'il y a une phase de contruction sans dalle
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim bEff As Decimal
        Dim iTravee As Integer
        Dim lConstruction As Boolean = lDalle.Contains(False)
        Dim lEnrob As Boolean = MyBeam.lEnrobage
        Dim zANE, InertieY, MelRd, zANP, MplRd As Decimal
        Dim strDebut As String = ""

        '# Largeur efficace

        If lGauche Then
            iTravee = 0
            bEff = MyBeam.BeffDalle(MyBeam.LongueurTravee(iTravee), iTravee, OptionsCalcul.lLargeurEfficaceSimplifiee, False)
        Else
            iTravee = MyBeam.IndiceDerniereTravee
            bEff = MyBeam.BeffDalle(0, iTravee, OptionsCalcul.lLargeurEfficaceSimplifiee, False)
        End If

        AddLigneNDC(TABW2 & BlocSP("EFFECTIVEW") & TABAFF & "b\-eff\=" & TABEGAL & GetStringInUnit(bEff, Enu_TypeVariable.Longueur, 3, 2, True))

        '# Propriétés élastiques

        AddTitreNdC(3, BlocSP("EPROPERTIES"))

        '--- Si section avec enrobage, on dépend encore des coefficients d'équivalence

        If lEnrob Then
            EditionProprietesElastiquesSectionConsoleMixteEnrobage(MyBeam, bEff, nbTab, lDalle, NeqEnrob)
        Else
            '--- En phase de construction

            If lConstruction Then

                MyBeam.Section.ProfilA.ProprietesMyy(-1, True, MyBeam.Param.Gamma.GammaM0, zANE, InertieY, MelRd, zANP, MplRd)

                AddLigneNDC(TABW3 & "\U" & BlocSP("CONSTRUCTIONP") & "\u" &
                            TABAFF & "I\-el,y\=" & TABEGAL & GetStringInUnit(InertieY, Enu_TypeVariable.InertieCM4, 3, 2, True))
                AddLigneNDC(TABAFF & "z\-el,y\=" & TABEGAL & GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 3, 2, True))

            End If

            '--- En phase mixte

            MyBeam.Section.ProprietesElastiquesMixteMyy(-1, True, MyBeam.Param.Gamma, 1, 1, bEff, MyBeam.Dalle, zANE, InertieY, MelRd)

            If lConstruction Then
                strDebut = TABW3 & "\U" & BlocSP("COMPOSITEP") & "\u"
            Else
                strDebut = ""
            End If

            AddLigneNDC(strDebut & TABAFF & "I\-el,y\=" & TABEGAL & GetStringInUnit(InertieY, Enu_TypeVariable.InertieCM4, 3, 2, True))
            AddLigneNDC(TABAFF & "z\-el,y\=" & TABEGAL & GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 3, 2, True))

        End If

        '# Propriétés élastiques

        AddTitreNdC(3, BlocSP("PPROPERTIES"))

        MyBeam.Section.ProprietesPlastiquesMixteMyy(-1, True, MyBeam.Param.Gamma, 0, bEff, MyBeam.Dalle, zANP, MplRd)

        AddLigneNDC(TABAFF & "M\-pl,Rd\=" & TABEGAL & GetStringInUnit(MplRd, Enu_TypeVariable.Moment, 3, 2, True))
        AddLigneNDC(TABAFF & "z\-pl,y\=" & TABEGAL & GetStringInUnit(zANP, Enu_TypeVariable.Dimension, 3, 2, True))

    End Sub

    Private Sub EditionProprietesElastiquesSectionConsoleMixteEnrobage(MyBeam As cls_Poutre, bEff As Decimal, nbTab As Integer,
                                                                       lDalle() As Boolean, NeqEnrob() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   15/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une poutre mixte avec enrobage partiel
        '   Edition en console en M<0
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :   Poutre
        '   bEff        [E] :   Largeur efficace dalle
        '   nbTab       [E] :   Nombre de ligne dans le tableau
        '   lDalle      [E] :   Indique si dalle active dans la ligne
        '   NeqEnrob    [E] :   Coefficients d'équivalence pour le béton de l'enrobage
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim NCOL As Integer = 4
        Dim zANE, InertieY, MelRd As Decimal
        Dim LargCol() As Integer = {20, 12, 10, 10}
        Dim iTab As Integer
        Dim strPhase As String

        '--> Entête du Tableau

        AddLigneNDC("\TABLEAU 20", False)
        InitialiseLigneTableau(NCOL, HLIGNEENTETE)

        AddCelluleFond(LargCol(0), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, BlocSP("STAGE"))

        AddCelluleFond(LargCol(1), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "n")
        AddCelluleFond(LargCol(2), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "I\-el,y\=")
        AddCelluleFond(LargCol(3), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "z\-el,y\=")

        InitialiseLigneTableau(NCOL, HLIGNEENTETE)

        AddCelluleFond(LargCol(0), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "")

        AddCelluleFond(LargCol(1), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, BlocSP("ENCASEMENT").ToLower)
        AddCelluleFond(LargCol(2), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "cm\+4\=")
        AddCelluleFond(LargCol(3), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "mm")

        '--> Ligne du tableau

        For iTab = 0 To nbTab - 1
            If lDalle(iTab) Then
                MyBeam.Section.ProprietesElastiquesMixteMyy(-1, True, MyBeam.Param.Gamma, NeqEnrob(iTab), 1, bEff, MyBeam.Dalle, zANE, InertieY, MelRd)
                strPhase = BlocSP("COMPOSITE")
            Else
                MyBeam.Section.ProprietesElastiquesMyy(-1, True, MyBeam.Param.Gamma, NeqEnrob(iTab), zANE, InertieY, MelRd)
                strPhase = BlocSP("STEELENCASED")
            End If

            InitialiseLigneTableau(NCOL, HLIGNE)

            AddCellule(LargCol(0), Bordures.Tous, PositionTexteInCell.Centre, strPhase)
            AddCellule(LargCol(1), Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(NeqEnrob(iTab), Enu_TypeVariable.SansType, 3, 2, False))
            AddCellule(LargCol(2), Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(InertieY, Enu_TypeVariable.Inertie, 4, 2, False))
            AddCellule(LargCol(3), Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 4, 2, False))

        Next

        FinTableau()

    End Sub

    Private Sub EditionProprietesElastiquesSectionMixtePositiveB(MyBeam As cls_Poutre, bEff As Decimal, nbTab As Integer,
                                                                 lDalle() As Boolean, NeqDalle() As Decimal, NeqEnrob() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   15/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des propriétés de sections pour une poutre mixte
        '   Edition à mi-travée en M>0
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :   Poutre
        '   bEff        [E] :   Largeur efficace dalle
        '   nbTab       [E] :   Nombre de ligne dans le tableau
        '   lDalle      [E] :   Indique si dalle active dans la ligne
        '   NeqDalle    [E] :   Coefficients d'équivalence pour le béton de la dalle
        '   NeqEnrob    [E] :   Coefficients d'équivalence pour le béton de l'enrobage
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lEnrob As Boolean = MyBeam.lEnrobage
        Dim NCOL As Integer
        Dim zANE, InertieY, MelRd As Decimal
        Const pLC1 As Single = 12
        Const pLC2 As Single = 10

        '--> Entête du tableau

        EnteteTableauPropElastiqueMixte(lEnrob, NCOL)

        '--> Lignes du tableau

        For iTab As Integer = 0 To nbTab - 1

            MyBeam.Section.ProprietesElastiquesMixteMyy(1, True, MyBeam.Param.Gamma, NeqEnrob(iTab), NeqDalle(iTab), bEff,
                                             MyBeam.Dalle, zANE, InertieY, MelRd, lDalle(iTab))

            InitialiseLigneTableau(NCOL, HLIGNE)

            If lDalle(iTab) Then
                AddCellule(pLC1, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(NeqDalle(iTab), Enu_TypeVariable.SansType, 3, 2, False))
            Else
                AddCellule(pLC1, Bordures.Tous, PositionTexteInCell.Centre, "-")
            End If
            If lEnrob Then
                AddCellule(pLC1, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(NeqEnrob(iTab), Enu_TypeVariable.SansType, 3, 2, False))
            End If
            AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(InertieY, Enu_TypeVariable.InertieCM4, 4, 0, False))
            AddCellule(pLC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(zANE, Enu_TypeVariable.Dimension, 3, 2, False))

        Next


        FinTableau()

    End Sub

    Private Sub EnteteTableauPropElastiqueMixte(lEnrob As Boolean, ByRef NCOL As Integer)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entete du tableau des propriétés de sections pour une poutre mixte avec ou sans enrobage partiel
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Const pLC1 As Single = 12
        Const pLC2 As Single = 10

        '--> Initialisation

        If lEnrob Then NCOL = 4 Else NCOL = 3

        '--> Entête

        AddLigneNDC("\TABLEAU 20", False)
        InitialiseLigneTableau(NCOL, HLIGNEENTETE)

        AddCelluleFond(pLC1, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "n")
        If lEnrob Then _
           AddCelluleFond(pLC1, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "n")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "I\-el,y\=")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "z\-el,y\=")

        InitialiseLigneTableau(NCOL, HLIGNEENTETE)

        AddCelluleFond(pLC1, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, BlocSP("SLAB").ToLower)
        If lEnrob Then _
           AddCelluleFond(pLC1, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, BlocSP("ENCASEMENT").ToLower)

        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "cm\+4\=")
        AddCelluleFond(pLC2, Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "mm")

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

        Const nbminCombi As Integer = 12
        Dim lRetraitELU As Boolean = False                      '#ALERTE Pour le moment, à pondérer plus tard

        '--> En fonction des options NDC

        If Not (OptionsNdC.lDispFM_FLS Or OptionsNdC.lDispFM_SLS Or OptionsNdC.lDispFM_ULS Or OptionsNdC.lDispFMLoadCase) Then
            Exit Sub
        End If

        '--> Initialisation

        SautePage()

        AddTitreNdC(1, BlocAnalyse("ANALYSIS"))

        '--> Listes de cas de charges

        EditionListeCdCdansAnalyse(MyBeam)

        '--> Analyses par cas de charge

        If OptionsNdC.lDispFMLoadCase Then
            AddTitreNdC(2, BlocAnalyse("ELEMNTRY_LC"))

            For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count - 1

                '--> On affiche le cas de charge uniquement si le cas de charge est disponible
                If MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).lRunCalcul Then
                    EditionAnalyseChargeA(MyProjet.Poutres(MyProjet.IndEnCours), MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i))

                    '--> On affiche le diagramme des efforts si l'option est activée 
                    If OptionsNdC.lDispFMDiagrams Then
                        Const NbLigDiag As Integer = 20
                        If nbLignes + NbLigDiag > MAXLIGNEPPAG Then SautePage()
                        ' Les options 10, 80 30 et cadre doivent toujous commencer en 3 eme place
                        AddLigneNDC("\IMG RDM_CHARGESA " & " 10 80 30 NoCadre " & CStr(i))
                        nbLignes += NbLigDiag
                    End If

                End If
            Next
        End If

        '--> Analyses par combinaisons ELU

        If OptionsNdC.lDispFM_ULS Then

            If nbLignes + nbminCombi > MAXLIGNEPPAG Then SautePage()

            AddTitreNdC(2, BlocAnalyse("ELEMNTRY_ULS"))

            If MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU.nbCombi = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU.nbCombi - 1

                    EditionAnalyseCombiELU(MyProjet.Poutres(MyProjet.IndEnCours), i)

                    '--> On affiche le diagramme des efforts si l'option est activée 
                    If OptionsNdC.lDispFMDiagrams Then
                        Const NbLigDiag As Integer = 20
                        If nbLignes + NbLigDiag > MAXLIGNEPPAG Then SautePage()
                        ' Les options 10, 80 30 et cadre doivent toujous commencer en 3 eme place
                        AddLigneNDC("\IMG RDM_COMBO " & " 10 80 30 NoCadre " & CStr(i) & " ELU " & lRetraitELU)
                        nbLignes += NbLigDiag
                    End If

                Next
            End If
        End If

        '--> Analyses par combinaisons ELS

        If OptionsNdC.lDispFM_SLS Then

            If nbLignes + nbminCombi > MAXLIGNEPPAG Then SautePage()

            AddTitreNdC(2, BlocAnalyse("ELEMNTRY_SLS"))


            If MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELS.nbCombi = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELS.nbCombi - 1

                    EditionAnalyseCombiELS(MyProjet.Poutres(MyProjet.IndEnCours), i)

                Next
            End If
        End If

        '--> Analyses par combinaisons ELF

        If OptionsNdC.lDispFM_FLS Then

            If nbLignes + nbminCombi > MAXLIGNEPPAG Then SautePage()

            AddTitreNdC(2, BlocAnalyse("ELEMNTRY_FLS"))

            If MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELF.nbCombi = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELF.nbCombi - 1

                    EditionAnalyseCombiELF(MyProjet.Poutres(MyProjet.IndEnCours), i)

                Next
            End If
        End If

        '--> Analyses par combinaisons ELU pendant la phase de construction

        If OptionsNdC.lDispFM_ULS And MyBeam.lMixte Then

            If nbLignes + nbminCombi > MAXLIGNEPPAG Then SautePage()

            AddTitreNdC(2, BlocAnalyse("ELEMNTRY_ULSC"))

            If MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELCU.nbCombi = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELCU.nbCombi - 1

                    EditionAnalyseCombiELU_Construction(MyProjet.Poutres(MyProjet.IndEnCours), i)

                    '--> On affiche le diagramme des efforts si l'option est activée 
                    If OptionsNdC.lDispFMDiagrams Then
                        Const NbLigDiag As Integer = 20
                        If nbLignes + NbLigDiag > MAXLIGNEPPAG Then SautePage()
                        ' Les options 10, 80 30 et cadre doivent toujous commencer en 3 eme place
                        AddLigneNDC("\IMG RDM_COMBO " & " 10 80 30 NoCadre " & CStr(i) & " ELUC " & lRetraitELU)
                        nbLignes += NbLigDiag
                    End If

                Next
            End If
        End If

        '--> Analyses par combinaisons ELS pendant la phase de construction

        If OptionsNdC.lDispFM_SLS And MyBeam.lMixte Then

            If nbLignes + nbminCombi > MAXLIGNEPPAG Then SautePage()

            AddTitreNdC(2, BlocAnalyse("ELEMNTRY_SLSC"))


            If MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELCS.nbCombi = 0 Then
                AddLigneNDC(TABW2 & BlocG("NOCOMBO"))
            Else
                For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELCS.nbCombi - 1

                    EditionAnalyseCombiELS_Construction(MyProjet.Poutres(MyProjet.IndEnCours), i)

                Next
            End If
        End If

    End Sub

    Private Sub EditionListeCdCdansAnalyse(myPoutre As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition la liste des cas de charges dans l'analyse de la poutre
        '   qui diffère des cas de charge définis par l'utilisateur
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Poutre
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pLC() As Single = {40, 20, 12, 12}
        Dim NCOL As Integer
        Dim lMixte As Boolean = myPoutre.lMixte
        Dim lEnrob As Boolean = myPoutre.lEnrobage
        Dim iDebTrav As Integer = myPoutre.IndicePremiereTravee
        Dim iFinTrav As Integer = myPoutre.IndiceDerniereTravee
        Dim Phase As String = ""
        Dim Titre As String = ""
        Dim iTab As Integer
        Dim lDalle As Boolean
        Dim pBordures As Integer = Bordures.Gauche + Bordures.Droite
        Dim lAffiche() As Boolean = Nothing
        Dim iLastCase, iCas, nbCas As Integer
        Dim lNoteConfig As Boolean = False
        Dim SymbolConfig() As String = {"Q1#1", "Q1#2", "Q1#3", "Q2#1", "Q2#2", "Q2#3"}

        '--> Initialisation

        If lMixte And lEnrob Then
            NCOL = 4
        ElseIf lMixte Or lEnrob Then
            NCOL = 3
        Else
            NCOL = 1
        End If
        nbCas = 0
        ReDim lAffiche(myPoutre.ChargesA.Count - 1)
        For iCas = 0 To myPoutre.ChargesA.Count - 1
            lAffiche(iCas) = myPoutre.ChargesA(iCas).EstNonNul(iDebTrav, iFinTrav)
            If lAffiche(iCas) Then
                iLastCase = iCas
                nbCas += 1
            End If
        Next

        '--> Traitement

        AddTitreNdC(2, BlocAnalyse("LOADCASELIST"))

        '# Entête

        AddLigneNDC("\TABLEAU 10", False)

        '--> Entete

        InitialiseLigneTableau(NCOL, HLIGNE)

        AddCelluleFond(pLC(0), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, BlocAnalyse("LOADCASES"))
        If lMixte Or lEnrob Then
            AddCelluleFond(pLC(1), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, BlocAnalyse("PHASE"))
        End If
        If lMixte Then
            AddCelluleFond(pLC(2), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "n")
        End If
        If lEnrob Then
            AddCelluleFond(pLC(3), Bordures.Tous - Bordures.Bas, PositionTexteInCell.Centre, "n")
        End If

        InitialiseLigneTableau(NCOL, HLIGNE)

        AddCelluleFond(pLC(0), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "")
        If lMixte Or lEnrob Then
            AddCelluleFond(pLC(1), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, "")
        End If
        If lMixte Then
            AddCelluleFond(pLC(2), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, BlocAnalyse("SLAB"))
        End If
        If lEnrob Then
            AddCelluleFond(pLC(3), Bordures.Tous - Bordures.Haut, PositionTexteInCell.Centre, BlocAnalyse("ENCASEMENT"))
        End If

        '--> Boucles sur les cas de charges

        For iCas = 0 To myPoutre.ChargesA.Count - 1

            If lAffiche(iCas) Then
                InitialiseLigneTableau(NCOL, HLIGNE)

                If iCas = iLastCase Then pBordures += Bordures.Bas

                '# Titre du cas de charge

                Titre = GetTitreFromSymbole(myPoutre.ChargesA(iCas).Symbol)
                AddCellule(pLC(0), pBordures, PositionTexteInCell.Gauche, myPoutre.ChargesA(iCas).Symbol & " " & Titre)

                If SymbolConfig.Contains(myPoutre.ChargesA(iCas).Symbol) Then lNoteConfig = True

                '# Phase

                iTab = myPoutre.ChargesA(iCas).IndElts

                If lMixte Or lEnrob Then

                    If lMixte Then
                        If myPoutre.Elements(iTab).lMixte Then
                            Phase = BlocAnalyse("COMPOSITE")
                            lDalle = True
                        Else
                            lDalle = False
                            If lEnrob Then
                                Phase = BlocAnalyse("STEELENCASED")
                            Else
                                Phase = BlocAnalyse("STEELONLY")
                            End If
                        End If
                    Else
                        Phase = BlocAnalyse("STEELENCASED")
                    End If

                    AddCellule(pLC(1), pBordures, PositionTexteInCell.Gauche, Phase)
                End If

                '# Coefficient d'équivalence dalle

                If lMixte Then
                    If lDalle Then
                        AddCellule(pLC(2), pBordures, PositionTexteInCell.Centre, GetStringInUnit(myPoutre.Elements(iTab).nEqDalle, Enu_TypeVariable.SansType, 3, 2, False))
                    Else
                        AddCellule(pLC(2), pBordures, PositionTexteInCell.Centre, "-")
                    End If
                End If

                '# Coefficient d'équivalence enrobage

                If lEnrob Then
                    AddCellule(pLC(3), pBordures, PositionTexteInCell.Centre, GetStringInUnit(myPoutre.Elements(iTab).nEqEnrob, Enu_TypeVariable.SansType, 3, 2, False))
                End If

            End If

        Next

        FinTableau()

        '--> Note sur les configurations

        Dim strFormatNoteFin As String = "\i"
        Dim strFormatNote As String = "\I"
        Dim strPuce As String = "- "

        If lNoteConfig Then
            AddLigneNDC(TABW2 & strFormatNote & BlocAnalyse("NOTES"))
            AddLigneNDC(TABW3 & strPuce & BlocAnalyse("CONFIGURATION1"))
            AddLigneNDC(TABW3 & strPuce & BlocAnalyse("CONFIGURATION2"))
            AddLigneNDC(TABW3 & strPuce & BlocAnalyse("CONFIGURATION3") & strFormatNoteFin)
        End If

    End Sub

    Private Function GetTitreFromSymbole(Symbol As String) As String
        '-------------------------------------------------------------------------------------------
        '   15/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Renvoie le titre du cas de charge en fonction de son abbréviation
        '-------------------------------------------------------------------------------------------
        '   Symbol      [E] :   Abbréviation du cas de charge
        '-------------------------------------------------------------------------------------------

        Dim pTitre As String = BlocAnalyse("NOTFOUND")
        Dim Config As String

        Select Case Symbol.ToUpper
            Case "G"
                pTitre = BlocAnalyse("PERMANENTL")
            Case "G1"
                pTitre = BlocAnalyse("SELFW")
            Case "G2"
                pTitre = BlocAnalyse("OTHERPERMANENTL")
            Case "G1PP"
                pTitre = BlocAnalyse("SELFWWITHPROPPS")
            Case "G1C"
                pTitre = BlocAnalyse("SELFWWITHOUTPROPPS")
            Case "Q1", "Q2"
                pTitre = BlocAnalyse("LIVEL") & " " & Symbol
            Case "SHC"
                pTitre = BlocAnalyse("SHRINKAGEL")
            Case "QC"
                pTitre = BlocAnalyse("CONSTRUCTIONL")
            Case "Q1#1", "Q1#2", "Q1#3"
                Config = Symbol.Substring(3, 1)
                pTitre = BlocAnalyse("LIVEL") & " Q1 - " & BlocAnalyse("CONFIGURATION") & Config
            Case "Q2#1", "Q2#2", "Q2#3"
                Config = Symbol.Substring(3, 1)
                pTitre = BlocAnalyse("LIVEL") & " Q2 - " & BlocAnalyse("CONFIGURATION") & Config

        End Select


        Return pTitre

    End Function


    Private Sub EditionAnalyseCombiELU(myPoutre As cls_Poutre, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Poutre
        '   iCombi      [E] :   Indice de la combinaison ELU
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim Mmin, Mmax, Vmin, Vmax As Decimal
        Dim iNodeMinMoment As Integer = -1
        Dim iNodeMaxMoment As Integer = -1
        Dim iNodeMinTranchant As Integer = -1
        Dim iNodeMaxTranchant As Integer = -1

        '--> Initialisation

        lRetrait = False                      '#ALERTE Pour le moment, à pondérer plus tard

        '--> Affichage de la combinaison

        Dim strELU As String = BlocAnalyse("ULS")
        strELU += "_0"

        AffichageCombinaisonCharge(myPoutre, myPoutre.CombiA_ELU, strELU & CStr(iCombi), iCombi, lRetrait)

        '--> Calcul des M et V

        myPoutre.CombiA_ELU.CombineMoments(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, lRetrait)
        myPoutre.CombiA_ELU.CombineEffortsT(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, lRetrait)

        'Récupère les valeurs enveloppes
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(VEd, VEd.GetUpperBound(0) + 1, Vmax, Vmin, iNodeMaxTranchant, iNodeMinTranchant)
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(MEd, MEd.GetUpperBound(0) + 1, Mmax, Mmin, iNodeMaxMoment, iNodeMinMoment)

        '--> Affichage de la combinaison

        EditionTableauEfforts(myPoutre, MEd, VEd,
                              Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                              Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

    End Sub

    Private Sub EditionAnalyseCombiELU_Construction(myPoutre As cls_Poutre, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Poutre
        '   iCombi      [E] :   Indice de la combinaison ELU
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim Mmin, Mmax, Vmin, Vmax As Decimal
        Dim iNodeMinMoment As Integer = -1
        Dim iNodeMaxMoment As Integer = -1
        Dim iNodeMinTranchant As Integer = -1
        Dim iNodeMaxTranchant As Integer = -1

        '--> Initialisation

        lRetrait = True

        '--> Affichage de la combinaison

        Dim strELU As String = BlocAnalyse("ULS")
        strELU += "_C_0"

        AffichageCombinaisonCharge(myPoutre, myPoutre.CombiA_ELCU, strELU & CStr(iCombi), iCombi, lRetrait)

        '--> Calcul des M et V

        myPoutre.CombiA_ELCU.CombineMoments(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, lRetrait)
        myPoutre.CombiA_ELCU.CombineEffortsT(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, lRetrait)

        'Récupère les valeurs enveloppes
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(VEd, VEd.GetUpperBound(0) + 1, Vmax, Vmin, iNodeMaxTranchant, iNodeMinTranchant)
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(MEd, MEd.GetUpperBound(0) + 1, Mmax, Mmin, iNodeMaxMoment, iNodeMinMoment)

        '--> Affichage de la combinaison

        EditionTableauEfforts(myPoutre, MEd, VEd,
                              Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                              Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

    End Sub

    Private Sub EditionTableauEfforts(myPoutre As cls_Poutre, MEd(,) As Decimal, VEd(,) As Decimal,
                                      Mmin As Decimal, Mmax As Decimal, iNodeMinMoment As Integer, iNodeMaxMoment As Integer,
                                      Vmin As Decimal, Vmax As Decimal, iNodeMinTranchant As Integer, iNodeMaxTranchant As Integer)

        '-------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition d'un tableau d'efforts et moments
        '-------------------------------------------------------------------------------------------
        '   myPoutre        [E]
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMultispan As Boolean
        Dim NCol, PosTab As Integer
        Dim iTravee, iNode As Integer
        Dim iNodeO, iNodeE As Integer
        Dim iTravDeb, iTravFin As Integer
        Dim iTraveeAffichee As Integer = 1
        'Dim iCompteur As Integer = 0
        'Dim NbLignesMax() As Integer = {25, 30}
        Dim iTab As Integer = 0

        '--> Initialisation

        lMultispan = (myPoutre.NbTravees > 1)
        iTravDeb = myPoutre.IndicePremiereTravee
        iTravFin = myPoutre.IndiceDerniereTravee

        '--> Affichage

        '# Entête

        EnteteTableauAnalyseCombi(lMultispan, NCol, PosTab)

        '# Tableau

        For iTravee = iTravDeb To iTravFin
            iNodeO = myPoutre.Nodes.iNodeExtTrav(iTravee, 0)
            iNodeE = myPoutre.Nodes.iNodeExtTrav(iTravee, 1)

            '=== Extrémité gauche

            If iTravee = iTravDeb Then

                LigneTableauMVCombiExtremite(lMultispan, True, NCol, PosTab, iNode, iTraveeAffichee, myPoutre.Nodes.xTravee(iNode), myPoutre.Nodes.xGlobal(iNode),
                                             VEd(0, 1), MEd(0, 1),
                                        Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                                        Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)
                'iCompteur += 1

            End If

            '=== Lignes intermédiaires

            For iNode = iNodeO + 1 To iNodeE - 1

                'iCompteur += 1
                'nbLignes += 1 * EquivalenceLigneTableau

                'If iCompteur > NbLignesMax(iTab) Then
                If nbLignes > MAXLIGNEPPAG Then
                    FinTableau()
                    'iCompteur = 0
                    iTab = 1
                    SautePage()
                    EnteteTableauAnalyseCombi(lMultispan, NCol, PosTab)

                End If

                LigneTableauMVCombi(lMultispan, NCol, PosTab, iNode, iTraveeAffichee, myPoutre.Nodes.xGlobal(iNode), myPoutre.Nodes.xTravee(iNode),
                                        VEd(iNode, 0), VEd(iNode, 1), MEd(iNode, 0), MEd(iNode, 1),
                                        Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                                        Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

            Next

            '=== Appui droite

            If iTravee = iTravFin Then
                LigneTableauMVCombiExtremite(lMultispan, False, NCol, PosTab, iNodeE, iTraveeAffichee, myPoutre.Nodes.xTravee(iNodeE), myPoutre.Nodes.xGlobal(iNodeE),
                                             VEd(iNodeE, 0), MEd(iNodeE, 0),
                                        Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                                        Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)
            Else
                LigneTableauMVCombiAppui(NCol, PosTab, iNodeE, iTraveeAffichee, myPoutre.Nodes.xTravee(iNode), myPoutre.Nodes.xGlobal(iNode),
                                         VEd(iNode, 0), VEd(iNode, 1), MEd(iNode, 0), MEd(iNode, 1),
                                        Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                                        Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)
            End If
            'iCompteur += 1
            iTraveeAffichee += 1
        Next

        '# Fin du Tableau

        FinTableau()

    End Sub

    Private Sub EditionAnalyseCombiELF(myPoutre As cls_Poutre, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison feu
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Indice de la poutre
        '   iCombi      [E] :   Indice de la combinaison ELU
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim Mmin, Mmax, Vmin, Vmax As Decimal
        Dim iNodeMinMoment As Integer = -1
        Dim iNodeMaxMoment As Integer = -1
        Dim iNodeMinTranchant As Integer = -1
        Dim iNodeMaxTranchant As Integer = -1

        '--> Initialisation

        lRetrait = True

        '--> Affichage de la combinaison

        AffichageCombinaisonCharge(myPoutre, myPoutre.CombiA_ELF, "ELF_0" & CStr(iCombi), iCombi, lRetrait)

        '--> Calcul des M et V

        myPoutre.CombiA_ELF.CombineMoments(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, lRetrait)
        myPoutre.CombiA_ELF.CombineEffortsT(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, lRetrait)

        'Récupère les valeurs enveloppes
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(VEd, VEd.GetUpperBound(0) + 1, Vmax, Vmin, iNodeMaxTranchant, iNodeMinTranchant)
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(MEd, MEd.GetUpperBound(0) + 1, Mmax, Mmin, iNodeMaxMoment, iNodeMinMoment)

        '--> Affichage de la combinaison

        EditionTableauEfforts(myPoutre, MEd, VEd,
            Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
            Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

    End Sub

    Private Sub EditionAnalyseCombiELS(myPoutre As cls_Poutre, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison ELS
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Indice de la poutre
        '   iCombi      [E] :   Indice de la combinaison ELU
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim Mmin, Mmax, Vmin, Vmax As Decimal
        Dim iNodeMinMoment As Integer = -1
        Dim iNodeMaxMoment As Integer = -1
        Dim iNodeMinTranchant As Integer = -1
        Dim iNodeMaxTranchant As Integer = -1

        '--> Initialisation

        lRetrait = True

        '--> Affichage de la combinaison

        Dim strELS As String = BlocAnalyse("SLS")
        strELS += "_0"

        AffichageCombinaisonCharge(myPoutre, myPoutre.CombiA_ELS, strELS & CStr(iCombi), iCombi, lRetrait)

        '--> Calcul des M et V

        myPoutre.CombiA_ELS.CombineMoments(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, lRetrait)
        myPoutre.CombiA_ELS.CombineEffortsT(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, lRetrait)

        'Récupère les valeurs enveloppes
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(VEd, VEd.GetUpperBound(0) + 1, Vmax, Vmin, iNodeMaxTranchant, iNodeMinTranchant)
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(MEd, MEd.GetUpperBound(0) + 1, Mmax, Mmin, iNodeMaxMoment, iNodeMinMoment)

        '--> Affichage de la combinaison

        EditionTableauEfforts(myPoutre, MEd, VEd,
                              Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                              Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

    End Sub

    Private Sub EditionAnalyseCombiELS_Construction(myPoutre As cls_Poutre, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des efforts dans la poutre après analyse pour une combinaison ELS
        '-------------------------------------------------------------------------------------------
        '   myPoutre    [E] :   Indice de la poutre
        '   iCombi      [E] :   Indice de la combinaison ELU
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lRetrait As Boolean = True
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim Mmin, Mmax, Vmin, Vmax As Decimal
        Dim iNodeMinMoment As Integer = -1
        Dim iNodeMaxMoment As Integer = -1
        Dim iNodeMinTranchant As Integer = -1
        Dim iNodeMaxTranchant As Integer = -1

        '--> Initialisation

        lRetrait = True

        '--> Affichage de la combinaison

        Dim strELS As String = BlocAnalyse("SLS")
        strELS += "_C_0"

        AffichageCombinaisonCharge(myPoutre, myPoutre.CombiA_ELCS, strELS & CStr(iCombi), iCombi, lRetrait)

        '--> Calcul des M et V

        myPoutre.CombiA_ELCS.CombineMoments(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, MEd, lRetrait)
        myPoutre.CombiA_ELCS.CombineEffortsT(iCombi, myPoutre.Nodes.nbNodes, myPoutre.ChargesA, VEd, lRetrait)

        'Récupère les valeurs enveloppes
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(VEd, VEd.GetUpperBound(0) + 1, Vmax, Vmin, iNodeMaxTranchant, iNodeMinTranchant)
        PMXMoteur2.Mod_Outils.EnveloppeTableauEfforts(MEd, MEd.GetUpperBound(0) + 1, Mmax, Mmin, iNodeMaxMoment, iNodeMinMoment)

        '--> Affichage de la combinaison

        EditionTableauEfforts(myPoutre, MEd, VEd,
                              Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
                              Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

    End Sub

    Private Sub LigneTableauMVCombiExtremite(lMultiSpan As Boolean, lGauche As Boolean, NCol As Integer, Pos As Integer,
                                             iNode As Integer, iTravee As Integer, xPosT As Decimal, xPosG As Decimal,
                                             VEd As Decimal, MEd As Decimal,
                                             Mmin As Decimal, Mmax As Decimal, iNodeMinMoment As Integer, iNodeMaxMoment As Integer,
                                             Vmin As Decimal, Vmax As Decimal, iNodeMinTranchant As Integer, iNodeMaxTranchant As Integer)
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
        '   lGrasVEd    [E] :   Indique si on doit afficher la valeur de VEd en gras ou non
        '   lGrasMEd    [E] :   Indique si on doit afficher la valeur de MEd en gras ou non
        '-------------------------------------------------------------------------------------------

        '--> Déclaration variables locales
        Dim stringVEd, stringMEd As String


        InitialiseLigneTableau(NCol, HLIGNE)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iNode + 1))

        '# Position et travée

        If lMultiSpan Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee))

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))
        Else

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))

        End If

        '# Effort tranchant

        stringVEd = GetStringInUnit(VEd, Enu_TypeVariable.Effort, 3, 2, False)

        If iNode = iNodeMinTranchant Or iNode = iNodeMaxTranchant Then
            If VEd = Vmin Or VEd = Vmax Then
                stringVEd = "\G" & stringVEd & "\g" 'on met le texte en gras
            End If
        End If

        If lGauche Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEd)
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEd)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
        End If

        '# Moment fléchissant

        stringMEd = GetStringInUnit(MEd, Enu_TypeVariable.Effort, 3, 2, False)

        If iNode = iNodeMinMoment Or iNode = iNodeMaxMoment Then
            If MEd = Mmin Or MEd = Mmax Then
                stringMEd = "\G" & stringMEd & "\g" 'on met le texte en gras
            End If
        End If

        If lGauche Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEd)
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEd)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, "")
        End If

    End Sub

    Private Sub LigneTableauMVCombiAppui(NCol As Integer, Pos As Integer,
                                         iNode As Integer, iTravee As Integer, xPosT As Decimal, xPosG As Decimal,
                                         VEdG As Decimal, VEdd As Decimal, MEdG As Decimal, MEdD As Decimal,
                                             Mmin As Decimal, Mmax As Decimal, iNodeMinMoment As Integer, iNodeMaxMoment As Integer,
                                             Vmin As Decimal, Vmax As Decimal, iNodeMinTranchant As Integer, iNodeMaxTranchant As Integer)
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
        Dim stringVEdG, stringVEdD, stringMEdG, stringMEdD As String
        stringMEdG = "" : stringMEdD = ""

        '--> Initialisation

        lOneM = IsEqual(MEdD, MEdG)
        lOneV = IsEqual(VEdd, VEdG)

        pNColLigne = NCol - 2
        If Not lOneM Then pNColLigne += 1
        If Not lOneV Then pNColLigne += 1

        InitialiseLigneTableau(pNColLigne, HLIGNE)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iNode + 1))

        '# Position et travée

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee) & " / " & CStr(iTravee + 1))

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False) & " / 0")
        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))

        '# Effort tranchant

        stringVEdG = GetStringInUnit(VEdG, Enu_TypeVariable.Effort, 3, 2, False)
        stringVEdD = GetStringInUnit(VEdd, Enu_TypeVariable.Effort, 3, 2, False)

        If iNode = iNodeMinTranchant Or iNode = iNodeMaxTranchant Then
            If VEdG = Vmin Or VEdG = Vmax Then
                stringVEdG = "\G" & stringVEdG & "\g" 'on met le texte en gras
            End If

            If VEdd = Vmin Or VEdd = Vmax Then
                stringVEdD = "\G" & stringVEdD & "\g" 'on met le texte en gras
            End If

        End If

        If lOneV Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEdG)
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEdG)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEdD)
        End If

        '# Moment fléchissant

        If iNode = iNodeMinMoment Or iNode = iNodeMaxMoment Then
            If MEdG = Mmin Or MEdG = Mmax Then
                stringMEdG = "\G" & stringMEdG & "\g" 'on met le texte en gras
            End If

            If MEdD = Mmin Or MEdD = Mmax Then
                stringMEdD = "\G" & stringMEdD & "\g" 'on met le texte en gras
            End If

        End If

        If lOneM Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEdG)
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEdG)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEdD)
        End If


    End Sub

    Private Sub LigneTableauMVCombi(lMultiSpan As Boolean, NCol As Integer, Pos As Integer,
                                    iNode As Integer, iTravee As Integer, xPosG As Decimal, xPosT As Decimal,
                                    VEdG As Decimal, VEdd As Decimal, MEdG As Decimal, MEdD As Decimal,
                                    Mmin As Decimal, Mmax As Decimal, iNodeMinMoment As Integer, iNodeMaxMoment As Integer,
                                      Vmin As Decimal, Vmax As Decimal, iNodeMinTranchant As Integer, iNodeMaxTranchant As Integer)
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
        Dim stringVEdG, stringVEdD, stringMEdG, stringMEdD As String

        '--> Initialisation

        lOneM = IsEqual(MEdD, MEdG)
        lOneV = IsEqual(VEdd, VEdG)

        pNColLigne = NCol - 2
        If Not lOneM Then pNColLigne += 1
        If Not lOneV Then pNColLigne += 1

        InitialiseLigneTableau(pNColLigne, HLIGNE)

        AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iNode + 1))

        '# Position et travée

        If lMultiSpan Then
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, CStr(iTravee))

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosG, Enu_TypeVariable.Longueur, 3, 2, False))
        Else

            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(xPosT, Enu_TypeVariable.Longueur, 3, 2, False))

        End If

        '# Effort tranchant

        stringVEdG = GetStringInUnit(VEdG, Enu_TypeVariable.Effort, 3, 2, False)
        stringVEdD = GetStringInUnit(VEdd, Enu_TypeVariable.Effort, 3, 2, False)

        If iNode = iNodeMinTranchant Or iNode = iNodeMaxTranchant Then
            If VEdG = Vmin Or VEdG = Vmax Then
                stringVEdG = "\G" & stringVEdG & "\g" 'on met le texte en gras
            End If

            If VEdd = Vmin Or VEdd = Vmax Then
                stringVEdD = "\G" & stringVEdD & "\g" 'on met le texte en gras
            End If

        End If

        If lOneV Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEdG)
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEdG)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringVEdD)
        End If

        '# Moment fléchissant

        stringMEdG = GetStringInUnit(MEdG, Enu_TypeVariable.Effort, 3, 2, False)
        stringMEdD = GetStringInUnit(MEdD, Enu_TypeVariable.Effort, 3, 2, False)

        If iNode = iNodeMinMoment Or iNode = iNodeMaxMoment Then
            If MEdG = Mmin Or MEdG = Mmax Then
                stringMEdG = "\G" & stringMEdG & "\g" 'on met le texte en gras
            End If

            If MEdD = Mmin Or MEdD = Mmax Then
                stringMEdD = "\G" & stringMEdD & "\g" 'on met le texte en gras
            End If

        End If

        If lOneM Then
            AddCellule(2 * LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEdG)
        Else
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEdG)
            AddCellule(LC3, Bordures.Tous, PositionTexteInCell.Centre, stringMEdD)
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

        AddLigneNDC("\TABLEAU " & CStr(Pos), False)

        InitialiseLigneTableau(NCol, HLIGNEENTETE)

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

            If (Not IsEqual(myCombi.CoefCombi(iCombi)(jCdc), 0)) And myPoutre.ChargesA(jCdc).lRunCalcul And lAffiche Then

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

        '--> Déclarations

        Dim iTraveeAffichee As Integer = 1
        Dim iCompteur As Integer = 0
        Dim NbLignesMax() As Integer = {25, 30}
        Dim iTab As Integer = 0
        Const NbLignesReq As Integer = 10
        Dim Mmin, Mmax, Vmin, Vmax As Decimal
        Dim iNodeMinMoment As Integer = -1
        Dim iNodeMaxMoment As Integer = -1
        Dim iNodeMinTranchant As Integer = -1
        Dim iNodeMaxTranchant As Integer = -1

        '--> Initialisation

        If nbLignes + NbLignesReq > MAXLIGNEPPAG Then SautePage()
        ChargeA.EnveloppesMoments(Mmax, iNodeMaxMoment, Mmin, iNodeMinMoment) 'obtention des valeurs et noeuds des moments enveloppes 
        ChargeA.EnveloppesTranchants(Vmax, iNodeMaxTranchant, Vmin, iNodeMinTranchant) 'obtention des valeurs et noeuds des moments enveloppes 

        '--> Affichage du cas de charge

        AddTitreNdC(3, ChargeA.Symbol & " : " & ChargeA.Nom)

        If Not ChargeA.lRunCalcul Then
            AddLigneNDC(TABW2 & BlocAnalyse("NOTCALCULATION"))
            Exit Sub
        End If

        '--> Affichage des réactions

        EditionChargeAReactions(MyPoutreLoc, ChargeA)

        '--> Affichage du tableau des sollicitations

        EditionTableauEfforts(MyPoutreLoc, ChargeA.MYY, ChargeA.VZ,
            Mmin, Mmax, iNodeMinMoment, iNodeMaxMoment,
            Vmin, Vmax, iNodeMinTranchant, iNodeMaxTranchant)

    End Sub

    Private Sub EditionChargeAReactions(MyBeam As cls_Poutre, ChargeA As cls_CasDeCharge)
        '-------------------------------------------------------------------------------------------
        '   15/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des réactions dans la poutre après analyse pour un cas de charge
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim symbAppui() As String = {"A", "B", "C"}
        Dim iRApp() As Integer
        Dim lEtais As Boolean = (ChargeA.Symbol = cls_Poutre.symbG1PP)
        Dim strDebutLigne As String = ""

        '--> Initialisation

        ReDim iRApp(MyBeam.Nodes.NbAppuis - 1)

        If lEtais Then
            If MyBeam.lEtaisConsoleGauche Then
                iRApp(0) = 1
            Else
                iRApp(0) = 0
            End If
            If MyBeam.lEtaisConsoleDroite Then
                iRApp(1) = ChargeA.RZ.GetUpperBound(0) - 1
            Else
                iRApp(1) = ChargeA.RZ.GetUpperBound(0)
            End If
        Else
            iRApp = {0, 1}
        End If

        '--> Traitement

        AddLigneNDC(TABW2 & BlocAnalyse("REACTIONS") & TABAFF & "R\-" & symbAppui(0) & "\=" & TABEGAL & GetStringInUnit(ChargeA.RZ(iRApp(0)), Enu_TypeVariable.Effort, 3, 1, True))
        For iNode As Integer = 1 To MyBeam.Nodes.NbAppuis - 1
            AddLigneNDC(TABAFF & "R\-" & symbAppui(iNode) & "\=" & TABEGAL & GetStringInUnit(ChargeA.RZ(iRApp(iNode)), Enu_TypeVariable.Effort, 3, 1, True))
        Next

        If lEtais Then
            If MyBeam.lEtaisConsoleGauche Then
                AddLigneNDC(TABW2 & BlocAnalyse("REACTIONPROPL") & TABAFF & "R\-g\=" & TABEGAL & GetStringInUnit(ChargeA.RZ(0), Enu_TypeVariable.Effort, 3, 1, True))

            End If

            For iEtais As Integer = 0 To MyBeam.NbEtaiement - 1
                If iEtais = 0 Then
                    strDebutLigne = TABW2 & BlocAnalyse("REACTIONSPROPS")
                Else
                    strDebutLigne = ""
                End If
                AddLigneNDC(strDebutLigne & TABAFF & "R\-p" & CStr(iEtais + 1) & "\=" & TABEGAL & GetStringInUnit(ChargeA.RZ(iEtais + iRApp(0) + 1), Enu_TypeVariable.Effort, 3, 1, True))
            Next

            If MyBeam.lEtaisConsoleGauche Then
                AddLigneNDC(TABW2 & BlocAnalyse("REACTIONPROPR") & TABAFF & "R\-d\=" & TABEGAL & GetStringInUnit(ChargeA.RZ(ChargeA.RZ.GetUpperBound(0)), Enu_TypeVariable.Effort, 3, 1, True))

            End If

        End If

        SauteLigne()

    End Sub


    'Private Function IndiceTravee(Node As Integer, iNodeAppui As Integer()) As Integer()
    '    '-------------------------------------------------------------------------------------------
    '    '   10/11/23 :  Création - GUD
    '    '-------------------------------------------------------------------------------------------
    '    '   Permet de renvoyer l'indice de la travée à laquelle appartient le noeud 
    '    '   Dans le cas où le noeud appartient à deux travées, l'indice de la travée renvoyée est celle de gauche (sauf pour le tout premier noeud)
    '    '-------------------------------------------------------------------------------------------

    '    Dim indTravee(1) As Integer

    '    For j As Integer = 0 To iNodeAppui.Count - 1 'On ne commence pas à l'indice 0 exprès car l'indice de la travée du premier noeud est 1
    '        If Node <= iNodeAppui(j) Then
    '            indTravee(0) = j + 1
    '            If Node = iNodeAppui(j) And j <> iNodeAppui.Count - 1 Then
    '                indTravee(1) = indTravee(0) + 1
    '            Else
    '                indTravee(1) = indTravee(0)
    '            End If
    '            Return indTravee
    '        End If
    '    Next

    'End Function

#End Region

#Region "***Feraillage transversal***"

    Private Sub EditionFerraillageTransversal(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Détail du calcul des armatures transversales
        '-------------------------------------------------------------------------------------------

        AddTitreNdC(2, BlocELU("CRITERIA_TRANSREBAR"))

        AddLigneNDC(TABW2 & BlocELU("NBTRANSVERSELAYER") & TABAFF & MyBeam.NbTransverseLayer)
        AddLigneNDC(TABW2 & BlocELU("MINTRANSVERSEREINF") & TABAFF & "\Sr\s\-t,min\=" & TABEGAL & GetStringInUnit(MyBeam.rho_t_min, Enu_TypeVariable.SansType, 3, -1, True) & " (UNITE ???)")

        Dim strFailureMode As String = ""
        Dim str_aa, str_bb, str_dd As String
        str_aa = "a-a"
        str_bb = "b-b"
        str_dd = "d-d"

        Dim l_aa, l_bb, l_dd As Boolean 'indique quels mode de ruine on vérifie
        If Not MyBeam.Dalle.lMixte Then
            If MyBeam.NbTransverseLayer = 2 Then 'cas solid slab without prefabricated part (Table 51 du MT)
                strFailureMode = str_aa & ", " & str_bb
                l_aa = True
                l_bb = True
                l_dd = False
            Else 'cas solid slab with prefabricated part (table 51 du MT)
                strFailureMode = str_aa & ", " & str_dd
                l_aa = True
                l_bb = False
                l_dd = True
            End If
        Else
            strFailureMode = str_aa
            l_aa = True
            l_bb = False
            l_dd = False
        End If

        AddLigneNDC(TABW2 & BlocELU("POTENTIALSHEARFAILURE") & TABAFF & strFailureMode)

        SauteLigne()

        If l_aa Then EditionVerificationELUArmaturesTransv(MyBeam, str_aa, 0)
        If l_bb Then EditionVerificationELUArmaturesTransv(MyBeam, str_bb, 1)
        If l_dd Then EditionVerificationELUArmaturesTransv(MyBeam, str_dd, 2)

    End Sub

    ''' <summary>
    ''' Edition du tableau de vérification des armatures transversales
    ''' </summary>
    ''' <param name="MyBeam"> Poutre en cours </param>
    ''' <param name="str_failureArea"> nom du mode de ruine à afficher </param>
    ''' <param name="ind_failureArea"> indice du mode de ruine associé :0a-a = 0, b-b = 1, d-d = 2</param>
    Private Sub EditionVerificationELUArmaturesTransv(MyBeam As cls_Poutre, str_failureArea As String, ind_failureArea As Integer)
        '-------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - GUD
        '-------------------------------------------------------------------------------------------
        '   Tableau des armatures transversales
        '-------------------------------------------------------------------------------------------
        '   MyBeam              [E] :   Poutre en cours 
        '   str_failureArea     [E] :   nom du mode de ruine à afficher
        '   ind_failureArea     [E] :   indice du mode de ruine associé :0a-a = 0, b-b = 1, d-d = 2
        '-------------------------------------------------------------------------------------------

        AddTitreNdC(3, BlocELU("SHEARFAILUREAREA") & " : " & str_failureArea)

        AddLigneNDC("\TABLEAU 10")

        Dim nbColonne = 8

        If nbLignes + 2 * HLIGNE > MAXLIGNEPPAG Then SautePage()

        InitialiseLigneTableau(nbColonne, HLIGNE)
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, BlocG("SPAN"))
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, BlocG("ZONE"))
        AddCelluleFond(LC4, Bordures.Tous, PositionTexteInCell.Centre, "n\-r\=")
        AddCelluleFond(LC2_3, Bordures.Tous, PositionTexteInCell.Centre, "\St\s\-Ed," & str_failureArea & "\= " & "(" & LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes) & ")")
        AddCelluleFond(LC2_3, Bordures.Tous, PositionTexteInCell.Centre, "\Sq\s\-f,min," & str_failureArea & "\= (°)")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "\Sq\s\-f," & str_failureArea & "\= (°)")
        AddCelluleFond(LC2_3, Bordures.Tous, PositionTexteInCell.Centre, "\SG\s\-sf," & str_failureArea & "\=")
        AddCelluleFond(LC2, Bordures.Tous, PositionTexteInCell.Centre, "(A\-sf\=/s\-f\=)\-" & str_failureArea & "\= (cm\+2\=/m)")

        'SauteLigne()

        For i As Integer = MyBeam.IndicePremiereTravee To MyBeam.IndiceDerniereTravee
            For j As Integer = 0 To MyBeam.NombreZones(i) - 1

                If nbLignes + HLIGNE > MAXLIGNEPPAG Then SautePage()

                InitialiseLigneTableau(nbColonne, HLIGNE)
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, CStr(i + 1))
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, CStr(j + 1))
                AddCellule(LC4, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.NombreGoujonsTransv(i, j), Enu_TypeVariable.SansType, 4, 0, False))
                AddCellule(LC2_3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.TauEd(i, j, ind_failureArea), Enu_TypeVariable.Contrainte, 4, 2, False))
                AddCellule(LC2_3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(GetAngleInDegree(MyBeam.Thetaf_min(i, j)), Enu_TypeVariable.SansType, 4, 2, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(GetAngleInDegree(MyBeam.Thetaf(i, j, ind_failureArea)), Enu_TypeVariable.SansType, 4, 2, False))
                AddCellule(LC2_3, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.Gamma_sf(i, j, ind_failureArea), Enu_TypeVariable.SansType, 4, 2, False))
                AddCellule(LC2, Bordures.Tous, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.As_s_transv(i, j, ind_failureArea), Enu_TypeVariable.AireCM2, 4, 2, False))

            Next
        Next

        FinTableau()
    End Sub

#End Region

#Region "***Edition vérifications ELU***"

    Private Sub EditionVerificationsELU(MyBeam As cls_Poutre)
        '-------------------------------------------------------------------------------------------
        '   12/10/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Edition des vérifications ELU (en phase finale pour les poutres mixtes)
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lMixte As Boolean = MyBeam.lMixte
        Dim lEtaiement As Boolean = (MyBeam.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.FullyPropped)

        '--> Initialisation

        SautePage()

        If MyBeam.lMixte Then
            AddTitreNdC(1, BlocELU("ULS_CHECKS_FINAL"))
        Else
            AddTitreNdC(1, BlocELU("ULS_CHECKS"))
        End If

        If Not MyBeam.VerificationsELUDispo(MyBeam.lMixte) Then Exit Sub
        'If (Not MyBeam.lMixte) And MyBeam.lEnrobage Then Exit Sub

        '--> Traitement

        '# Synthèse des critères

        EditionVerificationsELUSummary(MyBeam)

        '# Calcul détaillé des critères sous combinaisons ELU

        EditionVerificationsELUCombi(MyBeam, False)

        '# Poutres mixtes : ferraillage transversal

        If MyBeam.lMixte Then
            EditionFerraillageTransversal(MyBeam)
        End If

        '--> Phase de construction pour les poutres mixtes

        If MyBeam.lMixte And (Not lEtaiement) Then

            SautePage()
            AddTitreNdC(1, BlocELU("ULS_CHECKS_CONSTRUCTION"))

            '# Synthèse des critères

            EditionVerificationsELUSummary(MyBeam, True)

            '# Calcul détaillé des critères sous combinaisons ELU

            EditionVerificationsELUCombi(MyBeam, True)

        End If


    End Sub

    Private Sub EditionVerificationsELUSummary(MyBeam As cls_Poutre, Optional lConstruction As Boolean = False)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Synthèse des critères ELU
        '-------------------------------------------------------------------------------------------
        '   MyBeam          [E] :   Poutre
        '   lConstruction   [E] :   Indique si phase de construction pour une poutre mixte
        '-------------------------------------------------------------------------------------------

        '--> Titre

        AddTitreNdC(2, BlocELU("CRITERIA_SUM"))

        AddLigneNDC(TABW2 & BlocELU("INFO_S") & "   " & BlocELU("INFO_NS"))
        SauteLigne()

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul
                EditionVerificationsELUSummaryACIER(MyBeam, 0)
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                If lConstruction Then
                    EditionVerificationsELUSummaryACIER(MyBeam, 0)
                Else
                    EditionVerificationsELUSummaryMIXTE(MyBeam, 0)
                End If
        End Select

        Exit Sub

    End Sub

    Private Sub EditionVerificationsELUSummaryACIER(MyBeam As cls_Poutre, iVerif As Integer)

        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Synthèse des critères ELU pour une poutre acier (avec ou sans enrobage)
        '-------------------------------------------------------------------------------------------

        If MyBeam.Param.lElasticDesign Then
            '--> Calcul élastique imposé

            AddLigneNDC(TABW2 & "Calcul élastique imposé")

        Else

            If MyBeam.VerifAcier(iVerif).lCalculPlastic Then
                '--> Calcul Plastique

                AddLigneNDC(TABW2 & "Calcul plastique")
                AddTitreNdC(3, BlocELU("SECTIONSR"))
                'AddLigneNDC(TABW2 & BlocELU("M_CRITERIA") & TABAFF & "\SG\s\-M\=" & TABEGAL & 0)
                AfficheSyntheseCritere(MyBeam.VerifAcier(iVerif).CritereM, "\SG\s\-M\=", BlocELU("M_CRITERIA"))
                AfficheSyntheseCritere(MyBeam.VerifAcier(iVerif).CritereV, "\SG\s\-V\=", BlocELU("V_CRITERIA"))

                AddTitreNdC(3, BlocELU("BEAMR"))

                AfficheSyntheseCritereLT(MyBeam.VerifAcier(iVerif).CritereLTB, "\SG\s\-LT\=", BlocELU("LTB_CRITERIA"))
            Else

            End If
        End If

    End Sub


    Private Sub EditionVerificationsELUSummaryMIXTE(MyBeam As cls_Poutre, iVerif As Integer)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Synthèse des critères ELU pour une poutre mixte (avec ou sans enrobage)
        '-------------------------------------------------------------------------------------------

        If MyBeam.Param.lElasticDesign Then
            '--> Calcul élastique imposé

            AddLigneNDC(TABW2 & BlocELU("ELASTIC_DESIGN_IMPOSED"))

        Else

            If MyBeam.VerifMixte(iVerif).lCalculPlastic Then
                '--> Calcul Plastique

                AddLigneNDC(TABW2 & BlocELU("PLASTIC_DESIGN"))
                'AddLigneNDC(TABW2 & BlocELU("M_CRITERIA") & TABAFF & "\SG\s\-M\=" & TABEGAL & 0)
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereM, "\SG\s\-M\=", BlocELU("M_CRITERIA"))
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereV, "\SG\s\-V\=", BlocELU("V_CRITERIA"))
                If MyBeam.Section.IsInteractionMV(MyBeam.Param.EtaW) Then AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereVb, "\SG\s\-Vb\=", BlocELU("VB_CRITERIA"))
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereMV, "\SG\s\-MV\=", BlocELU("MV_CRITERIA"))

                SauteLigne()

                EditionVerificationsELUSummaryMIXTEDegConnexion(MyBeam, iVerif)

            Else

                '--> Calcul élastique classe 3

                AddLigneNDC(TABW2 & BlocELU("ELASTIC_DESIGN"))

                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereM, "\SG\s\-M\=", BlocELU("M_CRITERIA"))
                AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereV, "\SG\s\-V\=", BlocELU("V_CRITERIA"))
                If MyBeam.Section.IsInteractionMV(MyBeam.Param.EtaW) Then AfficheSyntheseCritere(MyBeam.VerifMixte(iVerif).CritereVb, "\SG\s\-Vb\=", BlocELU("VB_CRITERIA"))

            End If
        End If

    End Sub

    Private Sub EditionVerificationsELUSummaryMIXTEDegConnexion(MyBeam As cls_Poutre, iVerif As Integer)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Synthèse des degrés de connexion pour une poutre mixte (avec ou sans enrobage)
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :   Poutre à traiter
        '   iVerif      [E] :   Indice de la vérification
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTravee As Integer
        Dim iDebTrav, iFinTrav As Integer
        Dim lMulti As Boolean
        Const TVAR1 As String = "\T20"

        '--> Initialisations

        iDebTrav = MyBeam.IndicePremiereTravee
        iFinTrav = MyBeam.IndiceDerniereTravee
        lMulti = (iFinTrav > iDebTrav)

        '--> Traitement

        AddLigneNDC(TABW2 & BlocELU("CONNECTION"))

        '--> Console gauche

        If MyBeam.lTraveeConsoleGauche Then

            AddLigneNDC(TVAR1 & "\U" & BlocG("LEFTCANTILEVER") & "\u")

            AfficheDegreConnexion(MyBeam.VerifMixte(iVerif).DegConnex(0, 1), 1, -1)

        End If

        '--> Travées centrales

        For iTravee = 1 To MyBeam.NombreTraveesDeuxAppuis

            If lMulti Then
                If MyBeam.NombreTraveesDeuxAppuis = 1 Then
                    AddLigneNDC(TVAR1 & "\U" & BlocG("SPAN_MAIN") & " " & CStr(iTravee) & "\u")
                Else
                    AddLigneNDC(TVAR1 & "\U" & BlocG("SPAN_N") & CStr(iTravee) & "\u")
                End If
            End If

            AfficheDegreConnexion(MyBeam.VerifMixte(iVerif).DegConnex(iTravee, 0), MyBeam.VerifMixte(iVerif).DegConnexMin(iTravee), 1)
            If lMulti Then
                AfficheDegreConnexion(MyBeam.VerifMixte(iVerif).DegConnex(iTravee, 1), 1, -1)
            End If
        Next

        '--> Console droite

        If MyBeam.lTraveeConsoleDroite Then

            AddLigneNDC(TVAR1 & "\U" & BlocG("RIGHTCANTILEVER") & "\u")

            AfficheDegreConnexion(MyBeam.VerifMixte(iVerif).DegConnex(iFinTrav, 1), 1, -1)

        End If

    End Sub

    Private Sub AfficheDegreConnexion(Eta As Decimal, EtaMin As Decimal, SigneM As Decimal)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage du degré de connexion d'une travée et de la valeur mini
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :   Poutre à traiter
        '   iVerif      [E] :   Indice de la vérification
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim strGras, strFinGras As String
        Const Symbol As String = "\Sh\s"
        Const SymbolMin As String = "\Sh\s\-min\="
        Dim TABOK As String = "\T85"
        Dim TABInfo As String = "\T70"
        Dim strOK As String
        Dim infoM As String

        '--> Initialisation

        If SigneM > 0 Then infoM = " (M>0)" Else infoM = " (M<0)"

        If IsSmaller(Eta, EtaMin) Then
            strGras = "\G"
            strFinGras = "\g"
            strOK = "NS"
        Else
            strGras = ""
            strFinGras = ""
            strOK = "S"
        End If

        AddLigneNDC(TABW2 & BlocELU("DEGREEOFSHEARCONNEC") & infoM & TABAFF & strGras &
                    Symbol & TABEGAL & GetStringInUnit(Eta, Enu_TypeVariable.SansType, 3, 2, False) &
                    TABOK & strOK & strFinGras)
        AddLigneNDC(TABW2 & BlocELU("MINDEGREE") & TABAFF & strGras &
                    SymbolMin & TABEGAL & GetStringInUnit(EtaMin, Enu_TypeVariable.SansType, 3, 2, False) &
                    strFinGras)

    End Sub


    Private Sub AfficheSyntheseCritere(Critere As cls_Critere, Symbol As String, Titre As String)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage de la synthèse d'un critère
        '-------------------------------------------------------------------------------------------
        '   Critere     [E] :
        '   Symbol      [E] :
        '   Titre       [E] :
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim strGras As String = ""
        Dim strFinGras As String = ""
        Dim TABOK As String = "\T85"
        Dim TABInfo As String = "\T70"
        Dim strOK As String = ""
        Dim Valeur As Decimal = Critere.CritereMax

        '--> Initialisation

        PrepareStyleCritere(Valeur, strGras, strFinGras, strOK)

        '--> Affichage

        AddLigneNDC(TABW3 & Titre & TABAFF & strGras &
                    Symbol & TABEGAL & GetStringInUnit(Valeur, Enu_TypeVariable.SansType, 3, 2, False) &
                    strFinGras & TABInfo & "(N" & CStr(Critere.iNodeM + 1) & "/" & strRacineELU & "_" & CStr(Critere.iCombiM + 1) & ")" & strGras & TABOK & strOK & strFinGras)

    End Sub


    Private Sub AfficheSyntheseCritereLT(Critere As cls_Critere, Symbol As String, Titre As String)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage de la synthèse d'un critère de déversement
        '-------------------------------------------------------------------------------------------
        '   Critere     [E] :
        '   Symbol      [E] :
        '   Titre       [E] :
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim strGras As String = ""
        Dim strFinGras As String = ""
        Dim TABOK As String = "\T85"
        Dim TABInfo As String = "\T70"
        Dim strOK As String = ""
        Dim Valeur As Decimal = Critere.CritereMax

        '--> Initialisation

        PrepareStyleCritere(Valeur, strGras, strFinGras, strOK)

        '--> Affichage

        AddLigneNDC(TABW3 & Titre & TABAFF & strGras &
                    Symbol & TABEGAL & GetStringInUnit(Valeur, Enu_TypeVariable.SansType, 3, 2, False) &
                    strFinGras & TABInfo & "(S" & CStr(Critere.iNodeM + 1) & "/" & strRacineELU & "_" & CStr(Critere.iCombiM + 1) & ")" & strGras & TABOK & strOK & strFinGras)

    End Sub

    Private Sub PrepareStyleCritere(Valeur As Decimal, ByRef strGras As String, ByRef strFinGras As String, ByRef strOK As String)
        '-------------------------------------------------------------------------------------------
        '   18/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage de la synthèse d'un critère
        '-------------------------------------------------------------------------------------------
        '   Valeur      [E] :   Valeur maximale du critere
        '   strGras     [E] :
        '   strFinGras  [E] :   Paramètres pour mixe en forme (en gras si non satisfait)
        '   strOK       [E] :   Conclusion sur le critere
        '-------------------------------------------------------------------------------------------

        If IsGreater(Valeur, 1) Then
            strGras = "\G"
            strFinGras = "\g"
            strOK = ">1   NS"
        Else
            strGras = ""
            strFinGras = ""
            strOK = "<= 1  S"
        End If

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
            If i = iTraveeDeb Then
                AddCellule(LC3, MyBordures(i), PositionTexteInCell.Centre, MyBeam.CombiA_ELU.Symbole(iCombi))
            Else
                AddCellule(LC3, MyBordures(i), PositionTexteInCell.Centre, "")
            End If
            If lMultiSpan Then
                AddCellule(LC3, MyBordures(i), PositionTexteInCell.Centre, CStr(i + 1))
            End If
            If lElastic Then
            Else
                '** Affichage de GammaM
                If lMixte Then
                    'AffichageCritereELU(MyBeam.VerifMixte(iVerif).CritereM, iNodeD, iNodeF, MyBordures(i))
                    AffichageCritereELU_N(MyBeam.VerifMixte(iVerif).CritereM, i, iCombi, MyBordures(i))
                Else
                    'AffichageCritereELU(MyBeam.VerifAcier(iVerif).CritereM, iNodeD, iNodeF, MyBordures(i))
                    AffichageCritereELU_N(MyBeam.VerifAcier(iVerif).CritereM, i, iCombi, MyBordures(i))
                End If
                '** Affichage de GammaV
                If lMixte Then
                    'AffichageCritereELU(MyBeam.VerifMixte(iVerif).CritereV, iNodeD, iNodeF, MyBordures(i))
                    AffichageCritereELU_N(MyBeam.VerifMixte(iVerif).CritereV, i, iCombi, MyBordures(i))
                Else
                    'AffichageCritereELU(MyBeam.VerifAcier(iVerif).CritereV, iNodeD, iNodeF, MyBordures(i))
                    AffichageCritereELU_N(MyBeam.VerifAcier(iVerif).CritereV, i, iCombi, MyBordures(i))
                End If
            End If
        Next

    End Sub

    Private Sub AffichageCritereELU_N(Critere As cls_Critere, iTravee As Integer, iCombi As Integer, vBordure As Integer)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Extraction et affichage de la valeur d'un critère sur une travée
        '-------------------------------------------------------------------------------------------
        '   Critere         [E] :   Critère affiché dans la cellule
        '   iTravee         [E] :   Indice de la travée
        '   iCombi          [E] :   Indice de la combinaison
        '   vBordure        [E] :   Gestion des bordures de la cellule
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim ValCrit As Decimal
        Dim iNodeM As Integer
        Dim lMaxi As Boolean

        Dim StyleG As String = ""
        Dim StyleGFin As String = ""

        '--> Initialisation

        ValCrit = Critere.CritereCombiT(iCombi, iTravee)
        lMaxi = IsEqual(ValCrit, Critere.CritereMax)
        If lMaxi Then
            StyleG = "\G"
            StyleGFin = "\g"
        End If

        iNodeM = Critere.CritereCombiN(iCombi, iTravee) + 1

        '--> Affichage

        AddCellule(LC3, vBordure, PositionTexteInCell.Centre, StyleG & GetStringInUnit(ValCrit, Enu_TypeVariable.SansType, 3, 2, False) & " (N" & CStr(iNodeM) & ")" & StyleGFin)

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

    Private Sub EditionVerificationsELUCombi(MyBeam As cls_Poutre, lConstructionP As Boolean)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage détaillé des critères ELU par combinaison
        '-------------------------------------------------------------------------------------------
        '   MyBeam          [E] :   Poutre
        '   lConstructionP  [E] :   Indique si phase de construction, pour les poutres mixtes
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NCOL As Integer
        Dim iCombi As Integer
        Dim nbMinCombi As Decimal = 5.1 + 1.3 * MyBeam.CombiA_ELU.nbCombi + 1
        Dim nbCombi As Integer
        Dim lMixte As Boolean = MyBeam.lMixte

        '--> Initialisation

        If lConstructionP Then
            nbCombi = MyBeam.CombiA_ELCU.nbCombi
        Else
            nbCombi = MyBeam.CombiA_ELU.nbCombi
        End If

        If nbLignes + nbMinCombi > MAXLIGNEPPAG Then SautePage()

        AddTitreNdC(2, BlocELU("ULS_COMBI_CHECK"))

        '--> Tableau des critères de résistance / combinaison

        EnteteTableauCriteresELU(MyBeam, NCOL)

        For iCombi = 0 To nbCombi - 1
            AffichageTableauCriteresELU(MyBeam, (MyBeam.NbTravees > 1), NCOL, iCombi)
        Next

        FinTableau()

        '--> Combinaison des charges et moment critiques de déversement élastique

        If (Not lMixte) Or lConstructionP Then
            EnteteTableauAlphaCr(MyBeam, NCOL)

            For iCombi = 0 To nbCombi - 1
                AffichageTableauAlphaCr(MyBeam, NCOL, iCombi)
            Next

            FinTableau()
        End If


    End Sub

    Private Sub AffichageTableauAlphaCr(MyBeam As cls_Poutre, ByRef NCOL As Integer, iCombi As Integer)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage dans le tableau des critères ELU des résultats pour une combinaison
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTraveeDeb As Integer = MyBeam.IndicePremiereTravee
        Dim iTraveeFin As Integer = MyBeam.IndiceDerniereTravee
        Dim MyBord As Integer
        Dim i As Integer
        Dim lElastic As Boolean = MyBeam.Param.lElasticDesign
        Dim lMixte As Boolean = MyBeam.lMixte
        Const iVerif As Integer = 0

        '--> Initialisation

        MyBord = Bordures.Tous

        '--> Traitement


        InitialiseLigne(NCOL, HLIGNE, False)
            AddCellule(LC3, MyBord, PositionTexteInCell.Centre, MyBeam.CombiA_ELU.Symbole(iCombi))
        AddCellule(LC3, MyBord, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.VerifAcier(iVerif).AlphaCrLTB(iCombi), Enu_TypeVariable.SansType, 4, 3, False))

        For i = iTraveeDeb To iTraveeFin

            AddCellule(LC3, MyBord, PositionTexteInCell.Centre, GetStringInUnit(MyBeam.VerifAcier(iVerif).McrLTB(iCombi, i), Enu_TypeVariable.Moment, 4, 3, False))

        Next

    End Sub

    Private Sub EnteteTableauAlphaCr(MyBeam As cls_Poutre, ByRef NCOL As Integer)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Entête du tableau des critères ELU par combinaison
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim PostTab As Integer = 20
        Dim lMultiSpan As Boolean = (MyBeam.NbTravees > 1)
        Dim i As Integer
        Dim myBord As Integer

        '--> Initialisation

        NCOL = 2 + MyBeam.NbTravees
        'If lMultiSpan Then NCOL += 1

        AddLigneNDC("\TABLEAU " & CStr(PostTab))

        myBord = Bordures.Tous
        If lMultiSpan Then myBord -= Bordures.Bas

        '--> Entête

        InitialiseLigne(NCOL, HLIGNEENTETE, False)

        AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "Combi")

        'AddCelluleFond(LC3, Bordures.Tous, PositionTexteInCell.Centre, BlocELU("SPAN"))
        AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "\Sa\s\-cr\=")
        AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "M\-cr\=")

        For i = 1 To MyBeam.NbTravees - 1
            AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "M\-cr\=")
        Next

        If lMultiSpan Then
            InitialiseLigne(NCOL, HLIGNEENTETE, False)
            myBord = Bordures.Tous - Bordures.Haut
            AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "")
            AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "")
            For i = 1 To MyBeam.NbTravees
                AddCelluleFond(LC3, myBord, PositionTexteInCell.Centre, "S" & CStr(i))
            Next
        End If

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

        If MyBeam.Hivoss.lHivossMethod Then
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

        Dim lMixte As Boolean = MyBeam.lMixte
        Dim lTableEta As Boolean = lMixte And MyBeam.Param.lFlechesETA

        '--> Initialisation

        If MyBeam.CombiA_ELS.nbCombi = 0 Then Exit Sub

        '--> Tableau Normal

        If lMixte Then AddLigneNDC("\T10" & BlocELS("CASE_FULLINTERACTION"))

        EditionSLSTaleauFlechesCombis(MyBeam, False)

        '--> Tableau des flèches avec prise en compte du glissement

        If lTableEta Then

            AddLigneNDC("\T10" & BlocELS("CASE_PARTIALINTERACTION"))
            EditionSLSTaleauFlechesCombis(MyBeam, True)

        End If

    End Sub

    Private Sub EditionSLSTaleauFlechesCombis(MyBeam As cls_Poutre, lETA As Boolean)
        '-------------------------------------------------------------------------------------------
        '   05/02/24 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage du tableau des flèches par combinaison
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :
        '   lETA        [E] :   Indique si flèche normales ou flèches ETA (prenant en compte le glissement)
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMultispan As Boolean = (MyBeam.NbTravees > 1)
        Dim NCOL As Integer
        Dim LargCol() As Single = Nothing

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

            LigneTableauFlecheCombi(MyBeam, jCombi, lMultispan, NCOL, LargCol, lETA)

            'End If
        Next

        FinTableau()

    End Sub

    Private Sub LigneTableauFlecheCombi(MyBeam As cls_Poutre, iCombi As Integer, lMultiSpan As Boolean, NCOL As Integer, LargCol() As Single, lETA As Boolean)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ligne pour le tableau des flèches par cdc
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :
        '   iCase       [E] :   Indique du cas de charge
        '   lMultiSpan  [E] :   Indique si poutre à plusieurs travées
        '   NCOL        [E] :   Nombre colonnes dans le tableau
        '   LargCol     [E] :   Largeur des colonnes du tableau
        '   lETA        [E] :   Indique si flèche normales ou flcèhes ETA (prenant en compte le glissement)
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

        MyBeam.CombiA_ELS.CombineFleches(iCombi, MyBeam.Nodes.nbNodes, MyBeam.ChargesA, UZCombi, lCombiRetrait, lETA)

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

        '--( Déclarations

        Dim lMixte As Boolean = MyBeam.lMixte
        Dim lTableEta As Boolean = lMixte And MyBeam.Param.lFlechesETA

        '--( Tableau normal

        If lMixte Then AddLigneNDC("\T10" & BlocELS("CASE_FULLINTERACTION"))

        EditionSLSTaleauFlechesCharges(MyBeam, False)

        '--( Tableau en prenant en compte le glissement

        If lTableEta Then
            AddLigneNDC("\T10" & BlocELS("CASE_PARTIALINTERACTION"))
            EditionSLSTaleauFlechesCharges(MyBeam, True)
        End If


    End Sub

    Private Sub EditionSLSTaleauFlechesCharges(MyBeam As cls_Poutre, lETA As Boolean)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Affichage du tableau des flèches par cdc
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :
        '   lETA        [E] :   Indique si flèche normales ou flèches ETA (prenant en compte le glissement)
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

                LigneTableauFlecheCdc(MyBeam, jCdc, lMultispan, NCOL, LargCol, lETA)

            End If
        Next

        FinTableau()

    End Sub

    Private Sub LigneTableauFlecheCdc(MyBeam As cls_Poutre, iCase As Integer, lMultiSpan As Boolean, NCOL As Integer, LargCol() As Single, Optional lETA As Boolean = False)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ligne pour le tableau des flèches par cdc
        '-------------------------------------------------------------------------------------------
        '   MyBeam      [E] :
        '   iCase       [E] :   Indique du cas de charge
        '   lMultiSpan  [E] :   Indique si poutre à plusieurs travées
        '   NCOL        [E] :   Nombre colonnes dans le tableau
        '   LargCol     [E] :   Largeur des colonnes du tableau
        '   lETA        [E] :   Indique si flèche normales ou flcèhes ETA (prenant en compte le glissement)
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyBordures(MyBeam.IndiceDerniereTravee) As Integer
        Dim iTraveeDeb As Integer = MyBeam.IndicePremiereTravee
        Dim iTraveeFin As Integer = MyBeam.IndiceDerniereTravee
        Dim FlechesMax() As Decimal = Nothing
        Dim iCell As Integer
        Dim RatioX As Decimal
        Dim ChaineRatioX As String
        Dim plETA As Boolean = lETA And (Not IsNothing(MyBeam.ChargesA(iCase).UZEta))

        '--> Initialisations

        For i As Integer = iTraveeDeb To iTraveeFin
            MyBordures(i) = Bordures.Gauche + Bordures.Droite
        Next
        MyBordures(iTraveeDeb) += Bordures.Haut
        MyBordures(iTraveeFin) += Bordures.Bas

        If plETA Then
            ExtraireFlecheEnveloppes(MyBeam.ChargesA(iCase).UZEta, iTraveeDeb, iTraveeFin, MyBeam.Nodes.iNodeExtTrav, FlechesMax)
        Else
            ExtraireFlecheEnveloppes(MyBeam.ChargesA(iCase).UZ, iTraveeDeb, iTraveeFin, MyBeam.Nodes.iNodeExtTrav, FlechesMax)
        End If

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


    'Private Structure strHivossTable

    '    Public nbLigne As Integer
    '    Public listeLigneMasse As Ligne()
    '    Public listeFrequence As Decimal()

    'End Structure

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

        Const pTABVAR As String = "\T45"
        Const kPC As Decimal = 100
        Dim AllFloorVibration As New Dictionary(Of Integer, cls_MethodHivoss.strHivossTable)
        'Const TABVAR As String = " :\T45"
        Const DFORMAT As String = "0"
        Dim Frequency, ModalMass As Decimal
        Dim HResult As String = ""
        Dim HVal As Decimal
        'Dim Reactions() As Decimal
        Dim IndConfort As Integer
        Dim TableConfort(2) As String
        Dim PorteeDalle As Decimal
        Dim MasseProfil As Decimal

        'Dim lDefini() As Boolean
        Dim lMixte As Boolean = MyBeam.lMixte
        Dim FreqDalle, FreqBeam As Decimal
        Dim MySymb As String = ""
        Dim Chaine As String = ""

        '--[ Titre

        SautePage()
        AddTitreNdC(2, BlocHiVoss("HIVOSSTITLE"))

        '--[ Initialisations

        lMixte = MyBeam.lMixte

        TableConfort(0) = BlocHiVoss("CRECOMMENDED")
        TableConfort(1) = BlocHiVoss("CCRITICAL")
        TableConfort(2) = BlocHiVoss("CNOTRECOMMENDED")

        MyBeam.Hivoss.CalculAmortissement()

        MyBeam.Hivoss.ChargerValeursHivoss(AllFloorVibration)

        MyBeam.Modal.Analyse(MyBeam, MyBeam.Hivoss.ratioQ, MyBeam.Hivoss.IndexQ)
        Frequency = MyBeam.Modal.Frequence

        ModalMass = MyBeam.Modal.MassTotal / 2              ' A MODIFIER ? pour les multispan

        ''--[ Prise en compte de la fréquence propre de dalle pour les poutres mixtes:

        If MyBeam.Hivoss.lFreqDalle And LogicielOptions.lExpert Then
            MasseProfil = MyBeam.Section.ProfilA.Aire * cls_Acier.RHOACIER
            PorteeDalle = MyBeam.PorteeDalle

            MyBeam.Dalle.FrequenceDalle(MyBeam.LongueurTravee(1), PorteeDalle, MyBeam.LargeurInfluence, MasseProfil, MyBeam.Param.GraviteG)
            FreqBeam = Frequency
            Frequency = CDec(1 / Math.Sqrt(1 / FreqBeam ^ 2 + 1 / FreqDalle ^ 2))
        End If

        ''--[ Affichages des données

        Select Case MyBeam.Hivoss.UtilisationPlancher
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Bureau : Chaine = BlocHiVoss("UOFFICE")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Education : Chaine = BlocHiVoss("USCHOOL")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Hotel : Chaine = BlocHiVoss("UHOTEL")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Industriel : Chaine = BlocHiVoss("UINDUSTRIAL")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.MaisonRetraite : Chaine = BlocHiVoss("USENIOR")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Residentiel : Chaine = BlocHiVoss("URESIDENTIAL")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Reunion : Chaine = BlocHiVoss("UMEETING")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Sante : Chaine = BlocHiVoss("UHOSPITAL")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.Sports : Chaine = BlocHiVoss("USPORTS")
            Case cls_MethodHivoss.Enu_UtilisationPlancher.ZoneSensible : Chaine = BlocHiVoss("UCRITICAL")
        End Select
        AddLigneNDC(TABW2 & BlocHiVoss("USAGE") & pTABVAR & Chaine)
        SauteLigne()
        AddLigneNDC(TABW2 & BlocHiVoss("DSTRUC") & pTABVAR & "D1 = " & Format(MyBeam.Hivoss.AmortiStructure_D1 * kPC, DFORMAT) & " %")
        AddLigneNDC(TABW2 & BlocHiVoss("DFURNITURE") & pTABVAR & "D2 = " & Format(MyBeam.Hivoss.AmortiMobilier_D2 * kPC, DFORMAT) & " %")
        AddLigneNDC(TABW2 & BlocHiVoss("DFINISHING") & pTABVAR & "D3 = " & Format(MyBeam.Hivoss.AmortiFinition_D3 * kPC, DFORMAT) & " %")
        AddLigneNDC(TABW2 & BlocHiVoss("DTOTAL") & pTABVAR & "D = " & Format(MyBeam.Hivoss.AmortiTotal_Dtot * kPC, DFORMAT) & " %")

        SauteLigne()

        AddLigneNDC(TABW2 & BlocHiVoss("COMBIMASS") & pTABVAR & "G + " & GetStringInUnit(MyBeam.Hivoss.ratioQ, Enu_TypeVariable.SansType, 3, 1, False) & " Q" & Format(MyBeam.Hivoss.IndexQ, "0"))

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

        '--[ Affichage des fréquences propres et de la masse modale

        SauteLigne()
        If MyBeam.Hivoss.lFreqDalle And LogicielOptions.lExpert Then
            AddLigneNDC(TABW2 & BlocHiVoss("EIGENFB") & pTABVAR & GetStringInUnit(FreqBeam, Enu_TypeVariable.Frequence, 3, 1, True))
            AddLigneNDC(TABW2 & BlocHiVoss("EIGENFS") & pTABVAR & GetStringInUnit(FreqDalle, Enu_TypeVariable.Frequence, 3, 1, True))
            AddLigneNDC(TABW2 & BlocHiVoss("EIGENFC") & pTABVAR & GetStringInUnit(Frequency, Enu_TypeVariable.Frequence, 3, 1, True))
        Else
            AddLigneNDC(TABW2 & BlocHiVoss("EIGENF") & pTABVAR & GetStringInUnit(Frequency, Enu_TypeVariable.Frequence, 3, 1, True))
        End If
        SauteLigne()
        AddLigneNDC(TABW2 & BlocHiVoss("MODALMASS") & pTABVAR & GetStringInUnit(ModalMass, Enu_TypeVariable.SansType, 3, 0, False) & " kg")

        ''--[ Calcul Hivoss

        MyBeam.Hivoss.CalculMethodHivoss(CInt(MyBeam.Hivoss.AmortiTotal_Dtot * kPC), Frequency, ModalMass, HResult, HVal)
        IndConfort = MyBeam.Hivoss.ConfortAssessment(HResult)

        SauteLigne()
        If HResult = "A" Then
            MySymb = "<"
        ElseIf HResult = "!" Then
            MySymb = ">"
        Else
            MySymb = "="
        End If

        AddLigneNDC(TABW2 & BlocHiVoss("OSRMS") & pTABVAR & "OS-RMS\-90\= " & MySymb & " " & GetStringInUnit(HVal, Enu_TypeVariable.SansType, 3, 1, False) & " m/s")
        AddLigneNDC(TABW2 & BlocHiVoss("CPERCEPTION") & pTABVAR & HResult)
        AddLigneNDC(TABW2 & BlocHiVoss("COMFORTASS") & pTABVAR & TableConfort(IndConfort))

        If OptionsNdC.lShowHivossCurve Then
            SautePage()

            MyNote.AddLigneInRapport("\IMG HIVOSS 5 85 80 NoCadre " _
                                   & Format(MyBeam.Hivoss.AmortiTotal_Dtot * kPC, DFORMAT) & " " _
                                   & Format(Frequency, "0.00") & " " _
                                   & Format(ModalMass, "0.00") & " " _
                                   & BlocHiVoss("DAMPING"))
        End If



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


        AddTitreDoc(0, BlocG("TITLE").ToUpper)
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

    Private Sub InitialiseLigneTableau(ByVal NombreCellules As Integer, ByVal hLigne As Single)

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

                Model_LigneTableauElement(lMultispan, NCol, PosTab, i, iTravee, Ai, Iyi)

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

#Region "   Edition Note Catalogue "

    Sub AAA_EditionCATALOGUE(ByVal lEdite As Boolean, MyProfilA_loc As cls_ProfilA)
        '----------------------------------------------------------------------------------------------
        '
        '   30/11/23 :  Création - GUD
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

        '--[ Initialisation des fichiers langues


        Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_MAIN")
        BlocLine.CreationBloc(BlocG)

        BlocLine = New Cls_LinesOfFile(LogicielFichiers.LangueNDC, "#NDC_SECTIONPROP")
        BlocLine.CreationBloc(BlocSP)

        '--[ Initialisations

        MyNote = New Cls_Rapport("Arial", 1.5, 3, 3)

        '--[ Création de la Note

        lChapitreOutOfScope = False

        '--[ Initialisation

        MyNote.Clear()

        'Initialiser les indices
        For I = 0 To 2
            MyNote.IndTitre(I) = 0
        Next

        If MyProjet.Entreprise.Trim = "" Then
            MyNote.EntetePrincipal = MyProjet.Utilisateur
        Else
            MyNote.EntetePrincipal = MyProjet.Entreprise & " - " & MyProjet.Utilisateur
        End If
        MyNote.EnteteSecond = MyProjet.Nom

        MyNote.EtiquetteLigne(0, 0) = BlocG("USER")
        MyNote.EtiquetteLigne(1, 0) = BlocG("SOCIETE")
        MyNote.EtiquetteLigne(2, 0) = BlocG("PROJET")

        Const DPTS As String = ":  "
        MyNote.EtiquetteLigne(0, 1) = DPTS & MyProjet.Utilisateur
        MyNote.EtiquetteLigne(1, 1) = DPTS & MyProjet.Entreprise
        MyNote.EtiquetteLigne(2, 1) = DPTS & MyProjet.Nom

        MyNote.FootNote = BlocG("FOOTNOTE")

        '--|=========================================
        '--| PAGE DE GARDE
        '--|=========================================

        EditionPageDeGarde(MyProjet.Nom)

        '--|=========================================
        '--| PARAMETRES
        '--|=========================================


        SautePage()

        AddTitreNdC(1, BlocG("SPROFILE"))

        AddLigneNDC(TABW2 & BlocG("HS_PROFILE") & TABAFF & "h\-s\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.ha, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("BF_PROFILE") & TABAFF & "b\-f\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.Bfs, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("TF_PROFILE") & TABAFF & "t\-f\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.Tfs, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("HW_PROFILE") & TABAFF & "h\-w\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.HauteurAmeHw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("DW_PROFILE") & TABAFF & "d\-w\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.HauteurAmeDw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("TW_PROFILE") & TABAFF & "t\-w\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.Tw, Enu_TypeVariable.Dimension, 3, -1, True))
        AddLigneNDC(TABW2 & BlocG("RC_PROFILE") & TABAFF & "r" & TABEGAL & GetStringInUnit(MyProfilA_loc.Rcs, Enu_TypeVariable.Dimension, 2, -1, True))

        SauteLigne()

        AddLigneNDC(TABW2 & BlocG("A_PROFILE") & TABAFF & "A" & TABEGAL & GetStringInUnit(MyProfilA_loc.Aire, Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("AV_PROFILE") & TABAFF & "A\-v\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.AireAv, Enu_TypeVariable.AireCM2, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("IY_PROFILE") & TABAFF & "I\-y\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.InertieY, Enu_TypeVariable.InertieCM4, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("IZ_PROFILE") & TABAFF & "I\-z\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.InertieZ, Enu_TypeVariable.InertieCM4, 4, 0, True))
        AddLigneNDC(TABW2 & BlocG("WEL_Y_PROFILE") & TABAFF & "W\-el,y\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.ModuleWelY, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("WEL_Z_PROFILE") & TABAFF & "W\-el,z\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.ModuleWelZ, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("WPL_Y_PROFILE") & TABAFF & "W\-pl\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.ModuleWplY, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("WPL_Z_PROFILE") & TABAFF & "W\-pl\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.ModuleWplz, Enu_TypeVariable.ModuleCM3, 4, 1, True))
        AddLigneNDC(TABW2 & BlocG("IT_PROFILE") & TABAFF & "I\-t\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.InertieT, Enu_TypeVariable.InertieCM4, 4, 2, True))
        AddLigneNDC(TABW2 & BlocG("IW_PROFILE") & TABAFF & "I\-w\=" & TABEGAL & GetStringInUnit(MyProfilA_loc.InertieW, Enu_TypeVariable.InertieWCM6, 4, 0, True))

        AddLigneNDC("\IMG PROFIL_ACIER_DATABASE 10 80 30 NoCadre " & MyProfilA_loc.NomProfile)

        '--[ Edition de la Note dans l'Editeur

        If lEdite Then
            '--> Ouverture de la fenêtre
            Frm_NoteCalcul.ShowDialog()
            Frm_NoteCalcul.Dispose()
            '--> Liberation de la note
            MyNote.Dispose()
        End If


    End Sub



#End Region


End Module
