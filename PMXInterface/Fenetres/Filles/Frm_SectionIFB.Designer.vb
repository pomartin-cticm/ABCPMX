<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SectionIFB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_SectionIFB))
        Me.GridAciers = New System.Windows.Forms.DataGridView()
        Me.Col_Grade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Qualite = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_ReductionCurve = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.etq_UnitDim4 = New System.Windows.Forms.Label()
        Me.etq_UnitDim3 = New System.Windows.Forms.Label()
        Me.txt_tp = New System.Windows.Forms.TextBox()
        Me.txt_bp = New System.Windows.Forms.TextBox()
        Me.img_tp = New System.Windows.Forms.PictureBox()
        Me.img_bp = New System.Windows.Forms.PictureBox()
        Me.lbl_Thickness = New System.Windows.Forms.Label()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.pan_DefinitionAcier = New System.Windows.Forms.Panel()
        Me.lbl_ReductionCurve = New System.Windows.Forms.Label()
        Me.lbl_Qualite = New System.Windows.Forms.Label()
        Me.lbl_Grade = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLpan_Saisie = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Plat = New System.Windows.Forms.Panel()
        Me.lbl_InfoFyWP = New System.Windows.Forms.Label()
        Me.cmb_NuancePlat = New System.Windows.Forms.ComboBox()
        Me.lbl_Width = New System.Windows.Forms.Label()
        Me.lbl_WPSteel = New System.Windows.Forms.Label()
        Me.lbl_Plat = New System.Windows.Forms.Label()
        Me.lbl_IFBSection = New System.Windows.Forms.Label()
        Me.Pan_DimPRS = New System.Windows.Forms.Panel()
        Me.chk_Hw = New System.Windows.Forms.CheckBox()
        Me.chk_Ht = New System.Windows.Forms.CheckBox()
        Me.lbl_Info = New System.Windows.Forms.Label()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.etq_UnitDim2 = New System.Windows.Forms.Label()
        Me.txt_ha = New System.Windows.Forms.TextBox()
        Me.txt_hw = New System.Windows.Forms.TextBox()
        Me.img_ha = New System.Windows.Forms.PictureBox()
        Me.img_hw = New System.Windows.Forms.PictureBox()
        Me.lbl_Height = New System.Windows.Forms.Label()
        Me.lbl_Web = New System.Windows.Forms.Label()
        Me.pan_Lamine = New System.Windows.Forms.Panel()
        Me.lbl_Profiles = New System.Windows.Forms.Label()
        Me.Grid_ProfilesSup = New System.Windows.Forms.DataGridView()
        Me.Col_ListeSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HISTARSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_Gamme = New System.Windows.Forms.Label()
        Me.lst_GammeS = New System.Windows.Forms.ListBox()
        Me.lbl_ParentProfile = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Acier = New System.Windows.Forms.Panel()
        Me.btn_FyFu = New System.Windows.Forms.Button()
        Me.cmb_ReductionCurveWP = New System.Windows.Forms.ComboBox()
        Me.cmb_GradeWP = New System.Windows.Forms.ComboBox()
        Me.img_ReductionCurve = New System.Windows.Forms.PictureBox()
        Me.img_Section = New System.Windows.Forms.PictureBox()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Provi = New System.Windows.Forms.Panel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.imgList_UY = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.GridAciers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_tp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_DefinitionAcier.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLpan_Saisie.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_Plat.SuspendLayout()
        Me.Pan_DimPRS.SuspendLayout()
        CType(Me.img_ha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Lamine.SuspendLayout()
        CType(Me.Grid_ProfilesSup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pan_Acier.SuspendLayout()
        CType(Me.img_ReductionCurve, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Section, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.GridAciers.Location = New System.Drawing.Point(13, 38)
        Me.GridAciers.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridAciers.MultiSelect = False
        Me.GridAciers.Name = "GridAciers"
        Me.GridAciers.RowHeadersVisible = False
        Me.GridAciers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GridAciers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridAciers.ShowCellToolTips = False
        Me.GridAciers.Size = New System.Drawing.Size(448, 524)
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
        'etq_UnitDim4
        '
        Me.etq_UnitDim4.AutoSize = True
        Me.etq_UnitDim4.Location = New System.Drawing.Point(385, 55)
        Me.etq_UnitDim4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim4.Name = "etq_UnitDim4"
        Me.etq_UnitDim4.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim4.TabIndex = 3
        Me.etq_UnitDim4.Text = "mm"
        '
        'etq_UnitDim3
        '
        Me.etq_UnitDim3.AutoSize = True
        Me.etq_UnitDim3.Location = New System.Drawing.Point(385, 20)
        Me.etq_UnitDim3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim3.Name = "etq_UnitDim3"
        Me.etq_UnitDim3.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim3.TabIndex = 3
        Me.etq_UnitDim3.Text = "mm"
        '
        'txt_tp
        '
        Me.txt_tp.Location = New System.Drawing.Point(300, 52)
        Me.txt_tp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_tp.Name = "txt_tp"
        Me.txt_tp.Size = New System.Drawing.Size(76, 22)
        Me.txt_tp.TabIndex = 2
        '
        'txt_bp
        '
        Me.txt_bp.Location = New System.Drawing.Point(300, 16)
        Me.txt_bp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_bp.Name = "txt_bp"
        Me.txt_bp.Size = New System.Drawing.Size(76, 22)
        Me.txt_bp.TabIndex = 2
        '
        'img_tp
        '
        Me.img_tp.Location = New System.Drawing.Point(252, 52)
        Me.img_tp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.img_tp.Name = "img_tp"
        Me.img_tp.Size = New System.Drawing.Size(49, 25)
        Me.img_tp.TabIndex = 1
        Me.img_tp.TabStop = False
        '
        'img_bp
        '
        Me.img_bp.Location = New System.Drawing.Point(252, 16)
        Me.img_bp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.img_bp.Name = "img_bp"
        Me.img_bp.Size = New System.Drawing.Size(49, 25)
        Me.img_bp.TabIndex = 1
        Me.img_bp.TabStop = False
        '
        'lbl_Thickness
        '
        Me.lbl_Thickness.AutoSize = True
        Me.lbl_Thickness.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_Thickness.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Thickness.Location = New System.Drawing.Point(15, 52)
        Me.lbl_Thickness.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Thickness.Name = "lbl_Thickness"
        Me.lbl_Thickness.Size = New System.Drawing.Size(72, 13)
        Me.lbl_Thickness.TabIndex = 0
        Me.lbl_Thickness.Text = "lbl_Thickness"
        Me.lbl_Thickness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.lbl_Acier.Size = New System.Drawing.Size(472, 37)
        Me.lbl_Acier.TabIndex = 1
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DefinitionAcier
        '
        Me.pan_DefinitionAcier.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DefinitionAcier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefinitionAcier.Controls.Add(Me.lbl_ReductionCurve)
        Me.pan_DefinitionAcier.Controls.Add(Me.lbl_Qualite)
        Me.pan_DefinitionAcier.Controls.Add(Me.lbl_Grade)
        Me.pan_DefinitionAcier.Controls.Add(Me.GridAciers)
        Me.pan_DefinitionAcier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefinitionAcier.Location = New System.Drawing.Point(0, 37)
        Me.pan_DefinitionAcier.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DefinitionAcier.Name = "pan_DefinitionAcier"
        Me.pan_DefinitionAcier.Size = New System.Drawing.Size(472, 587)
        Me.pan_DefinitionAcier.TabIndex = 2
        '
        'lbl_ReductionCurve
        '
        Me.lbl_ReductionCurve.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_ReductionCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ReductionCurve.Location = New System.Drawing.Point(225, 14)
        Me.lbl_ReductionCurve.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ReductionCurve.Name = "lbl_ReductionCurve"
        Me.lbl_ReductionCurve.Size = New System.Drawing.Size(235, 20)
        Me.lbl_ReductionCurve.TabIndex = 24
        Me.lbl_ReductionCurve.Text = "lbl_ReductionCurve"
        Me.lbl_ReductionCurve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Qualite
        '
        Me.lbl_Qualite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Qualite.Location = New System.Drawing.Point(120, 14)
        Me.lbl_Qualite.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Qualite.Name = "lbl_Qualite"
        Me.lbl_Qualite.Size = New System.Drawing.Size(106, 20)
        Me.lbl_Qualite.TabIndex = 23
        Me.lbl_Qualite.Text = "lbl_Qualite"
        Me.lbl_Qualite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Grade
        '
        Me.lbl_Grade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Grade.Location = New System.Drawing.Point(15, 14)
        Me.lbl_Grade.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Grade.Name = "lbl_Grade"
        Me.lbl_Grade.Size = New System.Drawing.Size(106, 20)
        Me.lbl_Grade.TabIndex = 22
        Me.lbl_Grade.Text = "lbl_Grade"
        Me.lbl_Grade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_Acier, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pan_DefinitionAcier, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(472, 624)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TableLayoutPanel1)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(871, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(472, 624)
        Me.pan_Droite.TabIndex = 1
        '
        'TLpan_Saisie
        '
        Me.TLpan_Saisie.ColumnCount = 3
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Saisie.Controls.Add(Me.pan_Droite, 2, 0)
        Me.TLpan_Saisie.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLpan_Saisie.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TLpan_Saisie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Saisie.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Saisie.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Saisie.Name = "TLpan_Saisie"
        Me.TLpan_Saisie.RowCount = 1
        Me.TLpan_Saisie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Saisie.Size = New System.Drawing.Size(1343, 624)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(471, 624)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.pan_Plat, 0, 5)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Plat, 0, 4)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_IFBSection, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.Pan_DimPRS, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Lamine, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_ParentProfile, 0, 0)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 6
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 246.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(471, 624)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'pan_Plat
        '
        Me.pan_Plat.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Plat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Plat.Controls.Add(Me.lbl_InfoFyWP)
        Me.pan_Plat.Controls.Add(Me.cmb_NuancePlat)
        Me.pan_Plat.Controls.Add(Me.lbl_Width)
        Me.pan_Plat.Controls.Add(Me.lbl_Thickness)
        Me.pan_Plat.Controls.Add(Me.lbl_WPSteel)
        Me.pan_Plat.Controls.Add(Me.txt_bp)
        Me.pan_Plat.Controls.Add(Me.txt_tp)
        Me.pan_Plat.Controls.Add(Me.etq_UnitDim4)
        Me.pan_Plat.Controls.Add(Me.etq_UnitDim3)
        Me.pan_Plat.Controls.Add(Me.img_bp)
        Me.pan_Plat.Controls.Add(Me.img_tp)
        Me.pan_Plat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Plat.Location = New System.Drawing.Point(0, 474)
        Me.pan_Plat.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Plat.Name = "pan_Plat"
        Me.pan_Plat.Size = New System.Drawing.Size(471, 150)
        Me.pan_Plat.TabIndex = 7
        '
        'lbl_InfoFyWP
        '
        Me.lbl_InfoFyWP.AutoSize = True
        Me.lbl_InfoFyWP.Location = New System.Drawing.Point(256, 124)
        Me.lbl_InfoFyWP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_InfoFyWP.Name = "lbl_InfoFyWP"
        Me.lbl_InfoFyWP.Size = New System.Drawing.Size(86, 16)
        Me.lbl_InfoFyWP.TabIndex = 38
        Me.lbl_InfoFyWP.Text = "lbl_InfoFyWP"
        '
        'cmb_NuancePlat
        '
        Me.cmb_NuancePlat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NuancePlat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NuancePlat.FormattingEnabled = True
        Me.cmb_NuancePlat.Location = New System.Drawing.Point(252, 92)
        Me.cmb_NuancePlat.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_NuancePlat.Name = "cmb_NuancePlat"
        Me.cmb_NuancePlat.Size = New System.Drawing.Size(150, 24)
        Me.cmb_NuancePlat.TabIndex = 59
        '
        'lbl_Width
        '
        Me.lbl_Width.AutoSize = True
        Me.lbl_Width.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_Width.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Width.Location = New System.Drawing.Point(15, 16)
        Me.lbl_Width.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Width.Name = "lbl_Width"
        Me.lbl_Width.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Width.TabIndex = 0
        Me.lbl_Width.Text = "lbl_Width"
        Me.lbl_Width.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_WPSteel
        '
        Me.lbl_WPSteel.AutoSize = True
        Me.lbl_WPSteel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_WPSteel.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_WPSteel.Location = New System.Drawing.Point(19, 96)
        Me.lbl_WPSteel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_WPSteel.Name = "lbl_WPSteel"
        Me.lbl_WPSteel.Size = New System.Drawing.Size(65, 13)
        Me.lbl_WPSteel.TabIndex = 35
        Me.lbl_WPSteel.Text = "lbl_WPSteel"
        Me.lbl_WPSteel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Plat
        '
        Me.lbl_Plat.AutoSize = True
        Me.lbl_Plat.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Plat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Plat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Plat.Location = New System.Drawing.Point(0, 438)
        Me.lbl_Plat.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.lbl_Plat.Name = "lbl_Plat"
        Me.lbl_Plat.Size = New System.Drawing.Size(471, 36)
        Me.lbl_Plat.TabIndex = 6
        Me.lbl_Plat.Text = "lbl_Plat"
        Me.lbl_Plat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_IFBSection
        '
        Me.lbl_IFBSection.AutoSize = True
        Me.lbl_IFBSection.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_IFBSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_IFBSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_IFBSection.Location = New System.Drawing.Point(0, 284)
        Me.lbl_IFBSection.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.lbl_IFBSection.Name = "lbl_IFBSection"
        Me.lbl_IFBSection.Size = New System.Drawing.Size(471, 36)
        Me.lbl_IFBSection.TabIndex = 5
        Me.lbl_IFBSection.Text = "lbl_IFBSection"
        Me.lbl_IFBSection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pan_DimPRS
        '
        Me.Pan_DimPRS.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_DimPRS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_DimPRS.Controls.Add(Me.chk_Hw)
        Me.Pan_DimPRS.Controls.Add(Me.chk_Ht)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_Info)
        Me.Pan_DimPRS.Controls.Add(Me.etq_UnitDim1)
        Me.Pan_DimPRS.Controls.Add(Me.etq_UnitDim2)
        Me.Pan_DimPRS.Controls.Add(Me.txt_ha)
        Me.Pan_DimPRS.Controls.Add(Me.txt_hw)
        Me.Pan_DimPRS.Controls.Add(Me.img_ha)
        Me.Pan_DimPRS.Controls.Add(Me.img_hw)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_Height)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_Web)
        Me.Pan_DimPRS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_DimPRS.Location = New System.Drawing.Point(0, 320)
        Me.Pan_DimPRS.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_DimPRS.Name = "Pan_DimPRS"
        Me.Pan_DimPRS.Size = New System.Drawing.Size(471, 117)
        Me.Pan_DimPRS.TabIndex = 4
        '
        'chk_Hw
        '
        Me.chk_Hw.AutoSize = True
        Me.chk_Hw.Location = New System.Drawing.Point(224, 50)
        Me.chk_Hw.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_Hw.Name = "chk_Hw"
        Me.chk_Hw.Size = New System.Drawing.Size(15, 14)
        Me.chk_Hw.TabIndex = 34
        Me.chk_Hw.UseVisualStyleBackColor = True
        '
        'chk_Ht
        '
        Me.chk_Ht.AutoSize = True
        Me.chk_Ht.Location = New System.Drawing.Point(224, 11)
        Me.chk_Ht.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_Ht.Name = "chk_Ht"
        Me.chk_Ht.Size = New System.Drawing.Size(15, 14)
        Me.chk_Ht.TabIndex = 33
        Me.chk_Ht.UseVisualStyleBackColor = True
        '
        'lbl_Info
        '
        Me.lbl_Info.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Info.Location = New System.Drawing.Point(23, 82)
        Me.lbl_Info.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Info.Name = "lbl_Info"
        Me.lbl_Info.Size = New System.Drawing.Size(437, 16)
        Me.lbl_Info.TabIndex = 4
        Me.lbl_Info.Text = "lbl_Info"
        Me.lbl_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(385, 15)
        Me.etq_UnitDim1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim1.TabIndex = 3
        Me.etq_UnitDim1.Text = "mm"
        '
        'etq_UnitDim2
        '
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(385, 50)
        Me.etq_UnitDim2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim2.TabIndex = 3
        Me.etq_UnitDim2.Text = "mm"
        '
        'txt_ha
        '
        Me.txt_ha.Location = New System.Drawing.Point(300, 11)
        Me.txt_ha.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_ha.Name = "txt_ha"
        Me.txt_ha.Size = New System.Drawing.Size(76, 22)
        Me.txt_ha.TabIndex = 2
        '
        'txt_hw
        '
        Me.txt_hw.Location = New System.Drawing.Point(300, 47)
        Me.txt_hw.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_hw.Name = "txt_hw"
        Me.txt_hw.Size = New System.Drawing.Size(76, 22)
        Me.txt_hw.TabIndex = 2
        '
        'img_ha
        '
        Me.img_ha.Location = New System.Drawing.Point(252, 11)
        Me.img_ha.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.img_ha.Name = "img_ha"
        Me.img_ha.Size = New System.Drawing.Size(49, 25)
        Me.img_ha.TabIndex = 1
        Me.img_ha.TabStop = False
        '
        'img_hw
        '
        Me.img_hw.Location = New System.Drawing.Point(252, 47)
        Me.img_hw.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.img_hw.Name = "img_hw"
        Me.img_hw.Size = New System.Drawing.Size(49, 25)
        Me.img_hw.TabIndex = 1
        Me.img_hw.TabStop = False
        '
        'lbl_Height
        '
        Me.lbl_Height.AutoSize = True
        Me.lbl_Height.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_Height.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Height.Location = New System.Drawing.Point(15, 11)
        Me.lbl_Height.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Height.Name = "lbl_Height"
        Me.lbl_Height.Size = New System.Drawing.Size(54, 13)
        Me.lbl_Height.TabIndex = 0
        Me.lbl_Height.Text = "lbl_Height"
        Me.lbl_Height.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Web
        '
        Me.lbl_Web.AutoSize = True
        Me.lbl_Web.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_Web.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Web.Location = New System.Drawing.Point(15, 47)
        Me.lbl_Web.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Web.Name = "lbl_Web"
        Me.lbl_Web.Size = New System.Drawing.Size(46, 13)
        Me.lbl_Web.TabIndex = 0
        Me.lbl_Web.Text = "lbl_Web"
        Me.lbl_Web.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pan_Lamine
        '
        Me.pan_Lamine.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Lamine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Lamine.Controls.Add(Me.lbl_Profiles)
        Me.pan_Lamine.Controls.Add(Me.Grid_ProfilesSup)
        Me.pan_Lamine.Controls.Add(Me.lbl_Gamme)
        Me.pan_Lamine.Controls.Add(Me.lst_GammeS)
        Me.pan_Lamine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Lamine.Location = New System.Drawing.Point(0, 37)
        Me.pan_Lamine.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Lamine.Name = "pan_Lamine"
        Me.pan_Lamine.Size = New System.Drawing.Size(471, 246)
        Me.pan_Lamine.TabIndex = 3
        '
        'lbl_Profiles
        '
        Me.lbl_Profiles.AutoSize = True
        Me.lbl_Profiles.Location = New System.Drawing.Point(176, 7)
        Me.lbl_Profiles.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Profiles.Name = "lbl_Profiles"
        Me.lbl_Profiles.Size = New System.Drawing.Size(73, 16)
        Me.lbl_Profiles.TabIndex = 19
        Me.lbl_Profiles.Text = "lbl_Profiles"
        Me.lbl_Profiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Grid_ProfilesSup.Location = New System.Drawing.Point(172, 31)
        Me.Grid_ProfilesSup.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Grid_ProfilesSup.MultiSelect = False
        Me.Grid_ProfilesSup.Name = "Grid_ProfilesSup"
        Me.Grid_ProfilesSup.RowHeadersVisible = False
        Me.Grid_ProfilesSup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_ProfilesSup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_ProfilesSup.ShowCellToolTips = False
        Me.Grid_ProfilesSup.Size = New System.Drawing.Size(287, 197)
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
        'lbl_Gamme
        '
        Me.lbl_Gamme.AutoSize = True
        Me.lbl_Gamme.Location = New System.Drawing.Point(9, 7)
        Me.lbl_Gamme.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Gamme.Name = "lbl_Gamme"
        Me.lbl_Gamme.Size = New System.Drawing.Size(76, 16)
        Me.lbl_Gamme.TabIndex = 16
        Me.lbl_Gamme.Text = "lbl_Gamme"
        Me.lbl_Gamme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lst_GammeS
        '
        Me.lst_GammeS.FormattingEnabled = True
        Me.lst_GammeS.ItemHeight = 16
        Me.lst_GammeS.Location = New System.Drawing.Point(8, 31)
        Me.lst_GammeS.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lst_GammeS.Name = "lst_GammeS"
        Me.lst_GammeS.Size = New System.Drawing.Size(157, 196)
        Me.lst_GammeS.TabIndex = 17
        '
        'lbl_ParentProfile
        '
        Me.lbl_ParentProfile.AutoSize = True
        Me.lbl_ParentProfile.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ParentProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ParentProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ParentProfile.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ParentProfile.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ParentProfile.Name = "lbl_ParentProfile"
        Me.lbl_ParentProfile.Size = New System.Drawing.Size(471, 37)
        Me.lbl_ParentProfile.TabIndex = 1
        Me.lbl_ParentProfile.Text = "lbl_ParentProfile"
        Me.lbl_ParentProfile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.pan_Acier, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.img_Section, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(471, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(400, 624)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'pan_Acier
        '
        Me.pan_Acier.Controls.Add(Me.btn_FyFu)
        Me.pan_Acier.Controls.Add(Me.cmb_ReductionCurveWP)
        Me.pan_Acier.Controls.Add(Me.cmb_GradeWP)
        Me.pan_Acier.Controls.Add(Me.img_ReductionCurve)
        Me.pan_Acier.Location = New System.Drawing.Point(1, 312)
        Me.pan_Acier.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.pan_Acier.Name = "pan_Acier"
        Me.pan_Acier.Size = New System.Drawing.Size(327, 289)
        Me.pan_Acier.TabIndex = 3
        '
        'btn_FyFu
        '
        Me.btn_FyFu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_FyFu.Image = CType(resources.GetObject("btn_FyFu.Image"), System.Drawing.Image)
        Me.btn_FyFu.Location = New System.Drawing.Point(285, 4)
        Me.btn_FyFu.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_FyFu.Name = "btn_FyFu"
        Me.btn_FyFu.Size = New System.Drawing.Size(37, 34)
        Me.btn_FyFu.TabIndex = 2
        Me.btn_FyFu.UseVisualStyleBackColor = True
        '
        'cmb_ReductionCurveWP
        '
        Me.cmb_ReductionCurveWP.FormattingEnabled = True
        Me.cmb_ReductionCurveWP.Location = New System.Drawing.Point(152, 206)
        Me.cmb_ReductionCurveWP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_ReductionCurveWP.Name = "cmb_ReductionCurveWP"
        Me.cmb_ReductionCurveWP.Size = New System.Drawing.Size(99, 24)
        Me.cmb_ReductionCurveWP.TabIndex = 36
        Me.cmb_ReductionCurveWP.Visible = False
        '
        'cmb_GradeWP
        '
        Me.cmb_GradeWP.FormattingEnabled = True
        Me.cmb_GradeWP.Location = New System.Drawing.Point(60, 206)
        Me.cmb_GradeWP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_GradeWP.Name = "cmb_GradeWP"
        Me.cmb_GradeWP.Size = New System.Drawing.Size(65, 24)
        Me.cmb_GradeWP.TabIndex = 37
        Me.cmb_GradeWP.Visible = False
        '
        'img_ReductionCurve
        '
        Me.img_ReductionCurve.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_ReductionCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_ReductionCurve.Location = New System.Drawing.Point(45, 28)
        Me.img_ReductionCurve.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.img_ReductionCurve.Name = "img_ReductionCurve"
        Me.img_ReductionCurve.Size = New System.Drawing.Size(133, 41)
        Me.img_ReductionCurve.TabIndex = 1
        Me.img_ReductionCurve.TabStop = False
        '
        'img_Section
        '
        Me.img_Section.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Section.Location = New System.Drawing.Point(1, 0)
        Me.img_Section.Margin = New System.Windows.Forms.Padding(1, 0, 1, 1)
        Me.img_Section.Name = "img_Section"
        Me.img_Section.Size = New System.Drawing.Size(133, 61)
        Me.img_Section.TabIndex = 0
        Me.img_Section.TabStop = False
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_Saisie)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1343, 624)
        Me.pan_Main.TabIndex = 1
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(689, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(502, 4)
        Me.btn_Annuler.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(152, 33)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Provi
        '
        Me.pan_Provi.Location = New System.Drawing.Point(849, 4)
        Me.pan_Provi.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Provi.Name = "pan_Provi"
        Me.pan_Provi.Size = New System.Drawing.Size(348, 33)
        Me.pan_Provi.TabIndex = 2
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.pan_Provi, 4, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 636)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1343, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1351, 681)
        Me.pan_General.TabIndex = 5
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.pan_Main, 0, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(1351, 681)
        Me.TLpan_Main.TabIndex = 0
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'imgList_UY
        '
        Me.imgList_UY.ImageStream = CType(resources.GetObject("imgList_UY.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_UY.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_UY.Images.SetKeyName(0, "Fu")
        Me.imgList_UY.Images.SetKeyName(1, "Fy")
        '
        'Frm_SectionIFB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1351, 681)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_SectionIFB"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_SectionIFB_A"
        CType(Me.GridAciers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_tp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_DefinitionAcier.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pan_Droite.ResumeLayout(False)
        Me.TLpan_Saisie.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.pan_Plat.ResumeLayout(False)
        Me.pan_Plat.PerformLayout()
        Me.Pan_DimPRS.ResumeLayout(False)
        Me.Pan_DimPRS.PerformLayout()
        CType(Me.img_ha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Lamine.ResumeLayout(False)
        Me.pan_Lamine.PerformLayout()
        CType(Me.Grid_ProfilesSup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.pan_Acier.ResumeLayout(False)
        CType(Me.img_ReductionCurve, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Section, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridAciers As DataGridView
    Friend WithEvents Col_Grade As DataGridViewTextBoxColumn
    Friend WithEvents Col_Qualite As DataGridViewTextBoxColumn
    Friend WithEvents Col_ReductionCurve As DataGridViewTextBoxColumn
    Friend WithEvents etq_UnitDim4 As Label
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents txt_tp As TextBox
    Friend WithEvents txt_bp As TextBox
    Friend WithEvents img_tp As PictureBox
    Friend WithEvents img_bp As PictureBox
    Friend WithEvents lbl_Thickness As Label
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents pan_DefinitionAcier As Panel
    Friend WithEvents lbl_ReductionCurve As Label
    Friend WithEvents lbl_Qualite As Label
    Friend WithEvents lbl_Grade As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLpan_Saisie As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_IFBSection As Label
    Friend WithEvents Pan_DimPRS As Panel
    Friend WithEvents lbl_Width As Label
    Friend WithEvents pan_Lamine As Panel
    Friend WithEvents lbl_Profiles As Label
    Friend WithEvents Grid_ProfilesSup As DataGridView
    Friend WithEvents Col_ListeSup As DataGridViewTextBoxColumn
    Friend WithEvents Col_HISTARSup As DataGridViewTextBoxColumn
    Friend WithEvents lbl_Gamme As Label
    Friend WithEvents lst_GammeS As ListBox
    Friend WithEvents lbl_ParentProfile As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents img_Section As PictureBox
    Friend WithEvents pan_Main As Panel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Provi As Panel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents txt_hw As TextBox
    Friend WithEvents img_hw As PictureBox
    Friend WithEvents lbl_Web As Label
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents txt_ha As TextBox
    Friend WithEvents img_ha As PictureBox
    Friend WithEvents lbl_Height As Label
    Friend WithEvents lbl_Info As Label
    Friend WithEvents chk_Hw As CheckBox
    Friend WithEvents chk_Ht As CheckBox
    Friend WithEvents lbl_InfoFyWP As Label
    Friend WithEvents cmb_ReductionCurveWP As ComboBox
    Friend WithEvents cmb_GradeWP As ComboBox
    Friend WithEvents lbl_WPSteel As Label
    Friend WithEvents pan_Acier As Panel
    Friend WithEvents btn_FyFu As Button
    Friend WithEvents img_ReductionCurve As PictureBox
    Friend WithEvents imgList_UY As ImageList
    Friend WithEvents pan_Plat As Panel
    Friend WithEvents lbl_Plat As Label
    Friend WithEvents cmb_NuancePlat As ComboBox
End Class
