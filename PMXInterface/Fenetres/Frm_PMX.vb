Imports System.IO
Imports PMXMoteur2

Public Class Frm_PMX

#Region " Variables locales "

    Dim lBuild As Boolean = True
    Private lOpenAvecFichier As Boolean = False

    Enum EnuFilleEnCours
        Accueil
        Portees
        Entraxes
        Dalle
        Section
        Enrobage
        Connexion
        Maintiens
        Etaiement
        Chargements
        Combinaisons
        Options
        Hivoss
    End Enum
    Dim FilleEnCours As EnuFilleEnCours = EnuFilleEnCours.Accueil

    '--> Gestion de l'affichage des projets et des poutres

    Dim tab_ChkSections As List(Of CheckBox)

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_PMX_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '--> Initialisation générale du logiciel

        InitialiseLogiciel()

        '--> Fenetre Juridique
        If My.Settings.lJuridique Then

            If Frm_Juridique.ShowDialog() = DialogResult.No Then
                Me.Close()
                Exit Sub
            End If

        End If

        '--> Préparation de la fenêtre

        GestionLangue()
        GestionStyle()

        '--> Affichage

        'Si le chemin d'un fichier est passé en argument
        '--> c'est-à-dire que le logiciel est ouvert par le biais d'un fichier sdx+
        If Environment.GetCommandLineArgs().Length = 2 Then
            If (Environment.GetCommandLineArgs(1) <> "") Then
                'Sauvegarde du chemin du fichier
                lOpenAvecFichier = True
                ' FileName = Environment.GetCommandLineArgs(1)
            End If
        End If

        If lOpenAvecFichier Then
            'ReadInFile(FileName)    '--> Ouverture du fichier 
        Else
            lBuild = False
            Frm_Ouverture.ShowDialog()    '--> Fenetre Ouverture

        End If
        AfficheFenetreEnCours()

    End Sub

    Private Sub GestionLangue()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_MAIN")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                '--> Boutons 'Fichier'
                Me.Btn_Files.Text = Bloc("FILE")
                Me.OpenToolStripMenuItem.Text = Bloc("OPEN") & "..."
                Me.NewToolStripMenuItem.Text = Bloc("NEW")
                Me.SaveToolStripMenuItem.Text = Bloc("SAVE")
                Me.SaveAsToolStripMenuItem.Text = Bloc("SAVEAS") & "..."
                Me.RecentFileToolStripMenuItem.Text = Bloc("RECENTFILES")
                Me.QuitToolStripMenuItem.Text = Bloc("EXIT")

                '--> Boutons 'Projet'

                Me.Btn_Project.Text = "Toto Projet"

                '=== CONTENU DE LA FENETRE =========================================================

                Me.Btn_New.ToolTipText = Bloc("NPROJET") & "..."

                '=== BARRE d'OUTILS POUR LES POUTRES

                Me.TSbtn_Accueil.ToolTipText = Bloc("TSBACCUEIL")
                Me.TSbtn_Portees.ToolTipText = Bloc("TSBPORTEES")
                'Me.TSbtn_Entraxe.ToolTipText = Bloc("TSBENTRAXE")

                Me.TSbtn_Dalle.ToolTipText = Bloc("TSBDALLE")
                Me.TSbtn_SectionA.ToolTipText = Bloc("TSBSECTIONA")
                Me.TSbtn_Enrobage.ToolTipText = Bloc("TSBENROBAGE")
                Me.TSbtn_Connexion.ToolTipText = Bloc("TSBCONNECTION")

                Me.TSbtn_Etaiement.ToolTipText = Bloc("TSBETAIEMENT")

                '=== MESSAGES GENERAUX

                ErreurCapacite_LNG = Bloc("ERRORCAPACITY")
                ErreurNonNul_LNG = Bloc("ERROREMPTYCELL")
                ErreurNonNum_LNG = Bloc("ERRORNONNUMERIC")
                ErreurHorsBornes_LNG = Bloc("ERROROUTBOUNDS")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_PMX/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Text = LogicielInfo.NomLogiciel

        Select Case LogicielInfo.Maitre
            Case EnuMaitre.CTICM
                Me.img_Logo.Image = Me.ContainerLogo.Images("Logo_CTICM")

        End Select


        Me.TLPan_Main.RowStyles(0).Height = 0
        Me.TLPan_Main.RowStyles(1).Height = 0

        ' Me.TLPan_Main.RowStyles(2).Height = 0

        Me.TLPan_Main.RowStyles(5).Height = 0
    End Sub

#End Region

#Region " Provisoire "

    Private Sub PaintPanel(sender As Object, e As PaintEventArgs)
        DessineBordurePanel(e.Graphics, sender.ClientRectangle.Width, sender.ClientRectangle.Height)
    End Sub

    Private Sub DessineBordurePanel(MyGr As Graphics, sWi As Single, sHi As Single)

        Dim xo, yo As Integer
        Dim xe, ye As Integer

        Dim PenGris As New Pen(Color.Gray)
        Dim PenBlanc As New Pen(Color.White)

        xo = sWi - 2
        xe = xo
        yo = 1
        ye = sHi - 1

        MyGr.DrawLine(PenGris, xo, yo, xe, ye)

        xo = sWi - 1
        xe = xo

        MyGr.DrawLine(PenBlanc, xo, yo, xe, ye)


    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        'Me.Split_Main.Panel1Collapsed = Not Me.CheckBox3.Checked
    End Sub


#End Region

#Region " Gestion Evènements sur Projets "
    Private Sub cmb_Projet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Projet.SelectedIndexChanged

    End Sub

    Private Sub AfficheProjetEnCours()



    End Sub

    Private Sub AffichePoutreEnCours()

    End Sub

    Private Sub ClickNouveauProjet(sender As Object, e As EventArgs) Handles btn_NewN.Click, Btn_New.Click

        Frm_AjoutePP.ShowDialog()

    End Sub

    Private Sub AjouterPoutre()

        '--> Ouverture de la fenêtre
        Dim dg As DialogResult = Frm_AjoutePP.ShowDialog()

        '--> Si ajout d'une nouvelle section
        If dg = DialogResult.OK Then

            '--> Affichage de la section créee
            'AffichageFenetreFille()

            '--> Mise à jour du TreeView
            AffichageTViewChk()

        End If
    End Sub


#End Region

#Region " Gestion des barres d'outils "

    Private Sub Btn_AddSection_Click(sender As Object, e As EventArgs) Handles Btn_AddSection.Click, Btn_AddSectionN.Click
        AjouterPoutre()
    End Sub


#End Region

#Region " Gestion Barre d'outils poutre "

    Private Sub GestionBoutonsMenuPoutre(sender As Object, e As EventArgs) Handles TSbtn_Portees.Click, TSbtn_Accueil.Click, TSbtn_Maintiens.Click, TSbtn_Etaiement.Click, TSbtn_SectionA.Click, TSbtn_Enrobage.Click, TSbtn_Dalle.Click, TSbtn_Connexion.Click, TSbtn_Hivoss.Click

        Select Case sender.name
            Case Me.TSbtn_Accueil.Name
                FilleEnCours = EnuFilleEnCours.Accueil
            Case Me.TSbtn_Portees.Name
                FilleEnCours = EnuFilleEnCours.Portees
                ' Case Me.TSbtn_Entraxe.Name
           '     FilleEnCours = EnuFilleEnCours.Entraxes

            Case Me.TSbtn_Dalle.Name
                FilleEnCours = EnuFilleEnCours.Dalle
            Case Me.TSbtn_SectionA.Name
                FilleEnCours = EnuFilleEnCours.Section
            Case Me.TSbtn_Enrobage.Name
                FilleEnCours = EnuFilleEnCours.Enrobage
            Case Me.TSbtn_Maintiens.Name
                FilleEnCours = EnuFilleEnCours.Maintiens
            Case Me.TSbtn_Etaiement.Name
                FilleEnCours = EnuFilleEnCours.Etaiement
            Case Me.TSbtn_Chargements.Name
                FilleEnCours = EnuFilleEnCours.Chargements

            Case Me.TSbtn_Hivoss.Name
                FilleEnCours = EnuFilleEnCours.Hivoss

        End Select
        AfficheFenetreEnCours()

    End Sub

    Private Sub AfficheFenetreEnCours()

        Select Case FilleEnCours
            Case EnuFilleEnCours.Portees
                If LogicielOptions.lFenetres Then
                    Frm_Portees.ShowDialog()
                Else
                    Frm_Portees.InitialiserFenetre()
                    Me.TLPan_ZoneDeSaisie.Controls.Add(Frm_Portees.pan_Main, 0, 1)
                End If

            Case EnuFilleEnCours.Dalle
                If LogicielOptions.lFenetres Then
                    Frm_Dalle.ShowDialog()

                End If
            Case EnuFilleEnCours.Section
                If LogicielOptions.lFenetres Then
                    Frm_SectionAcierStandard.ShowDialog()
                Else

                End If

            Case EnuFilleEnCours.Enrobage
                If LogicielOptions.lFenetres Then
                    Frm_Enrobage.ShowDialog()
                Else

                End If

            Case EnuFilleEnCours.Maintiens
                If LogicielOptions.lFenetres Then
                    Frm_Maintiens.ShowDialog()
                Else

                End If

            Case EnuFilleEnCours.Etaiement
                If LogicielOptions.lFenetres Then
                    Frm_Etaiement.ShowDialog()
                Else

                End If

        End Select

        MAJMainToolBar()

    End Sub

    Private Sub MAJMainToolBar()


        If MyProjet.Poutres(MyProjet.IndEnCours).lDonneesSauvees Then
                Me.TSbtn_SaveN.Image = ImgList_Menu.Images("Enregistrer_OK")
            Else
            If MyProjet.Poutres(MyProjet.IndEnCours).NouvellePoutre Then
                Me.TSbtn_SaveN.Image = ImgList_Menu.Images("EnregistrerVierge")
            Else

                Me.TSbtn_SaveN.Image = ImgList_Menu.Images("Enregistrer_NotOK")
            End If
        End If


    End Sub


#End Region

#Region " Barre d'outils principale "

    Private Sub TSbtn_SaveN_Click(sender As Object, e As EventArgs) Handles TSbtn_SaveN.Click

        MyProjet.Poutres(MyProjet.IndEnCours).lDonneesSauvees = True
        MAJMainToolBar()

    End Sub

#End Region

#Region " Affichage des poutres du projet "

    Public Sub AffichageTViewChk()

        If MyProjet.Poutres.Count = 0 Then Exit Sub

        Me.cmb_Projet.Items.Clear()
        Me.cmb_Projet.Items.Add(MyProjet.Nom)
        Me.cmb_Projet.SelectedIndex = 0

        Me.tab_ChkSections = New List(Of CheckBox)

        For i As Integer = 0 To MyProjet.Poutres.Count - 1

            Me.tab_ChkSections.Add(New CheckBox)
            Me.tab_ChkSections(i).Appearance = Appearance.Button
            Me.tab_ChkSections(i).Dock = DockStyle.Fill
            Me.tab_ChkSections(i).BackColor = SystemColors.ControlLight 'Me.ToolStrip_Menu_Section.BackColor
            Me.tab_ChkSections(i).ForeColor = SystemColors.WindowText

            Me.tab_ChkSections(i).Text = MyProjet.Poutres(i).Label
            Me.tab_ChkSections(i).Name = "MyX" & CStr(i)
            Me.tab_ChkSections(i).Tag = CStr(i)
            AddHandler Me.tab_ChkSections(i).CheckedChanged, AddressOf ChoixSection_CheckedChanged
            'AddHandler Me.tab_ChkSections(i).Paint, AddressOf chkBox_Section_Paint
        Next

        Me.TLPan_ListPoutres.RowCount = MyProjet.Poutres.Count + 1
        Me.TLPan_ListPoutres.Controls.Clear()
        Me.TLPan_ListPoutres.RowStyles.Clear()

        For i As Integer = 0 To MyProjet.Poutres.Count - 1

            Me.TLPan_ListPoutres.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
            'Me.TableLayoutPanel_TreeChk.RowStyles(i).SizeType = SizeType.Absolute
            'Me.TableLayoutPanel_TreeChk.RowStyles(i).Height = 40.0!
        Next

        Dim HCum = 0
        For i As Integer = 0 To MyProjet.Poutres.Count - 1
            Me.TLPan_ListPoutres.Controls.Add(Me.tab_ChkSections(i), 0, i)
            HCum += Me.TLPan_ListPoutres.RowStyles(i).Height
        Next
        Me.TLPan_ListPoutres.Height = HCum

        Me.tab_ChkSections(MyProjet.IndEnCours).Checked = True

    End Sub

    Private Sub ChoixSection_CheckedChanged(sender As Object, e As EventArgs)

        If lbuild Then Exit Sub
        Dim lChecked As Boolean = sender.checked
        lbuild = True

        DeselectionneTousChk(sender.name)

        Dim Indice As Integer = TraiteReal(sender.name.ToString.Substring(3))
        MyProjet.IndEnCours = Indice
        'AffichageFenetreFille()

        sender.checked = lChecked
        sender.backcolor = Color.Gold
        lbuild = False

    End Sub

    ''' <summary>
    ''' Déselectionne tous les checkbox de la fenêtre
    ''' </summary>
    '''  ''' <param name="SenderName">[E] Nom du chkbox qui n'est pas déselectionné</param>
    Private Sub DeselectionneTousChk(Optional SenderName As String = "")

        For Each o As Control In Me.TLPan_ListPoutres.Controls
            If TypeOf o Is CheckBox Then
                If o.Name <> SenderName Then
                    CType(o, CheckBox).Checked = False
                    CType(o, CheckBox).BackColor = Me.TLPan_ListPoutres.BackColor
                    'CType(o,CheckBox).
                End If
            End If
        Next

    End Sub



#End Region


End Class
