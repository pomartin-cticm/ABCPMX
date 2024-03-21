<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_NoteCalcul
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_NoteCalcul))
        Me.ToolStrip_Menu = New System.Windows.Forms.ToolStrip()
        Me.Btn_PageDeb = New System.Windows.Forms.ToolStripButton()
        Me.Btn_PagePrec = New System.Windows.Forms.ToolStripButton()
        Me.Btn_PageSuiv = New System.Windows.Forms.ToolStripButton()
        Me.Btn_PageFin = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_Dezoomer = New System.Windows.Forms.ToolStripButton()
        Me.Btn_Zoomer = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_Navigation = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_Imprimer = New System.Windows.Forms.ToolStripButton()
        Me.Btn_ToPDF = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_Langue = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_Sommaire = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.Btn_Options = New System.Windows.Forms.ToolStripDropDownButton()
        Me.Btn_UltraSynthese = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_Synthese = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_Complet = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_Detail = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_DessinPoutre = New System.Windows.Forms.ToolStripMenuItem()
        Me.Btn_Logo = New System.Windows.Forms.ToolStripButton()
        Me.Btn_Correct = New System.Windows.Forms.ToolStripButton()
        Me.Btn_Error = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip_InfoZoom = New System.Windows.Forms.StatusStrip()
        Me.StatusPage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusBeam = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusLangue = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusDetail = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel_NDC = New System.Windows.Forms.Panel()
        Me.SplitContainer_NDC = New System.Windows.Forms.SplitContainer()
        Me.Panel_Nav = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Navigation = New System.Windows.Forms.TableLayoutPanel()
        Me.TreeView_NDC = New System.Windows.Forms.TreeView()
        Me.Button_FermerNav = New System.Windows.Forms.Button()
        Me.Label_Navigation = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_NDC = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_OuvrirNav = New System.Windows.Forms.Button()
        Me.img_Note = New System.Windows.Forms.PictureBox()
        Me.VScrollBar_NDC = New System.Windows.Forms.VScrollBar()
        Me.TrackBar_Zoom = New System.Windows.Forms.TrackBar()
        Me.lbl_Zoom = New System.Windows.Forms.Label()
        Me.PrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog = New System.Windows.Forms.PrintDialog()
        Me.SaveFileDialog_PDF = New System.Windows.Forms.SaveFileDialog()
        Me.ToolStrip_Menu.SuspendLayout()
        Me.StatusStrip_InfoZoom.SuspendLayout()
        Me.Panel_NDC.SuspendLayout()
        CType(Me.SplitContainer_NDC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer_NDC.Panel1.SuspendLayout()
        Me.SplitContainer_NDC.Panel2.SuspendLayout()
        Me.SplitContainer_NDC.SuspendLayout()
        Me.Panel_Nav.SuspendLayout()
        Me.TableLayoutPanel_Navigation.SuspendLayout()
        Me.TableLayoutPanel_NDC.SuspendLayout()
        CType(Me.img_Note, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar_Zoom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip_Menu
        '
        Me.ToolStrip_Menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip_Menu.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip_Menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Btn_PageDeb, Me.Btn_PagePrec, Me.Btn_PageSuiv, Me.Btn_PageFin, Me.ToolStripSeparator2, Me.Btn_Dezoomer, Me.Btn_Zoomer, Me.ToolStripSeparator1, Me.Btn_Navigation, Me.ToolStripSeparator6, Me.Btn_Imprimer, Me.Btn_ToPDF, Me.ToolStripSeparator3, Me.Btn_Langue, Me.ToolStripSeparator4, Me.Btn_Sommaire, Me.ToolStripSeparator5, Me.Btn_Options, Me.Btn_Logo, Me.Btn_Correct, Me.Btn_Error})
        Me.ToolStrip_Menu.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_Menu.Name = "ToolStrip_Menu"
        Me.ToolStrip_Menu.Padding = New System.Windows.Forms.Padding(3, 0, 1, 0)
        Me.ToolStrip_Menu.Size = New System.Drawing.Size(734, 31)
        Me.ToolStrip_Menu.TabIndex = 2
        Me.ToolStrip_Menu.Text = "ToolStrip_Menu"
        '
        'Btn_PageDeb
        '
        Me.Btn_PageDeb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_PageDeb.Image = CType(resources.GetObject("Btn_PageDeb.Image"), System.Drawing.Image)
        Me.Btn_PageDeb.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_PageDeb.Name = "Btn_PageDeb"
        Me.Btn_PageDeb.Size = New System.Drawing.Size(28, 28)
        Me.Btn_PageDeb.Text = "ToolStripButton1"
        '
        'Btn_PagePrec
        '
        Me.Btn_PagePrec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_PagePrec.Image = CType(resources.GetObject("Btn_PagePrec.Image"), System.Drawing.Image)
        Me.Btn_PagePrec.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_PagePrec.Name = "Btn_PagePrec"
        Me.Btn_PagePrec.Size = New System.Drawing.Size(28, 28)
        Me.Btn_PagePrec.Text = "ToolStripButton1"
        '
        'Btn_PageSuiv
        '
        Me.Btn_PageSuiv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_PageSuiv.Image = CType(resources.GetObject("Btn_PageSuiv.Image"), System.Drawing.Image)
        Me.Btn_PageSuiv.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_PageSuiv.Name = "Btn_PageSuiv"
        Me.Btn_PageSuiv.Size = New System.Drawing.Size(28, 28)
        Me.Btn_PageSuiv.Text = "ToolStripButton1"
        '
        'Btn_PageFin
        '
        Me.Btn_PageFin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_PageFin.Image = CType(resources.GetObject("Btn_PageFin.Image"), System.Drawing.Image)
        Me.Btn_PageFin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_PageFin.Name = "Btn_PageFin"
        Me.Btn_PageFin.Size = New System.Drawing.Size(28, 28)
        Me.Btn_PageFin.Text = "ToolStripButton1"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'Btn_Dezoomer
        '
        Me.Btn_Dezoomer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Dezoomer.Image = CType(resources.GetObject("Btn_Dezoomer.Image"), System.Drawing.Image)
        Me.Btn_Dezoomer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Dezoomer.Name = "Btn_Dezoomer"
        Me.Btn_Dezoomer.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Dezoomer.Text = "ToolStripButton2"
        '
        'Btn_Zoomer
        '
        Me.Btn_Zoomer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Zoomer.Image = CType(resources.GetObject("Btn_Zoomer.Image"), System.Drawing.Image)
        Me.Btn_Zoomer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Zoomer.Name = "Btn_Zoomer"
        Me.Btn_Zoomer.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Zoomer.Text = "ToolStripButton1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'Btn_Navigation
        '
        Me.Btn_Navigation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Navigation.Image = CType(resources.GetObject("Btn_Navigation.Image"), System.Drawing.Image)
        Me.Btn_Navigation.ImageTransparentColor = System.Drawing.Color.White
        Me.Btn_Navigation.Name = "Btn_Navigation"
        Me.Btn_Navigation.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Navigation.Text = "ToolStripButton1"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 31)
        '
        'Btn_Imprimer
        '
        Me.Btn_Imprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Imprimer.Image = CType(resources.GetObject("Btn_Imprimer.Image"), System.Drawing.Image)
        Me.Btn_Imprimer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Imprimer.Name = "Btn_Imprimer"
        Me.Btn_Imprimer.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Imprimer.Text = "ToolStripButton1"
        '
        'Btn_ToPDF
        '
        Me.Btn_ToPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_ToPDF.Image = CType(resources.GetObject("Btn_ToPDF.Image"), System.Drawing.Image)
        Me.Btn_ToPDF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_ToPDF.Name = "Btn_ToPDF"
        Me.Btn_ToPDF.Size = New System.Drawing.Size(28, 28)
        Me.Btn_ToPDF.Text = "Btn_ToPDF"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 31)
        '
        'Btn_Langue
        '
        Me.Btn_Langue.AutoToolTip = False
        Me.Btn_Langue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Btn_Langue.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Langue.Name = "Btn_Langue"
        Me.Btn_Langue.Size = New System.Drawing.Size(82, 28)
        Me.Btn_Langue.Text = "Btn_Langue"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 31)
        '
        'Btn_Sommaire
        '
        Me.Btn_Sommaire.AutoToolTip = False
        Me.Btn_Sommaire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Btn_Sommaire.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Sommaire.Name = "Btn_Sommaire"
        Me.Btn_Sommaire.Size = New System.Drawing.Size(97, 28)
        Me.Btn_Sommaire.Text = "Btn_Sommaire"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 31)
        '
        'Btn_Options
        '
        Me.Btn_Options.AutoToolTip = False
        Me.Btn_Options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Btn_Options.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Btn_UltraSynthese, Me.Btn_Synthese, Me.Btn_Complet, Me.Btn_Detail, Me.Btn_DessinPoutre})
        Me.Btn_Options.Image = CType(resources.GetObject("Btn_Options.Image"), System.Drawing.Image)
        Me.Btn_Options.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Options.Name = "Btn_Options"
        Me.Btn_Options.Size = New System.Drawing.Size(85, 28)
        Me.Btn_Options.Text = "Btn_Options"
        '
        'Btn_UltraSynthese
        '
        Me.Btn_UltraSynthese.Name = "Btn_UltraSynthese"
        Me.Btn_UltraSynthese.Size = New System.Drawing.Size(169, 22)
        Me.Btn_UltraSynthese.Text = "Btn_UltraSynthese"
        Me.Btn_UltraSynthese.Visible = False
        '
        'Btn_Synthese
        '
        Me.Btn_Synthese.Name = "Btn_Synthese"
        Me.Btn_Synthese.Size = New System.Drawing.Size(169, 22)
        Me.Btn_Synthese.Text = "Btn_Synthese"
        '
        'Btn_Complet
        '
        Me.Btn_Complet.Name = "Btn_Complet"
        Me.Btn_Complet.Size = New System.Drawing.Size(169, 22)
        Me.Btn_Complet.Text = "Btn_Complet"
        '
        'Btn_Detail
        '
        Me.Btn_Detail.Name = "Btn_Detail"
        Me.Btn_Detail.Size = New System.Drawing.Size(169, 22)
        Me.Btn_Detail.Text = "Btn_Detail"
        Me.Btn_Detail.Visible = False
        '
        'Btn_DessinPoutre
        '
        Me.Btn_DessinPoutre.Name = "Btn_DessinPoutre"
        Me.Btn_DessinPoutre.Size = New System.Drawing.Size(169, 22)
        Me.Btn_DessinPoutre.Text = "Btn_DessinPoutre"
        Me.Btn_DessinPoutre.Visible = False
        '
        'Btn_Logo
        '
        Me.Btn_Logo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Logo.Image = CType(resources.GetObject("Btn_Logo.Image"), System.Drawing.Image)
        Me.Btn_Logo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Logo.Name = "Btn_Logo"
        Me.Btn_Logo.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Logo.Text = "ToolStripButton1"
        '
        'Btn_Correct
        '
        Me.Btn_Correct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Correct.Image = CType(resources.GetObject("Btn_Correct.Image"), System.Drawing.Image)
        Me.Btn_Correct.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Correct.Name = "Btn_Correct"
        Me.Btn_Correct.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Correct.Text = "ToolStripButton1"
        '
        'Btn_Error
        '
        Me.Btn_Error.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Btn_Error.Image = CType(resources.GetObject("Btn_Error.Image"), System.Drawing.Image)
        Me.Btn_Error.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_Error.Name = "Btn_Error"
        Me.Btn_Error.Size = New System.Drawing.Size(28, 28)
        Me.Btn_Error.Text = "ToolStripButton2"
        '
        'StatusStrip_InfoZoom
        '
        Me.StatusStrip_InfoZoom.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip_InfoZoom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusPage, Me.StatusBeam, Me.StatusLangue, Me.StatusDetail})
        Me.StatusStrip_InfoZoom.Location = New System.Drawing.Point(0, 737)
        Me.StatusStrip_InfoZoom.Name = "StatusStrip_InfoZoom"
        Me.StatusStrip_InfoZoom.Size = New System.Drawing.Size(734, 24)
        Me.StatusStrip_InfoZoom.SizingGrip = False
        Me.StatusStrip_InfoZoom.TabIndex = 8
        '
        'StatusPage
        '
        Me.StatusPage.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusPage.Name = "StatusPage"
        Me.StatusPage.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.StatusPage.Size = New System.Drawing.Size(73, 19)
        Me.StatusPage.Text = "StatusPage"
        '
        'StatusBeam
        '
        Me.StatusBeam.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusBeam.Name = "StatusBeam"
        Me.StatusBeam.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.StatusBeam.Size = New System.Drawing.Size(77, 19)
        Me.StatusBeam.Text = "StatusBeam"
        '
        'StatusLangue
        '
        Me.StatusLangue.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusLangue.Name = "StatusLangue"
        Me.StatusLangue.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.StatusLangue.Size = New System.Drawing.Size(86, 19)
        Me.StatusLangue.Text = "StatusLangue"
        '
        'StatusDetail
        '
        Me.StatusDetail.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.StatusDetail.Name = "StatusDetail"
        Me.StatusDetail.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.StatusDetail.Size = New System.Drawing.Size(77, 19)
        Me.StatusDetail.Text = "StatusDetail"
        Me.StatusDetail.Visible = False
        '
        'Panel_NDC
        '
        Me.Panel_NDC.BackColor = System.Drawing.SystemColors.Control
        Me.Panel_NDC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_NDC.Controls.Add(Me.SplitContainer_NDC)
        Me.Panel_NDC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_NDC.Location = New System.Drawing.Point(0, 31)
        Me.Panel_NDC.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_NDC.Name = "Panel_NDC"
        Me.Panel_NDC.Size = New System.Drawing.Size(734, 706)
        Me.Panel_NDC.TabIndex = 9
        '
        'SplitContainer_NDC
        '
        Me.SplitContainer_NDC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer_NDC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer_NDC.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer_NDC.Name = "SplitContainer_NDC"
        '
        'SplitContainer_NDC.Panel1
        '
        Me.SplitContainer_NDC.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer_NDC.Panel1.Controls.Add(Me.Panel_Nav)
        Me.SplitContainer_NDC.Panel1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.SplitContainer_NDC.Panel1MinSize = 50
        '
        'SplitContainer_NDC.Panel2
        '
        Me.SplitContainer_NDC.Panel2.Controls.Add(Me.TableLayoutPanel_NDC)
        Me.SplitContainer_NDC.Panel2.Controls.Add(Me.VScrollBar_NDC)
        Me.SplitContainer_NDC.Panel2MinSize = 0
        Me.SplitContainer_NDC.Size = New System.Drawing.Size(732, 704)
        Me.SplitContainer_NDC.SplitterDistance = 200
        Me.SplitContainer_NDC.SplitterIncrement = 5
        Me.SplitContainer_NDC.SplitterWidth = 3
        Me.SplitContainer_NDC.TabIndex = 6
        Me.SplitContainer_NDC.TabStop = False
        '
        'Panel_Nav
        '
        Me.Panel_Nav.BackColor = System.Drawing.Color.Ivory
        Me.Panel_Nav.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Nav.Controls.Add(Me.TableLayoutPanel_Navigation)
        Me.Panel_Nav.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Nav.Location = New System.Drawing.Point(5, 5)
        Me.Panel_Nav.Name = "Panel_Nav"
        Me.Panel_Nav.Size = New System.Drawing.Size(190, 694)
        Me.Panel_Nav.TabIndex = 5
        '
        'TableLayoutPanel_Navigation
        '
        Me.TableLayoutPanel_Navigation.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Navigation.ColumnCount = 2
        Me.TableLayoutPanel_Navigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Navigation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel_Navigation.Controls.Add(Me.TreeView_NDC, 0, 1)
        Me.TableLayoutPanel_Navigation.Controls.Add(Me.Button_FermerNav, 1, 0)
        Me.TableLayoutPanel_Navigation.Controls.Add(Me.Label_Navigation, 0, 0)
        Me.TableLayoutPanel_Navigation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Navigation.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Navigation.Name = "TableLayoutPanel_Navigation"
        Me.TableLayoutPanel_Navigation.RowCount = 2
        Me.TableLayoutPanel_Navigation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel_Navigation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Navigation.Size = New System.Drawing.Size(188, 692)
        Me.TableLayoutPanel_Navigation.TabIndex = 4
        '
        'TreeView_NDC
        '
        Me.TreeView_NDC.BackColor = System.Drawing.Color.Ivory
        Me.TreeView_NDC.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TableLayoutPanel_Navigation.SetColumnSpan(Me.TreeView_NDC, 2)
        Me.TreeView_NDC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView_NDC.Location = New System.Drawing.Point(0, 26)
        Me.TreeView_NDC.Margin = New System.Windows.Forms.Padding(0)
        Me.TreeView_NDC.Name = "TreeView_NDC"
        Me.TreeView_NDC.Size = New System.Drawing.Size(188, 666)
        Me.TreeView_NDC.TabIndex = 2
        Me.TreeView_NDC.TabStop = False
        '
        'Button_FermerNav
        '
        Me.Button_FermerNav.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_FermerNav.Image = CType(resources.GetObject("Button_FermerNav.Image"), System.Drawing.Image)
        Me.Button_FermerNav.Location = New System.Drawing.Point(164, 2)
        Me.Button_FermerNav.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button_FermerNav.Name = "Button_FermerNav"
        Me.Button_FermerNav.Size = New System.Drawing.Size(22, 22)
        Me.Button_FermerNav.TabIndex = 1
        Me.Button_FermerNav.UseVisualStyleBackColor = True
        '
        'Label_Navigation
        '
        Me.Label_Navigation.AutoSize = True
        Me.Label_Navigation.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label_Navigation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Navigation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Navigation.Location = New System.Drawing.Point(3, 3)
        Me.Label_Navigation.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label_Navigation.Name = "Label_Navigation"
        Me.Label_Navigation.Size = New System.Drawing.Size(158, 20)
        Me.Label_Navigation.TabIndex = 5
        Me.Label_Navigation.Text = "Label_Navigation"
        Me.Label_Navigation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_NDC
        '
        Me.TableLayoutPanel_NDC.ColumnCount = 3
        Me.TableLayoutPanel_NDC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_NDC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_NDC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_NDC.Controls.Add(Me.Button_OuvrirNav, 0, 0)
        Me.TableLayoutPanel_NDC.Controls.Add(Me.img_Note, 1, 0)
        Me.TableLayoutPanel_NDC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_NDC.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_NDC.Name = "TableLayoutPanel_NDC"
        Me.TableLayoutPanel_NDC.RowCount = 2
        Me.TableLayoutPanel_NDC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel_NDC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_NDC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_NDC.Size = New System.Drawing.Size(515, 704)
        Me.TableLayoutPanel_NDC.TabIndex = 5
        '
        'Button_OuvrirNav
        '
        Me.Button_OuvrirNav.BackColor = System.Drawing.Color.White
        Me.Button_OuvrirNav.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button_OuvrirNav.Image = CType(resources.GetObject("Button_OuvrirNav.Image"), System.Drawing.Image)
        Me.Button_OuvrirNav.Location = New System.Drawing.Point(2, 2)
        Me.Button_OuvrirNav.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button_OuvrirNav.Name = "Button_OuvrirNav"
        Me.Button_OuvrirNav.Size = New System.Drawing.Size(22, 19)
        Me.Button_OuvrirNav.TabIndex = 1
        Me.Button_OuvrirNav.UseVisualStyleBackColor = False
        '
        'img_Note
        '
        Me.img_Note.BackColor = System.Drawing.Color.White
        Me.img_Note.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Note.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_Note.Location = New System.Drawing.Point(103, 0)
        Me.img_Note.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Note.Name = "img_Note"
        Me.TableLayoutPanel_NDC.SetRowSpan(Me.img_Note, 2)
        Me.img_Note.Size = New System.Drawing.Size(309, 704)
        Me.img_Note.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.img_Note.TabIndex = 1
        Me.img_Note.TabStop = False
        '
        'VScrollBar_NDC
        '
        Me.VScrollBar_NDC.Dock = System.Windows.Forms.DockStyle.Right
        Me.VScrollBar_NDC.Location = New System.Drawing.Point(515, 0)
        Me.VScrollBar_NDC.Name = "VScrollBar_NDC"
        Me.VScrollBar_NDC.Size = New System.Drawing.Size(14, 704)
        Me.VScrollBar_NDC.TabIndex = 2
        Me.VScrollBar_NDC.TabStop = True
        '
        'TrackBar_Zoom
        '
        Me.TrackBar_Zoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar_Zoom.BackColor = System.Drawing.SystemColors.Control
        Me.TrackBar_Zoom.LargeChange = 20
        Me.TrackBar_Zoom.Location = New System.Drawing.Point(652, 739)
        Me.TrackBar_Zoom.Margin = New System.Windows.Forms.Padding(0)
        Me.TrackBar_Zoom.Maximum = 100
        Me.TrackBar_Zoom.MaximumSize = New System.Drawing.Size(80, 20)
        Me.TrackBar_Zoom.MinimumSize = New System.Drawing.Size(80, 20)
        Me.TrackBar_Zoom.Name = "TrackBar_Zoom"
        Me.TrackBar_Zoom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TrackBar_Zoom.Size = New System.Drawing.Size(80, 45)
        Me.TrackBar_Zoom.SmallChange = 10
        Me.TrackBar_Zoom.TabIndex = 11
        Me.TrackBar_Zoom.TickFrequency = 20
        Me.TrackBar_Zoom.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.TrackBar_Zoom.Value = 50
        '
        'lbl_Zoom
        '
        Me.lbl_Zoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Zoom.AutoEllipsis = True
        Me.lbl_Zoom.Location = New System.Drawing.Point(595, 743)
        Me.lbl_Zoom.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Zoom.Name = "lbl_Zoom"
        Me.lbl_Zoom.Size = New System.Drawing.Size(57, 13)
        Me.lbl_Zoom.TabIndex = 10
        Me.lbl_Zoom.Text = "lbl_Zoom"
        Me.lbl_Zoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PrintDocument
        '
        '
        'PrintDialog
        '
        Me.PrintDialog.UseEXDialog = True
        '
        'Frm_NoteCalcul
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 761)
        Me.Controls.Add(Me.TrackBar_Zoom)
        Me.Controls.Add(Me.lbl_Zoom)
        Me.Controls.Add(Me.Panel_NDC)
        Me.Controls.Add(Me.StatusStrip_InfoZoom)
        Me.Controls.Add(Me.ToolStrip_Menu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 548)
        Me.Name = "Frm_NoteCalcul"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_NoteCalcul"
        Me.ToolStrip_Menu.ResumeLayout(False)
        Me.ToolStrip_Menu.PerformLayout()
        Me.StatusStrip_InfoZoom.ResumeLayout(False)
        Me.StatusStrip_InfoZoom.PerformLayout()
        Me.Panel_NDC.ResumeLayout(False)
        Me.SplitContainer_NDC.Panel1.ResumeLayout(False)
        Me.SplitContainer_NDC.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer_NDC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer_NDC.ResumeLayout(False)
        Me.Panel_Nav.ResumeLayout(False)
        Me.TableLayoutPanel_Navigation.ResumeLayout(False)
        Me.TableLayoutPanel_Navigation.PerformLayout()
        Me.TableLayoutPanel_NDC.ResumeLayout(False)
        CType(Me.img_Note, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar_Zoom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip_Menu As ToolStrip
    Friend WithEvents Btn_PageDeb As ToolStripButton
    Friend WithEvents Btn_PagePrec As ToolStripButton
    Friend WithEvents Btn_PageSuiv As ToolStripButton
    Friend WithEvents Btn_PageFin As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents Btn_Dezoomer As ToolStripButton
    Friend WithEvents Btn_Zoomer As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents Btn_Navigation As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents Btn_Imprimer As ToolStripButton
    Friend WithEvents Btn_ToPDF As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents Btn_Langue As ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents Btn_Options As ToolStripDropDownButton
    Friend WithEvents Btn_UltraSynthese As ToolStripMenuItem
    Friend WithEvents Btn_Synthese As ToolStripMenuItem
    Friend WithEvents Btn_Complet As ToolStripMenuItem
    Friend WithEvents Btn_Detail As ToolStripMenuItem
    Friend WithEvents Btn_DessinPoutre As ToolStripMenuItem
    Friend WithEvents StatusStrip_InfoZoom As StatusStrip
    Friend WithEvents StatusPage As ToolStripStatusLabel
    Friend WithEvents StatusLangue As ToolStripStatusLabel
    Friend WithEvents StatusDetail As ToolStripStatusLabel
    Friend WithEvents Panel_NDC As Panel
    Friend WithEvents SplitContainer_NDC As SplitContainer
    Friend WithEvents Panel_Nav As Panel
    Friend WithEvents TableLayoutPanel_Navigation As TableLayoutPanel
    Friend WithEvents TreeView_NDC As TreeView
    Friend WithEvents Button_FermerNav As Button
    Friend WithEvents Label_Navigation As Label
    Friend WithEvents TableLayoutPanel_NDC As TableLayoutPanel
    Friend WithEvents Button_OuvrirNav As Button
    Friend WithEvents img_Note As PictureBox
    Friend WithEvents VScrollBar_NDC As VScrollBar
    Friend WithEvents TrackBar_Zoom As TrackBar
    Friend WithEvents lbl_Zoom As Label
    Friend WithEvents PrintDocument As Printing.PrintDocument
    Friend WithEvents PrintDialog As PrintDialog
    Friend WithEvents SaveFileDialog_PDF As SaveFileDialog
    Friend WithEvents StatusBeam As ToolStripStatusLabel
    Friend WithEvents Btn_Logo As ToolStripButton
    Friend WithEvents Btn_Correct As ToolStripButton
    Friend WithEvents Btn_Error As ToolStripButton
    Friend WithEvents Btn_Sommaire As ToolStripDropDownButton
End Class
