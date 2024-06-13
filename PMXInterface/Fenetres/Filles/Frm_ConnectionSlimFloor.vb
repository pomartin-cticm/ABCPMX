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
    Dim MyPoutreLoc As New cls_Poutre()

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_
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
    ''' variable locale qui informe quelle travée est affichée à l'écran
    ''' 1er item: donne la nature de la travée
    ''' 2nd item: donne l'indice de la travée selectionnée
    ''' </summary>
    Dim traveeEnCours As Integer = 1

    Dim x_lbl_esp_longi_dalle_mixte As Decimal = -20
    Dim x_lbl_esp_longi_dalle_pleine As Decimal = 13

    Dim x_txt_cmb_esp_longi_dalle_mixte As Decimal = 135
    Dim x_txt_cmb_longi_dalle_pleine As Decimal = 168

    Dim y_txt_cmb_esp_longi_actif As Decimal = 40
    Dim y_txt_cmb_esp_longi_passif As Decimal = 61

    ''' <summary>
    ''' Indique la présence d'un bac disposé transversalement (=True) ou non (=False)
    ''' </summary>
    Dim lBacTransv As Boolean

    ''' <summary>
    ''' Permet de stocker localement le mot clé associé aux goujons 
    ''' </summary>
    Dim strStud As String

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

    ''' <summary>
    ''' Donne le nombre de goujons max
    ''' </summary>
    Dim nb_goujons_trans_max As Integer


    'Définition des valeurs limites pour les caractéristiques des goujons
    Dim Hauteur_Goujon_MIN As Decimal
    Dim Hauteur_Goujon_MAX_CONSEILLEE As Decimal 'valeur conseillée à ne pas dépasser 
    Dim Hauteur_Goujon_MAX As Decimal 'valeur à ne pas dépasser dans tous les cas 
    Dim Diametre_Goujon_MIN As Decimal
    Dim Diametre_Goujon_MAX As Decimal

    'Définition des valeurs limites pour les caractéristiques longitudinales
    Dim Espacement_Longi_MIN As Decimal 'sxi,min dans les ST
    Dim Espacement_Longi_MAX As Decimal 'sxi,max dans les ST
    Dim Nb_Ondes_MIN As Integer
    Dim Nb_Ondes_MAX As Integer

    'Définition des valeurs limites pour les caractéristiques transversales
    Dim Espacement_Trans_MIN As Decimal
    Dim Pince_Trans_MIN As Decimal 'Correspond à eD,min dans les Specifications Techniques 
    Dim b_app_min As Decimal
    Dim Nb_TransV_Row_MIN As Integer
    Dim Nb_TransV_Row_MAX As Integer

    Dim NbTravees As Integer


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

        NbTravees = MyPoutreLoc.NbTravees

        'Par défaut on affiche la première travée sur deux appuis
        traveeEnCours = 1


        MAJ_Valeurs_Limites()

        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
            lBacTransv = True
        Else
            lBacTransv = False
        End If

        'Corrige les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)
        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For j As Integer = 0 To 2
                If Not (MyPoutreLoc.NombreGoujonsTransv(i, j) >= Nb_TransV_Row_MIN And MyPoutreLoc.NombreGoujonsTransv(i, j) <= Nb_TransV_Row_MAX) Then
                    MyPoutreLoc.NombreGoujonsTransv(i, j) = Nb_TransV_Row_MIN
                End If

                If lBacTransv Then
                    If Not (MyPoutreLoc.Espacement_Bac_TransZone(i, j) >= Nb_Ondes_MIN And MyPoutreLoc.Espacement_Bac_TransZone(i, j) <= Nb_Ondes_MAX) Then
                        MyPoutreLoc.Espacement_Bac_TransZone(i, j) = Nb_Ondes_MIN
                    End If
                End If
            Next
        Next

        'Il y'a toujours au moins 1 zone 
        Me.cmb_NbRow_I1.Visible = True
        Me.cmb_EspLongi_I1.Visible = lBacTransv
        Me.txt_EspLongi_I1.Visible = Not lBacTransv

        ltxt_Largeur_I1Enter = False
        lMAJAffichage = False

        '--> Initialisation table des variables goujons

        Dim nbStuds As Integer = BaseGoujons.Count
        ReDim tabLabelGoujons(nbStuds - 1)
        ReDim tabLabelGoujonsAff(nbStuds - 1)

        For iStud As Integer = 0 To nbStuds - 1
            tabLabelGoujons(iStud) = BaseGoujons(iStud).nom
            tabLabelGoujonsAff(iStud) = PrefixeG & BaseGoujons(iStud).nom
        Next

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


                '=== MENU CONNECTION ==============================================================='

                Me.lbl_Connection.Text = Bloc("CONNECTION")

                Me.lbl_NbRows.Text = Bloc("ROW_NUMBER")
                If lBacTransv Then
                    Me.lbl_EspacementLongi.Text = Bloc("DISPOSITION_LON_BAC_TR")
                Else
                    Me.lbl_EspacementLongi.Text = Bloc("DISPOSITION_LON_NO_BAC") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")"
                End If

                strStud = Bloc("STUDS")
                strRib = Bloc("RIB")
                strRibs = Bloc("RIBS")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitD.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitHsc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFy.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitFu.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub RemplirComboBox()
        Dim index As Integer = 0

        NbTravees = MyPoutreLoc.NbTravees
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

        Me.cmb_NbRow_I1.Items.Clear()

        For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
            Me.cmb_NbRow_I1.Items.Add(i)
        Next

        Me.cmb_NbRow_I1.SelectedIndex = 0


        If lBacTransv Then
            Me.cmb_EspLongi_I1.Items.Clear()

            For i As Integer = Nb_Ondes_MIN To Nb_Ondes_MAX
                Me.cmb_EspLongi_I1.Items.Add(i & " " & strRib)
            Next

            Me.cmb_EspLongi_I1.SelectedIndex = 0
        End If

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

        'Gestion de l'affichage en fonction de la présence ou non d'un bac transversal
        If lBacTransv Then
            cmb_EspLongi_I1.Location = New Point(cmb_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_actif)
            txt_EspLongi_I1.Location = New Point(txt_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_passif)
        Else
            cmb_EspLongi_I1.Location = New Point(cmb_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_passif)
            txt_EspLongi_I1.Location = New Point(txt_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_actif)
        End If

        cmb_EspLongi_I1.Visible = lBacTransv
        txt_EspLongi_I1.Visible = Not lBacTransv

        'Gestion de l'affichage en fonction de la présence d'une dalle mixte ou non
        If MyPoutreLoc.Dalle.lMixte Then
            Me.lbl_NbRows.Location = New Point(x_lbl_esp_longi_dalle_mixte, lbl_NbRows.Location.Y)
            Me.lbl_EspacementLongi.Location = New Point(x_lbl_esp_longi_dalle_mixte, lbl_EspacementLongi.Location.Y)

            Me.cmb_NbRow_I1.Location = New Point(x_txt_cmb_esp_longi_dalle_mixte, cmb_NbRow_I1.Location.Y)
            Me.cmb_EspLongi_I1.Location = New Point(x_txt_cmb_esp_longi_dalle_mixte, cmb_EspLongi_I1.Location.Y)
            Me.txt_EspLongi_I1.Location = New Point(x_txt_cmb_esp_longi_dalle_mixte, txt_EspLongi_I1.Location.Y)
        Else
            Me.lbl_NbRows.Location = New Point(x_lbl_esp_longi_dalle_pleine, lbl_NbRows.Location.Y)
            Me.lbl_EspacementLongi.Location = New Point(x_lbl_esp_longi_dalle_pleine, lbl_EspacementLongi.Location.Y)

            Me.cmb_NbRow_I1.Location = New Point(x_txt_cmb_longi_dalle_pleine, cmb_NbRow_I1.Location.Y)
            Me.cmb_EspLongi_I1.Location = New Point(x_txt_cmb_longi_dalle_pleine, cmb_EspLongi_I1.Location.Y)
            Me.txt_EspLongi_I1.Location = New Point(x_txt_cmb_longi_dalle_pleine, txt_EspLongi_I1.Location.Y)
        End If

        'Gestion des valeurs de la poutre en cours
        With MyPoutreLoc
            Me.cmb_NbRow_I1.SelectedIndex = .NombreGoujonsTransv(traveeEnCours, 0) - 1

            If Not lBacTransv Then
                Me.txt_EspLongi_I1.Text = GetStringInUnit(.EspacementZone(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
            Else
                Me.cmb_EspLongi_I1.SelectedIndex = .Espacement_Bac_TransZone(traveeEnCours, 0) - 1
            End If

            Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonTot(traveeEnCours) & " " & strStud

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
        list_txtbox.Add(Me.txt_hsc)
        list_txtbox.Add(Me.txt_d)

        If Not lBacTransv Then
            If MyPoutreLoc.NombreZones(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_EspLongi_I1)
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

            GereTransfertValeur(MyPoutreLoc.lAutomaticDesign, .lAutomaticDesign, lModif)

            GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.nom, .Dalle.ConnecteurGoujonSoude.nom, lModif)
            GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.hsc, .Dalle.ConnecteurGoujonSoude.hsc, lModif)
            GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d, .Dalle.ConnecteurGoujonSoude.d, lModif)
            GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fy, .Dalle.ConnecteurGoujonSoude.Fy, lModif)
            GereTransfertValeur(MyPoutreLoc.Dalle.ConnecteurGoujonSoude.Fu, .Dalle.ConnecteurGoujonSoude.Fu, lModif)


            For i As Integer = .IndicePremiereTravee To .IndiceDerniereTravee

                GereTransfertValeur(MyPoutreLoc.NombreZones(i), .NombreZones(i), lModif)
                'GereTransfertValeur(MyPoutreLoc.NombreGoujonsTot(i), .NombreGoujonsTot(i), lModif)

                For j As Integer = 0 To .NombreZones(i) - 1

                    GereTransfertValeur(MyPoutreLoc.LongueurZone(i, j), .LongueurZone(i, j), lModif)
                    GereTransfertValeur(MyPoutreLoc.NombreGoujonsTransv(i, j), .NombreGoujonsTransv(i, j), lModif)

                    If lBacTransv Then
                        GereTransfertValeur(MyPoutreLoc.Espacement_Bac_TransZone(i, j), .Espacement_Bac_TransZone(i, j), lModif)
                        .EspacementZone(i, j) = .Espacement_Bac_TransZone(i, j) * .Dalle.Bac.Ep

                    Else
                        GereTransfertValeur(MyPoutreLoc.EspacementZone(i, j), .EspacementZone(i, j), lModif)
                    End If
                Next

            Next

        End With

    End Sub


#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_d.Paint, img_hsc.Paint, img_fy.Paint, img_fu.Paint

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
        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            Hauteur_Goujon_MIN = Math.Max(Hauteur_Goujon_MIN, MyPoutreLoc.Dalle.Bac.Hp + 2 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d)
        End If
        Hauteur_Goujon_MAX_CONSEILLEE = MyPoutreLoc.Dalle.Ep_td - 20 / 1000
        Hauteur_Goujon_MAX = MyPoutreLoc.Dalle.Ep_td

        Diametre_Goujon_MIN = 16 / 1000 'Valeur arbitraire (16 mm), je me suis basé sur la clause 6.6.1.2(1) de l'EC4 actuel
        Diametre_Goujon_MAX = 0
        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire And (MyPoutreLoc.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus Or MyPoutreLoc.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.BetonSeulContinu) Then
            If MyPoutreLoc.Dalle.Bac.lPreperce Then
                Diametre_Goujon_MAX = 22 / 1000
            Else
                Diametre_Goujon_MAX = 20 / 1000
            End If
        Else
            Diametre_Goujon_MAX = 25 / 1000 'Valeur arbitraire (25 mm), je me suis basé sur la clause 6.6.1.2(1) de l'EC4 actuel
        End If

        nb_goujons_trans_max = 0
        For i As Integer = 0 To MyPoutreLoc.NombreZones(traveeEnCours) - 1
            nb_goujons_trans_max = Math.Max(nb_goujons_trans_max, MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, i))
        Next
        If nb_goujons_trans_max >= 2 Then Diametre_Goujon_MAX = Math.Min(2.5 * MyPoutreLoc.Section.ProfilA.Tfs, Diametre_Goujon_MAX)

        Espacement_Longi_MIN = 5 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d
        Espacement_Longi_MAX = Math.Min(800 / 1000, 6 * MyPoutreLoc.Dalle.Ep_td)
        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
            Nb_Ondes_MIN = 1
            Nb_Ondes_MAX = Math.Floor(Espacement_Longi_MAX / MyPoutreLoc.Dalle.Bac.Ep)
        End If

        'Définition des valeurs limites pour les caractéristiques transversales

        Pince_Trans_MIN = 20 / 1000
        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            Espacement_Trans_MIN = 4 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d
        Else 'dalle pleine ou préfa
            Espacement_Trans_MIN = 2.5 * MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d
        End If
        b_app_min = OptionsSlimFloor.bappmin
        Nb_TransV_Row_MIN = 1

        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
            If MyPoutreLoc.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu Then
                Nb_TransV_Row_MAX = Math.Floor((MyPoutreLoc.Section.ProfilA.Bfs - 2 * b_app_min - 2 * Pince_Trans_MIN - MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d) / Espacement_Trans_MIN + 1)
            Else
                Nb_TransV_Row_MAX = Math.Min(2, Math.Floor((MyPoutreLoc.Section.ProfilA.Bfs - 2 * Pince_Trans_MIN - MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d) / Espacement_Trans_MIN + 1))
            End If
        Else
            Nb_TransV_Row_MAX = Math.Floor((MyPoutreLoc.Section.ProfilA.Bfs - 2 * Pince_Trans_MIN - MyPoutreLoc.Dalle.ConnecteurGoujonSoude.d) / Espacement_Trans_MIN + 1)
        End If

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

        Me.cmb_NbRow_I1.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) - 1

        If lBacTransv Then
            Me.cmb_EspLongi_I1.SelectedIndex = MyPoutreLoc.Espacement_Bac_TransZone(traveeEnCours, 0) - 1
        Else
            Me.txt_EspLongi_I1.Text = GetStringInUnit(MyPoutreLoc.EspacementZone(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
        End If

        lMAJAffichage = False
    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub MAJ_SommeGoujons()
        Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonTot(traveeEnCours) & " " & strStud
    End Sub

    Private Sub cmb_NbRow_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbRow_I1.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) = cmb_NbRow_I1.SelectedIndex + 1

        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

        img_Stud.Invalidate()
    End Sub



    Private Sub cmb_EspLongi_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_EspLongi_I1.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        MyPoutreLoc.Espacement_Bac_TransZone(traveeEnCours, 0) = cmb_EspLongi_I1.SelectedIndex + 1
        MyPoutreLoc.EspacementZone(traveeEnCours, 0) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_TransZone(traveeEnCours, 0)

        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

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
        Dim lMAJ_cmb_EspLongi As Boolean = False


        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For j As Integer = 0 To 2
                If Not (MyPoutreLoc.NombreGoujonsTransv(i, j) >= Nb_TransV_Row_MIN And MyPoutreLoc.NombreGoujonsTransv(i, j) <= Nb_TransV_Row_MAX) Then
                    MyPoutreLoc.NombreGoujonsTransv(i, j) = Nb_TransV_Row_MIN
                    lMAJ_cmb_NbRow = True
                End If

                If lBacTransv Then
                    If Not (MyPoutreLoc.Espacement_Bac_TransZone(i, j) >= Nb_Ondes_MIN And MyPoutreLoc.Espacement_Bac_TransZone(i, j) <= Nb_Ondes_MAX) Then
                        MyPoutreLoc.Espacement_Bac_TransZone(i, j) = Nb_Ondes_MIN
                        lMAJ_cmb_EspLongi = True
                    End If
                Else
                    ValeurUI = Me.txt_EspLongi_I1.Text
                    VerificationSaisie(Me.txt_EspLongi_I1, ValeurUI, False)

                End If
            Next
        Next

        If lMAJ_cmb_NbRow Then
            Me.cmb_NbRow_I1.Items.Clear()

            For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
                Me.cmb_NbRow_I1.Items.Add(i)
            Next

            Me.cmb_NbRow_I1.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) - 1
        End If

        If lMAJ_cmb_EspLongi Then
            Me.cmb_EspLongi_I1.Items.Clear()

            For i As Integer = Nb_Ondes_MIN To Nb_Ondes_MAX
                Me.cmb_EspLongi_I1.Items.Add(i & " " & strRib)
            Next

            Me.cmb_EspLongi_I1.SelectedIndex = MyPoutreLoc.Espacement_Bac_TransZone(traveeEnCours, 0) - 1
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

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_Connection, ValMin, lValMin, ValMax, lValMax)
        Else

            ValeurUI = TraiteReal(MyTxt.Text) * kUnit

            If VerifValConseillee And lValMaxConseillee And ValeurUI > ValMaxConseillee Then
                MsgBox(strValMaxConseillee & "hsc > td - 20 mm")
            End If

            'ErrorProvider_Frm_Connection.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

    Private Sub cmb_TypeConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeConnection.SelectedIndexChanged
        Select Case cmb_TypeConnection.SelectedItem
            Case strGoujonSemelle
                MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
            Case strGoujonAme
                MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
            Case strArmatureAme
                MyPoutreLoc.Dalle.typeConnecteur = cls_Dalle.Enum_TypeConnecteur.ArmatureAme
        End Select
        img_Stud.Invalidate()
    End Sub


#End Region

#End Region

End Class