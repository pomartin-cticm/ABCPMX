Imports System.IO
Imports PMXMoteur2

Public Class Frm_Ouverture

#Region " Variables locales "

    Dim strFiltresExtension As String
    Dim strNonDispo As String
    Dim strSaveModif As String

    Public lNewP As Boolean = False

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
    ''' AffichageOptFeu du texte de la fenêtre dans la langue séléctionnée
    ''' </summary>
    Public Sub GestionLangue()

        If File.Exists(LogicielFichiers.Langue) Then
            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_START")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                strFiltresExtension = Bloc("FILE")

                Me.Text = LogicielInfo.NomLogiciel

                Me.TabPage_NewProject.Text = Bloc("NEWPROJECT")
                Me.TabPage_OpenProject.Text = Bloc("OPENPROJECT")

                Me.lbl_OpenFile.Text = Bloc("OPENPROJECT")
                Me.Button_OpenProject.Text = Bloc("BROWSEFILES")

                Me.lbl_RecentFiles.Text = Bloc("RECENTFILES")

                Me.Button_Valider.Text = Bloc("OK")

                strNonDispo = Bloc("NOTAVAILABLE") & Chr(13) & RemplaceDollar(Bloc("LATER"), LogicielInfo.Racine)

                strSaveModif = Bloc("SAVEPROJECT")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_Ouverture/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub InitialiserFenetre()
        '--------------------------------------------------------------------
        '   Initialisation de la fenêtre avec des paramètres par défaut
        '--------------------------------------------------------------------

        Me.Icon = Frm_PMX.Icon

        GestionStyle()

        '==> Nouveau Projet

        '  Frm_AjoutSectionN.InitialiserFenetre()
        '  Frm_AjoutSectionN.GestionLangue()
        Frm_AjoutePP.lOuverture = True
        Frm_AjoutePP.InitialiseFromOutside()
        TabPage_NewProject.Controls.Add(Frm_AjoutePP.pan_Choix)

        '==> Ouvrir Projet
        RemplirListe_RecentFiles()

    End Sub

    Private Sub GestionStyle()

        Me.lbl_OpenFile.BackColor = CouleurBackBandeaux
        Me.lbl_OpenFile.ForeColor = CouleurForeBandeaux

        Me.lbl_RecentFiles.BackColor = CouleurBackBandeaux
        Me.lbl_RecentFiles.ForeColor = CouleurForeBandeaux

        Me.TLpan_OpenProject.RowStyles(6).Height = 0
        Me.TLpan_OpenProject.RowStyles(8).Height = 0
        Me.TLpan_OpenProject.ColumnStyles(0).Width = 0
        Me.TLpan_OpenProject.ColumnStyles(4).Width = 0

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

#Region " Gestion Evenements "

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

    Private Sub GestionProjetEnCours(ByRef lFermer As Boolean)
        '--------------------------------------------------------------------
        '   02/07/2025 - Création - POM
        '--------------------------------------------------------------------
        '   Avant d'écraser les poutres du projet en cours, on demande si sauvegarde
        '--------------------------------------------------------------------
        '--------------------------------------------------------------------

        Dim Rep As DialogResult


        If Not MyProjet.lSaved Then

            Dim Message As String
            Message = RemplaceDollar(strSaveModif, MyProjet.Nom)

            Rep = DemandeConfirmationYesNoCancel(Message)
        Else
            Rep = Windows.Forms.DialogResult.Yes
        End If

        Select Case Rep
            Case Windows.Forms.DialogResult.Yes
                Frm_PMX.EnregistrerProjetEnCours()
                lFermer = True
            Case Windows.Forms.DialogResult.No
                lFermer = True
            Case Windows.Forms.DialogResult.Cancel
                lFermer = False
        End Select

        If lFermer Then

        End If

    End Sub

    Private Sub Button_Valider_Click(sender As Object, e As EventArgs) Handles Button_Valider.Click

        Dim lFermer As Boolean
        Dim lCancel As Boolean = False

        '--> Gestion d'un nouveau projet

        If Me.lNewP Then

            GestionProjetEnCours(lFermer)

            If Not lFermer Then
                lCancel = True
            Else
                MyProjet.Poutres.Clear()
            End If

        End If

        If Not lCancel Then

            If TabPage_NewProject.Visible Then          '==> NOUVEAU PROJET

                Dim lOK As Boolean

                '--> Ajout de la nouvelle poutre

                Frm_AjoutePP.TraitementSaisie(lOK)

                '--> AffichageOptFeu de la soudure créee
                '   Frm_MAIN.AffichageFenetreFille()

                If lOK Then
                    '--> Mise à jour du TreeView
                    Frm_PMX.AffichageTViewChk()

                    '--> Fermeture
                    Me.Close()
                Else
                    'MsgBox(strNonDispo)

                    GestionErrorsPMX("", "", strNonDispo, False)

                End If

            ElseIf TabPage_OpenProject.Visible Then     '==> OUVRIR PROJET

                If ListBox_RecentFiles.SelectedIndices.Count > 0 Then   '--> un fichier récent séléctionné

                    Dim FileName As String = ListBox_RecentFiles.SelectedItem

                    Ouverture_FichierRecent(FileName)

                End If

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

            Frm_PMX.ReadInFile(FileName)
            Frm_PMX.EnregistreDansFichiersRecents(FileName)
            Me.Close()

        End If

    End Sub

    Private Sub Frm_Ouverture_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        '--> Fermeture de la fenêtre AjoutSoudure ouverte dans la fenêtre
        ' sinon bug à la prochaine ouverture de la fenêtre AjoutSoudure

        Frm_AjoutePP.Close()

    End Sub


#End Region

End Class