Imports System.IO
Imports PMXMoteur2

Public Class Frm_Ouverture

#Region "Variables locales"

    Dim strFiltresExtension As String

#End Region

#Region "=== Ouverture Fenetre ==="

    ''' <summary>
    ''' Ouverture de la fenêtre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Ouverture_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GestionLangue()

        InitialiserFenetre()

    End Sub

    ''' <summary>
    ''' Affichage du texte de la fenêtre dans la langue séléctionnée
    ''' </summary>
    Public Sub GestionLangue()

        If File.Exists(LogicielFichiers.Langue) Then
            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_OUVERTURE")
            BlocLine.CreationBloc(Bloc)

            Try

                strFiltresExtension = Bloc("FILE")

                Me.Text = LogicielInfo.NomLogiciel

                Me.TabPage_NewProject.Text = Bloc("NEWPROJECT")
                Me.TabPage_OpenProject.Text = Bloc("OPENPROJECT")

                Me.Label_OpenFile.Text = Bloc("OPENPROJECT")
                Me.Button_OpenProject.Text = Bloc("BROWSEFILES")

                Me.Label_RecentFiles.Text = Bloc("RECENTFILES")

                Me.Button_Valider.Text = Bloc("OK")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_Ouverture/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub InitialiserFenetre()
        '--------------------------------------------------------------------
        '   Initialisation de la fenêtre avec des paramètres par défaut
        '--------------------------------------------------------------------

        '==> Nouveau Projet

        '  Frm_AjoutSectionN.InitialiserFenetre()
        '  Frm_AjoutSectionN.GestionLangue()
        Frm_AjoutePP.lOuverture = True
        Frm_AjoutePP.InitialiseFromOutside()
        TabPage_NewProject.Controls.Add(Frm_AjoutePP.pan_Choix)

        '==> Ouvrir Projet
        RemplirListe_RecentFiles()

    End Sub

    Private Sub RemplirListe_RecentFiles()
        '--------------------------------------------------------------------
        '   Remplir la liste avec les 10 derniers fichiers récents
        '--------------------------------------------------------------------

        'Initialisation
        ListBox_RecentFiles.Items.Clear()

        'Traitement
        For i = 0 To LogicielFichiers.RecentFiles.Count - 1
            ListBox_RecentFiles.Items.Add(LogicielFichiers.RecentFiles(i))
        Next

    End Sub

#End Region

#Region " Gestion Evenement "

    Private Sub Button_OpenProject_Click(sender As Object, e As EventArgs) Handles Button_OpenProject.Click

        '--> Déclaration
        Dim FileName As String

        '# Contrôle sauvegarde du projet en cours

        '# Demande nom fichier

        '--> Préparation de la boite de dialogue OpenFile

        Me.OpenFileDialog_Project.InitialDirectory = LogicielOptions.RepertoireTravail
        'Me.OpenFileDialog_Project.DefaultExt = LogicielInfo.Extension
        Me.OpenFileDialog_Project.Filter = strFiltresExtension & " (*." & LogicielInfo.Extension & ")|*." & LogicielInfo.Extension
        Me.OpenFileDialog_Project.FileName = ""
        Me.OpenFileDialog_Project.ShowDialog()

        FileName = Me.OpenFileDialog_Project.FileName

        '# Ouverture

        '--> Gestion du résultat de la boite de dialogue
        If FileName <> "" Then

            Frm_PMX.ReadInFile(FileName)
            Frm_PMX.EnregistreDansFichiersRecents(FileName)
            Me.Close()
        End If

    End Sub

    Private Sub ListBox_RecentFiles_DoubleClick(sender As Object, e As EventArgs) Handles ListBox_RecentFiles.DoubleClick

        If ListBox_RecentFiles.SelectedIndices.Count > 0 Then   '--> un fichier récent séléctionné

            Dim FileName As String = ListBox_RecentFiles.SelectedItem

            Ouverture_FichierRecent(FileName)

        End If

    End Sub

#End Region

#Region "=== Fermeture Fenetre ==="

    Private Sub Button_Valider_Click(sender As Object, e As EventArgs) Handles Button_Valider.Click

        If TabPage_NewProject.Visible Then          '==> NOUVEAU PROJET

            '--> Ajout de la nouvelle poutre

            Frm_AjoutePP.TraitementSaisie()

            '--> Affichage de la soudure créee
            '   Frm_MAIN.AffichageFenetreFille()

            '--> Mise à jour du TreeView
            Frm_PMX.AffichageTViewChk()

            '--> Fermeture
            Me.Close()

        ElseIf TabPage_OpenProject.Visible Then     '==> OUVRIR PROJET

            If ListBox_RecentFiles.SelectedIndices.Count > 0 Then   '--> un fichier récent séléctionné

                Dim FileName As String = ListBox_RecentFiles.SelectedItem

                Ouverture_FichierRecent(FileName)

            End If

        End If

    End Sub

    Private Sub Ouverture_FichierRecent(ByVal FileName As String)
        '--------------------------------------------------------------------
        '   Ouverture d'un fichier récent 
        '--------------------------------------------------------------------

        '--> Fichier n'existe plus
        If Not File.Exists(FileName) Then
            MsgBox("Le fichier n'existe pas | File doesn't exist : " & FileName, MsgBoxStyle.Critical)

            '--> Suppression de la liste
            If LogicielFichiers.RecentFiles.Contains(FileName) Then LogicielFichiers.RecentFiles.Remove(FileName)
            '--> MAJ de la liste
            'Frm_MAIN.AffichageRecentFiles()
            RemplirListe_RecentFiles()

        Else

            Me.Close()

            '--> Lecture du fichier
            'ReadInFile(FileName)

        End If

    End Sub

    Private Sub Frm_Ouverture_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        '--> Fermeture de la fenêtre AjoutSoudure ouverte dans la fenêtre
        ' sinon bug à la prochaine ouverture de la fenêtre AjoutSoudure

        Frm_AjoutePP.Close()

    End Sub

#End Region

End Class