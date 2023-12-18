<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBac
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
        Me.components = New System.ComponentModel.Container()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_MaitienB = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Floor = New System.Windows.Forms.Label()
        Me.pan_DefPlancher = New System.Windows.Forms.Panel()
        Me.lbl_Portee = New System.Windows.Forms.Label()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.txt_LargeurP = New System.Windows.Forms.TextBox()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_LongueurP = New System.Windows.Forms.TextBox()
        Me.lbl_DimensionsGlobales = New System.Windows.Forms.Label()
        Me.txt_NbSheetsTransverse = New System.Windows.Forms.TextBox()
        Me.lbl_NbSheetsTransverse = New System.Windows.Forms.Label()
        Me.img_Portees = New System.Windows.Forms.PictureBox()
        Me.lbl_Largeur = New System.Windows.Forms.Label()
        Me.lbl_Panneau = New System.Windows.Forms.Label()
        Me.pan_Panneau = New System.Windows.Forms.Panel()
        Me.lbl_NbSpans = New System.Windows.Forms.Label()
        Me.cmb_NbSpan = New System.Windows.Forms.ComboBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_SheetWidth = New System.Windows.Forms.Label()
        Me.lbl_SheetLength = New System.Windows.Forms.Label()
        Me.etq_UnitL4 = New System.Windows.Forms.Label()
        Me.txt_SheetWidth = New System.Windows.Forms.TextBox()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_SheetLength = New System.Windows.Forms.TextBox()
        Me.lbl_IndSheetDimensions = New System.Windows.Forms.Label()
        Me.lbl_Transition = New System.Windows.Forms.Label()
        Me.cmb_Transition = New System.Windows.Forms.ComboBox()
        Me.TLpan_Centre = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_FixationSolive = New System.Windows.Forms.Label()
        Me.lbl_Couturage = New System.Windows.Forms.Label()
        Me.pan_FixationSolive = New System.Windows.Forms.Panel()
        Me.pan_Couturage = New System.Windows.Forms.Panel()
        Me.lbl_Fixation = New System.Windows.Forms.Label()
        Me.cmb_FixationPoutre = New System.Windows.Forms.ComboBox()
        Me.lbl_TypeFixation = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_MaitienB.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_DefPlancher.SuspendLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Panneau.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TLpan_Centre.SuspendLayout()
        Me.pan_FixationSolive.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(976, 565)
        Me.pan_General.TabIndex = 3
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.pan_Main, 0, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(976, 565)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 528)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(970, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(498, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(358, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_MaitienB)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(970, 519)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_MaitienB
        '
        Me.TLPan_MaitienB.ColumnCount = 3
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 251.0!))
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_MaitienB.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_MaitienB.Controls.Add(Me.img_Portees, 2, 0)
        Me.TLPan_MaitienB.Controls.Add(Me.TLpan_Centre, 1, 0)
        Me.TLPan_MaitienB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_MaitienB.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_MaitienB.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_MaitienB.Name = "TLPan_MaitienB"
        Me.TLPan_MaitienB.RowCount = 1
        Me.TLPan_MaitienB.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_MaitienB.Size = New System.Drawing.Size(970, 519)
        Me.TLPan_MaitienB.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 519)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Panneau, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Floor, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_DefPlancher, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Panneau, 0, 3)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 5
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 427)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Floor
        '
        Me.lbl_Floor.AutoSize = True
        Me.lbl_Floor.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Floor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Floor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Floor.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Floor.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Floor.Name = "lbl_Floor"
        Me.lbl_Floor.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Floor.TabIndex = 0
        Me.lbl_Floor.Text = "lbl_Floor"
        Me.lbl_Floor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DefPlancher
        '
        Me.pan_DefPlancher.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DefPlancher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefPlancher.Controls.Add(Me.cmb_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.txt_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.Panel1)
        Me.pan_DefPlancher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefPlancher.Location = New System.Drawing.Point(0, 30)
        Me.pan_DefPlancher.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_DefPlancher.Name = "pan_DefPlancher"
        Me.pan_DefPlancher.Size = New System.Drawing.Size(250, 179)
        Me.pan_DefPlancher.TabIndex = 1
        '
        'lbl_Portee
        '
        Me.lbl_Portee.Location = New System.Drawing.Point(24, 26)
        Me.lbl_Portee.Name = "lbl_Portee"
        Me.lbl_Portee.Size = New System.Drawing.Size(118, 13)
        Me.lbl_Portee.TabIndex = 78
        Me.lbl_Portee.Text = "lbl_Portee"
        Me.lbl_Portee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(212, 52)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL2.TabIndex = 77
        Me.etq_UnitL2.Text = "kN"
        '
        'txt_LargeurP
        '
        Me.txt_LargeurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LargeurP.Location = New System.Drawing.Point(148, 48)
        Me.txt_LargeurP.Name = "txt_LargeurP"
        Me.txt_LargeurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LargeurP.TabIndex = 76
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(212, 26)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL1.TabIndex = 75
        Me.etq_UnitL1.Text = "kN"
        '
        'txt_LongueurP
        '
        Me.txt_LongueurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LongueurP.Location = New System.Drawing.Point(148, 22)
        Me.txt_LongueurP.Name = "txt_LongueurP"
        Me.txt_LongueurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LongueurP.TabIndex = 73
        '
        'lbl_DimensionsGlobales
        '
        Me.lbl_DimensionsGlobales.AutoSize = True
        Me.lbl_DimensionsGlobales.Location = New System.Drawing.Point(7, 2)
        Me.lbl_DimensionsGlobales.Name = "lbl_DimensionsGlobales"
        Me.lbl_DimensionsGlobales.Size = New System.Drawing.Size(118, 13)
        Me.lbl_DimensionsGlobales.TabIndex = 2
        Me.lbl_DimensionsGlobales.Text = "lbl_DimensionsGlobales"
        '
        'txt_NbSheetsTransverse
        '
        Me.txt_NbSheetsTransverse.Location = New System.Drawing.Point(149, 29)
        Me.txt_NbSheetsTransverse.Name = "txt_NbSheetsTransverse"
        Me.txt_NbSheetsTransverse.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbSheetsTransverse.TabIndex = 1
        '
        'lbl_NbSheetsTransverse
        '
        Me.lbl_NbSheetsTransverse.AutoSize = True
        Me.lbl_NbSheetsTransverse.Location = New System.Drawing.Point(8, 6)
        Me.lbl_NbSheetsTransverse.Name = "lbl_NbSheetsTransverse"
        Me.lbl_NbSheetsTransverse.Size = New System.Drawing.Size(123, 13)
        Me.lbl_NbSheetsTransverse.TabIndex = 0
        Me.lbl_NbSheetsTransverse.Text = "lbl_NbSheetsTransverse"
        '
        'img_Portees
        '
        Me.img_Portees.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Portees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Portees.Location = New System.Drawing.Point(502, 0)
        Me.img_Portees.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Portees.Name = "img_Portees"
        Me.img_Portees.Size = New System.Drawing.Size(100, 50)
        Me.img_Portees.TabIndex = 1
        Me.img_Portees.TabStop = False
        '
        'lbl_Largeur
        '
        Me.lbl_Largeur.Location = New System.Drawing.Point(24, 52)
        Me.lbl_Largeur.Name = "lbl_Largeur"
        Me.lbl_Largeur.Size = New System.Drawing.Size(118, 13)
        Me.lbl_Largeur.TabIndex = 79
        Me.lbl_Largeur.Text = "lbl_Largeur"
        Me.lbl_Largeur.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Panneau
        '
        Me.lbl_Panneau.AutoSize = True
        Me.lbl_Panneau.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Panneau.Location = New System.Drawing.Point(0, 210)
        Me.lbl_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Panneau.Name = "lbl_Panneau"
        Me.lbl_Panneau.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Panneau.TabIndex = 2
        Me.lbl_Panneau.Text = "lbl_Panneau"
        Me.lbl_Panneau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Panneau
        '
        Me.pan_Panneau.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Panneau.Controls.Add(Me.cmb_NbSpan)
        Me.pan_Panneau.Controls.Add(Me.lbl_NbSpans)
        Me.pan_Panneau.Controls.Add(Me.Panel2)
        Me.pan_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Panneau.Location = New System.Drawing.Point(0, 240)
        Me.pan_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Panneau.Name = "pan_Panneau"
        Me.pan_Panneau.Size = New System.Drawing.Size(250, 140)
        Me.pan_Panneau.TabIndex = 3
        '
        'lbl_NbSpans
        '
        Me.lbl_NbSpans.AutoSize = True
        Me.lbl_NbSpans.Location = New System.Drawing.Point(8, 10)
        Me.lbl_NbSpans.Name = "lbl_NbSpans"
        Me.lbl_NbSpans.Size = New System.Drawing.Size(67, 13)
        Me.lbl_NbSpans.TabIndex = 3
        Me.lbl_NbSpans.Text = "lbl_NbSpans"
        '
        'cmb_NbSpan
        '
        Me.cmb_NbSpan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NbSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbSpan.FormattingEnabled = True
        Me.cmb_NbSpan.Location = New System.Drawing.Point(149, 26)
        Me.cmb_NbSpan.Name = "cmb_NbSpan"
        Me.cmb_NbSpan.Size = New System.Drawing.Size(75, 21)
        Me.cmb_NbSpan.TabIndex = 5
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_Largeur)
        Me.Panel1.Controls.Add(Me.lbl_DimensionsGlobales)
        Me.Panel1.Controls.Add(Me.lbl_Portee)
        Me.Panel1.Controls.Add(Me.txt_LongueurP)
        Me.Panel1.Controls.Add(Me.etq_UnitL2)
        Me.Panel1.Controls.Add(Me.etq_UnitL1)
        Me.Panel1.Controls.Add(Me.txt_LargeurP)
        Me.Panel1.Location = New System.Drawing.Point(1, 53)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(244, 71)
        Me.Panel1.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_SheetWidth)
        Me.Panel2.Controls.Add(Me.lbl_SheetLength)
        Me.Panel2.Controls.Add(Me.etq_UnitL4)
        Me.Panel2.Controls.Add(Me.txt_SheetWidth)
        Me.Panel2.Controls.Add(Me.etq_UnitL3)
        Me.Panel2.Controls.Add(Me.txt_SheetLength)
        Me.Panel2.Controls.Add(Me.lbl_IndSheetDimensions)
        Me.Panel2.Location = New System.Drawing.Point(1, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(244, 71)
        Me.Panel2.TabIndex = 5
        '
        'lbl_SheetWidth
        '
        Me.lbl_SheetWidth.Location = New System.Drawing.Point(24, 52)
        Me.lbl_SheetWidth.Name = "lbl_SheetWidth"
        Me.lbl_SheetWidth.Size = New System.Drawing.Size(118, 13)
        Me.lbl_SheetWidth.TabIndex = 93
        Me.lbl_SheetWidth.Text = "lbl_SheetWidth"
        Me.lbl_SheetWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_SheetLength
        '
        Me.lbl_SheetLength.Location = New System.Drawing.Point(24, 26)
        Me.lbl_SheetLength.Name = "lbl_SheetLength"
        Me.lbl_SheetLength.Size = New System.Drawing.Size(118, 13)
        Me.lbl_SheetLength.TabIndex = 92
        Me.lbl_SheetLength.Text = "lbl_SheetLength"
        Me.lbl_SheetLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL4
        '
        Me.etq_UnitL4.AutoSize = True
        Me.etq_UnitL4.Location = New System.Drawing.Point(212, 52)
        Me.etq_UnitL4.Name = "etq_UnitL4"
        Me.etq_UnitL4.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL4.TabIndex = 91
        Me.etq_UnitL4.Text = "kN"
        '
        'txt_SheetWidth
        '
        Me.txt_SheetWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetWidth.Location = New System.Drawing.Point(148, 48)
        Me.txt_SheetWidth.Name = "txt_SheetWidth"
        Me.txt_SheetWidth.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetWidth.TabIndex = 90
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(212, 26)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL3.TabIndex = 89
        Me.etq_UnitL3.Text = "kN"
        '
        'txt_SheetLength
        '
        Me.txt_SheetLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetLength.Location = New System.Drawing.Point(148, 22)
        Me.txt_SheetLength.Name = "txt_SheetLength"
        Me.txt_SheetLength.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetLength.TabIndex = 88
        '
        'lbl_IndSheetDimensions
        '
        Me.lbl_IndSheetDimensions.AutoSize = True
        Me.lbl_IndSheetDimensions.Location = New System.Drawing.Point(7, 2)
        Me.lbl_IndSheetDimensions.Name = "lbl_IndSheetDimensions"
        Me.lbl_IndSheetDimensions.Size = New System.Drawing.Size(120, 13)
        Me.lbl_IndSheetDimensions.TabIndex = 87
        Me.lbl_IndSheetDimensions.Text = "lbl_IndSheetDimensions"
        '
        'lbl_Transition
        '
        Me.lbl_Transition.AutoSize = True
        Me.lbl_Transition.Location = New System.Drawing.Point(8, 131)
        Me.lbl_Transition.Name = "lbl_Transition"
        Me.lbl_Transition.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Transition.TabIndex = 5
        Me.lbl_Transition.Text = "lbl_Transition"
        '
        'cmb_Transition
        '
        Me.cmb_Transition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Transition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Transition.FormattingEnabled = True
        Me.cmb_Transition.Location = New System.Drawing.Point(28, 152)
        Me.cmb_Transition.Name = "cmb_Transition"
        Me.cmb_Transition.Size = New System.Drawing.Size(196, 21)
        Me.cmb_Transition.TabIndex = 6
        '
        'TLpan_Centre
        '
        Me.TLpan_Centre.ColumnCount = 1
        Me.TLpan_Centre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Centre.Controls.Add(Me.pan_Couturage, 0, 3)
        Me.TLpan_Centre.Controls.Add(Me.lbl_Couturage, 0, 2)
        Me.TLpan_Centre.Controls.Add(Me.lbl_FixationSolive, 0, 0)
        Me.TLpan_Centre.Controls.Add(Me.pan_FixationSolive, 0, 1)
        Me.TLpan_Centre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Centre.Location = New System.Drawing.Point(251, 0)
        Me.TLpan_Centre.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Centre.Name = "TLpan_Centre"
        Me.TLpan_Centre.RowCount = 5
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Centre.Size = New System.Drawing.Size(250, 519)
        Me.TLpan_Centre.TabIndex = 2
        '
        'lbl_FixationSolive
        '
        Me.lbl_FixationSolive.AutoSize = True
        Me.lbl_FixationSolive.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_FixationSolive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_FixationSolive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_FixationSolive.Location = New System.Drawing.Point(0, 0)
        Me.lbl_FixationSolive.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_FixationSolive.Name = "lbl_FixationSolive"
        Me.lbl_FixationSolive.Size = New System.Drawing.Size(250, 30)
        Me.lbl_FixationSolive.TabIndex = 1
        Me.lbl_FixationSolive.Text = "lbl_FixationSolive"
        Me.lbl_FixationSolive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Couturage
        '
        Me.lbl_Couturage.AutoSize = True
        Me.lbl_Couturage.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Couturage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Couturage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Couturage.Location = New System.Drawing.Point(0, 150)
        Me.lbl_Couturage.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Couturage.Name = "lbl_Couturage"
        Me.lbl_Couturage.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Couturage.TabIndex = 2
        Me.lbl_Couturage.Text = "lbl_Couturage"
        Me.lbl_Couturage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_FixationSolive
        '
        Me.pan_FixationSolive.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_FixationSolive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_FixationSolive.Controls.Add(Me.ComboBox1)
        Me.pan_FixationSolive.Controls.Add(Me.lbl_TypeFixation)
        Me.pan_FixationSolive.Controls.Add(Me.cmb_FixationPoutre)
        Me.pan_FixationSolive.Controls.Add(Me.lbl_Fixation)
        Me.pan_FixationSolive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_FixationSolive.Location = New System.Drawing.Point(0, 30)
        Me.pan_FixationSolive.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_FixationSolive.Name = "pan_FixationSolive"
        Me.pan_FixationSolive.Size = New System.Drawing.Size(250, 119)
        Me.pan_FixationSolive.TabIndex = 3
        '
        'pan_Couturage
        '
        Me.pan_Couturage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Couturage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Couturage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Couturage.Location = New System.Drawing.Point(0, 180)
        Me.pan_Couturage.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Couturage.Name = "pan_Couturage"
        Me.pan_Couturage.Size = New System.Drawing.Size(250, 150)
        Me.pan_Couturage.TabIndex = 4
        '
        'lbl_Fixation
        '
        Me.lbl_Fixation.AutoSize = True
        Me.lbl_Fixation.Location = New System.Drawing.Point(8, 9)
        Me.lbl_Fixation.Name = "lbl_Fixation"
        Me.lbl_Fixation.Size = New System.Drawing.Size(59, 13)
        Me.lbl_Fixation.TabIndex = 6
        Me.lbl_Fixation.Text = "lbl_Fixation"
        '
        'cmb_FixationPoutre
        '
        Me.cmb_FixationPoutre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_FixationPoutre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_FixationPoutre.FormattingEnabled = True
        Me.cmb_FixationPoutre.Location = New System.Drawing.Point(119, 6)
        Me.cmb_FixationPoutre.Name = "cmb_FixationPoutre"
        Me.cmb_FixationPoutre.Size = New System.Drawing.Size(116, 21)
        Me.cmb_FixationPoutre.TabIndex = 7
        '
        'lbl_TypeFixation
        '
        Me.lbl_TypeFixation.AutoSize = True
        Me.lbl_TypeFixation.Location = New System.Drawing.Point(8, 36)
        Me.lbl_TypeFixation.Name = "lbl_TypeFixation"
        Me.lbl_TypeFixation.Size = New System.Drawing.Size(83, 13)
        Me.lbl_TypeFixation.TabIndex = 8
        Me.lbl_TypeFixation.Text = "lbl_TypeFixation"
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(39, 55)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(196, 21)
        Me.ComboBox1.TabIndex = 9
        '
        'Frm_MaintienBac
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(976, 565)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_MaintienBac"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_MaintienBac"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_MaitienB.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_DefPlancher.ResumeLayout(False)
        Me.pan_DefPlancher.PerformLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Panneau.ResumeLayout(False)
        Me.pan_Panneau.PerformLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TLpan_Centre.ResumeLayout(False)
        Me.TLpan_Centre.PerformLayout()
        Me.pan_FixationSolive.ResumeLayout(False)
        Me.pan_FixationSolive.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_MaitienB As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Floor As Label
    Friend WithEvents pan_DefPlancher As Panel
    Friend WithEvents img_Portees As PictureBox
    Friend WithEvents lbl_DimensionsGlobales As Label
    Friend WithEvents txt_NbSheetsTransverse As TextBox
    Friend WithEvents lbl_NbSheetsTransverse As Label
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents txt_LargeurP As TextBox
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_LongueurP As TextBox
    Friend WithEvents lbl_Portee As Label
    Friend WithEvents lbl_Largeur As Label
    Friend WithEvents lbl_Panneau As Label
    Friend WithEvents pan_Panneau As Panel
    Friend WithEvents lbl_NbSpans As Label
    Friend WithEvents cmb_NbSpan As ComboBox
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents cmb_Transition As ComboBox
    Friend WithEvents lbl_Transition As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbl_SheetWidth As Label
    Friend WithEvents lbl_SheetLength As Label
    Friend WithEvents etq_UnitL4 As Label
    Friend WithEvents txt_SheetWidth As TextBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_SheetLength As TextBox
    Friend WithEvents lbl_IndSheetDimensions As Label
    Friend WithEvents TLpan_Centre As TableLayoutPanel
    Friend WithEvents pan_Couturage As Panel
    Friend WithEvents lbl_Couturage As Label
    Friend WithEvents lbl_FixationSolive As Label
    Friend WithEvents pan_FixationSolive As Panel
    Friend WithEvents cmb_FixationPoutre As ComboBox
    Friend WithEvents lbl_Fixation As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents lbl_TypeFixation As Label
End Class
