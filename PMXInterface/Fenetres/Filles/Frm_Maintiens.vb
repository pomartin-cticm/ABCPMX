Imports PMXMoteur2
Imports System.IO

Public Class Frm_Maintiens

#Region " Variables locales "

    ''' <summary>
    ''' informe si la fenêtre est en cours de construction ou non
    ''' </summary>
    Dim lBuild As Boolean = True

    ''' <summary>
    ''' indice qui informe du numéro de travée en cours
    ''' </summary>
    Dim iSelect As Integer = 1
    '----------------------------------------------
    '   1 pour la travée principale
    '   -1 si rien de selectionné
    '   0 console gauche
    '   99 console droite
    '----------------------------------------------

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_travee
    ''' </summary>
    Dim strTypeTravee() As String
    Dim strTypeTravee_ConsoleGauche As String
    Dim strTypeTravee_TraveeCentrale As String
    Dim strTypeTravee_ConsoleDroite As String
    Dim strSpan As String

    ''' <summary>
    ''' Abscisse de la souris sur le dessin
    ''' </summary>
    Dim X_Mousse As Decimal = 0

    ''' <summary>
    ''' Ordonnée de la souris sur le dessin
    ''' </summary>
    Dim Y_Mousse As Decimal = 0

    ''' <summary>
    ''' variable locale qui informe si le clique de la souris est enfoncé ou non
    ''' </summary>
    Dim lMouseDown As Boolean = False

    ''' <summary>
    ''' variable locale pour enreristrer la position des cotations sur le dessin.
    ''' La premiere colonne suit l'abcisse de la cotation
    ''' La deuxième colonne suit l'ordonnée de la cotation
    ''' </summary>
    Dim positionCotesInferieuresDessin(,) As Decimal

    ''' <summary>
    ''' variable locale pour enregistrer la position des maintiens sur le dessin
    ''' La première colonne suit l'abcisse des maintiens
    ''' La deuxième colonne suit l'ordonné min du maintien
    ''' La troisième colonne suit l'ordonné max du maintien
    ''' </summary>
    Dim positionMaintiensDessin(,) As Decimal

    ''' <summary>
    ''' variable locale qui indique si la souris à cliquer au droit d'une cotation
    ''' </summary>
    Dim lClickCote As Boolean = False

    ''' <summary>
    ''' variable locale qui enregistre l'indice de la cotation selectionnée, le cas échéant
    ''' </summary>
    Dim indiceCoteSelectionnee As Integer

    ''' <summary>
    ''' variable locale qui informe si la souris survole ou non une cotation sur le dessin
    ''' </summary>
    Dim lMouseOnCote As Boolean = False

    ''' <summary>
    ''' variable locale qui informe si la souris survole ou non la poignée d'un maintien sur le dessin
    ''' </summary>
    Dim lMouseOnPoigneeMaintien As Boolean = False

    ''' <summary>
    ''' variable locale qui enregistre l'épaisseur de la semelle du dessin affiché
    ''' </summary>
    Dim EpaisseurSemelleDessin As Decimal = 0

    ''' <summary>
    ''' variable locale qui informe si la souris survole ou non un maintien de semelle sur le dessin
    ''' </summary>
    Dim lMouseOnMaintien As Boolean = False

    ''' <summary>
    ''' variable locale qui informe quelle travée est affichée à l'écran
    ''' </summary>
    Dim traveeEnCours As (cls_Poutre.EnuTypeTravee, Integer) = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)

    ''' <summary>
    ''' variable locale qui informe du chemin vers l'icone pour déplacer les maintiens horizontalement
    ''' </summary>
    Dim MyCursor As New Cursor(LogicielRep.Images & "\FrmMaintien_MoveH.ico")

    Dim NbTravees As Integer

    Dim FontFrm As Font

    Dim strInfoW_PoutreMixte As String

    Dim lSelectInfoN As Boolean = False

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Portees_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitialiserFenetre()

    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        RemplirComboTypeTravee()
        PrepareFlechesNavigation()
        MAJI_BtnNavigation()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_LATERALRESTRAINTS")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                strTypeTravee_TraveeCentrale = Bloc("MAINSPAN")
                strTypeTravee_ConsoleGauche = Bloc("LEFTCANT")
                strTypeTravee_ConsoleDroite = Bloc("RIGHTCANT")
                strSpan = Bloc("SPAN")

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

                Me.lbl_Maintiens.Text = Bloc("RESTRAINTS")
                Me.lbl_Travee.Text = Bloc("SPAN")
                Me.rad_NonRestrain.Text = Bloc("NORESTRAINT")
                Me.rad_FullyRestrain.Text = Bloc("FULLYRESTRAINED")
                Me.rad_PointRestrain.Text = Bloc("POINTRESTRAINTS")

                'Me.lbl_.Text = Bloc("DRAWCONTROL")
                Me.btn_Add.Text = Bloc("ADD")
                Me.btn_Delete.Text = Bloc("DELETE")

                strInfoW_PoutreMixte = Bloc("INFOCOMPOSITE") ' "Pour les poutres mixtes, les maitiens latéraux ne sont pris en compte que dans la phase de construction, en l'absence d'étaiement"

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub RemplirComboTypeTravee()

        '==POM=============================
        Dim index As Integer = 0

        NbTravees = MyPoutreLoc.NbTravees
        ReDim strTypeTravee(NbTravees - 1)
        Dim lCentral As Boolean = (MyPoutreLoc.NombreTraveesDeuxAppuis = 1)
        If MyPoutreLoc.lTraveeConsoleGauche Then
            strTypeTravee(index) = strTypeTravee_ConsoleGauche
            index += 1
        End If
        For i As Integer = 1 To MyPoutreLoc.NombreTraveesDeuxAppuis
            If lCentral Then
                strTypeTravee(index) = strTypeTravee_TraveeCentrale
            Else
                strTypeTravee(index) = strSpan & " no " & CStr(i)
            End If
            index += 1
        Next
        If MyPoutreLoc.lTraveeConsoleDroite Then strTypeTravee(index) = strTypeTravee_ConsoleDroite
        '==================================

        Me.cmb_Travee.Items.Clear()
        Me.cmb_Travee.Items.AddRange(strTypeTravee)
        If MyPoutreLoc.lTraveeConsoleGauche Then
            Me.cmb_Travee.SelectedIndex = 1
        Else
            Me.cmb_Travee.SelectedIndex = 0
        End If
        traveeEnCours = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)
    End Sub

    Private Sub PrepareFlechesNavigation()
        '===POM

        Me.btn_Suivant.Visible = (NbTravees > 1)
        Me.btn_Precedent.Visible = (NbTravees > 1)

    End Sub

    Private Sub MAJI_BtnNavigation()
        '===POM
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

        Me.img_InfoN.BackColor = SystemColors.ControlLightLight

        'Me.lbl_.BackColor = CouleurBackBandeaux
        'Me.lbl_.ForeColor = CouleurForeBandeaux

        Me.lbl_Maintiens.BackColor = CouleurBackBandeaux
        Me.lbl_Maintiens.ForeColor = CouleurForeBandeaux

        Me.TLpan_Main.ColumnStyles(0).Width = LargeurColonneSaisie

        Me.img_Maintiens.Dock = DockStyle.Fill
        Me.img_Maintiens.BorderStyle = BorderStyle.FixedSingle

        Me.txt_Cotations.Visible = False

        FontFrm = New Font(Me.rad_FullyRestrain.Font.Name, SizeFontFrm)

    End Sub

    Private Sub AfficherPoutreEnCours()

        'Me.img_info.Visible = MyPoutreLoc.lMixte
        Me.img_InfoN.Visible = MyPoutreLoc.lMixte

        'Select Case myBeamLoc.TypeMaintien(traveeEnCours.Item2)
        Select Case MyPoutreLoc.TypeMaintien
            Case cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
                rad_NonRestrain.Checked = True

            Case cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained
                rad_FullyRestrain.Checked = True

            Case cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
                rad_PointRestrain.Checked = True
        End Select

        'MAJ_pan_ControlDessin(rad_PointRestrain.Checked)
        MAJI_PointRestraints(rad_PointRestrain.Checked)

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


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours)

            For i_travee As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee

                If MyPoutreLoc.Maintiens(i_travee).Count <> .Maintiens(i_travee).Count Then
                    lModif = True
                    .Maintiens(i_travee) = MyPoutreLoc.Maintiens(i_travee)
                Else
                    For Each maintiens In MyPoutreLoc.Maintiens(i_travee)
                        Dim i_maintiens As Integer = MyPoutreLoc.Maintiens(i_travee).IndexOf(maintiens)

                        If Not maintiens.Equals(.Maintiens(i_travee)(i_maintiens)) Then
                            lModif = True
                            .Maintiens(i_travee) = MyPoutreLoc.Maintiens(i_travee)
                        End If

                    Next
                End If

                'If .TypeMaintien(i_travee) <> myBeamLoc.TypeMaintien(i_travee) Then
                If MyPoutreLoc.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained And MyPoutreLoc.NombreTotalMaintiens = 0 Then MyPoutreLoc.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained

                If .TypeMaintien <> MyPoutreLoc.TypeMaintien Then
                    lModif = True
                    .TypeMaintien = MyPoutreLoc.TypeMaintien
                End If

            Next

        End With
    End Sub

#End Region

#Region " Dessins "
    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Maintiens.Paint

        iSelect = traveeEnCours.Item2
        DessinFrmMaintiens(e.Graphics, MyPoutreLoc, FontFrm, Me.img_Maintiens.ClientRectangle.Width, Me.img_Maintiens.ClientRectangle.Height,
                           1, iSelect, True, positionCotesInferieuresDessin, positionMaintiensDessin, EpaisseurSemelleDessin)

    End Sub

#End Region

#Region " Gestion de la souris dans l'image "

    Private Sub MousseMove(sender As Object, e As MouseEventArgs) Handles img_Maintiens.MouseMove
        If lBuild Then Exit Sub

        'Position de la sourie dans la fenêtre affichée
        X_Mousse = e.X
        Y_Mousse = e.Y

        If lMouseDown Then
            DeplacementMaintienSemelle(MyPoutreLoc, Me.img_Maintiens.ClientRectangle.Width, Me.img_Maintiens.ClientRectangle.Height, 1, iSelect, traveeEnCours.Item2, X_Mousse)

        Else

            'Observe si la souris se trouve sur une cotation, auquel cas on change la souris avec un curseur main

            lMouseOnCote = False

            If Not positionCotesInferieuresDessin Is Nothing Then
                For i As Integer = 0 To positionCotesInferieuresDessin.GetLength(0) - 1

                    If (Math.Abs(positionCotesInferieuresDessin(i, 0) - X_Mousse) <= txt_Cotations.Width / 2) And (Math.Abs(positionCotesInferieuresDessin(i, 1) - Y_Mousse) <= txt_Cotations.Height / 2) Then
                        lMouseOnCote = True
                        Exit For
                    End If

                Next
            End If

            'Observe si la souris se trouve sur une poignée de maintien, auquel cas on change la souris avec un curseur personnalisé;  ou sur un maintien de semelle sup ou inf, auquel cas on change la souris avec un curseur main

            lMouseOnPoigneeMaintien = False
            lMouseOnMaintien = False

            If Not positionMaintiensDessin Is Nothing Then
                For i As Integer = 0 To positionMaintiensDessin.GetLength(0) - 1

                    If Math.Abs(positionMaintiensDessin(i, 0) - X_Mousse) <= EpaisseurSemelleDessin Then
                        If (Math.Abs((positionMaintiensDessin(i, 1) + positionMaintiensDessin(i, 2)) / 2 - Y_Mousse) <= EpaisseurSemelleDessin) Then
                            lMouseOnPoigneeMaintien = True
                            Exit For
                        ElseIf (Math.Abs(positionMaintiensDessin(i, 1) - Y_Mousse) <= EpaisseurSemelleDessin) Or (Math.Abs(positionMaintiensDessin(i, 2) - Y_Mousse) <= EpaisseurSemelleDessin) Then
                            lMouseOnMaintien = True
                            Exit For
                        End If

                    End If

                Next
            End If

            'Modification du curseur en fonction de ce que survole la souris

            If lMouseOnPoigneeMaintien Then
                Me.img_Maintiens.Cursor = MyCursor
            ElseIf lMouseOnCote Or lMouseOnMaintien Then
                Me.img_Maintiens.Cursor = Cursors.Hand
            Else
                Me.img_Maintiens.Cursor = Cursors.Default
            End If



            'Si la souris bouge alors que le clique est maintenu, et si on est au droit d'un maintien, alors celui-ci peut être déplacé


        End If

        img_Maintiens.Invalidate()

    End Sub

    Private Sub MouseClickDown(sender As Object, e As MouseEventArgs) Handles img_Maintiens.MouseDown
        If lBuild Then Exit Sub

        Mod_Dessins.GestionClickDownMousse(MyPoutreLoc, Me.img_Maintiens.ClientRectangle.Width, Me.img_Maintiens.ClientRectangle.Height, 1, iSelect, traveeEnCours.Item2, X_Mousse, Y_Mousse, lMouseOnPoigneeMaintien)
        img_Maintiens.Invalidate()

        'Regarde si on clique sur une cotation
        'Le cas échéant, on déplace le txtbox au droit de la cote sélectionnée

        lClickCote = False

        If Not positionCotesInferieuresDessin Is Nothing Then
            For i As Integer = 0 To positionCotesInferieuresDessin.GetLength(0) - 1

                If (Math.Abs(positionCotesInferieuresDessin(i, 0) - X_Mousse) <= txt_Cotations.Width / 2) And (Math.Abs(positionCotesInferieuresDessin(i, 1) - Y_Mousse) <= txt_Cotations.Height / 2) Then
                    txt_Cotations.Visible = True
                    txt_Cotations.Location = New Point(positionCotesInferieuresDessin(i, 0) - txt_Cotations.Width / 2, positionCotesInferieuresDessin(i, 1) - txt_Cotations.Height / 2)
                    If i = 0 Then
                        txt_Cotations.Text = GetStringNoUnit(MyPoutreLoc.Maintiens(traveeEnCours.Item2)(0).x_Loc, Enu_TypeVariable.Longueur)
                    ElseIf i = positionCotesInferieuresDessin.GetLength(0) - 1 Then
                        txt_Cotations.Text = GetStringNoUnit(MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) - MyPoutreLoc.Maintiens(traveeEnCours.Item2)(i - 1).x_Loc, Enu_TypeVariable.Longueur)
                    Else
                        txt_Cotations.Text = GetStringNoUnit(MyPoutreLoc.Maintiens(traveeEnCours.Item2)(i).x_Loc - MyPoutreLoc.Maintiens(traveeEnCours.Item2)(i - 1).x_Loc, Enu_TypeVariable.Longueur)
                    End If

                    lClickCote = True
                    indiceCoteSelectionnee = i

                    Me.AcceptButton = Nothing

                End If

            Next
        End If

        If Not lClickCote Then txt_Cotations.Visible = False

        lMouseDown = True

    End Sub

    Private Sub MouseClickUp(sender As Object, e As MouseEventArgs) Handles img_Maintiens.MouseUp
        If lBuild Then Exit Sub

        'Permet de déselectionner l'ensemble des maintiens une fois que la souris est relachée
        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For Each maintien As cls_Maintiens In MyPoutreLoc.Maintiens(i)
                maintien.lMaintienSelectionne = False
            Next

            img_Maintiens.Invalidate()

            lMouseDown = False
        Next
    End Sub


    Private Sub LeaveTxtCotation(sender As Object, e As EventArgs) Handles txt_Cotations.Leave

        txt_Cotations.Visible = False

        Me.AcceptButton = Me.btn_OK
        Me.btn_OK.Select()

    End Sub

    Private Sub KeyPressTxtCotation(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_Cotations.KeyPress

        'Gestion de la touche entrée lorsque l'utilisateur est dans un txtbox
        Select Case e.KeyChar
            Case Chr(13) 'Retour chariot
                txt_Cotations.Visible = False
                Validation_txt_Cotation()

            Case Chr(27) 'Touche echap
                txt_Cotations.Visible = False

        End Select

    End Sub

#End Region

#Region " Evènements "

    Private Sub img_Maintiens_Resize(sender As Object, e As EventArgs) Handles img_Maintiens.Resize
        Me.img_Maintiens.Invalidate()
    End Sub


    Private Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        If lBuild Then Exit Sub

        If MyPoutreLoc.TypeMaintien <> cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained Then
            MyPoutreLoc.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
            lBuild = True
            Me.rad_PointRestrain.Checked = True
            lBuild = False
            MAJI_PointRestraints(True)
        End If

        If MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count < NBRESTRAINMAX Then
            MyPoutreLoc.Maintiens(traveeEnCours.Item2).Add(New cls_Maintiens(MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 2, True, True, False))
            MAJ_PositionMaintiens()
            img_Maintiens.Invalidate()
        End If

    End Sub

    Private Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click
        If lBuild Then Exit Sub

        If MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count > Mod_Declarations.NBRESTRAINMIN Then
            MyPoutreLoc.Maintiens(traveeEnCours.Item2).Remove(MyPoutreLoc.Maintiens(traveeEnCours.Item2).Last)
            MAJ_PositionMaintiens()
            img_Maintiens.Invalidate()
        End If

    End Sub
    Private Sub MouseClick_HorsPanImg(sender As Object, e As PaintEventArgs) Handles pan_Maintiens.Paint
        'Rend invisible les textbox lorsqu'on clique ailleurs
        txt_Cotations.Visible = False

    End Sub

    Private Sub MAJ_PositionMaintiens()
        'Dim index_maintien As Integer

        '--( Déclarations

        Dim val As Decimal = 0

        Dim lConsoleG As Boolean
        Dim lConsoleD As Boolean
        Dim x0, DeltaX As Decimal

        '--( Initialisations

        lConsoleG = (traveeEnCours.Item2 = 0)
        lConsoleD = (traveeEnCours.Item2 = MyPoutreLoc.IndiceTraveeConsoleDroite)

        If lConsoleG Or lConsoleD Then
            DeltaX = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / (MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count)
        Else
            DeltaX = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / (MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count + 1)
        End If
        If lConsoleG Then
            x0 = 0
        Else
            x0 = DeltaX
        End If

        '--( Traitement

        'Lissage des positions des maintiens lorsqu'on ajoute ou supprime un maintien

        For i As Integer = 0 To MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count - 1

            MyPoutreLoc.Maintiens(traveeEnCours.Item2)(i).x_Loc = x0 + DeltaX * i

        Next

        'For Each maintiens As cls_Maintiens In myBeamLoc.Maintiens(traveeEnCours.Item2)
        '    'index_maintien = myBeamLoc.Maintiens(traveeEnCours.Item2).IndexOf(maintiens)
        '    val += myBeamLoc.LongueurTravee(traveeEnCours.Item2) / (myBeamLoc.Maintiens(traveeEnCours.Item2).Count + 1)
        '    maintiens.x_Loc = val
        'Next
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

    Private Sub comboTraveeSelectionneeChanged(sender As Object, e As EventArgs) Handles cmb_Travee.SelectedIndexChanged
        If lBuild Then Exit Sub

        'permet de mettre à jour les variables locales qui tracent l'indice de la travée en cours 

        '===POM
        'Select Case cmb_Travee.Text
        '    Case strTypeTravee_ConsoleGauche
        '        traveeEnCours = (cls_Poutre.EnuTypeTravee.ConsoleGauche, 0)
        '        iSelect = 0

        '    Case strTypeTravee_TraveeCentrale
        '        traveeEnCours = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)
        '        iSelect = 1

        '    Case strTypeTravee_ConsoleDroite
        '        traveeEnCours = (cls_Poutre.EnuTypeTravee.ConsoleDroite, myBeamLoc.IndiceTraveeConsoleDroite)
        '        iSelect = 99

        'End Select

        Select Case Me.cmb_Travee.SelectedIndex
            Case 0
                If MyPoutreLoc.lTraveeConsoleGauche Then
                    traveeEnCours = (cls_Poutre.EnuTypeTravee.ConsoleGauche, 0)
                    iSelect = 0
                Else
                    traveeEnCours = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)
                    iSelect = 1
                End If
            Case NbTravees - 1
                If MyPoutreLoc.lTraveeConsoleDroite Then
                    traveeEnCours = (cls_Poutre.EnuTypeTravee.ConsoleDroite, MyPoutreLoc.IndiceTraveeConsoleDroite)
                    iSelect = 99
                Else
                    traveeEnCours = (cls_Poutre.EnuTypeTravee.DeuxAppuis, NbTravees - 1)
                    iSelect = 1
                End If
            Case Else
                Dim i0 As Integer
                If MyPoutreLoc.lTraveeConsoleGauche Then i0 = 0 Else i0 = 1
                traveeEnCours = (cls_Poutre.EnuTypeTravee.DeuxAppuis, Me.cmb_Travee.SelectedIndex + i0)
                iSelect = 1
        End Select


        '===POM
        Select Case MyPoutreLoc.TypeMaintien
            Case cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
                rad_NonRestrain.Checked = True
            Case cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained
                rad_FullyRestrain.Checked = True
            Case cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
                rad_PointRestrain.Checked = True
        End Select

        MAJI_BtnNavigation()

        img_Maintiens.Invalidate()

    End Sub

    Private Sub Validation_txt_Cotation()

        'Vérifie et valide le nombre renseigné par l'utilisateur dans la cote sélectionnée

        If IsNumeric(txt_Cotations.Text) Then

            'Définition des variables locales
            Dim val As Double = CDec(txt_Cotations.Text)
            Dim xLoc As Decimal
            Dim nbMaintiens As Integer = MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count
            Dim longueurTraveeEnCours As Decimal = MyPoutreLoc.LongueurTravee(traveeEnCours.Item2)

            Dim decal As Decimal = MyPoutreLoc.Section.ProfilA.ha

            If val >= 0 Then

                With MyPoutreLoc

                    val = Math.Max(val, decal)
                    val = Math.Min(val, longueurTraveeEnCours - decal)

                    If indiceCoteSelectionnee = 0 Then
                        If nbMaintiens = 1 Then
                            .Maintiens(traveeEnCours.Item2)(0).x_Loc = val
                        Else
                            .Maintiens(traveeEnCours.Item2)(0).x_Loc = Math.Min(val, .Maintiens(traveeEnCours.Item2)(1).x_Loc - decal)
                        End If

                    ElseIf indiceCoteSelectionnee = positionCotesInferieuresDessin.GetLength(0) - 1 Then
                        If nbMaintiens = 1 Then
                            .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee - 1).x_Loc = longueurTraveeEnCours - val
                        Else
                            .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee - 1).x_Loc = Math.Max(longueurTraveeEnCours - val, .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee - 2).x_Loc + decal)
                        End If
                    Else
                        If nbMaintiens = 2 Then
                            xLoc = .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee - 1).x_Loc + val
                            xLoc = Math.Min(xLoc, longueurTraveeEnCours - decal)
                            .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee).x_Loc = xLoc
                        Else
                            If indiceCoteSelectionnee = positionCotesInferieuresDessin.GetLength(0) - 2 Then
                                xLoc = .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee - 1).x_Loc + val
                                xLoc = Math.Min(xLoc, longueurTraveeEnCours - decal)
                                .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee).x_Loc = xLoc
                            Else
                                xLoc = .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee - 1).x_Loc + val
                                xLoc = Math.Min(xLoc, .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee + 1).x_Loc - decal)
                                .Maintiens(traveeEnCours.Item2)(indiceCoteSelectionnee).x_Loc = xLoc
                            End If
                        End If
                    End If
                End With
            End If


        End If

        img_Maintiens.Invalidate()


    End Sub

    Private Sub rad_Restrain_CheckedChanged(sender As Object, e As EventArgs) Handles rad_NonRestrain.CheckedChanged, rad_FullyRestrain.CheckedChanged, rad_FullyRestrain.CheckedChanged
        If lBuild Then Exit Sub

        MAJ_PositionMaintiens()

        Select Case True
            Case rad_NonRestrain.Checked
                MyPoutreLoc.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.NotRestrained
            Case rad_FullyRestrain.Checked
                MyPoutreLoc.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.FullyRestrained
            Case rad_PointRestrain.Checked
                MyPoutreLoc.TypeMaintien = cls_Poutre.EnuTypeMaintiensPoutre.PointRestrained
        End Select

        'MAJ_pan_ControlDessin(rad_PointRestrain.Checked)
        MAJI_PointRestraints(rad_PointRestrain.Checked)

        img_Maintiens.Invalidate()
    End Sub

    'Private Sub MAJ_pan_ControlDessin(lEnable As Boolean)
    '    'Affiche le panel qui permet d'ajouter ou de supprimer des maintiens ponctuels uniquement si rad_PointRestrain est selectionné
    '    Me.pan_ControlDessin.Enabled = lEnable
    '    Me.lbl_.Enabled = lEnable
    'End Sub

    Private Sub MAJI_PointRestraints(lEnable As Boolean)
        Me.btn_Delete.Enabled = lEnable
    End Sub

#End Region

#Region " Infos W "

    Private Sub img_InfoN_MouseEnter(sender As Object, e As EventArgs) Handles img_InfoN.MouseEnter
        lSelectInfoN = True
        Me.img_InfoN.Invalidate()
    End Sub

    Private Sub img_InfoN_MouseLeave(sender As Object, e As EventArgs) Handles img_InfoN.MouseLeave
        lSelectInfoN = False
        Me.img_InfoN.Invalidate()
    End Sub

    Private Sub img_InfoN_Click(sender As Object, e As EventArgs) Handles img_InfoN.Click
        PublieInfoDegreConnex()
    End Sub

    Private Sub img_InfoN_Paint(sender As Object, e As PaintEventArgs) Handles img_InfoN.Paint
        DessineIconeInfo(e.Graphics, Me.img_InfoN, lSelectInfoN)
    End Sub

    'Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
    '    'If InfoW_lVisible Then
    '    '    InfoW_Fermer()
    '    'Else
    '    PublieInfoDegreConnex()
    '    'End If
    'End Sub

    Private Sub PublieInfoDegreConnex()

        InfoW.InitialiseInfo()

        InfoW.AddInfo(strInfoW_PoutreMixte)

        InfoW.Publie()

    End Sub


#End Region

End Class