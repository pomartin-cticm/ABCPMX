Imports PMXMoteur2
Imports System.IO

Public Class Frm_Connection

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const PrefixeG As String = "M "

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim myBeamLoc As New cls_Poutre(NomChargements)

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_travee
    ''' </summary>
    Dim strTypeTravee() As String
    Dim strTypeTravee_ConsoleGauche As String
    Dim strTypeTravee_TraveeCentrale As String
    Dim strTypeTravee_ConsoleDroite As String
    Dim strSpan As String

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

    Dim NbTravees As Integer

    Dim FontFrm As Font

    '== POM
    Dim tabDiam() As Decimal
    Dim tabHsc() As Decimal

    Dim strInfoW_DegreConnex As String
    Dim strWarningGoujons As String

    Dim strErrorDia(1) As String

    Dim lMultiSpan As Boolean

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
        PrepareFlechesNavigation()
        MAJI_BtnNavigation()
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

        '--( Déclarations

        Dim lMixte As Boolean
        Dim lTrans As Boolean       ' Nervure perpendiculaire à la poutre
        Dim lNCont As Boolean       ' Nervure béton continue sur semelle
        Dim lCf220 As Boolean       ' Dalle mixte avec cofraplus 220

        '--( Dupplication de la poutre en cours

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), myBeamLoc)

        '--( Initialisation des paramètres

        NbTravees = myBeamLoc.NbTravees
        lMixte = (myBeamLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte)
        lTrans = (myBeamLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)
        lNCont = Not (myBeamLoc.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu)
        lCf220 = myBeamLoc.Dalle.Bac.lCofraplus220
        lMultiSpan = myBeamLoc.lMultiSpan

        Me.lbl_EtaSymbol.Visible = Not lMultiSpan
        Me.lbl_DegreConnex.Visible = Not lMultiSpan

        'Par défaut on affiche la première travée sur deux appuis
        traveeEnCours = 1

        MAJ_Valeurs_Limites()

        'If lMixte And lTrans Then
        '    lBacTransv = True
        'Else
        '    lBacTransv = False
        'End If
        '== On applique les dispositions de bacs perpendiculaires si la nervure est continue, et hors bac cofraplus 220
        lBacTransv = lMixte And lTrans And lNCont And (Not lCf220)

        'Corrige les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)
        For i As Integer = myBeamLoc.IndicePremiereTravee To myBeamLoc.IndiceDerniereTravee
            If Not (myBeamLoc.NombreZones(i) >= Nb_Zones_MIN And myBeamLoc.NombreZones(i) <= Nb_Zones_MAX) Then
                myBeamLoc.NombreZones(i) = Nb_Zones_MIN
            End If

            For j As Integer = 0 To 2
                If Not (myBeamLoc.NrTransZone(i, j) >= Nb_TransV_Row_MIN And myBeamLoc.NrTransZone(i, j) <= Nb_TransV_Row_MAX) Then
                    myBeamLoc.NrTransZone(i, j) = Nb_TransV_Row_MIN
                End If

                If lBacTransv Then
                    If Not (myBeamLoc.Espacement_Bac_TransZone(i, j) >= Nb_Ondes_MIN And myBeamLoc.Espacement_Bac_TransZone(i, j) <= Nb_Ondes_MAX) Then
                        myBeamLoc.Espacement_Bac_TransZone(i, j) = Nb_Ondes_MIN
                    End If
                End If
            Next
        Next

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

        Me.btn_Ajouter.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MAX
        Me.btn_Supprimer.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MIN

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
                Me.lbl_Stud.Text = Bloc("STUD")

                strValMaxConseillee = Bloc("RECMAXVALUE")


                '=== MENU CONNECTION ==============================================================='

                Me.lbl_Connection.Text = Bloc("CONNECTION")

                Me.chk_AutomaticDesign.Text = Bloc("AUTOMATICDESIGN")
                Me.txt_Portee.Text = Bloc("SPAN")
                strTypeTravee_ConsoleGauche = Bloc("LEFTCANT")
                strTypeTravee_TraveeCentrale = Bloc("MAINSPAN")
                strTypeTravee_ConsoleDroite = Bloc("RIGHTCANT")
                strSpan = Bloc("SPAN")

                Me.txt_Largeur.Text = Bloc("WIDTH") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.txt_NbRows.Text = Bloc("ROW_NUMBER")
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

                strWarningGoujons = Bloc("STUDCONCRETECOVER")
                strInfoW_DegreConnex = Bloc("INFOW_SHEARCONNECTION")

                strErrorDia(0) = Bloc("STUDPREPUNCHED")
                strErrorDia(1) = Bloc("STUDWELDEDTHROUGH")

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

        Me.etq_UnitD.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitHsc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFy.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitFu.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub RemplirComboBox()
        Dim index As Integer = 0

        NbTravees = myBeamLoc.NbTravees
        ReDim strTypeTravee(NbTravees - 1)
        Dim lCentral As Boolean = (myBeamLoc.NombreTraveesDeuxAppuis = 1)
        If myBeamLoc.lTraveeConsoleGauche Then
            strTypeTravee(index) = strTypeTravee_ConsoleGauche
            index += 1
        End If
        For i As Integer = 1 To myBeamLoc.NombreTraveesDeuxAppuis
            If lCentral Then
                strTypeTravee(index) = strTypeTravee_TraveeCentrale
            Else
                strTypeTravee(index) = strSpan & " no " & CStr(i)
            End If
            index += 1
        Next
        If myBeamLoc.lTraveeConsoleDroite Then strTypeTravee(index) = strTypeTravee_ConsoleDroite

        Me.cmb_Travee.Items.Clear()
        Me.cmb_Travee.Items.AddRange(strTypeTravee)
        If myBeamLoc.lTraveeConsoleGauche Then
            Me.cmb_Travee.SelectedIndex = 1
        Else
            Me.cmb_Travee.SelectedIndex = 0
        End If

        Old_SelectedIndex_cmbTravee = Me.cmb_Travee.SelectedIndex

        Me.cmb_goujons.Items.Clear()
        Me.cmb_goujons.Items.AddRange(tabLabelGoujonsAff)
        Me.cmb_goujons.SelectedIndex = 0

        Me.cmb_NbRow_I1.Items.Clear()
        Me.cmb_NbRow_I2.Items.Clear()
        Me.cmb_NbRow_I3.Items.Clear()

        For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
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

            For i As Integer = Nb_Ondes_MIN To Nb_Ondes_MAX
                Me.cmb_EspLongi_I1.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I2.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I3.Items.Add(i & " " & strRib)
            Next

            Me.cmb_EspLongi_I1.SelectedIndex = 0
            Me.cmb_EspLongi_I2.SelectedIndex = 0
            Me.cmb_EspLongi_I3.SelectedIndex = 0
        End If

    End Sub

    Private Sub PrepareFlechesNavigation()

        Me.btn_Suivant.Visible = (NbTravees > 1)
        Me.btn_Precedent.Visible = (NbTravees > 1)

        If NbTravees = 1 Then
            Me.cmb_Travee.Location = New Point(0, Me.cmb_Travee.Location.Y)
        Else
            Me.cmb_Travee.Location = New Point(27, Me.cmb_Travee.Location.Y)
        End If

    End Sub

    Private Sub MAJI_BtnNavigation()
        If (NbTravees > 1) Then
            If Me.cmb_Travee.SelectedIndex = 0 Then
                Me.btn_Precedent.Image = imgList_Navigation.Images("PrecedentNonDispo")
            Else
                Me.btn_Precedent.Image = imgList_Navigation.Images("Precedent")
            End If
            If Me.cmb_Travee.SelectedIndex = NbTravees - 1 Then
                Me.btn_Suivant.Image = imgList_Navigation.Images("SuivantNonDispo")
            Else
                Me.btn_Suivant.Image = imgList_Navigation.Images("Suivant")
            End If
        End If
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

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
        With myBeamLoc

            Me.chk_AutomaticDesign.Checked = .lAutomaticDesign

            If .NombreZones(traveeEnCours) >= 1 Then
                Me.txt_Largeur_I1.Text = GetStringInUnit(.LongueurZone(traveeEnCours, 0), Enu_TypeVariable.Longueur, 4, 2, False)
                Me.cmb_NbRow_I1.SelectedIndex = .NrTransZone(traveeEnCours, 0) - 1

                If Not lBacTransv Then
                    Me.txt_EspLongi_I1.Text = GetStringInUnit(.EspacementZone(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
                Else
                    Me.cmb_EspLongi_I1.SelectedIndex = .Espacement_Bac_TransZone(traveeEnCours, 0) - 1
                End If
            End If

            If .NombreZones(traveeEnCours) >= 2 Then
                Me.txt_Largeur_I2.Text = GetStringInUnit(.LongueurZone(traveeEnCours, 1), Enu_TypeVariable.Longueur, 4, 2, False)
                Me.cmb_NbRow_I2.SelectedIndex = .NrTransZone(traveeEnCours, 1) - 1

                If Not lBacTransv Then
                    Me.txt_EspLongi_I2.Text = GetStringInUnit(.EspacementZone(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 0, False)
                Else
                    Me.cmb_EspLongi_I2.SelectedIndex = .Espacement_Bac_TransZone(traveeEnCours, 1) - 1
                End If
            End If

            If .NombreZones(traveeEnCours) >= 3 Then
                Me.txt_Largeur_I3.Text = GetStringInUnit(.LongueurZone(traveeEnCours, 2), Enu_TypeVariable.Longueur, 4, 2, False)
                Me.cmb_NbRow_I3.SelectedIndex = .NrTransZone(traveeEnCours, 2) - 1

                If Not lBacTransv Then
                    Me.txt_EspLongi_I3.Text = GetStringInUnit(.EspacementZone(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 0, False)
                Else
                    Me.cmb_EspLongi_I3.SelectedIndex = .Espacement_Bac_TransZone(traveeEnCours, 2) - 1
                End If
            End If


            'Me.cmb_NbRow_I1.SelectedItem = .NrTransZone(traveeEnCours, 0)
            'Me.cmb_NbRow_I2.SelectedItem = .NrTransZone(traveeEnCours, 1)
            'Me.cmb_NbRow_I3.SelectedItem = .NrTransZone(traveeEnCours, 2)

            'If lBacTransv Then
            '    Me.cmb_EspLongi_I1.SelectedItem = .Espacement_Bac_Trans(traveeEnCours, 0)
            '    Me.cmb_EspLongi_I2.SelectedItem = .Espacement_Bac_Trans(traveeEnCours, 1)
            '    Me.cmb_EspLongi_I3.SelectedItem = .Espacement_Bac_Trans(traveeEnCours, 2)
            'Else
            '    Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
            '    Me.txt_EspLongi_I2.Text = GetStringInUnit(.Espacement(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 0, False)
            '    Me.txt_EspLongi_I3.Text = GetStringInUnit(.Espacement(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 0, False)
            'End If

            'Me.etq_Somme.Text = myBeamLoc.NombreGoujonsTot(traveeEnCours) & " " & strStud
            Me.etq_Somme.Text = myBeamLoc.NombreGoujonTot(traveeEnCours) & " " & strStud

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

        '== Vérification des textbox pour la définition des zones de saisie
        'Vérification des textbox au cas où
        Dim list_txtbox As New List(Of TextBox)
        list_txtbox.Add(Me.txt_hsc)
        list_txtbox.Add(Me.txt_d)
        'list_txtbox.Add(Me.txt_fy)
        'list_txtbox.Add(Me.txt_fu)

        If myBeamLoc.NombreZones(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_Largeur_I1)
        If myBeamLoc.NombreZones(traveeEnCours) >= 2 Then list_txtbox.Add(Me.txt_Largeur_I2)
        If myBeamLoc.NombreZones(traveeEnCours) >= 3 Then list_txtbox.Add(Me.txt_Largeur_I3)

        If Not lBacTransv Then
            If myBeamLoc.NombreZones(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_EspLongi_I1)
            If myBeamLoc.NombreZones(traveeEnCours) >= 2 Then list_txtbox.Add(Me.txt_EspLongi_I2)
            If myBeamLoc.NombreZones(traveeEnCours) >= 3 Then list_txtbox.Add(Me.txt_EspLongi_I3)
        End If

        Dim ValeurUI As Decimal

        For Each txtbox_loc As TextBox In list_txtbox
            VerificationSaisie(txtbox_loc, ValeurUI, True)
            If Not ErrorProvider_Frm_Connection.GetError(txtbox_loc) = String.Empty Then
                lFrm_Valide = False
                Exit For
            End If
        Next

        '== Vérification de l'enrobage de béton

        Dim Cc, CcMin As Decimal
        Dim Chaine As String = ""
        Dim Reference As String

        Cc = myBeamLoc.Dalle.zTop - myBeamLoc.Dalle.Goujons.hsc

        CcMin = Goujons_EnrobageMini()

        If IsSmaller(Cc, CcMin) And IsGreaterOrEqual(Cc, 0) Then

            Reference = Goujons_ReferenceEnrobageMini() & " (" & GetStringInUnitN(CcMin, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiNdC, True) & ")"
            Chaine = RemplaceDollar(strWarningGoujons, GetStringInUnitN(Cc, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiNdC, True))
            Chaine = RemplaceDollar(Chaine, Reference)

            Dim Rep As MsgBoxResult

            Rep = MsgBox(Chaine, MsgBoxStyle.OkCancel, LogicielInfo.Racine)

            lFrm_Valide = (Rep = MsgBoxResult.Ok)

        End If

        Return lFrm_Valide
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        With MyProjet.Poutres(MyProjet.IndEnCours)

            lModif = False

            GereTransfertValeur(myBeamLoc.lAutomaticDesign, .lAutomaticDesign, lModif)

            If Not Me.chk_AutomaticDesign.Checked Then

                GereTransfertValeur(myBeamLoc.Dalle.Goujons.nom, .Dalle.Goujons.nom, lModif)
                GereTransfertValeur(myBeamLoc.Dalle.Goujons.hsc, .Dalle.Goujons.hsc, lModif)
                GereTransfertValeur(myBeamLoc.Dalle.Goujons.d, .Dalle.Goujons.d, lModif)
                GereTransfertValeur(myBeamLoc.Dalle.Goujons.Fy, .Dalle.Goujons.Fy, lModif)
                GereTransfertValeur(myBeamLoc.Dalle.Goujons.Fu, .Dalle.Goujons.Fu, lModif)

                For i As Integer = .IndicePremiereTravee To .IndiceDerniereTravee

                    GereTransfertValeur(myBeamLoc.NombreZones(i), .NombreZones(i), lModif)
                    'GereTransfertValeur(myBeamLoc.NombreGoujonsTot(i), .NombreGoujonsTot(i), lModif)

                    For j As Integer = 0 To .NombreZones(i) - 1

                        GereTransfertValeur(myBeamLoc.LongueurZone(i, j), .LongueurZone(i, j), lModif)
                        GereTransfertValeur(myBeamLoc.NrTransZone(i, j), .NrTransZone(i, j), lModif)

                        If lBacTransv Then
                            GereTransfertValeur(myBeamLoc.Espacement_Bac_TransZone(i, j), .Espacement_Bac_TransZone(i, j), lModif)
                            .EspacementZone(i, j) = .Espacement_Bac_TransZone(i, j) * .Dalle.Bac.Ep

                        Else
                            GereTransfertValeur(myBeamLoc.EspacementZone(i, j), .EspacementZone(i, j), lModif)
                        End If
                    Next

                Next

            End If
        End With

    End Sub


    'Private Sub TransfertSaisie(ByRef lModif As Boolean)
    '    With MyProjet.Poutres(MyProjet.IndEnCours)

    '        If .lAutomaticDesign <> myBeamLoc.lAutomaticDesign Then
    '            lModif = True
    '            .lAutomaticDesign = myBeamLoc.lAutomaticDesign
    '        End If

    '        If Not Me.chk_AutomaticDesign.Checked Then

    '            If .Dalle.Connecteur.nom <> myBeamLoc.Dalle.Connecteur.nom Then
    '                lModif = True
    '                .Dalle.Connecteur.nom = myBeamLoc.Dalle.Connecteur.nom
    '                .Dalle.Connecteur.Caracteristiques_Goujons()
    '            End If

    '            For i As Integer = .IndicePremiereTravee To .IndiceDerniereTravee

    '                If .NombreZone(i) <> myBeamLoc.NombreZone(i) Then
    '                    lModif = True
    '                    .NombreZone(i) = myBeamLoc.NombreZone(i)
    '                End If

    '                If .NombreGoujonsTot(i) <> myBeamLoc.NombreGoujonsTot(i) Then
    '                    lModif = True
    '                    .NombreGoujonsTot(i) = myBeamLoc.NombreGoujonsTot(i)
    '                End If

    '                For j As Integer = 0 To .NombreZone(i) - 1
    '                    If .Longueur_Zone(i, j) <> myBeamLoc.Longueur_Zone(i, j) Then
    '                        lModif = True
    '                        .Longueur_Zone(i, j) = myBeamLoc.Longueur_Zone(i, j)
    '                    End If

    '                    If .NrTransZone(i, j) <> myBeamLoc.NrTransZone(i, j) Then
    '                        lModif = True
    '                        .NrTransZone(i, j) = myBeamLoc.NrTransZone(i, j)
    '                    End If

    '                    If lBacTransv Then
    '                        If .Espacement_Bac_Trans(i, j) <> myBeamLoc.Espacement_Bac_Trans(i, j) Then
    '                            lModif = True
    '                            .Espacement_Bac_Trans(i, j) = myBeamLoc.Espacement_Bac_Trans(i, j)
    '                            .Espacement(i, j) = .Espacement_Bac_Trans(i, j) * .Dalle.Bac.Ep
    '                        End If
    '                    Else
    '                        If .Espacement(i, j) <> myBeamLoc.Espacement(i, j) Then
    '                            lModif = True
    '                            .Espacement(i, j) = myBeamLoc.Espacement(i, j)
    '                        End If
    '                    End If
    '                Next

    '            Next

    '        End If
    '    End With


    'End Sub


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
        DessinFrmConnection_Connection(e.Graphics, myBeamLoc, FontFrm, Me.img_Connection.ClientRectangle.Width, Me.img_Connection.ClientRectangle.Height, 1, traveeEnCours, Not myBeamLoc.lAutomaticDesign, strStud)
    End Sub

    Private Sub DessinConnecteurs(sender As Object, e As PaintEventArgs) Handles img_Stud.Paint
        DessinFrmConnection_Connecteurs(e.Graphics, Me.img_Stud.ClientRectangle.Width, Me.img_Stud.ClientRectangle.Height, 1, myBeamLoc)
    End Sub

#End Region

#Region " Evènements "
    ''' <summary>
    ''' Met à jour les valeurs limites en fonction des données renseignées
    ''' </summary>
    Private Sub MAJ_Valeurs_Limites()

        '--( Déclaration

        Dim lMixte As Boolean
        Dim lCofraPlus220 As Boolean
        Dim zTop As Decimal
        Dim lPerpend As Boolean
        Dim lNervureC As Boolean

        '--( Initialisation

        lMixte = myBeamLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte
        lPerpend = (myBeamLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)
        lNervureC = Not (myBeamLoc.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu)
        lCofraPlus220 = myBeamLoc.Dalle.Bac.lCofraplus220
        zTop = myBeamLoc.Dalle.zTop

        'Définition des valeurs limites pour les caractéristiques des goujons
        '--( Hauteur mini des goujons
        If myBeamLoc.Param.lGeneration1 Then
            Hauteur_Goujon_MIN = GOUJ_RAPHsurDMIN_G1 * myBeamLoc.Dalle.Goujons.d
        Else
            Hauteur_Goujon_MIN = GOUJ_RAPHsurDMIN_G2 * myBeamLoc.Dalle.Goujons.d
        End If
        If lMixte And (Not lCofraPlus220) Then
            Hauteur_Goujon_MIN = Math.Max(Hauteur_Goujon_MIN, myBeamLoc.Dalle.Bac.Hp + GOUJ_RAPHsurDSURBAC * myBeamLoc.Dalle.Goujons.d)
        End If

        '--( Hauteur maxi du goujon

        Hauteur_Goujon_MAX_CONSEILLEE = zTop - ENROBAGEMIN
        Hauteur_Goujon_MAX = zTop

        Diametre_Goujon_MIN = GOUJ_DMIN
        Diametre_Goujon_MAX = 0

        If lMixte And lPerpend And lNervureC And Not lCofraPlus220 Then
            If myBeamLoc.Dalle.Bac.lPreperce Then
                Diametre_Goujon_MAX = GOUJ_DMAXPERPREP
            Else
                Diametre_Goujon_MAX = GOUJ_DMAXPERWT
            End If
        Else
            Diametre_Goujon_MAX = GOUJ_DMAXDEF
        End If

        'Définition des valeurs limites pour les caractéristiques longitudinales
        Longueur_Zone_MIN = Math.Min(1, myBeamLoc.LongueurTravee(traveeEnCours))
        Longueur_Zone_MAX = myBeamLoc.LongueurTravee(traveeEnCours)
        Nb_Zones_MIN = 1

        If traveeEnCours = 0 Or traveeEnCours = myBeamLoc.IndiceTraveeConsoleDroite Then
            Nb_Zones_MAX = 1 'dans le cas de consoles, on impose une seule zone de connection 
        Else 'cas d'une travée courante
            Nb_Zones_MAX = Math.Min(Math.Floor(myBeamLoc.LongueurTravee(traveeEnCours) / Longueur_Zone_MIN), 3)
        End If

        Espacement_Longi_MIN = GOUJ_RAPESPXsurD_MIN * myBeamLoc.Dalle.Goujons.d
        Espacement_Longi_MAX = Math.Min(GOUJ_RAPESPX, GOUJ_RAPESPXsurTD_MAX * myBeamLoc.Dalle.Ep_td)
        If myBeamLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And myBeamLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
            Nb_Ondes_MIN = 1
            Nb_Ondes_MAX = Math.Floor(Espacement_Longi_MAX / myBeamLoc.Dalle.Bac.Ep)
        End If

        'Définition des valeurs limites pour les caractéristiques transversales

        Pince_Trans_MIN = GOUJ_DBORD_MIN
        If myBeamLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            Espacement_Trans_MIN = GOUJ_RAPESPYsurD_MIXTE_MIN * myBeamLoc.Dalle.Goujons.d
        Else 'dalle pleine ou préfa
            Espacement_Trans_MIN = GOUJ_RAPESPYsurD_PLEINE_MIN * myBeamLoc.Dalle.Goujons.d
        End If
        'b_app_min = OptionsSlimFloor.bappmin
        b_app_min = BAC_LARGAPP_MIN
        Nb_TransV_Row_MIN = 1
        If myBeamLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte And myBeamLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then
            If myBeamLoc.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu Then
                Nb_TransV_Row_MAX = Math.Floor((myBeamLoc.Section.ProfilA.Bfs - 2 * b_app_min - 2 * Pince_Trans_MIN - myBeamLoc.Dalle.Goujons.d) / Espacement_Trans_MIN + 1)
            Else
                Nb_TransV_Row_MAX = Math.Min(2, Math.Floor((myBeamLoc.Section.ProfilA.Bfs - 2 * Pince_Trans_MIN - myBeamLoc.Dalle.Goujons.d) / Espacement_Trans_MIN + 1))
            End If
        Else
            Nb_TransV_Row_MAX = Math.Floor((myBeamLoc.Section.ProfilA.Bfs - 2 * Pince_Trans_MIN - myBeamLoc.Dalle.Goujons.d) / Espacement_Trans_MIN + 1)
        End If

        Nb_TransV_Row_MAX = Math.Max(1, Nb_TransV_Row_MAX)
    End Sub

    ''' <summary>
    ''' Met à jour la fenêtre lorsque l'option automatic design est sélectionnée
    ''' </summary>
    Private Sub MAJ_AutomaticDesign()
        With myBeamLoc

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
                Me.btn_Ajouter.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MAX
                Me.btn_Supprimer.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MIN
            End If

            Me.etq_Somme.Visible = Not .lAutomaticDesign

            Me.img_Connection.Invalidate()

        End With
    End Sub

    ''' <summary>
    ''' MAJ des textboxs et comboboxs dans la zone des connecteurs
    ''' </summary>
    Private Sub MAJ_affichage_txt_connecteurs()
        Const NON As Enu_AfficheUnite = Enu_AfficheUnite.Non
        Me.cmb_goujons.SelectedIndex = Array.IndexOf(tabLabelGoujons, myBeamLoc.Dalle.Goujons.nom)
        Me.txt_hsc.Text = GetStringInUnitN(myBeamLoc.Dalle.Goujons.hsc, Enu_TypeVariable.Dimension, 4, 3, NON, True)
        Me.txt_d.Text = GetStringInUnitN(myBeamLoc.Dalle.Goujons.d, Enu_TypeVariable.Dimension, 4, 3, NON, True)
        Me.txt_fy.Text = GetStringInUnitN(myBeamLoc.Dalle.Goujons.Fy, Enu_TypeVariable.Contrainte, 4, 3, NON, True)
        Me.txt_fu.Text = GetStringInUnitN(myBeamLoc.Dalle.Goujons.Fu, Enu_TypeVariable.Contrainte, 4, 3, NON, True)
    End Sub

    ''' <summary>
    ''' MAJ des textboxs et combobox dans la zone de connection
    ''' </summary>
    Private Sub MAJ_affichage_txt_cmb_connection()
        lMAJAffichage = True

        'Met à jours la visibilité des txtbox

        Me.txt_Largeur_I1.ReadOnly = myBeamLoc.NombreZones(traveeEnCours) = 1

        Me.txt_I2.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 2
        Me.txt_Largeur_I2.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 2
        Me.cmb_NbRow_I2.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 2
        Me.cmb_EspLongi_I2.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 2 And lBacTransv
        Me.txt_EspLongi_I2.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 2 And Not lBacTransv

        Me.txt_I3.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 3
        Me.txt_Largeur_I3.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 3
        Me.cmb_NbRow_I3.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 3
        Me.cmb_EspLongi_I3.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 3 And lBacTransv
        Me.txt_EspLongi_I3.Visible = myBeamLoc.NombreZones(traveeEnCours) >= 3 And Not lBacTransv

        'Met à jour les valeurs dans les txtbox ou cmbbox 

        If Not ltxt_Largeur_I1Enter Or lBtnAjouterSupprimer Then Me.txt_Largeur_I1.Text = GetStringInUnit(myBeamLoc.LongueurZone(traveeEnCours, 0), Enu_TypeVariable.Longueur, 4, 2, False)
        If Not ltxt_Largeur_I2Enter Or lBtnAjouterSupprimer Then Me.txt_Largeur_I2.Text = GetStringInUnit(myBeamLoc.LongueurZone(traveeEnCours, 1), Enu_TypeVariable.Longueur, 4, 2, False)
        If Not ltxt_Largeur_I3Enter Or lBtnAjouterSupprimer Then Me.txt_Largeur_I3.Text = GetStringInUnit(myBeamLoc.LongueurZone(traveeEnCours, 2), Enu_TypeVariable.Longueur, 4, 2, False)

        Me.cmb_NbRow_I1.SelectedIndex = myBeamLoc.NrTransZone(traveeEnCours, 0) - 1
        Me.cmb_NbRow_I2.SelectedIndex = myBeamLoc.NrTransZone(traveeEnCours, 1) - 1
        Me.cmb_NbRow_I3.SelectedIndex = myBeamLoc.NrTransZone(traveeEnCours, 2) - 1

        If lBacTransv Then
            Me.cmb_EspLongi_I1.SelectedIndex = myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 0) - 1
            Me.cmb_EspLongi_I2.SelectedIndex = myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 1) - 1
            Me.cmb_EspLongi_I3.SelectedIndex = myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 2) - 1
        Else
            Me.txt_EspLongi_I1.Text = GetStringInUnit(myBeamLoc.EspacementZone(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 0, False)
            Me.txt_EspLongi_I2.Text = GetStringInUnit(myBeamLoc.EspacementZone(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 0, False)
            Me.txt_EspLongi_I3.Text = GetStringInUnit(myBeamLoc.EspacementZone(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 0, False)
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

    Private Sub GestionNavigation(sender As Object, e As EventArgs) Handles btn_Suivant.Click, btn_Precedent.Click
        Dim Index As Integer = Me.cmb_Travee.SelectedIndex
        Select Case sender.name
            Case Me.btn_Precedent.Name
                Me.cmb_Travee.SelectedIndex = Math.Max(0, Index - 1)
            Case Me.btn_Suivant.Name
                Me.cmb_Travee.SelectedIndex = Math.Min(NbTravees - 1, Index + 1)
        End Select
        MAJI_BtnNavigation()
    End Sub


    Private Sub btn_Ajouter_Click(sender As Object, e As EventArgs) Handles btn_Ajouter.Click
        If lBuild Then Exit Sub

        lBtnAjouterSupprimer = True

        If myBeamLoc.NombreZones(traveeEnCours) <= Nb_Zones_MAX - 1 Then myBeamLoc.NombreZones(traveeEnCours) += 1
        Me.btn_Ajouter.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MAX
        Me.btn_Supprimer.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MIN
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

        ValideSaisieFenetre()

        lBtnAjouterSupprimer = False
    End Sub

    Private Sub btn_Supprimer_Click(sender As Object, e As EventArgs) Handles btn_Supprimer.Click
        If lBuild Then Exit Sub

        lBtnAjouterSupprimer = True

        If myBeamLoc.NombreZones(traveeEnCours) >= Nb_Zones_MIN + 1 Then myBeamLoc.NombreZones(traveeEnCours) -= 1
        Me.btn_Ajouter.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MAX
        Me.btn_Supprimer.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MIN
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()

        ValideSaisieFenetre()

        lBtnAjouterSupprimer = False
    End Sub

    Sub MAJ_Nb_Zone()

        'MAJ des longueurs de zone suite à un clique Ajouter ou Supprimer
        If lBuild Then Exit Sub
        Select Case myBeamLoc.NombreZones(traveeEnCours)
            Case 1
                myBeamLoc.LongueurZone(traveeEnCours, 0) = myBeamLoc.LongueurTravee(traveeEnCours)
                myBeamLoc.LongueurZone(traveeEnCours, 1) = 0
                myBeamLoc.LongueurZone(traveeEnCours, 2) = 0
            Case 2
                myBeamLoc.LongueurZone(traveeEnCours, 0) = myBeamLoc.LongueurTravee(traveeEnCours) / 2
                myBeamLoc.LongueurZone(traveeEnCours, 1) = myBeamLoc.LongueurTravee(traveeEnCours) / 2
                myBeamLoc.LongueurZone(traveeEnCours, 2) = 0
            Case 3
                myBeamLoc.LongueurZone(traveeEnCours, 0) = myBeamLoc.LongueurTravee(traveeEnCours) / 3
                myBeamLoc.LongueurZone(traveeEnCours, 1) = myBeamLoc.LongueurTravee(traveeEnCours) / 3
                myBeamLoc.LongueurZone(traveeEnCours, 2) = myBeamLoc.LongueurTravee(traveeEnCours) / 3
        End Select


    End Sub

    Private Sub MAJ_SommeGoujons()
        'MAJ du calcul de la somme des goujons après les modifications des valeurs


        Me.etq_Somme.Text = myBeamLoc.NombreGoujonTot(traveeEnCours) & " " & strStud

            Me.img_Connection.Invalidate()

            MAJ_DegreConnexion()

    End Sub

    Private Sub MAJ_DegreConnexion()
        '-----------------------------------------------------------------------------------------
        '   24/07/25 : Création - V1.20 - POM
        '-----------------------------------------------------------------------------------------
        '   Mis à jour du degré de connexion à mi portée de la poutre
        '-----------------------------------------------------------------------------------------

        Dim Eta As Decimal

        If Not lMultiSpan Then
            Eta = DegreeConnexionMiTravee()

            Me.lbl_DegreConnex.Text = "= " & GetStringInUnitN(Eta, Enu_TypeVariable.SansType, 4, 3, Enu_AfficheUnite.Non, True)
        End If

    End Sub

    Private Function DegreeConnexionMiTravee() As Decimal
        '-----------------------------------------------------------------------------------------
        '   24/07/25 : Création - V1.20 - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul du degré de connexion à mi portée de la poutre en cours
        '   Calcul pour la travée 0, supposée entièrement en M>0
        '-----------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Eta As Decimal
        Dim RConnexG, RConnexD, NConnex As Decimal
        Dim bEff, NDalle As Decimal
        Const iTravee As Integer = 1
        Const lSimple As Boolean = False
        Dim GammaC As Decimal = myBeamLoc.Param.Gamma.GammaC
        Dim GammaS As Decimal = myBeamLoc.Param.Gamma.GammaS
        Dim GammaM0 As Decimal = myBeamLoc.Param.Gamma.GammaM0
        Dim NProfile, NArmaEnrobage, NEnrobage As Decimal
        Dim LTravee As Decimal = myBeamLoc.LongueurTravee(iTravee)
        Dim xPosT As Decimal = LTravee / 2

        '--( Initialisation

        NProfile = myBeamLoc.Section.ResistanceTractionProfile(gammaM0)
        If myBeamLoc.Section.lEnrobage Then
            NEnrobage = myBeamLoc.Section.NResistanceCompressionEnrobage(GammaC)
            NArmaEnrobage = myBeamLoc.Section.NResistanceArmaturesEnrobage(GammaS)
        End If

        '--( Calculs

        bEff = myBeamLoc.BeffDalle(xPosT, iTravee, lSimple, False)
        NDalle = myBeamLoc.Dalle.NResistanceCompressionDalle(bEff, GammaC)
        NConnex = Math.Min(NDalle, NProfile + NArmaEnrobage)

        '----> Résistance de la connexion disponible à gauche et à droite
        RConnexG = myBeamLoc.RConnex(iTravee, xPosT, True)
        RConnexD = myBeamLoc.RConnex(iTravee, xPosT, False)

        '----> Degré de connexion

        Eta = Math.Min(RConnexG, RConnexD) / NConnex

        Return Eta
    End Function


    Private Sub txt_Largeur_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_Largeur_I1.TextChanged, txt_Largeur_I2.TextChanged, txt_Largeur_I3.TextChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

            Select Case sender.name
                Case txt_Largeur_I1.Name
                    If Not ltxt_Largeur_I1Enter Then Exit Sub
                    myBeamLoc.LongueurZone(traveeEnCours, 0) = ValeurUI

                    Select Case myBeamLoc.NombreZones(traveeEnCours)
                        Case 1
                            myBeamLoc.LongueurZone(traveeEnCours, 1) = 0
                            myBeamLoc.LongueurZone(traveeEnCours, 2) = 0
                        Case 2
                            myBeamLoc.LongueurZone(traveeEnCours, 1) = myBeamLoc.LongueurTravee(traveeEnCours) - myBeamLoc.LongueurZone(traveeEnCours, 0)
                            myBeamLoc.LongueurZone(traveeEnCours, 2) = 0
                        Case 3
                            myBeamLoc.LongueurZone(traveeEnCours, 1) = (myBeamLoc.LongueurTravee(traveeEnCours) - myBeamLoc.LongueurZone(traveeEnCours, 0)) / 2
                            myBeamLoc.LongueurZone(traveeEnCours, 2) = (myBeamLoc.LongueurTravee(traveeEnCours) - myBeamLoc.LongueurZone(traveeEnCours, 0)) / 2
                    End Select

                Case txt_Largeur_I2.Name
                    If Not ltxt_Largeur_I2Enter Then Exit Sub
                    myBeamLoc.LongueurZone(traveeEnCours, 1) = ValeurUI

                    If myBeamLoc.NombreZones(traveeEnCours) = 2 Then
                        myBeamLoc.LongueurZone(traveeEnCours, 0) = myBeamLoc.LongueurTravee(traveeEnCours) - myBeamLoc.LongueurZone(traveeEnCours, 1)
                        myBeamLoc.LongueurZone(traveeEnCours, 2) = 0
                    Else '3 zones
                        myBeamLoc.LongueurZone(traveeEnCours, 2) = myBeamLoc.LongueurTravee(traveeEnCours) - myBeamLoc.LongueurZone(traveeEnCours, 0) - myBeamLoc.LongueurZone(traveeEnCours, 1)
                    End If

                Case txt_Largeur_I3.Name
                    If Not ltxt_Largeur_I3Enter Then Exit Sub
                    myBeamLoc.LongueurZone(traveeEnCours, 2) = ValeurUI
                    myBeamLoc.LongueurZone(traveeEnCours, 0) = myBeamLoc.LongueurTravee(traveeEnCours) - myBeamLoc.LongueurZone(traveeEnCours, 1) - myBeamLoc.LongueurZone(traveeEnCours, 2)
            End Select
            MAJ_SommeGoujons()
            MAJ_affichage_txt_cmb_connection()

        End If
    End Sub

    Private Sub cmb_NbRow_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbRow_I1.SelectedIndexChanged, cmb_NbRow_I2.SelectedIndexChanged, cmb_NbRow_I3.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Select Case sender.name
            Case cmb_NbRow_I1.Name
                myBeamLoc.NrTransZone(traveeEnCours, 0) = cmb_NbRow_I1.SelectedIndex + 1
            Case cmb_NbRow_I2.Name
                myBeamLoc.NrTransZone(traveeEnCours, 1) = cmb_NbRow_I2.SelectedIndex + 1
            Case cmb_NbRow_I3.Name
                myBeamLoc.NrTransZone(traveeEnCours, 2) = cmb_NbRow_I3.SelectedIndex + 1
        End Select

        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb_connection()
    End Sub



    Private Sub cmb_EspLongi_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_EspLongi_I1.SelectedIndexChanged, cmb_EspLongi_I2.SelectedIndexChanged, cmb_EspLongi_I3.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub
        Select Case sender.name
            Case cmb_EspLongi_I1.Name
                myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 0) = cmb_EspLongi_I1.SelectedIndex + 1
                myBeamLoc.EspacementZone(traveeEnCours, 0) = myBeamLoc.Esp_longi_bac * myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 0)
            Case cmb_EspLongi_I2.Name
                myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 1) = cmb_EspLongi_I2.SelectedIndex + 1
                myBeamLoc.EspacementZone(traveeEnCours, 1) = myBeamLoc.Esp_longi_bac * myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 1)
            Case cmb_EspLongi_I3.Name
                myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 2) = cmb_EspLongi_I3.SelectedIndex + 1
                myBeamLoc.EspacementZone(traveeEnCours, 2) = myBeamLoc.Esp_longi_bac * myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 2)
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
                    myBeamLoc.EspacementZone(traveeEnCours, 0) = ValeurUI
                Case txt_EspLongi_I2.Name
                    myBeamLoc.EspacementZone(traveeEnCours, 1) = ValeurUI
                Case txt_EspLongi_I3.Name
                    myBeamLoc.EspacementZone(traveeEnCours, 2) = ValeurUI
            End Select
            MAJ_SommeGoujons()

        End If
    End Sub

    Private Sub VerifDiametreGoujons(DiaG As Decimal)
        '---------------------------------------------------------------------------------------------------------------------------------
        '   03/03/25 :  Création - POM
        '---------------------------------------------------------------------------------------------------------------------------------
        '   On vérifie que le diamètre du goujon sélectionné est compatible avec le domaine d'application
        '---------------------------------------------------------------------------------------------------------------------------------
        '   DiaG        [E] :   Diamètre du goujon selectionné
        '---------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lScope As Boolean = True
        Dim DiaMin As Decimal
        Dim DiaMax As Decimal

        Const DIAMIN_PREPERCE As Decimal = 19 / 1000
        Const DIAMAX_PREPERCE As Decimal = 22 / 1000
        Const DIAMAX_SOUDEAT As Decimal = 20 / 1000

        Dim TextError As String = ""

        '--( Traitement des dalles mixtes continues perpendiculaires

        If myBeamLoc.Dalle.lMixte Then

            If myBeamLoc.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire Then

                If myBeamLoc.Dalle.Bac.AppuiT <> cls_Bac.EnuConfigTAppui.Discontinu Then

                    If myBeamLoc.Dalle.Bac.lPreperce Then

                        DiaMin = DIAMIN_PREPERCE
                        DiaMax = DIAMAX_PREPERCE
                        TextError = strErrorDia(0)

                    Else

                        DiaMin = 0
                        DiaMax = DIAMAX_SOUDEAT
                        TextError = strErrorDia(1)

                    End If

                    If IsSmaller(DiaG, DiaMin) Then lScope = False
                    If IsGreater(DiaG, DiaMax) Then lScope = False

                End If

            End If

        End If

        If lScope Then

            ErrorProvider_Frm_Connection.SetError(cmb_goujons, "")

        Else

            ErrorProvider_Frm_Connection.SetError(cmb_goujons, TextError)

        End If


    End Sub

    Private Sub cmb_goujons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_goujons.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim iStud As Integer = cmb_goujons.SelectedIndex
        myBeamLoc.Dalle.Goujons.nom = tabLabelGoujons(iStud)
        'myBeamLoc.Dalle.Connecteur.Caracteristiques_Goujons()
        myBeamLoc.Dalle.Goujons.hsc = BaseGoujons(iStud).hsc
        myBeamLoc.Dalle.Goujons.d = BaseGoujons(iStud).d
        myBeamLoc.Dalle.Goujons.Fy = BaseGoujons(iStud).Fy
        myBeamLoc.Dalle.Goujons.Fu = BaseGoujons(iStud).Fu

        MAJ_affichage_txt_connecteurs()
        img_Stud.Invalidate()

        MAJ_Valeurs_Limites()


        VerifDiametreGoujons(myBeamLoc.Dalle.Goujons.d)

        '=====

        'Lorsqu'on modifie le diamètre des goujons, les valeurs limites de hsc, sx et sy changent. On doit corriger les valeurs de certaines variables si nécessaire (utile en cas d'un changement de certaines valeurs dans les fenêtres précédentes)

        Dim ValeurUI As Decimal
        ValeurUI = Me.txt_hsc.Text
        VerificationSaisie(Me.txt_hsc, ValeurUI, False) 'Vérification de la hauteur du goujon

        Dim lMAJ_cmb_NbRow As Boolean = False
        Dim lMAJ_cmb_EspLongi As Boolean = False


        For i As Integer = myBeamLoc.IndicePremiereTravee To myBeamLoc.IndiceDerniereTravee
            For j As Integer = 0 To 2
                If Not (myBeamLoc.NrTransZone(i, j) >= Nb_TransV_Row_MIN And myBeamLoc.NrTransZone(i, j) <= Nb_TransV_Row_MAX) Then
                    myBeamLoc.NrTransZone(i, j) = Nb_TransV_Row_MIN
                    lMAJ_cmb_NbRow = True
                End If

                If lBacTransv Then
                    If Not (myBeamLoc.Espacement_Bac_TransZone(i, j) >= Nb_Ondes_MIN And myBeamLoc.Espacement_Bac_TransZone(i, j) <= Nb_Ondes_MAX) Then
                        myBeamLoc.Espacement_Bac_TransZone(i, j) = Nb_Ondes_MIN
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

            For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
                Me.cmb_NbRow_I1.Items.Add(i)
                Me.cmb_NbRow_I2.Items.Add(i)
                Me.cmb_NbRow_I3.Items.Add(i)
            Next

            Me.cmb_NbRow_I1.SelectedIndex = myBeamLoc.NrTransZone(traveeEnCours, 0) - 1
            Me.cmb_NbRow_I2.SelectedIndex = myBeamLoc.NrTransZone(traveeEnCours, 1) - 1
            Me.cmb_NbRow_I3.SelectedIndex = myBeamLoc.NrTransZone(traveeEnCours, 2) - 1
        End If

        If lMAJ_cmb_EspLongi Then
            Me.cmb_EspLongi_I1.Items.Clear()
            Me.cmb_EspLongi_I2.Items.Clear()
            Me.cmb_EspLongi_I3.Items.Clear()

            For i As Integer = Nb_Ondes_MIN To Nb_Ondes_MAX
                Me.cmb_EspLongi_I1.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I2.Items.Add(i & " " & strRib)
                Me.cmb_EspLongi_I3.Items.Add(i & " " & strRib)
            Next

            Me.cmb_EspLongi_I1.SelectedIndex = myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 0) - 1
            Me.cmb_EspLongi_I2.SelectedIndex = myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 1) - 1
            Me.cmb_EspLongi_I3.SelectedIndex = myBeamLoc.Espacement_Bac_TransZone(traveeEnCours, 2) - 1
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
                    traveeEnCours = myBeamLoc.IndiceTraveeConsoleDroite
            End Select

            'Réinitialise les boutons Ajouter/Supprimer

            MAJ_Valeurs_Limites()

            Me.btn_Ajouter.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MAX
            Me.btn_Supprimer.Enabled = Not myBeamLoc.NombreZones(traveeEnCours) = Nb_Zones_MIN

            MAJ_SommeGoujons()
            MAJ_affichage_txt_cmb_connection()
            MAJI_BtnNavigation()

        Else

            If Not cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee Then MsgBox(WarningMessage_CmbTravee)
            cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee

        End If

    End Sub

    Private Sub chk_AutomaticDesign_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AutomaticDesign.CheckedChanged
        If lBuild Then Exit Sub
        myBeamLoc.lAutomaticDesign = chk_AutomaticDesign.Checked
        MAJ_AutomaticDesign()
    End Sub


#End Region

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

                ValMin = Math.Round(Hauteur_Goujon_MIN, 3) / kUnit
                ValMaxConseillee = Hauteur_Goujon_MAX_CONSEILLEE / kUnit
                lValMaxConseillee = True
                ValMax = Hauteur_Goujon_MAX / kUnit

            Case Me.txt_d.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = Diametre_Goujon_MIN / kUnit
                ValMax = Diametre_Goujon_MAX / kUnit

            Case Me.txt_Largeur_I1.Name, Me.txt_Largeur_I2.Name, Me.txt_Largeur_I3.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

                ValMin = Longueur_Zone_MIN / kUnit
                ValMax = Longueur_Zone_MAX / kUnit

            Case Me.txt_EspLongi_I1.Name, Me.txt_EspLongi_I2.Name, Me.txt_EspLongi_I3.Name
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




#End Region

#Region " Infos W "

    Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
        'If InfoW_lVisible Then
        '    InfoW_Fermer()
        'Else
        PublieInfoDegreConnex()
        'End If
    End Sub

    Private Sub PublieInfoDegreConnex()

        InfoW_Initialise()
        InfoW_Add(strInfoW_DegreConnex)
        'InfoW_Add("Le degré de connexion est calculé à mi-travée de la poutre, en supposant que la poutre en entièrement sous moment positif")

        InfosW_Publie()

    End Sub

#End Region

End Class