Imports PMXMoteur2
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip

Public Class Frm_ConnectionSlimFloor

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const PrefixeG As String = "M "

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_TypeConnection
    ''' </summary>
    Dim strTypeConnection As String()

    ''' <summary>
    ''' Stockage des strings qui seront dans le combobox strTypeConnection
    ''' </summary>
    Dim strGoujonSemelle, strGoujonAme, strArmatureAme As String

    ''' <summary>
    ''' Indice de la base de donnée du goujon affiché
    ''' </summary>
    Dim ind_database As Integer

    ''' <summary>
    ''' Importe la liste de noms des goujons disponibles
    ''' </summary>
    Dim tabLabelGoujons() As String

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_studs
    ''' </summary>
    Dim tabLabelGoujonsAff() As String

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_Acier
    ''' </summary>
    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

    ''' <summary>
    ''' variable locale qui informe quelle travée est affichée à l'écran
    ''' 1er item: donne la nature de la travée
    ''' 2nd item: donne l'indice de la travée selectionnée
    ''' </summary>
    Dim traveeEnCours As Integer = 1

    ''' <summary>
    ''' Permet de stocker localement le mot clé associé aux goujons 
    ''' </summary>
    Dim strStud As String

    ''' <summary>
    ''' Permet de stocker localement le mot clé associé aux aramtures 
    ''' </summary>
    Dim strReinf As String

    ''' <summary>
    ''' Permet de stocker localement le message à afficher lorque la valeur dépasse celle conseillée (message non bloquant)
    ''' </summary>
    Dim strValMaxConseillee As String

    ''' <summary>
    ''' Permet de stocker localement le mot clé associé aux ondes
    ''' </summary>
    Dim strRib As String
    Dim strRibs As String

    ''' <summary>
    ''' Permet de savoir si on est au sein d'un des txtbox_largeur
    ''' </summary>
    Dim ltxt_Largeur_I1Enter As Boolean

    ''' <summary>
    ''' Permet de bloquer les évènements quand on mets à jour les valeurs dans les txtbox
    ''' </summary>
    Dim lMAJAffichage As Boolean

    'Définition des valeurs limites pour les caractéristiques des goujons
    Dim Hauteur_Goujon_MIN As Decimal
    Dim Hauteur_Goujon_MAX_CONSEILLEE As Decimal 'valeur conseillée à ne pas dépasser 
    Dim Hauteur_Goujon_MAX As Decimal 'valeur à ne pas dépasser dans tous les cas 
    Dim Diametre_Goujon_MIN As Decimal
    Dim Diametre_Goujon_MAX As Decimal
    Dim Diametre_Arma_MIN As Decimal
    Dim Diametre_Arma_MAX As Decimal

    'Définition des valeurs limites pour les caractéristiques longitudinales
    Dim Espacement_Longi_MIN As Decimal 'sxi,min dans les ST
    Dim Espacement_Longi_MAX As Decimal 'sxi,max dans les ST

    'Définition des valeurs limites pour les caractéristiques transversales
    Dim Espacement_Trans_MIN As Decimal
    Dim Pince_Trans_MIN As Decimal 'Correspond à eD,min dans les Specifications Techniques 
    Dim b_app_min As Decimal
    Dim Nb_TransV_Row_MIN As Integer
    Dim Nb_TransV_Row_MAX As Integer

    Private ReadOnly Property lGoujonsSoudes As Boolean
        Get
            lGoujonsSoudes = MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup Or MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
        End Get
    End Property

    '== POM
    Dim tabDiam() As Decimal
    Dim tabHsc() As Decimal

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Connection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True

        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        RemplirComboBox()
        AfficherPoutreEnCours()
        MAJ_TLPanGauche()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_connecteurs()
        MAJ_affichage_txt_cmb_connection()

        lBuild = False
    End Sub

    ''' <summary>
    ''' Initialise les valeurs des variables locales 
    ''' </summary>
    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        traveeEnCours = 1 'Dans le cas des Slimfloors, il n'y a qu'une travée

        MAJ_Valeurs_Limites()

        'Corrige les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)
        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For j As Integer = 0 To 2
                If Not (MyPoutreLoc.NombreGoujonsTransv(i, j) >= Nb_TransV_Row_MIN And MyPoutreLoc.NombreGoujonsTransv(i, j) <= Nb_TransV_Row_MAX) Then
                    MyPoutreLoc.NombreGoujonsTransv(i, j) = Nb_TransV_Row_MIN
                End If
            Next
        Next

        ltxt_Largeur_I1Enter = False
        lMAJAffichage = False

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CONNECTION")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")


                '=== MENU CONNECTEUR ==============================================================='

                Me.lbl_Connecteurs.Text = Bloc("CONNECTORS")

                strGoujonSemelle = Bloc("FLANGE_STUD")
                strGoujonAme = Bloc("WEB_STUD")
                strArmatureAme = Bloc("WEB_REINF")

                ReDim strTypeConnection(2)
                strTypeConnection(0) = strGoujonSemelle
                strTypeConnection(1) = strGoujonAme
                strTypeConnection(2) = strArmatureAme

                strValMaxConseillee = Bloc("RECMAXVALUE")

                '--> Initialisation table des variables goujons

                Dim nbStuds As Integer = BaseGoujons.Count
                ReDim tabLabelGoujons(nbStuds - 1)
                ReDim tabLabelGoujonsAff(nbStuds - 1)

                For iStud As Integer = 0 To nbStuds - 1
                    tabLabelGoujons(iStud) = BaseGoujons(iStud).nom
                    tabLabelGoujonsAff(iStud) = PrefixeG & BaseGoujons(iStud).nom
                Next

                Me.lbl_ClasseA.Text = Bloc("CLASS")

                '=== MENU CONNECTION ==============================================================='

                Me.lbl_Connection.Text = Bloc("CONNECTION")

                Me.lbl_NbRows.Text = Bloc("ROW_NUMBER")

                Me.lbl_EspacementLongi.Text = Bloc("DISPOSITION_LON_NO_BAC") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")"

                strStud = Bloc("STUDS")
                strReinf = Bloc("REINFORCEMENTS")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub GestionUnites()

        '--> Etiquettes unités partie goujons soudés
        Me.etq_UnitD.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitHsc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFy.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitFu.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)


        '--> Etiquettes unités partie armatures 
        Me.etq_UnitPhiS.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFsk.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub RemplirComboBox()
        Dim index As Integer = 0

        Dim lCentral As Boolean = (MyPoutreLoc.NombreTraveesDeuxAppuis = 1)

        For i As Integer = 1 To MyPoutreLoc.NombreTraveesDeuxAppuis
            index += 1
        Next

        Me.cmb_TypeConnection.Items.Clear()
        Me.cmb_TypeConnection.Items.AddRange(strTypeConnection)
        Me.cmb_TypeConnection.SelectedIndex = 0

        Me.cmb_goujons.Items.Clear()
        Me.cmb_goujons.Items.AddRange(tabLabelGoujonsAff)
        Me.cmb_goujons.SelectedIndex = 0

        Me.cmb_Acier.Items.Clear()
        Me.cmb_Acier.Items.AddRange(ClasseAcierArma)
        Me.cmb_Acier.SelectedIndex = 0

        Me.cmb_NbRow_I1.Items.Clear()

        For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
            Me.cmb_NbRow_I1.Items.Add(i)
        Next

        Me.cmb_NbRow_I1.SelectedIndex = 0

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Connecteurs.BackColor = CouleurBackBandeaux
        Me.lbl_Connecteurs.ForeColor = CouleurForeBandeaux

        Me.lbl_Connection.BackColor = CouleurBackBandeaux
        Me.lbl_Connection.ForeColor = CouleurForeBandeaux

        Me.txt_hsc.ReadOnly = True
        Me.txt_d.ReadOnly = True
        Me.txt_fy.ReadOnly = True
        Me.txt_fu.ReadOnly = True
        Me.txt_hsc.BackColor = CouleurReadOnly
        Me.txt_d.BackColor = CouleurReadOnly
        Me.txt_fy.BackColor = CouleurReadOnly
        Me.txt_fu.BackColor = CouleurReadOnly

    End Sub

    ''' <summary>
    ''' Renseigne les valeurs dans les txtboxs et les comboboxs
    ''' </summary>
    Private Sub AfficherPoutreEnCours()

        'Type connecteur
        Select Case MyPoutreLoc.Dalle.typeConnecteur
            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                cmb_TypeConnection.SelectedItem = strGoujonSemelle
            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
                cmb_TypeConnection.SelectedItem = strGoujonAme
            Case cls_Dalle.Enum_TypeConnecteur.ArmatureAme
                cmb_TypeConnection.SelectedItem = strArmatureAme
        End Select

        Me.txt_PhiS.Text = GetStringInUnit(MyPoutreLoc.Dalle.ConnecteurArmature.ds, Enu_TypeVariable.Dimension, 3, 0, False)

        'Classe de l'acier
        If Me.ClasseAcierArma.Contains(MyPoutreLoc.Dalle.ConnecteurArmature.Acier.Classe) Then
            Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, MyPoutreLoc.Dalle.ConnecteurArmature.Acier.Classe)
        Else
            Me.cmb_Acier.SelectedIndex = 0
        End If

        MAJI_ProprietesAcier()

        'Gestion des valeurs de la poutre en cours
        With MyPoutreLoc
            Me.cmb_NbRow_I1.SelectedIndex = .NombreGoujonsTransv(traveeEnCours, 0) - 1
            Me.txt_EspLongi_I1.Text = GetStringInUnit(.EspacementZone(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)

        End With
    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Vide la fenêtre des erreurs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        ErrorProvider_Frm_Connection.Clear()
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Dim lFrm_Valide As Boolean = True

        Dim list_txtbox As New List(Of TextBox)


        If lGoujonsSoudes Then
            list_txtbox.Add(Me.txt_hsc)
            list_txtbox.Add(Me.txt_d)
            list_txtbox.Add(Me.txt_EspLongi_I1)
        Else
            list_txtbox.Add(Me.txt_PhiS)
        End If

        Dim ValeurUI As Decimal

        For Each txtbox_loc As TextBox In list_txtbox
            VerificationSaisie(txtbox_loc, ValeurUI, True)
            If Not ErrorProvider_Frm_Connection.GetError(txtbox_loc) = String.Empty Then
                lFrm_Valide = False
                Exit For
            End If
        Next

        Return lFrm_Valide
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        With MyProjet.Poutres(MyProjet.IndEnCours)

            lModif = False

            If MyPoutreLoc.Dalle.typeConnecteur <> .Dalle.typeConnecteur Then
                .Dalle.typeConnecteur = MyPoutreLoc.Dalle.typeConnecteur
                lModif = True
            End If

            If lGoujonsSoudes Then
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.nom, .Dalle.ConnecteurGoujonSoude.nom, lModif)
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.hsc, .Dalle.ConnecteurGoujonSoude.hsc, lModif)
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d, .Dalle.ConnecteurGoujonSoude.d, lModif)
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fy, .Dalle.ConnecteurGoujonSoude.Fy, lModif)
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fu, .Dalle.ConnecteurGoujonSoude.Fu, lModif)
            Else
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurArmature.ds, .Dalle.ConnecteurArmature.ds, lModif)
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurArmature.Acier.Classe, .Dalle.ConnecteurArmature.Acier.Classe, lModif)
                GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurArmature.Acier.FsK, .Dalle.ConnecteurArmature.Acier.FsK, lModif)
            End If

            GereTransfertValeur(MyPoutreLoc.NombreGoujonsTransv(1, 0), .NombreGoujonsTransv(1, 0), lModif)
            GereTransfertValeur(MyPoutreLoc.EspacementZone(1, 0), .EspacementZone(1, 0), lModif)

        End With

    End Sub


#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_d.Paint, img_hsc.Paint, img_fy.Paint, img_fu.Paint, img_PhiS.Paint, img_Fsk.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lIndice = True
        lGrec = False
        lEgal = True

        Select Case sender.name
            Case Me.img_hsc.Name

                strSymbol = "h"
                strIndice = "sc"

            Case Me.img_d.Name

                strSymbol = "d"
                strIndice = "  "


            Case Me.img_fy.Name

                strSymbol = "f"
                strIndice = "y "

            Case Me.img_fu.Name

                strSymbol = "f"
                strIndice = "u "

            Case Me.img_PhiS.Name

                strSymbol = "f"
                strIndice = "s"

                lGrec = True

            Case Me.img_Fsk.Name

                strSymbol = "f"
                strIndice = "sk"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

    Private Sub DessinConnecteurs(sender As Object, e As PaintEventArgs) Handles img_Stud.Paint
        DessineDalleConnectionSlimfloor(e.Graphics, Me.img_Stud.ClientRectangle.Width, Me.img_Stud.ClientRectangle.Height,
            MyPoutreLoc, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire)
    End Sub


#End Region

#Region " Evènements "

    ''' <summary>
    ''' Met à jour les valeurs limites en fonction des données renseignées
    ''' </summary>
    Private Sub MAJ_Valeurs_Limites()
        'Valeurs en mètres

        'Définition des valeurs limites pour les caractéristiques des goujons
        Hauteur_Goujon_MIN = 3 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d
        Hauteur_Goujon_MAX_CONSEILLEE = Math.Max(0, MyPoutreLoc.Dalle.Ep_td - MyPoutreLoc.Section.ProfilA.zRefAraseSup - 20 / 1000)
        Hauteur_Goujon_MAX = Math.Max(0, MyPoutreLoc.Dalle.Ep_td - MyPoutreLoc.Section.ProfilA.zRefAraseSup)

        Diametre_Goujon_MIN = 16 / 1000 'Valeur arbitraire (16 mm), je me suis basé sur la clause 6.6.1.2(1) de l'EC4 actuel
        Diametre_Goujon_MAX = 0
        Diametre_Goujon_MAX = 25 / 1000 'Valeur arbitraire (25 mm), je me suis basé sur la clause 6.6.1.2(1) de l'EC4 actuel

        Diametre_Arma_MIN = 25 / 1000
        Diametre_Arma_MAX = 40 / 1000

        If lGoujonsSoudes Then
            Espacement_Longi_MIN = 5 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d
            Espacement_Longi_MAX = Math.Min(800 / 1000, 6 * MyPoutreLoc.Dalle.Ep_td)
        Else
            Espacement_Longi_MIN = 0
            Espacement_Longi_MAX = 125 / 1000
        End If

        'Définition des valeurs limites pour les caractéristiques transversales

        Pince_Trans_MIN = 20 / 1000
        Espacement_Trans_MIN = 2.5 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d

        Nb_TransV_Row_MIN = 1
        Nb_TransV_Row_MAX = Math.Floor((MyPoutreLoc.Section.ProfilA.Bfs - 2 * Pince_Trans_MIN - MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d) / Espacement_Trans_MIN + 1)
        Nb_TransV_Row_MAX = Math.Max(Nb_TransV_Row_MAX, Nb_TransV_Row_MIN)


    End Sub

    ''' <summary>
    ''' MAJ des textboxs et comboboxs dans la zone des connecteurs
    ''' </summary>
    Private Sub MAJ_affichage_txt_connecteurs()
        Me.cmb_goujons.SelectedIndex = Array.IndexOf(tabLabelGoujons, MyPoutreLoc.Dalle.ConnecteurGoujonSoude.nom)
        Me.txt_hsc.Text = GetStringInUnit(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.hsc, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_d.Text = GetStringInUnit(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_fy.Text = GetStringInUnit(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fy, Enu_TypeVariable.Contrainte, 4, 0, False)
        Me.txt_fu.Text = GetStringInUnit(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fu, Enu_TypeVariable.Contrainte, 4, 0, False)
    End Sub

    ''' <summary>
    ''' MAJ des textboxs et combobox dans la zone de connection
    ''' </summary>
    Private Sub MAJ_affichage_txt_cmb_connection()
        lMAJAffichage = True

        'Met à jour les valeurs dans les txtbox ou cmbbox 

        Me.cmb_NbRow_I1.Enabled = MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
        If Not MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup Then cmb_NbRow_I1.SelectedItem = 1

        lMAJAffichage = False
    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub cmb_TypeConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeConnection.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case cmb_TypeConnection.SelectedItem
            Case strGoujonSemelle
                MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
            Case strGoujonAme
                MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
            Case strArmatureAme
                MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.ArmatureAme
        End Select

        img_Stud.Invalidate()

        MAJ_TLPanGauche()
        MAJ_affichage_txt_cmb_connection()
        MAJ_Valeurs_Limites()
    End Sub

    Private Sub MAJ_TLPanGauche()


        Select Case MyPoutreLoc.Dalle.typeConnecteur
            Case cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup, cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
                Me.TLPan_Gauche.RowStyles.Item(2).Height = 170
                Me.TLPan_Gauche.RowStyles.Item(3).Height = 0

                Me.Height = 590 - 120

            Case cls_Dalle.Enum_TypeConnecteur.ArmatureAme
                Me.TLPan_Gauche.RowStyles.Item(2).Height = 0
                Me.TLPan_Gauche.RowStyles.Item(3).Height = 120

                Me.Height = 590 - 170

        End Select

    End Sub

    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged
        If lBuild Then Exit Sub
        MyPoutreLoc.Dalle.ConnecteurArmature.Acier.Classe = Me.ClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()

        Me.img_Stud.Invalidate()
    End Sub


    Private Sub MAJI_ProprietesAcier()
        MyPoutreLoc.Dalle.ConnecteurArmature.Acier.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(MyPoutreLoc.Dalle.ConnecteurArmature.Acier.FsK, Enu_TypeVariable.Contrainte)
    End Sub

    Private Sub MAJ_SommeGoujons()
        If lGoujonsSoudes Then
            Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonTot(traveeEnCours) & " " & strStud
        Else
            Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonTot(traveeEnCours) & " " & strReinf
        End If
    End Sub

    Private Sub cmb_NbRow_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbRow_I1.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) = cmb_NbRow_I1.SelectedIndex + 1

        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

        img_Stud.Invalidate()
    End Sub

    Private Sub txt_EspLongi_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_EspLongi_I1.TextChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            MyPoutreLoc.EspacementZone(traveeEnCours, 0) = ValeurUI
            MAJ_SommeGoujons()

        End If
    End Sub

    Private Sub cmb_goujons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_goujons.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim iStud As Integer = cmb_goujons.SelectedIndex
        MyPoutreLoc.Dalle.ConnecteurGoujonSoude.nom = tabLabelGoujons(iStud)
        MyPoutreLoc.Dalle.ConnecteurGoujonSoude.hsc = BaseGoujons(iStud).hsc
        MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d = BaseGoujons(iStud).d
        MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fy = BaseGoujons(iStud).Fy
        MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fu = BaseGoujons(iStud).Fu

        MAJ_affichage_txt_connecteurs()
        img_Stud.Invalidate()

        MAJ_Valeurs_Limites()

        'Lorsqu'on modifie le diamètre des goujons, les valeurs limites de hsc, sx et sy changent. On doit corriger les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)


        Dim ValeurUI As Decimal
        ValeurUI = Me.txt_hsc.Text
        VerificationSaisie(Me.txt_hsc, ValeurUI, False) 'Vérification de la hauteur du goujon

        Dim lMAJ_cmb_NbRow As Boolean = False


        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For j As Integer = 0 To 2
                If Not (MyPoutreLoc.NombreGoujonsTransv(i, j) >= Nb_TransV_Row_MIN And MyPoutreLoc.NombreGoujonsTransv(i, j) <= Nb_TransV_Row_MAX) Then
                    MyPoutreLoc.NombreGoujonsTransv(i, j) = Nb_TransV_Row_MIN
                    lMAJ_cmb_NbRow = True
                End If

                ValeurUI = Me.txt_EspLongi_I1.Text
                VerificationSaisie(Me.txt_EspLongi_I1, ValeurUI, False)
            Next
        Next

        If lMAJ_cmb_NbRow Then
            Me.cmb_NbRow_I1.Items.Clear()

            For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
                Me.cmb_NbRow_I1.Items.Add(i)
            Next

            Me.cmb_NbRow_I1.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) - 1
        End If

    End Sub

    Private Sub txt_PhiS_TextChanged(sender As Object, e As EventArgs) Handles txt_PhiS.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            MyPoutreLoc.Dalle.ConnecteurArmature.ds = ValeurUI
        End If
    End Sub


#Region " Vérifiation des données "
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal, Optional VerifValConseillee As Boolean = False) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_Frm_Connection.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax, ValMaxConseillee As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim lValMaxConseillee As Boolean = False
        Dim kUnit As Decimal

        Select Case MyTxt.Name
            Case Me.txt_hsc.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = Hauteur_Goujon_MIN / kUnit
                ValMaxConseillee = Hauteur_Goujon_MAX_CONSEILLEE / kUnit
                lValMaxConseillee = True
                ValMax = Hauteur_Goujon_MAX / kUnit

            Case Me.txt_d.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = Diametre_Goujon_MIN / kUnit
                ValMax = Diametre_Goujon_MAX / kUnit

            Case Me.txt_EspLongi_I1.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = Espacement_Longi_MIN / kUnit
                ValMax = Espacement_Longi_MAX / kUnit

            Case Me.txt_PhiS.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = Diametre_Arma_MIN / kUnit
                ValMax = Diametre_Arma_MAX / kUnit

        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_Connection, ValMin, lValMin, ValMax, lValMax)
        Else

            ValeurUI = TraiteReal(MyTxt.Text) * kUnit

            If VerifValConseillee And lValMaxConseillee And ValeurUI > ValMaxConseillee Then
                If lGoujonsSoudes Then
                    MsgBox(strValMaxConseillee & "hsc > td - 20 mm")
                Else
                    MsgBox(strValMaxConseillee & "hsc > td - h - 20 mm")
                End If
            End If

                'ErrorProvider_Frm_Connection.Clear()
            End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region

#End Region

End Class