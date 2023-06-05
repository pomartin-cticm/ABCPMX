<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SectionAcierStandard
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_Saisie = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Section = New System.Windows.Forms.Label()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.pan_DefinitionAcier = New System.Windows.Forms.Panel()
        Me.etq_ReductionCurve = New System.Windows.Forms.Label()
        Me.etq_Qualite = New System.Windows.Forms.Label()
        Me.etq_Grade = New System.Windows.Forms.Label()
        Me.GridAciers = New System.Windows.Forms.DataGridView()
        Me.Col_Grade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Qualite = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_ReductionCurve = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pan_DefinitionSection = New System.Windows.Forms.Panel()
        Me.etq_ProfilS = New System.Windows.Forms.Label()
        Me.Grid_ProfilesSup = New System.Windows.Forms.DataGridView()
        Me.Col_ListeSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HISTARSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.etq_GammeS = New System.Windows.Forms.Label()
        Me.lst_GammeS = New System.Windows.Forms.ListBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_Saisie.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pan_DefinitionAcier.SuspendLayout()
        CType(Me.GridAciers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_DefinitionSection.SuspendLayout()
        CType(Me.Grid_ProfilesSup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1013, 518)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1013, 518)
        Me.TLpan_Main.TabIndex = 0
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Main.Controls.Add(Me.TLpan_Saisie)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1007, 472)
        Me.pan_Main.TabIndex = 1
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(376, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(516, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 481)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1007, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'TLpan_Saisie
        '
        Me.TLpan_Saisie.ColumnCount = 3
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Saisie.Controls.Add(Me.pan_Droite, 2, 0)
        Me.TLpan_Saisie.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLpan_Saisie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Saisie.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Saisie.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Saisie.Name = "TLpan_Saisie"
        Me.TLpan_Saisie.RowCount = 1
        Me.TLpan_Saisie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Saisie.Size = New System.Drawing.Size(1005, 470)
        Me.TLpan_Saisie.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLpan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(352, 470)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Section, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_DefinitionSection, 0, 1)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 2
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(352, 470)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'lbl_Section
        '
        Me.lbl_Section.AutoSize = True
        Me.lbl_Section.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Section.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Section.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Section.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Section.Name = "lbl_Section"
        Me.lbl_Section.Size = New System.Drawing.Size(352, 30)
        Me.lbl_Section.TabIndex = 1
        Me.lbl_Section.Text = "lbl_Section"
        Me.lbl_Section.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TableLayoutPanel1)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(652, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(353, 470)
        Me.pan_Droite.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_Acier, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pan_DefinitionAcier, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(353, 470)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lbl_Acier
        '
        Me.lbl_Acier.AutoSize = True
        Me.lbl_Acier.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Acier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Acier.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(353, 30)
        Me.lbl_Acier.TabIndex = 1
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DefinitionAcier
        '
        Me.pan_DefinitionAcier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefinitionAcier.Controls.Add(Me.etq_ReductionCurve)
        Me.pan_DefinitionAcier.Controls.Add(Me.etq_Qualite)
        Me.pan_DefinitionAcier.Controls.Add(Me.etq_Grade)
        Me.pan_DefinitionAcier.Controls.Add(Me.GridAciers)
        Me.pan_DefinitionAcier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefinitionAcier.Location = New System.Drawing.Point(0, 30)
        Me.pan_DefinitionAcier.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DefinitionAcier.Name = "pan_DefinitionAcier"
        Me.pan_DefinitionAcier.Size = New System.Drawing.Size(353, 440)
        Me.pan_DefinitionAcier.TabIndex = 2
        '
        'etq_ReductionCurve
        '
        Me.etq_ReductionCurve.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_ReductionCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.etq_ReductionCurve.Location = New System.Drawing.Point(169, 11)
        Me.etq_ReductionCurve.Name = "etq_ReductionCurve"
        Me.etq_ReductionCurve.Size = New System.Drawing.Size(175, 17)
        Me.etq_ReductionCurve.TabIndex = 24
        Me.etq_ReductionCurve.Text = "etq_ReductionCurve"
        Me.etq_ReductionCurve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'etq_Qualite
        '
        Me.etq_Qualite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.etq_Qualite.Location = New System.Drawing.Point(90, 11)
        Me.etq_Qualite.Name = "etq_Qualite"
        Me.etq_Qualite.Size = New System.Drawing.Size(80, 17)
        Me.etq_Qualite.TabIndex = 23
        Me.etq_Qualite.Text = "etq_Qualite"
        Me.etq_Qualite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'etq_Grade
        '
        Me.etq_Grade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.etq_Grade.Location = New System.Drawing.Point(11, 11)
        Me.etq_Grade.Name = "etq_Grade"
        Me.etq_Grade.Size = New System.Drawing.Size(80, 17)
        Me.etq_Grade.TabIndex = 22
        Me.etq_Grade.Text = "etq_Grade"
        Me.etq_Grade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GridAciers
        '
        Me.GridAciers.AllowUserToAddRows = False
        Me.GridAciers.AllowUserToDeleteRows = False
        Me.GridAciers.AllowUserToResizeColumns = False
        Me.GridAciers.AllowUserToResizeRows = False
        Me.GridAciers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridAciers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridAciers.ColumnHeadersVisible = False
        Me.GridAciers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Grade, Me.Col_Qualite, Me.Col_ReductionCurve})
        Me.GridAciers.Location = New System.Drawing.Point(10, 31)
        Me.GridAciers.MultiSelect = False
        Me.GridAciers.Name = "GridAciers"
        Me.GridAciers.RowHeadersVisible = False
        Me.GridAciers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GridAciers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridAciers.ShowCellToolTips = False
        Me.GridAciers.Size = New System.Drawing.Size(334, 390)
        Me.GridAciers.TabIndex = 21
        '
        'Col_Grade
        '
        Me.Col_Grade.HeaderText = "Col_Grade"
        Me.Col_Grade.Name = "Col_Grade"
        Me.Col_Grade.ReadOnly = True
        '
        'Col_Qualite
        '
        Me.Col_Qualite.HeaderText = "Col_Qualite"
        Me.Col_Qualite.Name = "Col_Qualite"
        Me.Col_Qualite.ReadOnly = True
        '
        'Col_ReductionCurve
        '
        Me.Col_ReductionCurve.HeaderText = "Col_ReductionCurve"
        Me.Col_ReductionCurve.Name = "Col_ReductionCurve"
        Me.Col_ReductionCurve.ReadOnly = True
        '
        'pan_DefinitionSection
        '
        Me.pan_DefinitionSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefinitionSection.Controls.Add(Me.etq_ProfilS)
        Me.pan_DefinitionSection.Controls.Add(Me.Grid_ProfilesSup)
        Me.pan_DefinitionSection.Controls.Add(Me.etq_GammeS)
        Me.pan_DefinitionSection.Controls.Add(Me.lst_GammeS)
        Me.pan_DefinitionSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefinitionSection.Location = New System.Drawing.Point(0, 30)
        Me.pan_DefinitionSection.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DefinitionSection.Name = "pan_DefinitionSection"
        Me.pan_DefinitionSection.Size = New System.Drawing.Size(352, 440)
        Me.pan_DefinitionSection.TabIndex = 2
        '
        'etq_ProfilS
        '
        Me.etq_ProfilS.AutoSize = True
        Me.etq_ProfilS.Location = New System.Drawing.Point(135, 91)
        Me.etq_ProfilS.Name = "etq_ProfilS"
        Me.etq_ProfilS.Size = New System.Drawing.Size(58, 13)
        Me.etq_ProfilS.TabIndex = 19
        Me.etq_ProfilS.Text = "etq_ProfilS"
        Me.etq_ProfilS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Grid_ProfilesSup
        '
        Me.Grid_ProfilesSup.AllowUserToAddRows = False
        Me.Grid_ProfilesSup.AllowUserToDeleteRows = False
        Me.Grid_ProfilesSup.AllowUserToResizeColumns = False
        Me.Grid_ProfilesSup.AllowUserToResizeRows = False
        Me.Grid_ProfilesSup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_ProfilesSup.ColumnHeadersVisible = False
        Me.Grid_ProfilesSup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_ListeSup, Me.Col_HISTARSup})
        Me.Grid_ProfilesSup.Location = New System.Drawing.Point(132, 110)
        Me.Grid_ProfilesSup.MultiSelect = False
        Me.Grid_ProfilesSup.Name = "Grid_ProfilesSup"
        Me.Grid_ProfilesSup.RowHeadersVisible = False
        Me.Grid_ProfilesSup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_ProfilesSup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_ProfilesSup.ShowCellToolTips = False
        Me.Grid_ProfilesSup.Size = New System.Drawing.Size(215, 160)
        Me.Grid_ProfilesSup.TabIndex = 18
        '
        'Col_ListeSup
        '
        Me.Col_ListeSup.HeaderText = "Col_ListeSup"
        Me.Col_ListeSup.Name = "Col_ListeSup"
        Me.Col_ListeSup.ReadOnly = True
        '
        'Col_HISTARSup
        '
        Me.Col_HISTARSup.HeaderText = "Col_HISTARSup"
        Me.Col_HISTARSup.Name = "Col_HISTARSup"
        Me.Col_HISTARSup.ReadOnly = True
        '
        'etq_GammeS
        '
        Me.etq_GammeS.AutoSize = True
        Me.etq_GammeS.Location = New System.Drawing.Point(8, 91)
        Me.etq_GammeS.Name = "etq_GammeS"
        Me.etq_GammeS.Size = New System.Drawing.Size(71, 13)
        Me.etq_GammeS.TabIndex = 16
        Me.etq_GammeS.Text = "etq_GammeS"
        Me.etq_GammeS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lst_GammeS
        '
        Me.lst_GammeS.FormattingEnabled = True
        Me.lst_GammeS.Location = New System.Drawing.Point(7, 110)
        Me.lst_GammeS.Name = "lst_GammeS"
        Me.lst_GammeS.Size = New System.Drawing.Size(119, 160)
        Me.lst_GammeS.TabIndex = 17
        '
        'Frm_SectionAcierStandard
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1013, 518)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_SectionAcierStandard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_SectionAcierStandard"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_Saisie.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.pan_Droite.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pan_DefinitionAcier.ResumeLayout(False)
        CType(Me.GridAciers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_DefinitionSection.ResumeLayout(False)
        Me.pan_DefinitionSection.PerformLayout()
        CType(Me.Grid_ProfilesSup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Saisie As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents pan_DefinitionAcier As Panel
    Friend WithEvents etq_ReductionCurve As Label
    Friend WithEvents etq_Qualite As Label
    Friend WithEvents etq_Grade As Label
    Friend WithEvents GridAciers As DataGridView
    Friend WithEvents Col_Grade As DataGridViewTextBoxColumn
    Friend WithEvents Col_Qualite As DataGridViewTextBoxColumn
    Friend WithEvents Col_ReductionCurve As DataGridViewTextBoxColumn
    Friend WithEvents lbl_Section As Label
    Friend WithEvents pan_DefinitionSection As Panel
    Friend WithEvents etq_ProfilS As Label
    Friend WithEvents Grid_ProfilesSup As DataGridView
    Friend WithEvents Col_ListeSup As DataGridViewTextBoxColumn
    Friend WithEvents Col_HISTARSup As DataGridViewTextBoxColumn
    Friend WithEvents etq_GammeS As Label
    Friend WithEvents lst_GammeS As ListBox
End Class
