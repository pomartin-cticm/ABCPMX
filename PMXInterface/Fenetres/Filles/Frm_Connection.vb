Imports PMXMoteur2
Imports System.IO


Public Class Frm_Connection

#Region " Variables locales "

    Dim lBuild As Boolean = True

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
    ''' Définition d'une liste de string pour remplir le cmb_studs
    ''' </summary>
    Dim strGoujons() As String

    ''' <summary>
    ''' variable locale qui informe quelle travée est affichée à l'écran
    ''' 1er item: donne la nature de la travée
    ''' 2nd item: donne l'indice de la travée selectionnée
    ''' </summary>
    Dim traveeEnCours As (cls_Poutre.EnuTypeTravee, Integer) = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)

    Dim y_txt_cmb_esp_longi_actif As Decimal = 129
    Dim y_txt_cmb_esp_longi_passif As Decimal = 153

    ''' <summary>
    ''' Indique la présence d'un bac disposé transversalement (=True) ou non (=False)
    ''' </summary>
    Dim lBacTransv As Boolean

    ''' <summary>
    ''' Permet de stocker localement le mot clé associé aux goujons 
    ''' </summary>
    Dim strStud As String

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
    ''' Définition des valeurs limites pour les textbox
    ''' </summary>
    Dim LONGUEURMIN As Decimal
    Dim LONGUEURMAX As Decimal
    Dim NBTRANSVROWMIN As Integer
    Dim NBTRANSVROWMAX
    Dim ESPACEMENTMIN As Decimal
    Dim ESPACEMENTMAX As Decimal


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Connection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        RemplirComboBox()
        AfficherPoutreEnCours()
        MAJ_Nb_Zone()
        MAJ_affichage_txt_cmb()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        If MyPoutreLoc.Dalle.type = Cls_Dalle.Enum_TypeDalle.Mixte And MyPoutreLoc.Dalle.Bac.orientation = Cls_Bac.Enum_Orientation.Perpendiculaire Then
            lBacTransv = True
        Else
            lBacTransv = False
        End If

        'Par défaut on affiche la première travée sur deux appuis
        traveeEnCours.Item1 = cls_Poutre.EnuTypeTravee.DeuxAppuis
        traveeEnCours.Item2 = 1

        'Il y'a toujours au moins 1 zone 
        Me.txt_Largeur_I1.Visible = True
        Me.cmb_NbRow_I1.Visible = True
        Me.cmb_EspLongi_I1.Visible = lBacTransv
        Me.txt_EspLongi_I1.Visible = Not lBacTransv

        ltxt_Largeur_I1Enter = False
        ltxt_Largeur_I2Enter = False
        ltxt_Largeur_I3Enter = False

        lMAJAffichage = False

        LONGUEURMIN = 0.1 / 1000
        LONGUEURMAX = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2)
        NBTRANSVROWMIN = 1
        NBTRANSVROWMAX = 5
        ESPACEMENTMIN = 0.05
        ESPACEMENTMAX = 0.5
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


                '=== MENU CONNECTION ==============================================================='

                strGoujons = MyPoutreLoc.Dalle.Connecteur.Get_ListName_GoujonDatabase()

                For i As Integer = 0 To strGoujons.Length - 1
                    strGoujons(i) = Bloc("DIAMETER") & " " & strGoujons(i)
                Next

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

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitD.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitHsc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitFy.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitFu.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
    End Sub

    Private Sub RemplirComboBox()
        Me.cmb_Travee.Items.Clear()
        Me.cmb_Travee.Items.AddRange(strTypeTravee)
        Me.cmb_Travee.SelectedIndex = 0

        Me.cmb_goujons.Items.Clear()
        Me.cmb_goujons.Items.AddRange(strGoujons)
        Me.cmb_goujons.SelectedIndex = 0

        Me.cmb_NbRow_I1.Items.Clear()
        Me.cmb_NbRow_I2.Items.Clear()
        Me.cmb_NbRow_I3.Items.Clear()

        For i As Integer = NBTRANSVROWMIN To NBTRANSVROWMAX
            Me.cmb_NbRow_I1.Items.Add(i)
            Me.cmb_NbRow_I2.Items.Add(i)
            Me.cmb_NbRow_I3.Items.Add(i)
        Next

        Me.cmb_EspLongi_I1.Items.Clear()
        Me.cmb_EspLongi_I1.Items.Add(1 & " " & strRib)
        Me.cmb_EspLongi_I1.Items.Add(2 & " " & strRibs)
        Me.cmb_EspLongi_I1.Items.Add(3 & " " & strRibs)
        Me.cmb_EspLongi_I1.Items.Add(4 & " " & strRibs)

        Me.cmb_EspLongi_I2.Items.Clear()
        Me.cmb_EspLongi_I2.Items.Add(1 & " " & strRib)
        Me.cmb_EspLongi_I2.Items.Add(2 & " " & strRibs)
        Me.cmb_EspLongi_I2.Items.Add(3 & " " & strRibs)
        Me.cmb_EspLongi_I2.Items.Add(4 & " " & strRibs)

        Me.cmb_EspLongi_I3.Items.Clear()
        Me.cmb_EspLongi_I3.Items.Add(1 & " " & strRib)
        Me.cmb_EspLongi_I3.Items.Add(2 & " " & strRibs)
        Me.cmb_EspLongi_I3.Items.Add(3 & " " & strRibs)
        Me.cmb_EspLongi_I3.Items.Add(4 & " " & strRibs)

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Connecteurs.BackColor = CouleurBackBandeaux
        Me.lbl_Connecteurs.ForeColor = CouleurForeBandeaux

        Me.lbl_Connection.BackColor = CouleurBackBandeaux
        Me.lbl_Connection.ForeColor = CouleurForeBandeaux

        Me.img_Connection.Dock = DockStyle.Fill
        Me.img_Connection.BorderStyle = BorderStyle.FixedSingle

        Me.txt_hsc.ReadOnly = True
        Me.txt_d.ReadOnly = True
        Me.txt_fy.ReadOnly = True
        Me.txt_fu.ReadOnly = True


    End Sub

    Private Sub AfficherPoutreEnCours()
        With MyPoutreLoc.Dalle.Connecteur
            ind_database = .IndiceDataBase
            Me.txt_hsc.Text = .hsc
            Me.txt_d.Text = .d
            Me.txt_fy.Text = .Fy
            Me.txt_fu.Text = .Fu
        End With

        'Affiche un cmb ou un txt en fonction de la présence ou non d'un bac transversal
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

        'Affiche les valeurs de la poutre en cours
        With MyPoutreLoc
            Select Case .NombreZone(traveeEnCours.Item2)
                Case 1
                    Me.txt_Largeur_I1.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours.Item2, 0), Enu_TypeVariable.Longueur, 3, 3, False)
                    Me.cmb_NbRow_I1.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 0)
                    If Not lBacTransv Then
                        Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 0), Enu_TypeVariable.Dimension, 3, 3, False)
                    Else
                        Me.cmb_EspLongi_I1.Text = .Espacement_Bac_Trans(traveeEnCours.Item2, 0)
                    End If
                Case 2
                    Me.txt_Largeur_I1.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours.Item2, 0), Enu_TypeVariable.Longueur, 3, 3, False)
                    Me.cmb_NbRow_I1.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 0)
                    If Not lBacTransv Then
                        Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 0), Enu_TypeVariable.Dimension, 3, 3, False)
                    Else
                        Me.cmb_EspLongi_I1.Text = .Espacement_Bac_Trans(traveeEnCours.Item2, 0)
                    End If

                    Me.txt_Largeur_I2.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours.Item2, 1), Enu_TypeVariable.Longueur, 3, 3, False)
                    Me.cmb_NbRow_I2.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 1)
                    If Not lBacTransv Then
                        Me.txt_EspLongi_I2.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 1), Enu_TypeVariable.Longueur, 3, 3, False)
                    Else
                        Me.cmb_EspLongi_I2.Text = .Espacement_Bac_Trans(traveeEnCours.Item2, 1)
                    End If
                Case 3
                    Me.txt_Largeur_I1.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours.Item2, 0), Enu_TypeVariable.Longueur, 3, 3, False)
                    Me.cmb_NbRow_I1.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 0)
                    If Not lBacTransv Then
                        Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 0), Enu_TypeVariable.Dimension, 3, 3, False)
                    Else
                        Me.cmb_EspLongi_I1.Text = .Espacement_Bac_Trans(traveeEnCours.Item2, 0)
                    End If

                    Me.txt_Largeur_I2.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours.Item2, 1), Enu_TypeVariable.Longueur, 3, 3, False)
                    Me.cmb_NbRow_I2.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 1)
                    If Not lBacTransv Then
                        Me.txt_EspLongi_I2.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 1), Enu_TypeVariable.Longueur, 3, 3, False)
                    Else
                        Me.cmb_EspLongi_I2.Text = .Espacement_Bac_Trans(traveeEnCours.Item2, 1)
                    End If

                    Me.txt_Largeur_I3.Text = GetStringInUnit(.Longueur_Zone(traveeEnCours.Item2, 2), Enu_TypeVariable.Longueur, 3, 3, False)
                    Me.cmb_NbRow_I2.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 2)
                    If Not lBacTransv Then
                        Me.txt_EspLongi_I3.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 2), Enu_TypeVariable.Longueur, 3, 3, False)
                    Else
                        Me.cmb_EspLongi_I3.Text = .Espacement_Bac_Trans(traveeEnCours.Item2, 2)
                    End If
            End Select

            Me.cmb_NbRow_I1.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 0)
            Me.cmb_NbRow_I2.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 1)
            Me.cmb_NbRow_I3.SelectedItem = .NombreGoujonsTransv(traveeEnCours.Item2, 2)

            Me.cmb_EspLongi_I1.SelectedItem = .Espacement_Bac_Trans(traveeEnCours.Item2, 0)
            Me.cmb_EspLongi_I2.SelectedItem = .Espacement_Bac_Trans(traveeEnCours.Item2, 1)
            Me.cmb_EspLongi_I3.SelectedItem = .Espacement_Bac_Trans(traveeEnCours.Item2, 2)

            Me.txt_EspLongi_I1.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 0), Enu_TypeVariable.Dimension, 3, 3, False)
            Me.txt_EspLongi_I2.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 1), Enu_TypeVariable.Dimension, 3, 3, False)
            Me.txt_EspLongi_I3.Text = GetStringInUnit(.Espacement(traveeEnCours.Item2, 2), Enu_TypeVariable.Dimension, 3, 3, False)

            Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonsTot(traveeEnCours.Item2) & " " & strStud

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

            End If
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

    End Sub

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

                strSymbol = "h"
                strIndice = "  "



            Case Me.img_fy.Name

                strSymbol = "f"
                strIndice = "y "

            Case Me.img_fu.Name

                strSymbol = "f"
                strIndice = "u "

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "

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
        MAJ_affichage_txt_cmb()
    End Sub

    Private Sub MAJ_affichage_txt_cmb()
        lMAJAffichage = True

        'Met à jours la visibilité des txtbox
        Me.txt_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 2
        Me.txt_Largeur_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 2
        Me.cmb_NbRow_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 2
        Me.cmb_EspLongi_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 2 And lBacTransv
        Me.txt_EspLongi_I2.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 2 And Not lBacTransv

        Me.txt_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 3
        Me.txt_Largeur_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 3
        Me.cmb_NbRow_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 3
        Me.cmb_EspLongi_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 3 And lBacTransv
        Me.txt_EspLongi_I3.Visible = MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 3 And Not lBacTransv

        'Met à jour les valeurs dans les txtbox ou cmbbox 

        If Not ltxt_Largeur_I1Enter Then Me.txt_Largeur_I1.Text = GetStringInUnit(MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0), Enu_TypeVariable.Longueur, 3, 3, False)
        If Not ltxt_Largeur_I2Enter Then Me.txt_Largeur_I2.Text = GetStringInUnit(MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1), Enu_TypeVariable.Longueur, 3, 3, False)
        If Not ltxt_Largeur_I3Enter Then Me.txt_Largeur_I3.Text = GetStringInUnit(MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2), Enu_TypeVariable.Longueur, 3, 3, False)

        Me.cmb_NbRow_I1.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, 0) - 1
        Me.cmb_NbRow_I2.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, 1) - 1
        Me.cmb_NbRow_I3.SelectedIndex = MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, 2) - 1

        Me.cmb_EspLongi_I1.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 0) - 1
        Me.cmb_EspLongi_I2.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 1) - 1
        Me.cmb_EspLongi_I3.SelectedIndex = MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 2) - 1

        Me.txt_EspLongi_I1.Text = GetStringInUnit(MyPoutreLoc.Espacement(traveeEnCours.Item2, 0), Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_EspLongi_I2.Text = GetStringInUnit(MyPoutreLoc.Espacement(traveeEnCours.Item2, 1), Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_EspLongi_I3.Text = GetStringInUnit(MyPoutreLoc.Espacement(traveeEnCours.Item2, 2), Enu_TypeVariable.Dimension, 3, 3, False)

        lMAJAffichage = False
    End Sub

#End Region

#Region " Evènements saisie "
    Private Sub btn_Ajouter_Click(sender As Object, e As EventArgs) Handles btn_Ajouter.Click
        If lBuild Then Exit Sub

        If MyPoutreLoc.NombreZone(traveeEnCours.Item2) <= 2 Then MyPoutreLoc.NombreZone(traveeEnCours.Item2) += 1
        Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours.Item2) = 3
        Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours.Item2) = 1
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb()
    End Sub

    Private Sub btn_Supprimer_Click(sender As Object, e As EventArgs) Handles btn_Supprimer.Click
        If lBuild Then Exit Sub

        If MyPoutreLoc.NombreZone(traveeEnCours.Item2) >= 2 Then MyPoutreLoc.NombreZone(traveeEnCours.Item2) -= 1
        Me.btn_Ajouter.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours.Item2) = 3
        Me.btn_Supprimer.Enabled = Not MyPoutreLoc.NombreZone(traveeEnCours.Item2) = 1
        MAJ_Nb_Zone()
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb()
    End Sub

    'Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

    '    Dim lOk As Boolean = True
    '    ErrorProvider_Frm_Connection.Clear()

    '    Dim iErreur As Integer
    '    Dim ValMin, ValMax As Decimal
    '    Dim lValMax As Boolean = True
    '    Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

    '    Select Case MyTxt.Name
    '        Case Me.txt_MainSpan.Name

    '            ValMin = PORTEEMIN / kUnit
    '            ValMax = PORTEEMAX / kUnit

    '        Case Me.txt_PorteeConsoleG.Name, Me.txt_PorteeConsoleD.Name

    '            ValMin = CONSOLEMIN / kUnit
    '            ValMax = RATIOCONSOLEMAX * MyPoutreLoc.LongueurTravee(1) / kUnit

    '        Case Me.txt_D1.Name, Me.txt_D2.Name

    '            ValMin = ENTRAXEMIN / kUnit
    '            ValMax = ENTRAXEMAX / kUnit

    '        Case Me.txt_TremieGauche.Name
    '            ValMin = 0
    '            ValMax = MyPoutreLoc.EntraxeD1 / 2

    '        Case Me.txt_TremieDroite.Name
    '            ValMin = 0
    '            ValMax = MyPoutreLoc.EntraxeD2 / 2

    '    End Select
    '    iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

    '    If iErreur <> 0 Then
    '        NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
    '    Else
    '        ValeurUI = TraiteReal(MyTxt.Text) * kUnit
    '        ErrorProvider.Clear()
    '    End If

    '    lOk = (iErreur = 0)
    '    Return lOk
    'End Function

    Sub MAJ_Nb_Zone()

        'MAJ des longueurs de zone suite à un clique Ajouter ou Supprimer
        If lBuild Then Exit Sub
        Select Case MyPoutreLoc.NombreZone(traveeEnCours.Item2)
            Case 1
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2)
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = 0
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = 0
            Case 2
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 2
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 2
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = 0
            Case 3
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 3
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 3
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 3
        End Select


    End Sub

    Private Sub MAJ_SommeGoujons()
        'MAJ du calcul de la somme des goujons après les modifications des valeurs

        MyPoutreLoc.NombreGoujonsTot(traveeEnCours.Item2) = 0
        For i As Integer = 0 To 2
            MyPoutreLoc.NombreGoujonsTot(traveeEnCours.Item2) += MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, i) * MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, i) / MyPoutreLoc.Espacement(traveeEnCours.Item2, i)
        Next

        Me.etq_Somme.Text = MyPoutreLoc.NombreGoujonsTot(traveeEnCours.Item2)

    End Sub


    Private Sub txt_Largeur_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_Largeur_I1.TextChanged, txt_Largeur_I2.TextChanged, txt_Largeur_I3.TextChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case sender.name
            Case txt_Largeur_I1.Name
                If Not ltxt_Largeur_I1Enter Then Exit Sub
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) = txt_Largeur_I1.Text * kUnit

                Select Case MyPoutreLoc.NombreZone(traveeEnCours.Item2)
                    Case 1
                        MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = 0
                        MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = 0
                    Case 2
                        MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0)
                        MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = 0
                    Case 3
                        MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = (MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0)) / 2
                        MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = (MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0)) / 2
                End Select

            Case txt_Largeur_I2.Name
                If Not ltxt_Largeur_I2Enter Then Exit Sub
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) = txt_Largeur_I2.Text * kUnit

                If MyPoutreLoc.NombreZone(traveeEnCours.Item2) = 2 Then
                    MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1)
                    MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = 0
                Else '3 zones
                    MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1)
                End If

            Case txt_Largeur_I3.Name
                If Not ltxt_Largeur_I3Enter Then Exit Sub
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2) = txt_Largeur_I3.Text * kUnit
                MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 0) = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 1) - MyPoutreLoc.Longueur_Zone(traveeEnCours.Item2, 2)
        End Select
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb()
    End Sub

    Private Sub cmb_NbRow_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbRow_I1.SelectedIndexChanged, cmb_NbRow_I2.SelectedIndexChanged, cmb_NbRow_I3.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Select Case sender.name
            Case cmb_NbRow_I1.Name
                MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, 0) = cmb_NbRow_I1.SelectedIndex + 1
            Case cmb_NbRow_I2.Name
                MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, 1) = cmb_NbRow_I2.SelectedIndex + 1
            Case cmb_NbRow_I3.Name
                MyPoutreLoc.NombreGoujonsTransv(traveeEnCours.Item2, 2) = cmb_NbRow_I2.SelectedIndex + 1
        End Select

        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb()
    End Sub



    Private Sub cmb_EspLongi_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_EspLongi_I1.SelectedIndexChanged, cmb_EspLongi_I2.SelectedIndexChanged, cmb_EspLongi_I3.SelectedIndexChanged
        If lBuild Or lMAJAffichage Then Exit Sub
        Select Case sender.name
            Case cmb_EspLongi_I1.Name
                MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 0) = cmb_EspLongi_I1.SelectedIndex + 1
                MyPoutreLoc.Espacement(traveeEnCours.Item2, 0) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 0)
            Case cmb_EspLongi_I2.Name
                MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 1) = cmb_EspLongi_I2.SelectedIndex + 1
                MyPoutreLoc.Espacement(traveeEnCours.Item2, 1) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 1)
            Case cmb_EspLongi_I3.Name
                MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 2) = cmb_EspLongi_I3.SelectedIndex + 1
                MyPoutreLoc.Espacement(traveeEnCours.Item2, 2) = MyPoutreLoc.Esp_longi_bac * MyPoutreLoc.Espacement_Bac_Trans(traveeEnCours.Item2, 2)
        End Select
        MAJ_SommeGoujons()
        MAJ_affichage_txt_cmb()
    End Sub

    Private Sub txt_EspLongi_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_EspLongi_I1.TextChanged, txt_EspLongi_I2.TextChanged, txt_EspLongi_I3.TextChanged
        If lBuild Or lMAJAffichage Then Exit Sub

        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case sender.name
            Case txt_EspLongi_I1.Name
                MyPoutreLoc.Espacement(traveeEnCours.Item2, 0) = txt_EspLongi_I1.Text * kUnit
            Case txt_EspLongi_I2.Name
                MyPoutreLoc.Espacement(traveeEnCours.Item2, 1) = txt_EspLongi_I2.Text * kUnit
            Case txt_EspLongi_I3.Name
                MyPoutreLoc.Espacement(traveeEnCours.Item2, 2) = txt_EspLongi_I3.Text * kUnit
        End Select
        MAJ_SommeGoujons()
    End Sub





#End Region

End Class