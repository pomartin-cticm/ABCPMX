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

    Private Const TABAFF As String = " :\T45"
    Private Const TABAFF2 As String = " \T45"

    '--> Taille tableau
    Private Const LC1 As Decimal = 30        'taille colonne 1
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

        Select Case MyBeam.TypeSection
            Case cls_Section.Enum_TypeSection.Acier
                AddLigneNDC(TABW2 & "\G" & Bloc("GSTEELB") & "\g")

        End Select


        AddLigneNDC("\IMG PORTEE 10 80 30 NoCadre")

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

    End Sub

    Private Sub EditionParametresSlab(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des paramètres de base d'une section
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("SLAB"))

    End Sub

    Private Sub EditionParametresMaintiens(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des maintiens latéraux d'une poutre
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("LATERALR"))

    End Sub

    Private Sub EditionParametresCoefGamma(ByVal MyBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------
        '   10/07/23 :  Création - Version 1.00 - POM
        '----------------------------------------------------------------------------------------------
        '   Edition des coefficients partiels
        '----------------------------------------------------------------------------------------------

        SautePage()

        AddTitreNdC(2, Bloc("GAMMA"))

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
