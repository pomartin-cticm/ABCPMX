Imports System.IO
Imports PMXMoteur2

Public Class Frm_NoteCalcul

#Region " Variables "

    '--> Gestion de la NDC
    Private iPageEncours As Integer         'Page à afficher
    Private PageAffichee As Integer         'Numéro de la page affiché en bas
    Private nbPageAffiche As Integer        'Nbre de page affiché à l'écran
    Private YTopPageEncours As Single       'Position du Top de la feuille dans le Graphics
    Private nbPages As Integer              'Nombre de pages
    Private LPage As Single                 'Longueur d'une page

    '--> Langues, Somaire, Options
    Private ReadOnly LangueEnCours As String
    Private ReadOnly tabLangue As New List(Of String)
    'Private ReadOnly tabAbbrege As New List(Of String)
    Private ReadOnly ToolLangue As New List(Of ToolStripMenuItem)
    Private ReadOnly ToolSommaire As New List(Of ToolStripMenuItem)
    'Private ToolNote As New List(Of ToolStripMenuItem)
    Private PageTitre() As Integer

    Private lBuild As Boolean = False       'Fenêtre en construction
    Private lResize As Boolean = False      'Fenêtre en train de changer de taille
    Private lLoad As Boolean = False        'Fenêtre lancée
    Private lControl As Boolean = False     'Touche 'Ctrl' enfoncée

    Private saveAs As String                'texte conversion PDF

    '--> Printer
    Private iBoucle As Integer
    Private lFirst As Boolean

#End Region

#Region "=== Ouverture Fenetre ==="

    Private Sub Frm_NoteCalcul_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '--------------------------------------------------------------------
        '   Ouverture de la fenêtre
        '--------------------------------------------------------------------

        lLoad = True

        '--> Fenêtre au centre de l'écran
        Me.Location = LocationForm

        '--> Pas visible mais utilisé dans la NDC
        Me.Btn_Logo.Visible = False
        Me.Btn_Correct.Visible = False
        Me.Btn_Error.Visible = False

        ''--> On affiche pas le bouton si aucune liste de torseurs est définie dans le projet
        'If Not MyProjct.ListeTorseurDefined Then Btn_Options.Visible = False

        '--> Initialisation de l'interface
        GestionLangue()
        InitialisationFenetre()

        Me.VScrollBar_NDC.Select()

    End Sub

    Public Sub GestionLangue()
        '--------------------------------------------------------------------
        '   Affichage du texte de la fenêtre dans la langue séléctionnée
        '--------------------------------------------------------------------

        If File.Exists(LogicielFichiers.Langue) Then
            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CALCULATIONSHEET")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")

                Me.TSbtn_Fermer.ToolTipText = Bloc("CLOSE")

                '--> Déplacement dans la NDC
                Me.Btn_PageDeb.ToolTipText = Bloc("FIRST")
                Me.Btn_PagePrec.ToolTipText = Bloc("PREVIOUS")
                Me.Btn_PageSuiv.ToolTipText = Bloc("NEXT")
                Me.Btn_PageFin.ToolTipText = Bloc("LAST")

                '--> Zoom avant & arrière
                Me.Btn_Zoomer.ToolTipText = Bloc("ZOOMIN")
                Me.Btn_Dezoomer.ToolTipText = Bloc("ZOOMOUT")

                '--> Volet de navigation
                Me.Btn_Navigation.ToolTipText = Bloc("NAVPAN")

                '--> Impression et conversion PDF
                Me.Btn_Imprimer.ToolTipText = Bloc("PRINT") & " (Ctrl+P)"
                Me.Btn_ToPDF.ToolTipText = Bloc("EXPORTPDF") & " (Ctrl+E)"
                Me.saveAs = Bloc("SAVEAS")

                '--> Langues
                Me.Btn_Langue.Text = Bloc("LANGUAGE")
                'Me.Btn_Langue.ToolTipText = Bloc("LANGUAGE")

                '--> Sommaires
                Me.Btn_Sommaire.Text = Bloc("SUMMARY")
                'Me.Btn_Sommaire.ToolTipText = Bloc("SUMMARY")

                '--> Options - Type d'affichage dans la NDC
                Me.Btn_Options.Text = Bloc("BTNOPTIONS")
                Me.Btn_Options.ToolTipText = Bloc("BTNOPTIONS")
                Me.Btn_UltraSynthese.Text = Bloc("BTNULTRA")
                Me.Btn_UltraSynthese.ToolTipText = Bloc("BTNULTRA")
                Me.Btn_Synthese.Text = Bloc("BTNSYNTHESE")
                Me.Btn_Synthese.ToolTipText = Bloc("BTNSYNTHESE")
                Me.Btn_Complet.Text = Bloc("BTNCOMPLET")
                Me.Btn_Complet.ToolTipText = Bloc("BTNCOMPLET")
                Me.Btn_Detail.Text = Bloc("BTNDETAILED")
                Me.Btn_Detail.ToolTipText = Bloc("BTNDETAILED")
                Me.Btn_DessinPoutre.Text = Bloc("BTNDRAWING")
                Me.Btn_DessinPoutre.ToolTipText = Bloc("BTNDRAWING")

                '--> Navigation
                Me.Label_Navigation.Text = Bloc("NAVIGATION").ToUpper

                '--> Zoom
                Me.lbl_Zoom.Text = Bloc("ZOOM") + " :"

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_NoteCalcul/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub InitialisationFenetre()

        lBuild = True

        '--> Général
        Me.Icon = Frm_PMX.Icon

        '--> Initialisations
        nbPageAffiche = 1
        iPageEncours = 0
        YTopPageEncours = 0
        PageAffichee = iPageEncours + 1

        '--> Préparation de la note de calcul
        MyNote.InitialisationAffichage(Me.img_Note.CreateGraphics, Me.img_Note.Width)

        GestionObjets()

        '--[ Recuperation des langues disponibles pour la note de calcul

        For iLangue As Integer = 0 To LogicielInfo.ListeLangueNDC.Count - 1

            ToolLangue.Add(New ToolStripMenuItem(LogicielInfo.ListeLangueNDC(iLangue)))
            Me.Btn_Langue.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {ToolLangue(iLangue)})
            AddHandler ToolLangue(iLangue).Click, AddressOf Me.GestionMenuLangue

        Next

        '--> Langue séléctionnée
        ToolLangue(LogicielOptions.IndLangueNDC).Checked = True

        '--[ Menu Détail de la note

        Select Case LogicielInfo.DetailNDC
            'Case Enum_NiveauDetailNDC.UltraSynthese
            '    Me.Btn_UltraSynthese.Checked = True
            '    Me.StatusDetail.Text = Me.Btn_UltraSynthese.Text
            Case Enum_NiveauDetailNDC.Synthese
                Me.Btn_Synthese.Checked = True
                Me.StatusDetail.Text = Me.Btn_Synthese.Text
            Case Enum_NiveauDetailNDC.Complete
                Me.Btn_Complet.Checked = True
                Me.StatusDetail.Text = Me.Btn_Complet.Text
                'Case Enum_NiveauDetailNDC.Detaillee
                '    Me.Btn_Detail.Checked = True
                '    Me.StatusDetail.Text = Me.Btn_Detail.Text
                'Case Enum_NiveauDetailNDC.DessinPoutre
                '    Me.Btn_DessinPoutre.Checked = True
                '    Me.StatusDetail.Text = Me.Btn_DessinPoutre.Text
        End Select

        '--[ Pied de page 
        Me.StatusPage.Text = CStr(PageAffichee) & "/" & CStr(nbPages)   'Page
        If MyProjet.Nom <> "" Then                               'Nom du projet
            Me.StatusBeam.Visible = True
            Me.StatusBeam.Text = MyProjet.Nom
        Else
            Me.StatusBeam.Visible = False
        End If
        Me.StatusLangue.Text = LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC)  'Langue

        '--[ Création et activation du sommaire
        ActiveSommaire()

        '--[ Initialisation des paramètres de la fenêtre

        'taille minimum du panel contenant le dessin de la NDC
        Me.SplitContainer_NDC.Panel2MinSize = 300

        'taille de la fenêtre
        If lMaximised Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
        Me.Height = HeightForm
        Me.Width = WidthForm

        'volet de navigation
        Me.SplitContainer_NDC.SplitterDistance = WidthNavPane
        Me.SplitContainer_NDC.Panel1Collapsed = Not lNavPane
        Me.Button_OuvrirNav.Visible = Not lNavPane
        Me.Btn_Navigation.Checked = lNavPane

        'zoom
        Me.TrackBar_Zoom.Value = ZoomValue

        lBuild = False

    End Sub

    Private Sub GestionObjets()
        '----------------------------------
        '
        '   MAJ paramètres de la note
        '   MAJ vScroolBar
        '
        '---------------------------------

        '--> Parametres de la Note
        nbPages = MyNote.NombrePages            'Nbre de page
        LPage = RAPPORTA4 * Me.img_Note.Width   'Longueur d'une page

        '--> Reinitialisation des objets

        If lBuild Or lResize Then

            If lBuild Then Me.VScrollBar_NDC.Value = 0 'initialisation si construction fenêtre

            If Not (nbPages * LPage - Me.img_Note.Height < 0) Then 'Paramétrage du ScrollBar

                Me.VScrollBar_NDC.Maximum = nbPages * LPage - Me.img_Note.Height                        'Valeur la + grande 
                Me.VScrollBar_NDC.LargeChange = Me.VScrollBar_NDC.Maximum / MyNote.NombrePages / 10 * 3 'Grand déplacement
                Me.VScrollBar_NDC.SmallChange = Me.VScrollBar_NDC.LargeChange / 10                      'Petit déplacement

                '--[ Compenser Bug Objet VSCROLL
                Me.VScrollBar_NDC.Maximum += Me.VScrollBar_NDC.LargeChange - 1
            Else
                'correctif bug - obliger de laisser afficher scrollbar
                Me.VScrollBar_NDC.Value = 0
                Me.VScrollBar_NDC.Maximum = 0
            End If

        End If

    End Sub

    Private Sub ActiveSommaire()
        '----------------------------------------------------------------------------------------
        '
        '   30/06/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------
        '
        '   Recupère les titres et les place dans le menu Sommaire
        '
        '----------------------------------------------------------------------------------------

        Dim Titres() As String = Nothing
        Dim NiveauTitre() As Integer = Nothing
        Dim nbTitres As Integer

        Dim iLast1 As Integer = -1
        Dim iLast2 As Integer = -1
        Dim iTitre1 As Integer = 0

        MyNote.GetTitles(Titres, NiveauTitre, PageTitre, nbTitres)
        ToolSommaire.Clear()

        '--> Initialisation TreeView
        Me.TreeView_NDC.BeginUpdate()
        Me.TreeView_NDC.Nodes.Clear()
        AddHandler TreeView_NDC.NodeMouseClick, AddressOf Me.GestionSommaireTreeView

        For i As Integer = 0 To nbTitres - 1

            If NiveauTitre(i) = 1 Then '--> Ajout Titre principal

                ToolSommaire.Add(New ToolStripMenuItem(Titres(i)))

                TreeView_NDC.Nodes.Add("", Titres(i))
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Name = "S" & Format(i, "00")
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).ForeColor = BleuCTICM
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).NodeFont = New Font("Arial", 10, FontStyle.Underline Or FontStyle.Bold)


            ElseIf NiveauTitre(i) = 2 Then '--> Ajout Titre secondaire

                ToolSommaire.Add(New ToolStripMenuItem("-- " & Titres(i)))

                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Add("", Titres(i), 6)
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Name = "S" & Format(i, "00")
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).ForeColor = BleuCTICM
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).NodeFont = New Font("Arial", 8, FontStyle.Italic Or FontStyle.Bold)

            Else '--> Ajout sous-titre

                ToolSommaire.Add(New ToolStripMenuItem("   " & Titres(i)))

                If Titres(i).LastOrDefault = ":" Then
                    Titres(i) = Titres(i).Substring(0, Titres(i).Length - 2)
                End If
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes.Add("", Titres(i), 7)
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes.Count - 1).Name = "S" & Format(i, "00")
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes.Count - 1).ForeColor = GrisCTICM
                TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes(TreeView_NDC.Nodes(TreeView_NDC.Nodes.Count - 1).Nodes.Count - 1).Nodes.Count - 1).NodeFont = New Font("Arial", 7, FontStyle.Italic Or FontStyle.Bold)

            End If

            ToolSommaire(i).Name = "S" & Format(i, "00")
            AddHandler ToolSommaire(i).Click, AddressOf Me.GestionSommaire
            Me.Btn_Sommaire.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {ToolSommaire(i)})

            '--> Police et Couleur dans le Button_Sommaire
            If NiveauTitre(i) = 1 Then          '--> Ajout Titre principal
                Me.Btn_Sommaire.DropDownItems.Item(i).ForeColor = BleuCTICM
                Me.Btn_Sommaire.DropDownItems.Item(i).Font = New Font("Arial", 10, FontStyle.Underline Or FontStyle.Bold)
            ElseIf NiveauTitre(i) = 2 Then      '--> Ajout Titre secondaire
                Me.Btn_Sommaire.DropDownItems.Item(i).ForeColor = BleuCTICM
                Me.Btn_Sommaire.DropDownItems.Item(i).Font = New Font("Arial", 8, FontStyle.Italic Or FontStyle.Bold)
            Else                                '--> Ajout sous-titre
                Me.Btn_Sommaire.DropDownItems.Item(i).Visible = False
                'Me.Btn_Sommaire.DropDownItems.Item(i).ForeColor = GrisCTICM
                'Me.Btn_Sommaire.DropDownItems.Item(i).Font = New Font("Arial", 7, FontStyle.Italic Or FontStyle.Bold)
            End If

        Next

        '--> Fin Maj + Expansion de toutes les branches du TreeView
        Me.TreeView_NDC.EndUpdate()
        Me.TreeView_NDC.ExpandAll()
        '--> Premier noeud sélectionné 
        If Me.TreeView_NDC.Nodes.Count <> 0 Then Me.TreeView_NDC.SelectedNode = Me.TreeView_NDC.Nodes(0)

    End Sub

#End Region

#Region " Gestion Menu ToolStrip "

    Private Sub TSbtn_Fermer_Click(sender As Object, e As EventArgs) Handles TSbtn_Fermer.Click
        Me.Close()
    End Sub

    Private Sub GestionClickBarreOutils(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Btn_Imprimer.Click, Btn_PageSuiv.Click, Btn_PagePrec.Click, Btn_PageFin.Click,
        Btn_PageDeb.Click, Btn_ToPDF.Click, Btn_Zoomer.Click, Btn_Dezoomer.Click

        Dim ScrollValue As Single = Me.VScrollBar_NDC.Value

        Select Case sender.name

            Case Me.Btn_PageDeb.Name             '--[ Début

                Me.VScrollBar_NDC.Value = 0
                GestionScroll()

            Case Me.Btn_PagePrec.Name            '--[ Précédent

                ScrollValue -= LPage
                ScrollValue = Math.Max(ScrollValue, Me.VScrollBar_NDC.Minimum)
                Me.VScrollBar_NDC.Value = ScrollValue
                GestionScroll()

            Case Me.Btn_PageSuiv.Name            '--[ Suivant

                ScrollValue += LPage
                ScrollValue = Math.Min(ScrollValue, Me.VScrollBar_NDC.Maximum - Me.VScrollBar_NDC.LargeChange + 1)
                Me.VScrollBar_NDC.Value = ScrollValue
                GestionScroll()

            Case Me.Btn_PageFin.Name             '--[ Fin

                Me.VScrollBar_NDC.Value = Math.Min((nbPages - 1) * LPage, Me.VScrollBar_NDC.Maximum - Me.VScrollBar_NDC.LargeChange + 1)
                GestionScroll()

            Case Btn_Dezoomer.Name                 '--[ Dézoomer l'image

                If TrackBar_Zoom.Value - 10 >= TrackBar_Zoom.Minimum Then
                    TrackBar_Zoom.Value -= 10
                Else
                    TrackBar_Zoom.Value = 0
                End If

            Case Btn_Zoomer.Name                 '--[ Zoomer l'image

                If TrackBar_Zoom.Value + 10 <= TrackBar_Zoom.Maximum Then
                    TrackBar_Zoom.Value += 10
                Else
                    TrackBar_Zoom.Value = 100
                End If

            Case Me.Btn_Imprimer.Name            '--[ Bouton pour imprimer

                GestionImprimer()

            Case Me.Btn_ToPDF.Name               '--[ Bouton pour convertir NDC en PDF

                ConversionPDF()

        End Select

    End Sub

#End Region

#Region " Dessin de la NDC "

    Private Sub Img_Note_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Note.Paint

        MyNote.DrawPage(iPageEncours, nbPageAffiche, e.Graphics, New SolidBrush(Color.Black), False, True,
                        Me.img_Note.Width, Me.img_Note.Height, YTopPageEncours)

    End Sub

#End Region

#Region " Gestion de l'impression "

    Private Sub GestionImprimer()
        '==> FONCTION DE PARAMETRAGE DE L'IMPRESSION <=='

        Try

            '--> Paramètres de l'impression
            Me.PrintDialog.AllowSomePages = True
            Me.PrintDialog.AllowCurrentPage = True
            Me.PrintDialog.AllowPrintToFile = False
            Me.PrintDialog.PrinterSettings.MinimumPage = 1
            Me.PrintDialog.PrinterSettings.MaximumPage = MyNote.NombrePages
            Me.PrintDialog.PrinterSettings.FromPage = 1
            Me.PrintDialog.PrinterSettings.ToPage = MyNote.NombrePages

            Dim Result As DialogResult = Me.PrintDialog.ShowDialog()

            If Result = Windows.Forms.DialogResult.OK Then

                Me.PrintDocument.PrinterSettings = Me.PrintDialog.PrinterSettings
                Select Case Me.PrintDialog.PrinterSettings.PrintRange
                    Case Printing.PrintRange.AllPages
                        Me.PrintDocument.PrinterSettings.FromPage = 1
                        Me.PrintDocument.PrinterSettings.ToPage = MyNote.NombrePages
                    Case Printing.PrintRange.CurrentPage
                        Me.PrintDocument.PrinterSettings.FromPage = iPageEncours + 1
                        Me.PrintDocument.PrinterSettings.ToPage = iPageEncours + 1
                    Case Printing.PrintRange.SomePages
                        'Rien
                End Select

                Me.PrintDocument.DocumentName = Me.PrintDocument.PrinterSettings.ToPage

                '--> Lancement de l'impression
                Dim RememberPageEnCours As Integer = iPageEncours

                iBoucle = 1
                iPageEncours = Me.PrintDialog.PrinterSettings.FromPage - 1
                lFirst = True

                If Me.PrintDocument.PrinterSettings.PrinterName = PrintPDFName Then '--> Conversion PDF

                    '--> Paramètre de la fenêtre d'enregistrement du fichier
                    Me.SaveFileDialog_PDF.Filter = "|*.pdf"
                    Me.SaveFileDialog_PDF.Title = saveAs
                    If MyProjet.Nom <> "" Then
                        Me.SaveFileDialog_PDF.FileName = MyProjet.Nom
                    Else
                        Me.SaveFileDialog_PDF.FileName = ""
                    End If

                    If Me.SaveFileDialog_PDF.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        '--> Paramètre pour la conversion en PDF
                        Me.PrintDocument.DocumentName = Me.PrintDocument.PrinterSettings.ToPage & " (" & Me.Btn_ToPDF.ToolTipText & ")"
                        Me.PrintDocument.PrinterSettings.PrintToFile = True
                        Me.PrintDocument.PrinterSettings.PrintFileName = Me.SaveFileDialog_PDF.FileName
                    Else
                        Exit Sub
                    End If

                End If

                '--> Impression
                Me.PrintDocument.Print()

                '--> Réafficher la note dans la fenetre
                iPageEncours = RememberPageEnCours
                MyNote.InitialisationAffichage(Me.img_Note.CreateGraphics, Me.img_Note.Width)
                Me.img_Note.Invalidate()

            End If

        Catch ex As Exception

            MsgBox("Erreur impression | Error print ", MsgBoxStyle.Critical, "Frm_NoteCalcul/GestionImprimer")

        End Try

    End Sub

    Private Sub ConversionPDF()
        '---------------------------------------------------------------------------
        '
        '   Création BD - 20/11/19 : Conversion directe de la NDC vers fichier PDF 
        '
        '---------------------------------------------------------------------------

        Try

            '--> Paramètres pour la conversion en PDF
            Me.PrintDocument.PrinterSettings.MinimumPage = 1
            Me.PrintDocument.PrinterSettings.MaximumPage = MyNote.NombrePages
            Me.PrintDocument.PrinterSettings.FromPage = 1
            Me.PrintDocument.PrinterSettings.ToPage = MyNote.NombrePages
            Me.PrintDocument.PrinterSettings.Copies = 1
            Me.PrintDocument.PrinterSettings.PrinterName = PrintPDFName
            Me.PrintDocument.PrinterSettings.PrintToFile = True
            Me.PrintDocument.DocumentName = MyNote.NombrePages & " " & Me.Btn_ToPDF.ToolTipText

            Dim RememberPageEnCours As Integer = iPageEncours

            '--> Lancement conversion
            iBoucle = 1
            iPageEncours = 0
            lFirst = True

            '--> Fichier d'enregistrement du PDF
            Me.SaveFileDialog_PDF.Filter = "|*.pdf"
            Me.SaveFileDialog_PDF.Title = saveAs

            If MyProjet.Nom <> "" Then
                Me.SaveFileDialog_PDF.FileName = MyProjet.Nom
            Else
                Me.SaveFileDialog_PDF.FileName = ""
            End If
            If Me.SaveFileDialog_PDF.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.PrintDocument.PrinterSettings.PrintFileName = Me.SaveFileDialog_PDF.FileName
            Else
                Exit Sub
            End If

            '--> Impression
            Me.PrintDocument.Print()

            '--> Réafficher la note dans la fenetre
            iPageEncours = RememberPageEnCours
            MyNote.InitialisationAffichage(Me.img_Note.CreateGraphics, Me.img_Note.Width)
            Me.img_Note.Invalidate()

        Catch ex As Exception

            MsgBox("Erreur conversion PDF | Error convert PDF", MsgBoxStyle.Critical, "Frm_NoteCalcul/ConversionPDF")

        End Try

    End Sub

    Private Sub PrintDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage
        '==> FONCTION D'IMPRESSION <=='

        '--> Déclaration
        Dim nbPages As Integer = MyNote.NombrePages
        Dim sWi As Single = e.PageBounds.Width
        Dim sHi As Single = e.PageBounds.Height

        '--> Première page
        If lFirst Then
            MyNote.InitialisationAffichage(e.Graphics, sWi)
        End If

        '--> Dessin de la page
        MyNote.DrawPage(iPageEncours, 1, e.Graphics, New SolidBrush(Color.Black), True, lFirst, sWi, sHi, 0)
        lFirst = False

        '--> Traitement suite
        If iPageEncours < Me.PrintDocument.PrinterSettings.ToPage - 1 Then
            iPageEncours += 1
            e.HasMorePages = True
        Else
            If iBoucle < Me.PrintDocument.PrinterSettings.Copies Then
                iBoucle += 1
                iPageEncours = Me.PrintDocument.PrinterSettings.FromPage - 1
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If
        End If

    End Sub

#End Region

#Region " Gestion du Scroll "

    Private Sub GestionMouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        '==> GESTION DU SCROLL DANS LA FENÊTRE <=='

        If Not lControl Then    '--> Gestion normale du scroll

            Dim ScrollValue As Integer = Me.VScrollBar_NDC.Value

            'ScrollValue += -e.Delta / 120 * Me.VScrollBar1.LargeChange
            ScrollValue += -e.Delta / 250 * Me.VScrollBar_NDC.LargeChange  '--> Scroll plus fluide

            '--> Empeche les déplacement en dehors des bornes du document
            If ScrollValue < 0 Then ScrollValue = 0
            Dim MaxiV As Integer = Me.VScrollBar_NDC.Maximum - Me.VScrollBar_NDC.LargeChange + 1
            If ScrollValue > MaxiV Then ScrollValue = MaxiV

            '--> Modification de la valeur
            Me.VScrollBar_NDC.Value = ScrollValue

            GestionScroll()

        Else '--> Touche Ctrl enfoncé + Scroll souris 

            If e.Delta > 0 Then '--> Réduit le zoom
                If TrackBar_Zoom.Value + 5 <= TrackBar_Zoom.Maximum Then
                    TrackBar_Zoom.Value += 5
                Else
                    TrackBar_Zoom.Value = 100
                End If
            Else                '--> Augmente le zoom
                If TrackBar_Zoom.Value - 5 >= TrackBar_Zoom.Minimum Then
                    TrackBar_Zoom.Value -= 5
                Else
                    TrackBar_Zoom.Value = 0
                End If
            End If

        End If

    End Sub

    Private Sub GestionScroll()
        '----------------------------------------------------------------
        '
        '   Gestion du scroll bar
        '
        '----------------------------------------------------------------

        Dim HFinPage As Single

        '--[ Calcul de la position du scroll

        Dim iPage As Integer

        iPage = Math.Floor(Me.VScrollBar_NDC.Value / LPage)
        If iPage > nbPages - 1 Then iPage = nbPages - 1

        '--[ Calcul de la position supérieure de la page en cours

        YTopPageEncours = (iPage) * LPage - Me.VScrollBar_NDC.Value

        '--[ Numérotatio de la page

        iPageEncours = iPage
        PageAffichee = iPageEncours + 1
        'If Math.Abs(YTopPageEncours) / LPage > 0.97 Then PageAffichee += 1 MODIF BD
        If Math.Abs(YTopPageEncours) / LPage > 0.5 Then PageAffichee += 1

        Me.StatusPage.Text = CStr(PageAffichee) & "/" & CStr(nbPages)

        '--[ Calcul de la position supérieure de la page en cours

        YTopPageEncours = (iPage) * LPage - Me.VScrollBar_NDC.Value

        '--[ Combien de page affichable à l'écran ?

        If iPageEncours = nbPages - 1 Then
            nbPageAffiche = 1
        Else

            nbPageAffiche = 1

            HFinPage = YTopPageEncours + LPage

            Do While (HFinPage < Me.img_Note.Height) And (nbPages > 1)
                HFinPage += LPage
                nbPageAffiche += 1
            Loop

        End If

        '--[ On redessine

        Me.img_Note.Invalidate()

    End Sub

    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar_NDC.Scroll

        If Not lBuild Then GestionScroll()

    End Sub

    Private Sub TableLayoutPanel_NDC_Click(sender As Object, e As EventArgs) Handles TableLayoutPanel_NDC.Click, img_Note.Click

        '--> Récupérer le focus sur la scroll bar
        Me.VScrollBar_NDC.Select()

    End Sub

#End Region

#Region " Gestion des Menus Sommaire/Langue/Options "

    Private Sub GestionMenuLangue(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '-----------------------------------------------------------------------
        '   Modification de la langue du contenu de la note de calcul
        '-----------------------------------------------------------------------

        '--> Si langue différente
        If LogicielOptions.IndLangueNDC <> Array.IndexOf(LogicielInfo.ListeLangueNDC, sender.text) Then

            '--> Modif Checked
            For i As Integer = 0 To LogicielInfo.ListeLangueNDC.Count - 1
                ToolLangue(i).Checked = False
            Next
            ToolLangue(Array.IndexOf(LogicielInfo.ListeLangueNDC, sender.text)).Checked = True

            '--> Modif langue 
            LogicielOptions.IndLangueNDC = Array.IndexOf(LogicielInfo.ListeLangueNDC, sender.text)
            InitialiseLNGFileName_NDC()

            '--> Rechargement du contenu de la NDC
            AAA_GenereNOTEdeCALCUL(MyProjet)

            '--> Sommaire
            Me.Btn_Sommaire.DropDownItems.Clear()
            ActiveSommaire()

            '--> Pied de page
            Me.StatusLangue.Text = sender.text

            '--> MAJ Dessin
            Me.img_Note.Invalidate()

        End If

    End Sub

    Private Sub GestionSommaire(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '--> Fonction utilisé par le bouton 'Sommaire'

        Dim Indice As Integer = Val(sender.name.substring(1))

        Me.VScrollBar_NDC.Value = Math.Min(PageTitre(Indice) * LPage, Me.VScrollBar_NDC.Maximum - Me.VScrollBar_NDC.LargeChange + 1)

        GestionScroll()

    End Sub

    Private Sub GestionSommaireTreeView(ByVal treeView As TreeView, ByVal e As TreeNodeMouseClickEventArgs)
        '--> Fonction utilisé par le TreeView

        Dim Indice As Integer = Val(e.Node.Name.Substring(1))

        Me.VScrollBar_NDC.Value = Math.Min(PageTitre(Indice) * LPage, Me.VScrollBar_NDC.Maximum - Me.VScrollBar_NDC.LargeChange + 1)

        GestionScroll()

    End Sub

    Private Sub GestionOptions(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Synthese.Click, Btn_Detail.Click, Btn_Complet.Click, Btn_UltraSynthese.Click, Btn_DessinPoutre.Click
        '-----------------------------------------------------------------------
        '   Modification de l'affichage de la NDC
        '-----------------------------------------------------------------------

        Dim Name As String = sender.name
        Dim OldDetail As Enum_NiveauDetailNDC = LogicielInfo.DetailNDC

        'Me.Btn_UltraSynthese.Checked = (Name = Me.Btn_UltraSynthese.Name)
        Me.Btn_Synthese.Checked = (Name = Me.Btn_Synthese.Name)
        Me.Btn_Complet.Checked = (Name = Me.Btn_Complet.Name)
        'Me.Btn_Detail.Checked = (Name = Me.Btn_Detail.Name)
        'Me.Btn_DessinPoutre.Checked = (Name = Me.Btn_DessinPoutre.Name)

        Select Case Name
            'Case Me.Btn_Detail.Name : InfoLogiciel.DetailNDC = Enum_NiveauDetailNDC.Detaillee
            Case Me.Btn_Complet.Name : LogicielInfo.DetailNDC = Enum_NiveauDetailNDC.Complete
            Case Me.Btn_Synthese.Name : LogicielInfo.DetailNDC = Enum_NiveauDetailNDC.Synthese
                'Case Me.Btn_UltraSynthese.Name : InfoLogiciel.DetailNDC = Enum_NiveauDetailNDC.UltraSynthese
                'Case Me.Btn_DessinPoutre.Name : InfoLogiciel.DetailNDC = Enum_NiveauDetailNDC.DessinPoutre
        End Select

        If LogicielInfo.DetailNDC <> OldDetail Then

            lBuild = True

            iPageEncours = 0

            '--> Rechargement du contenu de la NDC
            AAA_GenereNOTEdeCALCUL(MyProjet)

            Me.Btn_Sommaire.DropDownItems.Clear() 'R 21-014 - Bed - 24/09/21
            ActiveSommaire()

            GestionObjets()
            GestionScroll()

            'Mise à jour de l'affichage de la note
            Me.img_Note.Invalidate()

            Me.StatusDetail.Text = sender.text

            lBuild = False

        End If

    End Sub

#End Region

#Region " Changement de taille de la fenêtre "

    Private Sub Img_Note_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles img_Note.Resize
        '==> CHANGEMENT DE TAILLE DE LA FENÊTRE <=='

        lResize = True '--> début du changement de taille

        If lLoad Then

            '--> Récupération de la position du scrool
            Dim OldValueScrollBar As Decimal = Me.VScrollBar_NDC.Value         'Utilisation de Decimal pour calcul avec grand nombre
            Dim OldMaximumScrollBar As Decimal = Me.VScrollBar_NDC.Maximum

            '--> MAJ des paramètres de dessin
            MyNote.InitialisationAffichage(Me.img_Note.CreateGraphics, Me.img_Note.Width)
            GestionObjets()

            '--> Modification de la valeur du scroll pour retrouver la position initiale (au début du changement de taille)
            If OldMaximumScrollBar <> 0 Then
                Dim result As Decimal = OldValueScrollBar * Me.VScrollBar_NDC.Maximum / OldMaximumScrollBar
                Me.VScrollBar_NDC.Value = result
            Else
                Me.VScrollBar_NDC.Value = 0
            End If

            '--> MAJ des paramètres + MAJ dessin
            GestionScroll()

        End If

    End Sub

    Private Sub Frm_NoteCalcul_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        '==> CHANGEMENT DE TAILLE DE LA FENÊTRE <=='

        '--> Ajout d'un Zoom lorsque la fenêtre est maximisée
        If (Me.WindowState = FormWindowState.Maximized) And Me.TrackBar_Zoom.Value = 100 Then
            Me.TrackBar_Zoom.Value = 50
        End If

        '--> Vérification que le bloc navigation ne prenne pas trop de place par rapport à la NDC
        If Me.SplitContainer_NDC.Width - Me.SplitContainer_NDC.Panel1.Width < 300 Then
            Me.SplitContainer_NDC.SplitterDistance = Me.SplitContainer_NDC.Width - 300
        End If

        '--> MAJ dessin du trait de sépération 
        Me.SplitContainer_NDC.Invalidate()

    End Sub

    Private Sub Frm_NoteCalcul_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        '==> FIN CHANGEMENT DE TAILLE DE LA FENÊTRE <=='

        lResize = False

    End Sub

#End Region

#Region " Gestion du zoom "

    Private Sub TrackBar_Zoom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar_Zoom.ValueChanged
        '==> ZOOM DE LA NDC <=='

        If lLoad Then

            '--> Taille de la colonne du dessin
            Dim widthImage = TableLayoutPanel_NDC.ColumnStyles(1).Width

            '--> Reduction ou augmentation de la taille des deux colonnes autour du dessin
            Me.TableLayoutPanel_NDC.ColumnStyles(0).Width = widthImage - widthImage * (TrackBar_Zoom.Value / 100)
            Me.TableLayoutPanel_NDC.ColumnStyles(2).Width = widthImage - widthImage * (TrackBar_Zoom.Value / 100)

            '--> Affichage des boutons pour zoomer et dézoomer
            If TrackBar_Zoom.Value = 100 Then
                Btn_Zoomer.Enabled = False
            ElseIf TrackBar_Zoom.Value = 0 Then
                Btn_Dezoomer.Enabled = False
            Else
                Btn_Zoomer.Enabled = True
                Btn_Dezoomer.Enabled = True
            End If

        End If

    End Sub

    Private Sub Frm_NoteCalcul_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        '==> Utilisation avec le scroll - Fonction GestionMouseWheel <=='

        Dim ScrollValue As Single = Me.VScrollBar_NDC.Value

        If e.KeyCode = Keys.ControlKey Then     '--> Touche Ctrl enfoncé
            lControl = True
        End If

        '--> CONTROLE
        If e.KeyCode = Keys.P And lControl Then '--> Ctrl + P : Impression
            GestionImprimer()
            lControl = False
        ElseIf e.KeyCode = Keys.e And lControl Then '--> Ctrl + E : Export PDF
            ConversionPDF()
            lControl = False
        End If

    End Sub

    Private Sub Frm_NoteCalcul_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp

        If e.KeyCode = Keys.ControlKey Then '--> Relachement de la touche Ctrl
            lControl = False
            '--> MAJ dessin du trait de sépération 
            Me.SplitContainer_NDC.Invalidate()
        End If

    End Sub

#End Region

#Region " Gestion du volet de navigation "

    Private Sub SplitContainer_NDC_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer_NDC.Paint

        '--> Trait entre les deux panels
        e.Graphics.FillRectangle(Brushes.Gray, SplitContainer_NDC.SplitterRectangle)

    End Sub

    Private Sub Button_FermerNav_Click(sender As Object, e As EventArgs) Handles Button_FermerNav.Click

        Me.SplitContainer_NDC.Panel1Collapsed = True
        Me.Button_OuvrirNav.Visible = True
        Me.Btn_Navigation.Checked = False

    End Sub

    Private Sub Button_OuvrirNav_Click(sender As Object, e As EventArgs) Handles Button_OuvrirNav.Click

        Me.SplitContainer_NDC.Panel1Collapsed = False
        Me.Button_OuvrirNav.Visible = False
        Me.Btn_Navigation.Checked = True

        '--> Vérification que le bloc navigation ne prenne pas trop de place par rapport à la NDC
        If Me.SplitContainer_NDC.Width - Me.SplitContainer_NDC.Panel1.Width < 300 Then
            Me.SplitContainer_NDC.SplitterDistance = Me.SplitContainer_NDC.Width - 300
        End If

    End Sub

    Private Sub StatusPage_Click(sender As Object, e As EventArgs) Handles StatusPage.Click, Btn_Navigation.Click

        Me.SplitContainer_NDC.Panel1Collapsed = Not Me.SplitContainer_NDC.Panel1Collapsed
        Me.Button_OuvrirNav.Visible = Me.SplitContainer_NDC.Panel1Collapsed
        Me.Btn_Navigation.Checked = Not Me.SplitContainer_NDC.Panel1Collapsed

        '--> Vérification que le bloc navigation ne prenne pas trop de place par rapport à la NDC
        If Me.SplitContainer_NDC.Width - Me.SplitContainer_NDC.Panel1.Width < 300 Then
            Me.SplitContainer_NDC.SplitterDistance = Me.SplitContainer_NDC.Width - 300
        End If

    End Sub

#End Region

#Region "=== Fermeture de la fenêtre ==="

    Private Sub Frm_NoteCalcul_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '--> Enregistrement des paramètres de la fenêtre

        'Taille de la fenêtre
        lMaximised = (Me.WindowState = FormWindowState.Maximized)
        If lMaximised Then      '--> Taille par défaut
            WidthForm = 750
            HeightForm = SystemInformation.WorkingArea.Height - 10
        Else
            WidthForm = Me.Width
            HeightForm = Me.Height
        End If
        'Volet de navigation
        lNavPane = Me.Btn_Navigation.Checked
        WidthNavPane = Me.SplitContainer_NDC.SplitterDistance

        'Zoom
        ZoomValue = Me.TrackBar_Zoom.Value


        'Position centrée avec la nouvelle taille
        LocationForm = New Point(SystemInformation.WorkingArea.Width / 2 - WidthForm / 2, SystemInformation.WorkingArea.Height / 2 - HeightForm / 2)

    End Sub



#End Region

End Class