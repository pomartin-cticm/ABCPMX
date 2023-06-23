Imports PMXMoteur2
Imports System.IO

Public Class Frm_Maintiens

#Region " Variables locales "

    Dim lBuild As Boolean = True
    Dim iSelect As Integer = 1
    '----------------------------------------------
    '   1 pour la travée principale
    '   -1 si rien de selectionné
    '   0 console gauche
    '   99 console droite
    '----------------------------------------------

    Dim MyPoutreLoc As New cls_Poutre

    Dim strTypeTravee() As String
    Dim strTypeTravee_ConsoleGauche As String
    Dim strTypeTravee_TraveeCentrale As String
    Dim strTypeTravee_ConsoleDroite As String

    Dim X_Mousse As Decimal = 0
    Dim Y_Mousse As Decimal = 0

    Dim lMouseDown As Boolean = False

    Dim traveeEnCours As (cls_Poutre.EnuTypeTravee, Integer) = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Portees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()

        RemplirComboTypeTravee()

        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_MAINTIENS")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                strTypeTravee_TraveeCentrale = Bloc("MAINSPAN")
                strTypeTravee_ConsoleGauche = Bloc("CONSOLEG")
                strTypeTravee_ConsoleDroite = Bloc("CONSOLED")

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


            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub RemplirComboTypeTravee()
        Me.cmb_Travee.Items.Clear()
        Me.cmb_Travee.Items.AddRange(strTypeTravee)
        Me.cmb_Travee.SelectedIndex = 0
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.img_Maintiens.Dock = DockStyle.Fill
        Me.img_Maintiens.BorderStyle = BorderStyle.FixedSingle

    End Sub

    Private Sub AfficherPoutreEnCours()

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

#End Region

#Region " Dessins "
    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Maintiens.Paint

        DessinFrmMaintiens(e.Graphics, MyPoutreLoc, Me.img_Maintiens.ClientRectangle.Width, Me.img_Maintiens.ClientRectangle.Height, 1, iSelect, False)

    End Sub

#End Region

#Region "Gestion de la souris dans l'image"

    Private Sub MousseMove(sender As Object, e As MouseEventArgs) Handles img_Maintiens.MouseMove
        If lBuild Then Exit Sub
        'Position de la sourie dans la fenêtre affichée
        X_Mousse = e.X
        Y_Mousse = e.Y

        If lMouseDown Then
            DeplacementMaintienSemelle(MyPoutreLoc, Me.img_Maintiens.ClientRectangle.Width, Me.img_Maintiens.ClientRectangle.Height, 1, iSelect, traveeEnCours.Item2, X_Mousse)
        End If

        img_Maintiens.Invalidate()

    End Sub

    Private Sub MouseClickDown(sender As Object, e As MouseEventArgs) Handles img_Maintiens.MouseDown
        If lBuild Then Exit Sub

        Mod_Dessins.ModificationMaintienSemelle(MyPoutreLoc, Me.img_Maintiens.ClientRectangle.Width, Me.img_Maintiens.ClientRectangle.Height, 1, iSelect, traveeEnCours.Item2, X_Mousse, Y_Mousse)
        img_Maintiens.Invalidate()

        lMouseDown = True

    End Sub

    Private Sub MouseClickUp(sender As Object, e As MouseEventArgs) Handles img_Maintiens.MouseUp
        If lBuild Then Exit Sub

        For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
            For Each maintien As cls_Maintiens In MyPoutreLoc.Maintiens(i)
                maintien.lMaintienSelectionne = False
            Next

            img_Maintiens.Invalidate()

            lMouseDown = False
        Next
    End Sub




#End Region



#Region " Evènements "

    Private Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        MyPoutreLoc.Maintiens(traveeEnCours.Item2).Add(New cls_Maintiens(MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / 2, True, True, False))

        MAJ_PositionMaintiens()

        img_Maintiens.Invalidate()
    End Sub

    Private Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click
        If MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count <> 0 Then
            MyPoutreLoc.Maintiens(traveeEnCours.Item2).Remove(MyPoutreLoc.Maintiens(traveeEnCours.Item2).Last)
            MAJ_PositionMaintiens()
            img_Maintiens.Invalidate()
        End If

    End Sub


    Private Sub MAJ_PositionMaintiens()
        Dim index_maintien As Integer

        For Each maintiens As cls_Maintiens In MyPoutreLoc.Maintiens(traveeEnCours.Item2)
            index_maintien = MyPoutreLoc.Maintiens(traveeEnCours.Item2).IndexOf(maintiens)
            maintiens.x_Loc = (index_maintien + 1) * MyPoutreLoc.LongueurTravee(traveeEnCours.Item2) / (MyPoutreLoc.Maintiens(traveeEnCours.Item2).Count + 1)
        Next
    End Sub

#End Region

#Region " Evènements saisie "
    Private Sub comboTraveeSelectionneeChanged(sender As Object, e As EventArgs) Handles cmb_Travee.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case cmb_Travee.Text
            Case strTypeTravee_ConsoleGauche
                traveeEnCours = (cls_Poutre.EnuTypeTravee.ConsoleGauche, 0)
                iSelect = 0

            Case strTypeTravee_TraveeCentrale
                traveeEnCours = (cls_Poutre.EnuTypeTravee.DeuxAppuis, 1)
                iSelect = 1

            Case strTypeTravee_ConsoleDroite
                traveeEnCours = (cls_Poutre.EnuTypeTravee.ConsoleDroite, MyPoutreLoc.IndiceTraveeConsoleDroite)
                iSelect = 99
        End Select

        img_Maintiens.Invalidate()

    End Sub



#End Region

End Class