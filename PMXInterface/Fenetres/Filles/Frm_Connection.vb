Imports PMXMoteur2
Imports System.IO


Public Class Frm_Connection

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const PrefixeG As String = "M "

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_travee
    ''' </summary>
    Dim strTypeTravee() As String
    Dim strTypeTravee_ConsoleGauche As String
    Dim strTypeTravee_TraveeCentrale As String
    Dim strTypeTravee_ConsoleDroite As String

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

    Dim y_txt_cmb_esp_longi_actif As Decimal = 141
    Dim y_txt_cmb_esp_longi_passif As Decimal = 165

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
    Dim ltxt_Largeur_I2Enter As Boolean
    Dim ltxt_Largeur_I3Enter As Boolean

    ''' <summary>
    ''' Permet de bloquer les évènements quand on mets à jour les valeurs dans les txtbox
    ''' </summary>
    Dim lMAJAffichage As Boolean

    ''' <summary>
    ''' Indique que l'on vient de cliquer sur le bouton ajouter ou supprimer
    ''' </summary>
    Dim lBtnAjouterSupprimer As Boolean

    ''' <summary>
    ''' Donne le nombre de goujons max
    ''' </summary>
    Dim nb_goujons_trans_max As Integer

    ''' <summary>
    ''' Permet de garder en mémoire l'ancien indice du combobox travee
    ''' </summary>
    Dim Old_SelectedIndex_cmbTravee As Integer

    ''' <summary>
    ''' Message d'avertissement à afficher en cas de changement du cmb_travee alors qu'il y'a des erreurs à corriger
    ''' </summary>
    Dim WarningMessage_CmbTravee As String

    'Définition des valeurs limites pour les caractéristiques des goujons
    Dim Hauteur_Goujon_MIN As Decimal
    Dim Hauteur_Goujon_MAX_CONSEILLEE As Decimal 'valeur conseillée à ne pas dépasser 
    Dim Hauteur_Goujon_MAX As Decimal 'valeur à ne pas dépasser dans tous les cas 
    Dim Diametre_Goujon_MIN As Decimal
    Dim Diametre_Goujon_MAX As Decimal

    'Définition des valeurs limites pour les caractéristiques longitudinales
    Dim Nb_Zones_MIN As Integer
    Dim Nb_Zones_MAX As Integer
    Dim Longueur_Zone_MIN As Decimal
    Dim Longueur_Zone_MAX As Decimal
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
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_connecteurs()
        MAJ_affichage_txt_cmb_connection()
        MAJ_AutomaticDesign()
        lBuild = False
    End Sub

    ''' <summary>
    ''' Initialise les valeurs des variables locales 
    ''' </summary>
    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        MAJ_Valeurs_Limites()

        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire Then
            lBacTransv = True
        Else
            lBacTransv = False
        End If

        'Corrige les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)
        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            If Not (MyPoutreLoc.NombreZone(i) >= NB_ZONES_MIN And MyPoutreLoc.NombreZone(i) <= NB_ZONES_MAX) Then
                MyPoutreLoc.NombreZone(i) = NB_ZONES_MIN
            End If

            For j As Integer = 0 To 2
                If Not (MyPoutreLoc.NombreGoujonsTransv(i, j) >= NB_TRANSV_ROW_MIN And MyPoutreLoc.NombreGoujonsTransv(i, j) <= NB_TRANSV_ROW_MAX) Then
                    MyPoutreLoc.NombreGoujonsTransv(i, j) = NB_TRANSV_ROW_MIN
                End If

                If lBacTransv Then
                    If Not (MyPoutreLoc.Espacement_Bac_Trans(i, j) >= NB_ONDES_MIN And MyPoutreLoc.Espacement_Bac_Trans(i, j) <= NB_ONDES_MAX) Then
                        MyPoutreLoc.Espacement_Bac_Trans(i, j) = NB_ONDES_MIN
                    End If
                End If
            Next
        Next

        'Par défaut on affiche la première travée sur deux appuis
        traveeEnCours = 1

        'Il y'a toujours au moins 1 zone 
        Me.txt_Largeur_I1.Visible = True
        Me.cmb_NbRow_I1.Visible = True
        Me.cmb_EspLongi_I1.Visible = lBacTransv
        Me.txt_EspLongi_I1.Visible = Not lBacTransv

        ltxt_Largeur_I1Enter = False
        ltxt_Largeur_I2Enter = False
        ltxt_Largeur_I3Enter = False

        lMAJAffichage = False
        lBtnAjouterSupprimer = False

        Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MAX
        Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MIN

        '--> Initialisation table des variables goujons

        Dim nbStuds As Integer = BaseGoujons.GetUpperBound(0) + 1
        ReDim tabLabelGoujons(nbStuds - 1)
        ReDim tabLabelGoujonsAff(nbStuds - 1)

        For iStud As Integer = 0 To nbStuds - 1
            tabLabelGoujons(iStud) = BaseGoujons(iStud).Item1
            tabLabelGoujonsAff(iStud) = PrefixeG & BaseGoujons(iStud).Item1
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


                strValMaxConseillee = Bloc("RECMAXVALUE")


                '=== MENU CONNECTION ==============================================================='

                Me.lbl_Connection.Text = Bloc("CONNECTION")

                Me.chk_AutomaticDesign.Text = Bloc("AUTOMATICDESIGN")
                Me.txt_Portee.Text = Bloc("SPAN")
                strTypeTravee_ConsoleGauche = Bloc("LEFTCANT")
                strTypeTravee_TraveeCentrale = Bloc("MAINSPAN")
                strTypeTravee_ConsoleDroite = Bloc("RIGHTCANT")

                If MyPoutreLoc.lTraveeConsoleGauche Or MyPoutreLoc.lTraveeConsoleDroite Then
                    If MyPoutreLoc.lTraveeConsoleGauche And MyPoutreLoc.lTraveeConsoleDroite Then
                        ReDim strTypeTravee(2)
                    Else
                        ReDim strTypeTravee(1)
                    End If
                Else
                    ReDim strTypeTravee(0)
                End If

                strTypeTravee(0) = strTypeTravee_TraveeCentrale
                If MyPoutreLoc.lTraveeConsoleGauche Then strTypeTravee(1) = strTypeTravee_ConsoleGauche
                If MyPoutreLoc.lTraveeConsoleDroite Then
                    If MyPoutreLoc.lTraveeConsoleGauche Then
                        strTypeTravee(2) = strTypeTravee_ConsoleDroite
                    Else
                        strTypeTravee(1) = strTypeTravee_ConsoleDroite
                    End If
                End If

                Me.txt_Largeur.Text = Bloc("WIDTH") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.txt_NbRows.Text = Bloc("ROW_NUMBER") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                If lBacTransv Then
                    Me.txt_EspacementLongi.Text = Bloc("DISPOSITION_LON_BAC_TR")
                Else
                    Me.txt_EspacementLongi.Text = Bloc("DISPOSITION_LON_NO_BAC") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")"
                End If

                Me.btn_Ajouter.Text = Bloc("ADD")
                Me.btn_Supprimer.Text = Bloc("DELETE")

                strStud = Bloc("STUDS")
                strRib = Bloc("RIB")
                strRibs = Bloc("RIBS")

                WarningMessage_CmbTravee = Bloc("WARNING_CMBTRAVEE")


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

        Me.cmb_Travee.Items.Clear()
        Me.cmb_Travee.Items.AddRange(strTypeTravee)
        Me.cmb_Travee.SelectedIndex = 0

        Old_SelectedIndex_cmbTravee = Me.cmb_Travee.SelectedIndex

        Me.cmb_goujons.Items.Clear()
        Me.cmb_goujons.Items.AddRange(tabLabelGoujonsAff)
        Me.cmb_goujons.SelectedIndex = 0

        Me.cmb_NbRow_I1.Items.Clear()
        Me.cmb_NbRow_I2.Items.Clear()
        Me.cmb_NbRow_I3.Items.Clear()

        For i As Integer = NB_TRANSV_ROW_MIN To NB_TRANSV_ROW_MAX
            Me.cmb_NbRow_I1.Items.Add(i)
            Me.cmb_NbRow_I2.Items.Add(i)
            Me.cmb_NbRow_I3.Items.Add(i)
        Next

        Me.cmb_NbRow_I1.SelectedIndex = 0
        Me.cmb_NbRow_I2.SelectedIndex = 0
        Me.cmb_NbRow_I3.SelectedIndex = 0


        If lBacTransv Then
            Me.cmb_EspLongi_I1.Items.Clear()
            Me.cmb_EspLongi_I2.Items.Clear()
            Me.cmb_EspLongi_I3.Items.Clear()

            For i As Integer = NB_ONDES_MIN To NB_ONDES_MAX
                Me.cmb_EspLongi_I1.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I2.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I3.Items.Add(i & " " & strRib)
            Next

            Me.cmb_EspLongi_I1.SelectedIndex = 0
            Me.cmb_EspLongi_I2.SelectedIndex = 0
            Me.cmb_EspLongi_I3.SelectedIndex = 0
        End If

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Connecteurs.BackColor = CouleurBackBandeaux
        Me.lbl_Connecteurs.ForeColor = CouleurForeBandeaux

        Me.lbl_Connection.BackColor = CouleurBackBandeaux
        Me.lbl_Connection.ForeColor = CouleurForeBandeaux

        Me.img_Connection.Dock = DockStyle.Fill
        '
        'Me.img_Connection.BorderStyle = BorderStyle.FixedSingle

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
            cmb_EspLongi_I2.Location = New Point(cmb_EspLongi_I2.Location.X, y_txt_cmb_esp_longi_actif)
            cmb_EspLongi_I3.Location = New Point(cmb_EspLongi_I3.Location.X, y_txt_cmb_esp_longi_actif)

            txt_EspLongi_I1.Location = New Point(txt_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_passif)
            txt_EspLongi_I2.Location = New Point(txt_EspLongi_I2.Location.X, y_txt_cmb_esp_longi_passif)
            txt_EspLongi_I3.Location = New Point(txt_EspLongi_I3.Location.X, y_txt_cmb_esp_longi_passif)
        Else
            cmb_EspLongi_I1.Location = New Point(cmb_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_passif)
            cmb_EspLongi_I2.Location = New Point(cmb_EspLongi_I2.Location.X, y_txt_cmb_esp_longi_passif)
            cmb_EspLongi_I3.Location = New Point(cmb_EspLongi_I3.Location.X, y_txt_cmb_esp_longi_passif)

            txt_EspLongi_I1.Location = New Point(txt_EspLongi_I1.Location.X, y_txt_cmb_esp_longi_actif)
            txt_EspLongi_I2.Location = New Point(txt_EspLongi_I2.Location.X, y_txt_cmb_esp_longi_actif)
            txt_EspLongi_I3.Location = New Point(txt_EspLongi_I3.Location.X, y_txt_cmb_esp_longi_actif)
        End If

        cmb_EspLongi_I1.Visible = lBacTransv
        cmb_EspLongi_I2.Visible = lBacTransv
        cmb_EspLongi_I3.Visible = lBacTransv

        txt_EspLongi_I1.Visible = Not lBacTransv
        txt_EspLongi_I2.Visible = Not lBacTransv
        txt_EspLongi_I3.Visible = Not lBacTransv

        'Gestion des valeurs de la poutre en cours
        With MyPoutreLoc

            Me.chk_AutomaticDesign.Checked = .lAutomaticDesign

            If .NombreZone(traveeEnCours) >= 1 Then
                Me.txt_Largeur_I1.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours, 0), Enu_TypeVariable.Longueur, 4, 2, False)
                Me.cmb_NbRow_I1.SelectedIndex = .NombreGoujonsTransv(traveeEnCours, 0) - 1

                If Not lBacTransv Then
                    Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
                Else
                    Me.cmb_EspLongi_I1.SelectedIndex = .Espacement_Bac_Trans(traveeEnCours, 0) - 1
                End If
            End If

            If .NombreZone(traveeEnCours) >= 2 Then
                Me.txt_Largeur_I2.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours, 1), Enu_TypeVariable.Longueur, 4, 2, False)
                Me.cmb_NbRow_I2.SelectedIndex = .NombreGoujonsTransv(traveeEnCours, 1) - 1

                If Not lBacTransv Then
                    Me.txt_EspLongi_I2.Text = GetStringInUnit(.Espacement(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 0, False)
                Else
                    Me.cmb_EspLongi_I2.SelectedIndex = .Espacement_Bac_Trans(traveeEnCours, 1) - 1
                End If
            End If

            If .NombreZone(traveeEnCours) >= 3 Then
                Me.txt_Largeur_I3.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours, 2), Enu_TypeVariable.Longueur, 4, 2, False)
                Me.cmb_NbRow_I3.SelectedIndex = .NombreGoujonsTransv(traveeEnCours, 2) - 1

                If Not lBacTransv Then
                    Me.txt_EspLongi_I3.Text = GetStringInUnit(.Espacement(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 0, False)
                Else
                    Me.cmb_EspLongi_I3.SelectedIndex = .Espacement_Bac_Trans(traveeEnCours, 2) - 1
                End If
            End If


            'Me.cmb_NbRow_I1.SelectedItem = .NombreGoujonsTransv(traveeEnCours, 0)
            'Me.cmb_NbRow_I2.SelectedItem = .NombreGoujonsTransv(traveeEnCours, 1)
            'Me.cmb_NbRow_I3.SelectedItem = .NombreGoujonsTransv(traveeEnCours, 2)

            'If lBacTransv Then
            '    Me.cmb_EspLongi_I1.SelectedItem = .Espacement_Bac_Trans(traveeEnCours, 0)
            '    Me.cmb_EspLongi_I2.SelectedItem = .Espacement_Bac_Trans(traveeEnCours, 1)
            '    Me.cmb_EspLongi_I3.SelectedItem = .Espacement_Bac_Trans(traveeEnCours, 2)
            'Else
            '    Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
            '    Me.txt_EspLongi_I2.Text = GetStringInUnit(.Espacement(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 0, False)
            '    Me.txt_EspLongi_I3.Text = GetStringInUnit(.Espacement(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 0, False)
            'End If

            Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonsTot(traveeEnCours) & " " & strStud

        End With
    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Or Me.chk_AutomaticDesign.Checked Then

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

        'Vérification des textbox au cas où
        Dim list_txtbox As New List(Of TextBox)
        list_txtbox.Add(Me.txt_hsc)
        list_txtbox.Add(Me.txt_d)
        'list_txtbox.Add(Me.txt_fy)
        'list_txtbox.Add(Me.txt_fu)

        If MyPoutreLoc.NombreZone(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_Largeur_I1)
        If MyPoutreLoc.NombreZone(traveeEnCours) >= 2 Then list_txtbox.Add(Me.txt_Largeur_I2)
        If MyPoutreLoc.NombreZone(traveeEnCours) >= 3 Then list_txtbox.Add(Me.txt_Largeur_I3)

        If Not lBacTransv Then
            If MyPoutreLoc.NombreZone(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_EspLongi_I1)
            If MyPoutreLoc.NombreZone(traveeEnCours) >= 2 Then list_txtbox.Add(Me.txt_EspLongi_I2)
            If MyPoutreLoc.NombreZone(traveeEnCours) >= 3 Then list_txtbox.Add(Me.txt_EspLongi_I3)
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

            If .lAutomaticDesign <> MyPoutreLoc.lAutomaticDesign Then
                lModif = True
                .lAutomaticDesign = MyPoutreLoc.lAutomaticDesign
            End If

            If Not Me.chk_AutomaticDesign.Checked Then

                If .Dalle.Connecteur.nom <> MyPoutreLoc.Dalle.Connecteur.nom Then
                    lModif = True
                    .Dalle.Connecteur.nom = MyPoutreLoc.Dalle.Connecteur.nom
                    .Dalle.Connecteur.Caracteristiques_Goujons()
                End If

                For i As Integer = .IndicePremiereTravee To .IndiceDerniereTravee

                    If .NombreZone(i) <> MyPoutreLoc.NombreZone(i) Then
                        lModif = True
                        .NombreZone(i) = MyPoutreLoc.NombreZone(i)
                    End If

                    If .NombreGoujonsTot(i) <> MyPoutreLoc.NombreGoujonsTot(i) Then
                        lModif = True
                        .NombreGoujonsTot(i) = MyPoutreLoc.NombreGoujonsTot(i)
                    End If

                    For j As Integer = 0 To .NombreZone(i) - 1
                        If .Longueur_Zone(i, j) <> MyPoutreLoc.Longueur_Zone(i, j) Then
                            lModif = True
                            .Longueur_Zone(i, j) = MyPoutreLoc.Longueur_Zone(i, j)
                        End If

                        If .NombreGoujonsTransv(i, j) <> MyPoutreLoc.NombreGoujonsTransv(i, j) Then
                            lModif = True
                            .NombreGoujonsTransv(i, j) = MyPoutreLoc.NombreGoujonsTransv(i, j)
                        End If

                        If lBacTransv Then
                            If .Espacement_Bac_Trans(i, j) <> MyPoutreLoc.Espacement_Bac_Trans(i, j) Then
                                lModif = True
                                .Espacement_Bac_Trans(i, j) = MyPoutreLoc.Espacement_Bac_Trans(i, j)
                                .Espacement(i, j) = .Espacement_Bac_Trans(i, j) * .Dalle.Bac.e_p
                            End If
                        Else
                            If .Espacement(i, j) <> MyPoutreLoc.Espacement(i, j) Then
                                lModif = True
                                .Espacement(i, j) = MyPoutreLoc.Espacement(i, j)
                            End If
                        End If
                    Next

                Next

            End If
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

    Private Sub DessinConnection(sender As Object, e As PaintEventArgs) Handles img_Connection.Paint
        DessinFrmConnection_Connection(e.Graphics, MyPoutreLoc, Me.img_Connection.ClientRectangle.Width, Me.img_Connection.ClientRectangle.Height, 1, traveeEnCours, Not MyPoutreLoc.lAutomaticDesign, strStud)
    End Sub

    Private Sub DessinConnecteurs(sender As Object, e As PaintEventArgs) Handles img_Stud.Paint
        DessinFrmConnection_Connecteurs(e.Graphics, Me.img_Stud.ClientRectangle.Width, Me.img_Stud.ClientRectangle.Height, 1, MyPoutreLoc)
    End Sub

#End Region

#Region " Evènements "
    ''' <summary>
    ''' Met à jour les valeurs limites en fonction des données renseignées
    ''' </summary>
    Private Sub MAJ_Valeurs_Limites()
        'Valeurs en mètres

        'Définition des valeurs limites pour les caractéristiques des goujons
        HAUTEUR_GOUJON_MIN = 3 * MyPoutreLoc.Dalle.Connecteur.d
        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte Then
            HAUTEUR_GOUJON_MIN = Math.Max(HAUTEUR_GOUJON_MIN, MyPoutreLoc.Dalle.Bac.h_p + 2 * MyPoutreLoc.Dalle.Connecteur.d)
        End If
        HAUTEUR_GOUJON_MAX_CONSEILLEE = MyPoutreLoc.Dalle.t_d - 20 / 1000
        HAUTEUR_GOUJON_MAX = MyPoutreLoc.Dalle.t_d

        DIAMETRE_GOUJON_MIN = 16 / 1000 'Valeur arbitraire (16 mm), je me suis basé sur la clause 6.6.1.2(1) de l'EC4 actuel
        DIAMETRE_GOUJON_MAX = 0
        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire And (MyPoutreLoc.Dalle.Bac.AppuiT = Cls_Bac.EnuConfigTAppui.NervureEtBacContinus Or MyPoutreLoc.Dalle.Bac.AppuiT = Cls_Bac.EnuConfigTAppui.BetonSeulContinu) Then
            If MyPoutreLoc.Dalle.Bac.lPreperce Then
                DIAMETRE_GOUJON_MAX = 22 / 1000
            Else
                DIAMETRE_GOUJON_MAX = 20 / 1000
            End If
        Else
            DIAMETRE_GOUJON_MAX = 25 / 1000 'Valeur arbitraire (25 mm), je me suis basé sur la clause 6.6.1.2(1) de l'EC4 actuel
        End If

        nb_goujons_trans_max = 0
        For i As Integer = 0 To MyPoutreLoc.NombreZone(traveeEnCours) - 1
            nb_goujons_trans_max = Math.Max(nb_goujons_trans_max, MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, i))
        Next
        If nb_goujons_trans_max >= 2 Then DIAMETRE_GOUJON_MAX = Math.Min(2.5 * MyPoutreLoc.Section.ProfilA.t_fs, DIAMETRE_GOUJON_MAX)

        'Définition des valeurs limites pour les caractéristiques longitudinales
        LONGUEUR_ZONE_MIN = Math.Min(1, MyPoutreLoc.LongueurTravee(traveeEnCours))
        LONGUEUR_ZONE_MAX = MyPoutreLoc.LongueurTravee(traveeEnCours)
        NB_ZONES_MIN = 1
        NB_ZONES_MAX = Math.Min(Math.Floor(MyPoutreLoc.LongueurTravee(traveeEnCours) / LONGUEUR_ZONE_MIN), 3)
        ESPACEMENT_LONGI_MIN = 5 * MyPoutreLoc.Dalle.Connecteur.d
        ESPACEMENT_LONGI_MAX = Math.Min(800 / 1000, 6 * MyPoutreLoc.Dalle.t_d)
        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire Then
            NB_ONDES_MIN = 1
            NB_ONDES_MAX = Math.Floor(ESPACEMENT_LONGI_MAX / MyPoutreLoc.Dalle.Bac.e_p)
        End If

        'Définition des valeurs limites pour les caractéristiques transversales

        PINCE_TRANS_MIN = 20 / 1000
        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte Then
            ESPACEMENT_TRANS_MIN = 4 * MyPoutreLoc.Dalle.Connecteur.d
        Else 'dalle pleine ou préfa
            ESPACEMENT_TRANS_MIN = 2.5 * MyPoutreLoc.Dalle.Connecteur.d
        End If
        b_app_min = 50 / 1000
        NB_TRANSV_ROW_MIN = 1
        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire Then
            If MyPoutreLoc.Dalle.Bac.AppuiT = Cls_Bac.EnuConfigTAppui.Discontinu Then
                NB_TRANSV_ROW_MAX = Math.Floor((MyPoutreLoc.Section.ProfilA.b_fs - 2 * b_app_min - 2 * PINCE_TRANS_MIN - MyPoutreLoc.Dalle.Connecteur.d) / ESPACEMENT_TRANS_MIN + 1)
            Else
                NB_TRANSV_ROW_MAX = Math.Min(2, Math.Floor((MyPoutreLoc.Section.ProfilA.b_fs - 2 * PINCE_TRANS_MIN - MyPoutreLoc.Dalle.Connecteur.d) / ESPACEMENT_TRANS_MIN + 1))
            End If
        Else
            NB_TRANSV_ROW_MAX = Math.Floor((MyPoutreLoc.Section.ProfilA.b_fs - 2 * PINCE_TRANS_MIN - MyPoutreLoc.Dalle.Connecteur.d) / ESPACEMENT_TRANS_MIN + 1)
        End If


    End Sub

    ''' <summary>
    ''' Met à jour la fenêtre lorsque l'option automatic design est sélectionnée
    ''' </summary>
    Private Sub MAJ_AutomaticDesign()
        With MyPoutreLoc

            Me.txt_Portee.Enabled = Not .lAutomaticDesign
            Me.cmb_Travee.Enabled = Not .lAutomaticDesign

            Me.txt_Indice.Enabled = Not .lAutomaticDesign
            Me.txt_I1.Enabled = Not .lAutomaticDesign
            Me.txt_I2.Enabled = Not .lAutomaticDesign
            Me.txt_I3.Enabled = Not .lAutomaticDesign

            Me.txt_Largeur.Enabled = Not .lAutomaticDesign
            Me.txt_Largeur_I1.Enabled = Not .lAutomaticDesign
            Me.txt_Largeur_I2.Enabled = Not .lAutomaticDesign
            Me.txt_Largeur_I3.Enabled = Not .lAutomaticDesign

            Me.txt_NbRows.Enabled = Not .lAutomaticDesign
            Me.cmb_NbRow_I1.Enabled = Not .lAutomaticDesign
            Me.cmb_NbRow_I2.Enabled = Not .lAutomaticDesign
            Me.cmb_NbRow_I3.Enabled = Not .lAutomaticDesign

            Me.txt_EspacementLongi.Enabled = Not .lAutomaticDesign
            Me.cmb_EspLongi_I1.Enabled = Not .lAutomaticDesign
            Me.cmb_EspLongi_I2.Enabled = Not .lAutomaticDesign
            Me.cmb_EspLongi_I3.Enabled = Not .lAutomaticDesign

            Me.txt_EspLongi_I1.Enabled = Not .lAutomaticDesign
            Me.txt_EspLongi_I2.Enabled = Not .lAutomaticDesign
            Me.txt_EspLongi_I3.Enabled = Not .lAutomaticDesign

            If .lAutomaticDesign Then
                Me.btn_Ajouter.Enabled = Not .lAutomaticDesign
                Me.btn_Supprimer.Enabled = Not .lAutomaticDesign
            Else
                Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MAX
                Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MIN
            End If

            Me.etq_Somme.Visible = Not .lAutomaticDesign

            Me.img_Connection.Invalidate()

        End With
    End Sub

    ''' <summary>
    ''' MAJ des textboxs et comboboxs dans la zone des connecteurs
    ''' </summary>
    Private Sub MAJ_affichage_txt_connecteurs()
        Me.cmb_goujons.SelectedIndex = Array.IndexOf(tabLabelGoujons, MyPoutreLoc.Dalle.Connecteur.nom)
        Me.txt_hsc.Text = GetStringInUnit(MyPoutreLoc.Dalle.Connecteur.hsc, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_d.Text = GetStringInUnit(MyPoutreLoc.Dalle.Connecteur.d, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_fy.Text = GetStringInUnit(MyPoutreLoc.Dalle.Connecteur.Fy, Enu_TypeVariable.Contrainte, 4, 0, False)
        Me.txt_fu.Text = GetStringInUnit(MyPoutreLoc.Dalle.Connecteur.Fu, Enu_TypeVariable.Contrainte, 4, 0, False)
    End Sub

    ''' <summary>
    ''' MAJ des textboxs et combobox dans la zone de connection
    ''' </summary>
    Private Sub MAJ_affichage_txt_cmb_connection()
        lMAJAffichage = True

        'Met à jours la visibilité des txtbox

        Me.txt_Largeur_I1.ReadOnly = MyPoutreLoc.NombreZone(traveeEnCours) = 1

        Me.txt_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 2
        Me.txt_Largeur_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 2
        Me.cmb_NbRow_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 2
        Me.cmb_EspLongi_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 2 And lBacTransv
        Me.txt_EspLongi_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 2 And Not lBacTransv

        Me.txt_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 3
        Me.txt_Largeur_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 3
        Me.cmb_NbRow_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 3
        Me.cmb_EspLongi_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 3 And lBacTransv
        Me.txt_EspLongi_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours) >= 3 And Not lBacTransv

        'Met à jour les valeurs dans les txtbox ou cmbbox 

        If Not ltxt_Largeur_I1Enter Or lBtnAjouterSupprimer Then Me.txt_Largeur_I1.Text = GetStringInUnit(MyPoutreLoc.Longueur_Zone(traveeEnCours, 0), Enu_TypeVariable.Longueur, 4, 2, False)
        If Not ltxt_Largeur_I2Enter Or lBtnAjouterSupprimer Then Me.txt_Largeur_I2.Text = GetStringInUnit(MyPoutreLoc.Longueur_Zone(traveeEnCours, 1), Enu_TypeVariable.Longueur, 4, 2, False)
        If Not ltxt_Largeur_I3Enter Or lBtnAjouterSupprimer Then Me.txt_Largeur_I3.Text = GetStringInUnit(MyPoutreLoc.Longueur_Zone(traveeEnCours, 2), Enu_TypeVariable.Longueur, 4, 2, False)

        Me.cmb_NbRow_I1.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) - 1
        Me.cmb_NbRow_I2.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 1) - 1
        Me.cmb_NbRow_I3.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 2) - 1

        If lBacTransv Then
            Me.cmb_EspLongi_I1.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 0) - 1
            Me.cmb_EspLongi_I2.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 1) - 1
            Me.cmb_EspLongi_I3.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 2) - 1
        Else
            Me.txt_EspLongi_I1.Text = GetStringInUnit(MyPoutreLoc.Espacement(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
            Me.txt_EspLongi_I2.Text = GetStringInUnit(MyPoutreLoc.Espacement(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 0, False)
            Me.txt_EspLongi_I3.Text = GetStringInUnit(MyPoutreLoc.Espacement(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 0, False)
        End If

        lMAJAffichage = False
    End Sub

    ''' <summary>
    ''' Indique dans quel textbox on se situe
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txt_Largeur_I1_I2_I3_Enter(sender As Object, e As EventArgs) Handles txt_Largeur_I1.Enter, txt_Largeur_I2.Enter, txt_Largeur_I3.Enter
        If lBuild Or lMAJAffichage Then Exit Sub

        ltxt_Largeur_I1Enter = sender.name = txt_Largeur_I1.Name
        ltxt_Largeur_I2Enter = sender.name = txt_Largeur_I2.Name
        ltxt_Largeur_I3Enter = sender.name = txt_Largeur_I3.Name

    End Sub

    Private Sub txt_Largeur_EspLongi_I1_I2_I3_Leave(sender As Object, e As EventArgs) Handles txt_Largeur_I1.Leave, txt_Largeur_I2.Leave, txt_Largeur_I3.Leave
        ltxt_Largeur_I1Enter = sender.name = txt_Largeur_I1.Name
        ltxt_Largeur_I2Enter = sender.name = txt_Largeur_I2.Name
        ltxt_Largeur_I3Enter = sender.name = txt_Largeur_I3.Name
        MAJ_affichage_txt_cmb_connection()
    End Sub

#End Region

#Region " Evènements saisie "
    Private Sub btn_Ajouter_Click(sender As Object, e As EventArgs) Handles btn_Ajouter.Click
        If lBuild Then Exit Sub

        lBtnAjouterSupprimer = True

        If MyPoutreLoc.NombreZone(traveeEnCours) <= NB_ZONES_MAX - 1 Then MyPoutreLoc.NombreZone(traveeEnCours) += 1
        Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MAX
        Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MIN
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

        ValideSaisieFenetre()

        lBtnAjouterSupprimer = False
    End Sub

    Private Sub btn_Supprimer_Click(sender As Object, e As EventArgs) Handles btn_Supprimer.Click
        If lBuild Then Exit Sub

        lBtnAjouterSupprimer = True

        If MyPoutreLoc.NombreZone(traveeEnCours) >= NB_ZONES_MIN + 1 Then MyPoutreLoc.NombreZone(traveeEnCours) -= 1
        Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MAX
        Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MIN
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

        ValideSaisieFenetre()

        lBtnAjouterSupprimer = False
    End Sub

    Sub MAJ_Nb_Zone()

        'MAJ des longueurs de zone suite à un clique Ajouter ou Supprimer
        If lBuild Then Exit Sub
        Select Case MyPoutreLoc.NombreZone(traveeEnCours)
            Case 1
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours)
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = 0
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = 0
            Case 2
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours) / 2
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = MyPoutreLoc.LongueurTravee(traveeEnCours) / 2
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = 0
            Case 3
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours) / 3
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = MyPoutreLoc.LongueurTravee(traveeEnCours) / 3
                MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = MyPoutreLoc.LongueurTravee(traveeEnCours) / 3
        End Select


    End Sub

    Private Sub MAJ_SommeGoujons()
        'MAJ du calcul de la somme des goujons après les modifications des valeurs

        MyPoutreLoc.NombreGoujonsTot(traveeEnCours) = 0
        For i As Integer = 0 To 2
            MyPoutreLoc.NombreGoujonsTot(traveeEnCours) += Math.Floor(MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, i) * MyPoutreLoc.Longueur_Zone(traveeEnCours, i) / MyPoutreLoc.Espacement(traveeEnCours, i))
        Next

        Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonsTot(traveeEnCours) & " " & strStud

        Me.img_Connection.Invalidate()

    End Sub


    Private Sub txt_Largeur_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_Largeur_I1.TextChanged, txt_Largeur_I2.TextChanged, txt_Largeur_I3.TextChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

            Select Case sender.name
                Case txt_Largeur_I1.Name
                    If Not ltxt_Largeur_I1Enter Then Exit Sub
                    MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) = ValeurUI

                    Select Case MyPoutreLoc.NombreZone(traveeEnCours)
                        Case 1
                            MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = 0
                            MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = 0
                        Case 2
                            MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = MyPoutreLoc.LongueurTravee(traveeEnCours) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 0)
                            MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = 0
                        Case 3
                            MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = (MyPoutreLoc.LongueurTravee(traveeEnCours) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 0)) / 2
                            MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = (MyPoutreLoc.LongueurTravee(traveeEnCours) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 0)) / 2
                    End Select

                Case txt_Largeur_I2.Name
                    If Not ltxt_Largeur_I2Enter Then Exit Sub
                    MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) = ValeurUI

                    If MyPoutreLoc.NombreZone(traveeEnCours) = 2 Then
                        MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 1)
                        MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = 0
                    Else '3 zones
                        MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = MyPoutreLoc.LongueurTravee(traveeEnCours) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 1)
                    End If

                Case txt_Largeur_I3.Name
                    If Not ltxt_Largeur_I3Enter Then Exit Sub
                    MyPoutreLoc.Longueur_Zone(traveeEnCours, 2) = ValeurUI
                    MyPoutreLoc.Longueur_Zone(traveeEnCours, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 1) - MyPoutreLoc.Longueur_Zone(traveeEnCours, 2)
            End Select
            MAJ_SommeGoujons()
            MAJ_affichage_txt_cmb_connection()

        End If
    End Sub

    Private Sub cmb_NbRow_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbRow_I1.SelectedIndexChanged, cmb_NbRow_I2.SelectedIndexChanged, cmb_NbRow_I3.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Select Case sender.name
            Case cmb_NbRow_I1.Name
                MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) = cmb_NbRow_I1.SelectedIndex + 1
            Case cmb_NbRow_I2.Name
                MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 1) = cmb_NbRow_I2.SelectedIndex + 1
            Case cmb_NbRow_I3.Name
                MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 2) = cmb_NbRow_I3.SelectedIndex + 1
        End Select

        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()
    End Sub



    Private Sub cmb_EspLongi_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_EspLongi_I1.SelectedIndexChanged, cmb_EspLongi_I2.SelectedIndexChanged, cmb_EspLongi_I3.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub
        Select Case sender.name
            Case cmb_EspLongi_I1.Name
                MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 0) = cmb_EspLongi_I1.SelectedIndex + 1
                MyPoutreLoc.Espacement(traveeEnCours, 0) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 0)
            Case cmb_EspLongi_I2.Name
                MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 1) = cmb_EspLongi_I2.SelectedIndex + 1
                MyPoutreLoc.Espacement(traveeEnCours, 1) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 1)
            Case cmb_EspLongi_I3.Name
                MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 2) = cmb_EspLongi_I3.SelectedIndex + 1
                MyPoutreLoc.Espacement(traveeEnCours, 2) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 2)
        End Select
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()
    End Sub

    Private Sub txt_EspLongi_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_EspLongi_I1.TextChanged, txt_EspLongi_I2.TextChanged, txt_EspLongi_I3.TextChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case txt_EspLongi_I1.Name
                    MyPoutreLoc.Espacement(traveeEnCours, 0) = ValeurUI
                Case txt_EspLongi_I2.Name
                    MyPoutreLoc.Espacement(traveeEnCours, 1) = ValeurUI
                Case txt_EspLongi_I3.Name
                    MyPoutreLoc.Espacement(traveeEnCours, 2) = ValeurUI
            End Select
            MAJ_SommeGoujons()

        End If
    End Sub

    Private Sub cmb_goujons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_goujons.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim iStud As Integer = cmb_goujons.SelectedIndex
        MyPoutreLoc.Dalle.Connecteur.nom = tabLabelGoujons(istud)
        'MyPoutreLoc.Dalle.Connecteur.Caracteristiques_Goujons()
        MyPoutreLoc.Dalle.Connecteur.hsc = BaseGoujons(iStud).Item2
        MyPoutreLoc.Dalle.Connecteur.d = BaseGoujons(iStud).Item3
        MyPoutreLoc.Dalle.Connecteur.Fy = BaseGoujons(iStud).Item4
        MyPoutreLoc.Dalle.Connecteur.Fu = BaseGoujons(iStud).Item5

        MAJ_affichage_txt_connecteurs()
        img_Stud.Invalidate()

        MAJ_Valeurs_Limites()

        'Lorsqu'on modifie le diamètre des goujons, les valeurs limites de hsc, sx et sy changent. On doit corriger les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)


        Dim ValeurUI As Decimal
        ValeurUI = Me.txt_hsc.Text
        VerificationSaisie(Me.txt_hsc, Me.txt_hsc.Text, False) 'Vérification de la hauteur du goujon

        Dim lMAJ_cmb_NbRow As Boolean = False
        Dim lMAJ_cmb_EspLongi As Boolean = False


        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For j As Integer = 0 To 2
                If Not (MyPoutreLoc.NombreGoujonsTransv(i, j) >= NB_TRANSV_ROW_MIN And MyPoutreLoc.NombreGoujonsTransv(i, j) <= NB_TRANSV_ROW_MAX) Then
                    MyPoutreLoc.NombreGoujonsTransv(i, j) = NB_TRANSV_ROW_MIN
                    lMAJ_cmb_NbRow = True
                End If

                If lBacTransv Then
                    If Not (MyPoutreLoc.Espacement_Bac_Trans(i, j) >= NB_ONDES_MIN And MyPoutreLoc.Espacement_Bac_Trans(i, j) <= NB_ONDES_MAX) Then
                        MyPoutreLoc.Espacement_Bac_Trans(i, j) = NB_ONDES_MIN
                        lMAJ_cmb_EspLongi = True
                    End If
                Else


                    ValeurUI = Me.txt_EspLongi_I1.Text
                    VerificationSaisie(Me.txt_EspLongi_I1, ValeurUI, False)

                    ValeurUI = Me.txt_EspLongi_I2.Text
                    VerificationSaisie(Me.txt_EspLongi_I2, ValeurUI, False)

                    ValeurUI = Me.txt_EspLongi_I3.Text
                    VerificationSaisie(Me.txt_EspLongi_I3, ValeurUI, False)
                End If
            Next
        Next

        If lMAJ_cmb_NbRow Then
            Me.cmb_NbRow_I1.Items.Clear()
            Me.cmb_NbRow_I2.Items.Clear()
            Me.cmb_NbRow_I3.Items.Clear()

            For i As Integer = NB_TRANSV_ROW_MIN To NB_TRANSV_ROW_MAX
                Me.cmb_NbRow_I1.Items.Add(i)
                Me.cmb_NbRow_I2.Items.Add(i)
                Me.cmb_NbRow_I3.Items.Add(i)
            Next

            Me.cmb_NbRow_I1.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 0) - 1
            Me.cmb_NbRow_I2.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 1) - 1
            Me.cmb_NbRow_I3.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours, 2) - 1
        End If

        If lMAJ_cmb_EspLongi Then
            Me.cmb_EspLongi_I1.Items.Clear()
            Me.cmb_EspLongi_I2.Items.Clear()
            Me.cmb_EspLongi_I3.Items.Clear()

            For i As Integer = NB_ONDES_MIN To NB_ONDES_MAX
                Me.cmb_EspLongi_I1.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I2.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I3.Items.Add(i & " " & strRib)
            Next

            Me.cmb_EspLongi_I1.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 0) - 1
            Me.cmb_EspLongi_I2.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 1) - 1
            Me.cmb_EspLongi_I3.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours, 2) - 1
        End If


    End Sub

    Private Sub cmb_Travee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Travee.SelectedIndexChanged
        If lBuild Then Exit Sub

        If ValideSaisieFenetre() Then

            Old_SelectedIndex_cmbTravee = cmb_Travee.SelectedIndex

            'permet de mettre à jour les variables locales qui tracent l'indice de la travée en cours 

            Select Case cmb_Travee.Text
                Case strTypeTravee_ConsoleGauche
                    traveeEnCours = 0

                Case strTypeTravee_TraveeCentrale
                    traveeEnCours = 1

                Case strTypeTravee_ConsoleDroite
                    traveeEnCours = MyPoutreLoc.IndiceTraveeConsoleDroite
            End Select

            'Réinitialise les boutons Ajouter/Supprimer

            Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MAX
            Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours) = NB_ZONES_MIN

            MAJ_SommeGoujons()
            MAJ_affichage_txt_cmb_connection()

        Else

            If Not cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee Then MsgBox(WarningMessage_CmbTravee)
            cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee

        End If

    End Sub

    Private Sub chk_AutomaticDesign_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AutomaticDesign.CheckedChanged
        If lBuild Then Exit Sub
        MyPoutreLoc.lAutomaticDesign = chk_AutomaticDesign.Checked
        MAJ_AutomaticDesign()
    End Sub


#Region " Vérifiation des données "
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal, Optional VerifValConseillee As Boolean = False) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_Frm_Connection.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax, ValMaxConseillee As Decimal
        Dim lValMax As Boolean = True
        Dim lValMaxConseillee As Boolean = False
        Dim kUnit As Decimal

        Select Case MyTxt.Name
            Case Me.txt_hsc.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = HAUTEUR_GOUJON_MIN / kUnit
                ValMaxConseillee = HAUTEUR_GOUJON_MAX_CONSEILLEE / kUnit
                lValMaxConseillee = True
                ValMax = HAUTEUR_GOUJON_MAX / kUnit

            Case Me.txt_d.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = DIAMETRE_GOUJON_MIN / kUnit
                ValMax = DIAMETRE_GOUJON_MAX / kUnit

            Case Me.txt_Largeur_I1.Name, Me.txt_Largeur_I2.Name, Me.txt_Largeur_I3.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

                ValMin = LONGUEUR_ZONE_MIN / kUnit
                ValMax = LONGUEUR_ZONE_MAX / kUnit

            Case Me.txt_EspLongi_I1.Name, Me.txt_EspLongi_I2.Name, Me.txt_EspLongi_I3.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = ESPACEMENT_LONGI_MIN / kUnit
                ValMax = ESPACEMENT_LONGI_MAX / kUnit

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_Connection, ValMin, ValMax)
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

#End Region



#End Region

End Class